jQuery(document).ready(function () {
    jQuery('#st_date').datepicker({ dateFormat: "dd/M/yy" })
    jQuery('#end_date').datepicker({ dateFormat: "dd/M/yy" })
  
    $('#st_date').focusout(function () {
        var str = $(this).val();
        if ($.datepicker.formatDate('MM dd, yy', new Date(jQuery("#st_date").val())) == 'undefined NaN, NaN' && jQuery("#st_date").val() != '') {
            setInfoMsg('Please enter a valid date !!!');
            $(this).val('');
        }
    });
    $('#end_date').focusout(function () {
        var str = $(this).val();
        if ($.datepicker.formatDate('MM dd, yy', new Date(jQuery("#end_date").val())) == 'undefined NaN, NaN' && jQuery("#end_date").val() != '') {
            setInfoMsg('Please enter a valid date !!!');
            $(this).val('');
        }
    });
   
    jQuery('.btn-accounts-upload-clear-data').click(function (e) {
        Lobibox.confirm({
            msg: "Do you want to clear?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    document.getElementById("accounts-upload-frm").reset();
                }
            }
        })
    });
    jQuery('.btn-accounts-upload-data').click(function (e) {

        var start = new Date(jQuery("#st_date").val());
        var end = new Date(jQuery("#end_date").val());

        if (jQuery("#st_date").val() != "" || jQuery("#end_date").val() != "")
        {
            if (start > end)
            {
                setInfoMsg('Invalid Date range');
            }
            else
            {

                Lobibox.confirm({
                    msg: "Do you want to continue process?",
                    callback: function ($this, type, ev) {
                        if (type == "yes") {
                            var formdata = jQuery("#accounts-upload-frm").serialize();
                            jQuery.ajax({
                                type: "GET",
                                url: "/AccountsUploads/UploadAccounts",
                                data: formdata,
                                contentType: "application/json;charset=utf-8",
                                dataType: "json",
                                success: function (result) {
                                    if (result.login == true) {
                                        if (result.success == true)
                                        {
                                            setSuccesssMsg(result.msg);
                                            document.getElementById("accounts-upload-frm").reset();
                                        } else {
                                            if (result.type == "Error")
                                            {
                                                setError(result.msg);

                                            }
                                            if (result.type == "Info")
                                            {
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
    loadStatus();
    function loadStatus() {
        jQuery.ajax({
            type: "GET",
            url: "/AccountsUploads/LoadStatus",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        var select = document.getElementById("pay_type");
                        jQuery("#pay_type").empty();
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
})