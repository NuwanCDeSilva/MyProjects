<%@ Page Title="" Language="C#" MasterPageFile="~/PDAWeb.Master" AutoEventWireup="true" CodeBehind="ChangeLocation.aspx.cs" Inherits="FastForward.SCMPDA.ChangeLocation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script type="text/javascript">
        function RadioCheck(rb) {
            var gv = document.getElementById("<%=grdlocation.ClientID%>");
         var rbs = gv.getElementsByTagName("input");

         var row = rb.parentNode.parentNode;
         for (var i = 0; i < rbs.length; i++) {
             if (rbs[i].type == "radio") {
                 if (rbs[i].checked && rbs[i] != rb) {
                     rbs[i].checked = false;
                     break;
                 }
             }
         }
        }

        function HideLabel() {
            var seconds = 3;
            setTimeout(function () {
                document.getElementById("<%=divok.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };

  

   function scrollTop() {
       $('body').animate({ scrollTop: 0 }, 500);
   };

   

    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdatePanel ID="main" runat="server">
        <ContentTemplate>

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
                        Change Location
                    </div>

                    <div class="panel-body location-body">
                        <div class="row">
                            <div class="col-sm-12">

                                <div class="col-sm-12 serch-box">
                                    <div class="row">

                                        <div class="col-sm-3 labelText1 lbltxt-set">
                                            Location
                                        </div>
                                        <%--<div class="row">
                                            <div class="col-sm-12">
                                            </div>
                                        </div>--%>

                                        <div class="col-sm-5 labelText1 inside-val">
                                            <asp:TextBox ID="txtfindloc" runat="server" CssClass="form-control txt-wdth-set ControlText col-sm-8" AutoPostBack="true" OnTextChanged="txtfindloc_TextChanged"></asp:TextBox>
                                        
                                            
                                  
                                        </div>
                                        <div class="col-sm-5 labelText1">
                                            <asp:Button ID="btnfindloc" runat="server" CssClass="btn-warning btn-info form-control col-sm-4" Text="Find" OnClick="btnfindloc_Click" />
                                            </div>
                                        <%--<div class="row">
                                            <div class="col-sm-12">
                                            </div>
                                        </div>
                                         <div class="row">
                                            <div class="col-sm-12">
                                            </div>
                                        </div>
                                         <div class="row">
                                            <div class="col-sm-12">
                                            </div>
                                        </div>
                                         <div class="row">
                                            <div class="col-sm-12">
                                            </div>
                                        </div>
                                         <div class="row">
                                            <div class="col-sm-12">
                                            </div>
                                        </div>
                                         <div class="row">
                                            <div class="col-sm-12">
                                            </div>
                                        </div>
                                         <div class="row">
                                            <div class="col-sm-12">
                                            </div>
                                        </div>
                                         <div class="row">
                                            <div class="col-sm-12">
                                            </div>
                                        </div>
                                         <div class="row">
                                            <div class="col-sm-12">
                                            </div>
                                        </div>
                                         <div class="row">
                                            <div class="col-sm-12">
                                            </div>
                                        </div>
                                         <div class="row">
                                            <div class="col-sm-12">
                                            </div>
                                        </div>
                                         <div class="row">
                                            <div class="col-sm-12">
                                            </div>
                                        </div>
                                         <div class="row">
                                            <div class="col-sm-12">
                                            </div>
                                        </div>--%>
                                        
                                    </div>
                                </div>

                                <%--<div class="row">
                                    <div class="col-sm-12">
                                    </div>
                                </div>--%>

                                <div class="col-sm-12">
                                    <div class="row">

                                        <div class="col-sm-12 panelscoll itm-pnl">

                                            <asp:GridView ID="grdlocation" runat="server"
                                                AutoGenerateColumns="false" Font-Names="Arial"
                                                CssClass="table table-hover table-striped labelText1" 
                                                GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:RadioButton ID="RadioButton1" runat="server"
                                                                onclick="RadioCheck(this);" />
                                                            <asp:HiddenField ID="HiddenField1" runat="server" 
                                                                Value='<%#Eval("Code")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField ItemStyle-Width="150px" DataField="Code"
                                                        HeaderText="Code" />
<%--                                                    <asp:BoundField ItemStyle-Width="1px" DataField="Description" Visible="false"
                                                        HeaderText="Description" />--%>
                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                    </div>
                                </div>

                                  <%--<div class="row">
                                    <div class="col-sm-12">
                                    </div>
                                </div>--%>

                                <div class="col-sm-12">
                                    <div class="row">
                                        <div class="chg-loc-btn">
                                            <asp:Button ID="btnchangeloc" runat="server" CssClass="btn-info form-control chg-loc-pge-btn" Text="Change Location" OnClick="btnchangeloc_Click"  />
                                            <asp:Button ID="btnback" runat="server" CssClass="btn-info form-control chg-loc-pge-btn" Text="Back" OnClick="btnback_Click" />
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
