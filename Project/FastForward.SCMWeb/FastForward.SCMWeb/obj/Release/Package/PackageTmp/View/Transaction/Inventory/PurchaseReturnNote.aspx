<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="PurchaseReturnNote.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Inventory.PurchaseReturnNote" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/ucOutScan.ascx" TagPrefix="uc1" TagName="ucOutScan" %>

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
        function showStickyNoticeToast() {
            $().toastmessage('showWarningToast', "Warning Dialog which is fading away ...");
        }
        function showStickyNoticeToast(value) {
            $().toastmessage('showToast', {
                text: value,
                sticky: true,
                position: 'top-center',
                type: 'warning',
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
                sticky: false,
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
        function ConfirmSendToPDA() {
            var selectedvaldelitm = confirm("Are you sure you want to send ?");
            if (selectedvaldelitm) {
                document.getElementById('<%=txtpdasend.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtpdasend.ClientID %>').value = "No";
            }
        };

        function CheckAll(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdAdSearch.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }
        function Enable() {
            return;
        }
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
        function SaveConfirm() {
            var selectedvalue = confirm("Do you want to save data?");
            if (selectedvalue) {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "No";
            }
        };
        function TempSaveConfirm() {
            var selectedvalue = confirm("Do you want to temporary save data?");
            if (selectedvalue) {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "No";
            }
        };
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
        function ConfirmPrint() {
            var selectedvalueOrdPlace = confirm("Do you want to print Doument ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=hdnprint.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdnprint.ClientID %>').value = "No";
            }
        };
        // Reference the textbox

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
    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>

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
                    <asp:HiddenField ID="txtClearlconformmessageValue" runat="server" />
                    <asp:HiddenField ID="txtSavelconformmessageValue" runat="server" />
                    <asp:HiddenField ID="txtUpdateconformmessageValue" runat="server" />
                    <asp:HiddenField ID="txtApprovalconformmessageValue" runat="server" />
                    <asp:HiddenField ID="txtCancelconformmessageValue" runat="server" />
                    <asp:HiddenField ID="txtDeleteconformmessageValue" runat="server" />
                       <asp:HiddenField ID="txtpdasend" runat="server" />
                     <asp:HiddenField ID="hdnprint" runat="server" />
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="col-sm-7  buttonrow">
                                <div id="WarningGRN" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">
                                    <div class="col-sm-11">
                                        <asp:Label ID="lblH1" runat="server" Text="Label"></asp:Label>
                                        <asp:Label ID="lblBackDateInfor" runat="server"></asp:Label>

                                    </div>


                                </div>
                                <div class="col-sm-2">
                                    <strong>Send to PDA</strong>
                                </div>

                                <div class="col-sm-1">
                                    <asp:CheckBox runat="server" ID="chkpda" AutoPostBack="true" Enabled="false" />
                                </div>
                            </div>
                            <div class="col-sm-5  buttonRow">
                                <div class="col-sm-1">
                                </div>
                                <div class="col-sm-3 paddingRight0">
                                    <asp:LinkButton ID="lbtnTempSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="TempSaveConfirm()" OnClick="lbtnTempSave_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Temporary Save
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-2 paddingRight0">
                                    <asp:LinkButton ID="lbtnSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="SaveConfirm()" OnClick="lbtnSave_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                    </asp:LinkButton>
                                </div>
                                 <div class="col-sm-2 paddingRight0 paddingLeft0">
                                    <asp:LinkButton ID="lbtnprint" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmPrint()" OnClick="lbtnPrint_Click">
                                    <span class="glyphicon glyphicon-print" aria-hidden="true"></span>Print
                                    </asp:LinkButton>
                                </div>
                           
                                <div class="col-sm-2 paddingRight0 paddingLeft0">
                                    <asp:LinkButton ID="lbtnprintserial" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmPrint()" OnClick="lbtnprintserial_Click">
                                    <span class="glyphicon glyphicon-print" aria-hidden="true"></span>Print Serail
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-2">
                                    <asp:LinkButton ID="lbtnClear" runat="server" CssClass="floatRight" OnClientClick="ClearConfirm()" OnClick="lbtnClear_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-sm-10 paddingRight5">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    General Detail
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-2 paddingRight0">
                                            <div class="col-sm-3 labelText1 paddingLeft0">
                                                Date
                                            </div>
                                            <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                <asp:TextBox runat="server" Enabled="false" ID="txtDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="lbtnDate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDate"
                                                    PopupButtonID="lbtnDate" Format="dd/MMM/yyyy">
                                                </asp:CalendarExtender>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 paddingRight0 paddingLeft0">
                                            <div class="col-sm-4 labelText1">
                                                Sub Type
                                            </div>
                                            <div class="col-sm-8 paddingRight0 paddingLeft0">
                                                <asp:DropDownList ID="ddlAdjSubType" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlAdjSubType_SelectedIndexChanged">
                                                    <asp:ListItem Text=" - - Select - - " Value="0" Selected="True"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <asp:Label ID="lblSubTypeDesc" Visible="false" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="col-sm-6 labelText1 paddingRight0 paddingLeft0">
                                                Manual Ref #
                                            </div>
                                            <div class="col-sm-6 paddingRight0 paddingLeft0">
                                                <asp:TextBox runat="server" ID="txtManualRef" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-2 paddingRight0 paddingLeft0">
                                            <div class="col-sm-4 labelText1 paddingLeft0 paddingRight0">
                                                Order Type 
                                            </div>
                                            <div class="col-sm-8 paddingLeft0 ">
                                                <asp:DropDownList ID="ddlMainType" AutoPostBack="True" CausesValidation="false" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text=" - - Select - - " Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Local" Value="L" />
                                                    <asp:ListItem Text="Imports" Value="I" />
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 paddingRight0 paddingLeft0">
                                            <div class="col-sm-4 labelText1">
                                                Supplier Code
                                            </div>
                                            <div class="col-sm-5 paddingRight5">
                                                <asp:TextBox runat="server" AutoPostBack="true" ID="txtOtherRef" CausesValidation="false" CssClass="form-control" OnTextChanged="txtOtherRef_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft5 paddingRight0 ">
                                                <asp:LinkButton ID="lbtnSearch_Supplier" runat="server" CausesValidation="false" OnClick="lbtnSearch_Supplier_Click">
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
                                        <div class="col-sm-4 ">
                                            <div class="row">
                                                <div class="col-sm-4 labelText1 ">
                                                    Delivery Instruction 
                                                </div>
                                                <div class="col-sm-8 paddingRight5">
                                                    <asp:TextBox runat="server" ID="txtDAdd1" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4 labelText1 ">
                                                </div>
                                                <div class="col-sm-8 paddingRight5">
                                                    <asp:TextBox runat="server" ID="txtDAdd2" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4 ">
                                            <div class="col-sm-2 labelText1 ">
                                                Remark
                                            </div>
                                            <div class="col-sm-10 paddingRight5">
                                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtRemarks" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 paddingLeft5 paddingRight0">
                                            <div class="row">
                                                <div class="col-sm-4 labelText1 paddingLeft5 paddingRight0">
                                                    Transport Method
                                                </div>
                                                <div class="col-sm-6 paddingLeft5 paddingRight0">
                                                    <asp:DropDownList ID="ddlDeliver" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDeliver_SelectedIndexChanged">
                                                        <%-- <asp:ListItem Text=" - - Select - - " Value="0" Selected="True"></asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4 labelText1  paddingLeft5 paddingRight0">
                                                    <asp:Label ID="lblVehicle" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-6 paddingLeft5 paddingRight0">
                                                    <asp:TextBox runat="server" ID="txtVehicle" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft5 paddingRight0 ">
                                                    <asp:LinkButton ID="lbtnvehicle" Visible="false" runat="server" CausesValidation="false">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                            <asp:LinkButton ID="lbtnAdvSeach" runat="server" OnClick="lbtnAdvSeach_Click">
                                                         Advance Search
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
                        <div class="col-sm-2 paddingLeft5">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                </div>
                                <div class="panel-body">
                                    <div class="row">

                                        <div class="col-sm-5 labelText1 ">
                                            Document #
                                        </div>
                                        <div class="col-sm-1  paddingLeft0 paddingRight0  ">
                                            <asp:CheckBox ID="chk_Temp" runat="server"></asp:CheckBox>
                                        </div>
                                        <div class="col-sm-5  paddingLeft5 paddingRight0">
                                            Temporary Save
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-10 labelText1">
                                            <asp:TextBox runat="server" ID="txtDocumentNo" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtDocumentNo_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft5 paddingRight0 ">
                                            <asp:LinkButton ID="lbtnDocNo" runat="server" CausesValidation="false" OnClick="lbtnDocNo_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-5 labelText1">
                                            Sequence No -
                                        </div>
                                        <div class="col-sm-5 labelText1 paddingLeft5 paddingRight0">
                                            <%--<asp:Label ID="lblUserSeqNo" runat="server"></asp:Label>--%>
                                            <asp:TextBox ID="txtUserSeqNo" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-11">
                                            <asp:DropDownList ID="ddlSeqNo" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSeqNo_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-12 ">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                </div>
                                <div class="panel-body">
                                    <uc1:ucOutScan runat="server" ID="ucOutScan" />

                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-default ">
                                <div class="panel-body">
                                    <div class="bs-example">
                                        <ul class="nav nav-tabs" id="myTab">
                                            <li class="active"><a href="#Item" data-toggle="tab">Item</a></li>
                                            <li><a href="#Serial" data-toggle="tab">Serial #</a></li>
                                        </ul>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>
                                    <div class="tab-content">
                                        <div class="tab-pane active" id="Item">
                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-sm-12">
                                                        <div class="panel panel-default">
                                                            <div class="panel-body panelscollbar height150">
                                                                <asp:GridView ID="grdItems" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Add">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtnitm_AddSerial" runat="server" CausesValidation="false" OnClick="lbtnitm_AddSerial_Click" Width="5px">
                                                                        <span class="glyphicon glyphicon-plus" aria-hidden="true" ></span>
                                                                                </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Remove">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtnitm_Remove" runat="server" CausesValidation="false" OnClientClick="DeleteConfirm()" Width="5px" OnClick="lbtnitm_Remove_Click">
                                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true" ></span>
                                                                                </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="#" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblitri_line_no" runat="server" Text='<%# Bind("itri_line_no") %>' Width="5px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Item">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblitri_itm_cd" runat="server" Text='<%# Bind("itri_itm_cd") %>' Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Description">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="itm_Description" runat="server" Text='<%# Bind("mi_longdesc") %>' Width="200px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Model">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="itm_Model" runat="server" Text='<%# Bind("mi_model") %>' Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Status" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="itm_Status" runat="server" Text='<%# Bind("itri_itm_stus") %>' Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Status">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Mis_desc" runat="server" Text='<%# Bind("Mis_desc") %>' Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Unit Cost" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="itri_unit_price" runat="server" Text='<%# Bind("itri_unit_price","{0:n}") %>' Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="App. Qty">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="itm_AppQty" runat="server" Text='<%# Bind("itri_app_qty","{0:n}") %>' Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Pick Qty">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblitri_qty" runat="server" Text='<%# Bind("Itri_bqty","{0:n}") %>' Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                             <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Request" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="itm_requestno" runat="server" Text='<%# Bind("itri_note") %>' Width="50px"></asp:Label>
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
                                        <div class="tab-pane" id="Serial">
                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-sm-12">
                                                        <div class="panel panel-default">
                                                            <div class="panel-body panelscollbar height150">
                                                                <asp:GridView ID="grdSerial" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Delete">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtnser_Remove" runat="server" CausesValidation="false" OnClientClick="DeleteConfirm()" Width="5px" OnClick="lbtnser_Remove_Click">
                                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true" ></span>
                                                                                </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Item">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ser_Item" runat="server" Text='<%# Bind("tus_itm_cd") %>' Width="40px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Model">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ser_Model" runat="server" Text='<%# Bind("tus_itm_model") %>' Width="80px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Status" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ser_Status" runat="server" Text='<%# Bind("tus_itm_stus") %>' Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Status">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Tus_itm_stus_Desc" runat="server" Text='<%# Bind("Tus_itm_stus_Desc") %>' Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Qty">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ser_Qty" runat="server" Text='<%# Bind("tus_qty","{0:n}") %>' Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Serial 1">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ser_Serial1" runat="server" Text='<%# Bind("tus_ser_1") %>' Width="20px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Serial 2">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ser_Serial2" runat="server" Text='<%# Bind("tus_ser_2") %>' Width="20px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Serial 3">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ser_Serial3" runat="server" Text='<%# Bind("tus_ser_3") %>' Width="60px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Bin" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ser_Bin" runat="server" Text='<%# Bind("tus_bin") %>' Width="20px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Serial ID">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ser_SerialID" runat="server" Text='<%# Bind("tus_ser_id") %>' Width="20px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Request" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ser_requestno" runat="server" Text='<%# Bind("tus_base_doc_no") %>' Width="200px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="BaseLineNo" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ser_BaseLineNo" runat="server" Text='<%# Bind("tus_base_itm_line") %>' Width="20px"></asp:Label>
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

                            </div>
                        </div>
                    </div>


                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel19" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button4" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="userAserial" runat="server" Enabled="True" TargetControlID="Button4"
                PopupControlID="pnlASerialpopup" CancelControlID="LinkButton1" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel23" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlASerialpopup" DefaultButton="lbtnSearch">
                <div runat="server" id="Div7" class="panel panel-default height400 width700">
                    <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="LinkButton1" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <%--<span>Commen Search</span>--%>
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12" id="Div8" runat="server">
                                <div class="row">
                                    <div class="col-sm-12">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-4">
                                    </div>
                                    <div class="col-sm-8">
                                        <div class="col-sm-4 labelText1">
                                            Search by key
                                        </div>
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy14" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                            <ContentTemplate>
                                                <div class="col-sm-3 paddingRight5">
                                                    <asp:DropDownList ID="DropDownList2" runat="server" class="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div class="col-sm-4 labelText1">
                                            Search by word
                                        </div>
                                        <div class="col-sm-4 paddingRight5">
                                            <asp:TextBox ID="TextBox2" CausesValidation="false" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbyword_TextChanged"></asp:TextBox>
                                        </div>
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy15" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel21" runat="server">
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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy16" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="GridView1" CausesValidation="false" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResult_SelectedIndexChanged" OnPageIndexChanging="grdResult_PageIndexChanging">
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
    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserPopoup" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="pnlpopup" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel18" runat="server">
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
                            <div class="col-sm-12" id="Div3" runat="server">
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
                                                <asp:GridView ID="grdResult" CausesValidation="false" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResult_SelectedIndexChanged" OnPageIndexChanging="grdResult_PageIndexChanging">
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
    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserDPopoup" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="pnlDpopup" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlDpopup" DefaultButton="lbtnSearch">
                <div runat="server" id="Div1" class="panel panel-default height400 width700">

                    <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
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
                            <div class="col-sm-12" id="Div2" runat="server">
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
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFDate"
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
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTDate"
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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy5" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy6" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
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
                                    runat="server" AssociatedUpdatePanelID="UpdatePanel7">

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

    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserAdPopup" runat="server" Enabled="True" TargetControlID="Button2"
                PopupControlID="pnlASearchpopup" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress7" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait1580" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait1580" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlASearchpopup">
                <div runat="server" id="Div4" class="panel panel-default height400 width700">
                    <asp:Label ID="lblAvalue" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="btnAClose" runat="server" OnClick="btnAClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <%--<span>Commen Search</span>--%>
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12" id="Div5" runat="server">
                                <div class="row">
                                    <div class="col-sm-12">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12" id="Div6" runat="server">
                                        <div class="col-sm-2 labelText1">
                                            Search by key
                                        </div>
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy11" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                            <ContentTemplate>
                                                <div class="col-sm-3 paddingRight5">
                                                    <asp:DropDownList ID="ddlSearchbykeyA" runat="server" class="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div class="col-sm-2 labelText1">
                                            Search by word
                                        </div>
                                        <div class="col-sm-4 paddingRight5">
                                            <asp:TextBox ID="txtSearchbywordA" CausesValidation="false" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbywordA_TextChanged"></asp:TextBox>
                                        </div>
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy12" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                            <ContentTemplate>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnSearchA" runat="server" OnClick="lbtnSearchA_Click">
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
                                    <div class="col-sm-8">
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Button ID="btnAllItem" runat="server" Visible="true" CssClass="btn btn-primary btn-xs" Text="Add All Items" OnClick="btnAllItem_Click" />
                                        <%-- <asp:LinkButton ID="LinkButton3" runat="server" OnClick="btnAdvanceAddItem_Click">
                                            <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>Add Item
                                                    </asp:LinkButton>--%>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Button ID="btnAdvanceAddItem" runat="server" Visible="true" CssClass="btn btn-primary btn-xs" Text="Add Item" OnClick="btnAdvanceAddItem_Click" />
                                        <%-- <asp:LinkButton ID="LinkButton3" runat="server" OnClick="btnAdvanceAddItem_Click">
                                            <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>Add Item
                                                    </asp:LinkButton>--%>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy13" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                            <ContentTemplate>
                                                <div style="max-height:250px; overflow-y:scroll;">
                                                <asp:GridView ID="grdAdSearch" AutoGenerateColumns="false" CausesValidation="false" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" 
                                                    GridLines="None" CssClass="table table-hover table-striped"  PagerStyle-CssClass="cssPager"  
                                                    OnSelectedIndexChanged="grdAdSearch_SelectedIndexChanged" OnPageIndexChanging="grdAdSearch_PageIndexChanging">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="allchk" runat="server" Width="5px" onclick="CheckAll(this)"></asp:CheckBox>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="selectchk" runat="server" Width="5px"></asp:CheckBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Code">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Col_ItemCode" runat="server" Text='<%# Bind("Item") %>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Serial">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Col_Serial1" runat="server" Text='<%# Bind("Serial") %>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Col_inb_qty" runat="server" Text='<%# Bind("inb_qty") %>' Width="30px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Supplier">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Col_Supplier" runat="server" Text='<%# Bind("Supplier") %>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cost" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Col_ins_unit_cost" runat="server" Text='<%# Bind("ins_unit_cost") %>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="status" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Col_ins_itm_stus" runat="server" Text='<%# Bind("ins_itm_stus") %>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

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
                </div>
            </asp:Panel>


        </ContentTemplate>

    </asp:UpdatePanel>

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
                <div runat="server" id="Div9" class="panel panel-default height150 width525">
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
                                                    <asp:DropDownList ID="ddlloadingbay" runat="server" TabIndex="2" CssClass="form-control" ></asp:DropDownList>
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
     <asp:Panel ID="pnlConfBox" runat="server" align="center">
        <div runat="server" id="Div10" class="panel panel-info height120 width250">
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
                                <asp:Button ID="Button8" runat="server" Text="yes" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="Button1_Click" />
                            </div>
                            <div class="col-sm-2 ">
                                <asp:Button ID="Button9" runat="server" Text="No" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="Button2_Click" />
                            </div>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>
</asp:Content>
