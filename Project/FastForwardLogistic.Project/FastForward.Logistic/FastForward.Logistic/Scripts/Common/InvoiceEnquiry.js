jQuery(document).ready(function () {
    $(function () {
        jQuery(".search-Inv-list").click(function () {
            LoadJobservice();
        });
    });
    
    LoadModeOfShipment();
    function LoadJobservice() {
        jQuery.ajax({
            type: "GET",
            url: "/InvoiceEnquiry/LoadInvoiceEnquiry",
            data: { JobNo: jQuery("#Jb_jb_no").val(), modOfShpmnt: jQuery("#sh_mode").val(), typOfShpmnt: jQuery("#shipment_type").val(), cusCode: jQuery("#cus_cd").val(), hbl: jQuery("#Bl_h_doc_no").val() },
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
        jQuery('.inv-Enquiry-Hdr .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {
                jQuery('.inv-Enquiry-Hdr').append('<tr class="new-row">' +
                      '<td>' + data[i].Tih_job_no + '</td>' +
                       '<td>' + data[i].Tih_inv_no + '</td>' +
                       '<td>' + data[i].Tih_cus_cd + '</td>' +
                       '<td>' + data[i].Tih_bl_d_no + '</td>' +
                       '<td>' + data[i].Tih_bl_h_no + '</td>' +
                       '<td>' + data[i].Tih_bl_m_no + '</td>' +
                       '<td>' + getFormatedDateInput(data[i].Tih_inv_dt) + '</td>' +
                       '<td>' + data[i].Tih_inv_tp + '</td>' +
                       '<td>' + data[i].Tih_inv_sub_tp + '</td>' +
                       '<td align="right">' + data[i].Tih_inv_amt + '</td>' +
                       '<td align="right">' + data[i].Tih_settle_amt + '</td>' +
                       '<td align="right">' + data[i].Tih_bal_settle_amt + '</td>' +
                       '<td style="display:none;">' + data[i].Tih_seq_no + '</td>' +
                       '<td style="text-align:center;"><input type="button" id="viewInvDetails" name="viewInvDetails" value="View" class="Inv-Enq-det" /></td>' +
                        '</tr>');
            }
            ItemView();
        }
    }
    function ItemView() {
        jQuery(".Inv-Enq-det").click(function (evt) {
            evt.preventDefault();
            var td = jQuery(this).parent('td');
            var tr = jQuery(td).parent('tr');
            var SeqNo = jQuery(tr).find('td:eq(12)').html();
            jQuery.ajax({
                type: "GET",
                url: "/InvoiceEnquiry/InvoiceDetails",
                data: { SeqNo: SeqNo },
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            SetInvDtlGrid(result.data);
                        }
                    } else {
                        Logout();
                    }
                }
            });
        });
    }
    function SetInvDtlGrid(data) {
        jQuery('.inv-Enquiry-Dtl .new-row').remove();
        if (data.length > 0) {
            for (i = 0; i < data.length; i++) {
                jQuery('.inv-Enquiry-Dtl').append('<tr class="new-row">' +
                      '<td>' + data[i].Tid_cha_code + '</td>' +
                       '<td>' + data[i].Tid_cha_desc + '</td>' +
                       '<td>' + data[i].Tid_curr_cd + '</td>' +
                       '<td align="right">' + data[i].Tid_ex_rate + '</td>' +
                       '<td align="right">' + data[i].Tid_cha_amt + '</td>' +
                       '</tr>');
            }
            ItemView();
        }
    }
    jQuery(".search-job-no").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Date", "Status"];
        field = "jobno"
        var x = new CommonSearch(headerKeys, field);
    });

    jQuery(".btn-cust_search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Name", "NIC", "Mobile", "BR No"];
        field = "cusCode2"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".btn-house-bl").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code"];
        field = "BL_H_DOC_NO"
        var x = new CommonSearch(headerKeys, field);
    });
    jQuery(".search-ty-sh-no").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row","Code","Description"];
        field = "TY_OF_SHIPMNT"
        var x = new CommonSearch(headerKeys, field);
    });
    function LoadModeOfShipment() {
        jQuery.ajax({
            type: "GET",
            url: "/InvoiceEnquiry/LoadModeOfShipment",
            data: {},
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success = true) {
                        var select = document.getElementById("sh_mode");
                        jQuery("#sh_mode").empty();
                        var options = [];
                        var option = document.createElement('option');
                        if (result.data != null && result.data.length != 0) {
                            for (i = 0; i < result.data.length; i++) {
                                option.text = result.data[i].Text;
                                option.value = result.data[i].Value;
                                options.push(option.outerHTML);
                            }
                        } else {
                            option.text = "Select Mode";
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

    $(function () {
        jQuery(".clear-data").click(function () {
            ClearPage();
        });
    });

    function ClearPage() {
        jQuery("#shipment_type").val("");
        jQuery("#cus_cd").val("");
        jQuery("#Jb_jb_no").val("");
        jQuery("#Bl_h_doc_no").val("");
        jQuery(".inv-Enquiry-Dtl .new-row").remove();
        jQuery(".inv-Enquiry-Hdr .new-row").remove();
        jQuery("#sh_mode").val("");
    }
});