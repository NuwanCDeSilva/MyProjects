<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="PerformaInvoice.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.PI.PerformaInvoice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
              && (charCode < 48 || charCode > 57))
                return false;
            return true;
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

        function ConfirmClear() {
            var selectedvalueOrd = confirm("Do you want to clear all details ?");
            if (selectedvalueOrd) {
                document.getElementById('<%=hdnClear.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdnClear.ClientID %>').value = "No";
            }
        };

        function ConfirmSave() {
            var selectedvalueOrdPlace = confirm("Do you want to save ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=hdnSave.ClientID %>').value = "Yes";
            }
            else {
                document.getElementById('<%=hdnSave.ClientID %>').value = "No";
            }
        };

        function ConfirmUpdate() {
            var selectedvalueOrdPlace = confirm("Do you want to Update ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=hdnUpdate.ClientID %>').value = "Yes";
            }
            else {
                document.getElementById('<%=hdnUpdate.ClientID %>').value = "No";
            }
        };

        function ConfirmApprove() {
            var selectedvalueOrdPlace = confirm("Do you want to Approve ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=hdnApprove.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdnApprove.ClientID %>').value = "No";
            }
        };

        function ConfirmCancel() {
            var selectedvalueOrdPlace = confirm("Do you want to cancel ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=hdnCancel.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdnCancel.ClientID %>').value = "No";
            }
        };

        function ConfirmItemDelete() {
            var selectedvalueOrdPlace = confirm("Do you want to delete ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=hdnItemDelete.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdnItemDelete.ClientID %>').value = "No";
            }
        };


        function ConfirmItemDeleteAll() {
            var selectedvalueOrdPlace = confirm("Do you want to delete all items ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=hdnItemDeleteAll.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdnItemDeleteAll.ClientID %>').value = "No";
            }
        };

        function CheckAllItems(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdInvoiceDetails.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        };

        function CheckAll2(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdInvoiceDetails.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <asp:HiddenField ID="hdnSave" runat="server" />
    <asp:HiddenField ID="hdnUpdate" runat="server" />
    <asp:HiddenField ID="hdnApprove" runat="server" />
    <asp:HiddenField ID="hdnCancel" runat="server" />
    <asp:HiddenField ID="hdnClear" runat="server" />
    <asp:HiddenField ID="hdnItemDelete" runat="server" />
    <asp:HiddenField ID="hdnItemDeleteAll" runat="server" />
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-sm-7  buttonrow">
                    <div id="divWarning" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">
                        <strong>Alert!</strong>
                        <asp:Label ID="lblWarning" runat="server"></asp:Label>
                        <asp:LinkButton ID="lbtnlWarning" runat="server" CausesValidation="false" CssClass="floatRight">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                        </asp:LinkButton>
                    </div>
                    <div id="divSuccess" runat="server" visible="false" class="alert alert-success alert-dismissible" role="alert">
                        <strong>Success!</strong>
                        <asp:Label ID="lblSuccess" runat="server"></asp:Label>
                        <asp:LinkButton ID="lbtnSuccess" runat="server" CausesValidation="false" CssClass="floatRight">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                        </asp:LinkButton>
                    </div>
                    <div id="divAlert" runat="server" visible="false" class="alert alert-info alert-dismissible" role="alert">
                        <strong>Alert!</strong>
                        <asp:Label ID="lblAlert" runat="server"></asp:Label>
                        <asp:LinkButton ID="lbtnAlert" runat="server" CausesValidation="false" CssClass="floatRight">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="col-sm-4  buttonRow">
                    <div class="col-sm-3 paddingRight0">
                        <asp:LinkButton ID="lbtnSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmSave()" OnClick="lbtnSave_Click">
                            <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                        </asp:LinkButton>
                    </div>
                    <div class="col-sm-3 paddingRight0">
                        <asp:LinkButton ID="lbtnUpdate" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmUpdate()" OnClick="lbtnUpdate_Click">
                            <span class="glyphicon glyphicon-edit" aria-hidden="true"></span>Update
                        </asp:LinkButton>
                    </div>
                    <div class="col-sm-3 paddingRight0">
                        <asp:LinkButton ID="lbtnCancel" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmCancel()" OnClick="lbtnCancel_Click">
                            <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>Cancel
                        </asp:LinkButton>
                    </div>
                    <div class="col-sm-3 paddingRight0">
                        <asp:LinkButton ID="lbtnClear" runat="server" CssClass="floatRight" OnClientClick="ConfirmClear()" OnClick="lbtnClear_Click">
                            <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="col-sm-1  buttonRow">
                    <div class="col-sm-12">
                        <div class="col-sm-12 paddingRight0">
                            <div class="dropdown">
                                <a id="dLabel" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
                                    <span class="glyphicon glyphicon-menu-hamburger"></span>
                                </a>
                                <div class="dropdown-menu menupopup" aria-labelledby="dLabel">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-sm-12">
                                                <asp:LinkButton ID="lbtnKitItem" CausesValidation="false" runat="server" OnClick="lbtnKitItem_Click">
                                                        <span class="glyphicon glyphicon-briefcase fontsize18" aria-hidden="true"></span>Kit Item
                                                </asp:LinkButton>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height10">
                                                </div>
                                            </div>
                                            <div class="col-sm-12 paddingRight0">
                                                <asp:LinkButton ID="lbtnExcelUpload" CausesValidation="false" runat="server" OnClick="lbtnExcelUpload_Click">
                                                        <span class="glyphicon glyphicon-upload fontsize18" aria-hidden="true"></span>Excel Upload
                                                </asp:LinkButton>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height10">
                                                </div>
                                            </div>
                                            <div class="col-sm-12">
                                                <asp:LinkButton ID="lbtnApprove" runat="server" OnClick="lbtnApprove_Click" OnClientClick="ConfirmApprove()">
                                                        <span class="glyphicon glyphicon-thumbs-up fontsize18" aria-hidden="true"></span>Approve
                                                </asp:LinkButton>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height10">
                                                </div>
                                            </div>
                                            <div class="col-sm-12">
                                                <asp:LinkButton ID="lbtnExport" runat="server" OnClick="lbtnExport_Click">
                                                        <span class="glyphicon glyphicon-download fontsize18" aria-hidden="true"></span>Export to Excel
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
        </ContentTemplate>
    </asp:UpdatePanel>


    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <strong>Proforma Invoice</strong>

                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12 ">
                            <div class="bs-example">
                                <ul class="nav nav-tabs" id="myTab">
                                    <li class="active"><a href="#ItemCategorization" data-toggle="tab">PI Details</a></li>
                                    <li><a href="#Fin" data-toggle="tab">Fin</a></li>
                                </ul>
                            </div>
                            <div class="col-sm-12 ">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="ItemCategorization">
                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class="col-sm-12 height10">
                                                    </div>
                                                </div>
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="panel panel-default">

                                                                    <div class="panel-body">
                                                                        <div class="row">
                                                                            <div class="col-sm-6">
                                                                                <div class="col-sm-6">
                                                                                    <div class="row">
                                                                                        <div class="col-sm-4 labelText1">
                                                                                            PI NO
                                                                                        </div>
                                                                                        <div class="col-sm-7 paddingRight5">
                                                                                            <asp:TextBox ID="txtPINo" OnTextChanged="txtPINo_TextChanged" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control"></asp:TextBox>
                                                                                        </div>
                                                                                        <div class="col-sm-1 paddingLeft0">
                                                                                            <asp:LinkButton ID="lbtnPINo" runat="server" OnClick="lbtnPINo_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-sm-12 height2">
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-sm-4 labelText1">
                                                                                            PI Date
                                                                                        </div>
                                                                                        <div class="col-sm-7 paddingRight5">
                                                                                            <asp:TextBox ID="txtPIDate" runat="server" class="form-control" ReadOnly="True"></asp:TextBox>
                                                                                            <asp:TextBox ID="txtPIStatus" runat="server" class="form-control" ReadOnly="True" Visible="false"></asp:TextBox>
                                                                                        </div>
                                                                                        <div class="col-sm-1 paddingLeft0">
                                                                                            <asp:LinkButton ID="lbtnPIDate" runat="server" CausesValidation="false" Enabled="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtPIDate"
                                                                                                PopupButtonID="lbtnPIDate" Format="dd/MMM/yyyy" Enabled="false">
                                                                                            </asp:CalendarExtender>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-sm-12 height2">
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-sm-4 labelText1">
                                                                                            Order Ref No
                                                                                        </div>
                                                                                        <div class="col-sm-8">
                                                                                            <asp:TextBox ID="txtOrderRefNo" CausesValidation="false" runat="server" class="form-control" ReadOnly="True"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-sm-12 height2">
                                                                                        </div>
                                                                                    </div>

                                                                                </div>
                                                                                <div class="col-sm-6">
                                                                                    <div class="row">
                                                                                        <div class="col-sm-4 labelText1">
                                                                                            Status
                                                                                        </div>
                                                                                        <div class="col-sm-7 paddingRight5">
                                                                                            <asp:TextBox ID="txtStatus" CausesValidation="false" ReadOnly="true" runat="server" class="form-control" ForeColor="Red"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-sm-4 labelText1">
                                                                                            Order Plan No
                                                                                        </div>
                                                                                        <div class="col-sm-7 paddingRight5">
                                                                                            <asp:TextBox ID="txtOrderPlanNo" AutoPostBack="true" OnTextChanged="txtOrderPlanNo_TextChanged" CausesValidation="false" ReadOnly="false" runat="server" class="form-control"></asp:TextBox>
                                                                                        </div>
                                                                                        <div class="col-sm-1 paddingLeft0">
                                                                                            <asp:LinkButton ID="lbtnOrderPlanNo" runat="server" OnClick="lbtnOrderPlanNo_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-sm-12 height3">
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-sm-4 labelText1">
                                                                                            Order Period 
                                                                                        </div>
                                                                                        <div class="col-sm-8 OrderPeriod">
                                                                                            <asp:GridView ID="grdOrderPeriod" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeader="false"
                                                                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                                                <Columns>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="chkOrderPeriod" runat="server" AutoPostBack="true" OnCheckedChanged="chkOrderPeriod_CheckedChanged" />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:BoundField HeaderText='YEAR' DataField="YEAR" />
                                                                                                    <asp:BoundField HeaderText='MONTH' DataField="MONTH" />
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:HiddenField ID="hdnOrderPeriod" runat="server" Value='<%# Bind("MONTHINT") %>' />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                            </div>
                                                                            <div class="col-sm-6 paddingLeft0">
                                                                                <div class="row">
                                                                                    <div class="col-sm-6">

                                                                                        <div class="row">
                                                                                            <div class="col-sm-12 height2">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="col-sm-4 labelText1">
                                                                                                Supplier
                                                                                            </div>
                                                                                            <div class="col-sm-7 paddingRight5">
                                                                                                <asp:TextBox ID="txtSupplier" ReadOnly="true" CausesValidation="false" runat="server" class="form-control"></asp:TextBox>
                                                                                            </div>
                                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                                <asp:LinkButton ID="lbtnSupplier" runat="server" OnClick="lbtnSupplier_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                                </asp:LinkButton>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="col-sm-12 height2">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="col-sm-4 labelText1">
                                                                                                Trade Terms
                                                                                            </div>
                                                                                            <div class="col-sm-8 paddingRight5">
                                                                                                <asp:DropDownList ID="ddlTradeTerms" runat="server" AutoPostBack="true" class="form-control" OnTextChanged="ddlTradeTerms_TextChanged">
                                                                                                </asp:DropDownList>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="col-sm-12 height2">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="col-sm-4 labelText1">
                                                                                                Mode of Shipment
                                                                                            </div>
                                                                                            <div class="col-sm-8 paddingRight5">
                                                                                                <asp:DropDownList ID="ddlModeofShipment" runat="server" class="form-control">
                                                                                                    <asp:ListItem Text=" - - Select - - " Value="0" Selected="True"></asp:ListItem>
                                                                                                    <asp:ListItem Text="Air" Value="A"></asp:ListItem>
                                                                                                    <asp:ListItem Text="Sea" Value="S"></asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="col-sm-12 height2">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="col-sm-4 labelText1">
                                                                                                Port of Origin
                                                                                            </div>
                                                                                            <div class="col-sm-8 paddingRight5">
                                                                                                <asp:DropDownList ID="ddlPortofOrigin" runat="server" class="form-control" AutoPostBack="true" OnTextChanged="ddlPortofOrigin_TextChanged">
                                                                                                    <asp:ListItem Text=" - - Select - - " Value="0" Selected="True"></asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="col-sm-12 height2">
                                                                                            </div>
                                                                                        </div>

                                                                                    </div>
                                                                                    <div class="col-sm-6">
                                                                                        <div class="row">
                                                                                            <div class="col-sm-4 labelText1">
                                                                                                Manual Ref No
                                                                                            </div>
                                                                                            <div class="col-sm-8">
                                                                                                <asp:TextBox ID="txtManualRefNo" CausesValidation="false" runat="server" class="form-control" MaxLength="50"></asp:TextBox>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="col-sm-12 height2">
                                                                                            </div>
                                                                                        </div>
                                                                                        <%--<div class="row">
                                                    <div class="col-sm-12 labelText1">
                                                        <asp:Label ID="lblSupplier" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height2">
                                                    </div>
                                                </div>--%>
                                                                                        <div class="row">
                                                                                            <div class="col-sm-4 labelText1">
                                                                                                Partial Shipment
                                                                                            </div>
                                                                                            <div class="col-sm-2 paddingRight5 height22">
                                                                                                <asp:CheckBox ID="chkPartialShipment" runat="server" />
                                                                                            </div>
                                                                                            <div class="col-sm-6 paddingLeft0">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="col-sm-12 height2">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="col-sm-4 labelText1">
                                                                                                ETD
                                                                                            </div>
                                                                                            <div class="col-sm-7 paddingRight5">
                                                                                                <asp:TextBox ID="txtETD" runat="server" class="form-control" AutoPostBack="true" OnTextChanged="txtETD_TextChanged"></asp:TextBox>
                                                                                            </div>
                                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                                <asp:LinkButton ID="lbtnETD" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                                                </asp:LinkButton>
                                                                                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtETD"
                                                                                                    PopupButtonID="lbtnETD" Format="dd/MMM/yyyy">
                                                                                                </asp:CalendarExtender>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="col-sm-12 height2">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="col-sm-4 labelText1">
                                                                                                ETA
                                                                                            </div>
                                                                                            <div class="col-sm-7 paddingRight5">
                                                                                                <asp:TextBox ID="txtETA" runat="server" class="form-control" ReadOnly="True"></asp:TextBox>
                                                                                            </div>
                                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                                <asp:LinkButton ID="lbtnETA" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                                                </asp:LinkButton>
                                                                                                <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtETA"
                                                                                                    PopupButtonID="lbtnETA" Format="dd/MMM/yyyy">
                                                                                                </asp:CalendarExtender>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="col-sm-12 height2">
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">

                                                                                    <div class="col-sm-2 labelText1">
                                                                                        Remark
                                                                                    </div>
                                                                                    <div class="col-sm-10">
                                                                                        <asp:TextBox ID="txtRemark" runat="server" class="form-control" Rows="1" TextMode="MultiLine"></asp:TextBox>
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
                                                                    <%-- <div class="panel-heading">Performa Invoice Details</div>--%>
                                                                    <div class="row">
                                                                        <div class="panel-body">
                                                                            <div class="col-sm-12">
                                                                                <div class="panel panel-default">
                                                                                    <div class="panel-heading">
                                                                                        <div class="row">
                                                                                            <div class="col-sm-6">
                                                                                                <div class="row">
                                                                                                    <div class="col-sm-4">
                                                                                                        <div class="row">
                                                                                                            <div class="col-sm-3 labelText1">
                                                                                                                Item
                                                                                                            </div>
                                                                                                            <div class="col-sm-8 paddingRight5">
                                                                                                                <asp:TextBox ID="txtItem" CausesValidation="false" runat="server" MaxLength="30" class="form-control" AutoPostBack="true" OnTextChanged="txtItem_TextChanged"></asp:TextBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                                                <asp:LinkButton ID="lbtnItem" runat="server" OnClick="lbtnItem_Click">
                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="col-sm-4">
                                                                                                        <div class="row">
                                                                                                            <div class="col-sm-3 labelText1">
                                                                                                                Model
                                                                                                            </div>
                                                                                                            <div class="col-sm-8 paddingRight5">
                                                                                                                <asp:TextBox ID="txtModel" CausesValidation="false" runat="server" MaxLength="30" class="form-control"></asp:TextBox>
                                                                                                            </div>
                                                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                                                <asp:LinkButton ID="lbtnModel" runat="server" OnClick="lbtnModel_Click">
                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                        </div>

                                                                                                    </div>
                                                                                                    <div class="col-sm-4">
                                                                                                        <div class="row">
                                                                                                            <div class="col-sm-4 labelText1">
                                                                                                                Item Type
                                                                                                            </div>
                                                                                                            <div class="col-sm-8 paddingRight5">
                                                                                                                <asp:DropDownList ID="ddlItemType" runat="server" class="form-control">
                                                                                                                    <asp:ListItem Text="Main" Value="M"></asp:ListItem>
                                                                                                                    <asp:ListItem Text="Component" Value="C"></asp:ListItem>
                                                                                                                    <asp:ListItem Text="Accessories" Value="A"></asp:ListItem>
                                                                                                                </asp:DropDownList>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-sm-6">
                                                                                                <div class="row">
                                                                                                    <div class="col-sm-4">
                                                                                                        <div class="row">
                                                                                                            <div class="col-sm-3 labelText1">
                                                                                                                Colour
                                                                                                            </div>
                                                                                                            <div class="col-sm-9 paddingRight5">
                                                                                                                <asp:TextBox ID="txtColour" CausesValidation="false" runat="server" MaxLength="30" class="form-control"></asp:TextBox>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="col-sm-3">
                                                                                                        <div class="row">
                                                                                                            <div class="col-sm-4 labelText1">
                                                                                                                Qty
                                                                                                            </div>
                                                                                                            <div class="col-sm-8 paddingRight5">
                                                                                                                <asp:TextBox ID="txtQty" CausesValidation="false" runat="server" MaxLength="10" class="form-control textAlignRight"></asp:TextBox>
                                                                                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtQty" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="col-sm-4">
                                                                                                        <div class="row">
                                                                                                            <div class="col-sm-4 labelText1">
                                                                                                                Unit Rate 
                                                                                                            </div>
                                                                                                            <div class="col-sm-8 paddingRight5">
                                                                                                                <asp:TextBox ID="txtUnitRate" CausesValidation="false" runat="server" MaxLength="12" class="form-control textAlignRight"></asp:TextBox>
                                                                                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtUnitRate" FilterType="Numbers, Custom" ValidChars="."></asp:FilteredTextBoxExtender>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="col-sm-1">
                                                                                                        <div class="row">
                                                                                                            <div class="col-sm-6 paddingRight5 paddingLeft5">
                                                                                                                <asp:LinkButton ID="lbtnAdd" runat="server" OnClick="lbtnAdd_Click">
                                                                                <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                                                                </asp:LinkButton>
                                                                                                            </div>
                                                                                                            <div class="col-sm-6 paddingRight5 paddingLeft5">
                                                                                                                <asp:LinkButton ID="lbtnClearItem" runat="server" OnClick="lbtnClearItem_Click">
                                                                                <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>
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
                                                                            <div class="col-sm-12">
                                                                                <div class="panel panel-default">
                                                                                    <%-- <div class="row">
                                                    <div class="panel-body">
                                                        <div class="col-sm-1 labelText1">Description</div>
                                                        <div class="col-sm-3 labelText1">
                                                            <asp:Label ID="lblDescription" runat="server" Text="" CssClass="Color1 fontWeight600"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-1 labelText1">Brand</div>
                                                        <div class="col-sm-3 labelText1">
                                                            <asp:Label ID="lblBrand" runat="server" Text="" CssClass="Color1 fontWeight600"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-1 labelText1">UOM</div>
                                                        <div class="col-sm-3 labelText1">
                                                            <asp:Label ID="lblUOM" runat="server" Text="" CssClass="Color1 fontWeight600"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>--%>
                                                                                </div>
                                                                            </div>
                                                                            <%--<div class="col-sm-12 GridScroll">--%>
                                                                            <div class="col-sm-12">
                                                                                <div class="panel-body panelscoll1" style="height: 120px">

                                                                                    <%--<div style ="height: 120px; overflow : scroll">--%>
                                                                                    <asp:GridView ID="grdInvoiceDetails" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                                        EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                                        <Columns>
                                                                                            <%--<asp:TemplateField HeaderText="" Visible="true">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="allchk" runat="server" Width="5px" onclick="CheckAll2(this)"></asp:CheckBox>
                                                          
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chk_Req" runat="server" Checked="false" Width="5px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                                                            <asp:TemplateField HeaderText="Item Code">
                                                                                                <EditItemTemplate>
                                                                                                    <asp:Label ID="lblIOI_ITM_CDEdit" runat="server" Text='<%# Bind("IOI_ITM_CD") %>'></asp:Label>
                                                                                                </EditItemTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblIOI_ITM_CD" runat="server" Text='<%# Bind("IOI_ITM_CD") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle Width="15%" />
                                                                                                <ItemStyle Width="15%" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Description">
                                                                                                <EditItemTemplate>
                                                                                                    <asp:Label ID="lblIOI_DESCEdit" runat="server" Text='<%# Bind("IOI_DESC") %>'></asp:Label>
                                                                                                </EditItemTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblIOI_DESC" runat="server" Text='<%# Bind("IOI_DESC") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle Width="15%" />
                                                                                                <ItemStyle Width="15%" />
                                                                                            </asp:TemplateField>
                                                <%--                                            <asp:TemplateField HeaderText="PartNo">
                                                                                                <EditItemTemplate>
                                                                                                    <asp:Label ID="lblIOI_PARTNOEdit" runat="server" Text='<%# Bind("MI_PART_NO") %>'></asp:Label>
                                                                                                </EditItemTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblIOI_PARTNO" runat="server" Text='<%# Bind("MI_PART_NO") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle Width="15%" />
                                                                                                <ItemStyle Width="15%" />
                                                                                            </asp:TemplateField>--%>
                                                                                            <asp:TemplateField HeaderText="Model">
                                                                                                <EditItemTemplate>
                                                                                                    <asp:Label ID="lblIOI_MODELEdit" runat="server" Text='<%# Bind("IOI_MODEL") %>'></asp:Label>
                                                                                                </EditItemTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblIOI_MODEL" runat="server" Text='<%# Bind("IOI_MODEL") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle Width="10%" />
                                                                                                <ItemStyle Width="10%" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Colour">
                                                                                                <EditItemTemplate>
                                                                                                    <asp:Label ID="lblIOI_COLOREdit" runat="server" Text='<%# Bind("IOI_COLOR") %>'></asp:Label>
                                                                                                </EditItemTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblIOI_COLOR" runat="server" Text='<%# Bind("IOI_COLOR") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle Width="10%" />
                                                                                                <ItemStyle Width="10%" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="UOM">
                                                                                                <EditItemTemplate>
                                                                                                    <asp:Label ID="lblIOI_UOMEdit" runat="server" Text='<%# Bind("IOI_UOM") %>'></asp:Label>
                                                                                                </EditItemTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblIOI_UOM" runat="server" Text='<%# Bind("IOI_UOM") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle Width="10%" />
                                                                                                <ItemStyle Width="10%" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Type">
                                                                                                <EditItemTemplate>
                                                                                                    <asp:Label ID="lblIOI_ITM_TPEdit" runat="server" Text='<%# Bind("IOI_ITM_TP") %>'></asp:Label>
                                                                                                </EditItemTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblIOI_ITM_TP" runat="server" Text='<%# Bind("IOI_ITM_TP") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle Width="7%" />
                                                                                                <ItemStyle Width="7%" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Order Qty">
                                                                                                <EditItemTemplate>
                                                                                                    <asp:TextBox ID="txtOrderQty" runat="server" Text='<%# Bind("IOI_BAL_QTY") %>'></asp:TextBox>
                                                                                                </EditItemTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblOrderQty" runat="server" Text='<%# Bind("IOI_BAL_QTY", "{0:N2}") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle CssClass="textAlignRight" Width="6%" />
                                                                                                <ItemStyle HorizontalAlign="Right" Width="6%" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Unit Price">
                                                                                                <EditItemTemplate>
                                                                                                    <asp:TextBox ID="txtUnitPrice" runat="server" Text='<%# Bind("IOI_UNIT_RT", "{0:N5}") %>'></asp:TextBox>
                                                                                                </EditItemTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblUnitPrice" runat="server" Text='<%# Bind("IOI_UNIT_RT", "{0:N5}") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle CssClass="textAlignRight" Width="7%" />
                                                                                                <ItemStyle HorizontalAlign="Right" Width="7%" />
                                                                                            </asp:TemplateField>
                                                                                            <%--<asp:BoundField HeaderText='IOI_YY' DataField="IOI_YY" Visible="false" />
                                                    <asp:BoundField HeaderText='IOI_MM' DataField="IOI_MM" Visible="false" />--%>
                                                                                            <asp:TemplateField HeaderText="Value">
                                                                                                <EditItemTemplate>
                                                                                                    <asp:Label ID="lblItemVlueEdit" runat="server" Text='<%# Bind("ITEMVLUE", "{0:N5}") %>'></asp:Label>
                                                                                                </EditItemTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblItemVlue" runat="server" Text='<%# Bind("ITEMVLUE", "{0:N5}") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle CssClass="textAlignRight" Width="10%" />
                                                                                                <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField ShowHeader="False">
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="lbtngrdInvoiceDetailsEdit" CausesValidation="false" runat="server" OnClick="lbtngrdInvoiceDetailsEdit_Click">
                                                                <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>
                                                                                                    </asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                                <EditItemTemplate>
                                                                                                    <asp:LinkButton ID="lbtngrdInvoiceDetailsUpdate" runat="server" CausesValidation="false" CommandName="Update" OnClick="lbtngrdInvoiceDetailsUpdate_Click">
                                                                <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                                                                    </asp:LinkButton>
                                                                                                    &nbsp;<asp:LinkButton ID="lbtngrdInvoiceDetailsCancel" runat="server" CausesValidation="false" CommandName="Cancel" OnClick="lbtngrdInvoiceDetailsCancel_Click">
                                                                <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>
                                                                                                    </asp:LinkButton>
                                                                                                </EditItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Right" Width="5%" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="lbtngrdInvoiceDetailsDalete" runat="server" CausesValidation="false" OnClientClick="ConfirmItemDelete()" OnClick="lbtngrdInvoiceDetailsDalete_Click">
                                                                <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                                                                                    </asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="" Visible="true">
                                                                                                <HeaderTemplate>
                                                                                                    <asp:LinkButton ID="lbtnDelAllItem" runat="server" CausesValidation="false" OnClientClick="ConfirmItemDeleteAll()" OnClick="lbtnDelAllItem_Click">
                                                                <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                                                                                    </asp:LinkButton>

                                                                                                </HeaderTemplate>
                                                                                            </asp:TemplateField>

                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-12">
                                                                                <div class="panel panel-default">
                                                                                    <div class="panel-heading">
                                                                                        <div class="row">
                                                                                            <div class="col-sm-12">
                                                                                                <div class="col-sm-8">
                                                                                                    <div class="col-sm-1 labelText1">Description</div>
                                                                                                    <div class="col-sm-3 labelText1">
                                                                                                        <asp:Label ID="lblDescription" runat="server" Text="" CssClass="Color1 fontWeight600"></asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-sm-1 labelText1">Brand</div>
                                                                                                    <div class="col-sm-1 labelText1">
                                                                                                        <asp:Label ID="lblBrand" runat="server" Text="" CssClass="Color1 fontWeight600"></asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-sm-1 labelText1">UOM</div>
                                                                                                    <div class="col-sm-1 labelText1">
                                                                                                        <asp:Label ID="lblUOM" runat="server" Text="" CssClass="Color1 fontWeight600"></asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-sm-1 labelText1">Currency -</div>
                                                                                                    <div class="col-sm-1 labelText1">
                                                                                                        <asp:Label ID="lblSCurrancy" runat="server" Text="" CssClass="Color1 fontWeight600"></asp:Label>
                                                                                                        <asp:Label ID="lblCompanyCurrancy" ForeColor="Red" Visible="false" runat="server" Text="Label"></asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-sm-1 labelText1">Rate -</div>
                                                                                                    <div class="col-sm-1 labelText1">
                                                                                                        <asp:Label ID="lblERate" runat="server" Text="" CssClass="Color1 fontWeight600"></asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-sm-4">
                                                                                                    <div class="row">
                                                                                                        <div class="col-sm-2 labelText1">
                                                                                                        </div>
                                                                                                        <div class="col-sm-2 labelText1">
                                                                                                            Total Qty
                                                                                                        </div>
                                                                                                        <div class="col-sm-2 paddingLeft0">
                                                                                                            <asp:TextBox ID="txtTotalOrderQty" CausesValidation="false" ReadOnly="true" runat="server" class="form-control textAlignRight"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-sm-2 labelText1 paddingLeft5">
                                                                                                            Total Value
                                                                                                        </div>
                                                                                                        <div class="col-sm-1 labelText1">
                                                                                                            <asp:Label ID="lblTotalOrderValue" runat="server" Text=""></asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-sm-3 paddingLeft0 paddingRight5">
                                                                                                            <asp:TextBox ID="txtTotalOrderValue" CausesValidation="false" ReadOnly="true" runat="server" class="form-control textAlignRight"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <%-- <div class="col-sm-1 labelText1">
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
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-5">
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <div class="panel panel-default">
                                                                            <div id="PanelBank" runat="server" class="panel-body">
                                                                                <div class="col-sm-12">
                                                                                    <div class="row">
                                                                                        <div class="col-sm-12 height5">
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
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4 paddingLeft0">
                                                                <div class="panel panel-default">
                                                                    <div class="panel-body">
                                                                        <div class="row">
                                                                            <div class="col-sm-12">
                                                                                <div class="panel-body panelscoll1" style="height: 60px">
                                                                                    <asp:GridView ID="grdElement" CssClass="table table-hover table-striped" runat="server" GridLines="None"
                                                                                        AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataText="No data found...">
                                                                                        <Columns>
                                                                                            <asp:BoundField HeaderText='Element Code' DataField="CODE" Visible="false" />
                                                                                            <asp:BoundField HeaderText='Element Code' DataField="DESCRIPTION" />
                                                                                            <asp:TemplateField HeaderText="Element Value">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtElementValue" CausesValidation="false" ReadOnly='<%# Bind("Enable") %>' Text='<%# Bind("Value", "{0:N2}") %>' runat="server" AutoPostBack="true" class="form-control textAlignRight"
                                                                                                        OnTextChanged="txtElementValue_TextChanged" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-sm-9">
                                                                                    </div>
                                                                                    <div class="col-sm-2">
                                                                                        <asp:Label ID="lblElemTotal" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-3 paddingLeft0">
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <div class="panel panel-default">
                                                                            <div class="panel-body">
                                                                                <div class="col-sm-12">
                                                                                    <div class="row">
                                                                                        <div class="col-sm-12 height5">
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <%--<div class="col-sm-4 labelText1">
                                                        Currency                                                            
                                                    </div>--%>
                                                                                        <div class="col-sm-4 labelText1">
                                                                                            <asp:Label ID="lblTotalAmount" runat="server" Text="" Visible="false"></asp:Label>
                                                                                            <%--<asp:Label ID="lblCurrency" runat="server" Text=""></asp:Label>--%>
                                                                                        </div>
                                                                                        <div class="col-sm-4 labelText1">
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-sm-12 height5">
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


                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="tab-pane" id="Fin">
                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class="col-sm-12 height10">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-1 labelText1">
                                                        Payment Terms
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <div class="row">
                                                            <div class="col-sm-6 paddingRight5">
                                                                <asp:DropDownList ID="ddlPaymentTerms" runat="server" class="form-control" AutoPostBack="true"
                                                                    OnTextChanged="ddlPaymentTerms_TextChanged">
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-sm-6 paddingLeft5">
                                                                <asp:DropDownList ID="ddlSubPaymentTerms" runat="server" class="form-control">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-1 labelText1">
                                                        Credit Period
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:TextBox ID="txtCreaditPeriod" CausesValidation="false" runat="server" MaxLength="3" class="form-control textAlignRight"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-1 labelText1">
                                                        Bank
                                                    </div>
                                                    <div class="col-sm-1 paddingRight5">
                                                        <asp:TextBox ID="txtBank" runat="server" class="form-control" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnBank" runat="server" OnClick="lbtnBank_Click">
                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-1 labelText1">
                                                        Account
                                                    </div>
                                                    <div class="col-sm-2 paddingRight5">
                                                        <asp:TextBox ID="txtAccount" runat="server" class="form-control" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnAccount" runat="server" OnClick="lbtnAccount_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>





                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
                            <asp:ModalPopupExtender ID="UserPopup" runat="server" Enabled="True" TargetControlID="Button3"
                                PopupControlID="pnlpopup" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
                            </asp:ModalPopupExtender>
                        </ContentTemplate>

                    </asp:UpdatePanel>
                    <asp:Panel runat="server" ID="pnlpopup" DefaultButton="lbtnSearch">
                        <div runat="server" id="test" class="panel panel-default height400 width700">

                            <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <asp:LinkButton ID="btnClose" runat="server" OnClick="btnClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                                    </asp:LinkButton>
                                    <%--<span>Commen Search</span>--%>
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
                    </asp:Panel>






                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:Button ID="btnkit" runat="server" Text="Button" Style="display: none;" />
                            <asp:ModalPopupExtender ID="mpkit" runat="server" Enabled="True" TargetControlID="btnkit"
                                PopupControlID="pnlpopupkit" CancelControlID="btnClose1" PopupDragHandleControlID="PopupHeaderKit" Drag="true" BackgroundCssClass="modalBackground">
                            </asp:ModalPopupExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel runat="server" ID="pnlpopupkitgrd">
                        <ContentTemplate>
                            <asp:HiddenField ID="txtconfirmclear" runat="server" />
                            <asp:HiddenField ID="txtconfirmplaceord" runat="server" />
                            <asp:HiddenField ID="txtapprove" runat="server" />
                            <asp:HiddenField ID="txtcancel" runat="server" />
                            <asp:Panel runat="server" ID="pnlpopupkit">
                                <div runat="server" id="DivKit" class="panel panel-primary Mheight">
                                    <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            <asp:LinkButton ID="btnClose1" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                                            </asp:LinkButton>
                                            <div class="col-sm-11">
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                        </div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div id="pnlscroll" class="panelscoll" style="height: 310px">
                                                        <asp:GridView ID="grdkititems" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped">
                                                            <EmptyDataTemplate>
                                                                <table class="table table-hover table-striped" border="1" style="border-collapse: collapse;" rules="all">
                                                                    <tbody>
                                                                        <tr>
                                                                            <th scope="col">Code
                                                                            </th>
                                                                            <th scope="col">Description
                                                                            </th>
                                                                            <th scope="col">Category 1
                                                                            </th>
                                                                            <th scope="col">Category 2
                                                                            </th>
                                                                            <th scope="col">Brand
                                                                            </th>
                                                                            <th scope="col">Model
                                                                            </th>
                                                                            <th scope="col">Part No
                                                                            </th>
                                                                            <th scope="col">Colour
                                                                            </th>
                                                                            <th scope="col">Item Type
                                                                            </th>
                                                                            <th scope="col">UOM
                                                                            </th>
                                                                            <th scope="col">Status
                                                                            </th>
                                                                            <th scope="col">Cost
                                                                            </th>
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
                                                                <asp:BoundField DataField="MI_CD" HeaderText="Code" ItemStyle-Width="100" ReadOnly="true" />
                                                                <asp:BoundField DataField="MI_SHORTDESC" HeaderText="Description" ItemStyle-Width="150" ReadOnly="true" />
                                                                <asp:BoundField DataField="MI_CATE_1" HeaderText="Category 1" ItemStyle-Width="100" ReadOnly="true" />
                                                                <asp:BoundField DataField="MI_CATE_2" HeaderText="Category 2" ItemStyle-Width="100" ReadOnly="true" />
                                                                <asp:BoundField DataField="MI_BRAND" HeaderText="Brand" ItemStyle-Width="100" ReadOnly="true" />
                                                                <asp:BoundField DataField="MI_MODEL" HeaderText="Model" ItemStyle-Width="100" ReadOnly="true" />
                                                                <asp:BoundField DataField="MI_PART_NO" HeaderText="Part No" ItemStyle-Width="150" ReadOnly="true" />
                                                                <asp:BoundField DataField="MI_COLOR_INT" HeaderText="Colour" ItemStyle-Width="100" ReadOnly="true" />
                                                                <asp:BoundField DataField="MI_ITM_TP" HeaderText="Item Type" ItemStyle-Width="150" ReadOnly="true" />
                                                                <asp:BoundField DataField="MI_ITM_UOM" HeaderText="UOM" ItemStyle-Width="100" ReadOnly="true" />
                                                                <asp:BoundField DataField="MI_ITM_STUS" HeaderText="Status" ItemStyle-Width="100" ReadOnly="true" />
                                                                <asp:BoundField DataField="MI_ITMTOT_COST" HeaderText="Cost" ItemStyle-Width="100" ReadOnly="true" ItemStyle-HorizontalAlign="Right" />
                                                            </Columns>
                                                        </asp:GridView>
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
                            <asp:Button ID="btnexcel" runat="server" Text="Button" Style="display: none;" />
                            <asp:ModalPopupExtender ID="mpexcel" runat="server" Enabled="True" TargetControlID="btnexcel"
                                PopupControlID="pnlpopupExcel" CancelControlID="btnClose2" PopupDragHandleControlID="PopupHeaderExcel" Drag="true" BackgroundCssClass="modalBackground">
                            </asp:ModalPopupExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:Panel runat="server" ID="pnlpopupExcel" DefaultButton="lbtnSearch">
                        <div runat="server" id="Div1" class="panel panel-primary Mheight">
                            <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <asp:LinkButton ID="btnClose2" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                                    </asp:LinkButton>
                                    <div class="col-sm-11">
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-sm-6">
                                                <div class="row">
                                                    <div class="col-sm-4 labelText1">
                                                        File Name
                                                    </div>
                                                    <div class="col-sm-8">
                                                        <asp:FileUpload ID="fileupexcelupload" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="row">
                                                    <div class="col-sm-4 labelText1">
                                                        <asp:RadioButtonList ID="rbHDR" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                                            <asp:ListItem Text="Yes" Value="Yes" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                    <div class="col-sm-8">
                                                        <asp:LinkButton ID="lbtnuploadexcel" runat="server" OnClick="lbtnuploadexcel_Click">
                                        <span class="glyphicon glyphicon-upload" aria-hidden="true">Upload</span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
                            <asp:ModalPopupExtender ID="dupicatepopup" runat="server" Enabled="True" TargetControlID="Button1"
                                PopupControlID="pnlduplicate" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
                            </asp:ModalPopupExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:Panel ID="pnlduplicate" runat="server" align="center">
                        <asp:Label ID="Label6" runat="server" Text="Label" Visible="false"></asp:Label>
                        <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="panel panel-default">
                                    <div class="panel-heading">
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
                                            <div class="col-sm-2">
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:Button ID="Button2" runat="server" Text="yes" CausesValidation="false" class="btn btn-primary" OnClick="Button1_Click" />
                                            </div>
                                            <div class="col-sm-4 ">
                                                <asp:Button ID="Button4" runat="server" Text="No" CausesValidation="false" class="btn btn-primary" OnClick="Button2_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                    <asp:UpdatePanel ID="UpdatePanel25" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button7" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="SbuPopup" runat="server" Enabled="True" TargetControlID="Button7"
                PopupControlID="pnlSBU" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel ID="pnlSBU" runat="server" align="center">
        <asp:Label ID="Label5" runat="server" Text="Label" Visible="false"></asp:Label>
        <asp:ScriptManagerProxy ID="ScriptManagerProxy11" runat="server"></asp:ScriptManagerProxy>
        <asp:UpdatePanel ID="UpdatePanel26" runat="server">
            <ContentTemplate>
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <span>Alert</span>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblSbuMsg1" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblSbuMsg2" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lMssg2" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label10" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label11" runat="server"></asp:Label>
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
</asp:Content>
