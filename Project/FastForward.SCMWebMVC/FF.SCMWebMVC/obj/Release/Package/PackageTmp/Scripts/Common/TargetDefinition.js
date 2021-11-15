jQuery(document).ready(function () {
    jQuery('#month').val(my_date_formatmonth(new Date()));
    jQuery('#month').datepicker({ dateFormat: "MM yy" });
    jQuery(".commission-search").click(function (evt) {
        var headerKeys = Array();
        headerKeys = ["Row", "Code"];
        field = "CommissionCode";
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".empcode-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Epf No", "Category", "First Name", "Last Name", "NIC"];
        field = "employee4"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".manager-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Epf No", "Category", "First Name", "Last Name", "NIC"];
        field = "employee5"
        var x = new CommonSearch(headerKeys, field);
    });
    AddTargets();
    function AddTargets() {
        jQuery(".add-data").unbind('click').click(function (evt) {
            evt.preventDefault();
            var CommCode = jQuery("#commcode").val();
            var month = jQuery("#month").val();
            var execcd = jQuery("#exc_cd").val();
            var target = jQuery("#target").val();
            var fromper = jQuery("#fperc").val();
            var toper = jQuery("#tperc").val();
            var execcomm = jQuery("#execomm").val();
            var manager = jQuery("#manager").val();
            var managercomm = jQuery("#managercomm").val();
            var brand = jQuery("#brand").val();

            if (CommCode == "" || month == "" || execcd == "" || target == "" || fromper == "" || toper == "" || execcomm == "") {
                setInfoMsg('Cannot Empty Fields!!');
                return;
            }
            Lobibox.confirm({
                msg: "Do you want to add ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/TargetDefintion/AddTargets",
                            data: { CommCode: CommCode, month: month, execcd: execcd, target: target, fromper: fromper, toper: toper, execcomm: execcomm, manager: manager, managercomm: managercomm, brand: brand },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                jQuery('.target-data-row .new-row').remove();
                                if (result.login == true) {
                                    if (result.success == true) {
                                        updateTargetgrid(result.list);
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
    }
    function updateTargetgrid(data) {
        
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) { 
                jQuery('.target-data-row').append('<tr class="new-row">' +
                    '<td>' + parseFloat(i+1)  + '</td>' +
                          '<td>' + data[i].rctc_doc_no + '</td>' +
                          '<td>' + data[i].rctc_exec + '</td>' +
                           '<td>' + data[i].rctc_month + '</td>' +
                         '<td style="color: black; font-size: x-small;text-align:right">' + ReplaceNumberWithCommas(data[i].rctc_st_per) + '</td>' +
                          '<td style="color: black; font-size: x-small;text-align:right">' + ReplaceNumberWithCommas(data[i].rctc_end_per) + '</td>' +
                           '<td style="color: black; font-size: x-small;text-align:right">' + ReplaceNumberWithCommas(data[i].rctc_target) + '</td>' +
                          '<td style="color: black; font-size: x-small;text-align:right">' + ReplaceNumberWithCommas(data[i].rctc_exc_rate) + '</td>' +
                          '<td>' + data[i].rctc_mngr + '</td>' +
                          '<td style="color: black; font-size: x-small;text-align:right">' + ReplaceNumberWithCommas(data[i].rctc_mngr_rate) + '</td>' +
                           '<td>' + data[i].rctc_anal1 + '</td>' +
                       '<td style="text-align:center;"><img class="delete-img remove-target-ovrt" src="../Resources/images/Remove.png"></td>' +
                        '</tr>');
            }
            RemoveGrid();
        }
    }
    function getFormatedDate1(date) {
        var dte = new Date(parseInt(date.substr(6)));
        if (my_date_format_tran(dte) != "NaN/undefined/NaN")
            return my_date_format_tran(dte);
    }
    function ReplaceNumberWithCommas(yourNumber) {
        //Seperates the components of the number
        var n = yourNumber.toString().split(".");
        if (n.length == 1) {
            yourNumber = yourNumber.toString() + ".00";
        }
        n = yourNumber.toString().split(".");

        //Comma-fies the first part
        n[0] = n[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        //Combines the two sections

        return n.join(".");
    }
    function RemoveGrid() {
        jQuery(".remove-target-ovrt").unbind('click').click(function (evt) {
            evt.preventDefault();
            var td = jQuery(this).parent('td');
            var tr = jQuery(td).parent('tr');
            var docno = jQuery(tr).find('td:eq(1)').html();
            var execcd = jQuery(tr).find('td:eq(2)').html();
            var month = jQuery(tr).find('td:eq(3)').html();
            var from = jQuery(tr).find('td:eq(4)').html();
            var to = jQuery(tr).find('td:eq(5)').html();
            var brand = jQuery(tr).find('td:eq(10)').html();

            Lobibox.confirm({
                msg: "Do you want to remove ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/TargetDefintion/RemoveGrid",
                            data: { docno: docno, execcd: execcd, month: month, from: from, to: to, brand: brand },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                jQuery('.target-data-row .new-row').remove();
                                if (result.login == true) {
                                    if (result.success == true) {
                                        updateTargetgrid(result.list);

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
    }
    

    jQuery(".btn-clear-data").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to clear ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    jQuery.ajax({
                        cache: false,
                        type: "POST",
                        url: "/TargetDefintion/ClearSession",
                        data: {},
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) { 
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

    jQuery(".btn-save-data").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to save ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    jQuery.ajax({
                        cache: false,
                        type: "GET",
                        url: "/TargetDefintion/SaveDetails",
                        data: {},
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    setSuccesssMsg(result.msg);
                                   // clearAll();
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
    jQuery(".btn-show-data").click(function (evt) {
        var CommCode = jQuery("#commcode").val();
        var month = jQuery("#month").val();
        var execcd = jQuery("#exc_cd").val();

        if (CommCode=="")
        {
            setInfoMsg('Please Enter Commission Code!');
            return;
        }
        if (month == "") {
            setInfoMsg('Please Select Month!');
            return;
        }
        if (execcd == "") {
            setInfoMsg(' Please Select Exec Code !');
            return;
        }

        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/TargetDefintion/LoadDetails",
            data: { CommCode: CommCode, month: month, execcd: execcd },
            success: function (result) {
                jQuery('.target-data-row .new-row').remove();
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.list.length > 0) {
                            //set item data
                            if (result.list.length > 0) {
                                updateTargetgrid(result.list);
                            }
                        } else {
                            setInfoMsg('No Data Found!');
                        }
                    } else {
                        setInfoMsg('No Data Found! Please Check Details!!');
                        return;
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    jQuery(".search-brand-cd").click(function (evt) {
        var headerKeys = Array();
        headerKeys = ["Row", "Code", "Description"];
        var data = jQuery("#BrandMngr").val();
        field = "srchItemBrandM";
        var resData = new CommonSearch(headerKeys, field, data);
    });
});

function clearAll() {
    jQuery("#commcode").val("");
    jQuery("#fperc").val("");
    jQuery("#managercomm").val("");
    jQuery("#tperc").val("");
    jQuery("#brand").val("");
    jQuery("#exc_cd").val("");
    jQuery("#target").val("");
    jQuery("#manager").val("");
    jQuery("#execomm").val("");
    jQuery('.target-data-row .new-row').remove();
}