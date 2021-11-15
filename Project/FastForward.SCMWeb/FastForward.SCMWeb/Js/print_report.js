jQuery(document).ready(function () {

    jQuery(".btn-print-repot-whf").click(function (evt) {
        console.log("hrrhi");
        evt.preventDefault();
        var dvReport = document.getElementById("BodyContent_CVWharf");
        var frame1 = dvReport.getElementsByTagName("iframe")[0];
        console.log("hrrhi");
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


    jQuery(".btn-print-repot").click(function (evt) {
        evt.preventDefault();
        var dvReport = document.getElementById("BodyContent_CVInventory");
        var frame1 = dvReport.getElementsByTagName("iframe")[0];
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
    jQuery(".btn-print-salerepot").click(function (evt) {
        evt.preventDefault();
        var dvReport = document.getElementById("BodyContent_CVSale");
        var frame1 = dvReport.getElementsByTagName("iframe")[0];
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