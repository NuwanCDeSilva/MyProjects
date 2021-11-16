jQuery(document).ready(function () {
    jQuery('#stdate').val(my_date_format(new Date()));
    jQuery('#enddate').val(my_date_format(new Date()));
    LoadPcs();
    LoadTypes();
    function LoadTypes() {
        jQuery.ajax({
            type: "GET",
            url: "/SunUpload/LoadTypes",
            data: {},
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        //var select = document.getElementById("Type");
                        jQuery("#Type").empty();
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
                        //select.insertAdjacentHTML('beforeEnd', options.join('\n'));
                    } else {
                        setError(result.msg);
                    }
                } else {
                    Logout();
                }
            }
        })
    }

    function clearForm() {
        LoadPcs();

    }
    LoadDevTypes();
    function LoadDevTypes() {
        jQuery.ajax({
            type: "GET",
            url: "/SunUpload/LoadDevTypes",
            data: {},
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("DvType");
                        jQuery("#DvType").empty();
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
    
    function LoadPcs() {
        jQuery.ajax({
            type: "GET",
            url: "/SunUpload/LoadPcs",
            //data: { type: jQuery('#Type').val() },
            data: { type: "Debtor" },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        //LoadPC
                        SetPcDataList(result.data);
                    } else {
                        Logout();
                    }
                }
            }
        })
    }

    jQuery("#Type").change(function () {
        if (jQuery('#Type').val() == "Debtor")
        {
            jQuery.ajax({
                type: "GET",
                url: "/SunUpload/LoadPcs",
                data: { type: jQuery('#Type').val() },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result)
                {
                    if (result.login == true)
                    {
                        if (result.success = true)
                        {
                            //LoadPC
                            SetPcDataList(result.data);
                        } else {
                            Logout();
                        }
                    }
                }
            })
        }
    });

    function SetPcDataList(data) {
        jQuery('.tbl-pc-list .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {
                jQuery('table.tbl-pc-list').append('<tr class="new-row">' +
                     '<td><input type="checkbox" id="chkpc" name="chkpc" value="' + data[i].PC + '"></td>' +
                                           '<td>' + data[i].PC + '</td>' +
                                           '</tr>');
            }
        }
    }
    jQuery(".btn-clear").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to clear data ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    window.location.href = "/SunUpload";
                }
            }
        })

    });
    jQuery('.btn-upload').click(function (e) {

        Lobibox.confirm({
            msg: "Do you want to continue process?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    jQuery.ajax({
                        type: "GET",
                        url: "/SunUpload/Upload",
                        data: { pcs: $('input[name="chkpc"]:checked').serialize(), fdate: jQuery('#stdate').val(), tdate: jQuery('#enddate').val(), Dtype: jQuery('#DvType').val() },
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    setSuccesssMsg(result.msg);
                                    jQuery("#path").val(result.path);
                                    clearForm();

                                } else {
                                        setInfoMsg(result.msg);
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



    jQuery("#CHEKALL").unbind('click').click(function (evt) {
        var txt = "";
        if (jQuery('#CHEKALL').is(":checked")) {
            //jQuery('#chkpc').prop("checked", true);
            $("input[name='chkpc']").prop("checked", $(this).is(":checked"));
            //if (data.length > 0) {
            //    for (i = 0; i < data.length; i++) {
            //        jQuery('#chkpc_'+i +'').prop("checked", true);
            //    }
            //}
            
            txt = "ALL";
        } else

        {
            $("input[name='chkpc']").prop("checked", $(this).is(":checked"));
    //jQuery('#chkpc').prop("checked", false);
            //if (data.length > 0) {
            //    for (i = 0; i < data.length; i++) {
            //        jQuery('#chkpc_'+i +'').prop("checked", false);
            //    }
            //}           
            txt = "NONE";
        }
        //jQuery.ajax({
        //    type: "GET",
        //    url: "/InventoryAge/updateCheckStatus",
        //    contentType: "application/json;charset=utf-8",
        //    data: { type: txt },
        //    dataType: "json",
        //    success: function (result) {
        //        if (result.login == true) {
        //            if (result.success == true) {

        //            }
        //        } else {
        //            Logout();
        //        }
        //    }
        //});
    });

    jQuery("#stdate").focusout(function () {
        var code = jQuery(this).val();
        if (code == "") {
            jQuery('#stdate').val(my_date_format(new Date()));
        }
    });
    jQuery("#enddate").focusout(function () {
        var code = jQuery(this).val();
        if (code == "") {
            jQuery('#enddate').val(my_date_format(new Date()));
        }
    });
});