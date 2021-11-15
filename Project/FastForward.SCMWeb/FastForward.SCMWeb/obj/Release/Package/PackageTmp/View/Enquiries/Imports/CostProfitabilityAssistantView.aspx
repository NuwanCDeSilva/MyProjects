<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="CostProfitabilityAssistantView.aspx.cs" Inherits="FastForward.SCMWeb.View.Enquiries.Imports.Cost_and_Profitability_Assistant_View" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>

        function SaveConfirm() {
            var selectedvalue = confirm("Do you want to save data?");
            if (selectedvalue) {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "No";
            }
        };
        function ClearConfirm() {
            var selectedvalue = confirm("Do you want to clear data?");
            if (selectedvalue) {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "No";
            }
        };
        function DeleteConfirm() {
            var selectedvalue = confirm("Do you want to delete data?");
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
        function CheckAllgrdReq(oCheckbox) {
            var GridView2 = document.getElementById("<%=grditemCost.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }
        function CheckBoxCheck(rb) {
            debugger;
            var gv = document.getElementById("<%=grditemCost.ClientID%>");
            var chk = gv.getElementsByTagName("input");
            var row = rb.parentNode.parentNode;
            for (var i = 0; i < chk.length; i++) {
                if (chk[i].type == "checkbox") {
                    if (chk[i].checked && chk[i] != rb) {
                        chk[i].checked = false;
                        break;
                    }
                }
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
    <asp:HiddenField ID="txtClearlconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtSavelconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtUpdateconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtApprovalconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtCancelconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtDeleteconformmessageValue" runat="server" />
    <div class="panel panel-default marginLeftRight5">
        <div class="panel-body">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-sm-8  buttonrow">
                        </div>
                        <div class="col-sm-4  buttonRow">

                            <div class="col-sm-9">
                            </div>

                            <div class="col-sm-3">
                                <asp:LinkButton ID="lblUClear" runat="server" CssClass="floatRight" OnClientClick="ClearConfirm()" OnClick="lbtnClear_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-2 paddingRight0">
                            <div class="row">
                                <div class="col-sm-4 labelText1">
                                    Company
                                </div>
                                <div class="col-sm-6 paddingLeft0 paddingRight0">
                                    <asp:TextBox runat="server" ID="txtcompany" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtcompany_TextChanged"></asp:TextBox>
                                </div>
                                <div class="col-sm-1 ">
                                    <asp:LinkButton ID="lbtncompany" runat="server" CausesValidation="false" OnClick="lbtncompany_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4 labelText1">
                                    Order from
                                </div>
                                <div class="col-sm-6 paddingLeft0 paddingRight0">
                                    <asp:DropDownList ID="ddlorderfrom" CausesValidation="false" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="All" Value="A" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Local" Value="L" />
                                        <asp:ListItem Text="Imports" Value="I" />
                                    </asp:DropDownList>
                                </div>

                            </div>
                        </div>
                        <div class="col-sm-3 ">
                            <div class="row">
                                <div class="col-sm-3 labelText1 ">
                                    Item
                                </div>
                                <div class="col-sm-8 paddingLeft0 paddingRight0">
                                    <asp:TextBox runat="server" ID="txtItem" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtItem_TextChanged"></asp:TextBox>
                                </div>
                                <div class="col-sm-1  paddingRight0">
                                    <asp:LinkButton ID="lbtnItem" runat="server" CausesValidation="false" OnClick="lbtnItem_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3 labelText1">
                                    Model
                                </div>
                                <div class="col-sm-8 paddingLeft0 paddingRight0">
                                    <asp:TextBox runat="server" ID="txtModel" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtModel_TextChanged"></asp:TextBox>
                                </div>
                                <div class="col-sm-1  paddingRight0">
                                    <asp:LinkButton ID="lbtnmodel" runat="server" CausesValidation="false" OnClick="lbtnmodel_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                </div>

                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="row">
                                <div class="col-sm-3 labelText1 ">
                                    Status
                                </div>
                                <div class="col-sm-8 paddingLeft0 paddingRight0">
                                    <asp:DropDownList ID="ddlStatus" CausesValidation="false" runat="server" CssClass="form-control">
                                        <asp:ListItem Text=" - - Select - - " Value="0" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3 labelText1">
                                    Bl #
                                </div>
                                <div class="col-sm-8 paddingLeft0 paddingRight0">
                                    <asp:TextBox runat="server" ID="txtbl" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtModel_TextChanged"></asp:TextBox>
                                </div>
                                <div class="col-sm-1  paddingRight0">
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" OnClick="lbtnDocNumber_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                </div>

                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="row">
                                <div class="col-sm-5 labelText1">
                                    Filter row
                                </div>
                                <div class="col-sm-6 paddingLeft0 paddingRight0">
                                    <asp:TextBox runat="server" ID="txtLast" onkeypress="return isNumberKey(event)" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>
                            <div class="row">
                                <%--  Order Request--%>
                                <div class="col-sm-5 labelText1">
                                </div>
                                <div class="col-sm-6 paddingLeft0 paddingRight0">
                                    <asp:DropDownList ID="ddlOrderRequest" Visible="false" CausesValidation="false" runat="server" CssClass="form-control">
                                        <asp:ListItem Text=" - - Select - - " Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Sales" />
                                        <asp:ListItem Text="Promotion" />
                                    </asp:DropDownList>
                                </div>

                            </div>
                        </div>
                        <div class="col-sm-1 paddingLeft0">

                            <asp:LinkButton ID="lbtnSearchall" runat="server" OnClick="lbtnSearchall_Click">
                                                            <span class="glyphicon glyphicon-search fontsize20 right5" aria-hidden="true"></span>
                            </asp:LinkButton>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel-body panelscollbar height120">
                                <asp:GridView ID="grditemCost" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                    EmptyDataText="No data found..." AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="" Visible="true">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="allchk" runat="server" Width="5px" onclick="CheckAllgrdReq(this)"></asp:CheckBox>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_Req" AutoPostBack="true" runat="server" onclick="CheckBoxCheck(this);" Checked="false" Width="5px" OnCheckedChanged="chk_Req_CheckedChanged_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Ref.">
                                            <ItemTemplate>
                                                <asp:Label ID="col_POH_DOC_NO" runat="server" Text='<%# Bind("POH_DOC_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GRN #" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="col_ITH_DOC_NO" runat="server" Text='<%# Bind("ITH_DOC_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Location" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="col_ith_loc" runat="server" Text='<%# Bind("ith_loc") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item">
                                            <ItemTemplate>
                                                <asp:Label ID="col_ITB_ITM_CD" runat="server" Text='<%# Bind("ITB_ITM_CD") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Model">
                                            <ItemTemplate>
                                                <asp:Label ID="col_mi_model" runat="server" Text='<%# Bind("mi_model") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Payment Mode">
                                            <ItemTemplate>
                                                <asp:Label ID="col_charge" runat="server" Text='<%# Bind("charge") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Duty free cost without pal">
                                            <ItemTemplate>
                                                <asp:Label ID="col_Df" runat="server" Text='<%# Bind("Df") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Duty free cost with Pal">
                                            <ItemTemplate>
                                                <asp:Label ID="col_DF_Pal" runat="server" Text='<%# Bind("DF_Pal") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Duty Paid">
                                            <ItemTemplate>
                                                <asp:Label ID="col_DutyPaid" runat="server" Text='<%# Bind("DutyPaid") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="suplier" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="col_mbe_name" runat="server" Text='<%# Bind("mbe_name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Des" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="col_mi_longdesc" runat="server" Text='<%# Bind("mi_longdesc") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cost" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="col_ITB_UNIT_COST" runat="server" Text='<%# Bind("ITB_UNIT_COST") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12 height20">
                        </div>
                    </div>
                    <div class="panel panel-default marginLeftRight5">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="row">
                                        <div class="col-sm-2 fontWeight900">
                                            Supplier:
                                        </div>
                                        <div class="col-sm-8">
                                            <asp:Label ID="lbsupplier" runat="server" CssClass="Color1 fontWeight900"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4 fontWeight900">
                                    <div class="row">
                                        <div class="col-sm-4">
                                            Item Description:
                                        </div>
                                        <div class="col-sm-8">
                                            <asp:Label ID="lbDes" runat="server" CssClass="Color1 fontWeight900"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">


                        <div class="col-sm-8">
                            <div class="panel panel-default marginLeftRight5">

                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-3">
                                            Price Managing
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:LinkButton ID="lbtnView" runat="server" OnClick="lbtnView_Click">
                                          <span class="glyphicon glyphicon-search " aria-hidden="true"></span>View
                                            </asp:LinkButton>

                                        </div>
                                        <div class="col-sm-2">
                                            Apply Percentage
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:TextBox runat="server" ID="txtApplyPer" onkeypress="return isNumberKey(event)" CausesValidation="false" CssClass="form-control"></asp:TextBox>

                                        </div>
                                        <div class="col-sm-1">
                                            <asp:Button ID="btnApply" class="btn btn-success btn-xs" runat="server" Text="Process" OnClick="btnApply_Click" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 height10">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-sm-12 ">
                                                <div class="bs-example">
                                                    <ul class="nav nav-tabs" id="myTab">
                                                        <li class="active"><a href="#DF" data-toggle="tab">Duty Free</a></li>
                                                        <li><a href="#DP" data-toggle="tab">Duty Paid</a></li>
                                                    </ul>

                                                </div>
                                                <div class="tab-content">
                                                    <div class="tab-pane active" id="DF">
                                                        <div class="panel-body panelscollbar height120">
                                                            <asp:GridView ID="grdDFPriceBook" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                <Columns>

                                                                    <asp:TemplateField HeaderText="Price Book">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_sapd_pb_tp_cd" runat="server" Text='<%# Bind("sapd_pb_tp_cd") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Price Level">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_sapd_pbk_lvl_cd" runat="server" Text='<%# Bind("sapd_pbk_lvl_cd") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Valid">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_sapd_to_date" runat="server" Text='<%# Bind("sapd_to_date", "{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Price">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_sapd_itm_price" runat="server" Text='<%# Bind("sapd_itm_price") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Margin">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_Margin1" runat="server" Text='<%# Bind("Margin1") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_Prece1" runat="server" Text='<%# Bind("Prece1") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Price">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_sapd_itm_price2" runat="server" Text='<%# Bind("sapd_itm_price") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Margin">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_Margin1" runat="server" Text='<%# Bind("Margin2") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_Prece1" runat="server" Text='<%# Bind("Prece2") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Logistic Cost">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_LgCost" runat="server" Text='<%# Bind("LgCost") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Logistic Cost Margin">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_LgMargin" runat="server" Text='<%# Bind("LgMargin") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                    <div class="tab-pane " id="DP">
                                                        <div class="panel-body panelscollbar height120">
                                                            <asp:GridView ID="grdDpPriceBook" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Price Book">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_sapd_pb_tp_cd" runat="server" Text='<%# Bind("sapd_pb_tp_cd") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Price Level">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_sapd_pbk_lvl_cd" runat="server" Text='<%# Bind("sapd_pbk_lvl_cd") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Valid">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_sapd_to_date" runat="server" Text='<%# Bind("sapd_to_date", "{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Price">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_sapd_itm_price" runat="server" Text='<%# Bind("sapd_itm_price") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Margin ">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_Margin1" runat="server" Text='<%# Bind("Margin1") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_Prece1" runat="server" Text='<%# Bind("Prece1") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Price">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_sapd_itm_price2" runat="server" Text='<%# Bind("sapd_itm_price") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Margin">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_Margin1" runat="server" Text='<%# Bind("Margin2") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_Prece1" runat="server" Text='<%# Bind("Prece2") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Logistic Cost">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_LgCost" runat="server" Text='<%# Bind("LgCost") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Logistic Cost Margin">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_LgMargin" runat="server" Text='<%# Bind("LgMargin") %>'></asp:Label>
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

                        <div class="col-sm-4">
                            <div class="panel panel-default marginLeftRight5">
                                <div class="panel-heading">
                                    Inventory Balance
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-1">
                                            DF
                                            
                                        </div>
                                        <div class="col-sm-1 ">
                                            <asp:RadioButton ID="rdioDF" Checked="true" GroupName="DFDP" runat="server" />
                                        </div>
                                        <div class="col-sm-1">
                                            DP
                                            
                                        </div>
                                        <div class="col-sm-1 ">
                                            <asp:RadioButton ID="rdioDP" GroupName="DFDP" runat="server" />
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:Button ID="btnInventory" class="btn btn-default btn-xs" runat="server" Text="View" OnClick="btnInventory_Click" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 height10">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">

                                            <div class="panel-body panelscollbar height120">
                                                <asp:GridView ID="grdinventory" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="Channel">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_ml_sev_chnl" runat="server" Text='<%# Bind("ml_cate_2") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Stock in hand">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_INL_FREE_QTY" runat="server" Text='<%# Bind("INL_FREE_QTY") %>'></asp:Label>
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
    </div>

    <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserPopoup" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="pnlpopup" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpopup" DefaultButton="lbtnSearch">
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
                    <div class="col-sm-12" id="Div4" runat="server">
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
</asp:Content>
