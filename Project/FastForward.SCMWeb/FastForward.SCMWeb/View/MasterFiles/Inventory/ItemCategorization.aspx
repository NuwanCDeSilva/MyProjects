<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="ItemCategorization.aspx.cs" Inherits="FastForward.SCMWeb.View.MasterFiles.Inventory.Item_Categorization" %>

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
        function CancelConfirm() {
            var selectedvalue = confirm("Do you want to cancel MRN?");
            if (selectedvalue) {
                document.getElementById('<%=txtCancelconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtCancelconformmessageValue.ClientID %>').value = "No";
            }
        };
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
              && (charCode < 48 || charCode > 57))
                return false;
            return true;
        };

        function CheckAllgrdreqItem(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdCate1.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }
        function CheckAllgrdCate2(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdCate2.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }
        function CheckAllgrdCate3(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdCate3.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }
        function CheckAllgrdCate4(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdCate4.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }
        function CheckAllgrdmodel(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdModel.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }

        function CheckAllgrdBrand(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdBrand.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }
        function CheckAllgrdUOM(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdUOM.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }
        function CheckAllgrdColor(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdColor.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }

        function CheckBoxCheckReq_item(rb) {
            debugger;
            var gv = document.getElementById("<%=grdCate1.ClientID%>");
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
    <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel1">

        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait2" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait2" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>

    </asp:UpdateProgress>
    <asp:UpdateProgress ID="UpdateProgress3" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel3">

        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait3" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait3" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>

    </asp:UpdateProgress>
    <asp:UpdateProgress ID="UpdateProgress4" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel4">

        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait4" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait4" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>

    </asp:UpdateProgress>
    <asp:UpdateProgress ID="UpdateProgress5" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel5">

        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait5" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait5" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:HiddenField ID="txtClearlconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtSavelconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtUpdateconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtApprovalconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtCancelconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtDeleteconformmessageValue" runat="server" />
    <asp:HiddenField ID="hdfTabIndex" runat="server" />
    <asp:HiddenField ID="TabName" runat="server" />
    <div class="panel panel-default marginLeftRight5">
        <div class="panel-body">
            <div class="row paddingtopbottom0">
                <div class="col-sm-8 paddingtopbottom0">
                </div>
                <div class="col-sm-4 buttonRow">
                    <div class="col-sm-10 paddingLeft0 paddingRight0">
                    </div>
                    <div class="col-sm-2 paddingLeft0 paddingRight0">
                        <asp:LinkButton ID="lbtnClear" runat="server" CausesValidation="false" OnClick="lbtnClear_Click" OnClientClick="ClearConfirm()">
                                                                    <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                        </asp:LinkButton>
                    </div>

                </div>
            </div>
            <div class="row">
                <div class="col-sm-12 ">
                    <div class="bs-example">
                        <ul class="nav nav-tabs" id="myTab">
                            <li class="active"><a href="#ItemCategorization" data-toggle="tab">Item Categorization</a></li>
                            <li onclick="document.getElementById('<%= lblmodelgrid.ClientID %>').click();"><a href="#ItemModel" data-toggle="tab">Item Model</a></li>
                            <li onclick="document.getElementById('<%= lbtnBrand.ClientID %>').click();"><a href="#ItemBrand" data-toggle="tab">Item Brand</a></li>
                            <li onclick="document.getElementById('<%= lbtnUOM.ClientID %>').click();"><a href="#ItemUOM" data-toggle="tab">Item UOM </a></li>
                            <li onclick="document.getElementById('<%= lbtnColor.ClientID %>').click();"><a href="#ItemColor" data-toggle="tab">Item Color</a></li>
                        </ul>
                    </div>
                    <div class="col-sm-12 ">
                        <div class="tab-content">
                            <div class="tab-pane active" id="ItemCategorization">

                                <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>

                                        <div class="row">

                                            <div class="col-sm-12">

                                                <div class="panel panel-default">
                                                    <%--   <div class="panel-heading paddingtopbottom0">Main Categorization</div>--%>
                                                    <div class="panel-body">
                                                        <div class="col-sm-4">
                                                            <div class="row">
                                                                <div class="panel panel-default">
                                                                    <div class="panel-heading paddingtopbottom0">Main Categorization</div>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-sm-2 labelText1">
                                                                        Code 
                                                                    </div>
                                                                    <div class="col-sm-5">
                                                                        <asp:TextBox runat="server" Style="text-transform: uppercase" MaxLength="15" ID="txtCate1" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtCate1_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnCate1" runat="server" CausesValidation="false" OnClick="lbtnCate1_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-sm-2 labelText1">
                                                                        Active
                                                                    </div>
                                                                    <div class="col-sm-1">
                                                                        <asp:CheckBox ID="check_active_cat1" runat="server" Width="5px"></asp:CheckBox>
                                                                    </div>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-sm-2 labelText1">
                                                                        Description 
                                                                    </div>
                                                                    <div class="col-sm-9">
                                                                        <asp:TextBox runat="server" ID="txtCate1Des" CausesValidation="false" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnAddCate1" runat="server" CausesValidation="false" OnClick="lbtnAddCate1_Click">
                                                                    <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-3 labelText1 ">
                                                                    </div>

                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0 paddingLeft0">
                                                                        <asp:LinkButton ID="lbtnSave1" runat="server" CausesValidation="false" OnClick="lbtnSave1_Click">
                                                                    <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-sm-1 labelText1">
                                                                    </div>
                                                                    <div class="col-sm-3 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnClear1" runat="server" CausesValidation="false" OnClick="lbtnClear1_Click" OnClientClick="ClearConfirm()">
                                                                    <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-sm-3 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnDeletecat1" Visible="false" runat="server" CausesValidation="false" OnClick="lbtnDeletecat1_Click" OnClientClick="DeleteConfirm()">
                                                                    <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>Delete
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="col-sm-8">
                                                            <div class="row">
                                                                <div class="col-sm-1 labelText1">
                                                                    Code 
                                                                </div>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox runat="server" Style="text-transform: uppercase" MaxLength="15" ID="txtcatsearch" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtcatsearch_TextChanged"></asp:TextBox>
                                                                </div>
                                                                  <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" OnClick="lbtnCate1_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 ">
                                                                    <div class="panel-body  panelscollbar height85 paddingtopbottom0">
                                                                        <asp:GridView ID="grdCate1" ShowHeader="true" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                            EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="" Visible="false">
                                                                                    <HeaderTemplate>
                                                                                        <asp:CheckBox ID="allchk_reqitem" runat="server" Width="5px" onclick="CheckAllgrdreqItem(this)"></asp:CheckBox>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chk_ReqItem" AutoPostBack="true" runat="server" Checked="false" Width="5px" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Add" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lbtnCat1Select" runat="server" CausesValidation="false" Width="5px" OnClick="lbtnCat1Select_Click">
                                                                        <span class="glyphicon glyphicon-arrow-down" aria-hidden="true" ></span>
                                                                                        </asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Code">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="colmCode" runat="server" Text='<%# Bind("Ric1_cd") %>' Width="50px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Description">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="colmDes" runat="server" Text='<%# Bind("Ric1_desc") %>' Width="150px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Active">
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="colmStataus" runat="server" Checked='<%#Convert.ToBoolean(Eval("Ric1_act")) %>' Width="5px" AutoPostBack="true" OnCheckedChanged="colmStataus_Click" />
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
                                            <div class="col-sm-12">
                                                <div class="panel panel-default">
                                                    <%-- <div class="panel-heading paddingtopbottom0">Main Categorization</div>--%>
                                                    <div class="panel-body">
                                                        <div class="col-sm-4">
                                                            <div class="row">
                                                                <div class="panel panel-default">
                                                                    <div class="panel-heading paddingtopbottom0">Sub Categorization</div>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-sm-2 labelText1">
                                                                        Code 
                                                                    </div>
                                                                    <div class="col-sm-5">
                                                                        <asp:TextBox runat="server" MaxLength="20" Style="text-transform: uppercase" ID="txtCate2" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtCate2_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnCate2" runat="server" CausesValidation="false" OnClick="lbtnCate2_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-sm-2 labelText1">
                                                                        Active
                                                                    </div>
                                                                    <div class="col-sm-1">
                                                                        <asp:CheckBox ID="check_active_cat2" runat="server" Width="5px"></asp:CheckBox>
                                                                    </div>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-sm-2 labelText1">
                                                                        Description 
                                                                    </div>
                                                                    <div class="col-sm-9">
                                                                        <asp:TextBox runat="server" ID="txtCate2Des" CausesValidation="false" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnAddCate2" runat="server" CausesValidation="false" OnClick="lbtnAddCate2_Click">
                                                                    <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-3 labelText1 ">
                                                                    </div>

                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0 paddingLeft0">
                                                                        <asp:LinkButton ID="lbtnSave2" runat="server" CausesValidation="false" OnClick="lbtnSave2_Click">
                                                                    <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-sm-1 labelText1">
                                                                    </div>
                                                                    <div class="col-sm-3 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnClear2" runat="server" CausesValidation="false" OnClick="lbtnClear2_Click" OnClientClick="ClearConfirm()">
                                                                    <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-sm-3 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnDeletecat2" Visible="false" runat="server" CausesValidation="false" OnClick="lbtnDeletecat2_Click" OnClientClick="DeleteConfirm()">
                                                                    <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>Delete
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="col-sm-8">
                                                            <div class="row">
                                                                <div class="col-sm-12 ">
                                                                    <div class="panel-body  panelscollbar height85 paddingtopbottom0">

                                                                        <asp:Label ID="lbnodat" runat="server" Text="No data found..." Visible="false"></asp:Label>
                                                                        <asp:GridView ID="grdCate2" ShowHeader="true" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                            EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="" Visible="false">
                                                                                    <HeaderTemplate>
                                                                                        <asp:CheckBox ID="allchk_reqitem" runat="server" Width="5px" onclick="CheckAllgrdCate2(this)"></asp:CheckBox>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chk_ReqItem_cat2" AutoPostBack="true" runat="server" Checked="false" Width="5px" />
                                                                                    </ItemTemplate>

                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Add" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lbtnCat2Select" runat="server" CausesValidation="false" Width="5px" OnClick="lbtnCat2Select_Click">
                                                                        <span class="glyphicon glyphicon-arrow-down" aria-hidden="true" ></span>
                                                                                        </asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Code">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="colmCode_Cate2" runat="server" Text='<%# Bind("Ric2_cd") %>' Width="50px"></asp:Label>
                                                                                    </ItemTemplate>

                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Description">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="colmDes_Cate2" runat="server" Text='<%# Bind("Ric2_desc") %>' Width="150px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Active">
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="colmStataus_Cate2" runat="server" Checked='<%#Convert.ToBoolean(Eval("Ric2_act")) %>' Width="5px" OnCheckedChanged="colmStataus_Cate2_Click" AutoPostBack="true" />
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
                                            <div class="col-sm-12">

                                                <div class="panel panel-default">
                                                    <%--  <div class="panel-heading paddingtopbottom0">Main Categorization</div>--%>
                                                    <div class="panel-body">
                                                        <div class="col-sm-4">
                                                            <div class="row">
                                                                <div class="panel panel-default">
                                                                    <div class="panel-heading paddingtopbottom0">Item Range 1</div>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-sm-2 labelText1">
                                                                        Code 
                                                                    </div>
                                                                    <div class="col-sm-5">
                                                                        <asp:TextBox runat="server" MaxLength="20" Style="text-transform: uppercase" ID="txtCate3" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtntCate3" runat="server" CausesValidation="false" OnClick="lbtntCate3_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-sm-2 labelText1">
                                                                        Active
                                                                    </div>
                                                                    <div class="col-sm-1">
                                                                        <asp:CheckBox ID="check_active_cat3" runat="server" Width="5px"></asp:CheckBox>
                                                                    </div>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-sm-2 labelText1">
                                                                        Description 
                                                                    </div>
                                                                    <div class="col-sm-9">
                                                                        <asp:TextBox runat="server" ID="txtCate3Des" CausesValidation="false" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnAddCate3" runat="server" CausesValidation="false" OnClick="lbtnAddCate3_Click">
                                                                    <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-3 labelText1 ">
                                                                    </div>

                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0 paddingLeft0">
                                                                        <asp:LinkButton ID="labtnSave3" runat="server" CausesValidation="false" OnClick="labtnSave3_Click">
                                                                    <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-sm-1 labelText1">
                                                                    </div>
                                                                    <div class="col-sm-3 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnClear3" runat="server" CausesValidation="false" OnClick="lbtnClear3_Click" OnClientClick="ClearConfirm()">
                                                                    <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-sm-3 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnDeletecat3" Visible="false" runat="server" CausesValidation="false" OnClick="lbtnDeletecat3_Click" OnClientClick="DeleteConfirm()">
                                                                    <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>Delete
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="col-sm-8">
                                                            <div class="row">
                                                                <div class="col-sm-12 ">
                                                                    <div class="panel-body  panelscollbar height85 paddingtopbottom0">
                                                                        <asp:Label ID="lbCate3no" runat="server" Text="No data found..." Visible="false"></asp:Label>
                                                                        <asp:GridView ID="grdCate3" ShowHeader="true" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                            EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="" Visible="false">
                                                                                    <HeaderTemplate>
                                                                                        <asp:CheckBox ID="allchk_reqitem" runat="server" Width="5px" onclick="CheckAllgrdCate3(this)"></asp:CheckBox>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chk_ReqItem_cat3" AutoPostBack="true" runat="server" Checked="false" Width="5px" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Add" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lbtnCat3Select" runat="server" CausesValidation="false" Width="5px" OnClick="lbtnCat3Select_Click">
                                                                        <span class="glyphicon glyphicon-arrow-down" aria-hidden="true" ></span>
                                                                                        </asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Code">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="colmCode_Cate3" runat="server" Text='<%# Bind("Ric3_cd") %>' Width="50px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Description">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="colmDes_Cate3" runat="server" Text='<%# Bind("Ric2_desc") %>' Width="150px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Active">
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="colmStataus_Cate3" runat="server" Checked='<%#Convert.ToBoolean(Eval("Ric2_act")) %>' Width="5px" OnCheckedChanged="colmStataus_Cate3_Click" AutoPostBack="true" />
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
                                            <div class="col-sm-12">

                                                <div class="panel panel-default">
                                                    <%-- <div class="panel-heading paddingtopbottom0">Main Categorization</div>--%>
                                                    <div class="panel-body">
                                                        <div class="col-sm-4">
                                                            <div class="row">
                                                                <div class="panel panel-default">
                                                                    <div class="panel-heading paddingtopbottom0">Item Range 2</div>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-sm-2 labelText1">
                                                                        Code 
                                                                    </div>
                                                                    <div class="col-sm-5">
                                                                        <asp:TextBox runat="server" MaxLength="20" Style="text-transform: uppercase" ID="txtCate4" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtCate4_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnCate4" runat="server" CausesValidation="false" OnClick="lbtnCate4_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-sm-2 labelText1">
                                                                        Active
                                                                    </div>
                                                                    <div class="col-sm-1">
                                                                        <asp:CheckBox ID="check_active_cat4" runat="server" Width="5px"></asp:CheckBox>
                                                                    </div>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-sm-2 labelText1">
                                                                        Description 
                                                                    </div>
                                                                    <div class="col-sm-9">
                                                                        <asp:TextBox runat="server" ID="txtCate4Des" CausesValidation="false" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnAddCate4" runat="server" CausesValidation="false" OnClick="lbtnAddCate4_Click">
                                                                    <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-3 labelText1 ">
                                                                    </div>

                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0 paddingLeft0">
                                                                        <asp:LinkButton ID="labtnSave4" runat="server" CausesValidation="false" OnClick="labtnSave4_Click">
                                                                    <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-sm-1 labelText1">
                                                                    </div>
                                                                    <div class="col-sm-3 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnClear4" runat="server" CausesValidation="false" OnClick="lbtnClear4_Click" OnClientClick="ClearConfirm()">
                                                                    <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-sm-3 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnDeletecat4" Visible="false" runat="server" CausesValidation="false" OnClick="lbtnDeletecat4_Click" OnClientClick="DeleteConfirm()">
                                                                    <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>Delete
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="col-sm-8">
                                                            <div class="row">
                                                                <div class="col-sm-12 ">
                                                                    <div class="panel-body  panelscollbar height85 paddingtopbottom0">
                                                                        <asp:Label ID="lbCate4no" runat="server" Text="No data found..." Visible="false"></asp:Label>
                                                                        <asp:GridView ID="grdCate4" ShowHeader="true" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                            EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="" Visible="false">
                                                                                    <HeaderTemplate>
                                                                                        <asp:CheckBox ID="allchk_reqitem" runat="server" Width="5px" onclick="CheckAllgrdCate4(this)"></asp:CheckBox>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chk_ReqItem_cat3" AutoPostBack="true" runat="server" Checked="false" Width="5px" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Add" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lbtnCat4Select" runat="server" CausesValidation="false" Width="5px" OnClick="lbtnCat4Select_Click">
                                                                        <span class="glyphicon glyphicon-arrow-down" aria-hidden="true" ></span>
                                                                                        </asp:LinkButton>
                                                                                    </ItemTemplate>

                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Code">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="colmCode_Cate4" runat="server" Text='<%# Bind("Ric4_cd") %>' Width="50px"></asp:Label>
                                                                                    </ItemTemplate>

                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Description">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="colmDes_Cate4" runat="server" Text='<%# Bind("Ric4_desc") %>' Width="150px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Active">
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="colmStataus_Cate4" runat="server" Checked='<%#Convert.ToBoolean(Eval("Ric4_act")) %>' Width="5px" AutoPostBack="true" />
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
                                                    <%--  <div class="panel-heading paddingtopbottom0">Main Categorization</div>--%>
                                                    <div class="panel-body">
                                                        <div class="col-sm-4 ">
                                                            <div class="row">
                                                                <div class="panel panel-default">
                                                                    <div class="panel-heading paddingtopbottom0">Item Range 3</div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-2 labelText1">
                                                                        Code 
                                                                    </div>
                                                                    <div class="col-sm-5">
                                                                        <asp:TextBox runat="server" MaxLength="20" ID="txtCate5" Style="text-transform: uppercase" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtCate5_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnCate5" runat="server" CausesValidation="false" OnClick="lbtnCate5_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-sm-2 labelText1">
                                                                        Active
                                                                    </div>
                                                                    <div class="col-sm-1">
                                                                        <asp:CheckBox ID="check_active_cat5" runat="server" Width="5px"></asp:CheckBox>
                                                                    </div>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-sm-2 labelText1">
                                                                        Description 
                                                                    </div>
                                                                    <div class="col-sm-9">
                                                                        <asp:TextBox runat="server" ID="txtCate5Des" CausesValidation="false" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnAddCate5" runat="server" CausesValidation="false" OnClick="lbtnAddCate5_Click">
                                                                    <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-3 labelText1 ">
                                                                    </div>

                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0 paddingLeft0">
                                                                        <asp:LinkButton ID="labtnSave5" runat="server" CausesValidation="false" OnClick="labtnSave5_Click">
                                                                    <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-sm-1 labelText1">
                                                                    </div>
                                                                    <div class="col-sm-3 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnClear5" runat="server" CausesValidation="false" OnClick="lbtnClear5_Click" OnClientClick="ClearConfirm()">
                                                                    <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-sm-3 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnDeletecat5" Visible="false" runat="server" CausesValidation="false" OnClick="lbtnDeletecat5_Click" OnClientClick="DeleteConfirm()">
                                                                    <span class="glyphicon glyphicon-trash"  aria-hidden="true"></span>Delete
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="col-sm-8">
                                                            <div class="row">
                                                                <div class="col-sm-12 ">
                                                                    <div class="panel-body  panelscollbar height85 paddingtopbottom0">
                                                                        <asp:Label ID="lbCate5no" runat="server" Text="No data found..." Visible="false"></asp:Label>
                                                                        <asp:GridView ID="grdCate5" ShowHeader="true" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                            EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label runat="server" Width="20px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Code">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="colmCode_Cate5" runat="server" Text='<%# Bind("Ric5_cd") %>' Width="50px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Description">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="colmDes_Cate5" runat="server" Text='<%# Bind("Ric5_desc") %>' Width="150px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Active">
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="colmStataus_Cate5" runat="server" Checked='<%#Convert.ToBoolean(Eval("Ric5_act")) %>' Width="5px" OnCheckedChanged="colmStataus_Cate5_Click" AutoPostBack="true" />
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
                            <div class="tab-pane" id="ItemModel">
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                                <asp:LinkButton ID="lblmodelgrid" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lblmodelgrid_Click">
                                                  <%--<span class="glyphicon glyphicon-edit" aria-hidden="true" style="font-size:20px"></span>--%>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">Model</div>
                                                    <div class="panel-body">
                                                        <div class="col-sm-4">
                                                            <div class="row">
                                                                <div class="col-sm-3 labelText1">
                                                                    Model 
                                                                </div>
                                                                <div class="col-sm-5">
                                                                    <asp:TextBox runat="server" ID="txtModel" Style="text-transform: uppercase" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtModel_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:LinkButton ID="lbtnSearchModel" runat="server" CausesValidation="false" OnClick="lbtnSearchModel_Click">
                                                                                 <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 height5">
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-3 labelText1">
                                                                    Description 
                                                                </div>
                                                                <div class="col-sm-9">
                                                                    <asp:TextBox runat="server" ID="txtModelDes" TextMode="MultiLine" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>

                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 height5">
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-3 labelText1">
                                                                    Main Category
                                                                </div>
                                                                <div class="col-sm-5">
                                                                    <asp:TextBox runat="server" ID="txtMainCat" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtMainCat_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:LinkButton ID="lbtnSrch_mainCat" runat="server" CausesValidation="false" OnClick="lbtnSrch_mainCat_Click">
                                                                                 <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-2 labelText1">
                                                                    Active
                                                                </div>
                                                                <div class="col-sm-1">
                                                                    <asp:CheckBox ID="chk_Active_model" runat="server" Width="5px"></asp:CheckBox>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="row">
                                                                <div class="col-sm-2 labelText1">
                                                                    Sub Category 1
                                                                </div>
                                                                <div class="col-sm-5">
                                                                    <asp:TextBox runat="server" ID="txtCat1" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtCat1_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:LinkButton ID="lbtnSrch_cat1" runat="server" CausesValidation="false" OnClick="lbtnSrch_cat1_Click">
                                                                                 <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 height5">
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-2 labelText1">
                                                                    Sub Category 2
                                                                </div>
                                                                <div class="col-sm-5">
                                                                    <asp:TextBox runat="server" ID="txtCat2" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtCat2_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:LinkButton ID="lbtnSrch_cat2" runat="server" CausesValidation="false" OnClick="lbtnSrch_cat2_Click">
                                                                                 <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 height5">
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-2 labelText1">
                                                                    Sub Category 3
                                                                </div>
                                                                <div class="col-sm-5">
                                                                    <asp:TextBox runat="server" ID="txtCat3" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtCat3_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:LinkButton ID="lbtnSrch_cat3" runat="server" CausesValidation="false" OnClick="lbtnSrch_cat3_Click">
                                                                                 <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 height5">
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-2 labelText1">
                                                                    Sub Category 4
                                                                </div>
                                                                <div class="col-sm-5">
                                                                    <asp:TextBox runat="server" ID="txtCat4" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtCat4_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:LinkButton ID="lbtnSrch_cat4" runat="server" CausesValidation="false" OnClick="lbtnSrch_cat4_Click">
                                                                                 <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:LinkButton ID="lbtnAddModel" runat="server" CausesValidation="false" OnClick="lbtnAddModel_Click">
                                                                    <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>

                                                            </div>
                                                        </div>


                                                        <div class="col-sm-2">
                                                            <div class="row">
                                                                <div class="col-sm-4 paddingLeft0 paddingRight0 buttonRow">
                                                                    <asp:LinkButton ID="lbtnSaveModel" runat="server" CausesValidation="false" OnClick="lbtnSaveModel_Click">
                                                                    <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-4 paddingLeft0 paddingRight0 buttonRow">
                                                                    <asp:LinkButton ID="lbtnClearModel" runat="server" CausesValidation="false" OnClick="lbtnClearModel_Click" OnClientClick="ClearConfirm()">
                                                                    <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-4 paddingLeft0 paddingRight0 buttonRow">
                                                                    <asp:LinkButton ID="lbtnmodelDelete" Visible="false" runat="server" CausesValidation="false" OnClick="lbtnmodelDelete_Click" OnClientClick="DeleteConfirm()">
                                                                    <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>Delete
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 ">
                                                <div class="panel-body  panelscollbar height300 paddingtopbottom0">
                                                    <asp:GridView ID="grdModel" ShowHeader="true" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                        EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="" Visible="false">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="allchk_reqitem" runat="server" Width="5px" onclick="CheckAllgrdmodel(this)"></asp:CheckBox>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chk_ReqItem_model" runat="server" Checked="false" Width="5px" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Code">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="colmMm_cd_model" runat="server" Text='<%# Bind("Mm_cd") %>' Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Description">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="colmMm_desc_model" runat="server" Text='<%# Bind("Mm_desc") %>' Width="300px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Main Category">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Cat_1" runat="server" Text='<%# Bind("Mm_cat1") %>' Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sub Category 1">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Cat_2" runat="server" Text='<%# Bind("Mm_cat2") %>' Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sub Category 2">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Cat_3" runat="server" Text='<%# Bind("Mm_cat3") %>' Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sub Category 3">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Cat_4" runat="server" Text='<%# Bind("Mm_cat4") %>' Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sub Category 4">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Cat_5" runat="server" Text='<%# Bind("Mm_cat5") %>' Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Active">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="colmMm_act" runat="server" Checked='<%#Convert.ToBoolean(Eval("Mm_act")) %>' Width="5px" OnCheckedChanged="colmMm_act_Click" AutoPostBack="true" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="tab-pane" id="ItemBrand">
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                                <asp:LinkButton ID="lbtnBrand" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lbtnBrand_Click">
                                                  <%--<span class="glyphicon glyphicon-edit" aria-hidden="true" style="font-size:20px"></span>--%>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">Brand</div>
                                                    <div class="panel-body">
                                                        <div class="col-sm-10">
                                                            <div class="row">
                                                                <div class="col-sm-3 labelText1">
                                                                    Brand Code
                                                                </div>
                                                                <div class="col-sm-5">
                                                                    <asp:TextBox runat="server" ID="txtBrand" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtBrand_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:LinkButton ID="lbtnSearchBrand" runat="server" CausesValidation="false" OnClick="lbtnSearchBrand_Click">
                                                                                 <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-2 labelText1">
                                                                    Active
                                                                </div>
                                                                <div class="col-sm-1">
                                                                    <asp:CheckBox ID="chk_Active_brand" runat="server" Width="5px"></asp:CheckBox>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 height5">
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-3 labelText1">
                                                                    Description
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <asp:TextBox runat="server" ID="txtBrandName" TextMode="MultiLine" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:LinkButton ID="lbtnAddBrand" runat="server" CausesValidation="false" OnClick="lbtnAddBrand_Click">
                                                                    <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>

                                                            </div>

                                                        </div>
                                                        <div class="col-sm-2">
                                                            <div class="row">
                                                                <div class="col-sm-4 paddingLeft0 paddingRight0 buttonRow">
                                                                    <asp:LinkButton ID="lbtnSaveBrand" runat="server" CausesValidation="false" OnClick="lbtnSaveBrand_Click">
                                                                    <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-4 paddingLeft0 paddingRight0 buttonRow">
                                                                    <asp:LinkButton ID="lbtnClearBrand" runat="server" CausesValidation="false" OnClick="lbtnClearBrand_Click" OnClientClick="ClearConfirm()">
                                                                    <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-4 paddingLeft0 paddingRight0 buttonRow">
                                                                    <asp:LinkButton ID="lbtnDeleteBrand" runat="server" Visible="false" CausesValidation="false" OnClick="lbtnDeleteBrand_Click" OnClientClick="DeleteConfirm()">
                                                                    <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>Delete
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 ">
                                                <div class="panel-body  panelscollbar height300 paddingtopbottom0">
                                                    <asp:GridView ID="grdBrand" ShowHeader="true" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                        EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="" Visible="false">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="allchk_reqitem" runat="server" Width="5px" onclick="CheckAllgrdBrand(this)"></asp:CheckBox>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chk_ReqItem_Brnad" AutoPostBack="true" runat="server" Checked="false" Width="5px" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Code">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="colmMb_cd_brand" runat="server" Text='<%# Bind("Mb_cd") %>' Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Description">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="colmMb_desc_model" runat="server" Text='<%# Bind("Mb_desc") %>' Width="300px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Active">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="colmMb_act" runat="server" Checked='<%#Convert.ToBoolean(Eval("Mb_act")) %>' Width="5px" OnCheckedChanged="colmMb_act_Click" AutoPostBack="true" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="tab-pane" id="ItemUOM">
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy5" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                                <asp:LinkButton ID="lbtnUOM" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lbtnUOM_Click">
                                                  <%--<span class="glyphicon glyphicon-edit" aria-hidden="true" style="font-size:20px"></span>--%>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">UOM</div>
                                                    <div class="panel-body">
                                                        <div class="col-sm-10">
                                                            <div class="row">
                                                                <div class="col-sm-2 labelText1">
                                                                    UOM
                                                                </div>
                                                                <div class="col-sm-5">
                                                                    <asp:TextBox runat="server" MaxLength="5" AutoPostBack="true" ID="txtUOM" CausesValidation="false" CssClass="form-control" OnTextChanged="txtUOM_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:LinkButton ID="lbtnSearchUOM" runat="server" CausesValidation="false" OnClick="lbtnSearchUOM_Click">
                                                                                 <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-2 labelText1">
                                                                    Active
                                                                </div>
                                                                <div class="col-sm-1">
                                                                    <asp:CheckBox ID="chk_Active_uom" runat="server" Width="5px"></asp:CheckBox>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 height5">
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-2 labelText1">
                                                                    Description
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <asp:TextBox runat="server" ID="txtUOMDes" TextMode="MultiLine" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:LinkButton ID="lbtnAddUOM" runat="server" CausesValidation="false" OnClick="lbtnAddUOM_Click">
                                                                    <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>

                                                            </div>

                                                        </div>
                                                        <div class="col-sm-2">
                                                            <div class="row">
                                                                <div class="col-sm-4 paddingLeft0 paddingRight0 buttonRow">
                                                                    <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="false" OnClick="lbtnSaveUOM_Click">
                                                                    <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-4 paddingLeft0 paddingRight0 buttonRow">
                                                                    <asp:LinkButton ID="lbtnClearUOM" runat="server" CausesValidation="false" OnClick="lbtnClearUOM_Click" OnClientClick="ClearConfirm()">
                                                                    <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-4 paddingLeft0 paddingRight0 buttonRow">
                                                                    <asp:LinkButton ID="lbtnDeleteUOM" runat="server" Visible="false" CausesValidation="false" OnClick="lbtnDeleteUOM_Click" OnClientClick="DeleteConfirm()">
                                                                    <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>Delete
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 ">
                                                <div class="panel-body  panelscollbar height300 paddingtopbottom0">
                                                    <asp:GridView ID="grdUOM" ShowHeader="true" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                        EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="" Visible="false">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="allchk_reqitem" runat="server" Width="5px" onclick="CheckAllgrdUOM(this)"></asp:CheckBox>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chk_ReqItem_UOM" AutoPostBack="true" runat="server" Checked="false" Width="5px" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="UOM">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="colmMsu_cd" runat="server" Text='<%# Bind("Msu_cd") %>' Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Description">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="colmMsu_desc" runat="server" Text='<%# Bind("Msu_desc") %>' Width="300px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Active">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="colmMsu_act" runat="server" Checked='<%#Convert.ToBoolean(Eval("Msu_act")) %>' Width="5px" OnCheckedChanged="colmMsu_act_Click" AutoPostBack="true" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="tab-pane" id="ItemColor">
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy6" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                                <asp:LinkButton ID="lbtnColor" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lbtnColor_Click">
                                                  <%--<span class="glyphicon glyphicon-edit" aria-hidden="true" style="font-size:20px"></span>--%>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">Color</div>
                                                    <div class="panel-body">
                                                        <div class="col-sm-10">
                                                            <div class="row">
                                                                <div class="col-sm-2 labelText1">
                                                                    Color
                                                                </div>
                                                                <div class="col-sm-5">
                                                                    <asp:TextBox runat="server" AutoPostBack="true" ID="txtColor" CausesValidation="false" CssClass="form-control" OnTextChanged="txtColor_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:LinkButton ID="lbtnSrhClr" runat="server" CausesValidation="false" OnClick="lbtnSrhClr_Click">
                                                                                 <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-1 labelText1">
                                                                    Active
                                                                </div>
                                                                <div class="col-sm-1">
                                                                    <asp:CheckBox ID="chk_act_color" runat="server" Width="5px"></asp:CheckBox>
                                                                </div>
                                                            </div>

                                                            <div class="row">
                                                                <div class="col-sm-12 height5">
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-2 labelText1">
                                                                    Description
                                                                </div>
                                                                <div class="col-sm-3">
                                                                    <asp:TextBox runat="server" ID="txtColorDes" TextMode="MultiLine" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:LinkButton ID="lbtnAddColor" runat="server" CausesValidation="false" OnClick="lbtnAddColor_Click">
                                                                    <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>

                                                            </div>

                                                        </div>
                                                        <div class="col-sm-2">
                                                            <div class="row">
                                                                <div class="col-sm-4 paddingLeft0 paddingRight0 buttonRow">
                                                                    <asp:LinkButton ID="lbtnSaveColor" runat="server" CausesValidation="false" OnClick="lbtnSaveColor_Click">
                                                                    <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-4 paddingLeft0 paddingRight0 buttonRow">
                                                                    <asp:LinkButton ID="lbtnClearColor" runat="server" CausesValidation="false" OnClick="lbtnClearColor_Click" OnClientClick="ClearConfirm()">
                                                                    <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-4 paddingLeft0 paddingRight0 buttonRow">
                                                                    <asp:LinkButton ID="lbtnDeleteColor" runat="server" Visible="false" CausesValidation="false" OnClick="lbtnDeleteColor_Click" OnClientClick="DeleteConfirm()">
                                                                    <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>Delete
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 ">
                                                <div class="panel-body  panelscollbar height300 paddingtopbottom0">
                                                    <asp:GridView ID="grdColor" ShowHeader="true" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                        EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="" Visible="false">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="allchk_reqitem" runat="server" Width="5px" onclick="CheckAllgrdColor(this)"></asp:CheckBox>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chk_ReqItem_Color" AutoPostBack="true" runat="server" Checked="false" Width="5px" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Code">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="colmClr_cd" runat="server" Text='<%# Bind("Clr_cd") %>' Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Description">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="colmClr_desc" runat="server" Text='<%# Bind("Clr_desc") %>' Width="300px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Active">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="colmClr_stus" runat="server" Checked='<%#Convert.ToBoolean(Eval("Clr_stus")) %>' Width="5px" OnCheckedChanged="colmClr_stus_Click" AutoPostBack="true" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <%--</div>--%>
                </div>
            </div>
        </div>
    </div>
    <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserPopoup" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="pnlpopup" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
        <ContentTemplate>
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

        </ContentTemplate>

    </asp:UpdatePanel>

    <script type="text/javascript">
        Sys.Application.add_load(fun);
        function fun() {
            $('#myTab a').click(function (e) {
                e.preventDefault();
                $(this).tab('show');
                //  alert($(this).attr('href'));
                document.getElementById('<%=hdfTabIndex.ClientID %>').value = $(this).attr('href');
            });

            $(document).ready(function () {
                var tab = document.getElementById('<%= hdfTabIndex.ClientID%>').value;
                // alert(tab);
                $('#myTab a[href="' + tab + '"]').tab('show');
            });
        }
    </script>
</asp:Content>
