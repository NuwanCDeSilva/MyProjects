jQuery(document).ready(function () {
    jQuery('#Search_frm_date').datepicker({ dateFormat: "dd/M/yy" })
    jQuery('#Search_to_date').datepicker({ dateFormat: "dd/M/yy" })
    jQuery('#Mer_vad_from').datepicker({ dateFormat: "dd/M/yy" })
    jQuery('#Mer_vad_to').datepicker({ dateFormat: "dd/M/yy" })

    jQuery(".frm-exg-currency-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Currency Code", "Currency Description"];
        field = "currencysearch"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".to-exg-currency-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Currency Code", "Currency Description"];
        field = "currencysearchnew"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".exg-to-currency-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Currency Code", "Currency Description"];
        field = "currencysearchto"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".exg-frm-currency-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Currency Code", "Currency Description"];
        field = "currencysearchfrm"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery('.btn-exg-save-data').click(function (e) {

        var currentdate = new Date();
        var current =  new Date(currentdate);
        var frm = new Date(jQuery("#Mer_vad_from").val());
        var to =  new Date(jQuery("#Mer_vad_to").val());
        if (jQuery("#Mer_vad_to").val() != "") {

            if (frm > to) {
                setInfoMsg("Selected Date Range Wrong!!!");
            } else {



                Lobibox.confirm({
                    msg: "Do you want to continue process?",
                    callback: function ($this, type, ev) {
                        if (type == "yes") {
                            var formdata = jQuery("#exg-rate-crte-frm").serialize();
                            //  $("#enq-crte-frm").submit();
                            // setInfoMsg("Submit Complete");
                            jQuery.ajax({
                                type: "GET",
                                url: "/ExchangeRate/SaveExchangeRate",
                                data: formdata,
                                contentType: "application/json;charset=utf-8",
                                dataType: "json",
                                success: function (result) {
                                    if (result.login == true) {
                                        if (result.success == true) {
                                            setSuccesssMsg(result.msg);
                                            document.getElementById("exg-rate-crte-frm").reset();
                                            jQuery(".exg-table .new-row").remove();
                                           
                                            // jQuery('.Pending-Cancel').hide();
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

                });


            }

        } else {
            setInfoMsg("To Date Required.");
        }
        //document.getElementById("driverall-frm").submit();

    });
    jQuery(".btn-exg-clear-data").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to clear data ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    document.getElementById("exg-rate-crte-frm").reset();
                    jQuery(".exg-table .new-row").remove();


                    return false;
                }
            }
        });

    });

    jQuery(".btn-exg-search-data").click(function () {
        jQuery(".exg-table .new-row").remove();
        var frmcurr = jQuery("#Search_frm_currency").val();
        var tocurr = jQuery("#Search_to_currency").val();
        var date = jQuery("#Search_to_date").val();
        if (frmcurr == "" | tocurr == "" | date == "") {
            setInfoMsg("Please Enter Exchange Rate Details");
        } else {
            jQuery.ajax({
                type: "GET",
                url: "/ExchangeRate/getExchangeData",
                data: { frmcurr: jQuery("#Search_frm_currency").val(), date: jQuery("#Search_to_date").val(), tocurr: jQuery("#Search_to_currency").val() },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.data != null) {
                                setinvFieldValue(result.data);
                            } else {


                            }

                        } else {
                            if (response.type == "Error") {
                                setError(response.msg);
                            } else if (response.type == "Info") {
                                setInfoMsg(response.msg);
                            }
                        }
                    } else {
                        Logout();
                    }
                }

            });
        }
       

    });
    function setinvFieldValue(data) {
        if (data != "") {
            if (data != null) {
                for (i = 0; i < data.length; i++) {
                    jQuery('table.exg-table').append('<tr class="new-row"><td>'
                        + data[i].Mer_cur + '</td><td>'
                        + data[i].Mer_to_cur + '</td><td>'
                        + getFormatedDateInput(data[i].Mer_buyvad_from) + '</td><td>'
                        + getFormatedDateInput(data[i].Mer_buyvad_to) + '</td><td>'
                       + data[i].Mer_bnkbuy_rt + '</td><td>'
                       + data[i].Mer_bnksel_rt + '</td></tr>');

                }
            }
        }
    }
    $('#Search_to_date').focusout(function () {
        var str = $(this).val();
        if ($.datepicker.formatDate('MM dd, yy', new Date(jQuery("#Search_to_date").val())) == 'undefined NaN, NaN' && jQuery("#Search_to_date").val() != '') {
            setInfoMsg('Please enter a valid date !!!');
            $(this).val('');
        }
    });
    $('#Mer_vad_from').focusout(function () {
        var str = $(this).val();
        if ($.datepicker.formatDate('MM dd, yy', new Date(jQuery("#Mer_vad_from").val())) == 'undefined NaN, NaN' && jQuery("#Mer_vad_from").val() != '') {
            setInfoMsg('Please enter a valid date !!!');
            $(this).val('');
        }
    });
    $('#Mer_vad_to').focusout(function () {
        var str = $(this).val();
        if ($.datepicker.formatDate('MM dd, yy', new Date(jQuery("#Mer_vad_to").val())) == 'undefined NaN, NaN' && jQuery("#Mer_vad_to").val() != '') {
            setInfoMsg('Please enter a valid date !!!');
            $(this).val('');
        }
    });
    $('#Mer_bnkbuy_rt').focusout(function () {
        var str = $(this).val();
        var numRange = /^[0-9]\d*(\.\d+)?$/;
        if (!numRange.test(str)) {
            setInfoMsg('Please enter a valid Rate !!!');
            $(this).val('');
        }
    });
    $('#Mer_bnksel_rt').focusout(function () {
        var str = $(this).val();
        var numRange = /^[0-9]\d*(\.\d+)?$/;
        if (!numRange.test(str)) {
            setInfoMsg('Please enter a valid Rate !!!');
            $(this).val('');
        }
    });
});