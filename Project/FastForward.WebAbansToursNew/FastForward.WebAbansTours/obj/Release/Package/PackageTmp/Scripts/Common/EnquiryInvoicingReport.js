jQuery(document).ready(function () {
    jQuery(".enqinv-report-cls .btn-frm-back").click(function () {
        //if (jQuery(".txt-cst-sht-num").val() != "") {
        //    window.location.href = "/Invoicing/Index/";
        //} else {
        //    window.location.href = "/Invoicing"
        //}
        window.close();
    });
    jQuery(".enqinv-report-cls .btn-print-repot").click(function () {

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