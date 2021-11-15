jQuery(document).ready(function () {
    jQuery('#SAC_FRM_DT').datepicker({ dateFormat: "dd/M/yy" })
    jQuery('#SAC_TO_DT').datepicker({ dateFormat: "dd/M/yy" })
    jQuery('#STC_FRM_DT').datepicker({ dateFormat: "dd/M/yy" })
    jQuery('#STC_TO_DT').datepicker({ dateFormat: "dd/M/yy" })
    jQuery('#SSM_FRM_DT').datepicker({ dateFormat: "dd/M/yy" })
    jQuery('#SSM_TO_DT').datepicker({ dateFormat: "dd/M/yy" })

    
    jQuery(".from-town-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description", "Region Code", "Capital"];
        field = "ChfCountry"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".to-town-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description", "Region Code", "Capital"];
        field = "ChtCountry"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".trn-frtwn-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Town", "District", "Province", "Code"];
        field = "chgcdfromTownSrchTrn"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".trn-totwn-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Town", "District", "Province", "Code"];
        field = "chgcdtoTownSrchTrn"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".air-code-search").click(function () {
        if (jQuery("#ChargeCode").val() == "AIRTVL")
        {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description", "Service By", "From", "To", "Rate", "From Date", "To Date"];
            field = "ChrgCdeSrchArival"
            var x = new CommonSearch(headerKeys, field, jQuery("#ChargeCode").val());
        } 

    });
    jQuery(".trns-code-search").click(function () {
        if (jQuery("#ChargeCode").val() == "TRANS") {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description", "Service By", "From Date", "To Date", "From", "To", "Rate", "Class", "Vehicle"];
            field = "ChrgCdeSrchTrans"
            var x = new CommonSearch(headerKeys, field,jQuery("#ChargeCode").val());
        }

    });
    jQuery(".msc-code-search").click(function () {
        if (jQuery("#ChargeCode").val() == "MSCELNS" || jQuery("#ChargeCode").val() == "OVSLAGMT" || jQuery("#ChargeCode").val() == "HTLRTS") {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description", "Service By", "From Date", "To Date", "Rate", ];
            field = "ChrgCdeSrchMsclens"
            var x = new CommonSearch(headerKeys, field, jQuery("#ChargeCode").val());
        }

    });

    loadingPkgTypes();
    function loadingPkgTypes() {
        jQuery.ajax({
            type: "GET",
            url: "/ChargeCodeDef/getPkgTypes",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("STC_RT_TP");
                        jQuery("#STC_RT_TP").empty();
                        var options = [];
                        var option = document.createElement('option');
                        if (result.data != null && result.data.length != 0) {
                            option.text = "-Select-";
                            option.value = "";
                            options.push(option.outerHTML);
                            for (i = 0; i < result.data.length; i++) {
                                option.text = result.data[i].SPT_DESC;
                                option.value = result.data[i].SPT_CD;
                                options.push(option.outerHTML);
                            }
                        } else {
                            option.text = "-Select-";
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
    jQuery("#SAC_CD").focusout(function () {
        jQuery.ajax({
            type: "GET",
            url: "/ChargeCodeDef/getAirChargeCodedet",
            contentType: "application/json;charset=utf-8",
            data: { val: jQuery(this).val(), chgcode: jQuery("#ChargeCode").val() },
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.data != "") {
                            setFieldValue(result.data);
                            jQuery(".btn-cd-save-data").val("Update");
                        } else {
                            jQuery(".btn-cd-save-data").val("Save");
                           
                            $('#SAC_RT, #SAC_FRM_DT, #SAC_TO_DT, #SAC_TIC_DESC, #SAC_ADD_DESC,#SAC_FROM,#SAC_TP,#SAC_TO').val('');
                          
                        }
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    jQuery("#SAC_CD").keypress(function (e) {
        if (e.which == 13) {
            jQuery.ajax({
                type: "GET",
                url: "/ChargeCodeDef/getAirChargeCodedet",
                contentType: "application/json;charset=utf-8",
                data: { val: jQuery(this).val(), chgcode: jQuery("#ChargeCode").val() },
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.data != "") {
                                setFieldValue(result.data);
                                jQuery(".btn-cd-save-data").val("Update");
                            } else {
                                jQuery(".btn-cd-save-data").val("Save");

                                $('#SAC_RT, #SAC_FRM_DT, #SAC_TO_DT, #SAC_TIC_DESC, #SAC_ADD_DESC,#SAC_FROM,#SAC_TP,#SAC_TO').val('');

                            }
                        }
                    } else {
                        Logout();
                    }
                }
            });
        }
    });
    loadAitTicketType();
    function loadAitTicketType() {
        jQuery.ajax({
            type: "GET",
            url: "/ChargeCodeDef/getAirTicketTypes",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("SAC_TCKT_TP");
                        jQuery("#SAC_TCKT_TP").empty();
                        var options = [];
                        var option = document.createElement('option');
                        if (result.data != null && result.data.length != 0) {
                            for (i = 0; i < result.data.length; i++) {
                                option.text = result.data[i].AIT_DESC;
                                option.value = result.data[i].AIT_CD;
                                options.push(option.outerHTML);
                            }
                        } else {
                            option.text = "-Select-";
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
    function setFieldValue(data)
    {
        if (data != "") {
            jQuery("#SAC_CUR").val(data[0].SAC_CUR);
            jQuery("#SAC_SCV_BY").val(data[0].SAC_SCV_BY);
            jQuery("#SAC_RT").val(data[0].SAC_RT);
            jQuery("#SAC_CLS").val(data[0].SAC_CLS);
            jQuery("#SAC_IS_CHILD").val(data[0].SAC_IS_CHILD);
            jQuery("#SAC_TIC_DESC").val(data[0].SAC_TIC_DESC);
            jQuery("#SAC_ADD_DESC").val(data[0].SAC_ADD_DESC);
            jQuery("#SAC_FROM").val(data[0].SAC_FROM);
            jQuery("#SAC_TP").val(data[0].SAC_TP);
            jQuery("#SAC_TO").val(data[0].SAC_TO);
            jQuery('#SAC_FRM_DT').val(getFormatedDateInput(data[0].SAC_FRM_DT));
            jQuery('#SAC_TO_DT').val(getFormatedDateInput(data[0].SAC_TO_DT));
            jQuery('#SAC_TAX_RT').val(data[0].SAC_TAX_RT);
            jQuery('#SAC_TCKT_TP').val(data[0].SAC_TCKT_TP);
            
        }
    }
    jQuery("#STC_CD").focusout(function () {
            jQuery.ajax({
                type: "GET",
                url: "/ChargeCodeDef/getTrnsChargeCodedet",
                contentType: "application/json;charset=utf-8",
                data: { val: jQuery(this).val(), chgcode: jQuery("#ChargeCode").val() },
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.data != "") {
                                setFieldValueTrns(result.data);
                                jQuery(".btn-cd-save-data").val("Update");
                            } else {
                                jQuery(".btn-cd-save-data").val("Save");

                                $('#STC_VEH_TP, #STC_DESC, #STC_RT_TP, #STC_FRM_KM, #STC_TO_KM,#STC_FRM_DT,#STC_TO_DT,#STC_FRM,#STC_TO').val('');
                            }
                        }
                    } else {
                        Logout();
                    }
                }
            });
    });
    jQuery("#STC_CD").keypress(function (e) {
        if (e.which == 13) {
            jQuery.ajax({
                type: "GET",
                url: "/ChargeCodeDef/getTrnsChargeCodedet",
                contentType: "application/json;charset=utf-8",
                data: { val: jQuery(this).val(), chgcode: jQuery("#ChargeCode").val() },
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.data != "") {
                                setFieldValueTrns(result.data);
                                jQuery(".btn-cd-save-data").val("Update");
                            } else {
                                jQuery(".btn-cd-save-data").val("Save");

                                $('#STC_VEH_TP, #STC_DESC, #STC_RT_TP, #STC_FRM_KM, #STC_TO_KM,#STC_FRM_DT,#STC_TO_DT,#STC_FRM,#STC_TO').val('');
                            }
                        }
                    } else {
                        Logout();
                    }
                }
            });
        }
    });
    function setFieldValueTrns(data) {
        if (data != "") {
            jQuery("#STC_VEH_TP").val(data[0].STC_VEH_TP);
            jQuery("#STC_DESC").val(data[0].STC_DESC);
            jQuery("#STC_RT_TP").val(data[0].STC_RT_TP);
            jQuery("#STC_SER_BY").val(data[0].STC_SER_BY);
            jQuery("#STC_CLS").val(data[0].STC_CLS);
            jQuery("#STC_FRM_KM").val(data[0].STC_FRM_KM);
            jQuery("#STC_TO_KM").val(data[0].STC_TO_KM);
            jQuery("#STC_AD_RT").val(data[0].STC_AD_RT);
            jQuery("#STC_FRM").val(data[0].STC_FRM);
            jQuery("#STC_RT").val(data[0].STC_RT);
            jQuery('#STC_FRM_DT').val(getFormatedDateInput(data[0].STC_FRM_DT));
            jQuery('#STC_TO_DT').val(getFormatedDateInput(data[0].STC_TO_DT));
            jQuery("#STC_CURR").val(data[0].STC_CURR);
            jQuery("#STC_TO").val(data[0].STC_TO);
            jQuery("#STC_TAX_RT").val(data[0].STC_TAX_RT);
        }
    }
    jQuery("#SSM_CD").focusout(function () {
        jQuery.ajax({
            type: "GET",
            url: "/ChargeCodeDef/getMscChargeCodedet",
            contentType: "application/json;charset=utf-8",
            data: { val: jQuery(this).val(), chgcode: jQuery("#ChargeCode").val() },
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.data != "")
                        {
                            setFieldValueMsc(result.data);
                            jQuery(".btn-cd-save-data").val("Update");
                        } else {
                            jQuery(".btn-cd-save-data").val("Save");
                            $('#SSM_DESC, #SSM_RT_TP, #SSM_FRM_DT, #SSM_TO_DT, #SSM_RT').val('');
                          
                        }
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    jQuery("#SSM_CD").keypress(function (e) {
        if (e.which == 13) {
            jQuery.ajax({
                type: "GET",
                url: "/ChargeCodeDef/getMscChargeCodedet",
                contentType: "application/json;charset=utf-8",
                data: { val: jQuery(this).val(), chgcode: jQuery("#ChargeCode").val() },
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.data != "") {
                                setFieldValueMsc(result.data);
                                jQuery(".btn-cd-save-data").val("Update");
                            } else {
                                jQuery(".btn-cd-save-data").val("Save");
                                $('#SSM_DESC, #SSM_RT_TP, #SSM_FRM_DT, #SSM_TO_DT, #SSM_RT').val('');

                            }
                        }
                    } else {
                        Logout();
                    }
                }
            });
        }
    });
    function setFieldValueMsc(data) {
        if (data != "") {
            jQuery("#SSM_DESC").val(data[0].SSM_DESC);
            jQuery("#SSM_SER_PRO").val(data[0].SSM_SER_PRO);
            jQuery("#SSM_RT_TP").val(data[0].SSM_RT_TP);
            jQuery("#SSM_CUR").val(data[0].SSM_CUR);
            jQuery("#SSM_RT").val(data[0].SSM_RT);
            jQuery('#SSM_FRM_DT').val(getFormatedDateInput(data[0].SSM_FRM_DT));
            jQuery('#SSM_TO_DT').val(getFormatedDateInput(data[0].SSM_TO_DT));
            jQuery("#SSM_TAX_RT").val(data[0].SSM_TAX_RT);
            jQuery("#SSM_PARENT_CD").val(data[0].SSM_PARENT_CD);
            jQuery("#SSM_PERDAY_RTE").val(data[0].SSM_PERDAY_RTE);
        }
    }
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
                        var select = document.getElementById("ChargeCode");
                        jQuery("#ChargeCode").empty();
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
                        jQuery("#ChargeCode").trigger("change");
                    } else {
                        setError(result.msg);
                    }
                } else {
                    Logout();
                }
            }
        })
    }
    loadCurrency();
    function loadCurrency() {
        jQuery.ajax({
            type: "GET",
            url: "/ChargeCodeDef/Currency",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("SAC_CUR");
                        jQuery("#SAC_CUR").empty();
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
                        if (options.length > 0)
                            jQuery("#SAC_CUR").val("USD");
                    } else {
                        setError(result.msg);
                    }
                } else {
                    Logout();
                }
            }
        })
    }
    loadCurrency2();
    function loadCurrency2() {
        jQuery.ajax({
            type: "GET",
            url: "/ChargeCodeDef/Currency",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("STC_CURR");
                        jQuery("#STC_CURR").empty();
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
                        if (options.length > 0)
                            jQuery("#STC_CURR").val("USD");
                    } else {
                        setError(result.msg);
                    }
                } else {
                    Logout();
                }
            }
        })
    }
    loadCurrency3();
    function loadCurrency3() {
        jQuery.ajax({
            type: "GET",
            url: "/ChargeCodeDef/Currency",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("SSM_CUR");
                        jQuery("#SSM_CUR").empty();
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
                        if (options.length > 0)
                            jQuery("#SSM_CUR").val("USD");
                    } else {
                        setError(result.msg);
                    }
                } else {
                    Logout();
                }
            }
        })
    }
    function LoadServiceProvider(value) {
        if (value == "") value = jQuery("#ChargeCode").val();
        jQuery.ajax({
            type: "GET",
            url: "/ChargeCodeDef/LoadServiceProvider",
            data: { type:value },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("SAC_SCV_BY");
                        jQuery("#SAC_SCV_BY").empty();
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
    function LoadClassForAir(value) {
        if (value == "") value = jQuery("#ChargeCode").val();
        jQuery.ajax({
            type: "GET",
            url: "/ChargeCodeDef/LoadClass",
            data: { type: value },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("SAC_CLS");
                        jQuery("#SAC_CLS").empty();
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
    LoadIsChild();
    function LoadIsChild() {
        jQuery.ajax({
            type: "GET",
            url: "/ChargeCodeDef/LoadIsChild",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("SAC_IS_CHILD");
                        jQuery("#SAC_IS_CHILD").empty();
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
                        if (options.length > 0)
                            jQuery("#SAC_IS_CHILD").val("Yes");
                    } else {
                        setError(result.msg);
                    }
                } else {
                    Logout();
                }
            }
        })
   
    }
 
    function LoadServiceByForTravel() {
        jQuery.ajax({
            type: "GET",
            url: "/ChargeCodeDef/LoadServiceByForTravel",
            data: { type: jQuery("#ChargeCode").val() },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("STC_SER_BY");
                        jQuery("#STC_SER_BY").empty();
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
    function LoadClassForTravel () {
        jQuery.ajax({
            type: "GET",
            url: "/ChargeCodeDef/LoadClassForTravel",
            data: { type: jQuery("#ChargeCode").val() },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("STC_CLS");
                        jQuery("#STC_CLS").empty();
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
    function LoadServiceProviderForMsc () {
        jQuery.ajax({
            type: "GET",
            url: "/ChargeCodeDef/LoadServiceProviderForMsc",
            data: { type: jQuery("#ChargeCode").val() },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("SSM_SER_PRO");
                        jQuery("#SSM_SER_PRO").empty();
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
    if (jQuery("#ChargeCode").val() != "AIRTVL") {
        jQuery(".trans-det-pnl #view2").slideUp();
        jQuery(".code-with-air").hide();
    }
    if (jQuery("#ChargeCode").val() != "MSCELNS" && jQuery("#ChargeCode").val() != "OVSLAGMT") {
        jQuery(".trans-det-pnl #view2").slideUp();
        jQuery(".code-with-msc").hide();
    }
    if (jQuery("#ChargeCode").val() != "TRANS") {
        jQuery(".trans-det-pnl #view2").slideUp();
        jQuery(".code-with-travel").hide();
    }
    if (jQuery("#ChargeCode").val() == "") {
        LoadServiceProvider("AIRTVL");
        LoadClassForAir("AIRTVL");
        jQuery(".trans-det-pnl #view2").slideUp();
        jQuery(".code-with-air").show();
        document.getElementById("msc-travel").reset();
        document.getElementById("air-travel").reset();
        document.getElementById("tr-travel").reset();
        jQuery(".btn-cd-save-data").val("Save");
    }
    jQuery("#ChargeCode").change(function () {
        jQuery(".code-with-air").hide();
        jQuery(".code-with-msc").hide();
        jQuery(".code-with-travel").hide();
        if (jQuery(this).val() == "AIRTVL") {
            LoadServiceProvider("AIRTVL");
            LoadClassForAir("AIRTVL");
            jQuery(".code-with-air").show();
            jQuery(".trans-det-pnl #view2").slideDown();
            document.getElementById("msc-travel").reset();
            document.getElementById("air-travel").reset();
            document.getElementById("tr-travel").reset();
           
            jQuery(".airtvl-tab-title").empty();
            jQuery(".airtvl-tab-title").html(jQuery("#ChargeCode option:selected").text());
            jQuery(".btn-cd-save-data").val("Save");
        } else if (jQuery(this).val() == "MSCELNS" || jQuery(this).val() == "OVSLAGMT" || jQuery(this).val() == "HTLRTS") {
            LoadServiceProviderForMsc();
            jQuery(".code-with-msc").show();
            jQuery(".trans-det-pnl #view2").slideDown();
            document.getElementById("msc-travel").reset();
            document.getElementById("air-travel").reset();
            document.getElementById("tr-travel").reset();
            jQuery(".mesc-tab-title").empty();
            jQuery(".mesc-tab-title").html(jQuery("#ChargeCode option:selected").text());

            jQuery(".btn-cd-save-data").val("Save");
            if (jQuery(this).val() == "MSCELNS") {
                jQuery(".other-chrg-mes").show();
            } else {
                jQuery(".other-chrg-mes").hide();
            }

        } else if (jQuery(this).val() == "TRANS") {
            LoadServiceByForTravel();
            LoadClassForTravel();
            jQuery(".code-with-travel").show();
            jQuery(".trans-det-pnl #view2").slideDown();
            document.getElementById("msc-travel").reset();
            document.getElementById("air-travel").reset();
            document.getElementById("tr-travel").reset();
            jQuery(".trvl-tab-title").empty();
            jQuery(".trvl-tab-title").html(jQuery("#ChargeCode option:selected").text());
            jQuery(".btn-cd-save-data").val("Save");
        } else {
            jQuery(".trans-det-pnl #view2").slideUp();
            jQuery(".code-with-air").hide();
        }
    });
    //jQuery("#ChargeCode").change(function () {
    //    if (jQuery(this).val() == "TRANS") {
    //        LoadServiceByForTravel();
    //        LoadClassForTravel();
    //        jQuery(".code-with-travel").show();
    //        jQuery(".trans-det-pnl #view2").slideDown();
    //        document.getElementById("msc-travel").reset();
    //        document.getElementById("air-travel").reset();
    //        document.getElementById("tr-travel").reset();
    //        jQuery(".btn-cd-save-data").val("Save");
    //    } else {
    //        jQuery(".trans-det-pnl #view2").slideUp();
    //        jQuery(".code-with-travel").hide();
    //    }
    //});
    //jQuery("#ChargeCode").change(function () {
    //    if (jQuery(this).val() == "MSCELNS" | jQuery(this).val() == "OVSLAGMT") {
    //        LoadServiceProviderForMsc();
    //        jQuery(".code-with-msc").show();
    //        jQuery(".trans-det-pnl #view2").slideDown();
    //        document.getElementById("msc-travel").reset();
    //        document.getElementById("air-travel").reset();
    //        document.getElementById("tr-travel").reset();
    //        jQuery(".btn-cd-save-data").val("Save");
    //    } else {
    //        jQuery(".trans-det-pnl #view2").slideUp();
    //        jQuery(".code-with-msc").hide();
    //    }
    //});

    //validation
    $('#SAC_RT').focusout(function () {
        var str = $(this).val();
        var numRange = /^\-?([0-9]+(\.[0-9]+)?|Infinity)$/;
        if (!numRange.test(str)) {
            setInfoMsg('Please enter a valid Rate !!!');
            $(this).val('');
        }
    });
    $('#SAC_FRM_DT').focusout(function () {
        var str = $(this).val();
        if ($.datepicker.formatDate('MM dd, yy', new Date(jQuery("#SAC_FRM_DT").val())) == 'undefined NaN, NaN' && jQuery("#SAC_FRM_DT").val() != '') {
            setInfoMsg('Please enter a valid date !!!');
            $(this).val('');
        }
    });
    $('#SAC_TO_DT').focusout(function () {
        var str = $(this).val();
        if ($.datepicker.formatDate('MM dd, yy', new Date(jQuery("#SAC_TO_DT").val())) == 'undefined NaN, NaN' && jQuery("#SAC_TO_DT").val() != '') {
            setInfoMsg('Please enter a valid date !!!');
            $(this).val('');
        }
    });
    $('#SSM_FRM_DT').focusout(function () {
        var str = $(this).val();
        if ($.datepicker.formatDate('MM dd, yy', new Date(jQuery("#SSM_FRM_DT").val())) == 'undefined NaN, NaN' && jQuery("#SSM_FRM_DT").val() != '') {
            setInfoMsg('Please enter a valid date !!!');
            $(this).val('');
        }
    });
    $('#SSM_TO_DT').focusout(function () {
        var str = $(this).val();
        if ($.datepicker.formatDate('MM dd, yy', new Date(jQuery("#SSM_TO_DT").val())) == 'undefined NaN, NaN' && jQuery("#SSM_TO_DT").val() != '') {
            setInfoMsg('Please enter a valid date !!!');
            $(this).val('');
        }
    });
    $('#SSM_RT').focusout(function () {
        var str = $(this).val();
        var numRange = /^\-?([0-9]+(\.[0-9]+)?|Infinity)$/;
        if (!numRange.test(str)) {
            setInfoMsg('Please enter a valid Rate !!!');
            $(this).val('');
        }
    });
    $('#STC_FRM_KM').focusout(function () {
        var str = $(this).val();
        var numRange = /^\-?([0-9]+(\.[0-9]+)?|Infinity)$/;
        if (!numRange.test(str)) {
            setInfoMsg('Please enter a valid Km !!!');
            $(this).val('');
        }
    });
    $('#STC_TO_KM').focusout(function () {
        var str = $(this).val();
        var numRange = /^\-?([0-9]+(\.[0-9]+)?|Infinity)$/;
        if (!numRange.test(str)) {
            setInfoMsg('Please enter a valid Km !!!');
            $(this).val('');
        }
    });
    $('#STC_FRM_DT').focusout(function () {
        var str = $(this).val();
        if ($.datepicker.formatDate('MM dd, yy', new Date(jQuery("#STC_FRM_DT").val())) == 'undefined NaN, NaN' && jQuery("#STC_FRM_DT").val() != '') {
            setInfoMsg('Please enter a valid date !!!');
            $(this).val('');
        }
    });
    $('#STC_TO_DT').focusout(function () {
        var str = $(this).val();
        if ($.datepicker.formatDate('MM dd, yy', new Date(jQuery("#STC_TO_DT").val())) == 'undefined NaN, NaN' && jQuery("#STC_TO_DT").val() != '') {
            setInfoMsg('Please enter a valid date !!!');
            $(this).val('');
        }
    });
    $('#STC_RT').focusout(function () {
        var str = $(this).val();
        var numRange = /^\-?([0-9]+(\.[0-9]+)?|Infinity)$/;
        if (!numRange.test(str)) {
            setInfoMsg('Please enter a valid Rate !!!');
            $(this).val('');
        }
    });
    $('#STC_AD_RT').focusout(function () {
        var str = $(this).val();
        var numRange = /^\-?([0-9]+(\.[0-9]+)?|Infinity)$/;
        if (!numRange.test(str)) {
            setInfoMsg('Please enter a valid Aditional Rate !!!');
            $(this).val('');
        }
    });
    jQuery('.btn-cd-clear-data').click(function (e) {
      
        Lobibox.confirm({
            msg: "Do you want to clear data ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    
                    document.getElementById("msc-travel").reset();
                    document.getElementById("air-travel").reset();
                    document.getElementById("tr-travel").reset();
                    jQuery(".btn-cd-save-data").val("Save");
                 
                }
            }
        });
     
    });

    jQuery('.btn-cd-save-data').click(function (e) {

        var currentdate = new Date();
        var current = $.datepicker.formatDate('dd/mm/yy', new Date(currentdate));
        var frm =  new Date(jQuery("#SAC_FRM_DT").val());
        var to =  new Date(jQuery("#SAC_TO_DT").val());
        var cd = jQuery("#ChargeCode").val();
        var frm1 =  new Date(jQuery("#SSM_FRM_DT").val());
        var to1 = new Date(jQuery("#SSM_TO_DT").val());
        var frm2 = new Date(jQuery("#STC_FRM_DT").val());
        var to2 = new Date(jQuery("#STC_TO_DT").val());
           


                if (cd == "AIRTVL")
                {
                    if (jQuery("#SAC_TO_DT").val() != "" | jQuery("#SAC_FRM_DT").val() != "") 
                    {
                        if (frm >  to) {
                            setInfoMsg("Selected Date Range Wrong!!!");
                        } else {

                            Lobibox.confirm({
                                msg: "Do you want to continue process?",
                                callback: function ($this, type, ev) {
                                    if (type == "yes") {
                                        var formdata = jQuery("#air-travel").serialize();

                                        //  $("#enq-crte-frm").submit();
                                        // setInfoMsg("Submit Complete");
                                        jQuery.ajax({
                                            type: "GET",
                                            url: "/ChargeCodeDef/SaveAirChageCodes?chgcd=" + cd,
                                            data: formdata,
                                            contentType: "application/json;charset=utf-8",
                                            dataType: "json",
                                            success: function (result) {
                                                if (result.login == true) {
                                                    if (result.success == true) {
                                                        setSuccesssMsg(result.msg);
                                                        document.getElementById("air-travel").reset();

                                                        // jQuery('.Pending-Cancel').hide();
                                                    } else {
                                                        if (result.type == "Error") {
                                                            setError(result.msg);

                                                        }
                                                        if (result.type == "Info") {
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
                        }
                    }else{
                        setInfoMsg("Date Required");
                    }
                }

                if (cd == "OVSLAGMT" || cd == "MSCELNS" || cd=="HTLRTS")
                {
                    if (jQuery("#SSM_FRM_DT").val() != "" | jQuery("#SSM_TO_DT").val() != "")
                    {
                        if (frm1 > to1) {
                            setInfoMsg("Selected Date Range Wrong!!!");
                        } else {
                    Lobibox.confirm({
                        msg: "Do you want to continue process?",
                        callback: function ($this, type, ev) {
                            if (type == "yes") {
                                var formdata = jQuery("#msc-travel").serialize();

                                //  $("#enq-crte-frm").submit();
                                // setInfoMsg("Submit Complete");
                                jQuery.ajax({
                                    type: "GET",
                                    url: "/ChargeCodeDef/SaveMSCData?chgcd=" + cd,
                                    data: formdata,
                                    contentType: "application/json;charset=utf-8",
                                    dataType: "json",
                                    success: function (result) {
                                        if (result.login == true) {
                                            if (result.success == true) {
                                                setSuccesssMsg(result.msg);
                                                document.getElementById("msc-travel").reset();

                                                // jQuery('.Pending-Cancel').hide();
                                            } else {
                                                if (result.type == "Error") {
                                                    setError(result.msg);

                                                }
                                                if (result.type == "Info") {
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
                        }
                    } else {
                        setInfoMsg("Date Required");
                    }
                }
                if (cd == "TRANS") {
                    if (jQuery("#STC_TO_DT").val() != "" | jQuery("#STC_FRM_DT").val() != "")
                    {
                        if (frm2 >  to2) {
                            setInfoMsg("Selected Date Range Wrong!!!");
                        } else
                        {
                            Lobibox.confirm({
                                msg: "Do you want to continue process?",
                                callback: function ($this, type, ev) {
                                    if (type == "yes") {
                                        var formdata = jQuery("#tr-travel").serialize();

                                        //  $("#enq-crte-frm").submit();
                                        // setInfoMsg("Submit Complete");
                                        jQuery.ajax({
                                            type: "GET",
                                            url: "/ChargeCodeDef/SaveTravelChageCodes?chgcd=" + cd,
                                            data: formdata,
                                            contentType: "application/json;charset=utf-8",
                                            dataType: "json",
                                            success: function (result) {
                                                if (result.login == true) {
                                                    if (result.success == true) {
                                                        setSuccesssMsg(result.msg);
                                                        document.getElementById("tr-travel").reset();

                                                        // jQuery('.Pending-Cancel').hide();
                                                    } else {
                                                        if (result.type == "Error") {
                                                            setError(result.msg);

                                                        }
                                                        if (result.type == "Info") {
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
                        }
                    } else
                    {
                        setInfoMsg("Date Required");
                    }
                   
                }

            

       
        //document.getElementById("driverall-frm").submit();

    });
    jQuery('.imprt-cd-data').click(function (e) {
        var cd = jQuery("#ChargeCode").val();
        if (jQuery('#UploadedFile').val() == "") {
            setInfoMsg("Please Sellect File");
        } else {
            Lobibox.confirm({
                msg: "Do you want to continue process?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        $("#imp-data").submit();

                    }
                }

            });
        }


    });
    //check numbers and decimal  only
    jQuery('#SSM_TAX_RT,#STC_TAX_RT,#SAC_TAX_RT').on("input", function (event) {
        if (!jQuery.isNumeric(this.value)) {
            this.value = "";
        }
        if (parseFloat(this.value) < 0) {
            this.value = "";
        }

    });
    jQuery('#SSM_TAX_RT,#STC_TAX_RT,#SAC_TAX_RT').keypress(function (event) {
        if ((event.which != 46 || $(this).val().indexOf('.') != -1) &&
          ((event.which < 48 || event.which > 57) &&
            (event.which != 0 && event.which != 8))) {
            event.preventDefault();
        }

        var text = $(this).val();

        if ((text.indexOf('.') != -1) &&
          (text.substring(text.indexOf('.')).length > 2) &&
          (event.which != 0 && event.which != 8) &&
          ($(this)[0].selectionStart >= text.length - 2)) {
            event.preventDefault();
        }
    });
    jQuery(".paret-code-search").click(function () {
        if (jQuery("#ChargeCode").val() == "MSCELNS") {
                var headerKeys = Array()
                headerKeys = ["Row", "Code", "Description", "Service By", "From Date", "To Date", "From", "To", "Rate", "Class", "Vehicle"];
                field = "parentSrchTrans"
                var x = new CommonSearch(headerKeys, field, "TRANS");
            }
    });
    jQuery("#SSM_PARENT_CD").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            if (jQuery("#ChargeCode").val() == "MSCELNS") {
                var headerKeys = Array()
                headerKeys = ["Row", "Code", "Description", "Service By", "From Date", "To Date", "From", "To", "Rate", "Class", "Vehicle"];
                field = "parentSrchTrans"
                var x = new CommonSearch(headerKeys, field, "TRANS");
            }
        } else if (evt.keyCode == 13) {
            if (jQuery(this).val() != "") {
                validateTravelChgCd(jQuery(this).val());
            }
        }
    });

    jQuery("#SSM_PARENT_CD").focusout(function () {
        if (jQuery(this).val() != "") {
            validateTravelChgCd(jQuery(this).val());
        }
    });
    function validateTravelChgCd(code) {
        if (code != "") {
            jQuery.ajax({
                type: "GET",
                url: "/ChargeCodeDef/getTrnsChargeCodedet",
                contentType: "application/json;charset=utf-8",
                data: { val: code, chgcode: "TRANS" },
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.data == "") {
                                setInfoMsg("Invalid charg code.");
                                jQuery("#SSM_PARENT_CD").val("");
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