<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="Mis_Rep.aspx.cs" Inherits="FastForward.SCMWeb.View.Reports.Mis.Mis_Rep" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/ucLoactionSearch.ascx" TagPrefix="uc1" TagName="ucLoactionSearch" %>
<%@ Register Src="~/UserControls/ucProfitCenterSearch.ascx" TagPrefix="uc1" TagName="ucProfitCenterSearch" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/jquery.toastmessage.js"></script>

    <link href="<%=Request.ApplicationPath%>Css/jquery-ui.theme.min.css" rel="stylesheet" />
    <link href="<%=Request.ApplicationPath%>Css/jquery-ui.min.css" rel="stylesheet" />
    <style>
        .panel-heading {
            padding-bottom: 0;
            padding-bottom: 0;
        }
    </style>
    <script>
        function pageLoad(sender, args) {
            jQuery(".txtFromDate").datepicker({ dateFormat: "d/M/yy", timeFormat: "hh:mm tt" });
            jQuery(".txtToDate").datepicker({ dateFormat: "d/M/yy", timeFormat: "hh:mm tt" });
            jQuery(".txtAsAt").datepicker({ dateFormat: "d/M/yy", timeFormat: "hh:mm tt" });
            jQuery(".txtExDate").datepicker({ dateFormat: "d/M/yy", timeFormat: "hh:mm tt" });
        }
    </script>
    <script type="text/javascript">

        function DateValid(sender, args) {
            var fromDate = Date.parse(document.getElementById('<%=txtFromDate.ClientID%>').value);
            var toDate = Date.parse(document.getElementById('<%=txtToDate.ClientID%>').value);
            if (toDate < fromDate) {
                document.getElementById('<%=txtToDate.ClientID%>').value = document.getElementById('<%=hdfCurrentDate.ClientID%>').value;
                showStickyWarningToast('Please select a valid date range !');
            }
        }

        function isDays(evt, textBox) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if ((charCode == 8)) {
                return true;
            }
            if (textBox.value.length < 5) {
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                else {
                    return true;
                }
            }
            else {
                textBox.value = textBox.value.substr(0, 5);
                alert('Maximum 5 characters are allowed ');
                return false;
            }
        }
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                showStickyWarningToast('Age is incorrect !');
                return false;

            }
            return true;
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
        function yearChange() {
            var d = new Date();
            var n = d.getMonth() + 1;

            jQuery("#BodyContent_ddlMonth").val(n);

            var year = $("#BodyContent_ddlYear").val()
            var month = $("#BodyContent_ddlMonth").val();


            setDateNew(year, month);

        }
        function radChange() {
            var d = new Date();
            var n = d.getMonth() + 1;
            $("#BodyContent_ddlMonth").val(5);



        }

        function monthChange() {
            var year = $("#BodyContent_ddlYear").val()
            var month = $("#BodyContent_ddlMonth").val();

            setDate(year, month);
        }

        function setDate(year, month) {
            var frmdate = new Date(year, month - 1, 1);
            var todate = new Date(year, month, 0);
            $("#BodyContent_txtFromDate").val(frmdate.getDate() + "/" + $("#BodyContent_ddlMonth option:selected").text() + "/" + frmdate.getFullYear());
            $("#BodyContent_txtToDate").val(todate.getDate() + "/" + $("#BodyContent_ddlMonth option:selected").text() + "/" + todate.getFullYear());

        }

        function setDateNew(year, month) {
            var frmdate = new Date(year, month - 1, 1);
            var todate = new Date(year, month, 0);
            $("#BodyContent_txtFromDate").val(frmdate.getDate() + "/" + $("#BodyContent_ddlMonth option:selected").text() + "/" + frmdate.getFullYear());
            $("#BodyContent_txtToDate").val(todate.getDate() + "/" + $("#BodyContent_ddlMonth option:selected").text() + "/" + todate.getFullYear());

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

        .exceldown {
            text-align: center;
            position: center;
            margin-left: 600px;
        }

        .marginleft {
            margin-left: 3px;
        }
    </style>

    <script type="text/javascript">
        function ClearListData() {
            document.getElementById('<%=listGroup.ClientID %>').options.length = 0;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
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
                <asp:CheckBox runat="server" ID="chkAllAdmin" Text="MULTIPLE TEAM" AutoPostBack="True" OnCheckedChanged="chkAllAdmin_CheckedChanged"></asp:CheckBox>
                <asp:CheckBox Checked="false" Text="MULTIPLE COM" runat="server" ID="chkAllCompany" AutoPostBack="True" OnCheckedChanged="chkAllCompany_CheckedChanged"></asp:CheckBox>
                <asp:LinkButton ID="btnDownloadfile" Text="Download" OnClick="btnDownloadfile_Click" runat="server">
                                                                    Download
                </asp:LinkButton>
            </div>
            <div class="col-md-6">
                <asp:RadioButton ID="rbword" Text="Word" Checked="true" CssClass="marginleft" runat="server" GroupName="importreports"></asp:RadioButton>
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
                    <asp:HiddenField ID="hdfCurrentDate" runat="server" />
                    <asp:HiddenField ID="hdfShowPopUp" Value="0" runat="server" />
                    <asp:Panel runat="server" DefaultButton="lbtnView">
                        <div class="col-sm-12 col-md-12 panel panel-default">
                            <div class="row buttonRow" id="HederBtn">
                                <div class="col-sm-12 col-md-12">
                                    <div class="col-md-10">
                                        <h1 style="font-size: small; margin-top: 2px">MIS Report</h1>
                                    </div>
                                    <div class="col-md-1 paddingRight0">
                                        <asp:LinkButton ID="lbtnView" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lbtnView_Click"> 
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
                                        <div class="col-sm-12 col-md-12 panel panel-default padding0 height230 " style="height: 632px">
                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px;">
                                                        <asp:RadioButton ID="rad01" GroupName="grpReport" runat="server" AutoPostBack="True" OnCheckedChanged="rad01_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label1" runat="server" Text="GP Report"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <%--<div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px;">
                                                        <asp:RadioButton ID="rad02" GroupName="grpReport" runat="server" AutoPostBack="True" OnCheckedChanged="rad02_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label2" runat="server" Text="Profit Center Wise Sales Valuation"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>--%>
                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px;">
                                                        <asp:RadioButton ID="rad03" GroupName="grpReport" runat="server" AutoPostBack="True" OnCheckedChanged="rad03_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label4" runat="server" Text="Pending Delivery Report"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px;">
                                                        <asp:RadioButton ID="rad04" GroupName="grpReport" runat="server" AutoPostBack="True" OnCheckedChanged="rad04_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label5" runat="server" Text="Pending Sales Report-Detail"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <%--                                             <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px;">
                                                        <asp:RadioButton ID="rad05" GroupName="grpReport" runat="server" AutoPostBack="True" OnCheckedChanged="rad05_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label6" runat="server" Text="SO Status Report-Detail"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>    --%>

                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px;">
                                                        <asp:RadioButton ID="rad06" GroupName="grpReport" runat="server" AutoPostBack="True" OnCheckedChanged="rad06_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label7" runat="server" Text="Sales Order Status Report"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px;">
                                                        <asp:RadioButton ID="rad07" GroupName="grpReport" runat="server" AutoPostBack="True" OnCheckedChanged="rad07_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label8" runat="server" Text="Sales and Inventory Summary Report"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px;">
                                                        <asp:RadioButton ID="rad08" GroupName="grpReport" runat="server" AutoPostBack="True" OnCheckedChanged="rad08_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label9" runat="server" Text="Valuation Report"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px;">
                                                        <asp:RadioButton ID="rad09" GroupName="grpReport" runat="server" AutoPostBack="True" OnCheckedChanged="rad09_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label10" runat="server" Text="Sale Details Report"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px;">
                                                        <asp:RadioButton ID="rad10" GroupName="grpReport" runat="server" AutoPostBack="True" OnCheckedChanged="rad10_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label11" runat="server" Text="GP Reconciliation Report"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px;">
                                                        <asp:RadioButton ID="rad11" GroupName="grpReport" runat="server" AutoPostBack="True" OnCheckedChanged="rad11_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label12" runat="server" Text="Stock Adjustment Summary"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px;">
                                                        <asp:RadioButton ID="rad12" GroupName="grpReport" runat="server" AutoPostBack="True" OnCheckedChanged="rad12_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label13" runat="server" Text="GIT Reconcilation Report"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px;">
                                                        <asp:RadioButton ID="rad13" GroupName="grpReport" runat="server" AutoPostBack="True" OnCheckedChanged="rad13_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label14" runat="server" Text="AOD Reconcilation Report"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px;">
                                                        <asp:RadioButton ID="rad14" GroupName="grpReport" runat="server" AutoPostBack="True" OnCheckedChanged="rad14_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label15" runat="server" Text="Sale Details Report Category Wise"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px;">
                                                        <asp:RadioButton ID="rad15" GroupName="grpReport" runat="server" AutoPostBack="True" OnCheckedChanged="rad15_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label16" runat="server" Text="RCC Listing Report"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px;">
                                                        <asp:RadioButton ID="rad16" GroupName="grpReport" runat="server" AutoPostBack="True" OnCheckedChanged="rad16_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label17" runat="server" Text="Age of Revert Report"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px;">
                                                        <asp:RadioButton ID="rad17" GroupName="grpReport" runat="server" AutoPostBack="True" OnCheckedChanged="rad17_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label18" runat="server" Text="GP Analysis Report"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px;">
                                                        <asp:RadioButton ID="rad18" GroupName="grpReport" runat="server" AutoPostBack="True" OnCheckedChanged="rad18_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label2" runat="server" Text="GP Report with Replaced Items"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px;">
                                                        <asp:RadioButton ID="rad19" GroupName="grpReport" runat="server" AutoPostBack="True" OnCheckedChanged="rad19_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label6" runat="server" Text="Fitch Report"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px;">
                                                        <asp:RadioButton ID="rad21" GroupName="grpReport" runat="server" AutoPostBack="True" OnCheckedChanged="rad19_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label20" runat="server" Text="Fitch Report-Cate/Brand wise"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px;">
                                                        <asp:RadioButton ID="rad20" GroupName="grpReport" runat="server" AutoPostBack="True" OnCheckedChanged="rad120_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label19" runat="server" Text="PSI Report"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px;">
                                                        <asp:RadioButton ID="rad22" GroupName="grpReport" runat="server" AutoPostBack="True" OnCheckedChanged="rad09_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label21" runat="server" Text="Sales Reconcilation"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px;">
                                                        <asp:RadioButton ID="rad23" GroupName="grpReport" runat="server" AutoPostBack="True" OnCheckedChanged="rad08_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label22" runat="server" Text="Valuation Report (Insurance)"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px;">
                                                        <asp:RadioButton ID="rad24" GroupName="grpReport" runat="server" AutoPostBack="True" OnCheckedChanged="rad25_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label23" runat="server" Text="Sales Order Summery Report"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px;">
                                                        <asp:RadioButton ID="rad25" GroupName="grpReport" runat="server" AutoPostBack="True" OnCheckedChanged="rad24_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label24" runat="server" Text="Credit Card Summery Report"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px;">
                                                        <asp:RadioButton ID="rad26" GroupName="grpReport" runat="server" AutoPostBack="True" OnCheckedChanged="rad26_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label25" runat="server" Text="Sales Order Summery Report New"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px;">
                                                        <asp:RadioButton ID="rad27" GroupName="grpReport" runat="server" AutoPostBack="True" OnCheckedChanged="rad27_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label26" runat="server" Text="CategoryWise Trading GP"></asp:Label>
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
                                                        <div class="col-sm-12 col-md-12 panel panel-default padding0" style="height: 632px;">
                                                            <div class="panel-heading padding0">
                                                                <strong><b>Parameter Details</b></strong>
                                                            </div>
                                                            <div class="panel-body" style="padding-left: 1.5px; padding-right: 1.5px;">
                                                                <div class="row">
                                                                    <div class="col-sm-12 col-md-12">
                                                                        <div class="col-md-3" style="padding-left: 1px; padding-right: 1.5px;">
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
                                                                                                    </ContentTemplate>
                                                                                                </asp:UpdatePanel>
                                                                                            </div>
                                                                                            <div class="col-md-2 labelText1">
                                                                                                <asp:Label runat="server" Text="Multiple" />
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="panel panel-body" style="height: 185px">
                                                                                    <div class="row">
                                                                                        <div class="col-md-12">
                                                                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                                                                                            <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                                                                                                <ContentTemplate>
                                                                                                    <div class="" style="max-height: 160px; overflow: auto;">
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
                                                                        <div class="col-md-3" style="padding-left: 1.5px; padding-right: 1.5px;">
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
                                                                                                    </ContentTemplate>
                                                                                                </asp:UpdatePanel>
                                                                                            </div>
                                                                                            <div class="col-md-2 labelText1">
                                                                                                <asp:Label runat="server" Text="Multiple" />
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="panel panel-body" style="height: 185px">
                                                                                    <div class="row">
                                                                                        <div class="col-md-12">
                                                                                            <div class="panelscoll2" style="height: 160px">
                                                                                                <asp:GridView ID="dgvAdminTeam" CssClass="table table-hover table-striped bound" runat="server" GridLines="None" PagerStyle-CssClass="cssPager"
                                                                                                    EmptyDataText="No data found..." AutoGenerateColumns="False" AllowPaging="false" PageSize="3" OnPageIndexChanging="dgvAdminTeam_PageIndexChanging" OnRowDataBound="dgvAdminTeam_RowDataBound">
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

                                                                                                        <%--   <asp:BoundField DataField="mso_com_cd" HeaderText="Com Code">
                                                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                                                        </asp:BoundField>--%>
                                                                                                        <%--<asp:BoundField DataField="mso_cd" HeaderText="Admin Code">
                                                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                                                        </asp:BoundField>--%>
                                                                                                        <asp:TemplateField HeaderText="Com Code">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblAdminCome" runat="server" Text='<%# Bind("mso_com_cd") %>' Width="80px"></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Admin Code">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblAdminCode" runat="server" Text='<%# Bind("mso_cd") %>' Width="80px"></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>

                                                                                                    </Columns>
                                                                                                    <PagerStyle CssClass="cssPager"></PagerStyle>
                                                                                                </asp:GridView>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>

                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6" style="padding-left: 1.5px; padding-right: 1.5px;">
                                                                            <div class="col-md-12 padding0">
                                                                                <div class="col-md-7 padding0" id="divLoc">
                                                                                    <asp:Panel runat="server" ID="pnlLocation" Visible="true">
                                                                                        <div>
                                                                                            <uc1:ucLoactionSearch runat="server" ID="ucLoactionSearch" />
                                                                                        </div>
                                                                                    </asp:Panel>
                                                                                    <asp:Panel runat="server" ID="pnlProfitCenter" Visible="false">
                                                                                        <uc1:ucProfitCenterSearch runat="server" ID="ucProfitCenterSearch" />
                                                                                    </asp:Panel>
                                                                                </div>
                                                                                <div class="col-md-1">
                                                                                    <div class="row buttonRow">
                                                                                        <div class="col-md-12 padding0">
                                                                                            <div class="col-md-11" style="padding-left: 15px; padding-right: 5px;">
                                                                                                <br />
                                                                                                <br />

                                                                                                <asp:LinkButton ID="lbtnAddLocation" CausesValidation="false" runat="server" OnClick="lbtnAddLocation_Click">
                                                                                                <span class="glyphicon glyphicon-arrow-right" aria-hidden="true"  ></span>
                                                                                                </asp:LinkButton>
                                                                                                <br />
                                                                                                <br />
                                                                                                <br />
                                                                                                <br />
                                                                                                <br />
                                                                                                <br />
                                                                                                <br />
                                                                                                <br />
                                                                                                <br />
                                                                                                <div class="col-sm-1 col-md-11 padding0">
                                                                                                    <asp:LinkButton ID="btnLocation" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnLocation_Click">
                                                                                                               -->
                                                                                                    </asp:LinkButton>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-md-4" style="padding-left: 1.5px; padding-right: 0px;">
                                                                                    <div class="row">
                                                                                        <div class="col-md-12">
                                                                                            <div class="panel panel-default">
                                                                                                <div class="panel-heading">
                                                                                                    <b>Location List</b>
                                                                                                </div>
                                                                                                <div class="panel-body panelscoll2" style="height: 150px">
                                                                                                    <asp:GridView ID="dgvLocation" CssClass="table table-hover table-striped bound" ShowHeader="False" runat="server" GridLines="none" PagerStyle-CssClass="csspager"
                                                                                                        EmptyDataText="no data found..." AutoGenerateColumns="false">
                                                                                                        <EditRowStyle BackColor="midnightblue" />
                                                                                                        <EmptyDataTemplate>
                                                                                                            <table class="table table-hover table-striped" border="0" style="border-collapse: collapse;">
                                                                                                                <tbody>
                                                                                                                    <tr></tr>
                                                                                                                    <tr>
                                                                                                                        <td>no records found.</td>
                                                                                                                    </tr>
                                                                                                                    <tr></tr>
                                                                                                                </tbody>
                                                                                                            </table>
                                                                                                        </EmptyDataTemplate>
                                                                                                        <Columns>
                                                                                                            <asp:TemplateField HeaderText="">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:CheckBox ID="chkLocation" runat="server" />
                                                                                                                </ItemTemplate>
                                                                                                                <HeaderStyle Width="10px" />
                                                                                                                <ItemStyle Width="10px" />
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="Location">
                                                                                                                <%--<ItemTemplate>
                                                                                                                    <asp:Label ID="lblLocation" runat="server" Text='<%# Bind("PROFIT_CENTER") %>' Width="80px"></asp:Label>
                                                                                                                </ItemTemplate>Container.DataItem--%>
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblLocation" runat="server" Text='<%# Bind("pc") %>' Width="80px"></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                                <HeaderStyle Width="80px" />
                                                                                                                <ItemStyle Width="80px" />
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblCompany" Visible="false" runat="server" Text='<%# Bind("com") %>' Width="80px"></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                                <HeaderStyle Width="80px" />
                                                                                                                <ItemStyle Width="80px" />
                                                                                                            </asp:TemplateField>
                                                                                                        </Columns>
                                                                                                        <PagerStyle CssClass="csspager"></PagerStyle>
                                                                                                    </asp:GridView>
                                                                                                </div>
                                                                                            </div>

                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row buttonRow">
                                                                                        <div class="col-md-12">
                                                                                            <div class="col-sm-4 ">
                                                                                                <asp:LinkButton ID="lbtnLocationAll" CausesValidation="false" runat="server" OnClientClick="CheckAllLocation()" OnClick="lbtnLocationAll_Click">
                                                                                                <span               class="glyphicon glyphicon-record" aria-hidden="true"  ></span>All
                                                                                                </asp:LinkButton>
                                                                                            </div>
                                                                                            <div class="col-sm-4  ">
                                                                                                <asp:LinkButton ID="lbtnLocationNone" CausesValidation="false" runat="server" OnClick="lbtnLocationNone_Click">
                                                                                                <span class="glyphicon glyphicon-flash" aria-hidden="true"  ></span>None
                                                                                                </asp:LinkButton>
                                                                                            </div>

                                                                                            <div class="col-sm-4  ">
                                                                                                <asp:LinkButton ID="lbtnLocationClear" CausesValidation="false" runat="server" OnClick="lbtnLocationClear_Click">
                                                                                                <span class="glyphicon glyphicon-refresh" aria-hidden="true"  ></span>Clear
                                                                                                </asp:LinkButton>
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
                                                                        <div class="col-sm-7 col-md-4" style="padding-left: 1.5px; padding-right: 1.5px;">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <div class="panel panel-default">
                                                                                        <div class="panel panel-heading padding0">
                                                                                            <b>Document Criteria</b>
                                                                                        </div>
                                                                                        <div class="panel panel-body" style="height: 273px;">
                                                                                            <div class="col-sm-12 col-md-12 padding0">
                                                                                                <div class="row">
                                                                                                    <div class="col-md-12">
                                                                                                        <div class="col-sm-4 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Doc type</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-5 col-md-5 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtDocType" AutoPostBack="true" OnTextChanged="txtDocType_TextChanged" CssClass="form-control"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtnSeDocType" CausesValidation="false" runat="server" OnClick="lbtnSeDocType_Click">
                                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <asp:CheckBox Checked="true" runat="server" ID="chkAllDocType" AutoPostBack="True" OnCheckedChanged="chkAllDocType_CheckedChanged"></asp:CheckBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <asp:Label runat="server" Text="All" />
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 padding0">
                                                                                                            <asp:LinkButton ID="btnDocType" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnDocType_Click">
                                                                                                               -->
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-md-12">
                                                                                                        <div class="col-sm-4 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Doc #</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-5 col-md-5 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtDocNo" AutoPostBack="true" OnTextChanged="txtDocNo_TextChanged" CssClass="form-control"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtnSedoNo" CausesValidation="false" runat="server" OnClick="lbtnSedoNo_Click">
                                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <asp:CheckBox Checked="true" runat="server" ID="chkAllDocNo" AutoPostBack="True" OnCheckedChanged="chkAllDocNo_CheckedChanged"></asp:CheckBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <asp:Label runat="server" Text="All" />
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 padding0">
                                                                                                            <asp:LinkButton ID="btnAddDoc" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnAddDoc_Click">
                                                                                                               -->
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>

                                                                                                <div class="row">
                                                                                                    <div class="col-md-12">
                                                                                                        <div class="col-sm-4 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Direction</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-5 col-md-5 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtDirection" AutoPostBack="true" OnTextChanged="txtDirection_TextChanged" CssClass="form-control"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtnSeDerection" CausesValidation="false" runat="server" OnClick="lbtnSeDerection_Click">
                                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <asp:CheckBox Checked="true" runat="server" ID="chkAllDirNo" AutoPostBack="True" OnCheckedChanged="chkAllDirNo_CheckedChanged"></asp:CheckBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <asp:Label runat="server" Text="All" />
                                                                                                        </div>

                                                                                                    </div>
                                                                                                </div>

                                                                                                <div class="row">
                                                                                                    <div class="col-md-12">
                                                                                                        <div class="col-sm-3 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Batch No</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-4 padding0">
                                                                                                            <asp:TextBox Style="text-transform: uppercase" runat="server" ID="txtBatchNo" CssClass="form-control"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <%--<asp:LinkButton ID="lbtnSeBatchNo" CausesValidation="false" runat="server" >
                                                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>--%>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <%--<asp:CheckBox Checked="true" runat="server" ID="chkAllBatchNo" AutoPostBack="True" OnCheckedChanged="chkAllBatchNo_CheckedChanged" ></asp:CheckBox>--%>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <asp:Label runat="server" Text="" />
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>

                                                                                                <div class="row">
                                                                                                    <div class="col-md-12">
                                                                                                        <div class="col-sm-3 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Customer</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-4 padding0">
                                                                                                            <asp:TextBox Style="text-transform: uppercase" runat="server" ID="txtCustomer" CssClass="form-control"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtnSeCustomer" CausesValidation="false" runat="server"
                                                                                                                OnClick="lbtnSeCustomer_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <asp:CheckBox Checked="true" runat="server" ID="chkAllCustomer" AutoPostBack="True" OnCheckedChanged="chkAllDirNo_CheckedChanged"></asp:CheckBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <asp:Label runat="server" Text="All" />
                                                                                                        </div>
                                                                                                        <div class="col-sm-2 col-md-2 padding0">
                                                                                                            <asp:LinkButton ID="btnSeCustormer" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnSeCustormer_Click">
                                                                                                             <span class="glyphicon glyphicon-arrow-right" style="color:purple;font-size:small"></span>
                                                                                                            </asp:LinkButton>
                                                                                                            <asp:LinkButton ID="btnSeCustormerRemove" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnSeCustormerRemove_Click">
                                                                                                             <span class="glyphicon glyphicon-remove" style="color:purple;font-size:small"></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>

                                                                                                <div class="row">
                                                                                                    <div class="col-md-12">
                                                                                                        <div class="col-sm-3 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Executive</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-4 padding0">
                                                                                                            <asp:TextBox Style="text-transform: uppercase" runat="server" ID="txtExecutive" CssClass="form-control"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtnSeExecutive" CausesValidation="false" runat="server"
                                                                                                                OnClick="lbtnSeExecutive_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <asp:CheckBox Checked="true" runat="server" ID="chkAllExecutive" AutoPostBack="True" OnCheckedChanged="chkAllDirNo_CheckedChanged"></asp:CheckBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <asp:Label runat="server" Text="All" />
                                                                                                        </div>
                                                                                                        <div class="col-sm-2 col-md-2 padding0">
                                                                                                            <asp:LinkButton ID="btnexective" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnexective_Click">
                                                                                                             <span class="glyphicon glyphicon-arrow-right" style="color:purple;font-size:small"></span>
                                                                                                            </asp:LinkButton>
                                                                                                            <asp:LinkButton ID="btnexectiveRemove" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnexectiveRemove_Click">
                                                                                                             <span class="glyphicon glyphicon-remove" style="color:purple;font-size:small"></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>

                                                                                                <div class="row">
                                                                                                    <div class="col-md-12">
                                                                                                        <div class="col-sm-3 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Promotor</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-4 padding0">
                                                                                                            <asp:TextBox Style="text-transform: uppercase" runat="server" ID="txtPromotor" CssClass="form-control"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtnSePromotor" CausesValidation="false" runat="server"
                                                                                                                OnClick="lbtnSePromotor_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <asp:CheckBox Checked="true" runat="server" ID="chkAllPromotor" AutoPostBack="True" OnCheckedChanged="chkAllDirNo_CheckedChanged"></asp:CheckBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <asp:Label runat="server" Text="All" />
                                                                                                        </div>
                                                                                                        <div class="col-sm-2 col-md-2 padding0">
                                                                                                            <asp:LinkButton ID="btnpromoter" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnpromoter_Click">
                                                                                                             <span class="glyphicon glyphicon-arrow-right" style="color:purple;font-size:small"></span>
                                                                                                            </asp:LinkButton>
                                                                                                            <asp:LinkButton ID="btnpromoterRemove" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnpromoterRemove_Click">
                                                                                                             <span class="glyphicon glyphicon-remove" style="color:purple;font-size:small"></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-md-12">
                                                                                                        <div class="col-sm-3 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Other Loc</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-5 col-md-4 padding0">
                                                                                                            <asp:TextBox Style="text-transform: uppercase" runat="server" ID="txtotherloc" CssClass="form-control"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtnotherloc" CausesValidation="false" runat="server"
                                                                                                                OnClick="lbtnotherloc_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>

                                                                                                <div class="row">
                                                                                                    <div class="col-md-12">
                                                                                                        <div class="col-md-2 labelText1 paddingLeft0">
                                                                                                            <asp:Label runat="server"> From Team</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-3 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtoperteamfrom" CssClass="form-control"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtnfromopteam" CausesValidation="false" runat="server" OnClick="lbtnfromopteam_Click">  
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>

                                                                                                        <div class="col-md-2 labelText1 paddingLeft0">
                                                                                                            <asp:Label runat="server"> To Team</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-3 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtoperteamto" CssClass="form-control"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtntoopteam" CausesValidation="false" runat="server" OnClick="lbtntoopteam_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>

                                                                                                <div class="row">

                                                                                                    <div class="col-md-12">
                                                                                                        <div class="col-md-8 padding0">
                                                                                                            <div class="col-md-1">
                                                                                                                <asp:CheckBox runat="server" ID="chkExportExcel" AutoPostBack="True" />
                                                                                                            </div>
                                                                                                            <div class="col-md-10 labelText1">
                                                                                                                <asp:Label runat="server">Export Excel</asp:Label>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>

                                                                                                <div>
                                                                                                    <asp:RadioButton ID="rbwithicom" Text="Intercompany" Checked="true" runat="server" GroupName="intrcompny"></asp:RadioButton>
                                                                                                    <asp:RadioButton ID="rbwithoutintercompny" Text="Without intercompany" runat="server" GroupName="intrcompny"></asp:RadioButton>
                                                                                                    <asp:RadioButton ID="rbboth" Text="Both" runat="server" GroupName="intrcompny"></asp:RadioButton>
                                                                                                </div>

                                                                                                <div class="row">
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                </div>

                                                                                                <div class="row">
                                                                                                    <div class="col-md-12" style="padding-top: 4px;">
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                        </div>
                                                                        <div class="col-md-4 padding0" id="edit">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <div class="panel panel-default padding0">
                                                                                        <div class="panel panel-heading padding0">
                                                                                            <strong><b>Date Criteria</b></strong>
                                                                                        </div>
                                                                                        <div class="panel-body">
                                                                                            <div style="height: 90px;">
                                                                                                <div class="row">
                                                                                                </div>
                                                                                                <div class="row " style="padding-top: 3px; padding-bottom: 0px;">
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-sm-12 col-md-12">
                                                                                                        <div class="col-sm-2 col-md-2 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Year</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-3 padding0">
                                                                                                            <asp:DropDownList runat="server" ID="ddlYear" CssClass="form-control" onchange="yearChange()">
                                                                                                            </asp:DropDownList>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 labelText1 padding0">
                                                                                                        </div>
                                                                                                        <div class="col-sm-2 col-md-2 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Month</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-4 col-md-4 padding0">
                                                                                                            <asp:DropDownList runat="server" ID="ddlMonth" CssClass="form-control" onchange="monthChange()">
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
                                                                                                <div class="row " style="padding-top: 3px; padding-bottom: 0px;">
                                                                                                    <div class="col-sm-12 col-md-12 padding0">
                                                                                                        <div class="form-group input-group lab" style="margin-top: 4px; margin-bottom: 4px;">
                                                                                                            <div class="col-sm-1 col-md-1">
                                                                                                                <asp:RadioButton runat="server" ID="radioEtaDate" GroupName="dateType"></asp:RadioButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 col-md-2 labelText1 paddingLeft0">
                                                                                                                <asp:Label runat="server">ETA Date</asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1">
                                                                                                                <asp:RadioButton runat="server" ID="radioEtdDate" GroupName="dateType"></asp:RadioButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 col-md-2 labelText1 paddingLeft0">
                                                                                                                <asp:Label runat="server">ETD Date</asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1">
                                                                                                                <asp:RadioButton runat="server" ID="radioClearDate" GroupName="dateType"></asp:RadioButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-4 col-md-4 labelText1 paddingLeft0">
                                                                                                                <asp:Label runat="server">Clearence Date</asp:Label>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-sm-12 col-md-12">
                                                                                                        <div class="col-sm-2 col-md-2 labelText1 padding0">
                                                                                                            <asp:Label runat="server">From Date</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-4 col-md-4 padding0" style="padding-left: 5px; padding-right: 2px;">
                                                                                                            <asp:TextBox runat="server" placeholder="DD/MMM/yyyy" ID="txtFromDate" CssClass="txtFromDate form-control" />
                                                                                                        </div>

                                                                                                        <div class="col-sm-2 col-md-2 labelText1 padding0">
                                                                                                            <asp:Label runat="server">To Date</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-4 col-md-4" style="padding-left: 2px; padding-right: 2px;">
                                                                                                            <asp:TextBox runat="server" placeholder="DD/MMM/yyyy" ID="txtToDate" CssClass="txtToDate form-control" OnTextChanged="txtToDate_TextChanged" />
                                                                                                        </div>

                                                                                                    </div>
                                                                                                    <div class="col-sm-12 col-md-12">
                                                                                                        <div class="col-sm-2 col-md-2 labelText1 padding0">
                                                                                                            <asp:Label runat="server">From Date</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-4 col-md-4 padding0" style="padding-left: 5px; padding-right: 2px;">
                                                                                                            <asp:TextBox runat="server" placeholder="DD/MMM/yyyy" ID="txtFromDate2" CssClass="txtFromDate form-control" />
                                                                                                        </div>

                                                                                                        <div class="col-sm-2 col-md-2 labelText1 padding0">
                                                                                                            <asp:Label runat="server">To Date</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-4 col-md-4" style="padding-left: 2px; padding-right: 2px;">
                                                                                                            <asp:TextBox runat="server" placeholder="DD/MMM/yyyy" ID="txtToDate2" CssClass="txtToDate form-control" OnTextChanged="txtToDate2_TextChanged" />
                                                                                                        </div>

                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row" style="padding-top: 3px;">
                                                                                                    <div class="col-sm-12 col-md-12">
                                                                                                        <div class="col-sm-3 col-md-2 labelText1 padding0">
                                                                                                            <asp:Label runat="server">As At Date</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-4 col-md-4 padding0" style="padding-left: 5px; padding-right: 2px;">
                                                                                                            <asp:TextBox runat="server" placeholder="DD/MMM/yyyy" ID="txtAsAt" CssClass="txtAsAt form-control" />
                                                                                                        </div>

                                                                                                        <div class="col-sm-3 col-md-2 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Expiry Date</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-4 col-md-4 padding0" style="padding-left: 5px; padding-right: 2px;">
                                                                                                            <asp:TextBox runat="server" placeholder="DD/MMM/yyyy" ID="txtExDate" CssClass="txtExDate form-control" />
                                                                                                        </div>

                                                                                                    </div>
                                                                                                </div>

                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <div class="row">
                                                                                        <div class="col-md-12">
                                                                                            <div class="panel panel-default">
                                                                                                <div class="panel-body" style="height: 132px;">
                                                                                                    <div class="row">
                                                                                                        <div class="col-md-12">
                                                                                                            <div class="col-sm-3 col-md-3 labelText1 padding0">
                                                                                                                <asp:Label runat="server">Brand Manager</asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-sm-3 col-md-4 padding0">
                                                                                                                <asp:TextBox runat="server" ID="txtBrandMan" CssClass="form-control"></asp:TextBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                                <asp:LinkButton ID="lbtnSeBrandMan" CausesValidation="false" runat="server" OnClick="lbtnSeBrandMan_Click">
                                                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                                <asp:CheckBox Checked="true" runat="server" ID="chkAllBrandMan" AutoPostBack="True" OnCheckedChanged="chkAllBrandMan_CheckedChanged"></asp:CheckBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                                <asp:Label runat="server" Text="All" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 col-md-2 padding0">
                                                                                                                <asp:LinkButton ID="btnBrandMan" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnBrandMan_Click">
                                                                                                                <span class="glyphicon glyphicon-arrow-right" style="color:purple;font-size:small"></span>
                                                                                                                </asp:LinkButton>
                                                                                                                <asp:LinkButton ID="btnBrandManRemove" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnBrandManRemove_Click">
                                                                                                               <span class="glyphicon glyphicon-remove" style="color:purple;font-size:small"></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>

                                                                                                    <div class="row">
                                                                                                        <div class="col-md-12 ">
                                                                                                            <div class="col-sm-3 col-md-3 labelText1 padding0 ">
                                                                                                                <asp:Label runat="server">Item Status</asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-sm-3 col-md-4 padding0 ">
                                                                                                                <asp:TextBox runat="server" ID="txtItemStat" CssClass="form-control" Enabled="False"></asp:TextBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                                <asp:LinkButton ID="lbtnSeItemSta" CausesValidation="false" runat="server" OnClick="lbtnSeItemSta_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                                <asp:CheckBox Checked="true" runat="server" ID="chkAllStat" AutoPostBack="True" OnCheckedChanged="chkAllStat_CheckedChanged"></asp:CheckBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                                <asp:Label runat="server" Text="All" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 col-md-2 padding0">
                                                                                                                <asp:LinkButton ID="btnItemStats" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnItemStats_Click">
                                                                                                                <span class="glyphicon glyphicon-arrow-right" style="color:purple;font-size:small"></span>
                                                                                                                </asp:LinkButton>
                                                                                                                <asp:LinkButton ID="btnItemStatsRemove" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnItemStatsRemove_Click">
                                                                                                                <span class="glyphicon glyphicon-remove" style="color:purple;font-size:small"></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="row">
                                                                                                        <div class="col-md-12 ">
                                                                                                            <div class="col-sm-3 col-md-3 labelText1 padding0 ">
                                                                                                                <asp:Label runat="server">Supplier</asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-sm-3 col-md-4 padding0 ">
                                                                                                                <asp:TextBox AutoPostBack="true" runat="server" ID="txtSupplier"
                                                                                                                    OnTextChanged="txtSupplier_TextChanged" CssClass="form-control" Enabled="true"></asp:TextBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                                <asp:LinkButton ID="lbtnSeSupplier" CausesValidation="false" runat="server" OnClick="lbtnSeSupplier_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                                <asp:CheckBox Checked="true" runat="server" ID="chkAllSupplier" AutoPostBack="True" OnCheckedChanged="chkAllSupplier_CheckedChanged"></asp:CheckBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                                <asp:Label runat="server" Text="All" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 col-md-2 padding0">
                                                                                                                <asp:LinkButton ID="btnSupplier" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnSupplier_Click">
                                                                                                                <span class="glyphicon glyphicon-arrow-right" style="color:purple;font-size:small"></span>
                                                                                                                </asp:LinkButton>
                                                                                                                <asp:LinkButton ID="btnSupplierRemove" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnSupplierRemove_Click">
                                                                                                                <span class="glyphicon glyphicon-remove" style="color:purple;font-size:small"></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="row">
                                                                                                    </div>
                                                                                                    <div class="row">
                                                                                                        <div class="col-md-12 ">
                                                                                                            <div class="col-md-4 labelText1 padding0">
                                                                                                                <asp:Label runat="server">ABC Classification</asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-md-3 padding0">
                                                                                                                <asp:DropDownList runat="server" ID="ddlClassification" CssClass="form-control">
                                                                                                                    <asp:ListItem Value="0" Text="--Select--" />
                                                                                                                    <asp:ListItem Value="A" Text="A" />
                                                                                                                    <asp:ListItem Value="B" Text="B" />
                                                                                                                    <asp:ListItem Value="C" Text="C" />
                                                                                                                </asp:DropDownList>
                                                                                                            </div>
                                                                                                            <div class="col-sm-4 padding0">
                                                                                                                <div class="col-md-12 paddingRight0">
                                                                                                                    <div class="col-sm-1 col-md-2 height22" style="padding-left: 0px; padding-right: 3px;">
                                                                                                                        <asp:CheckBox runat="server" ID="CheckBox1" AutoPostBack="True"></asp:CheckBox>
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-8 col-md-10 paddingLeft0">
                                                                                                                        <asp:Label runat="server" Text="Status Wise" />
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>



                                                                                                    <div class="row">
                                                                                                        <div class="col-md-12 ">
                                                                                                            <div class="col-sm-2 padding0">
                                                                                                                <asp:Label Text="Filter By" runat="server" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 padding0">
                                                                                                                <asp:RadioButton GroupName="radwise" Text="" ID="radwith" runat="server" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 padding0">
                                                                                                                <asp:Label Text="Qty" runat="server" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-1">
                                                                                                                <asp:RadioButton GroupName="radwise" Text="" ID="radwithout" runat="server" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 padding0">
                                                                                                                <asp:Label Text="Value" runat="server" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-1">
                                                                                                                <asp:RadioButton GroupName="radwise" Text="" ID="radfoc" runat="server" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 padding0">
                                                                                                                <asp:Label Text="Both" runat="server" />
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>


                                                                                                    <div class="row">
                                                                                                        <div class="col-md-12 ">
                                                                                                            <div class="col-sm-2 padding0">
                                                                                                                <asp:Label Text="FOC Status" runat="server" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 padding0">
                                                                                                                <asp:RadioButton GroupName="radwise1" Text="" ID="radwith1" runat="server" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 padding0">
                                                                                                                <asp:Label Text="With" runat="server" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-1">
                                                                                                                <asp:RadioButton GroupName="radwise1" Text="" ID="radwithout1" runat="server" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 padding0">
                                                                                                                <asp:Label Text="Without" runat="server" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-1">
                                                                                                                <asp:RadioButton GroupName="radwise1" Text="" ID="radfoc1" runat="server" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 padding0">
                                                                                                                <asp:Label Text="FOC" runat="server" />
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>

                                                                                                    <!--<div class="row">
                                                                                                        <div class="col-md-12 ">
                                                                                                            <div class="col-sm-2 padding0">
                                                                                                                <asp:label text="Invoice Type" runat="server" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 padding0">
                                                                                                                <asp:RadioButton GroupName="InvType" Text="" ID="rbSale" runat="server" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 padding0">
                                                                                                                <asp:label text="Sale" runat="server" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-1">
                                                                                                                <asp:RadioButton GroupName="InvType" Text="" ID="rbReversal" runat="server" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 padding0">
                                                                                                                <asp:label text="Reversal" runat="server" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-1">
                                                                                                                <asp:RadioButton GroupName="InvType" Text="" ID="rbAll" runat="server" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 padding0">
                                                                                                                <asp:label text="All" runat="server" />
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>-->

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
                                                                                            <div class="panel panel-body" style="height: 218px; padding-left: 2px;">
                                                                                                <div class="col-md-9">
                                                                                                    <div class="row">
                                                                                                        <div class="col-sm-12 col-md-12" style="padding-left: 3px; padding-right: 3px;">
                                                                                                            <div class="col-sm-3 col-md-2 labelText1 padding0">
                                                                                                                <asp:Label runat="server">Cat. 1</asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 col-md-3 padding0">
                                                                                                                <asp:TextBox runat="server" Style="text-transform: uppercase" ID="txtCat1" CssClass="form-control"></asp:TextBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                                <asp:LinkButton ID="lbtnSeCat1" CausesValidation="false" runat="server" OnClick="lbtnSeCat1_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1 height22" style="padding-left: 8px; padding-right: 0px;">
                                                                                                                <asp:CheckBox runat="server" ID="chkAllCat1" AutoPostBack="True" OnCheckedChanged="chkAllCat1_CheckedChanged"></asp:CheckBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1" style="padding-left: 3px; padding-right: 0px;">
                                                                                                                <asp:Label runat="server" Text="All" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-2" style="padding-left: 0px; padding-right: 0px;">
                                                                                                                <asp:LinkButton ID="lbtnAddCat1" CausesValidation="false" runat="server" OnClick="lbtnAddCat1_Click">
                                                                                                    <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"  ></span>
                                                                                                                </asp:LinkButton>
                                                                                                                <asp:LinkButton ID="lbtnAddCat1Remove" CausesValidation="false" runat="server" OnClick="lbtnAddCat1Remove_Click">
                                                                                                    <span class="glyphicon glyphicon-remove" style="color:purple;font-size:small"></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 col-md-2 padding0">
                                                                                                                <asp:LinkButton ID="btnCat1" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnCat1_Click">
                                                                                                              <span class="glyphicon glyphicon-arrow-right" style="color:purple;font-size:small"></span>
                                                                                                                </asp:LinkButton>
                                                                                                                <asp:LinkButton ID="btnCat1Remove" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnCat1Remove_Click">
                                                                                                               <span class="glyphicon glyphicon-remove" style="color:purple;font-size:small"></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="row">
                                                                                                        <div class="col-sm-12 col-md-12" style="padding-left: 3px; padding-right: 3px;">
                                                                                                            <div class="col-sm-3 col-md-2 labelText1 padding0">
                                                                                                                <asp:Label runat="server">Cat. 2</asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 col-md-3 padding0">
                                                                                                                <asp:TextBox runat="server" Style="text-transform: uppercase" ID="txtCat2" CssClass="form-control"></asp:TextBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1" style="padding-left: 3px; padding-right: 0px;">
                                                                                                                <asp:LinkButton ID="lbtnSeCat2" CausesValidation="false" runat="server" OnClick="lbtnSeCat2_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1 height22" style="padding-left: 8px; padding-right: 0px;">
                                                                                                                <asp:CheckBox runat="server" ID="chkAllCat2" AutoPostBack="True" OnCheckedChanged="chkAllCat2_CheckedChanged"></asp:CheckBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1" style="padding-left: 3px; padding-right: 0px;">
                                                                                                                <asp:Label runat="server" Text="All" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-2" style="padding-left: 0px; padding-right: 0px;">
                                                                                                                <asp:LinkButton ID="lbtnAddCat2" CausesValidation="false" runat="server" OnClick="lbtnAddCat2_Click">
                                                                                                                <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"  ></span>
                                                                                                                </asp:LinkButton>
                                                                                                                <asp:LinkButton ID="lbtnAddCat2Remove" CausesValidation="false" runat="server" OnClick="lbtnAddCat2Remove_Click">
                                                                                                                <span class="glyphicon glyphicon-remove" style="color:purple;font-size:small"></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 col-md-2 padding0">
                                                                                                                <asp:LinkButton ID="btnCat2" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnCat2_Click">
                                                                                                              <span class="glyphicon glyphicon-arrow-right" style="color:purple;font-size:small"></span>
                                                                                                                </asp:LinkButton>
                                                                                                                <asp:LinkButton ID="btnCat2Remove" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnCat2Remove_Click">
                                                                                                              <span class="glyphicon glyphicon-remove" style="color:purple;font-size:small"></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="row">
                                                                                                        <div class="col-sm-12 col-md-12" style="padding-left: 3px; padding-right: 3px;">
                                                                                                            <div class="col-sm-3 col-md-2 labelText1 padding0">
                                                                                                                <asp:Label runat="server">Cat. 3</asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 col-md-3 padding0">
                                                                                                                <asp:TextBox runat="server" Style="text-transform: uppercase" ID="txtCat3" CssClass="form-control"></asp:TextBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                                <asp:LinkButton ID="lbtnSeCat3" CausesValidation="false" runat="server" OnClick="lbtnSeCat3_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1 height22" style="padding-left: 8px; padding-right: 0px;">
                                                                                                                <asp:CheckBox runat="server" ID="chkAllCat3" AutoPostBack="True" OnCheckedChanged="chkAllCat3_CheckedChanged"></asp:CheckBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1" style="padding-left: 3px; padding-right: 0px;">
                                                                                                                <asp:Label runat="server" Text="All" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-2" style="padding-left: 0px; padding-right: 0px;">
                                                                                                                <asp:LinkButton ID="lbtnAddCat3" CausesValidation="false" runat="server" OnClick="lbtnAddCat3_Click">
                                                                                                               <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"  ></span>
                                                                                                                </asp:LinkButton>
                                                                                                                <asp:LinkButton ID="lbtnAddCat3Remove" CausesValidation="false" runat="server" OnClick="lbtnAddCat3Remove_Click">
                                                                                                               <span class="glyphicon glyphicon-remove" style="color:purple;font-size:small"></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 col-md-2 padding0">
                                                                                                                <asp:LinkButton ID="btnCat3" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnCat3_Click">
                                                                                                               <span class="glyphicon glyphicon-arrow-right" style="color:purple;font-size:small"></span>
                                                                                                                </asp:LinkButton>
                                                                                                                <asp:LinkButton ID="btnCat3Remove" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnCat3Remove_Click">
                                                                                                                <span class="glyphicon glyphicon-remove" style="color:purple;font-size:small"></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="row">
                                                                                                        <div class="col-sm-12 col-md-12" style="padding-left: 3px; padding-right: 3px;">
                                                                                                            <div class="col-sm-3 col-md-2 labelText1 padding0">
                                                                                                                <asp:Label runat="server">Cat. 4</asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 col-md-3 padding0">
                                                                                                                <asp:TextBox runat="server" Style="text-transform: uppercase" ID="txtCat4" CssClass="form-control"></asp:TextBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                                <asp:LinkButton ID="lbtnSeCat4" CausesValidation="false" runat="server" OnClick="lbtnSeCat4_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1 height22" style="padding-left: 8px; padding-right: 0px;">
                                                                                                                <asp:CheckBox runat="server" ID="chkAllCat4" AutoPostBack="True" OnCheckedChanged="chkAllCat4_CheckedChanged"></asp:CheckBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1" style="padding-left: 3px; padding-right: 0px;">
                                                                                                                <asp:Label runat="server" Text="All" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-2 padding0">
                                                                                                                <asp:LinkButton ID="lbtnAddCat4" CausesValidation="false" runat="server" OnClick="lbtnAddCat4_Click">
                                                                                                                 <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"  ></span>
                                                                                                                </asp:LinkButton>
                                                                                                                <asp:LinkButton ID="lbtnAddCat4Remove" CausesValidation="false" runat="server" OnClick="lbtnAddCat4Remove_Click">
                                                                                                                <span class="glyphicon glyphicon-remove" style="color:purple;font-size:small"></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 col-md-2 padding0">
                                                                                                                <asp:LinkButton ID="btnCat4" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnCat4_Click">
                                                                                                                <span class="glyphicon glyphicon-arrow-right" style="color:purple;font-size:small"></span>
                                                                                                                </asp:LinkButton>
                                                                                                                <asp:LinkButton ID="btnCat4Remove" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnCat4Remove_Click">
                                                                                                               <span class="glyphicon glyphicon-remove" style="color:purple;font-size:small"></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="row">
                                                                                                        <div class="col-sm-12 col-md-12" style="padding-left: 3px; padding-right: 3px;">
                                                                                                            <div class="col-sm-3 col-md-2 labelText1 padding0">
                                                                                                                <asp:Label runat="server">Cat. 5</asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 col-md-3 padding0">
                                                                                                                <asp:TextBox runat="server" Style="text-transform: uppercase" ID="txtCat5" CssClass="form-control"></asp:TextBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                                <asp:LinkButton ID="lbtnSeCat5" CausesValidation="false" runat="server" OnClick="lbtnSeCat5_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1 height22" style="padding-left: 8px; padding-right: 0px;">
                                                                                                                <asp:CheckBox runat="server" ID="chkAllCat5" AutoPostBack="True" OnCheckedChanged="chkAllCat5_CheckedChanged"></asp:CheckBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1" style="padding-left: 3px; padding-right: 0px;">
                                                                                                                <asp:Label runat="server" Text="All" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-2 padding0">
                                                                                                                <asp:LinkButton ID="lbtnAddCat5" CausesValidation="false" runat="server" OnClick="lbtnAddCat5_Click">
                                                                                                                <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"  ></span>
                                                                                                                </asp:LinkButton>
                                                                                                                <asp:LinkButton ID="lbtnAddCat5Remove" CausesValidation="false" runat="server" OnClick="lbtnAddCat5Remove_Click">
                                                                                                                <span class="glyphicon glyphicon-remove" style="color:purple;font-size:small"></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 col-md-2 padding0">
                                                                                                                <asp:LinkButton ID="btnCat5" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnCat5_Click">
                                                                                                               <span class="glyphicon glyphicon-arrow-right" style="color:purple;font-size:small"></span>
                                                                                                                </asp:LinkButton>
                                                                                                                <asp:LinkButton ID="btnCat5Remove" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnCat5Remove_Click">
                                                                                                               <span class="glyphicon glyphicon-remove" style="color:purple;font-size:small"></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="row">
                                                                                                        <div class="col-md-12 " style="padding-left: 3px; padding-right: 3px;">
                                                                                                            <div class="col-sm-3 col-md-2 labelText1 padding0">
                                                                                                                <asp:Label runat="server">Code</asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 col-md-3 padding0">
                                                                                                                <asp:TextBox runat="server" Style="text-transform: uppercase" ID="txtItemCode" CssClass="form-control"></asp:TextBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                                <asp:LinkButton ID="lbtnSeItemCode" CausesValidation="false" runat="server" OnClick="lbtnSeItemCode_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1 height22" style="padding-left: 8px; padding-right: 0px;">
                                                                                                                <asp:CheckBox runat="server" ID="chkAllItemCode" AutoPostBack="True" OnCheckedChanged="chkAllItemCode_CheckedChanged"></asp:CheckBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1" style="padding-left: 3px; padding-right: 0px;">
                                                                                                                <asp:Label runat="server" Text="All" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-2 padding0">
                                                                                                                <asp:LinkButton ID="lbtnAddItemCode" CausesValidation="false" runat="server" OnClick="lbtnAddItemCode_Click">
                                                                                                                 <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"  ></span>
                                                                                                                </asp:LinkButton>
                                                                                                                <asp:LinkButton ID="lbtnAddItemCodeRemove" CausesValidation="false" runat="server" OnClick="lbtnAddItemCodeRemove_Click">
                                                                                                                <span class="glyphicon glyphicon-remove" style="color:purple;font-size:small"></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 col-md-2 padding0">
                                                                                                                <asp:LinkButton ID="btnItemCode" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnItemCode_Click">
                                                                                                                 <span class="glyphicon glyphicon-arrow-right" style="color:purple;font-size:small"></span>
                                                                                                                </asp:LinkButton>
                                                                                                                <asp:LinkButton ID="btnItemCodeRemove" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnItemCodeRemove_Click">
                                                                                                                  <span class="glyphicon glyphicon-remove" style="color:purple;font-size:small"></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="row">
                                                                                                        <div class="col-md-12" style="padding-left: 3px; padding-right: 3px;">
                                                                                                            <div class="col-sm-3 col-md-2 labelText1 padding0">
                                                                                                                <asp:Label runat="server">Brand</asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 col-md-3 padding0">
                                                                                                                <asp:TextBox runat="server" Style="text-transform: uppercase" ID="txtBrand" CssClass="form-control"></asp:TextBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                                <asp:LinkButton ID="lbtSeBrand" CausesValidation="false" runat="server" OnClick="lbtSeBrand_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1 height22 paddingRight0" style="padding-left: 8px; padding-right: 0px;">
                                                                                                                <asp:CheckBox runat="server" ID="chkAllBrand" AutoPostBack="True" OnCheckedChanged="chkAllBrand_CheckedChanged"></asp:CheckBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1" style="padding-left: 3px; padding-right: 0px;">
                                                                                                                <asp:Label runat="server" Text="All" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-2 padding0">
                                                                                                                <asp:LinkButton ID="lbtnAddBrand" CausesValidation="false" runat="server" OnClick="lbtnAddBrand_Click">
                                                                                                                <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"  ></span>
                                                                                                                </asp:LinkButton>
                                                                                                                <asp:LinkButton ID="lbtnAddBrandRemove" CausesValidation="false" runat="server" OnClick="lbtnAddBrandRemove_Click">
                                                                                                               <span class="glyphicon glyphicon-remove" style="color:purple;font-size:small"></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 col-md-2 padding0">
                                                                                                                <asp:LinkButton ID="btnBrand" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnBrand_Click">
                                                                                                               <span class="glyphicon glyphicon-arrow-right" style="color:purple;font-size:small"></span>
                                                                                                                </asp:LinkButton>
                                                                                                                <asp:LinkButton ID="btnBrandRemove" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnBrandRemove_Click">
                                                                                                                <span class="glyphicon glyphicon-remove" style="color:purple;font-size:small"></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="row">
                                                                                                        <div class="col-md-12" style="padding-left: 3px; padding-right: 3px;">
                                                                                                            <div class="col-sm-3 col-md-2 labelText1 padding0">
                                                                                                                <asp:Label runat="server">Model</asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 col-md-3 padding0">
                                                                                                                <asp:TextBox runat="server" Style="text-transform: uppercase" ID="txtModel" CssClass="form-control"></asp:TextBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                                <asp:LinkButton ID="lbtnSeModel" CausesValidation="false" runat="server" OnClick="lbtnSeModel_Click">
                                                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1 height22" style="padding-left: 8px; padding-right: 0px;">
                                                                                                                <asp:CheckBox runat="server" ID="chkAllModel" AutoPostBack="True" OnCheckedChanged="chkAllModel_CheckedChanged"></asp:CheckBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1" style="padding-left: 3px; padding-right: 0px;">
                                                                                                                <asp:Label runat="server" Text="All" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-2 padding0">
                                                                                                                <asp:LinkButton ID="lbtnAddModel" CausesValidation="false" runat="server" OnClick="lbtnAddModel_Click">
                                                                                                                 <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"  ></span>
                                                                                                                </asp:LinkButton>
                                                                                                                <asp:LinkButton ID="lbtnAddModelRemove" CausesValidation="false" runat="server" OnClick="lbtnAddModelRemove_Click">
                                                                                                                <span class="glyphicon glyphicon-remove" style="color:purple;font-size:small"></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 col-md-2 padding0">
                                                                                                                <asp:LinkButton ID="btnModel" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnModel_Click">
                                                                                                                <span class="glyphicon glyphicon-arrow-right" style="color:purple;font-size:small"></span>
                                                                                                                </asp:LinkButton>
                                                                                                                <asp:LinkButton ID="btnModelRemove" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnModelRemove_Click">
                                                                                                                <span class="glyphicon glyphicon-remove" style="color:purple;font-size:small"></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="row">
                                                                                                        <div class="col-md-12" style="padding-left: 3px; padding-right: 3px;">
                                                                                                            <div class="col-sm-3 col-md-2 labelText1 padding0">
                                                                                                                <asp:Label ToolTip="Classification" runat="server">Class.</asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 col-md-3 padding0">
                                                                                                                <asp:TextBox runat="server" ID="txtClassi" CssClass="form-control"></asp:TextBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                                <asp:LinkButton ID="lbtnSeClass" CausesValidation="false" runat="server">
                                                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1 height22" style="padding-left: 8px; padding-right: 0px;">
                                                                                                                <asp:CheckBox Checked="true" runat="server" ID="chkAllClass" AutoPostBack="True"></asp:CheckBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1" style="padding-left: 3px; padding-right: 0px;">
                                                                                                                <asp:Label runat="server" Text="All" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-2 padding0">
                                                                                                                <asp:LinkButton ID="lbtnAddClass" CausesValidation="false" runat="server" OnClick="lbtnAddModel_Click">
                                                                                                   <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"  ></span>
                                                                                                                </asp:LinkButton>
                                                                                                                <asp:LinkButton ID="lbtnRemoveClass" CausesValidation="false" runat="server" OnClick="lbtnRemoveClass_Click">
                                                                                                                <span class="glyphicon glyphicon-remove" style="color:purple;font-size:small"></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 col-md-2 padding0">
                                                                                                                <asp:LinkButton ID="btnClass" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnModel_Click">
                                                                                                              <span class="glyphicon glyphicon-arrow-right" style="color:purple;font-size:small"></span>
                                                                                                                </asp:LinkButton>
                                                                                                                <asp:LinkButton ID="btnClassRemove" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnClassRemove_Click">
                                                                                                                <span class="glyphicon glyphicon-remove" style="color:purple;font-size:small"></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>

                                                                                                    <div class="row">

                                                                                                        <div class="col-sm-4 col-md-4" style="padding-left: 3px; padding-right: 0px;">
                                                                                                            <asp:Label runat="server" Text="With Reversal" />
                                                                                                        </div>

                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px; padding-right: 0px;">
                                                                                                            <asp:CheckBox ID="chkWithReversal" runat="server" />
                                                                                                        </div>

                                                                                                        <div class="col-sm-4 col-md-4" style="padding-left: 3px; padding-right: 0px;">
                                                                                                            <asp:Label runat="server" Text="With Replaced Items" />
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px; padding-right: 0px;">
                                                                                                            <asp:CheckBox ID="chkWithReplItems" runat="server" />
                                                                                                        </div>

                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-3 padding0">
                                                                                                    <div class="row">
                                                                                                        <div class="col-md-12">
                                                                                                            <asp:ListBox ID="listGroup" runat="server" Height="150px" AutoPostBack="True" Width="100%"></asp:ListBox>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="row">
                                                                                                        <div class="col-md-12">
                                                                                                            <div class="col-md-2">
                                                                                                            </div>
                                                                                                            <div class="col-md-10 padding0" style="text-align: center;">
                                                                                                                <asp:LinkButton ID="btnClearGroup" ForeColor="Black" ToolTip="Clear group data" CausesValidation="false" runat="server" OnClientClick="ClearListData();" OnClick="btnClearGroup_Click">
                                                                                                            Clear Group
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                            <div class="col-md-1">
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





                                                                <!--  Delevery Admin Team -->
                                                                <div class="row">

                                                                    <div class="col-md-3" style="padding-left: 16.5px; padding-right: 1.5px;">
                                                                        <div class="panel panel-default">
                                                                            <div class="panel panel-heading" style="height: 27px;">
                                                                                <div class="row">
                                                                                    <div class="col-md-12 " style="padding-left: 1.5px;">
                                                                                        <div class="col-md-8">
                                                                                            <b>Delivery Admin Team</b>
                                                                                        </div>
                                                                                        <div class="col-md-1 height22" style="padding-left: 18px; padding-right: 3px;">
                                                                                            <asp:UpdatePanel runat="server" ID="UpdatePanel15">
                                                                                                <ContentTemplate>
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </div>
                                                                                        <div class="col-md-2 labelText1">
                                                                                            <asp:Label runat="server" Text="Multiple" />
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="panel panel-body" style="height: 75px">
                                                                                <div class="row">
                                                                                    <div class="col-md-12">
                                                                                        <div class="panelscoll2" style="height: 22px">
                                                                                            <asp:GridView ID="dgvDelAdminTeam" CssClass="table table-hover table-striped bound" runat="server" GridLines="None" PagerStyle-CssClass="cssPager"
                                                                                                EmptyDataText="No data found..." AutoGenerateColumns="False" AllowPaging="false" PageSize="3" OnPageIndexChanging="dgvAdminTeam_PageIndexChanging" OnRowDataBound="dgvAdminTeam_RowDataBound">
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
                                                                                                            <tr></tr>
                                                                                                    </table>
                                                                                                </EmptyDataTemplate>
                                                                                                <Columns>
                                                                                                    <asp:TemplateField HeaderText="">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkDelAdminTeam" runat="server" AutoPostBack="True" OnCheckedChanged="chkDelAdminTeam_CheckedChanged" />
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle Width="10px" />
                                                                                                        <ItemStyle Width="10px" />
                                                                                                    </asp:TemplateField>

                                                                                                    <asp:BoundField DataField="mso_com_cd" HeaderText="Com Code">
                                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                                                    </asp:BoundField>
                                                                                                    <%--<asp:boundfield datafield="mso_cd" headertext="Admin Code">
                                                                                                        <headerstyle horizontalalign="Center" />
                                                                                                        <itemstyle horizontalalign="Left" />
                                                                                                    </asp:boundfield>--%>
                                                                                                    <asp:TemplateField HeaderText="Delivery Admin Code">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblDelAdminCode" runat="server" Text='<%# Bind("mso_cd") %>' Width="80px"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>

                                                                                                </Columns>
                                                                                                <PagerStyle CssClass="cssPager"></PagerStyle>
                                                                                            </asp:GridView>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-sm-12 col-md-12">
                                                                        <div class="col-sm-12" style="padding-left: 1px; padding-right: 1.5px;">
                                                                            <div class="col-md-12 padding0">
                                                                                <div class="panel panel-default padding0">
                                                                                    <div class="panel panel-body" style="height: 106px;">
                                                                                        <div class="col-md-12">
                                                                                            <div class="row">
                                                                                                <div class="col-sm-6" style="padding-left: 0px; padding-right: 0px;">
                                                                                                    <div class="col-md-3" style="padding-left: 0px; padding-right: 1px">
                                                                                                        <asp:Label runat="server">Item Cat. 1</asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-md-3" style="padding-left: 1px; padding-right: 1px">
                                                                                                        <asp:Label runat="server">Item Cat. 2</asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-md-3" style="padding-left: 1px; padding-right: 1px">
                                                                                                        <asp:Label runat="server">Item Cat. 3</asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-md-3" style="padding-left: 1px; padding-right: 0px">
                                                                                                        <asp:Label runat="server">Item Cat. 4</asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-sm-6" style="padding-left: 1px; padding-right: 1px">
                                                                                                    <div class="col-md-3" style="padding-left: 1px; padding-right: 1px">
                                                                                                        <asp:Label runat="server">Item Cat. 5</asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-md-3" style="padding-left: 1px; padding-right: 1px">
                                                                                                        <asp:Label runat="server">Item Code</asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-md-3" style="padding-left: 1px; padding-right: 1px">
                                                                                                        <asp:Label runat="server">Brand</asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-md-3" style="padding-left: 1px; padding-right: 0px">
                                                                                                        <asp:Label runat="server">Model</asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="row">
                                                                                                <div class="col-sm-6" style="padding-left: 0px; padding-right: 1px">
                                                                                                    <div class="col-md-3" style="padding-left: 2px; padding-right: 1px">
                                                                                                        <asp:ListBox ID="listCat1" runat="server" Height="88px" AutoPostBack="True" Width="100%"></asp:ListBox>
                                                                                                    </div>
                                                                                                    <div class="col-md-3" style="padding-left: 1px; padding-right: 1px">
                                                                                                        <asp:ListBox ID="listCat2" runat="server" Height="88px" AutoPostBack="True" Width="100%"></asp:ListBox>
                                                                                                    </div>
                                                                                                    <div class="col-md-3" style="padding-left: 1px; padding-right: 1px">
                                                                                                        <asp:ListBox ID="listCat3" runat="server" Height="88px" AutoPostBack="True" Width="100%"></asp:ListBox>
                                                                                                    </div>
                                                                                                    <div class="col-md-3" style="padding-left: 1px; padding-right: 0px">
                                                                                                        <asp:ListBox ID="listCat4" runat="server" Height="88px" AutoPostBack="True" Width="100%"></asp:ListBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-sm-6" style="padding-left: 0px; padding-right: 1px">
                                                                                                    <div class="col-md-3" style="padding-left: 1px; padding-right: 1px">
                                                                                                        <asp:ListBox ID="listCat5" runat="server" Height="88px" AutoPostBack="True" Width="100%"></asp:ListBox>
                                                                                                    </div>
                                                                                                    <div class="col-md-3" style="padding-left: 1px; padding-right: 1px">
                                                                                                        <asp:ListBox ID="listItemCode" runat="server" Height="88px" AutoPostBack="True" Width="100%"></asp:ListBox>
                                                                                                    </div>
                                                                                                    <div class="col-md-3" style="padding-left: 1px; padding-right: 1px">
                                                                                                        <asp:ListBox ID="listBrand" runat="server" Height="88px" AutoPostBack="True" Width="100%"></asp:ListBox>
                                                                                                    </div>
                                                                                                    <div class="col-md-3" style="padding-left: 1px; padding-right: 0px">
                                                                                                        <asp:ListBox ID="listModel" runat="server" Height="88px" AutoPostBack="True" Width="100%"></asp:ListBox>
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
                        <%--temp div--%>
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
</asp:Content>
