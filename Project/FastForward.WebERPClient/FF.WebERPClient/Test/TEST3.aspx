<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="TEST3.aspx.cs" Inherits="FF.WebERPClient.Test.TEST3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" language="javascript">


        function GetCommon() {
            FF.WebERPClient.LocalWebServices.CommonSearchWebServive.TestMethod(onSucceeded, onFailed);
        
        }

        //SucceededCallback method.
        function onSucceeded(result) {
            alert(result);
            var divResults = document.getElementById("<%= divResults.ClientID %>");        
            divResults.innerHTML = result;

        }

        //FailedCallback method.
        function onFailed(error) {
            var divResults = document.getElementById("<%= divResults.ClientID %>");
            divResults.innerText = "No data- error";

        }
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="sd" runat="server">
        <ContentTemplate>
            <div id="divResults" runat="server" clientidmode="Static">
                
            </div>
            <div>
                <asp:Button ID="btnBut" Text="Ok"  runat="server" />
            </div>
            <div>
                <asp:TextBox ID="txt" runat="server" AutoPostBack="false" onblur="GetCommon();"></asp:TextBox>
            </div>
            <br />

            <div id="displayUserPerm" runat="server">           
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
