jQuery(document).ready(function () {
   
   // jQuery('#gdate').datepicker({ dateFormat: "dd/M/yy" })

    jQuery('#monthdate').val(my_date_formatmonth(new Date()));
    jQuery('#monthdate').datepicker({
        dateFormat: "MM yy",
        onSelect: function (dateText) {
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/CollectionBonusAdjustments/LoadGraceDate",
            data: { ProfCenter: jQuery("#location").val(), month: jQuery("#monthdate").val() },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        jQuery("#gdate").val(getFormatedDate1(result.date));
                    } else {
                        setInfoMsg("Error!");
                        return;
                    }
                } else {
                    Logout();
                }
            }
        });
    }
    })
    jQuery('#effectmonth').val(my_date_formatmonth(new Date()));
    jQuery('#effectmonth').datepicker({ dateFormat: "MM yy" })
    LoadGraceDate();
    function LoadGraceDate()
    {
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/CollectionBonusAdjustments/LoadGraceDate",
            data: { ProfCenter: jQuery("#location").val(), month: jQuery("#monthdate").val() },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true)
                    {
                        jQuery("#gdate").val(getFormatedDate1(result.date));
                    } else {
                        setInfoMsg("Error!");
                        return;
                    }
                } else {
                    Logout();
                }
            }
        });
    }

$('#monthdate').focusout(function () {
    jQuery.ajax({
        cache: false,
        type: "GET",
        url: "/CollectionBonusAdjustments/LoadGraceDate",
        data: { ProfCenter: jQuery("#location").val(), month: jQuery("#monthdate").val() },
        success: function (result) {
            if (result.login == true) {
                if (result.success == true) {
                    jQuery("#gdate").val(getFormatedDate1(result.date));
                } else {
                    setInfoMsg("Error!");
                    return;
                }
            } else {
                Logout();
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
                    window.location.href = "/CollectionBonusAdjustments";
                }
            }
        });

    });
    jQuery(".loc-search").click(function (evt) {
        var headerKeys = Array();
        headerKeys = ["Row", "Code", "Description"];
        field = "srchPCM2";
        var data = {
            chnl: "",
            sChnl: "",
            area: "",
            regn: "",
            zone: "",
            type: "PC"
        };
        var x = new CommonSearch(headerKeys, field, data);
    });
    profitcenterFocusout();
    function profitcenterFocusout() {
        $('#location').focusout(function () {
            var pc = $(this).val();
            jQuery.ajax({
                cache: false,
                type: "GET",
                url: "/CollectionBonusAdjustments/LoadBalaArrearsList",
                data: { ProfCenter: jQuery("#location").val(), month: jQuery("#monthdate").val(), effectivedate: jQuery("#effectmonth").val() },
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.list.length > 0) {
                                //show list
                                updateArrBalAccounts(result.list);
                              
                            }
                            jQuery.ajax({
                                cache: false,
                                type: "GET",
                                url: "/CollectionBonusAdjustments/LoadGraceDate",
                                data: { ProfCenter: jQuery("#location").val(), month: jQuery("#monthdate").val() },
                                success: function (result) {
                                    if (result.login == true) {
                                        if (result.success == true) {
                                            jQuery("#gdate").val(getFormatedDate1(result.date));
                                        } else {
                                            setInfoMsg("Error!");
                                            return;
                                        }
                                    } else {
                                        Logout();
                                    }
                                }
                            });
                           
                        } else {
                            setInfoMsg('No Arreas Limit For ' + jQuery("#location").val());
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
        var pc = $(this).val();
        if (jQuery("#location").val()=="")
        {
            setInfoMsg("Please Select PC");
            return;
        }
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/CollectionBonusAdjustments/LoadBalaArrearsList",
            data: { ProfCenter: jQuery("#location").val(), month: jQuery("#monthdate").val(), effectivedate: jQuery("#effectmonth").val() },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.list.length > 0) {
                            //show list
                            updateArrBalAccounts(result.list);
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
    });
    function viewalllist() {
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/CollectionBonusAdjustments/LoadBalaArrearsList",
            data: { ProfCenter: jQuery("#location").val(), month: jQuery("#monthdate").val(), effectivedate: jQuery("#effectmonth").val() },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.list.length > 0) {
                            //show list
                            updateArrBalAccounts(result.list);
                           
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
    function updateArrBalAccounts(data) {
        jQuery('.adj-data-row .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {

                var totare = data[i].Haad_act_arr_amt;
                var adjval = data[i].HAAD_DISP_ADJ + data[i].HAAD_SER_PROB + data[i].HAAD_LOD_ADJ + data[i].HAAD_OTH + data[i].HAAD_DIRIYA_ADJ + data[i].HAAD_SHOP_COM_ADJ + data[i].HAAD_HAND_OVER + data[i].HAAD_ARR_RELE_MONTHS;;
                var netarre = totare - adjval - Number(data[i].HAAD_GRCE_SETT);

                jQuery('.adj-data-row').append('<tr class="new-row">' +
                          '<td>' + data[i].Haad_pc + '</td>' +
                          '<td>' + my_date_formatmonth(getFormatedDate1(data[i].Haad_date)) + '</td>' +
                           '<td>' + data[i].Haad_acc_cd + ' (' + data[i].Scheme + ')' + '</td>' +
                         '<td style="color: black; font-size: x-small;text-align:right">' + ReplaceNumberWithCommas(data[i].Haad_act_arr_amt) + '</td>' +
                         '<td style="color: black; font-size: x-small;text-align:right">' + ReplaceNumberWithCommas(data[i].HAAD_GRCE_SETT) + '</td>' +
                          '<td style="color: black; font-size: x-small;text-align:right">' + ReplaceNumberWithCommas(data[i].HAAD_ARR_RELE_MONTHS) + '</td>' +
                            '<td style="color: black; font-size: x-small;text-align:right">' + ReplaceNumberWithCommas(data[i].HAAD_SER_PROB) + '</td>' +
                            '<td style="color: black; font-size: x-small;text-align:right">' + ReplaceNumberWithCommas(data[i].HAAD_DISP_ADJ) + '</td>' +
                           '<td style="color: black; font-size: x-small;text-align:right">' +ReplaceNumberWithCommas( data[i].HAAD_DIRIYA_ADJ) + '</td>' +
                               '<td style="color: black; font-size: x-small;text-align:right">' + ReplaceNumberWithCommas(data[i].HAAD_LOD_ADJ) + '</td>' +
                                                          '<td style="color: black; font-size: x-small;text-align:right">' + ReplaceNumberWithCommas(data[i].HAAD_SHOP_COM_ADJ) + '</td>' +
                               '<td style="color: black; font-size: x-small;text-align:right">' + ReplaceNumberWithCommas(data[i].HAAD_OTH) + '</td>' +
    '<td>' + data[i].HAAD_REMARK + '</td>' +
                               '<td style="color: black; font-size: x-small;text-align:right">' + ReplaceNumberWithCommas(data[i].HAAD_HAND_OVER) + '</td>' +
                                                          '<td style="color: black; font-size: x-small;text-align:right">' + ReplaceNumberWithCommas(data[i].HAAD_ADJ_TOT) + '</td>' +
                               '<td style="color: black; font-size: x-small;text-align:right">' + ReplaceNumberWithCommas(netarre.toFixed(2)) + '</td>' +
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
            EditAccountCode();
         
        }
    }
    function updateArrBalAccounts2(data) {
        jQuery('.adj-data-row .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {

                var totare = data[i].Haad_act_arr_amt;
                var adjval = data[i].HAAD_DISP_ADJ + data[i].HAAD_SER_PROB + data[i].HAAD_LOD_ADJ + data[i].HAAD_OTH + data[i].HAAD_DIRIYA_ADJ + data[i].HAAD_SHOP_COM_ADJ + data[i].HAAD_HAND_OVER + data[i].HAAD_ARR_RELE_MONTHS;
                var netarre = totare - adjval - Number(data[i].HAAD_GRCE_SETT);

                jQuery('.adj-data-row').append('<tr class="new-row">' +
                          '<td>' + data[i].Haad_pc + '</td>' +
                          '<td>' + my_date_formatmonth(getFormatedDate1(data[i].Haad_date)) + '</td>' +
                           '<td>' + data[i].Haad_acc_cd + ' (' + data[i].Scheme + ')' + '</td>' +
                         '<td style="color: black; font-size: x-small;text-align:right">' + ReplaceNumberWithCommas(data[i].Haad_act_arr_amt) + '</td>' +
                         '<td style="color: black; font-size: x-small;text-align:right">' + ReplaceNumberWithCommas(data[i].HAAD_GRCE_SETT) + '</td>' +
                          '<td style="color: black; font-size: x-small;text-align:right">' + ReplaceNumberWithCommas(data[i].HAAD_ARR_RELE_MONTHS) + '</td>' +
                            '<td style="color: black; font-size: x-small;text-align:right">' + ReplaceNumberWithCommas(data[i].HAAD_SER_PROB) + '</td>' +
                            '<td style="color: black; font-size: x-small;text-align:right">' + ReplaceNumberWithCommas(data[i].HAAD_DISP_ADJ) + '</td>' +
                           '<td style="color: black; font-size: x-small;text-align:right">' + ReplaceNumberWithCommas(data[i].HAAD_DIRIYA_ADJ) + '</td>' +
                               '<td style="color: black; font-size: x-small;text-align:right">' + ReplaceNumberWithCommas(data[i].HAAD_LOD_ADJ) + '</td>' +
                                                          '<td style="color: black; font-size: x-small;text-align:right">' + ReplaceNumberWithCommas(data[i].HAAD_SHOP_COM_ADJ) + '</td>' +
                               '<td style="color: black; font-size: x-small;text-align:right">' + ReplaceNumberWithCommas(data[i].HAAD_OTH) + '</td>' +
    '<td>' + data[i].HAAD_REMARK + '</td>' +
                               '<td style="color: black; font-size: x-small;text-align:right">' + ReplaceNumberWithCommas(data[i].HAAD_HAND_OVER) + '</td>' +
                                                          '<td style="color: black; font-size: x-small;text-align:right">' + ReplaceNumberWithCommas(data[i].HAAD_ADJ_TOT) + '</td>' +
                               '<td style="color: black; font-size: x-small;text-align:right">' + ReplaceNumberWithCommas(netarre.toFixed(2)) + '</td>' +
                       '<td style="text-align:center;"><img class="delete-img edit-arre-adj" src="../Resources/images/arrowup.png"></td>' +
                        '</tr>');
            }
         
            EditAccountCode();

        }
    }
    function LoadpageData(newPageValue) {
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/CollectionBonusAdjustments/GetPageData",
            data: { newPageValue: newPageValue },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.list.length > 0) {
                            //show list
                            updateArrBalAccounts2(result.list);
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
    $('#Searchval').on('input', function (e) {
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/CollectionBonusAdjustments/GetSearchData",
            data: { Accno: jQuery("#Searchval").val() },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.list.length > 0) {
                            //show list
                            updateArrBalAccounts2(result.list);
                        }
                    } else {
                        setInfoMsg('Invalid No');
                        return;
                    }
                } else {
                    Logout();
                }
            }
        });
    });
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
    function getFormatedDate1(date) {
        var dte = new Date(parseInt(date.substr(6)));
        if (my_date_format_tran(dte) != "NaN/undefined/NaN")
            return my_date_format_tran(dte);
    }
    function EditAccountCode()
    {
        jQuery(".edit-arre-adj").unbind('click').click(function (evt) {
            evt.preventDefault();
            var td = jQuery(this).parent('td');
            var tr = jQuery(td).parent('tr');
            var loc = jQuery(tr).find('td:eq(0)').html();
            var account = jQuery(tr).find('td:eq(2)').html();
            var month = jQuery(tr).find('td:eq(1)').html();
            var arreas = jQuery(tr).find('td:eq(3)').html();

            Lobibox.confirm({
                msg: "Do you want to Edit ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/CollectionBonusAdjustments/EditAdjAccount",
                            data: { PC: loc, Account: account, Month: month, Arre: arreas },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        //Fill Forms
                                        fillfields(result.list);
                                        jQuery("#schem").val(result.schem);
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
    var origrper = 0;
    var ornetsale = 0;
    var realgrval = 0;
    function fillfields(data)
    {
        jQuery("#location").val(data[0].Haad_pc);
        jQuery("#accno").val(data[0].Haad_acc_cd);
        jQuery("#monthdate").val(my_date_formatmonth(getFormatedDate1(data[0].Haad_date)));
        jQuery("#gdate").val(getFormatedDate1( data[0].HAAD_GRCE_DATE));
        jQuery("#grpsettl").val(data[0].HAAD_GRCE_SETT);
        jQuery("#arramt").val(data[0].Haad_act_arr_amt);
        jQuery("#dispute").val(data[0].HAAD_DISP_ADJ);
        jQuery("#service").val(data[0].HAAD_SER_PROB);
       
        jQuery("#lod").val(data[0].HAAD_LOD_ADJ);
        jQuery("#diriya").val(data[0].HAAD_DIRIYA_ADJ);
        jQuery("#other").val(data[0].HAAD_OTH);
        jQuery("#handovrrej").val(data[0].handoverreject);
        jQuery("#dg1month").val(data[0].HAAD_ARR_RELE_MONTHS);
        jQuery("#srcomp").val(data[0].HAAD_SHOP_COM_ADJ);
        jQuery("#reson").val(data[0].HAAD_REMARK);
        //arearscalc
        var totare = data[0].Haad_act_arr_amt;
        var adjval = data[0].HAAD_DISP_ADJ + data[0].HAAD_SER_PROB + data[0].HAAD_LOD_ADJ + data[0].HAAD_OTH + data[0].HAAD_DIRIYA_ADJ + data[0].HAAD_SHOP_COM_ADJ + data[0].handoverreject + Number(data[0].HAAD_GRCE_SETT) + data[0].HAAD_ARR_RELE_MONTHS;
        jQuery("#totadjs").val(adjval);
        var netarre = totare - adjval;
        jQuery("#netarrears").val(netarre.toFixed(2));

        //desable
        jQuery("#arramt").attr('readonly', true);
        jQuery("#grpsettl").attr('readonly', true);
        jQuery("#netarrears").attr('readonly', true);
        jQuery("#handovrrej").attr('readonly', true);
        jQuery("#dg1month").attr('readonly', true);
        

        origrper = Number(data[0].HAAD_GRCE_PER_COLL);
        ornetsale = Number(netarre);
        realorgrval = Number(data[0].HAAD_GRCE_SETT_ADJ);

        fixnumbers();
    }

    function StartNumbers()
    {
        $("#arramt").val(CommasTOnumberWith(jQuery("#arramt").val()));
        $("#grpsettl").val(CommasTOnumberWith(jQuery("#grpsettl").val()));
        $("#dg1month").val(CommasTOnumberWith(jQuery("#dg1month").val()));
        $("#service").val(CommasTOnumberWith(jQuery("#service").val()));
        $("#dispute").val(CommasTOnumberWith(jQuery("#dispute").val()));
        $("#diriya").val(CommasTOnumberWith(jQuery("#diriya").val()));
        $("#lod").val(CommasTOnumberWith(jQuery("#lod").val()));
        $("#srcomp").val(CommasTOnumberWith(jQuery("#srcomp").val()));
        $("#other").val(CommasTOnumberWith(jQuery("#other").val()));
        $("#handovrrej").val(CommasTOnumberWith(jQuery("#handovrrej").val()));
        $("#totadjs").val(CommasTOnumberWith(jQuery("#totadjs").val()));
        $("#netarrears").val(CommasTOnumberWith(jQuery("#netarrears").val()));
    }
    function fixnumbers() {
        $("#arramt").val(numberWithCommas(jQuery("#arramt").val()));
        $("#grpsettl").val(numberWithCommas(jQuery("#grpsettl").val()));
        $("#dg1month").val(numberWithCommas(jQuery("#dg1month").val()));
        $("#service").val(numberWithCommas(jQuery("#service").val()));
        $("#dispute").val(numberWithCommas(jQuery("#dispute").val()));
        $("#diriya").val(numberWithCommas(jQuery("#diriya").val()));
        $("#lod").val(numberWithCommas(jQuery("#lod").val()));
        $("#srcomp").val(numberWithCommas(jQuery("#srcomp").val()));
        $("#other").val(numberWithCommas(jQuery("#other").val()));
        $("#handovrrej").val(numberWithCommas(jQuery("#handovrrej").val()));
        $("#totadjs").val(numberWithCommas(jQuery("#totadjs").val()));
        $("#netarrears").val(numberWithCommas(jQuery("#netarrears").val()));
    }

    function  AllTotAdj(dispadj,ser,totadj,lodadj,oth, diriya,comput,dgmonth, grace, hand)
    {
        if (dispadj < 0)
        {
            dispadj = 0;
        }
        if (ser < 0) {
            ser = 0;
        }
        if (totadj < 0) {
            totadj = 0;
        }
        if (lodadj < 0) {
            lodadj = 0;
        }
        if (oth < 0) {
            oth = 0;
        }
        if (diriya < 0) {
            diriya = 0;
        }
        if (comput < 0) {
            comput = 0;
        }
        return dispadj + ser + lodadj + oth + diriya + comput + dgmonth + grace+hand;
    }
    function Commatonumber(number )
    {
        alert(number.text($(this).text().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")));
      return  number.text($(this).text().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
    }

    jQuery("#service").on("input", function () {
        StartNumbers();
        var grval = jQuery("#grpsettl").val(); 
        var relese_month = jQuery("#dg1month").val();
        var hand = jQuery("#handovrrej").val();
        var totadj = AllTotAdj(Number(jQuery("#dispute").val()), Number(jQuery("#service").val()), Number(jQuery("#totadjs").val()), Number(jQuery("#lod").val()), Number(jQuery("#other").val()), Number(jQuery("#diriya").val()), Number(jQuery("#srcomp").val()), Number(grval), Number(relese_month), Number(hand));
        var arre = jQuery("#arramt").val();
        var netarre = arre - totadj;
        
        
        if (netarre<0)
        {
            var realgrval = Number(arre - totadj) + Number(grval);
            if (realgrval <0)
            {
                setInfoMsg("Can't exceed Total: " + (Number(origrper) + Number(ornetsale)));
                clearfields();
                
            }
            jQuery("#netarrears").val(0);
            if (realgrval.toFixed(2) == "NaN") {
                jQuery("#grpsettl").val(0);
            } else {
                jQuery("#grpsettl").val(realgrval.toFixed(2));
            }

        } else {
            if (netarre.toFixed(2) == "NaN") {
                jQuery("#netarrears").val(0);
            } else {
                jQuery("#netarrears").val(netarre.toFixed(2));
            }
            jQuery("#grpsettl").val(origrper.toFixed(2));
            $("#totadjs").val(totadj.toFixed(2));
           
            
        }
    });
    jQuery("#totadjs").on("input", function () {
        StartNumbers();
        var grval = jQuery("#grpsettl").val();
        var relese_month = jQuery("#dg1month").val();
        var hand = jQuery("#handovrrej").val();
        var totadj = AllTotAdj(Number(jQuery("#dispute").val()), Number(jQuery("#service").val()), Number(jQuery("#totadjs").val()), Number(jQuery("#lod").val()), Number(jQuery("#other").val()), Number(jQuery("#diriya").val()), Number(jQuery("#srcomp").val()), Number(grval), Number(relese_month), Number(hand));
        var arre = jQuery("#arramt").val();
        var netarre = arre - totadj;

        if (netarre < 0) {

            var realgrval = Number(arre - totadj) + Number(grval);
            if (realgrval < 0) {
                setInfoMsg("Can't exceed Total: " + (Number(origrper) + Number(ornetsale)));
                clearfields();

            }


            jQuery("#netarrears").val(0);

            var realgrval = Number(arre - totadj) + Number(grval);
            if (realgrval.toFixed(2) == "NaN") {
                jQuery("#grpsettl").val(0);
            } else {
                jQuery("#grpsettl").val(realgrval.toFixed(2));
            }

        } else {
            if (netarre.toFixed(2) == "NaN") {
                jQuery("#netarrears").val(0);
            } else {
                jQuery("#netarrears").val(netarre.toFixed(2));
            }
            jQuery("#grpsettl").val(origrper.toFixed(2));
            $("#totadjs").val(totadj.toFixed(2));

        }
    });
    jQuery("#lod").on("input", function () {
        StartNumbers();
        var grval = jQuery("#grpsettl").val();
        var relese_month = jQuery("#dg1month").val();
        var hand = jQuery("#handovrrej").val();
        var totadj = AllTotAdj(Number(jQuery("#dispute").val()), Number(jQuery("#service").val()), Number(jQuery("#totadjs").val()), Number(jQuery("#lod").val()), Number(jQuery("#other").val()), Number(jQuery("#diriya").val()), Number(jQuery("#srcomp").val()), Number(grval), Number(relese_month), Number(hand));
        var arre = jQuery("#arramt").val();
        var netarre = arre - totadj;

        if (netarre < 0) {
            var realgrval = Number(arre - totadj) + Number(grval);
            if (realgrval < 0) {
                setInfoMsg("Can't exceed Total: " + (Number(origrper) + Number(ornetsale)));
                clearfields();

            }


            jQuery("#netarrears").val(0);

            var realgrval = Number(arre - totadj) + Number(grval);
            if (realgrval.toFixed(2) == "NaN") {
                jQuery("#grpsettl").val(0);
            } else {
                jQuery("#grpsettl").val(realgrval.toFixed(2));
            }

        } else {
            if (netarre.toFixed(2) == "NaN") {
                jQuery("#netarrears").val(0);
            } else {
                jQuery("#netarrears").val(netarre.toFixed(2));
            }
            jQuery("#grpsettl").val(origrper.toFixed(2));
            $("#totadjs").val(totadj.toFixed(2));

        }
    });
    jQuery("#diriya").on("input", function () {
        StartNumbers();
        var grval = jQuery("#grpsettl").val();
        var relese_month = jQuery("#dg1month").val();
        var hand = jQuery("#handovrrej").val();
        var totadj = AllTotAdj(Number(jQuery("#dispute").val()), Number(jQuery("#service").val()), Number(jQuery("#totadjs").val()), Number(jQuery("#lod").val()), Number(jQuery("#other").val()), Number(jQuery("#diriya").val()), Number(jQuery("#srcomp").val()), Number(grval), Number(relese_month), Number(hand));
        var arre = jQuery("#arramt").val();
        var netarre = arre - totadj;
        if (netarre < 0) {
            var realgrval = Number(arre - totadj) + Number(grval);
            if (realgrval < 0) {
                setInfoMsg("Can't exceed Total: " + (Number(origrper) + Number(ornetsale)));
                clearfields();

            }
            jQuery("#netarrears").val(0);

            var realgrval = Number(arre - totadj) + Number(grval);
            if (realgrval.toFixed(2) == "NaN") {
                jQuery("#grpsettl").val(0);
            } else {
                jQuery("#grpsettl").val(realgrval.toFixed(2));
            }

        } else {
            if (netarre.toFixed(2) == "NaN") {
                jQuery("#netarrears").val(0);
            } else {
                jQuery("#netarrears").val(netarre.toFixed(2));
            }
            jQuery("#grpsettl").val(origrper.toFixed(2));
            $("#totadjs").val(totadj.toFixed(2));

        }
    });
    jQuery("#other").on("input", function () {
        StartNumbers();
        var grval = jQuery("#grpsettl").val();
        var relese_month = jQuery("#dg1month").val();
        var hand = jQuery("#handovrrej").val();
        var totadj = AllTotAdj(Number(jQuery("#dispute").val()), Number(jQuery("#service").val()), Number(jQuery("#totadjs").val()), Number(jQuery("#lod").val()), Number(jQuery("#other").val()), Number(jQuery("#diriya").val()), Number(jQuery("#srcomp").val()), Number(grval), Number(relese_month), Number(hand));
        var arre = jQuery("#arramt").val();
        var netarre = arre - totadj;

        if (netarre < 0) {
            var realgrval = Number(arre - totadj) + Number(grval);
            if (realgrval < 0) {
                setInfoMsg("Can't exceed Total: " +(Number( origrper )+ Number( ornetsale)));
                clearfields();

            }

            jQuery("#netarrears").val(0);

            var realgrval = Number(arre - totadj) + Number(grval);
            if (realgrval.toFixed(2) == "NaN") {
                jQuery("#grpsettl").val(0);
            } else {
                jQuery("#grpsettl").val(realgrval.toFixed(2));
            }

        } else {
            if (netarre.toFixed(2) == "NaN") {
                jQuery("#netarrears").val(0);
            } else {
                jQuery("#netarrears").val(netarre.toFixed(2));
            }
            jQuery("#grpsettl").val(origrper.toFixed(2));
            $("#totadjs").val(totadj.toFixed(2));

        }
    });
    jQuery("#dispute").on("input", function () {
        StartNumbers();
        var grval = jQuery("#grpsettl").val();
        var relese_month = jQuery("#dg1month").val();
        var hand = jQuery("#handovrrej").val();
        var totadj = AllTotAdj(Number(jQuery("#dispute").val()), Number(jQuery("#service").val()), Number(jQuery("#totadjs").val()), Number(jQuery("#lod").val()), Number(jQuery("#other").val()), Number(jQuery("#diriya").val()), Number(jQuery("#srcomp").val()), Number(grval), Number(relese_month), Number(hand));
        var arre = jQuery("#arramt").val();
        var netarre = arre - totadj;

        if (netarre < 0) {
            var realgrval = Number(arre - totadj) + Number(grval);
            if (realgrval < 0) {
                setInfoMsg("Can't exceed Total: " + (Number(origrper) + Number(ornetsale)));
                clearfields();

            }

            jQuery("#netarrears").val(0);

            var realgrval = Number(arre - totadj) + Number(grval);
            if (realgrval.toFixed(2) == "NaN") {
                jQuery("#grpsettl").val(0);
            } else {
                jQuery("#grpsettl").val(realgrval.toFixed(2));
            }

        } else {
            if (netarre.toFixed(2) == "NaN") {
                jQuery("#netarrears").val(0);
            } else {
                jQuery("#netarrears").val(netarre.toFixed(2));
            }
            jQuery("#grpsettl").val(origrper.toFixed(2));
            $("#totadjs").val(totadj.toFixed(2));

        }
    });
    jQuery("#srcomp").on("input", function () {
        StartNumbers();
        var grval = jQuery("#grpsettl").val();
        var relese_month = jQuery("#dg1month").val();
        var hand = jQuery("#handovrrej").val();
        var totadj = AllTotAdj(Number(jQuery("#dispute").val()), Number(jQuery("#service").val()), Number(jQuery("#totadjs").val()), Number(jQuery("#lod").val()), Number(jQuery("#other").val()), Number(jQuery("#diriya").val()), Number(jQuery("#srcomp").val()), Number(grval), Number(relese_month), Number(hand));
        var arre = jQuery("#arramt").val();
        var netarre = arre - totadj;

        if (netarre < 0) {
            var realgrval = Number(arre - totadj) + Number(grval);
            if (realgrval < 0) {
                setInfoMsg("Can't exceed Total: " + (Number(origrper) + Number(ornetsale)));
                clearfields();

            }

            jQuery("#netarrears").val(0);

            var realgrval = Number(arre - totadj) + Number(grval);
            if (realgrval.toFixed(2) == "NaN") {
                jQuery("#grpsettl").val(0);
            } else {
                jQuery("#grpsettl").val(realgrval.toFixed(2));
            }

        } else {
            if (netarre.toFixed(2) == "NaN") {
                jQuery("#netarrears").val(0);
            } else {
                jQuery("#netarrears").val(netarre.toFixed(2));
            }
            jQuery("#grpsettl").val(origrper.toFixed(2));

            $("#totadjs").val(totadj.toFixed(2));
        }
    });

    //$('#service').focusout(function () {
    //    fixnumbers();
    //});
  
    function clearfields()
    {
        jQuery("#accno").val("");
        jQuery("#gdate").val("");
        jQuery("#grpsettl").val("");
        jQuery("#arramt").val("");
        jQuery("#dispute").val("");
        jQuery("#service").val("");
        jQuery("#totadjs").val("");
        jQuery("#lod").val("");
        jQuery("#diriya").val("");
        jQuery("#other").val("");
        jQuery("#handovrrej").val("");
    }
    $('input#service').blur(function () {
        StartNumbers();
        var num = parseFloat($(this).val());
        var cleanNum = num.toFixed(2);
        if (cleanNum == "NaN") {
            cleanNum = 0;
        }
        if (Number(cleanNum) < 0) {
            cleanNum = 0;
        }
        $(this).val(cleanNum);
        fixnumbers();
    });
    $('input#totadjs').blur(function () {
        StartNumbers();
        var num = parseFloat($(this).val());
        var cleanNum = num.toFixed(2);
        if (cleanNum == "NaN") {
            cleanNum = 0;
        }
        if (Number(cleanNum) < 0) {
            cleanNum = 0;
        }
        $(this).val(cleanNum);
    });
    $('input#lod').blur(function () {
        StartNumbers();
        var num = parseFloat($(this).val());
        var cleanNum = num.toFixed(2);
        if (cleanNum == "NaN") {
            cleanNum = 0;
        }
        if (Number(cleanNum) < 0) {
            cleanNum = 0;
        }
        $(this).val(cleanNum);
        fixnumbers();
    });
    $('input#dispute').blur(function () {
        StartNumbers();
        var num = parseFloat($(this).val());
        var cleanNum = num.toFixed(2);
        if (cleanNum == "NaN") {
            cleanNum = 0;
        }
        if (Number(cleanNum) < 0) {
            cleanNum = 0;
        }
        $(this).val(cleanNum);
        fixnumbers();
    });
    $('input#diriya').blur(function () {
        StartNumbers();
        var num = parseFloat(Number( $(this).val()));
        var cleanNum = num.toFixed(2);
        if (cleanNum == "NaN") {
            cleanNum = 0;
        }
        if (Number(cleanNum) < 0) {
            cleanNum = 0;
        }
        $(this).val(cleanNum);
        fixnumbers();
    });
    $('input#other').blur(function () {
        StartNumbers();
        var num = parseFloat($(this).val());
        var cleanNum = num.toFixed(2);
        if (cleanNum == "NaN") {
            cleanNum = 0;
        }
        if (Number(cleanNum) < 0) {
            cleanNum = 0;
        }
        $(this).val(cleanNum);
        fixnumbers();
    });
    $('input#srcomp').blur(function () {
        StartNumbers();
        var num = parseFloat($(this).val());
        var cleanNum = num.toFixed(2);
        if (cleanNum == "NaN") {
            cleanNum = 0;
        }
        if (Number(cleanNum) < 0) {
            cleanNum = 0;
        }
        $(this).val(cleanNum);
        fixnumbers();
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
                        url: "/CollectionBonusAdjustments/SaveAdjDetails",
                        data: { pc: jQuery("#location").val(), month: jQuery("#monthdate").val(), accno: jQuery("#accno").val(), ngrsett: jQuery("#grpsettl").val(), realarr: jQuery("#arramt").val(), ndue: jQuery("#dg1month").val(), nser: jQuery("#service").val(), ntadj: jQuery("#totadjs").val(), nlod: jQuery("#lod").val(), ndisput: jQuery("#dispute").val(), netarr: jQuery("#netarrears").val(), ndiri: jQuery("#diriya").val(), nother: jQuery("#other").val(), srcomp: jQuery("#srcomp").val(), reson: jQuery("#reson").val(), effectdate: jQuery("#effectmonth").val() },
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    setSuccesssMsg(result.msg);
                                    viewalllist();
                                    clearfields();

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
    function CommasTOnumberWith(yourNumber) {
        //Seperates the components of the number
        var n = yourNumber.toString().split(",");
        if (n.length == 1) {
            yourNumber = yourNumber.toString();
        }
        n = yourNumber.toString().split(",");
        var num="";

        for (var i = 0 ; i < n.length ; i++)
        {
            num = num + n[i].toString();
        }
        return num;
    }
    function LoadReason() {
        jQuery.ajax({
            type: "GET",
            url: "/CollectionBonusAdjustments/LoadReason",
            data: { },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("reson");
                        jQuery("#reson").empty();
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
    //  LoadPriceLevel();
    //$('#reson').click(function () {
    //    LoadReason();
    //});
    //$('#reson').keypress(function (e) {
    //    if (e.which == 13) {
    //        LoadReason();
    //    }

    //});
    LoadReason();
});