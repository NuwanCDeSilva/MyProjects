<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="ItemCannibalizedComponentSetup.aspx.cs" Inherits="FastForward.SCMWeb.View.MasterFiles.Inventory.ItemCannibalizedComponentSetup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script type="text/javascript">


        function ConfirmClearForm() {
            var selectedvalueclear = confirm("Do you want to clear all details ?");
            if (selectedvalueclear) {
                document.getElementById('<%=txtconfirmclear.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconfirmclear.ClientID %>').value = "No";
            }
        };

        function ConfirmSave() {
            var selectedvalsave = confirm("Do you want to save ?");
            if (selectedvalsave) {
                document.getElementById('<%=txtsaveconfirm.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtsaveconfirm.ClientID %>').value = "No";
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

        function showSuccessToast() {
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
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
            $().toastmessage('showToast', {
                text: value,
                sticky: true,
                position: 'top-center',
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

        function Check(textBox, maxLength) {
            if (textBox.value.length > maxLength) {
                alert("Maximum characters allowed are " + maxLength);
                textBox.value = textBox.value.substr(0, maxLength);
            }
        };

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

    <style>
        .POPupResultspanelscroll {
            height:290px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanelMain">

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
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
        <ContentTemplate>
                            <asp:HiddenField runat="server" ID="hdfCostTp" Value="Cost"/>
            <asp:HiddenField ID="txtconfirmclear" runat="server" />
            <asp:HiddenField ID="txtsaveconfirm" runat="server" />
            <asp:HiddenField ID="txtconfirmdelete" runat="server" />


            <div class="panel panel-default marginLeftRight5">

                <div class="row">
                    <div class="col-sm-12" style="height: 28px;">
                        <div class="col-sm-8 ">
                            <div id="Information" runat="server" visible="false" class="alert alert-success alert-info" role="alert">
                                <div class="col-sm-11">
                                    <strong>Info!</strong>
                                    <asp:Label ID="lblBackDateInfor" runat="server"></asp:Label>
                                    <asp:Label ID="lblH1" runat="server" Text="Label"></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-4  buttonRow floatRight padding0 paddingtopbottom0">
                            <div class="col-sm-3 paddingRight0">
                            </div>
                            <div class="col-sm-3 paddingRight0">
                            </div>
                            <div class="col-sm-3 paddingRight0">
                                <asp:LinkButton ID="btnSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmSave();" OnClick="btnSave_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                </asp:LinkButton>
                            </div>
                            <div class="col-sm-3">
                                <asp:LinkButton ID="btnClear" runat="server" CssClass="floatRight" OnClientClick="ConfirmClearForm();" OnClick="btnClear_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">

                    <div class="panel-body">

                        <div class="col-sm-9">

                            <div class="panel panel-default">

                                <div class="panel-heading pannelheading ">
                                    <strong>Main Item Cannibalization Component Setup</strong>
                                </div>

                                <div class="panel-body" id="panelbodydiv">

                                    <div class="col-sm-6">
                                        <div class="row">

                                            <div class="col-sm-4 labelText1 ">
                                                Main Item Code
                                            </div>

                                            <div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtItem" TabIndex="1" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtItem_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="lbtnitem" runat="server" OnClick="lbtnitem_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>

                                        </div>

                                    </div>

                                    <div class="col-sm-6">
                                        <div class="row">

                                            <div class="col-sm-4 labelText1">
                                                Serial #
                                            </div>


                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtSer" TabIndex="2" CausesValidation="false" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtSer_TextChanged"></asp:TextBox>
                                            </div>

                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="btnsrch_ser" runat="server" OnClick="btnsrch_ser_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>


                                        </div>

                                    </div>

                                </div>
                            </div>
                        </div>

                        <div class="col-sm-3">

                            <div class="panel panel-default">

                                <div class="panel-heading pannelheading ">
                                    <strong>Cost Method</strong>
                                </div>

                                <div class="panel-body" id="panelbodydiv1">

                                    <div class="col-sm-12">
                                        <div class="row">

                                            <asp:RadioButtonList ID="RBordby" TabIndex="3" AutoPostBack="true" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="Percentage &nbsp;&nbsp;" Value="1" Selected="True"/>
                                                <asp:ListItem Text="Amount" Value="0" />
                                            </asp:RadioButtonList>

                                        </div>

                                    </div>

                                </div>
                            </div>
                        </div>

                    </div>
                </div>

                <div class="row">

                    <div class="panel-body">

                        <div class="col-sm-12">

                            <div class="panel panel-default">

                                <div class="panel-heading pannelheading ">
                                    Assign Components
                                </div>

                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-sm-5 padding0">
                                                <div class="col-sm-2 labelText1 padding0">
                                                    Item Code
                                                </div>
                                                <div class="col-sm-4 padding0">
                                                    <asp:TextBox ID="txtitmcodeenter" TabIndex="4" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtitmcodeenter_TextChanged"></asp:TextBox>
                                                </div>

                                                <div class="col-sm-1" style="padding-left: 3px; padding-right: 0px;">
                                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="col-sm-1 labelText1 padding0">
                                                    Description
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:TextBox ID="txtdescenter" TabIndex="5" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-7 padding0">
                                                <div class="col-sm-2 padding0">
                                                    <div class="col-sm-6 labelText1 padding0">
                                                        No of Unit
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <asp:TextBox ID="txtnoofunitenter" TabIndex="6" onkeydown="return jsDecimals(event);" runat="server" 
                                                            CssClass=" form-control rightMc"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3 padding0">
                                                    <div class="col-sm-4 labelText1">
                                                        Cost
                                                    </div>
                                                    <div class="col-sm-8 padding0">
                                                        <asp:TextBox ID="txtcostenter" TabIndex="7"  runat="server" 
                                                            CssClass="txtcostenter form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3 padding0" runat="server" visible="false">
                                                    <div class="col-sm-4 labelText1">
                                                        Seq.#
                                                    </div>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtseqnoenter" Text="0" TabIndex="8" runat="server" CssClass="form-control txtseqnoenter"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3 padding0">
                                                    <div class="col-sm-6 labelText1 ">
                                                        Status
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <asp:DropDownList ID="ddlstus" runat="server" AutoPostBack="true" CssClass="form-control">
                                                            <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                 <div class="col-sm-3 padding0">
                                                    <div class="col-sm-6 labelText1 ">
                                                        Allow Edit
                                                    </div>
                                                    <div class="col-sm-6 padding0">
                                                        <asp:DropDownList ID="editableddl" runat="server" AutoPostBack="true" CssClass="form-control">
                                                            <asp:ListItem Text="Editable" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Uneditable" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-1 padding0">
                                                     <div class="col-sm-12">
                                                        <asp:LinkButton ID="lbtnstus" runat="server" OnClick="lbtnstus_Click">
                                                                                <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
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

                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-12">

                                            <div class="panelscoll4">

                                                <asp:GridView ID="dgvSelect" AutoGenerateColumns="False" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found..." OnRowDataBound="dgvSelect_RowDataBound" OnRowUpdating="dgvSelect_RowUpdating" OnRowDeleting="dgvSelect_RowDeleting">

                                                    <Columns>

                                                        <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="" />

                                                        <asp:TemplateField ShowHeader="false">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtngrdInvoiceDetailsEdit" CausesValidation="false" runat="server" OnClick="lbtngrdInvoiceDetailsEdit_Click">
                                                                                                    <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:LinkButton ID="lbtngrdInvoiceDetailsUpdate" runat="server" CausesValidation="false" CommandName="Update" OnClick="lbtngrdInvoiceDetailsUpdate_Click">
                                                                                                    <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                                <asp:LinkButton ID="lbtngrdInvoiceDetailsCancel" runat="server" CausesValidation="false" CommandName="Cancel" OnClick="lbtngrdInvoiceDetailsCancel_Click">
                                                                                                    <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </EditItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="3%" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtngrdInvoiceDetailsDalete" runat="server" CausesValidation="false" OnClientClick="ConfirmDelete();" OnClick="lbtngrdInvoiceDetailsDalete_Click">
                                                                                                    <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Code">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblitemcode" runat="server" Text='<%# Bind("mikc_itm_code_component") %>' Width="150px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Description">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldesc" runat="server" Text='<%# Bind("mikc_desc_component") %>' Width="250px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="No of Unit">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtnoofunits" CssClass="rightMc" onkeydown="return jsDecimals(event);" runat="server" Style="text-align: right;"
                                                                    Text='<%# Bind("mikc_no_of_unit","{0:N2}") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <div class="oprefnotxtbox">
                                                                    <asp:Label ID="lblunits" runat="server" Text='<%# Bind("mikc_no_of_unit","{0:N2}") %>' Width="100px"></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField >
                                                            <HeaderTemplate>
                                                                <asp:Label runat="server" ID="lclCostHed" Text='Cost'></asp:Label>
                                                            </HeaderTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtcostgrid" CssClass="txtcostgrid" runat="server" Style="text-align: right;" 
                                                                    Text='<%# Bind("mikc_cost","{0:N2}") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <div class="oprefnotxtbox">
                                                                    <asp:Label ID="lblcost" Style="text-align: right;" runat="server" Text='<%# Bind("mikc_cost","{0:N2}") %>' Width="100px"></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Sequence #" Visible="false">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtsequence" runat="server" Style="text-align: right;" CssClass="txtsequence" Text='<%# Bind("mikc_seq_no") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblseq" runat="server" Style="text-align: right;" Text='<%# Bind("mikc_seq_no") %>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:Label Width="10px" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="mikc_itm_type" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="mikc_itm_type" runat="server" Text='<%# Bind("mikc_itm_type") %>' Width="1px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblstatusvalue" runat="server" Text='<%# Eval("mikc_active") %>' Visible="false" />
                                                                <asp:DropDownList ID="ddlnewstus" runat="server" CssClass="form-control" AutoPostBack="true" Width="80px">
                                                                    <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="mikc_item_cate" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="mikc_item_cate" runat="server" Text='<%# Bind("mikc_item_cate") %>' Width="1px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="mikc_cost_method" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="mikc_cost_method" runat="server" Text='<%# Bind("mikc_cost_method") %>' Width="1px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="mikc_chg_main_serial" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="mikc_chg_main_serial" runat="server" Text='<%# Bind("mikc_chg_main_serial") %>' Width="1px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="mikc_uom" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="mikc_uom" runat="server" Text='<%# Bind("mikc_uom") %>' Width="1px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="mikc_isscan" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="mikc_isscan" runat="server" Text='<%# Bind("mikc_isscan") %>' Width="1px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="mikc_scan_seq" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="mikc_scan_seq" runat="server" Text='<%# Bind("mikc_scan_seq") %>' Width="1px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="mikc_tp" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="mikc_tp" runat="server" Text='<%# Bind("mikc_tp") %>' Width="1px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                          <asp:TemplateField HeaderText="Allow Edit">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbleditvalue" runat="server" Text='<%# Eval("mikc_allow_edit") %>' Visible="false" />
                                                                <asp:DropDownList ID="ddlaaloedit" runat="server" CssClass="form-control" AutoPostBack="true" Width="80px">
                                                                    <asp:ListItem Text="Editable" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="Uneditable" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
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
                    <asp:LinkButton ID="btnClose" runat="server" OnClick="btnClose_Click">
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
                        <asp:Panel runat="server" DefaultButton="lbtnSearch">
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
                                <asp:TextBox ID="txtSearchbyword" CausesValidation="false" placeholder="Search by word" class="form-control" runat="server" ></asp:TextBox>
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

                                
                            </asp:UpdatePanel>
                        </div>
                            </asp:Panel>
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
                                        <asp:GridView ID="grdResult" CausesValidation="false" AllowPaging="true" runat="server" GridLines="None" 
                                            CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" 
                                            OnPageIndexChanging="grdResult_PageIndexChanging" OnSelectedIndexChanged="grdResult_SelectedIndexChanged">
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


    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btn2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="MpDelivery" runat="server" Enabled="True" TargetControlID="btn2"
                PopupControlID="pnldel" PopupDragHandleControlID="PopupHeaderKit" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnldel" DefaultButton="lbtnSearch">
        <div runat="server" id="Div1" class="panel panel-primary Mheight" style="height:112px;">

            <asp:Label ID="Label7" runat="server" Text="Label" Visible="false"></asp:Label>

            <div class="panel panel-default">
                <div class="panel-heading" style="height:25px;">

                  
                    <div class="col-sm-11">
                        <strong><b>Main Item Advanced Search</b></strong>
                    </div>
                    <div class="col-sm-1">
                          <asp:LinkButton ID="LinkButton13" runat="server" OnClick="LinkButton13_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    </div>
                </div>
                <div class="panel-body">


                    <asp:UpdatePanel ID="blkcancelpnl" runat="server">
                        <ContentTemplate>


                            <div class="row">

                                <div class="col-sm-12">

                                    <div class="col-sm-6">
                                        <div class="row">

                                            <div class="col-sm-4">
                                                Location
                                            </div>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtLoc" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1">
                                                <asp:LinkButton ID="btnUserLocation" runat="server" OnClick="btnUserLocation_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-6">
                                        <div class="row">

                                            <div class="col-sm-5">
                                                Bin Location
                                            </div>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtbinlocation" runat="server" CssClass="form-control"></asp:TextBox>
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

                                    <div class="col-sm-6">
                                        <div class="row">

                                            <div class="col-sm-4">
                                                Serial #
                                            </div>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtserialpopup" runat="server" CssClass="form-control" 
                                                    ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-6">
                                        <div class="row">

                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-11">
                                                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">
                                                        <span class="glyphicon glyphicon-ok-sign fontsize20" style="margin-top:-6px" aria-hidden="true"></span>
                                                </asp:LinkButton>
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
    </asp:Panel>


    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnexcel" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpexcel" runat="server" Enabled="True" TargetControlID="btnexcel"
                PopupControlID="pnlpopupExcel" PopupDragHandleControlID="PopupHeaderExcel" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpopupExcel" DefaultButton="lbtnSearch">
        <div runat="server" id="Div2" class="panel panel-primary" style="width:500px;">

            <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading" style="height:25px;">
                    
                    <div class="col-sm-11">
                    </div>
                    <div class="col-sm-1">
                        <asp:LinkButton ID="LinkButton8" runat="server" OnClick="LinkButton8_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12">

                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="pnlpopupExcelss">

                                        <div class="col-sm-12">

                                            <div class="panel panel-default">

                                                <div class="panel-heading">
                                                    <strong>
                                                        <asp:Label ID="lblcaption" runat="server"></asp:Label>
                                                    </strong>
                                                </div>

                                                <div class="panel-body">

                                                    <div class="row">

                                                        <div id="dvitm" runat="server">
                                                            <div class="col-sm-12">

                                                                <div class="" style="height:250px; overflow-x:hidden; overflow-y:auto;">

                                                                    <asp:GridView ID="dgvItem" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found..." OnSelectedIndexChanged="dgvItem_SelectedIndexChanged">

                                                                        <Columns>

                                                                            <asp:CommandField ShowSelectButton="true" ButtonType="Link" />

                                                                            <asp:TemplateField HeaderText="Item Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblitemcodepopup" runat="server" Text='<%# Bind("ins_itm_cd") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Serial #">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSerialPop" runat="server" Text='<%# Bind("ins_SER_1") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                        </Columns>
                                                                    </asp:GridView>

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

                                                    </div>


                                                </div>


                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>



                                    </asp:Panel>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </asp:Panel>
     <asp:UpdatePanel runat="server" ID="u1">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupSerial" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="testPanel" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="testPanel" DefaultButton="LinkButton1">
        <div runat="server" id="Div3" class="panel panel-primary Mheight" style="width: 700px;">
            <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default" style="height: 370px;">
                <div class="panel-heading" style="height: 25px;">
                    <div class="col-sm-11"><B>Main Item Advanced Search</B></div>
                    <div class="col-sm-1">
                         <asp:LinkButton ID="LinkButton3" runat="server">
                             <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="panel-body">
    
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy8" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="dgvResult" CausesValidation="false" runat="server" AllowPaging="True"
                                        GridLines="None" CssClass="table table-hover table-striped" PageSize="15" PagerStyle-CssClass="cssPager" ShowHeaderWhenEmpty="true"
                                        EmptyDataText="No data found..."
                                         OnPageIndexChanged="dgvResult_PageIndexChanged" OnSelectedIndexChanged="dgvResult_SelectedIndexChanged">
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
        Sys.Application.add_load(func);
        function func() {
           
            $('.txtseqnoenter, .txtsequence').keypress(function (evt) {
                // var ch = (evt.which) ? evt.which : evt.keyCode;
                var charCode = evt.which;
                var str = $(this).val();
            
                if ((charCode == 8) || (charCode == 9) || (charCode == 0)) {
                    return true;
                }
                else if (str.length < 30) {
                    if (charCode > 47 && charCode < 58) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else {
                    $(this).value = str.substr(0, 30);
                    alert('Maximum 30 characters are allowed ');
                    return false;
                }
            });
           
            jQuery('.rightMc').on('input', function (event) {
                if (!jQuery.isNumeric(this.value)) {
                    this.value = "";
                }
                if (parseFloat(this.value) <= 0) {
                    this.value = "";
                }
            });

            jQuery('.rightMc').keypress(function (event) {
                if ((event.which != 46 || $(this).val().indexOf('.') != -1) &&
                  ((event.which < 48 || event.which > 57) &&
                    (event.which != 0 && event.which != 8))) {
                    event.preventDefault();
                }

                var text = $(this).val();
                if ((text.indexOf('.') != -1) &&
                  (text.substring(text.indexOf('.')).length > 2) &&
                  (event.which != 0 && event.which != 8) &&
                  ($(this)[0].selectionStart >= text.length - 2)) {
                    event.preventDefault();
                }
            });
            $('.txtcostenter, txtcostgrid').keypress(function (evt) {
                // var ch = (evt.which) ? evt.which : evt.keyCode;
                var charCode = evt.which;
                var str = $(this).val();
                
                if ((charCode == 8) || (charCode == 9) || (charCode == 0)) {
                    
                    return true;
                }
                else if (str.length < 15) {
                    if (charCode > 47 && charCode < 58) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else {
                    $(this).value = str.substr(0,15);
                    alert('Maximum 15 characters are allowed ');
                    return false;
                }
            });

            $('.txtcostenter, txtcostgrid').mousedown(function (e) {
                if (e.button == 2) {
                    alert('This functionality is disabled !');
                    return false;
                } else {
                    return true;
                }
            });
            }
    </script>
</asp:Content>
