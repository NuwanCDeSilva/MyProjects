<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="CostStucture.aspx.cs" Inherits="FastForward.SCMWeb.View.MasterFiles.Imports.CostStucture" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>
        function ClearConfirm() {
            var selectedvalue = confirm("Do you want to Clear data?");
            if (selectedvalue) {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "No";
            }
        };
        function SaveConfirm() {
            var selectedvalue = confirm("Do you want to Save data?");
            if (selectedvalue) {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "No";
            }
        };
        function UpdateConfirm() {
            var selectedvalue = confirm("Do you want to Save data?");
            if (selectedvalue) {
                document.getElementById('<%=txtUpdateconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtUpdateconformmessageValue.ClientID %>').value = "No";
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
        function CheckBoxCheck(rb) {
            debugger;
            var gv = document.getElementById("<%=grdCostcat.ClientID%>");
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

        function CheckBoxCheck2(rb) {
            debugger;
            var gv = document.getElementById("<%=grdCosetSeg.ClientID%>");
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="panel panel-default marginLeftRight5">
        <div class="panel-body">
            <div class="bs-example">
                <ul class="nav nav-tabs" id="myTab">
                    <li class="active"><a href="#CostElement" data-toggle="tab">Cost Element</a></li>
                    <li><a href="#CostCategory" data-toggle="tab">Cost Category Master</a></li>
                    <li><a href="#CostSegment" data-toggle="tab">Cost Segment Master</a></li>

                </ul>
            </div>
            <div class="tab-content">
                <asp:HiddenField ID="txtClearlconformmessageValue" runat="server" />
                <asp:HiddenField ID="txtSavelconformmessageValue" runat="server" />
                <asp:HiddenField ID="txtUpdateconformmessageValue" runat="server" />
                <asp:HiddenField ID="txtApprovalconformmessageValue" runat="server" />
                <asp:HiddenField ID="txtCancelconformmessageValue" runat="server" />
                <asp:HiddenField ID="txtDeleteconformmessageValue" runat="server" />
                <div class="tab-pane active" id="CostElement">

                    <asp:ScriptManagerProxy ID="ScriptManagerProxy5" runat="server"></asp:ScriptManagerProxy>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-sm-8  buttonrow">
                                    <div id="WarnningCostType" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">

                                        <div class="col-sm-11">
                                            <strong>Alert!</strong>
                                            <asp:Label ID="lblWarnningCostType" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnWarningCostCategoryClose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div id="SuccessCostType" runat="server" visible="false" class="alert alert-success alert-dismissible" role="alert">
                                        <div class="col-sm-11">
                                            <strong>Success!</strong>
                                            <asp:Label ID="lblSuccessCostType" runat="server"></asp:Label>

                                        </div>
                                        <div class="col-sm-1">
                                            <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnWarningCostCategoryClose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>

                                    </div>
                                    <div id="Div6" runat="server" visible="false" class="alert alert-info alert-dismissible" role="alert">
                                        <strong>Alert!</strong>
                                        <asp:Label ID="Label6" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-4  buttonRow">
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-4 paddingRight0">
                                        <asp:LinkButton ID="lbtnCostTypeAdd" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="SaveConfirm()" OnClick="lbtnCostTypeAdd_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Add New/Update
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:LinkButton ID="lbtnCostTypeClear" runat="server" CssClass="floatRight" OnClientClick="ClearConfirm()" OnClick="lbtnCostTypeClear_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                        </asp:LinkButton>
                                    </div>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6 ">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="col-sm-2">
                                        <div class="panel panel-default height400">
                                            <div class="panel-heading">
                                            </div>
                                            <div class="panel-body panelscoll4 ">

                                                <asp:TreeView ID="treeView1" CssClass="nav nav-list tree" runat="server" ShowCheckBoxes="All" ShowLines="true" Height="100px" Width="100px" ExpandDepth="0" EnableClientScript="false">
                                                </asp:TreeView>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-5">
                                        <div class="panel panel-default height400">
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-sm-12  height80">
                                                        <div class="panel panel-default">
                                                            <div class="panel-body panelscoll">
                                                                <asp:GridView ID="grdCostcat" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false" OnSelectedIndexChanged="grdCostcat_SelectedIndexChanged">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="COST_CS" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" onclick="CheckBoxCheck(this);" Checked="false" Width="20px" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="CODE">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="CODE" runat="server" Text='<%# Bind("CODE") %>' Width="5px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="DESCRIPTION">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="DESCRIPTION" runat="server" Text='<%# Bind("DESCRIPTION") %>' Width="200px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height30">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="panel panel-default">

                                                            <div class="panel-body">
                                                                <div class="row">
                                                                    <div class="col-sm-3">
                                                                        Cost Category Code
                                                                    </div>
                                                                    <div class="col-sm-4">
                                                                        Cost Segment Code
                                                                    </div>
                                                                    <div class="col-sm-1">
                                                                        Active
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-3">
                                                                        <asp:TextBox ID="txtCTCategoryC" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-3">
                                                                        <asp:TextBox ID="txtCTSegC" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnCTSegC" runat="server" CausesValidation="false" OnClick="lbtnCTSegC_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-sm-1">
                                                                        <asp:CheckBox ID="chkCTActive" Checked="true" runat="server" />
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnAddSeg" runat="server" CausesValidation="false" OnClick="lbtnAddSeg_Click">
                                                        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="panel panel-default">
                                                            <div class="panel-heading">
                                                            </div>
                                                            <div class="panel-body panelscoll2">
                                                                <asp:GridView ID="grdCosetSeg" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="COST_Seg_" runat="server" AutoPostBack="true" OnCheckedChanged="COST_Seg__CheckedChanged" Checked="false" Width="20px" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="CODE">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="mcat_cd" runat="server" Text='<%# Bind("mcat_cd") %>' Width="5px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="DESCRIPTION">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="msse_desc" runat="server" Text='<%# Bind("msse_desc") %>' Width="200px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Active" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="mcat_act" runat="server" Checked='<%# Eval("mcat_act").ToString().ToUpper().Trim() == "1" ? true : false %>' Width="20px" />
                                                                                <%--  <asp:Label ID="mcat_act" runat="server" Text='<%# Bind("mcat_act") %>' Width="200px"></asp:Label>--%>
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

                                    <div class="col-sm-5">
                                        <div class="row">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                </div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-sm-4">
                                                            Cost Segment Code
                                                        </div>
                                                        <div class="col-sm-2" runat="server" visible="false">
                                                            Edit
                                                        </div>
                                                        <div class="col-sm-2">
                                                            Active
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-3">
                                                            <asp:TextBox ID="txtcostEleSeg" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                            <asp:LinkButton ID="lbtneleseg" runat="server" CausesValidation="false" OnClick="lbtneleseg_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                        <div class="col-sm-2" runat="server" Visible="false">
                                                            <asp:CheckBox ID="chkEleEdit"  Checked="true" runat="server" />
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <asp:CheckBox ID="chkEleActive" Checked="true" runat="server" />
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                            <asp:LinkButton ID="lbtnAddELE" runat="server" CausesValidation="false" OnClick="lbtnAddELE_Click">
                                                        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                </div>

                                                <div class="panel-body height350">
                                                    <asp:GridView ID="gridELE" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="C.Code">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="mcae_ele_cat" runat="server" Text='<%# Bind("mcae_ele_cat") %>' Width="20px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="S. Code">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="mcae_ele_tp" runat="server" Text='<%# Bind("mcae_ele_tp") %>' Width="20px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Code">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="mcae_cd" runat="server" Text='<%# Bind("mcae_cd") %>' Width="20px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="EDIT" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="mcae_is_edit" runat="server" AutoPostBack="true" Checked='<%# Eval("mcae_is_edit").ToString().ToUpper().Trim() == "1" ? true : false %>' Width="20px" />
                                                                    <%--  <asp:Label ID="mcat_act" runat="server" Text='<%# Bind("mcat_act") %>' Width="200px"></asp:Label>--%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Active" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="mcae_act" runat="server" AutoPostBack="true" Checked='<%# Eval("mcae_act").ToString().ToUpper().Trim() == "1" ? true : false %>' Width="20px" />
                                                                    <%--  <asp:Label ID="mcat_act" runat="server" Text='<%# Bind("mcat_act") %>' Width="200px"></asp:Label>--%>
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
                <div class="tab-pane" id="CostCategory">
                    <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                    <asp:UpdatePanel ID="CostCategoryU" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-sm-8  buttonrow">
                                    <div id="WarningCostCategory" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">
                                        <div class="col-sm-11">
                                            <strong>Alert!</strong>
                                            <asp:Label ID="lblWarningCostCategory" runat="server"></asp:Label>

                                        </div>
                                        <div class="col-sm-1">
                                            <asp:LinkButton ID="lbtnWarningCostCategoryClose" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnWarningCostCategoryClose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>



                                    </div>


                                    <div id="SuccessCostCategory" runat="server" visible="false" class="alert alert-success alert-dismissible" role="alert">
                                        <div class="col-sm-11">
                                            <strong>Success!</strong>
                                            <asp:Label ID="lblSuccessCostCategory" runat="server"></asp:Label>

                                        </div>
                                        <div class="col-sm-1">
                                            <asp:LinkButton ID="lbtnSuccessCostCategoryclose" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnWarningCostCategoryClose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div id="AlertUser" runat="server" visible="false" class="alert alert-info alert-dismissible" role="alert">
                                        <strong>Alert!</strong>
                                        <asp:Label ID="lblUAlert" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-4  buttonRow">
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-4 paddingRight0">
                                        <asp:LinkButton ID="btnAddNewCostCategory" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="SaveConfirm()" OnClick="btnAddNewCostCategory_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Add New/Update
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:LinkButton ID="lblCostCategoryClear" runat="server" CssClass="floatRight" OnClientClick="ClearConfirm()" OnClick="lblCostCategoryClear_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                        </asp:LinkButton>
                                    </div>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">Cost Category Master</div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-sm-4 labelText1">
                                                    Cost Category Code
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtCostCategoryCode" MaxLength="5" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnCaterCode" runat="server" CausesValidation="false" OnClick="lbtnCaterCode_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4 labelText1">
                                                    Description
                                                </div>
                                                <div class="col-sm-7 height20">
                                                    <asp:TextBox ID="txtCaterDes" MaxLength="25"  CausesValidation="false" AutoPostBack="true"  CssClass="form-control" runat="server" onkeypress="filterDigits(event)"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height20">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4 labelText1">
                                                    Active
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:CheckBox ID="chkCatergoryActive" Checked="true" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height22">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="tab-pane" id="CostSegment">
                    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-sm-8  buttonrow">
                                    <div id="WarnningCostSegment" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">
                                        <div class="col-sm-11">
                                            <strong>Alert!</strong>
                                            <asp:Label ID="lblWarnningCostSegment" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnWarningCostCategoryClose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div id="SuccessCostSegment" runat="server" visible="false" class="alert alert-success alert-dismissible" role="alert">
                                        <div class="col-sm-11">
                                            <strong>Success!</strong>
                                            <asp:Label ID="lblSuccessCostSegment" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnWarningCostCategoryClose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div id="Div3" runat="server" visible="false" class="alert alert-info alert-dismissible" role="alert">
                                        <strong>Alert!</strong>
                                        <asp:Label ID="Label3" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-4  buttonRow">
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-4 paddingRight0">
                                        <asp:LinkButton ID="lbtnCostSegmentAdd" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="SaveConfirm()" OnClick="lbtnCostSegmentAdd_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Add New/Update
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:LinkButton ID="lbtnCostSegmentClear" runat="server" CssClass="floatRight" OnClientClick="ClearConfirm()" OnClick="lblCostCategoryClear_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                        </asp:LinkButton>
                                    </div>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">Cost Segment Master</div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-sm-4 labelText1">
                                                    Cost Segment Code
                                                </div>
                                                <div class="col-sm-5">
                                                    <asp:TextBox ID="txtCSegmentCode" MaxLength="5" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnCsegCode" runat="server" CausesValidation="false" OnClick="lbtnCsegCode_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4 labelText1">
                                                    Description
                                                </div>
                                                <div class="col-sm-7 height20">
                                                    <asp:TextBox ID="txtSegDescription" MaxLength="25"  CausesValidation="false" AutoPostBack="true"  CssClass="form-control" runat="server" onkeypress="filterDigits(event)"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height20">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4 labelText1">
                                                    Active
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:CheckBox ID="chkSegActive" Checked="true" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height22">
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
</asp:Content>
