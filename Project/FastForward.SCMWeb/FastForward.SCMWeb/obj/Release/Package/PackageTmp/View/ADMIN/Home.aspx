<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="FastForward.SCMWeb.View.ADMIN.Home" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="panel panel-default marginLeftRight5">
        <div class="panel-body">
            <div class="row">
                <div class="col-sm-1 height30">
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="col-sm-1">
                        <asp:Image ID="Image1" ImageUrl="~/images/banners/FFNew-125x41.png" runat="server" />
                    </div>
                    <div class="col-sm-4">
                    </div>
                    <div class="col-sm-4">
                    </div>

                </div>
            </div>
             <div class="row">
                <div class ="col-lg-1 height250">
                    </div>
                 </div>

            <%--<asp:Button ID="Button1" runat="server" Text="Fill Form in Popup" />
 
<cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="gvgrnlist" TargetControlID="Button1"
    CancelControlID="Button2" BackgroundCssClass="Background">
</cc1:ModalPopupExtender>
<asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" style = "display:none">
    <iframe style=" width: 350px; height: 300px;" id="irm1" src="WebForm2.aspx" runat="server"></iframe>
   <br/>
    <asp:Button ID="Button2" runat="server" Text="Close" />
</asp:Panel>--%>
          

            <div class="row">
                <div class="col-sm-12 ">
                    <div class="col-sm-4">
                    </div>
                    <div class="col-sm-4">
                    </div>
                    <div class="col-sm-4">
                      <%--  <asp:Image ID="Image2" ImageUrl="~/images/banners/Abans.png" runat="server" />--%>
                    </div>

                </div>
            </div>
        </div>
    </div>

     
    <asp:UpdatePanel runat="server" ID="upSearch">
        <ContentTemplate>
            <asp:Button ID="btn1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupSearch" runat="server" Enabled="True" TargetControlID="btn1"
                PopupControlID="pnlSearch" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlSearch" >
        <div runat="server" id="test" class="panel panel-primary" style="padding: 5px;">
            <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default" style="height: 350px; width: 800px;">
                <div class="panel-heading" style="height: 30px;">
                    <div class="col-sm-3">
                     </div>
                    <div class="col-sm-8">
                       <span style="text-align:end;font-size:medium;font-family:Arial"><b>This is your last log-in and log-off history</b></span>
                    </div>
                    <div class="col-sm-1">
               
                        <asp:LinkButton ID="btnClose" runat="server" OnClick="btnClose_Click">
                             <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                           
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="row height16">
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy8" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                     <div class="panelscoll" style="height: 250px">
                                    <asp:GridView ID="dgvResult" CausesValidation="false" runat="server" AllowPaging="false" 
                                        GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" ShowHeaderWhenEmpty="true"
                                        EmptyDataText="No data found...">
                                        <Columns>
                                           <%-- <asp:TemplateField HeaderText="Log On">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblogon" runat="server" Text='<%# Bind("Log_On") %>' Width="5px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Log Off">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblogoff" runat="server" Text='<%# Bind("Log_Off") %>' Width="5px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Company">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lbcom" runat="server" Text='<%# Bind("Login_Company") %>' Width="5px"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IP">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lbip" runat="server" Text='<%# Bind("Login_IP") %>' Width="5px"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PC">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lbpc" runat="server" Text='<%# Bind("Login_PC") %>' Width="5px"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Domain">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lbldom" runat="server" Text='<%# Bind("Login_Domain") %>' Width="5px"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                    </asp:GridView>
                                         </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
