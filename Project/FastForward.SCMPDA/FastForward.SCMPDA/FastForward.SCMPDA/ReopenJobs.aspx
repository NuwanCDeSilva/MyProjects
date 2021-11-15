<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/PDAWeb.Master" CodeBehind="ReopenJobs.aspx.cs" Inherits="FastForward.SCMPDA.ReopenJobs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script type="text/javascript">
        function scrollTop() {
            $('body').animate({ scrollTop: 0 }, 500);
        };
        function ConfirmReopen() {
            var selectedvalueOrdPlace = confirm("Do you want to reopen ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=txtconfirmreopen.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconfirmreopen.ClientID %>').value = "No";
            }
        };
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdatePanel ID="main" runat="server">
        <ContentTemplate>
             <asp:HiddenField ID="txtconfirmreopen" runat="server" />
            <div class="col_sm-12">

                <div class="row">
                    <div class="col-sm-12 labelText1">

                        <div visible="false" class="alert alert-success" role="alert" runat="server" id="divok">

                            <div class="col-sm-12">
                                <asp:Label ID="lblok" runat="server"></asp:Label>
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
                       Reopen finished Job
                    </div>

                    <div class="panel-body curjb-pnl">
                        <div class="row">
                            <div class="col-sm-12">

                                <div class="col-sm-12">
                                    <div class="row">

                                        <div class="col-sm-6 labelText1">
                                            Job
                                        </div>

                                        <div class="col-sm-6">
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-12">
                                    <div class="row">

                                        <div class="col-sm-12 panelscoll">

                                            <asp:GridView ID="grdjobs" runat="server"
                                                AutoGenerateColumns="false" Font-Names="Arial"
                                                CssClass="table table-hover table-striped labelText1" 
                                                GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found..."  OnRowDataBound="grdjobs_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:LinkButton ID="lbtnreopen" runat="server" CausesValidation="false" Text="Reopen" OnClientClick="ConfirmReopen();" OnClick="lbtnreopen_Click">
                                                                    </asp:LinkButton>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:RadioButton ID="RadioButton1" runat="server"
                                                                onclick="RadioCheck(this);" Visible="false" />
                                                            <asp:HiddenField ID="HiddenField1" runat="server" 
                                                                Value='<%#Eval("tuh_doc_no")%>' />
                                                             <asp:HiddenField ID="HiddenField2" runat="server" 
                                                                Value='<%#Eval("tuh_usrseq_no")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Doc No" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldoc" runat="server" Text='<%# Bind("tuh_doc_no") %>' Width="75px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Seq No" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblseq" runat="server" Text='<%# Bind("tuh_usrseq_no") %>' Width="75px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField ItemStyle-Width="300px" DataField="tuh_doc_no"
                                                        HeaderText="Job No" />
                                                    <asp:BoundField ItemStyle-Width="150px" DataField="DATE"
                                                        HeaderText="Date " Visible="false" />
                                                    
                                                </Columns>
                                                <SelectedRowStyle BackColor="Silver" />
                                            </asp:GridView>

                                        </div>
                                    </div>
                                </div>
                                  <div class="col-sm-6">
                                      </div>
                                <div class="col-sm-6">
                                     <div class="row">
                                    <div class="group-button">
                                            <asp:Button ID="btnback" runat="server" CssClass="btn-info form-control" Text="Back" OnClick="btnback_Click" />
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