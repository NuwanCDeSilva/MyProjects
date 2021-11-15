jQuery(document).ready(function () {
    //jQuery(".add-account").click(function () {
    //    jQuery(".new-acc-col").append('<div class="col-sm-6 new-add-acc">' +
    //                    '<div class="col-sm-2">Debit Account</div>' +
    //                    '<div class="col-sm-4"><input type="text" class="form-control" /></div>' +
    //                    '<div class="col-sm-3">-Account Description-</div>' +
    //                    '<div class="col-sm-1"><img onclick="removeAcc(this)" class="remove-account" src="/Content/images/remove.png" alt="" width="15" height="15"></div>' +
    //                '</div>');
    //});
    jQuery(".btn-approve-data").hide();
    jQuery(".btn-reject-data").hide();
    jQuery(".btn-save-data").val("Save")
    var date = new Date();
    jQuery('#ReqDate').val(my_date_format(date));
    pendingRequestData();
   //jQuery("#ReqDate").datepicker({ dateFormat: "dd/M/yy" });
    //jQuery("#GrosAmt").on("input", function () {
    //    if (jQuery.isNumeric(jQuery(this).val().replace(",", ""))) {
    //        updateNetAmount();
    //    } else {
    //        if (jQuery(this).val() != "") {
    //            setInfoMsg("Please enter valid amount to Gross Amount");
    //            jQuery(this).val("");
    //        }
    //        updateNetAmount();
    //    }
    //});
    jQuery("#Tax").on("input", function () {
        if (jQuery.isNumeric(jQuery(this).val().replace(",", ""))) {
            updateNetAmount();
        } else {
            if (jQuery(this).val() != "") {
                setInfoMsg("Please enter valid amount to TAX");
                jQuery(this).val(""); 
            }
            updateNetAmount();
        }
    });
    jQuery(".btn-clear-data").click(function () {
        Lobibox.confirm({
            msg: "Do you want to clear form ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    clearForm();
                }
            }
        });
    });
    jQuery(".srch-req-no").click(function () {
        var headerKeys = Array();
        headerKeys = ["Row", "Request #", "Date","Pay Type","Creditor","Net Amount"];
        field = "SRCHPAYREQ";
        var data = jQuery("#DropdownReqTp").val();
        var x = new CommonSearch(headerKeys, field, data);
    });

    jQuery("#ReqNo").focusout(function () {
        if (jQuery(this).val() != "") {
            loadReqDet(jQuery(this).val());
        }
    });
    jQuery("#GrosAmt").focusout( function () {
        if (jQuery.isNumeric(jQuery(this).val().replace(",", ""))) {
            jQuery(this).val(addCommas(parseFloat(jQuery(this).val().replace(",", "")).toFixed(2)));
        } 
    });
    jQuery("#Tax").focusout(function () {
        if (jQuery.isNumeric(jQuery(this).val().replace(",",""))) {
            jQuery(this).val(addCommas(parseFloat(jQuery(this).val().replace(",", "")).toFixed(2)));
        }
    });

    jQuery(".btn-save-data").click(function () {
        var formdata = jQuery("#parreqfrm").serialize();
        jQuery.ajax({
            cache: false,
            type: "POST",
            url: "/PaymentRequest/savePaymentRequest?operation=" + jQuery(".btn-save-data").val(),
            data:formdata,
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        setSuccesssMsg(result.msg);
                        clearForm();
                    } else {
                        setInfoMsg(result.msg);
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    jQuery(".add-account").click(function () {
        AddAccountDetails();
    });
    jQuery("#Creditor").focusout(function () {
        if (jQuery(this).val() != "") {
            var cde = jQuery(this).val();
            creditoFocusout(cde);
        }
    });
    jQuery(".rch-debtacc-no").click(function () {
        var headerKeys = Array();
        headerKeys = ["Row", "Code","Description"];
        field = "SRCHDEBTACC";
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".rch-creacc-no").click(function () {
        var headerKeys = Array();
        headerKeys = ["Row", "Code","Description"];
        field = "SRCHCREACC";
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".srch-pay-tp").click(function () {
        var headerKeys = Array();
        headerKeys = ["Row", "Code","Description"];
        field = "SRCHPYTP";
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery(".tax-search").click(function () {
        if (jQuery("#Creditor").val() != "") {
            var headerKeys = Array();
            var data = jQuery("#Creditor").val();
            headerKeys = ["Row", "Code", "Description", "Rate"];
            field = "SRCHTAX";
            var x = new CommonSearch(headerKeys, field, data);
        } else {
            setInfoMsg("Please select creditor.");
        }
    });

    jQuery(".srch-ref-no").click(function () {
        if (jQuery("#Creditor").val() != "") {
            if (jQuery("#PTTYCSH").val() != "") {
                var headerKeys = Array();
                var data = { creditor: jQuery("#Creditor").val(), type: jQuery("#DropdownReqTp").val() };
                headerKeys = ["Row", "Purchase Order", "Cost", ""];
                field = "SRCHREFNO";
                var x = new CommonSearch(headerKeys, field, data);
            } else {
                setInfoMsg("Cannot add refference for Petty cash type.");
            }
        } else {
            setInfoMsg("Please select creditor.");
        }
    });
    jQuery("#TaxCode").focusout(function () {
        if (jQuery(this).val()) {
            taxTypeFocusOut(jQuery(this).val());
        }
    });
    jQuery("#PayType").focusout(function () {
        if (jQuery(this).val()) {
            payTypeFocusOut(jQuery(this).val());
        }
    });
    jQuery("#AccNo").focusout(function () {
        if (jQuery(this).val()) {
            jQuery(".debitor-name").html("--");
            debitorFocusout(jQuery(this).val());
            debFocusout(jQuery(this).val());
        } else {
            jQuery(".template-itemval").empty();
            jQuery(".dynamic-table").empty();
        }
    });
    jQuery("#AccAmount").on("keydown", function (evt) {
        if (evt.keyCode == 13) {
            AddAccountDetails();
        }
    });
    jQuery("#GrosAmt").focusout(function () {
        if (jQuery(this).val() != "") {
            if (jQuery("#Creditor").val() != "") {
                var amt = jQuery(this).val();                 
                if (parseFloat(amt) > 0) {
                    jQuery.ajax({
                        type: "GET",
                        url: "/PaymentRequest/validateAmtAndTax?amount=" + amt + "&creditor="+jQuery("#Creditor").val(),
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    if (result.auto == 1) {
                                        jQuery("#NetAmt").val(addCommas(parseFloat(result.netAmt).toFixed(2)));
                                        jQuery("#Tax").val(parseFloat(result.totalTax).toFixed(2));
                                        addTaxDetails(result.data, true);
                                    }
                                } else {
                                    setInfoMsg(result.msg);
                                }
                            } else {
                                Logout();
                            }
                        }
                    });
                } else {
                    setInfoMsg("Please enter valid gross amount.");
                }
            } else {
                setInfoMsg("Please select creditor code.");
                jQuery("#Creditor").focus();
            }
        }
    });
    jQuery(".btn-approve-data").click(function () {
        if (jQuery("#ReqNo").val() != "") {
            Lobibox.confirm({
                msg: "Do you want to " + jQuery(".btn-approve-data").val() + " request ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "POST",
                            url: "/PaymentRequest/approveRequest?ReqNo=" + jQuery("#ReqNo").val()+"&val="+ jQuery(".btn-approve-data").val(),
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        clearForm();
                                        setSuccesssMsg(result.msg);
                                    } else {
                                        setInfoMsg(result.msg);
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
            setInfoMsg("Please select valid request number.");
        }
    });
    jQuery(".btn-process-data").click(function () {
        if (jQuery("#ReqNo").val() != "") {
            Lobibox.confirm({
                msg: "Do you want to process " + jQuery("#ReqNo").val() + " request ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "POST",
                            url: "/PaymentRequest/processRequest?ReqNo=" + jQuery("#ReqNo").val()+"&reqtp="+jQuery("#DropdownReqTp").val(),
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        clearForm();
                                        setSuccesssMsg(result.msg);
                                    } else {
                                        setInfoMsg(result.msg);
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
            setInfoMsg("Please select valid request number.");
        }
    });
    jQuery(".btn-reject-data").click(function () {
        jQuery(".rejreq-no").empty();
        if (jQuery("#ReqNo").val() != "") {
            jQuery(".rejreq-no").html(jQuery("#ReqNo").val());
            jQuery('#myModalRejReq').modal({
                keyboard: true,
                backdrop: 'static'
            }, 'show');

        } else {
            setInfoMsg("Please select request to reject.");
        }
    });
    jQuery(".btn-rejectapp-data").click(function () {
        if (jQuery("#ReqNo").val() != "") {
            Lobibox.confirm({
                msg: "Do you want to " + jQuery(".btn-rejectapp-data").val() + " request ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery('#myModalRejReq').modal("hide");
                        jQuery.ajax({
                            type: "POST",
                            url: "/PaymentRequest/approveRequest?ReqNo=" + jQuery("#ReqNo").val() + "&val=" + jQuery(".btn-rejectapp-data").val() + "&reason=" + jQuery("#RejectReason").val(),
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        clearForm();
                                        setSuccesssMsg(result.msg);

                                    } else {
                                        setInfoMsg(result.msg);
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
            setInfoMsg("Please select valid request number.");
        }
    });

    jQuery(".add-tax-data").click(function () {
        if (jQuery("#Creditor").val() != "") {
            jQuery(".itm-tax-table tr.new-row").remove();
            if (jQuery(".TaxCode").val()!="" && jQuery(".TaxAmount").val()!="") {
                jQuery.ajax({
                    cache: false,
                    async: false,
                    type: "GET",
                    url: "/PaymentRequest/addRequestTax?accNo=" + jQuery("#Creditor").val() + "&TaxCode=" + jQuery("#TaxCode").val() + "&TaxAmount=" + jQuery("#TaxAmount").val(),
                    success: function (result) {
                        if (result.login == true) {
                            if (result.success == true) {
                                addTaxDetails(result.data,false);
                            } else {
                                setInfoMsg(result.msg);
                            }
                        } else {
                            Logout();
                        }
                    }
                });
            } else {
                setInfoMsg("Please add tax code and amount.");
            }
        } else {
            setInfoMsg("Please select creditor.");
        }
    });
    jQuery("#DropdownReqTp").change(function () {
        clearForm(false);
        pendingRequestData();
        if (jQuery(this).val() != "PTTYCSH") {
            jQuery('#Tax').attr('readonly', 'true');
            jQuery("#GrosAmt").attr('readonly', 'true');
        } else {
            jQuery('#Tax').removeAttr("readonly");
            jQuery("#GrosAmt").removeAttr("readonly");
        }
    });
});
function AddAccountDetails() {
    if (jQuery("#AccNo").val() != "" && jQuery("#AccAmount").val() != "") {
        jQuery.ajax({
            cache: false,
            async: false,
            type: "GET",
            url: "/PaymentRequest/addAccountDetails?accNo=" + jQuery("#AccNo").val() + "&amount=" + jQuery("#AccAmount").val(),
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        jQuery("table.acc-detail-tbl tr.new-row").remove();
                        jQuery(".acc-total").empty();
                        jQuery("#AccNo").val("");
                        jQuery("#AccAmount").val("");
                        addAccountDetails(result.data);
                    } else {
                        setInfoMsg(result.msg);
                    }
                } else {
                    Logout();
                }
            }
        });
    } else {
        setInfoMsg("Please add account code and amount.");
    }
}
//function payAccFocusOut(value) {
//    jQuery.ajax({
//        cache: false,
//        type: "GET",
//        url: "/PaymentRequest/validateAccountCde?code=" + value,
//        success: function (result) {
//            if (result.login == true) {
//                if (result.success == true) {

//                } else {
//                    jQuery("#AccNo").val("");
//                    setInfoMsg(result.msg);
//                }
//            } else {
//                Logout();
//            }
//        }
//    });
//}
function payTypeFocusOut(value) {
    jQuery.ajax({
        cache: false,
        type: "GET",
        url: "/PaymentRequest/validatePayTp?code=" + value,
        success: function (result) {
            if (result.login == true) {
                if (result.success == true) {   

                } else {
                    jQuery("#PayType").val("");
                    setInfoMsg(result.msg);
                }
            } else {
                Logout();
            }
        }
    });
}
function creditoFocusout(cde,creditor)
{
    //jQuery("#GrosAmt").val("");
    jQuery.ajax({
        cache: false,
        async: false,
        type: "GET",
        url: "/PaymentRequest/validateAccountCde?code=" + cde ,
        success: function (result) {
            if (result.login == true) {
                if (result.success == true) {
                  jQuery(".cred-name").html(result.data.RCA_ACC_DESC);
                  if (result.data.RCA_ANAL5 == 1) {
                        jQuery('#Tax').attr('readonly', 'true');
                  } else {
                      jQuery('#Tax').removeAttr("readonly");
                  }
                  //jQuery("#GrosAmt").focus();
                } else {
                    setError(result.msg);
                }
            } else {
                Logout();
            }
        }
    });
}

function debFocusout(cde, creditor) {
    jQuery.ajax({
        cache: false,
        async: false,
        type: "GET",
        url: "/PaymentRequest/validateAccountCde?code=" + cde,
        success: function (result) {
            if (result.login == true) {
                if (result.success == true) {
                        jQuery(".debitor-name").html(result.data.RCA_ACC_DESC);
                } else {
                    setError(result.msg);
                }
            } else {
                Logout();
            }
        }
    });
}
function debitorFocusout(cde, notupdategrd){
    if (notupdategrd == null) {
        notupdategrd = false;
    }


    var val = jQuery("#ReqNo").val();
    jQuery(".item-table table.dynamic-table").empty();
    jQuery(".template-itemval").empty();
    jQuery.ajax({
        cache: false,
        async: false,
        type: "GET",
        url: "/TemplateManager/getSavedItemTemplates?module=PAYREQ&code=" + cde + "&savedVavl=" + val,
        success: function (result) {
            if (result.login == true) {
                if (result.success == true) {
                    if (notupdategrd == false)
                        getSavedDynamicValues(val, cde)
                } else { 
                        setInfoMsg(result.msg);
                }
            } else {
                Logout();
            }
        }
    });
}

function getSavedDynamicValues(reqno, accno) {

    jQuery.ajax({
        cache: false,
        type: "GET",
        async: false,
        //url: "/TemplateManager/getNextTemplateField?module=PAYREQ&code=" + cde + "&seq=" + "0" + "&direction=" + 1,
        url: '/TemplateManager/getItemSavedValues',
        data: { code: accno },
        success: function (result) {
            if (result.login == true) {
                if (result.success == true) {
                    //jQuery(".item-table table.dynamic-table").empty();
                    //drawFormItemTemplateField(result, 0, cde);
                    if (result.det != null) {
                        if (result.det.length > 0) {
                            drawFormTemplateField(result.det);

                            var hed = "";
                            hed += '<th class="fix-width-table">Account Code</th>'
                            for (i = 0; i < result.det[0].TEMPLATE_DET.length; i++) {
                                if (result.det[0].TEMPLATE_DET[i].DEF_VAL_FLD == "1") {
                                    if (result.det[0].TEMPLATE_DET[i].FIELD_TYPE == "NUMBERFIELD") {
                                    hed += '<th class="fix-width-table number">DEBIT'/* + result.det[0].TEMPLATE_DET[i].FIELD_NAME*/ + '</th>'
                                    hed += '<th class="fix-width-table number">CREDIT' /*+ result.det[0].TEMPLATE_DET[i].FIELD_NAME */ + '</th>'
                                } else {
                                    hed += '<th class="fix-width-table">-' + result.det[0].TEMPLATE_DET[i].FIELD_NAME + '</th>'
                                }
                            }else{
                             hed += '<th class="fix-width-table">' + result.det[0].TEMPLATE_DET[i].FIELD_NAME + '</th>'
                            }
                                
                            }
                            hed += '<th class="fix-width-table text_align-center"></th>'
                            hed = '<tr class="hed-row grid-dhr-clm">' + hed + '</tr>';
                            jQuery(".item-table table.dynamic-table").append(hed);
                            if (reqno != "") {
                                jQuery.ajax({
                                    cache: false,
                                    type: "GET",
                                    async: false,
                                    //url: "/TemplateManager/getNextTemplateField?module=PAYREQ&code=" + cde + "&seq=" + "0" + "&direction=" + 1,
                                    url: '/PaymentRequest/getSavedValueDetails',
                                    data: { code: accno, savedVal: reqno },
                                    success: function (respnse) {
                                        if (respnse.login == true) {
                                            if (respnse.success == true) {
                                                if (respnse.hedDet.length > 0) {
                                                    addTempValDet(respnse.value);
                                                    jQuery("#AccNo").val(accno);
                                                    //debitorFocusout(accno);
                                                }
                                            } else {
                                                setInfoMsg(respnse.msg);
                                            }
                                        } else {
                                            Logout();
                                        }
                                    }
                                });
                            }

                        }
                    }

                } else {
                    setInfoMsg(result.msg);
                }

            } else {
                Logout()
            }
        }
    });
    
}
function updateNetAmount() {
    var val = 0;
    if (jQuery("#GrosAmt").val() != "" && jQuery("#Tax").val() != "") {

        val = addCommas(parseFloat(parseFloat(jQuery("#GrosAmt").val().replace(",", "")) + parseFloat(jQuery("#Tax").val().replace(",", ""))).toFixed(2));

    } else if (jQuery("#GrosAmt").val() != "" && jQuery("#Tax").val() == "") {

        val = addCommas(parseFloat(jQuery("#GrosAmt").val().replace(",", "")).toFixed(2));

    } else if (jQuery("#GrosAmt").val() == "" && jQuery("#Tax").val() != "") {

        val = addCommas(parseFloat(jQuery("#Tax").val().replace(",", "")).toFixed(2));
    }
    jQuery("#NetAmt").val(val); 
    
}
function addTempValDet(data) {
    jQuery('#dynamin-val-form')[0].reset();
    if (data.length > 0) {
        for (j = 0; j < data.length; j++) { 
            if (data[j].STUS == 1) {
                var det = "";
                det += '<td class="fix-width-table">' + data[j].ITEMS[0].RTIV_UNQ_CD + '</td>';
                for (i = 0; i < data[j].ITEMS.length; i++) {
                    if (data[j].ITEMS[i].RO_TYPE == "NUMBERFIELD") {
                        if (data[j].ITEMS[i].DEF_VAL_FLD == "1") {
                            if (data[j].RTIV_DIRECT == "0") {
                                det += '<td class="fix-width-table number">' + addCommas(parseFloat(data[j].ITEMS[i].RTIV_VALUE).toFixed(2)) + '</td>'
                                det += '<td class="fix-width-table number">0.00</td>'
                            } else {
                                det += '<td class="fix-width-table number">0.00</td>'
                                det += '<td class="fix-width-table number">-' + addCommas(parseFloat(data[j].ITEMS[i].RTIV_VALUE).toFixed(2)) + '</td>'
                            }
                        } else {
                            det += '<td class="fix-width-table number">' + addCommas(parseFloat(data[j].ITEMS[i].RTIV_VALUE).toFixed(2)) + '</td>'
                        }
                        
                    } else {
                        det += '<td class="fix-width-table">' + data[j].ITEMS[i].RTIV_VALUE + '</td>'
                    }
                }
                det += "<td class='fix-width-table text_align-center'><img data-val=" + data[j].UNQ_VAL + "_" + data[j].RTIV_SEQ + " onclick='removeAddedValue(this)' class='remove-temp-val' src='/Resources/Images/Account/remove.png' alt='' width='20' height='20'></td>";
                det = '<tr class="new-row">' + det + '</tr>';
                jQuery(".item-table table.dynamic-table").append(det);
            }
        }
    }
}
function removeAddedValue(obj) {
    Lobibox.confirm({
        msg: "Do you want to remove detials ?",
        callback: function ($this, type, ev) {
            if (type == "yes") {
                jQuery(".item-table table.dynamic-table .new-row").empty();
                jQuery(".acc-detail-tbl .new-row").remove();
                jQuery(".acc-total").empty();
                var id = jQuery(obj).attr('data-val');
                jQuery.ajax({
                    cache: false,
                    type: "GET",
                    url: "/PaymentRequest/removeRequestItem?detid=" + id,
                    success: function (result) {
                        if (result.login == true) {
                            if (result.success == true) {
                                if (result.value.length > 0) {
                                    
                                    addTempValDet(result.value);
                                    addAccountDetailsNew(result.MST_PAY_REQ_DET);
                                }
                            } else {
                                setInfoMsg(result.msg);
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
function drawFormTemplateField(data) {
    jQuery(".template-itemval").empty();
    jQuery(".dynamic-table").empty();
    if (data != null && data.length > 0) {
        for (j = 0 ; j < data.length; j++) {
            var fieldset = "<fieldset class='filedset_" + data[j].TEMPLATE_ID + "'>";
            var field = "";
            //fieldset += "<legend>" + data[j].TEMPLATE_NAME + "</legend>";
            //fieldset += field;
            fieldset += "</fieldset>";
            jQuery(".template-itemval").append(fieldset);

            for (i = 0; i < data[j].TEMPLATE_DET.length; i++) {

                var sch = "";
                var with_searh = "";
                if (data[j].TEMPLATE_DET[i].IS_HAVE_SEARCH == "1") {
                    with_searh = "withsearch";
                    sch = "<img  id='" + data[j].TEMPLATE_DET[i].DETAIL_ID + "' data-srch='" + data[j].TEMPLATE_DET[i].SEARCH_FLD + "' alt='Search' style='width:20px;' class='seach-dyn-srch srch-icon puls' src='/Resources/images/Account/search.png'>";
                }
                if (data[j].TEMPLATE_DET[i].FIELD_TYPE == "TEXTFIELD") {
                    if (data[j].TEMPLATE_DET[i].IS_HAVE_SEARCH == "1") {
                        field = "<div class='col-sm-3'><label class='sec-label'>" + data[j].TEMPLATE_DET[i].FIELD_NAME + "</label><input readonly='readonly'  class='val-fld " + with_searh + " form-control input-common-width' type='text' name='name_" + data[j].TEMPLATE_DET[i].FIELD_TYPE + "_" + data[j].TEMPLATE_ID + "_" + data[j].TEMPLATE_DET[i].DETAIL_ID + "'>" + sch + "</div>";
                    } else {
                        field = "<div class='col-sm-3'><label class='sec-label'>" + data[j].TEMPLATE_DET[i].FIELD_NAME + "</label><input   class='val-fld " + with_searh + " form-control input-common-width' type='text' name='name_" + data[j].TEMPLATE_DET[i].FIELD_TYPE + "_" + data[j].TEMPLATE_ID + "_" + data[j].TEMPLATE_DET[i].DETAIL_ID + "'>" + sch + "</div>";

                    }
                }
                else if (data[j].TEMPLATE_DET[i].FIELD_TYPE == "COMBOBOX") {
                    field = "<div class='col-sm-2'><label class='sec-label'>" + data[j].TEMPLATE_DET[i].FIELD_NAME + "</label><select id='COMBOBOX_ID_" + data[j].TEMPLATE_DET[i].DETAIL_ID + "'  class='form-control input-common-width COMBOBOX_" + data[j].TEMPLATE_DET[i].DETAIL_ID + " combobox-dynamictval'   data-val='" + data[j].TEMPLATE_DET[i].DETAIL_ID + "' name='name_" + data[j].TEMPLATE_DET[i].FIELD_TYPE + "_" + +data[j].TEMPLATE_ID + "_" + +data[j].TEMPLATE_DET[i].DETAIL_ID + "'>"
                        + "<option value=''>Select</option>"
                            + "</select></div>";
                }
                else if (data[j].TEMPLATE_DET[i].FIELD_TYPE == "DATEFIELD") {
                    field = "<div class='col-sm-3'><label class='sec-label'>" + data[j].TEMPLATE_DET[i].FIELD_NAME + "</label><input   readonly type='text' class='form-control input-common-width date-field' name='name_" + data[j].TEMPLATE_DET[i].FIELD_TYPE + "_" + +data[j].TEMPLATE_ID + "_" + data[j].TEMPLATE_DET[i].DETAIL_ID + "'></div>";

                }
                else if (data[j].TEMPLATE_DET[i].FIELD_TYPE == "NUMBERFIELD") {
                    field = "<div class='col-sm-3'><label class='sec-label'>" + data[j].TEMPLATE_DET[i].FIELD_NAME + "</label><input oninput='validateNumberField(this)'  class='number form-control input-common-width' type='text' name='name_" + data[j].TEMPLATE_DET[i].FIELD_TYPE + "_" + data[j].TEMPLATE_ID + "_" + data[j].TEMPLATE_DET[i].DETAIL_ID + "'></div>";
                } else if (data[j].TEMPLATE_DET[i].FIELD_TYPE == "CHECKBOX") {
                    field = "<div class='col-sm-1'><label class='sec-label'>" + data[j].TEMPLATE_DET[i].FIELD_NAME + "</label><input style='display:none;' type='text' class='chkbx-fld-val form-control' name='name_" + data[j].TEMPLATE_DET[i].FIELD_TYPE + "_" + data[j].TEMPLATE_ID + "_" + data[j].TEMPLATE_DET[i].DETAIL_ID + "'/><input  class='chkbx-fld form-control input-common-width' type='checkbox'/></div>";
                }
                jQuery(".template-itemval ." + "filedset_" + data[j].TEMPLATE_ID).append(field);

            }
            jQuery(".template-itemval .date-field").datepicker({ dateFormat: "dd/M/yy" });
            jQuery('.chkbx-fld-val').val("CR");

            jQuery('.chkbx-fld').change(function () {
                cb = jQuery(this);
                if (cb.prop('checked')) {
                    jQuery(jQuery(this).siblings('input.chkbx-fld-val')).val("DR");
                } else {
                    jQuery('.chkbx-fld-val').val("CR");
                }
            });
        }
       
        jQuery(".template-itemval .filedset_" + data[0].TEMPLATE_ID).append('<label class="sec-label"></label><div style="margin-top: 25px;" class="col-sm-1"><img class="add-field-data plus" src="/Resources/Images/Account/Dpwnarrow.png" alt="" width="20" height="20"></div>');
        addRandomDataEvent();
        $(".template-itemval .combobox-dynamictval").each(function () {
            var id = jQuery(this).attr('data-val');
            var vale = jQuery(this).attr('data-selected');
            if (jQuery(jQuery(jQuery(this)).find("option")).length <= 1) {
                jQuery.ajax({
                    cache: false,
                    type: "GET",
                    url: "/TemplateManager/getComboDetails?detid=" + id,
                    success: function (result) {
                        if (result.login == true) {
                            if (result.success == true) {
                                if (result.data.length > 0) {
                                    var opt = "";
                                    for (i = 0 ; i < result.data.length; i++) {
                                        var selected = "";
                                        //var selected = (result.data[i].ROLD_CODE == vale) ? " selectd = 1 " : "";
                                        opt += "<option " + selected + " value=" + result.data[i].ROLD_CODE + ">" + result.data[i].ROLD_NAME + "</option>";
                                    }
                                    jQuery(".template-itemval .COMBOBOX_" + id).html(opt);
                                    jQuery(".template-itemval .COMBOBOX_" + id).val(vale);
                                }
                            } else {
                                setInfoMsg(result.msg);
                            }
                        } else {
                            Logout();
                        }
                    }
                });
            }

        });
        jQuery(".seach-dyn-srch").click(function () {
            var srch = jQuery(this).attr('data-srch');
            var txtFld = jQuery(this).siblings("input.val-fld");
            var headerKeys = Array();
            headerKeys = ["Row", "Code", "Description"];
            field = "COMSRCHFLD";
            data = { searchFld: srch, txtFld: txtFld }
            var x = new CommonSearch(headerKeys, field, data);
        });
        //if (jQuery('.template-itemval .combobox-dynamictval').length > 0) {
        //    jQuery('.template-itemval .combobox-dynamictval').click(function () {
        //        var id = jQuery(this).attr('data-val');
        //        var vale = jQuery(this).attr('data-selected');
        //        console.log(vale);
        //        if (jQuery(jQuery(jQuery(this)).find("option")).length <= 1) {
        //            jQuery.ajax({
        //                cache: false,
        //                type: "GET",
        //                url: "/TemplateManager/getComboDetails?detid=" + id,
        //                success: function (result) {
        //                    if (result.login == true) {
        //                        if (result.success == true) {
        //                            if (result.data.length > 0) {
        //                                var opt = "";
        //                                for (i = 0 ; i < result.data.length; i++) {
        //                                    var selected = "";
        //                                    //var selected = (result.data[i].ROLD_CODE == vale) ? " selectd = 1 " : "";
        //                                    opt += "<option "+selected+" value=" + result.data[i].ROLD_CODE + ">" + result.data[i].ROLD_NAME + "</option>";
        //                                }
        //                                jQuery(".template-itemval .COMBOBOX_" + id).html(opt);
        //                                jQuery(".template-itemval .COMBOBOX_" + id).val(vale);
        //                            }
        //                        } else {
        //                            setInfoMsg(result.msg);
        //                        }
        //                    } else {
        //                        Logout();
        //                    }
        //                }
        //            });
        //        }

        //    });

        //}
    }
}
function addRandomDataEvent() {
    jQuery(".add-field-data").click(function () {
        var formdata = jQuery("#dynamin-val-form").serialize();
        var cre = 0;
        if (jQuery(".iscredit").prop("checked")) {
            cre = 1;
        }
        jQuery.ajax({
            //cache: false,
            type: "POST",
            url: "/PaymentRequest/addNewDynamicValue?code=" + jQuery("#AccNo").val()+"&iscredit="+cre,
            data:  formdata  ,
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        jQuery(".iscredit").prop("checked", false);
                        jQuery(".item-table table.dynamic-table .new-row").empty();
                        addTempValDet(result.ACC_VAL);
                        addAccountDetails(result.MST_PAY_REQ_DET);
                    } else {
                        setInfoMsg(result.msg);
                    }

                } else {
                    Logout()
                }
            }
        });
    });
}

function removeAcc(th) {
    Lobibox.confirm({
        msg: "Do you want to remove account ?",
        callback: function ($this, type, ev) {
            if (type == "yes") {
                var LineNo = jQuery(jQuery(jQuery(th).parent())).siblings("td.LineNo").html();
                jQuery.ajax({
                    cache: false,
                    type: "GET",
                    url: "/PaymentRequest/removeAccountDetaills?module=PAYREQ&LineNo=" + LineNo ,
                    success: function (result) {
                        if (result.login == true) {
                            if (result.success == true) {
                                jQuery("table.acc-detail-tbl tr.new-row").remove();
                                jQuery(".acc-total").empty();
                                addAccountDetails(result.data);
                            } else {
                                setInfoMsg(result.msg);
                            }

                        } else {
                            Logout()
                        }
                    }
                });
                
            }
        }
    });
}
function addAccountDetails(data) {
    jQuery("table.acc-detail-tbl .new-row").remove();
    jQuery(".acc-total").empty();
    if (data.length > 0) {
        var total = 0;
        for (i = 0; i < data.length; i++) {
            //if (data[i].MPRD_STUS == 1) {
                jQuery("table.acc-detail-tbl").append("<tr class='new-row'>" +
                                    "<td class='LineNo nodisplay'>" + data[i].MPRD_ITM_LINE + "</td>" +
                                    "<td class='AccountNo'>" + data[i].MPRD_ACC_NO + "</td>" +
                                    "<td>---</td>" +
                                    "<td class='right-align amount'>" + addCommas(parseFloat(data[i].MPRD_AMT).toFixed(2)) + "</td>" +
                                    //"<td class='center-align'><img onclick='removeAcc(this)' class='remove-account' src='/Resources/Images/Account/remove.png' alt='' width='20' height='20'></td>" +
                                "</tr>");

                total = parseFloat(total) + parseFloat(data[i].MPRD_AMT);
           // }
        }
        jQuery(".acc-total").append("<b>( " + addCommas(parseFloat(total).toFixed(2)) + " )</b>");
        jQuery("table.acc-detail-tbl td.AccountNo").click(function () {
            var reqno = jQuery("#ReqNo").val();
            var accNo = jQuery(this).html();
            debitorFocusout(accNo, true);
            debtFocusout(jQuery(this).html());
            accountNumberClick(reqno, accNo)
        });
    }
}
function accountNumberClick(reqno, accno)
{
    //debitorFocusout(accno);
    getSavedDynamicValues(reqno, accno);
}
function addAccountDetailsNew(data) {
    jQuery(".acc-detail-tbl .new-row").remove();
    jQuery(".acc-total").empty();
    if (data.length > 0) {
        var total = 0;
        for (i = 0; i < data.length; i++) {
            //if (data[i].MPRD_STUS == 1) {
                jQuery("table.acc-detail-tbl").append("<tr class='new-row'>" +
                                    "<td class='LineNo nodisplay'>" + data[i].MPRD_ITM_LINE + "</td>" +
                                    "<td class='AccountNo'>" + data[i].MPRD_ACC_NO + "</td>" +
                                    "<td>" + data[i].ACC_DESC + "</td>" +
                                    "<td class='right-align amount'>" + addCommas(parseFloat(data[i].MPRD_AMT).toFixed(2)) + "</td>" +
                                    //"<td class='center-align'><img onclick='removeAcc(this)' class='remove-account' src='/Resources/Images/Account/remove.png' alt='' width='20' height='20'></td>" +
                                "</tr>");
                total = parseFloat(total) + parseFloat(data[i].MPRD_AMT);
            //}
        }
        jQuery(".acc-total").append("<b>( " + addCommas(parseFloat(total).toFixed(2)) + " )</b>");
        jQuery("table.acc-detail-tbl td.AccountNo").click(function () {
            var reqno = jQuery("#ReqNo").val();
            var accNo = jQuery(this).html();
            debitorFocusout(accNo, true);
            debFocusout(jQuery(this).html());
            accountNumberClick(reqno, accNo)
        });
    }
}
    function drawFormItemTemplateField(result, seq, cde) {
        data = result.data;
        jQuery(".template-itemvalval").empty();
        if (data != null && data.length > 0) {
            for (j = 0 ; j < data.length; j++) {
                var fieldset = "<fieldset class='filedset_" + data[j].TEMPLATE_ID + "'>";
                var field = "";
                fieldset += "<legend>" + data[j].TEMPLATE_NAME + "</legend>";
                //fieldset += field;
                fieldset += "</fieldset>";
                jQuery(".template-itemvalval").append(fieldset);

                for (i = 0; i < data[j].TEMPLATE_DET.length; i++) {

                    if (data[j].TEMPLATE_DET[i].FIELD_TYPE == "TEXTFIELD") {
                        field = "<div class='col-sm-4'><div class='col-sm-4'><label class='sec-label'>" + data[j].TEMPLATE_DET[i].FIELD_NAME + "</label></div><div class='col-sm-8'><input  value='" + data[j].TEMPLATE_DET[i].FIELD_VALUE + "'  class='form-control input-common-width' type='text' name='name_" + data[j].TEMPLATE_ID + "_" + data[j].TEMPLATE_DET[i].DETAIL_ID + "'></div></div>";
                    }
                    else if (data[j].TEMPLATE_DET[i].FIELD_TYPE == "COMBOBOX") {
                        field = "<div class='col-sm-4'><div class='col-sm-4'><label class='sec-label'>" + data[j].TEMPLATE_DET[i].FIELD_NAME + "</label></div><div class='col-sm-8'><select id='COMBOBOX_ID_" + data[j].TEMPLATE_DET[i].DETAIL_ID + "'  class='form-control input-common-width COMBOBOX_" + data[j].TEMPLATE_DET[i].DETAIL_ID + " combobox-dynamictval'   data-val='" + data[j].TEMPLATE_DET[i].DETAIL_ID + "' name='name_" + data[j].TEMPLATE_ID + "_" + +data[j].TEMPLATE_DET[i].DETAIL_ID + "'>"
                            + "<option value=''>Select</option>"
                                + "</select></div></div>";
                    }
                    else if (data[j].TEMPLATE_DET[i].FIELD_TYPE == "DATEFIELD") {
                        field = "<div class='col-sm-4'><div class='col-sm-4'><label class='sec-label'>" + data[j].TEMPLATE_DET[i].FIELD_NAME + "</label></div><div class='col-sm-8'><input value='" + data[j].TEMPLATE_DET[i].FIELD_VALUE + "'  readonly type='text' class='form-control input-common-width date-field' name='name_" + data[j].TEMPLATE_ID + "_" + data[j].TEMPLATE_DET[i].DETAIL_ID + "'></div></div>";

                    } else if (data[j].TEMPLATE_DET[i].FIELD_TYPE == "NUMBERFIELD") {
                        field = "<div class='col-sm-4'><div class='col-sm-4'><label class='sec-label'>" + data[j].TEMPLATE_DET[i].FIELD_NAME + "</label></div><div class='col-sm-8'><input oninput='validateNumberField(this)' value='" + data[j].TEMPLATE_DET[i].FIELD_VALUE + "'  class='number form-control input-common-width' type='text' name='name_" + data[j].TEMPLATE_ID + "_" + data[j].TEMPLATE_DET[i].DETAIL_ID + "'></div></div>";
                    }else if (data[j].TEMPLATE_DET[i].FIELD_TYPE == "CHECKBOX") {
                        field = "<div class='col-sm-4'><div class='col-sm-4'><label class='sec-label'>" + data[j].TEMPLATE_DET[i].FIELD_NAME + "</label></div><div class='col-sm-8'><input style='display:none;' type='text' class='chkbx-fld-val form-control' name='name_" + data[j].TEMPLATE_DET[i].FIELD_TYPE + "_" + data[j].TEMPLATE_ID + "_" + data[j].TEMPLATE_DET[i].DETAIL_ID + "'/><input    class='form-control input-common-width'  type='checkbox'/></div></div>";
                    }
                    jQuery(".template-itemvalval ." + "filedset_" + data[j].TEMPLATE_ID).append(field);
                    jQuery('.chkbx-fld-val').val("CR");

                    jQuery('.chkbx-fld').change(function () {
                        cb = jQuery(this);
                        if (cb.prop('checked')) {
                            jQuery(jQuery(this).siblings('input.chkbx-fld-val')).val("DR");
                        } else {
                            jQuery('.chkbx-fld-val').val("CR");
                        }
                    });
                }

                jQuery(".template-itemvalval .date-field").datepicker({ dateFormat: "dd/M/yy" });
            }
            if (seq == 0 || result.previous == 0) {
                jQuery(".template-itemvalval").append("<button data-direct=1 data-value=" + result.nextValue + " class='btn btn-default btn-default-style  btn-xext-previous pull-right'>Next</button>");
            } else {

                if (result.nextValue != 0) {
                    jQuery(".template-itemvalval").append("<button data-direct=1 data-value=" + result.nextValue + " class='btn btn-default btn-default-style btn-xext-previous pull-right'>Next</button>");
                }
                jQuery(".template-itemvalval").append("<button data-direct=-1 data-value=" + result.previous + " class='btn btn-default btn-default-style  btn-xext-previous pull-right'>Previous</button>");

            }
            jQuery('.btn-xext-previous').click(function (evt) {
                evt.preventDefault();
                var d = jQuery(this).data('value');
                var direct = jQuery(this).data('direct');
                jQuery.ajax({
                    cache: false,
                    type: "GET",
                    url: "/TemplateManager/getNextTemplateField?module=PAYREQ&code=" + cde + "&seq=" + d + "&direction=" + direct,
                    success: function (result) {
                        if (result.login == true) {
                            if (result.success == true) {
                                drawFormItemTemplateField(result, d, cde);
                            } else {
                                setInfoMsg(result.msg);
                            }

                        } else {
                            Logout()
                        }
                    }
                });
            });
            jQuery(".template-itemvalval .elete-det-tempfrm").click(function (evt) {
                var id = this.id;
                var th = jQuery(this);
                var assingDoc = jQuery(".template-uniq-class").val();
                Lobibox.confirm({
                    msg: "Do you want to delete template from form ?",
                    callback: function ($this, type, ev) {
                        if (type == "yes") {
                            evt.preventDefault();

                            jQuery.ajax({
                                cache: false,
                                type: "GET",
                                url: "/TemplateManager/removeAddedTemplate?hedid=" + id + "&assigncode=" + assingDoc,
                                success: function (result) {
                                    if (result.login == true) {
                                        if (result.success == true) {
                                            jQuery(jQuery(jQuery(th).parent('legend'))).parent('fieldset').remove();
                                        } else {
                                            setInfoMsg(result.msg);
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

            $(".template-itemvalval .combobox-dynamictval").each(function () {
                var id = jQuery(this).attr('data-val');
                var vale = jQuery(this).attr('data-selected');
                if (jQuery(jQuery(jQuery(this)).find("option")).length <= 1) {
                    jQuery.ajax({
                        cache: false,
                        type: "GET",
                        url: "/TemplateManager/getComboDetails?detid=" + id,
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    if (result.data.length > 0) {
                                        var opt = "";
                                        for (i = 0 ; i < result.data.length; i++) {
                                            var selected = "";
                                            //var selected = (result.data[i].ROLD_CODE == vale) ? " selectd = 1 " : "";
                                            opt += "<option " + selected + " value=" + result.data[i].ROLD_CODE + ">" + result.data[i].ROLD_NAME + "</option>";
                                        }
                                        jQuery(".template-itemvalval .COMBOBOX_" + id).html(opt);
                                        jQuery(".template-itemvalval .COMBOBOX_" + id).val(vale);
                                    }
                                } else {
                                    setInfoMsg(result.msg);
                                }
                            } else {
                                Logout();
                            }
                        }
                    });
                }

            });
        }
    }

    function validateNumberField(ths) {
        if (jQuery(ths).val() != "") {
            if (!jQuery.isNumeric(jQuery(ths).val())) {
                setInfoMsg("Please enter valis number.");
                jQuery(ths).val("");
                jQuery(ths).focus();
            }
        }
    }

    function clearForm(load) {
        var payTp = jQuery("#DropdownReqTp").val();    
        jQuery("#parreqfrm")[0].reset();
        var date = new Date();
        jQuery('#ReqDate').val(my_date_format(date));
        jQuery("table.dynamic-table .new-row").remove();
        jQuery("table.acc-detail-tbl .new-row").remove();
        jQuery(".acc-total").empty();
        jQuery(".btn-save-data").show();
        jQuery(".btn-save-data").val("Save");
        jQuery(".btn-approve-data").hide();
        jQuery(".btn-reject-data").hide();
        jQuery(".template-itemval").empty();
        jQuery(".dynamic-table").empty();
        jQuery(".cred-name").html("--");
        jQuery(".debitor-name").html("--");
        jQuery("#AccNo").val("");
        jQuery(".iscredit").prop("checked", false);
        jQuery(".itm-tax-table tr.new-row").remove();
        jQuery(".po-det-disp tr.new-row").remove();
        if (load == null) {
            pendingRequestData();
            var payTp = "PTTYCSH";
        }
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/PaymentRequest/clearSesstion",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                   
                    }
                }
            }
        });
        jQuery("#GrosAmt").attr("readonly", false);
        jQuery("#Tax").attr("readonly", false);
        jQuery("#DropdownReqTp").val(payTp);
    }
    function pendingRequestData() {
        jQuery(".pending-item-table table .new-row").remove();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/PaymentRequest/getPendingRequest?type=" + jQuery("#DropdownReqTp").val(),
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.data.length > 0) {                        
                            for (i = 0; i < result.data.length; i++) {
                                var st = "";
                                if (result.data[i].MPRH_STUS == "P") {
                                    st = "Pending";
                                } else if (result.data[i].MPRH_STUS == "A") {
                                    st = "Approved";
                                } else if (result.data[i].MPRH_STUS == "F") {
                                    st = "Processed";
                                }
                                jQuery(".pending-item-table table").append("<tr class='new-row'><th class='grd-itmslct'>Select</th><th class='req-no'>" + result.data[i].MPRH_REQ_NO + "</th><th>" + my_date_format(convertDate(result.data[i].MPRH_REQ_DT)) + "</th><th>" + result.data[i].MPRH_CREDITOR + "</th><th>" + st + "</th></tr>");
                            }
                            jQuery(".grd-itmslct").click(function () {
                                clearForm(true);
                                jQuery("#ReqNo").val(jQuery(this).siblings(".req-no").html());
                                loadReqDet(jQuery(this).siblings(".req-no").html());
                            });
                        }
                    } else {
                        result.msg;
                    }
                } else {
                    Logout();
                }
            }
        });
    }

    function loadReqDet(req) {
        
        jQuery(".template-itemval").empty();
        jQuery(".dynamic-table").empty();
        jQuery("#AccNo").val("");
        jQuery.ajax({
            cache: false,
            type: "POST",
            url: "/PaymentRequest/getRequestDetails?reqno=" + req+"&reqtp="+jQuery("#DropdownReqTp").val(),
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        jQuery("#PayType").val(result.hdr.MPRH_PAY_TP);
                        jQuery("#Creditor").val(result.hdr.MPRH_CREDITOR);
                        jQuery("#description").val(result.hdr.MPRH_RMK);
                        jQuery("#ReqDate").val(my_date_format(convertDate(result.hdr.MPRH_REQ_DT)));
                        jQuery(".btn-approve-data").show();
                        jQuery(".btn-process-data").hide();
                        jQuery(".btn-reject-data").hide(); 
                        if (result.hdr.MPRH_STUS == "A") {
                            jQuery(".btn-save-data").hide();
                            jQuery(".btn-approve-data").val("Reset");
                        } else if (result.hdr.MPRH_STUS == "P") {
                            jQuery(".btn-save-data").show();
                            jQuery(".btn-approve-data").val("Approve");
                            jQuery(".btn-save-data").val("Update");
                        } else if (result.hdr.MPRH_STUS == "F") {
                            jQuery(".btn-save-data").hide();
                            jQuery(".btn-approve-data").hide();
                             
                        }
                        
                        if (result.hdr.MPRH_STUS == "A") {
                            jQuery(".btn-process-data").show();
                        }
                        if (result.hdr.MPRH_STUS == "P") {
                            jQuery(".btn-reject-data").show();
                        }
                        if (result.det.length > 0) {
                            addAccountDetailsNew(result.det);
                        } 
                        var cde = jQuery("#Creditor").val();
                        creditoFocusout(cde);
                        addTaxDetails(result.tax, result.auto);
                        updateVals("POSRC", result.selecteditem, result.totTax, result.totCost);
                        jQuery("#GrosAmt").val(addCommas(parseFloat(result.hdr.MPRH_GROS_AMT).toFixed(2)));
                        jQuery("#Tax").val(addCommas(parseFloat(result.hdr.MPRH_TAX).toFixed(2)));
                        jQuery("#NetAmt").val(addCommas(parseFloat(result.hdr.MPRH_NET_AMT).toFixed(2)));

                    } else {
                        setInfoMsg(result.msg);
                    }
                } else {
                    Logout();
                }
            }
        });
    }

    function addTaxDetails(data,auto) {
        jQuery(".itm-tax-table tr.new-row").remove();
        if (data!=null && data.length > 0) {
            for (i = 0; i < data.length; i++) {
                if (data[i].MAT_STUS == 1) {
                    jQuery(".itm-tax-table table tbody").append("<tr class='new-row'>" +
                                            "<td class='seq_no hidden'>" + data[i].MAT_SEQ + "</td>" +
                                            "<td>" + data[i].MAT_TAX_CD + "</td>" +
                                            "<td class='number'>" + addCommas(parseFloat(data[i].MAT_TAX_RT).toFixed(2)) + "</td>" +
                                            "<td class='number'>" + addCommas(parseFloat(data[i].MAT_TAX_AMT).toFixed(2)) + "</td>" +
                                            "<td class='center-align'><img onclick='removeTaxDet(this," + auto + ")' class='remove-account' src='/Resources/Images/Account/remove.png' alt='' width='20' height='20'></td>" +
                                            "</tr>");
                }
            }
        }
    }
    function removeTaxDet(item, auto) {
        if (auto == false) {
            Lobibox.confirm({
                msg: "Do you want to remove account ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        var LineNo = jQuery(jQuery(jQuery(jQuery(item)).parent('td')).siblings("td.seq_no")).html();
                        jQuery.ajax({
                            cache: false,
                            type: "GET",
                            url: "/PaymentRequest/removeTaxDetaills?seq=" + LineNo,
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        if (result.data.length > 0) {
                                            jQuery(".itm-tax-table tr.new-row").remove();
                                            addTaxDetails(result.data, auto);
                                        }
                                    } else {
                                        setInfoMsg(result.msg);
                                    }

                                } else {
                                    Logout()
                                }
                            }
                        });

                    }
                }
            });
        } else {
            setInfoMsg("Cannot remove auto genarated tax details.");
        }
    }

    function addColumnsFront(val,type) {
        var valVal = jQuery(val).attr("value");
        var valCost = jQuery(val).attr("cost");
        var type = type;
        jQuery.ajax({
            type: "GET",
            url: "/PaymentRequest/addSelectedValues",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: { type: type, value: valVal, crecost: valCost, creditor: jQuery("#Creditor").val() },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        updateVals(type, result.selecteditem, result.totTax, result.totCost);
                        addTaxDetails(result.MST_ACC_TAX,false);
                    }
                }
            }
        });
    }

    function updateVals(type, value, tax, cost) {
        
        if (type == "POSRC") {
            jQuery(".po-det-disp tr.new-row").remove();
            if (value.length > 0) {
                for (i = 0 ; i < value.length; i++) {
                    jQuery(".po-det-disp").append("<tr class='chip new-row'><td class='valuefld'>" + value[i].PURNO + "</td><td class='valuefld number'>" + addCommas(value[i].COST) + "</td><td class='valuefld number'>" +addCommas(value[i].TAX) + "</td><td class='closebtn' onclick='removeChipFront(this,\"POSRC\")'>&times;</td></tr>");

                }
            }
            jQuery("#Tax").val(tax);
            jQuery("#GrosAmt").val(cost);
            updateNetAmount();
        } 
    }
    function removeChipFront(value, type) {
        var val = jQuery(value).siblings(".valuefld").html();

        if (val != "") {
            jQuery.ajax({
                type: "GET",
                url: "/PaymentRequest/removeSelected",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                data: { type: type, value: val },
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            updateVals(type, result.selecteditem, result.totTax, result.totCost);
                        }
                    }
                }
            });
        }
    }

