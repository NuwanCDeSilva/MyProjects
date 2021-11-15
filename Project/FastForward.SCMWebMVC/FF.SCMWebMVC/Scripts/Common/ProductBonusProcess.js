jQuery(document).ready(function () {
    jQuery('#fdate').val(my_date_format(new Date()));
    jQuery('#fdate').datepicker({
        dateFormat: "dd/M/yy",
        onSelect: function (dateText) {
            jQuery('#sfdate').val(jQuery('#fdate').val());
        }
    })
    jQuery('#tdate').val(my_date_format(new Date()));
    jQuery('#tdate').datepicker({
        dateFormat: "dd/M/yy",
        onSelect: function (dateText) {
            jQuery('#stdate').val(jQuery('#tdate').val());
        }
    })
    jQuery('#sfdate').val(my_date_format(new Date()));
    jQuery('#sfdate').datepicker({ dateFormat: "dd/M/yy" })
    jQuery('#stdate').val(my_date_format(new Date()));
    jQuery('#stdate').datepicker({ dateFormat: "dd/M/yy" })
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
    jQuery(".btn-show-data").click(function () {
        var Code = jQuery("#Code").val();
        var fdate = jQuery("#fdate").val();
        var tdate = jQuery("#tdate").val();
        var salesfdate = jQuery("#sfdate").val();
        var salestdate = jQuery("#stdate").val();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/ProductBonusProcess/ViewProductBonus",
            data: { Code: Code, FromDate: fdate, ToDate: tdate, salesfdate: salesfdate, salestdate: salestdate },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.type == "info") {
                            setInfoMsg(result.msg);
                        } else {
                            if (result.summery.length > 0) {
                                SetProductBonusData(result.summery);
                            }
                        }

                    } else {
                        setError(result.msg);
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    function SetProductBonusData(data) {
        jQuery('.bonus-data-row .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {
                jQuery('.bonus-data-row').append('<tr class="new-row">' +
                        '<td>' + data[i].ExecCode + '</td>' +
                          '<td>' + data[i].ExecName + '</td>' +
                           '<td>' + data[i].pc + '</td>' +
                              '<td>' + data[i].TotAmmount.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString() + '</td>' +
                                '<td>' + data[i].Qty + '</td>' +
                                '<td>' + data[i].TotMarks.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString() + '</td>' +
                       '<td style="text-align:center;"><img class="delete-img invoice-view" src="../Resources/images/Search-icon.png"></td>' +
                        '</tr>');
            }
            InvoiceNumbersViewFunction();
        }
    }
    function InvoiceNumbersViewFunction() {
        jQuery(".invoice-view").unbind('click').click(function (evt) {
            evt.preventDefault();
            var td = jQuery(this).parent('td');
            var tr = jQuery(td).parent('tr');
            var exec = jQuery(tr).find('td:eq(0)').html();
            jQuery.ajax({
                type: "GET",
                url: "/ProductBonusProcess/GetInvoiceNumbers",
                data: { ExecCode: exec },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.allinvoice.length > 0) {
                                SetInvoiceDetails(result.allinvoice);

                                jQuery('#invoicedetails').modal({
                                    keyboard: false,
                                    backdrop: 'static'
                                }, 'show');
                            }

                        }
                    } else {
                        Logout();
                    }
                }
            });
        });
    }
    function SetInvoiceDetails(data) {
        jQuery('.invoice-data-row .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {
                jQuery('.invoice-data-row').append('<tr class="new-row">' +
                        '<td>' + data[i].InvoiceNo + '</td>' +
                          '<td>' + data[i].ItemCode + '</td>' +
                            '<td>' + data[i].TotAmmount.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString() + '</td>' +
                        '</tr>');
            }
        }
    }
    jQuery(".Code-search").click(function (evt) {
        var headerKeys = Array();
        //headerKeys = ["Row", "Code"]; //Commented by Udesh 06-Nov-2018
        headerKeys = ["Row", "Code", "Circ No"]; //Added by Udesh 06-Nov-2018
        field = "BonusCode2";
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".btn-save-data").click(function () {
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/ProductBonusProcess/SaveProductBonus",
            data: {},
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.type == "err") {
                            setInfoMsg(result.msg);
                        } else {
                            setSuccesssMsg(result.msg);
                        }
                    } else {
                        setError(result.msg);
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    jQuery(".btn-clear-data").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to clear data ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    window.location.href = "/ProductBonusProcess";
                }
            }
        });

    });
});