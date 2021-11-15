<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/Shared/ReportSite.Master" CodeBehind="ReceiptPrintViwer.aspx.cs" Inherits="FastForward.WebAbansTours.Print_Module.Print_Viwer.ReceiptPrintViwer" %>

 <%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <CR:CrystalReportViewer ID="ReceiptReport"   runat="server"  HasCrystalLogo="False"
    AutoDataBind="True"  Height="50px"  EnableParameterPrompt="false" EnableDatabaseLogonPrompt="false" ToolPanelWidth="200px" 
    Width="350px" ToolPanelView="None" />
</asp:Content>
