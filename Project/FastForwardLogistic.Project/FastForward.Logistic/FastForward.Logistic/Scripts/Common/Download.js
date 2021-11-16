jQuery(document).ready(function () {
    jQuery(".btn-gethbl-data").click(function (evt) {
        evt.preventDefault();
        jQuery('.bl-display-tbl').empty();
        if (jQuery("#MasterBLNumber").val() != "") {
            jQuery.ajax({
                type: "GET",
                data: { blno: jQuery("#MasterBLNumber").val() },
                url: "/HouseBLDownload/validateDocument",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.login == true) {
                        if (result.success == true) {
                            if (result.data.length > 0) {
                                for (i = 0; i < result.data.length; i++) {
                                    jQuery('.bl-display-tbl').append('<tr class="new-row">' +
                                            '<td class="chk-bl-data">' + '<input class="select-bl" type="checkbox" name="selectedBl" value="' + result.data[i].Bl_h_doc_no + '">' + '</td>' +
                                            '<td>' + result.data[i].Bl_h_doc_no + '</td>' +
                                            '</tr>');
                                }
                            }
                           // 
                        } else {
                            setInfoMsg(result.msg);
                        }
                    } else {
                        Logout();
                    }
                }
            });
        } else {
            setInfoMsg("Please enter BL Number.");
        }
    });

    jQuery(".btn-download-data").click(function (evt) {
        evt.preventDefault();
        var checkBL = "";
        var i = 0;
        jQuery('input[name="selectedBl"]:checked').each(function () {
            if (this.value != "") {
                checkBL += this.value + ',';
            }
        });
        if (checkBL == "") {
            setInfoMsg("Please select House BL Number's to download.");
        } else {
            jQuery("#download").submit();
        }

    });


});