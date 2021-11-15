jQuery(document).ready(function () {
    jQuery('#date').val(my_date_format(new Date()));
    jQuery('#date').datepicker({ dateFormat: "dd/M/yy" })

    jQuery('#bmonthdate').val(my_date_formatmonth(new Date()));
    jQuery('#bmonthdate').datepicker({ dateFormat: "MM yy" })

    //add-acc
    AddHandOverAccounts();
    function AddHandOverAccounts() {
        jQuery(".add-acc").unbind('click').click(function (evt) {
            evt.preventDefault();
            var date = jQuery("#date").val();
            var bonusmonth = jQuery("#bmonthdate").val();
            var location = jQuery("#location").val();
            var accno = jQuery("#accno").val();
            var rejamm = jQuery("#rejammount").val();

            if (date == "" || bonusmonth == "" || location == "" || accno == "" || rejamm == "") {
                setInfoMsg('Cannot Empty Fields!!');
                return;
            }
            Lobibox.confirm({
                msg: "Do you want to add ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/HandingOverAccounts/AddHandOverAccounts",
                            data: { Date: date, BonusMonth: bonusmonth,Location: location, AccNo: accno, RejectAmm: rejamm },
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
        jQuery('.acc-data-row .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {
                jQuery('.acc-data-row').append('<tr class="new-row">' +
                          '<td>' + data[i].Hhoa_pc + '</td>' +
                          '<td>' + data[i].Hhoa_ac + '</td>' +
                           '<td>' + my_date_formatmonth(getFormatedDate1(data[i].Hhoa_bonus_month)) + '</td>' +
                         '<td style="color: black; font-size: x-small;text-align:right">' + ReplaceNumberWithCommas(data[i].Hhoa_rej_lmt) + '</td>' +
                          '<td style="color: black; font-size: x-small;text-align:right">' + ReplaceNumberWithCommas(data[i].Hhoa_rej_lmt) + '</td>' +
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
            var loc = jQuery(tr).find('td:eq(0)').html();
            var account = jQuery(tr).find('td:eq(1)').html();
            var month = jQuery(tr).find('td:eq(2)').html();
            var ammont = jQuery(tr).find('td:eq(3)').html();

            Lobibox.confirm({
                msg: "Do you want to remove ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/HandingOverAccounts/RemoveAccountCode",
                            data: { PC: loc, Account: account, Month: month, Ammount: ammont },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true)
                                    {
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
    var mainpc = "";
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
        mainpc = jQuery("#location").val();
    });
    jQuery(".accno-search").click(function (evt) {

        if (jQuery("#location").val()=="")
        {
            setInfoMsg("Please Select Location");
            return;
        }

        var headerKeys = Array();
        headerKeys = ["Row", "Account Code"];
        field = "HandAcc";
        var data = {
            pc: jQuery("#location").val(),
            date: jQuery("#bmonthdate").val()
        };
        var x = new CommonSearch(headerKeys, field, data);
    });
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
                        url: "/HandingOverAccounts/SaveHandAccount",
                        data: { },
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
    function clearAll()
    {
        jQuery("#location").val("");
        jQuery("#locationdesc").val("");
        jQuery("#accno").val("");
        jQuery("#accno").val("");
        jQuery('.acc-data-row .new-row').remove();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/HandingOverAccounts/ClearAll",
            data: {},
            success: function (result) {
            }
        });
    }
    jQuery(".btn-view-data").click(function (evt) {
        if ($("#chkop1").is(':checked') == true) {
            var bal = "1";
        }
        if (jQuery("#location").val()=="")
        {
            setInfoMsg('Please Select PC');
            return;
        }

        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/HandingOverAccounts/LoadHandOverDetails",
            data: { Pc: jQuery("#location").val(), Isbalance: bal },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.list.length > 0) {
                            //set item data
                            if (result.list.length > 0) {
                                updateHandingOverAccountsLOAD(result.list);
                            } 
                        } else {
                            setInfoMsg('No Data Found!');
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
    function updateHandingOverAccountsLOAD(data) {
        jQuery('.acc-data-row .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {
                jQuery('.acc-data-row').append('<tr class="new-row">' +
                          '<td>' + data[i].Hhoa_pc + '</td>' +
                          '<td>' + data[i].Hhoa_ac + '</td>' +
                           '<td>' + my_date_formatmonth(getFormatedDate1(data[i].Hhoa_bonus_month)) + '</td>' +
                         '<td style="color: black; font-size: x-small;text-align:right">' + ReplaceNumberWithCommas(data[i].Hhoa_rej_lmt) + '</td>' +
                          '<td style="color: black; font-size: x-small;text-align:right">' + ReplaceNumberWithCommas(data[i].Hhoa_avl_bal) + '</td>' +
                       '<td style="text-align:center;"><img class="delete-img remove-target-ovrt" src="../Resources/images/Remove.png"></td>' +
                        '</tr>');
            }
            RemoveAccountCode();
        }
    }
    jQuery(".btn-clear-data").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to clear data ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    window.location.href = "/HandingOverAccounts";
                }
            }
        });

    });

    //
    $('#rejammount').focusout(function () {
        var str = $(this).val();
        var numRange = /^[0-9]\d*(\.\d+)?$/;

        if (!jQuery.isNumeric(str)) {
            setInfoMsg('Please enter a valid Amount !!!');
            $(this).val("");
            return;
        } else {
            if (Number(str) < 0) {
                setInfoMsg('Please Enter Valid Amount !!!');
                $(this).val("");
                return;
            }
        }
        var fromvalue = numberWithCommas(jQuery("#rejammount").val());
        $("#rejammount").val(fromvalue);
    });
    //$('#location').focusout(function () {
    //    var str = $(this).val();
    //        jQuery("#accno").val("");
    //        jQuery("#rejammount").val("");
    //});
    function ReplaceNumberWithCommas(yourNumber) {
        //Seperates the components of the number
        var n = yourNumber.toString().split(".");
        if(n.length==1)
        {
            yourNumber = yourNumber.toString() + ".00";
        }
        n = yourNumber.toString().split(".");

        //Comma-fies the first part
        n[0] = n[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        //Combines the two sections
       
        return n.join(".");
    }
    profitcenterFocusout();
    function profitcenterFocusout() {
        $('#location').focusout(function () {
            var pc = $(this).val();
            jQuery.ajax({
                cache: false,
                type: "GET",
                url: "/Search/getProfitCenters",
                data: { pgeNum: "1", pgeSize: "10", searchFld: "Code", searchVal: pc },
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.data.length > 0) {

                                for (i = 0 ; i < result.data.length ;i++)
                                {
                                    if (result.data[i].Mpc_cd == jQuery("#location").val())
                                    {
                                        jQuery("#locationdesc").val(result.data[i].Mpc_desc);
                                    }
                                }
                              
                            }
                        } else {
                            setInfoMsg('Invalid Profi Center!!');
                            jQuery("#location").val("");
                            jQuery("#location").focus();
                            return;
                        }
                    } else {
                        Logout();
                    }
                }
            });
        });
    }
    AccountCodeFocusOut();

    function AccountCodeFocusOut() {
        $('#accno').focusout(function () {


            if (jQuery("#location").val() == "") {
                setInfoMsg('Please Select Location!!!');
                jQuery("#accno").val("");
                jQuery("#location").focus();
                return;
            }

            var acc = $(this).val();
            jQuery.ajax({
                cache: false,
                type: "GET",
                url: "/Search/HandOverAccCodeSearch",
                data: { pgeNum: "1", pgeSize: "10", searchFld: "Account Code", searchVal: acc, pc: jQuery("#location").val(), date: jQuery("#date").val() },
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.data.length > 0) {
                            }
                        } else {
                            setInfoMsg('Invalid Account Code!!');
                            jQuery("#accno").val("");
                            return;
                        }
                    } else {
                        Logout();
                    }
                }
            });
        });
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
    jQuery(".btn-excel-upload").unbind('click').click(function (evt) {
        evt.preventDefault();
        jQuery('#exclupload').modal({
            keyboard: false,
            backdrop: 'static'
        }, 'show');
    });
    jQuery(".cls-excel-popup").unbind('click').click(function (evt) {
        $('#exclupload').modal('hide');
    });
    jQuery('.imprt-cd-data').click(function (e) {
        if (jQuery('#UploadedFile').val() == "") {
            setInfoMsg("Please Sellect File");
        } else {
            Lobibox.confirm({
                msg: "Do you want to continue process?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        //var formCollection = jQuery("#imp-data").serialize();
                        //jQuery.ajax({
                        //    cache: false,
                        //    type: "POST",
                        //    contentType: false,
                        //    processData: false,
                        //    async: true,
                        //    url: "/HandingOverAccounts/BindExceldata",
                        //    data: formCollection,
                        //    success: function (result) {
                        //        if (result.login == true) {
                        //            if (result.success == true) {
                        //                updateHandingOverAccounts(result.list);
                        //            } else {
                        //                setInfoMsg(result.msg);
                        //            }
                        //        } else {
                        //            Logout();
                        //        }
                        //    }
                        //});

                        var form = $('#imp-data')[0];
                        var dataString = new FormData(form);
                        $.ajax({
                            url: '/HandingOverAccounts/BindExceldata',  //Server script to process data
                            type: 'POST',
                            xhr: function () {  // Custom XMLHttpRequest
                                var myXhr = $.ajaxSettings.xhr();
                                if (myXhr.upload) { // Check if upload property exists
                                    //myXhr.upload.onprogress = progressHandlingFunction
                                    myXhr.upload.addEventListener('progress', progressHandlingFunction,
                                    false); // For handling the progress of the upload
                                }
                                return myXhr;
                            },
                            //Ajax events
                            // Form data
                            data: dataString,
                            //Options to tell jQuery not to process data or worry about content-type.
                            cache: false,
                            contentType: false,
                            processData: false,
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        updateHandingOverAccounts(result.list);
                                        $('#exclupload').modal('hide');
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
    function progressHandlingFunction(e) {
        if (e.lengthComputable) {
            var percentComplete = Math.round(e.loaded * 100 / e.total);
            $("#FileProgress").css("width",
            percentComplete + '%').attr('aria-valuenow', percentComplete);
            $('#FileProgress span').text(percentComplete + "%");
        }
        else {
            $('#FileProgress span').text('unable to compute');
        }
    }
});