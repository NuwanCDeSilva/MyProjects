jQuery(document).ready(function () {
    loadUserDetails();

    jQuery(".btn-save-data").click(function (evt) {
        evt.preventDefault();
        if (jQuery("#Se_usr_id").val() != "") {
            var formdata = jQuery("#up-prof-frm").serialize();
            jQuery.ajax({
                type: "GET",
                url: "/UpdateProfile/updatePassword",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                data: formdata,
                success: function (result) {
                    if (result.login == true) {
                        if (result.success = true) {
                            jQuery("#Se_usr_pw").val("");
                            jQuery("#newPassword").val("");
                            jQuery("#confirmPassword").val("");
                            setInfoMsg(result.msg);
                        } else {
                            setError(result.msg);
                        }
                    } else {
                        Logout();
                    }
                }
            });
        }
    });
});

function loadUserDetails() {
    jQuery.ajax({
        type: "GET",
        url: "/UpdateProfile/getUserDetails",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.login == true) {
                if (result.success = true) {
                    jQuery("#Se_usr_id").val(result.usr.Se_usr_id);
                    jQuery("#Se_usr_name").val(result.usr.Se_usr_name);
                } else {
                    setError(result.msg);
                }
            } else {
                Logout();
            }
        }
    });
}