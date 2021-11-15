var globeCustomer = "";
var globeInvNo = "";
var invoiceType = "";
jQuery(document).ready(function () {
    jQuery("#Sird_settle_amt").val("");
    jQuery("#Deposit_bank_cd").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Account Code", "Description"];
            field = "bnkAcc";
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery(".bnk-acc-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Account Code", "Description"];
        field = "bnkAcc";
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery("#cheque-bank").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description", "Id"];
            field = "chkBnkAcc"
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery(".chk-bnk-acc-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description","Id"];
        field = "chkBnkAcc";
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery("#cheque-bank").focusout(function () {
        if (jQuery(this).val() != "") {
            jQuery.ajax({
                type: "GET",
                url: "/Payment/getBankCode",
                data: { bankId: jQuery(this).val() },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.code != null) {
                                jQuery("#Cheque_bnk_cd").val(result.code);
                            } else {
                                jQuery("#Cheque_bnk_cd").val("");
                            }
                        } else {
                            jQuery("#cheque-bank").val("");
                            jQuery("#Cheque_bnk_cd").val("");
                            jQuery("#cheque-bank").focus();
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
            jQuery("#cheque-bank").val("");
            jQuery("#Cheque_bnk_cd").val("");
        }
    });
    jQuery("#Cred_crd_ofline").on("change", function () {
        if (jQuery('#Cred_crd_ofline').is(":checked")) {
            jQuery('#Cred_crd_online').removeAttr('checked');
            var promotion = "false";
            var piriod = 0;
            var bank = "";
            if (jQuery('#Cred_crd_promotion').is(":checked")) {
                promotion = "true";
            }
            if (jQuery("#Cred_crd_promo-mnth-period").val() != "") {
                piriod = jQuery("#Cred_crd_promo-mnth-period").val();
            }
            if (jQuery("#Cred_crd_bank").val() != "") {
                bank = jQuery("#Cred_crd_bank").val();
            }
            var online = "false";
            var offline = "true";
            changeValueOfcred(online, offline, promotion, bank, piriod);
        } else {
            jQuery("#Cred_crd_ofline").prop('checked', true);
        }
    });

    jQuery("#Cred_crd_online").on("change", function () {
        if (jQuery('#Cred_crd_online').is(":checked")) {
            jQuery("#Cred_crd_ofline").removeAttr('checked');
            var promotion = "false";
            var piriod = 0;
            var bank = "";
            if (jQuery('#Cred_crd_promotion').is(":checked")) {
                promotion = "true";
            }
            if( jQuery("#Cred_crd_promo-mnth-period").val()!=""){
                piriod = jQuery("#Cred_crd_promo-mnth-period").val();
            }
            if(jQuery("#Cred_crd_bank").val()!=""){
                bank=jQuery("#Cred_crd_bank").val();
            }
            var online = "true";
            var offline = "false";
            changeValueOfcred(online, offline, promotion, bank, piriod);
        } else {
            jQuery("#Cred_crd_online").prop('checked', true);
        }
    });
    if (jQuery('#Cred_crd_promotion').is(":checked")) {
        jQuery('#Cred_crd_promo-mnth-period').removeAttr('readonly');
    } else {
        jQuery('#Cred_crd_promo-mnth-period').attr('readonly', true);
    }
    jQuery("#Cred_crd_promotion").on("change", function () {

        if (jQuery('#Cred_crd_promotion').is(":checked")) {
            jQuery('#Cred_crd_promo-mnth-period').removeAttr('readonly');
            changeVal();
        } else {
            jQuery('#Cred_crd_promo-mnth-period').val("");
            jQuery('#Cred_crd_promo-mnth-period').attr('readonly', 'true');
            changeVal();
        }
    });


    jQuery("#Cred_crd_promo-mnth-period").on("input", function () {
        var online = "false";
        var offline = "false";
        var promotion = "false";
        var bank = "";
        var piriod = 0;
        if (jQuery('#Cred_crd_online').is(":checked")) {
            online = "true";
        }
        if (jQuery('#Cred_crd_ofline').is(":checked")) {
            offilne = "true";
        }
        if (jQuery('#Cred_crd_promotion').is(":checked")) {
            promotion = "true";
        }
        if (jQuery("#Cred_crd_bank").val() != "") {
            bank = jQuery("#Cred_crd_bank").val();
        }
        if (jQuery("#Cred_crd_promo-mnth-period").val() != "") {
            piriod = jQuery("#Cred_crd_promo-mnth-period").val();
        }
        if (jQuery("#Cred_crd_promo-mnth-period").val() != "") {
            if (jQuery.isNumeric(jQuery("#Cred_crd_promo-mnth-period").val())) {
                

                changeValueOfcred(online, offline, promotion, bank, piriod);

            } else {
                jQuery("#Cred_crd_promo-mnth-period").val("");
                setError("Invalid period.");
            }
        }else{
            changeValueOfcred(online, offline, promotion, bank, 0);
        }
    });

    function changeVal() {
        var promotion = "false";
        var piriod = 0;
        var bank = "";
        if (jQuery('#Cred_crd_promotion').is(":checked")) {
            promotion = "true";
        }
        if (jQuery("#Cred_crd_promo-mnth-period").val() != "") {
            piriod = jQuery("#Cred_crd_promo-mnth-period").val();
        }
        if (jQuery("#Cred_crd_bank").val() != "") {
            bank = jQuery("#Cred_crd_bank").val();
        }
        var online = "false";
        var offline = "false";
        if (jQuery('#Cred_crd_online').is(":checked")) {
            online = "true";
        }
        if (jQuery('#Cred_crd_ofline').is(":checked")) {
            offline = "true";
        }
        if (online == "true" || offline == "true") {
            changeValueOfcred(online, offline, promotion, bank, piriod);
        } else {
            jQuery(".control-label.mid-code-cls").empty();
        }
    }
    function changeValueOfcred(online, offline, promotion, bank, piriod) {
        jQuery.ajax({
            type: "GET",
            url: "/Payment/LoadMIDno",
            data: { online: online, offline: offline, promotion: promotion, bank: bank, piriod: piriod },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        jQuery(".control-label.mid-code-cls").empty();
                        jQuery(".control-label.mid-code-cls").append("<div style='font-weight: bold;'>" + result.micode + "</div>");
                    } else {
                        jQuery(".control-label.mid-code-cls").empty();
                        jQuery(".control-label.mid-code-cls").append("<div style='color: red;'>" + result.msg + "</div>");
                    }
                } else {
                    Logout();
                }
            }

        });

    }
    jQuery("#Cred_crd_bank").focusout(function () {
        if (jQuery(this).val() != "") {
            jQuery.ajax({
                type: "GET",
                url: "/Payment/getBankCode",
                data: { bankId: jQuery(this).val() },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.code != null) {
                                jQuery("#Cred_crd_bank_value").val(result.code);
                            } else {
                                jQuery("#Cred_crd_bank_value").val("");
                            }
                        } else {
                            jQuery("#Cred_crd_bank_value").val("");
                            jQuery("#Cred_crd_bank").val("");
                            jQuery("#Cred_crd_bank").focus();
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


            ///update credit card types
            jQuery.ajax({
                type: "GET",
                url: "/Payment/LoadCardType",
                data: { bank: jQuery(this).val() },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        var select = document.getElementById("Cred-card-typ");
                        jQuery("#Cred-card-typ").empty();
                        var options = [];
                        var option = document.createElement('option');
                        if (result.success == true) {
                            if (result.data != null && result.data.length != 0) {
                                for (var i = 0; i < result.data.length; i++) {
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
                            option.text = "Select Type";
                            option.value = "";
                            options.push(option.outerHTML);
                            select.insertAdjacentHTML('beforeEnd', options.join('\n'));
                        }
                    } else {
                        Logout();
                    }
                }
            });
            ///end update credit card types

        } else {
            jQuery("#Cred_crd_bank_value").val("");
            jQuery("#Cred_crd_bank").val("");
        }
        ///changes add foe change mid code
        changeVal();
        ///end change mid code
    });

    jQuery("#Cread-crd-expire-dt").datepicker({ dateFormat: "dd/M/yy"})
    jQuery("#Cheque_branch_cd").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            if (jQuery("#Cheque_bnk_cd").val() != "") {
                var headerKeys = Array()
                headerKeys = ["Row", "Code", "Description"];
                field = "bnkChqBranch";
                data = jQuery("#Cheque_bnk_cd").val();
                var x = new CommonSearch(headerKeys, field, data);
            } else {
                jQuery("#cheque-bank").focus();
                setError("Select bank first.");
            }
        }
    });
    jQuery(".chk-bnk-branch-search").click(function () {
        if (jQuery("#Cheque_bnk_cd").val() != "") {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description"];
            field = "bnkChqBranch";
            data = jQuery("#Cheque_bnk_cd").val();
            var x = new CommonSearch(headerKeys, field, data);
        } else {
            jQuery("#cheque-bank").focus();
            setError("Select bank first.");
        }
    });

    jQuery("#Cred_crd_bank").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description", "Id"];
            field = "credBnkAcc"
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery(".cred-crd-bnksearch").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description", "Id"];
        field = "credBnkAcc";
        var x = new CommonSearch(headerKeys, field);
    });

    
    jQuery(".pay-mode-panel.cash-panel").hide();
    jQuery(".pay-mode-panel.cheque-panel").hide();
    jQuery("#Sird_pay_tp").change(function () {
        clearAllValues();
        if (jQuery(this).val() != "") {
            switch (jQuery(this).val()) {
                case "EZCASH":
                    hideAllPanel();
                    break;
                case "TR_CHEQUE":
                    displayPanels(true, false, false, true, false, false, false, false, false, false);
                    break;
                case "BANK_SLIP":
                    displayPanels(false, true, false, true, false, false, false, false, false, false);
                    break;
                case "DEBT":
                    displayPanels(false, false, true, true, false, false, false, false, false, false);
                    break;
                case "CASH":
                    displayPanels(false, false, false, true, false, false, false, false, false, false)
                    break;
                case "ADVAN":
                    displayPanels(false, false, false, true, true, false, false, false, false, false)
                    break;
                case "CRNOTE":
                    displayPanels(false, false, false, true, false, true, false, false, false, false);
                    break;
                case "CHEQUE":
                    displayPanels(true, false, false, true, false, false, false, false, false, false);
                    break;
                case "CRCD":
                    displayPanels(false, false, false, true, false, false, false, true, false, false);
                    break;
                case "LORE":
                    displayPanels(false, false, false, true, false, false, false, false, true, false)
                    break;
                case "GVO":
                    displayPanels(false, false, false, true, false, false, false, false, false, true)
                    break;
                case "STAR_PO":
                    hideAllPanel();
                    break;
                default:
                    hideAllPanel();
                    break;
            }
        } else {
            hideAllPanel();
        }
    });
    //display hide panels
    var hideAllPanel = function () {
            jQuery(".pay-mode-panel.cheque-panel").hide();
            jQuery(".pay-mode-panel.bnk_slip-panel").hide();
            jQuery(".pay-mode-panel.debt-panel").hide();
            jQuery(".pay-mode-panel.cash-panel").hide();
            jQuery(".pay-mode-panel.advan-panel").hide();
            jQuery(".pay-mode-panel.cred-note-panel").hide();
            jQuery(".pay-mode-panel.cheque-panel").hide();
            jQuery(".pay-mode-panel.crcd-panel").hide();
            jQuery(".pay-mode-panel.lore-panel").hide();
            jQuery(".pay-mode-panel.gift-voucher-panel").hide();
    }
    var displayPanels = function (TR_CHEQUE, BANK_SLIP, DEBT, CASH, ADVAN, CRNOTE, CHEQUE, CRCD, LORE, GVO) {
        if (TR_CHEQUE == true || CHEQUE==true) {
            jQuery(".pay-mode-panel.cheque-panel").show();
        } else {
            jQuery(".pay-mode-panel.cheque-panel").hide();
        }

        if (BANK_SLIP == true) {
            jQuery(".pay-mode-panel.bnk_slip-panel").show();
        } else {
            jQuery(".pay-mode-panel.bnk_slip-panel").hide();
        }

        if (DEBT == true) {
            jQuery(".pay-mode-panel.debt-panel").show();
        } else {
            jQuery(".pay-mode-panel.debt-panel").hide();
        }
        if (CASH == true) {
            jQuery(".pay-mode-panel.cash-panel").show();
        } else {
            jQuery(".pay-mode-panel.cash-panel").hide();
        }

        if (ADVAN == true) {
            jQuery(".pay-mode-panel.advan-panel").show();
        } else {
            jQuery(".pay-mode-panel.advan-panel").hide();
        }

        if (CRNOTE == true) {
            jQuery(".pay-mode-panel.cred-note-panel").show();
        } else {
            jQuery(".pay-mode-panel.cred-note-panel").hide();
        }
        //if (CHEQUE == true) {
        //    jQuery(".pay-mode-panel.cheque-panel").show();
        //} else {
        //    jQuery(".pay-mode-panel.cheque-panel").hide();
        //}
        if (CRCD == true) {
            jQuery(".pay-mode-panel.crcd-panel").show();
        } else {
            jQuery(".pay-mode-panel.crcd-panel").hide();
        }
        if (LORE == true) {
            jQuery(".pay-mode-panel.lore-panel").show();
        } else {
            jQuery(".pay-mode-panel.lore-panel").hide();
        }
        if (GVO == true) {
            jQuery(".pay-mode-panel.gift-voucher-panel").show();
        } else {
            jQuery(".pay-mode-panel.gift-voucher-panel").hide();
        }

    }
    //end display hide panels

    jQuery(".chk-date").datepicker({ dateFormat: "dd/M/yy" });
    jQuery("#Bnk_slip_acc_dte").datepicker({ dateFormat: "dd/M/yy" });
    

    function setError(msg) {
        if (jQuery(".animated-super-fast").length == 0) {
            Lobibox.alert('error',
            {
                msg: msg
            });
        }
    }
    function setSuccesssMsg(msg) {
        if (jQuery(".animated-super-fast").length == 0) {
            Lobibox.alert('success',
            {
                msg: msg
            });
        }
    }

    function setInfoMsg(msg) {
        if (jQuery(".animated-super-fast").length == 0) {
            Lobibox.alert('info',
            {
                msg: msg
            });
        }
    }


    jQuery("#Debt_bank_name").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description", "Id"];
            field = "debtBnkAcc"
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery(".debt-bnksearch").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description", "Id"];
        field = "debtBnkAcc";
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery("#Debt_bank_name").focusout(function () {
        if (jQuery(this).val() != "") {
            jQuery.ajax({
                type: "GET",
                url: "/Payment/getBankCode",
                data: { bankId: jQuery(this).val() },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.code != null) {
                                jQuery("#Debt_bank_cd").val(result.code);
                            } else {
                                jQuery("#Debt_bank_cd").val("");
                            }
                        } else {
                            jQuery("#Debt_bank_cd").val("");
                            jQuery("#Debt_bank_name").val("");
                            jQuery("#Debt_bank_name").focus();
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
            jQuery("#Debt_bank_cd").val("");
        }
    });
    //advance panel
    jQuery("#Advan_ref_no").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Receipt No", "Receipt Ref", "Receipt Date", "Ref", "Balancce Amt", "Customer"];
            data = globeCustomer;//pass customer from here{default set to null for future use}
            field = "advRefNo"
            var x = new CommonSearch(headerKeys, field,data);
        }
    });
    jQuery(".advan-ref-no-srch").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Receipt No", "Receipt Ref", "Receipt Date", "Ref", "Balancce Amt", "Customer"];
        data = globeCustomer;//pass customer from here{default set to null for future use}
        field = "advRefNo";
        var x = new CommonSearch(headerKeys, field, data);
    });

    jQuery("#Advan_ref_no").on("focusout", function () {
        if (jQuery(this).val() != "") {
            jQuery.ajax({
                type: "GET",
                url: "/Payment/getAdvanceRefAmount",
                data: { cuscd: globeCustomer, receiptno: jQuery(this).val() },//pass customer from here{default set to null for future use}
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.data != null) {
                                jQuery("#Advan_ref_amount").val(result.data);
                            } else {
                                jQuery("#Advan_ref_amount").val("");
                            }
                        } else {
                            jQuery("#Advan_ref_amount").val("");
                            jQuery("#Advan_ref_no").val("");
                            jQuery("#Advan_ref_no").focus();
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
            jQuery("#Advan_ref_amount").val("");
        }
    });
    //end advance panel

    //credit note panel
    jQuery("#Cred_note_ref_no").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Invoice", "Manual Ref", "Base No", "Credit Amt"];
            data = globeCustomer;//pass customer from here{default set to null for future use}
            field = "credNteRefNo"
            var x = new CommonSearch(headerKeys, field, data);
        }
    });
    jQuery(".cred-note-ref-no-srch").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Invoice", "Manual Ref", "Base No", "Credit Amt"];
        data = globeCustomer;//pass customer from here{default set to null for future use}
        field = "credNteRefNo";
        var x = new CommonSearch(headerKeys, field, data);
    });

    jQuery("#Cred_note_ref_no").on("focusout", function () {
        if (jQuery(this).val() != "") {
            jQuery.ajax({
                type: "GET",
                url: "/Payment/getCreditRefAmount",
                data: { cuscd: globeCustomer, receiptno: jQuery(this).val() },//pass customer from here{default set to null for future use}
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.data != null) {
                                jQuery("#Cred_note_ref_amount").val(result.data);
                            } else {
                                jQuery("#Cred_note_ref_amount").val("");
                            }
                        } else {
                            jQuery("#Cred_note_ref_amount").val("");
                            jQuery("#Cred_note_ref_no").val("");
                            jQuery("#Cred_note_ref_no").focus();
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
            jQuery("#Cred_note_ref_amount").val("");
        }
    });

    //end credit note panel
    //Loyalty crd
    jQuery("#Lore_crd_no").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Card No", "Serial", "Type"];
            field = "loyalCrdSrch"
            data = globeCustomer;//pass customer from here{default set to null for future use}
            var x = new CommonSearch(headerKeys, field, data);
        }
    });
    jQuery(".lore-cus-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Card No", "Serial", "Type"];
        field = "loyalCrdSrch";
        data = globeCustomer;
        var x = new CommonSearch(headerKeys, field, data);
    });

    jQuery("#Lore_crd_no").on("focusout", function () {
        if (jQuery(this).val() != "") {
            jQuery.ajax({
                type: "GET",
                url: "/Payment/getLoyaltyDetails",
                data: { customer:globeCustomer ,loyalNu: jQuery(this).val() },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.data != null) {
                                jQuery(".cust-display-lbl").empty();
                                jQuery(".bal-point-display-lbl").empty();
                                jQuery(".loy-typ-display-lbl").empty();
                                jQuery(".pnt-val-display-lbl").empty();
                                jQuery(".cust-display-lbl").append(result.data.Salcm_cus_cd);
                                jQuery(".bal-point-display-lbl").append(result.data.Salcm_bal_pt);
                                jQuery(".loy-typ-display-lbl").append(result.data.Salcm_loty_tp);
                                jQuery(".pnt-val-display-lbl").append(result.data.Salcm_col_pt);
                            } else {
                                jQuery("#Lore_crd_no").val("");
                            }
                        } else {
                            jQuery("#Lore_crd_no").val("");
                            jQuery("#Cred_note_ref_no").focus();
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
            jQuery("#Cred_note_ref_amount").val("");
        }
    });



    //end loyalty card
    ///gift voucher search
    jQuery("#Gift_vouche_no").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Page","Reference", "Book", "Status", "From","To","Create","Balance"];
            field = "giftVoucherSrch"
            data = globeCustomer;//pass customer from here{default set to null for future use}
            var x = new CommonSearch(headerKeys, field, data);
        }
    });
    jQuery(".gift-voucher-srch").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row","Page", "Reference", "Book", "Status", "From", "To", "Create", "Balance"];
        field = "giftVoucherSrch";
        data = globeCustomer;//pass customer from here{default set to null for future use}
        var x = new CommonSearch(headerKeys, field, data);
    });

    jQuery("#Gift_vouche_no").on("focusout", function () {
        if (jQuery(this).val() != "") {
            jQuery.ajax({
                type: "GET",
                url: "/Payment/getGiftVoucherDetails",
                data: { voucherPageNo: jQuery(this).val() },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.data != null) {
                                jQuery('.voucher-table .new-row').remove();
                                for (i = 0; i < result.data.length; i++) {
                                    if (result.data.Gvp_book != "") {
                                        jQuery('.voucher-table').append(
                                            '<tr class="new-row fixed-row"><td style="text-align:center;"><img class="add-img add-data-cls" src="../Resources/images/Add.png"></td>' +
                                            '<td class="val-field">' + result.data[i].Gvp_book + '</td>' +
                                            '<td class="val-field">' + result.data[i].Gvp_page + '</td>' +
                                            '<td class="val-field">' + result.data[i].Gvp_amt + '</td>' +
                                            '<td class="val-field">' + result.data[i].Gvp_gv_cd + '</td>' +
                                            '<td class="val-field">' + result.data[i].Gvp_gv_prefix + '</td>' +
                                            '<td class="val-field">' + my_date_format(result.data[i].Gvp_valid_from) + '</td>' +
                                            '<td class="val-field">' + my_date_format(result.data[i].Gvp_valid_to) + '</td>' +
                                            '<td class="val-field" style="display:none;>' + result.data[i].Gvp_cus_cd + '</td>' +
                                            '<td class="val-field" style="display:none;>' + result.data[i].Gvp_cus_name + '</td>' +
                                            '<td class="val-field" style="display:none;>' + result.data[i].Gvp_cus_add1 + '</td>' +
                                            '<td class="val-field" style="display:none;>' + result.data[i].Gvp_cus_mob + '</td>' +
                                            '</tr>');
                                    }
                                }
                                jQuery(".gift-cust-cd-lbl").empty();
                                jQuery(".gift-cust-name-lbl").empty();
                                jQuery(".gift-cust-add-lbl").empty();
                                jQuery(".gift-cust-mob-lbl").empty();
                                jQuery(".gift-cust-book-lbl").empty();
                                jQuery(".gift-cust-book-code-lbl").empty();
                                jQuery(".gift-cust-pref-lbl").empty();

                                jQuery(".add-img.add-data-cls").on("click", function () {
                                    var td = jQuery(this);
                                    var to_date = jQuery(td).closest("tr").find('td:eq(7)').text();
                                    var from_date = jQuery(td).closest("tr").find('td:eq(6)').text();
                                    if (new Date(Date.parse(to_date)) > new Date()) {
                                        
                                        var Gvp_book = jQuery(td).closest("tr").find('td:eq(1)').text();
                                        var Gvp_cus_name = jQuery(td).closest("tr").find('td:eq(9)').text();
                                        var Gvp_cus_cd = jQuery(td).closest("tr").find('td:eq(8)').text();
                                        var Gvp_cus_add1 = jQuery(td).closest("tr").find('td:eq(10)').text();
                                        var Gvp_cus_mob = jQuery(td).closest("tr").find('td:eq(11)').text();
                                        var Gvp_gv_prefix = jQuery(td).closest("tr").find('td:eq(5)').text();
                                        var Gvp_gv_cd = jQuery(td).closest("tr").find('td:eq(4)').text();
                                        jQuery(".gift-cust-cd-lbl").empty();
                                        jQuery(".gift-cust-cd-lbl").append(Gvp_cus_cd);
                                        jQuery(".gift-cust-name-lbl").empty();
                                        jQuery(".gift-cust-name-lbl").append(Gvp_cus_name);
                                        jQuery(".gift-cust-add-lbl").empty();
                                        jQuery(".gift-cust-add-lbl").append(Gvp_cus_add1);
                                        jQuery(".gift-cust-mob-lbl").empty();
                                        jQuery(".gift-cust-mob-lbl").append(Gvp_cus_mob);

                                        jQuery(".gift-cust-book-lbl").empty();
                                        jQuery(".gift-cust-book-lbl").append(Gvp_book);

                                        jQuery(".gift-cust-book-code-lbl").empty();
                                        jQuery(".gift-cust-book-code-lbl").append(Gvp_gv_cd);
                                        jQuery(".gift-cust-pref-lbl").empty();
                                        jQuery(".gift-cust-pref-lbl").append(Gvp_gv_prefix);
                                    } else {
                                        setError("Gift voucher From and To dates not in range\nFrom Date - " +from_date + "\nTo Date - " + to_date);
                                    }
                                   
                                    
                                });
                            } else {
                                jQuery('.voucher-table .new-row').remove();
                            }
                        } else {
                            jQuery("#Gift_vouche_no").val("");
                            jQuery("#Gift_vouche_no").focus();
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
            jQuery('.voucher-table .new-row').remove();
            jQuery("#Cred_note_ref_amount").val("");
        }
    });

   
    var my_date_format = function (input) {
        input = new Date(parseInt(input.substr(6)));
        var monthNames = [
          "Jan", "Feb", "Mar",
          "Apr", "May", "Jun", "Jul",
          "Aug", "Sep", "Oct",
          "Nov", "Dec"
        ];

        var date = new Date(Date.parse(input));
        var day = date.getDate();
        var monthIndex = date.getMonth();
        var year = date.getFullYear();

        return (day + "/" + monthNames[monthIndex] + "/" + year);
    };
    ///end gift voucher search

    

    var addedMountVal = 0;
    //payment adding button
    jQuery(".add-price-amount-btn").on("click", function (evt) {
        evt.preventDefault();
        var url = "";
        var data = {};
        if (jQuery("#Sird_settle_amt").val() != "") {
            if (jQuery.isNumeric(jQuery("#Sird_settle_amt").val())) {
                if (jQuery("#Sird_pay_tp").val() != "") {
                    var payType=jQuery("#Sird_pay_tp").val();
                    if (payType == "EZCASH" || payType == "STAR_PO") {
                        url = "/Payment/updateEazyCashStarPoPayment";
                        data = { payMode:jQuery("#Sird_pay_tp").val(),
                            totAmount:jQuery(".tot-amount-val").html(),
                            addedAmount:jQuery("#Sird_settle_amt").val(),
                            invoiceNo: globeInvNo,
                            customer: globeCustomer
                        };
                    }
                    if (payType == "CHEQUE" || payType=="TR_CHEQUE") {
                        url = "/Payment/updateChequePayment";
                        data = { payMode:jQuery("#Sird_pay_tp").val(),
                            totAmount:jQuery(".tot-amount-val").html(),
                            addedAmount:jQuery("#Sird_settle_amt").val(),
                            chequebank: jQuery("#cheque-bank").val(),
                            chequeBankCd:jQuery("#Cheque_bnk_cd").val(),
                            chqBranchCd :jQuery("#Cheque_branch_cd").val(),
                            chequeNum:jQuery("#Cheque_ref_no").val(), 
                            chqDt:jQuery("#Cheque_dt").val(), 
                            depositBank:jQuery("#Deposit_bank_cd").val(),
                            depositBranch:jQuery("#Deposit_branch_cd").val(),
                            customer: globeCustomer,
                            invoiceNo:globeInvNo

                        };
                    }
                    if (payType == "BANK_SLIP") {
                        url = "/Payment/updateBankSlipPayment";
                        data = {
                            payMode: jQuery("#Sird_pay_tp").val(),
                            totAmount: jQuery(".tot-amount-val").html(),
                            addedAmount: jQuery("#Sird_settle_amt").val(),
                            accountNo: jQuery("#Cnk_slip_acc_no").val(),
                            accountDate: jQuery("#Bnk_slip_acc_dte").val(),
                            depositBank: jQuery("#Deposit_bank_cd").val(),
                            depositBranch: jQuery("#Deposit_branch_cd").val(),
                            invoiceNo:globeInvNo,
                            customer: globeCustomer
                        };
                    }
                    if (payType == "DEBT") {
                        url = "/Payment/updateDEBTPayment";
                        data = {
                            payMode: jQuery("#Sird_pay_tp").val(),
                            totAmount:jQuery(".tot-amount-val").html(), 
                            addedAmount:jQuery("#Sird_settle_amt").val(), 
                            cardNo:jQuery("#Debt_crd_no").val(), 
                            cardBank:jQuery("#Debt_bank_name").val(),
                            cardBankCd:jQuery("#Debt_bank_cd").val(), 
                            depositBank:jQuery("#Deposit_bank_cd").val(), 
                            depositBranch:jQuery("#Deposit_branch_cd").val(), 
                            invoiceNo: globeInvNo,
                            customer: globeCustomer
                        };
                    }
                    if (payType == "CASH") {
                        url = "/Payment/updateCashPayment";
                        data = {
                            payMode: jQuery("#Sird_pay_tp").val(),
                            totAmount: jQuery(".tot-amount-val").html(),
                            addedAmount: jQuery("#Sird_settle_amt").val(),
                            depositBank: jQuery("#Deposit_bank_cd").val(),
                            depositBranch: jQuery("#Deposit_branch_cd").val(),
                            invoiceNo: globeInvNo,
                            customer: globeCustomer
                        };
                    }
                    if (payType == "LORE") {
                        url = "/Payment/updateLOREPayment";
                        data = {
                            payMode: jQuery("#Sird_pay_tp").val(),
                            totAmount: jQuery(".tot-amount-val").html(),
                            addedAmount: jQuery("#Sird_settle_amt").val(),
                            loreCrdNo: jQuery("#Lore_crd_no").val(),
                            depositBank: jQuery("#Deposit_bank_cd").val(),
                            depositBranch: jQuery("#Deposit_branch_cd").val(),
                            customer:globeCustomer, 
                            invoiceNo:globeInvNo
                        };
                    }
                    if (payType == "CRNOTE") {
                        url = "/Payment/updateCRNOTEPayment";
                        data = {
                            payMode: jQuery("#Sird_pay_tp").val(),
                            totAmount: jQuery(".tot-amount-val").html(),
                            addedAmount: jQuery("#Sird_settle_amt").val(),
                            refNo: jQuery("#Cred_note_ref_no").val(),
                            refAmount:jQuery("#Cred_note_ref_amount").val(),
                            depositBank: jQuery("#Deposit_bank_cd").val(),
                            depositBranch: jQuery("#Deposit_branch_cd").val(),
                            customer: globeCustomer,
                            invoiceNo: globeInvNo
                        };
                    }
                    if (payType == "ADVAN") {
                        url = "/Payment/updateADVANPayment";
                        data = {
                            payMode: jQuery("#Sird_pay_tp").val(),
                            totAmount: jQuery(".tot-amount-val").html(),
                            addedAmount: jQuery("#Sird_settle_amt").val(),
                            refNo: jQuery("#Advan_ref_no").val(),
                            refAmount: jQuery("#Advan_ref_amount").val(),
                            depositBank: jQuery("#Deposit_bank_cd").val(),
                            depositBranch: jQuery("#Deposit_branch_cd").val(),
                            customer: globeCustomer,
                            invoiceNo: globeInvNo
                        };

                    }
                    if (payType == "GVO") {
                        url = "/Payment/updateGVOPayment";
                        data = {
                            payMode: jQuery("#Sird_pay_tp").val(),
                            totAmount: jQuery(".tot-amount-val").html(),
                            addedAmount: jQuery("#Sird_settle_amt").val(),
                            voucherNo: jQuery("#Gift_vouche_no").val(),
                            voucherBook: jQuery(".gift-cust-book-lbl").html(),
                            invType: invoiceType,
                            depositBank: jQuery("#Deposit_bank_cd").val(),
                            depositBranch: jQuery("#Deposit_branch_cd").val(),
                            customer: globeCustomer,
                            invoiceNo: globeInvNo
                        };

                    }
                    if (payType == "CRCD") {
                        url = "/Payment/updateCRCDPayment";
                        data = {
                            payMode: jQuery("#Sird_pay_tp").val(),
                            totAmount: jQuery(".tot-amount-val").html(),
                            addedAmount: jQuery("#Sird_settle_amt").val(),
                            crdNo:jQuery("#Cred_crd_ref_no").val(),
                            cardBank:jQuery("#Cred_crd_bank").val(),
                            crdExpDt:jQuery("#Cread-crd-expire-dt").val(), 
                            cardType:jQuery("#Cred-card-typ").val(), 
                            batch:jQuery("#Cred_crd_batch").val(), 
                            offline:jQuery("#Cred_crd_ofline").val(),
                            online: jQuery("#Cred_crd_Cred_crd_online").val(),
                            depositBank: jQuery("#Deposit_bank_cd").val(),
                            depositBranch: jQuery("#Deposit_branch_cd").val(),
                            customer: globeCustomer,
                            invoiceNo: globeInvNo
                        };

                    }
                    jQuery(".add-price-amount-btn").attr("disabled", true);
                    var runed = true;
                    jQuery.ajax({
                        type: "GET",
                        url: url,
                        data: data,
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    //addedMountVal = parseFloat(addedMountVal) + parseFloat(jQuery("#Sird_settle_amt").val());
                                    //jQuery(".tot-paid-amount-val").empty();
                                    //jQuery(".tot-paid-amount-val").html(addedMountVal);
                                    jQuery(".tot-paid-amount-val").empty();
                                    jQuery(".tot-paid-amount-val").html(result.totPaid);
                                    var tot = (jQuery(".tot-amount-val").html() != "") ? jQuery(".tot-amount-val").html() : 0;
                                    jQuery("#Sird_settle_amt").val(parseFloat(tot) - parseFloat(result.totPaid));
                                    jQuery(".bal-amount-val").empty();
                                    jQuery(".bal-amount-val").html(parseFloat(tot) - parseFloat(result.totPaid));
                                    clearAllValues();
                                    jQuery(".add-price-amount-btn").removeAttr("disabled");
                                    var depBnk = (typeof data.depositBank != "undefined") ? data.depositBank : "";
                                    var depBrn = (typeof data.depositBranch != "undefined") ? data.depositBranch : "";
                                    var crdTyp = (typeof data.cardType != "undefined") ? data.cardType : "";
                                    jQuery('table.payment-table').append('<tr class="new-row">' +
                                            '<td style="text-align:center;"><img class="delete-img remove-pay-mehod-cls" src="../Resources/images/Remove.png"></td>' +
                                            '<td>' + data.payMode + '</td>' +
                                            '<td>' + depBnk + '</td>' +
                                            '<td>' + depBrn + '</td>' +
                                            '<td>' + crdTyp + '</td>' +
                                            '<td>' + data.addedAmount + '</td>' +
                                            '</tr>');
                                    jQuery(".remove-pay-mehod-cls").unbind('click').click(function (evt) {
                                            evt.preventDefault();
                                            var td = jQuery(this).parent('td');
                                            var mode = jQuery(td).closest("tr").find('td:eq(1)').text();
                                            var amt = jQuery(td).closest("tr").find('td:eq(5)').text();
                                            jQuery.ajax({
                                                type: "GET",
                                                url: "/Payment/removePaymode",
                                                data: { payMode: mode, amount: amt },
                                                contentType: "application/json;charset=utf-8",
                                                dataType: "json",
                                                success: function (result) {
                                                    if (result.login == true) {
                                                        if (result.success == true) {
                                                            jQuery(td).parent('tr').remove();
                                                            jQuery(".tot-paid-amount-val").empty();
                                                            var totv = (jQuery(".tot-amount-val").html()) ? jQuery(".tot-amount-val").html() : 0;
                                                            jQuery(".tot-paid-amount-val").html(result.totPaid);
                                                            jQuery("#Sird_settle_amt").val(parseFloat(totv) - parseFloat(result.totPaid));
                                                            jQuery(".bal-amount-val").empty();
                                                            jQuery(".bal-amount-val").html(parseFloat(totv) - parseFloat(result.totPaid));
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
                                        });
                                } else {
                                    jQuery(".add-price-amount-btn").removeAttr("disabled");
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
                    setError("Select payment method.");
                }
            } else {
                setError("Invalid amount entered.");
            }
        } else {
            setError("Invalid amount entered.");
        }
    });
   //check numbers and decimal  only
    jQuery('#Sird_settle_amt').on("input", function (event) {
        if (!jQuery.isNumeric(this.value)) {
            this.value = "";
        }
        if (parseFloat(this.value) < 0) {
            this.value = "";
        }
       
    });
    jQuery('#Sird_settle_amt').keypress(function (event) {
        if ((event.which != 46 || $(this).val().indexOf('.') != -1) &&
          ((event.which < 48 || event.which > 57) &&
            (event.which != 0 && event.which != 8))) {
            event.preventDefault();
        }

        var text = $(this).val();

        if ((text.indexOf('.') != -1) &&
          (text.substring(text.indexOf('.')).length > 2) &&
          (event.which != 0 && event.which != 8) &&
          ($(this)[0].selectionStart >= text.length - 2)) {
            event.preventDefault();
        }
    });


    
});
//end payment adding button
/////////////////////////////////
//clear all the fields in paymethods

function clearAllValues() {
    //case
    jQuery("#Deposit_bank_cd").val("");
    jQuery("#Deposit_branch_cd").val("");
    //cheque
    jQuery("#Cheque_ref_no").val("");
    jQuery("#Cheque_dt").val("");
    jQuery("#cheque-bank").val("");
    jQuery("#Cheque_bnk_cd").val("");
    jQuery("#Cheque_branch_cd").val("");
    //credcrd
    jQuery("#Cred_crd_ref_no").val("");
    jQuery("#Cred-card-typ").val("");
    jQuery("#Cred_crd_bank").val("");
    jQuery("#Cred_crd_bank_value").val("");
    jQuery("#Cred_crd_batch").val("");
    jQuery('#Cred_crd_ofline').prop('checked', false);
    jQuery('#Cred_crd_online').prop('checked', false);

    jQuery('#Cred_crd_promotion').prop('checked', false);
    jQuery("#Cred_crd_promo-mnth-period").val("");
    jQuery(".mid-code-cls").empty();
    //lore
    jQuery("#Lore_crd_no").val("");
    jQuery(".cust-display-lbl").empty();
    jQuery(".bal-point-display-lbl").empty();
    jQuery(".loy-typ-display-lbl").empty();
    jQuery(".pnt-val-display-lbl").empty();
    //bnk-slip
    jQuery("#Cnk_slip_acc_no").val("");
    jQuery("#Bnk_slip_acc_dte").val("");
    jQuery("#Debt_crd_no").val("");
    jQuery("#Debt_bank_name").val("");
    jQuery("#Debt_bank_cd").val("");
    //advan
    jQuery("#Advan_ref_no").val("");
    jQuery("#Advan_ref_amount").val("");
    //cred note
    jQuery("#Cred_note_ref_no").val("");
    jQuery("#Cred_note_ref_amount").val("");
    //gift vou
    jQuery("#Gift_vouche_no").val("");
    jQuery(".gift-cust-cd-lbl").empty();
    jQuery(".gift-cust-name-lbl").empty();
    jQuery(".gift-cust-add-lbl").empty();
    jQuery(".gift-cust-mob-lbl").empty();
    jQuery(".gift-cust-book-lbl").empty();
    jQuery(".gift-cust-book-code-lbl").empty();
    jQuery(".gift-cust-pref-lbl").empty();
    jQuery('.voucher-table .new-row').remove();
    jQuery("#Cread-crd-expire-dt").val("");
    //jQuery(".tot-amount-val").html("0");//2016.03.12
}
//end clear
function loadPayModes(fromtype) {
    if (jQuery("#Sird_pay_tp").length > 0) {
        invoiceType=fromtype;
        jQuery.ajax({
            type: "GET",
            url: "/Payment/loadPaymenttypes",
            data: { payFormType: fromtype },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    var select = document.getElementById("Sird_pay_tp");
                    jQuery("#Sird_pay_tp").empty();
                    var options = [];
                    var option = document.createElement('option');
                    option.text = "Select Pay Mode"
                    option.value = "";
                    options.push(option.outerHTML);
                    if (result.success == true) {
                        if (result.data != null && result.data.length != 0) {
                            for (var i = 0; i < result.data.length; i++) {
                                option.text = result.data[i];
                                option.value = result.data[i];
                                options.push(option.outerHTML);
                            }
                        }
                    } else {
                        if (result.type == "Error") {
                            setError(result.msg);
                        }
                        if (result.type == "Info") {
                            setInfoMsg(result.msg);
                        }
                    }
                    select.insertAdjacentHTML('beforeEnd', options.join('\n'));
                } else {
                    Logout();
                }
            }
        });
    }
}

function loadPayModesTypes(type) {
    loadPayModes(type);
}

function clearPaymentValues() {
    clearAllValues();
}

function updateCurrencyAmount(amount, customerCode, invNo) {
    globeCustomer = customerCode;
    globeInvNo = invNo;
    jQuery("#Sird_settle_amt").val(amount);
    jQuery(".bal-amount-val").empty();
    jQuery(".bal-amount-val").append(amount);
    jQuery(".tot-amount-val").empty();
    jQuery(".tot-amount-val").append(amount);
}
//////////////////////////////////
//updateCurrencyAmount(5000, "CONT-024332", "");