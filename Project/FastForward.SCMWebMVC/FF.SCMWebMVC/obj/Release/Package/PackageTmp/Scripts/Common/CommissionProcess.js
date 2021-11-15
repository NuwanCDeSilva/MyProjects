jQuery(document).ready(function () {
    jQuery('#fdate').val(my_date_format(new Date()));
    jQuery('#fdate').datepicker({ dateFormat: "dd/M/yy" })
    jQuery('#tdate').val(my_date_format(new Date()));
    jQuery('#tdate').datepicker({ dateFormat: "dd/M/yy" })

    jQuery(".profit-center-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description", "Address", "Chanel"];
        field = "ProfitCenter"
        var x = new CommonSearch(headerKeys, field);
    });
    profitcenterFocusout();
    function profitcenterFocusout() {
        $('#ProfitCenter').focusout(function () {
            var pc = $(this).val();
            jQuery.ajax({
                cache: false,
                type: "GET",
                url: "/Search/getProfitCenters",
                data: { pgeNum: "1", pgeSize: "10", searchFld: "Code", searchVal: pc },
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.data.length > 0) {
                            }
                        } else {
                            setInfoMsg('Invalid Profi Center!!');
                            jQuery("#ProfitCenter").val("");
                            jQuery("#ProfitCenter").focus();
                            return;
                        }
                    } else {
                        Logout();
                    }
                }
            });
        });
    }

    jQuery(".btn-show-data").click(function () {
        var pc = jQuery("#ProfitCenter").val();
        var fdate = jQuery("#fdate").val();
        var tdate = jQuery("#tdate").val();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/CommissionProcess/LoadCommissions",
            data: { ProfitCenter: pc, FromDate: fdate, ToDate: tdate },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.type=="info")
                        {
                            setInfoMsg(result.msg);
                        } else
                        {
                            if (result.summery.length > 0)
                            {
                                SetCommissionData(result.summery);
                            }
                            if (result.effect>0)
                            {
                                setSuccesssMsg("Finalized!!!");
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
    jQuery(".btn-show-elite-data").click(function () {
        var pc = jQuery("#ProfitCenter").val();
        var fdate = jQuery("#fdate").val();
        var tdate = jQuery("#tdate").val();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/CommissionProcess/LoadEiteCommissions",
            data: { ProfitCenter: pc, FromDate: fdate, ToDate: tdate },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.type == "info") {
                            setInfoMsg(result.msg);
                        } else {
                            if (result.summery.length > 0) {
                                SetCommissionData(result.summery);
                            }
                            if (result.effect > 0) {
                                setSuccesssMsg("Finalized!!!");
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
    jQuery(".add-pc").click(function () {
        var pc = jQuery("#ProfitCenter").val();
        if (pc == "") {
            setInfoMsg('Please Select PC');
        } else {
            jQuery.ajax({
                cache: false,
                type: "GET",
                url: "/CommissionProcess/AddProfitCenters",
                data: { proficenter: jQuery("#ProfitCenter").val() },
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.data.length > 0) {
                                //set grid
                                SetPcGrid(result.data);
                                jQuery("#ProfitCenter").val("");
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
    function SetPcGrid(data) {
        jQuery('.pc-row .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {
                jQuery('.pc-row').append('<tr class="new-row">' +
                        '<td>' + data[i].pccode + '</td>' +
                       '<td style="text-align:center;"><img class="delete-img remove-pccd" src="../Resources/images/Remove.png"></td>' +
                        '</tr>');
            }
            RemovePCCodeFunction();
        }
    }
    function RemovePCCodeFunction() {
        jQuery(".delete-img.remove-pccd").unbind('click').click(function (evt) {
            evt.preventDefault();
            var td = jQuery(this).parent('td');
            var tr = jQuery(td).parent('tr');
            var pccd = jQuery(tr).find('td:eq(0)').html();
            Lobibox.confirm({
                msg: "Do you want to remove ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/CommissionProcess/RemovePCCode",
                            data: { profitcenter: pccd },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        SetPcGrid(result.data);
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
    profitcenterFocusout();
    function profitcenterFocusout() {
        $('#ProfitCenter').focusout(function () {
            var pc = $(this).val();
            jQuery.ajax({
                cache: false,
                type: "GET",
                url: "/Search/getProfitCenters",
                data: { pgeNum: "1", pgeSize: "10", searchFld: "Code", searchVal: pc },
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.data.length > 0) {
                            }
                        } else {
                            setInfoMsg('Invalid Profi Center!!');
                            jQuery("#ProfitCenter").val("");
                            jQuery("#ProfitCenter").focus();
                            return;
                        }
                    } else {
                        Logout();
                    }
                }
            });
        });
    }
    jQuery(".channel-search").click(function (evt) {
        var headerKeys = Array();
        headerKeys = ["Row", "Code", "Description"];
        field = "srchChnl";
        var data = "channel";
        var x = new CommonSearch(headerKeys, field, data);
    });
    $('#chanel').focusout(function () {
        var chanal = $(this).val();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/CommissionProcess/GetPC",
            data: { Chanal: chanal },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.pclist.length > 0) {
                            SetPcGrid(result.pclist);
                        }
                    } else {
                        setInfoMsg('Invalid Chanal Code!!');
                        jQuery("#chanel").val("");
                        jQuery("#chanel").focus();
                        return;
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    function SetCommissionData(data) {
        jQuery('.commission-data-row .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {
                jQuery('.commission-data-row').append('<tr class="new-row">' +
                        '<td>' + data[i].ExecCode + '</td>' +
                          '<td>' + data[i].ExecName + '</td>' +
                            '<td>' + data[i].Item + '</td>' +
                              '<td>' + data[i].TotValue.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString() + '</td>' +
                                '<td>' + data[i].Qty + '</td>' +
                                '<td>' + data[i].ItemCommission.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString() + '</td>' +
                              '<td>' + data[i].EmpCommission.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString() + '</td>' +
                                '<td>' + data[i].FinalCommission.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString() + '</td>' +
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
                url: "/CommissionProcess/GetInvoiceNumbers",
                data: { ExecCode: exec },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.allinvoice.length > 0)
                            {
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
    function SetInvoiceDetails(data)
    {
        jQuery('.invoice-data-row .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {
                jQuery('.invoice-data-row').append('<tr class="new-row">' +
                        '<td>' + data[i].InvoiceNo + '</td>' +
                          '<td>' + data[i].Item + '</td>' +
                            '<td>' + data[i].TotValue.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString() + '</td>' +
                        '</tr>');
            }
        }
    }
    jQuery(".btn-finalize-data").click(function () {
        var fromdate = jQuery("#fdate").val();
        var todate = jQuery("#tdate").val();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/CommissionProcess/SaveCommissions",
            data: {Fromdate :fromdate,Todate:todate },
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
                    window.location.href = "/CommissionProcess";
                }
            }
        });

    });
    
    jQuery(".commission-search").click(function (evt) {
        var headerKeys = Array();
        headerKeys = ["Row", "Code"];
        field = "CommissionCode";
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery(".btn-viewCommissions-data").click(function () {
        var pc = jQuery("#ProfitCenter").val();
        var fdate = jQuery("#fdate").val();
        var tdate = jQuery("#tdate").val();
        var commcode = jQuery("#commcode").val();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/CommissionProcess/ViewCommissions",
            data: { ProfitCenter: pc, FromDate: fdate, ToDate: tdate, circularCode: commcode },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        //if (result.type == "err") {
                        //    setInfoMsg(result.msg);
                        //} else {
                        //    setSuccesssMsg(result.msg);
                        //}
                        if (result.number == 0) {

                        } else {
                            window.location.href = result.urlpath;
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

    jQuery(".btn-detail-data").click(function () {
      
        var fdate = jQuery("#fdate").val();
        var tdate = jQuery("#tdate").val();
        var commcode = jQuery("#commcode").val();
      
         jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/CommissionProcess/ViewDetails",
            data: { fdate: fdate, tdate: tdate, commcode: commcode },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                       // if (result.type == "err") {
                            //setInfoMsg(result.msg);
                       // } else {
                           // setSuccesssMsg(result.msg);
                       // }
                        if (result.number == 0) {


                        } else {
                            window.location.href = result.urlpath;
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

    //jQuery(".btn-target-data").click(function () {
    //    var fdate = jQuery("#fdate").val();
    //    var tdate = jQuery("#tdate").val();
    //    var commcode = jQuery("#commcode").val();
    //    window.open(
    //          "/CommissionProcess/ViewWithTarget",
    //          '_blank' // <- This is what makes it open in a new window.
    //      );
    //});

    //jQuery(".btn-target-data").click(function () {

    //    var fdate = jQuery("#fdate").val();
    //    var tdate = jQuery("#tdate").val();
    //    var commcode = jQuery("#commcode").val();

    //    jQuery.ajax({
    //        cache: false,
    //        type: "GET",
    //        url: "/CommissionProcess/ViewWithTarget",
    //        data: { FromDate: fdate, ToDate: tdate, circularCode: commcode },
    //        success: function (result) {
    //            if (result.login == true) {
    //                if (result.success == true) {
    //                    if (result.number == 0) {
    //                    } else {
    //                        window.location.href = result.urlpath;
    //                    }
    //                } else {
    //                    setError(result.msg);
    //                }
    //            } else {
    //                //Logout();
    //            }
    //        }
    //    });
    //});

    $(document).on("click", "[type='checkbox']", function (e) {
        if (this.checked) {
            $(this).attr("value", "true");
        } else {
            $(this).attr("value", "false");
        }
    });

    jQuery(".btn-target-data").click(function () {

        if (jQuery("#commcode").val() == "") {
            setInfoMsg("Please select circular code");
            return;
        }
        window.open(
              "/CommissionProcess/ViewWithTarget?FromDate=" + jQuery("#fdate").val() + "&ToDate=" + jQuery("#tdate").val() + "&circularCode=" + jQuery("#commcode").val() + "&ExcelChecked=" + jQuery("#chkExcel").val() + "&EmpCode=" + jQuery("#overemp").val(),
              '_blank' // <- This is what makes it open in a new window.
          );
    });

    jQuery(".btn-Sls-Con-data").click(function () {

        window.open(
              "/CommissionProcess/SalesContribution?FromDate=" + jQuery("#fdate").val() + "&ToDate=" + jQuery("#tdate").val() + "&circularCode=" + jQuery("#commcode").val() + "&ExcelChecked=" + jQuery("#chkExcel").val(),
              '_blank' // <- This is what makes it open in a new window.
          );
    });

    jQuery(".emp-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Epf No", "Category", "First Name", "Last Name", "NIC"];
        field = "employeeNew"
        var x = new CommonSearch(headerKeys, field);
    });
    //jQuery(".btn-viewCommissions-data").click(function (evt) {
    //    jQuery.ajax({
    //        type: 'POST',
    //        url: '/CommissionProcess/ExportExcel',
    //        success: function (result) {
    //            if (result.number == 0) {

    //            } else {
    //                window.location.href = result.urlpath;
    //            }
    //        }
    //    })
    //});
});