jQuery(document).ready(function () {
    dataEntry();
    jQuery(".btn-save-data").click(function (event) {
        event.preventDefault();
        jQuery(this).attr("disabled", true);
        var formdata = jQuery("#customer-crte-frm").serialize();
        Lobibox.confirm({
            msg: "Do you want to continue ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    jQuery.ajax({
                        type: 'POST',
                        url: '/DataEntry/CustomerCreation',
                        data: formdata,
                        success: function (response) {
                            if (response.login == true) {
                                if (response.success == true) {
                                    document.getElementById("customer-crte-frm").reset();
                                    fieldEnable();
                                    setSuccesssMsg(response.msg);
                                    jQuery(".btn-save-data").val("Create");
                                    jQuery(".btn-save-data").removeAttr("disabled");
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
                }
            }
        });
    });
});