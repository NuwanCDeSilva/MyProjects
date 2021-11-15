<%@ Page Title="" Language="C#" MasterPageFile="~/Basic.Master" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="FF.WebERPClient.ErrorPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            font-size: large;
            color: #FF0000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<p> 
<span style="color: red;">There is an error on the server. Please log-out and log-in to the system and try again!</span> 
</p>
</asp:Content>
