jQuery(document).ready(function () {
    jQuery('#fromdate').val(my_date_format_with_time(new Date()));
    jQuery('#fromdate').datepicker({ dateFormat: "dd/M/yy" })
    jQuery('#todate').val(my_date_format_with_time(new Date()));
    jQuery('#todate').datepicker({ dateFormat: "dd/M/yy" })
    jQuery('#elmonth').val(my_date_formatmonth(new Date()));
    jQuery('#elmonth').datepicker({ dateFormat: "MM yy" });
    jQuery(".profit-center-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description", "Address", "Chanel"];
        field = "ProfitCenter"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".el-profit-center-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description", "Address", "Chanel"];
        field = "ELProfitCenter"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".el-add-profit-center-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description", "Address", "Chanel"];
        field = "ELProfitCenteradd"
        var x = new CommonSearch(headerKeys, field);
    });
    profitcenterFocusout();
    function profitcenterFocusout() {
        $('#ProfitCenter').focusout(function () {
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
                            }
                        } else {
                            setInfoMsg('Invalid Profi Center!!');
                            jQuery("#ProfitCenter").val("");
                            jQuery("#ProfitCenter").focus();
                            return;
                        }
                    } else {
                        Logout();
                    }
                }
            });
        });
    }
    jQuery(".add-pc").click(function () {
        var pc = jQuery("#ProfitCenter").val();
        var angtarget = "0";
        if ($("#chkavgtarget").is(':checked') == true)
        {
            angtarget = "1";
        }
        if (pc == "") {
            setInfoMsg('Please Select PC');
        } else {
            jQuery.ajax({
                cache: false,
                type: "GET",
                url: "/CommissionDefinition/AddProfitCenters",
                data: { proficenter: jQuery("#ProfitCenter").val(), AVGTarget: angtarget },
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.data.length > 0) {
                                //set grid
                                SetPcGrid(result.data);
                                jQuery("#ProfitCenter").val("");
                            }
                        } else {
                            setInfoMsg(result.msg);
                        }
                    } else {
                        Logout();
                    }
                }
            });
        }
    });
    function SetPcGrid(data) {
        jQuery('.pc-row .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {
                jQuery('.pc-row').append('<tr class="new-row">' +
                        '<td>' + data[i].pccode + '</td>' +
                        '<td>' + data[i].avgtarget + '</td>' +
                       '<td style="text-align:center;"><img class="delete-img remove-pccd" src="../Resources/images/Remove.png"></td>' +
                        '</tr>');
            }
            RemovePCCodeFunction();
        }
    }
    function RemovePCCodeFunction() {
        jQuery(".delete-img.remove-pccd").unbind('click').click(function (evt) {
            evt.preventDefault();
            var td = jQuery(this).parent('td');
            var tr = jQuery(td).parent('tr');
            var pccd = jQuery(tr).find('td:eq(0)').html();
            Lobibox.confirm({
                msg: "Do you want to remove ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/CommissionDefinition/RemovePCCode",
                            data: { profitcenter: pccd },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        SetPcGrid(result.data);
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
    $('#settperiod').focusout(function () {
        var str = $(this).val();
        var numRange = /^[0-9+]+$/;
        if (!numRange.test(str)) {
            setInfoMsg('Please enter a valid Period !!!');
            $(this).val("");
        }
    });
    $('#invtype').focusout(function () {
        var pc = $(this).val();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/Search/getInvoiceType",
            data: { pgeNum: "1", pgeSize: "10", searchFld: "Code", searchVal: pc },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.data.length > 0) {
                        }
                    } else {
                        setInfoMsg('Invalid Invoice Type!!');
                        jQuery("#invtype").val("");
                        jQuery("#invtype").focus();
                        return;
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    jQuery(".emp-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Epf No", "Category", "First Name", "Last Name", "NIC"];
        field = "employee"
        var x = new CommonSearch(headerKeys, field);
    });
    //jQuery(".emp-search").click(function () {
    //    var headerKeys = Array()
    //    headerKeys = ["Row", "Category"];
    //    field = "employeecat"
    //    var x = new CommonSearch(headerKeys, field);
    //});

    $('#overemp').focusout(function () {
        var emp = $(this).val();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/Search/getEmployeeCategory",
            data: { pgeNum: "1", pgeSize: "10", searchFld: "Category", searchVal: emp },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.data.length > 0) {
                        }
                    } else {
                        setInfoMsg('Invalid Employee Cat!!');
                        jQuery("#overemp").val("");
                        jQuery("#overemp").focus();
                        return;
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    //$('#overcommission').focusout(function () {
    //    var str = $(this).val();
    //    var numRange = /^[0-9]\d*(\.\d+)?$/;
    //    if (!numRange.test(str)) {
    //        setInfoMsg('Please enter a valid Commission !!!');
    //        $(this).val("");
    //    //} else {
    //    //    if (Number(str) > 100) {
    //    //        setInfoMsg('Cannot Exceed 100% !!!');
    //    //        $(this).val("");
    //    //    }
    //    }
    //});
    $('#overcommission').focusout(function () {
        var str = $(this).val();
        var numRange = /^[0-9]\d*(\.\d+)?$/;

        if (!jQuery.isNumeric(str)) {
            setInfoMsg('Please enter a valid Commission !!!');
            $(this).val("");
        } else {
            //if (Number(str) > 100) {
            //    setInfoMsg('Cannot Exceed 100% !!!');
            //    $(this).val("");
            //}
        }
    });
    jQuery(".add-over-comm").click(function () {
        var emp = jQuery("#overemp").val();
        var epf = jQuery(".efp-cd").html();
        var comper = jQuery("#overcommission").val();
        var stdays = jQuery("#overstperiod").val();
        var enddates = jQuery("#overendpeiod").val();
        if ($("#chkoverride").is(':checked') == false)
        {
            var overd = 0;
        } else {
            var overd = 1;
        }

        if ($("#checkisinvrtbtu").is(':checked') == false) {
            var invert = 0;
        } else {
            var invert = 1;
        }
        if (emp == "") {
            setInfoMsg('Please Select Employee');
        } else if (comper == "") {
            setInfoMsg('Please Enter Commission');
        }
        else {
            jQuery.ajax({
                cache: false,
                type: "GET",
                url: "/CommissionDefinition/AddOverrideEmployee",
                data: { employee: emp, commission: comper, Epf: epf, StartDate: stdays, EndDates: enddates, Ovrt: overd, invbtu: invert },
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.data.length > 0) {
                                //set grid
                                SetOverrideGrid(result.data);
                                jQuery("#overemp").val("");
                                jQuery("#overcommission").val("");
                            }
                        } else {
                            setInfoMsg(result.msg);
                        }
                    } else {
                        Logout();
                    }
                }
            });
        }
    });
    function SetOverrideGrid(data) {
        jQuery('.over-comm-row .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {
                jQuery('.over-comm-row').append('<tr class="new-row">' +
                        '<td>' + data[i].Epf + '</td>' +
                          '<td>' + data[i].startdays + "-" + data[i].enddays + '</td>' +
                           '<td>' + data[i].Ovrt + '</td>' +
                         '<td>' + data[i].Commission + '</td>' +
                       '<td style="text-align:center;"><img class="delete-img remove-override-emp" src="../Resources/images/Remove.png"></td>' +
                        '</tr>');
            }
            RemoveOverrideEmpFunction();
        }
    }
    function RemoveOverrideEmpFunction() {
        jQuery(".remove-override-emp").unbind('click').click(function (evt) {
            evt.preventDefault();
            var td = jQuery(this).parent('td');
            var tr = jQuery(td).parent('tr');
            var empcd = jQuery(tr).find('td:eq(0)').html();
            var range = jQuery(tr).find('td:eq(1)').html();
            var comm = jQuery(tr).find('td:eq(3)').html();
            Lobibox.confirm({
                msg: "Do you want to remove ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/CommissionDefinition/RemoveEmpCode",
                            data: { EmpCode: empcd, Daterange: range, comm: comm },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        SetOverrideGrid(result.data);
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
    jQuery(".search-item-cd").click(function (evt) {
        var headerKeys = Array();
        headerKeys = ["Row", "Code", "Description"];
        field = "srchItemsM";
        var data = {
            chnl: jQuery("#ItemCode").val(),
            type: "sub_channel"
        }
        var x = new CommonSearch(headerKeys, field, data);
    });
    $('#itemcode').focusout(function () {
        var pc = $(this).val();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/Search/getItems",
            data: { pgeNum: "1", pgeSize: "10", searchFld: "Code", searchVal: pc },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.data.length > 0) {
                        }
                    } else {
                        setInfoMsg('Invalid Item Code!!');
                        jQuery("#itemcode").val("");
                        jQuery("#itemcode").focus();
                        return;
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    jQuery(".search-model-cd").click(function (evt) {
        var headerKeys = Array();
        headerKeys = ["Row", "Code", "Description"];
        field = "srchItemModelM";
        var x = new CommonSearch(headerKeys, field);
    });
    $('#model').focusout(function () {
        var pc = $(this).val();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/Search/getItemModel",
            data: { pgeNum: "1", pgeSize: "10", searchFld: "Code", searchVal: pc },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.data.length > 0) {
                        }
                    } else {
                        setInfoMsg('Invalid Model Code!!');
                        jQuery("#model").val("");
                        jQuery("#model").focus();
                        return;
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    jQuery(".search-brand-cd").click(function (evt) {
        var headerKeys = Array();
        headerKeys = ["Row", "Code", "Description"];
        var data = jQuery("#BrandMngr").val();
        field = "srchItemBrandM";
        var resData = new CommonSearch(headerKeys, field, data);
    });
    $('#brand').focusout(function () {
        var pc = $(this).val();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/Search/getItemBrands",
            data: { pgeNum: "1", pgeSize: "10", searchFld: "Code", searchVal: pc },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.data.length > 0) {
                        }
                    } else {
                        setInfoMsg('Invalid Brand Code!!');
                        jQuery("#brand").val("");
                        jQuery("#brand").focus();
                        return;
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    jQuery(".search-cat1-cd").click(function (evt) {
        var headerKeys = Array();
        headerKeys = ["Row", "Code", "Description"];
        field = "srchMainCatM";
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".search-btu_cat").click(function (evt) {
        var headerKeys = Array();
        headerKeys = ["Row", "Code", "Description"];
        field = "srchMainCatM2";
        var x = new CommonSearch(headerKeys, field);
    });
    $('#cat1').focusout(function () {
        var pc = $(this).val();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/Search/getMainCategory",
            data: { pgeNum: "1", pgeSize: "10", searchFld: "Code", searchVal: pc },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.data.length > 0) {
                        }
                    } else {
                        setInfoMsg('Invalid Cat 1 Code!!');
                        jQuery("#cat1").val("");
                        jQuery("#cat1").focus();
                        return;
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    jQuery(".search-cat2-cd").click(function (evt) {
        var headerKeys = Array();
        headerKeys = ["Row", "Code", "Description"];
        field = "srchCategory2M";
        var data = {
            chnl: jQuery("#SaleType").val(),
            sChnl: jQuery("#ItemCode").val(),
            type: "Category2"
        };
        var x = new CommonSearch(headerKeys, field, data);
    });
    jQuery(".search-inv-tp").click(function (evt) {
        var headerKeys = Array();
        headerKeys = ["Row", "Code", "Description"];
        field = "InvType";
        var x = new CommonSearch(headerKeys, field);
    });
    $('#cat2').focusout(function () {
        var pc = $(this).val();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/Search/getCategory2",
            data: { pgeNum: "1", pgeSize: "10", searchFld: "Code", searchVal: pc },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.data.length > 0) {
                        }
                    } else {
                        setInfoMsg('Invalid Cat 2 Code!!');
                        jQuery("#cat2").val("");
                        jQuery("#cat2").focus();
                        return;
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    $('#minvalue').focusout(function () {
        var str = $(this).val();
        var numRange = /^[0-9]\d*(\.\d+)?$/;
        if (!numRange.test(str)) {
            setInfoMsg('Please enter a valid Value !!!');
            $(this).val("");
        }
        if (jQuery("#maxtvalue").val() != "") {
            if (Number(str) > Number(jQuery("#maxtvalue").val())) {
                setInfoMsg('Cannot Exceed Max Value !!!');
                $(this).val("");
            }
        }

    });
    $('#maxtvalue').focusout(function () {
        var str = $(this).val();
        var numRange = /^[0-9]\d*(\.\d+)?$/;
        if (!numRange.test(str)) {
            setInfoMsg('Please enter a valid Value !!!');
            $(this).val("");
        }
        if (jQuery("#minvalue").val() != "") {
            if (Number(str) < Number(jQuery("#minvalue").val())) {
                setInfoMsg('Cannot be less than Min Value !!!');
                $(this).val("");
            }
        }

    });
    $('#settstartdays').focusout(function () {
        var str = $(this).val();
        var numRange = /^[0-9]\d*(\.\d+)?$/;
        if (!numRange.test(str)) {
            setInfoMsg('Please enter a valid Days !!!');
            $(this).val("");
        }
        if (jQuery("#settenddays").val() != "") {
            if (Number(str) > Number(jQuery("#settenddays").val())) {
                setInfoMsg('Cannot Exceed EndDays !!!');
                $(this).val("");
            }
        }

    });
    $('#settenddays').focusout(function () {
        var str = $(this).val();
        var numRange = /^[0-9]\d*(\.\d+)?$/;
        if (!numRange.test(str)) {
            setInfoMsg('Please enter a valid Dyas !!!');
            $(this).val("");
        }
        if (jQuery("#settstartdays").val() != "") {
            if (Number(str) < Number(jQuery("#settstartdays").val())) {
                setInfoMsg('Cannot be less than Start Days !!!');
                $(this).val("");
            }
        }

    });
    $('#valcommisiion').focusout(function () {
        var str = $(this).val();
        var numRange = /^[0-9]\d*(\.\d+)?$/;
       
        if (!jQuery.isNumeric(str)) {
            setInfoMsg('Please enter a valid Commission !!!');
            $(this).val("");
        } else {
            //if (Number(str) > 100) {
            //    setInfoMsg('Cannot Exceed 100% !!!');
            //    $(this).val("");
            //}
        }
    });
    jQuery(".add-val-comm").click(function () {
        var code = jQuery("#itemcode").val();
        var model = jQuery("#model").val();
        var brand = jQuery("#brand").val();
        var cat1 = jQuery("#cat1").val();
        var cat2 = jQuery("#cat2").val();
        var minval = jQuery("#minvalue").val();
        var maxval = jQuery("#maxtvalue").val();
        var sttlmntend = jQuery("#settenddays").val();
        var sttlmntstart = jQuery("#settstartdays").val();
        var commission = jQuery("#valcommisiion").val();
        var invtype = jQuery("#invtype").val();

        if (minval == "" && maxval == "") {
            setInfoMsg('Please Select Value Range!!');
        } else if (sttlmntend == "" && sttlmntstart=="")
        {
            setInfoMsg('Please Select Settlement Date Range!!');
        } else if (commission=="")
        {
            setInfoMsg('Please Select Commission!!');
        }
        else {
            jQuery.ajax({
                cache: false,
                type: "GET",
                url: "/CommissionDefinition/AddItemValueRange",
                data: { Itemcode: code, Model: model, Brand: brand, Cat1: cat1, Cat2: cat2, MinVal: minval, MaxVal: maxval, Stlstart: sttlmntstart, Stlend: sttlmntend, Commission: commission, InvType:invtype },
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.data.length > 0) {
                                //set grid
                                SetItemValues(result.data);
                                jQuery("#itemcode").val("");
                                jQuery("#model").val("");
                                jQuery("#brand").val("");
                                jQuery("#cat1").val("");
                                jQuery("#cat2").val("");
                                jQuery("#minvalue").val("");
                                jQuery("#maxtvalue").val("");
                                jQuery("#valcommisiion").val("");
                                jQuery("#settenddays").val("");
                                jQuery("#settstartdays").val("");
                            }
                        } else {
                            setInfoMsg(result.msg);
                        }
                    } else {
                        Logout();
                    }
                }
            });
        }
    });
    function SetItemValues(data) {
        jQuery('.commi-item-table .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {
                jQuery('.commi-item-table').append('<tr class="new-row">' +
                        '<td>' + data[i].ItemCode + '</td>' +
                         '<td>' + data[i].ItemModel + '</td>' +
                         '<td>' + data[i].ItemBrand + '</td>' +
                         '<td>' + data[i].Cat1 + '</td>' +
                         '<td>' + data[i].Cat2 + '</td>' +
                          '<td>' + data[i].InvType + '</td>' +
                         '<td>' + data[i].Range + '</td>' +
                         '<td>' + data[i].SettlRange + '</td>' +
                         '<td>' + data[i].Commission + '</td>' +
                       '<td style="text-align:center;"><img class="delete-img remove-item-range" src="../Resources/images/Remove.png"></td>' +
                        '</tr>');
            }
            RemoveItemValueFunction();
        }
    }
    function RemoveItemValueFunction() {
        jQuery(".remove-item-range").unbind('click').click(function (evt) {
            evt.preventDefault();
            var td = jQuery(this).parent('td');
            var tr = jQuery(td).parent('tr');
            var itemcode = jQuery(tr).find('td:eq(0)').html();
            var model = jQuery(tr).find('td:eq(1)').html();
            var brand = jQuery(tr).find('td:eq(2)').html();
            var cat1 = jQuery(tr).find('td:eq(3)').html();
            var cat2 = jQuery(tr).find('td:eq(4)').html();
            var invtype = jQuery(tr).find('td:eq(5)').html();
            var valrange = jQuery(tr).find('td:eq(6)').html();
            var daterange = jQuery(tr).find('td:eq(7)').html();
            Lobibox.confirm({
                msg: "Do you want to remove ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/CommissionDefinition/RemoveItemValueRange",
                            data: { ItemCode: itemcode, Model: model, Brand: brand, Cat1: cat1, Cat2: cat2, valrange: valrange, daterange: daterange, invtype: invtype },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        SetItemValues(result.data);
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
    jQuery(".btn-clear-data").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to clear data ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    window.location.href = "/CommissionDefinition";
                }
            }
        });

    });
    jQuery(".btn-comm-save-data").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to save ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    var fromdate = jQuery("#fromdate").val();
                    var todate = jQuery("#todate").val();
                    var setlperiod = 0; //jQuery("#settperiod").val();
                    var commicode = jQuery("#commcode").val();
                    if ($("#rdbsale").is(':checked') == true) {
                        var saletype = "SALE";
                    }
                    if ($("#rdbgp").is(':checked') == true) {
                        var saletype = "GP";
                    }
                    if ($("#rdbqty").is(':checked') == true) {
                        var saletype = "QTY";
                    }
                    if ($("#rddiliver").is(':checked') == true) {
                        var salemode = "DELI";
                    }
                    if ($("#rdbwithrev").is(':checked') == true) {
                        var salemode = "REV";
                    }
                    if ($("#rdbtotinv").is(':checked') == true) {
                        var salemode = "INV";
                    }
                    if ($("#chkqtywise").is(':checked') == true) {
                        var commitype = "QTYWISE";
                    }
                    if ($("#chkvaluewise").is(':checked') == true) {
                        var commitype = "VALWISE";
                    }
                    if ($("#chkactive").is(':checked') == true) {
                        var active = "1";
                    } else {
                        var active = "0";
                    }
                    if ($("#rdcollrec").is(':checked') == true) {
                        var collecttype = "REC";
                    } else {
                        var collecttype = "INV";
                    }
                    jQuery.ajax({
                        cache: false,
                        type: "GET",
                        url: "/CommissionDefinition/SaveCommissionDefinition",
                        data: { FromDate: fromdate, ToDate: todate, Period: setlperiod, SaleType: saletype, SaleMode: salemode, CommiType: commitype, CommissionCode: commicode, Active: active,CollectType:collecttype },
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
    jQuery(".commission-search").click(function (evt) {
        var headerKeys = Array();
        headerKeys = ["Row", "Code"];
        field = "CommissionCode";
        var x = new CommonSearch(headerKeys, field);
    });
    $('#commcode').focusout(function () {
        var pc = $(this).val();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/CommissionDefinition/LoadDetails",
            data: { CommCode: jQuery("#commcode").val() },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.hdrdata.length > 0)
                        {
                            // set hdr field
                            SetHdrData(result.hdrdata);
                            //set item data
                            if (result.Item_value_commission.length>0)
                            {
                                SetItemValues(result.Item_value_commission);
                            }
                            if (result.Ov_commission.length > 0)
                            {
                                SetOverrideGrid(result.Ov_commission);
                            }
                            if (result.pclist.length>0)
                            {
                                SetPcGrid(result.pclist);
                            }
                            if (result.targets.length > 0)
                            {
                                updateTargetBaseCommission(result.targets);
                            }
                            if (result.targetsovt.length > 0) {
                                updateTargetBaseOvt(result.targetsovt);
                            }
                            if (result.collectovt.length > 0) {
                                updateCollectionBaseOverride(result.collectovt);
                            }
                            if (result._eli_comm.length > 0) {
                                SetEliteItemValues(result._eli_comm);
                            }
                            if (result._eli_add.length > 0) {
                                SetEliteAddValues(result._eli_add);
                            }
                        }
                    } else {
                        setInfoMsg('No Data Found! Please Check Commission Code!!');
                        return;
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    function SetHdrData(data)
    {
        jQuery("#fromdate").val(getFormatedDate1(data[0].Rch_from_dt));
        jQuery("#todate").val(getFormatedDate1(data[0].Rch_to_dt));
        //jQuery("#settperiod").val(data[0].Rch_settl_period);
        if (data[0].Rch_calc_type == "SALE")
        {
            $("#rdbsale").attr('checked', true);
        } else if (data[0].Rch_calc_type == "GP")
        {
            $("#rdbgp").attr('checked', true);
        } else
        {
            $("#rdbqty").attr('checked', true);
        }

        if (data[0].Rch_sales_type == "DELI")
        {
            $("#rddiliver").attr('checked', true);
        } else if (data[0].Rch_sales_type == "REV")
        {
            $("#rdbwithrev").attr('checked', true);
        } else
        {
            $("#rdbtotinv").attr('checked', true);
        }

        if (data[0].Rch_comm_type == "QTYWISE")
        {
            $("#chkqtywise").attr('checked', true);
        } else {
            $("#chkvaluewise").attr('checked', true);
        }
        if (data[0].Rch_act == "1") {
            $("#chkactive").attr('checked', true);
        } else {
            $("#chkactive").attr('checked', false);
        }
        if (data[0].Rch_collect_tp == "REC" || data[0].Rch_collect_tp == null) {
            $("#rdcollrec").attr('checked', true);
        } else {
            $("#rdcollinv").attr('checked', true);
        }
    }
    function getFormatedDate1(date) {
        var dte = new Date(parseInt(date.substr(6)));
        if (my_date_format_tran(dte) != "NaN/undefined/NaN")
            return my_date_format_tran(dte);
    }

    function clearAll()
    {
        jQuery('.pc-row .new-row').remove();
        jQuery('.over-comm-row .new-row').remove();
        jQuery('.commi-item-table .new-row').remove();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/CommissionDefinition/ClearAll",
            data: { },
            success: function (result) {
                if (result.login == true)
                {
                   
                } else {
                    Logout();
                }
            }
        });
    }
    jQuery(".channel-search").click(function (evt) {
        var headerKeys = Array();
        headerKeys = ["Row", "Code", "Description"];
        field = "srchChnl";
        var data = {
            Company:"",
            type: "channel"
        }
        var x = new CommonSearch(headerKeys, field, data);
    });
    $('#chanel').focusout(function () {
        var chanal = $(this).val();
        var avg = "0";
        if ($("#chkavgtarget").is(':checked') == true)
        {
            avg = "1";
        }
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/CommissionDefinition/GetPC",
            data: { Chanal: chanal, AVG: avg },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.pclist.length > 0) {
                            SetPcGrid(result.pclist);
                        }
                    } else {
                        setInfoMsg('Invalid Chanal Code!!');
                        jQuery("#chanel").val("");
                        jQuery("#chanel").focus();
                        return;
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    jQuery("#chkistargetbaserate").unbind('click').click(function (evt) {
        evt.preventDefault();
        jQuery('#addChgcdPop').modal({
            keyboard: false,
            backdrop: 'static'
        }, 'show');
        $('#st_range_val').focusout(function ()
        {
            var str = $(this).val();
            var numRange = /^[0-9]\d*(\.\d+)?$/;

            if (!jQuery.isNumeric(str))
            {
                setInfoMsg('Please enter a valid range !!!');
                $(this).val("");
            }
        });
        $('#end_range_val').focusout(function () {
            var str = $(this).val();
            var numRange = /^[0-9]\d*(\.\d+)?$/;

            if (!jQuery.isNumeric(str)) {
                setInfoMsg('Please enter a valid range !!!');
                $(this).val("");
            }
        });
        $('#comm_rate').focusout(function () {
            var str = $(this).val();
            var numRange = /^[0-9]\d*(\.\d+)?$/;

            if (!jQuery.isNumeric(str)) {
                setInfoMsg('Please enter a valid Commission !!!');
                $(this).val("");
            } else {
                if (Number(str) > 100) {
                    setInfoMsg('Cannot Exceed 100% !!!');
                    $(this).val("");
                }
            }
        });
        //Add function
        TargetBaseCommissionAdd();
        jQuery(".search-exc").click(function () {
            var headerKeys = Array()
            headerKeys = ["Row", "Epf No", "Category", "First Name", "Last Name", "NIC"];
            field = "employee4"
            var x = new CommonSearch(headerKeys, field);
        });
    });
  
    function TargetBaseCommissionAdd() {
        jQuery(".add-target-comm").unbind('click').click(function (evt) {
            evt.preventDefault();
            var st_val = jQuery("#st_range_val").val();
            var end_val = jQuery("#end_range_val").val();
            var comm_rate = jQuery("#comm_rate").val();
            var exec = jQuery("#exc_cd").val();
            if (st_val == "" || end_val == "" || comm_rate=="")
            {
                setInfoMsg('Cannot Empty Fields!!');
                return;
            }


            Lobibox.confirm({
                msg: "Do you want to add ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/CommissionDefinition/TargetBaseCommissionAdd",
                            data: { Stval: st_val, Endval: end_val, Rate: comm_rate , Exec:exec},
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        updateTargetBaseCommission(result.list);
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
    function updateTargetBaseCommission(data) {
        jQuery('.target-base-comm .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {
                jQuery('.target-base-comm').append('<tr class="new-row">' +
                     '<td>' + data[i].rct_anal1 + '</td>' +
                          '<td>' + data[i].rct_st_val + "-" + data[i].rct_end_val + '</td>' +
                         '<td>' + data[i].rct_rate + '</td>' +
                       '<td style="text-align:center;"><img class="delete-img remove-target_comm" src="../Resources/images/Remove.png"></td>' +
                        '</tr>');
            }
            RemoveTargetBaseCommission();
        }
    }
    function RemoveTargetBaseCommission() {
        jQuery(".remove-target_comm").unbind('click').click(function (evt) {
            evt.preventDefault();
            var td = jQuery(this).parent('td');
            var tr = jQuery(td).parent('tr');
            var exec = jQuery(tr).find('td:eq(0)').html();
            var range = jQuery(tr).find('td:eq(1)').html();
            var commis = jQuery(tr).find('td:eq(2)').html();
            Lobibox.confirm({
                msg: "Do you want to remove ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/CommissionDefinition/RemoveTargetBaseCommission",
                            data: { Range: range, Commission: commis , Exec:exec},
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        updateTargetBaseCommission(result.list);
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


    jQuery("#chkistargetbaseover").unbind('click').click(function (evt) {
        evt.preventDefault();
        jQuery('#addtagetovrt').modal({
            keyboard: false,
            backdrop: 'static'
        }, 'show');
        $('#st_range_val1').focusout(function () {
            var str = $(this).val();
            var numRange = /^[0-9]\d*(\.\d+)?$/;

            if (!jQuery.isNumeric(str)) {
                setInfoMsg('Please enter a valid range !!!');
                $(this).val("");
            }
        });
        $('#end_range_val1').focusout(function () {
            var str = $(this).val();
            var numRange = /^[0-9]\d*(\.\d+)?$/;

            if (!jQuery.isNumeric(str)) {
                setInfoMsg('Please enter a valid range !!!');
                $(this).val("");
            }
        });
        $('#comm_rate1').focusout(function () {
            var str = $(this).val();
            var numRange = /^[0-9]\d*(\.\d+)?$/;

            if (!jQuery.isNumeric(str)) {
                setInfoMsg('Please enter a valid Commission !!!');
                $(this).val("");
            } else {
                if (Number(str) > 100) {
                    setInfoMsg('Cannot Exceed 100% !!!');
                    $(this).val("");
                }
            }
        });
        //Add function
        TargetBaseOverrideAdd();
        jQuery(".search-target-emp").click(function () {
            var headerKeys = Array()
            headerKeys = ["Row", "Epf No", "Category", "First Name", "Last Name", "NIC"];
            field = "employee2"
            var x = new CommonSearch(headerKeys, field);
        });
    });
    function TargetBaseOverrideAdd() {
        jQuery(".add-target-ovrt").unbind('click').click(function (evt) {
            evt.preventDefault();
            var st_val = jQuery("#st_range_val1").val();
            var end_val = jQuery("#end_range_val1").val();
            var comm_rate = jQuery("#comm_rate1").val();
            var empcat = jQuery("#emp_cd_ov").val();
            var emp = jQuery(".efp-cd2").html();

            if (st_val == "" || end_val == "" || comm_rate == "" || empcat=="") {
                setInfoMsg('Cannot Empty Fields!!');
                return;
            }


            Lobibox.confirm({
                msg: "Do you want to add ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/CommissionDefinition/TargetBaseOverrideAdd",
                            data: { Stval: st_val, Endval: end_val, Rate: comm_rate,emp:emp, empcat:empcat },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        updateTargetBaseOvt(result.list);
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
    function updateTargetBaseOvt(data) {
        jQuery('.target-base-ovt .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {
                jQuery('.target-base-ovt').append('<tr class="new-row">' +
                          '<td>' + data[i].rcto_emp_cd + '</td>' +
                          '<td>' + data[i].rcto_st_val + "-" + data[i].rcto_end_val + '</td>' +
                         '<td>' + data[i].rcto_rate + '</td>' +
                       '<td style="text-align:center;"><img class="delete-img remove-target-ovrt" src="../Resources/images/Remove.png"></td>' +
                        '</tr>');
            }
            RemoveTargetBaseOverride();
        }
    }
    function RemoveTargetBaseOverride() {
        jQuery(".remove-target-ovrt").unbind('click').click(function (evt) {
            evt.preventDefault();
            var td = jQuery(this).parent('td');
            var tr = jQuery(td).parent('tr');
            var emp = jQuery(tr).find('td:eq(0)').html();
            var range = jQuery(tr).find('td:eq(1)').html();
            var commis = jQuery(tr).find('td:eq(2)').html();

            Lobibox.confirm({
                msg: "Do you want to remove ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/CommissionDefinition/RemoveTargetBaseOverride",
                            data: { Range: range, Commission: commis, Emp: emp },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        updateTargetBaseOvt(result.list);
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
    LoadBTUType();
    function LoadBTUType() {
        jQuery.ajax({
            type: "GET",
            url: "/CommissionDefinition/LoadBTUType",
            data: {},
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("btu_item_type");
                        jQuery("#btu_item_type").empty();
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
    jQuery("#chkiscollectbaseover").unbind('click').click(function (evt) {
        evt.preventDefault();
        jQuery('#addcollectionovrt').modal({
            keyboard: false,
            backdrop: 'static'
        }, 'show');
        $('#st_range_val2').focusout(function () {
            var str = $(this).val();
            var numRange = /^[0-9]\d*(\.\d+)?$/;

            if (!jQuery.isNumeric(str)) {
                setInfoMsg('Please enter a valid range !!!');
                $(this).val("");
            }
        });
        $('#end_range_val2').focusout(function () {
            var str = $(this).val();
            var numRange = /^[0-9]\d*(\.\d+)?$/;

            if (!jQuery.isNumeric(str)) {
                setInfoMsg('Please enter a valid range !!!');
                $(this).val("");
            }
        });
        $('#comm_rate2').focusout(function () {
            var str = $(this).val();
            var numRange = /^[0-9]\d*(\.\d+)?$/;

            if (!jQuery.isNumeric(str)) {
                setInfoMsg('Please enter a valid Commission !!!');
                $(this).val("");
            } else {
                if (Number(str) > 100) {
                    setInfoMsg('Cannot Exceed 100% !!!');
                    $(this).val("");
                }
            }
        });
        //Add function
        CollectBaseOverrideAdd();
        jQuery(".search-collect-emp").click(function () {
            var headerKeys = Array()
            headerKeys = ["Row", "Epf No", "Category", "First Name", "Last Name", "NIC"];
            field = "employee3"
            var x = new CommonSearch(headerKeys, field);
        });
        jQuery(".search-coll-invtype").click(function (evt) {
            var headerKeys = Array();
            headerKeys = ["Row", "Code", "Description"];
            field = "InvType2";
            var x = new CommonSearch(headerKeys, field);
        });
    });
    jQuery("#chkbtuitems").unbind('click').click(function (evt) {
        evt.preventDefault();
        jQuery('#addbtuitems').modal({
            keyboard: false,
            backdrop: 'static'
        }, 'show');
        $('#btu_st_val').focusout(function () {
            var str = $(this).val();
            var numRange = /^[0-9]\d*(\.\d+)?$/;

            if (!jQuery.isNumeric(str)) {
                setInfoMsg('Please enter a valid range !!!');
                $(this).val("");
            }
        });
        $('#btu_end_val').focusout(function () {
            var str = $(this).val();
            var numRange = /^[0-9]\d*(\.\d+)?$/;

            if (!jQuery.isNumeric(str)) {
                setInfoMsg('Please enter a valid range !!!');
                $(this).val("");
            }
        });
        $('#btu_commission').focusout(function () {
            var str = $(this).val();
            var numRange = /^[0-9]\d*(\.\d+)?$/;

            if (!jQuery.isNumeric(str)) {
                setInfoMsg('Please enter a valid Commission !!!');
                $(this).val("");
            } else {
               
            }
        });
        //Add function
        BTUItemsAdd();
        jQuery(".search-btu-cd").click(function () {
            if (jQuery("#btu_cat").val()=="")
            {
                setInfoMsg('Please Select Cat!!!');
            }

            var headerKeys = Array()
            headerKeys = ["Row", "BTU"];
            field = "btuval";
            var data = {
                Cat: jQuery("#btu_cat").val()
            }
            var x = new CommonSearch(headerKeys, field,data);
        });
    });
    function CollectBaseOverrideAdd() {
        jQuery(".add-collect-base-ovt").unbind('click').click(function (evt) {
            evt.preventDefault();
            var st_val = jQuery("#st_range_val2").val();
            var end_val = jQuery("#end_range_val2").val();
            var comm_rate = jQuery("#comm_rate2").val();
            var empcat = jQuery("#emp_cd_ov2").val();
            var emp = jQuery(".efp-cd3").html();
            var invtype = jQuery("#inv_type2").val();
            var stldate1 = jQuery("#sett_st_date").val();
            var stldate2 = jQuery("#sett_end_date").val();

            if (st_val == "" || end_val == "" || comm_rate == "" || empcat == "" || stldate1=="" || stldate2=="") {
                setInfoMsg('Cannot Empty Fields!!');
                return;
            }
            Lobibox.confirm({
                msg: "Do you want to add ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/CommissionDefinition/CollectionBaseOverrideAdd",
                            data: { Stval: st_val, Endval: end_val, Rate: comm_rate, emp: emp, empcat: empcat,invtype:invtype,stldate_st:stldate1,stldate_end:stldate2 },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        updateCollectionBaseOverride(result.list);
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
    function BTUItemsAdd() {
        jQuery(".add-btu-val").unbind('click').click(function (evt) {
            evt.preventDefault();
            var st_val = jQuery("#btu_st_val").val();
            var end_val = jQuery("#btu_end_val").val();
            var comm_rate = jQuery("#btu_commission").val();
            var category = jQuery("#btu_cat").val();
            var btu = jQuery("#btu_code").val();
            var btu2 = jQuery("#btu_code2").val();
            var btutype = jQuery("#btu_item_type").val();

            if (st_val == "" || end_val == "" || comm_rate == "" || category == "") {
                setInfoMsg('Cannot Empty Fields!!');
                return;
            }
            Lobibox.confirm({
                msg: "Do you want to add ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/CommissionDefinition/BTUItemsAdd",
                            data: { Stval: st_val, Endval: end_val, Rate: comm_rate, Cat: category, BTU: btu, BTU2: btu2, BTUtype: btutype },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        SetItemValues(result.data);
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
    function updateCollectionBaseOverride(data) {
        jQuery('.collect-base-ovt .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {
                jQuery('.collect-base-ovt').append('<tr class="new-row">' +
                          '<td>' + data[i].rcco_emp_cd + '</td>' +
                           '<td>' + data[i].rcco_inv_tp + '</td>' +
                          '<td>' + data[i].rcco_st_val + "-" + data[i].rcco_end_val + '</td>' +
                          '<td>' + data[i].rcco_stl_st_dt + "-" + data[i].rcco_stl_end_dt + '</td>' +
                         '<td>' + data[i].rcco_rate + '</td>' +
                       '<td style="text-align:center;"><img class="delete-img remove-collect-ovrt" src="../Resources/images/Remove.png"></td>' +
                        '</tr>');
            }
            RemoveCollectionBaseOvrt();
        }
    }
    function RemoveCollectionBaseOvrt() {
        jQuery(".remove-collect-ovrt").unbind('click').click(function (evt) {
            evt.preventDefault();
            var td = jQuery(this).parent('td');
            var tr = jQuery(td).parent('tr');
            var emp = jQuery(tr).find('td:eq(0)').html();
            var invtype = jQuery(tr).find('td:eq(1)').html();
            var range = jQuery(tr).find('td:eq(2)').html();
            var daterange = jQuery(tr).find('td:eq(3)').html();
            var commis = jQuery(tr).find('td:eq(4)').html();

            Lobibox.confirm({
                msg: "Do you want to remove ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/CommissionDefinition/RemoveCollectionBaseOverride",
                            data: { Range: range, Commission: commis, Emp: emp,daterange:daterange,invtype:invtype },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        updateCollectionBaseOverride(result.list);
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
    jQuery("#chkelitecomm").unbind('click').click(function (evt) {
        evt.preventDefault();
        jQuery('#elitecommission').modal({
            keyboard: false,
            backdrop: 'static'
        }, 'show');
    });
    LoadEliteTargetType();
    function LoadEliteTargetType() {
        jQuery.ajax({
            type: "GET",
            url: "/CommissionDefinition/LoadTarget",
            data: { },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("elicommtype");
                        jQuery("#elicommtype").empty();
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

    //
    EliteCommissionTargetAdd();
    function EliteCommissionTargetAdd() {
        jQuery(".add-eli-comm-val").unbind('click').click(function (evt) {
            evt.preventDefault();
            var pc = jQuery("#elitepc").val();
            var TargetType = jQuery("#elicommtype").val();
            var Month = jQuery("#elmonth").val();
            var TargetVal = jQuery("#eltarget").val();
            var fromper = jQuery("#elfrom").val();
            var toper = jQuery("#elto").val();
            var mngcomm = jQuery("#elmngcomm").val();
            var execomm = jQuery("#elexccomm").val();
            var ovrcashcomm = jQuery("#elovcashi").val();
            var ovrhelpcomm = jQuery("#elovrhelp").val();

            if (pc == "" || TargetType == "" || Month == "") {
                setInfoMsg('Cannot Empty Fields!!');
                return;
            }
            Lobibox.confirm({
                msg: "Do you want to add ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/CommissionDefinition/AddEliteItems",
                            data: { pc: pc, TargetType: TargetType, Month: Month, TargetVal: TargetVal, fromper: fromper, toper: toper, mngcomm: mngcomm, execomm: execomm, ovrcashcomm: ovrcashcomm, ovrhelpcomm: ovrhelpcomm },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        SetEliteItemValues(result.data);
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
    function SetEliteItemValues(data) {
        jQuery('.eli-tar-tbl .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {
                jQuery('.eli-tar-tbl').append('<tr class="new-row">' +
                        '<td>' + data[i].rect_pc + '</td>' +
                         '<td>' + data[i].rect_tp + '</td>' +
                         '<td>' + data[i].rect_month + '</td>' +
                         '<td>' + data[i].rect_target + '</td>' +
                         '<td>' + data[i].rect_frm_per + '</td>' +
                          '<td>' + data[i].rect_to_per + '</td>' +
                         '<td>' + data[i].rect_mng_comm + '</td>' +
                         '<td>' + data[i].rect_exc_comm + '</td>' +
                         '<td>' + data[i].rect_cashi_comm + '</td>' +
                          '<td>' + data[i].rect_help_comm + '</td>' +
                       '<td style="text-align:center;"><img class="delete-img remove-eli-comm" src="../Resources/images/Remove.png"></td>' +
                        '</tr>');
            }
            RemoveEliteItemValueFunction();
        }
    }
    function RemoveEliteItemValueFunction() {
        jQuery(".remove-eli-comm").unbind('click').click(function (evt) {
            evt.preventDefault();
            var td = jQuery(this).parent('td');
            var tr = jQuery(td).parent('tr');
            var pc = jQuery(tr).find('td:eq(0)').html();
            var Type = jQuery(tr).find('td:eq(1)').html();
            var Month = jQuery(tr).find('td:eq(2)').html();
            Lobibox.confirm({
                msg: "Do you want to remove ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/CommissionDefinition/RemoveEliteItemValueRange",
                            data: { pc: pc, Type: Type, Month: Month },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        SetEliteItemValues(result.data);
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
    jQuery("#chkeliteaddcomm").unbind('click').click(function (evt) {
        evt.preventDefault();
        jQuery('#eliteaddcommission').modal({
            keyboard: false,
            backdrop: 'static'
        }, 'show');
    });
    LoadEliteAddiTargetType();
    function LoadEliteAddiTargetType() {
        jQuery.ajax({
            type: "GET",
            url: "/CommissionDefinition/LoadAdditionalTarget",
            data: {},
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("elicommaddtype");
                        jQuery("#elicommaddtype").empty();
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
    EliteAdditinalCommissionAdd();
    function EliteAdditinalCommissionAdd() {
        jQuery(".add-eli-addi-comm-val").unbind('click').click(function (evt) {
            evt.preventDefault();
            var pc = jQuery("#eliteaddpc").val();
            var slab = jQuery("#elslab").val();
            var from = jQuery("#eladdfrom").val();
            var to = jQuery("#eladdto").val();
            var comm = jQuery("#eladdcomm").val();
            var type2 = jQuery("#elicommaddtype").val();
            var gap = jQuery("#eladdcommgp").val();

            if (pc == "" || slab == "" || type2 == "") {
                setInfoMsg('Cannot Empty Fields!!');
                return;
            }
            Lobibox.confirm({
                msg: "Do you want to add ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/CommissionDefinition/AddEliteAdditinalItems",
                            data: { pc: pc, slab: slab, from: from, to: to, comm: comm, type2: type2, gap: gap },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        SetEliteAddValues(result.data);
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
    function SetEliteAddValues(data) {
        jQuery('.eli-addcomm-tbl .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {
                jQuery('.eli-addcomm-tbl').append('<tr class="new-row">' +
                        '<td>' + data[i].rcat_pc + '</td>' +
                         '<td>' + data[i].rcat_slab + '</td>' +
                         '<td>' + data[i].rcat_from + '</td>' +
                         '<td>' + data[i].rcat_to + '</td>' +
                         '<td>' + data[i].rcat_comm + '</td>' +
                          '<td>' + data[i].rcat_type + '</td>' +
                         '<td>' + data[i].rcat_gapval + '</td>' +
                       '<td style="text-align:center;"><img class="delete-img remove-eli-add-comm" src="../Resources/images/Remove.png"></td>' +
                        '</tr>');
            }
           RemoveAddiEliteItemValueFunction();
        }
    }
    function RemoveAddiEliteItemValueFunction() {
        jQuery(".remove-eli-add-comm").unbind('click').click(function (evt) {
            evt.preventDefault();
            var td = jQuery(this).parent('td');
            var tr = jQuery(td).parent('tr');
            var pc = jQuery(tr).find('td:eq(0)').html();
            var Slab = jQuery(tr).find('td:eq(1)').html();
            Lobibox.confirm({
                msg: "Do you want to remove ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/CommissionDefinition/RemoveAddiEliteItemValueRange",
                            data: { pc: pc, Slab: Slab},
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        SetEliteAddValues(result.data);
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