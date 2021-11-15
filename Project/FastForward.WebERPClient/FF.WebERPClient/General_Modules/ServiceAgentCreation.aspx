<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ServiceAgentCreation.aspx.cs" Inherits="FF.WebERPClient.General_Modules.ServiceAgentCreation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/uc_MsgInfo.ascx" TagName="uc_MsgInfo" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/uc_CommonSearch.ascx" TagName="uc_CommonSearch"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/uc_CustomerCreation.ascx" TagName="uc_CustomerCreation"
    TagPrefix="uc2" %>
<%@ Register Src="../UserControls/uc_CustCreationExternalDet.ascx" TagName="uc_CustCreationExternalDet"
    TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="float: left; width: 100%;">
        <div style="float: left; width: 99%; height: 22px; text-align: right; background-color: #1E4A9F">
            <div style="float: left;">
                <asp:Label ID="lblDispalyInfor" runat="server" Text="Back date allow for" CssClass="Label"
                    ForeColor="Yellow"></asp:Label>
            </div>
            <div style="float: right;">
                <asp:Button ID="btnSave" runat="server" Text="Save" Height="85%" Width="70px" CssClass="Button"
                    OnClick="btnSave_Click" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" Height="85%" Width="70px" CssClass="Button"
                    OnClick="btnClear_Click" />
                <asp:Button ID="btnClose" runat="server" Text="Close" Height="100%" Width="70px"
                    CssClass="Button" OnClick="btnClose_Click" />
            </div>
        </div>
    </div>
    <asp:UpdatePanel ID="pnl_head" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="float: left; width: 100%">
                <div style="float: left; width: 1%">
                    &nbsp;
                </div>
                <div style="float: left; width: 13%">
                    <asp:Label ID="Label24" runat="server" Text="Service Agent Code : "></asp:Label>
                </div>
                <div style="float: left; width: 1%">
                    &nbsp;
                </div>
                <div style="float: left; height: 8px; width: 23%;">
                    <asp:TextBox ID="txtCode" runat="server" CssClass="TextBox" Width="200px"></asp:TextBox>
                </div>
                <asp:ImageButton ID="imgbtnCode" runat="server" ImageUrl="~/Images/icon_search.png"
                    OnClick="imgbtnCode_Click" />
            </div>
            <div style="float: left; height: 15px; width: 100%;">
            </div>
            <div class="commonPageCss" style="float: left; width: 100%">
                <div class="CollapsiblePanelHeader" style="width: 100%">
                    Service Agent Details
                </div>
            </div>
            <div style="float: left; height: 15px; width: 100%;">
            </div>
            <div style="float: left; height: 8px; width: 100%;">
                <div style="float: left; width: 1%">
                    &nbsp;
                </div>
                <div style="float: left; height: 8px; width: 13%;">
                    <asp:Label ID="Label13" runat="server" Text="Name"></asp:Label>
                </div>
                <div style="float: left; width: 1%">
                    &nbsp;
                </div>
                <div style="float: left; height: 8px; width: 80%;">
                    <asp:TextBox ID="txtName" runat="server" CssClass="TextBox" Width="400px" MaxLength="100"></asp:TextBox>
                </div>
            </div>
            <div style="float: left; height: 15px; width: 100%;">
            </div>
            <div style="float: left; height: 8px; width: 100%;">
                <div style="float: left; width: 1%">
                    &nbsp;
                </div>
                <div style="float: left; height: 8px; width: 13%;">
                    <asp:Label ID="Label1" runat="server" Text="Address 1"></asp:Label>
                </div>
                <div style="float: left; width: 1%">
                    &nbsp;
                </div>
                <div style="float: left; height: 8px; width: 80%;">
                    <asp:TextBox ID="txtAddr1" runat="server" CssClass="TextBox" Width="600px" MaxLength="100"></asp:TextBox>
                </div>
            </div>
            <div style="float: left; height: 15px; width: 100%;">
            </div>
            <div style="float: left; height: 8px; width: 100%;">
                <div style="float: left; width: 1%">
                    &nbsp;
                </div>
                <div style="float: left; height: 8px; width: 13%;">
                    <asp:Label ID="Label2" runat="server" Text="Address 2"></asp:Label>
                </div>
                <div style="float: left; width: 1%">
                    &nbsp;
                </div>
                <div style="float: left; height: 8px; width: 80%;">
                    <asp:TextBox ID="txtAddr2" runat="server" CssClass="TextBox" Width="400px" MaxLength="100"></asp:TextBox>
                </div>
            </div>
            <div style="float: left; height: 15px; width: 100%;">
            </div>
            <div style="float: left; height: 8px; width: 100%;">
                <div style="float: left; width: 1%">
                    &nbsp;
                </div>
                <div style="float: left; height: 8px; width: 13%;">
                    <asp:Label ID="Label3" runat="server" Text="Address 3"></asp:Label>
                </div>
                <div style="float: left; width: 1%">
                    &nbsp;
                </div>
                <div style="float: left; height: 8px; width: 80%;">
                    <asp:TextBox ID="txtAddr3" runat="server" CssClass="TextBox" Width="300px" MaxLength="100"></asp:TextBox>
                </div>
            </div>
            <div style="float: left; height: 15px; width: 100%;">
            </div>
            <div style="float: left; height: 8px; width: 100%;">
                <div style="float: left; width: 1%">
                    &nbsp;
                </div>
                <div style="float: left; height: 8px; width: 13%;">
                    <asp:Label ID="Label4" runat="server" Text="Telephone"></asp:Label>
                </div>
                <div style="float: left; width: 1%">
                    &nbsp;
                </div>
                <div style="float: left; height: 8px; width: 80%;">
                    <asp:TextBox ID="txtTel" runat="server" CssClass="TextBox" Width="265px" MaxLength="100"></asp:TextBox>
                </div>
            </div>
            <div style="float: left; height: 15px; width: 100%;">
            </div>
            <div style="float: left; height: 8px; width: 100%;">
                <div style="float: left; width: 1%">
                    &nbsp;
                </div>
                <div style="float: left; height: 8px; width: 13%;">
                    <asp:Label ID="Label5" runat="server" Text="Fax"></asp:Label>
                </div>
                <div style="float: left; width: 1%">
                    &nbsp;
                </div>
                <div style="float: left; height: 8px; width: 80%;">
                    <asp:TextBox ID="txtFax" runat="server" CssClass="TextBox" Width="265px" MaxLength="100"></asp:TextBox>
                </div>
            </div>
            <div style="float: left; height: 15px; width: 100%;">
            </div>
            <div style="float: left; height: 8px; width: 100%;">
                <div style="float: left; width: 1%">
                    &nbsp;
                </div>
                <div style="float: left; height: 8px; width: 13%;">
                    <asp:Label ID="Label6" runat="server" Text="Town"></asp:Label>
                </div>
                <div style="float: left; width: 1%">
                    &nbsp;
                </div>
                <div style="float: left; height: 8px; width: 80%;">
                    <asp:TextBox ID="txtTown" runat="server" CssClass="TextBox" Width="265px" MaxLength="100"></asp:TextBox>
                </div>
            </div>
            <div style="float: left; height: 15px; width: 100%;">
            </div>
            <div style="float: left; width: 100%">
                <div style="float: left; width: 1%">
                    &nbsp;
                </div>
                <div style="float: left; width: 13%">
                    <asp:Label ID="Label12" runat="server" Text="Service Category"></asp:Label>
                </div>
                <div style="float: left; width: 1%">
                    &nbsp;
                </div>
                <div style="float: left; height: 8px; width: 23%;">
                    <asp:TextBox ID="txtCat" runat="server" CssClass="TextBox" Width="200px"></asp:TextBox>
                </div>
                <asp:ImageButton ID="imgBtnSerCat" runat="server" ImageUrl="~/Images/icon_search.png"
                    OnClick="imgBtnSerCat_Click" />
            </div>
            <div style="float: left; height: 15px; width: 100%;">
            </div>
            <div style="float: left; height: 8px; width: 100%;">
                <div style="float: left; width: 1%">
                    &nbsp;
                </div>
                <div style="float: left; height: 8px; width: 13%;">
                    <asp:Label ID="Label7" runat="server" Text="Contact Person"></asp:Label>
                </div>
                <div style="float: left; width: 1%">
                    &nbsp;
                </div>
                <div style="float: left; height: 8px; width: 80%;">
                    <asp:TextBox ID="txtContact" runat="server" CssClass="TextBox" Width="400px" MaxLength="100"></asp:TextBox>
                </div>
            </div>
            <div style="float: left; height: 15px; width: 100%;">
            </div>
            <div style="float: left; height: 8px; width: 100%;">
                <div style="float: left; width: 1%">
                    &nbsp;
                </div>
                <div style="float: left; height: 8px; width: 13%;">
                    <asp:Label ID="Label8" runat="server" Text="Cordinator"></asp:Label>
                </div>
                <div style="float: left; width: 1%">
                    &nbsp;
                </div>
                <div style="float: left; height: 8px; width: 80%;">
                    <asp:TextBox ID="txtCordinator" runat="server" CssClass="TextBox" Width="400px" MaxLength="100"></asp:TextBox>
                </div>
            </div>
            <div style="float: left; height: 15px; width: 100%;">
            </div>
            <div style="float: left; height: 8px; width: 100%;">
                <div style="float: left; width: 1%">
                    &nbsp;
                </div>
                <div style="float: left; height: 8px; width: 13%;">
                    <asp:Label ID="Label9" runat="server" Text="Status"></asp:Label>
                </div>
                <div style="float: left; width: 1%">
                    &nbsp;
                </div>
                <div style="float: left; height: 8px; width: 80%;">
                    <asp:DropDownList ID="ddlStatus" runat="server" Width="166px" CssClass="ComboBox">
                    </asp:DropDownList>
                </div>
            </div>
            <div style="float: left; height: 15px; width: 100%;">
            </div>
            <div style="float: left; width: 100%">
                <div style="float: left; width: 1%">
                    &nbsp;
                </div>
                <div style="float: left; width: 13%">
                    <asp:Label ID="Label11" runat="server" Text="Mapped Location"></asp:Label>
                </div>
                <div style="float: left; width: 1%">
                    &nbsp;
                </div>
                <div style="float: left; height: 8px; width: 23%;">
                    <asp:TextBox ID="txtMapedLoc" runat="server" CssClass="TextBox" Width="200px"></asp:TextBox>
                </div>
                <asp:ImageButton ID="imgBtnMapLoc" runat="server" ImageUrl="~/Images/icon_search.png"
                    OnClick="imgBtnMapLoc_Click" />
            </div>
            <div style="display: none;">
                <asp:Button ID="btnCode" runat="server" OnClick="GetServiceAgentData" />
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
