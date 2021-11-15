jQuery(document).ready(function () {
    var data = [
    {
        value: 300,
        color: "#F7464A",
        highlight: "#FF5A5E",
        label: "Red"
    },
    {
        value: 50,
        color: "#46BFBD",
        highlight: "#5AD3D1",
        label: "Green"
    },
    {
        value: 100,
        color: "#FDB45C",
        highlight: "#FFC870",
        label: "Yellow"
    }
    ]
    var myPieChart1;
    var myPieChart2;
    jQuery(".btn-cost-enq-view").click(function () {
        if (typeof jQuery('input[name=REPORT_TYPE]:checked', '#frm-reporting').val() != "undefined") {
            if (jQuery("#FrmDate").val() != "" && jQuery("#ToDate").val() != "") {
                var data = jQuery('#frm-reporting').serialize();

                jQuery.ajax({
                    type: "GET",
                    data: data,
                    url: "/CostEnquiry/getEnqiyDetails",
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        if (result.login == true) {
                            if (result.success == true) {
                                var gpData = [];
                                var revanueData = [];
                                jQuery('.costenq-details-table').empty();
                                if (result._cost_Hdr_list_summery.length > 0) {
                                    jQuery('.costenq-details-table').append("<tr><th>Profit Center</th><th>Total Cost</th><th>	Total Revenue</th><th>GP</th><th>GP%</th></tr>");
                                    for (i = 0; i < result._cost_Hdr_list_summery.length ; i++) {


                                        var gpval = parseFloat(result._cost_Hdr_list_summery[i].QCH_TOT_VALUE) - parseFloat(result._cost_Hdr_list_summery[i].QCH_TOT_COST_LOCAL);
                                        var gpPer = parseFloat(gpval) / parseFloat(result._cost_Hdr_list_summery[i].QCH_TOT_VALUE) * 100;

                                        jQuery('.costenq-details-table').append('<tr class="new-row">' +
                                                        '<td>' + result._cost_Hdr_list_summery[i].QCH_SBU + '</td>' +
                                                        '<td class="text-left-align">' + addCommas(result._cost_Hdr_list_summery[i].QCH_TOT_COST_LOCAL) + '</td>' +
                                                        '<td class="text-left-align">' + addCommas(result._cost_Hdr_list_summery[i].QCH_TOT_VALUE) + '</td>' +
                                                        '<td class="text-left-align">' + addCommas(gpval.toFixed(2)) + '</td>' +
                                                        '<td class="text-left-align">' + gpPer.toFixed(2) + '</td>' +
                                                        '</tr>');

                                        gpData.push({
                                            data: gpPer.toFixed(2),
                                            label: result._cost_Hdr_list_summery[i].QCH_SBU
                                        });

                                        revanueData.push({
                                            data: result._cost_Hdr_list_summery[i].QCH_TOT_VALUE.toFixed(2),
                                            label: result._cost_Hdr_list_summery[i].QCH_TOT_VALUE.toFixed(2)
                                        });
                                        jQuery("#hover1").html("");
                                        jQuery("#hover").html("");
                                        gpChart(gpData);
                                        revChart(revanueData);
                                    }
                                } else if (result._cost_Hdr_list_Details.length > 0) {

                                    jQuery('.costenq-details-table').append("<tr><th>Cost Sheet #</th><th>Date</th><th>Total Cost</th><th>Total Revenu</th><th>GP</th><th>GP%</th></tr>");
                                    for (i = 0; i < result._cost_Hdr_list_Details.length ; i++) {
                                        var gpval = parseFloat(result._cost_Hdr_list_Details[i].QCH_TOT_VALUE) - parseFloat(result._cost_Hdr_list_Details[i].QCH_TOT_COST_LOCAL);
                                        var gpPer = parseFloat(gpval) / parseFloat(result._cost_Hdr_list_Details[i].QCH_TOT_VALUE) * 100;

                                        jQuery('.costenq-details-table').append('<tr class="new-row">' +
                                                        '<td>' + result._cost_Hdr_list_Details[i].QCH_COST_NO + '</td>' +
                                                        '<td>' + getFormatedDateInput(result._cost_Hdr_list_Details[i].QCH_DT) + '</td>' +
                                                        '<td class="text-left-align">' + addCommas(result._cost_Hdr_list_Details[i].QCH_TOT_COST_LOCAL) + '</td>' +
                                                        '<td class="text-left-align">' + addCommas(result._cost_Hdr_list_Details[i].QCH_TOT_VALUE) + '</td>' +
                                                        '<td class="text-left-align">' + addCommas(gpval.toFixed(2)) + '</td>' +
                                                        '<td class="text-left-align">' + gpPer.toFixed(2) + '</td>' +
                                                        '</tr>');

                                        gpData.push({
                                            data: gpPer.toFixed(2),
                                            label: result._cost_Hdr_list_Details[i].QCH_COST_NO
                                        });

                                        revanueData.push({
                                            data: result._cost_Hdr_list_Details[i].QCH_TOT_VALUE.toFixed(2),
                                            label: result._cost_Hdr_list_Details[i].QCH_TOT_VALUE.toFixed(2)
                                        })
                                        gpChart(gpData);
                                        revChart(revanueData);
                                    }
                                } else {
                                    var gpData = [];
                                    var revanueData = [];
                                    gpData.push({
                                        data: 0
                                    });

                                    revanueData.push({
                                        data: 0
                                    });
                                    gpChart(gpData);
                                    revChart(revanueData);
                                    jQuery("#hover1").html("");
                                    jQuery("#hover").html("");
                                    jQuery('.costenq-details-table').append("<tr><td style=' border:none; color: #ff6666; position: absolute; width:130px; font-weight: bold;'>No data found.</td></tr>");
                                }


                            } else {
                                if (typeof result.type != "undefined") {
                                    if (result.type == "Error") {
                                        setError(result.msg);
                                    }
                                    if (result.type == "Info") {
                                        setInfoMsg(result.msg);
                                    }
                                }
                            }
                        } else {
                            Logout();
                        }
                    }
                });
            } else {
                setInfoMsg("Please select valid date range.");
            }
        } else {
            setInfoMsg("Please check details of summery to view.");
        }
    });

   
    
    
    jQuery('#FrmDate').datepicker({ maxDate: new Date(), dateFormat: "dd/M/yy" });
    jQuery('#ToDate').datepicker({ maxDate: new Date(), dateFormat: "dd/M/yy" });

    

    jQuery('#FrmDate').val(getFormatedDate(getMonthAgoMonth(new Date())));
    jQuery('#ToDate').val(getFormatedDate(new Date()));

    jQuery("#ProfitCenter").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description"];
            field = "costProfCenSrch";
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery(".prof-cen-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description"];
        field = "costProfCenSrch";
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery("#Customer").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Name", "Nic", "Mobile", "Br No"];
            field = "costCusSrch"
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery(".customer-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Name", "Nic", "Mobile", "Br No"];
        field = "costCusSrch"
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery("#ReqNo").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Enquiry Id", "Ref Num", "Type", "Customer Code", "Name", "Address"];
            field = "costEnqSrch"
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery(".req-no-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Enquiry Id", "Ref Num", "Type", "Customer Code", "Name", "Address"];
        field = "costEnqSrch"
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery("#SerCde").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description"];
            field = "costSerCdeSrch"
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery(".ser-cde-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description"];
        field = "costSerCdeSrch"
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery("#CostShtRef").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Cost Sheet No", "Ref"];
            field = "costShetRefSrch"
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery(".cst-sht-ref-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Cost Sheet No", "Ref"];
        field = "costShetRefSrch"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".btn-cost-enq-clear").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to clear data ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    clearCostEntry();
                }
            }
        });

    });
    function clearCostEntry() {
        document.getElementById("frm-reporting").reset();
        jQuery('#FrmDate').val(getFormatedDate(getMonthAgoMonth(new Date())));
        jQuery('#ToDate').val(getFormatedDate(new Date()));
        jQuery('.costenq-details-table').empty();
        var gpData = [];
        var revanueData = [];
        gpData.push({
            data: 0
        });

        revanueData.push({
            data: 0
        });
        gpChart(gpData);
        revChart(revanueData);
        jQuery("#hover1").html("");
        jQuery("#hover").html("");
    }
    jQuery("#FrmDate,#ToDate").focusout(function () {
        if (ValidDate(jQuery(this).val()) == "false" && jQuery(this).val() != "") {
            setInfoMsg("Please enter valid date.");
            jQuery(this).val("");
        }
    });
});

function gpChart(gpData) {
    var placeholder = jQuery("#GpChart");
    placeholder.unbind();

    jQuery.plot(placeholder, gpData, {
        series: {
            pie: {
                show: true
            }
        },
        grid: {
            hoverable: true,
            clickable: true
        }
    });

    setCode([
        "jQuery.plot('#GpChart', gpData, {",
        "    series: {",
        "        pie: {",
        "            show: true",
        "        }",
        "    },",
        "    grid: {",
        "        hoverable: true,",
        "        clickable: true",
        "    },",
        "    legend: {",
        "        show: false",
        "    }",
        "});"
    ]);

    placeholder.bind("plothover", function (event, pos, obj) {

        if (!obj) {
            return;
        }

        var percent = parseFloat(obj.series.percent).toFixed(2);
        jQuery("#hover").html("<span style='background:none; font-weight:bold; color:" + obj.series.color + "'>" + obj.series.label + " (" + percent + "%)</span>");
    });

    placeholder.bind("plotclick", function (event, pos, obj) {

        if (!obj) {
            return;
        }

        percent = parseFloat(obj.series.percent).toFixed(2);
        //alert("" + obj.series.label + ": " + percent + "%");
    });
}

function revChart(revData) {
    var placeholder = jQuery("#RevChart");
    placeholder.unbind();

    jQuery.plot(placeholder, revData, {
        series: {
            pie: {
                show: true
            }
        },
        grid: {
            hoverable: true,
            clickable: true
        }
    });

    setCode([
        "jQuery.plot('#GpChart', gpData, {",
        "    series: {",
        "        pie: {",
        "            show: true",
        "        }",
        "    },",
        "    grid: {",
        "        hoverable: true,",
        "        clickable: true",
        "    },",
        "    legend: {",
        "        show: false",
        "    }",
        "});"
    ]);

    placeholder.bind("plothover", function (event, pos, obj) {

        if (!obj) {
            return;
        }

        var percent = parseFloat(obj.series.percent).toFixed(2);
        jQuery("#hover1").html("<span style='font-weight:bold; color:" + obj.series.color + "'>" + obj.series.label + " (" + percent + "%)</span>");
    });

    placeholder.bind("plotclick", function (event, pos, obj) {

        if (!obj) {
            return;
        }

        percent = parseFloat(obj.series.percent).toFixed(2);
        //alert("" + obj.series.label + ": " + percent + "%");
    });
}
function setCode(lines) {
    jQuery("#code").text(lines.join("\n"));
}