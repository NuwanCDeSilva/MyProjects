jQuery(document).ready(function () {
    if (jQuery(".main-pge-msg .error-msg") != null) {
        if (jQuery(".main-pge-msg .error-msg p") != null) {
            var error = jQuery(".main-pge-msg .error-msg p").html();
            if (typeof (error) != "undefined") {
                //setMessage(error, "error");
                showNotice(error);
            }
        }
    }
    if (jQuery(".main-pge-msg .notice-msg") != null) {
        if (jQuery(".main-pge-msg .notice-msg p") != null) {
            var error = jQuery(".main-pge-msg .notice-msg p").html();
            if (typeof (error) != "undefined") {
                //setMessage(error, "notice");
                showNotice(error);
            }
        }
    }
    //load companies when user id box is filled
    if (jQuery(".user_name_cls").length > 0 && jQuery(".user_name_cls").val() != null && jQuery(".user_name_cls").val()!="")
    {
        getCompany();
    }
    //get comany when focus out the user name event
    jQuery(".user_name_cls").focusout(function () {
        
        getCompany();
    })
    jQuery("#Mst_com_Mc_cd").focus(function () {
        getCompany();
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
                if (result.success = true) {
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
                        showNotice(result.msg);
                        //if (jQuery(".container .messages") != null && jQuery("#User_name").val() != "") {
                        //    jQuery(".container .messages").append(' <div class=" row navbar navbar-inverse navbar-fixed-top error-msg"><div class="col-md-5 col-md-offset-3 ">' + result .msg+ ' </div></div>');
                        //}
                    }
                    select.insertAdjacentHTML('beforeEnd', options.join('\n'));
                } else {
                    //if (jQuery(".container .messages") != null) {
                    //    jQuery(".container .messages").append(' <div class=" row navbar navbar-inverse navbar-fixed-top error-msg"><div class="col-md-5 col-md-offset-3 ">' + result.msg + ' </div></div>');
                    //}
                    showNotice(result.msg);
                }
                activeLoginButton();
            },
            error: function (response) {
                if (response.success = false)
                {
                    jQuery(".container .messages").empty();
                    var select = document.getElementById("Mst_com_Mc_cd");
                    jQuery("#Mst_com_Mc_cd").empty();
                    var options = [];
                    var option = document.createElement('option');
                    option.text = "Select Company";
                    option.value = "";
                    options.push(option.outerHTML);
                    select.insertAdjacentHTML('beforeEnd', options.join('\n'));
                    //if (jQuery(".container .messages") != null) {
                       
                    //    jQuery(".container .messages").append(' <div class=" row navbar navbar-inverse navbar-fixed-top error-msg"><div class="col-md-5 col-md-offset-3 ">' + result.msg + ' </div></div>');
                    //}
                    showNotice(result.msg);
                    activeLoginButton();
                }
                
            }

        });
        
    }
    //company drop down change event
    jQuery(".company-dropdown").change(function (evt) {
        evt.preventDefault();
        if (jQuery(".company-dropdown").val() != null) {
            jQuery(".company-description").val(jQuery('#Mst_com_Mc_cd :selected').text());
        } else {
            jQuery(".company-description").val("");
        }

    });

    //;ogin form login button click event
    jQuery(".login-btn").click(function (evt) {
        var form = jQuery('.login-form');
        jQuery(".container .messages").empty();
        deactiveLoginButton();
        evt.preventDefault();
        jQuery.ajax({
            cache: false,
            async: true,
            type: "POST",
            url: '/',//+form.attr('action') + '&request=req'
            data: form.serialize()+ '&request=req',
            success: function (data)
            {
                if (data != null){
                    if (data.success==true) {
                        window.location.replace("/Home/Index");
                    } else
                    {
                        //if (jQuery(".container .messages").length > 0) {

                        //    jQuery(".container .messages").append(' <div class=" row navbar navbar-inverse navbar-fixed-top error-msg"><div class="col-md-5 col-md-offset-3 ">' + data.msg + ' </div></div>');
                        showNotice(data.msg);
                        activeLoginButton();
                        //}
                    }
                }
            }
        });
    });

    var spinnerVisible = false;
    function showProgress() {
        if (!spinnerVisible) {
            jQuery("div#spinner img").fadeIn("fast");
            spinnerVisible = true;
        }
    };
    function hideProgress() {
        if (spinnerVisible) {
            var spinner = jQuery("div#spinner img");
            spinner.stop();
            spinner.fadeOut("fast");
            spinnerVisible = false;
        }
    };
    //deactivate login form button
    function deactiveLoginButton() {
        if (jQuery('.btn-primary.login-btn')!=null){
            jQuery('.btn-primary.login-btn').attr('disabled', true);
           // showProgress();
        }
    }
    //activate login form button
    function activeLoginButton() {
        if (jQuery('.btn-primary.login-btn') != null) {
            jQuery('.btn-primary.login-btn').attr('disabled', false);
          //  hideProgress();
        }
    }
    //login user div mouse hover event
    jQuery(".main-log-panel ul.main-logoff .login-user-cls").mouseover(function () {
        if (jQuery(window).width() >= 768) {
            if (jQuery(".user-det-panel") != null && jQuery('.user-det-panel').css('display') == 'none') {
                if (jQuery(".user-det-panel") != null) {
                    var possition = jQuery(".login-ul.main-logoff").position();
                    jQuery(".user-det-panel").css({ top: possition.top + 45, left: possition.left + 20 });
                    setTimeout(function () { if (!jQuery(".user-det-panel").is(":hover")) jQuery(".user-det-panel").fadeIn(); }, 100);
                }
            }
        }
    });
    //login user div mouse leave event
    jQuery(".main-log-panel ul.main-logoff .login-user-cls").mouseleave(function () {
        if (jQuery(window).width() >= 768) {
            if (jQuery(".user-det-panel") != null) {
                setTimeout(function () { if (!jQuery(".user-det-panel").is(":hover")) jQuery(".user-det-panel").fadeOut(); }, 1000);
            }
        }
    });
    //login user details panel mouse leave event
    jQuery(".user-det-panel").mouseleave(function () {
        setTimeout(function () {
            if (!jQuery(".user-det-panel").is(":hover"))
                jQuery(".user-det-panel").fadeOut();
        }, 1000);
    });

    jQuery(window).resize(function () {

        //var possition = jQuery(".login-ul.main-logoff").position();
        //jQuery(".user-det-panel").css({ top: possition.top + 45, left: possition.left });
    });
    //log out link click event in home page
    jQuery(".logout-link-cls").click(function (evt) {
        jQuery.ajax({
            cache: false,
            async: true,
            type: "POST",
            url: "/Login/LogOff",
            data:{request:"req"},
            success: function (result) {
                if (result.length > 0) {
                    console.log(result);
                    if (result.success.length > 0 && result.success == true) {
                        window.location.replace("~/Login/Login");
                    } else {
                        if (result.msg.length>0) {
                            console.log(result.msg);
                        }
                    }
                }
            },
            error: function (result) {

            }
        });
    });


/*
sripts for cusdec form
*/
    if (jQuery(".download-xml-form .database-select").length > 0) {
        getDatabases();
    }
    jQuery("#DBModelList_Add_db_id").focus(function () {
        getDatabases();
    });
    //get databases for dropdown list in cusdec
    function getDatabases()
    {
        if (jQuery(".download-xml-form .database-select") != null) {
            jQuery.ajax({
                type: "GET",
                url: "/Cusdec/GetDatabaseList",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                   if(result != null){
                        if (result.login == true) {
                            if (result.success == true) {
                                var select = document.getElementById("DBModelList_Add_db_id");
                                jQuery(".download-xml-form .database-select").empty();
                                var options = [];
                                var option = document.createElement('option');
                                option.text = "Select Database"
                                option.value = "";
                                options.push(option.outerHTML);
                                if (result.data != null && result.data.length != 0) {
                                    for (var i = 0; i < result.data.length; i++) {
                                        option.text = result.data[i].Add_db_name;
                                        option.value = result.data[i].Add_db_id;
                                        options.push(option.outerHTML);
                                    }
                                } else {
                                    option.text = "Select Database";
                                    option.value = "";
                                    options.push(option.outerHTML);
                                }
                                select.insertAdjacentHTML('beforeEnd', options.join('\n'))

                            } else {
                                if (result.msg.length > 0) {
                                    //setMessage(result.msg,'error');
                                    showNotice(result.msg);
                                }
                            }
                        } else {
                            Logout();
                        }
                   }
                },
                error: function (result) {

                }
            });

        }
    }
    //database dropdown change event
    jQuery(".download-xml-form .database-select").change(function (evt) {
        evt.preventDefault();
        if (jQuery(".download-xml-form .database-select").val() != null && jQuery(".download-xml-form .group-select").length > 0) {
            var dbid = jQuery(".download-xml-form .database-select").val();
            var select = document.getElementById("DocGrpList_Adg_grup_id");
            setGrpDropDownEmpty(select);
            var selecttype = document.getElementById("DocTypeList_Adt_tp_id");
            setTypeDropDownEmpty(selecttype);
            getGroups(dbid);
        }
    });

    //get groups for dropdown list in cusdec
    function getGroups(dbid)
    {
        jQuery.ajax({
            type: "GET",
            url: "/Cusdec/GetGroupList",
            data:{database:dbid},
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result != null) {
                    if (result.login == true) {
                        var select = document.getElementById("DocGrpList_Adg_grup_id");
                        jQuery(".download-xml-form .group-select").empty();
                        if (result.success == true) {
                            var options = [];
                            var option = document.createElement('option');
                            option.text = "Select Group"
                            option.value = "";
                            options.push(option.outerHTML);
                            if (result.data != null && result.data.length != 0) {
                                for (var i = 0; i < result.data.length; i++) {
                                    option.text = result.data[i].Adg_grup_name;
                                    option.value = result.data[i].Adg_grup_id;
                                    options.push(option.outerHTML);
                                }
                                select.insertAdjacentHTML('beforeEnd', options.join('\n'))
                            } else {
                                if (jQuery(".download-xml-form .database-select").val() != null && jQuery(".download-xml-form .group-select").length > 0) {
                                    //setMessage(result.msg,'error');
                                    showNotice(result.msg);
                                }
                                setGrpDropDownEmpty(select);
                            }
                            

                        } else {
                            setGrpDropDownEmpty(select);
                            if (result.msg.length > 0) {
                                //setMessage(result.msg,'error');
                                showNotice(result.msg);
                            }
                        }
                    } else {
                        Logout();
                    }
                }
            },
            error: function (result) {

            }
        });
    }

    //group dropdown change event
    jQuery(".download-xml-form .group-select").change(function (evt) {
        evt.preventDefault();
        if (jQuery(".download-xml-form .database-select").val() != null && 
            jQuery(".download-xml-form .group-select").val() != null &&
            jQuery(".download-xml-form .type-select").length > 0) {

            var dbid = jQuery(".download-xml-form .database-select").val();
            var grpid = jQuery(".download-xml-form .group-select").val();
            var select = document.getElementById("DocTypeList_Adt_tp_id");
            setTypeDropDownEmpty(select);
            getDocTypes(dbid, grpid);

        }
    });
    //get document types for dropdown list in cusdec
    function getDocTypes(dbid, grpid)
    {
        jQuery.ajax({
            type: "GET",
            url: "/Cusdec/GetTypeList",
            data: { database: dbid,groupid:grpid },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result != null) {
                    if (result.login == true) {
                        var select = document.getElementById("DocTypeList_Adt_tp_id");
                        jQuery(".download-xml-form .type-select").empty();
                        if (result.success == true) {
                            var options = [];
                            var option = document.createElement('option');
                            option.text = "Select Type"
                            option.value = "";
                            options.push(option.outerHTML);
                            if (result.data != null && result.data.length != 0) {
                                for (var i = 0; i < result.data.length; i++) {
                                    option.text = result.data[i].Adt_tp_name;
                                    option.value = result.data[i].Adt_tp_id;
                                    options.push(option.outerHTML);
                                }
                                select.insertAdjacentHTML('beforeEnd', options.join('\n'))
                            } else {
                                if (jQuery(".download-xml-form .database-select").val() != null && jQuery(".download-xml-form .group-select").length > 0) {
                                    //setMessage(result.msg,'error');
                                    showError(result.msg);
                                }
                                setTypeDropDownEmpty(select);
                            }
                            

                        } else {
                            setTypeDropDownEmpty(select);
                            if (result.msg.length > 0) {
                                //setMessage(result.msg,'error');
                                showError(result.msg);
                            }
                        }
                    } else {
                        Logout();
                    }
                }
            },
            error: function (result) {

            }
        });
    }
    //empty the group dropdown 
    function setGrpDropDownEmpty(select) {
        
        jQuery(".download-xml-form .group-select").empty();
        var options = [];
        var option = document.createElement('option');
        option.text = "Select Group";
        option.value = "";
        options.push(option.outerHTML);
        select.insertAdjacentHTML('beforeEnd', options.join('\n'));
    }

    //empty the type dropdown 
    function setTypeDropDownEmpty(select) {
        jQuery(".download-xml-form .type-select").empty();
        var options = [];
        var option = document.createElement('option');
        option.text = "Select Type";
        option.value = "";
        options.push(option.outerHTML);
        select.insertAdjacentHTML('beforeEnd', options.join('\n'));
    }
    //set given errors after login
    function setMessage(msg, type) {
        if (type == 'error') {
            if (jQuery(".navbar-main-page .main-pge-msg").length > 0) {
                jQuery(".navbar-main-page .main-pge-msg .error-msg").empty();
                if (jQuery(".navbar-main-page .main-pge-msg .notice-msg").length > 0) {
                    jQuery(".navbar-main-page .main-pge-msg .notice-msg").empty();
                }
                jQuery(".navbar-main-page .main-pge-msg .error-msg").text(msg);
                jQuery(".navbar-main-page .main-pge-msg").show();
                setTimeout(function () {
                    jQuery(".navbar-main-page .main-pge-msg").slideUp();
                    jQuery(".navbar-main-page .main-pge-msg .error-msg").empty();
                }, 3500);
            }
            else if (type == 'notice') {
                jQuery(".navbar-main-page .main-pge-msg .notice-msg").empty();
                if (jQuery(".navbar-main-page .main-pge-msg .error-msg").length > 0) {
                    jQuery(".navbar-main-page .main-pge-msg .error-msg").empty();
                }
                jQuery(".navbar-main-page .main-pge-msg .notice-msg").text(msg);
                jQuery(".navbar-main-page .main-pge-msg").show();
                setTimeout(function () {
                    jQuery(".navbar-main-page .main-pge-msg").slideUp();
                    jQuery(".navbar-main-page .main-pge-msg .notice-msg").empty();
                }, 3500);
            }
        }
    }

    //genarate button click
    jQuery(".download-xml-form .download-btn").click(function (evt) {
        var form = jQuery(this).closest("form");
        deactiveGenarateButton();
        evt.preventDefault();
        jQuery.ajax({
            cache: false,
            async: true,
            type: "POST",
            url: '/Cusdec/GenarateXml',
            data: form.serialize() + '&request=req',
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        form.submit();
                        if (typeof(result.msg)!="undefined" && result.msg.length > 0) {
                            //setMessage(result.msg, 'notice');
                            showNotice(result.msg);
                        }
                    } else {
                        if (typeof(result.msg)!="undefined" && result.msg.length > 0) {
                            // setMessage(result.msg, 'error');
                            showNotice(result.msg);
                        }
                    }
                    activeGenarateButton();
                } else {
                    Logout();
                }
            }
        })
    });
    //deactivate the genarate button
    function deactiveGenarateButton()
    {
        if (jQuery('.btn-primary.download-btn') != null) {
            jQuery('.btn-primary.download-btn').attr('disabled', true);
        }
    }
    //activate the genarate button
    function activeGenarateButton() {
        if (jQuery('.btn-primary.download-btn') != null) {
            jQuery('.btn-primary.download-btn').attr('disabled', false);
        }
    }
    function Logout() {
        if (alert("Login session has expired!") == true) {
            
        } else {
            window.location.replace("/Login/Login");
        }
    }
    var first = true;
    var first_time = true;
    var headerKeys = Array()
    headerKeys = ["Row", "Document No", "Document Date", "Document Type", "Location Of Gods", "Place of Loading", "Procedure Code"];
    jQuery.ajaxSetup({ cache: false });
    jQuery("div input#Doc_Number").keypress(function (e) {
        if (e.keyCode == 113) {
            var src = jQuery('#DBModelList_Add_db_id').val();
            if (jQuery.trim(src).length != 0) {
                
                if (headerKeys.length > 0 && first_time) {
                    var selecter = jQuery('#myModal .filter-key-cls');
                    selecter.empty();
                    for (i = 1; i < headerKeys.length; i++) {
                        var newOption = jQuery('<option value="' + headerKeys[i] + '">' + headerKeys[i] + '</option>');
                        selecter.append(newOption);
                    }
                    var head = jQuery('#myModalContent .table-responsive .table thead tr');
                    head.empty();
                    for (j = 0; j < headerKeys.length; j++) {
                        var newHead = jQuery('<th>' + headerKeys[j] + '</th>');
                        head.append( newHead);
                    }
                    first_time = false;
                }
                jQuery('div input').css('cursor', 'wait');
                jQuery(".se-pre-con").fadeIn("slow");
                var pgeNum = 1;
                var pgeSize = jQuery('.modal-content .cls-select-page-cont').val();
                var searchFld = jQuery('select.filter-key-cls').val();
                var searchVal = jQuery('input#KeyWord').val();
                loadDetails(src, pgeNum, pgeSize, searchFld, searchVal, headerKeys);
                
            } else {
                // setMessage('Please select db source first.', "error");
                showNotice('Please select db source first.');
            }
            return false;
        }
    });
    jQuery('#myModal').on('shown.bs.modal', function () {
        jQuery("#KeyWord").focus();
        jQuery(".modal-content").draggable({ handle: ".Title.panel-heading", containment: "body" });
        jQuery('.Title.panel-heading').css('cursor','move');
    });
    jQuery(document).keypress(function (evt) {
        if (evt.keyCode == 27) {
                if (jQuery('#myModal:visible').length == 1) {
                    jQuery('#myModal').modal('hide');
                }
        }
    });
    jQuery('#myModal .close-btn').click(function (e) {
        e.preventDefault();
        jQuery('#myModal').modal('hide');
    });
    function setSerchPanel(tableValues, headerKeys) {

        //if (tableValues != null) {
        //    if (tableValues.length > 0) {
                if (jQuery('.table-responsive tbody').length > 0) {
                    jQuery('.table-responsive tbody').empty();
                }
                if (tableValues != null) {
                    if (tableValues.length > 0) {
                        for (i = 0; i < tableValues.length; i++) {
                            var a = "";
                            (i % 2 == 1) ? a = 'class="coloured"' : "";
                            jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                                '</td><td>' + tableValues[i].DOCUMENT_NO +
                                '</td><td>' + tableValues[i].DOCUMENT_DATE +
                                '</td><td>' + tableValues[i].DOCUMENT_TYPE +
                                '</td><td>' + tableValues[i].LOCATION_OF_GOODS +
                                '</td><td>' + tableValues[i].PLACE_OF_LOADING +
                                '</td><td>' + tableValues[i].PROCEDUER_CODE + '</td></tr>');
                        }
                    }
                } else {
                    if (jQuery('.table-responsive tbody').length > 0) {
                        jQuery('.table-responsive tbody').append("<tr><td style=' border:none; color: #ff6666; position: absolute; width: 196px; font-weight: bold;'>No documents found for this search criteria.</td></tr>");
                    }
                }
                jQuery('#myModal').modal({
                    keyboard: false,
                    backdrop: 'static'
                }, 'show');
                jQuery('tr', '#myModal table tbody').click(function () {
                    jQuery('tr', '#myModal table tbody').removeClass('selected');
                    jQuery('tr', '#myModal table tbody').css('color', 'black');
                    jQuery(this).addClass('selected');
                    jQuery(this).css('color', 'red');
                });
                jQuery('tr', '#myModal table tbody').dblclick(function () {
                    jQuery('#Doc_Number').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Doc_Number').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Doc_Number').focus();
                });
          //}
        //} else {
        //    if (jQuery('.table-responsive tbody').length > 0) {
        //        jQuery('.table-responsive tbody').empty();
        //    }
        //    if (jQuery('.table-responsive tbody').length > 0) {
        //        jQuery('.table-responsive tbody').append("<tr><td style=' border:none; color: #ff6666; position: absolute; width: 196px; font-weight: bold;'>No documents found for this search criteria.</td></tr>");
        //    }
        //}
    }
    var first = true;
    var check = false;
    function loadDetails(src, pgeNum, pgeSize, searchFld, searchVal, headerKeys, check) {
            jQuery.ajax({
                cache: false,
                type: "GET",
                url: "/Search/getDocumentDetails",
                data: { dbsrcid: src, pgeNum: pgeNum, pgeSize: pgeSize, searchFld: searchFld, searchVal: searchVal },
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.data.length > 0) {
                                if (first == true) {
                                    paging(result.totalDoc, pgeNum, true);
                                    first = false;
                                }
                                setSerchPanel(result.data, headerKeys);
                                jQuery('#myModal').css('cursor', 'default');
                                jQuery('div input').css('cursor', 'default');
                                jQuery(".se-pre-con").fadeOut("slow");
                                //if (check == true) {
                                    jQuery('#paging').empty();
                                    paging(result.totalDoc, pgeNum, false);
                                    check = false;
                                //}
                            }
                        } else {
                                jQuery('#myModal').css('cursor', 'default');
                                jQuery('div input').css('cursor', 'default');
                                jQuery(".se-pre-con").fadeOut("slow");
                                jQuery('#paging').empty();
                                setSerchPanel(null, headerKeys);
                        }
                    } else {
                        Logout();
                    }
                }
            });
    }
   
    paging = function (total, page, test) {
        jQuery('#paging').bootpag({
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
                jQuery('#myModal').css('cursor', 'wait');
                jQuery(".se-pre-con").fadeIn("slow");
                var searchFld = jQuery('select.filter-key-cls').val();
                var searchVal = jQuery('input#KeyWord').val();
                var src = jQuery('#DBModelList_Add_db_id').val();
                var pgeSize = jQuery('.modal-content .cls-select-page-cont').val();
                loadDetails(src, num, pgeSize, searchFld, searchVal, headerKeys);
            }
        });
       
    }

    jQuery('.modal-content .cls-select-page-cont').change(function (e) {
        jQuery('#myModal').css('cursor', 'wait');
        jQuery(".se-pre-con").fadeIn("slow");
        var searchFld = jQuery('select.filter-key-cls').val();
        var searchVal = jQuery('input#KeyWord').val();
        var src = jQuery('#DBModelList_Add_db_id').val();
        var pgeSize = jQuery('.modal-content .cls-select-page-cont').val();
        var pgeNum = 1;
        loadDetails(src, pgeNum, pgeSize, searchFld, searchVal, headerKeys,true);
   });
    jQuery(document).on("keypress", function (e) {
       if (e.keyCode == 13) {
           if (jQuery('.modal-content #KeyWord').is(':focus')==true) {
               jQuery('#myModal').css('cursor', 'wait');
               jQuery(".se-pre-con").fadeIn("slow");
               var searchFld = jQuery('select.filter-key-cls').val();
               var searchVal = jQuery('input#KeyWord').val();
               var src = jQuery('#DBModelList_Add_db_id').val();
               var pgeSize = jQuery('.modal-content .cls-select-page-cont').val();
               var pgeNum = 1;
               loadDetails(src, pgeNum, pgeSize, searchFld, searchVal, headerKeys, true);
           } else {
               if (jQuery("#myModal").is(":visible") == true) {
                   if (jQuery('tr.selected td', '#myModal table tbody').length > 0) {
                       jQuery('#Doc_Number').val("");
                       var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                       jQuery('#Doc_Number').val(value);
                       jQuery('#myModal').modal('hide');
                       jQuery("#KeyWord").val("");
                       jQuery('#Doc_Number').focus();
                   }
                   
               }
           }
          
       } else if (e.keyCode == 40) {
           if (jQuery('.modal-content #KeyWord').is(':focus')==true) {
               var aa = jQuery('#myModal table tbody tr');
               if (typeof aa[0] != "undefined") {
                   jQuery('.modal-content input#KeyWord').focusout();
                   jQuery('tr', '#myModal table tbody').removeClass('selected');
                   jQuery('tr', '#myModal table tbody').css('color', 'black');
                   jQuery(aa[0]).addClass('selected');
                   jQuery(aa[0]).css('color', 'red');
               }
           }
       }
       if (jQuery("#myModal").is(":visible") == true) {
           if (jQuery('tr td', '#myModal table tbody').length > 0) {
               if (e.keyCode == 40) {
                   if (jQuery('.modal-content #KeyWord').is(':focus') == false) {
                       var aa = jQuery('#myModal table tbody tr.selected').next();
                       if (typeof aa[0] != "undefined") {
                           jQuery('tr', '#myModal table tbody').removeClass('selected');
                           jQuery('tr', '#myModal table tbody').css('color', 'black');
                           jQuery(aa[0]).addClass('selected');
                           jQuery(aa[0]).css('color', 'red');
                       }
                   }
               }
               else if (e.keyCode == 38) {
                   if (jQuery('.modal-content #KeyWord').is(':focus') == false) {
                       var bb = jQuery('#myModal table tbody tr.selected').prev();
                       if (typeof bb[0] != "undefined") {
                           jQuery('tr', '#myModal table tbody').removeClass('selected');
                           jQuery('tr', '#myModal table tbody').css('color', 'black');
                           jQuery(bb[0]).addClass('selected');
                           jQuery(bb[0]).css('color', 'red');
                       }
                   }
                   
               }
           }
       }
    });

    jQuery('.filter-key-cls').change(function (evt) {
        evt.preventDefault();
        jQuery('#KeyWord').val("");
        if (jQuery(this).val() == 'Document Date') {
            jQuery('#KeyWord').attr('readonly', 'true');
            jQuery('#KeyWord').datepicker({maxDate: new Date() });
        } else {
            jQuery('#KeyWord').removeAttr("readonly");
            jQuery('#KeyWord').datepicker("destroy");
        }
        jQuery('#KeyWord').focus();
        
    });
    
/*end message popup js*/
});

function showNotice(msg) {
    jQuery().toastmessage('showToast', {
        text: msg,
        sticky: false,
        inEffectDuration: 1000,
        stayTime: 5000,
        preventDuplicates: true,
        position: 'top-center',
        type: 'notice',
        closeText: '',
        close: function () {
        }
    });
}
function showWarning(msg) {
    jQuery().toastmessage('showToast', {
        text: msg,
        sticky: false,
        inEffectDuration: 1000,
        stayTime: 5000,
        preventDuplicates: true,
        position: 'top-center',
        type: 'warning',
        closeText: '',
        close: function () {
        }
    });
}

function showError(msg) {
    jQuery().toastmessage('showToast', {
        text: msg,
        sticky: true,
        inEffectDuration: 1000,
        stayTime: 5000,
        preventDuplicates: true,
        position: 'top-center',
        type: 'error',
        closeText: '',
        close: function () {
        }
    });
}
function showSuccess(msg) {
    jQuery().toastmessage('showToast', {
        text: msg,
        sticky: false,
        inEffectDuration: 1000,
        stayTime: 5000,
        preventDuplicates: true,
        position: 'top-center',
        type: 'success',
        closeText: '',
        close: function () {
        }
    });
}