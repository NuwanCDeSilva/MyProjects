jQuery(document).ready(function () {
    loadCompany();
    jQuery("#chkmultiple").attr("disabled", true);//disable for future process
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

    jQuery("#chkAllCompany").click(function () {
        if (jQuery('#chkmultiple').is(":checked")) {
            if (jQuery('#chkAllCompany').is(":checked")) {
                jQuery(".select-company").prop("checked", true);
            } else {
                jQuery(".select-company").prop("checked", false);
            }
        }
    });

    jQuery("#ProfCen").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description"];
            field = "rptProfCenSrch"
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery(".btn-pro-cnt").click(function (evt) {
        evt.preventDefault();
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description", "Channel"];
        field = "ptycshsetpc";
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".prof-cen-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description"];
        field = "rptProfCenSrch"
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery("#Channel").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description"];
            field = "rptCnelSrch"
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery(".channel-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description"];
        field = "rptCnelSrch"
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery("#SubChannel").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description"];
            data = jQuery("#Channel").val();
            field = "rptSubCnelSrch"
            var x = new CommonSearch(headerKeys, field, data);
        }
    });
    jQuery(".subchannel-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description"];
        field = "rptSubCnelSrch"
        data = jQuery("#Channel").val();
        var x = new CommonSearch(headerKeys, field, data);
    });

    jQuery(".sales-rpt-cls .add-prof-center").click(function () {
        loadProfitCenters();
    });
    jQuery("#chkAllProfCenter").click(function () {
        if (jQuery('#chkAllProfCenter').is(":checked")) {
            jQuery(".profit-centers").prop("checked", true);
        } else {
            jQuery(".profit-centers").prop("checked", false);
        }
    });
    jQuery('#FromDate').datepicker({ maxDate: new Date(), dateFormat: "dd/M/yy" });
    jQuery('#ToDate').datepicker({ maxDate: new Date(), dateFormat: "dd/M/yy" });
    jQuery('#FromDate').val(getFormatedDate(getMonthAgoMonth(getFormatedDate(new Date()))));
    jQuery('#ToDate').val(getFormatedDate(new Date()));

    jQuery('#AsAtDate').datepicker({ maxDate: new Date(), dateFormat: "dd/M/yy" });
    jQuery('#AsAtDate').val(getFormatedDate(new Date()));



    jQuery("#Customer").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Name", "Nic", "Mobile", "Br No"];
            field = "salesCusSrch";
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery(".sales-cus-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Name", "Nic", "Mobile", "Br No"];
        field = "salesCusSrch";
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery(".operations-dri-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "EPF NO", "FIRST NAME", "LAST NAME", "Mobile", "Nic"];
        field = "logDriSearch";
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".operations-veh-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "REG NO", "VEHICLE TYPE", "MODEL", "OWNER"];
        field = "logVehiSearch";
        var x = new CommonSearch(headerKeys, field);
    });


    jQuery(".btn-opr-rpt-down").click(function (evt) {
        
        if (jQuery("#FromDate").val() != "" && jQuery("#ToDate").val() != "" && jQuery("#AsAtDate").val() != "") {
            evt.preventDefault();
            var checkcom = false;

            //if (jQuery("#DocNo").val() == "")
            //{
            //    setInfoMsg("Please select Doc No to view.");
            //    return;
            //}
           
            jQuery('input[name="selectedcompany[]"]:checked').each(function () {
                if (this.value != "") {
                    checkcom = true;
                }
            });
            var checkpc = false;
            jQuery('input[name="selectedprofcen[]"]:checked').each(function () {
                if (this.value != "") {
                    checkpc = true;

                }
            });
            
            if (typeof jQuery('input[name=rad_select]:checked', '#frm-report').val() != "undefined") {
                if (jQuery("#InvoiceDate").is(':checked')) {
                    setInfoMsg("Please select job status");
                }
                else {
                    jQuery("#frm-report").submit();
                }
                    
            } else {
                setInfoMsg("Please select report to view.");
            }
        } else {
            setInfoMsg("Please select valid date range.");
        }

        
    });

    jQuery(".btn-sls-rpt-clear").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to clear data ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    clearSalesReportForm();
                }
            }
        });
    });


    jQuery("#FromDate,#ToDate,#AsAtDate").focusout(function () {
        if (ValidDate(jQuery(this).val()) == "false" && jQuery(this).val() != "") {
            setInfoMsg("Please enter valid date.");
            jQuery(this).val("");
        }
    });
    jQuery(".btn-custype-srch").click(function (evt) {
        evt.preventDefault();
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description"];
        field = "custypsrch";
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".btn-paytype-srch").click(function (evt) {
        evt.preventDefault();
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description"];
        field = "paytypsrch";
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".btn-creusr-srch").click(function (evt) {
        evt.preventDefault();
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Name"];
        field = "creusrsrch";
        var x = new CommonSearch(headerKeys, field);
    });
});
function loadProfitCenters() {
    var channel = jQuery("#Channel").val();
    var subcnl = jQuery("#SubChannel").val();
    var pc = jQuery("#ProfCen").val();
    jQuery.ajax({
        type: "GET",
        url: "/Reporting/loadProfitCenters?channel=" + channel + "&subChanel=" + subcnl + "&proCen=" + pc,
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.login == true) {
                if (result.success == true) {
                    jQuery('.profcen-display-tbl .new-row').remove();
                    if (result.profCen.length > 0) {
                        for (i = 0; i < result.profCen.length; i++) {
                            jQuery('.profcen-display-tbl').append('<tr class="new-row">' +
                                    '<td class="chk-prof-cen-data">' + '<input class="profit-centers" type="checkbox" name="selectedprofcen[]" id="chkpc" value="' + result.profCen[i] + '">' + '</td>' +
                                    '<td>' + result.profCen[i] + '</td>' + '</td>' +
                                    '</tr>');
                        }
                    } else {
                        jQuery('.profcen-display-tbl').append('<tr class="new-row">' + "<td style='border:none; color: #ff6666; position: absolute; width: 80%; font-weight: bold;'>No profit centers found for display</td>" + '</tr>');
                    }
                } else {
                    jQuery('.profcen-display-tbl').append('<tr class="new-row">' + "<td style='border:none; color: #ff6666; position: absolute; width: 80%; font-weight: bold;'>No profit centers found for display</td>" + '</tr>');
                }
                jQuery("#Channel").val("");
                jQuery("#SubChannel").val("");
                jQuery("#ProfCen").val("");
            } else {
                Logout();
            }
        }
    });
}

function loadCompany() {
    jQuery.ajax({
        type: "GET",
        url: "/Reporting/loadCompany",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.login == true) {
                if (result.success == true) {
                    jQuery('.compny-display-tbl .new-row').remove();
                    if (result.companyList.length > 0) {
                        for (i = 0; i < result.companyList.length; i++) {
                            jQuery('.compny-display-tbl').append('<tr class="new-row">' +
                                    '<td class="chk-cmpny-data">' + '<input class="select-company" type="checkbox" name="selectedcompany[]" value="' + result.companyList[i].SEC_COM_CD + '">' + '</td>' +
                                    '<td>' + result.companyList[i].SEC_COM_CD + '</td>' +
                                    '<td>' + result.companyList[i].MasterComp.Mc_desc + '</td>' +
                                    '</tr>');
                        }
                        jQuery(".select-company").click(function () {
                            if (!jQuery('#chkmultiple').is(":checked")) {
                                jQuery(".select-company").prop("checked", false);
                                jQuery(this).prop("checked", true);
                            }
                            if (jQuery('[name="selectedcompany[]"]:checked').length == result.companyList.length) {
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
function clearSalesReportForm() {
    jQuery("#chkAllCompany").attr("disabled", true);
    jQuery("#chkAllCompany").prop("checked", false);
    jQuery("#chkmultiple").prop("checked", false);
    jQuery(".select-company").prop("checked", false);
    jQuery('.profcen-display-tbl .new-row').remove();
    jQuery('#FromDate').val(getFormatedDate(new Date()));
    jQuery('#ToDate').val(getFormatedDate(new Date()));
    jQuery('#AsAtDate').val(getFormatedDate(new Date()));
    jQuery("#Channel").val("");
    jQuery("#SubChannel").val("");
    jQuery("#ProfCen").val("");
    jQuery("#Customer").val("");
    jQuery("#cus_cd").val("");
    jQuery("#invno").val("");
    jQuery("#DocNo").val("");
    jQuery("#Bl_m_doc_no").val("");
    jQuery("#Bl_h_doc_no").val("");
    jQuery("#ProfitCenter").val("");
    jQuery("#job-number").val("");
    jQuery("#chkAllProfCenter").prop("checked", false);
    jQuery("#REPORT_TYPE").prop("checked", false);
}

$(document).ready(function () {
    $('input[type="radio"]').click(function () {
        $('#AsAtDate').attr("disabled", "disabled")
        $('#AsAtDate').removeAttr("disabled");
        if ($(this).attr('id') == 'salesWithGP' || $(this).attr('id') == 'sales_man' || $(this).attr('id') == 'cus_wise' || $(this).attr('id') == 'pro_wise' || $(this).attr('id') == 'vol_wise') {
            $('#rptOption').show();
            $('#rptJobOption').hide();
            $('#rptMarketingOption').hide();
            $('#rptJobOptionType').hide();
            $('#rptJobOptionTypeSub').hide();
        }
        else if ($(this).attr('id') == 'jobStatusReport' || $(this).attr('id') == 'pending' || $(this).attr('id') == 'close' || $(this).attr('id') == 'notinvoiced') {
            $('#rptJobOption').show();
            $('#rptOption').hide();
            $('#rptMarketingOption').hide();
            $('#rptJobOptionType').hide();
            $('#rptJobOptionTypeSub').hide();
        }
        else if ($(this).attr('id') == 'MarketingReport' || $(this).attr('id') == 'salesman' || $(this).attr('id') == 'customer' || $(this).attr('id') == 'product' || $(this).attr('id') == 'agent') {
            $('#rptOption').hide();
            $('#rptJobOption').hide();
            $('#rptMarketingOption').show();
            $('#rptJobOptionType').hide();
            $('#rptJobOptionTypeSub').hide();
        }
        else if ($(this).attr('id') == 'InvoiceViseReport' || $(this).attr('id') == 'InvoiceDate' || $(this).attr('id') == 'JobCloseDate') {
            $('#rptJobOption').hide();
            $('#rptOption').hide();
            $('#rptMarketingOption').hide(); 
            $('#rptJobOptionType').show();
            $('#rptJobOptionTypeSub').hide();
            if ($(this).attr('id') == 'InvoiceDate') {
                $('#rptJobOptionTypeSub').show();
            }
            $('#AsAtDate').attr("disabled", "disabled")
        }
        else if($(this).attr('id') == 'InvoiceDatePending' || $(this).attr('id') == 'InvoiceDateClose')
        {
            $('#rptJobOption').hide();
            $('#rptOption').hide();
            $('#rptMarketingOption').hide();
            $('#rptJobOptionType').show();
            $('#rptJobOptionTypeSub').show();
            $('#AsAtDate').attr("disabled", "disabled")
        }
          
        else {
            $('#rptOption').hide();
            $('#rptJobOption').hide();
            $('#rptMarketingOption').hide();
            $('#rptJobOptionType').hide();
            $('#rptJobOptionTypeSub').hide();
        }
    });
});

jQuery(".btn-cust_search").click(function () {
    var headerKeys = Array()
    headerKeys = ["Row", "Code", "Name", "NIC", "Mobile", "BR No"];
    field = "cusCode2"
    var x = new CommonSearch(headerKeys, field);
});

jQuery(".inv-no-search").click(function () {

    var headerKeys = Array()
    headerKeys =["Row", "Invoice No", "Job No", "Date"];
    field = "InvoiceNoDfNew"
    data = jQuery("#Bl_h_doc_no").val();
    var x = new CommonSearchDateFiltered(headerKeys, field, data);
});

jQuery(".btn-manual-bl").click(function () {
    //click_status = "M";
    var headerKeys = Array()
    headerKeys = ["Row", "Master BL", "House BL", "Draft BL", "Date", "Job No"];
    field = "BLnoM"
    //var x = new CommonSearch(headerKeys, field);
    var x = new CommonSearchDateFiltered(headerKeys, field);
});

jQuery(".btn-house-bl").click(function () {
    //click_status = "H";
    var headerKeys = Array()
    headerKeys = ["Row", "Code","JobNo","Customer", "Agent", "Date"];
    field = "BL_H_DOC_NO"
    var x = new CommonSearchDateFiltered(headerKeys, field);
});

//jQuery("#ProfitCenter").on("keydown", function (evt) {
//    if (evt.keyCode == 113) {
//        var headerKeys = Array()
//        headerKeys = ["Row", "Code", "Description", "Channel"];
//        field = "ptycshsetpc";
//        var x = new CommonSearch(headerKeys, field);
//    }
//});

jQuery("#ProfitCenter").focusout(function () {
    var pc = jQuery(this).val();
    if (pc != "") {
        pcfocusout(pc);
    }
});

function pcfocusout(pc) {
    jQuery.ajax({
        type: "GET",
        url: "/Validation/validateProfitCenter?pccd=" + pc,
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.login == false) {
                Logout();
            } else {
                if (result.success == false) {
                    setInfoMsg(result.msg);
                    jQuery("#ProfitCenter").val("");
                    jQuery("#ProfitCenter").focus();
                }
                if (result.success == true) {
                    if (result.data.MPC_CD == null) {
                        setInfoMsg("Please enter valid profit center.");
                        jQuery("#ProfitCenter").val("");
                        jQuery("#ProfitCenter").focus();
                    } else {
                        jQuery(".pc-desc").empty();
                        jQuery(".pc-desc").html(result.data.MPC_DESC);
                    }
                }
            }
        }
    });
}