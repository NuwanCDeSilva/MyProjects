<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="SalesReportViewer.aspx.cs" Inherits="FastForward.SCMWeb.View.Reports.Sales.SalesReportViewer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <script src="../../../Js/jquery.min.js"></script>
    <script src="../../../Js/print_report.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
     <input type="submit" value="Print" class="btn btn-default btn-default-style btn-print-salerepot" />
    <div class="panel panel-default marginLeftRight5 height525">
        <div class="panel-body">

            <div class="panel panel-default height500">

                 <CR:CrystalReportViewer ID="CVSale" runat="server" AutoDataBind="true" HasExportButton="False" HasCrystalLogo="False" HasRefreshButton="False" HasDrillUpButton="False"/>
            <br />
            <%--<asp:Button ID="Button1" runat="server" Text="View Report" />
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

            <asp:Button ID="btnExport" Text="Export" runat="server"  />

            <CR:CrystalReportSource ID="CVSourceImport" runat="server">
                <Report FileName="SahanTest\CR.rpt">
                </Report>
            </CR:CrystalReportSource>--%>


                </div>
            </div>
         </div>
</asp:Content>
