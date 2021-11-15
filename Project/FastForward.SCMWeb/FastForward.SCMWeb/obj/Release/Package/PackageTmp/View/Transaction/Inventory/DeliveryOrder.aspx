<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" MaintainScrollPositionOnPostback="false" AutoEventWireup="true" CodeBehind="DeliveryOrder.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Inventory.DeliveryOrder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/ucOutScan.ascx" TagPrefix="uc1" TagName="ucOutScan" %>
<%--<%@ Register Src="~/UserControls/ucTransportMethode.ascx" TagPrefix="uc1" TagName="ucTransportMethode" %>--%>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <%--<style>
        .panel-default {
            margin-top:1px;
            margin-bottom:1px;
            padding-top:1px;
            padding-bottom:1px;
        }
        .panel {
            margin-top:1px;
            margin-bottom:1px;
            padding-top:1px;
            padding-bottom:1px;
        }
    </style>--%>

    <script>
        //$(document).ready(function () {
        //    maintainScrollPosition();
        //});
        //function pageLoad() {
        //    maintainScrollPosition();
        //}


        function setScrollPosition(scrollValue) {

            $('#<%=hfScrollPosition.ClientID%>').val(scrollValue);
            console.log($('#<%=hfScrollPosition.ClientID%>').val());
        }
        function maintainScrollPosition() {
            $('.dvScroll').scrollTop($('#<%=hfScrollPosition.ClientID%>').val());
        }
   <%--     function maintainScrollPosition() {
            $(".dvScroll").scrollTop($('#<%=hfScrollPosition.ClientID%>').val());

              console.log($('#<%=hfScrollPosition.ClientID%>').val());
          }
        function setScrollPosition(scrollValue) {
            $('#<%=hfScrollPosition.ClientID%>').val(scrollValue);

          }--%>
          //function scrollTop() {
          //    window.document.body.scrollTop = 0;
          //    window.document.documentElement.scrollTop = 0;
          //};
    </script>

    <script type="text/javascript">

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

        function jsIsUserFriendlyChar(val, step) {
            if (val == 8 || val == 9 || val == 13 || val == 45 || val == 46) {
                return true;
            }
            if ((val > 16 && val < 21) || (val > 34 && val < 41)) {
                return true;
            }
            if (step == "Decimals") {
                if (val == 190 || val == 110) {
                    return true;
                }
            }
            return false;
        };


    </script>
    <script>
        function ConfirmSendToPDA() {
            var selectedvaldelitm = confirm("Are you sure you want to send ?");
            if (selectedvaldelitm) {
                document.getElementById('<%=txtpdasend.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtpdasend.ClientID %>').value = "No";
            }
        };
        function SaveConfirm() {
            var selectedvalue = confirm("Do you want to Save data?");
            if (selectedvalue) {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "1";
            } else {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "0";
            }
        };
        function ClearConfirm() {
            var selectedvalue = confirm("Do you want to Clear data?");
            if (selectedvalue) {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "1";
            } else {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "0";
            }
        };

        function SerialDeleteConfirm() {
            var selectedvalue = confirm("Do you want to delete this serial?");
            if (selectedvalue) {
                document.getElementById('<%=hdfSerialDelete.ClientID %>').value = "1";
            } else {
                document.getElementById('<%=hdfSerialDelete.ClientID %>').value = "0";
            }
        };


    </script>
    <script type="text/javascript">

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
                position: 'top-center',
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
        .panel {
            padding-top: 0px;
            padding-bottom: 0px;
            margin-bottom: 0px;
            margin-top: 0px;
        }
    </style>
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="pnlMain">
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
        <asp:UpdatePanel runat="server" ID="pnlMain">
            <ContentTemplate>
                <div class="panel-body">
                    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField ID="txtClearlconformmessageValue" runat="server" />
                            <asp:HiddenField ID="txtSavelconformmessageValue" runat="server" />
                            <asp:HiddenField ID="hdfSerialDelete" runat="server" />
                            <asp:HiddenField ID="hdfIsRecalled" runat="server" />
                            <asp:HiddenField ID="txtpdasend" runat="server" />
                            <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                            <div class="row">
                                <div class="col-sm-12" style="height: 24px;">
                                    <div class="col-sm-8  buttonrow">
                                        <div class="col-sm-2">
                                            <strong>Send to PDA</strong>
                                        </div>                                       

                                        <div class="col-sm-1">
                                            <asp:CheckBox runat="server" ID="chkpda" Enabled="false" AutoPostBack="true" OnCheckedChanged="chkpda_CheckedChanged" />
                                        </div>
                                        <div class="col-sm-7">
                                             <asp:Label ID="_lblcontrol" runat="server" ForeColor="Red"></asp:Label>
                                        </div>
                                        <asp:Panel runat="server" Visible="false">
                                            <div id="DivWarning" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">
                                                <div class="col-sm-11">
                                                    <strong>Alert!</strong>
                                                    <asp:Label ID="lblWarning" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-1">
                                                    <asp:LinkButton ID="btnWarning" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnWGRN_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div id="DivSuccess" runat="server" visible="false" class="alert alert-success alert-dismissible" role="alert">
                                                <div class="col-sm-11">
                                                    <strong>Success!</strong>
                                                    <asp:Label ID="lblSuccess" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-1">
                                                    <asp:LinkButton ID="btnSuccess" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnWGRN_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div id="DivInformation" runat="server" visible="false" class="alert alert-success alert-info" role="alert">
                                                <div class="col-sm-11">
                                                    <strong>Info!</strong>
                                                    <asp:Label ID="lblBackDateInfor" runat="server"></asp:Label>
                                                    <asp:Label ID="lblH1" runat="server" Text="Label"></asp:Label>
                                                </div>
                                                <div class="col-sm-1">
                                                    <asp:LinkButton ID="btnInformationclose" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnWGRN_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <div class="col-sm-4  buttonRow">
                                        <%--<div class="col-sm-3"></div>--%>
                                        <div class="col-sm-2 paddingRight0 paddingLeft0">
                                            <asp:LinkButton ID="lbtnprint" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmPrint()" OnClick="lbtnprint_Click">
                                    <span class="glyphicon glyphicon-print" aria-hidden="true"></span>Print
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-3 paddingRight0 ">
                                            <asp:LinkButton ID="lprintcourier" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmPrint()" OnClick="lprintcourier_Click">
                                    <span class="glyphicon glyphicon-print" aria-hidden="true"></span>Courier
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-2 paddingRight0 paddingLeft0">
                                            <asp:LinkButton ID="lserialprint" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmPrint()" OnClick="lserialprint_Click">
                                    <span class="glyphicon glyphicon-print" aria-hidden="true"></span>Serial
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-2 paddingRight0">
                                            <asp:LinkButton ID="lbtnSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="return SaveConfirm()" OnClick="lbtnSave_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:LinkButton ID="lbtnClear" runat="server" CssClass="floatRight" OnClientClick="return ClearConfirm()" OnClick="lbtnClear_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:ScriptManagerProxy ID="ScriptManagerProxy8" runat="server"></asp:ScriptManagerProxy>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:Panel runat="server" DefaultButton="btnGetInvoices">
                                        <div class="panel panel-default">
                                            <div class="panel-heading height22">
                                                <div class="col-sm-7 paddingRight0">
                                                    <b>Customer Deliveries (Prior Billing)</b>
                                                </div>
                                                <div class="col-sm-4 paddingRight0" style="margin-top: -5px;">
                                                    <asp:RadioButtonList ID="rbnMainType" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbnMainType_SelectedIndexChanged" CssClass="RadioSpace">
                                                        <asp:ListItem Text="Invoice Based" Value="invo" />
                                                        <asp:ListItem Text="Quotation Based" Value="quat" />
                                                    </asp:RadioButtonList>
                                                </div>

                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-3 padding0">
                                                            <div class="col-sm-6 padding0">
                                                                <div class="col-sm-3 labelText1 padding0">
                                                                    From 
                                                                </div>
                                                                <div class="col-sm-8 padding0">
                                                                    <asp:TextBox ID="txtFrom" Enabled="false" CausesValidation="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1" style="padding-left: 3px; padding-right: 0px;">
                                                                    <asp:LinkButton ID="btnFrom" runat="server" CausesValidation="false">
                                                                  <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFrom" Animated="true"
                                                                        PopupButtonID="btnFrom" Format="dd/MMM/yyyy">
                                                                    </asp:CalendarExtender>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6 paddingRight0">
                                                                <div class="col-sm-3 labelText1 padding0">
                                                                    &nbsp;&nbsp;&nbsp;&nbsp; To 
                                                                </div>
                                                                <div class="col-sm-8 padding0">
                                                                    <asp:TextBox ID="txtTo" Enabled="false" CausesValidation="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1" style="padding-left: 3px; padding-right: 0px;">
                                                                    <asp:LinkButton ID="btnTo" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtTo" Animated="true"
                                                                        PopupButtonID="btnTo" Format="dd/MMM/yyyy">
                                                                    </asp:CalendarExtender>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-2 padding0">
                                                            <div class="col-sm-4 labelText1 paddingRight0">
                                                                Customer 
                                                            </div>
                                                            <div class="col-sm-7 padding0">
                                                                <asp:TextBox ID="txtFindCustomer" CausesValidation="false" AutoPostBack="true" OnTextChanged="txtFindCustomer_TextChanged" runat="server" class="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1" style="padding-left: 3px;">
                                                                <asp:LinkButton ID="btnCustomer" runat="server" OnClick="btnCustomer_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-2 padding0">
                                                            <div class="col-sm-4 labelText1 paddingRight0">
                                                                <asp:Label Text="Invoice" ID="lblDocType" runat="server" />
                                                            </div>
                                                            <div class="col-sm-7 padding0">
                                                                <asp:TextBox ID="txtFindInvoiceNo" CausesValidation="false" AutoPostBack="true" runat="server" OnTextChanged="txtFindInvoiceNo_TextChanged" class="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1" style="padding-left: 3px;">
                                                                <asp:LinkButton ID="btnInvoice" runat="server" OnClick="btnInvoice_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-1" style="margin-top: 2px;">
                                                            <asp:LinkButton ID="btnDealerInvoice" runat="server" Visible="false" OnClick="btnDealerInvoice_Click">Dealer Invoice</asp:LinkButton>
                                                        </div>
                                                        <div class="col-sm-2 ">
                                                            <div class="col-sm-1" style="padding-left: 0px; padding-right: 3px; margin-top: 2px;">
                                                                <asp:CheckBox ID="chkDelFrmAnyLoc" Text="" runat="server" AutoPostBack="true"
                                                                    OnCheckedChanged="chkOtherLocationInoices_CheckedChanged" />
                                                            </div>
                                                            <div class="col-sm-7 labelText1" style="padding-left: 3px; padding-right: 0px;">
                                                                <asp:Label Text="Other Loc Invoices" runat="server" />
                                                            </div>
                                                            <div class="col-sm-1 paddingRight0" style="margin-top: -8px;">
                                                                <asp:LinkButton ID="btnGetInvoices" runat="server" OnClick="btnGetInvoices_Click">
                                            <span class="glyphicon glyphicon-search fontsize20 right5" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-2 padding0">
                                                            <div class="col-sm-4 labelText1 padding0">
                                                                Document #
                                                            </div>
                                                            <div class="col-sm-7 padding0">
                                                                <asp:TextBox ID="txtDocumentNo" CausesValidation="false" AutoPostBack="true" runat="server" OnTextChanged="txtDocumentNo_TextChanged" class="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1" style="padding-left: 3px;">
                                                                <asp:LinkButton ID="btnDocumentNo" runat="server" OnClick="btnDocumentNo_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
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
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="panel panel-default">
                                        <div class="panel-body">
                                            <div class="row  ">
                                                <div class="col-sm-12  ">
                                                    <div class="col-sm-2 padding03">
                                                        <div class="col-sm-1 ">
                                                            <asp:CheckBox ID="chkAODoutserials" runat="server" Width="1px"></asp:CheckBox>
                                                        </div>
                                                        <div class="col-sm-10 padding0 ">
                                                            Auto Scan Non-Serialized Items
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-2 padding0">
                                                        <div class="col-sm-1 ">
                                                            <asp:CheckBox ID="chkRno" runat="server" Width="1px"></asp:CheckBox>
                                                        </div>
                                                        <div class="col-sm-10 padding03">
                                                            Bond Entry Details
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-2 padding0">
                                                        <div class="col-sm-1 ">
                                                            <asp:CheckBox ID="Chkbondchange" runat="server" Width="1px" OnCheckedChanged="Chkbondchange_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
                                                        </div>
                                                        <div class="col-sm-10 padding03">
                                                            Bond Change
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-2 padding0 ">
                                                        <div class="col-sm-1  ">
                                                            <asp:CheckBox AutoPostBack="true" ID="chkPendingDoc" Text="" runat="server" />
                                                        </div>
                                                        <div class="col-sm-10 padding0 ">
                                                            <asp:Label Text="PDA Completed" runat="server" ID="lblAllPendin" />
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <asp:LinkButton ID="btnColapse" Visible="true" Text="Transport Method" runat="server">Transport Method
                                                                        <span class="glyphicon" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12" style="height: 120px;">
                                                    <asp:GridView ID="dvPendingInvoices" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True" OnPageIndexChanging="dvPendingInvoices_PageIndexChanging" PagerStyle-CssClass="cssPager"
                                                        EmptyDataText="No data found..." AutoGenerateColumns="False" AllowPaging="True" PageSize="4">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btndgvpendSelect" CausesValidation="false" CommandName="Select" OnClick="btndgvpendSelect_Click" CommandArgument="<%# Container.DataItemIndex %>" runat="server">
                                                            <span class="glyphicon glyphicon-play-circle" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Invoice Number">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSAH_INV_NO" runat="server" Text='<%# Bind("SAH_INV_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="PC">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSAH_PC" runat="server" Text='<%# Bind("SAH_PC") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSAH_DT" runat="server" Text='<%# Bind("SAH_DT", "{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Account Number">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSAH_ACC_NO" runat="server" Text='<%# Bind("SAH_ACC_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Customer Code">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSAH_CUS_CD" runat="server" Text='<%# Bind("SAH_CUS_CD") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSAH_CUS_NAME" runat="server" Text='<%# Bind("SAH_CUS_NAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Address 1">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSAH_CUS_ADD1" runat="server" Text='<%# Bind("SAH_CUS_ADD1") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Address 2">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSAH_CUS_ADD2" runat="server" Text='<%# Bind("SAH_CUS_ADD2") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Invoice Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSAH_INV_TP" runat="server" Text='<%# Bind("SAH_INV_TP") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="So #" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSAH_REF_DOC" runat="server" Text='<%# Bind("SAH_REF_DOC") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Entry #" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSAH_ANAL_2" runat="server" Text='<%# Bind("SAH_ANAL_2") %>'></asp:Label>
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <%-- Transport Methode add 22Feb2018 --%>
                    <div class="row">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="col-sm-12">
                                    <asp:CollapsiblePanelExtender runat="server" ID="cpe" TargetControlID="pnlCollaps" CollapseControlID="btnColapse"
                                        ExpandControlID="btnColapse" Collapsed="true" CollapsedSize="0" ExpandedSize="200" ExpandedText="(Collapse...)" CollapsedText="(Expand...)">
                                    </asp:CollapsiblePanelExtender>
                                    <asp:Panel ID="pnlCollaps" runat="server">
                                        <div class="panel panel-default">
                                            <div class="panel panel-heading">
                                                Transport Method
                                            </div>
                                            <div class="panel panel-body">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-7 padding0">
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-3 padding0 labelText1 text-center">
                                                                        <div class="row">
                                                                            <div class="col-sm-12">
                                                                                Transport Method
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-sm-12">
                                                                                <div class="col-sm-12 labelText1" style="padding-left: 3px; padding-right: 3px;">
                                                                                    <asp:DropDownList ID="ddlTransportMe" AutoPostBack="true" OnSelectedIndexChanged="ddlTransportMe_SelectedIndexChanged" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-3 padding0 labelText1 text-center">
                                                                        <div class="row">
                                                                            <div class="col-sm-12">
                                                                                Service By
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-sm-12">
                                                                                <div class="col-sm-12 labelText1" style="padding-left: 3px; padding-right: 3px;">
                                                                                    <asp:DropDownList AutoPostBack="true" ID="ddlServiceBy" OnSelectedIndexChanged="ddlServiceBy_SelectedIndexChanged" AppendDataBoundItems="true" runat="server" CssClass="form-control">
                                                                                        <asp:ListItem Value="0" Text="--Select--" />
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div runat="server" id="divSubLvl" class="col-sm-2 padding0 labelText1 text-center">
                                                                        <div class="row">
                                                                            <div class="col-sm-12">
                                                                                <asp:Label ID="lblSubLvl" runat="server" />
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-sm-12">
                                                                                <div class="col-sm-10 padding0 labelText1">
                                                                                    <asp:UpdatePanel runat="server">
                                                                                        <ContentTemplate>
                                                                                            <asp:TextBox AutoPostBack="true" OnTextChanged="txtSubLvl_TextChanged" ID="txtSubLvl" CssClass="form-control" runat="server" />
                                                                                        </ContentTemplate>
                                                                                    </asp:UpdatePanel>
                                                                                </div>
                                                                                <div class="col-sm-2 labelText1" style="padding-left: 3px; padding-right: 3px;">
                                                                                    <asp:LinkButton ID="lbtnSeVehicle" CausesValidation="false" runat="server" OnClick="lbtnSeVehicle_Click">
                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-4 labelText1 text-center">
                                                                        <div class="row">
                                                                            <div class="col-sm-12">
                                                                                Remarks
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-sm-12 labelText1" style="padding-right: 10px; padding-left: 10px;">
                                                                                <asp:TextBox ID="txtRemarksTrans" CssClass="form-control" runat="server" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-2 " runat="server" id="divNxtLvlD1">
                                                            <div class="row">
                                                                <div class="col-sm-12 labelText1">
                                                                    <asp:Label ID="lblNxtLvlD1" Text="" runat="server" />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-9 padding0" style="margin-top: -1px;">
                                                                        <asp:UpdatePanel runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:TextBox ID="txtNxtLvlD1" AutoPostBack="true" OnTextChanged="txtNxtLvlD1_TextChanged" CssClass="form-control" runat="server" />
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                    <div class="col-sm-3 padding3">
                                                                        <asp:LinkButton ID="lbtnSeDriver" CausesValidation="false" runat="server" OnClick="lbtnSeDriver_Click">
                                                                               <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-2 padding0" runat="server" id="divNxtLvlD2">
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-2 padding0 labelText1">
                                                                        <asp:Label ID="lblNxtLvlD2" Text="" runat="server" CssClass="labelText1" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-9 padding0" style="margin-top: -1px;">
                                                                        <asp:UpdatePanel runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:TextBox ID="txtNxtLvlD2" AutoPostBack="true" OnTextChanged="txtNxtLvlD2_TextChanged" CssClass="form-control" runat="server" />
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                    <div class="col-sm-3 padding3">
                                                                        <asp:LinkButton ID="lbtnSeHelper" CausesValidation="false" runat="server" OnClick="lbtnSeHelper_Click">
                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                        </asp:LinkButton>
                                                                    </div> 
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-1 padding0">
                                                            <div class="col-sm-9 paddingRight0">
                                                                <div class="row">
                                                                    <div class="col-sm-12 labelText1 text-center padding0">
                                                                        Nof packing
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12 " style="margin-top: -1px;">
                                                                        <asp:TextBox ID="txtNoOfPacking" CssClass="txtNoOfPacking form-control" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-3 padding0 labelText1">
                                                                <div class="buttonRow">
                                                                    <div style="margin-top: 10px;">
                                                                        <asp:LinkButton ID="lbtnAdd" runat="server" CausesValidation="false" CssClass="floatleft"
                                                                            OnClick="lbtnAdd_Click">
                                                                                <span class="glyphicon glyphicon-arrow-down" aria-hidden="true" ></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin-top: 2px;">
                                                    <div class="col-sm-12">
                                                        <div style="height: 125px; overflow-x: hidden; overflow-y: auto;">
                                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy5" runat="server"></asp:ScriptManagerProxy>
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <asp:GridView ID="dgvTrns" CssClass="table table-hover table-striped" runat="server"
                                                                        GridLines="None" PagerStyle-CssClass="cssPager"
                                                                        EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                                                        <EditRowStyle BackColor="MidnightBlue" />
                                                                        <Columns>

                                                                            <asp:TemplateField HeaderText="Tra. Method">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTraMe" Text='<%# Bind("Itrn_trns_pty_tp") %>' runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Service By">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSerBy" Text='<%# Bind("Itrn_trns_pty_cd") %>' runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblHedSubLvl" Text='Ref #' runat="server" />
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblItmSubLvl" Text='<%# Bind("Itrn_ref_no") %>' runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Remarks">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRemarks" Text='<%# Bind("Itrn_rmk") %>' runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblHedNxtLvlD1" Text='Driver/Hand Over' runat="server" />
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblItmNxtLvlD1" Text='<%# Bind("Itrn_anal1") %>' runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblHedNxtLvlD2" Text='Helper' runat="server" />
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblItmNxtLvlD2" Text='<%# Bind("Itrn_anal2") %>' runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRowNo" Text='<%# Bind("_grdRowNo") %>' Visible="false" runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <div style="margin-top: -3PX; width: 10px;">
                                                                                        <asp:LinkButton ID="lbtnDel" runat="server" CausesValidation="false"
                                                                                            OnClientClick="return ConfirmDelete()" OnClick="lbtnDel_Click">
                                                                                                    <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                        </Columns>
                                                                        <PagerStyle CssClass="cssPager"></PagerStyle>
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
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-default">
                                <div class="panel-heading" style="height: 22px;">
                                    <div class="col-sm-3">General Details</div>
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="col-sm-6">
                                            <asp:Label Text="lblInvoiceNo" ID="lblInvoiceNo" runat="server" Visible="false" />
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:Label Text="lblInvoiceDate" ID="lblInvoiceDate" runat="server" Visible="false" />
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-sm-5 padding0">
                                                <div class="col-sm-4 padding0">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <div class="col-sm-3 labelText1 padding0">
                                                                Date 
                                                            </div>
                                                            <div class="col-sm-6 padding0">
                                                                <asp:TextBox ID="dtpDODate" Enabled="false" CausesValidation="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft3">
                                                                <asp:LinkButton ID="btnDoDAte" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="dtpDODate" Animated="true"
                                                                    PopupButtonID="btnDoDAte" Format="dd/MMM/yyyy">
                                                                </asp:CalendarExtender>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-sm-6 paddingRight0">
                                                    <asp:FileUpload ID="fileupexcelupload" runat="server" />
                                                </div>
                                                <div class="col-sm-2 padding0">
                                                    <asp:Button Text="Upload Serial(s)" ID="btnUploadSerials" runat="server" OnClick="btnUploadSerials_Click" />
                                                </div>
                                            </div>
                                            <div class="col-sm-2 text-center">
                                                <%-- <asp:LinkButton ID="lbtnTransportMth" runat="server" CausesValidation="false" OnClick="lbtnTransportMth_Click">Transport Method
                                                         <span class="glyphicon" aria-hidden="true"></span>
                                                </asp:LinkButton>--%>
                                            </div>
                                            <div class="col-sm-1 text-center">
                                                <asp:Button Text="Send to scan" ID="Button3" runat="server" OnClick="btnSentScan_Click" />
                                                <asp:LinkButton ID="lbtnentry" Visible="false" runat="server" CausesValidation="false" OnClick="lbtnentry_Click">Entry No
                                                         <span class="glyphicon" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>

                                            <div class="col-sm-4 paddingRight0">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <div class="col-sm-7 padding0">
                                                            <div class="col-sm-4 paddingRight0">
                                                                <asp:TextBox ID="textBox1" CssClass="form-control" ReadOnly="true" Text="Invoice No :" runat="server" />
                                                            </div>
                                                            <div class="col-sm-8 paddingLeft3">
                                                                <asp:TextBox ID="txtInvcNo" runat="server" CssClass="form-control" ReadOnly="true" />
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-5 padding0">
                                                            <div class="col-sm-5 padding0">
                                                                <asp:TextBox ID="textBox2" Text="Invoice Date :" runat="server" CssClass="form-control" ReadOnly="true" />
                                                            </div>
                                                            <div class="col-sm-7 paddingRight0 paddingLeft3">
                                                                <asp:TextBox ID="txtInvcDate" runat="server" CssClass="form-control" ReadOnly="true" />
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <div class="col-sm-2">
                                                        <div class="row">
                                                            <div class="col-sm-4 padding0 labelText1">
                                                                Manual Ref No
                                                            </div>
                                                            <div class="col-sm-8 padding0">
                                                                <div class="col-sm-1">
                                                                    <asp:CheckBox ID="chkManualRef" Text="" runat="server" AutoPostBack="true" OnCheckedChanged="chkManualRef_CheckedChanged" />
                                                                </div>
                                                                <div class="col-sm-9 paddingLeft3 labelText1 paddingRight0">
                                                                    <asp:Label Text="By Manual Doc" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 paddingLeft0">
                                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtManualRefNo" />
                                                                <asp:Button Text="Get Serial(s)" ID="btnBOC" Visible="false" OnClick="btnBOC_Click" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-11 paddingLeft0 labelText1">
                                                                <div class="col-sm-1 padding0">
                                                                    <asp:CheckBox ID="chkEditAddress" AutoPostBack="true" Text="" OnCheckedChanged="chkEditAddress_CheckedChanged" runat="server" />
                                                                </div>
                                                                <div class="col-sm-10 paddingLeft3 ">
                                                                    <asp:Label Text="Edit delivery address" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-7">
                                                        <div class="row">
                                                            <div class="col-sm-9 labelText1">
                                                                <%--Customer--%>
                                                            </div>
                                                            <div class="col-sm-2 labelText1">
                                                                <asp:Label ID="lblentryno" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-1 labelText1">
                                                                Customer
                                                            </div>
                                                            <div class="col-sm-3 padding0">
                                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtCustCode" placeholder="" />
                                                            </div>
                                                            <div class="col-sm-7 paddingLeft3">
                                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtCustName" placeholder="" />
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-1 labelText1">
                                                                Address
                                                            </div>
                                                            <div class="col-sm-3 padding0">
                                                                <asp:TextBox runat="server" ReadOnly="true" CssClass="form-control" ID="txtCustAddress1" />
                                                            </div>
                                                            <div class="col-sm-7 paddingLeft3">
                                                                <asp:TextBox runat="server" ReadOnly="true" CssClass="form-control" ID="txtCustAddress2" />
                                                            </div>

                                                            <asp:Panel runat="server" Visible="false">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-4">
                                                                        Transport Method
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <asp:DropDownList ID="ddlDeliver" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDeliver_SelectedIndexChanged">
                                                                            <%-- <asp:ListItem Text=" - - Select - - " Value="0" Selected="True"></asp:ListItem>--%>
                                                                        </asp:DropDownList>
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
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-4">
                                                                        <asp:Label ID="lblVehicle" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtVehicleNo" />
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3 padding0">
                                                        <div class="row">
                                                            <div class="col-sm-1 labelText1">
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-2 labelText1 padding0">
                                                                    Remarks
                                                                </div>
                                                                <div class="col-sm-10 labelText1">
                                                                    <asp:TextBox runat="server" AutoPostBack="true" ID="txtRemarks" OnTextChanged="txtRemarks_TextChanged" Height="40px" TextMode="MultiLine" CssClass="txtRemarks form-control" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="panel panel-default">
                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <uc1:ucOutScan runat="server" ID="ucOutScan" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="row">

                                <div class="col-sm-12">
                                    <div class="panel panel-default ">
                                        <div class="panel-heading" style="height: 22px;">
                                            Delivery Item Details
                                        </div>
                                        <div class="panel-body">
                                            <div class="bs-example">
                                                <ul class="nav nav-tabs" id="myTab">
                                                    <li class="active"><a href="#Item" data-toggle="tab">Item</a></li>
                                                    <li><a href="#Serial" data-toggle="tab">Serial</a></li>
                                                    <li><a href="#SelectedVouchers" data-toggle="tab">Selected Vouchers</a></li>
                                                    <div class="row">
                                                        <div class="col-sm-9">
                                                            <div class="col-sm-8">
                                                            </div>
                                                            <div class="col-sm-4 padding0">
                                                                <div class="col-sm-1 labelText1">
                                                                    <asp:CheckBox Text="" ID="chkChangeSimilarItem" Style="float: right" runat="server" />
                                                                </div>
                                                                <div class="col-sm-8 labelText1 paddingLeft3">
                                                                    <asp:Label Text="Change Similar Items" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ul>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="tab-content">
                                                <div class="tab-pane active" id="Item">
                                                    <div style="height: 100px; overflow-x: hidden; overflow-y: auto;">
                                                        <asp:GridView ID="dvDOItems" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                            EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnAddSerials" runat="server" CausesValidation="false" OnClick="btnAddSerials_Click">
                                                                                <span aria-hidden="true" class="glyphicon glyphicon-plus"></span>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Invoice No">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSAD_INV_NO" runat="server" Text='<%# Bind("SAD_INV_NO") %>' Visible="false"></asp:Label>
                                                                        <asp:LinkButton ID="btnSAD_INV_NO" runat="server" OnClick="btnSAD_INV_NO_Click" Text='<%# Bind("SAD_INV_NO") %>'></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="#" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsad_itm_line" runat="server" Text='<%# Bind("sad_itm_line") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Item">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSad_itm_cd" runat="server" Text='<%# Bind("Sad_itm_cd") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Similar Item ">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSad_sim_itm_cd" runat="server" Text='<%# Bind("Sad_sim_itm_cd") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Description">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMi_longdesc" runat="server" Text='<%# Bind("Mi_longdesc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="P.No">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMi_part_no" runat="server" Text='<%# Bind("Mi_part_no") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Model">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMi_model" runat="server" Text='<%# Bind("Mi_model") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Status">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSad_itm_stus" runat="server" Visible="false" Text='<%# Bind("Sad_itm_stus") %>'></asp:Label>
                                                                        <asp:Label ID="lblSad_itm_stus_desc" runat="server" Text='<%# Bind("Sad_itm_stus_desc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Invoice Qty">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSad_qty" runat="server" Text='<%# Bind("Sad_qty","{0:N2}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                    <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Bond Qty">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCus_ITM_QTY" runat="server" Text='<%# Bind("Cus_ITM_QTY","{0:N2}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                    <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Delivered Qty">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSad_do_qty" runat="server" Text='<%# Bind("Sad_do_qty","{0:N2}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                    <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Pick Qty">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPickQty" runat="server" Text='<%# Bind("SAD_SRN_QTY","{0:N2}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                    <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTemp" runat="server" Text='' Width="10px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Warr Period">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSad_warr_period" runat="server" Text='<%# Bind("Sad_warr_period") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" Width="73px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTemp2" runat="server" Text='' Width="5px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Warr Remarks">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSad_warr_remarks" runat="server" Text='<%# Bind("Sad_warr_remarks") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Promo Code">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSad_promo_cd" runat="server" Text='<%# Bind("Sad_promo_cd") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Is Approved">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSad_isapp" Visible="false" runat="server" Text='<%# Bind("Sad_isapp") %>'></asp:Label>
                                                                        <asp:CheckBox Text=" " ID="chkSad_isapp" Enabled="false" runat="server" Checked='<%#Convert.ToBoolean(Eval("Sad_isapp")) %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Is Cover Note">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSad_iscovernote" Visible="false" runat="server" Text='<%# Bind("Sad_iscovernote") %>'></asp:Label>
                                                                        <asp:CheckBox Text=" " ID="chkSad_iscovernote" Enabled="false" runat="server" Checked='<%#Convert.ToBoolean(Eval("Sad_iscovernote")) %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Price Book">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsad_pbook" runat="server" Text='<%# Bind("sad_pbook") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Price Level">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsad_pb_lvl" runat="server" Text='<%# Bind("sad_pb_lvl") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Isre" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsad_res_line_no" runat="server" Text='<%# Bind("sad_res_line_no") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Isre" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsad_res_no" runat="server" Text='<%# Bind("sad_res_no") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbtnDFSSerial" runat="server" CausesValidation="false" OnClick="lbtnDFSSerial_Click">
                                                                                <span aria-hidden="true" class="glyphicon glyphicon-arrow-up"></span>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>

                                                <div class="tab-pane" id="Serial">
                                                    <div class="panel panel-default">
                                                        <div class="panel-body panelscollbar height150">
                                                            <asp:GridView ID="dvDOSerials" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sequence Number" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTUS_USRSEQ_NO" runat="server" Text='<%# Bind("TUS_USRSEQ_NO") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtngrdItemsDalete" runat="server" CausesValidation="false" OnClientClick="return SerialDeleteConfirm()" OnClick="lbtngrdItemsDalete_Click1">
                                                                                <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="BIN">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTUS_BIN" runat="server" Text='<%# Bind("TUS_BIN") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Item">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTUS_ITM_CD" runat="server" Text='<%# Bind("TUS_ITM_CD") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Description">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTUS_ITM_DESC" runat="server" Text='<%# Bind("TUS_ITM_DESC") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Model">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTUS_ITM_MODEL" runat="server" Text='<%# Bind("TUS_ITM_MODEL") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Brand">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTUS_ITM_BRAND" runat="server" Text='<%# Bind("TUS_ITM_BRAND") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Status">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTUS_ITM_STUS" runat="server" Visible="false" Text='<%# Bind("TUS_ITM_STUS") %>'></asp:Label>
                                                                            <asp:Label ID="lblTus_itm_stus_Desc" runat="server" Text='<%# Bind("Tus_itm_stus_Desc") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Serial 1">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTUS_SER_1" runat="server" Text='<%# Bind("TUS_SER_1") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Serial 2">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTUS_SER_2" runat="server" Text='<%# Bind("TUS_SER_2") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Warranty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTUS_WARR_NO" runat="server" Text='<%# Bind("TUS_WARR_NO") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Serial ID" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTUS_SER_ID" runat="server" Text='<%# Bind("TUS_SER_ID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="tab-pane" id="SelectedVouchers">
                                                    <asp:GridView ID="gvSGv" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                        EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnsgv_delete" runat="server" CausesValidation="false" OnClick="btnsgv_delete_Click">
                                                                                <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="sgv_item" HeaderText="Item" ReadOnly="true" />
                                                            <asp:BoundField DataField="sgv_status" HeaderText="Status" ReadOnly="true" />
                                                            <asp:BoundField DataField="sgv_book" HeaderText="Book" ReadOnly="true" />
                                                            <asp:BoundField DataField="sgv_page" HeaderText="Page" ReadOnly="true" />
                                                            <asp:BoundField DataField="sgv_attacheditem" HeaderText="Attached Item" ReadOnly="true" />
                                                            <asp:TemplateField HeaderText="Inv Line">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsgv_invline" runat="server" Text='<%# Bind("sgv_invline") %>'></asp:Label>
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

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>

        <%-- pnl search --%>
    <asp:UpdatePanel runat="server" ID="upSearch">
        <ContentTemplate>
            <asp:Button ID="btn1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupSearch" runat="server" Enabled="True" TargetControlID="btn1"
                PopupControlID="Panel3" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
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
    
    <asp:Panel runat="server" ID="Panel3" DefaultButton="lbtnSearch">
        <asp:UpdatePanel runat="server" ID="upPnlSerch">
            <ContentTemplate>
                <div runat="server" id="Div12" class="panel panel-primary" style="padding: 5px;">
                    <div class="panel panel-default" style="height: 350px; width: 700px;">
                        <div class="panel-heading" style="height: 25px;">
                            <div class="col-sm-10"></div>
                            <div class="col-sm-1">
                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="btnClose_Click">
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
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
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
                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtSerByKey" CausesValidation="false" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="linkbtnSearch_Click"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy6" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                        <ContentTemplate>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="linkbtnSearch" runat="server" OnClick="linkbtnSearch_Click">
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
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
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

    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnSearch" runat="server" Text="Button" Style="display: none" />
            <asp:ModalPopupExtender ID="mpSearch" runat="server" Enabled="True" TargetControlID="btnSearch"
                PopupControlID="pnlSearch" CancelControlID="btnSearchClose" PopupDragHandleControlID="divSearchHdr" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlSearch" DefaultButton="lbtnSearch" Style="display: none">
        <div runat="server" id="test" class="panel panel-default height400 width800">
            <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading" id="divSearchHdr">
                    <asp:LinkButton ID="btnSearchClose" runat="server" OnClick="btnSearchClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <div class="col-sm-11">
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12" id="Div3" runat="server">
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
                                    <asp:TextBox ID="txtSearchbyword" CausesValidation="false" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbyword_TextChanged"></asp:TextBox>
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
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
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
    </asp:Panel>

    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnSerialPick" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpPickSerial" runat="server" Enabled="True" TargetControlID="btnSerialPick"
                PopupControlID="pnlPickSerial" CancelControlID="btnPSPClose" PopupDragHandleControlID="divPSPHdr" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
       <asp:UpdateProgress ID="UpdateProgress7" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="UpdatePanel18">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWaidt10" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWadit10" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel18" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlPickSerial" Style="display: none">
                <div runat="server" id="Div4" class="panel panel-default height300 width700">
                    <div class="panel panel-default">
                        <div class="panel-heading" id="divPSPHdr">
                            <asp:LinkButton ID="btnPSPClose" runat="server" OnClick="btnPSPClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <div class="col-sm-11">
                                <b>Advanced Serial Search</b>
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12" id="Div5" runat="server">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:Label ID="lbliqty" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblpick" runat="server" Visible="false"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12" id="Div6" runat="server">
                                        <div class="col-sm-2 labelText1">
                                            Search by key
                                        </div>
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy11" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                            <ContentTemplate>
                                                <div class="col-sm-3 paddingRight5">
                                                    <asp:DropDownList ID="ddlSearchbykeyA" runat="server" class="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div class="col-sm-2 labelText1">
                                            Search by word
                                        </div>
                                        <div class="col-sm-4 paddingRight5">
                                            <asp:TextBox ID="txtSearchbywordA" CausesValidation="false" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbywordA_TextChanged"></asp:TextBox>
                                        </div>
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy12" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                            <ContentTemplate>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnSearchA" runat="server" OnClick="lbtnSearchA_Click">
                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtSearchbywordA" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-2">
                                        <asp:TextBox runat="server" ID="txtPopupQty" Text="0.00" CssClass="form-control" Visible="false" />
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Label Text="0.00" ID="lblPopupQty" runat="server" Visible="false" />
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Label Text="0.00" ID="lblScanQty" runat="server" Visible="false" />
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Label Text="0.00" ID="lblApprovedQty" runat="server" Visible="false" />
                                    </div>
                                    <div class="col-sm-5">
                                    </div>
                                    <div class="col-sm-1" style="float: right">
                                        <asp:Button ID="btnAdvanceAddItem" runat="server" CssClass="btn btn-primary btn-xs" Text="Add" OnClick="btnAdvanceAddItem_Click" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy13" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdAdSearch" AutoGenerateColumns="False" CausesValidation="false" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True"
                                                    runat="server"
                                                    GridLines="None" CssClass="table table-hover table-striped" PagerStyle-CssClass="cssPager" AllowPaging="True" PageSize="7"
                                                    OnSelectedIndexChanged="grdAdSearch_SelectedIndexChanged" OnPageIndexChanging="grdAdSearch_PageIndexChanging">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="selectchk" runat="server"
                                                                    OnCheckedChanged="selectchk_CheckedChanged" AutoPostBack="true"
                                                                    Checked='<%#Convert.ToBoolean(Eval("Tus_isSelect")) %>'
                                                                    Width="10px"></asp:CheckBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Serial 1">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTus_ser_1" runat="server" Text='<%# Bind("Tus_ser_1") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Serial 2">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTus_ser_2" runat="server" Text='<%# Bind("Tus_ser_2") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Warranty">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTus_warr_no" runat="server" Text='<%# Bind("Tus_warr_no") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTus_itm_stus" runat="server" Visible="false" Text='<%# Bind("Tus_itm_stus") %>'></asp:Label>
                                                                <asp:Label ID="lbtnTus_itm_stus_Desc2" runat="server" Text='<%# Bind("Tus_itm_stus_Desc") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="BIN">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTus_bin" runat="server" Text='<%# Bind("Tus_bin") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Serial ID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTus_ser_id" runat="server" Text='<%# Bind("Tus_ser_id") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Desc" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTus_itm_desc" runat="server" Text='<%# Bind("Tus_itm_desc") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Inward Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltus_doc_dt" runat="server" Text='<%# Bind("tus_doc_dt","{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Supplier">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltus_exist_supp" runat="server" Text='<%# Bind("tus_exist_supp") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="cssPager" />
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

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnmpgift" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpGiftVoucher" runat="server" Enabled="True" TargetControlID="btnmpgift"
                PopupControlID="pnlGiftVouchers" CancelControlID="btnGVCLose" PopupDragHandleControlID="divGiftHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlGiftVouchers" DefaultButton="lbtnSearch" Style="display: none;">
        <div runat="server" id="Div1" class="panel panel-default height400 width700">
            <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading" id="divGiftHeader">
                    <span>Gift Vouchers</span>
                    <asp:LinkButton ID="btnGVCLose" runat="server" OnClick="btnGVCLose_Click" Style="float: right">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <div class="col-sm-11">
                        <asp:Label Text="lblInvLine" ID="lblInvLine" Visible="false" runat="server" />
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12" id="Div2" runat="server">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-sm-4 paddingRight0">
                                    <div class="col-sm-4 paddingRight0 paddingLeft0">
                                        <div class="col-sm-4 paddingRight0 paddingLeft0">
                                            Item
                                        </div>
                                        <div class="col-sm-8 paddingRight0 paddingLeft0">
                                            <asp:Label ID="lblGvItem" Text="text" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-sm-6  paddingRight0 paddingLeft0">
                                        <div class="col-sm-4 paddingRight0 paddingLeft0">
                                            Status
                                        </div>
                                        <div class="col-sm-8 paddingRight0 paddingLeft0">
                                            <asp:Label ID="lblGvStatus" Text="text" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-8 paddingRight0 paddingLeft0">
                                    <div class="col-sm-4 paddingRight0 paddingLeft0">
                                        <div class="col-sm-4 paddingRight0 paddingLeft0">
                                            Qty
                                        </div>
                                        <div class="col-sm-8 paddingRight0 paddingLeft0">
                                            <asp:Label ID="lblGvQty" Text="text" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-sm-8 paddingLeft0">
                                        <div class="col-sm-3 paddingLeft0">
                                            Page
                                        </div>
                                        <div class="col-sm-8 paddingLeft0">
                                            <asp:TextBox CssClass="form-control" onkeydown="return jsDecimals(event);" runat="server" ID="txtPage" />
                                        </div>
                                        <div class="col-sm-1 paddingRight0">
                                            <asp:LinkButton ID="btnAddGv" runat="server" OnClick="btnAddGv_Click" Style="float: right">
                                            <span class="glyphicon glyphicon-plus" aria-hidden="true">Close</span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 height5">
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:GridView ID="gvGiftVoucher" AutoGenerateColumns="False" CausesValidation="false" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdAdSearch_SelectedIndexChanged" OnPageIndexChanging="grdAdSearch_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btngv_delete" runat="server" CausesValidation="false" OnClick="btngv_delete_Click">
                                                     <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("gv_item") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("gv_status") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Book">
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("gv_book") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Page">
                                            <ItemTemplate>
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("gv_page") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Attached Item">
                                            <ItemTemplate>
                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("gv_attacheditem") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Invoice Line">
                                            <ItemTemplate>
                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("gv_invline") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="cssPager" />
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="col-sm-12 height5">
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Button Text="Confirm" ID="btnConfirmGv" runat="server" OnClick="btnConfirmGv_Click" Style="float: right" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnmpDealerInvoice" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpDealerInvoice" runat="server" Enabled="True" TargetControlID="btnmpDealerInvoice"
                PopupControlID="pnlDealerInvoice" CancelControlID="btnDealerInvoiceClose" PopupDragHandleControlID="divpnlDealerInvoice" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlDealerInvoice" Style="display: none">
        <div runat="server" id="Div7" class="panel panel-default height400 width700">
            <div class="panel panel-default">
                <div class="panel-heading" id="divpnlDealerInvoice">
                    <span>Upload dealer invoices</span>
                    <asp:LinkButton ID="btnDealerInvoiceClose" runat="server" OnClick="btnDealerInvoiceClose_Click" Style="float: right">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <div class="col-sm-11">
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12" id="Div8" runat="server">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Invoice Number
                                </div>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtSCMInvcNo" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtSCMInvcNo_TextChanged" runat="server" />
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtSCMInvcDate" AutoPostBack="true" CssClass="form-control" ReadOnly="true" runat="server" />
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Customer Name
                                </div>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtSCMCustName" AutoPostBack="true" CssClass="form-control" ReadOnly="true" runat="server" />
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Customer Code
                                </div>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtSCMCustCode" AutoPostBack="true" CssClass="form-control" ReadOnly="true" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:UpdatePanel ID="UpdatePanel25" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnconfbox" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupConfBox" runat="server" Enabled="True" TargetControlID="btnconfbox"
                PopupControlID="pnlConfBox" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlConfBox" runat="server" align="center">
        <div runat="server" id="Div10" class="panel panel-info height120 width250">
            <asp:Label ID="Label6" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:ScriptManagerProxy ID="ScriptManagerProxy19" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                <ContentTemplate>

                    <div class="panel panel-heading">
                        <span>Alert</span>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblMssg" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblMssg1" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblMssg2" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblMssg3" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblMssg4" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height22">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-1">
                            </div>
                            <div class="col-sm-4">
                                <asp:Button ID="Button8" runat="server" Text="yes" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="Button1_Click" />
                            </div>
                            <div class="col-sm-2 ">
                                <asp:Button ID="Button9" runat="server" Text="No" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="Button2_Click" />
                            </div>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>
    <asp:UpdatePanel ID="up2" runat="server">
        <ContentTemplate>
            <div>
                <asp:Button ID="btn11" runat="server" Text="Button" Style="display: none;" />
                <asp:ModalPopupExtender ID="popupTransport" runat="server" Enabled="True" TargetControlID="btn11"
                    PopupControlID="pnlPop" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
                </asp:ModalPopupExtender>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlPop" Style="margin-top: -100px;">
        <div runat="server" class="panel panel-default height45 width700 ">
            <div class="">
                <div class="panel panel-default">
                    <div class="panel-heading height30">
                        <div class="col-sm-11">
                            <strong><b>Transport Method</b></strong>
                        </div>
                        <div class="col-sm-1">
                            <asp:LinkButton ID="lbtnCls" runat="server" OnClick="lbtnCls_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="panel panel-body">
                        <%--<uc1:ucTransportMethode runat="server" ID="ucTransportMethode" />--%>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>



    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnaddcpst" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpAddBondItems" runat="server" Enabled="True" TargetControlID="btnaddcpst"
                PopupControlID="pnlAddBondItems" CancelControlID="btnCloseItem" PopupDragHandleControlID="Div6" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel runat="server" ID="pnlAddBondItems" DefaultButton="lbtnSearch">
                <div class="panel panel-default width700 ">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-sm-11">
                                <strong>Bond Entry Details</strong>
                            </div>
                            <div class="col-sm-1">
                                <asp:LinkButton ID="btnCloseItem" runat="server">
                                       <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-sm-2">
                                    Search by key
                                </div>
                                <div class="col-sm-2">
                                    <asp:DropDownList ID="ddlbonds" runat="server" class="form-control">
                                        <asp:ListItem Text="Doc.#" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <asp:Panel runat="server" DefaultButton="btnnserbon">
                                    <div class="col-sm-4">

                                        <asp:TextBox ID="txtsearchbond" CssClass="form-control" runat="server" />
                                    </div>
                                    <div class="col-sm-2 paddingRight5">
                                        <asp:Button ID="btnnserbon" class="btn btn-warning btn-xs" runat="server" Text="Upload" Style="display: none;"
                                            OnClick="btnnserbon_Click" />
                                    </div>
                                </asp:Panel>

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height10">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 ">
                                <div class="panel panel-default">

                                    
                                    <div class="dvScroll" style="overflow-y: scroll; height: 230px;" onscroll="setScrollPosition(this.scrollTop);">
<%--                                    <div id="dvScroll" class="panel-body panelscollbar height230" onscroll="setScrollPosition(this.scrollTop);">--%>
                                        <asp:GridView ID="grdCUSDec" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No data found..." AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Add" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtngrdCusde" runat="server" CausesValidation="false" Width="5px" OnClick="lbtngrdCusde_Click">
                                                                        <span class="glyphicon glyphicon-arrow-down" aria-hidden="true" ></span>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_bond" AutoPostBack="true" runat="server" Checked="false" Width="5px" OnCheckedChanged="chk_bond_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Doc.#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcuh_doc_no" runat="server" Text='<%# Bind("cuh_doc_no") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cusdec #">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcuh_oth_no" runat="server" Text='<%# Bind("cuh_oth_no") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcuh_dt" runat="server" Text='<%# Bind("cuh_dt","{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Request #">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcuh_sun_req_no" runat="server" Text='<%# Bind("cuh_sun_req_no") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bond #">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcuh_sun_bond_no" runat="server" Text='<%# Bind("cuh_sun_bond_no") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height10">
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-12 ">
                                <div class="panel panel-default">
                                    <div class="panel-body panelscollbar height120">
                                        <asp:GridView ID="grdCusitem" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No data found..." AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Item">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcui_itm_cd" runat="server" Text='<%# Bind("cui_itm_cd") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcui_itm_desc" runat="server" Text='<%# Bind("cui_itm_desc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcuh_sun_bond_no" runat="server" Text='<%# Bind("cui_qty","{0:N2}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-12 height10">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-10">
                            </div>
                            <div class="col-sm-2">
                                <asp:Button ID="btncontinue" runat="server" Text="Continue" class="btn btn-default btn-sm" OnClick="btncontinue_Click" />
                            </div>
                        </div>
                    </div>





                </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>




    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btncus" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="MPPDA" runat="server" Enabled="True" TargetControlID="btncus"
                PopupControlID="CustomerPanel" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>

            <asp:Panel runat="server" ID="CustomerPanel">
                <div runat="server" id="Div9" class="panel panel-default height150 width525">
                    <asp:Label ID="Label14" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="btncClose" runat="server" CausesValidation="false" OnClick="btncClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12 paddingLeft0 paddingRight0">

                                <div class="row">

                                    <div class="col-sm-12">

                                        <div class="col-sm-12">
                                            <div class="row">

                                                <div class="col-sm-5 labelText1">
                                                    Document No
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtdocname" runat="server" TabIndex="1" CssClass="form-control"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12 height10">
                                            </div>
                                        </div>

                                        <div class="col-sm-12">
                                            <div class="row">

                                                <div class="col-sm-5 labelText1">
                                                    Loading Bay
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="ddlloadingbay" runat="server" TabIndex="2" CssClass="form-control"></asp:DropDownList>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12 height10">
                                            </div>
                                        </div>

                                        <div class="col-sm-12">
                                            <div class="row">

                                                <div class="col-sm-5">
                                                </div>

                                                <div class="col-sm-7">
                                                    <asp:Button ID="btnsend" runat="server" TabIndex="3" CssClass="btn-info form-control" Text="Send" OnClientClick="ConfirmSendToPDA();" OnClick="btnsend_Click" />
                                                </div>

                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12 height10">
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12 height10">
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

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnbonchnge" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="MPBONDCH" runat="server" Enabled="True" TargetControlID="btnbonchnge"
                PopupControlID="Bondchangepanal" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>

            <asp:Panel runat="server" ID="Bondchangepanal">
                <div runat="server" id="Div11" class="panel panel-default height150 width525">
                    <asp:Label ID="Label7" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="lbtnclosebondchnge" runat="server" CausesValidation="false" OnClick="lbtnclosebondchnge_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12 paddingLeft0 paddingRight0">

                                <div class="row">

                                    <div class="col-sm-12">

                                        <div class="col-sm-12">
                                            <div class="row">

                                                <div class="col-sm-5 labelText1">
                                                    Bond No
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtbondchno" runat="server" TabIndex="1" CssClass="form-control"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12 height10">
                                            </div>
                                        </div>

                                        <div class="col-sm-12">
                                            <div class="row">

                                                <div class="col-sm-5 labelText1">
                                                    Location
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtbondchloc" runat="server" TabIndex="1" CssClass="form-control"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12 height10">
                                            </div>
                                        </div>

                                        <div class="col-sm-12">
                                            <div class="row">

                                                <div class="col-sm-5">
                                                </div>

                                                <div class="col-sm-7">
                                                    <asp:Button ID="btnbondchnge" runat="server" TabIndex="3" CssClass="btn-info form-control" Text="Process" OnClick="btnbondchnge_Click" />
                                                </div>

                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12 height10">
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12 height10">
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

    <asp:UpdatePanel ID="UpdatePanel32" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button10" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="SbuPopup" runat="server" Enabled="True" TargetControlID="Button10"
                PopupControlID="pnlSBU" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel ID="pnlSBU" runat="server" align="center">
        <asp:Label ID="Label4" runat="server" Text="Label" Visible="false"></asp:Label>
        <asp:ScriptManagerProxy ID="ScriptManagerProxy17" runat="server"></asp:ScriptManagerProxy>
        <asp:UpdatePanel ID="UpdatePanel33" runat="server">
            <ContentTemplate>
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <span>Alert</span>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label5" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblbinMssg" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblbinMssg1" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label9" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label10" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height22">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-4">
                            </div>
                            <div class="col-sm-4">
                                <asp:Button ID="btnSbu" runat="server" Text="Ok" CausesValidation="false" class="btn btn-primary" OnClick="btnSbu_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>


    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="ModalPopupSimilerItem" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="pnlAddSimilerItems" CancelControlID="lbtnScolse" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

            <asp:Panel runat="server" ID="pnlAddSimilerItems">
                <div class="panel panel-default width700 ">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-sm-11">
                                <strong>Similer Item </strong>
                            </div>
                            <div class="col-sm-1">
                                <asp:LinkButton ID="lbtnScolse" runat="server">
                                       <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12 ">
                                <div class="panel panel-default">
                                    <div class="panel-body panelscollbar height120">
                                        <asp:GridView ID="grdsimilItem" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No data found..." AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Add" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnSgrdItem" runat="server" CausesValidation="false" Width="5px" OnClick="lbtnSgrdItem_Click">
                                                                        <span class="glyphicon glyphicon-arrow-down" aria-hidden="true" ></span>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Seq No" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="MISI_SEQ_NO" runat="server" Text='<%# Bind("MISI_SEQ_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Type" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="MISI_TP" runat="server" Text='<%# Bind("MISI_TP") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Company" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="MISI_COM" runat="server" Text='<%# Bind("MISI_COM") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Code" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="MISI_ITM_CD" runat="server" Text='<%# Bind("MISI_ITM_CD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Similar Item Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="MISI_SIM_ITM_CD" runat="server" Text='<%# Bind("MISI_SIM_ITM_CD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="MI_LONGDESC" runat="server" Text='<%# Bind("MI_LONGDESC") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Model">
                                                    <ItemTemplate>
                                                        <asp:Label ID="MI_MODEL" runat="server" Text='<%# Bind("MI_MODEL") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Brand">
                                                    <ItemTemplate>
                                                        <asp:Label ID="MI_BRAND" runat="server" Text='<%# Bind("MI_BRAND") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Valid From" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="MISI_FROM" runat="server" Text='<%# Bind("MISI_FROM") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Valid To" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="MISI_TO" runat="server" Text='<%# Bind("MISI_TO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Profit Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="MISI_PC" runat="server" Text='<%# Bind("MISI_PC") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Location" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="MISI_LOC" runat="server" Text='<%# Bind("MISI_LOC") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Document No" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="MISI_DOC_NO" runat="server" Text='<%# Bind("MISI_DOC_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Promotion Code" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="MISI_PROMO" runat="server" Text='<%# Bind("MISI_PROMO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height10">
                                <asp:Label ID="lblItemcode" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblitemLineno" runat="server" Visible="false"></asp:Label>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-12 height10">
                            </div>
                        </div>

                    </div>
                </div>

            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>


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
                <div runat="server" id="Div20" class="panel panel-default height400 width700">

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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
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
                                                <asp:GridView ID="grdResultD" CausesValidation="false" runat="server" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" GridLines="None" CssClass="table table-hover table-striped" PageSize="5" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResultD_SelectedIndexChanged" OnPageIndexChanging="grdResultD_PageIndexChanging">
                                                    <Columns>
                                                        <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10" />

                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>

                                <asp:UpdateProgress ID="UpdateProgress6" DisplayAfter="10"
                                    runat="server" AssociatedUpdatePanelID="UpdatePanel24">

                                    <ProgressTemplate>
                                        <div class="divWaiting">
                                            <asp:Label ID="lblWait6" runat="server"
                                                Text="Please wait... " />
                                            <asp:Image ID="imgWait6" runat="server"
                                                ImageAlign="Middle" ImageUrl="~/images/banners/hidden_progress_bar.gif" />
                                        </div>
                                    </ProgressTemplate>

                                </asp:UpdateProgress>
                            </div>
                        </div>

                    </div>

                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="Button4" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="MpserialPicker" runat="server" Enabled="True" TargetControlID="Button4"
                PopupControlID="pnlserial" PopupDragHandleControlID="divPrice" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlserial">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="panel panel-default width250">



                    <div class="panel-heading height30" id="divPrice" style="height: 30px;">
                        <div class="col-sm-10">
                        </div>
                        <div class="col-sm-2">
                            <asp:LinkButton ID="lbtnPriceClose" runat="server" Style="float: right">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="panel-body" style="height: 30px;">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Panel runat="server" DefaultButton="lbtnSerClick">
                                    <asp:Panel runat="server" ID="pnlserialDFS">

                                        <div class="col-sm-12">
                                            <div class="row">
                                                <div class="col-sm-2 labelText1 paddingLeft0">
                                                    Serial #
                                                </div>
                                                <div class="col-sm-10 labelText1 paddingLeft0">
                                                    <asp:TextBox ID="txtPriceEdit" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>

                                        </div>
                                    </asp:Panel>
                                    <asp:Panel runat="server" ID="pnlQtyDFS" Visible="false">

                                        <div class="col-sm-12">
                                            <div class="row">
                                                <div class="col-sm-2 labelText1 paddingLeft0">
                                                    QTY
                                                </div>
                                                <div class="col-sm-10 labelText1 paddingLeft0">
                                                    <asp:TextBox ID="txtqtyDFS" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>

                                        </div>
                                    </asp:Panel>
                                    <asp:LinkButton ID="lbtnSerClick" OnClick="lbtnSerClick_Click" runat="server" Style="display: none;">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>


    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="Button6" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="MdlSOA" runat="server" TargetControlID="Button6"
                PopupControlID="pnlSOApopup" CancelControlID="lbtnclosSOA" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="Panel1">
        <asp:ScriptManagerProxy ID="ScriptManagerProxy24" runat="server"></asp:ScriptManagerProxy>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div runat="server" id="pnlSOApopup" class="panel panel-default height400 width850">
                    <asp:Label ID="Label23" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading height30">
                            <div class="col-sm-11">
                                <strong>SOA Numbers</strong>
                            </div>
                            <div class="col-sm-1">
                                <asp:LinkButton ID="lbtnclosSOA" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12" id="Div27" runat="server">
                                <div class="row">
                                    <div class="col-sm-12">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-8">
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Button ID="btnsalesorder" Visible="false" runat="server" Text="Add Sales Order Item" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnsalesorder_Click" />
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Button ID="btnSOA" runat="server" Text="Add SOA Item" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnSOA_Click" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy26" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdSOA" CausesValidation="false" AutoGenerateColumns="false" runat="server" GridLines="None" CssClass="table table-hover table-striped" OnSelectedIndexChanged="grdResult_SelectedIndexChanged">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Select">
                                                            <%--  <HeaderTemplate >
                                                                <asp:CheckBox ID="allchk" runat="server" Width="5px" onclick="CheckAllgrdReq(this)"></asp:CheckBox>
                                                            </HeaderTemplate>--%>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chk_Soa" runat="server" onclick="CheckBoxCheck(this);" Checked="false" Width="5px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SOA #">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblitr_req_no" runat="server" Text='<%# Bind("itr_req_no") %>'></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SO #">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblitr_ref" runat="server" Text='<%# Bind("itr_ref") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
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


    <script src="<%=Request.ApplicationPath%>Js/jquery.toastmessage.js"></script>
    <script>
        //Sys.Application.add_load(fun);
        //function fun() {
        //    $(document).ready(function () {
        //        maintainScrollPosition();
        //    });
        //}
        Sys.Application.add_load(fun);
        function fun() {
            $(document).ready(function () {
                console.log('redy doc');
                console.log($('#<%=hfScrollPosition.ClientID%>').val());
                maintainScrollPosition();
            });
        }
    </script>
</asp:Content>
