<%@ Page Title="" Language="C#" MasterPageFile="~/PDAWeb.Master" AutoEventWireup="true" CodeBehind="SampleDesign.aspx.cs" Inherits="FastForward.SCMPDA.SampleDesign" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdatePanel ID="main" runat="server">
        <ContentTemplate>

            <div class="col_sm-12">

                <div class="row">
                    <div class="col-sm-12 labelText1">

                        <div visible="false" class="alert alert-success" role="alert" runat="server" id="divok">

                            <div class="col-sm-12">
                                <asp:Label ID="lblok" Text="Error occured please close" runat="server"></asp:Label>
                                <asp:LinkButton ID="lbtnok" runat="server" CausesValidation="false" CssClass="floatright">
                                        <span class="glyphicon glyphicon-remove " aria-hidden="true" ></span>
                                </asp:LinkButton>
                            </div>

                        </div>

                        <div visible="false" class="alert alert-danger" role="alert" runat="server" id="divalert">

                            <div class="col-sm-12">
                                <asp:Label ID="lblalert" runat="server"></asp:Label>
                                <asp:LinkButton ID="lbtnalert" runat="server" CausesValidation="false" CssClass="floatright">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                    </asp:LinkButton>
                            </div>

                        </div>

                        <div visible="false" class="alert alert-info" role="alert" runat="server" id="Divinfo">
                            <div class="col-sm-12">
                                <asp:Label ID="lblinfo" runat="server"></asp:Label>
                                <asp:LinkButton ID="lbtninfo" runat="server" CausesValidation="false" CssClass="floatright">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                    </asp:LinkButton>
                            </div>
                        </div>

                    </div>


                </div>

                <div class="row">
                    <div class="col-sm-12">
                    </div>
                </div>
                 
                <div class="panel panel-default mainpnlmargin">
                    <div class="panel-heading defaultpanelheader">
                        Log In
                    </div>

                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12">

                                <div class="col-sm-12">
                                    <div class="row">

                                        <div class="col-sm-6 labelText1">
                                            Username
                                        </div>

                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtuser" runat="server" CssClass="form-control ControlText"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-12">
                                    </div>
                                </div>

                                <div class="col-sm-12">
                                    <div class="row">

                                        <div class="col-sm-6 labelText1">
                                            Password
                                        </div>

                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtpw" runat="server" CssClass="form-control ControlText"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-12">
                                    </div>
                                </div>

                                <div class="col-sm-12">
                                    <div class="row">

                                        <div class="col-sm-6 labelText1">
                                            Company
                                        </div>

                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlcompany" AutoPostBack="true" runat="server" CssClass="form-control ControlText"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-12">
                                    </div>
                                </div>


                                <div class="col-sm-12">
                                    <div class="row">

                                        <div class="col-sm-6 labelText1">
                                        </div>

                                        <div class="col-sm-6">
                                            <asp:Button ID="btnlogin" runat="server" CssClass="btn-primary form-control" Text="Login" />
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
