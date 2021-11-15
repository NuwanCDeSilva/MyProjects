<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="InternalAssetItemIssue.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Inventory.InternalAssetItemIssue" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/ucOutScan.ascx" TagPrefix="uc1" TagName="ucOutScan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>

        function SaveConfirm() {
            var selectedvalue = confirm("Do you want to Save data?");
            if (selectedvalue) {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "No";
            }
        };
        function ClearConfirm() {
            var selectedvalue = confirm("Do you want to Clear data?");
            if (selectedvalue) {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "No";
            }
        };
        function DeleteConfirm() {
            var selectedvalue = confirm("Do you want to Delete data?");
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
        };
        function checkDateto(sender, args) {

            if ((sender._selectedDate < new Date())) {
                $().toastmessage('showToast', {
                    text: 'You cannot select a day earlier than today!',
                    sticky: false,
                    position: 'top-center',
                    type: 'warning',
                    closeText: '',
                    close: function () {
                        console.log("toast is closed ...");
                    }

                });
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        };
        function checkDateF(sender, args) {

            if ((sender._selectedDate > new Date())) {
                alert("You cannot select a day future date !");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        };
        function checkDate(sender, args) {

            if ((sender._selectedDate > document.getElementById('<%=txtFDate.ClientID%>').value)) {
                alert("You cannot select a day earlier than today!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }

    </script>
    <script type="text/javascript">
        function closeDialog() {
            $(this).showStickySuccessToast("close");
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
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="txtClearlconformmessageValue" runat="server" />
            <asp:HiddenField ID="txtSavelconformmessageValue" runat="server" />
            <asp:HiddenField ID="txtUpdateconformmessageValue" runat="server" />
            <asp:HiddenField ID="txtApprovalconformmessageValue" runat="server" />
            <asp:HiddenField ID="txtCancelconformmessageValue" runat="server" />
            <asp:HiddenField ID="txtDeleteconformmessageValue" runat="server" />

            <div class="panel panel-default marginLeftRight5 paddingbottom0">
                <div class="panel-body paddingtopbottom0">
                    <div class="row">
                        <div class="col-sm-7  buttonrow">
                        </div>
                        <div class="col-sm-4  buttonRow">
                            <div class="col-sm-6 paddingRight0">
                            </div>
                            <div class="col-sm-2 paddingRight0">
                            </div>
                            <div class="col-sm-4 paddingRight0 paddingLeft0">
                                <asp:LinkButton ID="lbtnSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="SaveConfirm()" OnClick="lbtnSave_Click">
                                    <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                </asp:LinkButton>
                            </div>

                        </div>
                        <div class="col-sm-1  buttonRow paddingLeft0">
                            <div class="col-sm-12 paddingLeft5">
                                <asp:LinkButton ID="lbtnClear" runat="server" CssClass="floatRight" OnClientClick="ClearConfirm()" OnClick="lbtnClear_Click">
                                    <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12 ">
                            <div class="panel panel-default">
                                <div class="panel-heading paddingtopbottom0"><strong>Internal Asset Item Issue</strong> </div>
                                <div class="panel-body">
                                   
                                        <div class="row">
                                            <div class="col-sm-8">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading paddingtopbottom0">Issue Party</div>
                                                    <div class="panel-body">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="row">
                                                                    <div class="col-sm-3">
                                                                        <div class="row">
                                                                            <div class="col-sm-4 labelText1">
                                                                                Date
                                                                            </div>
                                                                            <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" onkeypress="return RestrictSpace()"
                                                                                    Format="dd/MMM/yyyy" Enabled="false">
                                                                                </asp:TextBox>
                                                                            </div>
                                                                            <div class="col-sm-2 paddingLeft0 paddingRight0">
                                                                                <asp:LinkButton ID="lbtnDate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDate"
                                                                                    PopupButtonID="lbtnDate" Format="dd/MMM/yyyy">
                                                                                </asp:CalendarExtender>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-3">
                                                                        <div class="row">
                                                                            <div class="col-sm-4 labelText1 paddingLeft5">
                                                                                Requestor
                                                                            </div>
                                                                            <div class="col-sm-5 paddingRight5 paddingLeft5">
                                                                                <asp:TextBox ID="txtRequestor" CausesValidation="false" runat="server" class="form-control" MaxLength="20" AutoPostBack="true" OnTextChanged="txtRequestor_TextChanged"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-sm-2 paddingLeft0 paddingRight0">
                                                                                <asp:LinkButton ID="lbtnRequestor" runat="server" OnClick="lbtnRequestor_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <div class="row">
                                                                            <div class="col-sm-2 labelText1">
                                                                                Ref #
                                                                            </div>
                                                                            <div class="col-sm-10">
                                                                                <asp:TextBox ID="txtRef" CausesValidation="false" runat="server" MaxLength="50" class="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="row">
                                                                    <div class="col-sm-1 labelText1">
                                                                        Remarks
                                                                    </div>
                                                                    <div class="col-sm-11 paddingLeft0">
                                                                        <asp:TextBox ID="txtRemarks" CausesValidation="false" runat="server" class="form-control" MaxLength="100"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 paddingLeft0">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading paddingtopbottom0">Searching</div>
                                                    <div class="panel-body">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="row">
                                                                    <div class="col-sm-3 labelText1">
                                                                        Sequence #
                                                                    </div>
                                                                    <div class="col-sm-4">
                                                                        <asp:TextBox ID="txtUserSeqNo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-5 paddingLeft0">
                                                                        <asp:DropDownList ID="ddlSeqNo" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSeqNo_SelectedIndexChanged">
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
                                                            <div class="col-sm-12">
                                                                <div class="row">
                                                                    <div class="col-sm-3 labelText1">
                                                                        Issue #
                                                                    </div>
                                                                    <div class="col-sm-8 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txtIssueNo" CausesValidation="false" runat="server" class="form-control" AutoPostBack="true" OnTextChanged="txtIssueNo_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="lbtnIssueNo" runat="server" OnClick="lbtnIssueNo_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
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
                                            <div class="col-sm-8">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading paddingtopbottom0">Pending Orders</div>
                                                    <div class="panel-body">
                                                        <div class="row">
                                                            <div class="col-sm-11 paddingRight0">
                                                                <div class="row">
                                                                    <div class="col-sm-12 height5">
                                                                    </div>
                                                                </div>
                                                                <div class="panel panel-default">
                                                                    <div class="panel-body">
                                                                        <div class="col-sm-4 paddingLeft0">
                                                                            <div class="row">
                                                                                <div class="col-sm-4 labelText1">
                                                                                    From Date
                                                                                </div>
                                                                                <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" onkeypress="return RestrictSpace()"
                                                                                        Format="dd/MMM/yyyy" Enabled="false">
                                                                                    </asp:TextBox>
                                                                                </div>
                                                                                <div class="col-sm-2 paddingLeft0 paddingRight0">
                                                                                    <asp:LinkButton ID="lbtnFromDate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                                    </asp:LinkButton>
                                                                                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtFromDate"
                                                                                        PopupButtonID="lbtnFromDate" Format="dd/MMM/yyyy">
                                                                                    </asp:CalendarExtender>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4 paddingLeft0">
                                                                            <div class="row">
                                                                                <div class="col-sm-4 labelText1">
                                                                                    To Date
                                                                                </div>
                                                                                <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" onkeypress="return RestrictSpace()"
                                                                                        Format="dd/MMM/yyyy" Enabled="false">
                                                                                    </asp:TextBox>
                                                                                </div>
                                                                                <div class="col-sm-2 paddingLeft0 paddingRight0">
                                                                                    <asp:LinkButton ID="lbtnToDate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                                    </asp:LinkButton>
                                                                                    <asp:CalendarExtender ID="CalendarExtender5" runat="server" OnClientDateSelectionChanged="checkDateto" TargetControlID="txtToDate"
                                                                                        PopupButtonID="lbtnToDate" Format="dd/MMM/yyyy">
                                                                                    </asp:CalendarExtender>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4 paddingLeft0">
                                                                            <div class="row">
                                                                                <div class="col-sm-4 labelText1">
                                                                                    Request #
                                                                                </div>
                                                                                <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                                    <asp:TextBox ID="txtRequestNo" CausesValidation="false" runat="server" class="form-control" MaxLength="20" AutoPostBack="true" OnTextChanged="txtRequestNo_TextChanged"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                                    <asp:LinkButton ID="lbtnRequestNo" runat="server" Visible="false" OnClick="lbtnRequestNo_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12 height5">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-1">
                                                                <div class="panel panel-default">
                                                                    <div class="panel-heading height42">
                                                                        <asp:LinkButton ID="lbtnSearchall" runat="server" OnClick="lbtnSearchall_Click">
                                                            <span class="glyphicon glyphicon-search fontsize20 right5" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="panel-body  panelscollbar height80 ">
                                                                    <asp:GridView ID="grdPendingOrders" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                        EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                        <Columns>
                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="selectchk" AutoPostBack="true" runat="server" Width="5px" OnCheckedChanged="selectchk_CheckedChanged"></asp:CheckBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Order #">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_itr_req_no" runat="server" Text='<%# Bind("itr_req_no") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Order Date">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_itr_dt" runat="server" Text='<%# Bind("itr_dt", "{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Ref #">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_itr_ref" runat="server" Text='<%# Bind("itr_ref") %>'></asp:Label>
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
                                            <div class="col-sm-4 paddingLeft0">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">Delivery</div>
                                                    <div class="panel-body">
                                                        <div class="row">
                                                            <div class="col-sm-12">

                                                                <div class="row">
                                                                    <div class="col-sm-4 labelText1  paddingRight0">
                                                                        Transport Method
                                                                    </div>
                                                                    <div class="col-sm-6 paddingLeft5 paddingRight0">
                                                                        <asp:DropDownList ID="ddlDeliver" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDeliver_SelectedIndexChanged">
                                                                            <%-- <asp:ListItem Text=" - - Select - - " Value="0" Selected="True"></asp:ListItem>--%>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-4 abelText1  paddingRight0">
                                                                        <asp:Label ID="lblVehicle" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div class="col-sm-6 paddingLeft5 paddingRight0">
                                                                        <asp:TextBox ID="txtVehicle" runat="server" CssClass="form-control" />
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12 height80">
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <uc1:ucOutScan runat="server" ID="ucOutScan" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="panel panel-default ">
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
                                                            <div class="tab-pane active " id="Item">
                                                                <div class="panel-body  panelscollbar height80 ">
                                                                    <asp:GridView ID="grdItems" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                        EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lbtnGrdSerial" runat="server" CausesValidation="false" OnClick="lbtnGrdSerial_Click">
                                                                <span aria-hidden="true" class="glyphicon glyphicon-arrow-up"></span>
                                                                                    </asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Item">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_itri_itm_cd" runat="server" Text='<%# Bind("itri_itm_cd") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Description">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_mi_longdesc" runat="server" Text='<%# Bind("mi_longdesc") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Model">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_mi_model" runat="server" Text='<%# Bind("mi_model") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%-- <asp:TemplateField HeaderText="Status" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_itri_itm_stus" runat="server" Text='<%# Bind("itri_itm_stus") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                                            <asp:TemplateField HeaderText="Status" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_mis_desc" runat="server" Text='<%# Bind("mis_desc") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Part #">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_mi_part_no" runat="server" Text='<%# Bind("mi_part_no") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Req. Qty">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_itri_qty" runat="server" Text='<%# Bind("itri_qty","{0:n}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Approve. Qty">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_itri_app_qty" runat="server" Text='<%# Bind("itri_app_qty","{0:n}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Po. Qty">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_itri_po_qty" runat="server" Text='<%# Bind("itri_po_qty","{0:n}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Balance Qty">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_itri_bqty" runat="server" Text='<%# Bind("itri_bqty","{0:n}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Iss Qty" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_itri_issue_qty" runat="server" Text='<%# Bind("itri_issue_qty","{0:n}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Pick Qty">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblitri_qty" runat="server" Text='<%# Bind("itri_mqty","{0:n}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Line No" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_itri_line_no" runat="server" Text='<%# Bind("itri_line_no") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Req No" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_ITR_REQ_NO" runat="server" Text='<%# Bind("ITR_REQ_NO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="job line" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_itri_job_line" runat="server" Text='<%# Bind("itri_job_line") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                            <div class="tab-pane" id="Serial">
                                                                <div class="panel-body  panelscollbar height80 ">
                                                                    <asp:GridView ID="grdSerial" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                        EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lbtngrdSerialDalete" runat="server" CausesValidation="false" OnClientClick="DeleteConfirm()" OnClick="lbtnRemove_Click">
                                                                <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                                                                    </asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Item">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="TUS_ITM_CD" runat="server" Text='<%# Bind("TUS_ITM_CD") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Description">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="TUS_ITM_DESC" runat="server" Text='<%# Bind("TUS_ITM_DESC") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Model">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="TUS_ITM_MODEL" runat="server" Text='<%# Bind("TUS_ITM_MODEL") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Brand">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="TUS_ITM_BRAND" runat="server" Text='<%# Bind("TUS_ITM_BRAND") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Qty">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Tus_qty" runat="server" Text='<%# Bind("Tus_qty","{0:n}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Item Status" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="TUS_ITM_STUS" runat="server" Text='<%# Bind("TUS_ITM_STUS") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Item Status">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Mis_desc" runat="server" Text='<%# Bind("Mis_desc") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Serial 1">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="TUS_SER_1" runat="server" Text='<%# Bind("TUS_SER_1") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Serial 2">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="TUS_SER_2" runat="server" Text='<%# Bind("TUS_SER_2") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Warr No">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="TUS_WARR_NO" runat="server" Text='<%# Bind("TUS_WARR_NO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Serial ID">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="TUS_SER_ID" runat="server" Text='<%# Bind("TUS_SER_ID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Bin" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="TUS_BIN" runat="server" Text='<%# Bind("TUS_BIN") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Req Doc" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="tus_base_doc_no" runat="server" Text='<%# Bind("tus_base_doc_no") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Line" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="tus_base_itm_line" runat="server" Text='<%# Bind("tus_base_itm_line") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--</asp:TemplateField>--%>
                                                                            <%--<asp:BoundField DataField="tus_itm_model" HeaderText="Model" ReadOnly="true" />
                                                    <asp:BoundField DataField="tus_itm_stus" HeaderText="Status" ReadOnly="true" />
                                                    <asp:BoundField DataField="tus_qty" HeaderText="Qty" ReadOnly="true" />
                                                    <asp:BoundField DataField="tus_ser_1" HeaderText="Serial 1" ReadOnly="true" />
                                                    <asp:BoundField DataField="tus_ser_2" HeaderText="Serial 2" ReadOnly="true" />
                                                    <asp:BoundField DataField="tus_ser_3" HeaderText="Serial 3" ReadOnly="true" Visible="false" />
                                                    <asp:BoundField DataField="tus_bin" HeaderText="Bin" ReadOnly="true" Visible="false" />
                                                    <asp:BoundField DataField="tus_ser_id" HeaderText="Serial ID" ReadOnly="true" Visible="false" />
                                                    <asp:BoundField DataField="tus_base_doc_no" HeaderText="Request" ReadOnly="true" Visible="false" />
                                                    <asp:BoundField DataField="tus_base_itm_line" HeaderText="BaseLineNo" ReadOnly="true" Visible="false" />--%>
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
        </ContentTemplate>
    </asp:UpdatePanel>

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

    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button5" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserDPopoup" runat="server" Enabled="True" TargetControlID="Button5"
                PopupControlID="pnlDpopup" CancelControlID="btnDClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlDpopup" DefaultButton="lbtnSearch">
                <div runat="server" id="Div5" class="panel panel-default height400 width950">

                    <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="btnDClose" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <%--<span>Commen Search</span>--%>
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12" id="Div6" runat="server">
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
                                            <asp:TextBox runat="server" Enabled="false" ID="txtFDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnFDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txtFDate"
                                                PopupButtonID="lbtnFDate" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-2 labelText1">
                                            To
                                        </div>
                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                            <asp:TextBox runat="server" Enabled="false" ID="txtTDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnTDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="txtTDate"
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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy11" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                            <ContentTemplate>
                                                <div class="col-sm-8 paddingRight5">
                                                    <asp:DropDownList ID="ddlSearchbykeyD" runat="server" class="form-control">
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
                                            <asp:TextBox ID="txtSearchbywordD" CausesValidation="false" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbywordD_TextChanged"></asp:TextBox>
                                        </div>
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy12" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                            <ContentTemplate>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnSearchD" runat="server" OnClick="lbtnSearchD_Click">
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
                                    <div class="col-sm-12 ">
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy13" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel22" runat="server">
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

                                <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
                                    runat="server" AssociatedUpdatePanelID="UpdatePanel7">

                                    <ProgressTemplate>
                                        <div class="divWaiting">
                                            <asp:Label ID="lblWait" runat="server"
                                                Text="Please wait... " />
                                            <asp:Image ID="imgWait" runat="server"
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
</asp:Content>
