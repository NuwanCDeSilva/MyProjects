jQuery(document).ready(function () {
    jQuery(".costing-report-cls .btn-frm-back").click(function () {
        //if (jQuery(".txt-rece-no").val() != "") {
        //    window.location.href = "/ReceiptEntry/Index";
        //} else {
        //    window.location.href = "/ReceiptEntry"
        //}
        window.close();
    });
    jQuery(".costing-report-cls .btn-print-repot").click(function () {

        var dvReport = document.getElementById("iframe");
        var innerDoc = dvReport.contentDocument || dvReport.contentWindow.document;
        var frame1 = innerDoc.getElementsByTagName("iframe")[0];
        if (navigator.appName.indexOf("Internet Explorer") != -1) {
            frame1.name = frame1.id;
            window.frames[frame1.id].focus();
            window.frames[frame1.id].print();
        }
        else {
            var frameDoc = frame1.contentWindow ? frame1.contentWindow : frame1.contentDocument.document ? frame1.contentDocument.document : frame1.contentDocument;
            frameDoc.print();
        }

    });
});