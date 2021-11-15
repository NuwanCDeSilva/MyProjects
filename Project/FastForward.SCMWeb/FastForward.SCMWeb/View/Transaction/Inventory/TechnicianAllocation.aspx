<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="TechnicianAllocation.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Inventory.TechnicianAllocation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">

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
                position: 'top-center',
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
    <script>
        function ClearConfirm() {
            var selectedvalue = confirm("Do you want to clear data?");
            if (selectedvalue) {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "No";
            }
        };
        function SaveConfirm() {
            var selectedvalue = confirm("Do you want to save ?");
            if (selectedvalue) {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "No";
            }
        };
        function DeleteConfirm() {

            var selectedvalue = confirm("Do you want to delete item ?");
            if (selectedvalue) {
                document.getElementById('<%=txtDeleteconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtDeleteconformmessageValue.ClientID %>').value = "No";
            }
        };
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
              && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
    </script>
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

        #MSearch {
            font-size: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="panel panel-default marginLeftRight5">
        <div class="panel-body">
            <div class="tab-pane" id="df">
                <asp:HiddenField ID="txtClearlconformmessageValue" runat="server" />
                <asp:HiddenField ID="txtSavelconformmessageValue" runat="server" />
                <asp:HiddenField ID="txtDeleteconformmessageValue" runat="server" />
                <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>

                        <div class="row">
                            <div class="col-sm-12 ">
                                <div class="panel panel-default">
                                    <div class="panel panel-heading">
                                        <strong><b>Technician Allocation</b></strong>
                                    </div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-sm-6  buttonrow">
                                                <div id="WarnningBin" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">
                                                    <div class="col-sm-11">
                                                        <strong>Alert!</strong>
                                                        <asp:Label ID="lblWarnninglBin" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:LinkButton ID="LinkButton8" runat="server" CausesValidation="false" CssClass="floatright">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div id="SuccessBin" runat="server" visible="false" class="alert alert-success alert-dismissible" role="alert">
                                                    <div class="col-sm-11">
                                                        <strong>Success!</strong>
                                                        <asp:Label ID="lblSuccessBin" runat="server"></asp:Label>

                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:LinkButton ID="LinkButton9" runat="server" CausesValidation="false" CssClass="floatright">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>

                                                </div>
                                                <div id="Div3" runat="server" visible="false" class="alert alert-info alert-dismissible" role="alert">
                                                    <strong>Alert!</strong>
                                                    <asp:Label ID="Label3" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-6  buttonRow">
                                                <div class="col-sm-5">
                                                </div>
                                                <div class="col-sm-3 paddingRight0">
                                                    <asp:LinkButton ID="lbtnTechAlloSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="SaveConfirm()" OnClick="lbtnTechAlloSave_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:LinkButton ID="lbtnTechAlloClear" runat="server" CssClass="floatRight" OnClientClick="ClearConfirm()" OnClick="lbtnTechAlloClear_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="row">
                                            <div class="col-sm-12 ">
                                                <div class="panel panel-default">
                                                    <div class="panel-body">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-1 labelText1 ">
                                                                    Model :
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:TextBox runat="server" ID="txtModel" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft5 Lwidth">
                                                                    <asp:LinkButton ID="lbtnModel" runat="server" CausesValidation="false" OnClick="lbtnModel_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-1 labelText1 ">
                                                                    Item :
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:TextBox runat="server" ID="txtItem" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft5 Lwidth">
                                                                    <asp:LinkButton ID="lbtnItem" runat="server" CausesValidation="false" OnClick="lbtnItem_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-1 labelText1 ">
                                                                    Engine # :
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:TextBox runat="server" ID="txtEngine" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft5 Lwidth">
                                                                    <asp:LinkButton ID="lbtnEngine" runat="server" CausesValidation="false" OnClick="lbtnEngine_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-1 labelText1">
                                                                    chassi # :
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:TextBox runat="server" ID="txtchassi" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-2 paddingLeft5 Lwidth">
                                                                    <asp:LinkButton ID="lbtnchassi" runat="server" CausesValidation="false" OnClick="lbtnchassi_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-1 labelText1" style="width: 60px">
                                                                    Job # :
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:TextBox runat="server" ID="txtJob" CausesValidation="false" CssClass="form-control" Width="165px"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-2 paddingLeft40 Lwidth" style="padding-left: 60px">
                                                                    <asp:LinkButton ID="lbtnJob" runat="server" CausesValidation="false" OnClick="lbtnJob_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft100 Lwidth" style="padding-left: 15px">
                                                                    <asp:LinkButton ID="lbtnSearchJob" runat="server" CausesValidation="false" OnClick="lbtnSearchJob_Click">
                                                        <span id="MSearch" class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        Search
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-1 labelText1">
                                                                    AOD # :
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:TextBox runat="server" ID="txtAod" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-2 paddingLeft5 Lwidth">
                                                                    <asp:LinkButton ID="lbtnAod" runat="server" CausesValidation="false" OnClick="lbtnAod_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-1 labelText1">
                                                                    Date From :
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:TextBox runat="server" Enabled="true" ID="txtFromDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-2 paddingLeft5 Lwidth">
                                                                    <asp:LinkButton ID="lbtnToDate" Visible="true" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate"
                                                                        PopupButtonID="lbtnToDate" Format="dd/MMM/yyyy">
                                                                    </asp:CalendarExtender>
                                                                </div>
                                                                <div class="col-sm-1 labelText1">
                                                                    Date To :
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:TextBox runat="server" Enabled="true" ID="txtToDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-2 paddingLeft5 Lwidth">
                                                                    <asp:LinkButton ID="lbtnDate" Visible="true" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtToDate"
                                                                        PopupButtonID="lbtnDate" Format="dd/MMM/yyyy">
                                                                    </asp:CalendarExtender>
                                                                </div>
                                                                <div class="col-sm-1 labelText1 paddingLeft7 paddingRight0">
                                                                    Not Allocated :
                                                                </div>
                                                                <div class="col-sm-1 Lwidth">
                                                                    <asp:CheckBox ID="chkNotAllocated" runat="server" Checked="true" Width="5px" />
                                                                </div>
                                    <%--                            <div class="col-sm-1 labelText1 paddingLeft7 paddingRight0">
                                                                    Customer Expected :
                                                                </div>
                                                                <div class="col-sm-1 Lwidth">
                                                                    <asp:CheckBox ID="chkCusExpected" runat="server" Checked="true" Width="5px" />
                                                                </div>--%>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="panel panel-default">
                                                                    <div class=" panel-body height200 panelscollbar">
                                                                        <asp:GridView PageSize="7" ID="grdJobDeatils" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false" OnSelectedIndexChanged="grdJobDeatils_SelectedIndexChanged" OnDataBound="grdJobDeatils_DataBound" AllowPaging="true" OnPageIndexChanging="grdJobDeatils_PageIndexChanging">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="" Visible="true">
                                                                                    <HeaderTemplate>
                                                                                        <span>
                                                                                            <asp:CheckBox ID="chkHeaderAllApp" Checked="false" runat="server" AutoPostBack="true" OnCheckedChanged="chkHeaderAllApp_CheckedChanged" />
                                                                                            All
                                                                                        </span>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <div id="delbtndiv">
                                                                                            <asp:CheckBox ID="ChkselectItm" runat="server" AutoPostBack="true" OnCheckedChanged="ChkselectItm_CheckedChanged"></asp:CheckBox>
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText=" Model Code" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="col_Model" runat="server" Text='<%# Bind("Model") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Item Code" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="col_ItemCode" runat="server" Text='<%# Bind("ItemCode") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Job No" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="col_JobNo" runat="server" Text='<%# Bind("JobNo") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="AOD No" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="col_ReqNo" runat="server" Text='<%# Bind("ReqNo") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Engine No" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="col_EngineNo" runat="server" Text='<%# Bind("EngineNo") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Chassis No" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="col_ChassisNo" runat="server" Text='<%# Bind("ChassisNo") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Allocated" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="col_Allocated" runat="server" Text='<%# Bind("Allocated") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="LineNo" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="col_LineNo" runat="server" Text='<%# Bind("LineNo") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Town" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="col_Town" runat="server" Text='<%# Bind("SJB_TOWN") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-1 labelText1 ">
                                                                    Technician :
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0" style="width: 50px">
                                                                    <asp:TextBox runat="server" ID="txtTechCode" CausesValidation="false" CssClass="form-control" placeHolder="Code"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0" style="width: 130px">
                                                                    <asp:TextBox ReadOnly="true" runat="server" ID="txtTechName" CausesValidation="false" CssClass="form-control" placeHolder="Technician Name"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft5 Lwidth">
                                                                    <asp:LinkButton ID="lbtnTechCode" runat="server" CausesValidation="false" OnClick="lbtnTechCode_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-1 labelText1">
                                                                    Date From :
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:TextBox ReadOnly="true" runat="server" Enabled="true" ID="txtTDateFrom" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-2 paddingLeft5 Lwidth">
                                                                    <asp:LinkButton ID="lbtnToDateT" Visible="true" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTDateFrom"
                                                                        PopupButtonID="lbtnToDateT" Format="dd/MMM/yyyy">
                                                                    </asp:CalendarExtender>
                                                                </div>
                                                                <div class="col-sm-1 labelText1">
                                                                    Date To :
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:TextBox ReadOnly="true" runat="server" Enabled="true" ID="txtTDateTo" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-2 paddingLeft5 Lwidth">
                                                                    <asp:LinkButton ID="lbtnDateT" Visible="true" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtTDateTo"
                                                                        PopupButtonID="lbtnDateT" Format="dd/MMM/yyyy">
                                                                    </asp:CalendarExtender>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft100 Lwidth" style="padding-left: 80px">
                                                                    <asp:LinkButton ID="lbtnAllocate" runat="server" CausesValidation="false" OnClick="lbtnAllocate_Click">
                                                        <span class="glyphicon glyphicon-plus-sign" aria-hidden="true"></span>
                                                        Allocate
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="panel panel-default">
                                                                    <div class=" panel-body height200 panelscollbar">
                                                                        <asp:GridView ID="grdTechnician" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false" OnSelectedIndexChanged="grdTechnician_SelectedIndexChanged">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lbtnDetaltecost" CausesValidation="false" CommandName="Delete" runat="server" OnClientClick="DeleteConfirm()" OnClick="lbtnDetaltecost_Click">
                                                                                            <span class="glyphicon glyphicon-trash" aria-hidden="true" style="font-size:15px"></span> 
                                                                                        </asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText=" Job No" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="col_TJobNo" runat="server" Text='<%# Bind("JobNo") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Engine No" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="col_TEngineNo" runat="server" Text='<%# Bind("EngineNo") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Chassis No" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="col_TChassisNo" runat="server" Text='<%# Bind("ChassisNo") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Employee Name" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="col_TEmpName" runat="server" Text='<%# Bind("EmpName") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="From Date" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="col_TFDate" runat="server" Text='<%# Bind("FromDate","{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="To Date" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="col_TToDate" runat="server" Text='<%# Bind("ToDate","{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="LineNo" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="col_TLineNo" runat="server" Text='<%# Bind("LineNo") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="TechCode" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="col_TTechCode" runat="server" Text='<%# Bind("TechCode") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Town" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="col_Town" runat="server" Text='<%# Bind("TownNo") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Status" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="ChkselectTechItm" Checked="true" runat="server" AutoPostBack="true"></asp:CheckBox>
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

                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserPopoup" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="pnlpopup" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpopup" DefaultButton="lbtnSearch">
        <asp:ScriptManagerProxy ID="ScriptManagerProxy17" runat="server"></asp:ScriptManagerProxy>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div runat="server" id="test" class="panel panel-default height400 width700">
                    <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="btnClose" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <%--<span>Commen Search</span>--%>
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12 paddingLeft0 paddingRight0">
                                <div class="row">
                                    <div class="col-sm-12">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12" id="search" runat="server">
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
                                            <asp:TextBox ID="txtSearchbyword" CausesValidation="false" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server"></asp:TextBox>
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
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdResult" CausesValidation="false" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResult_SelectedIndexChanged" OnPageIndexChanging="grdResult_PageIndexChanging">
                                                    <Columns>
                                                        <asp:ButtonField Text="select" CommandName="Select" ItemStyle-Width="10" />
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

    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserDPopoup" runat="server" Enabled="True" TargetControlID="Button2"
                PopupControlID="pnlDpopup" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel21" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlDpopup" DefaultButton="lbtnSearch">
                <div runat="server" id="Div20" class="panel panel-default height400 width1085">

                    <asp:Label ID="Label12" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="btnDClose" runat="server" OnClick="btnDClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <%--<span>Commen Search</span>--%>
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12" id="Div21" runat="server">
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="col-sm-5">
                                    <div class="row">
                                        <div class="col-sm-2 labelText1">
                                            From
                                        </div>
                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                            <asp:TextBox runat="server" TabIndex="200" Enabled="true" ID="txtFDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnFDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtFDate"
                                                PopupButtonID="lbtnFDate" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-2 labelText1">
                                            To
                                        </div>
                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                            <asp:TextBox runat="server" Enabled="true" TabIndex="201" ID="txtTDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnTDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtTDate"
                                                PopupButtonID="lbtnTDate" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>

                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnDateS" runat="server" OnClick="lbtnDateS_Click">
                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-7">

                                    <div class="row">

                                        <div class="col-sm-3 labelText1">
                                            Search by key
                                        </div>
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy18" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                            <ContentTemplate>
                                                <div class="col-sm-8 paddingRight5">
                                                    <asp:DropDownList ID="ddlSearchbykeyD" TabIndex="202" runat="server" class="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3 labelText1">
                                            Search by word
                                        </div>
                                        <div class="col-sm-8 paddingRight5">
                                            <asp:TextBox ID="txtSearchbywordD" CausesValidation="false" TabIndex="203" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbywordD_TextChanged"></asp:TextBox>
                                        </div>
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy19" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                            <ContentTemplate>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnSearchD" runat="server" OnClick="lbtnSearchD_Click">
                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </ContentTemplate>

                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtSearchbywordD" />
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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy20" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdResultD" CausesValidation="false" runat="server" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResultD_SelectedIndexChanged" OnPageIndexChanging="grdResultD_PageIndexChanging">
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
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
