jQuery(document).ready(function () {

    jQuery(".btn-flight").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description"];
        field = "VESSEL1"
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery(".cst-ele-srch").click(function () {
        //evt.preventDefault();
        var headerKeys = Array()
        headerKeys = ["Row", "Cost Code", "Description", "Account Code"];
        field = "costele1";
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery(".btn-load-port").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description"];
        field = "PORTSREF"
        var x = new CommonSearch(headerKeys, field);
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

        jQuery("#VM_VESSAL_CD").val("");
        jQuery("#VM_VESSAL_NAME").val("");
        jQuery("#MCE_CD").val("");
        jQuery("#MCE_DESC").val("");
        jQuery("#PA_PRT_CD").val("");
        jQuery("#PA_PRT_NAME").val("");
        jQuery("#MCE_ACC_CD").val("");
        jQuery("#PA_ACT").val("2");
        jQuery("#VM_ACT").val("2");
        jQuery("#MCE_ACT").val("2");
        jQuery("#MCE_COST_REV_STS").val("0");
        //loadPendingRequest();
        //clearSession();
    }
    //jQuery("#VM_VESSAL_CD").on("keydown", function (evt) {
    //    if (evt.keyCode == 113) {
    //        var headerKeys = Array()
    //        headerKeys = ["Row", "Code", "Description"];
    //        field = "VESSEL1"
    //        var x = new CommonSearch(headerKeys, field);
    //    }
    //});

    function setFieldValue(data) {
        if (typeof data != "undefined") {
            jQuery("#VM_VESSAL_CD").val(data[0].VM_VESSAL_CD);
            jQuery("#VM_VESSAL_NAME").val(data[0].VM_VESSAL_NAME);
            jQuery("#VM_ACT").val(data[0].VM_ACT);

        }
    }

    function setFieldValuecst(data) {
        if (typeof data != "undefined") {
            jQuery("#MCE_CD").val(data[0].MCE_CD);
            jQuery("#MCE_DESC").val(data[0].MCE_DESC);
            jQuery("#MCE_ACT").val(data[0].MCE_ACT);
            jQuery("#MCE_ACC_CD").val(data[0].MCE_ACC_CD);
            jQuery("#MCE_COST_REV_STS").val(data[0].MCE_COST_REV_STS);
        }
    }

    function setFieldValueport(data) {
        if (typeof data != "undefined") {
            jQuery("#PA_PRT_CD").val(data[0].PA_PRT_CD);
            jQuery("#PA_PRT_NAME").val(data[0].PA_PRT_NAME);
            jQuery("#PA_ACT").val(data[0].PA_ACT);
        }
    }

    jQuery("#VM_VESSAL_CD").focusout(function () {
        var code = jQuery(this).val();
        //FlightValidate(code);
        vslDetLoad();
    });

    jQuery("#MCE_CD").focusout(function () {
        var code = jQuery(this).val();
        //CostValidate(code);
        cstDetLoad();
    });

    jQuery("#PA_PRT_CD").focusout(function () {
        var code = jQuery(this).val();
        //PortValidate(code);
        prtDetLoad();
    });

    function FlightValidate(code) {
        if (code != "") {
            if (code != "") {
                jQuery.ajax({
                    type: "GET",
                    url: "/Validation/ValidayeVessels?searchVal=" + code,
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        if (result.login == false) {
                            Logout();
                        }
                        if (result.success == false) {
                            setInfoMsg(result.msg);
                            jQuery("#VM_VESSAL_CD").val("");
                            jQuery("#VM_VESSAL_CD").focus();
                        }
                    }
                });
            }
        }
    }

    function CostValidate(code) {
        if (code != "") {
            if (code != "") {
                jQuery.ajax({
                    type: "GET",
                    url: "/Validation/validateCostElement?eleCode=" + code,
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        if (result.login == false) {
                            Logout();
                        }
                        if (result.success == false) {
                            setInfoMsg(result.msg);
                            jQuery("#MCE_CD").val("");
                            jQuery("#MCE_CD").focus();
                        }
                    }
                });
            }
        }
    }

    function PortValidate(code) {
        if (code != "") {
            if (code != "") {
                jQuery.ajax({
                    type: "GET",
                    url: "/Validation/ValidatePort?searchVal=" + code,
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        if (result.login == false) {
                            Logout();
                        }
                        if (result.success == false) {
                            setInfoMsg(result.msg);
                            jQuery("#PA_PRT_CD").val("");
                            jQuery("#PA_PRT_CD").focus();
                        }
                    }
                });
            }
        }
    }

    function vslDetLoad() {
        if (jQuery("#VM_VESSAL_CD").val() != "") {
            var val = jQuery("#VM_VESSAL_CD").val();
            jQuery.ajax({
                type: "GET",
                url: "/ReferenceDetails/LoadVessalDetails",
                contentType: "application/json;charset=utf-8",
                data: { val: val },
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.data != "") {
                                //clearForm();
                                setFieldValue(result.data);
                                //disabledField();
                                jQuery(".btn-emp-update-data").val("Update_vsl");
                            } else {
                                //clearForm();
                                jQuery("#VM_VESSAL_CD").val(val);
                                jQuery(".btn-emp-save-data").val("Create_vsl");
                            }
                        }
                    } else {
                        Logout();
                    }
                }
            });
        }
    }

    function cstDetLoad() {
        if (jQuery("#MCE_CD").val() != "") {
            var val = jQuery("#MCE_CD").val();
            jQuery.ajax({
                type: "GET",
                url: "/ReferenceDetails/LoadCostDetails",
                contentType: "application/json;charset=utf-8",
                data: { val: val },
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.data != "") {
                                //clearForm();
                                setFieldValuecst(result.data);
                                //disabledField();
                                jQuery(".btn-emp-update-data").val("Update_cst");
                            } else {
                                //clearForm();
                                jQuery("#MCE_CD").val(val);
                                jQuery(".btn-emp-save-data").val("Create_cst");
                            }
                        }
                    } else {
                        Logout();
                    }
                }
            });
        }
    }

    function prtDetLoad() {
        if (jQuery("#PA_PRT_CD").val() != "") {
            var val = jQuery("#PA_PRT_CD").val();
            jQuery.ajax({
                type: "GET",
                url: "/ReferenceDetails/LoadPortDetails",
                contentType: "application/json;charset=utf-8",
                data: { val: val },
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.data != "") {
                                //clearForm();
                                setFieldValueport(result.data);
                                //disabledField();
                                jQuery(".btn-emp-update-data").val("Update_prt");
                            } else {
                                //clearForm();
                                jQuery("#PA_PRT_CD").val(val);
                                jQuery(".btn-emp-save-data").val("Create_prt");
                            }
                        }
                    } else {
                        Logout();
                    }
                }
            });
        }
    }

    jQuery(".btn-emp-save-data").click(function (event) {
        //jQuery(this).attr("disabled", true);
        event.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to continue ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    if (jQuery(".btn-emp-save-data").val() == "Create_vsl") {
                        var formdata = jQuery("#emply-crte-frm").serialize();
                        jQuery.ajax({
                            type: 'POST',
                            url: '/ReferenceDetails/vesselCreation',
                            data: formdata,
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
                                    //jQuery(".btn-emp-save-data").removeAttr("disabled");
                                } else {
                                    Logout();
                                }
                            }
                        });
                        return false;
                    }
                    else if(jQuery(".btn-emp-save-data").val() == "Create_cst")
                    {
                        var formdata = jQuery("#emply-crte-frm").serialize();
                        jQuery.ajax({
                            type: 'POST',
                            url: '/ReferenceDetails/CosteleCreation',
                            data: formdata,
                            success: function (response) {
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
                                //jQuery(".btn-emp-save-data").removeAttr("disabled");
                            }
                        });
                        return false;
                    }
                    else if (jQuery(".btn-emp-save-data").val() == "Create_prt")
                    {
                        var formdata = jQuery("#emply-crte-frm").serialize();
                        jQuery.ajax({
                            type: 'POST',
                            url: '/ReferenceDetails/PortCreation',
                            data: formdata,
                            success: function (response) {
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
                                //jQuery(".btn-emp-save-data").removeAttr("disabled");
                            }
                        });
                        return false;
                    }
                    else
                    {
                        setInfoMsg("Please Use Update Option");
                    }
                }
            }
        });

    });

    jQuery(".btn-emp-update-data").click(function (event) {
        //jQuery(this).attr("disabled", true);
        event.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to Update?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    if (jQuery(".btn-emp-update-data").val() == "Update_vsl") {
                        var formdata = jQuery("#emply-crte-frm").serialize();
                        jQuery.ajax({
                            type: 'POST',
                            url: '/ReferenceDetails/vesselUpdate',
                            data: formdata,
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
                                    //jQuery(".btn-emp-update-data").removeAttr("disabled");
                                } else {
                                    Logout();
                                }
                            }
                        });
                        return false;
                    }
                    else if (jQuery(".btn-emp-update-data").val() == "Update_cst")
                    {
                        var formdata = jQuery("#emply-crte-frm").serialize();
                        jQuery.ajax({
                            type: 'POST',
                            url: '/ReferenceDetails/CosteleUpdate',
                            data: formdata,
                            success: function (response) {
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
                                //jQuery(".btn-emp-update-data").removeAttr("disabled");
                            }
                        });
                        return false;
                    }
                    else if (jQuery(".btn-emp-update-data").val() == "Update_prt")
                    {
                        var formdata = jQuery("#emply-crte-frm").serialize();
                        jQuery.ajax({
                            type: 'POST',
                            url: '/ReferenceDetails/PortUpdate',
                            data: formdata,
                            success: function (response) {
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
                                //jQuery(".btn-emp-update-data").removeAttr("disabled");
                            }
                        });
                        return false;
                    }
                    else {
                        setInfoMsg("Please Use Save Option");
                    }
                    
                }
            }
        });

    });
});