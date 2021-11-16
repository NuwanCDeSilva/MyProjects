jQuery(document).ready(function () {
    jQuery("#FrmDate").val(getFormatedDate(new Date()));
    jQuery("#ToDate").val(getFormatedDate(new Date()));
    jQuery(".search-Req-no").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Request No", "Mannual Ref", "Request Date"];
        field = "ptychsreq";
        data = "PTCSHREQ";
        var x = new CommonSearch(headerKeys, field, data);
    });
    jQuery(".btn-pro-cnt").click(function (evt) {
        evt.preventDefault();
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description", "Channel"];
        field = "ptycshsetpc";
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".btn-job-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Job No", "Job Date", "Pouch No"];
        field = "ptyjob";
        var x = new CommonSearch(headerKeys, field);
    });
    $(function () {
        jQuery(".clear-data").click(function ()
        {
            clearPage();
        });
    });
    function clearPage()
    {
        jQuery("#FrmDate").val("");
        jQuery("#ToDate").val("");
        jQuery("#RequestNo").val("");
        jQuery("#JobNo").val("");
        jQuery("#ProfitCenter").val("");
        jQuery(".Vou-Enquiry-Hdr .new-row").remove();
        jQuery(".Vou-Enquiry-Dtl .new-row").remove();
    }
    $(function () {
        jQuery(".search-list").click(function () {
            if (jQuery("#ProfitCenter").val() != "") {
                LoadPaymentVoucher();
            }
            else
            {
                setInfoMsg('Please select profit center');
            }
        });
    });
    function LoadPaymentVoucher() {
        jQuery.ajax({
            type: "GET",
            url: "/PaymentVoucherEnquiry/LoadVoucherDetails",
            data: { frmDate: jQuery("#FrmDate").val(), toDate: jQuery("#ToDate").val(), reqNo: jQuery("#RequestNo").val(), jobNo: jQuery("#JobNo").val(), manRefNo: jQuery("#man_ref_no").val(), proCnt: jQuery("#ProfitCenter").val() },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        SetInvEnquiryGrid(result.data);
                    } else {
                        setError(result.msg);
                    }
                } else {
                    Logout();
                }
            }
        })
    }
    function SetInvEnquiryGrid(data) {
        jQuery('.Vou-Enquiry-Hdr .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {
                jQuery('.Vou-Enquiry-Hdr').append('<tr class="new-row">' +
                      '<td>' + data[i].TPRH_REQ_NO + '</td>' +
                       '<td>' + getFormatedDateInput(data[i].TPRH_REQ_DT) + '</td>' +
                       '<td>' + data[i].TPRH_MANUAL_REF + '</td>' +
                       '<td>' + data[i].TPRH_PC_CD + '</td>' +
                       '<td>' + data[i].TPRH_REQ_BY + '</td>' +
                       '<td>' + data[i].TPRD_JOB_NO + '</td>' +
                       '<td>' + data[i].TPRH_STUS + '</td>' +
                       '<td>' + data[i].TPRH_PAY_TO + '</td>' +
                       '<td style="display:none;">' + data[i].TPRH_SEQ + '</td>' +
                       '<td style="text-align:center;"><input type="button" id="viewVouDetails" name="viewVouDetails" value="View" class="Vou-Enq-det" /></td>' +
                        '</tr>');
            }
            VouDetailsView();
        }
    }
    function VouDetailsView() {
        jQuery(".Vou-Enq-det").click(function (evt) {
            evt.preventDefault();
            var td = jQuery(this).parent('td');
            var tr = jQuery(td).parent('tr');
            var ReqNo = jQuery(tr).find('td:eq(0)').html();
            var SeqNo = jQuery(tr).find('td:eq(8)').html();
            jQuery.ajax({
                type: "GET",
                url: "/PaymentVoucherEnquiry/VouDetails",
                data: { ReqNo: ReqNo,SeqNo: SeqNo },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            SetVouDtlGrid(result.data);
                        }
                    } else {
                        Logout();
                    }
                }
            });
        });
    }
    function SetVouDtlGrid(data) {
        jQuery('.Vou-Enquiry-Dtl .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {
                jQuery('.Vou-Enquiry-Dtl').append('<tr class="new-row">' +
                      '<td>' + data[i].TPRD_LINE_NO + '</td>' +
                       '<td>' + data[i].TPRD_ELEMENT_CD + '</td>' +
                       '<td>' + data[i].TPRD_ELEMENT_DESC + '</td>' +
                       '<td>' + data[i].TPRD_CURRENCY_CODE + '</td>' +
                       '<td align="right">' + data[i].TPRD_UNIT_PRICE + '</td>' +
                       '<td>' + data[i].TPRD_COMMENTS + '</td>' +
                       '</tr>');
            }
        }
    }
});