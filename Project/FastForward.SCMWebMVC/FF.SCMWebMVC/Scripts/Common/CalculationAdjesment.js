jQuery(document).ready(function () {
    jQuery('#monthdate').val(my_date_formatmonth(new Date()));
    jQuery('#monthdate').datepicker({ dateFormat: "MM yy" })
    jQuery('#effectmonth').val(my_date_formatmonth(new Date()));
    jQuery('#effectmonth').datepicker({ dateFormat: "MM yy" })
    jQuery(".manager-search").click(function (evt) {
        var headerKeys = Array();
        headerKeys = ["Row", "Code", "Name"];
        field = "shwmanager";
        var x = new CommonSearch(headerKeys, field);
    });

    ManagerCodeFocusout();
    function ManagerCodeFocusout() {
        $('#manager').focusout(function () {
            var pc = $(this).val();
            jQuery.ajax({
                cache: false,
                type: "GET",
                url: "/CalculationAdjesment/getShwroomManagerData",
                data: { ManagerCD: jQuery("#manager").val() },
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.list.length > 0)
                            {
                            }
                        } else {
                            setInfoMsg('Invalid Manager!!');
                            jQuery("#manager").val("");
                            jQuery("#manager").focus();
                            return;
                        }
                    } else {
                        Logout();
                    }
                }
            });
        });
    }
    jQuery(".btn-view-data").click(function (evt) {
        if (jQuery("#monthdate").val()=="")
        {
            setInfoMsg("Please Select Bonus Month");
            return;
        }
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/CalculationAdjesment/getManagerTotAdj",
            data: { BonusMonth: jQuery("#monthdate").val(), Manager: jQuery("#manager").val(), EffectDAte: jQuery("#effectmonth").val() },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.data.length > 0)
                        {
                            updateArrBalAccounts(result.data);
                            
                            Clearfields();
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
                          '<td>' + my_date_formatmonth(getFormatedDate1(data[i].Haa_date)) + '</td>' +
                       '<td style="text-align:center;"><img class="delete-img search-pc-adj" src="../Resources/images/Search-icon.png"></td>' +
                       '<td style="text-align:center;"><img class="delete-img edit-arre-adj" src="../Resources/images/arrowup.png"></td>' +
                        '</tr>');
            }
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
            EditDataCode();
            ViewPc();

        }
    }
    function updateArrBalAccounts2(data) {
        jQuery('.adj-data-row .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {
                jQuery('.adj-data-row').append('<tr class="new-row">' +
                          '<td>' + data[i].Haa_mng_cd + '</td>' +
                          '<td>' + my_date_formatmonth(getFormatedDate1(data[i].Haa_date)) + '</td>' +
                       '<td style="text-align:center;"><img class="delete-img search-pc-adj" src="../Resources/images/Search-icon.png"></td>' +
                       '<td style="text-align:center;"><img class="delete-img edit-arre-adj" src="../Resources/images/arrowup.png"></td>' +
                        '</tr>');
            }
            EditDataCode();
            ViewPc();

        }
    }
    function LoadpageData(newPageValue) {
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/CalculationAdjesment/GetPageData",
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
                            url: "/CalculationAdjesment/EditDataCode",
                            data: { Manager: Manager, PC: jQuery("#pc").val() },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        //Fill Forms
                                        fillfields(result.list, result.prvarr, result.prvadj, result.prsupcoladj, result.prgrccolladj, result.prtotclbal, result.prtotnumact, result.prmsupcoll);

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

    function ViewPc()
    {
        jQuery(".search-pc-adj").unbind('click').click(function (evt) {
            evt.preventDefault();
            var td = jQuery(this).parent('td');
            var tr = jQuery(td).parent('tr');
            var Manager = jQuery(tr).find('td:eq(0)').html();

            Lobibox.confirm({
                msg: "Do you want to show pc ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/CalculationAdjesment/ShowPCCodes",
                            data: { Manager: Manager },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        //View PC Grid
                                        ShowAllPcs(result.list);

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

    function StartNumbers() {
        jQuery("#currduetotal").val(CommasTOnumberWith(jQuery("#currduetotal").val()));
        jQuery("#duetotaladj").val(CommasTOnumberWith(jQuery("#duetotaladj").val()));
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
       // jQuery("#totrecbal").val(CommasTOnumberWith(jQuery("#totrecbal").val()));
        jQuery("#totremadj").val(CommasTOnumberWith(jQuery("#totremadj").val()));
        jQuery("#duetotal").val(CommasTOnumberWith(jQuery("#duetotal").val()));
        jQuery("#othadj").val(CommasTOnumberWith(jQuery("#othadj").val()));
        jQuery("#handovadj").val(CommasTOnumberWith(jQuery("#handovadj").val()));
       // jQuery("#totnofactacc").val(CommasTOnumberWith(jQuery("#totnofactacc").val()));
    }
    function fixnumbers() {
        jQuery("#currduetotal").val(numberWithCommas(jQuery("#currduetotal").val()));
        jQuery("#duetotaladj").val(numberWithCommas(jQuery("#duetotaladj").val()));
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
        jQuery("#othadj").val(numberWithCommas(jQuery("#othadj").val()));
        jQuery("#handovadj").val(numberWithCommas(jQuery("#handovadj").val()));
        //jQuery("#totnofactacc").val(numberWithCommas(jQuery("#totnofactacc").val()));
        jQuery("#totremadj").val(numberWithCommas(jQuery("#totremadj").val()));
        jQuery("#duetotal").val(numberWithCommas(jQuery("#duetotal").val()));
        //jQuery("#totrecbal").val(numberWithCommas(jQuery("#totrecbal").val()));
        		       

    }
    function fillfields(data, prevarr, prevadj, prsupcoladj, prgrccolladj, prtotclbal, prtotnumact,prmsuppcoll)
    {
        //prevadj
        jQuery("#currduetotal").val(data[0].HAA_CURR_DUE_TOT - data[0].HAA_HAND_OVER);
        jQuery("#duetotaladj").val(data[0].HAA_ADJ_DUE_TOT);
        jQuery("#prevarre").val(prevarr);
        jQuery("#premontharreadj").val(data[0].HAA_ADJ_PREV_TOTARR);
        jQuery("#currmonarr").val(data[0].Haa_act_arr_amt - data[0].HAA_HAND_OVER);
        jQuery("#currmonarradj").val(data[0].HAA_ADJ_CURR_ARR);
        jQuery("#gracepersett").val(Number( data[0].HAA_GRCE_SETT).toFixed(2));
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
        jQuery("#premongrcoll").val( Number(prgrccolladj).toFixed(2));
        jQuery("#premongrcolladj").val(Number(data[0].HAA_ADJ_PRE_GR_PER_COLL.toFixed(2)));
        jQuery("#premonsupcoll").val(Number(prsupcoladj).toFixed(2));
        jQuery("#premonsupcolladj").val(Number(data[0].HAA_ADJ_PR_MO_SUP_COLL).toFixed(2));
       // jQuery("#totrecbal").val(data[0].Haa_tot_clos_bal);
        jQuery("#totremadj").val(data[0].HAA_ADJ_REMITT);
        jQuery("#othadj").val(data[0].HAA_OTH);
        jQuery("#grpercolladj").val(data[0].HAA_GRC_PRD_COL_ADJ);
       // jQuery("#totnofactacc").val(data[0].Haa_tot_no_of_act_acc);
        jQuery("#mainpc").val(data[0].Haa_pc);
        //arearscalc
        var duetotal = data[0].HAA_CURR_DUE_TOT + prevarr - data[0].HAA_ADJ_PREV_TOTARR - data[0].HAA_ADJ_DUE_TOT - data[0].HAA_HAND_OVER;
        jQuery("#duetotal").val(duetotal);
        jQuery("#handovadj").val(data[0].HAA_HAND_OVER);
        //Total Remmited
        jQuery("#netremittance").val((Number(jQuery("#totremitted").val()) + Number(jQuery("#totremadj").val()) + Number(jQuery("#suppcollec").val())
    + Number(jQuery("#suppcollecadj").val()) + Number(jQuery("#grpercoll").val()) + Number(jQuery("#grpercolladj").val()) - Number(jQuery("#premongrcoll").val())
   + Number(jQuery("#premongrcolladj").val()) - Number(jQuery("#premonsupcoll").val()) + Number(jQuery("#premonsupcolladj").val())).toFixed(2));
        //supplymentary commission
        fixnumbers();
    }
    function Clearfields() {

        jQuery("#currduetotal").val("");
        jQuery("#duetotaladj").val("");
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
        jQuery("#suppcollec").val("");
        jQuery("#suppcollecadj").val("");
        jQuery("#grpercoll").val("");
        jQuery("#grpercolladj").val("");
        jQuery("#premongrcoll").val("");
        jQuery("#premongrcolladj").val("");
        jQuery("#premonsupcoll").val("");
        jQuery("#premonsupcolladj").val("");
        jQuery("#netremittance").val("");
        //jQuery("#totrecbal").val("");
        jQuery("#totremadj").val("");
        jQuery("#othadj").val("");
        jQuery("#grpercolladj").val("");
        //arearscalc
        jQuery("#duetotal").val("");
        jQuery("#handovadj").val("");
        jQuery("#mainpc").val("");
        //Total Remmited
        //supplymentary commission
        fixnumbers();
    }
    function ShowAllPcs(data)
    {
        jQuery('#pclistpopup').modal({
            keyboard: false,
            backdrop: 'static'
        }, 'show');

        updatePcList(data);
    }
    function updatePcList(data) {
        jQuery('.pc-table .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {
                jQuery('.pc-table').append('<tr class="new-row">' +
                     '<td>' + data[i].Haa_pc + '</td>' +
                       '<td style="text-align:center;"><img class="delete-img editpc" src="../Resources/images/arrowup.png"></td>' +
                        '</tr>');
            }
            PcEdit();
        }
    }

    function PcEdit()
    {
        jQuery(".editpc").unbind('click').click(function (evt) {
            evt.preventDefault();
            var td = jQuery(this).parent('td');
            var tr = jQuery(td).parent('tr');
            var Pc = jQuery(tr).find('td:eq(0)').html();
            //
            jQuery("#pc").val(Pc);
        });
    }
    jQuery(".btn-clear-data").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to clear data ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    window.location.href = "/CalculationAdjesment";
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
                    var duetotadj = jQuery("#duetotaladj").val();
                    var prevmontharreadj = jQuery("#premontharreadj").val();
                    var duetotal = jQuery("#duetotal").val();
                    var currarradj = jQuery("#currmonarradj").val();
                    var gracepersettadj = jQuery("#gracepersettadj").val();
                    var shortremitance = jQuery("#shortremitt").val();
                    var issuechkrtn = jQuery("#issuechqret").val();
                    var totremiadj = jQuery("#totremadj").val();
                    var supplcolladj = jQuery("#suppcollecadj").val();
                    var gracepercolladj = jQuery("#grpercolladj").val();
                    var prevmonthgracecolladj = jQuery("#premongrcolladj").val();
                    var prevmonthsupcolladj = jQuery("#premonsupcolladj").val();
                   // var totrecbal = jQuery("#totrecbal").val();
                    var effectdate = jQuery("#effectmonth").val();
                    var othadj = jQuery("#othadj").val();
                    var netremitance = jQuery("#netremittance").val();

                    if (duetotadj == "" || prevmontharreadj == "" || currarradj == "" || duetotal == "" || gracepersettadj == "" || shortremitance == "" || issuechkrtn == "" || totremiadj == "" || supplcolladj == "" || gracepercolladj == "" || prevmonthgracecolladj == "" || prevmonthsupcolladj == ""  || othadj=="")
                    {
                        setInfoMsg("Cannot Empty Fields");
                        return;
                    }

                    jQuery.ajax({
                        cache: false,
                        type: "GET",
                        url: "/CalculationAdjesment/SaveAdjDetails",
                        data: {
                            duetotadj: duetotadj, prevmontharreadj: prevmontharreadj, duetotal: duetotal, currarradj: currarradj,
                            gracepersettadj: gracepersettadj, shortremitance: shortremitance, issuechkrtn: issuechkrtn, totremiadj: totremiadj,
                            supplcolladj: supplcolladj, gracepercolladj: gracepercolladj, prevmonthgracecolladj: prevmonthgracecolladj,
                            prevmonthsupcolladj: prevmonthsupcolladj, totrecbal: 0, effectdate: effectdate, othadj: othadj, netremitance: netremitance
                        },
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
    function CommasTOnumberWith(yourNumber) {
        //Seperates the components of the number
        if (yourNumber == null) {
            return 0;
        }
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
    $('input#duetotaladj').blur(function () {
        StartNumbers();
        var num = parseFloat($(this).val());
        var cleanNum = num.toFixed(2);
        if (cleanNum == "NaN") {
            cleanNum = 0;
        }
        $(this).val(cleanNum);
        fixnumbers();
    });
    $('input#currmonarradj').blur(function () {
        StartNumbers();

        var num = parseFloat($(this).val());
        var cleanNum = num.toFixed(2);
        if (cleanNum == "NaN") {
            cleanNum = 0;
        }
        $(this).val(cleanNum);
        fixnumbers();
    });
    $('input#gracepersettadj').blur(function () {
        StartNumbers();

        var num = parseFloat($(this).val());
        var cleanNum = num.toFixed(2);
        if (cleanNum == "NaN") {
            cleanNum = 0;
        }
        $(this).val(cleanNum);
        fixnumbers();
    });

    $('input#shortremitt').blur(function () {
        StartNumbers();
     
        var num = parseFloat($(this).val());
        var cleanNum = num.toFixed(2);
        if (cleanNum == "NaN") {
            cleanNum = 0;
        } else if (Number(cleanNum)<0) {
            cleanNum = 0;
        }
        $(this).val(cleanNum);
        fixnumbers();
    });
    $('input#issuechqret').blur(function () {
        StartNumbers();

        var num = parseFloat($(this).val());
        var cleanNum = num.toFixed(2);
        if (cleanNum == "NaN") {
            cleanNum = 0;
        }
        else if (Number(cleanNum) < 0) {
            cleanNum = 0;
        }
        $(this).val(cleanNum);
        fixnumbers();
    });
    $('input#totremadj').blur(function () {
        StartNumbers();

        var num = parseFloat($(this).val());
        var cleanNum = num.toFixed(2);
        if (cleanNum == "NaN") {
            cleanNum = 0;
        }
        $(this).val(cleanNum);
        fixnumbers();
    });
    $('input#suppcollecadj').blur(function () {
        StartNumbers();

        var num = parseFloat($(this).val());
        var cleanNum = num.toFixed(2);
        if (cleanNum == "NaN") {
            cleanNum = 0;
        }
        $(this).val(cleanNum);
        fixnumbers();
    });
    //$('input#premongrcolladj').blur(function () {
    //    StartNumbers();

    //    var num = parseFloat($(this).val());
    //    var cleanNum = num.toFixed(2);
    //    if (cleanNum == "NaN") {
    //        cleanNum = 0;
    //    }
    //    $(this).val(cleanNum);
    //    fixnumbers();
    //});
    $('input#grpercolladj').blur(function () {
        StartNumbers();

        var num = parseFloat($(this).val());
        var cleanNum = num.toFixed(2);
        if (cleanNum == "NaN") {
            cleanNum = 0;
        }
        $(this).val(cleanNum);
        fixnumbers();
    });
    //$('input#premonsupcolladj').blur(function () {
    //    StartNumbers();

    //    var num = parseFloat($(this).val());
    //    var cleanNum = num.toFixed(2);
    //    if (cleanNum == "NaN") {
    //        cleanNum = 0;
    //    }
    //    $(this).val(cleanNum);
    //    fixnumbers();
    //});
    //$('input#totrecbal').blur(function () {
    //    StartNumbers();

    //    var num = parseFloat($(this).val());
    //    var cleanNum = num.toFixed(2);
    //    if (cleanNum == "NaN") {
    //        cleanNum = 0;
    //    }
    //    $(this).val(cleanNum);
    //    fixnumbers();
    //});
    jQuery("#duetotaladj").on("input", function () {
        StartNumbers();
        var duetot = jQuery("#currduetotal").val();
        var dueadj = jQuery("#duetotaladj").val();
        var prearradj = jQuery("#premontharreadj").val();
        var prearr = jQuery("#prevarre").val();
        var arre = jQuery("#arramt").val();
        var bal = (Number(duetot) + Number(prearr) + Number(prearradj) + Number(dueadj)).toFixed(2);
        jQuery("#duetotal").val(bal);
    });
    $('input#premontharreadj').blur(function () {
        StartNumbers();
        var num = parseFloat($(this).val());
        var cleanNum = num.toFixed(2);
        if (cleanNum == "NaN") {
            cleanNum = 0;
        }
        $(this).val(cleanNum);
        fixnumbers();
    });
    jQuery("#premontharreadj").on("input", function () {
        StartNumbers();
        var duetot = jQuery("#currduetotal").val();
        var dueadj = jQuery("#duetotaladj").val();
        var prearradj = jQuery("#premontharreadj").val();
        var prearr = jQuery("#prevarre").val();
        var arre = jQuery("#arramt").val();
        var hand = jQuery("#handovadj").val();
        var bal = (Number(duetot) + Number(prearr) - Number(prearradj) + Number(dueadj) - Number(hand)).toFixed(2);
        jQuery("#duetotal").val(bal);
    });
    $('input#premonsupcolladj').blur(function () {
        StartNumbers();
        var num = parseFloat($(this).val());
        var cleanNum = num.toFixed(2);
        if (cleanNum == "NaN") {
            cleanNum = 0;
        }
        $(this).val(cleanNum);
        fixnumbers();
    });
    jQuery("#premonsupcolladj").on("input", function () {
        StartNumbers();
        var duetot = jQuery("#currduetotal").val();
        var dueadj = jQuery("#duetotaladj").val();
        var prearradj = jQuery("#premontharreadj").val();
        var prearr = jQuery("#prevarre").val();
        var arre = jQuery("#arramt").val();
        var hand = jQuery("#handovadj").val();
        var bal = (Number(duetot) + Number(prearr) - Number(prearradj) + Number(dueadj) - Number(hand)).toFixed(2);
        jQuery("#duetotal").val(bal);
    });
    jQuery("#premonsupcolladj").on("input", function () {
        StartNumbers();
        var netremit = (Number(jQuery("#totremitted").val()) + Number(jQuery("#totremadj").val()) + Number(jQuery("#suppcollec").val())
+ Number(jQuery("#suppcollecadj").val()) + Number(jQuery("#grpercoll").val()) + Number(jQuery("#grpercolladj").val()) - Number(jQuery("#premongrcoll").val())
+ Number(jQuery("#premongrcolladj").val()) - Number(jQuery("#premonsupcoll").val()) + Number(jQuery("#premonsupcolladj").val())).toFixed(2);
        jQuery("#netremittance").val(netremit);
    });
    $('input#premongrcolladj').blur(function () {
        StartNumbers();
        var num = parseFloat($(this).val());
        var cleanNum = num.toFixed(2);
        if (cleanNum == "NaN") {
            cleanNum = 0;
        }
        $(this).val(cleanNum);
        fixnumbers();
    });
    jQuery("#premongrcolladj").on("input", function () {
        StartNumbers();
        var netremit = (Number(jQuery("#totremitted").val()) + Number(jQuery("#totremadj").val()) + Number(jQuery("#suppcollec").val())
+ Number(jQuery("#suppcollecadj").val()) + Number(jQuery("#grpercoll").val()) + Number(jQuery("#grpercolladj").val()) - Number(jQuery("#premongrcoll").val())
+ Number(jQuery("#premongrcolladj").val()) - Number(jQuery("#premonsupcoll").val()) + Number(jQuery("#premonsupcolladj").val())).toFixed(2);
        jQuery("#netremittance").val(netremit);
    });
    jQuery("#totremadj").on("input", function () {
        StartNumbers();
        var netremit = (Number(jQuery("#totremitted").val()) + Number(jQuery("#totremadj").val()) + Number(jQuery("#suppcollec").val())
+ Number(jQuery("#suppcollecadj").val()) + Number(jQuery("#grpercoll").val()) + Number(jQuery("#grpercolladj").val()) - Number(jQuery("#premongrcoll").val())
+ Number(jQuery("#premongrcolladj").val()) - Number(jQuery("#premonsupcoll").val()) + Number(jQuery("#premonsupcolladj").val())).toFixed(2);
        jQuery("#netremittance").val(netremit);
    });
    jQuery("#suppcollecadj").on("input", function () {
        StartNumbers();
        var netremit = (Number(jQuery("#totremitted").val()) + Number(jQuery("#totremadj").val()) + Number(jQuery("#suppcollec").val())
+ Number(jQuery("#suppcollecadj").val()) + Number(jQuery("#grpercoll").val()) + Number(jQuery("#grpercolladj").val()) - Number(jQuery("#premongrcoll").val())
+ Number(jQuery("#premongrcolladj").val()) - Number(jQuery("#premonsupcoll").val()) + Number(jQuery("#premonsupcolladj").val())).toFixed(2);
        jQuery("#netremittance").val(netremit);
    });
    jQuery("#grpercolladj").on("input", function () {
        StartNumbers();
        var netremit = (Number(jQuery("#totremitted").val()) + Number(jQuery("#totremadj").val()) + Number(jQuery("#suppcollec").val())
+ Number(jQuery("#suppcollecadj").val()) + Number(jQuery("#grpercoll").val()) + Number(jQuery("#grpercolladj").val()) - Number(jQuery("#premongrcoll").val())
+ Number(jQuery("#premongrcolladj").val()) - Number(jQuery("#premonsupcoll").val()) + Number(jQuery("#premonsupcolladj").val())).toFixed(2);
        jQuery("#netremittance").val(netremit);
    });
});