<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="OrderFinancing.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Imports.OrderFinancing.OrderFinancing" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
        function UpdateConfirm() {
            var selectedvalue = confirm("Do you want to save data?");
            if (selectedvalue) {
                document.getElementById('<%=txtUpdateconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtUpdateconformmessageValue.ClientID %>').value = "No";
            }
        };
        function ApprovalConfirm() {
            var selectedvalue = confirm("Do you want to approval data?");
            if (selectedvalue) {
                document.getElementById('<%=txtApprovalconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtApprovalconformmessageValue.ClientID %>').value = "No";
            }
        };
        function CancelConfirm() {
            var selectedvalue = confirm("Do you want to cancel data?");
            if (selectedvalue) {
                document.getElementById('<%=txtCancelconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtCancelconformmessageValue.ClientID %>').value = "No";
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
        function Enable() {
            return;
        }
    </script>
    <script type="text/javascript"><!--
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
    }
    function CheckAll(oCheckbox) {
        var GridView2 = document.getElementById("<%=grdPayAmount.ClientID %>");
        for (i = 1; i < GridView2.rows.length; i++) {
            GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
        }
    }

    function checkDate(sender, args) {

        if ((sender._selectedDate < new Date())) {
            alert("You cannot select a day earlier than today!");
            sender._selectedDate = new Date();
            sender._textbox.set_Value(sender._selectedDate.format(sender._format))
        }
    }

    function CheckAll2(oCheckbox) {
        var GridView2 = document.getElementById("<%=grdPid.ClientID %>");
        for (i = 1; i < GridView2.rows.length; i++) {
            GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
        }
    }
    function nocopy() {
        alert("Copying is not allowed!");
        return false;
    }

    $(function () {
        var controls = $(".disable");
        controls.bind("paste", function () {
            return false;
        });
        controls.bind("drop", function () {
            return false;
        });
        controls.bind("cut", function () {
            return false;
        });
        controls.bind("copy", function () {
            return false;
        });
    });
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
    <div class="panel panel-default marginLeftRight5">
        <div class="panel-body">
            <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="col-sm-6  buttonrow">
                                <div id="WarningOF" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">
                                    <div class="col-sm-11">
                                        <strong>Alert!</strong>
                                        <asp:Label ID="lblWOF" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-1">
                                        <asp:LinkButton ID="LinkButton6" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnWarningCostCategoryClose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>

                                </div>
                                <asp:Label ID="lblSeqno" Visible="false" runat="server" Text="Label"></asp:Label>
                                <div id="SuccessOF" runat="server" visible="false" class="alert alert-success alert-dismissible" role="alert">
                                    <div class="col-sm-11">
                                        <strong>Success!</strong>
                                        <asp:Label ID="lblSOF" runat="server"></asp:Label>

                                    </div>
                                    <div class="col-sm-1">
                                        <asp:LinkButton ID="LinkButton7" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnWarningCostCategoryClose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6  buttonRow">
                                    <div class="col-sm-1"></div>
                                    <div class="col-sm-2 paddingRight0">
                                        <asp:LinkButton ID="lbtnApproval" OnClientClick="ApprovalConfirm()" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lbtnApproval_Click">
                                        <span class="glyphicon glyphicon-thumbs-up" aria-hidden="true"></span>Approve
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-2 paddingRight0">
                                        <asp:LinkButton ID="lbtnCancel" OnClientClick="CancelConfirm()" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lbtnCancel_Click">
                                        <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>Cancel
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-2 paddingRight0">
                                        <asp:LinkButton ID="lbtnUpdate" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lbtnUpdate_Click" OnClientClick="UpdateConfirm()">
                                        <span class="glyphicon glyphicon-edit" aria-hidden="true"></span>Update
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-2 paddingRight0">
                                        <asp:LinkButton ID="lbtnAdd" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lbtnAdd_Click" OnClientClick="SaveConfirm()">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                        </asp:LinkButton>
                                    </div>

                                    <div class="col-sm-2">
                                        <asp:LinkButton ID="lbtnClear" runat="server" CssClass="floatRight" OnClientClick="ClearConfirm()" OnClick="lbtnClear_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
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
                                                        <div class="col-sm-12">
                                                            <asp:LinkButton ID="lbtnVKitItem" CausesValidation="false" runat="server" OnClick="lbtnVKitItem_Click">
                                                                    <span class="glyphicon glyphicon-shopping-cart fontsize15" aria-hidden="true"></span>View Kit item
                                                            </asp:LinkButton>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 height10">
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-12 paddingRight0">
                                                            <asp:LinkButton ID="lbtnVItem" CausesValidation="false" runat="server" OnClick="lbtnVItem_Click">
                                                                <span class="glyphicon glyphicon-book fontsize15" aria-hidden="true"></span>View Item
                                                            </asp:LinkButton>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-sm-12 height10">
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-12 paddingRight0">
                                                            <asp:LinkButton ID="lbtnSDetails" CausesValidation="false" runat="server" OnClick="lbtnSDetails_Click">
                                                                    <span class="glyphicon glyphicon-plane fontsize15" aria-hidden="true"></span>Shipping Details
                                                            </asp:LinkButton>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 height10">
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-12">
                                                            <asp:LinkButton ID="lbtnsettlepayment" Visible="true" runat="server" OnClick="lbtnsettlepayment_Click">
                                                                <span class="glyphicon glyphicon-export fontsize15" aria-hidden="true"></span>settle payment
                                                            </asp:LinkButton>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 height10">
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-12 paddingRight0">
                                                            <asp:LinkButton ID="lbtnBankCFacility" Visible="false" CausesValidation="false" runat="server">
                                                                    <span class="glyphicon glyphicon-upload fontsize15" aria-hidden="true"></span>Bank Creadit Facility
                                                            </asp:LinkButton>
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

                    <asp:HiddenField ID="txtClearlconformmessageValue" runat="server" />
                    <asp:HiddenField ID="txtSavelconformmessageValue" runat="server" />
                    <asp:HiddenField ID="txtUpdateconformmessageValue" runat="server" />
                    <asp:HiddenField ID="txtApprovalconformmessageValue" runat="server" />
                    <asp:HiddenField ID="txtCancelconformmessageValue" runat="server" />
                    <asp:HiddenField ID="txtDeleteconformmessageValue" runat="server" />
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-default">
                                <div class="panel-heading paddingtop0 paddingtopbottom0">

                                    <div class="row">
                                        <div class="col-sm-7">
                                           <strong>Order Finance</strong>
                                        </div>
                                        <div class="col-sm-2">
                                         
                                        </div>
                                        <div class="col-sm-3">
                                            
                                        </div>
                                    </div>
                                </div>
                                 <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-5 paddingRight5">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">

                                                <div class="row">
                                                    <div class="col-sm-7">
                                                        Document Details
                                                    </div>
                                                    <div class="col-sm-2">
                                                        Status
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:Label ID="lblStatus" runat="server" Text="Default" ForeColor="Red"></asp:Label>
                                                    </div>
                                                </div>


                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="row">
                                                            <div class="col-sm-5 labelText1">
                                                                Payment Term
                                                            </div>
                                                            <div class="col-sm-3 paddingLeft0 paddingRight0">
                                                                <asp:DropDownList ID="ddlPaymentTerms" AutoPostBack="True" CausesValidation="false" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPaymentTerms_SelectedIndexChanged"></asp:DropDownList>

                                                            </div>
                                                            <div class="col-sm-3 paddingLeft0 paddingRight0">
                                                                <asp:DropDownList ID="ddlSubpaymentTerms" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSubpaymentTerms_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-sm-5 labelText1">
                                                                Credit Period
                                                            </div>
                                                            <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                <asp:TextBox runat="server" ReadOnly="false" onkeypress="return isNumberKey(event)" ID="txtCPeriod" oncopy="return false"
                                                                    onpaste="return false"
                                                                    oncut="return false" MaxLength="3" CausesValidation="false" AutoPostBack="true" CssClass="form-control disable" OnTextChanged="txtCPeriod_TextChanged"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-5 labelText1">
                                                                Doc #
                                                            </div>
                                                            <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                <asp:TextBox runat="server" ReadOnly="true" ID="txtDocNo" CausesValidation="false" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft0">
                                                                <asp:LinkButton ID="lbtnDocSearch" runat="server" CausesValidation="false" OnClick="lbtnDocSearch_Click">
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
                                                                Date
                                                            </div>
                                                            <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                <asp:TextBox runat="server" ReadOnly="true" ID="txtPIDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft0">
                                                                <asp:LinkButton ID="lbtnPIDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtPIDate"
                                                                    PopupButtonID="lbtnPIDate" Format="dd/MMM/yyyy">
                                                                </asp:CalendarExtender>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-5 labelText1">
                                                                Expiry Date
                                                            </div>
                                                            <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                <asp:TextBox runat="server" ID="txtEDate" CausesValidation="false" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft0">
                                                                <asp:LinkButton ID="lbtnExpirDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                                <asp:CalendarExtender ID="CalendarExtender1" OnClientDateSelectionChanged="checkDate" runat="server" TargetControlID="txtEDate"
                                                                    PopupButtonID="lbtnExpirDate" Format="dd/MMM/yyyy">
                                                                </asp:CalendarExtender>
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
                                                            <div class="col-sm-5 labelText1">
                                                                Utility Value
                                                            </div>
                                                            <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                <asp:TextBox runat="server" oncopy="return false"
                                                                    onpaste="return false"
                                                                    oncut="return false" ID="txtUtilityV" CausesValidation="false" AutoPostBack="true" CssClass="form-control" onkeypress="return isNumberKey(event)" OnTextChanged="txtUtilityV_TextChanged"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>
                                                        <div class="row">

                                                            <div class="col-sm-5 labelText1">
                                                                Total Amount
                                                            </div>
                                                            <div class="col-sm-6 paddingRight5 paddingLeft0">

                                                                <asp:TextBox runat="server" ReadOnly="true" ID="txtTFAmount" CausesValidation="false" AutoPostBack="true" CssClass="form-control"></asp:TextBox>

                                                            </div>

                                                        </div>
                                                        <div class="row">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <div class="col-sm-5 paddingRight0 labelText1">
                                                                        Latest Ship.. Date 
                                                                    </div>
                                                                    <div class="col-sm-5 paddingRight5 paddingLeft0">

                                                                        <asp:TextBox runat="server" ID="txtShipDate" CausesValidation="false" Enabled="false" CssClass="form-control"></asp:TextBox>

                                                                    </div>
                                                                    <div class="col-sm-2 paddingLeft0">
                                                                        <asp:LinkButton ID="lbtnShDt" runat="server" CausesValidation="false">
                                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                        <asp:LinkButton ID="lbtnShClr" runat="server" CausesValidation="false" OnClick="lbtnShClr_Click">
                                                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                        <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtShipDate"
                                                                            PopupButtonID="lbtnShDt" Format="dd/MMM/yyyy">
                                                                        </asp:CalendarExtender>
                                                                    </div>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="row">
                                                            <div class="col-sm-5 labelText1">
                                                                Bank Ref #
                                                            </div>
                                                            <div class="col-sm-5 paddingRight5 paddingLeft5">
                                                                <asp:TextBox runat="server" ID="txtBankRefno" CausesValidation="false" AutoPostBack="true" OnTextChanged="txtBankRefno_TextChanged"
                                                                    CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft0">
                                                                <asp:LinkButton ID="lbtnBankRefSearch" runat="server" CausesValidation="false" OnClick="lbtnBankRefSearch_Click">
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
                                                                File #
                                                            </div>
                                                            <div class="col-sm-5 paddingRight5 paddingLeft5">
                                                                <asp:TextBox runat="server" ID="txtfileNo" CausesValidation="false" CssClass="form-control" OnTextChanged="txtfileNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft0">
                                                                <asp:LinkButton ID="lbtnFileSearch" runat="server" CausesValidation="false" OnClick="lbtnFileSearch_Click">
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
                                                                Manual Ref #
                                                            </div>
                                                            <div class="col-sm-5 paddingRight5 paddingLeft5">
                                                              <%--  pls dont increse length becouse sun take 15 charactors--%>
                                                                <asp:TextBox runat="server" ID="txtManualRefNo" CausesValidation="false" CssClass="form-control" MaxLength="15"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft0">
                                                                <asp:LinkButton ID="lbtnMRefSearch" runat="server" CausesValidation="false" OnClick="lbtnMRefSearch_Click">
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
                                                                Bank
                                                            </div>
                                                            <div class="col-sm-5 paddingRight5 paddingLeft5">
                                                                <asp:TextBox runat="server" ReadOnly="false" AutoPostBack="true" ID="txtBank" CausesValidation="false" CssClass="form-control" OnTextChanged="txtBank_TextChanged"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft0">
                                                                <asp:LinkButton ID="lbtnBankSearch" runat="server" CausesValidation="false" OnClick="lbtnBankSearch_Click">
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
                                                                Account #
                                                            </div>
                                                            <div class="col-sm-5 paddingRight5 paddingLeft5">
                                                                <asp:TextBox runat="server" ReadOnly="true" ID="txtAccountNo" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtAccountNo_TextChanged"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft0">
                                                                <asp:LinkButton ID="lbtnAccountSearch" runat="server" CausesValidation="false" OnClick="lbtnAccountSearch_Click">
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
                                                                Branch Code
                                                            </div>
                                                            <div class="col-sm-5 paddingRight5 paddingLeft5">
                                                                <asp:TextBox runat="server" ReadOnly="true" AutoPostBack="true" ID="txtbranch" CausesValidation="false" CssClass="form-control" OnTextChanged="txtBank_TextChanged"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-5 labelText1">
                                                                Facility Limit
                                                            </div>
                                                            <div class="col-sm-5 paddingRight5 paddingLeft5">
                                                                <asp:TextBox runat="server" ReadOnly="true" ID="txtFLimit" CausesValidation="false" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft0">
                                                                <asp:LinkButton ID="lbtnFLimit" runat="server" CausesValidation="false" OnClick="lbtnFLimit_Click">
                                                        <span class="glyphicon glyphicon-tag" aria-hidden="true"></span>
                                                                </asp:LinkButton>
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
                                    <div class="col-sm-7 paddingLeft5">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                <div class="row">
                                                    <div class="col-sm-7">
                                                        PI Details
                                                    </div>
                                                    <div class="col-sm-2">
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:LinkButton ID="lbtnPDelete" runat="server" CausesValidation="false" OnClick="lbtnPDelete_Click" OnClientClick="DeleteConfirm()">
                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true" ></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-sm-1 labelText1">
                                                        PI #
                                                    </div>
                                                    <div class="col-sm-3 paddingRight5">
                                                        <asp:TextBox runat="server" oncopy="return false"
                                                            onpaste="return false"
                                                            oncut="return false" ID="txtPiNo" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnPiNo" runat="server" CausesValidation="false" OnClick="lbtnPiNo_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 ">
                                                        <div class="panel panel-default">
                                                            <div class="panel-body panelscoll height5">
                                                                <asp:GridView ID="grdPid" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" DataKeyNames="IP_PI_NO" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false" OnRowCommand="grdPid_RowCommand" OnRowDeleted="grdPid_RowDeleted" OnRowDeleting="grdPid_RowDeleting" OnRowDataBound="grdPid_RowDataBound">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="" Visible="true">
                                                                            <HeaderTemplate>
                                                                                <asp:CheckBox ID="allchk" runat="server" Width="5px" onclick="CheckAll2(this)"></asp:CheckBox>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="SEC_DE_PI" runat="server" Checked="false" Width="20px" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="SEQ" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="IP_SEQ_NO" runat="server" Text='<%# Bind("IP_SEQ_NO") %>' Width="10px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="PI #">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="IP_PI_NO" runat="server" Text='<%# Bind("IP_PI_NO") %>' Width="95px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="IP_PI_DT" runat="server" Text='<%# Bind("IP_PI_DT", "{0:dd/MMM/yyyy}") %>' Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Trade Term">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="IP_TOP" runat="server" Text='<%# Bind("IP_TOP") %>' Width="20px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Bank">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="IP_BANK_CD" runat="server" Text='<%# Bind("IP_BANK_CD") %>' Width="60px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Account#">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="IP_BANK_ACC_NO" runat="server" Text='<%# Bind("IP_BANK_ACC_NO") %>' Width="40px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Amount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="IP_TOT_AMT" runat="server" Text='<%# Bind("IP_TOT_AMT","{0:#,0.00}") %>' Width="10px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="ASeq" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ifp_is_pi_amd" runat="server" Text='<%# Bind("ifp_is_pi_amd") %>' Width="10px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Active" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="IFP_ACT" runat="server" Text='<%# Bind("IFP_ACT") %>' Width="10px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>


                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height20">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height10">
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-12 paddingRight5">
                                                        <div class="col-sm-8">
                                                            <asp:LinkButton ID="lbtnAmendPI" runat="server" CausesValidation="false" Visible="false" OnClick="lbtnAmendPI_Click">
                                                        <span class="glyphicon glyphicon-tags" aria-hidden="true" ></span> New Amendment PI
                                                            </asp:LinkButton>
                                                        </div>

                                                        <div class="col-sm-2 labelText1">
                                                            Total
                                                        </div>
                                                        <div class="col-sm-2 paddingRight5">
                                                            <asp:TextBox runat="server" ID="txtPTotal" ReadOnly="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtPTotal_TextChanged"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-sm-5 paddingRight5">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">Amendment Details</div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-sm-5">
                                                        <div class="row">
                                                            <div class="col-sm-4 labelText1">
                                                                Date
                                                            </div>
                                                            <asp:Label ID="lblAmdNo" Visible="false" runat="server" Text="Label"></asp:Label>
                                                            <div class="col-sm-6 paddingRight5">
                                                                <asp:TextBox runat="server" ReadOnly="true" ID="txtADate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-2 paddingLeft0">
                                                                <asp:LinkButton ID="lbtnADate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                                <asp:CalendarExtender OnClientDateSelectionChanged="checkDate" ID="CalendarExtender2" runat="server" TargetControlID="txtADate"
                                                                    PopupButtonID="lbtnADate" Format="dd/MMM/yyyy">
                                                                </asp:CalendarExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="row">
                                                            <div class="col-sm-4 labelText1">
                                                                Amount
                                                            </div>
                                                            <div class="col-sm-5 paddingRight5">
                                                                <asp:TextBox runat="server" ID="txtAamount" ReadOnly="true" CausesValidation="false" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1">
                                                                <asp:LinkButton ID="lbtnAadd" Visible="false" CausesValidation="false" runat="server" CssClass="floatRight">
                                        <span class="glyphicon glyphicon-triangle-bottom" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>


                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="panel panel-default">
                                                            <div class="panel-body panelscoll2">
                                                                <asp:GridView ID="grdADetails" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Amendment #">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ANo" runat="server" Text='<%# Bind("ANo") %>' Width="10px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Date" runat="server" Text='<%# Bind("Date", "{0:dd/MMM/yyyy}") %>' Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Amount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Amount" runat="server" Text='<%# Bind("Amount","{0:#,0.00}") %>' Width="50px"></asp:Label>
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
                                    <div class="col-sm-3 paddingLeft5 paddingRight5">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">Insurance Details  </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-sm-4 labelText1">
                                                        Company
                                                    </div>
                                                    <div class="col-sm-5 paddingRight5">
                                                        <asp:TextBox runat="server" ID="txtICompany" CausesValidation="false" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnInSearch" runat="server" CausesValidation="false" OnClick="lbtnInSearch_Click">
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
                                                        Policy #
                                                    </div>
                                                    <div class="col-sm-5 paddingRight5">
                                                        <asp:TextBox runat="server" oncopy="return false"
                                                            onpaste="return false"
                                                            oncut="return false" ID="txtIPloicyNo" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-4 labelText1">
                                                        Sum of Interest
                                                    </div>
                                                    <div class="col-sm-5 paddingRight5">
                                                        <asp:TextBox runat="server" oncopy="return false"
                                                            onpaste="return false"
                                                            oncut="return false" onkeypress="return isNumberKey(event)" ID="txtIsumInterest" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtIsumInterest_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        LKR
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-4 labelText1">
                                                        Premium
                                                    </div>
                                                    <div class="col-sm-5 paddingRight5">
                                                        <asp:TextBox runat="server" oncopy="return false"
                                                            onpaste="return false"
                                                            oncut="return false" onkeypress="return isNumberKey(event)" ID="txtIPremium" CausesValidation="false" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        LKR
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-4 labelText1">
                                                        Policy Date
                                                    </div>
                                                    <div class="col-sm-5 paddingRight5">
                                                        <asp:TextBox runat="server" ID="txtIpolicyDate" CausesValidation="false" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnIpolicyDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtIpolicyDate"
                                                            PopupButtonID="lbtnIpolicyDate" Format="dd/MMM/yyyy">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height30">
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-12 height30">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height30">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height10">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4 paddingLeft5">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                <div class="row">
                                                    <div class="col-sm-7">
                                                        Cost Involvement Details
                                                    </div>
                                                    <div class="col-sm-2">
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:LinkButton ID="lbtnCDelete" runat="server" CausesValidation="false" OnClick="lbtnCDelete_Click" OnClientClick="DeleteConfirm()">
                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-sm-4">
                                                        Cost Type                         
                                                    </div>
                                                    <div class="col-sm-3 paddingRight0">
                                                        Supplier Amt|<asp:Label ID="lblSCurrancy" ForeColor="Red" runat="server" Text="Label"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-4 ">
                                                        Company Amt|<asp:Label ID="lblCompanyCurrancy" ForeColor="Red" runat="server" Text="Label"></asp:Label>
                                                    </div>

                                                </div>
                                                <asp:Label ID="lblCostLineNo" runat="server" Visible="false" Text="Label"></asp:Label>
                                                <div class="row">
                                                    <div class="col-sm-4 paddingRight0">
                                                        <asp:TextBox runat="server" ID="txtCost" CausesValidation="false" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 Lwidth paddingLeft0">
                                                        <asp:LinkButton ID="lbtncostSearch" runat="server" OnClick="lbtncostSearch_Click">
                                     <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div class="col-sm-3 paddingRight0 paddingLeft0">
                                                        <asp:TextBox runat="server" ID="txtAmount" oncopy="return false"
                                                            onpaste="return false"
                                                            oncut="return false" onkeypress="return isNumberKey(event)" CausesValidation="false" Style="text-align: right" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtAmount_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-3 paddingRight0 paddingLeft0 ">
                                                        <asp:TextBox runat="server" ID="txtLAmount" Style="text-align: right" ReadOnly="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 Lwidth paddingLeft0">
                                                        <asp:LinkButton ID="lbtnCAdd" runat="server" OnClick="lbtnCAdd_Click">
                                     <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>

                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="panel panel-default">
                                                            <div class="panel-body  panelscoll1">
                                                                <div class="row">
                                                                    <div class="col-sm-12 ">
                                                                        <asp:GridView ID="grdCDetails" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" ShowHeader="False" runat="server" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                                                            <Columns>
                                                                                <%-- <asp:CommandField ShowDeleteButton="True" ButtonType="Button"   />--%>
                                                                                <asp:TemplateField HeaderText="Delete" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <%--<asp:LinkButton ID="lbtnPDelete" runat="server" CausesValidation="false">
                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                                                            </asp:LinkButton>--%>
                                                                                        <asp:CheckBox ID="SEC_DEF" runat="server" Checked="false" Width="20px" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField ShowHeader="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="CType" runat="server" Text='<%# Bind("CType") %>' Width="10px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ShowHeader="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Amout" runat="server" Text='<%# Bind("Amount","{0:#,0.00}") %>' Width="10px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ShowHeader="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="AmountL" runat="server" Text='<%# Bind("AmountL","{0:#,0.00}") %>' Width="10px"></asp:Label>
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
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height30">
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-12">

                                                        <div class="col-sm-3 labelText1">
                                                            Exchange Rate
                                                        </div>
                                                        <div class="col-sm-2 paddingRight5">
                                                            <asp:TextBox runat="server" ReadOnly="true" ID="txtERate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 labelText1">
                                                            Total
                                                        </div>
                                                        <div class="col-sm-3 paddingRight5">
                                                            <asp:TextBox runat="server" onkeypress="filterDigits(event)" ID="txtcTotal" ReadOnly="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-3 paddingRight5">
                                                            <asp:TextBox runat="server" onkeypress="filterDigits(event)" ID="txtCLKRTotal" ReadOnly="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                        </div>


                                                    </div>
                                                </div>



                                            </div>
                                        </div>
                                    </div>




                                </div>
                                <%--  <div class="row">
                        <div class="col-sm-8">
                            <div class="panel panel-default">
                                <div class="panel-heading">Bank credit facility</div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-2">
                                            <div class="row">
                                                <div class="col-sm-5 labelText1">
                                                    Bank
                                                </div>
                                                <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                    <asp:TextBox runat="server" ID="TextBox1" CausesValidation="false" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                             <div class="row">
                                                <div class="col-sm-5 labelText1">
                                                    Account #
                                                </div>
                                                <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                    <asp:TextBox runat="server" ID="TextBox2" CausesValidation="false" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                             <div class="row">
                                                <div class="col-sm-5 labelText1">
                                                    Facility Limit
                                                </div>
                                                <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                    <asp:TextBox runat="server" ID="TextBox3" CausesValidation="false" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                             <div class="row">
                                                <div class="col-sm-5 labelText1">
                                                   Utility Value
                                                </div>
                                                <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                    <asp:TextBox runat="server" ID="TextBox4" CausesValidation="false" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="panel panel-default">
                                <div class="panel-heading">Shipping Details</div>
                                <div class="panel-body">
                                      <div class="row">
                                        <div class="col-sm-5">
                                            <div class="row">
                                                <div class="col-sm-5 labelText1">
                                                    Shipper
                                                </div>
                                                <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                    <asp:TextBox runat="server" ID="TextBox5" CausesValidation="false" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-sm-5">
                                             <div class="row">
                                                <div class="col-sm-5 labelText1">
                                                   Trade Term
                                                </div>
                                                <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                    <asp:TextBox runat="server" ID="TextBox6" CausesValidation="false" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>
                                      </div>
                                </div>
                            </div>
                        </div>
                    </div>--%>
                                     </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

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


    <%-- Style="display: none"--%>
    <asp:Panel runat="server" ID="pnlpopup" DefaultButton="lbtnSearch">
        <div runat="server" id="test" class="panel panel-default height400 width700">

            <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
            <%--<asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>--%>
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
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdResult" CausesValidation="false" OnPageIndexChanging="grdResult_PageIndexChanging" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="12" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResult_SelectedIndexChanged">
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

    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="SupplierPopoup" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="pnlpopupsuplier" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpopupsuplier" DefaultButton="lbtnSearch">
        <div runat="server" id="Div1" class="panel panel-default Mheight2">
            <div class="col-sm-12">
                <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                <%--<asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>--%>
                <div class="panel panel-default">
                    <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>

                            <div class="panel-heading">

                                <%--<span>Commen Search</span>--%>
                                <div class="col-sm-10">
                                </div>
                                <div class="col-sm-2">
                                    <asp:LinkButton ID="LinkButton1" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 height5">
                                </div>
                            </div>
                            <div class="panel-body">
                                <div class="col-sm-12">


                                    <div class="row">
                                        <div class="col-sm-5 labelText1">
                                            Supplier code
                                        </div>
                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                            <asp:TextBox runat="server" ReadOnly="true" ID="txtsuppcode" CausesValidation="false" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-5 labelText1">
                                            Supplier Name
                                        </div>
                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                            <asp:TextBox runat="server" ReadOnly="true" ID="txtSName" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-5 labelText1">
                                            Trade Terms
                                        </div>
                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                            <asp:TextBox runat="server" ReadOnly="true" ID="txtTradeT" CausesValidation="false" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
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


    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="ItemPopup" runat="server" Enabled="True" TargetControlID="Button2"
                PopupControlID="pnlpopupItem" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlpopupItem" DefaultButton="lbtnPiItemSearch">

                <div runat="server" id="Div2" class="panel panel-default  height400 width700">
                    <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
                    <%--<asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>--%>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="LinkButton2" runat="server">
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
                                    <div class="col-sm-12">

                                        <div class="col-sm-2 labelText1">
                                            Search by PI #
                                        </div>
                                        <div class="col-sm-4 paddingRight5">
                                            <asp:TextBox ID="txtPiItem" CausesValidation="false" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtPiItem_TextChanged"></asp:TextBox>
                                        </div>
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy5" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnPiItemSearch" runat="server" OnClick="lbtnPiItemSearch_Click">
                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtPiItem" />
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
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdItemResult" CausesValidation="false" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" AllowPaging="True">
                                                    <Columns>
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

    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button4" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="KitItemPopup" runat="server" Enabled="True" TargetControlID="Button4"
                PopupControlID="pnlpopupKitItem" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlpopupKitItem" DefaultButton="lbtnKitSearch">
                <div runat="server" id="Div3" class="panel panel-default height400 width700">

                    <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
                    <%--<asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>--%>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="LinkButton3" runat="server">
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
                                    <div class="col-sm-12">

                                        <div class="col-sm-2 labelText1">
                                            Search by PI #
                                        </div>
                                        <div class="col-sm-4 paddingRight5">
                                            <asp:TextBox ID="txtKitItemSearch" CausesValidation="false" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtKitItemSearch_TextChanged"></asp:TextBox>
                                        </div>
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy4" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                            <ContentTemplate>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnKitSearch" runat="server" OnClick="lbtnKitSearch_Click">
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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdKitItem" CausesValidation="false" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" AllowPaging="True">
                                                    <Columns>
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


    <asp:UpdatePanel ID="UpdatePanel16" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button5" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="facilitypopup" runat="server" Enabled="True" TargetControlID="Button5"
                PopupControlID="pnlPLimit" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>


    <%-- Style="display: none"--%>
    <asp:UpdatePanel ID="UpdatePanel17" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlPLimit" DefaultButton="lbtnSearch">

                <div runat="server" id="Div4" class="panel panel-default height400 width700">

                    <asp:Label ID="Label4" runat="server" Text="Label" Visible="false"></asp:Label>
                    <%--<asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>--%>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="LinkButton4" runat="server">
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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy12" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdFLimit" CausesValidation="false" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResult_SelectedIndexChanged">
                                                    <Columns>
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

    <%--payment Method--%>
    <asp:UpdatePanel ID="UpdatePanel18" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button6" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="paypopup" runat="server" Enabled="True" TargetControlID="Button6"
                PopupControlID="pnlPaymethod" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlPaymethod" DefaultButton="lbtnAddPayAmount">

                <div runat="server" id="Div5" class="panel panel-default height400 width700">
                    <asp:Label ID="lblPaylineno" runat="server" Text="0" Visible="false"></asp:Label>
                    <asp:Label ID="Label5" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="lbltype" runat="server" Text="Label" Visible="false"></asp:Label>


                    <div class="panel panel-default">

                        <div class="panel-heading">
                            <asp:LinkButton ID="LinkButton5" runat="server">
                             <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <%--<span>Commen Search</span>--%>
                            <div class="col-sm-11">
                                <asp:Label ID="lblerror" ForeColor="Red" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12 paddingLeft0 paddingRight0">
                                <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-sm-2 labelText1">
                                                    Pay Type
                                                </div>
                                                <div class="col-sm-2 paddingRight0">
                                                    <asp:DropDownList ID="ddlPayType" CausesValidation="false" runat="server" CssClass="form-control">
                                                        <asp:ListItem Text="Cash" Value="0"> </asp:ListItem>
                                                        <asp:ListItem Text="Cheque" Value="1"> </asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2 labelText1">
                                                    Amount Type
                                                </div>
                                                <div class="col-sm-2 paddingRight5">
                                                    <asp:DropDownList ID="ddlAmountType" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlAmountType_SelectedIndexChanged">
                                                        <asp:ListItem Text="--Select--" Value="0"> </asp:ListItem>
                                                        <asp:ListItem Text="Rate" Value="1"> </asp:ListItem>
                                                        <asp:ListItem Text="Amount" Value="2"> </asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-3 paddingRight5">
                                                    <asp:TextBox runat="server" oncopy="return false"
                                                        onpaste="return false"
                                                        oncut="return false" onkeypress="return isNumberKey(event)" ID="txtRate" Visible="false" CausesValidation="false" CssClass="form-control"></asp:TextBox>

                                                </div>
                                                <div class="col-sm-1 Lwidth paddingLeft0">
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                                <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-sm-12 height20">
                                                <div class="col-sm-2 labelText1">
                                                </div>
                                                <div class="col-sm-2 paddingRight0">
                                                </div>
                                                <div class="col-sm-2 labelText1">
                                                </div>
                                                <div class="col-sm-2 paddingRight5">
                                                    <asp:TextBox runat="server" oncopy="return false"
                                                        onpaste="return false"
                                                        oncut="return false" onkeypress="return isNumberKey(event)" AutoPostBack="true" ID="txtPayAmount" CausesValidation="false" CssClass="form-control" OnTextChanged="txtPayAmount_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-3 paddingRight5">
                                                    <asp:TextBox runat="server" oncopy="return false"
                                                        onpaste="return false"
                                                        oncut="return false" onkeypress="return isNumberKey(event)" ID="txtPayAmountLKR" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                                    <ContentTemplate>
                                                        <div class="col-sm-1 Lwidth paddingLeft0">
                                                            <asp:LinkButton ID="lbtnAddPayAmount" runat="server" OnClick="lbtnAddPayAmount_Click">
                                     <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>


                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>


                                <div class="row">
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height20">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 ">
                                        <div class="col-sm-2 labelText1">
                                            Settle Amount
                                        </div>
                                        <div class="col-sm-2 paddingRight0">
                                            <asp:Label ID="lblSamount" runat="server" Text="" ForeColor="Green"></asp:Label>
                                        </div>
                                        <div class="col-sm-2 labelText1">
                                            Complete Amount
                                        </div>
                                        <div class="col-sm-2 paddingRight5">
                                            <asp:Label ID="lblCAmount" runat="server" Text="0" Visible="false" ForeColor="Green"></asp:Label>
                                            <asp:Label ID="lblCPAmount" runat="server" Text="0" Visible="false" ForeColor="Green"></asp:Label>
                                        </div>
                                        <div class="col-sm-2 paddingRight0">
                                            Available Amount
                                        </div>
                                        <div class="col-sm-2 paddingRight0 paddingLeft0">
                                            <asp:Label ID="lblAamount" runat="server" Text="" ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy8" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdPayAmount" AutoGenerateColumns="false" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" ShowHeader="true" runat="server" GridLines="None" CssClass="table table-hover table-striped" PagerStyle-CssClass="cssPager">
                                                    <Columns>
                                                        <asp:TemplateField Visible="true">
                                                            <HeaderTemplate>
                                                                <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                                    <asp:CheckBox ID="allchk" runat="server" Width="5px" onclick="CheckAll(this)"></asp:CheckBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingRight0">

                                                                    <asp:LinkButton ID="lbtnPayDelete" OnClick="lbtnPayDelete_Click" runat="server" CausesValidation="false" OnClientClick="DeleteConfirm()">
                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                                                    </asp:LinkButton>

                                                                </div>

                                                            </HeaderTemplate>
                                                            <ItemTemplate>

                                                                <asp:CheckBox ID="SEC_DEPayment" runat="server" Checked="false" Width="2px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ShowHeader="true" HeaderText="Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="PType" runat="server" Text='<%# Bind("PType") %>' Width="2px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ShowHeader="false" HeaderText="Amount Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="AmountType" runat="server" Text='<%# Bind("AmountType") %>' Width="10px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ShowHeader="false" HeaderText="Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Amount" runat="server" Text='<%# Bind("Amount") %>' Width="10px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ShowHeader="false" HeaderText="Company Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="IFY_AMT_DEAL" runat="server" Text='<%# Bind("IFY_AMT_DEAL") %>' Width="10px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ShowHeader="false" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="IFY_ANAL_2" runat="server" Text='<%# Bind("IFY_ANAL_2") %>' Width="10px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
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




    <asp:UpdatePanel ID="UpdatePanel25" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button7" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="SbuPopup" runat="server" Enabled="True" TargetControlID="Button7"
                PopupControlID="pnlSBU" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel ID="pnlSBU" runat="server" align="center">
        <asp:Label ID="Label6" runat="server" Text="Label" Visible="false"></asp:Label>
        <asp:ScriptManagerProxy ID="ScriptManagerProxy11" runat="server"></asp:ScriptManagerProxy>
        <asp:UpdatePanel ID="UpdatePanel26" runat="server">
            <ContentTemplate>
                <div class="panel panel-danger">
                    <div class="panel-heading">
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
                            <div class="col-sm-4">
                            </div>
                            <div class="col-sm-4">
                                <asp:Button ID="btnSbu" runat="server" Text="Ok" CausesValidation="false" class="btn btn-primary" OnClick="btnSbu_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>



</asp:Content>
