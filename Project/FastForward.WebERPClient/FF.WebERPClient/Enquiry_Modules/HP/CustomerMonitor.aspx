<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerMonitor.aspx.cs" Inherits="FF.WebERPClient.Enquiry_Modules.HP.CustomerMonitor" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="~/UserControls/uc_HpAccountSummary.ascx" tagname="uc_HpAccountSummary" tagprefix="AC" %>
<%@ Register src="~/UserControls/uc_HpAccountDetail.ascx" tagname="uc_HpAccountDetail" tagprefix="AD" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server" >
<script src="../../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>

<style type="text/css">
#mainDiv
{
    height:150px;
    overflow:scroll;
}
        
* {margin:0; padding:0}
.sm {list-style:none; width:413px; height:109px; display:block; overflow:hidden}
.sm li {float:left; display:inline; overflow:hidden}
        
</style>

<script type="text/javascript" >

    function JsonCheckNull(value) {
        var _return = null;
        if (value == '' || value == null) {
            _return = '&nbsp;';
        }
        else {
            _return = value;
        }
        return _return;
    }

    function JsonDateFormat(date) {
        var myDate = new Date(parseInt(date.substr(6)));
        var output = myDate.getDate() + "\\" + parseInt(parseInt(myDate.getMonth()) + 1) + "\\" + myDate.getFullYear();
        return output;
    }

    $(document).ready(function () {
        ClearGrid();
        var _hdnSearchType = document.getElementById('<%=hdnSearchType.ClientID%>');
        var _hdnSearchValue = document.getElementById('<%=hdnSearchValue.ClientID%>');
        var _isFired = document.getElementById('<%= hdnIsHpAccountFire.ClientID %>');
        if (_hdnSearchType.value != '' && _hdnSearchValue.value != '') {
            __customer
             GetMonitorCustomer(_hdnSearchValue.value, _hdnSearchType.value);
            _isFired.value = 1;
        }

    });

    var __customer;

    function SelectDocument(rowIndex, tblid) {
        var _isFired = document.getElementById('<%= hdnIsHpAccountFire.ClientID %>');
        var _tp = document.getElementById(tblid.id).getElementsByTagName('tr')[rowIndex].getElementsByTagName('td')[1].innerHTML;
        var _document = document.getElementById(tblid.id).getElementsByTagName('tr')[rowIndex].getElementsByTagName('td')[2].innerHTML;
        var _hpaccsum = document.getElementById('<%= btnGetHpPayment.ClientID %>')
        var _field = document.getElementById('<%= hdnDocumentNo.ClientID %>');
        var _dtp = document.getElementById('<%= hdnDocumentType.ClientID %>');
        var _customer = document.getElementById('<%= hdnCustomer.ClientID %>');
        _field.value = _document;
        _dtp.value = _tp;

        if (_tp == 'HS' && _field.value != '') {
            BindGuarantorDetail(_document);
            BindAccountSummary(_document);

            _hpaccsum.click();
            _isFired.value = 1;

            BindAccountSchedule(_document);
            BindAccountSchedulHistory(_document);
            BindAccountRevert(_document);
            BindAccountRevertRelease(_document);
            BindAccountExchange(_document);
            BindHireSaleAccountBalance(_customer.value);
            BindHireSalePaymentBalance(_document);
            BindAccountTransfer(_document);
            BindAccountCustomerTrasnfer(_document);
            BindAccountDiriyaDetail(_document);
        }
        BindAccountInvoiceWithSerial(_document);
        BindCreditOutStandDocument(_customer.value);
        BindCustomerPaymentSummary(_customer.value);
        BindInvoiceReceipt(_document);

    }

    function SelectDocumentforReceipt(rowIndex, tblid) {
        var _document = document.getElementById(tblid.id).getElementsByTagName('tr')[rowIndex].getElementsByTagName('td')[2].innerHTML;
        BindInvoiceReceipt(_document);

    }
    
    function SearchCustomer(_parameter, _searchtype) {
        var _hdnSearchType = document.getElementById('<%=hdnSearchType.ClientID%>');
        var _hdnSearchValue = document.getElementById('<%=hdnSearchValue.ClientID%>');
        var _isFired = document.getElementById('<%= hdnIsHpAccountFire.ClientID %>');
        if (_parameter.value != '') {
            _hdnSearchType.value = _searchtype;
            _hdnSearchValue.value = _parameter.value;
            _isFired.value = 0;
            GetMonitorCustomer(_parameter.value, _searchtype);
        }
    }
    
    function GetMonitorCustomer(_parameter, _searchtype) {
       var _customer = document.getElementById('<%=hdnCustomer.ClientID%>');
       $.ajax({

           type: "POST",
           url: "/LocalWebServices/CommonSearchWebServive.asmx/GetMonitorCustomer",
           cache: false,
           contentType: "application/json; charset=utf-8",
           data: "{_parameter : '" + _parameter + "',_searchtype : '" + _searchtype + "'}",
           dataType: "json",
           success: function (msg) {
               if (msg.d == null) { msg.d = _customer; }
               if (msg != null) {
                   BindCustomerDetail(msg.d);
                   BindDocument(msg.d);
                   __customer = msg.d;
                   _customer.value = msg.d;
               }
               else {
                   alert('Selected search having many customers.');
                   _customer.value = '';
               }

           },
           error: function (x, e) { alert("The call to the server side failed. " + x.responseText); }
       });
    }

    function BindCustomerDetail(customer) {
        $.ajax({
            type: "POST",
            url: "/LocalWebServices/CommonSearchWebServive.asmx/GetCustomerInformationByCustomer",
            cache: false,
            contentType: "application/json; charset=utf-8",
            data: "{_customer : '" + customer + "'}",
            dataType: "json",
            success: function (msg) {
                $("#divCName").html(JsonCheckNull(msg.d.Mbe_name));
                $("#divCSSI").html(JsonCheckNull(msg.d.Mbe_nic) + '/ ' + JsonCheckNull(msg.d.Mbe_pp_no) + '/ ' + JsonCheckNull(msg.d.Mbe_dl_no));
                $("#divCDOB").html(JsonDateFormat(msg.d.Mbe_dob));
                $("#divCMobile").html(JsonCheckNull(msg.d.Mbe_mob));
                $("#divCAddress").html(JsonCheckNull(msg.d.Mbe_add1) + JsonCheckNull(msg.d.Mbe_add2));
                $("#divHTown").html(JsonCheckNull(msg.d.Mbe_town_cd));
                $("#divHDistrict").html(JsonCheckNull(msg.d.Mbe_distric_cd));
                $("#divHPostal").html(JsonCheckNull(msg.d.Mbe_postal_cd));
                $("#divHProvince").html(JsonCheckNull(msg.d.Mbe_province_cd));
                $("#divHEmail").html(JsonCheckNull(msg.d.Mbe_email));
                //Present
                $("#divPAddress").html(JsonCheckNull(msg.d.Mbe_cr_add1) + JsonCheckNull(msg.d.Mbe_cr_add2));
                $("#divPTown").html(JsonCheckNull(msg.d.Mbe_cr_town_cd));
                $("#divPDistrict").html(JsonCheckNull(msg.d.Mbe_cr_distric_cd));
                $("#divPPostal").html(JsonCheckNull(msg.d.Mbe_cr_postal_cd));
                $("#divPProvince").html(JsonCheckNull(msg.d.Mbe_cr_province_cd));
                //Work
                $("#divWName").html(JsonCheckNull(msg.d.Mbe_wr_com_name));
                $("#divWAddress").html(JsonCheckNull(msg.d.Mbe_wr_add1) + JsonCheckNull(msg.d.Mbe_wr_add2));
                $("#divWPhone").html(JsonCheckNull(msg.d.Mbe_wr_tel));
                $("#divWFax").html(JsonCheckNull(msg.d.Mbe_wr_fax));
                $("#divWDepartment").html(JsonCheckNull(msg.d.Mbe_wr_dept));
                $("#divWProfession").html(JsonCheckNull(msg.d.Mbe_wr_proffesion));
                $("#divWDesignation").html(JsonCheckNull(msg.d.Mbe_wr_designation));
                $("#divWEmail").html(JsonCheckNull(msg.d.Mbe_wr_email));
                //Tax
                $("#divTExcepted").html(JsonCheckNull(msg.d.Mbe_tax_ex));
                $("#divTRegistered").html(JsonCheckNull(msg.d.Mbe_is_tax));
                $("#divTRegisterNo").html(JsonCheckNull(msg.d.Mbe_tax_no));

                $("#divTSRegistered").html(JsonCheckNull(msg.d.Mbe_is_svat));
                $("#divTSRegisterNo").html(JsonCheckNull(msg.d.Mbe_svat_no));

            },
            error: function (x, e) { alert("The call to the server side failed. " + x.responseText); }
        });
    }
        
    function ClearGrid() {
        $("#divMultiAccount").html('&nbsp;');
        $("#divGuaranter").html('&nbsp;');
        $("#divAccSummary").html('&nbsp;');
        $("#divScheduleHistory").html('&nbsp;');
        $("#divRevert").html('&nbsp;');
        $("#divRevertRelease").html('&nbsp;');
        $("#divExchange").html('&nbsp;');
        $("#divProductDet").html('&nbsp;');
        $("#divCrOutStand").html('&nbsp;');
        $("#divCrAll").html('&nbsp;');
        $("#divCrSummary").html('&nbsp;');
        $("#divCrReceipt").html('&nbsp;');
        $("#divHireAccountBal").html('&nbsp;');
        $("#divLSchedule").html('&nbsp;');
        $("#divAccountPayBalance").html('&nbsp;');
        $("#divLocationTrasnfer").html('&nbsp;');
        $("#divCustomerTrasnfer").html('&nbsp;');
        $("#divDiriyaDet").html('&nbsp;');

        BindCustomerDetail('');
        BindGuarantorDetail('');
        BindAccountSummary('');
        BindAccountSchedulHistory('');
        BindAccountInvoiceWithSerial('');
        BindAccountRevert('');
        BindAccountRevertRelease('');
        BindAccountExchange('');
        BindCreditOutStandDocument('');
        BindCustomerPaymentSummary('');
        BindInvoiceReceipt('');
        BindHireSaleAccountBalance('');
        BindAccountSchedule('');
        BindHireSalePaymentBalance('');
        BindAccountTransfer('');
        BindAccountCustomerTrasnfer('');
        BindAccountDiriyaDetail('');


    } 
    
    function BindDocument(customer) {
        $.ajax({
            type: "POST",
            url: "/LocalWebServices/CommonSearchWebServive.asmx/GetMonitorByCustomerDocument",
            cache: false,
            contentType: "application/json; charset=utf-8",
            data: "{_customer : '" + customer + "',_tableid:'tblcusdoc'}",
            dataType: "json",
            success: function (msg) {
                $("#divMultiAccount").html(msg.d);
                var _isFired = document.getElementById('<%= hdnIsHpAccountFire.ClientID %>');
                var _field = document.getElementById('<%= hdnDocumentNo.ClientID %>');
                var _dtp = document.getElementById('<%= hdnDocumentType.ClientID %>');
                var _hpaccsum = document.getElementById('<%= btnGetHpPayment.ClientID %>')
                if (msg.d.length != 473) {
                    if (document.getElementById("tblcusdoc").getElementsByTagName('tr').length == 2) {
                        _field.value = '';
                        _dtp.value = '';
                        var _tp = document.getElementById("tblcusdoc").getElementsByTagName('tr')[1].getElementsByTagName('td')[1].innerHTML;
                        var _document = document.getElementById("tblcusdoc").getElementsByTagName('tr')[1].getElementsByTagName('td')[2].innerHTML;
                        _field.value = _document;
                        _dtp.value = _tp;
                    }
                    if (_dtp.value == 'HS' && _field.value!='') {
                        BindGuarantorDetail(_field.value);
                        BindAccountSummary(_field.value);
                        if (_isFired.value != '1') {
                            _hpaccsum.click();
                            _isFired.value = 1;
                        }
                        BindAccountSchedule(_field.value);
                        BindAccountSchedulHistory(_field.value);
                        BindAccountRevert(_field.value);
                        BindAccountRevertRelease(_field.value);
                        BindAccountExchange(_field.value);
                        BindHireSaleAccountBalance(customer);
                        BindHireSalePaymentBalance(_field.value);
                        BindAccountTransfer(_field.value);
                        BindAccountCustomerTrasnfer(_field.value);
                        BindAccountDiriyaDetail(_field.value);
                    }
                    BindAccountInvoiceWithSerial(_field.value);
                    BindCreditOutStandDocument(customer);
                    BindCustomerPaymentSummary(customer);
                    BindInvoiceReceipt(_field.value);

                }
                else {
                    _field.value = '';
                    _dtp.value = '';
                    ClearGrid();
                }
            },
            error: function (x, e) { alert("The call to the server side failed. " + x.responseText); }
        });
    }

    function BindGuarantorDetail(account) {
        $.ajax({
            type: "POST",
            url: "/LocalWebServices/CommonSearchWebServive.asmx/GetGuarantorDetail",
            cache: false,
            contentType: "application/json; charset=utf-8",
            data: "{_account : '" + account + "',_tableid:'tblguarantor'}",
            dataType: "json",
            success: function (msg) { $("#divGuaranter").html(msg.d); },
            error: function (x, e) { alert("The call to the server side failed. " + x.responseText); }
        })

    }

    function BindAccountSummary(account) {
        $.ajax({
            type: "POST",
            url: "/LocalWebServices/CommonSearchWebServive.asmx/GetAccountSummary",
            cache: false,
            contentType: "application/json; charset=utf-8",
            data: "{_account : '" + account + "',_tableid:'tblaccsummary'}",
            dataType: "json",
            success: function (msg) { $("#divAccSummary").html(msg.d); },
            error: function (x, e) { alert("The call to the server side failed. " + x.responseText); }
        });
    }

    function BindAccountSchedulHistory(account) {
        $.ajax({
            type: "POST",
            url: "/LocalWebServices/CommonSearchWebServive.asmx/GetAccountScheduleHistory",
            cache: false,
            contentType: "application/json; charset=utf-8",
            data: "{_account : '" + account + "',_tableid:'tblaccschhistory'}",
            dataType: "json",
            success: function (msg) { $("#divScheduleHistory").html(msg.d); },
            error: function (x, e) { alert("The call to the server side failed. " + x.responseText); }
        });
    }
    function BindAccountSchedule(account) {
        $.ajax({
            type: "POST",
            url: "/LocalWebServices/CommonSearchWebServive.asmx/GetCustomerAccountSchedule",
            cache: false,
            contentType: "application/json; charset=utf-8",
            data: "{_account : '" + account + "',_tableid:'tblaccschedule'}",
            dataType: "json",
            success: function (msg) { $("#divLSchedule").html(msg.d); },
            error: function (x, e) { alert("The call to the server side failed. " + x.responseText); }
        })
    }
    
    function BindAccountInvoiceWithSerial(account) {
        $.ajax({
            type: "POST",
            url: "/LocalWebServices/CommonSearchWebServive.asmx/GetInvoiceWithSerial",
            cache: false,
            contentType: "application/json; charset=utf-8",
            data: "{_invoice : '" + account + "',_tableid:'tblaccinvoiceserial'}",
            dataType: "json",
            success: function (msg) { $("#divProductDet").html(msg.d); },
            error: function (x, e) { alert("The call to the server side failed. " + x.responseText); }
        });
    }

    function BindAccountRevert(account) {
        $.ajax({
            type: "POST",
            url: "/LocalWebServices/CommonSearchWebServive.asmx/GetRevertAccountDetail",
            cache: false,
            contentType: "application/json; charset=utf-8",
            data: "{_account : '" + account + "',_tableid:'tblaccrevert'}",
            dataType: "json",
            success: function (msg) { $("#divRevert").html(msg.d); },
            error: function (x, e) { alert("The call to the server side failed. " + x.responseText); }
        });
    }

    function BindAccountRevertRelease(account) {
        $.ajax({
            type: "POST",
            url: "/LocalWebServices/CommonSearchWebServive.asmx/GetRevertReleaseAccountDetail",
            cache: false,
            contentType: "application/json; charset=utf-8",
            data: "{_account : '" + account + "',_tableid:'tblaccrevertrelease'}",
            dataType: "json",
            success: function (msg) { $("#divRevertRelease").html(msg.d); },
            error: function (x, e) { alert("The call to the server side failed. " + x.responseText); }
        });
    }

    function BindAccountExchange(account) {
        $.ajax({
            type: "POST",
            url: "/LocalWebServices/CommonSearchWebServive.asmx/GetExchangeDetail",
            cache: false,
            contentType: "application/json; charset=utf-8",
            data: "{_account : '" + account + "',_tableid:'tblaccexchange'}",
            dataType: "json",
            success: function (msg) { $("#divExchange").html(msg.d); },
            error: function (x, e) { alert("The call to the server side failed. " + x.responseText); }
        });
    }

    function BindCreditOutStandDocument(customer) {
        $.ajax({
            type: "POST",
            url: "/LocalWebServices/CommonSearchWebServive.asmx/GetCustomerDocumentOutstand",
            cache: false,
            contentType: "application/json; charset=utf-8",
            data: "{_customer : '" + customer + "',_tableid:'tbloutstand'}",
            dataType: "json",
            success: function (msg) { $("#divCrOutStand").html(msg.d); },
            error: function (x, e) { alert("The call to the server side failed. " + x.responseText); }
        })
    }

    function BindCustomerPaymentSummary(customer) { 
        $.ajax({
            type: "POST",
            url: "/LocalWebServices/CommonSearchWebServive.asmx/GetCustomerPaymentSummary",
            cache: false,
            contentType: "application/json; charset=utf-8",
            data: "{_customer : '" + customer + "',_tableid:'tblpaysummary'}",
            dataType: "json",
            success: function (msg) { $("#divCrSummary").html(msg.d); },
            error: function (x, e) { alert("The call to the server side failed. " + x.responseText); }
        })
    }

    function BindInvoiceReceipt(document) { 
        $.ajax({
            type: "POST",
            url: "/LocalWebServices/CommonSearchWebServive.asmx/GetInvoiceReceipt",
            cache: false,
            contentType: "application/json; charset=utf-8",
            data: "{_document : '" + document + "',_tableid:'tblreceipt'}",
            dataType: "json",
            success: function (msg) { $("#divCrReceipt").html(msg.d); },
            error: function (x, e) { alert("The call to the server side failed. " + x.responseText); }
        })
    }

    function BindHireSaleAccountBalance(customer) { 
        $.ajax({
            type: "POST",
            url: "/LocalWebServices/CommonSearchWebServive.asmx/GetHireSaleAccountBalance",
            cache: false,
            contentType: "application/json; charset=utf-8",
            data: "{_customer : '" + customer + "',_tableid:'tblreceipt'}",
            dataType: "json",
            success: function (msg) { $("#divHireAccountBal").html(msg.d); },
            error: function (x, e) { alert("The call to the server side failed. " + x.responseText); }
        })
    }

    function BindHireSalePaymentBalance(account) { 
        $.ajax({
            type: "POST",
            url: "/LocalWebServices/CommonSearchWebServive.asmx/GetCustomerAccountBalance",
            cache: false,
            contentType: "application/json; charset=utf-8",
            data: "{_account : '" + account + "',_tableid:'tblhirepaybal'}",
            dataType: "json",
            success: function (msg) { $("#divAccountPayBalance").html(msg.d); },
            error: function (x, e) { alert("The call to the server side failed. " + x.responseText); }
        })
    }

    function BindAccountTransfer(account) { 
        $.ajax({
            type: "POST",
            url: "/LocalWebServices/CommonSearchWebServive.asmx/GetAccountTransfer",
            cache: false,
            contentType: "application/json; charset=utf-8",
            data: "{_account : '" + account + "',_tableid:'tblloctra'}",
            dataType: "json",
            success: function (msg) { $("#divLocationTrasnfer").html(msg.d); },
            error: function (x, e) { alert("The call to the server side failed. " + x.responseText); }
        })
    }
    
    function BindAccountCustomerTrasnfer(account) { 
        $.ajax({
            type: "POST",
            url: "/LocalWebServices/CommonSearchWebServive.asmx/GetAccountCustomerTrasnfer",
            cache: false,
            contentType: "application/json; charset=utf-8",
            data: "{_account : '" + account + "',_tableid:'tblcustra'}",
            dataType: "json",
            success: function (msg) { $("#divCustomerTrasnfer").html(msg.d); },
            error: function (x, e) { alert("The call to the server side failed. " + x.responseText); }
        })
    }

    function BindAccountDiriyaDetail(account) { 
        $.ajax({
            type: "POST",
            url: "/LocalWebServices/CommonSearchWebServive.asmx/GetAccountDiriyaDetail",
            cache: false,
            contentType: "application/json; charset=utf-8",
            data: "{_account : '" + account + "',_tableid:'tbldiriya'}",
            dataType: "json",
            success: function (msg) { $("#divDiriyaDet").html(msg.d); },
            error: function (x, e) { alert("The call to the server side failed. " + x.responseText); }
        })
    }
    
</script>
<%--<asp:UpdatePanel ID="sd" runat="server">
<ContentTemplate>--%>
<%-- Main Div --%>
<div class="maindiv" >
    <%--Button Area--%>
    <div class="PanelHeader invheadersize">
        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="Button invbtn" OnClick="Clear" />
        <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="Button invbtn" OnClick="Close"  />
    </div>

    <div style="float:left; width:100%; padding-top:1px;">
        <div style="float:left; width:60%;height:90px;  ">
            <div class="PanelHeader invcollapsovrid">Search Area</div>
            <div style="float:left; width:100%;height:69px; padding-top:2px;"> 
                    <div style="float:left;width:100%;padding-top:1px;" >
                        <div style="float:left;width:1%;" >&nbsp;</div>
                        <div style="float:left;width:13%;" >Account</div>
                        <div style="float:left;width:19%;"> <div style="float:left; width:80%;"><asp:TextBox runat="server" ID="txtAccount" CssClass="TextBox" Width="100%" onblur="SearchCustomer(this,'ACCOUNT');"></asp:TextBox></div> <div style="float:left; width:20%;"> <asp:ImageButton ID="ImgBtnAccountNo" runat="server"  ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" /></div></div>
                  
                        <div style="float:left;width:1%;" >&nbsp;</div>
                        <div style="float:left;width:13%;" >Invoice</div>
                        <div style="float:left;width:19%;"><div style="float:left; width:80%;"><asp:TextBox runat="server" ID="txtInvoice" CssClass="TextBox" Width="100%" onblur="SearchCustomer(this, 'INVOICE');" ></asp:TextBox></div><div style="float:left;width:20%;"><asp:ImageButton ID="ImageButton1" runat="server"  ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" /></div></div>

                        <div style="float:left;width:1%;" >&nbsp;</div>
                        <div style="float:left;width:13%;" >SSI</div>
                        <div style="float:left;width:19%;"><asp:TextBox runat="server" ID="txtSSI" CssClass="TextBox" Width="80%" onblur="SearchCustomer(this, 'SSI');"></asp:TextBox></div>
                    </div>
                    <div style="float:left;width:100%;padding-top:1px;" >
                        <div style="float:left;width:1%;" >&nbsp;</div>
                        <div style="float:left;width:13%;" >Receipt</div>
                        <div style="float:left;width:19%;"><div style="float:left; width:80%;"><asp:TextBox runat="server" ID="txtReceipt" CssClass="TextBox" Width="100%"  onblur="SearchCustomer(this, 'RECEIPT');"></asp:TextBox></div> <div style="float:left;width:20%;"><asp:ImageButton ID="ImageButton10" runat="server"  ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" /></div></div>

                        <div style="float:left;width:1%;" >&nbsp;</div>
                        <div style="float:left;width:13%;" >Veh. Receipt</div>
                        <div style="float:left;width:19%;"><div style="float:left; width:80%;"><asp:TextBox runat="server" ID="txtVehReceipt" CssClass="TextBox" Width="100%"  onblur="SearchCustomer(this, 'VEHREGRECEIPT');"></asp:TextBox></div> <div style="float:left;width:20%;"><asp:ImageButton ID="ImageButton2" runat="server"  ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" /></div></div>
                  
                        <div style="float:left;width:1%;" >&nbsp;</div>
                        <div style="float:left;width:13%;" >Ins. Receipt</div>
                        <div style="float:left;width:19%;"><div style="float:left; width:80%;"><asp:TextBox runat="server" ID="txtInsReceipt" CssClass="TextBox" Width="100%" onblur="SearchCustomer(this, 'VEHINSRECEIPT');"></asp:TextBox></div> <div style="float:left;width:20%;"><asp:ImageButton ID="ImageButton3" runat="server"  ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" /></div></div>
                    </div>
                    <div style="float:left;width:100%;padding-top:1px;" >
                        <div style="float:left;width:1%;" >&nbsp;</div>
                        <div style="float:left;width:13%;" >Serial</div>
                        <div style="float:left;width:19%;"><div style="float:left; width:80%;"><asp:TextBox runat="server" ID="txtSerial" CssClass="TextBox" Width="100%" onblur="SearchCustomer(this, 'SERIALNO');"></asp:TextBox></div><div style="float:left;width:20%;"><asp:ImageButton ID="ImageButton4" runat="server"  ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" /></div></div>
                  
                        <div style="float:left;width:1%;" >&nbsp;</div>
                        <div style="float:left;width:13%;" >Veh. Reg.</div>
                        <div style="float:left;width:19%;"><div style="float:left; width:80%;"><asp:TextBox runat="server" ID="txtVehRegistration" CssClass="TextBox" Width="100%" onblur="SearchCustomer(this, 'VEHREGISTRATION');"></asp:TextBox></div> <div style="float:left;width:20%;"><asp:ImageButton ID="ImageButton5" runat="server"  ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" /></div></div>

                        <div style="float:left;width:1%;" >&nbsp;</div>
                        <div style="float:left;width:13%;" >Del. Order</div>
                        <div style="float:left;width:19%;"><div style="float:left; width:80%;"><asp:TextBox runat="server" ID="txtDelOrder" CssClass="TextBox" Width="100%" onblur="SearchCustomer(this, 'DELIVERYORDER');"></asp:TextBox></div> <div style="float:left;width:20%;"><asp:ImageButton ID="ImageButton6" runat="server"  ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" /></div></div>
                    </div>
                    <div style="float:left;width:100%;padding-top:1px;" >
                        <div style="float:left;width:1%;" >&nbsp;</div>
                        <div style="float:left;width:13%;" >Return Nt.</div>
                        <div style="float:left;width:19%;"><div style="float:left; width:80%;"><asp:TextBox runat="server" ID="txtSRN" CssClass="TextBox" Width="100%"  onblur="SearchCustomer(this, 'SALESRETURN');"></asp:TextBox></div><div style="float:left;width:20%;"><asp:ImageButton ID="ImageButton7" runat="server"  ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" /></div></div>
                  
                        <div style="float:left;width:1%;" >&nbsp;</div>
                        <div style="float:left;width:13%;" >Cover Nt.</div>
                        <div style="float:left;width:19%;"><div style="float:left; width:80%;"><asp:TextBox runat="server" ID="txtCoverNt" CssClass="TextBox" Width="100%"  onblur="SearchCustomer(this, 'COVERTNOTE');"></asp:TextBox></div> <div style="float:left;width:20%;"><asp:ImageButton ID="ImageButton8" runat="server"  ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" /></div></div>

                        <div style="float:left;width:1%;" >&nbsp;</div>
                        <div style="float:left;width:13%;" >Customer</div>
                        <div style="float:left;width:19%;"><div style="float:left; width:80%;"><asp:TextBox runat="server" ID="txtCustomer" CssClass="TextBox" Width="100%"  onblur="SearchCustomer(this, 'CUSTOMER');"></asp:TextBox></div> <div style="float:left;width:20%;"><asp:ImageButton ID="ImageButton9" runat="server"  ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" /></div></div>
                    </div>                 
            </div>
        </div>
        <div style="float:left; width:1%;"> &nbsp;</div>
        <div style="float:left; width:39%; height:90px;">
            <div class="PanelHeader invcollapsovrid">Accounts</div>
            <asp:Panel runat="server" ID="pnlAccount" ScrollBars="Auto" Width="100%" Height="72px" >
                <div style="float:left;width:100%;" id="divMultiAccount"> </div>
            </asp:Panel>
        </div>
    </div>

    <div style="float:left; width:100%;height:1px;"> &nbsp;</div>

    <div style="float:left; width:43%;">
        <div style="float:left; width:100%;height:270px;">
            <div class="PanelHeader invcollapsovrid">Customer Detail</div>
            <div style="float:left;width:100%;padding-top:1px;background-color: #EFF3FB;" >
                <div style="float:left;width:1%;" >&nbsp;</div>
                <div style="float:left;width:20%;" >Name</div>
                <div style="float:left;width:1%;" >:</div>
                <div style="float:left;width:1%;" >&nbsp;</div>
                <div style="float:left;width:77%;" id="divCName" ></div>
            </div>
            <div style="float:left;width:100%;padding-top:1px;" >
                <div style="float:left;width:1%;" >&nbsp;</div>
                <div style="float:left;width:20%;" >NIC/PP/DL</div>
                <div style="float:left;width:1%;" >:</div>
                <div style="float:left;width:1%;" >&nbsp;</div>
                <div style="float:left;width:77%;" id="divCSSI" ></div>
            </div>
            <div style="float:left;width:100%;padding-top:1px;background-color: #EFF3FB;" >
                <div style="float:left;width:1%;" >&nbsp;</div>
                <div style="float:left;width:20%;" >DOB</div>
                <div style="float:left;width:1%;" >:</div>
                <div style="float:left;width:1%;" >&nbsp;</div>
                <div style="float:left;width:27%;" id="divCDOB" ></div>

                <div style="float:left;width:1%;" >&nbsp;</div>
                <div style="float:left;width:20%;" >Mobile</div>
                <div style="float:left;width:1%;" >:</div>
                <div style="float:left;width:1%;" >&nbsp;</div>
                <div style="float:left;width:27%;" id="divCMobile" ></div>
            </div>
            <div style="float:left;width:100%;padding-top:1px; padding-bottom:1px;" >
                <div style="float:left;width:1%;" >&nbsp;</div>
                <div style="float:left;width:20%;" >Address</div>
                <div style="float:left;width:1%;" >:</div>
                <div style="float:left;width:1%;" >&nbsp;</div>
                <div style="float:left;width:77%;overflow: auto; overflow-style: marquee; height:32px;" id="divCAddress" ></div>
            </div>

            <div style="float:left;width:100%;padding-top:1px;border-top:1px solid #9E9E9E;"  >
                <asp:TabContainer runat="server" ID="tab1" Height="265px">
                    <%--Home Detail--%>
                    <asp:TabPanel runat="server" ID="tabp1" HeaderText="Home">
                        <HeaderTemplate> Home </HeaderTemplate>
                        <ContentTemplate>
                            <div style="float:left;width:94%; height:110px;" class="tabPanelCss">
                                <div style="float:left;width:100%;padding-top:3px;" >
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:23%;" >Town</div>
                                    <div style="float:left;width:1%;" >:</div>
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:24%;" id="divHTown" >&nbsp;</div>

                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:23%;" >District</div>
                                    <div style="float:left;width:1%;" >:</div>
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:24%;" id="divHDistrict" >&nbsp;</div>
                                </div>
                                <div style="float:left;width:100%;padding-top:3px;" >
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:23%;" >Postal Code</div>
                                    <div style="float:left;width:1%;" >:</div>
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:24%;" id="divHPostal" >&nbsp;</div>

                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:23%;" >Province</div>
                                    <div style="float:left;width:1%;" >:</div>
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:24%;" id="divHProvince" >&nbsp;</div>
                                </div>
                                <div style="float:left;width:100%;padding-top:3px;padding-bottom:3px;" >
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:23%;" >Email</div>
                                    <div style="float:left;width:1%;" >:</div>
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:74%;" id="divHEmail" >&nbsp;</div>
                                </div>
                                <div style="Width:100%; color:Black; padding-bottom:1px; padding-top:1px; font-weight:bold;">Present Detail___________________________</div>
                                <div style="float:left;width:100%;padding-top:3px; padding-bottom:3px;" >
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:23%;" >Address</div>
                                    <div style="float:left;width:1%;" >:</div>
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:74%;" id="divPAddress" >&nbsp;</div>
                                </div>
                                <div style="float:left;width:100%;padding-top:3px;" >
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:23%;" >Town</div>
                                    <div style="float:left;width:1%;" >:</div>
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:24%;" id="divPTown" >&nbsp;</div>

                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:23%;" >District</div>
                                    <div style="float:left;width:1%;" >:</div>
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:24%;" id="divPDistrict" >&nbsp;</div>
                                </div>
                                <div style="float:left;width:100%;padding-top:3px;" >
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:23%;" >Postal Code</div>
                                    <div style="float:left;width:1%;" >:</div>
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:24%;" id="divPPostal" >&nbsp;</div>

                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:23%;" >Province</div>
                                    <div style="float:left;width:1%;" >:</div>
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:24%;" id="divPProvince" >&nbsp;</div>
                                </div>
                            </div>  
                        </ContentTemplate>
                    </asp:TabPanel>
                    <%--Work Place--%>
                    <asp:TabPanel runat="server" ID="tabp2"  HeaderText="Work" >
                        <HeaderTemplate> Work </HeaderTemplate>
                        <ContentTemplate>
                            <div style="float:left;width:94%;height:110px;" class="tabPanelCss">
                                <div style="float:left;width:100%;padding-top:3px;" >
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:23%;" >Name</div>
                                    <div style="float:left;width:1%;" >:</div>
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:74%;" id="divWName" >&nbsp;</div>
                                </div>
                                <div style="float:left;width:100%;padding-top:3px;" >
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:23%;" >Address</div>
                                    <div style="float:left;width:1%;" >:</div>
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:74%;overflow: auto; overflow-style: marquee;" id="divWAddress" >&nbsp;</div>
                                </div>
                                <div style="float:left;width:100%;padding-top:3px;" >
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:23%;" >Phone</div>
                                    <div style="float:left;width:1%;" >:</div>
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:24%;" id="divWPhone" >&nbsp;</div>

                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:23%;" >Fax</div>
                                    <div style="float:left;width:1%;" >:</div>
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:24%;" id="divWFax" >&nbsp;</div>
                                </div>
                                <div style="float:left;width:100%;padding-top:3px;" >
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:23%;" >Department</div>
                                    <div style="float:left;width:1%;" >:</div>
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:24%;" id="divWDepartment" >&nbsp;</div>

                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:23%;" >Profession</div>
                                    <div style="float:left;width:1%;" >:</div>
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:24%;" id="divWProfession" >&nbsp;</div>
                                </div>
                                <div style="float:left;width:100%;padding-top:3px;" >
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:23%;" >Designation</div>
                                    <div style="float:left;width:1%;" >:</div>
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:74%;" id="divWDesignation" >&nbsp;</div>
                                </div>
                                <div style="float:left;width:100%;padding-top:3px;" >
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:23%;" >Email</div>
                                    <div style="float:left;width:1%;" >:</div>
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:74%;" id="divWEmail" >&nbsp;</div>
                                </div>
                            </div> 
                        </ContentTemplate>
                    </asp:TabPanel>
                    <%--Tax Detail--%>
                    <asp:TabPanel runat="server" ID="tabp3" HeaderText="Tax" >
                        <HeaderTemplate> Tax </HeaderTemplate>
                        <ContentTemplate>
                            <div style="float:left;width:94%;height:110px;" class="tabPanelCss">
                                <div style="float:left;width:100%;padding-top:3px;" >
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:23%;" >Excempted</div>
                                    <div style="float:left;width:1%;" >:</div>
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:24%;" id="divTExcepted" >&nbsp;</div>

                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:23%;" >Registered</div>
                                    <div style="float:left;width:1%;" >:</div>
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:24%;" id="divTRegistered" >&nbsp;</div>
                                </div>
                                <div style="float:left;width:100%;padding-top:3px; padding-bottom:3px;" >
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:23%;" >Reg. No</div>
                                    <div style="float:left;width:1%;" >:</div>
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:74%;" id="divTRegisterNo" >&nbsp;</div>
                                </div>
                                <div  style="border-bottom-style:none;Width:100%;color:Black; padding-bottom:1px; padding-top:1px; font-weight:bold;">SVAT Detail_____________________________</div>
                                <div style="float:left;width:100%;padding-top:3px;" >
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:23%;" >Registered</div>
                                    <div style="float:left;width:1%;" >:</div>
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:74%;" id="divTSRegistered" >&nbsp;</div>
                                </div>
                                <div style="float:left;width:100%;padding-top:3px;" >
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:23%;" >Reg. No</div>
                                    <div style="float:left;width:1%;" >:</div>
                                    <div style="float:left;width:1%;" >&nbsp;</div>
                                    <div style="float:left;width:74%;" id="divTSRegisterNo" >&nbsp;</div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>

                </asp:TabContainer>
            </div>
        </div>
        <div style="float:left; width:100%;height:1px;"> &nbsp;</div>
        <div style="float:left; width:100%;height:110px;">
            <div class="PanelHeader invcollapsovrid">Guarantor Detail</div>
            <div style="float:left;width:100%;padding-top:1px;background-color: #EFF3FB;" >
                <asp:Panel runat="server" ID="Panel1" ScrollBars="Auto" Width="100%" Height="90px" >
                    <div style="float:left;width:100%;" id="divGuaranter"> </div>
                </asp:Panel>
            </div>
        </div>
        <div style="float:left; width:100%;height:1px;"> &nbsp;</div>
        <div style="float:left; width:100%;height:145px;">
            <div class="PanelHeader invcollapsovrid">Account Summary</div>
            <div style="float:left;width:100%;padding-top:1px;background-color: #EFF3FB;" >
                <asp:Panel runat="server" ID="Panel2" ScrollBars="Auto" Width="100%" Height="120px" >
                    <div style="float:left;width:100%;" id="divAccSummary"> </div>
                </asp:Panel>
            </div>
        </div>
    </div>

   <div style="float:left; width:2%;"> &nbsp;</div>
    <div style="float:left; width:55%;height:443px;">
        <%-- Button Area --%>
        <div style="float:left; width:100%;height:20px;">
            <asp:Button runat="server" ID="btnPayment" Text ="Payment" CssClass="Button" OnClick="btnPaymentClick" Width="60px" /> 
            <asp:Button runat="server" ID="btnProduct" Text ="Product" CssClass="Button" OnClick="btnProductClick" Width="60px"/> 
            <asp:Button runat="server" ID="btnSchedule" Text ="Schedule" CssClass="Button" OnClick="btnScheduleClick" Width="60px"/> 
            <asp:Button runat="server" ID="btnTransfers" Text ="Transfers" CssClass="Button" OnClick="btnTransfersClick" Width="70px"/> 
            <asp:Button runat="server" ID="btnDiriya" Text ="Insu. & Registration" CssClass="Button" OnClick="btnDiriyaClick" Width="115px"/> 
            <asp:Button runat="server" ID="btnCredit" Text ="Credit Sale" CssClass="Button" OnClick="btnCreditClick" Width="70px"/> 
            <asp:Button runat="server" ID="btnBlackList" Text ="Black List" CssClass="Button" OnClick="btnBlackListClick" Width="60px"/> 
        </div>
        <div style="float:left; width:100%;">
        <%-- Payment Area --%>
        <div style="height:372px;" id="divPayment" runat="server"   visible="true">
            <div style="float:left; width:100%;" id="divHSPayment"  runat="server">
                <div style="float:left; width:15%;">&nbsp;</div>
                <div style="float:left; position:static; padding-top:3px;width:70%;"> <AC:uc_HpAccountSummary ID="uc_HpAccountSummary1" runat="server" />
                </div> 
                <div style="float:left; width:15%;">&nbsp;</div>
                <div style="float:left; width:100%; padding-top:10px;">
                    <asp:Panel runat="server" ID="Panel73" ScrollBars="Auto" Width="100%" Height="290px" >
                        <div style="float:left;width:100%;" id="divAccountPayBalance"> </div>
                    </asp:Panel>
                </div>
            </div>
            <div style="float:left; width:100%;" id="divCSPayment"  runat="server" >
                <div style="float:left; width:100%;" id="divCSPayDetail" >
                </div>
            </div>
        </div>
        <%-- Product Area --%>
        <div style="height:372px;" id="divProduct"  runat="server"  visible="false">
            <asp:Panel runat="server" ID="Panel3" ScrollBars="Auto" Width="100%" Height="90px" >
                <div style="float:left;width:100%;"> Product Detail </div>
                <div style="float:left;width:100%;" id="divProductDet"> </div>
            </asp:Panel> 
            <asp:Panel runat="server" ID="Panel4" ScrollBars="Auto" Width="100%" Height="90px" >
                <div style="float:left;width:100%;"> Revert Detail </div>
                <div style="float:left;width:100%;" id="divRevert"> </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="Panel5" ScrollBars="Auto" Width="100%" Height="90px" >
                <div style="float:left;width:100%;"> Revert Release Detail </div>
                <div style="float:left;width:100%;" id="divRevertRelease"> </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="Panel6" ScrollBars="Auto" Width="100%" Height="90px" >
                <div style="float:left;width:100%;"> Exchange Detail </div>
                <div style="float:left;width:100%;" id="divExchange"> </div>
            </asp:Panel>                                                 
        </div>
        <%-- Schedule/Re-Schedule --%>
        <div style="height:372px;" id="divSchedule"  runat="server"  visible="false">
            <div style="float:left;width:100%;" >
                <div style="float:left;width:100%;" >Schedule Detail</div>
                <asp:Panel runat="server" ID="Panel71" ScrollBars="Auto" Width="100%" Height="200px" >
                    <div style="float:left;width:100%;" id="divLSchedule"> </div>
                </asp:Panel>
            </div>
            <div style="float:left;width:100%;" >
                <div style="float:left;width:100%;" >Re-Schedule Detail</div>
                <asp:Panel runat="server" ID="Panel72" ScrollBars="Auto" Width="100%" Height="200px" >
                    <div style="float:left;width:100%;" id="divScheduleHistory"> </div>
                </asp:Panel>
            </div>
        </div>
        <%-- Transfers --%>
        <div style="height:372px;" id="divTransfers"  runat="server"  visible="false">
            <div style="float:left;width:100%;" >
                <div style="float:left;width:100%;" >Location Transfer</div>
                <asp:Panel runat="server" ID="Panel7" ScrollBars="Auto" Width="100%" Height="200px" >
                    <div style="float:left;width:100%;" id="divLocationTrasnfer"> </div>
                </asp:Panel>
            </div>
            <div style="float:left;width:100%;" >
                <div style="float:left;width:100%;" >Customer Trasnfer</div>
                <asp:Panel runat="server" ID="Panel9" ScrollBars="Auto" Width="100%" Height="200px" >
                    <div style="float:left;width:100%;" id="divCustomerTrasnfer"> </div>
                </asp:Panel>
            </div>
        </div>
        <%-- Diriya --%>
        <div style="height:372px;" id="divDiriya"  runat="server" visible="false">
            <div style="float:left;width:100%;" >
                <div style="float:left;width:100%;" >HP Insurance Detail</div>
                <asp:Panel runat="server" ID="Panel13" ScrollBars="Auto" Width="100%" Height="200px" >
                    <div style="float:left;width:100%;" id="divDiriyaDet"> </div>
                </asp:Panel>
            </div>
            <div style="float:left;width:100%;" >
                <div style="float:left;width:100%;" >Vehicle Insurance Detail</div>
                <asp:Panel runat="server" ID="Panel131" ScrollBars="Auto" Width="100%" Height="200px" >
                    <div style="float:left;width:100%;" id="divVhlInsu"> </div>
                </asp:Panel>
            </div>
        </div>
        <%-- Credit Sales --%>
        <div style="height:372px;" id="divCredit"  runat="server" visible="false">
            <div style="float:left; width:100%;">
                <%-- Out Stands --%>
                <div style="float:left; width:100%;">
                    <asp:Panel runat="server" ID="Panel8" ScrollBars="Auto" Width="100%" Height="190px" >
                        <div style="float:left;width:100%;" id="divCrOutStand"> </div>
                    </asp:Panel>
                </div>
            </div>
            <div style="float:left; width:100%;">
                <%-- Summary --%>
                <div style="float:left; width:20%;">
                    <asp:Panel runat="server" ID="Panel10" ScrollBars="Auto" Width="100%" Height="190px" >
                        <div style="float:left;width:100%;" id="divCrSummary"> </div>
                    </asp:Panel>
                </div>
                <div style="float:left; width:2%;">&nbsp;</div>
                <%-- Detail by Selected area --%>
                <div style="float:left; width:78%;">
                    <asp:Panel runat="server" ID="Panel11" ScrollBars="Auto" Width="100%" Height="190px" >
                        <div style="float:left;width:100%;" id="divCrReceipt"> </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
        <%-- Black List Process --%>
        <div style="height:372px;" id="divBlackList"  runat="server" visible="false">
            <div style="float:left; width:100%;" >
                <div style="float:left; width:50%;" ><asp:RadioButton runat="server" ID="radHeadoffice" Text="By Head Office" GroupName="puka" /></div>
                <div style="float:left; width:50%;" ><asp:RadioButton runat="server" ID="radShowroom" Text="By Showroom" GroupName="puka" Checked="true" /></div>
            </div>
            <div style="float:left; width:100%;" >
                <asp:Panel runat="server" ID="Panel12" ScrollBars="Auto" Width="100%" Height="190px" >
                    <div style="float:left;width:100%;" id="divHireAccountBal"> </div>
                </asp:Panel>
            </div>
            <div style="float:left; width:100%;" >
                <div style=" float:left; width:10%" > Value </div>
                <div style=" float:left; width:90%" > <asp:TextBox ID="txtBlkDefValue" runat="server" CssClass="TextBox" Text="" Width="80%"></asp:TextBox> </div>
            </div>
            <div style="float:left; width:100%;" >
                <div style=" float:left; width:10%" > Reason </div>
                <div style=" float:left; width:90%" > <asp:TextBox ID="txtBlkReason" runat="server" CssClass="TextBox" Text="" Width="80%"></asp:TextBox> </div>                    
            </div>
            <div style="float:left; width:100%;" >
                <asp:Button ID="btnBlackListSave" runat="server" CssClass="Button" Text="Black List" Width="60px" />
            </div>

        </div>
        </div>
    </div>
   <asp:HiddenField Value="" ID="hdnDocumentNo" runat="server" ClientIDMode="Static" />
   <asp:HiddenField Value="" ID="hdnDocumentType" runat="server" ClientIDMode="Static" />
   <asp:HiddenField Value="" ID="hdnCustomer" runat="server" ClientIDMode="Static" />
   <asp:HiddenField Value="" ID="hdnSearchType" runat="server" ClientIDMode="Static" />
   <asp:HiddenField Value="" ID="hdnSearchValue" runat="server" ClientIDMode="Static" />
    <asp:HiddenField Value="" ID="hdnIsHpAccountFire" runat="server" ClientIDMode="Static" />
</div>
<%--</ContentTemplate>
</asp:UpdatePanel>--%>

<%-- Control Area --%>
<div style="display: none;">
   <asp:Button runat="server" ID="btnGetHpPayment" OnClick="GetHpPayment"   />
</div>

</asp:Content>
