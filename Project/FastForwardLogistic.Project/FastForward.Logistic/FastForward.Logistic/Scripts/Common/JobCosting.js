jQuery(document).ready(function () {

    var total_cost = 0;
    var total_rev_amount = 0;
    var total_diff = 0;
    var job_status = "N";
    //jQuery('.btn-approve').attr("disabled", "true");
    //jQuery('.btn-cancel').attr("disabled", "true");
    jQuery('.btn-print').attr("disabled", "true");
    jQuery('.btn-save').attr("disabled", "true");
    jQuery('.btn-approve').attr("disabled", "true");
    jQuery('.btn-approve2').attr("disabled", "true");
    
    //jQuery(".btn-save").show();
    //jQuery(".btn-approve2").hide();
    //jQuery(".btn-emp-update-data").show();
    //jQuery(".btn-emp-save-data").hide();

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
    jQuery(".cst-ele-srch").click(function (evt) {
        evt.preventDefault();
        var headerKeys = Array()
        headerKeys = ["Row", "Description", "Cost Code", "Account Code"];
        field = "costele";
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".cst-ele-srchRev").click(function (evt) {
        evt.preventDefault();
        var headerKeys = Array()
        headerKeys = ["Row", "Description", "Cost Code", "Account Code"];
        field = "costeleRev";
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery("#CstEle").focusout(function () {
        var code = jQuery(this).val();
        cstelemtFocusOut(code);
    });
    jQuery("#CstEleRev").focusout(function () {
        var code = jQuery(this).val();
        cstelemtFocusOutRev(code);
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
                        url: "/JobCosting/LoadJobCostinData",
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
                                    var profit = ReplaceNumberWithCommas(Number(result.costdiff).toFixed(2));
                                    //alert("sdgdsgf");
                                    if (profit >= 0)
                                    {
                                        jQuery(".costdiff").html("Rs. " + profit + "  PROFIT");
                                        jQuery(".costdiffl").html("");
                                    }
                                    else
                                    {
                                        jQuery(".costdiff").html("");
                                        jQuery(".costdiffl").html("(Rs. " + profit + ")  LOSS");
                                    }
                                    jQuery(".precoststatus").empty();
                                    if (result.TJC_APP_1 >= 1 && result.TJC_APP_2 == 0 && result.TJC_ACT >=1) {
                                        jQuery(".precoststatus").html("Status : Approved by level 1");
                                        jQuery('.btn-print').attr("disabled", "true");
                                        jQuery('.btn-save').attr("disabled", "true");
                                        jQuery('.btn-approve').attr("disabled", "true");
                                        //jQuery('.btn-approve2').attr("disabled", "false");
                                        jQuery('.btn-approve2').removeAttr("disabled");
                                    }
                                    if (result.TJC_APP_1 >= 1 && result.TJC_APP_2 >= 1 && result.TJC_ACT >= 1) {
                                        jQuery(".precoststatus").html("Status : Approved by level 1 and 2");
                                        //jQuery('.btn-print').attr("disabled", "false");
                                        jQuery('.btn-print').removeAttr("disabled");
                                        jQuery('.btn-save').attr("disabled", "true");
                                        jQuery('.btn-approve').attr("disabled", "true");
                                        jQuery('.btn-approve2').attr("disabled", "true");
                                    }
                                    if (result.TJC_APP_1 == 0 && result.TJC_APP_2 == 0 && result.TJC_ACT >= 1) {
                                        jQuery(".precoststatus").html("Status : Pending");
                                        jQuery('.btn-print').attr("disabled", "true");
                                        //jQuery('.btn-save').attr("enable", "true");
                                        jQuery('.btn-save').removeAttr("disabled");
                                        //jQuery('.btn-save').attr("disabled", "true");
                                        jQuery('.btn-approve').removeAttr("disabled");
                                        //jQuery('.btn-approve').attr("disabled", "true");
                                        jQuery('.btn-approve2').attr("disabled", "true");
                                    }
                                    if (result.TJC_APP_1 == 0 && result.TJC_APP_2 == 0 && result.TJC_ACT == 0)
                                    {
                                        jQuery(".precoststatus").html("Status : Pending");
                                        jQuery('.btn-print').attr("disabled", "true");
                                        //jQuery('.btn-save').attr("enable", "true");
                                        jQuery('.btn-save').removeAttr("disabled");
                                        //jQuery('.btn-approve').removeAttr("disabled");
                                        jQuery('.btn-approve').attr("disabled", "true");
                                        jQuery('.btn-approve2').attr("disabled", "true");
                                    }

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
                                          "/JobCosting/RequestPrint?JobNo=" + jQuery("#jobno").val(),
                                          '_blank' // <- This is what makes it open in a new window.
                                         );
                    //var check = "";
                    //jQuery.ajax({
                    //    type: "GET",
                    //    url: "/JobCosting/RequestPrint",
                    //    data: { JobNo: jQuery("#jobno").val() },
                    //    contentType: "application/json;charset=utf-8",
                    //    dataType: "json",
                    //    success: function (result) {
                    //        if (result.login == true) {
                    //            if (result.success == true) {

                    //                window.open(
                    //                      "/JobCosting/RequestPrint?JobNo=" + jQuery("#jobno").val(),
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
                                jQuery("#desc").val(result.data.MCE_DESC);
                            }
                        }
                    }
                }
            });
        }
    }
    function cstelemtFocusOutRev(code) {
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
                            jQuery("#CstEleRev").val("");
                            jQuery("#CstEleRev").focus();
                        }
                        if (result.success == true) {
                            if (result.data.MCE_CD == null) {
                                setInfoMsg("Please enter valid cost element code.");
                                jQuery("#CstEleRev").val("");
                                jQuery("#CstEleRev").focus();
                            } else {
                                jQuery("#descRev").val(result.data.MCE_DESC);
                            }
                        }
                    }
                }
            });
        }
    }
    function setcostdata(data) {
        jQuery('.tbl-cost-data .new-row').remove();
        if (data.length > 0) {
            var costtot = 0;
            for (i = 0; i < data.length; i++) {
                if (Number(data[i].TJC_COST_AMT) > 0) {
                    jQuery('.tbl-cost-data').append('<tr class="new-row">' +
                       '<td>' + data[i].TJC_ELEMENT_CODE + '</td>' +
                       '<td>' + data[i].TJC_DESC + '</td>' +
                       '<td class="dec-align">' + ReplaceNumberWithCommas(Number(data[i].TJC_MARGIN).toFixed(2)) + '</td>' +
                       '<td class="dec-align">' + ReplaceNumberWithCommas(Number(data[i].TJC_COST_AMT).toFixed(2)) + '</td>' +
                       
                       //'<td>' + ' <button class="btn btn-sm-min btn-red-fullbg remove-cost-data">' +
                       //                 '<i class="fa fa-times" aria-hidden="true"></i>' +
                       //            ' </button>' + '</td>' +

                        '</tr>');
                    costtot = costtot + Number(data[i].TJC_COST_AMT);
                }


            }
            jQuery(".tbl-cost-data tbody tr.new-row").click(function (evt) {
                evt.preventDefault();
                var code = jQuery(this).closest("tr").find('td:eq(0)').text();
                var desc = jQuery(this).closest("tr").find('td:eq(1)').text();
                var cost = jQuery(this).closest("tr").find('td:eq(3)').text();
                var margin = jQuery(this).closest("tr").find('td:eq(2)').text();
                
                jQuery("#CstEle").val(code);
                jQuery("#desc").val(desc);
                jQuery("#cost").val(cost);
                jQuery("#margin").val(margin);
                jQuery("#margindef").val(margin);
                //loadRequestDataBySeq(value);
            });
            total_diff = 0;
            jQuery("#totcost").val(ReplaceNumberWithCommas(Number(costtot).toFixed(2)));
            total_cost = costtot;
            total_diff = total_rev_amount - total_cost;
            jQuery("#costdiff").val(ReplaceNumberWithCommas(Number(total_diff).toFixed(2)));
            var profit = ReplaceNumberWithCommas(Number(total_diff).toFixed(2));
            //alert("1111");
            if (total_diff >= 0)
            {
                jQuery(".costdiff").html("Rs. " + profit + "  PROFIT");
                jQuery(".costdiffl").html("");
            }
            else
            {
                jQuery(".costdiff").html("");
                jQuery(".costdiffl").html("(Rs. " + profit + ")  LOSS");
            }
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
      '<td class="dec-align">' + ReplaceNumberWithCommas(Number(data[i].TJC_MARGIN).toFixed(2)) + '</td>' +
       '<td class="dec-align">' + ReplaceNumberWithCommas(Number(data[i].TJC_COST_AMT).toFixed(2)) + '</td>' +
       
        '</tr>');

                    revtot = revtot + Number(data[i].TJC_COST_AMT);
                }


            }
            jQuery(".tbl-rev-data tbody tr.new-row").click(function (evt) {
                evt.preventDefault();
                var coderev = jQuery(this).closest("tr").find('td:eq(0)').text();
                var descrev = jQuery(this).closest("tr").find('td:eq(1)').text();
                var costrev = jQuery(this).closest("tr").find('td:eq(3)').text();
                var marginrev = jQuery(this).closest("tr").find('td:eq(2)').text();

                jQuery("#CstEleRev").val(coderev);
                jQuery("#descRev").val(descrev);
                jQuery("#costRev").val(costrev);
                //jQuery("#margin").val(marginrev);
                //jQuery("#margindef").val(marginrev);
                //loadRequestDataBySeq(value);
            });
            jQuery("#totrev").val(ReplaceNumberWithCommas(Number(revtot).toFixed(2)));
            total_rev_amount = revtot;
            // ServiceCheckChangeFunction();
            //var x = parseFloat(jQuery("#totrev").val());
            var x = jQuery("#totrev").val().replace(/,/g, '');
            //var y = parseFloat(jQuery("#totcost").val());
            var y = jQuery("#totcost").val().replace(/,/g, '');
            var diff = x - y;
            jQuery("#costdiff").val(ReplaceNumberWithCommas(Number(diff).toFixed(2)));
            var profit = ReplaceNumberWithCommas(Number(diff).toFixed(2));
            //alert(profit);
            if (diff >= 0) {
                jQuery(".costdiff").html("Rs. " + profit + "  PROFIT");
                jQuery(".costdiffl").html("");
            }
            else
            {
                jQuery(".costdiff").html("");
                jQuery(".costdiffl").html("(Rs. " + profit + ")  LOSS");
            }
            //costdiff = totrev - totcost;
        }
    }
    function RemoveNewCostData() {
        jQuery(".remove-cost-data").unbind('click').click(function (evt) {           
            evt.preventDefault();
            var td = jQuery(this).parent('td');
            var tr = jQuery(td).parent('tr');
            var desc = jQuery(tr).find('td:eq(1)').html();
            var cost = jQuery(tr).find('td:eq(2)').html();
            Lobibox.confirm({
                msg: "Do you want to Remove data?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        var check = "";
                        jQuery.ajax({
                            type: "GET",
                            url: "/JobCosting/RemoveCostDetails",
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
            url: "/JobCosting/JobOrPouchDetails",
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
                            var cus_code = result.data[0].MBE_CD;
                            var cus_name = result.data[0].MBE_NAME;

                            jQuery('#cus_code').val(cus_code);
                            jQuery('#cus_name').val(cus_name);

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
        var validatemsg = "";
        var newmargin = parseFloat(jQuery("#margin").val());
        var oldmargin = parseFloat(jQuery("#margindef").val());

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

        if (newmargin < oldmargin) {
            validatemsg = "Entered margin is less than default margin.Do you want to add data?";
        }
        else
        {
            validatemsg = "Do you want to add data?";
        }
        Lobibox.confirm({
            //msg: "Do you want to add data?",
            msg: validatemsg,
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    var check = "";
                    jQuery.ajax({
                        type: "GET",
                        url: "/JobCosting/AddJobCostinData",
                        data: { JobNo: jQuery("#jobno").val(), Desc: jQuery("#desc").val(), Cost: jQuery("#cost").val(), CstEle: jQuery("#CstEle").val(), Margin: jQuery("#margin").val() },
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    //set cost grid
                                    setcostdata(result.data);
                                    setrevndata(result.actualCostData);
                                    //setrevndata(result.data);
                                    jQuery("#CstEle").val("");
                                    jQuery("#desc").val("");
                                    jQuery("#cost").val(0)
                                    jQuery("#margin").val(0)

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

    //jQuery('.btn-add-cost').click(function (e) {
    jQuery('.cost').keypress(function (e) {
        var key = e.which;
        if (key == 13) {
            var validatemsg = "";
            var newmargin = parseFloat(jQuery("#margin").val());
            var oldmargin = parseFloat(jQuery("#margindef").val());

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

            if (newmargin < oldmargin) {
                validatemsg = "Entered margin is less than default margin.Do you want to add data?";
            }
            else {
                validatemsg = "Do you want to add data?";
            }
            Lobibox.confirm({
                //msg: "Do you want to add data?",
                msg: validatemsg,
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        var check = "";
                        jQuery.ajax({
                            type: "GET",
                            url: "/JobCosting/AddJobCostinData",
                            data: { JobNo: jQuery("#jobno").val(), Desc: jQuery("#desc").val(), Cost: jQuery("#cost").val(), CstEle: jQuery("#CstEle").val(), Margin: jQuery("#margin").val() },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        //set cost grid
                                        setcostdata(result.data);
                                        setrevndata(result.actualCostData);
                                        //setrevndata(result.data);
                                        jQuery("#CstEle").val("");
                                        jQuery("#desc").val("");
                                        jQuery("#cost").val(0)
                                        jQuery("#margin").val(0)

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
        }
    });

    jQuery('.btn-add-costrev').click(function (e) {
        //var validatemsg = "";
        //var newmargin = parseFloat(jQuery("#margin").val());
        //var oldmargin = parseFloat(jQuery("#margindef").val());

        if (jQuery("#jobno").val() == "") {
            setInfoMsg("Please Select Job");
            return;
        }
        if (jQuery("#descRev").val() == "") {
            setInfoMsg("Please Select Description");
            return;
        }
        if (jQuery("#costRev").val() == "") {
            setInfoMsg("Please Select Cost");
            return;
        }

        //if (newmargin < oldmargin) {
        //    validatemsg = "Entered margin is less than default margin.Do you want to add data?";
        //}
        //else {
        //    validatemsg = "Do you want to add data?";
        //}
        Lobibox.confirm({
            msg: "Do you want to add data?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    var check = "";
                    jQuery.ajax({
                        type: "GET",
                        url: "/JobCosting/AddJobCostinDataRev",
                        data: { JobNo: jQuery("#jobno").val(), Desc: jQuery("#descRev").val(), Cost: jQuery("#costRev").val(), CstEle: jQuery("#CstEleRev").val(), Margin: jQuery("#margin").val() },
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    //set cost grid
                                    //setcostdata(result.data);
                                    setrevndata(result.data);
                                    //setrevndata(result.data);
                                    jQuery("#CstEleRev").val("");
                                    jQuery("#descRev").val("");
                                    jQuery("#costRev").val(0)
                                    jQuery("#margin").val(0)

                                    if(result.marginout = 1)
                                    {
                                        setInfoMsg("Revenue is less than the margin");
                                    }

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

    //jQuery('.btn-add-costrev').click(function (e) {
    jQuery('.costRev').keypress(function (e) {
        var key = e.which;
        if (key == 13) {
            //var validatemsg = "";
            //var newmargin = parseFloat(jQuery("#margin").val());
            //var oldmargin = parseFloat(jQuery("#margindef").val());

            if (jQuery("#jobno").val() == "") {
                setInfoMsg("Please Select Job");
                return;
            }
            if (jQuery("#descRev").val() == "") {
                setInfoMsg("Please Select Description");
                return;
            }
            if (jQuery("#costRev").val() == "") {
                setInfoMsg("Please Select Cost");
                return;
            }

            //if (newmargin < oldmargin) {
            //    validatemsg = "Entered margin is less than default margin.Do you want to add data?";
            //}
            //else {
            //    validatemsg = "Do you want to add data?";
            //}
            Lobibox.confirm({
                msg: "Do you want to add data?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        var check = "";
                        jQuery.ajax({
                            type: "GET",
                            url: "/JobCosting/AddJobCostinDataRev",
                            data: { JobNo: jQuery("#jobno").val(), Desc: jQuery("#descRev").val(), Cost: jQuery("#costRev").val(), CstEle: jQuery("#CstEleRev").val(), Margin: jQuery("#margin").val() },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        //set cost grid
                                        //setcostdata(result.data);
                                        setrevndata(result.data);
                                        //setrevndata(result.data);
                                        jQuery("#CstEleRev").val("");
                                        jQuery("#descRev").val("");
                                        jQuery("#costRev").val(0)
                                        jQuery("#margin").val(0)

                                        if (result.marginout = 1) {
                                            setInfoMsg("Revenue is less than the margin");
                                        }

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
        }
    });

    jQuery(".btn-clear").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to clear data ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    window.location.href = "/JobCosting";
                }
            }
        })

    });
    jQuery('.btn-save').click(function (e) {

        if (jQuery("#jobno").val() == "") {
            setInfoMsg("Please Select Job");
            return;
        }
        if (jQuery("#date").val() == "") {
            setInfoMsg("Please Select Date");
            return;
        }
        if (job_status == "F") {
            setInfoMsg("This job is already closed");
            return;
        }
        Lobibox.confirm({
            msg: "Do you want to save?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    var check = "";
                    jQuery.ajax({
                        type: "GET",
                        url: "/JobCosting/SaveJobClose",
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
    jQuery('.btn-approve').click(function (e) {

        if (jQuery("#jobno").val() == "") {
            setInfoMsg("Please Select Job");
            return;
        }
        if (jQuery("#date").val() == "") {
            setInfoMsg("Please Select Date");
            return;
        }
        if (job_status == "F") {
            setInfoMsg("This job is already closed");
            return;
        }
        Lobibox.confirm({
            msg: "Do you want to approve?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    var check = "";
                    jQuery.ajax({
                        type: "GET",
                        url: "/JobCosting/ApproveJobCost",
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
    jQuery('.btn-approve2').click(function (e) {

        if (jQuery("#jobno").val() == "") {
            setInfoMsg("Please Select Job");
            return;
        }
        if (jQuery("#date").val() == "") {
            setInfoMsg("Please Select Date");
            return;
        }
        if (job_status == "F") {
            setInfoMsg("This job is already closed");
            return;
        }
        Lobibox.confirm({
            msg: "Do you want to approve?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    var check = "";
                    jQuery.ajax({
                        type: "GET",
                        url: "/JobCosting/ApproveJobCost2",
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
                        url: "/JobCosting/ReopenJobClose",
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
        jQuery("#CstEle").val("");
        jQuery("#desc").val("");
        jQuery("#cost").val(0);
        jQuery(".costdiff").html("Rs. 0");
        jQuery(".precoststatus").html("-");

        jQuery("#cus_code").val("");
        jQuery("#cus_name").val("");

        jQuery('.tbl-rev-data .new-row').remove();
        jQuery('.tbl-cost-data .new-row').remove();
        total_cost = 0;
        total_rev_amount = 0;
        total_diff = 0;
        jQuery("#costdiff").val();
        job_status = "N";

        jQuery.ajax({
            type: "GET",
            url: "/JobCosting/ClearSession",
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