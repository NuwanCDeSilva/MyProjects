jQuery(document).ready(function () {

    jQuery('#mnthfrom').val(my_date_formatmonth(new Date()));
    jQuery('#mnthfrom').datepicker({ dateFormat: "MM yy" })


    jQuery(".loc-search").click(function () {
        var headerKeys = Array()
        headerKeys = ["Row", "Code", "Description", "Address", "Chanel"];
        field = "locationmg"
        var data = {
            mgr: jQuery("#manager").val()
        }
        var x = new CommonSearch(headerKeys, field, data);
    });

    jQuery(".btn-view-data").click(function () {

        window.open(
              "/CollectionBonusVoucher/GetBonusVoucher?Date=" + jQuery("#mnthfrom").val() + "&PcCode=" + jQuery("#location").val(),
              '_blank' // <- This is what makes it open in a new window.
          );
    });
});