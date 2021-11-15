<%@ Page Title="" Language="C#" MasterPageFile="~/PDAWeb.Master" AutoEventWireup="true" CodeBehind="CreateJob.aspx.cs" Inherits="FastForward.SCMPDA.CreateJob" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script type="text/javascript">

        function scrollTop() {
            $('body').animate({ scrollTop: 0 }, 500);
        };

    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdatePanel ID="main" runat="server">
        <ContentTemplate>

            <div class="col_sm-12" runat="server" id="maindvcrjob">

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

                <div class="panel panel-default mainpnlmargin create-job-pnl">
                    <div class="panel-heading defaultpanelheader">
                        Create Job
                    </div>

                    <div class="panel-body pnl-bdy-cre-jb">
                        <div class="row">
                            <div class="col-sm-12">

                                <div class="col-sm-12">
                                    <div class="row">

                                        <div class="col-sm-6 labelText1 cre-jb-sel-doc">
                                            Select Doc Type
                                        </div>

                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddldoctype" AutoPostBack="true" TabIndex="3" runat="server" CssClass="form-control ControlText cre-jb-cls" OnSelectedIndexChanged="ddldoctype_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                        <div class="row">

                                            <asp:Panel ID="itmCdPnl" runat="server" DefaultButton="btToLocClk">

                                                <div class="col-sm-6  labelText1 to-lbl-cls">
                                                    To Location
                                                </div>
                                                <div class="col-sm-6 ">
                                                    <asp:TextBox ID="txtToLocation" runat="server" AutoPostBack="false" TabIndex="1" CssClass="form-control ControlText uppercase to-loc-txt"></asp:TextBox>
                                                    <asp:Button ID="btToLocClk" runat="server"  CssClass="btn-info hide form-control" Text="OK" OnClick="btToLocClk_Click" />
                                            </asp:Panel>
                                        </div>
                                </div>
                            </div>

                            <%--<div class="row">
                                    <div class="col-sm-12">
                                    </div>
                                </div>--%>

                            <div class="col-sm-12">
                                <div class="row btn-cre-jb-cont">

                                    <asp:Button ID="btncurrjob" runat="server" CssClass="btn-info form-control button-create-job" Text="Create Job Number" OnClick="btncurrjob_Click" />
                                    <asp:Button ID="btnback" runat="server" CssClass="btn-info form-control button-create-job-bk" Text="Back" OnClick="btnback_Click" />
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
