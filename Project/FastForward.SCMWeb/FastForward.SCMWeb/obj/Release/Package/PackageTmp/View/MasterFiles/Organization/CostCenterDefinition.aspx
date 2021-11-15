<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="CostCenterDefinition.aspx.cs" Inherits="FastForward.SCMWeb.View.MasterFiles.Organization.CostCenterDefinition" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
    <%@ Register Src="~/UserControls/ucHirerachyDetails.ascx" TagPrefix="uc1" TagName="ucLoc1" %>
    <%@ Register Src="~/UserControls/ucTransportMethode.ascx" TagPrefix="uc2" TagName="ucTransportMethode" %>


    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/jquery.toastmessage.js"></script>
    <script>
        function SaveConfirm() {
            var selectedvalue = confirm("Do you want to save data?");
            if (selectedvalue) {
                document.getElementById('<%=txtSaveconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtSaveconformmessageValue.ClientID %>').value = "No";
            }
        };

    </script>
    <script type="text/javascript">
        function ConfirmClearForm() {
            var selectedvalueOrd = confirm("Do you want to clear all details ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function showSuccessToast() {
            $().toastmessage('showSuccessToast', "Success Dialog which is fading away ...");
        }
        function showStickySuccessToast(value) {
            $().toastmessage('showToast', {
                text: value,
                sticky: false,
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
            $().toastmessage('showToast', {
                text: value,
                sticky: false,
                position: 'top-left',
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
        }

        //allow only strings 
        function jsStrings(event) {

            var englishAlphabetAndWhiteSpace = /[A-Za-z ]/g;
            var key = String.fromCharCode(event.which);
            console.log("here");
            if (event.keyCode == 8 || event.keyCode == 37 || event.keyCode == 39 || englishAlphabetAndWhiteSpace.test(key)) {
                return true;
            }

            return false;
        }

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



    </script>
    <style type="text/css">
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

        .panel.panel-heading {
            margin-bottom: 0;
        }
    </style>
    <style>
      
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="pnlTopPanel">

        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait1" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait1" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>

    </asp:UpdateProgress>

    <asp:HiddenField ID="txtClearlconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtSaveconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtUpdateconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtApprovalconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtCancelconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtDeleteconformmessageValue" runat="server" />
    <asp:HiddenField ID="hdfTabIndex" runat="server" />

    <div class="panel panel-default paddingLeft15 paddingRight15">
        <div class="panel-body">
            <asp:UpdatePanel ID="pnlTopPanel" runat="server">
                <ContentTemplate>
                    <div class="row buttonRow">
                        <div class="col-sm-12">
                            <div class="col-sm-10">
                            </div>
                            <div class="col-sm-2">
                                <div class="col-sm-6">
                                    <asp:LinkButton ID="lbtnSaveRoleOpt" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="SaveConfirm()" OnClick="lbtnSaveRoleOpt_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true" ></span>Save
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-6">
                                    <asp:LinkButton ID="linkClear" runat="server" CausesValidation="false" CssClass="floatRight"
                                        OnClick="linkClear_Click" OnClientClick="return ConfirmClearForm();">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true" ></span>Clear
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <strong><b>Profit/ Cost Center Definition</b></strong>
                            </div>
                            <div class="panel-body">

                                <div class="row">
                                    <div class="col-sm-5">
                                        <%-- Hierarchy Details --%>
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                <strong><b>Hierachy Details</b></strong>
                                            </div>
                                            <div class="panel-body">
                                                <uc1:ucLoc1 ID="ucLoc1" runat="server" />
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height45">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-7 paddingLeft0">
                                        <%-- Basic Details --%>
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                <strong><b>Main Details</b></strong>
                                            </div>
                                            <div class="panel-body paddingLeft15">
                                                <div class="row">
                                                    <div class="col-sm-2 labelText1">
                                                        Code
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox OnTextChanged="txtPC_TextChanged" AutoPostBack="true"
                                                            Style="text-transform: uppercase" ID="txtPC" runat="server"
                                                            CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1" style="padding-left: 3px;">
                                                        <asp:LinkButton ID="LinkButtonCode" runat="server" OnClick="LinkButtonCode_Click">
                                                                            <span class="glyphicon glyphicon-search"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        Type
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <asp:DropDownList ID="cmbType" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value="0" Text="--SELECT--" />
                                                            <asp:ListItem Value="P">PROFIT</asp:ListItem>
                                                            <asp:ListItem Value="C">COST</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-2 labelText1">
                                                        Description
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <asp:TextBox ID="txtDesc" Style="text-transform: uppercase" runat="server" CssClass="form-control" OnTextChanged="txtDesc_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        Province
                                                    </div>
                                                    <div class="col-sm-2 paddingRight0">
                                                        <asp:TextBox ID="txtProvince" Style="text-transform: uppercase" AutoPostBack="true" OnTextChanged="txtProvince_TextChanged" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1" style="padding-left: 3px;">
                                                        <asp:LinkButton ID="LinkButtonprovince" runat="server" OnClick="LinkButtonprovince_Click">
                                                                            <span class="glyphicon glyphicon-search"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-2 labelText1">
                                                        Address
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <asp:TextBox ID="txtAdd1" Style="text-transform: uppercase" runat="server" CssClass="form-control" MaxLength="100" OnTextChanged="txtAdd1_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        District
                                                    </div>
                                                    <div class="col-sm-2 paddingRight0">
                                                        <asp:TextBox ID="txtDistrict" Style="text-transform: uppercase" AutoPostBack="true" OnTextChanged="txtDistrict_TextChanged" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1" style="padding-left: 3px;">
                                                        <asp:LinkButton ID="LinkButtonDistrict" runat="server" OnClick="LinkButtonDistrict_Click">
                                                         <span class="glyphicon glyphicon-search"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-2 labelText1">
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <asp:TextBox ID="txtAdd2" runat="server" Style="text-transform: uppercase" CssClass="form-control" MaxLength="100" OnTextChanged="txtAdd2_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        Town
                                                    </div>
                                                    <div class="col-sm-2 paddingRight0">
                                                        <asp:TextBox ID="txtTown" Style="text-transform: uppercase" AutoPostBack="true" OnTextChanged="txtTown_TextChanged" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1" style="padding-left: 3px;">
                                                        <asp:LinkButton ID="LinkButtonTown" runat="server" OnClick="LinkButtonTown_Click">
                                                         <span class="glyphicon glyphicon-search"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-2 labelText1">
                                                        Main PC
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox OnTextChanged="txtMainPC_TextChanged" AutoPostBack="true"
                                                            Style="text-transform: uppercase" ID="txtMainPC" runat="server"
                                                            CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1" style="padding-left: 3px;">
                                                        <asp:LinkButton ID="LinkButtonMainPC" runat="server" OnClick="LinkButtonMainPC_Click">
                                                                            <span class="glyphicon glyphicon-search"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        Other Ref #
                                                    </div>
                                                    <div class="col-sm-2 paddingRight0">
                                                        <asp:TextBox ID="txtOthRef" runat="server" CssClass="form-control" MaxLength="28" OnTextChanged="txtOthRef_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-2 labelText1">
                                                        Phone #
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtPhone" runat="server" CssClass="txtPhone form-control" OnTextChanged="txtPhone_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1">
                                                    </div>
                                                    <div class="col-sm-2 labelText1">
                                                        Email
                                                    </div>
                                                    <div class="col-sm-4 ">
                                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="txtEmail form-control" MaxLength="100"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-2 labelText1">
                                                        Fax # 
                                                    </div>
                                                    <div class="col-sm-3 ">
                                                        <asp:TextBox ID="txtFax" runat="server" CssClass="txtFax form-control" OnTextChanged="txtFax_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1">
                                                    </div>
                                                    <div class="col-sm-2 labelText1">
                                                        Active
                                                    </div>
                                                    <div class="col-sm-2" style="margin-top: 3px; padding-right: 15px;">
                                                        <asp:CheckBox ID="chkAct" runat="server" Checked="true" Text="" CssClass="" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-2 labelText1">
                                                        Manager EPF #
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtEpf" Enabled="true" CausesValidation="false" runat="server" CssClass="form-control" MaxLength="15" OnTextChanged="txtEpf_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1">
                                                    </div>
                                                    <div class="col-sm-2">
                                                        Manager Name
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <asp:TextBox ID="txtManName" Style="text-transform: uppercase" Enabled="true" CausesValidation="false" runat="server" CssClass="form-control" onkeydown="return jsStrings(event);" AutoPostBack="true" onpaste="return false" MaxLength="100" OnTextChanged="txtManName_TextChanged"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-4">
                                                        <div class="col-sm-4 padding0 labelText1">
                                                            Open Date    
                                                        </div>
                                                        <%-- <div class="col-sm-6 padding0">
                                                                                    <asp:TextBox ID="dtOpenDate" onkeypress="return RestrictSpace()"
                                                                                        Enabled="false" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="dtOpenDate_TextChange"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-sm-1" style="padding-left: 3px;">
                                                                                    <asp:LinkButton ID="btFrom" runat="server" CausesValidation="false"> <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span></asp:LinkButton>
                                                                                    <asp:CalendarExtender ID="CalendarExtenderfrom" runat="server" TargetControlID="dtOpenDate" Animated="true"
                                                                                        PopupButtonID="btFrom" Format="dd/MMM/yyyy">
                                                                                    </asp:CalendarExtender>
                                                                                </div>--%>
                                                        <div class="col-sm-6 padding0">
                                                            <asp:TextBox ID="dtOpenDate" runat="server" onkeypress="return RestrictSpace()" CssClass="form-control" Format="dd/MMM/yyyy" Enabled="true" AutoPostBack="true" OnTextChanged="dtOpenDate_TextChange"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1" style="padding-left: 3px;">
                                                            <asp:LinkButton ID="btFrom" runat="server" CausesValidation="false"> <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span></asp:LinkButton>
                                                            <asp:CalendarExtender ID="CalendarExtenderfrom" runat="server" TargetControlID="dtOpenDate"
                                                                PopupButtonID="btFrom" Format="dd/MMM/yyyy">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="col-sm-4 padding0 labelText1">
                                                            H/O Date    
                                                        </div>
                                                        <div class="col-sm-6 padding0">
                                                            <asp:TextBox ID="dtHOvr" Enabled="false" CausesValidation="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1" style="padding-left: 3px;">
                                                            <asp:LinkButton ID="LinkButtonto" runat="server" CausesValidation="false"> <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span></asp:LinkButton>
                                                            <asp:CalendarExtender ID="CalendarExtenderto" runat="server" TargetControlID="dtHOvr" Animated="true"
                                                                PopupButtonID="LinkButtonto" Format="dd/MMM/yyyy" BehaviorID="handOverDateExtender">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="col-sm-4 padding0 labelText1">
                                                            Joined Date    
                                                        </div>
                                                        <div class="col-sm-6 padding0">
                                                            <asp:TextBox ID="dtJoined" Enabled="false" CausesValidation="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1" style="padding-left: 3px;">
                                                            <asp:LinkButton ID="LinkButtonjoineddate" runat="server" CausesValidation="false"> <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span></asp:LinkButton>
                                                            <asp:CalendarExtender ID="CalendarExtenderjoineddate" runat="server" TargetControlID="dtJoined" Animated="true"
                                                                PopupButtonID="LinkButtonjoineddate" Format="dd/MMM/yyyy">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-4">
                                                        <div class="col-sm-4 padding0">
                                                            Square feet
                                                        </div>
                                                        <div class="col-sm-6 padding0">
                                                            <asp:TextBox ID="txtSquareFeet" Enabled="true" CausesValidation="false" runat="server" CssClass="form-control" Text="0" AutoPostBack="true" onpaste="return false" OnTextChanged="txtSquareFeet_TextChanged"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="col-sm-4 padding0">
                                                            No of Staff
                                                        </div>
                                                        <div class="col-sm-6 padding0 ">
                                                            <asp:TextBox ID="txtstaff" Enabled="true" CausesValidation="false" runat="server" CssClass="txtstaff form-control" Text="0" onkeydown="return jsStrings(event);" AutoPostBack="true" onpaste="return false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="col-sm-4 padding0 labelText1">
                                                            Grade
                                                        </div>
                                                        <div class="col-sm-6 padding0 ">
                                                            <%--<asp:TextBox ID="txtGrade" Enabled="true" CausesValidation="false" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                            <asp:TextBox ID="txtGrade" Style="text-transform: uppercase" AutoPostBack="true" OnTextChanged="txtGrade_TextChanged" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    <div class="col-sm-1" style="padding-left: 3px;">
                                                        <asp:LinkButton ID="LinkButtonGrade" runat="server" OnClick="LinkButtonGrade_Click">
                                                         <span class="glyphicon glyphicon-search"></span>
                                                        </asp:LinkButton>
                                                    </div>


                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-4 paddingRight0">
                                                        <div class="col-sm-1 padding0" style="margin-top: 3px; padding-left: 0px; padding-right: 15px;">
                                                            <asp:CheckBox ID="chkFwdSale" runat="server" AutoPostBack="true" Text=""
                                                                CssClass="" OnCheckedChanged="chkFwdSale_CheckedChanged" />
                                                        </div>
                                                        <div class="col-sm-7 padding0 labelText1">
                                                            <asp:Label Text="Forward Sales Consider From" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-2 padding0">
                                                        <div class="col-sm-10 labelText1 padding0">
                                                            <asp:TextBox ID="dtFwdSale" Enabled="false" CausesValidation="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1" style="padding-left: 3px; margin-top: 3px;">
                                                            <asp:LinkButton ID="LinkButtonforwardsales" runat="server" CausesValidation="false"> <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span></asp:LinkButton>
                                                            <asp:CalendarExtender ID="CalendarExtenderforwardsales" runat="server" TargetControlID="dtFwdSale" Animated="true"
                                                                PopupButtonID="LinkButtonforwardsales" Format="dd/MMM/yyyy">
                                                            </asp:CalendarExtender>

                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="col-sm-5 paddingRight0 labelText1">
                                                            Maximum Forward Sales
                                                        </div>
                                                        <div class="col-sm-3 padding0">
                                                            <asp:TextBox ID="txtMaxFwdSale" Enabled="true" CausesValidation="false" runat="server" CssClass="txtMaxFwdSale form-control" Text="0" onkeypress='return ((event.charCode >= 48 && event.charCode <= 57) | event.charCode ==8 )' AutoPostBack="true" onpaste="return false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>


                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="col-sm-4">
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-5">
                                        <%-- General Parameters --%>
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                <b>General Parameters</b>
                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-3 labelText1 padding0">
                                                            <asp:Label ID="LabelAddhours" runat="server" Text="Add. Hours"></asp:Label>
                                                        </div>

                                                        <div class="col-sm-2 labelText1 padding0">
                                                            <asp:TextBox ID="txtAddHours" runat="server" CssClass="txtAddHours form-control" Text="0" onkeydown="return jsStrings(event);" AutoPostBack="true" onpaste="return false"></asp:TextBox>

                                                        </div>
                                                        <div class="col-sm-1">
                                                        </div>
                                                        <!-- newly added -->
                                                        <div class="col-sm-2 labelText1 padding0">
                                                            <asp:Label ID="LabelAdminTeam" runat="server" Text="Opr. Admin Team"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-3 paddingLeft5 labelText1" style="width: 128px">
                                                            <asp:TextBox ID="txtOprTeam" AutoPostBack="true" runat="server" CssClass="form-control" OnTextChanged="txtOprTeam_TextChanged"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 padding0 labelText1" style="padding-left: 2px; padding-right: 0px;">
                                                            <asp:LinkButton ID="lbtnSeAdminTm" runat="server" Visible="true" OnClick="lbtnSeAdminTm_Click">
                                                                                <span class="glyphicon glyphicon-search"></span>
                                                            </asp:LinkButton>
                                                        </div>

                                                        <!-- -->

                                                    </div>
                                                </div>

                                                <!-- deleted row-->
                                                <!-- deleted row-->

                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-3 labelText1 padding0">
                                                            <asp:Label ID="LabelDefaultLocation" runat="server" Text="Default Location"></asp:Label>
                                                        </div>

                                                        <div class="col-sm-2 labelText1 padding0">
                                                            <asp:TextBox ID="txtDefLoc" runat="server" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtDefLoc_TextChanged"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1" style="padding-left: 3px; padding-right: 0px;">
                                                            <asp:LinkButton ID="lbtnSeDefLocation" runat="server" Visible="true" OnClick="lbtnSeDefLocation_Click">
                                                                                <span class="glyphicon glyphicon-search"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                        <!-- newly added-->
                                                        <div class="col-sm-2 labelText1 padding0">
                                                            <asp:Label ID="LabelDelivary" runat="server" Text="Delivery Criteria"></asp:Label>
                                                        </div>

                                                        <div class="col-sm-2 labelText1 paddingLeft5" style="width: 147px">
                                                            <asp:DropDownList ID="cmbDel" runat="server" CssClass="form-control">
                                                                <asp:ListItem Value="0">--SELECT--</asp:ListItem>
                                                                <asp:ListItem Value="DO with Serial">DO WITH SERIAL</asp:ListItem>
                                                                <asp:ListItem Value="DO without Serial">DO WITHOUT SERIAL</asp:ListItem>
                                                                <asp:ListItem Value="DO Later">DO LATER</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <!-- newly added-->




                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-12">
                                                    </div>
                                                </div>

                                                <div class="row">

                                                    <div class="col-sm-12">
                                                        <div class="col-sm-3 labelText1 padding0">
                                                            <asp:Label ID="LabelExtended" runat="server" Text="Extended War. Period"></asp:Label>

                                                        </div>

                                                        <div class="col-sm-2 labelText1 padding0">
                                                            <asp:TextBox ID="txtExtWar" runat="server" CssClass="txtExtWar form-control" Text="0" onkeydown="return jsStrings(event);" AutoPostBack="true" onpaste="return false" MaxLength="2"></asp:TextBox>

                                                        </div>

                                                        <div class="col-sm-1">
                                                        </div>
                                                        <!-- -->
                                                        <div class="col-sm-3 padding0">
                                                            <div class="col-sm-1 padding0" style="margin-top: 3px; padding-right: 18px;">
                                                                <asp:CheckBox ID="chkMultiDept" runat="server" AutoPostBack="true" Text=""
                                                                    CssClass="" OnCheckedChanged="chkMultiDept_CheckedChanged" />
                                                            </div>
                                                            <div class="col-sm-8 padding0 labelText1">
                                                                <asp:Label runat="server" Text="Mult. Department" />
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-2 padding0 labelText1" style="width: 85px">
                                                            <asp:TextBox ID="txtDefDept" AutoPostBack="true" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <!-- -->
                                                        <div class="col-sm-1 labelText1" style="padding-left: 2px; padding-right: 0px;">
                                                            <asp:LinkButton ID="lbtn_Srch_dep" runat="server" Visible="true" OnClick="lbtn_Srch_dep_Click">
                                                                                <span class="glyphicon glyphicon-search"></span>
                                                            </asp:LinkButton>
                                                        </div>

                                                    </div>

                                                </div>

                                                <div class="row" style="margin-top: 5px;">
                                                    <div class="col-sm-3 paddingRight0">
                                                        <div class="col-sm-1" style="padding-left: 0px; padding-right: 15px;">
                                                            <asp:CheckBox ID="chkSMS" runat="server" Text="" CssClass="" />
                                                        </div>
                                                        <div class="col-sm-10" style="padding-left: 3px; padding-right: 0px;">
                                                            <asp:Label Text="Sales Order SMS" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                    </div>
                                                    <div class="col-sm-4 paddingLeft0">
                                                        <div class="col-sm-1 padding0 labelText1" style="padding-left: 0px; padding-right: 18px;">
                                                            <asp:CheckBox ID="chkInterCom" runat="server" Text="" CssClass="" />
                                                        </div>
                                                        <div class="col-sm-8 padding0 labelText1">
                                                            <asp:Label Text="Inter Company" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height45">
                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                    <div class="col-sm-7 paddingLeft0">
                                        <%-- Sales Parameters  --%>
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                <strong><b>Sales Parameters </b></strong>
                                            </div>
                                            <div class="panel-body paddingLeft5">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-4">
                                                            <div class="col-sm-1" style="padding-left: 0px; padding-right: 15px;">
                                                                <asp:CheckBox ID="chkPrintDisc" runat="server" />
                                                            </div>
                                                            <div class="col-sm-10 padding0">
                                                                <asp:Label Text="Print Discount" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4 ">
                                                            <div class="col-sm-1" style="margin-top: 3px; padding-left: 0px; padding-right: 15px;">
                                                                <asp:CheckBox ID="chkChkPay" runat="server" Text="" CssClass="" />
                                                            </div>
                                                            <div class="col-sm-10 padding0">
                                                                <asp:Label Text="Check Payment" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4 paddingRight0">
                                                            <div class="col-sm-1" style="margin-top: 3px; padding-left: 0px; padding-right: 15px;">
                                                                <asp:CheckBox ID="chkManDoc" runat="server" Text="" CssClass="" />
                                                            </div>
                                                            <div class="col-sm-10 padding0">
                                                                <asp:Label Text="Manual Doc. Maintain" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-4">
                                                            <div class="col-sm-1" style="margin-top: 3px; padding-left: 0px; padding-right: 15px;">
                                                                <asp:CheckBox ID="chkPrintPay" runat="server" Text="" CssClass="" />
                                                            </div>
                                                            <div class="col-sm-10 padding0">
                                                                <asp:Label Text="Print Payement" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <div class="col-sm-1" style="margin-top: 3px; padding-left: 0px; padding-right: 15px;">
                                                                <asp:CheckBox ID="chkManCash" runat="server" Text="" CssClass="" />
                                                            </div>
                                                            <div class="col-sm-10 padding0" style="padding-right: 0px;">
                                                                <asp:Label Text="Check Manual Cash Memo" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <div class="col-sm-1" style="margin-top: 3px; padding-left: 0px; padding-right: 15px;">
                                                                <asp:CheckBox ID="chkHPRec" runat="server" Text="" CssClass="" />
                                                            </div>
                                                            <div class="col-sm-10 padding0">
                                                                <asp:Label Text="HP Receipt" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-4">
                                                            <div class="col-sm-1" style="margin-top: 3px; padding-left: 0px; padding-right: 15px;">
                                                                <asp:CheckBox ID="chkAllowPrice" runat="server" Text="" CssClass="" />
                                                            </div>
                                                            <div class="col-sm-10 padding0">
                                                                <asp:Label Text="Allow Enter Price Manual" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <div class="col-sm-1" style="margin-top: 3px; padding-left: 0px; padding-right: 15px;">
                                                                <asp:CheckBox ID="chkOrdRest" runat="server" Text="" CssClass="" />
                                                            </div>
                                                            <div class="col-sm-10 padding0">
                                                                <asp:Label Text="Order Restrict" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <div class="col-sm-1" style="margin-top: 3px; padding-left: 0px; padding-right: 15px;">
                                                                <asp:CheckBox ID="chk_edit_price" runat="server" Text="" CssClass="" Visible="false" />
                                                                <asp:CheckBox ID="chkPrintWarRem" runat="server" Text="" CssClass="" />
                                                            </div>
                                                            <div class="col-sm-10 padding0">
                                                                <asp:Label Text="Edit Price" runat="server" Visible="false" />
                                                                <asp:Label Text="Print Warranty Remarks" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-4">
                                                            <div class="col-sm-1" style="margin-top: 3px; padding-left: 0px; padding-right: 15px;">
                                                                <asp:CheckBox ID="chkCredBal" runat="server" Text="" CssClass="" />
                                                            </div>
                                                            <div class="col-sm-10 padding0">
                                                                <asp:Label Text="Check Credit Balance" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <div class="col-sm-1">
                                                            </div>
                                                            <div class="col-sm-11 ">
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <div class="col-sm-1">
                                                            </div>
                                                            <div class="col-sm-10">
                                                                <asp:Label Text="" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">

                                                    <div class="col-sm-6 paddingLeft5 paddingRight0">
                                                        <div class="col-sm-4 paddingRight0 labelText1">
                                                            <asp:Label ID="lblDefPriceBook" runat="server" Text="Default Price Book"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-4 paddingLeft5 labelText1">
                                                            <asp:TextBox ID="txtPB" AutoPostBack="true" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 labelText1" style="padding-left: 3px; padding-right: 0px;">
                                                            <asp:LinkButton ID="lbtn_Srch_PB" runat="server" OnClick="lbtn_Srch_PB_Click">
                                                                            <span class="glyphicon glyphicon-search"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-6 paddingLeft5 paddingRight5">
                                                        <div class="col-sm-4 paddingRight0 labelText1">
                                                            <asp:Label ID="Label4" runat="server" Text="Def. Customer"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-4 paddingLeft5 labelText1">
                                                            <asp:TextBox ID="txtDefCustomer" AutoPostBack="true" OnTextChanged="txtDefCustomer_TextChanged" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 padding0 labelText1" style="padding-left: 3px; padding-right: 0px;">
                                                            <asp:LinkButton ID="lbtnSeCust" runat="server" OnClick="lbtnSeCust_Click">
                                                                            <span class="glyphicon glyphicon-search"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6 paddingLeft5 paddingRight0">
                                                        <div class="col-sm-4 paddingRight0 labelText1">
                                                            <asp:Label ID="LabelOrdervalid" runat="server" Text="Order Valid Period"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-4 paddingLeft5 labelText1">
                                                            <asp:TextBox ID="txtValidPrd" runat="server" CssClass="txtValidPrd form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6 paddingLeft5 paddingRight5">
                                                        <div class="col-sm-4 paddingRight0 labelText1">
                                                            <asp:Label ID="lblExchangeRate" runat="server" Text="Def. Currency"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-4 paddingLeft5 labelText1">
                                                            <asp:TextBox ID="txtDefExRt" runat="server" OnTextChanged="txtDefExRt_TextChanged" AutoPostBack="true" Style="text-transform: uppercase" Enabled="true" CausesValidation="false" CssClass="form-control" onkeydown="return jsStrings(event);" MaxLength="10"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 labelText1" style="padding-left: 3px; padding-right: 0px;">
                                                            <asp:LinkButton ID="lbtnSeToCurr" CausesValidation="false" runat="server" OnClick="lbtnSeToCurr_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-4">
                                                            <div class="col-sm-1">
                                                            </div>
                                                            <div class="col-sm-10">
                                                                <asp:Label Text="" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <div class="col-sm-1">
                                                            </div>
                                                            <div class="col-sm-10">
                                                                <asp:Label Text="" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <div class="col-sm-1">
                                                            </div>
                                                            <div class="col-sm-10">
                                                                <asp:Label Text="" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-4">
                                                            <div class="col-sm-1">
                                                            </div>
                                                            <div class="col-sm-10">
                                                                <asp:Label Text="" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <div class="col-sm-1">
                                                            </div>
                                                            <div class="col-sm-10">
                                                                <asp:Label Text="" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <div class="col-sm-1">
                                                            </div>
                                                            <div class="col-sm-10">
                                                                <asp:Label Text="" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                        </div>
                                        <div class="col-sm-6">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <!-- -->
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div runat="server" style="width: 427px">
                <asp:Button ID="ButtonBL" runat="server" Text="" Style="display: none;" />
            </div>
            <asp:ModalPopupExtender ID="UserBL" runat="server" Enabled="True" TargetControlID="ButtonBL"
                PopupControlID="testPanelBL" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- hiddn form-->
    <div>
        <asp:Panel runat="server" ID="testPanelBL" DefaultButton="ButtonBL">
            <div runat="server" id="Div1" class="panel panel-primary Mheight">

                <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>


                <div class="panel panel-default">
                    <div class="panel-heading">
                        <asp:LinkButton ID="LinkButton3" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                        </asp:LinkButton>
                        <%--<span>Commen Search</span>--%>
                        <div class="col-sm-11">
                        </div>
                        <div class="col-sm-1">
                        </div>
                    </div>


                    <div class="panel-body">
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

                                <div class="col-sm-3 paddingRight5">
                                    <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="DropDownList1" runat="server" class="form-control">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="col-sm-2 labelText1">
                                    Search by word 
                                </div>

                                <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel runat="server" DefaultButton="LinkButtonBLNOS">
                                            <%--onkeydown="return (event.keyCode!=13);"--%>
                                            <div class="col-sm-4 paddingRight5">
                                                <asp:TextBox ID="TextBox1" placeholder="Search by word" CausesValidation="false" class="form-control"
                                                    runat="server" Visible="true"></asp:TextBox>
                                            </div>

                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="LinkButtonBLNOS" runat="server" Visible="true" OnClick="LinkButtonBLNOS_Click">
                                     <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                </asp:LinkButton>
                                            </div>
                                        </asp:Panel>
                                    </ContentTemplate>


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

                                <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>

                                        <asp:GridView ID="BLLoad" CausesValidation="false" runat="server" AllowPaging="True" GridLines="None"
                                            CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager"
                                            OnPageIndexChanging="BLLoad_PageIndexChanging" OnSelectedIndexChanged="BLLoad_SelectedIndexChanged">
                                            <Columns>
                                                <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10">
                                                    <ItemStyle Width="10px" />
                                                </asp:ButtonField>
                                            </Columns>
                                            <HeaderStyle Width="10px" />
                                            <PagerStyle CssClass="cssPager" />
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                        </div>
                    </div>



                </div>
            </div>
        </asp:Panel>
    </div>

    <!--province -->
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div runat="server" style="width: 427px">
                <asp:Button ID="Button1" runat="server" Text="" Style="display: none;" />
            </div>
            <asp:ModalPopupExtender ID="ModalPopupExtenderProvince" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="Panel1" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- hiddn form-->
    <div>
        <asp:Panel runat="server" ID="Panel1" DefaultButton="Button1">
            <div runat="server" id="Div2" class="panel panel-primary Mheight">

                <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>


                <div class="panel panel-default">
                    <div class="panel-heading">
                        <asp:LinkButton ID="LinkButton4" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                        </asp:LinkButton>
                        <%--<span>Commen Search</span>--%>
                        <div class="col-sm-11">
                        </div>
                        <div class="col-sm-1">
                        </div>
                    </div>


                    <div class="panel-body">
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

                                <div class="col-sm-3 paddingRight5">
                                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="DropDownList2" runat="server" class="form-control">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="col-sm-2 labelText1">
                                    Search by word 
                                </div>
                                <asp:Panel runat="server" DefaultButton="LinkButton5">
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                            <%--onkeydown="return (event.keyCode!=13);"--%>
                                            <div class="col-sm-4 paddingRight5">
                                                <asp:TextBox ID="TextBox2" placeholder="Search by word" CausesValidation="true" class="form-control" runat="server" Visible="true"></asp:TextBox>
                                            </div>

                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="LinkButton5" runat="server" Visible="true" OnClick="LinkButton5_Click">
                                     <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                </asp:LinkButton>
                                            </div>
                                        </ContentTemplate>

                                    </asp:UpdatePanel>
                                </asp:Panel>
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
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>

                                        <asp:GridView ID="GridView1" CausesValidation="false" runat="server" AllowPaging="True" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" OnPageIndexChanging="GridView1_PageIndexChanging" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                            <Columns>
                                                <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10">
                                                    <ItemStyle Width="10px" />
                                                </asp:ButtonField>

                                            </Columns>
                                            <HeaderStyle Width="10px" />
                                            <PagerStyle CssClass="cssPager" />
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>



                </div>
            </div>
        </asp:Panel>
    </div>

    <!--district -->
    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
        <ContentTemplate>
            <div runat="server" style="width: 427px">
                <asp:Button ID="Button2" runat="server" Text="" Style="display: none;" />
            </div>
            <asp:ModalPopupExtender ID="ModalPopupExtenderDistrict" runat="server" Enabled="True" TargetControlID="Button2"
                PopupControlID="Panel2" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- hiddn form-->
    <div>
        <asp:Panel runat="server" ID="Panel2" DefaultButton="Button2">
            <div runat="server" id="Div3" class="panel panel-primary Mheight">

                <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>


                <div class="panel panel-default">
                    <div class="panel-heading">
                        <asp:LinkButton ID="LinkButton6" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                        </asp:LinkButton>
                        <%--<span>Commen Search</span>--%>
                        <div class="col-sm-11">
                        </div>
                        <div class="col-sm-1">
                        </div>
                    </div>


                    <div class="panel-body">
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

                                <div class="col-sm-3 paddingRight5">
                                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="DropDownList3" runat="server" class="form-control">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="col-sm-2 labelText1">
                                    Search by word 
                                </div>
                                <asp:Panel runat="server" DefaultButton="LinkButton7">
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy5" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                        <ContentTemplate>
                                            <%--onkeydown="return (event.keyCode!=13);"--%>
                                            <div class="col-sm-4 paddingRight5">
                                                <asp:TextBox ID="TextBox3" placeholder="Search by word" CausesValidation="false" class="form-control"
                                                    runat="server" Visible="true"></asp:TextBox>
                                            </div>

                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="LinkButton7" runat="server" Visible="true" OnClick="LinkButton7_Click">
                                     <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                </asp:LinkButton>
                                            </div>
                                        </ContentTemplate>


                                    </asp:UpdatePanel>
                                </asp:Panel>
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

                                <asp:ScriptManagerProxy ID="ScriptManagerProxy6" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                    <ContentTemplate>

                                        <asp:GridView ID="GridView2" CausesValidation="false" runat="server" AllowPaging="True" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" OnPageIndexChanging="GridView2_PageIndexChanging" OnSelectedIndexChanged="GridView2_SelectedIndexChanged">
                                            <Columns>
                                                <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10">
                                                    <ItemStyle Width="10px" />
                                                </asp:ButtonField>

                                            </Columns>
                                            <HeaderStyle Width="10px" />
                                            <PagerStyle CssClass="cssPager" />
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>



                </div>
            </div>
        </asp:Panel>
    </div>

    <asp:UpdatePanel runat="server" ID="update">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="ItemPopup" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="testPanel" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel runat="server" ID="testPanel" DefaultButton="lbtnSearch">
        <div runat="server" id="test" class="panel panel-primary Mheight">
            <asp:Label ID="Label5" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="btnClose" runat="server">
                             <span class="glyphicon glyphicon-remove" style="margin-left:560px" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                </div>
                <div class="panel-body">
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
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-3 paddingRight5">
                                        <asp:DropDownList ID="cmbSearchbykey" runat="server" class="form-control" AutoPostBack="false">
                                        </asp:DropDownList>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="col-sm-2 labelText1">
                                Search by word
                            </div>

                            <asp:Label ID="lblSearchType" runat="server" Text="" Visible="False"></asp:Label>
                            <div class="col-sm-4 paddingRight5">
                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtSearchbyword" CausesValidation="false" class="form-control" AutoPostBack="false" runat="server"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy9" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-1 paddingLeft0">
                                        <asp:LinkButton ID="lbtnSearch" runat="server" OnClick="lbtnSearch_Click">
                                     <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
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
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="dgvResultItem" CausesValidation="false" runat="server" AllowPaging="True" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager"
                                        OnPageIndexChanging="dgvResultItem_PageIndexChanging"
                                        OnSelectedIndexChanged="dgvResultItem_SelectedIndexChanged">
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
    </asp:Panel>

    <script>
        Sys.Application.add_load(fun);
        function fun() {
            if (typeof jQuery == 'undefined') {
                alert('jQuery is not loaded');
            }
            $('.txtEmail').focusout(function () {
                var str = $(this).val();
                var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
                if (!emailReg.test(str)) {
                    showStickyWarningToast('Please enter a valid email address !!!');
                    $(this).val('');
                }
            });

            $('.txtPhone, .txtFax').keypress(function (evt) {
                evt = (evt) ? evt : window.event;
                //var charCode = (evt.which) ? evt.which : evt.keyCode;
                var charCode = evt.which;
                var str = jQuery(this).val();
                //alert(charCode);
                if ((charCode == 8) || (charCode == 9) || (charCode == 0)) {
                    return true;
                }
                else if (str.length < 15) {
                    if ((charCode < 58 && charCode > 47)) {
                        return true;
                    }
                    if ((charCode == 43)) {
                        // var no = str.value;
                        var result = "+" + str;
                        //console.log(result);
                        //  alert(result);
                        if (str.charAt(0) != "+") {
                            $(this).val(result)
                            $(this).value = result;
                            return false;
                        }
                        else {
                            return false;
                        }
                    }
                    else {
                        return false;
                    }
                }
                else {
                    $(this).value = str.substr(0, 15);
                    alert('Maximum 15 characters are allowed ');
                    return false;
                }
            });

            $('.txtSquareFeet,.txtstaff,.txtMaxFwdSale,.txtAddHours,.txtValidPrd').keypress(function (evt) {
                // var ch = (evt.which) ? evt.which : evt.keyCode;
                var charCode = evt.which;
                var str = $(this).val();
                //console.log(charCode);
                if ((charCode == 8) || (charCode == 9) || (charCode == 0)) {
                    return true;
                }
                else if (str.length < 5) {
                    if (charCode > 47 && charCode < 58) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else {
                    $(this).value = str.substr(0, 5);
                    alert('Maximum 5 characters are allowed ');
                    return false;
                }
            });

            $('.txtExtWar').keypress(function (evt) {
                // var ch = (evt.which) ? evt.which : evt.keyCode;
                var ch = evt.which;
                var str = $(this).val();
                console.log(ch);
                if (ch == 46) {
                    if (str.indexOf(".") == -1) {
                        return true;
                    } else {
                        return false;
                    }
                }
                else if ((ch == 8) || (ch == 9) || (ch == 46) || (ch == 0)) {
                    return true;
                }
                else if (ch > 47 && ch < 58) {
                    return true;
                }
                else {
                    return false;
                }
            })
            $('#BodyContent_txtPC,#BodyContent_txtProvince,#BodyContent_txtDistrict,#BodyContent_txtTown,#BodyContent_txtMainPC,#BodyContent_txtOthRef,#BodyContent_txtPhone,#BodyContent_txtFax,#BodyContent_txtEpf,#BodyContent_txtManName,#BodyContent_txtSquareFeet').on('keypress', function (event) {
                var regex = new RegExp("^[`~!@#$%^&*()=+]+$");
                var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
                if (regex.test(key)) {
                    event.preventDefault();
                    return false;
                }
            });

            $('#BodyContent_txtDesc,#BodyContent_txtAdd1,#BodyContent_txtAdd2').on('keypress', function (event) {
                var regex = new RegExp("^[`~!@#$%^&*=+]+$");
                var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
                if (regex.test(key)) {
                    event.preventDefault();
                    return false;
                }
            });
        }
    </script>

</asp:Content>
