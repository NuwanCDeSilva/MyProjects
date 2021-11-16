jQuery(document).ready(function () {



    jQuery(document).ajaxStart(function () {
        jQuery(".imagelog.page-loadin").show();
    }).ajaxStop(function () {
        jQuery(".imagelog.page-loadin").hide();
    });
    //load companies when user id box is filled
    if (jQuery(".user_name_cls").length > 0 && jQuery(".user_name_cls").val() != null && jQuery(".user_name_cls").val() != "") {
        getCompany();
    }
    //get comany when focus out the user name event
    jQuery(".user_name_cls").focusout(function () {

        getCompany();
    })
    jQuery("#Mst_com_Mc_cd").on("focus", function () {
        if (jQuery("#User_name").val() != "") {
            getCompany();
        }
    });
    //get company list for selected user
    function getCompany() {
        jQuery(".container .messages").empty();
        deactiveLoginButton();
        var username = jQuery(".user_name_cls").val();
        jQuery.ajax({
            type: "GET",
            url: "/Login/GetCompanyList",
            data: { username: username },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.success == true) {
                    var select = document.getElementById("Mst_com_Mc_cd");
                    jQuery("#Mst_com_Mc_cd").empty();
                    var options = [];
                    var option = document.createElement('option');
                    if (result.data != null && result.data.length != 0) {
                        for (var i = 0; i < result.data.length; i++) {
                            option.text = result.data[i].MasterComp.Mc_desc;
                            option.value = result.data[i].SEC_COM_CD;
                            options.push(option.outerHTML);
                        }
                        jQuery(".company-description").val(result.data[0].MasterComp.Mc_desc);
                    } else {
                        option.text = "Select Company";
                        option.value = "";
                        options.push(option.outerHTML);
                        setError(result.msg);
                    }
                    select.insertAdjacentHTML('beforeEnd', options.join('\n'));
                } else {
                    setError(result.msg);
                }
                activeLoginButton();
            },
            error: function (response) {
                if (response.success = false) {
                    jQuery(".container .messages").empty();
                    var select = document.getElementById("Mst_com_Mc_cd");
                    jQuery("#Mst_com_Mc_cd").empty();
                    var options = [];
                    var option = document.createElement('option');
                    option.text = "Select Company";
                    option.value = "";
                    options.push(option.outerHTML);
                    select.insertAdjacentHTML('beforeEnd', options.join('\n'));
                    setError(result.msg);
                    activeLoginButton();
                }

            }

        });

    }
    //deactivate login form button
    function deactiveLoginButton() {
        if (jQuery('.btn-primary.login-btn') != null) {
            jQuery('.btn-primary.login-btn').attr('disabled', true);
        }
    }
    //activate login form button
    function activeLoginButton() {
        if (jQuery('.btn-primary.login-btn') != null) {
            jQuery('.btn-primary.login-btn').attr('disabled', false);
        }
    }
    function setError(msg) {
        jQuery(".animated-super-fast").remove();
        if (jQuery(".animated-super-fast").length == 0) {
            Lobibox.alert('error',
            {
                msg: msg
            });
        }
    }
    function setSuccesssMsg(msg) {
        jQuery(".animated-super-fast").remove();
        if (jQuery(".animated-super-fast").length == 0) {
            Lobibox.alert('success',
        {
            msg: msg
        });
        }
    }

    function setInfoMsg(msg) {
        jQuery(".animated-super-fast").remove();
        if (jQuery(".animated-super-fast").length == 0) {
            Lobibox.alert('info',
             {
                 msg: msg
             });
        }
    }
    //;ogin form login button click event
    jQuery(".login-btn").click(function (evt) {
        var form = jQuery('.login-form');
        jQuery(".login-btn").prop('disabled', true);
        jQuery(".container .messages").empty();
        deactiveLoginButton();
        evt.preventDefault();
        jQuery.ajax({
            cache: false,
            async: true,
            type: "POST",
            url: '/Login/Login',//+form.attr('action') + '&request=req'
            data: form.serialize() + '&request=req',
            success: function (data) {
                if (data != null) {
                    if (data.success == true) {
                        window.location.replace("/Home/Index");
                        activeLoginButton();
                    } else {
                        jQuery(".login-btn").removeAttr("disabled");
                        setError(data.msg);
                        activeLoginButton();

                    }
                }
            }
        });
    });
});