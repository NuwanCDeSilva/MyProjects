<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_CommonSearch.ascx.cs"
    Inherits="FF.WebERPClient.UserControls.uc_CommonSearch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<script type="text/javascript" >


    //Close the Popup.
    function ClosePopup() {
        $find("<%= ucmdpExtender.ClientID %>").hide();
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
        var Cells = selectedRow.getElementsByTagName("td");
        var _resultString = '';
        //-------------------------------Added by Prabhath
        for (var i = 0; i < Cells.length-1; ++i) {
            if (_resultString == '')
                _resultString = Cells[i].innerText;
            else
            _resultString +=  '|' + Cells[i].innerText ;
        }
        FF.WebERPClient.LocalWebServices.CustomSessionProvider.SetGlbSearchResult(_resultString, OnStatusCell, OnFailCell);
        //-------------------------------Added by Prabhath
        var selectedValue = Cells[0].innerText;

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
        if (resultObjectName.value == 'txtMasterUserLocation') {
            window.location.href = '/Default.aspx';
        }

        if (resultObjectName.value == 'txtMasterProfitCenters') {
            window.location.href = '/Default.aspx';
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


        //NOTE:Call the common web service method as script, for all search modules.
        //Need correct format for Web service method. ex:-(Namespace.className.methodname(params,onSucceededCallback,onFailedCallback).
        FF.WebERPClient.LocalWebServices.CommonSearchWebServive.GetCommonSearchDetails(initialParams, searchCatergory, searchText, onSucceeded, onFailed);
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
        var txtMasterUserLocation = document.getElementById("txtMasterUserLocation");
        var txtMasterProfitCenters = document.getElementById("txtMasterProfitCenters");

        if (txtMasterUserLocation != null) {
            FF.WebERPClient.LocalWebServices.CustomSessionProvider.SetSession("UserDefLoca", txtMasterUserLocation.value, onSucceeded, onFailed);
        }
        
        if (txtMasterProfitCenters != null) {
            FF.WebERPClient.LocalWebServices.CustomSessionProvider.SetSession("UserDefProf", txtMasterProfitCenters.value, onSucceeded, onFailed);
            //alert(txtMasterProfitCenters.value)
        }
    }

    function ClearPopUp()
    {
      document.getElementById("<%= txtSearchText.ClientID %>").value = "";
    }



</script>
<asp:ModalPopupExtender ID="ucmdpExtender" runat="server" TargetControlID="lnkbtnDummy" ClientIDMode="Static"
    PopupControlID="panEdit" BackgroundCssClass="modalBackground" CancelControlID="imgbtnClose"
    PopupDragHandleControlID="divpopHeader">
</asp:ModalPopupExtender>
<asp:Panel ID="panEdit" runat="server" Height="450px" Width="650px" CssClass="ModalWindow">
    <div class="popUpHeader" id="divpopHeader">
        <div style="float: left; width: 80%">
            Search Header</div>
        <div style="float: left; width: 20%; text-align: right">
            <asp:ImageButton ID="imgbtnClose" runat="server" ImageUrl="~/Images/0032-01-16x16x16.ICO" />&nbsp;
        </div>
    </div>
    <asp:Panel ID="pnlSerachCritria" runat="server" GroupingText="Search Critria" Width="75%"
        CssClass="subPanelCss">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    Search By :
                </td>
                <td>
                    <asp:DropDownList ID="ddlSearchCatergory" runat="server" ClientIDMode="Static">
                    </asp:DropDownList>
                </td>
                <td style="width: 25px">
                    &nbsp;
                </td>
                <td>
                    Search Word :
                </td>
                <td>
                    <asp:TextBox ID="txtSearchText" runat="server" ClientIDMode="Static" onkeyup="GetCommonSearchDetails()"></asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlSearchRsults" runat="server" GroupingText="Search Results" Width="98%"
        CssClass="subPanelCss">
        <asp:Panel ID="Panel1" runat="server" Width="98%" Height="250px" ScrollBars="Auto">
            <div id="divResults" runat="server" ClientIDMode="Static">
            </div>
        </asp:Panel>
    </asp:Panel>
    <asp:LinkButton ID="lnkbtnDummy" runat="server"></asp:LinkButton>
    <asp:HiddenField ID="hdnResultControl" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnSearchParams" ClientIDMode="Static" runat="server" />
</asp:Panel>
