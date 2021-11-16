jQuery(document).ready(function () {
    jQuery("#FromDate").val(getFormatedDate(new Date()));
    jQuery("#ToDate").val(getFormatedDate(new Date()));
    jQuery(".btn-display-det").click(function (evt) {
        evt.preventDefault();
        jQuery(".port-data").empty();
        jQuery("#portContainer").empty();
        jQuery("#agentContainer").empty();
        jQuery.ajax({
            type: "GET",
            data: { frmdt: jQuery("#FromDate").val(), todt: jQuery("#ToDate").val() },
            url: "/AgentAnalysis/getAgetPortDetails",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        jQuery(".port-data").empty();
                        if (result.data.length > 0) {
                            for (i = 0; i < result.data.length; i++) {
                                jQuery(".port-data").append(
                                    "<tr>" +
                                        "<td class='underline-port'>" + result.data[i].BL_PORT_LOAD + "</td>"
                                        + "<td class='underline-port'>" + result.data[i].PA_PRT_NAME + "</td>"
                                        + "<td class='sp-underline-agent'>" + result.data[i].BL_DEL_AGENT_CD + "</td>"
                                        + "<td class='sp-underline-agent'>" + result.data[i].BL_DEL_AGENT_NAME + "</td>"
                                        + "<td>" + result.data[i].CNT + "</td>"
                                        + "<td>" + result.data[i].CONTAINERFCL + "</td>"
                                    +"</tr>"
                                    );
                            }
                            portTotalChart(result.portTotal);
                            agentTotalChart(result.agentTotal);
                            //portClick();
                            //agentClick();
                        }
                        //loadBarChart(result.data);
                        //loadHighBarchart(result.data,result.fromPorts);
                        //if (result.agents.length > 0) {
                        //    jQuery("#datatable thead tr").empty();
                        //    jQuery("#datatable thead tr").append("<th></th>");
                        //    for (i = 0; i < result.agents.length; i++) {
                        //        jQuery("#datatable thead tr").append("<th>" + result.agents[i] + "</th>");
                        //    }
                        //    if (result.data.length > 0) {
                        //        jQuery("#datatable tbody").empty();
                        //        for (i = 0; i < result.agents.length; i++) {
                        //            //jQuery("#datatable tbody").append("<tr>" +
                        //            //    "<td>" + result.agents[i]["BL_PORT_LOAD"] + "</td>" +
                        //            //     "<td>" + result.agents[i]["CNT"] + "</td>" +
                        //            //    "</tr>");
                        //        }
                        //    }
                        //}
                        //loadTableChart();

                        //loadMoreBarChart(result.portCd, result.portName,result.data,result.agentCd,result.agentName);
                    } else {
                        if (result.type == "Info") {
                            setInfoMsg(result.msg);
                        }
                        if (result.type == "Error") {
                            showError(result.msg);
                        }
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    jQuery(".clear-inv").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to clear data ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    jQuery(".port-data").empty();
                    jQuery("#portContainer").empty();
                    jQuery("#agentContainer").empty();

                }
            }
        });

    });
    
});
function loadTableChart() {
    Highcharts.chart('container', {
        data: {
            table: 'datatable'
        },
        chart: {
            type: 'column'
        },
        title: {
            text: 'Data extracted from a HTML table in the page'
        },
        yAxis: {
            allowDecimals: false,
            title: {
                text: 'Units'
            }
        },
        tooltip: {
            formatter: function () {
                return '<b>' + this.series.name + '</b><br/>' +
                    this.point.y + ' ' + this.point.name.toLowerCase();
            }
        }
    });
}

function loadMoreBarChart(portcd, portname, data,agentCd,agentName) {
    console.log(portname);
    console.log(portcd);
    Morris.Bar({
        element: 'chartContainer',
        data://data,
            [
          { y: '025', "HK": 100, "SG": 90 ,"AAGER":20 },
          { y: '040', "SG": 65,"AIN":58 },
        ],
        xkey: "BL_DEL_AGENT_NAME",
        ykeys: portcd,
        labels: portname
    });
}
function loadHighBarchart(data,ports)
{
    console.log(data);
    Highcharts.chart('chartContainer', {
        chart: {
            type: 'column'
        },
        title: {
            text: 'Monthly Average Rainfall'
        },
        subtitle: {
            text: 'Source: WorldClimate.com'
        },
        xAxis: {
            categories: ports,
            crosshair: true
        },
        yAxis: {
            min: 0,
            title: {
                text: 'Rainfall (mm)'
            }
        },
        tooltip: {
            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                '<td style="padding:0"><b>{point.y:.1f}</b></td></tr>',
            footerFormat: '</table>',
            shared: true,
            useHTML: true
        },
        plotOptions: {
            column: {
                pointPadding: 0.2,
                borderWidth: 0
            }
        },
        series: //data
            [{
            name: 'Tokyo',
            data: [49.9, 71.5, 106.4, 129.2, 144.0, 144.0]

        }, {
            name: 'New York',
            data: [83.6, 78.8, 98.5, 93.4, 106.0, 144.0]

        }, {
            name: 'New York',
            data: [48.9, 38.8, 39.3, 41.4, 47.0, 144.0]

        }, {
            name: 'Berlin',
            data: [42.4, 33.2, 34.5, 39.7, 52.6, 144.0]

        }]
    });
}
function loadBarChart(data) {
    //cart start
    var chart = new CanvasJS.Chart("chartContainer", {
        zoomEnabled: true,
        panEnabled: true,
        title: {
            text: "Agent Analysis Chart"
        },
        data:data
        //    [
        //{
        //    type: "column",
        //    showInLegend: true,
        //    name: "Agt1",
        //    dataPoints: [
        //    { y: 1, label: "Port1" },
        //    { y: 2, label: "Port2" },
        //    { y: 3, label: "Port3" },

        //    ]
        //}, {
        //    type: "column",
        //    showInLegend: true,
        //    name: "Agt2",
        //    dataPoints: [
        //    { y: 3, label: "Port1" },
        //    { y: 0, label: "Port2" },
        //    { y: 8, label: "Port3" },

        //    ]
        //}
        //, {
        //    type: "column",
        //    showInLegend: true,
        //    name: "Agt3",
        //    dataPoints: [
        //    { y: 5, label: "Port1" },
        //    { y: 3, label: "Port2" },
        //    { y: 5, label: "Port3" },
        //    { y: 10, label:"Port4" },
        //    ]
        //}
        ////, {
        ////    type: "column",
        ////    showInLegend: true,
        ////    name: "Agt4",
        ////    dataPoints: [
        ////    { y: 5, label: "Port2" },
        ////    { y: 7, label: "Port3" },

        ////    ]
        ////}
        //]
    });

    chart.render();
}
function loadCompany() {

    jQuery.ajax({
        type: "GET",
        url: "/AgentAnalysis/loadCompany",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.login == true) {
                if (result.success == true) {
                    jQuery('.compny-display-tbl .new-row').remove();
                    if (result.companyList.length > 0) {
                        for (i = 0; i < result.companyList.length; i++) {
                            jQuery('.compny-display-tbl').append('<tr class="new-row">' +
                                    '<td class="chk-cmpny-data">' + '<input class="select-company" type="checkbox" name="selectedcompany" value="' + result.companyList[i].SEC_COM_CD + '">' + '</td>' +
                                    '<td>' + result.companyList[i].SEC_COM_CD + '</td>' +
                                    '<td>' + result.companyList[i].MasterComp.Mc_desc + '</td>' +
                                    '</tr>');
                        }
                        jQuery(".select-company").click(function () {
                            if (!jQuery('#chkmultiple').is(":checked")) {
                                jQuery(".select-company").prop("checked", false);
                                jQuery(this).prop("checked", true);
                            }
                            if (jQuery('[name="selectedcompany"]:checked').length == result.companyList.length) {
                                jQuery("#chkAllCompany").prop("checked", true);
                            } else {
                                jQuery("#chkAllCompany").prop("checked", false);
                            }
                        });

                    } else {
                        jQuery('.profcen-display-tbl').append('<tr class="new-row">' + "<td style='border:none; color: #ff6666; position: absolute; width: 80%; font-weight: bold;'>No company found for display</td>" + '</tr>');
                    }
                    jQuery("#chkAllCompany").attr("disabled", true);
                    jQuery("#chkmultiple").click(function () {
                        if (jQuery('#chkmultiple').is(":checked")) {
                            jQuery("#chkAllCompany").removeAttr("disabled");
                        } else {
                            jQuery("#chkAllCompany").attr("disabled", true);
                            jQuery("#chkAllCompany").prop("checked", false);
                            jQuery(".select-company").prop("checked", false);
                        }
                    });
                } else {
                    jQuery('.profcen-display-tbl').append('<tr class="new-row">' + "<td style='border:none; color: #ff6666; position: absolute; width: 80%; font-weight: bold;'>No company found for display</td>" + '</tr>');
                    setErrorPopUp(result.msg);
                }
            } else {
                Logout();
            }
        }
    });
}
function agentClick() {
    jQuery(".sp-underline-agent").click(function (evt) {
        evt.preventDefault();
        var formdata = jQuery("#filterdata-frm").serialize();
        jQuery.ajax({
            type: "GET",
            data: formdata + "&agent=" + jQuery(this).html(),
            url: "/AgentAnalysis/getFromPortData",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.data.length > 0) {
                            jQuery("#myModalPop").modal("show");
                            setPopup(result.data, 'agent', "# of Containers : "+result.cntfcl);
                        } else {
                            showNotice(result.msg);
                        }
                    } else {
                        if (result.type == "Info") {
                            showNotice(result.msg);
                        }
                        if (result.type == "Error") {
                            showError(result.msg);
                        }
                    }
                    jQuery(".grph-disply-btn").prop('disabled', false);
                } else {
                    Logout();
                }
            }
        });
    });
}

function portClick() {
    jQuery(".underline-port").click(function (evt) {
        evt.preventDefault();
        var formdata = jQuery("#filterdata-frm").serialize();
        jQuery.ajax({
            type: "GET",
            data: formdata+"&port="+jQuery(this).html(),
            url: "/AgentAnalysis/getFromPortData",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.data.length > 0) {
                            jQuery("#myModalPop").modal("show");
                            setPopup(result.data,'port');
                        } else {
                            showNotice(result.msg);
                        }
                    } else {
                        if (result.type == "Info") {
                            showNotice(result.msg);
                        }
                        if (result.type == "Error") {
                            showError(result.msg);
                        }
                    }
                    jQuery(".grph-disply-btn").prop('disabled', false);
                } else {
                    Logout();
                }
            }
        });
    });
}
function setPopup(data, type, cntfcl) {
    jQuery("#myModalPop .modal-title").empty();
    if (type == 'port') {
        jQuery("#myModalPop .modal-title").html(data[0].PA_PRT_NAME + "-" + data[0].BL_PORT_LOAD);
    } else {
        jQuery("#myModalPop .modal-title").html(data[0].BL_DEL_AGENT_CD + "-" + data[0].BL_DEL_AGENT_NAME +" <div class='bold-text'> ("+cntfcl+")</div> ");
    }

    var arr = [];
    var len = data.length;
    var total = 0;
    for (var i = 0; i < len; i++) {
        total = parseFloat(total) + parseFloat(data[i].CNT);
    }
    if (type == 'port') {
        for (var i = 0; i < len; i++) {
            var per = (parseFloat(data[i].CNT) / parseFloat(total)) * 100;
            arr.push({
                y: parseFloat(per).toFixed(2),
                name: data[i].BL_DEL_AGENT_NAME + ' (' + data[i].CNT + ') '
            });
        }
    } else {
        for (var i = 0; i < len; i++) {
            var per = (parseFloat(data[i].CNT) / parseFloat(total)) * 100;
            arr.push({
                y: parseFloat(per).toFixed(2),
                name: data[i].BL_PORT_LOAD + ' (' + data[i].CNT + ') '
            });
        }
    }
    var chart = new CanvasJS.Chart("chartContainer",
    {
        //title: {
        //    text: cntfcl
        //},
        exportFileName: "Pie Chart",
        exportEnabled: true,
        animationEnabled: true,
        legend: {
            verticalAlign: "bottom",
            horizontalAlign: "center",
            fontSize: 12
        },
        data: [
        {
            type: "pie",
            showInLegend: true,
            toolTipContent: "{name}: <strong>{y}%</strong>",
            indexLabel: "{name} {y}%",
            indexLabelFontSize: 12,
            dataPoints: arr
        }
        ]
    });
    chart.render();
}

function portTotalChart(port) {

    var arr = [];
    var len = port.length;
    var total = 0;
    for (var i = 0; i < len; i++) {
        total = parseFloat(total) + parseFloat(port[i].CNT);
    }
        for (var i = 0; i < len; i++) {
            var per = (parseFloat(port[i].CNT) / parseFloat(total)) * 100;
            arr.push({
                y: parseFloat(per).toFixed(2),
                name: port[i].PA_PRT_NAME + ' -' + port[i].BL_PORT_LOAD + " ( # of Shipments-" + port[i].CNT + " (" + parseFloat(per).toFixed(2) + "%) )",
                extra: port[i].PA_PRT_NAME + ' -' + port[i].BL_PORT_LOAD
            });
 }
        var chart = new CanvasJS.Chart("portContainer",
    {
    title: {
        text: "Ports"
    },
    exportFileName: "Pie Chart",
    exportEnabled: true,
    animationEnabled: true,
    legend: {
        verticalAlign: "bottom",
        horizontalAlign: "center",
        fontSize: 12
    },
    data: [
    {
        type: "pie",
        showInLegend: true,
        toolTipContent: "{extra}: <strong>{y}%</strong>",
        indexLabel: "{extra} {y}%",
        indexLabelFontSize: 12,
        dataPoints: arr
    }
    ]
});
    chart.render();
}
function agentTotalChart(agent) {
    var arr = [];
    var len = agent.length;
    var total = 0;
    for (var i = 0; i < len; i++) {
        total = parseFloat(total) + parseFloat(agent[i].CNT);
    }
    for (var i = 0; i < len; i++) {
        var per = (parseFloat(agent[i].CNT) / parseFloat(total)) * 100;
        arr.push({
            y: parseFloat(per).toFixed(2),
            name: agent[i].BL_DEL_AGENT_CD + ' -' + agent[i].BL_DEL_AGENT_NAME + " ( # of Shipments-" + agent[i].CNT + " (" + parseFloat(per).toFixed(2) + "%) )",
            extra: agent[i].BL_DEL_AGENT_CD + ' -' + agent[i].BL_DEL_AGENT_NAME,
        });
    }
    var chart = new CanvasJS.Chart("agentContainer",
{
    title: {
        text: "Agents"
    },
    exportFileName: "Pie Chart",
    exportEnabled: true,
    animationEnabled: true,
    legend: {
        verticalAlign: "bottom",
        horizontalAlign: "center",
        fontSize: 12
    },
    data: [
    {
        type: "pie",
        showInLegend: true,
        toolTipContent: "{extra}: <strong>{y}%</strong>",
        indexLabel: "{extra} {y}%",
        indexLabelFontSize: 12,
        dataPoints: arr

    }
    ]
});
    chart.render();
}