jQuery(document).ready(function () {
    jQuery('#bmonthdate').val(my_date_formatmonth(new Date()));
    jQuery('#bmonthdate').datepicker({ dateFormat: "MM yy" });
    jQuery(".loc-search").click(function (evt) {
        var headerKeys = Array();
        headerKeys = ["Row", "Code", "Description"];
        field = "srchPCM2";
        var data = {
            chnl: "",
            sChnl: "",
            area: "",
            regn: "",
            zone: "",
            type: "PC"
        };
        var x = new CommonSearch(headerKeys, field, data);
    });
    jQuery(".btn-arrears-process-data").click(function (evt) {
        if (jQuery("#bmonthdate").val() == "") {
            setInfoMsg('Please Select Month');
            return;
        }
        Lobibox.confirm({
            msg: "Do you want to process data ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    jQuery.ajax({
                        cache: false,
                        type: "GET",
                        url: "/ProcessArreas/ArrearsProcess",
                        data: { Month: jQuery("#bmonthdate").val() },
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    setSuccesssMsg(result.msg);
                                } else {
                                    if (result.type=="Info")
                                    {
                                        setInfoMsg(result.msg);
                                    } else {
                                        setError(result.msg);
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
    jQuery(".btn-view-data").click(function (evt) {
        var pc = $(this).val();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/ProcessArreas/LoadArrdata",
            data: { Pc: jQuery("#location").val(), Month: jQuery("#bmonthdate").val() },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.list.length > 0) {
                            //set item data
                            if (result.list.length > 0) {
                                updatePC(result.list);
                            }
                        }
                    } else {
                        setInfoMsg('No Data Found! Please Check PC Code!!');
                        return;
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    function ReplaceNumberWithCommas(yourNumber) {
        //Seperates the components of the number
        var n = yourNumber.toString().split(".");
        if (n.length == 1) {
            yourNumber = yourNumber.toString() + ".00";
        }
        n = yourNumber.toString().split(".");

        //Comma-fies the first part
        n[0] = n[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        //Combines the two sections

        return n.join(".");
    }
    function updatePC(data) {
        jQuery('.acc-data-row .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {
                jQuery('.acc-data-row').append('<tr class="new-row">' +
                          '<td>' + data[i].Haa_pc + '</td>' +
                           '<td>' + ReplaceNumberWithCommas(data[i].Haa_act_arr_amt) + '</td>' +
                            '<td>' + data[i].Haa_tot_no_of_arr_acc + '</td>' +
                            '<td>' + ReplaceNumberWithCommas(data[i].Haa_tot_clos_bal) + '</td>' +
                            '<td>' + data[i].Haa_tot_no_of_act_acc + '</td>' +
                           '<td>' + "Completed"+ '</td>' +
                        '</tr>');
            }
        }
    }
});