<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="BankFacilityDefinition.aspx.cs" Inherits="FastForward.SCMWeb.View.MasterFiles.Operational.BankFacilityDefinition" %>

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

        function ConfirmClearForm() {
            var selectedvalueOrd = confirm("Do you want to clear all details ?");
            if (selectedvalueOrd) {
                document.getElementById('<%=txtconfirmclear.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconfirmclear.ClientID %>').value = "No";
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

    <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server" DisplayAfter="10">
        <ProgressTemplate>

            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>

        </ProgressTemplate>
    </asp:UpdateProgress>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="txtconfirmclear" runat="server" />
            <asp:HiddenField ID="txtconfirmplaceord" runat="server" />

            <div class="panel panel-default marginLeftRight5">

                <div class="row">

                    <div class="col-sm-8  buttonrow">

                        <div visible="false" class="alert alert-success" role="alert" runat="server" id="divok">

                            <div class="col-sm-11  buttonrow ">
                                <strong>Well done!</strong>
                                <asp:Label ID="lblok" runat="server"></asp:Label>
                            </div>

                            <div class="col-sm-1  buttonrow">
                                <div class="col-sm-3  buttonrow">
                                    <asp:LinkButton ID="lbtndivokclose" runat="server" CausesValidation="false" CssClass="floatright">
                                        <span class="glyphicon glyphicon-remove " aria-hidden="true" ></span>
                                    </asp:LinkButton>
                                </div>
                            </div>

                        </div>

                        <div visible="false" class="alert alert-danger" role="alert" runat="server" id="divalert">
                            <div class="col-sm-11  buttonrow ">
                                <strong>Alert!</strong>
                                <asp:Label ID="lblalert" runat="server"></asp:Label>
                            </div>
                            <div class="col-sm-1  buttonrow">
                                <div class="col-sm-3  buttonrow">
                                    <asp:LinkButton ID="lbtndicalertclose" runat="server" CausesValidation="false" CssClass="floatright">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>

                        <div visible="false" class="alert alert-info" role="alert" runat="server" id="Divinfo">
                            <div class="col-sm-11  buttonrow ">
                                <strong>Alert!</strong>
                                <asp:Label ID="lblinfo" runat="server"></asp:Label>
                            </div>
                            <div class="col-sm-1  buttonrow">
                                <div class="col-sm-3  buttonrow">
                                    <asp:LinkButton ID="lbtndivinfoclose" runat="server" CausesValidation="false" CssClass="floatright">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="col-sm-4  buttonRow crnbuttonrowmargin">

                        <div id="Information" runat="server" visible="false" class="alert alert-success alert-info" role="alert">
                            <div class="col-sm-11">
                                <strong>Info!</strong>
                                <asp:Label ID="lblBackDateInfor" runat="server"></asp:Label>
                                <asp:Label ID="lblH1" runat="server" Text="Label"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-3 paddingRight0">
                            <asp:LinkButton ID="LinkButton2" CausesValidation="false" runat="server" Visible="false" CssClass="floatleft">
                            <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Temp Save
                            </asp:LinkButton>
                        </div>

                        <div class="col-sm-4 paddingRight15">
                            <asp:LinkButton ID="lbtnsave" CausesValidation="false" runat="server" Visible="false" CssClass="floatleft">
                            <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save/Process
                            </asp:LinkButton>
                        </div>

                        <div class="col-sm-3 paddingLeft15" style="margin-left: -20px">
                            <asp:LinkButton ID="lbtncancel" CausesValidation="false" runat="server" CssClass="floatleft" OnClientClick="ConfirmPlaceOrder();" OnClick="lbtncancel_Click">
                                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                            </asp:LinkButton>
                        </div>

                        <div class="col-sm-2 paddingRight0">
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="floatRight" OnClientClick="ConfirmClearForm();" OnClick="LinkButton1_Click">
                            <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                            </asp:LinkButton>
                        </div>

                    </div>

                </div>

                <div class="row">

                    <div class="panel-body">

                        <div class="col-sm-12">

                            <div class="panel panel-default">

                                <div class="panel-heading pannelheading ">
                                    <strong>Bank Facility Definition</strong>
                                </div>

                                <div class="panel-body">

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-4 labelText1">
                                                Bank Code
                                            </div>

                                            <div class="col-sm-7 paddingRight5">
                                                <asp:TextBox ID="txtbankcode" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtbankcode_TextChanged" Style="text-transform: uppercase"></asp:TextBox>
                                            </div>

                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="lbtnbank" runat="server" TabIndex="1" OnClick="lbtnbank_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-4 labelText1">
                                                Branch
                                            </div>

                                            <div class="col-sm-7 paddingRight5">
                                                <asp:TextBox ID="txtbranch" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtbranch_TextChanged" Style="text-transform: uppercase"></asp:TextBox>
                                            </div>

                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="LinkButton3" runat="server" TabIndex="2" OnClick="LinkButton3_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-5 labelText1">
                                                Account #
                                            </div>

                                            <div class="col-sm-6 paddingRight5">
                                                <asp:TextBox ID="txtaccno" runat="server" CssClass="form-control" TabIndex="3" AutoPostBack="true" OnTextChanged="txtaccno_TextChanged" Style="text-transform: uppercase"></asp:TextBox>
                                            </div>

                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-4 labelText1">
                                                Currency
                                            </div>

                                            <div class="col-sm-7 paddingRight5">
                                                <asp:TextBox ID="txtcurr" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtcurr_TextChanged" TabIndex="4"></asp:TextBox>
                                            </div>

                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="LinkButton7" runat="server" OnClick="LinkButton7_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>

                                        </div>
                                    </div>



                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-4 labelText1">
                                                Valid From
                                            </div>

                                            <div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtvalidfrom" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                        onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                    <asp:CalendarExtender ID="calexdate" runat="server" TargetControlID="txtvalidfrom"
                                                        PopupButtonID="lbtnfrm" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>

                                                <div id="caldv" class="col-sm-1 paddingLeft0" style="margin-left: -10px; margin-top: -2px">
                                                    <asp:LinkButton ID="lbtnfrm" TabIndex="5" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-4 labelText1">
                                                Valid To
                                            </div>

                                            <div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtvalidto" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                        onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtvalidto"
                                                        PopupButtonID="LinkButton5" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>

                                                <div id="caldsv" class="col-sm-1 paddingLeft0" style="margin-left: -10px; margin-top: -2px">
                                                    <asp:LinkButton ID="LinkButton5" TabIndex="6" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-5 labelText1">
                                                Facility
                                            </div>

                                            <div class="col-sm-7 paddingRight5">
                                                <asp:DropDownList ID="ddlfacility" runat="server" AutoPostBack="true" TabIndex="7" CssClass="form-control" OnSelectedIndexChanged="ddlfacility_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-4 labelText1">
                                                Facility Date
                                            </div>

                                            <div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="txtfacilitydate" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                        onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtfacilitydate"
                                                        PopupButtonID="LinkButton6" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>

                                                <div id="caldddsv" class="col-sm-1 paddingLeft0" style="margin-left: -10px; margin-top: -2px">
                                                    <asp:LinkButton ID="LinkButton6" TabIndex="8" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-4 labelText1">
                                                Facility Amount
                                            </div>

                                            <div class="col-sm-8 paddingRight5">
                                                <asp:TextBox ID="txtfacilityamy" runat="server" onkeydown="return jsDecimals(event);" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtfacilityamy_TextChanged" TabIndex="9"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-4 labelText1">
                                                Utilized Amount
                                            </div>

                                            <div class="col-sm-8 paddingRight5">
                                                <asp:TextBox ID="txtutilamt" runat="server" onkeydown="return jsDecimals(event);" CssClass="form-control" TabIndex="10" AutoPostBack="true" OnTextChanged="txtutilamt_TextChanged"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-5 labelText1">
                                                Balance
                                            </div>

                                            <div class="col-sm-7 paddingRight5">
                                                <asp:TextBox ID="txtbal" runat="server" ReadOnly="true" onkeydown="return jsDecimals(event);" CssClass="form-control" TabIndex="11"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-4 labelText1">
                                                Bank RT %
                                            </div>

                                            <div class="col-sm-8 paddingRight5">
                                                <asp:TextBox ID="txtbankrt" runat="server" onkeydown="return jsDecimals(event);" CssClass="form-control" TabIndex="12"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>


                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-4 labelText1">
                                                Other Ref
                                            </div>

                                            <div class="col-sm-8 paddingRight5">
                                                <asp:TextBox ID="txtourref" runat="server" CssClass="form-control" TabIndex="13"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-4 labelText1">
                                                Bank Ref
                                            </div>

                                            <div class="col-sm-8 paddingRight5">
                                                <asp:TextBox ID="txtbankref" runat="server" CssClass="form-control" TabIndex="14"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-5 labelText1">
                                                Commission Rate %
                                            </div>

                                            <div class="col-sm-7 paddingRight5">
                                                <asp:TextBox ID="txtcomrate" runat="server" onkeydown="return jsDecimals(event);" CssClass="form-control" TabIndex="15"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <div class="row">

                                            <div class="col-sm-4 labelText1">
                                                Active
                                            </div>

                                            <div class="col-sm-7 paddingRight5">
                                                <asp:DropDownList ID="ddlstus" runat="server" AutoPostBack="true" TabIndex="16" CssClass="form-control">
                                                    <asp:ListItem Text="Active" Selected="True" Value="1" />
                                                    <asp:ListItem Text="Inactive" Value="0" />
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-sm-1 paddingRight5">
                                                <asp:LinkButton ID="lbtnadd" runat="server" CausesValidation="false" OnClick="lbtnadd_Click">
                                                   <span class="glyphicon glyphicon-plus" tabindex="17" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>

                                    <div class="row">

                                        <div class="panel-body">

                                            <div class="col-sm-12">

                                                <div class="panel panel-default">

                                                    <div class="panel-heading pannelheading ">
                                                        Facility Details
                                                    </div>

                                                    <div class="panel-body">

                                                        <div class="row">
                                                            <div class="col-sm-12">

                                                                <div class="panelscoll275">

                                                                    <asp:GridView ID="gvaccdetails" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">

                                                                        <Columns>

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
                                                                                    &nbsp;<asp:LinkButton ID="lbtngrdInvoiceDetailsCancel" runat="server" CausesValidation="false" CommandName="Cancel" OnClick="lbtngrdInvoiceDetailsCancel_Click">
                                                                <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>
                                                                                    </asp:LinkButton>
                                                                                </EditItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Right" Width="1%" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Bank Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblbankcode" runat="server" Text='<%# Bind("msbf_cd") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Branch Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblbranch" runat="server" Text='<%# Bind("msbf_branch_cd") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Account #">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblacc" runat="server" Text='<%# Bind("msbf_acc_cd") %>' Width="125px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Currency">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblcurr" runat="server" Text='<%# Bind("msbf_curr") %>' Width="75px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Valid From">
                                                                                <EditItemTemplate>
                                                                                    <asp:TextBox ID="txtvalidfrom"  Text='<%# Bind("msbf_valid_frm", "{0:dd/MMM/yyyy}") %>' runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                                                        onkeypress="return RestrictSpace()"></asp:TextBox>
                                                                                    <asp:CalendarExtender ID="calexdatefrom" runat="server" TargetControlID="txtvalidfrom"
                                                                                        PopupButtonID="lblvalidfrom" Format="dd/MMM/yyyy">
                                                                                    </asp:CalendarExtender>
                                                                                </EditItemTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblvalidfrom" runat="server" Text='<%# Bind("msbf_valid_frm", "{0:dd/MMM/yyyy}") %>' Width="85px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Valid To">
                                                                                <EditItemTemplate>
                                                                                    <asp:TextBox ID="txtvalidto" Text='<%# Bind("msbf_valid_to", "{0:dd/MMM/yyyy}") %>' runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                                                        onkeypress="return RestrictSpace()"></asp:TextBox>
                                                                                    <asp:CalendarExtender ID="calexdateto" runat="server" TargetControlID="txtvalidto"
                                                                                        PopupButtonID="lblvalidto" Format="dd/MMM/yyyy">
                                                                                    </asp:CalendarExtender>
                                                                                  </EditItemTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblvalidto" runat="server" Text='<%# Bind("msbf_valid_to", "{0:dd/MMM/yyyy}") %>' Width="85px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Facility">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblfacility" runat="server" Text='<%# Bind("msbf_fac_tp") %>' Width="75px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Facility Date">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblfacilitydate" runat="server" Text='<%# Bind("msbf_fac_dt", "{0:dd/MMM/yyyy}") %>' Width="75px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Other Ref">
                                                                                <EditItemTemplate>
                                                                                    <asp:TextBox ID="txtourref" runat="server" Text='<%# Bind("msbf_our_ref") %>'></asp:TextBox>
                                                                                </EditItemTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblourref" runat="server" Text='<%# Bind("msbf_our_ref") %>' Width="125px"></asp:Label>
                                                                                </ItemTemplate>

                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Bank Ref">
                                                                                <EditItemTemplate>
                                                                                    <asp:TextBox ID="txtbankref" runat="server" Text='<%# Bind("msbf_bank_ref") %>'></asp:TextBox>
                                                                                </EditItemTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblbankref" runat="server" Text='<%# Bind("msbf_bank_ref") %>' Width="125px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Status">
                                                                                <EditItemTemplate>
                                                                                    <asp:DropDownList ID="ddlstatus" runat="server" Text='<%# Bind("msbf_act") %>' CssClass="form-control" AutoPostBack="true" AppendDataBoundItems="true" Width="100px">
                                                                                        <asp:ListItem Text="Active" Value="Active" />
                                                                                        <asp:ListItem Text="Inactive" Value="Inactive" />
                                                                                    </asp:DropDownList>
                                                                                </EditItemTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblstus" runat="server" Text='<%# Bind("msbf_act") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Facility Amount">
                                                                                <EditItemTemplate>
                                                                                    <asp:TextBox ID="txtfacilityamt" runat="server" Text='<%# Bind("msbf_fac_lmt") %>'></asp:TextBox>
                                                                                </EditItemTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblfacilityamt" runat="server" Text='<%# Bind("msbf_fac_lmt","{0:#,0.00}") %>' Width="125px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="textAlignRight" Width="7%" />
                                                                                <ItemStyle HorizontalAlign="Right" Width="7%" />
                                                                            </asp:TemplateField>
                                                                         
                                                                            <asp:TemplateField HeaderText="Utilized Amount">
                                                                                <EditItemTemplate>
                                                                                    <asp:TextBox ID="txtutilamt" runat="server" Text='<%# Bind("msbf_fac_ult") %>'></asp:TextBox>
                                                                                </EditItemTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblutilamt" runat="server" Text='<%# Bind("msbf_fac_ult","{0:#,0.00}") %>' Width="125px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="textAlignRight" Width="7%" />
                                                                                <ItemStyle HorizontalAlign="Right" Width="7%" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Balance">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblbalance" runat="server" Text='<%# Bind("balance","{0:#,0.00}") %>' Width="125px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="textAlignRight" Width="7%" />
                                                                                <ItemStyle HorizontalAlign="Right" Width="7%" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Bank RT %">
                                                                                <EditItemTemplate>
                                                                                    <asp:TextBox ID="txtbankrt" runat="server" Text='<%# Bind("msbf_fac_rt") %>'></asp:TextBox>
                                                                                </EditItemTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblbankrt" runat="server" Text='<%# Bind("msbf_fac_rt", "{0:N2}") %>' Width="125px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="textAlignRight" Width="7%" />
                                                                                <ItemStyle HorizontalAlign="Right" Width="7%" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Commission Rate %">
                                                                                <EditItemTemplate>
                                                                                    <asp:TextBox ID="txtcomrate" runat="server" Text='<%# Bind("msbf_comm_rt") %>'></asp:TextBox>
                                                                                </EditItemTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblcomrate" runat="server" Text='<%# Bind("msbf_comm_rt", "{0:N2}") %>' Width="120px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="textAlignRight" Width="10%" />
                                                                                <ItemStyle HorizontalAlign="Right" Width="10%" />
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


</asp:Content>
