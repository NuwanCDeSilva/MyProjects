<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="PenaltyChargeProcess.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Finance.PenaltyChargeProcess" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/jquery.toastmessage.js"></script>

    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/jquery.toastmessage.js"></script>


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
                sticky: false,
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
                sticky: false,
                position: 'top-center',
                type: 'error',
                closeText: '',
                close: function () {
                    console.log("toast is closed ...");
                }
            });
        }
        function SaveConfirm() {

            var selectedvalue = confirm("Do you want to process data?");
            if (selectedvalue) {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "No";
            }
        };
        function CancelConfirm() {

            var selectedvalue = confirm("Do you want to Cancel data?");
            if (selectedvalue) {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "No";
            }
        };
    </script>
    <style>
        .dropdownpalan {
            left: -126px !important;
            top: 25px !important;
        }

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

        .labledesign {
            color: #1f1782;
            font-size: larger;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="txtSavelconformmessageValue" runat="server" />
    <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel1">
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="panel panel-default">
                <div class="panel-heading paddingtop0 paddingtopbottom0">

                    <div class="row">
                        <div class="col-sm-7">
                            <strong>Penalty Charge Process</strong>
                        </div>
                        <div class="col-sm-2">
                        </div>
                        <div class="col-sm-3">
                        </div>
                    </div>
                </div>

                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-3">
                            <div class="col-sm-4 labelText1">
                                PC
                            </div>
                            <div class="col-sm-6 paddingRight5 paddingLeft0">
                                <asp:TextBox runat="server" ID="txtpc" CausesValidation="false" CssClass="form-control" OnTextChanged="txtpc_TextChanged"></asp:TextBox>
                            </div>
                            <div class="col-sm-1">
                                <asp:LinkButton ID="btnpc" runat="server" CausesValidation="false" OnClick="btnpc_Click">
                                <span class="glyphicon glyphicon-search"></span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="col-sm-4 labelText1">
                                Inv Type
                            </div>
                            <div class="col-sm-6 paddingRight5 paddingLeft0">
                                <asp:TextBox runat="server" ID="txtinvtype" CausesValidation="false" CssClass="form-control" OnTextChanged="txtinvtype_TextChanged"></asp:TextBox>
                            </div>
                            <div class="col-sm-1">
                                <asp:LinkButton ID="lbtninvtype" runat="server" CausesValidation="false" OnClick="lbtninvtype_Click">
                                <span class="glyphicon glyphicon-search"></span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="col-sm-4 labelText1">
                                 Date
                            </div>
                            <div class="col-sm-6 paddingRight5 paddingLeft0">
                                <asp:TextBox runat="server" ID="txtasatdate" CausesValidation="false" CssClass="form-control" OnTextChanged="txtasatdate_TextChanged"></asp:TextBox>
                            </div>
                            <div class="col-sm-1 paddingLeft0">
                                <asp:LinkButton ID="lbasdate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                </asp:LinkButton>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtasatdate"
                                    PopupButtonID="lbasdate" Format="dd/MMM/yyyy">
                                </asp:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-sm-1">
                            <asp:Button runat="server" ID="btnupload" Text="Process" CssClass="form-control btn-primary" OnClick="btnupload_Click" OnClientClick="SaveConfirm();" />
                        </div>
                        <div class="col-sm-1">
                            <asp:Button runat="server" ID="lbtncancel" Text="Cancel" CssClass="form-control btn-primary" OnClick="lbtncancel_Click" OnClientClick="CancelConfirm();" />
                        </div>
                        <div class="col-sm-1">
                            <asp:Button runat="server" ID="lbtnprint" Text="Print" CssClass="form-control btn-primary" OnClick="lbtnprint_Click" />
                        </div>
                    </div>
                    <div class="row">
                        <asp:Label runat="server" ID="lbldocuments" CssClass="labledesign"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <asp:Label runat="server" ID="Label1" CssClass="labledesign"></asp:Label>
                </div>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading paddingtop0 paddingtopbottom0">

                    <div class="row">
                        <div class="col-sm-7">
                            <strong>Penalty Charge Details</strong>
                        </div>
                        <div class="col-sm-2">
                        </div>
                        <div class="col-sm-3">
                        </div>
                    </div>
                </div>

                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-3">
                            <div class="col-sm-4 labelText1">
                                From Date
                            </div>
                            <div class="col-sm-6 paddingRight5 paddingLeft0">
                                <asp:TextBox runat="server" ID="txtfdate" CausesValidation="false" CssClass="form-control" OnTextChanged="txtfdate_TextChanged"></asp:TextBox>
                            </div>
                            <div class="col-sm-1 paddingLeft0">
                                <asp:LinkButton ID="lbtnSDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                </asp:LinkButton>
                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtfdate"
                                    PopupButtonID="lbtnSDate" Format="dd/MMM/yyyy">
                                </asp:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="col-sm-4 labelText1">
                                To Date
                            </div>
                            <div class="col-sm-6 paddingRight5 paddingLeft0">
                                <asp:TextBox runat="server" ID="txttdate" CausesValidation="false" CssClass="form-control" OnTextChanged="txttdate_TextChanged"></asp:TextBox>
                            </div>
                            <div class="col-sm-1 paddingLeft0">
                                <asp:LinkButton ID="lbtntdate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                </asp:LinkButton>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txttdate"
                                    PopupButtonID="lbtntdate" Format="dd/MMM/yyyy">
                                </asp:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-sm-1">
                            <asp:Button runat="server" ID="lbtnview" Text="View" CssClass="form-control btn-primary" OnClick="lbtnview_Click" />
                        </div>
                        <div class="col-sm-1">
                            <asp:CheckBox runat="server" AutoPostBack="true" ID="chkall" Text="All" OnCheckedChanged="chkall_CheckedChanged" />
                        </div>
                    </div>

                </div>
                <div class="col-sm-6">

                    <div class="panelscoll" style="height: 150px">

                        <asp:GridView ID="grdinv" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                            <Columns>
                                <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkselectinv" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inv No">
                                    <ItemTemplate>
                                        <asp:Label ID="lbinvno" runat="server" Text='<%# Bind("sah_inv_no") %>' Width="120px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ref No">
                                    <ItemTemplate>
                                        <asp:Label ID="lbrefno" runat="server" Text='<%# Bind("sah_ref_doc") %>' Width="120px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbdate" runat="server" Text='<%# Bind("sah_dt", "{0:dd/MMM/yyyy}") %>' Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Custormer">
                                    <ItemTemplate>
                                        <asp:Label ID="lbcustormer" runat="server" Text='<%# Bind("sah_cus_cd") %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Value">
                                    <ItemTemplate>
                                        <asp:Label ID="txtlpsundes" runat="server" Text='<%# Bind("sah_anal_7") %>' Width="120px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>

                    </div>


                </div>
                <div class="col-sm-3">

                    <div class="panelscoll" style="height: 150px">

                        <asp:GridView ID="grdrealinv" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                            <Columns>
                                <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkallinv" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inv No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblrealinvno" runat="server" Text='<%# Bind("inv_no") %>' Width="120px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>

                    </div>


                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>




    <asp:UpdatePanel runat="server" ID="upSearch">
        <ContentTemplate>
            <asp:Button ID="btn1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupSearch" runat="server" Enabled="True" TargetControlID="btn1"
                PopupControlID="pnlSearch" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlSearch" DefaultButton="lbtnSearch">
        <div runat="server" id="test" class="panel panel-primary" style="padding: 5px;">
            <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default" style="height: 350px; width: 700px;">
                <div class="panel-heading" style="height: 25px;">
                    <div class="col-sm-11"></div>
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
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
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
    </asp:Panel>



</asp:Content>
