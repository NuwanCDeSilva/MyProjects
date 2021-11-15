function dataEntry(){
    var type = "new";
    if (jQuery(".frm-cust-det #Mbe_cust_lang").length > 0) {
        loadLanguage();
    }
    if (jQuery(".frm-cust-det #Mbe_cate").length > 0) {
        loadCusType();
    }
    if (jQuery(".frm-cust-det #MBE_TIT").length > 0) {
        loadTitle();
    }
    if (jQuery(".frm-cust-det #Mbe_sex").length > 0) {
        loadSex();
    }
    clearCustomerData();
    function loadLanguage() {
        jQuery.ajax({
            type: "GET",
            url: "/DataEntry/LoadLanguage",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        var select = document.getElementById("Mbe_cust_lang");
                        jQuery("#Mbe_cust_lang").empty();
                        var options = [];
                        var option = document.createElement('option');
                        if (result.data != null && result.data.length != 0) {
                            for (var i = 0; i < result.data.length; i++) {
                                option.text = result.data[i].MLA_DESC;
                                option.value = result.data[i].MLA_CD;
                                options.push(option.outerHTML);
                            }
                        } else {
                            option.text = "Select Language";
                            option.value = "";
                            options.push(option.outerHTML);
                        }
                        select.insertAdjacentHTML('beforeEnd', options.join('\n'));
                    } else {
                        option.text = "Select Language";
                        option.value = "";
                        options.push(option.outerHTML);
                        setError(result.msg);
                    }
                } else {
                    Logout();
                }
            }

        });
    }
 
    function loadCusType() {
        jQuery.ajax({
            type: "GET",
            url: "/DataEntry/LoadCustomerType",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        var select = document.getElementById("Mbe_cate");
                        jQuery("#Mbe_cate").empty();
                        var options = [];
                        var option = document.createElement('option');
                        if (result.data != null && result.data.length != 0) {
                            for (var i = 0; i < result.data.length; i++) {
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

        });
    }
    //function loadTaxPosition() {
    //    jQuery.ajax({
    //        type: "GET",
    //        url: "/DataEntry/loadTaxPosition",
    //        contentType: "application/json;charset=utf-8",
    //        dataType: "json",
    //        success: function (result) {
    //            if (result.login == true) {
    //                if (result.success == true) {
    //                    var select = document.getElementById("mbe_ref_no");
    //                    jQuery("#mbe_ref_no").empty();
    //                    var options = [];
    //                    var option = document.createElement('option');
    //                    if (result.data != null && result.data.length != 0) {
    //                        for (var i = 0; i < result.data.length; i++) {
    //                            option.text = result.data[i].Text;
    //                            option.value = result.data[i].Value;
    //                            options.push(option.outerHTML);
    //                        }
    //                    } else {
    //                        option.text = "Tax Position";
    //                        option.value = "";
    //                        options.push(option.outerHTML);
    //                    }
    //                    select.insertAdjacentHTML('beforeEnd', options.join('\n'));
    //                } else {
    //                    setError(result.msg);
    //                }
    //            } else {
    //                Logout();
    //            }
    //        }

    //    });
    //}
    //loadTaxPosition();
    function loadTitle() {
        jQuery.ajax({
            type: "GET",
            url: "/DataEntry/loadTitles",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        var select = document.getElementById("MBE_TIT");
                        jQuery("#MBE_TIT").empty();
                        var options = [];
                        var option = document.createElement('option');
                        if (result.data != null && result.data.length != 0) {
                            for (var i = 0; i < result.data.length; i++) {
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

        });
    }
    function loadSex() {
        jQuery.ajax({
            type: "GET",
            url: "/DataEntry/loadSex",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        var select = document.getElementById("Mbe_sex");
                        jQuery("#Mbe_sex").empty();
                        var options = [];
                        var option = document.createElement('option');
                        if (result.data != null && result.data.length != 0) {
                            for (var i = 0; i < result.data.length; i++) {
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

        });
    }

    function loadcreditperiod() {
        jQuery.ajax({
            type: "GET",
            url: "/DataEntry/loadcreditperiod",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        var select = document.getElementById("Mbe_Credit_Period");
                        jQuery("#Mbe_Credit_Period").empty();
                        var options = [];
                        var option = document.createElement('option');
                        if (result.data != null && result.data.length != 0) {
                            for (var i = 0; i < result.data.length; i++) {
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

        });
    }
    loadcreditperiod();
    jQuery("form.frm-cust-det #Mbe_cd").focusout(function () {
        codeFocusOut();
    });
    function codeFocusOut() {
        if (jQuery("form.frm-cust-det #Mbe_cd").val() != "") {

            jQuery.ajax({
                type: "GET",
                url: "/DataEntry/cusCodeTextChanged",
                data: { val: jQuery("form.frm-cust-det #Mbe_cd").val() },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                            if (result.success == true) {
                                if (typeof (result.local) != "undefined") {
                                    setFieldValue(result.data, false, true);
                                    updateChgDetTable(result.chgcdDetList);
                                }
                                if (typeof (result.group) != "undefined") {
                                    setFieldValue(result.data, true, false);
                                    updateChgDetTable(result.chgcdDetList);
                                }
                                if (typeof (result.group) == "undefined" && typeof (result.local) == "undefined") {
                                    jQuery(".btn-save-data").val("Create");
                                    jQuery(".txt-operation").val("Create");
                                    jQuery(".txt-type").val("");
                                    fieldEnable();
                                }
                            } else {
                                clearCustomerData();
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
    }
    jQuery("form.frm-cust-det #Mbe_cr_town_cd").focusout(function () {
        if (jQuery(this).val() != "") {
            crtownOut(jQuery(this).val());            
        }
    });
    function crtownOut(town) {
        jQuery.ajax({
            type: "GET",
            url: "/DataEntry/preTownTextChanged",
            data: { val: town },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (typeof (result.data) != "undefined") {
                            jQuery("#Mbe_cr_distric_cd").val(result.data.district);
                            jQuery("#Mbe_cr_province_cd").val(result.data.province);
                            jQuery("#Mbe_cr_postal_cd").val(result.data.postalCD);
                            jQuery("#Mbe_cr_country_cd").val(result.data.countryCD);
                        }
                    } else {
                        jQuery("#Mbe_cr_town_cd").val("");
                        jQuery("#Mbe_cr_distric_cd").val("");
                        jQuery("#Mbe_cr_province_cd").val("");
                        jQuery("#Mbe_cr_postal_cd").val("");
                        jQuery("#Mbe_cr_country_cd").val("");
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

    jQuery("form.frm-cust-det #Mbe_town_cd").focusout(function () {
        if (jQuery(this).val() != "") {
            loadTownData(jQuery(this).val());
        }
    });
    function loadTownData(towncd) {
        jQuery.ajax({
            type: "GET",
            url: "/DataEntry/preTownTextChanged",
            data: { val: towncd },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (typeof (result.data) != "undefined") {
                            jQuery("#Mbe_distric_cd").val(result.data.district);
                            jQuery("#Mbe_province_cd").val(result.data.province);
                            jQuery("#Mbe_postal_cd").val(result.data.postalCD);
                            //jQuery("#Mbe_country_cd").val(result.data.countryCD);
                        }
                    } else {
                        jQuery("#Mbe_town_cd").val("");
                        jQuery("#Mbe_distric_cd").val("");
                        jQuery("#Mbe_province_cd").val("");
                        jQuery("#Mbe_postal_cd").val("");
                        //jQuery("#Mbe_country_cd").val("");
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
    jQuery("form.frm-cust-det #Mbe_dl_no").focusout(function () {
        var attr = $(this).attr('readonly');
        if (typeof attr !== typeof undefined && attr !== false) {
            // ...
        } else {
            if (jQuery(this).val() != "") {
                jQuery.ajax({
                    type: "GET",
                    url: "/DataEntry/DLTextChanged",
                    data: { val: jQuery(this).val() },
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        if (result.login == true) {
                            if (result.success == true) {
                                if (typeof (result.local) != "undefined") {
                                    setFieldValue(result.data, false, true);
                                    updateChgDetTable(result.chgcdDetList);
                                }
                                if (typeof (result.group) != "undefined") {
                                    setFieldValue(result.data, true, false);
                                    updateChgDetTable(result.chgcdDetList);
                                }
                                if (typeof (result.group) == "undefined" && typeof (result.local) == "undefined") {
                                    jQuery(".btn-save-data").val("Create");
                                    jQuery(".txt-operation").val("Create");
                                    jQuery(".txt-type").val("");
                                    fieldEnable();
                                }
                            } else {
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
        }
    });
    jQuery("form.frm-cust-det #Mbe_mob").focusout(function () {
        var attr = $(this).attr('readonly');
        if (typeof attr !== typeof undefined && attr !== false) {
            // ...
        } else {
            if (jQuery(this).val() != "") {
                jQuery.ajax({
                    type: "GET",
                    url: "/DataEntry/MobiletextChanged",
                    data: { val: jQuery(this).val() },
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        if (result.login == true) {
                            if (result.success == true) {
                                if (typeof (result.local) != "undefined") {
                                    setFieldValue(result.data, false, true);
                                    updateChgDetTable(result.chgcdDetList);
                                }
                                if (typeof (result.group) != "undefined") {
                                    setFieldValue(result.data, true, false);
                                    updateChgDetTable(result.chgcdDetList);
                                }
                                if (typeof (result.group) == "undefined" && typeof (result.local) == "undefined") {
                                    jQuery(".btn-save-data").val("Create");
                                    jQuery(".txt-operation").val("Create");
                                    jQuery(".txt-type").val("");
                                    fieldEnable();
                                }
                            } else {
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
        }

    });
    jQuery("form.frm-cust-det #Mbe_br_no").focusout(function () {
        var attr = $(this).attr('readonly');
        if (typeof attr !== typeof undefined && attr !== false) {
            // ...
        } else {
            if (jQuery(this).val() != "") {
                jQuery.ajax({
                    type: "GET",
                    url: "/DataEntry/BRTextChanged",
                    data: { val: jQuery(this).val() },
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        if (result.login == true) {
                            if (result.success == true) {
                                if (typeof (result.local) != "undefined") {
                                    setFieldValue(result.data, false, true);
                                    updateChgDetTable(result.chgcdDetList);
                                }
                                if (typeof (result.group) != "undefined") {
                                    setFieldValue(result.data, true, false);
                                    updateChgDetTable(result.chgcdDetList);
                                }
                                if (typeof (result.group) == "undefined" && typeof (result.local) == "undefined") {
                                    jQuery(".btn-save-data").val("Create");
                                    jQuery(".txt-operation").val("Create");
                                    jQuery(".txt-type").val("");
                                    fieldEnable();
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
    //function Logout() {
    //    if (alert("Login session has expired!") == true) {

    //    } else {
    //        window.location.replace("/Login/Index");
    //    }
    //}
    jQuery('#Mbe_mob,#Mbe_tel,#Mbe_cr_tel,#Mbe_wr_tel,#Mbe_wr_fax').on('input', function (event) {
        this.value = this.value.replace(/[^0-9]/g, '');
    });

    jQuery('#MBE_FNAME,#MBE_SNAME,#Mbe_name,#Mbe_wr_com_name').on('input', function () {
        var node = jQuery(this);
        node.val(node.val().replace(/[^a-z^A-Z^ ]/g, ''));
    });
    function setFieldValue(data, group, local) {
        if (data != "" && local) {
            jQuery("#MBE_FNAME").val(data.MBE_FNAME);
            jQuery("#MBE_INI").val(data.MBE_INI);
            jQuery("#MBE_SNAME").val(data.MBE_SNAME);
            jQuery("#Mbe_add1").val(data.Mbe_add1);
            jQuery("#Mbe_add2").val(data.Mbe_add2);
            jQuery('#Mbe_agre_send_email').prop('checked', data.Mbe_agre_send_email);
            jQuery('#Mbe_agre_send_sms').prop('checked', data.Mbe_agre_send_sms);
            jQuery("#Mbe_br_no").val(data.Mbe_br_no);
            jQuery("#Mbe_cate").val(data.Mbe_cate);
            jQuery("#Mbe_cd").val(data.Mbe_cd);
            jQuery("#Mbe_com").val(data.Mbe_com);
            jQuery("#Mbe_country_cd").val(data.Mbe_country_cd);
            jQuery("#Mbe_cr_add1").val(data.Mbe_cr_add1);
            jQuery("#Mbe_cr_add2").val(data.Mbe_cr_add2);
            jQuery("#Mbe_cr_country_cd").val(data.Mbe_cr_country_cd);
            jQuery("#Mbe_cr_email").val(data.Mbe_cr_email);
            jQuery("#Mbe_cr_fax").val(data.Mbe_cr_fax);
            jQuery("#Mbe_cr_postal_cd").val(data.Mbe_cr_postal_cd);
            jQuery("#Mbe_cr_province_cd").val(data.Mbe_cr_province_cd);
            jQuery("#Mbe_cr_tel").val(data.Mbe_cr_tel);
            jQuery("#Mbe_cr_town_cd").val(data.Mbe_cr_town_cd);
            crtownOut(data.Mbe_cr_town_cd);
            jQuery("#Mbe_cust_lang").val(data.Mbe_cust_lang);
            jQuery("#Mbe_distric_cd").val(data.Mbe_distric_cd);
            jQuery("#Mbe_dl_no").val(data.Mbe_dl_no);
            var dob = new Date(parseInt(data.Mbe_dob.substr(6)));
            if (my_date_format(dob) != "NaN/undefined/NaN")
                jQuery("#Mbe_dob").val(my_date_format(dob));
            jQuery("#Mbe_email").val(data.Mbe_email);
            jQuery('#Mbe_agre_send_email').prop('checked', data.Mbe_is_svat);
            jQuery('#Mbe_is_tax').prop('checked', data.Mbe_is_tax);
            jQuery("#Mbe_mob").val(data.Mbe_mob);

            //var title = data.Mbe_name.substr(0, data.Mbe_name.indexOf('.'));
            //var name = data.Mbe_name.substr(data.Mbe_name.indexOf(' ') + 1);
            jQuery("#Mbe_name").val(data.Mbe_name);//(data.Mbe_name);
            jQuery("#MBE_TIT").val(data.MBE_TIT);//(data.MBE_TIT);
            jQuery("#Mbe_nationality").val(data.Mbe_nationality);
            jQuery("#Mbe_nic").val(data.Mbe_nic);
            jQuery("#Mbe_postal_cd").val(data.Mbe_postal_cd);
            jQuery("#Mbe_pp_no").val(data.Mbe_pp_no);
            jQuery("#Mbe_province_cd").val(data.Mbe_province_cd);
            jQuery("#Mbe_sex").val(data.Mbe_sex);
            jQuery("#Mbe_svat_no").val(data.Mbe_svat_no);
            jQuery("#Mbe_tax_ex").val(data.Mbe_tax_ex);
            jQuery("#Mbe_tax_no").val(data.Mbe_tax_no);
            jQuery("#Mbe_tel").val(data.Mbe_tel);
            jQuery("#Mbe_town_cd").val(data.Mbe_town_cd);

            loadTownData(data.Mbe_town_cd);

            jQuery("#Mbe_wr_add1").val(data.Mbe_wr_add1);
            jQuery("#Mbe_wr_add2").val(data.Mbe_wr_add2);
            jQuery("#Mbe_wr_com_name").val(data.Mbe_wr_com_name);
            jQuery("#Mbe_wr_dept").val(data.Mbe_wr_dept);
            jQuery("#Mbe_wr_designation").val(data.Mbe_wr_designation);
            jQuery("#Mbe_wr_email").val(data.Mbe_wr_email);
            jQuery("#Mbe_wr_fax").val(data.Mbe_wr_fax);
            jQuery("#Mbe_wr_tel").val(data.Mbe_wr_tel);
            jQuery("#Mbe_acc_cd").val(data.Mbe_acc_cd);

            var biyr = new Date(parseInt(data.Mbe_BI_Year.substr(6)));
            if (my_date_format(biyr) != "NaN/undefined/NaN")
                jQuery("#Mbe_BI_Year").val(my_date_format(biyr));

            jQuery("#Mbe_Credit_Period").val(data.Mbe_Credit_Period);

            var ppisu = new Date(parseInt(data.Mbe_pp_isu_dte.substr(6)));
            if (my_date_format(ppisu) != "NaN/undefined/NaN")
                jQuery("#Mbe_pp_isu_dte").val(my_date_format(ppisu));
            var ppexp = new Date(parseInt(data.Mbe_pp_exp_dte.substr(6)));
            if (my_date_format(ppexp) != "NaN/undefined/NaN")
                jQuery("#Mbe_pp_exp_dte").val(my_date_format(ppexp));
            var dlisu = new Date(parseInt(data.Mbe_dl_isu_dte.substr(6)));
            if (my_date_format(dlisu) != "NaN/undefined/NaN")
                jQuery("#Mbe_dl_isu_dte").val(my_date_format(dlisu));
            var dlexp = new Date(parseInt(data.Mbe_dl_exp_dte.substr(6)));
            if (my_date_format(dlexp) != "NaN/undefined/NaN")
                jQuery("#Mbe_dl_exp_dte").val(my_date_format(dlexp));

            
            jQuery(".btn-save-data").val("Update");
            jQuery(".txt-operation").val("Update");
            type = "local";
            jQuery(".txt-type").val("local");
            fieldDisable();
        }
        if (data != "" && group) {
            jQuery("#Mbe_nic").val(data.Mbg_nic);
            jQuery("#Mbe_add1").val(data.Mbg_add1);
            jQuery("#Mbe_add2").val(data.Mbg_add2);
            jQuery("#Mbe_br_no").val(data.Mbg_br_no);
            jQuery("#Mbe_cd").val(data.Mbg_cd);
            jQuery("#Mbe_country_cd").val(data.Mbg_country_cd);
            jQuery("#Mbe_distric_cd").val(data.Mbg_distric_cd);
            jQuery("#Mbe_dl_no").val(data.Mbg_dl_no);
            var dob = new Date(parseInt(data.Mbg_dob.substr(6)));
            if (my_date_format(dob) != "NaN/undefined/NaN")
                jQuery("#Mbe_dob").val(my_date_format(dob));
            jQuery("#Mbe_email").val(data.Mbg_email);
            jQuery("#Mbe_fax").val(data.Mbg_fax);
            jQuery("#Mbe_fname").val(data.Mbg_fname);
            jQuery("#Mbe_ini").val(data.Mbg_ini);
            jQuery("#Mbe_mob").val(data.Mbg_mob);
            //jQuery("#Mbe_name").val(data.Mbg_name);
            jQuery("#Mbe_nationality").val(data.Mbg_nationality);
            jQuery("#Mbe_nic").val(data.Mbg_nic);
            jQuery("#Mbe_postal_cd").val(data.Mbg_postal_cd);
            jQuery("#Mbe_pp_no").val(data.Mbg_pp_no);
            jQuery("#Mbe_province_cd").val(data.Mbg_province_cd);
            jQuery("#Mbe_sex").val(data.Mbg_sex);
            jQuery("#Mbe_sname").val(data.Mbg_sname);
            jQuery("#Mbe_tel").val(data.Mbg_tel);
            //var title = data.Mbg_name.substr(0, data.Mbg_name.indexOf('.'));
            //var name = data.Mbg_name.substr(data.Mbg_name.indexOf(' ') + 1);
            jQuery("#Mbe_name").val(data.Mbg_name);//(data.Mbe_name);
            jQuery("#Mbe_tit").val(data.Mbg_tit);//(data.MBE_TIT);

            //jQuery("#Mbe_tit").val(data.Mbg_tit);

            var ppisu = new Date(parseInt(data.Mbg_pp_isu_dte.substr(6)));
            if (my_date_format(ppisu) != "NaN/undefined/NaN")
                jQuery("#Mbe_pp_isu_dte").val(my_date_format(ppisu));
            var ppexp = new Date(parseInt(data.Mbg_pp_exp_dte.substr(6)));
            if (my_date_format(ppexp) != "NaN/undefined/NaN")
                jQuery("#Mbe_pp_exp_dte").val(my_date_format(ppexp));
            var dlisu = new Date(parseInt(data.Mbg_dl_isu_dte.substr(6)));
            if (my_date_format(dlisu) != "NaN/undefined/NaN")
                jQuery("#Mbe_dl_isu_dte").val(my_date_format(dlisu));
            var dlexp = new Date(parseInt(data.Mbg_dl_exp_dte.substr(6)));
            if (my_date_format(dlexp) != "NaN/undefined/NaN")
                jQuery("#Mbe_dl_exp_dte").val(my_date_format(dlexp));

            var biyr = new Date(parseInt(data.Mbe_BI_Year.substr(6)));
            if (my_date_format(biyr) != "NaN/undefined/NaN")
                jQuery("#Mbe_BI_Year").val(my_date_format(biyr));

            jQuery("#Mbe_Credit_Period").val(data.Mbe_Credit_Period);

            jQuery("#Mbe_town_cd").val(data.Mbg_town_cd);
            jQuery(".btn-save-data").val("Update");
            jQuery(".txt-operation").val("Update");
            type = "group";
            jQuery(".txt-type").val("group");
            fieldDisable();
        }


    }
    var my_date_format = function (input) {
        var d = new Date(Date.parse(input));
        var month = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
        var date = d.getDay() + "/" + month[d.getMonth()] + "/" + d.getFullYear();
        var time = d.toLocaleTimeString().toLowerCase().replace(/([\d]+:[\d]+):[\d]+(\s\w+)/g, "$1$2");
        return (date);
    };
    jQuery('#Mbe_dob').datepicker({ maxDate: new Date(), dateFormat: "dd/M/yy" })

    jQuery('#Mbe_pp_isu_dte').datepicker({ maxDate: new Date(), dateFormat: "dd/M/yy" })
    jQuery('#Mbe_pp_exp_dte').datepicker({ dateFormat: "dd/M/yy" })
    jQuery('#Mbe_dl_isu_dte').datepicker({ maxDate: new Date(), dateFormat: "dd/M/yy" })
    jQuery('#Mbe_dl_exp_dte').datepicker({ dateFormat: "dd/M/yy" })
    jQuery('#Mbe_BI_Year').datepicker({ dateFormat: "dd/M/yy" })

    jQuery(".btn-clear-data").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to clear data ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    clearCustomerData();
                }
            }
        });

    });
    
    jQuery("#Mbe_cd").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Name", "Nic", "Mobile", "Br No"];
            field = "cusCode"
            var x = new CommonSearch(headerKeys, field);
        }
        if (evt.keyCode == 13) {
            codeFocusOut();
        }
    });

    jQuery("#Mbe_town_cd").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Town", "District", "Province", "Code"];
            field = "perTown"
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery("#Mbe_cr_town_cd").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Town", "District", "Province", "Code"];
            field = "preTown"
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery(".cus-cd-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Name", "Nic", "Mobile", "Br No"];
        field = "cusCode"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".per-town-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Town", "District", "Province", "Code"];
        field = "perTown"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".pre-town-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Town", "District", "Province", "Code"];
        field = "preTown"
        var x = new CommonSearch(headerKeys, field);
    });
    if (jQuery("#Mbe_nic").length > 0) {
        jQuery("#Mbe_nic").focusout(function () {
            var attr = $(this).attr('readonly');
            if (typeof attr !== typeof undefined && attr !== false) {
                // ...
            } else {
                loadFromNic();
                loadDataFromNic();
            }
        });
        jQuery("#Mbe_nic").keypress(function (evt) {
            if (evt.keyCode == 13) {
                var attr = $(this).attr('readonly');
                if (typeof attr !== typeof undefined && attr !== false) {
                    // ...
                } else {
                    loadFromNic();
                    loadDataFromNic();
                }
            }

        });
    }

    function loadFromNic() {
        jQuery.ajax({
            type: "GET",
            url: "/DataEntry/dobGeneration",
            contentType: "application/json;charset=utf-8",
            data: { nic: jQuery("#Mbe_nic").val() },
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.dob != "") {
                            jQuery("#Mbe_dob").val(result.dob);
                        } else {
                            jQuery("#Mbe_dob").val("");
                        }
                    }
                } else {
                    Logout();
                }
            }
        });
    }
    function loadDataFromNic() {
        if (jQuery("#Mbe_nic").val() != "") {
            jQuery.ajax({
                type: "GET",
                url: "/DataEntry/getDataCustomerFromNic",
                data: { nic: jQuery("#Mbe_nic").val() },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (typeof (result.local) != "undefined") {
                                setFieldValue(result.data, false, true);
                            }
                            if (typeof (result.group) != "undefined") {
                                setFieldValue(result.data, true, false);
                            }
                            if (typeof (result.group) == "undefined" && typeof (result.local) == "undefined") {
                                jQuery(".btn-save-data").val("Create");
                                jQuery(".txt-operation").val("Create");
                                jQuery(".txt-type").val("");
                                fieldEnable();
                            }
                        }
                    } else {
                        Logout();
                    }
                }
            });
        } else {
            jQuery("#Mbe_dob").val("");
        }
    }
    jQuery("#Mbe_cr_tel").focusout(function () {
        if (jQuery.isNumeric(jQuery("#Mbe_cr_tel").val())) {
            if (jQuery("#Mbe_cr_tel").val().length === 10) {

            } else {
                setInfoMsg("Invalid phone number.");
                jQuery("#Mbe_cr_tel").val("");
                jQuery("#Mbe_cr_tel").focus();
            }
        } else {
            setInfoMsg("Invalid phone number.");
            jQuery("#Mbe_cr_tel").val("");
            jQuery("#Mbe_cr_tel").focus();
        }
    });
    jQuery("#MEMP_CON_PER_MOB").focusout(function () {
        if (jQuery.isNumeric(jQuery("#MEMP_CON_PER_MOB").val())) {
            if (jQuery("#MEMP_CON_PER_MOB").val().length === 10) {

            } else {
                setInfoMsg("Invalid phone number.");
                jQuery("#MEMP_CON_PER_MOB").val("");
                jQuery("#MEMP_CON_PER_MOB").focus();
            }
        } else {
            setInfoMsg("Invalid phone number.");
            jQuery("#MEMP_CON_PER_MOB").val("");
            jQuery("#MEMP_CON_PER_MOB").focus();
        }
    });


    jQuery("#Mbe_tel").focusout(function () {
        if (jQuery.isNumeric(jQuery("#Mbe_tel").val())) {
            if (jQuery("#Mbe_tel").val().length == 10) {

            } else {
                setInfoMsg("Invalid phone number.");
                jQuery("#Mbe_tel").val("");
                jQuery("#Mbe_tel").focus();
            }
        }
    });
    jQuery("#Mbe_mob").focusout(function () {

        if (jQuery.isNumeric(jQuery("#Mbe_mob").val())) {
            if (jQuery("#Mbe_mob").val().length > 9 && jQuery("#Mbe_mob").val().length < 12) {

            } else {
                setInfoMsg("Invalid mobile number.");
                jQuery("#Mbe_mob").val("");
                jQuery("#Mbe_mob").focus();
            }
        }


    });
    jQuery("#Mbe_wr_email").focusout(function () {
        if (jQuery(this).val() != "") {
            if (!isValidEmailAddress(jQuery(this).val())) {
                jQuery("#Mbe_wr_email").val("");
                setInfoMsg("Invalid email address.");
                jQuery("#Mbe_wr_email").focus();
            }
        }
    });
    jQuery("#Mbe_email").focusout(function () {
        if (jQuery(this).val() != "") {
            if (!isValidEmailAddress(jQuery(this).val())) {
                jQuery("#Mbe_email").val("");
                setInfoMsg("Invalid email address.");
                jQuery("#Mbe_email").focus();
            }
        }
    });
    jQuery("#Mbe_country_cd").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description", "Region Code", "Capital"];
            field = "presentCountry";
            var x = new CommonSearch(headerKeys, field);
        }
        if (evt.keyCode == 13) {
            validatePreCountry(jQuery(this).val());
        }
    });
    jQuery(".cntry-cd-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description", "Region Code", "Capital"];
        field = "presentCountry";
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery("#Mbe_country_cd").focusout(function () {
        if (jQuery(this).val()!="") {
            validatePreCountry(jQuery(this).val());
        }
    });

    function validatePreCountry(code) {
        if (code != "") {
            jQuery.ajax({
                type: "GET",
                url: "/DataEntry/validateCountry",
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
                            jQuery("#Mbe_country_cd").val("");
                        }
                    } else {
                        Logout();
                    }
                }
            });
        }
    }

    //jQuery("#Mbe_cr_country_cd").on("keydown", function (evt) {
    //    if (evt.keyCode == 113) {
    //        var headerKeys = Array()
    //        headerKeys = ["Row", "Code", "Description", "Region Code", "Capital"];
    //        field = "crCountry";
    //        var x = new CommonSearch(headerKeys, field);
    //    }
    //    if (evt.keyCode == 13) {
    //        validateCrCountry(jQuery(this).val());
    //    }
    //});
    //jQuery(".cntry-cr-cd-search").click(function () {
    //    var headerKeys = Array()
    //    headerKeys = ["Row", "Code", "Description", "Region Code", "Capital"];
    //    field = "crCountry";
    //    var x = new CommonSearch(headerKeys, field);
    //});
    //jQuery("#Mbe_cr_country_cd").focusout(function () {
    //    if (jQuery(this).val() != "") {
    //        validateCrCountry(jQuery(this).val());
    //    }
    //});
    //function validateCrCountry(code) {
    //    if (code != "") {
    //        jQuery.ajax({
    //            type: "GET",
    //            url: "/DataEntry/validateCountry",
    //            contentType: "application/json;charset=utf-8",
    //            data: { country: code },
    //            dataType: "json",
    //            success: function (result) {
    //                if (result.login == true) {
    //                    if (result.success == false) {
    //                        if (result.type == "Error") {
    //                            setError(result.msg);
    //                        }
    //                        if (result.type == "Info") {
    //                            setInfoMsg(result.msg);
    //                        }
    //                        jQuery("#Mbe_cr_country_cd").val("");
    //                    }
    //                } else {
    //                    Logout();
    //                }
    //            }
    //        });
    //    }
    //}

    //Add charge codes

    jQuery(".btn-add-data").unbind().click(function () {
        if (jQuery("#Mbe_cd").val() != "") {
            jQuery('#addChgcdPop').modal({
                keyboard: false,
                backdrop: 'static'
            }, 'show');
            jQuery("#cusCd").val(jQuery("#Mbe_cd").val());
            jQuery(".btn-chg-save").unbind().click(function () {
                jQuery.ajax({
                    type: "GET",
                    url: "/DataEntry/SaveBusChg",
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        if (result.login == true) {
                            if (result.success == false) {
 
                                Lobibox.confirm({
                                    msg: result.msg,
                                    callback: function ($this, type, ev) {
                                        if (type == "yes") {
                                            jQuery('#addChgcdPop').modal('hide');
                                           
                                        }
                                    }
                                });
                            } else {
                                jQuery('#addChgcdPop').modal('hide');
                                setSuccesssMsg(" Update Successfull");
                            }
                        } else {
                            Logout();
                        }
                    }
                });


            });
            jQuery(".cus-det-add").unbind().click(function () {
                if (jQuery("#cusCd").val() != "" && jQuery("#chgtype").val() != "" && jQuery("#chgcd").val() != "") {
                    jQuery.ajax({
                        type: "GET",
                        url: "/DataEntry/addChargeCodes",
                        data: { cusCd: jQuery("#cusCd").val(), chgtype: jQuery("#chgtype").val(), chgcd:jQuery("#chgcd").val() },
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    updateChgDetTable(result.chgcdDetList);
                                    jQuery("#chgcd").val("");
                                } else {
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
                    return false;
                } else {
                    setInfoMsg("Invalid Charge Code details.");
                }
                return false;
            });
           

        } else {
            setInfoMsg("Please enter Customer Code..");
        }

    });

    function updateChgDetTable(det) {
        jQuery('.inv-cus-tab .new-row').remove();
        if (det != null) {
            for (i = 0; i < det.length; i++) {
                jQuery('.inv-cus-tab').append('<tr class="new-row">' +
                        '<td>' + det[i].bcd_cus_cd + '</td>' +
                        '<td>' + det[i].bcd_chg_type + '</td>' +
                        '<td>' + det[i].bcd_chg_cd + '</td>' +
                         '<td><img class="delete-img remove-cus-cls" src="/Resources/images/Remove.png"></td>' +
                        '</tr>');
            }
           removeClickFunction();
        }
    }
    function removeClickFunction() {
        jQuery(".remove-cus-cls").unbind().click(function () {
            var td = jQuery(this);
            var value = jQuery(td).closest("tr").find('td:eq(2)').text();
            Lobibox.confirm({
                msg: "Do you want to remove this ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/DataEntry/RemoveChargeCodes?chgcd=" + value,
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        updateChgDetTable(result.chgcdDetList);
                                    } else {
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
                }
            });
        });

    }
    jQuery("#Mbe_pp_exp_dte,#Mbe_pp_isu_dte,#Mbe_pp_exp_dte,#Mbe_dl_isu_dte,#Mbe_dl_exp_dte,#Mbe_dob").focusout(function () {
        if (ValidDate(jQuery(this).val()) == "false" && jQuery(this).val()!="") {
            setInfoMsg("Please enter valid date.");
            jQuery(this).val("");
        }
    });

    jQuery("#Mbe_cate").change(function () {

        if (jQuery("#Mbe_cate").val() == "COMPANY") {
            jQuery("#Mbe_pp_no").attr("disabled", true);
            jQuery("#Mbe_pp_exp_dte").attr("disabled", true);
            jQuery("#Mbe_pp_isu_dte").attr("disabled", true);
            jQuery("#Mbe_dl_no").attr("disabled", true);
            jQuery("#Mbe_dl_isu_dte").attr("disabled", true);
            jQuery("#Mbe_dl_exp_dte").attr("disabled", true);
            jQuery("#Mbe_sex").attr("disabled", true);
            jQuery("#MBE_TIT").attr("disabled", true);
            jQuery("#MBE_INI").attr("disabled", true);
            jQuery("#Mbe_dob").attr("disabled", true);
            jQuery("#Mbe_nic").attr("disabled", true);
            jQuery("#MBE_SNAME").attr("disabled", true);
            
            
        } else {
            jQuery("#Mbe_pp_no").attr("disabled", false);
            jQuery("#Mbe_pp_exp_dte").attr("disabled", false);
            jQuery("#Mbe_pp_isu_dte").attr("disabled", false);
            jQuery("#Mbe_dl_no").attr("disabled", false);
            jQuery("#Mbe_dl_isu_dte").attr("disabled", false);
            jQuery("#Mbe_dl_exp_dte").attr("disabled", false);
            jQuery("#Mbe_sex").attr("disabled", false);
            jQuery("#MBE_TIT").attr("disabled", false);
            jQuery("#MBE_INI").attr("disabled", false);
            jQuery("#Mbe_dob").attr("disabled", false);
            jQuery("#Mbe_nic").attr("disabled", false);
            jQuery("#MBE_SNAME").attr("disabled", false);
        }
       
    });

    loadCostType();
    function loadCostType() {
        jQuery.ajax({
            type: "GET",
            url: "/ChargeCodeDef/CostType",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("chgtype");
                        jQuery("#chgtype").empty();
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
                        jQuery("#chgtype").trigger("change");
                    } else {
                        setError(result.msg);
                    }
                } else {
                    Logout();
                }
            }
        })
    }

    jQuery(".air-code-search").click(function () {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description", "Service By", "From", "To", "Rate", "From Date", "To Date"];
            if (jQuery("#chgtype").val() == "AIRTVL")
            {
                field = "ChrgCdeSrchArival"
            }
            if (jQuery("#chgtype").val() == "MSCELNS" || jQuery("#chgtype").val() == "OVSLAGMT" || jQuery("#chgtype").val() == "HTLRTS") {
                field = "ChrgCdeSrchMsclens"
            }
            if (jQuery("#chgtype").val() == "TRANS") {
                field = "ChrgCdeSrchTrans"
            }
            var x = new CommonSearch(headerKeys, field, jQuery("#chgtype").val());
    });
};
function clearCustomerData() {
    document.getElementById("customer-crte-frm").reset();
    jQuery(".btn-save-data").val("Create");
    jQuery("#Mbe_postal_cd").attr('disabled', true);
    //jQuery("#Mbe_country_cd").attr('disabled', true);
    jQuery("#Mbe_cr_postal_cd").attr('disabled', true);
    jQuery("#Mbe_cr_country_cd").attr('disabled', true);
    jQuery("#Mbe_distric_cd").attr('disabled', true);
    jQuery("#Mbe_province_cd").attr('disabled', true);
    jQuery("#Mbe_cr_distric_cd").attr('disabled', true);
    jQuery("#Mbe_cr_province_cd").attr('disabled', true);
    fieldEnable();
}
function fieldEnable() {
    jQuery("#MBE_INI").removeAttr('readonly');
    //jQuery("#Mbe_dob").attr('readonly', true);
    jQuery("#MBE_FNAME").removeAttr('readonly');
    jQuery("#MBE_SNAME").removeAttr('readonly');
    jQuery("#Mbe_name").removeAttr('readonly');
    jQuery("#Mbe_nic").removeAttr('readonly');
    jQuery("#Mbe_cd").removeAttr('readonly');
    jQuery("#Mbe_pp_no").removeAttr('readonly');
    jQuery("#Mbe_dl_no").removeAttr('readonly');
    jQuery("#Mbe_br_no").removeAttr('readonly');
    jQuery("#Mbe_cate").removeAttr('readonly');
    jQuery("#MBE_TIT").removeAttr('readonly');
    jQuery("#Mbe_sex").removeAttr('readonly');
}
function fieldDisable() {
    jQuery("#MBE_INI").attr('readonly', true);
    //jQuery("#Mbe_dob").attr('readonly', true);
    //jQuery("#MBE_FNAME").attr('readonly', true);
    //jQuery("#MBE_SNAME").attr('readonly', true);
    //jQuery("#Mbe_name").attr('readonly', true);
    //jQuery("#Mbe_nic").attr('readonly', true);
    jQuery("#Mbe_cd").attr('readonly', true);
    //jQuery("#Mbe_pp_no").attr('readonly', true);
    //jQuery("#Mbe_dl_no").attr('readonly', true);
    //jQuery("#Mbe_br_no").attr('readonly', true);
    jQuery("#Mbe_cate").attr('readonly', true);
    jQuery("#MBE_TIT").attr('readonly', true);
    jQuery("#Mbe_sex").attr('readonly', true);
}
function isValidEmailAddress(emailAddress) {
    var pattern = /^([a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+(\.[a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+)*|"((([ \t]*\r\n)?[ \t]+)?([\x01-\x08\x0b\x0c\x0e-\x1f\x7f\x21\x23-\x5b\x5d-\x7e\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|\\[\x01-\x09\x0b\x0c\x0d-\x7f\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))*(([ \t]*\r\n)?[ \t]+)?")@(([a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.)+([a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.?$/i;
    return pattern.test(emailAddress);
}
//if (jQuery("#Mbe_cate").val() != "COMPANY") {
//    jQuery(".com_details").hide();
//}
