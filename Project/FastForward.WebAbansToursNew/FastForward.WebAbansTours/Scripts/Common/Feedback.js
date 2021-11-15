jQuery(document).ready(function () {
    //jQuery("input[type='radio']").css("display", "none");
    jQuery(".btn-fb-clear-data").click(function (evt) {
        evt.preventDefault();
        Lobibox.confirm({
            msg: "Do you want to clear data ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    clearFeedBackForm();
                }
            }
        });
    });
    jQuery(".btn-fb-save-data").click(function () {
        Lobibox.confirm({
            msg: "Do you want to save ?",
            callback: function ($this, type, ev) {
                if (type == "yes") {
                    var formData = jQuery('#feedback-crte-frm').serialize();
                    jQuery.ajax({
                        url: '/Feedback/SaveFeedback',
                        type: 'POST',
                        data: formData,
                        dataType: "json",
                        success: function (result) {
                            if (result.success == true) {
                                setSuccesssMsg(result.msg);
                                clearFeedBackForm();
                            }else{
                                if (result.type == "Error") {
                                    setError(result.msg);
                                }
                                if (result.type == "Info") {
                                    setInfoMsg(result.msg);
                                }
                            }
                        }
                    });
                    
                }
            }
        });
    });

    jQuery("#EnqId").focusout(function () {
        getFeedEnquiryDetails(jQuery(this).val());
    });
    jQuery("#EnqId").on("keydown", function (evt) {
        if (evt.keyCode == 113) {
            var headerKeys = Array()
            headerKeys = ["Row", "Enquiry Id", "Ref Num", "Type", "Customer Code", "Name", "Address"];
            field = "feedEnqIdSrch";
            var x = new CommonSearch(headerKeys, field);
        }
        if (evt.keyCode == 13) {
            getFeedEnquiryDetails(jQuery(this).val());
        }
    })
    jQuery(".feed-enq-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Enquiry Id", "Ref Num", "Type", "Customer Code", "Name", "Address"];
        field = "feedEnqIdSrch";
        var x = new CommonSearch(headerKeys, field);
    });
});
function clearFeedBackForm() {
    jQuery("#EnqId").val("");
    jQuery(':input', '#feedback-crte-frm')
              .not(':button, :submit, :reset, :hidden')
              .removeAttr('checked')
              .removeAttr('selected');
}
function getFeedEnquiryDetails(enqId){
    if (enqId != "") {
        jQuery.ajax({
            type: "GET",
            url: "/Feedback/getEnquiryDetails?enqId=" + enqId,
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        getEnquiryFeedbacDetails(enqId);
                    } else {
                        setInfoMsg("Invalid enquiry id.");
                    }
                } else {
                    Logout();
                }
            }
        });
    }
}
function getEnquiryFeedbacDetails(enqId) {
    if (enqId != "") {
        jQuery.ajax({
            type: "GET",
            url: "/Feedback/getEnquiryFeedback?enqId="+enqId,
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.login == true) {
                    if (result.success == true) {
                        if (typeof result.feedData != "undefined") {
                            setFeedbackData(result.feedData);
                        } else {
                            jQuery(':input', '#feedback-crte-frm')
                          .not(':button, :submit, :reset, :hidden')
                          .removeAttr('checked')
                          .removeAttr('selected');
                        }
                    }
                } else {
                    Logout();
                }
            }
        });
    }
}
function  setFeedbackData(feedData){
    for (i = 0; i < feedData.length; i++) {
        if (feedData[i].SSVL_VALSEQ != 0) {
            jQuery(":input[name=" + feedData[i].SSVL_QSTSEQ + "][value=" + feedData[i].SSVL_QSTSEQ + "-" + feedData[i].SSVL_VALSEQ + "-" + feedData[i].SSVL_ANS + "]").prop('checked', true);
        } else {
            jQuery(":input[name=" + feedData[i].SSVL_QSTSEQ + "]").val(feedData[i].SSVL_ANS);
        }
        
    }
}