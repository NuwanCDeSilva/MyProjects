<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="CustomClearEntryUpdate.aspx.cs" Inherits="FastForward.SCMWeb.View.Additional.CustomClearEntryUpdate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="<%=Request.ApplicationPath%>Css/jquery-ui.theme.min.css" rel="stylesheet" />
    <link href="<%=Request.ApplicationPath%>Css/jquery-ui.min.css" rel="stylesheet" />
    <script>
        function ConfProcess() {
            var selectedvalueOrd = confirm("Do you want to update ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfClear() {
            var selectedvalueOrd = confirm("Do you want to clear ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
    </script>
    <script>
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
    <style>
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

        .panel {
            padding-top: 0px;
            padding-bottom: 0px;
            margin-bottom: 0px;
            margin-top: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="upMain">
                <ProgressTemplate>
                    <div class="divWaiting">
                        <asp:Label ID="lblWait" runat="server"
                            Text="Please wait... " />
                        <asp:Image ID="imgWait" runat="server"
                            ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdatePanel runat="server" ID="upMain">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-default">
                                <div class="panel panel-heading">
                                    <strong><b>Customs Cleared Entry Update</b></strong>
                                    <asp:Label Text="" Visible="false" ID="lblWarStus" runat="server" />
                                </div>
                                <div class="panel panel-body padding0">
                                    <div class="row labelText1">
                                        <div class="col-sm-12">
                                            <div class="panel panel-default">
                                                <div class="col-sm-12">
                                                    <div class="col-sm-10">

                                                    </div>
                                                    <div class="col-sm-2">
                                                        <div class="buttonRow">
                                                            <asp:LinkButton ID="btnClear" runat="server" CssClass="floatRight" OnClientClick="return ClearConfirm();" OnClick="lbtnClear_Click">
                                                                <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row labelText1">
                                        <div class="col-sm-12">
                                            <div class="panel panel-default">
                                                <div class="panel panel-body padding0">
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-3 padding0">
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-4 labelText1 padding0 ">
                                                                        Document #
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight0">
                                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtEntryNo" AutoPostBack="true"
                                                                            OnTextChanged="txtEntryNo_TextChanged" Style="text-transform: uppercase" />
                                                                    </div>
                                                                    <div class="col-sm-1 padding3">
                                                                        <%-- <asp:LinkButton ID="lbtnSeEntryNo" runat="server" OnClick="lbtnSeEntryNo_Click">
                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>--%>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-4 labelText1 padding0">
                                                                        Entry #
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight0">
                                                                        <asp:TextBox runat="server" MaxLength="30" CssClass="form-control" ID="txtEntryDesc" AutoPostBack="true"
                                                                            OnTextChanged="txtEntryDesc_TextChanged" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-4 labelText1 padding0">
                                                                        Entry Date
                                                                    </div>
                                                                    <div class="col-sm-4 paddingRight0">
                                                                        <asp:TextBox runat="server" Enabled="false" ID="txtDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 padding3">
                                                                        <asp:LinkButton ID="btnDate" runat="server" CausesValidation="false">
                                                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDate"
                                                                            PopupButtonID="btnDate" Format="dd/MMM/yyyy">
                                                                        </asp:CalendarExtender>
                                                                    </div>
                                                                    <div class="col-sm-1">
                                                                        <asp:Button OnClientClick="return ConfProcess();"  Text="Update" ID="lbtnUpdateEntry" OnClick="lbtnUpdateEntry_Click" runat="server" />
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
                                        <div class="col-sm-12">
                                            <div class="panel panel-default">
                                                <div class="panel panel-heading">
                                                    <strong><b>Excel Data</b></strong>
                                                </div>
                                                <div class="panel panel-body padding0">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <div class="col-sm-6 padding0">
                                                                <div style="height: 400px; overflow-y: auto;">
                                                                    <asp:GridView ID="dgvExcel" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                        EmptyDataText="No data found..." AutoGenerateColumns="False"
                                                                        PagerStyle-CssClass="cssPager">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Document#">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblcuh_doc_no" Text='<%# Bind("cuh_doc_no") %>' runat="server" Width="100%" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="80px" />
                                                                                <HeaderStyle Width="80px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Entry # ">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblcuh_cusdec_entry_no" Text='<%# Bind("cuh_cusdec_entry_no") %>' runat="server" Width="100%" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="80px" />
                                                                                <HeaderStyle Width="80px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Entry Date">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblcuh_cusdec_entry_dt" Visible='<%# !Eval("cuh_cusdec_entry_dt","{0:dd/MMM/yyyy}").ToString().Equals("01/Jan/0001")%>' 
                                                                                        Text='<%# Bind("cuh_cusdec_entry_dt","{0:dd/MMM/yyyy}") %>' runat="server" Width="100%" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="80px" />
                                                                                <HeaderStyle Width="80px" />
                                                                            </asp:TemplateField>

                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <div class="row labelText1">
                                                                    <div class="col-sm-12">
                                                                        <asp:Button Text="Excel Upload" ID="btnExcelDataUpload" OnClick="btnExcelDataUpload_Click" runat="server" />
                                                                    </div>
                                                                </div>
                                                               <%-- <div class="row labelText1">
                                                                    <div class="col-sm-12">
                                                                        <asp:Button Text="Entry Data Update" ID="btnExcelDataProcess" OnClick="btnExcelDataProcess_Click" runat="server" />
                                                                    </div>
                                                                </div>--%>
                                                                <div class="row labelText1">
                                                                    <div class="col-sm-12">
                                                                        <asp:Button OnClientClick="return ConfProcess();" Text="Process " ID="btnExcelDataUpdate" OnClick="btnExcelDataUpdate_Click" runat="server" />
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
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>
     <%-- pnl search --%>
    <asp:UpdatePanel runat="server" ID="upSearch">
        <ContentTemplate>
            <asp:Button ID="btn1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupSearch" runat="server" Enabled="True" TargetControlID="btn1"
                PopupControlID="pnlSearch" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="upPnlSerch">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait2" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait2" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:Panel runat="server" ID="pnlSearch" DefaultButton="lbtnSearch">
        <asp:UpdatePanel runat="server" ID="upPnlSerch">
            <ContentTemplate>
                <div runat="server" id="test" class="panel panel-primary" style="padding: 5px;">
                    <div class="panel panel-default" style="height: 350px; width: 700px;">
                        <div class="panel-heading" style="height: 25px;">
                            <div class="col-sm-10"></div>
                            <div class="col-sm-1">
                                <asp:LinkButton ID="btnClose" runat="server" OnClick="btnClose_Click">
                             <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row height16">
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="col-sm-4 labelText1">
                                        Search by key
                                    </div>
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy10" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                        <ContentTemplate>
                                            <div class="col-sm-6 paddingRight5">
                                                <asp:DropDownList ID="ddlSerByKey" runat="server" class="form-control" AutoPostBack="false">
                                                </asp:DropDownList>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="col-sm-6">
                                    <div class="col-sm-4 labelText1">
                                        Search by word
                                    </div>
                                    <div class="col-sm-6 paddingRight5">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtSerByKey" CausesValidation="false" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="lbtnSearch_Click"></asp:TextBox>
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
                                            <asp:AsyncPostBackTrigger ControlID="txtSerByKey" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 height16">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy8" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="dgvResult" CausesValidation="false" runat="server" AllowPaging="True"
                                                GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" ShowHeaderWhenEmpty="true"
                                                EmptyDataText="No data found..."
                                                OnPageIndexChanging="dgvResult_PageIndexChanging" OnSelectedIndexChanged="dgvResult_SelectedIndexChanged">
                                                <Columns>
                                                    <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="12" />
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
    </asp:Panel>

       <%-- pnl excel Upload --%>
     <asp:UpdatePanel ID="upExcelUpload" runat="server">
        <ContentTemplate>
            <asp:Button ID="btn10" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popupExcel" runat="server" Enabled="True" TargetControlID="btn10"
                PopupControlID="pnlExcelUpload" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlExcelUpload">
        <div class="panel panel-default height45 width700 ">
            <div class="panel panel-default">
                <div class="panel-heading height30">
                    <div class="col-sm-11">
                        Excel Upload
                    </div>
                    <div class="col-sm-1">
                        <asp:LinkButton ID="lbtnExcelUploadClose" runat="server" OnClick="lbtnExcelUploadClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                        </asp:LinkButton>
                    </div>
                    <%--<span>Commen Search</span>--%>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12 paddingLeft0 paddingRight0">
                        <div class="row height22">
                            <div class="col-sm-11">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lblExcelUploadError" runat="server" ForeColor="Red" Text="Label" Visible="false"></asp:Label>
                                        <asp:Label ID="lblExcelUploadSuccess" runat="server" ForeColor="Green" Visible="false"></asp:Label>
                                        <asp:Label ID="lblExcelUploadInfo" runat="server" ForeColor="Blue" Visible="false"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="row">
                            <asp:Panel runat="server">
                                <div class="col-sm-12">
                                    <div class="col-sm-10 paddingRight5">
                                        <asp:FileUpload ID="fileUploadExcel" runat="server" />
                                    </div>
                                    <div class="col-sm-2 paddingRight5">
                                        <asp:Button ID="lbtnUploadExcel" class="btn btn-warning btn-xs" runat="server" Text="Upload"
                                            OnClick="lbtnUploadExcel_Click" />
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
     <asp:UpdatePanel ID="UpdatePanel11" runat="server">
        <ContentTemplate>
            <asp:Button ID="btn15" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popupErro" runat="server" Enabled="True" TargetControlID="btn15"
                PopupControlID="pnlExcelErro" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server" ID="UpdatePanel12">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlExcelErro">
                <div runat="server" id="Div4" class="panel panel-primary" style="padding: 5px;">
                    <div class="panel panel-default" style="width: 1100px;">
                        <div class="panel-heading" style="height: 25px;">
                            <div class="col-sm-11 padding0">
                                <strong>Excel Incorrect Data</strong>
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel panel-body padding0">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="row buttonRow">
                                                <div class="col-sm-12">
                                                    <div class="col-sm-7">
                                                    </div>

                                                    <div class="col-sm-5 padding0">
                                                        <div class="col-sm-4">
                                                            
                                                        </div>
                                                        <div class="col-sm-4">
                                                           <%-- <asp:LinkButton ID="lbtnExcSave" OnClientClick="return ConfSave()" runat="server" OnClick="lbtnExcSave_Click">
                                                      <span class="glyphicon glyphicon-saved"  aria-hidden="true"></span>Save
                                                            </asp:LinkButton>--%>
                                                        </div>
                                                        <div class="col-sm-4 text-right">
                                                            <asp:LinkButton ID="lbtnExcClose"  runat="server" OnClick="lbtnExcClose_Click">
                                                    <span class="glyphicon glyphicon-remove"  aria-hidden="true"></span>Close
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="panel panel-default">
                                                        <div class="panel panel-body padding0">
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div style="height: 400px; overflow-y:auto;">
                                                                        <asp:GridView ID="dgvError" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                            EmptyDataText="No data found..." AutoGenerateColumns="False" 
                                                                            PagerStyle-CssClass="cssPager">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Document#">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblcuh_doc_no" Text='<%# Bind("cuh_doc_no") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="60px" />
                                                                                    <HeaderStyle Width="60px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Entry # ">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblcuh_cusdec_entry_no" Text='<%# Bind("cuh_cusdec_entry_no") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="60px" />
                                                                                    <HeaderStyle Width="60px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Entry Date">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblcuh_cusdec_entry_dt" Visible='<%# !Eval("cuh_cusdec_entry_dt").ToString().Equals("01/Jan/0001")%>' 
                                                                                            Text='<%# Bind("cuh_cusdec_entry_dt","{0:dd/MMM/yyyy}") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="50px" />
                                                                                    <HeaderStyle Width="50px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblErro" Text='<%# Bind("Tmp_ex_err") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="100px" />
                                                                                    <HeaderStyle Width="100px" />
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
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
