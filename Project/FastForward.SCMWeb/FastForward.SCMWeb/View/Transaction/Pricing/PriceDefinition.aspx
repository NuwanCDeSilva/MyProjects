<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="PriceDefinition.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Pricing.PriceDefinition" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script type="text/javascript">
        function Enable() {
            return;
        }
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
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
              && (charCode < 48 || charCode > 57))
                return false;

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
        function ApprovalConfirm() {
            var selectedvalue = confirm("Do you want to approval data?");
            if (selectedvalue) {
                document.getElementById('<%=txtApprovalconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtApprovalconformmessageValue.ClientID %>').value = "No";
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
        function AmendConfirm() {

            var selectedvalue = confirm("Do you want to amend data?");
            if (selectedvalue) {
                document.getElementById('<%=txtconfirmAmend.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconfirmAmend.ClientID %>').value = "No";
            }
        };
        function ConfirmClearForm() {
            var selectedvalueOrd = confirm("Do you want to clear ?");
            if (selectedvalueOrd) {
                document.getElementById('<%=txtconfirmclear.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconfirmclear.ClientID %>').value = "No";
            }
        };
        function ConfirmClear() {
            var selectedvalueOrd = confirm("Do you want to clear all details ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfirmDelete() {
            var selectedvaldelitm = confirm("Do you want to remove ?");
            if (selectedvaldelitm) {
                document.getElementById('<%=txtconfirmdelete.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconfirmdelete.ClientID %>').value = "No";
            }
        };
        function ConfirmCancel() {
            var selectedvaldelitm = confirm("Do you want to canel ?");
            if (selectedvaldelitm) {
                document.getElementById('<%=txtconfirmcancel.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconfirmcancel.ClientID %>').value = "No";
            }
        };
        function ConfirmPlaceOrder() {
            var selectedvalueOrdPlace = confirm("Do you want to save ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=txtconfirmplaceord.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconfirmplaceord.ClientID %>').value = "No";
            }
        };

        function Enable() {
            return;
        }

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

    <div class="panel panel-default marginLeftRight5 height525">
        <div class="panel-body">

            <div class="panel panel-default height500">
                <asp:HiddenField ID="txtconfirmAmend" runat="server" />
                <asp:HiddenField ID="txtconfirmclear" runat="server" />
                <asp:HiddenField ID="txtconfirmplaceord" runat="server" />
                <asp:HiddenField ID="txtconfirmdelete" runat="server" />
                <asp:HiddenField ID="txtApprovalconformmessageValue" runat="server" />
                <asp:HiddenField ID="hdfTabIndex" runat="server" />
                <asp:HiddenField ID="txtconfirmcancel" runat="server" />
                <asp:HiddenField ID="txtSavelconformmessageValue" runat="server" />
                <ul id="myTab" class="nav nav-tabs">

                    <li class="active">
                        <a href="#RoleCreation" data-toggle="tab">Price Book & Level Maintain</a>
                    </li>

                    <li>
                        <a href="#GrantPrivileges" data-toggle="tab">Assign Profit Center</a>
                    </li>

                    <li onclick="document.getElementById('<%= btnHiddenClear.ClientID %>').click();">
                        <a href="#NewPriceCreation" data-toggle="tab">Create New Price</a>
                    </li>

                </ul>

                <div id="myTabContent" class="tab-content">

                    <div class="tab-pane fade in active" id="RoleCreation">
                        <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                            <ContentTemplate>

                                <div class="col-sm-4 buttonRow PDButtons">

                                    <div class="col-sm-3 paddingRight0">
                                    </div>

                                    <div class="col-sm-3 paddingRight0">
                                    </div>

                                    <div class="col-sm-3 paddingRight0">

                                        <asp:LinkButton ID="lbtnsave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmPlaceOrder();" OnClick="lbtnsave_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true" style="font-size:20px"></span>Save Book
                                        </asp:LinkButton>

                                    </div>

                                    <div class="col-sm-3 paddingRight0">

                                        <asp:LinkButton ID="lbtnclear" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmClearForm();"
                                            OnClick="lbtnclear_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true" style="font-size:20px"></span>Clear Book
                                        </asp:LinkButton>

                                    </div>

                                </div>

                                <div class="col-sm-12">

                                    <div class="panel panel-default">

                                        <div class="panel-heading">
                                            Price Book Creation
                                        </div>

                                        <div class="panel-body">

                                            <div>

                                                <div class="row">

                                                    <div class="col-sm-12">

                                                        <div class="col-sm-12">

                                                            <div class="panel panel-default">
                                                                <div class="panel-heading">
                                                                </div>
                                                                <div class="panel-body">

                                                                    <div class="col-sm-3">

                                                                        <div class="row">

                                                                            <div class="col-sm-3 labelText1">
                                                                                Company
                                                                            </div>

                                                                            <div class="col-sm-8 paddingRight5">
                                                                                <asp:TextBox ID="txtcomcode" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                                            </div>

                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                <asp:LinkButton ID="lbtncomcode" runat="server" TabIndex="1" OnClick="lbtncomcode_Click" Visible="false">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </div>

                                                                        </div>

                                                                    </div>

                                                                    <div class="col-sm-3">

                                                                        <div class="row">

                                                                            <div class="col-sm-3 labelText1">
                                                                                Price Book
                                                                            </div>

                                                                            <div class="col-sm-8 paddingRight5">
                                                                                <asp:TextBox ID="txtprbuk" Style="text-transform: uppercase;" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            </div>

                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                <asp:LinkButton ID="lbtnpb" runat="server" TabIndex="2" OnClick="lbtnpb_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </div>

                                                                        </div>

                                                                    </div>

                                                                    <div class="col-sm-3">

                                                                        <div class="row">

                                                                            <div class="col-sm-3 labelText1">
                                                                            </div>

                                                                            <div class="col-sm-9 paddingRight5">
                                                                                <asp:TextBox ID="txtpb2" runat="server" TabIndex="3" CssClass="form-control"></asp:TextBox>
                                                                            </div>

                                                                        </div>

                                                                    </div>

                                                                    <div class="col-sm-3">

                                                                        <div class="row">

                                                                            <div class="col-sm-3 labelText1">
                                                                                Active
                                                                            </div>

                                                                            <div class="col-sm-3 paddingRight5">
                                                                                <asp:CheckBox ID="chkactive" runat="server" AutoPostBack="true" TabIndex="5" />
                                                                            </div>

                                                                            <div class="col-sm-4 labelText1">
                                                                                Search Books
                                                                            </div>

                                                                            <div class="col-sm-1 ">
                                                                                <asp:LinkButton ID="lbtnsearchpb" runat="server" OnClick="lbtnsearchpb_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true" style="font-size:20px"></span>
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

                                                                            <div class="col-sm-12">

                                                                                <div class="panel panel-default">

                                                                                    <div class="panel-heading pannelheading ">
                                                                                    </div>

                                                                                    <div class="panel-body">

                                                                                        <div class="row">
                                                                                            <div class="col-sm-12">

                                                                                                <div class="panelscoll">

                                                                                                    <asp:GridView ID="gvbook" AutoGenerateColumns="false" runat="server" OnRowDataBound="gvbook_RowDataBound" OnSelectedIndexChanged="gvbook_SelectedIndexChanged" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">

                                                                                                        <Columns>
                                                                                                            <asp:CommandField ShowSelectButton="true" ButtonType="Link" />

                                                                                                            <asp:TemplateField HeaderText="Code">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblcode" runat="server" Text='<%# Bind("sapb_pb") %>' Width="150px"></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>

                                                                                                            <asp:TemplateField HeaderText="Description">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lbldesc" runat="server" Text='<%# Bind("sapb_desc") %>' Width="200px"></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>

                                                                                                            <asp:TemplateField HeaderText="Company" Visible="false">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblcompany" runat="server" Text='<%# Bind("sapb_com") %>' Width="200px"></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>

                                                                                                            <asp:TemplateField HeaderText="Active" Visible="false">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblact" runat="server" Text='<%# Bind("sapb_act") %>' Width="200px"></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>

                                                                                                            <asp:TemplateField HeaderText="Status">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblstustext" runat="server" Text='<%# Bind("statustext") %>' Width="200px"></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>

                                                                                                        </Columns>
                                                                                                        <SelectedRowStyle BackColor="Silver" />
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

                                            </div>

                                        </div>

                                    </div>

                                </div>

                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>

                                <div class="col-sm-4 buttonRow PDButtons">

                                    <div class="col-sm-3 paddingRight0">
                                    </div>

                                    <div class="col-sm-3 paddingRight0">
                                    </div>

                                    <div class="col-sm-3 paddingRight0">

                                        <asp:LinkButton ID="lbtnsave2" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmPlaceOrder();" OnClick="lbtnsave2_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true" style="font-size:20px"></span>Save Level
                                        </asp:LinkButton>

                                    </div>

                                    <div class="col-sm-3 paddingRight0">

                                        <asp:LinkButton ID="lbtnclear2" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmClearForm();" OnClick="lbtnclear2_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true" style="font-size:20px"></span>Clear Level
                                        </asp:LinkButton>

                                    </div>

                                </div>

                                <div class="col-sm-12">

                                    <div class="panel panel-default">

                                        <div class="panel-heading">
                                            Price Level Creation
                                        </div>

                                        <div class="panel-body">

                                            <div>

                                                <div class="row">

                                                    <div class="col-sm-12">

                                                        <div class="col-sm-12">

                                                            <div class="panel panel-default">
                                                                <div class="panel-heading">
                                                                </div>
                                                                <div class="panel-body">

                                                                    <div class="col-sm-2">

                                                                        <div class="row">

                                                                            <div class="col-sm-3 labelText1">
                                                                                Company
                                                                            </div>

                                                                            <div class="col-sm-8 paddingRight5">
                                                                                <asp:TextBox ID="txtcomcodde2" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                                            </div>

                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                <asp:LinkButton ID="lbtncomcode2" runat="server" TabIndex="5" OnClick="lbtncomcode2_Click" Visible="false">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </div>


                                                                        </div>

                                                                    </div>

                                                                    <div class="col-sm-2">

                                                                        <div class="row">

                                                                            <div class="col-sm-3 labelText1">
                                                                                Book
                                                                            </div>

                                                                            <div class="col-sm-8 paddingRight5">
                                                                                <asp:TextBox ID="txtpb3" runat="server" CssClass="form-control label-info"></asp:TextBox>
                                                                            </div>

                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                <asp:LinkButton ID="lbtnfindpb2" runat="server" TabIndex="6" OnClick="lbtnfindpb2_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </div>


                                                                        </div>

                                                                    </div>

                                                                    <div class="col-sm-1">

                                                                        <div class="row">

                                                                            <div class="col-sm-12 paddingLeft0">
                                                                                <asp:TextBox ID="txtpb4" TabIndex="7" runat="server" CssClass="form-control label-info"></asp:TextBox>
                                                                            </div>

                                                                        </div>

                                                                    </div>

                                                                    <div class="col-sm-2">

                                                                        <div class="row">

                                                                            <div class="col-sm-3 labelText1">
                                                                                Level
                                                                            </div>

                                                                            <div class="col-sm-7 paddingRight5">
                                                                                <asp:TextBox ID="txtpricelevel" runat="server" CssClass="form-control label-success"></asp:TextBox>
                                                                            </div>

                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                <asp:LinkButton ID="lbtnpriclvl" runat="server" TabIndex="8" OnClick="lbtnpriclvl_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </div>


                                                                        </div>

                                                                    </div>

                                                                    <div class="col-sm-1">

                                                                        <div class="row">

                                                                            <div class="col-sm-4 labelText1">
                                                                                <strong>All</strong>
                                                                            </div>

                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                <asp:CheckBox runat="server" ID="chlallchecked" AutoPostBack="true" TabIndex="9" />
                                                                            </div>

                                                                        </div>

                                                                    </div>

                                                                    <div class="col-sm-1 PBActChkBox">

                                                                        <div class="row">

                                                                            <div class="col-sm-6 labelText1">
                                                                                <strong>Active</strong>
                                                                            </div>

                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                <asp:CheckBox runat="server" ID="chkactivlvl" AutoPostBack="true" TabIndex="10" />
                                                                            </div>

                                                                        </div>

                                                                    </div>

                                                                    <div class="col-sm-2">

                                                                        <div class="row">

                                                                            <div class="col-sm-12 paddingRight5">
                                                                                <asp:TextBox ID="txtpricelevel3" runat="server" TabIndex="11" CssClass="form-control label-success"></asp:TextBox>
                                                                            </div>

                                                                        </div>

                                                                    </div>

                                                                    <div class="col-sm-1">

                                                                        <div class="row">

                                                                            <div class="col-sm-10 labelText1">
                                                                                For Sales
                                                                            </div>

                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                <asp:CheckBox runat="server" ID="chkforsales" AutoPostBack="true" TabIndex="12" />
                                                                            </div>

                                                                        </div>

                                                                    </div>

                                                                    <div class="row">
                                                                        <div class="col-sm-12 height5">
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-sm-2">

                                                                        <div class="row">

                                                                            <div class="col-sm-3 labelText1">
                                                                                Item Status
                                                                            </div>

                                                                            <div class="col-sm-8 paddingRight5">
                                                                                <asp:DropDownList ID="ddlstatus" runat="server" TabIndex="13" AutoPostBack="true" CssClass="form-control label-warning"></asp:DropDownList>
                                                                            </div>


                                                                        </div>

                                                                    </div>

                                                                    <div class="col-sm-1">

                                                                        <div class="row">

                                                                            <div class="col-sm-10 labelText1 label-warning">
                                                                                Check Status
                                                                            </div>

                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                <asp:CheckBox runat="server" ID="chkcheckstus" AutoPostBack="true" TabIndex="14" />
                                                                            </div>

                                                                        </div>

                                                                    </div>

                                                                    <div class="col-sm-2">

                                                                        <div class="row">

                                                                            <div class="col-sm-3 labelText1">
                                                                                Currency
                                                                            </div>

                                                                            <div class="col-sm-8 paddingRight5">
                                                                                <asp:TextBox ID="txtcurr" runat="server" CssClass="form-control label-primary" BackColor="Pink"></asp:TextBox>
                                                                            </div>

                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                <asp:LinkButton ID="LinkButton1" runat="server" TabIndex="15" OnClick="LinkButton1_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </div>


                                                                        </div>

                                                                    </div>

                                                                    <div class="col-sm-1">

                                                                        <div class="row">

                                                                            <div class="col-sm-10 labelText1" style="background-color: pink">
                                                                                With VAT
                                                                            </div>

                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                <asp:CheckBox runat="server" ID="chkvat" AutoPostBack="true" TabIndex="16" />
                                                                            </div>

                                                                        </div>

                                                                    </div>

                                                                    <div class="col-sm-1">

                                                                        <div class="row">

                                                                            <div class="col-sm-10 labelText1" style="background-color: tan">
                                                                                Warranty Base
                                                                            </div>

                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                <asp:CheckBox runat="server" ID="chkwarr" AutoPostBack="true" TabIndex="17" />
                                                                            </div>

                                                                        </div>

                                                                    </div>

                                                                    <div class="col-sm-2">

                                                                        <div class="row">

                                                                            <div class="col-sm-6 labelText1">
                                                                                Period (Month)
                                                                            </div>

                                                                            <div class="col-sm-6 paddingRight5">
                                                                                <asp:TextBox ID="txtperiod" runat="server" BackColor="Tan" TabIndex="18" CssClass="form-control" onkeydown="return jsDecimals(event);"></asp:TextBox>
                                                                            </div>

                                                                        </div>

                                                                    </div>

                                                                    <div class="col-sm-1">

                                                                        <div class="row">

                                                                            <div class="col-sm-10 labelText1">
                                                                                Serialized
                                                                            </div>

                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                <asp:CheckBox runat="server" ID="chkser" AutoPostBack="true" TabIndex="19" />
                                                                            </div>

                                                                        </div>

                                                                    </div>

                                                                    <div class="col-sm-1">

                                                                        <div class="row">

                                                                            <div class="col-sm-10 labelText1">
                                                                                Based on Tot Sales Qty
                                                                            </div>

                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                <asp:CheckBox runat="server" ID="chktotsales" AutoPostBack="true" TabIndex="20" />
                                                                            </div>

                                                                        </div>

                                                                    </div>

                                                                    <div class="col-sm-1">

                                                                        <div class="row">

                                                                            <div class="col-sm-10 labelText1">
                                                                                Transfer
                                                                            </div>

                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                <asp:CheckBox runat="server" ID="chktransfer" AutoPostBack="true" TabIndex="21" />
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

                                                                    <div class="col-sm-3">

                                                                        <div class="row">

                                                                            <div class="col-sm-3 labelText1">
                                                                                Aging Price
                                                                            </div>

                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                <asp:CheckBox runat="server" ID="chkaging" AutoPostBack="true" TabIndex="22" />
                                                                            </div>

                                                                        </div>

                                                                    </div>

                                                                    <div class="col-sm-3">

                                                                        <div class="row">

                                                                            <div class="col-sm-3 labelText1">
                                                                                Batch Wise
                                                                            </div>

                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                <asp:CheckBox runat="server" ID="chkbatchwise" AutoPostBack="true" TabIndex="23" />
                                                                            </div>

                                                                        </div>

                                                                    </div>

                                                                    <div class="col-sm-3">

                                                                        <div class="row">

                                                                            <div class="col-sm-3 labelText1">
                                                                                Level Active
                                                                            </div>

                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                <asp:CheckBox runat="server" ID="chklvlactive" AutoPostBack="true" TabIndex="24" />
                                                                            </div>

                                                                        </div>

                                                                    </div>

                                                                    <div class="col-sm-3">

                                                                        <div class="row">

                                                                            <div class="col-sm-3 labelText1">
                                                                                Model Base
                                                                            </div>

                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                <asp:CheckBox runat="server" ID="chkModelBase" AutoPostBack="true" TabIndex="24" />
                                                                            </div>

                                                                        </div>

                                                                    </div>

                                                                    <div class="col-sm-3">

                                                                        <div class="row">

                                                                            <div class="col-sm-12 labelText1">
                                                                                <asp:Label ID="lblinfo" runat="server" Font-Bold="true" CssClass="labelText1" Text="All prices attached here will have above conditions."></asp:Label>
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
                    </div>

                    <div class="tab-pane fade" id="GrantPrivileges">
                        <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>

                                <div class="col-sm-4 buttonRow PDButtonsPC">

                                    <div class="col-sm-3 paddingRight0">

                                        <asp:LinkButton ID="btnMSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmPlaceOrder();" OnClick="btnMSave_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true" style="font-size:20px"></span>Save
                                        </asp:LinkButton>

                                    </div>

                                    <div class="col-sm-3 paddingRight0">

                                        <asp:LinkButton ID="lbtnClearProfitCenter" CausesValidation="false" runat="server" CssClass="floatRight"
                                            OnClientClick="return ConfirmClear();" OnClick="lbtnClearProfitCenter_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true" style="font-size:20px"></span>Clear
                                        </asp:LinkButton>

                                    </div>

                                </div>

                                <div class="col-sm-12">

                                    <div class="panel panel-default">

                                        <div class="panel-heading">
                                            :: Assign to profit centers ::
                                        </div>

                                        <div class="panel-body">

                                            <div>

                                                <div class="row">

                                                    <div class="col-sm-12">

                                                        <div class="col-sm-4">

                                                            <div class="panel panel-default">
                                                                <div class="panel-heading">
                                                                    Main Parameters
                                                                </div>

                                                                <div class="panel-body">

                                                                    <div class="col-sm-12">

                                                                        <div class="row">

                                                                            <div class="col-sm-3 labelText1">
                                                                                Price Book
                                                                            </div>

                                                                            <div class="col-sm-8 paddingRight5">
                                                                                <asp:TextBox ID="txtMBook" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                                            </div>

                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                <asp:LinkButton ID="lbtnpbsearchpc" runat="server" TabIndex="1" OnClick="lbtnpbsearchpc_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </div>


                                                                        </div>

                                                                    </div>

                                                                    <div class="col-sm-12">

                                                                        <div class="row">

                                                                            <div class="col-sm-3 labelText1">
                                                                                Price Level
                                                                            </div>

                                                                            <div class="col-sm-8 paddingRight5">
                                                                                <asp:TextBox ID="txtMLevel" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                                            </div>

                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                <asp:LinkButton ID="lbtnprlvlpc" runat="server" TabIndex="2" OnClick="lbtnprlvlpc_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </div>


                                                                        </div>

                                                                    </div>

                                                                    <div class="col-sm-12">

                                                                        <div class="row">

                                                                            <div class="col-sm-3 labelText1">
                                                                                Profit Center
                                                                            </div>

                                                                            <div class="col-sm-8 paddingRight5">
                                                                                <asp:TextBox ID="txtMPc" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                                            </div>

                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                <asp:LinkButton ID="lbtnpc" runat="server" TabIndex="3" OnClick="lbtnpc_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </div>


                                                                        </div>

                                                                    </div>

                                                                    <div class="col-sm-12">

                                                                        <div class="row">

                                                                            <div class="col-sm-3 labelText1">
                                                                                Invoice Type
                                                                            </div>

                                                                            <div class="col-sm-8 paddingRight5">
                                                                                <asp:TextBox ID="txtMInvType" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            </div>

                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                <asp:LinkButton ID="lbtninv" runat="server" TabIndex="4" OnClick="lbtninv_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </div>


                                                                        </div>

                                                                    </div>

                                                                    <div class="col-sm-12">

                                                                        <div class="row">

                                                                            <div class="col-sm-12">

                                                                                <asp:Button ID="btnload" Text="Load" runat="server" TabIndex="5" OnClick="btnload_Click" CssClass="btn btn-default" />

                                                                            </div>


                                                                        </div>

                                                                    </div>

                                                                </div>

                                                            </div>

                                                        </div>

                                                        <div class="col-sm-5">

                                                            <div class="panel panel-default">

                                                                <div class="panel-heading">
                                                                    Applicable Profit Center
                                                                </div>
                                                                <div class="panel-body">

                                                                    <div class="col-sm-7">

                                                                        <div class="panel panel-default">

                                                                            <div class="panel-heading">
                                                                            </div>

                                                                            <div class="panel-body">

                                                                                <div class="col-sm-12">

                                                                                    <div class="row">

                                                                                        <div class="col-sm-4 labelText1">
                                                                                            Channel
                                                                                        </div>

                                                                                        <div class="col-sm-7 paddingRight5">
                                                                                            <asp:TextBox ID="txtmChannel" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                        </div>

                                                                                        <div class="col-sm-1 paddingLeft0">
                                                                                            <asp:LinkButton ID="lbtnfindchannel" runat="server" TabIndex="6" OnClick="lbtnfindchannel_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>


                                                                                    </div>

                                                                                </div>

                                                                                <div class="col-sm-12">

                                                                                    <div class="row">

                                                                                        <div class="col-sm-4 labelText1">
                                                                                            Sub Channel
                                                                                        </div>

                                                                                        <div class="col-sm-7 paddingRight5">
                                                                                            <asp:TextBox ID="txtmSubChannel" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                        </div>

                                                                                        <div class="col-sm-1 paddingLeft0">
                                                                                            <asp:LinkButton ID="lbtnsubchannel" runat="server" TabIndex="7" OnClick="lbtnsubchannel_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>


                                                                                    </div>

                                                                                </div>

                                                                                <div class="col-sm-12">

                                                                                    <div class="row">

                                                                                        <div class="col-sm-4 labelText1">
                                                                                            Profit Center
                                                                                        </div>

                                                                                        <div class="col-sm-7 paddingRight5">
                                                                                            <asp:TextBox ID="txtMAppPc" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                        </div>

                                                                                        <div class="col-sm-1 paddingLeft0">
                                                                                            <asp:LinkButton ID="lbtnpcfind" runat="server" TabIndex="8" OnClick="lbtnpcfind_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>


                                                                                    </div>

                                                                                </div>


                                                                            </div>

                                                                        </div>

                                                                    </div>

                                                                    <div class="col-sm-5">

                                                                        <div class="panel panel-default">

                                                                            <div class="panel-heading">
                                                                            </div>
                                                                            <div class="panel-body panelscoll">

                                                                                <asp:CheckBoxList runat="server" ID="chklstbox" CssClass="media-list"></asp:CheckBoxList>

                                                                            </div>

                                                                        </div>

                                                                    </div>

                                                                    <div class="col-sm-12">

                                                                        <div class="row">

                                                                            <div class="col-sm-3">
                                                                                <asp:Button ID="Button5" Text=">>" runat="server" TabIndex="9" CssClass="btn btn-default" OnClick="Button5_Click" />
                                                                            </div>

                                                                            <div class="col-sm-3">
                                                                                <asp:Button ID="Button1" Text="Select All" runat="server" TabIndex="10" CssClass="btn btn-default" OnClick="Button1_Click" />
                                                                            </div>

                                                                            <div class="col-sm-3">
                                                                                <asp:Button ID="Button2" Text="Unselect" runat="server" TabIndex="11" CssClass="btn btn-default" OnClick="Button2_Click" />
                                                                            </div>

                                                                            <div class="col-sm-3">
                                                                                <asp:Button ID="Button4" Text="Clear" runat="server" TabIndex="12" CssClass="btn btn-default" OnClick="Button4_Click" />
                                                                            </div>

                                                                        </div>

                                                                    </div>

                                                                </div>

                                                            </div>

                                                        </div>

                                                        <div class="col-sm-3">

                                                            <div class="panel panel-default">

                                                                <div class="panel-heading">
                                                                    Other Parameters
                                                                </div>

                                                                <div class="panel-body">
                                                                    <div class="col-sm-12">

                                                                        <div class="row" runat="server" visible="false">

                                                                            <div class="col-sm-7 labelText1">
                                                                                Default Discount Applicable
                                                                            </div>

                                                                            <div class="col-sm-5 paddingRight5">
                                                                                <asp:CheckBox ID="chkDefDis" runat="server" TabIndex="13" AutoPostBack="true" OnCheckedChanged="chkDefDis_CheckedChanged" />
                                                                            </div>


                                                                        </div>

                                                                        <div class="row" runat="server" visible="false">

                                                                            <div class="col-sm-6 labelText1">
                                                                                Discount Rate
                                                                            </div>

                                                                            <div class="col-sm-6 paddingRight5">
                                                                                <asp:TextBox ID="txtDisRate" runat="server" CssClass="form-control" onkeydown="return jsDecimals(event);" TabIndex="14" MaxLength="25" Style="text-align: right"></asp:TextBox>
                                                                            </div>


                                                                        </div>

                                                                        <div class="row">

                                                                            <div class="col-sm-7 labelText1">
                                                                                Check Credit
                                                                            </div>

                                                                            <div class="col-sm-5 paddingRight5">
                                                                                <asp:CheckBox ID="chkChkCredit" runat="server" TabIndex="14" />
                                                                            </div>


                                                                        </div>

                                                                        <div class="row">

                                                                            <div class="col-sm-6 labelText1">
                                                                                Prefix
                                                                            </div>

                                                                            <div class="col-sm-6 paddingRight5">
                                                                                <asp:TextBox ID="txtPrefix" runat="server" MaxLength="10" CssClass="form-control" TabIndex="15"></asp:TextBox>
                                                                            </div>


                                                                        </div>

                                                                        <div class="row">

                                                                            <div class="col-sm-7 labelText1">
                                                                                Set Default
                                                                            </div>

                                                                            <div class="col-sm-5 paddingRight5">
                                                                                <asp:CheckBox ID="chkSetDefault" runat="server" AutoPostBack="true" TabIndex="16" OnCheckedChanged="chkSetDefault_CheckedChanged" />
                                                                            </div>


                                                                        </div>

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

                                                        <div class="col-sm-11">

                                                            <div class="panel panel-default">
                                                                <div class="panel-heading">
                                                                    :: Define Details ::
                                                                </div>

                                                                <div class="panel-body">

                                                                    <div class="row">
                                                                        <div class="col-sm-12">

                                                                            <div class="panelscoll">

                                                                                <asp:GridView ID="dgvMDef" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">

                                                                                    <Columns>

                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="lbtngrdInvoiceDetailsDalete" runat="server" CausesValidation="false" OnClientClick="ConfirmDelete();" OnClick="lbtngrdInvoiceDetailsDalete_Click">
                                                                                                <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                                                                                </asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Company">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblcompany" runat="server" Text='<%# Bind("sadd_com") %>' Width="100px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Profit Center">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblpc" runat="server" Text='<%# Bind("sadd_pc") %>' Width="100px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Inv.Type">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblinvtype" runat="server" Text='<%# Bind("sadd_doc_tp") %>' Width="100px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Price Book">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblprbook" runat="server" Text='<%# Bind("sadd_pb") %>' Width="100px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Level">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbllevel" runat="server" Text='<%# Bind("sadd_p_lvl") %>' Width="100px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Dis.App">
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="chkdisapp" runat="server" Checked='<%#Convert.ToBoolean(Eval("sadd_is_disc")) %>' />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Dis.Rate">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbldisrate" runat="server" Text='<%#Eval("sadd_disc_rt","{0:n}")%>' Width="75px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="1%" HorizontalAlign="Center" />
                                                                                            <HeaderStyle HorizontalAlign="Right" />
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Check Credit">
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="chkcheckcredit" runat="server" Checked='<%#Convert.ToBoolean(Eval("sadd_chk_credit_bal")) %>' />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Print Prefix">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblprintprefix" runat="server" Text='<%# Bind("sadd_prefix") %>' Width="100px"></asp:Label>
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

                                                        <div class="col-sm-1">
                                                            <asp:LinkButton ID="lbtnapply" CausesValidation="false" runat="server" TabIndex="60" OnClick="lbtnapply_Click">
                                                        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                            </asp:LinkButton>
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

                    <div class="tab-pane fade" id="NewPriceCreation">
                        <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-5  buttonrow">
                                        </div>
                                        <div class="col-sm-7 buttonRow">
                                            <div class="col-sm-6">
                                            </div>
                                            <div class="col-sm-2  paddingLeft0  ">
                                                <asp:LinkButton ID="btnPCreate" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmPlaceOrder();" OnClick="btnPCreate_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                                </asp:LinkButton>
                                            </div>
                                            <div class="col-sm-2  paddingRight0 ">
                                                <asp:LinkButton ID="btnPApprove" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ApprovalConfirm();" OnClick="LinkButton2_Click">
                                        <span class="glyphicon glyphicon-thumbs-up" aria-hidden="true" ></span>Approve
                                                </asp:LinkButton>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0 paddingRight0 ">

                                                <asp:LinkButton ID="btnPClear" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmClearForm();" OnClick="btnPClear_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true" ></span>Clear
                                                </asp:LinkButton>
                                            </div>
                                            <div class="col-sm-1 paddingRight0">
                                                <div class="dropdown">
                                                    <a id="dLabel" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
                                                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                                                    </a>
                                                    <div class="dropdown-menu menupopup" aria-labelledby="dLabel">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-12  ">
                                                                    <asp:LinkButton ID="lbtnamend" CausesValidation="false" runat="server" OnClientClick="AmendConfirm();" OnClick="lbtnamend_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true" ></span>Amend
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-12  ">
                                                                    <asp:LinkButton ID="lbtnsaveas" CausesValidation="false" runat="server" OnClientClick="SaveConfirm();" OnClick="lbtnsaveas_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true" ></span>Save As
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-12  ">
                                                                    <asp:LinkButton ID="lbtnExcelUpload" runat="server" OnClick="lbtnExcelUpload_Click">
                                        <span class="glyphicon glyphicon-upload" aria-hidden="true"></span>Excel Upload
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-12  ">
                                                                    <asp:LinkButton ID="lbtncancel" CausesValidation="false" runat="server" OnClientClick="ConfirmCancel();" OnClick="lbtncancel_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true" ></span>Cancel
                                                                    </asp:LinkButton><asp:LinkButton ID="btnHiddenClear" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="btnHiddenClear_Click" Visible="true">
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-sm-12">

                                            <div class="panel panel-default">

                                                <div class="panel-heading">

                                                    <div class="row">
                                                        <div class="col-sm-7">
                                                            :: Create New Price ::
                                                        </div>
                                                        <div class="col-sm-1">
                                                            Status:
                                                        </div>
                                                        <div class="col-sm-3 paddingLeft0">
                                                            <asp:Label ID="lblPAStatus" runat="server" ForeColor="Red"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="panel-body">

                                                    <div class="row">


                                                        <div class="col-sm-6">
                                                            <div class="col-sm-6">

                                                                <div class="row">
                                                                    <div class="col-sm-3 labelText1">
                                                                        Price Book
                                                                    </div>
                                                                    <div class="col-sm-8 paddingRight5">
                                                                        <asp:TextBox ID="txtPPriceBook" Style="text-transform: uppercase;" runat="server" CssClass="form-control" OnTextChanged="txtPPriceBook_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                    </div>

                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="btnSearchPPriceBook" runat="server" TabIndex="1" OnClick="btnSearchPPriceBook_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-sm-3 labelText1">
                                                                        Price Level
                                                                    </div>
                                                                    <div class="col-sm-8 paddingRight5">
                                                                        <asp:TextBox ID="txtPPriceLevel" Style="text-transform: uppercase;" runat="server" CssClass="form-control" OnTextChanged="txtPPriceLevel_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                    </div>

                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="btnSearchPPriceLevel" runat="server" TabIndex="1" OnClick="btnSearchPPriceLevel_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-3 labelText1">
                                                                        From date
                                                                    </div>
                                                                    <div class="col-sm-8 paddingRight5">
                                                                        <asp:TextBox runat="server" ReadOnly="true" ID="txtPFromDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="lbtnPFromDate" runat="server" CausesValidation="false">
                                                                                    <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtPFromDate"
                                                                            PopupButtonID="lbtnPFromDate" Format="dd/MMM/yyyy">
                                                                        </asp:CalendarExtender>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-3 labelText1">
                                                                        To date
                                                                    </div>
                                                                    <div class="col-sm-8 paddingRight5">
                                                                        <asp:TextBox runat="server" Enabled="false" ID="txtPToDate" CausesValidation="false" CssClass="form-control" Text="31/Dec/2999"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="lbtnPToDate" runat="server" CausesValidation="false">
                                                                                    <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtPToDate"
                                                                            PopupButtonID="lbtnPToDate" Format="dd/MMM/yyyy" Enabled="false">
                                                                        </asp:CalendarExtender>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-4 labelText1">
                                                                        change End Date
                                                                    </div>

                                                                    <div class="col-sm-8 paddingRight5">
                                                                        <asp:CheckBox ID="chkPEndDt" runat="server" AutoPostBack="true" OnCheckedChanged="chkPEndDt_CheckedChanged" />
                                                                    </div>
                                                                </div>
                                                                <asp:Panel runat="server" ID="pnlmulti">
                                                                    <div class="row">
                                                                        <div class="col-sm-4 labelText1">
                                                                            Qty Multiplier
                                                                        </div>

                                                                        <div class="col-sm-8 paddingRight5">
                                                                            <asp:CheckBox ID="chkPMulti" runat="server" AutoPostBack="true" />
                                                                        </div>
                                                                    </div>
                                                                </asp:Panel>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <div class="row">
                                                                    <div class="col-sm-5 labelText1">
                                                                        Approval pending 
                                                                    </div>

                                                                    <div class="col-sm-5 paddingRight5">
                                                                        <asp:CheckBox ID="chkAppPen" runat="server" />
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-4 labelText1">
                                                                        Promotion #
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight5">
                                                                        <asp:TextBox ID="txtPPromoCd" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtPPromoCd_TextChanged"></asp:TextBox>
                                                                    </div>

                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="lbtnPPromoCd" runat="server" TabIndex="1" OnClick="lbtnPPromoCd_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-sm-4 labelText1">
                                                                        Circular # 
                                                                    </div>
                                                                    <div class="col-sm-6 paddingRight5">
                                                                        <asp:TextBox ID="txtPcir" Style="text-transform: uppercase;" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>

                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="lbtnPCir" runat="server" TabIndex="1" OnClick="lbtnPCir_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="lbtnLoadDet" runat="server" TabIndex="1" OnClick="lbtnLoadDet_Click">
                                                                                <span class="glyphicon glyphicon-arrow-right" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-4 labelText1">
                                                                        Category 
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight5">
                                                                        <asp:DropDownList ID="ddlPCat" class="form-control" runat="server">
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-4 labelText1">
                                                                        Type 
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight5">
                                                                        <asp:DropDownList ID="ddlPType" class="form-control" runat="server" AutoPostBack="true" OnTextChanged="ddlPType_TextChanged" OnSelectedIndexChanged="ddlPType_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-4 labelText1">
                                                                        Customer 
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight5">
                                                                        <asp:TextBox ID="txtPCus" runat="server" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtCustomer_TextChanged"></asp:TextBox>
                                                                    </div>

                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="lbtnPCus" runat="server" TabIndex="1" OnClick="lbtnCustomer_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>


                                                        <div class="col-sm-6">
                                                            <div class="panel panel-default">

                                                                <div class="panel-heading paddingtopbottom0">Promotions details</div>
                                                                <div class="panel-body height100">

                                                                    <div class="row">
                                                                        <div class="col-sm-12">
                                                                            <div class="panel-body ">

                                                                                <asp:CheckBoxList runat="server" ID="chkDetList"
                                                                                    RepeatColumns="5"
                                                                                    RepeatDirection="Vertical"
                                                                                    RepeatLayout="Table" Width="500"
                                                                                    TextAlign="Right"
                                                                                    ForeColor="#333"
                                                                                    Font-Bold="true">
                                                                                </asp:CheckBoxList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 labelText1">
                                                                    <%--                                  <div class="col-sm-6">
                                                                                <asp:Label ID="lblN1" runat="server" CssClass="form-control" Text="Promotions details">
                                                                                </asp:Label>


                                                                            </div>--%>
                                                                    <div class="col-sm-2">
                                                                        <asp:LinkButton ID="lbtnView" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lbtnView_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span> View </asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-sm-2">
                                                                        <asp:LinkButton ID="lbnSelectAll" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lbnSelectAll_Click">
                                                                                            <span class="glyphicon glyphicon-check" aria-hidden="true"></span> Select All</asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-sm-2">
                                                                        <asp:LinkButton ID="lblUnSelect" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lblUnSelect_Click">
                                                                                            <span class="glyphicon glyphicon-unchecked" aria-hidden="true"></span> Unselect All</asp:LinkButton>
                                                                    </div>
                                                                </div>

                                                            </div>

                                                        </div>

                                                    </div>
                                                    <%-- <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="row">
                                                        <div class="col-sm-1 labelText1">
                                                            Upload File Path :
                                                        </div>
                                                        <div class="col-sm-5 paddingRight5">
                                                            <asp:TextBox ID="txtPUploadFile" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-6 paddingRight0">
                                                            <div class="col-sm-3 labelText1">
                                                                Combine Promotion
                                                            </div>


                                                            <asp:Button ID="btnBrows" Text="Select" runat="server" CssClass="btn btn-default" />
                                                            <asp:Button ID="btnUpload" Text="Upload" runat="server" CssClass="btn btn-default" />
                                                        </div>

                                                    </div>

                                                </div>
                                            </div>--%>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>

                                        <div class="col-sm-12">

                                            <div class="panel panel-default">

                                                <div class="panel-heading">
                                                    :: Price Details ::
                                                </div>
                                                <div class="panel-body">


                                                    <div class="row">

                                                        <div class="col-sm-3">
                                                            <div class="col-sm-3 labelText1">
                                                                Item code
                                                            </div>
                                                            <div class="col-sm-7 paddingRight5">
                                                                <asp:TextBox ID="txtpItem" runat="server"  TabIndex="100" Style="text-transform: uppercase;" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtpItem_TextChanged"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft0">
                                                                <asp:LinkButton ID="lbtnPItm" runat="server" TabIndex="1" OnClick="btnSearch_Item_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-2">
                                                            <div class="col-sm-4 labelText1">
                                                                Qty From
                                                            </div>
                                                            <div class="col-sm-7 paddingRight5">
                                                                <asp:TextBox ID="txtpQtyFrom" runat="server" TabIndex="101" onkeypress="return isNumberKey(event)"  CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-2">
                                                            <div class="col-sm-3 labelText1">
                                                                Price
                                                            </div>
                                                            <div class="col-sm-7 paddingRight5">
                                                                <asp:TextBox ID="txtpPrice" runat="server" TabIndex="102" onkeypress="return isNumberKey(event)" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-5">
                                                            <div class="col-sm-3 labelText1">
                                                                Warranty Remarks
                                                            </div>
                                                            <div class="col-sm-7 paddingRight5">
                                                                <asp:TextBox ID="txtPWaraRmk" runat="server" TabIndex="103" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row">

                                                        <div class="col-sm-3">
                                                            <div class="col-sm-3 labelText1">
                                                                Model No
                                                            </div>
                                                            <div class="col-sm-7 paddingRight5">
                                                                <asp:TextBox ID="txtpModel" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft0">
                                                                <asp:LinkButton ID="LinkButton3" runat="server" TabIndex="1">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-2">
                                                            <div class="col-sm-4 labelText1">
                                                                Qty To
                                                            </div>
                                                            <div class="col-sm-7 paddingRight5">
                                                                <asp:TextBox ID="txtPQtyTo" runat="server" TabIndex="104" onkeypress="return isNumberKey(event)" CssClass="form-control" ></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-2">
                                                            <div class="col-sm-3 labelText1">
                                                                Times
                                                            </div>
                                                            <div class="col-sm-7 paddingRight5">
                                                                <asp:TextBox ID="txtpTimes" runat="server" TabIndex="105" CssClass="form-control" ></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-4">
                                                            <div class="col-sm-3 labelText1">
                                                                Inactive price
                                                            </div>
                                                            <div class="col-sm-7 paddingRight5">
                                                                <asp:CheckBox ID="chkpInactive" runat="server" TabIndex="106"  />
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-1">
                                                            <div class="col-sm-7 paddingRight5">
                                                                <asp:LinkButton ID="lbtnAddPrice" CausesValidation="false" TabIndex="107" runat="server" OnClick="lbtnAddPrice_Click">
                                                        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <div class="panel-body panelscollbar height100">
                                                                <asp:GridView ID="grdPriceDet" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                    EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                    <Columns>
                                                                        <asp:TemplateField Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtnRemove" runat="server" CausesValidation="false" Width="10px" OnClientClick="ConfirmDelete();" OnClick="lbtnRemove_Click">
                                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true" ></span>
                                                                                </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Pick" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtnAddCombine" runat="server" CausesValidation="false" Width="5px" OnClick="lbtnAddCombine_Click">
                                                                        <span class="glyphicon glyphicon-download" aria-hidden="true" ></span>
                                                                                </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Item">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="sapd_itm_cd" runat="server" Text='<%# Bind("sapd_itm_cd") %>' Width="75px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Model">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="sapd_model" runat="server" Text='<%# Bind("sapd_model") %>' Width="60px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="From Qty">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="sapd_qty_from" runat="server" Text='<%# Bind("sapd_qty_from") %>' Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="To Qty">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="sapd_qty_to" runat="server" Text='<%# Bind("sapd_qty_to") %>' Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Price">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="sapd_itm_price" runat="server" Text='<%# Bind("sapd_itm_price") %>' Width="75px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Times">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="sapd_no_of_times" runat="server" Text='<%# Bind("sapd_no_of_times") %>' Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Warranty Remarks">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="sapd_warr_remarks" runat="server" Text='<%# Bind("sapd_warr_remarks") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Inactive">
                                                                            <ItemTemplate>
                                                                                <%--<asp:Label ID="sapd_is_cancel" runat="server" Text='<%# Bind("sapd_is_cancel") %>' Width="10px"></asp:Label>--%>
                                                                                <asp:CheckBox ID="sapd_is_cancel" runat="server" Width="10px" Checked='<%#Convert.ToBoolean(Eval("sapd_is_cancel")) %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Avg. Cost">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="sapd_avg_cost" runat="server" Text='<%# Bind("sapd_avg_cost") %>' Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Latest Cost">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="sapd_lst_cost" runat="server" Text='<%# Bind("sapd_lst_cost") %>' Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Margin">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="sapd_margin" runat="server" Text='<%# Bind("sapd_margin","{0:N}") %>' Width="50px"></asp:Label>
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
                                        <div class="col-sm-12">
                                            <asp:Button ID="btnAppPC" Text="Applicable Profit centers" runat="server" CssClass="btn btn-default" OnClick="btnAppPC_Click" />
                                            <asp:Button ID="btnAddRest" Text="Restrictions" runat="server" CssClass="btn btn-default" OnClick="btnAddRest_Click" />
                                            <%--<asp:Button ID="btnAddCom" Text="Add Combine Items" runat="server" CssClass="btn btn-default" OnClick="btnAddCom_Click" />--%>
                                        </div>
                                        <asp:Panel runat="server" ID="pnlcombin">
                                            <div class="col-sm-4">
                                                <div class="col-sm-8 labelText1">
                                                    Confirm to continue without combine / free items
                                                </div>
                                                <div class="col-sm-1 paddingRight5">
                                                    <asp:CheckBox ID="chkWithOutCombine" runat="server" AutoPostBack="true" />
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel runat="server" ID="pnlpro">
                                            <div class="col-sm-1 labelText1 paddingLeft0 paddingRight0">
                                                Combine Promotion
                                            </div>
                                            <div class="col-sm-1 paddingRight5">
                                                <asp:CheckBox ID="chkComPromo" runat="server" AutoPostBack="true" />
                                            </div>
                                        </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>


                    </div>
                </div>

            </div>

        </div>
    </div>



    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnRes" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupRes" runat="server" Enabled="True" TargetControlID="btnRes"
                PopupControlID="pnllRes" CancelControlID="lbtnCloseRest" PopupDragHandleControlID="Div2" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:Panel runat="server" ID="pnllRes" DefaultButton="lbtnSearch">
        <div runat="server" id="Div2" class="panel panel-primary" style="width: 600px; height: 250px; padding: 0px;">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-sm-10">
                            :: Price Restriction ::
                        </div>
                        <div class="col-sm-1">
                            <asp:LinkButton ID="lbtnCloseRest" runat="server">
                                       <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-2">
                        Need Customer
                    </div>
                    <div class="col-sm-4 paddingRight5">
                        <asp:CheckBox ID="chkNeedCus" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-2">
                        Need NIC
                    </div>
                    <div class="col-sm-4 paddingRight5">
                        <asp:CheckBox ID="chkNeedNIC" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-2">
                        Need Mobile
                    </div>
                    <div class="col-sm-4 paddingRight5">
                        <asp:CheckBox ID="chkNeedMob" runat="server" />
                    </div>
                </div>

                <div class="row">
                    <div class="col-sm-2">
                        Message
                    </div>
                    <div class="col-sm-9 paddingRight5">
                        <asp:TextBox ID="txtRMessage" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <asp:Button ID="btnResConfirm" Text="Confirm" runat="server" CssClass="btn btn-default" />
                <asp:Button ID="btnResClear" Text="Clear" runat="server" CssClass="btn btn-default" OnClick="btnResClear_Click" />
            </div>
        </div>

    </asp:Panel>





    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnaddcpst" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpAddCostItems" runat="server" Enabled="True" TargetControlID="btnaddcpst"
                PopupControlID="pnlAddPCAlloc" CancelControlID="btnClosePCAttach" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlAddPCAlloc" DefaultButton="lbtnSearch">
                <div runat="server" id="Div6" class="panel panel-default" style="width: 600px; height: 250px; padding: 0px;">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-sm-10">
                                    :: Profit Center Allocation ::
                                </div>
                                <div class="col-sm-1">
                                    <asp:LinkButton ID="btnClosePCAttach" runat="server">
                                       <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="panel-body">

                            <div class="col-sm-7">

                                <div class="panel panel-default">

                                    <div class="panel-heading">
                                    </div>

                                    <div class="panel-body">

                                        <div class="col-sm-12">

                                            <div class="row">

                                                <div class="col-sm-4 labelText1">
                                                    Channel
                                                </div>

                                                <div class="col-sm-7 paddingRight5">
                                                    <asp:TextBox ID="txtMChan" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>

                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="LinkButton4" runat="server" TabIndex="6" OnClick="lbtnfindMchannel_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>


                                            </div>

                                        </div>

                                        <div class="col-sm-12">

                                            <div class="row">

                                                <div class="col-sm-4 labelText1">
                                                    Sub Channel
                                                </div>

                                                <div class="col-sm-7 paddingRight5">
                                                    <asp:TextBox ID="txtMSubChan" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>

                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="LinkButton5" runat="server" TabIndex="7" OnClick="lbtnsubMchannel_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>


                                            </div>

                                        </div>

                                        <div class="col-sm-12">

                                            <div class="row">

                                                <div class="col-sm-4 labelText1">
                                                    Profit Center
                                                </div>

                                                <div class="col-sm-7 paddingRight5">
                                                    <asp:TextBox ID="txtMPCenter" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>

                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="LinkButton6" runat="server" TabIndex="8" OnClick="lbtnpcMfind_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>


                                            </div>

                                        </div>


                                    </div>

                                </div>

                            </div>

                            <div class="col-sm-5">

                                <div class="panel panel-default">

                                    <div class="panel-heading">
                                    </div>
                                    <div class="panel-body panelscoll">

                                        <asp:CheckBoxList runat="server" ID="chkMList" CssClass="media-list"></asp:CheckBoxList>

                                    </div>

                                </div>

                            </div>

                            <div class="col-sm-12">

                                <div class="row">

                                    <div class="col-sm-2">
                                        <asp:Button ID="brtnaddM" Text=">>" runat="server" TabIndex="9" CssClass="btn btn-default btn-xs" OnClick="brtnaddM_Click" />
                                    </div>

                                    <div class="col-sm-3">
                                        <asp:Button ID="btnselectall" Text="Select All" runat="server" TabIndex="10" CssClass="btn btn-default btn-xs" OnClick="btnselectall_Click" />
                                    </div>

                                    <div class="col-sm-3">
                                        <asp:Button ID="btnunselect" Text="Unselect" runat="server" TabIndex="11" CssClass="btn btn-default btn-xs" OnClick="btnunselect_Click" />
                                    </div>

                                    <div class="col-sm-2">
                                        <asp:Button ID="btnclear" Text="Clear" runat="server" TabIndex="12" CssClass="btn btn-default btn-xs" OnClick="btnclear_Click" />
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Button ID="btnAppPcApply" Text="Apply" runat="server" TabIndex="12" CssClass="btn btn-default btn-xs" OnClick="btnAppPcApply_Click" />
                                    </div>
                                </div>

                            </div>
                            <div class="row">

                                <div class="col-sm-12 height5">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-11 panelscollbar height100">
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="grdAppPC" CausesValidation="false" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="8" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResult_SelectedIndexChanged" OnPageIndexChanging="grdResult_PageIndexChanging">
                                                <Columns>
                                                    <asp:TemplateField Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnpCRemove" runat="server" CausesValidation="false" Width="10px" OnClientClick="ConfirmDelete();" OnClick="lbtnpCRemove_Click">
                                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true" ></span>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Profit Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_a_Pc" runat="server" Text='<%# Bind("SRPR_PC") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Promo code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_a_Promo" runat="server" Text='<%# Bind("SRPR_PROMO_CD") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Active">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="col_a_Act" runat="server" Width="80px" Checked='<%#Convert.ToBoolean(Eval("Srpr_act")) %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PbSeq" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_a_pbSeq" runat="server" Text='<%# Bind("srpr_pbseq") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                            </div>
                            <div class="row">

                                <div class="col-sm-12 height5">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-8">
                                    <asp:Button ID="btnuppdateAp" Text="Update" runat="server" TabIndex="11" CssClass="btn btn-default btn-xs" OnClick="btnuppdateAp_Click" />
                                </div>

                                <div class="col-sm-2">
                                    <asp:Button ID="btnokAp" Text="ok" runat="server" TabIndex="12" CssClass="btn btn-default btn-xs" OnClick="btnokAp_Click" />
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button ID="btncleargrid" Text="Clear" runat="server" TabIndex="12" CssClass="btn btn-default btn-xs" OnClick="btncleargrid_Click" />
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
            <asp:Button ID="btnAddComDet" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mAddCombine" runat="server" Enabled="True" TargetControlID="btnAddComDet"
                PopupControlID="pnlAddPromo" CancelControlID="btnCloseCombine" PopupDragHandleControlID="Div6" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlAddPromo" DefaultButton="lbtnSearch">
                <div runat="server" id="Div1" class="panel panel-primary" style="width: 600px; height: 250px; padding: 0px;">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-sm-10">
                                    :: Combine / Free promotion setup ::
                                </div>
                                <div class="col-sm-1">
                                    <asp:LinkButton ID="btnCloseCombine" runat="server">
                                       <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-2">
                                Main Item
                            </div>
                            <div class="col-sm-4 paddingRight5">
                                <asp:TextBox ID="txtCMainItem" runat="server" CssClass="form-control" AutoPostBack="true" Enabled="false"></asp:TextBox>
                            </div>

                            <div class="col-sm-2">
                                Main Price
                            </div>
                            <div class="col-sm-3 paddingRight5">
                                <asp:TextBox ID="txtCMainPrice" runat="server" CssClass="form-control" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                <asp:TextBox ID="txtCMainLine" runat="server" CssClass="form-control" AutoPostBack="true" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtCPBSeq" runat="server" CssClass="form-control" AutoPostBack="true" Visible="false"></asp:TextBox>
                            </div>

                        </div>

                        <div class="row">
                            <div class="col-sm-2">
                                Item Code
                            </div>
                            <div class="col-sm-3 paddingRight5">
                                <asp:TextBox ID="txtCItem" runat="server"  CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-1 paddingLeft0">
                                <asp:LinkButton ID="lbtnCItemSearch" runat="server" OnClick="lbtnCItemSearch_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </div>

                            <div class="col-sm-2">
                                Qty
                            </div>
                            <div class="col-sm-3 paddingRight5">
                                <asp:TextBox ID="txtCQty" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-sm-2">
                                Price
                            </div>
                            <div class="col-sm-3 paddingRight5">
                                <asp:TextBox ID="txtCPrice" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-1">
                                <div class="col-sm-7 paddingRight5">
                                    <asp:LinkButton ID="lbtnAddComDet" CausesValidation="false" runat="server" OnClick="lbtnAddComDet_Click">
                                                        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>

                        </div>


                    </div>

                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel-body panelscollbar height100">
                                <asp:GridView ID="dgvComDet" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                    EmptyDataText="No data found..." AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Main Item">
                                            <ItemTemplate>
                                                <asp:Label ID="sapc_main_itm_cd" runat="server" Text='<%# Bind("sapc_main_itm_cd") %>' Width="75px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item">
                                            <ItemTemplate>
                                                <asp:Label ID="sapc_itm_cd" runat="server" Text='<%# Bind("sapc_itm_cd") %>' Width="75px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="sapc_qty" runat="server" Text='<%# Bind("sapc_qty") %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Price">
                                            <ItemTemplate>
                                                <asp:Label ID="sapc_price" runat="server" Text='<%# Bind("sapc_price") %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>

            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="SIPopup" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="pnlpopup" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpopup" DefaultButton="lbtnSearch">
        <div runat="server" id="test" class="panel panel-primary Mheight">

            <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="btnClose" runat="server" >
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
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>

                                    <div id="resultgrd" class="panelscoll POPupResultspanelscroll">
                                        <asp:GridView ID="grdResult" CausesValidation="false" AllowPaging="true" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" OnPageIndexChanging="grdResult_PageIndexChanging" OnSelectedIndexChanged="grdResult_SelectedIndexChanged">
                                            <Columns>
                                                <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>



    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
        <ContentTemplate>

            <asp:Button ID="Button6" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="excelUpload" runat="server" Enabled="True" TargetControlID="Button6"
                PopupControlID="pnlexcel" PopupDragHandleControlID="PopupHeader" CancelControlID="btnClose3" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>


        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlexcel">
        <div runat="server" id="Div7" class="panel panel-default height45 width700 ">


            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="btnClose3" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <%--<span>Commen Search</span>--%>
                    <div class="col-sm-11">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblalert" runat="server" ForeColor="Red" Text="Label" Visible="false"></asp:Label>
                                <asp:Label ID="lblsuccess" runat="server" ForeColor="Green" Visible="false"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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

                            <div class="col-sm-12" id="Div8" runat="server">
                                <div class="col-sm-5 paddingRight5">
                                    <asp:FileUpload ID="fileupexcelupload" runat="server" />

                                </div>

                                <div class="col-sm-2 paddingRight5">
                                    <asp:Button ID="Button7" class="btn btn-warning btn-xs" runat="server" Text="Upload"
                                        OnClick="btnUpload_Click" />
                                </div>


                            </div>


                            <%--<div class="row">--%>
                            <div class="col-sm-12 ">
                                <div id="divUpcompleted" visible="false" runat="server" class="alert alert-info alert-success" role="alert">
                                    <div class="col-sm-9">
                                        <strong>Alert!</strong>
                                        <asp:Label ID="Label4" Text="Excel file upload completed. Do you want to process?" runat="server"></asp:Label>
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



    <asp:UpdatePanel ID="UpdatePanel21" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button8" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupConfBox" runat="server" Enabled="True" TargetControlID="Button8"
                PopupControlID="pnlConfBox" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlConfBox" runat="server" align="center">
        <div runat="server" id="Div10" class="panel panel-info height120 width250">
            <asp:Label ID="Label6" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:ScriptManagerProxy ID="ScriptManagerProxy19" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel22" runat="server">
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
                                <asp:Button ID="btnok" runat="server" Text="yes" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnok_Click" />
                            </div>
                            <div class="col-sm-2 ">
                                <asp:Button ID="btnno" runat="server" Text="No" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnno_Click" />
                            </div>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>


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
