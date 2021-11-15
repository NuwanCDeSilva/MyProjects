jQuery(document).ready(function () {


    jQuery(".reg-no-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Reg No", "Vehicle Type", "Model", "Owner"];
        field = "fleet"
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery(".veh-no-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Reg No", "Vehicle Type", "Owner"];
        field = "driverall"
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery(".driver-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Epf No", "First Name", "Last Name"];
        field = "employeedet"
        var x = new CommonSearch(headerKeys, field);
    });

    //inv print
    jQuery(".btn-fleet-Invoice").unbind().click(function () {
        if (jQuery("#MSTF_REGNO").val() != "") {
            jQuery('#fleetinvoice').modal({
                keyboard: false,
                backdrop: 'static'
            }, 'show');
            jQuery(".btn-get-fleet-print").click(function () {
                    window.open(
                          "/FleetDefinition/FleetInvoicePrint?fleet=" + jQuery("#MSTF_REGNO").val() + "&fromdate=" + jQuery("#fdate").val() + "&todate=" + jQuery("#tdate").val(),
                          '_blank' // <- This is what makes it open in a new window.
                      );
            });


        } else {
            setInfoMsg("Please enter Vehicle Registration Code..");
        }

    });

    
    jQuery("#MSTF_REGNO").focusout(function () {
        if (jQuery(this).val() != "") {
            var val = jQuery(this).val();
            jQuery.ajax({
                type: "GET",
                url: "/FleetDefinition/getFleetDetails",
                contentType: "application/json;charset=utf-8",
                data: { val: jQuery(this).val() },
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.data != "") {
                                setFieldValue(result.data);
                                jQuery(".btn-fleet-Add-data").val("Update");
                                jQuery("#MSTF_REGNO").attr("readonly", true);
                               
                            } else {
                                clearForm();
                                jQuery("#MSTF_REGNO").val(val);
                                jQuery(".btn-add-data").val("Create");
                            }
                        }
                    } else {
                        Logout();
                    }
                }
            });
        }
    });
    jQuery("#MSTF_REGNO").keypress(function (e) {
        if (e.which == 13) {
            if (jQuery(this).val() != "") {
                var val = jQuery(this).val();
                jQuery.ajax({
                    type: "GET",
                    url: "/FleetDefinition/getFleetDetails",
                    contentType: "application/json;charset=utf-8",
                    data: { val: jQuery(this).val() },
                    dataType: "json",
                    success: function (result) {
                        if (result.login == true) {
                            if (result.success == true) {
                                if (result.data != "") {
                                    setFieldValue(result.data);
                                    jQuery(".btn-fleet-Add-data").val("Update");
                                    jQuery("#MSTF_REGNO").attr("readonly", true);

                                } else {
                                    clearForm();
                                    jQuery("#MSTF_REGNO").val(val);
                                    jQuery(".btn-add-data").val("Create");
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
    function setFieldValue(data) {
        if (data != "") {
            jQuery("#MSTF_REGNO").val(data.MSTF_REGNO);
            jQuery("#MSTF_ACT").val(data.MSTF_ACT);
            jQuery("#MSTF_VEH_TP").val(data.MSTF_VEH_TP);
            jQuery("#MSTF_MODEL").val(data.MSTF_MODEL);
            jQuery("#MSTF_BRD").val(data.MSTF_BRD);
            jQuery("#MSTF_ENGIN_CAP").val(data.MSTF_ENGIN_CAP);
            jQuery("#MSTF_FUAL_TP").val(data.MSTF_FUAL_TP);
            jQuery("#MSTF_NOOF_SEAT").val(data.MSTF_NOOF_SEAT);
            jQuery("#MSTF_SIPP_CD").val(data.MSTF_SIPP_CD);
            jQuery("#MSTF_OWN").val(data.MSTF_OWN);
            jQuery("#MSTF_OWN_NM").val(data.MSTF_OWN_NM);
            jQuery("#MSTF_OWN_CONT").val(data.MSTF_OWN_CONT);
            jQuery("#MSTF_OWN_EMAIL").val(data.MSTF_OWN_EMAIL);
            jQuery("#MSTF_LST_SERMET").val(data.MSTF_LST_SERMET);
            jQuery("#MSTF_COST").val(data.MSTF_COST);
            jQuery("#MSTF_REASON").val(data.MSTF_REASON);
            jQuery("#MSTF_OWN_ADD1").val(data.MSTF_OWN_ADD1);
            jQuery("#MSTF_OWN_ADD2").val(data.MSTF_OWN_ADD2);
            jQuery("#MSTF_OWN_NIC").val(data.MSTF_OWN_NIC);
            jQuery("#MSTF_PRO_MILGE").val(data.MSTF_PRO_MILGE);
            jQuery("#MSTF_ADD_FULL_DAY").val(data.MSTF_ADD_FULL_DAY);
            jQuery("#MSTF_ADD_HALF_DAY").val(data.MSTF_ADD_HALF_DAY);
            jQuery("#MSTF_ADD_AIR_RET").val(data.MSTF_ADD_AIR_RET);
            jQuery("#MSTF_CORR_AMT").val(data.MSTF_CORR_AMT);
            jQuery("#MSTF_HIRING_DEPOSITE").val(data.MSTF_HIRING_DEPOSITE);

            // hiring check 
            if (jQuery("#MSTF_REASON").val() == "Hiring") {
                jQuery(".hiring-data").show();
            } else {
                jQuery(".hiring-data").hide();
            }


            var EXP = new Date(parseInt(data.MSTF_INSU_EXP.substr(6)));
            var EXP = $.datepicker.formatDate('yy-mm-dd', EXP);
            jQuery("#MSTF_INSU_EXP").val(EXP);
            jQuery("#MSTF_INSU_COM").val(data.MSTF_INSU_COM);
            var EXP2 = new Date(parseInt(data.MSTF_REG_EXP.substr(6)));
            var EXP2 = $.datepicker.formatDate('yy-mm-dd', EXP2);
            jQuery("#MSTF_REG_EXP").val(EXP2);
            jQuery(".btn-fleet-save-data").val("Update");
            var intIsLease = Number(data.MSTF_IS_LEASE);
            if (intIsLease == 1) {
                $('input:radio[name="MSTF_IS_LEASE"][value="1"]').prop('checked', true);
            } else {
                $('input:radio[name="MSTF_IS_LEASE"][value="0"]').prop('checked', true);
            }

            var DT = new Date(parseInt(data.MSTF_DT.substr(6)));
            var dt = $.datepicker.formatDate('yy-mm-dd', DT);
            jQuery("#MSTF_DT").val(dt);
            var DT1 = new Date(parseInt(data.MSTF_FROM_DT.substr(6)));
            var dt1 = $.datepicker.formatDate('yy-mm-dd', DT1);
            jQuery("#MSTF_FROM_DT").val(dt1);
            var DT2 = new Date(parseInt(data.MSTF_TO_DT.substr(6)));
            var dt2 = $.datepicker.formatDate('yy-mm-dd', DT2);
            jQuery("#MSTF_TO_DT").val(dt2);


            jQuery("#MSTF_ST_METER").val(data.MSTF_ST_METER);
            jQuery("#MSTF_TOU_REGNO").val(data.MSTF_TOU_REGNO);
            jQuery("#MSTF_COMMENTS").val(data.MSTF_COMMENTS);

            if (data.profitCenterLstss != null) {
                jQuery('.profit-center-table .new-row').remove();
                for (i = 0; i < data.profitCenterLstss.length; i++) {
                    if (data.profitCenterLstss[i].MFA_PC != "") {
                        jQuery('.profit-center-table').append('<tr class="new-row"><td class="val-field">' + data.profitCenterLstss[i].MFA_PC + '</td><td style="text-align:center;"><img class="delete-img remove-row-cls" src="../Resources/images/Delete.png"></td></tr>');
                    }
                }
                jQuery(".delete-img.remove-row-cls").click(function () {
                    var td = jQuery(this).parent('td');
                    var value = jQuery(td).siblings("td").html();
                    jQuery.ajax({
                        type: "GET",
                        url: "/FleetDefinition/removeProfitCenter",
                        contentType: "application/json;charset=utf-8",
                        data: { val: value },
                        dataType: "json",
                        success: function (result) {

                        }
                    });

                    jQuery(td).parent('tr').remove();
                });
            }
            if (data.mstFleetDriver != null) {
                jQuery('table.vehicle-allocation-table  .new-row').remove();
                for (i = 0; i < data.mstFleetDriver.length; i++) {
                    var check = "";
                    if (data.mstFleetDriver[i].MFD_ACT == 1) {
                        check = '<input class="vehicle-activate" type="checkbox" name="active" checked>';
                    } else {
                        check = '<input class="vehicle-activate" type="checkbox" name="active">';
                    }
                    jQuery('table.vehicle-allocation-table ').append('<tr class="new-row"><td class="vehi-no">'
                        + data.mstFleetDriver[i].MFD_VEH_NO + '</td><td class="driver-name">'
                        + data.mstFleetDriver[i].MFD_DRI + '</td><td class="driver-code">'
                        + getFormatedDate(data.mstFleetDriver[i].MFD_FRM_DT) + '</td><td class="to-dte">'
                        + getFormatedDate(data.mstFleetDriver[i].MFD_TO_DT) + '</td><td class="allow-id">'
                        + data.mstFleetDriver[i].MFD_SEQ + '</td><td style="text-align:center;">' + check + '</td></tr>');//<img class="delete-img remove-vehicle-row-cls" src="../Resources/images/Delete.png">
                }

                jQuery(".vehicle-activate").change(function () {
                    var td = jQuery(this).parent('td');
                    var sib = jQuery(td).siblings("td.allow-id").html();
                    jQuery.ajax({
                        type: "GET",
                        url: "/FleetDefinition/updateVehicle",
                        contentType: "application/json;charset=utf-8",
                        data: { allowId: sib, activate: jQuery(this).is(':checked') },
                        dataType: "json",
                        success: function (result) {

                        }
                    });

                });
            }

        }

    }

    //uploads doc
    jQuery(".btn-fleet-docupload").click(function ()
    {
        if (jQuery("#MSTF_REGNO").val()=="")
        {
            setInfoMsg("Please Select Registration No:");
        } else {
            jQuery("#job_number").val(jQuery("#MSTF_REGNO").val());
            jQuery("#job_number2").val(jQuery("#MSTF_REGNO").val());
        }
       
    });
    jQuery(".get-images").click(function () {
        jQuery.ajax({
            type: "GET",
            url: "/ImageUpload/GetImageDetails",
            data: { enqid: jQuery("#job_number2").val() },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.data != null) {
                            setImagesValue(result.data);
                        } else {


                        }

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
    })
    function setImagesValue(data) {
        jQuery('.image-view-grid ').empty();
        if (data != null) {

            for (i = 0; i < data.length; i++) {
                jQuery('.image-view-grid ').append('<a href="' + data[i].Jbimg_img_path + data[i].Jbimg_img + '" target="_blank"> <img src="' + data[i].Jbimg_img_path + data[i].Jbimg_img + '" class="upload-images col-md-3" title="Search"></a>');

            }
        }
    }
    jQuery("#MFD_VEH_NO").focus(function () {
        if (jQuery(this).val() != "") {
            var val = jQuery(this).val();
            jQuery.ajax({
                type: "GET",
                url: "/DriverAllocation/getDriverAllocDetailsByID",
                contentType: "application/json;charset=utf-8",
                data: { val: jQuery(this).val() },
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.data != "") {

                                setFieldValue2(result.data);

                                // jQuery(".btn-add-data").val("Update");

                            } else {
                                // clearForm();
                                jQuery("#MFD_VEH_NO").val(val);

                                //jQuery(".btn-add-data").val("Create");
                            }
                        }
                    } else {
                        Logout();
                    }
                }
            });
        }
    });

    function setFieldValue2(data) {
        if (data != "") {
            if (data != null) {
                jQuery('table.vehicle-allocation-table  .new-row').remove();
                for (i = 0; i < data.length; i++) {
                    var link = '/DriverAllocation/StatusOperationDriAlloc/' + data[i].MFD_SEQ + '?text=' + data[i].MFD_VEH_NO;
                    var link2 = '/DriverAllocation/StatusOperationDriAlloc2/' + data[i].MFD_SEQ + '?text=' + data[i].MFD_VEH_NO;
                    if (data[i].MFD_ACT == 1) {
                        jQuery('table.vehicle-allocation-table ').append('<tr class="new-row"><td class="vehi-no">'
                       + data[i].MFD_VEH_NO + '</td><td class="driver-name">'
                       + data[i].MFD_DRI + '</td><td class="driver-code">'
                       + my_date_format_with_time(convertDate(data[i].MFD_FRM_DT))+ '</td><td class="to-dte">'
                       + my_date_format_with_time(convertDate(data[i].MFD_TO_DT)) + '</td><td>'
                       + '<a class="status_alert" data-sq=' + data[i].MFD_SEQ + ' href="' + link + '&st=vehno' + '" >Active</a>' + '</td></tr>');
                    }
                    else {
                        jQuery('table.vehicle-allocation-table ').append('<tr class="new-row"><td class="vehi-no">'
                      + data[i].MFD_VEH_NO + '</td><td class="driver-name">'
                      + data[i].MFD_DRI + '</td><td class="driver-code">'
                      + my_date_format_with_time(convertDate(data[i].MFD_FRM_DT)) + '</td><td class="to-dte">'
                      + my_date_format_with_time(convertDate(data[i].MFD_TO_DT)) + '</td><td>'
                      + '<a class="status_alert2" href="' + link2 + '&st=vehno' + '">Inactive</a>' + '</td></tr>');
                    }
                }
                $(".status_alert").click(function () {
                    setInfoMsg("Inactivating...");
                });
                $(".status_alert2").click(function () {
                    setInfoMsg("Activating.....");
                });
            }
        }
    }
   
    jQuery("#MFD_DRI").focus(function () {
        if (jQuery(this).val() != "") {
            var val = jQuery(this).val();
            jQuery.ajax({
                type: "GET",
                url: "/DriverAllocation/getDriverAllocDetailsByDriver",
                contentType: "application/json;charset=utf-8",
                data: { val: jQuery(this).val() },
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.data != "") {
                                setFieldValue3(result.data);
                                // jQuery(".btn-add-data").val("Update");

                            } else {
                                // clearForm();
                                jQuery("#MFD_DRI").val(val);
                                //jQuery(".btn-add-data").val("Create");
                            }
                        }
                    } else {
                        Logout();
                    }
                }
            });
        }
    });

    function setFieldValue3(data) {
        if (data != "") {
           
            if (data != null) {
                jQuery('table.vehicle-allocation-table2  .new-row').remove();
                for (i = 0; i < data.length; i++) {
                    var link = '/DriverAllocation/StatusOperationDriAlloc/' + data[i].MFD_SEQ + '?text=' + data[i].MFD_DRI;

                    var link2 = '/DriverAllocation/StatusOperationDriAlloc2/' + data[i].MFD_SEQ + '?text=' + data[i].MFD_DRI;
                    if (data[i].MFD_ACT == 1) {
                        jQuery('table.vehicle-allocation-table2 ').append('<tr class="new-row"><td class="vehi-no">'
                             + data[i].memp_first_name + '</td><td class="driver-code">'
                            + data[i].MFD_VEH_NO + '</td><td class="driver-name">'
                            + my_date_format_with_time(convertDate(data[i].MFD_FRM_DT)) + '</td><td class="to-dte">'
                           + my_date_format_with_time(convertDate(data[i].MFD_TO_DT)) + '</td><td>'
                           + '<a class="status_alert" href="' + link + '&st=' + '" >Active</a>' + '</td></tr>');
                    } else {
                        jQuery('table.vehicle-allocation-table2 ').append('<tr class="new-row"><td class="vehi-no">'
                             + data[i].memp_first_name + '</td><td class="driver-code">'
                           + data[i].MFD_VEH_NO + '</td><td class="driver-name">'
                           + my_date_format_with_time(convertDate(data[i].MFD_FRM_DT)) + '</td><td class="to-dte">'
                        + my_date_format_with_time(convertDate(data[i].MFD_TO_DT)) + '</td><td>'
                      + '<a class="status_alert2" href="' + link2 + '&st=' + '">Inactive</a>' + '</td></tr>');
                    }
                }
                $(".status_alert").click(function () {
                    setInfoMsg("Inactivating....");
                });
                $(".status_alert2").click(function () {
                    setInfoMsg("Activating....");
                });
            }
        }
    }

    function getFormatedDate(date) {
        var dte = new Date(parseInt(date.substr(6)));
        return my_date_format(dte);
    }
    var my_date_format = function (input) {
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
    jQuery(".add-prof-cen-data").click(function (evt) {
        evt.preventDefault();
        var exists = false;
        if (jQuery("#profitcenter").val() != "") {
            jQuery('.profit-center-table tr').each(function () {
                var data = jQuery(this).find(".val-field").html();
                if (data == jQuery("#profitcenter").val()) {
                    exists = true;
                }
            });
            if (exists == false) {
                jQuery('.profit-center-table').append('<tr class="new-row"><td class="val-field">' + jQuery("#profitcenter").val() + '</td><td style="text-align:center;"><img class="delete-img remove-row-cls" src="../Resources/images/Delete.png"></td></tr>');
                exists = false;
                jQuery.ajax({
                    type: "GET",
                    url: "/FleetDefinition/addProfitCenter",
                    contentType: "application/json;charset=utf-8",
                    data: { val: jQuery("#profitcenter").val(), reg: jQuery("#MSTF_REGNO").val() },
                    dataType: "json",
                    success: function (result) {

                    }
                });
                jQuery(".delete-img.remove-row-cls").click(function () {
                    var td = jQuery(this).parent('td');
                    var value = jQuery(td).siblings("td").html();
                    jQuery.ajax({
                        type: "GET",
                        url: "/FleetDefinition/removeProfitCenter",
                        contentType: "application/json;charset=utf-8",
                        data: { val: value },
                        dataType: "json",
                        success: function (result) {

                        }
                    });

                    jQuery(td).parent('tr').remove();
                });
            }
            jQuery("#profitcenter").val("");
        }
    });
    jQuery(".btn-fleet-Print").click(function (evt) {
        if (jQuery("#MSTF_REGNO").val()=="")
        {
            setInfoMsg("Please Select Registration No!!");
        } else
        {
            var regno = jQuery("#MSTF_REGNO").val();
            var prtype = jQuery("#MSTF_REASON").val();
            window.open('/FleetDefinition/FleetAggrement?regno= ' + regno + "&prtype="+prtype, '_blank')
        }
    });
    jQuery(".btn-fleet-clear-data").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to clear data ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    document.getElementById("fleet-crte-frm").reset();
                    var td = jQuery("tr.new-row").remove();
                    jQuery(".btn-fleet-Add-data").val("Create");
                    jQuery(".vehicle-allocation-table .new-row").remove();
                    jQuery("#MSTF_REGNO").removeAttr("readonly");

                    jQuery.ajax({
                        type: 'POST',
                        url: '/FleetDefinition/removeProfitCenter',
                        success: function (response) {
                        }
                    });
                    return false;
                }
            }
        });

    });
    jQuery(".btn-drall-clear-data").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to clear data ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    document.getElementById("driverall-frm").reset();
                    jQuery(".vehicle-allocation-table .new-row").remove();
                    jQuery(".vehicle-allocation-table2 .new-row").remove();
                    jQuery(".div1").empty();
                    jQuery(".div2").empty();
                }
            }
        });

    });
    jQuery('.btn-drall-Add-data').click(function (e) {

        var currentdate = new Date();
        var current = new Date(currentdate);
        var frm =  new Date(jQuery("#MFD_FRM_DT").val());
        var to =  new Date(jQuery("#MFD_TO_DT").val());
        if ( jQuery("#MFD_TO_DT").val() != "") {

            if (new Date(jQuery("#MFD_FRM_DT").val()) > new Date(jQuery("#MFD_TO_DT").val())) {
                setInfoMsg("Selected Date Range Wrong!!!");
               
            } else {


               
                Lobibox.confirm({
                    msg: "Do you want to continue process?",
                    callback: function ($this, type, ev) {
                        if (type == "yes") {
                            var formdata = jQuery("#driverall-frm").serialize();
                            //  $("#enq-crte-frm").submit();
                            // setInfoMsg("Submit Complete");
                            jQuery.ajax({
                                type: "GET",
                                url: "/DriverAllocation/DriverAllocateNew",
                                data: formdata,
                                contentType: "application/json;charset=utf-8",
                                dataType: "json",
                                success: function (result) {
                                    if (result.login == true) {
                                        if (result.success == true) {
                                            setSuccesssMsg(result.msg);
                                            document.getElementById("driverall-frm").reset();
                                            jQuery(".vehicle-allocation-table .new-row").remove();
                                            jQuery(".vehicle-allocation-table2 .new-row").remove();
                                            jQuery(".div1").empty();
                                            jQuery(".div2").empty();
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
            setInfoMsg("To Date Required.");
        }
        //document.getElementById("driverall-frm").submit();

    });
    jQuery('.btn-fleet-Add-data').click(function (e) {
        var reg = jQuery("#MSTF_REGNO").val();
        var act = jQuery("#MSTF_ACT").val();
        var tp = jQuery("#MSTF_VEH_TP").val();
        var mod = jQuery("#MSTF_MODEL").val();
        var brd = jQuery("#MSTF_BRD").val();
        var cap = jQuery("#MSTF_ENGIN_CAP").val();
        var ftp = jQuery("#MSTF_FUAL_TP").val();
        var numseat = jQuery("#MSTF_NOOF_SEAT").val();
        var sipcd = jQuery("#MSTF_SIPP_CD").val();
        var own = jQuery("#MSTF_OWN").val();
        var ownname = jQuery("#MSTF_OWN_NM").val();
        var cont = jQuery("#MSTF_OWN_CONT").val();
        var email = jQuery("#MSTF_OWN_EMAIL").val();
        var lastsm = jQuery("#MSTF_LST_SERMET").val();
        var current = new Date();
        if (jQuery("#MFD_FRM_DT").val() != "" && jQuery("#MFD_TO_DT").val()) {
            var frm =new Date(jQuery("#MFD_FRM_DT").val());
            var to = new Date(jQuery("#MFD_TO_DT").val());
        }


        var exp = new Date(jQuery("#MSTF_INSU_EXP").val());
        var Rexp =new Date(jQuery("#MSTF_REG_EXP").val());
        var date = new Date(jQuery("#MSTF_DT").val());
        if (1>2)
        {
            setInfoMsg("Input Fields Required");
        } else {
            if (validatePhone(cont) == false) {
                setInfoMsg("Invalid Contact Number");
            } else {
                if (exp != "undefined NaN, NaN" && Rexp != "undefined NaN, NaN" && date != "undefined NaN, NaN") {
                    if ((frm != null | to != null) && (frm > to | frm < current |to < current)) {
                        setInfoMsg("Driver Allocation Date Range Wrong!!");
                    } else {
                       
                        Lobibox.confirm({
                            msg: "Do you want to continue process?",
                            callback: function ($this, type, ev) {
                                if (type == "yes") {
                                    var formdata = jQuery("#fleet-crte-frm").serialize();
                                    //  $("#enq-crte-frm").submit();
                                    // setInfoMsg("Submit Complete");
                                    jQuery.ajax({
                                        type: "GET",
                                        url: "/FleetDefinition/FleetCreationNew",
                                        data: formdata,
                                        contentType: "application/json;charset=utf-8",
                                        dataType: "json",
                                        success: function (result) {
                                            if (result.login == true) {
                                                if (result.success == true)
                                                {
                                                    setSuccesssMsg(result.msg);
                                                    document.getElementById("fleet-crte-frm").reset();
                                                    var td = jQuery("tr.new-row").remove();
                                                    jQuery(".btn-fleet-save-data").val("Create");
                                                    jQuery(".vehicle-allocation-table .new-row").remove();
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
                    setInfoMsg("Not Date Selected Or Invalid Date");
                }
            }





        }





    });
    function validatePhone(txtPhone) {

        var filter = /^[0-9-+]+$/;
        if (filter.test(txtPhone) && txtPhone.length < 12) {
            return true;
        }
        else {
            return false;
        }
    }

    /* 
    $(function() {

    $('#btnSubmit').bind('click', function(){
3
        var txtVal =  $('#txtDate').val();
4
        if(isDate(txtVal))
5
            alert('Valid Date');
6
        else
7
            alert('Invalid Date');
8
    });
9
});

    */
    jQuery(".ins-by-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description"];
        field = "insby"
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery(".profit-center-search").click(function () {
        if (jQuery("#MSTF_REGNO").val() != "") {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description", "Address", "Chanel"];
            field = "ProfitCenter"
            var x = new CommonSearch(headerKeys, field);
        } else {
            // setError("Add register no.");
            setInfoMsg('Please enter Registration Number');
        }
    });

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
    if (jQuery("#MSTF_REASON").val() == "Hiring" || jQuery("#MSTF_REASON").val() == "") {
        jQuery(".hiring-data").show();
    }
    jQuery("#MSTF_REASON").change(function () {
        if (jQuery("#MSTF_REASON").val() != "Hiring") {
            jQuery(".hiring-data").hide();
        } else {
            jQuery(".hiring-data").show();
        }
    })
});