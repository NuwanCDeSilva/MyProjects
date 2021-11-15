jQuery(document).ready(function () {
    jQuery(".addtemplate").click(function (evt) {
        evt.preventDefault();
        loadExistsTempletes(1,10,"","",false);
        jQuery('#TemplateManager').modal({
            keyboard: false,
            backdrop: 'static'
        }, 'show');
    });
    jQuery(".btn-save-data").click(function (evt) {
        evt.preventDefault();
        var formdata = jQuery("#account-deffrm").serialize();
        jQuery.ajax({
            type: 'POST',
            url: '/AccountDefinition/save',
            data: formdata,
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        jQuery(".template-item").empty();
                        setSuccesssMsg("Successfully save details.");
                    } else {
                        setInfoMsg(result.msg);
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    jQuery("#account-code").focusout(function (evt) {
        evt.preventDefault();
        if (jQuery(this).val() != "") {
            var code = jQuery(this).val();
            jQuery.ajax({
                type: 'GET',
                async:false,
                url: '/TemplateManager/getItemSavedValues',
                data: { code: code },
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            drawFormTemplateField(result.det);
                        } else {
                            setInfoMsg(result.msg);
                        }
                    } else {
                        Logout();
                    }
                }
            });
        }
    });
    jQuery(".rch-creacc-no").click(function () {
        var headerKeys = Array();
        headerKeys = ["Row", "Code", "Description"];
        field = "SRCHCREACC1";
        var x = new CommonSearch(headerKeys, field);
    });
});
