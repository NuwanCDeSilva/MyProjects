<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MonthYearPicker.ascx.cs" Inherits="FastForward.SCMWeb.UserControls.MonthYearPicker" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
  <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
      <script  src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>
<link href="<%=Request.ApplicationPath%>Css/bootstrap.css" rel="stylesheet" />

<link href="<%=Request.ApplicationPath%>Css/style.css" rel="stylesheet" />

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

        <div id="main">

            <div>
                <div>
                    <asp:TextBox ID="txtValue" runat="server" ReadOnly="true" Width="184px" class="form-control"></asp:TextBox>
                </div>

                <div id="caldv" class="yearmonthselector">
                    <asp:LinkButton ID="lbtnSelect" CausesValidation="false" OnClick="btnSelect_Click" runat="server">
                                                        <span class="glyphicon glyphicon-triangle-left" aria-hidden="true" style="font-size:20px"></span>
                    </asp:LinkButton>
                </div>
            </div>

        </div>

        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                Loading...
            </ProgressTemplate>
        </asp:UpdateProgress>

        <asp:Panel ID="pnlDate" runat="server" Visible="false" CssClass="DatePanel" Width="190px">
            <div id="drop">
                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control" Width="100px">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" Width="100px">
                </asp:DropDownList>
            </div>
            <div id="but" style="margin-left: -140px">

                <asp:LinkButton ID="lbtnSet" CausesValidation="false" OnClick="btnSet_Click" runat="server" OnClientClick="Confirm()">
                                                        <span class="glyphicon glyphicon-check" aria-hidden="true" style="font-size:20px"></span>Set
                </asp:LinkButton>

            </div>
        </asp:Panel>
           
    </ContentTemplate>

</asp:UpdatePanel>
