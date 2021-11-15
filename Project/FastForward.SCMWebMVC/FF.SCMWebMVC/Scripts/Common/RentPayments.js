jQuery(document).ready(function () {
    var dynmicdiv = 0;
    loadcalendars();
    var columnIDList = [];
    var OwnerDetailsList = [];
    var MonthlyInsList = [];
    var MonthlyInsFromDate = [];
    var MonthlyInsToDate = [];

    //functions

    function loadcalendars() {
        jQuery('#rentfromdate').val(my_date_format(new Date()));
        jQuery('#rentfromdate').datepicker({ dateFormat: "dd/M/yy" });
        jQuery('#renttodate').val(my_date_format(new Date()));
        jQuery('#renttodate').datepicker({ dateFormat: "dd/M/yy" });
        jQuery('#advancefromdate').val(my_date_format(new Date()));
        jQuery('#advancefromdate').datepicker({ dateFormat: "dd/M/yy" });
        jQuery('#advancetodate').val(my_date_format(new Date()));
        jQuery('#advancetodate').datepicker({ dateFormat: "dd/M/yy" });
    }

    function clear() {
        $('#rentdts').val("");
        $('#paymenttype').val("");
        $('#paymensubttype').val("");
        $('#pft').val("");
        $('#creditacc').val("");
        $('#debitacc').val("");
        $('#agreementref').val("");
        $('#addresse1').val("");
        $('#addresse2').val("");
        $('#sqfeet').val("");
        $('#remark').val("");
        $('#district').val("");
        $('#provision').val("");
        $('#sqfeet').val("");
        $('#terminatewithdate').prop('checked', false);
        $('#stoppaymentwithdate').prop('checked', false);
        loadcalendars();
        $('#ownerdetailstbl').empty();
        $('#ownerdetailstbl').empty();
        $('#schedule_table').empty();
        $('.dynamiccolumns').empty();

    }


    function addtownertbl() {
        var max = 0;
        $('.deletedynamiccol').each(function () {
            if (this.id > max) {
                max = this.id;
            }
        })
        max = max;
        var formdata = jQuery("#schownerdet-frm").serialize();
        var schid = jQuery('#rentdts').val();
        jQuery.ajax({
            type: "POST",
            url: "/RentPayments/OwnerDetailsManagement",
            data: formdata + "&scheduleid=" + schid + "&recid=" + max,
            dataType: "json",
            success: function (result) {
            }
            , error: function (data) { console.log(data) }
        });
    }

    profitcenterFocusout();
    function profitcenterFocusout() {
        $('#pft').focusout(function () {
            var pc = $(this).val();
            jQuery.ajax({
                cache: false,
                type: "GET",
                url: "/Search/getProfitCenters",
                data: { pgeNum: "1", pgeSize: "10", searchFld: "Code", searchVal: pc },
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.data.length > 0) {
                            }
                        } else {
                            setInfoMsg('Invalid Profi Center!!');
                            jQuery("#ProfitCenter").val("");
                            jQuery("#ProfitCenter").focus();
                            return;
                        }
                    } else {
                        Logout();
                    }
                }
            });
        });
    }

    function getTexboxesColumns() {
        var ctype = $('#paymenttype').val();
        $('.dynamiccolumns').empty();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/RentPayments/getSCHColumns",
            data: { p_type: ctype },
            success: function (result) {
                var columns = "";
                var tableheaders = "<tr class='table-hed'><th>No</th>";
                for (var x = 0; x < result.data.length; x++) {
                    columns = columns + '<div class="col-md-2">' + result.data[x].psc_name + '</div>' + '<div class="col-md-2 dynamictexbox"><input type="text" name=' + result.data[x].psi_hed_cd + '-' + result.data[x].psi_col_seq + ' class="form-control with-search"></div>';
                    tableheaders = tableheaders + "<th>" + result.data[x].psc_name + "</th>";
                }
                tableheaders += '<th></th>';
                tableheaders += '<th></th></tr>';

                $('.dynamiccolumns').append(columns);
                //$('.dynamiccolumns').append("<div class=row><button class='addtoownertable'>Add</button></div>");
                $('#ownerdetailstbl').append(tableheaders);
            },
            error: function (data) {
                console.log(data);
            },

        });
    }

    function getOwnerDetails() {
        $('#ownerdetailstbl').empty();
        var tdata = "";
        var tfinal = "";
        var incre = 1;
        var recid = 0;
        var recid = "";
        var schid = $('#rentdts').val();
        var p_type = $('#paymenttype').val();
        jQuery.ajax({
            type: "GET",
            url: "/RentPayments/getSCHOwnerDetails",
            data: { p_schid: schid, p_type: p_type },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                getTexboxesColumns();
                jQuery.ajax({
                    cache: false,
                    type: "GET",
                    url: "/RentPayments/getRecordidcounts",
                    dataType: "json",
                    success: function (result1) {
                        for (var x = 0; x < result1.data.length; x++) {
                            recid = result1.data[x].psa_rec_id;
                            jQuery.ajax({
                                cache: false,
                                type: "GET",
                                url: "/RentPayments/getRecordbyid",
                                data: { recordid: recid },
                                dataType: "json",
                                success: function (result3) {

                                    var tdata = '<tr><td>' + incre++ + '</td>';
                                    for (var y = 0; y < result3.data.length; y++) {
                                        tdata = tdata + '<td>' + result3.data[y].psa_value + '</td>';
                                        recid = result3.data[y].psa_rec_id;
                                    }
                                    tdata = tdata + '<td><button id=' + recid + ' class="deletedynamiccol">Delete</button></td>';
                                    tdata = tdata + '<td><button id=' + recid + ' class="updatedynamiccol">Update</button></td>' + '</tr>';
                                    $('#ownerdetailstbl tr:last').after(tdata);
                                }
                            });
                        }
                        console.log(tfinal);
                    }
                });
            }
            , error: function (data) { console.log(data) }
        });
    }

    function tablecolumns() {
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/RentPayments/getColumns",
            dataType: "json",
            success: function (result) {
            }
        });

    }


    function Save_Rent_Details() {
        var formdata = jQuery("#rentdetails-frm").serialize();
        var rentfrmdate = $('#rentfromdate').val();
        var renttodate = $('#renttodate').val();
        jQuery.ajax({
            type: "POST",
            url: "/RentPayments/SAVE_RENT_PAYMENT_DETAILS",
            data: formdata + "&rentfromdt=" + rentfrmdate + "&renttodt=" + renttodate,
            dataType: "json",
            success: function (result) {
            }
            , error: function (data) { console.log(data) }
        });
    }

    function Get_Schedule_Details() {
        var formdata = jQuery("#rentdetails-frm").serialize();
        var rentfrmdate = $('#rentfromdate').val();
        var renttodate = $('#renttodate').val();
        jQuery.ajax({
            type: "POST",
            url: "/RentPayments/SAVE_RENT_PAYMENT_DETAILS",
            data: formdata + "&rentfromdt=" + rentfrmdate + "&renttodt=" + renttodate,
            dataType: "json",
            success: function (result) {
            }
            , error: function (data) { console.log(data) }
        });
    }


    //clicks

    $('.dynamicadd').click(function () {

        var div = "<div class='row dynamicclass' id=" + dynmicdiv + "> <div class='col-md-12'><div class='col-md-2'>Monthly Installement</div>" +
                   "<div class='col-md-2'><input type='text' id='monthlyinstallement' name='monthlyinstallement' class='form-control with-search'></div>" +
                   "<div class='col-md-1'></div><div class='col-md-1'>Ins Period</div><div class='col-md-2'><input type='text' name='date' class='form-control insfromdate' readonly></div>" +
                   "<div class='col-md-1'>to</div><div class='col-md-2'><input type='text' name='date' class='form-control instodate' readonly></div></div></div>";

        $('#dynamicinstallements').append(div);

        $('#dynamicinstallements').each(function () {
            jQuery('.instodate').val(my_date_format(new Date()));
            jQuery('.instodate').datepicker({ dateFormat: "dd/M/yy" });
            jQuery('.insfromdate').val(my_date_format(new Date()));
            jQuery('.insfromdate').datepicker({ dateFormat: "dd/M/yy" });

        });

        dynmicdiv = dynmicdiv + 1;

        $('#dynamicinstallements').find('.insfromdate:first').val($('#rentfromdate').val());
        $('#dynamicinstallements').find('.instodate:last').val($('#renttodate').val());



        $('#dynamicinstallements').each(function () {
            console.log('asd');
            $('#dynamicinstallements').find('.insfromdate').attr('disabled', false);
            $('#dynamicinstallements').find('.instodate').attr('disabled', false);
        });
        $('#dynamicinstallements').find('.insfromdate:first').attr('disabled', true);
        $('#dynamicinstallements').find('.instodate:last').attr('disabled', true);

    });

    $('#addtoownertbl').click(function () {
        var val = 0;
        var incre = 1;
        var ownername = $('#ownername').val();
        var ownernic = $('#ownernic').val();
        var owneraddress = $('#owneradd').val();
        var ownerbankaccno = $('#ownerbankacc').val();
        var ownerbankcode = $('#ownerbankcode').val();
        var ownervatno = $('#ownervat').val();

        if (ownername == "") {
            status = "Please enter a name";
            setInfoMsg(status);
            return;
            vali = 1;
        }
        if (ownernic == "") {
            status = "Please enter a NIC";
            setInfoMsg(status);
            return;
            vali = 1;
        }
        var items = [];
        if (val == 0) {
            jQuery('#ownerdetailstbl').append('<tr><td>' + incre + '</td>' +
           '<td>' + ownername + '</td>' +
           '<td>' + ownernic + '</td>' +
           '<td>' + owneraddress + '</td>' +
           '<td>' + ownerbankaccno + '</td>' +
           '<td>' + ownerbankcode + '</td>' +
           '<td>' + ownervatno + '</td>' +
           '<td>' + '<button class="btn btn-primary btn-xs floatright deleteowner" value="Delete" id="' + incre + '">Add</button>' + '</td>' +
           '</tr>')
            incre++;
        }
        $('#ownername').val("");
        $('#ownernic').val("");
        $('#owneradd').val("");
        $('#ownerbankacc').val("");
        $('#ownerbankcode').val("");
        $('#ownervat').val("");
    });


    jQuery("body").on('click', '.deleteowner', function () {
        var id = this.id;
        $("#ownerdetailstbl tr").eq(id).remove();
    });

    $('.dynamicremove').click(function () {
        $('body').find('.dynamicclass:last').remove();
    });

    $('.pft-search').click(function () {

        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description", "Address", "Chanel"];
        field = "ProfitCentersRent"
        var x = new CommonSearch(headerKeys, field);

    });

    $('#advancefromdate').change(function () {
        var advancefrmdt = $('#advancefromdate').val();
        var rentperifrom = $('#rentfromdate').val();
        if (Date.parse(advancefrmdt) < Date.parse(rentperifrom)) {
            $('#advancefromdate').val(rentperifrom);
        }
    });


    $('#advancetodate').change(function () {
        var advanctodt = $('#advancetodate').val();
        var rentperito = $('#renttodate').val();
        if (Date.parse(advanctodt) > Date.parse(rentperito)) {
            $('#advancetodate').val(rentperito);
        }
    });

    $('#dynamicinstallements').find('.insfromdate:first').change(function () {

        alert();

    });


    $('.btn-save-data').click(function () {
        var validation = 0;
        var paymenttype = $('#paymenttype').val();
        var paymensubttype = $('#paymensubttype').val();
        var profitcenter = $('#pft').val();
        var addresse1 = $('#addresse1').val();
        var creditacc = $('#creditacc').val();
        var debitacc = $('#debitacc').val();
        var agreementref = $('#agreementref').val();

        if (paymenttype == "") {
            validation = 1;
            status = "Payment type cannot be empty";
            setInfoMsg(status);
            return;
        }
        if (paymensubttype == "") {
            validation = 1;
            status = "Payment subtype cannot be empty";
            setInfoMsg(status);
            return;
        }
        if (profitcenter == "") {
            validation = 1;
            status = "Profit center cannot be empty";
            setInfoMsg(status);
            return;
        }

        if (addresse1 == "") {
            validation = 1;
            status = "Address cannot be empty";
            setInfoMsg(status);
            return;
        }
        if (creditacc == "") {
            validation = 1;
            status = "Credit account cannot be empty";
            setInfoMsg(status);
            return;
        }
        if (debitacc == "") {
            validation = 1;
            status = "Debit account cannot be empty";
            setInfoMsg(status);
            return;
        }
        if (agreementref == "") {
            validation = 1;
            status = "Agreement Ref cannot be empty";
            setInfoMsg(status);
            return;
        }

        if (validation == 0) {
            Lobibox.confirm({
                msg: "Are you sure ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        Save_Rent_Details();
                        clear();
                        loadcalendars(); 
                        $('#rentdts').attr('readonly', false);
                        $('#paymenttype').attr('readonly', false);
                        $('#paymensubttype').attr('readonly', false);
                        status = "Succesfully Updated";
                        setInfoMsg(status);
                        return;
                    }
                }
            });
        }
    });



    $('.rendshd-search').click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "ID", "Profitcenter"];
        field = "RentHeaders"
        var x = new CommonSearch(headerKeys, field);

    });

    $('.payment-type-search').click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "ID", "Description"];
        field = "PaymentTypes"
        var x = new CommonSearch(headerKeys, field);
    });

    $('.payment-subtype-search').click(function () {
        var type = $("#paymenttype").val();
        var data = { p_type: type };
        var headerKeys = Array()
        headerKeys = ["Row", "ID", "Description"];
        field = "PaymentSubTypes"
        var x = new CommonSearch(headerKeys, field, data);
    });

    $('#deleteownertbl').click(function () {
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/RentPayments/getColumns",
            dataType: "json",
            success: function (result) {
            }
        });
    });

    $('#paymenttype').focusout(function () {

        var schid = $('#rentdts').val();
        if (schid == "") {
            $('#ownerdetailstbl').empty();
            getTexboxesColumns();
        }
    });


    jQuery("body").on('click', '.addtoownertable', function (e) {
        e.preventDefault();
        var max = 0;
        $('.deletedynamiccol').each(function () {
            if (this.id > max) {
                max = this.id;

            }
        })

        max = parseInt(max) + 1;
        console.log(max);
        var rowCount = $('#ownerdetailstbl tr').length;
        var tbdata = "<td>" + (rowCount) + "</td>";
        var columns = $('.dynamiccolumns input[type=text]').each(function () {
            $xval = $(this);
            tbdata = tbdata + '<td name="' + rowCount + '' + $xval[0].value + '">' + $xval[0].value + '</td>';
        });
        tbdata += '<td><button id=' + max + ' class="deletedynamiccol">Delete</button></td>';
        tbdata += '<td><button id=' + max + ' class="Updatedynamiccol">Update</button></td>';
        tbdata = '<tr>' + tbdata + '</tr>';
        jQuery('#ownerdetailstbl').append(tbdata);

        addtownertbl();
    });



    jQuery("body").on('click', '.deletedynamiccol', function (e) {
        e.preventDefault();
        var schcode = jQuery('#rentdts').val();
        var p_psa_hed_cd_c = jQuery('#paymenttype').val();
        var recordid = this.id;

        $xval = $(this);
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/RentPayments/DeleteRecord",
            data: { recordid: this.id },
            dataType: "json",
            success: function (result) {
                jQuery.ajax({
                    cache: false,
                    type: "GET",
                    url: "/RentPayments/RemoveSCHOwnerDetails",
                    data: { p_psa_fld_cd: schcode, p_psa_hed_cd: p_psa_hed_cd_c, p_psa_rec_id: recordid },
                    dataType: "json",
                    success: function (result) {
                    }
                });
            }
        });
        $(this).closest("tr").remove();
    });

    jQuery("body").on('click', '.updatedynamiccol', function (e) {
        var recordid = this.id;
        e.preventDefault();
        var colid = "";
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/RentPayments/getColumns",
            dataType: "json",
            success: function (result) {
                console.log(result);
                jQuery.ajax({
                    cache: false,
                    type: "GET",
                    url: "/RentPayments/GetOwnerRecordDetails",
                    data: { recordid: recordid },
                    dataType: "json",
                    success: function (result1) {
                        console.log(result1);
                        for (var x = 0; x < result.data.length; x++) {
                            coldid = result.data[x].psi_hed_cd + '-' + result.data[x].psi_col_seq;
                            console.log(coldid);

                            var gg = 'input[name="' + coldid + '"]';
                            jQuery(gg).val(result1.data[x].psa_value);
                        }
                    }
                });
            }
        });
        $(this).closest("tr").remove();
    });

    jQuery('#rentdts').focusout(function () {
        getOwnerDetails();
        getScheduleDetails();
    });


    function getScheduleDetails() {
        $("#schedule_table").find("tr:not(:first)").remove();
        var psh_no = jQuery('#rentdts').val();
        var tdata = "";
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/RentPayments/GetScheduleDetails",
            data: { p_psh_no: psh_no },
            dataType: "json",
            success: function (result) {
                for (var x = 0; x < result.data.length; x++) {
                    tdata += "<tr>";
                    tdata = tdata + "<td>" + result.data[x].psd_line+ "</td>" + "<td>" + result.data[x].psd_due + "</td>" + "<td>" + result.data[x].psd_amt + "</td>" +
                        "<td>" + result.data[x].psd_ded_amt + "</td>" + "<td>" + result.data[x].psd_net_amt + "</td>" + "<td>" + result.data[x].psd_pay_amt + "</td>";
                    tdata += "</tr>";
                }

                jQuery('#schedule_table').append(tdata);
            }
        });
    }


    jQuery('.btn-approve-data').click(function () {
        if (jQuery('#rentdts').val() != "") {
            jQuery.ajax({
                cache: false,
                type: "GET",
                url: "/RentPayments/ApproveRentPayment",
                dataType: "json",
                success: function (result) {

                    if (result.msg == "1") {
                        var schno = $('#rentdts').val();
                        jQuery.ajax({
                            cache: false,
                            type: "GET",
                            url: "/RentPayments/UpdateSCHStatus",
                            data: { p_psh_no: schno },
                            dataType: "json",
                            success: function (result) {
                                setInfoMsg("Succesfully Approved");
                                return;
                            }
                        });
                    }
                    else{
                    setInfoMsg(result.msg);
                    return;
                }
                }
                , error: function (data) { console.log(data) }
            });
        } else {
            setInfoMsg("Please select a valid Schedule ID");
            return;
        }
    })

    jQuery('#addtoshedule').click(function () {
        $("#schedule_table").find("tr:not(:first)").remove();

        var MonthlyInsList = [];
        var MonthlyInsFromDate = [];
        var MonthlyInsToDate = [];

        $('.dynamicclass').find('#monthlyinstallement').each(function (index, element) {

            MonthlyInsList.push($(element).val());
        })

        $('.dynamicclass').find('.insfromdate').each(function (index, element) {

            MonthlyInsFromDate.push($(element).val());
        })

        $('.dynamicclass').find('.instodate').each(function (index, element) {

            MonthlyInsToDate.push($(element).val());
        })

        console.log(MonthlyInsList);
        console.log(MonthlyInsFromDate);
        console.log(MonthlyInsToDate);

        var prentfromdate = $('#rentfromdate').val();
        var prenttodate = $('#renttodate').val();
        var padvancedamout = $('#advancedamt').val();
        var padvancedfromdate = $('#advancefromdate').val();
        var padvancedtodate = $('#advancetodate').val();
        var tdata = "";
        jQuery.ajax({
            cache: false,
            type: "GET",
            traditional: true,
            url: "/RentPayments/CrtShedule",
            data: { rentfromdate: prentfromdate, renttodate: prenttodate, advancedamout: padvancedamout, advancedfromdate: padvancedfromdate, advancedtodate: padvancedtodate, MonthlyInsList: MonthlyInsList, MonthlyInsFromDate: MonthlyInsFromDate, MonthlyInsToDate: MonthlyInsToDate, },
            dataType: "json",
            success: function (result) {

                console.log(result);
                for (var x = 0; x < result.data.length; x++) {
                    tdata += "<tr>";
                    tdata = tdata + "<td>" + (x + 1) + "</td>" + "<td>" + result.data[x].psd_due + "</td>" + "<td>" + result.data[x].psd_amt + "</td>" +
                        "<td>" + result.data[x].psd_ded_amt + "</td>" + "<td>" + result.data[x].psd_net_amt + "</td><td>" + result.data[x].psd_pay_amt + "</td>";
                    tdata += "</tr>";
                }
                jQuery('#schedule_table').append(tdata);
            }
        });


    });

});