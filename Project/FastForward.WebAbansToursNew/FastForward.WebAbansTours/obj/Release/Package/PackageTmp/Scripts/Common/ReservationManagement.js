jQuery(document).ready(function () {
    loadEnquiryData();
    clearForm();
    loadCusType();
    jQuery('#GCE_FLY_DATE').focusout(function () {
        var str = jQuery(this).val();
        if (jQuery.datepicker.formatDate('MM dd, yy', new Date(jQuery("#GCE_FLY_DATE").val())) == 'undefined NaN, NaN' && jQuery("#GCE_FLY_DATE").val() != '') {
            setInfoMsg('Please enter a valid date !!!');
            jQuery(this).val('');
        }
    });
    jQuery('#fromdt').datepicker({ dateFormat: "dd/M/yy" });
    jQuery('#todt').datepicker({ dateFormat: "dd/M/yy" });
    jQuery('#vehifromdt').datepicker({ dateFormat: "dd/M/yy" });
    jQuery('#vehitodt').datepicker({ dateFormat: "dd/M/yy" });
    jQuery("#fromdt,#todt,#vehitodt,#vehifromdt").focusout(function () {
        if (ValidDate(jQuery(this).val()) == "false" && jQuery(this).val() != "") {
            setInfoMsg("Please enter valid date.");
            jQuery(this).val("");
        }
    });
    loadingVehicleTypes();
    function loadingVehicleTypes() {
        jQuery.ajax({
            type: "GET",
            url: "/ReservationManagement/getVehicleTypes",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("GCE_VEH_TP");
                        jQuery("#GCE_VEH_TP").empty();
                        var options = [];
                        var option = document.createElement('option');
                        if (result.data != null && result.data.length != 0) {
                            for (i = 0; i < result.data.length; i++) {
                                option.text = result.data[i].SVT_DESC;
                                option.value = result.data[i].SVT_CD;
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
    function loadEnquiryData() {
        jQuery.ajax({
            type: "GET",
            url: "/ReservationManagement/loadTransportEnqData",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        jQuery('.pending-trans-enqui .new-row').remove();
                        if (result.data.length > 0) {
                            for (i = 0; i < result.data.length; i++) {
                                jQuery('.pending-trans-enqui #enq-all-tbl').append('<tr class="new-row">' +
                                        '<td class="select-enq-data">' + 'Select' + '</td>' +
                                        '<td>' + result.data[i].GCE_CUS_CD + '</td>' +
                                        '<td>' + result.data[i].GCE_ENQ_ID + '</td>' +
                                        '<td>' + result.data[i].GCE_REF + '</td>' +
                                        '<td>' + getFormatedDate(result.data[i].GCE_EXPECT_DT) + '</td>' +
                                        '</tr>');
                            }
                            //loadPagin();
                            $("#enq-all-tbl").pageMe({ pagerSelector: '#myPager', showPrevNext: true, hidePageNumbers: false, perPage: 3 });
                            jQuery(".pending-trans-enqui .select-enq-data").unbind('click').click(function (evt) {
                                var enqId = jQuery(this).closest("tr").find('td:eq(2)').text();
                                jQuery(".btn-save-trans-data").val("Update");
                                jQuery(".btn-cancel-trans-data").show();
                                jQuery(".btn-print-invo-data").show();
                                loadEnqData(enqId)

                            });
                        }
                        else {
                            jQuery(".btn-cancel-trans-data").show();
                            jQuery(".btn-print-invo-data").show();
                        }
                    } else {
                        if (result.type == "Error") {
                            setError(result.msg);
                        } else if (result.type == "Info") {
                            setInfoMsg(result.msg);
                        }
                        jQuery(".btn-cancel-trans-data").hide();
                        jQuery(".btn-print-invo-data").hide();
                    }
                } else {
                    Logout();
                }
            }
        });
    }
    loadingPkgTypes();
    function loadingPkgTypes() {
        jQuery.ajax({
            type: "GET",
            url: "/ReservationManagement/getPkgTypes",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("ChargType");
                        jQuery("#ChargType").empty();
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
    function getFormatedDate(date) {
        var dte = new Date(parseInt(date.substr(6)));
        if (my_date_format_tran(dte) != "NaN/undefined/NaN")
            return my_date_format_tran(dte);
    }
    jQuery("#GCE_CUS_CD").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Name", "Nic", "Mobile", "Br No"];
            field = "cusCodeForTraEnq"
            var x = new CommonSearch(headerKeys, field);
        }
        if (evt.keyCode == 13) {
            codeFocusOut(jQuery(this).val());
        }
    });
    jQuery(".cus-cd-trans-enq-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Name", "Nic", "Mobile", "Br No"];
        field = "cusCodeForTraEnq"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery("#GCE_CUS_CD").focusout(function () {
        codeFocusOut(jQuery(this).val());
    });
    jQuery("#GCE_PP_NO").focusout(function () {
        passportNumberLoad(jQuery(this).val());
    });
    jQuery("#GCE_MOB").focusout(function () {
        mobileNumberLoad(jQuery(this).val());
    });
    function mobileNumberLoad(mobile) {
        if (mobile != "") {
            jQuery.ajax({
                type: "GET",
                url: "/ReservationManagement/mobileTextChanged",
                data: { mobile: mobile },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (typeof (result.local) != "undefined") {
                                jQuery("#GCE_NIC").val(result.data.Mbe_nic);
                                jQuery("#GCE_CUS_CD").val(result.data.Mbe_cd);
                                jQuery("#GCE_MOB").val(result.data.Mbe_mob);
                                jQuery("#GCE_NAME").val(result.data.Mbe_name);
                                jQuery("#GCE_ADD1").val(result.data.Mbe_add1);
                                jQuery("#GCE_ADD2").val(result.data.Mbe_add2);
                                jQuery("#GCE_PP_NO").val(result.data.Mbe_pp_no);
                                jQuery("#GCE_CUS_TYPE").val(result.data.Mbe_cate);
                            }
                            if (typeof (result.group) != "undefined") {
                                jQuery("#GCE_NIC").val(result.data.Mbg_nic);
                                jQuery("#GCE_CUS_CD").val(result.data.Mbg_cd);
                                jQuery("#GCE_MOB").val(result.data.Mbg_mob);
                                jQuery("#GCE_NAME").val(result.data.Mbg_name);
                                jQuery("#GCE_ADD1").val(result.data.Mbg_add1);
                                jQuery("#GCE_ADD2").val(result.data.Mbg_add2);
                                jQuery("#GCE_PP_NO").val(result.data.Mbg_pp_no);
                                jQuery("#GCE_CUS_TYPE").val(result.data.Mbg_cate);
                            }
                        }

                    } else {
                        Logout();
                    }

                }

            });
        }
    }
    function passportNumberLoad(ppNo) {
        if (ppNo != "") {
            jQuery.ajax({
                type: "GET",
                url: "/ReservationManagement/ppNoTextChanged",
                data: { ppNo: ppNo },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (typeof (result.local) != "undefined") {
                                jQuery("#GCE_NIC").val(result.data.Mbe_nic);
                                jQuery("#GCE_CUS_CD").val(result.data.Mbe_cd);
                                jQuery("#GCE_MOB").val(result.data.Mbe_mob);
                                jQuery("#GCE_NAME").val(result.data.Mbe_name);
                                jQuery("#GCE_ADD1").val(result.data.Mbe_add1);
                                jQuery("#GCE_ADD2").val(result.data.Mbe_add2);
                                jQuery("#GCE_PP_NO").val(result.data.Mbe_pp_no);
                                jQuery("#GCE_CUS_TYPE").val(result.data.Mbe_cate);
                            }
                            if (typeof (result.group) != "undefined") {
                                jQuery("#GCE_NIC").val(result.data.Mbg_nic);
                                jQuery("#GCE_CUS_CD").val(result.data.Mbg_cd);
                                jQuery("#GCE_MOB").val(result.data.Mbg_mob);
                                jQuery("#GCE_NAME").val(result.data.Mbg_name);
                                jQuery("#GCE_ADD1").val(result.data.Mbg_add1);
                                jQuery("#GCE_ADD2").val(result.data.Mbg_add2);
                                jQuery("#GCE_PP_NO").val(result.data.Mbg_pp_no);
                                jQuery("#GCE_CUS_TYPE").val(result.data.Mbg_cate);
                            }
                        }

                    } else {
                        Logout();
                    }

                }

            });
        }
    }

    function codeFocusOut(code) {
        if (code != "") {
            jQuery.ajax({
                type: "GET",
                url: "/ReservationManagement/cusCodeTextChanged",
                data: { cusCd: code },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.success == true) {
                                if (typeof (result.local) != "undefined") {
                                    jQuery("#GCE_NIC").val(result.data.Mbe_nic);
                                    jQuery("#GCE_CUS_CD").val(result.data.Mbe_cd);
                                    jQuery("#GCE_MOB").val(result.data.Mbe_mob);
                                    jQuery("#GCE_NAME").val(result.data.Mbe_name);
                                    jQuery("#GCE_ADD1").val(result.data.Mbe_add1);
                                    jQuery("#GCE_ADD2").val(result.data.Mbe_add2);
                                    jQuery("#GCE_PP_NO").val(result.data.Mbe_pp_no);
                                    jQuery("#GCE_CUS_TYPE").val(result.data.Mbe_cate);
                                }
                                if (typeof (result.group) != "undefined") {
                                    jQuery("#GCE_NIC").val(result.data.Mbg_nic);
                                    jQuery("#GCE_CUS_CD").val(result.data.Mbg_cd);
                                    jQuery("#GCE_MOB").val(result.data.Mbg_mob);
                                    jQuery("#GCE_NAME").val(result.data.Mbg_name);
                                    jQuery("#GCE_ADD1").val(result.data.Mbg_add1);
                                    jQuery("#GCE_ADD2").val(result.data.Mbg_add2);
                                    jQuery("#GCE_PP_NO").val(result.data.Mbg_pp_no);
                                    jQuery("#GCE_CUS_TYPE").val(result.data.Mbg_cate);
                                }
                            } else {
                                jQuery("#GCE_NIC").val("");
                                jQuery("#GCE_CUS_CD").val("");
                                jQuery("#GCE_MOB").val("");
                                jQuery("#GCE_NAME").val("");
                                jQuery("#GCE_ADD1").val("");
                                jQuery("#GCE_ADD2").val("");
                                jQuery("#GCE_PP_NO").val("");
                                jQuery("#GCE_CUS_TYPE").val("");
                                if (result.type == "Info") {
                                    setInfoMsg(result.msg);
                                }
                                if (result.type == "Error") {
                                    setInfoMsg(result.msg);
                                }
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
    function setFieldValueCustomer(data) {
        if (data != "") {
            jQuery("#GCE_MOB").val(data.Mbe_mob);
            jQuery("#GCE_NIC").val(data.Mbe_nic);
            //var title = data.Mbe_name.substr(0, data.Mbe_name.indexOf('.'));
            //var name = data.Mbe_name.substr(data.Mbe_name.indexOf(' ') + 1);
            jQuery("#GCE_NAME").val(data.Mbe_name);
            jQuery("#GCE_ADD1").val(data.Mbe_add1);
            jQuery("#GCE_ADD2").val(data.Mbe_add2);
            jQuery("#GCE_PP_NO").val(data.Mbe_pp_no);
            jQuery("#GCE_CUS_TYPE").val(result.data.Mbe_cate);
        } else {
            jQuery("#GCE_MOB").val("");
            jQuery("#GCE_NIC").val("");
            jQuery("#GCE_NAME").val("");
            jQuery("#GCE_ADD1").val("");
            jQuery("#GCE_ADD2").val("");
            jQuery("#GCE_PP_NO").val("");
            jQuery("#GCE_CUS_TYPE").val("");
        }
    }
    function loadCusType() {
        jQuery.ajax({
            type: "GET",
            url: "/ReservationManagement/LoadCustomerType",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        var select = document.getElementById("GCE_CUS_TYPE");
                        jQuery("#GCE_CUS_TYPE").empty();
                        var options = [];
                        var option = document.createElement('option');
                        option.text = "-Select-"
                        option.value = "";
                        options.push(option.outerHTML);
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
    function setError(msg) {
        Lobibox.alert('error',
        {
            msg: msg
        });
    }
    function setSuccesssMsg(msg) {
        Lobibox.alert('success',
        {
            msg: msg
        });
    }
    function setInfoMsg(msg) {
        Lobibox.alert('info',
         {
             msg: msg
         });
    }
    var my_date_format_tran = function (input) {
        var monthNames = [
          "Jan", "Feb", "Mar",
          "Apr", "May", "Jun", "Jul",
          "Aug", "Sep", "Oct",
          "Nov", "Dec"
        ];

        var date = new Date(Date.parse(input));;
        var day = date.getDate();
        var monthIndex = date.getMonth();
        var year = date.getFullYear();

        return (day + "/" + monthNames[monthIndex] + "/" + year);
    };


    jQuery("#GCE_ENQ_ID").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Enquiry Id", "Ref Num", "Customer Code", "Name", "Address"];
            field = "transportEnq"
            var x = new CommonSearch(headerKeys, field);
        }
        if (evt.keyCode == 13) {
            //codeFocusOut(jQuery(this).val());
        }
    });
    jQuery(".trans-enq-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Enquiry Id", "Ref Num", "Customer Code", "Name", "Address"];
        field = "transportEnq"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery('#GCE_EXPECT_DT').datetimepicker({ dateFormat: "dd/M/yy", timeFormat: "HH:mm" });//{ minDate: new Date(), dateFormat: "dd/M/yy", timeFormat: "hh:mm tt" }
    jQuery('#GCE_RET_DT').datetimepicker({ dateFormat: "dd/M/yy", timeFormat: "HH:mm" });//{ minDate: new Date(), dateFormat: "dd/M/yy", timeFormat: "hh:mm tt" }
    jQuery('#GCE_FLY_DATE').datetimepicker({ minDate: new Date(), dateFormat: "dd/M/yy", timeFormat: "HH:mm" });
    jQuery('#GCE_EXPECT_DT').val(my_date_format_with_time(new Date()));
    jQuery('#GCE_RET_DT').val(my_date_format_with_time(new Date()));
    jQuery('#GCE_FLY_DATE').val(my_date_format_with_time(new Date()));
    jQuery("#drivercd").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Epf No", "First Name", "Last Name", "MOBILE", "NIC"];
            field = "driverSrch"
            var x = new CommonSearch(headerKeys, field);
        }
        if (evt.keyCode == 13) {
            driverCodeLoad(jQuery(this).val());
        }
    });
    jQuery(".trans-driver-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Epf No", "First Name", "Last Name", "MOBILE", "NIC"];
        field = "driverSrch"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery("#drivercd").focusout(function () {
        driverCodeLoad(jQuery(this).val());
    });
    function driverCodeLoad(driver) {
        if (jQuery("#MEMP_CD").val() != "") {
            var val = driver;
            jQuery.ajax({
                type: "GET",
                url: "/ReservationManagement/getEmployeeDetails",
                contentType: "application/json;charset=utf-8",
                data: { val: val },
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (typeof result.data != "undefined") {
                                jQuery("#drivername").val(result.data.MEMP_FIRST_NAME);
                                jQuery("#drivercontact").val(result.data.MEMP_MOBI_NO);
                            } else {
                                jQuery("#drivercd").val("");
                                jQuery("#drivername").val("");
                                jQuery("#drivercontact").val("");
                                if (result.type == "Error") {
                                    setError(result.msg);
                                } else if (result.type == "Info") {
                                    setInfoMsg(result.msg);
                                }
                            }
                        }
                    } else {
                        Logout();
                    }
                }
            });
        }
    }

    jQuery(".trans-vehicle-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Reg No", "Brand", "Model", "Vehicle Type", "Owner", "Owner Contact"];
        field = "transportFleet"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery("#vehiregno").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Reg No", "Brand", "Model", "Vehicle Type", "Owner", "Owner Contact"];
            field = "transportFleet"
            var x = new CommonSearch(headerKeys, field);
        }
        if (evt.keyCode == 13) {
            fleetDataLoad(jQuery(this).val());
        }
    });
    jQuery("#vehiregno").focusout(function () {
        fleetDataLoad(jQuery(this).val());
    });

    function fleetDataLoad(fleet) {
        if (fleet != "") {
            jQuery.ajax({
                type: "GET",
                url: "/ReservationManagement/getFleetDetails",
                contentType: "application/json;charset=utf-8",
                data: { val: fleet },
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == false) {
                            jQuery("#vehiregno").val("");
                            if (result.type == "Error") {
                                setError(result.msg);
                            } else if (result.type == "Info") {
                                setInfoMsg(result.msg);
                            }
                        } else {
                            jQuery("#vehiregno").val(result.data.MSTF_REGNO);
                            jQuery("#vehibrand").val(result.data.MSTF_BRD);
                            jQuery("#vehimodel").val(result.data.MSTF_MODEL);
                        }
                    } else {
                        Logout();
                    }
                }
            });
        }
    }

    jQuery("#GCE_FRM_TN").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            jQuery.ajax({
                type: "GET",
                url: "/ReservationManagement/checkFacLocAvailability",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            var headerKeys = Array()
                            headerKeys = ["Row", "Code", "Description"];
                            field = "pckFacLocSrch"
                            var x = new CommonSearch(headerKeys, field);
                        } else {
                            var headerKeys = Array()
                            headerKeys = ["Row", "Town", "District", "Province", "Code"];
                            field = "pickTownSrch"
                            var x = new CommonSearch(headerKeys, field);
                        }
                    } else {
                        Logout();
                    }
                }
            });

        }
        if (evt.keyCode == 13) {
            getPickTown(jQuery(this).val());
        }
    });
    jQuery(".pick-town-search").click(function () {

        jQuery.ajax({
            type: "GET",
            url: "/ReservationManagement/checkFacLocAvailability",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        var headerKeys = Array()
                        headerKeys = ["Row", "Code", "Description"];
                        field = "pckFacLocSrch"
                        var x = new CommonSearch(headerKeys, field);
                    } else {
                        var headerKeys = Array()
                        headerKeys = ["Row", "Town", "District", "Province", "Code"];
                        field = "pickTownSrch"
                        var x = new CommonSearch(headerKeys, field);
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    jQuery("#GCE_FRM_TN").focusout(function () {
        getPickTown(jQuery(this).val());
    });
    function getPickTown(town) {
        if (town != "") {
            jQuery.ajax({
                type: "GET",
                url: "/ReservationManagement/preTownTextChanged",
                data: { val: town },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == false) {
                            jQuery("#GCE_FRM_TN").val("");
                            setInfoMsg(result.msg);
                        }
                    } else {
                        Logout();
                    }
                }
            });
        }
    }

    jQuery("#GCE_TO_TN").focusout(function () {
        getDropTown(jQuery(this).val());
    });
    jQuery("#GCE_TO_TN").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            jQuery.ajax({
                type: "GET",
                url: "/ReservationManagement/checkFacLocAvailability",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            var headerKeys = Array()
                            headerKeys = ["Row", "Code", "Description"];
                            field = "dropFacLocSrch"
                            var x = new CommonSearch(headerKeys, field);
                        } else {
                            var headerKeys = Array()
                            headerKeys = ["Row", "Town", "District", "Province", "Code"];
                            field = "dropTownSrch"
                            var x = new CommonSearch(headerKeys, field);
                        }
                    } else {
                        Logout();
                    }
                }
            });
        }
        if (evt.keyCode == 13) {
            getDropTown(jQuery(this).val());
        }
    });
    jQuery(".drop-town-search").click(function () {
        jQuery.ajax({
            type: "GET",
            url: "/ReservationManagement/checkFacLocAvailability",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        var headerKeys = Array()
                        headerKeys = ["Row", "Code", "Description"];
                        field = "dropFacLocSrch"
                        var x = new CommonSearch(headerKeys, field);
                    } else {
                        var headerKeys = Array()
                        headerKeys = ["Row", "Town", "District", "Province", "Code"];
                        field = "dropTownSrch"
                        var x = new CommonSearch(headerKeys, field);
                    }
                } else {
                    Logout();
                }
            }
        });

    });

    function getDropTown(town) {
        if (town != "") {
            jQuery.ajax({
                type: "GET",
                url: "/ReservationManagement/preTownTextChanged",
                data: { val: town },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == false) {
                            jQuery("#GCE_TO_TN").val("");
                            setInfoMsg(result.msg);
                        }
                    } else {
                        Logout();
                    }
                }
            });
        }
    }

    jQuery("#ChargCode").on("keydown", function (evt) {
        var invoicing = "False";
        if (jQuery('#Invoicing').is(":checked")) {
            invoicing = "True";
        }
        if (invoicing == "False") {
            var type = jQuery("input[name='trans-type']:checked");
            if (typeof jQuery(type).val() != "undefined") {
                if (evt.keyCode == 113) {
                    if (jQuery(type).val() = "T") {
                        if (jQuery("#ChargType").val() != "") {
                            if (jQuery("#GCE_REQ_NO_VEH").val() != "") {
                                var headerKeys = Array()
                                headerKeys = ["Row", "Code", "Description", "Service By", "From Date", "To Date", "From", "To", "Rate", "Class", "Vehicle"];
                                field = "chrgCdeSrchWithType"
                                var type = jQuery("#ChargType").val();
                                var x = new CommonSearch(headerKeys, field, type);
                            } else {
                                setInfoMsg("Please add number of Vehicles.");
                                jQuery("#GCE_REQ_NO_VEH").focus();
                            }
                        } else {
                            setInfoMsg("Please select transport charg type");
                        }
                    } else if (jQuery(type).val() = "O") {
                        if (evt.keyCode == 113) {
                            var headerKeys = Array()
                            headerKeys = ["Row", "Code", "Description", "Service By", "From Date", "To Date", "Rate"];
                            field = "othChargCdearch";
                            data = "MSCELNS";
                            var x = new CommonSearch(headerKeys, field, data);
                        } else if (evt.keyCode == 13) {
                            getChargCodeDetls(jQuery(this).val());
                        }
                    } else {
                        setInfoMsg("Please select charge type.");
                    }
                }
                if (evt.keyCode == 13) {
                    getChargCodeDetls(jQuery(this).val());
                }
            }
        } else {
            setInfoMsg("Cannot add charges while invoicing.");
        }
    });
    jQuery(".charg-cde-search").click(function () {
        var invoicing = "False";
        if (jQuery('#Invoicing').is(":checked")) {
            invoicing = "True";
        }
        if (invoicing == "False") {
            var type = jQuery("input[name='trans-type']:checked");
            if (typeof jQuery(type).val() != "undefined") {
                if (jQuery(type).val() == "T") {
                    if (jQuery("#ChargType").val() != "") {
                        if (jQuery("#GCE_REQ_NO_VEH").val() != "") {
                            var headerKeys = Array()
                            headerKeys = ["Row", "Code", "Description", "Service By", "From Date", "To Date", "From", "To", "Rate", "Class", "Vehicle"];
                            field = "chrgCdeSrchWithType";
                            var type = jQuery("#ChargType").val();
                            var x = new CommonSearch(headerKeys, field, type);
                        } else {
                            setInfoMsg("Please add number of Vehicles.");
                            jQuery("#GCE_REQ_NO_VEH").focus();
                        }
                    } else {
                        setInfoMsg("Please select charge type.");
                    }
                } else if (jQuery(type).val() == "O") {
                    var headerKeys = Array()
                    headerKeys = ["Row", "Code", "Description", "Service By", "From Date", "To Date", "Rate"];
                    field = "othChargCdearch";
                    data = "MSCELNS";
                    var x = new CommonSearch(headerKeys, field, data);
                } else {
                    setInfoMsg("Please select charge type.");
                }
            }
        } else {
            setInfoMsg("Cannot add charges while invoicing.");
        }
    });
    jQuery("#ChargCode").focusout(function () {
        var invoicing = "False";
        if (jQuery('#Invoicing').is(":checked")) {
            invoicing = "True";
        }
        if (invoicing == "False") {
            if (jQuery("#ChargCode").val() != "") {
                if (jQuery("#GCE_REQ_NO_VEH").val() != "") {
                    var type = jQuery("input[name='trans-type']:checked");
                    if (typeof jQuery(type).val() != "undefined") {
                        if (jQuery(type).val() == "T") {
                            if (jQuery("#ChargType").val() != "") {
                                getChargCodeDetls(jQuery(this).val());
                            } else {
                                setInfoMsg("Please select charge type.");
                                jQuery("#ChargCode").val("");
                            }
                        } else if (jQuery(type).val() == "O") {
                            getChargCodeDetls(jQuery(this).val());
                        }
                    }
                } else {
                    setInfoMsg("Please add number of Vehicles.");
                    jQuery("#GCE_REQ_NO_VEH").focus();
                }
            }
        } else {
            setInfoMsg("Cannot add charges while invoicing.");
        }
    });

    var currency;
    function getChargCodeDetls(code) {

        if (code != "") {
            var type = jQuery("input[name='trans-type']:checked");
            if (typeof jQuery(type).val() != "undefined") {
                jQuery.ajax({
                    type: "GET",
                    url: "/ReservationManagement/loadChargCode",
                    data: { code: code, service: jQuery(type).val(), type: jQuery("#ChargType").val() },
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        if (result.login == true) {
                            if (result.success == true) {
                                if (jQuery(type).val() == "T") {
                                    jQuery("#UnitRate").val(result.code);
                                    jQuery("label.extra-charges").empty();
                                    jQuery("label.extra-charges").html(result.data.STC_AD_RT);
                                    jQuery("label.tax-charges").empty();
                                    jQuery("label.tax-charges").html(result.data.STC_TAX_RT);
                                    updateCurrencyPanel(result.code);
                                    currency = result.Curr;
                                } else if (jQuery(type).val() == "O") {
                                    jQuery("#UnitRate").val(result.code);
                                    jQuery("label.extra-charges").empty();
                                    jQuery("label.extra-charges").html(result.data.SSM_AD_RT);
                                    jQuery("label.tax-charges").empty();
                                    jQuery("label.tax-charges").html(result.data.SSM_TAX_RT);
                                    updateCurrencyPanel(result.code);
                                    currency = result.Curr;
                                } else {
                                    setInfoMsg("Please select valid service.");
                                }

                            } else {
                                jQuery("#UnitRate").val("");
                                jQuery("#ChargCode").val("");
                                if (result.type == "Error") {
                                    setError(result.msg);
                                }
                                if (result.type == "Info") {
                                    setInfoMsg(result.msg);
                                }
                                jQuery("#ChargCode").focus();
                                jQuery("label.tax-charges").empty();
                                jQuery("label.tax-charges").html('0.00');
                            }
                        } else {
                            Logout();
                        }
                    }

                });
            } else {
                setInfoMsg("Please select service.");
            }
        }
    }
    jQuery("#GCE_NIC").on("keydown", function (evt) {
        if (evt.keyCode == 13) {
            loadCustomerData(jQuery(this).val());
        }
    });
    jQuery("#GCE_NIC").focusout(function (evt) {
        loadCustomerData(jQuery(this).val());
    });
    function loadCustomerData(val) {
        if (val != "") {
            jQuery.ajax({
                type: "GET",
                url: "/ReservationManagement/getDataCustomerFromNic",
                data: { nic: val },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (typeof (result.local) != "undefined") {
                                jQuery("#GCE_NIC").val(result.data.Mbe_nic);
                                jQuery("#GCE_CUS_CD").val(result.data.Mbe_cd);
                                jQuery("#GCE_MOB").val(result.data.Mbe_mob);
                                jQuery("#GCE_NAME").val(result.data.Mbe_name);
                                jQuery("#GCE_ADD1").val(result.data.Mbe_add1);
                                jQuery("#GCE_ADD2").val(result.data.Mbe_add2);
                                jQuery("#GCE_PP_NO").val(result.data.Mbe_pp_no);
                                jQuery("#GCE_CUS_TYPE").val(result.data.Mbe_cate);
                            }
                            if (typeof (result.group) != "undefined") {
                                jQuery("#GCE_NIC").val(result.data.Mbg_nic);
                                jQuery("#GCE_CUS_CD").val(result.data.Mbg_cd);
                                jQuery("#GCE_MOB").val(result.data.Mbg_mob);
                                jQuery("#GCE_NAME").val(result.data.Mbg_name);
                                jQuery("#GCE_ADD1").val(result.data.Mbg_add1);
                                jQuery("#GCE_ADD2").val(result.data.Mbg_add2);
                                jQuery("#GCE_PP_NO").val(result.data.Mbg_pp_no);
                                jQuery("#GCE_CUS_TYPE").val(result.data.Mbg_cate);
                            }
                        } else {
                            jQuery("#GCE_NIC").val("");
                            jQuery("#GCE_CUS_CD").val("");
                            jQuery("#GCE_MOB").val("");
                            jQuery("#GCE_NAME").val("");
                            jQuery("#GCE_ADD1").val("");
                            jQuery("#GCE_ADD2").val("");
                            jQuery("#GCE_PP_NO").val("");
                            jQuery("#GCE_CUS_TYPE").val("");
                            if (result.type == "Info") {
                                setInfoMsg(result.msg);
                            }
                            if (result.type == "Error") {
                                setInfoMsg(result.msg);
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
    jQuery("#GCE_NO_PASS").focusout(function () {
        if (jQuery("#GCE_NO_PASS").val() != "") {
            if (!jQuery.isNumeric(jQuery("#GCE_NO_PASS").val())) {
                jQuery("#GCE_NO_PASS").val("");
                setInfoMsg("Passengers only can be number.");
                jQuery("#GCE_NO_PASS").focus();
            }
        }
    });
    jQuery("#GCE_NO_PASS").on("keydown", function (evt) {
        if (evt.keyCode == 13) {
            if (jQuery("#GCE_NO_PASS").val() != "") {
                if (!jQuery.isNumeric(jQuery("#GCE_NO_PASS").val())) {
                    jQuery("#GCE_NO_PASS").val("");
                    setInfoMsg("Passengers only can be number.");
                    jQuery("#GCE_NO_PASS").focus();
                }
            }
        }
    });
    jQuery("#GCE_REQ_NO_VEH").focusout(function () {
        if (jQuery("#GCE_REQ_NO_VEH").val() != "") {
            if (!jQuery.isNumeric(jQuery("#GCE_REQ_NO_VEH").val())) {
                jQuery("#GCE_REQ_NO_VEH").val("");
                setInfoMsg("Number of vehicles only can be number.");
                jQuery("#GCE_REQ_NO_VEH").focus();
            }
        }
    });
    jQuery("#GCE_REQ_NO_VEH").on("keydown", function (evt) {
        if (evt.keyCode == 13) {
            if (jQuery("#GCE_REQ_NO_VEH").val() != "") {
                if (!jQuery.isNumeric(jQuery("#GCE_REQ_NO_VEH").val())) {
                    jQuery("#GCE_REQ_NO_VEH").val("");
                    setInfoMsg("Number of vehicles only can be number.");
                    jQuery("#GCE_REQ_NO_VEH").focus();
                }
            }
        }
    });
    jQuery("#DiscountRate").focusout(function () {
        calculatePrices();
    });
    jQuery("#DiscountRate").on("keydown", function (evt) {
        if (evt.keyCode == 13) {
            calculatePrices();
        }
    });

    jQuery(".trans-type").click(function () {
        var type = jQuery("input[name='trans-type']:checked");
        if (typeof jQuery(type).val() != "undefined") {
            if (jQuery(type).val() == "T") {
                jQuery(".checge-showstat").show();
            } else {
                jQuery(".checge-showstat").hide();
            }
        }
    });
    function calculatePrices() {
        if (jQuery("#ChargCode").val() != "") {
            var type = jQuery("input[name='trans-type']:checked");
            if (typeof jQuery(type).val() != "undefined") {
                jQuery.ajax({
                    type: "GET",
                    url: "/ReservationManagement/loadChargCode",
                    data: { code: jQuery("#ChargCode").val(), service: jQuery(type).val() },
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        if (result.login == true) {
                            if (result.success == true) {
                                if (jQuery("#DiscountRate").val() != "") {
                                    if (jQuery.isNumeric(jQuery("#DiscountRate").val())) {
                                        if (jQuery("#GCE_REQ_NO_VEH").val() != "") {
                                            if (jQuery.isNumeric(jQuery("#GCE_REQ_NO_VEH").val())) {
                                                updateCurrencyPanel(jQuery("#UnitRate").val());
                                            } else {
                                                setInfoMsg("Invalid number of Vehicles.");
                                                jQuery("#GCE_REQ_NO_VEH").focus();
                                                jQuery("#DiscountRate").val("");
                                            }
                                        } else {
                                            setInfoMsg("Please add number of Vehicles.");
                                            jQuery("#GCE_REQ_NO_VEH").focus();
                                            jQuery("#DiscountRate").val("");
                                        }
                                    } else {
                                        setInfoMsg("Invalid discount percentage.");
                                        jQuery("#DiscountRate").val("");
                                        jQuery("#DiscountRate").focus();
                                    }
                                } else {
                                    jQuery("#DiscountAmount").val("");
                                    updateCurrencyPanel(jQuery("#UnitRate").val());
                                }
                            } else {
                                jQuery("#ChargCode").val("");
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
            } else {

            }


        } else {
            jQuery("#DiscountRate").val("");
            jQuery("#ChargCode").focus();
        }
    }

    function updatePayment(amount) {
        var payAmount = parseFloat(amount);
        var currentTot = (jQuery("#TotalAmount").val() != "") ? parseFloat(jQuery("#TotalAmount").val()) : 0;
        var finTot = currentTot + amount;
        jQuery("#TotalAmount").val(finTot);
        updateCurrencyAmount(finTot, jQuery("#GCE_CUS_CD").val(), jQuery("#GCE_ENQ_ID").val());
    }
    jQuery(".add-price-amount-trans").on("click", function () {
        if (jQuery("#ChargCode").val() != "") {
            if (jQuery("#GCE_REQ_NO_VEH").val()) {
                if (jQuery("#UnitRate").val() != "") {
                    var chargCode = jQuery("#ChargCode").val();
                    var passengers = jQuery("#GCE_REQ_NO_VEH").val();
                    var discountPercentage = jQuery("#DiscountRate").val();
                    var unitAmount = jQuery("#UnitRate").val();

                    UpdateChargecodeTable(chargCode, passengers, discountPercentage, unitAmount, "", "");
                } else {
                    setInfoMsg("Invalid unit rate.")
                }
            } else {
                setInfoMsg("Invalid passenger count.")
            }

        } else {
            setInfoMsg("Invalid charg Code");
        }

    });

    function SetChargecodeTable(data, total, invoicing) {
        if (invoicing == null) {
            invoicing = "False";
        }
        var remove = '<td style="text-align:center;"><img class="delete-img remove-cost-mehod-cls" src="../Resources/images/Remove.png"></td>';
        var isInv = '<td style="text-align:center;"><img class="right-img" src="/Resources/images/False.png"></td>'
        if (data.SCH_INVOICED == 1) {
            isInv = '<td style="text-align:center;"><img class="right-img" src="/Resources/images/True.png"></td>';
            remove = "<td></td>";
        }
        if (invoicing == "True") {
            remove = "<td></td>";
        }
        jQuery('.charg-enq-tbl').append('<tr class="new-row">' +
                                        '<td>' + data.SCH_ITM_CD + '</td>' +
                                        '<td class="text-left-align">' + addCommas(data.SCH_UNIT_RT) + '</td>' +
                                        '<td class="text-left-align">' + data.SCH_DISC_RT + '</td>' +
                                        '<td class="text-left-align">' + addCommas(data.SCH_ITM_TAX_AMT) + '</td>' +
                                        '<td class="text-left-align">' + addCommas(data.SCH_TOT_AMT) + '</td>' +
                                        isInv +
                                        remove +
                                        '</tr>');

        if (total > 0) {
            jQuery("#TotalAmount").val(total);
            jQuery("#Sird_settle_amt").val(total);
            jQuery(".bal-amount-val").empty();
            jQuery(".bal-amount-val").html(total);
            jQuery(".tot-amount-val").empty();
            jQuery(".tot-amount-val").html(total);

        } else {
            updatePayment(data.Sch_tot_amt);
        }

        jQuery("#ChargCode").val("");
        jQuery("#UnitRate").val("");
        jQuery("#DiscountRate").val("");
        jQuery("#DiscountAmount").val("");
        jQuery("#FinalDiscountedAmount").val("");
        jQuery(".extra-charges").empty();
        jQuery(".extra-charges").html("0.00");
        jQuery(".tax-charges").empty();
        jQuery(".tax-charges").html("0.00");

        jQuery(".remove-cost-mehod-cls").unbind().click(function (evt) {
            var td = jQuery(this).parent('td');
            var tr = jQuery(td).parent('tr');
            var cgCd = jQuery(tr).find('td:eq(0)').html();
            var unitRate = jQuery(tr).find('td:eq(1)').html();
            var totcst = jQuery(tr).find('td:eq(3)').html();
            var discount = jQuery(tr).find('td:eq(2)').html();

            DeleteChargecodeTable(cgCd, totcst);
        });
    }

    function UpdateChargecodeTable(chargCode, passengers, discountPercentage, unitAmount, deleteitem, total) {
        var taxPerc = jQuery(".tax-charges").html();
        var type = jQuery("input[name='trans-type']:checked");
        var type = jQuery("input[name='trans-type']:checked");
        var numOfDays = 1;
        if (typeof jQuery(type).val() != "undefined") {
            if (jQuery(type).val() == "T") {
                numOfDays = getNumberOfDays(jQuery("#GCE_RET_DT").val(), jQuery("#GCE_EXPECT_DT").val());
            }
        }
        if (typeof jQuery(type).val() != "undefined") {
            jQuery.ajax({
                type: "GET",
                url: "/ReservationManagement/updateCharges",
                data: {
                    chargCode: chargCode,
                    passengers: passengers,
                    discountPercentage: discountPercentage,
                    unitAmount: unitAmount,
                    deleteitem: deleteitem,
                    currency: currency,
                    tax: taxPerc,
                    service: jQuery(type).val(),
                    numOfDays: numOfDays
                },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            SetChargecodeTable(result.data, result.total);
                            if (result.libData.GCD_CD != null) {
                                jQuery("#GCE_LBLTY_CHG").val(result.libData.GCD_LBLTY_AMT);
                                jQuery("#GCE_DEPOSIT_CHG").val(result.libData.GCD_DPST_AMT);
                            }
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

    function DeleteChargecodeTable(cgCd, totcst) {
        var invoicing = "False";
        if (jQuery('#Invoicing').is(":checked")) {
            invoicing = "True";
        }
        Lobibox.confirm({
            msg: "Do you want to remove ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    jQuery('.charg-enq-tbl .new-row').remove();
                    jQuery.ajax({
                        type: "GET",
                        url: "/ReservationManagement/removeChargeItem",
                        data: { chgCd: cgCd, totcst: totcst, invoicing: invoicing },
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    var total = result.totAmt;
                                    for (i = 0; i < result.data.length ; i++) {
                                        SetChargecodeTable(result.data[i], total);
                                    }
                                    if (result.existslibData != null) {
                                        jQuery("#GCE_DEPOSIT_CHG").val(0);
                                        jQuery("#GCE_LBLTY_CHG").val(0);
                                    }
                                    if (result.data.length == 0) {
                                        jQuery("#TotalAmount").val(0);
                                        jQuery("#Sird_settle_amt").val(0);

                                    }

                                }
                            }
                        }
                    });
                }
            }
        });
    }

    function clearForm() {
        jQuery("#GCE_CUS_CD").val("");
        jQuery("#GCE_SEQ").val("0");
        jQuery("#GCE_MOB").val("");
        jQuery("#GCE_NIC").val("");
        jQuery("#GCE_NAME").val("");
        jQuery("#GCE_ADD1").val("");
        jQuery("#GCE_ADD2").val("");
        jQuery("#GCE_PP_NO").val("");
        jQuery("#GCE_ENQ_ID").val("");
        jQuery("#GCE_ENQ_SUB_TP").val("");
        jQuery("#GCE_VEH_TP").val("");
        jQuery("#GCE_NO_PASS").val("");
        jQuery("#GCE_REQ_NO_VEH").val("1");
        jQuery("#GCE_FRM_TN").val("");
        jQuery("#GCE_FRM_ADD").val("");
        jQuery("#GCE_RENTAL_AGENT").val("");
        jQuery("#GCE_CITY_OF_ISSUE").val("");
        jQuery("#GCE_TO_TN").val("");
        jQuery("#GCE_TO_ADD").val("");
        jQuery("#GCE_CONT_PER").val("");
        jQuery("#GCE_CONT_MOB").val("");
        jQuery("#GCE_REF").val("");
        jQuery("#GCE_ENQ").val("");
        jQuery('#GCE_EXPECT_DT').val(my_date_format_with_time(new Date()));
        jQuery('#GCE_RET_DT').val(my_date_format_with_time(new Date()));
        jQuery("#GEN_CUST_ENQSER_GCS_SER_PROVIDER").val("");
        jQuery("#GCE_FLEET").val("");
        //jQuery("#GCE_DRIVER").val("");
        jQuery("#drivername").val("");
        jQuery("#drivercontact").val("");
        jQuery("#fromdt").val("");
        jQuery("#todt").val("");
        jQuery('table.driver-allocation-table .new-row').remove();
        jQuery("#ChargCode").val("");
        jQuery("#UnitRate").val("");
        jQuery("#DiscountRate").val("");
        jQuery("#DiscountAmount").val("");
        jQuery("#FinalDiscountedAmount").val("");
        jQuery("#TotalAmount").val("");
        jQuery(".charg-enq-tbl .new-row").remove();
        clearPaymentValues();
        clearSesions();
        jQuery(".payment-table .new-row").remove();
        jQuery(".tot-paid-amount-val").empty();
        jQuery(".tot-paid-amount-val").html("0.00");
        jQuery("#GCE_CUS_TYPE").val("");
        jQuery(".btn-save-trans-data").removeAttr("disabled");
        jQuery(".btn-cancel-trans-data").removeAttr("disabled");
        jQuery(".btn-cancel-trans-data").hide();
        jQuery(".btn-print-invo-data").hide();
        jQuery(".rentl-agnt-name").empty();
        jQuery(".city-ofisu-name").empty();
        jQuery("#GCE_DL_TYPE").val("");
        jQuery('#fromdt').datepicker({ dateFormat: "dd/M/yy" })
        jQuery('#todt').datepicker({ dateFormat: "dd/M/yy" })
        jQuery('#Invoicing').prop('checked', false);
        jQuery(".all-dri-cnt").empty();
        jQuery(".all-dri-cnt").html("Allocated Driver Count : 0");

        jQuery(".all-vehi-cnt").empty();
        jQuery(".all-vehi-cnt").html("Allocated Driver Count : 0");
        jQuery("#GCE_LBLTY_CHG").val("");
        jQuery("#GCE_DEPOSIT_CHG").val("");
        return true;
    }
    jQuery(".btn-clear-trans-data").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to clear data ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    clearForm();
                    jQuery(".btn-save-trans-data").val("Create");
                }
            }
        });

    });

    jQuery(".btn-save-trans-data").click(function () {

        Lobibox.confirm({
            msg: "Do you want to continue ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    jQuery(".btn-save-trans-data").attr("disabled", true);
                    var formdata = jQuery("#trans-enq-frm").serialize();
                    var InvTyp = "CS";
                    var drivername = jQuery("#drivername").val();
                    var drivercontact = jQuery("#drivercontact").val();
                    var invoicing = "False";
                    if (jQuery('#Invoicing').is(":checked")) {
                        invoicing = "True";
                    }
                    jQuery.ajax({
                        type: "GET",
                        url: "/ReservationManagement/saveEnquiryData?InvTyp=" + InvTyp + '&drivername=' + drivername + '&drivercontact=' + drivercontact + "&invoicing=" + invoicing,
                        data: formdata,
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            jQuery(".btn-save-trans-data").removeAttr("disabled");
                            if (result.login == true) {
                                if (result.success == true) {
                                    var invoicing = "False";
                                    if (jQuery('#Invoicing').is(":checked")) {
                                        invoicing = "True";
                                    }
                                    if (invoicing=="True") {
                                        Lobibox.confirm({
                                            msg: "Do you want to print invoice ?",
                                            callback: function ($this, type, ev) {
                                                if (type == "yes") {
                                                    window.open(
                                                          "/ReservationManagement/ReservationInvoicingReport?enqNo=" + jQuery("#GCE_ENQ_ID").val(),
                                                          '_blank' // <- This is what makes it open in a new window.
                                                      );
                                                    setSuccesssMsg(result.msg);
                                                    clearForm();
                                                    loadEnquiryData();
                                                    jQuery(".btn-save-trans-data").val("Create");
                                                } else {
                                                    setSuccesssMsg(result.msg);
                                                    clearForm();
                                                    loadEnquiryData();
                                                    jQuery(".btn-save-trans-data").val("Create");
                                                }
                                            }
                                        });
                                    } else {
                                        setSuccesssMsg(result.msg);
                                        clearForm();
                                        loadEnquiryData();
                                        jQuery(".btn-save-trans-data").val("Create");
                                    }
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
    });
    jQuery("#GCE_ENQ_ID").on("keypress", function (evt) {
        jQuery(".btn-cancel-trans-data").hide();
        jQuery(".btn-print-invo-data").hide();
        if (jQuery(this).val() != "") {
            if (evt.keyCode == 13) {

                loadEnqData(jQuery(this).val());
            }
        }
    });
    jQuery("#GCE_ENQ_ID").on("focusout", function () {
        jQuery(".btn-cancel-trans-data").hide();
        jQuery(".btn-print-invo-data").hide();
        if (jQuery(this).val() != "") {
            loadEnqData(jQuery(this).val());
        }
    });
    function loadEnqData(enqId) {
        if (enqId != "") {
            clearForm();
            jQuery.ajax({
                type: "GET",
                url: "/ReservationManagement/getEnquiryData",
                contentType: "application/json;charset=utf-8",
                data: { enqId: enqId },
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.data.GCE_ENQ_ID != null) {
                                setEnqData(result.data, result.recItems);
                                //driverCodeLoad(result.data.GCE_DRIVER);
                                jQuery(".btn-save-trans-data").val("Update");
                                jQuery(".btn-cancel-trans-data").show();
                                jQuery(".btn-print-invo-data").show();
                                jQuery(".all-dri-cnt").empty();
                                jQuery(".all-dri-cnt").html("Allocated Driver Count : " + result.driCnt);

                                jQuery(".all-vehi-cnt").empty();
                                jQuery(".all-vehi-cnt").html("Allocated Driver Count : " + result.vehCnt);
                            } else {
                                setInfoMsg("Invalid enquiry id.");
                                jQuery(".btn-save-trans-data").val("Create");
                                jQuery(".btn-cancel-trans-data").hide();
                                jQuery(".btn-print-invo-data").hide();
                            }
                        } else {
                            if (result.type == "Error") {
                                setError(result.msg);
                            }
                            if (result.type == "Info") {
                                setInfoMsg(result.msg);
                            }
                            jQuery(".btn-cancel-trans-data").hide();
                            jQuery(".btn-print-invo-data").hide();
                        }
                    } else {
                        Logout();
                    }
                }
            });
        }

    }
    function setEnqData(data, recItems) {
        if (typeof data != 'undefined') {
            jQuery("#GCE_ADD1").val(data.GCE_ADD1);
            jQuery("#GCE_ADD2").val(data.GCE_ADD2);
            jQuery("#GCE_BILL_CUSADD1").val(data.GCE_BILL_CUSADD1);
            jQuery("#GCE_BILL_CUSADD2").val(data.GCE_BILL_CUSADD2);
            jQuery("#GCE_CONT_CD").val(data.GCE_CONT_CD);
            jQuery("#GCE_CONT_MOB").val(data.GCE_CONT_MOB);
            jQuery("#GCE_CONT_PER").val(data.GCE_CONT_PER);
            jQuery("#GCE_CUS_CD").val(data.GCE_CUS_CD);
            //jQuery("#GCE_DRIVER").val(data.GCE_DRIVER);
            jQuery("#GCE_ENQ").val(data.GCE_ENQ);
            jQuery("#GCE_ENQ_ID").val(data.GCE_ENQ_ID);
            jQuery("#GCE_ENQ_SBU").val(data.GCE_ENQ_SBU);
            jQuery("#GCE_ENQ_SUB_TP").val(data.GCE_ENQ_SUB_TP);
            jQuery("#GCE_EXPECT_DT").val(my_date_format_with_time(convertDate(data.GCE_EXPECT_DT)));
            jQuery("#GCE_FLEET").val(data.GCE_FLEET);
            jQuery("#GCE_FRM_ADD").val(data.GCE_FRM_ADD);
            jQuery("#GCE_CITY_OF_ISSUE").val(data.GCE_CITY_OF_ISSUE);
            jQuery("#GCE_RENTAL_AGENT").val(data.GCE_RENTAL_AGENT);
            jQuery("#GCE_FRM_TN").val(data.GCE_FRM_TN);
            jQuery("#GCE_MOB").val(data.GCE_MOB);
            jQuery("#GCE_CUS_TYPE").val(data.GCE_CUS_TYPE);
            jQuery("#GCE_PP_NO").val(data.GCE_PP_NO);
            if (data.GCE_NAME != null) {
                //var title = data.GCE_NAME.substr(0, data.GCE_NAME.indexOf('.'));
                //var name = data.GCE_NAME.substr(data.GCE_NAME.indexOf(' ') + 1);
                jQuery("#GCE_NAME").val(data.GCE_NAME);
            } else {
                jQuery("#GCE_NAME").val("");
            }

            jQuery("#GCE_NIC").val(data.GCE_NIC);
            jQuery("#GCE_NO_PASS").val(data.GCE_NO_PASS);
            jQuery("#GCE_REF").val(data.GCE_REF);
            jQuery("#GCE_REQ_NO_VEH").val(data.GCE_REQ_NO_VEH);
            jQuery("#GCE_RET_DT").val(my_date_format_with_time(convertDate(data.GCE_RET_DT)));
            jQuery("#GCE_SBU").val(data.GCE_SBU);
            jQuery("#GCE_SEQ").val(data.GCE_SEQ);
            jQuery("#GCE_TO_ADD").val(data.GCE_TO_ADD);
            jQuery("#GCE_TO_TN").val(data.GCE_TO_TN);
            jQuery("#GCE_VEH_TP").val(data.GCE_VEH_TP);
            jQuery("#GCE_DL_TYPE").val(data.GCE_DL_TYPE);
            jQuery("#GCE_LBLTY_CHG").val(data.GCE_LBLTY_CHG);
            jQuery("#GCE_DEPOSIT_CHG").val(data.GCE_DEPOSIT_CHG);
            if (data.DRIVER_DETAILS != null) {
                jQuery("#drivername").val(data.DRIVER_DETAILS.Mbg_name);
                jQuery("#drivercontact").val(data.DRIVER_DETAILS.Mbg_contact);
            } else {
                jQuery("#drivername").val("");
                jQuery("#drivercontact").val("");
            }
            var total = 0;
            if (data.ENQ_CHARGES.length > 0) {

                for (i = 0; i < data.ENQ_CHARGES.length; i++) {
                    var remove = '<td style="text-align:center;"><img class="delete-img remove-cost-mehod-cls" src="../Resources/images/Remove.png"></td>';
                    var isInv = '<td style="text-align:center;"><img class="right-img" src="/Resources/images/False.png"></td>'
                    if (data.ENQ_CHARGES[i].SCH_INVOICED == 1) {
                        isInv = '<td style="text-align:center;"><img class="right-img" src="/Resources/images/True.png"></td>';
                        remove = "<td></td>";
                    }

                    jQuery('.charg-enq-tbl').append('<tr class="new-row">' +
                    '<td>' + data.ENQ_CHARGES[i].SCH_ITM_CD + '</td>' +
                    '<td class="text-left-align">' + addCommas(data.ENQ_CHARGES[i].SCH_UNIT_RT) + '</td>' +
                    '<td class="text-left-align">' + data.ENQ_CHARGES[i].SCH_DISC_RT + '</td>' +
                    '<td class="text-left-align">' + addCommas(data.ENQ_CHARGES[i].SCH_ITM_TAX_AMT) + '</td>' +
                    '<td class="text-left-align">' + addCommas(data.ENQ_CHARGES[i].SCH_TOT_AMT) + '</td>' +
                    isInv +
                     remove +
                    '</tr>');
                    if (data.ENQ_CHARGES[i].SCH_INVOICED == 0) {
                        total = parseFloat(data.ENQ_CHARGES[i].SCH_TOT_AMT) + parseFloat(total);
                    }
                }
                jQuery(".remove-cost-mehod-cls").unbind().click(function (evt) {
                    var td = jQuery(this).parent('td');
                    var tr = jQuery(td).parent('tr');
                    var cgCd = jQuery(tr).find('td:eq(0)').html();
                    var unitRate = jQuery(tr).find('td:eq(1)').html();
                    var totcst = jQuery(tr).find('td:eq(3)').html();
                    var discount = jQuery(tr).find('td:eq(2)').html();

                    DeleteChargecodeTable(cgCd, totcst);
                });
                jQuery(".btn-cancel-trans-data").attr("disabled", true);
            } else {
                jQuery(".btn-cancel-trans-data").removeAttr("disabled");
            }
            updateCurrencyAmount(total, data.GCE_CUS_CD, "");
            if (recItems.length > 0) {
                setPayment(recItems);
                jQuery(".btn-save-trans-data").attr("disabled", true);
                jQuery(".btn-cancel-trans-data").attr("disabled", true);
            } else {
                jQuery(".btn-save-trans-data").removeAttr("disabled");
                jQuery(".btn-cancel-trans-data").removeAttr("disabled");
            }
            validateCountry(data.GCE_CITY_OF_ISSUE);
            rentalAgentValidate(data.GCE_RENTAL_AGENT);


        }
    }

    function clearSesions() {
        jQuery.ajax({
            type: 'POST',
            url: '/ReservationManagement/clearValues',
            success: function (response) {
                return true;
            }
        });
        return false;
    }


    $('td', 'table').each(function (i) {
        $(this).text(i + 1);
    });

    function loadPagin() {
        jQuery("#myPager").empty();
        jQuery('table.pending-trans-enqui').each(function () {
            var currentPage = 0;
            var numPerPage = 4;
            var $table = $(this);
            $table.bind('repaginate', function () {
                $table.find('tbody tr').hide().slice(currentPage * numPerPage, (currentPage + 1) * numPerPage).show();
            });
            $table.trigger('repaginate');
            var numRows = $table.find('tbody tr').length;
            var numPages = Math.ceil(numRows / numPerPage);
            var $pager = $('<div class="pager req-det-pager"></div>');
            for (var page = 0; page < numPages; page++) {
                $('<span class="page-number"></span>').text(page + 1).bind('click', {
                    newPage: page
                }, function (event) {
                    currentPage = event.data['newPage'];
                    $table.trigger('repaginate');
                    $(this).addClass('active').siblings().removeClass('active');
                }).appendTo($pager).addClass('clickable');
            }
            $pager.insertAfter($table).find('span.page-number:first').addClass('active');
        });

    }
    $.fn.pageMe = function (opts) {
        jQuery("#myPager").empty();
        var $this = this,
            defaults = {
                perPage: 4,
                showPrevNext: true,
                hidePageNumbers: false
            },
            settings = $.extend(defaults, opts);

        var listElement = $this;
        var perPage = settings.perPage;
        var children = listElement.children();
        var pager = $('.pager');
        if (typeof settings.childSelector != "undefined") {
            children = listElement.find(settings.childSelector);
        }

        if (typeof settings.pagerSelector != "undefined") {
            pager = $(settings.pagerSelector);
        }

        var numItems = children.size();
        var numPages = Math.ceil(numItems / perPage);

        pager.data("curr", 0);

        if (settings.showPrevNext) {
            $('<li><a href="#" class="prev_link">«</a></li>').appendTo(pager);
        }

        var curr = 0;
        while (numPages > curr && (settings.hidePageNumbers == false)) {
            $('<li><a href="#" class="page_link">' + (curr + 1) + '</a></li>').appendTo(pager);
            curr++;
        }

        if (settings.showPrevNext) {
            $('<li><a href="#" class="next_link">»</a></li>').appendTo(pager);
        }

        pager.find('.page_link:first').addClass('active');
        pager.find('.prev_link').hide();
        if (numPages <= 1) {
            pager.find('.next_link').hide();
        }
        pager.children().eq(1).addClass("active");

        children.hide();
        children.slice(0, perPage).show();

        pager.find('li .page_link').click(function () {
            var clickedPage = $(this).html().valueOf() - 1;
            goTo(clickedPage, perPage);
            return false;
        });
        pager.find('li .prev_link').click(function () {
            previous();
            return false;
        });
        pager.find('li .next_link').click(function () {
            next();
            return false;
        });

        function previous() {
            var goToPage = parseInt(pager.data("curr")) - 1;
            goTo(goToPage);
        }

        function next() {
            goToPage = parseInt(pager.data("curr")) + 1;
            goTo(goToPage);
        }

        function goTo(page) {
            var startAt = page * perPage,
                endOn = startAt + perPage;

            children.css('display', 'none').slice(startAt, endOn).show();

            if (page >= 1) {
                pager.find('.prev_link').show();
            }
            else {
                pager.find('.prev_link').hide();
            }

            if (page < (numPages - 1)) {
                pager.find('.next_link').show();
            }
            else {
                pager.find('.next_link').hide();
            }

            pager.data("curr", page);
            pager.children().removeClass("active");
            pager.children().eq(page + 1).addClass("active");

        }
    };


    function setError(msg) {
        Lobibox.alert('error',
        {
            msg: msg
        });
    }
    function setSuccesssMsg(msg) {
        Lobibox.alert('success',
        {
            msg: msg
        });
    }
    function setInfoMsg(msg) {
        Lobibox.alert('info',
         {
             msg: msg
         });
    }
    loadPayModesTypes("CS");


    var first = true;
    jQuery(".add-customer-data").click(function () {
        jQuery(".customer-create-popup").dialog({
            height: 580,
            width: "75%",
            resizable: false,
            draggable: false,
            //closeOnEscape: true,
            //title: "Create Customer",
            modal: true,
            open: function (event, ui) {
                //$(event.target).parent().css('position', 'fixed');
                jQuery(event.target).parent().css('top', '50px');
                jQuery(event.target).parent().css('left', '10%');
                jQuery(".customer-create-popup").css('overflow-x', '-moz-hidden-unscrollable');
                if (first == true) {
                    dataEntry();
                    first = false;
                }
                jQuery(".customer-create-popup .btn-close-form").click(function () {
                    jQuery(".customer-create-popup").dialog('close');
                    clearCustomerData();
                });
            }
            //,
            //buttons: {
            //    Close: function () {
            //        jQuery(this).dialog('close');
            //        clearCustomerData()
            //    }
            //}
        });
    });
    var cuscd;
    var mob;
    var nic;
    var name;
    var add1;
    var add2;
    var PPnO;
    jQuery(".btn-save-data").click(function (event) {
        cuscd = jQuery("#customer-crte-frm #Mbe_cd").val();
        mob = jQuery("#customer-crte-frm #Mbe_mob").val();
        nic = jQuery("#customer-crte-frm #Mbe_nic").val();
        name = jQuery("#customer-crte-frm #Mbe_name").val();
        add1 = jQuery("#customer-crte-frm #Mbe_add1").val();
        add2 = jQuery("#customer-crte-frm #Mbe_add2").val();
        PPnO = jQuery("#customer-crte-frm #Mbe_pp_no").val();
        event.preventDefault();
        jQuery(this).attr("disabled", true);
        var formdata = jQuery("#customer-crte-frm").serialize();
        jQuery.ajax({
            type: 'POST',
            url: '/DataEntry/CustomerCreation',
            data: formdata,
            success: function (response) {
                if (response.login == true) {
                    if (response.success == true) {
                        document.getElementById("customer-crte-frm").reset();
                        fieldEnable();
                        clearCustomerData();
                        setSuccesssMsg(response.msg);
                        jQuery(".btn-save-data").removeAttr("disabled");
                        jQuery(".customer-create-popup").dialog('close');
                        if (response.cusCd != "") {
                            jQuery("#trans-enq-frm #GCE_CUS_CD").val(response.cusCd);
                        } else {
                            jQuery("#trans-enq-frm #GCE_CUS_CD").val(cuscd);
                        }
                        jQuery("#trans-enq-frm #GCE_MOB").val(mob);
                        jQuery("#trans-enq-frm #GCE_NIC").val(nic);
                        jQuery("#trans-enq-frm #GCE_NAME").val(name);
                        jQuery("#trans-enq-frm #GCE_ADD1").val(add1);
                        jQuery("#trans-enq-frm #GCE_ADD2").val(add2);
                        jQuery("#trans-enq-frm #GCE_pp_no").val(PPnO);


                    } else {
                        jQuery(".btn-save-data").removeAttr("disabled");
                        if (response.type == "Error") {
                            setError(response.msg);
                        } else if (response.type == "Info") {
                            setInfoMsg(response.msg);
                        }

                    }
                } else {
                    Logout();
                }
            }
        });
        return false;
    });

    jQuery('#GCE_CONT_MOB,#GCE_REQ_NO_VEH').on('input', function (event) {
        this.value = this.value.replace(/[^0-9]/g, '');
    });


    //check numbers and decimal  only
    jQuery('#GCE_NO_PASS,#DiscountRate,#DiscountAmount,#FinalDiscountedAmount,#GCE_LBLTY_CHG').on("input", function (event) {
        if (!jQuery.isNumeric(this.value)) {
            this.value = "";
        }
        if (parseFloat(this.value) < 0) {
            this.value = "";
        }

    });
    jQuery('#GCE_NO_PASS,#DiscountRate,#DiscountAmount,#FinalDiscountedAmount,#GCE_LBLTY_CHG').keypress(function (event) {
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
    jQuery("#UnitRate").on("input", function () {
        updateCurrencyPanel(jQuery(this).val());
        //jQuery("#DiscountRate").val(0);
        //jQuery("#DiscountAmount").val(0);
        //console.log(jQuery("#UnitRate").val().length);
        //if (jQuery("#UnitRate").val().length == 0) {
        //    console.log("inside");
        //    jQuery("#DiscountAmount").val("0");
        //    jQuery("#FinalDiscountedAmount").val("0")
        //}
        //var pax = parseFloat(jQuery("#GCE_REQ_NO_VEH").val());
        //var UniteRate = parseFloat(jQuery("#UnitRate").val());
        //var Dis = parseFloat(jQuery("#DiscountRate").val());
        //var Discount = Dis * pax * UniteRate  / 100;
        //jQuery("#DiscountAmount").val(Discount);
        //var Total = (pax * UniteRate ) - Discount;
        //jQuery("#FinalDiscountedAmount").val(Total.toFixed(2));
    });
    jQuery(".cty-issu-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description", "Region Code", "Capital"];
        field = "reseCountry";
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery("#GCE_CITY_OF_ISSUE").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description", "Region Code", "Capital"];
            field = "reseCountry";
            var x = new CommonSearch(headerKeys, field);
        }
        if (evt.keyCode == 13) {
            validateCountry(jQuery(this).val());
        }
    });
    jQuery("#GCE_CITY_OF_ISSUE").focusout(function () {
        if (jQuery(this).val() != "") {
            validateCountry(jQuery(this).val());
        }
    });

    jQuery(".rentl-agnt-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Name", "Nic", "Mobile", "Br No"];
        field = "rentalAgentSrch";
        data = { type: "R" };
        var x = new CommonSearch(headerKeys, field, data);
    });
    jQuery("#GCE_RENTAL_AGENT").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Name", "Nic", "Mobile", "Br No"];
            field = "rentalAgentSrch";
            data = { type: "R" };
            var x = new CommonSearch(headerKeys, field, data);
        }
        if (evt.keyCode == 13) {
            rentalAgentValidate(jQuery(this).val());
        }
    });
    jQuery("#GCE_RENTAL_AGENT").focusout(function () {
        if (jQuery(this).val() != "") {
            rentalAgentValidate(jQuery(this).val());
        }
    });
    jQuery(".btn-cancel-trans-data").click(function () {
        if (jQuery("#GCE_ENQ_ID").val() != "") {
            jQuery("#cancelEnqId").val(jQuery("#GCE_ENQ_ID").val());
            jQuery('#cancelEnqModal').modal('show');
            jQuery("#cancelRemarks").focus();
        } else {
            setInfoMsg("Please select enquiry id.");
        }

    });

    jQuery(".deposit-cde-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row","Code" ,"Liability Amount", "Deposit Amount", "Daily Rental Code", "Daily Rental Amount"];
        field = "depositSrch";
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery("#GCE_LBLTY_CHG").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code","Liability Amount", "Deposit Amount", "Daily Rental Code", "Daily Rental Amount"];
            field = "depositSrch";
            var x = new CommonSearch(headerKeys, field);
        }
        if (evt.keyCode == 13) {
            validateLiability(jQuery(this).val());
        }
    });
    jQuery("#GCE_LBLTY_CHG").focusout(function () {
        if (jQuery(this).val() != "") {
            validateLiability(jQuery(this).val());
        }
    });


    jQuery(".save-enq-cancel").click(function () {
        if (jQuery("#cancelEnqId").val() != "" && jQuery("#cancelEnqId").val() == jQuery("#GCE_ENQ_ID").val()) {
            jQuery.ajax({
                type: "GET",
                url: "/ReservationManagement/enquiryCancel",
                data: { enqId: jQuery("#cancelEnqId").val(), remark: jQuery("#cancelRemarks").val() },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            setSuccesssMsg(result.msg);
                            jQuery('#cancelEnqModal').modal('hide');
                            clearForm();
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
        } else {
            setInfoMsg("Invalid enquiry id.");
        }
    });
    jQuery("#Invoicing").click(function () {
        var enqId = jQuery("#GCE_ENQ_ID").val();
        if (enqId != "") {
            jQuery('.charg-enq-tbl .new-row').remove();
            if (jQuery('#Invoicing').is(":checked")) {
                jQuery.ajax({
                    type: "GET",
                    url: "/ReservationManagement/getUpdatedCharges",
                    contentType: "application/json;charset=utf-8",
                    data: { enqId: enqId },
                    dataType: "json",
                    success: function (result) {
                        if (result.login == true) {
                            if (result.success == true) {
                                if (result.data != null) {
                                    if (result.data.length > 0) {
                                        var total = result.totAmt;
                                        for (i = 0; i < result.data.length ; i++) {
                                            SetChargecodeTable(result.data[i], total, "True");
                                        }
                                    }
                                }
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
            } else {
                jQuery.ajax({
                    type: "GET",
                    url: "/ReservationManagement/getOldCharges",
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        if (result.login == true) {
                            if (result.success == true) {
                                if (result.data != null) {
                                    if (result.data.length > 0) {
                                        var total = result.totAmt;
                                        for (i = 0; i < result.data.length ; i++) {
                                            SetChargecodeTable(result.data[i], total, "False");
                                        }
                                    }
                                }
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
        } else {
            jQuery('#Invoicing').prop('checked', false);
            setInfoMsg("Please select enquiry id.");
        }

    });

    jQuery(".trans-enq-cls .check-url").click(function () {
        window.open(
                   '/CheckInOut/Index?enqId=' + jQuery("#GCE_ENQ_ID").val(),
                   '_blank' // <- This is what makes it open in a new window.
               );
    });

    jQuery(".search-img.add-multi-vehicles").click(function () {
        if (jQuery("#vehiregno").val() != "") {
            if (jQuery("#vehifromdt").val() != "" && jQuery("#vehitodt").val() != "") {
                jQuery.ajax({
                    type: "GET",
                    url: "/ReservationManagement/addEnquiryVehicles",
                    data: { vehiNo: jQuery("#vehiregno").val(), fromdt: jQuery("#vehifromdt").val(), todt: jQuery("#vehitodt").val(), GCE_EXPECT_DT: jQuery("#GCE_EXPECT_DT").val(), GCE_RET_DT: jQuery("#GCE_RET_DT").val(), enqId: jQuery("#GCE_ENQ_ID").val() },
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        if (result.login == true) {
                            if (result.success == true) {
                                if (result.vehicleList.length > 0) {
                                    updateVehicleTable(result.vehicleList);
                                }
                                clearVehicleField();
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
            } else {
                setInfoMsg("Please select from date and to date.");
            }
        } else {
            setInfoMsg("Please select driver code.");
        }
    });

    jQuery(".search-img.add-multi-drivers").click(function () {
        if (jQuery("#drivercd").val() != "") {
            if (jQuery("#fromdt").val() != "" && jQuery("#todt").val() != "") {
                jQuery.ajax({
                    type: "GET",
                    url: "/ReservationManagement/addEnquiryDrivers",
                    data: { driver: jQuery("#drivercd").val(), fromdt: jQuery("#fromdt").val(), todt: jQuery("#todt").val(), GCE_EXPECT_DT: jQuery("#GCE_EXPECT_DT").val(), GCE_RET_DT: jQuery("#GCE_RET_DT").val(), enqId: jQuery("#GCE_ENQ_ID").val() },
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        if (result.login == true) {
                            if (result.success == true) {
                                if (result.driverList.length > 0) {
                                    updateDriverTable(result.driverList);
                                }
                                clearDriverField();
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
            } else {
                setInfoMsg("Please select from date and to date.");
            }
        } else {
            setInfoMsg("Please select driver code.");
        }
    });
    jQuery(".btn-allow-driver").click(function (evt) {
        evt.preventDefault();
        clearDriverField();
        jQuery.ajax({
            type: "GET",
            url: "/ReservationManagement/getAssigedDrivers",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        updateDriverTable(result.driverList);
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
        jQuery('#driverModal').modal({ backdrop: 'static', keyboard: false });
    });
    jQuery(".allocate-cancel").click(function (evt) {
        evt.preventDefault();
        jQuery('#driverModal').modal("hide");
    });
    jQuery(".btn-allow-vehicle").click(function (evt) {
        evt.preventDefault();
        clearVehicleField();
        jQuery.ajax({
            type: "GET",
            url: "/ReservationManagement/getAssigedVehicles",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        updateVehicleTable(result.fleetList);
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
        jQuery('#vehicleModal').modal({ backdrop: 'static', keyboard: false });
    });
    jQuery(".allocate-vehi-cancel").click(function (evt) {
        evt.preventDefault();
        jQuery('#vehicleModal').modal("hide");
    });

    jQuery(".btn-print-invo-data").click(function () {
        if (jQuery("#GCE_ENQ_ID").val() != "") {
            //window.location.href = "/Invoicing/InvoicingReport?invNo=" + jQuery("#Sah_inv_no").val();
            window.open(
                  "/ReservationManagement/ReservationInvoicingReport?enqNo=" + jQuery("#GCE_ENQ_ID").val(),
                  '_blank' // <- This is what makes it open in a new window.
              );
        } else {
            setInfoMsg("Please enter enquiry id.");
        }
    });
});

function clearVehicleField() {
    jQuery("#vehiregno").val("");
    jQuery("#vehibrand").val("");
    jQuery("#vehimodel").val("");
    jQuery("#vehifromdt").val("");
    jQuery("#vehitodt").val("");
}
function updateVehicleTable(fleetList) {
    jQuery('table.vehicle-allocation-table .new-row').remove();
    if (fleetList.length > 0) {
        var count = 0;
        for (i = 0; i < fleetList.length; i++) {
            if (fleetList[i].GCF_ACT == 1) {
                jQuery('table.vehicle-allocation-table').append('<tr class="new-row">' +
                    '<td>' + fleetList[i].GCF_FLEET + '</td>' +
                    '<td>' + fleetList[i].GCF_BRAND + '</td>' +
                    '<td>' + fleetList[i].GCF_MODEL + '</td>' +
                    '<td>' + getFormatedDateInput(fleetList[i].GCF_FROM_DT) + '</td>' +
                    '<td>' + getFormatedDateInput(fleetList[i].GCF_TO_DT) + '</td>' +
                    '<td style="text-align:center;"><img class="delete-img remove-vehicle-cls" src="../Resources/images/Remove.png"></td>' +
                    '</tr>');
                count += parseFloat(fleetList[i].GCF_ACT);
            }
        }
        jQuery(".all-vehi-cnt").empty();
        jQuery(".all-vehi-cnt").html("Allocated Vehicle Count : " + count);
        removeVehicles();
    }
}


function updateDriverTable(driverList) {
    jQuery('table.driver-allocation-table .new-row').remove();
    if (driverList.length > 0) {
        var count=0;
        for (i = 0; i < driverList.length; i++) {
            if (driverList[i].GCD_ACT == 1) {
                jQuery('table.driver-allocation-table').append('<tr class="new-row">' +
                    '<td>' + driverList[i].GCD_DRIVER_CD + '</td>' +
                    '<td>' + driverList[i].GCD_DRIVER_NAME + '</td>' +
                    '<td>' + driverList[i].GCD_DRIVER_CONTACT + '</td>' +
                    '<td>' + getFormatedDateInput(driverList[i].GCD_FROM_DT) + '</td>' +
                    '<td>' + getFormatedDateInput(driverList[i].GCD_TO_DT) + '</td>' +
                    '<td style="text-align:center;"><img class="delete-img remove-driver-cls" src="../Resources/images/Remove.png"></td>' +
                    '</tr>');
                count += parseFloat(driverList[i].GCD_ACT);
            }
        }
        jQuery(".all-dri-cnt").empty();
        jQuery(".all-dri-cnt").html("Allocated Driver Count : " + count);
        removeDrivers();
    }
}
function removeVehicles() {
    jQuery(".remove-vehicle-cls").unbind().click(function (evt) {
        evt.preventDefault();
        var td = jQuery(this).parent('td');
        var tr = jQuery(td).parent('tr');
        var driver = jQuery(tr).find('td:eq(0)').html();
        var fromdt = jQuery(tr).find('td:eq(3)').html();
        var todt = jQuery(tr).find('td:eq(4)').html();
        var enqId = jQuery("#GCE_ENQ_ID").val();
        Lobibox.confirm({
            msg: "Do you want to remove ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    jQuery('.vehicle-allocation-table .new-row').remove();
                    jQuery.ajax({
                        type: "GET",
                        url: "/ReservationManagement/removeEnquiryVehicles",
                        data: { fleet: driver, fromdt: fromdt, todt: todt, enqId: enqId },
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    updateVehicleTable(result.vehicleList);
                                } else {
                                    if (result.type == "Error") {
                                        setError(result.msg);
                                    }
                                    if (result.type == "Info") {
                                        setInfoMsg(result.msg);
                                    }
                                }
                            }
                        }
                    });
                }
            }
        });

    });
}
function removeDrivers() {
    jQuery(".remove-driver-cls").unbind().click(function (evt) {
        evt.preventDefault();
        var td = jQuery(this).parent('td');
        var tr = jQuery(td).parent('tr');
        var driver = jQuery(tr).find('td:eq(0)').html();
        var fromdt = jQuery(tr).find('td:eq(3)').html();
        var todt = jQuery(tr).find('td:eq(4)').html();
        var enqId = jQuery("#GCE_ENQ_ID").val();
        Lobibox.confirm({
            msg: "Do you want to remove ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    jQuery('.driver-allocation-table .new-row').remove();
                    jQuery.ajax({
                        type: "GET",
                        url: "/ReservationManagement/removeEnquiryDrivers",
                        data: { driver: driver, fromdt: fromdt, todt: todt, enqId: enqId },
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    updateDriverTable(result.driverList);
                                } else {
                                    if (result.type == "Error") {
                                        setError(result.msg);
                                    }
                                    if (result.type == "Info") {
                                        setInfoMsg(result.msg);
                                    }
                                }
                            }
                        }
                    });
                }
            }
        });

    });
}

function clearDriverField()
{
    jQuery("#drivercd").val("");
    jQuery("#drivername").val("");
    jQuery("#drivercontact").val("");
    jQuery("#fromdt").val("");
    jQuery("#todt").val("");
}
function rentalAgentValidate(code) {
    if (code != "") {
        jQuery.ajax({
            type: "GET",
            url: "/ReservationManagement/cusCodeTextChanged",
            data: { cusCd: code },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == false) {
                        setInfoMsg("Invalid rental agent code.");
                        jQuery(".rentl-agnt-name").empty();
                        jQuery("#GCE_RENTAL_AGENT").val("");
                    } else {
                        jQuery(".rentl-agnt-name").empty();
                        if (typeof (result.local) != "undefined") {
                            jQuery(".rentl-agnt-name").html(result.data.Mbe_name);
                        }
                        if (typeof (result.group) != "undefined") {
                            jQuery(".rentl-agnt-name").html(result.data.Mbg_name);
                        }
                    }
                } else {
                    Logout();
                }

            }

        });
    } else {
        jQuery(".rentl-agnt-name").empty();
    }
}
function validateCountry(code) {
    if (code != "") {
        jQuery.ajax({
            type: "GET",
            url: "/ReservationManagement/validateCountry",
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
                        jQuery("#GCE_CITY_OF_ISSUE").val("");
                        jQuery(".city-ofisu-name").empty();
                    } else {

                        jQuery(".city-ofisu-name").empty();
                        jQuery(".city-ofisu-name").html(result.countryList.MCU_DESC);

                    }
                } else {
                    Logout();
                }
            }
        });
    } else {
        jQuery(".city-ofisu-name").empty();
    }
}
function updateCurrencyPanel(code) {
    var type = jQuery("input[name='trans-type']:checked");
    var numOfDays = 1;
    if (typeof jQuery(type).val() != "undefined") {
        if (jQuery(type).val() == "T") {
            numOfDays = getNumberOfDays(jQuery("#GCE_RET_DT").val(), jQuery("#GCE_EXPECT_DT").val());
        }
    }
    var discount = (jQuery("#DiscountRate").val() != "") ? parseFloat(jQuery("#DiscountRate").val()) : 0;
    var passengers = parseFloat(jQuery("#GCE_REQ_NO_VEH").val());
    var unitrate = (code != "") ? parseFloat(code) : 0;
    var discountAmount = (passengers * unitrate) * discount / 100 * parseFloat(numOfDays);
    var payAmount = parseFloat(passengers * unitrate) - parseFloat(discountAmount);
    var payAmountWithTax = parseFloat(payAmount) + parseFloat(payAmount) * parseFloat(jQuery(".tax-charges").html()) / 100;
    var afterDatePaym = parseFloat(numOfDays) * parseFloat(payAmountWithTax);
    if (unitrate != "NaN") {
        jQuery("#DiscountAmount").val(discountAmount.toFixed(2));
        jQuery("#FinalDiscountedAmount").val(afterDatePaym.toFixed(2));
    } else {
        jQuery("#DiscountAmount").val("");
        jQuery("#FinalDiscountedAmount").val("");
    }

    //var cuttotAmt = (jQuery("#TotalAmount").val() != "") ? parseFloat(jQuery("#TotalAmount").val()) : 0;
    //var crtTot=payAmount+cuttotAmt;
    //jQuery("#TotalAmount").val(crtTot);

}
function setPayment(paymentData) {
    if (paymentData.length > 0) {
        var total = 0;
        jQuery('table.payment-table .new-row').remove();
        for (i = 0; i < paymentData.length; i++) {
            jQuery('table.payment-table').append('<tr class="new-row">' +
                '<td style="text-align:center;"></td>' +
                '<td>' + paymentData[i].Sird_pay_tp + '</td>' +
                '<td>' + paymentData[i].Sird_deposit_bank_cd + '</td>' +
                '<td>' + paymentData[i].Sird_deposit_branch + '</td>' +
                '<td>' + paymentData[i].Sird_cc_tp + '</td>' +
                '<td>' + paymentData[i].Sird_settle_amt + '</td>' +
                '</tr>');
            total = parseFloat(total) + parseFloat(paymentData[i].Sird_settle_amt);
        }
        jQuery(".bal-amount-val").empty();
        jQuery(".bal-amount-val").html("0");
        jQuery(".tot-paid-amount-val").empty();
        jQuery(".tot-paid-amount-val").html(total);
        jQuery("#Sird_settle_amt").val("");
    }
}
function validateLiability(liability)
{
    if (liability != "") {
        jQuery.ajax({
            type: "GET",
            url: "/ReservationManagement/validateLiability",
            contentType: "application/json;charset=utf-8",
            data:{liability:liability},
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
                        jQuery("#GCE_LBLTY_CHG").val("");
                        jQuery("#GCE_DEPOSIT_CHG").val("");
                    } else {

                        jQuery("#GCE_LBLTY_CHG").val(result.data.GCD_LBLTY_AMT);
                        jQuery("#GCE_DEPOSIT_CHG").val(result.data.GCD_DPST_AMT);

                    }
                } else {
                    Logout();
                }
            }
        });
    }
}