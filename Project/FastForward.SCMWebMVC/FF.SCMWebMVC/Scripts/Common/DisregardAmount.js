jQuery(document).ready(function () {

    var frmval;
    var toval;

    jQuery('#mnthfrom').val(my_date_formatmonth(new Date()));
    jQuery('#mnthfrom').datepicker({ dateFormat: "M/yy" })

    jQuery('#mnthto').val(my_date_formatmonth(new Date()));
    jQuery('#mnthto').datepicker({ dateFormat: "M/yy" })

    jQuery(".btn-clear-data").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to clear data ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    window.location.href = "/DisregardAmount";
                }
            }
        });

    });

    function getFormatedDate1(date) {
        var dte = new Date(parseInt(date.substr(6)));
        if (my_date_format_tran(dte) != "NaN/undefined/NaN")
            return my_date_format_tran(dte);
    }

    jQuery(".btn-view-data").click(function (evt) {
        var Circode = jQuery("#circode").val();
        if (jQuery("#circode").val()=="") {
        setInfoMsg('Please select the circular number!!');
        return;
    }
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/DisregardAmount/LoadMainDetails",
            data: { Circode: jQuery("#circode").val() },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.list != null) {
                            //set item data
                            if (result.list.length > 0) {
                                updatemanagerdetail(result.list);
                            }
                        }
                        else {
                            setInfoMsg('No data found!!');
                            return;
                        }
                    } else {
                        setInfoMsg('Please select the circular number!!');
                        return;
                    }
                } else {
                    Logout();
                }
            }
        });
    });

    $('#percentage').keypress(function (e) {
        var regex = new RegExp("^[0-9.]+$");
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        if (regex.test(str)) {
            return true;
        }

        e.preventDefault();
        return false;
    });

    $('#valuefrom').keypress(function (e) {
        var regex = new RegExp("^[0-9.]+$");
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        if (regex.test(str)) {
            return true;
        }

        e.preventDefault();
        return false;
    });

    $('#valueto').keypress(function (e) {
        var regex = new RegExp("^[0-9.]+$");
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        if (regex.test(str)) {
            return true;
        }

        e.preventDefault();
        return false;
    });

    function numberWithCommas1(x) {
        return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    }
    function numberWithCommas(yourNumber) {
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

    jQuery(".channel-search").click(function (evt) {
        $("#location").val("");
        var headerKeys = Array();
        headerKeys = ["Row", "Code", "Description"];
        field = "srchChnlAll";
        //var data = "channel";
        var data = {
            chnl: "",
            sChnl: "",
            area: "",
            regn: "",
            zone: "",
            type: "channel"
        };
        var x = new CommonSearch(headerKeys, field, data);
    });

    jQuery(".loc-search").click(function (evt) {
        $("#chanel").val("");
        var headerKeys = Array();
        headerKeys = ["Row", "Code", "Description"];
        field = "srchPCM4";
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

    jQuery(".circular-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "circular code"];
        field = "circular"
        var data = {
            Cat: 2
        }
        var x = new CommonSearch(headerKeys, field, data);
    });

    jQuery("#valuefrom").focusout(function (evt) {
        frmval = "";
        frmval = jQuery("#valuefrom").val();
        var fromvalue = numberWithCommas(jQuery("#valuefrom").val());
        $("#valuefrom").val(fromvalue);
    });

    jQuery("#valueto").focusout(function (evt) {
        toval = "";
        toval = jQuery("#valueto").val();
        var tovalue = numberWithCommas(jQuery("#valueto").val());
        $("#valueto").val(tovalue);
    });

    jQuery("#circode").focusout(function (evt) {
        var manager = $(this).val();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/DisregardAmount/LoadMainDetails",
            data: { Circode: jQuery("#circode").val(), Cat: 2 },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.list != null) {
                            //set item data
                            if (result.list.length > 0) {
                                updateMainDetails(result.list);
                            }
                        }
                    } else {
                        //setInfoMsg('No Data Found! Please Check Circular Code!!');
                        setInfoMsg('This circular number already saved with differnt type');
                        $("#circode").val('');
                        return;
                    }
                } else {
                    Logout();
                }
            }
        });
    });

    AddHandOverAccounts();
    function AddHandOverAccounts() {
        jQuery(".add-acc").unbind('click').click(function (evt) {
            evt.preventDefault();
            var circode = jQuery("#circode").val();
            //var valuefrom = jQuery("#valuefrom").val();
            //var valueto = jQuery("#valueto").val();
            var valuefrom = frmval;
            var valueto = toval;
            var percentage = jQuery("#percentage").val();
            var base = jQuery("#base").val();

            if ($("#rate").is(':checked') == true) {
                var rate = "true";

            }
            else {
                var rate = "false";
            }

            if (parseInt(valuefrom) > parseInt(valueto)) {
                setInfoMsg('From Value can not be greater than To Value!!');
                return;
            }

            frmval = "";
            frmval = jQuery("#valuefrom").val();
            //alert(frmval);
            valuefrom = numberWithCommas(jQuery("#valuefrom").val());
            $("#valuefrom").val(valuefrom);
            //alert(valuefrom);

            toval = "";
            toval = jQuery("#valueto").val();
            //alert(toval);
            valueto = numberWithCommas(jQuery("#valueto").val());
            $("#valueto").val(valueto);
            //alert(valueto);

            if (circode == "" || valuefrom == "" || valueto == "" || percentage == "" || base == "0" ) {
                setInfoMsg('Cannot Empty Fields!!');
                return;
            }

            //if ((percentage) < 0 || (percentage) > 100) {
            //    if (rate == "true") {
            //        setInfoMsg('Percentage Value can not be less than 0 and greater than 100 !!');
            //        return;
            //    }
            //}
            if (percentage > 100) {
                if (rate == "true") {
                    setInfoMsg('Percentage Value can not be less than 0 and greater than 100 !!');
                    return;
                }
            }

            if (parseInt(percentage) < 0 || parseInt(percentage) > 100) {
                if (rate == "true") {
                    setInfoMsg('Percentage Value can not be less than 0 and greater than 100 !!');
                    return;
                }
            }


            Lobibox.confirm({
                msg: "Do you want to add ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/DisregardAmount/AddMainDetails",
                            data: { Circode: circode, Valuefrom: valuefrom, Valueto: valueto, Percentage: percentage, Rate: rate, Base: base },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        updateMainDetails(result.list);
                                    } else {
                                        setInfoMsg(result.msg);
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
    function updateMainDetails(data) {
        jQuery('.acc-data-row .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {
                jQuery('.acc-data-row').append('<tr class="new-row">' +
                          '<td>' + data[i].hdvr_circular + '</td>' +
                          '<td align="right">' + numberWithCommas(data[i].hdvr_from_val) + '</td>' +
                          '<td align="right">' + numberWithCommas(data[i].hdvr_to_val) + '</td>' +
                          '<td align="right">' + data[i].hdvr_val + '</td>' +
                          '<td>' + data[i].hdvr_base + '</td>' +
                       '<td style="text-align:center;"><img class="delete-img remove-target-ovrt" src="../Resources/images/Remove.png"></td>' +
                        '</tr>');
            }
            RemoveAccountCode();
        }
    }

    function clearAll() {
        jQuery('.acc-data-row .new-row').remove();
        jQuery('.acc-data-rownew .new-row').remove();

        $("#circode").val('');
        $("#valuefrom").val('');
        $("#valueto").val('');
        $("#percentage").val('');

        $("#location").val('');
        $("#chanel").val('');


        //jQuery("#calmethod").val() = "-Select-";
        //jQuery("#pccat").val() = "-Select-";
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/DisregardAmount/ClearAll",
            data: {},
            success: function (result) {
                if (result.login == true) {

                } else {
                    Logout();
                }
            }
        });
    }

    function RemoveAccountCode() {
        jQuery(".remove-target-ovrt").unbind('click').click(function (evt) {
            evt.preventDefault();
            var td = jQuery(this).parent('td');
            var tr = jQuery(td).parent('tr');
            var circode = jQuery(tr).find('td:eq(0)').html();
            var valuefrom = jQuery(tr).find('td:eq(1)').html();
            var valueto = jQuery(tr).find('td:eq(2)').html();
            var percentage = jQuery(tr).find('td:eq(3)').html();
            var base = jQuery(tr).find('td:eq(4)').html();

            Lobibox.confirm({
                msg: "Do you want to remove ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/DisregardAmount/RemoveMainDetails",
                            data: { Circode: circode, Valuefrom: valuefrom, Valueto: valueto, Percentage: percentage, Base: base },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        updateMainDetails(result.list);

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

    jQuery(".btn-save-data").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to save ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    jQuery.ajax({
                        cache: false,
                        type: "GET",
                        url: "/DisregardAmount/SaveAllDetails",
                        data: {},
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    setSuccesssMsg(result.msg);
                                    clearAll();
                                } else {
                                    setError(result.msg);
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

    AddLocDetail();
    function AddLocDetail() {
        jQuery(".add-accnew").unbind('click').click(function (evt) {
            evt.preventDefault();
            var circode = jQuery("#circode").val();
            //var manager = jQuery("#manager").val();
            var location = jQuery("#location").val();
            var mnthfrom = jQuery("#mnthfrom").val();
            var mnthto = jQuery("#mnthto").val();
            var channel = jQuery("#chanel").val();


            //if (circode == "" || manager == "" || location == "" || mnthfrom == "" || mnthto == "") {
            if (circode == "" || mnthfrom == "" || mnthto == "" ) {
                setInfoMsg('Cannot Empty Fields!!');
                return;
            }

            if (location == "" && channel == "") {
                $("#location").val("");
                $("#chanel").val("");
                setInfoMsg('Please select channel or profit center');
                return;
            }

            if (location != "" && channel != "") {
                $("#location").val("");
                $("#chanel").val("");
                setInfoMsg('Please select only channel or profit center');
                return;
            }

            Lobibox.confirm({
                msg: "Do you want to add ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/DisregardAmount/AddLocDetails",
                            data: { Circode: circode, Channel: channel, Location: location, Mnthfrom: mnthfrom, Mnthto: mnthto },
                            //data: { Circode: circode, Manager: manager, Mnthfrom: mnthfrom, Mnthto: mnthto },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        updateLocDetail(result.list);
                                    } else {
                                        setInfoMsg(result.msg);
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
    function updateLocDetail(data) {
        jQuery('.acc-data-rownew .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {
                jQuery('.acc-data-rownew').append('<tr class="new-row">' +
                          '<td>' + jQuery("#circode").val() + '</td>' +
                          '<td>' + data[i].hdpd_channel + '</td>' +
                          '<td>' + data[i].hdpd_pc + '</td>' +
                          '<td>' + my_date_formatmonth(getFormatedDate1(data[i].hdpd_from_dt)) + '</td>' +
                          '<td>' + my_date_formatmonth(getFormatedDate1(data[i].hdpd_to_dt)) + '</td>' +
                       '<td style="text-align:center;"><img class="delete-img remove-target-ovrtnew" src="../Resources/images/Remove.png"></td>' +
                        '</tr>');
            }
            RemoveLocDetail();
        }
    }

    function RemoveLocDetail() {
        jQuery(".remove-target-ovrtnew").unbind('click').click(function (evt) {
            evt.preventDefault();
            var td = jQuery(this).parent('td');
            var tr = jQuery(td).parent('tr');
            var circode = jQuery(tr).find('td:eq(0)').html();
            var manager = jQuery(tr).find('td:eq(1)').html();
            //var location = jQuery(tr).find('td:eq(2)').html();
            var mnthfrom = jQuery(tr).find('td:eq(3)').html();
            var mnthto = jQuery(tr).find('td:eq(3)').html();


            Lobibox.confirm({
                msg: "Do you want to remove ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/DisregardAmount/RemoveLocDetails",
                            //data: { Circode: circode, Manager: manager, Location: location, Mnthfrom: mnthfrom, Mnthto: mnthto },
                            data: { Circode: circode, Manager: manager, Mnthfrom: mnthfrom, Mnthto: mnthto },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        updateLocDetail(result.list);

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
});