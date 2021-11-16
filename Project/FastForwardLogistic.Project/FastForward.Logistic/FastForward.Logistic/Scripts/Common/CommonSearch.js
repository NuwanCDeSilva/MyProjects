var first = true;
var first_time = true;
var headerKeys;
var field = "";
var parameterData = "";

function CommonSearch(headerKeys, selectedfield, data) {
    
    jQuery("#searpanel tbody").empty();
    jQuery("#KeyWord").val("");
    headerKeys = headerKeys;
    field = selectedfield;
    if (data != null) {
        parameterData = data;
    } else {
        parameterData = null;
    }
    
    if (headerKeys.length > 0) {
        var selecter = jQuery('#modalview .filter-key-cls');
        selecter.empty();
        for (i = 1; i < headerKeys.length; i++) {
            var newOption = jQuery('<option value="' + headerKeys[i] + '">' + headerKeys[i] + '</option>');
            selecter.append(newOption);
        }
        var head = jQuery('#modalview .table-responsive .table thead tr');
        head.empty();
        for (j = 0; j < headerKeys.length; j++) {
            var newHead = "";
            if (headerKeys[j].toUpperCase() == "ROW") {
                 newHead = jQuery('<th style="width:36px;">' + headerKeys[j].toUpperCase() + '</th>');
            } else {
                 newHead = jQuery('<th>' + headerKeys[j].toUpperCase() + '</th>');
            }
            head.append(newHead);
        }
        //first_time = false;
    }
    jQuery('body').css('cursor', 'wait');
    //jQuery(".se-pre-con").fadeIn("slow");
    var pgeNum = 1;
    var pgeSize = jQuery('.modal-content .cls-select-page-cont').val();
    var searchFld = jQuery('select.filter-key-cls').val();
    var searchVal = jQuery('input#KeyWord').val();
    loadDetails(pgeNum, pgeSize, searchFld, searchVal, headerKeys);
    
    return false;
}

function loadDetails(pgeNum, pgeSize, searchFld, searchVal, headerKeys, check) {
    
    if (field == "cusCode" || field == "cusCodecus") {
         newurl = "/Search/GetCustomerDetails"
    }
    if (field == "cusCodecus1") {
        newurl = "/Search/GetCustomerDetails"
    }
    //Dilshan on 22/08/2017*************
    if (field == "custyp") {
        newurl = "/Search/loadcustomertype"
    }
    if (field == "cusnic") {
        newurl = "/Search/GetCustomerDetails"
    }
    if (field == "cusbr") {
        newurl = "/Search/GetCustomerDetails"
    }
    if (field == "cuspassp") {
        newurl = "/Search/GetCustomerDetails"
    }
    if (field == "cusdl") {
        newurl = "/Search/GetCustomerDetails"
    }
    if (field == "custel") {
        newurl = "/Search/GetCustomerDetails"
    }
    if (field == "cusexecu") {
        newurl = "/Search/getEmployeeDetails"
    }
    if (field == "cusintroexe") {
        newurl = "/Search/getEmployeeDetails"
    }
    if (field == "presentCountry1") {
        newurl = "/Search/getCountry";
    }
    if (field == "perTown1") {
        newurl = "/Search/getTownwithCountry";
    }
    if (field == "perTownCus") {
        newurl = "/Search/getTownwithCountry";
    }
    if (field == "perDistrictCus") {
        newurl = "/Search/getDistrictwithCountry";
    }
    if (field == "perProvinceCus") {
        newurl = "/Search/getProvincewithCountry";
    }
    //Dilshan 31/08/2017
    if (field == "ProfitCenterGlobe") {
        newurl = "/Search/getUserProfitCenters";
    }
    if (field == "ProfitCenterGlobeCus") {
        newurl = "/Search/getUserProfitCenters";
    }
    if (field == "CompanyGlobe") {
        newurl = "/Search/getUserCompanySet";
    }
    if (field == "CompanyGlobeCus") {
        newurl = "/Search/getUserCompanySet";
    }
    // Added by Chathura on 13-Sep-2017
    if (field == "ModeOfShipment") { 
        
        newurl = "/Search/getModeOfShipment";
    }
    if (field == "ModeOfShipmentCus") {

        newurl = "/Search/getModeOfShipment";
    }
    //**********************************
    if (field == "cusCode2" || field == "cusCode9" || field == "cusCode10" || field == "cusCode11" || field == "cusCodeCp") {
        newurl = "/Search/GetCustomerDetails"
    }
    if (field == "cusCodetype") {
        newurl = "/Search/GetCustomerDetailsByType"
    }
    if (field == "cusCodeCpType") {
        newurl = "/Search/GetCustomerDetailsByType_New"
    }
    //if (field == "cusCode3" || field == "cusCode6" || field == "cusCode8") {
    //    newurl = "/Search/GetCustomerDetailsC"
    //}
    if (field == "cusCode9jn") {
        
        newurl = "/Search/GetCustomerDetailsByJobNo?jobno=" + parameterData
    }
    if (field == "cusCode3" || field == "cusCode8") {
        newurl = "/Search/GetCustomerDetailsC"
    }
    if (field == "cusCode8") {
        newurl = "/Search/GetServiceAgent"
    }
    if (field == "cusCodeJf") {
        newurl = "/Search/GetCustomerDetailsCJobFiltered?jobno=" + parameterData
    }
    if (field == "cusCode6") {
        newurl = "/Search/GetCustomerDetailsNP"
    }
    if (field == "cusCode4") {
        newurl = "/Search/GetSupplir"
    }
    if (field == "cusCode5") {
        newurl = "/Search/GetConsignee"
    }
    if (field == "cusCode7") {
        newurl = "/Search/GetAgent"
    }
    if (field == "jobno" || field == "jobno2" || field == "jobno3") {
        newurl = "/Search/GetJobNumber"
    }
    if (field == "BLno" || field == "BLno2") {
        newurl = "/Search/GetBLNumberD"
    }
      if (field == "BLnoH" || field == "BLno2H") {
        newurl = "/Search/GetBLNumberH"
        }
    if (field == "BLnoM" || field == "BLno2M") {
        newurl = "/Search/GetBLNumberM"
    }
    if (field == "UOM" || field == "UOM1" || field == "UOM2" || field == "UOM3" || field == "UOMPTY") {
        newurl = "/Search/GetUOM"
    }
    if (field == "BL_JOB_NO" || field == "BL_H_DOC_NO" || field == "BL_D_DOC_NO" || field == "BL_M_DOC_NO" || field == "BL_POUCH_NO" || field == "BL_TERMINAL" || field == "BL_MANUAL_D_REF" || field == "BL_MANUAL_H_REF" || field == "BL_MANUAL_M_REF" || field == "BL_H_DOC_NOALL" || field == "BL_MANUAL_H_REF1" || field == "BL_MANUAL_M_REF1") {
        newurl = "/Search/getAllSearch"
        pgeNum = pgeNum + "-" + field;
    }
    if (field == "BL_H_DOC_NO2E") {
        
        newurl = "/Search/getAllSearchByJobNo?jobno=" + parameterData;
        pgeNum = pgeNum + "-" + field;
    }
    if (field == "PORTS" || field == "PORTS2") {
        newurl = "/Search/getPorts"
    }
    if (field == "PORTSREF") {
        newurl = "/Search/getPortsRef"
    }
    if (field == "VESSEL") {
        newurl = "/Search/getVessels"
    }
    if (field == "VESSEL1") {
        newurl = "/Search/getVesselsRef"
    }
    if (field == "pouchnojob") {
        newurl = "/Search/getJobPouchSearch"
    }
    if (field == "pouchnojob2") {
        newurl = "/Search/getJobPouchSearch"
    }
    if (field == "pouchnojob3") {
        newurl = "/Search/getJobPouchSearch2"
    }
    if (field == "Mesure") {
    newurl = "/Search/GetMesureTp"
    }

    if (field == "ProfitCenter" || field == "ptycshsetpc") {
        newurl = "/Search/getUserProfitCenters";
    }
    if (field == "employee" || field == "employee2" || field == "employee3" || field == "employee4") {
        newurl = "/Search/getEmployeeDetails";
    }
    //dilshan on 26/05/2018***********
    if (field == "employeeex") {
        newurl = "/Search/getEmployeeDetailsEx";
    }
    //********************************
    if (field == "ptychsreq") {
        newurl = "/Search/getPtyChshReqDet?type=" + parameterData;
    }
    if (field == "consignee") {
        newurl = "/Search/getConsigneeDetails?type=" + parameterData;
    }
    if (field == "payconsignee") {
        newurl = "/Search/getConsigneeDetails?type=" + parameterData;
    }
    if (field == "payto") {
        newurl = "/Search/getConsigneeDetails?type=" + parameterData;
    }
    if (field == "ptyjob") {
        newurl = "/Search/getPettyCashJob";
    }
    if (field == "costele" || field == "costelem" || field == "costeleRev") {
        newurl = "/Search/getCostElement";
    }
    if (field == "revtele") {
        newurl = "/Search/getRevenueElements";
    }
    if (field == "costele1") {
        newurl = "/Search/getCostElementRef";
    }
    if (field == "curcd" || field == "curcd1" || field == "curcd2" || field == "curcd3") {
        newurl = "/Search/getCurrencyCodes";
    }
    if (field == "detserch") {
        newurl = "/Search/getTeVehLcDetails";
    }
    if (field == "InvoiceNo") {
        newurl = "/Search/getInvoiceNo";
    }
    if (field == "InvoiceNoCrd") {
        newurl = "/Search/getInvoiceNoCrd";
    }
    if (field == "InvoiceNo3") {
        newurl = "/Search/getDebtNo";
    }
    if (field == "InvoiceNo2") {
        newurl = "/Search/getInvoiceNoByCus?cus=" + parameterData.debt + "&othpc=" + parameterData.othpc;
    }
    if (field == "InvoiceNoRef") {
        newurl = "/Search/getInvoiceNo";
    }
    if (field == "setlmentno") {
        newurl = "/Search/getSettlementList";
    }
    if (field == "presentCountry") {
        newurl = "/Search/getCountry";
    }
    if (field == "perTown") {
        newurl = "/Search/getTownDetails";
    }
    if (field == "bnkAcc") {
        newurl = "/Search/getDepositedBanks";
    }
    if (field == "chkBnkAcc" || field == "credBnkAcc" || field == "debtBnkAcc") {
        newurl = "/Search/getBusComBanks";
    }
    if (field == "bnkChqBranch") {
        newurl = "/Search/getBankBranchs?bankcd=" + parameterData;
    }
    if (field == "divisionSearch") {
        newurl = "/Search/getDivisions";
    }
    if (field == "receiptTYypeSearch") {
        newurl = "/Search/getReceiptTypes";
    }
    if (field == "receiptSearch") {
        newurl = "/Search/getReceiptEntries?unallow=" + parameterData.chk + "&recTyp=" + parameterData.recTyp + "&customer=" + parameterData.customer;
    }
    if (field == "TY_OF_SHIPMNT") {
        newurl = "/Search/getShipmentType";
    }
    if (field == "purjobno") {
        newurl = "/Search/getJobNoByPouch?pouch=" + parameterData;
    }
    if (field == "custypsrch") {
        newurl = "/Search/loadcustomertype";
    }
    if (field == "paytypsrch") {
        newurl = "/Search/loadpaymenttype";
    }
    if (field == "creusrsrch") {
        newurl = "/Search/loadcreateuser";
    }
    if (field == "TY_OF_ROLE_ID") {
        newurl = "/Search/loadroleid?companyId=" + parameterData.company;
    }
    if (field == "TY_OF_ROLE_ID_VR") {
        newurl = "/Search/loadroleid?companyId=" + parameterData.company;
    }
    if (field == "OPTION_ID_DET") {
        newurl = "/Search/loadroleid?companyId=" + parameterData.company;
    }
    if (field == "TY_OF_ROLE_ID_GRANT") {
        newurl = "/Search/loadroleid?companyId=" + parameterData.company;
    }
    if (field == "USER_LIST") {
        newurl = "/Search/getUserList";
    }
    if (field == "DEPT_LIST") {
        newurl = "/Search/getDeptList";
    }
    if (field == "DESIG_LIST") {
        newurl = "/Search/getDesignationList";
    }
    if (field == "USER_LIST_ASI") {
        newurl = "/Search/getUserList";
    }
    if (field == "COMPANY_LIST_ASI") {
        newurl = "/Search/getCompanyList";
    }
    jQuery.ajax({
        cache: false,
        type: "GET",
        url: newurl,
        data: { pgeNum: pgeNum, pgeSize: pgeSize, searchFld: searchFld, searchVal: searchVal },
        success: function (result) {
            if (result.login == true) {
                if (result.success == true) {
                    if (result.data.length > 0) {
                        if (first == true) {
                            paging(result.totalDoc, pgeNum, true);
                            first = false;
                        }
                        setSerchPanel(result.data, headerKeys);
                        jQuery('#modalview').css('cursor', 'default');
                        jQuery('body').css('cursor', 'default');
                        //jQuery(".se-pre-con").fadeOut("slow");
                        //if (check == true) {
                        jQuery('#paging').empty();
                        paging(result.totalDoc, pgeNum, false);
                        check = false;
                        //}
                    }
                } else {
                    jQuery('#modalview').css('cursor', 'default');
                    jQuery('body').css('cursor', 'default');
                    //jQuery(".se-pre-con").fadeOut("slow");
                    jQuery('#paging').empty();
                    setSerchPanel(null, headerKeys);
                }
            } else {
                Logout();
            }
        }
    });

}
function setSerchPanel(tableValues, headerKeys) {
    //if (tableValues != null) {
    //    if (tableValues.length > 0) {
    if (jQuery('.table-responsive tbody').length > 0) {
        jQuery('.table-responsive tbody').empty();
    }
    if (tableValues != null) {
        if (tableValues.length > 0) {
            if (field == "cusCode" || field == "cusCodecus") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('table.cls-search-pnl').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].Mbe_cd +
                        '</td><td>' + tableValues[i].Mbe_name +
                        '</td><td>' + tableValues[i].Mbe_nic +
                        '</td><td>' + tableValues[i].Mbe_mob +
                        '</td><td>' + tableValues[i].Mbe_br_no + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Customer Search");

            }
            if (field == "cusCodecus1") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    var chk = "";
                    if (tableValues[i].SELECT == 1) {
                        chk = "checked";
                    }
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].Mbe_cd +
                        '</td><td>' + tableValues[i].Mbe_name +
                        '</td><td>' + tableValues[i].Mbe_nic +
                        '</td><td>' + tableValues[i].Mbe_mob +
                        '</td><td>' + tableValues[i].Mbe_br_no +
                        '<td>' + '<input type="checkbox" ' + chk + ' name="ccustomer" id="' + tableValues[i].Mbe_cd + '" value="' + tableValues[i].Mbe_name + '" onclick="addFiltersFront(this,\'ccustomer\');" >' + '</td></tr>'
                        );
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Customer Search");

            }
            //Dilshan on 22/08/2017*******************************
            if (field == "custyp") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('table.cls-search-pnl').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].met_trans_tp +
                        '</td><td>' + tableValues[i].met_desc + '</td></tr>');
                      //  '</td><td>' + tableValues[i].Mbe_nic + 
                      //  '</td><td>' + tableValues[i].Mbe_mob +
                      //  '</td><td>' + tableValues[i].Mbe_br_no + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Customer Type Search");

            }
            //Dilshan 31/08/2017
            if (field == "ProfitCenterGlobe") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].Mpc_cd +
                        '</td><td>' + tableValues[i].Mpc_desc +
                        '</td><td>' + tableValues[i].Mpc_add_1 +
                        //'</td><td>' + tableValues[i].Mpc_chnl +   // Commented by Chathura on 19-sep-2017
                        '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Profit Center Search");
            }
            if (field == "ProfitCenterGlobeCus") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    var chk = "";
                    if (tableValues[i].SELECT == 1) {
                        chk = "checked";
                    }
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].Mpc_cd +
                        '</td><td>' + tableValues[i].Mpc_desc +
                        '<td>' + '<input type="checkbox" ' + chk + ' name="cpc" id="' + tableValues[i].Mpc_cd + '" value="' + tableValues[i].Mpc_desc + '" onclick="addFiltersFront(this,\'cpc\');" >' + '</td></tr>'
                        );
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Profit Center Search");
            }
            //Added by Chathura on 13-Sep-2017
            if (field == "ModeOfShipment") {       
                
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].Mms_cd +
                        '</td><td>' + tableValues[i].Mms_desc + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Mode of Shipment");
            }
            if (field == "ModeOfShipmentCus") {

                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    var chk = "";
                    if (tableValues[i].SELECT == 1) {
                        chk = "checked";
                    }
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].Mms_cd +
                        '</td><td>' + tableValues[i].Mms_desc +
                        '<td>' + '<input type="checkbox" ' + chk + ' name="cmode" id="' + tableValues[i].Mms_cd + '" value="' + tableValues[i].Mms_desc + '" onclick="addFiltersFront(this,\'cmode\');" >' + '</td></tr>'
                        );
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Mode of Shipment");
            }

            if (field == "CompanyGlobe") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].Mc_cd +
                        '</td><td>' + tableValues[i].Mc_desc + '</td></tr>');
                        //'</td><td>' + tableValues[i].Mpc_add_1 +
                        //'</td><td>' + tableValues[i].Mpc_chnl + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Company Search");
            }
            if (field == "CompanyGlobeCus") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    var chk = "";
                    if (tableValues[i].SELECT == 1) {
                        chk = "checked";
                    }
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].Mc_cd +
                        '</td><td>' + tableValues[i].Mc_desc +
                        '<td>' + '<input type="checkbox" ' + chk + ' name="ccompany" id="' + tableValues[i].Mc_cd + '" value="' + tableValues[i].Mc_desc + '" onclick="addFiltersFront(this,\'ccompany\');" >' + '</td></tr>'
                        );
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Company Search");
            }

            if (field == "cusnic") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('table.cls-search-pnl').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].Mbe_cd +
                       // '</td><td>' + tableValues[i].Mbe_name +
                        '</td><td>' + tableValues[i].Mbe_nic + '</td></tr>');
                    //  '</td><td>' + tableValues[i].Mbe_mob +
                    //  '</td><td>' + tableValues[i].Mbe_br_no + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Customer NIC Search");

            }

            if (field == "cusbr") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('table.cls-search-pnl').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].Mbe_cd +
                       // '</td><td>' + tableValues[i].Mbe_name +
                       // '</td><td>' + tableValues[i].Mbe_nic + 
                    //  '</td><td>' + tableValues[i].Mbe_mob +
                      '</td><td>' + tableValues[i].Mbe_br_no + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Customer BR No Search");

            }

            if (field == "cuspassp") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('table.cls-search-pnl').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].Mbe_cd +
                        '</td><td>' + tableValues[i].Mbe_pp_no + '</td></tr>');
                    //  '</td><td>' + tableValues[i].Mbe_nic + 
                    //  '</td><td>' + tableValues[i].Mbe_mob +
                    //  '</td><td>' + tableValues[i].Mbe_br_no + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Customer Passport No Search");

            }

            if (field == "cusdl") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('table.cls-search-pnl').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].Mbe_cd +
                       // '</td><td>' + tableValues[i].Mbe_name +
                        '</td><td>' + tableValues[i].MbE_DL_NO + '</td></tr>');
                    //  '</td><td>' + tableValues[i].Mbe_mob +
                    //  '</td><td>' + tableValues[i].Mbe_br_no + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Customer Driving licence No Search");

            }

            if (field == "custel") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('table.cls-search-pnl').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].Mbe_cd +
                       // '</td><td>' + tableValues[i].Mbe_name +
                       // '</td><td>' + tableValues[i].Mbe_nic + 
                      '</td><td>' + tableValues[i].Mbe_mob + '</td></tr>');
                      //'</td><td>' + tableValues[i].Mbe_br_no + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Customer Mobile No Search");

            }

            //if (field == "cusexecu") {
            //    for (i = 0; i < tableValues.length; i++) {
            //        var a = "";
            //        (i % 2 == 1) ? a = 'class="coloured"' : "";
            //        jQuery('table.cls-search-pnl').append('<tr ' +a + '><td style="width:36px;">' + tableValues[i].R__ +
            //            '</td><td>' + tableValues[i].Mbe_cd +
            //            '</td><td>' + tableValues[i].Mbe_name + 
            //            '</td><td>' + tableValues[i].Mbe_nic + 
            //            '</td><td>' + tableValues[i].Mbe_mob + '</td></tr>');
            //        //  '</td><td>' + tableValues[i].Mbe_br_no + '</td></tr>');
            //    }
            //    jQuery(".serch-panel-title").empty();
            //    jQuery(".serch-panel-title").html("Introduce Executive Officer Search");

            //}

            if (field == "cusexecu" || field == "cusintroexe") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('table.cls-search-pnl').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].ESEP_EPF +
                        '</td><td>' + tableValues[i].ESEP_CAT_SUBCD +
                        '</td><td>' + tableValues[i].ESEP_FIRST_NAME +
                        '</td><td>' + tableValues[i].ESEP_LAST_NAME +
                        '</td><td>' + tableValues[i].ESEP_NIC + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Employee Search");
            }
            //dilshan on 26/01/2018
            if (field == "cusCodetype" || field == "cusCodeCpType") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('table.cls-search-pnl').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].Mbe_name +
                        '</td><td>' + tableValues[i].Mbe_cd +
                        '</td><td>' + tableValues[i].Mbe_nic +
                        '</td><td>' + tableValues[i].Mbe_mob +
                        '</td><td>' + tableValues[i].Mbe_br_no + '</td></tr>');
                }
            }
            //***********************************************
            if (field == "cusCode2" || field == "cusCode9" || field == "cusCode10" || field == "cusCode11" || field == "cusCodeCp") {
                if (field == "cusCode2" || field == "cusCode11" || field == "cusCodeCp") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('table.cls-search-pnl').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].Mbe_name +
                            '</td><td>' + tableValues[i].Mbe_cd +
                            '</td><td>' + tableValues[i].Mbe_nic +
                            '</td><td>' + tableValues[i].Mbe_mob +
                            '</td><td>' + tableValues[i].Mbe_br_no + '</td></tr>');
                    }
                }
                else {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('table.cls-search-pnl').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].Mbe_cd +
                            '</td><td>' + tableValues[i].Mbe_name +
                            '</td><td>' + tableValues[i].Mbe_nic +
                            '</td><td>' + tableValues[i].Mbe_mob +
                            '</td><td>' + tableValues[i].Mbe_br_no + '</td></tr>');
                    }
                }
                
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Customer Search");

            }
            if (field == "cusCode9jn") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('table.cls-search-pnl').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                         '</td><td>' + tableValues[i].Mbe_name +
                        '</td><td>' + tableValues[i].Mbe_cd +                       
                        '</td><td>' + tableValues[i].Mbe_nic +
                        '</td><td>' + tableValues[i].Mbe_mob +
                        '</td><td>' + tableValues[i].Mbe_br_no + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Customer Search");

            }
            if (field == "cusCode3" || field == "cusCode4" || field == "cusCode5" || field == "cusCode6" || field == "cusCode7" || field == "cusCode8") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('table.cls-search-pnl').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].Mbe_name +
                        '</td><td>' + tableValues[i].Mbe_cd +
                        '</td><td>' + tableValues[i].Mbe_nic +
                        '</td><td>' + tableValues[i].Mbe_mob +
                        '</td><td>' + tableValues[i].Mbe_br_no + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                if (field == "cusCode4") {
                    jQuery(".serch-panel-title").html("Shipper Search");
                }
                else if (field == "cusCode5") {
                    jQuery(".serch-panel-title").html("Consignee Search");
                }
                else if (field == "cusCode6") {
                    jQuery(".serch-panel-title").html("Notify party Search");
                }
                else if (field == "cusCode7") {
                    jQuery(".serch-panel-title").html("Agent Search");
                }
                else if (field == "cusCode8") {
                    jQuery(".serch-panel-title").html("Carrier/Shipping Line Search");
                }
                else {
                    jQuery(".serch-panel-title").html("Customer Search");
                }
                

            }
            if (field == "cusCodeJf") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('table.cls-search-pnl').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].Mbe_name +
                        '</td><td>' + tableValues[i].Mbe_cd +                        
                        '</td><td>' + tableValues[i].Mbe_nic +
                        '</td><td>' + tableValues[i].Mbe_mob +
                        '</td><td>' + tableValues[i].Mbe_br_no + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Customer Search");

            }
            if (field == "jobno" || field == "jobno2" || field == "jobno3") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('table.cls-search-pnl').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].JB_JB_NO +
                         '</td><td>' + tableValues[i].JB_POUCH_NO +
                        '</td><td>' + getFormatedDate1(tableValues[i].JB_JB_DT) +
                        '</td><td>' + tableValues[i].JB_STUS + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Job Num Search");

            }
             if (field == "BLno" || field == "BLno2" || field == "BLnoH" || field == "BLno2H" || field == "BLnoM" || field == "BLno2M") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('table.cls-search-pnl').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].DOC_NO +
                        '</td><td>' + getFormatedDate1(tableValues[i].DOC_DT) + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("BL Num Search");

            }
            if (field == "UOM" || field == "UOM1" || field == "UOM2" || field == "UOM3" || field == "UOMPTY") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('table.cls-search-pnl').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].PT_TP_CD +
                        '</td><td>' + tableValues[i].PT_DESC + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("UOM Search");

             }
             if (field == "Mesure") {
                 for (i = 0; i < tableValues.length; i++) {
                     var a = "";
                     (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('table.cls-search-pnl').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                         '</td><td>' + tableValues[i].mt_cd +
                         '</td><td>' + tableValues[i].mt_desc + '</td></tr>');
                 }
                 jQuery(".serch-panel-title").empty();
                 jQuery(".serch-panel-title").html("UOM Search");

             }
             if (field == "purjobno") {
                 for (i = 0; i < tableValues.length; i++) {
                     var a = "";
                     (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('table.cls-search-pnl').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                         '</td><td>' + tableValues[i].JB_JB_NO + '</td></tr>');
                 }
                 jQuery(".serch-panel-title").empty();
                 jQuery(".serch-panel-title").html("Job No Search");

             }
             if (field == "PORTS" || field == "PORTS2" || field == "PORTSREF") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('table.cls-search-pnl').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].PA_PRT_NAME +
                        '</td><td>' + tableValues[i].PA_PRT_CD + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Ports Search");

            }
            if (field == "BL_JOB_NO" || field == "BL_H_DOC_NO" || field == "BL_D_DOC_NO" || field == "BL_M_DOC_NO" || field == "BL_POUCH_NO" || field == "BL_TERMINAL" || field == "BL_MANUAL_D_REF" || field == "BL_MANUAL_H_REF" || field == "BL_H_DOC_NO2" || field == "pouchnojob" || field == "BL_MANUAL_M_REF" || field == "BL_H_DOC_NOALL") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('table.cls-search-pnl').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].CODE + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                if (field == "BL_MANUAL_D_REF") {
                    jQuery(".serch-panel-title").html("Manual Draft B/L Search");
                }
                else if (field == "BL_MANUAL_H_REF") {
                    jQuery(".serch-panel-title").html("Manual House B/L Search");
                }
                //else if (field == "BL_MANUAL_H_REF1") {
                //    jQuery(".serch-panel-title").html("Manual House B/L Search");
                //}
                else if (field == "BL_MANUAL_M_REF") {
                    jQuery(".serch-panel-title").html("Manual Master B/L Search");
                }
                else if (field == "BL_TERMINAL") {
                    jQuery(".serch-panel-title").html("Terminal ID Search");
                }
                else {
                    jQuery(".serch-panel-title").html(" Search");
                }
                

            }
            if (field == "BL_MANUAL_H_REF1") {

                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('table.cls-search-pnl').append('<tr ' + a + '><td class="search-box-rownum-td">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].CODE +
                        '</td><td>' + tableValues[i].BL + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Manual House B/L Search");

            }
            if (field == "BL_MANUAL_M_REF1") {

                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('table.cls-search-pnl').append('<tr ' + a + '><td class="search-box-rownum-td">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].CODE +
                        '</td><td>' + tableValues[i].BL + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Manual Master B/L Search");

            }
            if (field == "BL_H_DOC_NO2E") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('table.cls-search-pnl').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].CODE + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html(" Search");

            }
            if (field == "VESSEL" || field == "VESSEL1") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('table.cls-search-pnl').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].VM_VESSAL_NAME +
                        '</td><td>' + tableValues[i].VM_VESSAL_CD + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Vessel Search");

            }
            if (field == "pouchnojob2") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('table.cls-search-pnl').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].CODE + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Pouch No Search");

            }
            if (field == "pouchnojob3") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('table.cls-search-pnl').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].CODE + '</td><td>'
                      + tableValues[i].JOBNO + ' </td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Pouch ID Search");

            }
            if (field == "ProfitCenter" || field == "ptycshsetpc") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].Mpc_cd +
                        '</td><td>' + tableValues[i].Mpc_desc +
                        '</td><td>' + tableValues[i].Mpc_chnl + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Profit Center Search");
            }
            if (field == "employee" || field == "employee2" || field == "employee3" || field == "employee4") {
                if (field == "employee") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].ESEP_FIRST_NAME +
                            '</td><td>' + tableValues[i].ESEP_LAST_NAME +
                            '</td><td>' + tableValues[i].ESEP_EPF +
                            '</td><td>' + tableValues[i].ESEP_CAT_SUBCD +                            
                            '</td><td>' + tableValues[i].ESEP_NIC + '</td></tr>');
                    }
                }
                else {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].ESEP_EPF +
                            '</td><td>' + tableValues[i].ESEP_CAT_SUBCD +
                            '</td><td>' + tableValues[i].ESEP_FIRST_NAME +
                            '</td><td>' + tableValues[i].ESEP_LAST_NAME +
                            '</td><td>' + tableValues[i].ESEP_NIC + '</td></tr>');
                    }
                }
                
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Employee Search");
            }
            //dilshan
            if (field == "employeeex") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].ESEP_EPF +
                            '</td><td>' + tableValues[i].ESEP_CAT_SUBCD +
                            '</td><td>' + tableValues[i].ESEP_FIRST_NAME +
                            '</td><td>' + tableValues[i].ESEP_LAST_NAME +
                            '</td><td>' + tableValues[i].ESEP_NIC + '</td></tr>');                    
                }
            
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Employee Search");
            }
            //***********
            if (field == "ptychsreq") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].TPRH_REQ_NO +
                        '</td><td>' + tableValues[i].TPRH_MANUAL_REF +
                        '</td><td>' + getFormatedDateInput(tableValues[i].TPRH_REQ_DT) + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Request Search");
            }
            if (field == "consignee" || field == "payto") {
                if (field == "payto") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].Mbe_name +
                            '</td><td>' + tableValues[i].Mbe_cd +
                            '</td><td>' + tableValues[i].Mbe_acc_cd +                            
                            '</td><td>' + tableValues[i].Mbe_add1 +
                            '</td><td>' + tableValues[i].Mbe_mob + '</td></tr>');
                    }
                }
                else {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].Mbe_cd +
                            '</td><td>' + tableValues[i].Mbe_acc_cd +
                            '</td><td>' + tableValues[i].Mbe_name +
                            '</td><td>' + tableValues[i].Mbe_add1 +
                            '</td><td>' + tableValues[i].Mbe_mob + '</td></tr>');
                    }
                }
                
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Consignee Search");
                if (field == "payto") {
                    jQuery(".serch-panel-title").html("Payment Party Search");
                }
            }
            if (field == "payconsignee") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].Mbe_name +
                        '</td><td>' + tableValues[i].Mbe_cd +
                        '</td><td>' + tableValues[i].Mbe_acc_cd +                        
                        '</td><td>' + tableValues[i].Mbe_add1 +
                        '</td><td>' + tableValues[i].Mbe_mob + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Consignee Search");
            }
            if (field == "ptyjob") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].JB_JB_NO +
                        '</td><td>' + getFormatedDateInput(tableValues[i].JB_JB_DT) +
                        '</td><td>' + tableValues[i].JB_POUCH_NO + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Job Search");
            }
            if (field == "costele" || field == "costelem" || field == "costele1" || field == "revtele" || field == "costeleRev") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].MCE_DESC +
                        '</td><td>' + tableValues[i].MCE_CD +
                        '</td><td>' + tableValues[i].MCE_ACC_CD + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Cost Element Search");
            }
            if (field == "curcd" || field == "curcd1" || field == "curcd2" || field == "curcd3") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].MCR_CD +
                        '</td><td>' + tableValues[i].MCR_DESC + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Currency Code Search");
            } 
            if (field == "detserch") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].FVN_CD +
                        '</td><td>' + tableValues[i].FVN_DESC + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Veh/LC/Tel Search");
            }
            if (field == "InvoiceNo") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].TIH_INV_NO +
                        '</td><td>' + getFormatedDateInput(tableValues[i].TIH_INV_DT) + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Invoice No Search");
            }
            if (field == "InvoiceNoCrd") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].TIH_INV_NO +
                        '</td><td>' + getFormatedDateInput(tableValues[i].TIH_INV_DT) + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Credit Note No Search");
            }
            if (field == "InvoiceNo3") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].TIH_INV_NO +
                        '</td><td>' + getFormatedDateInput(tableValues[i].TIH_INV_DT) + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Debit Note Search");
            }
            if (field == "InvoiceNo2") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].TIH_INV_NO +
                        '</td><td>' + tableValues[i].TIH_PC_CD +
                        '</td><td>' + tableValues[i].TIH_BAL_SETTLE_AMT +
                        '</td><td>' + getFormatedDateInput(tableValues[i].TIH_INV_DT) + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Invoice No Search");
            }
            if (field == "InvoiceNoRef") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].TIH_INV_NO +
                        '</td><td>' + getFormatedDateInput(tableValues[i].TIH_INV_DT) + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Other Ref No Search");
            }
            if (field == "setlmentno") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].TPSH_SETTLE_NO +
                        '</td><td>' + tableValues[i].TPSH_MAN_REF +
                        '</td><td>' + getFormatedDateInput(tableValues[i].TPSH_SETTLE_DT) + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Request Settlement Search");
            }
            if (field == "presentCountry" || field == "presentCountry1") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].MCU_CD +
                        '</td><td>' + tableValues[i].MCU_DESC +
                        '</td><td>' + tableValues[i].MCU_REGION_CD +
                        '</td><td>' + tableValues[i].MCU_CAPITAL + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Country Search");
             }

            if (field == "perTown" || field == "perTown1") {
                 for (i = 0; i < tableValues.length; i++) {
                     var a = "";
                     (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                         '</td><td>' + tableValues[i].mt_desc +
                         '</td><td>' + tableValues[i].mdis_desc +
                         '</td><td>' + tableValues[i].mpro_desc +
                         '</td><td>' + tableValues[i].mt_cd + '</td></tr>');
                 }
                 jQuery(".serch-panel-title").empty();
                 jQuery(".serch-panel-title").html("Town Search");
            }
            if (field == "perTownCus") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    var chk = "";
                    if (tableValues[i].SELECT == 1) {
                        chk = "checked";
                    }
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].mt_desc +
                        '</td><td>' + tableValues[i].mdis_desc +
                        '</td><td>' + tableValues[i].mpro_desc +
                        '</td><td>' + tableValues[i].mt_cd +
                        '<td>' + '<input type="checkbox" ' + chk + ' name="ctown" id="' + tableValues[i].mt_cd + '" value="' + tableValues[i].mt_desc + '" onclick="addFiltersFront(this,\'ctown\');" >' + '</td></tr>'
                        );
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Town Search");
             }
            if (field == "perDistrictCus") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    var chk = "";
                    if (tableValues[i].SELECT == 1) {
                        chk = "checked";
                    }
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].mdis_desc +
                        '</td><td>' + tableValues[i].mpro_desc +
                        '</td><td>' + tableValues[i].mdis_cd +
                        '<td>' + '<input type="checkbox" ' + chk + ' name="cdistrict" id="' + tableValues[i].mdis_cd + '" value="' + tableValues[i].mdis_desc + '" onclick="addFiltersFront(this,\'cdistrict\');" >' + '</td></tr>'
                        );
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("District Search");
            }
            if (field == "perProvinceCus") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    var chk = "";
                    if (tableValues[i].SELECT == 1) {
                        chk = "checked";
                    }
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].mpro_cd +
                        '</td><td>' + tableValues[i].mpro_desc +
                        '<td>' + '<input type="checkbox" ' + chk + ' name="cprovince" id="' + tableValues[i].mpro_cd + '" value="' + tableValues[i].mpro_desc + '" onclick="addFiltersFront(this,\'cprovince\');" >' + '</td></tr>'
                        );
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Province Search");
            }
             if (field == "bnkAcc") {
                 for (i = 0; i < tableValues.length; i++) {
                     var a = "";
                     (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                         '</td><td>' + tableValues[i].MSBA_ACC_CD +
                         '</td><td>' + tableValues[i].MSBA_ACC_DESC + '</td></tr>');
                 }
                 jQuery(".serch-panel-title").empty();
                 jQuery(".serch-panel-title").html("Bank Search");
             }
             if (field == "chkBnkAcc" || field == "credBnkAcc" || field == "debtBnkAcc") {
                 for (i = 0; i < tableValues.length; i++) {
                     var a = "";
                     (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                         '</td><td>' + tableValues[i].MBI_ID +
                         '</td><td>' + tableValues[i].MBI_DESC +
                         '</td><td>' + tableValues[i].MBI_CD + '</td></tr>');
                 }
                 jQuery(".serch-panel-title").empty();
                 jQuery(".serch-panel-title").html("Bank Search");
             }
             if (field == "bnkChqBranch") {
                 for (i = 0; i < tableValues.length; i++) {
                     var a = "";
                     (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                         '</td><td>' + tableValues[i].MBB_CD +
                         '</td><td>' + tableValues[i].MBB_DESC + '</td></tr>');
                 }
                 jQuery(".serch-panel-title").empty();
                 jQuery(".serch-panel-title").html("Bank Branch Search");
             }
             if (field == "receiptTYypeSearch") {
                 for (i = 0; i < tableValues.length; i++) {
                     var a = "";
                     (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                         '</td><td>' + tableValues[i].MSRT_CD +
                         '</td><td>' + tableValues[i].MSRT_DESC + '</td></tr>');
                 }
                 jQuery(".serch-panel-title").empty();
                 jQuery(".serch-panel-title").html("Receipt Type Search");
             }
             if (field == "TY_OF_SHIPMNT") {
                 for (i = 0; i < tableValues.length; i++) {
                     var a = "";
                     (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                         '</td><td>' + tableValues[i].CODE +
                         '</td><td>' + tableValues[i].DESCRIPTION + '</td></tr>');
                 }
                 jQuery(".serch-panel-title").empty();
                 jQuery(".serch-panel-title").html("Shipment Type Search");
             }
             if (field == "receiptSearch") {
                 for (i = 0; i < tableValues.length; i++) {
                     var a = "";
                     (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                         '</td><td>' + tableValues[i].SAR_RECEIPT_NO +
                         '</td><td>' + tableValues[i].SAR_MANUAL_REF_NO +
                         '</td><td>' + tableValues[i].SAR_RECEIPT_DATE +
                         '</td><td>' + tableValues[i].SAR_ANAL_3 + '</td></tr>');
                 }

                 jQuery(".serch-panel-title").empty();
                 jQuery(".serch-panel-title").html("Receipt Search");
             }
             if (field == "divisionSearch") {
                 for (i = 0; i < tableValues.length; i++) {
                     var a = "";
                     (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                         '</td><td>' + tableValues[i].MSRD_CD +
                         '</td><td>' + tableValues[i].MSRD_DESC + '</td></tr>');
                 }
                 jQuery(".serch-panel-title").empty();
                 jQuery(".serch-panel-title").html("Company Search");
             }
             if (field == "custypsrch") {
                 for (i = 0; i < tableValues.length; i++) {
                     var a = "";
                     (i % 2 == 1) ? a = 'class="coloured"' : "";
                     jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                         '</td><td>' + tableValues[i].met_trans_tp +
                         '</td><td>' + tableValues[i].met_desc + '</td></tr>');
                 }
                 jQuery(".serch-panel-title").empty();
                 jQuery(".serch-panel-title").html("Customer Type Search");
             }
             if (field == "paytypsrch") {
                 for (i = 0; i < tableValues.length; i++) {
                     var a = "";
                     (i % 2 == 1) ? a = 'class="coloured"' : "";
                     jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                         '</td><td>' + tableValues[i].sapt_cd +
                         '</td><td>' + tableValues[i].sapt_desc + '</td></tr>');
                 }
                 jQuery(".serch-panel-title").empty();
                 jQuery(".serch-panel-title").html("Payment Type Search");
             }

             if (field == "creusrsrch") {
                 for (i = 0; i < tableValues.length; i++) {
                     var a = "";
                     (i % 2 == 1) ? a = 'class="coloured"' : "";
                     jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                         '</td><td>' + tableValues[i].se_usr_id +
                         '</td><td>' + tableValues[i].se_usr_name + '</td></tr>');
                 }
                 jQuery(".serch-panel-title").empty();
                 jQuery(".serch-panel-title").html("Created User Search");
             }
            if (field == "TY_OF_ROLE_ID") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].RoleID +
                        '</td><td>' + tableValues[i].RoleDescription +
                        '</td><td class="hidecolumn">' + tableValues[i].RoleName +
                        '</td><td class="hidecolumn">' + tableValues[i].ComapanyName +
                        '</td><td class="hidecolumn">' + tableValues[i].ActiveStatus +
                        '</td></tr>');
                }
                jQuery(".hidecolumn").css({ "display": "none" });
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("User Role Search");
            }
            if (field == "TY_OF_ROLE_ID_GRANT") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].RoleID +
                        '</td><td>' + tableValues[i].RoleDescription +
                        '</td><td class="hidecolumn">' + tableValues[i].RoleName +
                        '</td><td class="hidecolumn">' + tableValues[i].ComapanyName +
                        '</td><td class="hidecolumn">' + tableValues[i].ActiveStatus +
                        '</td></tr>');
                }
                jQuery(".hidecolumn").css({ "display": "none" });
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("User Role Search");
            }
            if (field == "TY_OF_ROLE_ID_VR") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].RoleID +
                        '</td><td>' + tableValues[i].RoleDescription +
                        '</td><td class="hidecolumn">' + tableValues[i].RoleName +
                        '</td><td class="hidecolumn">' + tableValues[i].ComapanyName +
                        '</td></tr>');
                }
                jQuery(".hidecolumn").css({ "display": "none" });
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("User Role Search");
            }
            if (field == "OPTION_ID_DET") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].RoleID +
                        '</td><td>' + tableValues[i].RoleDescription +
                        '</td><td class="hidecolumn">' + tableValues[i].RoleName +
                        '</td><td class="hidecolumn">' + tableValues[i].ComapanyName +
                        '</td><td class="hidecolumn">' + tableValues[i].ActiveStatus +
                        '</td></tr>');
                }
                jQuery(".hidecolumn").css({ "display": "none" });
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("User Role Search");
            }
            if (field == "USER_LIST") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +   //0
                        '</td><td>' + tableValues[i].Se_usr_id +   //1
                        '</td><td>' + tableValues[i].Se_usr_desc + //2
                        '</td><td class="hidecolumn">' + tableValues[i].Se_usr_name +//3
                        '</td><td class="hidecolumn">' + tableValues[i].Se_usr_pw +//4
                        '</td><td class="hidecolumn">' + tableValues[i].Se_usr_cat +//5
                        '</td><td class="hidecolumn">' + tableValues[i].Se_dept_id +//6
                        '</td><td class="hidecolumn">' + tableValues[i].Se_emp_id +//7
                        '</td><td class="hidecolumn">' + tableValues[i].Se_emp_cd +//8
                        '</td><td class="hidecolumn">' + tableValues[i].Se_isdomain +//9
                        '</td><td class="hidecolumn">' + tableValues[i].Se_iswinauthend +//10
                        '</td><td class="hidecolumn">' + tableValues[i].Se_domain_id +//11
                        '</td><td class="hidecolumn">' + tableValues[i].Se_SUN_ID +//12
                        '</td><td class="hidecolumn">' + tableValues[i].se_Email +//13
                        '</td><td class="hidecolumn">' + tableValues[i].se_Mob +//14
                        '</td><td class="hidecolumn">' + tableValues[i].se_Phone +//15
                        '</td><td class="hidecolumn">' + tableValues[i].Se_act +//16
                        '</td><td class="hidecolumn">' + tableValues[i].Se_ischange_pw +//17
                        '</td><td class="hidecolumn">' + tableValues[i].Se_pw_mustchange +//18
                        '</td><td class="hidecolumn">' + tableValues[i].Se_act_rmk +//19
                        '</td><td class="hidecolumn">' + tableValues[i].Ad_title +//20
                        '</td><td class="hidecolumn">' + tableValues[i].Ad_full_name +//21
                        '</td><td class="hidecolumn">' + tableValues[i].Ad_department +//22
                        '</td></tr>');
                }
                console.log(tableValues);
                jQuery(".hidecolumn").css({ "display": "none" });
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("User Search");
            }

            if (field == "USER_LIST_ASI") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +   //0
                        '</td><td>' + tableValues[i].Se_usr_id +   //1
                        '</td><td>' + tableValues[i].Se_usr_desc + //2
                        '</td><td class="hidecolumn">' + tableValues[i].Se_usr_name +//3
                        '</td><td class="hidecolumn">' + tableValues[i].Se_usr_pw +//4
                        '</td><td class="hidecolumn">' + tableValues[i].Se_usr_cat +//5
                        '</td><td class="hidecolumn">' + tableValues[i].Se_dept_id +//6
                        '</td><td class="hidecolumn">' + tableValues[i].Se_emp_id +//7
                        '</td><td class="hidecolumn">' + tableValues[i].Se_emp_cd +//8
                        '</td><td class="hidecolumn">' + tableValues[i].Se_isdomain +//9
                        '</td><td class="hidecolumn">' + tableValues[i].Se_iswinauthend +//10
                        '</td><td class="hidecolumn">' + tableValues[i].Se_domain_id +//11
                        '</td><td class="hidecolumn">' + tableValues[i].Se_SUN_ID +//12
                        '</td><td class="hidecolumn">' + tableValues[i].se_Email +//13
                        '</td><td class="hidecolumn">' + tableValues[i].se_Mob +//14
                        '</td><td class="hidecolumn">' + tableValues[i].se_Phone +//15
                        '</td><td class="hidecolumn">' + tableValues[i].Se_act +//16
                        '</td><td class="hidecolumn">' + tableValues[i].Se_ischange_pw +//17
                        '</td><td class="hidecolumn">' + tableValues[i].Se_pw_mustchange +//18
                        '</td><td class="hidecolumn">' + tableValues[i].Se_act_rmk +//19
                        '</td></tr>');
                }
                $("#company-details-table").find("tr:gt(0)").remove();
                console.log(tableValues);
                jQuery(".hidecolumn").css({ "display": "none" });
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("User Search");
            }

            if (field == "DEPT_LIST") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +   //0
                        '</td><td>' + tableValues[i].Departcode +   //1
                        '</td><td>' + tableValues[i].DepartDesc + //2
                        '</td><td>' + tableValues[i].DepartHead + //2
                        '</td></tr>');
                }
                jQuery(".hidecolumn").css({ "display": "none" });
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Department Search");
            }

            if (field == "COMPANY_LIST_ASI") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +   //0
                        '</td><td>' + tableValues[i].Mc_cd +   //1
                        '</td><td>' + tableValues[i].Mc_desc + //2
                        '</td></tr>');
                }
                jQuery(".hidecolumn").css({ "display": "none" });
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Company Search");
            }

            if (field == "DESIG_LIST") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td style="width:36px;">' + tableValues[i].R__ +   //0
                        '</td><td>' + tableValues[i].DesignationCat +   //1
                        '</td><td>' + tableValues[i].DesignationDesc + //2
                        '</td></tr>');
                }
                jQuery(".hidecolumn").css({ "display": "none" });
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Department Search");
            }
        }
    }
    else {
        if (jQuery('.table-responsive tbody').length > 0) {
            jQuery('.table-responsive tbody').append("<tr><td style=' border:none; color: #ff6666; position: absolute; width: 100%; font-weight: bold;'>No data found for this search criteria.</td></tr>");
        }
    }
        jQuery('#modalview').modal({
            keyboard: false,
            backdrop: 'static'
        }, 'show');
        jQuery('tr', '#modalview table tbody').click(function () {
            jQuery('tr', '#modalview table tbody').removeClass('selected');
            jQuery('tr', '#modalview table tbody').css('color', 'black');
            jQuery(this).addClass('selected');
            jQuery(this).css('color', 'blue');
            setValue(field);
        });
    //jQuery('tr', '#modalview table tbody').dblclick(function () {
    //    setValue(field);
    //});

   }
    function setValue(field) {
        if (jQuery("#modalview").is(":visible") == true) {
            if (jQuery('tr.selected td', '#modalview table tbody').length > 0) {
                if (field == "cusCode") {
                    jQuery('#MBE_CD').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#MBE_CD').val(value);
                    //ALERT(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#MBE_CD').focus();
                }
                //Dilshan on 23/08/2017
                if (field == "custyp") {
                    jQuery('#MBE_TP').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#MBE_TP').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#MBE_TP').focus();
                }
                //Dilshan on 31/08/2017 
                if (field == "ProfitCenterGlobe") {
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery.ajax({
                        cache: false,
                        async: true,
                        type: "POST",
                        url: "/Home/ChangeProfitCenter",
                        data: { pc: value },
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    location.reload();
                                } else {
                                    if (result.type == "Error") {
                                        setError(result.msg);
                                    } else if (result.type == "Info") {
                                        setInfoMsg(result.msg);
                                    }
                                }

                            } else {
                                Logout();
                            }
                        }
                    });
                }

                if (field == "CompanyGlobe") {
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery.ajax({
                        cache: false,
                        async: true,
                        type: "POST",
                        url: "/Home/ChangeCompany",
                        data: { pc: value },
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    location.reload();
                                } else {
                                    if (result.type == "Error") {
                                        setError(result.msg);
                                    } else if (result.type == "Info") {
                                        setInfoMsg(result.msg);
                                    }
                                }

                            } else {
                                Logout();
                            }
                        }
                    });
                }

                // Added by Chathura 13-Sep-2017
                if (field == "ModeOfShipment") { 
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery.ajax({
                        cache: false,
                        async: true,
                        type: "POST",
                        url: "/Home/ChangeModeOfShipment",
                        data: { pc: value },
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    location.reload();
                                } else {
                                    if (result.type == "Error") {
                                        setError(result.msg);
                                    } else if (result.type == "Info") {
                                        setInfoMsg(result.msg);
                                    }
                                }

                            } else {
                                Logout();
                            }
                        }
                    });
                }

                if (field == "cusbr") {
                    jQuery('#MBE_CD').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#MBE_CD').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#MBE_CD').focus();
                }
                if (field == "cuspassp") {
                    jQuery('#MBE_CD').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#MBE_CD').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#MBE_CD').focus();
                }
                if (field == "cusdl") {
                    jQuery('#MBE_CD').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#MBE_CD').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#MBE_CD').focus();
                }
                if (field == "custel") {
                    jQuery('#MBE_CD').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#MBE_CD').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#MBE_CD').focus();
                }
                if (field == "cusnic") {
                    jQuery('#MBE_CD').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#MBE_CD').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#MBE_CD').focus();
                }

                if (field == "cusexecu") {
                    jQuery('#cusintroex_id').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#cusintroex_id').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#cusintroex_id').focus();
                }

                if (field == "cusintroexe") {
                    jQuery('#MBE_INTRO_EX').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#MBE_INTRO_EX').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#MBE_INTRO_EX').focus();
                }

                if (field == "cusCode2") {
                    jQuery('#cus_cd').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(2)').text();
                    jQuery('#cus_cd').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#cus_cd').focus();
                }

                if (field == "cusCodetype") {
                    jQuery('#cus_cd').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(2)').text();
                    jQuery('#cus_cd').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#cus_cd').focus();
                }

                if (field == "cusCode3") {
                    jQuery('#Bl_cus_cd').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(2)').text();
                    var name = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Bl_cus_cd').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Bl_cus_cd').focus();
                    jQuery('.tbl-cus-name .new-row').remove();
                    jQuery('.tbl-cus-name').append('<tr class="new-row">' +
                         '<td>' + name + '</td>' + '</tr>'
                        );
                }
                if (field == "cusCodeJf") {
                    jQuery('#Bl_cus_cd').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(2)').text();
                    var name = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Bl_cus_cd').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Bl_cus_cd').focus();
                    jQuery('.tbl-cus-name .new-row').remove();
                    jQuery('.tbl-cus-name').append('<tr class="new-row">' +
                         '<td>' + name + '</td>' + '</tr>'
                        );
                }
                if (field == "jobno") {
                    jQuery('#Jb_jb_no').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Jb_jb_no').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Jb_jb_no').focus();
                }
                if (field == "BLno") {
                    jQuery('#Bl_d_doc_no').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Bl_d_doc_no').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Bl_d_doc_no').focus();
                }
                if (field == "UOM") {
                    jQuery('#bld_grs_weight_uom').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#bld_grs_weight_uom').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#bld_grs_weight_uom').focus();
                }
                if (field == "UOM1") {
                    jQuery('#bld_net_weight_uom').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#bld_net_weight_uom').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#bld_net_weight_uom').focus();
                }
                if (field == "UOM2") {
                    jQuery('#bld_measure_uom').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#bld_measure_uom').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#bld_measure_uom').focus();
                }
                if (field == "UOM3") {
                    jQuery('#bl_pack_uom').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#bl_pack_uom').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#bl_pack_uom').focus();
                }
                if (field == "BL_JOB_NO") {
                    jQuery('#Bl_job_no').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Bl_job_no').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Bl_job_no').focus();
                }
                if (field == "BL_JOB_NO") {
                    jQuery('#Bl_job_no').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Bl_job_no').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Bl_job_no').focus();
                }
                if (field == "BL_H_DOC_NO") {
                    
                    jQuery('#Bl_h_doc_no').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    
                    jQuery('#Bl_h_doc_no').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Bl_h_doc_no').focus();
                }
                if (field == "BL_H_DOC_NOALL") {
                    jQuery('#houseblall').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#houseblall').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#houseblall').focus();
                }
                if (field == "BL_D_DOC_NO") {
                    jQuery('#Bl_d_doc_no').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Bl_d_doc_no').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Bl_d_doc_no').focus();
                }
                if (field == "BL_M_DOC_NO") {
                    jQuery('#Bl_m_doc_no').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Bl_m_doc_no').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Bl_m_doc_no').focus();
                }
                if (field == "BL_PORT_LOAD") {
                    jQuery('#Bl_port_load').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Bl_port_load').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Bl_port_load').focus();
                }
                if (field == "BL_TERMINAL") {
                    jQuery('#bl_terminal').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#bl_terminal').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#bl_terminal').focus();
                }
                if (field == "PORTS2") {
                    jQuery('#Bl_port_discharge').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(2)').text();
                    jQuery('#Bl_port_discharge').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Bl_port_discharge').focus();
                }
                if (field == "cusCode4") {
                    jQuery('#Bl_shipper_cd').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(2)').text();
                    var name = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Bl_shipper_cd').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Bl_shipper_cd').focus();
                    jQuery('.tbl-shipp-name .new-row').remove();
                    jQuery('.tbl-shipp-name').append('<tr class="new-row">' +
                         '<td>' + name + '</td>' + '</tr>'
                        );
                }
                if (field == "cusCode5") {
                    jQuery('#Bl_consignee_cd').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(2)').text();
                    var name = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Bl_consignee_cd').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Bl_consignee_cd').focus();
                    jQuery('.tbl-cons-name .new-row').remove();
                    jQuery('.tbl-cons-name').append('<tr class="new-row">' +
                         '<td>' + name + '</td>' + '</tr>'
                        );
                }
                if (field == "cusCode6") {
                    jQuery('#Bl_ntfy_party_cd').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(2)').text();
                    var name = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Bl_ntfy_party_cd').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Bl_ntfy_party_cd').focus();
                    jQuery('.tbl-notify-name .new-row').remove();
                    jQuery('.tbl-notify-name').append('<tr class="new-row">' +
                         '<td>' + name + '</td>' + '</tr>'
                        );
                }
                if (field == "cusCode7") {
                    jQuery('#Bl_agent_cd').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(2)').text();
                    var name = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Bl_agent_cd').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Bl_agent_cd').focus();
                    jQuery('.tbl-agent-name .new-row').remove();
                    jQuery('.tbl-agent-name').append('<tr class="new-row">' +
                         '<td>' + name + '</td>' + '</tr>'
                        );
                }
                if (field == "cusCode8") {
                    jQuery('#Bl_ship_line_cd').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(2)').text();
                    var name = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Bl_ship_line_cd').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Bl_ship_line_cd').focus();
                    jQuery('.tbl-carr-name .new-row').remove();
                    jQuery('.tbl-carr-name').append('<tr class="new-row">' +
                         '<td>' + name + '</td>' + '</tr>'
                        );
                }
                if (field == "cusCode9") {
                    jQuery('#Tih_cus_cd').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(2)').text();
                    var name = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Tih_cus_cd').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Tih_cus_cd').focus();
                    jQuery('.tbl-cus-name .new-row').remove();
                    jQuery('.tbl-cus-name').append('<tr class="new-row">' +
                         '<td>' + name + '</td>' + '</tr>'
                        );
                }
                if (field == "cusCode9jn") {
                    jQuery('#Tih_cus_cd').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(2)').text();
                    var name = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Tih_cus_cd').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Tih_cus_cd').focus();
                    jQuery('.tbl-cus-name .new-row').remove();
                    jQuery('.tbl-cus-name').append('<tr class="new-row">' +
                         '<td>' + name + '</td>' + '</tr>'
                        );
                }
                if (field == "PORTS") {
                    jQuery('#Bl_port_load').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(2)').text();
                    jQuery('#Bl_port_load').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Bl_port_load').focus();
                }
                //Dilshan 08/09/2017
                if (field == "PORTSREF") {
                    jQuery('#PA_PRT_CD').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#PA_PRT_CD').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#PA_PRT_CD').focus();
                }
                if (field == "VESSEL") {
                    jQuery('#Bl_vessal_no').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(2)').text();
                    jQuery('#Bl_vessal_no').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Bl_vessal_no').focus();
                }
                //dilshan 06/09/2017
                if (field == "VESSEL1") {
                    jQuery('#VM_VESSAL_CD').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#VM_VESSAL_CD').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#VM_VESSAL_CD').focus();
                }

                if (field == "BL_POUCH_NO") {
                    jQuery('#Bl_pouch_no').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Bl_pouch_no').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Bl_pouch_no').focus();
                }
                if (field == "BL_MANUAL_D_REF") {
                    jQuery('#bl_manual_d_ref').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#bl_manual_d_ref').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#bl_manual_d_ref').focus();
                }
                if (field == "jobno2") {
                    jQuery('#Bl_job_no').val("");
                    jQuery('#Bl_pouch_no').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    var pouch = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(2)').text();
                    jQuery('#Bl_job_no').val(value);
                    jQuery('#Bl_pouch_no').val(pouch);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Bl_job_no').focus();
                }
                if (field == "BLno2") {
                    jQuery('#Bl_doc_no').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Bl_doc_no').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Bl_doc_no').focus();
                }

                if (field == "ProfitCenter" || field == "ptycshsetpc") {
                    jQuery('#ProfitCenter').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#ProfitCenter').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#ProfitCenter').focus();
                }
                if (field == "employee") {
                    jQuery('#ReqBy').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(3)').text();
                    jQuery('#ReqBy').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#ReqBy').focus();
                }
                if (field == "employee2") {
                    jQuery('#Tih_ex_cd').val("");
                    jQuery('#Tih_exec_name').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    var fname = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(3)').text();
                    var lname = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(4)').text();
                    jQuery('#Tih_ex_cd').val(value);
                    jQuery('#Tih_exec_name').val(fname + " " + lname);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Tih_ex_cd').focus();
                }
                //dilshan
                if (field == "employeeex") {
                    jQuery('#Tih_ex_cd').val("");
                    jQuery('#Tih_exec_name').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    var fname = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(3)').text();
                    var lname = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(4)').text();
                    jQuery('#Tih_ex_cd').val(value);
                    jQuery('#Tih_exec_name').val(fname + " " + lname);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Tih_ex_cd').focus();
                }
                //*******
                if (field == "employee3") {
                    jQuery('#Jb_sales_ex_cd').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Jb_sales_ex_cd').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Jb_sales_ex_cd').focus();
                }
                if (field == "employee4") {
                    jQuery('#ESEP_EPF').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#ESEP_EPF').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#ESEP_EPF').focus();
                }
                if (field == "ptychsreq") {
                    jQuery('#RequestNo').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#RequestNo').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#RequestNo').focus();
                }
                if (field == "consignee") {
                    jQuery('#Consignee').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Consignee').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Consignee').focus();
                }
                    if (field == "payconsignee") {
                        jQuery('#Consignee').val("");
                        var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(2)').text();
                        jQuery('#Consignee').val(value);
                        jQuery('#modalview').modal('hide');
                        jQuery("#KeyWord").val("");
                        jQuery('#Consignee').focus();
                } if (field == "payto") {
                    jQuery('#PayTo').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(2)').text();
                    jQuery('#PayTo').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#PayTo').focus();
                }
                if (field == "BLnoM") {
                    
                    jQuery('#Bl_m_doc_no').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Bl_m_doc_no').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Bl_m_doc_no').focus();
                }
                if (field == "BLnoH") {
                    jQuery('#Bl_h_doc_no').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Bl_h_doc_no').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Bl_h_doc_no').focus();
                }
                if (field == "ptyjob") {
                    jQuery('#JobNo').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#JobNo').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#JobNo').focus();
                }
                if (field == "costele") {
                    jQuery('#CstEle').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(2)').text();
                    jQuery('#CstEle').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#CstEle').focus();
                }
                if (field == "costeleRev") {
                    jQuery('#CstEleRev').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(2)').text();
                    jQuery('#CstEleRev').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#CstEleRev').focus();
                }
                if (field == "revtele") {
                    jQuery('#CstEle').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(2)').text();
                    jQuery('#CstEle').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#CstEle').focus();
                }
                if (field == "costelem") {
                    jQuery('#merg').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(2)').text();
                    jQuery('#merg').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#merg').focus();
                }
                //dilshan 07/09/2017
                if (field == "costele1") {
                    jQuery('#MCE_CD').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(2)').text();
                    jQuery('#MCE_CD').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#MCE_CD').focus();
                }
                if (field == "UOMPTY") {
                    jQuery('#UOM').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#UOM').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#UOM').focus();
                }
                if (field == "curcd") {
                    jQuery('#Currency').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Currency').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Currency').focus();
                }

                if (field == "curcd1") {
                    jQuery('#MBE_CUR_CD').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#MBE_CUR_CD').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#MBE_CUR_CD').focus();
                }

                if (field == "detserch") {
                    jQuery('#VehLcTel').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#VehLcTel').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#VehLcTel').focus();
                } 
                if (field == "curcd2") {
                    jQuery('#Tih_inv_curr').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Tih_inv_curr').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Tih_inv_curr').focus();
                }
                if (field == "InvoiceNo") {
                    jQuery('#Tih_inv_no').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Tih_inv_no').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Tih_inv_no').focus();
                }
                if (field == "InvoiceNoCrd") {
                    jQuery('#Tih_inv_no').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Tih_inv_no').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Tih_inv_no').focus();
                }
                if (field == "InvoiceNo3") {
                    jQuery('#Tih_inv_no').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Tih_inv_no').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Tih_inv_no').focus();
                }
                if (field == "InvoiceNo2") {
                    jQuery('#InvoiceNo').val("");
                    jQuery('#Ammountdup').val("");
                    jQuery('#Ammount').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    var ammount = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(3)').text();
                    
                    var amntdup = ReplaceNumberWithCommas(Number(ammount.replace(/[^0-9\.-]+/g, "")).toFixed(2));
                    var amntognl = ReplaceNumberWithCommas(Number(ammount.replace(/[^0-9\.-]+/g, "")).toFixed(2));
                    
                    jQuery('#InvoiceNo').val(value);
                    jQuery('#Ammountdup').val(amntdup); // Edited by Chathura on 20-sep-2017
                    jQuery('#Ammount').val(amntognl); // Edited by Chathura on 20-sep-2017
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Ammount').focus();
                }
                if (field == "InvoiceNoRef") {
                    jQuery('#Tih_other_ref_no').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Tih_other_ref_no').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Tih_other_ref_no').focus();
                }
                if (field == "jobno3") {
                    jQuery('#Tih_job_no').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Tih_job_no').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Tih_job_no').focus();
                }
                if (field == "BL_H_DOC_NO2") {
                    jQuery('#Tih_bl_h_no').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Tih_bl_h_no').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Tih_bl_h_no').focus();
                }
                if (field == "BL_H_DOC_NO2E") {
                    jQuery('#Tih_bl_h_no').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Tih_bl_h_no').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Tih_bl_h_no').focus();
                    
                    jQuery.ajax({
                        cache: false,
                        async: true,
                        type: "POST",
                        url: "/Invoice/LoadBLDetails",
                        data: { BLno: value },
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    
                                    jQuery("#d_bl_doc").text(result.data[0].bl_d_doc_no);
                                    jQuery("#d_bl_ref").text(result.data[0].BL_MANUAL_D_REF);
                                    jQuery("#h_bl_doc").text(result.data[0].bl_h_doc_no);
                                    jQuery("#h_bl_ref").text(result.data[0].BL_MANUAL_H_REF);
                                    jQuery("#m_bl_doc").text(result.data[0].bl_m_doc_no);
                                    jQuery("#m_bl_ref").text(result.data[0].BL_MANUAL_M_REF);
                                    jQuery("#bl_eta").text(my_date_format(new Date(parseInt(result.data[0].bl_est_time_arr.substr(6)))));
                                    jQuery("#bl_etd").text(my_date_format(new Date(parseInt(result.data[0].bl_est_time_dep.substr(6)))));
                                } else {
                                    if (result.type == "Error") {
                                        //setError(result.msg);
                                    } else if (result.type == "Info") {
                                        //setInfoMsg(result.msg);
                                    }
                                }

                            } else {
                                //Logout();
                            }
                        }
                    });
                }
                if (field == "Mesure") {
                    jQuery('#bld_measure_uom').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#bld_measure_uom').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#bld_measure_uom').focus();
                }
                if (field == "cusCode10") {
                    jQuery('#Tih_inv_party_cd').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    var name = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(2)').text();
                    jQuery('#Tih_inv_party_cd').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Tih_inv_party_cd').focus();
                    jQuery('.tbl-inv-party .new-row').remove();
                    jQuery('.tbl-inv-party').append('<tr class="new-row">' +
                         '<td>' + name + '</td>' + '</tr>'
                        );
                }
                if (field == "cusCodeCp") {
                    jQuery('#Tih_inv_party_cd').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(2)').text();
                    var name = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Tih_inv_party_cd').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Tih_inv_party_cd').focus();
                    jQuery('.tbl-inv-party .new-row').remove();
                    jQuery('.tbl-inv-party').append('<tr class="new-row">' +
                         '<td>' + name + '</td>' + '</tr>'
                        );
                }
                if (field == "cusCodeCpType") {
                    jQuery('#Tih_inv_party_cd').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(2)').text();
                    var name = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Tih_inv_party_cd').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Tih_inv_party_cd').focus();
                    jQuery('.tbl-inv-party .new-row').remove();
                    jQuery('.tbl-inv-party').append('<tr class="new-row">' +
                         '<td>' + name + '</td>' + '</tr>'
                        );
                }
                if (field == "cusCode11") {
                    jQuery('#Sar_debtor_cd').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(2)').text();
                    var name = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Sar_debtor_cd').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Sar_debtor_cd').focus();
                    jQuery('.tbl-cus-name .new-row').remove();
                    jQuery('.tbl-cus-name').append('<tr class="new-row">' +
                         '<td>' + name + '</td>' + '</tr>'
                        );
                }
                if (field == "setlmentno") {
                    jQuery('#SettlementNo').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#SettlementNo').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#SettlementNo').focus();
                } 
                if (field == "curcd3") {
                    jQuery('#dealer_currncy').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#dealer_currncy').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#dealer_currncy').focus();
                }
                if (field == "presentCountry") {
                    jQuery('#country').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#country').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#country').focus();
                }
                if (field == "presentCountry1") {
                    jQuery('#MBE_COUNTRY_CD').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#MBE_COUNTRY_CD').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#MBE_COUNTRY_CD').focus();
                }
                if (field == "perTown") {
                    jQuery('#town').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#town').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#town').focus();
                }
                if (field == "perTown1") {
                    jQuery('#MBE_TOWN_CD').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#MBE_TOWN_CD').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#MBE_TOWN_CD').focus();
                }
                if (field == "pouchnojob") {
                    jQuery('#Jb_pouch_no').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Jb_pouch_no').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Jb_pouch_no').focus();
                }
                if (field == "pouchnojob2") {
                    jQuery('#pouchno').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#pouchno').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#pouchno').focus();
                }
                if (field == "pouchnojob3") {
                    jQuery('#Bl_pouch_no').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    var job = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(2)').text();
                    jQuery('#Bl_pouch_no').val(value);
                    jQuery('#Bl_job_no').val(job);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#pouchno').focus();
                }
                if (field == "bnkAcc") {
                    jQuery('#Deposit_bank_cd').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Deposit_bank_cd').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Deposit_bank_cd').focus();
                }
                if (field == "debtBnkAcc") {
                    jQuery('#Debt_bank_name').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(3)').text();
                    jQuery('#Debt_bank_name').val(value);
                    var hidevalue = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Debt_bank_cd').val(hidevalue);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Debt_bank_name').focus();

                }
                if (field == "chkBnkAcc") {
                    jQuery('#cheque-bank').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#cheque-bank').val(value);
                    var hidevalue = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(3)').text();
                    jQuery('#Cheque_bnk_cd').val(hidevalue);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Cheque_branch_cd').val("");
                    jQuery('#cheque-bank').focus();
                }
                if (field == "bnkChqBranch") {
                    jQuery('#Cheque_branch_cd').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Cheque_branch_cd').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Cheque_branch_cd').focus();
                }
                if (field == "credBnkAcc") {
                    jQuery('#Cred_crd_bank').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(3)').text();
                    jQuery('#Cred_crd_bank').val(value);
                    var hidevalue = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Cred_crd_bank_value').val(hidevalue);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Cred_crd_bank').focus();
                }
                if (field == "BL_MANUAL_H_REF") {
                    jQuery('#bl_manual_h_ref').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#bl_manual_h_ref').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#bl_manual_h_ref').focus();
                }
                if (field == "BL_MANUAL_H_REF1") {
                    jQuery('#bl_manual_h_ref').val("");
                    jQuery('#Bl_h_doc_no').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    var value1 = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(2)').text();
                    jQuery('#bl_manual_h_ref').val(value);
                    jQuery('#Bl_h_doc_no').val(value1);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Bl_h_doc_no').focus();
                }
                if (field == "BL_MANUAL_M_REF1") {
                    jQuery('#bl_manual_m_ref').val("");
                    jQuery('#Bl_m_doc_no').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    var value1 = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(2)').text();
                    jQuery('#bl_manual_m_ref').val(value);
                    jQuery('#Bl_m_doc_no').val(value1);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Bl_m_doc_no').focus();
                }
                if (field == "BL_MANUAL_M_REF") {
                    jQuery('#bl_manual_m_ref').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#bl_manual_m_ref').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#bl_manual_m_ref').focus();
                }
                if (field == "receiptTYypeSearch") {
                    jQuery('#Sar_receipt_type').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Sar_receipt_type').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Sar_receipt_type').focus();
                    if (value == "AD-HOC") {
                        jQuery("#cus_heading").text("Direct receipt customer details");
                    }
                    else {
                        jQuery("#cus_heading").text("Debtor details");
                    }
                    clearpageOnReceiptType();
                }
                if (field == "TY_OF_SHIPMNT") {
                    jQuery('#shipment_type').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#shipment_type').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#shipment_type').focus();
                }
                if (field == "receiptSearch") {
                    
                    jQuery('#Sar_receipt_no').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    global_receipt_date = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(3)').text();
                    
                    jQuery('#Sar_receipt_no').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery(".cancel-rec").show();
                    jQuery('#Sar_receipt_no').focus();
                } 
                if (field == "purjobno") {
                    jQuery('#jobno').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#jobno').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#jobno').focus();
                }
                if (field == "cusCodecus") {
                    jQuery('#MBE_CD').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#MBE_CD').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#MBE_CD').focus();
                }
                if (field == "divisionSearch") {
                    jQuery('#Division').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Division').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Division').focus();
                }
                if (field == "custypsrch") {
                    jQuery('#CusType').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#CusType').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#CusType').focus();
                }
                if (field == "paytypsrch") {
                    jQuery('#PayType').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#PayType').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#PayType').focus();
                }
                if (field == "creusrsrch") {
                    jQuery('#CreateUser').val("");
                    var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#CreateUser').val(value);
                    jQuery('#modalview').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#CreateUser').focus();
            }
            if (field == "TY_OF_ROLE_ID") {
                var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                var value1 = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(2)').text();
                var value2 = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(3)').text();
                var value3 = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(5)').text();

                jQuery('#role_id').val(value);
                jQuery('#role_name').val(value2);
                jQuery('#role_description').val(value1);
                if (value3 == 1) {
                    jQuery("#IsActive").prop("checked", true);
                }
                else {
                    jQuery("#IsActive").prop("checked", false);
                }

                jQuery('#modalview').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#CreateUser').focus();
            }
            if (field == "TY_OF_ROLE_ID_GRANT") {
                var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                var value1 = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(2)').text();
                var value2 = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(3)').text();
                var value3 = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(5)').text();

                jQuery('#role_id_grant_privilage').val(value);
                jQuery('#role_name_grant_privilage').val(value2);
                jQuery('#role_description_grant_privilage').val(value1);
                if (value3 == 1) {
                    jQuery("#IsActiveGrantprivilage").prop("checked", true);
                }
                else {
                    jQuery("#IsActiveGrantprivilage").prop("checked", false);
                }

                jQuery('#modalview').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#CreateUser').focus();
                }
            if (field == "TY_OF_ROLE_ID_VR") {
                var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                var value1 = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(2)').text();
                var value2 = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(3)').text();

                jQuery('#role_id_view_role').val(value);
                jQuery('#role_description_view_role').val(value1);

                jQuery('#modalview').modal('hide');
                jQuery("#KeyWord").val("");

            }
            if (field == "OPTION_ID_DET") {
                var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                var value1 = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(2)').text();
                var value2 = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(3)').text();
                var value3 = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(5)').text();

                jQuery('#role_id_grant_role').val(value);
                jQuery('#role_description_grant_role').val(value1);
                jQuery('#role_name_grant_role').val(value2);
                if (value3 == '1') {

                    jQuery("#IsActiveGrantRole").prop("checked", true);
                }
                else { jQuery("#IsActiveGrantRole").prop("checked", false); }

                jQuery('#modalview').modal('hide');
                jQuery("#KeyWord").val("");
            }

            if (field == "USER_LIST") {
                var userid = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                var name = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(2)').text();
                var password = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(4)').text();
                var designation = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(5)').text();
                var department = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(6)').text();
                var empid = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(7)').text();
                var empcode = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(8)').text();
                var isdomain = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(9)').text();
                var isauth = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(10)').text();
                var domainid = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(11)').text();
                var sunid = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(12)').text();
                var email = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(13)').text();
                var mobile = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(14)').text();
                var phone = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(15)').text();
                var actstat = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(16)').text();
                var allowchange = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(17)').text();
                var mustchange = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(18)').text();
                var Se_act_rmk = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(19)').text();
                var sedomainname = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(20)').text();
                var sedomaintitle = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(21)').text();
                var sedomaindeparment = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(22)').text();

                jQuery('#user_id').val(userid);
                jQuery('#full_name').val(name);
                jQuery('#description').val(name);
                jQuery('#password').val(password);
                jQuery('#confirm_password').val(password);
                jQuery('#designation_id').val(designation);
                jQuery('#department_id').val(department);
                jQuery('#employee_id').val(empid);
                jQuery('#emp_code').val(empcode);
                jQuery('#domain_id').val(domainid);
                jQuery('#sun_user_id').val(sunid);
                jQuery('#email').val(email);
                jQuery('#mobile_no').val(mobile);
                jQuery('#phone_no').val(phone);
                jQuery('#se_act_rmk').val(Se_act_rmk);
                jQuery('#domain_name').val(sedomainname);
                jQuery('#domain_title').val(sedomaintitle);
                jQuery('#domain_department').val(sedomaindeparment);

                if (actstat == 0) {
                    $("#Inactive").prop("checked", true);
                }
                else if (actstat == -1) {
                    $("#Locked").prop("checked", true);
                }
                else if (actstat == -2) {
                    $("#PermanentlyDisable").prop("checked", true);
                }
                else {
                    $("#Active").prop("checked", true);
                }

                if (isdomain == '1') {
                    jQuery("#IsDomain").prop("checked", true);
                }
                else {
                    jQuery("#IsDomain").prop("checked", false);
                }
                if (isauth == '1') {
                    jQuery("#IsWindowsAuth").prop("checked", true);
                }
                else {
                    jQuery("#IsWindowsAuth").prop("checked", false);
                }
                if (allowchange == '1') {
                    jQuery("#IsAllowToChangePassword").prop("checked", true);
                }
                else {
                    jQuery("#IsAllowToChangePassword").prop("checked", false);
                }
                if (mustchange == '1') {
                    jQuery("#IsUserMustChangeNxtLog").prop("checked", true);
                }
                else {
                    jQuery("#IsUserMustChangeNxtLog").prop("checked", false);
                }

                $('#user_id').attr('readonly', true);
                $('#createico').hide();
                

                jQuery('#modalview').modal('hide');
                jQuery("#KeyWord").val("");
            }

            if (field == "USER_LIST_ASI") {
                var userid = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                var name = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(2)').text();
                var password = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(4)').text();
                var designation = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(5)').text();
                var department = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(6)').text();
                var empid = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(7)').text();


                jQuery('#user_id_asi').val(userid);
                jQuery('#full_name_asi').val(name);
                jQuery('#description_asi').val(name);
                jQuery('#designation_asi').val(designation);
                jQuery('#department_asi').val(department);
                jQuery('#empid_asi').val(empid);

                jQuery('#modalview').modal('hide');
                jQuery("#KeyWord").val("");
            }

            if (field == "DEPT_LIST") {
                var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                var value1 = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(2)').text();

                jQuery('#department_id').val(value);

                jQuery('#modalview').modal('hide');
                jQuery("#KeyWord").val("");
            }

            if (field == "COMPANY_LIST_ASI") {
                var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                var value1 = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(2)').text();
  
                jQuery('#description_company_asi').val(value1);
                jQuery('#company_id_asi').val(value);

                jQuery('#modalview').modal('hide');
                jQuery("#KeyWord").val("");
            }

            if (field == "DESIG_LIST") {
                var value = jQuery('tr.selected td', '#modalview table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#designation_id').val(value);

                jQuery('#modalview').modal('hide');
                jQuery("#KeyWord").val("");
            }

            }
        }

    }
 
    var my_date_format = function (input) {
        var monthNames = [
          "Jan", "Feb", "Mar",
          "Apr", "May", "Jun", "Jul",
          "Aug", "Sep", "Oct",
          "Nov", "Dec"
        ];

        var date = new Date(Date.parse(input));;
        var day = date.getDate();
        var monthIndex = date.getMonth();
        var year = date.getFullYear();

        return (day + "/" + monthNames[monthIndex] + "/" + year);
    };
    jQuery(document).ready(function () {

        jQuery('#modalview').on('shown.bs.modal', function () {
            jQuery("#KeyWord").focus();
            //jQuery(".modal-content").draggable({ handle: ".Title.panel-heading", containment: "body" });
            jQuery('.Title.panel-heading').css('cursor', 'move');
        });
        jQuery(document).keypress(function (evt) {
            
            if (evt.keyCode == 27) {
                if (jQuery('#modalview:visible').length == 1) {
                    jQuery("#KeyWord").val("");
                    jQuery('#modalview').modal('hide');
                }
            }
        });
        jQuery('#modalview .close-btn').click(function (e) {
            e.preventDefault();
            jQuery("#KeyWord").val("");
            jQuery('#modalview').modal('hide');
        });
        // This	creates a new object
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
                    jQuery('#modalview').css('cursor', 'wait');
                    jQuery(".se-pre-con").fadeIn("slow");
                    var searchFld = jQuery('select.filter-key-cls').val();
                    var searchVal = jQuery('input#KeyWord').val();
                    var pgeSize = jQuery('.modal-content .cls-select-page-cont').val();
                    loadDetails(num, pgeSize, searchFld, searchVal, headerKeys);
                }
            });

        }
        jQuery('.modal-content .cls-select-page-cont').change(function (e) {
            jQuery('#modalview').css('cursor', 'wait');
            jQuery(".se-pre-con").fadeIn("slow");
            var searchFld = jQuery('select.filter-key-cls').val();
            var searchVal = jQuery('input#KeyWord').val();
            var pgeSize = jQuery('.modal-content .cls-select-page-cont').val();
            var pgeNum = 1;
            loadDetails(pgeNum, pgeSize, searchFld, searchVal, headerKeys, true);
        });

        jQuery(document).on("keypress", function (e) {
            
            if (e.keyCode == 13) {
                
                if (jQuery('.modal-content #KeyWord').is(':focus') == true) {
                    
                    jQuery('#modalview').css('cursor', 'wait');
                    jQuery(".se-pre-con").fadeIn("slow");
                    var searchFld = jQuery('select.filter-key-cls').val();
                    var searchVal = jQuery('input#KeyWord').val();
                    var pgeSize = jQuery('.modal-content .cls-select-page-cont').val();
                    var pgeNum = 1;
                    //jQuery('table.cls-search-pnl tr:first').focus();
                    
                    
                    //$("#modalview table tbody tr td:first").focus();
                    
                    loadDetails(pgeNum, pgeSize, searchFld, searchVal, headerKeys, true);
                    //$('.cls-search-pnl').closest('tr').next().focus();
                    //
                    //jQuery('.modal-content #KeyWord').focusout();
                    //var aa = jQuery('#modalview table tbody tr');
                    //jQuery('tr', '#modalview table tbody').removeClass('selected');
                    //jQuery('tr', '#modalview table tbody').css('color', 'black');
                    //jQuery(aa[0]).addClass('selected');
                    //jQuery(aa[0]).css('color', 'red');
                    //jQuery(aa[0]).focus();
                    //
                } else {
                    setValue(field);
                    ////$('.cls-search-pnl').closest('tr').next().find('td').eq(0).focus();
                    //
                    ////jQuery('table.cls-search-pnl tr:first').addClass('selected');
                    ////var aa = jQuery('#modalview table tbody tr.selected').next();
                    //var aa = jQuery('#modalview table tbody tr');
                    //jQuery('tr', '#modalview table tbody').removeClass('selected');
                    //jQuery('tr', '#modalview table tbody').css('color', 'black');
                    //jQuery(aa[0]).addClass('selected');
                    //jQuery(aa[0]).css('color', 'red');
                    //jQuery(aa[0]).focus();
                }

            } else if (e.keyCode == 40) {
                if (jQuery('.modal-content #KeyWord').is(':focus') == true) {
                    
                    var aa = jQuery('#modalview table tbody tr');
                    if (typeof aa[0] != "undefined") {
                        
                        jQuery('.modal-content #KeyWord').focusout();
                        jQuery('tr', '#modalview table tbody').removeClass('selected');
                        jQuery('tr', '#modalview table tbody').css('color', 'black');
                        jQuery(aa[0]).addClass('selected');
                        jQuery(aa[0]).css('color', 'red');
                    }
                }
            }
            
        });
        
        $(document).keydown(function (e) {
            
            if (e.keyCode == 39) {
                        
                        if (jQuery('.modal-content #KeyWord').is(':focus') == true) {
                            
                            var aa = jQuery('#modalview table tbody tr');
                            if (typeof aa[0] != "undefined") {
                                
                                jQuery('.modal-content #KeyWord').focusout();
                                $(".modal-content #KeyWord").blur();
                                jQuery('tr', '#modalview table tbody').removeClass('selected');
                                jQuery('tr', '#modalview table tbody').css('color', 'black');
                                jQuery(aa[0]).addClass('selected');
                                jQuery(aa[0]).css('color', 'red');
                            }
                        }
            }


            if (jQuery("#modalview").is(":visible") == true) {
                
                if (jQuery('tr td', '#modalview table tbody').length > 0) {
                    
                    if (e.keyCode == 40) {
                        
                        if (jQuery('.modal-content #KeyWord').is(':focus') == false) {
                            jQuery('.modal-content #KeyWord').focusout();
                            var aa = jQuery('#modalview table tbody tr.selected').next();
                            
                            if (typeof aa[0] != "undefined") {
                                jQuery('tr', '#modalview table tbody').removeClass('selected');
                                jQuery('tr', '#modalview table tbody').css('color', 'black');
                                jQuery(aa[0]).addClass('selected');
                                jQuery(aa[0]).css('color', 'red');
                            }
                        }
                    }
                    else if (e.keyCode == 38) {
                        
                        if (jQuery('.modal-content #KeyWord').is(':focus') == false) {
                            
                            jQuery('.modal-content #KeyWord').focusout();
                            var bb = jQuery('#modalview table tbody tr.selected').prev();
                            
                            if (typeof bb[0] != "undefined") {
                                jQuery('tr', '#modalview table tbody').removeClass('selected');
                                jQuery('tr', '#modalview table tbody').css('color', 'black');
                                jQuery(bb[0]).addClass('selected');
                                jQuery(bb[0]).css('color', 'red');
                            }
                        }

                    }
                }
            }

        });
    
    });
    function getFormatedDate1(date) {
        var dte = new Date(parseInt(date.substr(6)));
        if (my_date_format_tran(dte) != "NaN/undefined/NaN")
            return my_date_format_tran(dte);
    }
    function Logout() {
        Lobibox.alert('info', {
            msg: 'Login session has expired!',
            buttons: {
                ok: {
                    'class': 'btn btn-info',
                    closeOnClick: false
                }
            },
            callback: function (lobibox, type) {
                var btnType;
                if (type === 'ok') {
                    window.location.replace("/Login/Index");
                }
            }
        });
    }

    function clearpageOnReceiptType() {
        
        //jQuery("#Sar_manual_ref_no").val("");
        //jQuery('#Sar_receipt_date').val(my_date_format_tran(my_date_format_with_time(new Date()).toString()));
        //jQuery("#Sar_debtor_cd").val("");
        //jQuery("#Sar_debtor_name").val("");
        //jQuery("#Sar_manual_ref_no").val("");
        //jQuery(".tot-paid-amount-val").empty();
        //jQuery(".tot-paid-amount-val").html("");
        //jQuery("#TotalAmount").val(0);
        //jQuery('table.payment-table .new-row').remove();
        //jQuery('table.payment-table').remove();
        //jQuery("#VehLcTel").val(""); // Added by Chathura on 20-sep-2017
        //jQuery("#InvoiceNo").val(""); // Added by Chathura on 20-sep-2017
        //jQuery(".tbl-cus-name tr").remove(); // Added by Chathura on 20-sep-2017
        //jQuery("#Ammountdup").val(""); // Commented by Chathura on 20-sep-2017
        //jQuery("#Ammount").val(""); // Commented by Chathura on 20-sep-2017
        //jQuery(".cancel-rec").hide();
        //jQuery("#cus_heading").text("Debtor details");
        //jQuery.ajax({
        //    type: "GET",
        //    url: "/RecieptEntry/ClearSession",
        //    data: {},
        //    contentType: "application/json;charset=utf-8",
        //    dataType: "json",
        //})
    }

