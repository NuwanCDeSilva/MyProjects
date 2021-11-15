<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="AODDocumentAmendment.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Inventory.AODDocumentAmendment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
        function Enable() {
            return;
        };
        function CheckAllItems(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdItems.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        };

    </script>
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
                position: 'top-left',
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdnSave" runat="server" />
            <asp:HiddenField ID="hdnUpdate" runat="server" />
            <asp:HiddenField ID="hdnApprove" runat="server" />
            <asp:HiddenField ID="hdnCancel" runat="server" />
            <asp:HiddenField ID="hdnClear" runat="server" />
            <div class="panel panel-default marginLeftRight5 paddingbottom0">
                <div class="panel-body paddingtopbottom0">
                    <div class="row">
                        <div class="col-sm-7  buttonrow">
                            <div id="WarningGRN" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">
                                <div class="col-sm-11">
                                    <asp:Label ID="lblH1" runat="server" Text="Label"></asp:Label>
                                    <asp:Label ID="lblBackDateInfor" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div id="divWarning" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">
                                <strong>Warning!</strong>
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
                            </div>
                            <div class="col-sm-3 paddingRight0">
                                <%--<asp:LinkButton ID="lbtnCancel" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmCancel()" OnClick="lbtnCancel_Click">
                            <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>Cancel
                                </asp:LinkButton>--%>
                            </div>
                            <div class="col-sm-3 paddingRight0">
                                <%--<asp:LinkButton ID="lbtnUpdate" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmUpdate()" OnClick="lbtnUpdate_Click">
                            <span class="glyphicon glyphicon-edit" aria-hidden="true"></span>Update
                                </asp:LinkButton>--%>
                            </div>
                            <div class="col-sm-3 paddingRight0">
                                <asp:LinkButton ID="lbtnSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmSave()" OnClick="lbtnSave_Click">
                            <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-sm-1  buttonRow paddingLeft0">
                            <div class="col-sm-12 paddingLeft5">
                                <asp:LinkButton ID="lbtnClear" runat="server" CssClass="floatRight" OnClientClick="ConfirmClear()" OnClick="lbtnClear_Click">
                            <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">AOD Document Amendment</div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-5">
                                            <div class="col-sm-5">
                                                <div class="row">
                                                    <div class="col-sm-5 labelText1">
                                                        Date
                                                    </div>
                                                    <div class="col-sm-5 paddingRight5 paddingLeft0">
                                                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" onkeypress="return RestrictSpace()"
                                                            Format="dd/MMM/yyyy" Enabled="false"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-2 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnDate" runat="server" Visible="false" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDate"
                                                            PopupButtonID="lbtnDate" Format="dd/MMM/yyyy">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-7">
                                                <div class="row">
                                                    <div class="col-sm-5 labelText1">
                                                        Incorrect Location
                                                    </div>
                                                    <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                        <asp:TextBox ID="txtIncorrectLocation" CausesValidation="false" runat="server" class="form-control" AutoPostBack="true" OnTextChanged="txtIncorrectLocation_TextChanged"></asp:TextBox>
                                                        <asp:TextBox ID="txtOriginalIssueLoc" CausesValidation="false" runat="server" class="form-control" AutoPostBack="true" Visible="false"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnIncorrectLocation" runat="server" OnClick="lbtnIncorrectLocation_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="col-sm-12">
                                                <div class="row">
                                                    <div class="col-sm-2 labelText1">
                                                        Manual Ref
                                                    </div>
                                                    <div class="col-sm-10 paddingRight5 paddingLeft5">
                                                        <asp:TextBox ID="txtManualRef" CausesValidation="false" runat="server" MaxLength="50" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="col-sm-12">
                                                <div class="row">
                                                    <div class="col-sm-2 labelText1">
                                                        Other Ref
                                                    </div>
                                                    <div class="col-sm-10  paddingRight5 paddingLeft5">
                                                        <asp:TextBox ID="txtOtherRef" CausesValidation="false" runat="server" MaxLength="50" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-7 paddingLeft0">
                                            <div class="col-sm-4">
                                                <div class="row">
                                                    <div class="col-sm-4 labelText1 paddingLeft0">
                                                        Document No
                                                    </div>
                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                        <asp:TextBox ID="txtDocumentNo" CausesValidation="false" runat="server" class="form-control" AutoPostBack="true" OnTextChanged="txtDocumentNo_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnDocumentNo" runat="server" OnClick="lbtnDocumentNo_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-5">
                                                <div class="row">
                                                    <div class="col-sm-5 labelText1">
                                                        Correct Location
                                                    </div>
                                                    <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                        <asp:TextBox ID="txtCorrectLocation" CausesValidation="false" runat="server" class="form-control" AutoPostBack="true" OnTextChanged="txtCorrectLocation_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnCorrectLocation" runat="server" OnClick="lbtnCorrectLocation_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="row">
                                                    <div class="col-sm-3 labelText1">
                                                        Type
                                                    </div>
                                                    <div class="col-sm-9 paddingRight5 paddingLeft0">
                                                        <asp:DropDownList ID="ddlType" runat="server" class="form-control">
                                                            <asp:ListItem Text="- - Select - -" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Correction " Value="C"></asp:ListItem>
                                                            <asp:ListItem Text="Misplace" Value="M"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="col-sm-12">
                                                <div class="row">
                                                    <div class="col-sm-1 labelText1 paddingLeft0">
                                                        Remarks
                                                    </div>
                                                    <div class="col-sm-11 paddingRight5">
                                                        <asp:TextBox ID="txtRemarks" runat="server" MaxLength="200" class="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
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
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">AOD Items</div>
                                <div class="panel-body">
                                    <div class="bs-example">
                                        <ul class="nav nav-tabs" id="myTab">
                                            <li class="active"><a href="#Item" data-toggle="tab">Item</a></li>
                                            <li><a href="#Serial" data-toggle="tab">Serial</a></li>
                                        </ul>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>
                                    <div class="tab-content">
                                        <div class="tab-pane active" id="Item">
                                            <asp:GridView ID="grdItems" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkHeader" runat="server" onclick="CheckAllItems(this)" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkIyem" runat="server" Checked="false" />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                        <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="LineNo" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitri_line_no" runat="server" Text='<%# Bind("itri_line_no") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitri_itm_cd" runat="server" Text='<%# Bind("itri_itm_cd") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="25%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblmi_longdesc" runat="server" Text='<%# Bind("mi_longdesc") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="30%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Model">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblmi_model" runat="server" Text='<%# Bind("mi_model") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="20%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitri_itm_stus" runat="server" Text='<%# Bind("itri_itm_stus") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit Cost" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitri_unit_price" runat="server" Text='<%# Bind("itri_unit_price") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="App. Qty" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitri_app_qty" runat="server" Text='<%# Bind("itri_app_qty") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitri_qty" runat="server" Text='<%# Bind("itri_qty") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Request" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitri_note" runat="server" Text='<%# Bind("itri_note") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="supplier" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItri_supplier" runat="server" Text='<%# Bind("Itri_supplier") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="batchno" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Itri_batchno" runat="server" Text='<%# Bind("Itri_batchno") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="grndate" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItri_grndate" runat="server" Text='<%# Bind("Itri_grndate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="expdate" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItri_expdate" runat="server" Text='<%# Bind("Itri_expdate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="tab-pane" id="Serial">
                                            <asp:GridView ID="grdSerial" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:BoundField DataField="tus_itm_cd" HeaderText="Item" ReadOnly="true" />
                                                    <asp:BoundField DataField="tus_itm_model" HeaderText="Model" ReadOnly="true" />
                                                    <asp:BoundField DataField="tus_itm_stus" HeaderText="Status" ReadOnly="true" />
                                                    <asp:BoundField DataField="tus_qty" HeaderText="Qty" ReadOnly="true" />
                                                    <asp:BoundField DataField="tus_ser_1" HeaderText="Serial 1" ReadOnly="true" />
                                                    <asp:BoundField DataField="tus_ser_2" HeaderText="Serial 2" ReadOnly="true" />
                                                    <asp:BoundField DataField="tus_ser_3" HeaderText="Serial 3" ReadOnly="true" Visible="false" />
                                                    <asp:BoundField DataField="tus_bin" HeaderText="Bin" ReadOnly="true" Visible="false" />
                                                    <asp:BoundField DataField="tus_ser_id" HeaderText="Serial ID" ReadOnly="true" Visible="false" />
                                                    <asp:BoundField DataField="tus_base_doc_no" HeaderText="Request" ReadOnly="true" Visible="false" />
                                                    <asp:BoundField DataField="tus_base_itm_line" HeaderText="BaseLineNo" ReadOnly="true" Visible="false" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnhidden" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserPopup" runat="server" Enabled="True" TargetControlID="btnhidden"
                PopupControlID="pnlModalPopup" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
   </ContentTemplate>
    </asp:UpdatePanel>
      <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlModalPopup" DefaultButton="lbtnSearch">
                <div runat="server" id="test" class="panel panel-default height400 width850">
                    <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="btnClose" runat="server">
                                <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-12 height5">
                                </div>
                            </div>
                            <div class="row" id="DateRow" runat="server" visible="false">
                                <div class="col-sm-12">
                                    <div class="col-sm-2 labelText1">
                                        From Date
                                    </div>
                                    <div class="col-sm-3 paddingRight5">
                                        <div class="col-sm-11 paddingRight5 paddingLeft0">
                                            <asp:TextBox ID="txtFromDateSearch" runat="server" CssClass="form-control" onkeypress="return RestrictSpace()"
                                                Format="dd/MMM/yyyy" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                            <asp:LinkButton ID="lbtnFromDateSearch" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDateSearch"
                                                PopupButtonID="lbtnFromDateSearch" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 labelText1">
                                        To Date
                                    </div>
                                    <div class="col-sm-4 paddingRight5">
                                        <div class="col-sm-8 paddingRight5 paddingLeft0">
                                            <asp:TextBox ID="txtToDateSearch" runat="server" CssClass="form-control" onkeypress="return RestrictSpace()"
                                                Format="dd/MMM/yyyy" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                            <asp:LinkButton ID="lbtnToDateSearch" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDateSearch"
                                                PopupButtonID="lbtnToDateSearch" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="col-sm-1 labelText1">
                                    </div>
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
                                        <asp:DropDownList ID="ddlSearchbykey" runat="server" class="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2 labelText1">
                                        Search by word
                                    </div>
                                    <div class="col-sm-4 paddingRight5">
                                        <asp:TextBox ID="txtSearchbyword" CausesValidation="true" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbyword_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-1 paddingLeft0">
                                        <asp:LinkButton ID="lbtnSearch" runat="server" OnClick="lbtnSearch_Click">
                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>
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
                                <div class="col-sm-12 height5">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:GridView ID="grdResult" CausesValidation="false" runat="server" OnSelectedIndexChanged="grdResult_SelectedIndexChanged" AllowPaging="True" OnPageIndexChanging="grdResult_PageIndexChanging" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager">
                                        <Columns>
                                            <asp:ButtonField Text="select" CommandName="Select" ItemStyle-Width="10" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
      </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
