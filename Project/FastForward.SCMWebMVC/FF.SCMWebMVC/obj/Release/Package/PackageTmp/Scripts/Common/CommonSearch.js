var first = true;
var first_time = true;
var headerKeys;
var field = "";
var parameterData = "";
var company = "";
function CommonSearch(headerKeys, selectedField, data) {
    headerKeys = headerKeys;
    field = selectedField;
    if (data != null) {
        parameterData = data;
    } else {
        parameterData = null;
    }
    
    if (headerKeys.length > 0) {
        var selecter = jQuery('#myModal .filter-key-cls');
        selecter.empty();
        for (i = 1; i < headerKeys.length; i++) {
            if (headerKeys[i] != "") {
                var newOption = jQuery('<option value="' + headerKeys[i] + '">' + headerKeys[i] + '</option>');
                selecter.append(newOption);
            }
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

    if (field == "ProfitCenter") {
        newurl = "/Search/getProfitCenters";
    }
    if (field == "ELProfitCenter") {
        newurl = "/Search/getProfitCenters";
    }
    if (field == "ELProfitCenteradd") {
        newurl = "/Search/getProfitCenters";
    }
    if (field == "employee" || field == "employeeNew" || field == "employee1") {
        newurl = "/Search/getEmployeeDetails";
    }
    if (field == "manager") {
        newurl = "/Search/getManagerDetails";
    }
    if (field == "SchemeType") {
        newurl = "/Search/getSchemeType";
    }
    if (field == "Scheme") {
        newurl = "/Search/getScheme";
    }
    if (field == "shwmanager") {
        newurl = "/Search/getShwManagerdata";
    }
    if (field == "locationmg") {
        newurl = "/Search/getMgProfitCenters?mgr=" + parameterData.mgr;
    }
    if (field == "employee2") {
        newurl = "/Search/getEmployeeDetails";
    }
    if (field == "employee3") {
        newurl = "/Search/getEmployeeDetails";
    }
    if (field == "employee4") {
        newurl = "/Search/getEmployeeDetails";
    }
    if (field == "employee5") {
        newurl = "/Search/getEmployeeDetails";
    }
    if (field == "employeecat") {
        newurl = "/Search/getEmployeeCategory";
    }
    if (field == "srchItemsM") {
        newurl = "/Search/getItems";
    }
    if (field == "srchItemModelM") {
        newurl = "/Search/getItemModel";
    }
    if (field == "srchItemBrandM") {
        newurl = "/Search/getItemBrands";
    }
    if (field == "srchMainCatM") {
        newurl = "/Search/getMainCategory";
    }
    if (field == "srchMainCatM2") {
        newurl = "/Search/getMainCategory";
    }
    if (field == "srchCategory2M") {
        newurl = "/Search/getCategory2";
    }
    if (field == "CommissionCode") {
        newurl = "/Search/CommissionCodeSearch";
    }
    if (field == "BonusCode" || field == "BonusCode2") {
        newurl = "/Search/BonusCodeSearch";
    }
    if (field == "ChartAccountCode") {
        newurl = "/Search/ChartAccCodeSearch";
    }
    if (field == "srchChnl") {
        newurl = "/Search/getLocHierarchy?type=" + parameterData.type + "&com=" + parameterData.Company;
    }
    if (field == "InvType") {
        newurl = "/Search/getInvoiceType";
    }
    if (field == "PriceCircula") {
        newurl = "/Search/getPriceCircula";
    }
    if (field == "InvType2") {
        newurl = "/Search/getInvoiceType";
    }
    if (field == "srchSubChnlM") {
        newurl = "/Search/getLocHierarchy?channel=" + parameterData.chnl + "&type=" + parameterData.type + "&com=" + parameterData.Company;
    }
    if (field == "HandAcc") {
        newurl = "/Search/HandOverAccCodeSearch?pc=" + parameterData.pc + "&date=" + parameterData.date;
    }
    if (field == "srchRegionM") {
        newurl = "/Search/getLocHierarchy?channel=" + parameterData.chnl + "&subChannel=" + parameterData.sChnl + "&area=" + parameterData.area + "&type=" + parameterData.type + "&com=" + parameterData.Company;
    }
    if (field == "srchZoneM") {
        newurl = "/Search/getLocHierarchy?channel=" + parameterData.chnl + "&subChannel=" + parameterData.sChnl + "&area=" + parameterData.area + "&region=" + parameterData.regn + "&type=" + parameterData.type + "&com=" + parameterData.Company;
    }
    if (field == "srchPCM") {
        newurl = "/Search/getLocHierarchy?channel=" + parameterData.chnl + "&subChannel=" + parameterData.sChnl + "&area=" + parameterData.area + "&region=" + parameterData.regn + "&zone=" + parameterData.zone + "&type=" + parameterData.type + "&com=" + parameterData.Company;
    }
    if (field == "srchPCM2" || field == "srchPCM3") {
        newurl = "/Search/getLocHierarchy?channel=" + parameterData.chnl + "&subChannel=" + parameterData.sChnl + "&area=" + parameterData.area + "&region=" + parameterData.regn + "&zone=" + parameterData.zone + "&type=" + parameterData.type;
    }
    if (field == "srchPCM4" || field == "srchPCM4M" || field == "srchChnlAll") {
        newurl = "/Search/getLocHierarchyAll?channel=" + parameterData.chnl + "&subChannel=" + parameterData.sChnl + "&area=" + parameterData.area + "&region=" + parameterData.regn + "&zone=" + parameterData.zone + "&type=" + parameterData.type;
    }
    if (field == "CusCode") {
        newurl = "/Search/getCustomerDetails";
    }
    if (field == "btuval") {
        newurl = "/Search/getBTUVal?Cat=" + parameterData.Cat;
    }
    if (field == "circular") {
        newurl = "/Search/getCircular?Cat=" + parameterData.Cat;
    }
    if (field == "circularcbd") {
        newurl = "/Search/getCircularcbd";
    }
    if (field == "ProfitCentersRent") {
        newurl = "/Search/getProfitCenters";
    }
    if (field == "RentHeaders") {
        newurl = "/Search/getRentSCHDetail";
    }
    if (field == "PaymentTypes") {
        newurl = "/Search/getPaymentTypes";
    }
    if (field == "PaymentSubTypes") {
        newurl = "/Search/getPaymentSubTypes?p_type=" + parameterData.p_type;
    }
    if (field == "BonusCode3") {
        newurl = "/Search/BonusCodeSearch?schemeCode=" + parameterData.schemeCode;
    }
    if (field == "SchemaCodeSearch") {
        newurl = "/Search/SchemeNumberSearch?circularcode=" + parameterData.circularcode;
    }
    if (field == "SRCHPAYREQ") {
        newurl = "/Search/searchPaymentRequest?reqtp=" + parameterData;
    }
    if (field == "SRCHDEBTACC" || field == "SRCHCREACC" || field == "SRCHCREACC1") {
        newurl = "/Search/searchAccountnumbers";
    }
    if (field == "SRCHFILDSRCH") {
        newurl = "/Search/getSearchField";
    }
    if (field == "COMSRCHFLD") {
        newurl = "/Search/getDynamicSrchValues?field=" + parameterData.searchFld;
    }
    if (field == "SRCHPYTP") {
        newurl = "/Search/srchAccPaymentTypes";
    }
    if (field == "SRCHTAX") {
        newurl = "/Search/srchTaxTypes?creditor=" + parameterData;
    }
    if (field == "SRCHREFNO") {
        newurl = "/Search/srchAccountPODet?creditor=" + parameterData.creditor + "&type=" + parameterData.type;
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
                        jQuery('#myModal').css('cursor', 'default');
                        jQuery('body').css('cursor', 'default');
                        jQuery(".se-pre-con").fadeOut("slow");
                        jQuery('#paging').empty();
                        paging(result.totalDoc, pgeNum, false);
                        check = false;
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
    if (jQuery('.table-responsive tbody').length > 0) {
        jQuery('.table-responsive tbody').empty();
    }
    if (tableValues != null) {
        if (tableValues.length > 0) {
            if (field == "ProfitCenter" || field == "ProfitCenter1" || field == "ProfitCentersRent" || field == "ELProfitCenter" || field == "ELProfitCenteradd") {
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
                jQuery(".serch-panel-title").html("ProfitCenter Search");
            } else if (field == "locationmg") {
                console.log("inside tablem value");
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
                jQuery(".serch-panel-title").html("ProfitCenter Search");
            }
            else if (field == "employee" || field == "employeeNew" || field == "employee1")
            {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].ESEP_EPF +
                        '</td><td>' + tableValues[i].ESEP_CAT_SUBCD +
                        '</td><td>' + tableValues[i].ESEP_FIRST_NAME +
                        '</td><td>' + tableValues[i].ESEP_LAST_NAME +
                        '</td><td>' + tableValues[i].ESEP_NIC + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Employee Search");
            }
            else if (field == "manager") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].ESEP_EPF +
                        '</td><td>' + tableValues[i].ESEP_CAT_SUBCD +
                        '</td><td>' + tableValues[i].ESEP_FIRST_NAME +
                        '</td><td>' + tableValues[i].ESEP_LAST_NAME +
                        '</td><td>' + tableValues[i].ESEP_NIC + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Employee Search");
            }
            else if (field == "SchemeType") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].hst_cd +
                        '</td><td>' + tableValues[i].hst_desc + '</td></tr>');
                } 
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("SchemeType Search");
            }
            else if (field == "Scheme") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].hsd_cd +
                        '</td><td>' + tableValues[i].hsd_desc + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Scheme Search");
            }
            else if (field == "shwmanager") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].HMFA_MGR_CD +
                        '</td><td>' + tableValues[i].HMFA_MGR_NAME + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Manager Search");
            }
            else if (field == "employee2") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].ESEP_EPF +
                        '</td><td>' + tableValues[i].ESEP_CAT_SUBCD +
                        '</td><td>' + tableValues[i].ESEP_FIRST_NAME +
                        '</td><td>' + tableValues[i].ESEP_LAST_NAME +
                        '</td><td>' + tableValues[i].ESEP_NIC + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Employee Search");
            } else if (field == "employee3") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].ESEP_EPF +
                        '</td><td>' + tableValues[i].ESEP_CAT_SUBCD +
                        '</td><td>' + tableValues[i].ESEP_FIRST_NAME +
                        '</td><td>' + tableValues[i].ESEP_LAST_NAME +
                        '</td><td>' + tableValues[i].ESEP_NIC + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Employee Search");
            } else if (field == "employee4") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].ESEP_EPF +
                        '</td><td>' + tableValues[i].ESEP_CAT_SUBCD +
                        '</td><td>' + tableValues[i].ESEP_FIRST_NAME +
                        '</td><td>' + tableValues[i].ESEP_LAST_NAME +
                        '</td><td>' + tableValues[i].ESEP_NIC + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Employee Search");
            }
            else if (field == "employee5") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].ESEP_EPF +
                        '</td><td>' + tableValues[i].ESEP_CAT_SUBCD +
                        '</td><td>' + tableValues[i].ESEP_FIRST_NAME +
                        '</td><td>' + tableValues[i].ESEP_LAST_NAME +
                        '</td><td>' + tableValues[i].ESEP_NIC + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Employee Search");
            } else if (field == "employeecat")
            {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].ESEP_CAT_CD + '</td>'+
                       '<td>' + tableValues[i].ESEP_EPF + '</td>' + '</tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Employee Search");
            } else if (field == "srchItemsM") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].srtp_cd +
                        '</td><td>' + tableValues[i].srtp_desc + '</td></tr>'
                        );
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Item Search");
            } else if (field == "srchItemModelM") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].mm_cd +
                        '</td><td>' + tableValues[i].mm_desc + + '</td></tr>'
                        );
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Model Search");
            } else if (field == "srchItemBrandM") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].mb_cd +
                        '</td><td>' + tableValues[i].mb_desc +'</td></tr>'
                        );
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Brand Search");
            } else if (field == "srchMainCatM") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].main_cat_cd +
                        '</td><td>' + tableValues[i].main_cat_desc + '</td></tr>'
                        );
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Main Category Search");
            } else if (field == "srchMainCatM2") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].main_cat_cd +
                        '</td><td>' + tableValues[i].main_cat_desc + '</td></tr>'
                        );
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Main Category Search");
            }
            else if (field == "srchCategory2M") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].cat2_cd +
                        '</td><td>' + tableValues[i].cat2_desc +'</td></tr>'
                        );
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Sub Category Search");
            } else if (field == "CommissionCode") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].Rch_comm_cd + '</td></tr>'
                        );
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Commission Search");
            }
            else if (field == "BonusCode" || field == "BonusCode2") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";

                    //Added "Rbh_anal1" column to table -- Udesh 12-Oct-2018
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].Rbh_doc_no +
                        '</td><td>' + tableValues[i].Rbh_anal1 +
                        '</td></tr>'
                        );
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Bonus Search");
            }
            else if (field == "ChartAccountCode") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].rcg_acc_no + '</td></tr>'
                        );
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Bonus Search");
            } else if (field == "srchChnl") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].loc_hirch_cd +
                        '</td><td>' + tableValues[i].loc_hirch_desc + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Channel Search");
            } else if (field == "srchChnlAll") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].loc_hirch_cd +
                        '</td><td>' + tableValues[i].loc_hirch_desc + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Channel Search");
            } else if (field == "InvType") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].srtp_cd +
                        '</td><td>' + tableValues[i].srtp_desc + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Invoice Type Search");
            } else if (field == "PriceCircula") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].srtp_cd +
                        '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Promotion Search");
            }
            else if (field == "InvType2") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].srtp_cd +
                        '</td><td>' + tableValues[i].srtp_desc + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Invoice Type Search");
            } else if (field == "srchSubChnlM") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].loc_hirch_cd +
                        '</td><td>' + tableValues[i].loc_hirch_desc +'</td></tr>'
                        );
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Sub Channel ");
            } else if (field == "srchRegionM") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].loc_hirch_cd +
                        '</td><td>' + tableValues[i].loc_hirch_desc + '</tr>'
                        );
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Region Search");
            }
            else if (field == "srchZoneM") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].loc_hirch_cd +
                        '</td><td>' + tableValues[i].loc_hirch_desc +'</tr>'
                        );
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Zone Search");
            } else if (field == "srchPCM" || field == "srchPCM2" || field == "srchPCM3" || field == "srchPCM4" || field == "srchPCM4M") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].loc_hirch_cd +
                        '</td><td>' + tableValues[i].loc_hirch_desc + '</td></tr>'
                        );
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("PC Search");
            } else if (field == "circular") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].hdvr_circular + '</td></tr>'
                        );
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Circular Search");
            } else if (field == "circularcbd") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].hbp_circular + '</td></tr>'
                        );
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Circular Search");
            }
            else if (field == "CusCode") {
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
                jQuery(".serch-panel-title").html("Custormer Code");
            }
            else if (field == "RentHeaders") {
                for (i = 0; i < tableValues.length; i++) {
                    console.log(tableValues);
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].psh_no +
                        '</td><td>' + tableValues[i].psh_pc +
                        '</td><td class="hidecolumn">' + tableValues[i].psh_pay_tp + //3
                        '</td><td class="hidecolumn">' + tableValues[i].psh_pay_sb_tp +//4
                        '</td><td class="hidecolumn">' + tableValues[i].psh_add1 +//5
                        '</td><td class="hidecolumn">' + tableValues[i].psh_add2 +//6
                        '</td><td class="hidecolumn">' + tableValues[i].psh_anl1 +//7
                        '</td><td class="hidecolumn">' + tableValues[i].psh_dist +//8
                        '</td><td class="hidecolumn">' + tableValues[i].psh_rmk +//9
                        '</td><td class="hidecolumn">' + tableValues[i].psh_prv +//10
                        '</td><td class="hidecolumn">' + tableValues[i].psh_cr_acc +//11
                        '</td><td class="hidecolumn">' + tableValues[i].psh_dr_acc +//12
                        '</td><td class="hidecolumn">' + tableValues[i].psh_ref_no +//13
                        '</td><td class="hidecolumn">' + tableValues[i].psh_frm_dt +//14
                        '</td><td class="hidecolumn">' + tableValues[i].psh_to_dt +//15
                        '</td><td class="hidecolumn">' + tableValues[i].psh_trm +//16
                        '</td><td class="hidecolumn">' + tableValues[i].psh_stp +//17
                        '</td><td class="hidecolumn">' + tableValues[i].psh_stus +//18
                        '</td></tr>');
                }
                jQuery(".hidecolumn").css({ "display": "none" });
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Rent Headers");
            }
            else if (field == "PaymentTypes") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].rat_cd +
                        '</td><td>' + tableValues[i].rat_desc +
                        '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Payment Types");
            }
            else if (field == "PaymentSubTypes") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].rat_cd +
                        '</td><td>' + tableValues[i].rat_desc +
                        '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Payment SubTypes");
            }
            else if (field == "btuval") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].MI_SIZE + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("BTU Code");
            }else if (field == "HandAcc") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].HAL_ACC_NO + '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Account Code");
            }
            else if (field == "BonusCode" || field == "BonusCode3") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].Rbh_doc_no + '</td></tr>'
                        );
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Bonus Search");
            }
            else if (field == "SchemaCodeSearch") {
                console.log(tableValues);
                for (i = 0; i < tableValues.length; i++) {
                    if (tableValues[i].Rbh_anal1 != "") {
                        var a = "";
                        (i % 2 == 1) ? a = 'class="coloured"' : "";
                        jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                            '</td><td>' + tableValues[i].Rbh_anal1 + '</td></tr>');
                    }
                }
                console.log(jQuery('.table-responsive tbody').length);
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Circular Code");
            } else if (field == "SRCHPAYREQ") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].MPRH_REQ_NO +
                        '</td><td>' +my_date_format(convertDate(tableValues[i].MPRH_REQ_DT)) +
                        '</td><td>' + tableValues[i].MPRH_PAY_TP +
                        '</td><td>' + tableValues[i].MPRH_CREDITOR +
                        '</td><td>' +addCommas(parseFloat(tableValues[i].MPRH_NET_AMT).toFixed(2) )+ 
                        '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Payment Requests");
            } else if (field == "SRCHDEBTACC" || field == "SRCHCREACC" || field == "SRCHCREACC1") {
            
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].RCA_ACC_NO +
                        '</td><td>' + tableValues[i].RCA_ACC_DESC +
                        '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Account Code");
            } else if (field == "SRCHFILDSRCH") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].RTS_CD +
                        '</td><td>' + tableValues[i].RTS_DESC +
                        '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Search Field");
            } else if (field == "COMSRCHFLD") {

                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].CODE +
                        '</td><td>' + tableValues[i].DESCRIPTION +
                        '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Search " + parameterData.searchFld);
            } else if (field == "SRCHPYTP") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].rat_cd +
                        '</td><td>' + tableValues[i].rat_desc +
                        '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Search Payment type");
            } else if (field == "SRCHTAX") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].TAX_CD +
                        '</td><td>' + tableValues[i].TAX_DESC +
                        '</td><td>' + tableValues[i].TAX_RATE +
                        '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Search Tax");
            } else if (field == "SRCHREFNO") {
                for (i = 0; i < tableValues.length; i++) {
                    var a = "";
                    var chk = "";
                    if (tableValues[i].SELECT == 1) {
                        chk = "checked";
                    }
                    (i % 2 == 1) ? a = 'class="coloured"' : "";
                    jQuery('.table-responsive tbody').append('<tr ' + a + '><td>' + tableValues[i].R__ +
                        '</td><td>' + tableValues[i].PURNO +
                        '</td><td>' +addCommas (tableValues[i].COST) +
                        '<td>' + '<input type="checkbox" name="col" ' + chk + ' id="' + tableValues[i].PURNO + '" cost="' + tableValues[i].COST + '" value="' + tableValues[i].PURNO + '" onclick="addColumnsFront(this,\'POSRC\');">' + '</td></tr>' +

                        '</td></tr>');
                }
                jQuery(".serch-panel-title").empty();
                jQuery(".serch-panel-title").html("Search Tax");

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
            if (field == "ProfitCenter") {
                jQuery('#ProfitCenter').val("");
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#ProfitCenter').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#ProfitCenter').focus();
            }
            else if (field == "ProfitCentersRent") {
                jQuery('#pft').val("");
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                var value1 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(3)').text();
                jQuery('#pft').val(value); 
                jQuery('#pftaddresse').val(value1);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#pft').focus();
            } else if (field == "ELProfitCenter") {
                jQuery('#elitepc').val("");
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                var value1 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(3)').text();
                jQuery('#elitepc').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#elitepc').focus();
            
        } else if (field == "ELProfitCenteradd") {
            jQuery('#elitepc').val("");
            var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
            var value1 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(3)').text();
            jQuery('#eliteaddpc').val(value);
            jQuery('#myModal').modal('hide');
            jQuery("#KeyWord").val("");
            jQuery('#eliteaddpc').focus();
        }
            else if (field == "ProfitCenter1") {
                jQuery('#location').val("");
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#location').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#location').focus();
            }
            else if (field == "locationmg") {
                jQuery('#location').val("");
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#location').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#location').focus();
            }
            else if (field == "employee")
            {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(2)').text();
                var value2 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#overemp').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#overemp').focus();
                jQuery('label.efp-cd').empty();
                jQuery('label.efp-cd').append(value2);
            } else if (field == "employeeNew") {
                var value2 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#overemp').val(value2);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#overemp').focus();
            }
            else if (field == "employee1") {
                jQuery('#Manager').val("");
                var value2 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                var desc = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(3)').text();
                jQuery('#Manager').val(value2);
                jQuery('#Mgrname').val(desc);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#Manager').focus();
            }
            else if (field == "RentHeaders") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                var value0 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(2)').text();
                var value1 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(3)').text();
                var value2 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(4)').text();
                var value3 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(5)').text();
                var value4 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(6)').text();
                var value5 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(7)').text();
                var value6 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(8)').text();
                var value7 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(9)').text();
                var value8 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(10)').text();
                var value9 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(11)').text();
                var value10 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(12)').text();
                var value11 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(13)').text();
                var value12 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(14)').text();
                var value13 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(15)').text();
                var value14 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(16)').text();
                var value15 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(17)').text();
                var value16 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(18)').text();
                jQuery('#rentdts').val(value);
                jQuery('#paymenttype').val(value1);
                jQuery('#paymensubttype').val(value2);
                jQuery('#addresse1').val(value3);
                jQuery('#addresse2').val(value4);
                jQuery('#sqfeet').val(value5);
                jQuery('#district').val(value6);
                jQuery('#remark').val(value7);
                jQuery('#provision').val(value8);
                jQuery('#creditacc').val(value9);
                jQuery('#debitacc').val(value10);
                jQuery('#agreementref').val(value11);
                jQuery('#rentfromdate').val(my_date_format(new Date(value12)));
                jQuery('#renttodate').val(my_date_format(new Date(value13)));
                jQuery('#pft').val(value0); 
                $('#rentdts').attr('readonly', true);
                $('#paymenttype').attr('readonly', true);
                $('#paymensubttype').attr('readonly', true);
                $('#pft').attr('readonly', true);
                if (value14 == '0') {
                    jQuery('#terminatewithdate').prop('checked', true);
                }
                else {
                    jQuery('#stoppaymentwithdate').prop('checked', true);
                }
                if (value16 == "A") {
                    $('.btn-approve-data').css('display', 'none');
                }

                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#rentdts').focus();

            }
            else if (field == "PaymentTypes") {
                var value2 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                var desc = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(2)').text();
                jQuery('#paymenttype').val(value2);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#paymenttype').focus();
            }
            else if (field == "PaymentSubTypes") {
                var value2 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                var desc = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(2)').text();
                jQuery('#paymensubttype').val(value2);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#paymensubttype').focus();
            }
            else if (field == "manager") {
                jQuery('#manager').val("");
                var value2 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                var desc = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(3)').text();
                jQuery('#manager').val(value2);
                jQuery('#Mgrname').val(desc);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#manager').focus();
            }
            else if (field == "SchemeType") {
                jQuery('#defcode').val("");
                var value2 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#defcode').val(value2);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#defcode').focus();
            }
            else if (field == "Scheme") {
                jQuery('#defcode').val("");
                var value2 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#defcode').val(value2);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#defcode').focus();
            }
            else if (field == "shwmanager") {
                jQuery('#manager').val("");
                var value2 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                var desc = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(2)').text();
                jQuery('#manager').val(value2);
                jQuery('#name').val(desc);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#manager').focus();
            }
            else if (field == "employee2") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(2)').text();
                var value2 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#emp_cd_ov').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#overemp').focus();
                jQuery('label.efp-cd2').empty();
                jQuery('label.efp-cd2').append(value2);
            } else if (field == "employee3") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(2)').text();
                var value2 = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#emp_cd_ov2').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#emp_cd_ov2').focus();
                jQuery('label.efp-cd3').empty();
                jQuery('label.efp-cd3').append(value2);
            } else if (field == "employee4") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#exc_cd').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#exc_cd').focus();

            }
            else if (field == "employee5") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#manager').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#manager').focus();

            } else if (field == "employeecat")
            {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#overemp').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#overemp').focus();
            } else if (field == "srchItemsM") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#itemcode').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#itemcode').focus();
            } else if (field == "srchItemModelM") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#model').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#model').focus();
            }
            else if (field == "srchItemBrandM") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#brand').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#brand').focus();
            } else if (field == "srchMainCatM") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#cat1').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#cat1').focus();
            }
            else if (field == "srchMainCatM2") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#btu_cat').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#btu_cat').focus();
            }
            else if (field == "srchCategory2M") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#cat2').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#cat2').focus();
            } else if (field == "CommissionCode") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#commcode').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#commcode').focus();
            }
            else if (field == "BonusCode") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#bonuscode').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#bonuscode').focus();
            }
            else if (field == "ChartAccountCode") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#accountscode').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#accountscode').focus();
            }
            else if (field == "BonusCode2") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#Code').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#Code').focus();
            } else if (field == "srchChnl") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#chanel').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#chanel').focus();
            } else if (field == "srchChnlAll") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#chanel').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#chanel').focus();
            } else if (field == "InvType") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#invtype').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#invtype').focus();
            } else if (field == "PriceCircula") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#pricecircular').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#pricecircular').focus();
            }
            else if (field == "InvType2") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#inv_type2').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#inv_type2').focus();
            } else if (field == "srchSubChnlM") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#schannel').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#schannel').focus();
            } else if (field == "srchRegionM") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#region').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#region').focus();
            } else if (field == "srchZoneM") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#zone').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#zone').focus();
            }
            else if (field == "srchPCM") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#pc').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#pc').focus();
            }
            else if (field == "srchPCM2") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                var desc = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(2)').text();
                jQuery('#location').val(value);
                jQuery('#locationdesc').val(desc);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#location').focus();
            }
            else if (field == "srchPCM3") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#location').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#location').focus();           
            }
            else if (field == "srchPCM4") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#location').val(value);
                jQuery('#mainlocation').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#location').focus();
            }
            else if (field == "srchPCM4M") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#mainlocation').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#mainlocation').focus();
            }
            else if (field == "circular") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#circode').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#circode').focus();
            }
            else if (field == "circularcbd") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#circode').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#circode').focus();
            }
            else if (field == "CusCode") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#custormer').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#custormer').focus();
            } else if (field == "btuval") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#btu_code').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#btu_code').focus();
            } else if (field == "HandAcc") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#accno').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#accno').focus();
            }
            else if (field == "BonusCode3") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#circode').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#circode').focus();
            }
            else if (field == "SchemaCodeSearch") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#schnumber').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#Code').focus();
            } else if (field == "SRCHPAYREQ") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#ReqNo').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#ReqNo').focus();
            } else if (field == "SRCHDEBTACC") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery('#AccNo').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#AccNo').focus();
            } else if (field == "SRCHCREACC") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                var desc = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(2)').text();
                jQuery('#Creditor').val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery('#Creditor').focus();
                jQuery(".cred-name").empty();
                jQuery(".cred-name").html(desc)
            } else if (field == "SRCHFILDSRCH") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery(parameterData).val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery(parameterData).focus();
            } else if (field == "COMSRCHFLD") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery(parameterData.txtFld).val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery(parameterData.txtFld).focus();
            } else if (field == "SRCHPYTP") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery("#PayType").val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery("#PayType").focus();
            } else if (field == "SRCHCREACC1") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery("#account-code").val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery("#account-code").focus();
            } else if (field == "SRCHTAX") {
                var value = jQuery('tr.selected td', '#myModal table tbody').closest("tr").find('td:eq(1)').text();
                jQuery("#TaxCode").val(value);
                jQuery('#myModal').modal('hide');
                jQuery("#KeyWord").val("");
                jQuery("#TaxCode").focus();
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
var my_date_formatmonth = function (input) {
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

    return ( monthNames[monthIndex] + "/" + year);
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