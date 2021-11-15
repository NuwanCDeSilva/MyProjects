jQuery(document).ready(function () {
    jQuery('#date').val(my_date_format_with_time(new Date()));
    jQuery('#date').datepicker({ dateFormat: "dd/M/yy" });
    jQuery('#fromdt').val(my_date_format_with_time(new Date()));
    jQuery('#fromdt').datepicker({ dateFormat: "dd/M/yy" })

    jQuery(".channel-search").click(function (evt) {
        var headerKeys = Array();
        headerKeys = ["Row", "Code", "Description"];
        field = "srchChnl";
        var data = {
            Company: $('input[name="selectedcompany"]:checked').val(),
            type: "channel"
        }
        var x = new CommonSearch(headerKeys, field, data);
    });
    $('#chanel').focusout(function () {
        var chanal = $(this).val();
        var com = "";
        if (jQuery('[name="selectedcompany"]:checked').length == 0) {
           var com = "ABL";
        } else {
           var com = $('input[name="selectedcompany"]:checked').val();
        }
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/ProductBonusDefinition/ChanalelTextChange",
            data: { Chanal: chanal, Com: com },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.data.length > 0) {
                        }
                    } else {
                        setInfoMsg('Invalid Chanal Code!!');
                        jQuery("#chanel").val("");
                        return;
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    jQuery(".schannel-search").click(function (evt) {
        var headerKeys = Array();
        headerKeys = ["Row", "Code", "Description"];
        field = "srchSubChnlM";
        var data = {
            chnl: "",
            type: "sub_channel",
            Company: $('input[name="selectedcompany"]:checked').val()
        }
        var x = new CommonSearch(headerKeys, field, data);
    });
    $('#schannel').focusout(function () {
        var schanal = $(this).val();
        if (jQuery('[name="selectedcompany"]:checked').length == 0) {
            var com = "ABL";
        } else {
            var com = $('input[name="selectedcompany"]:checked').val();
        }
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/ProductBonusDefinition/SubChanalelTextChange",
            data: { SChanal: schanal, Com:com },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.data.length > 0) {
                        }
                    } else {
                        setInfoMsg('Invalid Sub Chanal Code!!');
                        jQuery("#schannel").val("");
                        return;
                    }
                } else {
                    Logout();
                }
            }
        });
    });

    jQuery(".region-search").click(function (evt) {
        var headerKeys = Array();
        headerKeys = ["Row", "Code", "Description"];
        field = "srchRegionM";
        var data = {
            chnl: "",
            sChnl: "",
            area: "",
            type: "region",
            Company: $('input[name="selectedcompany"]:checked').val()
        };
        var x = new CommonSearch(headerKeys, field, data);
    });
    $('#region').focusout(function () {
        var region = $(this).val();
        if (jQuery('[name="selectedcompany"]:checked').length == 0) {
            var com = "ABL";
        } else {
            var com = $('input[name="selectedcompany"]:checked').val();
        }
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/ProductBonusDefinition/RegionTextChange",
            data: { Region: region, Com:com },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.data.length > 0) {
                        }
                    } else {
                        setInfoMsg('Invalid Region Code!!');
                        jQuery("#region").val("");
                        return;
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    jQuery(".zone-search").click(function (evt) {
        var headerKeys = Array();
        headerKeys = ["Row", "Code", "Description"];
        field = "srchZoneM";
        var data = {
            chnl: "",
            sChnl: "",
            area: "",
            regn: "",
            type: "zone",
            Company: $('input[name="selectedcompany"]:checked').val()
        };
        var x = new CommonSearch(headerKeys, field, data);
    });
    $('#zone').focusout(function () {
        var zone = $(this).val();
        if (jQuery('[name="selectedcompany"]:checked').length == 0) {
            var com = "ABL";
        } else {
            var com = $('input[name="selectedcompany"]:checked').val();
        }
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/ProductBonusDefinition/ZoneTextChange",
            data: { Zone: zone, Com:com },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.data.length > 0) {
                        }
                    } else {
                        setInfoMsg('Invalid Zone Code!!');
                        jQuery("#zone").val("");
                        return;
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    jQuery(".pc-search").click(function (evt) {
        var headerKeys = Array();
        headerKeys = ["Row", "Code", "Description"];
        field = "srchPCM";
        var data = {
            chnl: "",
            sChnl: "",
            area: "",
            regn: "",
            zone: "",
            type: "PC",
            Company: $('input[name="selectedcompany"]:checked').val()
        };
        var x = new CommonSearch(headerKeys, field, data);
    });
    $('#pc').focusout(function () {
        var pc = $(this).val();
        if (jQuery('[name="selectedcompany"]:checked').length == 0) {
            var com = "ABL";
        } else {
            var com = $('input[name="selectedcompany"]:checked').val();
        }
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/ProductBonusDefinition/PCTextChange",
            data: { PC: pc, Com:com },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.data.length > 0) {
                        }
                    } else {
                        setInfoMsg('Invalid PC Code!!');
                        jQuery("#pc").val("");
                        return;
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    jQuery(".itemcode-search").click(function (evt) {
        var headerKeys = Array();
        headerKeys = ["Row", "Code", "Description"];
        field = "srchItemsM";
        var data = {
            chnl: jQuery("#ItemCode").val(),
            type: "sub_channel"
        }
        var x = new CommonSearch(headerKeys, field, data);
    });
    $('#itemcode').focusout(function () {
        var pc = $(this).val();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/Search/getItems",
            data: { pgeNum: "1", pgeSize: "10", searchFld: "Code", searchVal: pc },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.data.length > 0) {
                        }
                    } else {
                        setInfoMsg('Invalid Item Code!!');
                        jQuery("#itemcode").val("");
                        jQuery("#itemcode").focus();
                        return;
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    jQuery(".model-search").click(function (evt) {
        var headerKeys = Array();
        headerKeys = ["Row", "Code", "Description"];
        field = "srchItemModelM";
        var x = new CommonSearch(headerKeys, field);
    });
    $('#model').focusout(function () {
        var pc = $(this).val();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/Search/getItemModel",
            data: { pgeNum: "1", pgeSize: "10", searchFld: "Code", searchVal: pc },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.data.length > 0) {
                        }
                    } else {
                        setInfoMsg('Invalid Model Code!!');
                        jQuery("#model").val("");
                        jQuery("#model").focus();
                        return;
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    jQuery(".brand-search").click(function (evt) {
        var headerKeys = Array();
        headerKeys = ["Row", "Code", "Description"];
        var data = jQuery("#BrandMngr").val();
        field = "srchItemBrandM";
        var resData = new CommonSearch(headerKeys, field, data);
    });
    $('#brand').focusout(function () {
        var pc = $(this).val();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/Search/getItemBrands",
            data: { pgeNum: "1", pgeSize: "10", searchFld: "Code", searchVal: pc },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.data.length > 0) {
                        }
                    } else {
                        setInfoMsg('Invalid Brand Code!!');
                        jQuery("#brand").val("");
                        jQuery("#brand").focus();
                        return;
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    jQuery(".cat1-search").click(function (evt) {
        var headerKeys = Array();
        headerKeys = ["Row", "Code", "Description"];
        field = "srchMainCatM";
        var x = new CommonSearch(headerKeys, field);
    });
    $('#cat1').focusout(function () {
        var pc = $(this).val();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/Search/getMainCategory",
            data: { pgeNum: "1", pgeSize: "10", searchFld: "Code", searchVal: pc },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.data.length > 0) {
                        }
                    } else {
                        setInfoMsg('Invalid Cat 1 Code!!');
                        jQuery("#cat1").val("");
                        jQuery("#cat1").focus();
                        return;
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    jQuery(".cat2-search").click(function (evt) {
        var headerKeys = Array();
        headerKeys = ["Row", "Code", "Description"];
        field = "srchCategory2M";
        var data = {
            chnl: jQuery("#SaleType").val(),
            sChnl: jQuery("#ItemCode").val(),
            type: "Category2"
        };
        var x = new CommonSearch(headerKeys, field, data);
    });
    $('#cat2').focusout(function () {
        var pc = $(this).val();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/Search/getCategory2",
            data: { pgeNum: "1", pgeSize: "10", searchFld: "Code", searchVal: pc },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.data.length > 0) {
                        }
                    } else {
                        setInfoMsg('Invalid Cat 2 Code!!');
                        jQuery("#cat2").val("");
                        jQuery("#cat2").focus();
                        return;
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    function LoadPriceBook() {
        jQuery.ajax({
            type: "GET",
            url: "/ProductBonusDefinition/LoadPriceBook",
            data: { invType: jQuery("form.frm-inv-det #Sah_inv_tp").val() },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("PriceBook");
                        jQuery("#PriceBook").empty();
                        var options = [];
                        var option = document.createElement('option');
                        if (result.data != null && result.data.length != 0) {
                            for (i = 0; i < result.data.length; i++) {
                                option.text = result.data[i].Text;
                                option.value = result.data[i].Value;
                                options.push(option.outerHTML);
                            }
                        } else {
                            option.text = "Select Type";
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
        })
    }
    //  LoadPriceBook();
    function LoadPriceLevel() {
        jQuery.ajax({
            type: "GET",
            url: "/ProductBonusDefinition/LoadPriceLevel",
            data: { invType: jQuery("form.frm-inv-det #Sah_inv_tp").val(), book: jQuery("#PriceBook").val() },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("PriceLevel");
                        jQuery("#PriceLevel").empty();
                        var options = [];
                        var option = document.createElement('option');
                        if (result.data != null && result.data.length != 0) {
                            for (i = 0; i < result.data.length; i++) {
                                option.text = result.data[i].Text;
                                option.value = result.data[i].Value;
                                options.push(option.outerHTML);
                            }
                        } else {
                            option.text = "Select Type";
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
        })
    }
    LoadPriceBook();

    $("#PriceBook").change(function () {
        jQuery('#PricelevelPopup').modal({
            keyboard: false,
            backdrop: 'static'
        }, 'show');

            jQuery.ajax({
                type: "GET",
                url: "/ProductBonusDefinition/LoadPriceLevel",
                data: { invType: jQuery("form.frm-inv-det #Sah_inv_tp").val(), book: jQuery("#PriceBook").val() },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success = true)
                        {
                            if (result.data != null && result.data.length != 0) {
                                setPricelevelbox(result.data);
                            }
                        }
                    }
                }
            });

    });
    //dilshan*********************
    $("#paymodesub").change(function () {
        jQuery('#SubTPPopup').modal({
            keyboard: false,
            backdrop: 'static'
        }, 'show');

        jQuery.ajax({
            type: "GET",
            url: "/ProductBonusDefinition/LoadSSubType",
            //data: { invType: jQuery("form.frm-inv-det #Sah_inv_tp").val(), book: jQuery("#PriceBook").val() },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        if (result.data != null && result.data.length != 0) {
                            setSubTypebox(result.data);
                        }
                    }
                }
            }
        });

    });
    function setSubTypebox(data) {
        jQuery('.subtype-box .new-row').remove();
        for (i = 0; i < data.length; i++) {
            jQuery('.subtype-box').append('<tr class="new-row">' +
                  '<td>' + data[i].Value + '</td>' +
                   '<td style="text-align:center;">  <input type="checkbox" id="chkst" name="chkst"  class="form-control" value="' + data[i].Value + '"></td>' +
                    '</tr>');
        }
    }
    //************************
    function setPricelevelbox(data)
    {
        jQuery('.pricelevel-box .new-row').remove();
        for (i = 0; i < data.length; i++) {
            jQuery('.pricelevel-box').append('<tr class="new-row">' +
                  '<td>' + data[i].Value + '</td>' +
                   '<td style="text-align:center;">  <input type="checkbox" id="chkpl" name="chkpl"  class="form-control" value="' + data[i].Value + '"></td>' +
                    '</tr>');
        }
    }

    //  LoadPriceLevel();
    //$('#PriceBook').click(function () {
    //    LoadPriceBook();
    //});
    //$('#PriceBook').keypress(function (e) {
    //    if (e.which == 13) {
    //        LoadPriceBook();
    //    }
       
    //});
    $('#PriceLevel').focus(function () {
        LoadPriceLevel();
    });
    LoadSlabBase();
    function LoadPayType() {
        jQuery.ajax({
            type: "GET",
            url: "/ProductBonusDefinition/LoadPayType",
            data: {},
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("paymode");
                        jQuery("#paymode").empty();
                        var options = [];
                        var option = document.createElement('option');
                        if (result.data != null && result.data.length != 0) {
                            for (i = 0; i < result.data.length; i++) {
                                option.text = result.data[i].Text;
                                option.value = result.data[i].Value;
                                options.push(option.outerHTML);
                            }
                        } else {
                            option.text = "Select Type";
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
        })
    }
    LoadTargetBase();
    function LoadSlabBase() {
        jQuery.ajax({
            type: "GET",
            url: "/ProductBonusDefinition/LoadSlabBase",
            data: {},
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("slabbaseon");
                        jQuery("#slabbaseon").empty();
                        var options = [];
                        var option = document.createElement('option');
                        if (result.data != null && result.data.length != 0) {
                            for (i = 0; i < result.data.length; i++) {
                                option.text = result.data[i].Text;
                                option.value = result.data[i].Value;
                                options.push(option.outerHTML);
                            }
                        } else {
                            option.text = "Select Type";
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
        })
    }
    function LoadTargetBase() {
        jQuery.ajax({
            type: "GET",
            url: "/ProductBonusDefinition/LoadTargetBase",
            data: {},
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("targetbase");
                        jQuery("#targetbase").empty();
                        var options = [];
                        var option = document.createElement('option');
                        if (result.data != null && result.data.length != 0) {
                            for (i = 0; i < result.data.length; i++) {
                                option.text = result.data[i].Text;
                                option.value = result.data[i].Value;
                                options.push(option.outerHTML);
                            }
                        } else {
                            option.text = "Select Type";
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
        })
    }
    $('#paymode').click(function () {
        LoadPayType();
    });
    $('#paymode').keypress(function (e) {
        if (e.which == 13) {
            LoadPayType();
        }
       
    });
    LoadSalesSubType();
    function LoadSalesSubType() {
        jQuery.ajax({
            type: "GET",
            url: "/ProductBonusDefinition/LoadSalesSubType",
            data: {},
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("paymodesub");
                        jQuery("#paymodesub").empty();
                        var options = [];
                        var option = document.createElement('option');
                        if (result.data != null && result.data.length != 0) {
                            for (i = 0; i < result.data.length; i++) {
                                option.text = result.data[i].Text;
                                option.value = result.data[i].Value;
                                options.push(option.outerHTML);
                            }
                        } else {
                            option.text = "Select Type";
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
        })
    }
    jQuery(".salestype-search").click(function (evt) {
        var headerKeys = Array();
        headerKeys = ["Row", "Code", "Description"];
        field = "InvType";
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".pricecircular-search").click(function (evt) {
        var headerKeys = Array();
        headerKeys = ["Row", "Code"];
        field = "PriceCircula";
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".custormer-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Name", "Nic", "Mobile", "Br No"];
        field = "CusCode"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".add-bonus-loc").click(function () {
        var chnl = jQuery("#chanel").val().toUpperCase();
        var schnl = jQuery("#schannel").val().toUpperCase();
        var region = jQuery("#region").val().toUpperCase();
        var zone = jQuery("#zone").val().toUpperCase();
        var pc = jQuery("#pc").val().toUpperCase();
        var com = "";
        if (jQuery('[name="selectedcompany"]:checked').length == 0) {
            com = "ABL";
        } else {
            com=  $('input[name="selectedcompany"]:checked').val();
        }

        if (chnl == "" && schnl == "" && region == "" && zone == "" && pc == "") {
            setInfoMsg('Please Enter One of loc details');
            return;
        }
        else {
            var val = 0;
            if (chnl != "")
            {
                val = val + 1;
            }
            if (schnl != "") {
                val = val + 1;
            }
            if (region != "") {
                val = val + 1;
            }
            if (zone != "") {
                val = val + 1;
            }
            if (pc != "") {
                val = val + 1;
            }
            if (val > 1) {
                setInfoMsg('Cant Select more than One  loc details');
                return;
            } else {
                //if (chnl != "") {
                //    $("#schannel").prop("disabled", true);
                //    $("#region").prop("disabled", true);
                //    $("#zone").prop("disabled", true);
                //    $("#pc").prop("disabled", true);

                //    $(".schannel-search").hide();
                //    $(".region-search").hide();
                //    $(".zone-search").hide();
                //    $(".pc-search").hide();
                //}
                //if (schnl != "") {
                //    $("#chanel").prop("disabled", true);
                //    $("#region").prop("disabled", true);
                //    $("#zone").prop("disabled", true);
                //    $("#pc").prop("disabled", true);

                //    $(".schannel-search").hide();
                //    $(".region-search").hide();
                //    $(".zone-search").hide();
                //    $(".pc-search").hide();
                //}
               
            }
            jQuery.ajax({
                cache: false,
                type: "GET",
                url: "/ProductBonusDefinition/AddLocationDetails",
                data: { Channel: chnl, Schannel: schnl, Region: region, Zone: zone, PC: pc, Company: com },
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.data.length > 0) {
                                //set grid
                                SetLocationDetails(result.data);
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
    function SetLocationDetails(data) {
        jQuery('.bonus-loc-table .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {
                jQuery('.bonus-loc-table').append('<tr class="new-row">' +
                    
                      '<td>' + data[i].Rbl_anal1 + '</td>' +
                        '<td>' + data[i].Rbl_chnl + '</td>' +
                          '<td>' + data[i].Rbl_sub_chnl + '</td>' +
                           '<td>' + data[i].Rbl_region + '</td>' +
                         '<td>' + data[i].Rbl_zone + '</td>' +
                            '<td>' + data[i].Rbl_pc + '</td>' +
                       '<td style="text-align:center;"><img class="delete-img remove-loc-det" src="../Resources/images/Remove.png"></td>' +
                        '</tr>');
            }
            RemoveLocationFunction();
        }
    }
    function RemoveLocationFunction() {
        jQuery(".remove-loc-det").unbind('click').click(function (evt) {
            evt.preventDefault();
            var td = jQuery(this).parent('td');
            var tr = jQuery(td).parent('tr');
            var Company = jQuery(tr).find('td:eq(0)').html();
            var channel = jQuery(tr).find('td:eq(1)').html();
            var schannel = jQuery(tr).find('td:eq(2)').html();
            var region = jQuery(tr).find('td:eq(3)').html();
            var zone = jQuery(tr).find('td:eq(4)').html();
            var pc = jQuery(tr).find('td:eq(5)').html();
            Lobibox.confirm({
                msg: "Do you want to remove ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/ProductBonusDefinition/RemoveLocDet",
                            data: { Chnl: channel, Schnl: schannel, Region: region, Zone: zone, PC: pc, Company: Company },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        SetLocationDetails(result.data);
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
    }
    jQuery(".add-bonus-item").click(function () {
        var itemcode = jQuery("#itemcode").val();
        var cat1 = jQuery("#cat1").val();
        var cat2 = jQuery("#cat2").val();
        var brand = jQuery("#brand").val();
        var model = jQuery("#model").val();
        var PriceBook = jQuery("#PriceBook").val();
        var PriceLevel = jQuery("#PriceLevel").val();
        var pricecircular = jQuery("#pricecircular").val();
        var custormer = jQuery("#custormer").val();
        var hpscheme = jQuery("#hpscheme").val();
        var invtype = jQuery("#invtype").val();
        var paymode = jQuery("#paymode").val();
        var paymodesub = jQuery("#paymodesub").val();
        var slabbaseon = jQuery("#slabbaseon").val();
        var targetbase = jQuery("#targetbase").val();
        var fromval = jQuery("#fromval").val();
        var toval = jQuery("#toval").val();
        var marks = jQuery("#marks").val();
        if ($("#chkmulty").is(':checked') == true) {
            var malty = "1";
        } else {
            var malty = "0";
        }
        var CombineNo = jQuery("#combineno").val();
        var TotCombQty = jQuery("#totcombqty").val();

        var selectedPL = $('input[name="chkpl"]:checked').serialize();
        var selectedST = $('input[name="chkst"]:checked').serialize();

        if (itemcode == "" && cat1 == "" && cat2 == "" && brand == "" && model == "" && PriceBook == "" && PriceLevel == "" && pricecircular == "" && invtype == "" && paymode == "" && paymodesub=="" && slabbaseon=="") {
            setInfoMsg('Please Enter One of Item details');
        } else if (marks=="") {
            setInfoMsg('Please Enter Marks');
        } else if (fromval == "" && toval=="")
        {
            setInfoMsg('Please Enter From Val and To Val');
        }
        else {
            jQuery.ajax({
                cache: false,
                type: "GET",
                url: "/ProductBonusDefinition/AddItemDetails",
                data: { itemcode: itemcode, cat1: cat1, cat2: cat2, brand: brand, model: model, PriceBook: PriceBook, PriceLevel: PriceLevel, pricecircular: pricecircular, invtype: invtype, paymode: paymode, paymodesub: paymodesub, slabbaseon: slabbaseon, custormer: custormer, hpscheme: hpscheme, fromval: fromval, toval: toval, marks: marks, malty: malty, targetbase: targetbase, CombineNo: CombineNo, TotCombQty: TotCombQty, PLALL: selectedPL, STALL: selectedST },
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.data.length > 0) {
                                //set grid
                                SetItemDetails(result.data);
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
    function SetItemDetails(data) {
        jQuery('.bonus-item-table .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {
                jQuery('.bonus-item-table').append('<tr class="new-row">' +
                        '<td>' + data[i].Rbd_item_cd + '</td>' +
                          '<td>' + data[i].Rbd_cat1 + '</td>' +
                           '<td>' + data[i].Rbd_cat2 + '</td>' +
                         '<td>' + data[i].Rdb_brand + '</td>' +
                            '<td>' + data[i].Rbd_model + '</td>' +
                             '<td>' + data[i].Rbd_pb + '</td>' +
                          '<td>' + data[i].Rbd_pl + '</td>' +
                           '<td>' + data[i].Rbd_price_circul + '</td>' +
                         '<td>' + data[i].Rbd_cus_cd + '</td>' +
                            '<td>' + data[i].Rbd_hp_schm + '</td>' +
                              '<td>' + data[i].Rbd_sales_tp + '</td>' +
                          '<td>' + data[i].Rbd_pay_mode + '</td>' +
                          '<td>' + data[i].Rbd_pay_sub_tp + '</td>' +
                           '<td>' + data[i].Rdb_anal2 + '</td>' +
                           '<td>' + data[i].Rbd_slab_base + '</td>' +
                         '<td>' + data[i].Rbd_from_val + '</td>' +
                            '<td>' + data[i].Rdb_to_val + '</td>' +
                            '<td>' + data[i].Rdb_marks + '</td>' +
                             '<td>' + data[i].Rdb_anal3 + '</td>' +
                       '<td style="text-align:center;"><img class="delete-img remove-item-det" src="../Resources/images/Remove.png"></td>' +
                        '</tr>');
            }
            RemoveItemDet();
        }
    }
    function RemoveItemDet() {
        jQuery(".remove-item-det").unbind('click').click(function (evt) {
            evt.preventDefault();
            var td = jQuery(this).parent('td');
            var tr = jQuery(td).parent('tr');
            var itemcode = jQuery(tr).find('td:eq(0)').html();
            var cat1 = jQuery(tr).find('td:eq(1)').html();
            var cat2 = jQuery(tr).find('td:eq(2)').html();
            var brand = jQuery(tr).find('td:eq(3)').html();
            var model = jQuery(tr).find('td:eq(4)').html();
            var PriceBook = jQuery(tr).find('td:eq(5)').html();
            var PriceLevel = jQuery(tr).find('td:eq(6)').html();
            var pricecircular = jQuery(tr).find('td:eq(7)').html();
            var custormer = jQuery(tr).find('td:eq(8)').html();
            var hpscheme = jQuery(tr).find('td:eq(9)').html();
            var invtype = jQuery(tr).find('td:eq(10)').html();
            var paymode = jQuery(tr).find('td:eq(11)').html();
            var paymodesub = jQuery(tr).find('td:eq(12)').html();
            var TargBase = jQuery(tr).find('td:eq(13)').html();
            var slabbaseon = jQuery(tr).find('td:eq(14)').html();
            var fromval = jQuery(tr).find('td:eq(15)').html();
            var toval = jQuery(tr).find('td:eq(16)').html();
            var marks = jQuery(tr).find('td:eq(17)').html();
            Lobibox.confirm({
                msg: "Do you want to remove ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/ProductBonusDefinition/RemoveItemDet",
                            data: { itemcode: itemcode, cat1: cat1, cat2: cat2, brand: brand, model: model, PriceBook: PriceBook, PriceLevel: PriceLevel, pricecircular: pricecircular, invtype: invtype, paymode: paymode, paymodesub: paymodesub, slabbaseon: slabbaseon, custormer: custormer, hpscheme: hpscheme, fromval: fromval, toval: toval, marks: marks },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        SetItemDetails(result.data);
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
    }
    jQuery(".btn-bonus-save-data").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to save ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    var date = jQuery("#date").val();
                    var validfrom = jQuery("#fromdt").val();
                    var fsalelimit = jQuery("#fwdsalelimit").val();
                    var areas = jQuery("#areaslimit").val();
                    var outlimit = jQuery("#outstanlimit").val();
                    var outdatelimit = jQuery("#outstandtlimit").val();
                    if ($("#radqtybase").is(':checked') == true) {
                        var calctype = "QTY";
                    }
                    if ($("#radvalbase").is(':checked') == true) {
                        var calctype = "VALUE";
                    }
                    if ($("#radgpbase").is(':checked') == true) {
                        var calctype = "GP";
                    }
                    if ($("#radinvoice").is(':checked') == true) {
                        var salesmethod = "INV";
                    }
                    if ($("#radeliverd").is(':checked') == true) {
                        var salesmethod = "DELI";
                    }
                    if ($("#radwithreversal").is(':checked') == true) {
                        var salesmethod = "REV";
                    }
                    if ($("#active").is(':checked') == true) {
                        var active = "1";
                    }
                    var doc = jQuery("#bonuscode").val();
                    var circ_cd = jQuery("#manualcirno").val();
                    if ($("#chkcomitm").is(':checked') == true) {
                        var iscommitem = "1";
                    } else {
                        var iscommitem = "0";
                    }
                    var maxdiscount = jQuery("#maxdiscrate").val();
                    if ($("#withpbpl").is(':checked') == true) {
                        var withpb = "1";
                    } else {
                        var withpb = "0";
                    }
                    if ($("#withprom").is(':checked') == true) {
                        var withpromo = "1";
                    } else {
                        var withpromo = "0";
                    }
                    var combinetot = jQuery("#totcommqty").val();
                    var remarks = jQuery("#remark").val();

                    if ($("#isrereportall").is(':checked') == true) {
                        var Option = "A";
                    }
                    if ($("#isrereport").is(':checked') == true) {
                        var Option = "M";
                    }
                    if ($("#norereport").is(':checked') == true) {
                        var Option = "N";
                    }

                    jQuery.ajax({
                        cache: false,
                        type: "GET",
                        url: "/ProductBonusDefinition/SaveProductBonusDefinition",
                        data: {
                            Date: date, Fsalelimit: fsalelimit, Areas: areas, Outslimit: outlimit, Outdatelimit: outdatelimit,
                            CalcType: calctype, SalesMethod: salesmethod, Active: active, doc: doc, circ_cd: circ_cd,
                            iscommitem: iscommitem, combinetot: combinetot, maxdiscount: maxdiscount, withpb: withpb,
                            remarks: remarks, withpromo: withpromo, validfrom: validfrom, Option: Option
                        },
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    setSuccesssMsg(result.msg);
                                    clearAll();
                                } else {
                                    setError(result.msg);
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
    function clearAll() {
        jQuery('.bonus-loc-table .new-row').remove();
        jQuery('.bonus-item-table .new-row').remove();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/ProductBonusDefinition/ClearAll",
            data: {},
            success: function (result) {
                if (result.login == true) {

                } else {
                    Logout();
                }
            }
        });
    }
    jQuery(".btn-clear-data").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to clear data ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    window.location.href = "/ProductBonusDefinition";
                }
            }
        });

    });
    jQuery(".bonus-search").click(function (evt) {
        var headerKeys = Array();
        headerKeys = ["Row", "Code", "Circ No"];
        field = "BonusCode";
        var x = new CommonSearch(headerKeys, field);
    });
    $('#bonuscode').focusout(function () {
        var pc = $(this).val();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/ProductBonusDefinition/LoadDetails",
            data: { BonusCode: jQuery("#bonuscode").val() },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.hdrdata.length > 0) {
                            // set hdr field
                            SetHdrData(result.hdrdata);
                            
                            //set item data
                            if (result.itemdetails.length > 0) {
                                SetItemDetails(result.itemdetails);
                            }
                            if (result.Locdetails.length > 0) {
                                SetLocationDetails(result.Locdetails);
                            }
                        }
                    } else {
                        setInfoMsg('No Data Found! Please Check Bonus Code!!');
                        return;
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    function SetHdrData(data) {
        jQuery("#date").val(getFormatedDate1(data[0].Rbh_date));
        jQuery("#fromdt").val(getFormatedDate1(data[0].Rbh_valid_from));
        jQuery("#fwdsalelimit").val(data[0].Rbh_fw_sale_lmt);
        jQuery("#areaslimit").val(data[0].Rbh_areas_lmt);
        jQuery("#outstanlimit").val(data[0].Rbh_outs_lmt);
        jQuery("#outstandtlimit").val(data[0].Rbh_outs_dt_lmt);
        jQuery("#maxdiscrate").val(data[0].Rbh_disc_con);
        if (data[0].Rbh_calc_methd == "QTY") {
            $("#radqtybase").attr('checked', true);
        } else if (data[0].Rbh_calc_methd == "GP") {
            $("#radgpbase").attr('checked', true);
        } else {
            $("#radqtybase").attr('checked', true);
        }

        if (data[0].Rbh_sales_methd == "DELI") {
            $("#radeliverd").attr('checked', true);
        } else if (data[0].Rbh_sales_methd == "REV") {
            $("#radwithreversal").attr('checked', true);
        } else {
            $("#radinvoice").attr('checked', true);
        }
        if (data[0].Rbh_act == "1") {
            $("#active").attr('checked', true);
        } else {
            $("#active").attr('checked', false);
        }

        // Added by Udesh 12-Oct-2018
        if (data[0].Rbh_anal2 == "1") {
            $("#chkcomitm").attr('checked', true);
        } else {
            $("#chkcomitm").attr('checked', false);
        }

        if (data[0].Rbh_pb_cond == "1") {
            $("#withpbpl").attr('checked', true);
        } else {
            $("#withoutpbpl").attr('checked', true);
        }
        if (data[0].Rbh_anal5 == "1") {
            $("#withprom").attr('checked', true);
        } else {
            $("#withoutprom").attr('checked', true);
        }
        $("#norereport").attr('checked', false);
        if (data[0].Rbh_rerept_opt == "A") {
            $("#isrereportall").attr('checked', true);
            $("#isrereportall").prop("checked", true);
        }
        if (data[0].Rbh_rerept_opt == "M") {
            $("#isrereport").attr('checked', true);
            $("#isrereport").prop("checked", true);
        }
        if (data[0].Rbh_rerept_opt == "N") {
            $("#norereport").attr('checked', true);
            $("#norereport").prop("checked", true);
        }
        jQuery("#manualcirno").val(data[0].Rbh_anal1);
        jQuery("#remark").val(data[0].Rbh_anal4);
    }
    function getFormatedDate1(date) {
        var dte = new Date(parseInt(date.substr(6)));
        if (my_date_format_tran(dte) != "NaN/undefined/NaN")
            return my_date_format_tran(dte);
    }
    loadCompany();
    function loadCompany() {
        jQuery.ajax({
            type: "GET",
            url: "/Search/loadCompany",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        jQuery('.compny-display-tbl .new-row').remove();
                        if (result.companyList.length > 0) {
                            for (i = 0; i < result.companyList.length; i++) {
                                jQuery('.compny-display-tbl').append('<tr class="new-row">' +
                                        '<td class="chk-cmpny-data">' + '<input class="select-company" type="checkbox" name="selectedcompany" value="' + result.companyList[i].SEC_COM_CD + '">' + '</td>' +
                                        '<td>' + result.companyList[i].SEC_COM_CD + '</td>' +
                                        '<td>' + result.companyList[i].MasterComp.Mc_desc + '</td>' +
                                        '</tr>');
                            }
                            jQuery(".select-company").click(function () {
                                if (!jQuery('#chkmultiple').is(":checked")) {
                                    jQuery(".select-company").prop("checked", false);
                                    jQuery(this).prop("checked", true);
                                }
                                if (jQuery('[name="selectedcompany"]:checked').length == result.companyList.length) {
                                    jQuery("#chkAllCompany").prop("checked", true);
                                } else {
                                    jQuery("#chkAllCompany").prop("checked", false);
                                }
                            });

                        } else {
                            jQuery('.profcen-display-tbl').append('<tr class="new-row">' + "<td style='border:none; color: #ff6666; position: absolute; width: 80%; font-weight: bold;'>No company found for display</td>" + '</tr>');
                        }
                    } else {
                        jQuery('.profcen-display-tbl').append('<tr class="new-row">' + "<td style='border:none; color: #ff6666; position: absolute; width: 80%; font-weight: bold;'>No company found for display</td>" + '</tr>');
                    }
                } else {
                    Logout();
                }
            }
        });
    }
    jQuery("#chkAllCompany").click(function () {
        if (jQuery('#chkmultiple').is(":checked")) {
            if (jQuery('#chkAllCompany').is(":checked")) {
                jQuery(".select-company").prop("checked", true);
            } else {
                jQuery(".select-company").prop("checked", false);
            }
        }
    });
    jQuery("#chkAllCompany").attr("disabled", true);
    jQuery("#chkmultiple").click(function () {
        if (jQuery('#chkmultiple').is(":checked")) {
            jQuery("#chkAllCompany").removeAttr("disabled");
        } else {
            jQuery("#chkAllCompany").attr("disabled", true);
            jQuery("#chkAllCompany").prop("checked", false);
            jQuery(".select-company").prop("checked", false);
        }
    });
    //jQuery("#itemall").unbind('click').click(function (evt) {
    //    evt.preventDefault();
    //    if (jQuery('#itemall').is(":checked"))
    //    {
    //        jQuery('#exclupload').modal({
    //            keyboard: false,
    //            backdrop: 'static'
    //        }, 'show');
    //    }

    //});
    //jQuery(".btn-excel-upload").unbind('click').click(function (evt) {
    //    evt.preventDefault();
    //    jQuery('#exclupload').modal({
    //        keyboard: false,
    //        backdrop: 'static'
    //    }, 'show');
    //});

    $("#itemall").change(function (evt) {
        evt.preventDefault();
        //alert('ff');
        if (this.checked) {
            jQuery('#exclupload').modal({
                keyboard: false,
                backdrop: 'static'
            }, 'show');
        }
    });
    jQuery(".cls-excel-popup").unbind('click').click(function (evt) {
        $('#exclupload').modal('hide');
    });
    jQuery('.imprt-cd-data').click(function (e) {
        if (jQuery('#UploadedFile').val() == "") {
            setInfoMsg("Please Select File");
        } else {
            Lobibox.confirm({
                msg: "Do you want to continue process?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                       
                        var form = $('#imp-data')[0];
                       
                        var dataString = new FormData(form);
                        //alert("here1");
                        $.ajax({
                            url: "/ProductBonusDefinition/BindExceldata",  //Server script to process data
                            type: "POST",
                            xhr: function () {  // Custom XMLHttpRequest
                                var myXhr = $.ajaxSettings.xhr();
                                if (myXhr.upload) { // Check if upload property exists
                                    //myXhr.upload.onprogress = progressHandlingFunction
                                    myXhr.upload.addEventListener('progress', progressHandlingFunction,
                                    false); // For handling the progress of the upload
                                }
                                return myXhr;
                            },
                            //Ajax events
                            // Form data
                            data: dataString,
                            //Options to tell jQuery not to process data or worry about content-type.
                            cache: false,
                            contentType: false,
                            processData: false,
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        //alert('dgfdsfs');
                                        $('#exclupload').modal('hide');
                                        setSuccesssMsg("Successfully Uploaded");
                                    } else {
                                        setInfoMsg(result.msg);
                                    }
                                } else {
                                    Logout();
                                }
                            }
                        });
                    } else {
                        //alert("here2");
                    }
                }
            });
        }
    });
    function progressHandlingFunction(e) {
        if (e.lengthComputable) {
            var percentComplete = Math.round(e.loaded * 100 / e.total);
            $("#FileProgress").css("width",
            percentComplete + '%').attr('aria-valuenow', percentComplete);
            $('#FileProgress span').text(percentComplete + "%");
        }
        else {
            $('#FileProgress span').text('unable to compute');
        }
    }
  
    jQuery(".btn-excel-upload-data").unbind('click').click(function (evt) {

            jQuery('#exclupload2').modal({
                keyboard: false,
                backdrop: 'static'
            }, 'show');
    });

    // Udesh 12-Oct-2018
    jQuery(".btn-excel-print-data").click(function (evt) {

        var bonusCode = jQuery("#bonuscode").val();

        if (jQuery.trim(bonusCode).length > 0) {
            jQuery.ajax({
                cache: false,
                type: "GET",
                url: "/ProductBonusDefinition/PrintDetails",
                data: { BonusCode: bonusCode },
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {

                            window.location.href = result.urlpath;

                        } else {
                            setError(result.msg);
                        }
                    } else {
                        Logout();
                    }
                }
            });
        }
        else {
            setInfoMsg("Please select bonus code");
        }

    });

    jQuery(".upload-all-data").unbind('click').click(function (evt) {
        $('#exclupload2').modal('hide');
    });
    jQuery('.upload-all-data').click(function (e) {
        if (jQuery('#UploadedFile2').val() == "") {
            setInfoMsg("Please Select File");
        } else {
            Lobibox.confirm({
                msg: "Do you want to continue process?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {

                        var form = $('#imp-data2')[0];

                        var dataString = new FormData(form);
                        //alert("here1");
                        $.ajax({
                            url: "/ProductBonusDefinition/BindExceldata2",  //Server script to process data
                            type: "POST",
                            xhr: function () {  // Custom XMLHttpRequest
                                var myXhr = $.ajaxSettings.xhr();
                                if (myXhr.upload) { // Check if upload property exists
                                    //myXhr.upload.onprogress = progressHandlingFunction
                                    myXhr.upload.addEventListener('progress', progressHandlingFunction2,
                                    false); // For handling the progress of the upload
                                }
                                return myXhr;
                            },
                            //Ajax events
                            // Form data
                            data: dataString,
                            //Options to tell jQuery not to process data or worry about content-type.
                            cache: false,
                            contentType: false,
                            processData: false,
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        //alert('dgfdsfs');
                                        $('#exclupload2').modal('hide');
                                        SetItemDetails(result.list);
                                        setSuccesssMsg("Successfully Uploaded");
                                    } else {
                                   //     setInfoMsg(result.msg);

                                        jQuery('#excluploadErr').modal('show');
                                        var tbl = $("<table/>").attr("id", "mytable");
                                        tbl.attr("class", "table");
                                        tbl.empty();

                                        $("#excelErrtable").append(tbl);
                                        $("#mytable").append("<thead><th>Line No</th><th>Code</th><th>Description</th></thead>");
                                        for (var i = 0; i < result.data.length; i++) {
                                            var tr = "<tr>";
                                            var td1 = "<td>" + result.data[i]["LineNo"] + "</td>";
                                            var td2 = "<td>" + result.data[i]["Code"] + "</td>";
                                            var td3 = "<td>" + result.data[i]["Description"] + "</td></tr>";

                                            $("#mytable").append(tr + td1 + td2 + td3);

                                        }
                                    }
                                } else {
                                    Logout();
                                }
                            }
                        });
                    } else {
                        //alert("here2");
                    }
                }
            });
        }
    });
    function progressHandlingFunction2(e) {
        if (e.lengthComputable) {
            var percentComplete = Math.round(e.loaded * 100 / e.total);
            $("#FileProgress").css("width",
            percentComplete + '%').attr('aria-valuenow', percentComplete);
            $('#FileProgress span').text(percentComplete + "%");
        }
        else {
            $('#FileProgress span').text('unable to compute');
        }
    }

    jQuery(".close-excel-err-popup").unbind('click').click(function (evt) {
        $('#excluploadErr').modal('hide');
    });
});