var first = true;
var first_time = true;
var headerKeys;
var field = "";
var parameterData = "";
function CommonSearch(headerKeys, selectedField, data) {
    headerKeys = headerKeys;
    field = selectedField;
    if (data != null) {
        parameterData = data;
    } else {
        parameterData = null;
    }
    
    if (headerKeys.length > 0 ) {
        var selecter = jQuery('#myModal .filter-key-cls');
        selecter.empty();
        for (i = 1; i < headerKeys.length; i++) {
            var newOption = jQuery('<option value="' + headerKeys[i] + '">' + headerKeys[i] + '</option>');
            selecter.append(newOption);
        }
        var head = jQuery('#myModalContent .table-responsive .table thead tr');
        head.empty();
        for (j = 0; j < headerKeys.length; j++) {
            var newHead = jQuery('<th>' + headerKeys[j].toUpperCase() + '</th>');
            head.append(newHead);
        }
        //first_time = false;
    }
    jQuery('body').css('cursor', 'wait');
    jQuery(".se-pre-con").fadeIn("slow");
    var pgeNum = 1;
    var pgeSize = jQuery('.modal-content .cls-select-page-cont').val();
    var searchFld = jQuery('select.filter-key-cls').val();
    var searchVal = jQuery('input#KeyWord').val();
    loadDetails(pgeNum, pgeSize, searchFld, searchVal, headerKeys);

    return false;
}
    var first = true;
    var check = false;
    function loadDetails(pgeNum, pgeSize, searchFld, searchVal, headerKeys, check) {
        if (field == "cusCode" || field == "cusCodeForTraEnq" || field == "cusCodeEnq" || field == "guesscusCodeEnq" || field == "cusCodeForCost" || field == "cusCodeInv" || field == "srchCusReceipt" || field == "salesCusSrch" || field == "costCusSrch" || field == "logCusSearch" || field == "popCusDet" || field == "cusCodeForQuot" || field == "othPrtyCus" || field == "othConCusSrch" || field == "payConCusfSrch") {
            newurl="/Search/getCustomerDetails";
        }
        if (field == "perTown" || field == "preTown" || field == "pickTownSrch" || field == "dropTownSrch" || field == "chgcdfromTownSrch" || field == "chgcdtoTownSrch" || field == "chgcdfromTownSrchTrn" || field == "chgcdtoTownSrchTrn") {
            newurl = "/Search/getTownDetails";
        }
        if (field == "ProfitCenter" || field == "rptProfCenSrch" || field == "costProfCenSrch" || field == "costProfCenSrch") {
            newurl = "/Search/getProfitCenters";
        }
        if (field == "employee" ) {
            newurl = "/Search/getEmployeeDetails";
        }
        if (field == "employeedet") {
            newurl = "/Search/getEmployeeDetails";
        }
        if (field == "EnqByNic") {
            newurl = "/Search/GetEnqDataForNic?nic=" + parameterData;
           
        }
        if (field == "fleet" || field == "transportFleet") {
            newurl = "/Search/getFleetDetails";
        }
        if (field == "Country" || field == "DCountry" || field == "ChfCountry" || field == "ChtCountry" || field == "reseCountry" || field == "presentCountry" || field == "crCountry") {
            newurl = "/Search/getCountry";
        }
        if (field == "insby") {
            newurl = "/Search/getServiceProviderDetails";
        }
        if (field == "driverall") {
            newurl = "/Search/getFleetDetails";
        }
        if (field == "bnkAcc") {
            newurl = "/Search/getDepositedBanks";
        }
        if(field=="chkBnkAcc" || field=="credBnkAcc" || field=="debtBnkAcc"){
            newurl = "/Search/getBusComBanks";
        }
        if (field == "bnkChqBranch") {
            newurl = "/Search/getBankBranchs?bankcd=" + parameterData;
        }
        if (field == "advRefNo") {
            newurl = "/Search/getAdvanceRerference?cusCd=" + parameterData;
            
        }
        if (field == "credNteRefNo") {
            newurl = "/Search/getCredNoteRerference?customer=" + parameterData;
        }
        if (field == "giftVoucherSrch") {
            newurl = "/Search/getGiftVoucherSearch?customer=" + parameterData;
        }
        if (field == "loyalCrdSrch") {
            newurl = "/Search/getLoyaltyCard?customer=" + parameterData;
        }
        if (field == "InvEnq" || field == "costEnqSrch" || field == "costEnqIdSrch" || field == "InvEnqNew" || field == "quotEnqIdSrch" || field == "feedEnqIdSrch") {
            newurl = "/Search/geTtransportEnqiurySearch";
        }
        if (field == "transportEnq" || field == "chktransportEnq") {
            newurl = "/Search/geTRANSEnqiurySearch";
        }
        if (field == "chrgCdeSrchWithType") {
            newurl = "/Search/getChargCodeListWithType?type=" + parameterData;
        }
        if (field == "chrgCdeSrch" || field == "costChrgCdeSrchTrans" || field == "invChrgCdeSrchTrans" || field == "ChrgCdeSrchTrans" || field == "logChrgCdeSrchTrans" || field == "parentSrchTrans") {
            newurl = "/Search/getChargCodeList?service=" + parameterData;
        }
        if (field == "chrgCdeSrch2") {
            newurl = "/Search/getChargCodeList2?service=" + parameterData.cuscd + "&option=" + parameterData.option;
        }
        if (field == "costChrgCdeSrchArival" || field == "invChrgCdeSrchArival" || field == "ChrgCdeSrchArival") {
            newurl = "/Search/getChargCodeListArrival?service=" + parameterData;
        }
        if (field == "costChrgCdeSrchMsclens" || field == "invChrgCdeSrchMsclens" || field == "ChrgCdeSrchMsclens" || field == "logChargCdearch") {
            newurl = "/Search/getChargCodeListMsclens?service="+parameterData;
        }
        if ( field == "ChrgCdeSrchMsclens2" ) {
            newurl = "/Search/getChargCodeListMsclens2?service=" + parameterData.cuscd + "&option=" + parameterData.option;
        }
        if (field == "receiptTYypeSearch") {
            newurl = "/Search/getReceiptTypes";
        }
        if (field == "divisionSearch") {
            newurl = "/Search/getDivisions";
        }
        if (field == "invoceReceiptSrch") {
            newurl = "/Search/debtInvoiceSearch?sroth=" + parameterData.srChk + "&srothval=" + parameterData.srkVal + "&cusCd=" + parameterData.cusCd + "&type=" + parameterData.type;
        }
        if (field == "invoceSrch") {
            newurl = "/Search/debtInvoiceSearchNew";
        }
        if (field == "profCenSrch") {
            newurl = "/Search/getAllProfitCenters";
        }
        if (field == "gftVouReceipt") {
            newurl = "/Search/getGVISUVouchers";
        }
        if (field == "receiptSearch") {
            newurl = "/Search/getReceiptEntries?unallow=" + parameterData.chk + "&recTyp=" + parameterData.recTyp;
        }
        if (field == "rptCnelSrch") {
            newurl = "/Search/getChannels";
        }
        if (field == "currencysearch" ||field == "currencysearchnew" || field == "currencysearchfrm" || field == "currencysearchto") {
            newurl = "/Search/GetCurrency";
        }
        if (field == "rptSubCnelSrch") {
            newurl = "/Search/getSubChannels?channel="+parameterData;
        }
        if (field == "costSerCdeSrch") {
            newurl = "/Search/getServiceCodes";
        }
        if (field == "costShetRefSrch") {
            newurl = "/Search/getCoseSheets";
        }
        if (field == "facComSearch") {
            newurl = "/Search/GetLogCompanies";
        }
        if (field == "logSheetSearch") {
            newurl = "/Search/GetLogSheets?selCompany=" + parameterData.selCompany + "&profCen=" + parameterData.profCen;
        }
        if (field == "logEnqSearch") {
            newurl = "/Search/GetLogEnquiries";
        }
        if (field == "logDriSearch" || field == "driverSrch" || field == "driverSrchTran") {
            newurl = "/Search/GetLogDrivers";
        }
        if (field == "logVehiSearch") {
            newurl = "/Search/GetLogVehicles";
        }
        if( field == "receLogEnqSearch"){
            newurl = "/Search/getAllEnquirySearch";
        }
        if (field == "ProfitCenterGlobe" || field=="costProfCenSrch") {
            newurl = "/Search/getUserProfitCenters";
        }
        if (field == "rentalAgentSrch") {
            newurl = "/Search/getCustomersByType?type=" + parameterData.type;
        }
        if (field == "pckFacLocSrch" || field == "dropFacLocSrch") {
            newurl = "/Search/getFacLocation";
        }
        if (field == "othChargCdearch") {
            newurl = "/Search/getOtherChargesForTransport?service=" + parameterData;
        }
        if (field == "depositSrch") {
            newurl = "/Search/getDepositAmount";
        }
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: newurl,
            data: {pgeNum: pgeNum, pgeSize: pgeSize, searchFld: searchFld, searchVal: searchVal},
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
                            jQuery('body').css('cursor', 'default');
                            jQuery(".se-pre-con").fadeOut("slow");
                            //if (check == true) {
                            jQuery('#paging').empty();
                            paging(result.totalDoc, pgeNum, false);
                            check = false;
                            //}
                        }
                    } else {
                        jQuery('#myModal').css('cursor', 'default');
                        jQuery('body').css('cursor', 'default');
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
    function setSerchPanel(tableValues, headerKeys) {

        //if (tableValues != null) {
        //    if (tableValues.length > 0) {
        if (jQuery('.table-responsive tbody').length > 0) {
            jQuery('.table-responsive tbody').empty();
        }
        if (tableValues != null) {
            if (tableValues.length > 0) {
                if (field == "cusCode" || field == "cusCodeForTraEnq" || field == "cusCodeEnq" || field == "guesscusCodeEnq" || field == "cusCodeForCost" || field == "cusCodeInv" || field == "srchCusReceipt" || field == "salesCusSrch" || field == "costCusSrch" || field == "cusCodeForQuot" || field == "rentalAgentSrch" || field == "othPrtyCus" || field == "othConCusSrch" || field == "payConCusfSrch") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].Mbe_cd +
                            '</td><td>' + tableValues[i].Mbe_name +
                            '</td><td>' + tableValues[i].Mbe_nic +
                            '</td><td>' + tableValues[i].Mbe_mob +
                            '</td><td>' + tableValues[i].Mbe_br_no + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Customer Search");
                } else if (field == "perTown" || field == "preTown" || field == "pickTownSrch" || field == "dropTownSrch" || field == "chgcdfromTownSrch" || field == "chgcdtoTownSrch" || field == "chgcdfromTownSrchTrn" || field == "chgcdtoTownSrchTrn") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].mt_desc +
                            '</td><td>' + tableValues[i].mdis_desc +
                            '</td><td>' + tableValues[i].mpro_desc +
                            '</td><td>' + tableValues[i].mt_cd + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Town Search");
                } else if (field == "ProfitCenter" || field == "ProfitCenterGlobe") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].Mpc_cd +
                            '</td><td>' + tableValues[i].Mpc_desc +
                            '</td><td>' + tableValues[i].Mpc_add_1 +
                            '</td><td>' + tableValues[i].Mpc_chnl + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Profit Center Search");
                } else if (field == "fleet") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].MSTF_REGNO +
                            '</td><td>' + tableValues[i].MSTF_VEH_TP +
                            '</td><td>' + tableValues[i].MSTF_MODEL +
                            '</td><td>' + tableValues[i].MSTF_OWN_NM + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Fleet Search");
                } else if (field == "Country" || field == "DCountry" || field == "ChfCountry" || field == "ChtCountry" || field == "reseCountry" || field == "presentCountry" || field == "crCountry") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].MCU_CD +
                            '</td><td>' + tableValues[i].MCU_DESC +
                            '</td><td>' + tableValues[i].MCU_REGION_CD +
                            '</td><td>' + tableValues[i].MCU_CAPITAL + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Country Search");
                } else if (field == "driverall") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].MSTF_REGNO +
                            '</td><td>' + tableValues[i].MSTF_VEH_TP +
                            '</td><td>' + tableValues[i].MSTF_OWN_NM +
                            '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Vehicle Search");
                } else if (field == "insby") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].MSP_CD +
                            '</td><td>' + tableValues[i].MSP_DESC + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Service Provider Search");
                } else if (field == "employee") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].MEMP_EPF +
                            '</td><td>' + tableValues[i].MEMP_CAT_SUBCD +
                            '</td><td>' + tableValues[i].MEMP_FIRST_NAME +
                            '</td><td>' + tableValues[i].MEMP_LAST_NAME +
                            '</td><td>' + tableValues[i].MEMP_NIC + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Employee Search");
                } else if (field == "employeedet") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                             '</td><td>' + tableValues[i].MEMP_EPF +
                            '</td><td>' + tableValues[i].MEMP_FIRST_NAME +
                            '</td><td>' + tableValues[i].MEMP_LAST_NAME +
                            '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Driver Search");
                } else if (field == "bnkAcc" ) {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].MSBA_ACC_CD +
                            '</td><td>' + tableValues[i].MSBA_ACC_DESC + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Bank Search");
                } else if (field == "chkBnkAcc" || field == "credBnkAcc" || field == "debtBnkAcc") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].MBI_CD +
                            '</td><td>' + tableValues[i].MBI_DESC +
                            '</td><td>' + tableValues[i].MBI_ID + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Bank Search");
                } else if (field == "bnkChqBranch") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].MBB_CD +
                            '</td><td>' + tableValues[i].MBB_DESC + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Bank Branch Search");
                } else if (field == "advRefNo") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].SAR_RECEIPT_NO +
                            '</td><td>' + tableValues[i].SAR_MANUAL_REF_NO +
                            '</td><td>' + tableValues[i].SAR_RECEIPT_DATE +
                            '</td><td>' + tableValues[i].SAR_ANAL_3 +
                            '</td><td>' + tableValues[i].SAR_USED_AMT +
                            '</td><td>' + tableValues[i].SAR_DEBTOR_CD + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Advance Search");
                } else if (field == "credNteRefNo") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].SAH_INV_NO +
                            '</td><td>' + tableValues[i].SAH_MAN_REF +
                            '</td><td>' + tableValues[i].SAH_REF_DOC +
                            '</td><td>' + tableValues[i].CREDIT_AMT  + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Credit Note Search");
                } else if (field == "giftVoucherSrch") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].GVP_PAGE +
                            '</td><td>' + tableValues[i].GVP_REF +
                            '</td><td>' + tableValues[i].GVP_BOOK +
                            '</td><td>' + tableValues[i].GVP_STUS +
                            '</td><td>' + my_date_format( tableValues[i].GVP_VALID_FROM) +
                            '</td><td>' + my_date_format(tableValues[i].GVP_VALID_TO) +
                            '</td><td>' + my_date_format(tableValues[i].GVP_CRE_DT) +
                            '</td><td>' + tableValues[i].GVP_BAL_AMT + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Gift Voucher Search");
                }else if (field == "loyalCrdSrch") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].SALCM_NO +
                            '</td><td>' + tableValues[i].SALCM_CD_SER +
                            '</td><td>' + tableValues[i].SALCM_LOTY_TP + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Loyalty Card Search");
                } else if (field == "InvEnq" || field == "costEnqSrch" || field == "costEnqIdSrch" || field == "InvEnqNew" || field == "quotEnqIdSrch" || field == "feedEnqIdSrch") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].GCE_ENQ_ID +
                            '</td><td>' + tableValues[i].GCE_REF +
                            '</td><td>' + tableValues[i].GCE_ENQ_TP +
                            '</td><td>' + tableValues[i].GCE_CUS_CD +
                            '</td><td>' + tableValues[i].GCE_NAME +
                            '</td><td>' + tableValues[i].GCE_ADD1 + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Transport Enquiry Search");
                } else if (field == "transportEnq" || field == "chktransportEnq") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].GCE_ENQ_ID +
                            '</td><td>' + tableValues[i].GCE_REF +
                            '</td><td>' + tableValues[i].GCE_CUS_CD +
                            '</td><td>' + tableValues[i].GCE_NAME +
                            '</td><td>' + tableValues[i].GCE_ADD1 + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Transport Enquiry Search");
                } else if (field == "EnqByNic") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].GCE_ENQ_ID +
                            '</td><td>' + tableValues[i].GCE_REF +
                            '</td><td>' + tableValues[i].GCE_ENQ_TP +
                            '</td><td>' + tableValues[i].GCE_CUS_CD +
                            '</td><td>' + tableValues[i].GCE_NAME +
                             '</td><td>' + tableValues[i].GCE_NIC +
                            '</td><td>' + tableValues[i].GCE_ADD1 + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Transport Enquiry Search");
                }
                //else if (field == "driverSrch") {
                //    for (i = 0; i < tableValues.length; i++) {
                //        var a = "";
                //        (i % 2 == 1) ? a = 'class="coloured"' : "";
                //        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                //            '</td><td>' + tableValues[i].MEMP_EPF +
                //            '</td><td>' + tableValues[i].MEMP_FIRST_NAME +
                //            '</td><td>' + tableValues[i].MEMP_LAST_NAME +
                //            '</td><td>' + tableValues[i].MEMP_MOBI_NO +
                //            '</td><td>' + tableValues[i].MEMP_NIC + '</td></tr>');
                //    }
                //    jQuery(".serch-panel-title").empty();
                //    jQuery(".serch-panel-title").html("Driver Search");
                //}
                else if (field == "transportFleet") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].MSTF_REGNO +
                            '</td><td>' + tableValues[i].MSTF_BRD +
                            '</td><td>' + tableValues[i].MSTF_MODEL +
                            '</td><td>' + tableValues[i].MSTF_VEH_TP +
                            '</td><td>' + tableValues[i].MSTF_OWN_NM +
                            '</td><td>' + tableValues[i].MSTF_OWN_CONT + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Fleet Search");
                } else if (field == "chrgCdeSrch" || field == "chrgCdeSrch2" || field == "costChrgCdeSrchTrans" || field == "invChrgCdeSrchTrans" || field == "ChrgCdeSrchTrans" || field == "logChrgCdeSrchTrans" || field == "parentSrchTrans" || field == "chrgCdeSrchWithType") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].STC_CD +
                            '</td><td>' + tableValues[i].STC_DESC +
                            '</td><td>' + tableValues[i].SERVICE_PROVIDER_DESC +
                            '</td><td>' + tableValues[i].STC_FRM_DT +
                            '</td><td>' + tableValues[i].STC_TO_DT +
                            '</td><td>' + tableValues[i].STC_FRM +
                            '</td><td>' + tableValues[i].STC_TO +
                            '</td><td>' + tableValues[i].STC_RT + '(' + tableValues[i].STC_CURR + ')' +
                            '</td><td>' + tableValues[i].STC_CLS +
                            '</td><td>' + tableValues[i].STC_VEH_TP + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Charge Code Search");
                } else if (field == "costChrgCdeSrchArival" || field == "invChrgCdeSrchArival" || field == "ChrgCdeSrchArival") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].SAC_CD +
                            '</td><td>' + tableValues[i].SAC_TIC_DESC +
                            '</td><td>' + tableValues[i].SERVICE_PROVIDER_DESC +
                            '</td><td>' + tableValues[i].SAC_FROM +
                            '</td><td>' + tableValues[i].SAC_TO +
                            '</td><td>' + tableValues[i].SAC_RT + '(' + tableValues[i].SAC_CUR + ')' +
                            '</td><td>' + tableValues[i].SAC_FRM_DT +
                            '</td><td>' + tableValues[i].SAC_TO_DT + '</td></tr>');
                    }

                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Arrival Charge Code Search");
                } else if (field == "costChrgCdeSrchMsclens" || field == "invChrgCdeSrchMsclens" || field == "ChrgCdeSrchMsclens" || field == "logChargCdearch" || field == "othChargCdearch" || field == "ChrgCdeSrchMsclens2") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].SSM_CD +
                            '</td><td>' + tableValues[i].SSM_DESC +
                            '</td><td>' + tableValues[i].SERVICE_PROVIDER_DESC +
                            '</td><td>' + tableValues[i].SSM_FRM_DT +
                            '</td><td>' + tableValues[i].SSM_TO_DT +
                            '</td><td>' + tableValues[i].SSM_RT + '(' + tableValues[i].SSM_CUR + ')' + '</td></tr>');
                    }

                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Miscellaneous Charge Code Search");
                } else if (field == "receiptTYypeSearch") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].MSRT_CD +
                            '</td><td>' + tableValues[i].MSRT_DESC + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Receipt Type Search");
                } else if (field == "divisionSearch") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].MSRD_CD +
                            '</td><td>' + tableValues[i].MSRD_DESC + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Division Search");
                } else if (field == "invoceReceiptSrch" | field == "invoceSrch") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].SIH_INV_NO +
                            '</td><td>' + tableValues[i].SIH_MAN_REF +
                             '</td><td>' + tableValues[i].SIH_BALANCE + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Invoice Search");
                } else if (field == "profCenSrch") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].MPC_CD +
                            '</td><td>' + tableValues[i].MPC_DESC + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Profit Center Search");
                } else if (field == "gftVouReceipt") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].MI_CD +
                            '</td><td>' + tableValues[i].MI_LONGDESC +
                            '</td><td>' + tableValues[i].MI_MODEL + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Gift Voucher Search");
                } else if (field == "receiptSearch") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].SIR_RECEIPT_NO +
                            '</td><td>' + tableValues[i].SIR_MANUAL_REF_NO +
                            '</td><td>' + tableValues[i].SIR_RECEIPT_DATE +
                            '</td><td>' + tableValues[i].SIR_ANAL_3 + '</td></tr>');
                    }

                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Receipt Search");
                } else if (field == "rptProfCenSrch" || field == "costProfCenSrch") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].Mpc_cd +
                            '</td><td>' + tableValues[i].Mpc_desc + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Profit Center Search");
                } else if (field == "rptCnelSrch") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].MPI_VAL +
                            '</td><td>' + tableValues[i].MSC_DESC + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Channel Search");

                } else if (field == "currencysearch" || field == "currencysearchnew" || field == "currencysearchfrm" || field == "currencysearchto") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].MCR_CD +
                            '</td><td>' + tableValues[i].MCR_DESC + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Currency Search");
                } else if (field == "rptSubCnelSrch") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].MPI_VAL +
                            '</td><td>' + tableValues[i].MSSC_DESC + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Sub Channel Search");
                } else if (field == "costSerCdeSrch") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].MCC_CD +
                            '</td><td>' + tableValues[i].MCC_DESC + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Service Code Search");
                } else if (field == "costShetRefSrch" || field == "costEnqIdSrch") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].QCH_COST_NO +
                            '</td><td>' + tableValues[i].QCH_REF + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Cost Sheet Search");
                } else if (field == "facComSearch") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].MFB_FACCOM +
                            '</td><td>' + tableValues[i].MFB_FACPC + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Company Search");
                } else if (field == "logSheetSearch") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].TLH_LOG_NO +
                            '</td><td>' + tableValues[i].TLH_REQ_NO +
                            '</td><td>' + tableValues[i].TLH_CUS_CD +
                            '</td><td>' + tableValues[i].TLH_DRI_CD +
                            '</td><td>' + tableValues[i].TLH_FLEET + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Log Sheet Search");
                } else if (field == "logEnqSearch" || field == "receLogEnqSearch") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].GCE_ENQ_ID +
                            '</td><td>' + tableValues[i].GCE_REF +
                            '</td><td>' + tableValues[i].GCE_CUS_CD +
                            '</td><td>' + tableValues[i].GCE_NAME +
                            '</td><td>' + tableValues[i].GCE_ADD1 + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Enquiry Search");
                } else if (field == "logCusSearch") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].Mbe_cd +
                            '</td><td>' + tableValues[i].Mbe_name +
                            '</td><td>' + tableValues[i].MBE_ADD1 +
                            '</td><td>' + tableValues[i].MBE_ADD2 +
                            '</td><td>' + tableValues[i].Mbe_nic +
                            '</td><td>' + tableValues[i].Mbe_mob +
                            '</td><td>' + tableValues[i].Mbe_br_no +
                            '</td><td>' + tableValues[i].MBE_TAX_NO + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Customer Search");
                } else if (field == "logDriSearch" || field == "driverSrch" || field == "driverSrchTran") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].MEMP_EPF +
                            '</td><td>' + tableValues[i].MEMP_FIRST_NAME +
                            '</td><td>' + tableValues[i].MEMP_LAST_NAME +
                            '</td><td>' + tableValues[i].MEMP_MOBI_NO +
                            '</td><td>' + tableValues[i].MEMP_NIC + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Driver Search");
                } else if (field == "logVehiSearch") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].MSTF_REGNO +
                            '</td><td>' + tableValues[i].MSTF_BRD +
                            '</td><td>' + tableValues[i].MSTF_MODEL +
                            '</td><td>' + tableValues[i].MSTF_VEH_TP +
                            '</td><td>' + tableValues[i].MSTF_OWN_NM +
                            '</td><td>' + tableValues[i].MSTF_OWN_CONT + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Vehicle Search");
                } else if (field == "popCusDet") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].Mbe_cd +
                            '</td><td>' + tableValues[i].Mbe_nic +
                            '</td><td>' + tableValues[i].Mbe_pp_no +
                            '</td><td>' + tableValues[i].Mbe_mob +
                            '</td><td>' + tableValues[i].Mbe_name + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Customer Search");
                } else if (field == "pckFacLocSrch" || field == "dropFacLocSrch") {
                    console.log("right now");
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].FAC_CODE +
                            '</td><td>' + tableValues[i].FAC_DESC + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Facility Location Search");
                } else if (field == "depositSrch") {
                    for (i = 0; i < tableValues.length; i++) {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].GCD_CD +
                            '</td><td>' + tableValues[i].GCD_LBLTY_AMT +
                            '</td><td>' + tableValues[i].GCD_DPST_AMT +
                            '</td><td>' + tableValues[i].GCD_DAILY_RNT_CD +
                             '</td><td>' + tableValues[i].GCD_DAILY_RNT_AMT + '</td></tr>');
                    }
                    jQuery(".serch-panel-title").empty();
                    jQuery(".serch-panel-title").html("Deposit");
                }
            }
        } else {
            if (jQuery('.table-responsive tbody').length > 0) {
                jQuery('.table-responsive tbody').append("<tr><td style=' border:none; color: #ff6666; position: absolute; width: 100%; font-weight: bold;'>No items found for this search criteria.</td></tr>");
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
            jQuery(this).css('color', 'blue');
        });
        jQuery('tr', '#myModal table tbody').dblclick(function () {
            setValue(field);
        });
    }
    function setValue(field) {
        if (jQuery("#myModal").is(":visible") == true) {
            if (jQuery('tr.selected td', '#myModal table tbody').length > 0) {
                if (field == "cusCode") {
                    jQuery('#Mbe_cd').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Mbe_cd').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Mbe_cd').focus();
                } else if (field == "perTown") {
                    jQuery('#Mbe_town_cd').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Mbe_town_cd').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Mbe_town_cd').focus();
                }
                else if (field == "preTown") {
                    jQuery('#Mbe_cr_town_cd').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Mbe_cr_town_cd').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Mbe_cr_town_cd').focus();
                } else if (field == "ProfitCenter") {
                    jQuery('#profitcenter').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#profitcenter').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#profitcenter').focus();
                } else if (field == "employee") {
                    jQuery('#MEMP_CD').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#MEMP_CD').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#MEMP_CD').focus();
                } else if (field == "cusCodeEnq") {
                    jQuery('#GCE_CUS_CD').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#GCE_CUS_CD').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#GCE_CUS_CD').focus();

                } else if (field == "guesscusCodeEnq") {
                    jQuery('#GCE_CONT_CD').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#GCE_CONT_CD').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#GCE_CONT_CD').focus();
                } else if (field == "employeedet") {
                    jQuery('#MFD_DRI').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    var value2 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(2)').text();
                    var value3 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(4)').text();
                    jQuery('div.div2').empty();
                    jQuery('div.div2').append(
                         'Name-:' + value2 + '<br/>'
                         + 'Mobile-:' + value3
                     );
                    jQuery('#MFD_DRI').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#MFD_DRI').focus();
                } else if (field == "fleet") {
                    jQuery('#MSTF_REGNO').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#MSTF_REGNO').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#MSTF_REGNO').focus();
                } else if (field == "driverall") {
                    jQuery('#MFD_VEH_NO').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    var value2 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(2)').text();
                    var value3 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(3)').text();
                    var value4 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(4)').text();
                    jQuery('div.div1').empty();
                    jQuery('div.div1').append(
                       'Type-:' + value2 + '<br/>'
                     + 'Owner-:' + value3 + '<br/>'
                     + 'Contact-:' + value4
                     );

                    jQuery('#MFD_VEH_NO').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#MFD_VEH_NO').focus();
                } else if (field == "insby") {
                    jQuery('#MSTF_INSU_COM').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#MSTF_INSU_COM').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#MSTF_INSU_COM').focus();
                } else if (field == "bnkAcc") {
                    jQuery('#Deposit_bank_cd').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Deposit_bank_cd').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Deposit_bank_cd').focus();
                } else if (field == "chkBnkAcc") {
                    jQuery('#cheque-bank').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(3)').text();
                    jQuery('#cheque-bank').val(value);
                    var hidevalue = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Cheque_bnk_cd').val(hidevalue);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Cheque_branch_cd').val("");
                    jQuery('#cheque-bank').focus();
                } else if (field == "bnkChqBranch") {
                    jQuery('#Cheque_branch_cd').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Cheque_branch_cd').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Cheque_branch_cd').focus();
                } else if (field == "credBnkAcc") {
                    jQuery('#Cred_crd_bank').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(3)').text();
                    jQuery('#Cred_crd_bank').val(value);
                    var hidevalue = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Cred_crd_bank_value').val(hidevalue);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Cred_crd_bank').focus();
                } else if (field == "debtBnkAcc") {
                    jQuery('#Debt_bank_name').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(3)').text();
                    jQuery('#Debt_bank_name').val(value);
                    var hidevalue = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Debt_bank_cd').val(hidevalue);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Debt_bank_name').focus();

                } else if (field == "advRefNo") {
                    jQuery('#Advan_ref_no').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Advan_ref_no').val(value);
                    var amtvalue = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(5)').text();
                    jQuery('#Advan_ref_amount').val(amtvalue);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Advan_ref_no').focus();
                } else if (field == "credNteRefNo") {
                    jQuery('#Cred_note_ref_no').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Cred_note_ref_no').val(value);
                    var amtvalue = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(4)').text();
                    jQuery('#Cred_note_ref_amount').val(amtvalue);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Cred_note_ref_no').focus();
                } else if (field == "giftVoucherSrch") {
                    jQuery('#Gift_vouche_no').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Gift_vouche_no').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Gift_vouche_no').focus();
                } else if (field == "loyalCrdSrch") {
                    jQuery('#Lore_crd_no').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Lore_crd_no').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Lore_crd_no').focus();
                } else if (field == "cusCodeForTraEnq") {
                    jQuery('#GCE_CUS_CD').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#GCE_CUS_CD').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#GCE_CUS_CD').focus();
                } else if (field == "transportEnq") {
                    jQuery('#GCE_ENQ_ID').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#GCE_ENQ_ID').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#GCE_ENQ_ID').focus();
                } else if (field == "driverSrch") {
                    jQuery('#drivercd').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#drivercd').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#drivercd').focus();

                } else if (field == "transportFleet") {
                    jQuery('#vehiregno').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#vehiregno').val(value);
                    jQuery('#GCE_FLEET').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#vehiregno').focus();
                } else if (field == "Country") {
                    jQuery('#GCE_FRM_CONTRY').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#GCE_FRM_CONTRY').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#GCE_FRM_CONTRY').focus();
                } else if (field == "ChfCountry") {
                    jQuery('#SAC_FROM').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#SAC_FROM').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#SAC_FROM').focus();
                } else if (field == "ChtCountry") {
                    jQuery('#SAC_TO').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#SAC_TO').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#SAC_TO').focus();
                } else if (field == "DCountry") {
                    jQuery('#GCE_DEST_CONTRY').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#GCE_DEST_CONTRY').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#GCE_DEST_CONTRY').focus();
                } else if (field == "pickTownSrch") {
                    jQuery('#GCE_FRM_TN').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#GCE_FRM_TN').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#GCE_FRM_TN').focus();
                } else if (field == "dropTownSrch") {
                    jQuery('#GCE_TO_TN').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#GCE_TO_TN').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#GCE_TO_TN').focus();
                } else if (field == "chgcdfromTownSrch") {
                    jQuery('#SAC_FROM').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#SAC_FROM').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#SAC_FROM').focus();
                } else if (field == "chgcdtoTownSrch") {
                    jQuery('#SAC_TO').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#SAC_TO').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#SAC_TO').focus();
                } else if (field == "chgcdfromTownSrchTrn") {
                    jQuery('#STC_FRM').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#STC_FRM').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#STC_FRM').focus();
                } else if (field == "chgcdtoTownSrchTrn") {
                    jQuery('#STC_TO').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#STC_TO').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#STC_TO').focus();
                } else if (field == "chrgCdeSrch" || field == "chrgCdeSrchWithType" || field == "chrgCdeSrch2") {
                    jQuery('#ChargCode').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#ChargCode').val(value);
                    //var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(7)').text();
                    //jQuery('#UnitRate').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#ChargCode').focus();
                } else if (field == "costChrgCdeSrchTrans") {
                    jQuery('#CostChargCode').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#CostChargCode').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    if (typeof jQuery("#ServiceByCus") != "undefined" && jQuery("#ServiceByCus").length > 0) {
                        var value1 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(3)').text();
                        jQuery("#ServiceByCus").val(value1);
                    }
                    jQuery('#CostChargCode').focus();
                } else if (field == "costChrgCdeSrchArival") {
                    jQuery('#CostChargCode').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#CostChargCode').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#CostChargCode').focus();
                    if (typeof jQuery("#ServiceByCus") !="undefined" && jQuery("#ServiceByCus").length > 0) {
                        var value1 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(3)').text();
                        jQuery("#ServiceByCus").val(value1);
                    }
                } else if (field == "costChrgCdeSrchMsclens") {
                    jQuery('#CostChargCode').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#CostChargCode').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#CostChargCode').focus();
                    if (typeof jQuery("#ServiceByCus") != "undefined" && jQuery("#ServiceByCus").length > 0) {
                        var value1 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(3)').text();
                        jQuery("#ServiceByCus").val(value1);
                    }
                } else if (field == "ChrgCdeSrchTrans") {
                    jQuery('#STC_CD').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#STC_CD').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#STC_CD').focus();
                    jQuery('#chgcd').val(value);
                } else if (field == "ChrgCdeSrchArival") {
                    jQuery('#SAC_CD').val("");

                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#SAC_CD').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#SAC_CD').focus();
                    jQuery('#chgcd').val(value);
                } else if (field == "ChrgCdeSrchMsclens") {
                    jQuery('#SSM_CD').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#SSM_CD').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#SSM_CD').focus();
                    jQuery('#chgcd').val(value);
                } else if (field == "ChrgCdeSrchMsclens2") {
                    jQuery('#SSM_CD').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#ChargCode').focus();
                    jQuery('#ChargCode').val(value);
                } else if (field == "invChrgCdeSrchTrans") {
                    jQuery('#SubType').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#SubType').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#SubType').focus();
                } else if (field == "invChrgCdeSrchArival") {
                    jQuery('#SubType').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#SubType').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#SubType').focus();
                } else if (field == "invChrgCdeSrchMsclens") {
                    jQuery('#SubType').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#SubType').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#SubType').focus();
                } else if (field == "cusCodeForCost") {
                    jQuery('#QCH_CUS_CD').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#QCH_CUS_CD').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#QCH_CUS_CD').focus();
                } else if (field == "cusCodeInv") {
                    jQuery('#Sah_cus_cd').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Sah_cus_cd').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Sah_cus_cd').focus();
                } else if (field == "InvEnq") {
                    jQuery('#enq_id').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#enq_id').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#enq_id').focus();
                } else if (field == "InvEnqNew") {
                    jQuery('#job_number2').val("");
                    jQuery('#job_number').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#job_number2').val(value);
                    jQuery('#job_number').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#job_number2').focus();
                } else if (field == "receiptTYypeSearch") {
                    jQuery('#Sir_receipt_type').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Sir_receipt_type').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Sir_receipt_type').focus();
                }else if(field == "srchCusReceipt"){
                    jQuery('#Sir_debtor_cd').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Sir_debtor_cd').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Sir_debtor_cd').focus();
                } else if (field == "invoceReceiptSrch") {
                    jQuery('#InvoiceAdd').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#InvoiceAdd').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#InvoiceAdd').focus();
                } else if (field == "invoceSrch") {
                    jQuery('#Sah_inv_no').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Sah_inv_no').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Sah_inv_no').focus();
                } else if (field == "profCenSrch") {
                    jQuery('#OthSrVal').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#OthSrVal').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#OthSrVal').focus();
                } else if (field == "divisionSearch") {
                    jQuery('#Division').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Division').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Division').focus();
                } else if (field == "gftVouReceipt") {
                    jQuery('#VoucherCode').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#VoucherCode').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#VoucherCode').focus();
                } else if (field == "receiptSearch") {
                    jQuery('#Sir_receipt_no').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Sir_receipt_no').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Sir_receipt_no').focus();
                } else if (field == "rptProfCenSrch") {
                    jQuery('#ProfCen').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#ProfCen').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#ProfCen').focus();
                }else if(field == "rptCnelSrch"){
                    jQuery('#Channel').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Channel').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Channel').focus();
                } else if (field == "currencysearch") {
                    jQuery('#Search_frm_currency').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Search_frm_currency').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Search_frm_currency').focus();
                } else if (field == "currencysearchnew") {
                    jQuery('#Search_to_currency').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Search_to_currency').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Search_to_currency').focus();
                } else if (field == "currencysearchfrm") {
                    jQuery('#Mer_cur').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Mer_cur').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Mer_cur').focus();
                } else if (field == "currencysearchto") {
                    jQuery('#Mer_to_cur').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Mer_to_cur').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Mer_to_cur').focus();
                } else if (field == "rptSubCnelSrch") {
                    jQuery('#SubChannel').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#SubChannel').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#SubChannel').focus();
                } else if (field == "salesCusSrch") {
                    jQuery('#Customer').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Customer').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Customer').focus();
                } else if (field == "costProfCenSrch") {
                    jQuery('#ProfitCenter').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#ProfitCenter').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#ProfitCenter').focus();
                } else if (field == "costCusSrch") {
                    jQuery('#Customer').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Customer').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Customer').focus();
                } else if (field == "costEnqSrch") {
                    jQuery('#ReqNo').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#ReqNo').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#ReqNo').focus();
                } else if (field == "costSerCdeSrch") {
                    jQuery('#SerCde').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#SerCde').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#SerCde').focus();
                } else if (field == "costShetRefSrch") {
                    jQuery('#CostShtRef').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#CostShtRef').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#CostShtRef').focus();
                }else if( field == "costEnqIdSrch"){
                    jQuery('#QCH_OTH_DOC').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#QCH_OTH_DOC').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#QCH_OTH_DOC').focus();
                } else if (field == "facComSearch") {
                    jQuery('#TLH_COM').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    var value2 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(2)').text();
                    jQuery("#TLH_PC").val(value2);
                    jQuery('#TLH_COM').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#TLH_COM').focus();
                } else if (field == "logSheetSearch") {
                    jQuery('#TLH_LOG_NO').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#TLH_LOG_NO').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#TLH_LOG_NO').focus();
                } else if (field == "logCusSearch") {
                    jQuery('#TLH_CUS_CD').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#TLH_CUS_CD').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#TLH_CUS_CD').focus();
                } else if (field == "logDriSearch") {
                    jQuery('#TLH_DRI_CD').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#TLH_DRI_CD').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#TLH_DRI_CD').focus();
                } else if (field == "logVehiSearch") {
                    jQuery('#TLH_FLEET').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#TLH_FLEET').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#TLH_FLEET').focus();
                } else if (field == "logChargCdearch") {
                    jQuery('#CostChargCode').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#CostChargCode').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#CostChargCode').focus();
                } else if (field == "popCusDet") {
                    jQuery('#cusCd').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    var value1 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(2)').text();
                    var value2 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(3)').text();
                    var value3 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(4)').text();
                    var value4 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(5)').text();
                    jQuery('#cusCd').val(value);
                    jQuery('#cusNIC').val(value1);
                    jQuery('#Passport').val(value2);
                    jQuery('#Mobile').val(value3);
                    jQuery('#cusName').val(value4);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#cusCd').focus();
                } else if (field == "logEnqSearch") {
                    jQuery('#TLH_REQ_NO').val("");
                    jQuery('#TLH_CUS_CD').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    var value1 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(3)').text();
                    jQuery('#TLH_REQ_NO').val(value);
                    jQuery('#TLH_CUS_CD').val(value1);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#TLH_REQ_NO').focus();
                } else if (field == "chktransportEnq") {
                    jQuery('#CHK_ENQ_ID').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#CHK_ENQ_ID').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#CHK_ENQ_ID').focus();
                }else if(field == "logChrgCdeSrchTrans"){
                    jQuery('#CostChargCode').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#CostChargCode').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#CostChargCode').focus();
                } else if (field == "ProfitCenterGlobe") {
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
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
                } else if (field == "receLogEnqSearch") {
                    jQuery('#Sir_manual_ref_no').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Sir_manual_ref_no').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Sir_manual_ref_no').focus();
                } else if (field == "cusCodeForQuot") {
                    jQuery('#QCH_CUS_CD').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#QCH_CUS_CD').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#QCH_CUS_CD').focus();
                } else if (field == "quotEnqIdSrch") {
                    jQuery('#QCH_OTH_DOC').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#QCH_OTH_DOC').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#QCH_OTH_DOC').focus();
                } else if (field == "feedEnqIdSrch") {
                    jQuery('#EnqId').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#EnqId').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#EnqId').focus();
                } else if (field == "reseCountry") {
                    jQuery('#GCE_CITY_OF_ISSUE').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#GCE_CITY_OF_ISSUE').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#GCE_CITY_OF_ISSUE').focus();
                } else if (field == "rentalAgentSrch") {
                    jQuery('#GCE_RENTAL_AGENT').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#GCE_RENTAL_AGENT').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#GCE_RENTAL_AGENT').focus();
                } else if (field == "pckFacLocSrch") {
                    jQuery('#GCE_FRM_TN').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#GCE_FRM_TN').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#GCE_FRM_TN').focus();
                } else if (field == "dropFacLocSrch") {
                    jQuery('#GCE_TO_TN').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#GCE_TO_TN').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#GCE_TO_TN').focus();
                } else if (field == "presentCountry") {
                    jQuery('#Mbe_country_cd').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Mbe_country_cd').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Mbe_country_cd').focus();
                } else if (field == "crCountry") {
                    jQuery('#Mbe_cr_country_cd').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Mbe_cr_country_cd').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Mbe_cr_country_cd').focus();
                } else if (field == "parentSrchTrans") {
                    jQuery('#SSM_PARENT_CD').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#SSM_PARENT_CD').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#SSM_PARENT_CD').focus();
                } else if (field == "othChargCdearch") {
                    jQuery('#ChargCode').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#ChargCode').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#ChargCode').focus();
                } else if (field == "othPrtyCus") {
                    jQuery('#Sir_oth_partycd').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Sir_oth_partycd').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Sir_oth_partycd').focus();
                } else if (field == "othConCusSrch") {
                    jQuery('#Other_Party').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Other_Party').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Other_Party').focus();
                } else if (field == "payConCusfSrch") {
                    jQuery('#Customer').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#Customer').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#Customer').focus();
                } else if (field == "depositSrch") {
                    jQuery('#GCE_LBLTY_CHG').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(2)').text();
                    jQuery('#GCE_LBLTY_CHG').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#GCE_LBLTY_CHG').focus();
                } else if (field == "driverSrchTran") {
                    jQuery('#GCE_DRIVER').val("");
                    var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                    jQuery('#GCE_DRIVER').val(value);
                    jQuery('#myModal').modal('hide');
                    jQuery("#KeyWord").val("");
                    jQuery('#GCE_DRIVER').focus();
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

        jQuery('#myModal').on('shown.bs.modal', function () {
            jQuery("#KeyWord").focus();
            jQuery(".modal-content").draggable({ handle: ".Title.panel-heading", containment: "body" });
            jQuery('.Title.panel-heading').css('cursor', 'move');
        });
        jQuery(document).keypress(function (evt) {
            if (evt.keyCode == 27) {
                if (jQuery('#myModal:visible').length == 1) {
                    jQuery("#KeyWord").val("");
                    jQuery('#myModal').modal('hide');
                }
            }
        });
        jQuery('#myModal .close-btn').click(function (e) {
            e.preventDefault();
            jQuery("#KeyWord").val("");
            jQuery('#myModal').modal('hide');
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
                jQuery('#myModal').css('cursor', 'wait');
                jQuery(".se-pre-con").fadeIn("slow");
                var searchFld = jQuery('select.filter-key-cls').val();
                var searchVal = jQuery('input#KeyWord').val();
                var pgeSize = jQuery('.modal-content .cls-select-page-cont').val();
                loadDetails(num, pgeSize, searchFld, searchVal, headerKeys);
            }
        });

    }
    jQuery('.modal-content .cls-select-page-cont').change(function (e) {
        jQuery('#myModal').css('cursor', 'wait');
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
                jQuery('#myModal').css('cursor', 'wait');
                jQuery(".se-pre-con").fadeIn("slow");
                var searchFld = jQuery('select.filter-key-cls').val();
                var searchVal = jQuery('input#KeyWord').val();
                var pgeSize = jQuery('.modal-content .cls-select-page-cont').val();
                var pgeNum = 1;
                loadDetails(pgeNum, pgeSize, searchFld, searchVal, headerKeys, true);
            } else {
                setValue(field);
            }

        } else if (e.keyCode == 40) {
            if (jQuery('.modal-content #KeyWord').is(':focus') == true) {
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
   
});