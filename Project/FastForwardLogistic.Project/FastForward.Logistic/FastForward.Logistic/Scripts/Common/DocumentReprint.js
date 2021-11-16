jQuery(document).ready(function () {

    jQuery("#prnReqFrmDt").val(getFormatedPreMonth(new Date()));
    jQuery("#prnReqToDt").val(getFormatedDate(new Date()));

    //pcfocusout(jQuery("#ProfitCenter").val());
    loadPendingRequest();
    jQuery(".btn-emp-clear-data").click(function () {
        Lobibox.confirm({
            msg: "Do you want to clear data ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    clearForm();

                }
            }
        });
    });

    jQuery("#prnReqFrmDt").focusout(function () {
        var code = jQuery(this).val();
        if (code == "") {
            jQuery('#prnReqFrmDt').val(my_date_format(new Date()));
        }
    });
    jQuery("#prnReqToDt").focusout(function () {
        var code = jQuery(this).val();
        if (code == "") {
            jQuery('#prnReqToDt').val(my_date_format(new Date()));
        }
    });

    jQuery(".btn-emp-save-data").click(function (evt) {
        evt.preventDefault();
        reqno = jQuery("#Bl_h_doc_no").val();
        //types = jQuery("#ReqType").val();
        //paydate = jQuery("#PayDate").val();
        //appLvl = "1";
        Lobibox.confirm({
            msg: "Do you want approve?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    jQuery.ajax({
                        type: "POST",
                        url: "/DocumentReprint/approveRequest",
                        dataType: "json",
                        data: { reqno: reqno },
                        success: function (result) {
                            if (result.login == false) {
                                Logout();
                            } else {
                                if (result.success == true) {
                                    if (result.type == "Success") {
                                        setSuccesssMsg(result.msg);
                                        clearForm();
                                    }
                                } else {
                                    if (result.type == "Error") {
                                        setError(result.msg);
                                    }
                                    if (result.type == "Info") {
                                        setInfoMsg(result.msg);
                                    }
                                }

                            }
                        }
                    });
                }
            }
        });
    });


    //jQuery(".btn-emp-save-data").click(function () {
    //    Lobibox.confirm({
    //        msg: "Do you want to save ?",
    //        callback: function ($this, type, ev) {
    //            if (type == "yes") {
    //                var formdata = jQuery("#pettcase-frm").serialize();
    //                jQuery.ajax({
    //                    type: "POST",
    //                    url: "/PettyCash/savePettyCaseDet",
    //                    data: formdata,
    //                    dataType: "json",
    //                    success: function (result) {
    //                        if (result.login == false) {
    //                            Logout();
    //                        } else {
    //                            if (result.success == true) {
    //                                clearForm();
    //                                if (result.type == "Success") {
    //                                    setSuccesssMsg(result.msg);
    //                                }
    //                            } else {
    //                                if (result.type == "Error") {
    //                                    setError(result.msg);
    //                                }
    //                                if (result.type == "Info") {
    //                                    setInfoMsg(result.msg);
    //                                }
    //                            }

    //                        }
    //                    }
    //                });
    //            }
    //        }
    //    });

    //});

    jQuery(".pending-jobbtn").click(function (evt) {
        evt.preventDefault();
        loadPendingRequest();
    });

});

jQuery(".btn-house-bl").click(function () {
    //click_status = "H";
    var headerKeys = Array()
    headerKeys = ["Row", "Code", "JobNo", "Customer", "Agent", "Date"];
    field = "BL_H_DOC_NO"
    var x = new CommonSearchDateFiltered(headerKeys, field);
});

function clearForm() {
    document.getElementById("pettcase-frm").reset();
    jQuery(".pc-desc").empty();
    jQuery(".reqbydesc").empty();
    jQuery(".app-text").empty();
    jQuery("table.pendingreq-tbl tbody").empty();
    jQuery("table.job-dtl-tbl tbody").empty();
    jQuery(".lbl-name1").empty();
    jQuery(".lbl-name2").empty();
    jQuery(".lbl-name3").empty();
    jQuery(".lbl-name4").empty();
    jQuery(".lbl-tin").hide();
    if (jQuery("#ReqType").val() == "TTREQ") {
        jQuery(".cons-grp").show();
        jQuery(".pay-grp").hide();
        jQuery(".lbl-name1").html("-");
        jQuery(".lbl-name2").html("-");
        jQuery(".lbl-name3").html("-");
        jQuery(".lbl-name4").html("-");
        jQuery(".lbl-tin").show();
        jQuery("#topicorg").text("Payment To");
        jQuery("#Remarks").text("PLEASE ARRANGE THE PAYMENT BY CHEQUE");
    } else if (jQuery("#ReqType").val() == "PAYREQ") {
        jQuery(".cons-grp").hide();
        jQuery(".pay-grp").show();
        jQuery(".lbl-name1").html("-");
        jQuery(".lbl-name2").html("-");
        jQuery(".lbl-name3").html("-");
        jQuery(".lbl-name4").html("-");
        jQuery(".lbl-tin").show();
        jQuery("#topicorg").text("Consignee");
        jQuery("#Remarks").text("PLEASE ARRANGE THE PAYMENT BY CHEQUE");
    } else {
        jQuery(".cons-grp").hide();
        jQuery(".pay-grp").hide();
        jQuery("#topicorg").text("Consignee");
        jQuery("#Remarks").text("PLEASE ARRANGE THE PAYMENT IN CASH");
    }
    jQuery("#prnReqFrmDt").val(getFormatedPreMonth(new Date()));
    jQuery("#prnReqToDt").val(getFormatedDate(new Date()));
    jQuery("#PayDate").val(getFormatedDate(new Date()));
    jQuery("#ReqDate").val(getFormatedDate(new Date()));
    jQuery("#UploadDate").val(getFormatedDate(new Date()));
    jQuery(".btn-emp-save-data").show();
    jQuery(".btn-emp-update-data").hide();
    jQuery("#Total").val("");
    jQuery(".print-job-btn").hide();
    jQuery(".reject-btn").hide();
    clearSession();
    loadDefaultCurrency();
    loadPendingRequest();
    //jQuery("#Remarks").text("PLEASE ARRANGE THE PAYMENT BY CHEQUE");
    //pcfocusout(jQuery("#ProfitCenter").val());
}

function loadPendingRequest() {
    jQuery.ajax({
        type: "GET",
        url: "/DocumentReprint/loadPendingRequests",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: { fromdt: jQuery("#prnReqFrmDt").val(), todt: jQuery("#prnReqToDt").val() },
        success: function (result) {
            if (result.login == true) {
                jQuery(".pendingreq-tbl tbody").empty();
                if (result.success == true) {
                    if (result.data.length > 0) {
                        for (i = 0 ; i < result.data.length; i++) {
                            jQuery('.pendingreq-tbl tbody').append('<tr class="new-row">' +
                            '<td class="no-display">' + result.data[i].TPRH_SEQ + '</td>' +
                            '<td style="width:120px;">' + result.data[i].TPRH_REQ_NO + '</td>' +
                            '<td style="width:50px;">' + result.data[i].TPRH_MANUAL_REF + '</td>' +
                            '<td style="width:75px;">' + getFormatedDateInput(result.data[i].TPRH_REQ_DT) + '</td>' + + '</td>' +
                            '<td style="width:75px;">' + result.data[i].TPRH_TYPE_DESC + '</td>' +
                            '</tr>');

                            //jQuery('.pendingreq-tbl tbody').append('<tr class="new-row">' +
                            //'<td class="no-display">' + result.data[i].TPRH_SEQ + '</td>' +
                            //'<td>' + result.data[i].TPRH_REQ_NO + '</td>' +
                            //'<td>' + result.data[i].TPRH_MANUAL_REF + '</td>' +
                            //'<td>' + getFormatedDateInput(result.data[i].TPRH_REQ_DT) + '</td>' + + '</td>' +
                            //'<td>' + result.data[i].TPRH_TYPE_DESC + '</td>' +
                            //'</tr>');
                        }
                        jQuery(".pendingreq-tbl tbody tr.new-row").click(function (evt) {
                            evt.preventDefault();
                            var value = jQuery(this).closest("tr").find('td:eq(0)').text();
                            loadRequestDataBySeq(value);
                        });
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
function loadRequestDataBySeq(seq) {
    jQuery(".btn-emp-update-data").hide();
    jQuery(".btn-emp-save-data").show();
    if (seq != "") {
        jQuery.ajax({
            type: "GET",
            url: "/PettyCash/loadRequestDetailsbySeq?seq=" + seq,
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == false) {
                    Logout();
                } else {
                    if (result.success == false) {
                        setInfoMsg(result.msg);
                        jQuery("#RequestNo").val("");
                        jQuery("#RequestNo").focus();
                        jQuery(".print-job-btn").hide();
                        jQuery(".reject-btn").hide();
                        jQuery(".btn-emp-save-data").show();
                        jQuery(".btn-emp-update-data").hide();
                    }
                    if (result.success == true) {
                        if (result.data.TPRH_REQ_NO != null) {
                            jQuery(".print-job-btn").show();
                            jQuery(".reject-btn").show();
                            jQuery("#RequestNo").val(result.data.TPRH_REQ_NO);
                            jQuery("#ReqDate").val(getFormatedDateInput(result.data.TPRH_REQ_DT));
                            jQuery("#ReqType").val(result.data.TPRH_TYPE);
                            jQuery("#ReqBy").val(result.data.TPRH_REQ_BY);
                            //reqByFocusOut(result.data.TPRH_REQ_BY);
                            jQuery("#ManualRef").val(result.data.TPRH_MANUAL_REF);
                            jQuery("#ProfitCenter").val(result.data.TPRH_PC_CD);
                            jQuery("#Remarks").val(result.data.TPRH_REMARKS);
                            jQuery("#RequestSeq").val(result.data.TPRH_SEQ);
                            jQuery("#ReqType").val(result.data.TPRH_TYPE);
                            if (result.data.TPRH_TYPE == "TTREQ") {
                                jQuery("#Consignee").val(result.data.TPRH_PAY_TO);
                                jQuery(".cons-grp").show();
                                jQuery(".pay-grp").hide();
                                consigneeFocusOut(result.data.TPRH_PAY_TO);
                            } else if (result.data.TPRH_TYPE == "PAYREQ") {
                                jQuery("#PayTo").val(result.data.TPRH_PAY_TO);
                                jQuery(".cons-grp").hide();
                                jQuery(".pay-grp").show();
                                paytoFocusOut(result.data.TPRH_PAY_TO);
                            } else {
                                jQuery("#Consignee").val("");
                                jQuery("#PayTo").val("");
                                jQuery(".lbl-name1").empty();
                                jQuery(".lbl-name2").empty();
                                jQuery(".lbl-name3").empty();
                                jQuery(".lbl-name4").empty();
                                jQuery(".lbl-tin").hide();
                                jQuery(".cons-grp").hide();
                                jQuery(".pay-grp").hide();
                            }
                            jQuery(".app-text").empty();
                            if (result.data.TPRH_APP_1 == 1 && result.data.TPRH_APP_2 == 0 && result.data.TPRH_APP_3 == 0) {
                                jQuery(".app-text").html("Status : Approved by level 1");
                            }
                            if (result.data.TPRH_APP_1 == 1 && result.data.TPRH_APP_2 == 1 && result.data.TPRH_APP_3 == 0) {
                                jQuery(".app-text").html("Status : Approved by level 1 and 2");
                            }
                            if (result.data.TPRH_APP_1 == 1 && result.data.TPRH_APP_2 == 1 && result.data.TPRH_APP_3 == 1) {
                                jQuery(".app-text").html("Status : Approved by level 1,2 and 3");
                            }
                            if (result.data.TPRH_APP_1 == 0 && result.data.TPRH_APP_2 == 0 && result.data.TPRH_APP_3 == 0) {
                                jQuery(".app-text").html("Status : Pending");
                            }
                            items = result.item;

                            if (items.length > 0) {
                                jQuery("table.job-dtl-tbl tbody").empty();
                                for (i = 0 ; i < items.length; i++) {
                                    if (items[i].TPRD_ACT == 1) {
                                        jQuery('.job-dtl-tbl').append('<tr class="new-row">' +
                                        '<td style="width:30px;">' + parseFloat(parseFloat(i) + 1) + '</td>' +
                                        '<td style="width:120px;">' + items[i].TPRD_JOB_NO + '</td>' +
                                        '<td style="width:40px;" class="no-display">' + items[i].TPRD_LINE_NO + '</td>' +
                                        '<td style="width:78px;">' + items[i].TPRD_ELEMENT_CD + '</td>' +
                                        '<td style="width:130px;">' + items[i].TPRD_ELEMENT_DESC + '</td>' +
                                        '<td style="width:60px;">' + items[i].TPRD_UOM + '</td>' +
                                        '<td style="width:50px;" class="right-align">' + items[i].TPRD_NO_UNITS + '</td>' +
                                        '<td style="width:75px;" class="right-align">' + addCommas(parseFloat(items[i].TPRD_UNIT_PRICE).toFixed(2)) + '</td>' +
                                        '<td style="width:35px;">' + items[i].TPRD_CURRENCY_CODE + '</td>' +
                                        '<td style="width:40px;" class="right-align">' + items[i].TPRD_EX_RATE + '</td>' +
                                        '<td style="width:75px;" class="right-align">' + addCommas(parseFloat(items[i].TPRD_ELEMENT_AMT).toFixed(2)) + '</td>' +
                                        '<td style="width:85px;">' + items[i].TPRD_COMMENTS + '</td>' +
                                        '<td style="width:75px;">' + getFormatedDateInput(items[i].TPRD_UPLOAD_DATE) + '</td>' +
                                        '<td style="width:60px;">' + items[i].TPRD_VEC_TELE + '</td>' +
                                        '<td style="width:60px;">' + items[i].TPRD_INV_NO + '</td>' +
                                        '<td style="width:50px;">' + items[i].TPRD_INV_DT + '</td>' +
                                        '<td style="width:30px;">' + "<button class='btn btn-sm-min btn-red-fullbg remove-job-list'>" +
                                                "<i class='fa fa-times' aria-hidden='true'></i></button>" + '</td>' +
                                        '</tr>');


                                        //jQuery('.job-dtl-tbl').append('<tr class="new-row">' +
                                        //'<td>' + parseFloat(parseFloat(i) + 1) + '</td>' +
                                        //'<td>' + items[i].TPRD_JOB_NO + '</td>' +
                                        //'<td class="no-display">' + items[i].TPRD_LINE_NO + '</td>' +
                                        //'<td>' + items[i].TPRD_ELEMENT_CD + '</td>' +
                                        //'<td>' + items[i].TPRD_ELEMENT_DESC + '</td>' +
                                        //'<td>' + items[i].TPRD_UOM + '</td>' +
                                        //'<td class="right-align">' + items[i].TPRD_NO_UNITS + '</td>' +
                                        //'<td class="right-align">' + addCommas(parseFloat(items[i].TPRD_UNIT_PRICE).toFixed(2)) + '</td>' +
                                        //'<td>' + items[i].TPRD_CURRENCY_CODE + '</td>' +
                                        //'<td class="right-align">' + items[i].TPRD_EX_RATE + '</td>' +
                                        //'<td class="right-align">' + addCommas(parseFloat(items[i].TPRD_ELEMENT_AMT).toFixed(2)) + '</td>' +
                                        //'<td>' + items[i].TPRD_COMMENTS + '</td>' +
                                        //'<td>' + getFormatedDateInput(items[i].TPRD_UPLOAD_DATE) + '</td>' +
                                        //'<td>' + items[i].TPRD_VEC_TELE + '</td>' +
                                        //'<td>' + items[i].TPRD_INV_NO + '</td>' +
                                        //'<td>' + items[i].TPRD_INV_DT + '</td>' +
                                        //'<td>' + "<button class='btn btn-sm-min btn-red-fullbg remove-job-list'>" +
                                        //        "<i class='fa fa-times' aria-hidden='true'></i></button>" + '</td>' +
                                        //'</tr>');
                                    }
                                }
                            }
                            jQuery("#Total").val(addCommas(parseFloat(result.total).toFixed(2)));
                            jQuery(".btn-emp-update-data").show();
                            jQuery(".btn-emp-save-data").hide();
                            jQuery(".remove-job-list").click(function (evt) {
                                evt.preventDefault();
                                var job = jQuery(this).closest("tr").find('td:eq(1)').text();
                                var tp = jQuery(this);
                                var seq = jQuery("#RequestSeq").val();
                                Lobibox.confirm({
                                    msg: "Do you want to remove job :" + job + " ?",
                                    callback: function ($this, type, ev) {
                                        if (type == "yes") {
                                            var lineno = jQuery(tp).closest("tr").find('td:eq(2)').text();
                                            if (lineno != "" && job != "") {
                                                removeItem(lineno, job, seq);
                                            } else {
                                                setInfoMsg("Invalid job number.");
                                            }

                                        }
                                    }
                                });

                            });
                        } else {
                            setInfoMsg("Invalid request number");
                        }
                    }
                }
            }
        });
    }
}



function clearSession() {
    jQuery.ajax({
        type: "GET",
        url: "/PettyCash/clearSession",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.login == false) {
                Logout();
            }
        }
    });
}



