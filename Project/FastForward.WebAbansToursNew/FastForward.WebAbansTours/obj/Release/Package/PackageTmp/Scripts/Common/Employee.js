jQuery(document).ready(function () {
    clearForm();
    if (jQuery(".cus-typ-dropdown").length > 0) {
        jQuery.ajax({
            type: "GET",
            url: "/Employee/gettitleList",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        var select = document.getElementById("MEMP_TITLE");
                        jQuery("#MEMP_TITLE").empty();
                        var options = [];
                        var option = document.createElement('option');
                        if (result.data != null && result.data.length != 0) {
                            for (var i = 0; i < result.data.length; i++) {
                                option.text = result.data[i].Mstl_desc;
                                option.value = result.data[i].Mstl_cd;
                                options.push(option.outerHTML);
                            }
                        } else {
                            option.text = "Select Title";
                            option.value = "";
                            options.push(option.outerHTML);
                        }
                        select.insertAdjacentHTML('beforeEnd', options.join('\n'));
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

    if (jQuery("#MEMP_CAT_CD").length > 0) {
        jQuery.ajax({
            type: "GET",
            url: "/Employee/getEmployeeTypeList",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        var select = document.getElementById("MEMP_CAT_CD");
                        jQuery("#MEMP_CAT_CD").empty();
                        var options = [];
                        var option = document.createElement('option');
                        if (result.data != null && result.data.length != 0) {
                            for (var i = 0; i < result.data.length; i++) {
                                option.text = result.data[i].ECM_CAT;
                                option.value = result.data[i].ECM_CAT;
                                options.push(option.outerHTML);
                            }
                        } else {
                            option.text = "Select Type";
                            option.value = "";
                            options.push(option.outerHTML);
                        }
                        select.insertAdjacentHTML('beforeEnd', options.join('\n'));
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
    if (jQuery("#MEMP_CAT_CD").val() != "DRIVER") {
        jQuery(".driver-det-pnl #view2").slideUp();
        jQuery(".driver-detail-panel-set").hide();
    }
    jQuery("#MEMP_CAT_CD").change(function () {
        if (jQuery(this).val() == "DRIVER") {
            jQuery(".driver-detail-panel-set").show();
            jQuery(".driver-det-pnl #view2").slideDown();
        } else {
            jQuery(".driver-det-pnl #view2").slideUp();
            jQuery(".driver-detail-panel-set").hide();
        }
    });
    jQuery("#MEMP_CD").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Epf No", "Category", "First Name", "Last Name","NIC"];
            field = "employee"
            var x = new CommonSearch(headerKeys, field);
        }
        if (evt.keyCode == 13) {
            empDetLoad();
        }
    });
    jQuery(".emp-cd-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Epf No", "Category", "First Name", "Last Name", "NIC"];
        field = "employee"
        var x = new CommonSearch(headerKeys, field);
    });
    
    jQuery("#profitcenter").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            if (jQuery("#MEMP_CD").val() != "") {
                var headerKeys = Array()
                headerKeys = ["Row", "Code", "Description", "Address", "Chanel"];
                field = "ProfitCenter"
                var x = new CommonSearch(headerKeys, field);
            } else {
                setInfoMsg("Add emplyee code.");
            }
        } if (evt.keyCode == 13) {

            empDetLoad();
        }
    });
    jQuery(".profit-center-search").click(function () {
        if (jQuery("#MEMP_CD").val() != "") {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description", "Address", "Chanel"];
            field = "ProfitCenter"
            var x = new CommonSearch(headerKeys, field);
        } else {
            setInfoMsg("Add emplyee code.");
        }
    });
    //check numbers and decimal  only
    jQuery('#MEMP_COST').on("input", function (event) {
        if (!jQuery.isNumeric(this.value)) {
            this.value = "";
        }
        if (parseFloat(this.value) < 0) {
            this.value = "";
        }

    });
    jQuery('#MEMP_COST').keypress(function (event) {
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
    //function setError(msg) {
    //    Lobibox.alert('error',
    //    {
    //        msg: msg
    //    });
    //}
    //function setSuccesssMsg(msg) {
    //    Lobibox.alert('success',
    //    {
    //        msg: msg
    //    });
    //}
    //function setInfoMsg(msg) {
    //    Lobibox.alert('info',
    //    {
    //        msg: msg
    //    });
    //}
    function DriverType() {
        jQuery.ajax({
            type: "GET",
            url: "/Employee/LoadDriverTypes",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        var select = document.getElementById("MEMP_ANAL2");
                        jQuery("#MEMP_ANAL2").empty();
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
    DriverType();
    function Languages() {
        jQuery.ajax({
            type: "GET",
            url: "/Employee/LoadLanguages",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        var select = document.getElementById("language");
                        jQuery("#language").empty();
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
    Languages();
    jQuery("#MEMP_CD").focusout(function () {
        empDetLoad();
    });
    function empDetLoad() {
        if (jQuery("#MEMP_CD").val() != "") {
            var val = jQuery("#MEMP_CD").val();
            jQuery.ajax({
                type: "GET",
                url: "/Employee/getEmployeeDetails",
                contentType: "application/json;charset=utf-8",
                data: { val: val },
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.data != "") {
                                //clearForm();
                                setFieldValue(result.data);
                                disabledField();
                                jQuery(".btn-emp-save-data").val("Update");
                            } else {
                                //clearForm();
                                jQuery("#MEMP_CD").val(val);
                                jQuery(".btn-emp-save-data").val("Create");
                            }
                        }
                    } else {
                        Logout();
                    }
                }
            });
        }
    }
    jQuery("#MEMP_NIC").focusout(function () {
        if (jQuery("#MEMP_NIC").val() != "") {
            empDetLoadNIC();
        }
    });
    jQuery("#MEMP_NIC").on("keydown", function (evt) {
        if (evt.keyCode == 13) {
            if (jQuery("#MEMP_NIC").val() != "") {
                empDetLoadNIC();
            }
        }
    });
    function empDetLoadNIC() {
        var attr = jQuery("#MEMP_NIC").attr('readonly');
        if (typeof attr !== typeof undefined && attr !== false) {
            // ...
        } else {
            if (jQuery("#MEMP_NIC").val() != "") {
                var val = jQuery("#MEMP_NIC").val();
                var cde = jQuery("#MEMP_CD").val();
                jQuery.ajax({
                    type: "GET",
                    url: "/Employee/getEmployeeDetailsByNic",
                    contentType: "application/json;charset=utf-8",
                    data: { Nic: val },
                    dataType: "json",
                    success: function (result) {
                        if (result.login == true) {
                            if (result.success == true) {
                                if (typeof result.data != "undefined") {
                                    if (result.data != "") {
                                        //clearForm();
                                        setFieldValue(result.data);
                                        jQuery(".btn-emp-save-data").val("Update");
                                    } else {
                                        //clearForm();
                                        jQuery("#MEMP_NIC").val(val);
                                        jQuery("#MEMP_CD").val(cde);
                                        jQuery(".btn-emp-save-data").val("Create");
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
        
    }

    function setFieldValue(data) {
        if (typeof data != "undefined") {
            jQuery("#MEMP_CD").val(data.MEMP_CD);
            jQuery("#MEMP_ACT").val(data.MEMP_ACT);
            jQuery("#MEMP_TITLE").val(data.MEMP_TITLE);
            jQuery("#MEMP_FIRST_NAME").val(data.MEMP_FIRST_NAME);
            jQuery("#MEMP_LAST_NAME").val(data.MEMP_LAST_NAME);
            jQuery("#MEMP_LIVING_ADD_1").val(data.MEMP_LIVING_ADD_1);
            jQuery("#MEMP_LIVING_ADD_2").val(data.MEMP_LIVING_ADD_2);
            jQuery("#MEMP_NIC").val(data.MEMP_NIC);
            var dob = new Date(parseInt(data.MEMP_DOB.substr(6)));
            if (my_date_format(dob) != "NaN/undefined/NaN")
                jQuery("#MEMP_DOB").val(my_date_format(dob));
            var doj = new Date(parseInt(data.MEMP_DOJ.substr(6)));
            if (my_date_format(doj) != "NaN/undefined/NaN")
                jQuery("#MEMP_DOJ").val(my_date_format(doj));
            jQuery("#MEMP_TEL_HOME_NO").val(data.MEMP_TEL_HOME_NO);
            jQuery("#MEMP_MOBI_NO").val(data.MEMP_MOBI_NO);
            jQuery("#MEMP_EMAIL").val(data.MEMP_EMAIL);
            jQuery("#MEMP_CAT_SUBCD").val(data.MEMP_CAT_SUBCD);
            jQuery("#MEMP_CAT_CD").val(data.MEMP_CAT_CD);
            jQuery("#MEMP_COST").val(data.MEMP_COST);
            if (data.MEMP_CAT_CD == "DRIVER") {
                jQuery(".driver-detail-panel-set").show();
                jQuery(".driver-det-pnl #view2").slideDown();
                jQuery("#MEMP_LIC_NO").val(data.MEMP_LIC_NO);
                var exdt = new Date(parseInt(data.MEMP_LIC_EXDT.substr(6)));
                if (getMonthAgoMonth(exdt) != "NaN/undefined/NaN")
                    jQuery("#MEMP_LIC_EXDT").val(my_date_format(exdt));
                jQuery("#MEMP_TOU_LIC").val(data.MEMP_TOU_LIC);
                if (data.MEMP_ANAL1 == "I") {
                    jQuery('.MEMP_ANAL1')[1].checked = true;
                    jQuery('.MEMP_ANAL1')[0].checked = false;
                } else if (data.MEMP_ANAL1 == "L") {
                    jQuery('.MEMP_ANAL1')[0].checked = true;
                    jQuery('.MEMP_ANAL1')[1].checked = false;
                }
            } else {
                jQuery(".driver-det-pnl #view2").slideUp();
                jQuery(".driver-detail-panel-set").hide();
                jQuery("#MEMP_LIC_NO").val("");
                jQuery("#MEMP_LIC_EXDT").val("");
                jQuery("#MEMP_TOU_LIC").val("");
            }
            //set languages
            if (data.MEMP_ANAL3 !="")
            {
                var langstring = data.MEMP_ANAL3;
                var arr = langstring.split(',');
                jQuery('.language-table .new-row').remove();
                for (k = 0; k < arr.length; k++) {
                    if (arr[k] != "")
                    {
                        jQuery('.language-table').append('<tr class="new-row"><td class="val-field">' + arr[k] + '</td><td style="text-align:center;"><img class="delete-img remove-row-cls" src="../Resources/images/Delete.png"></td></tr>');
                    }
                  
                }
            }
           

            jQuery("#MEMP_CON_PER").val(data.MEMP_CON_PER);
            jQuery("#MEMP_CON_PER_MOB").val(data.MEMP_CON_PER_MOB);
            if (data.profitCenterLst != null) {
                jQuery('.profit-center-table .new-row').remove();
                for (i = 0; i < data.profitCenterLst.length; i++) {
                    if (data.profitCenterLst[i].MPE_PC != "") {
                        jQuery('.profit-center-table').append('<tr class="new-row"><td class="val-field">' + data.profitCenterLst[i].MPE_PC + '</td><td style="text-align:center;"><img class="delete-img remove-row-cls" src="../Resources/images/Delete.png"></td></tr>');
                    }
                }
                jQuery(".delete-img.remove-row-cls").click(function () {
                    var td = jQuery(this).parent('td');
                    var value = jQuery(td).siblings("td").html();
                    jQuery.ajax({
                        type: "GET",
                        url: "/Employee/removeLanguages",
                        contentType: "application/json;charset=utf-8",
                        data: { val: value },
                        dataType: "json",
                        success: function (result) {

                        }
                    });

                    jQuery(td).parent('tr').remove();
                });
            } else {
                jQuery('.profit-center-table .new-row').remove();
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
                        + data.mstFleetDriver[i].MFD_DRI + '</td><td class="from-dte">'
                        + getFormatedDate(data.mstFleetDriver[i].MFD_FRM_DT) + '</td><td class="to-dte">'
                        + getFormatedDate(data.mstFleetDriver[i].MFD_TO_DT) + '</td><td class="allow-id">'
                        + data.mstFleetDriver[i].MFD_SEQ + '</td><td style="text-align:center;">' + check + '</td></tr>');//<img class="delete-img remove-vehicle-row-cls" src="../Resources/images/Delete.png">
                }

                jQuery(".vehicle-activate").change(function () {
                    var td = jQuery(this).parent('td');
                    var sib = jQuery(td).siblings("td.allow-id").html();
                    jQuery.ajax({
                        type: "GET",
                        url: "/Employee/updateVehicle",
                        contentType: "application/json;charset=utf-8",
                        data: { allowId: sib, activate: jQuery(this).is(':checked') },
                        dataType: "json",
                        success: function (result) {

                        }
                    });

                });
            } else {
                jQuery('table.vehicle-allocation-table  .new-row').remove();
            }
        }
        disabledField();
    }
    function disabledField() {
        jQuery("#MEMP_CD").attr('readonly', true);
        jQuery("#MEMP_NIC").attr('readonly', true);
        jQuery("#MEMP_TITLE").attr('readonly', true);
        jQuery("#MEMP_FIRST_NAME").attr('readonly', true);
        jQuery("#MEMP_LAST_NAME").attr('readonly', true);
        jQuery("#MEMP_LIVING_ADD_1").attr('readonly', true);
        jQuery("#MEMP_LIVING_ADD_2").attr('readonly', true);
        jQuery("#MEMP_DOJ").attr('readonly', true);
        jQuery("#MEMP_DOB").attr('readonly', true);
    }
    function enabledField() {
        jQuery("#MEMP_CD").removeAttr('readonly');
        jQuery("#MEMP_NIC").removeAttr('readonly');
        jQuery("#MEMP_TITLE").removeAttr('readonly');
        jQuery("#MEMP_FIRST_NAME").removeAttr('readonly');
        jQuery("#MEMP_LAST_NAME").removeAttr('readonly');
        jQuery("#MEMP_LIVING_ADD_1").removeAttr('readonly');
        jQuery("#MEMP_LIVING_ADD_2").removeAttr('readonly');
        jQuery("#MEMP_DOJ").removeAttr('readonly');
        jQuery("#MEMP_DOB").removeAttr('readonly');
    }

    jQuery('#MEMP_DOJ').datepicker({ maxDate: new Date() , dateFormat: "dd/M/yy"});
    jQuery('#MEMP_DOB').datepicker({ maxDate: new Date(), dateFormat: "dd/M/yy" });
    jQuery('#MEMP_LIC_EXDT').datepicker({ dateFormat: "dd/M/yy" });
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
                    url: "/Employee/addProfitCenter",
                    contentType: "application/json;charset=utf-8",
                    data: { val: jQuery("#profitcenter").val(), epf: jQuery("#MEMP_CD").val() },
                    dataType: "json",
                    success: function (result) {

                    }
                });
                jQuery(".delete-img.remove-row-cls").click(function () {
                    var td = jQuery(this).parent('td');
                    var value = jQuery(td).siblings("td").html();
                    jQuery.ajax({
                        type: "GET",
                        url: "/Employee/removeProfitCenter",
                        contentType: "application/json;charset=utf-8",
                        data: { val: value },
                        dataType: "json",
                        success: function (result) {
                            if (result.success == true) {
                                jQuery(td).parent('tr').remove();
                            }

                        }
                    });


                });
            }
            jQuery("#profitcenter").val("");
        } else {
            setInfoMsg("Please add profit center.");
        }
    });
    jQuery(".add-lang-data").click(function (evt) {
        evt.preventDefault();
        var exists = false;
        if (jQuery("#language").val() != "") {
            jQuery('.language-table tr').each(function () {
                var data = jQuery(this).find(".val-field").html();
                if (data == jQuery("#language").val()) {
                    exists = true;
                }
            });
            if (exists == false) {
                jQuery('.language-table').append('<tr class="new-row"><td class="val-field">' + jQuery("#language").val() + '</td><td style="text-align:center;"><img class="delete-img remove-row-cls" src="../Resources/images/Delete.png"></td></tr>');
                exists = false;
                jQuery.ajax({
                    type: "GET",
                    url: "/Employee/addLanguages",
                    contentType: "application/json;charset=utf-8",
                    data: { val: jQuery("#language").val(), epf: jQuery("#MEMP_CD").val() },
                    dataType: "json",
                    success: function (result) {

                    }
                });
                jQuery(".delete-img.remove-row-cls").click(function () {
                    var td = jQuery(this).parent('td');
                    var value = jQuery(td).siblings("td").html();
                    jQuery.ajax({
                        type: "GET",
                        url: "/Employee/removeLanguages",
                        contentType: "application/json;charset=utf-8",
                        data: { val: value },
                        dataType: "json",
                        success: function (result) {
                            if (result.success == true) {
                                jQuery(td).parent('tr').remove();
                            }

                        }
                    });


                });
            }
        } else {
            setInfoMsg("Please add Language.");
        }
    });
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

       return (day+"/"+ monthNames[monthIndex]+"/"+ year);
    };
    jQuery(".btn-emp-save-data").click(function (event) {
        jQuery(this).attr("disabled", true);
        event.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to continue ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    if (jQuery(".btn-emp-save-data").val() == "Create") {
                        var formdata = jQuery("#emply-crte-frm").serialize();
                        jQuery.ajax({
                            type: 'POST',
                            url: '/Employee/employeeCreation',
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
                                    jQuery(".btn-emp-save-data").removeAttr("disabled");
                                } else {
                                    Logout();
                                }
                            }
                        });
                        return false;
                    } else {
                        var formdata = jQuery("#emply-crte-frm").serialize();
                        jQuery.ajax({
                            type: 'POST',
                            url: '/Employee/employeeUpdate',
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
                                jQuery(".btn-emp-save-data").removeAttr("disabled");
                            }
                        });
                        return false;
                    }
                }
            }
        });

    });
    function clearForm() {
        clearSessionData();
        enabledField();
        document.getElementById("emply-crte-frm").reset();
        jQuery(".profit-center-table tr.new-row").remove();
        jQuery(".language-table tr.new-row").remove();
        jQuery(".vehicle-allocation-table  tr.new-row").remove();
        jQuery(".driver-det-pnl #view2").slideUp();
        jQuery(".driver-detail-panel-set").hide();
        jQuery(".btn-emp-save-data").val("Create");
       
    }
    function clearSessionData() {
        jQuery.ajax({
            type: 'POST',
            url: '/Employee/removeSesData',
            success: function (response) {
            }
        });
    }
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

    if (jQuery("#vehino").length > 0) {
        jQuery.ajax({
            type: "GET",
            url: "/Employee/getVehicleList",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        var select = document.getElementById("vehino");
                        jQuery("#vehino").empty();
                        var options = [];
                        var option = document.createElement('option');
                        
                        if (result.data != null && result.data.length != 0) {
                            option.text = "Select Vahicle No"
                            option.value = "";
                            options.push(option.outerHTML);
                            for (var i = 0; i < result.data.length; i++) {
                                option.text = result.data[i];
                                option.value = result.data[i];
                                options.push(option.outerHTML);
                            }
                        } else {
                            option.text = "Select Vahicle No";
                            option.value = "";
                            options.push(option.outerHTML);
                        }
                        select.insertAdjacentHTML('beforeEnd', options.join('\n'));
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
    jQuery(".txt-from-date").datepicker({ dateFormat: "dd/M/yy" });
    jQuery(".txt-to-date").datepicker({ dateFormat: "dd/M/yy" });

    function getFormatedDate(date) {
        var dte = new Date(parseInt(date.substr(6)));
        if (my_date_format(dte) != "NaN/undefined/NaN")
        return my_date_format(dte);
    }
    jQuery(".add-vehicle-data").click(function (evt) {
        evt.preventDefault();
        if (jQuery("#MEMP_CD").val() != "" && jQuery("#from-date").val() <= jQuery("#to-date").val()) {
            if (jQuery("#vehino").val() != "" && jQuery("#from-date").val() != "" && jQuery("#to-date").val() != "") {
                jQuery.ajax({
                    type: "GET",
                    url: "/Employee/addVehicleAllowcation",
                    contentType: "application/json;charset=utf-8",
                    data: { empId: jQuery("#MEMP_CD").val(), vehicle: jQuery("#vehino").val(), from_dte: jQuery("#from-date").val(), to_dte: jQuery("#to-date").val() },
                    dataType: "json",
                    success: function (result) {

                        if (result.login == true) {
                            if (result.success == true) {
                                jQuery('table.vehicle-allocation-table ').append('<tr class="new-row"><td class="vehi-no">'
                        + jQuery("#vehino").val() + '</td><td class="driver-name">'
                        + jQuery("#MEMP_CD").val() + '</td><td class="driver-code">'
                        + jQuery("#MEMP_CD").val() + '</td><td class="from-dte">'
                        + jQuery("#from-date").val() + '</td><td class="to-dte">'
                        + jQuery("#to-date").val() + '</td><td class="allow-id">'
                        + "" + '</td><td style="text-align:center;"><input class="vehicle-activate" type="checkbox" name="active" checked></td></tr>');
                                jQuery("#vehino").val("");
                                jQuery("#from-date").val("");
                                jQuery("#to-date").val("");
                                jQuery(".vehicle-activate").change(function () {
                                    var td = jQuery(this).parent('td');
                                    var sib = jQuery(td).siblings("td.allow-id").html();
                                    jQuery.ajax({
                                        type: "GET",
                                        url: "/Employee/updateVehicle",
                                        contentType: "application/json;charset=utf-8",
                                        data: { allowId: sib, activate: jQuery(this).is(':checked') },
                                        dataType: "json",
                                        success: function (result) {
                                            if (result.login == false) {
                                                Logout();
                                            }
                                        }
                                    });

                                });
                            } else {
                                if (result.type == "Info") {
                                    setInfoMsg(result.msg);
                                }
                                if (result.type == "Error") {
                                    setError(result.msg);
                                }
                            }
                        } else {
                            Logout();
                        }
                        
                    }
                });
            } else {
               
                if (jQuery("#vehino").val() == "") {
                    
                    setInfoMsg("Select vehicle number.");
                }
                else if (jQuery("#from-date").val() == "") {
                    setInfoMsg("Select from date.");
                
                } else if (jQuery("#to-date").val() == "") {
                    setInfoMsg("Select to date.");
                }
            }
        } else {
            if (jQuery("#MEMP_CAT_CD").val() != "DRIVER") {
                setInfoMsg("Employee type must be DRIVER to add driver details");
            }else if (jQuery("#MEMP_CD").val() == "") {
                setInfoMsg("Add employee code.");
            } else if (jQuery("#from-date").val() > jQuery("#to-date").val()) {
                setInfoMsg("Invalid date range selected.");
            } else {
                setInfoMsg("Please add vehicle details.");
            }
           

        }
    });
    jQuery("#MEMP_DOB,#MEMP_DOJ").focusout(function () {
        if (ValidDate(jQuery(this).val()) == "false" && jQuery(this).val() != "") {
            setInfoMsg("Please enter valid date.");
            jQuery(this).val("");
        }
    });


    jQuery('#MEMP_NIC').keyup(function () {
        this.value = this.value.toUpperCase();
    });
    if (jQuery("#MEMP_NIC").length > 0) {
        jQuery("#MEMP_NIC").focusout(function () {

            var attr = $(this).attr('readonly');
            if (typeof attr !== typeof undefined && attr !== false) {
                // ...
            } else {
                if (jQuery("#MEMP_NIC").val().length == 10 || jQuery("#MEMP_NIC").val().length == 12) {
                    if (validateNIC(jQuery("#MEMP_NIC").val())) {
                        jQuery.ajax({
                            type: "GET",
                            url: "/Employee/dobGeneration",
                            contentType: "application/json;charset=utf-8",
                            data: { nic: jQuery(this).val() },
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        if (result.dob != "") {
                                            jQuery("#MEMP_DOB").val(result.dob);
                                            jQuery("#MEMP_TITLE").val(result.title);
                                        } else {
                                            jQuery("#MEMP_NIC").val("");
                                            jQuery("#MEMP_DOB").val("");
                                        }
                                    }
                                } else {
                                    Logout();
                                }
                            }
                        });
                    } else {
                        jQuery("#MEMP_NIC").val("");
                        jQuery("#MEMP_DOB").val("");
                        setInfoMsg("Please enter valid NIC.");
                    }
                } else {
                    jQuery("#MEMP_NIC").val("");
                    jQuery("#MEMP_DOB").val("");
                    setInfoMsg("Invalid NIC number.");
                }
            }
            
        });
    } else {
        jQuery("#MEMP_NIC").val("");
        jQuery("#MEMP_DOB").val("");
        setInfoMsg("Please enter valid NIC.");
    }

    jQuery("#MEMP_TEL_HOME_NO").focusout(function () {

        if (jQuery.isNumeric(jQuery("#MEMP_TEL_HOME_NO").val())) {
            if (jQuery("#MEMP_TEL_HOME_NO").val().length === 10) {

            } else {
                setInfoMsg("Invalid mobile number.");
                jQuery("#MEMP_TEL_HOME_NO").val("");
                jQuery("#MEMP_TEL_HOME_NO").focus();
            }
        }
    });

    jQuery("#MEMP_MOBI_NO").focusout(function () {

        if (jQuery.isNumeric(jQuery("#MEMP_MOBI_NO").val())) {
            if (jQuery("#MEMP_MOBI_NO").val().length === 10) {

            } else {
                setInfoMsg("Invalid mobile number.");
                jQuery("#MEMP_MOBI_NO").val("");
                jQuery("#MEMP_MOBI_NO").focus();
            }
        }
    });

    jQuery("#MEMP_EMAIL").focusout(function () {
        if (jQuery(this).val() != "") {
            if (!isValidEmailAddress(jQuery(this).val())) {
                jQuery("#MEMP_EMAIL").val("");
                setInfoMsg("Invalid email address.");
                jQuery("#MEMP_EMAIL").focus();
            }
        }
    });
    function isValidEmailAddress(emailAddress) {
        var pattern = /^([a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+(\.[a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+)*|"((([ \t]*\r\n)?[ \t]+)?([\x01-\x08\x0b\x0c\x0e-\x1f\x7f\x21\x23-\x5b\x5d-\x7e\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|\\[\x01-\x09\x0b\x0c\x0d-\x7f\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))*(([ \t]*\r\n)?[ \t]+)?")@(([a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.)+([a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.?$/i;
        return pattern.test(emailAddress);
    };
    jQuery('#MEMP_MOBI_NO,#MEMP_TEL_HOME_NO,#MEMP_CON_PER_MOB').on('input', function (event) {
        this.value = this.value.replace(/[^0-9]/g, '');
    });
    jQuery('#MEMP_FIRST_NAME,#MEMP_LAST_NAME,#MEMP_CON_PER').on('input', function () {
        var node = jQuery(this);
        node.val(node.val().replace(/[^a-z^A-Z^ ]/g, ''));
    });
    jQuery('#MEMP_CD').on('input', function () {
        var node = jQuery(this);
        node.val(node.val().replace(/[^0-9^a-z^A-Z^\-^]/g, ''));
    });
});