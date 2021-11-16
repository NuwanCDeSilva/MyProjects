jQuery(document).ready(function () {

    getCompany();
    var Optioidlist = [];
    var Shipmentidlist = [];
    var Menuidlist = [];
    $('.child1').addClass("hide");
    $('.child2').addClass("hide");
    $('.child3').addClass("hide");
    jQuery('#createicoshipment').addClass('hide');

    //$('#example_tree input[type="checkbox"]').click(function () {
        
    //    var x = $(this).is(':checked');

    //    if (x) {
    //        $(this).parentsUntil('.Mparent2').find('.childt2').prop('checked', true);
    //    }
    //});


    $('.Mparent').click(function () {
        $(this).children(".child1").removeClass("hide");

    });

    $('.Mparent2').click(function () {
        $(this).children(".child2").removeClass("hide");

    });

    $('.Mparent3').click(function () {
        $(this).children(".child3").removeClass("hide");
    });

    function getCompany() {
        jQuery.ajax({
            type: "GET",
            url: "/RoleCreation/GetSessionValues",
            contentType: "application/json;charset=utf-8",
            dataType: "text",
            success: function (result) {
                jQuery.ajax({
                    type: "GET",
                    url: "/Login/GetCompanyList",
                    data: { username: result },
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (datas) {
                        jQuery("#CompanyList").empty();
                        jQuery("#CompanyList_view_role").empty();
                        jQuery("#CompanyList_grant_role").empty();
                        jQuery("#CompanyList_grant_privilage").empty();
                        jQuery("#CompanyListShipment").empty();
                        var options = [];
                        var option = document.createElement('option');
                        if (datas.data != null && datas.data.length != 0) {
                            for (var i = 0; i < datas.data.length; i++) {
                                option.text = datas.data[i].MasterComp.Mc_desc;
                                option.value = datas.data[i].SEC_COM_CD;
                                options.push(option.outerHTML);
                            }
                        }
                        
                        for (var j = 0; j < options.length; j++) {
                            jQuery('#CompanyList').append(options[j]);
                            jQuery('#CompanyList_view_role').append(options[j]);
                            jQuery('#CompanyList_grant_role').append(options[j]);
                            jQuery('#CompanyList_grant_privilage').append(options[j]);
                            jQuery('#CompanyListShipment').append(options[j]);

                        }
                        
                        var Comname = jQuery("#CompanyList").val();
                        jQuery('#hiddentextid').val(Comname);
                        jQuery("#hiddencompanyid").val(Comname);
                        jQuery("#hiddencompanyidgr").val(Comname);
                        jQuery("#hiddencompanyidpriv").val(Comname);
                    }
                });
            }
            , error: function (data) { console.log(data) }

        });
    }

    function getMenuDetails() {
        $("#menu-role-details-table").find("tr:gt(0)").remove();
        jQuery.ajax({
            type: "GET",
            url: "/RoleCreation/GetSessionValues",
            contentType: "application/json;charset=utf-8",
            dataType: "text",
            success: function (result) {
                var roleid = jQuery("#role_id_view_role").val();
                var comapanyid = jQuery("#hiddentextid").val();
                jQuery.ajax({
                    type: "GET",
                    url: "/RoleCreation/GetMenuDetailsByRID",
                    data: { userid: result, companyId: comapanyid, roleid: roleid },
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (datas) {
                        
                        for (var i = 0; i < datas.data.length; i++) {
                            jQuery('#menu-role-details-table').append('<tr>' + '<tr><td>' + (i + 1) + '</td>' +
                                '<td>' + datas.data[i].ComapanyName + '</td>' +
                                '<td>' + datas.data[i].RoleID + '</td>' +
                                '<td>' + datas.data[i].RoleName + '</td>' +
                                '<td>' + datas.data[i].MenuName + '</td>' + '</tr>');
                        }
                    }
                });
            }
            , error: function (data) { console.log(data) }

        });
    }



    function getUserRoleDetails() {
        var roleid = jQuery("#role_id").val();
        var comapanyid = jQuery("#hiddentextid").val();
        jQuery.ajax({
            type: "GET",
            url: "/RoleCreation/GetRoleIdDetails",
            data: { companyId: comapanyid, roleid: roleid },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                jQuery("#userrolelist-table").find("tr:gt(0)").remove();

                for (var i = 0; i < result.data.length; i++) {
                    jQuery('#userrolelist-table').append('<tr><td>' + result.data[i].ComapanyName + '</td>' + '<td>' + result.data[i].RoleID + '</td>' +
                        '<td>' + result.data[i].RoleDescription + '</td>' +
                        '<td>' + result.data[i].CreatedBy + '</td>' +
                        '<td>' + result.data[i].ModifiedBy + '</td>' +
                        '<td>' + result.data[i].ModifiedDate + '</td>' + '</tr>');
                }
            }
            , error: function (data) { console.log(data) }
        });
    }

    function getMenusBComandRid() {
        var roleid = jQuery("#role_id_grant_privilage").val();
        var comapanyid = jQuery("#hiddencompanyidpriv").val();
        jQuery.ajax({
            type: "GET",
            url: "/RoleCreation/GetMenusByCandRid",
            data: { companyId: comapanyid, roleid: roleid },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                console.log(result);
                for (var x = 0; x < result.data.length; x++) {
                    var id = result.data[x].SSM_ID;
                    var test = $("#" + id).prop("checked", true);
                }
            }
            , error: function (data) { console.log(data) }
        });
    }

    function updateOptionIdDetails() {
        var roleid = jQuery("#role_id_grant_role").val();
        var comapanyid = jQuery("#hiddencompanyidgr").val();
        jQuery.ajax({
            type: "GET",
            traditional: true,
            url: "/RoleCreation/Update_Option_IDs",
            data: { Optioidlist: Optioidlist, company: comapanyid, roleid: roleid },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                $("#option-list-table").find("tr:gt(0)").remove();
                setInfoMsg("Option ID's has been updated");
                return;
            }
            , error: function (data) { console.log(data) }
        });
    }

    function updateMenuIdDetails() {
        Menuidlist = [];
        var roleid = jQuery("#role_id_grant_privilage").val();
        var comapanyid = jQuery("#hiddencompanyidpriv").val();

        var id = '0';
        var actstat = 'false';
        
        var checkboxes = $('.MenuOpId');

        for (var x = 0; x < checkboxes.length; x++) {
            id = checkboxes[x].id +'%'+ checkboxes[x].checked;
            Menuidlist.push(id);
        }
        jQuery.ajax({
            type: "GET",
            traditional: true,
            url: "/RoleCreation/Update_User_Menu",
            data: { MenuIdlist: Menuidlist, company: comapanyid, roleid: roleid },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                status = "Succesfully updated";
                setInfoMsg(status);
                jQuery('#role_id_grant_privilage').val('');
                jQuery('#role_name_grant_privilage').val('');
                jQuery('#role_description_grant_privilage').val('');
                jQuery('#IsActiveGrantprivilage').prop("checked", false);
                return;

            }, complete: function (data) {
                //var url = $("#RedirectToPage").val();
                //location.href = url;
            }
            , error: function (data) { console.log(data) }
        });
    }


    function getUserDetailsByRole() {
        var roleid = jQuery("#role_id_view_role").val();
        var comapanyid = jQuery("#hiddencompanyid").val();
        jQuery.ajax({
            type: "GET",
            url: "/RoleCreation/GetUserDetailsByRID",
            data: { companyId: comapanyid, roleid: roleid },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                jQuery("#user-role-details-table").find("tr:gt(0)").remove();
                for (var i = 0; i < result.data.length; i++) {
                    jQuery('#user-role-details-table').append('<tr><td>' + (i + 1) + '</td>' +
                        '<td>' + result.data[i].ComapanyName + '</td>' +
                        '<td>' + result.data[i].UserID + '</td>' +
                        '<td>' + result.data[i].UserName + '</td>' +
                        '<td>' + result.data[i].UserDescription + '</td>' +
                        '<td>' + result.data[i].Mobile + '</td>' +
                        '<td>' + result.data[i].Phone + '</td>' +
                        '<td>' + result.data[i].Domain + '</td>' + '</tr>');
                }
            }
            , error: function (data) { console.log(data) }
        });
    }

    function explode() {
        
    }


    function SaveUserRoles() {
        var roleid = jQuery("#role_id").val();
        var rolename = jQuery('#role_name').val();
        var comapanyid = jQuery("#hiddentextid").val();
        var active = jQuery('#IsActive').is(':checked');
        var choice = jQuery('#IsNew').is(':checked');
        var roledescription = jQuery("#role_description").val();
        jQuery.ajax({
            type: "GET",
            url: "/RoleCreation/Save_User_Roles",
            data: { company: comapanyid, roleid: roleid, rolename: rolename, description: roledescription, active: active, choice: choice },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (choice == true) {
                    $("#userrolelist-table").find("tr:gt(0)").remove();
                    setInfoMsg("User role has been created");
                    return;

                }
                else {
                    $("#userrolelist-table").find("tr:gt(0)").remove();
                    setInfoMsg("Details has been updated");
                    return;
                }
            }
            , error: function (data) {
                if (data.type == "Error") {
                    Lobibox.alert('error', { msg: data.msg });

                }

            }
        });
    }

    function clear_grant() {
        jQuery("#role_id_grant_role").val('');
        jQuery("#role_name_grant_role").val('');
        jQuery("#role_description_grant_role").val('');
        jQuery("#IsActiveGrantRole").prop("checked", false);
    }

    function clear() {
        jQuery("#role_name").val('');
        jQuery("#role_id").val('');
        jQuery("#role_description").val('');
        jQuery("#IsNew").prop("checked", false);
        jQuery("#IsActive").prop("checked", false);
    }


    jQuery(".role_id_search").click(function () {

        jQuery("#IsNew").prop("checked", false);
        var headerKeys = Array()
        headerKeys = ["ROW", "ID", "Description"];
        field = "TY_OF_ROLE_ID";
        data = { company: jQuery("#CompanyList").val() }
        var x = new CommonSearch(headerKeys, field, data);
    });

    jQuery(".role_id_search_view_role").click(function () {
        var headerKeys = Array()
        headerKeys = ["ROW", "ID", "Description"];
        field = "TY_OF_ROLE_ID_VR";
        data = { company: jQuery("#CompanyList_view_role").val() }
        var x = new CommonSearch(headerKeys, field, data);
    });

    jQuery(".role_id_search_grant_role").click(function () {
        var headerKeys = Array()
        headerKeys = ["ROW", "ID", "Description"];
        field = "OPTION_ID_DET";
        data = { company: jQuery("#CompanyList_grant_role").val() }
        var x = new CommonSearch(headerKeys, field, data);
    });

    jQuery(".role_id_search_grant_privilage").click(function () {
        var headerKeys = Array()
        headerKeys = ["ROW", "ID", "Description"];
        field = "TY_OF_ROLE_ID_GRANT";
        data = { company: jQuery("#CompanyList_grant_privilage").val() }
        var x = new CommonSearch(headerKeys, field, data);
    });


    jQuery("#addtable").click(function () {
        getUserRoleDetails();
    });

    jQuery("#addtablegrantprivi").click(function () {
        var vali = 0;
        var roleid = jQuery("#role_id_grant_privilage").val();
        
        if (roleid == '') {
            status = "Please select a Role Id";
            vali = 1;
            setInfoMsg(status);
            return;

        }
        if (vali == 0) {
            $('#example_tree .MenuOpId').prop('checked', false);
            getMenusBComandRid();
        }


    });

    jQuery("#add_us_table").click(function () {
        var vali = 0;
        var com = jQuery("#CompanyList_view_role").val();

       var roleid = jQuery("#role_id_view_role").val();
       if (com == "") {
           status = "Please select a company";
           vali = 1;
           setInfoMsg(status);
           return;

       }
       if (roleid == "") {
           status = "Please select a Role id";
           vali = 1;
           setInfoMsg(status);
           return;
       }
       if (vali == 0) {
           getMenuDetails();
           getUserDetailsByRole();
       }
    });

    jQuery("#CompanyList").change(function () {
        var selected = jQuery(this).val();
        jQuery('#hiddentextid').val(selected);
        jQuery("#role_name").val('');
        jQuery("#role_id").val('');
        jQuery("#role_description").val('');
        jQuery('#CompanyList_view_role').val(selected);
        jQuery("#IsActive").prop("checked", false);
    });

    jQuery("#CompanyList_view_role").change(function () {
        var selected = jQuery(this).val();
        jQuery('#hiddentextid').val(selected);
        jQuery('#CompanyList').val(selected);
    });

    jQuery("#CompanyList_grant_privilage").change(function () {
        var selected = jQuery(this).val();
        jQuery('#hiddencompanyidgr').val(selected);
    });

    jQuery("#CompanyList_grant_role").change(function () {
        var selected = jQuery(this).val();
        jQuery('#hiddencompanyidgr').val(selected);
    });

    jQuery("#CompanyList_grant_privilage").change(function () {
        var selected = jQuery(this).val();
        jQuery('#hiddencompanyidpriv').val(selected);
    });

    jQuery("#redd").click(function () {
        var url = $("#RedirectToPage").val();
        location.href = url;
    });

    jQuery("#updateico").click(function () {

        var rolename = jQuery("#role_name").val();
        var roleid = jQuery("#role_id").val();
        var roledescription = jQuery("#role_description").val();
        var statustext = "";
        var validation = 0;
        var val = jQuery("#IsNew").is(':checked');

        if (val == true) {
            if (roledescription == "") {
                validation = 1;
                status = "Role description cannot be empty";
                setInfoMsg(status);
                return;
            }
            if (rolename == "") {
                validation = 1;
                status = "Role name cannot be empty";
                setInfoMsg(status);
                return;
            }

            if (validation == 0) {
                Lobibox.confirm({
                    msg: "Are you sure ?",
                    callback: function ($this, type, ev) {
                        if (type == "yes") {
                            SaveUserRoles();
                            clear();
                        }
                    }
                });
            }
        }
        else {

            if (roledescription == "") {
                validation = 1;
                status = "Role description cannot be empty";
                setInfoMsg(status);
                return;
            }
            if (rolename == "") {
                validation = 1;
                status = "Role name cannot be empty";
                setInfoMsg(status);
                return;
            }
            if (roleid == "") {
                validation = 1;
                status = "Role ID cannot be empty";
                setInfoMsg(status);
                return;

            }
            if (validation == 0) {

                Lobibox.confirm({
                    msg: "Are you sure?",
                    callback: function ($this, type, ev) {
                        if (type == "yes") {
                            SaveUserRoles();
                            clear();
                        }
                    }
                });
            }
        }
    });

    jQuery('#IsNew').click(function () {
        var val = jQuery("#IsNew").is(':checked');
        if (val == true) {
            var role_id = jQuery("#CompanyList").val();
            jQuery("#role_name").val('');
            jQuery("#role_description").val('');
            jQuery("#role_id").val('');
            jQuery("#IsActive").prop("checked", false);
            jQuery("#glyphiconicon").removeClass("glyphicon-edit");
            jQuery("#glyphiconicon").addClass("glyphicon-save");
            jQuery('#choiceupdate').text('Create');
            jQuery('#IsActive').removeAttr("disabled");

        }
        else {
            jQuery('#choiceupdate').text('Update');
            jQuery("#glyphiconicon").removeClass("glyphicon-save");
            jQuery("#glyphiconicon").addClass("glyphicon-edit");
            jQuery('#IsActive').prop("disabled",true);
        }
    });

    $('li.dropdown').click(function () {
        $('li.dropdown').not(this).find('ul').hide();
        $(this).find('ul').toggle();
    });

    $('#IsNewShipment').click(function () {
        var check = $('#IsNewShipment').is(":checked");
        if (check == true) {
            jQuery("#glyphiconiconshipment").removeClass("glyphicon-edit");
            jQuery("#glyphiconiconshipment").addClass("glyphicon-save");
            jQuery('#choiceupdateshipment').text('Create');
            $("#mode_name").prop('readonly', false);
            $("#mode_description").prop('readonly', false);
        }
        else {
            jQuery("#glyphiconiconshipment").removeClass("glyphicon-save");
            jQuery("#glyphiconiconshipment").addClass("glyphicon-edit");
            jQuery('#choiceupdateshipment').text('Update');
            $("#mode_name").prop('readonly', true);
            $("#mode_description").prop('readonly', true);
        }
    });

    jQuery("body").on('click', '.checkoptions', function () {
        var vali = -1;
        var id = jQuery(this).val();
        var choiceop = jQuery(this).is(':checked');
        if (Optioidlist.length == 0) {
            Optioidlist.push(id +'%'+ choiceop);
        } else {
            for (var x = 0; x < Optioidlist.length; x++) {
                if (Optioidlist[x].includes(id)) {
                    Optioidlist.splice(x, 1);
                    vali = 0;
                    break;
                }
            }
            if (vali == -1) {
                Optioidlist.push(id + '%' + choiceop);
            }
        }
       console.log(Optioidlist);
    });

    jQuery("body").on('click', '.isActiveShipments', function () {
        var vali = -1;
        var id = jQuery(this).val();
        var choiceop = jQuery(this).is(':checked');
        if (Shipmentidlist.length == 0) {
            Shipmentidlist.push(id + '%' + choiceop);
        } else {
            for (var x = 0; x < Shipmentidlist.length; x++) {
                if (Shipmentidlist[x].includes(id)) {
                    Shipmentidlist.splice(x, 1);
                    vali = 0;
                    break;
                }
            }
            if (vali == -1) {
                Shipmentidlist.push(id + '%' + choiceop);
            }
        }
        console.log(Shipmentidlist);
    });

    //jQuery("body").on('click', '.MenuOpId', function () {
    //    var vali = -1;
    //    var id = jQuery(this).val();
    //    var choiceop = jQuery(this).is(':checked');
    //    console.log(id);

    //});


    jQuery('#addtablegrandroletable').click(function () {

        var vali = 0;
        var roleid = jQuery("#role_id_grant_role").val();

        if (roleid == '') {
            status = "Please select a Role Id";
            setInfoMsg(status);
            return;
            vali = 1;
        }

        if (vali == 0) {
            var comapanyid = jQuery("#hiddencompanyidgr").val();
            var checkstat = '';
            jQuery.ajax({
                type: "GET",
                url: "/RoleCreation/GetOptionID",
                data: { companyId: comapanyid, roleid: roleid },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    jQuery("#option-list-table").find("tr:gt(0)").remove();
                    for (var i = 0; i < result.data.length; i++) {
                        if (result.data[i].ActiveStatus == '1') {
                            checkstat = 'checked';

                        }
                        else {
                            checkstat = ''
                        }
                        jQuery('#option-list-table').append('<tr><td>' + '<input type="checkbox" class="checkoptions" value="' + result.data[i].OptionId + '"' + checkstat + '>' + '</td>' +
                            '<td>' + result.data[i].OptionId + '</td>' +
                            '<td>' + result.data[i].OptionTitle + '</td>' +
                            '<td>' + result.data[i].OptionDescription + '</td>' +
                            '<td>' + result.data[i].OptionCreatedBy + '</td>' +
                            '<td>' + result.data[i].OptionCreatedDate + '</td>' + '</tr>');
                    }
                }
                , error: function (data) { console.log(data) }
            });
        }
    });


    jQuery("#saveoptionico").click(function () {
        var validation = 0;
        var company = jQuery('#CompanyList_grant_role').val();
        var roleid = jQuery('#role_id_grant_role').val();
        if (company == "") {
            validation = 1;
            status = "Please select a company";
            setInfoMsg(status);
            return;
        }
        if (roleid == "") {
            validation = 1;
            status = "Please selecet a role";
            setInfoMsg(status);
            return;
        }
        if (validation == 0) {

            Lobibox.confirm({
                msg: "Are you sure?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        updateOptionIdDetails();
                        getCompany();
                        clear_grant();
                    }
                }
            });
        }
    });


    $(function () {
        $('#example_tree').find('span').click(function (e) {
            $(this).parent().children('ul').toggle();
        });
    });

    //$(function () {
    //    $("input[type='checkbox']").change(function () {
    //        $(this).siblings('ul').find("input[type='checkbox']").prop('checked', this.checked);
    //    });
    //});

$('input[type=checkbox]').click(function(){
    if(this.checked){
        $(this).parents('li').children('input[type=checkbox]').prop('checked',true);
    }
    var sibs = false;
    $(this).parent().find('input[type=checkbox]').prop('checked',this.checked);	

    $(this).closest('ul').children('li').find('input[type=checkbox]').each(function (index) {
        //if ($('input[type=checkbox]', this).is(':checked')) sibs = true;
        //$xval = $(this).is(":checked");
        if($(this).is(":checked"))
            sibs = true;
    });

    $xval = $(this).parents('ul').prev().prev().prop("checked", sibs);

});



    //$(function () {
    //    $("input[type='checkbox']").change(function () {
    //        if ($(this).is(':checked')) {
    //            $(this).parents('ul').siblings('input:checkbox').attr('checked', true);
    //        }
    //    });
    //});

    $("#saveoptionicopriv").click(function () {

        var validation = 0;
        var company = jQuery('#CompanyList_grant_privilage').val();
        var roleid = jQuery('#role_id_grant_privilage').val();

        if (roleid == "") {
            validation = 1;
            status = "Please selecet a Roleid";
            setInfoMsg(status);
            return;
        }

        if (company == "") {
            validation = 1;
            status = "Please selecet a company";
            setInfoMsg(status);
            return;
        }


        if (validation == 0) {
            Lobibox.confirm({
                msg: "Are you sure ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        updateMenuIdDetails();
                        clear();
                        $('#example_tree').find(".MenuOpId").prop('checked', false);
                    }
                }
            });
        }
    });


    jQuery('#addtableshipment').click(function () {
        var vali = 0;
        var companylist= jQuery("#CompanyListShipment").val();

        if (companylist == '') {
            status = "Please select a Company";
            setInfoMsg(status);
            return;
            vali = 1;
        }
        if (vali == 0) {
            var checkstat = '';
            var optionid = '';
            jQuery.ajax({
                type: "GET",
                url: "/RoleCreation/GetShipmentList",
                data: { company: companylist},
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    console.log(result);
                    jQuery("#shipmentlist_table").find("tr:gt(0)").remove();
                    for (var i = 0; i < result.data.length; i++) {
                        if (result.data[i].msc_act == '1') {
                            checkstat = 'checked';
                        }
                        else {
                            checkstat = ''
                        }
                        optionid = result.data[i].msc_cd;
                        jQuery('#shipmentlist_table').append('<tr><td>' + '<input type="checkbox" class="isActiveShipments" value="' + optionid + '"' + checkstat + '>' + '</td>' +
                            '<td>' + result.data[i].msc_com + '</td>' +
                            '<td>' + result.data[i].msc_cd + '</td>' +
                            '<td>' + result.data[i].msc_desc + '</td>' + '</tr>');
                    }
                }
                , error: function (data) { console.log(data) }
            });
        }
    });



    jQuery('#updateicoshipment').click(function () {

        var choice = jQuery("#IsNewShipment").is(":checked");
        var p_choice = "";
        if (choice == true) {
            p_choice = "create";
        }
        else {
            p_choice = "update";
        }
        var company = jQuery("#CompanyListShipment").val();
        var mode_name = jQuery("#mode_name").val();
        var ship_desc = jQuery("#mode_description").val();

        if (p_choice == "create" || (Shipmentidlist.length != 0)) {
            jQuery.ajax({
                type: "GET",
                traditional: true,
                url: "/RoleCreation/Update_Shipment_List",
                data: { ShipmentDetlist: Shipmentidlist, company: company, mode_name: mode_name, ShipDesc: ship_desc, choice: p_choice },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (choice == true) {
                        setInfoMsg("New shipment mode has been created");
                        return;

                    }
                    else {
                        setInfoMsg("Shipment details has been updated");
                        return;
                    }
                }
                , error: function (data) {
                    if (data.type == "Error") {
                        Lobibox.alert('error', { msg: data.msg });

                    }

                }
            });
        } else {
            setInfoMsg("Please create a new shipment or update details");
            return;

        }

    });
});