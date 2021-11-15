<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="Imports_Rep.aspx.cs" Inherits="FastForward.SCMWeb.View.Reports.Imports.Imports_Rep" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <link href="<%=Request.ApplicationPath%>Css/jquery-ui.theme.min.css" rel="stylesheet" />
    <link href="<%=Request.ApplicationPath%>Css/jquery-ui.min.css" rel="stylesheet" />


    <script type="text/javascript">

        function DateValid(sender, args) {
            var fromDate = Date.parse(document.getElementById('<%=txtFromDate.ClientID%>').value);
            var toDate = Date.parse(document.getElementById('<%=txtToDate.ClientID%>').value);
            if (toDate < fromDate) {
                document.getElementById('<%=txtToDate.ClientID%>').value = document.getElementById('<%=hdfCurrentDate.ClientID%>').value;
                showStickyWarningToast('Please select a valid date range !');
            }
        }

        function ClearConfirm() {
            var selectedvalue = confirm("Do you want to clear data?");
            if (selectedvalue) {
                document.getElementById('<%=hdfClearData.ClientID %>').value = "1";
            } else {
                document.getElementById('<%=hdfClearData.ClientID %>').value = "0";
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
                sticky: false,
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
                sticky: false,
                position: 'top-center',
                type: 'error',
                closeText: '',
                close: function () {
                    console.log("toast is closed ...");
                }

            });
        }
        function SetTarget() {
            document.forms[0].target = "_blank";
        }
    </script>

    <script type="text/javascript">
        function closeDialog() {
            $(this).showStickySuccessToast("close");
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
        function showStickyNoticeToast(value) {
            $().toastmessage('showToast', {
                text: value,
                sticky: true,
                position: 'top-left',
                type: 'notice',
                closeText: '',
                close: function () { console.log("toast is closed ..."); }
            });
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
    </style>

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

        .exceldown {
            text-align: center;
            position: center;
            margin-left: 600px;
        }

        .marginleft {
            margin-left: 4px;
        }
    </style>

    <script>
        function pageLoad(sender, args) {
            jQuery(".txtFromDate").datepicker({ dateFormat: "d/M/yy", timeFormat: "hh:mm tt" });
            jQuery(".txtToDate").datepicker({ dateFormat: "d/M/yy", timeFormat: "hh:mm tt" });
            jQuery(".txtAsAt").datepicker({ dateFormat: "d/M/yy", timeFormat: "hh:mm tt" });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function SetTarget() {
            document.forms[0].target = "_blank";
        }
    </script>
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel1">

        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>

    </asp:UpdateProgress>
    <div class="col-sm-12">
        <div class="row">
            <div class="col-md-6">
                <asp:LinkButton ID="btnDownloadfile" Text="Download" OnClick="btnDownloadfile_Click" runat="server" CssClass="exceldown">
                                                                    Download
                </asp:LinkButton>
            </div>
            <div class="col-md-6">
                <asp:RadioButton ID="rbpdf" Text="PDF" Checked="true" CssClass="marginleft" runat="server" GroupName="importreports"></asp:RadioButton>
                <asp:RadioButton ID="rbexel" Text="Excel" runat="server" CssClass="marginleft" GroupName="importreports"></asp:RadioButton>
                <asp:RadioButton ID="rbexeldata" Text="Excel Data Only" CssClass="marginleft" runat="server" GroupName="importreports"></asp:RadioButton>
            </div>

        </div>

    </div>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <div class="col-sm-12 col-md-12" style="margin-left: 3px;">
                <div class="row">
                    <asp:HiddenField ID="hdfClearData" runat="server" />
                    <asp:HiddenField ID="hdfShowPopUp" Value="0" runat="server" />
                    <asp:HiddenField ID="hdfCurrentDate" Value="0" runat="server" />
                    <asp:Panel runat="server" DefaultButton="lbtnView">
                        <div class="col-sm-12 col-md-12 panel panel-default">
                            <div class="row buttonRow" id="HederBtn">
                                <div class="col-sm-12 col-md-12">
                                    <div class="col-md-5">
                                        <h1 style="font-size: small; margin-top: 2px">Imports Report</h1>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:CheckBox ID="chkautomail" runat="server" Enabled="true" />
                                        Auto Email
                                    </div>
                                    <div class="col-md-1 paddingRight0">
                                        <asp:LinkButton ID="lbtnView" CausesValidation="false" runat="server" OnClientClick="SetTarget();" CssClass="floatRight" OnClick="lbtnView_Click"> 
                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>Display </asp:LinkButton>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="lbtnClear" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="return ClearConfirm()" OnClick="lbtnClear_Click"> 
                                <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 col-md-12 padding0">
                                    <div class="col-sm-2 col-md-2" style="padding-left: 3px; padding-right: 1.5px;">
                                        <div class="col-sm-12 col-md-12 panel panel-default padding0 height230 " style="height: 518px">
                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px;">
                                                        <asp:RadioButton ID="rad01" AutoPostBack="true" OnCheckedChanged="rad01_CheckedChanged" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label1" runat="server" Text="Total Imports"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton ID="rad02" AutoPostBack="true" OnCheckedChanged="rad02_CheckedChanged" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label2" runat="server" Text="Container Volume"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton ID="rad03" AutoPostBack="true" GroupName="grpReport" runat="server" OnCheckedChanged="rad03_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label4" runat="server" Text="List of LC - Bank Wise"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton ID="rad04" AutoPostBack="true" OnCheckedChanged="rad04_CheckedChanged" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label5" runat="server" Text="Order Status"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton ID="rad05" AutoPostBack="true" OnCheckedChanged="rad05_CheckedChanged" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label6" runat="server" Text="Shipment Schedule"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton ID="rad06" AutoPostBack="true" OnCheckedChanged="rad06_CheckedChanged" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label7" runat="server" Text="Shipment Status"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton ID="rad07" AutoPostBack="true" OnCheckedChanged="rad07_CheckedChanged" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label8" runat="server" Text="Import Cost Analysis "></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton ID="rad08" AutoPostBack="true" OnCheckedChanged="rad08_CheckedChanged" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label9" runat="server" Text="Cost Information Summary"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton ID="rad09" AutoPostBack="true" GroupName="grpReport" OnCheckedChanged="rad09_CheckedChanged" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 1px">
                                                        <asp:Label ID="Label10" runat="server" Text="Import Register With Container Deposit"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton ID="rad10" AutoPostBack="true" OnCheckedChanged="rad10_CheckedChanged" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label11" runat="server" Text="SLPA Register"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton ID="rad11" AutoPostBack="true" GroupName="grpReport" OnCheckedChanged="rad11_CheckedChanged" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label12" runat="server" Text="Costing Sheet"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton ID="rad12" AutoPostBack="true" GroupName="grpReport" OnCheckedChanged="rad12_CheckedChanged" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label13" runat="server" Text="Import Schedule"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton ID="rad13" AutoPostBack="true" GroupName="grpReport" OnCheckedChanged="rad13_CheckedChanged" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label14" runat="server" Text="CusDec Entry Request Details"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton ID="rad14" AutoPostBack="true" GroupName="grpReport" OnCheckedChanged="rad14_CheckedChanged" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label15" runat="server" Text="Profitability Report"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton ID="rad15" AutoPostBack="true" GroupName="grpReport" OnCheckedChanged="rad15_CheckedChanged" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label16" runat="server" Text="Entry Detail Report"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton ID="rad16" AutoPostBack="true" GroupName="grpReport" OnCheckedChanged="rad16_CheckedChanged" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label17" runat="server" Text="Import Schdule New"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton ID="rad17" AutoPostBack="true" GroupName="grpReport" OnCheckedChanged="rad17_CheckedChanged" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label18" runat="server" Text="Financial Document Details"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton ID="rad18" AutoPostBack="true" GroupName="grpReport" OnCheckedChanged="rad18_CheckedChanged" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label19" runat="server" Text="Duty Free Transfer Note"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton ID="rad19" AutoPostBack="true" GroupName="grpReport" OnCheckedChanged="rad19_CheckedChanged" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label20" runat="server" Text="Custom and Assessment Detail Report"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-sm-10 col-md-10 " style="padding-left: 1.5px; padding-right: 3px;">
                                        <div class="col-sm-12 col-md-12 ">
                                            <div class="row" id="ParameterDetails">
                                                <div class="col-sm-12 col-md-12 ">
                                                    <div class="row">
                                                        <div class="col-sm-12 col-md-12 panel panel-default padding0" style="height: 518px;">
                                                            <div class="panel-heading padding0">
                                                                <strong><b>Parameter Details</b></strong>
                                                            </div>
                                                            <div class="panel-body" style="padding-left: 1.5px; padding-right: 1.5px;">
                                                                <div class="row">
                                                                    <div class="col-sm-12 col-md-12">
                                                                        <div class="col-sm-4 col-md-4" style="padding-left: 1px; padding-right: 1.5px;">

                                                                            <div class="panel panel-default">
                                                                                <div class="panel panel-heading" style="height: 27px;">

                                                                                    <div class="row">
                                                                                        <div class="col-md-12 " style="padding-left: 1.5px;">
                                                                                            <div class="col-md-8">
                                                                                                <b>Company</b>
                                                                                            </div>
                                                                                            <div class="col-md-1 height22" style="padding-left: 18px; padding-right: 3px;">
                                                                                                <asp:UpdatePanel runat="server" ID="UpdatePanel4">
                                                                                                    <ContentTemplate>
                                                                                                        <asp:CheckBox Checked="false" runat="server" ID="chkAllCompany" AutoPostBack="True" OnCheckedChanged="chkAllCompany_CheckedChanged"></asp:CheckBox>
                                                                                                    </ContentTemplate>
                                                                                                </asp:UpdatePanel>
                                                                                            </div>
                                                                                            <div class="col-md-2 labelText1">
                                                                                                <asp:Label runat="server" Text="Multiple" />
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="panel panel-body" style="height: 125px">
                                                                                    <div class="row">
                                                                                        <div class="col-md-12">
                                                                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                                                                                            <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                                                                                                <ContentTemplate>
                                                                                                    <div class="" style="max-height: 115px; overflow: auto;">
                                                                                                        <asp:GridView ID="dgvCompany" CssClass="table table-hover table-striped" runat="server" GridLines="None" PagerStyle-CssClass="cssPager"
                                                                                                            EmptyDataText="No data found..." AutoGenerateColumns="False" AllowPaging="false" PageSize="3" OnPageIndexChanging="dgvCompany_PageIndexChanging">
                                                                                                            <EditRowStyle BackColor="MidnightBlue" />
                                                                                                            <EmptyDataTemplate>
                                                                                                                <table class="table table-hover table-striped" border="1" style="border-collapse: collapse;">
                                                                                                                    <tbody>
                                                                                                                        <tr>
                                                                                                                            <th scope="col">Code</th>
                                                                                                                            <th scope="col">Description</th>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td>No records found.
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                        </tr>
                                                                                                                </table>
                                                                                                            </EmptyDataTemplate>
                                                                                                            <Columns>
                                                                                                                <asp:TemplateField HeaderText="">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:CheckBox ID="chkCompanyCode" runat="server" AutoPostBack="True" OnCheckedChanged="chkCompanyCode_CheckedChanged" />
                                                                                                                    </ItemTemplate>
                                                                                                                    <HeaderStyle Width="10px" />
                                                                                                                    <ItemStyle Width="10px" />
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="Code">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lblCode" runat="server" Text='<%# Bind("SEC_COM_CD") %>' Width="50px"></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:BoundField DataField="MasterComp.mc_desc" HeaderText="Description">
                                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                                                </asp:BoundField>
                                                                                                            </Columns>
                                                                                                            <PagerStyle CssClass="cssPager"></PagerStyle>
                                                                                                        </asp:GridView>
                                                                                                    </div>
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </div>
                                                                                    </div>

                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4 col-md-4" style="padding-left: 1.5px; padding-right: 1.5px;">

                                                                            <div class="panel panel-default">
                                                                                <div class="panel panel-heading" style="height: 27px;">
                                                                                    <div class="row">
                                                                                        <div class="col-md-12 " style="padding-left: 1.5px;">
                                                                                            <div class="col-md-8">
                                                                                                <b>Admin Team</b>
                                                                                            </div>
                                                                                            <div class="col-md-1 height22" style="padding-left: 18px; padding-right: 3px;">
                                                                                                <asp:UpdatePanel runat="server" ID="UpdatePanel5">
                                                                                                    <ContentTemplate>
                                                                                                        <asp:CheckBox runat="server" ID="chkAllAdmin" AutoPostBack="True" OnCheckedChanged="chkAllAdmin_CheckedChanged"></asp:CheckBox>
                                                                                                    </ContentTemplate>
                                                                                                </asp:UpdatePanel>
                                                                                            </div>
                                                                                            <div class="col-md-2 labelText1">
                                                                                                <asp:Label runat="server" Text="Multiple" />
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="panel panel-body" style="height: 125px">
                                                                                    <div class="row">
                                                                                        <div class="col-md-12">
                                                                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                                                                                            <asp:UpdatePanel runat="server" ID="UpdatePanel6">
                                                                                                <ContentTemplate>
                                                                                                    <div class="panelscoll2" style="height: 120px">
                                                                                                        <asp:GridView ID="dgvAdminTeam" CssClass="table table-hover table-striped bound" runat="server" GridLines="None" PagerStyle-CssClass="cssPager"
                                                                                                            EmptyDataText="No data found..." AutoGenerateColumns="False" AllowPaging="false" PageSize="3">
                                                                                                            <EditRowStyle BackColor="MidnightBlue" />
                                                                                                            <EmptyDataTemplate>
                                                                                                                <table class="table table-hover table-striped" border="0" style="border-collapse: collapse;">
                                                                                                                    <tbody>
                                                                                                                        <tr>
                                                                                                                            <th scope="col">Code</th>
                                                                                                                            <th scope="col">Description</th>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td>No records found.
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                        </tr>
                                                                                                                </table>
                                                                                                            </EmptyDataTemplate>
                                                                                                            <Columns>
                                                                                                                <asp:TemplateField HeaderText="">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:CheckBox ID="chkAdminTeam" runat="server" AutoPostBack="True" OnCheckedChanged="chkAdminTeam_CheckedChanged" />
                                                                                                                    </ItemTemplate>
                                                                                                                    <HeaderStyle Width="10px" />
                                                                                                                    <ItemStyle Width="10px" />
                                                                                                                </asp:TemplateField>

                                                                                                                <asp:BoundField DataField="mso_com_cd" HeaderText="Com Code">
                                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                                                </asp:BoundField>
                                                                                                                <%--<asp:BoundField DataField="mso_cd" HeaderText="Admin Code">
                                                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                                                        </asp:BoundField>--%>
                                                                                                                <asp:TemplateField HeaderText="Admin Code">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lblAdminCode" runat="server" Text='<%# Bind("mso_cd") %>' Width="80px"></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                            </Columns>
                                                                                                            <PagerStyle CssClass="cssPager"></PagerStyle>
                                                                                                        </asp:GridView>
                                                                                                    </div>
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </div>
                                                                                    </div>

                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4 col-md-4" style="padding-left: 1.5px; padding-right: 0px;">
                                                                            <div class="panel panel-default padding0">
                                                                                <div class="panel panel-heading padding0">
                                                                                    <strong><b>Date Criteria</b></strong>
                                                                                </div>
                                                                                <div class="panel-body">
                                                                                    <div style="height: 127px;">
                                                                                        <div class="row">
                                                                                            <div class="col-sm-12 col-md-12">
                                                                                                <div class="col-sm-2 col-md-2 labelText1 padding0">
                                                                                                    <asp:Label runat="server">Year</asp:Label>
                                                                                                </div>
                                                                                                <div class="col-sm-3 col-md-3 padding0">
                                                                                                    <asp:DropDownList runat="server" ID="ddlYear" AutoPostBack="true"
                                                                                                        OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                                                                                                        CssClass="form-control" AppendDataBoundItems="True">
                                                                                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                </div>
                                                                                                <div class="col-sm-1 col-md-1 labelText1 padding0">
                                                                                                </div>
                                                                                                <div class="col-sm-2 col-md-2 labelText1 padding0">
                                                                                                    <asp:Label runat="server">Month</asp:Label>
                                                                                                </div>
                                                                                                <div class="col-sm-4 col-md-4 padding0">
                                                                                                    <asp:DropDownList runat="server" ID="ddlMonth" AutoPostBack="true"
                                                                                                        OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" CssClass="form-control">
                                                                                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                                                        <asp:ListItem Value="1">January</asp:ListItem>
                                                                                                        <asp:ListItem Value="2">February</asp:ListItem>
                                                                                                        <asp:ListItem Value="3">March</asp:ListItem>
                                                                                                        <asp:ListItem Value="4">April</asp:ListItem>
                                                                                                        <asp:ListItem Value="5">May</asp:ListItem>
                                                                                                        <asp:ListItem Value="6">June</asp:ListItem>
                                                                                                        <asp:ListItem Value="7">July</asp:ListItem>
                                                                                                        <asp:ListItem Value="8">August</asp:ListItem>
                                                                                                        <asp:ListItem Value="9">September</asp:ListItem>
                                                                                                        <asp:ListItem Value="10">October</asp:ListItem>
                                                                                                        <asp:ListItem Value="11">November</asp:ListItem>
                                                                                                        <asp:ListItem Value="12">December</asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row " style="padding-top: 10px; padding-bottom: 0px;">                                                                                            
                                                                                            <div class="form-group input-group lab">
                                                                                                <div class="col-sm-12 col-md-12 padding0">
                                                                                                    <div class="col-sm-1 col-md-1">
                                                                                                        <asp:RadioButton runat="server" ID="radioEtaDate" GroupName="dateType"></asp:RadioButton>
                                                                                                    </div>
                                                                                                    <div class="col-sm-2 col-md-2 labelText1 paddingLeft0">
                                                                                                        <asp:Label runat="server">ETA Date</asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-sm-1 col-md-1">
                                                                                                        <asp:RadioButton runat="server" ID="radioEtdDate" GroupName="dateType"></asp:RadioButton>
                                                                                                    </div>
                                                                                                    <div class="col-sm-3 col-md-3 labelText1 paddingLeft0">
                                                                                                        <asp:Label runat="server">ETD Date</asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-sm-1 col-md-1">
                                                                                                        <asp:RadioButton runat="server" ID="radioClearDate" GroupName="dateType"></asp:RadioButton>
                                                                                                    </div>
                                                                                                    <div class="col-sm-3 col-md-3 labelText1 paddingLeft0">
                                                                                                        <asp:Label runat="server">Clearance Date</asp:Label>
                                                                                                    </div>
                                                                                                </div>                                                                                                
                                                                                                <div class="col-sm-12 col-md-12 paddingtop5 paddingLeft0 paddingRight0">
                                                                                                    <div class="col-sm-1 col-md-1">
                                                                                                        <asp:RadioButton runat="server" ID="radioExpireDate" GroupName="dateType"></asp:RadioButton>
                                                                                                    </div>
                                                                                                    <div class="col-sm-2 col-md-2 labelText1 paddingLeft0">
                                                                                                        <asp:Label runat="server">Expire Date</asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-sm-1 col-md-1">
                                                                                                        <asp:RadioButton runat="server" ID="radioInsurancePolicyDate" GroupName="dateType"></asp:RadioButton>
                                                                                                    </div>
                                                                                                    <div class="col-sm-3 col-md-3 labelText1 paddingLeft0">
                                                                                                        <asp:Label runat="server">Insurance Policy Date</asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-sm-1 col-md-1">
                                                                                                        <asp:RadioButton runat="server" ID="radioFinanceDocDate" GroupName="dateType"></asp:RadioButton>
                                                                                                    </div>
                                                                                                    <div class="col-sm-3 col-md-3 labelText1 paddingLeft0">
                                                                                                        <asp:Label runat="server">Finance Doc Date</asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="col-sm-12 col-md-12">
                                                                                                <div class="col-sm-2 col-md-2 labelText1 padding0">
                                                                                                    <asp:Label runat="server">From Date</asp:Label>
                                                                                                </div>
                                                                                                <div class="col-sm-3 col-md-3 padding0" style="padding-left: 5px; padding-right: 2px;">
                                                                                                    <asp:TextBox runat="server" ID="txtFromDate" CssClass="txtFromDate form-control" />
                                                                                                </div>
                                                                                                <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                    <%--<asp:LinkButton ID="lbtnFromDate" CausesValidation="false" runat="server">
                                                                                    <span class="glyphicon glyphicon-calendar" aria-hidden="true"  ></span>
                                                                                                    </asp:LinkButton>
                                                                                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFromDate"
                                                                                                        PopupButtonID="lbtnFromDate" Format="dd/MMM/yyyy">
                                                                                                    </asp:CalendarExtender>--%>
                                                                                                </div>
                                                                                                <div class="col-sm-2 col-md-2 labelText1 padding0">
                                                                                                    <asp:Label runat="server">To Date</asp:Label>
                                                                                                </div>
                                                                                                <div class="col-sm-3 col-md-3" style="padding-left: 2px; padding-right: 2px;">
                                                                                                    <asp:TextBox runat="server" ID="txtToDate" CssClass="txtToDate form-control" OnTextChanged="txtToDate_TextChanged" />
                                                                                                </div>
                                                                                                <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                    <%--<asp:LinkButton ID="lbtnToDate" CausesValidation="false" runat="server">
                                                                                    <span class="glyphicon glyphicon-calendar" aria-hidden="true"  ></span>
                                                                                                    </asp:LinkButton>
                                                                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDate"
                                                                                                        OnClientDateSelectionChanged="DateValid"
                                                                                                        PopupButtonID="lbtnToDate" Format="dd/MMM/yyyy">
                                                                                                    </asp:CalendarExtender>--%>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row" style="padding-top: 10px;">
                                                                                            <div class="col-sm-12 col-md-12">
                                                                                                <div class="col-sm-3 col-md-2 labelText1 padding0">
                                                                                                    <asp:Label runat="server">As At Date</asp:Label>
                                                                                                </div>
                                                                                                <div class="col-sm-3 col-md-3 padding0" style="padding-left: 5px; padding-right: 2px;">
                                                                                                    <asp:TextBox runat="server" ID="txtAsAt" CssClass="txtAsAt form-control" />
                                                                                                </div>
                                                                                                <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                    <%--<asp:LinkButton ID="lbtnAsAt" CausesValidation="false" runat="server">
                                                                                    <span class="glyphicon glyphicon-calendar" aria-hidden="true"  ></span>
                                                                                                    </asp:LinkButton>
                                                                                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtAsAt"
                                                                                                        PopupButtonID="lbtnAsAt" Format="dd/MMM/yyyy">
                                                                                                    </asp:CalendarExtender>--%>
                                                                                                </div>
                                                                                                <div class="col-sm-5 col-md-5">
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12 col-md-12">
                                                                        <div class="col-sm-7 col-md-8" style="padding-left: 1.5px; padding-right: 1.5px;">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <div class="panel panel-default">
                                                                                        <div class="panel panel-heading padding0">
                                                                                            <b>Document Criteria</b>
                                                                                        </div>
                                                                                        <div class="panel panel-body">
                                                                                            <div class="col-sm-12 col-md-12 padding0">
                                                                                                <div class="row">
                                                                                                    <div class="col-md-6">
                                                                                                        <div class="col-sm-4 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Order Plan #</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-5 col-md-5 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtOrderPlanNo" CssClass="form-control" OnTextChanged="txtOrderPlanNo_TextChanged"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtnSeOrPlNo" CausesValidation="false" runat="server" OnClick="lbtnSeOrPlNo_Click">
                                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <asp:CheckBox runat="server" ID="chkAllOrPlNo" AutoPostBack="True" OnCheckedChanged="chkAllOrPlNo_CheckedChanged"></asp:CheckBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 padding0">
                                                                                                            <asp:Label runat="server" Text="All" />
                                                                                                        </div>

                                                                                                    </div>
                                                                                                    <div class="col-md-6">
                                                                                                        <div class="col-sm-4 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Bank</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-5 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtBankNo" CssClass="form-control" OnTextChanged="txtBankNo_TextChanged"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtnSeBankNo" CausesValidation="false" runat="server" OnClick="lbtnSeBankNo_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <asp:CheckBox runat="server" Checked="true" ID="chkAllBankNo" AutoPostBack="True" OnCheckedChanged="chkAllBankNo_CheckedChanged"></asp:CheckBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <asp:Label runat="server" Text="All" />
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-md-6">
                                                                                                        <div class="col-sm-3 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">PI #</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-5 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtPlNo" CssClass="form-control" OnTextChanged="txtPlNo_TextChanged"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtnSePlNo" CausesValidation="false" runat="server" OnClick="lbtnSePlNo_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <asp:CheckBox runat="server" Checked="true" ID="chkAllPLNo" AutoPostBack="True" OnCheckedChanged="chkAllPLNo_CheckedChanged"></asp:CheckBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <asp:Label runat="server" Text="All" />
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="col-md-6">
                                                                                                        <div class="col-sm-4 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Supplier</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-5 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtSupplier" CssClass="form-control" OnTextChanged="txtSupplier_TextChanged"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtnSeSupplier" CausesValidation="false" runat="server" OnClick="lbtnSeSupplier_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <asp:CheckBox runat="server" Checked="true" ID="chkAllSupplier" AutoPostBack="True" OnCheckedChanged="chkAllSupplier_CheckedChanged"></asp:CheckBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <asp:Label runat="server" Text="All" />
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-md-6">
                                                                                                        <div class="col-sm-3 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">LC #</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-5 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtLcNo" CssClass="form-control" OnTextChanged="txtLcNo_TextChanged"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtnSeLcNo" CausesValidation="false" runat="server" OnClick="lbtnSeLcNo_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <asp:CheckBox runat="server" Checked="true" ID="chkAllLcNo" AutoPostBack="True" OnCheckedChanged="chkAllLcNo_CheckedChanged"></asp:CheckBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <asp:Label runat="server" Text="All" />
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="col-md-6">
                                                                                                        <div class="col-sm-4 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Agent</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-5 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtAgent" CssClass="form-control" OnTextChanged="txtAgent_TextChanged"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtnSeAgent" CausesValidation="false" runat="server" OnClick="lbtnSeAgent_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <asp:CheckBox runat="server" Checked="true" ID="chkAllAgent" AutoPostBack="True" OnCheckedChanged="chkAllAgent_CheckedChanged"></asp:CheckBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <asp:Label runat="server" Text="All" />
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-md-6">
                                                                                                        <div class="col-sm-3 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">BL #</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-5 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtBlNo" CssClass="form-control" OnTextChanged="txtBlNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtnSeBlNo" CausesValidation="false" runat="server" OnClick="lbtnSeBlNo_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <asp:CheckBox runat="server" Checked="true" ID="chkAllBlNo" AutoPostBack="True" OnCheckedChanged="chkAllBlNo_CheckedChanged"></asp:CheckBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <asp:Label runat="server" Text="All" />
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="col-md-6">
                                                                                                        <div class="col-sm-4 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Country</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-5 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtCountry" CssClass="form-control" OnTextChanged="txtCountry_TextChanged"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtnSeCountry" CausesValidation="false" runat="server" OnClick="lbtnSeCountry_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <asp:CheckBox runat="server" Checked="true" ID="chkAllCountry" AutoPostBack="True" OnCheckedChanged="chkAllCountry_CheckedChanged"></asp:CheckBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <asp:Label runat="server" Text="All" />
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-md-6">
                                                                                                        <div class="col-sm-3 col-md-3 labelText1  padding0">
                                                                                                            <asp:Label runat="server">GRN #</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-5 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtGrnNo" CssClass="form-control" OnTextChanged="txtGrnNo_TextChanged"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtnSeGrnNo" CausesValidation="false" runat="server" OnClick="lbtnSeGrnNo_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <asp:CheckBox runat="server" Checked="true" ID="chkAllGrnNo" AutoPostBack="True" OnCheckedChanged="chkAllGrnNo_CheckedChanged"></asp:CheckBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <asp:Label runat="server" Text="All" />
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="col-md-6">
                                                                                                        <div class="col-sm-4 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Port</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-5 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtPort" CssClass="form-control" OnTextChanged="txtPort_TextChanged"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtnSePort" CausesValidation="false" runat="server" OnClick="lbtnSePort_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <asp:CheckBox runat="server" Checked="true" ID="chkAllPort" AutoPostBack="True" OnCheckedChanged="chkAllPort_CheckedChanged"></asp:CheckBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <asp:Label runat="server" Text="All" />
                                                                                                        </div>
                                                                                                    </div>



                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-md-6">
                                                                                                        <div class="col-sm-3 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Entry #</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-5 padding0">
                                                                                                            <asp:TextBox AutoPostBack="true" runat="server" ID="txtEntryNo" CssClass="form-control" OnTextChanged="txtEntryNo_TextChanged"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtnEntryNo" CausesValidation="false" runat="server" OnClick="lbtnEntryNo_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <asp:CheckBox runat="server" Checked="true" ID="chkAllEntryNo" AutoPostBack="True" OnCheckedChanged="chkAllEntryNo_CheckedChanged"></asp:CheckBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <asp:Label runat="server" Text="All" />
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <asp:LinkButton runat="server" ID="listentry" OnClick="listentry_Click">
                                                                                                                <span class="glyphicon glyphicon-arrow-down"></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="col-md-6">
                                                                                                        <div class="col-sm-4 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Variance</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-5 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtVariance" CssClass="form-control" OnTextChanged="txtPort_TextChanged"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <%-- <asp:LinkButton ID="LinkButton1" CausesValidation="false" runat="server" OnClick="lbtnSePort_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>--%>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <%--<asp:CheckBox runat="server" Checked="true" ID="CheckBox1" AutoPostBack="True" OnCheckedChanged="chkAllPort_CheckedChanged"></asp:CheckBox>--%>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <%--<asp:Label runat="server" Text="All" />--%>
                                                                                                        </div>
                                                                                                    </div>


                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-md-6">
                                                                                                        <div class="col-sm-3 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">To-bond #</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-5 padding0">
                                                                                                            <asp:TextBox AutoPostBack="true" runat="server" ID="txtToBondNo" CssClass="form-control" OnTextChanged="txtToBondNo_TextChanged"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtnSeToBond" CausesValidation="false" runat="server" OnClick="lbtnSeToBond_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>

                                                                                                        <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <asp:CheckBox runat="server" Checked="true" ID="chkAllTobond" AutoPostBack="True" OnCheckedChanged="chkAllTobond_CheckedChanged"></asp:CheckBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <asp:Label runat="server" Text="All" />
                                                                                                        </div>


                                                                                                        <div class="col-sm-4 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Buying Exrate  </asp:Label>
                                                                                                        </div>

                                                                                                        <div class="col-sm-3 col-md-5 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtBuyingExrate" CssClass="form-control" OnTextChanged="txtToBondNo_TextChanged"></asp:TextBox>
                                                                                                        </div>




                                                                                                    </div>
                                                                                                    <div class="col-md-6">
                                                                                                        <div class="col-sm-4 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Loading Place  </asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-5 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtloadingplace" CssClass="form-control" OnTextChanged="txtloadingplace_TextChanged"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbnloadingplace" CausesValidation="false" runat="server" OnClick="lbnloadingplace_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-md-6">
                                                                                                        <div class="col-sm-3 col-md-3 labelText1  padding0">
                                                                                                            <asp:Label runat="server">Request/Entry Type</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-5 padding0">
                                                                                                            <asp:DropDownList ID="ddlRequestType" runat="server" class="form-control">
                                                                                                            </asp:DropDownList>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <asp:CheckBox runat="server" Checked="true" ID="chlAllReqTp" AutoPostBack="True"></asp:CheckBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 padding0">
                                                                                                            <asp:Label runat="server" Text="All" />
                                                                                                        </div>

                                                                                                    </div>
                                                                                                    <div class="col-md-6">
                                                                                                        <div class="col-sm-3 col-md-3 labelText1  padding0">
                                                                                                            <asp:Label runat="server">Request No</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-5 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtReqNo" CssClass="form-control"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <asp:CheckBox runat="server" Checked="true" ID="chkUser" AutoPostBack="True"></asp:CheckBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-2 col-md-1 padding0">
                                                                                                            <asp:Label runat="server" Text="All User" />
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <asp:CheckBox runat="server" Checked="true" ID="chkViewAll" AutoPostBack="True"></asp:CheckBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-2 col-md-1 padding0">
                                                                                                            <asp:Label runat="server" Text="View All" />
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">

                                                                                                    <div class="col-md-6">
                                                                                                        <div class="col-sm-3 col-md-3 labelText1  padding0">
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-5 padding0">
                                                                                                            <asp:LinkButton ID="lbtnUpd" CausesValidation="false" runat="server" OnClientClick="SetTarget();" CssClass="floatRight" OnClick="lbtnUpd_Click"> 
                                                                                                                <span class="glyphicon glyphicon-save" aria-hidden="true"></span>Mark as Print </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <div class="col-md-6"></div>
                                                                                                    </div>
                                                                                                    <div class="row">
                                                                                                        <div class="col-md-6">
                                                                                                            <div class="col-sm-2 col-md-2 labelText1 padding0">
                                                                                                                <asp:Label runat="server">File# From  </asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-sm-3 col-md-3 padding0">

                                                                                                                <asp:TextBox runat="server" ID="txtfilenofrm" CssClass="form-control" OnTextChanged="txtfilenofrm_TextChanged"></asp:TextBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                                <asp:LinkButton ID="lbtnfilefrm" CausesValidation="false" runat="server" OnClick="lbtnfilefrm_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 col-md-2 labelText1 padding0">
                                                                                                                <asp:Label runat="server">File# To  </asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-sm-3 col-md-3 padding0">
                                                                                                                <asp:TextBox runat="server" ID="txtfilenoto" CssClass="form-control" OnTextChanged="txtfilenoto_TextChanged"></asp:TextBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                                <asp:LinkButton ID="lbtnfileto" CausesValidation="false" runat="server" OnClick="lbtnfileto_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">


                                                                                                    <div class="col-md-6">
                                                                                                        <div class="col-sm-3 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Price Book </asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-3 padding0">

                                                                                                            <asp:TextBox runat="server" ID="txtpricebook" CssClass="form-control" OnTextChanged="txtpricebook_TextChanged"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtnpricebook" CausesValidation="false" runat="server" OnClick="lbtnpricebook_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtaddpricebook" CausesValidation="false" runat="server" OnClick="lbtaddpricebook_Click">
                                                                                                    <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                    </div>

                                                                                                    <div class="col-md-6">
                                                                                                        <div class="col-sm-2 col-md-2 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Price Level  </asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-3 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtpricelevel" CssClass="form-control" OnTextChanged="txtpricelevel_TextChanged"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtnpricelevel" CausesValidation="false" runat="server" OnClick="lbtnpricelevel_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtnaddpricelvl" CausesValidation="false" runat="server" OnClick="lbtnaddpricelvl_Click">
                                                                                                    <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>

                                                                                                <div class="row">
                                                                                                    <div class="col-sm-6 col-md-6 padding0" style="height: 25px;">
                                                                                                        <div class="form-group input-group lab col-md-12">
                                                                                                            <div class="col-sm-1 col-md-1" style="margin-top: 3px;">
                                                                                                                <asp:RadioButton runat="server" ID="radPre" GroupName="dateType"></asp:RadioButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 col-md-2 labelText1 paddingLeft0">
                                                                                                                <asp:Label runat="server">Pre</asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1" style="padding-left: 10px; margin-top: 3px;">
                                                                                                                <asp:RadioButton runat="server" ID="radPost" GroupName="dateType"></asp:RadioButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 col-md-2 labelText1 paddingLeft0">
                                                                                                                <asp:Label runat="server">Post</asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1" style="margin-top: 3px;">
                                                                                                                <asp:RadioButton runat="server" ID="radAct" GroupName="dateType"></asp:RadioButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-4 col-md-2 labelText1 paddingLeft0">
                                                                                                                <asp:Label runat="server">Actual</asp:Label>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="col-sm-6 col-md-6" style="height: 25px;">
                                                                                                        <div class="form-group input-group lab col-md-12">
                                                                                                            <div class="col-sm-2 col-md-2 padding0">
                                                                                                                <asp:Label runat="server">Entry 01</asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-sm-3 col-md-3 padding0">
                                                                                                                <asp:TextBox runat="server" ID="EntryNumber01TextBox" CssClass="form-control" OnTextChanged="txtEntryNo01_TextChanged"></asp:TextBox>
                                                                                                            </div>
                                                                                                               <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="EntryNumber01Button" CausesValidation="false" runat="server" OnClick="lbtnEntry01No_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                            <div class="col-sm-2 col-md-2 padding0">
                                                                                                                <asp:Label runat="server">Entry 02</asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-sm-3 col-md-3 padding0">
                                                                                                                <asp:TextBox runat="server" ID="EntryNumber02TextBox" CssClass="form-control" OnTextChanged="txtEntryNo02_TextChanged"></asp:TextBox>
                                                                                                            </div>
                                                                                                               <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="EntryNumber02Button" CausesValidation="false" runat="server" OnClick="lbtnEntry02No_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>

                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                        </div>
                                                                        <div class="col-sm-4 col-md-4" style="padding-left: 1.5px; padding-right: 1.5px;">
                                                                            <div class="col-md-12">
                                                                                <div class="row">
                                                                                    <div class="col-md-12 padding0">
                                                                                        <div class="panel panel-default padding0">
                                                                                            <div class="panel panel-heading padding0">
                                                                                                <b>Item Criteria</b>
                                                                                            </div>
                                                                                            <div class="panel panel-body" style="height: 180px;">
                                                                                                <div class="row">
                                                                                                    <div class="col-sm-12 col-md-12 ">
                                                                                                        <div class="col-sm-3 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Item Cat. 1</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-3 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtCat1" CssClass="form-control" OnTextChanged="txtCat1_TextChanged"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtnSeCat1" CausesValidation="false" runat="server" OnClick="lbtnSeCat1_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <asp:CheckBox runat="server" ID="chkAllCat1" AutoPostBack="True" OnCheckedChanged="chkAllCat1_CheckedChanged"></asp:CheckBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <asp:Label runat="server" Text="All" />
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-2 paddingRight0">
                                                                                                            <asp:LinkButton ID="lbtnAddCat1" CausesValidation="false" runat="server" OnClick="lbtnAddCat1_Click">
                                                                                                    <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"  ></span>Add
                                                                                                            </asp:LinkButton>
                                                                                                        </div>

                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-sm-12 col-md-12">
                                                                                                        <div class="col-sm-3 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Item Cat. 2</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-3 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtCat2" CssClass="form-control" OnTextChanged="txtCat2_TextChanged"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtnSeCat2" CausesValidation="false" runat="server" OnClick="lbtnSeCat2_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <asp:CheckBox runat="server" ID="chkAllCat2" AutoPostBack="True" OnCheckedChanged="chkAllCat2_CheckedChanged"></asp:CheckBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <asp:Label runat="server" Text="All" />
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-2 paddingRight0">
                                                                                                            <asp:LinkButton ID="lbtnAddCat2" CausesValidation="false" runat="server" OnClick="lbtnAddCat2_Click">
                                                                                                    <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"  ></span>Add
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <%--<div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:Label runat="server" Text="Add" />
                                                                                                        </div>--%>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-sm-12 col-md-12">
                                                                                                        <div class="col-sm-3 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Item Cat. 3</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-3 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtCat3" CssClass="form-control" OnTextChanged="txtCat3_TextChanged"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtnSeCat3" CausesValidation="false" runat="server" OnClick="lbtnSeCat3_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <asp:CheckBox runat="server" ID="chkAllCat3" AutoPostBack="True" OnCheckedChanged="chkAllCat3_CheckedChanged"></asp:CheckBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <asp:Label runat="server" Text="All" />
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-2 paddingRight0">
                                                                                                            <asp:LinkButton ID="lbtnAddCat3" CausesValidation="false" runat="server" OnClick="lbtnAddCat3_Click">
                                                                                                    <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"  ></span>Add
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <%--<div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:Label runat="server" Text="Add" />
                                                                                                        </div>--%>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-sm-12 col-md-12">
                                                                                                        <div class="col-sm-3 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Item Cat. 4</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-3 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtCat4" CssClass="form-control" OnTextChanged="txtCat4_TextChanged"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtnSeCat4" CausesValidation="false" runat="server" OnClick="lbtnSeCat4_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <asp:CheckBox runat="server" ID="chkAllCat4" AutoPostBack="True" OnCheckedChanged="chkAllCat4_CheckedChanged"></asp:CheckBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <asp:Label runat="server" Text="All" />
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-2 paddingRight0">
                                                                                                            <asp:LinkButton ID="lbtnAddCat4" CausesValidation="false" runat="server" OnClick="lbtnAddCat4_Click">
                                                                                                    <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"  ></span>Add
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <%--<div class="col-sm-1 col-md-2" style="padding-left: 3px;">
                                                                                                            <asp:Label runat="server" Text="Add" />
                                                                                                        </div>--%>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-sm-12 col-md-12">
                                                                                                        <div class="col-sm-3 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Item Cat. 5</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-3 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtCat5" CssClass="form-control" OnTextChanged="txtCat5_TextChanged"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtnSeCat5" CausesValidation="false" runat="server" OnClick="lbtnSeCat5_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <asp:CheckBox runat="server" ID="chkAllCat5" AutoPostBack="True" OnCheckedChanged="chkAllCat5_CheckedChanged"></asp:CheckBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <asp:Label runat="server" Text="All" />
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-2 paddingRight0">
                                                                                                            <asp:LinkButton ID="lbtnAddCat5" CausesValidation="false" runat="server" OnClick="lbtnAddCat5_Click">
                                                                                                    <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"  ></span>Add
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <%--<div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:Label runat="server" Text="Add" />
                                                                                                        </div>--%>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-md-12 ">
                                                                                                        <div class="col-sm-3 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Item </asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-3 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtItemCode" CssClass="form-control" OnTextChanged="txtItemCode_TextChanged"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtnSeItemCode" CausesValidation="false" runat="server" OnClick="lbtnSeItemCode_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <asp:CheckBox runat="server" ID="chkAllItemCode" AutoPostBack="True" OnCheckedChanged="chkAllItemCode_CheckedChanged"></asp:CheckBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <asp:Label runat="server" Text="All" />
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-2 paddingRight0">
                                                                                                            <asp:LinkButton ID="lbtnAddItemCode" CausesValidation="false" runat="server" OnClick="lbtnAddItemCode_Click">
                                                                                                    <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"  ></span>Add
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <%--<div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:Label runat="server" Text="Add" />
                                                                                                        </div>--%>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-md-12">
                                                                                                        <div class="col-sm-3 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Brand</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-3 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtBrand" CssClass="form-control" OnTextChanged="txtBrand_TextChanged"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtSeBrand" CausesValidation="false" runat="server" OnClick="lbtSeBrand_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 height22 paddingRight0" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <asp:CheckBox runat="server" ID="chkAllBrand" AutoPostBack="True" OnCheckedChanged="chkAllBrand_CheckedChanged"></asp:CheckBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <asp:Label runat="server" Text="All" />
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-2 paddingRight0">
                                                                                                            <asp:LinkButton ID="lbtnAddBrand" CausesValidation="false" runat="server" OnClick="lbtnAddBrand_Click">
                                                                                                    <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"  ></span>Add
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <%--<div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:Label runat="server" Text="Add" />
                                                                                                        </div>--%>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-md-12">
                                                                                                        <div class="col-sm-3 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Model</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-3 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtModel" CssClass="form-control" OnTextChanged="txtModel_TextChanged"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtnSeModel" CausesValidation="false" runat="server" OnClick="lbtnSeModel_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <asp:CheckBox runat="server" ID="chkAllModel" AutoPostBack="True" OnCheckedChanged="chkAllModel_CheckedChanged"></asp:CheckBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <asp:Label runat="server" Text="All" />
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-2 paddingRight0">
                                                                                                            <asp:LinkButton ID="lbtnAddModel" CausesValidation="false" runat="server" OnClick="lbtnAddModel_Click">
                                                                                                    <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"  ></span>Add
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <%-- <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:Label runat="server" Text="Add" />
                                                                                                        </div>--%>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>

                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4 col-md-4" style="padding-left: 1.5px; padding-right: 1.5px;">
                                                                            <div class="col-md-12">
                                                                                <div class="row">
                                                                                    <div class="col-md-12 padding0">
                                                                                        <div class="panel panel-default padding0">
                                                                                            <div class="panel panel-body" style="height: 105px;">
                                                                                                <div class="row">
                                                                                                    <div class="col-sm-6 col-md-6 ">
                                                                                                        <div class="col-sm-6 col-md-6 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Buying Rate</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-6 col-md-6 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtbuyingrate" CssClass="form-control"></asp:TextBox>
                                                                                                        </div>

                                                                                                    </div>
                                                                                                    <div class="col-sm-6 col-md-6 ">
                                                                                                        <div class="col-sm-6 col-md-6 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Costing Rate</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-6 col-md-6 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtcostingRate" CssClass="form-control"></asp:TextBox>
                                                                                                        </div>

                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-sm-6 col-md-6 ">
                                                                                                        <div class="col-sm-6 col-md-6 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Forcaste Rate</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-6 col-md-6 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtfcstRate" CssClass="form-control"></asp:TextBox>
                                                                                                        </div>

                                                                                                    </div>
                                                                                                    <div class="col-sm-6 col-md-6 ">
                                                                                                        <div class="col-sm-6 col-md-6 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Markup Value</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-6 col-md-6 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtmarkValue" CssClass="form-control"></asp:TextBox>
                                                                                                        </div>

                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-sm-6 col-md-6 ">
                                                                                                        <div class="col-sm-6 col-md-6 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Assembly</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-6 col-md-6 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtasscharg" CssClass="form-control"></asp:TextBox>
                                                                                                        </div>

                                                                                                    </div>
                                                                                                    <div class="col-sm-6 col-md-6 ">
                                                                                                        <div class="col-sm-6 col-md-6 labelText1 padding0">
                                                                                                            <asp:Label runat="server">OverHead%</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-6 col-md-6 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtoverhd" CssClass="form-control"></asp:TextBox>
                                                                                                        </div>

                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-sm-4 col-md-6 ">
                                                                                                        <div class="col-sm-6 col-md-6 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Payment Term</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-6 col-md-6 padding0">
                                                                                                            <asp:DropDownList ID="ddlPaymentTerms" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                                                        </div>

                                                                                                    </div>
                                                                                                    <div class="col-sm-4 col-md-6 ">

                                                                                                        <asp:CheckBox ID="chknewall" Text="" Checked="true" CssClass="marginleft" runat="server"></asp:CheckBox>
                                                                                                        All
                                                                                                    </div>
                                                                                                    <div class="col-sm-4 col-md-6 ">

                                                                                                        <asp:CheckBox ID="chkblpending" Text="" Checked="true" CssClass="marginleft" runat="server"></asp:CheckBox>
                                                                                                        BL Pending
                                                                                                    </div>

                                                                                                </div>

                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12 col-md-12">
                                                                        <div class="col-sm-3" style="padding-left: 1.5px; padding-right: 1px;">
                                                                            <div class="panel panel-default padding0">
                                                                                <%-- <div class="panel panel-heading padding0">
                                                                            Document Criteria   
                                                                        </div>--%>
                                                                                <div class="panel panel-body" style="height: 106px;">
                                                                                    <div class="col-md-12 ">
                                                                                        <div class="col-md-6 padding0">
                                                                                            <div class="row">
                                                                                                <div class="col-md-1 paddingLeft0">
                                                                                                    <asp:RadioButton runat="server" AutoPostBack="true" ID="radioDetailRep"  OnCheckedChanged="radioDetailRep_CheckedChanged"  GroupName="grpReportType" />
                                                                                                </div>
                                                                                                <div class="col-md-10 padding0 labelText1">
                                                                                                    <asp:Label runat="server">Detail Report</asp:Label>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="row">
                                                                                                <div class="col-md-1 paddingLeft0">
                                                                                                    <asp:RadioButton runat="server" AutoPostBack="true" ID="radioSummaryRep"  OnCheckedChanged="radioSummaryRep_CheckedChanged" GroupName="grpReportType" />
                                                                                                </div>
                                                                                                <div class="col-md-10 padding0 labelText1">
                                                                                                    <asp:Label runat="server">Summary Report</asp:Label>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-6 padding0">
                                                                                            <div class="row">
                                                                                                <div class="col-md-1 paddingLeft0">
                                                                                                    <asp:CheckBox runat="server" ID="chkExportExcel" AutoPostBack="True" />
                                                                                                </div>
                                                                                                <div class="col-md-10 labelText1">
                                                                                                    <asp:Label runat="server">Export Excel</asp:Label>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="row">
                                                                                                <div class="col-md-1 paddingLeft0">
                                                                                                    <asp:CheckBox runat="server" ID="chkModelWise" AutoPostBack="True" />
                                                                                                </div>
                                                                                                <div class="col-md-10 labelText1">
                                                                                                    <asp:Label runat="server">Model Wise</asp:Label>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-9" style="padding-left: 1px; padding-right: 1.5px;">
                                                                            <div class="col-md-12 padding0">
                                                                                <div class="panel panel-default padding0">
                                                                                    <div class="panel panel-body" style="height: 106px;">
                                                                                        <div class="col-md-12">
                                                                                            <div class="row">
                                                                                                <div class="col-sm-6" style="padding-left: 0px; padding-right: 0px;">
                                                                                                    <div class="col-md-2" style="padding-left: 0px; padding-right: 1px">
                                                                                                        <asp:Label runat="server">Item Cat. 1</asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-md-2" style="padding-left: 1px; padding-right: 1px">
                                                                                                        <asp:Label runat="server">Item Cat. 2</asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-md-2" style="padding-left: 1px; padding-right: 1px">
                                                                                                        <asp:Label runat="server">Item Cat. 3</asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-md-2" style="padding-left: 1px; padding-right: 0px">
                                                                                                        <asp:Label runat="server">Item Cat. 4</asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-md-2" style="padding-left: 1px; padding-right: 1px">
                                                                                                        <asp:Label runat="server">Item Cat. 5</asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-md-2" style="padding-left: 1px; padding-right: 1px">
                                                                                                        <asp:Label runat="server">Item Code</asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-sm-6" style="padding-left: 1px; padding-right: 1px">


                                                                                                    <div class="col-md-2" style="padding-left: 1px; padding-right: 1px">
                                                                                                        <asp:Label runat="server">Brand</asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-md-2" style="padding-left: 1px; padding-right: 0px">
                                                                                                        <asp:Label runat="server">Model</asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-md-2" style="padding-left: 1px; padding-right: 0px">
                                                                                                        <asp:Label runat="server">Price Levels</asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-md-3" style="padding-left: 1px; padding-right: 0px; font-weight: bold">
                                                                                                        <asp:Label runat="server">Common</asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="row">
                                                                                                <div class="col-sm-6" style="padding-left: 0px; padding-right: 1px">
                                                                                                    <div class="col-md-2" style="padding-left: 2px; padding-right: 1px">
                                                                                                        <asp:ListBox ID="listCat1" runat="server" Height="88px" AutoPostBack="True" Width="100%"></asp:ListBox>
                                                                                                    </div>
                                                                                                    <div class="col-md-2" style="padding-left: 1px; padding-right: 1px">
                                                                                                        <asp:ListBox ID="listCat2" runat="server" Height="88px" AutoPostBack="True" Width="100%"></asp:ListBox>
                                                                                                    </div>
                                                                                                    <div class="col-md-2" style="padding-left: 1px; padding-right: 1px">
                                                                                                        <asp:ListBox ID="listCat3" runat="server" Height="88px" AutoPostBack="True" Width="100%"></asp:ListBox>
                                                                                                    </div>
                                                                                                    <div class="col-md-2" style="padding-left: 1px; padding-right: 0px">
                                                                                                        <asp:ListBox ID="listCat4" runat="server" Height="88px" AutoPostBack="True" Width="100%"></asp:ListBox>
                                                                                                    </div>
                                                                                                    <div class="col-md-2" style="padding-left: 1px; padding-right: 1px">
                                                                                                        <asp:ListBox ID="listCat5" runat="server" Height="88px" AutoPostBack="True" Width="100%"></asp:ListBox>
                                                                                                    </div>
                                                                                                    <div class="col-md-2" style="padding-left: 1px; padding-right: 1px">
                                                                                                        <asp:ListBox ID="listItemCode" runat="server" Height="88px" AutoPostBack="True" Width="100%"></asp:ListBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-sm-6" style="padding-left: 0px; padding-right: 1px">

                                                                                                    <div class="col-md-2" style="padding-left: 1px; padding-right: 1px">
                                                                                                        <asp:ListBox ID="listBrand" runat="server" Height="88px" AutoPostBack="True" Width="100%"></asp:ListBox>
                                                                                                    </div>
                                                                                                    <div class="col-md-2" style="padding-left: 1px; padding-right: 0px">
                                                                                                        <asp:ListBox ID="listModel" runat="server" Height="88px" AutoPostBack="True" Width="100%"></asp:ListBox>
                                                                                                    </div>
                                                                                                    <div class="col-md-2" style="padding-left: 1px; padding-right: 0px">
                                                                                                        <asp:ListBox ID="listPriceLevel" runat="server" Height="88px" AutoPostBack="true" Width="100%"></asp:ListBox>
                                                                                                    </div>
                                                                                                    <div class="col-md-3" style="padding-left: 1px; padding-right: 0px">
                                                                                                        <asp:ListBox ID="listCommon" runat="server" Height="88px" AutoPostBack="True" Width="100%"></asp:ListBox>
                                                                                                    </div>
                                                                                                    <div class="col-md-1">
                                                                                                        <asp:LinkButton ID="listclearall" runat="server" Height="44px" AutoPostBack="True" Width="100%">
                                                                                                            <span class="glyphicon glyphicon-refresh"></span>
                                                                                                        </asp:LinkButton>
                                                                                                        <asp:LinkButton ID="listdelete" runat="server" Height="44px" AutoPostBack="True" Width="100%" OnClick="listdelete_Click">
                                                                                                            <span class="glyphicon glyphicon-remove"></span>
                                                                                                        </asp:LinkButton>
                                                                                                    </div>

                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>

                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


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

            <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
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
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
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
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtSearchbyword" CausesValidation="false" class="form-control" AutoPostBack="false" runat="server"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy9" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
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
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="dgvResultItem" CausesValidation="false" runat="server" AllowPaging="True" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" OnPageIndexChanging="dgvResultItem_PageIndexChanging" OnSelectedIndexChanged="dgvResultItem_SelectedIndexChanged">
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

    <%--<asp:Panel runat="server" ID="SearchPanel" DefaultButton="lbtnSearch">
        <div runat="server" id="divSearch" class="panel panel-primary Mheight">
            <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
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
                            <div class="col-sm-3 paddingRight5">
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cmbSearchbykey" runat="server" class="form-control" AutoPostBack="false">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-2 labelText1">
                                Search by word
                            </div>
                            <asp:Label ID="lblSearchType" runat="server" Text="" Visible="False"></asp:Label>
                            <div class="col-sm-4 paddingRight5">
                                <asp:TextBox ID="txtSearchbyword" CausesValidation="false" class="form-control" AutoPostBack="true" runat="server"></asp:TextBox>
                            </div>
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy9" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
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
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="dgvResultItem" CausesValidation="false" runat="server" AllowPaging="True" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" OnPageIndexChanging="dgvResultItem_PageIndexChanging" OnSelectedIndexChanged="dgvResultItem_SelectedIndexChanged">
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

    </asp:Panel>--%>
</asp:Content>
