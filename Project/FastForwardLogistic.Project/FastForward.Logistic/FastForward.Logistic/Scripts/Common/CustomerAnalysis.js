var itemIds1 = [];
var modelIds1 = [];
var brandIds1 = [];
var cat1Ids1 = [];
var cat2Ids1 = [];
var cat3Ids1 = [];

var cusIds1 = [];
var invTpIds1 = [];
var schmTpIds1 = [];
var schmCdIds1 = [];
var ptypeCdIds1 = [];

var FOCstatus = "";
var IntcomStatus = "";
var intcurdate = "";

var ctownIds1 = [];
var ptownIds1 = [];
var pdistIds1 = [];
var pprovIds1 = [];
var bankIds1 = [];



jQuery(document).ready(function () {

    jQuery(".maptype").click(function () {
        if (jQuery(this).val() == "district") {
            jQuery("#map").show();
            jQuery("#mapregion").hide();
        } else if (jQuery(this).val() == "region") {
            jQuery("#map").hide();
            jQuery("#mapregion").show();
        }
    });

    jQuery(".cus-cd-search").click(function () {
        //alert("d");
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Name", "NIC", "Mobile", "BR No"];
        field = "cusCodecus1"
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery(".cus-company-search").click(function () {
        //alert("aaas");
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description"];
        field = "CompanyGlobeCus"
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery(".cus-mode-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description"];
        field = "ModeOfShipmentCus"
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery(".cus-pc-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description", "Address"];
        field = "ProfitCenterGlobeCus"
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery(".cus-town-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Town", "District", "Province", "Code"];
        field = "perTownCus"
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery(".cus-District-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "District", "Province", "Code"];
        field = "perDistrictCus"
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery(".cus-Province-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Province"];
        field = "perProvinceCus"
        var x = new CommonSearch(headerKeys, field);
    });

    //jQuery('#SalesFrom').val(my_date_format(new Date()));
    //jQuery('#SalesTo').val(my_date_format(new Date()));

    jQuery('#SalesFrom').datepicker({ maxDate: new Date(), dateFormat: "dd/M/yy" });
    jQuery('#SalesTo').datepicker({ maxDate: new Date(), dateFormat: "dd/M/yy" });
    jQuery('#SalesFrom').val(getFormatedDate(getMonthAgoMonth(getFormatedDate(new Date()))));
    jQuery('#SalesTo').val(getFormatedDate(new Date()));

    jQuery("#SalesFrom,#SalesTo").focusout(function () {
        if (ValidDate(jQuery(this).val()) == "false" && jQuery(this).val() != "") {
            setInfoMsg("Please enter valid date.");
            jQuery(this).val("");
        }
    });

    //jQuery("#SalesFrom").focusout(function () {

    //    //jQuery('#Tih_inv_dt').focus();

    //    var selected_date = jQuery(this).val();
    //    if (selected_date == "") {
    //        jQuery('#SalesFrom').val(my_date_format(new Date()));
    //    }
    //});
    //jQuery("#SalesTo").focusout(function () {

    //    //jQuery('#Tih_inv_dt').focus();

    //    var selected_date = jQuery(this).val();
    //    if (selected_date == "") {
    //        jQuery('#SalesTo').val(my_date_format(new Date()));
    //    }
    //});

    loadAgeAndSalary();

    loadCompany();
    searchChannel();
    searchSubChannel();
    searchArea();
    searchRegion();
    searchZone();
    searchPC();
    searchItemCodes();
    searchItemModel();
    searchBrand();
    searchMainCat();
    searchCategory2();
    searchCategory3();

    searchCustomer();

    serachInvoiceTyps();
    searchSchmTp();
    searchSchmCd();
    searchPaytyp();

    searchCTown();
    searchPTown();
    searchPDistrict();
    searchPProvince();
    searchBank();
    loadAgeAndSalary();
    //rowClick();


    var pgeSize = jQuery('.cls-no-Page').val();
    jQuery(".btn-display-det").click(function (evt) {
        evt.preventDefault();
        displayPopData();
    });
    //$(".catetype").change(function () {
    //    if (jQuery(this).val() == "item") {
    //        jQuery(".hidefield").show();
    //        jQuery('.invodet').attr('colspan', '11');
    //    } else if (jQuery(this).val() == "invoice") {
    //        jQuery(".hidefield").hide();
    //        jQuery('.invodet').attr('colspan', '5');
    //    }
    //});
    jQuery(".btn.sms-send").click(function (evt) {
        evt.preventDefault();
        $('#smsModalAll').modal('show');
        jQuery(".btn-disply-export").show();
    });


    $("#CheckAmount").keydown(function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            // Allow: Ctrl+A, Command+A
            (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
    $("#CheckVisit").keydown(function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            // Allow: Ctrl+A, Command+A
            (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
    $("#CheckAge").keydown(function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            // Allow: Ctrl+A, Command+A
            (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
    $("#CheckSalary").keydown(function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            // Allow: Ctrl+A, Command+A
            (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });

    //function loadAgeAndSalary() {
    //    jQuery.ajax({
    //        type: "GET",
    //        url: "/CustomerAnalysis/getAgeCategory",
    //        contentType: "application/json;charset=utf-8",
    //        dataType: "json",
    //        data: { type: "AGE" },
    //        success: function (result) {
    //            if (result.login == true) {
    //                if (result.success == true) {
    //                    jQuery('#CheckAge').empty();
    //                    var cont = "";
    //                    var json = jQuery.parseJSON(result.data);
    //                    cont += "<option value=''>SELECT</option>";
    //                    for (i = 0; i < json.length; i++) {
    //                        cont += "<option value=" + json[i].RBV_VAL + ">" + json[i].RBV_VAL + "</option>";
    //                    }
    //                    jQuery("#CheckAge").append(cont);
    //                }
    //            }
    //        }
    //    });
    //    jQuery.ajax({
    //        type: "GET",
    //        url: "/CustomerAnalysis/getAgeCategory",
    //        contentType: "application/json;charset=utf-8",
    //        dataType: "json",
    //        data: { type: "SALARY" },
    //        success: function (result) {
    //            if (result.login == true) {
    //                if (result.success == true) {
    //                    jQuery('#CheckSalary').empty();
    //                    var cont = "";
    //                    var json = jQuery.parseJSON(result.data);
    //                    cont += "<option value=''>SELECT</option>";
    //                    for (i = 0; i < json.length; i++) {
    //                        cont += "<option value=" + json[i].RBV_VAL + ">" + json[i].RBV_VAL + "</option>";
    //                    }
    //                    jQuery('#CheckSalary').append(cont);
    //                }
    //            }
    //        }
    //    });
    //}


    if (jQuery('#allmodel').is(":checked")) {
        jQuery("#txtModel").attr("readonly", "readonly");
    } else {
        jQuery("#txtModel").removeAttr("readonly");
    }
    jQuery("#allmodel").click(function () {
        if (jQuery('#allmodel').is(":checked")) {
            jQuery("#txtModel").attr("readonly", "readonly");
            jQuery("#txtModel").val("");
        } else {
            jQuery("#txtModel").removeAttr("readonly");
        }
    });

    if (jQuery('#allitems').is(":checked")) {
        jQuery("#txtItem").attr("readonly", "readonly");
    } else {
        jQuery("#txtItem").removeAttr("readonly");
    }
    jQuery("#allitems").click(function () {
        if (jQuery('#allitems').is(":checked")) {
            jQuery("#txtItem").attr("readonly", "readonly");
            jQuery("#txtItem").val("");
        } else {
            jQuery("#txtItem").removeAttr("readonly");
        }
    });



    //jQuery("#txtModel").on("keydown", function (evt) {
    //    if (evt.keyCode == 113) {
    //        var headerKeys = Array();
    //        headerKeys = ["Row", "Model", "Description", "Brand", "Category 1"];
    //        data = { brndMngr: jQuery("#BrandMngr").val(), brnd: jQuery("#Brand").val(), mcate: jQuery("#MainCat").val() };
    //        field = "srchItemModelAge";
    //        var x = new CommonSearch(headerKeys, field, data);
    //    }
    //});
    //jQuery(".model-search").click(function () {
    //    if (!jQuery('#allmodel').is(":checked")) {
    //        var headerKeys = Array();
    //        headerKeys = ["Row", "Model", "Description", "Brand", "Category 1"];
    //        field = "srchItemModelAge";
    //        data = { brndMngr: jQuery("#BrandMngr").val(), brnd: jQuery("#Brand").val(), mcate: jQuery("#MainCat").val() };
    //        var x = new CommonSearch(headerKeys, field, data);
    //    }
    //});
    jQuery(".item-search").click(function (evt) {
        if (!jQuery('#allitems').is(":checked")) {
            var headerKeys = Array();
            headerKeys = ["Row", "Code", "Description"];
            field = "srchItemsage";
            data = { brndMngr: "", brnd: jQuery("#Brand").val(), mcate: jQuery("#MainCat").val(), model: jQuery("#txtModel").val() };
            var x = new CommonSearch(headerKeys, field, data);
        }
    });
    jQuery("#txtItem").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            if (!jQuery('#txtItem').is(":checked")) {
                var headerKeys = Array();
                headerKeys = ["Row", "Code", "Description"];
                field = "srchItemsage";
                data = { brndMngr: "", brnd: jQuery("#Brand").val(), mcate: jQuery("#MainCat").val(), model: jQuery("#txtModel").val() };
                var x = new CommonSearch(headerKeys, field, data);
            }
        }
    });


    if (jQuery('#allcate').is(":checked")) {
        jQuery("#MainCat").attr("readonly", "readonly");
    } else {
        jQuery("#MainCat").removeAttr("readonly");
    }
    jQuery("#allcate").click(function () {
        if (jQuery('#allcate').is(":checked")) {
            jQuery("#MainCat").attr("readonly", "readonly");
            jQuery("#MainCat").val("");
        } else {
            jQuery("#MainCat").removeAttr("readonly");
        }
    });
    jQuery("#MainCat").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            if (!jQuery('#allcate').is(":checked")) {
                var headerKeys = Array();
                headerKeys = ["Row", "Code", "Description"];
                field = "srchMainCat";
                var x = new CommonSearch(headerKeys, field);
            }
        }
    });

    jQuery(".cate-search").click(function (evt) {
        if (!jQuery('#allcate').is(":checked")) {
            var headerKeys = Array();
            headerKeys = ["Row", "Code", "Description"];
            field = "srchMainCat";
            var x = new CommonSearch(headerKeys, field);
        }
    });
    if (jQuery('#allbrnd').is(":checked")) {
        jQuery("#Brand").attr("readonly", "readonly");
    } else {
        jQuery("#Brand").removeAttr("readonly");
    }
    jQuery("#allbrnd").click(function () {
        if (jQuery('#allbrnd').is(":checked")) {
            jQuery("#Brand").attr("readonly", "readonly");
            jQuery("#Brand").val("");
        } else {
            jQuery("#Brand").removeAttr("readonly");
        }
    });
    //jQuery("#Brand").on("keydown", function (evt) {
    //    if (evt.keyCode == 13) {
    //        if (!jQuery('#allbrnd').is(":checked")) {
    //            var headerKeys = Array();
    //            headerKeys = ["Row", "Code", "Description"];
    //            var data = "";
    //            field = "srchBrnd";
    //            var resData = new CommonSearch(headerKeys, field, data);
    //        }
    //    }
    //});

    //jQuery(".brnd-search").click(function (evt) {
    //    if (!jQuery('#allbrnd').is(":checked")) {
    //        var headerKeys = Array();
    //        headerKeys = ["Row", "Code", "Description"];
    //        var data = "";
    //        field = "srchBrnd";
    //        var resData = new CommonSearch(headerKeys, field, data);
    //    }
    //});

    //jQuery(".company-search").click(function (evt) {
    //    if (!jQuery('#AllCom').is(":checked")) {
    //        var headerKeys = Array();
    //        headerKeys = ["Row", "Code", "Description"];
    //        field = "srchAllComM";
    //        var x = new CommonSearch(headerKeys, field);
    //    }
    //});

    //customer town
    if (jQuery('#allcustown').is(":checked")) {
        jQuery("#CusTown").attr("readonly", "readonly");
    } else {
        jQuery("#CusTown").removeAttr("readonly");
    }
    jQuery("#allcustown").click(function () {
        if (jQuery('#allcustown').is(":checked")) {
            jQuery("#CusTown").attr("readonly", "readonly");
            jQuery("#CusTown").val("");
        } else {
            jQuery("#CusTown").removeAttr("readonly");
        }
    });
    jQuery("#CusTown").on("keydown", function (evt) {
        if (evt.keyCode == 13) {
            if (!jQuery('#allcustown').is(":checked")) {
                var headerKeys = Array();
                headerKeys = ["Row", "Code", "Description"];
                var data = "";
                field = "srchBrnd";
                var resData = new CommonSearch(headerKeys, field, data);
            }
        }
    });

    jQuery(".custown-search").click(function (evt) {
        if (!jQuery('#allcustown').is(":checked")) {
            var headerKeys = Array();
            headerKeys = ["Row", "Code", "Description"];
            var data = "";
            field = "srchBrnd";
            var resData = new CommonSearch(headerKeys, field, data);
        }
    });

    //showroom town
    if (jQuery('#allshtown').is(":checked")) {
        jQuery("#ShTown").attr("readonly", "readonly");
    } else {
        jQuery("#ShTown").removeAttr("readonly");
    }
    jQuery("#allshtown").click(function () {
        if (jQuery('#allshtown').is(":checked")) {
            jQuery("#ShTown").attr("readonly", "readonly");
            jQuery("#ShTown").val("");
        } else {
            jQuery("#ShTown").removeAttr("readonly");
        }
    });
    jQuery("#ShTown").on("keydown", function (evt) {
        if (evt.keyCode == 13) {
            if (!jQuery('#allshtown').is(":checked")) {
                var headerKeys = Array();
                headerKeys = ["Row", "Code", "Description"];
                var data = "";
                field = "srchBrnd";
                var resData = new CommonSearch(headerKeys, field, data);
            }
        }
    });

    jQuery(".custown-search").click(function (evt) {
        if (!jQuery('#allshtown').is(":checked")) {
            var headerKeys = Array();
            headerKeys = ["Row", "Code", "Description"];
            var data = "";
            field = "srchBrnd";
            var resData = new CommonSearch(headerKeys, field, data);
        }
    });
    //*************
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1;

    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd;
    }
    if (mm < 10) {
        mm = "0" + mm;
    }

    //jQuery("#SalesFrom").val(yyyy + "-" + mm + "-" + dd);
    //window.myDatePicker = new DatePicker('#SalesFrom', {
    //    datePickerClass: 'date-picker has-week-no',
    //    sundayBased: false,
    //    renderWeekNo: true
    //});

    //jQuery("#SalesTo").val(yyyy + "-" + mm + "-" + dd);
    //window.myDatePicker = new DatePicker('#SalesTo', {
    //    datePickerClass: 'date-picker has-week-no',
    //    sundayBased: false,
    //    renderWeekNo: true
    //});

    //jQuery("#SalesFrom").focusout(function () {

    //    //jQuery('#Tih_inv_dt').focus();

    //    var selected_date = jQuery(this).val();
    //    if (selected_date == "") {
    //        jQuery('#SalesFrom').val(my_date_format(new Date()));
    //    }
    //});
    //jQuery("#SalesTo").focusout(function () {

    //    //jQuery('#Tih_inv_dt').focus();

    //    var selected_date = jQuery(this).val();
    //    if (selected_date == "") {
    //        jQuery('#SalesTo').val(my_date_format(new Date()));
    //    }
    //});

    jQuery(".cls-no-Page").change(function () {
        var pgeSize = jQuery('.cls-no-Page').val();
        pagingCon(1, pgeSize);
    });
    jQuery(".cls-no-Pageinv").change(function () {
        var pgeSize = jQuery('.cls-no-Pageinv').val();
        pagingConInv(1, pgeSize);
    });
    jQuery(".maptype").click(function () {
        if (jQuery(this).val() == "district") {
            jQuery("#map").show();
            jQuery("#mapregion").hide();
        } else if (jQuery(this).val() == "region") {
            jQuery("#map").hide();
            jQuery("#mapregion").show();
        }
    });
});

function addValuesFront(value, type) {
    var valVal = jQuery(value).attr("value");
    var type = type;
    jQuery.ajax({
        type: "GET",
        url: "/CustomerAnalysis/addSelectedValues",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: { type: type, value: valVal },
        success: function (result) {
            if (result.login == true) {
                if (result.success == true) {
                    updateVals(type, result.selecteditem);
                }
            }
        }
    });
}

function updateVals(type, value) {
    if (type == "CHNL") {
        jQuery("#chnlChip").empty();
        if (value.length > 0) {
            for (i = 0 ; i < value.length; i++) {
                jQuery("#chnlChip").append("<div class='chip newchip'><span class='valuefld'>" + value[i].MSC_CD + "</span><span class='closebtn' onclick='removeChipFront(this,\"CHNL\")'>&times;</span></div>");

            }
        }
    } else if (type == "SCHNL") {
        jQuery("#schnlChip").empty();
        if (value.length > 0) {
            for (i = 0 ; i < value.length; i++) {
                jQuery("#schnlChip").append("<div class='chip newchip'><span class='valuefld'>" + value[i].MSC_CD + "</span><span class='closebtn' onclick='removeChipFront(this,\"SCHNL\")'>&times;</span></div>");

            }
        }
    } else if (type == "AREA") {
        jQuery("#areaChip").empty();
        if (value.length > 0) {
            for (i = 0 ; i < value.length; i++) {
                jQuery("#areaChip").append("<div class='chip newchip'><span class='valuefld'>" + value[i].MSC_CD + "</span><span class='closebtn' onclick='removeChipFront(this,\"AREA\")'>&times;</span></div>");

            }
        }
    } else if (type == "REGION") {
        jQuery("#regnChip").empty();
        if (value.length > 0) {
            for (i = 0 ; i < value.length; i++) {
                jQuery("#regnChip").append("<div class='chip newchip'><span class='valuefld'>" + value[i].MSC_CD + "</span><span class='closebtn' onclick='removeChipFront(this,\"REGION\")'>&times;</span></div>");

            }
        }
    } else if (type == "ZONE") {
        jQuery("#zoneChip").empty();
        if (value.length > 0) {
            for (i = 0 ; i < value.length; i++) {
                jQuery("#zoneChip").append("<div class='chip newchip'><span class='valuefld'>" + value[i].MSC_CD + "</span><span class='closebtn' onclick='removeChipFront(this,\"ZONE\")'>&times;</span></div>");

            }
        }
    } else if (type == "PC") {
        jQuery("#PCChip").empty();
        if (value.length > 0) {
            for (i = 0 ; i < value.length; i++) {
                jQuery("#PCChip").append("<div class='chip newchip'><span class='valuefld'>" + value[i].MSC_CD + "</span><span class='closebtn' onclick='removeChipFront(this,\"PC\")'>&times;</span></div>");

            }
        }
    } else if (type == "item") {
        jQuery("#itmChip").empty();
        if (value.length > 0) {
            for (i = 0 ; i < value.length; i++) {
                jQuery("#itmChip").append("<div class='chip newchip'><span class='disfld'>" + value[i].srtp_desc + "</span><span class='valuefld no-display'>" + value[i].srtp_cd + "</span><span class='closebtn' onclick='removeChipFront(this,\"item\")'>&times;</span></div>");

            }
        }
    } else if (type == "model") {
        jQuery("#modelChip").empty();
        if (value.length > 0) {
            for (i = 0 ; i < value.length; i++) {
                jQuery("#modelChip").append("<div class='chip newchip'><span class='disfld'>" + value[i].mm_desc + "</span><span class='valuefld no-display'>" + value[i].mm_cd + "</span><span class='closebtn' onclick='removeChipFront(this,\"model\")'>&times;</span></div>");

            }
        }
    } else if (type == "brnd") {
        jQuery("#brndChip").empty();
        if (value.length > 0) {
            for (i = 0 ; i < value.length; i++) {
                jQuery("#brndChip").append("<div class='chip newchip'><span class='disfld'>" + value[i].mb_desc + "</span><span class='valuefld no-display'>" + value[i].mb_cd + "</span><span class='closebtn' onclick='removeChipFront(this,\"brnd\")'>&times;</span></div>");

            }
        }
    } else if (type == "cat1") {
        jQuery("#cat1Chip").empty();
        if (value.length > 0) {
            for (i = 0 ; i < value.length; i++) {
                jQuery("#cat1Chip").append("<div class='chip newchip'><span class='disfld'>" + value[i].main_cat_desc + "</span><span class='valuefld no-display'>" + value[i].main_cat_cd + "</span><span class='closebtn' onclick='removeChipFront(this,\"cat1\")'>&times;</span></div>");

            }
        }
    } else if (type == "cat2") {
        jQuery("#cat2Chip").empty();
        if (value.length > 0) {
            for (i = 0 ; i < value.length; i++) {
                jQuery("#cat2Chip").append("<div class='chip newchip'><span class='disfld'>" + value[i].cat2_desc + "</span><span class='valuefld no-display'>" + value[i].cat2_cd + "</span><span class='closebtn' onclick='removeChipFront(this,\"cat2\")'>&times;</span></div>");

            }
        }
    } else if (type == "cat3") {
        jQuery("#cat3Chip").empty();
        if (value.length > 0) {
            for (i = 0 ; i < value.length; i++) {
                jQuery("#cat3Chip").append("<div class='chip newchip'><span class='disfld'>" + value[i].cat3_desc + "</span><span class='valuefld no-display'>" + value[i].cat3_cd + "</span><span class='closebtn' onclick='removeChipFront(this,\"cat3\")'>&times;</span></div>");

            }
        }
    } else if (type == "cust") {
        jQuery("#custChip").empty();
        if (value.length > 0) {
            for (i = 0 ; i < value.length; i++) {
                jQuery("#custChip").append("<div class='chip newchip'><span class='disfld'>" + value[i].Mbe_name + "</span><span class='valuefld no-display'>" + value[i].Mbe_cd + "</span><span class='closebtn' onclick='removeChipFront(this,\"cust\")'>&times;</span></div>");

            }
        }
    } else if (type == "ctown") {
        jQuery("#ctownChip").empty();
        if (value.length > 0) {
            for (i = 0 ; i < value.length; i++) {
                jQuery("#ctownChip").append("<div class='chip newchip'><span class='disfld'>" + value[i].mt_desc + "</span><span class='valuefld no-display'>" + value[i].mt_cd + "</span><span class='closebtn' onclick='removeChipFront(this,\"ctown\")'>&times;</span></div>");

            }
        }
    } else if (type == "cdistrict") {
        jQuery("#cdistrictChip").empty();
        if (value.length > 0) {
            for (i = 0 ; i < value.length; i++) {
                jQuery("#cdistrictChip").append("<div class='chip newchip'><span class='disfld'>" + value[i].mdis_desc + "</span><span class='valuefld no-display'>" + value[i].mdis_cd + "</span><span class='closebtn' onclick='removeChipFront(this,\"cdistrict\")'>&times;</span></div>");

            }
        }
    } else if (type == "cprovince") {
        jQuery("#cprovinceChip").empty();
        if (value.length > 0) {
            for (i = 0 ; i < value.length; i++) {
                jQuery("#cprovinceChip").append("<div class='chip newchip'><span class='disfld'>" + value[i].mpro_desc + "</span><span class='valuefld no-display'>" + value[i].mpro_cd + "</span><span class='closebtn' onclick='removeChipFront(this,\"cprovince\")'>&times;</span></div>");

            }
        }
    } else if (type == "ccompany") {
        jQuery("#ccompanyChip").empty();
        if (value.length > 0) {
            for (i = 0 ; i < value.length; i++) {
                jQuery("#ccompanyChip").append("<div class='chip newchip'><span class='disfld'>" + value[i].Mc_desc + "</span><span class='valuefld no-display'>" + value[i].Mc_cd + "</span><span class='closebtn' onclick='removeChipFront(this,\"ccompany\")'>&times;</span></div>");

            }
        }
    } else if (type == "cmode") {
        jQuery("#cmodeChip").empty();
        if (value.length > 0) {
            for (i = 0 ; i < value.length; i++) {
                jQuery("#cmodeChip").append("<div class='chip newchip'><span class='disfld'>" + value[i].Mms_desc + "</span><span class='valuefld no-display'>" + value[i].Mms_cd + "</span><span class='closebtn' onclick='removeChipFront(this,\"cmode\")'>&times;</span></div>");

            }
        }
    } else if (type == "cpc") {
        jQuery("#cpcChip").empty();
        if (value.length > 0) {
            for (i = 0 ; i < value.length; i++) {
                jQuery("#cpcChip").append("<div class='chip newchip'><span class='disfld'>" + value[i].Mpc_desc + "</span><span class='valuefld no-display'>" + value[i].Mpc_cd + "</span><span class='closebtn' onclick='removeChipFront(this,\"cpc\")'>&times;</span></div>");

            }
        }
    } else if (type == "ccustomer") {
        jQuery("#ccustomerChip").empty();
        if (value.length > 0) {
            for (i = 0 ; i < value.length; i++) {
                jQuery("#ccustomerChip").append("<div class='chip newchip'><span class='disfld'>" + value[i].Mbe_name + "</span><span class='valuefld no-display'>" + value[i].Mbe_cd + "</span><span class='closebtn' onclick='removeChipFront(this,\"ccustomer\")'>&times;</span></div>");

            }
        }
    } else if (type == "ptown") {
        jQuery("#ptownChip").empty();
        if (value.length > 0) {
            for (i = 0 ; i < value.length; i++) {
                jQuery("#ptownChip").append("<div class='chip newchip'><span class='disfld'>" + value[i].mt_desc + "</span><span class='valuefld no-display'>" + value[i].mt_cd + "</span><span class='closebtn' onclick='removeChipFront(this,\"ptown\")'>&times;</span></div>");

            }
        }
    } else if (type == "pdist") {
        jQuery("#pdistChip").empty();
        if (value.length > 0) {
            for (i = 0 ; i < value.length; i++) {
                jQuery("#pdistChip").append("<div class='chip newchip'><span class='disfld'>" + value[i].mds_district + "</span><span class='valuefld no-display'>" + value[i].mds_dist_cd + "</span><span class='closebtn' onclick='removeChipFront(this,\"pdist\")'>&times;</span></div>");

            }
        }
    } else if (type == "pprov") {
        jQuery("#pprovChip").empty();
        if (value.length > 0) {
            for (i = 0 ; i < value.length; i++) {
                jQuery("#pprovChip").append("<div class='chip newchip'><span class='disfld'>" + value[i].mds_province + "</span><span class='valuefld no-display'>" + value[i].mds_prov_cd + "</span><span class='closebtn' onclick='removeChipFront(this,\"pprov\")'>&times;</span></div>");

            }
        }
    } else if (type == "invTyp") {
        jQuery("#invTpChip").empty();
        if (value.length > 0) {
            for (i = 0 ; i < value.length; i++) {
                jQuery("#invTpChip").append("<div class='chip newchip'><span class='disfld'>" + value[i].srtp_desc + "</span><span class='valuefld no-display'>" + value[i].srtp_cd + "</span><span class='closebtn' onclick='removeChipFront(this,\"invTyp\")'>&times;</span></div>");

            }
        }
    } else if (type == "schmTp") {
        jQuery("#schmTpChip").empty();
        if (value.length > 0) {
            for (i = 0 ; i < value.length; i++) {
                jQuery("#schmTpChip").append("<div class='chip newchip'><span class='disfld'>" + value[i].main_cat_desc + "</span><span class='valuefld no-display'>" + value[i].main_cat_cd + "</span><span class='closebtn' onclick='removeChipFront(this,\"schmTp\")'>&times;</span></div>");

            }
        }
    } else if (type == "schmCd") {
        jQuery("#schmCdChip").empty();
        if (value.length > 0) {
            for (i = 0 ; i < value.length; i++) {
                jQuery("#schmCdChip").append("<div class='chip newchip'><span class='disfld'>" + value[i].main_cat_desc + "</span><span class='valuefld no-display'>" + value[i].main_cat_cd + "</span><span class='closebtn' onclick='removeChipFront(this,\"schmCd\")'>&times;</span></div>");

            }
        }
    } else if (type == "ptypeCd") {
        jQuery("#ptypeCdChip").empty();
        if (value.length > 0) {
            for (i = 0 ; i < value.length; i++) {
                jQuery("#ptypeCdChip").append("<div class='chip newchip'><span class='disfld'>" + value[i].main_cat_desc + "</span><span class='valuefld no-display'>" + value[i].main_cat_cd + "</span><span class='closebtn' onclick='removeChipFront(this,\"ptypeCd\")'>&times;</span></div>");

            }
        }
    } else if (type == "bank") {
        jQuery("#bankChip").empty();
        if (value.length > 0) {
            for (i = 0 ; i < value.length; i++) {
                jQuery("#bankChip").append("<div class='chip newchip'><span class='disfld'>" + value[i].srtp_desc + "</span><span class='valuefld no-display'>" + value[i].srtp_cd + "</span><span class='closebtn' onclick='removeChipFront(this,\"bank\")'>&times;</span></div>");

            }
        }
    }
}
function removeChipFront(value, type) {
    var val = jQuery(value).siblings(".valuefld").html();

    if (val != "") {
        jQuery.ajax({
            type: "GET",
            url: "/CustomerAnalysis/removeSelected",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: { type: type, value: val },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        updateVals(type, result.selecteditem);
                    }
                }
            }
        });
    }
}
function searchChannel() {

    jQuery(".chnl-search").click(function (evt) {
        if (!jQuery('#allchnls').is(":checked")) {
            var headerKeys = Array();
            headerKeys = ["Row", "Code", "Description", ""];
            field = "srchMulChnl";
            var checkcom = "";
            var i = 0;
            jQuery('input[name="selectedcompany"]:checked').each(function () {
                i = i + 1;
                if (this.value != "") {
                    checkcom += (i == jQuery('input[name="selectedcompany"]:checked').length) ? this.value : this.value + ',';
                }
            });
            if (checkcom == "") {
                setInfoMsgPopUp("Plese select company.");
            } else {
                data = { com: checkcom }
                var x = new CommonSearch(headerKeys, field, data);
            }
        }
    });
}
function searchSubChannel() {

    jQuery(".sub-chnl-search").click(function (evt) {
        if (!jQuery('#allsubchnls').is(":checked")) {
            var headerKeys = Array();
            headerKeys = ["Row", "Code", "Description", ""];
            field = "srchMulSubChnl";
            var checkcom = "";
            var i = 0;
            jQuery('input[name="selectedcompany"]:checked').each(function () {
                i = i + 1;
                if (this.value != "") {
                    checkcom += (i == jQuery('input[name="selectedcompany"]:checked').length) ? this.value : this.value + ',';
                }
            });
            if (checkcom == "") {
                setInfoMsgPopUp("Plese select company.");
            } else {
                data = { com: checkcom }
                var x = new CommonSearch(headerKeys, field, data);
            }
        }
    });


}
function searchArea() {

    jQuery(".area-search").click(function (evt) {
        if (!jQuery('#AllArea').is(":checked")) {
            var headerKeys = Array();
            headerKeys = ["Row", "Code", "Description", ""];
            field = "srchMulArea";
            var checkcom = "";
            var i = 0;
            jQuery('input[name="selectedcompany"]:checked').each(function () {
                i = i + 1;
                if (this.value != "") {
                    checkcom += (i == jQuery('input[name="selectedcompany"]:checked').length) ? this.value : this.value + ',';
                }
            });
            if (checkcom == "") {
                setInfoMsgPopUp("Plese select company.");
            } else {
                data = { com: checkcom }
                var x = new CommonSearch(headerKeys, field, data);
            }
        }
    });
}
function searchRegion() {

    jQuery(".region-search").click(function (evt) {
        if (!jQuery('#allregns').is(":checked")) {
            var headerKeys = Array();
            headerKeys = ["Row", "Code", "Description", ""];
            field = "srchMulRegion";
            var checkcom = "";
            var i = 0;
            jQuery('input[name="selectedcompany"]:checked').each(function () {
                i = i + 1;
                if (this.value != "") {
                    checkcom += (i == jQuery('input[name="selectedcompany"]:checked').length) ? this.value : this.value + ',';
                }
            });
            if (checkcom == "") {
                setInfoMsgPopUp("Plese select company.");
            } else {
                data = { com: checkcom }
                var x = new CommonSearch(headerKeys, field, data);
            }
        }
    });
}
function searchZone() {

    jQuery(".zone-search").click(function (evt) {
        if (!jQuery('#allzones').is(":checked")) {
            var headerKeys = Array();
            headerKeys = ["Row", "Code", "Description", ""];
            field = "srchMulZone";
            var checkcom = "";
            var i = 0;
            jQuery('input[name="selectedcompany"]:checked').each(function () {
                i = i + 1;
                if (this.value != "") {
                    checkcom += (i == jQuery('input[name="selectedcompany"]:checked').length) ? this.value : this.value + ',';
                }
            });
            if (checkcom == "") {
                setInfoMsgPopUp("Plese select company.");
            } else {
                data = { com: checkcom }
                var x = new CommonSearch(headerKeys, field, data);
            }
        }
    });
}
function searchPC() {

    jQuery(".pc-search").click(function (evt) {
        if (!jQuery('#AllPC').is(":checked")) {
            var headerKeys = Array();
            headerKeys = ["Row", "Code", "Description", ""];
            field = "srchMulPc";
            var checkcom = "";
            var i = 0;
            jQuery('input[name="selectedcompany"]:checked').each(function () {
                i = i + 1;
                if (this.value != "") {
                    checkcom += (i == jQuery('input[name="selectedcompany"]:checked').length) ? this.value : this.value + ',';
                }
            });
            if (checkcom == "") {
                setInfoMsgPopUp("Plese select company.");
            } else {
                data = { com: checkcom }
                var x = new CommonSearch(headerKeys, field, data);
            }
        }
    });
}
function loadCompany() {

    jQuery.ajax({
        type: "GET",
        url: "/ChnlWiseSales/loadCompany",
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
                                    '<td>' + result.companyList[i].SEC_COM_CD + '</td>' +
                                    '<td>' + result.companyList[i].MasterComp.Mc_desc + '</td>' +
                                    '</tr>');
                        }
                        jQuery(".select-company").click(function () {
                            if (!jQuery('#chkmultiple').is(":checked")) {
                                jQuery(".select-company").prop("checked", false);
                                jQuery(this).prop("checked", true);
                            }
                            if (jQuery('[name="selectedcompany"]:checked').length == result.companyList.length) {
                                jQuery("#chkAllCompany").prop("checked", true);
                            } else {
                                jQuery("#chkAllCompany").prop("checked", false);
                            }
                        });

                    } else {
                        jQuery('.profcen-display-tbl').append('<tr class="new-row">' + "<td style='border:none; color: #ff6666; position: absolute; width: 80%; font-weight: bold;'>No company found for display</td>" + '</tr>');
                    }
                    jQuery("#chkAllCompany").attr("disabled", true);
                    jQuery("#chkmultiple").click(function () {
                        if (jQuery('#chkmultiple').is(":checked")) {
                            jQuery("#chkAllCompany").removeAttr("disabled");
                        } else {
                            jQuery("#chkAllCompany").attr("disabled", true);
                            jQuery("#chkAllCompany").prop("checked", false);
                            jQuery(".select-company").prop("checked", false);
                        }
                    });

                    //jQuery("#chkAllCompany").click(function () {
                    //    if (jQuery('#chkmultiple').is(":checked")) {
                    //        if (jQuery('#chkAllCompany').is(":checked")) {
                    //            jQuery(".select-company").prop("checked", true);
                    //        } else {
                    //            jQuery(".select-company").prop("checked", false);
                    //        }
                    //    }
                    //});
                } else {
                    jQuery('.profcen-display-tbl').append('<tr class="new-row">' + "<td style='border:none; color: #ff6666; position: absolute; width: 80%; font-weight: bold;'>No company found for display</td>" + '</tr>');
                    setError(result.msg);
                }
            } else {
                Logout();
            }
        }
    });
}

function pagingTable(total, page, test) {
    jQuery('#pagingAnalysis').bootpag({
        total: total,
        page: page,
        maxVisible: 10,
        leaps: true,
        firstLastUse: true,
        first: '←',
        last: '→',
        wrapClass: 'pagination',
        activeClass: 'active',
        disabledClass: 'disabled',
        nextClass: 'next',
        prevClass: 'prev',
        lastClass: 'last',
        firstClass: 'first'
    }).on("page", function (event, num) {
        if (test == true) {
            var pgeSize = jQuery('.cls-no-Page').val();
            pagingCon(num, pgeSize);
        }
    });
}
function pagingTableInv(total, page, test) {
    jQuery('#pagingAnalysisinv').bootpag({
        total: total,
        page: page,
        maxVisible: 10,
        leaps: true,
        firstLastUse: true,
        first: '←',
        last: '→',
        wrapClass: 'pagination',
        activeClass: 'active',
        disabledClass: 'disabled',
        nextClass: 'next',
        prevClass: 'prev',
        lastClass: 'last',
        firstClass: 'first'
    }).on("page", function (event, num) {
        if (test == true) {
            var pgeSize = jQuery('.cls-no-Pageinv').val();
            pagingConInv(num, pgeSize);
        }
    });
}
var second = true;
function pagingCon(pgeNum, pgeSize) {
    jQuery(".result-display-tbl .new-row").remove();
    pgeSize = jQuery('.cls-no-Page').val();
    jQuery.ajax({
        type: "GET",
        url: "/CustomerAnalysis/itmDetPaging?pgeNum=" + pgeNum + "&pgeSize=" + pgeSize,
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        //data: { pgeNum: pgeNum, pgeSize: pgeSize },
        success: function (result) {
            if (result.login == true) {
                if (result.success == true) {
                    if (result.itmDetailsList.length > 0) {
                        jQuery(".export-btn").show();
                        var age = $("input[name='filterby']:checked").val();
                        //if (age == "item") {
                        for (i = 0; i < result.itmDetailsList.length; i++) {
                            var para = "'" + result.itmDetailsList[i].MOBILE + "'" + "," + "'" + result.itmDetailsList[i].NAME + "'";
                            jQuery('table.result-display-tbl tbody').append('<tr class="new-row">' +
                            //'<td>' + result.itmDetailsList[i].PC + '</td>' +
                            //'<td>' + result.itmDetailsList[i].PC_TOWN + '</td>' +
                            //'<td>' + result.itmDetailsList[i].ITEM_CODE + '</td>' +
                            //'<td>' + result.itmDetailsList[i].MODEL + '</td>' +
                            //'<td>' + result.itmDetailsList[i].BRAND + '</td>' +
                            //'<td>' + result.itmDetailsList[i].CATEGORY_1 + '</td>' +
                            //'<td>' + result.itmDetailsList[i].CATEGORY_2 + '</td>' +
                            //'<td>' + result.itmDetailsList[i].CATEGORY_3 + '</td>' +
                            //'<td>' + result.itmDetailsList[i].INVOICE_NO + '</td>' +
                            //'<td>' + getFormatedDateInput(result.itmDetailsList[i].INVOICE_DATE) + '</td>' +
                            //'<td class="float-right">' + addCommas(result.itmDetailsList[i].AMOUNT) + '</td>' +
                            '<td>' + result.itmDetailsList[i].CUSTOMER_CODE + '</td>' +
                            '<td>' + result.itmDetailsList[i].NAME + '</td>' +
                            '<td>' + result.itmDetailsList[i].ADDRESS + '</td>' +
                            '<td>' + result.itmDetailsList[i].MOBILE + '</td>' +
                            '<td>' + result.itmDetailsList[i].EMAIL + '</td>' +
                            '<td>' + result.itmDetailsList[i].TOWN + '</td>' +
                            '<td>' + result.itmDetailsList[i].NATIONALTY + '</td>' +
                            //'<td>' + result.itmDetailsList[i].DISTRICT + '</td>' +
                            //'<td>' + result.itmDetailsList[i].PROVINCE + '</td>' +

                            '<td><input type="button" value="Send SMS" class="btn btn-sm send-sms" onclick="message(' + para + ')" /></td>' +
                '</tr>');
                        }
                        //}
                        //else {
                        //    for (i = 0; i < result.itmDetailsList.length; i++) {
                        //        jQuery('table.result-display-tbl tbody').append('<tr class="new-row">' +
                        //            '<td>' + result.itmDetailsList[i].PC + '</td>' +
                        //            '<td>' + result.itmDetailsList[i].PC_TOWN + '</td>' +
                        //            '<td class="float-right">' + result.itmDetailsList[i].INVOICE_NO + '</td>' +
                        //            '<td>' + getFormatedDateInput(result.itmDetailsList[i].INVOICE_DATE) + '</td>' +
                        //            '<td class="float-right">' + addCommas(result.itmDetailsList[i].AMOUNT) + '</td>' +
                        //            '<td class="float-right">' + result.itmDetailsList[i].CUSTOMER_CODE + '</td>' +
                        //            '<td class="float-right">' + result.itmDetailsList[i].NAME + '</td>' +
                        //            '<td class="float-right">' + result.itmDetailsList[i].ADDRESS + '</td>' +
                        //            '<td class="float-right">' + result.itmDetailsList[i].MOBILE + '</td>' +
                        //             '<td class="float-right">' + result.itmDetailsList[i].EMAIL + '</td>' +
                        //             '<td class="float-right">' + result.itmDetailsList[i].TOWN + '</td>' +
                        //             '<td class="float-right">' + result.itmDetailsList[i].NATIONALTY + '</td>' +
                        //'</tr>');
                        //    }
                        //}

                        if (second == true) {
                            pagingTable(result.pageData, pgeNum, true);
                            second = false;
                        }

                        pagingTable(result.pageData, pgeNum, false);

                    }
                    else {
                        setInfoMsg("No data found");
                    }

                }
            } else {
                Logout();
            }
        }
    });
}
function pagingConInv(pgeNum, pgeSize) {
    jQuery(".result-display-tbl .new-row").remove();
    pgeSize = jQuery('.cls-no-Pageinv').val();
    jQuery.ajax({
        type: "GET",
        url: "/CustomerAnalysis/itmDetPaging?pgeNum=" + pgeNum + "&pgeSize=" + pgeSize,
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        //data: { pgeNum: pgeNum, pgeSize: pgeSize },
        success: function (result) {
            if (result.login == true) {
                if (result.success == true) {
                    if (result.itmDetailsList.length > 0) {
                        jQuery(".export-btn").show();
                        var age = $("input[name='filterby']:checked").val();
                        //if (age == "item") {
                        for (i = 0; i < result.itmDetailsList.length; i++) {
                            var para = "'" + result.itmDetailsList[i].MOBILE + "'" + "," + "'" + result.itmDetailsList[i].NAME + "'";
                            jQuery('table.result-display-tbl tbody').append('<tr class="new-row">' +
                            //'<td>' + result.itmDetailsList[i].PC + '</td>' +
                            //'<td>' + result.itmDetailsList[i].PC_TOWN + '</td>' +
                            //'<td>' + result.itmDetailsList[i].ITEM_CODE + '</td>' +
                            //'<td>' + result.itmDetailsList[i].MODEL + '</td>' +
                            //'<td>' + result.itmDetailsList[i].BRAND + '</td>' +
                            //'<td>' + result.itmDetailsList[i].CATEGORY_1 + '</td>' +
                            //'<td>' + result.itmDetailsList[i].CATEGORY_2 + '</td>' +
                            //'<td>' + result.itmDetailsList[i].CATEGORY_3 + '</td>' +
                            //'<td>' + result.itmDetailsList[i].INVOICE_NO + '</td>' +
                            //'<td>' + getFormatedDateInput(result.itmDetailsList[i].INVOICE_DATE) + '</td>' +
                            //'<td class="float-right">' + addCommas(result.itmDetailsList[i].AMOUNT) + '</td>' +
                            '<td>' + result.itmDetailsList[i].CUSTOMER_CODE + '</td>' +
                            '<td>' + result.itmDetailsList[i].NAME + '</td>' +
                            '<td>' + result.itmDetailsList[i].ADDRESS + '</td>' +
                            '<td>' + result.itmDetailsList[i].MOBILE + '</td>' +
                            '<td>' + result.itmDetailsList[i].EMAIL + '</td>' +
                            '<td>' + result.itmDetailsList[i].TOWN + '</td>' +
                            '<td>' + result.itmDetailsList[i].NATIONALTY + '</td>' +
                            //'<td>' + result.itmDetailsList[i].DISTRICT + '</td>' +
                            //'<td>' + result.itmDetailsList[i].PROVINCE + '</td>' +

                            '<td><input type="button" value="Send SMS" class="btn btn-sm send-sms" onclick="message(' + para + ')" /></td>' +
                '</tr>');
                        }
                        //}
                        //else {
                        //    for (i = 0; i < result.itmDetailsList.length; i++) {
                        //        jQuery('table.result-display-tbl tbody').append('<tr class="new-row">' +
                        //            '<td>' + result.itmDetailsList[i].PC + '</td>' +
                        //            '<td>' + result.itmDetailsList[i].PC_TOWN + '</td>' +
                        //            '<td class="float-right">' + result.itmDetailsList[i].INVOICE_NO + '</td>' +
                        //            '<td>' + getFormatedDateInput(result.itmDetailsList[i].INVOICE_DATE) + '</td>' +
                        //            '<td class="float-right">' + addCommas(result.itmDetailsList[i].AMOUNT) + '</td>' +
                        //            '<td class="float-right">' + result.itmDetailsList[i].CUSTOMER_CODE + '</td>' +
                        //            '<td class="float-right">' + result.itmDetailsList[i].NAME + '</td>' +
                        //            '<td class="float-right">' + result.itmDetailsList[i].ADDRESS + '</td>' +
                        //            '<td class="float-right">' + result.itmDetailsList[i].MOBILE + '</td>' +
                        //             '<td class="float-right">' + result.itmDetailsList[i].EMAIL + '</td>' +
                        //             '<td class="float-right">' + result.itmDetailsList[i].TOWN + '</td>' +
                        //             '<td class="float-right">' + result.itmDetailsList[i].NATIONALTY + '</td>' +
                        //'</tr>');
                        //    }
                        //}

                        if (second == true) {
                            pagingTableInv(result.pageData, pgeNum, true);
                            second = false;
                        }

                        pagingTableInv(result.pageData, pgeNum, false);

                    }
                    else {
                        setInfoMsg("No data found");
                    }

                }
            } else {
                Logout();
            }
        }
    });
}


function message(mobile, name) {
    if (mobile == "" || mobile == "#" || mobile == "N/A") {
        setInfoMsg("No Mobile Number Found");
    }
    else {
        $('#smsModal').modal('show');
        // console.log(mobile + name);
        document.getElementById('mobileNumber').value = mobile;
        document.getElementById('customerName').value = name;
    }
}
jQuery(".btn.sms-btn").click(function (evt) {
    evt.preventDefault();
    var mobileNo = document.getElementById('mobileNumber').value;
    var sms = document.getElementById('sms').value;
    var name = document.getElementById('customerName').value;
    if (sms == "") {
        setError("Message area should not be empty!");

    }
    else {
        jQuery.ajax({
            type: "POST",
            data: JSON.stringify({ mobileNumber: mobileNo, sms: sms, name: name }),
            url: "/CustomerAnalysis/SendSMS",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        $('#smsModal').modal('hide');
                        //$('#smsModal').modal({
                        //    show: true,
                        //    backdrop: 'static',
                        //    keyboard: false
                        //});
                        //showSuccess("Message has been sent to, " + mobileNo + " , " + name);
                        setSuccesssMsg("Message has been sent to, " + mobileNo + " , " + name);
                    }
                    if (result.success == false) {
                        alert('sdfsfsdfsdfsdfds');
                        // $('#smsModal').modal('hide');
                        setError("Message has been not sent");
                    }
                } else {
                    Logout();
                }
            }
        });
    }
});

jQuery(".btn.sms-btn-all").click(function (evt) {
    evt.preventDefault();
    var smstoall = document.getElementById('smstoall').value
    if (smstoall == "") {
        setError("Message area should not be empty!");

    }
    else {
        jQuery.ajax({
            type: "POST",
            data: JSON.stringify({ sms: smstoall }),
            url: "/CustomerAnalysis/SendSMStoAll",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        $('#smsModalAll').modal('hide');
                        setSuccesssMsg("Messages have been sent");
                        jQuery("#smstoallbtn").addClass('disabled');
                        jQuery("#smstoallinvbtn").addClass('disabled');
                        document.getElementById('smstoall').value = "";
                    }
                    if (result.success == false) {
                        // $('#smsModal').modal('hide');
                        setError("Message has been not sent");
                    }
                } else {
                    Logout();
                }
            }
        });
    }
});

function addFiltersFront(value, type) {
    var valVal = $(value).attr("value");
    var valId = $(value).attr("id");
    jQuery.ajax({
        type: "GET",
        url: "/CustomerAnalysis/addSelectedValues",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: { type: type, value: valId, desc: valVal },
        success: function (result) {
            if (result.login == true) {
                if (result.success == true) {
                    updateVals(type, result.selecteditem);
                }
            }
        }
    });


    //var newID = "chip_" + valId;

    //var myElem = document.getElementById(newID);
    //if (myElem)
    //    setInfoMsg("Already added!");
    //else {
    //    switch (type) {
    //        case "cat4":
    //            $("#cat4Chip").append("<div class='chip newchip' id='" + newID + "' subject= '" + valVal + "' >" + valVal + "<span class='closebtn' onclick='removeChipFront(this)'>&times;</span></div>");
    //            break;
    //        case "cat5":
    //            $("#cat5Chip").append("<div class='chip newchip' id='" + newID + "' subject= '" + valVal + "' >" + valVal + "<span class='closebtn' onclick='removeChipFront(this)'>&times;</span></div>");
    //            break;
    //        case "circular":
    //            $("#cirChip").append("<div class='chip newchip' id='" + newID + "' subject= '" + valVal + "' >" + valVal + "<span class='closebtn' onclick='removeChipFront(this)'>&times;</span></div>");
    //            break;
    //        case "schmTp":
    //            $("#schmTpChip").append("<div class='chip newchip' id='" + newID + "' subject= '" + valVal + "' >" + valVal + "<span class='closebtn' onclick='removeChipFront(this)'>&times;</span></div>");
    //            break;
    //        case "schmCd":
    //            $("#schmCdChip").append("<div class='chip newchip' id='" + newID + "' subject= '" + valVal + "' >" + valVal + "<span class='closebtn' onclick='removeChipFront(this)'>&times;</span></div>");
    //            break;
    //        case "ptypeCd":
    //            $("#ptypeCdChip").append("<div class='chip newchip' id='" + newID + "' subject= '" + valVal + "' >" + valVal + "<span class='closebtn' onclick='removeChipFront(this)'>&times;</span></div>");
    //            break;
    //        case "priBk":
    //            $("#priBkChip").append("<div class='chip newchip' id='" + value + "' subject= '" + value + "' >" + value + "<span class='closebtn' onclick='removeChipFront(this)'>&times;</span></div>");
    //            break;
    //        case "priLvl":
    //            $("#priLvlChip").append("<div class='chip newchip' id='" + value + "' subject= '" + value + "' >" + value + "<span class='closebtn' onclick='removeChipFront(this)'>&times;</span></div>");
    //            break;
    //        case "invSubTp":
    //            $("#invSubTpChip").append("<div class='chip newchip' id='" + newID + "' subject= '" + valVal + "' >" + valVal + "<span class='closebtn' onclick='removeChipFront(this)'>&times;</span></div>");
    //            break;
    //        case "dlvCus":
    //            $("#dlvCustChip").append("<div class='chip newchip' id='" + newID + "' subject= '" + valVal + "' >" + valVal + "<span class='closebtn' onclick='removeChipFront(this)'>&times;</span></div>");
    //            break;
    //        case "exec":
    //            $("#execChip").append("<div class='chip newchip' id='" + newID + "' subject= '" + valVal + "' >" + valVal + "<span class='closebtn' onclick='removeChipFront(this)'>&times;</span></div>");
    //            break;
    //        case "pc":
    //            $("#PCChip").append("<div class='chip newchip' id='" + newID + "' subject= '" + valVal + "' >" + valVal + "<span class='closebtn' onclick='removeChipFront(this)'>&times;</span></div>");
    //            break;
    //        case "expc":
    //            $("#exPCChip").append("<div class='chip newchip' id='" + newID + "' subject= '" + valVal + "' >" + valVal + "<span class='closebtn' onclick='removeChipFront(this)'>&times;</span></div>");
    //            break;
    //        case "area":
    //            $("#areaChip").append("<div class='chip newchip' id='" + newID + "' subject= '" + valVal + "' >" + valVal + "<span class='closebtn' onclick='removeChipFront(this)'>&times;</span></div>");
    //            break;
    //        case "com":
    //            $("#comChip").append("<div class='chip newchip' id='" + newID + "' subject= '" + valVal + "' >" + valVal + "<span class='closebtn' onclick='removeChipFront(this)'>&times;</span></div>");
    //            break;
    //        case "chnl":
    //            $("#chnlChip").append("<div class='chip newchip' id='" + newID + "' subject= '" + valVal + "' >" + valVal + "<span class='closebtn' onclick='removeChipFront(this)'>&times;</span></div>");
    //            break;
    //        case "schnl":
    //            $("#schnlChip").append("<div class='chip newchip' id='" + newID + "' subject= '" + valVal + "' >" + valVal + "<span class='closebtn' onclick='removeChipFront(this)'>&times;</span></div>");
    //            break;
    //        case "region":
    //            $("#regnChip").append("<div class='chip newchip' id='" + newID + "' subject= '" + valVal + "' >" + valVal + "<span class='closebtn' onclick='removeChipFront(this)'>&times;</span></div>");
    //            break;
    //        case "zone":
    //            $("#zoneChip").append("<div class='chip newchip' id='" + newID + "' subject= '" + valVal + "' >" + valVal + "<span class='closebtn' onclick='removeChipFront(this)'>&times;</span></div>");
    //            break;
    //        case "item":
    //            $("#itmChip").append("<div class='chip newchip' id='" + newID + "' subject= '" + valVal + "' >" + valVal + "<span class='closebtn' onclick='removeChipFront(this)'>&times;</span></div>");
    //            break;
    //        case "ctown":
    //            $("#ctownChip").append("<div class='chip newchip' id='" + newID + "' subject= '" + valVal + "' >" + valVal + "<span class='closebtn' onclick='removeChipFront(this)'>&times;</span></div>");
    //            break;
    //        case "ptown":
    //            $("#ptownChip").append("<div class='chip newchip' id='" + newID + "' subject= '" + valVal + "' >" + valVal + "<span class='closebtn' onclick='removeChipFront(this)'>&times;</span></div>");
    //            break;
    //        case "bank":
    //            $("#bankChip").append("<div class='chip newchip' id='" + newID + "' subject= '" + valVal + "' >" + valVal + "<span class='closebtn' onclick='removeChipFront(this)'>&times;</span></div>");
    //            break;
    //        case "model":
    //            $("#modelChip").append("<div class='chip newchip' id='" + newID + "' subject= '" + valVal + "' >" + valVal + "<span class='closebtn' onclick='removeChipFront(this)'>&times;</span></div>");
    //            break;
    //        case "brnd":
    //            $("#brndChip").append("<div class='chip newchip' id='" + newID + "' subject= '" + valVal + "' >" + valVal + "<span class='closebtn' onclick='removeChipFront(this)'>&times;</span></div>");
    //            break;
    //        case "cat2":
    //            $("#cat2Chip").append("<div class='chip newchip' id='" + newID + "' subject= '" + valVal + "' >" + valVal + "<span class='closebtn' onclick='removeChipFront(this)'>&times;</span></div>");
    //            break;
    //        case "cat3":
    //            $("#cat3Chip").append("<div class='chip newchip' id='" + newID + "' subject= '" + valVal + "' >" + valVal + "<span class='closebtn' onclick='removeChipFront(this)'>&times;</span></div>");
    //            break;
    //        case "cat1":
    //            $("#cat1Chip").append("<div class='chip newchip' id='" + newID + "' subject= '" + valVal + "' >" + valVal + "<span class='closebtn' onclick='removeChipFront(this)'>&times;</span></div>");
    //            break;
    //        case "brnd1":
    //            $("#brnd1Chip").append("<div class='chip newchip' id='" + newID + "' subject= '" + valVal + "' >" + valVal + "<span class='closebtn' onclick='removeChipFront(this)'>&times;</span></div>");
    //            break;
    //        case "cat21":
    //            $("#cat21Chip").append("<div class='chip newchip' id='" + newID + "' subject= '" + valVal + "' >" + valVal + "<span class='closebtn' onclick='removeChipFront(this)'>&times;</span></div>");
    //            break;
    //        case "cat31":
    //            $("#cat31Chip").append("<div class='chip newchip' id='" + newID + "' subject= '" + valVal + "' >" + valVal + "<span class='closebtn' onclick='removeChipFront(this)'>&times;</span></div>");
    //            break;
    //        case "cat11":
    //            $("#cat11Chip").append("<div class='chip newchip' id='" + newID + "' subject= '" + valVal + "' >" + valVal + "<span class='closebtn' onclick='removeChipFront(this)'>&times;</span></div>");
    //            break;
    //        case "cust":
    //            $("#custChip").append("<div class='chip newchip' id='" + newID + "' subject= '" + valVal + "' >" + valVal + "<span class='closebtn' onclick='removeChipFront(this)'>&times;</span></div>");
    //            break;
    //        case "invTyp":
    //            $("#invTpChip").append("<div class='chip newchip' id='" + newID + "' subject= '" + valVal + "' >" + valVal + "<span class='closebtn' onclick='removeChipFront(this)'>&times;</span></div>");
    //            break;
    //        default:
    //            break;
    //    }
    //}

}

function addColumnsFront(column) {
    var colVal = $(column).attr("value");
    var colId = $(column).attr("id");
    var newID = "chip_" + colId;

    var myElem = document.getElementById(newID);
    if (myElem)
        setInfoMsg("Already added!");
    else {
        $("#colChip").append("<div class='chip newchip' id='" + newID + "' subject='" + colVal + "'>" + colVal + "<span class='closebtn' onclick='removeChipFront(this)'>&times;</span></div>");
    }

}



function addRowsFront(row) {
    var rowVal = $(row).attr("value");
    var rowId = $(row).attr("id");
    var newID = "chip_" + rowId;

    var myElem = document.getElementById(newID);
    if (myElem)
        setInfoMsg("Already added!");
    else {
        $("#rowChip").append("<div class='chip newchip' id='" + newID + "' subject= '" + rowVal + "' >" + rowVal + "<span class='closebtn' onclick='removeChipFront(this)'>&times;</span></div>");
    }

}

//function addValuesFront(value) {
//    var valVal = $(value).attr("value");
//    var valId = $(value).attr("id");
//    var newID = "chip_" + valId;

//    var myElem = document.getElementById(newID);
//    if (myElem)
//        setInfoMsg("Already added!");
//    else {
//        $("#valChip").append("<div class='chip newchip' id='" + newID + "' subject= '" + valVal + "' >" + valVal + "<span class='closebtn' onclick='removeChipFront(this)'>&times;</span></div>");
//    }

//}



function searchItemCodes() {
    jQuery("#ItemCode").on("keydown", function (evt) {
        if (evt.keyCode == 13) {
            if (!jQuery('#allitems').is(":checked")) {
                var headerKeys = Array();
                headerKeys = ["Row", "Code", "Description", ""];
                field = "srchItemsMUserDef";
                var data = {
                    chnl: jQuery("#SaleType").val(),
                    type: "sub_channel"
                }
                var x = new CommonSearch(headerKeys, field, data);
            }
        }
    });
    jQuery(".itemCode-search").click(function (evt) {
        if (!jQuery('#allitems').is(":checked")) {
            var headerKeys = Array();
            headerKeys = ["Row", "Code", "Description", ""];
            field = "srchItemsMUserDef";
            var data = {
                chnl: jQuery("#ItemCode").val(),
                type: "sub_channel"
            }
            var x = new CommonSearch(headerKeys, field, data);
        }
    });
}

function searchCTown() {
    jQuery("#TownCode").on("keydown", function (evt) {
        if (evt.keyCode == 13) {
            if (!jQuery('#AllCtowns').is(":checked")) {
                var headerKeys = Array();
                headerKeys = ["Row", "Code", "Description", ""];
                field = "srchTown";
                var data = {
                    chnl: jQuery("#SaleType").val(),
                    type: "sub_channel"
                }
                var x = new CommonSearch(headerKeys, field, data);
            }
        }
    });
    jQuery(".townCode-search").click(function (evt) {
        if (!jQuery('#AllCtowns').is(":checked")) {
            var headerKeys = Array();
            headerKeys = ["Row", "Code", "Description", ""];
            field = "srchTown";
            var data = {
                chnl: jQuery("#TownCode").val(),
                type: "sub_channel"
            }
            var x = new CommonSearch(headerKeys, field, data);
        }
    });
}

function searchPTown() {
    jQuery("#PTownCode").on("keydown", function (evt) {
        if (evt.keyCode == 13) {
            if (!jQuery('#AllPtowns').is(":checked")) {
                var headerKeys = Array();
                headerKeys = ["Row", "Code", "Description", ""];
                field = "srchPTown";
                var data = {
                    chnl: jQuery("#SaleType").val(),
                    type: "sub_channel"
                }
                var x = new CommonSearch(headerKeys, field, data);
            }
        }
    });
    jQuery(".ptownCode-search").click(function (evt) {
        if (!jQuery('#AllPtowns').is(":checked")) {
            var headerKeys = Array();
            headerKeys = ["Row", "Code", "Description", ""];
            field = "srchPTown";
            var data = {
                chnl: jQuery("#PTownCode").val(),
                type: "sub_channel"
            }
            var x = new CommonSearch(headerKeys, field, data);
        }
    });
}

function searchPDistrict() {
    jQuery("#PDistCode").on("keydown", function (evt) {
        if (evt.keyCode == 13) {
            if (!jQuery('#AllPdists').is(":checked")) {
                var headerKeys = Array();
                headerKeys = ["Row", "Code", "Description", ""];
                field = "srchPDist";
                var data = {
                    chnl: jQuery("#SaleType").val(),
                    type: "sub_channel"
                }
                var x = new CommonSearch(headerKeys, field, data);
            }
        }
    });
    jQuery(".pdistCode-search").click(function (evt) {
        if (!jQuery('#AllPdists').is(":checked")) {
            var headerKeys = Array();
            headerKeys = ["Row", "Code", "Description", ""];
            field = "srchPDist";
            var data = {
                chnl: jQuery("#PDistCode").val(),
                type: "sub_channel"
            }
            var x = new CommonSearch(headerKeys, field, data);
        }
    });
}

function searchPProvince() {
    jQuery("#PProvCode").on("keydown", function (evt) {
        if (evt.keyCode == 13) {
            if (!jQuery('#AllPprovs').is(":checked")) {
                var headerKeys = Array();
                headerKeys = ["Row", "Code", "Description", ""];
                field = "srchPProv";
                var data = {
                    chnl: jQuery("#SaleType").val(),
                    type: "sub_channel"
                }
                var x = new CommonSearch(headerKeys, field, data);
            }
        }
    });
    jQuery(".pprovCode-search").click(function (evt) {
        if (!jQuery('#AllPprovs').is(":checked")) {
            var headerKeys = Array();
            headerKeys = ["Row", "Code", "Description", ""];
            field = "srchPProv";
            var data = {
                chnl: jQuery("#PProvCode").val(),
                type: "sub_channel"
            }
            var x = new CommonSearch(headerKeys, field, data);
        }
    });
}

function searchBank() {
    jQuery("#BankCode").on("keydown", function (evt) {
        if (evt.keyCode == 13) {
            var headerKeys = Array();
            headerKeys = ["Row", "Code", "Description"];
            field = "srchBank";

            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery(".bankcode-search").click(function (evt) {
        if (!jQuery('#AllBanks').is(":checked")) {
            var headerKeys = Array();
            headerKeys = ["Row", "Code", "Description", ""];
            field = "srchBank";
            var x = new CommonSearch(headerKeys, field);
        }
    });
}

function searchItemModel() {
    jQuery("#ItemModel").on("keydown", function (evt) {
        if (evt.keyCode == 13) {
            if (!jQuery('#allmodel').is(":checked")) {
                var headerKeys = Array();
                headerKeys = ["Row", "Code", "Description", ""];
                field = "srchItemModelMUserDef";
                var x = new CommonSearch(headerKeys, field);
            }
        }
    });

    jQuery(".model-search").click(function (evt) {
        if (!jQuery('#allmodel').is(":checked")) {
            var headerKeys = Array();
            headerKeys = ["Row", "Code", "Description", ""];
            field = "srchItemModelMUserDef";
            var x = new CommonSearch(headerKeys, field);
        }
    });
}

function searchBrand() {
    jQuery("#Brand").on("keydown", function (evt) {
        if (evt.keyCode == 13) {
            if (!jQuery('#allbrnd').is(":checked")) {
                var headerKeys = Array();
                headerKeys = ["Row", "Code", "Description", ""];
                var data = jQuery("#BrandMngr").val();
                field = "srchItemBrandMUserDef";
                var resData = new CommonSearch(headerKeys, field, data);
            }
        }
    });

    jQuery(".brnd-search").click(function (evt) {
        if (!jQuery('#allbrnd').is(":checked")) {
            var headerKeys = Array();
            headerKeys = ["Row", "Code", "Description", ""];
            var data = jQuery("#BrandMngr").val();
            field = "srchItemBrandMUserDef";
            var resData = new CommonSearch(headerKeys, field, data);
        }
    });
}

function searchMainCat() {
    jQuery("#MainCat").on("keydown", function (evt) {
        if (evt.keyCode == 13) {
            if (!jQuery('#allmaincat').is(":checked")) {
                var headerKeys = Array();
                headerKeys = ["Row", "Code", "Description", ""];
                field = "srchMainCatMUserDef";
                var x = new CommonSearch(headerKeys, field);
            }
        }
    });

    jQuery(".main-cat-search").click(function (evt) {
        if (!jQuery('#allmaincat').is(":checked")) {
            var headerKeys = Array();
            headerKeys = ["Row", "Code", "Description", ""];
            field = "srchMainCatMUserDef";
            var x = new CommonSearch(headerKeys, field);
        }
    });
}

function searchCategory2() {
    jQuery("#Category2").on("keydown", function (evt) {
        if (evt.keyCode == 13) {
            if (!jQuery('#allcategory2').is(":checked")) {
                var headerKeys = Array();
                headerKeys = ["Row", "Code", "Description", ""];
                field = "srchCategory2MUserDef";
                var data = {
                    chnl: jQuery("#SaleType").val(),
                    sChnl: jQuery("#ItemCode").val(),
                    type: "category2"
                };
                var x = new CommonSearch(headerKeys, field, data);
            }
        }
    });

    jQuery(".category2-search").click(function (evt) {
        if (!jQuery('#allcategory2').is(":checked")) {
            var headerKeys = Array();
            headerKeys = ["Row", "Code", "Description", ""];
            field = "srchCategory2MUserDef";
            var data = {
                chnl: jQuery("#SaleType").val(),
                sChnl: jQuery("#ItemCode").val(),
                type: "Category2"
            };
            var x = new CommonSearch(headerKeys, field, data);
        }
    });
}

function searchCategory3() {
    jQuery("#Category3").on("keydown", function (evt) {
        if (evt.keyCode == 13) {
            if (!jQuery('#allcategory3').is(":checked")) {
                var headerKeys = Array();
                headerKeys = ["Row", "Code", "Description", ""];
                field = "srchCategory3MUserDef";
                var data = {
                    chnl: jQuery("#SaleType").val(),
                    sChnl: jQuery("#ItemCode").val(),
                    type: "category3"
                };
                var x = new CommonSearch(headerKeys, field, data);
            }
        }
    });

    jQuery(".category3-search").click(function (evt) {
        if (!jQuery('#allcategory3').is(":checked")) {
            var headerKeys = Array();
            headerKeys = ["Row", "Code", "Description", ""];
            field = "srchCategory3MUserDef";
            var data = {
                chnl: jQuery("#SaleType").val(),
                sChnl: jQuery("#ItemCode").val(),
                type: "Category3"
            };
            var x = new CommonSearch(headerKeys, field, data);
        }
    });
}

function searchCustomer() {
    jQuery("#GCE_CUS_CD").on("keydown", function (evt) {
        if (evt.keyCode == 13) {
            if (!jQuery('#AllCus').is(":checked")) {
                var headerKeys = Array();
                headerKeys = ["Row", "Code", "Name", "NIC", "Mobile No", "BR No", ""];
                field = "CustM";
                var x = new CommonSearch(headerKeys, field);
            }
        }
    });

    jQuery(".cus-search").click(function (evt) {
        if (!jQuery('#AllCus').is(":checked")) {
            var headerKeys = Array();
            headerKeys = ["Row", "Code", "Name", "NIC", "Mobile No", "BR No", ""];
            field = "CustM";
            var x = new CommonSearch(headerKeys, field);
        }
    });
}

function serachInvoiceTyps() {
    jQuery("#SaleType").on("keydown", function (evt) {
        if (evt.keyCode == 13) {
            if (!jQuery('#allsaletypes').is(":checked")) {
                var headerKeys = Array();
                headerKeys = ["Row", "Code", "Description", ""];
                field = "srchSaleTypsM";
                var x = new CommonSearch(headerKeys, field);
            }
        }
    });

    jQuery(".saleType-search").click(function (evt) {
        if (!jQuery('#allsaletypes').is(":checked")) {
            var headerKeys = Array();
            headerKeys = ["Row", "Code", "Description", ""];
            field = "srchSaleTypsM";
            var x = new CommonSearch(headerKeys, field);
        }
    });
}

function searchSchmTp() {
    jQuery("#schmTp").on("keydown", function (evt) {
        if (evt.keyCode == 13) {
            if (!jQuery('#AllSchmTp').is(":checked")) {
                var headerKeys = Array();
                headerKeys = ["Row", "Code", "Description", ""];
                field = "srchSchmTypM";
                var x = new CommonSearch(headerKeys, field);
            }
        }
    });

    jQuery(".schm-tp-search").click(function (evt) {
        if (!jQuery('#AllSchmTp').is(":checked")) {
            var headerKeys = Array();
            headerKeys = ["Row", "Code", "Description", ""];
            field = "srchSchmTypM";
            var x = new CommonSearch(headerKeys, field);
        }
    });
}

function searchSchmCd() {
    jQuery("#schmCd").on("keydown", function (evt) {
        if (evt.keyCode == 13) {
            if (!jQuery('#AllSchmCd').is(":checked")) {
                var headerKeys = Array();
                headerKeys = ["Row", "Code", "Description", ""];
                field = "srchSchmCdM";
                var x = new CommonSearch(headerKeys, field);
            }
        }
    });

    jQuery(".schm-cd-search").click(function (evt) {
        if (!jQuery('#AllSchmCd').is(":checked")) {
            var headerKeys = Array();
            headerKeys = ["Row", "Code", "Description", ""];
            field = "srchSchmCdM";
            var x = new CommonSearch(headerKeys, field);
        }
    });
}

function searchPaytyp() {
    jQuery("#ptypeCd").on("keydown", function (evt) {
        if (evt.keyCode == 13) {
            if (!jQuery('#AllPtypeCd').is(":checked")) {
                var headerKeys = Array();
                headerKeys = ["Row", "Code", "Description", ""];
                field = "srchPtypCdM";
                var x = new CommonSearch(headerKeys, field);
            }
        }
    });

    jQuery(".ptyp-cd-search").click(function (evt) {
        if (!jQuery('#AllPtypeCd').is(":checked")) {
            var headerKeys = Array();
            headerKeys = ["Row", "Code", "Description", ""];
            field = "srchPtypCdM";
            var x = new CommonSearch(headerKeys, field);
        }
    });
}
function loadAgeAndSalary() {
    jQuery.ajax({
        type: "GET",
        url: "/CustomerAnalysis/getAgeCategory",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: { type: "AGE" },
        success: function (result) {
            if (result.login == true) {
                if (result.success == true) {
                    jQuery('#CheckAge').empty();
                    var cont = "";
                    var json = jQuery.parseJSON(result.data);
                    cont += "<option value=''>SELECT</option>";
                    for (i = 0; i < json.length; i++) {
                        cont += "<option value=" + json[i].RBV_VAL + ">" + json[i].RBV_VAL + "</option>";
                    }
                    jQuery("#CheckAge").append(cont);
                }
            }
        }
    });
    jQuery.ajax({
        type: "GET",
        url: "/CustomerAnalysis/getAgeCategory",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: { type: "SALARY" },
        success: function (result) {
            if (result.login == true) {
                if (result.success == true) {
                    jQuery('#CheckSalary').empty();
                    var cont = "";
                    var json = jQuery.parseJSON(result.data);
                    cont += "<option value=''>SELECT</option>";
                    for (i = 0; i < json.length; i++) {
                        cont += "<option value=" + json[i].RBV_VAL + ">" + json[i].RBV_VAL + "</option>";
                    }
                    jQuery('#CheckSalary').append(cont);
                }
            }
        }
    });
}

//Added By Dulaj 2018-Mar-28

jQuery(".btn.grph-disply-btn-inv").click(function (evt) {
    evt.preventDefault();
    displayData();
});


pgeSize = jQuery('.cls-no-Pageinv').val();
function pagingConInv(pgeNum, pgeSize) {
    jQuery(".result-display-tbl .new-row").remove();
    //alert("KKKK");
    pgeSize = jQuery('.cls-no-Pageinv').val();
    jQuery.ajax({
        type: "GET",
        url: "/CustomerAnalysis/itmDetPaging?pgeNum=" + pgeNum + "&pgeSize=" + pgeSize,
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        //data: { pgeNum: pgeNum, pgeSize: pgeSize },
        success: function (result) {
            if (result.login == true) {
                if (result.success == true) {
                    if (result.itmDetailsList.length > 0) {
                        jQuery(".export-btn").show();
                        var age = $("input[name='filterby']:checked").val();
                        //if (age == "item") {
                        for (i = 0; i < result.itmDetailsList.length; i++) {
                            jQuery('table.result-display-tbl tbody').append('<tr class="new-row">' +
                               '<td>' + result.itmDetailsList[i].PC + '</td>' +
                                    '<td>' + result.itmDetailsList[i].PC_TOWN + '</td>' +
                                    '<td>' + result.itmDetailsList[i].ITEM_CODE + '</td>' +
                                    '<td>' + result.itmDetailsList[i].MODEL + '</td>' +
                                    '<td>' + result.itmDetailsList[i].BRAND + '</td>' +
                                    '<td>' + result.itmDetailsList[i].CATEGORY_1 + '</td>' +
                                    '<td>' + result.itmDetailsList[i].CATEGORY_2 + '</td>' +
                                    '<td>' + result.itmDetailsList[i].CATEGORY_3 + '</td>' +
                                    '<td>' + result.itmDetailsList[i].INVOICE_NO + '</td>' +
                                    //'<td>' + getFormatedDateInput(result.itmDetailsList[i].INVOICE_DATE) + '</td>' +
                                    '<td class="float-right">' + addCommas(result.itmDetailsList[i].AMOUNT) + '</td>' +
                                    '<td>' + result.itmDetailsList[i].CUSTOMER_CODE + '</td>' +
                                    '<td>' + result.itmDetailsList[i].NAME + '</td>' +
                                    '<td>' + result.itmDetailsList[i].ADDRESS + '</td>' +
                                    '<td>' + result.itmDetailsList[i].MOBILE + '</td>' +
                                    '<td>' + result.itmDetailsList[i].EMAIL + '</td>' +
                                    '<td>' + result.itmDetailsList[i].TOWN + '</td>' +
                                    '<td>' + result.itmDetailsList[i].NATIONALTY + '</td>' +
                                    //'<td>' + result.itmDetailsList[i].DISTRICT + '</td>' +
                                    //'<td>' + result.itmDetailsList[i].PROVINCE + '</td>' +


                    '</tr>');
                        }

                        if (second == true) {
                            pagingTableInv(result.pageData, pgeNum, true);
                            second = false;
                        }

                        pagingTableInv(result.pageData, pgeNum, false);

                    }
                    else {
                        setInfoMsg("No data found");
                    }

                }
            } else {
                Logout();
            }
        }
    });
}
jQuery(".clear-inv").click(function (evt) {
    evt.preventDefault();
    location.reload();
});

function clickedFunction(code, type) {

    jQuery.ajax({
        type: "GET",
        async:false,
        url: "/CustomerAnalysis/addMapData?type=" + type + "&value=" + code,
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        //data: { pgeNum: pgeNum, pgeSize: pgeSize },
        success: function (result) {
            if (result.login == true) {
                if (result.success == true) {
                    displayPopData();
                } else {
                    if (result.type == "Info") {
                        setInfoMsg(result.msg);
                    }
                    if (result.type == "Error") {
                        showError(result.msg);
                    }
                }
            } else {
                Logout();
            }
        }
    });
}

function displayPopData() {
    
    jQuery('#pagingAnalysis').empty();
    jQuery('table.result-display-tbl .new-row').remove();
    var checkcom = "";
    var i = 0;
    jQuery('input[name="selectedcompany"]:checked').each(function () {
        if (this.value != "") {
            checkcom += this.value + ',';
        }
    });

    itemIds1 = [];
    modelIds1 = [];
    brandIds1 = [];
    cat1Ids1 = [];
    cat2Ids1 = [];
    cat3Ids1 = [];

    cusIds1 = [];
    invTpIds1 = [];
    schmTpIds1 = [];
    schmCdIds1 = [];
    ptypeCdIds1 = [];

    ctownIds1 = [];
    ptownIds1 = [];
    pdistIds1 = [];
    pprovIds1 = [];
    bankIds1 = [];
    var checkcom = "";
    var i = 0;
    jQuery('input[name="selectedcompany"]:checked').each(function () {
        i = i + 1;
        if (this.value != "") {
            checkcom += (i == jQuery('input[name="selectedcompany"]:checked').length) ? this.value : this.value + ',';
        }
    });
    //var i = document.getElementById("checkTable").checked;
    if (!i) {
       
        if (jQuery("#CheckAmount").val() != "") {
            jQuery(this).prop('disabled', true);
            jQuery('table.result-display-tbl .new-row').remove();
            jQuery(".export-btn").hide();
            //evt.preventDefault();
            var formdata = jQuery("#filterdata-frm").serialize();
            //alert(formdata);
            jQuery.ajax({
                type: "GET",

                data: formdata, //+ "&selectedcompany=" + checkcom,// +
                //+ "&Item=" + itemIds1 + "&Model=" + modelIds1 + "&Brand=" + brandIds1 + "&Cat1=" + cat1Ids1 + "&Cat2=" + cat2Ids1 + "&Cat3=" + cat3Ids1
                //+ "&Customer=" + cusIds1 + "&SalesType=" + invTpIds1 + "&SchemeType=" + schmTpIds1 + "&SchemeCode=" + schmCdIds1 + "&PtypeCode=" + ptypeCdIds1 + "&CTown=" + ctownIds1 + "&PTown=" + ptownIds1
                //+ "&BankCode=" + bankIds1 + "&PDist=" + pdistIds1 + "&PProv=" + pprovIds1,

                
                url: "/CustomerAnalysis/getCustomerDetails",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                async:false,
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.type == "Success") {
                                setSuccesssMsg(result.msg);

                            }
                            $('#salesReportModel').modal('show');
                            jQuery(".btn-disply-export").show();
                            pagingCon(1, pgeSize);
                        } else {
                            jQuery('table.result-display-tbl .new-row').remove();
                            if (result.type == "Info") {
                                setInfoMsg(result.msg);
                            }
                            if (result.type == "Error") {
                                showError(result.msg);
                            }
                        }
                        jQuery(".grph-disply-btn").prop('disabled', false);
                    } else {
                        Logout();
                    }
                }
            });
        } else {
            setInfoMsg("Please enter check invoice amount.");
            //evt.preventDefault();
        }
    }
    else {
        if (jQuery("#CheckAmount").val() != "") {
            jQuery(this).prop('disabled', false);
            jQuery('table.result-display-tbl .new-row').remove();
            //  jQuery(".export-btn").hide();
            // evt.preventDefault();
            var formdata = jQuery("#filterdata-frm").serialize();
            jQuery.ajax({
                type: "GET",

                data: formdata + "&selectedcompany=" + checkcom +
                + "&Item=" + itemIds1 + "&Model=" + modelIds1 + "&Brand=" + brandIds1 + "&Cat1=" + cat1Ids1 + "&Cat2=" + cat2Ids1 + "&Cat3=" + cat3Ids1
                + "&Customer=" + cusIds1 + "&SalesType=" + invTpIds1 + "&SchemeType=" + schmTpIds1 + "&SchemeCode=" + schmCdIds1 + "&PtypeCode=" + ptypeCdIds1 + "&CTown=" + ctownIds1 + "&PTown=" + ptownIds1
                + "&BankCode=" + bankIds1 + "&PDist=" + pdistIds1 + "&PProv=" + pprovIds1,

                url: "/CustomerAnalysis/getCustomerInvDetails",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.type == "Success") {
                                setSuccesssMsg(result.msg);

                            }
                            $('#salesReportModelinv').modal('show');
                            jQuery(".btn-disply-export").show();
                            pagingConInv(1, pgeSize);

                        } else {
                            jQuery('table.result-display-tbl .new-row').remove();
                            if (result.type == "Info") {
                                setInfoMsg(result.msg);
                            }
                            if (result.type == "Error") {
                                showError(result.msg);
                            }
                        }
                        jQuery(".grph-disply-btn").prop('disabled', false);
                    } else {
                        Logout();
                    }
                }
            });
        } else {
            setInfoMsg("Please enter check invoice amount.");
        }
    }
}


function displayData() {
    jQuery(this).prop('disabled', false);
    jQuery('#pagingAnalysisinv').empty();

    jQuery('table.result-display-tbl .new-row').remove();
    var checkcom = "";
    var i = 0;
    jQuery('input[name="selectedcompany"]:checked').each(function () {
        if (this.value != "") {
            checkcom += this.value + ',';
        }
    });

    itemIds1 = [];
    modelIds1 = [];
    brandIds1 = [];
    cat1Ids1 = [];
    cat2Ids1 = [];
    cat3Ids1 = [];

    cusIds1 = [];
    invTpIds1 = [];
    schmTpIds1 = [];
    schmCdIds1 = [];
    ptypeCdIds1 = [];

    ctownIds1 = [];
    ptownIds1 = [];
    pdistIds1 = [];
    pprovIds1 = [];
    bankIds1 = [];
    var checkcom = "";
    var i = 0;
    jQuery('input[name="selectedcompany"]:checked').each(function () {
        i = i + 1;
        if (this.value != "") {
            checkcom += (i == jQuery('input[name="selectedcompany"]:checked').length) ? this.value : this.value + ',';
        }
    });

    if (jQuery("#CheckAmount").val() != "") {
        jQuery(this).prop('disabled', false);
        jQuery('table.result-display-tbl .new-row').remove();
        jQuery(".export-btn").hide();
        var formdata = jQuery("#filterdata-frm").serialize();
        jQuery.ajax({
            type: "GET",

            data: formdata + "&selectedcompany=" + checkcom +
            + "&Item=" + itemIds1 + "&Model=" + modelIds1 + "&Brand=" + brandIds1 + "&Cat1=" + cat1Ids1 + "&Cat2=" + cat2Ids1 + "&Cat3=" + cat3Ids1
            + "&Customer=" + cusIds1 + "&SalesType=" + invTpIds1 + "&SchemeType=" + schmTpIds1 + "&SchemeCode=" + schmCdIds1 + "&PtypeCode=" + ptypeCdIds1 + "&CTown=" + ctownIds1 + "&PTown=" + ptownIds1
            + "&BankCode=" + bankIds1 + "&PDist=" + pdistIds1 + "&PProv=" + pprovIds1,

            url: "/CustomerAnalysis/getCustomerInvDetails",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.type == "Success") {
                            setSuccesssMsg(result.msg);

                        }
                        $('#salesReportModelinv').modal('show');
                        jQuery(".btn-disply-export").show();
                        pagingConInv(1, pgeSize);

                    } else {
                        jQuery('table.result-display-tbl .new-row').remove();
                        if (result.type == "Info") {
                            setInfoMsg(result.msg);
                        }
                        if (result.type == "Error") {
                            showError(result.msg);
                        }
                    }
                    jQuery(".grph-disply-btn").prop('disabled', false);
                } else {
                    Logout();
                }
            }
        });
    } else {
        setInfoMsg("Please enter check invoice amount.");
    }
}