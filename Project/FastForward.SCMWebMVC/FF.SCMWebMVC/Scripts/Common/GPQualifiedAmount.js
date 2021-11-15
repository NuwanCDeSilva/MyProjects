jQuery(document).ready(function () {

    jQuery('#mnthfrom').val(my_date_formatmonth(new Date()));
    jQuery('#mnthfrom').datepicker({ dateFormat: "M/yy" })

    jQuery('#mnthto').val(my_date_formatmonth(new Date()));
    jQuery('#mnthto').datepicker({ dateFormat: "M/yy" })

    var frmval;
    var toval;

    jQuery(".btn-view-data").click(function (evt) {
        var Circode = jQuery("#circode").val();
        if (jQuery("#circode").val() == "") {
            setInfoMsg('Please select the circular number!!');
            return;
        }
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/GPQualifiedAmount/LoadLocDetails",
            data: { Circode: jQuery("#circode").val() },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.list != null) {
                            //set item data
                            if (result.list.length > 0) {
                                updateLocDetail(result.list);
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

    $('#rate').click(function () {
        if (this.checked) {
            $(this).attr("value", "true");
        } else {
            $(this).attr("value", "false");
        }
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

    $('#valuefrom').keypress(function (e) {
        var regex = new RegExp("^[0-9.]+$");
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        if (regex.test(str)) {
            return true;
        }

        e.preventDefault();
        return false;
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


    $('#valueto').keypress(function (e) {
        var regex = new RegExp("^[0-9.]+$");
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        if (regex.test(str)) {
            return true;
        }

        e.preventDefault();
        return false;
    });

    jQuery(".loc-search").click(function () {
        if (jQuery("#manager").val() == "") {
            setInfoMsg('Please Select Manager');
        }
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description", "Address", "Chanel"];
        field = "locationmg"
        var data = {
            mgr: jQuery("#manager").val()
        }
        var x = new CommonSearch(headerKeys, field, data);
    });
    jQuery(".mgr-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Epf No", "Category", "First Name", "Last Name", "NIC"];
        field = "manager"
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery(".circular-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "circular code"];
        field = "circular"
        var data = {
            Cat: 1
        }
        var x = new CommonSearch(headerKeys, field,data);
    });
    
    jQuery("#circode").focusout(function (evt) {
        var manager = $(this).val();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/GPQualifiedAmount/LoadMainDetails",
            data: { Circode: jQuery("#circode").val() , Cat: 1 },
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
                        //setInfoMsg('No Data Found! Please Check PC Code!!');   
                        setInfoMsg('This circular number already saved with differnt type');
                        //jQuery('#circode').focus();
                        $("#circode").val('');
                        return;
                        
                    }
                } else {
                    Logout();
                }
            }
        });
    });

    function getFormatedDate1(date) {
        var dte = new Date(parseInt(date.substr(6)));
        if (my_date_format_tran(dte) != "NaN/undefined/NaN")
            return my_date_format_tran(dte);
    }

    jQuery(".btn-clear-data").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to clear data ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    window.location.href = "/GPQualifiedAmount";
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
            else{
                var rate = "false";
            }

            if (parseInt(valuefrom) > parseInt(valueto)) {
            //if (parseInt(strfrom) > parseInt(strto)) {
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
            //alert(rate);

            if (circode == "" || valuefrom == "" || valueto == "" || percentage == "" || base == "0") {
                setInfoMsg('Cannot Empty Fields!!');
                return;
            }
            if (percentage > 100) {
                if (rate == "true") {
                    setInfoMsg('Percentage Value can not be less than 0 and greater than 100 !!');
                    return;
                }
            }
            //if ((percentage) < 0 || (percentage) > 100) {
            //    if (rate == "true") {
            //        setInfoMsg('Percentage Value can not be less than 0 and greater than 100 !!');
            //        return;
            //    }
            //}
            if (parseInt(percentage) < 0 || parseInt(percentage) > 100) {
                if (rate == "true") {
                    setInfoMsg('Percentage Value can not be less than 0 and greater than 100 !!');
                    return;
                }
            }

            ////var str = valuefrom;
            //var strfrom = parseFloat(valuefrom.replace(',', '').replace(' ', ''))
            //alert(strfrom);
            //var strto = parseFloat(valueto.replace(',', '').replace(',', '').replace(',', ''))

            //alert(strto);
            ////if (parseInt(valuefrom) > parseInt(valueto)) {
            //if (parseInt(strfrom) > parseInt(strto)) {
            //    setInfoMsg('From Value can not be greater than To Value!!');
            //    return;
            //}
            Lobibox.confirm({
                msg: "Do you want to add ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/GPQualifiedAmount/AddMainDetails",
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
                          '<td align="right">' + numberWithCommas1(data[i].hdvr_val) + '</td>' +
                          '<td>' + data[i].hdvr_base + '</td>' +
                       '<td style="text-align:center;"><img class="delete-img remove-target-ovrt" src="../Resources/images/Remove.png"></td>' +
                        '</tr>');
            }
            RemoveAccountCode();
        }
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

            Lobibox.confirm({
                msg: "Do you want to remove ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/GPQualifiedAmount/RemoveMainDetails",
                            data: { Circode: circode, Valuefrom: valuefrom, Valueto: valueto, Percentage: percentage },
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

    AddLocDetail();
    function AddLocDetail() {
        jQuery(".add-accnew").unbind('click').click(function (evt) {
            evt.preventDefault();
            var circode = jQuery("#circode").val();
            var manager = jQuery("#manager").val();
            //var location = jQuery("#location").val();
            var mnthfrom = jQuery("#mnthfrom").val();
            var mnthto = jQuery("#mnthto").val();

            //alert(mnthfrom);
            //{ dateFormat: "MM yy" }

            //var d = new Date(jQuery("#mnthfrom").val());
            //var n = d.getFullYear();
            //alert(n);
            ////var date = new Date();
            ////var firstDay = new Date(date.getFullYear(), date.getMonth(), 1);

            ////var date = $("#mnthfrom").datepicker("getDate");
            //var monthfrom = $("#mnthfrom").datepicker('getDate').getMonth();
            //var yearfrom = $("#mnthfrom").datepicker('getDate').getFullYear();
            //alert(monthfrom);
            //alert(yearfrom);

            //var monthto = $("#mnthto").datepicker('getDate').getMonth();
            //var yearto = $("#mnthto").datepicker('getDate').getFullYear();


            //if (new Date(mnthfrom) > new Date(mnthto))
            //{
            //    setInfoMsg('From date can not be greater than To date fdfsfdff!!');
            //    return;
            //}
            //var qqq = mnthfrom.toString();
            
            //if (mnthfrom.toString() > mnthto.toString()) {
                //alert(qqq);
            //    setInfoMsg('From date can not be greater than To date!!');
            //    return;
            //}

            //if (circode == "" || manager == "" || location == "" || mnthfrom == "" || mnthto == "") {
            if (circode == "" || manager == "" || mnthfrom == "" || mnthto == "") {
                setInfoMsg('Cannot Empty Fields!!');
                return;
            }
            Lobibox.confirm({
                msg: "Do you want to add ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/GPQualifiedAmount/AddLocDetails",
                            //data: { Circode: circode, Manager: manager, Location: location, Mnthfrom: mnthfrom, Mnthto: mnthto },
                            data: { Circode: circode, Manager: manager, Mnthfrom: mnthfrom, Mnthto: mnthto },
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
                          '<td>' + data[i].hdpd_manager + '</td>' +
                          //'<td>' + data[i].hdpd_pc + '</td>' +
                          '<td>' + my_date_formatmonth(getFormatedDate1(data[i].hdpd_from_dt)) + '</td>' +
                          '<td>' + my_date_formatmonth(getFormatedDate1(data[i].hdpd_to_dt)) + '</td>' +
                       '<td style="text-align:center;"><img class="delete-img remove-target-ovrtnew" src="../Resources/images/Remove.png"></td>' +
                        '</tr>');
            }
            RemoveLocDetail();
        }
    }

    function clearAll() {
        jQuery('.acc-data-row .new-row').remove();
        jQuery('.acc-data-rownew .new-row').remove();
        $("#manager").val('');
        //$("#location").val('');
        $("#circode").val('');
        $("#valuefrom").val('');
        $("#valueto").val('');
        $("#percentage").val('');

        //jQuery("#calmethod").val() = "-Select-";
        //jQuery("#pccat").val() = "-Select-";
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/GPQualifiedAmount/ClearAll",
            data: {},
            success: function (result) {
                if (result.login == true) {

                } else {
                    Logout();
                }
            }
        });
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
                            url: "/GPQualifiedAmount/RemoveLocDetails",
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

    jQuery(".btn-save-data").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to save ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    jQuery.ajax({
                        cache: false,
                        type: "GET",
                        url: "/GPQualifiedAmount/SaveAllDetails",
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
});