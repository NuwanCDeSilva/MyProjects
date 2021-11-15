jQuery(document).ready(function () {
    var first = true;
    clearSession();
    jQuery(".with-deleted").click(function () {
        var check = jQuery(this).is(':checked');
        var pgeSize = jQuery('.templatecontent .cls-select-temp').val();
        loadExistsTempletes(1, pgeSize, "", "", check);
    });
    jQuery(".btn-close-tempform").click(function () {
        jQuery('#TemplateManager').modal('hide');
    });
    jQuery(".hedpop-done").click(function () {
        var id = jQuery(".head-iddone").html();
        jQuery.ajax({
            cache: false,
            async: false,
            type: "GET",
            url: "/TemplateManager/checkDefaultFieldAdd?tempId=" + id,
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        jQuery('#TemplateHdrPop').modal('hide');
                    } else {
                        setInfoMsg(result.msg);
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    jQuery(".detpop-done").click(function () {
        jQuery('#TemplateDetField').modal('hide');
    });
    jQuery(".add-more-field").click(function (evt) {
        evt.preventDefault();
        listEditItem();
    });
    jQuery(".btn-refresh-form").click(function (evt) {
        evt.preventDefault();
        var check = jQuery(".with-deleted").is(':checked');
        var pgeSize = jQuery('.templatecontent .cls-select-temp').val();
        var searchVal = jQuery(".serch-value").val();
        loadExistsTempletes(1, pgeSize, "", searchVal, check);
    });
    jQuery(".btn-create-template").click(function (evt) {
        evt.preventDefault();
        jQuery('#TemplateHdrPopCreate').modal({
            keyboard: false,
            backdrop: 'static'
        }, 'show');
    });
    jQuery(".crenew-done").click(function () {
        clearSession();
        jQuery(".template-field-cre").empty();
        jQuery(".temp-name-cre").val("");
        jQuery('#TemplateHdrPopCreate').modal('hide');
    });

    jQuery(".cre-add-more-field").click(function (evt) {
        evt.preventDefault();
        listEditItem(true);
    });
    jQuery(".save-newtemp").click(function (evt) {
        evt.preventDefault();
        var name = jQuery(".temp-name-cre").val();
        if (name != "") {
            jQuery.ajax({
                cache: false,
                async: false,
                type: "GET",
                url: "/TemplateManager/createNewTemplate?name=" + name,
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            jQuery(".template-field-cre").empty();
                            jQuery(".temp-name-cre").val("");
                            var check = jQuery(".with-deleted").is(':checked');
                            jQuery('#TemplateHdrPopCreate').modal('hide');
                            var pgeSize = jQuery('.templatecontent .cls-select-temp').val();
                            var searchVal = jQuery(".serch-value").val();
                            loadExistsTempletes(1, pgeSize, "", searchVal, check);
                        } else {
                            setInfoMsg(result.msg);
                        }
                    } else {
                        Logout();
                    }
                }
            });
        } else {
            setInfoMsg("Please enter template name.");
        }
    });
    // This	creates a new object
    paging = function (total, page, test) {
        jQuery('#paging-temp').bootpag({
            total: total,
            page: page,
            maxVisible: 5,
            leaps: true,
            firstLastUse: true,
            first: '←',
            last: '→',
            wrapClass: 'pagination',
            activeClass: 'active',
            disabledClass: 'disabled',
            nextClass: 'next',
            prevClass: 'prev',
            lastClass: 'last',
            firstClass: 'first'
        }).on("page", function (event, num) {
            if (test == true) {
                first = true;
                //jQuery('#myModal').css('cursor', 'wait');
                var searchFld = "";//jQuery('select.filter-key-cls').val();
                var searchVal = jQuery(".serch-value").val();
                var pgeSize = jQuery('.templatecontent .cls-select-temp').val();
                var check = jQuery(".with-deleted").is(':checked');
                loadExistsTempletes(num, pgeSize, searchFld, searchVal, check);
            }
        });

    }
    jQuery('.cls-pge-cunt .cls-select-temp').change(function (e) {
        var searchFld = "";//jQuery('select.filter-key-cls').val();
        var pgeSize = jQuery('.cls-pge-cunt .cls-select-temp').val();
        var pgeNum = 1;
        var check = jQuery(".with-deleted").is(':checked');
        var searchVal = jQuery(".serch-value").val();
        loadExistsTempletes(1, pgeSize, searchFld, searchVal, check);
    });
});
function clearSession() {
    jQuery.ajax({
        cache: false,
        async: false,
        type: "GET",
        url: "/TemplateManager/clearSession",
        success: function (result) {
            if (result.login == true) {
                if (result.success == false) {
                    setInfoMsg(result.msg);
                }
            } else {
                Logout();
            }
        }
    });
}
function listEditItem(newtemplate) {
    if (newtemplate == null) {
        newtemplate = false;
    }
    jQuery(".detail-field").empty();
    jQuery.ajax({
        cache: false,
        async: false,
        type: "GET",
        url: "/TemplateManager/getAllTemplateItemDetails",
        success: function (result) {
            if (result.login == true) {
                if (result.success == true) {

                    for (i = 0; i < result.det.length; i++) {
                        var field = "";
                        if (result.det[i].RO_TYPE == "TEXTFIELD") {
                            field = "<div class='align-field field_" + result.det[i].RO_ID + "'><label class='sec-label'>" + result.det[i].RO_NAME + "</label><input type='text' class='form-control input-common-width' name=" + result.det[i].RO_ID + "> <img data-value='" + result.det[i].RO_TYPE + "' name=" + result.det[i].RO_NAME + "  id=" + result.det[i].RO_ID + " alt='ADD' class='elete-add-img small-icon-cls' src='/Resources/images/Account/Add.png'></div>";
                        }
                        else if (result.det[i].RO_TYPE == "COMBOBOX") {
                            field = "<div class='align-field field_" + result.det[i].RO_ID + "'><label class='sec-label'>" + result.det[i].RO_NAME + "</label><select class='form-control input-common-width COMBOBOX_" + result.det[i].RO_ID + " combobox-dynamictemp' name=" + result.det[i].RO_ID + ">"
                                + "<option value=''>Select</option>"
                                    + "</select><img data-value='" + result.det[i].RO_TYPE + "'  name=" + result.det[i].RO_NAME + " id=" + result.det[i].RO_ID + " alt='ADD' class='elete-add-img small-icon-cls' src='/Resources/images/Account/Add.png'></div>";
                        }
                        else if (result.det[i].RO_TYPE == "DATEFIELD") {
                            field = "<div class='align-field field_" + result.det[i].RO_ID + "'><label class='sec-label'>" + result.det[i].RO_NAME + "</label><input readonly  type='text' class='form-control input-common-width class_" + result.det[i].RO_ID + "' name=" + result.det[i].RO_ID + "> <img data-value='" + result.det[i].RO_TYPE + "' name=" + result.det[i].RO_NAME + " id=" + result.det[i].RO_ID + " alt='ADD' class='elete-add-img small-icon-cls' src='/Resources/images/Account/Add.png'></div>";

                        }
                        else if (result.det[i].RO_TYPE == "NUMBERFIELD") {
                            field = "<div class='align-field field_" + result.det[i].RO_ID + "'><label class='sec-label'>" + result.det[i].RO_NAME + "</label><input type='text' class='number form-control input-common-width' name=" + result.det[i].RO_ID + "> <img data-value='" + result.det[i].RO_TYPE + "' name=" + result.det[i].RO_NAME + "  id=" + result.det[i].RO_ID + " alt='ADD' class='elete-add-img small-icon-cls' src='/Resources/images/Account/Add.png'></div>";
                        }
                        else if (result.det[i].RO_TYPE == "CHECKBOX") {
                            field = "<div class='align-field field_" + result.det[i].RO_ID + "'><label class='sec-label'>" + result.det[i].RO_NAME + "</label><input type='checkbox' class='number form-control input-common-width' name=" + result.det[i].RO_ID + "> <img data-value='" + result.det[i].RO_TYPE + "' name=" + result.det[i].RO_NAME + "  id=" + result.det[i].RO_ID + " alt='ADD' class='elete-add-img small-icon-cls' src='/Resources/images/Account/Add.png'></div>";
                        }
                        jQuery(".detail-field").append(field);
                        if (result.det[i].RO_TYPE == "DATEFIELD") {
                            jQuery(".class_" + result.det[i].RO_ID).datepicker({ dateFormat: "dd/M/yy" });
                        }
                    }
                    if (jQuery('.combobox-dynamictemp').length > 0) {
                        jQuery('.combobox-dynamictemp').click(function () {
                            var id = this.name;
                            jQuery.ajax({
                                cache: false,
                                type: "GET",
                                url: "/TemplateManager/getComboDetailsItmId?itmid=" + id,
                                success: function (result) {
                                    if (result.login == true) {
                                        if (result.success == true) {
                                            if (result.data.length > 0) {
                                                var opt = "";
                                                for (i = 0 ; i < result.data.length; i++) {

                                                    opt += "<option value=" + result.data[i].ROLD_CODE + ">" + result.data[i].ROLD_NAME + "</option>";
                                                }
                                                jQuery(".COMBOBOX_" + id).html(opt);
                                            }
                                        } else {
                                            setInfoMsg(result.msg);
                                        }
                                    } else {
                                        Logout();
                                    }
                                }
                            });

                        });
                    }
                    if (newtemplate == true) {
                        jQuery(".elete-add-img").click(function () {
                            var detid = this.id;
                            var name = jQuery(jQuery(this).siblings("label")).html();
                            var type = jQuery(this).attr('data-value');
                            addNewTemplateItem(name, detid,type)
                        });

                    } else {
                        jQuery(".elete-add-img").click(function () {
                            var hedid = jQuery(".temp-hed-id").val();
                            var detid = this.id;
                            var name = jQuery(jQuery(this).siblings("label")).html();
                            jQuery.ajax({
                                cache: false,
                                type: "GET",
                                url: "/TemplateManager/addTemplateField?hedid=" + hedid + "&detid=" + detid + "&name=" + name,
                                success: function (result) {
                                    if (result.login == true) {
                                        if (result.success == true) {
                                            addTemplateItem(hedid);
                                            jQuery('#TemplateDetField').modal('hide');
                                        } else {
                                            setInfoMsg(result.msg);
                                        }
                                    } else {
                                        Logout();
                                    }
                                }
                            });
                        });
                    }
                    jQuery('#TemplateDetField').modal({
                        keyboard: false,
                        backdrop: 'static'
                    }, 'show');
                } else {
                    setInfoMsg(result.msg);
                }
            } else {
                Logout();
            }
        }
    });

}
function addNewTemplateItem(name, detid, type) {
    
    jQuery.ajax({
        cache: false,
        type: "GET",
        url: "/TemplateManager/addNewTempHdrItem?name=" + name + "&detid=" + detid + "&type=" + type,
        success: function (result) {
            if (result.login == true) {
                if (result.success == true) {
                    jQuery(".template-field-cre").empty();
                    if (result.det.length > 0) {
                        jQuery('#TemplateDetField').modal('hide');

                        for (i = 0; i < result.det.length; i++) {
                            var vvv = "";
                            if (result.det[i].RTD_SRCH_FLD != null) {
                                vvv = result.det[i].RTD_SRCH_FLD;
                            }
                            var field = "<div>";
                            if (result.det[i].RTD_TYPE == "TEXTFIELD") {
                                field = "<div class='align-field field_" + result.det[i].RTD_OBJ_ID + "'><label class='sec-label'>" + result.det[i].RTD_NAME + "</label><input class='form-control input-common-width' type='text' name=" + result.det[i].RTD_OBJ_ID + "><div class='align-field'><input data-id='" + result.det[i].RTD_OBJ_ID + "' data-rw='" + result.det[i].RTD_ID + "' type='text' readonly='readonly' class='text-srch-box form-control fixed-width' value='" + vvv + "' ><img data-name=" + result.det[i].RTD_NAME + "  id=" + result.det[i].RTD_OBJ_ID + " alt='Search' class='seach-col-srch small-icon-cls' src='/Resources/images/Account/search.png'><img data-name=" + result.det[i].RTD_NAME + "  id=" + result.det[i].RTD_OBJ_ID + " alt='DELETE' class='delete-new-item small-icon-cls' src='/Resources/images/Account/delete.png'></div>";
                            }
                            else if (result.det[i].RTD_TYPE == "COMBOBOX") {
                                field = "<div class='align-field field_" + result.det[i].RTD_OBJ_ID + "'><label class='sec-label'>" + result.det[i].RTD_NAME + "</label><select class='form-control input-common-width COMBOBOX_" + result.det[i].RTD_OBJ_ID + " combobox-dynamictemp' name=" + result.det[i].RTD_OBJ_ID + ">"
                                    + "<option value=''>Select</option>"
                                        + "</select><div class='align-field'><img data-name=" + result.det[i].RTD_NAME + " id=" + result.det[i].RTD_OBJ_ID + " alt='DELETE' class='delete-new-item small-icon-cls' src='/Resources/images/Account/delete.png'></div>";
                            }
                            else if (result.det[i].RTD_TYPE == "DATEFIELD") {
                                field = "<div class='align-field field_" + result.det[i].RTD_OBJ_ID + "'><label class='sec-label'>" + result.det[i].RTD_NAME + "</label><input readonly  type='text' class='form-control input-common-width class_" + result.det[i].RTD_OBJ_ID + "' data-name=" + result.det[i].RTD_OBJ_ID + "><div class='align-field'><img name=" + result.det[i].RTD_NAME + " id=" + result.det[i].RTD_OBJ_ID + " alt='DELETE' class='delete-new-item small-icon-cls' src='/Resources/images/Account/delete.png'></div>";

                            } else if (result.det[i].RTD_TYPE == "NUMBERFIELD") {
                                field = "<div class='align-field field_" + result.det[i].RTD_OBJ_ID + "'><label class='sec-label'>" + result.det[i].RTD_NAME + "</label><input class='number form-control input-common-width' type='text' name=" + result.det[i].RTD_OBJ_ID + "><div class='align-field'> <input class='valueItmChkbox' data-id='" + result.det[i].RTD_ID + "' type='checkbox'>Mark Value<img data-name=" + result.det[i].RTD_NAME + "  id=" + result.det[i].RTD_OBJ_ID + " alt='DELETE' class='delete-new-item small-icon-cls' src='/Resources/images/Account/delete.png'></div>";
                            }
                            else if (result.det[i].RTD_TYPE == "CHECKBOX") {
                                field = "<div class='align-field field_" + result.det[i].RTD_OBJ_ID + "'><label class='sec-label'>" + result.det[i].RTD_NAME + "</label><input class='form-control input-common-width' type='checkbox' name=" + result.det[i].RTD_OBJ_ID + "><div class='align-field'><img data-name=" + result.det[i].RTD_NAME + "  id=" + result.det[i].RTD_OBJ_ID + " alt='DELETE' class='delete-new-item small-icon-cls' src='/Resources/images/Account/delete.png'></div>";
                            }
                            field += "</div>";
                            jQuery(".template-field-cre").append(field);
                            if (result.det[i].RTD_TYPE == "DATEFIELD") {
                                jQuery(".class_" + result.det[i].RO_ID).datepicker({ dateFormat: "dd/M/yy" });
                            }

                        }
                        jQuery(".delete-new-item").click(function (evt) {
                            evt.preventDefault();
                            var newid = this.id;
                            var thisv = jQuery(this).parent();
                            var dtname = jQuery(jQuery(jQuery(thisv)).siblings("label")).html();
                            jQuery.ajax({
                                cache: false,
                                type: "GET",
                                url: "/TemplateManager/deleteTempHdrItmNew?detid=" + newid + "&name=" + dtname,
                                success: function (result) {
                                    if (result.login == true) {
                                        if (result.success == true) {
                                            jQuery(thisv).parent().remove();
                                        } else {
                                            setInfoMsg(result.msg);
                                        }
                                    } else {
                                        Logout();
                                    }
                                }
                            });
                        });
                        jQuery(".valueItmChkbox").click(function () {
                            var val = false;
                            if (jQuery(this).is(":checked")) {
                                val = true;
                            }
                            var id = jQuery(this).attr('data-id');
                            jQuery.ajax({
                                cache: false,
                                type: "GET",
                                url: "/TemplateManager/updateDefaultTemplateNumberField?detid=" + id + "&val=" + val,
                                success: function (result) {
                                    if (result.login == true) {
                                        if (result.success == false) {
                                            setInfoMsg(result.msg);
                                        }
                                    } else {
                                        Logout();
                                    }
                                }
                            });
                        });
                        jQuery(".seach-col-srch").click(function () {
                            var data= jQuery(this).siblings('input');
                            var headerKeys = Array();
                            headerKeys = ["Row", "Code", "Description"];
                            field = "SRCHFILDSRCH";
                            var x = new CommonSearch(headerKeys, field, data);
                        });
                        jQuery(".text-srch-box").focusout(function () {
                            if (jQuery(this).val() != "") {
                                var vl = jQuery(this).val();
                                var id = jQuery(this).attr('data-rw');
                                var objid = jQuery(this).attr('data-id');
                                jQuery.ajax({
                                    cache: false,
                                    type: "GET",
                                    url: "/TemplateManager/addSearchForField?objid=" + objid + "&detid=" + id + "&val=" + vl,
                                    success: function (result) {
                                        if (result.login == true) {
                                            if (result.success == false) {
                                                setInfoMsg(result.msg);
                                            }
                                        } else {
                                            Logout();
                                        }
                                    }
                                });
                            }

                        });
                    }
                } else {
                    setInfoMsg(result.msg);
                }
            } else {
                Logout();
            }
        }
    });
}
var first = true;
function loadExistsTempletes(pgeNum, pgeSize, searchFld, searchVal, withdeleted) {
    jQuery(".template-det-table tr.new-row").remove();
    jQuery.ajax({
        cache: false,
        type: "GET",    
        url: "/TemplateManager/getTemplateHeaderDetails?withdeleted=" + withdeleted + "&pgeNum=" + pgeNum + "&pgeSize=" + pgeSize + "&searchFld=" + searchFld + "&searchVal=" + searchVal,
        success: function (result) {
            if (result.login == true) {
                if (result.success == true) {
                    if (result.data.length > 0) {
                        for (i = 0; i < result.data.length; i++) {
                            var del = (result.data[i].RTH_STUS == 1) ? '<img alt="Delete" class="deleteico-img small-icon-cls" src="/Resources/images/Account/delete.png">' : '<img class="small-icon-cls-disable" src="/Resources/images/Account/disable-delete.png">';
                            var selectclass = (result.data[i].RTH_STUS == 1) ? '<td class="select-row-template">Select</td>' : '<td>Select</td>';
                            jQuery('.template-det-table').append('<tr class="new-row">' +
                                 selectclass +
                                '<td class="select-id nodisplay">' + result.data[i].RTH_ID + '</td>' +
                                    '<td>' + result.data[i].RTH_CD + '</td>' +
                                      '<td class="select-name">' + result.data[i].RTH_DESC + '</td>' +
                                       '<td>' + result.data[i].RTH_CRE_BY + '</td>' +
                                       '<td>' + my_date_format_with_time(convertDate(result.data[i].RTH_CRE_DT)) + '</td>' +
                                     '<td>'
                                     + '<img alt="Edit" class="editico-img small-icon-cls" src="/Resources/images/Account/edit.png">'
                                     + del
                                     + '<img alt="Infomation" class="infoico-img small-icon-cls" src="/Resources/images/Account/info.png">'
                                     + '</td>' +
                                    '</tr>');
                        }
                        jQuery(".editico-img").click(function () {
                            var id = jQuery(jQuery(this).parent()).siblings("td.select-id").html();
                            var name = jQuery(jQuery(this).parent()).siblings("td.select-name").html();
                            editTemplateHdr(id, name);
                        });
                        jQuery(".deleteico-img").click(function () {
                            var id = jQuery(jQuery(this).parent()).siblings("td.select-id").html();
                            var name = jQuery(jQuery(this).parent()).siblings("td.select-name").html();
                            deleteTemplateHdr(id, name);
                        });
                        jQuery(".infoico-img").click(function () {
                           var id = jQuery(jQuery(this).parent()).siblings("td.select-id").html();
                            var name = jQuery(jQuery(this).parent()).siblings("td.select-name").html();
                            //infoTemplateHdr(id, name);
                            addTemplateItem(id, name, true);
                        });
                    }
                    jQuery(".select-row-template").click(function (evt) {
                        evt.preventDefault();
                        var hedid = jQuery(jQuery(this).siblings("td.select-id")).html();
                        if (hedid != null) {
                            jQuery.ajax({
                                cache: false,
                                type: "GET",
                                url: "/TemplateManager/getTemplateHedDetForForm?hedid=" + hedid,
                                success: function (result) {
                                    if (result.login == true) {
                                        if (result.success == true) {
                                            jQuery('#TemplateManager').modal('hide');
                                            var fieldset = "<fieldset>";
                                            var field = "";
                                            fieldset += "<legend>" + result.det[0].TEMPLATE_NAME + "<img id='fieldset_" + result.det[0].TEMPLATE_ID + "' alt='Delete' class='elete-det-tempfrm small-icon-cls' src='/Resources/images/Account/delete.png'></legend>";
                                            for (i = 0; i < result.det.length; i++) {
                                                if (result.det[i].FIELD_TYPE == "TEXTFIELD") {
                                                    field += "<div><label class='sec-label'>" + result.det[i].FIELD_NAME + "</label><input class='form-control input-common-width' type='text' name='name_" + result.det[i].TEMPLATE_ID + "_" + result.det[i].DETAIL_ID + "'></div>";
                                                }
                                                else if (result.det[i].FIELD_TYPE == "COMBOBOX") {
                                                    field += "<div><label class='sec-label'>" + result.det[i].FIELD_NAME + "</label><select  class='form-control input-common-width COMBOBOX_" + result.det[i].DETAIL_ID + " combobox-dynamictval' data-val='" + result.det[i].DETAIL_ID + "' name='name_" + result.det[i].TEMPLATE_ID + "_" + +result.det[i].DETAIL_ID + "'>"
                                                        + "<option value=''>Select</option>"
                                                            + "</select></div>";
                                                }
                                                else if (result.det[i].FIELD_TYPE == "DATEFIELD") {
                                                    field += "<div><label class='sec-label'>" + result.det[i].FIELD_NAME + "</label><input readonly type='text' class='form-control input-common-width date-field' name='name_" + result.det[i].TEMPLATE_ID + "_" + result.det[i].DETAIL_ID + "'></div>";

                                                } else if (result.det[i].FIELD_TYPE == "NUMBERFIELD") {
                                                    field += "<div><label class='sec-label'>" + result.det[i].FIELD_NAME + "</label><input class='number form-control input-common-width' type='text' name='name_" + result.det[i].TEMPLATE_ID + "_" + result.det[i].DETAIL_ID + "'></div>";
                                                } else if (result.det[i].FIELD_TYPE == "CHECKBOX") {
                                                    field += "<div><label class='sec-label'>" + result.det[i].FIELD_NAME + "</label><input class='form-control input-common-width' type='checkbox' name='name_" + result.det[i].TEMPLATE_ID + "_" + result.det[i].DETAIL_ID + "'></div>";
                                                }
                                            }
                                            fieldset += field;
                                            fieldset += "</fieldset>";
                                            jQuery(".template-item").append(fieldset);
                                            jQuery(".date-field").datepicker({ dateFormat: "dd/M/yy" });

                                           
                                            jQuery(".elete-det-tempfrm").click(function (evt) {
                                                var id = this.id;
                                                var th = jQuery(this);
                                                var assingDoc = jQuery(".template-uniq-class").val();
                                                var assingDoc = jQuery(".template-uniq-class").val();
                                                alert(assingDoc);
                                                Lobibox.confirm({
                                                    msg: "Do you want to delete template from form ?",
                                                    callback: function ($this, type, ev) {
                                                        if (type == "yes") {
                                                            evt.preventDefault();
                                                            jQuery.ajax({
                                                                cache: false,
                                                                type: "GET",
                                                                url: "/TemplateManager/removeAddedTemplate?hedid=" + id + "&assigncode=" + assingDoc,
                                                                success: function (result) {
                                                                    if (result.login == true) {
                                                                        if (result.success == true) {
                                                                            jQuery(jQuery(jQuery(th).parent('legend'))).parent('fieldset').remove();
                                                                        } else {
                                                                            setInfoMsg(result.msg);
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
                                            if (jQuery('.combobox-dynamictval').length > 0) {
                                                jQuery('.combobox-dynamictval').click(function () {
                                                    var id = jQuery(this).attr('data-val');
                                                    if (jQuery(jQuery(jQuery(this)).find("option")).length <= 1) {
                                                        jQuery.ajax({
                                                            cache: false,
                                                            type: "GET",
                                                            url: "/TemplateManager/getComboDetails?detid=" + id,
                                                            success: function (result) {
                                                                if (result.login == true) {
                                                                    if (result.success == true) {
                                                                        if (result.data.length > 0) {
                                                                            var opt = "";
                                                                            for (i = 0 ; i < result.data.length; i++) {

                                                                                opt += "<option value=" + result.data[i].ROLD_CODE + ">" + result.data[i].ROLD_NAME + "</option>";
                                                                            }
                                                                            jQuery(".COMBOBOX_" + id).html(opt);
                                                                        }
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
                                            }

                                        } else {
                                            setInfoMsg(result.msg);
                                        }

                                       
                                    } else {
                                        Logout();
                                    }
                                }
                            });
                            
                        } else {
                            setInfoMsg("Invalid template id.");
                        }
                    });

                    if (first == true) {
                        paging(result.totalDoc, pgeNum, true);
                        first = false;
                    }

                    jQuery('#paging').empty();
                    paging(result.totalDoc, pgeNum, false);
                    check = false;
                } else {
                    setInfoMsg(result.msg);
                }
            } else {
                Logout();
            }
        }
    });
}

function editTemplateHdr(hedid, name) {
    if (hedid != null) {
        Lobibox.confirm({
            msg: "Do you want to edit " + name + "?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    addTemplateItem(hedid, name);
                }
            }
        });
    } else {
        setInfoMsg("Invalid template.")
    }
}
function addTemplateItem(hedid, name, readonly) {
    var read = "";
    if (readonly == null) {
        readonly = false;
    } else {
        read = "disabled='disabled'";
    }
    jQuery.ajax({
        cache: false,
        type: "GET",
        url: "/TemplateManager/getTemplateHedDet?hedid=" + hedid + "&readony=" + readonly,
        success: function (result) {
            jQuery(".temp-name").val("");
            jQuery(".temp-hed-id").val("");
            jQuery(".template-field").empty();
            if (result.login == true) {
                if (result.success == true) {
                    jQuery("#TemplateHdrPop .modal-title").empty();
                    if (readonly == false) {
                        jQuery("#TemplateHdrPop .modal-title").html("Update Template " + name + "<div class='head-iddone no-display'>"+hedid+"</div>");
                    } else {
                        jQuery("#TemplateHdrPop .modal-title").html("View Template " + name + "<div class='head-iddone no-display'>" + hedid + "</div>");
                    }
                    jQuery(".temp-name").val(result.data.RTH_DESC);
                    jQuery(".temp-name").attr("disabled", "disabled");
                    jQuery(".temp-hed-id").val(result.data.RTH_ID);
                    if (readonly == true) {
                        jQuery(".add-more-field").hide();
                        jQuery(".info-addmorefield").hide();
                    } else {
                        jQuery(".add-more-field").show();
                        jQuery(".info-addmorefield").show();
                    }
                    if (result.det.length > 0) {
                        for (i = 0; i < result.det.length; i++) {
                            var field = "";
                            var deleimg = "";
                            var editname = "";
                            var read = "";
                            if (readonly == false) {
                                deleimg = "<img id=" + result.det[i].RTD_ID + " alt='Delete' class='elete-det-img small-icon-cls' src='/Resources/images/Account/delete.png'>";
                                editname = "<img alt='Edit' class='element-nameedit small-icon-cls float-left' src='/Resources/images/Account/edit.png'><input class='form-control input-common-width edit-textvalue nodisplay' id='editname_" + result.det[i].RTD_ID + "' value='" + result.det[i].RTD_NAME + "' >";
                                
                            } else {
                                read = " readonly='readonly' ";
                            }
                            var hasSearch = "";
                            if (result.det[i].RO_SEARCH == "1") {
                                var sch = "";
                                if (readonly == false) {
                                    sch = "<img data-name=" + result.det[i].RTD_NAME + "  id=" + result.det[i].RTD_OBJ_ID + " alt='Search' class='seach-col-srch small-icon-cls' src='/Resources/images/Account/search.png'>";
                                }
                                hasSearch = "<div class='align-field'><input data-id='" + result.det[i].RTD_OBJ_ID + "' data-rw='" + result.det[i].RTD_ID + "' type='text' readonly='readonly' class='text-srch-box form-control fixed-width' value='" + result.det[i].RTD_SRCH_FLD + "' >" + sch;
                            }
                            if (result.det[i].RO_TYPE == "TEXTFIELD") {
                                field = "<div class='align-field field_" + result.det[i].RTD_ID + "'>" + editname + "<label class='sec-label'>" + result.det[i].RTD_NAME + "</label><input class='form-control input-common-width' " + read + " type='text' name=" + result.det[i].RTD_ID + "> "  + hasSearch +  deleimg + "</div>";
                            }
                            else if (result.det[i].RO_TYPE == "COMBOBOX") {
                                field = "<div class='align-field field_" + result.det[i].RTD_ID + "'>" + editname + "<label class='sec-label'>" + result.det[i].RTD_NAME + "</label><select  " + read + " class='form-control input-common-width COMBOBOX_" + result.det[i].RTD_ID + " combobox-dynamic' name=" + result.det[i].RTD_ID + ">"
                                    + "<option value=''>Select</option>"
                                        + "</select>" + deleimg + "</div>";
                            }
                            else if (result.det[i].RO_TYPE == "DATEFIELD") {
                                field = "<div class='align-field field_" + result.det[i].RTD_ID + "'>" + editname + "<label class='sec-label'>" + result.det[i].RTD_NAME + "</label><input readonly " + read + " type='text' class='form-control input-common-width class_" + result.det[i].RTD_ID + "' name=" + result.det[i].RTD_ID + "> " + deleimg + "</div>";

                            } else if (result.det[i].RO_TYPE == "NUMBERFIELD") {
                                var check = "";
                                if (result.det[i].RTD_IS_VALUE == "1") {
                                    check = " checked='checked' ";
                                }
                                field = "<div class='align-field field_" + result.det[i].RTD_ID + "'>" + editname + "<label class='sec-label'>" + result.det[i].RTD_NAME + "</label><input class='number form-control input-common-width' " + read + " type='text' name=" + result.det[i].RTD_ID + "> <input class='valueItmChkbox' data-id='" + result.det[i].RTD_ID + "' " + check + read + "  type='checkbox'>Mark Value" + deleimg + "</div>";
                            } else if (result.det[i].RO_TYPE == "CHECKBOX") {
                                field = "<div class='align-field field_" + result.det[i].RTD_ID + "'>" + editname + "<label class='sec-label'>" + result.det[i].RTD_NAME + "</label><input class='form-control input-common-width' " + read + " type='checkbox' name=" + result.det[i].RTD_ID + "> " + hasSearch + deleimg + "</div>";
                            }
                            jQuery(".template-field").append(field);
                            if (result.det[i].RO_TYPE == "DATEFIELD") {
                                jQuery(".class_" + result.det[i].RTD_ID).datepicker({ dateFormat: "dd/M/yy" });
                            }
                        }
                        
                            jQuery(".valueItmChkbox").click(function () {
                                var val = false;
                                if (jQuery(this).is(":checked")) {
                                    val = true;
                                }
                                if (readonly == false) {
                                    var id = jQuery(this).attr('data-id');
                                    jQuery.ajax({
                                        cache: false,
                                        type: "GET",
                                        url: "/TemplateManager/updateDefaultTemplateNumberField?detid=" + id + "&val=" + val+"&update="+true,
                                        success: function (result) {
                                            if (result.login == true) {
                                                if (result.success == false) {
                                                    setInfoMsg(result.msg);
                                                }
                                            } else {
                                                Logout();
                                            }
                                        }
                                    });
                                } 
                            });
                        if (jQuery('.combobox-dynamic').length > 0) {
                            jQuery('.combobox-dynamic').click(function () {
                                var id = this.name;
                                jQuery.ajax({
                                    cache: false,
                                    type: "GET",
                                    url: "/TemplateManager/getComboDetails?detid=" + id,
                                    success: function (result) {
                                        if (result.login == true) {
                                            if (result.success == true) {
                                                if (result.data.length > 0) {
                                                    var opt = "";
                                                    for (i = 0 ; i < result.data.length; i++) {

                                                        opt += "<option value=" + result.data[i].ROLD_CODE + ">" + result.data[i].ROLD_NAME + "</option>";
                                                    }
                                                    jQuery(".COMBOBOX_" + id).html(opt);
                                                }
                                            } else {
                                                setInfoMsg(result.msg);
                                            }
                                        } else {
                                            Logout();
                                        }
                                    }
                                });

                            });
                        }
                        jQuery(".elete-det-img").click(function () {
                            var id = this.id;
                            Lobibox.confirm({
                                msg: "Do you want to delete field ?",
                                callback: function ($this, type, ev) {
                                    if (type == "yes") {
                                        jQuery.ajax({
                                            cache: false,
                                            type: "GET",
                                            url: "/TemplateManager/deleteFieldItem?detid=" + id,
                                            success: function (result) {
                                                if (result.login == true) {
                                                    if (result.success == true) {
                                                        jQuery(".field_" + id).remove();
                                                        setSuccesssMsg(result.msg);
                                                    } else {
                                                        setInfoMsg(result.msg);
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
                        jQuery(".element-nameedit").click(function () {
                            jQuery(jQuery(this).siblings("input")).show();
                            jQuery(jQuery(this).siblings("label")).hide();
                        });
                        jQuery(".edit-textvalue").focusout(function () {
                            var nme = jQuery(this).val();
                            var id = this.id;
                            updateLabelText(id, nme,this);
                            
                        });
                        if (readonly == false) {
                            jQuery(".seach-col-srch").click(function () {
                                var data = jQuery(this).siblings('input');
                                var headerKeys = Array();
                                headerKeys = ["Row", "Code", "Description"];
                                field = "SRCHFILDSRCH";
                                var x = new CommonSearch(headerKeys, field, data);
                            });
                            jQuery(".text-srch-box").focusout(function () {
                                if (jQuery(this).val() != "") {
                                    var vl = jQuery(this).val();
                                    var id = jQuery(this).attr('data-rw');
                                    var objid = jQuery(this).attr('data-id');
                                    jQuery.ajax({
                                        cache: false,
                                        type: "GET",
                                        url: "/TemplateManager/addSearchForField?objid=" + objid + "&detid=" + id + "&val=" + vl + "&update=" + true,
                                        success: function (result) {
                                            if (result.login == true) {
                                                if (result.success == false) {
                                                    setInfoMsg(result.msg);
                                                }
                                            } else {
                                                Logout();
                                            }
                                        }
                                    });
                                }

                            });
                        }
                    }
                    jQuery('#TemplateHdrPop').modal({
                        keyboard: false,
                        backdrop: 'static'
                    }, 'show');
                } else {
                    setInfoMsg(result.msg);
                }
            } else {
                Logout();
            }
        }
    });
}
function updateLabelText(id,name,obj){
    jQuery.ajax({
        cache: false,
        type: "GET",
        url: "/TemplateManager/updateLabelText?itmid=" + id + "&name=" + name,
        success: function (result) {
            if (result.login == true) {
                if (result.success == true) {
                    jQuery(obj).hide();
                    jQuery(jQuery(obj).siblings("label")).empty();
                    jQuery(jQuery(obj).siblings("label")).append(name);
                    jQuery(jQuery(obj).siblings("label")).show();
                } else {
                    setInfoMsg(result.msg);
                }
            } else {
                Logout();
            }
        }
    });
}
function deleteTemplateHdr(hedid, name) {
    if (hedid != null) {
        Lobibox.confirm({
            msg: "Do you want to delete " + name + " ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    jQuery.ajax({
                        cache: false,
                        type: "GET",
                        url: "/TemplateManager/updateStusTemplateHeader?hedid=" + hedid + "&stus=" + 0,
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    var check = jQuery(".with-deleted").is(':checked');
                                    var pgeSize = jQuery('.templatecontent .cls-select-temp').val();
                                    var searchVal = jQuery(".serch-value").val();
                                    loadExistsTempletes(1, pgeSize, "", searchVal, check);
                                    setSuccesssMsg(result.msg + "-" + name);
                                } else {
                                    setInfoMsg(result.msg);
                                }
                            } else {
                                Logout();
                            }
                        }
                    });
                }
            }
        });
    } else {
        setInfoMsg("Invalid template.")
    }
}
//function infoTemplateHdr(hedid, name) {
//    if (hedid != null) {
//        jQuery.ajax({
//            cache: false,
//            type: "GET",
//            url: "/TemplateManager/showInfoTemplateHdr?hedid=" + hedid,
//            success: function (result) {
//                if (result.login == true) {
//                    if (result.success == true) {

//                    }
//                } else {
//                    Logout();
//                }
//            }
//        });
//    } else {
//        setInfoMsg("Invalid template.")
//    }
//}

function drawFormTemplateField(data) {
    jQuery(".template-item").empty();
    if (data != null && data.length > 0) {
        for (j = 0 ; j < data.length; j++) {
            var fieldset = "<fieldset class='filedset_" + data[j].TEMPLATE_ID + "'>";
            var field = "";
            fieldset += "<legend>" + data[j].TEMPLATE_NAME + "<img id='fieldset_" + data[j].TEMPLATE_ID + "' alt='Delete' class='elete-det-tempfrm small-icon-cls' src='/Resources/images/Account/delete.png'></legend>";
            //fieldset += field;
            fieldset += "</fieldset>";
            jQuery(".template-item").append(fieldset);

            for (i = 0; i < data[j].TEMPLATE_DET.length; i++) {

                if (data[j].TEMPLATE_DET[i].FIELD_TYPE == "TEXTFIELD") {
                    field = "<div><label class='sec-label'>" + data[j].TEMPLATE_DET[i].FIELD_NAME + "</label><input   class='form-control input-common-width' type='text' name='name_" + data[j].TEMPLATE_ID + "_" + data[j].TEMPLATE_DET[i].DETAIL_ID + "'></div>";
                }
                else if (data[j].TEMPLATE_DET[i].FIELD_TYPE == "COMBOBOX") {
                    field = "<div><label class='sec-label'>" + data[j].TEMPLATE_DET[i].FIELD_NAME + "</label><select id='COMBOBOX_ID_" + data[j].TEMPLATE_DET[i].DETAIL_ID + "'  class='form-control input-common-width COMBOBOX_" + data[j].TEMPLATE_DET[i].DETAIL_ID + " combobox-dynamictval'   data-val='" + data[j].TEMPLATE_DET[i].DETAIL_ID + "' name='name_" + data[j].TEMPLATE_ID + "_" + +data[j].TEMPLATE_DET[i].DETAIL_ID + "'>"
                        + "<option value=''>Select</option>"
                            + "</select></div>";
                }
                else if (data[j].TEMPLATE_DET[i].FIELD_TYPE == "DATEFIELD") {
                    field = "<div><label class='sec-label'>" + data[j].TEMPLATE_DET[i].FIELD_NAME + "</label><input   readonly type='text' class='form-control input-common-width date-field' name='name_" + data[j].TEMPLATE_ID + "_" + data[j].TEMPLATE_DET[i].DETAIL_ID + "'></div>";

                } else if (data[j].TEMPLATE_DET[i].FIELD_TYPE == "NUMBERFIELD") {
                    field = "<div><label class='sec-label'>" + data[j].TEMPLATE_DET[i].FIELD_NAME + "</label><input   class='number form-control input-common-width' type='text' name='name_" + data[j].TEMPLATE_ID + "_" + data[j].TEMPLATE_DET[i].DETAIL_ID + "'></div>";
                } else if (data[j].TEMPLATE_DET[i].FIELD_TYPE == "CHECKBOX") {
                    field = "<div><label class='sec-label'>" + data[j].TEMPLATE_DET[i].FIELD_NAME + "</label><input   class='form-control input-common-width' type='checkbox' name='name_" + data[j].TEMPLATE_ID + "_" + data[j].TEMPLATE_DET[i].DETAIL_ID + "'></div>";
                }
                jQuery(".template-item ." + "filedset_" + data[j].TEMPLATE_ID).append(field);
                
            }
            jQuery(".template-item .date-field").datepicker({ dateFormat: "dd/M/yy" });

        }
        jQuery(".template-item .elete-det-tempfrm").click(function (evt) {
            var id = this.id;
            var th = jQuery(this);
            var assingDoc = jQuery(".template-uniq-class").val();
            Lobibox.confirm({
                msg: "Do you want to delete template from form ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        evt.preventDefault();

                        jQuery.ajax({
                            cache: false,
                            type: "GET",
                            url: "/TemplateManager/removeAddedTemplate?hedid=" + id + "&assigncode=" + assingDoc,
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        jQuery(jQuery(jQuery(th).parent('legend'))).parent('fieldset').remove();
                                    } else {
                                        setInfoMsg(result.msg);
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

        $(".template-item .combobox-dynamictval").each(function () {
            var id = jQuery(this).attr('data-val');
            var vale = jQuery(this).attr('data-selected');
            if (jQuery(jQuery(jQuery(this)).find("option")).length <= 1) {
                jQuery.ajax({
                    cache: false,
                    type: "GET",
                    url: "/TemplateManager/getComboDetails?detid=" + id,
                    success: function (result) {
                        if (result.login == true) {
                            if (result.success == true) {
                                if (result.data.length > 0) {
                                    var opt = "";
                                    for (i = 0 ; i < result.data.length; i++) {
                                        var selected = "";
                                        //var selected = (result.data[i].ROLD_CODE == vale) ? " selectd = 1 " : "";
                                        opt += "<option " + selected + " value=" + result.data[i].ROLD_CODE + ">" + result.data[i].ROLD_NAME + "</option>";
                                    }
                                    jQuery(".template-item .COMBOBOX_" + id).html(opt);
                                    jQuery(".template-item .COMBOBOX_" + id).val(vale);
                                }
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
        //if (jQuery('.template-item .combobox-dynamictval').length > 0) {
        //    jQuery('.template-item .combobox-dynamictval').click(function () {
        //        var id = jQuery(this).attr('data-val');
        //        var vale = jQuery(this).attr('data-selected');
        //        console.log(vale);
        //        if (jQuery(jQuery(jQuery(this)).find("option")).length <= 1) {
        //            jQuery.ajax({
        //                cache: false,
        //                type: "GET",
        //                url: "/TemplateManager/getComboDetails?detid=" + id,
        //                success: function (result) {
        //                    if (result.login == true) {
        //                        if (result.success == true) {
        //                            if (result.data.length > 0) {
        //                                var opt = "";
        //                                for (i = 0 ; i < result.data.length; i++) {
        //                                    var selected = "";
        //                                    //var selected = (result.data[i].ROLD_CODE == vale) ? " selectd = 1 " : "";
        //                                    opt += "<option "+selected+" value=" + result.data[i].ROLD_CODE + ">" + result.data[i].ROLD_NAME + "</option>";
        //                                }
        //                                jQuery(".template-item .COMBOBOX_" + id).html(opt);
        //                                jQuery(".template-item .COMBOBOX_" + id).val(vale);
        //                            }
        //                        } else {
        //                            setInfoMsg(result.msg);
        //                        }
        //                    } else {
        //                        Logout();
        //                    }
        //                }
        //            });
        //        }

        //    });

        //}
    }
}

