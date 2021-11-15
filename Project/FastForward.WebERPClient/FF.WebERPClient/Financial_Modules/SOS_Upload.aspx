<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="SOS_Upload.aspx.cs" Inherits="FF.WebERPClient.Financial_Modules.SOS_Upload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="float: left; width: 100%; color: Black;">
        <div style="float: left; width: 100%;">
            <div style="height: 22px; text-align: right;" class="PanelHeader">
                <asp:Button ID="btnProcess" runat="server" Text="Process" Height="85%" Width="70px"
                    CssClass="Button" OnClick="btnProcess_Click" />
                <asp:Button ID="btnClose" runat="server" Text="Close" Height="85%" Width="70px" CssClass="Button"
                    OnClick="btnClose_Click" />
            </div>
        </div>
    </div>
    <asp:UpdatePanel ID="pnlButtons" runat="server">
        <ContentTemplate>
            <div style="float: left; width: 100%; color: Black;">
                <asp:Panel ID="Panel1" runat="server" GroupingText="." Font-Size="11px" Width="893px"
                    Font-Names="Tahoma" Height="400px">
                    <div style="float: left; width: 100%;">
                        <div style="float: left; width: 10%;">
                            <asp:RadioButton ID="optSOS" runat="server" Text="SOS" GroupName="opt" OnCheckedChanged="optSOS_OnCheckedChanged"
                                AutoPostBack="true" Checked="true" /></div>
                        <div style="float: left; width: 15%;">
                            <asp:RadioButton ID="optDInv" runat="server" Text="Dealer Invoices" GroupName="opt" OnCheckedChanged="optAcc_OnCheckedChanged"
                                AutoPostBack="true" /></div>
                                <div style="float: left; width: 15%;">
                            <asp:RadioButton ID="optDRec" runat="server" Text="Dealer Receipts" GroupName="opt" 
                                AutoPostBack="true" /></div>
                    </div>
                    <div style="float: left; height: 6px; width: 100%;">
                    </div>
                    <div style="float: left; width: 100%;">
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 8%">
                            Profit Center
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 22%">
                            <asp:TextBox ID="txtPC" runat="server" CssClass="TextBox" Width="90px" ClientIDMode="Static"></asp:TextBox>
                            <asp:ImageButton ID="imgbtnPC" runat="server" ImageUrl="~/Images/Search.gif" OnClick="imgbtnPC_Click" />
                        </div>
                    </div>
                    <div style="float: left; height: 6px; width: 100%;">
                    </div>
                    <div style="float: left; width: 100%;">
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 8%">
                            Year
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 13%">
                            <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                CssClass="ComboBox" Width="105px">
                            </asp:DropDownList>
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 5%">
                            Month
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 13%">
                            <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                CssClass="ComboBox" Width="140" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div style="float: left; height: 6px; width: 100%;">
                    </div>
                    <div style="float: left; width: 100%;">
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 8%">
                            From Date
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 13%">
                            <asp:TextBox ID="txtFrom" runat="server" CssClass="TextBox" Width="90px" AutoPostBack="false"
                                ></asp:TextBox>
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 5%; height: 18px;">
                            To Date
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 15%">
                            <asp:TextBox ID="txtTo" runat="server" CssClass="TextBox" Width="90px" AutoPostBack="false"
                               ></asp:TextBox>
                        </div>
                    </div>
                    <div style="float: left; height: 6px; width: 100%;">
                    </div>
                    <div style="float: left; width: 100%;">
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 8%">
                            File Name
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 30%">
                            <asp:TextBox ID="txtFile" runat="server" CssClass="TextBox" Width="220px" AutoPostBack="false"></asp:TextBox>
                        </div>
                    </div>
                    <div style="float: left; height: 6px; width: 100%;">
                    </div>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
