jQuery(document).ready(function () {
    clearSession();

    var balanceAmount = 0.00;
    var balAmount = 0.00;
    var refundAmount = 0.00;
    var hasPermission = false;

    hasPermission = checkPermission();
   
    hasPermission = $("#permissonSession").data('value');
    

    jQuery("#SettlementDate").val(getFormatedDate(new Date()));
    jQuery("#payDt").val(getFormatedDate(new Date()));
    jQuery("#FromDt").val(getFormatedPreMonth(new Date()));
    jQuery("#ToDt").val(getFormatedDate(new Date()));
    jQuery(".btn-update-data").hide();
    jQuery("#TotReqAmt").val("0.00");
    jQuery("#TotSettleAmt").val("0.00");
    pcfocusout(jQuery("#ProfitCenter").val());
    jQuery('.btn-update-data').removeAttr("disabled");
    //jQuery('.btn-update-data').attr("disabled", "false");
    jQuery('#btnRefund').hide();
    jQuery('#btnRefundSave').hide();
    jQuery('#paysettle').hide();
    jQuery('#refunddetails').hide();
    jQuery('.btn-refund_cls').hide();
   
    if (hasPermission == "True") {
      
        jQuery('#btnRefundAll').show();
    }
    else {
       
        jQuery('#btnRefundAll').hide();
    }
 

    jQuery(".refund-table tbody").empty();

    jQuery(".pc-search").click(function (evt) {
        evt.preventDefault();
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description", "Channel"];
        field = "ptycshsetpc";
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery("#ProfitCenter").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description", "Channel"];
            field = "ptycshsetpc";
            var x = new CommonSearch(headerKeys, field);
        }
    });

    jQuery("#ProfitCenter").focusout(function () {
        var pc = jQuery(this).val();
        if (pc != "") {
            pcfocusout(pc);
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
    jQuery(".btn-search-jobno").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Job No", "Pouch No", "Date", "Status"];
        field = "jobnobl"
        var x = new CommonSearchDateFiltered(headerKeys, field);
    });
    jQuery("#Bl_job_no").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Job No", "Pouch No", "Date", "Status"];
            field = "jobnobl"
            var x = new CommonSearchDateFiltered(headerKeys, field);
        }
    });
    jQuery(".btn-search-jobno1").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Job No", "Pouch No", "Date", "Status"];
        field = "jobnobl1"
        var x = new CommonSearchDateFiltered(headerKeys, field);
    });
    jQuery("#Bl_job_no1").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Job No", "Pouch No", "Date", "Status"];
            field = "jobnobl1"
            var x = new CommonSearchDateFiltered(headerKeys, field);
        }
    });

    jQuery(".btn-cust_search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Name", "NIC", "Mobile", "BR No"];
        field = "cusCode11"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery("#Sar_debtor_cd").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Name", "NIC", "Mobile", "BR No"];
            field = "cusCode11"
            var x = new CommonSearch(headerKeys, field);
        }
    });

    jQuery("#btnRefund").click(function () { // Change #btnRefund_ to #btnRefund
        
        if (jQuery('#Bl_job_no').val() == null || jQuery('#Bl_job_no').val() == "") {
            setInfoMsg("Please select a job");
            return;
        }

        jQuery.ajax({
            type: "GET",
            url: "/PettyCashSettlement/validateHasJobWiseExists",
            data: { jobno: jQuery('#Bl_job_no').val() },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
              
                if (result.data == true) {
                
                    setInfoMsg("Refund details already added to the seleted job");
                    return;
                }
                else if (result.isAdded == true) {
                    setInfoMsg("Selected job no has been already fully refunded");
                    return;
                }
                else if (result.hasApprove == false) {
                    setInfoMsg("Settlement is not approved");
                    return;
                }
                else if (result.hasRejected == true) {
                    setInfoMsg("Settlement is rejected");
                    return;
                }
                else {

                    jQuery('#paysettle').show();
                    loadPayModesTypes("REFUND");
                    jQuery('#btnRefundSave').show();
                    jQuery('#btnRefund').hide();
                    jQuery('#refunddetails').show();

                    jQuery.ajax({
                        type: "GET",
                        url: "/PettyCashSettlement/ShowRefundAmounts",
                        data: null,
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            jQuery('#Sar_debtor_cd').focus();
                         
                            for (i = 0; i < result.data.length; i++) {
                                jQuery('table.refund-table').append('<tr class="new-row">' +
                                        '<td>' + result.data[i].TPSD_SETTLE_NO + '</td>' +
                                        '<td>' + result.data[i].TPSD_REQ_NO + '</td>' +
                                       '<td>' + result.data[i].TPSD_JOB_NO + '</td>' +
                                       '<td>' + addCommas(parseFloat(result.data[i].TPSD_SETTLE_AMT).toFixed(2)) + '</td>' +
                                       '<td style="width:30px;">' + "<input type='checkbox' class='select-refund-amnt'>" + '</td>' +
                                       '</tr>');
                            }
                            jQuery(".select-refund-amnt").unbind('click').click(function (evt) {
                            
                                var settleNo = jQuery(this).closest("tr").find('td:eq(0)').text();
                                var amnt = Number(jQuery(this).closest("tr").find('td:eq(3)').text().toString().replace(/[^0-9\.-]+/g, ""));
                           
                                if (jQuery(this).is(':checked') == true) {
                                    jQuery.ajax({
                                        type: "GET",
                                        url: "/PettyCashSettlement/AddRemoveRefunds",
                                        data: { settleNo: settleNo, remove: false },
                                        contentType: "application/json;charset=utf-8",
                                        dataType: "json",
                                        success: function (result) {
                                         
                                            refundAmount = refundAmount + amnt;
                                            jQuery('#Sird_settle_amt').val(addCommas(parseFloat(refundAmount).toFixed(2)));
                                            jQuery(".bal-amount-val").html(addCommas(parseFloat(refundAmount).toFixed(2)));
                                            jQuery(".tot-amount-val").html(addCommas(parseFloat(refundAmount).toFixed(2)));
                                         
                                        }
                                    });
                                } else {
                                    jQuery.ajax({
                                        type: "GET",
                                        url: "/PettyCashSettlement/AddRemoveRefunds",
                                        data: { settleNo: settleNo, remove: true },
                                        contentType: "application/json;charset=utf-8",
                                        dataType: "json",
                                        success: function (result) {
                                          
                                            refundAmount = refundAmount - amnt;
                                            jQuery('#Sird_settle_amt').val(addCommas(parseFloat(refundAmount).toFixed(2)));
                                            jQuery(".bal-amount-val").html(addCommas(parseFloat(refundAmount).toFixed(2)));
                                            jQuery(".tot-amount-val").html(addCommas(parseFloat(refundAmount).toFixed(2)));
                                           
                                        }
                                    });
                                }

                            });
                        }
                    });
                }
            }
        });

        

    });
    
    jQuery("#Bl_job_no").focusout(function () {
        var code = jQuery(this).val();
        jobNumberFocusOut(code);
    });

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
                                jQuery("#Bl_job_no").val("");
                            }
                            if (result.success == true) {
                                if (result.data.Jb_jb_no == null) {
                                    setInfoMsg("Please enter valid job number.");
                                    jQuery("#Bl_job_no").val("");
                                } else {

                                    // Load job wise settlement records

                                    jQuery.ajax({
                                        type: "GET",
                                        url: "/PettyCashSettlement/LoadAllRefundableJobData",
                                        contentType: "application/json;charset=utf-8",
                                        dataType: "json",
                                        data: { jobno: jQuery("#Bl_job_no").val() },
                                        success: function (result) {
                                            if (result.login == true) {
                                                jQuery(".set-itmdet tbody").empty();
                                                if (result.success == true) {
                                                    if (result.data.length > 0) {
                                                      
                                                        for (i = 0 ; i < result.data.length; i++) {
                                                       
                                                            jQuery('.set-itmdet').append('<tr class="new-row ' + '' + '">' +
                                                            '<td>'+ result.data[i].TPSD_SETTLE_NO + '</td>' +
                                                            '<td style="width:120px;>' + result.data[i].TPSD_REQ_NO + '</td>' +
                                                            '<td style="width:120px;>' + result.data[i].TPSD_JOB_NO + '</td>' +
                                                            '<td class="no-display">' + result.data[i].TPSD_LINE_NO + '</td>' +
                                                            '<td style="width:80px;">' + result.data[i].TPSD_ELEMENT_CD + '</td>' +
                                                            '<td style="width:140px;">' + result.data[i].TPSD_ELEMENT_DESC + '</td>' +
                                                            '<td class="right-align">' + addCommas(parseFloat(result.data[i].TPSD_REQ_AMT).toFixed(2)) + '</td>' +
                                                             '<td class="right-align">' + addCommas(parseFloat(result.data[i].TPSD_SETTLE_AMT).toFixed(2)) + '</td>' +
                                                            '<td>' + '' + '</td>' +
                                                            '<td>' + result.data[i].TPSD_REMARKS + '</td>' +
                                                            '<td>' + result.data[i].TPSD_VEC_TELE + '</td>' +
                                                            '<td style="width:30px;">' + '' + '</td>' +
                                                            '<td class="no-display"> ' + result.data[i].TPSD_SETLE_LINO_NO + '</td>' +
                                                            '</tr>');
                                                        }

                                                        jQuery("#TotReqAmt").val(addCommas(parseFloat(result.reqAmt).toFixed(2)));
                                                        jQuery("#TotSettleAmt").val(addCommas(parseFloat(result.setAmt).toFixed(2)));
                                                        balAmount = result.reqAmt - result.setAmt;
                                                        jQuery("#TxtBalanceAmount").val(addCommas(parseFloat(balAmount).toFixed(2)));

                                                        if (result.permission == true) {
                                                            jQuery('#btnRefund').show();
                                                            jQuery('#btnRefundAll').hide();
                                                            if (result.hasRefund == 1) {
                                                                jQuery('.btn-refund_cls').show();
                                                            }
                                                        }
                                                        else {
                                                            jQuery('#btnRefund').hide();
                                                            if (hasPermission == "True") {
                                                                jQuery('#btnRefundAll').show();
                                                                jQuery('.btn-refund_cls').hide();
                                                            }
                                                            else {
                                                                jQuery('#btnRefundAll').hide();
                                                            }
                                                        }
                                                    }
                                                    else {
                                                        
                                                        if (result.hasRefund == 1) {
                                                            jQuery('.btn-refund_cls').show();
                                                            jQuery('#btnRefundAll').hide();
                                                            setInfoMsg("Already refunded and can be canceled");
                                                        }
                                                        //else {
                                                        //    jQuery('#btnRefundAll').hide();
                                                        //    setInfoMsg("Not a refundable job no");
                                                        //}
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

                                    // --- End job wise settlement
                                }
                            }
                        }
                    }
                });
            }
        }
    }

    //dilshan 03/10/2018
    jQuery("#Bl_job_no1").focusout(function () {
        var code = jQuery(this).val();
        //alert(code);
        loadJobPendingRequest(code);
    });
    function loadJobPendingRequest(jobnum) {
        //alert("hfghfghfg");
        jQuery.ajax({
            type: "GET",
            url: "/PettyCashSettlement/loadJobPendingSettlementRequests",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: { fromdt: jQuery("#FromDt").val(), todt: jQuery("#ToDt").val(), jobno: jQuery("#Bl_job_no1").val() },
            success: function (result) {
                if (result.login == true) {
                    jQuery(".pendingreq-tbl tbody").empty();
                    if (result.success == true) {
                        if (result.data.length > 0) {
                            for (i = 0 ; i < result.data.length; i++) {
                                jQuery('.pendingreq-tbl tbody').append('<tr class="new-row">' +
                                '<td class="no-display">' + result.data[i].TPRH_SEQ + '</td>' +
                                '<td>' + result.data[i].TPRH_REQ_NO + '</td>' +
                                '<td>' + result.data[i].TPRH_MANUAL_REF + '</td>' +
                                '<td>' + result.data[i].TPRD_JOB_NO + '</td>' +
                                '<td>' + getFormatedDateInput(result.data[i].TPRH_REQ_DT) +
                                '<td style="width:30px;">' + "<input type='checkbox' class='select-pend-set'>" + '</td>' +
                                '</tr>');
                            }
                            jQuery(".select-pend-set").click(function (evt) {
                                var seq = jQuery(this).closest("tr").find('td:eq(0)').text();
                                var reqno = jQuery(this).closest("tr").find('td:eq(1)').text();
                                if (jQuery(this).is(':checked') == true) {
                                    loadRequestDataBySeq(seq, reqno, "ADD");
                                } else {
                                    loadRequestDataBySeq(seq, reqno, "REMOVE");
                                }

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
    
    loadPendingRequest();
    getMaxApproveLevel();
    jQuery(".load-pendingset").click(function (evt) {
        evt.preventDefault();
        var jobno = jQuery("#Bl_job_no1").val();
        if (jobno == "") {
            loadPendingRequest();
        }
    });
    jQuery("#SettlementNo").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            //var headerKeys = Array()
            //headerKeys = ["Row", "Settlement No", "Mannual Ref", "Settlement Date"];
            //field = "setlmentno";
            //var x = new CommonSearch(headerKeys, field);
            evt.preventDefault();
            var headerKeys = Array()
            headerKeys = ["Row", "Settlement No", "Mannual Ref", "Job No", "Settlement Date"];
            field = "setlmentno";
            var x = new CommonSearchDateFiltered(headerKeys, field);
        }
    });
    jQuery(".srch-settlement").click(function (evt) { // Added by Chathura on 30-sep-2017
        evt.preventDefault();
        var headerKeys = Array()
        headerKeys = ["Row", "Settlement No", "Mannual Ref", "Job No", "Settlement Date"];
        field = "setlmentno";
        var x = new CommonSearchDateFiltered(headerKeys, field);
    });
    jQuery("#SettlementNo").focusout(function () {
        var seq = jQuery("#SettlementSeq").val();
        var reqNo = jQuery(this).val();
        if (reqNo!="") {
            jQuery.ajax({
                type: "GET",
                url: "/PettyCashSettlement/getSetlementDetails?seq=" + seq + "&reqNo=" + reqNo,
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == false) {
                        Logout();
                    } else {
                        if (result.success == false) {
                            if (result.type == "Error") {
                                setError(result.msg);
                            }
                            if (result.type == "Info") {
                                setInfoMsg(result.msg);
                            }
                        }
                        if (result.success == true) {
                            jQuery(".btn-update-data").show();
                            jQuery(".save-form").hide();
                            jQuery("#SettlementNo").val(result.hdr.TPSH_SETTLE_NO);
                            jQuery("#SettlementSeq").val(result.hdr.TPSH_SEQ_NO);
                            jQuery("#SettlementDate").val(getFormatedDateInput(result.hdr.TPSH_SETTLE_DT));
                            jQuery("#ManRef").val(result.hdr.TPSH_MAN_REF);
                            //jQuery("#payDt").val(getFormatedDateInput(result.hdr.TPSH_PAY_DT));
                            jQuery("#ProfitCenter").val(result.hdr.TPSH_PC_CD);
                            jQuery("#Remarks").val(result.hdr.TPSH_REMARKS);
                            jQuery('.set-itmdet tbody').empty();
                            jQuery(".app-text").empty();
                            jQuery('#btnRefund').hide();
                            jQuery('.btn-update-data').removeAttr("disabled");
                            if (result.hdr.TPSH_APP1 == 1 && result.hdr.TPSH_APP2 == 0 && result.hdr.TPSH_APP3 == 0) {
                                jQuery(".app-text").html("Status : Approved by level 1");
                                if (result.userPermLvl == "1") {
                                    jQuery('.btn-update-data').attr("disabled", "true");
                                }
                                jQuery('#btnRefund').hide();
                            }
                            if (result.hdr.TPSH_APP1 == 1 && result.hdr.TPSH_APP2 == 1 && result.hdr.TPSH_APP3 == 0) {
                                jQuery(".app-text").html("Status : Approved by level 1 and 2");
                                if (result.userPermLvl == "2" || result.userPermLvl == "1") {
                                    jQuery('.btn-update-data').attr("disabled", "true");
                                }
                                jQuery('#btnRefund').hide();
                            }
                            if (result.hdr.TPSH_APP1 == 1 && result.hdr.TPSH_APP2 == 1 && result.hdr.TPSH_APP3 == 1) {
                                jQuery(".app-text").html("Status : Approved by level 1,2 and 3");
                                if ((result.userPermLvl == "1" || result.userPermLvl == "2" || result.userPermLvl == "3" )&& result.editable == false) {
                                    jQuery('.btn-update-data').attr("disabled", "true");
                                }
                                jQuery('#btnRefund').hide();
                                //jQuery('#btnRefund').show();
                            }
                            if (result.hdr.TPSH_APP1 == 0 && result.hdr.TPSH_APP2 == 0 && result.hdr.TPSH_APP3 == 0) {
                                jQuery(".app-text").html("Status : Pending");
                                jQuery('.btn-update-data').removeAttr("disabled");
                                jQuery('#btnRefund').hide();
                            }
                            if (result.itm.length > 0) {

                                for (i = 0 ; i < result.itm.length; i++) {
                                    if (result.itm[i].TPSD_ACT == 1) {
                                        var sel = "";
                                        var spcls = "";
                                        if (parseFloat(result.itm[i].TPSD_REQ_AMT) > 0) {
                                            sel = '<td class="select-rec"  style="width:40px;">' + "Select" + '</td>';
                                        } else {
                                            sel = '<td class="" style="width:40px;">' + "-" + '</td>'
                                            spcls = "special-colour setlement-row";
                                        }
                                        var sle = "No";
                                        if (result.itm[i].TPSD_ATT_RECEIPT == 1) {
                                            sle = "Yes";
                                        }
                                        var remove = "";
                                        if (parseFloat(result.itm[i].TPSD_SETTLE_AMT) > 0) {
                                            remove = "<button class='btn btn-sm-min btn-red-fullbg remove-list'><i class='fa fa-times' aria-hidden='true'></i></button>";
                                        } else {
                                            remove = "";
                                        }
                                        jQuery('.set-itmdet').append('<tr class="new-row ' + spcls + '">' +
                                         sel +
                                        '<td style="width:130px;">' + result.itm[i].TPSD_REQ_NO + '</td>' +
                                        '<td style="width:120px;">' + result.itm[i].TPSD_JOB_NO + '</td>' +
                                        '<td class="no-display">' + result.itm[i].TPSD_LINE_NO + '</td>' +
                                        '<td style="width:80px;">' + result.itm[i].TPSD_ELEMENT_CD + '</td>' +
                                        '<td style="width:140px;">' + result.itm[i].TPSD_ELEMENT_DESC + '</td>' +
                                        '<td class="right-align">' + addCommas(parseFloat(result.itm[i].TPSD_REQ_AMT).toFixed(2)) + '</td>' +
                                         '<td class="right-align">' + addCommas(parseFloat(result.itm[i].TPSD_SETTLE_AMT).toFixed(2)) + '</td>' +
                                        '<td>' + sle + '</td>' +
                                        '<td>' + result.itm[i].TPSD_REMARKS + '</td>' +
                                        '<td>' + result.itm[i].TPSD_VEC_TELE + '</td>' +
                                        '<td style="width:30px;">' + remove + '</td>' +
                                        '<td class="no-display"> ' + result.itm[i].TPSD_SETLE_LINO_NO + '</td>' +
                                        '</tr>');

                                        
                                    }
                                }
                              
                                jQuery(".remove-list").click(function (evt) {
                                    var td = jQuery(this).parent('td');
                                    var tr = jQuery(td).parent('tr');
                                   
                                    evt.preventDefault();
                                    var job = jQuery(this).closest("tr").find('td:eq(1)').text();
                                    var tp = jQuery(this);
                                    var seq = jQuery("#SettlementSeq").val();
                                    Lobibox.confirm({
                                        msg: "Do you want to remove settlement :" + job + " ?",
                                        callback: function ($this, type, ev) {
                                            if (type == "yes") {
                                                var lineno = jQuery(tp).closest("tr").find('td:eq(3)').text();
                                                var setlineno = jQuery(tp).closest("tr").find('td:eq(12)').text();
                                                if (lineno != "" && job != "") {
                                                    removeItem(lineno, job, seq,setlineno,tr);
                                                } else {
                                                    setInfoMsg("Invalid settlement number.");
                                                }

                                            }
                                        }
                                    });
                                });
                             
                                jQuery("#TotReqAmt").val(addCommas(parseFloat(result.reqAmt).toFixed(2)));
                                jQuery("#TotSettleAmt").val(addCommas(parseFloat(result.setAmt).toFixed(2)));
                                balAmount = result.reqAmt - result.setAmt;
                                jQuery("#TxtBalanceAmount").val(addCommas(parseFloat(balAmount).toFixed(2)));
                                jQuery(".select-rec").click(function () {
                                    var reqno = jQuery(this).closest("tr").find('td:eq(1)').text();
                                    var jobno = jQuery(this).closest("tr").find('td:eq(2)').text();
                                    var lineno = jQuery(this).closest("tr").find('td:eq(3)').text();
                                    var reqAmt = jQuery(this).closest("tr").find('td:eq(6)').text();

                                    setJobData(reqno, jobno, lineno, reqAmt);
                                });

                            }
                            if (result.editable == true) {
                                jQuery("table.set-itmdet tr.setlement-row").dblclick(function (evt) {
                                    var tr = jQuery(this);
                                    evt.preventDefault();
                                    var job = jQuery(this).closest("tr").find('td:eq(2)').text();
                                    var set = jQuery(this).closest("tr").find('td:eq(1)').text();
                                    var tp = jQuery(this);
                                    var seq = jQuery("#SettlementSeq").val();
                                    Lobibox.confirm({
                                        msg: "Do you want to update settlement no: " + set + " job no :" + job + " ?",
                                        callback: function ($this, type, ev) {
                                            if (type == "yes") {
                                              
                                                var lineno = jQuery(tp).closest("tr").find('td:eq(3)').text();
                                                var setlineno = jQuery(tp).closest("tr").find('td:eq(12)').text();
                                                if (lineno != "" && set != "") {
                                                    jQuery("#Request").val(jQuery(tp).closest("tr").find('td:eq(1)').text());
                                                    jQuery("#JobNo").val(jQuery(tp).closest("tr").find('td:eq(2)').text());
                                                    jQuery("#CstEle").val(jQuery(tp).closest("tr").find('td:eq(4)').text());
                                                    jQuery("#CstEleDesc").val(jQuery(tp).closest("tr").find('td:eq(5)').text());
                                                    jQuery("#ReqAmt").val(jQuery("#TotReqAmt").val().replace(",",""));
                                                    jQuery("#SetAmt").val(jQuery(tp).closest("tr").find('td:eq(7)').text().replace(",", ""));
                                                    var vv = 0;
                                                    if (jQuery(tp).closest("tr").find('td:eq(8)').text() == "Yes") {
                                                        vv = 1;
                                                    }
                                                    jQuery("#ReceiptAttr").val(vv);
                                                    jQuery("#jobRmk").val(jQuery(tp).closest("tr").find('td:eq(9)').text());
                                                    jQuery("#VehLcTel").val(jQuery(tp).closest("tr").find('td:eq(10)').text());
                                                    jQuery("#LineNo").val(jQuery(tp).closest("tr").find('td:eq(3)').text());
                                                    
                                                    removeItem(lineno, set, seq, setlineno, tr);
                                                    $(tr).remove();
                                                } else {
                                                    setInfoMsg("Invalid settlement number.");
                                                }

                                            }
                                        }
                                    });
                                });
                            } else {
                                jQuery("table.set-itmdet tr.setlement-row").dblclick(function (evt) {
                                    setInfoMsg(result.pemissionMsg);
                                });
                            }

                        }
                    }
                }
            });
        }
    });
    jQuery(".add-setlement").click(function (evt) {
        evt.preventDefault();
        jQuery(".add-setlement").attr('disabled', 'disabled');
        var req = jQuery("#Request").val();
        var jobno=jQuery("#JobNo").val();
        var lineno = jQuery("#LineNo").val();
        var cstEle = jQuery("#CstEle").val();
        var setAmt = jQuery("#SetAmt").val();
        var recAttr = jQuery("#ReceiptAttr").val();
        var rmk = jQuery("#jobRmk").val();
        var veh = jQuery("#VehLcTel").val();
        var settleNo = jQuery("#SettlementNo").val();
        if (req != "") {
            if (jobno != "") {
                if (cstEle != "") {
                    if (setAmt != "") {
                        if (validNumber(setAmt) && Number(setAmt)>0) {
                            if (recAttr != "") {
                                jQuery.ajax({
                                    type: "GET",
                                    url: "/PettyCashSettlement/addSettleDetails",
                                    contentType: "application/json;charset=utf-8",
                                    data: { req: req, jobno: jobno, lineno: lineno, cstEle: cstEle, setAmt: setAmt, recAttr: recAttr, rmk: rmk, veh: veh, settleNo: settleNo },
                                    dataType: "json",
                                    success: function (result) {
                                        if (result.login == true) {
                                            if (result.success == true) {
                                                jQuery("table.set-itmdet tbody").empty();
                                                if (result.data.length > 0) {
                                                    for (i = 0 ; i < result.data.length; i++) {
                                                        if (result.data[i].TPSD_ACT == 1) {
                                                            var sle = "No";
                                                            if (result.data[i].TPSD_ATT_RECEIPT == 1) {
                                                                sle = "Yes";
                                                            }

                                                            var sel = "";
                                                            var spcls = "";
                                                            if (parseFloat(result.data[i].TPSD_REQ_AMT) > 0) {
                                                                sel = '<td class="select-rec"  style="width:40px;">' + "Select" + '</td>';
                                                            } else {
                                                                sel = '<td class=""  style="width:40px;">' + "-" + '</td>'
                                                                spcls = "special-colour setlement-row";
                                                            }
                                                            var remove = "";
                                                            if (parseFloat(result.data[i].TPSD_SETTLE_AMT) > 0) {
                                                                remove = "<button class='btn btn-sm-min btn-red-fullbg remove-list'><i class='fa fa-times' aria-hidden='true'></i></button>";
                                                            } else {
                                                                remove = "";
                                                            }
                                                            jQuery('.set-itmdet').append('<tr class="new-row ' + spcls + '">' +
                                                            sel +
                                                            '<td  style="width:130px;">' + result.data[i].TPSD_REQ_NO + '</td>' +
                                                            '<td style="width:120px;">' + result.data[i].TPSD_JOB_NO + '</td>' +
                                                            '<td class="no-display">' + result.data[i].TPSD_LINE_NO + '</td>' +
                                                            '<td style="width:80px;">' + result.data[i].TPSD_ELEMENT_CD + '</td>' +
                                                            '<td style="width:140px;">' + result.data[i].TPSD_ELEMENT_DESC + '</td>' +
                                                            '<td class="right-align">' + addCommas(parseFloat(result.data[i].TPSD_REQ_AMT).toFixed(2)) + '</td>' +
                                                             '<td class="right-align">' + addCommas(parseFloat(result.data[i].TPSD_SETTLE_AMT).toFixed(2)) + '</td>' +
                                                            '<td>' + sle + '</td>' +
                                                            '<td>' + result.data[i].TPSD_REMARKS + '</td>' +
                                                            '<td>' + result.data[i].TPSD_VEC_TELE + '</td>' +

                                                             '<td style="width:30px;">' + remove + '</td>' +
                                                              '<td class="no-display">' + result.data[i].TPSD_SETLE_LINO_NO + '</td>' +
                                                            '</tr>');

                                                            $('#CstEle').focus();
                                                        }
                                                        
                                                    }
                                                    jQuery(".remove-list").click(function (evt) {
                                                        var td = jQuery(this).parent('td');
                                                        var tr = jQuery(td).parent('tr');
                                                     
                                                        evt.preventDefault();
                                                        var job = jQuery(this).closest("tr").find('td:eq(1)').text();
                                                        var tp = jQuery(this);
                                                        var seq = jQuery("#SettlementSeq").val();
                                                        Lobibox.confirm({
                                                            msg: "Do you want to remove settlement :" + job + " ?",
                                                            callback: function ($this, type, ev) {
                                                                if (type == "yes") {
                                                                    var lineno = jQuery(tp).closest("tr").find('td:eq(3)').text();
                                                                    var setlineno = jQuery(tp).closest("tr").find('td:eq(12)').text();
                                                                    if (lineno != "" && job != "") {
                                                                        removeItem(lineno, job, seq,setlineno,tr);
                                                                    } else {
                                                                        setInfoMsg("Invalid settlement number.");
                                                                    }

                                                                }
                                                            }
                                                        });

                                                    });
                                                    jQuery("#TotReqAmt").val(addCommas(parseFloat(result.TotReqAmt).toFixed(2)));
                                                    jQuery("#TotSettleAmt").val(addCommas(parseFloat(result.TotSetAmt).toFixed(2)));
                                                    balAmount = result.TotReqAmt - result.TotSetAmt;
                                                    jQuery("#TxtBalanceAmount").val(addCommas(parseFloat(balAmount).toFixed(2)));
                                                    clearJobData();
                                                    jQuery(".select-rec").click(function () {
                                                        var reqno = jQuery(this).closest("tr").find('td:eq(1)').text();
                                                        var jobno = jQuery(this).closest("tr").find('td:eq(2)').text();
                                                        var lineno = jQuery(this).closest("tr").find('td:eq(3)').text();
                                                        var reqAmt = jQuery(this).closest("tr").find('td:eq(6)').text();

                                                        setJobData(reqno, jobno, lineno, reqAmt);
                                                    });
                                                }

                                            } else {
                                                if (result.type == "Info") {
                                                    setInfoMsg(result.msg);
                                                }
                                                if (result.type == "Error") {
                                                    setError(result.msg);
                                                }
                                            }
                                        } else {
                                            Logout();
                                        }
                                    }
                                });


                            } else {
                                setInfoMsg("Please select recipt att.");
                            }
                        } else {
                            setInfoMsg("Please enter valid settle amount.");
                        }
                    } else {
                        setInfoMsg("Please add settle amount.");
                    }
                } else {
                    setInfoMsg("Please select cost element.");
                }
            } else {
                setInfoMsg("Please select job number.");
            }
        } else {
            setInfoMsg("Please select request number.");
        }
        jQuery(".add-setlement").removeAttr('disabled');
    });
    jQuery(".clear-form").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to clear data ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    clearForm();

                }
            }
        });

    });

    jQuery(".save-form,.btn-update-data").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to save ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    var formdata = jQuery("#pettcashset-frm").serialize();
                    jQuery.ajax({
                        type: "POST",
                        url: "/PettyCashSettlement/saveSettlmentDet",
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
                                        jQuery('.btn-update-data').attr("disabled", "fale");
                                    }
                                } else {
                                    if (result.type == "Error") {
                                        setError(result.msg);
                                        jQuery('.btn-update-data').attr("disabled", "fale");
                                    }
                                    if (result.type == "Info") {
                                        setInfoMsg(result.msg);
                                        jQuery('.btn-update-data').attr("disabled", "fale");
                                    }
                                }

                            }
                        }
                    });
                }
            }
        });

    });
    jQuery("#btnApp1").click(function (evt) {
        evt.preventDefault();
        reqno = jQuery("#SettlementNo").val();
        appLvl = "1";
        paydate = jQuery("#payDt").val();
        Lobibox.confirm({
            msg: "Do you want approve Level 1?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    jQuery.ajax({
                        type: "POST",
                        url: "/PettyCashSettlement/approveRequest",
                        dataType: "json",
                        data: { reqno: reqno,  appLvl: appLvl, paydate: paydate },
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
        reqno = jQuery("#SettlementNo").val();
        appLvl = "2";
        paydate = jQuery("#payDt").val();
        Lobibox.confirm({
            msg: "Do you want approve Level 2?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    jQuery.ajax({
                        type: "POST",
                        url: "/PettyCashSettlement/approveRequest",
                        dataType: "json",
                        data: { reqno: reqno, appLvl: appLvl, paydate: paydate },
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
        reqno = jQuery("#SettlementNo").val();
        appLvl = "3";
        paydate = jQuery("#payDt").val();
        Lobibox.confirm({
            msg: "Do you want approve Level 3?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    jQuery.ajax({
                        type: "POST",
                        url: "/PettyCashSettlement/approveRequest",
                        dataType: "json",
                        data: { reqno: reqno, appLvl: appLvl, paydate: paydate },
                        success: function (result) {
                            if (result.login == false) {
                                Logout();
                            } else {
                                if (result.success == true) {
                                    if (hasPermission == "True") {
                                        jQuery('#btnRefundAll').show();
                                    }
                                    else {
                                        jQuery('#btnRefundAll').hide();
                                    }
                                    if (result.type == "Success") {
                                        setSuccesssMsg(result.msg);
                                        clearForm();
                                    }
                                } else {
                                    jQuery('#btnRefund').hide();
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
    jQuery(".reject-btn").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to reject request ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    if (jQuery("#SettlementNo").val() != "") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/PettyCashSettlement/rejectRequest?reqno=" + jQuery("#SettlementNo").val(),
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == false) {
                                    Logout();
                                } else {
                                    if (result.success == true) {
                                        jQuery('#btnRefund').hide();
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
                        setInfoMsg("Please select a request to be rejected .");
                    }
                }
            }
        });

    });

    jQuery("#btnRefundAll").click(function (evt) { // Refund All
              
        jQuery.ajax({
            type: "GET",
            url: "/PettyCashSettlement/validateHasFullRefundExists",
            data: { reqno: jQuery('#Bl_job_no').val() },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {

                if (result.data == true) {
                  
                    //setInfoMsg("Refund details of Request No " + result.reqNo + " already added");
                    setInfoMsg("Some Request numbers already added");
                    return;
                }

                jQuery.ajax({
                    type: "GET",
                    url: "/PettyCashSettlement/ShowRefundAmounts",
                    data: null,
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        jQuery('#paysettle').show();
                        loadPayModesTypes("REFUND");
                        jQuery('#btnRefundSave').show();
                        jQuery('#btnRefund').hide();
                        jQuery('#refunddetails').show();
                        //jQuery('#btnRefundAll').hide();

                        jQuery('#Sar_debtor_cd').focus();
                        
                        for (i = 0; i < result.data.length; i++) {
                            jQuery('table.refund-table').append('<tr class="new-row">' +
                                    '<td>' + 'No' + '</td>' +
                                    '<td>' + result.data[i].TPSD_REQ_NO + '</td>' +
                                   '<td>' + result.data[i].TPSD_JOB_NO + '</td>' +
                                   '<td>' + addCommas(parseFloat(result.data[i].TPSD_SETTLE_AMT).toFixed(2)) + '</td>' +
                                   '<td style="width:30px;">' + "<input type='checkbox' disabled class='select-refund-amnt'>" + '</td>' +
                                   '</tr>');
                        }

                        jQuery.ajax({
                            type: "GET",
                            url: "/PettyCashSettlement/CollectAllRefundData",
                            data: { remove: false },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                              
                                refundAmount = result.data;
                                jQuery('#Sird_settle_amt').val(addCommas(parseFloat(refundAmount).toFixed(2)));
                                jQuery(".bal-amount-val").html(addCommas(parseFloat(refundAmount).toFixed(2)));
                                jQuery(".tot-amount-val").html(addCommas(parseFloat(refundAmount).toFixed(2)));
                            }
                        });

                    }
                });
            }
        });
    });

    jQuery("#btn-refund_cls").click(function (evt) { // Refund Cancel
        
        Lobibox.confirm({
            msg: "Do you want to cancel refunded details?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    var check = "";
                    jQuery.ajax({
                        type: "GET",
                        url: "/PettyCashSettlement/CancelRefund",
                        data: { jobno: jQuery("#Bl_job_no").val() },
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {

                            clearAllValues();
                            jQuery('#paysettle').hide();
                            jQuery('.btn-refund_cls').hide();
                            if (hasPermission == "True") {
                                jQuery('#btnRefundAll').show();
                            }
                            else {
                                jQuery('#btnRefundAll').hide();
                            }
                            refundAmount = 0.00;
                            clearForm();
                            clearPaymentDetails();

                            if (result.login == true) {
                                if (result.success == true) {
                                    setSuccesssMsg("Sucessfully Cancelled");
                                    //clearpage();
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
    
    jQuery("#btnRefundSave").click(function (evt) {
        evt.preventDefault();
        if (jQuery("#Sar_debtor_cd").val() == "" || jQuery("#Sar_debtor_cd").val() == null) {
            setInfoMsg("Debtor code cannot be balnk");
            return;
        }
        if (jQuery("#ManRefRef").val() == "" || jQuery("#ManRefRef").val() == null) {
            setInfoMsg("Manual Refund Ref cannot be balnk");
            return;
        }

        if ($('.payment-table tr').length < 2) {
            
            setInfoMsg("Please add payment details");
            return;
        }

        Lobibox.confirm({
            msg: "Do you want to save refund?",
            callback: function ($this, type, ev) {
                if (type == "yes") {

                    jQuery.ajax({
                        type: "GET",
                        url: "/PettyCashSettlement/SaveRecieptEntryRefund",
                        data: { jobno: jQuery("#Bl_job_no").val(), depBank: jQuery("#Deposit_bank_cd").val(), depBranch: jQuery("#Deposit_branch_cd").val(), debtor: jQuery("#Sar_debtor_cd").val(), manRef: jQuery("#ManRefRef").val() },
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {

                            var recieptnumber = result.receiptNo;
                           
                            var formdata = jQuery("#pettcashset-frm").serialize();
                           
                            jQuery.ajax({
                                type: "POST",
                                url: "/PettyCashSettlement/saveSettlmentDetRefund",
                                data: formdata + "&receiptNo=" + result.receiptNo,
                                dataType: "json",
                                success: function (result) {
                                 
                                    if (result.login == false) {
                                        Logout();
                                    } else {
                                        if (result.success == true) {
                                          
                                            clearAllValues();
                                            jQuery('#paysettle').hide();
                                            jQuery('.btn-refund_cls').hide();
                                            if (hasPermission == "True") {
                                                jQuery('#btnRefundAll').show();
                                            }
                                            else {
                                                jQuery('#btnRefundAll').hide();
                                            }
                                            refundAmount = 0.00;
                                            clearForm();
                                            clearPaymentDetails();

                                            if (result.login == true) {
                                                if (result.success == true) {
                                                   
                                                    setSuccesssMsg("Successfully Saved : " + recieptnumber);
                                                    window.open("/RecieptEntry/PrintReceipt?ReceiptNo=" + recieptnumber, "_blank");
                                                    
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
                    });
                }
            }
        });

    });

    

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
                            setInfoMsg("Invalid Tel/LC/ Veh.");
                            jQuery("#VehLcTel").val("");
                            jQuery("#VehLcTel").focus();
                        }
                    }
                }
            }
        });
    }
}
function loadPendingRequest() {
   
    jQuery.ajax({
        type: "GET",
        url: "/PettyCashSettlement/loadPendingSettlementRequests",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: { fromdt: jQuery("#FromDt").val(), todt: jQuery("#ToDt").val(), jobno: jQuery("#Bl_job_no").val() },
        success: function (result) {
            if (result.login == true) {
                jQuery(".pendingreq-tbl tbody").empty();
                if (result.success == true) {
                    if (result.data.length > 0) {
                        for (i = 0 ; i < result.data.length; i++) {
                            jQuery('.pendingreq-tbl tbody').append('<tr class="new-row">' +
                            '<td class="no-display">' + result.data[i].TPRH_SEQ + '</td>' +
                            '<td>' + result.data[i].TPRH_REQ_NO + '</td>' +
                            '<td>' + result.data[i].TPRH_MANUAL_REF + '</td>' +
                            '<td>' + result.data[i].TPRD_JOB_NO + '</td>' +
                            '<td>' + getFormatedDateInput(result.data[i].TPRH_REQ_DT) +
                            '<td style="width:30px;">' + "<input type='checkbox' class='select-pend-set'>"+ '</td>' +
                            '</tr>');
                        }
                        jQuery(".select-pend-set").click(function (evt) {
                            var seq = jQuery(this).closest("tr").find('td:eq(0)').text();
                            var reqno = jQuery(this).closest("tr").find('td:eq(1)').text();
                            if (jQuery(this).is(':checked') == true) {
                                loadRequestDataBySeq(seq, reqno,"ADD");
                            } else {
                                loadRequestDataBySeq(seq, reqno,"REMOVE");
                            }
                            
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
function loadRequestDataBySeq(seq, reqno,type) {
    if (seq != "" && reqno != "") {
        var url;
        if (type == "REMOVE") {
            url = "/PettyCashSettlement/removeRequstDetails";
        } else {
            url="/PettyCashSettlement/loadRequstDetails";
        }
        jQuery.ajax({
            type: "GET",
            url: url,
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: { seq: seq, reqno: reqno },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        jQuery("table.set-itmdet tbody").empty();
                        if (result.data.length > 0) {
                           
                            for (i = 0 ; i < result.data.length; i++) {
                                if (result.data[i].TPSD_ACT == 1) {
                                    var sel = "";
                                    var spcls = "";
                                    if (parseFloat(result.data[i].TPSD_REQ_AMT) > 0) {
                                        sel = '<td class="select-rec" style="width:40px;">' + "Select" + '</td>';
                                    } else {
                                        sel = '<td class="" style="width:40px;">' + "-" + '</td>'
                                        spcls = "special-colour setlement-row";
                                    }
                                    jQuery('.set-itmdet').append('<tr class="new-row ' + spcls + '">' +
                                     sel +
                                    '<td style="width:130px;">' + result.data[i].TPSD_REQ_NO + '</td>' +
                                    '<td style="width:120px;">' + result.data[i].TPSD_JOB_NO + '</td>' +
                                    '<td class="no-display">' + result.data[i].TPSD_LINE_NO + '</td>' +
                                    '<td style="width:80px;">' + result.data[i].TPSD_ELEMENT_CD + '</td>' +
                                    '<td style="width:140px;">' + result.data[i].TPSD_ELEMENT_DESC + '</td>' +
                                    '<td class="right-align">' + addCommas(parseFloat(result.data[i].TPSD_REQ_AMT).toFixed(2)) + '</td>' +
                                     '<td class="right-align">' + "0.00" + '</td>' +
                                    '<td>' + "False" + '</td>' +
                                    '<td>' + result.data[i].TPSD_REMARKS + '</td>' +
                                    '<td>' + result.data[i].TPSD_VEC_TELE + '</td>' +
                                    '<td class="no-display">' + result.data[i].TPSD_SETLE_LINO_NO + '</td>' +
                                    '<td style="width:30px;"></td>' +
                                    '</tr>');
                                }
                            }
                            jQuery("#TotReqAmt").val(addCommas(parseFloat(result.reqAmt).toFixed(2)));
                            jQuery(".select-rec").click(function () {
                                var reqno = jQuery(this).closest("tr").find('td:eq(1)').text();
                                var jobno = jQuery(this).closest("tr").find('td:eq(2)').text();
                                var lineno = jQuery(this).closest("tr").find('td:eq(3)').text();
                                var reqAmt = jQuery(this).closest("tr").find('td:eq(6)').text();

                                setJobData(reqno, jobno, lineno, reqAmt);
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
    } else {
        setInfoMsg("Invalid request number.");
    }
}
function clearSession() {
    jQuery.ajax({
        type: "GET",
        url: "/PettyCashSettlement/clearSession",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.login == false) {
                Logout();
            }
        }
    });
}
function setJobData(reqno, jobno, lineno, reqAmt) {
    if (reqno != "" && jobno != "" && lineno != "") {
        jQuery.ajax({
            type: "GET",
            url: "/PettyCashSettlement/addSettlement",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: { reqno: reqno, jobno: jobno,lineno : lineno },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        jQuery("#Request").val(reqno);
                        jQuery("#JobNo").val(jobno);
                        jQuery("#LineNo").val(lineno);
                        jQuery("#ReqAmt").val(addCommas(parseFloat(result.reqAmt).toFixed(2)));
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
        setInfoMsg("Invalid request details.")
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
                        jQuery("#CstEle").focus();
                    }
                    if (result.success == true) {
                        if (result.data.MCE_CD == null) {
                            setInfoMsg("Please enter valid cost element code.");
                            jQuery("#CstEle").val("");
                            jQuery("#CstEle").focus();
                        } else {
                            jQuery("#CstEleDesc").val(result.data.MCE_DESC);
                        }
                    }
                }
            }
        });
    }
}
function clearJobData() {
    //jQuery("#Request").val("");
    //jQuery("#JobNo").val("");
    //jQuery("#LineNo").val("");
    jQuery("#CstEle").val("");
    jQuery("#CstEleDesc").val("");
    //jQuery("#ReqAmt").val("");
    jQuery("#SetAmt").val("");
    jQuery("#ReceiptAttr").val("");
    jQuery("#jobRmk").val("");
    jQuery("#VehLcTel").val("");
}

function clearForm() {

    hasPermission = $("#permissonSession").data('value');

    jQuery(".btn-update-data").hide();
    jQuery(".save-form").show();
    document.getElementById("pettcashset-frm").reset();
    jQuery(".set-itmdet tbody").empty();
    jQuery(".app-text").empty();
    jQuery("#SettlementDate").val(getFormatedDate(new Date()));
    jQuery("#payDt").val(getFormatedDate(new Date()));
    jQuery("#FromDt").val(getFormatedPreMonth(new Date()));
    jQuery("#ToDt").val(getFormatedDate(new Date()));
    jQuery(".pc-desc").empty();
    pcfocusout(jQuery("#ProfitCenter").val());
    jQuery("#TotReqAmt").val("0.00");
    jQuery("#TotSettleAmt").val("0.00");
    jQuery('.btn-update-data').attr("disabled", "true");

    jQuery('#btnRefund').hide();
    jQuery('#btnRefundSave').hide();
    jQuery('#refunddetails').hide();
    jQuery('.btn-refund_cls').hide();
    if (hasPermission == "True") {
        jQuery('#btnRefundAll').show();
    }
    else {
        jQuery('#btnRefundAll').hide();
    }

    jQuery('#Sar_debtor_cd').val("");
    jQuery('#ManRefRef').val("");
    jQuery(".bal-amount-val").empty();
    jQuery(".tot-paid-amount-val").empty();
    jQuery(".tot-paid-amount-val").html("");
    jQuery(".bal-amount-val").html("");

    jQuery(".refund-table tbody").empty();

    clearAllValues();
    jQuery('#paysettle').hide();
    refundAmount = 0.00;

    clearPaymentDetails();
    loadPendingRequest();    
    clearSession();
}

function clearPaymentDetails() {

    refundAmount = 0.00;

    jQuery('#Sird_settle_amt').val("");
    jQuery('#Deposit_bank_cd').val("");
    jQuery('#Deposit_branch_cd').val("");

    jQuery(".payment-table tbody").empty();
}

function getMaxApproveLevel() {
    jQuery.ajax({
        type: "GET",
        url: "/PettyCashSettlement/getMaxApproveLevel",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.login == true) {
                if (result.success == true) {
                    if (result.log_autho == "1") {
                        jQuery("#btnApp1").removeAttr('disabled');
                        jQuery("#btnApp2").attr('disabled', 'disabled');
                        jQuery("#btnApp3").attr('disabled', 'disabled');
                        jQuery("#btnApp2").removeClass("hvr-sweep-to-left hvr-sweep-to-left-ash btn-ash");
                        jQuery("#btnApp3").removeClass("hvr-sweep-to-left hvr-sweep-to-left-green btn-green");
                    } else if (result.log_autho == "2") {
                        jQuery("#btnApp2").removeAttr('disabled');
                        jQuery("#btnApp1").attr('disabled', 'disabled');
                        jQuery("#btnApp3").attr('disabled', 'disabled');
                        jQuery("#btnApp1").removeClass("hvr-sweep-to-left hvr-sweep-to-left-yellow btn-yellow");
                        jQuery("#btnApp3").removeClass("hvr-sweep-to-left hvr-sweep-to-left-green btn-green");
                    } else if (result.log_autho == "3") {
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
jQuery(".print-job-btn").click(function (evt) {
    evt.preventDefault();
    Lobibox.confirm({
        msg: "Do you want to print request ?",
        callback: function ($this, type, ev) {
            if (type == "yes") {
                if (jQuery("#SettlementNo").val() != "" && jQuery("#SettlementSeq").val() != "") {
                    jQuery.ajax({
                        type: "GET",
                        url: "/PettyCashSettlement/validatePrint?seq=" + jQuery("#SettlementSeq").val(),
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            if (result.login == false) {
                                Logout();
                            } else {
                                if (result.success == true) {
                                    window.open("/PettyCashSettlement/Print?seq=" + jQuery("#SettlementSeq").val(), "_blank");
                                    window.open("/PettyCashSettlement/Settltment_Summary?seq=" + jQuery("#SettlementSeq").val(), "_blank");
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

function checkPermission() {
 
    var permission = false;

    jQuery.ajax({
        type: "GET",
        url: "/PettyCashSettlement/CheckPermissionForRefund",
        data: {status:"permission"},
        contentType: "application/json",
        dataType: "json",
        success: function (result) {
           
            hasPermission = result.data;
           
        }
    });

}

function removeItem(itmline, jobnum, seq, setlineno, _tr) {
    var td = jQuery(this).parent('td');
    var tr = jQuery(td).parent('tr');

    jQuery.ajax({
        type: "GET",
        url: "/PettyCashSettlement/removeSettlement?itmline=" + itmline + "&jobnum=" + jobnum + "&seq=" + seq + "&setlineno=" + setlineno,
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
                            //jQuery('.set-itmdet tbody').empty();
                            if (result.type == "Error") {
                                setInfoMsg(result.msg);
                            }
                            else {
                                $(_tr).remove();
                            }
                            
                            //if (result.data.length > 0) {

                                jQuery(".remove-list").unbind('click').click(function (evt) {
                                    var td = jQuery(this).parent('td');
                                    var tr = jQuery(td).parent('tr');
                                    evt.preventDefault();
                                    var job = jQuery(this).closest("tr").find('td:eq(1)').text();
                                    var tp = jQuery(this);
                                    var seq = jQuery("#SettlementSeq").val();
                                    Lobibox.confirm({
                                        msg: "Do you want to remove settlement :" + job + " ?",
                                        callback: function ($this, type, ev) {
                                            if (type == "yes") {
                                                var lineno = jQuery(tp).closest("tr").find('td:eq(3)').text();
                                                var setlineno = jQuery(tp).closest("tr").find('td:eq(12)').text();
                                                if (lineno != "" && job != "") {
                                                    removeItem(lineno, job, seq, setlineno, tr);
                                                    $(tr).remove();
                                                } else {
                                                    setInfoMsg("Invalid settlement number.");
                                                }
                                            }
                                        }
                                    });
                                });
                                jQuery("#TotReqAmt").val(addCommas(parseFloat(result.TotReqAmt).toFixed(2)));
                                jQuery("#TotSettleAmt").val(addCommas(parseFloat(result.TotSetAmt).toFixed(2)));
                                jQuery("#TxtBalanceAmount").val(addCommas((parseFloat(result.TotReqAmt) - parseFloat(result.TotSetAmt)).toFixed(2)));
                                jQuery(".select-rec").click(function () {
                                    var reqno = jQuery(this).closest("tr").find('td:eq(1)').text();
                                    var jobno = jQuery(this).closest("tr").find('td:eq(2)').text();
                                    var lineno = jQuery(this).closest("tr").find('td:eq(3)').text();
                                    var reqAmt = jQuery(this).closest("tr").find('td:eq(6)').text();

                                    setJobData(reqno, jobno, lineno, reqAmt);
                                });
                            //}
                        }
                    }
                }
            }
        }
    });
}

jQuery("#SettlementDate").focusout(function () {
    var code = jQuery(this).val();
    if (code == "") {
        jQuery('#SettlementDate').val(my_date_format(new Date()));
    }
});
jQuery("#payDt").focusout(function () {
    var code = jQuery(this).val();
    if (code == "") {
        jQuery('#payDt').val(my_date_format(new Date()));
    }
});
jQuery("#FromDt").focusout(function () {
    var code = jQuery(this).val();
    if (code == "") {
        jQuery('#FromDt').val(my_date_format(new Date()));
    }
});
jQuery("#ToDt").focusout(function () {
    var code = jQuery(this).val();
    if (code == "") {
        jQuery('#ToDt').val(my_date_format(new Date()));
    }
});