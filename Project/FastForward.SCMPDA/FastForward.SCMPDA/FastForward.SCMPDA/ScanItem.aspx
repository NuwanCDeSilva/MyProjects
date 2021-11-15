<%@ Page Title="" Language="C#" MasterPageFile="~/PDAWeb.Master" AutoEventWireup="true" CodeBehind="ScanItem.aspx.cs" Inherits="FastForward.SCMPDA.ScanItem" %>

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
                                <asp:LinkButton ID="lbtnok" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnok_Click">
                                        <span class="glyphicon glyphicon-remove " aria-hidden="true" ></span>
                                </asp:LinkButton>
                            </div>

                        </div>

                        <div visible="false" class="alert alert-danger" role="alert" runat="server" id="divalert">

                            <div class="col-sm-12">
                                <asp:Label ID="lblalert" runat="server"></asp:Label>
                                <asp:LinkButton ID="lbtnalert" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnalert_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </div>

                        </div>

                        <div visible="false" class="alert alert-info" role="alert" runat="server" id="Divinfo">
                            <div class="col-sm-12">
                                <asp:Label ID="lblinfo" runat="server"></asp:Label>
                                <asp:LinkButton ID="lbtninfo" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtninfo_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </div>
                        </div>

                    </div>


                </div>

                <div class="panel panel-default mainpnlmargin">
                    <div class="panel-heading defaultpanelheader">
                        Scan Items
                    </div>

                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12">

                                <div class="col-sm-12">
                                    <div class="row">

                                        <div class="col-sm-6">
                                            <asp:Button ID="btncreatejob" runat="server" CssClass="btn-info form-control button-scan-item" Text="Create Job" OnClick="btncreatejob_Click" />
                                        </div>
                                    </div>
                                </div>

                                <%-- <div class="row">
                                    <div class="col-sm-12">
                                    </div>
                                </div>--%>

                                <div class="col-sm-12">
                                    <div class="row">

                                        <div class="col-sm-6">
                                            <asp:Button ID="btncurrjob" runat="server" CssClass="btn-info form-control button-scan-item" Text="Current Job" OnClick="btncurrjob_Click" />
                                        </div>
                                    </div>
                                </div>
                                   <div class="col-sm-12"  id="trakserbtn" visible="false">
                                    <div class="row">
                                        <div class="col-sm-6">
                                                <asp:Button ID="btnTracker" runat="server" TabIndex="18" CssClass="btn-info form-control button-scan-item" Text="Tracker" OnClick="btnTracker_Click" />
                                        </div>
                                    </div>
                                </div>
                                 <%--<div class="row">
                                    <div class="col-sm-12">
                                    </div>
                                </div>--%>

                                <div class="col-sm-12">
                                    <div class="row">

                                        <div class="col-sm-6">
                                            <asp:Button ID="btnback" runat="server" CssClass="btn-info form-control button-scan-item" Text="Back" OnClick="btnback_Click" />
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
