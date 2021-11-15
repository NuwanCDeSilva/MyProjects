jQuery(document).ready(function () {
    //jQuery(".enq-id-search-new").click(function () {
    //    var headerKeys = Array()
    //    headerKeys = ["Row", "Enquiry Id", "Ref Num", "Type", "Customer Code", "Name", "Address"];
    //    field = "InvEnqNew"
    //    var x = new CommonSearch(headerKeys, field);
    //});

    jQuery(".popup-enq-ser").click(function (evt) {
        var enqid = $(this).data('enq-id');
        var enq = $(this).data('enq-rem');
        $.ajax({
            url: "/Home/GetEnqSerData",
            type: 'GET',
            cache: false,
            data: { enqid: enqid },
            success: function (result) {
                if (result != null) {
                    jQuery('table.enqser-by-enqid .new-row').remove();
                    for (i = 0; i < result.data.length; i++) {
                        jQuery('table.enqser-by-enqid ').append('<tr class="new-row"><td>'
                            + result.data[i].GCS_ENQ_ID + '</td><td>'
                            + result.data[i].GCS_PICK_TN + '</td><td>'
                            + result.data[i].GCS_DROP_TN + '</td><td>'
                           + result.data[i].GCS_COMMENT + '</td></tr>');

                    }
                    jQuery('.enq-rem').empty();
                    jQuery('.enq-rem').append('<p><span style="font-weight:bolder"> Remark: </span>' + enq + '</p>');
                }
            }
        });
    })
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

    jQuery(".documents-uploads-process").click(function () {
        var enqid = $(this).data('enq-id');
        jQuery.ajax({
            type: 'GET',
            cache: false,
            url: "/Home/SetEnqId",
            data: { enqid: enqid },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.data != null) {
                            jQuery("#job_number").val(enqid);
                            jQuery("#job_number2").val(enqid);
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

    jQuery(".send-sms-driver").click(function () {
        var enqid = $(this).data('enq-id');
        jQuery.ajax({
            type: 'GET',
            cache: false,
            url: "/Home/senEnquirySMSDriver",
            data: { enqid: enqid },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.msg != null)
                        {
                            setSuccesssMsg(result.msg);
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
    jQuery(".send-sms-cust").click(function () {
        var enqid = $(this).data('enq-id');
        jQuery.ajax({
            type: 'GET',
            cache: false,
            url: "/Home/senEnquirySMSCustomer",
            data: { enqid: enqid },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.msg != null) {
                            setSuccesssMsg(result.msg);
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

    jQuery(".fleet-alert-reg").click(function () {
        jQuery.ajax({
            type: 'GET',
            cache: false,
            url: "/Home/FleetAlertreg",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.msg != null) {
                            setInfoMsg(result.msg);
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
    jQuery(".fleet-alert-lis").click(function () {
        jQuery.ajax({
            type: 'GET',
            cache: false,
            url: "/Home/FleetAlertlis",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.msg != null) {
                            setInfoMsg(result.msg);
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
});