<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CustomerCreation.aspx.cs" Inherits="FF.AbansTours.DataEnty.CustomerCreation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script type="text/javascript">

        function jsDecimals(e) {
            var evt = (e) ? e : window.event;
            var key = (evt.keyCode) ? evt.keyCode : evt.which;
            if (key != null) {
                key = parseInt(key, 10);
                if ((key < 48 || key > 57) && (key < 96 || key > 105)) {
                    if (!jsIsUserFriendlyChar(key, "Decimals")) {
                        return false;
                    }
                }
                else {
                    if (evt.shiftKey) {
                        return false;
                    }
                }
            }
            return true;
        };

        function jsIsUserFriendlyChar(val, step) {
            if (val == 8 || val == 9 || val == 13 || val == 45 || val == 46) {
                return true;
            }
            if ((val > 16 && val < 21) || (val > 34 && val < 41)) {
                return true;
            }
            if (step == "Decimals") {
                if (val == 190 || val == 110) {
                    return true;
                }
            }
            return false;
        };

    </script>
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .ajax__tab_xp .ajax__tab_tab {
            height: 25px;
        }

        .btntext {
            -webkit-border-radius: 0;
            -moz-border-radius: 0;
            border-radius: 0px;
            font-family: Arial;
            color: #ffffff;
            font-size: 13px;
            background: #585e61;
            padding: 10px 40px 6px 40px;
            text-decoration: none;
            width = 100px;
        }

        .btn:hover {
            background: #3cb0fd;
            background-image: -webkit-linear-gradient(top, #3cb0fd, #3498db);
            background-image: -moz-linear-gradient(top, #3cb0fd, #3498db);
            background-image: -ms-linear-gradient(top, #3cb0fd, #3498db);
            background-image: -o-linear-gradient(top, #3cb0fd, #3498db);
            background-image: linear-gradient(to bottom, #3cb0fd, #3498db);
            text-decoration: none;
        }

        .myButton {
            -moz-box-shadow: inset 0px 1px 3px 0px #91b8b3;
            -webkit-box-shadow: inset 0px 1px 3px 0px #91b8b3;
            box-shadow: inset 0px 1px 3px 0px #91b8b3;
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0.05, #768d87), color-stop(1, #6c7c7c));
            background: -moz-linear-gradient(top, #768d87 5%, #6c7c7c 100%);
            background: -webkit-linear-gradient(top, #768d87 5%, #6c7c7c 100%);
            background: -o-linear-gradient(top, #768d87 5%, #6c7c7c 100%);
            background: -ms-linear-gradient(top, #768d87 5%, #6c7c7c 100%);
            background: linear-gradient(to bottom, #768d87 5%, #6c7c7c 100%);
            filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#768d87', endColorstr='#6c7c7c',GradientType=0);
            background-color: #768d87;
            -moz-border-radius: 5px;
            -webkit-border-radius: 5px;
            border-radius: 5px;
            border: 1px solid #566963;
            display: inline-block;
            cursor: pointer;
            color: #ffffff;
            font-family: arial;
            font-size: 15px;
            font-weight: bold;
            padding: 11px 23px;
            text-decoration: none;
            text-shadow: 0px -1px 0px #2b665e;
        }

            .myButton:hover {
                background: -webkit-gradient(linear, left top, left bottom, color-stop(0.05, #6c7c7c), color-stop(1, #768d87));
                background: -moz-linear-gradient(top, #6c7c7c 5%, #768d87 100%);
                background: -webkit-linear-gradient(top, #6c7c7c 5%, #768d87 100%);
                background: -o-linear-gradient(top, #6c7c7c 5%, #768d87 100%);
                background: -ms-linear-gradient(top, #6c7c7c 5%, #768d87 100%);
                background: linear-gradient(to bottom, #6c7c7c 5%, #768d87 100%);
                filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#6c7c7c', endColorstr='#768d87',GradientType=0);
                background-color: #6c7c7c;
            }

            .myButton:active {
                position: relative;
                top: 1px;
            }
    </style>
    <asp:UpdatePanel ID="upCommonBtn" runat="server">
        <ContentTemplate>
            <div style="height: 22px; width: 100%; text-align: right; background-color: Silver;">
                <asp:Button ID="btnCreate" Text="Save" runat="server" Width="80px" OnClick="btnCreate_Click" />
                <asp:Button ID="btnClear" Text="Clear" runat="server" Width="80px" OnClick="btnClear_Click" />
                <asp:Button ID="btnBack" Text="Back" runat="server" Width="80px" OnClick="btnBack_Click" />
                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" TargetControlID="btnClear"
                    ConfirmText="Do you want to clear?" ConfirmOnFormSubmit="false">
                </asp:ConfirmButtonExtender>
                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender4" runat="server" TargetControlID="btnCreate"
                    ConfirmText="Do you want to save?" ConfirmOnFormSubmit="false">
                </asp:ConfirmButtonExtender>
                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" TargetControlID="btnBack"
                    ConfirmText="Do you want go to previous page?" ConfirmOnFormSubmit="false">
                </asp:ConfirmButtonExtender>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="col-md-12">
        &nbsp;
    </div>
    <div class="row rowmargin0 col-md-12">
        <div class="panel panel-default">
            <div class="panel-heading pannelheading">
                Customer Creation
            </div>
            <div class="panel-body">
                <div class="row rowmargin0 col-md-12">
                    <asp:UpdatePanel ID="upLevel1" runat="server">
                        <ContentTemplate>
                            <div class="row">

                                <div class="col-md-6">
                                    <div class="col-md-4 padding2">
                                        Customer Type
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <asp:DropDownList ID="cmbType" runat="server" Width="80%" CssClass="ddlhight1">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-4 padding2">
                                        Customer Code
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <asp:TextBox ID="txtCusCode" runat="server" AutoPostBack="true" Width="80%" OnTextChanged="txtCusCode_TextChanged"></asp:TextBox>
                                        <asp:ImageButton ID="btnCustomer" runat="server" ImageUrl="../Images/icon_search.png"
                                            ImageAlign="Middle" OnClick="btnCustomer_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">

                                <div class="col-md-6">
                                    <div class="col-md-4 padding2">
                                        NIC Number
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <asp:TextBox ID="txtNIC" runat="server" AutoPostBack="true" Width="80%" OnTextChanged="txtNIC_TextChanged"
                                            MaxLength="10" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-4 padding2">
                                        BR Number
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <asp:TextBox ID="txtBR" runat="server" AutoPostBack="true" Width="80%" OnTextChanged="txtBR_TextChanged"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="col-md-4 padding2">
                                        Passport Number
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <asp:TextBox ID="txtPP" runat="server" AutoPostBack="true" Width="80%" OnTextChanged="txtPP_TextChanged" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-4 padding2">
                                        Mobile Number
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <asp:TextBox ID="txtMob" runat="server" AutoPostBack="true" Width="80%" MaxLength="10"
                                            OnTextChanged="txtMob_TextChanged"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">

                                <div class="col-md-6">
                                    <div class="col-md-4 padding2">
                                        DL Number
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <asp:TextBox ID="txtDL" runat="server" AutoPostBack="true" Width="80%" OnTextChanged="txtDL_TextChanged" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-4 padding2">
                                        Pref. Language
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <asp:DropDownList ID="cmbPrefLang" runat="server" Width="80%">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="col-md-4 padding2">
                                        Pref. Notification Alert
                                    </div>
                                    <div class="col-md-8 padding2">
                                        <asp:CheckBox ID="chkSMS" Text="  SMS" runat="server" />
                                        <asp:CheckBox ID="chkMail" Text="  Email" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

        <div class="row rowmargin0 col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading  pannelheading ">
                    Personal Details
                </div>
                <div class="panel-body">
                    <asp:UpdatePanel ID="upPersonalDetails" runat="server">
                        <ContentTemplate>
                            <div class="col-md-6 padding2">
                                <div class="col-md-4">
                                    Title
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="cmbTitle" runat="server" Width="80%" CssClass="ddlhight1">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6 padding2">
                                <div class="col-md-4">
                                    Sex
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="cmbSex" runat="server" Width="80%" CssClass="ddlhight1">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6 padding2">
                                <div class="col-md-4">
                                    Initials
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtInit" runat="server" Width="80%" />
                                </div>
                            </div>
                            <div class="col-md-6 padding2">
                                <div class="col-md-4">
                                    Date Of Birth
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtDateOfBirth" Enabled="false" CssClass="input-xlarge focused"
                                        runat="server" Width="80%" onkeypress="return RestrictSpace()"></asp:TextBox>
                                    <asp:CalendarExtender ID="txtClearingDateExtender" runat="server" Enabled="True"
                                        Format="dd/MMM/yyyy" PopupButtonID="cal" TargetControlID="txtDateOfBirth">
                                    </asp:CalendarExtender>
                                    <img alt="Calendar.." runat="server" height="16" src="../images/calendar.png" width="16" id="cal" style="cursor: pointer" />
                                </div>
                            </div>
                            <div class="col-md-6 padding2">
                                <div class="col-md-4">
                                    First Name
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtFname" runat="server" Width="80%" />
                                </div>
                            </div>
                            <div class="col-md-6 padding2">
                                <div class="col-md-4">
                                    Sur Name
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtSName" runat="server" Width="80%" />
                                </div>
                            </div>
                            <div class="col-md-12 padding2">
                                <div class="col-md-2">
                                    Name in full
                                </div>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtName" runat="server" Width="100%" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <asp:HiddenField ID="_isExsit" runat="server" />
            <asp:HiddenField ID="_isGroup" runat="server" />
            <asp:HiddenField ID="_isFromOther" runat="server" />
            <asp:TabContainer runat="server" ID="Tabs" Height="150px" ActiveTabIndex="0">
                <asp:TabPanel runat="server" ID="tbPermenent" HeaderText="Permenent">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="updatePanel1" runat="server">
                            <ContentTemplate>
                                <div style="float: left; height: 22px; width: 100%;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div dir="rtl" style="float: left; width: 10%;" align="right">
                                        Address
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 74%;">
                                        <asp:TextBox ID="txtPerAdd1" runat="server" Width="100%" />
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;
                                    </div>
                                </div>
                                <div style="float: left; height: 22px; width: 100%;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div dir="rtl" style="float: left; width: 10%;" align="right">
                                        Address
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 74%;">
                                        <asp:TextBox ID="txtPerAdd2" runat="server" Width="100%" />
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;
                                    </div>
                                </div>
                                <div style="float: left; height: 22px; width: 100%;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div dir="rtl" style="float: left; width: 10%;" align="right">
                                        Town
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 22%;">
                                        <asp:TextBox ID="txtPerTown" runat="server" Width="100%" OnTextChanged="txtPerTown_TextChanged"
                                            AutoPostBack="true"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 4%;">
                                        &nbsp;<asp:ImageButton ID="btnTownPerment" runat="server" ImageUrl="~/Images/icon_search.png"
                                            ImageAlign="Middle" OnClick="btnTownPerment_Click" />
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;
                                    </div>
                                    <div dir="rtl" style="float: left; width: 11%;" align="right">
                                        District
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 22%;">
                                        <asp:TextBox ID="txtPerDistrict" runat="server" Width="100%"></asp:TextBox>
                                    </div>
                                </div>
                                <div style="float: left; height: 22px; width: 100%;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div dir="rtl" style="float: left; width: 10%;" align="right">
                                        Postal Code
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 27%;">
                                        <asp:TextBox ID="txtPerPostal" runat="server" Width="100%" />
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;
                                    </div>
                                    <div dir="rtl" style="float: left; width: 10%;" align="right">
                                        Province
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 22%;">
                                        <asp:TextBox ID="txtPerProvince" runat="server" Width="100%"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 4%;">
                                        &nbsp;
                                    </div>
                                </div>
                                <div style="float: left; height: 22px; width: 100%;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div dir="rtl" style="float: left; width: 10%;" align="right">
                                        Country
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 27%;">
                                        <asp:TextBox ID="txtPerCountry" runat="server" Width="100%" />
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;
                                    </div>
                                    <div dir="rtl" style="float: left; width: 10%;" align="right">
                                        Phone
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 22%;">
                                        <asp:TextBox ID="txtPerPhone" runat="server" Width="100%" onkeydown="return jsDecimals(event);" OnTextChanged="txtPerPhone_TextChanged"
                                            AutoPostBack="True"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 4%;">
                                        &nbsp;
                                    </div>
                                </div>
                                <div style="float: left; height: 22px; width: 100%;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div dir="rtl" style="float: left; width: 10%;" align="right">
                                        Email
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 74%;">
                                        <asp:TextBox ID="txtPerEmail" runat="server" Width="100%" />
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" ID="tbPresent" HeaderText="Present">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="updatePanel1aa" runat="server">
                            <ContentTemplate>
                                <div style="float: left; height: 22px; width: 100%;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div dir="rtl" style="float: left; width: 10%;" align="right">
                                        Address 1
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 74%;">
                                        <asp:TextBox ID="txtPreAdd1" runat="server" Width="100%" />
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;
                                    </div>
                                </div>
                                <div style="float: left; height: 22px; width: 100%;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div dir="rtl" style="float: left; width: 10%;" align="right">
                                        Address 2
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 74%;">
                                        <asp:TextBox ID="txtPreAdd2" runat="server" Width="100%" />
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;
                                    </div>
                                </div>
                                <div style="float: left; height: 22px; width: 100%;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div dir="rtl" style="float: left; width: 10%;" align="right">
                                        Town
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 22%;">
                                        <asp:TextBox ID="txtPreTown" AutoPostBack="true" runat="server" Width="100%" OnTextChanged="txtPreTown_TextChanged"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 4%;">
                                        &nbsp;<asp:ImageButton ID="btnTownPresent" runat="server" ImageUrl="~/Images/icon_search.png"
                                            ImageAlign="Middle" OnClick="btnTownPresent_Click" />
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;
                                    </div>
                                    <div dir="rtl" style="float: left; width: 10%;" align="right">
                                        District
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 22%;">
                                        <asp:TextBox ID="txtPreDistrict" runat="server" Width="100%"></asp:TextBox>
                                    </div>
                                </div>
                                <div style="float: left; height: 22px; width: 100%;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div dir="rtl" style="float: left; width: 10%;" align="right">
                                        Postal Code
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 27%;">
                                        <asp:TextBox ID="txtPrePostal" runat="server" Width="100%" />
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;
                                    </div>
                                    <div dir="rtl" style="float: left; width: 10%;" align="right">
                                        Province
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 22%;">
                                        <asp:TextBox ID="txtPreProvince" runat="server" Width="100%"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 4%;">
                                        &nbsp;
                                    </div>
                                </div>
                                <div style="float: left; height: 22px; width: 100%;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div dir="rtl" style="float: left; width: 10%;" align="right">
                                        Country
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 27%;">
                                        <asp:TextBox ID="txtPreCountry" runat="server" Width="100%" />
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;
                                    </div>
                                    <div dir="rtl" style="float: left; width: 10%;" align="right">
                                        Phone
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 22%;">
                                        <asp:TextBox ID="txtPrePhone" onkeydown="return jsDecimals(event);" runat="server" Width="100%"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 4%;">
                                        &nbsp;
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" ID="tbWorkingPlace" HeaderText="Working Place">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="updatePanel3" runat="server">
                            <ContentTemplate>
                                <div style="float: left; height: 22px; width: 100%;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div dir="rtl" style="float: left; width: 10%;" align="right">
                                        Name
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 74%;">
                                        <asp:TextBox ID="txtWorkName" runat="server" Width="100%" />
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;
                                    </div>
                                </div>
                                <div style="float: left; height: 22px; width: 100%;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div dir="rtl" style="float: left; width: 10%;" align="right">
                                        Address
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 74%;">
                                        <asp:TextBox ID="txtWorkAdd1" runat="server" Width="100%" />
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;
                                    </div>
                                </div>
                                <div style="float: left; height: 22px; width: 100%;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div dir="rtl" style="float: left; width: 10%;" align="right">
                                        Address
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 74%;">
                                        <asp:TextBox ID="txtWorkAdd2" runat="server" Width="100%" />
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;
                                    </div>
                                </div>
                                <div style="float: left; height: 22px; width: 100%;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div dir="rtl" style="float: left; width: 10%;" align="right">
                                        Department
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 27%;">
                                        <asp:TextBox ID="txtWorkDept" runat="server" Width="100%"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;
                                    </div>
                                    <div dir="rtl" style="float: left; width: 10%;" align="right">
                                        Phone
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 22%;">
                                        <asp:TextBox ID="txtWorkPhone" onkeydown="return jsDecimals(event);" runat="server" Width="100%" AutoPostBack="false" OnTextChanged="txtWorkPhone_TextChanged"></asp:TextBox>
                                    </div>
                                </div>
                                <div style="float: left; height: 22px; width: 100%;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div dir="rtl" style="float: left; width: 10%;" align="right">
                                        Designation
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 27%;">
                                        <asp:TextBox ID="txtWorkDesig" runat="server" Width="100%" />
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;
                                    </div>
                                    <div dir="rtl" style="float: left; width: 10%;" align="right">
                                        Fax
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 22%;">
                                        <asp:TextBox ID="txtWorkFax" runat="server" Width="100%"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 4%;">
                                        &nbsp;
                                    </div>
                                </div>
                                <div style="float: left; height: 22px; width: 100%;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div dir="rtl" style="float: left; width: 10%;" align="right">
                                        Email
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 74%;">
                                        <asp:TextBox ID="txtWorkEmail" runat="server" Width="100%" />
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" ID="tbTXDetails" HeaderText="TX Details">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="updatePanel4" runat="server">
                            <ContentTemplate>
                                <div style="float: left; height: 22px; width: 100%;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div dir="rtl" style="float: left; width: 20%;" align="right">
                                        VAT Customer
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 10%;">
                                        <asp:CheckBox ID="chkVAT" Text="" runat="server" />
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;
                                    </div>
                                    <div dir="rtl" style="float: left; width: 20%;" align="right">
                                        VAT Exempted
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 10%;">
                                        <asp:CheckBox ID="chkVatEx" Text="" runat="server" />
                                    </div>
                                </div>
                                <div style="float: left; height: 22px; width: 100%;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div dir="rtl" style="float: left; width: 20%;" align="right">
                                        VAT Reg. Number
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 20%;">
                                        <asp:TextBox ID="txtVatreg" runat="server" />
                                    </div>
                                </div>
                                <div style="float: left; height: 22px; width: 100%;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div dir="rtl" style="float: left; width: 20%;" align="right">
                                        SVAT Customer
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 10%;">
                                        <asp:CheckBox ID="chkSVAT" Text="" runat="server" />
                                    </div>
                                </div>
                                <div style="float: left; height: 22px; width: 100%;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div dir="rtl" style="float: left; width: 20%;" align="right">
                                        SVAT Reg. Number
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 20%;">
                                        <asp:TextBox ID="txtSVATReg" runat="server" />
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </div>
    </div>
    <div class="col-md-12">
        &nbsp;
    </div>
</asp:Content>