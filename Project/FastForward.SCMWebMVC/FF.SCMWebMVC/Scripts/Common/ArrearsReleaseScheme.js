jQuery(document).ready(function () {
    var x = 0;
    
    var tableControl = document.getElementById('mytable');
    var arrayOfValues = [];

    jQuery('#valuefrom').val(my_date_format(new Date()));
    jQuery('#valuefrom').datepicker({ dateFormat: "dd/M/yy" })

    jQuery('#valueto').val(my_date_format(new Date()));
    jQuery('#valueto').datepicker({ dateFormat: "dd/M/yy" })

    jQuery('#accfrom').val(my_date_format(new Date()));
    jQuery('#accfrom').datepicker({ dateFormat: "dd/M/yy" })

    jQuery('#accto').val(my_date_format(new Date()));
    jQuery('#accto').datepicker({ dateFormat: "dd/M/yy" })

    jQuery(".btn-clear-data").click(function (evt) {
        x = 0;
        //alert(x);
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to clear data ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    window.location.href = "/ArrearsReleaseScheme";
                }
            }
        });

    });

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

    jQuery(".btn-view-data").click(function (evt) {
        var Circode = jQuery("#circode").val();
        x = 1;
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/ArrearsReleaseScheme/LoadMainDetails",
            data: { Channel: jQuery("#chanel").val(), Location: jQuery("#location").val() },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.list != null) {
                            //set item data
                            if (result.list.length > 0) {
                                updateMainDetails(result.list);
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

    $('#rental').keypress(function (e) {
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
    jQuery(".defcode-search").click(function () {
        
        if (jQuery("#deftype").val() == 0) {
            setInfoMsg('Please Select the Definition Type');
        }
        if (jQuery("#deftype").val() == "SchemeType") {
            loadShemeType();
            //var headerKeys = Array()
            //headerKeys = ["Row", "Code", "Description"];
            //field = "SchemeType"
            //var x = new CommonSearch(headerKeys, field);

        }
        else if (jQuery("#deftype").val() == "Scheme") {
            loadSheme();
            //var headerKeys = Array()
            //headerKeys = ["Row", "Code", "Description"];
            //field = "Scheme"
            //var x = new CommonSearch(headerKeys, field);
        }
    });


    function getFormatedDate1(date) {
        var dte = new Date(parseInt(date.substr(6)));
        if (my_date_format_tran(dte) != "NaN/undefined/NaN")
            return my_date_format_tran(dte);
    }

    AddHandOverAccounts();
    function AddHandOverAccounts() {
        jQuery(".add-acc").unbind('click').click(function (evt) {
            evt.preventDefault();
            var deftype = jQuery("#deftype").val();
            var valuefrom = jQuery("#valuefrom").val();
            var valueto = jQuery("#valueto").val();
            var defcode = jQuery("#defcode").val();
            var rental = jQuery("#rental").val();
            var location = jQuery("#location").val();
            var channel = jQuery("#chanel").val();
            var accfrom = jQuery("#accfrom").val();
            var accto = jQuery("#accto").val();


            //new-------------------------
           // $('input:checkbox:checked', tableControl).each(function () {
            //    arrayOfValues.push($(this).closest('tr').find('td:eq(1)').text());
           // }).get();
            var abc
            $('input:checkbox:checked', tableControl).each(function () {
                abc = $(this).closest('tr').find('td:eq(1)').text();
                //alert(abc);
            });
            //alert(abc);
            //abc
            //----------------------------

            if (deftype == "" || valuefrom == "" || valueto == "" || defcode == "" || rental == "" || abc == undefined || accfrom == "" || accto == "") {
                setInfoMsg('Cannot Empty Fields!!');
                return;
            }
            //alert(abc);

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

            if (new Date(valuefrom) > new Date(valueto)) {
                setInfoMsg('From date can not be greater than To date!!');
                return;
            }

            if (new Date(accfrom) > new Date(accto)) {
                setInfoMsg('Account creation From date can not be greater than To date!!');
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
                        $('input:checkbox:checked', tableControl).each(function () {
                            defcode = $(this).closest('tr').find('td:eq(1)').text();
                            jQuery.ajax({
                                type: "GET",
                                url: "/ArrearsReleaseScheme/AddMainDetails",
                                data: { Deftype: deftype, Valuefrom: valuefrom, Valueto: valueto, Defcode: defcode, Rental: rental, Channel: channel, Location: location, dataset: abc, Accfrom: accfrom, Accto: accto },
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
                          '<td>' + data[i].hars_sch + '</td>' +
                          '<td>' + my_date_format(getFormatedDate1(data[i].hars_eff_from)) + '</td>' +
                          '<td>' + my_date_format(getFormatedDate1(data[i].hars_eff_to)) + '</td>' +
                          '<td align="right">' + numberWithCommas1(data[i].hars_no_rnt) + '</td>' +
                          '<td>' + data[i].hars_channel + '</td>' +
                          '<td>' + data[i].hars_pc + '</td>' +
                          '<td>' + my_date_format(getFormatedDate1(data[i].hars_acc_from)) + '</td>' +
                          '<td>' + my_date_format(getFormatedDate1(data[i].hars_acc_to)) + '</td>' +
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
            //var deftype = jQuery(tr).find('td:eq(0)').html();
            var defcode = jQuery(tr).find('td:eq(0)').html();
            var valuefrom = jQuery(tr).find('td:eq(1)').html();
            var valueto = jQuery(tr).find('td:eq(2)').html();
            var accfrom = jQuery(tr).find('td:eq(6)').html();
            var accto = jQuery(tr).find('td:eq(7)').html();

            Lobibox.confirm({
                msg: "Do you want to remove ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/ArrearsReleaseScheme/RemoveMainDetails",
                            data: { Valuefrom: valuefrom, Valueto: valueto, Defcode: defcode, Accfrom: accfrom, Accto: accto },
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

    function clearAll() {
        jQuery('.acc-data-row .new-row').remove();
        $("#defcode").val('');
        $("#rental").val('');

        //jQuery("#calmethod").val() = "-Select-";
        //jQuery("#pccat").val() = "-Select-";
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/ArrearsReleaseScheme/ClearAll",
            data: {},
            success: function (result) {
                if (result.login == true) {

                } else {
                    Logout();
                }
            }
        });
    }


    jQuery(".btn-save-data").click(function (evt) {
        if (x == 2) {
            setInfoMsg('Records already saved');
        }
        else {
            evt.preventDefault();
            Lobibox.confirm({
                msg: "Do you want to save ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            cache: false,
                            type: "GET",
                            url: "/ArrearsReleaseScheme/SaveAllDetails",
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
        }
    });

    //loadSheme();
    function loadSheme() {
        jQuery.ajax({
            type: "GET",
            url: "/Search/loadScheme",
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
                                        '<td>' + result.companyList[i].hsd_cd + '</td>' +
                                        '<td>' + result.companyList[i].hsd_desc + '</td>' +
                                        '</tr>');
                            }
                            jQuery(".select-company").click(function () {
                                //if (!jQuery('#chkmultiple').is(":checked")) {
                                //jQuery(".select-company").prop("checked", false);
                                if (jQuery(this).is(":checked")) {
                                    jQuery(this).prop("checked", true);
                                }
                                else
                                {
                                    jQuery(this).prop("checked", false);
                                }
                                //}
                                if (jQuery('[name="selectedcompany"]:checked').length == result.companyList.length) {
                                    jQuery("#chkAllCompany").prop("checked", true);
                                } else {
                                    jQuery("#chkAllCompany").prop("checked", false);
                                }
                            });

                        } else {
                            jQuery('.profcen-display-tbl').append('<tr class="new-row">' + "<td style='border:none; color: #ff6666; position: absolute; width: 80%; font-weight: bold;'>No company found for display</td>" + '</tr>');
                        }
                    } else {
                        jQuery('.profcen-display-tbl').append('<tr class="new-row">' + "<td style='border:none; color: #ff6666; position: absolute; width: 80%; font-weight: bold;'>No company found for display</td>" + '</tr>');
                    }
                } else {
                    Logout();
                }
            }
        });
    }

    function loadShemeType() {
        jQuery.ajax({
            type: "GET",
            url: "/Search/loadSchemeType",
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
                                        '<td>' + result.companyList[i].hst_cd + '</td>' +
                                        '<td>' + result.companyList[i].hst_desc + '</td>' +
                                        '</tr>');
                            }
                            jQuery(".select-company").click(function () {
                                //if (!jQuery('#chkmultiple').is(":checked")) {
                                //jQuery(".select-company").prop("checked", false);
                                if (jQuery(this).is(":checked")) {
                                    jQuery(this).prop("checked", true);
                                }
                                else {
                                    jQuery(this).prop("checked", false);
                                }
                                //}
                                if (jQuery('[name="selectedcompany"]:checked').length == result.companyList.length) {
                                    jQuery("#chkAllCompany").prop("checked", true);
                                } else {
                                    jQuery("#chkAllCompany").prop("checked", false);
                                }
                            });

                        } else {
                            jQuery('.profcen-display-tbl').append('<tr class="new-row">' + "<td style='border:none; color: #ff6666; position: absolute; width: 80%; font-weight: bold;'>No company found for display</td>" + '</tr>');
                        }
                    } else {
                        jQuery('.profcen-display-tbl').append('<tr class="new-row">' + "<td style='border:none; color: #ff6666; position: absolute; width: 80%; font-weight: bold;'>No company found for display</td>" + '</tr>');
                    }
                } else {
                    Logout();
                }
            }
        });
    }

    jQuery("#chkAllCompany").click(function () {
        //if (jQuery('#chkmultiple').is(":checked")) {
            if (jQuery('#chkAllCompany').is(":checked")) {
                jQuery(".select-company").prop("checked", true);
            } else {
                jQuery(".select-company").prop("checked", false);
            }
       // }
    });
    //jQuery("#chkAllCompany").attr("disabled", true);
    jQuery("#chkmultiple").click(function () {
        if (jQuery('#chkmultiple').is(":checked")) {
            jQuery("#chkAllCompany").removeAttr("disabled");
        } else {
            jQuery("#chkAllCompany").attr("disabled", true);
            jQuery("#chkAllCompany").prop("checked", false);
            jQuery(".select-company").prop("checked", false);
        }
    });

});