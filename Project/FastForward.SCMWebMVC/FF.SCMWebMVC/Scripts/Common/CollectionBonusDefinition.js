jQuery(document).ready(function () {

    jQuery('#startdate').val(my_date_format(new Date()));
    jQuery('#startdate').datepicker({ dateFormat: "dd/M/yy" })

    jQuery('#shstartdate').val(my_date_format(new Date()));
    jQuery('#shstartdate').datepicker({ dateFormat: "dd/M/yy" })

    var tableControl = document.getElementById('mytable');
    var arrayOfValues = [];
    var frmval;
    var toval;
    var frmarr;
    var toarr;

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

    $('#taccount').keypress(function (e) {
        var regex = new RegExp("^[0-9.]+$");
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        if (regex.test(str)) {
            return true;
        }

        e.preventDefault();
        return false;
    });

    $('#bonusp').keypress(function (e) {
        var regex = new RegExp("^[0-9.]+$");
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        if (regex.test(str)) {
            return true;
        }

        e.preventDefault();
        return false;
    });

    $('#fromdate').keypress(function (e) {
        var regex = new RegExp("^[0-9.]+$");
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        if (regex.test(str)) {
            return true;
        }

        e.preventDefault();
        return false;
    });

    $('#todate').keypress(function (e) {
        var regex = new RegExp("^[0-9.]+$");
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        if (regex.test(str)) {
            return true;
        }

        e.preventDefault();
        return false;
    });

    $('#arrfpercentage').keypress(function (e) {
        var regex = new RegExp("^[0-9.]+$");
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        if (regex.test(str)) {
            return true;
        }

        e.preventDefault();
        return false;
    });

    $('#arrtpercentage').keypress(function (e) {
        var regex = new RegExp("^[0-9.]+$");
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        if (regex.test(str)) {
            return true;
        }

        e.preventDefault();
        return false;
    });

    $('#accfbalance').keypress(function (e) {
        var regex = new RegExp("^[0-9.]+$");
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        if (regex.test(str)) {
            return true;
        }

        e.preventDefault();
        return false;
    });

    $('#acctbalance').keypress(function (e) {
        var regex = new RegExp("^[0-9.]+$");
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        if (regex.test(str)) {
            return true;
        }

        e.preventDefault();
        return false;
    });

    jQuery("#arrfpercentage").focusout(function (evt) {
        frmarr = "";
        frmarr = jQuery("#arrfpercentage").val();
        var fromarr = numberWithCommas(jQuery("#arrfpercentage").val());
        $("#arrfpercentage").val(fromarr);
    });

    jQuery("#arrtpercentage").focusout(function (evt) {
        toarr = "";
        toarr = jQuery("#arrtpercentage").val();
        var tovarr = numberWithCommas(jQuery("#arrtpercentage").val());
        $("#arrtpercentage").val(tovarr);
    });

    jQuery("#accfbalance").focusout(function (evt) {
        frmval = "";
        frmval = jQuery("#accfbalance").val();
        var fromvalue = numberWithCommas(jQuery("#accfbalance").val());
        $("#accfbalance").val(fromvalue);
    });

    jQuery("#acctbalance").focusout(function (evt) {
        toval = "";
        toval = jQuery("#acctbalance").val();
        var tovalue = numberWithCommas(jQuery("#acctbalance").val());
        $("#acctbalance").val(tovalue);
    });
    //jQuery('#fromdate').val(my_date_format(new Date()));
    //jQuery('#fromdate').datepicker({ dateFormat: "dd/M/yy" })

    //jQuery('#todate').val(my_date_format(new Date()));
    //jQuery('#todate').datepicker({ dateFormat: "dd/M/yy" })

    //jQuery('#valueto').val(my_date_format(new Date()));
    //jQuery('#valueto').datepicker({ dateFormat: "dd/M/yy" })

    jQuery(".btn-clear-data").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to clear data ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    //clearAll();
                    window.location.href = "/CollectionBonusDefinition";
                }
            }   
        });

    });

    jQuery(".circular-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "circular code"];
        field = "circularcbd"
        var x = new CommonSearch(headerKeys, field);
    });

    function clearAll() {
        jQuery('.acc-data-row .new-row').remove();
        $("#circode").val('');
        $("#pccat").val('');
        $("#taccount").val('');
        $("#bonusp").val('');
        $("#fromdate").val('');
        $("#todate").val('');
        $("#arrfpercentage").val('');
        $("#arrtpercentage").val('');
        $("#accfbalance").val('');
        $("#acctbalance").val('');

        //jQuery("#calmethod").val() = "-Select-";
        //jQuery("#pccat").val() = "-Select-";
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/CollectionBonusDefinition/ClearAll",
            data: {},
            success: function (result) {
                if (result.login == true) {

                } else {
                    Logout();
                }
            }
        });
    }

    $('#pccat').focus(function () {
        LoadPCCat();
    });

    jQuery(".btn-view-data").click(function (evt) {
        var Circode = jQuery("#circode").val();
        if (jQuery("#circode").val() == "") {
            setInfoMsg('Please select the circular number!!');
            return;
        }
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/CollectionBonusDefinition/LoadMainDetails",
            data: { Circode: jQuery("#circode").val() },
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

    function LoadPCCat() {
        jQuery.ajax({
            type: "GET",
            url: "/CollectionBonusDefinition/LoadPCCat",
            data: { invType: jQuery("form.frm-inv-det #Sah_inv_tp").val() },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("pccat");
                        jQuery("#pccat").empty();
                        var options = [];
                        var option = document.createElement('option');
                        if (result.data != null && result.data.length != 0) {
                            for (i = 0; i < result.data.length; i++) {
                                option.text = result.data[i].Text;
                                option.value = result.data[i].Value;
                                options.push(option.outerHTML);
                            }
                        } else {
                            option.text = "Select Type";
                            option.value = "";
                            options.push(option.outerHTML);
                        }
                        select.insertAdjacentHTML('beforeEnd', options.join('\n'));
                    } else {
                        setError(result.msg);
                    }
                } else {
                    Logout();
                }
            }
        })
    }
    function getFormatedDate1(date) {
        var dte = new Date(parseInt(date.substr(6)));
        if (my_date_format_tran(dte) != "NaN/undefined/NaN")
            return my_date_format_tran(dte);
    }

    AddHandOverAccounts();
    function AddHandOverAccounts() {
        jQuery(".add-acc").unbind('click').click(function (evt) {
            evt.preventDefault();
            var circode = jQuery("#circode").val();
            var startdate = jQuery("#startdate").val();
            var pccat = jQuery("#pccat").val();
            var taccount = jQuery("#taccount").val();
            var bonusp = jQuery("#bonusp").val();
            var fromdate = jQuery("#fromdate").val();
            var todate = jQuery("#todate").val();
            var shstartdate = jQuery("#shstartdate").val();
            //var arrfpercentage = jQuery("#arrfpercentage").val();
            //var arrtpercentage = jQuery("#arrtpercentage").val();
            //var accfbalance = jQuery("#accfbalance").val();
            //var acctbalance = jQuery("#acctbalance").val();
            var arrfpercentage = frmarr;
            var arrtpercentage = toarr;
            var accfbalance = frmval;
            var acctbalance = toval;
            var channel;

            var abvalue;

            if ($("#chkBefore").is(':checked') == true) {
                var abvalue = "BEFORE";
            }
            else
            {
                var abvalue = "AFTER";
            }

            var abc;
            $('input:checkbox:checked', tableControl).each(function () {
                abc = $(this).closest('tr').find('td:eq(1)').text();
                //alert(abc);
            });

            if (circode == "" || startdate == "" || taccount == "" || bonusp == "" || fromdate == "" || todate == "" || arrfpercentage == "" || arrtpercentage == "" || accfbalance == "" || acctbalance == "" ) {
                setInfoMsg('Cannot Empty Fields!!');
                return;
            }

            if (pccat == "" || abc == undefined)
            {
                setInfoMsg('Please select profit center category and channel');
                return;
            }

            if (fromdate > todate) {
                setInfoMsg('From year can not be greater than To year!!');
                return;
            }

            if (bonusp > 100) {
                setInfoMsg('Percentage Value can not be less than 0 and greater than 100 !!');
                return;
            }
            if (arrfpercentage > 100) {
                setInfoMsg('Percentage Value can not be less than 0 and greater than 100 !!');
                return;
            }
            if (arrtpercentage > 100) {
                setInfoMsg('Percentage Value can not be less than 0 and greater than 100 !!');
                return;
            }

            if (parseInt(bonusp) < 0 || parseInt(bonusp) > 100) {
                setInfoMsg('Percentage Value can not be less than 0 and greater than 100 !!');
                return;
            }
            if (parseInt(arrfpercentage) < 0 || parseInt(arrfpercentage) > 100) {
                setInfoMsg('Arrears Percentage Value can not be less than 0 and greater than 100 !!');
                return;
            }
            if (parseInt(arrtpercentage) < 0 || parseInt(arrtpercentage) > 100) {
                setInfoMsg('Arrears Percentage Value can not be less than 0 and greater than 100 !!');
                return;
            }
            if (parseInt(arrfpercentage) > parseInt(arrtpercentage)) {
                setInfoMsg('From arrears value can not be greater than To arrears value!!');
                return;
            }

            if (parseInt(accfbalance) > parseInt(acctbalance)) {
                setInfoMsg('From acc. balance can not be greater than To acc. balance!!');
                return;
            }
            if (abc != undefined) {
                Lobibox.confirm({
                    msg: "Do you want to add ?",
                    callback: function ($this, type, ev) {
                        if (type == "yes") {
                            $('input:checkbox:checked', tableControl).each(function () {
                                channel = $(this).closest('tr').find('td:eq(1)').text();
                                jQuery.ajax({
                                    type: "GET",
                                    url: "/CollectionBonusDefinition/AddMainDetails",
                                    data: {
                                        Circode: circode, Startdate: startdate, Pccat: pccat, Taccount: taccount, Bonusp: bonusp, Fromdate: fromdate, Todate: todate, Arrfpercentage: arrfpercentage, Arrtpercentage: arrtpercentage, Accfbalance: accfbalance, Acctbalance: acctbalance, Channel: channel, ShStartdate: shstartdate, ABvalue: abvalue
                                    },
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
            }
            else
            {
                Lobibox.confirm({
                    msg: "Do you want to add ?",
                    callback: function ($this, type, ev) {
                        if (type == "yes") {                         
                                jQuery.ajax({
                                    type: "GET",
                                    url: "/CollectionBonusDefinition/AddMainDetails",
                                    data: {
                                        Circode: circode, Startdate: startdate, Pccat: pccat, Taccount: taccount, Bonusp: bonusp, Fromdate: fromdate, Todate: todate, Arrfpercentage: arrfpercentage, Arrtpercentage: arrtpercentage, Accfbalance: accfbalance, Acctbalance: acctbalance, Channel: "", ShStartdate: shstartdate
                                    },
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
            }

        });
    } 
    function updateMainDetails(data) {
        jQuery('.acc-data-row .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {
                jQuery('.acc-data-row').append('<tr class="new-row">' +
                          '<td>' + data[i].hbp_circular + '</td>' +
                          '<td>' + my_date_format(getFormatedDate1(data[i].hbp_strdate)) + '</td>' +
                          '<td>' + data[i].hbp_pccat + '</td>' +
                          '<td align="right">' + numberWithCommas1(data[i].hbp_taccount) + '</td>' +
                          '<td align="right">' + numberWithCommas1(data[i].hbp_bnsper) + '</td>' +
                          '<td align="right">' + numberWithCommas1(data[i].hbp_sr_fyear) + '</td>' +
                          '<td align="right">' + numberWithCommas1(data[i].hbp_sr_tyear) + '</td>' +
                          '<td align="right">' + numberWithCommas(data[i].hbp_from_arrper) + '</td>' +
                          '<td align="right">' + numberWithCommas(data[i].hbp_to_arrper) + '</td>' +
                          '<td align="right">' + numberWithCommas(data[i].hbp_from_bal) + '</td>' +
                          '<td align="right">' + numberWithCommas(data[i].hbp_to_bal) + '</td>' +
                          '<td>' + data[i].hbp_channel + '</td>' +
                          '<td>' + my_date_format(getFormatedDate1(data[i].hbp_shstrdate)) + '</td>' +
                          '<td>' + data[i].hbp_shstrab + '</td>' +
                       '<td style="text-align:center;"><img class="delete-img remove-target-ovrt" src="../Resources/images/Remove.png"></td>' +
                        '</tr>');
            }
            RemoveAccountCode();
        }
    }

    jQuery("#circode").focusout(function (evt) {
        var manager = $(this).val();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/CollectionBonusDefinition/LoadMainDetails",
            data: { Circode: jQuery("#circode").val() },
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
                        setInfoMsg('No Data Found! Please Check circular Code!!');
                        return;
                    }
                } else {
                    Logout();
                }
            }
        });
    });

    function RemoveAccountCode() {
        jQuery(".remove-target-ovrt").unbind('click').click(function (evt) {
            evt.preventDefault();
            var td = jQuery(this).parent('td');
            var tr = jQuery(td).parent('tr');
            var circode = jQuery(tr).find('td:eq(0)').html();
            var startdate = jQuery(tr).find('td:eq(1)').html();
            var pccat = jQuery(tr).find('td:eq(2)').html();
            //var valueto = jQuery(tr).find('td:eq(3)').html();

            Lobibox.confirm({
                msg: "Do you want to remove ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/CollectionBonusDefinition/RemoveMainDetails",
                            data: { Circode: circode, Startdate: startdate, Pccat: pccat },
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
                        url: "/CollectionBonusDefinition/SaveAllDetails",
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
    loadChannel();
    function loadChannel() {
        jQuery.ajax({
            type: "GET",
            url: "/Search/loadChannel",
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
                                        '<td>' + result.companyList[i].loc_hirch_cd + '</td>' +
                                        '<td>' + result.companyList[i].loc_hirch_desc + '</td>' +
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

});