<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="FastForward.SCMWeb.Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            color: rgb(255, 0, 0);
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <div visible="true" class="alert alert-danger" role="alert" runat="server" id="Div1">
            <div class="col-sm-11  buttonrow ">
                <strong>Alert!</strong>
                <asp:Label ID="Label1" runat="server"></asp:Label>
            </div>
        </div>

    <div class="panel panel-default marginLeftRight5">
        <div class="panel-body">
            <div class="panel panel-default">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <p>
                            <strong class="auto-style1" style="border-style: none; border-color: inherit; border-width: 0px; margin: 0px; padding: 0px; font-size: 15px; font-weight: bold; font-family: 'Helvetica Neue', Arial, sans-serif; font-style: normal; font-variant: normal; letter-spacing: normal; line-height: 19.5px; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(252, 252, 252);">Oops! Something went wrong.</strong>
                        </p>
                        <p>
                            <asp:Label ID="lblerror" runat="server" Text="Label"></asp:Label>
                        </p>
                        <p>
                        </p>
                        <p>
                        </p>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
