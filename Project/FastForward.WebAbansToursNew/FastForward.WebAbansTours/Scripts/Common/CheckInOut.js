jQuery(document).ready(function () {
    jQuery('#CHK_OUT_DTE').datetimepicker({ dateFormat: "dd/M/yy", timeFormat: "HH:mm" });
    jQuery('#CHK_IN_DTE').datetimepicker({ dateFormat: "dd/M/yy", timeFormat: "HH:mm" });
    jQuery('#CHK_OUT_DTE').val(my_date_format_with_time(new Date()));
    jQuery('#CHK_IN_DTE').val(my_date_format_with_time(new Date()));
    if (jQuery("#CHK_ENQ_ID").val() != "") {
        enqFocusOut(jQuery("#CHK_ENQ_ID").val());
    }
    //check numbers and decimal  only
    jQuery('#CHK_OUT_KM,#CHK_OUT_FUEL,#CHK_IN_KM,#CHK_IN_FUEL,#CHK_OTH_CHRG').on("input", function (event) {
        if (!jQuery.isNumeric(this.value)) {
            this.value = "";
        }
        if (parseFloat(this.value) < 0) {
            this.value = "";
        }

    });
    $('#CHK_OUT_KM').focusout(function () {
        var str = $(this).val();
        var numRange = /^[0-9+]+$/;
        if (!numRange.test(str))
        {
            setInfoMsg('Please enter a valid distance !!!');
            $(this).val('');
        } else {
            var indis = jQuery("#CHK_IN_KM").val();
            if (Number(indis) > Number(str))
            {
                setInfoMsg('Please Check Out In Distance!! ');
                $(this).val('');
            }
        }
    });
    $('#CHK_IN_KM').focusout(function () {
        var str = $(this).val();
        var numRange = /^[0-9+]+$/;
        if (!numRange.test(str)) {
            setInfoMsg('Please enter a valid distance !!!');
            $(this).val('');
        } else {
            var indis = jQuery("#CHK_OUT_KM").val();
            if (Number(indis) < Number(str)) {
                setInfoMsg('Please Check Out In Distance!! ');
                $(this).val('');
            }
        }
    });
    jQuery('#CHK_OUT_KM,#CHK_OUT_FUEL,#CHK_IN_KM,#CHK_IN_FUEL,#CHK_OTH_CHRG').keypress(function (event) {
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
    jQuery(".btn-chk-clear-data").click(function (evt) { 
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to clear data ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    clearChkFrom();
                }
            }
        });
    });
    jQuery(".btn-chk-save-data").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to continue ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    var formdata = jQuery("#checkin-frm").serialize();
                    jQuery.ajax({
                        type: "GET",
                        url: "/CheckInOut/saveCheckData",
                        contentType: "application/json;charset=utf-8",
                        data: formdata,
                        dataType: "json",
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    setSuccesssMsg(result.msg);
                                    jQuery("#img-upload-frm").submit();
                                    clearChkFrom();
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
    });
    jQuery("#CHK_ENQ_ID").focusout(function (evt) {
        if (jQuery(this).val() != "") {
            enqFocusOut(jQuery(this).val());
        }
    });
    jQuery("#CHK_ENQ_ID").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Enquiry Id", "Ref Num", "Customer Code", "Name", "Address"];
            field = "chktransportEnq"
            var x = new CommonSearch(headerKeys, field);
        } else if (evt.keyCode == 13) {
            enqFocusOut(jQuery(this).val());
        }
    });
    jQuery(".chkenq-id-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Enquiry Id", "Ref Num", "Customer Code", "Name", "Address"];
        field = "chktransportEnq"
        var x = new CommonSearch(headerKeys, field);
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
                        if (result.data.length > 0) {
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
    });

    function setImagesValue(data) {
        jQuery('.image-view-grid ').empty();
        if (data != null)
        {
      
            for (i = 0; i < data.length; i++) {
                if (ImageExist(data[i].Jbimg_img_path + data[i].Jbimg_img) == true)
                {
                    jQuery('.image-view-grid ').append('<a href="' + data[i].Jbimg_img_path + data[i].Jbimg_img + '" target="_blank"> <img src="' + data[i].Jbimg_img_path + data[i].Jbimg_img + '" class="upload-images col-md-3" title="Search"></a>');
                } 
          

            }
        } 
    }
    jQuery('.image-submit').click(function () {
        jQuery("#img-upload-frm").submit();
    });
    function ImageExist(url) {
        var http = new XMLHttpRequest();
        http.open('GET', url, false);
        http.send();
        return http.status != 404;
    }
});
function clearChkFrom() {
    jQuery("#CHK_OUT_KM").removeAttr("readonly");
    jQuery("#CHK_OUT_FUEL").removeAttr("readonly");
    jQuery('#CHK_OUT_DTE').val(my_date_format_with_time(new Date()));
    jQuery('#CHK_IN_DTE').val(my_date_format_with_time(new Date()));
    jQuery("#CHK_ENQ_ID").val("");
    jQuery("#CHK_OUT_KM").val("");
    jQuery("#CHK_OUT_FUEL").val("");
    jQuery("#CHK_IN_KM").val("");
    jQuery("#CHK_IN_FUEL").val("");
    jQuery("#CHK_OTH_CHRG").val("");
    jQuery("#CHK_RMKS").val("");
    jQuery('#CHK_OUT_DTE').datetimepicker({ dateFormat: "dd/M/yy", timeFormat: "HH:mm" });
}
function enqFocusOut(enqId) {
    jQuery.ajax({ 
        type: "GET",
        url: "/CheckInOut/enquiryDataLoad",
        contentType: "application/json;charset=utf-8",
        data: { enqId: enqId },
        dataType: "json",
        success: function (result) {
            if (result.login == true) {
                if (result.success == true) {
                    if (typeof result.chkOut!="undefined")
                        setChkValues(result.chkOut)
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
function setChkValues(data) {
    clearChkFrom();
    jQuery('#CHK_OUT_DTE').val(my_date_format_with_time(convertDate(data.CHK_OUT_DTE)));
    jQuery('#CHK_IN_DTE').val(my_date_format_with_time(convertDate(data.CHK_IN_DTE)));
    jQuery("#CHK_ENQ_ID").val(data.CHK_ENQ_ID);
    jQuery("#CHK_OUT_KM").val(data.CHK_OUT_KM);
    jQuery("#CHK_OUT_FUEL").val(data.CHK_OUT_FUEL);
    jQuery("#CHK_IN_KM").val(data.CHK_IN_KM);
    jQuery("#CHK_IN_FUEL").val(data.CHK_IN_FUEL);
    jQuery("#CHK_OTH_CHRG").val(data.CHK_OTH_CHRG);
    jQuery("#CHK_RMKS").val(data.CHK_RMKS);
    disableField();
}

function disableField() {
    jQuery('#CHK_OUT_DTE').attr("readonly",true);
    jQuery("#CHK_OUT_KM").attr("readonly", true);
    jQuery("#CHK_OUT_FUEL").attr("readonly", true);
    jQuery("#CHK_OUT_DTE").datepicker("destroy");
}