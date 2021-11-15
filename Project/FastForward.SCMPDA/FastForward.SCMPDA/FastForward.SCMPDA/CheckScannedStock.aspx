<%@ Page Title="" Language="C#" MasterPageFile="~/PDAWeb.Master" AutoEventWireup="true" CodeBehind="CheckScannedStock.aspx.cs" Inherits="FastForward.SCMPDA.CheckScannedStock" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script type="text/javascript">

       <%-- function ConfirmDelete() {
            var selectedvalueOrdPlace = confirm("Do you want to delete ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=txtconfirmdelete.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconfirmdelete.ClientID %>').value = "No";
            }
        };--%>

        function scrollTop() {
            $('body').animate({ scrollTop: 0 }, 500);
        };

    </script>
    <style>
        .DatePanel {
            position: absolute;
            background-color: #FFFFFF;
            border: 1px solid #646464;
            color: #000000;
            z-index: 1;
            font-family: tahoma,verdana,helvetica;
            font-size: 11px;
            padding: 4px;
            text-align: center;
            cursor: default;
            line-height: 20px;
        }

        .divWaiting {
            position: absolute;
            background-color: #FAFAFA;
            z-index: 2147483647 !important;
            opacity: 0.8;
            overflow: hidden;
            text-align: center;
            top: 0;
            left: 0;
            height: 100%;
            width: 100%;
            padding-top: 20%;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="main">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="~/Css/Images/hidden_progress_bar.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="main" runat="server">
        <ContentTemplate>

            <%--<asp:HiddenField ID="txtconfirmdelete" runat="server" />--%>

            <div class="col_sm-12" runat="server" id="dvscanjobs">

                <div class="row">
                    <div class="col-sm-12 labelText1">

                        <div visible="false" class="alert alert-success" role="alert" runat="server" id="divok">

                            <div class="col-sm-12">
                                <asp:Label ID="lblokjob" runat="server"></asp:Label>
                                <asp:LinkButton ID="lbtnok" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnok_Click">
                                        <span class="glyphicon glyphicon-remove " aria-hidden="true" ></span>
                                </asp:LinkButton>
                            </div>

                        </div>

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
                        Scan Details
                    </div>

                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12">

                                <div class="col-sm-12">
                                    <div class="row">

                                        <div class="col-sm-12 panelscoll itm-pnl ignore-width">

                                            <asp:GridView ID="grdscanneditems" runat="server"
                                                AutoGenerateColumns="false" Font-Names="Arial"
                                                CssClass="table table-hover table-striped labelText1"
                                                GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">

                                                <Columns>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:LinkButton ID="lbtndelete" runat="server" CausesValidation="false" Text="Delete" OnClick="lbtndelete_Click"> <%--OnClientClick="ConfirmDelete();"--%>
                                                        <%--<span class="glyphicon glyphicon-trash" aria-hidden="true"></span>--%>
                                                                    </asp:LinkButton>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--<asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:LinkButton Visible='<%# Eval("tus_ser_1").ToString().Equals("N/A") || Eval("tus_ser_1").ToString().Equals("")%>' ID="lbtndeletenonser" runat="server" CausesValidation="false" OnClick="lbtndeletenonser_Click">
                                                        <span class="glyphicon glyphicon-minus" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                    <asp:TemplateField HeaderText="Item">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitm" runat="server" Text='<%# Bind("tus_itm_cd") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Serial 1">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblse1" runat="server" Text='<%# Bind("tus_ser_1") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltotqty" runat="server" Text='<%# Bind("tus_qty") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblstatus" runat="server" Text='<%# Bind("Status") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Seq No" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblseq" runat="server" Text='<%# Bind("tus_usrseq_no") %>' Width="75px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Serial 2">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblbse2" runat="server" Text='<%# Bind("tus_ser_2") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCode" runat="server" Text='<%# Bind("tus_itm_stus") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Bin No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblbinno" runat="server" Text='<%# Bind("tus_bin") %>' Width="75px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                    </div>
                                </div>

                                <%--<div class="row">
                                    <div class="col-sm-12">
                                    </div>
                                </div>--%>

                                <div class="col-sm-6">
                                    <div class="row display-bx">

                                        <div class="labelText1 label label-default sp-margin">
                                            Scan Qty : 
                                            <asp:Label ID="lbltotqtylbl" runat="server" CssClass="labelText1"></asp:Label>
                                        </div>
                                        <div class="labelText1 label label-default">
                                            Doc Qty : 
                                            <asp:Label ID="lbldocqty" runat="server" CssClass="labelText1"></asp:Label>
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
