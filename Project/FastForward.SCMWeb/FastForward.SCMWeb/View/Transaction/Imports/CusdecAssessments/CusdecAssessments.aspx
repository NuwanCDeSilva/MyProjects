<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="CusdecAssessments.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Imports.Cusdec_Assessments.CusdecAssessments" %>

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

        function ConfirmApproveRequest() {
            var selectedvaldelitm = confirm("Do you want to approve ?");
            if (selectedvaldelitm) {
                document.getElementById('<%=txtapprove.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtapprove.ClientID %>').value = "No";
            }
        };

        function ConfirmCancelReq() {
            var selectedvaldelitm = confirm("Do you want to cancel ?");
            if (selectedvaldelitm) {
                document.getElementById('<%=txtcancel.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtcancel.ClientID %>').value = "No";
            }
        };

        function showSuccessToast() {
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
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
                position: 'top-left',
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
        .panel {
            margin-bottom: 1px;
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


    <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
        <ContentTemplate>

            <asp:HiddenField ID="txtconfirmclear" runat="server" />
            <asp:HiddenField ID="txtsaveconfirm" runat="server" />
            <asp:HiddenField ID="txtconfirmdelete" runat="server" />
            <asp:HiddenField ID="txtapprove" runat="server" />
            <asp:HiddenField ID="txtcancel" runat="server" />

            <div class="panel panel-default marginLeftRight5">

                <div class="row">

                    <div class="col-sm-7  buttonrow">
                        <div id="Information" runat="server" visible="false" class="alert alert-success alert-info" role="alert">

                            <strong>Info!</strong>
                            <asp:Label ID="lblBackDateInfor" runat="server"></asp:Label>
                            <asp:Label ID="lblH1" runat="server" Text="Label"></asp:Label>

                        </div>
                    </div>

                    <div class="col-sm-4  buttonRow">

                        <div class="col-sm-3 paddingRight0">
                            <asp:LinkButton ID="lbtnaddentry" CausesValidation="false" runat="server" CssClass="floatRight lbtnaddentry" OnClick="lbtnaddentry_Click">
                            <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>Add Entry
                            </asp:LinkButton>
                        </div>

                        <div class="col-sm-4 paddingRight0">
                            <asp:LinkButton ID="lbtnremoveentry" CausesValidation="false" CssClass="floatRight" runat="server" OnClientClick="ConfirmDelete();" OnClick="lbtnremoveentry_Click">
                            <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>Remove Entry
                            </asp:LinkButton>
                        </div>

                        <div class="col-sm-3 ">
                            <asp:LinkButton ID="lbtnsave" runat="server" CssClass="floatRight" OnClientClick="ConfirmSave();" OnClick="lbtnsave_Click">
                            <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                            </asp:LinkButton>
                        </div>

                        <div class="col-sm-2 paddingRight0">
                            <asp:LinkButton ID="lbuttonclear" CausesValidation="false" runat="server" OnClientClick="ConfirmClearForm();" OnClick="lbuttonclear_Click">
                                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear 
                            </asp:LinkButton>
                        </div>

                    </div>

                    <div class="col-sm-1  buttonRow">

                        <div class="col-sm-12">

                            <div class="col-sm-2 paddingRight0">
                                <div class="dropdown">
                                    <a id="dLabel" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
                                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                                    </a>
                                    <div class="dropdown-menu menupopup" aria-labelledby="dLabel">
                                        <div class="row">
                                            <div class="col-sm-12">

                                                <div id="hiddendv" runat="server">
                                                    <div class="col-sm-12 paddingRight0">
                                                        <asp:LinkButton ID="lbrnapprove" CausesValidation="false" runat="server" OnClientClick="ConfirmApproveRequest();" OnClick="lbrnapprove_Click">
                                                        <span class="glyphicon glyphicon-thumbs-up" aria-hidden="true"></span>Approve 
                                                        </asp:LinkButton>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-12 height10">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12 paddingRight0">
                                                    <asp:LinkButton ID="lbtncancel" CausesValidation="false" runat="server" OnClientClick="ConfirmCancelReq();" OnClick="lbtncancel_Click">
                                                        <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>Cancel 
                                                    </asp:LinkButton>
                                                </div>

                                                 <div class="col-sm-12 paddingRight0">
                                                    <asp:LinkButton ID="lbtnASTprint" CausesValidation="false" runat="server"  OnClick="lbtnASTprint_Click">
                                                        <span class="glyphicon glyphicon-print" aria-hidden="true"></span>AST Print 
                                                    </asp:LinkButton>
                                                </div>

                                                   <div class="col-sm-12 paddingRight0">
                                                    <asp:LinkButton ID="lbtnASTprint2" CausesValidation="false" runat="server"  OnClick="lbtnASTprint2_Click">
                                                        <span class="glyphicon glyphicon-print" aria-hidden="true"></span>AST PrintII 
                                                    </asp:LinkButton>
                                                </div>

                                              <%--     <div class="col-sm-12 paddingRight0">
                                                    <asp:LinkButton ID="lbtnASTAccountprint" CausesValidation="false" runat="server"  OnClick="lbtnASTAccountprint_Click">
                                                        <span class="glyphicon glyphicon-print" aria-hidden="true"></span>AST Account Print
                                                    </asp:LinkButton>
                                                </div>--%>
                                                <div class="row">
                                                    <div class="col-sm-12 height10">
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

                    <div class="panel-body">

                        <div class="col-sm-12">

                            <div class="panel panel-default">

                                <div class="panel-heading pannelheading ">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-sm-8">
                                                Entry Details
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="col-sm-6">
                                                </div>
                                                <div class="col-sm-1" style="padding-right: 3px; margin-top: 3px;">
                                                    <asp:CheckBox Text="" ID="chkDoDfs" OnCheckedChanged="chkDoDfs_CheckedChanged" AutoPostBack="true" runat="server" />
                                                </div>
                                                <div class="col-sm-2 labelText1 padding0">
                                                    <asp:Label Text="Do Dfs" runat="server" />
                                                </div>
                                                <div class="col-sm-1" style="padding-right: 3px; margin-top: 3px;">
                                                    <asp:CheckBox Text="" ID="chkReBond" OnCheckedChanged="chkReBond_CheckedChanged" AutoPostBack="true" runat="server" />
                                                </div>
                                                <div class="col-sm-2 labelText1 padding0">
                                                    <asp:Label Text="Re Bond " runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                                <div class="panel-body" id="panelbodydiv">

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-3 labelText1">
                                                Document No
                                            </div>

                                            <div class="col-sm-7 paddingRight5">
                                                <asp:TextBox ID="txtbondno" runat="server" AutoPostBack="true" onFocus="this.select()"
                                                    OnTextChanged="txtbondno_TextChanged" CssClass="txtbondno form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-2 paddingLeft0">
                                                <asp:LinkButton ID="lbtnbondload" runat="server" OnClick="lbtnbondload_Click" TabIndex="-1">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="col-sm-2">
                                        <div class="row">

                                            <div class="col-sm-3 labelText1">
                                                Amend
                                            </div>

                                            <div class="col-sm-6 paddingRight5">
                                                <asp:CheckBox runat="server" ID="chkpending" OnCheckedChanged="chkpending_CheckedChanged" AutoPostBack="true" TabIndex="-1" />
                                            </div>
                                        </div>

                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-6 labelText1">
                                                Related Bond No / Doc No
                                            </div>

                                            <div class="col-sm-6 paddingRight5">
                                                <asp:TextBox ID="txtrlatedbond" ReadOnly="true" runat="server" TabIndex="2" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="col-sm-4">
                                        <div class="row">

                                            <div class="col-sm-4 labelText1">
                                                Assessment Ref
                                            </div>

                                            <div class="col-sm-7 paddingRight5">
                                                <asp:TextBox ID="txtassref" runat="server"
                                                    OnTextChanged="txtassref_TextChanged" AutoPostBack="true"
                                                    TabIndex="-1" ReadOnly="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="lbtnassload" runat="server" TabIndex="-1" OnClick="lbtnassload_Click">
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

                    <div class="panel-body">

                        <div class="col-sm-6">

                            <div class="panel panel-default">

                                <div class="panel-heading pannelheading ">
                                    Entry Duty Details
                                </div>

                                <div class="panel-body">

                                    <div class="row">
                                        <div class="col-sm-12">

                                            <div class="" style="height: 230px; overflow-y: auto; overflow-x: hidden;">

                                                <asp:GridView ID="gvDutyentrydetails" AutoGenerateColumns="false" runat="server" TabIndex="60"
                                                    CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">

                                                    <Columns>

                                                        <asp:TemplateField HeaderText="cuds_cost_cat" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcuds_cost_cat" runat="server" Text='<%# Bind("cuds_cost_cat") %>' Width="1px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="cuds_cost_tp" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcuds_cost_tp" runat="server" Text='<%# Bind("cuds_cost_tp") %>' Width="1px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Duty">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblduty" runat="server" Text='<%# Bind("cuds_cost_ele") %>' Width="150px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Entry Amount">
                                                            <ItemTemplate>
                                                                <div class="oprefnotxtbox">
                                                                    <asp:Label ID="lblamount" runat="server" Text='<%# Bind("cuds_cost_ele_amt") %>' Width="140px"></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="8%" CssClass="gridHeaderAlignRight" />
                                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="cuds_cost_claim_amt" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_cost_claim_amt" runat="server" Text='<%# Bind("cuds_cost_claim_amt") %>' Width="1px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="cuds_cost_unclaim_amt" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_cost_unclaim_amt" runat="server" Text='<%# Bind("cuds_cost_unclaim_amt") %>' Width="1px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTemp" runat="server" Text='' Width="10px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Assessment Amount">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtassamt" CssClass="txtassamt form-control" runat="server"
                                                                    onFocus="this.select()"
                                                                    Text='<%# Bind("cuds_cost_stl_amt") %>' Style="text-align: right"
                                                                    onkeydown="return jsDecimals(event);" AutoPostBack="true"
                                                                    OnTextChanged="txtassamt_TextChanged"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="150px" CssClass="gridHeaderAlignRight" />
                                                            <ItemStyle Width="150px" CssClass="gridHeaderAlignRight" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowNo" runat="server" Text='<%# Bind("rowNo") %>' Width="1px"></asp:Label>
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

                        <div class="col-sm-3">

                            <div class="panel panel-default">

                                <div class="panel-heading pannelheading ">
                                </div>

                                <div class="panel-body">

                                    <div class="row">
                                        <div class="col-sm-12">

                                            <div class="col-sm-12">
                                                <div class="row">

                                                    <div class="col-sm-6 labelText1">
                                                        Assessment Notice No
                                                    </div>

                                                    <div class="col-sm-6 paddingRight5">
                                                        <asp:TextBox ID="txtassnoticeno" OnTextChanged="txtassnoticeno_TextChanged"
                                                            onFocus="this.select()"
                                                            runat="server" CssClass="txtassnoticeno form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="col-sm-12">
                                                <div class="row">

                                                    <div class="col-sm-6 labelText1">
                                                        Assessment Notice Date
                                                    </div>

                                                    <div>
                                                        <div class="col-sm-5">
                                                            <asp:TextBox ID="dtpFromDate" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                                onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                            <asp:CalendarExtender ID="calexdate" runat="server" TargetControlID="dtpFromDate"
                                                                PopupButtonID="lbtnassmentdate" Format="dd/MMM/yyyy">
                                                            </asp:CalendarExtender>
                                                        </div>

                                                        <div class="col-sm-1" style="margin-left: -10px; margin-top: -2px">
                                                            <asp:LinkButton ID="lbtnassmentdate" TabIndex="6" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="col-sm-12">
                                                <div class="row">

                                                    <div class="col-sm-6 labelText1">
                                                        Entry Total
                                                    </div>

                                                    <div class="col-sm-6 paddingRight5">
                                                        <asp:TextBox ID="txtentrytotal" TabIndex="6" Style="text-align: right" BorderStyle="None" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="col-sm-12">
                                                <div class="row">

                                                    <div class="col-sm-6 labelText1">
                                                        Assessment Total
                                                    </div>

                                                    <div class="col-sm-6 paddingRight5">
                                                        <asp:TextBox ID="txtasstot" TabIndex="7" runat="server" Style="text-align: right" BorderStyle="None" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="col-sm-12">
                                                <div class="row">

                                                    <div class="col-sm-6 labelText1">
                                                        Difference
                                                    </div>

                                                    <div class="col-sm-6 paddingRight5">
                                                        <asp:TextBox ID="txtcaldiff" TabIndex="8" runat="server" Style="text-align: right" BorderStyle="None" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>

                                        </div>

                                    </div>

                                </div>

                            </div>

                            <div class="panel panel-default">

                                <div class="panel-heading pannelheading ">
                                </div>

                                <div class="panel-body">

                                    <div class="row">
                                        <div class="col-sm-12">

                                            <div class="col-sm-12">
                                                <div class="row">

                                                    <div class="col-sm-6 labelText1">
                                                        Final Entry Total
                                                    </div>

                                                    <div class="col-sm-6 paddingRight5">
                                                        <asp:TextBox ID="txtfinaltotal" TabIndex="9" runat="server" Style="text-align: right" BorderStyle="None" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="col-sm-12">
                                                <div class="row">

                                                    <div class="col-sm-6 labelText1">
                                                        Final Assessment Total
                                                    </div>

                                                    <div class="col-sm-6 paddingRight5">
                                                        <asp:TextBox ID="txtfnalasstot" runat="server" Style="text-align: right" BorderStyle="None" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="col-sm-12">
                                                <div class="row">

                                                    <div class="col-sm-6 labelText1">
                                                        Final Assessment Difference
                                                    </div>

                                                    <div class="col-sm-6 paddingRight5">
                                                        <asp:TextBox ID="txtfinalassdiff" TabIndex="11" runat="server" Style="text-align: right" BorderStyle="None" ReadOnly="true" CssClass="form-control"></asp:TextBox>
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


                        </div>

                        <div class="col-sm-3">

                            <div class="panel panel-default">

                                <div class="panel-heading pannelheading ">
                                    All AOD Receipt Details
                                </div>

                                <div class="panel-body">

                                    <div class="row">
                                        <div class="col-sm-12">

                                            <div class="panelscoll2">

                                                <asp:GridView ID="gvaod" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="AOD #">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblaodno" runat="server" Text='<%# Bind("AOD_NO") %>' Width="150px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="AOD Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblaoddate" runat="server" Text='<%# Bind("AOD_DATE", "{0:dd/MMM/yyyy}") %>' Width="150px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="AOD Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblaodtype" runat="server" Text='<%# Bind("AOD_TYPE") %>' Width="150px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="From Location">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblfrom" runat="server" Text='<%# Bind("FROM_LOCATION") %>' Width="150px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Entry No" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblentrynoaod" runat="server" Text='<%# Bind("TO_LOCATION") %>' Width="150px"></asp:Label>
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

                    <div class="panel-body">

                        <div class="col-sm-6">

                            <div class="panel panel-default">

                                <div class="panel-heading pannelheading ">
                                    Total Entry Details
                                </div>

                                <div class="panel-body">

                                    <div class="row">
                                        <div class="col-sm-12">

                                            <div class="panelscoll">

                                                <asp:GridView ID="gvtotentry1" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">

                                                    <Columns>

                                                        <asp:TemplateField HeaderText="Seq No" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblseqnodetails" runat="server" Text='<%# Bind("istd_seq_no") %>' Width="1px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="line No" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbllineno" runat="server" Text='<%# Bind("istd_line_no") %>' Width="1px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Document No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblentryno" runat="server" Text='<%# Bind("istd_entry_no") %>' Width="75px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Notice No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblassno" runat="server" Text='<%# Bind("istd_assess_no") %>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Duty">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblduty" runat="server" Text='<%# Bind("istd_cost_ele") %>' Width="75px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Entry Amount">
                                                            <ItemTemplate>
                                                                <div class="oprefnotxtbox">
                                                                    <asp:Label ID="lblentryamt" runat="server" Text='<%# Bind("istd_cost_ele_amt","{0:n}") %>' Width="130px"></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="istd_cost_claim_amt" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblistd_cost_claim_amt" runat="server" Text='<%# Bind("istd_cost_claim_amt") %>' Width="1px"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="8%" HorizontalAlign="Center" />
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="istd_cost_unclaim_amt" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblistd_cost_unclaim_amt" runat="server" Text='<%# Bind("istd_cost_unclaim_amt") %>' Width="1px"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="8%" HorizontalAlign="Center" />
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;Assessment Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblassamt" runat="server" Text='<%# Bind("istd_cost_stl_amt","{0:n}") %>' Width="130px"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="8%" HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Difference">
                                                            <ItemTemplate>
                                                                <div class="oprefnotxtbox">
                                                                    <asp:Label ID="lbldiff" runat="server" Text='<%# Bind("istd_diff_amt","{0:n}") %>' Width="115px"></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Doc Date" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lvldate" runat="server" Text='<%# Bind("istd_assess_dt", "{0:dd/MMM/yyyy}") %>' Width="1px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="cuds_cost_cat" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcuds_cost_cat2" runat="server" Text='<%# Bind("cuds_cost_cat") %>' Width="1px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="cuds_cost_tp" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcuds_cost_tp2" runat="server" Text='<%# Bind("cuds_cost_tp") %>' Width="1px"></asp:Label>
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

                        <div class="col-sm-6">

                            <div class="panel panel-default">

                                <div class="panel-heading pannelheading">
                                    Total Entry Details Summary
                                </div>

                                <div class="panel-body">

                                    <div class="row">
                                        <div class="col-sm-12">

                                            <div class="panelscoll">

                                                <asp:GridView ID="gvtotentry2" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">

                                                    <Columns>

                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="chkcancel" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnSelect" runat="server" OnClick="lbtnSelect_Click">
                                                                    <span class="" aria-hidden="true"></span>Select
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="seqno" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblseqno" runat="server" Text='<%# Bind("isth_seq_no") %>' Width="1px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="docno" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldocno" runat="server" Text='<%# Bind("isth_doc_no") %>' Width="1px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Document No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblbondno" runat="server" Text='<%# Bind("istd_entry_no") %>' Width="75px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Entry Total">
                                                            <ItemTemplate>
                                                                <div class="oprefnotxtbox">
                                                                    <asp:Label ID="lblentrytot" runat="server" Text='<%# Bind("isth_entry_amt","{0:n}") %>' Width="130px"></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="8%" HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Assessment Total">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblasstot" runat="server" Text='<%# Bind("isth_stl_amt","{0:n}") %>' Width="155px"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="8%" HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Difference">
                                                            <ItemTemplate>
                                                                <div class="oprefnotxtbox">
                                                                    <asp:Label ID="lbldifffinal" runat="server" Text='<%# Bind("isth_diff_amt","{0:n}") %>' Width="130px"></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="8%" HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="assnoticeno" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblassnoticeno" runat="server" Text='<%# Bind("cuh_ast_noties_no") %>' Width="1px"></asp:Label>
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


    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="ASPopup" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="pnlpopupasessment" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpopupasessment" DefaultButton="lbtnSearch">
        <div runat="server" id="Div1" class="panel panel-primary Mheight">

            <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="LinkButton1" runat="server">
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

                    <div class="col-sm-5">
                        <div class="row">
                            <div class="col-sm-2 labelText1">
                                From
                            </div>
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-6 paddingRight5 paddingLeft0">

                                        <asp:TextBox runat="server" ID="txtFDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>

                                    </div>

                                    <div class="col-sm-1 paddingLeft0">
                                        <asp:LinkButton ID="lbtnFDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtFDate"
                                            PopupButtonID="lbtnFDate" Format="dd/MMM/yyyy">
                                        </asp:CalendarExtender>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <%-- <div class="row">--%>
                    </div>

                    <div class="col-sm-5">



                        <div class="col-sm-5 labelText1">
                            Search by key
                        </div>
                        <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="col-sm-7 paddingRight5">
                                    <asp:DropDownList ID="ddlSearchbykey2" runat="server" class="form-control">
                                    </asp:DropDownList>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <%--<div class="col-sm-2 labelText1">
                                Search by word
                            </div>
                            <div class="col-sm-4 paddingRight5">
                                <asp:TextBox ID="txtSearchbyword2" CausesValidation="false" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbyword2_TextChanged"></asp:TextBox>
                            </div>--%>
                    </div>


                    <%--<div class="col-sm-10">--%>
                    <div class="col-sm-5">
                        <div class="row">
                            <%--<div class="row">--%>
                            <div class="col-sm-2 labelText1">
                                To
                            </div>
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-6 paddingRight5 paddingLeft0">
                                        <asp:TextBox runat="server" ID="txtTDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-1 paddingLeft0">
                                        <asp:LinkButton ID="lbtnTDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                        <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtTDate"
                                            PopupButtonID="lbtnTDate" Format="dd/MMM/yyyy">
                                        </asp:CalendarExtender>

                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>


                            <%--  </div>--%>
                        </div>
                    </div>
                    <div class="col-sm-5">
                        <div class="col-sm-5 labelText1">
                            Search by word
                        </div>
                        <div class="col-sm-7 paddingRight5">
                            <asp:TextBox ID="txtSearchbyword2" CausesValidation="false" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbyword2_TextChanged"></asp:TextBox>
                        </div>
                    </div>
                    <%--<div class="col-sm-2 labelText1">
                                Search by key
                            </div>
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                            </div>--%>
                    <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="col-sm-1 paddingLeft0">
                                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="lbtnSearch_Click">
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
                        <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>

                                <div id="resultgrd" class="panelscoll POPupResultspanelscroll">
                                    <asp:GridView ID="grdResult2" CausesValidation="false" AllowPaging="true" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" OnPageIndexChanging="grdResult_PageIndexChanging" OnSelectedIndexChanged="grdResult_SelectedIndexChanged" EmptyDataText="No data found...">
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


     <asp:UpdatePanel ID="UpdatePanel15" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button5" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserDPopoup" runat="server" Enabled="True" TargetControlID="Button5"
                PopupControlID="pnlDpopup"  Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

     <asp:UpdatePanel ID="UpdatePanel19" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlDpopup" >
                <div runat="server" id="Div5" class="panel panel-default height400 width700">

                    <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="btnDCloseNew" runat="server" OnClick="btnDCloseNew_Click">
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
                                            <asp:TextBox runat="server" Enabled="false" ID="txtNewFrom" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnFDateNew" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txtNewFrom"
                                                PopupButtonID="lbtnFDateNew" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-2 labelText1">
                                            To
                                        </div>
                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                            <asp:TextBox runat="server" Enabled="false" ID="txtNewTo" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnTDateNew" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="txtNewTo"
                                                PopupButtonID="lbtnTDateNew" Format="dd/MMM/yyyy">
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
                                                    <asp:DropDownList ID="ddlSearchbykeyDate" runat="server" class="form-control">
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
                                            <asp:TextBox ID="txtSearchbywordDate" CausesValidation="false" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbywordDate_TextChanged"></asp:TextBox>
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
                                    <div class="col-sm-12">
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy13" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdResultDate" CausesValidation="false" runat="server" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" GridLines="None" CssClass="table table-hover table-striped" PageSize="5" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResult_SelectedIndexChanged" OnPageIndexChanging="grdResultDate_PageIndexChanging">
                                                    <Columns>
                                                        <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10" />

                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>

                                <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10"
                                    runat="server" AssociatedUpdatePanelID="UpdatePanel19">

                                    <ProgressTemplate>
                                        <div class="divWaiting2">
                                            <asp:Label ID="lblWait2" runat="server"
                                                Text="Please wait... " />
                                            <asp:Image ID="imgWait2" runat="server"
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
    <script>
        Sys.Application.add_load(func);
        function func() {
            $('.txtassamt').last().focusout(function () {
                $('#BodyContent_txtassnoticeno').focus();
            });

            $('.txtassnoticeno').focusout(function () {
                $('#BodyContent_lbtnaddentry').focus();
            });

            $('.lbtnaddentry').focusout(function () {
                $('#BodyContent_txtbondno').focus();
            });

            $('.txtbondno').focusout(function () {
                $('#BodyContent_gvDutyentrydetails_txtassamt').first().focus();
            });
        }
    </script>

</asp:Content>
