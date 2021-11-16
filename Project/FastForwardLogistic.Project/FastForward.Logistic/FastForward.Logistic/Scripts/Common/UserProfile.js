jQuery(document).ready(function () {
    //global variables
    var ispasschange = 'false';

    //function
    function updateOptionIdDetails(p_choice) {
        var p_Se_usr_id = jQuery('#user_id').val();
        var p_Se_usr_desc = jQuery('#description').val();
        var p_Se_usr_name = jQuery('#full_name').val();
        var p_Se_usr_pw = jQuery('#password').val();
        var p_Se_usr_cat = jQuery('#designation_id').val();//*
        var p_Se_dept_id = jQuery('#department_id').val();//*
        var p_Se_emp_id = jQuery('#employee_id').val();
        var p_Se_emp_cd = jQuery('#emp_code').val();
        var p_Se_SUN_ID = jQuery('#sun_user_id').val();
        var p_se_Email = jQuery('#email').val();
        var p_se_Mob = jQuery('#mobile_no').val();
        var p_se_Phone = jQuery('#phone_no').val();

        var formdata = jQuery("#userdetails-frm").serialize();
        console.log(formdata);
        jQuery.ajax({
            type: "POST",
            url: "/UserProfile/Save_User_Details",
            data: formdata + "&choice=" + p_choice + "&ispassword=" + ispasschange,
            dataType: "json",
            success: function (result) {
                clear();
            }
            , error: function (data) { console.log(data) }
        });
    }

    function isEmail(email) {
        var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        return regex.test(email);
    }

    function isphonenumber(inputtxt)
    {
        var phoneno = /^\d{10}$/;
        if (phoneno.test(inputtxt))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    function Delete_User() {
        var tablelen = jQuery('#company-details-table tr').length;
        var ComUserlist = [];
        var checkedboxes = $('.checkoptions:checkbox:checked');
        if (checkedboxes.length > 0) {
            for (var x = 0; x < checkedboxes.length; x++) {

                ComUserlist.push(checkedboxes[x].defaultValue);
            }
            jQuery.ajax({
                type: "GET",
                traditional: true,
                url: "/UserProfile/Delete_User_Company",
                data: { ComUserlist: ComUserlist },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {

                }
                , error: function (data) { console.log(data) }
            });
        }
        else {
            status = "Company cannot be empty";
            setInfoMsg(status);
            return;
        }
    }

    function Update_User_Company() {

        var userid = jQuery('#user_id_asi').val();
        var companyid = jQuery('#company_id_asi').val();
        var isdeafult = jQuery("#IsDefault").is(":checked");
        var isactive = jQuery("#IsActiveC").is(":checked");

        jQuery.ajax({
            type: "GET",
            url: "/UserProfile/Update_User_Company",
            data: { p_company: companyid, p_userid: userid, p_isactive: isactive, p_isdefault: isdeafult },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {

            }
            , error: function (data) { console.log(data) }
        });
    }

    function clear() {
        var p_Se_usr_id = jQuery('#user_id').val('');
        var p_Se_usr_desc = jQuery('#description').val('');
        var p_Se_usr_name = jQuery('#full_name').val('');
        var p_Se_usr_pw = jQuery('#password').val('');
        var p_Se_usr_cat = jQuery('#designation_id').val('');//*
        var p_Se_dept_id = jQuery('#department_id').val('');//*
        var p_Se_emp_id = jQuery('#employee_id').val('');
        var p_Se_emp_cd = jQuery('#emp_code').val('');
        var p_Se_SUN_ID = jQuery('#sun_user_id').val('');
        var p_se_Email = jQuery('#email').val('');
        var p_se_Mob = jQuery('#mobile_no').val('');
        var p_se_Phone = jQuery('#phone_no').val('');
        var p_Se_confirm_usr_pw = jQuery('#confirm_password').val('');
        var p_se_Email = jQuery('#designation_id').val('');
        var p_se_Mob = jQuery('#department_id').val('');
        var p_se_Phone = jQuery('#user_id').val('');
        var p_domain = jQuery('#domain_id').val('');
        var p_domain = jQuery('#sun_user_id').val('');
        jQuery('#domain_name').val('');
        jQuery('#domain_title').val('');
        jQuery('#domain_department').val('');
            
        $('#IsDomain').prop('checked', false);
        $('#IsWindowsAuth').prop('checked', false);
        $('#IsAllowToChangePassword').prop('checked', false);
        $('#IsUserMustChangeNxtLog').prop('checked', false);
        $("#Active").prop("checked", false);
        $("#Inactive").prop("checked", false);
        $("#Locked").prop("checked", false);
        $("#PermanentlyDisable").prop("checked", false);

    }

    function clearassigncom() {

        $('#full_name_asi').val("");
        $('#description_asi').val("");
        $('#department_asi').val("");
        $('#empid_asi').val("");
        $('#full_name_asi').val("");
        $('#designation_asi').val("");
        $('#user_id_asi').val("");

        $('#company_id_asi').val("");
        $('#description_company_asi').val("");
        $('#IsDefault').prop("checked", false);
        $('#IsActiveC').prop("checked", false);
        $("#company-details-table").find("tr:gt(0)").remove();


    }


    function validationvar() {
        var p_Se_usr_id = jQuery('#user_id').val();
        var p_Se_usr_desc = jQuery('#description').val();
        var p_Se_usr_name = jQuery('#full_name').val();
        var p_Se_usr_pw = jQuery('#password').val();
        var p_Se_usr_cat = jQuery('#designation_id').val();//*
        var p_Se_dept_id = jQuery('#department_id').val();//*
        var p_Se_emp_id = jQuery('#employee_id').val();
        var p_Se_emp_cd = jQuery('#emp_code').val();
        var p_Se_SUN_ID = jQuery('#sun_user_id').val();
        var p_se_Email = jQuery('#email').val();
        var p_se_Mob = jQuery('#mobile_no').val();
        var p_se_Phone = jQuery('#phone_no').val();
        var p_Se_confirm_usr_pw = jQuery('#confirm_password').val();
        var vali = 0;



        if (p_Se_usr_id == '') {
            vali = 1;
            status = "User ID cannot be empty";
            setInfoMsg(status);
            return;
        }
        if (p_Se_usr_name == '') {
            vali = 1;
            status = "User name cannot be empty";
            setInfoMsg(status);
            return;
        }
        if (p_Se_usr_pw == '') {
            vali = 1;
            status = "Password cannot be empty";
            setInfoMsg(status);
            return;
        }
        if (p_Se_usr_cat == '') {
            vali = 1;
            status = "Designation cannot be empty";
            setInfoMsg(status);
            return;
        }
        if (p_Se_dept_id == '') {
            vali = 1;
            status = "Department ID cannot be empty";
            setInfoMsg(status);
            return;
        }
        if (p_Se_emp_id == '') {
            vali = 1;
            status = "Employee ID cannot be empty";
            setInfoMsg(status);
            return;
        }
        if (p_Se_emp_cd == '') {
            vali = 1;
            status = "Employee code cannot be empty";
            setInfoMsg(status);
            return;
        }
        if (p_se_Email == '') {
            vali = 1;
            status = "Email ID cannot be empty";
            setInfoMsg(status);
            return;
        }

        if (isEmail(p_se_Email) == false) {
            vali = 1;
            status = "Please enter valid E-mail";
            setInfoMsg(status);
            return;

        }

        if (p_se_Mob == '') {
            vali = 1;
            status = "Mobile number ID cannot be empty";
            setInfoMsg(status);
            return;
        }

        if (p_se_Phone == '') {
            vali = 1;
            status = "Phone number cannot be empty";
            setInfoMsg(status);
            return;
        }

        if (isphonenumber(p_se_Mob) == false) {
            vali = 1;
            status = "Please enter valid mobile phone number";
            setInfoMsg(status);
            return;

        }

        if (isphonenumber(p_se_Phone) == false) {
            vali = 1;
            status = "Please enter valid phone number";
            setInfoMsg(status);
            return;

        }

        if (p_Se_confirm_usr_pw != p_Se_usr_pw) {
            vali = 1;
            status = "Passwords does not match";
            setInfoMsg(status);
            return;
        }

        return vali;

    }


    //Change Functions
    jQuery(".user_id_seach").click(function () {
        var headerKeys = Array()
        headerKeys = ["ROW", "User ID", "Name"];
        field = "USER_LIST";
        data = {};
        var x = new CommonSearch(headerKeys, field, data);
    });

    jQuery('#addtableassingcom').click(function () {

                var isdefault = '';
        var isactive = '';
        var userid = jQuery('#user_id_asi').val();
        if (userid != "") {
            jQuery.ajax({
                type: "GET",
                url: "/UserProfile/getUserListCom",
                data: { userID: userid },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    jQuery("#company-details-table").find("tr:gt(0)").remove();
                    for (var i = 0; i < result.data.length; i++) {
                        console.log(result);
                        if (result.data[i].isActive=='1') {
                            isactive = 'checked'
                        }
                        if (result.data[i].isDefault=='1') {
                            isdefault = 'checked'
                        }

                        jQuery('#company-details-table').append('<tr><td>' + '<input type="checkbox" class="checkoptions" value="' + result.data[i].CompanyId + '%' + result.data[i].UserId + '"' + '>' + '</td>' +
                            '<td>' + result.data[i].CompanyId + '</td>' +
                            '<td>' + result.data[i].CompanyDescription + '</td>' +
                            '<td>' + '<input type="checkbox" disabled="disable"' + isdefault + '>' + '</td>' +
                            '<td>' + '<input type="checkbox" disabled="disable"' + isactive + '>' + '</td>' + '</tr>');
                    }
                }
         , error: function (data) { console.log(data) }
            });
        }
    })


    jQuery(".user_id_asi_search").click(function () {
        var headerKeys = Array()
        headerKeys = ["ROW", "User ID", "Name"];
        field = "USER_LIST_ASI";
        data = {};
        var x = new CommonSearch(headerKeys, field, data);
    });

    jQuery(".company_id_asi_search").click(function () {
        var headerKeys = Array()
        headerKeys = ["ROW", "CODE", "Description"];
        field = "COMPANY_LIST_ASI";
        data = {};
        var x = new CommonSearch(headerKeys, field, data);
    });


    jQuery(".department_id_seach").click(function () {
        var headerKeys = Array()
        headerKeys = ["ROW", "ID", "Description", "Head Of Department"];
        field = "DEPT_LIST";
        data = {};
        var x = new CommonSearch(headerKeys, field, data);
    });

    jQuery(".designation_id_seach").click(function () {
        var headerKeys = Array()
        headerKeys = ["ROW", "ID", "Description"];
        field = "DESIG_LIST";
        data = {};
        var x = new CommonSearch(headerKeys, field, data);
    });

    jQuery("#updateico").click(function () {
        var vali = validationvar();
        if (vali == 0) {
            Lobibox.confirm({
                msg: "Are you sure ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        updateOptionIdDetails('update');
                    }
                }
            });
        }
    });

    jQuery('#deleteicoasi').click(function () {
        var val = 0;
        var userid = jQuery('#user_id_asi').val();
        var checkedboxes = $('.checkoptions:checkbox:checked');
        var count = checkedboxes.length;
        
        if (userid == '') {
            status = "Please select a User";
            setInfoMsg(status);
            val = 1;
            return;
        }
        if (count == 0) {
            status = "Please select a Company to delete";
            setInfoMsg(status);
            val = 1;
            return;
        }

        if (val == 0) {
            Lobibox.confirm({
                msg: "Are you sure ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        Delete_User();
                        clearassigncom();
                        status = "Succesfully deleted";
                        setInfoMsg(status);
                        return;
                    }
                }
            });
        }
    });

    jQuery("#createico").click(function () {
        var vali = validationvar();
        if (vali == 0) {
            Lobibox.confirm({
                msg: "Are you sure ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        updateOptionIdDetails('create');
                        clear();

                    }
                }
            });
        }
    });

    jQuery("#password").change(function () {
        ispasschange = 'true';
    });
    
    jQuery('#refreshicoasi').click(function () {
        clearassigncom();
    });




    jQuery('#refreshico').click(function () {
        clear();
    });

    jQuery('#createicoasi').click(function () {
        var vali = 0;
        if (vali == 0) {
            Lobibox.confirm({
                msg: "Are you sure ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        Update_User_Company();
                        clearassigncom();
                        status = "Succesfully updated";
                        setInfoMsg(status);
                        return;
                    }
                }
            });
        }

    });


    jQuery('#user_id_asi').focusout(function () {
        var isdefault = '';
        var isactive = '';
        var userid = jQuery('#user_id_asi').val();
        if (userid != "") {
            jQuery.ajax({
                type: "GET",
                url: "/UserProfile/getUserListCom",
                data: { userID: userid },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    jQuery("#company-details-table").find("tr:gt(0)").remove();
                    for (var i = 0; i < result.data.length; i++) {
                        console.log(result);
                        if (result.data[i].isActive=='1') {
                            isactive = 'checked'
                        }
                        if (result.data[i].isDefault=='1') {
                            isdefault = 'checked'
                        }

                        jQuery('#company-details-table').append('<tr><td>' + '<input type="checkbox" class="checkoptions" value="' + result.data[i].CompanyId + '%' + result.data[i].UserId + '"' + '>' + '</td>' +
                            '<td>' + result.data[i].CompanyId + '</td>' +
                            '<td>' + result.data[i].CompanyDescription + '</td>' +
                            '<td>' + '<input type="checkbox"' + isdefault + ' onclick="return false;">' + '</td>' +
                            '<td>' + '<input type="checkbox"' + isactive + 'onclick="return false;">' + '</td>' + '</tr>');
                    }
                }
         , error: function (data) { console.log(data) }
            });
        }


    });




});