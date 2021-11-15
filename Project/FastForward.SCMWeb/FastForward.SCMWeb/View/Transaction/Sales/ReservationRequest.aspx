<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="ReservationRequest.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Sales.ReservationRequest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/jquery.toastmessage.js"></script>
     <script type="text/javascript">
        function DeleteConfirm() {
            var selectedvalue = confirm("Do you want to delete data?");
            if (selectedvalue) {
                document.getElementById('<%=txtDeleteconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtDeleteconformmessageValue.ClientID %>').value = "No";
            }
        };
        function checkDate(sender, args) {
            if ((sender._selectedDate < new Date(new Date().toDateString()))) {
                $().toastmessage('showToast', {
                    text: "You cannot select a day earlier than today!",
                    sticky: false,
                    stayTime: 1500000,
                    position: 'top-center',
                    type: 'warning',
                    closeText: ''
                    //,
                    //close: function () {
                    //    console.log("toast is closed ...");
                    //}

                });
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
        function checkDate2(sender, args) {

            if ((sender._selectedDate < new Date(new Date().toDateString()))) {
                $().toastmessage('showToast', {
                    text: "You cannot select a day earlier than today!",
                    sticky: false,
                    stayTime: 1500000,
                    position: 'top-center',
                    type: 'warning',
                    closeText: ''
                    //,
                    //close: function () {
                    //    console.log("toast is closed ...");
                    //}

                });
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
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

        function ConfirmClear() {
            var selectedvalueOrd = confirm("Do you want to clear all details ?");
            if (selectedvalueOrd) {
                document.getElementById('<%=hdnClear.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdnClear.ClientID %>').value = "No";
            }
        };

        function ConfirmSave() {
            var selectedvalueOrdPlace = confirm("Do you want to save ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=hdnSave.ClientID %>').value = "Yes";
            }
            else {
                document.getElementById('<%=hdnSave.ClientID %>').value = "No";
            }
        };

        function ConfirmUpdate() {
            var selectedvalueOrdPlace = confirm("Do you want to update ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=hdnUpdate.ClientID %>').value = "Yes";
            }
            else {
                document.getElementById('<%=hdnUpdate.ClientID %>').value = "No";
            }
        };

        function ConfirmApprove() {
            var selectedvalueOrdPlace = confirm("Do you want to approve ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=hdnApprove.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdnApprove.ClientID %>').value = "No";
            }
        };

        function ConfirmCancel() {
            var selectedvalueOrdPlace = confirm("Do you want to cancel ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=hdnCancel.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdnCancel.ClientID %>').value = "No";
            }
        };
        function Enable() {
            return;
        }

    </script>
    <script type="text/javascript">
        function showStickySuccessToast(value) {
            $().toastmessage('showToast', {
                text: value,
                sticky: true,
                stayTime: 1500000,
                position: 'top-center',
                type: 'success',
                closeText: ''
                //,
                //close: function () {
                //    console.log("toast is closed ...");
                //}
            });
        }
        function showStickyNoticeToast(value) {
            $().toastmessage('showToast', {
                text: value,
                sticky: true,
                stayTime: 1500000,
                position: 'top-center',
                type: 'notice',
                closeText: ''
                //,
                //close: function () { console.log("toast is closed ..."); }
            });
        }
        function showStickyWarningToast(value) {
            $().toastmessage('showToast', {
                text: value,
                sticky: false,
                stayTime: 1500000,
                position: 'top-center',
                type: 'warning',
                closeText: ''
                //,
                //close: function () {
                //    console.log("toast is closed ...");
                //}

            });

        }
        function showStickyErrorToast(value) {

            $().toastmessage('showToast', {
                text: value,
                sticky: false,
                stayTime: 1500000,
                position: 'top-center',
                type: 'error',
                closeText: ''
                //,
                //close: function () {
                //    console.log("toast is closed ...");
                //}
            });
        }
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

        .panel {
            padding-top: 0px;
            padding-bottom: 0px;
            margin-bottom: 0px;
            margin-top: 0px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
     <asp:UpdateProgress ID="UpdateProgress3" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="totform">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait1aa112" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait1aa112" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="totform">
        <ContentTemplate>
            <asp:HiddenField ID="hdnSave" runat="server" />
            <asp:HiddenField ID="hdnUpdate" runat="server" />
            <asp:HiddenField ID="hdnApprove" runat="server" />
            <asp:HiddenField ID="hdnCancel" runat="server" />
            <asp:HiddenField ID="hdnClear" runat="server" />
            <asp:HiddenField ID="txtDeleteconformmessageValue" runat="server" />
            <div class="panel panel-default marginLeftRight5 paddingbottom0">
                <div class="panel-body paddingtopbottom0">
                    <div class="row">
                        <div class="col-sm-7  buttonrow">
                            <div id="divWarning" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">
                                <strong>Warning!</strong>
                                <asp:Label ID="lblWarning" runat="server"></asp:Label>
                                <asp:LinkButton ID="lbtnlWarning" runat="server" CausesValidation="false" CssClass="floatRight">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </div>
                            <div id="divSuccess" runat="server" visible="false" class="alert alert-success alert-dismissible" role="alert">
                                <strong>Success!</strong>
                                <asp:Label ID="lblSuccess" runat="server"></asp:Label>
                                <asp:LinkButton ID="lbtnSuccess" runat="server" CausesValidation="false" CssClass="floatRight">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </div>
                            <div id="divAlert" runat="server" visible="false" class="alert alert-info alert-dismissible" role="alert">
                                <strong>Alert!</strong>
                                <asp:Label ID="lblAlert" runat="server"></asp:Label>
                                <asp:LinkButton ID="lbtnAlert" runat="server" CausesValidation="false" CssClass="floatRight">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-sm-4  buttonRow">
                            <div class="col-sm-3 paddingRight0">
                                <asp:LinkButton ID="lbtnSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmSave()" OnClick="lbtnSave_Click">
                            <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                </asp:LinkButton>
                            </div>
                            <div class="col-sm-3 paddingRight0">
                                <asp:LinkButton ID="lbtnUpdate" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmUpdate()" OnClick="lbtnUpdate_Click">
                            <span class="glyphicon glyphicon-edit" aria-hidden="true"></span>Update
                                </asp:LinkButton>
                            </div>
                            <div class="col-sm-3 paddingRight0">
                                <asp:LinkButton ID="lbtnCancel" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmCancel()" OnClick="lbtnCancel_Click">
                            <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>Cancel
                                </asp:LinkButton>
                            </div>
                            <div class="col-sm-3 paddingRight0">
                                <asp:LinkButton ID="lbtnClear" runat="server" CssClass="floatRight" OnClientClick="ConfirmClear()" OnClick="lbtnClear_Click">
                            <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-sm-1  buttonRow paddingLeft0">
                            <div class="col-sm-12 paddingLeft5">
                                <asp:LinkButton ID="lbtnApprove" runat="server" OnClientClick="ConfirmApprove()" OnClick="lbtnApprove_Click">
                                    <span class="glyphicon glyphicon-thumbs-up fontsize18" aria-hidden="true"></span>Approve
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-default">
                                <div class="panel-heading"> <strong>Customer Reservation Request</strong> </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-7">
                                            <div class="col-sm-4">
                                                <div class="row">
                                                    <div class="col-sm-6 labelText1">
                                                        Request Reason
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlRequestReason" runat="server" AutoPostBack="true" class="form-control" OnSelectedIndexChanged="ddlRequestReason_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="row">
                                                    <div class="col-sm-6 labelText1">
                                                        Request Type
                                                    </div>
                                                    <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                        <asp:DropDownList ID="ddlRequestType" CausesValidation="false" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="row">
                                                    <div class="col-sm-4 labelText1">
                                                        Customer
                                                    </div>
                                                    <div class="col-sm-6 paddingRight5">
                                                        <asp:TextBox ID="txtCustomer" CausesValidation="false" runat="server" MaxLength="20" class="form-control" AutoPostBack="true" OnTextChanged="txtCustomer_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 ">
                                                        <asp:LinkButton ID="lbtnCustomer" runat="server" OnClick="lbtnCustomer_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
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
                                                    <div class="col-sm-2 labelText1">
                                                        Remarks 
                                                    </div>
                                                    <div class="col-sm-10 paddingRight5">
                                                        <asp:TextBox ID="txtRemarks" CausesValidation="false" TextMode="MultiLine" Rows="3" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-5 paddingLeft0">
                                            <div class="col-sm-6">
                                                <div class="row">
                                                    <div class="col-sm-4 labelText1">
                                                        Date
                                                    </div>
                                                    <div class="col-sm-6 paddingRight5">
                                                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control"
                                                            Format="dd/MMM/yyyy" Enabled="false"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0" style="width:28px">
                                                        <asp:LinkButton ID="lbtnDate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true" ></span>
                                                        </asp:LinkButton >
                                                        <asp:CalendarExtender ID="CalendarExtender3" OnClientDateSelectionChanged="checkDate" runat="server" TargetControlID="txtDate"
                                                            PopupButtonID="lbtnDate" Format="dd/MMM/yyyy">
                                                        </asp:CalendarExtender >
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="row">
                                                    <div class="col-sm-4 labelText1">
                                                        Request #
                                                    </div>
                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                        <asp:TextBox ID="txtRequestNo" CausesValidation="false" runat="server" AutoPostBack="true" class="form-control" OnTextChanged="txtRequestNo_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtRequestNo" runat="server" OnClick="lbtRequestNo_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="row">
                                                    <div class="col-sm-4 labelText1">
                                                        Expected date 
                                                    </div>
                                                    <div class="col-sm-6 paddingRight5">
                                                        <asp:TextBox ID="txtExpectedDate" runat="server" CssClass="form-control"
                                                            Format="dd/MMM/yyyy" Enabled="false"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnExpectedDate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                        <asp:CalendarExtender ID="CalendarExtender1" OnClientDateSelectionChanged="checkDate" runat="server" TargetControlID="txtExpectedDate"
                                                            PopupButtonID="lbtnExpectedDate" Format="dd/MMM/yyyy">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="row">
                                                    <div class="col-sm-4 labelText1">
                                                        Status
                                                    </div>
                                                    <div class="col-sm-8 paddingLeft0">
                                                        <asp:TextBox ID="txtStatus" CausesValidation="false" runat="server" ReadOnly="true" MaxLength="30" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="row">
                                                  
                                                        <div class="col-sm-4 labelText1">
                                                            Sales Exe.
                                                        </div>
                                                        <div class="col-sm-5 ">
                                                            <asp:TextBox ID="txtexcutive" runat="server" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtexcutive_TextChanged"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-2 paddingLeft0">
                                                            <asp:LinkButton ID="lbtnEx" runat="server" CausesValidation="false" OnClick="lbtnEx_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                       
                                                   
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="row">
                                                    <asp:Label ID="lblSalesEx" runat="server" ForeColor="#A513D0"></asp:Label>
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
                                <div class="panel-heading">Request Details</div>
                                <div class="row">
                                    <div class="panel-body">

                                        <div class="col-sm-12" id="ItemAdd" runat="server">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    <div class="row">
                                                        <div class="col-sm-4">
                                                            <div class="row">
                                                                <div class="col-sm-3 labelText1">
                                                                    Item
                                                                </div>
                                                                <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                    <asp:TextBox ID="txtItem" CausesValidation="false" runat="server" MaxLength="30" class="form-control" AutoPostBack="true" OnTextChanged="txtItem_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:LinkButton ID="lbtnItem" runat="server" OnClick="lbtnItem_Click">
                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0">
                                                                    <asp:LinkButton ID="lbtnItemStock" runat="server" OnClick="lbtnItemStock_Click">
                                                                                    <span class="glyphicon glyphicon-list-alt" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-8">
                                                            <div class="row">
                                                                <div class="col-sm-4">
                                                                    <div class="row">
                                                                        <div class="col-sm-4 labelText1">
                                                                            Status
                                                                        </div>
                                                                        <div class="col-sm-8 paddingLeft0">
                                                                            <asp:DropDownList ID="ddlStatus" runat="server" class="form-control">
                                                                                <asp:ListItem Text=" - - Select - - " Value="0"></asp:ListItem>
                                                                                <asp:ListItem Text="GOOD" Value="GOD"></asp:ListItem>
                                                                                <asp:ListItem Text="GOOD LP" Value="GDLP"></asp:ListItem>
                                                                                <asp:ListItem Text="AGE" Value="AGE"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="row">
                                                                        <div class="col-sm-4 labelText1">
                                                                            Location
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight5">
                                                                            <asp:TextBox ID="txtLocation" CausesValidation="false" runat="server" MaxLength="30" class="form-control" AutoPostBack="true"
                                                                                OnTextChanged="txtLocation_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0">
                                                                            <asp:LinkButton ID="lbtnLocation" runat="server" OnClick="lbtnLocation_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3">
                                                                    <div class="row">
                                                                        <div class="col-sm-3 labelText1">
                                                                            Qty
                                                                        </div>
                                                                        <div class="col-sm-9">
                                                                            <asp:TextBox ID="txtQty"  oncopy="return false"
                                                onpaste="return false"
                                                oncut="return false" CausesValidation="false" AutoPostBack="true" runat="server" onkeydown="return jsDecimals(event);" MaxLength="10" class="form-control textAlignRight"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-1">
                                                                    <div class="row">
                                                                        <div class="col-sm-6 paddingRight5 paddingLeft5">
                                                                            <asp:LinkButton ID="lbtnAdd" runat="server" OnClick="lbtnAdd_Click">
                                                                                <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                        <div class="col-sm-6 paddingRight5 paddingLeft5">
                                                                            <asp:LinkButton ID="lbtnClearItem" runat="server" OnClick="lbtnClearItem_Click">
                                                                                <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>
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
                                        <div class="col-sm-12">
                                            <div class="panel panel-default">
                                                <div class="row">
                                                    <div class="panel-body">
                                                        <div class="col-sm-12">
                                                            <div class="col-sm-1 labelText1">Description</div>
                                                            <div class="col-sm-2 labelText1">
                                                                <asp:Label ID="lblDescription" runat="server" Text="" CssClass="Color1 fontWeight900"></asp:Label>
                                                            </div>
                                                            <div class="col-sm-1 labelText1">Model</div>
                                                            <div class="col-sm-2 labelText1">
                                                                <asp:Label ID="lblModel" runat="server" Text="" CssClass="Color1 fontWeight900"></asp:Label>
                                                            </div>
                                                            <div class="col-sm-1 labelText1">Brand</div>
                                                            <div class="col-sm-2 labelText1">
                                                                <asp:Label ID="lblBrand" runat="server" Text="" CssClass="Color1 fontWeight900"></asp:Label>
                                                            </div>
                                                            <div class="col-sm-1 labelText1">Part #</div>
                                                            <div class="col-sm-2 labelText1">
                                                                <asp:Label ID="lblPartNo" runat="server" Text="" CssClass="Color1 fontWeight900"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 GridScroll230">
                                            <asp:GridView ID="grdItem" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnGrdSerial" runat="server" CausesValidation="false" OnClick="lbtnGrdSerial_Click">
                                                                <span aria-hidden="true" class="glyphicon glyphicon-th-list"></span>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Code">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblItemCodeEdit" runat="server" Text='<%# Bind("ITRI_ITM_CD") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("ITRI_ITM_CD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="15%" />
                                                        <ItemStyle Width="15%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblItemDescriptionEdit" runat="server" Text='<%# Bind("DESCRIPTION") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemDescription" runat="server" Text='<%# Bind("DESCRIPTION") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="25%" />
                                                        <ItemStyle Width="25%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Model">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblModelEdit" runat="server" Text='<%# Bind("MODEL") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblModel" runat="server" Text='<%# Bind("MODEL") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="15%" />
                                                        <ItemStyle Width="15%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblUnitPriceEdit" runat="server" Text='<%# Bind("MIS_DESC") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUnitPrice" runat="server" Text='<%# Bind("MIS_DESC") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="10%" />
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Location">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblVlueEdit" runat="server" Text='<%# Bind("ITRI_LOC") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVlue" runat="server" Text='<%# Bind("ITRI_LOC") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="10%" />
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qty">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtQtyEdit" oncopy="return false"
                                                onpaste="return false"
                                                oncut="return false" runat="server" class="form-control textAlignRight" onkeydown="return jsDecimals(event);" Text='<%# Bind("ITRI_QTY", "{0:N2}") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQty" runat="server" Text='<%# Bind("ITRI_QTY", "{0:N2}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="textAlignRight" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="linno" ShowHeader="False" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemlineno" runat="server" Text='<%# Bind("ITRI_LINE_NO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="25%" />
                                                        <ItemStyle Width="25%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ShowHeader="False">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnGrdItemEdit" CausesValidation="false" runat="server" OnClick="lbtnGrdItemEdit_Click">
                                                                <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:LinkButton ID="lbtnGrdItemUpdate" runat="server" CausesValidation="false" CommandName="Update" OnClick="lbtnGrdItemUpdate_Click">
                                                                <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                            &nbsp;<asp:LinkButton ID="lbtnGrdItemCancel" runat="server" CausesValidation="false" CommandName="Cancel" OnClick="lbtnGrdItemCancel_Click">
                                                                <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnGrdItemDalete" runat="server" Visible='<%#GetDaleteVisibility()%>' CausesValidation="false" OnClientClick="DeleteConfirm()" OnClick="lbtnGrdItemDalete_Click">
                                                                <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
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
                        <div class="col-sm-12">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-8">
                                            <div class="col-sm-12">
                                                <div class="row">
                                                    <div class="col-sm-1 labelText1">
                                                    </div>
                                                    <div class="col-sm-11 paddingRight5">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="col-sm-12">
                                                <div class="row">
                                                    <div class="col-sm-4 labelText1">
                                                        Total Qty
                                                    </div>
                                                    <div class="col-sm-5 paddingRight5">
                                                        <asp:TextBox ID="txtTotalQty" CausesValidation="false" ReadOnly="true" runat="server" class="form-control textAlignRight"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-4 labelText1">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-1">
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
    <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
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

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnSerial" runat="server" Text="" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupSerial" runat="server" Enabled="True" TargetControlID="btnSerial"
                PopupControlID="pnlSerial" CancelControlID="lbtnSerialClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlSerial" DefaultButton="lbtnSearch">
                <div runat="server" id="Div1" class="panel panel-default height400 width950">
                    <asp:Label ID="lblSerialvalue" runat="server" Text="" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="lbtnSerialClose" runat="server">
                                <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <div class="col-sm-11">
                                Serial Advanced Search
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-7 paddingRight0">
                                    <div runat="server" id="Div2" class="panel panel-default">
                                        <%--<asp:Label ID="lblvalue1" runat="server" Text="Label" Visible="false"></asp:Label>--%>

                                        <div class="panel-body height400">
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
                                                    <div class="col-sm-3 paddingRight5">
                                                        <asp:DropDownList ID="ddlSearchbykey1" runat="server" class="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2 labelText1">
                                                        Search by word
                                                    </div>
                                                    <div class="col-sm-4 paddingRight5">
                                                        <asp:TextBox ID="txtSearchbyword1" CausesValidation="true" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbyword_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnSearch1" runat="server" OnClick="lbtnSearch_Click">
                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
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

                                                <div class="col-sm-12  ">
                                                    <asp:GridView ID="grdResult1" CausesValidation="false" runat="server" OnSelectedIndexChanged="grdResult_SelectedIndexChanged" AllowPaging="True" OnPageIndexChanging="grdResult1_PageIndexChanging" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager">
                                                        <Columns>
                                                            <asp:ButtonField Text="select" CommandName="Select" ItemStyle-Width="100" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-sm-5">
                                    <div class="panel panel-default ">
                                        <div class="panel-body height400">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="grdSerial" CausesValidation="false" runat="server" GridLines="None" CssClass="table table-hover table-striped"
                                                        AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                        <Columns>
                                                            <asp:BoundField DataField="ITRS_SER_1" HeaderText="Serial 1" ReadOnly="true" />
                                                            <asp:BoundField DataField="ITRS_SER_2" HeaderText="Serial 2" ReadOnly="true" NullDisplayText="N/A" />
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnSerialDalete" runat="server" CausesValidation="false" OnClick="lbtnSerialDalete_Click" OnClientClick="DeleteConfirm()">
                                                                        <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="5%" HorizontalAlign="Center" />
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
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnStock" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpopStock" runat="server" Enabled="True" TargetControlID="btnStock"
                PopupControlID="pnlStock" CancelControlID="lbtnStock" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel runat="server" ID="pnlStock" DefaultButton="lbtnSearch">
                <div runat="server" id="Div3" class="panel panel-primary Mheight">
                    <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="lbtnStock" runat="server">
                                <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <div class="col-sm-11">
                                <strong>Item Stock Balance</strong>
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
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
                                    <asp:GridView ID="grdStock" CausesValidation="false" runat="server" OnSelectedIndexChanged="grdResult_SelectedIndexChanged"
                                        AllowPaging="True" OnPageIndexChanging="grdResult_PageIndexChanging" GridLines="None" AutoGenerateColumns="false"
                                        CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager">
                                        <Columns>
                                            <asp:BoundField DataField="INL_COM" HeaderText="COM" ReadOnly="true" />
                                            <asp:BoundField DataField="INL_LOC" HeaderText="LOC" ReadOnly="true" />
                                            <asp:BoundField DataField="INL_ITM_CD" HeaderText="ITEM" ReadOnly="true" />
                                            <asp:BoundField DataField="INL_ITM_STUS" HeaderText="STATUS" ReadOnly="true" />
                                            <asp:BoundField DataField="INL_QTY" HeaderText="QTY" ReadOnly="true" />
                                        </Columns>
                                    </asp:GridView>
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
            <asp:Button ID="btnconfbox" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupConfBox" runat="server" Enabled="True" TargetControlID="btnconfbox"
                PopupControlID="pnlConfBox" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="UpdatePanel26">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait1112" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait1112" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:Panel ID="pnlConfBox" runat="server" align="center">
        <div runat="server" id="Div5" class="panel panel-info height120 width250">
            <asp:Label ID="Label6" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:ScriptManagerProxy ID="ScriptManagerProxy19" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel26" runat="server">
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
                                <asp:Button ID="btnconfsave" runat="server" Text="yes" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnconfsave_Click" />
                            </div>
                            <div class="col-sm-2 ">
                                <asp:Button ID="btnconfcancel" runat="server" Text="No" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnconfcancel_Click" />
                            </div>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>


      <asp:UpdatePanel ID="UpdatePanel21" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button7" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserDPopoup" runat="server" Enabled="True" TargetControlID="Button7"
                PopupControlID="pnlDpopup" CancelControlID="btnDClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel22" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlDpopup" DefaultButton="lbtnSearch">
                <div runat="server" id="Div6" class="panel panel-default height400 width700">

                    <asp:Label ID="Label5" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="btnDClose" runat="server" OnClick="btnDClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <%--<span>Commen Search</span>--%>
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12" id="Div7" runat="server">
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
                                            <asp:TextBox runat="server" Enabled="false" ID="txtFDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnFDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFDate"
                                                PopupButtonID="lbtnFDate" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-2 labelText1">
                                            To
                                        </div>
                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                            <asp:TextBox runat="server" Enabled="false" ID="txtTDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnTDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtTDate"
                                                PopupButtonID="lbtnTDate" Format="dd/MMM/yyyy">
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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy8" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                            <ContentTemplate>
                                                <div class="col-sm-8 paddingRight5">
                                                    <asp:DropDownList ID="ddlSearchbykeyD" runat="server" class="form-control">
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
                                            <asp:TextBox ID="txtSearchbywordD" CausesValidation="false" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbywordD_TextChanged"></asp:TextBox>
                                        </div>
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy14" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel24" runat="server">
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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy15" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdResultD" CausesValidation="false" runat="server" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" GridLines="None" CssClass="table table-hover table-striped" PageSize="5" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResultD_SelectedIndexChanged" OnPageIndexChanging="grdResultD_PageIndexChanging">
                                                    <Columns>
                                                        <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10" />

                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>

                                <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
                                    runat="server" AssociatedUpdatePanelID="UpdatePanel22">

                                    <ProgressTemplate>
                                        <div class="divWaiting">
                                            <asp:Label ID="lblWait" runat="server"
                                                Text="Please wait... " />
                                            <asp:Image ID="imgWait" runat="server"
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

</asp:Content>
