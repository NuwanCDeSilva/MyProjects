<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TestCustCreate.aspx.cs" Inherits="FF.WebERPClient.Test.TestCustCreate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<%@ Register src="../UserControls/uc_CustomerCreation.ascx" tagname="uc_CustomerCreation" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div>
<div style="float: left; width: 50%;">
  <br />
    <uc1:uc_CustomerCreation ID="uc_CustomerCreation1" runat="server" />
    <br />
</div>
  
    <br />
    <br />
    <br />
</div>
</asp:Content>
