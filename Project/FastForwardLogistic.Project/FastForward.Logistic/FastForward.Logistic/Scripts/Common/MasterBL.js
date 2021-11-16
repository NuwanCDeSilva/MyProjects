jQuery(document).ready(function () {

    var click_status = "N";
    var isInternalEdit = false;
    var hasMaster = false;

    jQuery('#Bl_doc_dt').val(my_date_format(new Date()));
    jQuery('#Bl_est_time_arr').val(my_date_format_tran(my_date_format_with_time(new Date()).toString()));
    jQuery('#Bl_est_time_dep').val(my_date_format_tran(my_date_format_with_time(new Date()).toString()));
    jQuery('#bl_expires_dt').val(my_date_format_tran(my_date_format_with_time(new Date()).toString()));
    jQuery('#Bl_voage_dt').val(my_date_format_tran(my_date_format_with_time(new Date()).toString()));

    jQuery('.btn-save').show();
    jQuery('.btn-add-houseblallsave').hide();
    jQuery('.btn-update-hbl').hide();
    jQuery(".btn-add-houseblall").show();
    jQuery("#contaddbtn").show();
    jQuery("#contupdatebtn").hide();
    jQuery(".btn-update-houseblall").hide();
    

    changeComponentAttributes();

    jQuery('.btn-add-container').click(function (e) {
        if (jQuery("#blct_cont_no").val() == "") {
            setInfoMsg("Please Enter Container No");
            return;
        }
        if (jQuery("#blct_con_tp").val() == "") {
            setInfoMsg("Please Enter Container Type");
            return;
        }
        if (jQuery("#blct_seal_no").val() == "") {
            setInfoMsg("Please Enter Seal No");
            return;
        }
        Lobibox.confirm({
            msg: "Do you want to add container?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    jQuery.ajax({
                        type: "GET",
                        url: "/MasterBL/AddContainer",
                        data: { ContainerCode: jQuery("#blct_cont_no").val(), ContainerType: jQuery("#blct_con_tp").val(), Seal: jQuery("#blct_seal_no").val(), pack: jQuery("#blct_pack").val(), fullemp: jQuery("#blct_fully_empty").val() },
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    if (result.type == "Info") {
                                        setInfoMsg(result.msg);
                                        return;
                                    }
                                    SetContainerDataList(result.data);
                                    jQuery("#blct_cont_no").val("");
                                    jQuery("#blct_seal_no").val("");
                                    jQuery('#blct_con_tp').val(""); // Line added by Chathura on 16-sep-2017
                                    jQuery("#blct_pack").val("");
                                    jQuery("#blct_fully_empty").val("");
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

    function SetContainerDataList(data) {
        jQuery('.tbl-container .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {
                jQuery('.tbl-container').append('<tr class="new-row">' +
                      '<td>' + data[i].Blct_cont_no + '</td>' +
                        '<td>' + data[i].Blct_seal_no + '</td>' +
                        '<td>' + data[i].blct_con_tp + '</td>' +
                         '<td>' + data[i].Blct_pack + '</td>' +
                         '<td>' + ((data[i].Blct_fully_empty == 1) ? "Yes" : "No" + '</td>') + // Added by Chathura on 15-sep-2017
                       '<td>' + ' <input type="button" class="btn btn-sm btn-ash-fullbg remove-container-list" value="Remove" />' + '</td>' +
                        '<td>' + ' <input type="button" class="btn btn-sm btn-green-fullbg data-container-list" value="Edit" />' + '</td>' +
                        '</tr>');
            }
            RemoveDetailsFunction();
        }
    }
    function RemoveDetailsFunction() {
        jQuery(".remove-container-list").unbind('click').click(function (evt) {
            evt.preventDefault();
            var td = jQuery(this).parent('td');
            var tr = jQuery(td).parent('tr');
            var Code = jQuery(tr).find('td:eq(0)').html();
            var Seal = jQuery(tr).find('td:eq(1)').html();
            var Type = jQuery(tr).find('td:eq(2)').html();
            var Packs = jQuery(tr).find('td:eq(3)').html();
            var fulemp = jQuery(tr).find('td:eq(4)').html(); // Added by Chathura on 16-sep-2017

            jQuery("#blct_cont_no").val(Code);
            jQuery("#blct_seal_no").val(Seal);
            jQuery("#blct_con_tp").val(Type);
            jQuery("#blct_pack").val(Packs);
            jQuery("#blct_fully_empty").val((fulemp == "Yes") ? 1 : 0); // Added by Chathura on 16-sep-2017

            Lobibox.confirm({
                msg: "Do you want to remove container?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/MasterBL/RemoveContainer",
                            data: { Container: Code, Type: Type, Seal: Seal },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        SetContainerDataList(result.data);
                                        jQuery("#blct_cont_no").val("");
                                        jQuery("#blct_seal_no").val("");
                                        jQuery('#blct_con_tp').val(""); // Line added by Chathura on 16-sep-2017
                                        jQuery("#blct_pack").val("");
                                        jQuery("#blct_fully_empty").val("");
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

        jQuery(".data-container-list").unbind('click').click(function (evt) {
            evt.preventDefault();
            var td = jQuery(this).parent('td');
            var tr = jQuery(td).parent('tr');
            var Code = jQuery(tr).find('td:eq(0)').html();
            var Seal = jQuery(tr).find('td:eq(1)').html();
            var Type = jQuery(tr).find('td:eq(2)').html();
            var Packs = jQuery(tr).find('td:eq(3)').html();
            var fulemp = jQuery(tr).find('td:eq(4)').html(); // Added by Chathura on 16-sep-2017
            Lobibox.confirm({
                msg: "Do you want to edit container details ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {

                        jQuery("#blct_cont_no").val(Code);
                        jQuery("#blct_seal_no").val(Seal);
                        jQuery("#blct_con_tp").val(Type);
                        jQuery("#blct_pack").val(Packs);
                        jQuery("#blct_fully_empty").val((fulemp == "Yes") ? 1 : 0); // Added by Chathura on 16-sep-2017

                        jQuery("#contaddbtn").hide();
                        jQuery("#contupdatebtn").show();

                        jQuery(".btn-update-container").unbind('click').click(function (evt) {

                            jQuery.ajax({
                                type: "GET",
                                url: "/MasterBL/UpdateContainer",
                                data: { Container: Code, Type: Type, Seal: Seal, newContainer: jQuery("#blct_cont_no").val(), newType: jQuery("#blct_con_tp").val(), newSeal: jQuery("#blct_seal_no").val(), newPack: jQuery("#blct_pack").val(), newFullemp: jQuery("#blct_fully_empty").val() },
                                contentType: "application/json;charset=utf-8",
                                dataType: "json",
                                success: function (result) {
                                    if (result.login == true) {
                                        if (result.success == true) {
                                            jQuery(tr).find('td:eq(0)').html(jQuery("#blct_cont_no").val());
                                            jQuery(tr).find('td:eq(1)').html(jQuery("#blct_seal_no").val());
                                            jQuery(tr).find('td:eq(2)').html(jQuery("#blct_con_tp").val());
                                            jQuery(tr).find('td:eq(3)').html(jQuery("#blct_pack").val());
                                            jQuery(tr).find('td:eq(4)').html((jQuery("#blct_fully_empty").val() == 1) ? "Yes" : "No");

                                            jQuery("#blct_cont_no").val("");
                                            jQuery("#blct_seal_no").val("");
                                            jQuery('#blct_con_tp').val(""); // Line added by Chathura on 16-sep-2017
                                            jQuery("#blct_pack").val("");
                                            jQuery("#blct_fully_empty").val("");
                                        }
                                    } else {
                                        Logout();
                                    }
                                }
                            });

                            jQuery("#contaddbtn").show();
                            jQuery("#contupdatebtn").hide();
                        });

                    }
                }
            });
        });
    }

    jQuery('.btn-save').click(function (e) {

        //if (!validateSaveMandatory()) { return; }


        //check fields
        if (jQuery("#Bl_job_no").val() == "") {
            setInfoMsg("Please Select Job No");
            return;
        }
        if (jQuery("#Bl_rmk").val() == "") {
            setInfoMsg("Please Select Remark");
            return;
        }
        if (jQuery("#Bl_doc_dt").val() == "") {
            setInfoMsg("Please Select Date");
            return;
        }
        if (jQuery("#Bl_h_doc_no").val() == "") {
            setInfoMsg("Please Select House BL No");
            return;
        }
        if ($('.tbl-houseblall tr').length < 2) {
            setInfoMsg("Please add House B/L details");
            return;
        }
        Lobibox.confirm({
            msg: "Do you want to save master BL?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    var formdata = jQuery("#draftbldata-data").serialize();


                    jQuery.ajax({
                        type: "GET",
                        url: "/MasterBL/SaveMasterBL",
                        data: formdata,
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    setSuccesssMsg(result.msg);
                                    ClearAllFields();
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
    jQuery('.btn-update-hbl').click(function (e) {
       
        //check fields
        if (jQuery("#Bl_job_no").val() == "") {
            setInfoMsg("Please Select Job No");
            return;
        }
        if (jQuery("#Bl_rmk").val() == "") {
            setInfoMsg("Please Select Remark");
            return;
        }
        if (jQuery("#Bl_doc_dt").val() == "") {
            setInfoMsg("Please Select Date");
            return;
        }
        if (jQuery("#Bl_h_doc_no").val() == "") {
            setInfoMsg("Please Select House BL No");
            return;
        }
        Lobibox.confirm({
            msg: "Do you want to update?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    var formdata = jQuery("#draftbldata-data").serialize();
                   
                    jQuery.ajax({
                        type: "GET",
                        url: "/MasterBL/UpdateHouseBL",
                        data: formdata,
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                           
                            if (result.login == true) {
                                if (result.success == true) {
                                    setSuccesssMsg(result.msg);
                                    ClearAllFields();
                                    isInternalEdit = false;
                                    jQuery('.btn-add-houseblall').show();
                                    jQuery('.btn-update-houseblall').hide();
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
    jQuery(".btn-manual-bl").click(function () {
        click_status = "M";
        var headerKeys = Array()
        headerKeys = ["Row", "Master BL", "House BL", "Draft BL", "Date"];
        field = "BLnoM"
        var x = new CommonSearchDateFiltered(headerKeys, field);
    });
    jQuery(".grss-whgt-uom").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description"];
        field = "UOM"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".btn-net-uom").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description"];
        field = "UOM1"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".btn-pack-uom").click(function () {
        var headerKeys = Array()
        //headerKeys = ["Row", "Code", "Description"];
        headerKeys = ["Row", "Description", "Code"];
        field = "UOM3"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".btn-house-bl").click(function () {
        click_status = "H";
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Customer", "Agent", "Date"];
        field = "BL_H_DOC_NO"
        var x = new CommonSearchDateFiltered(headerKeys, field);
    });
    jQuery(".btn-load-port").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Description", "Code"];
        field = "PORTS"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".btn-terminal").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code"];
        field = "BL_TERMINAL"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".btn-dis-port").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Description", "Code"];
        field = "PORTS2"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".btn-shipper").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Name", "Code", "NIC", "Mobile", "BR No"];
        field = "cusCode4"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".btn-consignee").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Name", "Code", "NIC", "Mobile", "BR No"];
        field = "cusCode5"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".btn-notify-party").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Name", "Code", "NIC", "Mobile", "BR No"];
        field = "cusCode6"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".btn-agent").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Name", "Code", "NIC", "Mobile", "BR No"];
        field = "cusCode7"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".btn-carrier").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Name", "Code", "NIC", "Mobile", "BR No"];
        field = "cusCode8"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".btn-flight").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Description", "Code"];
        field = "VESSEL"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".btn-pouch-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Job No"];
        field = "pouchnojob3"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery("#Bl_pouch_no").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code"];
            field = "pouchnojob3"
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery(".btn-search-jobno").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Job No", "Pouch No", "Date", "Status"];
        field = "jobnobl"
        var x = new CommonSearchDateFiltered(headerKeys, field);
    });
    jQuery(".bl-no-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Date", "Status"];
        field = "BLno2M"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".btn-mesure-uom").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description"];
        field = "Mesure"
        var x = new CommonSearch(headerKeys, field);
    });

    $('#Bl_h_doc_no').focusout(function () {
        var pc = $(this).val();
     
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/HouseBL/HBLNoTextChange",
            data: { HBLno: jQuery("#Bl_h_doc_no").val() },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.containers.length > 0) {
                            SetContainerDataList(result.containers)
                        }
                        if (result.hBLs.length > 0) { // Added by Chathura on 18-sep-2017
                            SetBLAllListWithoutRemove(result.hdrdata)
                        }
                        if (result.hdrdata.length > 0) {
                            SetBLHdrFiels(result.hdrdata);

                            if (jQuery("#Bl_m_doc_no").val() != null && jQuery("#Bl_m_doc_no").val() != "") {
                                $(".btn-save").html('<i class="glyphicon glyphicon-ok-circle"></i> Update');
                                jQuery('.btn-save').show();
                                jQuery('.btn-add-houseblallsave').hide();
                                jQuery('.btn-update-hbl').hide();
                                jQuery('#Bl_m_doc_no').attr("readonly", "true");
                                jQuery('#bl_manual_m_ref').attr("readonly", "true");
                                jQuery('#bl_manual_h_ref').attr("readonly", "true");
                                jQuery('#Bl_m_doc_no').attr("style", "color:#000;");
                            }
                            else {
                                $(".btn-save").html('<i class="glyphicon glyphicon-ok-circle"></i> Save');
                                jQuery('.btn-save').show();
                                jQuery('.btn-add-houseblallsave').hide();
                                jQuery('.btn-update-hbl').hide();
                            }

                            jQuery('.tbl-cus-name .new-row').remove();
                            jQuery('.tbl-shipp-name .new-row').remove();
                            jQuery('.tbl-cons-name .new-row').remove();
                            jQuery('.tbl-notify-name .new-row').remove();
                            jQuery('.tbl-agent-name .new-row').remove();
                            jQuery('.tbl-carr-name .new-row').remove();
                            jQuery('.tbl-cus-name').append('<tr class="new-row">' +
                           '<td>' + result.cusname + '</td>' + '</tr>'
                          );
                            jQuery('.tbl-shipp-name').append('<tr class="new-row">' +
                                '<td>' + result.shipper + '</td>' + '</tr>'
                               );
                            jQuery('.tbl-agent-name').append('<tr class="new-row">' +
                              '<td>' + result.agent + '</td>' + '</tr>'
                             );
                            jQuery('.tbl-notify-name').append('<tr class="new-row">' +
                              '<td>' + result.notify + '</td>' + '</tr>'
                             );
                            jQuery('.tbl-cons-name').append('<tr class="new-row">' +
                              '<td>' + result.consign + '</td>' + '</tr>'
                             );
                            jQuery('.tbl-carr-name').append('<tr class="new-row">' +
                             '<td>' + result.carry + '</td>' + '</tr>'
                            );
                        } else {
                            if (jQuery("#Bl_h_doc_no").val() != "" && jQuery("#Bl_h_doc_no").val() != null) {
                                setInfoMsg("Invalid BL Id!!");
                            }
                        }
                        if (result.details.length > 0) {
                            SetBLitemData(result.details);
                        }

                    } else {
                        setInfoMsg('No Data Found! Please Check  Code!!');
                        return;
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    $('#Bl_d_doc_no').focusout(function () {
        var pc = $(this).val();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/DraftBL/BLNoTextChange",
            data: { BLno: jQuery("#Bl_d_doc_no").val() },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.containers.length > 0) {
                            SetContainerDataList(result.containers)
                        }
                        if (result.hdrdata.length > 0) {
                            SetBLHdrFiels(result.hdrdata);

                            jQuery('.tbl-cus-name .new-row').remove();
                            jQuery('.tbl-shipp-name .new-row').remove();
                            jQuery('.tbl-cons-name .new-row').remove();
                            jQuery('.tbl-notify-name .new-row').remove();
                            jQuery('.tbl-agent-name .new-row').remove();
                            jQuery('.tbl-carr-name .new-row').remove();
                            jQuery('.tbl-cus-name').append('<tr class="new-row">' +
                           '<td>' + result.cusname + '</td>' + '</tr>'
                          );
                            jQuery('.tbl-shipp-name').append('<tr class="new-row">' +
                                '<td>' + result.shipper + '</td>' + '</tr>'
                               );
                            jQuery('.tbl-agent-name').append('<tr class="new-row">' +
                              '<td>' + result.agent + '</td>' + '</tr>'
                             );
                            jQuery('.tbl-notify-name').append('<tr class="new-row">' +
                              '<td>' + result.notify + '</td>' + '</tr>'
                             );
                            jQuery('.tbl-cons-name').append('<tr class="new-row">' +
                              '<td>' + result.consign + '</td>' + '</tr>'
                             );
                            jQuery('.tbl-carr-name').append('<tr class="new-row">' +
                             '<td>' + result.carry + '</td>' + '</tr>'
                            );
                        } else {
                            if (jQuery("#Bl_d_doc_no").val() != null && jQuery("#Bl_d_doc_no").val() != "") { // This code segment added by Chathura on 19-sep-2017
                                setInfoMsg("Invalid BL Id!!");
                            }
                        }
                        if (result.details.length > 0) {
                            SetBLitemData(result.details);
                        }

                    } else {
                        setInfoMsg('No Data Found! Please Check  Code!!');
                        return;
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    $('#Bl_m_doc_no').focusout(function () {
       
        var pc = $(this).val();
        if (pc != "") {

            jQuery.ajax({
                cache: false,
                type: "GET",
                url: "/MasterBL/MBLNoTextChange",
                data: { MBLno: jQuery("#Bl_m_doc_no").val() },
                success: function (result) {
                
                    if (result.login == true) {
                        if (result.success == true) {

                            //jQuery('.tbl-houseblall .new-row').remove();

                            if (result.containers.length > 0) {
                                // SetBLHdrData(result.data);
                                SetContainerDataList(result.containers)
                            }
                            if (result.hBLs.length > 0) { // Added by Chathura on 18-sep-2017
                           
                                SetBLAllListWithoutRemove(result.hBLs)
                            }
                            if (result.hdrdata.length > 0) {
                           
                                SetBLHdrFiels(result.hdrdata);

                                // Below two lines added by Chathura on 19-sep-2017
                                //jQuery('.btn-save').hide();
                                jQuery('.btn-save').show();
                                jQuery('.btn-add-houseblallsave').hide();
                                jQuery('.btn-update-hbl').hide();
                                if (jQuery("#Bl_m_doc_no").val() != null && jQuery("#Bl_m_doc_no").val() != "") {
                                    $(".btn-save").html('<i class="glyphicon glyphicon-ok-circle"></i> Update');
                                    jQuery('#Bl_m_doc_no').attr("readonly", "true");
                                    jQuery('#bl_manual_m_ref').attr("readonly", "true");
                                    jQuery('#bl_manual_h_ref').attr("readonly", "true");
                                    jQuery('#Bl_m_doc_no').attr("style", "color:#000;");
                                }
                                else {
                                    $(".btn-save").html('<i class="glyphicon glyphicon-ok-circle"></i> Save');
                                }

                                jQuery('.tbl-cus-name .new-row').remove();
                                jQuery('.tbl-shipp-name .new-row').remove();
                                jQuery('.tbl-cons-name .new-row').remove();
                                jQuery('.tbl-notify-name .new-row').remove();
                                jQuery('.tbl-agent-name .new-row').remove();
                                jQuery('.tbl-carr-name .new-row').remove();
                                jQuery('.tbl-cus-name').append('<tr class="new-row">' +
                               '<td>' + result.cusname + '</td>' + '</tr>'
                              );
                                jQuery('.tbl-shipp-name').append('<tr class="new-row">' +
                                    '<td>' + result.shipper + '</td>' + '</tr>'
                                   );
                                jQuery('.tbl-agent-name').append('<tr class="new-row">' +
                                  '<td>' + result.agent + '</td>' + '</tr>'
                                 );
                                jQuery('.tbl-notify-name').append('<tr class="new-row">' +
                                  '<td>' + result.notify + '</td>' + '</tr>'
                                 );
                                jQuery('.tbl-cons-name').append('<tr class="new-row">' +
                                  '<td>' + result.consign + '</td>' + '</tr>'
                                 );
                                jQuery('.tbl-carr-name').append('<tr class="new-row">' +
                                 '<td>' + result.carry + '</td>' + '</tr>'
                                );
                            }
                            else {
                           
                                if (jQuery("#Bl_m_doc_no").val() != null && jQuery("#Bl_m_doc_no").val() != "") {
                             
                                    setInfoMsg("Invalid BL Id!!");

                                    ClearAllFields();

                                    if (jQuery("#Bl_m_doc_no").val() != null && jQuery("#Bl_m_doc_no").val() != "") {
                                        $(".btn-save").html('<i class="glyphicon glyphicon-ok-circle"></i> Update');
                                    }
                                    else {
                                        $(".btn-save").html('<i class="glyphicon glyphicon-ok-circle"></i> Save');
                                    }
                                }
                                //ClearAllFields();
                            }
                            if (result.details.length > 0) {
                               
                                SetBLitemData(result.details);
                            }

                        } else {
                            setInfoMsg('No Data Found! Please Check  Code!!');
                            return;
                        }
                    } else {
                        Logout();
                    }
                }
            });
        }
    });
    function SetBLHdrFiels(data) {
       
        jQuery("#Bl_job_no").val(data[0].Bl_job_no);
        jQuery("#Bl_pouch_no").val(data[0].Bl_pouch_no);
        jQuery("#Bl_h_doc_no").val(data[0].Bl_h_doc_no);
        jQuery("#Bl_doc_dt").val(getFormatedDate1(data[0].Bl_doc_dt));
        if (data[0].Bl_m_doc_no != null && data[0].Bl_m_doc_no != '') { jQuery("#Bl_m_doc_no").val(data[0].Bl_m_doc_no); hasMaster = true; } else { hasMaster = false; }
        jQuery("#bl_or_seq").val(data[0].bl_or_seq);
        jQuery("#Bl_freight_charg").val(data[0].Bl_freight_charg);
        jQuery("#Bl_port_load").val(data[0].Bl_port_load);
        jQuery("#bl_terminal").val(data[0].bl_terminal);
        jQuery("#Bl_port_discharge").val(data[0].Bl_port_discharge);
        jQuery("#bl_pay_mode").val(data[0].bl_pay_mode);
        jQuery("#bl_vsl_oper").val(data[0].bl_vsl_oper);
        jQuery("#Bl_port_discharge").val(data[0].Bl_port_discharge);
        jQuery("#bl_cntr_oper").val(data[0].bl_cntr_oper);
        jQuery("#Bl_palce_rec").val(data[0].Bl_palce_rec);
        jQuery("#Bl_voage_no").val(data[0].Bl_voage_no);
        jQuery("#Bl_est_time_arr").val(getFormatedDate1(data[0].Bl_est_time_arr));
        jQuery("#Bl_voage_dt").val(getFormatedDate1(data[0].Bl_voage_dt));
        jQuery("#Bl_est_time_dep").val(getFormatedDate1(data[0].Bl_est_time_dep));
        jQuery("#bl_expires_dt").val(getFormatedDate1(data[0].bl_expires_dt));
        jQuery("#Bl_rmk").val(data[0].Bl_rmk);
        jQuery("#Bl_cus_cd").val(data[0].Bl_cus_cd);
        jQuery("#Bl_shipper_cd").val(data[0].Bl_shipper_cd);
        jQuery("#Bl_consignee_cd").val(data[0].Bl_consignee_cd);
        jQuery("#Bl_ntfy_party_cd").val(data[0].Bl_ntfy_party_cd);
        jQuery("#Bl_agent_cd").val(data[0].Bl_agent_cd);
        jQuery("#Bl_d_doc_no").val(data[0].Bl_d_doc_no);
        jQuery("#Bl_ship_line_cd").val(data[0].Bl_ship_line_cd);
        jQuery("#bl_pack_uom").val(data[0].bl_pack_uom);
        jQuery("#Bl_vessal_no").val(data[0].Bl_vessal_no);
        jQuery("#Bl_palce_del").val(data[0].Bl_palce_del);
        jQuery("#bl_manual_d_ref").val(data[0].bl_manual_d_ref);
        jQuery("#bl_manual_h_ref").val(data[0].bl_manual_h_ref);
        jQuery("#bl_manual_m_ref").val(data[0].bl_manual_m_ref);

        jQuery("#bld_package_nos").val(data[0].bld_package_nos);
        jQuery("#bld_grs_weight").val(data[0].bld_grs_weight);
        jQuery("#bld_grs_weight_uom").val(data[0].bld_grs_weight_uom);
        jQuery("#bld_net_weight").val(data[0].bld_net_weight);
        jQuery("#bld_net_weight_uom").val(data[0].bld_net_weight_uom);
        jQuery("#bld_measure").val(data[0].bld_measure);
        jQuery("#bld_measure_uom").val(data[0].bld_measure_uom);

        if (jQuery("#Bl_m_doc_no").val() != null && jQuery("#Bl_m_doc_no").val() != "") {
            $(".btn-save").html('<i class="glyphicon glyphicon-ok-circle"></i> Update');
        }
        else {
            $(".btn-save").html('<i class="glyphicon glyphicon-ok-circle"></i> Save');
        }
    }

    function SetDisplayBLHdrFiels(data,detail) {
      
        jQuery("#Bl_job_no").val(data[0].Bl_job_no);
        jQuery("#Bl_pouch_no").val(data[0].Bl_pouch_no);
        jQuery("#Bl_h_doc_no").val(data[0].Bl_h_doc_no);
        jQuery("#Bl_doc_dt").val(getFormatedDate1(data[0].Bl_doc_dt));
        if (data[0].Bl_m_doc_no != null && data[0].Bl_m_doc_no != '') { jQuery("#Bl_m_doc_no").val(data[0].Bl_m_doc_no); hasMaster = true; } else { hasMaster = false; }
        jQuery("#bl_or_seq").val(data[0].bl_or_seq);
        jQuery("#Bl_freight_charg").val(data[0].Bl_freight_charg);
        jQuery("#Bl_port_load").val(data[0].Bl_port_load);
        jQuery("#bl_terminal").val(data[0].bl_terminal);
        jQuery("#Bl_port_discharge").val(data[0].Bl_port_discharge);
        jQuery("#bl_pay_mode").val(data[0].bl_pay_mode);
        jQuery("#bl_vsl_oper").val(data[0].bl_vsl_oper);
        jQuery("#Bl_port_discharge").val(data[0].Bl_port_discharge);
        jQuery("#bl_cntr_oper").val(data[0].bl_cntr_oper);
        jQuery("#Bl_palce_rec").val(data[0].Bl_palce_rec);
        jQuery("#Bl_voage_no").val(data[0].Bl_voage_no);
        jQuery("#Bl_est_time_arr").val(getFormatedDate1(data[0].Bl_est_time_arr));
        jQuery("#Bl_voage_dt").val(getFormatedDate1(data[0].Bl_voage_dt));
        jQuery("#Bl_est_time_dep").val(getFormatedDate1(data[0].Bl_est_time_dep));
        jQuery("#bl_expires_dt").val(getFormatedDate1(data[0].bl_expires_dt));
        jQuery("#Bl_rmk").val(data[0].Bl_rmk);
        jQuery("#Bl_cus_cd").val(data[0].Bl_cus_cd);
        jQuery("#Bl_shipper_cd").val(data[0].Bl_shipper_cd);
        jQuery("#Bl_consignee_cd").val(data[0].Bl_consignee_cd);
        jQuery("#Bl_ntfy_party_cd").val(data[0].Bl_ntfy_party_cd);
        jQuery("#Bl_agent_cd").val(data[0].Bl_agent_cd);
        jQuery("#Bl_d_doc_no").val(data[0].Bl_d_doc_no);
        jQuery("#Bl_ship_line_cd").val(data[0].Bl_ship_line_cd);
        jQuery("#bl_pack_uom").val(data[0].bl_pack_uom);
        jQuery("#Bl_vessal_no").val(data[0].Bl_vessal_no);
        jQuery("#Bl_palce_del").val(data[0].Bl_palce_del);
        jQuery("#bl_manual_d_ref").val(data[0].bl_manual_d_ref);
        jQuery("#bl_manual_h_ref").val(data[0].bl_manual_h_ref);
        jQuery("#bl_manual_m_ref").val(data[0].bl_manual_m_ref);

        jQuery("#bld_package_nos").val(detail[0].bld_package_nos);
        jQuery("#bld_grs_weight").val(detail[0].bld_grs_weight);
        jQuery("#bld_grs_weight_uom").val(detail[0].bld_grs_weight_uom);
        jQuery("#bld_net_weight").val(detail[0].bld_net_weight);
        jQuery("#bld_net_weight_uom").val(detail[0].bld_net_weight_uom);
        jQuery("#bld_measure").val(detail[0].bld_measure);
        jQuery("#bld_measure_uom").val(detail[0].bld_measure_uom);

        if (jQuery("#Bl_m_doc_no").val() != null && jQuery("#Bl_m_doc_no").val() != "") {
            $(".btn-save").html('<i class="glyphicon glyphicon-ok-circle"></i> Update');
        }
        else {
            $(".btn-save").html('<i class="glyphicon glyphicon-ok-circle"></i> Save');
        }
    }

    function clearpage() {
        window.location.href = "/MasterBL";
    }

    function SetBLitemData(data) {
        jQuery("#bld_mark_nos").val(data[0].bld_mark_nos);
        jQuery("#bld_desc_goods").val(data[0].bld_desc_goods);
        jQuery("#bld_package_nos").val(data[0].bld_package_nos);
        jQuery("#bld_grs_weight").val(data[0].bld_grs_weight);
        jQuery("#bld_grs_weight_uom").val(data[0].bld_grs_weight_uom);
        jQuery("#bld_net_weight").val(data[0].bld_net_weight);
        jQuery("#bld_net_weight_uom").val(data[0].bld_net_weight_uom);
        jQuery("#bld_measure").val(data[0].bld_measure);
        jQuery("#bld_measure_uom").val(data[0].bld_measure_uom);
    }
    jQuery(".btn-clear").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to clear data ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    window.location.href = "/MasterBL";
                }
            }
        });

    });
    jQuery(".btn-cust_search").click(function () {
        if (jQuery("#Bl_job_no").val() != null && jQuery("#Bl_job_no").val() != "") {
            var headerKeys = Array()
            headerKeys = ["Row", "Name", "Code", "NIC", "Mobile", "BR No"];
            field = "cusCodeJf";
            data = jQuery("#Bl_job_no").val();
     
            var x = new CommonSearch(headerKeys, field, data);
          
        }
        else {
            setInfoMsg("Please select Job No");
        }
    });
    LoadPayType();
    LoadoutType();
    LoadInType();
    function LoadPayType() {
        jQuery.ajax({
            type: "GET",
            url: "/MasterBL/LoadPayType",
            data: {},
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("bl_pay_mode");
                        jQuery("#bl_pay_mode").empty();
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
    function LoadoutType() {
        jQuery.ajax({
            type: "GET",
            url: "/MasterBL/LoadOutwordType",
            data: {},
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("Bl_load_out_tp");
                        jQuery("#Bl_load_out_tp").empty();
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
    function LoadInType() {
        jQuery.ajax({
            type: "GET",
            url: "/MasterBL/LoadOutwordType",
            data: {},
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("Bl_load_in_tp");
                        jQuery("#Bl_load_in_tp").empty();
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
    function ClearAllFields() {
        if (value == "SXP") {//"EXPS"

            jQuery("#Bl_port_load").val("LKCMB");
            jQuery("#Bl_port_discharge").val("");
            jQuery("#Bl_palce_del").val("");

        } else {
            jQuery("#Bl_port_load").val("");
            jQuery("#Bl_port_discharge").val("LKCMB");
            jQuery("#Bl_palce_del").val("COLOMBO");
        }
        jQuery("#Bl_job_no").val("");
        jQuery("#Bl_pouch_no").val("");
        jQuery("#Bl_h_doc_no").val("");
        jQuery("#Bl_doc_dt").val("");
        jQuery("#Bl_m_doc_no").val("");
        jQuery("#bl_or_seq").val("");
        jQuery("#Bl_freight_charg").val("");
        //jQuery("#Bl_port_load").val("");
        jQuery("#bl_terminal").val("");
        //jQuery("#Bl_port_discharge").val("");
        jQuery("#bl_pay_mode").val("");
        jQuery("#bl_vsl_oper").val("");
        //jQuery("#Bl_port_discharge").val("");
        jQuery("#bl_cntr_oper").val("");
        jQuery("#Bl_palce_rec").val("");
        jQuery("#Bl_voage_no").val("");
        jQuery("#Bl_est_time_arr").val("");
        jQuery("#Bl_voage_dt").val("");
        jQuery("#Bl_est_time_dep").val("");
        jQuery("#bl_expires_dt").val("");
        jQuery("#Bl_rmk").val("");
        jQuery("#Bl_cus_cd").val("");
        jQuery("#Bl_shipper_cd").val("");
        jQuery("#Bl_consignee_cd").val("");
        jQuery("#Bl_ntfy_party_cd").val("");
        jQuery("#Bl_agent_cd").val("");
        jQuery("#Bl_d_doc_no").val("");
        jQuery("#Bl_ship_line_cd").val("");
        jQuery("#bl_pack_uom").val("");
        jQuery("#Bl_vessal_no").val("");
        jQuery('.tbl-container .new-row').remove();
        jQuery("#bld_package_nos").val("");
        jQuery("#bld_grs_weight").val("");
        jQuery("#bld_grs_weight_uom").val("");
        jQuery("#bld_net_weight").val("");
        jQuery("#bld_net_weight_uom").val("");
        jQuery("#bld_measure").val("");
        jQuery("#bld_measure_uom").val("");
        jQuery("#bld_mark_nos").val("");
        jQuery("#bld_desc_goods").val("");
        //jQuery("#Bl_palce_del").val("");
        jQuery('#Bl_doc_dt').val(my_date_format_with_time(new Date()));
        jQuery("#bl_manual_d_ref").val("");
        jQuery("#bl_manual_h_ref").val("");
        jQuery("#bl_manual_m_ref").val("");
        jQuery('.tbl-cus-name .new-row').remove();
        jQuery('.tbl-shipp-name .new-row').remove();
        jQuery('.tbl-cons-name .new-row').remove();
        jQuery('.tbl-notify-name .new-row').remove();
        jQuery('.tbl-agent-name .new-row').remove();
        jQuery('.tbl-carr-name .new-row').remove();
        jQuery('.tbl-houseblall .new-row').remove();
        jQuery('.btn-save').show();
        jQuery('.btn-add-houseblallsave').hide();
        jQuery('.btn-update-hbl').hide();
        click_status = "N";
        isInternalEdit = false;
        hasMaster = false;

        jQuery('#Bl_est_time_arr').val(my_date_format_tran(my_date_format_with_time(new Date()).toString()));
        jQuery('#Bl_est_time_dep').val(my_date_format_tran(my_date_format_with_time(new Date()).toString()));
        jQuery('#bl_expires_dt').val(my_date_format_tran(my_date_format_with_time(new Date()).toString()));
        jQuery('#Bl_voage_dt').val(my_date_format_tran(my_date_format_with_time(new Date()).toString()));

        if (jQuery("#Bl_m_doc_no").val() != null && jQuery("#Bl_m_doc_no").val() != "") {
            $(".btn-save").html('<i class="glyphicon glyphicon-ok-circle"></i> Update');
        }
        else {
            $(".btn-save").html('<i class="glyphicon glyphicon-ok-circle"></i> Save');

        }
        
        jQuery('#Bl_m_doc_no').attr("readonly", "false");
        jQuery('#bl_manual_m_ref').attr("readonly", "false");
        jQuery('#bl_manual_h_ref').attr("readonly", "false");
        jQuery('#Bl_m_doc_no').removeAttr("readonly");
        jQuery('#bl_manual_m_ref').removeAttr("readonly");
        jQuery('#bl_manual_h_ref').removeAttr("readonly");
        jQuery('#Bl_m_doc_no').attr("style", "color:#000;");
      
        jQuery.ajax({
            type: "GET",
            url: "/DraftBL/ClearSession",
            data: {},
            contentType: "application/json;charset=utf-8",
            dataType: "json",
        })
    }
    jQuery("#bl_pack_uom").focusout(function () {
        var code = jQuery(this).val();
        UOMfocusOut(code);
    });
    function UOMfocusOut(code) {
        if (code != "") {
            if (code != "") {
                jQuery.ajax({
                    type: "GET",
                    url: "/Validation/validateUOM?uomcd=" + code,
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        if (result.login == false) {
                            Logout();
                        }
                        if (result.success == false) {
                            setInfoMsg(result.msg);
                            jQuery("#bl_pack_uom").val("");
                            jQuery("#bl_pack_uom").focus();
                        }
                        if (result.success == true) {
                            if (result.data.MT_CD == null) {
                                setInfoMsg("Please enter valid UOM.");
                                jQuery("#bl_pack_uom").val("");
                                jQuery("#bl_pack_uom").focus();
                            }
                        }
                    }
                });
            }
        }
    }

    jQuery("#bld_grs_weight_uom").focusout(function () {
        var code = jQuery(this).val();
        UOMfocusOut2(code);
    });
    function UOMfocusOut2(code) {
        if (code != "") {
            if (code != "") {
                jQuery.ajax({
                    type: "GET",
                    url: "/Validation/validateUOM?uomcd=" + code,
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        if (result.login == false) {
                            Logout();
                        }
                        if (result.success == false) {
                            setInfoMsg(result.msg);
                            jQuery("#bld_grs_weight_uom").val("");
                            jQuery("#bld_grs_weight_uom").focus();
                        }
                        if (result.success == true) {
                            if (result.data.MT_CD == null) {
                                setInfoMsg("Please enter valid UOM.");
                                jQuery("#bld_grs_weight_uom").val("");
                                jQuery("#bld_grs_weight_uom").focus();
                            }
                        }
                    }
                });
            }
        }
    }
    jQuery("#bld_net_weight_uom").focusout(function () {
        var code = jQuery(this).val();
        UOMfocusOut4(code);
    });
    function UOMfocusOut4(code) {
        if (code != "") {
            if (code != "") {
                jQuery.ajax({
                    type: "GET",
                    url: "/Validation/validateUOM?uomcd=" + code,
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        if (result.login == false) {
                            Logout();
                        }
                        if (result.success == false) {
                            setInfoMsg(result.msg);
                            jQuery("#bld_net_weight_uom").val("");
                            jQuery("#bld_net_weight_uom").focus();
                        }
                        if (result.success == true) {
                            if (result.data.MT_CD == null) {
                                setInfoMsg("Please enter valid UOM.");
                                jQuery("#bld_net_weight_uom").val("");
                                jQuery("#bld_net_weight_uom").focus();
                            }
                        }
                    }
                });
            }
        }
    }

    jQuery("#bld_measure_uom").focusout(function () {
        var code = jQuery(this).val();
        UOMfocusOut3(code);
    });
    function UOMfocusOut3(code) {
        if (code != "") {
            if (code != "") {
                jQuery.ajax({
                    type: "GET",
                    url: "/Validation/validateUOM?uomcd=" + code,
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        if (result.login == false) {
                            Logout();
                        }
                        if (result.success == false) {
                            setInfoMsg(result.msg);
                            jQuery("#bld_measure_uom").val("");
                            jQuery("#bld_measure_uom").focus();
                        }
                        if (result.success == true) {
                            if (result.data.MT_CD == null) {
                                setInfoMsg("Please enter valid UOM.");
                                jQuery("#bld_measure_uom").val("");
                                jQuery("#bld_measure_uom").focus();
                            }
                        }
                    }
                });
            }
        }
    }

    $('input#bld_package_nos').blur(function () {
        var num = parseFloat($(this).val());
        var cleanNum = num.toFixed(3);
        if (cleanNum == "NaN") {
            cleanNum = 0;
        }
        if (Number(cleanNum) < 0) {
            cleanNum = 0;
        }
        $(this).val(cleanNum);
    });
    $('input#bld_grs_weight').blur(function () {
        var num = parseFloat($(this).val());
        var cleanNum = num.toFixed(3);
        if (cleanNum == "NaN") {
            cleanNum = 0;
        }
        if (Number(cleanNum) < 0) {
            cleanNum = 0;
        }
        $(this).val(cleanNum);
    });
    $('input#bld_net_weight').blur(function () {
        var num = parseFloat($(this).val());
        var cleanNum = num.toFixed(3);
        if (cleanNum == "NaN") {
            cleanNum = 0;
        }
        if (Number(cleanNum) < 0) {
            cleanNum = 0;
        }
        $(this).val(cleanNum);
    });
    $('input#bld_measure').blur(function () {
        var num = parseFloat($(this).val());
        var cleanNum = num.toFixed(3);
        if (cleanNum == "NaN") {
            cleanNum = 0;
        }
        if (Number(cleanNum) < 0) {
            cleanNum = 0;
        }
        $(this).val(cleanNum);
    });
    $('input#Bl_freight_charg').blur(function () {
        var num = parseFloat($(this).val());
        var cleanNum = num.toFixed(2);
        if (cleanNum == "NaN") {
            cleanNum = 0;
        }
        if (Number(cleanNum) < 0) {
            cleanNum = 0;
        }
        $(this).val(cleanNum);
    });
    jQuery("#Bl_port_load").focusout(function () {
        var code = jQuery(this).val();
        PortValidate(code);
    });
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
                            jQuery("#Bl_port_load").val("");
                            jQuery("#Bl_port_load").focus();
                        }
                    }
                });
            }
        }
    }

    jQuery("#Bl_port_discharge").focusout(function () {
        var code = jQuery(this).val();
        PortValidate2(code);
    });
    function PortValidate2(code) {
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
                            jQuery("#Bl_port_discharge").val("");
                            jQuery("#Bl_port_discharge").focus();
                        }
                    }
                });
            }
        }
    }
    jQuery("#Bl_vessal_no").focusout(function () {
        var code = jQuery(this).val();
        FlightValidate(code);
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
                            jQuery("#Bl_vessal_no").val("");
                            jQuery("#Bl_vessal_no").focus();
                        }
                    }
                });
            }
        }
    }
    jQuery("#Bl_job_no").focusout(function () {
        var code = jQuery(this).val();
        jobNumberFocusOut(code);
    });
    function jobNumberFocusOut(jobnum) {
        if (jobnum != "") {
            if (jobnum != "") {
                jQuery.ajax({
                    type: "GET",
                    url: "/Validation/validateJobNumber?jobNum=" + jobnum,
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        if (result.login == false) {
                            Logout();
                        } else {
                            if (result.success == false) {
                                setInfoMsg(result.msg);
                                jQuery("#Bl_pouch_no").val("");
                                jQuery("#Bl_pouch_no").focus();
                                jQuery("#Bl_job_no").val("");
                            }
                            if (result.success == true) {
                                if (result.data.Jb_jb_no == null) {
                                    setInfoMsg("Please enter valid job number.");
                                    jQuery("#Bl_pouch_no").val("");
                                    jQuery("#Bl_pouch_no").focus();
                                    jQuery("#Bl_job_no").val("");
                                } else {
                                    jQuery("#Bl_pouch_no").val(result.data.Jb_pouch_no);
                                }
                            }
                        }
                    }
                });
            }
        }
    }

    jQuery("#Bl_pouch_no").focusout(function () {
        var code = jQuery(this).val();
        pouchNumberFocusOut(code);
    });
    function pouchNumberFocusOut(pouchno) {
        if (pouchno != "") {
            if (pouchno != "") {
                jQuery.ajax({
                    type: "GET",
                    url: "/Validation/validatePouchNumber?pouchno=" + pouchno,
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        if (result.login == false) {
                            Logout();
                        } else {
                            if (result.success == false) {
                                setInfoMsg(result.msg);
                                jQuery("#Bl_pouch_no").val("");
                                jQuery("#Bl_pouch_no").focus();
                                jQuery("#Bl_job_no").val("");
                            }
                            if (result.success == true) {
                                if (result.data.Jb_pouch_no == null) {
                                    setInfoMsg("Please enter valid pouch number.");
                                    jQuery("#Bl_pouch_no").val("");
                                    jQuery("#Bl_pouch_no").focus();
                                    jQuery("#Bl_job_no").val("");
                                } else {
                                    jQuery("#Bl_job_no").val(result.data.Jb_jb_no);
                                }
                            }
                        }
                    }
                });
            }
        }
    }
    jQuery(".bld-no-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code"];
        field = "BL_MANUAL_D_REF"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".blh-no-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code"];
        //field = "BL_MANUAL_H_REF"
        field = "BL_MANUAL_H_REF1"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".btn-draft-blid").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Ref.No", "Pouch No", "Date"];
        field = "BLnoD"
        var x = new CommonSearchDateFiltered(headerKeys, field);
    });
    jQuery(".blm-no-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code"];
        //field = "BL_MANUAL_M_REF"
        field = "BL_MANUAL_M_REF1"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".btn-new-ser").click(function (evt) {
        window.location.href = "/ServiceCreation";
    });
    LoadFullEmpty();
    function LoadFullEmpty() {
        jQuery.ajax({
            type: "GET",
            url: "/MasterBL/LoadFullyEmpty",
            data: {},
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("blct_fully_empty");
                        jQuery("#blct_fully_empty").empty();
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
    LoadContainerType();
    function LoadContainerType() {
        jQuery.ajax({
            type: "GET",
            url: "/HouseBL/LoadContainerType",
            data: {},
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("blct_con_tp");
                        jQuery("#blct_con_tp").empty();
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
    jQuery(".btn-search-houseblall").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code"];
        field = "BL_H_DOC_NOALL"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery('.btn-add-houseblall').click(function (e) {

        //if (!validateSaveMandatory()) { return; }

        var isExistsHBL = false;
        
        if (jQuery("#Bl_h_doc_no").val() == "") {
            setInfoMsg("Please Select House BL No");
            return;
        }
            //else if (jQuery("#Bl_m_doc_no").val() != "" && jQuery("#Bl_m_doc_no").val() != null) { // Added by Chathura on 18-sep-2017 - else if added
            //    setInfoMsg("Selected House BL No is already assigned to a Master BL");
            //    return;
            //}
        else if (hasMaster == true) { // Added by Chathura on 18-sep-2017 - else if added
            setInfoMsg("Selected House BL No is already assigned to a Master BL");
            return;
        }
        Lobibox.confirm({
            msg: "Do you want to continue add house BL?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                  
                    jQuery.ajax({
                        type: "GET",
                        url: "/MasterBL/AddHouseBLAll",
                        //url: "/HouseBL/HBLNoTextChange",
                        data: { BLNo: jQuery("#Bl_h_doc_no").val(), isEdit: isInternalEdit },
                        //data: { HBLNo: jQuery("#Bl_h_doc_no").val() },
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                         
                            if (result.login == true) {

                                if (result.success == true) {

                                    if (result.type == "Info") {
                                        setInfoMsg(result.msg);
                                        return;
                                    }
                                
                                    SetBLAllList(result.hdrdata);

                                    jQuery('.btn-add-houseblallsave').show();
                                    jQuery('.btn-update-hbl').hide();
                            
                                    jQuery("#houseblall").val("");
                                } else {
                                    if (result.type == "Error") {
                                        setError(result.msg);

                                    }
                                    if (result.type == "Info") {
                                        setInfoMsg(result.msg);
                                    }
                                }

                                isInternalEdit = false;
                              
                            } else {
                                Logout();
                            }
                        }
                    });
                }
            }

        });
    });



    function SetBLAllList(data) {
    
        //jQuery('.tbl-houseblall .new-row').remove();
        if (data.length > 0) {
            jQuery('.btn-save').hide();
            jQuery('.btn-add-houseblallsave').show();
            jQuery('.btn-update-hbl').hide();
            for (i = 0; i < data.length; i++) {
              
                jQuery('.tbl-houseblall').append('<tr class="new-row">' +
                      '<td>' + data[i].Bl_doc_no + '</td>' +
                      '<td>' + data[i].Bl_d_doc_no + '</td>' +
                      '<td>' + data[i].Bl_job_no + '</td>' +
                      '<td>' + data[i].Bl_pouch_no + '</td>' +
                      '<td>' + data[i].Bl_cus_cd + '</td>' +
                      '<td>' + ' <input type="button" class="btn btn-sm btn-red-fullbg remove-blall-list" value="Remove" />' + '</td>' +
                       '<td>' + ' <input type="button" class="btn btn-sm btn-green-fullbg data-blall-list" value="Data" />' + '</td>' +
                        '</tr>');
            }
            ClearAllFieldsWOHBL(); // Added by Chathura on 18-sep-2017
            RemoveBLAllFunction();
            DisplayData();
        } else {
            jQuery('.btn-save').show();
            jQuery('.btn-add-houseblallsave').hide();
            jQuery('.btn-update-hbl').hide();
        }
    }
    // Added by Chathura on 18-sep-2017
    function SetBLAllListWithoutRemove(data) {
       
        jQuery('.tbl-houseblall .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {
            
                jQuery('.tbl-houseblall').append('<tr class="new-row">' +
                      '<td>' + data[i].Bl_h_doc_no + '</td>' +
                      '<td>' + data[i].Bl_d_doc_no + '</td>' +
                      '<td>' + data[i].Bl_job_no + '</td>' +
                      '<td>' + data[i].Bl_pouch_no + '</td>' +
                      '<td>' + data[i].Bl_cus_cd + '</td>' +
                       '<td>' + ' <input type="button" disabled class="btn btn-sm btn-red-fullbg remove-blall-list" value="Remove" />' + '</td>' +
                       '<td>' + ' <input type="button" class="btn btn-sm btn-green-fullbg data-blall-list" value="Data" />' + '</td>' +
                        '</tr>');
            }
            ClearAllFieldsWOHBL(); // CMC
            RemoveBLAllFunction();
            DisplayData();
        } else {
            jQuery('.btn-save').show();
            jQuery('.btn-add-houseblallsave').hide();
            jQuery('.btn-update-hbl').hide();
        }
    }
    function RemoveBLAllFunction() {
        jQuery(".remove-blall-list").unbind('click').click(function (evt) {
            evt.preventDefault();
            var td = jQuery(this).parent('td');
            var tr = jQuery(td).parent('tr');
            var Code = jQuery(tr).find('td:eq(0)').html();
            Lobibox.confirm({
                msg: "Do you want to remove ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {

                        jQuery.ajax({
                            type: "GET",
                            url: "/MasterBL/RemoveAllBl",
                            data: { BL: Code, },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                       
                                        $(tr).remove();
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
    function DisplayData() {
     


        jQuery(".data-blall-list").unbind('click').click(function (evt) {
          
            evt.preventDefault();
            var td = jQuery(this).parent('td');
            var tr = jQuery(td).parent('tr');

            var Code = jQuery(tr).find('td:eq(0)').html();
           
            Lobibox.confirm({
                msg: "Do you want to display data ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/MasterBL/DisplayData",
                            data: { BL: Code, },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                       
                                        jQuery('.btn-save').hide();
                                        jQuery('.btn-add-houseblallsave').hide();
                                        jQuery('.btn-update-hbl').show();
                                        isInternalEdit = true;
                                        jQuery('.btn-add-houseblall').hide();
                                        jQuery('.btn-update-houseblall').show();

                                        if (result.containers.length > 0) {
                                            // SetBLHdrData(result.data);
                                            SetContainerDataList(result.containers)
                                        }
                                        if (result.details.length > 0) {
                                            SetBLitemData(result.details);
                                        }
                                        if (result.hdrdata.length > 0) {
                                            SetDisplayBLHdrFiels(result.hdrdata, result.details);
                                            jQuery('.tbl-cus-name .new-row').remove();
                                            jQuery('.tbl-shipp-name .new-row').remove();
                                            jQuery('.tbl-cons-name .new-row').remove();
                                            jQuery('.tbl-notify-name .new-row').remove();
                                            jQuery('.tbl-agent-name .new-row').remove();
                                            jQuery('.tbl-carr-name .new-row').remove();
                                            jQuery('.tbl-cus-name').append('<tr class="new-row">' +
                                           '<td>' + result.cusname + '</td>' + '</tr>'
                                          );
                                            jQuery('.tbl-shipp-name').append('<tr class="new-row">' +
                                                '<td>' + result.shipper + '</td>' + '</tr>'
                                               );
                                            jQuery('.tbl-agent-name').append('<tr class="new-row">' +
                                              '<td>' + result.agent + '</td>' + '</tr>'
                                             );
                                            jQuery('.tbl-notify-name').append('<tr class="new-row">' +
                                              '<td>' + result.notify + '</td>' + '</tr>'
                                             );
                                            jQuery('.tbl-cons-name').append('<tr class="new-row">' +
                                              '<td>' + result.consign + '</td>' + '</tr>'
                                             );
                                            jQuery('.tbl-carr-name').append('<tr class="new-row">' +
                                             '<td>' + result.carry + '</td>' + '</tr>'
                                            );
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

            jQuery('.btn-update-houseblall').unbind('click').click(function (e) {
              

                var isExistsHBL = false;
              
                if (jQuery("#Bl_h_doc_no").val() == "") {
                    setInfoMsg("Please Select House BL No");
                    return;
                }
                //else if (jQuery("#Bl_m_doc_no").val() != "" && jQuery("#Bl_m_doc_no").val() != null) { // Added by Chathura on 18-sep-2017 - else if added
                //    setInfoMsg("Selected House BL No is already assigned to a Master BL");
                //    return;
                //}
                Lobibox.confirm({
                    msg: "Do you want to add updates?",
                    callback: function ($this, type, ev) {
                        if (type == "yes") {
                            jQuery.ajax({
                                type: "GET",
                                url: "/MasterBL/AddHouseBLAll",
                                //url: "/HouseBL/HBLNoTextChange",
                                data: { BLNo: jQuery("#Bl_h_doc_no").val(), isEdit: isInternalEdit },
                                //data: { HBLNo: jQuery("#Bl_h_doc_no").val() },
                                contentType: "application/json;charset=utf-8",
                                dataType: "json",
                                success: function (result) {

                                    if (result.login == true) {

                                        if (result.success == true) {

                                            if (result.type == "Info") {
                                                setInfoMsg(result.msg);
                                                return;
                                            }
                                           
                                            jQuery(tr).find('td:eq(0)').html(jQuery("#Bl_h_doc_no").val());
                                            jQuery(tr).find('td:eq(1)').html(jQuery("#Bl_d_doc_no").val());
                                            jQuery(tr).find('td:eq(2)').html(jQuery("#Bl_job_no").val());
                                            jQuery(tr).find('td:eq(3)').html(jQuery("#Bl_pouch_no").val());
                                            jQuery(tr).find('td:eq(4)').html(jQuery("#Bl_cus_cd").val());

                                            ClearAllFieldsWOHBL(); // Added by Chathura on 18-sep-2017
                                            RemoveBLAllFunction();
                                            //DisplayData();

                                            jQuery(".btn-update-houseblall").hide();
                                            jQuery(".btn-add-houseblall").show();
                                            isInternalEdit = false;

                                            jQuery('.btn-add-houseblallsave').show();
                                            jQuery('.btn-update-hbl').hide();
                                        
                                            //jQuery('.btn-save').hide();
                                            //jQuery('.btn-add-houseblallsave').hide();

                                            jQuery("#houseblall").val("");
                                        } else {
                                            if (result.type == "Error") {
                                                setError(result.msg);

                                            }
                                            if (result.type == "Info") {
                                                setInfoMsg(result.msg);
                                            }
                                        }

                                        isInternalEdit = false;

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




    }

    jQuery('.btn-add-houseblallsave').click(function (e) {

        //// Below lines commented by Chathura on 18-sep-2017
        //check fields
        //if (jQuery("#Bl_job_no").val() == "") {
        //    setInfoMsg("Please Select Job No");
        //    return;
        //}
        //if (jQuery("#Bl_rmk").val() == "") {
        //    setInfoMsg("Please Select Remark");
        //    return;
        //}
        //if (jQuery("#Bl_doc_dt").val() == "") {
        //    setInfoMsg("Please Select Date");
        //    return;
        //}
        //if (jQuery("#Bl_h_doc_no").val() == "") {
        //    setInfoMsg("Please Select House BL No");
        //    return;
        //}
        Lobibox.confirm({
            msg: "Do you want to save?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    var formdata = jQuery("#draftbldata-data").serialize();
                    jQuery.ajax({
                        type: "GET",
                        url: "/MasterBL/SaveMasterBL",
                        data: formdata,
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    setSuccesssMsg(result.msg);
                                    ClearAllFields();
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


    jQuery("#Bl_doc_dt").focusout(function () {
        var code = jQuery(this).val();
        if (code == "") {
            jQuery('#Bl_doc_dt').val(my_date_format(new Date()));
        }
    });
    // Added by Chathura on 19-sep-2017
    jQuery("#Bl_d_doc_no").on("keydown", function (evt) {
    
        if (evt.keyCode == 113) {
            //var headerKeys = Array()
            //headerKeys = ["Row", "Code"];
            //field = "BL_D_DOC_NO"
            //var x = new CommonSearch(headerKeys, field);
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Ref.No", "Pouch No", "Date"];
            field = "BLnoD"
            var x = new CommonSearchDateFiltered(headerKeys, field);
        }
    });
    jQuery("#Bl_cus_cd").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            if (jQuery("#Bl_job_no").val() != null && jQuery("#Bl_job_no").val() != "") {
                var headerKeys = Array()
                headerKeys = ["Row", "Name", "Code", "NIC", "Mobile", "BR No"];
                field = "cusCodeJf";
                data = jQuery("#Bl_job_no").val();
              
                var x = new CommonSearch(headerKeys, field, data);
             
            }
            else {
                setInfoMsg("Please select Job No");
            }
        }
    });
    jQuery("#Bl_est_time_arr").focusout(function () {
        var code = jQuery(this).val();
        if (code == "") {
            jQuery('#Bl_est_time_arr').val(my_date_format(new Date()));
        }
    });
    jQuery("#Bl_est_time_dep").focusout(function () {
        var code = jQuery(this).val();
        if (code == "") {
            jQuery('#Bl_est_time_dep').val(my_date_format(new Date()));
        }
    });
    jQuery("#Bl_voage_dt").focusout(function () {
        var code = jQuery(this).val();
        if (code == "") {
            jQuery('#Bl_voage_dt').val(my_date_format(new Date()));
        }
    });
    jQuery("#bl_expires_dt").focusout(function () {
        var code = jQuery(this).val();
        if (code == "") {
            jQuery('#bl_expires_dt').val(my_date_format(new Date()));
        }
    });
    //f2 function 
    jQuery("#bld_grs_weight_uom").on("keydown", function (evt) {
        //alert("d");
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description"];
            field = "UOM"
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery("#bld_net_weight_uom").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description"];
            field = "UOM1"
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery("#bld_net_weight_uom").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description"];
            field = "UOM1"
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery("#bl_pack_uom").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description"];
            field = "UOM3"
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery("#Bl_h_doc_no").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            click_status = "H";
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Customer", "Agent", "Date"];
            field = "BL_H_DOC_NO"
            var x = new CommonSearchDateFiltered(headerKeys, field);
        }
    });
    jQuery("#Bl_m_doc_no").on("keydown", function (evt) { // Changed by Chathura on 27-sep-2017
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Master BL", "House BL", "Draft BL", "Date"];
            field = "BLnoM"
            var x = new CommonSearchDateFiltered(headerKeys, field);
        }
    });
    jQuery("#Bl_port_load").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Description", "Code"];
            field = "PORTS"
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery("#bl_terminal").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code"];
            field = "BL_TERMINAL"
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery("#Bl_port_discharge").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Description", "Code"];
            field = "PORTS2"
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery("#Bl_shipper_cd").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Name", "Code", "NIC", "Mobile", "BR No"];
            field = "cusCode4"
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery("#Bl_consignee_cd").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Name", "Code", "NIC", "Mobile", "BR No"];
            field = "cusCode5"
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery("#Bl_ntfy_party_cd").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Name", "Code", "NIC", "Mobile", "BR No"];
            field = "cusCode6"
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery("#Bl_agent_cd").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Name", "Code", "NIC", "Mobile", "BR No"];
            field = "cusCode7"
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery("#Bl_ship_line_cd").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Name", "Code", "NIC", "Mobile", "BR No"];
            field = "cusCode8"
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery("#Bl_vessal_no").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Description", "Code"];
            field = "VESSEL"
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery("#Bl_job_no").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Job No", "Pouch No", "Date", "Status"];
            field = "jobnobl"
            var x = new CommonSearchDateFiltered(headerKeys, field);
        }
    });
    jQuery("#bld_measure_uom").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description"];
            field = "Mesure"
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery("#bl_manual_d_ref").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Date", "Status"];
            field = "BL_MANUAL_D_REF"
            var x = new CommonSearch(headerKeys, field);
        }
    });

    $('#Bl_cus_cd').focusout(function () {
        var pc = $(this).val();
      
        if (jQuery("#Bl_cus_cd").val() != null && jQuery("#Bl_cus_cd").val() != "") {
            jQuery.ajax({
                cache: false,
                type: "GET",
                url: "/MasterBL/GetCustomerBasicDetails",
                data: { cuscode: jQuery("#Bl_cus_cd").val() },
                success: function (result) {
                    
                    if (result.login == true) {
                        if (result.success == true) {

                            jQuery('.tbl-cus-name .new-row').remove();
                            jQuery('.tbl-cus-name').append('<tr class="new-row">' +
                             '<td>' + result.data.MBE_NAME + '<br/>' + result.data.MBE_ADD1 + '<br/>' + result.data.MBE_ADD2
                             + '</td>' + '</tr>'
                            );
                       
                            //jQuery('.tbl-cus-name .new-row').remove();
                            //jQuery('.tbl-cus-name').append('<tr class="new-row">' +
                            //    '<td>' + (result.data != null) ? result.data.MBE_NAME : "" + '</td>' + '</tr>'
                            //   );
                        } else {
                            jQuery("#Bl_cus_cd").val("");
                            setInfoMsg('No Data Found! Please Check  Code!!');
                            return;
                        }
                    } else {
                        Logout();
                    }
                }
            });
        }
    });
    $('#Bl_shipper_cd').focusout(function () {
        var pc = $(this).val();
     
        if (jQuery("#Bl_shipper_cd").val() != null && jQuery("#Bl_shipper_cd").val() != "") {
            jQuery.ajax({
                cache: false,
                type: "GET",
                url: "/MasterBL/GetCustomerBasicDetails",
                data: { cuscode: jQuery("#Bl_shipper_cd").val() },
                success: function (result) {
                    
                    if (result.login == true) {
                        if (result.success == true) {

                            jQuery('.tbl-shipp-name .new-row').remove();
                            jQuery('.tbl-shipp-name').append('<tr class="new-row">' +
                             '<td>' + result.data.MBE_NAME + '<br/>' + result.data.MBE_ADD1 + '<br/>' + result.data.MBE_ADD2
                             + '</td>' + '</tr>'
                            );
                           
                            //jQuery('.tbl-shipp-name .new-row').remove();
                            //jQuery('.tbl-shipp-name').append('<tr class="new-row">' +
                            //    '<td>' + (result.data != null) ? result.data.MBE_NAME : "" + '</td>' + '</tr>'
                            //   );
                        } else {
                            jQuery("#Bl_shipper_cd").val("");
                            setInfoMsg('No Data Found! Please Check  Code!!');
                            return;
                        }
                    } else {
                        Logout();
                    }
                }
            });
        }
    });
    $('#Bl_consignee_cd').focusout(function () {
        var pc = $(this).val();
    
        if (jQuery("#Bl_consignee_cd").val() != null && jQuery("#Bl_consignee_cd").val() != "") {
            jQuery.ajax({
                cache: false,
                type: "GET",
                url: "/MasterBL/GetCustomerBasicDetails",
                data: { cuscode: jQuery("#Bl_consignee_cd").val() },
                success: function (result) {
                   
                    if (result.login == true) {
                        if (result.success == true) {

                            jQuery('.tbl-cons-name .new-row').remove();
                            jQuery('.tbl-cons-name').append('<tr class="new-row">' +
                             '<td>' + result.data.MBE_NAME + '<br/>' + result.data.MBE_ADD1 + '<br/>' + result.data.MBE_ADD2
                             + '</td>' + '</tr>'
                            );
                          
                            //jQuery('.tbl-cons-name .new-row').remove();
                            //jQuery('.tbl-cons-name').append('<tr class="new-row">' +
                            //    '<td>' + (result.data != null) ? result.data.MBE_NAME : "" + '</td>' + '</tr>'
                            //   );
                        } else {
                            jQuery("#Bl_consignee_cd").val("");
                            setInfoMsg('No Data Found! Please Check  Code!!');
                            return;
                        }
                    } else {
                        Logout();
                    }
                }
            });
        }
    });
    $('#Bl_ntfy_party_cd').focusout(function () {
        var pc = $(this).val();
       
        if (jQuery("#Bl_ntfy_party_cd").val() != null && jQuery("#Bl_ntfy_party_cd").val() != "") {
            jQuery.ajax({
                cache: false,
                type: "GET",
                url: "/MasterBL/GetCustomerBasicDetails",
                data: { cuscode: jQuery("#Bl_ntfy_party_cd").val() },
                success: function (result) {
                 
                    if (result.login == true) {
                        if (result.success == true) {

                            jQuery('.tbl-notify-name .new-row').remove();
                            jQuery('.tbl-notify-name').append('<tr class="new-row">' +
                             '<td>' + result.data.MBE_NAME + '<br/>' + result.data.MBE_ADD1 + '<br/>' + result.data.MBE_ADD2
                             + '</td>' + '</tr>'
                            );
                        
                            //jQuery('.tbl-notify-name .new-row').remove();
                            //jQuery('.tbl-notify-name').append('<tr class="new-row">' +
                            //    '<td>' + (result.data != null) ? result.data.MBE_NAME : "" + '</td>' + '</tr>'
                            //   );
                        } else {
                            jQuery("#Bl_ntfy_party_cd").val("");
                            setInfoMsg('No Data Found! Please Check  Code!!');
                            return;
                        }
                    } else {
                        Logout();
                    }
                }
            });
        }
    });
    $('#Bl_agent_cd').focusout(function () {
        var pc = $(this).val();
        
        if (jQuery("#Bl_agent_cd").val() != null && jQuery("#Bl_agent_cd").val() != "") {
            jQuery.ajax({
                cache: false,
                type: "GET",
                url: "/MasterBL/GetCustomerBasicDetails",
                data: { cuscode: jQuery("#Bl_agent_cd").val() },
                success: function (result) {
                    
                    if (result.login == true) {
                        if (result.success == true) {

                            jQuery('.tbl-agent-name .new-row').remove();
                            jQuery('.tbl-agent-name').append('<tr class="new-row">' +
                             '<td>' + result.data.MBE_NAME + '<br/>' + result.data.MBE_ADD1 + '<br/>' + result.data.MBE_ADD2
                             + '</td>' + '</tr>'
                            );
                          
                            //jQuery('.tbl-agent-name .new-row').remove();
                            //jQuery('.tbl-agent-name').append('<tr class="new-row">' +
                            //    '<td>' + (result.data != null) ? result.data.MBE_NAME : "" + '</td>' + '</tr>'
                            //   );
                        } else {
                            jQuery("#Bl_agent_cd").val("");
                            setInfoMsg('No Data Found! Please Check  Code!!');
                            return;
                        }
                    } else {
                        Logout();
                    }
                }
            });
        }
    });
    $('input#blct_pack').blur(function () {
        var num = parseFloat($(this).val());
        var cleanNum = num.toFixed(0);
        if (cleanNum == "NaN") {
            cleanNum = 0;
        }
        if (Number(cleanNum) < 0) {
            cleanNum = 0;
        }
        $(this).val(cleanNum);
    });
    $('#Bl_ship_line_cd').focusout(function () {
        var pc = $(this).val();
       
        if (jQuery("#Bl_ship_line_cd").val() != null && jQuery("#Bl_ship_line_cd").val() != "") {
            jQuery.ajax({
                cache: false,
                type: "GET",
                url: "/MasterBL/GetCustomerBasicDetails",
                data: { cuscode: jQuery("#Bl_ship_line_cd").val() },
                success: function (result) {
                  
                    if (result.login == true) {
                        if (result.success == true) {

                            jQuery('.tbl-carr-name .new-row').remove();
                            jQuery('.tbl-carr-name').append('<tr class="new-row">' +
                             '<td>' + result.data.MBE_NAME + '<br/>' +result.data.MBE_ADD1 + '<br/>' +result.data.MBE_ADD2
                             + '</td>' + '</tr>'
                            );
                        
                            //jQuery('.tbl-carr-name .new-row').remove();
                            //jQuery('.tbl-carr-name').append('<tr class="new-row">' +
                            //    '<td>' + (result.data != null) ? result.data.MBE_NAME : "" + '</td>' + '</tr>'
                            //   );
                        } else {
                            setInfoMsg('No Data Found! Please Check  Code!!');
                            return;
                        }
                    } else {
                        Logout();
                    }
                }
            });
        }
    });




    /* Added by Chathura on 18-sep-2017 */
    function ClearAllFieldsWOHBL() {
        jQuery("#Bl_job_no").val("");
        jQuery("#Bl_pouch_no").val("");
        jQuery("#Bl_h_doc_no").val("");
        jQuery("#Bl_doc_dt").val("");
        //jQuery("#Bl_m_doc_no").val("");
        jQuery("#bl_or_seq").val("");
        jQuery("#Bl_freight_charg").val("");
        jQuery("#Bl_port_load").val("");
        //flight no
        jQuery("#bl_terminal").val("");
        jQuery("#Bl_port_discharge").val("");
        jQuery("#bl_pay_mode").val("");
        jQuery("#bl_vsl_oper").val("");
        jQuery("#Bl_port_discharge").val("");
        // final desti jQuery("#final_desti").val(data[0].final_desti);
        //Cargo_type
        jQuery("#bl_cntr_oper").val("");
        jQuery("#Bl_palce_rec").val("");
        jQuery("#Bl_voage_no").val("");
        jQuery("#Bl_est_time_arr").val("");
        // Move_in_type
        jQuery("#Bl_voage_dt").val("");
        jQuery("#Bl_est_time_dep").val("");
        jQuery("#bl_expires_dt").val("");
        jQuery("#Bl_rmk").val("");
        jQuery("#Bl_cus_cd").val("");
        jQuery("#Bl_shipper_cd").val("");
        jQuery("#Bl_consignee_cd").val("");
        jQuery("#Bl_ntfy_party_cd").val("");
        jQuery("#Bl_agent_cd").val("");
        jQuery("#Bl_d_doc_no").val("");
        jQuery("#Bl_ship_line_cd").val("");
        jQuery("#bl_pack_uom").val("");
        jQuery("#Bl_vessal_no").val("");
        jQuery('.tbl-container .new-row').remove();
        jQuery("#bld_package_nos").val("");
        jQuery("#bld_grs_weight").val("");
        jQuery("#bld_grs_weight_uom").val("");
        jQuery("#bld_net_weight").val("");
        jQuery("#bld_net_weight_uom").val("");
        jQuery("#bld_measure").val("");
        jQuery("#bld_measure_uom").val("");
        jQuery("#bld_mark_nos").val("");
        jQuery("#bld_desc_goods").val("");
        jQuery("#Bl_palce_del").val("");
        jQuery('#Bl_doc_dt').val(my_date_format_with_time(new Date()));
        jQuery("#bl_manual_d_ref").val("");
        jQuery("#bl_manual_h_ref").val("");
        jQuery("#bl_manual_m_ref").val("");
        jQuery('.tbl-cus-name .new-row').remove();
        jQuery('.tbl-shipp-name .new-row').remove();
        jQuery('.tbl-cons-name .new-row').remove();
        jQuery('.tbl-notify-name .new-row').remove();
        jQuery('.tbl-agent-name .new-row').remove();
        jQuery('.tbl-carr-name .new-row').remove();

        click_status = "N";
        isInternalEdit = false;
        hasMaster = false;

        if (jQuery("#Bl_m_doc_no").val() != null && jQuery("#Bl_m_doc_no").val() != "") {
            $(".btn-save").html('<i class="glyphicon glyphicon-ok-circle"></i> Update');
        }
        else {
            $(".btn-save").html('<i class="glyphicon glyphicon-ok-circle"></i> Save');
        }

        jQuery('#Bl_m_doc_no').attr("readonly", "false");
        jQuery('#bl_manual_m_ref').attr("readonly", "false");
        jQuery('#bl_manual_h_ref').attr("readonly", "false");
        jQuery('#Bl_m_doc_no').attr("style", "color:#000;");

        jQuery('#Bl_est_time_arr').val(my_date_format_tran(my_date_format_with_time(new Date()).toString()));
        jQuery('#Bl_est_time_dep').val(my_date_format_tran(my_date_format_with_time(new Date()).toString()));
        jQuery('#bl_expires_dt').val(my_date_format_tran(my_date_format_with_time(new Date()).toString()));
        jQuery('#Bl_voage_dt').val(my_date_format_tran(my_date_format_with_time(new Date()).toString()));
        //jQuery('.tbl-houseblall .new-row').remove();
        //jQuery('.btn-save').show();
        //jQuery('.btn-add-houseblallsave').hide();
        //jQuery.ajax({
        //    type: "GET",
        //    url: "/DraftBL/ClearSession",
        //    data: {},
        //    contentType: "application/json;charset=utf-8",
        //    dataType: "json",
        //})
    }

    function changeComponentAttributes() {
        var value = $("#prfSession").data('value');
        var valuechnl = $("#chnlSession").data('value');
     
        jQuery('#Bl_ntfy_party_cd').val("N/A");
        jQuery('#Bl_agent_cd').val("N/A");
        jQuery('#Bl_ship_line_cd').val("N/A");
        jQuery('#Bl_cus_cd').val("N/A");
        jQuery('#Bl_shipper_cd').val("N/A");
        jQuery('#Bl_consignee_cd').val("N/A");

        jQuery("#Bl_port_load").val("");
        jQuery("#Bl_port_discharge").val("LKCMB");
        jQuery("#Bl_palce_del").val("COLOMBO");

        if (valuechnl == "SEA") {
            jQuery("#carriersl").text("Shipping Line");
            jQuery("#vesselflight").text("Vessel");
            jQuery("#movecargotype").text("Move Out Type");
        }
        else {
            jQuery("#carriersl").text("Air Line");
            jQuery("#vesselflight").text("Flight");
            jQuery("#movecargotype").text("Move Out Type");
        }

        if (value == "SIM") {//"IMPS"
            jQuery("#vesselflight").text("Vessel");
            jQuery("#carriersl").text("Shipping Line");
            jQuery("#movecargotype").text("Move Out Type");
        }
        else if (value == "AIM") {//"IMPA"
            jQuery("#vesselflight").text("Flight");
            jQuery("#carriersl").text("Air Line");
            jQuery("#movecargotype").text("Move Out Type");
        }
        else if (value == "SXP") {//"EXPS"
            jQuery("#vesselflight").text("Vessel");
            jQuery("#carriersl").text("Carrier");
            jQuery("#movecargotype").text("Cargo Type");
            jQuery('#Bl_freight_charg').attr("disabled", "true");
            jQuery('#bld_package_nos').attr("disabled", "true");
            jQuery('#bl_pack_uom').attr("disabled", "true");
            jQuery('.btn-pack-uom').attr("disabled", "true");
            jQuery('#Bl_palce_rec').attr("disabled", "true");
            jQuery('#Bl_load_in_tp').attr("disabled", "true");
            jQuery('#Bl_voage_dt').attr("disabled", "true");
            jQuery('#bl_expires_dt').attr("disabled", "true");
            //jQuery('#Bl_voage_no').attr("disabled", "true");
            jQuery("#Bl_port_load").val("LKCMB");
            jQuery("#Bl_port_discharge").val("");
            jQuery("#Bl_palce_del").val("");
        }
        else if (value == "AEX") {//"EXPA"
            jQuery("#vesselflight").text("Flight");
            jQuery("#carriersl").text("Carrier");
            jQuery("#movecargotype").text("Cargo Type");
            jQuery('#Bl_freight_charg').attr("disabled", "true");
            jQuery('#bld_package_nos').attr("disabled", "true");
            jQuery('#bl_pack_uom').attr("disabled", "true");
            jQuery('.btn-pack-uom').attr("disabled", "true");
            jQuery('#Bl_palce_rec').attr("disabled", "true");
            jQuery('#Bl_load_in_tp').attr("disabled", "true");
            jQuery('#Bl_voage_dt').attr("disabled", "true");
            jQuery('#bl_expires_dt').attr("disabled", "true");
            jQuery('#bl_terminal').attr("disabled", "true");
            jQuery('.btn-terminal').attr("disabled", "true");
            jQuery('#bl_vsl_oper').attr("disabled", "true");
            jQuery('#bl_cntr_oper').attr("disabled", "true");
            jQuery('#Bl_voage_no').attr("disabled", "true");

        }
        else {
            //jQuery("#vesselflight").text("Flight");
            //jQuery("#carriersl").text("Air Line");

            jQuery('#Bl_freight_charg').attr("disabled", "false");
            jQuery('#bld_package_nos').attr("disabled", "false");
            jQuery('#bl_pack_uom').attr("disabled", "false");
            jQuery('.btn-pack-uom').attr("disabled", "false");
            jQuery('#Bl_palce_rec').attr("disabled", "false");
            jQuery('#Bl_load_in_tp').attr("disabled", "false");
            jQuery('#Bl_voage_dt').attr("disabled", "false");
            jQuery('#bl_expires_dt').attr("disabled", "false");
            jQuery('#bl_terminal').attr("disabled", "false");
            jQuery('.btn-terminal').attr("disabled", "false");
            jQuery('#bl_vsl_oper').attr("disabled", "false");
            jQuery('#bl_cntr_oper').attr("disabled", "false");
            jQuery('#Bl_voage_no').attr("disabled", "false");
        }
    }

    function validateSaveMandatory() {
        //if ((jQuery("#Bl_d_doc_no").val() == null && jQuery("#Bl_d_doc_no").val() == "") && ($('#Bl_d_doc_no').is('[disabled=disabled]')) == false) { setInfoMsg(""); return false; }
        //if ((jQuery("#Bl_job_no").val() == null || jQuery("#Bl_job_no").val() == "") && ($('#Bl_job_no').is('[disabled=disabled]')) == false) { setInfoMsg("Job ID cannot be empty"); return false; }
        //if ((jQuery("#Bl_pouch_no").val() == null || jQuery("#Bl_pouch_no").val() == "") && ($('#Bl_pouch_no').is('[disabled=disabled]')) == false) { setInfoMsg("Pouch ID cannot be empty"); return false; }
        if ((jQuery("#bl_manual_h_ref").val() == null || jQuery("#bl_manual_h_ref").val() == "") && ($('#bl_manual_h_ref').is('[disabled=disabled]')) == false) { setInfoMsg("Manual House B/L Ref cannot be empty"); return false; }
        if ((jQuery("#Bl_doc_dt").val() == null || jQuery("#Bl_doc_dt").val() == "") && ($('#Bl_doc_dt').is('[disabled=disabled]')) == false) { setInfoMsg("Date cannot be empty"); return false; }
        if ((jQuery("#bl_manual_d_ref").val() == null || jQuery("#bl_manual_d_ref").val() == "") && ($('#bl_manual_d_ref').is('[disabled=disabled]')) == false) { setInfoMsg("Manual Draft B/L cannot be empty"); return false; }
        if ((jQuery("#bl_manual_m_ref").val() == null || jQuery("#bl_manual_m_ref").val() == "") && ($('#bl_manual_m_ref').is('[disabled=disabled]')) == false) { setInfoMsg("Manual Master B/L Ref cannot be empty"); return false; }
        if ((jQuery("#bl_or_seq").val() == null || jQuery("#bl_or_seq").val() == "") && ($('#bl_or_seq').is('[disabled=disabled]')) == false) { setInfoMsg("Seq No cannot be empty"); return false; }

        if ((jQuery("#Bl_cus_cd").val() == null || jQuery("#Bl_cus_cd").val() == "") && ($('#Bl_cus_cd').is('[disabled=disabled]')) == false) { setInfoMsg("Customer cannot be empty"); return false; }
        if ((jQuery("#Bl_shipper_cd").val() == null || jQuery("#Bl_shipper_cd").val() == "") && ($('#Bl_shipper_cd').is('[disabled=disabled]')) == false) { setInfoMsg("Shipper cannot be empty"); return false; }
        if ((jQuery("#Bl_consignee_cd").val() == null || jQuery("#Bl_consignee_cd").val() == "") && ($('#Bl_consignee_cd').is('[disabled=disabled]')) == false) { setInfoMsg("Consignee cannot be empty"); return false; }
        if ((jQuery("#Bl_ntfy_party_cd").val() == null || jQuery("#Bl_ntfy_party_cd").val() == "") && ($('#Bl_ntfy_party_cd').is('[disabled=disabled]')) == false) { setInfoMsg("Notify Party cannot be empty"); return false; }
        if ((jQuery("#Bl_agent_cd").val() == null || jQuery("#Bl_agent_cd").val() == "") && ($('#Bl_agent_cd').is('[disabled=disabled]')) == false) { setInfoMsg("Agent cannot be empty"); return false; }
        if ((jQuery("#Bl_ship_line_cd").val() == null || jQuery("#Bl_ship_line_cd").val() == "") && ($('#Bl_ship_line_cd').is('[disabled=disabled]')) == false) { setInfoMsg("Shipping Line cannot be empty"); return false; }

        if ((jQuery("#Bl_freight_charg").val() == null || jQuery("#Bl_freight_charg").val() == "") && ($('#Bl_freight_charg').is('[disabled=disabled]')) == false) { setInfoMsg("Freight Charge cannot be empty"); return false; }
        //if ((jQuery("#bl_pay_mode").val() == null || jQuery("#bl_pay_mode").val() == "") && ($('#bl_pay_mode').is('[disabled=disabled]')) == false) { setInfoMsg(""); return false; }
        if ((jQuery("#bld_package_nos").val() == null || jQuery("#bld_package_nos").val() == "") && ($('#bld_package_nos').is('[disabled=disabled]')) == false) { setInfoMsg("Packages cannot be empty"); return false; }
        if ((jQuery("#bl_pack_uom").val() == null || jQuery("#bl_pack_uom").val() == "") && ($('#bl_pack_uom').is('[disabled=disabled]')) == false) { setInfoMsg("Packages measurement unit cannot be empty"); return false; }
        if ((jQuery("#bld_grs_weight").val() == null || jQuery("#bld_grs_weight").val() == "") && ($('#bld_grs_weight').is('[disabled=disabled]')) == false) { setInfoMsg("Gross Weight cannot be empty"); return false; }
        if ((jQuery("#bld_grs_weight_uom").val() == null || jQuery("#bld_grs_weight_uom").val() == "") && ($('#bld_grs_weight_uom').is('[disabled=disabled]')) == false) { setInfoMsg("Gross Weight measurment unit cannot be empty"); return false; }
        if ((jQuery("#bld_net_weight").val() == null || jQuery("#bld_net_weight").val() == "") && ($('#bld_net_weight').is('[disabled=disabled]')) == false) { setInfoMsg("Net Weight cannot be empty"); return false; }
        if ((jQuery("#bld_net_weight_uom").val() == null || jQuery("#bld_net_weight_uom").val() == "") && ($('#bld_net_weight_uom').is('[disabled=disabled]')) == false) { setInfoMsg("Net Weight measurement unit cannot be empty"); return false; }
        if ((jQuery("#bld_measure").val() == null || jQuery("#bld_measure").val() == "") && ($('#bld_measure').is('[disabled=disabled]')) == false) { setInfoMsg("Measurment cannot be empty"); return false; }
        if ((jQuery("#bld_measure_uom").val() == null || jQuery("#bld_measure_uom").val() == "") && ($('#bld_measure_uom').is('[disabled=disabled]')) == false) { setInfoMsg("Measurement type cannot be empty"); return false; }
        if ((jQuery("#Bl_port_load").val() == null || jQuery("#Bl_port_load").val() == "") && ($('#Bl_port_load').is('[disabled=disabled]')) == false) { setInfoMsg("Load Port cannot be empty"); return false; }
        if ((jQuery("#Bl_port_discharge").val() == null || jQuery("#Bl_port_discharge").val() == "") && ($('#Bl_port_discharge').is('[disabled=disabled]')) == false) { setInfoMsg("Discharge Port cannot be empty"); return false; }
        if ((jQuery("#Bl_palce_del").val() == null || jQuery("#Bl_palce_del").val() == "") && ($('#Bl_palce_del').is('[disabled=disabled]')) == false) { setInfoMsg("Final destination cannot be empty"); return false; }
        if ((jQuery("#Bl_palce_rec").val() == null || jQuery("#Bl_palce_rec").val() == "") && ($('#Bl_palce_rec').is('[disabled=disabled]')) == false) { setInfoMsg("Place Receipt cannot be empty"); return false; }
        if ((jQuery("#Bl_load_in_tp").val() == null || jQuery("#Bl_load_in_tp").val() == "") && ($('#Bl_load_in_tp').is('[disabled=disabled]')) == false) { setInfoMsg("Move in Type cannot be empty"); return false; }
        if ((jQuery("#Bl_load_out_tp").val() == null || jQuery("#Bl_load_out_tp").val() == "") && ($('#Bl_load_out_tp').is('[disabled=disabled]')) == false) { setInfoMsg("Move out Type cannot be empty"); return false; }
        if ((jQuery("#Bl_vessal_no").val() == null || jQuery("#Bl_vessal_no").val() == "") && ($('#Bl_vessal_no').is('[disabled=disabled]')) == false) { setInfoMsg("vessel cannot be empty"); return false; }
        if ((jQuery("#Bl_voage_no").val() == null || jQuery("#Bl_voage_no").val() == "") && ($('#Bl_voage_no').is('[disabled=disabled]')) == false) { setInfoMsg("Voyage cannot be empty"); return false; }
        if ((jQuery("#Bl_voage_dt").val() == null || jQuery("#Bl_voage_dt").val() == "") && ($('#Bl_voage_dt').is('[disabled=disabled]')) == false) { setInfoMsg("Voyage date cannot be empty"); return false; }
        if ((jQuery("#Bl_est_time_arr").val() == null || jQuery("#Bl_est_time_arr").val() == "") && ($('#Bl_est_time_arr').is('[disabled=disabled]')) == false) { setInfoMsg("ETA cannot be empty"); return false; }
        if ((jQuery("#Bl_est_time_dep").val() == null || jQuery("#Bl_est_time_dep").val() == "") && ($('#Bl_est_time_dep').is('[disabled=disabled]')) == false) { setInfoMsg("ETD cannot be empty"); return false; }
        if ((jQuery("#bl_expires_dt").val() == null || jQuery("#bl_expires_dt").val() == "") && ($('#bl_expires_dt').is('[disabled=disabled]')) == false) { setInfoMsg("Expiry Date cannot be empty"); return false; }
        if ((jQuery("#bl_terminal").val() == null || jQuery("#bl_terminal").val() == "") && ($('#bl_terminal').is('[disabled=disabled]')) == false) { setInfoMsg("Terminal ID cannot be empty"); return false; }
        if ((jQuery("#bl_vsl_oper").val() == null || jQuery("#bl_vsl_oper").val() == "") && ($('#bl_vsl_oper').is('[disabled=disabled]')) == false) { setInfoMsg("VSL Operator cannot be empty"); return false; }
        if ((jQuery("#bl_cntr_oper").val() == null || jQuery("#bl_cntr_oper").val() == "") && ($('#bl_cntr_oper').is('[disabled=disabled]')) == false) { setInfoMsg("CNTR Operator cannot be empty"); return false; }
        if ((jQuery("#Bl_rmk").val() == null || jQuery("#Bl_rmk").val() == "") && ($('#Bl_rmk').is('[disabled=disabled]')) == false) { setInfoMsg("Remarks cannot be empty"); return false; }
        if ((jQuery("#bld_mark_nos").val() == null || jQuery("#bld_mark_nos").val() == "") && ($('#bld_mark_nos').is('[disabled=disabled]')) == false) { setInfoMsg("Marks and Numbers cannot be empty"); return false; }
        if ((jQuery("#bld_desc_goods").val() == null || jQuery("#bld_desc_goods").val() == "") && ($('#bld_desc_goods').is('[disabled=disabled]')) == false) { setInfoMsg("Description of goods cannot be empty"); return false; }



        return true;
    }


});