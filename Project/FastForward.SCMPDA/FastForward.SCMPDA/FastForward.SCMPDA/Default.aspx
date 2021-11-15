<%@ Page Title="" Language="C#" MasterPageFile="~/PDAWeb.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FastForward.SCMPDA.Default" %>
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

            <div class="col_sm-12" runat="server" id="maindvdef">

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
                    <div class="panel-heading defaultpanelheader def-pnl-hed">
                        Barcode Reader
                    </div>

                    <div class="panel-body default-pnl-bdy">
                        <div class="row">
                            <div class="col-sm-12">

                                <div class="col-sm-12">
                                    <div class="row">

                                        <div class="col-sm-12 labelText1 def-lbl-txt">
                                            Category
                                        </div>
                                        <div class="col-sm-12">
                                            <asp:DropDownList ID="ddltypes" AutoPostBack="true" TabIndex="1" runat="server" CssClass="form-control ControlText def-lbl-drop" OnSelectedIndexChanged="ddltypes_SelectedIndexChanged">
                                                    <asp:ListItem Text="-Select-" Value="-1"></asp:ListItem>    
                                                    <asp:ListItem Text="Stock In" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Stock Out" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Stock Verification" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <%--<div class="row">
                                    <div class="col-sm-12">
                                    </div>
                                </div>--%>

                                <%--<div class="col-sm-12">
                                    <div class="row">

                                        <div class="col-sm-6">--%>
                                           <%-- <asp:RadioButtonList ID="rbtypes" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbtypes_SelectedIndexChanged">
                                                <asp:ListItem Text="Stock In" Value="1"  />
                                                <asp:ListItem Text="Stock Out" Value="0" />
                                            </asp:RadioButtonList>--%>
                                            
                                        <%--</div>
                                    </div>
                                </div>--%>

                                <div class="col-sm-12 def-buttn-blk def-btn-h">
                                    <%--<div class="row">--%>

                                        <%--<div class="col-sm-6">--%>
                                            <asp:Button ID="btnscan" runat="server" CssClass="btn-info form-control" Text="Scan Item" OnClick="btnscan_Click" />
                                        <%--</div>--%>
                                    <%--</div>--%>
                                <%--</div>--%>

                                  <%--<div class="row">
                                    <div class="col-sm-12">
                                    </div>
                                </div>--%>

                                <%--<div class="col-sm-12">--%>
                                    <%--<div class="row">--%>

                                        <%--<div class="col-sm-6">--%>
                                            <asp:Button ID="btnsetings" runat="server" CssClass="btn-info form-control" Text="Settings" OnClick="btnsetings_Click" />
                                        <%--</div>--%>
                                    <%--</div>--%>
                                </div>

                            </div>
                        </div>
                    </div>

                </div>

            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
