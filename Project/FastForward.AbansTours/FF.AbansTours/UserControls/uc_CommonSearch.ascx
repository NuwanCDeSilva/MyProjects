<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_CommonSearch.ascx.cs"
    Inherits="FF.AbansTours.UserControls.uc_CommonSearch" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<script type="text/javascript">

    //Close the Popup.
    function ClosePopup() {
        $find("<%= ucmdpExtender.ClientID %>").hide();
        //        window.location.href = 'Default.aspx';
        ClearPopUp();
    }

    //Show the Popup.
    function ShowPopup() {
        $find("<%= ucmdpExtender.ClientID %>").show();

    }


    //Method for html table row click event.
    function TableRowClick(rowIndex) {
        //Get previous value of location text box / profit center text box
        //Get the selected cell value using selected rowIndex.
        var tabRowId = "tabRow" + rowIndex;

        var selectedRow = document.getElementById(tabRowId);
        //alert(selectedRow);

        var Cells = selectedRow.getElementsByTagName("td");
        var _resultString = '';

        //-------------------------------Added by Prabhath
        for (var i = 0; i < Cells.length - 1; ++i) {

            if (_resultString == '')
                _resultString = Cells[i].innerHTML;
            else
                _resultString += '|' + Cells[i].innerHTML;

            //alert(_resultString);

        }
        FF.AbansTours.LocalWebServices.CustomSessionProvider.SetGlbSearchResult(_resultString, OnStatusCell, OnFailCell);
        //-------------------------------Added by Prabhath
        //        var selectedValue = Cells[0].innerText;
        var selectedValue = Cells[0].innerHTML;

        //Get the result object Name using hidden field.
        var resultObjectName = document.getElementById("<%= hdnResultControl.ClientID %>");
        //alert(resultObjectName.value);

        //Get the result object and set the value.
        var resultObject = document.getElementById(resultObjectName.value);
        resultObject.value = selectedValue;

        //Set focus to result object.
        if (!resultObject.disabled) {
            resultObject.focus();
        }

        //Update the Session data for master details.
        UpdateSessionData();

        //Clear PopUp data.
        ClearPopUp();


        //Close modal popup.
        ClosePopup();

        //Set Main page after change location or proft center (Chamal 28/06/2012)

        if (resultObjectName.value == 'lblLoc') {
            alert("Location Changed");

            window.location.href = '../Default.aspx';
        }

        if (resultObjectName.value == 'lblPC') {
            alert("Profit Center Changed Changed");
            window.location.href = '../Default.aspx';
        }
    }

    function OnStatusCell() {
    }

    function OnFailCell() {

    }

    //This is a common javascript method for all search modules.
    function GetCommonSearchDetails() {
        //Get the initial parameters set by parent page.
        var initialParams = document.getElementById("<%= hdnSearchParams.ClientID %>").value;

        //Get the current search Catergory from drop down list.
        var searchCatergory = document.getElementById("<%= ddlSearchCatergory.ClientID %>").value;

        //Get the current search text from textbox.
        var searchText = document.getElementById("<%= txtSearchText.ClientID %>").value;

        var dateEnable = document.getElementById("<%= hdnDateEnable.ClientID %>").value;


        //NOTE:Call the common web service method as script, for all search modules.
        //Need correct format for Web service method. ex:-(Namespace.className.methodname(params,onSucceededCallback,onFailedCallback).

        if (dateEnable == "DATE") {

            var fromDate = document.getElementById("<%= txtFromDate.ClientID %>").value;

            var toDate = document.getElementById("<%= txtToDate.ClientID %>").value;

            //document.getElementById("<%= txtSearchText.ClientID %>").value = fromDate;

            //FF.AbansTours.LocalWebServices.CommonSearchWebServive.GetCommonSearchDetails(initialParams, searchCatergory, searchText, onSucceeded, onFailed);

            FF.AbansTours.LocalWebServices.CommonSearchWebServive.GetCommonSearchDetailsDate(initialParams, searchCatergory, searchText, fromDate, toDate, onSucceeded, onFailed);
        }
        else {
            FF.AbansTours.LocalWebServices.CommonSearchWebServive.GetCommonSearchDetails(initialParams, searchCatergory, searchText, onSucceeded, onFailed);
        }

    }

    //SucceededCallback method.
    function onSucceeded(result) {
        var divResults = document.getElementById("<%= divResults.ClientID %>");
        divResults.innerHTML = result;
    }

    //FailedCallback method.
    function onFailed(error) {
        var divResults = document.getElementById("<%= divResults.ClientID %>");
        divResults.innerText = "";
    }

    //NOTE : This method used to update the Session variable from JavaScript.
    function UpdateSessionData() {
        var txtMasterUserLocation = document.getElementById("lblLoc");
        var txtMasterProfitCenters = document.getElementById("lblPC");

        if (txtMasterUserLocation != null) {
            FF.AbansTours.LocalWebServices.CustomSessionProvider.SetSession("UserDefLoca", txtMasterUserLocation.value, onSucceeded, onFailed);
        }

        if (txtMasterProfitCenters != null) {
            FF.AbansTours.LocalWebServices.CustomSessionProvider.SetSession("UserDefProf", txtMasterProfitCenters.value, onSucceeded, onFailed);
        }
    }

    function ClearPopUp() {
        document.getElementById("<%= txtSearchText.ClientID %>").value = "";
    }
</script>
<style type="text/css">
    .modalBackground {
        background-color: #CCCCFF;
        filter: alpha(opacity=40);
        opacity: 0.5;
    }

    .ModalWindow {
        border: solid 1px #c0c0c0;
        background: #f0f0f0;
        padding: 0px 0px 0px 0px;
        position: absolute;
        top: -1000px;
        border: 0px rgb(89,89,89) solid;
        background: rgb(254, 254, 254);
        -moz-box-shadow: 0px 0px 34px 16px rgb(128,128,128);
        -webkit-box-shadow: 0px 0px 34px 16px rgb(128,128,128);
        box-shadow: 0px 0px 34px 16px rgb(128,128,128);
        padding: 2px;
    }

    .popUpHeader {
        background-color: Navy;
        color: White;
        width: 100%;
        height: 20px;
        float: left;
    }

    .subPanelCss {
        padding: 0px 0px 0px 0px; /*padding: 0px 10px 0px 10px;*/
    }

    .SearchHeader {
        width: 100%;
        height: 36px;
        border: 1px rgb(89,89,89) solid;
        background: rgb(98, 98, 98);
        color: rgb(255, 255, 255);
        font-size: 20px;
        font-weight: inherit;
        font-family: inherit;
        font-style: inherit;
        text-decoration: inherit;
        text-align: left;
        line-height: 1.3em;
    }

    .sortable tr {
        cursor: pointer;
    }

    .sortable td {
        padding: 0 10px 0 2px;
    }
</style>
<asp:ModalPopupExtender ID="ucmdpExtender" runat="server" TargetControlID="lnkbtnDummy"
    ClientIDMode="Static" PopupControlID="panEdit" BackgroundCssClass="modalBackground"
    CancelControlID="imgbtnClose" PopupDragHandleControlID="divpopHeader">
</asp:ModalPopupExtender>
<asp:Panel ID="panEdit" runat="server" Height="450px" Width="740px" CssClass="ModalWindow">
    <div class="SearchHeader" id="divpopHeader" align="left">
        <div style="float: left; width: 95%">
            Search Header
        </div>
        <div style="float: left; width: 5%; text-align: right; background-color: White">
            <asp:ImageButton ID="imgbtnClose" runat="server" ImageUrl="~/images/uploadify-cancel.png" />&nbsp;
        </div>
    </div>
    &nbsp;
    <asp:Panel ID="pnlSerachCritria" runat="server" GroupingText="Search Criteria" Width="100%"
        CssClass="subPanelCss" align="left">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>Search By :
                </td>
                <td>
                    <asp:DropDownList ID="ddlSearchCatergory" runat="server" ClientIDMode="Static">
                    </asp:DropDownList>
                </td>
                <td style="width: 25px">&nbsp;
                </td>
                <td>Search Word :
                </td>
                <td>
                    <asp:TextBox ID="txtSearchText" runat="server" ClientIDMode="Static" onkeyup="GetCommonSearchDetails()"
                        OnTextChanged="txtSearchText_TextChanged"></asp:TextBox>
                </td>
            </tr>
            <br />
            <tr>
                <asp:Panel ID="pnlDate" runat="server" Visible="false">
                    <td>From Date :
                    </td>
                    <td>
                        <asp:TextBox ID="txtFromDate" runat="server" ClientIDMode="Static" Enabled="false"
                            onchange="GetCommonSearchDetails()"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFromDate"
                            PopupButtonID="imgbtnFromDate" Format="dd-MMM-yyyy">
                        </asp:CalendarExtender>
                        <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/calendar.png"
                            ImageAlign="Middle" CssClass="imageicon" />
                    </td>
                    <td style="width: 25px">&nbsp;
                    </td>
                    <td>To Date :
                    </td>
                    <td>
                        <asp:TextBox ID="txtToDate" runat="server" ClientIDMode="Static" Enabled="false"
                            onchange="GetCommonSearchDetails()"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDate"
                            PopupButtonID="imgbtnToDate" Format="dd-MMM-yyyy">
                        </asp:CalendarExtender>
                        <asp:ImageButton ID="imgbtnToDate" runat="server" ImageUrl="~/Images/calendar.png"
                            ImageAlign="Middle" CssClass="imageicon" />
                    </td>
                </asp:Panel>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlSearchRsults" runat="server" GroupingText="Search Results" Width="98%"
        CssClass="subPanelCss" />
    <asp:Panel ID="Panel1" runat="server" Width="98%" Height="230px" ScrollBars="Both"
        CssClass="subPanelCss">
        <div id="divResults" runat="server" class="sortable">
        </div>
    </asp:Panel>
    <asp:LinkButton ID="lnkbtnDummy" runat="server"></asp:LinkButton>
    <asp:HiddenField ID="hdnResultControl" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnSearchParams" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnDateEnable" ClientIDMode="Static" runat="server" />
</asp:Panel>
