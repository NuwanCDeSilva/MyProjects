<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="WebForm1.aspx.cs" Inherits="FF.WebERPClient.WebForm1" %>

<%@ Register Src="UserControls/uc_CommonSearch.ascx" TagName="uc_CommonSearch" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

<script type="text/javascript" language="javascript">

    function TableRowClick(rowIndex) 
    {
        var tabRowId = "tabRow" + rowIndex;
        var selectedRow = document.getElementById(tabRowId);
        var Cells = selectedRow.getElementsByTagName("td");
        var val = Cells[0].innerText;
        alert(val);       
    }

</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
        <br />
        <uc1:uc_CommonSearch ID="uc_CommonSearch1" runat="server" />
    </div>
    <br />
    <div id="divResult" runat="server">
    </div>
    <hr />
    <div id="MainContent_divResult">
        <%--<table border='1px' cellpadding='2' cellspacing='0' style='border: solid 1px Black;
            font-size: small;'>
            <tr align='left' valign='top'>
                <td align='left' valign='top'>
                    Company Code
                </td>
                <td align='left' valign='top'>
                    Description
                </td>
                <td align='left' valign='top'>
                    Currency
                </td>
                <td align='left' valign='top'>
                    CreatedBy
                </td>
                <td align='left' valign='top'>
                    SessionId
                </td>
            </tr>
            <tr align='left' valign='top' onmouseover="this.style.background=&#39;#eeff00&#39;" onmouseout="this.style.background=&#39;#ffffff&#39;"
                    onclick="TableRowClick(0)" id="tabRow0">
                <td align='left' valign='top'>
                    RPL
                </td>
                <td align='left' valign='top'>
                    Abans Retail (Pvt) Ltd
                </td>
                <td align='left' valign='top'>
                    LKR
                </td>
                <td align='left' valign='top'>
                    SCM
                </td>
                <td align='left' valign='top'>
                    123456789
                </td>
            </tr>
            <tr align='left' valign='top'onmouseover="this.style.background=&#39;#eeff00&#39;" onmouseout="this.style.background=&#39;#ffffff&#39;"
                    onclick="TableRowClick(1)" id="tabRow1">
                <td align='left' valign='top'>
                    ABL
                </td>
                <td align='left' valign='top'>
                    Abans (Pvt) Ltd
                </td>
                <td align='left' valign='top'>
                    LKR
                </td>
                <td align='left' valign='top'>
                    SCM
                </td>
                <td align='left' valign='top'>
                    123456789
                </td>
            </tr>
            <tr align='left' valign='top' onmouseover="this.style.background=&#39;#eeff00&#39;" onmouseout="this.style.background=&#39;#ffffff&#39;"
                    onclick="TableRowClick(2)" id="tabRow2">
                <td align='left' valign='top'>
                    AZE
                </td>
                <td align='left' valign='top'>
                    A-Z Electronics (Pvt) Ltd
                </td>
                <td align='left' valign='top'>
                    LKR
                </td>
                <td align='left' valign='top'>
                    SCM
                </td>
                <td align='left' valign='top'>
                    123456789
                </td>
            </tr>
            <tr align='left' valign='top' onmouseover="this.style.background=&#39;#eeff00&#39;" onmouseout="this.style.background=&#39;#ffffff&#39;"
                    onclick="TableRowClick(3)" id="tabRow3">
                <td align='left' valign='top'>
                    ABE
                </td>
                <td align='left' valign='top'>
                    Abans Electronics (Pvt) Ltd
                </td>
                <td align='left' valign='top'>
                    LKR
                </td>
                <td align='left' valign='top'>
                    SCM
                </td>
                <td align='left' valign='top'>
                    123456789
                </td>
            </tr>
        </table>--%>
    </div>

    <br /><br />
    <%--<table cellspacing="0" rules="all" border="1" id="htmlMaintable"
        style="border-collapse: collapse;">
        <tr>
            <th scope="col">
                Company Code
            </th>
            <th scope="col">
                Description
            </th>
            <th scope="col">
                Currency
            </th>
            <th scope="col">
                CreatedBy
            </th>
            <th scope="col">
                SessionId
            </th>
        </tr>
        <tr  onmouseover="this.style.background=&#39;#eeff00&#39;" onmouseout="this.style.background=&#39;#ffffff&#39;"
            onclick="TableRowClick(0)" id="tabRow0">
            <td>
                RPL
            </td>
            <td>
                Abans Retail (Pvt) Ltd
            </td>
            <td>
                LKR
            </td>
            <td>
                SCM
            </td>
            <td>
                123456789
            </td>
        </tr>
        <tr onmouseover="this.style.background=&#39;#eeff00&#39;" onmouseout="this.style.background=&#39;#ffffff&#39;"
            onclick="TableRowClick(1)" id="tabRow1">
            <td>
                ABL
            </td>
            <td>
                Abans (Pvt) Ltd
            </td>
            <td>
                LKR
            </td>
            <td>
                SCM
            </td>
            <td>
                123456789
            </td>
        </tr>
        <tr onmouseover="this.style.background=&#39;#eeff00&#39;" onmouseout="this.style.background=&#39;#ffffff&#39;"
            onclick="TableRowClick(2)" id="tabRow2">
            <td>
                AZE
            </td>
            <td>
                A-Z Electronics (Pvt) Ltd
            </td>
            <td>
                LKR
            </td>
            <td>
                SCM
            </td>
            <td>
                123456789
            </td>
        </tr>
        <tr onmouseover="this.style.background=&#39;#eeff00&#39;" onmouseout="this.style.background=&#39;#ffffff&#39;"
            onclick="TableRowClick(3)" id="tabRow3">
            <td>
                ABE
            </td>
            <td>
                Abans Electronics (Pvt) Ltd
            </td>
            <td>
                LKR
            </td>
            <td>
                SCM
            </td>
            <td>
                123456789
            </td>
        </tr>
    </table>--%>
</asp:Content>
