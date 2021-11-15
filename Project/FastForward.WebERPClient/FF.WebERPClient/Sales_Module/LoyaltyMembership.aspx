<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="LoyaltyMembership.aspx.cs" Inherits="FF.WebERPClient.Sales_Module.LoyaltyMembership" %>

<%@ Register Src="../UserControls/uc_CustomerCreation.ascx" TagName="uc_CustomerCreation"
    TagPrefix="uc2" %>
<%@ Register Src="../UserControls/uc_CustCreationExternalDet.ascx" TagName="uc_CustCreationExternalDet"
    TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function numbersonly(e, decimal) {
            var key;
            var keychar;

            if (window.event) {
                key = window.event.keyCode;
            }
            else if (e) {
                key = e.which;
            }
            else {
                return true;
            }
            keychar = String.fromCharCode(key);

            if ((key == null) || (key == 0) || (key == 8) || (key == 9) || (key == 13) || (key == 27)) {
                return true;
            }
            else if ((("0123456789").indexOf(keychar) > -1)) {
                return true;
            }
            else if (decimal && (keychar == ".")) {
                return true;
            }
            else
                return false;
        }
    </script>
    <style>
        .Panel legend
        {
            color: Blue;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>
            <div style="height: 22px; text-align: right;" class="PanelHeader">
                <asp:Button ID="ButtonSave" runat="server" Text="Save" CssClass="Button" OnClick="ButtonSave_Click" />
                <asp:Button ID="ButtonClear" runat="server" Text="Clear" CssClass="Button" OnClick="ButtonClear_Click" />
                <asp:Button ID="ButtonClose" runat="server" Text="Close" CssClass="Button" OnClick="ButtonClose_Click" />
            </div>
            <div style="float: left; height: 10px; width: 100%">
                &nbsp;
            </div>
            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                <asp:TabPanel ID="TabPanel1" HeaderText="Loyalty Membership" runat="server">
                    <ContentTemplate>
                        <div style="float: left; height: 10%; color: Black;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 100%; color: Black; font-family: Verdana; font-size: 11px;">
                            <div style="float: left; width: 39%; color: Black;">
                                <asp:Panel ID="Panel1" runat="server" GroupingText="Membership Details" CssClass="Panel">
                                    <div style="float: left; width: 100%;">
                                        <div style="float: left; width: 1%; color: Black;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 30%;">
                                            Loyality Type
                                        </div>
                                        <div style="float: left; width: 69%;">
                                            <asp:TextBox ID="TextBoxLoyaltyType" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                                             <asp:ImageButton ID="ImageButtonLoyalty" runat="server" 
                                                ImageUrl="~/Images/icon_search.png" onclick="ImageButtonLoyalty_Click"
                                                                />
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; padding-top: 2px;">
                                        <div style="float: left; width: 1%; color: Black;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 30%;">
                                            Card Number
                                        </div>
                                        <div style="float: left; width: 69%;">
                                            <asp:TextBox ID="TextBoxLoyaltyNumber" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; padding-top: 2px;">
                                        <div style="float: left; width: 1%; color: Black;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 30%;">
                                            Card serial
                                        </div>
                                        <div style="float: left; width: 69%;">
                                            <asp:TextBox ID="TextBoxCardSerial" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; padding-top: 2px;">
                                        <div style="float: left; width: 1%; color: Black;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 30%;">
                                            Customer Code
                                        </div>
                                        <div style="float: left; width: 69%;">
                                            <asp:TextBox ID="TextBoxCusCode" runat="server" CssClass="TextBox TextBoxUpper" Enabled="False"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; padding-top: 2px;">
                                        <div style="float: left; width: 1%; color: Black;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 30%;">
                                            valid From
                                        </div>
                                        <div style="float: left; width: 69%;">
                                            <asp:TextBox ID="TextBoxValidFrom" runat="server" CssClass="TextBox TextBoxUpper"
                                                Enabled="False" Width="108px"></asp:TextBox>
                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBoxValidFrom"
                                                PopupButtonID="Image1" Enabled="True" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; padding-top: 2px;">
                                        <div style="float: left; width: 1%; color: Black;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 30%;">
                                            Valid To
                                        </div>
                                        <div style="float: left; width: 69%;">
                                            <asp:TextBox ID="TextBoxValidTo" runat="server" CssClass="TextBox TextBoxUpper" Enabled="False"
                                                Width="108px"></asp:TextBox>
                                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TextBoxValidTo"
                                                PopupButtonID="Image2" Enabled="True" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; padding-top: 2px;">
                                        <div style="float: left; width: 1%; color: Black;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 30%;">
                                            Contact No
                                        </div>
                                        <div style="float: left; width: 69%;">
                                            <asp:TextBox ID="TextBoxContactNo" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; padding-top: 2px;">
                                        <div style="float: left; width: 1%; color: Black;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 30%;">
                                            Email
                                        </div>
                                        <div style="float: left; width: 69%;">
                                            <asp:TextBox ID="TextBoxEmail" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                            <div style="float: left; width: 1%">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 59%">
                                <asp:Panel ID="Panel2" runat="server" GroupingText="Customer Details" CssClass="Panel">
                                    <asp:Panel ID="PanelCCre" runat="server" Height="470px" ScrollBars="Auto">
                                        <div style="float: left; height: 8px; width: 100%;">
                                        </div>
                                        <div style="height: 22px; text-align: right;">
                                            <asp:Button ID="ButtonAddCus" runat="server" Text="Save" Height="80%" Width="70px"
                                                CssClass="Button" OnClick="ButtonAddCus_Click" />
                                        </div>
                                        <div style="float: left; width: 100%">
                                            <div style="float: left; width: 100%">
                                                <uc2:uc_CustomerCreation ID="cusCreP1" runat="server" />
                                            </div>
                                            <div style="float: left; width: 100%">
                                                <uc3:uc_CustCreationExternalDet ID="cusCreP2" runat="server" EnableViewState="False" />
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </asp:Panel>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel2" HeaderText="History Transfer" runat="server">
                    <ContentTemplate>
                        <div style="float: left; height: 10%; color: Black;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 100%; color: Black; font-family: Verdana; font-size: 11px;">
                            <div style="float: left; width: 100%; color: Black;">
                                <asp:Panel ID="Panel3" runat="server" GroupingText=" " CssClass="Panel">
                                    <div style="float: left; width: 100%; padding-top: 3px;">
                                        <div style="float: left; width: 1%; color: Black;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 30%; color: Black;">
                                            Customer Identity
                                        </div>
                                        <div style="float: left; width: 69%; color: Black;">
                                            <asp:TextBox ID="TextBoxTHCusCd" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                                            <asp:Button ID="ButtonCheck" runat="server" CssClass="Button" Text="Check" OnClick="ButtonCheck_Click" />
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%;padding-top: 3px;">
                                        <div style="float: left; width: 1%; color: Black;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 30%; color: Black;">
                                            Card No
                                        </div>
                                        <div style="float: left; width: 69%; color: Black;">
                                            <asp:TextBox ID="TextBoxTHCardNo" runat="server" CssClass="TextBox TextBoxUpper"
                                                AutoPostBack="True" OnTextChanged="TextBoxTHCardNo_TextChanged"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; padding-top: 3px;">
                                        <div style="float: left; width: 1%; color: Black;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 30%; color: Black;">
                                            Loyalty Type
                                        </div>
                                        <div style="float: left; width: 69%; color: Black;">
                                            <asp:DropDownList ID="DropDownListLoyaltyType" runat="server" CssClass="ComboBox"
                                                AppendDataBoundItems="True">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; padding-top: 3px;">
                                        <%--  <asp:Button ID="ButtonGet" runat="server" Text="Get" />--%>
                                    </div>
                                    <div style="float: left; width: 100%; padding-top: 3px;">
                                        <asp:Panel ID="PanelLoyaltyDetails" runat="server" CssClass="Panel" GroupingText="Loyalty Details" Visible="false">
                                            <div style="float: left; width: 100%;">
                                                <div style="float: left; width: 1%; color: Black;">
                                                    &nbsp;
                                                </div>
                                                <div style="float: left; width: 48%; color: Black;">
                                                    
                                                </div>
                                                <div style="float: left; width: 1%; color: Black;">
                                                    &nbsp;
                                                </div>
                                                <div style="float: left; width: 48%; color: Black;">
                                                   
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <div style="float: left; width: 100%; padding-top: 3px;">
                                        <div style="float: left; width: 1%; color: Black;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 30%; color: Black;">
                                            New Serial
                                        </div>
                                        <div style="float: left; width: 69%; color: Black;">
                                            <asp:TextBox ID="TextBoxNewSerial" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
