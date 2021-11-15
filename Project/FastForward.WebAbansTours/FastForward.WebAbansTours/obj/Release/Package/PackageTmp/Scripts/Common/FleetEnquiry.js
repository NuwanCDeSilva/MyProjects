jQuery(document).ready(function () {
    jQuery('#FrmDate').datepicker({ maxDate: new Date(), dateFormat: "dd/M/yy" });
    jQuery('#ToDate').datepicker({ maxDate: new Date(), dateFormat: "dd/M/yy" });

    jQuery('#FrmDate').val(getFormatedDate(getMonthAgoMonth(new Date())));
    jQuery('#ToDate').val(getFormatedDate(new Date()));
    jQuery(".btn-fleet-enq-view").click(function () {
        var fdate = jQuery("#FrmDate").val();
        var todate = jQuery('#ToDate').val();
        jQuery.ajax({
            type: "GET",
            url: "/FleetEnquiry/FleetAllocationDaily",
            data: { fdate: fdate ,todate: todate,},
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
              
    
                if (result.login == true) {
                    if (result.success == true) {
                       
                        loadFleedEnq(result.data);
                        
                    } else {
                       
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    function loadFleedEnq(mydata) {
        var chart = new CanvasJS.Chart("chartContainer",
                                     {
                                         title: {
                                             text: "Fleet Allocation"
                                         },
                                         axisY: {
                                             minimum: (new Date(2016, 0, 28, 11, 30, 00)).getTime(),
                                             interval: (1 * 60 * 60 * 1000),
                                             labelFormatter: function (e) {
                                                 return CanvasJS.formatDate(e.value, "DD-MMM-YY h:mm:ss TT");
                                             },
                                             gridThickness: 2

                                         },
                                         axisX: {

                                             reversed: true
                                         },

                                         toolTip: {
                                             contentFormatter: function (e) {

                                                 return "<strong>" + e.entries[0].dataPoint.label + "</strong></br> Start: " + CanvasJS.formatDate(e.entries[0].dataPoint.y[0], "DD-MMM-YY h:mm:ss TT") + "</br>End : " + CanvasJS.formatDate(e.entries[0].dataPoint.y[1], "DD-MMM-YY h:mm:ss TT");
                                             }
                                         },

                                         data: [
                                           {
                                               type: "rangeBar",
                                               xValueType: "dateTime",
                                               dataPoints: [
                           { label: "Walking", y: [mydata[0].mfd_frm_dt, mydata[0].mfd_to_dt] }
                           
                                               ]
                                           }
                                         ]
                                     });
        chart.render();

    }


    
});
