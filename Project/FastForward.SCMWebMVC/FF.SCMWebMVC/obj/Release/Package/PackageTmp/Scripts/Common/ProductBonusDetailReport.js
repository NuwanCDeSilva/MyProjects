jQuery(document).ready(function () {



    //Functions


    //Clicks
    jQuery(".circular-search").click(function (evt) {
        var schemeCode = $('#schnumber').val();
        var data = { schemeCode: schemeCode };
        var headerKeys = Array();
        headerKeys = ["Row", "Code"];
        field = "BonusCode3";
        var x = new CommonSearch(headerKeys, field, data);
    });


    jQuery(".schnumber-search").click(function (evt) {

        var circular_code = $('#circode').val();
        var msg = "Please select Scheme code";
      //  if (circular_code != '') {
            var data = { circularcode: circular_code };
            var headerKeys = Array();
            headerKeys = ["Row", "Code"];
            field = "SchemaCodeSearch";
            var x = new CommonSearch(headerKeys, field, data);
     //   }
       // else {
        //    setInfoMsg(msg);
         //   return;
      //  }
    });

    jQuery('#circode').focusout(function () {
        $('#datescir').empty();
        var circular_code = $('#circode').val();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/ProductBonusDetailReport/GetDateRange",
            data: { p_cir_code: circular_code },
            dataType: "json",
            success: function (result) {
                $xval = result;
                console.log(result);
                if (result.date!=null) {
                    for (var x = 0; x < result.date.length; x++) {
                        $('#datescir').append($('<option>',
                             {
                                 value: x,
                                 text: result.date[x].rbf_from_dt + " to " + result.date[x].rbf_to_dt
                             }));
                    }
                }
                else {
                    if (circular_code != "")
{
                    setInfoMsg("No valid data found");
                    jQuery('#circode').val('');
                    }
                //    jQuery('#schnumber').val('');
                    $('#datescir').empty();
                    return;
                }
            }
        });
        var scheme_code = jQuery('#circode').val();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/ProductBonusDetailReport/SchemeNumberSearch",
            data: { circularcode: scheme_code },
            dataType: "json",
            success: function (result) {
                $xval = result;
                console.log(result);
                if (result != null) {
                    jQuery('#HiddenSchemeNumber').val(result.data[0].Rbh_anal1);
                }
            }
        });

    });

    jQuery(".btn-detail-data").click(function () {
        var seldate = $('#datescir').find(":selected").text();
        if (seldate != "No data found") {
            console.log("asdasd");
            var datefrm = '';
            var dateto = '';
            var valDate = [];
            seldate = seldate.trim();
            valDate = seldate.split("to");
            datefrm = valDate[0];
            dateto = valDate[1];
            console.log(seldate);
            var msg = "Please select Scheme code";
            var scheme = $('#circode').val();
            if (scheme != '') {
            window.open(
                  "/ProductBonusDetailReport/ViewWithTarget?p_circular_code=" + jQuery("#circode").val() + "&p_scehema_code=" + jQuery("#schnumber").val() + "&FromDate=" + datefrm + "&ToDate=" + dateto,
                  '_blank' // <- This is what makes it open in a new window.
              );
            } else {
                setInfoMsg(msg);
                return;
            }
        } else {
            setInfoMsg("Please select a valid date");
            return;
        }
    });

    jQuery(".btn-view-data").click(function () {

        var seldate = $('#datescir').find(":selected").text();
        if (seldate != "No data found") {
            console.log("asdasd");
            var datefrm = '';
            var dateto = '';
            var valDate = [];
            seldate = seldate.trim();
            valDate = seldate.split("to");
            datefrm = valDate[0];
            dateto = valDate[1];
            console.log(seldate);
            var msg = "Please select Scheme code";
            var scheme = $('#circode').val();
            if (scheme != '') {
                window.open(
                      "/ProductBonusDetailReport/ViewWithTargetSummary?p_circular_code=" + jQuery("#circode").val() + "&p_scehema_code=" + jQuery("#HiddenSchemeNumber").val() + "&FromDate=" + datefrm + "&ToDate=" + dateto,
                      '_blank' // <- This is what makes it open in a new window.
                  );
            } else {
                setInfoMsg(msg);
                return;
            }
        } else {
            setInfoMsg("Please select a valid date");
            return;
        }
    });


    jQuery('.btn-clear-data').click(function () {
        jQuery('#circode').val('');
        jQuery('#schnumber').val('');
        $('#datescir').empty();
    });

});