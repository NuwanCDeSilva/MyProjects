jQuery(document).ready(function () {
    loadReqTypes();
    loadDefaultCurrency();
    userApprovePermission();
    clearSession();
    getRequestUser();

    jQuery("#Remarks").text("PLEASE ARRANGE THE PAYMENT IN CASH");
    jQuery("#topicorg").text("Consignee");
    //jQuery("#ReqDate").datepicker("option", "disabled", true);
    jQuery("#ReqDate").prop('disabled', true);


    jQuery("#ReqType").on("change", function () {
        clearFormOnReqTypeChange();
        jQuery("#Consignee").val("");
        jQuery("#PayTo").val("");
        jQuery(".lbl-name1").empty();
        jQuery(".lbl-name2").empty();
        jQuery(".lbl-name3").empty();
        jQuery(".lbl-name4").empty();
        jQuery(".lbl-tin").hide();
        jQuery("#RequestNo").val("");
        if (jQuery(this).val() == "TTREQ") {
            jQuery(".cons-grp").show();
            jQuery(".pay-grp").hide();
            jQuery(".lbl-name1").html("-");
            jQuery(".lbl-name2").html("-");
            jQuery(".lbl-name3").html("-");
            jQuery(".lbl-name4").html("-");
            jQuery(".lbl-tin").show();
            jQuery("#topicorg").text("Payment To");
            jQuery("#Remarks").text("PLEASE ARRANGE THE PAYMENT BY CHEQUE");
        } else if (jQuery(this).val() == "PAYREQ") {
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
        //jQuery("#Remarks").text("PLEASE ARRANGE THE PAYMENT BY CHEQUE");

        getRequestUser();
    });
    jQuery("#prnReqFrmDt").val(getFormatedPreMonth(new Date()));
    jQuery("#prnReqToDt").val(getFormatedDate(new Date()));
    jQuery("#PayDate").val(getFormatedDate(new Date()));
    jQuery("#ReqDate").val(getFormatedDate(new Date()));
    jQuery("#UploadDate").val(getFormatedDate(new Date()));
    pcfocusout(jQuery("#ProfitCenter").val());
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
    //jQuery(".pc_list").click(function (evt) {
    //    evt.preventDefault();
    //    var headerKeys = Array()
    //    headerKeys = ["Row", "Code", "Description", "Channel"];
    //    field = "ProfitCenter";
    //    var x = new CommonSearch(headerKeys, field);
    //});

    //jQuery("#ProfitCenter").on("keydown", function (evt) {
    //    if (evt.keyCode == 113) {
    //        var headerKeys = Array()
    //        headerKeys = ["Row", "Code", "Description", "Channel"];
    //        field = "ProfitCenter";
    //        var x = new CommonSearch(headerKeys, field);
    //    }
    //});

    jQuery(".inv-no-search").click(function () {

        var headerKeys = Array()
        headerKeys = ["Row", "Invoice No", "Job No", "Date"];
        field = "InvoiceNoDfPetty"
        var x = new CommonSearchDateFiltered(headerKeys, field);
    });

    jQuery("#ReqDate").focusout(function () {
        var code = jQuery(this).val();
        if (code == "") {
            jQuery('#ReqDate').val(my_date_format(new Date()));
        }
    });
    jQuery("#PayDate").focusout(function () {
        var code = jQuery(this).val();
        if (code == "") {
            jQuery('#PayDate').val(my_date_format(new Date()));
        }
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
    jQuery("#UploadDate").focusout(function () {
        var code = jQuery(this).val();
        if (code == "") {
            jQuery('#UploadDate').val(my_date_format(new Date()));
        }
    });
    jQuery("#InvDt").focusout(function () {
        var code = jQuery(this).val();
        if (code == "") {
            jQuery('#InvDt').val(my_date_format(new Date()));
        }
    });

    jQuery(".reqby_searc").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "First Name", "Last Name", "Epf No", "Category", "NIC"];
        field = "employee";
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery(".srch-cons").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Name", "Consignee Code", "Account Code", "Add1", "Mobile"];
        field = "payconsignee";
        data = "ACC_CRD";
        var x = new CommonSearch(headerKeys, field, data);
    });
    jQuery("#Consignee").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Name", "Consignee Code", "Account Code", "Add1", "Mobile"];
            field = "payconsignee";
            data = "ACC_CRD";
            var x = new CommonSearch(headerKeys, field, data);
        }
    });

    jQuery(".pay-to-btn").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Name", "Consignee Code", "Account Code", "Add1", "Mobile"];
        field = "payto";
        data = "ACC_CRD";
        var x = new CommonSearch(headerKeys, field, data);
    });
    jQuery("#PayTo").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Name", "Consignee Code", "Account Code", "Add1", "Mobile"];
            field = "payto";
            data = "ACC_CRD";
            var x = new CommonSearch(headerKeys, field, data);
        }
    });
    jQuery("#RequestNo").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            //var headerKeys = Array()
            //headerKeys = ["Row", "Request No", "Mannual Ref", "Request Date"];
            //field = "ptychsreq";
            //data = jQuery("#ReqType").val();
            //var x = new CommonSearch(headerKeys, field, data);
            var headerKeys = Array()
            headerKeys = ["Row", "Request No", "Mannual Ref", "Request Date"];
            field = "ptychsreq";
            data = jQuery("#ReqType").val();
            var x = new CommonSearchDateFiltered(headerKeys, field, data);
        }
    });

    //jQuery(".req_no_serch").click(function () {
    //    var headerKeys = Array()
    //    headerKeys = ["Row", "Request No", "Mannual Ref", "Request Date"];
    //    field = "ptychsreq";
    //    data = jQuery("#ReqType").val();
    //    var x = new CommonSearch(headerKeys, field, data);
    //});
    jQuery(".req_no_serch").click(function () { // Added by Chathura on 30-sep-2017
        var headerKeys = Array()
        headerKeys = ["Row", "Request No", "Mannual Ref", "Request Date"];
        field = "ptychsreq";
        data = jQuery("#ReqType").val();
        var x = new CommonSearchDateFiltered(headerKeys, field, data);
    });

    jQuery(".job-no-srch").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Job No", "Job Date","Pouch No"];
        field = "ptyjob";
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery("#JobNo").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Job No","Job Date", "Pouch No" ];
            field = "ptyjob";
            var x = new CommonSearch(headerKeys, field);
        }
    });


    jQuery("#ReqBy").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "First Name", "Last Name", "Epf No", "Category", "NIC"];
            field = "employee";
            var x = new CommonSearch(headerKeys, field);
        }
    });

    jQuery(".cst-ele-srch").click(function (evt) {
        evt.preventDefault();
        var headerKeys = Array()
        headerKeys = ["Row", "Description", "Cost Code", "Account Code"];
        field = "costele";
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery("#CstEle").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Description", "Cost Code", "Account Code"];
            field = "costele";
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery("#CstEle").focusout(function () {
        var code = jQuery(this).val();
        cstelemtFocusOut(code);
    });
    
    jQuery(".curency-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description"];
        field = "curcd";
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery("#Currency").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description"];
            field = "curcd";
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery("#Currency").focusout(function () {
        var code = jQuery(this).val();
        CurrencyfocusOut(code);
    });
    function CurrencyfocusOut(code) {
        if (code != "") {
            jQuery.ajax({
                type: "GET",
                url: "/Validation/validateCurrencyCode?curcd=" + code,
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == false) {
                        Logout();
                    } else {
                        if (result.success == false) {
                            setInfoMsg(result.msg);
                            jQuery("#Currency").val("");
                            jQuery("#Currency").focus();
                        }
                        if (result.success == true) {
                            if (result.data.MCR_CD == null) {
                                setInfoMsg("Please enter valid Currency code.");
                                jQuery("#Currency").val("");
                                jQuery("#Currency").focus();
                            } else {
                                jQuery("#ExchgRate").val("");
                                loadExchageRate(code);
                            }
                        }
                    }
                }
            });

        }
    }
    jQuery(".vehlcph-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description"];
        field = "detserch"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery("#VehLcTel").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description"];
            field = "detserch"
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery("#VehLcTel").focusout(function () {
        var code = jQuery(this).val();
        telVehLcFocusout(code);
    });
    
    jQuery("#Units").focusout(function () {
        if (jQuery(this).val() != "") {
            if (Number(jQuery(this).val()) > 0) {
                updateAmount();
            } else {
                jQuery(this).val("");
                setInfoMsg("Unit must be greter than 0");
            }
        }
    });
    jQuery("#UnitPrice").focusout(function () {
        if (jQuery(this).val() != "") {
            if (Number(jQuery(this).val()) > 0) {
                updateAmount();
            } else {
                jQuery(this).val("");
                setInfoMsg("Unit price must be greter than 0");
            }
        }
    });
    
    jQuery(".uom-srch").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description"];
        field = "UOMPTY"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery("#UOM").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description"];
            field = "UOMPTY"
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery("#UOM").focusout(function () {
        var code = jQuery(this).val();
        UOMfocusOut(code);
    });
    
    jQuery("#PayTo").focusout(function () {
        var cuscd = jQuery(this).val();
        paytoFocusOut(cuscd);
    });
    jQuery("#JobNo").focusout(function () {
        var jobnum = jQuery(this).val();
        jobNumberFocusOut(jobnum);
    });
    
    


    jQuery("#Consignee").focusout(function () {
        var cuscd = jQuery(this).val();
        consigneeFocusOut(cuscd);
    });
    jQuery("#ReqBy").focusout(function () {
        var empCd = jQuery(this).val();
        if (empCd != "") {
            reqByFocusOut(empCd);
        }
    });
    
    jQuery("#ProfitCenter").focusout(function () {
        var pc = jQuery(this).val();
        if (pc != "") {
            pcfocusout(pc);
        }
    });

    jQuery("#RequestNo").focusout(function () {
        jQuery(".btn-emp-update-data").hide();
        jQuery(".btn-emp-save-data").show();
        var type = jQuery("#ReqType").val();
        var req = jQuery(this).val();
        if (req != "") {
            jQuery.ajax({
                type: "GET",
                url: "/PettyCash/loadRequestDetails?type=" + type + "&reqno=" + req,
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
                                jQuery("#ReqDate").val(getFormatedDateInput(result.data.TPRH_REQ_DT));
                                jQuery("#ReqBy").val(result.data.TPRH_REQ_BY);
                                reqByFocusOut(result.data.TPRH_REQ_BY);
                                jQuery("#ManualRef").val(result.data.TPRH_MANUAL_REF);
                                jQuery("#ProfitCenter").val(result.data.TPRH_PC_CD);
                                pcfocusout(result.data.TPRH_PC_CD);
                                jQuery("#Remarks").val(result.data.TPRH_REMARKS);
                                jQuery("#RequestSeq").val(result.data.TPRH_SEQ);
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
                                if (result.data.TPRH_TYPE == "TTREQ") {
                                    jQuery("#Consignee").val(result.data.TPRH_PAY_TO);
                                    consigneeFocusOut(result.data.TPRH_PAY_TO);
                                } else if (result.data.TPRH_TYPE == "PAYREQ") {
                                    jQuery("#PayTo").val(result.data.TPRH_PAY_TO);
                                    paytoFocusOut(result.data.TPRH_PAY_TO);
                                } else {
                                    jQuery("#Consignee").val("");
                                    jQuery("#PayTo").val("");
                                    jQuery(".lbl-name1").empty();
                                    jQuery(".lbl-name2").empty();
                                    jQuery(".lbl-name3").empty();
                                    jQuery(".cons-grp").hide();
                                    jQuery(".pay-grp").hide();
                                }
                                jQuery("#ReqType").val(result.data.TPRH_TYPE);
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
                                            '<td style="width:75px;" class="uploaddate">' + getFormatedDateInput(items[i].TPRD_UPLOAD_DATE) + '</td>' +
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
                                            //'<td class="uploaddate">' + getFormatedDateInput(items[i].TPRD_UPLOAD_DATE) + '</td>' +
                                            //'<td>' + items[i].TPRD_VEC_TELE + '</td>' +
                                            //'<td>' + items[i].TPRD_INV_NO + '</td>' +
                                            //'<td>' + items[i].TPRD_INV_DT + '</td>' +
                                            //'<td>' + "<button class='btn btn-sm-min btn-red-fullbg remove-job-list'>" +
                                            //        "<i class='fa fa-times' aria-hidden='true'></i></button>" + '</td>' +
                                            //'</tr>');
                                        }
                                    }

                                }
                                jQuery("#Total").val( addCommas(parseFloat(result.total).toFixed(2)));
                                jQuery(".btn-emp-update-data").show();
                                jQuery(".btn-emp-save-data").hide();
                                jQuery(".reject-btn").show();
                                jQuery(".print-job-btn").show();
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
                                                    removeItem(lineno, job,seq);
                                                } else {
                                                    setInfoMsg("Invalid job number.");
                                                }

                                            }
                                        }
                                    });
                                   
                                });
                                if (result.updateJob == true) {
                                    jQuery("table.job-dtl-tbl tr.new-row").dblclick(function (evt) {
                                        var ele = jQuery(this);
                                        evt.preventDefault();
                                        var job = jQuery(this).closest("tr").find('td:eq(1)').text();
                                        var tp = jQuery(this);
                                        var seq = jQuery("#RequestSeq").val();
                                        Lobibox.confirm({
                                            msg: "Do you want to update job :" + job + " ?",
                                            callback: function ($this, type, ev) {
                                                if (type == "yes") {
                                                    var lineno = jQuery(tp).closest("tr").find('td:eq(2)').text();
                                                    if (lineno != "" && job != "") {
                                                        jQuery("#JobNo").val(jQuery(ele).find('td:eq(1)').text());
                                                        jQuery("#CstEle").val(jQuery(ele).find('td:eq(3)').text());
                                                        jQuery("#CstEleDesc").val(jQuery(ele).find('td:eq(4)').text());
                                                        jQuery("#UOM").val(jQuery(ele).find('td:eq(5)').text());
                                                        jQuery("#Units").val(jQuery(ele).find('td:eq(6)').text());
                                                        jQuery("#UnitPrice").val(jQuery(ele).find('td:eq(7)').text().replace(",", ""));
                                                        jQuery("#Currency").val(jQuery(ele).find('td:eq(8)').text());
                                                        jQuery("#ExchgRate").val(jQuery(ele).find('td:eq(9)').text());
                                                        jQuery("#Amount").val(jQuery(ele).find('td:eq(10)').text().replace(",", ""));
                                                        jQuery("#Comments").val(jQuery(ele).find('td:eq(11)').text());
                                                        jQuery("#UploadDate").val(jQuery(ele).find('td:eq(12)').text());
                                                        jQuery("#VehLcTel").val(jQuery(ele).find('td:eq(13)').text());
                                                        jQuery("#InvNo").val(jQuery(ele).find('td:eq(14)').text());
                                                        jQuery("#InvDt").val(jQuery(ele).find('td:eq(15)').text());
                                                        if (jQuery("#CstEle").val() != "")
                                                            removeItem(lineno, job, seq, false);
                                                    } else {
                                                        setInfoMsg("Invalid job number.Unable to update.");
                                                    }

                                                }
                                            }
                                        });
                                    });
                                } else {
                                    jQuery("table.job-dtl-tbl tr.new-row").dblclick(function (evt) {
                                        setInfoMsg(result.pemissionMsg);
                                    });
                                }

                            } else {
                                setInfoMsg("Invalid request number");
                            }
                        }
                    }
                }
            });
        }
    });
    jQuery(".add-job-btn").click(function (evt) {
        evt.preventDefault();
        jQuery(".add-job-btn").attr('disabled', 'disabled');
        types = "";
        if (jQuery("#ReqType").val() == "PAYREQ")
        {            
            if (jQuery("#InvNo").val() != "")
            {
                if (jQuery("#InvDt").val() == "") {
                    types = "error";
                    setInfoMsg("Please enter invoice date.");
                }
            }
            else {
                types = "error";
                setInfoMsg("Please enter invoice no.");
            }
        }
        //alert(types);
        if (jQuery("#JobNo").val() != "") {
            if (jQuery("#CstEle").val() != "") {
                if (jQuery("#UOM").val() != "") {
                    if (jQuery("#Currency").val() != "") {
                        if (jQuery("#ExchgRate").val() != "") {
                            if (validNumber(jQuery("#ExchgRate").val())) {
                                if (jQuery("#Units").val() != "") {
                                    if (validNumber(jQuery("#Units").val())) {
                                        if (jQuery("#UnitPrice").val() != "") {
                                            if (validNumber(jQuery("#Units").val())) {
                                                if (types == "") {
                                                jQuery.ajax({
                                                    type: "GET",
                                                    data: {
                                                        JobNo: jQuery("#JobNo").val(),
                                                        CstEle: jQuery("#CstEle").val(),
                                                        UOM: jQuery("#UOM").val(),
                                                        Units: jQuery("#Units").val(),
                                                        UnitPrice: jQuery("#UnitPrice").val(),
                                                        Currency: jQuery("#Currency").val(),
                                                        Comments: jQuery("#Comments").val(),
                                                        UploadDate: jQuery("#UploadDate").val(),
                                                        VehLcTel: jQuery("#VehLcTel").val(),
                                                        InvNo: jQuery("#InvNo").val(),
                                                        InvDt: jQuery("#InvDt").val(),
                                                        exrng: jQuery("#ExchgRate").val()
                                                    },
                                                    url: "/PettyCash/addJobDetails",
                                                    contentType: "application/json;charset=utf-8",
                                                    dataType: "json",
                                                    success: function (result) {
                                                        if (result.login == false) {
                                                            Logout();
                                                        } else {
                                                            if (result.success == true) {
                                                                jQuery("table.job-dtl-tbl tbody").empty();
                                                                for (i = 0 ; i < result.reqDet.length; i++) {
                                                                    if (result.reqDet[i].TPRD_ACT == 1) {
                                                                        jQuery('.job-dtl-tbl').append('<tr class="new-row">' +
                                                                        '<td style="width:30px;">' + parseFloat(parseFloat(i) + 1) + '</td>' +
                                                                        '<td style="width:120px;">' + result.reqDet[i].TPRD_JOB_NO + '</td>' +
                                                                        '<td style="width:40px;" class="no-display">' + result.reqDet[i].TPRD_LINE_NO + '</td>' +
                                                                        '<td style="width:78px;">' + result.reqDet[i].TPRD_ELEMENT_CD + '</td>' +
                                                                        '<td style="width:130px;">' + result.reqDet[i].TPRD_ELEMENT_DESC + '</td>' +
                                                                        '<td style="width:60px;">' + result.reqDet[i].TPRD_UOM + '</td>' +
                                                                        '<td style="width:50px;" class="right-align">' + result.reqDet[i].TPRD_NO_UNITS + '</td>' +
                                                                        '<td style="width:75px;" class="right-align">' + addCommas(parseFloat(result.reqDet[i].TPRD_UNIT_PRICE).toFixed(2)) + '</td>' +
                                                                        '<td style="width:35px;">' + result.reqDet[i].TPRD_CURRENCY_CODE + '</td>' +
                                                                        '<td style="width:40px;" class="right-align">' + result.reqDet[i].TPRD_EX_RATE + '</td>' +
                                                                        '<td style="width:75px;" class="right-align">' + addCommas(parseFloat(result.reqDet[i].TPRD_ELEMENT_AMT).toFixed(2)) + '</td>' +
                                                                        '<td style="width:85px;">' + result.reqDet[i].TPRD_COMMENTS + '</td>' +
                                                                        '<td style="width:75px;">' + getFormatedDateInput(result.reqDet[i].TPRD_UPLOAD_DATE) + '</td>' +
                                                                        '<td style="width:60px;">' + result.reqDet[i].TPRD_VEC_TELE + '</td>' +
                                                                        '<td style="width:60px;">' + result.reqDet[i].TPRD_INV_NO + '</td>' +
                                                                        '<td style="width:50px;">' + result.reqDet[i].TPRD_INV_DT + '</td>' +
                                                                        '<td style="width:30px;">' + "<button class='btn btn-sm-min btn-red-fullbg remove-job-list'>" +
                                                                                "<i class='fa fa-times' aria-hidden='true'></i></button>" + '</td>' +
                                                                        '</tr>');

                                                                        //jQuery('.job-dtl-tbl').append('<tr class="new-row">' +
                                                                        //'<td>' + parseFloat(parseFloat(i) + 1) + '</td>' +
                                                                        //'<td>' + result.reqDet[i].TPRD_JOB_NO + '</td>' +
                                                                        //'<td class="no-display">' + result.reqDet[i].TPRD_LINE_NO + '</td>' +
                                                                        //'<td>' + result.reqDet[i].TPRD_ELEMENT_CD + '</td>' +
                                                                        //'<td>' + result.reqDet[i].TPRD_ELEMENT_DESC + '</td>' +
                                                                        //'<td>' + result.reqDet[i].TPRD_UOM + '</td>' +
                                                                        //'<td class="right-align">' + result.reqDet[i].TPRD_NO_UNITS + '</td>' +
                                                                        //'<td class="right-align">' + addCommas(parseFloat(result.reqDet[i].TPRD_UNIT_PRICE).toFixed(2)) + '</td>' +
                                                                        //'<td>' + result.reqDet[i].TPRD_CURRENCY_CODE + '</td>' +
                                                                        //'<td class="right-align">' + result.reqDet[i].TPRD_EX_RATE + '</td>' +
                                                                        //'<td class="right-align">' + addCommas(parseFloat(result.reqDet[i].TPRD_ELEMENT_AMT).toFixed(2)) + '</td>' +
                                                                        //'<td>' + result.reqDet[i].TPRD_COMMENTS + '</td>' +
                                                                        //'<td>' + getFormatedDateInput(result.reqDet[i].TPRD_UPLOAD_DATE) + '</td>' +
                                                                        //'<td>' + result.reqDet[i].TPRD_VEC_TELE + '</td>' +
                                                                        //'<td>' + result.reqDet[i].TPRD_INV_NO + '</td>' +
                                                                        //'<td>' + result.reqDet[i].TPRD_INV_DT + '</td>' +
                                                                        //'<td>' + "<button class='btn btn-sm-min btn-red-fullbg remove-job-list'>" +
                                                                        //        "<i class='fa fa-times' aria-hidden='true'></i></button>" + '</td>' +
                                                                        //'</tr>');

                                                                        $('#CstEle').focus();
                                                                    }
                                                                }
                                                                clearAddJob();
                                                                jQuery("#Total").val(addCommas(parseFloat(result.total).toFixed(2)));
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
                                                                                    removeItem(lineno, job,seq);
                                                                                } else {
                                                                                    setInfoMsg("Invalid job number.");
                                                                                }

                                                                            }
                                                                        }
                                                                    });

                                                                });
                                                                if (result.updateJob == true) {
                                                                    jQuery("table.job-dtl-tbl tr.new-row").dblclick(function (evt) {
                                                                        var ele = jQuery(this);
                                                                        evt.preventDefault();
                                                                        var job = jQuery(this).closest("tr").find('td:eq(1)').text();
                                                                        var tp = jQuery(this);
                                                                        var seq = jQuery("#RequestSeq").val();
                                                                        Lobibox.confirm({
                                                                            msg: "Do you want to update job :" + job + " ?",
                                                                            callback: function ($this, type, ev) {
                                                                                if (type == "yes") {
                                                                                    var lineno = jQuery(tp).closest("tr").find('td:eq(2)').text();
                                                                                    if (lineno != "" && job != "") {
                                                                                        jQuery("#JobNo").val(jQuery(ele).find('td:eq(1)').text());
                                                                                        jQuery("#CstEle").val(jQuery(ele).find('td:eq(3)').text());
                                                                                        jQuery("#CstEleDesc").val(jQuery(ele).find('td:eq(4)').text());
                                                                                        jQuery("#UOM").val(jQuery(ele).find('td:eq(5)').text());
                                                                                        jQuery("#Units").val(jQuery(ele).find('td:eq(6)').text());
                                                                                        jQuery("#UnitPrice").val(jQuery(ele).find('td:eq(7)').text().replace(",", ""));
                                                                                        jQuery("#Currency").val(jQuery(ele).find('td:eq(8)').text());
                                                                                        jQuery("#ExchgRate").val(jQuery(ele).find('td:eq(9)').text());
                                                                                        jQuery("#Amount").val(jQuery(ele).find('td:eq(10)').text().replace(",", ""));
                                                                                        jQuery("#Comments").val(jQuery(ele).find('td:eq(11)').text());
                                                                                        jQuery("#UploadDate").val(jQuery(ele).find('td:eq(12)').text());
                                                                                        jQuery("#VehLcTel").val(jQuery(ele).find('td:eq(13)').text());
                                                                                        jQuery("#InvNo").val(jQuery(ele).find('td:eq(14)').text());
                                                                                        jQuery("#InvDt").val(jQuery(ele).find('td:eq(15)').text());
                                                                                        if (jQuery("#CstEle").val() != "")
                                                                                            removeItem(lineno, job, seq, false);
                                                                                    } else {
                                                                                        setInfoMsg("Invalid job number.Unable to update.");
                                                                                    }

                                                                                }
                                                                            }
                                                                        });
                                                                    });
                                                                } else {
                                                                    jQuery("table.job-dtl-tbl tr.new-row").dblclick(function (evt) {
                                                                        setInfoMsg(result.pemissionMsg);
                                                                    });
                                                                }
                                                            } else {
                                                                setInfoMsg(result.msg);
                                                            }
                                                        }
                                                    }
                                                });
                                                } else {
                                                    setInfoMsg("Please enter invoice no & date.");
                                                }
                                            } else {
                                                setInfoMsg("Please enter valid unit price.");
                                            }
                                        } else {
                                            setInfoMsg("Please enter valid unit price.");
                                        }
                                    } else {
                                        setInfoMsg("Invalid units number.");
                                    }
                                } else {
                                    setInfoMsg("Please enter number of units.");
                                }
                            } else {
                                setInfoMsg("Invalid exchange rate value.");
                            }
                        }
                    } else {
                        setInfoMsg("Please enter currency.");
                    }
                } else {
                    setInfoMsg("Please enter UOM.");
                }
            } else {
                setInfoMsg("Please enter cost element.");
            }
        } else {
            setInfoMsg("Plese enter job number.");
        }
        jQuery(".add-job-btn").removeAttr('disabled');
    });
    jQuery(".add-job-btn").dblclick(function (evt) {
        evt.preventDefault();
    });
    jQuery(".pending-jobbtn").click(function (evt) {
        evt.preventDefault();
        loadPendingRequest();
    });

    jQuery("#btnApp1").click(function (evt) {
        evt.preventDefault();
        reqno = jQuery("#RequestNo").val();
        types = jQuery("#ReqType").val();
        paydate = jQuery("#PayDate").val();
        appLvl = "1";
        Lobibox.confirm({
            msg: "Do you want approve Level 1?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    jQuery.ajax({
                        type: "POST",
                        url: "/PettyCash/approveRequest",
                        dataType: "json",
                        data: { reqno: reqno, type: types, appLvl: appLvl, paydate: paydate },
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
    jQuery("#btnApp2").click(function (evt) {
        evt.preventDefault();
        reqno = jQuery("#RequestNo").val();
        types = jQuery("#ReqType").val();
        appLvl = "2";
        paydate = jQuery("#PayDate").val();
        Lobibox.confirm({
            msg: "Do you want approve Level 2?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    jQuery.ajax({
                        type: "POST",
                        url: "/PettyCash/approveRequest",
                        dataType: "json",
                        data: { reqno: reqno, type: types, appLvl: appLvl, paydate: paydate },
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
    jQuery("#btnApp3").click(function (evt) {
        evt.preventDefault();
        reqno = jQuery("#RequestNo").val();
        types = jQuery("#ReqType").val();
        appLvl = "3";
        paydate = jQuery("#PayDate").val();
        Lobibox.confirm({
            msg: "Do you want approve Level 3?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    jQuery.ajax({
                        type: "POST",
                        url: "/PettyCash/approveRequest",
                        dataType: "json",
                        data: { reqno: reqno, type: types, appLvl: appLvl, paydate: paydate },
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
    jQuery(".btn-emp-save-data").click(function () {
        if (jQuery('#ReqType').val() == "TTREQ" && (jQuery('#Consignee').val() == null || jQuery('#Consignee').val() == "")) {
            setInfoMsg("Please enter payment to.");
            return;
        }
        if (jQuery('#ReqType').val() == "PAYREQ" && (jQuery('#PayTo').val() == null || jQuery('#PayTo').val() == "")) {
            setInfoMsg("Please enter payment to.");
            return;
        }
        if ((jQuery('#ManualRef').val() == null || jQuery('#ManualRef').val() == "")) {
            setInfoMsg("Please enter Manual Ref.");
            return;
        }
        Lobibox.confirm({
            msg: "Do you want to save ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    var formdata = jQuery("#pettcase-frm").serialize();
                    jQuery.ajax({
                        type: "POST",
                        url: "/PettyCash/savePettyCaseDet",
                        data: formdata,
                        dataType: "json",
                        success: function (result) {
                            if (result.login == false) {
                                Logout();
                            } else {
                                if (result.success == true) {
                                    clearForm();
                                    if (result.type == "Success") {
                                        setSuccesssMsg(result.msg);
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
    jQuery(".print-job-btn").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to print request ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    if (jQuery("#RequestNo").val() != "" && jQuery("#RequestSeq").val() != "") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/PettyCash/validatePrint?seq=" + jQuery("#RequestSeq").val(),
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == false) {
                                    Logout();
                                } else {
                                    if (result.success == true) {
                                        window.open("/PettyCash/Print?seq=" + jQuery("#RequestSeq").val(), "_blank");
                                    } else {
                                        if (result.type == "Info") {
                                            setInfoMsg(result.msg);
                                        } else if (result.type == "Error") {
                                            setError(result.msg);
                                        } else {
                                            setInfoMsg(result.msg);
                                        }

                                    }
                                }
                            }
                        });
                    } else {
                        setInfoMsg("Please select request for print.");
                    }
                }
            }
        });
        
    });
    jQuery(".reject-btn").click(function (evt) {
        evt.preventDefault();

        Lobibox.confirm({
            msg: "Do you want to reject request ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    if (jQuery("#RequestNo").val() != "" && jQuery("#RequestSeq").val() != "") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/PettyCash/rejectRequest?seq=" + jQuery("#RequestSeq").val(),
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == false) {
                                    Logout();
                                } else {
                                    if (result.success == true) {
                                        clearForm();
                                        if (result.type == "Success") {
                                            setSuccesssMsg(result.msg);
                                        }
                                    } else {
                                        if (result.type == "Info") {
                                            setInfoMsg(result.msg);
                                        } else if (result.type == "Error") {
                                            setError(result.msg);
                                        } else {
                                            setInfoMsg(result.msg);
                                        }

                                    }
                                }
                            }
                        });
                    } else {
                        setInfoMsg("Please select request for print.");
                    }
                }
            }
        });
       
    });
    jQuery(".btn-emp-update-data").click(function (evt) {
        evt.preventDefault();
        jQuery(this).attr('disabled', 'disabled');
        seq = jQuery("#RequestSeq").val();
        ManualRef = jQuery("#ManualRef").val();
        Remarks = jQuery("#Remarks").val();
        ProfitCenter = jQuery("#ProfitCenter").val();
        Lobibox.confirm({
            msg: "Do you want update request?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    jQuery.ajax({
                        type: "POST",
                        url: "/PettyCash/updateRequest",
                        dataType: "json",
                        data: { seq: seq, ManualRef: ManualRef, Remarks: Remarks, ProfitCenter: ProfitCenter },
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
        jQuery(this).removeAttr('disabled');
    });
    jQuery("#ExchgRate").change(function () {
        if (jQuery(this).val() != "") {
            if (Number(jQuery(this).val()) > 0) {
                updateAmount();
            } else {
                jQuery(this).val("");
                setInfoMsg("Exchange rate must be greter than 0");
            }
        }
    });
});

function loadReqTypes() {
    jQuery.ajax({
        type: "GET",
        url: "/PettyCash/loadRetuestTypes",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.login == true) {
                if (result.success == true) {
                    var select = document.getElementById("ReqType");
                    jQuery("#ReqType").empty();
                    var options = [];
                    var option = document.createElement('option');
                    if (result.data != null && result.data.length != 0) {
                        for (i = 0; i < result.data.length; i++) {
                            option.text = result.data[i].MRT_DESC;
                            option.value = result.data[i].MRT_CD;
                            options.push(option.outerHTML);
                        }
                    } else {
                        option.text = "Select Type";
                        option.value = "";
                        options.push(option.outerHTML);
                    }
                    select.insertAdjacentHTML('beforeEnd', options.join('\n'));
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
                    } else if (jQuery("#ReqType").val() == "PAYREQ") {
                        jQuery(".cons-grp").hide();
                        jQuery(".pay-grp").show();
                        jQuery(".lbl-name1").html("-");
                        jQuery(".lbl-name2").html("-");
                        jQuery(".lbl-name3").html("-");
                        jQuery(".lbl-name4").html("-");
                        jQuery(".lbl-tin").show();
                    } else {
                        jQuery(".cons-grp").hide();
                        jQuery(".pay-grp").hide();
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
    pcfocusout(jQuery("#ProfitCenter").val());
}
function clearFormOnReqTypeChange() {
    //document.getElementById("pettcase-frm").reset();
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
    jQuery("#ReqBy").val("");
    clearSession();
    loadDefaultCurrency();
    loadPendingRequest();
    jQuery("#Remarks").text("PLEASE ARRANGE THE PAYMENT IN CASH");
    pcfocusout(jQuery("#ProfitCenter").val());
}
function loadDefaultCurrency() {
    jQuery.ajax({
        type: "GET",
        url: "/PettyCash/loadCompanyDta",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.login == false) {
                Logout();
            } else {
                if (result.success == false) {
                    setInfoMsg(result.msg);
                }
                if (result.success == true) {
                    if (result.data.MC_CD == null) {
                        setInfoMsg("Invalid login company.");
                    }
                    if (result.data.MC_CUR_CD == null) {
                        setInfoMsg("Please setup company default currency.");
                    } else {
                        jQuery("#Currency").val(result.data.MC_CUR_CD);

                        if (jQuery("#Currency").val() != "") {
                            loadExchageRate(jQuery("#Currency").val());
                        }
                    }
                }
            }
        }
    });
}

function loadExchageRate(code) {
    if (code != "") {
        jQuery.ajax({
            type: "GET",
            url: "/Validation/getExcahaneRate",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: { currency: code },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        jQuery("#ExchgRate").val(result.data);
                        updateAmount();
                    } else {
                        if (result.type == "Error") {
                            setError(result.msg);
                        } else if (result.type == "Info") {
                            setInfoMsg(result.msg);
                        }
                        jQuery("#Currency").val("");
                        jQuery("#ExchgRate").val("");
                    }
                } else {
                    Logout();
                }
            }
        });
    }
}
function updateAmount() {
    if (jQuery("#Units").val() != "")
    {
        var units = jQuery("#Units").val();
        if (jQuery("#UnitPrice").val() != "") {
            var uprice = jQuery("#UnitPrice").val();
            if (jQuery("#Currency").val() != "") {
                var cur = jQuery("#Currency").val();
                var exrng=jQuery("#ExchgRate").val();
                jQuery.ajax({
                    type: "GET",
                    url: "/PettyCash/updateAmount",
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    data: { units: units, uprice: uprice, cur: cur, exrng: exrng },
                    success: function (result) {
                        if (result.login == true) {
                            if (result.success == true) {
                                jQuery("#Amount").val(result.priceAmt);
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
    }
}

function userApprovePermission() {
    jQuery.ajax({
        type: "GET",
        url: "/PettyCash/getApprovePermission",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.login == true) {
                if (result.success == true) {
                    if (result.appPer == "1") {
                        jQuery("#btnApp1").removeAttr('disabled');
                        jQuery("#btnApp2").attr('disabled', 'disabled');
                        jQuery("#btnApp3").attr('disabled', 'disabled');
                        jQuery("#btnApp2").removeClass("hvr-sweep-to-left hvr-sweep-to-left-ash btn-ash");
                        jQuery("#btnApp3").removeClass("hvr-sweep-to-left hvr-sweep-to-left-green btn-green");
                    } else if (result.appPer == "2") {
                        jQuery("#btnApp2").removeAttr('disabled');
                        jQuery("#btnApp1").attr('disabled', 'disabled');
                        jQuery("#btnApp3").attr('disabled', 'disabled');
                        jQuery("#btnApp1").removeClass("hvr-sweep-to-left hvr-sweep-to-left-yellow btn-yellow");
                        jQuery("#btnApp3").removeClass("hvr-sweep-to-left hvr-sweep-to-left-green btn-green");
                    } else if (result.appPer == "3") {
                        jQuery("#btnApp3").removeAttr('disabled');
                        jQuery("#btnApp2").attr('disabled', 'disabled');
                        jQuery("#btnApp1").attr('disabled', 'disabled');
                        jQuery("#btnApp1").removeClass("hvr-sweep-to-left hvr-sweep-to-left-yellow btn-yellow");
                        jQuery("#btnApp2").removeClass("hvr-sweep-to-left hvr-sweep-to-left-ash btn-ash");
                    } else {
                        jQuery("#btnApp1").removeAttr('disabled');
                        jQuery("#btnApp2").removeAttr('disabled');
                        jQuery("#btnApp3").removeAttr('disabled');
                        jQuery("#btnApp1").removeClass("hvr-sweep-to-left hvr-sweep-to-left-yellow btn-yellow");
                        jQuery("#btnApp2").removeClass("hvr-sweep-to-left hvr-sweep-to-left-ash btn-ash");
                        jQuery("#btnApp3").removeClass("hvr-sweep-to-left hvr-sweep-to-left-green btn-green");
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
function clearAddJob() {
    //jQuery("#JobNo").val("");
    jQuery("#CstEle").val("");
    jQuery("#CstEleDesc").val("");
    jQuery("#CstEleDesc").css('cursor', 'default').attr('title', '');
    jQuery("#UOM").val("");
    jQuery("#Units").val("");
    jQuery("#UnitPrice").val("");
    jQuery("#Amount").val("");
    jQuery("#Comments").val("");
    jQuery("#VehLcTel").val("");
    //jQuery("#InvNo").val("");
}
function loadPendingRequest() {
    jQuery.ajax({
        type: "GET",
        url: "/PettyCash/loadPendingRequests",
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
                            reqByFocusOut(result.data.TPRH_REQ_BY);
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
                                                removeItem(lineno, job,seq);
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
function paytoFocusOut(cuscd) {
    if (cuscd != "") {
        jQuery.ajax({
            type: "GET",
            url: "/Validation/validateConsigneeAccountCode?cuscd=" + cuscd + "&type=ACC_CRD",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == false) {
                    Logout();
                } else {
                    jQuery(".lbl-name1").empty();
                    jQuery(".lbl-name2").empty();
                    jQuery(".lbl-name3").empty();
                    jQuery(".lbl-name4").empty();
                    jQuery(".lbl-name1").html("-");
                    jQuery(".lbl-name2").html("-");
                    jQuery(".lbl-name3").html("-");
                    jQuery(".lbl-name4").html("-");
                    jQuery(".lbl-tin").show();
                    if (result.success == false) {
                        setInfoMsg(result.msg);
                        jQuery("#PayTo").val("");
                        jQuery("#PayTo").focus();
                    }
                    if (result.success == true) {
                        if (result.data.MBE_ACC_CD == null) {
                            setInfoMsg("Invalid pay customer.");
                            jQuery("#PayTo").val("");
                            jQuery("#PayTo").focus();
                        } else if (result.data.MBE_ACT == 0) {
                            setInfoMsg("Inactive customer.");
                            jQuery("#PayTo").val("");
                            jQuery("#PayTo").focus();
                        } else if (result.data.MBE_IS_SUSPEND == 1) {
                            setInfoMsg("Suspended customer.");
                            jQuery("#PayTo").val("");
                            jQuery("#PayTo").focus();
                        } else if (result.data.MBE_ACC_CD != null) {
                            jQuery(".lbl-name1").html(result.data.MBE_NAME);
                            jQuery(".lbl-name2").html(result.data.MBE_ADD1);
                            jQuery(".lbl-name3").html(result.data.MBE_ADD2);
                            jQuery(".lbl-tin").show();
                            jQuery(".lbl-name4").html(result.data.MBE_OTH_ID_NO);
                        }
                    }
                }
            }
        });
    }
}
function consigneeFocusOut(cuscd) {
    if (cuscd != "") {
        jQuery.ajax({
            type: "GET",
            url: "/Validation/validateConsigneeAccountCode?cuscd=" + cuscd + "&type=ACC_CRD",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == false) {
                    Logout();
                } else {
                    jQuery(".lbl-name1").empty();
                    jQuery(".lbl-name2").empty();
                    jQuery(".lbl-name3").empty
                    jQuery(".lbl-tin").hide();
                    jQuery(".lbl-name4").empty();
                    if (result.success == false) {
                        setInfoMsg(result.msg);
                        jQuery("#Consignee").val("");
                        jQuery("#Consignee").focus();
                    }
                    if (result.success == true) {
                        if (result.data.MBE_ACC_CD == null) {
                            setInfoMsg("Invalid consignee account code.");
                            jQuery("#Consignee").val("");
                            jQuery("#Consignee").focus();
                        } else if (result.data.MBE_ACT == 0) {
                            setInfoMsg("Inactive customer.");
                            jQuery("#Consignee").val("");
                            jQuery("#Consignee").focus();
                        } else if (result.data.MBE_IS_SUSPEND == 1) {
                            setInfoMsg("Suspended customer.");
                            jQuery("#Consignee").val("");
                            jQuery("#Consignee").focus();
                        } else if (result.data.MBE_ACC_CD != null) {
                            jQuery(".lbl-name1").html(result.data.MBE_NAME);
                            jQuery(".lbl-name2").html(result.data.MBE_ADD1);
                            jQuery(".lbl-name3").html(result.data.MBE_ADD2);
                            jQuery(".lbl-name4").html(result.data.MBE_OTH_ID_NO);
                            jQuery(".lbl-tin").show();
                        }
                    }
                }
            }
        });
    }
}

function cstelemtFocusOut(code) {
    if (code != "") {
        jQuery.ajax({
            type: "GET",
            url: "/Validation/validateCostElement?eleCode=" + code,
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == false) {
                    Logout();
                } else {
                    if (result.success == false) {
                        setInfoMsg(result.msg);
                        jQuery("#CstEle").val("");
                        jQuery("#CstEleDesc").val("");
                        jQuery("#CstEle").focus();
                    }
                    if (result.success == true) {
                        if (result.data.MCE_CD == null) {
                            setInfoMsg("Please enter valid cost element code.");
                            jQuery("#CstEle").val("");
                            jQuery("#CstEleDesc").val("");
                            jQuery("#CstEle").focus();
                        } else {
                            jQuery("#CstEleDesc").val(result.data.MCE_DESC);
                            $("#CstEleDesc").css('cursor', 'default').attr('title', result.data.MCE_DESC);
                        }
                    }
                }
            }
        });
    }
}
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
function UOMfocusOut(code) {
    if (code != "") {
        if (code != "") {
            jQuery.ajax({
                type: "GET",
                url: "/Validation/validateUOM?uomcd=" + code,
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == false) {
                        Logout();
                    }
                    if (result.success == false) {
                        setInfoMsg(result.msg);
                        jQuery("#UOM").val("");
                        jQuery("#UOM").focus();
                    }
                    if (result.success == true) {
                        if (result.data.MT_CD == null) {
                            setInfoMsg("Please enter valid UOM.");
                            jQuery("#UOM").val("");
                            jQuery("#UOM").focus();
                        }
                    }
                }
            });
        }
    }
}
function jobNumberFocusOut(jobnum) {
    if (jobnum != "") {
        if (jobnum != "") {
            jQuery.ajax({
                type: "GET",
                url: "/Validation/validateJobNumber?jobNum=" + jobnum,
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == false) {
                        Logout();
                    } else {
                        if (result.success == false) {
                            setInfoMsg(result.msg);
                            jQuery("#JobNo").val("");
                            jQuery("#JobNo").focus();
                        }
                        if (result.success == true) {
                            if (result.data.Jb_jb_no == null) {
                                setInfoMsg("Please enter valid job number.");
                                jQuery("#JobNo").val("");
                                jQuery("#JobNo").focus();
                            }
                        }
                    }
                }
            });
        }
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
function removeItem(itmline, jobnum, seq, clear) {
    if (clear == null) {
        clear = true;
    }
    jQuery.ajax({
        type: "GET",
        url: "/PettyCash/removeJob?itmline=" + itmline + "&jobnum=" + jobnum + "&seq=" + seq,
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.login == false) {
                Logout();
            } else {
                if (result.success == false) {
                    setInfoMsg(result.msg);
                }
                if (result.success == true) {
                    if (result.login == false) {
                        Logout();
                    } else {
                        if (result.success == true) {
                            jQuery("table.job-dtl-tbl tbody").empty();

                            for (i = 0 ; i < result.reqDet.length; i++) {
                                if (result.reqDet[i].TPRD_ACT == 1) {
                                    jQuery('.job-dtl-tbl').append('<tr class="new-row">' +
                                    '<td style="width:30px;">' + parseFloat(parseFloat(i) + 1) + '</td>' +
                                    '<td style="width:100px;">' + result.reqDet[i].TPRD_JOB_NO + '</td>' +
                                    '<td style="width:40px;" class="no-display">' + result.reqDet[i].TPRD_LINE_NO + '</td>' +
                                    '<td style="width:40px;">' + result.reqDet[i].TPRD_ELEMENT_CD + '</td>' +
                                    '<td style="width:130px;">' + result.reqDet[i].TPRD_ELEMENT_DESC + '</td>' +
                                    '<td style="width:60px;">' + result.reqDet[i].TPRD_UOM + '</td>' +
                                    '<td style="width:50px;" class="right-align">' + result.reqDet[i].TPRD_NO_UNITS + '</td>' +
                                    '<td style="width:75px;" class="right-align">' + addCommas(parseFloat(result.reqDet[i].TPRD_UNIT_PRICE).toFixed(2)) + '</td>' +
                                    '<td style="width:35px;">' + result.reqDet[i].TPRD_CURRENCY_CODE + '</td>' +
                                    '<td style="width:40px;" class="right-align">' + result.reqDet[i].TPRD_EX_RATE + '</td>' +
                                    '<td style="width:75px;" class="right-align">' + addCommas(parseFloat(result.reqDet[i].TPRD_ELEMENT_AMT).toFixed(2)) + '</td>' +
                                    '<td style="width:85px;">' + result.reqDet[i].TPRD_COMMENTS + '</td>' +
                                    '<td style="width:75px;">' + getFormatedDateInput(result.reqDet[i].TPRD_UPLOAD_DATE) + '</td>' +
                                    '<td style="width:60px;">' + result.reqDet[i].TPRD_VEC_TELE + '</td>' +
                                    '<td style="width:80px;">' + result.reqDet[i].TPRD_INV_NO + '</td>' +
                                    '<td style="width:75px;">' + result.reqDet[i].TPRD_INV_DT + '</td>' +
                                    '<td style="width:30px;">' + "<button class='btn btn-sm-min btn-red-fullbg remove-job-list'>" +
                                            "<i class='fa fa-times' aria-hidden='true'></i></button>" + '</td>' +
                                    '</tr>');

                                    //jQuery('.job-dtl-tbl').append('<tr class="new-row">' +
                                    //'<td>' + parseFloat(parseFloat(i) + 1) + '</td>' +
                                    //'<td>' + result.reqDet[i].TPRD_JOB_NO + '</td>' +
                                    //'<td class="no-display">' + result.reqDet[i].TPRD_LINE_NO + '</td>' +
                                    //'<td>' + result.reqDet[i].TPRD_ELEMENT_CD + '</td>' +
                                    //'<td>' + result.reqDet[i].TPRD_ELEMENT_DESC + '</td>' +
                                    //'<td>' + result.reqDet[i].TPRD_UOM + '</td>' +
                                    //'<td class="right-align">' + result.reqDet[i].TPRD_NO_UNITS + '</td>' +
                                    //'<td class="right-align">' + addCommas(parseFloat(result.reqDet[i].TPRD_UNIT_PRICE).toFixed(2)) + '</td>' +
                                    //'<td>' + result.reqDet[i].TPRD_CURRENCY_CODE + '</td>' +
                                    //'<td class="right-align">' + result.reqDet[i].TPRD_EX_RATE + '</td>' +
                                    //'<td class="right-align">' + addCommas(parseFloat(result.reqDet[i].TPRD_ELEMENT_AMT).toFixed(2)) + '</td>' +
                                    //'<td>' + result.reqDet[i].TPRD_COMMENTS + '</td>' +
                                    //'<td>' + getFormatedDateInput(result.reqDet[i].TPRD_UPLOAD_DATE) + '</td>' +
                                    //'<td>' + result.reqDet[i].TPRD_VEC_TELE + '</td>' +
                                    //'<td>' + result.reqDet[i].TPRD_INV_NO + '</td>' +
                                    //'<td>' + result.reqDet[i].TPRD_INV_DT + '</td>' +
                                    //'<td>' + "<button class='btn btn-sm-min btn-red-fullbg remove-job-list'>" +
                                    //        "<i class='fa fa-times' aria-hidden='true'></i></button>" + '</td>' +
                                    //'</tr>');
                                }
                            }
                            if (clear == true)
                            { clearAddJob(); }
                            jQuery("#Total").val(addCommas(parseFloat(result.total).toFixed(2)));
                            jQuery(".remove-job-list").click(function (evt) {
                                evt.preventDefault();
                                var job = jQuery(this).closest("tr").find('td:eq(1)').text();
                                var tp = jQuery(this);
                                var seq=jQuery("#RequestSeq").val();
                                Lobibox.confirm({
                                    msg: "Do you want to remove job :" + job + " ?",
                                    callback: function ($this, type, ev) {
                                        if (type == "yes") {
                                            var lineno = jQuery(tp).closest("tr").find('td:eq(2)').text();
                                            if (lineno != "" && job != "") {
                                                removeItem(lineno, job,seq);
                                            } else {
                                                setInfoMsg("Invalid job number.");
                                            }

                                        }
                                    }
                                });

                            });
                            if (result.updateJob == true) {
                                jQuery("table.job-dtl-tbl tr.new-row").dblclick(function (evt) {
                                    var ele = jQuery(this);
                                    evt.preventDefault();
                                    var job = jQuery(this).closest("tr").find('td:eq(1)').text();
                                    var tp = jQuery(this);
                                    var seq = jQuery("#RequestSeq").val();
                                    Lobibox.confirm({
                                        msg: "Do you want to update job :" + job + " ?",
                                        callback: function ($this, type, ev) {
                                            if (type == "yes") {
                                                var lineno = jQuery(tp).closest("tr").find('td:eq(2)').text();
                                                if (lineno != "" && job != "") {
                                                    jQuery("#JobNo").val(jQuery(ele).find('td:eq(1)').text());
                                                    jQuery("#CstEle").val(jQuery(ele).find('td:eq(3)').text());
                                                    jQuery("#CstEleDesc").val(jQuery(ele).find('td:eq(4)').text());
                                                    jQuery("#UOM").val(jQuery(ele).find('td:eq(5)').text());
                                                    jQuery("#Units").val(jQuery(ele).find('td:eq(6)').text());
                                                    jQuery("#UnitPrice").val(jQuery(ele).find('td:eq(7)').text().replace(",", ""));
                                                    jQuery("#Currency").val(jQuery(ele).find('td:eq(8)').text());
                                                    jQuery("#ExchgRate").val(jQuery(ele).find('td:eq(9)').text());
                                                    jQuery("#Amount").val(jQuery(ele).find('td:eq(10)').text().replace(",", ""));
                                                    jQuery("#Comments").val(jQuery(ele).find('td:eq(11)').text());
                                                    jQuery("#UploadDate").val(jQuery(ele).find('td:eq(12)').text());
                                                    jQuery("#VehLcTel").val(jQuery(ele).find('td:eq(13)').text());
                                                    jQuery("#InvNo").val(jQuery(ele).find('td:eq(14)').text());
                                                    jQuery("#InvDt").val(jQuery(ele).find('td:eq(15)').text());
                                                    if (jQuery("#CstEle").val() != "")
                                                        removeItem(lineno, job, seq, false);
                                                } else {
                                                    setInfoMsg("Invalid job number.Unable to update.");
                                                }

                                            }
                                        }
                                    });
                                });
                            } else {
                                jQuery("table.job-dtl-tbl tr.new-row").dblclick(function (evt) {
                                    setInfoMsg(result.pemissionMsg);
                                });
                            }
                        } else {
                            setInfoMsg(result.msg);
                        }
                    }
                }
            }
        }
    });
}
function pcfocusout(pc) {
    jQuery.ajax({
        type: "GET",
        url: "/Validation/validateProfitCenter?pccd=" + pc,
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.login == false) {
                Logout();
            } else {
                if (result.success == false) {
                    setInfoMsg(result.msg);
                    jQuery("#ProfitCenter").val("");
                    jQuery("#ProfitCenter").focus();
                }
                if (result.success == true) {
                    if (result.data.MPC_CD == null) {
                        setInfoMsg("Please enter valid profit center.");
                        jQuery("#ProfitCenter").val("");
                        jQuery("#ProfitCenter").focus();
                    } else {
                        jQuery(".pc-desc").empty();
                        jQuery(".pc-desc").html(result.data.MPC_DESC);
                    }
                }
            }
        }
    });
}
function getRequestUser() {
    jQuery.ajax({
        type: "GET",
        //url: "/Validation/validateEmployeeByCode?empCd=" + empCd,
        url: "/PettyCash/getRequestUser",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.login == false) {
                Logout();
            } else {
                if (result.success == false) {
                    setInfoMsg(result.msg);
                    jQuery("#ReqBy").val("");
                    jQuery("#ReqBy").focus();
                }
                if (result.success == true) {
                    jQuery(".reqbydesc").empty();
                    if (result.data.ESEP_EPF == null) {
                        setInfoMsg("Please enter valid request by user.");
                        jQuery("#ReqBy").val("");
                        jQuery("#ReqBy").focus();
                    }
                    else {
                        jQuery("#ReqBy").val(result.data.ESEP_EPF);
                        jQuery(".reqbydesc").html(result.data.ESEP_FIRST_NAME);
                    }
                }
            }
        }
    });
}
function  reqByFocusOut(empCd){
    jQuery.ajax({
        type: "GET",
        url: "/Validation/validateEmployeeByCode?empCd=" + empCd,
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.login == false) {
                Logout();
            } else {
                if (result.success == false) {
                    setInfoMsg(result.msg);
                    jQuery("#ReqBy").val("");
                    jQuery("#ReqBy").focus();
                }
                if (result.success == true) {
                    jQuery(".reqbydesc").empty();
                    if (result.data.ESEP_EPF == null) {
                        setInfoMsg("Please enter valid request by user.");
                        jQuery("#ReqBy").val("");
                        jQuery("#ReqBy").focus();
                    }
                    else {
                        jQuery(".reqbydesc").html(result.data.ESEP_FIRST_NAME);
                    }
                }
            }
        }
    });
}

function setCursorPosition(pos) {
    this.each(function (index, elem) {
        if (elem.setSelectionRange) {
            elem.setSelectionRange(pos, pos);
        } else if (elem.createTextRange) {
            var range = elem.createTextRange();
            range.collapse(true);
            range.moveEnd('character', pos);
            range.moveStart('character', pos);
            range.select();
        }
    });
    return this;
};