jQuery(document).ready(function () {

    jQuery('#MSTF_REG_EXP').datepicker({ dateFormat: "dd/M/yy" })
    jQuery('#MSTF_INSU_EXP').datepicker({ dateFormat: "dd/M/yy" })
    jQuery('#MSTF_DT').datepicker({ dateFormat: "dd/M/yy" })
    jQuery('#MFD_FRM_DT').datepicker({ dateFormat: "dd/M/yy" })
    jQuery('#MFD_TO_DT').datepicker({ dateFormat: "dd/M/yy" })
    jQuery('#MSTF_FROM_DT').datepicker({ dateFormat: "dd/M/yy" })
    jQuery('#MSTF_TO_DT').datepicker({ dateFormat: "dd/M/yy" })
    jQuery('#fdate').datepicker({ dateFormat: "dd/M/yy" })
    jQuery('#tdate').datepicker({ dateFormat: "dd/M/yy" })
    function loadfleetStatus() {
        jQuery.ajax({
            type: "GET",
            url: "/FleetDefinition/LoadStatus",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        var select = document.getElementById("MSTF_ACT");
                        jQuery("#MSTF_ACT").empty();
                        var options = [];
                        var option = document.createElement('option');
                        if (result.data != null && result.data.length != 0) {
                            for (var i = 0; i < result.data.length; i++) {
                                option.text = result.data[i].Text;
                                option.value = result.data[i].Value;
                                options.push(option.outerHTML);
                            }
                        } else {
                            option.text = "Select Status";
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
    if (jQuery(".frm-fleet-det #MSTF_ACT").length > 0)
    {
        loadfleetStatus();
    }
    function loadFleetType() {
        jQuery.ajax({
            type: "GET",
            url: "/FleetDefinition/LoadVehType",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        var select = document.getElementById("MSTF_VEH_TP");
                        jQuery("#MSTF_VEH_TP").empty();
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
    if (jQuery(".frm-fleet-det #MSTF_VEH_TP").length > 0) {
        loadFleetType();
    }
    function loadResons() {
        jQuery.ajax({
            type: "GET",
            url: "/FleetDefinition/LoadResons",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        var select = document.getElementById("MSTF_REASON");
                        jQuery("#MSTF_REASON").empty();
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
    loadResons();
    function loadFleetFuelType() {
        jQuery.ajax({
            type: "GET",
            url: "/FleetDefinition/LoadFuelType",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        var select = document.getElementById("MSTF_FUAL_TP");
                        jQuery("#MSTF_FUAL_TP").empty();
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
    if (jQuery(".frm-fleet-det #MSTF_FUAL_TP").length > 0) {
        loadFleetFuelType();
        
    }
    function loadSource() {
        jQuery.ajax({
            type: "GET",
            url: "/FleetDefinition/LoadSource",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        var select = document.getElementById("MSTF_OWN");
                        jQuery("#MSTF_OWN").empty();
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
    if (jQuery(".frm-fleet-det #MSTF_OWN").length > 0) {
        loadSource();
    }
    $('#MSTF_OWN_EMAIL').focusout(function () {
        var str = $(this).val();
        var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
        if (!emailReg.test(str)) {
            setInfoMsg('Please enter a valid email address !!!');
            $(this).val('');
        }
    });

    $('#MSTF_OWN_CONT').focusout(function () {
        var str = $(this).val();
        var numRange = /^[0-9-+]+$/;
        if (!numRange.test(str)) {
            setInfoMsg('Please enter a valid Contact !!!');
            $(this).val('');
        }
    });

    $('#MSTF_INSU_EXP').focusout(function () {
        var str = $(this).val();
        if ($.datepicker.formatDate('MM dd, yy', new Date(jQuery("#MSTF_INSU_EXP").val())) == 'undefined NaN, NaN' && jQuery("#MSTF_INSU_EXP").val() != '') {
            setInfoMsg('Please enter a valid date !!!');
            $(this).val('');
        }
    });
    $('#MSTF_REG_EXP').focusout(function () {
        var str = $(this).val();
        if ($.datepicker.formatDate('MM dd, yy', new Date(jQuery("#MSTF_REG_EXP").val())) == 'undefined NaN, NaN' && jQuery("#MSTF_REG_EXP").val() != '') {
            setInfoMsg('Please enter a valid date !!!');
            $(this).val('');
        }
    });
    $('#MSTF_DT').focusout(function () {
        var str = $(this).val();
        if ($.datepicker.formatDate('MM dd, yy', new Date(jQuery("#MSTF_DT").val())) == 'undefined NaN, NaN' && jQuery("#MSTF_DT").val() != '') {
            setInfoMsg('Please enter a valid date !!!');
            $(this).val('');
        }
    });
    $('#MFD_FRM_DT').focusout(function () {
        var str = $(this).val();
        if ($.datepicker.formatDate('MM dd, yy', new Date(jQuery("#MFD_FRM_DT").val())) == 'undefined NaN, NaN' && jQuery("#MFD_FRM_DT").val() != '') {
            setInfoMsg('Please enter a valid date !!!');
            $(this).val('');
        }
    });
    $('#MFD_TO_DT').focusout(function () {
        var str = $(this).val();
        if ($.datepicker.formatDate('MM dd, yy', new Date(jQuery("#MFD_TO_DT").val())) == 'undefined NaN, NaN' && jQuery("#MFD_TO_DT").val() != '') {
            setInfoMsg('Please enter a valid date !!!');
            $(this).val('');
        }
    });
    $('#MSTF_TO_DT').focusout(function () {
        var str = $(this).val();
        if ($.datepicker.formatDate('MM dd, yy', new Date(jQuery("#MSTF_TO_DT").val())) == 'undefined NaN, NaN' && jQuery("#MSTF_TO_DT").val() != '') {
            setInfoMsg('Please enter a valid date !!!');
            $(this).val('');
        }
    });
    $('#MSTF_FROM_DT').focusout(function () {
        var str = $(this).val();
        if ($.datepicker.formatDate('MM dd, yy', new Date(jQuery("#MSTF_FROM_DT").val())) == 'undefined NaN, NaN' && jQuery("#MSTF_FROM_DT").val() != '') {
            setInfoMsg('Please enter a valid date !!!');
            $(this).val('');
        }
    });
    $('#MSTF_NOOF_SEAT').focusout(function () {
        var str = $(this).val();
        var numRange = /^[0-9+]+$/;
        if (!numRange.test(str)) {
            setInfoMsg('Please enter a valid No Of Seats !!!');
            $(this).val('');
        }
    });
    $('#MSTF_LST_SERMET').focusout(function () {
        var str = $(this).val();
        var numRange = /^[0-9+]+$/;
        if (!numRange.test(str)) {
            setInfoMsg('Please enter a valid Service Meter !!!');
            $(this).val('');
        }
    });
    $('#MSTF_ST_METER').focusout(function () {
        var str = $(this).val();
        var numRange = /^[0-9+]+$/;
        if (!numRange.test(str)) {
            setInfoMsg('Please enter a valid Start Meter !!!');
            $(this).val('');
        }
    });
});

      