jQuery(document).ready(function () {

    //jQuery('#Jb_jb_dt').val(my_date_format_with_time(new Date()));
    jQuery('#Jb_jb_dt').val(my_date_format_tran(my_date_format_with_time(new Date()).toString()));
   // jQuery('#Jb_jb_dt').datepicker({ dateFormat: "dd/M/yy" })
    LoadPendingJobRequest();
    function LoadPendingJobRequest() {
        jQuery.ajax({
            type: "GET",
            url: "/JobDefinition/LoadPendingJobRequse",
            data: {},
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        SetPendingJobGrid(result.data);
                    } else {
                        setError(result.msg);
                    }
                } else {
                    Logout();
                }
            }
        })
    }
    function SetPendingJobGrid(data) {
        jQuery('.pending-requset .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {
                jQuery('.pending-requset').append('<tr class="new-row">' +
                      '<td>' + data[i].rq_no + '</td>' +
                        '<td>' + data[i].rq_pouch_no + '</td>' +
                          '<td>' + getFormatedDate1(data[i].rq_dt) + '</td>' +
                           '<td>' + data[i].rq_rmk + '</td>' +
                       '<td style="text-align:center;"><input type="checkbox" id="chkreq" name="chkreq" class="chk-pen-req" /></td>' +
                        '</tr>');
            }
            CheckPendingListFunction();
        }
    }
    function CheckPendingListFunction() {
        jQuery(".chk-pen-req").unbind('checked').change(function (evt) {
            evt.preventDefault();
            var consoles = 0;
            if ($("#console").is(':checked') == true) {
                consoles = "1";
            }

            var td = jQuery(this).parent('td');
            var tr = jQuery(td).parent('tr');
            var ReqNo = jQuery(tr).find('td:eq(0)').html();
            var PerNo = jQuery(tr).find('td:eq(1)').html();
                        jQuery.ajax({
                            type: "GET",
                            url: "/JobDefinition/CheckChangeRequest",
                            data: { ReqNo: ReqNo, PerNo: PerNo, consoles: consoles },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true)
                                    {
                                    } else {
                                        setInfoMsg(result.msg);
                                        ClearPage();
                                        LoadPendingJobRequest();
                                    }
                                } else {
                                    Logout();
                                }
                            }
                        });
                   
        });
    }
    function SetJobServiceGrid(data) {
        jQuery('.job-services .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {
                jQuery('.job-services').append('<tr class="new-row">' +
                      '<td>' + data[i].fms_ser_cd + '</td>' +
                       '<td>' + data[i].fms_ser_desc + '</td>' +
                       '<td style="text-align:center;"><input type="checkbox" id="chkservice" name="chkservice" class="chk-ser-det" /></td>' +
                        '</tr>');
            }
            ServiceCheckChangeFunction();
        }
    }
    function ServiceCheckChangeFunction() {
        jQuery(".chk-ser-det").unbind('checked').change(function (evt) {
            evt.preventDefault();
            var td = jQuery(this).parent('td');
            var tr = jQuery(td).parent('tr');
            var Code = jQuery(tr).find('td:eq(0)').html();
            var Desc = jQuery(tr).find('td:eq(1)').html();
            jQuery.ajax({
                type: "GET",
                url: "/JobDefinition/CheckChangeService",
                data: { Code: Code, Desc: Desc },
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

        });
    }
    LoadJobservice();
    function LoadJobservice() {
        jQuery.ajax({
            type: "GET",
            url: "/JobDefinition/LoadJobServices",
            data: {},
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        SetJobServiceGrid(result.data);
                    } else {
                        setError(result.msg);
                    }
                } else {
                    Logout();
                }
            }
        })
    }
   // LoadJobStatus();
    LoadEntityTypes();
    function LoadJobStatus() {
        jQuery.ajax({
            type: "GET",
            url: "/JobDefinition/LoadJobStatus",
            data: {},
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("Jb_stus");
                        jQuery("#Jb_stus").empty();
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
    function LoadEntityTypes() {
        jQuery.ajax({
            type: "GET",
            url: "/JobDefinition/LoadEntityTypes",
            data: {},
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("en_type");
                        jQuery("#en_type").empty();
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
    function getFormatedDate1(date) {
        var dte = new Date(parseInt(date.substr(6)));
        if (my_date_format_tran(dte) != "NaN/undefined/NaN")
            return my_date_format_tran(dte);
    }
    jQuery('.btn-save-jobser').click(function (e) {

        if (jQuery("#Jb_jb_dt").val() =="")
        {
            setInfoMsg("Please Select Date");
            return;
        }
      
        if ($('.cus-data-list tr').length < 2) {
          
            setInfoMsg("Please add customer details");
            return;
        }
       
        Lobibox.confirm({
            msg: "Do you want to continue process?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    var check = "";
                    var formdata = jQuery("#job-data").serialize();
                    jQuery.ajax({
                        type: "GET",
                        url: "/JobDefinition/SaveJobDefinition",
                        data: formdata + "&PendingReq=" + "",
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    setSuccesssMsg(result.msg);
                                    //clear page
                                    ClearPage();

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
    function ClearPage()
    {
        jQuery("#Jb_pouch_no").val("");
        jQuery("#Jb_jb_dt").val("");
        //jQuery("#Jb_stus").val("Pending");
        jQuery("#Jb_rmk").val("");
        jQuery("#Jb_pouch_no").val("");
        jQuery("#Jb_pouch_no").val("");
        jQuery("#Jb_pouch_no").val("");
        jQuery("#Jb_pouch_no").val("");
        jQuery("#Jb_jb_no").val("");
        jQuery("#Jb_sales_ex_cd").val("");
        
       //exam  dgfg
        jQuery('#Jb_jb_dt').val(my_date_format_with_time(new Date()));
        jQuery.ajax({
            type: "GET",
            url: "/JobDefinition/ClearSession",
        });
        jQuery('.cus-data-list .new-row').remove();
        LoadEntityTypes();
        LoadJobservice();
        LoadPendingJobRequest();
    }
    jQuery(".btn-cust_search").click(function () {
        //alert("d");
        var headerKeys = Array()
        headerKeys = ["Row", "Name", "Code", "NIC", "Mobile", "BR No"];
        //field = "cusCode2"
        field = "cusCodetype"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery("#cus_cd").on("keydown", function (evt) {
        //alert("d");
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Name", "Code", "NIC", "Mobile", "BR No"];
            field = "cusCode2"
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery(".search-job-no").click(function () {
        //var headerKeys = Array()
        //headerKeys = ["Row", "Code", "Date", "Status"];
        //field = "jobno"
        //var x = new CommonSearch(headerKeys, field);
        var headerKeys = Array()
        headerKeys = ["Row", "Job No", "Pouch No", "Date", "Status"];
        field = "jobdefno"
        var x = new CommonSearchDateFiltered(headerKeys, field);
    });
    jQuery("#Jb_jb_no").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            //var headerKeys = Array()
            //headerKeys = ["Row", "Code", "Date", "Status"];
            //field = "jobno"
            //var x = new CommonSearch(headerKeys, field);
            var headerKeys = Array()
            headerKeys = ["Row", "Job No", "Pouch No", "Date", "Status"];
            field = "jobdefno"
            var x = new CommonSearchDateFiltered(headerKeys, field);
        }
    });
    jQuery(".btn-pouch-no").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Pouch No", "Job No", "Date"];
        field = "pouchnojobdef"
        var x = new CommonSearchDateFiltered(headerKeys, field);
    });
    jQuery("#Jb_pouch_no").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Pouch No", "Job No", "Date"];
            field = "pouchnojobdef"
            var x = new CommonSearchDateFiltered(headerKeys, field);
        }
    });
    jQuery(".curency-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description"];
        field = "curcd3";
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery("#dealer_currncy").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description"];
            field = "curcd3";
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery(".btn-exec-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Epf No", "Category", "First Name", "Last Name", "NIC"];
        field = "employee3";
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery("#Jb_sales_ex_cd").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Epf No", "Category", "First Name", "Last Name", "NIC"];
            field = "employee3";
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery(".cntry-cd-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description", "Region Code", "Capital"];
        field = "presentCountry";
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery("#country").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Code", "Description", "Region Code", "Capital"];
            field = "presentCountry";
            var x = new CommonSearch(headerKeys, field);
        }
    });
    jQuery(".per-town-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Town", "District", "Province", "Code"];
        field = "perTown"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery("#town").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Town", "District", "Province", "Code"];
            field = "perTown"
            var x = new CommonSearch(headerKeys, field);
        }
    });
    $('#cus_cd').focusout(function () {
        var pc = $(this).val();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/JobDefinition/LoadCustormerDetails",
            data: { CustormerCode: jQuery("#cus_cd").val() },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.data.length > 0) {
                            // set custormer field
                            SetCustormerData(result.data);
                        } else {
                            setInfoMsg('Please Enter Correct Custormer');
                            ClearCustormerData();
                        }
                    } else {
                        setInfoMsg('No Data Found! Please Check Code!!');
                        return;
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    $('#Jb_jb_no').focusout(function () {
        var pc = $(this).val();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/JobDefinition/LoadAlldata",
            data: { Job: jQuery("#Jb_jb_no").val() },
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.details.length > 0) {
                           
                            // set custormer field
                            SetCustormerDataList(result.details);
                        }
                        
                        if (result.hdrdata.length > 0) {
                            // set hdr field
                            SetHDRData(result.hdrdata);
                        } else {
                            if (jQuery("#Jb_jb_no").val() != "" && jQuery("#Jb_jb_no").val() != null) {
                                setInfoMsg('Invalid job Number');
                            }                            
                            jQuery("#Jb_jb_no").val("");
                        }
                    } else {
                        setInfoMsg('No Data Found! Please Check Code!!');
                        return;
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    $('#Jb_pouch_no').focusout(function () {
       
        var pc = $(this).val();
        jQuery.ajax({
            cache: false,
            type: "GET",
            url: "/JobDefinition/LoadAlldataByPouch",
            data: { pouch: jQuery("#Jb_pouch_no").val() },
            success: function (result) {
               
                if (result.login == true) {
                    if (result.success == true) {
                        if (result.details.length > 0) {
                            // set custormer field
                            SetCustormerDataList(result.details);
                        }
                       
                        if (result.hdrdata.length > 0) {
                            // set hdr field
                            SetHDRData(result.hdrdata);
                        } else {
                            if (jQuery("#Jb_pouch_no").val() != "" && jQuery("#Jb_pouch_no").val() != null) {
                                setInfoMsg('Invalid pouch Number');
                            }                            
                            jQuery("#Jb_pouch_no").val("");
                        }
                    } else {
                        setInfoMsg('No Data Found! Please Check Code!!');
                        return;
                    }
                } else {
                    Logout();
                }
            }
        });
    });
    function SetHDRData(data)
    {
        jQuery("#Jb_jb_no").val(data[0].Jb_jb_no);
        jQuery("#Jb_pouch_no").val(data[0].Jb_pouch_no);
        //if (data[0].Jb_stus=="A")
        //{
        //    jQuery("#Jb_stus").val("Approved");
        //}
        //if (data[0].Jb_stus == "P") {
        //    jQuery("#Jb_stus").val("Pending");
        //}
        //if (data[0].Jb_stus == "C") {
        //    jQuery("#Jb_stus").val("Cancel");
        //}
        
        jQuery("#Jb_rmk").val(data[0].Jb_rmk);
        jQuery("#Jb_jb_dt").val(getFormatedDate1(data[0].Jb_jb_dt));
        if (data[0].Jb_stus == "A" || data[0].Jb_stus=="C")
        {
            jQuery(".btn-save-jobser").hide();
        } else {
            jQuery(".btn-save-jobser").show();
        }

    }
    function SetCustormerData(data)
    {
        //alert("d");
        jQuery("#tin_cd").val(data[0].MBE_OTH_ID_NO);
        jQuery("#cuss_name").val(data[0].MBE_NAME);
        jQuery("#cus_addr1").val(data[0].MBE_ADD1);
        jQuery("#cus_addr2").val(data[0].MBE_ADD2);
        jQuery("#email").val(data[0].MBE_EMAIL);
        jQuery("#country").val(data[0].MBE_COUNTRY_CD);
        jQuery("#province").val(data[0].MBE_PROVINCE_CD);
        jQuery("#distric").val(data[0].MBE_DISTRIC_CD);
        jQuery("#Telephone").val(data[0].MBE_TEL);
        jQuery("#Fax").val(data[0].MBE_FAX);
        jQuery("#Jb_sales_ex_cd").val(data[0].MBE_ACC_CD);
        
        
    }
    function ClearCustormerData() {
        jQuery("#tin_cd").val("");
        jQuery("#cuss_name").val("");
        jQuery("#cus_addr1").val("");
        jQuery("#cus_addr2").val("");
        jQuery("#email").val("");
        jQuery("#country").val("");
        jQuery("#province").val("");
        jQuery("#distric").val("");
        jQuery("#Telephone").val("");
        jQuery("#Fax").val("");
        jQuery("#cus_cd").val("");

    }
    jQuery('.add-cus-list').click(function (e) {

       
        if (jQuery("#cus_cd").val() == "") {
            setInfoMsg("Please Select Customer");
            return;
        }
        Lobibox.confirm({
            msg: "Do you want to continue process?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    jQuery.ajax({
                        type: "GET",
                        url: "/JobDefinition/AddCustomer",
                        data: { CustormerCode: jQuery("#cus_cd").val(), CustomerType: jQuery("#en_type").val(), Exec: jQuery("#Jb_sales_ex_cd").val(), Name: jQuery("#cuss_name").val() },
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            if (result.login == true) {
                                if (result.success == true) {
                                    SetCustormerDataList(result.data);
                                    jQuery("#cus_cd").val("");
                                    jQuery("#tin_cd").val("");
                                    jQuery("#dealer_currncy").val("");
                                    jQuery("#cuss_name").val("");
                                    jQuery("#Jb_sales_ex_cd").val("");
                                    jQuery("#country").val("");
                                    jQuery("#cus_addr1").val("");
                                    jQuery("#province").val("");
                                    jQuery("#distric").val("");
                                    jQuery("#cus_addr2").val("");
                                    jQuery("#Telephone").val("");
                                    jQuery("#Fax").val("");
                                    jQuery("#email").val("");
                                } else {
                                    if (result.type == "Error") {
                                        setError(result.msg);

                                    }
                                    if (result.type == "Info") {
                                        setInfoMsg(result.msg);
                                    }
                                    if (result.notice==true)
                                    {
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
    function SetCustormerDataList(data) {
        jQuery('.cus-data-list .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {
                jQuery('.cus-data-list').append('<tr class="new-row">' +
                      '<td>' + data[i].JS_CUS_CD + '</td>' +
                       '<td>' + data[i].JS_SER_TP + '</td>' +
                        '<td>' + data[i].JS_PC + '</td>' +
                         '<td>' + data[i].Name + '</td>' +
                       '<td>' +' <input type="button" class="btn btn-sm btn-ash-fullbg remove-cus-list" value="Remove" />' +'</td>' +
                        '</tr>');
            }
            RemoveDetailsFunction();
        }
    }
    function RemoveDetailsFunction() {
        jQuery(".remove-cus-list").unbind('click').click(function (evt) {
            evt.preventDefault();
            var td = jQuery(this).parent('td');
            var tr = jQuery(td).parent('tr');
            var Code = jQuery(tr).find('td:eq(0)').html();
            var Type = jQuery(tr).find('td:eq(1)').html();
            Lobibox.confirm({
                msg: "Do you want to remove ?",
                callback: function ($this, type, ev) {
                    if (type == "yes") {
                        jQuery.ajax({
                            type: "GET",
                            url: "/JobDefinition/RemoveJobDet",
                            data: { Custormer: Code, Type: Type },
                            contentType: "application/json;charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                if (result.login == true) {
                                    if (result.success == true) {
                                        SetCustormerDataList(result.data);
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
    jQuery(".btn-clr-data").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to clear data ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    window.location.href = "/JobDefinition";
                }
            }
        });

    });
    jQuery("#dealer_currncy").focusout(function () {
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
                        jQuery("#dealer_currncy").val("");
                        jQuery("#dealer_currncy").focus();
                    }
                    if (result.success == true) {
                        if (result.data.MCR_CD == null) {
                            setInfoMsg("Please enter valid Currency code.");
                            jQuery("#dealer_currncy").val("");
                            jQuery("#dealer_currncy").focus();
                        } else {
                            //jQuery("#Sah_currency").val("");
                            //loadExchageRate(code);
                        }
                    }
                }
            });

        }
    }
    jQuery("#Jb_sales_ex_cd").focusout(function () {
        var code = jQuery(this).val();
        ExcfocusOut(code);
    });
    function ExcfocusOut(code) {
        if (code != "") {
            jQuery.ajax({
                type: "GET",
                url: "/Validation/validateEmployeeByCode?empCd=" + code,
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.success == false) {
                        setInfoMsg(result.msg);
                        jQuery("#Jb_sales_ex_cd").val("");
                        jQuery("#Jb_sales_ex_cd").focus();
                    }
                    if (result.success == true) {
                        if (result.data.ESEP_EPF == null) {
                            setInfoMsg("Please enter valid sales executive code.");
                            jQuery("#Jb_sales_ex_cd").val("");
                            jQuery("#Jb_sales_ex_cd").focus();
                        } else {
                            //jQuery("#Sah_currency").val("");
                            //loadExchageRate(code);
                        }
                    }
                }
            });

        }
    }
    jQuery("#country").focusout(function () {
        if (jQuery(this).val() != "") {
            validatePreCountry(jQuery(this).val());
        }
    });

    function validatePreCountry(code) {
        if (code != "") {
            jQuery.ajax({
                type: "GET",
                url: "/Validation/validateCountry",
                contentType: "application/json;charset=utf-8",
                data: { country: code },
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == false) {
                            if (result.type == "Error") {
                                setError(result.msg);
                            }
                            if (result.type == "Info") {
                                setInfoMsg(result.msg);
                            }
                            jQuery("#country").val("");
                        }
                    } else {
                        Logout();
                    }
                }
            });
        }
    }
    jQuery("#town").focusout(function () {
        if (jQuery(this).val() != "") {
            loadTownData(jQuery(this).val());
        }
    });
    function loadTownData(towncd) {
        jQuery.ajax({
            type: "GET",
            url: "/JobDefinition/preTownTextChanged",
            data: { val: towncd },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (typeof (result.data) != "undefined") {
                           // jQuery("#Mbe_distric_cd").val(result.data.district);
                            jQuery("#Province").val(result.data.province);
                           // jQuery("#Mbe_postal_cd").val(result.data.postalCD);
                            //jQuery("#Mbe_country_cd").val(result.data.countryCD);
                        }
                    } else {
                        jQuery("#town").val("");
                       // jQuery("#Mbe_distric_cd").val("");
                        jQuery("#Province").val("");
                        //jQuery("#Mbe_postal_cd").val("");
                        //jQuery("#Mbe_country_cd").val("");
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
    jQuery('#Telephone,#Fax').on('input', function (event) {
        this.value = this.value.replace(/[^0-9]/g, '');
    });
    jQuery("#email").focusout(function () {
        if (jQuery(this).val() != "") {
            if (!isValidEmailAddress(jQuery(this).val())) {
                jQuery("#email").val("");
                setInfoMsg("Invalid email address.");
                jQuery("#email").focus();
            }
        }
    });
    function isValidEmailAddress(emailAddress) {
        var pattern = /^([a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+(\.[a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+)*|"((([ \t]*\r\n)?[ \t]+)?([\x01-\x08\x0b\x0c\x0e-\x1f\x7f\x21\x23-\x5b\x5d-\x7e\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|\\[\x01-\x09\x0b\x0c\x0d-\x7f\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))*(([ \t]*\r\n)?[ \t]+)?")@(([a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.)+([a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.?$/i;
        return pattern.test(emailAddress);
    }
});