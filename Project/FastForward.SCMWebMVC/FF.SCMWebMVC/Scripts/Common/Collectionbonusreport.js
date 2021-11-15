jQuery(document).ready(function () {

    jQuery('#rdate').val(my_date_formatmonth(new Date()));
    jQuery('#rdate').datepicker({ dateFormat: "MM yy" })

    function getFormatedDate1(date) {
        var dte = new Date(parseInt(date.substr(6)));
        if (my_date_format_tran(dte) != "NaN/undefined/NaN")
            return my_date_format_tran(dte);
    }

    jQuery(".btn-detail-data").click(function () {

        var rdate = jQuery("#rdate").val();
        //var tdate = jQuery("#tdate").val();
        //var commcode = jQuery("#commcode").val();

        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/Collectionbonusreport/ViewDetails",
            data: { fdate: rdate },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        // if (result.type == "err") {
                        //setInfoMsg(result.msg);
                        // } else {
                        // setSuccesssMsg(result.msg);
                        // }
                        if (result.number == 0) {


                        } else {
                            window.location.href = result.urlpath;
                        }

                    } else {
                        setError(result.msg);
                    }
                } else {
                    Logout();
                }
            }
        });
    });
});