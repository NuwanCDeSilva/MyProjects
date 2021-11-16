jQuery(document).ready(function () {
    jQuery('#Tih_inv_dt').val(my_date_format(new Date()));

    jQuery('.btn-save-inv').removeAttr("disabled");
    jQuery('.btn-temp-print').removeAttr("disabled");
    jQuery('.btn-approve').attr("disabled", "true");
    jQuery('.btn-cancel').attr("disabled", "true");
    jQuery('.print-job-btn').attr("disabled", "true");
    jQuery('#Tih_inv_curr').val("LKR");

    jQuery(".search-job-no").click(function () { // Added by Chathura on 4-oct-2017       
        var headerKeys = Array()
        headerKeys = ["Row", "Job No", "Pouch No", "Date", "Status"];
        field = "jobno3"
        var x = new CommonSearchDateFiltered(headerKeys, field);
    });
    jQuery("#Tih_job_no").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Job No", "Pouch No", "Date", "Status"];
            field = "jobno3"
            var x = new CommonSearchDateFiltered(headerKeys, field);
        }
    });
    jQuery(".btn-house-bl").click(function () {        
        if (jQuery("#Tih_job_no").val() != "" && jQuery("#Tih_job_no").val() != null) {
            click_status = "H";
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Customer", "Agent", "Date"];
            field = "BL_H_DOC_NOINV"
            data = jQuery("#Tih_job_no").val();
            var x = new CommonSearchDateFiltered(headerKeys, field, data);
        }
        else {
            setInfoMsg("Select job no for H/BL number");
        }
    });
    jQuery(".curency-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description"];
        field = "curcd2";
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".inv-no-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Credit Note No", "Date"];
        field = "InvoiceNoCrd"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".crd-inv-no-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Invoice No", "Job No", "Date"];
        field = "InvoiceNoDfApprove"
        var x = new CommonSearchDateFiltered(headerKeys, field);
    });
    jQuery(".inv-no-searchref").click(function () {
        //var headerKeys = Array()
        //headerKeys = ["Row", "Invoice No", "Date"];
        //field = "InvoiceNoRef"
        //var x = new CommonSearch(headerKeys, field);
        var headerKeys = Array()
        headerKeys = ["Row", "Invoice No", "Job No", "Date"];
        field = "InvoiceNoRef"
        var x = new CommonSearchDateFiltered(headerKeys, field);
    });

    jQuery(".btn-cust_search").click(function () {
        //var headerKeys = Array()
        //headerKeys = ["Row", "Code", "Name", "NIC", "Mobile", "BR No"];
        //field = "cusCode9"
        //var x = new CommonSearch(headerKeys, field);
        if (jQuery("#Tih_job_no").val() != null && jQuery("#Tih_job_no").val() != "") {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Name", "NIC", "Mobile", "BR No"];
            field = "cusCode9jn";
            data = jQuery("#Tih_job_no").val();
            var x = new CommonSearch(headerKeys, field, data);
        }
        else {
            setInfoMsg("Select job no for customer search");
        }
    });
    jQuery(".btn-invoiceprty-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Name", "Code", "NIC", "Mobile", "BR No"];
        field = "cusCodeCp"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".btn-exec-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Epf No", "Category", "First Name", "Last Name", "NIC"];
        field = "employee2";
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".cst-ele-srch").click(function (evt) {
        evt.preventDefault();
        var headerKeys = Array()
        headerKeys = ["Row", "Description", "Cost Code", "Account Code"];
        field = "costele";
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".uom-srch").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description"];
        field = "UOMPTY"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".curency-search2").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description"];
        field = "curcd";
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery("#Tih_job_no").focusout(function () {
        var jobnum = jQuery(this).val();
        jobNumberFocusOut(jobnum);
    });
    function jobNumberFocusOut(jobnum) {
        if (jobnum != "") {
            if (jobnum != "") {
                jQuery.ajax({
                    type: "GET",
                    url: "/Validation/validateJobNumber?jobNum=" + jobnum,
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        if (result.success == false) {
                            setInfoMsg(result.msg);
                            jQuery("#Tih_job_no").val("");
                            jQuery("#Tih_bl_h_no").val("");
                            jQuery("#Tih_job_no").focus();
                        }
                        if (result.success == true) {
                            jQuery("#Tih_bl_h_no").val("");
                            if (result.data.Jb_jb_no == null) {
                                setInfoMsg("Please enter valid job number.");
                                jQuery("#Tih_job_no").val("");
                                jQuery("#Tih_bl_h_no").val("");
                                jQuery("#Tih_job_no").focus();
                            }
                        }
                    }
                });
            }
        }
    }
    $('#Tih_bl_h_no').focusout(function () {
        var pc = $(this).val();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/Validation/validateHouseBLNumber",
            data: { HBLno: jQuery("#Tih_bl_h_no").val() },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.Count == 0) {
                            if (jQuery("#Tih_bl_h_no").val() != null && jQuery("#Tih_bl_h_no").val() != "") {
                                //setInfoMsg("Invalid BL Id!!");
                            }
                            $('#Tih_bl_h_no').val("");
                            return;
                        }
                        else {
                            jQuery.ajax({
                                cache: false,
                                async: true,
                                type: "POST",
                                url: "/Invoice/LoadBLDetails",
                                data: { BLno: jQuery("#Tih_bl_h_no").val() },
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
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    $('#Tih_inv_cred_no').focusout(function () {
        if (jQuery("#Tih_inv_cred_no").val() != "") {
            var pc = $(this).val();
            jQuery.ajax({
                cache: false,
                type: "GET",
                url: "/CreditNote/GetAllDataInv",
                data: { InvNo: jQuery("#Tih_inv_cred_no").val() },
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {

                            //Set Inv Hdr  Data
                            SetInvHdr(result.hdrdata);
                            SetInvDataList(result.itemdata);

                            if (result.Recitem.length > 0) {
                                SetPaymentDataList(result.Recitem);
                            }
                            jQuery('.tbl-cus-name .new-row').remove();
                            jQuery('.tbl-inv-party .new-row').remove();
                            jQuery('.tbl-inv-party').append('<tr class="new-row">' +
                             '<td>' + result.Ptyname + '</td>' + '</tr>'
                            );
                            jQuery('.tbl-cus-name').append('<tr class="new-row">' +
                                '<td>' + result.Cusname + '</td>' + '</tr>'
                               );

                            if (result.hdrdata.length > 0) {
                                //alert('1');
                                //jQuery('.btn-save-inv').attr("disabled", "true");
                                jQuery('.btn-save-inv').attr("enabled", "true");
                            }
                            else {
                                //alert('2');
                                jQuery('.btn-save-inv').removeAttr("disabled");
                            }
                            //if (result.hdrdata[0].Tih_inv_status == "A") {
                            //    alert('3');
                            //    jQuery('.btn-approve').attr("disabled", "true");
                            //    jQuery('.btn-save-inv').attr("disabled", "true");
                            //    jQuery('.btn-cancel').removeAttr("disabled");
                            //    jQuery('.print-job-btn').removeAttr("disabled");
                            //}
                            //else {
                            //    if (result.permission == true) {
                            //        jQuery('.btn-approve').removeAttr("disabled");
                            //    }
                            //    jQuery('.btn-cancel').removeAttr("disabled");
                            //    jQuery('.print-job-btn').attr("disabled", "true");
                            //}
                            if (result.hdrdata[0].Tih_is_cancel == 1) {
                                jQuery('btn-cancel').attr("disabled", "true");
                            }
                            jQuery("#Tih_inv_no").val("")
                        } else {
                            setInfoMsg(result.msg);
                            //clearpage();
                            jQuery("#Tih_inv_cred_no").val("");
                        }
                    } else {
                        Logout();
                    }
                }
            });
        }

    });
    $('#Tih_inv_no').focusout(function () {
        var pc = $(this).val();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/CreditNote/GetAllData",
            data: { InvNo: jQuery("#Tih_inv_no").val() },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {

                        //Set Inv Hdr  Data
                        SetInvHdr(result.hdrdata);
                        SetInvDataList(result.itemdata)
                       
                        jQuery("#Tih_inv_amt").val(ReplaceNumberWithCommas(Number(result.Total).toFixed(2)));
                        jQuery('.tbl-cus-name .new-row').remove();
                        jQuery('.tbl-inv-party .new-row').remove();
                        jQuery('.tbl-inv-party').append('<tr class="new-row">' +
                         '<td>' + result.Ptyname + '</td>' + '</tr>'
                        );
                        jQuery('.tbl-cus-name').append('<tr class="new-row">' +
                            '<td>' + result.Cusname + '</td>' + '</tr>'
                           );
                        
                        if (result.hdrdata.length > 0) {
                            jQuery('.btn-save-inv').attr("disabled", "true");                            
                        }
                        else {
                            jQuery('.btn-save-inv').removeAttr("disabled");
                        }
                        if (result.hdrdata[0].Tih_inv_status == "A") {
                            jQuery('.btn-approve').attr("disabled", "true");
                            //jQuery('btn-cancel').attr("disabled", "true");
                            jQuery('.print-job-btn').removeAttr("disabled");
                            jQuery('.btn-cancel').removeAttr("disabled");
                        }
                        else {
                            if (result.permission == true) {
                                jQuery('.btn-approve').removeAttr("disabled");
                            }
                            jQuery('.btn-cancel').removeAttr("disabled");
                            jQuery('.print-job-btn').attr("disabled", "true");
                        }
                        if (result.hdrdata[0].Tih_is_cancel == 1) {
                            jQuery('btn-cancel').attr("disabled", "true");
                        }
                        //else {
                        //    jQuery('.btn-cancel').removeAttr("disabled");
                        //}
                        

                    } else {
                        //setInfoMsg(result.msg);
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    function SetInvHdr(data) {
        jQuery("#Tih_job_no").val(data[0].Tih_job_no);
        jQuery("#Tih_bl_h_no").val(data[0].Tih_bl_h_no);
        //jQuery("#Tih_inv_dt").val(getFormatedDate1(data[0].Tih_inv_dt));
        jQuery('#Tih_inv_dt').val(my_date_format(new Date()));
        jQuery("#Tih_inv_curr").val(data[0].Tih_inv_curr);
        jQuery("#Tih_inv_no").val(data[0].Tih_inv_no);
        jQuery("#Tih_ex_cd").val(data[0].Tih_ex_cd);
        jQuery("#Tih_cus_cd").val(data[0].Tih_cus_cd);
        jQuery("#Tih_inv_party_cd").val(data[0].Tih_inv_party_cd);
        jQuery("#Tih_rmk").val(data[0].Tih_rmk);
        jQuery("#Tih_other_ref_no").val(data[0].Tih_other_ref_no);
        jQuery("#Tih_other_ref_no").val(data[0].Tih_other_ref_no);
        jQuery("#Tih_exec_name").val(data[0].Tih_exec_name);
        jQuery("#Tih_pono").val(data[0].Tih_pono);
        jQuery("#Tih_inv_amt").val(ReplaceNumberWithCommas(data[0].Tih_inv_amt));
    }
    jQuery("#Tih_inv_curr").focusout(function () {
        var code = jQuery(this).val();
        CurrencyfocusOut(code);
    });
    function CurrencyfocusOut(code) {
        if (code != "") {
            jQuery.ajax({
                type: "GET",
                url: "/Validation/validateCurrencyCode?curcd=" + code,
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.success == false) {
                        setInfoMsg(result.msg);
                        jQuery("#Tih_inv_curr").val("");
                        jQuery("#Tih_inv_curr").focus();
                    }
                    if (result.success == true) {
                        if (result.data.MCR_CD == null) {
                            setInfoMsg("Please enter valid Currency code.");
                            jQuery("#Tih_inv_curr").val("");
                            jQuery("#Tih_inv_curr").focus();
                        } else {
                            loadExchageRate2(code);
                        }
                    }
                }
            });

        }
    }
    function loadExchageRate2(code) {
        if (code != "") {
            jQuery.ajax({
                type: "GET",
                url: "/Validation/getExcahaneRate",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                data: { currency: code },
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            // jQuery("#ExchgRate").val(result.data);
                            // updateAmount();
                        } else {
                            if (result.type == "Error") {
                                setError(result.msg);
                            } else if (result.type == "Info") {
                                setInfoMsg(result.msg);
                                jQuery("#Tih_inv_curr").val("");
                            }


                        }
                    } else {
                        Logout();
                    }
                }
            });
        }
    }
    LoadServices();
    function LoadServices() {
        jQuery.ajax({
            type: "GET",
            url: "/CreditNote/LoadJobServices",
            data: {},
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("Tid_ser_cd");
                        jQuery("#Tid_ser_cd").empty();
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
    jQuery("#CstEle").focusout(function () {
        var code = jQuery(this).val();
        cstelemtFocusOut(code);
    });
    function cstelemtFocusOut(code) {
        if (code != "") {
            jQuery.ajax({
                type: "GET",
                url: "/Validation/validateCostElement?eleCode=" + code,
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == false) {
                        Logout();
                    } else {
                        if (result.success == false) {
                            setInfoMsg(result.msg);
                            jQuery("#CstEle").val("");
                            jQuery("#CstEle").focus();
                        }
                        if (result.success == true) {
                            if (result.data.MCE_CD == null) {
                                setInfoMsg("Please enter valid cost element code.");
                                jQuery("#CstEle").val("");
                                jQuery("#CstEle").focus();
                            } else {
                                jQuery("#CstEleDesc").val(result.data.MCE_DESC);
                            }
                        }
                    }
                }
            });
        }
    }
    jQuery("#UOM").focusout(function () {
        var code = jQuery(this).val();
        UOMfocusOut(code);
    });
    function UOMfocusOut(code) {
        if (code != "") {
            if (code != "") {
                jQuery.ajax({
                    type: "GET",
                    url: "/Validation/validateUOM?uomcd=" + code,
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        if (result.login == false) {
                            Logout();
                        }
                        if (result.success == false) {
                            setInfoMsg(result.msg);
                            jQuery("#UOM").val("");
                            jQuery("#UOM").focus();
                        }
                        if (result.success == true) {
                            if (result.data.MT_CD == null) {
                                setInfoMsg("Please enter valid UOM.");
                                jQuery("#UOM").val("");
                                jQuery("#UOM").focus();
                            }
                        }
                    }
                });
            }
        }
    }
    jQuery("#Currency").focusout(function () {
        var code = jQuery(this).val();
        CurrencyfocusOut2(code);
    });
    function CurrencyfocusOut2(code) {
        if (code != "") {
            jQuery.ajax({
                type: "GET",
                url: "/Validation/validateCurrencyCode?curcd=" + code,
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == false) {
                        Logout();
                    } else {
                        if (result.success == false) {
                            setInfoMsg(result.msg);
                            jQuery("#Currency").val("");
                            jQuery("#Currency").focus();
                        }
                        if (result.success == true) {
                            if (result.data.MCR_CD == null) {
                                setInfoMsg("Please enter valid Currency code.");
                                jQuery("#Currency").val("");
                                jQuery("#Currency").focus();
                            } else {
                                jQuery("#ExchgRate").val("");
                                loadExchageRate(code);
                            }
                        }
                    }
                }
            });

        }
    }
    function loadExchageRate(code) {
        if (code != "") {
            jQuery.ajax({
                type: "GET",
                url: "/Validation/getExcahaneRate",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                data: { currency: code },
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            jQuery("#ExchgRate").val(result.data);
                            updateAmount();
                        } else {
                            if (result.type == "Error") {
                                setError(result.msg);
                            } else if (result.type == "Info") {
                                setInfoMsg(result.msg);
                            }
                            jQuery("#Currency").val("");
                            jQuery("#ExchgRate").val("");
                        }
                    } else {
                        Logout();
                    }
                }
            });
        }
    }
    jQuery("#Units").on("input", function () {
        var pax = parseFloat(jQuery("#Units").val());
        var UniteRate = parseFloat(jQuery("#UnitPrice").val());
        var Rate = parseFloat(jQuery("#ExchgRate").val());
        var Total = (pax * UniteRate * Rate);
        if (Total.toFixed(2) == "NaN") {
            jQuery("#Amount").val(0);
        } else {
            jQuery("#Amount").val(Total.toFixed(2));
        }

    });
    jQuery("#UnitPrice").on("input", function () {
        var pax = parseFloat(jQuery("#Units").val());
        var UniteRate = parseFloat(jQuery("#UnitPrice").val());
        var Rate = parseFloat(jQuery("#ExchgRate").val());
        var Total = (pax * UniteRate * Rate);
        if (Total.toFixed(2) == "NaN") {
            jQuery("#Amount").val(0);
        } else {
            jQuery("#Amount").val(Total.toFixed(2));
        }

    });
    jQuery("#ExchgRate").on("input", function () {
        var pax = parseFloat(jQuery("#Units").val());
        var UniteRate = parseFloat(jQuery("#UnitPrice").val());
        var Rate = parseFloat(jQuery("#ExchgRate").val());
        var Total = (pax * UniteRate * Rate);
        if (Total.toFixed(2) == "NaN") {
            jQuery("#Amount").val(0);
        } else {
            jQuery("#Amount").val(Total.toFixed(2));
        }

    });
    function updateAmount() {
        var pax = parseFloat(jQuery("#Units").val());
        var UniteRate = parseFloat(jQuery("#UnitPrice").val());
        var Rate = parseFloat(jQuery("#ExchgRate").val());
        var Total = (pax * UniteRate * Rate);
        if (Total.toFixed(2) == "NaN") {
            jQuery("#Amount").val(0);
        } else {
            jQuery("#Amount").val(Total.toFixed(2));
        }
    }
    jQuery('.btn-add-item').click(function (e) {
       
        if (jQuery("#Tih_job_no").val() == "") {
            setInfoMsg("Please Select Job");
            return;
        }
        //if (jQuery("#merg").val() == "") {
        //    setInfoMsg("Please Select Merge");
        //    return;
        //}
    
        if (jQuery("#Tih_inv_curr").val() == "") {
            setInfoMsg("Please Select Invoice Currency");
            return;
        }
        if (jQuery("#CstEle").val() == "") {
            setInfoMsg("Element cannot be empty");
            return;
        }
        if (jQuery("#CstEleDesc").val() == "") {
            setInfoMsg("Description cannot be empty");
            return;
        }
        if (jQuery("#UOM").val() == "") {
            setInfoMsg("UOM cannot be empty");
            return;
        }
        if (jQuery("#Units").val() == "") {
            setInfoMsg("No of units cannot be empty");
            return;
        }
        if (jQuery("#UnitPrice").val() == "") {
            setInfoMsg("Unit price cannot be empty");
            return;
        }
        if (jQuery("#Currency").val() == "") {
            setInfoMsg("Currency cannot be empty");
            return;
        }
        if (jQuery("#ExchgRate").val() == "") {
            setInfoMsg("Exchange rate cannot be empty");
            return;
        }
        Lobibox.confirm({
            msg: "Do you want to continue process?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                   
                    jQuery.ajax({
                        type: "GET",
                        url: "/CreditNote/AddItems",
                        data: { Service: jQuery("#Tid_ser_cd").val(), Ele: jQuery("#CstEle").val(), Desc: jQuery("#CstEleDesc").val(), Uom: jQuery("#UOM").val(), Units: jQuery("#Units").val(), UnPri: jQuery("#UnitPrice").val(), Curr: jQuery("#Currency").val(), Rate: jQuery("#ExchgRate").val(), Total: jQuery("#Amount").val(), Remark: jQuery("#rmk").val(), Merge: jQuery("#merg").val(), Job: jQuery("#Tih_job_no").val(), InvoiceCurr: jQuery("#Tih_inv_curr").val(), invparty: jQuery("#Tih_inv_party_cd").val() },
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                           
                            if (result.login == true) {
                                if (result.success == true) {
                                  
                                    SetInvDataList(result.data);
                                    jQuery("#Tih_inv_amt").val(ReplaceNumberWithCommas(Number(result.total).toFixed(2)));
                                    jQuery("#CstEle").val("");
                                    jQuery("#CstEleDesc").val("");
                                    jQuery("#UOM").val("");
                                    jQuery("#Units").val("");
                                    jQuery("#UnitPrice").val("");
                                    jQuery("#Currency").val("");
                                    jQuery("#rmk").val("");
                                    jQuery("#merg").val("");
                                    Rate: jQuery("#ExchgRate").val("");
                                    Total: jQuery("#Amount").val("");
                                    // updatePayment(result.total, "DEBT");
                                    $('#CstEle').focus();
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
            }

        });

    });
    function SetInvDataList(data) {
        jQuery('.tbl-inv-itm .new-row').remove();
        if (data.length > 0) {

            var removeBtn = "";            
           
            for (i = 0; i < data.length; i++) {
                if(data[i].Tid_cha_code=="NBT" || data[i].Tid_cha_code == "VAT"){
                    removeBtn = "";
                }
                else{
                    removeBtn = ' <button class="btn btn-sm-min btn-red-fullbg remove-inv-list">' +
                                        '<i class="fa fa-times" aria-hidden="true"></i>' +
                                   ' </button>';
                }

                jQuery('.tbl-inv-itm').append('<tr class="new-row">' +
                            '<td style="width:30px;">' + data[i].Tid_line_no + '</td>' +
                            '<td style="width:80px;">' + data[i].Tid_ser_cd + '</td>' +
                            '<td style="width:75px;">' + data[i].Tid_cha_code + '</td>' +
                            '<td style="width:205px;">' + data[i].Tid_cha_desc + '</td>' +
                            '<td style="width:50px;">' + data[i].Tid_units + '</td>' +
                            '<td style="width:50px;">' + data[i].Tid_qty + '</td>' +
                            '<td style="width:75px;" class="dec-align">' + ReplaceNumberWithCommas(Number(data[i].Tid_unit_amnt).toFixed(2)) + '</td>' +
                            '<td style="width:57px;">' + data[i].Tid_curr_cd + '</td>' +
                            '<td style="width:45px;" class="dec-align">' + ReplaceNumberWithCommas(Number(data[i].Tid_ex_rate).toFixed(2)) + '</td>' +
                            '<td style="width:85px;" class="dec-align">' + ReplaceNumberWithCommas(Number(data[i].Tid_cha_amt).toFixed(2)) + '</td>' +
                            '<td style="width:80px;">' + data[i].Tid_merge_chacode + '</td>' +
                            '<td style="width:80px;">' + data[i].Tid_rmk + '</td>' +
                            '<td style="width:30px;">' + removeBtn + '</td>' +

                        '</tr>');
            }
            RemoveDetailsFunction();
        }
    }
    function RemoveDetailsFunction() {
        jQuery(".remove-inv-list").unbind('click').click(function (evt) {
            evt.preventDefault();
            var td = jQuery(this).parent('td');
            var tr = jQuery(td).parent('tr');
            var line = jQuery(tr).find('td:eq(0)').html();
            Lobibox.confirm({
                msg: "Do you want to remove ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/CreditNote/RemoveInvItem",
                            data: { linenumber: line, InvoiceCurr: jQuery("#Tih_inv_curr").val() },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        SetInvDataList(result.data);
                                        jQuery("#Tih_inv_amt").val(ReplaceNumberWithCommas(Number(result.total).toFixed(2)));
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
    jQuery('.btn-save-inv').click(function (e) {
        var chnlvalue = $("#chnlSession").data('value');
        if (jQuery("#Tih_job_no").val() == "") {
            setInfoMsg("Please Select Job");
            return;
        }
        if (jQuery("#Tih_bl_h_no").val() == "" && chnlvalue != "CON" && chnlvalue != "WH" && chnlvalue != "YD" && chnlvalue != "TRP") {
            setInfoMsg("Please Select BL No");
            return;
        }
        if (jQuery("#Tih_inv_dt").val() == "") {
            setInfoMsg("Please Select Date");
            return;
        }
        if (jQuery("#Tih_inv_curr").val() == "") {
            setInfoMsg("Please Select Invoice Currency");
            return;
        }
        if (jQuery("#Tih_ex_cd").val() == "") {
            setInfoMsg("Please Select Account Manager");
            return;
        }
        if (jQuery("#Tih_cus_cd").val() == "") {
            setInfoMsg("Please Select Customer");
            return;
        }
        if (jQuery("#Tih_inv_party_cd").val() == "") {
            setInfoMsg("Please Select Invoice Party");
            return;
        }
        if (jQuery("#Tih_rmk").val() == "") {
            setInfoMsg("Please Select Remark");
            return;
        }
        Lobibox.confirm({
            msg: "Do you want to continue process?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    var check = "";
                    var formdata = jQuery("#inv-data").serialize();
                    jQuery.ajax({
                        type: "GET",
                        url: "/CreditNote/SaveCreditNote",
                        data: formdata,
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    setSuccesssMsg(result.msg);
                                    //clear page
                                    clearpage();

                                } else {
                                    if (result.Type == "Error") {
                                        setError(result.msg);

                                    }
                                    if (result.Type == "Info") {
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
    jQuery(".print-job-btn").click(function (evt) {
        evt.preventDefault();
        var InvType = "Full";
        Lobibox.confirm({
            msg: "Do you want to print request ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    if (jQuery("#Tih_inv_no").val() != "") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/CreditNote/validatePrint?Inv=" + jQuery("#Tih_inv_no").val(),
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                //if (result.login == false) {
                                //    Logout();
                                //} else {
                                //    if (result.success == true) {
                                //        window.open("/Invoice/Print?Inv=" + jQuery("#Tih_inv_no").val(), "_blank");
                                //    } else {
                                //        if (result.type == "Info") {
                                //            setInfoMsg(result.msg);
                                //        } else if (result.type == "Error") {
                                //            setError(result.msg);
                                //        } else {
                                //            setInfoMsg(result.msg);
                                //        }
                                //    }
                                //}

                                if (result.login == false) {
                                    Logout();
                                } else {
                                    if (result.success == true) {
                                        if ($("#Half").is(':checked') == true) {
                                            InvType = "Half";
                                        } if ($("#FullWOLogo").is(':checked') == true) {
                                            InvType = "FullWOLogo";
                                        }
                                        window.open("/Invoice/CreditPrint?Inv=" + jQuery("#Tih_inv_no").val() + "&InvType=" + InvType, "_blank");
                                    } else {
                                        if (result.type == "Info") {
                                            setInfoMsg(result.msg);
                                        } else if (result.type == "Error") {
                                            setError(result.msg);
                                        } else {
                                            setInfoMsg(result.msg);
                                        }
                                    }
                                }
                            }
                        });
                    } else {
                        setInfoMsg("Please select request for print.");
                    }
                }
            }
        });

    });

    jQuery(".btn-temp-print").click(function (evt) {
        evt.preventDefault();
        var InvType = "Full";
        Lobibox.confirm({
            msg: "Do you want to preview request ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    if (jQuery("#Tih_inv_no").val() != "") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/CreditNote/validatePrint?Inv=" + jQuery("#Tih_inv_no").val(),
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                //if (result.login == false) {
                                //    Logout();
                                //} else {
                                //    if (result.success == true) {
                                //        window.open("/Invoice/Print?Inv=" + jQuery("#Tih_inv_no").val(), "_blank");
                                //    } else {
                                //        if (result.type == "Info") {
                                //            setInfoMsg(result.msg);
                                //        } else if (result.type == "Error") {
                                //            setError(result.msg);
                                //        } else {
                                //            setInfoMsg(result.msg);
                                //        }
                                //    }
                                //}

                                if (result.login == false) {
                                    Logout();
                                } else {
                                    if (result.success == true) {
                                        if ($("#Half").is(':checked') == true) {
                                            InvType = "Half";
                                        } if ($("#FullWOLogo").is(':checked') == true) {
                                            InvType = "FullWOLogo";
                                        }
                                        window.open("/Invoice/CreditPrintPreview?Inv=" + jQuery("#Tih_inv_no").val() + "&InvType=" + InvType, "_blank");
                                    } else {
                                        if (result.type == "Info") {
                                            setInfoMsg(result.msg);
                                        } else if (result.type == "Error") {
                                            setError(result.msg);
                                        } else {
                                            setInfoMsg(result.msg);
                                        }
                                    }
                                }
                            }
                        });
                    } else {
                        setInfoMsg("Please select request for print.");
                    }
                }
            }
        });

    });
    function updatePayment(amount, type) {
        var payAmount = parseFloat(amount);
        var currentTot = (jQuery(".tot-amount-val").html() != "") ? parseFloat(jQuery(".tot-amount-val").html()) : 0;
        var finTot = parseFloat(currentTot) + parseFloat(amount);
        if (type == "DEBT") {
            updateCurrencyAmount(parseFloat(finTot), jQuery("#Tih_cus_cd").val(), jQuery("#Tih_inv_no").val());
        } else {
            updateCurrencyAmount(parseFloat(finTot), jQuery("#Tih_cus_cd").val(), "");
        }

    }
    function clearpage() {
        jQuery("#Tih_job_no").val("");
        jQuery("#Tih_bl_h_no").val("");
        jQuery('#Tih_inv_dt').val(my_date_format(new Date()));
        jQuery("#Tih_inv_curr").val("");
        jQuery("#Tih_inv_no").val("");
        jQuery("#Tih_ex_cd").val("");
        jQuery("#Tih_cus_cd").val("");
        jQuery("#Tih_inv_party_cd").val("");
        jQuery("#Tih_rmk").val("");
        jQuery("#Tih_other_ref_no").val("");
        jQuery("#Tih_other_ref_no").val("");
        jQuery("#Exc_name").val("");
        jQuery("#Tih_pono").val("");
        jQuery("#Tih_inv_amt").val("");
        jQuery('.tbl-inv-itm .new-row').remove();
        jQuery(".tot-paid-amount-val").empty();
        jQuery(".bal-amount-val").empty();
        jQuery('table.payment-table').remove();
        jQuery('.tbl-cus-name .new-row').remove();
        jQuery('.tbl-inv-party .new-row').remove();
        jQuery("#Tih_exec_name").val("");
        jQuery("#Tih_pono").val("");
        jQuery("#Tih_inv_amt").val("");
        jQuery('#Tih_inv_curr').val("LKR");

        jQuery('.btn-save-inv').removeAttr("disabled");
        jQuery('.btn-temp-print').removeAttr("disabled");
        jQuery('.btn-approve').attr("disabled", "true");
        jQuery('.btn-cancel').attr("disabled", "true");
        jQuery('.print-job-btn').attr("disabled", "true");
        jQuery('#Tih_inv_curr').val("LKR");
        jQuery("#Tih_inv_cred_no").val("");
        
        jQuery.ajax({
            type: "GET",
            url: "/CreditNote/ClearSession",
            data: {},
            contentType: "application/json;charset=utf-8",
            dataType: "json",
        })
    }
    jQuery(".clear-inv").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to clear data ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    window.location.href = "/CreditNote";
                }
            }
        })

    });
    function ReplaceNumberWithCommas(yourNumber) {
        //Seperates the components of the number
        var n = yourNumber.toString().split(".");
        //Comma-fies the first part
        n[0] = n[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        //Combines the two sections
        return n.join(".");
    }
    jQuery(".cst-merg-srch").click(function (evt) {
        evt.preventDefault();
        var headerKeys = Array()
        headerKeys = ["Row", "Cost Code", "Description", "Account Code"];
        field = "costelem";
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery("#merg").focusout(function () {
        var code = jQuery(this).val();
        cstelemtFocusOut2(code);
    });
    jQuery(".curency-search").focusout(function () {
        jQuery("#Tih_ex_cd").focus();
    });
    function cstelemtFocusOut2(code) {
        if (code != "") {
            jQuery.ajax({
                type: "GET",
                url: "/Validation/validateCostElement?eleCode=" + code,
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == false) {
                        Logout();
                    } else {
                        if (result.success == false) {
                            setInfoMsg(result.msg);
                            jQuery("#merg").val("");
                            jQuery("#merg").focus();
                        }
                        if (result.success == true) {
                            if (result.data.MCE_CD == null) {
                                setInfoMsg("Please enter valid cost Merge code.");
                                jQuery("#merg").val("");
                                jQuery("#merg").focus();
                            } else {
                                //jQuery("#CstEleDesc").val(result.data.MCE_DESC);
                            }
                        }
                    }
                }
            });
        }
    }
    jQuery('.btn-approve').click(function (e) {
       
        if (jQuery("#Tih_inv_no").val() == null || jQuery("#Tih_inv_no").val() == "") {
            setInfoMsg("Please select or enter invoice no.");
            return;
        }
        
        Lobibox.confirm({
            msg: "Do you want to continue process?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                  
                    var check = "";
                    jQuery.ajax({
                        type: "GET",
                        url: "/CreditNote/StatusChange",
                        data: { Invoiceno: jQuery("#Tih_inv_no").val(),Status: "A" },
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                         
                            if (result.login == true) {
                                if (result.success == true) {
                                    if (result.type == "Succ") {
                                        setSuccesssMsg(result.msg);
                                        clearpage();
                                    }
                                    if (result.type == "Info") {
                                        setInfoMsg(result.msg);
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
                }
            }

        });
    });
    jQuery('.btn-cancel').click(function (e) {

        if (jQuery("#Tih_inv_no").val() == null || jQuery("#Tih_inv_no").val() == "") {
            setInfoMsg("Please select invoice no.");
            return;
        }

        Lobibox.confirm({
            msg: "Do you want to continue process?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    var check = "";
                    jQuery.ajax({
                        type: "GET",
                        url: "/CreditNote/StatusChange",
                        data: { Invoiceno: jQuery("#Tih_inv_no").val(), Status: "C" },
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    setSuccesssMsg("Sucessfully Cancelled");
                                    clearpage();
                                } else {
                                    if (result.Type == "Error") {
                                        setError(result.msg);

                                    }
                                    if (result.Type == "Info") {
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

    $("#Tih_job_no").keypress(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }
    });
    $("#Tih_bl_h_no").keypress(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }
    });
    $("#Tih_inv_dt").keypress(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }
    });
    $("#Tih_inv_curr").keypress(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }
    });
    $("#Tih_inv_no").keypress(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }
    });
    $("#Tih_ex_cd").keypress(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }
    });
    $("#Tih_exec_name").keypress(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }
    });
    $("#Tih_cus_cd").keypress(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }
    });
    $("#Tih_inv_party_cd").keypress(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }
    });
    $("#Tih_other_ref_no").keypress(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }
    });
    $("#Tih_pono").keypress(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }
    });
    $("#CstEle").keypress(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }
    });
    $("#CstEleDesc").keypress(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }
    });
    $("#UOM").keypress(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }
    });
    $("#Units").keypress(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }
    });
    $("#UnitPrice").keypress(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }
    });
    $("#Currency").keypress(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }
    });
    $("#ExchgRate").keypress(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }
    });
    $("#Amount").keypress(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }
    });
    $("#rmk").keypress(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }
    });
    $("#merg").keypress(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }
    });

    jQuery("#Tih_bl_h_no").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            if (jQuery("#Tih_job_no").val() != "" && jQuery("#Tih_job_no").val() != null) {
                click_status = "H";
                var headerKeys = Array()
                headerKeys = ["Row", "Code", "Customer", "Agent", "Date"];
                field = "BL_H_DOC_NOINV"
                data = jQuery("#Tih_job_no").val();
                var x = new CommonSearchDateFiltered(headerKeys, field, data);
            }
            else {
                setInfoMsg("Select job no for H/BL number");
            }
        }
    });
    jQuery("#Tih_inv_no").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Credit Note No", "Date"];
            field = "InvoiceNoCrd"
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery("#Tih_inv_curr").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
        
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description"];
            field = "curcd2";
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery("#Tih_cus_cd").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
          
            if (jQuery("#Tih_job_no").val() != null && jQuery("#Tih_job_no").val() != "") {
                var headerKeys = Array()
                headerKeys = ["Row", "Code", "Name", "NIC", "Mobile", "BR No"];
                field = "cusCode9jn";
                data = jQuery("#Tih_job_no").val();
                var x = new CommonSearch(headerKeys, field, data);
            }
            else {
                setInfoMsg("Select job no for customer search");
            }
        }
    });
    jQuery("#Tih_inv_party_cd").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
          
            var headerKeys = Array()
            headerKeys = ["Row", "Name", "Code", "NIC", "Mobile", "BR No"];
            field = "cusCodeCp"
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery("#CstEle").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
          
            evt.preventDefault();
            var headerKeys = Array()
            headerKeys = ["Row", "Description", "Cost Code", "Account Code"];
            field = "costele";
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery("#UOM").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
         
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description"];
            field = "UOMPTY"
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery("#Currency").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
           
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description"];
            field = "curcd";
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery("#merg").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
        
            evt.preventDefault();
            var headerKeys = Array()
            headerKeys = ["Row", "Cost Code", "Description", "Account Code"];
            field = "costelem";
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery("#Tih_other_ref_no").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
        
            var headerKeys = Array()
            headerKeys = ["Row", "Invoice No", "Job No", "Date"];
            field = "InvoiceNoRef"
            var x = new CommonSearchDateFiltered(headerKeys, field);
        }
    });
    jQuery("#Tih_ex_cd").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
        
            var headerKeys = Array()
            headerKeys = ["Row", "Epf No", "Category", "First Name", "Last Name", "NIC"];
            field = "employee2";
            var x = new CommonSearch(headerKeys, field);
        }
    });

    jQuery("#Tih_inv_dt").focusout(function () {
        var code = jQuery(this).val();
        if (code == "") {
            jQuery('#Tih_inv_dt').val(my_date_format(new Date()));
        }
    });

});