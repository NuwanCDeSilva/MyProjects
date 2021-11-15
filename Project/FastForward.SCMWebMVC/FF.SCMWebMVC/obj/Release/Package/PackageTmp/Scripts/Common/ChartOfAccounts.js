jQuery(document).ready(function () {

    jQuery(".acc-search").click(function (evt) {
        var headerKeys = Array();
        headerKeys = ["Row", "Code"];
        field = "ChartAccountCode";
        var x = new CommonSearch(headerKeys, field);
    });


    jQuery(".btn-excel-upload").unbind('click').click(function (evt) {
        evt.preventDefault();
        jQuery('#exclupload').modal({
            keyboard: false,
            backdrop: 'static'
        }, 'show');
    });
    jQuery(".cls-excel-popup").unbind('click').click(function (evt) {
        $('#exclupload').modal('hide');
    });
    jQuery('.imprt-cd-data').click(function (e) {
        if (jQuery('#UploadedFile').val() == "") {
            setInfoMsg("Please Sellect File");
        } else {
            Lobibox.confirm({
                msg: "Do you want to continue process?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        var form = $('#imp-data')[0];
                        var dataString = new FormData(form);
                        $.ajax({
                            url: '/ChartOfAccounts/BindExceldata',  //Server script to process data
                            type: 'POST',
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
                                        BindAccDetails(result.data);
                                        $('#exclupload').modal('hide');
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
        }
    });
    function LoadMainTypes() {
        jQuery.ajax({
            type: "GET",
            url: "/ChartOfAccounts/LoadMainTypes",
            data: {},
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("acc_main_type");
                        jQuery("#acc_main_type").empty();
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
    function LoadMainTypes2() {
        jQuery.ajax({
            type: "GET",
            url: "/ChartOfAccounts/LoadMainTypes",
            data: {},
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("code");
                        jQuery("#code").empty();
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
    function LoadHeading() {
        jQuery.ajax({
            type: "GET",
            url: "/ChartOfAccounts/LoadHeading",
            data: {},
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("header");
                        jQuery("#header").empty();
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
    LoadMainTypes();
    LoadMainTypes2();
    LoadHeading();
    function LoadIsSubTp() {
        jQuery.ajax({
            type: "GET",
            url: "/ChartOfAccounts/LoadYesNoType",
            data: {},
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("issubtp");
                        jQuery("#issubtp").empty();
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
    LoadIsSubTp();
    $("#acc_main_type").change(function () {
        jQuery.ajax({
            type: "GET",
            url: "/ChartOfAccounts/LoadSubTypes",
            data: { MainType: $("#acc_main_type").val() },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("acc_sub_type");
                        jQuery("#acc_sub_type").empty();
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
                        if (result.Type=="Info")
                        {
                            setInfoMsg(result.data);
                        } else {
                            setError(result.msg);
                        }
                      
                    }
                } else {
                    Logout();
                }
            }
        })

    });
    $("#acc_sub_type").change(function () {
        jQuery.ajax({
            type: "GET",
            url: "/ChartOfAccounts/AdditionalTypes",
            data: { Subtype: $("#acc_sub_type").val() },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("acc_addi_type");
                        jQuery("#acc_addi_type").empty();
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
                        if (result.Type == "Info") {
                            setInfoMsg(result.data);
                        } else {
                            setError(result.msg);
                        }

                    }
                } else {
                    Logout();
                }
            }
        })

    });
    $("#code").change(function () {
        jQuery.ajax({
            type: "GET",
            url: "/ChartOfAccounts/LoadSubTypes",
            data: { MainType: $("#code").val() },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("type");
                        jQuery("#type").empty();
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
                        if (result.Type == "Info") {
                            setInfoMsg(result.data);
                        } else {
                            setError(result.msg);
                        }

                    }
                } else {
                    Logout();
                }
            }
        })

    });
    jQuery(".btn-mtype-crt").unbind('click').click(function (evt) {
        evt.preventDefault();
        jQuery('#maintypecrt').modal({
            keyboard: false,
            backdrop: 'static'
        }, 'show');
    });
    jQuery(".cls-type-popup").unbind('click').click(function (evt) {
        $('#maintypecrt').modal('hide');
    });
    //save-tp-data
    jQuery(".save-tp-data").click(function (evt) {
        evt.preventDefault();
        var Code = jQuery("#code").val();
        var Type = jQuery("#type").val();
        var Desc = jQuery("#desc").val();
        var OrdeNo = jQuery("#order").val();
        var Header = jQuery("#header").val();
        var IsSTp = jQuery("#issubtp").val();
        Lobibox.confirm({
            msg: "Do you want to save ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    jQuery.ajax({
                        cache: false,
                        type: "GET",
                        url: "/ChartOfAccounts/SaveAccountGRPDetails",
                        data: { Code: Code, Type: Type, Desc: Desc, OrdeNo: OrdeNo, Header: Header, IsSTp: IsSTp },
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    setSuccesssMsg(result.Msg);
                                } else {
                                    setError(result.Msg);
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

    //add-acc-det
    jQuery(".add-acc-det").click(function (evt) {
        evt.preventDefault();
        var MainType = jQuery("#acc_main_type").val();
        var SubType = jQuery("#acc_sub_type").val();
        var Addtype = jQuery("#acc_addi_type").val();
        var AccountCode = jQuery("#accountscode").val();
        var AccountName = jQuery("#accountsname").val();
        var OtherRef = jQuery("#acc_other_ref").val();
        var SuppAddr = jQuery("#suplier_address").val();
        var VatRegNo = jQuery("#vat_reg_no").val();
        if ($("#chkactive").is(':checked') == true) {
            var Active = "1";
        } else {
            var Active = "0";
        }
        Lobibox.confirm({
            msg: "Do you want to add ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    jQuery.ajax({
                        cache: false,
                        type: "GET",
                        url: "/ChartOfAccounts/AddAccountDetails",
                        data: { MainType: MainType, SubType: SubType, Addtype: Addtype, AccountCode: AccountCode, AccountName: AccountName, OtherRef: OtherRef, SuppAddr: SuppAddr, VatRegNo: VatRegNo, Active: Active },
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    BindAccDetails(result.Data);
                                } else {
                                    if (result.Type = "Info")
                                    {
                                        setInfoMsg(result.Msg);
                                    } else {
                                        setError(result.Msg);
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

    });
    //btn-save-data
    jQuery(".btn-save-data").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to Save ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    jQuery.ajax({
                        cache: false,
                        type: "GET",
                        url: "/ChartOfAccounts/SaveCharAccounts",
                        data: {},
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    setSuccesssMsg(result.Msg);
                                } else {
                                    if (result.Type = "Info") {
                                        setInfoMsg(result.Msg);
                                    } else {
                                        setError(result.Msg);
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

    });
    function BindAccDetails(data) {
        jQuery('.acc-data-row .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {
                jQuery('.acc-data-row').append('<tr class="new-row">' +
                        '<td>' + data[i].rca_acc_no + '</td>' +
                          '<td>' + data[i].rca_acc_desc + '</td>' +
                           '<td>' + data[i].rca_mgrp_cd + '</td>' +
                         '<td>' + data[i].rca_sgrp_cd + '</td>' +
                            '<td>' + data[i].rca_ssub_cd + '</td>' +
                             '<td>' + data[i].rca_acc_rmk + '</td>' +
                          '<td>' + data[i].rca_anal1 + '</td>' +
                           '<td>' + data[i].rca_anal2 + '</td>' +
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
            var AccNo = jQuery(tr).find('td:eq(0)').html();
            var Mtype = jQuery(tr).find('td:eq(2)').html();
            var Stype = jQuery(tr).find('td:eq(3)').html();
            var OtherType = jQuery(tr).find('td:eq(4)').html();
            Lobibox.confirm({
                msg: "Do you want to remove ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/ChartOfAccounts/RemoveItemDet",
                            data: { AccNo: AccNo, Mtype: Mtype, Stype: Stype, OtherType: OtherType},
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        BindAccDetails(result.data);
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
    $('#accountscode').focusout(function () {
        var Code = $(this).val();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/ChartOfAccounts/LoadDetails",
            data: { Code: Code },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.data.length > 0) {
                            BindAccDetails(result.data);
                            jQuery("#acc_main_type").val(result.Maintype);
                            jQuery("#acc_sub_type").val(result.SubType);
                            jQuery("#acc_addi_type").val(result.AddType);
                            jQuery("#accountsname").val(result.AccName);
                        }
                    } else {
                    }
                } else {
                    Logout();
                }
            }
        });
    });
});