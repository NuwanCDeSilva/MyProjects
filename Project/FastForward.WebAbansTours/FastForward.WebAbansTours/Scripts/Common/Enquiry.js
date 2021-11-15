jQuery(document).ready(function () {

    jQuery('#GCE_EXPECT_DT').datepicker({ dateFormat: "dd/M/yy" })
    jQuery('#GCS_EXP_DT').datepicker({ dateFormat: "dd/M/yy" })
    jQuery('#GCS_DROP_DT').datepicker({ dateFormat: "dd/M/yy" })
    jQuery('#GCE_FLY_DATE').datetimepicker({ minDate: new Date(), dateFormat: "dd/M/yy", timeFormat: "HH:mm" });
    jQuery('#GCS_EXP_TIME').timepicker({
        format: ' hh:ii'
    });
    jQuery('#GCS_DROP_TIME').timepicker({
        format: ' hh:ii'
    });
 
    //$(function () {
    //    $('#fileUpload').fileupload({
    //        dataType: 'json',
    //        done: function (e, data) {
    //            $.each(data.result.files, function (index, file) {
    //                $('<p/>').text(file.name).appendTo(document.body);
    //            });
    //        }
    //    });
    //});
  
    jQuery(".pick-town-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Town", "District", "Province", "Code"];
        field = "pickTownSrch"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".drop-town-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Town", "District", "Province", "Code"];
        field = "dropTownSrch"
        var x = new CommonSearch(headerKeys, field);
    });
 
    var serviceSel=null;
    $('.select-to-pending-cancel').click(function () {
     
        $.ajax({
            url: "/EnquiryManagement/GetEnqData",
            type: 'GET',
            cache: false,
            data: { id: $(this).data("list-id"), status: $(this).data("list-status"), reason: $(this).data("list-reason"), cuscd: $(this).data("list-cuscd"), mobile: $(this).data("list-mob"), nic: $(this).data("list-nic"), name: $(this).data("list-name"), add1: $(this).data("list-add1"), add2: $(this).data("list-add2"), email: $(this).data("list-email"), gcuscd: $(this).data("list-concd"), gname: $(this).data("list-gname"), gmob: $(this).data("list-gmob"), gemail: $(this).data("list-gemail"), flyno: $(this).data("list-flyno"), flydate: $(this).data("list-flydate") },
            success: function (result) {
                jQuery('div.button-cls').empty();
                if (result != null)
                {
                    jQuery("#GCE_CUS_CD").val(result.cuscd);
                    jQuery("#GCE_MOB").val(result.mobile);
                    jQuery("#GCE_NIC").val(result.nic);
                    jQuery("#GCE_NAME").val(result.name);
                    jQuery("#GCE_ADD1").val(result.add1);
                    jQuery("#GCE_ADD2").val(result.add2);
                    jQuery("#GCE_EMAIL").val(result.email);
                    jQuery("#GCE_GUESS").val(result.gname);
                    jQuery("#GCE_CONT_MOB").val(result.gmob);
                    jQuery("#GCE_CONT_EMAIL").val(result.gemail);
                    jQuery("#GCE_CONT_CD").val(result.gcuscd)
                    jQuery("#job_number2").val(result.id);
                    jQuery("#job_number").val(result.id);
                    jQuery("#GCE_FLY_NO").val(result.flyno);
                    jQuery("#GCE_FLY_DATE").val(my_date_format_with_time(convertDate(result.flydate)));
                    //hidden Enq id
                    jQuery("#GCE_ENQ_ID").val(result.id);

                    // enq service data
                    jQuery("#GCS_COMMENT").val(result.data[0].GCS_COMMENT);
                    jQuery("#GCS_DROP").val(result.data[0].GCS_DROP);
                    jQuery("#GCS_DROP_TN").val(result.data[0].GCS_DROP_TN);
                    console.log(result.data[0].GCS_FAC);
                    jQuery("#GCS_FAC").val(result.data[0].GCS_FAC);
                    serviceSel = result.data[0].GCS_SERVICE;
                    loadService();
                    
                    jQuery("#GCS_UNITS").val(result.data[0].GCS_UNITS);
                    jQuery("#GCS_EXP_DT").val(my_date_format_with_time(convertDate(result.data[0].GCS_EXP_DT)));
                    jQuery("#GCS_EXP_TIME").val(my_date_format_with_time(convertDate(result.data[0].GCS_EXP_TIME)));
                    jQuery("#GCS_DROP_DT").val(my_date_format_with_time(convertDate(result.data[0].GCS_DROP_DT)));
                    jQuery("#GCS_DROP_TIME").val(my_date_format_with_time(convertDate(result.data[0].GCS_DROP_TIME)));
                  
                    jQuery("#GCS_PICK_FRM").val(result.data[0].GCS_PICK_FRM)
                    jQuery("#GCS_PICK_TN").val(result.data[0].GCS_PICK_TN);
                    jQuery("#GCS_SERVICE").val(result.data[0].GCS_SERVICE);
                    jQuery("#GCS_SER_PROVIDER").val(result.data[0].GCS_SER_PROVIDER);
                    jQuery("#GCS_VEH_TP").val(result.data[0].GCS_VEH_TP);

                    if (result.data[0].GCS_FAC == "HACCO")
                    {
                        jQuery(".trans-det-pnl #view2").slideUp();
                        jQuery(".trans-detail-panel-set").hide();
                    }

                  

                    jQuery("#job_number2").focus();
                    jQuery('div.button-cls').append('  <input type="button" value="Clear" class="btn btn-default btn-default-style btn-Enq-clear-data" />'+
           ' <input type="button" value="Update" class="btn btn-default btn-default-style btn-Enq-Create" /> <input type="button" value="Pending Cancel" class="btn btn-default btn-default-style Pending-Cancel" data-toggle="modal" data-target="#enqmodal" />');
                    
                    
                }
             
                jQuery(".Pending-Cancel").click(function () {
                    jQuery('div.enq-pen-can').empty();
                    jQuery('div.enq-pen-can').append(
                        '<form method="post" action="/EnquiryManagement/UpdateEnq" class="form-horizontal pending-cancel-frm">' +
                        '<div class="form-group">'+
                     
                        '<div class="col-md-12">' + '<input type="text" value=' + result.id + ' name="enqid" readonly />' +
                       '</div>'
                        + '</div>' +
                        '<div class="form-group">'+
 
                        '<div class="col-md-8">' + '<textarea name="reason" id="reason" required placeholder="Enter Reason"></textarea>' +
                       '</div>'
                        + '</div>' +
                         '<div class="form-group">'
                        + ' <input type="submit" class="btn btn-default btn-default-style btn-sm Pending-Cancel" value="Pending Cancel">'
                       + '</div>' + '</form>'
                        );
                   
                   
                });
                jQuery(".btn-Enq-clear-data").click(function (evt) {
                    evt.preventDefault();
                    Lobibox.confirm({
                        msg: "Do you want to clear data ?",
                        callback: function ($this, type, ev) {
                            if (type == "yes") {
                                document.getElementById("enq-crte-frm").reset();
                                jQuery(".partialView_enq").empty();
                                jQuery('.Pending-Cancel').hide();

                                return false;
                            }
                        }
                    });

                });
                jQuery('.btn-Enq-Create').click(function (e) {

                    var currentdate = new Date();
                    var current =  new Date(currentdate);
                    var pkfrm = new Date(jQuery("#GCS_EXP_DT").val());
                    var pkto = new Date(jQuery("#GCS_DROP_DT").val());
                    var cuscd = jQuery("#GCE_CUS_CD").val();
                    var pax = jQuery("#GCE_NO_PASS").val();
                    var contact = jQuery("#GCE_CONT_MOB").val();
                    var units = jQuery("#GCS_UNITS").val();
                    var servie = jQuery("#GCS_SERVICE").val();
                    var gussname = jQuery("#GCE_GUESS").val();
                    var country = jQuery("#GCE_FRM_CONTRY").val();
                    var endcountry = jQuery("#GCE_DEST_CONTRY").val();
                    var serby = jQuery("#GCS_SER_PROVIDER").val();
                    var comment = jQuery("#GCS_COMMENT").val();
                    var picktwn = jQuery("#GCE_FRM_TN").val();
                    var drptwn = jQuery("#GCE_TO_TN").val();
                    if (cuscd == "") {
                        setInfoMsg("Custormer Code Required");
                    } else {
                        if ((jQuery("#GCS_FAC").val() != "TNSPT") && (country == "" | endcountry == "" | pax == "")) {
                            setInfoMsg("Country or Pax Required");
                        } else {
                            if (gussname == "" ) {
                                setInfoMsg("Guess Contact Details  Required");
                            } else {
                                if (units == "" | servie == null | serby == "") {
                                    setInfoMsg("Service Details  Required");
                                } else {
                                    if (jQuery("#GCS_FAC").val() != "TNSPT") {

                                        Lobibox.confirm({
                                            msg: "Do you want to Save Enquiry ?",
                                            callback: function ($this, type, ev) {
                                                if (type == "yes") {
                                                    var formdata = jQuery("#enq-crte-frm").serialize();
                                                  //  $("#enq-crte-frm").submit();
                                                    // setInfoMsg("Submit Complete");
                                                    jQuery.ajax({
                                                        type: "GET",
                                                        url: "/EnquiryManagement/SaveToursEnqDataNew",
                                                        data: formdata,
                                                        contentType: "application/json;charset=utf-8",
                                                        dataType: "json",
                                                        success: function (result) {
                                                            if (result.login == true) {
                                                                if (result.success == true) {
                                                                    setSuccesssMsg(result.msg);
                                                                    $("#img-upload-frm").submit();
                                                                  
                                                                 
                                                                    //document.getElementById("enq-crte-frm").reset();
                                                                    //jQuery(".partialView_enq").empty();
                                                                    //jQuery('.Pending-Cancel').hide();
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
                                    } else {

                                        if ((pkfrm != "undefined NaN, NaN" && pkto != "undefined NaN, NaN") && picktwn != "" && drptwn != "") {

                                            if ((pkfrm > pkto)) {
                                                setInfoMsg("Date Range Wrong!!");
                                            } else {

                                                Lobibox.confirm({
                                                    msg: "Do you want to continue process?",
                                                    callback: function ($this, type, ev) {
                                                        if (type == "yes") {
                                                            var formdata = jQuery("#enq-crte-frm").serialize();
                                                            jQuery.ajax({
                                                                type: "GET",
                                                                url: "/EnquiryManagement/SaveToursEnqDataNew",
                                                                data: formdata,
                                                                contentType: "application/json;charset=utf-8",
                                                                dataType: "json",
                                                                success: function (result) {
                                                                    if (result.login == true) {
                                                                        if (result.success == true) {
                                                                          
                                                                            setSuccesssMsg(result.msg);
                                                                            $("#img-upload-frm").submit();
                                                                                //document.getElementById("enq-crte-frm").reset();
                                                                                //jQuery(".partialView_enq").empty();
                                                                                //jQuery('.Pending-Cancel').hide();
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
                                            setInfoMsg("Transport Details Not Fill");
                                        }

                                    }
                                }
                            }
                        }

                    }


                    //document.getElementById("driverall-frm").submit();

                });
            }
        });
        return false;
    });
    $('#job_number2').focus(function () {
        loadEnqData(jQuery('#job_number2').val())
    })
    function loadEnqData(enqId) {
        jQuery.ajax({
            type: "GET",
            url: "/EnquiryManagement/getEnquiryData",
            contentType: "application/json;charset=utf-8",
            data: { enqId: enqId },
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        setEnqData(result.data);
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
    function setEnqData(data) {
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
            jQuery("#GCE_FRM_CONTRY").val(data.GCE_FRM_CONTRY);
            jQuery("#GCE_DEST_CONTRY").val(data.GCE_DEST_CONTRY);
            //jQuery("#job_number").val(data.GCS_FAC);
            //jQuery("#GCS_SERVICE").val(data.GCS_SERVICE);
            //jQuery("#GCS_SER_PROVIDER").val(data.GCS_SER_PROVIDER);

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
            if (data.CHARGER_VALUE.length > 0) {
                for (i = 0; i < data.CHARGER_VALUE.length; i++) {
                    jQuery('.charg-enq-tbl').append('<tr class="new-row">' +
                    '<td>' + data.CHARGER_VALUE[i].Sad_itm_cd + '</td>' +
                    '<td class="text-left-align">' + addCommas(data.CHARGER_VALUE[i].Sad_unit_rt) + '</td>' +
                    '<td class="text-left-align">' + data.CHARGER_VALUE[i].Sad_disc_rt + '</td>' +
                    '<td class="text-left-align">' + addCommas(data.CHARGER_VALUE[i].Sad_tot_amt) + '</td>' +
                    '</tr>');
                }
            }

        }
    }
    $('#GCE_MOB').focusout(function () {
        var str = $(this).val();
        var numRange = /^[0-9-+]+$/;
        if (!numRange.test(str)) {
            setInfoMsg('Please enter a valid Contact !!!');
            $(this).val('');
        }
    });
    $('#GCE_CONT_MOB').focusout(function () {
        var str = $(this).val();
        var numRange = /^[0-9-+]+$/;
        if (!numRange.test(str)) {
            setInfoMsg('Please enter a valid Contact !!!');
            $(this).val('');
        }
    });

    jQuery(".cus-cd-search2").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Name", "Nic", "Mobile", "Br No"];
        field = "cusCodeEnq"
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery(".guss-cus-cd-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Name", "Nic", "Mobile", "Br No"];
        field = "guesscusCodeEnq"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".cus-country-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description", "Region Code", "Capital"];
        field = "Country"
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery(".cus-des-country-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description", "Region Code", "Capital"];
        field = "DCountry"
        var x = new CommonSearch(headerKeys, field);
    });

    function setFieldValue(data, group, local) {
        if (data != "" && local) {
            jQuery("#GCE_MOB").val(data.Mbe_mob);
            jQuery("#GCE_NIC").val(data.Mbe_nic);
            jQuery("#GCE_NAME").val(data.MBE_TIT+ " " + data.Mbe_name);
            jQuery("#GCE_ADD1").val(data.Mbe_add1);
            jQuery("#GCE_ADD2").val(data.Mbe_add2);
            jQuery("#GCE_EMAIL").val(data.Mbe_email);
           // jQuery("#GCE_GUESS").val(data.Mbe_name);
           // jQuery("#GCE_CONT_MOB").val(data.Mbe_mob);
          //  jQuery("#GCE_CONT_EMAIL").val(data.Mbe_email);
          //  jQuery("#GCE_CONT_CD").val(jQuery("#GCE_CUS_CD").val())
            type = "local";
            jQuery(".txt-type").val("local");

        }
        if (data != "" && group) {
            jQuery("#GCE_MOB").val(data.Mbg_mob);
            jQuery("#GCE_NIC").val(data.Mbg_nic);
            jQuery("#GCE_NAME").val(data._mbg_tit + " " + data.Mbg_name);
            jQuery("#GCE_ADD1").val(data.Mbg_add1);
            jQuery("#GCE_ADD2").val(data.Mbg_add2);
            jQuery("#GCE_EMAIL").val(data.Mbe_email);
           // jQuery("#GCE_GUESS").val(data.Mbg_name);
           // jQuery("#GCE_CONT_MOB").val(data.Mbg_mob);
           // jQuery("#GCE_CONT_EMAIL").val(data.Mbg_email);
            //jQuery("#GCE_CONT_CD").val(jQuery("#GCE_CUS_CD").val())
            type = "group";
            jQuery(".txt-type").val("group");

        }


    }
    loadExecutive();
    function loadExecutive() {
        jQuery.ajax({
            type: "GET",
            url: "/EnquiryManagement/LoadExecutive",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("GCE_EX_CD");
                        jQuery("#GCE_EX_CD").empty();
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
    jQuery("form.frm-cust-det #GCE_CUS_CD").focusout(function () {
        codeFocusOut();
    });
    jQuery("form.frm-cust-det #GCE_CONT_CD").focusout(function () {
        codeFocusOut2();
    });
    function codeFocusOut() {
        if (jQuery("form.frm-cust-det #GCE_CUS_CD").val() != "") {

            jQuery.ajax({
                type: "GET",
                url: "/DataEntry/cusCodeTextChanged",
                data: { val: jQuery("form.frm-cust-det #GCE_CUS_CD").val() },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
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
                        }
                    } else {
                        Logout();
                    }

                }

            });
        }
    }
    function codeFocusOut2() {
        if (jQuery("form.frm-cust-det #GCE_CONT_CD").val() != "") {

            jQuery.ajax({
                type: "GET",
                url: "/DataEntry/cusCodeTextChanged",
                data: { val: jQuery("form.frm-cust-det #GCE_CONT_CD").val() },
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
    // Get Enq data for Nic No
    $('.history-trans-enq-data-for-nic').click(function () {
        if (jQuery("#GCE_NIC").val() == "") {
            setInfoMsg("Please Select NIC");
        } else {
            var headerKeys = Array()
            data = jQuery("#GCE_NIC").val();
            headerKeys = ["Row", "Enq ID", "Reference", "Type", "Code", "Name", "NIC", "Address"];
            field = "EnqByNic"
            var x = new CommonSearch(headerKeys, field, data);
        }


        return false;
    });

  
   
    jQuery("#GCE_TO_TN").focusout(function () {
        getDropTown(jQuery(this).val());
    })
    function getDropTown(town) {
        if (town != "") {
            jQuery.ajax({
                type: "GET",
                url: "/DataEntry/preTownTextChanged",
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
  
    // clear Enq

    jQuery(".btn-Enq-clear-data").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to clear data ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    document.getElementById("enq-crte-frm").reset();
                    jQuery(".partialView_enq").empty();


                    return false;
                }
            }
        });

    });


    //jQuery('.getreq').focus(function () {
    //    loadService()
    //});


    function loadService() {
        $.ajax({
            url: "/EnquiryManagement/RequiredType",
            type: 'GET',
            cache: false,
            data: { fac: jQuery("#GCS_FAC").val() },
            success: function (result) {
                if (result != null) {
                    if (result.login == true) {
                        if (result.success == true) {
                            var select = document.getElementById("GCS_SERVICE");
                            jQuery("#GCS_SERVICE").empty();
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
                            if (serviceSel != null) {
                                jQuery('.getreq').val(serviceSel);
                                serviceSel = null;
                            }
                        } else {
                            setError(result.msg);
                        }
                    } else {
                        Logout();
                    }
                }
            }
        });
    }

    if (jQuery(".frm-cust-det #GCS_FAC").length > 0) {
        loadRequiredType();
    }


$("#GCE_NO_PASS").focusout(function () {
    var val = jQuery("#GCE_NO_PASS").val();
    jQuery("#GCS_UNITS").val(val);

})
function validatePhone(txtPhone) {

    var filter = /^[0-9-+]+$/;
    if (filter.test(txtPhone) && txtPhone.length < 12) {
        return true;
    }
    else {
        return false;
    }
}
/// Nic Validation


$('#GCE_NIC').focusout(function () {
    var str = $(this).val();
    if (str.length == 10 | str.length == 12) {
        if (str.length == 10) {
            var cnic_no_regex = /^[0-9+]{9}[V|v|x|X]$/;
            var cond = cnic_no_regex.test(str);
            if (cond == false) {
                setInfoMsg('Invalid NIC Number');
                $(this).val('');
            }
        } else {
            if (str.length == 12) {
                var cnic_no_regex = /^[0-9]{12}$/;
                var cond = cnic_no_regex.test(str);
                if (cond == false) {
                    setInfoMsg('Invalid NIC Number');
                    $(this).val('');
                }
            }
        }
    } else
    {
        setInfoMsg('Invalid NIC Number');
        $(this).val('');
    }
   
});
$('#Mbe_nic').focusout(function () {
    var str = $(this).val();
    if (str.length == 10 | str.length == 12) {
        if (str.length == 10) {
            var cnic_no_regex = /^[0-9+]{9}[V|v|x|X]$/;
            var cond = cnic_no_regex.test(str);
            if (cond == false) {
                setInfoMsg('Invalid NIC Number');
                $(this).val('');
            }
        } else {
            if (str.length == 12) {
                var cnic_no_regex = /^[0-9]{12}$/;
                var cond = cnic_no_regex.test(str);
                if (cond == false) {
                    setInfoMsg('Invalid NIC Number');
                    $(this).val('');
                }
            }
        }
    } else {
        setInfoMsg('Invalid NIC Number');
        $(this).val('');
    }

});
$('#GCE_NO_PASS').focusout(function () {
    var str = $(this).val();
    var numRange = /^[0-9+]+$/;
    if (!numRange.test(str)) {
        setInfoMsg('Please enter a valid Pax !!!');
        $(this).val('');
    }
});

$('#GCE_EMAIL').focusout(function () {
    var str = $(this).val();
    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
    if (!emailReg.test(str)) {
        setInfoMsg('Please enter a valid email address !!!');
        $(this).val('');
    }
});
$('#GCE_CONT_EMAIL').focusout(function () {
    var str = $(this).val();
    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
    if (!emailReg.test(str)) {
        setInfoMsg('Please enter a valid email address !!!');
        $(this).val('');
    }
});

$('#GCE_CONT_MOB').focusout(function () {
    var str = $(this).val();
    var numRange = /^[0-9-+]+$/;
    if (!numRange.test(str)) {
        setInfoMsg('Please enter a valid Contact !!!');
        $(this).val('');
    }
});

$('#GCS_EXP_DT').focusout(function () {
    var str = $(this).val();
    if ($.datepicker.formatDate('MM dd, yy', new Date(jQuery("#GCS_EXP_DT").val())) == 'undefined NaN, NaN' && jQuery("#GCS_EXP_DT").val()!='') {
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
$('#GCS_DROP_DT').focusout(function () {
    var str = $(this).val();
    if ($.datepicker.formatDate('MM dd, yy', new Date(jQuery("#GCS_DROP_DT").val())) == 'undefined NaN, NaN' && jQuery("#GCS_DROP_DT").val() != '') {
        setInfoMsg('Please enter a valid date !!!');
        $(this).val('');
    }
});


   
//if (jQuery("#GCS_FAC").val() != "TNSPT") {
//        jQuery(".trans-det-pnl #view2").slideUp();
//        jQuery(".trans-detail-panel-set").hide();
//    }

jQuery("#GCS_FAC").change(function () {
    if (jQuery(this).val() == "TNSPT" || jQuery(this).val() == "TPACK") {
        jQuery(".trans-detail-panel-set").show();
        jQuery(".trans-det-pnl #view2").slideDown();
        jQuery("#GCE_FRM_CONTRY").val('');
        jQuery("#GCE_DEST_CONTRY").val('');
    } else {
        jQuery(".trans-det-pnl #view2").slideUp();
        jQuery(".trans-detail-panel-set").hide();
    }
    loadService();
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
jQuery('.btn-Enq-Create').click(function (e)
{

    var currentdate = new Date();
    var current = $.datepicker.formatDate('MM dd, yy', new Date(currentdate));
    var pkfrm = $.datepicker.formatDate('MM dd, yy', new Date(jQuery("#GCS_EXP_DT").val()));
    var pkto = $.datepicker.formatDate('MM dd, yy', new Date(jQuery("#GCS_DROP_DT").val()));
    var cuscd = jQuery("#GCE_CUS_CD").val();
    var pax = jQuery("#GCE_NO_PASS").val();
    var contact = jQuery("#GCE_CONT_MOB").val();
    var units = jQuery("#GCS_UNITS").val();
    var servie = jQuery("#GCS_SERVICE").val();
    var gussname = jQuery("#GCE_GUESS").val();
    var country = jQuery("#GCE_FRM_CONTRY").val();
    var endcountry = jQuery("#GCE_DEST_CONTRY").val();
    var serby=jQuery("#GCS_SER_PROVIDER").val();
    var comment=jQuery("#GCS_COMMENT").val();
    var picktwn=jQuery("#GCE_FRM_TN").val();
    var drptwn=jQuery("#GCE_TO_TN").val();

    if (cuscd == "")
    {
        setInfoMsg("Custormer Code Required");
    } else {
        if ((jQuery("#GCS_FAC").val() != "TNSPT") && (country == "" | endcountry == "" | pax == ""))
        {
            setInfoMsg("Country or Pax Required");
        } else {
            if (gussname == "")
            {
                setInfoMsg("Guess Contact Details  Required");
            } else {
                if (units == "" || servie == "")
                {
                    if (servie == "")
                    {
                        setInfoMsg("Please select service.");
                    }else{
                        setInfoMsg("Service Details Required");
                    }
                    
                } else {
                    if (jQuery("#GCS_FAC").val() != "TNSPT")
                    {
                       
                        Lobibox.confirm({
                            msg: "Do you want to continue process?",
                            callback: function ($this, type, ev) {
                                if (type == "yes") {
                                    var formdata = jQuery("#enq-crte-frm").serialize();
                                    //  $("#enq-crte-frm").submit();
                                    // setInfoMsg("Submit Complete");
                                    jQuery.ajax({
                                        type: "GET",
                                        url: "/EnquiryManagement/SaveToursEnqDataNew",
                                        data: formdata,
                                        contentType: "application/json;charset=utf-8",
                                        dataType: "json",
                                        success: function (result) {
                                            if (result.login == true) {
                                                if (result.success == true) {
                                                   
                                                    setSuccesssMsg(result.msg);
                                                    $("#img-upload-frm").submit();
                                                    //document.getElementById("enq-crte-frm").reset();
                                                    //jQuery(".partialView_enq").empty();
                                                    //jQuery('.Pending-Cancel').hide();
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
                    } else {
                       
                        if ((pkfrm != "undefined NaN, NaN" && pkto != "undefined NaN, NaN") && picktwn != "" && drptwn!="")
                        {
                            
                            if ( (pkfrm > pkto | pkfrm < current | pkto < current))
                            {
                                setInfoMsg("Date Range Wrong!!");
                            } else {
                               
                                Lobibox.confirm({
                                    msg: "Do you want to continue process?",
                                    callback: function ($this, type, ev) {
                                        var formdata = jQuery("#enq-crte-frm").serialize();
                                        if (type == "yes") {
                                            jQuery.ajax({
                                                type: "GET",
                                                url: "/EnquiryManagement/SaveToursEnqDataNew",
                                                data: formdata,
                                                contentType: "application/json;charset=utf-8",
                                                dataType: "json",
                                                success: function (result) {
                                                    if (result.login == true) {
                                                        if (result.success == true) {
                                                          
                                                            setSuccesssMsg(result.msg);
                                                            $("#img-upload-frm").submit();
                                                            //document.getElementById("enq-crte-frm").reset();
                                                            //jQuery(".partialView_enq").empty();
                                                            //jQuery('.Pending-Cancel').hide();
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
                            setInfoMsg("Transport Details Not Fill");
                        }

                    }
                }
            }
        }

    }

   
    //document.getElementById("driverall-frm").submit();

});
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
    email = jQuery("#customer-crte-frm #Mbe_email").val();
    event.preventDefault();
    jQuery(this).attr("disabled", true);
    var formdata = jQuery("#customer-crte-frm").serialize();
    jQuery.ajax({
        type: 'POST',
        url: '/DataEntry/CustomerCreation',
        data: formdata,
        success: function (response) {
            if (response.success == true) {
                document.getElementById("customer-crte-frm").reset();
                fieldEnable();
                clearCustomerData();
                setSuccesssMsg(response.msg);
                jQuery(".btn-save-data").removeAttr("disabled");
                jQuery(".customer-create-popup").dialog('close');
              
                jQuery("#enq-crte-frm #GCE_MOB").val(mob);
                jQuery("#enq-crte-frm #GCE_NIC").val(nic);
                jQuery("#enq-crte-frm #GCE_NAME").val(name);
                jQuery("#enq-crte-frm #GCE_ADD1").val(add1);
                jQuery("#enq-crte-frm #GCE_ADD2").val(add2);
         
                jQuery("#enq-crte-frm #GCE_GUESS").val(name);
                jQuery("#enq-crte-frm #GCE_CONT_MOB").val(mob);
                jQuery("#enq-crte-frm #GCE_CONT_EMAIL").val(email);

                if (cuscd == "") {
                    jQuery("#enq-crte-frm #GCE_CUS_CD").val(response.cusCd);
                    jQuery("#enq-crte-frm #GCE_CONT_CD").val(response.cusCd);
                } else {
                    jQuery("#enq-crte-frm #GCE_CUS_CD").val(cuscd);
                    jQuery("#enq-crte-frm #GCE_CONT_CD").val(cuscd);
                }

            } else {
                jQuery(".btn-save-data").removeAttr("disabled");
                if (response.type == "Error") {
                    setError(response.msg);
                } else if (response.type == "Info") {
                    setInfoMsg(response.msg);
                }

            }
        }
    });
    return false;
});
function loadRequiredType() {
    jQuery.ajax({
        type: "GET",
        url: "/EnquiryManagement/Facility",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.login == true) {
                if (result.success == true) {
                    var select = document.getElementById("GCS_FAC");
                    jQuery("#GCS_FAC").empty();
                    var options = [];
                    var option = document.createElement('option');
                    if (result.data != null && result.data.length != 0)
                    {
                        for (i = 0; i < result.data.length; i++)
                        {
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

                    if (jQuery("#GCS_FAC").val() == "TNSPT" || jQuery("#GCS_FAC").val() == "TPACK") {
                        jQuery(".trans-detail-panel-set").show();
                        jQuery(".trans-det-pnl #view2").slideDown();
                        jQuery("#GCE_FRM_CONTRY").val('');
                        jQuery("#GCE_DEST_CONTRY").val('');
                    } else {
                        jQuery(".trans-det-pnl #view2").slideUp();
                        jQuery(".trans-detail-panel-set").hide();
                    }
                    loadService();
                } else {
                    setError(result.msg);
                }
            } else {
                Logout();
            }
        }

    });
}
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
                    if (result.data.length>0) {
                        setImagesValue(result.data);
                      
                    } else {
                        jQuery('.image-view-grid ').empty();
                        setInfoMsg("Never Attach Documents For This Job Number");

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
    if (data != null)
    {
      
        for (i = 0; i < data.length; i++) {
            if (ImageExist(data[i].Jbimg_img_path + data[i].Jbimg_img) == true)
            {
                var res = data[i].Jbimg_img.split(".");
              
                if (res[1] == "jpg" | res[1] == "png" | res[1] == "jpeg")
                {
                    jQuery('.image-view-grid ').append('<a href="' + data[i].Jbimg_img_path + data[i].Jbimg_img + '" target="_blank"> <img src="' + data[i].Jbimg_img_path + data[i].Jbimg_img + '" class="upload-images col-md-3" title="Search"></a>');
                } else
                {
                    jQuery('.image-view-grid ').append('<a href="' + data[i].Jbimg_img_path + data[i].Jbimg_img + '" target="_blank">Document '+i+1+'</a>');
                }
              
            } 
          

        }
    } 
}
jQuery('.image-submit').click(function () {
    $("#img-upload-frm").submit();
});
function ImageExist(url) {
    var http = new XMLHttpRequest();
    http.open('GET', url, false);
    http.send();
    return http.status != 404;
}
jQuery("#GCE_EXPECT_DT").focusout(function () {
    if (ValidDate(jQuery(this).val()) == "false" && jQuery(this).val() != "") {
        setInfoMsg("Please enter valid date.");
        jQuery(this).val("");
    }
});
jQuery("#GCE_CONT_MOB").focusout(function () {
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
                                setFieldValueCus(result.data, false, true);
                            }
                            if (typeof (result.group) != "undefined") {
                                setFieldValueCus(result.data, true, false);
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
jQuery("#Mbe_nic").focusout(function () {
    loadDataFromNic();
});
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
                            setFieldValueCus(result.data, false, true);
                        }
                        if (typeof (result.group) != "undefined") {
                            setFieldValueCus(result.data, true, false);
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
jQuery("#Mbe_br_number").focusout(function () {
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
                                setFieldValueCus(result.data, false, true);
                            }
                            if (typeof (result.group) != "undefined") {
                                setFieldValueCus(result.data, true, false);
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

function setFieldValueCus(data, group, local) {
    if (data != "" && local) {
        jQuery("#Mbe_address").val(data.Mbe_add1);
        jQuery("#Mbe_br_number").val(data.Mbe_br_no);
        jQuery("#GCE_CONT_CD").val(data.Mbe_cd);
        jQuery("#GCE_CONT_EMAIL").val(data.Mbe_email);
        jQuery("#GCE_CONT_MOB").val(data.Mbe_mob);
        jQuery("#GCE_GUESS").val(data.Mbe_name);
        jQuery("#Mbe_nic").val(data.Mbe_nic);
        jQuery("#Mbe_pp_number").val(data.Mbe_pp_no);

       
    }
    if (data != "" && group) {
        jQuery("#Mbe_address").val(data.Mbe_add1);
        jQuery("#Mbe_br_number").val(data.Mbe_br_no);
        jQuery("#GCE_CONT_CD").val(data.Mbe_cd);
        jQuery("#GCE_CONT_EMAIL").val(data.Mbe_email);
        jQuery("#GCE_CONT_MOB").val(data.Mbe_mob);
        jQuery("#GCE_GUESS").val(data.Mbe_name);
        jQuery("#Mbe_br_number").val(data.Mbe_nic);
        jQuery("#Mbe_pp_number").val(data.Mbe_pp_no);
    }


}
});