<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" MaintainScrollPositionOnPostback="false" AutoEventWireup="true" CodeBehind="Inv_Rep.aspx.cs" Inherits="FastForward.SCMWeb.View.Reports.Inventory.Inv_Rep" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/ucLoactionSearch.ascx" TagPrefix="uc1" TagName="ucLoactionSearch" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/jquery.toastmessage.js"></script>
    <link href="<%=Request.ApplicationPath%>Css/jquery-ui.theme.min.css" rel="stylesheet" />
    <link href="<%=Request.ApplicationPath%>Css/jquery-ui.min.css" rel="stylesheet" />

    <script>
        function pageLoad(sender, args) {
            jQuery(".txtFromDate").datepicker({ dateFormat: "d/M/yy", timeFormat: "hh:mm tt" });
            jQuery(".txtToDate").datepicker({ dateFormat: "d/M/yy", timeFormat: "hh:mm tt" });
            jQuery(".txtAsAt").datepicker({ dateFormat: "d/M/yy", timeFormat: "hh:mm tt" });
            jQuery(".txtExDate").datepicker({ dateFormat: "d/M/yy", timeFormat: "hh:mm tt" });
        }
    </script>

    <style>
        .panel-heading {
            padding-bottom: 0;
            padding-bottom: 0;
        }

        .toast-type-warning {
            width: 320px;
        }

        .exceldown {
            text-align: center;
            position: center;
            margin-left: 600px;
        }
    </style>
    <script type="text/javascript">


        hiddenlink();
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
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
            $().toastmessage('showSuccessToast', "Success Dialog which is fading away ...");
        }
        function showStickySuccessToast(value) {
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
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
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
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
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
            $().toastmessage('showErrorToast', "Error Dialog which is fading away ...");
        }
        function showStickyErrorToast(value) {
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
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
        if ($("#BodyContent_rad40").cheked == false) {
            $("#BodyContent_btnDownloadfile").hide();
        }
        //$("#BodyContent_btnDownloadfile").hide();
        //$("#BodyContent_btnDownloadfile").css('visibility', 'hidden');
        function Showlink() {

            $("#BodyContent_btnDownloadfile").show();
        }
        function hiddenlink() {

            $("#BodyContent_btnDownloadfile").hide();
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

        .marginleft {
            margin-left: 4px;
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
        runat="server" AssoChargeiatedUpdatePanelID="UpdatePanel1">

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
                <asp:LinkButton ID="btnDownloadfile" Text="Download" OnClick="btnDownloadfile_Click" runat="server" CssClass="exceldown"> Download </asp:LinkButton>
            </div>
            <div class="col-md-6">
                <asp:RadioButton ID="rbword" Text="Word" Checked="true" CssClass="marginleft" runat="server" GroupName="importreports"></asp:RadioButton>
                <asp:RadioButton ID="rbpdf" Text="PDF" Checked="true" CssClass="marginleft" runat="server" GroupName="importreports"></asp:RadioButton>
                <asp:RadioButton ID="rbexel" Text="Excel" runat="server" CssClass="marginleft" GroupName="importreports"></asp:RadioButton>
                <asp:RadioButton ID="rbexeldata" Text="Excel Data Only" CssClass="marginleft" runat="server" GroupName="importreports"></asp:RadioButton>
            </div>

        </div>

    </div>
    <%--  <asp:Button ID="btnDownloadfile"  Text="Download" OnClick="btnDownloadfile_Click" runat="server" CssClass="exceldown btn-default" />--%>

    <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <div class="col-sm-12 col-md-12" style="margin-left: 3px;">
                <div class="row">
                    <asp:HiddenField ID="hdfClearData" runat="server" />
                    <asp:HiddenField ID="hdfCurrentDate" runat="server" />
                    <asp:HiddenField ID="hdfShowPopUp" Value="0" runat="server" />
                    <asp:Panel runat="server" ID="pnl1" DefaultButton="lbtnView">
                        <div class="col-sm-12 col-md-12 panel panel-default">
                            <div class="row buttonRow" id="HederBtn">
                                <div class="col-sm-12 col-md-12">
                                    <div class="col-md-9">
                                        <h1 style="font-size: small; margin-top: 2px">Inventory Report</h1>
                                    </div>

                                    <div class="col-md-1 ">
                                        <%--   <asp:LinkButton ID="btnDownloadfile"  runat="server" CssClass="floatRight" OnClick="btnDownloadfile_Click"> 
                                <span class="glyphicon glyphicon-download" aria-hidden="true"></span>Download </asp:LinkButton>--%>
                                        <%-- <asp:Button ID="btnDownloadfile" Text="Download" OnClick="btnDownloadfile_Click" runat="server" />--%>
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
                                        <div class="col-sm-12 col-md-12 panel panel-default padding0 height230 " style="height: 634px; overflow: auto;">
                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px;">
                                                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="rad01_CheckedChanged" ID="rad01" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label1" runat="server" Text="Current Stock Balance (Items)"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="rad02_CheckedChanged" ID="rad02" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label2" runat="server" Text="Current Stock Balance (Serial)"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="rad03_CheckedChanged" ID="rad03" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label4" runat="server" Text="Stock Balance as at (Items)"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="rad04_CheckedChanged" ID="rad04" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label5" runat="server" Text="Stock Balance as at (Serial)"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="rad05_CheckedChanged" ID="rad05" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label6" runat="server" Text="Movement Audit Trial (Items)"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="rad06_CheckedChanged" ID="rad06" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label7" runat="server" Text="Movement Audit Trial (Serials)"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <%--              <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="rad07_CheckedChanged" ID="rad07" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label10" runat="server" Text="Age (Items)"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="rad08_CheckedChanged" ID="rad08" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label8" runat="server" Text="Age (Serials)"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>--%>
                                            <%--         <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="rad09_CheckedChanged" ID="rad09" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label9" runat="server" Text="Age - Expiry Date-wise"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>--%>

                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="rad10_CheckedChanged" ID="rad10" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label11" runat="server" Text="PO Summary "></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="rad11_CheckedChanged" ID="rad11" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label12" runat="server" Text="PO Detail (IV4)"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="rad12_CheckedChanged" ID="rad12" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label13" runat="server" Text="Local Purchase cost Detail"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="rad13_CheckedChanged" ID="rad13" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label14" runat="server" Text="PO-GRN pending Detail"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="rad14_CheckedChanged" ID="rad14" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label15" runat="server" Text="Bond Balance"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="rad15_CheckedChanged" ID="rad15" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label16" runat="server" Text="Local Purchase Cost Detail"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="rad16_CheckedChanged" ID="rad16" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label17" runat="server" Text="Value Addition"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="rad17_CheckedChanged" ID="rad17" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label18" runat="server" Text="Item-wise Transaction Detail "></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="rad18_CheckedChanged" ID="rad18" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label19" runat="server" Text="Stock Ledger"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="rad19_CheckedChanged" ID="rad19" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label20" runat="server" Text="Movement - Cost Detail"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="rad20_CheckedChanged" ID="rad20" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label21" runat="server" Text="Serial Movement"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="rad21_CheckedChanged" ID="rad21" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label22" runat="server" Text="Current Age Analysis"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="rad51_CheckedChanged" ID="rad51" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label48" runat="server" Text="Current Age Analysis(Company)"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="rad22_CheckedChanged" ID="rad22" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label23" runat="server" Text="Item Age Analysis with Serial"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="rad23_CheckedChanged" ID="rad23" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label24" runat="server" Text="Inventory Statement"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="rad24_CheckedChanged" ID="rad24" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label25" runat="server" Text="Movement Summary"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="rad25_CheckedChanged" ID="rad25" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label26" runat="server" Text="Item Serial"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="rad26_CheckedChanged" ID="rad26" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label27" runat="server" Text="Consignment Movement"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="rad27_CheckedChanged" ID="rad27" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label28" runat="server" Text="Item Age Analysis"></asp:Label>
                                                    </div>
                                                </div>

                                                <%--          <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="rad28_CheckedChanged" ID="rad28" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label29" runat="server" Text="Age Monitoring"></asp:Label>
                                                    </div>
                                                </div>--%>

                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="rad29_CheckedChanged" ID="rad29" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label30" runat="server" Text="GIT - Current"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="rad30_CheckedChanged" ID="rad30" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label31" runat="server" Text="GIT - Asat"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" ID="rad31" GroupName="grpReport" runat="server" OnCheckedChanged="rad31_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label8" runat="server" Text="To Bond Status"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" ID="rad32" GroupName="grpReport" runat="server" OnCheckedChanged="rad32_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label10" runat="server" Text="Item Buffer Status"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" ID="rad33" GroupName="grpReport" runat="server" OnCheckedChanged="rad33_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label32" runat="server" Text="Allocation Details"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" ID="rad34" GroupName="grpReport" runat="server" OnCheckedChanged="rad34_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label33" runat="server" Text="Dispatch Request Status"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 col-md-12" style="display: none">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" ID="rad35" GroupName="grpReport" runat="server" OnCheckedChanged="rad35_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label34" runat="server" Text="Reservation Details"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" ID="rad36" GroupName="grpReport" runat="server" OnCheckedChanged="rad36_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label35" runat="server" Text="Reservation Summary"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" ID="rad37" GroupName="grpReport" runat="server" OnCheckedChanged="rad37_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label36" runat="server" Text="Location Details"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" ID="rad38" GroupName="grpReport" runat="server" OnCheckedChanged="rad38_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label37" runat="server" Text="Inventory Liability"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" ID="rad39" GroupName="grpReport" runat="server" OnCheckedChanged="rad39_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label38" runat="server" Text="Last Number Sequence"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" ID="rad40" GroupName="grpReport" runat="server" OnCheckedChanged="rad40_CheckedChanged" OnClick="Showlink()" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label39" runat="server" Text="Item Profile Details"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" ID="rad41" GroupName="grpReport" runat="server" OnCheckedChanged="rad41_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label9" runat="server" Text="Item-Wise Warehouse Movements"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" ID="rad42" GroupName="grpReport" runat="server" OnCheckedChanged="rad42_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label29" runat="server" Text="Approval Dispatch Status"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" ID="rad43" GroupName="grpReport" runat="server" OnCheckedChanged="rad43_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label40" runat="server" Text="AOD Reconciliation Report"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" ID="rad44" GroupName="grpReport" runat="server" OnCheckedChanged="rad44_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label41" runat="server" Text="Excess Short Stock Report"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" ID="rad45" GroupName="grpReport" runat="server" OnCheckedChanged="rad45_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label42" runat="server" Text="Showroom Request Report"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" ID="rad46" GroupName="grpReport" runat="server" OnCheckedChanged="rad46_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label43" runat="server" Text="Daily warehouse stock status"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" ID="rad47" GroupName="grpReport" runat="server" OnCheckedChanged="rad47_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label44" runat="server" Text="Reservation Items"></asp:Label>
                                                    </div>
                                                </div>

                                                <!--<div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" ID="rad48" GroupName="grpReport" runat="server" OnCheckedChanged="rad48_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label45" runat="server" Text="CusDec Entry Request Details"></asp:Label>
                                                    </div>
                                                </div>-->
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" ID="rad49" GroupName="grpReport" runat="server" OnCheckedChanged="rad49_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label46" runat="server" Text="ADJ Details"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" ID="rad50" GroupName="grpReport" runat="server" OnCheckedChanged="rad49_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label47" runat="server" Text="Pending Entry Details"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" ID="rad52" GroupName="grpReport" runat="server" OnCheckedChanged="rad52_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label53" runat="server" Text="AOD IN-OUT Details"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" ID="rad53" GroupName="grpReport" runat="server" OnCheckedChanged="rad53_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label50" runat="server" Text="Current Consingment Stock Value"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" ID="rad54" GroupName="grpReport" runat="server" OnCheckedChanged="rad54_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label49" runat="server" Text="AsAt Consingment Stock Value"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" ID="rad55" GroupName="grpReport" runat="server" OnCheckedChanged="rad55_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label55" runat="server" Text="Daily Entry Details (Bond)"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" ID="rad56" GroupName="grpReport" runat="server" OnCheckedChanged="rad56_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label56" runat="server" Text="Charge Sheet - Product condition"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" ID="rad57" GroupName="grpReport" runat="server" OnCheckedChanged="rad57_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label57" runat="server" Text="Current Availability Against BL"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" ID="rad58" GroupName="grpReport" runat="server" OnCheckedChanged="rad58_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label58" runat="server" Text="Aging report for Provisioning"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px">
                                                        <asp:RadioButton AutoPostBack="true" ID="rad59" GroupName="grpReport" runat="server" OnCheckedChanged="rad59_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label59" runat="server" Text="Fixed Assets Details with Depreciation"></asp:Label>
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
                                                        <div class="col-sm-12 col-md-12 panel panel-default padding0" style="height: 634px;">
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
                                                                                            <div class="col-md-2 height22" style="padding-left: 25px; padding-right: 3px;">
                                                                                                <%-- <asp:UpdatePanel runat="server" ID="UpdatePanel4">
                                                                                                    <ContentTemplate>--%>
                                                                                                <asp:CheckBox Checked="false" runat="server" ID="chkAllCompany" AutoPostBack="True" OnCheckedChanged="chkAllCompany_CheckedChanged"></asp:CheckBox>
                                                                                                <%-- </ContentTemplate>
                                                                                                </asp:UpdatePanel>--%>
                                                                                            </div>
                                                                                            <div class="col-md-2 labelText1 paddingLeft0">
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
                                                                                            <div class="col-md-2 height22" style="padding-left: 25px; padding-right: 3px;">
                                                                                                <%-- <asp:UpdatePanel runat="server" ID="UpdatePanel5">
                                                                                                    <ContentTemplate>--%>
                                                                                                <asp:CheckBox runat="server" ID="chkAllAdmin" AutoPostBack="True" OnCheckedChanged="chkAllAdmin_CheckedChanged"></asp:CheckBox>
                                                                                                <%--</ContentTemplate>
                                                                                                </asp:UpdatePanel>--%>
                                                                                            </div>
                                                                                            <div class="col-md-2 labelText1 paddingLeft0">
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
                                                                                        </div>
                                                                                    </div>

                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6" style="padding-left: 1.5px; padding-right: 1.5px;">
                                                                            <div class="col-md-12 padding0">
                                                                                <div class="col-md-7 padding0" id="divLoc">

                                                                                    <asp:Panel runat="server" ID="locationPanel">
                                                                                        <div>
                                                                                            <uc1:ucLoactionSearch runat="server" ID="ucLoactionSearch" />
                                                                                        </div>
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
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblLocation" runat="server" Text='<%# Bind("PROFIT_CENTER") %>' Width="80px"></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                                <HeaderStyle Width="80px" />
                                                                                                                <ItemStyle Width="80px" />
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblCompany" Visible="false" runat="server" Text='<%# Bind("ml_com_cd") %>' Width="80px"></asp:Label>
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
                                                                                        <div class="panel panel-body" style="height: 330px;">
                                                                                            <div class="col-sm-12 col-md-12 padding0">
                                                                                                <div class="row">
                                                                                                    <div class="col-md-12">
                                                                                                        <div class="col-sm-4 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Doc type</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-5 col-md-5 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtDocType" CssClass="form-control"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtnSeDocType" CausesValidation="false" runat="server" OnClick="lbtnSeDocType_Click">
                                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <asp:CheckBox runat="server" ID="chkAllDocType" AutoPostBack="True" OnCheckedChanged="chkAllDocType_CheckedChanged"></asp:CheckBox>
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
                                                                                                        <div class="col-sm-3 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Doc Sub Type</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-5 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtDocSubType" CssClass="form-control"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtnSeDocSubType" CausesValidation="false" runat="server" OnClick="lbtnSeDocSubType_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <asp:CheckBox runat="server" ID="chkAllDocSubType" AutoPostBack="True" OnCheckedChanged="chkAllDocSubType_CheckedChanged"></asp:CheckBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <asp:Label runat="server" Text="All" />
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 padding0">
                                                                                                            <asp:LinkButton ID="btnDocSubType" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnDocSubType_Click">
                                                                                                               -->
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-md-12">
                                                                                                        <div class="col-sm-3 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Document No</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-5 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtDocNo" CssClass="form-control"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtnSeDocNo" CausesValidation="false" runat="server" OnClick="lbtnSeDocNo_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <asp:CheckBox runat="server" ID="chkAllDocNo" AutoPostBack="True" OnCheckedChanged="chkAllDocNo_CheckedChanged"></asp:CheckBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <asp:Label Visible="false" runat="server" Text="All" />
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-md-12">
                                                                                                        <div class="col-sm-3 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Direction</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-5 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtDirection" CssClass="form-control"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtnSeDerection" CausesValidation="false" runat="server" OnClick="lbtnSeDerection_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <asp:CheckBox runat="server" ID="chkAllDirNo" AutoPostBack="True" OnCheckedChanged="chkAllDirNo_CheckedChanged"></asp:CheckBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <asp:Label runat="server" Text="All" />
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <%--<div class="row">
                                                                                                    <div class="col-md-12">
                                                                                                        <div class="col-sm-3 col-md-3 labelText1  padding0">
                                                                                                            <asp:Label runat="server">Entry Type</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-6 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtEntryType" CssClass="form-control"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtnSeEntry" CausesValidation="false" runat="server" OnClick="lbtnSeEntry_Click" >
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <asp:CheckBox Checked="true" runat="server" ID="chkAllEntType" AutoPostBack="True" OnCheckedChanged="chkAllEntType_CheckedChanged"></asp:CheckBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <asp:Label runat="server" Text="All" />
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-md-12">
                                                                                                        <div class="col-sm-3 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Receipt Type</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-6 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtRecType" CssClass="form-control"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtnSeReceipt" CausesValidation="false" runat="server"  >
                                                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <asp:CheckBox Checked="true" runat="server" ID="chkAllRecType" AutoPostBack="True" OnCheckedChanged="chkAllRecType_CheckedChanged" ></asp:CheckBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <asp:Label runat="server" Text="All" />
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>--%>
                                                                                                <div class="row">
                                                                                                    <div class="col-md-12">
                                                                                                        <div class="col-sm-3 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Batch No</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-5 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtBatchNo" CssClass="form-control"></asp:TextBox>
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
                                                                                                            <asp:Label runat="server">Bond Number</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-5 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtBondNumber" CssClass="form-control"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="btnBondNumber" CausesValidation="false" runat="server" OnClick="btnBondNumber_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <%-- <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <asp:CheckBox Checked="true" runat="server" ID="CheckBox2" AutoPostBack="True" OnCheckedChanged="chkAllDocSubType_CheckedChanged"></asp:CheckBox>
                                                                                                        </div>--%>
                                                                                                        <%--  <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <asp:Label runat="server" Text="All" />
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 padding0">
                                                                                                            <asp:LinkButton ID="LinkButton2" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnDocSubType_Click">
                                                                                                               -->
                                                                                                            </asp:LinkButton>
                                                                                                        </div>--%>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-md-12">
                                                                                                        <div class="col-sm-3 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">GRN No</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-5 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtGrnNo" CssClass="form-control"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="btnGrnNo" CausesValidation="false" runat="server" OnClick="btnGrnNo_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <%-- <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <asp:CheckBox Checked="true" runat="server" ID="CheckBox2" AutoPostBack="True" OnCheckedChanged="chkAllDocSubType_CheckedChanged"></asp:CheckBox>
                                                                                                        </div>--%>
                                                                                                        <%--  <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <asp:Label runat="server" Text="All" />
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 padding0">
                                                                                                            <asp:LinkButton ID="LinkButton2" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnDocSubType_Click">
                                                                                                               -->
                                                                                                            </asp:LinkButton>
                                                                                                        </div>--%>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-md-12">
                                                                                                        <div class="col-sm-3 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Request No</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-5 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtRequestNo" CssClass="form-control"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="btnRequestNo" CausesValidation="false" runat="server" OnClick="btnRequestNo_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-md-12">

                                                                                                        <div class="col-md-3 labelText1 paddingLeft0">
                                                                                                            <asp:Label runat="server">Other Loc</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-2 col-md-2 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtOtherloc" CssClass="form-control"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="btnotherloc" CausesValidation="false" runat="server" OnClick="btnotherloc_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>


                                                                                                        <div class="col-md-3 labelText1 paddingLeft0">
                                                                                                            <asp:Label runat="server">Oper Team</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-2 col-md-2 padding0">
                                                                                                            <asp:TextBox runat="server" ID="txtadmintm" CssClass="form-control"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="btmadmintm" CausesValidation="false" runat="server" OnClick="btmadmintm_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-md-12" style="padding-top: 4px;">
                                                                                                        <div class="col-md-3 labelText1 paddingLeft0">
                                                                                                            <asp:Label runat="server">Report Type</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-md-3 padding0">
                                                                                                            <asp:DropDownList runat="server" ID="ddlReportType" CssClass="form-control">
                                                                                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                                                                                <asp:ListItem>ALL</asp:ListItem>
                                                                                                                <asp:ListItem>GRAN</asp:ListItem>
                                                                                                                <asp:ListItem>DIN</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </div>
                                                                                                        <div class="col-md-3 labelText1">
                                                                                                            <asp:Label runat="server">Doc Status</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-md-3 padding0">
                                                                                                            <asp:DropDownList runat="server" ID="ddlDocStatus" CssClass="form-control">
                                                                                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                                                                                <asp:ListItem>X - ALL</asp:ListItem>
                                                                                                                <asp:ListItem>F - FINISHED/CONFIRMED</asp:ListItem>
                                                                                                                <asp:ListItem>P - PENDING</asp:ListItem>
                                                                                                                <asp:ListItem>A - APPROVED</asp:ListItem>
                                                                                                                <asp:ListItem>C - CANCEL</asp:ListItem>
                                                                                                                <asp:ListItem>R - REJECTED</asp:ListItem>
                                                                                                                <asp:ListItem>T - TRANSFERED</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-md-12">
                                                                                                        <div class="col-md-6 padding0">
                                                                                                            <div class="col-md-1 height22 padding0">
                                                                                                                <asp:CheckBox ID="chkWithGit" runat="server" AutoPostBack="true" Enabled="true" />
                                                                                                            </div>
                                                                                                            <div class="col-md-9">
                                                                                                                With GIT
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="col-md-6 padding0">
                                                                                                            <div class="col-md-1 height22">
                                                                                                                <asp:CheckBox ID="chkWithJob" runat="server" AutoPostBack="true" Enabled="true" />
                                                                                                            </div>
                                                                                                            <div class="col-md-9">
                                                                                                                With JOB
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-md-12">
                                                                                                        <div class="col-md-6 padding0">
                                                                                                            <div class="col-md-1 height22 padding0">
                                                                                                                <asp:CheckBox ID="chkWithCostWIP" runat="server" AutoPostBack="true" Enabled="true" OnCheckedChanged="chkWithCostWIP_CheckedChanged" />
                                                                                                            </div>
                                                                                                            <div class="col-md-9">
                                                                                                                With Cost-WIP
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="col-md-6 padding0">
                                                                                                            <div class="col-md-1 height22">
                                                                                                                <asp:CheckBox ID="chkWithServiceWIP" runat="server" AutoPostBack="true" Enabled="true" />
                                                                                                            </div>
                                                                                                            <div class="col-md-9">
                                                                                                                With Service-WIP
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-sm-12 col-md-12 padding0" style="height: 25px;">
                                                                                                        <div class="form-group input-group lab col-md-12">
                                                                                                            <div class="col-sm-1 col-md-1" style="margin-top: 3px;">
                                                                                                                <asp:RadioButton runat="server" ID="radioBoth" GroupName="dateType"></asp:RadioButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 col-md-2 labelText1 paddingLeft0">
                                                                                                                <asp:Label runat="server">Both</asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1" style="padding-left: 10px; margin-top: 3px;">
                                                                                                                <asp:RadioButton runat="server" ID="radioLocal" GroupName="dateType"></asp:RadioButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 col-md-2 labelText1 paddingLeft0">
                                                                                                                <asp:Label runat="server">Local</asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1" style="margin-top: 3px;">
                                                                                                                <asp:RadioButton runat="server" ID="radioImport" GroupName="dateType"></asp:RadioButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-4 col-md-2 labelText1 paddingLeft0">
                                                                                                                <asp:Label runat="server">Import</asp:Label>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class=" row">
                                                                                                    <div class="col-sm-12">
                                                                                                        <div class="col-sm-1 padding0">
                                                                                                            <asp:CheckBox Checked="false" runat="server" ID="chkExportExcel" AutoPostBack="false"></asp:CheckBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 paddingLeft0">
                                                                                                            <asp:Label runat="server" Text="Export Excel" />
                                                                                                        </div>


                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-sm-1 padding0 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                        <asp:RadioButton runat="server" ID="chkNor" GroupName="radsumdet" Checked="true"></asp:RadioButton>
                                                                                                    </div>
                                                                                                    <div class="col-sm-1 paddingLeft0">
                                                                                                        <asp:Label runat="server" Text="Normal" />
                                                                                                    </div>

                                                                                                    <div class="col-sm-1 padding0 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                        <asp:RadioButton runat="server" ID="chkSumm" GroupName="radsumdet"></asp:RadioButton>
                                                                                                    </div>
                                                                                                    <div class="col-sm-1 paddingLeft0">
                                                                                                        <asp:Label runat="server" Text="Summary" />
                                                                                                    </div>
                                                                                                    <div class="col-sm-1 padding0 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                        <asp:RadioButton runat="server" ID="chkDet" GroupName="radsumdet"></asp:RadioButton>
                                                                                                    </div>
                                                                                                    <div class="col-sm-1 paddingLeft0">
                                                                                                        <asp:Label runat="server" Text="Detail" />
                                                                                                    </div>
                                                                                                    <div class="col-sm-1 padding0 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                        <asp:RadioButton runat="server" ID="chklist" GroupName="radsumdet"></asp:RadioButton>
                                                                                                    </div>
                                                                                                    <div class="col-sm-1 paddingLeft0">
                                                                                                        <asp:Label runat="server" Text="Listing" />
                                                                                                    </div>
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
                                                                                                </div>

                                                                                                <%--Validator For Date Range--%>
                                                                                                <%-- <asp:CompareValidator id="cvtxtStartDate" runat="server" 
                                                                                                 ControlToCompare="txtFromDate" cultureinvariantvalues="true" 
                                                                                                 display="Dynamic" enableclientscript="true"  
                                                                                                 ControlToValidate="txtToDate" 
                                                                                                 ErrorMessage="Start date must be earlier than finish date"
                                                                                                 type="Date" setfocusonerror="true" Operator="GreaterThanEqual" 
                                                                                                 text="Start date must be earlier than finish date"></asp:CompareValidator>--%>

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
                                                                                                <div class="panel-body" style="height: 215px;">
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
                                                                                                                <asp:CheckBox runat="server" ID="chkAllBrandMan" AutoPostBack="True" OnCheckedChanged="chkAllBrandMan_CheckedChanged"></asp:CheckBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                                <asp:Label runat="server" Text="All" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1 padding0">
                                                                                                                <asp:LinkButton ID="btnBrandMan" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnBrandMan_Click">
                                                                                                               -->
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
                                                                                                                <asp:CheckBox runat="server" ID="chkAllStat" AutoPostBack="True" OnCheckedChanged="chkAllStat_CheckedChanged"></asp:CheckBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                                <asp:Label runat="server" Text="All" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1 padding0">
                                                                                                                <asp:LinkButton ID="btnItemStats" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnItemStats_Click">
                                                                                                               -->
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
                                                                                                                <asp:CheckBox runat="server" ID="chkAllSupplier" AutoPostBack="True" OnCheckedChanged="chkAllSupplier_CheckedChanged"></asp:CheckBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                                <asp:Label runat="server" Text="All" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1 padding0">
                                                                                                                <asp:LinkButton ID="btnSupplier" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnSupplier_Click">
                                                                                                               -->
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
                                                                                                            <div class="col-md-3 labelText1 paddingLeft0">
                                                                                                                <asp:Label runat="server">Request By</asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-sm-3 col-md-3 padding0">
                                                                                                                <asp:TextBox runat="server" ID="txtReqto" OnTextChanged="txtReqto_TextChanged" CssClass="form-control"></asp:TextBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                                <asp:LinkButton ID="btnReqto" CausesValidation="false" runat="server" OnClick="btnReqto_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                            <div class="col-md-3 labelText1  paddingLeft0">
                                                                                                                Age Days From
                                                                                                            </div>
                                                                                                            <div class="col-md-2 padding0 ">
                                                                                                                <asp:TextBox runat="server" onpaste="return false" ID="txtAgeFrom" CssClass="form-control" onkeypress="return isDays(event,this)"></asp:TextBox>
                                                                                                            </div>

                                                                                                        </div>
                                                                                                    </div>

                                                                                                    <div class="row">
                                                                                                        <div class="col-md-12">

                                                                                                            <div class="col-md-3 labelText1 paddingLeft0">
                                                                                                                <asp:Label runat="server">Dispatch Loc</asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-sm-3 col-md-3 padding0">
                                                                                                                <asp:TextBox runat="server" ID="txtReqfrom" OnTextChanged="txtReqfrom_TextChanged" CssClass="form-control"></asp:TextBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                                <asp:LinkButton ID="btnReqfrom" CausesValidation="false" runat="server" OnClick="btnReqfrom_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>

                                                                                                            <div class="col-md-3 labelText1">
                                                                                                                To
                                                                                                            </div>
                                                                                                            <div class="col-md-2 padding0">
                                                                                                                <asp:TextBox runat="server" onpaste="return false" ID="txtAgeTo" CssClass="form-control" onkeypress="return isDays(event,this)"></asp:TextBox>
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
                                                                                                            <div class="col-md-2 labelText1 paddingLeft0">
                                                                                                                <asp:Label runat="server">Customer</asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-sm-3 col-md-3 padding0">
                                                                                                                <asp:TextBox runat="server" ID="txtcustormer" CssClass="form-control"></asp:TextBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                                <asp:LinkButton ID="lbtcussearch" CausesValidation="false" runat="server" OnClick="lbtcussearch_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <%-----------------------------------------------%>
                                                                                                    <div class="row">
                                                                                                        <div class="col-md-12">
                                                                                                            <div class="col-md-2 labelText1 paddingLeft0">
                                                                                                                <asp:Label runat="server">UserID</asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-sm-3 col-md-3 padding0">
                                                                                                                <asp:TextBox runat="server" ID="txtUser" CssClass="form-control"></asp:TextBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                                <asp:LinkButton ID="lbtusrsearch" CausesValidation="false" runat="server" OnClick="lbtusrsearch_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <%---------------------------------------------------------%>
                                                                                                    <!--new coding start -->
                                                                                                    <div class="row">
                                                                                                        <div class="col-md-12">
                                                                                                            <div class="col-sm-1">
                                                                                                                <asp:RadioButton Text="" ID="radTotRec" GroupName="radDelType" runat="server" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 padding0">
                                                                                                                <asp:Label Text="Total Rec." runat="server" />
                                                                                                            </div>

                                                                                                            <div class="col-sm-1">
                                                                                                                <asp:RadioButton Text="" ID="radPartRec" GroupName="radDelType" runat="server" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 padding0">
                                                                                                                <asp:Label Text="Part Rec." runat="server" />
                                                                                                            </div>

                                                                                                            <div class="col-sm-1">
                                                                                                                <asp:RadioButton Text="" ID="radNotRec" GroupName="radDelType" runat="server" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 padding0">
                                                                                                                <asp:Label Text="Not Rec." runat="server" />
                                                                                                            </div>

                                                                                                        </div>
                                                                                                    </div>


                                                                                                    <div class="row">
                                                                                                        <div class="col-md-12 ">
                                                                                                            <div class="col-md-4 labelText1 padding0">
                                                                                                                <asp:Label runat="server">Aging Group</asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-md-3 padding0">
                                                                                                                <asp:DropDownList runat="server" ID="DropDownListAging" CssClass="form-control">
                                                                                                                    <asp:ListItem Value="0" Text="--Select--" />
                                                                                                                </asp:DropDownList>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>

                                                                                                    <!--new coding end -->
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
                                                                                            <div class="panel panel-body" style="height: 180px; padding-left: 2px;">
                                                                                                <div class="row">
                                                                                                    <div class="col-sm-12">

                                                                                                        <div class="col-md-9">
                                                                                                            <div class="row">
                                                                                                                <div class="col-sm-12 col-md-12" style="padding-left: 3px; padding-right: 3px;">
                                                                                                                    <div class="col-sm-3 col-md-2 labelText1 padding0">
                                                                                                                        <asp:Label runat="server">Cat. 1</asp:Label>
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-3 col-md-4 padding0">
                                                                                                                        <asp:TextBox runat="server" ID="txtCat1" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtCat1_TextChanged"></asp:TextBox>
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
                                                                                                                    <div class="col-sm-1 col-md-1 padding0">
                                                                                                                        <asp:LinkButton ID="btnCat1" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnCat1_Click">
                                                                                                               -->
                                                                                                                        </asp:LinkButton>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="row">
                                                                                                                <div class="col-sm-12 col-md-12" style="padding-left: 3px; padding-right: 3px;">
                                                                                                                    <div class="col-sm-3 col-md-2 labelText1 padding0">
                                                                                                                        <asp:Label runat="server">Cat. 2</asp:Label>
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-3 col-md-4 padding0">
                                                                                                                        <asp:TextBox runat="server" ID="txtCat2" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtCat2_TextChanged"></asp:TextBox>
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
                                                                                                                    <div class="col-sm-1 col-md-1 padding0">
                                                                                                                        <asp:LinkButton ID="btnCat2" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnCat2_Click">
                                                                                                               -->
                                                                                                                        </asp:LinkButton>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="row">
                                                                                                                <div class="col-sm-12 col-md-12" style="padding-left: 3px; padding-right: 3px;">
                                                                                                                    <div class="col-sm-3 col-md-2 labelText1 padding0">
                                                                                                                        <asp:Label runat="server">Cat. 3</asp:Label>
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-3 col-md-4 padding0">
                                                                                                                        <asp:TextBox runat="server" ID="txtCat3" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtCat3_TextChanged"></asp:TextBox>
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
                                                                                                                    <div class="col-sm-1 col-md-1 padding0">
                                                                                                                        <asp:LinkButton ID="btnCat3" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnCat3_Click">
                                                                                                               -->
                                                                                                                        </asp:LinkButton>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="row">
                                                                                                                <div class="col-sm-12 col-md-12" style="padding-left: 3px; padding-right: 3px;">
                                                                                                                    <div class="col-sm-3 col-md-2 labelText1 padding0">
                                                                                                                        <asp:Label runat="server">Cat. 4</asp:Label>
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-3 col-md-4 padding0">
                                                                                                                        <asp:TextBox runat="server" ID="txtCat4" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtCat4_TextChanged"></asp:TextBox>
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
                                                                                                                    <div class="col-sm-1 col-md-1 padding0">
                                                                                                                        <asp:LinkButton ID="btnCat4" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnCat4_Click">
                                                                                                               -->
                                                                                                                        </asp:LinkButton>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="row">
                                                                                                                <div class="col-sm-12 col-md-12" style="padding-left: 3px; padding-right: 3px;">
                                                                                                                    <div class="col-sm-3 col-md-2 labelText1 padding0">
                                                                                                                        <asp:Label runat="server">Cat. 5</asp:Label>
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-3 col-md-4 padding0">
                                                                                                                        <asp:TextBox runat="server" ID="txtCat5" CssClass="form-control" OnTextChanged="txtCat5_TextChanged"></asp:TextBox>
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
                                                                                                                    <div class="col-sm-1 col-md-1 padding0">
                                                                                                                        <asp:LinkButton ID="btnCat5" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnCat5_Click">
                                                                                                               -->
                                                                                                                        </asp:LinkButton>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="row">
                                                                                                                <div class="col-md-12 " style="padding-left: 3px; padding-right: 3px;">
                                                                                                                    <div class="col-sm-3 col-md-2 labelText1 padding0">
                                                                                                                        <asp:Label runat="server">Code</asp:Label>
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-3 col-md-4 padding0">
                                                                                                                        <asp:TextBox runat="server" ID="txtItemCode" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtItemCode_TextChanged"></asp:TextBox>
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
                                                                                                                    <div class="col-sm-1 col-md-1 padding0">
                                                                                                                        <asp:LinkButton ID="btnItemCode" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnItemCode_Click">
                                                                                                               -->
                                                                                                                        </asp:LinkButton>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="row">
                                                                                                                <div class="col-md-12" style="padding-left: 3px; padding-right: 3px;">
                                                                                                                    <div class="col-sm-3 col-md-2 labelText1 padding0">
                                                                                                                        <asp:Label runat="server">Brand</asp:Label>
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-3 col-md-4 padding0">
                                                                                                                        <asp:TextBox runat="server" ID="txtBrand" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtBrand_TextChanged"></asp:TextBox>
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
                                                                                                                    <div class="col-sm-1 col-md-1 padding0">
                                                                                                                        <asp:LinkButton ID="btnBrand" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnBrand_Click">
                                                                                                               -->
                                                                                                                        </asp:LinkButton>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="row">
                                                                                                                <div class="col-md-12" style="padding-left: 3px; padding-right: 3px;">
                                                                                                                    <div class="col-sm-3 col-md-2 labelText1 padding0">
                                                                                                                        <asp:Label runat="server">Model</asp:Label>
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-3 col-md-4 padding0">
                                                                                                                        <asp:TextBox runat="server" AutoPostBack="true" ID="txtModel" CssClass="form-control" OnTextChanged="txtModel_TextChanged"></asp:TextBox>
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
                                                                                                                    <div class="col-sm-1 col-md-1 padding0">
                                                                                                                        <asp:LinkButton ID="btnModel" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnModel_Click">
                                                                                                               -->
                                                                                                                        </asp:LinkButton>
                                                                                                                    </div>
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
                                                                                                <div class="row">
                                                                                                    <br />
                                                                                                    <div class="col-sm-7">
                                                                                                        <div class="panel panel-default padding0">
                                                                                                            <div class="panel panel-body padding0">
                                                                                                                <div class="row paddingtopbottom0">
                                                                                                                    <div class="col-sm-1">
                                                                                                                        <asp:RadioButton Text="" ID="radExcess" GroupName="radexsht" runat="server" />
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-2 padding0">
                                                                                                                        <asp:Label Text="Excess" runat="server" />
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-1">
                                                                                                                        <asp:RadioButton Text=" " ID="radShort" GroupName="radexsht" runat="server" />
                                                                                                                    </div>

                                                                                                                    <div class="col-sm-2 padding0">
                                                                                                                        <asp:Label Text="Short" runat="server" />
                                                                                                                    </div>


                                                                                                                    <div class="col-sm-1">
                                                                                                                        <asp:RadioButton Text=" " ID="radBoth" GroupName="radexsht" runat="server" Checked="true" />
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-2 padding0">
                                                                                                                        <asp:Label Text="Both" runat="server" />
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="col-sm-5 paddingLeft0">
                                                                                                        <div class="panel panel-default padding0">
                                                                                                            <div class="panel panel-body padding0">
                                                                                                                <div class="row paddingtopbottom0">
                                                                                                                    <div class="col-sm-1">
                                                                                                                        <asp:RadioButton Text="" ID="radInv" OnCheckedChanged="radInv_Click" AutoPostBack="true" Checked="true" GroupName="radinvsale" runat="server" />
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-2 padding0">
                                                                                                                        <asp:Label Text="Inventory" runat="server" />
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-1">
                                                                                                                        <asp:RadioButton Text="" OnCheckedChanged="radSale_Click" AutoPostBack="true" ID="radSale" GroupName="radinvsale" runat="server" />
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-2 padding0">
                                                                                                                        <asp:Label Text="Sales" runat="server" />
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">

                                                                                                    <div class="col-sm-12">
                                                                                                        <div class="panel panel-default padding0">
                                                                                                            <div class="panel panel-body padding0">
                                                                                                                <div class="row paddingtopbottom0">
                                                                                                                    <div class="col-sm-1">
                                                                                                                        <asp:RadioButton GroupName="radwise" Text="" ID="radlocwise" Checked="true" runat="server" />
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-1 padding0">
                                                                                                                        <asp:Label Text="Location wise" runat="server" />
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-1">
                                                                                                                        <asp:RadioButton GroupName="radwise" Text="" ID="radcatwise" runat="server" />
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-1 padding0">
                                                                                                                        <asp:Label Text="Category wise" runat="server" />
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-1">
                                                                                                                        <asp:RadioButton GroupName="radwise" Text="" ID="radsubcatwise" runat="server" />
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-2 padding0">
                                                                                                                        <asp:Label Text="Cat/S cat wise" runat="server" />
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-1">
                                                                                                                        <asp:RadioButton GroupName="radwise" Text="" ID="radstatuswise" runat="server" />
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-1 padding0">
                                                                                                                        <asp:Label Text="Status wise" runat="server" />
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-1">
                                                                                                                        <asp:RadioButton GroupName="radwise" Text="" ID="raditembrandwise" runat="server" />
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-1 padding0">
                                                                                                                        <asp:Label Text="Item/Brand Wise" runat="server" />
                                                                                                                    </div>
                                                                                                                </div>

                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>

                                                                                                </div>

                                                                                                <div class="row">

                                                                                                    <div class="col-sm-12">
                                                                                                        <div class="panel panel-default padding0">
                                                                                                            <div class="panel panel-body padding0">
                                                                                                                <div class="row paddingtopbottom0">
                                                                                                                    <div class="col-sm-1">
                                                                                                                        <asp:RadioButton GroupName="radresstatus" Text="" ID="radrebond" runat="server" />
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-2 padding0">
                                                                                                                        <asp:Label Text="ReBond" runat="server" />
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-1">
                                                                                                                        <asp:RadioButton GroupName="radresstatus" Text="" ID="radexbond" runat="server" />
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-2 padding0">
                                                                                                                        <asp:Label Text="ExBond" runat="server" />
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-1">
                                                                                                                        <asp:RadioButton GroupName="radresstatus" Text="" ID="radBOI" runat="server" />
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-2 padding0">
                                                                                                                        <asp:Label Text="BOI" runat="server" />
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-1">
                                                                                                                        <asp:RadioButton GroupName="radresstatus" Text="" ID="radbondall" runat="server" />
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-2 padding0">
                                                                                                                        <asp:Label Text="All" runat="server" />
                                                                                                                    </div>
                                                                                                                </div>

                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>

                                                                                                </div>
                                                                                                <div class="row">

                                                                                                    <div class="col-sm-12">
                                                                                                        <div class="panel panel-default padding0">
                                                                                                            <div class="panel panel-body padding0">
                                                                                                                <div class="row paddingtopbottom0">
                                                                                                                    <div class="col-sm-1">
                                                                                                                        <asp:RadioButton GroupName="raditemact" Text="" ID="raditmact" runat="server" />
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-2 padding0">
                                                                                                                        <asp:Label Text="Active" runat="server" />
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-1">
                                                                                                                        <asp:RadioButton GroupName="raditemact" Text="" ID="raditminact" runat="server" />
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-2 padding0">
                                                                                                                        <asp:Label Text="Inactive" runat="server" />
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-1">
                                                                                                                        <asp:RadioButton GroupName="raditemact" Text="" ID="radallitm" Checked="true" runat="server" />
                                                                                                                    </div>
                                                                                                                    <div class="col-sm-2 padding0">
                                                                                                                        <asp:Label Text="All" runat="server" />
                                                                                                                    </div>
                                                                                                                </div>

                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>

                                                                                                </div>

                                                                                                <div class="row">
                                                                                                    <div class="col-sm-1">
                                                                                                    </div>
                                                                                                    <div class=" col-sm-1 padding0">
                                                                                                        <asp:CheckBox ID="chkWithSerial" runat="server" AutoPostBack="True" />

                                                                                                    </div>
                                                                                                    <div class="col-sm-3 padding0">
                                                                                                        <asp:Label Text="With Serials" runat="server" />
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
                                                                        <div class="col-sm-12" style="padding-left: 1px; padding-right: 1.5px;">
                                                                            <div class="col-md-12 padding0">
                                                                                <div class="panel panel-default padding0">
                                                                                    <div class="panel panel-body" style="height: 108px;">
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
