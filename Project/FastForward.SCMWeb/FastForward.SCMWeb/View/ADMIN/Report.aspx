<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="FastForward.SCMWeb.View.ADMIN.Report" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

     <div class="panel panel-default marginLeftRight5 height525">
        <div class="panel-body">

            <div class="panel panel-default height500">

                 <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" HasExportButton="False"/>
            <br />
            <asp:Button ID="Button1" runat="server" Text="View Report" OnClick="Button1_Click1" />
            <br />
            <br />
            <br />
            <br />

            <asp:RadioButtonList ID="rbFormat" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Word" Value="Word" Selected="True" />
                <asp:ListItem Text="Excel" Value="Excel" />
                <asp:ListItem Text="PDF" Value="PDF" />
                <asp:ListItem Text="CSV" Value="CSV" />
            </asp:RadioButtonList>
            <br />

            <asp:Button ID="btnExport" Text="Export" runat="server" OnClick="ExportPDF" />

            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                <Report FileName="SahanTest\CR.rpt">
                </Report>
            </CR:CrystalReportSource>


                </div>
            </div>
         </div>
    
</asp:Content>
