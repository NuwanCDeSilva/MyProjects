jQuery(document).ready(function () {
    jQuery('#monthdate').val(my_date_formatmonth(new Date()));
    jQuery('#monthdate').datepicker({ dateFormat: "MM yy" });

    jQuery('#effectdate').val(my_date_formatmonth(new Date()));
    jQuery('#effectdate').datepicker({ dateFormat: "MM yy" });

    jQuery(".manager-search").click(function (evt) {
        var headerKeys = Array();
        headerKeys = ["Row", "Code", "Name"];
        field = "shwmanager";
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".btn-view-data").click(function (evt) {
        if (jQuery("#monthdate").val() == "") {
            setInfoMsg("Please Select Bonus Month");
            return;
        }
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/BonusCalculation/getManagerTotAdj",
            data: { BonusMonth: jQuery("#monthdate").val(), Manager: jQuery("#manager").val(), EffectDate: jQuery("#effectdate").val() },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.data.length > 0) {
                            updateArrBalAccounts(result.data);
                        }
                    } else {
                    }
                } else {
                    Logout();
                }
            }
        });

    });
    function updateArrBalAccounts(data) {
        jQuery('.adj-data-row .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {
                jQuery('.adj-data-row').append('<tr class="new-row">' +
                          '<td>' + data[i].Haa_mng_cd + '</td>' +
                          '<td>' + data[i].Haa_pc + '</td>' +
                          '<td>' + my_date_formatmonth(getFormatedDate1(data[i].Haa_date)) + '</td>' +
                       '<td style="text-align:center;"><img class="delete-img edit-arre-adj" src="../Resources/images/arrow_down.png"></td>' +
                        '</tr>');
            }
            EditDataCode();
            $('#green').smartpaginator({
                totalrecords: data.length,
                recordsperpage: 10,
                datacontainer: 'dt', dataelement: 'tr',
                initval: 0, next: 'Next', prev: 'Prev',
                first: 'First', last: 'Last', theme: 'green',
                onchange: onChange
            });

            function onChange(newPageValue) {
                LoadpageData(newPageValue);
            }
        }
    }
    function updateArrBalAccounts2(data) {
        jQuery('.adj-data-row .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {
                jQuery('.adj-data-row').append('<tr class="new-row">' +
                          '<td>' + data[i].Haa_mng_cd + '</td>' +
                            '<td>' + data[i].Haa_pc + '</td>' +
                          '<td>' + my_date_formatmonth(getFormatedDate1(data[i].Haa_date)) + '</td>' +
                       '<td style="text-align:center;"><img class="delete-img edit-arre-adj" src="../Resources/images/arrow_down.png"></td>' +
                        '</tr>');
            }
            EditDataCode();
        }
    }
    function LoadpageData(newPageValue) {
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/BonusCalculation/GetPageData",
            data: { newPageValue: newPageValue },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.data.length > 0) {
                            //show list
                            updateArrBalAccounts2(result.data);
                        }
                    } else {
                        setInfoMsg('No Arreas Limit For ' + jQuery("#location").val());
                        return;
                    }
                } else {
                    Logout();
                }
            }
        });
    }
    function getFormatedDate1(date) {
        var dte = new Date(parseInt(date.substr(6)));
        if (my_date_format_tran(dte) != "NaN/undefined/NaN")
            return my_date_format_tran(dte);
    }

    function EditDataCode() {
        jQuery(".edit-arre-adj").unbind('click').click(function (evt) {
            evt.preventDefault();
            var td = jQuery(this).parent('td');
            var tr = jQuery(td).parent('tr');
            var Manager = jQuery(tr).find('td:eq(0)').html();

            Lobibox.confirm({
                msg: "Do you want to Edit ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/BonusCalculation/EditDataCode",
                            data: { Manager: Manager },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        //Fill Forms
                                        fillfields(result.list, result.prvarr, result.prvadj, result.prsupcoladj, result.prgrccolladj, result.prtotclbal, result.prtotnumact, result.prmsupcoll, result.dsprefu, result.dates);

                                    } else {
                                        setInfoMsg(result.msg);
                                        return;
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
    var Company="";
    var Prifitcenter="";
    var Dateee = my_date_formatmonth(new Date());
    var Years = 0;
    var Manager = "";
    var gepfrt = 0;
    var getfrt = 0;
    function fillfields(data, prevarr, prevadj, prsupcoladj, prgrccolladj, prtotclbal, prtotnumact, prmsuppcoll, dsprefu,dates) {
        
        Company = data[0].Haa_com;
        Profitcenter = data[0].Haa_pc;
        Dateee = jQuery("#monthdate").val();
        Years = data[0].Years;
        Manager = data[0].Haa_mng_cd;
        jQuery("#activeaccounts").val(data[0].Haa_tot_no_of_act_acc);
        jQuery("#periodinop").val( dates);
        jQuery("#handover").val(data[0].HAA_HAND_OVER);
        jQuery("#spcremark").val(data[0].HAA_SPC_RMK);
        jQuery("#pc").val(data[0].Haa_pc);
        jQuery("#manager").val(data[0].Haa_mng_cd);
        jQuery("#category").val(data[0].Haa_pc_cat);
        jQuery("#category2").val(data[0].Haa_pc_cat);
        jQuery("#currmonthdue").val(data[0].HAA_CURR_DUE_TOT - data[0].HAA_HAND_OVER);
        jQuery("#currmonthdueadj").val(data[0].HAA_ADJ_DUE_TOT);
        jQuery("#prevarre").val(prevarr - data[0].HAA_HAND_OVER);
        jQuery("#premontharreadj").val(prevadj);
        jQuery("#currmonarr").val(data[0].Haa_act_arr_amt - data[0].HAA_HAND_OVER);
        jQuery("#currmonarradj").val(data[0].HAA_ADJ_CURR_ARR);
        jQuery("#gracepersett").val(data[0].HAA_GRCE_SETT);
        jQuery("#gracepersettadj").val(data[0].HAA_ADJ_GRA_PER_SETT);
        jQuery("#shortremitt").val(data[0].HAA_SHORT_REMITT);
        jQuery("#arrerelschadj").val(data[0].HAA_ARR_RELE_MONTHS);
        jQuery("#shopcomadj").val(data[0].HAA_SHOP_COM_ADJ);
        jQuery("#diriyaadj").val(data[0].HAA_DIRIYA_ADJ);
        jQuery("#lodadj").val(data[0].HAA_LOD_ADJ);
        jQuery("#serprobaccadj").val(data[0].HAA_SER_PROB);
        jQuery("#disputecorre").val(data[0].HAA_DISP_ADJ);
        jQuery("#issuechqret").val(data[0].HAA_ISSUE_CHQ_RTN_ADJ);
        jQuery("#totremitted").val(data[0].HAA_TOT_REMITT - data[0].HAA_HAND_OVER);
        jQuery("#suppcollec").val(data[0].HAA_SUPP_COLL - data[0].HAA_HAND_OVER);
        jQuery("#suppcollecadj").val(data[0].HAA_SUPP_COLL_ADJ);
        jQuery("#grpercoll").val(data[0].HAA_GRCE_PER_COLL);
        jQuery("#grpercolladj").val(data[0].HAA_GRC_PRD_COL_ADJ);
        jQuery("#premongrcoll").val(prgrccolladj);
        jQuery("#premongrcolladj").val(Number(data[0].HAA_ADJ_PRE_GR_PER_COLL.toFixed(2)));
        jQuery("#premonsupcoll").val(Number(prsupcoladj).toFixed(2));
        jQuery("#premonsupcolladj").val(Number(data[0].HAA_ADJ_PR_MO_SUP_COLL).toFixed(2));
        jQuery("#totremadj").val(data[0].HAA_ADJ_REMITT);
        // jQuery("#netremittance").val((Number(jQuery("#totremitted").val()) - Number(jQuery("#totremadj").val()) + Number(jQuery("#suppcollec").val())));
        jQuery("#netremittance").val((Number(jQuery("#totremitted").val()) + Number(jQuery("#totremadj").val()) + Number(jQuery("#suppcollec").val())
+ Number(jQuery("#suppcollecadj").val()) + Number(jQuery("#grpercoll").val()) + Number(jQuery("#grpercolladj").val()) - Number(jQuery("#premongrcoll").val())
+ Number(jQuery("#premongrcolladj").val()) - Number(jQuery("#premonsupcoll").val()) + Number(jQuery("#premonsupcolladj").val())).toFixed(2));

        jQuery("#totrecbal").val(data[0].Haa_tot_clos_bal);
        
        jQuery("#disregartamt").val(Number( data[0].HAA_DISREG_AMT).toFixed(2));
        jQuery("#grapernotqlyamt").val(Number(data[0].HAA_GRC_PER_NT_QL).toFixed(2));
        var duetotal = data[0].HAA_CURR_DUE_TOT + prevarr - data[0].HAA_ADJ_DUE_TOT;
        jQuery("#duetotal").val(duetotal.toFixed(2));
        jQuery("#totaladj").val( (data[0].HAA_SHORT_REMITT 
            + data[0].HAA_ARR_RELE_MONTHS + data[0].HAA_SHOP_COM_ADJ + data[0].HAA_DIRIYA_ADJ + data[0].HAA_LOD_ADJ+
           data[0].HAA_SER_PROB + data[0].HAA_DISP_ADJ + data[0].HAA_ISSUE_CHQ_RTN_ADJ + data[0].HAA_OTH
            + data[0].HAA_GRCE_SETT).toFixed(2));



        jQuery("#netarramt").val((data[0].Haa_act_arr_amt - data[0].HAA_HAND_OVER + data[0].HAA_ADJ_GRA_PER_SETT - Number(jQuery("#totaladj").val())).toFixed(2));

        if (data[0].HAA_GRCE_SETT > 0) {
            if (data[0].HAA_GRCE_SETT.toFixed(2) >= data[0].HAA_GRC_PER_NT_QL.toFixed(2)) {
                jQuery("#netarramt").val(((data[0].Haa_act_arr_amt - data[0].HAA_HAND_OVER  + data[0].HAA_ADJ_GRA_PER_SETT - Number(jQuery("#totaladj").val()).toFixed(2) + data[0].HAA_GRC_PER_NT_QL)).toFixed(2));
            } else {
                jQuery("#netarramt").val(((data[0].Haa_act_arr_amt - data[0].HAA_HAND_OVER + data[0].HAA_ADJ_GRA_PER_SETT - Number(jQuery("#totaladj").val()).toFixed(2) + data[0].HAA_GRCE_SETT)).toFixed(2));
            }
        }

        if (Number(jQuery("#netarramt").val() <= data[0].HAA_DISREG_AMT))
        {
            jQuery("#totaladj").val((Number(jQuery("#totaladj").val()) + Number(jQuery("#netarramt").val())).toFixed(2));
            jQuery("#netarramt").val(0);
            
        } else {
            jQuery("#netarramt").val((Number(jQuery("#netarramt").val()).toFixed(2) - data[0].HAA_DISREG_AMT).toFixed(2));
            jQuery("#totaladj").val((Number(jQuery("#totaladj").val()) + data[0].HAA_DISREG_AMT).toFixed(2));
        }

        jQuery("#arrper").val((Number(jQuery("#netarramt").val()) * 100 / Number(jQuery("#duetotal").val())).toFixed(2));

        ///
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/BonusCalculation/GetBonusCorrectData",
            data: { arreper: Number(jQuery("#arrper").val()), remitance: Number(jQuery("#netremittance").val()) },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        jQuery("#bonuspers").val(result.bonusrate);
                        jQuery("#targetnoofaccounts").val(result.targetacc.toFixed(2));
                        jQuery("#bonusamt").val(result.bonusamt.toFixed(2));
                        jQuery("#periodinop").val(result.dates);
                        var grosbonus = Number(jQuery("#bonusamt").val());

                        jQuery("#totalgrosscollbns").val(grosbonus.toFixed(2));
                        jQuery("#accdeptrefund").val(data[0].HAA_ACC_DEPT_REFUND);
                        jQuery("#accdeptrefuremark").val(data[0].HAA_ACC_DEPT_REFUREMK);
                        jQuery("#accdeptdeduct").val(data[0].HAA_ACC_DEPT_DEDUC);
                        jQuery("#accdeptdeducremark").val(data[0].HAA_ACC_DEPT_DEDUCRMK);

                        jQuery("#invdeptrefund").val(data[0].HAA_INV_DEPT_REFUND);
                        jQuery("#invdeptrefuremark").val(data[0].HAA_INV_DEPT_REFUREMK);
                        jQuery("#invdeptdeduct").val(data[0].HAA_INV_DEPT_DEDUC);
                        jQuery("#invdeptdeducremark").val(data[0].HAA_INV_DEPT_DEDUCRMK);

                        jQuery("#creddeptrefund").val(data[0].HAA_CRED_DEPT_REFUND);
                        jQuery("#creddeptrefuremark").val(data[0].HAA_CRED_DEPT_REFUREMK);
                        jQuery("#creddeptdeduct").val(data[0].HAA_CRED_DEPT_DEDUC);
                        jQuery("#creddeptdeducremark").val(data[0].HAA_CRED_DEPT_DEDUCRMK);

                        //totalgrosscollbns


                        var EPF = data[0].HAA_EPF_RT * grosbonus / 100;
                        var ESP = data[0].HAA_ESD_RT * grosbonus / 100;
                        jQuery("#epfcontr").val(EPF.toFixed(2));
                        jQuery("#esdcontri").val(ESP.toFixed(2));

                        gepfrt = data[0].HAA_EPF_RT;
                        getfrt = data[0].HAA_ESD_RT;
                        //arearscalc
                        jQuery("#othadj").val(data[0].HAA_SPC_VAL);
                        jQuery("#dispbnsrefded").val(Number(data[0].HAA_BONUS_REF_DED).toFixed(2));

                        jQuery("#avgperacc").val((Number(data[0].HAA_TAG_PER.toFixed(2)) / Number(result.targetacc)).toFixed(2));

                        jQuery("#adj").val(data[0].HAA_OTH);

                        var gross = Number(jQuery("#totalgrosscollbns").val());
                        var dispute = Number(jQuery("#dispbnsrefded").val());
                        var accref = Number(jQuery("#accdeptrefund").val());
                        var invref = Number(jQuery("#invdeptrefund").val());
                        var credref = Number(jQuery("#creddeptrefund").val());
                        var accded = Number(jQuery("#accdeptdeduct").val());
                        var invded = Number(jQuery("#invdeptdeduct").val());
                        var credded = Number(jQuery("#creddeptdeduct").val());
                        var epf = Number(jQuery("#epfcontr").val());
                        var esd = Number(jQuery("#esdcontri").val());
                        var other = Number(jQuery("#othadj").val());
                    
                        var netbonus = NetBonus(gross, dispute, accref, invref, credref, accded, invded, credded, epf, esd, other)
                        jQuery("#totalnetbns").val(netbonus);


                      

                        // jQuery("#dispbnsrefded").val(dsprefu); HAA_BONUS_REF_DED
                        //jQuery("#othadj").val(data[0].HAA_OTH);
                      
                       
                        //Total Remmited othadj

                        //supplymentary commission
                        fixnumbers();
                    } else {
                    }
                } else {
                    Logout();
                }
            }
        });


        
        
    }
    function StartNumbers() {

        jQuery("#currmonthdue").val(CommasTOnumberWith(jQuery("#currmonthdue").val()));
        jQuery("#currmonthdueadj").val(CommasTOnumberWith(jQuery("#currmonthdueadj").val()));
        jQuery("#prevarre").val(CommasTOnumberWith(jQuery("#prevarre").val()));
        jQuery("#premontharreadj").val(CommasTOnumberWith(jQuery("#premontharreadj").val()));
        jQuery("#currmonarr").val(CommasTOnumberWith(jQuery("#currmonarr").val()));
        jQuery("#currmonarradj").val(CommasTOnumberWith(jQuery("#currmonarradj").val()));
        jQuery("#gracepersett").val(CommasTOnumberWith(jQuery("#gracepersett").val()));
        jQuery("#gracepersettadj").val(CommasTOnumberWith(jQuery("#gracepersettadj").val()));
        jQuery("#shortremitt").val(CommasTOnumberWith(jQuery("#shortremitt").val()));
        jQuery("#arrerelschadj").val(CommasTOnumberWith(jQuery("#arrerelschadj").val()));
        jQuery("#shopcomadj").val(CommasTOnumberWith(jQuery("#shopcomadj").val()));
        jQuery("#diriyaadj").val(CommasTOnumberWith(jQuery("#diriyaadj").val()));
        jQuery("#lodadj").val(CommasTOnumberWith(jQuery("#lodadj").val()));
        jQuery("#serprobaccadj").val(CommasTOnumberWith(jQuery("#serprobaccadj").val()));
        jQuery("#disputecorre").val(CommasTOnumberWith(jQuery("#disputecorre").val()));
        jQuery("#issuechqret").val(CommasTOnumberWith(jQuery("#issuechqret").val()));
        jQuery("#totremitted").val(CommasTOnumberWith(jQuery("#totremitted").val()));
        jQuery("#serprobaccadj").val(CommasTOnumberWith(jQuery("#serprobaccadj").val()));
        jQuery("#suppcollec").val(CommasTOnumberWith(jQuery("#suppcollec").val()));
        jQuery("#suppcollecadj").val(CommasTOnumberWith(jQuery("#suppcollecadj").val()));
        jQuery("#grpercoll").val(CommasTOnumberWith(jQuery("#grpercoll").val()));
        jQuery("#grpercolladj").val(CommasTOnumberWith(jQuery("#grpercolladj").val()));
        jQuery("#premongrcoll").val(CommasTOnumberWith(jQuery("#premongrcoll").val()));
        jQuery("#premongrcolladj").val(CommasTOnumberWith(jQuery("#premongrcolladj").val()));
        jQuery("#premonsupcoll").val(CommasTOnumberWith(jQuery("#premonsupcoll").val()));
        jQuery("#premonsupcolladj").val(CommasTOnumberWith(jQuery("#premonsupcolladj").val()));
        jQuery("#netremittance").val(CommasTOnumberWith(jQuery("#netremittance").val()));
        jQuery("#totrecbal").val(CommasTOnumberWith(jQuery("#totrecbal").val()));
        jQuery("#totremadj").val(CommasTOnumberWith(jQuery("#totremadj").val()));
        jQuery("#disregartamt").val(CommasTOnumberWith(jQuery("#disregartamt").val()));
        jQuery("#grapernotqlyamt").val(CommasTOnumberWith(jQuery("#grapernotqlyamt").val()));
        jQuery("#totaladj").val(CommasTOnumberWith(jQuery("#totaladj").val()));
        jQuery("#netarramt").val(CommasTOnumberWith(jQuery("#netarramt").val()));
        jQuery("#arrper").val(CommasTOnumberWith(jQuery("#arrper").val()));
        jQuery("#bonuspers").val(CommasTOnumberWith(jQuery("#bonuspers").val()));
        jQuery("#bonuspers").val(CommasTOnumberWith(jQuery("#bonuspers").val()));
        jQuery("#bonusamt").val(CommasTOnumberWith(jQuery("#bonusamt").val()));

        jQuery("#accdeptrefund").val(CommasTOnumberWith(jQuery("#accdeptrefund").val()));
        jQuery("#accdeptdeduct").val(CommasTOnumberWith(jQuery("#accdeptdeduct").val()));

        jQuery("#invdeptrefund").val(CommasTOnumberWith(jQuery("#invdeptrefund").val()));
        jQuery("#invdeptdeduct").val(CommasTOnumberWith(jQuery("#invdeptdeduct").val()));

        jQuery("#creddeptrefund").val(CommasTOnumberWith(jQuery("#creddeptrefund").val()));
        jQuery("#creddeptdeduct").val(CommasTOnumberWith(jQuery("#creddeptdeduct").val()));

        jQuery("#totalgrosscollbns").val(CommasTOnumberWith(jQuery("#totalgrosscollbns").val()));
        jQuery("#epfcontr").val(CommasTOnumberWith(jQuery("#epfcontr").val()));
        jQuery("#esdcontri").val(CommasTOnumberWith(jQuery("#esdcontri").val()));
        jQuery("#duetotal").val(CommasTOnumberWith(jQuery("#duetotal").val()));

        jQuery("#targetnoofaccounts").val(CommasTOnumberWith(jQuery("#targetnoofaccounts").val()));
        jQuery("#avgperacc").val(CommasTOnumberWith(jQuery("#avgperacc").val()));
        jQuery("#othadj").val(CommasTOnumberWith(jQuery("#othadj").val()));
        jQuery("#dispbnsrefded").val(CommasTOnumberWith(jQuery("#dispbnsrefded").val()));


    }
    function fixnumbers() {
        jQuery("#currmonthdue").val(numberWithCommas(jQuery("#currmonthdue").val()));
        jQuery("#currmonthdueadj").val(numberWithCommas(jQuery("#currmonthdueadj").val()));
        jQuery("#prevarre").val(numberWithCommas(jQuery("#prevarre").val()));
        jQuery("#premontharreadj").val(numberWithCommas(jQuery("#premontharreadj").val()));
        jQuery("#currmonarr").val(numberWithCommas(jQuery("#currmonarr").val()));
        jQuery("#currmonarradj").val(numberWithCommas(jQuery("#currmonarradj").val()));
        jQuery("#gracepersett").val(numberWithCommas(jQuery("#gracepersett").val()));
        jQuery("#gracepersettadj").val(numberWithCommas(jQuery("#gracepersettadj").val()));
        jQuery("#shortremitt").val(numberWithCommas(jQuery("#shortremitt").val()));
        jQuery("#arrerelschadj").val(numberWithCommas(jQuery("#arrerelschadj").val()));
        jQuery("#shopcomadj").val(numberWithCommas(jQuery("#shopcomadj").val()));
        jQuery("#diriyaadj").val(numberWithCommas(jQuery("#diriyaadj").val()));
        jQuery("#lodadj").val(numberWithCommas(jQuery("#lodadj").val()));
        jQuery("#serprobaccadj").val(numberWithCommas(jQuery("#serprobaccadj").val()));
        jQuery("#disputecorre").val(numberWithCommas(jQuery("#disputecorre").val()));
        jQuery("#issuechqret").val(numberWithCommas(jQuery("#issuechqret").val()));
        jQuery("#totremitted").val(numberWithCommas(jQuery("#totremitted").val()));
        jQuery("#serprobaccadj").val(numberWithCommas(jQuery("#serprobaccadj").val()));
        jQuery("#suppcollec").val(numberWithCommas(jQuery("#suppcollec").val()));
        jQuery("#suppcollecadj").val(numberWithCommas(jQuery("#suppcollecadj").val()));
        jQuery("#grpercoll").val(numberWithCommas(jQuery("#grpercoll").val()));
        jQuery("#grpercolladj").val(numberWithCommas(jQuery("#grpercolladj").val()));
        jQuery("#premongrcoll").val(numberWithCommas(jQuery("#premongrcoll").val()));
        jQuery("#premongrcolladj").val(numberWithCommas(jQuery("#premongrcolladj").val()));
        jQuery("#premonsupcoll").val(numberWithCommas(jQuery("#premonsupcoll").val()));
        jQuery("#premonsupcolladj").val(numberWithCommas(jQuery("#premonsupcolladj").val()));
        jQuery("#netremittance").val(numberWithCommas(jQuery("#netremittance").val()));
        jQuery("#totrecbal").val(numberWithCommas(jQuery("#totrecbal").val()));
        jQuery("#totremadj").val(numberWithCommas(jQuery("#totremadj").val()));
        jQuery("#disregartamt").val(numberWithCommas(jQuery("#disregartamt").val()));
        jQuery("#grapernotqlyamt").val(numberWithCommas(jQuery("#grapernotqlyamt").val()));
        jQuery("#totaladj").val(numberWithCommas(jQuery("#totaladj").val()));
        jQuery("#netarramt").val(numberWithCommas(jQuery("#netarramt").val()));
        jQuery("#arrper").val(numberWithCommas(jQuery("#arrper").val()));
        jQuery("#bonuspers").val(numberWithCommas(jQuery("#bonuspers").val()));
        jQuery("#bonuspers").val(numberWithCommas(jQuery("#bonuspers").val()));
        jQuery("#bonusamt").val(numberWithCommas(jQuery("#bonusamt").val()));

        jQuery("#accdeptrefund").val(numberWithCommas(jQuery("#accdeptrefund").val()));
        jQuery("#accdeptdeduct").val(numberWithCommas(jQuery("#accdeptdeduct").val()));

        jQuery("#invdeptrefund").val(numberWithCommas(jQuery("#invdeptrefund").val()));
        jQuery("#invdeptdeduct").val(numberWithCommas(jQuery("#invdeptdeduct").val()));

        jQuery("#creddeptrefund").val(numberWithCommas(jQuery("#creddeptrefund").val()));
        jQuery("#creddeptdeduct").val(numberWithCommas(jQuery("#creddeptdeduct").val()));

        jQuery("#totalgrosscollbns").val(numberWithCommas(jQuery("#totalgrosscollbns").val()));
        jQuery("#epfcontr").val(numberWithCommas(jQuery("#epfcontr").val()));
        jQuery("#esdcontri").val(numberWithCommas(jQuery("#esdcontri").val()));
        jQuery("#duetotal").val(numberWithCommas(jQuery("#duetotal").val()));

        jQuery("#targetnoofaccounts").val(numberWithCommas(jQuery("#targetnoofaccounts").val()));
        jQuery("#avgperacc").val(numberWithCommas(jQuery("#avgperacc").val()));
        jQuery("#othadj").val(numberWithCommas(jQuery("#othadj").val()));
        jQuery("#dispbnsrefded").val(numberWithCommas(jQuery("#dispbnsrefded").val())); 
        jQuery("#totalnetbns").val(numberWithCommas(jQuery("#totalnetbns").val()));
    }
    function getFormatedDate1(date) {
        var dte = new Date(parseInt(date.substr(6)));
        if (my_date_format_tran(dte) != "NaN/undefined/NaN")
            return my_date_format_tran(dte);
    }
    jQuery(".btn-save-data").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to save ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    StartNumbers();
                    var grossbonus = jQuery("#totalgrosscollbns").val();
                    var epfval = jQuery("#epfcontr").val();
                    var esd = jQuery("#esdcontri").val();
                    var totalnetbonus = jQuery("#totalnetbns").val();
                    var adjesments = jQuery("#adj").val();
                    var deduc = jQuery("#dispbnsrefded").val();
                    var Accrefund = jQuery("#accdeptrefund").val();
                    var Accrefrmk = jQuery("#accdeptrefuremark").val();
                    var Accdedu = jQuery("#accdeptdeduct").val();
                    var Accdedurmk = jQuery("#accdeptdeducremark").val();
                    var Invrefund = jQuery("#invdeptrefund").val();
                    var Invrefrmk = jQuery("#invdeptrefuremark").val();
                    var Invdedu = jQuery("#invdeptdeduct").val();
                    var Invdedurmk = jQuery("#invdeptdeducremark").val();
                    var Credrefund = jQuery("#creddeptrefund").val();
                    var Credrefrmk = jQuery("#creddeptrefuremark").val();
                    var Creddedu = jQuery("#creddeptdeduct").val();
                    var Creddedurmk = jQuery("#creddeptdeducremark").val();
                    var effectdate = jQuery("#effectdate").val();
                    var spcremk = jQuery("#spcremark").val();
                    var spcval = jQuery("#othadj").val();

                    if (grossbonus == "" || epfval == "" || esd == "" || totalnetbonus == "" || adjesments == "" || deduc=="") {
                        setInfoMsg("Cannot Empty Fields");
                        return;
                    }

                    if (Number(Accdedu) > 0 && Accdedurmk == "")
                    {
                        setInfoMsg("Please Enter Remark For Accounts Dept Deductions");
                        return;
                    }
                    if (Number(Invdedu) > 0 && Invdedurmk == "") {
                        setInfoMsg("Please Enter Remark For Inventory Dept Deductions");
                        return;
                    }
                    if (Number(Creddedu) > 0 && Creddedurmk == "") {
                        setInfoMsg("Please Enter Remark For Credit Dept Deductions");
                        return;
                    }
                    if (Number(Accrefund) > 0 && Accrefrmk == "") {
                        setInfoMsg("Please Enter Remark For Accounts Dept Refunds");
                        return;
                    }
                    if (Number(Invrefund) > 0 && Invrefrmk == "") {
                        setInfoMsg("Please Enter Remark For Inventory Dept Refunds");
                        return;
                    }
                    if (Number(Credrefund) > 0 && Credrefrmk == "") {
                        setInfoMsg("Please Enter Remark For Credit Dept Refunds");
                        return;
                    }
                    if (Number(spcval) > 0 && spcremk == "") {
                        setInfoMsg("Please Enter Remark For Special Refund/Deduction");
                        return;
                    }
                    jQuery.ajax({
                        cache: false,
                        type: "GET",
                        url: "/BonusCalculation/SaveAdjDetails",
                        data: {
                            grossbonus: grossbonus, epfval: epfval, esd: esd, totalnetbonus: totalnetbonus, adjesments: adjesments, deduc: deduc,
                            Accrefund: Accrefund, Accrefrmk: Accrefrmk, Accdedu: Accdedu, Accdedurmk: Accdedurmk,
                            Invrefund: Invrefund, Invrefrmk: Invrefrmk, Invdedu: Invdedu, Invdedurmk: Invdedurmk,
                            Credrefund: Credrefund, Credrefrmk: Credrefrmk, Creddedu: Creddedu, Creddedurmk: Creddedurmk, effectdate: effectdate, spcremk: spcremk,
                            spcval: spcval
                        },
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    setSuccesssMsg(result.msg);
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
    jQuery(".btn-finl-data").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to finalyz ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    var grossbonus = jQuery("#totalgrosscollbns").val();
                    var ded_ref = jQuery("#dispbnsrefded").val();
                    var netbonus = jQuery("#totalnetbns").val();
                    var totalnetbonus = jQuery("#totalnetbns").val();
                    if (grossbonus == "" || ded_ref == "" || netbonus == "" || totalnetbonus == "") {
                        setInfoMsg("Cannot Empty Fields");
                        return;
                    }

                    jQuery.ajax({
                        cache: false,
                        type: "GET",
                        url: "/BonusCalculation/Finalize",
                        data: {
                            grossbonus: grossbonus, ded_ref: ded_ref, netbonus: netbonus, totalnetbonus: totalnetbonus
                        },
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    setSuccesssMsg(result.msg);
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
    function clearAll()
    {
        jQuery("#category").val("");
        jQuery("#currmonthdue").val("");
        jQuery("#currmonthdueadj").val("");
        jQuery("#prevarre").val("");
        jQuery("#premontharreadj").val("");
        jQuery("#currmonarr").val("");
        jQuery("#currmonarradj").val("");
        jQuery("#gracepersett").val("");
        jQuery("#gracepersettadj").val("");
        jQuery("#shortremitt").val("");
        jQuery("#arrerelschadj").val("");
        jQuery("#shopcomadj").val("");
        jQuery("#diriyaadj").val("");
        jQuery("#lodadj").val("");
        jQuery("#serprobaccadj").val("");
        jQuery("#disputecorre").val("");
        jQuery("#issuechqret").val("");
        jQuery("#totremitted").val("");
        jQuery("#serprobaccadj").val("");
        jQuery("#suppcollec").val("");
        jQuery("#suppcollecadj").val("");
        jQuery("#grpercoll").val("");
        jQuery("#grpercolladj").val("");
        jQuery("#premongrcoll").val("");
        jQuery("#premongrcolladj").val("");
        jQuery("#premonsupcoll").val("");
        jQuery("#premonsupcolladj").val("");
        jQuery("#netremittance").val("");
        jQuery("#totrecbal").val("");
        jQuery("#totremadj").val("");
        jQuery("#disregartamt").val("");
        jQuery("#disregartamt").val("");
        jQuery("#grapernotqlyamt").val("");
        jQuery("#totaladj").val("");
        jQuery("#netarramt").val("");
        jQuery("#arrper").val("");
        jQuery("#bonuspers").val("");
        jQuery("#bonuspers").val("");
        jQuery("#bonusamt").val("");
        jQuery("#accdeptrefund").val("");
        jQuery("#accdeptdeduct").val("");
        jQuery("#invdeptrefund").val("");
        jQuery("#invdeptdeduct").val("");
        jQuery("#creddeptrefund").val("");
        jQuery("#creddeptdeduct").val("");
        jQuery("#totalgrosscollbns").val("");
        jQuery("#epfcontr").val("");
        jQuery("#esdcontri").val("");
        jQuery("#duetotal").val("");
        jQuery("#targetnoofaccounts").val("");
        jQuery("#avgperacc").val("");
        jQuery("#othadj").val("");
        jQuery("#dispbnsrefded").val("");
        jQuery("#totalnetbns").val("");
        jQuery("#accdeptrefuremark").val("");
        jQuery("#accdeptdeducremark").val("");
        jQuery("#invdeptrefuremark").val("");
        jQuery("#invdeptdeducremark").val("");
        jQuery("#creddeptrefuremark").val("");
        jQuery("#creddeptdeducremark").val("");
        jQuery("#adj").val("");
        jQuery("#handover").val("");
        jQuery("#activeaccounts").val("");
        jQuery("#periodinop").val("");
        jQuery("#category2").val("");
        jQuery("#spcremark").val("");
    }
    $('input#dispbnsrefded').blur(function () {
        StartNumbers();
        var num = parseFloat($(this).val());
        var cleanNum = num.toFixed(2);
        if (cleanNum == "NaN") {
            cleanNum = 0;
        }
        $(this).val(cleanNum);
        fixnumbers();
    });
    jQuery("#dispbnsrefded").on("input", function () {
        StartNumbers();
        
        var bonusamt = Number(jQuery("#bonusamt").val());
        var dispute = Number(jQuery("#dispbnsrefded").val());

        var realgross = bonusamt + dispute;
        jQuery("#totalgrosscollbns").val(realgross);
        var gross = Number(jQuery("#totalgrosscollbns").val());
      
        var accref = Number(jQuery("#accdeptrefund").val());
        var invref = Number(jQuery("#invdeptrefund").val());
        var credref = Number(jQuery("#creddeptrefund").val());
        var accded = Number(jQuery("#accdeptdeduct").val());
        var invded = Number(jQuery("#invdeptdeduct").val());
        var credded = Number(jQuery("#creddeptdeduct").val());
        var epf = Number(jQuery("#epfcontr").val());
        var esd = Number(jQuery("#esdcontri").val());
        var other = Number(jQuery("#othadj").val());

        var netbonus = NetBonus(gross, dispute, accref, invref, credref, accded, invded, credded, epf, esd, other)
        jQuery("#totalnetbns").val(netbonus);

        var EPF = gepfrt * gross / 100;
        var ESP = getfrt * gross / 100;
        jQuery("#epfcontr").val(EPF.toFixed(2));
        jQuery("#esdcontri").val(ESP.toFixed(2));

    });

    $('input#accdeptdeduct').blur(function () {
        StartNumbers();
        var num = parseFloat($(this).val());
        var cleanNum = num.toFixed(2);
        if (cleanNum == "NaN") {
            cleanNum = 0;
        }
        if (cleanNum < 0) {
            cleanNum = 0;
        }
        $(this).val(cleanNum);
        fixnumbers();
    });
    jQuery("#accdeptdeduct").on("input", function () {
        StartNumbers();

        var gross = Number(jQuery("#totalgrosscollbns").val());
        var dispute = Number(jQuery("#dispbnsrefded").val());
        var accref = Number(jQuery("#accdeptrefund").val());
        var invref = Number(jQuery("#invdeptrefund").val());
        var credref = Number(jQuery("#creddeptrefund").val());
        var accded = Number(jQuery("#accdeptdeduct").val());
        var invded = Number(jQuery("#invdeptdeduct").val());
        var credded = Number(jQuery("#creddeptdeduct").val());
        var epf = Number(jQuery("#epfcontr").val());
        var esd = Number(jQuery("#esdcontri").val());
        var other = Number(jQuery("#othadj").val());

        var netbonus = NetBonus(gross, dispute, accref, invref, credref, accded, invded, credded, epf, esd, other)
        jQuery("#totalnetbns").val(netbonus);
    });
    $('input#invdeptdeduct').blur(function () {
        StartNumbers();
        var num = parseFloat($(this).val());
        var cleanNum = num.toFixed(2);
        if (cleanNum == "NaN") {
            cleanNum = 0;
        }
        if (cleanNum < 0) {
            cleanNum = 0;
        }
        $(this).val(cleanNum);
        fixnumbers();
    });
    jQuery("#invdeptdeduct").on("input", function () {
        StartNumbers();

        var gross = Number(jQuery("#totalgrosscollbns").val());
        var dispute = Number(jQuery("#dispbnsrefded").val());
        var accref = Number(jQuery("#accdeptrefund").val());
        var invref = Number(jQuery("#invdeptrefund").val());
        var credref = Number(jQuery("#creddeptrefund").val());
        var accded = Number(jQuery("#accdeptdeduct").val());
        var invded = Number(jQuery("#invdeptdeduct").val());
        var credded = Number(jQuery("#creddeptdeduct").val());
        var epf = Number(jQuery("#epfcontr").val());
        var esd = Number(jQuery("#esdcontri").val());
        var other = Number(jQuery("#othadj").val());

        var netbonus = NetBonus(gross, dispute, accref, invref, credref, accded, invded, credded, epf, esd, other)
        jQuery("#totalnetbns").val(netbonus);
    });
    $('input#creddeptdeduct').blur(function () {
        StartNumbers();
        var num = parseFloat($(this).val());
        var cleanNum = num.toFixed(2);
        if (cleanNum == "NaN") {
            cleanNum = 0;
        }
        if (cleanNum < 0) {
            cleanNum = 0;
        }
        $(this).val(cleanNum);
        fixnumbers();
    });
    jQuery("#creddeptdeduct").on("input", function () {
        StartNumbers();

        var gross = Number(jQuery("#totalgrosscollbns").val());
        var dispute = Number(jQuery("#dispbnsrefded").val());
        var accref = Number(jQuery("#accdeptrefund").val());
        var invref = Number(jQuery("#invdeptrefund").val());
        var credref = Number(jQuery("#creddeptrefund").val());
        var accded = Number(jQuery("#accdeptdeduct").val());
        var invded = Number(jQuery("#invdeptdeduct").val());
        var credded = Number(jQuery("#creddeptdeduct").val());
        var epf = Number(jQuery("#epfcontr").val());
        var esd = Number(jQuery("#esdcontri").val());
        var other = Number(jQuery("#othadj").val());

        var netbonus = NetBonus(gross, dispute, accref, invref, credref, accded, invded, credded, epf, esd, other)
        jQuery("#totalnetbns").val(netbonus);
    });
    $('input#accdeptrefund').blur(function () {
        StartNumbers();
        var num = parseFloat($(this).val());
        var cleanNum = num.toFixed(2);
        if (cleanNum == "NaN") {
            cleanNum = 0;
        }
        if (cleanNum < 0) {
            cleanNum = 0;
        }
        $(this).val(cleanNum);
        fixnumbers();
    });
    jQuery("#accdeptrefund").on("input", function () {
        StartNumbers();
        var gross = Number(jQuery("#totalgrosscollbns").val());
        var dispute = Number(jQuery("#dispbnsrefded").val());
        var accref = Number(jQuery("#accdeptrefund").val());
        var invref = Number(jQuery("#invdeptrefund").val());
        var credref = Number(jQuery("#creddeptrefund").val());
        var accded = Number(jQuery("#accdeptdeduct").val());
        var invded = Number(jQuery("#invdeptdeduct").val());
        var credded = Number(jQuery("#creddeptdeduct").val());
        var epf = Number(jQuery("#epfcontr").val());
        var esd = Number(jQuery("#esdcontri").val());
        var other = Number(jQuery("#othadj").val());

        var netbonus = NetBonus(gross, dispute, accref, invref, credref, accded, invded, credded, epf, esd, other)
        jQuery("#totalnetbns").val(netbonus);

    });
    $('input#accdeptrefund').blur(function () {
        StartNumbers();
        var num = parseFloat($(this).val());
        var cleanNum = num.toFixed(2);
        if (cleanNum == "NaN") {
            cleanNum = 0;
        }
        if (cleanNum < 0) {
            cleanNum = 0;
        }
        $(this).val(cleanNum);
        fixnumbers();
    });
    jQuery("#invdeptrefund").on("input", function () {
        StartNumbers();
        var gross = Number(jQuery("#totalgrosscollbns").val());
        var dispute = Number(jQuery("#dispbnsrefded").val());
        var accref = Number(jQuery("#accdeptrefund").val());
        var invref = Number(jQuery("#invdeptrefund").val());
        var credref = Number(jQuery("#creddeptrefund").val());
        var accded = Number(jQuery("#accdeptdeduct").val());
        var invded = Number(jQuery("#invdeptdeduct").val());
        var credded = Number(jQuery("#creddeptdeduct").val());
        var epf = Number(jQuery("#epfcontr").val());
        var esd = Number(jQuery("#esdcontri").val());
        var other = Number(jQuery("#othadj").val());

        var netbonus = NetBonus(gross, dispute, accref, invref, credref, accded, invded, credded, epf, esd, other)
        jQuery("#totalnetbns").val(netbonus);

    });
    $('input#creddeptrefund').blur(function () {
        StartNumbers();
        var num = parseFloat($(this).val());
        var cleanNum = num.toFixed(2);
        if (cleanNum == "NaN") {
            cleanNum = 0;
        }
        if (cleanNum < 0) {
            cleanNum = 0;
        }
        $(this).val(cleanNum);
        fixnumbers();
    });
    jQuery("#creddeptrefund").on("input", function () {
        StartNumbers();
        var gross = Number(jQuery("#totalgrosscollbns").val());
        var dispute = Number(jQuery("#dispbnsrefded").val());
        var accref = Number(jQuery("#accdeptrefund").val());
        var invref = Number(jQuery("#invdeptrefund").val());
        var credref = Number(jQuery("#creddeptrefund").val());
        var accded = Number(jQuery("#accdeptdeduct").val());
        var invded = Number(jQuery("#invdeptdeduct").val());
        var credded = Number(jQuery("#creddeptdeduct").val());
        var epf = Number(jQuery("#epfcontr").val());
        var esd = Number(jQuery("#esdcontri").val());
        var other = Number(jQuery("#othadj").val());

        var netbonus = NetBonus(gross, dispute, accref, invref, credref, accded, invded, credded, epf, esd, other)
        jQuery("#totalnetbns").val(netbonus);

    });
    $('input#othadj').blur(function () {
        StartNumbers();
        var num = parseFloat($(this).val());
        var cleanNum = num.toFixed(2);
        if (cleanNum == "NaN") {
            cleanNum = 0;
        }
        $(this).val(cleanNum);
        fixnumbers();
    });
    jQuery("#othadj").on("input", function () {
        StartNumbers();
        var gross = Number(jQuery("#totalgrosscollbns").val());
        var dispute = Number(jQuery("#dispbnsrefded").val());
        var accref = Number(jQuery("#accdeptrefund").val());
        var invref = Number(jQuery("#invdeptrefund").val());
        var credref = Number(jQuery("#creddeptrefund").val());
        var accded = Number(jQuery("#accdeptdeduct").val());
        var invded = Number(jQuery("#invdeptdeduct").val());
        var credded = Number(jQuery("#creddeptdeduct").val());
        var epf = Number(jQuery("#epfcontr").val());
        var esd = Number(jQuery("#esdcontri").val());
        var other = Number(jQuery("#othadj").val());

        var netbonus = NetBonus(gross, dispute, accref, invref, credref, accded, invded, credded, epf, esd, other)
        jQuery("#totalnetbns").val(netbonus);

    });
    $('input#totrecbal').blur(function () {
        StartNumbers();
        var num = parseFloat($(this).val());
        var cleanNum = num.toFixed(2);
        if (cleanNum == "NaN") {
            cleanNum = 0;
        }
        $(this).val(cleanNum);
        fixnumbers();
    });
    //jQuery("#totrecbal").on("input", function () {
    //    StartNumbers();
    //    var ClosingBal = Number(jQuery("#totrecbal").val());

    //    jQuery.ajax({
    //        cache: false,
    //        type: "GET",
    //        url: "/BonusCalculation/CalcNotQualifiAmt",
    //        data: {
    //            closingbal: ClosingBal, com: Company, pc: Profitcenter, bonusdate: jQuery("#monthdate").val()
    //        },
    //        success: function (result) {
    //            if (result.login == true) {
    //                if (result.success == true) {
    //                    jQuery("#grapernotqlyamt").val(Number(result.NVal).toFixed(2));
    //                    jQuery("#disregartamt").val(Number(result.DVal).toFixed(2));
    //                } else {
    //                    setError(result.msg);
    //                }
    //            } else {
    //                Logout();
    //            }
    //        }
    //    });
    //});

    //$('input#activeaccounts').blur(function () {
    //    StartNumbers();
    //    var num = parseFloat($(this).val());
    //    var cleanNum = num.toFixed(2);
    //    if (cleanNum == "NaN") {
    //        cleanNum = 0;
    //    }
    //    $(this).val(cleanNum);
    //    fixnumbers();
    //});
    //jQuery("#activeaccounts").on("input", function () {
    //    StartNumbers();
    //    var Accounts = Number(jQuery("#activeaccounts").val());

    //    jQuery.ajax({
    //        cache: false,
    //        type: "GET",
    //        url: "/BonusCalculation/AccountsTextChange",
    //        data: {
    //            _arrper: jQuery("#arrper").val(), com: Company, pccat: jQuery("#category2").val(), pc: Profitcenter, _year: Years, noacc: jQuery("#activeaccounts").val(), closingbal: jQuery("#totrecbal").val(), monthremit: jQuery("#totremitted").val(),Manager:Manager
    //        },
    //        success: function (result) {
    //            if (result.login == true) {
    //                if (result.success == true) {
    //                    jQuery("#bonuspers").val(Number(result.rate).toFixed(2));
    //                    jQuery("#bonusamt").val(Number(result.bonusamt).toFixed(2));
    //                    jQuery("#targetnoofaccounts").val(Number(result.targetacc).toFixed(2));
    //                    jQuery("#avgperacc").val(Number(result.targetper).toFixed(2));
    //                } else {
    //                    setError(result.msg);
    //                }
    //            } else {
    //                Logout();
    //            }
    //        }
    //    });
    //});

    function NetBonus(gross,dispute,accref,invref,credref,accded,invded,credded,epf,esd,other) {
      
        var netbonus = gross + 0 + accref + invref + credref - accded - invded - credded - epf - esd + other;
        
        if (jQuery.isNumeric(netbonus)==false)
        {
            netbonus = 0;
        }
        return netbonus.toFixed(2);
    }
    function CommasTOnumberWith(yourNumber) {
        //Seperates the components of the number
        var n = yourNumber.toString().split(",");
        if (n.length == 1) {
            yourNumber = yourNumber.toString();
        }
        n = yourNumber.toString().split(",");
        var num = "";

        for (var i = 0 ; i < n.length ; i++) {
            num = num + n[i].toString();
        }
        return num;
    }
    function numberWithCommas(yourNumber) {
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

    jQuery(".closaccchange-click").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to re cal ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    StartNumbers();
                    var closingbal = jQuery("#totrecbal").val();
                    var accounts = jQuery("#activeaccounts").val();

                    if (closingbal == "" || accounts == "") {
                        setInfoMsg("Cannot Empty Fields");
                        return;
                    }
                    jQuery.ajax({
                        cache: false,
                        type: "GET",
                        url: "/BonusCalculation/UpdateClosingbal",
                        data: {
                            closingbal: closingbal, accounts: accounts
                        },
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    if (result.Type = "Success") {
                                        jQuery.ajax({
                                            cache: false,
                                            type: "GET",
                                            url: "/BonusCalculation/getManagerTotAdj",
                                            data: { BonusMonth: jQuery("#monthdate").val(), Manager: jQuery("#manager").val(), EffectDate: jQuery("#effectdate").val() },
                                            success: function (result) {
                                                if (result.login == true) {
                                                    if (result.success == true) {
                                                        if (result.data.length > 0) {
                                                            updateArrBalAccounts(result.data);
                                                        }
                                                    } else {
                                                    }
                                                } else {
                                                    Logout();
                                                }
                                            }
                                        });
                                        clearAll();
                                        setSuccesssMsg(result.msg);
                                    }else
                                    if (result.Type = "Error") {
                                        setError(result.msg);
                                    }else
                                    if (result.Type = "Info") {
                                        setInfoMsg(result.msg);
                                    }
                                 
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
    jQuery(".btn-clear-data").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to clear data ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    window.location.href = "/BonusCalculation";
                }
            }
        });

    });
    jQuery(".btn-excel-upload").unbind('click').click(function (evt) {
        evt.preventDefault();
        jQuery('#exclupload').modal({
            keyboard: false,
            backdrop: 'static'
        }, 'show');
    });
    jQuery(".cls-excel-popup").unbind('click').click(function (evt) {
        $('#exclupload').modal('hide');
    });
    jQuery('.imprt-cd-data').click(function (e) {
        if (jQuery('#UploadedFile').val() == "") {
            setInfoMsg("Please Sellect File");
        } else {
            Lobibox.confirm({
                msg: "Do you want to continue process?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        var form = $('#imp-data')[0];
                        var dataString = new FormData(form);
                        $.ajax({
                            url: '/BonusCalculation/UpdateRemarks',  //Server script to process data
                            type: 'POST',
                            xhr: function () {  // Custom XMLHttpRequest
                                var myXhr = $.ajaxSettings.xhr();
                                if (myXhr.upload) { // Check if upload property exists
                                    //myXhr.upload.onprogress = progressHandlingFunction
                                    myXhr.upload.addEventListener('progress', progressHandlingFunction,
                                    false); // For handling the progress of the upload
                                }
                                return myXhr;
                            },
                            //Ajax events
                            // Form data
                            data: dataString,
                            //Options to tell jQuery not to process data or worry about content-type.
                            cache: false,
                            contentType: false,
                            processData: false,
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                       //
                                        $('#exclupload').modal('hide');
                                        setSuccesssMsg(result.msg);
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
        }


    });
    function progressHandlingFunction(e) {
        if (e.lengthComputable) {
            var percentComplete = Math.round(e.loaded * 100 / e.total);
            $("#FileProgress").css("width",
            percentComplete + '%').attr('aria-valuenow', percentComplete);
            $('#FileProgress span').text(percentComplete + "%");
        }
        else {
            $('#FileProgress span').text('unable to compute');
        }
    }
});