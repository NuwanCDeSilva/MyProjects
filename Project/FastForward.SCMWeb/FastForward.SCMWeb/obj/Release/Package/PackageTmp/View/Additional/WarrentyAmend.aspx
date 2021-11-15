<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="WarrentyAmend.aspx.cs" Inherits="FastForward.SCMWeb.View.Additional.WarrentyAmend" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">


    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/jquery.toastmessage.js"></script>
    <script type="text/javascript">

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
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
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
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
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

    <script>
        function ClearConfirm() {
            var selectedvalue = confirm("Do you want to clear data ?");
            if (selectedvalue) {
                document.getElementById('<%=hdfClearData.ClientID %>').value = "1";
            } else {
                document.getElementById('<%=hdfClearData.ClientID %>').value = "0";
            }
        };
        function SaveConfirm() {
            var selectedvalue = confirm("Do you want to save request ?");
            if (selectedvalue) {
                document.getElementById('<%=hdfSave.ClientID %>').value = "1";
            } else {
                document.getElementById('<%=hdfSave.ClientID %>').value = "0";
            }
        };

        function AproveConfirm() {
            var selectedvalue = confirm("Do you want to approve request ?");
            if (selectedvalue) {
                document.getElementById('<%=hdfApprove.ClientID %>').value = "1";
            } else {
                document.getElementById('<%=hdfApprove.ClientID %>').value = "0";
            }
        };
        function RejectConfirm() {
            var selectedvalue = confirm("Do you want to reject request ?");
            if (selectedvalue) {
                document.getElementById('<%=hdfReject.ClientID %>').value = "1";
            } else {
                document.getElementById('<%=hdfReject.ClientID %>').value = "0";
            }
        };
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
        }
    </script>

    <script>
        function CheckAllCompany(Checkbox) {
            // alert('S');
            var GridVwHeaderChckbox = document.getElementById("<%=dgvReqData.ClientID %>");
            for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
    <style>
        .panel {
            margin-bottom: 1px;
        }

        #GHead .table.table-hover.table-striped {
            margin-bottom: 0;
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
    <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <div class="row">
                <asp:HiddenField ID="hdfRowCountItems" runat="server" Value="0" />
                <div class="col-sm-12">
                    <div class="row buttonRow">
                        <div class="col-sm-8">
                        </div>
                        <div class="col-sm-4 padding0">
                            <div class="col-sm-2 padding0">
                            </div>
                            <div class="col-sm-10 padding0">
                                <div class="col-sm-3 padding0">
                                    <asp:LinkButton ID="lbtnRequest" runat="server" CausesValidation="false" CssClass="floatRight" OnClick="lbtnRequest_Click">
                                          <span class="glyphicon glyphicon-open" aria-hidden="true"></span>Request                                    
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-3 padding0">
                                    <asp:LinkButton ID="lbtnApprove" runat="server" CausesValidation="false" CssClass="floatRight" OnClick="lbtnApprove_Click">
                                          <span class="glyphicon glyphicon-thumbs-up" aria-hidden="true"></span>Approval                                   
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-3 padding0">
                                    <asp:LinkButton ID="lbtnReject" runat="server" CausesValidation="false" CssClass="floatRight" OnClick="lbtnReject_Click">
                                          <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>Reject                                   
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-3" >
                                    <asp:LinkButton ID="lbtnClear" OnClientClick="return ClearConfirm()" runat="server" CausesValidation="false" CssClass="floatRight" OnClick="lbtnClear_Click">
                                          <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear                                   
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-default">
                                <div class="panel panel-heading">
                                    <strong><b>Warranty Amend </b></strong>
                                </div>
                                <div class="panel panel-body padding0">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-sm-4 padding0">
                                                <div class="col-sm-1">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <asp:CheckBox ID="chkReq" runat="server" Text="" CssClass="height20" Checked="false"
                                                                AutoPostBack="true" OnCheckedChanged="chkReq_CheckedChanged" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-sm-8 labelText1">
                                                    <asp:Label ID="Label10" runat="server" Text="Search by Request" Visible="true"></asp:Label>
                                                </div>
                                            </div>
                                             <div class="col-sm-4 padding0">
                                                <div class="col-sm-1">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <asp:CheckBox ID="chkloc" runat="server" Text="" CssClass="height20" Checked="false"
                                                                AutoPostBack="true" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-sm-8 labelText1">
                                                    <asp:Label ID="Label1" runat="server" Text="Search by Other location" Visible="true"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                    </div>
                                    <asp:Panel runat="server" ID="panel1" Visible="<%# chkReq.Checked==false %>">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="panel panel-default">

                                                    <div class="panel panel-body" style="padding-left: 0px; padding-right: 0px;">
                                                        <div class="row">
                                                            <asp:Panel runat="server" DefaultButton="lbtnMainSearch1">
                                                                <div class="col-sm-11" style="padding-left: 0px; padding-right: 0px;">
                                                                    <div class="col-sm-3">
                                                                        <div class="col-sm-3 paddingRight0">
                                                                            Serial #
                                                                        </div>
                                                                        <div class="col-sm-6" style="padding-left: 0px; padding-right: 0px;">
                                                                            <asp:TextBox ID="txtSerial" AutoPostBack="true" runat="server" CssClass="form-control" OnTextChanged="txtSerial_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1" style="padding-left: 3px; padding-right: 0px;">
                                                                            <asp:LinkButton ID="lbtnSeSerial" CausesValidation="false" runat="server" OnClick="lbtnSeSerial_Click">
                                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                            </asp:LinkButton>
                                                                        </div>

                                                                    </div>
                                                                    <div class="col-sm-3">
                                                                        <div class="col-sm-4 paddingRight0">
                                                                            Warranty #
                                                                        </div>
                                                                        <div class="col-sm-7" style="padding-left: 0px; padding-right: 0px;">
                                                                            <asp:TextBox ID="txtWarNo" AutoPostBack="true" runat="server" CssClass="form-control" OnTextChanged="txtWarNo_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1" style="padding-left: 3px; padding-right: 0px;">
                                                                            <asp:LinkButton ID="lbtnSeWarrenty" CausesValidation="false" runat="server" OnClick="lbtnSeWarrenty_Click">
                                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                            </asp:LinkButton>
                                                                        </div>

                                                                    </div>
                                                                    <div class="col-sm-3">
                                                                        <div class="col-sm-4 paddingRight0">
                                                                            Invoice #
                                                                        </div>
                                                                        <div class="col-sm-6" style="padding-left: 0px; padding-right: 0px;">
                                                                            <asp:TextBox ID="txtInvoiceNo" AutoPostBack="true" runat="server" CssClass="form-control" OnTextChanged="txtInvoiceNo_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1" style="padding-left: 3px; padding-right: 0px;">
                                                                            <asp:LinkButton ID="lbtnSeInvoiceNo" CausesValidation="false" runat="server" OnClick="lbtnSeInvoiceNo_Click">
                                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                            </asp:LinkButton>
                                                                        </div>

                                                                    </div>
                                                                    <div class="col-sm-3">
                                                                        <div class="col-sm-4 paddingRight0">
                                                                            DO #
                                                                        </div>
                                                                        <div class="col-sm-6" style="padding-left: 0px; padding-right: 0px;">
                                                                            <asp:TextBox ID="txtDoNo" AutoPostBack="true" runat="server" CssClass="form-control" OnTextChanged="txtDoNo_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1" style="padding-left: 3px; padding-right: 0px;">
                                                                            <asp:LinkButton ID="lbtnSeDo" CausesValidation="false" runat="server" OnClick="lbtnSeDo_Click">
                                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                            </asp:LinkButton>
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-1">
                                                                    <div class="row buttonRow" style="height: 32px; margin-top: -6px;">
                                                                        <asp:LinkButton ID="lbtnMainSearch1" CausesValidation="false" runat="server" Visible="true" OnClick="lbtnMainSearch1_Click">
                                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div>
                                                                    <div id="GHead" class="GHead"></div>
                                                                    <div style="height: 150px; overflow: auto;">
                                                                        <asp:GridView ID="dgvItemDetails" CssClass="dgvItemDetails table table-hover table-striped" ShowHeaderWhenEmpty="true"
                                                                            EmptyDataText="No data found..." runat="server" GridLines="None" PagerStyle-CssClass="cssPager"
                                                                            AutoGenerateColumns="False">
                                                                            <EditRowStyle BackColor="MidnightBlue" />
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="">
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkSelectItem" runat="server" Width="15px" AutoPostBack="True" OnCheckedChanged="chkSelectItem_CheckedChanged"></asp:CheckBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Item Code">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblItemCode" Text='<%# Bind("ItemCode") %>' runat="server" Width="100px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Description">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblDescription" Text='<%# Bind("Description") %>' runat="server" Width="180px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Model">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblModel" Text='<%# Bind("Model") %>' runat="server" Width="100px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Serial #">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSerial" Text='<%# Bind("SerialNo") %>' runat="server" Width="100px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Warranty #">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblWarranty" Text='<%# Bind("WarrantyNo") %>' runat="server" Width="120px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="War. Period">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblWarrantyPeriod" Text='<%# Bind("WarrantyPeriod") %>' runat="server" Width="70px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="War. Remarks">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblWarrantyRemarks" Text='<%# Bind("WarrantyRemarks") %>' runat="server" Width="150px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Document #">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblDocumentNo" Text='<%# Bind("DocumentNo") %>' runat="server" Width="100px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Invoice #">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblInvoiceNo" Text='<%# Bind("InvoiceNo") %>' runat="server" Width="100px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Inv. Date">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="InvoiceDate" Text='<%# Bind("InvoiceDate","{0:dd/MMM/yyyy}") %>' runat="server" Width="80px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Cust. Code">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblCustomerCode" Text='<%# Bind("CustomerCode") %>' ToolTip='<%# Bind("Customer") %>' runat="server" Width="80px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSerialId" Visible="false" Text='<%# Bind("irsm_ser_id") %>' ToolTip='<%# Bind("Customer") %>' runat="server" Width="80px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblWarrantyStartDate" Visible="false" Text='<%# Bind("WarrantyStartDate") %>' runat="server" Width="80px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblCustomer" Text='<%# Bind("Customer") %>' Visible="false" runat="server" Width="80px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <PagerStyle CssClass="cssPager"></PagerStyle>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class=" col-sm-12">
                                                                <div class="panel panel-default">
                                                                    <div class="panel panel-heading">
                                                                        <strong><b>Change Warranty</b></strong>
                                                                    </div>
                                                                    <div class=" panel panel-body">

                                                                        <div class="col-sm-12">
                                                                            <div class="col-sm-11">
                                                                                <div class="col-sm-4" style="padding-left: 4px; padding-right: 2px;">
                                                                                    <div class="panel panel-default">

                                                                                        <div class="panel panel-body" style="padding-left: 4px; padding-right: 2px;">
                                                                                            <div class="row">
                                                                                                <div class="col-sm-12">
                                                                                                    <div class="col-sm-3 labelText1">
                                                                                                        Period
                                                                                                    </div>
                                                                                                    <div class="col-sm-5">
                                                                                                        <asp:TextBox ID="txtChangePeriod" onkeydown="return jsDecimals(event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="row">
                                                                                                <div class="col-sm-12">
                                                                                                    <div class="col-sm-3 labelText1">
                                                                                                        Remarks
                                                                                                    </div>
                                                                                                    <div class="col-sm-9">
                                                                                                        <asp:TextBox ID="txtRemarksPeriod" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-4" style="padding-left: 2px; padding-right: 2px;">
                                                                                    <div class="panel panel-default">
                                                                                        <div class="panel panel-body" style="padding-left: 2px; padding-right: 2px;">
                                                                                            <div class="row">
                                                                                                <div class="col-sm-12">
                                                                                                    <div class="col-sm-3 labelText1">
                                                                                                        Customer
                                                                                                    </div>
                                                                                                    <div class="col-sm-6">
                                                                                                        <asp:TextBox AutoPostBack="true" ID="txtCustomer" runat="server"
                                                                                                            CssClass="form-control" OnTextChanged="txtCustomer_TextChanged"></asp:TextBox>
                                                                                                    </div>
                                                                                                    <div class="col-sm-1" style="padding-left: 3px;">
                                                                                                        <asp:LinkButton ID="lbtnSeCustomer" CausesValidation="false" runat="server" OnClick="lbtnSeCustomer_Click">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                        </asp:LinkButton>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="row">
                                                                                                <div class="col-sm-12">
                                                                                                    <div class="col-sm-3 labelText1">
                                                                                                        Details
                                                                                                    </div>
                                                                                                    <div class="col-sm-9">
                                                                                                        <asp:TextBox Enabled="false" ID="txtCusDetails" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-4" style="padding-left: 2px; padding-right: 2px;">
                                                                                    <div class="panel panel-default">
                                                                                        <div class="panel panel-body" style="height: 84px;">
                                                                                            <div class="row">
                                                                                                <div class="col-sm-12">
                                                                                                    <div class="col-sm-6 labelText1">
                                                                                                        Warranty Com Date
                                                                                                    </div>
                                                                                                    <div class="col-sm-4 padding0">
                                                                                                        <asp:TextBox ID="txtComDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                    </div>
                                                                                                    <div class="col-sm-1" style="padding-left: 3px;">
                                                                                                        <asp:LinkButton ID="lbtnFromDate" CausesValidation="false" runat="server">
                                                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"  ></span>
                                                                                                        </asp:LinkButton>
                                                                                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtComDate"
                                                                                                            PopupButtonID="lbtnFromDate" Format="dd/MMM/yyyy">
                                                                                                        </asp:CalendarExtender>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="row">
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-1 ">
                                                                                <div class="row buttonRow">
                                                                                    <div class="col-sm-1" style="padding-left: 3px; padding-right: 0px;">
                                                                                        <asp:LinkButton ID="lbtnAddChnages" CausesValidation="false" runat="server" OnClick="lbtnAddChnages_Click">
                                                                                            <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"  ></span>
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
                                                            <div class="col-sm-12">
                                                                <div class="panel panel-default" style="height: 132px; overflow: auto;">
                                                                    <asp:GridView ID="dgvChanges" CssClass="table table-hover table-striped"
                                                                        ShowHeaderWhenEmpty="true" EmptyDataText="No data found..."
                                                                        runat="server" GridLines="None" PagerStyle-CssClass="cssPager"
                                                                        AutoGenerateColumns="False">
                                                                        <EditRowStyle BackColor="MidnightBlue" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Item Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblitem_code" Text='<%# Bind("item_code") %>' runat="server" Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Description">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDescription" Text='<%# Bind("desc") %>' runat="server" Width="250px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Model">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblModel" Text='<%# Bind("model") %>' runat="server" Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Serial #">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblrwr_ser_id" Text='<%# Bind("serialNo") %>' runat="server" Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Req By">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRwr_req_by" Text='<%# Bind("Rwr_req_by") %>' runat="server" Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Req Date">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblrwr_req_dt" Text='<%# Bind("rwr_req_dt","{0:dd/MMM/yyyy}") %>' runat="server" Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Warr. Period">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbl1" Text='<%# Bind("rwr_warr_period") %>' runat="server" Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Warr. Remarks">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbl2" Text='<%# Bind("rwr_warr_rmk") %>' runat="server" Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Customer">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCustomer" Text='<%# Bind("rwr_cust_cd") %>' runat="server" Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Com Date">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbl3" Text='<%# Bind("rwr_st_dt","{0:dd/MMM/yyyy}") %>' runat="server" Width="100px"></asp:Label>
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
                                    </asp:Panel>
                                    <asp:Panel runat="server" ID="panel2" Visible="false">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="panel panel-default">
                                                    <div class="panel panel-body" style="padding-left: 0px; padding-right: 0px;">
                                                        <div class="row">
                                                            <div class="col-sm-5" style="padding-left: 0px; padding-right: 0px;">
                                                                <div class="col-sm-6">
                                                                    <div class="col-sm-5">
                                                                        From Date
                                                                    </div>
                                                                    <div class="col-sm-6" style="padding-left: 0px; padding-right: 0px;">
                                                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1" style="padding-left: 3px; padding-right: 0px;">
                                                                        <asp:LinkButton ID="lbtnFrom" CausesValidation="false" runat="server">
                                                                              <span class="glyphicon glyphicon-calendar" aria-hidden="true"  ></span>
                                                                        </asp:LinkButton>
                                                                        <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtFromDate"
                                                                            PopupButtonID="lbtnFrom" Format="dd/MMM/yyyy">
                                                                        </asp:CalendarExtender>
                                                                    </div>

                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="col-sm-5">
                                                                        To Date
                                                                    </div>
                                                                    <div class="col-sm-6" style="padding-left: 0px; padding-right: 0px;">
                                                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1" style="padding-left: 3px; padding-right: 0px;">
                                                                        <asp:LinkButton ID="lbtnTo" CausesValidation="false" runat="server">
                                                                              <span class="glyphicon glyphicon-calendar" aria-hidden="true"  ></span>
                                                                        </asp:LinkButton>
                                                                        <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtToDate"
                                                                            PopupButtonID="lbtnTo" Format="dd/MMM/yyyy">
                                                                        </asp:CalendarExtender>
                                                                    </div>

                                                                </div>
                                                                <%-- <div class="col-sm-4">
                                                                    <div class="col-sm-5">
                                                                        Status
                                                                    </div>
                                                                    <div class="col-sm-6" style="padding-left: 0px; padding-right: 0px;">
                                                                        <asp:DropDownList ID="ddlReqTp" runat="server" CssClass="form-control">
                                                                            <asp:ListItem Text="All" />
                                                                            <asp:ListItem Text="Pending" />
                                                                            <asp:ListItem Text="Approved" />
                                                                            <asp:ListItem Text="Reject" />
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>--%>
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <div class="row buttonRow" style="height: 23px; margin-top: -6px;">
                                                                    <asp:LinkButton ID="lbtnMainSearch2" CausesValidation="false" runat="server" Visible="true" OnClick="lbtnMainSearch2_Click">
                                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-5">
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
                                                        <strong><b>Request Details</b></strong>
                                                    </div>
                                                    <div class="panel panel-body">
                                                        <div style="height: 200px; overflow: auto;">
                                                            <asp:GridView ID="dgvReqData" CssClass="table table-hover table-striped"
                                                                ShowHeaderWhenEmpty="true" EmptyDataText="No data found..."
                                                                runat="server" GridLines="None" PagerStyle-CssClass="cssPager"
                                                                AutoGenerateColumns="False">
                                                                <EditRowStyle BackColor="MidnightBlue" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="">
                                                                        <HeaderTemplate>
                                                                            <asp:CheckBox CssClass="chkboxSelectAllCom" ID="chkboxSelectAllCom" onclick="CheckAllCompany(this);" runat="server" AutoPostBack="False" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkSelectReq" runat="server" Width="10px" AutoPostBack="true"></asp:CheckBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSeqNo" Visible="false" Text='<%# Bind("rwr_seq") %>' runat="server" Width="0px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Item Code">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblItmCode" Text='<%# Bind("irsm_itm_cd") %>' runat="server" Width="100px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                   
                                                                    <asp:TemplateField HeaderText="Description">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDescription" Text='<%# Bind("irsm_itm_desc") %>' runat="server" Width="220px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Req. Warr. Per.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl1" Text='<%# Bind("rwr_warr_period") %>' runat="server" Width="90px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                        <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblEmpty1" Visible="false" Text='' runat="server" Width="10px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Req. Warr. Start Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl2" Text='<%# Bind("rwr_st_dt","{0:dd/MMM/yyyy}") %>' runat="server" Width="90px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Current. Warr. Start Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl3" Text='<%# Bind("irsm_warr_start_dt","{0:dd/MMM/yyyy}") %>' runat="server" Width="90px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Req. Customer">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl4" Text='<%# Bind("rwr_cust_cd") %>' ToolTip='<%# Bind("rwr_cust_name") %>' runat="server" Width="100px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Curr. Customer">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl5" Text='<%# Bind("irsm_cust_cd") %>' ToolTip='<%# Bind("irsm_cust_name") %>' runat="server" Width="100px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Request by">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl6" Text='<%# Bind("rwr_req_by") %>' runat="server" Width="100px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Request Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl7" Text='<%# Bind("rwr_req_dt","{0:dd/MMM/yyyy}") %>' runat="server" Width="100px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="">
                                                                        <ItemTemplate>
                                                                            <div style="margin-top: -3PX; margin-right:12px;">
                                                                                <asp:LinkButton ID="lbtnItemView"  runat="server" CausesValidation="false" Width="10px"
                                                                                     OnClick="lbtnItemView_Click">
                                                                                                 <span aria-hidden="true" class="">View</span>
                                                                                </asp:LinkButton>

                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <%-- Item data--%>
                                                                     <asp:TemplateField HeaderText="">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblModel" Visible="false" Text='<%# Bind("irsm_itm_model") %>' runat="server" Width="0px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblBrand" Visible="false" Text='<%# Bind("irsm_itm_brand") %>' runat="server" Width="0px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblItmStatus" Visible="false" Text='<%# Bind("itm_stus") %>' runat="server" Width="0px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSNo" Visible="false" Text='<%# Bind("irsm_ser_1") %>' runat="server" Width="0px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblWNo" Visible="false" Text='<%# Bind("irsm_warr_no") %>' runat="server" Width="0px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblWRemarks" Visible="false" Text='<%# Bind("irsm_warr_rem") %>' runat="server" Width="0px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblWarrStatus" Visible="false" Text='<%# Eval("irsm_warr_stus").ToString().Equals("Y")?"Yes":"No" %>' 
                                                                                 runat="server" Width="0px"></asp:Label>
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
                                            <asp:Panel runat="server" Visible="false" ID="panelItemDetails">
                                                <div class="col-sm-12">
                                                    <div class="panel panel-default">
                                                        <div class="panel panel-heading">
                                                            <strong><b>Item Details</b></strong>
                                                        </div>
                                                        <div class="panel panel-body padding0">
                                                            <div class="col-sm-4 paddingLeft0">
                                                                <div class="row">
                                                                    <div class=" col-sm-12">
                                                                        <div class="col-sm-4 labelText1">
                                                                            Item Model
                                                                        </div>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtModel" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class=" col-sm-12">
                                                                        <div class="col-sm-4 labelText1">
                                                                            Item Brand
                                                                        </div>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtBrand" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class=" col-sm-12">
                                                                        <div class="col-sm-4 labelText1">
                                                                            Item Status
                                                                        </div>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtStatus" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class=" col-sm-12">
                                                                        <div class="col-sm-4 labelText1">
                                                                            Serial #
                                                                        </div>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtSerNo" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class=" col-sm-12">
                                                                        <div class="col-sm-4 labelText1">
                                                                            Warrenty #
                                                                        </div>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtWarrNo" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4">
                                                                <div class="row">
                                                                    <div class=" col-sm-12">
                                                                        <div class="col-sm-4 labelText1">
                                                                            Warrenty Remarks
                                                                        </div>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtWarrRem" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class=" col-sm-12">
                                                                        <div class="col-sm-4 labelText1">
                                                                            Present Warranty Status 
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <asp:TextBox ID="txtPreWarSts" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>

                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel runat="server" Visible="true">
        <div>
            <asp:HiddenField ID="hdfSave" runat="server" />
            <asp:HiddenField ID="hdfApprove" runat="server" />
            <asp:HiddenField ID="hdfReject" runat="server" />
            <asp:HiddenField ID="hdfClearData" runat="server" />
            <!-- -->
            <%--<div class="row">
                <div class="col-sm-12 ">
                    <div class="col-sm-12">
                        <div class="col-sm-7">
                        </div>
                        <div class="col-sm-5">
                            <div class="row buttonRow">
                                <div class="col-sm-2">
                                </div>
                                <div class="col-sm-4 paddingRight0">
                                </div>

                                <div class="col-sm-6">
                                    <asp:Label ID="Label9" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:CheckBox ID="CheckBox2" runat="server" Text="" CssClass="height20" AutoPostBack="true"
                                       />
                                </div>

                            </div>
                        </div>
                    </div>

                    <div class="col-sm-9">
                    </div>
                    <div class="col-sm-4 buttonRow">
                    </div>

                </div>

            </div>--%>
            <!-- -->

            <%--<div class="panel panel-default">
                <div class="panel-body">
                    <div class="row ">
                        <div class="col-sm-12">
                            <div class="col-sm-1">
                            </div>
                            <div class="col-sm-1">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Text="Serial#"></asp:Label>
                                            <asp:Label ID="Label2" runat="server" Text="From" Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox10" runat="server" CssClass="form-control" Visible="false" Width="100px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="btnETD" runat="server" CausesValidation="false" Visible="false">
                                                                    <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TextBox10"
                                                PopupButtonID="btnETD" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>


                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"><span class="glyphicon glyphicon-search"></span></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                                &nbsp;
                            </div>
                            <div class="col-sm-1"></div>
                            <div class="col-sm-1">

                                <asp:Label ID="Label3" runat="server" Text="Warranty#"></asp:Label>
                                <asp:Label ID="Label4" runat="server" Text="To" Visible="false"></asp:Label>
                            </div>
                            <div class="col-sm-1">



                                <!-- -->

                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="TextBox11" runat="server" CssClass="form-control" Visible="false" Width="100px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton9" runat="server" CausesValidation="false" Visible="false">
                                                                    <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBox11"
                                                PopupButtonID="LinkButton9" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>


                                        </td>
                                    </tr>
                                </table>


                                <!-- -->
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click"><span class="glyphicon glyphicon-search"></span></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="col-sm-1"></div>
                            <div class="col-sm-1">

                                <asp:Label ID="Label5" runat="server" Text="Invoice#"></asp:Label>


                            </div>
                            <div class="col-sm-1">

                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click"><span class="glyphicon glyphicon-search"></span></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>


                            </div>


                            <div class="col-sm-1"></div>
                            <div class="col-sm-1">

                                <asp:Label ID="Label6" runat="server" Text="DO#"></asp:Label>
                            </div>
                            <div class="col-sm-1">

                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click"><span class="glyphicon glyphicon-search"></span></asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label7" runat="server" Width="20px"></asp:Label>
                                        </td>

                                        <td>
                                            <asp:LinkButton ID="LinkButton10" runat="server" OnClick="LinkButton10_Click"><span class="glyphicon glyphicon-search fontsize15"></span></asp:LinkButton>

                                        </td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton_date" Visible="false" runat="server" OnClick="LinkButton_date_Click"><span class="glyphicon glyphicon-search fontsize15"></span></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>


                            </div>

                            <!-- <div class="col-sm-1">
                            
                        </div> -->



                        </div>

                    </div>


                    <!-- create gridview -->

                    <div class="panel panel-default">



                        <div class="panel-body padding0">
                            <div class="row">
                                <div class="col-sm-12">

                                    <div class="col-sm-12">



                                        <asp:GridView ID="gvSubSerial" runat="server" GridLines="None" Style="border-collapse: collapse" CssClass="table table-hover table-striped" PageSize="4" PagerStyle-CssClass="cssPager" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="gvSubSerial_PageIndexChanging" OnSelectedIndexChanged="gvSubSerial_SelectedIndexChanged">

                                            <Columns>



                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-1">
                                                                    <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" ValidationGroup="g" OnCheckedChanged="checkbox1_changed" />
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:BoundField DataField="IRSM_ITM_CD" HeaderText="Item Code" />
                                                <asp:BoundField DataField="IRSM_ITM_DESC" HeaderText="Description" />
                                                <asp:BoundField DataField="IRSM_ITM_MODEL" HeaderText="Model" />

                                                <asp:BoundField DataField="irsm_ser_1" HeaderText="Serial#" />
                                                <asp:BoundField DataField="IRSM_WARR_NO" HeaderText="Warranty#" />

                                                <asp:BoundField DataField="IRSM_INVOICE_NO" HeaderText="Invoice#" />
                                                <asp:BoundField DataField="IRSM_INVOICE_DT" HeaderText="Invoice Date" DataFormatString="{0:d}" />
                                                <asp:BoundField DataField="IRSM_CUST_CD" HeaderText="Customer Code" />
                                                <asp:BoundField DataField="IRSM_CUST_NAME" HeaderText="Customer Name" />

                                                <asp:BoundField DataField="IRSM_DOC_NO" HeaderText="Do#" />
                                                <asp:BoundField DataField="IRSM_DOC_DT" HeaderText="Do Date" DataFormatString="{0:d}" />

                                                <asp:BoundField DataField="IRSM_WARR_PERIOD" HeaderText="Warranty Period" />
                                                <asp:BoundField DataField="IRSM_WARR_REM" HeaderText="Warranty Remarks" />
                                                <asp:BoundField DataField="irsm_ser_id" HeaderText="serial_id" />



                                            </Columns>
                                            <EmptyDataTemplate>

                                                <table border="0" style="border-collapse: collapse" class="table table-hover table-striped" rules="all">
                                                    <tr style="background-color: lightgrey;">
                                                        <th scope="col">Item Code
                                                        </th>
                                                        <th scope="col">Description
                                                        </th>
                                                        <th scope="col">Model
                                                        </th>

                                                        <th scope="col">Serial#
                                                        </th>
                                                        <th scope="col">Warranty#
                                                        </th>
                                                        <th scope="col">Invoice#
                                                        </th>
                                                        <th scope="col">Invoice Date
                                                        </th>
                                                        <th scope="col">Customer Code

                                                        </th>

                                                        <th scope="col">Customer Name
                                                        </th>

                                                        <th scope="col">Do#
                                                        </th>

                                                        <th scope="col">Do Date
                                                        </th>

                                                        <th scope="col">Warranty Period
                                                        </th>

                                                        <th scope="col">Warranty Remarks
                                                        </th>

                                                    </tr>
                                                    <tr>
                                                        <td colspan="13">No Results found.
                                                        </td>
                                                    </tr>

                                                </table>
                                            </EmptyDataTemplate>


                                        </asp:GridView>


                                        <!-- hide grid -->

                                        <asp:GridView ID="GridView2" runat="server" Visible="false" GridLines="None" Style="border-collapse: collapse" CssClass="table table-hover table-striped" PageSize="4" PagerStyle-CssClass="cssPager" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="GridView2_PageIndexChanging">

                                            <Columns>





                                                <asp:BoundField DataField="rwr_req_by" HeaderText="Request By" />
                                                <asp:BoundField DataField="rwr_req_dt" HeaderText="Request Date" DataFormatString="{0:d}" />
                                                <asp:BoundField DataField="rwr_req_session" HeaderText="Request Session" />

                                                <asp:BoundField DataField="rwr_ser_id" HeaderText="Serial ID" />
                                                <asp:BoundField DataField="rwr_stus" HeaderText="Status" />

                                                <asp:BoundField DataField="rwr_app_by" HeaderText="App BY" />
                                                <asp:BoundField DataField="rwr_app_dt" HeaderText="App Date" DataFormatString="{0:d}" />
                                                <asp:BoundField DataField="rwr_app_session" HeaderText="App Session" />
                                                <asp:BoundField DataField="rwr_warr_period" HeaderText="Warrenty period" />

                                                <asp:BoundField DataField="rwr_warr_rmk" HeaderText="Warrenty Remarks" />
                                                <asp:BoundField DataField="rwr_st_dt" HeaderText="Warrenty start date" DataFormatString="{0:d}" />

                                                <asp:BoundField DataField="rwr_cust_cd" HeaderText="customer code" />




                                            </Columns>
                                            <EmptyDataTemplate>

                                                <table border="0" style="border-collapse: collapse" class="table table-hover table-striped" rules="all">
                                                    <tr style="background-color: lightgrey;">
                                                        <th scope="col">Request By
                                                        </th>
                                                        <th scope="col">Request Date
                                                        </th>
                                                        <th scope="col">Request Session
                                                        </th>

                                                        <th scope="col">Serial ID
                                                        </th>
                                                        <th scope="col">Status
                                                        </th>
                                                        <th scope="col">App BY
                                                        </th>
                                                        <th scope="col">App Date
                                                        </th>
                                                        <th scope="col">App Session

                                                        </th>

                                                        <th scope="col">Warrenty period
                                                        </th>

                                                        <th scope="col">Warrenty Remarks
                                                        </th>

                                                        <th scope="col">Warrenty start date
                                                        </th>

                                                        <th scope="col">customer code
                                                        </th>



                                                    </tr>
                                                    <tr>
                                                        <td colspan="12">No Results found.
                                                        </td>
                                                    </tr>

                                                </table>
                                            </EmptyDataTemplate>


                                        </asp:GridView>

                                        <!-- -->




                                    </div>
                                </div>
                            </div>

                        </div>

                    </div>

                    <div class="row">
                        <div class="col-sm-12">
                            <div class="col-sm-8">
                            </div>
                            <div class="col-sm-2"></div>
                            <div class="col-sm-1"></div>
                            <div class="col-sm-1">
                                <!--        <asp:LinkButton ID="LinkButton7" runat="server" OnClick="LinkButton7_Click"><span class="glyphicon glyphicon-save fontsize15"></span> Save</asp:LinkButton> -->
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12 height10">
                        </div>
                    </div>



                    <!-- create gridview -->
                    <div class="row">
                        <div class="col-sm-12 padding0">
                            <div class="col-sm-4">
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-sm-12">

                                                <div class="col-sm-3">
                                                    Change Period
                                                </div>
                                                <div class="col-sm-1"></div>
                                                <div class="col-sm-1">

                                                    <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-sm-3">
                                                    Remarks
                                                </div>
                                                <div class="col-sm-1"></div>
                                                <div class="col-sm-1">

                                                    <asp:TextBox ID="TextBox6" runat="server" TextMode="MultiLine" CssClass="form-control" Width="100px"></asp:TextBox>

                                                </div>

                                            </div>
                                        </div>

                                    </div>

                                </div>

                            </div>

                            <div class="col-sm-4">
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                    <ContentTemplate>
                                                        <div class="row">

                                                            <div class="col-sm-3">
                                                                Customer
                                                            </div>
                                                            <div class="col-sm-2"></div>
                                                            <div class="col-sm-1">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="TextBox7" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:LinkButton ID="LinkButton13" runat="server" OnClick="LinkButton13_Click"> <span class="glyphicon glyphicon-search"></span> </asp:LinkButton></td>
                                                                    </tr>
                                                                </table>

                                                            </div>

                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>


                                            </div>

                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="row">
                                                    <div class="col-sm-5">
                                                        Details
                                                    </div>

                                                    <div class="col-sm-1">

                                                        <asp:TextBox ID="TextBox8" runat="server" TextMode="MultiLine" CssClass="form-control" Width="100px">

                                                        </asp:TextBox>

                                                    </div>


                                                </div>

                                            </div>
                                        </div>


                                    </div>
                                </div>



                            </div>

                            <!-- third -->


                            <div class="col-sm-4">
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-sm-12">

                                                <div class="col-sm-5">
                                                    Change Warranty Com Date
                                                </div>

                                                <div class="col-sm-1">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="TextBox9" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="LinkButton5" runat="server"><span class="glyphicon glyphicon-calendar"></span></asp:LinkButton>
                                                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="TextBox9"
                                                                    PopupButtonID="LinkButton5" Format="dd/MMM/yyyy">
                                                                </asp:CalendarExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>


                                    </div>

                                </div>

                            </div>

                            <!-- -->




                        </div>


                        <!-- second panel-->

                        <!-- -->




                    </div>

                    <!-- changes -->
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="col-sm-8">
                            </div>
                            <div class="col-sm-2"></div>
                            <div class="col-sm-1"></div>

                        </div>
                    </div>
                    <!-- -->


                    <div class="row">
                        <div class="col-sm-12 height10">
                        </div>
                    </div>

                    <!-- -->
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-12">

                                    <div class="col-sm-12">



                                        <asp:GridView ID="GridView1" runat="server" GridLines="None" Style="border-collapse: collapse" CssClass="table table-hover table-striped" AutoGenerateColumns="true">

                                            <Columns>
                                            </Columns>
                                            <EmptyDataTemplate>

                                                <table border="0" style="border-collapse: collapse" class="table table-hover table-striped" rules="all">
                                                    <tr style="background-color: lightgrey;">
                                                        <th scope="col"></th>
                                                        <th scope="col"></th>
                                                        <th scope="col"></th>

                                                        <th scope="col"></th>
                                                        <th scope="col"></th>
                                                        <th scope="col"></th>
                                                        <th scope="col"></th>

                                                    </tr>
                                                    <tr>
                                                        <td colspan="7"></td>
                                                    </tr>

                                                </table>
                                            </EmptyDataTemplate>


                                        </asp:GridView>







                                    </div>
                                </div>
                            </div>

                        </div>

                    </div>
                    <!-- -->





                </div>


            </div>--%>
        </div>

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div runat="server" style="width: 427px">
                    <asp:Button ID="ButtonBL" runat="server" Text="" Style="display: none;" />
                </div>
                <asp:ModalPopupExtender ID="UserBL" runat="server" Enabled="True" TargetControlID="ButtonBL"
                    PopupControlID="testPanelBL" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
                </asp:ModalPopupExtender>
            </ContentTemplate>
        </asp:UpdatePanel>


        <div>
            <asp:Panel runat="server" ID="testPanelBL" DefaultButton="ButtonBL">
                <div runat="server" id="Div1" class="panel panel-primary Mheight">

                    <asp:Label ID="Label8" runat="server" Text="Label" Visible="false"></asp:Label>


                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="LinkButton12" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <%--<span>Commen Search</span>--%>
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                            </div>
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
                                        <asp:DropDownList ID="DropDownList1" runat="server" class="form-control">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-sm-2 labelText1">
                                        Search by word 
                                    </div>

                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <%--onkeydown="return (event.keyCode!=13);"--%>
                                            <div class="col-sm-4 paddingRight5">
                                                <asp:TextBox ID="TextBox12" placeholder="Search by word" CausesValidation="false" class="form-control" AutoPostBack="true" runat="server"></asp:TextBox>
                                            </div>

                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="LinkButtonBLNOS" runat="server">
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

                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>

                                            <asp:GridView ID="BLLoad" CausesValidation="false" runat="server" AllowPaging="True" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" AutoGenerateColumns="true">
                                                <Columns>
                                                    <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10">
                                                        <ItemStyle Width="10px" />
                                                    </asp:ButtonField>

                                                </Columns>
                                                <HeaderStyle Width="10px" />
                                                <PagerStyle CssClass="cssPager" />
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-4" style="padding: 0px;">
                            <asp:LinkButton ID="LinkButton14" runat="server" OnClick="LinkButton7_Click">
                                <span class="glyphicon glyphicon-save"></span> Add

                            </asp:LinkButton>
                        </div>

                    </div>
                </div>
            </asp:Panel>
        </div>

    </asp:Panel>

    <!-- -->
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="SIPopup" runat="server" TargetControlID="Button3"
                PopupControlID="pnlpopup" PopupDragHandleControlID="divSearchheader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>



    <asp:Panel runat="server" ID="pnlpopup" DefaultButton="lbtnSearch">
        <asp:ScriptManagerProxy ID="ScriptManagerProxy17" runat="server"></asp:ScriptManagerProxy>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div runat="server" id="test" class="panel panel-default height400 width850">
                    <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading height30" id="divSearchheader">
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                                <asp:LinkButton ID="btnCloseSearchMP" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12" id="Div10" runat="server">
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
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdResult" CausesValidation="false" AllowPaging="true" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" OnSelectedIndexChanged="grdResult_SelectedIndexChanged" OnPageIndexChanging="grdResult_PageIndexChanging">
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <!-- -->

    <asp:UpdatePanel runat="server" ID="update">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupSearch" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="testPanel" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true"
                BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel runat="server" ID="testPanel" DefaultButton="lbtnSearch1">
        <div runat="server" id="Div2" class="panel panel-primary Mheight">
            <asp:Label ID="Label11" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default" style="height: 350px;">
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
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel99" runat="server">
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
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtSearchbyword1" CausesValidation="false" class="form-control" AutoPostBack="false" runat="server"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy5" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-1 paddingLeft0">
                                        <asp:LinkButton ID="lbtnSearch1" runat="server" OnClick="lbtnSearch1_Click">
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
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy6" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="dgvResult" CausesValidation="false" runat="server" AllowPaging="True" GridLines="None"
                                        CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" OnPageIndexChanging="dgvResult_PageIndexChanging" OnSelectedIndexChanged="dgvResult_SelectedIndexChanged">
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
    <script>
        if (typeof jQuery == 'undefined') {
            alert('jQuery is not loaded');
        }
        Sys.Application.add_load(fun);
        function fun() {
            jQuery('#BodyContent_dgvReqData tr td').on('click', function () {

                if (jQuery('#BodyContent_dgvReqData tr td input:checked').length == jQuery('#BodyContent_dgvReqData tr td input').length) {
                    jQuery('.chkboxSelectAllCom input').attr('checked', true);
                } else {
                    jQuery('.chkboxSelectAllCom input').attr('checked', false);
                }
            });
        }
    </script>



</asp:Content>
