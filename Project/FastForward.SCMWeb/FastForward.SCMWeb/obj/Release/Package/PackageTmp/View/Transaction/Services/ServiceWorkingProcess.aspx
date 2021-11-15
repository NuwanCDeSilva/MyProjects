<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="ServiceWorkingProcess.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Services.ServiceWorkingProcess" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <link href="<%=Request.ApplicationPath%>Css/jquery-ui.theme.min.css" rel="stylesheet" />
    <link href="<%=Request.ApplicationPath%>Css/jquery-ui.min.css" rel="stylesheet" />
    <script>
        function ConfReOpen() {
            var selectedvalueOrd = confirm("Do you want to Re-Open the job ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfProcess() {
            var selectedvalueOrd = confirm("Do you want to process ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
      
        function ConfCancMrn() {
            var selectedvalueOrd = confirm("Do you need to cancel this MRN ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfAppApprove() {
            var selectedvalueOrd = confirm("Do you want to approve this MRN ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfReFix() {
            var selectedvalueOrd = confirm("Do you want to re-fix ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfOPRRemove() {
            var selectedvalueOrd = confirm("Are you sure do you want to remove ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfReturn() {
            var selectedvalueOrd = confirm("Do you want to return ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfSaveMrn() {
            var selectedvalueOrd = confirm("Do you need to process this MRN ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfStartJob() {
            var selectedvalueOrd = confirm("Do you want to start this job ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfClear() {
            var selectedvalueOrd = confirm("Are you sure do you want to clear ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfSave() {
            var selectedvalueOrd = confirm("Do you want to save ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
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
        function pageLoad(sender, args) {
            $("#<%=txtSelectedDateTime.ClientID %>").datetimepicker({ dateFormat: "dd/M/yy", timeFormat: "hh:mm tt" });
            $("#<%=txtJobCloseDt.ClientID %>").datetimepicker({ dateFormat: "dd/M/yy", timeFormat: "hh:mm tt" });
        };            

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
        /*.panel-default{
            padding-top: 0px;
            padding-bottom: 0px;
            margin-bottom: 0px;
            margin-top: 0px;
        }*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row" style="height: 500px;">
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
                                    <strong><b>Service Work In Progress</b></strong>
                                    <asp:Label Text="" Visible="false" ID="lblWarStus" runat="server" />
                                </div>
                                <div class="panel panel-body padding0">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-sm-10 padding0">
                                                <div class="panel panel-default">
                                                    <%-- <div class="panel panel-heading">
                                                        <strong><b>Parts and Metirials</b></strong>
                                                    </div>--%>
                                                    <div class="panel panel-body padding0">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-11">
                                                                    <asp:CheckBox Text="" ID="chkbulk" Visible="false" runat="server" />
                                                                </div>
                                                                <div class="col-sm-1 padding0">
                                                                    <div class="buttonRow">
                                                                        <div class="col-sm-12 padding0 text-center">
                                                                            <asp:LinkButton ID="lbtnClear" OnClick="lbtnClear_Click" CausesValidation="false" runat="server"
                                                                                OnClientClick="return ConfClear();" CssClass=""> 
                                                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-4 padding0">
                                                                    <div class="panel panel-default">
                                                                        <div class="panel panel-body">
                                                                            <div class="row" style="height:5px;">
                                                                                </div>
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-3 labelText1">
                                                                                        Job Stage
                                                                                    </div>
                                                                                    <div class="col-sm-8 paddingRight0">
                                                                                        <asp:DropDownList ID="ddlJobStage" AutoPostBack="true" OnSelectedIndexChanged="ddlJobStage_SelectedIndexChanged" runat="server" CssClass="form-control">
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-3 labelText1">
                                                                                        Job #
                                                                                    </div>
                                                                                    <div class="col-sm-8 paddingRight0">
                                                                                        <asp:TextBox AutoPostBack="true" OnTextChanged="txtJobNo_TextChanged" ID="txtJobNo" runat="server" CssClass="form-control" />
                                                                                    </div>
                                                                                    <div class="col-sm-1 padding3">
                                                                                        <asp:LinkButton ID="lbtnSeJobNo" CausesValidation="false" runat="server" OnClick="lbtnSeJobNo_Click">
                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-5 labelText1">
                                                                                        Current Stage
                                                                                    </div>
                                                                                    <div class="col-sm-7 labelText1 paddingLeft0">
                                                                                        <asp:Label Text="" ID="lblJobStage" runat="server" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-5 labelText1">
                                                                                        Schedule Start On
                                                                                    </div>
                                                                                    <div class="col-sm-7 labelText1 paddingLeft0">
                                                                                        <asp:Label Text="" ID="lblJobShdStartOn" runat="server" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-5 labelText1">
                                                                                        <asp:Button Text="Start / Open" ID="lbtnStartOpenJob" runat="server" OnClick="lbtnStartOpenJob_Click" />
                                                                                    </div>
                                                                                    <div class="col-sm-7 labelText1 paddingLeft0">
                                                                                        <asp:Label Text="" ID="lblStartOpenDate" runat="server" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-5 labelText1">
                                                                                        <asp:Button Text="Complete / Re-Open" ID="lbtnCompleteJob" OnClick="lbtnCompleteJob_Click" runat="server" />
                                                                                    </div>
                                                                                    <div class="col-sm-7 labelText1 paddingLeft0">
                                                                                        <asp:Label Text="" ID="lblCompleteDate" runat="server" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row labelText1">
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-5 labelText1">
                                                                                        Site
                                                                                    </div>
                                                                                    <div class="col-sm-7 labelText1 paddingLeft0">
                                                                                        <asp:Label Text="" ID="lblJobCategori" runat="server" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-5 labelText1">
                                                                                        Schedule End On
                                                                                    </div>
                                                                                    <div class="col-sm-7 labelText1 paddingLeft0">
                                                                                        <asp:Label Text="" ID="lblScheduleEnd" runat="server" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-5 labelText1">
                                                                                        Level Of Service
                                                                                    </div>
                                                                                    <div class="col-sm-7 labelText1 paddingLeft0">
                                                                                        <asp:Label Text="" ID="lblLevel" runat="server" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-5 labelText1">
                                                                                        Job Item Stage
                                                                                    </div>
                                                                                    <div class="col-sm-7 labelText1 paddingLeft0" style="background-color: InactiveCaption">
                                                                                        <asp:Label Text="" ID="lblStageText" runat="server" />
                                                                                    </div>
                                                                                    <asp:Label Visible="false" Text="" ID="lblJobStageNew" runat="server" />

                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-8 padding0">
                                                                    <div class="row">
                                                                        <div class="col-sm-12">
                                                                            <div class="panel panel-default">
                                                                                <div class="panel panel-body">
                                                                                    <div class="" style="height: 300px; overflow-y: scroll;">
                                                                                        <asp:GridView ID="dgvJobItem" CssClass="table table-hover table-striped"
                                                                                            ShowHeaderWhenEmpty="true" EmptyDataText="No data found..."
                                                                                            runat="server" GridLines="None" PagerStyle-CssClass="cssPager"
                                                                                            AutoGenerateColumns="False">
                                                                                            <Columns>
                                                                                                <asp:TemplateField HeaderText="">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:CheckBox Width="100%" Checked='<%#Convert.ToBoolean(Eval("_selectLine")) %>' ID="chkSelect" Text="" OnCheckedChanged="chkSelect_CheckedChanged" runat="server" />
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle Width="10px" />
                                                                                                    <HeaderStyle Width="10px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Job No" Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblJbd_jobno" Text='<%# Bind("Jbd_jobno") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Job Line" Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbljbd_jobline" Text='<%# Bind("jbd_jobline") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Item Code">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label Width="100%" ID="lbljbd_itm_cd" Text='<%# Bind("jbd_itm_cd") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle Width="100px" />
                                                                                                    <HeaderStyle Width="100px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Date" Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbldt" Text='<%# Bind("sjb_custexptdt") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Engine #">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label Width="100%" ID="lbljbd_ser1" Text='<%# Bind("jbd_ser1") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle Width="100px" />
                                                                                                    <HeaderStyle Width="100px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Warrenty #" Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbljbd_warr" Text='<%# Bind("jbd_warr") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="JBD_REGNo" Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbljbd_regno" Text='<%# Bind("jbd_regno") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Priority" Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblsjb_prority" Text='<%# Bind("sjb_prority") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="SJB_town" Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblsjb_b_town" Text='<%# Bind("sjb_b_town") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Job Category">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label Width="100%" ID="lblsc_desc" Text='<%# Bind("sc_desc") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle Width="100px" />
                                                                                                    <HeaderStyle Width="100px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="sc_direct" Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblsc_direct" Text='<%# Bind("sc_direct") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="sc_tp" Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblsc_tp" Text='<%# Bind("sc_tp") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Cus Expt Date">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label Width="100%" ID="lblsjb_custexptdt" Text='<%# Bind("sjb_custexptdt", "{0:dd/MMM/yyyy}") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle Width="6px" />
                                                                                                    <HeaderStyle Width="60px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Status" Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbljbd_act" Text='<%# Bind("jbd_act") %>' runat="server"></asp:Label>
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
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-2 padding0">
                                                <div class="panel panel-default">
                                                    <div class="panel panel-heading">
                                                        <strong><b>Parts and Metirials</b></strong>
                                                    </div>
                                                    <div class="panel panel-body padding0">
                                                        <div class="row buttonRow">
                                                            <div class="col-sm-12 text-left">
                                                                <asp:LinkButton ID="btnRequisition" OnClick="btnRequisition_Click"  CausesValidation="false" runat="server"
                                                                     CssClass=""> 
                                                                        <span class="glyphicon glyphicon-registration-mark" aria-hidden="true"></span>Requisition </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="row buttonRow">
                                                            <div class="col-sm-12 text-left">
                                                                <asp:LinkButton ID="lbtnOldPartRemove" OnClick="lbtnOldPartRemove_Click" CausesValidation="false" runat="server" CssClass=""> 
                                                                        <span class="glyphicon glyphicon-remove-circle" aria-hidden="true"></span>Old Part </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="row buttonRow">
                                                            <%--<div class="col-sm-12 text-left">
                                                                <asp:LinkButton ID="LinkButton3" OnClick="lbtnClear_Click" CausesValidation="false" runat="server"
                                                                    OnClientClick="return ConfClear();" CssClass=""> 
                                                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Stock return </asp:LinkButton>
                                                            </div>--%>
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
     <%-- pnl request create--%>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <asp:Button ID="btn3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popRequest" runat="server" Enabled="True" TargetControlID="btn3"
                PopupControlID="pnlRequest" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress4" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="upPnlRequest">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait4" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait4" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:Panel runat="server" ID="pnlRequest" DefaultButton="lbtnSearch">
        <asp:UpdatePanel runat="server" ID="upPnlRequest">
            <ContentTemplate>
                <div runat="server" id="Div2" class="panel panel-primary" style="padding: 5px;">
                    <div class="panel panel-default" style="height: 400px; width: 900px;">
                        <div class="panel-heading" style="height: 25px;">
                            <div class="col-sm-10">
                                <strong>Service Work in Progress - MRN</strong>
                            </div>
                            <div class="col-sm-2 text-right">
                                <asp:LinkButton ID="lbtnCloseRequest" runat="server" OnClick="lbtnCloseRequest_Click">
                                    <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-sm-6 padding0 labelText1">
                                        <div class="col-sm-7 padding0">
                                            <div class="col-sm-5 labelText1 padding0">
                                                Dispatch Requried
                                            </div>
                                            <div class="col-sm-5 padding0">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtDispatchRequried" AutoPostBack="true" OnTextChanged="txtDispatchRequried_TextChanged" />
                                            </div>
                                            <div class="col-sm-1 padding3">
                                                <asp:LinkButton ID="lbtnSeDispRequired" runat="server" OnClick="lbtnSeDispRequired_Click">
                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-sm-5 padding0">
                                            <div class="col-sm-2 padding0 labelText1">
                                                Date
                                            </div>
                                            <div class="col-sm-6 padding0">
                                                <asp:TextBox runat="server" Enabled="false" ID="txtRequestDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 padding3">
                                                <asp:LinkButton ID="btnDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtRequestDate"
                                                    PopupButtonID="btnDate" Format="dd/MMM/yyyy">
                                                </asp:CalendarExtender>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-sm-6 padding0">
                                        <div class="" style="font-size:15px;">
                                             <div class="col-sm-2 padding0">
                                            <asp:LinkButton ID="lbtnReqSave" OnClientClick="return ConfSaveMrn();" runat="server" OnClick="lbtnReqSave_Click">
                                                <span class="glyphicon glyphicon-save"  aria-hidden="true">Request</span>
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-2 padding0">
                                            <asp:LinkButton ID="lbtnReqApprove" OnClientClick="return ConfAppApprove();" runat="server" OnClick="lbtnReqApprove_Click">
                                                <span class="glyphicon glyphicon-saved"  aria-hidden="true">Approve</span>
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-2 padding0">
                                            <asp:LinkButton ID="lbtnReqCanc" OnClientClick="return ConfCancMrn();" runat="server" OnClick="lbtnReqCanc_Click">
                                                <span class="glyphicon glyphicon-remove"  aria-hidden="true">Cancel</span>
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-2 padding0">
                                            <asp:LinkButton ID="lbtnReqClear" runat="server" OnClick="lbtnReqClear_Click">
                                                <span class="glyphicon glyphicon-refresh"  aria-hidden="true">Clear</span>
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-3 padding0">
                                            <asp:LinkButton ID="lbtnShowMrn" runat="server" OnClick="lbtnShowMrn_Click">
                                                <span class="glyphicon glyphicon-open-file"  aria-hidden="true">Requested MRN</span>
                                            </asp:LinkButton>
                                        </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row height10">
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <%--<div class="col-sm-6 padding0">
                                        <div class="row labelText1">
                                            <div class="col-sm-12">
                                                <div class="col-sm-3 padding0">
                                                    By Estimate
                                                </div>
                                                <div class="col-sm-7 padding0">
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtEstimate" AutoPostBack="true" OnTextChanged="txtEstimate_TextChanged" />
                                                </div>
                                                <div class="col-sm-1 padding3">
                                                    <asp:LinkButton ID="lbtnSeEstimate" runat="server" OnClick="lbtnSeEstimate_Click">
                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>
                                    <div class="col-sm-6 padding0">
                                        <div class="row labelText1">
                                            <div class="col-sm-12">
                                                <div class="col-sm-2 padding0">
                                                    MRN
                                                </div>
                                                <div class="col-sm-8 padding0">
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtRequest" AutoPostBack="true" OnTextChanged="txtRequest_TextChanged" />
                                                </div>
                                                <div class="col-sm-1 padding3">
                                                    <asp:LinkButton ID="lbtnSeMrn" runat="server" OnClick="lbtnSeMrn_Click">
                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row height10">
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-sm-11 padding0">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-sm-5 padding0">
                                                <div class="col-sm-8 padding0">
                                                    Item
                                                </div>
                                                <div class="col-sm-4 padding0">
                                                    Status
                                                </div>
                                                </div>
                                                <div class="col-sm-1 padding0">
                                                    Quantity
                                                </div>
                                                <div class="col-sm-3 padding0">
                                                    Remarks
                                                </div>
                                                <div class="col-sm-3 padding0">
                                                    <div class="col-sm-6 padding0 text-right">
                                                        Ava. Qty
                                                    </div>
                                                    <div class="col-sm-6 padding0 text-right">
                                                        Free Qty
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-sm-5 padding0">
                                                    <div class="col-sm-7 padding0">
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtMrnItm" AutoPostBack="true" OnTextChanged="txtMrnItm_TextChanged" />
                                                    </div>
                                                    <div class="col-sm-1 padding3">
                                                        <asp:LinkButton ID="lbtnSeMrnItm" runat="server" OnClick="lbtnSeMrnItm_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div class="col-sm-4 padding0">
                                                        <asp:DropDownList OnSelectedIndexChanged="ddlMrnItmStus_SelectedIndexChanged" AppendDataBoundItems="true" runat="server" CssClass="form-control" ID="ddlMrnItmStus" AutoPostBack="true">
                                                            <asp:ListItem Value="0" Text="--Select--" />
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-1 padding0">
                                                    <asp:TextBox runat="server" CssClass="writeMclick text-right form-control" ID="txtMrnQty" AutoPostBack="true" OnTextChanged="txtMrnQty_TextChanged" />
                                                </div>
                                                <div class="col-sm-3 padding0">
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtMrnItmRemarks" />
                                                </div>
                                                <div class="col-sm-3 padding0">
                                                    <div class="col-sm-6 labelText1 padding0 text-right">
                                                        <asp:Label Text="0.00" CssClass="text-right" ID="lblMrnAvaQty" runat="server" />
                                                    </div>
                                                    <div class="col-sm-6 labelText1 padding0 text-right">
                                                        <asp:Label Text="0.00" CssClass="text-right" ID="lblMrnFreeQty" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-1 paddingRight0">
                                        <div class="buttonRow">
                                            <asp:LinkButton ID="lbtnMrnItmAdd" runat="server" OnClick="lbtnMrnItmAdd_Click">
                                                <span class="glyphicon glyphicon-arrow-down"  aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-sm-3 padding0">
                                        <asp:Label Text="Description :" ID="lblMrnDescr" runat="server" />
                                    </div>
                                    <div class="col-sm-3 padding0">
                                       <asp:Label Text="Model :" ID="lblMrnModel" runat="server" />
                                    </div>
                                    <div class="col-sm-3 padding0">
                                        <asp:Label Text="Brand :" ID="lblMrnBrand" runat="server" />
                                    </div>
                                    <div class="col-sm-3 padding0">
                                       <asp:Label Text="Serial Status :" ID="lblItemSubStatus" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="panel panel-default">
                                        <div class="panel panel-body">
                                            <div>
                                                <div class="" style="height: 150px; overflow-y: scroll;">
                                                    <asp:GridView ID="dgvMrnItms" CssClass="table table-hover table-striped"
                                                        ShowHeaderWhenEmpty="true" EmptyDataText="No data found..."
                                                        runat="server" GridLines="None" PagerStyle-CssClass="cssPager"
                                                        AutoGenerateColumns="False">
                                                        <Columns>
                                                           <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton Width="100%" ID="lbtnMrnGrdRemove" runat="server" OnClick="lbtnMrnGrdRemove_Click">
                                                                        <span class="glyphicon glyphicon-remove"  aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="15px" />
                                                                <ItemStyle Width="15px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton Width="100%" ID="lbtnMrnGrdEdit" runat="server" OnClick="lbtnMrnGrdEdit_Click">
                                                                        <span class="glyphicon glyphicon-edit"  aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="15px" />
                                                                <ItemStyle Width="15px" />
                                                            </asp:TemplateField>
                                                           <asp:TemplateField HeaderText="No" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItri_line_no" Text='<%# Bind("Itri_line_no") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Item Code">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItri_itm_cd" Width="100%" Text='<%# Bind("Itri_itm_cd") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                                <ItemStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Description">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMi_longdesc" Width="100%" Text='<%# Bind("Mi_longdesc") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                                <ItemStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Brand">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMi_brand" Width="100%" Text='<%# Bind("Mi_brand") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                                <ItemStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Model">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMi_Model" Width="100%" Text='<%# Bind("Mi_Model") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                                <ItemStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItri_mitm_stus" Width="100%" Text='<%# Bind("Itri_mitm_stus") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="70px" />
                                                                <ItemStyle Width="70px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Quantity">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItri_qty" Width="100%" Text='<%# Bind("Itri_qty", "{0:N2}") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="70px" CssClass="gridHeaderAlignRight" />
                                                                <ItemStyle Width="70px" CssClass="gridHeaderAlignRight"  />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Reservation" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItri_res_no" Width="100%" Text='<%# Bind("Itri_res_no") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="70px" />
                                                                <ItemStyle Width="70px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Remarks">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItri_note" Width="100%" Text='<%# Bind("Itri_note") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="70px" />
                                                                <ItemStyle Width="70px" />
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="MainItem" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblitri_mitm_cd" Width="100%" Text='<%# Bind("itri_mitm_cd") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="70px" />
                                                                <ItemStyle Width="70px" />
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
                            <div class="row height10">
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-sm-2 padding0">
                                        Remarks
                                    </div>
                                    <div class="col-sm-10">
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtMrnRemark"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <%-- pnl mrn item data --%>
    <asp:UpdatePanel runat="server" ID="UpdatePanel18">
        <ContentTemplate>
            <asp:Button ID="btnpop10" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popSavedMrn" runat="server" Enabled="True" TargetControlID="btnpop10"
                PopupControlID="pnlReqShow" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress8" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="UpdatePanel19">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait101" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait101" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:Panel runat="server" ID="pnlReqShow">
        <asp:UpdatePanel runat="server" ID="UpdatePanel19">
            <ContentTemplate>
                <div runat="server" id="Div7" class="panel panel-primary" style="padding: 5px;">
                    <div class="panel panel-default" style="height: 300px; width: 650px;">
                        <div class="panel-heading" style="height: 25px;">
                            <div class="col-sm-12">
                            <div class="col-sm-8">
                                <strong>Requested MRN</strong>
                            </div>
                            <div class="col-sm-2 padding0 text-right">
                                <asp:LinkButton ID="lbtnReqPrint" runat="server" OnClick="lbtnReqPrint_Click">
                                    <span class="glyphicon glyphicon-print"  aria-hidden="true">Print</span>
                                </asp:LinkButton>
                            </div>
                            <div class="col-sm-2 padding0 text-right">
                                <asp:LinkButton ID="lbtnCloseReq" runat="server" OnClick="lbtnCloseReq_Click">
                                    <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                                </asp:LinkButton>
                            </div>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="" style="height: 125px; overflow-y: scroll;">
                                <asp:GridView ID="dgvPrintMrn" CssClass="table table-hover table-striped"
                                    ShowHeaderWhenEmpty="true" EmptyDataText="No data found..."
                                    runat="server" GridLines="None" PagerStyle-CssClass="cssPager"
                                    AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="MRN">
                                            <ItemTemplate>
                                                <asp:Label ID="lblitr_req_no" Width="100%" Text='<%# Bind("itr_req_no") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="100px" />
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItri_itm_cd" Width="100%" Text='<%# Bind("Itri_itm_cd") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="100px" />
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Model">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMi_Model" Width="100%" Text='<%# Bind("Mi_Model") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="100px" />
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmss_desc" Width="100%" Text='<%# Bind("mss_desc") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="70px" />
                                            <ItemStyle Width="70px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItri_qty" Width="100%" Text='<%# Bind("itri_qty", "{0:N2}") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="70px" CssClass="gridHeaderAlignRight" />
                                            <ItemStyle Width="70px" CssClass="gridHeaderAlignRight" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                        </div>
                    </div>
                </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <%-- pnl start the Job --%>
     <asp:UpdatePanel ID="UpdatePanel7" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnPrintPnl" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popCompleate" runat="server" Enabled="True" TargetControlID="btnPrintPnl"
                PopupControlID="pnlComProcess" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress9" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel8">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait5" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait5" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/banners/hidden_progress_bar.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:Panel ID="pnlComProcess" runat="server" align="center">
        <div runat="server" id="Div8" class="panel panel-info height120 width250">
            <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:ScriptManagerProxy ID="ScriptManagerProxy4" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                <ContentTemplate>
                    <div class="col-sm-12 padding0">
                        <div class="panel panel-default padding0">
                            <div class="panel panel-heading text-left padding0">
                                <span>Select Date and Time</span>
                            </div>
                            <div class="panel-body padding0">
                                <div class="row">
                                    <div class="col-sm-12 text-left">
                                        <H5>Please select a date and time to start the job</H5>
                                    </div>
                                </div>
                                <div class="row height16">
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-9 paddingRight0">
                                            <asp:TextBox ID="txtSelectedDateTime" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row height16">
                                </div>
                                <div class="row">
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:Button ID="lbtnCompConf" OnClientClick="return ConfStartJob();" runat="server" Text="yes" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs"
                                            OnClick="lbtnCompConf_Click" />
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:Button ID="lbtnCompNotConf" runat="server" Text="No" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs"
                                            OnClick="lbtnCompNotConf_Click" />
                                    </div>
                                </div>
                                <div class="row height16">
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>

     <%-- pnl Job Close--%>
    <asp:UpdatePanel runat="server" ID="UpdatePanel12">
        <ContentTemplate>
            <asp:Button ID="btn522" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popJobClose" runat="server" Enabled="True" TargetControlID="btn522"
                PopupControlID="pnlJobClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress5" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="upJobClose">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait6" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait6" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:Panel runat="server" ID="pnlJobClose">
        <asp:UpdatePanel runat="server" ID="upJobClose">
            <ContentTemplate>
                <div runat="server" id="Div3" class="panel panel-primary" style="padding: 5px;">
                    <div class="panel panel-default" style="height: 550px; width: 900px;">
                        <div class="panel-heading" style="height: 25px;">
                            <div class="col-sm-10">
                                <strong>Service WIP Job Close</strong>
                            </div>
                            <div class="col-sm-2 text-right">
                                <asp:LinkButton ID="lbtnJobCloseClose" runat="server" OnClick="lbtnJobCloseClose_Click">
                                    <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-sm-8 padding0 labelText1">
                                        <div class="col-sm-5 padding0">
                                            <div class="col-sm-2 padding0 labelText1">
                                                Date
                                            </div>
                                            <div class="col-sm-8 padding0">
                                                <asp:TextBox runat="server" ID="txtJobCloseDt" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-5 padding0">
                                            <div class="col-sm-3 padding0 labelText1">
                                                Loaction
                                            </div>
                                            <div class="col-sm-4 padding0">
                                                <asp:TextBox OnTextChanged="txtAodLocation_TextChanged" AutoPostBack="true" runat="server" ID="txtAodLocation" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 padding3">
                                                <asp:LinkButton ID="lbtnSeAodLocation" runat="server" OnClick="lbtnSeAodLocation_Click">
                                    <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 padding0">
                                        <div class="" style="font-size: 15px;">
                                            <div class="col-sm-3 padding0">
                                                <asp:LinkButton ID="lbtnCloseJobView" runat="server" OnClick="lbtnCloseJobView_Click">
                                                <span class="glyphicon glyphicon-saved"  aria-hidden="true">View</span>
                                                </asp:LinkButton>
                                            </div>
                                            <div class="col-sm-5 padding0">
                                                <asp:LinkButton ID="lbtnCloseJobSave"  runat="server" OnClick="lbtnCloseJobSave_Click">
                                                <span class="glyphicon glyphicon-save"  aria-hidden="true">Job Close</span>
                                                </asp:LinkButton>
                                            </div>
                                            <div class="col-sm-3 padding0">
                                                <asp:LinkButton ID="lbtnCloseJobClear" runat="server" OnClick="lbtnCloseJobClear_Click">
                                                <span class="glyphicon glyphicon-remove"  aria-hidden="true">Clear</span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row height10">
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="panel panel-default">
                                        <div class="panel panel-heading">
                                            <strong>New Item Return</strong>
                                        </div>
                                        <div class="panel panel-body">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="col-sm-6 labelText1">
                                                        <div class="col-sm-2 padding0">
                                                            Remark
                                                        </div>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox runat="server" ID="txtRemarkNewItem" CssClass=" form-control" />
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4 labelText1">
                                                        <div class="col-sm-4 padding0">
                                                            Warehouse
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox runat="server" AutoPostBack="true" OnTextChanged="txtNewPartWarehouse_TextChanged" ID="txtNewPartWarehouse" CssClass=" form-control" />
                                                        </div>
                                                        <div class="col-sm-1 padding3">
                                                            <asp:LinkButton ID="lbtnSeWhHouseNewItmRtn" runat="server" OnClick="lbtnSeWhHouseNewItmRtn_Click">
                                                            <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-2 padding0">
                                                        <div class="buttonRow">
                                                            <asp:LinkButton OnClientClick="return ConfReturn();" ID="lbtnNPReturn" runat="server" OnClick="lbtnNPReturn_Click">
                                                                    <span  style="font-size:16px;" class="glyphicon"  aria-hidden="true">Return</span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="" style="height: 125px; overflow-y: scroll;">
                                                        <asp:GridView ID="dgvNewParts" CssClass="table table-hover table-striped"
                                                            ShowHeaderWhenEmpty="true" EmptyDataText="No data found..."
                                                            runat="server" GridLines="None" PagerStyle-CssClass="cssPager"
                                                            AutoGenerateColumns="False">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox Text="" ID="chkNewPartSelect" OnCheckedChanged="chkNewPartSelect_CheckedChanged" runat="server" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="15px" />
                                                                    <ItemStyle Width="15px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Item Code">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblITEM_CODE" Width="100%" Text='<%# Bind("ITEM_CODE") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="100px" />
                                                                    <ItemStyle Width="100px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Description">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDesc" Width="100%" Text='<%# Bind("Desc") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="100px" />
                                                                    <ItemStyle Width="100px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Status">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSTATUS_CODE" Width="100%" Text='<%# Bind("STATUS_CODE") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="100px" />
                                                                    <ItemStyle Width="100px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Status" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSTATUS_CODE" Width="100%" Text='<%# Bind("STATUS_CODE") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="100px" />
                                                                    <ItemStyle Width="100px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Status">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblITEM_STAUS" Width="100%" Text='<%# Bind("ITEM_STAUS") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="70px" />
                                                                    <ItemStyle Width="70px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Serial #">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSERIAL_NO" Width="100%" Text='<%# Bind("SERIAL_NO") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="70px" />
                                                                    <ItemStyle Width="70px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="SERIAL_ID" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSERIAL_ID" Width="100%" Text='<%# Bind("SERIAL_ID") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="70px" />
                                                                    <ItemStyle Width="70px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Qty">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblQty" Width="100%" Text='<%# Bind("Qty") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="70px" />
                                                                    <ItemStyle Width="70px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="In Date" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblInDate" Width="100%" Text='<%# Bind("InDate") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="70px" />
                                                                    <ItemStyle Width="70px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Document" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocNumber" Width="100%" Text='<%# Bind("DocNumber") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="70px" />
                                                                    <ItemStyle Width="70px" />
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
                            <div class="row height10">
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="panel panel-default">
                                        <div class="panel panel-heading">
                                            <strong>Old Parts and Additioanl items Return</strong>
                                        </div>
                                        <div class="panel panel-body">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="col-sm-6">
                                                    </div>
                                                    <div class="col-sm-4 labelText1">
                                                        <div class="col-sm-4 padding0 ">
                                                            Warehouse
                                                        </div>
                                                        <div class="col-sm-6 ">
                                                            <asp:TextBox AutoPostBack="true" OnTextChanged="txtOPWarehouse_TextChanged" runat="server" ID="txtOPWarehouse" CssClass="form-control" />
                                                        </div>
                                                        <div class="col-sm-1 padding3">
                                                            <asp:LinkButton ID="lbtnSeWaraeHouse" runat="server" OnClick="lbtnSeWaraeHouse_Click">
                                                            <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-1 padding0">
                                                        <div class="buttonRow">
                                                            <asp:LinkButton ID="lbtnOPReturn" runat="server" OnClick="lbtnOPReturn_Click">
                                                                <span class="glyphicon" style="font-size:16px;"  aria-hidden="true">Return</span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="" style="height: 125px; overflow-y: scroll;">
                                                    <asp:GridView ID="dgvOldPart" CssClass="table table-hover table-striped"
                                                        ShowHeaderWhenEmpty="true" EmptyDataText="No data found..."
                                                        runat="server" GridLines="None" PagerStyle-CssClass="cssPager"
                                                        AutoGenerateColumns="False">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox Text="" ID="chkOldPartSelect" OnCheckedChanged="chkOldPartSelect_CheckedChanged" runat="server" />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="15px" />
                                                                <ItemStyle Width="15px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="sop_seqno" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsop_seqno" Width="100%" Text='<%# Bind("sop_seqno") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                                <ItemStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Item Code">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSOP_OLDITMCD" Width="100%" Text='<%# Bind("SOP_OLDITMCD") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                                <ItemStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Description">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDESCRIPTION" Width="100%" Text='<%# Bind("DESCRIPTION") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                                <ItemStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSOP_OLDITMSTUS_TEXT" Width="100%" Text='<%# Bind("SOP_OLDITMSTUS_TEXT") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                                <ItemStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSOP_OLDITMSTUS" Width="100%" Text='<%# Bind("SOP_OLDITMSTUS") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                                <ItemStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Serial">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSOP_OLDITMSER1" Width="100%" Text='<%# Bind("SOP_OLDITMSER1") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="70px" />
                                                                <ItemStyle Width="70px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Qty">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSOP_OLDITMQTY" Width="100%" Text='<%# Bind("SOP_OLDITMQTY") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="70px" />
                                                                <ItemStyle Width="70px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="In Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSOP_CRE_DT" Width="100%" Text='<%# Bind("SOP_CRE_DT") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="70px" />
                                                                <ItemStyle Width="70px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Remark">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsop_rmk" Width="100%" Text='<%# Bind("sop_rmk") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="70px" />
                                                                <ItemStyle Width="70px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Replace">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox Checked='<%#Convert.ToBoolean(Eval("NewReplace")) %>' Text="" ID="chkOldPartReplace" OnCheckedChanged="chkOldPartReplace_CheckedChanged" runat="server" />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="15px" />
                                                                <ItemStyle Width="15px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="New Item Code">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNewItemCode" Width="100%" Text='' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                                <ItemStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="New Serial" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNewSerial" Width="100%" Text='' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="100px" />
                                                                <ItemStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="SourceTable" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSourceTable" Width="100%" Text='<%# Bind("SourceTable") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="70px" />
                                                                <ItemStyle Width="70px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="SOP_REQWCN" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSOP_REQWCN" Width="100%" Text='<%# Bind("SOP_REQWCN") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="70px" />
                                                                <ItemStyle Width="70px" />
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
                            <div class="col-sm-12">
                                <div class="row height10">
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  labelText1">
                                            Job Close Type
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:DropDownList runat="server" ID="ddlCloseType" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  labelText1">
                                            Technician Close Remark
                                        </div>
                                        <div class="col-sm-5 ">
                                            <asp:TextBox runat="server" ID="txtTechClsRmkNew" CssClass="form-control" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  labelText1">
                                            Technician Remark
                                        </div>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtTechnicanRemark" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  labelText1">
                                            Customer Close Remark
                                        </div>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtCusRemark" />
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
    </asp:Panel>

    <%-- pnl Old Part remove --%>
    <asp:UpdatePanel runat="server" ID="UpdatePanel15">
        <ContentTemplate>
            <asp:Button ID="btnpop1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popOldPartRem" runat="server" Enabled="True" TargetControlID="btnpop1"
                PopupControlID="pnlOldPartRemove" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress7" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="upOldPartRemove">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait10" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait10" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:Panel runat="server" ID="pnlOldPartRemove">
        <asp:UpdatePanel runat="server" ID="upOldPartRemove">
            <ContentTemplate>
                <div runat="server" id="Div5" class="panel panel-primary" style="padding: 5px;">
                    <div class="panel panel-default" style="height: 400px; width: 900px;">
                        <div class="panel-heading" style="height: 25px;">
                            <div class="col-sm-10">
                                <strong>Service Old Part Remove</strong>
                            </div>
                            <div class="col-sm-2 text-right">
                                <asp:LinkButton ID="lbtnOldPartRemClose" runat="server" OnClick="lbtnOldPartRemClose_Click">
                                    <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-sm-3">
                                        <div class="col-sm-1 padding03">
                                            <asp:CheckBox AutoPostBack="true" OnCheckedChanged="chkIsPeri_CheckedChanged" Text="" ID="chkIsPeri" runat="server" />
                                        </div>
                                        <div class="col-sm-10 padding0 labelText1">
                                            Peripharals
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="col-sm-2 labelText1">
                                            Date
                                        </div>
                                        <div class="col-sm-7 paddingRight5">
                                            <asp:TextBox runat="server" Enabled="false" ID="txtOldPartRemDt" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="btnOldPartRemDt" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtOldPartRemDt"
                                                PopupButtonID="btnOldPartRemDt" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 padding0">
                                        <div class="col-sm-2 padding0">
                                            
                                        </div>
                                        <div class="col-sm-2 padding0">
                                            <asp:LinkButton ID="lbtnOPRView" runat="server" OnClick="lbtnOPRView_Click">
                                                <span class="glyphicon glyphicon-saved"  aria-hidden="true">View</span>
                                            </asp:LinkButton>
                                        </div>
                                        
                                        <div class="col-sm-2 padding0">
                                            <asp:LinkButton ID="lbtnOPRSave" OnClientClick="return ConfProcess();" Visible="false"  runat="server" OnClick="lbtnOPRSave_Click">
                                                <span class="glyphicon glyphicon-saved"  aria-hidden="true">Save</span>
                                            </asp:LinkButton>
                                            <asp:LinkButton  ID="lbtnOPRRemove" OnClientClick="return ConfOPRRemove();" runat="server" OnClick="lbtnOPRRemove_Click">
                                                <span class="glyphicon glyphicon-saved"  aria-hidden="true">Remove</span>
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-2 padding0">
                                            <asp:LinkButton ID="lbtnOPRRefix" OnClientClick="return ConfReFix();" runat="server" OnClick="lbtnOPRRefix_Click">
                                                <span class="glyphicon glyphicon-saved"  aria-hidden="true">Re-Fix</span>
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-2 padding0">
                                            <asp:LinkButton ID="lbtnOPRClear" runat="server" OnClick="lbtnOPRClear_Click">
                                                <span class="glyphicon glyphicon-saved"  aria-hidden="true">Clear</span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row height16">
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-sm-12">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-sm-3 padding0">
                                                    <div class="col-sm-4 padding0">
                                                        Item Code
                                                    </div>
                                                    <div class="col-sm-7 padding0">
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtOPRItem" AutoPostBack="true" OnTextChanged="txtOPRItem_TextChanged" />
                                                    </div>
                                                    <div class="col-sm-1 padding3">
                                                        <asp:LinkButton ID="lbtnSeOprItm" runat="server" OnClick="lbtnSeOprItm_Click">
                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3 padding0">
                                                    <div class="col-sm-4 padding0">
                                                        Item Status
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <asp:DropDownList AppendDataBoundItems="true" runat="server" CssClass="form-control" ID="ddlOPRItemSts" AutoPostBack="true">
                                                            <asp:ListItem Value="0" Text="--Select--" />
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4 padding0">
                                                    <div class="col-sm-3 padding0" >
                                                        Serial
                                                    </div>
                                                    <div class="col-sm-9 paddingLeft0">
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtOPRSerial" AutoPostBack="true" OnTextChanged="txtOPRSerial_TextChanged" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-2 padding0">
                                                    <div class="col-sm-4 padding0">
                                                        Quantity
                                                    </div>
                                                    <div class="col-sm-7 padding0">
                                                        <asp:TextBox runat="server" CssClass="form-control text-right" ID="txtOPRQty" AutoPostBack="true" OnTextChanged="txtOPRQty_TextChanged" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="height:5px;">
                                    </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-sm-3 labelText1 padding0">
                                                    <asp:Label Text="Description :" ID="lblOPRDes" runat="server" />
                                                </div>
                                                <div class="col-sm-3 labelText1 padding0">
                                                    <asp:Label Text="Model :" ID="lblOPRModel" runat="server" />
                                                </div>
                                                <div class="col-sm-3 labelText1 padding0">
                                                    <asp:Label Text="Brand :" ID="lblOPRBrand" runat="server" />
                                                </div>
                                                <div class="col-sm-3 labelText1 padding0">
                                                    <asp:Label Text="Serial Status :" ID="lblOPRStus" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                   <%-- <div class="col-sm-1 padding0">
                                        <div class="" style="font-size: 15px;">
                                            <asp:LinkButton ID="lbtnOPRAddItem" runat="server" OnClick="lbtnOPRAddItem_Click">
                                                    <span style="font-size:16px;" class="glyphicon glyphicon-circle-arrow-down" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>--%>
                                </div>
                            </div>
                            <div class="row height16">
                            </div>
                            <div class="row height16">
                                <div class="col-sm-12">
                                    <div class="col-sm-11 padding0">
                                        <div class="col-sm-2">
                                            Remark
                                        </div>
                                        <div class="col-sm-9">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtOPRRemark"  />
                                        </div>
                                    </div>
                                    <div class="col-sm-1 padding0">
                                        <div class="" style="font-size: 15px;">
                                           <asp:LinkButton ID="lbtnOPRAddRemark" runat="server" OnClick="lbtnOPRAddRemark_Click">
                                                    <span style="font-size:16px;" class="glyphicon glyphicon-arrow-down" aria-hidden="true"></span>Add
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="height:5px;">
                                <div class="col-sm-12">
                                    <div>
                                        <asp:GridView ID="dgvOPR" CssClass="table table-hover table-striped"
                                            ShowHeaderWhenEmpty="true" EmptyDataText="No data found..."
                                            runat="server" GridLines="None" PagerStyle-CssClass="cssPager"
                                            AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox Text="" ID="chkOPRSelect" OnCheckedChanged="chkOPRSelect_CheckedChanged" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="15px" />
                                                    <ItemStyle Width="15px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SOP_LINE" Visible="false">
                                                    <ItemTemplate>
<%--                                                        <asp:Label ID="Label1" Width="100%" Text='<%# Bind("SOP_LINE") %>' runat="server"></asp:Label>--%>
                                                        <asp:Label ID="lblSOP_LINE" Width="100%" Text='' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="100px" />
                                                    <ItemStyle Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSOP_OLDITMCD" Width="100%" Text='<%# Bind("SOP_OLDITMCD") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="100px" />
                                                    <ItemStyle Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDESCRIPTION" Width="100%" Text='<%# Bind("DESCRIPTION") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="100px" />
                                                    <ItemStyle Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Part NO">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPARTNO" Width="100%" Text='<%# Bind("PARTNO") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="100px" />
                                                    <ItemStyle Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SOP_TP" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSOP_TP" Width="100%" Text='<%# Bind("SOP_TP") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="100px" />
                                                    <ItemStyle Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SOP_TP_text" Visible="false" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSOP_TP_text"  Width="100%" Text='<%# Bind("SOP_TP_text") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="100px" />
                                                    <ItemStyle Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSOP_OLDITMSTUS" Width="100%" Text='<%# Bind("SOP_OLDITMSTUS") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="100px" />
                                                    <ItemStyle Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSOP_OLDITMSTUS_TEXT" Width="100%" Text='<%# Bind("SOP_OLDITMSTUS_TEXT") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="100px" />
                                                    <ItemStyle Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Serial" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSOP_OLDITMSER1" Width="100%" Text='<%# Bind("SOP_OLDITMSER1") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="70px" />
                                                    <ItemStyle Width="70px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Serial" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSOP_OLDITMSER1New" Width="100%" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="70px" />
                                                    <ItemStyle Width="70px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSOP_OLDITMQTY" Width="100%" Text='<%# Bind("SOP_OLDITMQTY") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="70px" />
                                                    <ItemStyle Width="70px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remark">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsop_rmk" Width="100%" Text='<%# Bind("SOP_RMK") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="70px" />
                                                    <ItemStyle Width="70px" />
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

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

    <%-- pnl search Date--%>
    <asp:UpdatePanel runat="server" ID="upSearchDT">
        <ContentTemplate>
            <asp:Button ID="btn2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupSearchDT" runat="server" Enabled="True" TargetControlID="btn2"
                PopupControlID="pnlSearchDt" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress3" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="upPnlDt">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait3" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait3" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:Panel runat="server" ID="pnlSearchDt" DefaultButton="lbtnSearch">
        <asp:UpdatePanel runat="server" ID="upPnlDt">
            <ContentTemplate>
                <div runat="server" id="Div1" class="panel panel-primary" style="padding: 5px;">
                    <div class="panel panel-default" style="height: 450px; width: 850px;">
                        <div class="panel-heading" style="height: 25px;">
                            <div class="col-sm-11"></div>
                            <div class="col-sm-1">
                                <asp:LinkButton ID="btnCloseDt" runat="server" OnClick="btnCloseDt_Click">
                             <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <div class="col-sm-5">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-2 labelText1">
                                                            From
                                                        </div>
                                                        <div class="col-sm-6 paddingRight5">
                                                            <asp:TextBox runat="server" Enabled="false" ID="txtFDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0">
                                                            <asp:LinkButton ID="lbtnFDate" runat="server" CausesValidation="false">
                                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtFDate"
                                                                PopupButtonID="lbtnFDate" Format="dd/MMM/yyyy">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-2 labelText1">
                                                            To
                                                        </div>
                                                        <div class="col-sm-6 paddingRight5">

                                                            <asp:TextBox runat="server" Enabled="false" ID="txtTDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>

                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0">
                                                            <asp:LinkButton ID="lbtnTDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                            <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtTDate"
                                                                PopupButtonID="lbtnTDate" Format="dd/MMM/yyyy">
                                                            </asp:CalendarExtender>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div class="col-sm-7">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-sm-4 labelText1">
                                                    Search by key
                                                </div>
                                                <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <ContentTemplate>
                                                        <div class="col-sm-6 paddingRight5">
                                                            <asp:DropDownList ID="ddlSerByKeyDt" runat="server" class="form-control" AutoPostBack="false">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-sm-4 labelText1">
                                                    Search by word
                                                </div>
                                                <div class="col-sm-6 paddingRight5">
                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtSerByKeyDt" CausesValidation="false" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="lbtnSearchDt_Click"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                    <ContentTemplate>
                                                        <div class="col-sm-1 paddingLeft0">
                                                            <asp:LinkButton ID="lbtnSearchDt" runat="server" OnClick="lbtnSearchDt_Click">
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
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 height16">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                        <ContentTemplate>
                                            <div style="height: 355px;">
                                                <asp:GridView ID="dgvResultDt" CausesValidation="false" runat="server" AllowPaging="True"
                                                    GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" ShowHeaderWhenEmpty="true"
                                                    EmptyDataText="No data found..."
                                                    OnPageIndexChanging="dgvResultDt_PageIndexChanging" OnSelectedIndexChanged="dgvResultDt_SelectedIndexChanged">
                                                    <Columns>
                                                        <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="12" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
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

    <%-- pnl job close conf --%>
     <asp:UpdatePanel ID="UpdatePanel13" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnJobClosePnl" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popJobCloseConf" runat="server" Enabled="True" TargetControlID="btnJobClosePnl"
                PopupControlID="pnlJobClosConf" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress6" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel14">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait8" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait8" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/banners/hidden_progress_bar.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:Panel ID="pnlJobClosConf" runat="server" align="center">
        <div runat="server" id="Div4" class="panel panel-info" style="width:250px; height:100px;">
            <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:ScriptManagerProxy ID="ScriptManagerProxy5" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                <ContentTemplate>
                    <div class="col-sm-12 padding0">
                        <div class="panel panel-default padding0">
                            <div class="panel panel-heading text-left padding0">
                                <span>Job Close</span>
                            </div>
                            <div class="panel-body padding0">
                                <div class="row" style="height:15px;">
                                    </div>
                                <div class="row">
                                    <div class="col-sm-12 text-left">
                                        <strong> Do you want to close the job ? </strong>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:Button ID="btnJobCloseConfirm" runat="server" Text="yes" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs"
                                            OnClick="btnJobCloseConfirm_Click" />
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:Button ID="btnJobCloseNotConfirm" runat="server" Text="No" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs"
                                            OnClick="btnJobCloseNotConfirm_Click" />
                                    </div>
                                </div>
                                <div class="row height16">
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>
    <%-- pnl Old Part --%>
    <asp:UpdatePanel runat="server" ID="UpdatePanel16">
        <ContentTemplate>
            <asp:Button ID="btnpop2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popLocationError" runat="server" Enabled="True" TargetControlID="btnpop2"
                PopupControlID="pnlLocationError" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel runat="server" ID="pnlLocationError">
        <asp:UpdatePanel runat="server" ID="UpdatePanel17">
            <ContentTemplate>
                <div runat="server" id="Div6" class="panel panel-primary" style="padding: 5px;">
                    <div class="panel panel-default" >
                        <div class="panel-heading" style="height: 25px;">
                            <div class="col-sm-10">
                            </div>
                            <div class="col-sm-2 text-right">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-12">
                                    <strong>Service parameter(s) not setup !</strong>
                                </div>
                            </div>
                            <div class="row height10">
                                </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-sm-2">
                                    </div>
                                    
                                    <div class="col-sm-6">
                                        <%--<asp:Button ID="lbtnLocationErrNo" runat="server" Text="No" CausesValidation="false" class="btn btn-primary" OnClick="lbtnLocationErrNo_Click" />--%>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:Button ID="lbtnLocationErrOk" runat="server" Text="Ok" CausesValidation="false" class="btn btn-primary" OnClick="lbtnLocationErrOk_Click" />
                                    </div>
                                    
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
          Sys.Application.add_load(func);
          function func() {
              if (typeof jQuery == 'undefined') {
                  alert('jQuery is not loaded');
              }
              $('.writeMclick').mousedown(function (e) {
                  if (e.button == 2) {
                      alert('This functionality is disabled !');
                      return false;
                  } else {
                      return true;
                  }
              });
          }
    </script>
</asp:Content>
