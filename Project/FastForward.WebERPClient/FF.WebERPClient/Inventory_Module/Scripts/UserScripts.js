//function clickButton(e, buttonid) {
//    var evt = e ? e : window.event;
//    var bt = document.getElementById(buttonid);
//    if (bt) {
//        if (evt.keyCode == 113) {
//            bt.click();
//            return false;
//        }

//    }
//}


//function TableRowClicks(rowIndex, destCtrl) {
//    //Get the selected cell value using selected rowIndex.
//    var tabRowId = "tab" + rowIndex;
//    var selectedRow = document.getElementById(tabRowId);
//    var Cells = selectedRow.getElementsByTagName("td");
//    var selectedValue = Cells[0].innerText;


//    //Get the result object and set the value.
//    var resultObject = document.getElementById(destCtrl);
//    resultObject.value = selectedValue;
//    resultObject.focus();

//}





////---------------------------------------------------------------------------------------------------------Get Item Description
////function GetItemDescription(src, dest) {
////    var ctrl = document.getElementById(src);
////    if (ctrl.value != "") {
////        FF.WebERPClient.LocalWebServices.CommonSearchWebServive.GetItemDescription(ctrl.value, onDescriptionPass, onDescriptionFail, dest);
////    }
////}
//////SucceededCallback method.
////function onDescriptionPass(result, destCtrl) {
////    var divResults = document.getElementById(destCtrl);
////    divResults.value = result;


////}
//////FailedCallback method.
////function onDescriptionFail(error, destCtrl) {
////    var divResults = document.getElementById(destCtrl);
////    divResults.value = "";
////}




////---------------------------------------------------------------------------------------------------------Get Item Description
//function IsItemSerialized_1(src, objTarget) {
//    var ctrl = document.getElementById(src);
//    if (ctrl.value != "") {
//        FF.WebERPClient.LocalWebServices.CommonSearchWebServive.IsItemSerialized_1(ctrl.value, onSer1Pass, onSer1Fail, objTarget);
//    }
//}
////SucceededCallback method.
//function onSer1Pass(result, objTarget) {
//    var objs = new Array();
//    objs = objTarget.split("|");
//    var _Qty = document.getElementById(objs[0]);
//    var _Serial = document.getElementById(objs[1]);
//    if (_Qty.id == "txtQty") {
//        var hidn = document.getElementById("hdnIsSerialized_1");
//        hidn.value = result;
//    }
//    if (result == true) {
//        _Qty.value = "1";
//        _Qty.disabled = true;
//        _Serial.value = "";
//        _Serial.disabled = false;
//    }
//    else {
//        _Qty.value = "";
//        _Qty.disabled = false;
//        _Serial.value = "N/A";
//        _Serial.disabled = true;
//    }
//}

////FailedCallback method.
//function onSer1Fail(error, objQtys, objSerials) {
//    var _Qty = document.getElementById(objQty);
//    var _Serial = document.getElementById(objSerial);
//    var hidn = document.getElementById("hdnIsSerialized_1");
//    _Qty.value = "";
//    _Qty.Enabled = false;
//    _Serial.value = "";
//    _Serial.Enabled = false;
//    hidn.value = "";
//}


////---------------------------------------------------------------------------------------------------------Get Item Description
//function IsItemSerialized_2(src, dest) {
//    var ctrl = document.getElementById(src);
//    if (ctrl.value != "") {
//        FF.WebERPClient.LocalWebServices.CommonSearchWebServive.IsItemSerialized_2(ctrl.value, onSer2Pass, onSer2Fail, dest);
//    }
//}
////SucceededCallback method.
//function onSer2Pass(result, destCtrl) {
//    var hidn = document.getElementById(destCtrl);
//    hidn.value = result;
//    var _serial2 = document.getElementById("txtSerial2");
//    if (result == true) {
//        _serial2.disabled = false;
//        _serial2.value = "";
//    }
//    else {
//        _serial2.disabled = true;
//        _serial2.value = "N/A";
//    }

//}
////FailedCallback method.
//function onSer2Fail(error, destCtrl) {
//    var hidn = document.getElementById(destCtrl);
//    hidn.value = "";
//    var _serial2 = document.getElementById("txtSerial2");
//    _serial2.disabled = false;
//    _serial2.value = "";
//}



////---------------------------------------------------------------------------------------------------------Get Item Description
//function IsItemSerialized_3(src, dest) {
//    var ctrl = document.getElementById(src);
//    if (ctrl.value != "") {
//        FF.WebERPClient.LocalWebServices.CommonSearchWebServive.IsItemSerialized_3(ctrl.value, onSer3Pass, onSer3Fail, dest);
//    }
//}
////SucceededCallback method.
//function onSer3Pass(result, destCtrl) {
//    var hidn = document.getElementById(destCtrl);
//    hidn.value = result;
//    var _serial3 = document.getElementById("txtSerial3");
//    if (result == true) {
//        _serial3.disabled = false;
//        _serial3.value = "";
//    }
//    else {
//        _serial3.disabled = true;
//        _serial3.value = "N/A";
//    }
//}
////FailedCallback method.
//function onSer3Fail(error, destCtrl) {
//    var hidn = document.getElementById(destCtrl);
//    hidn.value = "false";
//    var _serial3 = document.getElementById("txtSerial3");
//    _serial3.disabled = false;
//    _serial3.value = "";
//}


////---------------------------------------------------------------------------------------------------------Get Item Description
//function IsItemHaveSubSerial(src, dest) {
//    var ctrl = document.getElementById(src);
//    if (ctrl.value != "") {
//        FF.WebERPClient.LocalWebServices.CommonSearchWebServive.IsItemHaveSubSerial(ctrl.value, onSubItemPass, onSubItemFail, dest);
//    }
//}
////SucceededCallback method.
//function onSubItemPass(result, destCtrl) {
//    var divResults = document.getElementById(destCtrl);
//    divResults.value = result;
//    var _image3 = document.getElementById("Image3");
//    //var _extender = document.getElementById("CPESubSerial");

//    if (result == true) {
//        $find('CPESubSerial')._doOpen();
//        _image3.disabled = false;
//    }
//    else {
//        $find('CPESubSerial')._doClose();
//        _image3.disabled = true;
//    }


//}
////FailedCallback method.
//function onSubItemFail(error, destCtrl) {
//    var divResults = document.getElementById(destCtrl);
//    divResults.value = "false";
//}


////Developed by Prabhath Wijetunge - on 15 03 2012
////Allow only numaric and decimal values
//function numbersonly(e, decimal) {
//    var key;
//    var keychar;

//    if (window.event) {
//        key = window.event.keyCode;
//    }
//    else if (e) {
//        key = e.which;
//    }
//    else {
//        return true;
//    }
//    keychar = String.fromCharCode(key);

//    if ((key == null) || (key == 0) || (key == 8) || (key == 9) || (key == 13) || (key == 27)) {
//        return true;
//    }
//    else if ((("0123456789").indexOf(keychar) > -1)) {
//        return true;
//    }
//    else if (decimal && (keychar == ".")) {
//        return true;
//    }
//    else
//        return false;
//}


////---------------------------------------------------------------------------------------------------------Get Item Description
//function IsUOMDecimalAllow(src, qty) {
//    var ctrl = document.getElementById(src);
//    if (ctrl.value != "") {
//        FF.WebERPClient.LocalWebServices.CommonSearchWebServive.IsUOMDecimalAllow(ctrl.value, onUOMPass, onUOMFail, qty);
//    }
//}

//function onUOMPass(result, qty) {
//    var objCtrl = document.getElementById(qty);
//    objCtrl.setAttribute("onKeyPress", "return numbersonly(event," + result + ")");

//}

//function onUOMFail(error, qty) {
//    var objCtrl = document.getElementById(qty);
//    objCtrl.setAttribute("onKeyPress", "return numbersonly(event,false)");
//}


//function Count(text, long, txtObject) {
//    // Change number to your max length.
//    var maxlength = new Number(long);
//    if (document.getElementById(txtObject).value.length > maxlength) {
//        text.value = text.value.substring(0, maxlength);
//        //Clear the text box
//        document.getElementById(txtObject).value = "";
//        alert(" You are allow to enter " + long + " chars");
//    }
//}