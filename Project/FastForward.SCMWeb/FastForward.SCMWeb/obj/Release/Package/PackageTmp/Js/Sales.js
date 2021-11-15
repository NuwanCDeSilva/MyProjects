jQuery(document).ready(function () {

    // jQuery("#BodyContent_ddlMonth").val(2);

    //jQuery("#BodyContent_ddlMonth").on("input", function () {
    //    var d = new Date();
    //    console.log(d);
    //})
    var d = new Date();
    var n = d.getMonth() + 1;
    jQuery("#BodyContent_ddlMonth").val(n);

    //$("#BodyContent_rad01").change(function () {
    //    if (this.checked) {
    //        var d = new Date();
    //        var n = d.getMonth() + 1;

    //        jQuery("#BodyContent_ddlMonth").val(n);
    //    }
    //})

    //$("#BodyContent_ddlYear").change(function () {
      
    //        var d = new Date();
    //        console.log(d);

    //        jQuery("#BodyContent_txtFromDate").val(d);
        
    //})




});