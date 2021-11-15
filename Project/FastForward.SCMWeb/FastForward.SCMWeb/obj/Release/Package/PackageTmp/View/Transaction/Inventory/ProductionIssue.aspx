<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="ProductionIssue.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Inventory.ProductionIssue" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">

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
    <script>

        function filterDigits(eventInstance) {
            eventInstance = eventInstance || window.event;
            key = eventInstance.keyCode || eventInstance.which;
            if ((key < 58) && (key > 47) || key == 45 || key == 8) {
                return true;
            }

            else {
                if (eventInstance.preventDefault)
                    eventInstance.preventDefault();
                eventInstance.returnValue = false;
                return false;

            } //if
        } //filterDigits


        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
              && (charCode < 48 || charCode > 57))
                return false;

            return true;
        };


        function checkDate(sender, args) {

            if ((sender._selectedDate < new Date())) {
                alert("You cannot select a day earlier than today!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
        function SaveConfirm() {

            var selectedvalue = confirm("Do you want to save data?");
            if (selectedvalue) {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "No";
            }
        };
        function DeleteConfirm() {

            var selectedvalue = confirm("Do you want to delete item?");
            if (selectedvalue) {
                document.getElementById('<%=txtDeleteconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtDeleteconformmessageValue.ClientID %>').value = "No";
            }
        };
        function UpdateConfirm() {

            var selectedvalue = confirm("Do you want to update data?");
            if (selectedvalue) {
                document.getElementById('<%=txtUpdateconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtUpdateconformmessageValue.ClientID %>').value = "No";
            }
        };
        function ApproveConfirm() {

            var selectedvalue = confirm("Do you want to finesh data?");
            if (selectedvalue) {
                document.getElementById('<%=txtApprovalconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtApprovalconformmessageValue.ClientID %>').value = "No";
            }
        };
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
              && (charCode < 48 || charCode > 57))
                return false;
            return true;
        };
        function ConfirmClearForm() {
            var selectedvalueOrd = confirm("Do you want to clear all details ?");
            if (selectedvalueOrd) {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "No";
            }
        };
        function ConfirmPDAForm() {
            var selectedvalueOrd = confirm("Do you want to sent to PDA ?");
            if (selectedvalueOrd) {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "No";
            }
        };
    </script>
    <style>
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
    <asp:HiddenField ID="txtACancelconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtClearlconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtSavelconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtUpdateconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtApprovalconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtCancelconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtDeleteconformmessageValue" runat="server" />
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="upmain">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="panel panel-default marginLeftRight5">
        <asp:UpdatePanel runat="server" ID="upmain">
            <ContentTemplate>
                <div class="row">
                    <div class="col-sm-12 buttonrow">

                        <div class="col-sm-12 buttonRow paddingRight5" id="divTopCheck" runat="server">
                            <div class="col-sm-7 buttonRow padding0">
                            </div>
                            <div class="col-sm-5 buttonRow padding0">

                                <div class="col-sm-2">
                                </div>
                                <div class="col-sm-2 paddingRight0">
                                    <asp:LinkButton ID="lbtnpda" runat="server" CssClass="floatRight"  OnClick="lbtnpda_Click"> 
                                        <span class="glyphicon glyphicon-upload" aria-hidden="true"></span>PDA
                                    </asp:LinkButton>
                                </div>

                                <div class="col-sm-2 paddingRight0">
                                    <asp:LinkButton ID="lbtnSave" runat="server" CssClass="floatRight" OnClientClick="SaveConfirm()" OnClick="lbtnSave_Click"> 
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-2">
                                    <asp:LinkButton ID="lblUClear" runat="server" CssClass="floatRight" OnClientClick="ConfirmClearForm();" OnClick="lbtnclear_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="row" id="divMainRow">
                    <div class="panel-body paddingbottom0">
                        <div class="col-sm-12">
                            <div class="panel panel-default marginLeftRight5">
                                <div class="panel-heading pannelheading  paddingtop0">
                                    <strong><b>Production Issue</b></strong>
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-sm-6 paddingRight0 paddingLeft5">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading pannelheading height16 paddingtop0 ">
                                                        <strong><b>Production Details</b></strong>
                                                    </div>
                                                    <div class="panel-body">
                                                        <div class="row">

                                                            <div class="col-sm-6">

                                                                <div class="row">
                                                                    <div class="col-sm-4 labelText1">
                                                                        Production Date 
                                                                    </div>
                                                                    <div class="col-sm-6  ">
                                                                        <asp:TextBox runat="server" Enabled="false" ID="txtpDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="lbtncompletedate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                        <asp:CalendarExtender ID="CalendarExtender1" OnClientDateSelectionChanged="checkDate" runat="server" TargetControlID="txtpDate"
                                                                            PopupButtonID="lbtncompletedate" Format="dd/MMM/yyyy">
                                                                        </asp:CalendarExtender>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-4 labelText1">
                                                                        From Location
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox runat="server" OnTextChanged="txtfloc_TextChanged" ID="txtfloc" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnloc" runat="server" CausesValidation="false" OnClick="lbtnloc_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-4 labelText1">
                                                                        To Location
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox runat="server" ID="txttoloc" OnTextChanged="txttoloc_TextChanged" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" OnClick="lbtnloc2_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-4 labelText1">
                                                                        Company
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox runat="server" ID="txtcom" Enabled="false" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-4 labelText1">
                                                                        Request #
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox runat="server" ID="txtreq" Enabled="false" Style="text-transform: uppercase" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-4 labelText1">
                                                                        Job.#
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox runat="server" ID="txtjob" Enabled="false" Style="text-transform: uppercase" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <div class="row">
                                                                    <div class="col-sm-3 labelText1">
                                                                        Main Item
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox runat="server" ID="txtmitem" Enabled="false" Style="text-transform: uppercase" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-3 labelText1">
                                                                        Vehicle #
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox runat="server" ID="txtvehi" Style="text-transform: uppercase" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-3 labelText1">
                                                                        Remark
                                                                    </div>
                                                                    <div class="col-sm-9">
                                                                        <asp:TextBox runat="server" TextMode="MultiLine" ID="txtremark" Style="text-transform: uppercase" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-3 height5">
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-3 labelText1">
                                                                        Total.Qty
                                                                    </div>
                                                                    <div class="col-sm-4">
                                                                        <asp:TextBox runat="server" ID="txtpqty" Enabled="false" onkeypress="filterDigits(event)" Style="text-align: right" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-3 labelText1">
                                                                        Issue Qty
                                                                    </div>
                                                                    <div class="col-sm-4">
                                                                        <asp:TextBox runat="server" ID="txtIqty" onkeypress="filterDigits(event)" Style="text-align: right" CausesValidation="false" CssClass="form-control" OnTextChanged="txtIqty_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-2">
                                                                        <asp:LinkButton ID="lbtnaddqty" CausesValidation="false" runat="server" OnClick="lbtnaddqty_Click">
                                                                            <span class="glyphicon glyphicon-arrow-down" aria-hidden="true" style="font-size:15px"></span> 
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>

                                                    </div>
                                                </div>

                                            </div>
                                            <div class="col-sm-6 paddingRight0 paddingLeft5">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading pannelheading height16 paddingtop0 ">
                                                        <strong><b>Pending MRN Details</b></strong>
                                                    </div>
                                                    <div class="panel-body">
                                                        <div class="row">
                                                            <div class="col-sm-5">
                                                                <div class="col-sm-4 labelText1">
                                                                    From Date 
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <asp:TextBox runat="server" Enabled="false" ID="txtfrom" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0">
                                                                    <asp:LinkButton ID="lbtnfrom" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtfrom"
                                                                        PopupButtonID="lbtnfrom" Format="dd/MMM/yyyy">
                                                                    </asp:CalendarExtender>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-5">
                                                                <div class="col-sm-4 labelText1">
                                                                    To Date 
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <asp:TextBox runat="server" Enabled="false" ID="txtto" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0">
                                                                    <asp:LinkButton ID="lblTo" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtto"
                                                                        PopupButtonID="lblTo" Format="dd/MMM/yyyy">
                                                                    </asp:CalendarExtender>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <asp:LinkButton ID="btnSearch" runat="server" CausesValidation="false" OnClick="btnSearch_Click">
                                                    <span class="glyphicon glyphicon-search fontsize20 right5" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 ">
                                                                <div class="panel-body  panelscollbar height100">
                                                                    <asp:GridView ID="grdpendinreq" ShowHeader="true" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                        EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lbtnselect" CausesValidation="false" runat="server" OnClick="lbtnselect_Click">
                                                                            <span class="glyphicon glyphicon-arrow-down" aria-hidden="true" style="font-size:15px"></span> 
                                                                                    </asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="seq" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_itr_seq_no" runat="server" Text='<%# Bind("itr_seq_no") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="MRN Date">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_itr_dt" runat="server" Text='<%# Bind("itr_dt","{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="MRN #">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_itr_req_no" runat="server" Text='<%# Bind("itr_req_no") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Ref #">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_itr_ref" runat="server" Text='<%# Bind("itr_ref") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Job No">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_itr_job_no" runat="server" Text='<%# Bind("itr_job_no") %>'></asp:Label>
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
                                            <div class="col-sm-6 paddingRight0">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading pannelheading height16 paddingtop0 ">
                                                        <strong><b>KIT Component Details</b></strong>
                                                    </div>

                                                    <div class="panel-body  panelscollbar height250">
                                                        <asp:GridView ID="grdItem" ShowHeader="true" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                            EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Item">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_spd_itm" runat="server" Text='<%# Bind("spd_itm") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Description">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_spd_itm_desc" runat="server" Text='<%# Bind("spd_itm_desc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Model">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_spd_model" runat="server" Text='<%# Bind("spd_model") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Qty">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_spd_est_qty" runat="server" Text='<%# Bind("spd_est_qty" ,"{0:N4}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-6 paddingLeft5">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading pannelheading height16 paddingtop0 ">
                                                        <strong><b>Generating Serials  </b></strong>
                                                    </div>
                                                    <div class="panel-body">
                                                        <div class="row">
                                                            <div class="col-sm-10 padding0">
                                                                <div class="col-sm-4 padding0">
                                                                    <div class="col-sm-4 labelText1">
                                                                        Prefix
                                                                    </div>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox runat="server" ID="txtprefix" CausesValidation="false" CssClass="form-control" OnTextChanged="txtprefix_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4 padding0">
                                                                    <div class="col-sm-6 labelText1">
                                                                        From Serial #
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox runat="server" ID="txtser1" onkeypress="filterDigits(event)" Style="text-align: right"
                                                                            CausesValidation="false" CssClass="diWMClick validateInt form-control" OnTextChanged="txtser1_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4 padding0">
                                                                    <div class="col-sm-5 labelText1">
                                                                        To Serial #
                                                                    </div>
                                                                    <div class="col-sm-7">
                                                                        <asp:TextBox runat="server" ID="txtser2" Style="text-align: right" CausesValidation="false"
                                                                            CssClass="diWMClick validateInt form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="col-sm-1 padding0">
                                                                <asp:LinkButton ID="lbtnadserial" runat="server" TabIndex="103" CausesValidation="false" OnClick="lbtnadserial_Click">
                                                                     <span class="glyphicon glyphicon-plus" style="font-size:20px;"  aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="col-sm-1 padding0">
                                                                <asp:LinkButton ID="lbtnclearserial" runat="server" TabIndex="103" CausesValidation="false" OnClick="llbtnclearserial_Click">
                                                                     <span class="glyphicon glyphicon-refresh" style="font-size:20px;"  aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="panel-body  panelscollbar height200">
                                                                    <asp:GridView ID="grdserial" ShowHeader="true" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                        EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Bin code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_Tus_bin" runat="server" Text='<%# Bind("Tus_bin") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Item Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_Tus_itm_cd" runat="server" Text='<%# Bind("Tus_itm_cd") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Item Status">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_Tus_itm_stus" runat="server" Text='<%# Bind("Tus_itm_stus_Desc") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Serial #">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_Tus_ser_1" runat="server" Text='<%# Bind("Tus_ser_1") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Qty">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="col_Tus_qty" runat="server" Text='<%# Bind("Tus_qty","{0:N2}") %>'></asp:Label>
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
                        </div>
                    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
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
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy6" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
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

            <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btncus" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="MPPDA" runat="server" Enabled="True" TargetControlID="btncus"
                PopupControlID="CustomerPanel" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>

            <asp:Panel runat="server" ID="CustomerPanel">
                <div runat="server" id="Div1" class="panel panel-default height150 width525">
                    <asp:Label ID="Label14" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="btncClose" runat="server" CausesValidation="false" OnClick="btncClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12 paddingLeft0 paddingRight0">

                                <div class="row">

                                    <div class="col-sm-12">
                                        <asp:Panel runat="server" ID="pnldoc" Visible="false">
                                            <div class="col-sm-12">
                                                <div class="row">

                                                    <div class="col-sm-5 labelText1">
                                                        Document No
                                                    </div>
                                                    <div class="col-sm-7">
                                                        <asp:TextBox ID="txtdocname" runat="server" TabIndex="1" CssClass="form-control"></asp:TextBox>
                                                    </div>

                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <div class="row">
                                            <div class="col-sm-12 height10">
                                            </div>
                                        </div>

                                        <div class="col-sm-12">
                                            <div class="row">

                                                <div class="col-sm-5 labelText1">
                                                    Loading Bay
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="ddlloadingbay" runat="server" TabIndex="2" CssClass="form-control"></asp:DropDownList>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12 height10">
                                            </div>
                                        </div>

                                        <div class="col-sm-12">
                                            <div class="row">

                                                <div class="col-sm-5">
                                                </div>

                                                <div class="col-sm-7">
                                                    <asp:Button ID="btnsend" runat="server" TabIndex="3" CssClass="btn-info form-control" Text="Send" OnClientClick="ConfirmSendToPDA();" OnClick="btnsend_Click" />
                                                </div>

                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12 height10">
                                            </div>
                                        </div>

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
            </asp:Panel>
            <asp:HiddenField ID="hfScrollPosition2" Value="0" runat="server" />
            <asp:HiddenField ID="hfScrollPosition1" Value="0" runat="server" />

        </ContentTemplate>
    </asp:UpdatePanel>
    <%-- add balance error popup --%>
    <asp:UpdatePanel runat="server" ID="UpdatePanel15">
        <ContentTemplate>
            <asp:Button ID="btnpop1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popBalancErr" runat="server" Enabled="True" TargetControlID="btnpop1"
                PopupControlID="pnlBalancErr" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress7" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="upBalancErr">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait10" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait10" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:Panel runat="server" ID="pnlBalancErr">
        <asp:UpdatePanel runat="server" ID="upBalancErr">
            <ContentTemplate>
                <div runat="server" id="Div5" class="panel panel-primary" style="padding: 5px;">
                    <div class="panel panel-default" style="height: 270px; width: 500px;">
                        <div class="panel-heading" style="height: 25px;">
                            <div class="col-sm-10 paddingLeft0">
                                <strong>Free stock not available </strong>
                            </div>
                            <div class="col-sm-2 text-right">
                                <asp:LinkButton ID="lbtnBalancErr" runat="server" OnClick="lbtnBalancErr_Click">
                                    <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="" style="height:235px; overflow-y:auto">
                                <asp:GridView ID="dgvItmBalErr" ShowHeader="true" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                    EmptyDataText="No data found..." AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Item Code">
                                            <ItemTemplate>
                                                <asp:Label Width="100%"  ID="lblInl_itm_cd" Text='<%# Bind("itemCode") %>' runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="100px"  />
                                            <ItemStyle Width="100px"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Required  Quantity">
                                            <ItemTemplate>
                                                <asp:Label Width="100%" ID="lblPick_qty" Text='<%# Bind("Pick_qty","{0:###,##0.00##}") %>' runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="100px" CssClass="gridHeaderAlignRight" />
                                            <ItemStyle Width="100px" CssClass="gridHeaderAlignRight" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Available Quantity">
                                            <ItemTemplate>
                                                <asp:Label Width="100%" ID="lblPick_qty" Text='<%# Bind("Sad_do_qty","{0:###,##0.00##}") %>' runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="100px" CssClass="gridHeaderAlignRight" />
                                            <ItemStyle Width="100px" CssClass="gridHeaderAlignRight" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
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
                else if (str.length < 5) {
                    if (charCode > 47 && charCode < 58) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else {
                    $(this).value = str.substr(0, 5);
                    //alert(charCode);
                    alert('Maximum 5 characters are allowed ');
                    return false;
                }
            });
        }
    </script>
</asp:Content>
