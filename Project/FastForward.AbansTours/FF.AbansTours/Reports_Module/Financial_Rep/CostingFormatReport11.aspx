<%@ Page Title="" Language="C#" MasterPageFile="~/Basic.Master" AutoEventWireup="true"
    CodeBehind="CostingFormatReport11.aspx.cs" Inherits="FF.AbansTours.Reports_Module.Financial_Rep.CostingFormatReport11" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style3 {
            width: 288px;
        }

        .style4 {
            width: 76px;
        }

        .style5 {
            width: 317px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div visible="true">
        <div>       
            <%--<CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True" GroupTreeImagesFolderUrl="" Height="50px" ReportSourceID="CrystalReportSource1" ToolbarImagesFolderUrl="" ToolPanelView="None" ToolPanelWidth="200px" Width="350px" />--%>
            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                <Report FileName="CostingFormat_Report.rpt">
                </Report>
            </CR:CrystalReportSource>
            <%--<CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                <Report FileName="CostingFormat_Report.rpt">
                </Report>
            </CR:CrystalReportSource>--%>
        </div>
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
