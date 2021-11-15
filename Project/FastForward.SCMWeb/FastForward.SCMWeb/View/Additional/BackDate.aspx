<%@ Page Title="BackDate Module" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="BackDate.aspx.cs" Inherits="FastForward.SCMWeb.View.Additional.BackDate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/ucLoactionSearch.ascx" TagPrefix="uc1" TagName="ucLoactionSearch" %>
<%@ Register Src="~/UserControls/ucProfitCenterSearch.ascx" TagPrefix="uc1" TagName="ucProfitCenterSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <link href="<%=Request.ApplicationPath%>Css/jquery-ui.theme.min.css" rel="stylesheet" />
    <link href="<%=Request.ApplicationPath%>Css/jquery-ui.min.css" rel="stylesheet" />

    <style type="text/css">
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
        }

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

        function showSuccessToast() {
            $().toastmessage('showSuccessToast', "Success Dialog which is fading away ...");
        }
        function showStickySuccessToast(value) {
            $().toastmessage('showToast', {
                text: value,
                sticky: true,
                position: 'top-center',
                type: 'success',
                closeText: '',
                close: function () {
                    console.log("toast is closed ...");
                }
            });

        }
        function showNoticeToast() {
            $().toastmessage('showNoticeToast', "Notice  Dialog which is fading away ...");
        }
        function showStickyNoticeToast(value) {
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
            $().toastmessage('showToast', {
                text: value,
                sticky: true,
                position: 'top-center',
                type: 'notice',
                closeText: '',
                close: function () { console.log("toast is closed ..."); }
            });
        }
        function showWarningToast() {
            $().toastmessage('showWarningToast', "Warning Dialog which is fading away ...");
        }
        function showStickyWarningToast(value) {
            $().toastmessage('showToast', {
                text: value,
                sticky: true,
                position: 'top-center',
                type: 'warning',
                closeText: '',
                close: function () {
                    console.log("toast is closed ...");
                }
            });
        }
        function showErrorToast() {
            $().toastmessage('showErrorToast', "Error Dialog which is fading away ...");
        }
        function showStickyErrorToast(value) {

            $().toastmessage('showToast', {
                text: value,
                sticky: true,
                position: 'top-center',
                type: 'error',
                closeText: '',
                close: function () {
                    console.log("toast is closed ...");
                }
            });
        };

    </script>

    <script type="text/javascript">

        function ConfirmSave() {
            var result = confirm("Do you want to save?");
            if (result) {
                return true;
            }
            else {
                return false;
            }
        };

        function ConfirmClearForm() {
            var result = confirm("Do you want to clear?");
            if (result) {
                return true;
            }
            else {
                return false;
            }
        };

        function ConfirmDelProf() {
            var result = confirm("Do you want to delete this profit center?");
            if (result) {
                return true;
            }
            else {
                return false;
            }
        };

        function ConfirmDelLoc() {
            var result = confirm("Do you want to delete this Location?");
            if (result) {
                return true;
            }
            else {
                return false;
            }
        };

        function ConfirmClearProf() {
            var result = confirm("Do you want to clear all selected profit centers?");
            if (result) {
                return true;
            }
            else {
                return false;
            }
        };

        function pageLoad(sender, args) {
            $("#<%=TextBoxBDAFrom.ClientID %>").datepicker({ dateFormat: "d/M/yy", timeFormat: "hh:mm tt" });
            $("#<%=TextBoxBDATo.ClientID %>").datepicker({ dateFormat: "d/M/yy", timeFormat: "hh:mm tt" });
            $("#<%=TextBoxBDVFrom.ClientID %>").datetimepicker({ dateFormat: "d/M/yy", timeFormat: "hh:mm tt" });
            $("#<%=TextBoxBDVTo.ClientID %>").datetimepicker({ dateFormat: "d/M/yy", timeFormat: "hh:mm tt" });
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdateProgress ID="UpdateProgress3" DisplayAfter="0" runat="server" AssociatedUpdatePanelID="upMainBtn">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait3" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait3" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="panel panel-default marginLeftRight5">
        <div class="row">
            <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:HiddenField runat="server" ID="hdfSeletedTab" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="col-sm-4  buttonRow buttonrowitemchange">

                        <asp:UpdatePanel ID="upMainBtn" runat="server">
                            <ContentTemplate>
                                <div class="col-sm-5 ">
                                </div>
                                <div class="col-sm-4">
                                    <asp:LinkButton ID="btnSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="return ConfirmSave();" OnClick="btnSave_Click">
                                    <span class="glyphicon glyphicon-saved" aria-hidden="true" ></span>Save
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-3">
                                    <asp:LinkButton ID="lbtnprintord" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="return ConfirmClearForm();" OnClick="lbtnprintord_Click">
                                    <span class="glyphicon glyphicon-refresh" aria-hidden="true" ></span>Clear
                                    </asp:LinkButton>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="row">
            <div class="panel-body">
                <div class="col-sm-12">
                    <div class="col-sm-12">
                        <div class="row">
                            <div class="panel panel-default ">
                                <div class="panel-heading pannelheading height16 paddingtop0">
                                    <b>Back Date</b>
                                    <asp:Label Text=" Allow current date transactions" runat="server" CssClass="floatRight" Style="padding-left: 6px;" />
                                    <asp:CheckBox ID="chkNotAllowToday" Text=" " runat="server" CssClass="floatRight" />
                                </div>
                                <div class="panel-body">
                                    <div class="col-sm-3">
                                        <div class="row">
                                            <div class="panel panel-default ">
                                                <div class="panel-heading pannelheading height16 paddingtop0">
                                                    Select Modules
                                                </div>
                                                <div class="panel-body panelscoll" style="height: 465px;">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <asp:TreeView ID="TreeView1" runat="server" ShowCheckBoxes="All" ShowLines="true" Height="100px" Width="100px" ExpandDepth="0" EnableClientScript="false"></asp:TreeView>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-9">
                                        <div class="row">
                                            <div class="panel panel-default ">
                                                <div class="panel-body">
                                                    <div>
                                                        <ul class="nav nav-tabs" role="tablist">
                                                            <li onclick="document.getElementById('<%=hdfSeletedTab.ClientID %>').value = '0';" role="presentation" class="active"><a href="#home" aria-controls="home" role="tab" data-toggle="tab">Profit Centers</a></li>
                                                            <li onclick="document.getElementById('<%=hdfSeletedTab.ClientID %>').value = '1';" role="presentation"><a href="#profile" aria-controls="profile" role="tab" data-toggle="tab">Locations</a></li>
                                                            <li onclick="document.getElementById('<%=hdfSeletedTab.ClientID %>').value = '2';" role="presentation"><a href="#messages" aria-controls="messages" role="tab" data-toggle="tab">Advance</a></li>
                                                        </ul>
                                                        <div class="tab-content">
                                                            <div role="tabpanel" class="tab-pane active" id="home">
                                                                <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="0" runat="server" AssociatedUpdatePanelID="upProf">
                                                                    <ProgressTemplate>
                                                                        <div class="divWaiting">
                                                                            <asp:Label ID="lblWait" runat="server"
                                                                                Text="Please wait... " />
                                                                            <asp:Image ID="imgWait" runat="server"
                                                                                ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
                                                                        </div>
                                                                    </ProgressTemplate>
                                                                </asp:UpdateProgress>

                                                                <asp:UpdateProgress ID="UpdateProgress4" DisplayAfter="0" runat="server" AssociatedUpdatePanelID="upbtnset">
                                                                    <ProgressTemplate>
                                                                        <div class="divWaiting">
                                                                            <asp:Label ID="lblWait4" runat="server"
                                                                                Text="Please wait... " />
                                                                            <asp:Image ID="imgWait4" runat="server"
                                                                                ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
                                                                        </div>
                                                                    </ProgressTemplate>
                                                                </asp:UpdateProgress>
                                                                <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                                                                <asp:UpdatePanel ID="upProf" runat="server">
                                                                    <ContentTemplate>
                                                                        <div class="col-sm-6">
                                                                            <uc1:ucProfitCenterSearch runat="server" ID="ucProfitCenterSearch" />
                                                                        </div>
                                                                        <div class="col-sm-1">
                                                                            <div class="row" style="height: 90px">
                                                                            </div>
                                                                            <div class="row paddingLeft15">
                                                                                <asp:LinkButton ID="btnAddPC" runat="server" CssClass="floatleft" OnClick="btnAddPC_Click">
                                                                                    <span class="glyphicon glyphicon-arrow-right" aria-hidden="true" style="font-size:24px;"></span>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-5">
                                                                            <div class="row" style="height: 200px !important">
                                                                                <div class="GHead2" id="GHead2"></div>
                                                                                <div style="height: 178px; overflow: auto;">
                                                                                    <asp:GridView ID="grvProfCents" AutoGenerateColumns="false" runat="server" CssClass="grvProfCents table table-hover table-striped" GridLines="None" EmptyDataText="No data found..." Style="margin-bottom: 0px">
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText=" ">
                                                                                                <ItemTemplate>
                                                                                                    <asp:CheckBox ID="chkSelectPC" Text=" " runat="server" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Code">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblPROFIT_CENTER" runat="server" Text='<%# Bind("PROFIT_CENTER") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:BoundField DataField="PC_DESCRIPTION" HeaderText="Description" />
                                                                                            <asp:TemplateField HeaderText=" ">
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="btnRemoveProfitCenter" runat="server" CssClass="floatleft" OnClientClick="return ConfirmDelProf()" OnClick="btnRemoveProfitCenter_Click">
                                                                                                    <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>
                                                                                                    </asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-sm-12 paddingLeft0">
                                                                                    <div class="col-sm-4">
                                                                                        <asp:Button ID="btnAllPc" Text="All" runat="server" Width="100px" OnClick="btnAllPc_Click" />
                                                                                    </div>
                                                                                    <div class="col-sm-4">
                                                                                        <asp:Button ID="btnNonePc" Text="None" runat="server" Width="100px" OnClick="btnNonePc_Click" />
                                                                                    </div>
                                                                                    <div class="col-sm-4">
                                                                                        <asp:Button ID="btnClearPc" Text="Clear" runat="server" Width="100px" OnClientClick="return ConfirmClearProf()" OnClick="btnClearPc_Click" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div role="tabpanel" class="tab-pane" id="profile">
                                                                <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="0" runat="server" AssociatedUpdatePanelID="upLocation">
                                                                    <ProgressTemplate>
                                                                        <div class="divWaiting">
                                                                            <asp:Label ID="lblWait1" runat="server"
                                                                                Text="Please wait... " />
                                                                            <asp:Image ID="imgWait1" runat="server"
                                                                                ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
                                                                        </div>
                                                                    </ProgressTemplate>
                                                                </asp:UpdateProgress>
                                                                <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
                                                                <asp:UpdatePanel ID="upLocation" runat="server">
                                                                    <ContentTemplate>
                                                                        <div class="col-sm-6">
                                                                            <uc1:ucLoactionSearch runat="server" ID="ucLoactionSearch" />
                                                                        </div>
                                                                        <div class="col-sm-1">
                                                                            <div class="row" style="height: 90px">
                                                                            </div>
                                                                            <div class="row paddingLeft15">
                                                                                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="btnAddLoc_Click">
                                                                                    <span class="glyphicon glyphicon-arrow-right" aria-hidden="true" style="font-size:24px;"></span>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-5">
                                                                            <div class="row" style="height: 200px !important">
                                                                                <div class="GHead3" id="GHead3"></div>
                                                                                <div style="height: 178px; overflow: auto;">
                                                                                    <asp:GridView ID="GridAllLocations" AutoGenerateColumns="false" runat="server" CssClass="grvProfCents table table-hover table-striped" GridLines="None" EmptyDataText="No data found..." Style="margin-bottom: 0px">
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText=" ">
                                                                                                <ItemTemplate>
                                                                                                    <asp:CheckBox ID="chkSelectLoc" Text=" " runat="server" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Code">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblLOCATION" runat="server" Text='<%# Bind("LOCATION") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:BoundField DataField="LOC_DESCRIPTION" HeaderText="Description" />
                                                                                            <asp:TemplateField HeaderText=" ">
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="btnRemoveLocation" runat="server" CssClass="floatleft" OnClientClick="return ConfirmDelLoc()" OnClick="btnRemoveLocation_Click">
                                                                                                    <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>
                                                                                                    </asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-sm-12 paddingLeft0">
                                                                                    <div class="col-sm-4">
                                                                                        <asp:Button ID="btnAllLoc" Text="All" runat="server" Width="100px" OnClick="btnAllLoc_Click" />
                                                                                    </div>
                                                                                    <div class="col-sm-4">
                                                                                        <asp:Button ID="btnNonLoc" Text="None" runat="server" Width="100px" OnClick="btnNonLoc_Click" />
                                                                                    </div>
                                                                                    <div class="col-sm-4">
                                                                                        <asp:Button ID="btnClearLoc" Text="Clear" runat="server" Width="100px" OnClick="btnClearLoc_Click" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div role="tabpanel" class="tab-pane" id="messages">
                                                                <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                                                                <asp:UpdatePanel runat="server">
                                                                    <ContentTemplate>
                                                                        <div class="col-sm-12 padding0">
                                                                            <div class="col-sm-8">
                                                                                <div class="row">
                                                                                    &nbsp;
                                                                                </div>
                                                                                <div class="row ">
                                                                                    <div class="col-sm-2 padding0">
                                                                                        Company
                                                                                    </div>
                                                                                    <div class="col-sm-4 padding0">
                                                                                        <div class="col-sm-11 padding0">
                                                                                            <asp:TextBox ID="TextBoxCompany_Other" runat="server" CssClass="form-control" Style="text-transform: uppercase" AutoPostBack="true" OnTextChanged="TextBoxCompany_Other_TextChanged" />
                                                                                        </div>
                                                                                        <div class="col-sm-1 padding0">
                                                                                            <asp:LinkButton ID="btnComSearch" runat="server" CssClass="floatRight" OnClick="btnComSearch_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-6">
                                                                                        <asp:TextBox ID="TextBoxCompany_Other_desc" runat="server" CssClass="form-control" ReadOnly="true" />
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row height10"></div>
                                                                                <div class="row">
                                                                                    <div class="col-sm-2 padding0">
                                                                                        Channel
                                                                                    </div>
                                                                                    <div class="col-sm-4 padding0">
                                                                                        <div class="col-sm-11 padding0">
                                                                                            <asp:TextBox ID="TextBoxChannel_Channal" runat="server" CssClass="form-control" Style="text-transform: uppercase" AutoPostBack="true" OnTextChanged="TextBoxChannel_Channal_TextChanged" />
                                                                                        </div>
                                                                                        <div class="col-sm-1 padding0">
                                                                                            <asp:LinkButton ID="btnChannalSearch" runat="server" CssClass="floatRight" OnClick="btnChannalSearch_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-6 ">
                                                                                        <asp:TextBox ID="TextBoxChannel_Channal_desc" ReadOnly="true" runat="server" CssClass="form-control" />
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row height10"></div>
                                                                                <div class="row">
                                                                                    <div class="col-sm-2 padding0">
                                                                                        Operation Admin Team
                                                                                    </div>
                                                                                    <div class="col-sm-4 padding0">
                                                                                        <div class="col-sm-11 padding0">
                                                                                            <asp:TextBox ID="txtOPE_code" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtOPE_code_TextChanged" />
                                                                                        </div>
                                                                                        <div class="col-sm-1 padding0">
                                                                                            <asp:LinkButton ID="btnOprationAdmi" runat="server" CssClass="floatRight" OnClick="btnOprationAdmi_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-6 ">
                                                                                        <asp:TextBox ID="TextBoxCompanyDes" ReadOnly="true" runat="server" CssClass="form-control" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-4">
                                                                                <div class="row">
                                                                                    <div class="col-sm-4">
                                                                                        &nbsp;
                                                                                    </div>
                                                                                    <div class="col-sm-8">
                                                                                        Select Option
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-sm-4">
                                                                                        &nbsp;
                                                                                    </div>
                                                                                    <div class="col-sm-8">
                                                                                        <asp:RadioButtonList ID="rbtOptions" runat="server">
                                                                                            <asp:ListItem Value="rdoCompany" Text="Company" />
                                                                                            <asp:ListItem Value="OPE" Text="OPE" />
                                                                                            <asp:ListItem Value="Channel" Text="Channel" />
                                                                                        </asp:RadioButtonList>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12">
                                                <asp:UpdatePanel ID="upbtnset" runat="server">
                                                    <ContentTemplate>
                                                        <div class="col-sm-5 panel panel-default padding0">
                                                            <div class="panel-heading pannelheading height16 paddingtop0">
                                                                Back Date Allow
                                                            </div>
                                                            <div class="panel-body">
                                                                <div class="col-sm-6 paddingRight0">
                                                                    <div class="col-sm-3 padding0">
                                                                        From
                                                                    </div>
                                                                    <div class="col-sm-8 padding0">
                                                                        <div class="col-sm-11 padding0">
                                                                            <asp:TextBox ID="TextBoxBDAFrom" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                                                onkeypress="return RestrictSpace()" Style="text-transform: uppercase"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1 padding0">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6 paddingRight0">
                                                                    <div class="col-sm-3 padding0">
                                                                        To
                                                                    </div>
                                                                    <div class="col-sm-8 padding0">
                                                                        <div class="col-sm-11 padding0">
                                                                            <asp:TextBox ID="TextBoxBDATo" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                                                onkeypress="return RestrictSpace()"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1 padding0">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6 panel panel-default padding0">
                                                            <div class="panel-heading pannelheading height16 paddingtop0">
                                                                Back Date Valid
                                                            </div>
                                                            <div class="panel-body">
                                                                <div class="col-sm-6 padding0">
                                                                    <div class="col-sm-2 padding0">
                                                                        From
                                                                    </div>
                                                                    <div class="col-sm-10 padding0">
                                                                        <div class="col-sm-11 padding0">
                                                                            <asp:TextBox ID="TextBoxBDVFrom" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                                                onkeypress="return RestrictSpace()" Style="text-transform: uppercase"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1 padding0">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6 paddingRight0">
                                                                    <div class="col-sm-1 padding0">
                                                                        To
                                                                    </div>
                                                                    <div class="col-sm-11 padding0">
                                                                        <div class="col-sm-11 padding0">
                                                                            <asp:TextBox ID="TextBoxBDVTo" runat="server" CssClass="form-control" Format="dd/MMM/yyyy hh:mm tt"
                                                                                onkeypress="return RestrictSpace()" Style="text-transform: uppercase"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1 padding0">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-1 panel panel-default padding0">
                                                            <div class="panel-body" style="font-size: 25px;">
                                                                <asp:LinkButton ID="ImgBtnViewBD" CausesValidation="false" runat="server" OnClick="ImgBtnViewBD_Click">
                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true" style="font-size:24px; padding-left:22px;"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 padding0">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <div class="panel panel-default ">
                                                        <div class="panel-heading pannelheading height16 paddingtop0">
                                                            View Back Dates
                                                        </div>
                                                        <div class="panel-body GridScroll">
                                                            <div class="GHead" id="GHead"></div>
                                                            <div style="height: 80px; overflow: auto;">
                                                                <asp:GridView ID="grvHistoryBackDates" runat="server" GridLines="None" EmptyDataText="No data found..." CssClass="grvHistoryBackDates table table-hover table-striped" AutoGenerateColumns="False" Style="margin-bottom: 0px;">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="GAD_COM" HeaderText="Com" ItemStyle-Width="70px" />
                                                                        <asp:BoundField DataField="GAD_LOC" HeaderText="PC / Location" ItemStyle-Width="70" />
                                                                        <asp:BoundField DataField="GAD_OPE" HeaderText="OPE" />
                                                                        <asp:BoundField DataField="SSM_DISP_NAME" HeaderText="Module Name" ItemStyle-Width="170" />
                                                                        <asp:BoundField DataField="GAD_FROM_DT" HeaderText="From" DataFormatString="{0:dd/MMM/yyyy hh:mm tt}" ItemStyle-Width="170" />
                                                                        <asp:BoundField DataField="GAD_TO_DT" HeaderText="To" DataFormatString="{0:dd/MMM/yyyy hh:mm tt}" ItemStyle-Width="170" />
                                                                        <asp:BoundField DataField="GAD_ACT_FROM_DT" HeaderText="Active From" DataFormatString="{0:dd/MMM/yyyy}" ItemStyle-Width="90" />
                                                                        <asp:BoundField DataField="GAD_ACT_TO_DT" HeaderText="Active To" DataFormatString="{0:dd/MMM/yyyy}" ItemStyle-Width="90" />
                                                                        <asp:BoundField DataField="GAD_CRE_BY" HeaderText="Created By" ItemStyle-Width="90" />
                                                                        <asp:BoundField DataField="GAD_CRE_DT" HeaderText="Created Date" ItemStyle-Width="165" />
                                                                        <asp:BoundField DataField="GAD_MODULE" HeaderText="Module ID" Visible="false" />
                                                                        <asp:TemplateField HeaderText="Allow Current Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblJan" runat="server" Text='<%# Bind("GAD_ALW_CURR_TRANS") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:Button ID="btnSearchbtn" runat="server" Text="Button" Style="display: none;" />
                <asp:ModalPopupExtender ID="mpSearch" runat="server" TargetControlID="btnSearchbtn"
                    PopupControlID="pnlpopup" PopupDragHandleControlID="divSearchheader" Drag="true" BackgroundCssClass="modalBackground">
                </asp:ModalPopupExtender>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:Panel runat="server" ID="pnlpopup" DefaultButton="lbtnSearch">
            <asp:ScriptManagerProxy ID="ScriptManagerProxy17" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div runat="server" id="test" class="panel panel-default height400 width850">
                        <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
                        <div class="panel panel-default">
                            <div class="panel-heading height30" id="divSearchheader">
                                <div class="col-sm-11">
                                </div>
                                <div class="col-sm-1">
                                    <asp:LinkButton ID="btnCloseSearchMP" runat="server" OnClick="btnCloseSearchMP_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="panel-body">
                                <div class="col-sm-12" id="Div10" runat="server">
                                    <div class="row">
                                        <div class="col-sm-12">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-sm-2 labelText1">
                                                Search by key
                                            </div>
                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy10" runat="server"></asp:ScriptManagerProxy>
                                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-sm-3 paddingRight5">
                                                        <asp:DropDownList ID="ddlSearchbykey" runat="server" class="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="col-sm-2 labelText1">
                                                Search by word
                                            </div>
                                            <div class="col-sm-4 paddingRight5">
                                                <asp:TextBox ID="txtSearchbyword" CausesValidation="false" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbyword_TextChanged"></asp:TextBox>
                                            </div>
                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy9" runat="server"></asp:ScriptManagerProxy>
                                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnSearch" runat="server" OnClick="lbtnSearch_Click">
                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="txtSearchbyword" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy4" runat="server"></asp:ScriptManagerProxy>
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView ID="grdResult" CausesValidation="false" AllowPaging="true" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" OnPageIndexChanging="grdResult_PageIndexChanging" OnSelectedIndexChanged="grdResult_SelectedIndexChanged">
                                                        <Columns>
                                                            <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>

        <script>
            Sys.Application.add_load(initSomething);
            Sys.Application.add_load(FreezePCGrid);
            Sys.Application.add_load(FreezeLOCGrid);

            function initSomething() {
                if (typeof jQuery == 'undefined') {
                    alert('jQuery is not loaded');
                }
                $('.grvHistoryBackDates').ready(function () {
                    var gridHeader = $('#<%=grvHistoryBackDates.ClientID%>').clone(true); // Here Clone Copy of Gridview with style
                    $(gridHeader).find("tr:gt(0)").remove(); // Here remove all rows except first row (header row)
                    var v = 0;
                    $('#<%=grvHistoryBackDates.ClientID%> tbody tr td').each(function (i) {
                        // Here Set Width of each th from gridview to new table(clone table) th 
                        if (v < $(this).width()) {
                            v = $(this).width();
                        }
                        console.log((v).toString());
                        $("th:nth-child(" + (i + 1) + ")", gridHeader).css('width', ($(this).width()).toString() + "px");
                    });
                    $('.GHead').append(gridHeader);
                    $('.GHead').css('position', 'inherit');
                    $('.GHead').css('width', '100%');
                    $('.GHead').css('top', $('#<%=grvHistoryBackDates.ClientID%>').offset().top);
                    jQuery('#BodyContent_grvHistoryBackDates tbody').children('tr').eq(1).remove();

                });
            };

            function FreezePCGrid() {
                if (typeof jQuery == 'undefined') {
                    alert('jQuery is not loaded');
                }
                $('.grvProfCents').ready(function () {
                    var gridHeader1 = $('#<%=grvProfCents.ClientID%>').clone(true); // Here Clone Copy of Gridview with style
                    $(gridHeader1).find("tr:gt(0)").remove(); // Here remove all rows except first row (header row)
                    var v = 0;
                    $('#<%=grvProfCents.ClientID%> tbody tr td').each(function (i) {
                        // Here Set Width of each th from gridview to new table(clone table) th 
                        if (v < $(this).width()) {
                            v = $(this).width();
                        }
                        console.log((v).toString());
                        $("th:nth-child(" + (i + 1) + ")", gridHeader1).css('width', ($(this).width()).toString() + "px");
                    });
                    $('.GHead2').append(gridHeader1);
                    $('.GHead2').css('position', 'inherit');
                    $('.GHead2').css('width', '100%');
                    $('.GHead2').css('top', $('#<%=grvProfCents.ClientID%>').offset().top);
                    jQuery('#BodyContent_grvProfCents tbody').children('tr').eq(1).remove();

                });
            };
            function FreezeLOCGrid() {
                if (typeof jQuery == 'undefined') {
                    alert('jQuery is not loaded');
                }
                $('.GridAllLocations').ready(function () {
                    var gridHeader1 = $('#<%=GridAllLocations.ClientID%>').clone(true); // Here Clone Copy of Gridview with style
                    $(gridHeader1).find("tr:gt(0)").remove(); // Here remove all rows except first row (header row)
                    var v = 0;
                    $('#<%=GridAllLocations.ClientID%> tbody tr td').each(function (i) {
                        // Here Set Width of each th from gridview to new table(clone table) th 
                        if (v < $(this).width()) {
                            v = $(this).width();
                        }
                        console.log((v).toString());
                        $("th:nth-child(" + (i + 1) + ")", gridHeader1).css('width', ($(this).width()).toString() + "px");
                    });
                    $('.GHead3').append(gridHeader1);
                    $('.GHead3').css('position', 'inherit');
                    $('.GHead3').css('width', '100%');
                    $('.GHead3').css('top', $('#<%=GridAllLocations.ClientID%>').offset().top);
                    jQuery('#BodyContent_GridAllLocations tbody').children('tr').eq(1).remove();

                });
            };

        </script>
</asp:Content>
