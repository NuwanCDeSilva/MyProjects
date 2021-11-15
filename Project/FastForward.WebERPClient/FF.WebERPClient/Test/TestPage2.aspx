<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="TestPage2.aspx.cs" Inherits="FF.WebERPClient.TestPage2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" language="javascript">

        function GetCommonSearchData() 
        {
            //alert("GetCommonSearchData In");
            FF.WebERPClient.LocalWebServices.CommonSearchWebServive.TestMethod(onSucceeded, onFailed);         
            //alert("GetCommonSearchData Out");
        }

        function onSucceeded(result, userContext, methodName) {
            $get('divResult').innerHTML = result;
        }
        function onFailed(error, userContext, methodName) {
            $get('divResult').innerText = "An error Occoured";
        }
    </script>
    <br />
    <input id="btnSearch" type="button" value="Search" onclick="GetCommonSearchData()" />
    <hr />
    <br />
    <div id="divResult">
    </div>
</asp:Content>
