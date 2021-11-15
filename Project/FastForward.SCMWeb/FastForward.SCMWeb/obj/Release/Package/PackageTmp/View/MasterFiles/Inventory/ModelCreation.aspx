<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="ModelCreation.aspx.cs" Inherits="FastForward.SCMWeb.View.MasterFiles.Inventory.ModelCreation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <!-- Messages -->
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
            var selectedvalue = confirm("Do you want to save data?");
            if (selectedvalue) {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "No";
            }
        };
        function DeleteConfirm() {
            var selectedvalue = confirm("Do you want to Delete?");
            if (selectedvalue) {
                document.getElementById('<%=txtdelete.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtdelete.ClientID %>').value = "No";
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
    <asp:HiddenField ID="txtClearlconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtSavelconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtdelete" runat="server" />
    <div class="panel panel-default marginLeftRight5">
        <div class="panel-body">
            <div class="col-md-12">
                <div class="col-md-3">
                    <h1 style="font-size: 11px; font-weight: bolder; margin-top: 1px; margin-left: 4px">Model Creation</h1>
                </div>
                <div class="col-md-3">
                    New Model
                 <asp:CheckBox ID="chkisnewitem" runat="server" Text=""></asp:CheckBox>
                </div>
            </div>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-default marginLeftRight5">
                                <div class="panel-body">
                                    <div class="row">

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
                                                    <asp:TextBox runat="server" ID="txtModelDes" CausesValidation="false" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtModelDes_TextChanged"></asp:TextBox>
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
                                                <div class="col-sm-3 labelText1">
                                                    Active
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:CheckBox ID="chk_Active_model" Checked="true" runat="server" Width="5px"></asp:CheckBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    UOM
                                                </div>
                                                <div class="col-sm-5">
                                                    <asp:DropDownList runat="server" ID="drpuom" CausesValidation="false" CssClass="form-control"></asp:DropDownList>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Brand
                                                </div>
                                                <div class="col-sm-5">
                                                    <asp:TextBox runat="server" ID="txtbrand" CausesValidation="false" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtbrand_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                    <asp:LinkButton ID="btn_brand" runat="server" CausesValidation="false" OnClick="btn_brand_Click">
                                                                                 <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 paddingRight0 labelText1">
                                                    Life span(Monthly)
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:TextBox runat="server" ID="txtLifSpan" CausesValidation="false" CssClass="diWMClick validateInt form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-3 paddingRight0 labelText1">
                                                    Country Of Orign
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:TextBox runat="server" ID="txtcountryoforign" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtcountryoforign_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                    <asp:LinkButton ID="lbtncountry" runat="server" CausesValidation="false" OnClick="lbtncountry_Click">
                                                                                 <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-3">
                                            <div class="row">
                                                <div class="col-sm-4 labelText1">
                                                    Category 1
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
                                                <%-- <div class="col-sm-12 height5">
                                                </div>--%>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4 labelText1">
                                                    Category 2
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
                                                <%--<div class="col-sm-12 height5">
                                                </div>--%>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4 labelText1">
                                                    Category 3
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
                                                <%-- <div class="col-sm-12 height5">
                                                </div>--%>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4 labelText1">
                                                    Category 4
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
                                                    <asp:LinkButton ID="lbtnAddModel" runat="server" CausesValidation="false" OnClick="lbtnAddModel_Click" Visible="false">
                                                                    <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>

                                            </div>



                                            <div class="row">
                                                <div class="col-sm-4 labelText1">
                                                    Discontinued 
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:CheckBox ID="chkDiscontinue" runat="server" Width="5px"></asp:CheckBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4 labelText1 ">
                                                    Discontinue Date
                                                </div>
                                                <div class="col-sm-4 paddingRight5 paddingLeft1">
                                                    <asp:TextBox runat="server" ReadOnly="true" ID="txtDiscontinuedDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnDiscontinue" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDiscontinuedDate"
                                                        PopupButtonID="lbtnDiscontinue" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4 labelText1 ">
                                                    Introduced Date
                                                </div>
                                                <div class="col-sm-4 paddingRight5 paddingLeft1">
                                                    <asp:TextBox runat="server" Enabled="false" ID="txtIntroDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnIntDt" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtIntroDate"
                                                        PopupButtonID="lbtnIntDt" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4 labelText1">
                                                    HS Code
                                                </div>
                                                <div class="col-sm-5">
                                                    <asp:TextBox runat="server" ID="txthscode" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txthscode_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                    <asp:LinkButton ID="lbtnhssearch" runat="server" CausesValidation="false" OnClick="lbtnhssearch_Click">
                                                                                 <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-3">
                                            <div class="row">
                                                <div class="col-sm-5 labelText1">
                                                    Company
                                                </div>
                                                <div class="col-sm-5">
                                                    <asp:TextBox runat="server" ID="txtuomcom" CausesValidation="false" CssClass="form-control" OnTextChanged="txtuomcom_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                    <asp:LinkButton ID="lbtnuomcom" runat="server" OnClick="lbtnuomcom_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-5 labelText1">
                                                    Convert  UOM
                                                </div>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList runat="server" ID="drpconuom" CausesValidation="false" CssClass="form-control"></asp:DropDownList>
                                                </div>

                                            </div>

                                            <div class="row">
                                                <div class="col-sm-5 labelText1">
                                                    Qty
                                                </div>
                                                <div class="col-sm-5">
                                                    <asp:TextBox runat="server" ID="txtqty" CausesValidation="false" CssClass="form-control" OnTextChanged="txtqty_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                    <asp:LinkButton runat="server" ID="btnuomadd" OnClick="btnuomadd_Click">  
<span class="glyphicon glyphicon-plus" aria-hidden="true"> </span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="panel-body  panelscollbar paddingtopbottom0" style="height: 70px">
                                                        <asp:GridView ID="grduomdata" ShowHeader="true" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                            EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Company">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="muomcom" runat="server" Text='<%# Bind("mmu_com") %>' Width="30px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Main UOM">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="muom" runat="server" Text='<%# Bind("mmu_model_uom") %>' Width="50px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Convert UOM">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="conuom" runat="server" Text='<%# Bind("mmu_con_uom") %>' Width="50px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Qty">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="qty" runat="server" Text='<%# Bind("mmu_qty") %>' Width="20px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnuomdelete" runat="server" OnClick="btnuomdelete_Click" Width="20px" OnClientClick="DeleteConfirm();"><span class="glyphicon glyphicon-remove"></span></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-sm-2">
                                            <div class="row">
                                                <div class="col-sm-4 paddingLeft0 paddingRight0 buttonRow">
                                                    <asp:LinkButton ID="lbtnSaveModel" runat="server" CausesValidation="false" OnClick="lbtnSaveModel_Click" OnClientClick="SaveConfirm()">
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


                                        <div class="row" style="display: none">
                                            <div class="col-sm-12 ">
                                                <div class="panel-body  panelscollbar height300 paddingtopbottom0">
                                                    <asp:GridView ID="grdModel" ShowHeader="true" Visible="false" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
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
                                                            <asp:TemplateField HeaderText="IsDiscontinued">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="colmMm_is_discontinue" runat="server" Checked='<%#Convert.ToBoolean(Eval("Mm_is_dis")) %>' Width="5px" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Discontinue Date">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="colmMm_discontinue_date" runat="server" Text='<%# Bind("Mm_dis_dt") %>' Width="5px" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Tax Strcture">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="colmm_tax_structure" runat="server" Text='<%# Bind("Mm_taxstruc_cd") %>' Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>




                                        <%--  <div class="row">
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
                                                            </div>--%>
                                    </div>



                                </div>
                            </div>






                            <%--<div class="panel-body">--%>

                            <!-- Replace Model -->
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="col-sm-12 paddingLeft0">
                                        <div class="panel panel-default">
                                            <div class="panel-heading paddingtopbottom0">
                                                Replace Models
                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-sm-5">
                                                        <div class="row">
                                                            <div class="col-sm-12 paddingLeft0">
                                                                <div class="col-sm-5 labelText1 ">
                                                                    Model
                                                                </div>
                                                                <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                    <asp:TextBox ID="txtrepModel" CausesValidation="false" runat="server" class="form-control" AutoPostBack="true" OnTextChanged="txtrepItem_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:LinkButton ID="lbtnSerchRep" runat="server" OnClick="lbtnSerchRep_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 paddingLeft0">
                                                                <div class="col-sm-5 labelText1 ">
                                                                    Description
                                                                </div>
                                                                <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                    <asp:TextBox ID="txtrepDes" TextMode="MultiLine" CausesValidation="false" runat="server" class="form-control" AutoPostBack="true" OnTextChanged="txtrepDes_TextChanged"></asp:TextBox>
                                                                </div>

                                                            </div>

                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 paddingLeft0">
                                                                <div class="col-sm-5 labelText1 ">
                                                                    Remark
                                                                </div>
                                                                <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                    <%-- <asp:DropDownList ID="ddlReplaceProduct2" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control">--%>
                                                                    <%-- <asp:ListItem Text="YES"></asp:ListItem>
                                                                        <asp:ListItem Text="NO"></asp:ListItem>--%>
                                                                    <%--                                                                            </asp:DropDownList>--%>

                                                                    <asp:DropDownList ID="ddlReplaceProduct" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control">
                                                                    </asp:DropDownList>


                                                                </div>

                                                            </div>

                                                        </div>

                                                        <div class="row">
                                                            <div class="col-sm-12 paddingLeft0">
                                                                <div class="col-sm-5 labelText1 ">
                                                                    Effective From
                                                                </div>
                                                                <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                    <asp:TextBox runat="server" ReadOnly="true" ID="txtEffectiveFrom" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0">
                                                                    <asp:LinkButton ID="lbtnEffective" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtEffectiveFrom"
                                                                        PopupButtonID="lbtnEffective" Format="dd/MMM/yyyy">
                                                                    </asp:CalendarExtender>
                                                                </div>


                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 paddingLeft0">
                                                                <div class="col-sm-5 labelText1 ">
                                                                    Active
                                                                </div>
                                                                <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                    <asp:DropDownList ID="ddlrepStatus" runat="server" class="form-control">
                                                                        <asp:ListItem Text="YES"></asp:ListItem>
                                                                        <asp:ListItem Text="NO"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:LinkButton ID="lbtnAddRepalced" runat="server" OnClick="lbtnAddRepalced_Click">
                                                            <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="col-sm-7">
                                                        <div class="panel-body panelscollbar height120">
                                                            <asp:GridView ID="grdRepalced" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                <Columns>

                                                                    <asp:TemplateField HeaderText="Model">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="rep_item" runat="server" Text='<%# Bind("Mrpl_model") %>' Width="100px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Replaced Model">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="rep_Desc" runat="server" Text='<%# Bind("Mrpl_replacedmodel") %>' Width="100px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Effective Date" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="rep_effective_date" runat="server" Text='<%# Bind("Mrpl_effect_dt","{0:dd/MM/yyyy}") %>' Width="100px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Active" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Rpl_active_status" runat="server" Text='<%# Bind("Mrpl_active") %>' Width="100px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Active">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Rpl_active_status" runat="server" Text='<%# Bind("Mrpl_active_status") %>' Width="100px"></asp:Label>
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
                                <!-- Row End-->




                                <div class="col-sm-6 paddingLeft0">
                                    <div class="col-sm-12 paddingLeft0">
                                        <div class="panel panel-default">
                                            <div class="panel-heading paddingtopbottom0">
                                                Company parameter

                                            </div>
                                            <div class="panel-body">
                                                <div class="col-sm-5 paddingLeft0">
                                                    <div class="row">
                                                        <div class="col-sm-12 paddingLeft0">
                                                            <div class="col-sm-6 labelText1 ">
                                                                Company
                                                            </div>
                                                            <div class="col-sm-3 paddingRight5 paddingLeft0">
                                                                <asp:TextBox ID="txtModelCom" AutoPostBack="true" CausesValidation="false" runat="server" class="form-control" OnTextChanged="txtItemCom_TextChanged"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                <asp:LinkButton ID="lbtnSearchModelCom" runat="server" OnClick="lbtnSearchitemCom_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="col-sm-2 paddingLeft0 paddingRight0">
                                                                <asp:LinkButton ID="lbtnMultipleCom" runat="server" OnClick="lbtnMultipleCom_Click" Visible="false">
                                                         Multiple
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12 paddingLeft0">
                                                            <div class="col-sm-6 labelText1 ">
                                                                Description
                                                            </div>
                                                            <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                <asp:TextBox ID="txtModelComDes" CausesValidation="false" runat="server" class="form-control" AutoPostBack="true" OnTextChanged="txtModelComDes_TextChanged"></asp:TextBox>
                                                            </div>

                                                        </div>

                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12 paddingLeft0">
                                                            <div class="col-sm-6 labelText1 ">
                                                                FOC Allow
                                                            </div>
                                                            <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                <asp:DropDownList ID="ddlFoc" runat="server" class="form-control">
                                                                    <asp:ListItem Text="Allow"></asp:ListItem>
                                                                    <asp:ListItem Text="Not Allow"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                        </div>

                                                    </div>
                                                    <asp:Panel runat="server" ID="pnpagency" Visible="false">
                                                        <div class="row">
                                                            <div class="col-sm-12 paddingLeft0">
                                                                <div class="col-sm-6 labelText1 ">
                                                                    Agency Type
                                                                </div>
                                                                <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                    <asp:DropDownList ID="ddlAgecType" runat="server" class="form-control">
                                                                        <asp:ListItem Text="N/A"></asp:ListItem>
                                                                        <asp:ListItem Text="Sole"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>

                                                            </div>

                                                        </div>
                                                    </asp:Panel>

                                                    <div class="row">
                                                        <div class="col-sm-12 paddingLeft0">
                                                            <div class="col-sm-6 labelText1 ">
                                                                prohibited Sales Type
                                                            </div>
                                                            <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                <asp:DropDownList ID="ddlhpSalesAccept" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control">
                                                                    <%-- <asp:ListItem Text="YES"></asp:ListItem>
                                                                        <asp:ListItem Text="NO"></asp:ListItem>--%>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12 paddingLeft0">
                                                            <div class="col-sm-6 labelText1 ">
                                                                Active
                                                            </div>
                                                            <div class="col-sm-5 paddingRight5 paddingLeft0">
                                                                <asp:DropDownList ID="ddlitemStatus" runat="server" class="form-control">
                                                                    <asp:ListItem Text="YES"></asp:ListItem>
                                                                    <asp:ListItem Text="NO"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                <asp:LinkButton ID="lbtnsearchComItemAdd" runat="server" OnClick="lbtnsearchComModelAdd_Click">
                                                            <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="col-sm-7">
                                                    <div class="panel-body panelscollbar height100">
                                                        <asp:GridView ID="grdComModel" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                            EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Company">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="ci_com" runat="server" Text='<%# Bind("Mcm_com") %>' Width="40px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Description">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="ci_des" runat="server" Text='<%# Bind("Mcm_com_desc") %>' Width="100px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="FOC" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="ci_foc" runat="server" Text='<%# Bind("Mcm_isfoc") %>' Width="40px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="FOC">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Mci_isfoc_status" runat="server" Text='<%# Bind("Mcm_isfoc_status") %>' Width="40px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="P.S.Type">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="ci_Msi_restric_inv_tp" runat="server" Text='<%# Bind("Mcm_restric_inv_tp") %>' Width="40px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Status" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="ci_status" runat="server" Text='<%# Bind("Mcm_act") %>' Width="40px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Active">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Mci_act_status" runat="server" Text='<%# Bind("Mcm_act_status") %>' Width="40px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btcomdelete" runat="server" OnClick="btcomdelete_Click" Width="20px" OnClientClick="DeleteConfirm();"><span class="glyphicon glyphicon-remove"></span></asp:LinkButton>
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


                            <div class="row">
                                <div class="col-sm-5">
                                    <div class="col-sm-3 labelText1 paddingLeft5">
                                        Tax Structure
                                    </div>
                                    <div class="col-sm-4 paddingRight5 paddingLeft0">
                                        <asp:TextBox ID="txtTaxStucture" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control" OnTextChanged="txtTaxStucture_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                        <asp:LinkButton ID="lbtnsrcTax" runat="server" OnClick="lbtnsrcTax_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-2 paddingLeft0 paddingRight0">
                                        <asp:LinkButton ID="lbtntxD" runat="server" Text="View Details" OnClick="lbtntxD_Click">
                                                           
                                        </asp:LinkButton>
                                    </div>




                                </div>
                            </div>



                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="col-sm-12 paddingLeft0">
                                        <div class="panel panel-default">
                                            <div class="panel-heading paddingtopbottom0">
                                                Model Segmentation
                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-sm-4">
                                                        <div class="row">
                                                            <div class="col-sm-12 paddingLeft0">
                                                                <div class="col-sm-5 labelText1 ">
                                                                    Company
                                                                </div>
                                                                <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                    <asp:TextBox ID="txtModelSegemntCompany" CausesValidation="false" runat="server" class="form-control" OnTextChanged="txtModelSegemntCompany_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lbtnSerchCom_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>

                                                        </div>

                                                    </div>
                                                    <div class="col-sm-7">
                                                        <div class="panel-body panelscollbar" style="height: 80px">
                                                            <asp:GridView ID="grdModelSegmentation" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="_Mandatory" AutoPostBack="true" runat="server" OnCheckedChanged="_Mandatory_CheckedChanged" Enabled="true" Checked='<%#Convert.ToBoolean(Eval("isMandatory")) %>' Width="30px"></asp:CheckBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Type">
                                                                        <ItemTemplate>
                                                                            <%-- rename labels --%>
                                                                            <asp:Label ID="rbt_tp" runat="server" Text='<%# Bind("TypeCD_") %>' Width="30px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Value(LKR)">
                                                                        <ItemTemplate>
                                                                            <%-- rename labels --%>
                                                                            <asp:Label ID="rbt_desc" runat="server" Text='<%# Bind("TypeDesctipt") %>' Width="30px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="ddlAgeVals" CssClass="form-control" runat="server" Width="80px"></asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-1">
                                                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false" OnClick="lbtnAddModelSegment_Click">
                                                                    <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>

                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-11">
                                                        <div class="col-sm-12 paddingLeft0">
                                                            <div class="panel panel-default">
                                                                <%--<div class="panel-heading paddingtopbottom0">
                                                Model Segmentation
                                            </div>--%>
                                                                <div class="panel-body">
                                                                    <div class="row">

                                                                        <div class="col-sm-12">
                                                                            <div class="panel-body panelscollbar" style="height: 80px">
                                                                                <asp:GridView ID="grdBusinessEntity" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                                    EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                                    <Columns>
                                                                                        <%--  <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="_Mandatory" AutoPostBack="true" runat="server" OnCheckedChanged="_Mandatory_CheckedChanged" Enabled="true" Checked='<%#Convert.ToBoolean(Eval("isMandatory")) %>' Width="100px"></asp:CheckBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>

                                                                                        <asp:TemplateField HeaderText="Company">
                                                                                            <ItemTemplate>
                                                                                                <%-- rename labels --%>
                                                                                                <asp:Label ID="rbt_tp" runat="server" Text='<%# Bind("rbv_com") %>' Width="100px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Type">
                                                                                            <ItemTemplate>
                                                                                                <%-- rename labels --%>
                                                                                                <asp:Label ID="rbt_tp" runat="server" Text='<%# Bind("rbv_tp") %>' Width="100px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Value(LKR)">
                                                                                            <ItemTemplate>
                                                                                                <%-- rename labels --%>
                                                                                                <asp:Label ID="rbt_desc" runat="server" Text='<%# Bind("rbv_val") %>' Width="100px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <%--  <ItemTemplate>
                                                                <asp:DropDownList ID="ddlAgeVals" CssClass="form-control" runat="server" Width="100px"></asp:DropDownList>
                                                            </ItemTemplate>--%>
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
                                    </div>




                                </div>
                            </div>
                            <!-- Row End-->






                        </div>

                    </div>



                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>


    <%-- <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>--%>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
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



    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="ModelSegPopup" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="segPnlpopup" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:Panel runat="server" ID="segPnlpopup" DefaultButton="lbtnSearch">
        <div runat="server" id="Div1" class="panel panel-default height400 width700">
            <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="LinkButton3" runat="server" OnClick="btnClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <%--<span>Commen Search</span>--%>
                    <div class="col-sm-11">
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12" id="Div2" runat="server">
                        <div class="row">
                            <div class="col-sm-12">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height5">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12" id="Div3" runat="server">
                                <div class="col-sm-2 labelText1">
                                    Search by key
                                </div>
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <div class="col-sm-3 paddingRight5">
                                            <asp:DropDownList ID="ddlSearchbykey2" runat="server" class="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="col-sm-2 labelText1">
                                    Search by word
                                </div>
                                <div class="col-sm-4 paddingRight5">

                                    <asp:TextBox ID="txtSearchbyword2" CausesValidation="false" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbyword2_TextChanged"></asp:TextBox>

                                </div>





                                <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnSearch2" runat="server" OnClick="lbtnSearch2_Click">
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
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy5" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdResult2" CausesValidation="false" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResult2_SelectedIndexChanged" OnPageIndexChanging="grdResult2_PageIndexChanging">
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



    <%-- <asp:ScriptManagerProxy ID="ScriptManagerProxy11" runat="server"></asp:ScriptManagerProxy>--%>
    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button7" runat="server" Text="Button" Style="display: none;" />
            <%-- <asp:ModalPopupExtender ID="taxDetailspopup" runat="server" Enabled="True" TargetControlID="Button7"
                PopupControlID="pnltaxdetails" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>--%>

            <asp:ModalPopupExtender ID="taxDetailspopup" runat="server" Enabled="True" TargetControlID="Button7"
                PopupControlID="pnltaxdetails" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnltaxdetails">
        <div runat="server" id="Div11" class="panel panel-default height300 width950">
            <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="LinkButton4" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>

                </div>
                <div class="panel-body">
                    <div class="col-sm-12" id="Div12" runat="server">
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
                                <div class="panel-body panelscollbar height300">
                                    <asp:GridView ID="grdTax" ShowHeader="true" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                        EmptyDataText="No data found..." AutoGenerateColumns="False">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Company">
                                                <ItemTemplate>
                                                    <asp:Label ID="colmCode" runat="server" Text='<%# Bind("ITS_COM") %>' Width="80px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="colmDes" runat="server" Visible="false" Text='<%# Bind("ITS_STUS") %>' Width="80px"></asp:Label>
                                                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("Its_stus_Des") %>' Width="80px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tax Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="colTaxtype" runat="server" Text='<%# Bind("ITS_TAX_CD") %>' Width="80px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tax Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="coltaxrate" runat="server" Text='<%# Bind("ITS_TAX_RATE") %>' Width="80px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Active">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="colmStataus" Enabled="false" runat="server" Checked='<%#Convert.ToBoolean(Eval("ITS_ACT")) %>' Width="5px" />
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


    </asp:Panel>



    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="MultipleCom" runat="server" Enabled="True" TargetControlID="Button2"
                PopupControlID="pnlMultiple" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>
    <asp:Panel runat="server" ID="pnlMultiple" DefaultButton="lbtnSearch">
        <div runat="server" id="div_multiple" class="panel panel-default height200 width700 panelscollbar">
            <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="LinkButton5" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>

                    <div class="col-sm-11">
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12" id="Div6" runat="server">
                        <div class="row">
                            <div class="col-sm-12">
                            </div>
                        </div>
                        <div class="row">
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="panel-body ">

                                    <asp:CheckBoxList runat="server" ID="chklstbox"
                                        RepeatColumns="5"
                                        RepeatDirection="Vertical"
                                        RepeatLayout="Table" Width="500"
                                        TextAlign="Right"
                                        ForeColor="#333"
                                        Font-Bold="false">
                                    </asp:CheckBoxList>


                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <script type="text/javascript">
        Sys.Application.add_load(fun);
        function fun() {
            $('.validateDecimal').keypress(function (evt) {
                // var ch = (evt.which) ? evt.which : evt.keyCode;
                var ch = evt.which;
                var str = $(this).val();
                // console.log(ch);
                if (ch == 46) {
                    if (str.indexOf(".") == -1) {
                        return true;
                    } else {
                        return false;
                    }
                }
                else if ((ch == 8) || (ch == 9) || (ch == 46) || (ch == 0)) {
                    return true;
                }
                else if (ch > 47 && ch < 58) {
                    return true;
                }
                else {
                    return false;
                }
            });
            $('.diWMClick').mousedown(function (e) {
                if (e.button == 2) {
                    alert('This functionality is disabled !');
                    return false;
                } else {
                    return true;
                }
            });
            $('.validateInt').keypress(function (evt) {
                // var ch = (evt.which) ? evt.which : evt.keyCode;
                var charCode = evt.which;
                var str = $(this).val();
                //console.log(charCode);
                if ((charCode == 8) || (charCode == 9) || (charCode == 0) || (charCode == 13)) {
                    return true;
                }
                else if (str.length < 4) {
                    if (charCode > 47 && charCode < 58) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else {
                    $(this).value = str.substr(0, 4);
                    //alert(charCode);
                    alert('Maximum 4 characters are allowed ');
                    return false;
                }
            });
            $('#BodyContent_txtrepModel').on('keypress', function (event) {
                var regex = new RegExp("^[~!@$%^&*]+$");
                var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
                if (regex.test(key)) {
                    event.preventDefault();
                    return false;
                }
            });

            $('#BodyContent_txtModelDes,#BodyContent_txtrepDes,#BodyContent_txtModelComDes').on('keypress', function (event) {
                var regex = new RegExp("^[~!@$%^&*]+$");
                var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
                if (regex.test(key)) {
                    event.preventDefault();
                    return false;
                }
            });
        }
    </script>
</asp:Content>
