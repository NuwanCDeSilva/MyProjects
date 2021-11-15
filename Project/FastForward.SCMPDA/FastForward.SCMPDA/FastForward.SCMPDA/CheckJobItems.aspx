<%@ Page Language="C#" MasterPageFile="~/PDAWeb.Master" AutoEventWireup="true" CodeBehind="CheckJobItems.aspx.cs" Inherits="FastForward.SCMPDA.CheckJobItems" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdatePanel ID="main" runat="server">
        <ContentTemplate>
            <div class="col_sm-12" runat="server" id="dvscanjobs">

                <div class="row">
                    <div class="col-sm-12 labelText1">

       
                        <div visible="false" class="alert alert-danger" role="alert" runat="server" id="divalert">

                            <div class="col-sm-12">
                                <asp:Label ID="lblalert" runat="server"></asp:Label>
                                <asp:LinkButton ID="lbtndicalertclose" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtndicalertclose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </div>

                        </div>

                        <div visible="false" class="alert alert-info" role="alert" runat="server" id="Divinfo">
                            <div class="col-sm-12">
                                <asp:Label ID="lblinfo" runat="server"></asp:Label>
                                <asp:LinkButton ID="lbtndivinfoclose" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtndivinfoclose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </div>
                        </div>

                    </div>


                </div>

                <div class="panel panel-default mainpnlmargin">
                    <div class="panel-heading defaultpanelheader">
                        Item Details
                    </div>

                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12">

                                <div class="col-sm-12">
                                    <div class="row">

                                        <div class="col-sm-12 panelscoll">

                                            <asp:GridView ID="grdjobitems" runat="server"
                                                AutoGenerateColumns="false" Font-Names="Arial"
                                                CssClass="table table-hover table-striped labelText1"
                                                GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No items found...">

                                                <Columns>

                                                    <asp:TemplateField HeaderText="Item Cd">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitm" runat="server" Text='<%# Bind("tui_req_itm_cd") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Req Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblstatus" runat="server" Text='<%# Bind("tui_req_itm_qty") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Scan Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblseq" runat="server" Text='<%# Bind("tui_pic_itm_qty") %>' Width="75px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="row">
                                        <div class="col-sm-12 label">
                                            <asp:Button ID="btnback" runat="server" CssClass="btn-info form-control button-chkscn-stc" Text="Back" OnClick="btnback_Click" />
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