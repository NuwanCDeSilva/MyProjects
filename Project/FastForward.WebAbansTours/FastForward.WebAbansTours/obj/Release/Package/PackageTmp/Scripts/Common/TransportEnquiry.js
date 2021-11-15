jQuery(document).ready(function () {
    loadEnquiryData();
    clearForm();
    $('#GCE_FLY_DATE').focusout(function () {
        var str = $(this).val();
        if ($.datepicker.formatDate('MM dd, yy', new Date(jQuery("#GCE_FLY_DATE").val())) == 'undefined NaN, NaN' && jQuery("#GCE_FLY_DATE").val() != '') {
            setInfoMsg('Please enter a valid date !!!');
            $(this).val('');
        }
    });
    $('#GCE_EXPECT_DT').focusout(function () {
        var str = $(this).val();
        if ($.datepicker.formatDate('MM dd, yy', new Date(jQuery("#GCE_EXPECT_DT").val())) == 'undefined NaN, NaN' && jQuery("#GCE_EXPECT_DT").val() != '') {
            setInfoMsg('Please enter a valid date !!!');
            $(this).val('');
        }
    });
    $('#GCE_RET_DT').focusout(function () {
        var str = $(this).val();
        if ($.datepicker.formatDate('MM dd, yy', new Date(jQuery("#GCE_RET_DT").val())) == 'undefined NaN, NaN' && jQuery("#GCE_RET_DT").val() != '') {
            setInfoMsg('Please enter a valid date !!!');
            $(this).val('');
        }
    });
    loadingVehicleTypes();
    function loadingVehicleTypes() {
        jQuery.ajax({
            type: "GET",
            url: "/TransportEnquiry/getVehicleTypes",
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
            url: "/TransportEnquiry/loadTransportEnqData",
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
                                loadEnqData(enqId)

                            });
                        }
                        else {

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
    //Print
    jQuery(".btn-print-trans-data").click(function (evt) {
        if (jQuery("#GCE_ENQ_ID").val() == "") {
            setInfoMsg("Please Select Enquiry!!");
        } else {
            var enqno = jQuery("#GCE_ENQ_ID").val();
            window.open('/TransportEnquiry/TransportPrint?enqno= ' + enqno, '_blank')
        }
    });

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
    jQuery(".guss-cus-cd-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Name", "Nic", "Mobile", "Br No"];
        field = "guesscusCodeEnq"
        var x = new CommonSearch(headerKeys, field);
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
    jQuery("#GCE_CONT_CD").focusout(function () {
        codeFocusOut2();
    });
    function codeFocusOut2() {
        if (jQuery("#GCE_CONT_CD").val() != "") {

            jQuery.ajax({
                type: "GET",
                url: "/DataEntry/cusCodeTextChanged",
                data: { val: jQuery("#GCE_CONT_CD").val() },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.success == true) {
                                if (typeof (result.local) != "undefined") {
                                    setFieldValue2(result.data, false, true);
                                }
                                if (typeof (result.group) != "undefined") {
                                    setFieldValue2(result.data, true, false);
                                }
                                if (typeof (result.group) == "undefined" && typeof (result.local) == "undefined") {
                                    jQuery(".btn-save-data").val("Create");
                                    jQuery(".txt-operation").val("Create");
                                    jQuery(".txt-type").val("");
                                    fieldEnable();
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
    function setFieldValue2(data, group, local) {
        if (data != "" && local) {

            jQuery("#GCE_GUESS").val(data.Mbe_name);
            jQuery("#GCE_CONT_MOB").val(data.Mbe_mob);
            jQuery("#GCE_CONT_EMAIL").val(data.Mbe_email);
            type = "local";
            jQuery(".txt-type").val("local");

        }
        if (data != "" && group) {

            jQuery("#GCE_GUESS").val(data.Mbg_name);
            jQuery("#GCE_CONT_MOB").val(data.Mbg_mob);
            jQuery("#GCE_CONT_EMAIL").val(data.Mbg_email);
            type = "group";
            jQuery(".txt-type").val("group");

        }


    }
    function codeFocusOut(code) {
        if (code != "") {
            jQuery.ajax({
                type: "GET",
                url: "/TransportEnquiry/cusCodeTextChanged",
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
                                    jQuery("#GCE_NAME").val(result.data.MBE_TIT + " " + result.data.Mbe_name);
                                    jQuery("#GCE_ADD1").val(result.data.Mbe_add1);
                                    jQuery("#GCE_ADD2").val(result.data.Mbe_add2);
                                }
                                if (typeof (result.group) != "undefined") {
                                    jQuery("#GCE_NIC").val(result.data.Mbg_nic);
                                    jQuery("#GCE_CUS_CD").val(result.data.Mbg_cd);
                                    jQuery("#GCE_MOB").val(result.data.Mbg_mob);
                                    jQuery("#GCE_NAME").val(result.data._mbg_tit + " " + result.data.Mbg_name);
                                    jQuery("#GCE_ADD1").val(result.data.Mbg_add1);
                                    jQuery("#GCE_ADD2").val(result.data.Mbg_add2);
                                }
                            } else {
                                jQuery("#GCE_NIC").val("");
                                jQuery("#GCE_CUS_CD").val("");
                                jQuery("#GCE_MOB").val("");
                                jQuery("#GCE_NAME").val("");
                                jQuery("#GCE_ADD1").val("");
                                jQuery("#GCE_ADD2").val("");
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
            var title = data.Mbe_name.substr(0, data.Mbe_name.indexOf('.'));
            var name = data.Mbe_name.substr(data.Mbe_name.indexOf(' ') + 1);
            jQuery("#GCE_NAME").val(name);
            jQuery("#GCE_ADD1").val(data.Mbe_add1);
            jQuery("#GCE_ADD2").val(data.Mbe_add2);
        } else {
            jQuery("#GCE_MOB").val("");
            jQuery("#GCE_NIC").val("");
            jQuery("#GCE_NAME").val("");
            jQuery("#GCE_ADD1").val("");
            jQuery("#GCE_ADD2").val("");
        }
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
    jQuery('#GCE_EXPECT_DT').datetimepicker({ minDate: new Date(), dateFormat: "dd/M/yy", timeFormat: "HH:mm" });//{ minDate: new Date(), dateFormat: "dd/M/yy", timeFormat: "hh:mm tt" }
    jQuery('#GCE_RET_DT').datetimepicker({ minDate: new Date(), dateFormat: "dd/M/yy", timeFormat: "HH:mm" });//{ minDate: new Date(), dateFormat: "dd/M/yy", timeFormat: "hh:mm tt" }
    jQuery('#GCE_FLY_DATE').datetimepicker({ minDate: new Date(), dateFormat: "dd/M/yy", timeFormat: "HH:mm" });
    jQuery('#GCE_EXPECT_DT').val(my_date_format_with_time(new Date()));
    jQuery('#GCE_RET_DT').val(my_date_format_with_time(new Date()));
    jQuery('#GCE_FLY_DATE').val(my_date_format_with_time(new Date()));
    jQuery('#Req_dt').datetimepicker({ minDate: new Date(), dateFormat: "dd/M/yy", timeFormat: "HH:mm" });
    jQuery('#Req_dt').val(my_date_format_with_time(new Date()));
    jQuery('#Ret_dt').datetimepicker({ minDate: new Date(), dateFormat: "dd/M/yy", timeFormat: "HH:mm" });
    jQuery('#Ret_dt').val(my_date_format_with_time(new Date()));
    jQuery("#GCE_DRIVER").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Epf No", "First Name", "Last Name", "MOBILE", "NIC"];
            field = "driverSrchTran"
            var x = new CommonSearch(headerKeys, field);
        }
        if (evt.keyCode == 13) {
            driverCodeLoad(jQuery(this).val());
        }
    });
    jQuery(".trans-driver-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Epf No", "First Name", "Last Name", "MOBILE", "NIC"];
        field = "driverSrchTran"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery("#GCE_DRIVER").focusout(function () {
        driverCodeLoad(jQuery(this).val());
    });
    function driverCodeLoad(driver) {
        if (jQuery("#GCE_DRIVER").val() != "") {
            var val = driver;
            jQuery.ajax({
                type: "GET",
                url: "/TransportEnquiry/getEmployeeDetails",
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
                                jQuery("#GCE_DRIVER").val("");
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
    jQuery("#GCE_FLEET").on("keydown", function (evt) {
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
    jQuery("#GCE_FLEET").focusout(function () {
        fleetDataLoad(jQuery(this).val());
    });

    function fleetDataLoad(fleet) {
        if (fleet != "") {
            jQuery.ajax({
                type: "GET",
                url: "/TransportEnquiry/getFleetDetails",
                contentType: "application/json;charset=utf-8",
                data: { val: fleet },
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == false) {
                            jQuery("#GCE_FLEET").val("");
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

    jQuery("#GCE_FRM_TN").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Town", "District", "Province", "Code"];
            field = "pickTownSrch"
            var x = new CommonSearch(headerKeys, field);
        }
        if (evt.keyCode == 13) {
            getPickTown(jQuery(this).val());
        }
    });
    jQuery(".pick-town-search").click(function () {
        jQuery.ajax({
            type: "GET",
            url: "/TransportEnquiry/checkFacLocAvailability",
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
                url: "/TransportEnquiry/preTownTextChanged",
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
            var headerKeys = Array()
            headerKeys = ["Row", "Town", "District", "Province", "Code"];
            field = "dropTownSrch"
            var x = new CommonSearch(headerKeys, field);
        }
        if (evt.keyCode == 13) {
            getDropTown(jQuery(this).val());
        }
    });
    jQuery(".drop-town-search").click(function () {
        jQuery.ajax({
            type: "GET",
            url: "/TransportEnquiry/checkFacLocAvailability",
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
                url: "/TransportEnquiry/preTownTextChanged",
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
        if (jQuery("#GCE_REQ_NO_VEH").val() != "") {
            if (evt.keyCode == 113) {
                var headerKeys = Array()
                headerKeys = ["Row", "Code", "Description","Service By", "From Date", "To Date", "From", "To", "Rate", "Class", "Vehicle"];
                field = "chrgCdeSrch"
                var x = new CommonSearch(headerKeys, field);
            }
            if (evt.keyCode == 13) {
                getChargCodeDetls(jQuery(this).val());
            }
        } else {
            setInfoMsg("Please add number of Vehicles.");
            jQuery("#GCE_REQ_NO_VEH").focus();
        }

    });
    //jQuery(".charg-cde-search").click(function () {
    //    if (jQuery("#GCE_REQ_NO_VEH").val() != "") {
    //        var headerKeys = Array()
    //        headerKeys = ["Row", "Code", "Description","Service By", "From Date", "To Date", "From", "To", "Rate", "Class", "Vehicle"];
    //        field = "chrgCdeSrch";
    //        var x = new CommonSearch(headerKeys, field);
    //    } else {
    //        setInfoMsg("Please add number of Vehicles.");
    //        jQuery("#GCE_REQ_NO_VEH").focus();
    //    }

    //});
    jQuery(".charg-cde-search").click(function () {
        if (jQuery("#GCE_REQ_NO_VEH").val() != "") {
            var headerKeys = Array()
           
           
            if ($('#radtype1').is(':checked'))
            {
                headerKeys = ["Row", "Code", "Description", "Service By", "From Date", "To Date", "From", "To", "Rate", "Class", "Vehicle"];
                field = "chrgCdeSrch2";
                var data = { cuscd: jQuery("#GCE_CUS_CD").val(), option:"1" }
                var x = new CommonSearch(headerKeys, field, data);
            } else
            {
                headerKeys = ["Row", "Code", "Description", "Service By", "From Date", "To Date", "Rate"];
                field = "ChrgCdeSrchMsclens2"
                var data = { cuscd: jQuery("#GCE_CUS_CD").val(), option: "2" }
                var x = new CommonSearch(headerKeys, field, data);
            }

           
        } else {
            setInfoMsg("Please add number of Vehicles.");
            jQuery("#GCE_REQ_NO_VEH").focus();
        }

    });
    jQuery("#ChargCode").focusout(function () {
        if (jQuery("#ChargCode").val() != "") {
            if (jQuery("#GCE_REQ_NO_VEH").val() != "") {

                if ($('#radtype1').is(':checked'))
                {
                    getChargCodeDetls(jQuery(this).val());
                } else
                {
                    getChargCodeDetls2(jQuery(this).val());
                }

               
            } else {
                setInfoMsg("Please add number of Vehicles.");
                jQuery("#GCE_REQ_NO_VEH").focus();
            }
        }
    });
    var currency;
    function getChargCodeDetls(code) {
        if (code != "") {
            jQuery.ajax({
                type: "GET",
                url: "/TransportEnquiry/loadChargCode",
                data: { code: code },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            jQuery("#UnitRate").val(result.code);
                            jQuery("label.extra-charges").empty();
                            jQuery("label.extra-charges").html(result.data.STC_AD_RT);
                            jQuery("label.tax-charges").empty();
                            jQuery("label.tax-charges").html(result.data.STC_TAX_RT);
                            updateCurrencyPanel(result.code);
                            currency = result.Curr;
                            
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
        }
    }

    function getChargCodeDetls2(code) {
        if (code != "") {
            jQuery.ajax({
                type: "GET",
                url: "/TransportEnquiry/loadChargCode2",
                data: { code: code },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            jQuery("#UnitRate").val(result.code);
                            jQuery("label.extra-charges").empty();
                            jQuery("label.extra-charges").html(result.data.STC_AD_RT);
                            jQuery("label.tax-charges").empty();
                            jQuery("label.tax-charges").html(result.data.STC_TAX_RT);
                            updateCurrencyPanel(result.code);
                            currency = result.Curr;

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
                url: "/TransportEnquiry/getDataCustomerFromNic",
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
                            }
                            if (typeof (result.group) != "undefined") {
                                jQuery("#GCE_NIC").val(result.data.Mbg_nic);
                                jQuery("#GCE_CUS_CD").val(result.data.Mbg_cd);
                                jQuery("#GCE_MOB").val(result.data.Mbg_mob);
                                jQuery("#GCE_NAME").val(result.data.Mbg_name);
                                jQuery("#GCE_ADD1").val(result.data.Mbg_add1);
                                jQuery("#GCE_ADD2").val(result.data.Mbg_add2);
                            }
                        } else {
                            jQuery("#GCE_NIC").val("");
                            jQuery("#GCE_CUS_CD").val("");
                            jQuery("#GCE_MOB").val("");
                            jQuery("#GCE_NAME").val("");
                            jQuery("#GCE_ADD1").val("");
                            jQuery("#GCE_ADD2").val("");
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
    function calculatePrices() {
        if (jQuery("#ChargCode").val() != "") {
            jQuery.ajax({
                type: "GET",
                url: "/TransportEnquiry/loadChargCode",
                data: { code: jQuery("#ChargCode").val() },
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
                    var items = jQuery("#Items").val();

                    UpdateChargecodeTable(chargCode, passengers, discountPercentage, unitAmount, "", "", items);
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

    function SetChargecodeTable(data,total) {
        jQuery('.charg-enq-tbl').append('<tr class="new-row">' +
                                        '<td>' + data.Sad_itm_cd + '</td>' +
                                        '<td class="text-left-align">' + addCommas(data.Sad_unit_rt) + '</td>' +
                                        '<td class="text-left-align">' + data.Sad_disc_rt + '</td>' +
                                        '<td class="text-left-align">' + addCommas(data.Sad_itm_tax_amt) + '</td>' +
                                        '<td class="text-left-align">' + addCommas(data.Sad_tot_amt) + '</td>' +
                                         '<td style="text-align:center;"><img class="delete-img remove-cost-mehod-cls" src="../Resources/images/Remove.png"></td>' +
                                        '</tr>');
    
        if (total > 0) {
            jQuery("#TotalAmount").val(total);
            jQuery("#Sird_settle_amt").val(total);
            
        } else {
            updatePayment(data.Sad_tot_amt);
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

    function UpdateChargecodeTable(chargCode, passengers, discountPercentage, unitAmount, deleteitem, total,items) {
        var taxPerc = jQuery(".tax-charges").html();
        jQuery.ajax({
            type: "GET",
            url: "/TransportEnquiry/updateCharges",
            data: {
                chargCode: chargCode,
                passengers: passengers,
                discountPercentage: discountPercentage,
                unitAmount: unitAmount,
                deleteitem: deleteitem,
                currency: currency,
                tax: taxPerc,
                items:items
            },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        SetChargecodeTable(result.data, total);

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

    function DeleteChargecodeTable(cgCd, totcst) {
     
        Lobibox.confirm({
            msg: "Do you want to remove ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    jQuery('.charg-enq-tbl .new-row').remove();
                    jQuery.ajax({
                        type: "GET",
                        url: "/TransportEnquiry/removeChargeItem",
                        data: { chgCd: cgCd, totcst: totcst },
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    var total = result.totAmt;
                                    for (i = 0; i < result.data.length; i++) {

                                        UpdateChargecodeTable(result.data[i].Sad_itm_cd, result.data[i].Sad_qty, 0, result.data[i].Sad_unit_rt, "Delete", total);
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
        jQuery("#GCE_ENQ_ID").val("");
        jQuery("#GCE_ENQ_SUB_TP").val("");
        jQuery("#GCE_VEH_TP").val("");
        jQuery("#GCE_NO_PASS").val("");
        jQuery("#GCE_REQ_NO_VEH").val("1");
        jQuery("#GCE_FRM_TN").val("");
        jQuery("#GCE_FRM_ADD").val("");
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
        jQuery("#GCE_DRIVER").val("");
        jQuery("#drivername").val("");
        jQuery("#drivercontact").val("");
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
                    jQuery(".btn-print-trans-data").hide();
                    jQuery(".btn-send-sms").hide();
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
                    jQuery.ajax({
                        type: "GET",
                        url: "/TransportEnquiry/saveEnquiryData?InvTyp=" + InvTyp + '&drivername=' + drivername + '&drivercontact=' + drivercontact,
                        data: formdata,
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            jQuery(".btn-save-trans-data").removeAttr("disabled");
                            if (result.login == true) {
                                if (result.success == true) {
                                    setSuccesssMsg(result.msg);
                                    clearForm();
                                    loadEnquiryData();
                                    jQuery(".btn-save-trans-data").val("Create");
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
        if (jQuery(this).val() != "") {
            if (evt.keyCode == 13) {

                loadEnqData(jQuery(this).val());
            }
        }
    });
    jQuery("#GCE_ENQ_ID").on("focusout", function () {
        if (jQuery(this).val() != "") {
            loadEnqData(jQuery(this).val());
        }
    });
    function loadEnqData(enqId) {
        if (enqId != "") {
            clearForm();
            jQuery.ajax({
                type: "GET",
                url: "/TransportEnquiry/getEnquiryData",
                contentType: "application/json;charset=utf-8",
                data: { enqId: enqId },
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            setEnqData(result.data, result.recItems);
                                driverCodeLoad(result.data.GCE_DRIVER);
                                jQuery(".btn-save-trans-data").val("Update");
                                jQuery(".btn-print-trans-data").show();
                                jQuery(".btn-send-sms").show();
                        } else {
                            jQuery(".btn-print-trans-data").hide();
                            jQuery(".btn-send-sms").hide();
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
            jQuery("#GCE_DRIVER").val(data.GCE_DRIVER);
            jQuery("#GCE_ENQ").val(data.GCE_ENQ);
            jQuery("#GCE_ENQ_ID").val(data.GCE_ENQ_ID);
            jQuery("#GCE_ENQ_SBU").val(data.GCE_ENQ_SBU);
            jQuery("#GCE_ENQ_SUB_TP").val(data.GCE_ENQ_SUB_TP);
            jQuery("#GCE_EXPECT_DT").val(my_date_format_with_time(convertDate(data.GCE_EXPECT_DT)));
            jQuery("#GCE_FLEET").val(data.GCE_FLEET);
            jQuery("#GCE_FRM_ADD").val(data.GCE_FRM_ADD);
            jQuery("#GCE_FRM_TN").val(data.GCE_FRM_TN);
            jQuery("#GCE_MOB").val(data.GCE_MOB);
            if (data.GCE_NAME != null) {
                var title = data.GCE_NAME.substr(0, data.GCE_NAME.indexOf('.'));
                var name = data.GCE_NAME.substr(data.GCE_NAME.indexOf(' ') + 1);
                jQuery("#GCE_NAME").val(name);
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
            if (data.DRIVER_DETAILS != null) {
                jQuery("#drivername").val(data.DRIVER_DETAILS.Mbg_name);
                jQuery("#drivercontact").val(data.DRIVER_DETAILS.Mbg_contact);
            } else {
                jQuery("#drivername").val("");
                jQuery("#drivercontact").val("");
            }
            var total = 0;
            if (data.CHARGER_VALUE.length > 0) {
               
                for (i = 0; i < data.CHARGER_VALUE.length; i++) {
                    jQuery('.charg-enq-tbl').append('<tr class="new-row">' +
                    '<td>' + data.CHARGER_VALUE[i].Sad_itm_cd + '</td>' +
                    '<td class="text-left-align">' + addCommas(data.CHARGER_VALUE[i].Sad_unit_rt) + '</td>' +
                    '<td class="text-left-align">' + data.CHARGER_VALUE[i].Sad_disc_rt + '</td>' +
                    '<td class="text-left-align">' + addCommas(data.CHARGER_VALUE[i].Sad_itm_tax_amt) + '</td>' +
                    '<td class="text-left-align">' + addCommas(data.CHARGER_VALUE[i].Sad_tot_amt) + '</td>' +
                     '<td style="text-align:center;"><img class="delete-img remove-cost-mehod-cls" src="../Resources/images/Remove.png"></td>' +
                    '</tr>');
                    total = parseFloat(data.CHARGER_VALUE[i].Sad_tot_amt) + parseFloat(total);
                }
            }
            updateCurrencyAmount(total, data.GCE_CUS_CD, "");
            if (recItems.length>0) {
                setPayment(recItems);
                jQuery(".btn-save-trans-data").attr("disabled", true);
            } else {
                jQuery(".btn-save-trans-data").removeAttr("disabled");
            }
        }
    }

    function clearSesions() {
        jQuery.ajax({
            type: 'POST',
            url: '/TransportEnquiry/clearValues',
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

    jQuery(".btn-save-data").click(function (event) {
        cuscd = jQuery("#customer-crte-frm #Mbe_cd").val();
        mob = jQuery("#customer-crte-frm #Mbe_mob").val();
        nic = jQuery("#customer-crte-frm #Mbe_nic").val();
        name = jQuery("#customer-crte-frm #Mbe_name").val();
        add1 = jQuery("#customer-crte-frm #Mbe_add1").val();
        add2 = jQuery("#customer-crte-frm #Mbe_add2").val();
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
    jQuery('#GCE_NO_PASS,#DiscountRate,#DiscountAmount,#FinalDiscountedAmount').on("input", function (event) {
        if (!jQuery.isNumeric(this.value)) {
            this.value = "";
        }
        if (parseFloat(this.value) < 0) {
            this.value = "";
        }

    });
    jQuery('#GCE_NO_PASS,#DiscountRate,#DiscountAmount,#FinalDiscountedAmount').keypress(function (event) {
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
    })
    jQuery(".btn-send-sms").click(function () {
        Lobibox.confirm({
            msg: "Do you want to send SMS ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    if (jQuery("#GCE_ENQ_ID").val() != "") {
                        jQuery.ajax({
                            type: 'POST',
                            url: '/TransportEnquiry/senEnquirySMS',
                            data: { enqNo: jQuery("#GCE_ENQ_ID").val() },
                            success: function (response) {
                                if (response.login == true) {
                                    if (response.success == true) {
                                        setSuccesssMsg(response.msg);
                                    } else {
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
                    } else {
                        setInfoMsg("Please select enquiry id.");
                    }
                }
            }
        });
    });
});
function updateCurrencyPanel(code) {
    var discount = (jQuery("#DiscountRate").val() != "") ? parseFloat(jQuery("#DiscountRate").val()) : 0;
    var passengers = parseFloat(jQuery("#GCE_REQ_NO_VEH").val());
    var unitrate = (code != "") ? parseFloat(code) : 0;
    var discountAmount = (passengers * unitrate) * discount / 100;
    var payAmount = parseFloat(passengers * unitrate) - parseFloat(discountAmount);
    var payAmountWithTax = parseFloat(payAmount) + parseFloat(payAmount) * parseFloat(jQuery(".tax-charges").html()) / 100;
    
    if (unitrate != "NaN") {
        jQuery("#DiscountAmount").val(discountAmount);
        jQuery("#FinalDiscountedAmount").val(payAmountWithTax);
    } else {
        jQuery("#DiscountAmount").val("");
        jQuery("#FinalDiscountedAmount").val("");
    }
    
    //var cuttotAmt = (jQuery("#TotalAmount").val() != "") ? parseFloat(jQuery("#TotalAmount").val()) : 0;
    //var crtTot=payAmount+cuttotAmt;
    //jQuery("#TotalAmount").val(crtTot);

}
function setPayment(paymentData) {
    if (paymentData.length>0) {
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
            total = parseFloat(total) +parseFloat(paymentData[i].Sird_settle_amt);
        }
        jQuery(".bal-amount-val").empty();
        jQuery(".bal-amount-val").html("0");
        jQuery(".tot-paid-amount-val").empty();
        jQuery(".tot-paid-amount-val").html(total);
        jQuery("#Sird_settle_amt").val("");
    }
}