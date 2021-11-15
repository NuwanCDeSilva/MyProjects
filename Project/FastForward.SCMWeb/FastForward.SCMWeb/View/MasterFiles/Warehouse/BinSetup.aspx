<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="BinSetup.aspx.cs" Inherits="FastForward.SCMWeb.View.MasterFiles.Warehouse.BinSetup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function CheckBoxCheck(rb) {
            debugger;
            var gv = document.getElementById("<%=grdZone.ClientID%>");
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
        function CheckAll(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdZone.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }
        function CheckAllAisle(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdAisle.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }
        function CheckAllRow(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdRow.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }
        function CheckAlllevel(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdlevel.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }
        function CheckAllBinItem(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdBinItem.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }
        function CheckAllstoreFacility(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdstoreFacility.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
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

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
              && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
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
    <div class="panel panel-default marginLeftRight5">
        <div class="panel-body">
            <div class="bs-example">
                <ul class="nav nav-tabs" id="myTab">
                    <li class="active"><a href="#LayoutPlan" data-toggle="tab">Layout Plan</a></li>
                    <li><a href="#BinLoaction" data-toggle="tab">Bin Location</a></li>
                    <li><a href="#LoadingBay" data-toggle="tab">Loading Bay</a></li>
                    <li><a href="#DocLoadingBay" data-toggle="tab">Doc. Loading Bay</a></li>
                </ul>
            </div>
            <div class="tab-content">
                <asp:HiddenField ID="txtClearlconformmessageValue" runat="server" />
                <asp:HiddenField ID="txtSavelconformmessageValue" runat="server" />
                <asp:HiddenField ID="txtUpdateconformmessageValue" runat="server" />
                <asp:HiddenField ID="txtApprovalconformmessageValue" runat="server" />
                <asp:HiddenField ID="txtCancelconformmessageValue" runat="server" />
                <asp:HiddenField ID="txtDeleteconformmessageValue" runat="server" />
                <div class="tab-pane active" id="LayoutPlan">
                    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-sm-6  buttonrow">
                                    <div id="Warnninglayoutplan" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">

                                        <div class="col-sm-11">
                                            <strong>Alert!</strong>
                                            <asp:Label ID="lblWarnninglayoutplan" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:LinkButton ID="lbtnlayplaneclose" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnlayplaneclose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div id="Successlayoutplan" runat="server" visible="false" class="alert alert-success alert-dismissible" role="alert">
                                        <div class="col-sm-11">
                                            <strong>Success!</strong>
                                            <asp:Label ID="lblSuccesslayoutplan" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnlayplaneclose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>

                                    </div>
                                    <div id="Div6" runat="server" visible="false" class="alert alert-info alert-dismissible" role="alert">
                                        <strong>Alert!</strong>
                                        <asp:Label ID="Label6" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-6  buttonRow">
                                    <div class="col-sm-4">
                                    </div>
                                    <div class="col-sm-3 paddingRight0">
                                        <asp:LinkButton ID="lbtnlayoutplanSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="SaveConfirm()" OnClick="lbtnlayoutplanSave_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Add New/Update
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:LinkButton ID="lbtnlayoutplanClear" runat="server" CssClass="floatRight" OnClientClick="ClearConfirm()" OnClick="lbtnlayoutplanClear_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:LinkButton ID="lbtnExcelUpload" runat="server" CssClass="floatRight" OnClick="lbtnExcelUpload_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Excel Upload
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6 paddingRight5">
                                    <div class="row">
                                        <div class="col-sm-12 ">
                                            <div class="panel panel-default">
                                                <div class="panel-body">
                                                    <div class="col-sm-2 labelText1 paddingLeft5">
                                                        Store Code
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                        <asp:TextBox runat="server" ID="txtStorecode" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtStorecode_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0 Lwidth">
                                                        <asp:LinkButton ID="lbtnDelLoca" runat="server" CausesValidation="false" OnClick="lbtnDelLoca_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div class="col-sm-1 labelText1 paddingLeft5">
                                                        Warehouse
                                                    </div>
                                                    <div class="col-sm-3 paddingRight0">
                                                        <asp:TextBox runat="server" ID="txtWarehouse" ReadOnly="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-2 labelText1 paddingLeft5">
                                                        Prefix for Bin
                                                    </div>
                                                    <div class="col-sm-2 paddingLeft0 paddingRight0">
                                                        <asp:TextBox runat="server" ID="txtPrefixBin" ReadOnly="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 height10">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="panel panel-default height550">
                                                <div class="panel-heading">
                                                </div>
                                                <div class="panel-body panelscoll3 ">
                                                    <asp:TreeView ID="treeViewLayout" CssClass="nav nav-list tree" runat="server" ShowCheckBoxes="All" ShowLines="true" Height="100px" Width="100px" ExpandDepth="0" EnableClientScript="false">
                                                    </asp:TreeView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-sm-6 paddingLeft5">
                                    <div class="row">
                                        <div class="col-sm-12 ">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    <div class="row">
                                                        <div class="col-sm-7">
                                                            Zone Definition
                                                        </div>
                                                        <div class="col-sm-2">
                                                        </div>
                                                        <div class="col-sm-3">
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-sm-2 labelText1">
                                                            Description
                                                        </div>
                                                        <div class="col-sm-3 paddingLeft0 paddingRight0">
                                                            <asp:TextBox runat="server" ID="txtZDes" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 labelText1">
                                                            Zone ID
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                            <asp:TextBox runat="server" Style='text-transform: uppercase' MaxLength="2" ID="txtZID" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 labelText1 paddingLeft5 paddingRight0">
                                                            Is Default
                                                        </div>
                                                        <div class="col-sm-1 Lwidth">
                                                            <asp:CheckBox ID="chk_zoneIsDef" runat="server" Checked="false" Width="5px" />
                                                        </div>
                                                        <div class="col-sm-1 labelText1">
                                                            Active
                                                        </div>
                                                        <div class="col-sm-1 Lwidth">
                                                            <asp:CheckBox ID="chk_zoneActive" runat="server" Checked="false" Width="5px" />
                                                        </div>
                                                        <div class="col-sm-1 Lwidth ">
                                                            <asp:LinkButton ID="lbtnAddZone" runat="server" CausesValidation="false" OnClick="lbtnAddZone_Click">
                                                        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <div class="panel panel-default">
                                                                <div class=" panel-body height80 panelscollbar">
                                                                    <asp:GridView ID="grdZone" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="" Visible="false">
                                                                                <HeaderTemplate>
                                                                                    <asp:CheckBox ID="allchk" runat="server" Width="5px" onclick="CheckAll(this)"></asp:CheckBox>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chk_zone" AutoPostBack="true" runat="server" Checked="false" Width="5px" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Select">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lbtnsZone" runat="server" CausesValidation="false" OnClick="lbtnsZone_Click">
                                                        <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"></span>
                                                                                    </asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Description">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_ZnDes" runat="server" Text='<%# Bind("IZ_ZN_DESC") %>' Width="110px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Zone ID">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_znId" runat="server" Text='<%# Bind("IZ_ZN_ID") %>' Width="20px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="IS Default" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chk_zoneIsDgrd" runat="server" Checked='<%# Bind("IZ_IS_DEF") %>' AutoPostBack="true" Width="5px" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Active" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chk_zoneactivegrd" runat="server" Checked='<%# Bind("IZ_ACT") %>' Width="5px" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="seq" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="chk_zseq" runat="server" Text='<%# Bind("IZ_ZN_SEQ") %>' Width="20px"></asp:Label>

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
                                    <div class="row">
                                        <div class="col-sm-12 ">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    <div class="row">
                                                        <div class="col-sm-7">
                                                            Aisle Definition
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <asp:Label ID="lblAisleValue" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-3">
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-sm-2 labelText1">
                                                            Description
                                                        </div>
                                                        <div class="col-sm-3 paddingLeft0 paddingRight0">
                                                            <asp:TextBox runat="server" ID="txtADes" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 labelText1">
                                                            Aisle ID
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                            <asp:TextBox runat="server" Style='text-transform: uppercase' MaxLength="2" ID="txtAId" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-2 labelText1">
                                                            Is Default
                                                        </div>
                                                        <div class="col-sm-1 Lwidth">
                                                            <asp:CheckBox ID="chk_AisleIsDef" runat="server" Checked="false" Width="5px" />
                                                        </div>
                                                        <div class="col-sm-1 labelText1">
                                                            Active
                                                        </div>
                                                        <div class="col-sm-1 Lwidth">
                                                            <asp:CheckBox ID="chk_AisleActive" runat="server" Checked="false" Width="5px" />
                                                        </div>
                                                        <div class="col-sm-1 Lwidth ">
                                                            <asp:LinkButton ID="lbtnAddAisle" runat="server" CausesValidation="false" OnClick="lbtnAddAisle_Click">
                                                        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <div class="panel panel-default">
                                                                <div class=" panel-body height80 panelscollbar">
                                                                    <asp:GridView ID="grdAisle" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="" Visible="false">
                                                                                <HeaderTemplate>
                                                                                    <asp:CheckBox ID="allchk" Visible="false" runat="server" Width="5px" onclick="CheckAllAisle(this)"></asp:CheckBox>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chk_Arisle" AutoPostBack="true" runat="server" Checked="false" Width="5px" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Select">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lbtnsArisle" runat="server" CausesValidation="false" OnClick="lbtnsArisle_Click">
                                                        <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"></span>
                                                                                    </asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Description">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_AisleDes" runat="server" Text='<%# Bind("IA_ASL_DESC") %>' Width="110px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Aisle ID">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_AisleId" runat="server" Text='<%# Bind("IA_ASL_ID") %>' Width="20px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="IS Default" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chk_AisleIsDgrd" runat="server" Checked='<%# Bind("IA_IS_DEF") %>' Width="5px" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Active" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chk_Aisleactivegrd" runat="server" Checked='<%# Bind("IA_ACT") %>' Width="5px" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="seq" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_AZseq" runat="server" Text='<%# Bind("IA_ZN_SEQ") %>' Width="20px"></asp:Label>

                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="seq" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_Aseq" runat="server" Text='<%# Bind("ia_asl_seq") %>' Width="20px"></asp:Label>

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
                                    <div class="row">
                                        <div class="col-sm-12 ">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    <div class="row">
                                                        <div class="col-sm-7">
                                                            Row Definition
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <asp:Label ID="lblRowvalue" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-3">
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-sm-2 labelText1">
                                                            Description
                                                        </div>
                                                        <div class="col-sm-3 paddingLeft0 paddingRight0">
                                                            <asp:TextBox runat="server" ID="txtRDes" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 labelText1">
                                                            Row ID
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                            <asp:TextBox runat="server" Style='text-transform: uppercase' MaxLength="2" ID="txtRId" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 labelText1 paddingLeft5 paddingRight0 ">
                                                            Is Default
                                                        </div>
                                                        <div class="col-sm-1 Lwidth">
                                                            <asp:CheckBox ID="chk_RowIsDef" runat="server" Checked="false" Width="5px" />
                                                        </div>
                                                        <div class="col-sm-1 labelText1">
                                                            Active
                                                        </div>
                                                        <div class="col-sm-1 Lwidth">
                                                            <asp:CheckBox ID="chk_RowActive" runat="server" Checked="false" Width="5px" />
                                                        </div>
                                                        <div class="col-sm-1 Lwidth ">
                                                            <asp:LinkButton ID="lbtnAddRow" runat="server" CausesValidation="false" OnClick="lbtnAddRow_Click">
                                                        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <div class="panel panel-default">
                                                                <div class=" panel-body height80 panelscollbar">
                                                                    <asp:GridView ID="grdRow" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="" Visible="false">
                                                                                <HeaderTemplate>
                                                                                    <asp:CheckBox ID="allchk" runat="server" Visible="false" Width="5px" onclick="CheckAllRow(this)"></asp:CheckBox>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chk_Row" AutoPostBack="true" runat="server" Checked="false" Width="5px" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Select">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lbtnsRow" runat="server" CausesValidation="false" OnClick="lbtnsRow_Click">
                                                        <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"></span>
                                                                                    </asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Description">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_RowDes" runat="server" Text='<%# Bind("IR_ROW_DESC") %>' Width="110px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Row ID">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_RowId" runat="server" Text='<%# Bind("IR_ROW_ID") %>' Width="20px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="IS Default" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chk_RowIsDgrd" runat="server" Checked='<%# Bind("IR_IS_DEF") %>' Width="5px" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Active" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chk_Rowactivegrd" runat="server" Checked='<%# Bind("IR_ACT") %>' Width="5px" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="seq" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_RZseq" runat="server" Text='<%# Bind("IR_ZN_SEQ") %>' Width="20px"></asp:Label>

                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="seq" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_Aseq" runat="server" Text='<%# Bind("ir_asl_seq") %>' Width="20px"></asp:Label>

                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="seq" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_Rseq" runat="server" Text='<%# Bind("ir_row_seq") %>' Width="20px"></asp:Label>

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
                                    <div class="row">
                                        <div class="col-sm-12 ">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    <div class="row">
                                                        <div class="col-sm-7">
                                                            Level Definition
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <asp:Label ID="lblLevelvalue" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-3">
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-sm-2 labelText1">
                                                            Description
                                                        </div>
                                                        <div class="col-sm-3 paddingLeft0 paddingRight0">
                                                            <asp:TextBox runat="server" ID="txtlDes" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 labelText1">
                                                            Level ID
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                            <asp:TextBox runat="server" Style='text-transform: uppercase'
                                                                MaxLength="2" ID="txtlId" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-2 labelText1">
                                                            Is Default
                                                        </div>
                                                        <div class="col-sm-1 Lwidth">
                                                            <asp:CheckBox ID="chk_levelIsDef" runat="server" Checked="false" Width="5px" />
                                                        </div>
                                                        <div class="col-sm-1 labelText1">
                                                            Active
                                                        </div>
                                                        <div class="col-sm-1 Lwidth">
                                                            <asp:CheckBox ID="chk_levelActive" runat="server" Checked="false" Width="5px" />
                                                        </div>
                                                        <div class="col-sm-1 Lwidth ">
                                                            <asp:LinkButton ID="lbtnAddlevel" runat="server" CausesValidation="false" OnClick="lbtnAddlevel_Click">
                                                        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <div class="panel panel-default">
                                                                <div class=" panel-body height80 panelscollbar">
                                                                    <asp:GridView ID="grdlevel" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="" Visible="false">
                                                                                <HeaderTemplate>
                                                                                    <asp:CheckBox ID="allchk" runat="server" Visible="false" Width="5px" onclick="CheckAlllevel(this)"></asp:CheckBox>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chk_level" AutoPostBack="true" runat="server" Checked="false" Width="5px" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Select">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lbtnslevel" runat="server" CausesValidation="false" OnClick="lbtnslevel_Click">
                                                        <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"></span>
                                                                                    </asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Description">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_LevelDes" runat="server" Text='<%# Bind("IL_LVL_DESC") %>' Width="110px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Level ID">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_LevelId" runat="server" Text='<%# Bind("IL_LVL_ID") %>' Width="20px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="IS Default" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chk_LevelIsDgrd" runat="server" Checked='<%# Bind("IL_IS_DEF") %>' Width="5px" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Active" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chk_Levelactivegrd" runat="server" Checked='<%# Bind("IL_ACT") %>' Width="5px" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="seq" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_lseq" runat="server" Text='<%# Bind("il_zn_seq") %>' Width="20px"></asp:Label>

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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="tab-pane" id="BinLoaction">
                    <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-sm-6  buttonrow">
                                    <div id="WarnningBin" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">

                                        <div class="col-sm-11">
                                            <strong>Alert!</strong>
                                            <asp:Label ID="lblWarnninglBin" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:LinkButton ID="LinkButton8" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnlayplaneclose_Click">
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
                                            <asp:LinkButton ID="LinkButton9" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnlayplaneclose_Click">
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
                                        <asp:LinkButton ID="lbtnBinSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="SaveConfirm()" OnClick="lbtnBinSave_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Add New/Update
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:LinkButton ID="lbtnBinClear" runat="server" CssClass="floatRight" OnClientClick="ClearConfirm()" OnClick="lbtnlayoutplanClear_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-1 paddingRight0">
                                        <asp:LinkButton ID="lbtManualybin" runat="server" CssClass="floatRight" OnClick="lbtManualybin_Click">
                                        <span class="glyphicon glyphicon-cog" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 ">
                                    <div class="panel panel-default">
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <div class="col-sm-2 labelText1 paddingLeft0 paddingRight0">
                                                        Store Code
                                                    </div>
                                                    <div class="col-sm-3 paddingLeft0 paddingRight0">
                                                        <asp:TextBox runat="server" ID="txtBinStore" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtBinStore_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0 Lwidth">
                                                        <asp:LinkButton ID="lbtnBinStore" runat="server" CausesValidation="false" OnClick="lbtnDelLoca_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div class="col-sm-2 labelText1 paddingLeft0 paddingRight0">
                                                        Warehouse
                                                    </div>
                                                    <div class="col-sm-4 paddingLeft0 paddingRight0">
                                                        <asp:TextBox runat="server" ID="txtBinwarehouse" ReadOnly="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-8">
                                                    <div class="col-sm-1 labelText1 ">
                                                        Zone ID
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                        <asp:TextBox runat="server" ID="txtBinzoneS" ReadOnly="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0 Lwidth">
                                                        <asp:LinkButton ID="LinkButton13" runat="server" CausesValidation="false" OnClick="lbtnBinzone_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div class="col-sm-1 labelText1 ">
                                                        Aisle ID
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                        <asp:TextBox runat="server" ID="txtBinAisleS" ReadOnly="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0 Lwidth">
                                                        <asp:LinkButton ID="lbtnBinAisleS" runat="server" CausesValidation="false" OnClick="lbtnBinAisle_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div class="col-sm-1 labelText1 ">
                                                        Row ID
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                        <asp:TextBox runat="server" ID="txtBinRowS" ReadOnly="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0 Lwidth">
                                                        <asp:LinkButton ID="LinkButton18" runat="server" CausesValidation="false" OnClick="lbtnBinRow_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div class="col-sm-1 labelText1">
                                                        Level
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                        <asp:TextBox runat="server" ID="txtBinLevelS" ReadOnly="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-2 paddingLeft0 Lwidth">
                                                        <asp:LinkButton ID="LinkButton19" runat="server" CausesValidation="false" OnClick="lbtnBinLevel_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div class="col-sm-1 Lwidth">
                                                    </div>
                                                    <div class="col-sm-2 paddingLeft0 Lwidth">
                                                        <asp:LinkButton ID="lbtnFilterBin" runat="server" CausesValidation="false" OnClick="lbtnFilterBin_Click">
                                                        <span class="glyphicon glyphicon-filter" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>

                                                    <%--<div class="col-sm-2 paddingLeft0 paddingRight0">
                                                        <asp:LinkButton ID="LinkButton21" runat="server" CausesValidation="false">
                                                        <span class="glyphicon glyphicon-list-alt" aria-hidden="true"></span>Manual Bin Create
                                                        </asp:LinkButton>
                                                    </div>--%>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="panel panel-default">
                                                        <div class=" panel-body height200 panelscollbar">
                                                            <asp:GridView ID="grdBinDeatils" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtnselectBin" runat="server" CausesValidation="false" OnClick="lbtnselectBin_Click">
                                                        <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Bin code" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_Bincode" runat="server" Text='<%# Bind("IBN_BIN_CD") %>' Width="50px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Description" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_BinDescription" runat="server" Text='<%# Bind("IBN_BIN_DESC") %>' Width="300px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="seq" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_BinSeq" runat="server" Text='<%# Bind("IBN_BIN_SEQ") %>' Width="20px"></asp:Label>
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
                            <div class="row">
                                <div class="col-sm-12 paddingLeft0">
                                    <div class="col-sm-7 paddingRight0">
                                        <div class="col-sm-4 paddingLeft0  paddingRight5">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    Bin Location Definition
                                                </div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-sm-12 paddingRight0">
                                                            <div class="col-sm-5 labelText1 paddingLeft0">
                                                                Zone ID
                                                            </div>
                                                            <div class="col-sm-5 paddingLeft0 paddingRight0">
                                                                <asp:TextBox runat="server" ReadOnly="true" ID="txtBinZone" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft0 Lwidth">
                                                                <asp:LinkButton ID="lbtnBinzone" runat="server" CausesValidation="false" OnClick="lbtnBinzone_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 height5">
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-5 labelText1 paddingLeft0">
                                                                Aisle ID
                                                            </div>
                                                            <div class="col-sm-5 paddingLeft0 paddingRight0">
                                                                <asp:TextBox runat="server" ReadOnly="true" ID="txtBinAisle" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft0 Lwidth">
                                                                <asp:LinkButton ID="lbtnBinAisle" runat="server" CausesValidation="false" OnClick="lbtnBinAisle_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 height5">
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-5 labelText1 paddingLeft0">
                                                                Row ID
                                                            </div>
                                                            <div class="col-sm-5 paddingLeft0 paddingRight0">
                                                                <asp:TextBox runat="server" ReadOnly="true" ID="txtBinRow" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft0 Lwidth">
                                                                <asp:LinkButton ID="lbtnBinRow" runat="server" CausesValidation="false" OnClick="lbtnBinRow_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 height5">
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-5 labelText1 paddingLeft0">
                                                                Level ID
                                                            </div>
                                                            <div class="col-sm-5 paddingLeft0 paddingRight0">
                                                                <asp:TextBox runat="server" ReadOnly="true" ID="txtBinLevel" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft0 Lwidth">
                                                                <asp:LinkButton ID="lbtnBinLevel" runat="server" CausesValidation="false" OnClick="lbtnBinLevel_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 height5">
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-5 labelText1 paddingLeft0">
                                                                Bin Location
                                                            </div>
                                                            <div class="col-sm-5 paddingLeft0 paddingRight0">
                                                                <asp:TextBox runat="server" MaxLength="20" ID="txtBinLocation" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 height5">
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-5 labelText1 paddingLeft0">
                                                                Description
                                                            </div>
                                                            <div class="col-sm-5 paddingLeft0 paddingRight0">
                                                                <asp:TextBox runat="server" MaxLength="30" ID="txtBinDes" TextMode="MultiLine" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 height5">
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-3 labelText1 paddingLeft0">
                                                                Is Default
                                                            </div>
                                                            <div class="col-sm-3 Lwidth">
                                                                <asp:CheckBox ID="chk_BinIsDef" runat="server" Checked="false" Width="5px" />
                                                            </div>
                                                            <div class="col-sm-2 labelText1 paddingLeft10">
                                                                Active
                                                            </div>
                                                            <div class="col-sm-1 Lwidth">
                                                                <asp:CheckBox ID="chk_BinActive" runat="server" Checked="false" Width="5px" />
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
                                        <div class="col-sm-8 paddingLeft0 paddingRight5">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    Bin physical Information
                                                </div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-sm-6 paddingRight0">
                                                            <div class="col-sm-4 labelText1 paddingLeft0">
                                                                Width
                                                            </div>
                                                            <div class="col-sm-4 paddingLeft0 paddingRight0">
                                                                <asp:TextBox runat="server" ID="txtwidth" onkeypress="return isNumberKey(event)" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtwidth_TextChanged"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-4 paddingLeft0 paddingRight0">
                                                                <asp:DropDownList ID="ddluomwidth" AutoPostBack="True" CausesValidation="false" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddluomwidth_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 height5">
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4 labelText1 paddingLeft0">
                                                                Length
                                                            </div>
                                                            <div class="col-sm-4 paddingLeft0 paddingRight0">
                                                                <asp:TextBox runat="server" AutoPostBack="True" onkeypress="return isNumberKey(event)" ID="txtlength" CausesValidation="false" CssClass="form-control" OnTextChanged="txtlength_TextChanged"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-4 paddingLeft0 paddingRight0">
                                                                <asp:DropDownList ID="ddluomlength" AutoPostBack="True" CausesValidation="false" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddluomlength_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 height5">
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4 labelText1 paddingLeft0">
                                                                Height
                                                            </div>
                                                            <div class="col-sm-4 paddingLeft0 paddingRight0">
                                                                <asp:TextBox runat="server" onkeypress="return isNumberKey(event)" AutoPostBack="True" ID="txtheight" CausesValidation="false" CssClass="form-control" OnTextChanged="txtheight_TextChanged"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-4 paddingLeft0 paddingRight0">
                                                                <asp:DropDownList ID="ddluomheight" AutoPostBack="True" CausesValidation="false" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddluomheight_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 height5">
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4 labelText1 paddingLeft0">
                                                                Max Weight
                                                            </div>
                                                            <div class="col-sm-4 paddingLeft0 paddingRight0">
                                                                <asp:TextBox runat="server" onkeypress="return isNumberKey(event)" AutoPostBack="true" ID="txtMaxweight" CausesValidation="false" CssClass="form-control" OnTextChanged="txtMaxweight_TextChanged"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-4 paddingLeft0 paddingRight0">
                                                                <asp:DropDownList ID="ddluommaxweight" AutoPostBack="True" CausesValidation="false" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6  paddingRight0">
                                                            <div class="col-sm-2 labelText1 paddingLeft0">
                                                                Tot.Space
                                                            </div>
                                                            <div class="col-sm-5 paddingLeft10 paddingRight0">
                                                                <asp:TextBox runat="server" ReadOnly="true" MaxLength="8" ID="txtTotspace" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-4 paddingLeft0 paddingRight0">
                                                                <asp:DropDownList ID="ddluomtot" Enabled="false" AutoPostBack="True" CausesValidation="false" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 height5">
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-2 labelText1 paddingLeft0">
                                                                Utilized
                                                            </div>
                                                            <div class="col-sm-5 paddingLeft10 paddingRight0">
                                                                <asp:TextBox runat="server" Text="0" ReadOnly="true" ID="txtutility" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-4 paddingLeft0 paddingRight0">
                                                                <asp:DropDownList ID="ddluomutiliz" Enabled="false" AutoPostBack="True" CausesValidation="false" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 height5">
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-2 labelText1 paddingLeft0">
                                                                Free
                                                            </div>
                                                            <div class="col-sm-5 paddingLeft10 paddingRight0">
                                                                <asp:TextBox runat="server" Text="0" ReadOnly="true" ID="txtfree" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-4 paddingLeft0 paddingRight0">
                                                                <asp:DropDownList ID="ddluomfree" AutoPostBack="True" CausesValidation="false" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-7 paddingLeft0 paddingRight5 ">
                                            <div class="panel panel-default">

                                                <div class="panel-body">
                                                    <div class="col-sm-12 paddingLeft0">
                                                        <div class="col-sm-6 paddingLeft0">
                                                            <div class="row">
                                                                <div class="col-sm-12 labelText1 ">
                                                                    Store Facility
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 height5">
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-10 paddingRight0">
                                                                    <asp:DropDownList ID="ddlStorageF" CausesValidation="false" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                </div>
                                                                <div class="col-sm-1 Lwidth ">
                                                                    <asp:LinkButton ID="lbtnAddStorageF" runat="server" CausesValidation="false" OnClick="lbtnAddStorageF_Click">
                                                        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 labelText1 ">
                                                                    Bin Type
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 height5">
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-10 paddingRight0">
                                                                    <asp:DropDownList ID="ddlbintype" CausesValidation="false" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="col-sm-6 paddingLeft0 paddingRight0">
                                                            <div class="panel panel-default">
                                                                <div class=" panel-body height80 panelscollbar">
                                                                    <asp:GridView ID="grdstoreFacility" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="" Visible="true">
                                                                                <HeaderTemplate>
                                                                                    <asp:CheckBox ID="allchk" runat="server" Text="Active" Width="50px" onclick="CheckAllstoreFacility(this)"></asp:CheckBox>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chk_Facility" runat="server" Checked='<%# Bind("IBNS_ACT") %>' Width="5px" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Store Code" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_storecode" runat="server" Text='<%# Bind("IBNS_STOR_CD") %>' Width="20px"></asp:Label>
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
                                    <div class="col-sm-5 paddingLeft0 paddingRight0">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                Bin item category definition
                                            </div>
                                            <div class="panel-body">

                                                <div class="col-sm-5 paddingLeft0 paddingRight0">
                                                    <div class="row">
                                                        <div class="col-sm-5 labelText1">
                                                            Main category
                                                        </div>
                                                        <div class="col-sm-6 paddingLeft0 paddingRight0">
                                                            <asp:TextBox runat="server" ID="txtmaincat" Enabled="false" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0 Lwidth">
                                                            <asp:LinkButton ID="lbtnMacat" runat="server" CausesValidation="false" OnClick="lbtnMacat_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-5 labelText1">
                                                            Sub category 1
                                                        </div>
                                                        <div class="col-sm-6 paddingLeft0 paddingRight0">
                                                            <asp:TextBox runat="server" ID="txtsubcat1" Enabled="false" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0 Lwidth">
                                                            <asp:LinkButton ID="lbtnsubcat1" runat="server" CausesValidation="false" OnClick="lbtnsubcat1_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-5 labelText1">
                                                            Sub category 2
                                                        </div>
                                                        <div class="col-sm-6 paddingLeft0 paddingRight0">
                                                            <asp:TextBox runat="server" ID="txtsubcat2" Enabled="false" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0 Lwidth">
                                                            <asp:LinkButton ID="lbtnsubcat2" runat="server" CausesValidation="false" OnClick="lbtnsubcat2_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-5 labelText1">
                                                            Sub category 3
                                                        </div>
                                                        <div class="col-sm-6 paddingLeft0 paddingRight0">
                                                            <asp:TextBox runat="server" ID="txtsubcat3" Enabled="false" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0 Lwidth">
                                                            <asp:LinkButton ID="lbtnsubcat3" runat="server" CausesValidation="false" OnClick="lbtnsubcat3_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-5 labelText1 ">
                                                            Sub category 4
                                                        </div>
                                                        <div class="col-sm-6 paddingLeft0 paddingRight0">
                                                            <asp:TextBox runat="server" ID="txtsubcat4" Enabled="false" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0 Lwidth">
                                                            <asp:LinkButton ID="lbtnsubcat4" runat="server" CausesValidation="false" OnClick="lbtnsubcat4_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-5 labelText1 ">
                                                        </div>
                                                        <div class="col-sm-6 paddingLeft0 paddingRight0">
                                                            <asp:LinkButton ID="lbtnAddItem" runat="server" CausesValidation="false" OnClick="lbtnAddItem_Click">
                                                        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0 Lwidth">
                                                        </div>

                                                    </div>
                                                </div>


                                                <div class="col-sm-7">
                                                    <div class="panel panel-default">
                                                        <div class=" panel-body height200 panelscollbar">
                                                            <asp:GridView ID="grdBinItem" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="" Visible="true">
                                                                        <HeaderTemplate>
                                                                            <asp:CheckBox ID="allchk" Text="Active" runat="server" Width="50px" onclick="CheckAllBinItem(this)"></asp:CheckBox>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chk_BinItem" runat="server" Checked='<%# Bind("IBNI_ACT") %>' Width="5px" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Main Item" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_IBNI_CAT_CD1" runat="server" Text='<%# Bind("IBNI_CAT_CD1") %>' Width="100px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="S.category1" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_IBNI_CAT_CD2" runat="server" Text='<%# Bind("IBNI_CAT_CD2") %>' Width="70px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="S.category2" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="IBNI_CAT_CD3" runat="server" Text='<%# Bind("IBNI_CAT_CD3") %>' Width="70px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="S.category3" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_IBNI_CAT_CD4" runat="server" Text='<%# Bind("IBNI_CAT_CD4") %>' Width="70px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="S.category4" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_IBNI_CAT_CD5" runat="server" Text='<%# Bind("IBNI_CAT_CD5") %>' Width="70px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Line" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_itemline" runat="server" Text='<%# Bind("IBNI_CAT_LINE") %>' Width="70px"></asp:Label>
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="tab-pane" id="LoadingBay">
                    <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>

                            <div class="row">
                                <div class="col-sm-12 ">
                                    <div class="panel panel-default">
                                        <div class="panel panel-heading">
                                            <strong><b>Loading Bay</b></strong>
                                        </div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-sm-6  buttonrow">
                                                    <div id="Div5" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">

                                                        <div class="col-sm-11">
                                                            <strong>Alert!</strong>
                                                            <asp:Label ID="Label1" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-1">
                                                            <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnlayplaneclose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div id="Div7" runat="server" visible="false" class="alert alert-success alert-dismissible" role="alert">
                                                        <div class="col-sm-11">
                                                            <strong>Success!</strong>
                                                            <asp:Label ID="Label5" runat="server"></asp:Label>

                                                        </div>
                                                        <div class="col-sm-1">
                                                            <asp:LinkButton ID="LinkButton5" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnlayplaneclose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>

                                                    </div>
                                                    <div id="Div8" runat="server" visible="false" class="alert alert-info alert-dismissible" role="alert">
                                                        <strong>Alert!</strong>
                                                        <asp:Label ID="Label7" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6  buttonRow">
                                                    <div class="col-sm-5">
                                                    </div>
                                                    <%--OnClientClick="SaveConfirm()"--%>
                                                    <div class="col-sm-3 paddingRight0">
                                                        <asp:LinkButton ID="lbtnLoadBaySave" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lbtnLoadBaySave_Click"> 
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Add New/Update
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <asp:LinkButton ID="lbtnLoadBayClean" runat="server" CssClass="floatRight" OnClientClick="ClearConfirm()" OnClick="lbtnLoadBayClean_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="panel panel-default">
                                                        <div class=" panel-body">

                                                            <div class="row">
                                                                <div class="col-sm-4" style="width: 415px">
                                                                    <div class="col-sm-2 labelText1 paddingLeft0 paddingRight0">
                                                                        User ID
                                                                    </div>
                                                                    <div class="col-sm-3 paddingLeft0 paddingRight0">
                                                                        <asp:TextBox runat="server" AutoPostBack="true" ID="txtUserId" CausesValidation="false" CssClass="form-control" OnTextChanged="txtUserId_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft5 Lwidth" id="divUser">
                                                                        <asp:LinkButton ID="lbtnUser" runat="server" CausesValidation="false" OnClick="lbtnUser_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-sm-2 labelText1 paddingLeft0 paddingRight0">
                                                                        User Name :
                                                                    </div>
                                                                    <div class="col-sm-4 paddingLeft0 paddingRight0">
                                                                        <asp:Label ID="lblUserNameView" runat="server" Text=""></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4" style="width: 375px">
                                                                    <div class="col-sm-2 labelText1 paddingLeft0 paddingRight0">
                                                                        Location
                                                                    </div>
                                                                    <div class="col-sm-3 paddingLeft20 paddingRight0">
                                                                        <asp:TextBox runat="server" AutoPostBack="true" ID="txtLocationID" CausesValidation="false" CssClass="form-control" OnTextChanged="txtLocationID_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft5 Lwidth">
                                                                        <asp:LinkButton ID="lbtnLocation" runat="server" CausesValidation="false" OnClick="lbtnLocation_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-sm-2 labelText1 paddingLeft0 paddingRight0">
                                                                        Location :
                                                                    </div>
                                                                    <div class="col-sm-4 paddingLeft0 paddingRight0">
                                                                        <asp:Label ID="lblLocationView" runat="server" Text=""></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4" style="width: 380px">
                                                                    <div class="col-sm-2 labelText1 paddingLeft0 paddingRight0">
                                                                        Loading Bay
                                                                    </div>
                                                                    <div class="col-sm-3 paddingLeft20 paddingRight0">
                                                                        <asp:TextBox runat="server" AutoPostBack="true" ID="txtLoadingBayID" CausesValidation="false" CssClass="form-control" OnTextChanged="txtLoadingBayID_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft5 Lwidth">
                                                                        <asp:LinkButton ID="lbtnLoadingBay" runat="server" CausesValidation="false" OnClick="lbtnLoadingBay_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-sm-2 labelText1 paddingLeft0 paddingRight0">
                                                                        Bay :
                                                                    </div>
                                                                    <div class="col-sm-4 paddingLeft0 paddingRight0">
                                                                        <asp:Label ID="lblLoadingBayView" runat="server" Text=""></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-1" style="padding-right: 25px">
                                                                    <div class="col-sm-1">
                                                                        Search
                                                                    </div>
                                                                    <div class="col-sm-1" style="padding-left: 25px">
                                                                        <asp:LinkButton ID="lbtnBaySearch" runat="server" CausesValidation="false" OnClick="lbtnBaySearch_Click" Text="Search">
                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>


                                                            <div class="row" style="padding-right: 85px">
                                                                <div class="col-sm-4">
                                                                    <div class="col-sm-1 labelText1">
                                                                        Active
                                                                    </div>
                                                                    <div class="col-sm-1 paddingRight5 labelText1">
                                                                        <asp:CheckBox ID="chkactive" runat="server" TabIndex="1" />
                                                                    </div>

                                                                    <div class="col-sm-1 labelText1" style="padding-right: 25px">
                                                                        Default
                                                                    </div>
                                                                    <div class="col-sm-1 paddingRight5 labelText1">
                                                                        <asp:CheckBox ID="chkdef" runat="server" TabIndex="1" />
                                                                    </div>
                                                                    <div class="col-sm-1 paddingRight5 labelText1" style="padding-bottom: 10px">
                                                                        <asp:LinkButton ID="lbtnadd" runat="server" OnClick="lbtnadd_Click">
                                                            <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
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
                                                        <div class=" panel-body height200 panelscollbar">
                                                            <asp:GridView ID="grdLocationBay" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false" OnSelectedIndexChanged="grdLocationBay_SelectedIndexChanged" OnDataBound="grdLocationBay_DataBound">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtnlocationBay" runat="server" CausesValidation="false" CommandName="Select">
                                                                            <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="User Id" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_userId" runat="server" Text='<%# Bind("USERID")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="User" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_user" runat="server" Text='<%# Bind("USER1")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Location" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_locationID" runat="server" Text='<%# Bind("LOCATIONID")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Location" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_location" runat="server" Text='<%# Bind("LOCATION")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Loading Bay" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_loadingBayID" runat="server" Text='<%# Bind("LOADINGBAYID")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Loading Bay" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_loadingBay" runat="server" Text='<%# Bind("LOADINGBAY")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Active" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_Active" runat="server" Text='<%# Bind("ACTIVE") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Default" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_Default" runat="server" Text='<%# Bind("DEFAULT1")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Line" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_Line" runat="server" Text='<%# Bind("LOADBAYLINE")%>'></asp:Label>
                                                                            <asp:HiddenField ID="hdnline" runat="server" Value='<%# Bind("LOADBAYLINE") %>' />
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

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

               <div class="tab-pane" id="DocLoadingBay">
                    <asp:ScriptManagerProxy ID="ScriptManagerProxy5" runat="server"></asp:ScriptManagerProxy>
                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                        <ContentTemplate>

                            <div class="row">
                                <div class="col-sm-12 ">
                                    <div class="panel panel-default">
                                        <div class="panel panel-heading">
                                            <strong><b>Loading Bay</b></strong>
                                        </div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-sm-6  buttonrow">
                                                    <div id="Div9" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">

                                                        <div class="col-sm-11">
                                                            <strong>Alert!</strong>
                                                            <asp:Label ID="Label8" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-1">
                                                            <asp:LinkButton ID="LinkButton6" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnlayplaneclose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div id="Div10" runat="server" visible="false" class="alert alert-success alert-dismissible" role="alert">
                                                        <div class="col-sm-11">
                                                            <strong>Success!</strong>
                                                            <asp:Label ID="Label9" runat="server"></asp:Label>

                                                        </div>
                                                        <div class="col-sm-1">
                                                            <asp:LinkButton ID="LinkButton7" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnlayplaneclose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>

                                                    </div>
                                                    <div id="Div11" runat="server" visible="false" class="alert alert-info alert-dismissible" role="alert">
                                                        <strong>Alert!</strong>
                                                        <asp:Label ID="Label10" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6  buttonRow">
                                                    <div class="col-sm-5">
                                                    </div>
                                                    <%--OnClientClick="SaveConfirm()"--%>
                                                    <div class="col-sm-3 paddingRight0">
                                                        <asp:LinkButton ID="lbtnDocLoadBaySave" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lbtnDocLoadBaySave_Click"> 
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Add New/Update
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <asp:LinkButton ID="LinkButton11" runat="server" CssClass="floatRight" OnClientClick="ClearConfirm()" OnClick="lbtnLoadBayClean_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="panel panel-default">
                                                        <div class=" panel-body">

                                                            <div class="row">
                                                                <div class="col-sm-4" style="width: 415px">
                                                                    <div class="col-sm-2 labelText1 paddingLeft0 paddingRight0">
                                                                        Doc. No
                                                                    </div>
                                                                    <div class="col-sm-3 paddingLeft0 paddingRight0">
                                                                        <asp:TextBox runat="server" AutoPostBack="true" ID="txtDocNo" CausesValidation="false" CssClass="form-control" OnTextChanged="txtDocNo_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft5 Lwidth" id="divDoc">
                                                                        <asp:LinkButton ID="LinkButton12" runat="server" CausesValidation="false" OnClick="lbtnDocno_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-sm-2 labelText1 paddingLeft0 paddingRight0">
                                                                        Doc. No :
                                                                    </div>
                                                                    <div class="col-sm-4 paddingLeft0 paddingRight0">
                                                                        <asp:Label ID="lblDocNoView" runat="server" Text=""></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4" style="width: 380px">
                                                                    <div class="col-sm-2 labelText1 paddingLeft0 paddingRight0">
                                                                        Loading Bay
                                                                    </div>
                                                                    <div class="col-sm-3 paddingLeft20 paddingRight0">
                                                                        <asp:TextBox runat="server" AutoPostBack="true" ID="txtDocLoadBay" CausesValidation="false" CssClass="form-control" OnTextChanged="txtDocLoadBay_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft5 Lwidth">
                                                                        <asp:LinkButton ID="LinkButton15" runat="server" CausesValidation="false" OnClick="lbtnDocLoadingBay_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-sm-2 labelText1 paddingLeft0 paddingRight0">
                                                                        Bay :
                                                                    </div>
                                                                    <div class="col-sm-4 paddingLeft0 paddingRight0">
                                                                        <asp:Label ID="lblLoadingDocBayView" runat="server" Text=""></asp:Label>
                                                                    </div>
                                                                </div>                                                                
                                                            </div>
                                                            <div class="row" style="padding-right: 85px">
                                                                <div class="col-sm-4">
                                                                    
                                                                    <div class="col-sm-1 paddingRight5 labelText1" style="padding-bottom: 10px">
                                                                        <asp:LinkButton ID="LinkButton14" runat="server" OnClick="lbtnDocAdd_Click">
                                                            <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
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
                                                        <div class=" panel-body height200 panelscollbar">
                                                            <asp:GridView ID="grdDocLocationBay" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false" OnSelectedIndexChanged="grdDocLocationBay_SelectedIndexChanged" OnDataBound="grdLocationBay_DataBound">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtnlocationBay" runat="server" CausesValidation="false" CommandName="Select">
                                                                            <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Doc. No" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_docno" runat="server" Text='<%# Bind("TUH_DOC_NO")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <%--<asp:TemplateField HeaderText="User" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_user" runat="server" Text='<%# Bind("USER1")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>
<%--                                                                    <asp:TemplateField HeaderText="Location" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_locationID" runat="server" Text='<%# Bind("LOCATIONID")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Location" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_location" runat="server" Text='<%# Bind("LOCATION")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>
                                                                    <asp:TemplateField HeaderText="Loading Bay" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_loadingBayID" runat="server" Text='<%# Bind("TUH_LOAD_BAY")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Loading Bay" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_loadingBay" runat="server" Text='<%# Bind("TUH_LOAD_BAY")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <%--<asp:TemplateField HeaderText="Active" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_Active" runat="server" Text='<%# Bind("ACTIVE") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Default" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="col_Default" runat="server" Text='<%# Bind("DEFAULT1")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>
                                                                    <asp:TemplateField HeaderText="Line" Visible="false">
                                                                        <ItemTemplate>
                                                                            <%--<asp:Label ID="col_Line" runat="server" Text='<%# Bind("LOADBAYLINE")%>'></asp:Label>
                                                                            <asp:HiddenField ID="hdnline" runat="server" Value='<%# Bind("LOADBAYLINE") %>' />--%>
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

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

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

    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="MaxBin" runat="server" Enabled="True" TargetControlID="Button2"
                PopupControlID="pnlBinCreate" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlBinCreate">
                <div runat="server" id="Div4" class="panel panel-default Mheight2 ">

                    <asp:Label ID="Label4" runat="server" Text="Label" Visible="false"></asp:Label>

                    <div class="panel panel-default">

                        <div class="panel-body">
                            <div class="col-sm-11 paddingRight5 paddingLeft0">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-8">
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:LinkButton ID="lbtnAddManulBin" runat="server" OnClick="lbtnAddManulBin_Click">
                             <span class="glyphicon glyphicon-saved" aria-hidden="true">Add</span>
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-1">
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:LinkButton ID="LinkButton2" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                                            </asp:LinkButton>
                                        </div>

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-5 labelText1">
                                        Bin Description
                                    </div>
                                    <div class="col-sm-6 paddingRight5 paddingLeft0">
                                        <asp:TextBox runat="server" ID="txtManulBinDes" TextMode="MultiLine" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-5 labelText1">
                                        Bin Location
                                    </div>
                                    <div class="col-sm-6 paddingRight5 paddingLeft0">
                                        <asp:TextBox runat="server" ID="txtManulBinLoc" MaxLength="20" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3 labelText1">
                                    </div>
                                    <div class="col-sm-2 labelText1">
                                        From
                                    </div>
                                    <div class="col-sm-2 paddingRight5 paddingLeft0">
                                        <asp:TextBox runat="server" ID="txtfrom" MaxLength="20" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-1 labelText1">
                                        To
                                    </div>
                                    <div class="col-sm-3 paddingRight5 paddingLeft5">
                                        <asp:TextBox runat="server" ID="txtTo" MaxLength="20" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height20">
                                    </div>
                                </div>



                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="excelUpload" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="pnlexcel" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlexcel">
        <div runat="server" id="Div1" class="panel panel-default height45 width700 ">


            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="LinkButton1" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <%--<span>Commen Search</span>--%>
                    <div class="col-sm-11">
                        <asp:Label ID="lblalert" runat="server" ForeColor="Red" Text="Label" Visible="false"></asp:Label>
                        <asp:Label ID="lblsuccess" runat="server" ForeColor="Green" Visible="false"></asp:Label>
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

                            <div class="col-sm-12" id="Div2" runat="server">
                                <div class="col-sm-5 paddingRight5">
                                    <asp:FileUpload ID="fileupexcelupload" runat="server" />

                                </div>

                                <div class="col-sm-2 paddingRight5">
                                    <asp:Button ID="btnUpload" class="btn btn-warning btn-xs" runat="server" Text="Upload"
                                        OnClick="btnUpload_Click" />
                                </div>


                            </div>


                            <%--<div class="row">--%>
                            <div class="col-sm-12 ">
                                <div id="divUpcompleted" visible="false" runat="server" class="alert alert-info alert-success" role="alert">
                                    <div class="col-sm-9">
                                        <strong>Alert!</strong>
                                        <asp:Label ID="Label2" Text="Excel file upload completed. Do you want to process?" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="btnprocess" class="btn btn-success btn-xs" runat="server" Text="Process" OnClick="btnprocess_Click" />
                                    </div>
                                </div>
                            </div>


                        </div>
                    </div>
                </div>
            </div>
        </div>

    </asp:Panel>


</asp:Content>
