<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HPAllDueSummaryPrint.aspx.cs" Inherits="FF.WebERPClient.Reports_Module.HP_Rep.HPAllDueSummaryPrint" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
     <script type="text/javascript">
         function RedirectToOrigin() {
             var hdn = document.getElementById('hdnValidator');
             hdn.value = '1';
         }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     <asp:UpdatePanel runat="server" ID="upPrint"> <ContentTemplate>
            <asp:Button ID="btnBack" runat="server" OnClientClick="RedirectToOrigin()" Text="Back"/>
    </ContentTemplate></asp:UpdatePanel>
    <asp:HiddenField ID="hdnValidator" runat="server" Value="0"  ClientIDMode="Static" />
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
        AutoDataBind="True" EnableParameterPrompt="False" GroupTreeImagesFolderUrl="" 
        Height="50px" ReportSourceID="CrystalReportSource1" ToolbarImagesFolderUrl="" 
        ToolPanelView="None" ToolPanelWidth="200px" Width="350px" />

    <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
        <Report FileName="executive_wise_sales.rpt">
        </Report>
    </CR:CrystalReportSource>
    </form>
</body>
</html>

