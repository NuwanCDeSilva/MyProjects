jQuery(document).ready(function () {

    jQuery('#MSTF_REG_EXP').datepicker({ dateFormat: "dd/M/yy" })
    jQuery('#MSTF_INSU_EXP').datepicker({ dateFormat: "dd/M/yy" })
    jQuery('#MSTF_DT').datepicker({ dateFormat: "dd/M/yy" })
   // jQuery('#MFD_FRM_DT').datepicker({ dateFormat: "dd/M/yy" })
   // jQuery('#MFD_TO_DT').datepicker({ dateFormat: "dd/M/yy" })
    jQuery('#MFD_FRM_DT').datetimepicker({ minDate: new Date(), dateFormat: "dd/M/yy", timeFormat: "HH:mm" });
    jQuery('#MFD_TO_DT').datetimepicker({ minDate: new Date(), dateFormat: "dd/M/yy", timeFormat: "HH:mm" });
    function loadfleetStatus() {
        jQuery.ajax({
            type: "GET",
            url: "/FleetDefinition/LoadStatus",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        var select = document.getElementById("MFD_ACT");
                        jQuery("#MFD_ACT").empty();
                        var options = [];
                        var option = document.createElement('option');
                        if (result.data != null && result.data.length != 0) {
                            for (var i = 0; i < result.data.length; i++) {
                                option.text = result.data[i].Text;
                                option.value = result.data[i].Value;
                                options.push(option.outerHTML);
                            }
                        } else {
                            option.text = "Select Status";
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
    if (jQuery(".frm-fleet-det #MFD_ACT").length > 0) {
        loadfleetStatus();
    }
    $('#MFD_FRM_DT').focusout(function () {
        var str = $(this).val();
        if ($.datepicker.formatDate('MM dd, yy', new Date(jQuery("#MFD_FRM_DT").val())) == 'undefined NaN, NaN' && jQuery("#MFD_FRM_DT").val() != '') {
            setInfoMsg('Please enter a valid date !!!');
            $(this).val('');
        }
    });
    $('#MFD_TO_DT').focusout(function () {
        var str = $(this).val();
        if ($.datepicker.formatDate('MM dd, yy', new Date(jQuery("#MFD_TO_DT").val())) == 'undefined NaN, NaN' && jQuery("#MFD_TO_DT").val() != '') {
            setInfoMsg('Please enter a valid date !!!');
            $(this).val('');
        }
    });
});

