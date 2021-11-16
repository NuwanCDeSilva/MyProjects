var taxsts = 0;
var _IS_TAX;
var _ACT;
var _IS_SVAT;
var _TAX_EX;
var _IS_SUSPEND;
var _AGRE_SEND_SMS;
var _AGRE_SEND_EMAIL;


jQuery(document).ready(function () {

    clearForm();

    function loadcustomercountry() {
        jQuery.ajax({
            type: "GET",
            url: "/CustomerDetails/loadcustomercoun",
            contentType: "application/json:charset=utf-8",
            dataType: "Json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        var select = document.getElementById("Mcu_cd");
                        jQuery("#Mcu_cd").empty();
                        var option = [];
                        var option = document.createElement('option')
                        if (result.data != null && result.data.length != 0) {
                            for (var i = 0; i < reslt.data.length; i++) {
                                option.text = result.data[i].text;
                                option.value = result.data[i].value;
                                option.push(option.outerHTML);
                            }

                        }
                        else {
                            option.text = "Select Country";
                            option.type = "";
                            option.push(option.outerHTML);
                        }
                        select.insertAdjacentHTML('beforeEnd', option.join('\n'));

                    }
                    else {
                        setError(result.msg);
                    }
                }
                else {
                    Logout();
                }
            }
        });
    }

    function loadcustomerentitymodes() {
        jQuery.ajax({
            type: "GET",
            url: "/CustomerDetails/loadentitymodes",
            contentType: "application/json:charset=utf-8",
            dataType: "Json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        var select = document.getElementById("Mcu_entity");
                        jQuery("#Mcu_entity").empty();
                        var option = [];
                        var option = document.createElement('option')
                        if (result.data != null && result.data.length != 0) {
                            for (var i = 0; i < reslt.data.length; i++) {
                                option.text = result.data[i].text;
                                option.value = result.data[i].value;
                                option.push(option.outerHTML);
                            }

                        }
                        else {
                            option.text = "Select Entity Mode";
                            option.type = "";
                            option.push(option.outerHTML);
                        }
                        select.insertAdjacentHTML('beforeEnd', option.join('\n'));

                    }
                    else {
                        setError(result.msg);
                    }
                }
                else {
                    Logout();
                }
            }
        });
    }

    jQuery(".cus-cd-search").click(function () {
        //alert("d");
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Name","NIC","Mobile","BR No"];
        field = "cusCodecus"
        var x = new CommonSearch(headerKeys, field);
    });
    //DILSHAN ON 22/08/2017

    jQuery("#MBE_CD").on("keydown", function (evt) {
        //alert("d");
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Name", "NIC", "Mobile", "BR No"];
            field = "cusCodecus"
            var x = new CommonSearch(headerKeys, field);
        }
    });

    jQuery(".cus-typ-search").click(function () {
        //alert("d");
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description"];
        field = "custyp"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".cus-br-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "BR No"];
        field = "cusbr"
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery(".cus-passp-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Passport No"];
        field = "cuspassp"
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery(".cus-dl-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Driving licence No"];
        field = "cusdl"
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery(".cus-tel-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Mobile"];
        field = "custel"
        var x = new CommonSearch(headerKeys, field);
    });

    //jQuery(".cus-execu-search").click(function () {
    //    var headerKeys = Array()
    //    headerKeys = ["Row", "Code", "Name", "NIC", "Mobile", "BR No"];
    //    field = "cusexecu"
    //    //field = "cusCode"
    //    var x = new CommonSearch(headerKeys, field);
    //});

    jQuery(".cus-exec-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Epf No", "Category", "First Name", "Last Name", "NIC"];
        field = "cusintroexe";
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery(".cus-nic-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "NIC"];
        field = "cusnic"
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery(".cntry-cd-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description", "Region Code", "Capital"];
        field = "presentCountry1";
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery("#country").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description", "Region Code", "Capital"];
            field = "presentCountry1";
            var x = new CommonSearch(headerKeys, field);
        }
    });

    jQuery(".per-town-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Town", "District", "Province", "Code"];
        field = "perTown1"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery("#MBE_TOWN_CD").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Town", "District", "Province", "Code"];
            field = "perTown1"
            var x = new CommonSearch(headerKeys, field);
        }
    });

    jQuery(".curency-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description"];
        field = "curcd1";
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery("#MBE_CUR_CD").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description"];
            field = "curcd"
            var x = new CommonSearch(headerKeys, field);
        }
    });

    $('#MBE_CD').focusout(function () {
        var pc = $(this).val();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/CustomerDetails/LoadCustormerDetails",
            data: { CustormerCode: jQuery("#MBE_CD").val() },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.data.length > 0) {
                            // set custormer field
                            SetCustormerData(result.data);
                        } else {
                            setInfoMsg('Please Enter Correct Custormer');
                            ClearCustormerData();
                        }
                    } else {
                        setInfoMsg('No Data Found! Please Check Code!!');
                        return;
                    }
                } else {
                    Logout();
                }
            }
        });
    });

    $('#MBE_COUNTRY_CD').focusout(function () {
        var pc = $(this).val();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/CustomerDetails/LoadCountryTown",
            data: { CountryCode: jQuery("#MBE_COUNTRY_CD").val() },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.data.length > 0) {
                            // set custormer field
                            //SetCustormerData(result.data);
                        } else {
                            //setInfoMsg('Please Enter Correct Custormer');
                            //ClearCustormerData();
                            jQuery("#MBE_PROVINCE_CD").val("OTHER");
                            jQuery("#MBE_DISTRIC_CD").val("OTHER");
                            jQuery("#MBE_TOWN_CD").val("OTHER");
                        }
                    } else {
                        setInfoMsg('No Data Found! Please Check Code!!');
                        return;
                    }
                } else {
                    Logout();
                }
            }
        });
    });

    jQuery("#MBE_CUR_CD").focusout(function () {
        if (jQuery(this).val() != "") {
            CurrencyfocusOut(jQuery(this).val());
        }
    });

    function CurrencyfocusOut(code) {
        if (code != "") {
            jQuery.ajax({
                type: "GET",
                url: "/Validation/validateCurrencyCode?curcd=" + code,
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.success == false) {
                        setInfoMsg(result.msg);
                        jQuery("#MBE_CUR_CD").val("");
                        jQuery("#MBE_CUR_CD").focus();
                    }
                    if (result.success == true) {
                        if (result.data.MCR_CD == null) {
                            setInfoMsg("Please enter valid Currency code.");
                            jQuery("#MBE_CUR_CD").val("");
                            jQuery("#MBE_CUR_CD").focus();
                        } else {

                            loadExchageRate2(code);
                        }
                    }
                }
            });

        }
    }

    function loadExchageRate2(code) {
        if (code != "") {
            jQuery.ajax({
                type: "GET",
                url: "/Validation/getExcahaneRate",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                data: { currency: code },
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            // jQuery("#ExchgRate").val(result.data);
                            // updateAmount();
                        } else {
                            if (result.type == "Error") {
                                setError(result.msg);
                            } else if (result.type == "Info") {
                                setInfoMsg(result.msg);
                                jQuery("#MBE_CUR_CD").val("");
                            }


                        }
                    } else {
                        Logout();
                    }
                }
            });
        }
    }

    //Dilshan on 23/08/2017
    jQuery("#MBE_COUNTRY_CD").focusout(function () {
        if (jQuery(this).val() != "") {
            validatePreCountry(jQuery(this).val());
        }
    });

    function validatePreCountry(code) {
        if (code != "") {
            jQuery.ajax({
                type: "GET",
                url: "/Validation/validateCountry",
                contentType: "application/json;charset=utf-8",
                data: { country: code },
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == false) {
                            if (result.type == "Error") {
                                setError(result.msg);
                            }
                            if (result.type == "Info") {
                                setInfoMsg(result.msg);
                            }
                            jQuery("#MBE_COUNTRY_CD").val("");
                        }
                    } else {
                        Logout();
                    }
                }
            });
        }
    }
    jQuery("#MBE_TOWN_CD").focusout(function () {
        if (jQuery(this).val() == "OTHER") {
            jQuery("#MBE_TOWN_CD").val("");
            var headerKeys = Array()
            headerKeys = ["Row", "Town", "District", "Province", "Code"];
            field = "perTown1"
            var x = new CommonSearch(headerKeys, field);
        }
        if (jQuery(this).val() != "") {
            loadTownData(jQuery(this).val());
        }
    });
    function loadTownData(towncd) {
        jQuery.ajax({
            type: "GET",
            url: "/CustomerDetails/preTownTextChanged",
            data: { val: towncd },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (typeof (result.data) != "undefined") {
                            jQuery("#MBE_DISTRIC_CD").val(result.data.district);
                            jQuery("#MBE_PROVINCE_CD").val(result.data.province);
                            // jQuery("#Mbe_postal_cd").val(result.data.postalCD);
                            jQuery("#MBE_COUNTRY_CD").val(result.data.countryCD);
                        }
                    } else {
                        jQuery("#MBE_TOWN_CD").val("");
                        jQuery("#MBE_DISTRIC_CD").val("");
                        jQuery("#MBE_PROVINCE_CD").val("");
                        //jQuery("#Mbe_postal_cd").val("");
                        jQuery("#MBE_COUNTRY_CD").val("");
                        if (result.type == "Error") {
                            setError(result.msg);
                        } else if (result.type == "Info") {
                            setInfoMsg(result.msg);
                        }
                    }
                } else {
                    Logout();
                }
            }
        });
    }

    //var MBE_TP = $('#MBE_TP').val();
    //var MBE_CD = $('#MBE_CD').val();
    //var MBE_NAME = $('#MBE_NAME').val();
    //var MBE_BR_NO = $('#MBE_BR_NO').val();
    //var MBE_OTH_ID_NO = $('#MBE_OTH_ID_NO').val();
    //var MBE_DL_NO = $('#MBE_DL_NO').val();
    //var MBE_NIC = $('#MBE_NIC').val();
    //var MBE_PP_NO = $('#MBE_PP_NO').val();
    //var MBE_IS_TAX = $('#MBE_IS_TAX').val();
    //var MBE_TAX_NO = $('#MBE_TAX_NO').val();
    //var MBE_IS_SVAT = $('#MBE_IS_SVAT').val();
    //var MBE_SVAT_NO = $('#MBE_SVAT_NO').val();
    //var MBE_TAX_EX = $('#MBE_TAX_EX').val();
    //var MBE_CUR_CD = $('#MBE_CUR_CD').val();
    //var cusseg_id = $('#cusseg_id').val();
    ////var cuscoun_id = $('#cuscoun_id').val();
    //var MBE_COUNTRY_CD = $('#MBE_COUNTRY_CD').val();
    //var MBE_EMAIL = $('#MBE_EMAIL').val();
    //var MBE_MOB = $('#MBE_MOB').val();
    //var MBE_ADD1 = $('#MBE_ADD1').val();
    ////var custwn_id = $('#custwn_id').val();
    //var MBE_TOWN_CD = $('#MBE_TOWN_CD').val();
    ////var curdist_id = $('#curdist_id').val();
    //var MBE_DISTRIC_CD = $('#MBE_DISTRIC_CD').val();
    ////var cuspro_id = $('#cuspro_id').val();
    //var MBE_PROVINCE_CD = $('#MBE_PROVINCE_CD').val();
    //var MBE_WEB = $('#MBE_WEB').val();
    //var MBE_TEL = $('#MBE_TEL').val();
    //var cuscntctper_id = $('#cuscntctper_id').val();
    //var MBE_FAX = $('#MBE_FAX').val();
    //var MBE_ACT = $('#MBE_ACT').val();
    //var MBE_IS_SUSPEND = $('#MBE_IS_SUSPEND').val();
    //var MBE_AGRE_SEND_SMS = $('#MBE_AGRE_SEND_SMS').val();
    //var MBE_AGRE_SEND_EMAIL = $('#MBE_AGRE_SEND_EMAIL').val();
    //var MBE_INTRO_EX = $('#MBE_INTRO_EX').val();
    //var cusentity_id = $('#cusentity_id').val();


    //var alldata = JSON.stringify({
    //    MBE_TP: MBE_TP,
    //    MBE_CD: MBE_CD,
    //    MBE_NAME: MBE_NAME,
    //    MBE_BR_NO: MBE_BR_NO,
    //    MBE_OTH_ID_NO: MBE_OTH_ID_NO,
    //    MBE_DL_NO: MBE_DL_NO,
    //    MBE_NIC: MBE_NIC,
    //    MBE_PP_NO: MBE_PP_NO,
    //    MBE_IS_TAX: MBE_IS_TAX,
    //    MBE_TAX_NO: MBE_TAX_NO,
    //    MBE_IS_SVAT: MBE_IS_SVAT,
    //    MBE_SVAT_NO: MBE_SVAT_NO,
    //    MBE_TAX_EX: MBE_TAX_EX,
    //    MBE_CUR_CD: MBE_CUR_CD,
    //    //cuscoun_id: cuscoun_id,
    //    MBE_COUNTRY_CD: MBE_COUNTRY_CD,
    //    MBE_EMAIL: MBE_EMAIL,
    //    MBE_MOB: MBE_MOB,
    //    MBE_ADD1: MBE_ADD1,
    //    //custwn_id: custwn_id,
    //    MBE_TOWN_CD: MBE_TOWN_CD,
    //    //curdist_id: curdist_id,
    //    MBE_DISTRIC_CD: MBE_DISTRIC_CD,
    //    //cuspro_id: cuspro_id,
    //    MBE_PROVINCE_CD: MBE_PROVINCE_CD,
    //    MBE_WEB: MBE_WEB,
    //    MBE_TEL: MBE_TEL,
    //    cuscntctper_id: cuscntctper_id,
    //    MBE_FAX: MBE_FAX,
    //    MBE_ACT: MBE_ACT,
    //    MBE_IS_SUSPEND: MBE_IS_SUSPEND,
    //    MBE_AGRE_SEND_SMS: MBE_AGRE_SEND_SMS,
    //    MBE_AGRE_SEND_EMAIL: MBE_AGRE_SEND_EMAIL,
    //    MBE_INTRO_EX: MBE_INTRO_EX,
    //    cusentity_id: cusentity_id,
    //    MBE_IS_SVAT: MBE_IS_SVAT,
    //    MBE_SVAT_NO: MBE_SVAT_NO

    //});


    //var otherdata = JSON.stringify({
    //    cusseg_id: cusseg_id
    //});

    jQuery(".btn-emp-save-data").click(function (event) {
       
        jQuery(this).attr("disabled", true);
        event.preventDefault();
         Lobibox.confirm({
             msg: "Do you want to continue ?",
              callback: function ($this, type, ev) {
                  if (type == "yes") {
                      var formdata = jQuery("#customer-data").serialize();
                      //alert(formdata);
                    if (jQuery(".btn-emp-save-data").val() == "Create") {
                        jQuery.ajax({
                            type: 'POST',
                            url: '/CustomerDetails/CreateCustomer',
                            //data: formdata,
                            data: formdata + "&_IS_TAX=" + _IS_TAX + "&_ACT=" + _ACT + "&_IS_SVAT=" + _IS_SVAT + "&_TAX_EX=" + _TAX_EX + "&_IS_SUSPEND=" + _IS_SUSPEND + "&_AGRE_SEND_SMS=" + _AGRE_SEND_SMS + "&_AGRE_SEND_EMAIL=" + _AGRE_SEND_EMAIL,
                            //data: { formdata: formdata, _IS_TAX: _IS_TAX, _ACT: _ACT, _IS_SVAT: _IS_SVAT, _TAX_EX: _TAX_EX, _IS_SUSPEND: _IS_SUSPEND, _AGRE_SEND_SMS: _AGRE_SEND_SMS, _AGRE_SEND_EMAIL: _AGRE_SEND_EMAIL },

                            success: function (response) {
                                if (response.login == true) {
                                    if (response.success == true) {
                                        clearForm();
                                        setSuccesssMsg(response.msg);

                                    } else {
                                        if (response.type == "Error") {
                                            setError(response.msg);
                                        } else if (response.type == "Info") {
                                            setInfoMsg(response.msg);
                                        }
                                    }
                                    jQuery(".btn-emp-save-data").removeAttr("disabled");
                                } else {
                                    Logout();
                                }
                            }
                        });
                        return false;
                    //} else {
                    //    var formdata = jQuery("#emply-crte-frm").serialize();
                    //    jQuery.ajax({
                    //        type: 'POST',
                    //        url: '/Employee/employeeUpdate',
                    //        data: formdata,
                    //        success: function (response) {
                    //            if (response.success == true) {
                    //                clearForm();
                    //                setSuccesssMsg(response.msg);

                    //            } else {
                    //                if (response.type == "Error") {
                    //                    setError(response.msg);
                    //                } else if (response.type == "Info") {
                    //                    setInfoMsg(response.msg);
                    //                }
                    //            }
                    //            jQuery(".btn-emp-save-data").removeAttr("disabled");
                    //        }
                    //    });
                    //    return false;
                    }
                }
            }
        });

    });

    //jQuery(".btn-emp-update-data").click(function (evt) {
    //    evt.preventDefault();
    //    jQuery(this).attr('disabled', 'disabled');
    //    seq = jQuery("#RequestSeq").val();
    //    ManualRef = jQuery("#ManualRef").val();
    //    Remarks = jQuery("#Remarks").val();
    //    ProfitCenter = jQuery("#ProfitCenter").val();
    //    Lobibox.confirm({
    //        msg: "Do you want update?",
    //        callback: function ($this, type, ev) {
    //            if (type == "yes") {
    //                jQuery.ajax({
    //                    type: "POST",
    //                    url: "/CustomerDetails/UpdateCustomer",
    //                    dataType: "json",
    //                    data: { seq: seq, ManualRef: ManualRef, Remarks: Remarks, ProfitCenter: ProfitCenter },

    //                    success: function (result) {
    //                        if (result.login == false) {
    //                            Logout();
    //                        } else {
    //                            if (result.success == true) {
    //                                if (result.type == "Success") {
    //                                    setSuccesssMsg(result.msg);
    //                                    clearForm();
    //                                }
    //                            } else {
    //                                if (result.type == "Error") {
    //                                    setError(result.msg);
    //                                }
    //                                if (result.type == "Info") {
    //                                    setInfoMsg(result.msg);
    //                                }
    //                            }

    //                        }
    //                    }
    //                });
    //            }
    //        }
    //    });
    //    jQuery(this).removeAttr('disabled');
    //});

    jQuery(".btn-emp-update-data").click(function (event) {

        jQuery(this).attr("disabled", true);
        event.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to Update ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    var formdata = jQuery("#customer-data").serialize();
                    //alert(formdata);
                    if (jQuery(".btn-emp-update-data").val() == "Update") {
                        jQuery.ajax({
                            type: 'GET',
                            url: '/CustomerDetails/UpdateCustomer',
                            //data: formdata,
                            //data: formdata + _IS_TAX + _ACT,
                            data: formdata + "&_IS_TAX=" + _IS_TAX + "&_ACT=" + _ACT + "&_IS_SVAT=" + _IS_SVAT + "&_TAX_EX=" + _TAX_EX + "&_IS_SUSPEND=" + _IS_SUSPEND + "&_AGRE_SEND_SMS=" + _AGRE_SEND_SMS + "&_AGRE_SEND_EMAIL=" + _AGRE_SEND_EMAIL,
                            //data: { formdata: formdata, active: "true" },
                            //data: { formdata: formdata, _IS_TAX: _IS_TAX, _ACT: _ACT, _IS_SVAT: _IS_SVAT, _TAX_EX: _TAX_EX, _IS_SUSPEND: _IS_SUSPEND, _AGRE_SEND_SMS: _AGRE_SEND_SMS, _AGRE_SEND_EMAIL: _AGRE_SEND_EMAIL },

                            success: function (response) {
                                if (response.login == true) {
                                    if (response.success == true) {
                                        clearForm();
                                        setSuccesssMsg(response.msg);

                                    } else {
                                        if (response.type == "Error") {
                                            setError(response.msg);
                                        } else if (response.type == "Info") {
                                            setInfoMsg(response.msg);
                                        }
                                    }
                                    jQuery(".btn-emp-update-data").removeAttr("disabled");
                                } else {
                                    Logout();
                                }
                            }
                        });
                        return false;
                        //} else {
                        //    var formdata = jQuery("#emply-crte-frm").serialize();
                        //    jQuery.ajax({
                        //        type: 'POST',
                        //        url: '/Employee/employeeUpdate',
                        //        data: formdata,
                        //        success: function (response) {
                        //            if (response.success == true) {
                        //                clearForm();
                        //                setSuccesssMsg(response.msg);

                        //            } else {
                        //                if (response.type == "Error") {
                        //                    setError(response.msg);
                        //                } else if (response.type == "Info") {
                        //                    setInfoMsg(response.msg);
                        //                }
                        //            }
                        //            jQuery(".btn-emp-save-data").removeAttr("disabled");
                        //        }
                        //    });
                        //    return false;
                    }
                }
            }
        });

    });

    jQuery(".btn-emp-clear-data").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to clear data ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    clearForm();
                }
            }
        });

    });


    function clearForm() {
        // clearSessionData();
        ClearCustormerData();
        //enabledField();
        //document.getElementById("emply-crte-frm").reset();
        jQuery(".btn-emp-save-data").val("Create");
        jQuery(".btn-emp-update-data").val("Update");
        //jQuery("#MBE_ACT").attr('checked', 'checked');

    }

    //Dilshan on 22/08/2017
    function SetCustormerData(data) {

        jQuery("#MBE_OTH_ID_NO").val(data[0].MBE_OTH_ID_NO);
        jQuery("#MBE_NAME").val(data[0].MBE_NAME);
        jQuery("#MBE_ADD1").val(data[0].MBE_ADD1);
        //jQuery("#cus_addr2").val(data[0].MBE_ADD2);
        jQuery("#MBE_ADD2").val(data[0].MBE_ADD2);
        jQuery("#MBE_EMAIL").val(data[0].MBE_EMAIL);
        jQuery("#MBE_COUNTRY_CD").val(data[0].MBE_COUNTRY_CD);
        jQuery("#MBE_PROVINCE_CD").val(data[0].MBE_PROVINCE_CD);
        jQuery("#MBE_DISTRIC_CD").val(data[0].MBE_DISTRIC_CD);
        jQuery("#MBE_TOWN_CD").val(data[0].MBE_TOWN_CD);
        jQuery("#MBE_TEL").val(data[0].MBE_TEL);
        jQuery("#MBE_FAX").val(data[0].MBE_FAX);
        //jQuery('#MBE_IS_SVAT').prop('checked', data[0].MBE_IS_SVAT);
        if (data[0].MBE_IS_SVAT == true)
        {
            _IS_SVAT = true;
        }
        else
        {
            _IS_SVAT = false;
        }
        jQuery('#MBE_IS_SVAT').prop('checked', _IS_SVAT);
        jQuery("#MBE_SVAT_NO").val(data[0].MBE_SVAT_NO);
        if (data[0].MBE_IS_TAX == true)
        {
            _IS_TAX = true;
        }
        else
        {
            _IS_TAX = false;
        }
        jQuery('#MBE_IS_TAX').prop('checked', _IS_TAX);
        jQuery("#MBE_TAX_NO").val(data[0].MBE_TAX_NO);
        if (data[0].MBE_TAX_EX == true)
        {
            _TAX_EX = true;
        }
        else
        {
            _TAX_EX = false;
        }
        jQuery('#MBE_TAX_EX').prop('checked', _TAX_EX);
        jQuery("#MBE_CUR_CD").val(data[0].MBE_CUR_CD);
        //jQuery('#MBE_ACT').prop('checked', data[0].MBE_ACT);
        if (data[0].MBE_ACT == true)
        {
            _ACT = true;
        }
        else
        {
            _ACT = false;
        }
        jQuery('#MBE_ACT').prop('checked', _ACT);
        if (data[0].MBE_AGRE_SEND_EMAIL == true)
        {
            _AGRE_SEND_EMAIL = true;
        }
        else
        {
            _AGRE_SEND_EMAIL = false;
        }
        jQuery('#MBE_AGRE_SEND_EMAIL').prop('checked', _AGRE_SEND_EMAIL);
        if (data[0].MBE_IS_SUSPEND == true)
        {
            _IS_SUSPEND = true;
        }
        else
        {
            _IS_SUSPEND = false;
        }
        jQuery('#MBE_IS_SUSPEND').prop('checked', _IS_SUSPEND);
        if (data[0].MBE_AGRE_SEND_SMS == true)
        {
            _AGRE_SEND_SMS = true;
        }
        else
        {
            _AGRE_SEND_SMS = false;
        }
        jQuery('#MBE_AGRE_SEND_SMS').prop('checked', _AGRE_SEND_SMS);
        jQuery("#MBE_MOB").val(data[0].MBE_MOB);
        jQuery("#MBE_TP").val(data[0].MBE_TP);
        jQuery("#MBE_BR_NO").val(data[0].MBE_BR_NO);
        jQuery("#MBE_DL_NO").val(data[0].MBE_DL_NO);
        jQuery("#MBE_NIC").val(data[0].MBE_NIC);
        jQuery("#MBE_PP_NO").val(data[0].MBE_PP_NO);
        jQuery("#MBE_WEB").val(data[0].MBE_WEB);
        jQuery("#MBE_INTRO_EX").val(data[0].MBE_INTRO_EX);
        jQuery("#MBE_CONTACT_PERSON").val(data[0].MBE_CONTACT_PERSON);
        jQuery("#MBE_REMARK").val(data[0].MBE_REMARK);
        jQuery("#MBE_ENT_MODULE").val(data[0].MBE_ENT_MODULE);
        jQuery("#MBE_CREDIT_DAYS").val(data[0].MBE_CREDIT_DAYS);

    }
    function ClearCustormerData() {
        jQuery("#MBE_CD").val("");
        jQuery("#MBE_OTH_ID_NO").val("");
        jQuery("#MBE_NAME").val("");
        jQuery("#MBE_ADD1").val("");
        //jQuery("#cus_addr2").val("");
        jQuery("#MBE_ADD2").val("");
        jQuery("#MBE_EMAIL").val("");
        //jQuery("#cuscoun_id").val("");
        jQuery("#MBE_COUNTRY_CD").val("");
        //jQuery("#cuspro_id").val("");
        jQuery("#MBE_PROVINCE_CD").val("");
        //jQuery("#curdist_id").val("");
        jQuery("#MBE_DISTRIC_CD").val("");
        jQuery("#MBE_TOWN_CD").val("");
        jQuery("#MBE_TEL").val("");
        jQuery("#MBE_FAX").val("");
        jQuery("#MBE_IS_SVAT").val("");
        jQuery("#MBE_SVAT_NO").val("");
        jQuery("#MBE_IS_TAX").val("");
        jQuery("#MBE_TAX_NO").val("");
        jQuery("#MBE_TAX_EX").val("");
        jQuery("#MBE_CUR_CD").val("");
        jQuery("#MBE_ACT").val("");
        jQuery("#MBE_AGRE_SEND_EMAIL").val("");
        jQuery("#MBE_IS_SUSPEND").val("");
        jQuery("#MBE_AGRE_SEND_SMS").val("");
        jQuery("#MBE_MOB").val("");
        jQuery("#MBE_TP").val("");
        jQuery("#MBE_BR_NO").val("");
        jQuery("#MBE_DL_NO").val("");
        jQuery("#MBE_NIC").val("");
        jQuery("#MBE_PP_NO").val("");
        jQuery("#MBE_WEB").val("");
        jQuery("#cuscntctper_id").val("");
        jQuery('#MBE_IS_SVAT').prop('checked', false);
        jQuery('#MBE_IS_TAX').prop('checked', false);
        jQuery('#MBE_TAX_EX').prop('checked', false);
        jQuery('#MBE_ACT').prop('checked', true);
        jQuery('#MBE_AGRE_SEND_EMAIL').prop('checked', false);
        jQuery('#MBE_IS_SUSPEND').prop('checked', false);
        jQuery('#MBE_AGRE_SEND_SMS').prop('checked', false);
        jQuery("#MBE_INTRO_EX").val("");
        jQuery("#MBE_CONTACT_PERSON").val("");
        jQuery("#MBE_REMARK").val("");
        jQuery("#MBE_CREDIT_DAYS").val("");

    }

    //jQuery("#tax").on("change", function () {
        
    //    if (jQuery('#tax').is(":checked")) {
    //        //var promotion = "false";
    //        jQuery("#tax").prop('checked', true);
    //    } else {
    //        jQuery("#tax").prop('checked', false);
    //    }
    //});

    $("#MBE_IS_TAX").on("click", function () {
        var state = $('#MBE_IS_TAX').prop("checked");
        if (!state) {
            $('#MBE_IS_TAX').prop("checked", false);
            _IS_TAX = false;
        }
        else {
            $('#MBE_IS_TAX').prop("checked", true);
            _IS_TAX = true;
        }

    });

    $("#MBE_ACT").on("click", function () {
        var state = $('#MBE_ACT').prop("checked");
        if (!state) {
            $('#MBE_ACT').prop('checked', false);
            _ACT = false;
        }
        else {
            $('#MBE_ACT').prop('checked', true);
            _ACT = true;
        }
    });

    $("#MBE_IS_SVAT").on("click", function () {
        var state = $('#MBE_IS_SVAT').prop("checked");
        if (!state) {
            $('#MBE_IS_SVAT').prop("checked", false);
            _IS_SVAT = false;
        }
        else {
            $('#MBE_IS_SVAT').prop("checked", true);
            _IS_SVAT = true;
        }
    });

    $("#MBE_TAX_EX").on("click", function () {
        var state = $('#MBE_TAX_EX').prop("checked");
        if (!state) {
            $('#MBE_TAX_EX').prop("checked", false);
            _TAX_EX = false;
        }
        else {
            $('#MBE_TAX_EX').prop("checked", true);
            _TAX_EX = true;
        }
    });

    $("#MBE_IS_SUSPEND").on("click", function () {
        var state = $('#MBE_IS_SUSPEND').prop("checked");
        if (!state) {
            $('#MBE_IS_SUSPEND').prop("checked", false);
            _IS_SUSPEND = false;
        }
        else {
            $('#MBE_IS_SUSPEND').prop("checked", true);
            _IS_SUSPEND = true;
        }
    });

    $("#MBE_AGRE_SEND_SMS").on("click", function () {
        var state = $('#MBE_AGRE_SEND_SMS').prop("checked");
        if (!state) {
            $('#MBE_AGRE_SEND_SMS').prop("checked", false);
            _AGRE_SEND_SMS = false;
        }
        else {
            $('#MBE_AGRE_SEND_SMS').prop("checked", true);
            _AGRE_SEND_SMS = true;
        }
    });

    $("#MBE_AGRE_SEND_EMAIL").on("click", function () {
        var state = $('#MBE_AGRE_SEND_EMAIL').prop("checked");
        if (!state) {
            $('#MBE_AGRE_SEND_EMAIL').prop("checked", false);
            _AGRE_SEND_EMAIL = false;
        }
        else {
            $('#MBE_AGRE_SEND_EMAIL').prop("checked", true);
            _AGRE_SEND_EMAIL = true;
        }
    });

    //jQuery(function ($) {
    //    $('#MBE_IS_TAX').prop('checked', false);
    //})

    //jQuery(function ($) {
    //    $('#MBE_ACT').prop('checked', false);
    //})

    //jQuery(function ($) {
    //    $('#MBE_IS_SVAT').prop('checked', false);
    //})

    //jQuery(function ($) {
    //    $('#MBE_TAX_EX').prop('checked', false);
    //})

    //jQuery(function ($) {
    //    $('#cussucesspend_id').prop('checked', false);
    //})

    //jQuery(function ($) {
    //    $('#cussms_id').prop('checked', false);
    //})

    //jQuery(function ($) {
    //    $('#cusalwemail_id').prop('checked', false);
    //})

    jQuery('.add-cus-list').click(function (e) {


        if (jQuery("#MBE_CD").val() == "") {
            setInfoMsg("Please Select Customer");
            return;
        }
        Lobibox.confirm({
            msg: "Do you want to continue process?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    jQuery.ajax({
                        type: "GET",
                        url: "/JobDefinition/AddCustomer",
                        data: { CustormerCode: jQuery("#MBE_CD").val(), CustomerType: jQuery("#en_type").val(), Exec: jQuery("#Jb_sales_ex_cd").val() },
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    SetCustormerDataList(result.data);
                                    jQuery("#MBE_CD").val("");
                                    jQuery("#MBE_OTH_ID_NO").val("");
                                    jQuery("#dealer_currncy").val("");
                                    jQuery("#MBE_NAME").val("");
                                    jQuery("#Jb_sales_ex_cd").val("");
                                    //jQuery("#cuscoun_id").val("");
                                    jQuery("#MBE_COUNTRY_CD").val("");
                                    jQuery("#MBE_ADD1").val("");
                                    jQuery("#MBE_ADD2").val("");
                                    //jQuery("#cuspro_id").val("");
                                    jQuery("#MBE_PROVINCE_CD").val("");
                                    //jQuery("#curdist_id").val("");
                                    jQuery("#MBE_DISTRIC_CD").val("");
                                    jQuery("#cus_addr2").val("");
                                    jQuery("#MBE_TEL").val("");
                                    jQuery("#MBE_FAX").val("");
                                    jQuery("#MBE_EMAIL").val("");
                                    jQuery("#MBE_IS_SVAT").val("");
                                    jQuery("#MBE_IS_SVAT").val("");
                                    jQuery("#MBE_SVAT_NO").val("");
                                    
                                } else {
                                    if (result.type == "Error") {
                                        setError(result.msg);

                                    }
                                    if (result.type == "Info") {
                                        setInfoMsg(result.msg);
                                    }
                                    if (result.notice == true) {
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
    });

});