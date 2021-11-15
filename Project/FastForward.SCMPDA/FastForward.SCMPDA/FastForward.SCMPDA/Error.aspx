<%@ Page Title="" Language="C#" MasterPageFile="~/PDAWeb.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="FastForward.SCMPDA.Error" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdatePanel ID="main" runat="server">
        <ContentTemplate>

            <div class="col_sm-12">

                <div class="panel panel panel-danger mainpnlmargin">
                    <div class="panel-heading defaultpanelheader">
                        Loading Bay 
                    </div>

                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12">

                                <div class="col-sm-12">
                                    <div class="row">

                                        <div class="col-sm-6 labelText1">
                                            Information
                                        </div>

                                        <div class="col-sm-6">
                                            <div class="alert alert-danger" role="alert">
                                                <asp:Label ID="lblalert" runat="server" CssClass="labelText1"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <asp:Button ID="btnchgloc" runat="server" CssClass="btn-info form-control col-sm-4" Text="Change Location" OnClick="btnchgloc_Click" />
                                            </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>

                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
