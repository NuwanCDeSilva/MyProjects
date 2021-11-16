jQuery(document).ready(function () {

    var total_cost = 0;
    var total_rev_amount = 0;
    var total_diff = 0;
    var job_status = "N";

    jQuery('#date').val(my_date_format(new Date()));
    jQuery(".btn-pouch-no").click(function () {
        //var headerKeys = Array()
        //headerKeys = ["Row", "Code"];
        //field = "pouchnojob2"
        //var x = new CommonSearch(headerKeys, field);
        var headerKeys = Array()
        headerKeys = ["Row", "Pouch No", "Job No", "Date"];
        field = "pouchnojob"
        var x = new CommonSearchDateFiltered(headerKeys, field);
    });
    jQuery(".btn-job-no").click(function () {
        //var headerKeys = Array()
        //headerKeys = ["Row", "Job No"];
        //field = "purjobno"
        //data = jQuery("#pouchno").val();
        //var x = new CommonSearch(headerKeys, field, data);
        var headerKeys = Array()
        headerKeys = ["Row", "Job No", "Pouch No", "Date", "Status"];
        field = "jobnoblclose1"
        var x = new CommonSearchDateFiltered(headerKeys, field);
    });
    jQuery(".btn-job-no").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Job No", "Pouch No", "Date", "Status"];
            field = "jobnoblclose1"
            var x = new CommonSearchDateFiltered(headerKeys, field);
        }
    });
    jQuery("#jobno").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Job No", "Pouch No", "Date", "Status"];
            field = "jobnoblclose1"
            var x = new CommonSearchDateFiltered(headerKeys, field);
        }
    });
    jQuery("#pouchno").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Pouch No", "Job No", "Date"];
            field = "pouchnojob"
            var x = new CommonSearchDateFiltered(headerKeys, field);
        }
    });
    jQuery('.btn-view').click(function (e) {

        if (jQuery("#jobno").val() == "") {
            setInfoMsg("Please Select Job");
            return;
        }

        Lobibox.confirm({
            msg: "Do you want to view data?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    var check = "";
                    jQuery.ajax({
                        type: "GET",
                        url: "/ManualJobClosure/LoadJobCostinData",
                        data: { JobNo: jQuery("#jobno").val() },
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    //set cost grid
                                    setcostdata(result.costData);
                                    setrevndata(result.actualCostData);

                                    jQuery("#costdiff").val(ReplaceNumberWithCommas(Number(result.costdiff).toFixed(2)));

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

    jQuery('.btn-print').click(function (e) {

        if (jQuery("#jobno").val() == "") {
            setInfoMsg("Please Select Job");
            debugger;
            return;
        }

        Lobibox.confirm({
            msg: "Do you want to print data?",
            callback: function ($this, type, ev) {
                if (type == "yes") {

                    window.open(
                                          "/ManualJobClosure/RequestPrint?JobNo=" + jQuery("#jobno").val(),
                                          '_blank' // <- This is what makes it open in a new window.
                                         );
                    //var check = "";
                    //jQuery.ajax({
                    //    type: "GET",
                    //    url: "/ManualJobClosure/RequestPrint",
                    //    data: { JobNo: jQuery("#jobno").val() },
                    //    contentType: "application/json;charset=utf-8",
                    //    dataType: "json",
                    //    success: function (result) {
                    //        if (result.login == true) {
                    //            if (result.success == true) {

                    //                window.open(
                    //                      "/ManualJobClosure/RequestPrint?JobNo=" + jQuery("#jobno").val(),
                    //                      '_blank' // <- This is what makes it open in a new window.
                    //                       );
                    //                ////set cost grid
                    //                //setcostdata(result.costData);
                    //                //setrevndata(result.actualCostData);

                    //                //jQuery("#costdiff").val(ReplaceNumberWithCommas(Number(result.costdiff).toFixed(2)));

                    //            } else {
                    //                if (result.Type == "Error") {
                    //                    setError(result.msg);

                    //                }
                    //                if (result.Type == "Info") {
                    //                    setInfoMsg(result.msg);
                    //                }
                    //            }
                    //        } else {
                    //            Logout();
                    //        }
                    //    }
                    //});
                }
            }

        });
    });


    jQuery("#pouchno").focusout(function () {
        var pouchNo = jQuery('#pouchno').val();
        if (pouchNo != "" && pouchno != null) {
            JobOrPouchDetails(pouchNo, 'PCH');
        }
    });
    jQuery("#jobno").focusout(function () {
        var jobno = jQuery('#jobno').val();
        if (jobno != "" && jobno != null) {
            JobOrPouchDetails(jobno, 'JOB');
        }
    });
    jQuery("#date").focusout(function () {
        var code = jQuery(this).val();
        if (code == "") {
            jQuery('#date').val(my_date_format(new Date()));
        }
    });


    function setcostdata(data) {
        jQuery('.tbl-cost-data .new-row').remove();
        if (data.length > 0) {
            var costtot = 0;
            for (i = 0; i < data.length; i++) {
                if (Number(data[i].TJC_COST_AMT) > 0) {
                    jQuery('.tbl-cost-data').append('<tr class="new-row">' +
                      '<td>' + data[i].TJC_DESC + '</td>' +
                       '<td class="dec-align">' + ReplaceNumberWithCommas(Number(data[i].TJC_COST_AMT).toFixed(2)) + '</td>' +
                       '<td>' + ' <button class="btn btn-sm-min btn-red-fullbg remove-cost-data">' +
                                        '<i class="fa fa-times" aria-hidden="true"></i>' +
                                   ' </button>' + '</td>' +

                        '</tr>');
                    costtot = costtot + Number(data[i].TJC_COST_AMT);
                }


            }
            total_diff = 0;
            jQuery("#totcost").val(ReplaceNumberWithCommas(Number(costtot).toFixed(2)));
            total_cost = costtot;
            total_diff = total_rev_amount - total_cost;
            jQuery("#costdiff").val(ReplaceNumberWithCommas(Number(total_diff).toFixed(2)));
            RemoveNewCostData();
        }
    }
    function setrevndata(data) {
        jQuery('.tbl-rev-data .new-row').remove();
        if (data.length > 0) {
            var revtot = 0;
            for (i = 0; i < data.length; i++) {
                if (Number(data[i].TJC_COST_AMT) > 0) {
                    jQuery('.tbl-rev-data').append('<tr class="new-row">' +
     '<td>' + data[i].TJC_ELEMENT_CODE + '</td>' +
      '<td>' + data[i].TJC_DESC + '</td>' +
       '<td class="dec-align">' + ReplaceNumberWithCommas(Number(data[i].TJC_COST_AMT).toFixed(2)) + '</td>' +

        '</tr>');

                    revtot = revtot + Number(data[i].TJC_COST_AMT);
                }


            }
            jQuery("#totrev").val(ReplaceNumberWithCommas(Number(revtot).toFixed(2)));
            total_rev_amount = revtot;
            // ServiceCheckChangeFunction();
        }
    }
    function RemoveNewCostData() {
        jQuery(".remove-cost-data").unbind('click').click(function (evt) {
            var td = jQuery(this).parent('td');
            var tr = jQuery(td).parent('tr');
            var desc = jQuery(tr).find('td:eq(0)').html();
            var cost = jQuery(tr).find('td:eq(1)').html();
            Lobibox.confirm({
                msg: "Do you want to Remove data?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        var check = "";
                        jQuery.ajax({
                            type: "GET",
                            url: "/ManualJobClosure/RemoveCostDetails",
                            data: { Desc: desc, Cost: cost },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        //set cost grid
                                        setcostdata(result.data);
                                        //setrevndata(result.data);

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
    }

    function JobOrPouchDetails(code, type) {

        jQuery.ajax({
            type: "GET",
            url: "/ManualJobClosure/JobOrPouchDetails",
            data: { code: code, type: type },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {

                if (result.login == false) {
                    Logout();
                } else {
                    if (result.success == false) {

                    }
                    if (result.success == true) {

                        if (result.count > 0) {
                            var job_no = result.data[0].JB_JB_NO;
                            var pouch_no = result.data[0].JB_POUCH_NO;
                            var job_date = result.data[0].JB_JB_DT;
                            job_status = result.data[0].JB_STUS;

                            if (type == "JOB") { jQuery('#pouchno').val(pouch_no); }
                            if (type == "PCH") { jQuery('#jobno').val(job_no); }
                            //jQuery('#date').val(job_date);
                            if (job_status == "P") {
                                jQuery('#status').val("Pending");
                            }
                            else if (job_status == "A") {
                                jQuery('#status').val("Approved");
                            }
                            else if (job_status == "F") {
                                jQuery('#status').val("Closed");
                            }
                                //else if (job_status == "C") {
                                //    jQuery('#status').val("Finished");
                                //    job_status = "C";
                                //}
                            else {
                                jQuery('#status').val("");
                            }
                        }
                    }
                }
            }
        });
    }

    jQuery('.btn-add-cost').click(function (e) {

        if (jQuery("#jobno").val() == "") {
            setInfoMsg("Please Select Job");
            return;
        }
        if (jQuery("#desc").val() == "") {
            setInfoMsg("Please Select Description");
            return;
        }
        if (jQuery("#cost").val() == "") {
            setInfoMsg("Please Select Cost");
            return;
        }
        Lobibox.confirm({
            msg: "Do you want to add data?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    var check = "";
                    jQuery.ajax({
                        type: "GET",
                        url: "/ManualJobClosure/AddJobCostinData",
                        data: { JobNo: jQuery("#jobno").val(), Desc: jQuery("#desc").val(), Cost: jQuery("#cost").val() },
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    //set cost grid
                                    setcostdata(result.data);
                                    //setrevndata(result.data);
                                    jQuery("#desc").val("");
                                    jQuery("#cost").val(0)

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

    jQuery(".btn-clear").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to clear data ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    window.location.href = "/ManualJobClosure";
                }
            }
        })

    });
    jQuery('.btn-save').click(function (e) {

        //if (jQuery("#jobno").val() == "") {
        //    setInfoMsg("Please Select Job");
        //    return;
        //}
        if (jQuery("#date").val() == "") {
            setInfoMsg("Please Select Date");
            return;
        }
        //if (job_status == "F") {
        //    setInfoMsg("This job is already closed");
        //    return;
        //}
        Lobibox.confirm({
            msg: "Do you want to close job?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    var check = "";
                    jQuery.ajax({
                        type: "GET",
                        url: "/ManualJobClosure/SaveJobClose",
                        data: { JobNo: jQuery("#jobno").val(), Date: jQuery("#date").val(), Remark: jQuery("#remark").val() },
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    //Clear Data
                                    Cleardata();
                                    setSuccesssMsg(result.msg);
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

    jQuery('.btn-reopen').click(function (e) {

        if (jQuery("#jobno").val() == "") {
            setInfoMsg("Please Select Job");
            return;
        }
        if (jQuery("#date").val() == "") {
            setInfoMsg("Please Select Date");
            return;
        }
        if (job_status != "F") {
            setInfoMsg("This job is not closed");
            return;
        }
        if (jQuery("#reremark").val() == "") {
            setInfoMsg("Please enter the reason");
            return;
        }
        Lobibox.confirm({
            msg: "Do you want to re-open this job?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    var check = "";
                    jQuery.ajax({
                        type: "GET",
                        url: "/ManualJobClosure/ReopenJobClose",
                        data: { JobNo: jQuery("#jobno").val(), Date: jQuery("#date").val(), Remark: jQuery("#reremark").val() },
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    //Clear Data
                                    Cleardata();
                                    setSuccesssMsg(result.msg);
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

    function Cleardata() {
        jQuery("#totcost").val(0);
        jQuery("#pouchno").val("");
        jQuery("#jobno").val("");
        jQuery("#status").val("");
        jQuery("#totrev").val(0);
        jQuery("#remark").val("");
        jQuery("#reremark").val("");
        jQuery('.tbl-rev-data .new-row').remove();
        jQuery('.tbl-cost-data .new-row').remove();
        total_cost = 0;
        total_rev_amount = 0;
        total_diff = 0;
        jQuery("#costdiff").val();
        job_status = "N";

        jQuery.ajax({
            type: "GET",
            url: "/ManualJobClosure/ClearSession",
            data: {},
            contentType: "application/json;charset=utf-8",
            dataType: "json",
        })
    }
    $('input#cost').blur(function () {
        var num = parseFloat($(this).val());
        var cleanNum = num.toFixed(2);
        if (cleanNum == "NaN") {
            cleanNum = 0;
        }
        if (Number(cleanNum) < 0) {
            cleanNum = 0;
        }
        $(this).val(cleanNum);
    });
    function ReplaceNumberWithCommas(yourNumber) {
        //Seperates the components of the number
        var n = yourNumber.toString().split(".");
        //Comma-fies the first part
        n[0] = n[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        //Combines the two sections
        return n.join(".");
    }
});