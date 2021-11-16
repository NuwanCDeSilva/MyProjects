jQuery(document).ready(function () {
    jQuery(document).ajaxStart(function () {
        jQuery(".page-loadin").show();
    }).ajaxStop(function () {
        jQuery(".page-loadin").hide();
    });
    
    var global_receipt_date = null;

    //log out link click event in home page
    jQuery(".logout-link-cls").click(function (evt) {
        jQuery.ajax({
            cache: false,
            async: true,
            type: "POST",
            url: "/Login/LogOff",
            data: { request: "req" },
            success: function (result) {
                if (result.success == true) {
                    window.location.replace("/Login");
                } else {
                    if (result.msg.length > 0) {
                        setError(result.msg);
                    }
                }
            },
            error: function (result) {

            }
        });
    });
    jQuery(".enq-id-search-new").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Enquiry Id", "Ref Num", "Type", "Customer Code", "Name", "Address"];
        field = "InvEnqNew"
        var x = new CommonSearch(headerKeys, field);
    });

    //jQuery("#demopage .btn-save-data").click(function (evt) {
    //    evt.preventDefault();
    //    jQuery(this).prop('disabled', false);
    //    jQuery.ajax({
    //        type: 'POST',
    //        url: '/DataEntry/CustomerCreation',
    //        data: jQuery('.frm-cust-det').serialize(),
    //        success: function (response) {
    //            if (response.login == true) {
    //                if (response.success == true) {
    //                    setError(response.msg);
    //                } else {
    //                    jQuery(this).prop('disabled', true);
    //                }
    //            } else {
    //                Logout();
    //            }
    //        }
    //    });
    //});
    //Dilshan on 31/08/2017 // Modified by Chathura on 19-sep-2017
    jQuery(".navbar-custom-menu .val-field1").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description", "Address"];
        field = "ProfitCenterGlobe"
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery(".navbar-custom-menu .val-field").click(function () {
        //alert("aaas");
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description"];
        field = "CompanyGlobe"
        var x = new CommonSearch(headerKeys, field);
    });

    //Added by Chathura on 13-Sep-2017
    jQuery(".navbar-custom-menu .val-field2").click(function () {        
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description"];
        field = "ModeOfShipment"
        var x = new CommonSearch(headerKeys, field);
    });

    //Dilshan on 31/08/2017
    //jQuery(".navbar-custom-menu .normal-li .val-field").click(function () {
    //    var headerKeys = Array()
    //    headerKeys = ["Row", "Code", "Description", "Address", "Channel"];
    //    field = "ProfitCenterGlobe"
    //    var x = new CommonSearch(headerKeys, field);
    //});

    if (window.innerWidth <= 760) {
        jQuery(".common-details-cls .contet-view").css("padding-left", "0px");
    } else if (window.innerWidth > 760 && window.innerWidth <= 980) {
        var wid = jQuery(".common-details-cls").width();
        jQuery(".common-details-cls .contet-view").css("padding-left", wid / 3);
    } else {
        var wid = jQuery(".common-details-cls").width();
        jQuery(".common-details-cls .contet-view").css("padding-left", "65%");
    }
    jQuery(window).resize(function () {
        if (window.innerWidth <= 760) {
            jQuery(".common-details-cls .contet-view").css("padding-left", "0px");
        } else if (window.innerWidth > 760 && window.innerWidth <= 980) {
            var wid = jQuery(".common-details-cls").width();
            jQuery(".common-details-cls .contet-view").css("padding-left", wid / 3);
        } else {
            var wid = jQuery(".common-details-cls").width();
            jQuery(".common-details-cls .contet-view").css("padding-left", "65%");
        }
    });
});
function showNotice(msg) {
    jQuery().toastmessage('showToast', {
        text: msg,
        sticky: false,
        inEffectDuration: 1000,
        stayTime: 5000,
        preventDuplicates: true,
        position: 'top-center',
        type: 'notice',
        closeText: '',
        close: function () {
        }
    });
}
function showWarning(msg) {
    jQuery().toastmessage('showToast', {
        text: msg,
        sticky: false,
        inEffectDuration: 1000,
        stayTime: 5000,
        preventDuplicates: true,
        position: 'top-center',
        type: 'warning',
        closeText: '',
        close: function () {
        }
    });
}

function showError(msg) {
    jQuery().toastmessage('showToast', {
        text: msg,
        sticky: true,
        inEffectDuration: 1000,
        stayTime: 5000,
        preventDuplicates: true,
        position: 'top-center',
        type: 'error',
        closeText: '',
        close: function () {
        }
    });
}
function showSuccess(msg) {
    jQuery().toastmessage('showToast', {
        text: msg,
        sticky: true,
        inEffectDuration: 1000,
        stayTime: 5000,
        preventDuplicates: true,
        position: 'top-center',
        type: 'success',
        closeText: '',
        close: function () {
        }
    });
}
function setError(msg) {
    jQuery(".animated-super-fast").remove();
    if (jQuery(".animated-super-fast").length == 0) {
        Lobibox.alert('error',
        {
            msg: msg
        });
    }
}
function setSuccesssMsg(msg) {
    jQuery(".animated-super-fast").remove();
    if (jQuery(".animated-super-fast").length == 0) {
        Lobibox.alert('success',
    {
        msg: msg
    });
    }
}

function setInfoMsg(msg) {
    jQuery(".animated-super-fast").remove();
    if (jQuery(".animated-super-fast").length == 0) {
        Lobibox.alert('info',
         {
             msg: msg
         });
    }
}
function getFormatedDate(input) {
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
}
function validateNIC(nic) {
    var cnic_no = nic;
    if (nic.length == 10) {
        var cnic_no_regex = /^[0-9+]{9}[V|v|x|X]$/;
    } else if (nic.length == 12) {
        var cnic_no_regex = /^[0-9]{12}$/;
    }
    if (cnic_no_regex.test(cnic_no)) {
        return true;
    }
    else {
        return false;
    }
}
function Logout() {
    Lobibox.alert('info', {
        msg: 'Login session has expired!',
        buttons: {
            ok: {
                'class': 'btn btn-info',
                closeOnClick: false
            }
        },
        callback: function (lobibox, type) {
            var btnType;
            if (type === 'ok') {
                window.location.replace("/Login/Index");
            }
        }
    });
    //if (alert("Login session has expired!") == true) {

    //} else {
    //    window.location.replace("/Login/Index");
    //}
}
var my_date_format_tran = function (input) {
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
function getFormatedDateInput(date) {
    var dte = new Date(parseInt(date.substr(6)));
    if (my_date_format(dte) != "NaN/undefined/NaN")
        return my_date_format(dte);
}

function my_date_format_with_time(input) {
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
    var min = date.getMinutes();
    var time = addZero(date.getHours()) + ":" + addZero(date.getMinutes());
    return (day + "/" + monthNames[monthIndex] + "/" + year + " " + time);
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
function addZero(i) {
    if (i < 10) {
        i = "0" + i;
    }
    return i;
}
function addCommas(nStr) {
    nStr += '';
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}

function validNumber(num) {
    return jQuery.isNumeric(num);
}
function convertDate(date) {
    //var date = date.match(/\((.*?)\)/);

    var x = [{ "id": 1, "start": date }, { "id": 2, "start": date }];
    //var myObj = jQuery.parseJSON('{"date_created":"' + date[1] + '"}'),
    //myDate = new Date(1000 * myObj.date_created);
    var myDate = new Date(x[0].start.match(/\d+/)[0] * 1);
    return myDate.toString();
}
function getMonthAgoMonth(date) {
    var d = new Date(date);
    if (d.getMonth() == 0) {
        d.setMonth(11);
        d.setYear(d.getFullYear() - 1);
    } else {
        d.setMonth(d.getMonth());
    }
    var date = (d.getMonth()) + '/' + d.getDate() + '/' + d.getFullYear();
    return date;
}
function ValidDate(date) {
    if (Date.parse(date)) {
        return "true";
    } else {
        return "false";
    }
}
function getNumberOfDays(fromDate, toDate) {
    if (fromDate == toDate) {
        return 1;
    } else {
        var dates = daydiff(Date.parse(toDate), Date.parse(fromDate));
        return Math.ceil(dates);
    }
}

function parseDate(str) {
    var mdy = str.split('/')
    return new Date(mdy[2], mdy[0] - 1, mdy[1]);
}

function daydiff(first, second) {
    return (second - first) / (1000 * 60 * 60 * 24)
}

function getFormatedPreMonth(input) {
    var monthNames = [
      "Jan", "Feb", "Mar",
      "Apr", "May", "Jun", "Jul",
      "Aug", "Sep", "Oct",
      "Nov", "Dec"
    ];

    var date = new Date(Date.parse(input));;
    var day = date.getDate();
    var monthIndex = date.getMonth();
    var preMonth = monthIndex - 1;
    var year = date.getFullYear();
    if (preMonth = -1) {
        preMonth = 11;
        year = year - 1;
    }
    //var year = date.getFullYear();
    return (day + "/" + monthNames[preMonth] + "/" + year);
}
function showSuccess(msg) {
    jQuery().toastmessage('showToast', {
        text: msg,
        sticky: true,
        inEffectDuration: 1000,
        stayTime: 5000,
        preventDuplicates: true,
        position: 'top-center',
        type: 'success',
        closeText: '',
        close: function () {
        }
    });
}
