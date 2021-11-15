<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" CodeBehind="ContainerEnquiry.aspx.cs" Inherits="FastForward.SCMWeb.View.Enquiries.Imports.ContainerTracker" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>
        function ClearCompanyText() {
            document.getElementById('txtCompany').value = '';
        }

        function DateValid(sender, args) {
            var fromDate = Date.parse(document.getElementById('<%=txtFromDate.ClientID%>').value);
            var toDate = Date.parse(document.getElementById('<%=txtToDate.ClientID%>').value);
            if (toDate < fromDate) {
                document.getElementById('<%=txtToDate.ClientID%>').value = document.getElementById('<%=hdfCurrentDate.ClientID%>').value;
                showStickyWarningToast('Please select a valid date range !');
            }
        }

    </script>
    <script type="text/javascript">
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
    </script>
    <style>
        .panelHeight {
            height: 98px;
            overflow: auto;
            margin-top: -3px;
        }

        .panel-heading {
            height: 21px;
            padding-top: 2px;
        }

        .highlighted td {
            color: black !important;
            background-color: rgba(181, 175, 175, 0.82) !important;
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
    </style>
    <style>
        .panel {
            margin-top: 1px;
            margin-bottom: 1px;
        }

        .buttonRow {
            height: 31px;
        }

        .panel-body {
            padding-bottom: 1px;
        }

        #GHead .table.table-hover.table-striped {
            margin-bottom: 0;
        }
    </style>
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
    <div id="row" class="row">

        <div class="col-sm-12 col-md-12">
            <asp:ScriptManagerProxy ID="ScriptManagerProxy15" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Panel runat="server" DefaultButton="lbtnView">
                        <div class="panel panel-default" style="padding-right: 0px; padding-left: 3px;">
                            <asp:HiddenField ID="hdfClearData" runat="server" Value="0" />
                            <asp:HiddenField ID="hdfCurrentDate" runat="server" Value="0" />
                            <div class="row buttonRow" id="HederBtn">
                                <div class="col-sm-12 col-md-12">
                                    <div class="col-md-10"></div>
                                    <div class="col-md-2">
                                        <div class="col-md-6 paddingRight0">
                                            <asp:LinkButton ID="lbtnView" CausesValidation="false" OnClick="lbtnMainSerch_Click" runat="server" CssClass="floatRight"> 
                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>View </asp:LinkButton>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:LinkButton ID="LinkButton1" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="return ClearConfirm()" OnClick="lbtnClear_Click">   
                                <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-heading" style="padding-bottom: 0px;">
                                <strong><b>Container Tracker</b></strong>
                            </div>
                            <div class="panel-body" style="padding-left: 0px; padding-right: 2px; padding-top: 0px; margin-top: -1px;">

                                <div class="row">
                                    <div class="col-sm-12 col-md-12">
                                        <div class="col-sm-3 col-md-4 " style="padding-right: 3px; padding-left: 0px; padding-bottom: 0px; padding-top: 0px;">
                                            <div class="col-sm-6 col-md-6" style="padding-right: 3px; padding-left: 0px;">
                                                <div class="panel panel-default">
                                                    <div class="panel-body" style="padding-bottom: 6px; padding-top: 0px;">
                                                        <%-- <asp:Panel runat="server" ID="Panel1" DefaultButton="lbtnMainSerch">--%>
                                                        <div class="col-md-12 padding0">
                                                            <div class="col-md-12 ">
                                                                <div class="row">
                                                                    <div class="col-md-7 labelText1 padding0">
                                                                        <asp:Label runat="server">Container #</asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-7 padding0">
                                                                        <asp:TextBox runat="server" ID="txtContainer" CausesValidation="false" AutoPostBack="false" CssClass="form-control dtpicker"
                                                                            Enabled="true" OnTextChanged="txtContainer_TextChanged1"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-md-1">
                                                                        <asp:LinkButton ID="lbtnSeContainer" CausesValidation="false" runat="server" OnClick="lbtnSeContainer_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-md-1 paddingright0">
                                                                        <asp:CheckBox Checked="true" ID="chkAllContainer" Class="height22" runat="server" AutoPostBack="true" Enabled="true" OnCheckedChanged="chkAllContainer_CheckedChanged" />
                                                                    </div>
                                                                    <div class="col-md-1 paddingLeft0">
                                                                        <asp:Label runat="server">All</asp:Label>
                                                                    </div>

                                                                </div>
                                                            </div>

                                                        </div>
                                                        <%--</asp:Panel>--%>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6 col-md-6" style="padding-right: 3px; padding-left: 0px;">
                                                <div class="panel panel-default">
                                                    <div class="panel-body" style="padding-bottom: 6px; padding-top: 0px; he">
                                                        <%--<asp:Panel runat="server" ID="Panel2" DefaultButton="lBtnContainer">--%>
                                                        <div class="col-md-12 padding0">
                                                            <div class="col-md-12 ">
                                                                <div class="row">
                                                                    <div class="col-md-7 labelText1 padding0">
                                                                        <asp:Label runat="server">B/L </asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-7 padding0">
                                                                        <asp:TextBox runat="server" ID="txtBlNo" CausesValidation="false" AutoPostBack="false" CssClass="form-control"
                                                                            Enabled="true" OnTextChanged="txtBlNo_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-md-1">
                                                                        <asp:LinkButton ID="lbtnSeBiNo" CausesValidation="false" runat="server" OnClick="lbtnSeBiNo_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-md-1 paddingright0">
                                                                        <asp:CheckBox Checked="true" ID="chkAllBlNo" Class="height22" runat="server" AutoPostBack="true" Enabled="true" OnCheckedChanged="chkAllBlNo_CheckedChanged" />
                                                                    </div>
                                                                    <div class="col-md-1 paddingLeft0">
                                                                        <asp:Label runat="server">All</asp:Label>
                                                                    </div>

                                                                </div>
                                                            </div>

                                                        </div>
                                                        <%--</asp:Panel>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-9 col-md-8" style="padding-right: 0px; padding-left: 1px;">
                                            <div class="panel panel-default">
                                                <div class="panel-body" style="padding-bottom: 4px; padding-top: 0px;">
                                                    <div class="row">
                                                        <div class="col-sm-12 col-md-12 ">
                                                            <div class="col-sm-12 col-md-12 padding0">
                                                                <div class="col-sm-12 col-md-12">
                                                                    <div class="col-sm-4 col-md-4 padding0">
                                                                        <div class="col-sm-6 col-md-6 paddingLeft0">
                                                                            <div class="row">
                                                                                
                                                                                <div class="col-sm-6">
                                                                                    <asp:Label runat="server">BL Date</asp:Label>
                                                                                </div>
                                                                                <div class="col-sm-1 ">
                                                                                    <asp:RadioButton Checked="true" GroupName="dates" ID="radbl" runat="server" AutoPostBack="true" />
                                                                                </div>
                                                                                <div class="col-sm-12">
                                                                                    <asp:Label runat="server">From</asp:Label>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-sm-12 col-md-12 padding0">
                                                                                    <div class="col-sm-9 col-md-9 padding0">
                                                                                        <asp:TextBox runat="server" ID="txtFromDate" CausesValidation="false" AutoPostBack="false" CssClass="form-control"
                                                                                            Enabled="true"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-sm-1 col-md-1 paddingLeft0" style="margin-left: 3px; margin-right: 5px;">
                                                                                        <asp:LinkButton ID="lbtnFromDate" CausesValidation="false" runat="server">
                                                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"  ></span>
                                                                                        </asp:LinkButton>
                                                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate"
                                                                                            PopupButtonID="lbtnFromDate" Format="dd/MMM/yyyy">
                                                                                        </asp:CalendarExtender>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-6 col-md-6 paddingLeft0">
                                                                            <div class="row">
                                                                                <div class="col-sm-6">
                                                                                    <asp:Label runat="server">ETA Date</asp:Label>
                                                                                </div>
                                                                                <div class="col-sm-1 ">
                                                                                    <asp:RadioButton  GroupName="dates" ID="radeta" runat="server" AutoPostBack="true" />
                                                                                </div>
                                                                                <div class="col-sm-10">
                                                                                    <asp:Label runat="server">To</asp:Label>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-sm-12 col-md-12 padding0">
                                                                                    <div class="col-sm-9 col-md-9 padding0">
                                                                                        <asp:TextBox runat="server" ID="txtToDate" CausesValidation="false" AutoPostBack="false" CssClass="form-control"
                                                                                            Enabled="true"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-sm-1 col-md-1 paddingLeft0" style="margin-left: 3px">
                                                                                        <asp:LinkButton ID="lbtnToDate" CausesValidation="false" runat="server">
                                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"  ></span>
                                                                                        </asp:LinkButton>
                                                                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate" OnClientDateSelectionChanged="DateValid"
                                                                                            PopupButtonID="lbtnToDate" Format="dd/MMM/yyyy">
                                                                                        </asp:CalendarExtender>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-8 col-md-8">
                                                                        <div class="col-sm-4 col-md-4 padding0">
                                                                            <div class="row">
                                                                                <div class="col-sm-12 padding0">
                                                                                    <div class="col-sm-3 col-md-3 padding0 labelText1">
                                                                                        <asp:Label runat="server">Agent </asp:Label>
                                                                                    </div>

                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-sm-12 padding0">
                                                                                    <div class="col-sm-6 col-md-6 padding0">
                                                                                        <asp:TextBox runat="server" ID="txtAgent" CausesValidation="false" AutoPostBack="false" CssClass="form-control"
                                                                                            Enabled="true" OnTextChanged="txtAgent_TextChanged"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-sm-1 col-md-1 padding0" style="margin-left: 3px">
                                                                                        <asp:CheckBox Checked="true" ID="chkAllAgent" Class="height22" runat="server" AutoPostBack="true" Enabled="true" OnCheckedChanged="chkAllAgent_CheckedChanged" />
                                                                                    </div>
                                                                                    <div class="col-sm-1 col-md-1 padding0 labelText1">
                                                                                        <asp:Label runat="server">All</asp:Label>
                                                                                    </div>
                                                                                    <div class="col-sm-3 col-md-3 padding0">
                                                                                        <asp:LinkButton ID="lbtnAgent" CausesValidation="false" runat="server" OnClick="lbtnAgent_Click">
                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4 col-md-4 padding0">
                                                                            <div class="row">
                                                                                <div class="col-sm-12 labelText1 padding0">
                                                                                    <asp:Label runat="server">Container Type </asp:Label>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-sm-12 padding0">
                                                                                    <div class="col-sm-6 col-md-6 padding0">
                                                                                        <asp:TextBox runat="server" ID="txtContainerType" CausesValidation="false" AutoPostBack="false" CssClass="form-control"
                                                                                            Enabled="true" OnTextChanged="txtContainerType_TextChanged"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-sm-1 col-md-1 padding0" style="margin-left: 3px">
                                                                                        <asp:CheckBox Checked="true" ID="chkAllContType" Class="height22" runat="server" AutoPostBack="true" Enabled="true" OnCheckedChanged="chkAllContType_CheckedChanged" />
                                                                                    </div>
                                                                                    <div class="col-sm-1 col-md-1 padding0">
                                                                                        <asp:Label runat="server">All</asp:Label>
                                                                                    </div>
                                                                                    <div class="col-sm-3 col-md-3 padding0">
                                                                                        <asp:LinkButton ID="lbtnContainerType" CausesValidation="false" runat="server" OnClick="lbtnContainerType_Click">
                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4 col-md-4 padding0">
                                                                            <div class="row">
                                                                                <div class="col-sm-12 padding0">
                                                                                    <div class="col-sm-2 padding0 labelText1">
                                                                                        <asp:Label runat="server">Company </asp:Label>
                                                                                    </div>


                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-sm-12 padding0">
                                                                                    <div class="col-sm-6 col-md-6 padding0">
                                                                                        <asp:TextBox runat="server" ID="txtCompany" CausesValidation="false" AutoPostBack="false" CssClass="form-control"
                                                                                            Enabled="true" OnTextChanged="txtCompany_TextChanged"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-sm-1 col-md-1 padding0" style="margin-left: 3px">
                                                                                        <asp:CheckBox Checked="true" ID="chkAllCompany" Class="height22" runat="server" AutoPostBack="true" Enabled="true" OnCheckedChanged="chkAllCompany_CheckedChanged" />
                                                                                    </div>
                                                                                    <div class="col-sm-1 col-md-1 padding0 labelText1">
                                                                                        <asp:Label runat="server">All</asp:Label>
                                                                                    </div>
                                                                                    <div class="col-sm-1 col-md-1 padding0">
                                                                                        <asp:LinkButton ID="lbtnCompnay" CausesValidation="false" runat="server" OnClick="lbtnCompnay_Click">
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
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-md-12">
                                        <div class="col-sm-6 col-md-6 paddingLeft0" style="padding-right: 2px;">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    <strong>Container List</strong>
                                                </div>
                                                <div class="panel panel-heading" style="padding-left: 0px; padding-right: 0px; padding-bottom: 0px; padding-top: 0px; height: 21px; margin-bottom: 0px;">
                                                    <table>
                                                        <tr style="">
                                                            <th scope="col" style="width: 154px; padding-left: 5px; padding-top: 2px;">Container #</th>
                                                            <th scope="col" style="width: 185px; padding-top: 2px; text-align: left;">Container Type</th>
                                                            <th scope="col" style="width: 200px; padding-left: 3px; padding-top: 2px;">Description</th>
                                                             <th scope="col" style="width: 200px; padding-left: 3px; padding-top: 2px;">BL #</th>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="panel-body panelHeight">
                                                    <asp:GridView ShowHeader="False" ID="dgvContainerDetails" CssClass="table table-hover table-striped" runat="server" GridLines="None" PagerStyle-CssClass="cssPager"
                                                        EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                        <EditRowStyle BackColor="MidnightBlue" />
                                                        <EmptyDataTemplate>
                                                            <table class="table table-hover table-striped" border="0" style="border-collapse: collapse;">
                                                                <tbody>
                                                                    <tr>
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
                                                            <asp:BoundField HeaderText="" DataField="ibc_desc">
                                                                <ItemStyle Width="150px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="" DataField="ibc_tp">
                                                                <ItemStyle Width="185px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="" DataField="mct_desc">

                                                                <HeaderStyle Width="280px" />
                                                            </asp:BoundField>
                                                              <asp:BoundField HeaderText="" DataField="ib_doc_no">

                                                                <HeaderStyle Width="100px" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <PagerStyle CssClass="cssPager"></PagerStyle>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-md-6 paddingRight0" style="padding-left: 2px;">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    <strong>Container Summary</strong>
                                                </div>
                                                <div class="panel panel-heading" style="padding-left: 3px; padding-right: 0px; padding-bottom: 0px; padding-top: 0px; height: 21px; margin-bottom: 0px;">
                                                    <table>
                                                        <tr style="">
                                                            <th scope="col" style="width: 163px; padding-left: 4px;">Container Type</th>
                                                            <th scope="col" style="width: 210px;">Description</th>
                                                            <th scope="col" style="width: 100px; text-align: right">No Of Container</th>
                                                             
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="panel-body panelHeight">
                                                    <asp:GridView ShowHeader="false" ID="dgvContainerSummary" CssClass="table table-hover table-striped" runat="server" GridLines="None" PagerStyle-CssClass="cssPager" EmptyDataText="No data found..." AutoGenerateColumns="False" AllowPaging="True" PageSize="5" OnPageIndexChanging="dgvContainerSummary_PageIndexChanging" OnPreRender="dgvContainerSummary_PreRender">
                                                        <EditRowStyle BackColor="MidnightBlue" />
                                                        <EmptyDataTemplate>
                                                            <table class="table table-hover table-striped" border="0" style="border-collapse: collapse;">
                                                                <tbody>
                                                                    <tr>
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
                                                            <asp:BoundField HeaderText="Container Type" DataField="ibc_tp">
                                                                <ItemStyle Width="161px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Description" DataField="mct_desc">
                                                                <ItemStyle Width="300px" />
                                                            </asp:BoundField>
                                                             
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTotal" Style="width: 250px;" CssClass="gridHeaderAlignRight" runat="server" Text='<%# Bind("TypeCount") %>' Visible="true"></asp:Label>
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
                                <div class="row">
                                    <div class="col-sm-12 col-md-12" style="width: 100%;">
                                        <div class="panel panel-default paddingLeftRight0" style="width: 100%;">
                                            <div class="panel-heading">
                                                <strong>B/L Details</strong>
                                            </div>
                                            <div class="panel panel-default" style="width: 100%;">
                                                <%-- <div class="panel panel-heading" style="padding-left: 0px; padding-right: 0px; padding-bottom: 0px; padding-top: 0px; height: 21px; margin-bottom: 0px;">
                                                   <table>
                                                        <tr>
                                                            <th scope="col" style="width: 36px; padding-left: 36px; padding-top: 2px;"></th>
                                                            <th scope="col" style="width: 138px; padding-left: 5px; padding-top: 2px;">B/L #</th>
                                                            <th scope="col" style="width: 137px; padding-left: 5px; padding-top: 2px;">Document #</th>
                                                            <th scope="col" style="width: 81px; padding-left: 5px; padding-top: 2px;">BL Date</th>
                                                            <th scope="col" style="width: 143px; padding-left: 5px; padding-top: 2px;">Supplier</th>
                                                            <th scope="col" style="width: 79px; padding-left: 5px; padding-top: 2px;">Consignee</th>
                                                            <th scope="col" style="width: 78px; padding-left: 5px; padding-top: 2px;">Declerant</th>
                                                            <th scope="col" style="width: 93px; padding-left: 5px; padding-top: 2px;">Agent</th>
                                                            <th scope="col" style="width: 95px; padding-left: 5px; padding-top: 2px;">Total Packages
                                                                 
                                                                <asp:Label ID="lblUMesur" runat="server" Text=""></asp:Label>
                                                                
                                                            </th>
                                                            <th scope="col" style="width: 115px; padding-left: 16px; padding-top: 2px;">Packing List #</th>
                                                            <th scope="col" style="width: 96px; padding-left: 5px; padding-top: 2px;">Vessel</th>
                                                            <th scope="col" style="width: 59px; padding-left: 5px; padding-top: 2px;">Voyaye</th>
                                                            <th scope="col" style="width: 114px; padding-left: 5px; padding-top: 2px; text-align:right;">Total
                                                                    
                                                                <asp:Label ID="lblUsd" runat="server" Text=""></asp:Label>
                                                                
                                                            </th>
                                                        </tr>
                                                    </table>
                                                </div>--%>
                                                <div class="panel panel-body" style="width: 100%;">
                                                    <div>
                                                        <div class="GHead" id="GHead"></div>
                                                        <div style="height: 100px; overflow: auto">
                                                            <asp:GridView ID="dgvBLDetails" ShowHeader="true" CssClass="table table-hover table-striped"
                                                                runat="server" GridLines="None"
                                                                AutoGenerateColumns="False" OnSelectedIndexChanged="dgvBLDetails_SelectedIndexChanged" OnRowDataBound="dgvBLDetails_RowDataBound">
                                                                <EditRowStyle BackColor="MidnightBlue" />
                                                                <EmptyDataTemplate>
                                                                    <table class="table table-hover table-striped" border="0" style="border-collapse: collapse;">
                                                                        <tbody>

                                                                            <tr>
                                                                                <td>No records found.
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                            </tr>
                                                                    </table>
                                                                </EmptyDataTemplate>
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="&nbsp;">
                                                                        <ItemTemplate>
                                                                            <asp:UpdatePanel runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:LinkButton ID="lbtnSelect" Width="30px" runat="server" OnClick="lbtnSelect_Click">
                                                                            Select
                                                                                    </asp:LinkButton>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </ItemTemplate>
                                                                        <%-- <HeaderStyle Width="15px" />
                                                                        <ItemStyle Width="15px" />--%>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="B/L #">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblno" runat="server" Text='<%# Bind("IB_BL_NO") %>' Visible="true"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <%-- <HeaderStyle Width="80px" />
                                                                        <ItemStyle Width="80px" />--%>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Document #">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDoc" runat="server" Text='<%# Bind("IB_DOC_NO") %>' Visible="true"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <%-- <HeaderStyle Width="100px" />
                                                                        <ItemStyle Width="100px" />--%>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="B/L Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblblno" runat="server" Text='<%# Bind("IB_BL_DT","{0:dd/MMM/yyyy}") %>' Visible="true"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <%--<HeaderStyle Width="80px" CssClass="gridHeaderAlignLeft" />
                                                                        <ItemStyle Width="80px" CssClass="gridHeaderAlignLeft" />--%>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="LC #">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbibi_fin_no" runat="server"  Text='<%# Bind("ibi_fin_no") %>' Visible="true"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <%--<HeaderStyle Width="80px" CssClass="gridHeaderAlignLeft" />
                                                                        <ItemStyle Width="80px" CssClass="gridHeaderAlignLeft" />--%>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Bank #">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbif_bank_cd" runat="server" Text='<%# Bind("if_bank_cd") %>' Visible="true"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <%--<HeaderStyle Width="80px" CssClass="gridHeaderAlignLeft" />
                                                                        <ItemStyle Width="80px" CssClass="gridHeaderAlignLeft" />--%>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Supplier">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSupCd"  runat="server" Text='<%# Bind("Supplier") %>' Visible="true"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <%--<HeaderStyle Width="120px" />
                                                                    <ItemStyle Width="120px" />--%>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Consigneer">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblConCd" runat="server" Text='<%# Bind("Consigner") %>' Visible="true"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <%--<HeaderStyle Width="100px" />
                                                                    <ItemStyle Width="100px" />--%>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Declarant">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDecCd" runat="server" Text='<%# Bind("Declarant") %>' Visible="true"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <%--<HeaderStyle Width="100px" />
                                                                    <ItemStyle Width="100px" />--%>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Agent">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAgeCd" runat="server"  Text='<%# Bind("Agent") %>' Visible="true"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <%--<HeaderStyle Width="100px" />
                                                                    <ItemStyle Width="100px" />--%>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Tot.Pack">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPkgTot" runat="server"  Style="text-align: right" Text='<%# Eval("IB_TOT_PKG","{0:N2}") %>' Visible="true"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                        <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText=" ">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSupplier" runat="server" Text='<%# Bind("Supplier") %>' Visible="false"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <%-- <HeaderStyle Width="10px" />
                                                                        <ItemStyle Width="10px" />--%>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText=" ">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCode" runat="server" Text='<%# Bind("IB_BL_NO") %>' Visible="false"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <%-- <HeaderStyle Width="10px" />
                                                                        <ItemStyle Width="10px" />--%>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPackingList" runat="server" Text='<%# Bind("IB_PACK_LST_NO") %>' Visible="false"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <%-- <HeaderStyle Width="120px" />
                                                                    <ItemStyle Width="120px" />--%>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Vessel">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("IB_VESSEL_NO") %>' Visible="true"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <%--<HeaderStyle Width="100px" />
                                                                    <ItemStyle Width="100px" />--%>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Voyage">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label2" runat="server"  Text='<%# Bind("IB_VOYAGE") %>' Visible="true"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <%-- <HeaderStyle Width="100px" />
                                                                    <ItemStyle Width="100px" />--%>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Total">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTotal"  Style="text-align: right" runat="server" Text='<%# Eval("ib_tot_bl_amt","{0:N2}") %>' Visible="true"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblConsigner" runat="server" Text='<%# Bind("Consigner") %>' Visible="false"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDeclarant" runat="server" Text='<%# Bind("Declarant") %>' Visible="false"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAgent" runat="server" Text='<%# Bind("Agent") %>' Visible="false"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTemp" runat="server" Style="text-align: right" Visible="false"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                        <ItemStyle HorizontalAlign="Right" />
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
                                    <div class="col-sm-12 col-md-12 paddingLeftRight0">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                <strong>Item details</strong>
                                            </div>
                                            <%--<div class="panel panel-heading paddingLeft0">
                                               <table>
                                                    <tr>
                                                        <th scope="col" style="width:170px;padding-left:6px;">Item</th>
                                                        <th scope="col" style="width:250px;">Description</th>
                                                        <th scope="col" style="width:150px;">Model</th>
                                                        <th scope="col" style="width:52px;">Part #</th>
                                                        <th scope="col" style="width:127px;">Brand</th>
                                                        <th scope="col" style="width:82px;">Qty</th>
                                                        <th scope="col" style="width:254px;">Financing Doc</th>
                                                        <th scope="col" style="width:60px;">PI #</th>
                                                    </tr>
                                                </table>
                                            </div>--%>

                                            <div class="">
                                                <div>
                                                    <div class="GHead1" id="GHead1"></div>
                                                    <div class="" style="height: 100px; overflow: auto;">
                                                        <asp:GridView ID="dgvItemList" CssClass="dgvItemList table table-hover table-striped"
                                                            runat="server" GridLines="None"
                                                            AutoGenerateColumns="False">

                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Item Code">
                                                                    <ItemTemplate>
                                                                        <asp:Label CssClass="lbllItem" ID="lblItem" runat="server" Text='<%# Bind("ibi_itm_cd") %>' Visible="true"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <%-- <HeaderStyle  Width="100px"/>
                                                                 <ItemStyle Width="100px"/>--%>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Description">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDesc" Width="120px" runat="server" Text='<%# Bind("mi_shortdesc") %>' Visible="true"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Model">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblModel" Width="80px" runat="server" Text='<%# Bind("mi_model") %>' Visible="true"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Part #">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPart" Width="80px" runat="server" Text='<%# Bind("mi_part_no") %>' Visible="true"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Brand">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBrand" Width="80px" runat="server" Text='<%# Bind("mi_brand") %>' Visible="true"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Qty">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblQty" Width="100px" runat="server" Text='<%# Bind("ibi_qty","{0:N}") %>' Visible="true"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                    <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAdType" runat="server" Text='' Width="20px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Financing Doc">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFinancingDoc" Width="150px" runat="server" Text='<%# Bind("ibi_fin_no") %>' Visible="true"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PI #">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFinancingDoc" Width="100px" runat="server" Text='<%# Bind("ibi_pi_no") %>' Visible="true"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>

                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

    </div>
    <asp:UpdatePanel runat="server" ID="update">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupSearch" runat="server" Enabled="True" TargetControlID="Button3"
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
    <%--<asp:UpdatePanel runat="server" ID="update">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupSearch" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="SearchPanel" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="SearchPanel" DefaultButton="lbtnSearch">
        <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
            <ContentTemplate>
                <div runat="server" id="divSearch" class="panel panel-primary Mheight">
                    <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
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
                                        <asp:DropDownList ID="cmbSearchbykey" runat="server" class="form-control" AutoPostBack="false">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2 labelText1">
                                        Search by word
                                    </div>
                                    <asp:Label ID="lblSearchType" runat="server" Text="" Visible="False"></asp:Label>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="col-sm-4 paddingRight5">
                                                <asp:TextBox ID="txtSearchbyword" CausesValidation="false" class="form-control" AutoPostBack="true" runat="server"></asp:TextBox>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
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
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
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
               
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>--%>
    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script>
        Sys.Application.add_load(initSomething);
        function initSomething() {
            if (typeof jQuery == 'undefined') {
                alert('jQuery is not loaded');
            }
            $('.dgvBLDetails').ready(function () {
                var gridHeader = $('#<%=dgvBLDetails.ClientID%>').clone(true); // Here Clone Copy of Gridview with style
                $(gridHeader).find("tr:gt(0)").remove(); // Here remove all rows except first row (header row)
                var v = 0;
                $('#<%=dgvBLDetails.ClientID%> tbody tr td').each(function (i) {
                    // Here Set Width of each th from gridview to new table(clone table) th 
                    if (v < $(this).width()) {
                        v = $(this).width();
                    }

                    $("th:nth-child(" + (i + 1) + ")", gridHeader).css('width', ($(this).width()).toString() + "px");
                });
                $('.GHead').append(gridHeader);
                $('.GHead').css('position', 'inherit');
                $('.GHead').css('width', '99%');
                $('.GHead').css('top', $('#<%=dgvBLDetails.ClientID%>').offset().top);
                jQuery('#BodyContent_dgvBLDetails tbody').children('tr').eq(1).remove();

            });

            $('.dgvItemList').ready(function () {
                console.log((v).toString());
            });
        }
    </script>
    <script>    
       
    </script>
</asp:Content>


