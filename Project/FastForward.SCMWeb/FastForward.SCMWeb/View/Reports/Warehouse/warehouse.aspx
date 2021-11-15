﻿<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="warehouse.aspx.cs" Inherits="FastForward.SCMWeb.View.Reports.Warehouse.warehouse" %>

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

            jQuery('#BodyContent_frmtime').timepicker({
                format: ' hh:ii'
            });
            jQuery('#BodyContent_totime').timepicker({
                format: ' hh:ii'
            });

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

        .marginleft {
            margin-left: 3px;
        }
    </style>
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
        function PrintedConfirm() {
            if (document.getElementById('BodyContent_chkprintRecord').checked && (document.getElementById('BodyContent_rad03').checked == true || document.getElementById('BodyContent_rad04').checked == true)) {
                var selectedvalue = confirm("Do you want to mark the selected data as printed?");
                if (selectedvalue) {
                    document.getElementById('<%=hdfmarkPrinted.ClientID %>').value = "1";
                } else {
                    document.getElementById('<%=hdfmarkPrinted.ClientID %>').value = "0";
                }
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
                <%-- <asp:LinkButton ID="btnDownloadfile" Text="Download" OnClick="btnDownloadfile_Click" runat="server" CssClass="exceldown">
                                                                    Download
    </asp:LinkButton>--%>
            </div>
            <div class="col-md-6">
                <asp:RadioButton ID="rbpdf" Text="PDF" Checked="true" CssClass="marginleft" runat="server" GroupName="importreports"></asp:RadioButton>
                <asp:RadioButton ID="rbexel" Text="Excel" runat="server" CssClass="marginleft" GroupName="importreports"></asp:RadioButton>
                <asp:RadioButton ID="rbexeldata" Text="Excel Data Only" CssClass="marginleft" runat="server" GroupName="importreports"></asp:RadioButton>
            </div>

        </div>

    </div>
    <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <div class="col-sm-12 col-md-12" style="margin-left: 3px;">
                <div class="row">
                    <asp:HiddenField ID="hdfClearData" runat="server" />
                    <asp:HiddenField ID="hdfCurrentDate" runat="server" />
                    <asp:HiddenField ID="hdfShowPopUp" Value="0" runat="server" />
                    <asp:HiddenField ID="hdfmarkPrinted" Value="0" runat="server" />
                    <asp:Panel runat="server" ID="pnl1" DefaultButton="lbtnView">
                        <div class="col-sm-12 col-md-12 panel panel-default">
                            <div class="row buttonRow" id="HederBtn">
                                <div class="col-sm-12 col-md-12">
                                    <div class="col-md-10">
                                        <h1 style="font-size: small; margin-top: 2px">Warehouse Report</h1>
                                    </div>
                                    <div class="col-md-1 paddingRight0">
                                        <asp:LinkButton ID="lbtnView" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lbtnView_Click" OnClientClick="return PrintedConfirm()"> 
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
                                                        <asp:Label ID="Label1" runat="server" Text="Item Pick Plan"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px;">
                                                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="rad02_CheckedChanged" ID="rad02" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label2" runat="server" Text="Stock verification"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px;">
                                                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="rad03_CheckedChanged" ID="rad03" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label4" runat="server" Text="Dispatch Request Summary"></asp:Label>
                                                    </div>
                                                </div>


                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px;">
                                                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="rad04_CheckedChanged" ID="rad04" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label5" runat="server" Text="Dispatch Request Details"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px;">
                                                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="rad05_CheckedChanged" ID="rad05" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label6" runat="server" Text="Item Dispatch Details"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px;">
                                                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="rad06_CheckedChanged" ID="rad06" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label7" runat="server" Text="Inter Transfer Request Report"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px;">
                                                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="rad07_CheckedChanged" ID="rad07" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label8" runat="server" Text="Approved MRN Number's"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12 col-md-12">
                                                    <div class="col-sm-1 col-md-1 height20" style="padding-left: 5px; padding-right: 3px;">
                                                        <asp:RadioButton AutoPostBack="true" OnCheckedChanged="rad08_CheckedChanged" ID="rad08" GroupName="grpReport" runat="server" />
                                                    </div>
                                                    <div class="col-sm-11 col-md-11 labelText1" style="padding-left: 3px; padding-right: 3px">
                                                        <asp:Label ID="Label9" runat="server" Text="Courier Slip Dailly Summary"></asp:Label>
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
                                                                                        <div class="panel panel-body" style="height: 365px;">
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
                                                                                                            <asp:CheckBox Checked="true" runat="server" ID="chkAllDocSubType" AutoPostBack="True" OnCheckedChanged="chkAllDocSubType_CheckedChanged"></asp:CheckBox>
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
                                                                                                            <asp:TextBox runat="server" ID="txtDocNo" CssClass="form-control" OnTextChanged="txtDocNo_TextChanged"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="lbtnSeDocNo" CausesValidation="false" runat="server" OnClick="lbtnSeDocNo_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                            <asp:CheckBox Checked="true" Visible="false" runat="server" ID="chkAllDocNo" AutoPostBack="True" OnCheckedChanged="chkAllDocNo_CheckedChanged"></asp:CheckBox>
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
                                                                                                            <asp:CheckBox Checked="true" runat="server" ID="chkAllDirNo" AutoPostBack="True" OnCheckedChanged="chkAllDirNo_CheckedChanged"></asp:CheckBox>
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
                                                                                                        <%--  <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                            <asp:LinkButton ID="btnotherloc" CausesValidation="false" runat="server" OnClick="btnotherloc_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>--%>

                                                                                                        <asp:UpdatePanel>
                                                                                                            <ContentTemplate>

                                                                                                                <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                                    <asp:LinkButton ID="btnOtherLocation" CausesValidation="false" runat="server" OnClick="lbtnOtherLocation_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                                    </asp:LinkButton>
                                                                                                                </div>

                                                                                                            </ContentTemplate>
                                                                                                        </asp:UpdatePanel>

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
                                                                                                            <asp:Label runat="server">Doc Status
                                                                                                            </asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-md-3 padding0">
                                                                                                            <asp:DropDownList runat="server" ID="ddlDocStatus" CssClass="form-control">
                                                                                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                                                                                <asp:ListItem Value="X">X - ALL</asp:ListItem>
                                                                                                                <asp:ListItem Value="F">F - FINISHED/CONFIRMED</asp:ListItem>
                                                                                                                <asp:ListItem Value="P">P - PENDING</asp:ListItem>
                                                                                                                <asp:ListItem Value="A">A - APPROVED</asp:ListItem>
                                                                                                                <asp:ListItem Value="C">C - CANCEL</asp:ListItem>
                                                                                                                <asp:ListItem Value="R">R - REJECTED</asp:ListItem>
                                                                                                                <asp:ListItem Value="T">T - TRANSFERED</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-md-12">
                                                                                                        <div class="col-sm-3 col-md-3 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Record Update</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 height22" style="padding-right: 50px">
                                                                                                            <asp:CheckBox Checked="true" runat="server" ID="chkprintRecord" AutoPostBack="True" Enabled="true"></asp:CheckBox>
                                                                                                        </div>
                                                                                                        <div class="col-md-3 labelText1">
                                                                                                            <asp:Label runat="server">Courier Type</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-md-3">
                                                                                                            <asp:DropDownList runat="server" ID="ddlServiceBy" CssClass="form-control" Enabled="false" Width="90px">
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
                                                                                                                <asp:CheckBox ID="chkWithCostWIP" runat="server" AutoPostBack="true" Enabled="true" />
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
                                                                                                            <div class="col-sm-2 col-md-2 labelText1 paddingLeft0">
                                                                                                                <asp:Label runat="server">Import</asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-sm-3 col-md-3" style="margin-top: 3px;">
                                                                                                                <asp:CheckBox runat="server" ID="chkwithbincd" Text="With Bin"></asp:CheckBox>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-sm-12 col-md-12 padding0" style="height: 25px;">
                                                                                                        <div class="form-group input-group lab col-md-12">
                                                                                                            <div class="col-sm-1 col-md-1" style="margin-top: 3px;">
                                                                                                                <asp:RadioButton AutoPostBack="true" runat="server" ID="radmodwise" OnCheckedChanged="radmodwise_CheckedChanged" GroupName="modwise"></asp:RadioButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 col-md-2 labelText1 paddingLeft0">
                                                                                                                <asp:Label runat="server">Model Wise</asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1" style="padding-left: 10px; margin-top: 3px;">
                                                                                                                <asp:RadioButton AutoPostBack="true" runat="server" ID="radmodwisesumm" OnCheckedChanged="radmodwisesumm_CheckedChanged" GroupName="modwise"></asp:RadioButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-2 col-md-2 labelText1 paddingLeft0">
                                                                                                                <asp:Label runat="server">Model Summery</asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1" style="margin-top: 3px;">
                                                                                                                <asp:RadioButton AutoPostBack="true" runat="server" ID="radrequestwise" OnCheckedChanged="radrequestwise_CheckedChanged" GroupName="modwise"></asp:RadioButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-4 col-md-2 labelText1 paddingLeft0">
                                                                                                                <asp:Label runat="server">Request Wise</asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1" style="margin-top: 3px;">
                                                                                                                <asp:RadioButton AutoPostBack="true" runat="server" ID="radreqwisesumm" OnCheckedChanged="radreqwisesumm_CheckedChanged" GroupName="modwise"></asp:RadioButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-4 col-md-2 labelText1 paddingLeft0">
                                                                                                                <asp:Label runat="server">Request Summery</asp:Label>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class=" row" style="padding-top:5px">
                                                                                                    <div class="col-sm-12">
                                                                                                        <div class="col-sm-1 padding0" style="width:15px">
                                                                                                            <asp:CheckBox Checked="false" runat="server" ID="chkExportExcel" AutoPostBack="false"></asp:CheckBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 paddingLeft0">
                                                                                                            <asp:Label runat="server" Text="Export Excel" />
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-2 labelText1 paddingLeft10 ">
                                                                                                            <asp:Label runat="server">Slip Frm #</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-1 padding0 " style="padding-left:5px">
                                                                                                            <asp:TextBox AutoPostBack="true" runat="server" ID="txtFromNo"
                                                                                                                CssClass="form-control" Enabled="true" Width="70px"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 49px;">
                                                                                                            <asp:LinkButton ID="lknCourier" CausesValidation="false" runat="server" OnClick="lknCourier_Click">
                                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-2 labelText1 paddingLeft20 ">
                                                                                                            <asp:Label runat="server">Slip To #</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-1 paddingLeft5 ">
                                                                                                            <asp:TextBox AutoPostBack="true" runat="server" ID="txtToNo"
                                                                                                                CssClass="form-control" Enabled="true" Width="70px"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1" style="padding-left: 51px;">
                                                                                                            <asp:LinkButton ID="lknCourierTo" CausesValidation="false" runat="server" OnClick="lknCourierTo_Click">
                                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <asp:Label runat="server" Text="All" Visible="false"/>
                                                                                                        </div>
                                                                                                        <%--style="padding-left: 10px; padding-right: 3px;"--%>
                                                                                                        <div class="col-sm-1 col-md-1 height22">
                                                                                                            <asp:CheckBox Checked="true" runat="server" ID="chkCouAll" AutoPostBack="True" Visible="false"></asp:CheckBox>
                                                                                                        </div>
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
                                                                                                            <asp:DropDownList runat="server" AutoPostBack="true" ID="ddlYear" CssClass="form-control" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                                                                                            </asp:DropDownList>
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 labelText1 padding0">
                                                                                                        </div>
                                                                                                        <div class="col-sm-2 col-md-2 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Month</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-4 col-md-4 padding0">
                                                                                                            <asp:DropDownList runat="server" AutoPostBack="true" ID="ddlMonth" CssClass="form-control" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                                                                                                                <asp:ListItem Value="Jan">January</asp:ListItem>
                                                                                                                <asp:ListItem Value="Feb">February</asp:ListItem>
                                                                                                                <asp:ListItem Value="Mar">March</asp:ListItem>
                                                                                                                <asp:ListItem Value="Apr">April</asp:ListItem>
                                                                                                                <asp:ListItem Value="May">May</asp:ListItem>
                                                                                                                <asp:ListItem Value="Jun">June</asp:ListItem>
                                                                                                                <asp:ListItem Value="Jul">July</asp:ListItem>
                                                                                                                <asp:ListItem Value="Aug">August</asp:ListItem>
                                                                                                                <asp:ListItem Value="Sep">September</asp:ListItem>
                                                                                                                <asp:ListItem Value="Oct">October</asp:ListItem>
                                                                                                                <asp:ListItem Value="Nov">November</asp:ListItem>
                                                                                                                <asp:ListItem Value="Dec">December</asp:ListItem>
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
                                                                                                            <div class="col-sm-2 col-md-2 labelText1 paddingLeft0">
                                                                                                                <asp:Label runat="server">Clr Date</asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1">
                                                                                                                <asp:CheckBox runat="server" ID="chkistime" OnCheckedChanged="chkistime_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1 labelText1 paddingLeft0">
                                                                                                                <asp:Label runat="server">Time</asp:Label>
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
                                                                                                            <asp:TextBox runat="server" placeholder="DD/MMM/yyyy" ID="txtFromDate" CssClass="txtFromDate form-control" />
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <%-- <asp:LinkButton ID="lbtnFromDate" CausesValidation="false" runat="server">
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
                                                                                                            <asp:TextBox runat="server" placeholder="DD/MMM/yyyy" ID="txtToDate" CssClass="txtToDate form-control" OnTextChanged="txtToDate_TextChanged" />
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <%--<asp:LinkButton ID="lbtnToDate" CausesValidation="false" runat="server">
                                                                                    <span class="glyphicon glyphicon-calendar" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDate" OnClientDateSelectionChanged="DateValid"
                                                                                                                PopupButtonID="lbtnToDate" Format="dd/MMM/yyyy">
                                                                                                            </asp:CalendarExtender>--%>
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
                                                                                                        <div class="col-sm-3 col-md-3 padding0" style="padding-left: 5px; padding-right: 2px;">
                                                                                                            <asp:TextBox runat="server" placeholder="DD/MMM/yyyy" ID="txtAsAt" CssClass="txtAsAt form-control" />
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <%-- <asp:LinkButton ID="lbtnAsAt" CausesValidation="false" runat="server">
                                                                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtAsAt"
                                                                                                                PopupButtonID="lbtnAsAt" Format="dd/MMM/yyyy">
                                                                                                            </asp:CalendarExtender>--%>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-2 labelText1 padding0">
                                                                                                            <asp:Label runat="server">Expiry Date</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-3 padding0" style="padding-left: 5px; padding-right: 2px;">
                                                                                                            <asp:TextBox runat="server" placeholder="DD/MMM/yyyy" ID="txtExDate" CssClass="txtExDate form-control" />
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <%--<asp:LinkButton ID="lbtnExDate" CausesValidation="false" runat="server">
                                                                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtExDate"
                                                                                                                PopupButtonID="lbtnExDate" Format="dd/MMM/yyyy">
                                                                                                            </asp:CalendarExtender>--%>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>

                                                                                                <div class="row" style="padding-top: 3px;">
                                                                                                    <div class="col-sm-12 col-md-12">
                                                                                                        <div class="col-sm-3 col-md-2 labelText1 padding0">
                                                                                                            <asp:Label runat="server">From Time</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-3 padding0" style="padding-left: 5px; padding-right: 2px;">
                                                                                                            <asp:TextBox runat="server" type="time" placeholder="hh:mm" ID="frmtime" CssClass="form-control" />
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <%-- <asp:LinkButton ID="lbtnAsAt" CausesValidation="false" runat="server">
                                                                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtAsAt"
                                                                                                                PopupButtonID="lbtnAsAt" Format="dd/MMM/yyyy">
                                                                                                            </asp:CalendarExtender>--%>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-2 labelText1 padding0">
                                                                                                            <asp:Label runat="server">To Time</asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 col-md-3 padding0" style="padding-left: 5px; padding-right: 2px;">
                                                                                                            <asp:TextBox runat="server" placeholder="hh:mm" ID="totime" CssClass="form-control" />
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                            <%--<asp:LinkButton ID="lbtnExDate" CausesValidation="false" runat="server">
                                                                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"  ></span>
                                                                                                            </asp:LinkButton>
                                                                                                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtExDate"
                                                                                                                PopupButtonID="lbtnExDate" Format="dd/MMM/yyyy">
                                                                                                            </asp:CalendarExtender>--%>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>

                                                                                                <div class="row " style="padding-top: 3px; padding-bottom: 0px;">
                                                                                                    <div class="col-sm-12 col-md-12 padding0">
                                                                                                        <div class="form-group input-group lab" style="margin-top: 4px; margin-bottom: 4px;">
                                                                                                            <div class="col-sm-1 col-md-1">
                                                                                                                <asp:RadioButton AutoPostBack="true" Checked="true" runat="server" ID="rbtnExpDte" GroupName="modwise"></asp:RadioButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-3 col-md-3 labelText1 paddingLeft0">
                                                                                                                <asp:Label runat="server">Exp Date</asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1">
                                                                                                                <asp:RadioButton AutoPostBack="true" runat="server" ID="rbtnCreDte" GroupName="modwise"></asp:RadioButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-4 col-md-4 labelText1 paddingLeft0">
                                                                                                                <asp:Label runat="server">Create Date</asp:Label>
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
                                                                                <div class="col-md-12">
                                                                                    <div class="row">
                                                                                        <div class="col-md-12">
                                                                                            <div class="panel panel-default">
                                                                                                <div class="panel-body" style="height: 210px;">
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
                                                                                                                <asp:CheckBox Checked="true" runat="server" ID="chkAllStat" AutoPostBack="True" OnCheckedChanged="chkAllStat_CheckedChanged"></asp:CheckBox>
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
                                                                                                                <asp:CheckBox Checked="true" runat="server" ID="chkAllSupplier" AutoPostBack="True" OnCheckedChanged="chkAllSupplier_CheckedChanged"></asp:CheckBox>
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
                                                                                                        <div class="col-md-12 ">
                                                                                                            <div class="col-sm-3 col-md-3 labelText1 padding0 ">
                                                                                                                <asp:Label runat="server">Route Det</asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-sm-3 col-md-4 padding0 ">
                                                                                                                <asp:TextBox AutoPostBack="true" runat="server" ID="txtroute" CssClass="form-control" Enabled="true" OnTextChanged="txtroute_TextChanged">
                                                                                                                </asp:TextBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                                <asp:LinkButton ID="lbtnroute" CausesValidation="false" runat="server" OnClick="lbtnroute_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1 height22" style="padding-left: 10px; padding-right: 3px;">
                                                                                                                <asp:CheckBox Checked="true" runat="server" ID="chkallroute" AutoPostBack="True" OnCheckedChanged="chkallroute_CheckedChanged"></asp:CheckBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1 paddingLeft0">
                                                                                                                <asp:Label runat="server" Text="All" />
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1 padding0">
                                                                                                                <asp:LinkButton ID="btnroute" CausesValidation="false" runat="server" ForeColor="Black" OnClick="btnroute_Click">
                                                                                                               <span class="glyphicon glyphicon-arrow-right" style="color:purple;font-size:small"></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="row">
                                                                                                        <div class="col-md-12 ">
                                                                                                            <div class="col-sm-3 col-md-3 labelText1 padding0 ">
                                                                                                                <asp:Label runat="server">Color</asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-sm-3 col-md-4 padding0 ">
                                                                                                                <asp:TextBox AutoPostBack="true" runat="server" ID="txtColor" CssClass="form-control" Enabled="true">
                                                                                                                </asp:TextBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                                <asp:LinkButton ID="lbtnColor" CausesValidation="false" runat="server" OnClick="lbtnColor_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>

                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="row">
                                                                                                        <div class="col-md-12 ">
                                                                                                            <div class="col-sm-3 col-md-3 labelText1 padding0 ">
                                                                                                                <asp:Label runat="server">Approved Doc</asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-sm-3 col-md-4 padding0 ">
                                                                                                                <asp:TextBox AutoPostBack="true" runat="server" ID="txtappdocno" CssClass="form-control" Enabled="true">
                                                                                                                </asp:TextBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                                <asp:LinkButton ID="lbtnappdocno" CausesValidation="false" runat="server" OnClick="lbtnappdocno_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>

                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="row">
                                                                                                        <div class="col-md-12 ">
                                                                                                            <div class="col-sm-3 col-md-3 labelText1 padding0 ">
                                                                                                                <asp:Label runat="server">Req app type</asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-sm-3 col-md-4 padding0 ">
                                                                                                                <asp:TextBox AutoPostBack="true" runat="server" ID="txtdoctypenew" CssClass="form-control" Enabled="true">
                                                                                                                </asp:TextBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 col-md-1" style="padding-left: 3px;">
                                                                                                                <asp:LinkButton ID="lbtndoctype" CausesValidation="false" runat="server" OnClick="lbtndoctype_Click">
                                                                                                             <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
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
                                                                                                            <div class="col-md-2 labelText1  paddingLeft0">
                                                                                                                Age From
                                                                                                            </div>
                                                                                                            <div class="col-md-2 padding0 ">
                                                                                                                <asp:TextBox runat="server" onpaste="return false" ID="txtAgeFrom" CssClass="form-control" onkeypress="return isDays(event,this)"></asp:TextBox>
                                                                                                            </div>
                                                                                                            <div class="col-md-2 labelText1">
                                                                                                                Age To
                                                                                                            </div>
                                                                                                            <div class="col-md-2 padding0">
                                                                                                                <asp:TextBox runat="server" onpaste="return false" ID="txtAgeTo" CssClass="form-control" onkeypress="return isDays(event,this)"></asp:TextBox>
                                                                                                            </div>
                                                                                                            <div class="col-md-2 labelText1">
                                                                                                                Status
                                                                                                            </div>
                                                                                                            <div class="col-md-2 padding0">
                                                                                                                <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control">
                                                                                                                    <asp:ListItem Value="A">PENDING</asp:ListItem>
                                                                                                                    <asp:ListItem Value="F">APPROVED</asp:ListItem>
                                                                                                                </asp:DropDownList>
                                                                                                            </div>

                                                                                                        </div>
                                                                                                    </div>


                                                                                                    <!--<div class="row">
                                                                                                        <div class="col-md-12 ">
                                                                                                           
                                                                                                            <div class="col-md-2 labelText1  paddingLeft0">
                                                                                                                Pending
                                                                                                            </div>
                                                                                                            <div class="col-md-2 padding0">
                                                                                                                <asp:CheckBox runat="server" ID="chkPending" AutoPostBack="True" OnCheckedChanged="chkPending_CheckedChanged"></asp:CheckBox>
                                                                                                            </div>
                                                                                                            <div class="col-md-2 labelText1  paddingLeft0">
                                                                                                                Approved
                                                                                                            </div>
                                                                                                            <div class="col-md-2 padding0">
                                                                                                                <asp:CheckBox runat="server" ID="chkApproved" AutoPostBack="True" OnCheckedChanged="chkApproved_CheckedChanged"></asp:CheckBox>
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
                                                                                                    <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"  ></span>Add
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
                                                                                                    <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"  ></span>Add
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
                                                                                                    <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"  ></span>Add
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
                                                                                                    <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"  ></span>Add
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
                                                                                                    <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"  ></span>Add
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
                                                                                                    <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"  ></span>Add
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
                                                                                                    <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"  ></span>Add
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
                                                                                                    <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"  ></span>Add
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

                                                                                            </div>
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
                                                                                            <b>Route Details</b>
                                                                                        </div>
                                                                                        <div class="panel-body panelscoll2" style="height: 100px">
                                                                                            <asp:GridView ID="dgvRoute" CssClass="table table-hover table-striped bound" ShowHeader="True" runat="server" GridLines="none" PagerStyle-CssClass="csspager"
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
                                                                                                    <asp:TemplateField HeaderText="Select">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkRouteDet" runat="server" />
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle Width="10px" />
                                                                                                        <ItemStyle Width="10px" />
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Route Code">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblRoutcd" runat="server" Text='<%# Bind("frs_cd") %>' Width="80px"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle Width="80px" />
                                                                                                        <ItemStyle Width="80px" />
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Destance">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblRoutdes" runat="server" Text='<%# Bind("frs_distance") %>' Width="80px"></asp:Label>
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
                                                                                        <asp:LinkButton ID="lbtnrouteAll" CausesValidation="false" runat="server" OnClientClick="CheckAllLocation()" OnClick="lbtnrouteAll_Click">
                                                                                                <span               class="glyphicon glyphicon-record" aria-hidden="true"  ></span>All
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                    <div class="col-sm-4  ">
                                                                                        <asp:LinkButton ID="lbtnrouteNone" CausesValidation="false" runat="server" OnClick="lbtnrouteNone_Click">
                                                                                                <span class="glyphicon glyphicon-flash" aria-hidden="true"  ></span>None
                                                                                        </asp:LinkButton>
                                                                                    </div>

                                                                                    <div class="col-sm-4  ">
                                                                                        <asp:LinkButton ID="lbtnrouteClear" CausesValidation="false" runat="server" OnClick="lbtnrouteClear_Click">
                                                                                                <span class="glyphicon glyphicon-refresh" aria-hidden="true"  ></span>Clear
                                                                                        </asp:LinkButton>
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
