<%@ Page Title="" Language="C#" MasterPageFile="~/Basic.Master" AutoEventWireup="true"
    CodeBehind="CostingFormatReport.aspx.cs" Inherits="FF.AbansTours.CostingFormatReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div visible="true">
                <div>
                    <CR:CrystalReportViewer ID="cryrCostingReport" runat="server" AutoDataBind="True" GroupTreeImagesFolderUrl="" Height="1202px" ReportSourceID="CrystalReportSource1" ToolbarImagesFolderUrl="" ToolPanelWidth="200px" Width="1104px" />

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
