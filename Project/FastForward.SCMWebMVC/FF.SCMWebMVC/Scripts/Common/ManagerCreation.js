jQuery(document).ready(function () {
    jQuery('#fdate').val(my_date_format(new Date()));
    jQuery('#fdate').datepicker({ dateFormat: "dd/M/yy" })

    //jQuery('#srdate').val(my_date_format(new Date()));
    var date = new Date();
    jQuery('#srdate').val(my_date_format(new Date(date.getFullYear(), date.getMonth(), 1)));
    jQuery('#srdate').datepicker({ dateFormat: "dd/M/yy" })

    jQuery('#bmonthdate').val(my_date_formatmonth(new Date()));
    jQuery('#bmonthdate').datepicker({ dateFormat: "MM yy" })

    //add-acc
    AddHandOverAccounts();
    function AddHandOverAccounts() {
        jQuery(".add-acc").unbind('click').click(function (evt) {
            evt.preventDefault();

            var date1 = new Date(jQuery("#fdate").val());
            //alert(date1);

            var day1 = $("#fdate").datepicker('getDate').getDate();
            var month1 = $("#fdate").datepicker('getDate').getMonth();
            if (day1 > 15) {
                jQuery('#bmonthdate').val(my_date_formatmonth(new Date(date1.getFullYear(), month1 + 1)));
                jQuery('#bmonthdate').datepicker({ dateFormat: "MM yy" })

                jQuery('#srdate').val(my_date_format(new Date(date1.getFullYear(), month1, 1)));
                jQuery('#srdate').datepicker({ dateFormat: "dd/M/yy" })
            }
            else {
                jQuery('#bmonthdate').val(my_date_formatmonth(new Date(date1.getFullYear(), month1 )));
                jQuery('#bmonthdate').datepicker({ dateFormat: "MM yy" })

                jQuery('#srdate').val(my_date_format(new Date(date1.getFullYear(), month1, 1)));
                jQuery('#srdate').datepicker({ dateFormat: "dd/M/yy" })
            }

            

            var fdate = jQuery("#fdate").val();
            var bonusmonth = jQuery("#bmonthdate").val();
            var srdate = jQuery("#srdate").val();
            var Manager = jQuery("#Manager").val();
            var Mgrname = jQuery("#Mgrname").val();
            var location = jQuery("#location").val();
            var calmethod = jQuery("#calmethod").val();
            var pccat = jQuery("#pccat").val();
            var mainlocation = jQuery("#mainlocation").val();
            //var accno = jQuery("#accno").val();
            //var rejamm = jQuery("#rejammount").val();

            if (fdate == "" || bonusmonth == "" || srdate == "" || location == "" || Manager == "" || calmethod == "0" || pccat=="" || mainlocation == "") {
                setInfoMsg('Cannot Empty Fields!!');
                return;
            }
            Lobibox.confirm({
                msg: "Do you want to add ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/ManagerCreation/AddHandOverAccounts",
                            data: { Date: fdate, BonusMonth: bonusmonth, Srdate: srdate, Location: location, Manager: Manager, Calmethod: calmethod, Pccat: pccat, Mgrname: Mgrname, Mainlocation: mainlocation},
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        updateHandingOverAccounts(result.list);
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
    function updateHandingOverAccounts(data) {
        var chk = "<input type='checkbox' id='chk_" + i + "' />";
        jQuery('.acc-data-row .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {
                var status = data[i].hmfa_act_stus;
                var chk;
                if (status == true) {
                    chk = "<input type='checkbox' id='chk_" + i + "' checked />";
                }
                else {
                    chk = "<input type='checkbox' id='chk_" + i + "'/>";
                }
                jQuery('.acc-data-row').append('<tr class="new-row">' +
                          '<td>' + data[i].hmfa_pc + '</td>' +
                          '<td>' + data[i].hmfa_mgr_cd + '</td>' +
                          '<td>' + data[i].hmfa_mgr_name + '</td>' +
                          '<td>' + data[i].hmfa_bonus_method + '</td>' +
                          '<td>' + data[i].hmfa_pc_cat + '</td>' +                         
                          '<td>' + my_date_format(getFormatedDate1(data[i].hmfa_sr_open_dt)) + '</td>' +
                          '<td>' + my_date_formatmonth(getFormatedDate1(data[i].hmfa_bonus_st_dt)) + '</td>' +
                          '<td>' + my_date_format(getFormatedDate1(data[i].hmfa_acc_dt)) + '</td>' +
                          '<td>' + data[i].hmfa_mainpc + '</td>' +
                         '<td>' + chk + '</td>' +
                       '<td style="text-align:center;"><img class="delete-img remove-target-ovrt" src="../Resources/images/Remove.png"></td>' +
                        '</tr>');
            }
            RemoveAccountCode();
        }
    }



    function updatemanagerdetail(data) {      
        //var chk = "<input type='checkbox' id='chk_" + i + "' />";
       // var chk = "<input type='checkbox' id='chk_" + i + "' checked=" + data[i].hmfa_act_stus + " />";
        jQuery('.acc-data-row .new-row').remove();
        if (data.length > 0) {
            
            for (i = 0; i < data.length; i++) {
                var status = data[i].hmfa_act_stus;
                var chk;
                if (status == true) {
                    chk = "<input type='checkbox' id='chk_" + i + "' checked />";
                }
                else {
                    chk = "<input type='checkbox' id='chk_" + i + "'/>";
                }
                jQuery('.acc-data-row').append('<tr class="new-row">' +
                          '<td>' + data[i].hmfa_pc + '</td>' +
                          '<td>' + data[i].hmfa_mgr_cd + '</td>' +
                          '<td>' + data[i].hmfa_mgr_name + '</td>' +
                          '<td>' + data[i].hmfa_bonus_method + '</td>' +
                          '<td>' + data[i].hmfa_pc_cat + '</td>' +
                          '<td>' + my_date_format(getFormatedDate1(data[i].hmfa_sr_open_dt)) + '</td>' +
                          '<td>' + my_date_formatmonth(getFormatedDate1(data[i].hmfa_bonus_st_dt)) + '</td>' +
                          '<td>' + my_date_format(getFormatedDate1(data[i].hmfa_acc_dt)) + '</td>' +
                          '<td>' + data[i].hmfa_mainpc + '</td>' +
                         '<td>' + chk + '</td>' +
                       '<td style="text-align:center;"><img class="delete-img remove-target-ovrt" src="../Resources/images/Remove.png"></td>' +
                        '</tr>');
            }
            RemoveAccountCode();
        }
    }

    $(function () {
        gridCheckboxes('GridChk');

        //get selection
        $('#btnGetSelection1').click(function () {
            var arr = $('#GridChk [name=id]:checked').map(function () {
                return $(this).val();
            }).get();

            $('#log1').html(JSON.stringify(arr));
        });
    });

    function RemoveAccountCode() {
        jQuery(".remove-target-ovrt").unbind('click').click(function (evt) {
            evt.preventDefault();
            var td = jQuery(this).parent('td');
            var tr = jQuery(td).parent('tr');
            var loc = jQuery(tr).find('td:eq(0)').html();
            var manager = jQuery(tr).find('td:eq(1)').html();
            var month = jQuery(tr).find('td:eq(6)').html();
            var ammont = jQuery(tr).find('td:eq(3)').html();

            Lobibox.confirm({
                msg: "Do you want to remove ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/ManagerCreation/RemoveAccountCode",
                            data: { PC: loc, Manager: manager, Month: month, Ammount: ammont },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        updateHandingOverAccounts(result.list);

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
    //jQuery(".loc-search").click(function (evt) {
    //    var headerKeys = Array();
    //    headerKeys = ["Row", "Code", "Description"];
    //    field = "srchPCM2";
    //    var data = {
    //        chnl: "",
    //        sChnl: "",
    //        area: "",
    //        regn: "",
    //        zone: "",
    //        type: "PC"
    //    };
    //    var x = new CommonSearch(headerKeys, field, data);
    //});

    //jQuery(".loc-search").click(function () {
    //    var headerKeys = Array()
    //    headerKeys = ["Row", "Code", "Description", "Address", "Chanel"];
    //    field = "ProfitCenter1"
    //    var x = new CommonSearch(headerKeys, field);
    //});

    jQuery(".loc-search").click(function (evt) {
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

    jQuery(".mloc-search").click(function (evt) {
        var headerKeys = Array();
        headerKeys = ["Row", "Code", "Description"];
        field = "srchPCM4M";
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
    jQuery(".mgr-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Epf No", "Category", "First Name", "Last Name", "NIC"];
        field = "employee1"
        var x = new CommonSearch(headerKeys, field);
    });
    //jQuery(".accno-search").click(function (evt) {

    //    if (jQuery("#location").val() == "") {
    //        setInfoMsg("Please Select Location");
    //        return;
    //    }

    //    var headerKeys = Array();
    //    headerKeys = ["Row", "Account Code"];
    //    field = "HandAcc";
    //    var data = {
    //        pc: jQuery("#location").val(),
    //        date: jQuery("#bmonthdate").val()
    //    };
    //    var x = new CommonSearch(headerKeys, field, data);
    //});
    function getFormatedDate1(date) {
        var dte = new Date(parseInt(date.substr(6)));
        if (my_date_format_tran(dte) != "NaN/undefined/NaN")
            return my_date_format_tran(dte);
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
                        url: "/ManagerCreation/SaveHandAccount",
                        data: {},
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    setSuccesssMsg(result.msg);
                                    clearAll();
                                    //window.location.href = "/ManagerCreation";
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

    function clearAll() {
        //jQuery('.bonus-loc-table .new-row').remove();
        jQuery('.acc-data-row .new-row').remove();
        $("#Manager").val('');
        $("#location").val('');
        $("#mainlocation").val('');

        //jQuery("#calmethod").val() = "-Select-";
        //jQuery("#pccat").val() = "-Select-";
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/ManagerCreation/ClearAll",
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

    function LoadPCCat() {
        jQuery.ajax({
            type: "GET",
            url: "/ManagerCreation/LoadPCCat",
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


    jQuery("#Manager").focusout(function (evt) {
        var manager = $(this).val();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/ManagerCreation/LoadHandOverDetails",
            data: { Manager: jQuery("#Manager").val() },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.list != null) {
                            //set item data
                            if (result.list.length > 0) {
                                updatemanagerdetail(result.list);
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

    jQuery(".btn-view-data").click(function (evt) {
        var pc = $(this).val();
        if ($("#chkop1").is(':checked') == true) {
            var bal = "1";
        }
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/ManagerCreation/LoadHandOverDetails",
            data: { Manager: jQuery("#Manager").val() },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.list != null) {
                            //set item data
                            if (result.list.length > 0) {
                                updatemanagerdetail(result.list);
                            }
                        }
                    } else {
                        setInfoMsg('No Data Found! Please Check Manager Code!!');
                        return;
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    jQuery(".btn-clear-data").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to clear data ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    window.location.href = "/ManagerCreation";
                }
            }
        });

    });
});