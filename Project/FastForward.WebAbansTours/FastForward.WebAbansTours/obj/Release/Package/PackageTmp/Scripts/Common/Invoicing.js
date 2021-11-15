jQuery(document).ready(function () {

    loadExecutive();
    loadPackageType();
    function getFormatedDate(date) {
        var dte = new Date(parseInt(date.substr(6)));
        return my_date_format(dte);
    }
    jQuery(".cus-cd-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Name", "Nic", "Mobile", "Br No"];
        field = "cusCodeInv"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".inv-no-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Invoice", "Reference", "Balance"];
        field = "invoceSrch"
        var x = new CommonSearch(headerKeys, field);
    });
    function setFieldValue(data, group, local) {
        if (data != "" && local) {
            jQuery("#cus_mobile").val(data.Mbe_mob);
            jQuery("#Sah_cus_add1").val(data.Mbe_add1);
            type = "local";
            jQuery(".txt-type").val("local");

        }
        if (data != "" && group) {
            jQuery("#cus_mobile").val(data.Mbe_mob);
            jQuery("#Sah_cus_add1").val(data.Mbe_add1);
            type = "group";
            jQuery(".txt-type").val("group");
        }
    }
    jQuery(".Add-cost-mehod-cls").click(function (evt) {

    })
    // jQuery("#enq_id").focus();
    //getInvoicedata()
    jQuery("#Sah_inv_tp").change(function () {
        LoadPriceBook();
        LoadPriceLevel();
    });
    getInvoicedata();
    function getInvoicedata() {
        var cuscode = jQuery("#Sah_cus_cd").val();
        var enqid = jQuery("#enq_id").val();
        var invno = jQuery("#Sah_inv_no").val();
        if (cuscode == null | cuscode == "") {
            loadInvoiceType();
            LoadPriceBook();
            LoadPriceLevel();

        } else {
            loadInvoiceType();
            LoadPriceBook();
            LoadPriceLevel();

            SetInvData(invno, enqid);
        }
        jQuery.ajax({
            type: "GET",
            url: "/Invoicing/getEnqDetailsByCusCD",
            contentType: "application/json;charset=utf-8",
            data: { val: cuscode },
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    jQuery('table.enq-by-cucd .new-row').remove();
                    if (result.success == true) {
                        if (result.data != "") {
                            setFieldValue3(result.data);

                            // jQuery(".btn-add-data").val("Update");

                        } else {

                        }
                    }
                } else {
                    Logout();
                }
            }
        });
        jQuery.ajax({
            type: "GET",
            url: "/Invoicing/getCostSheetData",
            data: { enqid: enqid },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.data != null) {
                            setinvFieldValue(result.data);
                        } else {


                        }

                    } else {
                        if (response.type == "Error") {
                            setError(response.msg);
                        } else if (response.type == "Info") {
                            setInfoMsg(response.msg);
                        }
                    }
                } else {
                    Logout();
                }
            }
        });
    }

    function SetInvData(invno, enqid) {
        jQuery.ajax({
            type: "GET",
            url: "/Invoicing/GetInvItmDeatails",
            data: { invno: invno, enqno: enqid },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.data.length > 0) {
                            jQuery("#Sah_inv_tp").val(result.type);
                            jQuery("#PriceBook").val(result.data[0].Sad_pbook);
                            jQuery("#PriceLevel").val(result.data[0].Sad_pb_lvl);
                            jQuery("#PackageType").val(result.pack);
                            jQuery("#Sah_sales_ex_cd").val(result.exe);
                            // setinvFieldValueInv(result.data);
                            invoiceFocusOut(invno);
                        } else {


                        }
                    } else {
                        if (response.type == "Error") {
                            setError(response.msg);
                        } else if (response.type == "Info") {
                            setInfoMsg(response.msg);
                        }
                    }
                } else {
                    Logout();
                }
            }
        });
    }

    var cuscd;
    var enqId;
    function setFieldValue3(data) {
        if (data != "") {
            if (data != null) {

                for (i = 0; i < data.length; i++) {
                    jQuery('table.enq-by-cucd ').append('<tr class="new-row"><td>'
                        + '<input type="checkbox" id="enq-check" checked class="selectEnq" data-cus-cd="' + data[i].GCE_CUS_CD + '" data-enq-id="' + data[i].GCE_ENQ_ID + '" />' + '</td><td>'
                        + data[i].GCE_CUS_CD + '</td><td>'
                        + data[i].GCE_ENQ_ID + '</td><td>'
                        + data[i].GCE_NAME + '</td><td>'
                       + getFormatedDate(data[i].GCE_DT) + '</td><td>'
                       + data[i].GCE_ENQ + '</td></tr>');

                }
                $('.selectEnq').on('change', function () {
                    if (this.checked) {
                        cuscd = $(this).data("cus-cd");
                        enqId = $(this).data("enq-id");
                        $.ajax({
                            url: "/Invoicing/getCostSheetData",
                            type: 'GET',
                            cache: false,
                            data: { enqid: enqId },
                            success: function (result) {
                                if (result != null) {

                                    for (i = 0; i < result.data.length; i++) {
                                        var discount = (result.data[i].QCD_QTY * result.data[i].QCD_UNIT_COST * result.data[i].QCD_EX_RATE) - result.data[i].QCD_TOT_COST;
                                        var dis = discount * 100 / (result.data[i].QCD_QTY * result.data[i].QCD_UNIT_COST * result.data[i].QCD_EX_RATE)

                                        updateChagresGrid(result.data[i].QCD_SUB_CATE, result.data[i].QCD_CAT, result.data[i].QCD_CURR, result.data[i].QCD_QTY, result.data[i].QCD_RMK, dis, discount, result.data[i].QCD_UNIT_COST, result.data[i].QCD_EX_RATE, result.data[i].QCD_AF_MARKUP, 0);
                                    }
                                }
                            }
                        });
                    } else {
                        cuscd = "";
                        enqId = "";
                        $.ajax({
                            url: "/Invoicing/getCostSheetData",
                            type: 'GET',
                            cache: false,
                            data: { cuscode: $(this).data("cus-cd"), enqid: $(this).data("enq-id") },
                            success: function (result) {
                                if (result != null) {
                                    for (i = 0; i < result.data.length; i++) {
                                        jQuery.ajax({
                                            type: "GET",
                                            url: "/Invoicing/removeChargeItem",
                                            data: { chgCd: result.data[i].QCD_SUB_CATE, pax: result.data[i].QCD_QTY, totcst: result.data[i].QCD_TOT_COST },
                                            contentType: "application/json;charset=utf-8",
                                            dataType: "json",
                                            success: function (result) {
                                                if (result.login == true) {
                                                    if (result.success == true) {
                                                        updateGridAndPriceValues(result.oMainItemsList);
                                                    }
                                                } else {
                                                    Logout();
                                                }
                                            }
                                        });
                                    }
                                }
                            }
                        });
                    }
                });

            }
        }
    }

    function setFieldValue4(data) {
        if (data != "") {
            if (data != null) {

                for (i = 0; i < data.length; i++) {
                    jQuery('table.enq-by-cucd ').append('<tr class="new-row"><td>'
                        + '<input type="checkbox" id="enq-check" class="selectEnq" data-cus-cd="' + data[i].GCE_CUS_CD + '" data-enq-id="' + data[i].GCE_ENQ_ID + '" />' + '</td><td>'
                        + data[i].GCE_CUS_CD + '</td><td>'
                        + data[i].GCE_ENQ_ID + '</td><td>'
                        + data[i].GCE_NAME + '</td><td>'
                       + getFormatedDate(data[i].GCE_DT) + '</td><td>'
                       + data[i].GCE_ENQ + '</td></tr>');

                }
                $('.selectEnq').on('change', function () {
                    if (this.checked) {
                        cuscd = $(this).data("cus-cd");
                        enqId = $(this).data("enq-id");
                        jQuery("#enq_id").val("");
                        $.ajax({
                            url: "/Invoicing/getEnquiryCharges",
                            type: 'GET',
                            cache: false,
                            data: { enqid: enqId },
                            success: function (result) {
                                if (result != null) {
                                    for (i = 0; i < result.data.length; i++) {
                                        var discount = (result.data[i].SCH_QTY * result.data[i].SCH_UNIT_RT) - result.data[i].SCH_TOT_AMT;
                                        var dis = discount * 100 / (result.data[i].SCH_QTY * result.data[i].SCH_UNIT_RT)

                                        updateChagresGrid(result.data[i].SCH_ITM_CD, result.data[i].SCH_ITM_STUS, result.data[i].SCH_CURR, result.data[i].SCH_QTY, result.data[i].SCH_ENQ_NO, dis, discount, result.data[i].SCH_UNIT_RT, 0, 1, 0);
                                    }
                                }
                            }
                        });
                    } else {
                        cuscd = "";
                        enqId = "";
                        $.ajax({
                            url: "/Invoicing/getEnquiryCharges",
                            type: 'GET',
                            cache: false,
                            data: { cuscode: $(this).data("cus-cd"), enqid: $(this).data("enq-id") },
                            success: function (result) {
                                if (result != null) {
                                    for (i = 0; i < result.data.length; i++) {
                                        jQuery.ajax({
                                            type: "GET",
                                            url: "/Invoicing/removeChargeItem",
                                            data: { chgCd: result.data[i].SCH_ITM_CD, pax: result.data[i].SCH_QTY, totcst: result.data[i].SCH_TOT_AMT },
                                            contentType: "application/json;charset=utf-8",
                                            dataType: "json",
                                            success: function (result) {
                                                if (result.login == true) {
                                                    if (result.success == true) {
                                                        updateGridAndPriceValues(result.oMainItemsList);
                                                    }
                                                } else {
                                                    Logout();
                                                }
                                            }
                                        });
                                    }
                                }
                            }
                        });
                    }
                });

            }
        }
    }
    jQuery("#Sah_inv_no").focusout(function () {
        invoiceFocusOut(jQuery(this).val());
        jQuery(".btn-inv-Add-data").hide();
    });
    function invoiceFocusOut(InvNo) {
        jQuery(".btn-drall-cancel-data").hide();
        jQuery(".btn-drall-reverse-data").hide();
        if (InvNo != "") {
            jQuery('.cost-sheet-table .new-row').remove();
            jQuery.ajax({
                type: "GET",
                url: "/Invoicing/getInvoiceDetails",
                data: { invNo: InvNo },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.data != null) {
                                setinvFieldValueInv(result.data);
                                setAllInvData();
                            } else {
                                setInfoMsg("Cannot find receipt details.");
                                // clearReciptForm();
                            }
                        } else {
                            if (response.type == "Error") {
                                setError(response.msg);
                            } else if (response.type == "Info") {
                                setInfoMsg(response.msg);
                            }
                        }
                    } else {
                        Logout();
                    }
                }
            });
        }
    }
    function setAllInvData() {
        jQuery.ajax({
            type: "GET",
            url: "/Invoicing/GetInvHDRDeatails",
            data: { invNo: jQuery("#Sah_inv_no").val() },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.data != null) {

                            jQuery("#enq_id").val(result.data[0].Sah_ref_doc);
                            jQuery("#Sah_dt").val(getFormatedDate(result.data[0].Sah_dt));
                            jQuery("#Sah_inv_tp").val(result.data[0].Sah_inv_tp);
                            jQuery("#Sah_sales_ex_cd").val(result.data[0].Sah_sales_ex_cd);
                            jQuery("#PackageType").val(result.data[0].Sah_anal_3);
                            jQuery("#Sah_remarks").val(result.data[0].Sah_remarks);
                            jQuery("#enq_id").focus();

                            if (result.data[0].Sah_stus != "C" && result.data[0].Sah_stus != "R") {
                                jQuery(".btn-drall-cancel-data").show();
                                jQuery(".btn-drall-reverse-data").show();
                            } else {
                                jQuery(".btn-drall-cancel-data").hide();
                                jQuery(".btn-drall-reverse-data").hide();
                            }
                            jQuery(".btn-drall-print-data").show();
                        } else {
                            setInfoMsg("Cannot find receipt details.");

                        }
                    } else {
                        if (response.type == "Error") {
                            setError(response.msg);
                        } else if (response.type == "Info") {
                            setInfoMsg(response.msg);
                        }
                    }
                } else {
                    Logout();
                }
            }
        });
    }
    function setinvFieldValue(data) {
        jQuery('.cost-sheet-table .new-row').remove();
        if (data != "") {
            if (data != null) {


                for (i = 0; i < data.length; i++) {
                    var discount = (data[i].QCD_QTY * data[i].QCD_UNIT_COST * data[i].QCD_EX_RATE) - data[i].QCD_TOT_LOCAL;
                    var dis = discount * 100 / (data[i].QCD_QTY * data[i].QCD_UNIT_COST * data[i].QCD_EX_RATE)

                    updateChagresGrid(data[i].QCD_SUB_CATE, data[i].QCD_CAT, data[i].QCD_CURR, data[i].QCD_QTY, data[i].QCD_RMK, dis, discount, data[i].QCD_UNIT_COST, data[i].QCD_EX_RATE, data[i].QCD_AF_MARKUP, 0);
                }

            }
        }
    }
    function setinvFieldValueInv(data) {
        jQuery('.cost-sheet-table .new-row').remove();
        if (data != "") {
            if (data != null) {
                for (i = 0; i < data.length; i++) {
                    var discount = ((data[i].Sad_qty * data[i].Sad_unit_rt / data[i].SII_EX_RT) - data[i].Sad_tot_amt / data[i].SII_EX_RT).toFixed(4);
                    var dis = (discount * 100 / (data[i].Sad_qty * data[i].Sad_unit_rt)).toFixed(2);

                    updateChagresGrid(data[i].Sad_itm_cd, data[i].Sad_itm_stus, data[i].SII_CURR, data[i].Sad_qty, data[i].Sad_warr_remarks, dis, discount, data[i].Sad_unit_rt, data[i].SII_EX_RT, 0, 0);
                }


            }
        }
    }

    jQuery("form.frm-inv-det #Sah_cus_cd").focus(function () {

        if (jQuery("#Sah_inv_no").val() != "") {
            document.getElementById("inv-frm").reset();
            jQuery(".btn-drall-print-data").hide();
            jQuery(".btn-drall-cancel-data").hide();
            jQuery(".btn-drall-reverse-data").hide();
            isload = 0;
            window.location.href = "/Invoicing";
        }
        codeFocusOut();
        jQuery.ajax({
            type: "GET",
            url: "/Invoicing/getEnqDetailsByCusCD",
            contentType: "application/json;charset=utf-8",
            data: { val: jQuery(this).val() },
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    jQuery('table.enq-by-cucd .new-row').remove();
                    if (result.success == true) {
                        if (result.data != "") {
                            setFieldValue4(result.data);

                            // jQuery(".btn-add-data").val("Update");

                        } else {

                        }
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    function codeFocusOut() {
        if (jQuery("form.frm-inv-det #Sah_cus_cd").val() != "") {
            jQuery.ajax({
                type: "GET",
                url: "/DataEntry/cusCodeTextChanged",
                data: { val: jQuery("form.frm-inv-det #Sah_cus_cd").val() },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.success == true) {
                                if (typeof (result.local) != "undefined") {
                                    setFieldValue(result.data, false, true);
                                }
                                if (typeof (result.group) != "undefined") {
                                    setFieldValue(result.data, true, false);
                                }
                                if (typeof (result.group) == "undefined" && typeof (result.local) == "undefined") {
                                    jQuery(".btn-save-data").val("Create");
                                    jQuery(".txt-operation").val("Create");
                                    jQuery(".txt-type").val("");

                                    fieldEnable();
                                }
                            }
                        }
                    } else {
                        Logout();
                    }

                }

            });
        }
    }
    jQuery(".enq-id-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Enquiry Id", "Ref Num", "Type", "Customer Code", "Name", "Address"];
        field = "InvEnq"
        var x = new CommonSearch(headerKeys, field);
    });
    //jQuery(".enq-id-search-new").click(function () {
    //    var headerKeys = Array()
    //    headerKeys = ["Row", "Enquiry Id", "Ref Num", "Type", "Customer Code", "Name", "Address"];
    //    field = "InvEnqNew"
    //    var x = new CommonSearch(headerKeys, field);
    //});

    jQuery("#enq_id").on("keypress", function (evt) {
        if (jQuery(this).val() != "") {
            if (evt.keyCode == 13) {

                loadEnqData(jQuery(this).val());
            }
        }
    });
    jQuery("#enq_id").on("focus", function () {
        if (jQuery(this).val() != "") {
            loadEnqData(jQuery(this).val());


        }
    });
    var isload = 0;
    jQuery("#enq_id").on("focusout", function () {
        if (jQuery(this).val() != "") {
            if (isload == 0) {
                costsheetdataload();
            }
        }
    });
    function loadEnqData(enqId) {
        jQuery.ajax({
            type: "GET",
            url: "/Invoicing/getEnquiryData",
            contentType: "application/json;charset=utf-8",
            data: { enqId: enqId },
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {

                        setEnqData(result.data);
                    } else {
                        if (result.type == "Error") {
                            setError(result.msg);
                        }
                        if (result.type == "Info") {
                            setInfoMsg(result.msg);
                        }
                    }
                } else {
                    Logout();
                }
            }
        });

    }
    function setEnqData(data) {
        if (typeof data != 'undefined') {
            jQuery("#Sah_cus_cd").val(data.GCE_CUS_CD);
            jQuery("#Sah_cus_add1").val(data.GCE_ADD1);
            jQuery("#cus_mobile").val(data.GCE_MOB);
        }
    }



    function loadInvoiceType() {
        jQuery.ajax({
            type: "GET",
            url: "/Invoicing/LoadInvoiceType",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("Sah_inv_tp");
                        jQuery("#Sah_inv_tp").empty();
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

    function loadExecutive() {
        jQuery.ajax({
            type: "GET",
            url: "/Invoicing/LoadExecutive",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("Sah_sales_ex_cd");
                        jQuery("#Sah_sales_ex_cd").empty();
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



    function LoadPriceBook() {
        jQuery.ajax({
            type: "GET",
            url: "/Invoicing/LoadPriceBook",
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
    jQuery("#PriceBook").focus(function () {
        jQuery.ajax({
            type: "GET",
            url: "/Invoicing/LoadPriceBookNew",
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
    })
    //  LoadPriceLevel();
    function LoadPriceLevel() {
        jQuery.ajax({
            type: "GET",
            url: "/Invoicing/LoadPriceLevel",
            data: { invType: jQuery("form.frm-inv-det #Sah_inv_tp").val(), book: jQuery("form.frm-inv-det #PriceBook").val() },
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
    jQuery("#PriceLevel").focus(function () {
        jQuery.ajax({
            type: "GET",
            url: "/Invoicing/LoadPriceLevelNew",
            data: { invType: jQuery("form.frm-inv-det #Sah_inv_tp").val(), book: jQuery("form.frm-inv-det #PriceBook").val() },
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
    })

    function loadPackageType() {
        jQuery.ajax({
            type: "GET",
            url: "/Invoicing/LoadPackageType",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("PackageType");
                        jQuery("#PackageType").empty();
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
    loadCostType();
    function loadCostType() {
        jQuery.ajax({
            type: "GET",
            url: "/Invoicing/CostType",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("ChargeCode");
                        jQuery("#ChargeCode").empty();
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
    loadCurrency();
    function loadCurrency() {
        jQuery.ajax({
            type: "GET",
            url: "/Invoicing/Currency",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("Currency");
                        jQuery("#Currency").empty();
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
                        if (options.length > 0)
                            jQuery("#Currency").val("LKR");
                    } else {
                        setError(result.msg);
                    }
                } else {
                    Logout();
                }
            }
        })
    }
    jQuery(".sub-type-search").click(function () {
        var data = jQuery("#ChargeCode").val();
        if (jQuery("#ChargeCode").val() == "TRANS") {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description", "Service By", "From Date", "To Date", "From", "To", "Rate", "Class", "Vehicle"];
            field = "invChrgCdeSrchTrans"
            var x = new CommonSearch(headerKeys, field, data);
        } else if (jQuery("#ChargeCode").val() == "AIRTVL") {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description", "Service By", "From", "To", "Rate", "From Date", "To Date"];
            field = "invChrgCdeSrchArival";
            var x = new CommonSearch(headerKeys, field, data);
        } else if (jQuery("#ChargeCode").val() == "MSCELNS") {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description", "Service By", "From Date", "To Date", "Rate"];
            field = "invChrgCdeSrchMsclens";
            var x = new CommonSearch(headerKeys, field, data);
        } else {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description", "Service By", "From Date", "To Date", "Rate"];
            field = "invChrgCdeSrchMsclens";
            var x = new CommonSearch(headerKeys, field, data);
        }

    });
    jQuery("#SubType").focusout(function () {
        if (jQuery(this).val() != "") {
            if (jQuery("#ChargeCode").val() != "") {
                getChargCodeDetls(jQuery(this).val(), jQuery("#ChargeCode").val());
            } else {
                setInfoMsg("Please select Charge Code.");
                jQuery("#SubType").val("");
            }

        }
    });
    jQuery("#Pax").on("input", function () {
        var pax = parseFloat(jQuery("#Pax").val());
        var UniteRate = parseFloat(jQuery("#UnitRate").val());
        var Tax = parseFloat(jQuery("#Tax").val());
        var Rate = parseFloat(jQuery(".currancy-rate-amt").html());
        // var Taxval = (Tax / 100) * pax * UniteRate * Rate;
        var Dis = parseFloat(jQuery("#Dis").val());
        var Discount = parseFloat(jQuery("#Discount").val());
        var Total = (pax * (UniteRate + Tax) * Rate);
        jQuery("#Total").val(Total.toFixed(2));
    })
    jQuery("#Dis").on("input", function () {
        var pax = parseFloat(jQuery("#Pax").val());
        var UniteRate = parseFloat(jQuery("#UnitRate").val());
        var Tax = parseFloat(jQuery("#Tax").val());
        var Rate = parseFloat(jQuery(".currancy-rate-amt").html());
        // var Taxval = (Tax / 100) * pax * UniteRate * Rate;
        var Dis = parseFloat(jQuery("#Dis").val());
        var Discount = Dis * pax * (UniteRate + Tax) / 100;
        jQuery("#Discount").val(Discount);
        var Total = (pax * (UniteRate + Tax)) - Discount;
        jQuery("#Total").val(Total.toFixed(2));
        var totallkr = Total * Rate;
        jQuery(".total-lkr").empty();
        jQuery(".total-lkr").html("Total :" + totallkr + " LKR");
    })
    jQuery("#Discount").on("input", function () {
        var pax = parseFloat(jQuery("#Pax").val());
        var UniteRate = parseFloat(jQuery("#UnitRate").val());
        var Rate = parseFloat(jQuery(".currancy-rate-amt").html());
        var Tax = parseFloat(jQuery("#Tax").val());
        // var Taxval = (Tax / 100) * pax * UniteRate * Rate;
        var Discount = parseFloat(jQuery("#Discount").val());

        var Dis = Discount / (pax * (UniteRate + Tax)) * 100;
        jQuery("#Dis").val(Dis);


        var Total = (pax * (UniteRate + Tax)) - Discount;
        jQuery("#Total").val(Total.toFixed(2));
        var totallkr = (Total * Rate).toFixed(2);

        jQuery(".total-lkr").empty();
        jQuery(".total-lkr").html("Total :" + totallkr + " LKR");
    });
    function updatePayment(amount) {
        var payAmount = parseFloat(amount);
        // var currentTot = (jQuery(".tot-amount-val").html() != "") ? parseFloat(jQuery(".tot-amount-val").html()) : 0;
        var finTot = parseFloat(0) + parseFloat(amount);
        updateCurrencyAmount(parseFloat(finTot), jQuery("#Sah_cus_cd").val(), "");

    }

    function getChargCodeDetls(code, service) {
        if (code != "" && service != "") {
            jQuery.ajax({
                type: "GET",
                url: "/Invoicing/loadChargCode",
                data: { code: code, service: service },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (service == "TRANS") {
                                jQuery("#Currency").val(result.data.STC_CURR);
                                jQuery("#UnitRate").val(Number(result.data.STC_RT));
                                jQuery("#Tax").val(Number(result.data.STC_TAX_RT) * Number(result.data.STC_RT) / 100);
                                CurrencyCodeChange();
                                jQuery("#Remark").focus();
                            } else if (service == "AIRTVL") {
                                jQuery("#Currency").val(result.data.SAC_CUR);
                                jQuery("#UnitRate").val(Number(result.data.SAC_RT));
                                jQuery("#Tax").val(Number(result.data.SAC_TAX_RT) * Number(result.data.SAC_RT) / 100);
                                CurrencyCodeChange();
                                jQuery("#Remark").focus();
                            } else if (service == "MSCELNS") {
                                jQuery("#Currency").val(result.data.SSM_CUR);
                                jQuery("#UnitRate").val(Number(result.data.SSM_RT));
                                jQuery("#Tax").val(Number(result.data.SSM_TAX_RT) * Number(result.data.SSM_RT) / 100);
                                CurrencyCodeChange();
                                jQuery("#Remark").focus();
                            } else {
                                jQuery("#Currency").val(result.data.SSM_CUR);
                                jQuery("#UnitRate").val(Number(result.data.SSM_RT));
                                jQuery("#Tax").val(Number(result.data.SSM_TAX_RT) * Number(result.data.SSM_RT) / 100);
                                CurrencyCodeChange();
                                jQuery("#Remark").focus();
                            }
                        } else {
                            jQuery("#CostFare").val("");
                            jQuery("#CostTotal").val("");
                            jQuery("#CostMarkup").val("");
                            jQuery("#CostTotalLkr").val("");
                            jQuery("#CostChargCode").val("");
                            if (result.type == "Error") {
                                setError(result.msg);
                            }
                            if (result.type == "Info") {
                                setInfoMsg(result.msg);
                            }
                            jQuery("#ChargCode").focus();
                        }
                    } else {
                        Logout();
                    }
                }
            });
        }
    }
    function CurrencyCodeChange() {
        jQuery.ajax({
            type: "GET",
            url: "/Invoicing/currencyCodeChange",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: { currency: jQuery("form.frm-inv-det #Currency").val() },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        jQuery(".currancy-rate-amt").empty();
                        jQuery(".currancy-rate-amt").html(result.data);
                        //if (result.spVal == false) {
                        //    setInfoMsg(result.msg);
                        //} else {

                        //} ////////////still cannot change the currency code it will set for selected chage code

                    } else {
                        jQuery(".currancy-rate-amt").empty();
                        jQuery(".currancy-rate-amt").html("0.00");
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
    jQuery(".add-inv-charg-data").click(function () {
        if (jQuery("#ChargeCode").val() != "") {
            if (jQuery("#SubType").val() != "") {
                if (jQuery("#Remark").val() != "") {
                    if (jQuery("#Pax").val() != "" || jQuery.isNumeric(jQuery("#Pax").val())) {
                        if (jQuery("#UnitRate").val() != "" || jQuery.isNumeric(jQuery("#UnitRate").val())) {
                            if (jQuery("#Dis").val() != "" || jQuery.isNumeric(jQuery("#Dis").val())) {
                                if (jQuery("#Discount").val() != "" || jQuery.isNumeric(jQuery("#Discount").val())) {
                                    if (jQuery("#UnitRate").val() != "" || jQuery.isNumeric(jQuery("#UnitRate").val())) {

                                        var chgCd = jQuery("#SubType").val();
                                        var service = jQuery("#ChargeCode").val();
                                        var currencyCode = jQuery("#Currency").val();
                                        var pax = jQuery("#Pax").val();
                                        var remarks = jQuery("#Remark").val();
                                        var dis = jQuery("#Dis").val();
                                        var discount = jQuery("#Discount").val();
                                        var unitrate = jQuery("#UnitRate").val();
                                        var Rate = parseFloat(jQuery(".currancy-rate-amt").html());
                                        var Tax = parseFloat(jQuery("#Tax").val());

                                        if (chgCd != "", service != "", currencyCode != "", pax != "", remarks != "", dis != "", discount != "") {

                                            updateChagresGrid(chgCd, service, currencyCode, pax, remarks, dis, discount, unitrate, Rate, 0, Tax);

                                        }
                                    } else {
                                        setInfoMsg("Add Unit Rate Or You Add Unit Rate Invalid");
                                    }
                                } else {
                                    setInfoMsg("Add Discount Or You Add Discount Invalid");
                                }

                            } else {
                                setInfoMsg("Add Dis Or You Add Dis Invalid");
                            }
                        } else {
                            setInfoMsg("Add Unit Rate Or You Add Rate Invalid");
                        }
                    } else {
                        setInfoMsg("Add number of pax Or You Add Pax Invalid");
                    }
                } else {
                    setInfoMsg("Plese select Remark");
                }
            } else {
                setInfoMsg("Plese select Sub Type.");
            }
        } else {
            setInfoMsg("Plese select Charge Code.");
        }


    });
    function updateChagresGrid(chgCd, service, currencyCode, pax, remarks, dis, discount, unitrate, Rate, Markup, Tax) {
        var row = jQuery(".cost-sheet-table .new-row")[0];
        var value;
        if (row == null) {
            value = currencyCode;
        } else {
            value = jQuery(row).find('td:eq(3)').text();
        }
        if (value == currencyCode) {
            jQuery.ajax({
                type: "GET",
                url: "/Invoicing/updateTourCharges",
                data: {
                    chgCd: chgCd,
                    service: service,
                    currencyCode: currencyCode,
                    pax: pax,
                    remarks: remarks,
                    dis: dis,
                    discount: discount,
                    unitrate: unitrate,
                    Rate: Rate,
                    Markup: Markup,
                    Tax: Tax
                },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.oMainItemsList.length > 0) {
                                data = result.oMainItemsList;
                                updateGridAndPriceValues(data);

                            }
                        } else {
                            if (result.type == "Error") {
                                setError(result.msg);
                            }
                            if (result.type == "Info") {
                                setInfoMsg(result.msg);
                            }
                        }
                    } else {
                        Logout();
                    }
                }
            });

        } else {
            setInfoMsg("Cannot use different currecy type for charges.");
        }
    }
    function updateGridAndPriceValues(data) {
        var cate;
        var totCostDisplay = 0;
        var totCostLKRDisplay = 0;
        var quotePerPax = 0;
        jQuery('.cost-sheet-table .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {
                if (data[i].QCD_CAT == "AIRTVL") {
                    cate = "Air Travel";
                } else if (data[i].QCD_CAT == "TRANS") {
                    cate = "Travels";
                } else if (data[i].QCD_CAT == "MSCELNS") {
                    cate = "Miscellaneous";
                }
                else {
                    cate = "Overseas Land Arrangement(per Pax)";
                }
                jQuery('.cost-sheet-table').append('<tr class="new-row">' +
                     '<td>' + '<input type="checkbox" checked="checked" />' + '</td>' +
                        '<td>' + data[i].Sad_itm_cd + '</td>' +
                        '<td>' + data[i].Sad_warr_remarks + '</td>' +
                         '<td>' + data[i].SII_CURR + '</td>' +
                         '<td>' + data[i].Sad_qty + '</td>' +
                         '<td>' + (parseFloat(data[i].Sad_unit_rt) / parseFloat(data[i].SII_EX_RT)).toFixed(2) + '</td>' +
                        '<td>' + (parseFloat(data[i].Sad_tot_amt) / parseFloat(data[i].SII_EX_RT)).toFixed(2) + '</td>' +

                       '<td style="text-align:center;"><img class="delete-img remove-cost-mehod-cls" src="../Resources/images/Remove.png"></td>' +
                        '</tr>');
                /*   totFareDisplay = parseFloat(totFareDisplay) + parseFloat(data[i].QCD_UNIT_COST);
                   totTaxDisplay = parseFloat(totTaxDisplay) + parseFloat(data[i].QCD_TAX);
                   totCostDisplay = parseFloat(totCostDisplay) + parseFloat(data[i].QCD_TOT_COST);
                   totCostLKRDisplay = parseFloat(totCostLKRDisplay) + parseFloat(data[i].QCD_TOT_LOCAL);
                   quotePerPax = quotePerPax + data[i].QCD_TOT_LOCAL / data[i].QCD_QTY;

                   if (parseFloat(data[i].QCD_MARKUP) > 0) {
                       jQuery("#QCH_MARKUP").attr("readonly", true);
                   }*/
                totCostDisplay = parseFloat(totCostDisplay) + parseFloat(data[i].Sad_tot_amt) / parseFloat(data[i].SII_EX_RT);
                //console.log(data[i].Sad_tot_amt);
                //jQuery("#Sird_settle_amt").val(totCostDisplay);
                //jQuery(".tot-paid-amount-val").empty();
                //jQuery(".tot-paid-amount-val").html(totCostDisplay);


            }
            removeChgClickFunction();
            /*   jQuery("#totalFare").val(totFareDisplay);
               jQuery("#totalTax").val(totTaxDisplay);
               jQuery("#totalCost").val(totCostDisplay);
               jQuery("#totalCostLKR").val(totCostLKRDisplay);
               jQuery("#QCH_TOT_VALUE").val(totCostLKRDisplay);
               jQuery("#QuotePerPax").val(quotePerPax);
               jQuery("#QCH_TOT_PAX").attr("readonly", true);
               */
            jQuery("#totalCost").val(totCostDisplay.toFixed(2));
            updatePayment(totCostDisplay);
            clearChargeDataField();
            jQuery("#Sird_settle_amt").val(totCostDisplay);
        } else {
            jQuery("#totalFare").val("");
            jQuery("#totalTax").val("");
            jQuery("#totalCost").val("");
            jQuery("#totalCostLKR").val("");
            jQuery("#QCH_TOT_VALUE").val("");
            jQuery("#QuotePerPax").val("");
            jQuery("#QCH_MARKUP").val("");
            jQuery("#QCH_TOT_PAX").removeAttr("readonly");
            jQuery("#QCH_MARKUP").removeAttr("readonly");
            jQuery(".cost-sheet-table .new-row").remove();
            jQuery("#Sird_settle_amt").val(0);
        }


    }
    function removeChgClickFunction() {
        jQuery(".delete-img.remove-cost-mehod-cls").unbind('click').click(function (evt) {
            evt.preventDefault();
            var td = jQuery(this).parent('td');
            var tr = jQuery(td).parent('tr');
            var cgCd = jQuery(tr).find('td:eq(1)').html();
            var qty = jQuery(tr).find('td:eq(4)').html();
            var totcst = jQuery(tr).find('td:eq(6)').html();
            Lobibox.confirm({
                msg: "Do you want to remove ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/Invoicing/removeChargeItem",
                            data: { chgCd: cgCd, pax: qty, totcst: totcst },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        updateGridAndPriceValues(result.oMainItemsList);
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
    function clearChargeDataField() {
        jQuery("#ChargeCode").val("");
        jQuery("#Pax").val("");
        jQuery("#SubType").val("");
        jQuery("#Currency").val("");
        jQuery("#Remark").val("");
        jQuery("#UnitRate").val("");
        jQuery("#Dis").val(0);
        jQuery("#Discount").val(0);
        jQuery("#Total").val(0);
        jQuery("#CostChargCode").focus();
    }


    jQuery("#Sird_pay_tp").focus(function (evt) {
        evt.preventDefault();
        var inv_type = jQuery("#Sah_inv_tp").val();
        if (inv_type == "") {
            setInfoMsg("please select invoice type");
        } else {
            loadPayModes(inv_type);
        }


    });

    jQuery(".btn-clear-inv-data").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to clear data ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    document.getElementById("inv-frm").reset();
                    jQuery(".btn-drall-print-data").hide();
                    jQuery(".btn-drall-cancel-data").hide();
                    jQuery(".btn-drall-reverse-data").hide();
                    // clearForm();
                    isload = 0;
                    window.location.href = "/Invoicing";
                }
            }
        });

    });
    jQuery(".btn-drall-reverse-data").click(function (evt) {
        evt.preventDefault();
        if (jQuery("#Sah_inv_no").val() != "") {
            Lobibox.confirm({
                msg: "Do you want to reverse this invoice ?",
                callback: function ($this, type, ev) {
                    var invNo = jQuery("#Sah_inv_no").val();
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/Invoicing/reverseInvoice?invNo=" + invNo,
                            data: { cuscode: cuscd, enqid: enqId },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        setSuccesssMsg(result.msg);
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
                }
            });
        } else {
            setInfoMsg("Invalid invoice number.");
        }
    });
    jQuery(".btn-drall-cancel-data").click(function (evt) {
        evt.preventDefault();
        if (jQuery("#Sah_inv_no").val() != "") {
            Lobibox.confirm({
                msg: "Do you want to cancel this invoice ?",
                callback: function ($this, type, ev) {
                    var invNo = jQuery("#Sah_inv_no").val();
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/Invoicing/cancelInvoice?invNo=" + invNo,
                            data: { cuscode: cuscd, enqid: enqId },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.success == true) {
                                    setSuccesssMsg(result.msg);

                                } else {
                                    if (result.type == "Error") {
                                        setError(result.msg);
                                    } else if (result.type == "Info") {
                                        setInfoMsg(result.msg);
                                    }
                                }
                            }
                        });
                    }
                }
            });
        }
    });
    $('#cus_mobile').focusout(function () {
        var str = $(this).val();
        var numRange = /^[0-9-+]+$/;
        if (!numRange.test(str)) {
            setInfoMsg('Please enter a valid Mobile Number');
            $(this).val('');
        }
    });
    $('#Pax').focusout(function () {
        var str = $(this).val();
        var numRange = /^[0-9+]+$/;
        if (!numRange.test(str)) {
            setInfoMsg('Please enter a valid Pax !!!');
            $(this).val('');
            jQuery("#Total").val(0);
        }
    });
    $('#UnitRate').focusout(function () {
        var str = $(this).val();
        var numRange = /^[0-9+]+$/;
        if (!numRange.test(str)) {
            setInfoMsg('Please enter a valid Unit Rate !!!');
            $(this).val('');
            jQuery("#Total").val(0);
        }
    });
    $('#Dis').focus(function () {
        var str = $(this).val();

        if (jQuery("#Pax").val() == "") {
            setInfoMsg('Please enter Pax !!!');
            $(this).val('');
        }
    });
    $('#Discount').focus(function () {
        var str = $(this).val();

        if (jQuery("#Pax").val() == "") {
            setInfoMsg('Please enter Pax !!!');
            $(this).val('');
        }
    });
    $('#Dis').focusout(function () {
        var str = $(this).val();
        var numRange = /^[0-9]\d*(\.\d+)?$/;
        if (!numRange.test(str)) {
            setInfoMsg('Please enter a valid Discount !!!');
            $(this).val('');
            jQuery("#Discount").val('')
        }
    });
    $('#Discount').focusout(function () {
        var str = $(this).val();
        var numRange = /^[0-9]\d*(\.\d+)?$/;
        if (!numRange.test(str)) {
            setInfoMsg('Please enter a valid Discount !!!');
            $(this).val('');
            jQuery("#Dis").val('')
        }
    });
    jQuery('.btn-inv-Add-data').click(function (e) {
        var cuscode = jQuery("#Sah_cus_cd").val();
        var pricebook = jQuery("#PriceBook").val();
        var pricelevel = jQuery("#PriceLevel").val();
        var paytype = jQuery("#Sird_pay_tp").val();
        var bank = jQuery("#Deposit_bank_cd").val();
        var branch = jQuery("#Deposit_branch_cd").val();
        var ammount = jQuery("#Sird_settle_amt").val();
        var remarks = jQuery("#Sah_remarks").val();
        var invType = jQuery("#Sah_inv_tp").val();
        var enqidnow = jQuery("#enq_id").val();
        if (cuscode == "") {
            setInfoMsg("Custormer Code Required");
        } else {
            if (pricebook == "" | pricelevel == "") {
                setInfoMsg("Price Book Or Price Level Required");
            } else {
                if (ammount == "") {
                    setInfoMsg("Please Add Items");
                } else {
                    if (remarks == "") {
                        setInfoMsg("You want to Fill remark");
                    } else {
                        if (invType == "") {
                            setInfoMsg("Please Select Invoice Type");
                        } else {
                            if (invType != "CRED" && invType != "DEBT" && paytype == "") {
                                setInfoMsg("Please Select Payment Mode");
                            } else {
                                Lobibox.confirm({
                                    msg: "Do you want to continue process?",
                                    callback: function ($this, type, ev) {
                                        if (type == "yes") {
                                            jQuery.ajax({
                                                type: "GET",
                                                url: "/Invoicing/ChangeEnqStatus",
                                                data: { cuscode: cuscd, enqid: enqId },
                                                contentType: "application/json;charset=utf-8",
                                                dataType: "json",
                                                success: function (result) {
                                                    if (result.data != null) {
                                                        jQuery(this).attr("disabled", true);
                                                        var formdata = jQuery("#inv-frm").serialize();
                                                        //$("#inv-frm").submit();
                                                        jQuery.ajax({
                                                            type: 'POST',
                                                            url: '/Invoicing/InvoiceCreateNew',
                                                            data: formdata,
                                                            success: function (response) {
                                                                if (response.login == true) {
                                                                    if (response.success == true) {
                                                                        document.getElementById("inv-frm").reset();
                                                                        // fieldEnable();
                                                                        setSuccesssMsg(response.msg);
                                                                        jQuery(".btn-inv-Add-data").val("Create");
                                                                        jQuery(".btn-inv-Add-data").removeAttr("disabled");
                                                                        // window.location.href = "/Invoicing";
                                                                        Lobibox.confirm({
                                                                            msg: "Do you want to print invoice ?",
                                                                            callback: function ($this, type, ev) {
                                                                                if (type == "yes") {
                                                                                    //window.location.href = "/Invoicing/InvoicingReport?invNo=" + jQuery("#Sah_inv_no").val();
                                                                                    console.log(jQuery("#enq_id").val());
                                                                                    if (enqidnow != "")
                                                                                    {
                                                                                        window.open("/Invoicing/InvoicingReport?invNo=" + response.InvNo,'_blank');
                                                                                    } else {
                                                                                        window.open("/Invoicing/InvoicingReport2?invNo=" + response.InvNo, '_blank');
                                                                                    }


                                                                                }
                                                                            }
                                                                        });
                                                                        clearInvoicingForm();
                                                                    } else {
                                                                        jQuery(".btn-inv-Add-data").removeAttr("disabled");
                                                                        if (response.type == "Error") {
                                                                            setError(response.msg);
                                                                        } else if (response.type == "Info") {
                                                                            setInfoMsg(response.msg);
                                                                        }

                                                                    }
                                                                } else {
                                                                    Logout();
                                                                }

                                                            }
                                                        });
                                                    }
                                                    // $("#inv-frm").submit();
                                                }
                                            });


                                        }
                                    }

                                });
                            }
                        }


                    }


                }


            }
        }


    });
    jQuery(".btn-drall-print-data").click(function () {
        if (jQuery("#Sah_inv_no").val() != "" && jQuery("#enq_id").val() != "") {
            //window.location.href = "/Invoicing/InvoicingReport?invNo=" + jQuery("#Sah_inv_no").val();
            window.open(
                  "/Invoicing/InvoicingReport?invNo=" + jQuery("#Sah_inv_no").val(),
                  '_blank' // <- This is what makes it open in a new window.
              );
        } else if (jQuery("#Sah_inv_no").val() != "" && jQuery("#enq_id").val() == "") {
            window.open(
                              "/Invoicing/InvoicingReport2?invNo=" + jQuery("#Sah_inv_no").val(),
                              '_blank' // <- This is what makes it open in a new window.
                          );
        } else {
            setInfoMsg("Invalid invoice number.");
        }
    });
    function clearSessionData() {
        jQuery.ajax({
            type: "GET",
            url: "/Invoicing/clearValues",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {

                    }
                } else {
                    Logout();
                }
            }
        });
    }
    function clearInvoicingForm() {
        clearSessionData();
        clearFormBoxes();
        clearPaymentValues();
    }
    function clearFormBoxes() {
        clearChargeDataField();
        jQuery("#Sah_cus_cd").val("");
        jQuery("#enq_id").val("");
        jQuery("#cus_mobile").val("");
        jQuery("#Sah_inv_no").val("");
        jQuery("#Sah_cus_add1").val("");
        jQuery("#totalCost").val("");
        jQuery("#Sah_remarks").val("");
        jQuery(".cost-sheet-table .new-row").remove();
        jQuery(".payment-table .new-row").remove();
        jQuery(".enq-by-cucd .new-row").remove();
        jQuery(".tot-paid-amount-val").empty();
    }
    buttonhidden();
    function buttonhidden() {
        if (jQuery("#Sah_inv_no").val() == "" | jQuery("#Sah_inv_no").val() == null) {

        } else {
            jQuery(".btn-inv-Add-data").hide();
        }
    }
    jQuery(".add-customerto-invoice").unbind().click(function () {
        if (jQuery("#enq_id").val() != "") {
            jQuery('#addCustomerPop').modal({
                keyboard: false,
                backdrop: 'static'
            }, 'show');
            jQuery('#addCustomerPop').unbind().on('shown.bs.modal', function () {
                jQuery.ajax({
                    type: "GET",
                    url: "/Invoicing/getInvoiceCustomers",
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        if (result.login == true) {
                            if (result.success == true) {
                                clearPopVal();
                                updateCusDetTable(result.paxDetList);
                                jQuery(".balanceanount").empty();
                                jQuery(".balanceanount").html(addCommas(result.balance));
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
            });
            jQuery('#addCustomerPop .btn-cus-close').unbind().click(function (e) {
                e.preventDefault();
                if (jQuery("#Sah_inv_no").val() != "") {
                    jQuery('#addCustomerPop').modal('hide');
                } else {
                    Lobibox.confirm({
                        msg: "Do you want cancel ?",
                        callback: function ($this, type, ev) {
                            if (type == "yes") {
                                jQuery.ajax({
                                    type: "GET",
                                    url: "/Invoicing/clearCustomers",
                                    contentType: "application/json;charset=utf-8",
                                    dataType: "json",
                                    success: function (result) {
                                        if (result.login == true) {
                                            if (result.success == true) {

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

                                jQuery('#addCustomerPop').modal('hide');
                            }
                        }
                    });
                }


            });

            jQuery(".btn-cus-save").unbind().click(function () {
                jQuery.ajax({
                    type: "GET",
                    url: "/Invoicing/checkCustomerAmounts",
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        if (result.login == true) {
                            if (result.success == false) {
                                Lobibox.confirm({
                                    msg: result.msg,
                                    callback: function ($this, type, ev) {
                                        if (type == "yes") {
                                            jQuery('#addCustomerPop').modal('hide');
                                        }
                                    }
                                });
                            } else {
                                jQuery('#addCustomerPop').modal('hide');
                            }
                        } else {
                            Logout();
                        }
                    }
                });


            });
            jQuery(".cus-det-add").unbind().click(function () {
                if (jQuery("#cusCd").val() != "" && jQuery("#cusName").val() != "") {
                    jQuery.ajax({
                        type: "GET",
                        url: "/Invoicing/addInvoiceCustomers",
                        data: { cusCd: jQuery("#cusCd").val(), cusName: jQuery("#cusName").val(), remk: jQuery("#remark").val(), mob: jQuery("#Mobile").val(), pp: jQuery("#Passport").val(), nic: jQuery("#cusNIC").val(), amount: jQuery("#amount").val() },
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    clearPopVal();
                                    updateCusDetTable(result.paxDetList);
                                    jQuery(".balanceanount").empty();
                                    jQuery(".balanceanount").html(addCommas(result.balance));
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
                    return false;
                } else {
                    setInfoMsg("Invalid customer details.");
                }
                return false;
            });
            jQuery(".cus-det-search").unbind().click(function () {
                var headerKeys = Array()
                headerKeys = ["Row", "Code", "Nic", "Passport No", "Mobile", "Name", ];
                field = "popCusDet";
                var x = new CommonSearch(headerKeys, field);
            });

            jQuery("#cusCd").unbind().focusout(function () {
                if (jQuery(this).val() != "" && jQuery("#enq_id").val() != "") {
                    jQuery.ajax({
                        type: "GET",
                        url: "/Invoicing/getCustomerAmount?enqId=" + jQuery("#enq_id").val(),
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    jQuery("#amount").val(result.paxAmount);
                                } else {
                                    clearPopVal();
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
                } else {
                    if (jQuery(this).val() == "") {
                        setInfoMsg("Invalid customer details.");
                    }
                    setInfoMsg("Invalid invoice details.");
                }
            });
        } else {
            setInfoMsg("Please enter invoice details.");
        }

    });
    function clearPopVal() {
        jQuery("#cusCd").val("CASH");
        jQuery("#cusNIC").val("");
        jQuery("#Passport").val("");
        jQuery("#Mobile").val("");
        jQuery("#cusName").val("");
        jQuery("#remark").val("");
        jQuery("#amount").val("");
    }
    function updateCusDetTable(det) {
        jQuery('.inv-cus-tab .new-row').remove();
        if (det != null) {
            for (i = 0; i < det.length; i++) {
                jQuery('.inv-cus-tab').append('<tr class="new-row">' +
                        '<td>' + det[i].SPD_CUS_CD + '</td>' +
                        '<td>' + det[i].SPD_NIC + '</td>' +
                        '<td>' + det[i].SPD_PP_NO + '</td>' +
                        '<td>' + det[i].SPD_MOB + '</td>' +
                        '<td>' + det[i].SPD_CUS_NAME + '</td>' +
                        '<td>' + addCommas(det[i].SPD_AMT.toFixed(2)) + '</td>' +
                        '<td>' + det[i].SPD_RMK + '</td>' +
                         '<td><img class="delete-img remove-cus-cls" src="/Resources/images/Remove.png"></td>' +
                        '</tr>');
            }
            removeClickFunction();
        }
    }

    function removeClickFunction() {
        jQuery(".remove-cus-cls").unbind().click(function () {
            var td = jQuery(this);
            var value = jQuery(td).closest("tr").find('td:eq(0)').text();
            Lobibox.confirm({
                msg: "Do you want to remove this ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/Invoicing/removeInvoiceCustomers?cusCd=" + value,
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        updateCusDetTable(result.paxDetList);
                                        jQuery(".balanceanount").empty();
                                        jQuery(".balanceanount").html(addCommas(result.balance));
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
                }
            });
        });

    }
    jQuery(".get-images").click(function () {
        jQuery.ajax({
            type: "GET",
            url: "/ImageUpload/GetImageDetails",
            data: { enqid: jQuery("#job_number2").val() },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.data != null) {
                            setImagesValue(result.data);
                        } else {


                        }

                    } else {
                        if (response.type == "Error") {
                            setError(response.msg);
                        } else if (response.type == "Info") {
                            setInfoMsg(response.msg);
                        }
                    }
                } else {
                    Logout();
                }
            }
        });
    })
    function setImagesValue(data) {
        jQuery('.image-view-grid ').empty();
        if (data != null) {

            for (i = 0; i < data.length; i++) {
                jQuery('.image-view-grid ').append('<a href="' + data[i].Jbimg_img_path + data[i].Jbimg_img + '" target="_blank"> <img src="' + data[i].Jbimg_img_path + data[i].Jbimg_img + '" class="upload-images col-md-3" title="Search"></a>');

            }
        }
    }
    jQuery(".image-submit").click(function () {
        jQuery("#img-upload-frm").submit();
    })

    function costsheetdataload() {
        if (jQuery("#Sah_inv_no").val() == "") {
            jQuery('.cost-sheet-table .new-row').remove();
            console.log("here");
            jQuery.ajax({
                type: "GET",
                url: "/Invoicing/getCostSheetData",
                data: { enqid: jQuery("#enq_id").val() },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.data != null) {
                                setinvFieldValue(result.data);
                                isload = 1;
                            } else {


                            }

                        } else {
                            if (response.type == "Error") {
                                setError(response.msg);
                            } else if (response.type == "Info") {
                                setInfoMsg(response.msg);
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