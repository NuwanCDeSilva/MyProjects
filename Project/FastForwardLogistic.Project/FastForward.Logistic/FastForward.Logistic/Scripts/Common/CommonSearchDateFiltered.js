var first = true;
var first_time = true;
var headerKeys;
var field = "";
var parameterData = "";

function CommonSearchDateFiltered(headerKeys, selectedfield, data) {
    
    var currDate = new Date();
    currDate.setMonth(currDate.getMonth() - 1);

    jQuery("#searpanel tbody").empty();
    jQuery("#KeyWorddf").val("");
    jQuery('#fromdate').val(my_date_format_tran(my_date_format_with_time(currDate).toString()));
    jQuery('#todate').val(my_date_format_tran(my_date_format_with_time(new Date()).toString()));
    headerKeys = headerKeys;
    field = selectedfield;
    if (data != null) {
        parameterData = data;
    } else {
        parameterData = null;
    }
    
    if (headerKeys.length > 0) {
        var selecter = jQuery('#modalviewdf .filter-key-clsdf');
        selecter.empty();
        for (i = 1; i < headerKeys.length; i++) {
            var newOption = jQuery('<option value="' + headerKeys[i] + '">' + headerKeys[i] + '</option>');
            selecter.append(newOption);
        }
        var head = jQuery('#modalviewdf .table-responsive .table thead tr');
        head.empty();
        for (j = 0; j < headerKeys.length; j++) {
            if (j == 0) {
                var newHead = jQuery('<th class="search-box-rownum-td">' + headerKeys[j].toUpperCase() + '</th>');
                head.append(newHead);
            }
            else {
                var newHead = jQuery('<th>' + headerKeys[j].toUpperCase() + '</th>');
                head.append(newHead);
            }
            
        }
    }
    
    jQuery('body').css('cursor', 'wait');
    var pgeNum = 1;
    var pgeSize = jQuery('.modal-content .cls-select-page-contdf').val();
    var searchFld = jQuery('select.filter-key-clsdf').val();
    var searchVal = jQuery('input#KeyWorddf').val();
    var from_date = jQuery('#fromdate').val();
    var to_date = jQuery('#todate').val();
    
    loadDetailsDf(pgeNum, pgeSize, searchFld, searchVal, headerKeys, false, from_date, to_date);
    
    return false;
}


function loadDetailsDf(pgeNum, pgeSize, searchFld, searchVal, headerKeys, check, fromDate, toDate) {
    
    var currDate = new Date();
    currDate.setMonth(currDate.getMonth() - 1);
    
    if (field == "receiptSearch") {
        newurl = "/Search/getReceiptEntries?unallow=" + parameterData.chk + "&recTyp=" + parameterData.recTyp + "&fd=" + fromDate + "&td=" + toDate + "&customer=" + parameterData.customer;
    }
    if (field == "ptychsreq") {
        newurl = "/Search/getPtyChshReqDet?type=" + parameterData + "&fd=" + fromDate + "&td=" + toDate;
    }
    if (field == "setlmentno") {
        newurl = "/Search/getSettlementList?fd=" + fromDate + "&td=" + toDate;
    }
    if (field == "jobno" || field == "jobno2" || field == "jobno3") {
        newurl = "/Search/GetJobNumber?fd=" + fromDate + "&td=" + toDate;
    }
    if (field == "jobdefno") {
        newurl = "/Search/GetJobNumber?fd=" + fromDate + "&td=" + toDate;
    }
    if (field == "jobinvno") {
        newurl = "/Search/GetJobNumber?fd=" + fromDate + "&td=" + toDate;
    }
    if (field == "jobnobl" || field == "jobnoblclose" || field == "jobnobl1") {
        newurl = "/Search/GetJobNumber?fd=" + fromDate + "&td=" + toDate;
    }
    if (field == "jobnoblclose") {
        newurl = "/Search/GetJobNumberInClose?fd=" + fromDate + "&td=" + toDate;
    }
    if (field == "jobnoblclose1") {
        newurl = "/Search/GetAllJobNumbers?fd=" + fromDate + "&td=" + toDate;
    }
    if (field == "BLnoM" || field == "BLno2M") {
        newurl = "/Search/GetBLNumberDfMBL?fd=" + fromDate + "&td=" + toDate;
    }
    if (field == "BL_JOB_NO" || field == "BL_H_DOC_NO" || field == "BL_D_DOC_NO" || field == "BL_M_DOC_NO" || field == "BL_POUCH_NO" || field == "BL_TERMINAL" || field == "BL_MANUAL_D_REF" || field == "BL_MANUAL_H_REF" || field == "BL_MANUAL_M_REF" || field == "BL_H_DOC_NOALL") {
        newurl = "/Search/getAllSearchDfHBL?fd=" + fromDate + "&td=" + toDate;
        pgeNum = pgeNum + "-" + field;
    }
    if (field == "BL_H_DOC_NOINV") {
        newurl = "/Search/getAllSearchDfHBLJob?fd=" + fromDate + "&td=" + toDate + "&jobno=" + parameterData;
        pgeNum = pgeNum + "-" + field;
    }
    if (field == "BLnoD") {
        newurl = "/Search/GetBLNumberDdf?fd=" + fromDate + "&td=" + toDate;
    }
    if (field == "pouchnojob" || field == "pouchnojobdef") {
        newurl = "/Search/getJobPouchSearchDateFiltered?fd=" + fromDate + "&td=" + toDate;
    }
    if (field == "InvoiceNoRef") {
        newurl = "/Search/getInvoiceNoDateFiltered?fd=" + fromDate + "&td=" + toDate;
    }
    if (field == "InvoiceNoDf" || field == "InvoiceNoDf1" || field == "InvoiceNoDfPetty") {
        newurl = "/Search/getInvoiceNoDf?fd=" + fromDate + "&td=" + toDate;
    }
    if (field == "InvoiceNoDfNew") {
        newurl = "/Search/getInvoiceNoDfNew?fd=" + fromDate + "&td=" + toDate + "&hbl=" + parameterData;
    }
    if (field == "InvoiceNoDfApprove") {
        newurl = "/Search/getInvoiceNoDfApp?fd=" + fromDate + "&td=" + toDate;
    }

    jQuery.ajax({
        cache: false,
        type: "GET",
        url: newurl,
        data: { pgeNum: pgeNum, pgeSize: pgeSize, searchFld: searchFld, searchVal: searchVal, fromDate: fromDate, toDate: toDate },
        success: function (result) {
            
            if (result.login == true) {
                if (result.success == true) {
                    if (result.data.length > 0) {
                        if (first == true) {
                            pagingdf(result.totalDoc, pgeNum, true);
                            first = false;
                        }
                        
                        setSerchPanelDf(result.data, headerKeys);
                        jQuery('#modalviewdf').css('cursor', 'default');
                        jQuery('body').css('cursor', 'default');
                        jQuery('#pagingdf').empty();
                        pagingdf(result.totalDoc, pgeNum, false);
                        check = false;
                    }
                } else {
                    
                    jQuery('#modalviewdf').css('cursor', 'default');
                    jQuery('body').css('cursor', 'default');
                    jQuery('#pagingdf').empty();
                    setSerchPanelDf(null, headerKeys);
                }
            } else {
                Logout();
            }
        }
    });

}
function setSerchPanelDf(tableValues, headerKeys) {
    
    if (jQuery('.table-responsive tbody').length > 0) {
        jQuery('.table-responsive tbody').empty();
    }
    if (tableValues != null) {
        if (tableValues.length > 0) {
            
            if (field == "receiptSearch") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td class="search-box-rownum-td">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].SAR_RECEIPT_NO +
                        '</td><td>' + tableValues[i].SAR_MANUAL_REF_NO +
                        '</td><td>' + tableValues[i].SAR_RECEIPT_DATE +
                        '</td><td>' + tableValues[i].SAR_ANAL_3 + '</td></tr>');
                }

                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Receipt Search");
            }
            if (field == "ptychsreq") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td class="search-box-rownum-td">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].TPRH_REQ_NO +
                        '</td><td>' + tableValues[i].TPRH_MANUAL_REF +
                        '</td><td>' + getFormatedDateInput(tableValues[i].TPRH_REQ_DT) + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Request Search");
            }
            if (field == "setlmentno") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td class="search-box-rownum-td">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].TPSH_SETTLE_NO +
                        '</td><td>' + tableValues[i].TPSH_MAN_REF +
                        '</td><td>' + tableValues[i].TPSD_JOB_NO +
                        '</td><td>' + getFormatedDateInput(tableValues[i].TPSH_SETTLE_DT) + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Request Settlement Search");
            }
            if (field == "jobno" || field == "jobno2" || field == "jobno3") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td class="search-box-rownum-td">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].JB_JB_NO +
                         '</td><td>' + tableValues[i].JB_POUCH_NO +
                        '</td><td>' + getFormatedDate1(tableValues[i].JB_JB_DT) +
                        '</td><td>' + tableValues[i].JB_STUS + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Job Num Search");

            }
            if (field == "jobdefno") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td class="search-box-rownum-td">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].JB_JB_NO +
                         '</td><td>' + tableValues[i].JB_POUCH_NO +
                        '</td><td>' + getFormatedDate1(tableValues[i].JB_JB_DT) +
                        '</td><td>' + tableValues[i].JB_STUS + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Job Num Search");

            }
            if (field == "jobinvno") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td class="search-box-rownum-td">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].JB_JB_NO +
                         '</td><td>' + tableValues[i].JB_POUCH_NO +
                        '</td><td>' + getFormatedDate1(tableValues[i].JB_JB_DT) +
                        '</td><td>' + tableValues[i].JB_STUS + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Job Num Search");

            }
            if (field == "jobnobl" || field == "jobnoblclose" || field == "jobnobl1") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td class="search-box-rownum-td">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].JB_JB_NO +
                         '</td><td>' + tableValues[i].JB_POUCH_NO +
                        '</td><td>' + getFormatedDate1(tableValues[i].JB_JB_DT) +
                        '</td><td>' + tableValues[i].JB_STUS + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Job No Search");

            }
            if (field == "jobnoblclose1") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td class="search-box-rownum-td">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].JB_JB_NO +
                         '</td><td>' + tableValues[i].JB_POUCH_NO +
                        '</td><td>' + getFormatedDate1(tableValues[i].JB_JB_DT) +
                        '</td><td>' + tableValues[i].JB_STUS + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Job No Search");

            }
            if (field == "BLnoM" || field == "BLno2M") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('table.cls-search-pnl').append('<tr ' + a + '><td class="search-box-rownum-td">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].DOC_NO +
                        '</td><td>' + tableValues[i].BL_H_DOC_NO +
                        '</td><td>' + tableValues[i].BL_D_DOC_NO +
                        '</td><td>' + getFormatedDate1(tableValues[i].DOC_DT) +
                        '</td><td>' + tableValues[i].BL_JOB_NO + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Master B/L Search");

            }
            // JoB No Added by Tharindu 2018-01-16
            if (field == "BL_H_DOC_NO" || field == "BL_H_DOC_NOINV") {
 
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('table.cls-search-pnl').append('<tr ' + a + '><td class="search-box-rownum-td">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].CODE +
                        '</td><td>' + tableValues[i].BL_JOB_NO +
                        '</td><td>' + tableValues[i].BL_CUS_CD +
                        '</td><td>' + tableValues[i].BL_AGENT_CD +
                        '</td><td>' + getFormatedDate1(tableValues[i].DATE_) + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("House B/L Search");

            }
            if (field == "BLnoD") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('table.cls-search-pnl').append('<tr ' + a + '>' +
                        '<td class="search-box-rownum-td">' + tableValues[i].R__ + '</td>' +
                        '<td>' + tableValues[i].DOC_NO +
                        '</td><td>' + tableValues[i].BL_MANUAL_D_REF +
                        '</td><td>' + tableValues[i].BL_POUCH_NO +
                        '</td><td>' + getFormatedDate1(tableValues[i].DOC_DT) + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Draft B/L Search");

            }
            if (field == "pouchnojob" || field == "pouchnojobdef") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('table.cls-search-pnl').append('<tr ' + a + '><td class="search-box-rownum-td">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].JB_POUCH_NO +
                        '</td><td>' + tableValues[i].JB_JB_NO +
                        '</td><td>' + getFormatedDate1(tableValues[i].JB_JB_DT) + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Pouch No Search");

            }
            if (field == "InvoiceNoRef") {                
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('table.cls-search-pnl').append('<tr ' + a + '><td class="search-box-rownum-td">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].TIH_INV_NO +
                        '</td><td>' + tableValues[i].JB_JB_NO +
                        '</td><td>' + getFormatedDateInput(tableValues[i].TIH_INV_DT) + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Other Ref No Search");
            }
            if (field == "InvoiceNoDf" || field == "InvoiceNoDf1" || field == "InvoiceNoDfPetty") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('table.cls-search-pnl').append('<tr ' + a + '><td class="search-box-rownum-td">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].TIH_INV_NO +
                        '</td><td>' + tableValues[i].JB_JB_NO +
                        '</td><td>' + getFormatedDateInput(tableValues[i].TIH_INV_DT) + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Invoice No Search");
            }
            if (field == "InvoiceNoDfApprove") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('table.cls-search-pnl').append('<tr ' + a + '><td class="search-box-rownum-td">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].TIH_INV_NO +
                        '</td><td>' + tableValues[i].JB_JB_NO +
                        '</td><td>' + getFormatedDateInput(tableValues[i].TIH_INV_DT) + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Invoice No Search");
            }

            if (field == "InvoiceNoDfNew") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('table.cls-search-pnl').append('<tr ' + a + '><td class="search-box-rownum-td">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].TIH_INV_NO +
                        '</td><td>' + tableValues[i].JB_JB_NO +
                        '</td><td>' + getFormatedDateInput(tableValues[i].TIH_INV_DT) + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Invoice No Search");
            }

        }
    }
    else {
        if (jQuery('.table-responsive tbody').length > 0) {
            jQuery('.table-responsive tbody').append("<tr><td style=' border:none; color: #ff6666; position: absolute; width: 100%; font-weight: bold;'>No data found for this search criteria.</td></tr>");
        }
    }
    jQuery('#modalviewdf').modal({
        keyboard: false,
        backdrop: 'static'
    }, 'show');
    jQuery('tr', '#modalviewdf table tbody').click(function () {
        jQuery('tr', '#modalviewdf table tbody').removeClass('selected');
        jQuery('tr', '#modalviewdf table tbody').css('color', 'black');
        jQuery(this).addClass('selected');
        jQuery(this).css('color', 'blue');
        setValueDf(field);
    });

}
function setValueDf(field) {
    
    var currDate = new Date();
    currDate.setMonth(currDate.getMonth() - 1);

    if (jQuery("#modalviewdf").is(":visible") == true) {
        if (jQuery('tr.selected td', '#modalviewdf table tbody').length > 0) {
            
            if (field == "receiptSearch") {
                
                jQuery('#Sar_receipt_no').val("");
                var value = jQuery('tr.selected td', '#modalviewdf table tbody').closest("tr").find('td:eq(1)').text();
                global_receipt_date = jQuery('tr.selected td', '#modalviewdf table tbody').closest("tr").find('td:eq(3)').text();
                
                jQuery('#Sar_receipt_no').val(value);
                jQuery('#modalviewdf').modal('hide');
                jQuery("#KeyWorddf").val("");
                jQuery('#fromdate').val(my_date_format_tran(my_date_format_with_time(currDate).toString()));
                jQuery('#todate').val(my_date_format_tran(my_date_format_with_time(new Date()).toString()));
                jQuery(".cancel-rec").show();
                jQuery('#Sar_receipt_no').focus();
            }
            if (field == "ptychsreq") {
                jQuery('#RequestNo').val("");
                var value = jQuery('tr.selected td', '#modalviewdf table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#RequestNo').val(value);
                jQuery('#modalviewdf').modal('hide');
                jQuery("#KeyWorddf").val("");
                jQuery('#RequestNo').focus();
            }
            if (field == "setlmentno") {
                jQuery('#SettlementNo').val("");
                var value = jQuery('tr.selected td', '#modalviewdf table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#SettlementNo').val(value);
                jQuery('#modalviewdf').modal('hide');
                jQuery("#KeyWorddf").val("");
                jQuery('#SettlementNo').focus();
            }
            if (field == "jobno" || field == "jobno2" || field == "jobno3") {
                jQuery('#Tih_job_no').val("");
                var value = jQuery('tr.selected td', '#modalviewdf table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#Tih_job_no').val(value);
                jQuery('#modalviewdf').modal('hide');
                jQuery("#KeyWorddf").val("");
                jQuery('#Tih_job_no').focus();
            }
            if (field == "jobdefno") {
                jQuery('#Jb_jb_no').val("");
                var value = jQuery('tr.selected td', '#modalviewdf table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#Jb_jb_no').val(value);
                jQuery('#modalviewdf').modal('hide');
                jQuery("#KeyWorddf").val("");
                jQuery('#Jb_jb_no').focus();
            }
            if (field == "jobinvno") {
                jQuery('#Tih_job_no').val("");
                var value = jQuery('tr.selected td', '#modalviewdf table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#Tih_job_no').val(value);
                jQuery('#modalviewdf').modal('hide');
                jQuery("#KeyWorddf").val("");
                jQuery('#Tih_job_no').focus();
            }
            if (field == "jobnobl") {
                jQuery('#Bl_job_no').val("");
                var value = jQuery('tr.selected td', '#modalviewdf table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#Bl_job_no').val(value);
                jQuery('#modalviewdf').modal('hide');
                jQuery("#KeyWorddf").val("");
                jQuery('#Bl_job_no').focus();
            }
            if (field == "jobnobl1") {
                jQuery('#Bl_job_no1').val("");
                var value = jQuery('tr.selected td', '#modalviewdf table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#Bl_job_no1').val(value);
                jQuery('#modalviewdf').modal('hide');
                jQuery("#KeyWorddf").val("");
                jQuery('#Bl_job_no1').focus();
            }
            if (field == "jobnoblclose") {
                jQuery('#jobno').val("");
                var value = jQuery('tr.selected td', '#modalviewdf table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#jobno').val(value);
                jQuery('#modalviewdf').modal('hide');
                jQuery("#KeyWorddf").val("");
                jQuery('#jobno').focus();
            }
            if (field == "jobnoblclose1") {
                jQuery('#jobno').val("");
                var value = jQuery('tr.selected td', '#modalviewdf table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#jobno').val(value);
                jQuery('#modalviewdf').modal('hide');
                jQuery("#KeyWorddf").val("");
                jQuery('#jobno').focus();
            }
            if (field == "BLnoM") {
                
                jQuery('#Bl_m_doc_no').val("");
                var value = jQuery('tr.selected td', '#modalviewdf table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#Bl_m_doc_no').val(value);
                jQuery('#modalviewdf').modal('hide');
                jQuery("#KeyWorddf").val("");
                jQuery('#Bl_m_doc_no').focus();
            }
            if (field == "BL_H_DOC_NO") {
                
                jQuery('#Bl_h_doc_no').val("");
                var value = jQuery('tr.selected td', '#modalviewdf table tbody').closest("tr").find('td:eq(1)').text();
                
                jQuery('#Bl_h_doc_no').val(value);
                jQuery('#modalviewdf').modal('hide');
                jQuery("#KeyWorddf").val("");
                jQuery('#Bl_h_doc_no').focus();
            }

            // Added by tharindu 2016-01-16
            if (field == "BL_H_DOC_NO") {

                jQuery('#job-number').val("");
                var value = jQuery('tr.selected td', '#modalviewdf table tbody').closest("tr").find('td:eq(2)').text();

                jQuery('#job-number').val(value);
                jQuery('#modalviewdf').modal('hide');
                jQuery("#KeyWorddf").val("");
                jQuery('#job-number').focus();
            }

            if (field == "BL_H_DOC_NOINV") {
                
                jQuery('#Tih_bl_h_no').val("");
                var value = jQuery('tr.selected td', '#modalviewdf table tbody').closest("tr").find('td:eq(1)').text();
                
                jQuery('#Tih_bl_h_no').val(value);
                jQuery('#modalviewdf').modal('hide');
                jQuery("#KeyWorddf").val("");
                jQuery('#Tih_bl_h_no').focus();
            }
            if (field == "BLnoD") {
                jQuery('#Bl_d_doc_no').val("");
                var value = jQuery('tr.selected td', '#modalviewdf table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#Bl_d_doc_no').val(value);
                jQuery('#modalviewdf').modal('hide');
                jQuery("#KeyWorddf").val("");
                jQuery('#Bl_d_doc_no').focus();
            }
            if (field == "pouchnojob") {
                jQuery('#pouchno').val("");
                var value = jQuery('tr.selected td', '#modalviewdf table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#pouchno').val(value);
                jQuery('#modalviewdf').modal('hide');
                jQuery("#KeyWorddf").val("");
                jQuery('#pouchno').focus();
            }
            if (field == "pouchnojobdef") {
                jQuery('#Jb_pouch_no').val("");
                var value = jQuery('tr.selected td', '#modalviewdf table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#Jb_pouch_no').val(value);
                jQuery('#modalviewdf').modal('hide');
                jQuery("#KeyWorddf").val("");
                jQuery('#Jb_pouch_no').focus();
            }
            if (field == "InvoiceNoRef") {
                jQuery('#Tih_other_ref_no').val("");
                var value = jQuery('tr.selected td', '#modalviewdf table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#Tih_other_ref_no').val(value);
                jQuery('#modalviewdf').modal('hide');
                jQuery("#KeyWorddf").val("");
                jQuery('#Tih_other_ref_no').focus();
            }
            if (field == "InvoiceNoDf") {
                jQuery('#Tih_inv_no').val("");
                var value = jQuery('tr.selected td', '#modalviewdf table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#Tih_inv_no').val(value);
                jQuery('#modalviewdf').modal('hide');
                jQuery("#KeyWorddf").val("");
                jQuery('#Tih_inv_no').focus();
            }
            if (field == "InvoiceNoDfPetty") {
                jQuery('#InvNo').val("");
                var value = jQuery('tr.selected td', '#modalviewdf table tbody').closest("tr").find('td:eq(1)').text();
                var value1 = jQuery('tr.selected td', '#modalviewdf table tbody').closest("tr").find('td:eq(3)').text();
                jQuery('#InvNo').val(value);
                jQuery('#InvDt').val(value1);
                jQuery('#modalviewdf').modal('hide');
                jQuery("#KeyWorddf").val("");
                jQuery('#InvNo').focus();
            }
            if (field == "InvoiceNoDfNew") {
                jQuery('#invno').val("");
                var value = jQuery('tr.selected td', '#modalviewdf table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#invno').val(value);
                jQuery('#modalviewdf').modal('hide');
                jQuery("#KeyWorddf").val("");
                jQuery('#invno').focus();
            }
            if (field == "InvoiceNoDf1") {
                jQuery('#Tih_inv_cred_no').val("");
                var value = jQuery('tr.selected td', '#modalviewdf table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#Tih_inv_cred_no').val(value);
                jQuery('#modalviewdf').modal('hide');
                jQuery("#KeyWorddf").val("");
                jQuery('#Tih_inv_cred_no').focus();
            }
            if (field == "InvoiceNoDfApprove") {
                jQuery('#Tih_inv_cred_no').val("");
                var value = jQuery('tr.selected td', '#modalviewdf table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#Tih_inv_cred_no').val(value);
                jQuery('#modalviewdf').modal('hide');
                jQuery("#KeyWorddf").val("");
                jQuery('#Tih_inv_cred_no').focus();
            }
        }
    }

}

var my_date_format = function (input) {
    var monthNames = [
      "Jan", "Feb", "Mar",
      "Apr", "May", "Jun", "Jul",
      "Aug", "Sep", "Oct",
      "Nov", "Dec"
    ];

    var date = new Date(Date.parse(input));;
    var day = date.getDate();
    var monthIndex = date.getMonth();
    var year = date.getFullYear();

    return (day + "/" + monthNames[monthIndex] + "/" + year);
};
jQuery(document).ready(function () {
    
    jQuery('#modalviewdf').on('shown.bs.modal', function () {
        
        jQuery("#KeyWorddf").focus();
        //jQuery(".modal-content").draggable({ handle: ".Title.panel-heading", containment: "body" });
        jQuery('.Title.panel-heading').css('cursor', 'move');
    });
    jQuery(document).keypress(function (evt) {
        if (evt.keyCode == 27) {
            
            if (jQuery('#modalviewdf:visible').length == 1) {
                jQuery("#KeyWorddf").val("");
                jQuery('#fromdate').val("");
                jQuery('#todate').val("");
                jQuery('#modalviewdf').modal('hide');
            }
        }
    });
    jQuery('#modalviewdf .close-btn').click(function (e) {
        e.preventDefault();
        jQuery('#fromdate').val("");
        jQuery('#todate').val("");
        jQuery("#KeyWorddf").val("");
        jQuery('#modalviewdf').modal('hide');

    });
    // This	creates a new object
    pagingdf = function (total, page, test) {
        
        jQuery('#pagingdf').bootpag({
            total: total,
            page: page,
            maxVisible: 5,
            leaps: true,
            firstLastUse: true,
            first: '←',
            last: '→',
            wrapClass: 'pagination',
            activeClass: 'active',
            disabledClass: 'disabled',
            nextClass: 'next',
            prevClass: 'prev',
            lastClass: 'last',
            firstClass: 'first'
        }).on("page", function (event, num) {
            
            if (test == true) {
                
                jQuery('#modalviewdf').css('cursor', 'wait');
                jQuery(".se-pre-con").fadeIn("slow");
                var searchFld = jQuery('select.filter-key-clsdf').val();
                var searchVal = jQuery('input#KeyWorddf').val();
                var from_date = jQuery('#fromdate').val();
                var to_date = jQuery('#todate').val();
                var pgeSize = jQuery('.modal-content .cls-select-page-contdf').val();
                
                loadDetailsDf(num, pgeSize, searchFld, searchVal, headerKeys, null, from_date, to_date);
            }
        });

    }
    jQuery('.modal-content .cls-select-page-contdf').change(function (e) {
        jQuery('#modalviewdf').css('cursor', 'wait');
        jQuery(".se-pre-con").fadeIn("slow");
        var searchFld = jQuery('select.filter-key-clsdf').val();
        var searchVal = jQuery('input#KeyWorddf').val();
        var from_date = jQuery('#fromdate').val();
        var to_date = jQuery('#todate').val();
        var pgeSize = jQuery('.modal-content .cls-select-page-contdf').val();
        var pgeNum = 1;
        
        loadDetailsDf(pgeNum, pgeSize, searchFld, searchVal, headerKeys, true, from_date, to_date);
    });

    jQuery(".btn-search-df").click(function (e) {
        
        jQuery('#modalviewdf').css('cursor', 'wait');
        jQuery(".se-pre-con").fadeIn("slow");
        var searchFld = jQuery('select.filter-key-clsdf').val();
        var searchVal = jQuery('input#KeyWorddf').val();
        var from_date = jQuery('#fromdate').val();
        var to_date = jQuery('#todate').val();
        var pgeSize = jQuery('.modal-content .cls-select-page-contdf').val();
        var pgeNum = 1;
        
        loadDetailsDf(pgeNum, pgeSize, searchFld, searchVal, headerKeys, true, from_date, to_date);

        if (jQuery("#modalviewdf").is(":visible") == true) {
            
            if (jQuery('tr td', '#modalviewdf table tbody').length > 0) {
                if (e.keyCode == 40) {
                    if (jQuery('.modal-content #KeyWorddf').is(':focus') == false) {
                        var aa = jQuery('#modalviewdf table tbody tr.selected').next();
                        if (typeof aa[0] != "undefined") {
                            jQuery('tr', '#modalviewdf table tbody').removeClass('selected');
                            jQuery('tr', '#modalviewdf table tbody').css('color', 'black');
                            jQuery(aa[0]).addClass('selected');
                            jQuery(aa[0]).css('color', 'red');
                        }
                    }
                }
                else if (e.keyCode == 38) {
                    
                    if (jQuery('.modal-content #KeyWorddf').is(':focus') == false) {
                        var bb = jQuery('#modalviewdf table tbody tr.selected').prev();
                        if (typeof bb[0] != "undefined") {
                            jQuery('tr', '#modalviewdf table tbody').removeClass('selected');
                            jQuery('tr', '#modalviewdf table tbody').css('color', 'black');
                            jQuery(bb[0]).addClass('selected');
                            jQuery(bb[0]).css('color', 'red');
                        }
                    }

                }
            }
        }

    });

    jQuery(document).on("keypress", function (e) {
        

        if (e.keyCode == 13) {
            
            if (jQuery('.modal-content #KeyWorddf').is(':focus') == true) {
                jQuery('#modalviewdf').css('cursor', 'wait');
                jQuery(".se-pre-con").fadeIn("slow");
                var searchFld = jQuery('select.filter-key-clsdf').val();
                var searchVal = jQuery('input#KeyWorddf').val();
                var from_date = jQuery('#fromdate').val();
                var to_date = jQuery('#todate').val();
                var pgeSize = jQuery('.modal-content .cls-select-page-contdf').val();
                var pgeNum = 1;
                
                
                loadDetailsDf(pgeNum, pgeSize, searchFld, searchVal, headerKeys, true, from_date, to_date);

                $('.cls-search-pnl').closest('tr').next().focus();
                
                jQuery('table.cls-search-pnl tr:first').addClass('selected');
                var aa = jQuery('.table.cls-search-pnl tbody tr').next();
                jQuery(aa[0]).addClass('selected');
                jQuery(aa[0]).css('color', 'red');
            } else {
                setValueDf(field);
            }

        } else if (e.keyCode == 40) {
            
            if (jQuery('.modal-content #KeyWorddf').is(':focus') == true) {
                var aa = jQuery('#modalviewdf table tbody tr');
                if (typeof aa[0] != "undefined") {
                    jQuery('.modal-content input#KeyWorddf').focusout();
                    jQuery('tr', '#modalviewdf table tbody').removeClass('selected');
                    jQuery('tr', '#modalviewdf table tbody').css('color', 'black');
                    jQuery(aa[0]).addClass('selected');
                    jQuery(aa[0]).css('color', 'red');
                }
            }
        }
        
    });

    $(document).keydown(function (e) {
        
        if (e.keyCode == 39) {
            
            if (jQuery('.modal-content #KeyWorddf').is(':focus') == true) {
                var aa = jQuery('#modalviewdf table tbody tr');
                if (typeof aa[0] != "undefined") {
                    jQuery('.modal-content input#KeyWorddf').focusout();
                    $(".modal-content #KeyWorddf").blur();
                    jQuery('tr', '#modalviewdf table tbody').removeClass('selected');
                    jQuery('tr', '#modalviewdf table tbody').css('color', 'black');
                    jQuery(aa[0]).addClass('selected');
                    jQuery(aa[0]).css('color', 'red');
                }
            }
        }
        if (jQuery("#modalviewdf").is(":visible") == true) {
            
            if (jQuery('tr td', '#modalviewdf table tbody').length > 0) {
                if (e.keyCode == 40) {
                    if (jQuery('.modal-content #KeyWorddf').is(':focus') == false) {
                        var aa = jQuery('#modalviewdf table tbody tr.selected').next();
                        if (typeof aa[0] != "undefined") {
                            jQuery('tr', '#modalviewdf table tbody').removeClass('selected');
                            jQuery('tr', '#modalviewdf table tbody').css('color', 'black');
                            jQuery(aa[0]).addClass('selected');
                            jQuery(aa[0]).css('color', 'red');
                        }
                    }
                }
                else if (e.keyCode == 38) {
                    
                    if (jQuery('.modal-content #KeyWorddf').is(':focus') == false) {
                        var bb = jQuery('#modalviewdf table tbody tr.selected').prev();
                        if (typeof bb[0] != "undefined") {
                            jQuery('tr', '#modalviewdf table tbody').removeClass('selected');
                            jQuery('tr', '#modalviewdf table tbody').css('color', 'black');
                            jQuery(bb[0]).addClass('selected');
                            jQuery(bb[0]).css('color', 'red');
                        }
                    }

                }
            }
        }

    });

    jQuery("#fromdate").focusout(function () {
        var code = jQuery(this).val();
        if (code == "") {
            jQuery('#fromdate').val(my_date_format(new Date()));
        }
    });
    jQuery("#todate").focusout(function () {
        var code = jQuery(this).val();
        if (code == "") {
            jQuery('#todate').val(my_date_format(new Date()));
        }
    });

});
function getFormatedDate1(date) {
    
    if (typeof date == 'undefined') {
        return "";
    } else {
        var dte = new Date(parseInt(date.substr(6)));
        if (my_date_format_tran(dte) != "NaN/undefined/NaN")
            return my_date_format_tran(dte);
    }
}
function Logout() {
    Lobibox.alert('info', {
        msg: 'Login session has expired!',
        buttons: {
            ok: {
                'class': 'btn btn-info',
                closeOnClick: false
            }
        },
        callback: function (lobibox, type) {
            var btnType;
            if (type === 'ok') {
                window.location.replace("/Login/Index");
            }
        }
    });
}
function clearpageOnReceiptType() {
    //
    //jQuery("#Sar_manual_ref_no").val("");
    //jQuery('#Sar_receipt_date').val(my_date_format_tran(my_date_format_with_time(new Date()).toString()));
    //jQuery("#Sar_debtor_cd").val("");
    //jQuery("#Sar_debtor_name").val("");
    //jQuery("#Sar_manual_ref_no").val("");
    //jQuery(".tot-paid-amount-val").empty();
    //jQuery(".tot-paid-amount-val").html("");
    //jQuery("#TotalAmount").val(0);
    //jQuery('table.payment-table .new-row').remove();
    //jQuery('table.payment-table').remove();
    //jQuery("#VehLcTel").val(""); // Added by Chathura on 20-sep-2017
    //jQuery("#InvoiceNo").val(""); // Added by Chathura on 20-sep-2017
    //jQuery(".tbl-cus-name tr").remove(); // Added by Chathura on 20-sep-2017
    //jQuery("#Ammountdup").val(""); // Commented by Chathura on 20-sep-2017
    //jQuery("#Ammount").val(""); // Commented by Chathura on 20-sep-2017
    //jQuery(".cancel-rec").hide();
    //jQuery("#cus_heading").text("Debtor details");
    //jQuery.ajax({
    //    type: "GET",
    //    url: "/RecieptEntry/ClearSession",
    //    data: {},
    //    contentType: "application/json;charset=utf-8",
    //    dataType: "json",
    //})
}

