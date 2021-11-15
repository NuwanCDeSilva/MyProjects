<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" MaintainScrollPositionOnPostback="false" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="GoodsReceivedNote.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Inventory.Goods_Received_Note___GRN" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/jquery.toastmessage.js"></script>
    <script>
        function setScrollPosition(scrollValue) {

            $('#<%=hfScrollPosition.ClientID%>').val(scrollValue);
        }
        function maintainScrollPosition() {
            $('.dvScroll').scrollTop($('#<%=hfScrollPosition.ClientID%>').val());
        }
        function setScrollPositionItem(scrollValue) {

            $('#<%=hfScrollPositionItem.ClientID%>').val(scrollValue);
        }
        function maintainScrollPositionItem() {
            $('.dvScrollItem').scrollTop($('#<%=hfScrollPositionItem.ClientID%>').val());
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
        function SendscanConfirm() {
            var selectedvalue = confirm("Are your sure you want to send to scan?");
            if (selectedvalue) {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "No";
            }
        };

        function SaveConfirm() {
            var selectedvalue = confirm("Do you want to Save data?");
            if (selectedvalue) {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "No";
            }
        };
        function ClearConfirm() {
            var selectedvalue = confirm("Do you want to Clear data?");
            if (selectedvalue) {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "No";
            }
        };
        function DeleteConfirm() {
            var selectedvalue = confirm("Do you want to Delete data?");
            if (selectedvalue) {
                document.getElementById('<%=txtDeleteconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtDeleteconformmessageValue.ClientID %>').value = "No";
            }
        };
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
              && (charCode < 48 || charCode > 57))
                return false;

            return true;
        };
        function checkDateE(sender, args) {

            if ((sender._selectedDate < new Date())) {
                alert("You cannot select a day earlier than today!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        };
        function checkDateF(sender, args) {

            if ((sender._selectedDate > new Date())) {
                alert("You cannot select a day future date !");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        };
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
              && (charCode < 48 || charCode > 57))
                return false;

            return true;
        };

        function Enable() {
            return;
        }
        function checkDateGRN(sender, args) {

            if ((sender._selectedDate < new Date())) {
                //alert("GRN date should be greater than or equal to PO date.");
                //sender._selectedDate = new Date();
                //sender._textbox.set_Value(sender._selectedDate.format(sender._format))
                $().toastmessage('showToast', {
                    text: "GRN date should be greater than or equal to PO date.",
                    sticky: true,
                    position: 'top-center',
                    type: 'warning',
                    closeText: '',
                    close: function () {
                        console.log("toast is closed ...");
                    }
                });
            }

        }
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
    <script type="text/javascript">
        function Focus() {
            // Reference the textbox
            var txt = document.getElementById("<%=tstSubSerial.ClientID%>");

            // If the textbox was found, call its focus function
            if (txt != null)
                txt.focus();
        };

        function doPostBack(t) {
            if (t.value != "") {
                __doPostBack(t.name, "");
            }
        };

    </script>
    <script type="text/javascript">

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

        .panel {
            margin-bottom: 1px;
            margin-top: 0px;
            padding-top: 1px;
            padding-bottom: 0px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <asp:UpdateProgress ID="UpdateProgress3" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel3">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait3" runat="server" Text="Please wait... " />
                <asp:Image ID="imgWait3" runat="server" ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdateProgress ID="UpdateProgress4" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait1" runat="server" Text="Please wait... " />
                <asp:Image ID="imgWait1" runat="server" ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel17">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait4" runat="server" Text="Please wait... " />
                <asp:Image ID="imgWait4" runat="server" ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
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
                    <asp:HiddenField ID="hdnprint" runat="server" />
                    <asp:HiddenField ID="txtpdasend" runat="server" />
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="col-sm-6  buttonrow">

                                <div id="WarningGRN" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">
                                    <div class="col-sm-11">
                                        <strong>Alert!</strong>
                                        <asp:Label ID="lblWGRN" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-1">
                                        <asp:LinkButton ID="lbtnWGRN" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnWGRN_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>

                                </div>

                                <div id="SuccessGRN" runat="server" visible="false" class="alert alert-success alert-dismissible" role="alert">
                                    <div class="col-sm-11">
                                        <strong>Success!</strong>
                                        <asp:Label ID="lblSGRN" runat="server"></asp:Label>

                                    </div>
                                    <div class="col-sm-1">
                                        <asp:LinkButton ID="LinkButton7" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnWGRN_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>

                                </div>
                                <div id="Information" runat="server" visible="false" class="alert alert-success alert-info" role="alert">
                                    <div class="col-sm-11">
                                        <strong>Info!</strong>
                                        <asp:Label ID="lblBackDateInfor" runat="server"></asp:Label>
                                        <asp:Label ID="lblH1" runat="server" Text="Label"></asp:Label>
                                    </div>

                                </div>
                            </div>

                            <div class="col-sm-6 padding0">
                                <div class="col-sm-1 padding0">
                                </div>
                                <div class="col-sm-11 padding0">
                                    <div class="col-sm-12 buttonRow">

                                        <div class="col-sm-2 paddingRight0 text-center paddingLeft0">
                                            <asp:LinkButton ID="lbtnSave" CausesValidation="false" runat="server" CssClass="" OnClick="lbtnAdd_Click" OnClientClick="SaveConfirm()">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-2 paddingRight0 text-center paddingLeft0">
                                            <asp:LinkButton ID="lbtnTempSave" CausesValidation="false" runat="server" CssClass="" OnClientClick="SaveConfirm()" OnClick="lbtnTempSave_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Temp Save
                                            </asp:LinkButton>
                                        </div>

                                        <div class="col-sm-2 paddingRight0 text-center paddingLeft0">
                                            <asp:LinkButton ID="lbtnprint" CausesValidation="false" runat="server" CssClass="" OnClientClick="ConfirmPrint()" OnClick="lbtnPrint_Click">
                                    <span class="glyphicon glyphicon-print" aria-hidden="true"></span>Print
                                            </asp:LinkButton>
                                        </div>

                                        <div class="col-sm-2 paddingRight0 text-center paddingLeft0">
                                            <asp:LinkButton ID="lbtnprintserial" CausesValidation="false" runat="server" CssClass="" OnClientClick="ConfirmPrint()" OnClick="lbtnprintserial_Click">
                                    <span class="glyphicon glyphicon-print" aria-hidden="true"></span>Print Serial
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-2 paddingRight0 text-center paddingLeft0">
                                            <asp:LinkButton ID="lbtnExcelUpload" CausesValidation="false" runat="server" CssClass="" OnClick="lbtnExcelUpload_Click">
                                        <span class="glyphicon glyphicon-upload" aria-hidden="true"></span>Excel Upload
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-2 paddingRight0 text-center paddingLeft0">
                                            <asp:LinkButton ID="lbtnClear" runat="server" CssClass="" OnClientClick="ClearConfirm()" OnClick="lbtnClear_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:ScriptManagerProxy ID="ScriptManagerProxy8" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div class="panel panel-default">
                        <div class="panel panel-heading">
                            <strong><b>Goods Received Note- GRN</b></strong>
                        </div>
                        <div class="panel panel-body padding0">
                            <div class="row">
                                <div class="col-sm-10 paddingRight5">
                                    <div class="panel panel-default">
                                        <div class="panel panel-heading" style="height: 21px;">
                                            <div class="row">
                                                <div class="col-sm-12 paddingRight0 paddingLeft0">
                                                    <div class="col-sm-4 paddingRight0 paddingLeft0">
                                                        Pending Purchase Order Requests 
                                                    </div>
                                                    <div class="col-sm-3"></div>
                                                    <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                        PDA Completed
                                                        
                                                    </div>
                                                    <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                        <asp:CheckBox runat="server" ID="chkPendingDoc" />
                                                    </div>

                                                    <div class="col-sm-2 paddingRight0 paddingLeft0">
                                                        Completed Purchase Orders
                                                        
                                                    </div>
                                                    <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                        <asp:CheckBox runat="server" ID="chkFPO" />
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-body">
                                            <div class="row">
                                                <div class="col-sm-2 paddingRight0">
                                                    <div class="col-sm-3 labelText1 paddingLeft0 paddingRight0">
                                                        Type 
                                                    </div>
                                                    <div class="col-sm-8 paddingLeft0 paddingRight0">
                                                        <asp:DropDownList ID="ddlMainType" AutoPostBack="True" CausesValidation="false" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlMainType_SelectedIndexChanged">
                                                            <asp:ListItem Text=" - - Select - - " Value="0" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="Local" Value="L" />
                                                            <asp:ListItem Text="Imports" Value="I" />
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2 paddingRight0">
                                                    <div class="col-sm-3 labelText1 paddingLeft0">
                                                        From
                                                    </div>
                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                        <asp:TextBox runat="server" ReadOnly="true" ID="txtFromDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnPFDate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtFromDate"
                                                            PopupButtonID="lbtnPFDate" Format="dd/MMM/yyyy">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2 paddingRight0 paddingLeft0">
                                                    <div class="col-sm-2 labelText1 paddingLeft0">
                                                        To
                                                    </div>
                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                        <asp:TextBox runat="server" ReadOnly="true" ID="txtToDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnTodate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDate"
                                                            PopupButtonID="lbtnTodate" Format="dd/MMM/yyyy">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2 paddingRight0 paddingLeft0">
                                                    <div class="col-sm-2 labelText1 paddingLeft0">
                                                        Supplier
                                                    </div>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox runat="server" ID="txtFindSupplier" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtFindSupplier_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0 paddingRight0 ">
                                                        <asp:LinkButton ID="lbtnSearch_Supplier" runat="server" CausesValidation="false" OnClick="lbtnSearch_Supplier_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2 paddingRight0 paddingLeft0">
                                                    <div class="col-sm-4 labelText1">
                                                        Order #
                                                    </div>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlPType" CausesValidation="false" runat="server" CssClass="form-control">
                                                            <asp:ListItem Text=" - - Select - - " Value="0" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="Order #" Value="1" />
                                                            <asp:ListItem Text="Ref #" Value="2" />
                                                            <asp:ListItem Text="Job #" Value="3" />
                                                            <asp:ListItem Text="SI Ref #" Value="4" />
                                                             <asp:ListItem Text="Bank Ref #" Value="5" />
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2 paddingRight0 paddingLeft0">
                                                    <div class="col-sm-8">
                                                        <asp:TextBox runat="server" ID="txtFindPONo" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtFindPONo_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0 paddingRight0 ">
                                                        <asp:LinkButton ID="lbtnSearch_PO" runat="server" CausesValidation="false" OnClick="lbtnSearch_PO_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0 paddingRight0 ">
                                                        <asp:LinkButton ID="lbtnFilter" runat="server" CausesValidation="false" OnClick="lbtnFilter_Click">
                                                        <span class="glyphicon glyphicon-search fontsize20" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5"></div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="panel panel-default">
                                                        <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                                        <div class="dvScroll panel panel-body padding0 panelscollbar height120" onscroll="setScrollPosition(this.scrollTop);">
                                                            <asp:GridView ID="grdPendingPo" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                                                <Columns>

                                                                    <asp:TemplateField HeaderText="Add" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtnPSelect" runat="server" CausesValidation="false" Width="5px" OnClick="lbtnPSelect_Click">
                                                                        <span class="glyphicon glyphicon-arrow-down" aria-hidden="true" ></span>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="5px" />
                                                                        <HeaderStyle Width="5px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="PDA" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtnPDA" runat="server" CausesValidation="false" Width="5px" OnClick="lbtnPDA_Click">
                                                                        <span class="glyphicon glyphicon-arrow-down" aria-hidden="true" ></span>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="5px" />
                                                                        <HeaderStyle Width="5px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="PO #">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="POH_DOC_NO" runat="server" Text='<%# Bind("POH_DOC_NO") %>' Width="100%"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="100px" />
                                                                        <HeaderStyle Width="100px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="PO Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="POH_DT" runat="server" Text='<%# Bind("POH_DT" , "{0:dd/MMM/yyyy}") %>' Width="100%"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="70px" />
                                                                        <HeaderStyle Width="70px" />
                                                                    </asp:TemplateField>
                                                                    <%--                          <asp:TemplateField HeaderText="UOM" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="POH_UOM" runat="server" Text='<%# Bind("POD_UOM") %>' Width="100%"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="100px" />
                                                                        <HeaderStyle Width="100px" />
                                                                    </asp:TemplateField>--%>
                                                                    <asp:TemplateField HeaderText="Ref #">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="POH_REF" runat="server" Text='<%# Bind("POH_REF") %>' Width="100%"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="50px" />
                                                                        <HeaderStyle Width="50px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Supplier Code">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="POH_SUPP" runat="server" Text='<%# Bind("POH_SUPP") %>' Width="100%"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="100px" />
                                                                        <HeaderStyle Width="100px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Supplier Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="MBE_NAME" runat="server" Text='<%# Bind("MBE_NAME") %>' Width="100%"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="200px" />
                                                                        <HeaderStyle Width="200px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Remarks">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="POH_REMARKS" runat="server" Text='<%# Bind("POH_REMARKS") %>' Width="100%"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="200px" />
                                                                        <HeaderStyle Width="200px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Type" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="POH_TP" runat="server" Text='<%# Bind("POH_TP") %>' Width="20px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="20px" />
                                                                        <HeaderStyle Width="20px" />
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
                                <div class="col-sm-2 paddingLeft5">
                                    <div class="panel panel-default">
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
                                                <div class="col-sm-10">
                                                    <asp:TextBox runat="server" ID="txtDocumentNo" Enabled="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0 paddingRight0 ">
                                                    <asp:LinkButton ID="lbtnDocNo" runat="server" CausesValidation="false" OnClick="lbtnDocNo_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6 height10">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <strong>Send to PDA</strong>
                                                </div>

                                                <div class="col-sm-1">
                                                    <asp:CheckBox runat="server" ID="chkpda" Enabled="false" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6 height10">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-10">
                                                    <strong runat="server" id="AutosacnDiv">Auto Scan Non-Serialized Items</strong>
                                                </div>

                                                <div class="col-sm-1">
                                                    <asp:CheckBox runat="server" ID="chkAODoutserials" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6 height10">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-10">
                                                    <asp:Button Text="Send to scan" ID="btnSentScan" Visible="true" runat="server" OnClick="btnSentScan_Click" OnClientClick="SendscanConfirm() " />

                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-10 paddingRight0">
                                                    <asp:TextBox runat="server" ID="txtDO" placeholder="DO #" Enabled="true" CausesValidation="false" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1">
                                                    <asp:LinkButton ID="lbtnDo" Visible="true" runat="server" CausesValidation="false" OnClick="lbtnDo_Click">
                                                        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                             <%-- Dulaj 2018-Oct-15 --%>
                                             <div class="row">
                                                <div class="col-sm-4">
                                                      <div class="row">
                                                          <div class="col-sm-6">
                                                            <asp:Label ID="LabelQR" runat="server" Text="QR" />
                                                              </div>
                                                          <div class="col-sm-6">
                                                              <asp:CheckBox runat="server" ID="CheckBoxQR" AutoPostBack="true" OnCheckedChanged="chkqr_CheckedChanged"/>
                                                          </div>
                                                          </div>                                                     
                                                </div>
                                                <div class="col-sm-8">
                                                    <div class="row">
                                                        <div class="col-sm-4">
                                                    <asp:Label ID="lblcomQR" runat="server" Text="Company" />
                                                            </div>
                                                        <div class="col-sm-8">
                                                   <asp:DropDownList ID="DropDownListQRCom" CausesValidation="false" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Text="Select" Value="0" Selected="True"></asp:ListItem>
                                                       <asp:ListItem Text="Hero" Value="1" ></asp:ListItem>
                                                       <asp:ListItem Text="TVS" Value="2" ></asp:ListItem>
                                                   </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                            </div>
                                            <%-- END --%>
                                        </div>
                                    </div>
                                </div>
                                <%--  <div class="col-sm-2 paddingLeft5">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    Option
                                </div>
                                <div class="panel-body  height80">
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height20">
                                    </div>
                                </div>
                            </div>
                        </div>--%>
                            </div>

                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="panel panel-default">
                                        <div class="panel-heading paddingtopbottom0">
                                            General Details
                                        </div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-sm-2 paddingRight0">
                                                    <div class="col-sm-3 labelText1 paddingLeft0">
                                                        Date
                                                    </div>
                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                        <asp:TextBox runat="server" Enabled="false" ID="txtDODate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnDoDate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDODate"
                                                            PopupButtonID="lbtnDoDate" Format="dd/MMM/yyyy">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                                <div class="col-sm-5 paddingRight0 paddingLeft0">
                                                    <div class="col-sm-1 labelText1 paddingLeft0 paddingRight0">
                                                        Supplier
                                                    </div>
                                                    <div class="col-sm-3 paddingLeft5 paddingRight0">
                                                        <asp:TextBox runat="server" ID="txtSuppCode" ReadOnly="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-8 paddingLeft0  ">
                                                        <asp:TextBox runat="server" ID="txtSuppName" ReadOnly="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2 paddingRight0 paddingLeft0">
                                                    <div class="col-sm-2 labelText1 paddingLeft0 paddingRight0">
                                                        PO #
                                                    </div>
                                                    <div class="col-sm-10 paddingLeft5 paddingRight0">
                                                        <asp:TextBox runat="server" ID="txtPONo" ReadOnly="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2 paddingRight0 paddingLeft0">
                                                    <div class="col-sm-3 labelText1  paddingRight0">
                                                        PO Date
                                                    </div>
                                                    <div class="col-sm-9 paddingLeft5 paddingRight0">
                                                        <asp:TextBox runat="server" ID="txtPODate" ReadOnly="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2 paddingRight0">
                                                    <div class="col-sm-3 labelText1 paddingLeft0">
                                                        Ref #
                                                    </div>
                                                    <div class="col-sm-9 paddingRight5 paddingLeft0">
                                                        <asp:TextBox runat="server" ID="txtPORefNo" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2 paddingRight0 paddingLeft0">
                                                    <div class="col-sm-3 labelText1 paddingLeft0 ">
                                                        Entry #
                                                    </div>
                                                    <div class="col-sm-9 paddingLeft5 paddingRight0">
                                                        <asp:TextBox runat="server" ID="txtEntry" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2 paddingRight0">
                                                    <div class="col-sm-5 labelText1 paddingLeft0">
                                                        Clearance Date
                                                    </div>
                                                    <div class="col-sm-6 paddingRight5 ">
                                                        <asp:TextBox runat="server" ReadOnly="true" ID="txtCDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnCDate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtCDate"
                                                            PopupButtonID="lbtnCDate" Format="dd/MMM/yyyy">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 paddingRight0 paddingLeft0">
                                                    <div class="col-sm-1 labelText1 paddingLeft0 paddingRight0">
                                                        Remarks
                                                    </div>
                                                    <div class="col-sm-11 paddingLeft5 ">
                                                        <asp:TextBox runat="server" ID="txtRemarks" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <asp:Panel runat="server" ID="pnlserialMaintan_true">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="panel panel-default">
                                            <div class="panel-heading paddingbottom0 paddingtop0" role="tab" id="">
                                                <div class="row">
                                                    <div class="col-sm-2">
                                                        <%-- <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">Add Item
                                        </a>--%>

                                                        <asp:LinkButton ID="lbtnAddItem" runat="server" CausesValidation="false"  OnClick="lbtnAddItem_Click"><label id="GrnItemLabal" runat="server">Add GRN Item</label>                                         
                                                        <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"></span>
                                                        </asp:LinkButton>

                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                            <asp:CheckBox ID="chkwaranty" runat="server"></asp:CheckBox>
                                                        </div>
                                                        <div class="col-sm-5 paddingLeft0">
                                                            Warranty
                                                        </div>
                                                    
                                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                            <asp:CheckBox ID="chkNewItem" runat="server"></asp:CheckBox>
                                                        </div>
                                                        <div class="col-sm-5 paddingLeft0" runat="server" id="NewItemDiv">
                                                            New Item
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-2">
                                                        <div class="">
                                                            <asp:LinkButton ID="lbtnValidateSerial" runat="server" CausesValidation="false" Text="Add Item"
                                                                OnClick="lbtnValidateSerial_Click"> Serial Excel Upload
                                                        <span class="glyphicon" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-5 padding0">
                                                        <div class="col-sm-2 paddingLeft0 labelText1" style="text-align: right">
                                                            Invoice #
                                                        </div>
                                                        <div class="col-sm-4 paddingLeft0">
                                                            <asp:TextBox runat="server" ID="txtSupInvNo" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-2 paddingLeft0 labelText1" style="text-align: right">
                                                            Invoice Date
                                                        </div>
                                                        <div class="col-sm-3 padding0">
                                                            <div class="col-sm-9 paddingRight5 paddingLeft0">
                                                                <asp:TextBox runat="server" Enabled="false" ID="txtSupInvDt" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-2 paddingLeft0">
                                                                <asp:LinkButton ID="lbtnSupInvDt" runat="server" CausesValidation="false">
                                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                                <asp:CalendarExtender ID="CalendarExtender9" runat="server" TargetControlID="txtSupInvDt"
                                                                    PopupButtonID="lbtnSupInvDt" Format="dd/MMM/yyyy">
                                                                </asp:CalendarExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <%--<div id="collapseTwo" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingTwo">--%>

                                            <div class="panel-body" runat="server" visible="false" id="divAddItem">
                                                <div class="panel-body">

                                                    <div class="row">
                                                        <div class="col-sm-12 paddingRight0 paddingLeft0">
                                                            <div class="col-sm-2 paddingRight0 paddingLeft5">
                                                                <div class="col-sm-3 labelText1 paddingLeft5 paddingRight5">
                                                                    Bin Code
                                                                </div>
                                                                <div class="col-sm-7">
                                                                    <asp:TextBox runat="server" ID="txtBincode" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0 ">
                                                                    <asp:LinkButton ID="lbtnBincode" runat="server" CausesValidation="false" OnClick="lbtnBincode_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-2 paddingRight0 paddingLeft0">
                                                                <asp:Panel runat="server" DefaultButton="btnI">
                                                                    <div class="col-sm-4 labelText1 " runat="server" id="ItemDiv">
                                                                        Item 
                                                                    </div>
                                                                    <div class="col-sm-7">
                                                                        <asp:TextBox runat="server" ID="txtItemCode" onblur="doPostBack(this)" AutoPostBack="true" OnTextChanged="txtItemCode_TextChanged" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0 ">
                                                                        <asp:LinkButton ID="lbtnIcode" runat="server" CausesValidation="false" OnClick="lbtnIcode_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-sm-1">
                                                                        <asp:Button ID="btnI" runat="server" OnClick="txtItemCode_TextChanged" Text="Submit" Style="display: none;" />
                                                                    </div>
                                                                </asp:Panel>
                                                            </div>
                                                            <div class="col-sm-3 paddingRight0 paddingLeft0">
                                                                <div class="col-sm-3 labelText1 ">
                                                                    Description
                                                                </div>
                                                                <div class="col-sm-9">
                                                                    <asp:TextBox runat="server" ID="txtItemDes" ReadOnly="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                                <div class="col-sm-5 labelText1 paddingRight5 ">
                                                                    Model
                                                                </div>
                                                                <div class="col-sm-7 paddingRight0 paddingLeft5">
                                                                    <asp:TextBox runat="server" ID="txtModel" ReadOnly="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-2 paddingRight0 paddingLeft0">
                                                                <div class="col-sm-4 labelText1 ">
                                                                    Brand
                                                                </div>
                                                                <div class="col-sm-7">
                                                                    <asp:TextBox runat="server" ID="txtBrand" ReadOnly="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-2 paddingRight0 paddingLeft0">
                                                                <div class="col-sm-4 labelText1 ">
                                                                    part #
                                                                </div>
                                                                <div class="col-sm-7">
                                                                    <asp:TextBox runat="server" ID="txtpartNo" ReadOnly="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-1 paddingRight0 paddingLeft0" runat="server" visible="false">
                                                                <div class="col-sm-4 labelText1 ">
                                                                    <asp:Label ID="lblDocQty" runat="server" Text="Label"></asp:Label>
                                                                </div>
                                                                <div class="col-sm-7">
                                                                    <asp:Label ID="lblScanQty" runat="server" Text="Label"></asp:Label>
                                                                    <asp:Label ID="lblPrice" runat="server" Text="Label"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-2 paddingRight0 paddingLeft5">
                                                            <div class="col-sm-4 labelText1 paddingLeft5" runat="server" id="asstetStatusDiv">
                                                              Item Status  
                                                            </div>
                                                            <div class="col-sm-8">
                                                                <asp:DropDownList ID="ddlStatus" CausesValidation="false" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Text=" - - Select - - " Value="0" Selected="True"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-7" runat="server" id="Qty" visible="true">
                                                            <div class="col-sm-2 paddingRight0 paddingLeft0">
                                                                <asp:Panel runat="server" DefaultButton="btnqty">
                                                                    <div class="col-sm-2 labelText1 paddingLeft0">
                                                                        Qty
                                                                    </div>
                                                                    <div class="col-sm-10">
                                                                        <asp:TextBox runat="server" ID="txtqty" oncopy="return false"
                                                                            onpaste="return false"
                                                                            oncut="return false" onkeypress="return isNumberKey(event)" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1">
                                                                        <asp:Button ID="btnqty" runat="server" OnClick="btnqty_Click" Text="Submit" Style="display: none;" />
                                                                    </div>
                                                                </asp:Panel>
                                                            </div>
                                                            <div class="col-sm-5 paddingRight0">
                                                                <div class="col-sm-4 labelText1 paddingLeft0">
                                                                    Manufacture Date
                                                                </div>
                                                                <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                    <asp:TextBox runat="server" ReadOnly="true" ID="txtMdate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0">
                                                                    <asp:LinkButton ID="lbtnMDate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                    <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtMdate"
                                                                        PopupButtonID="lbtnMDate" Format="dd/MMM/yyyy">
                                                                    </asp:CalendarExtender>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-5 paddingRight0">
                                                                <div class="col-sm-4 labelText1 paddingLeft0">
                                                                    Expiry  Date
                                                                </div>
                                                                <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                    <asp:TextBox runat="server" Enabled="false" ID="txtEDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0">
                                                                    <asp:LinkButton ID="lbtnEDate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                    <asp:CalendarExtender ID="CalendarExtender6" OnClientDateSelectionChanged="checkDate" runat="server" TargetControlID="txtEDate"
                                                                        PopupButtonID="lbtnEDate" Format="dd/MMM/yyyy">
                                                                    </asp:CalendarExtender>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-7" runat="server" id="divSer" visible="false">
                                                            <div class="col-sm-4 paddingRight0 paddingLeft0">

                                                                <asp:Panel runat="server" DefaultButton="btna">
                                                                    <div class="col-sm-3 labelText1 paddingLeft0">
                                                                        Serial # I
                                                                    </div>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox runat="server" AutoPostBack="false" ID="txtSerial1" OnTextChanged="txtSerial1_TextChanged" CausesValidation="false" CssClass="form-control"></asp:TextBox>

                                                                    </div>
                                                                    <div class="col-sm-1">
                                                                        <asp:Button ID="btna" runat="server" OnClick="test_Click" Text="Submit" Style="display: none;" />
                                                                    </div>
                                                                </asp:Panel>
                                                            </div>
                                                            <div class="col-sm-4 paddingRight0 paddingLeft0">
                                                                <asp:Panel runat="server" DefaultButton="btnSII">
                                                                    <div class="col-sm-3 labelText1 paddingLeft5">
                                                                        Serial # II
                                                                    </div>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox runat="server" ID="txtSerial2" Enabled="false" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1">
                                                                        <asp:Button ID="btnSII" runat="server" OnClick="btnSII_Click" Text="Submit" Style="display: none;" />
                                                                    </div>
                                                                </asp:Panel>
                                                            </div>
                                                            <div class="col-sm-4 paddingRight0 paddingLeft0">
                                                                <asp:Panel runat="server" DefaultButton="btnSII">
                                                                    <div class="col-sm-3 labelText1 paddingLeft5">
                                                                        Serial # III
                                                                    </div>
                                                                    <div class="col-sm-9">
                                                                        <asp:TextBox runat="server" onpaste="return false" ID="txtSerial3" Enabled="true" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1">
                                                                        <asp:Button ID="btnserail3" runat="server" OnClick="btnserail3_Click" Text="Submit" Style="display: none;" />
                                                                    </div>
                                                                </asp:Panel>

                                                            </div>
                                                        </div>
                                                        <div class="col-sm-3 paddingRight0 paddingLeft0">
                                                            <div class="col-sm-3 labelText1 paddingLeft5">
                                                                Batch #
                                                            </div>
                                                            <div class="col-sm-9">
                                                                <asp:TextBox runat="server" ID="txtBatchNo" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-1 paddingRight0 paddingLeft0" runat="server" visible="false">

                                                            <div class="col-sm-1 paddingLeft0 paddingRight0 ">
                                                                <asp:LinkButton ID="lbtnItemAdd" runat="server" OnClick="lbtnItemAdd_Click">
                                                            <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>

                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                            <%--</div>--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="panel panel-default ">
                                            <div class="panel-body padding0">
                                                <div class="bs-example">
                                                    <div class="col-sm-5 padding0">

                                                        <ul class="nav nav-tabs" id="myTab">
                                                            <li class="active"><a href="#Item" data-toggle="tab" runat="server" id="ListItem">Item</a></li>
                                                            <li><a href="#Serial" data-toggle="tab">Serial #</a></li>
                                                        </ul>

                                                    </div>
                                                    <div class="col-sm-2 padding0">
                                                        <div class="col-sm-1">
                                                            <asp:CheckBox ID="chkPickTop" AutoPostBack="true" OnCheckedChanged="chkPickTop_CheckedChanged" Text="" runat="server" />
                                                        </div>
                                                        <div class="col-sm-10 paddingLeft3">
                                                            <asp:Label Text="Show picked item on top" runat="server" id="showPickedLable"/>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        PO Total QTY
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:Label runat="server" ID="lblpototal" ForeColor="#A513D0"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        Pick Total QTY
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:Label runat="server" ID="lblpicktotal" ForeColor="#A513D0"></asp:Label>
                                                    </div>

                                                </div>
                                                <div class="tab-content">
                                                    <div class="tab-pane padding0 active" id="Item">
                                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <ContentTemplate>
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <div class="panel panel-default">
                                                                            <asp:HiddenField ID="hfScrollPositionItem" Value="0" runat="server" />
                                                                            <div class="dvScrollItem panel-body padding0 panelscollbar height150" onscroll="setScrollPositionItem(this.scrollTop);">
                                                                                <asp:GridView ID="grdDOItems" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="">
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="lbtnGet" runat="server" CausesValidation="false" Width="10px" OnClick="lbtnGet_Click">
                                                                        <span class="glyphicon glyphicon-arrow-up" aria-hidden="true" ></span>
                                                                                                </asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="10px" />
                                                                                            <HeaderStyle Width="10px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Seq. No" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="PODI_SEQ_NO" runat="server" Text='<%# Bind("PODI_SEQ_NO") %>' Width="100px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="#" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="PODI_LINE_NO" runat="server" Text='<%# Bind("PODI_LINE_NO") %>' Width="20px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <%--   <asp:TemplateField HeaderText="##" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="PODI_DEL_LINE_NO" runat="server" Text='<%# Bind("PODI_DEL_LINE_NO") %>' Width="80px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                                                        <asp:TemplateField HeaderText="Item">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="PODI_ITM_CD" runat="server" Text='<%# Bind("PODI_ITM_CD") %>' Width="100px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="100px" />
                                                                                            <HeaderStyle Width="100px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="UOM">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="UOM" runat="server" Text='<%# Bind("UOM") %>' Width="100px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="100px" />
                                                                                            <HeaderStyle Width="100px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Description">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="MI_LONGDESC" runat="server" Text='<%# Bind("MI_LONGDESC") %>' Width="100%"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="350px" />
                                                                                            <HeaderStyle Width="350px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Part No">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="MI_PART_NO" runat="server" Text='<%# Bind("MI_PART_NO") %>' Width="100%"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="100px" />
                                                                                            <HeaderStyle Width="100px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Model">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="MI_MODEL" runat="server" Text='<%# Bind("MI_MODEL") %>' Width="100px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="100px" />
                                                                                            <HeaderStyle Width="100px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Brand" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="MI_BRAND" runat="server" Text='<%# Bind("MI_BRAND") %>' Width="20px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Item Status" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="POD_ITM_STUS" runat="server" Text='<%# Bind("POD_ITM_STUS") %>' Width="20px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="PO Qty">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="PODI_QTY" runat="server" Text='<%# Bind("PODI_QTY","{0:N2}") %>' Width="80px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle CssClass="gridHeaderAlignRight" Width="80px" />
                                                                                            <HeaderStyle CssClass="gridHeaderAlignRight" Width="80px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Rem. Qty">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="PODI_BAL_QTY" runat="server" Text='<%# Bind("PODI_BAL_QTY","{0:N2}") %>' Width="80px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle CssClass="gridHeaderAlignRight" Width="80px" />
                                                                                            <HeaderStyle CssClass="gridHeaderAlignRight" Width="80px" />
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Pick Qty">
                                                                                            <EditItemTemplate>
                                                                                                <asp:TextBox ID="txtPickQty" runat="server" Text='<%# Bind("GRN_QTY","{0:N2}") %>'></asp:TextBox>
                                                                                            </EditItemTemplate>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="PickQty" runat="server" Text='<%# Bind("GRN_QTY","{0:N2}") %>' Width="80px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle CssClass="gridHeaderAlignRight" Width="80px" />
                                                                                            <HeaderStyle CssClass="gridHeaderAlignRight" Width="80px" />
                                                                                        </asp:TemplateField>
                                                                                        <%-- <asp:TemplateField HeaderText="Del. Loca" Visible="false">
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="PODI_LOCA" runat="server" Text='<%# Bind("PODI_LOCA") %>' Width="20px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                                                        <%--  <asp:TemplateField HeaderText="Remarks" Visible="false">
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="PODI_REMARKS" runat="server" Text='<%# Bind("PODI_REMARKS") %>' Width="20px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                                                        <asp:TemplateField HeaderText="Unit Cost" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="UNIT_PRICE" runat="server" Text='<%# Bind("UNIT_PRICE") %>' Width="20px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField ShowHeader="False">
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="lbtngrdDOItemstEdit_1" CausesValidation="false" runat="server" OnClick="lbtngrdDOItemstEdit_1_Click">
                                                                                        <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>
                                                                                                </asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                            <EditItemTemplate>
                                                                                                <asp:LinkButton ID="lbtngrdDOItemstUpdate_1" runat="server" CausesValidation="false" CommandName="Update" OnClick="lbtngrdDOItemstUpdate_1_Click">
                                                                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                                                                </asp:LinkButton>

                                                                                            </EditItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Right" Width="2%" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Row No" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblRowNo" runat="server" Text='<%# Bind("RowNo")%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <div class="tab-pane " id="Serial">
                                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                            <ContentTemplate>
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <div class="panel panel-default">
                                                                            <div class="panel-body padding0 panelscollbar height150">

                                                                                <asp:GridView ID="grdDOSerials" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" OnRowDataBound="grdDOSerials_DataBound" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Seq No" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="TUS_USRSEQ_NO" runat="server" Text='<%# Bind("TUS_USRSEQ_NO") %>' Width="100px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="lbtnViewSerial" runat="server" CausesValidation="false" Width="15px"
                                                                                                    OnClick="lbtnViewSerial_Click">
                                                                                                    <span class="glyphicon glyphicon-eye-open" aria-hidden="true"></span>
                                                                                                </asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="15px" />
                                                                                            <HeaderStyle Width="15px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="">
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="lbtnRemove" runat="server" CausesValidation="false" OnClientClick="DeleteConfirm()" Width="15px" OnClick="lbtnRemove_Click">
                                                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true" ></span>
                                                                                                </asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="15px" />
                                                                                            <HeaderStyle Width="15px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Bin">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="TUS_BIN" runat="server" Text='<%# Bind("TUS_BIN") %>' Width="50px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="50px" />
                                                                                            <HeaderStyle Width="50px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Item">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="TUS_ITM_CD" runat="server" Text='<%# Bind("TUS_ITM_CD") %>' Width="100px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="100px" />
                                                                                            <HeaderStyle Width="100px" />
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Description">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="TUS_ITM_DESC" runat="server" Text='<%# Bind("TUS_ITM_DESC") %>' Width="100%"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="350px" />
                                                                                            <HeaderStyle Width="350px" />
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Model">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="TUS_ITM_MODEL" runat="server" Text='<%# Bind("TUS_ITM_MODEL") %>' Width="100%"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="80px" />
                                                                                            <HeaderStyle Width="80px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Brand">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="TUS_ITM_BRAND" runat="server" Text='<%# Bind("TUS_ITM_BRAND") %>' Width="60px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="60px" />
                                                                                            <HeaderStyle Width="60px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Base Item ">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="tus_new_itm_cd" runat="server" Text='<%# Bind("tus_new_itm_cd") %>' Width="100%"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="100px" />
                                                                                            <HeaderStyle Width="100px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Qty">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Tus_qty" runat="server" Text='<%# Bind("Tus_qty","{0:N2}") %>' Width="40px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="40px" CssClass="gridHeaderAlignRight" />
                                                                                            <HeaderStyle Width="40px" CssClass="gridHeaderAlignRight" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label runat="server" Text='' Width="5px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="10px" />
                                                                                            <HeaderStyle Width="10px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Status" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="TUS_ITM_STUS" runat="server" Text='<%# Bind("TUS_ITM_STUS") %>' Width="60px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Status">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Mis_desc" runat="server" Text='<%# Bind("Mis_desc") %>' Width="60px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="60px" />
                                                                                            <HeaderStyle Width="60px" />
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Serial 1">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="TUS_SER_1" runat="server" Text='<%# Bind("TUS_SER_1") %>' Width="100%"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="100px" />
                                                                                            <HeaderStyle Width="100px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Serial 2">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="TUS_SER_2" runat="server" Text='<%# Bind("TUS_SER_2") %>' Width="100%"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="100px" />
                                                                                            <HeaderStyle Width="100px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Warr #">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="TUS_WARR_NO" runat="server" Text='<%# Bind("TUS_WARR_NO") %>' Width="100%"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="80px" />
                                                                                            <HeaderStyle Width="80px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Serial ID" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="TUS_SER_ID" runat="server" Text='<%# Bind("TUS_SER_ID") %>' Width="100%"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="60px" />
                                                                                            <HeaderStyle Width="60px" />
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>

                                                                            </div>
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
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pnlserialMaintan_false" Visible="false">
                                <div class="panel panel-warning">
                                    <div class="panel-heading paddingtopbottom0">
                                        <div class="row">

                                            <div class="col-sm-4">
                                                <div class="col-sm-3 labelText1 ">
                                                    Item Code
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox runat="server" Enabled="false" TabIndex="100" ID="txtitemcode_2" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0 paddingRight0 ">
                                                    <asp:LinkButton ID="lbtnitemcode_2" runat="server" CausesValidation="false" OnClick="lbtnitemcode_2_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="col-sm-3 labelText1 paddingLeft5">
                                                    Item Status
                                                </div>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlStatus_2" TabIndex="101" CausesValidation="false" runat="server" CssClass="form-control">
                                                        <asp:ListItem Text=" - - Select - - " Value="0" Selected="True"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="col-sm-2 labelText1 ">
                                                    Qty
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox runat="server" onkeypress="return isNumberKey(event)" Style="text-align: right" TabIndex="102" ID="txtQty_2" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0 paddingRight0 ">
                                                    <asp:LinkButton ID="lbtnadd_2" TabIndex="103" runat="server" CausesValidation="false" OnClick="lbtnadd_2_Click">
                                                        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="col-sm-8 labelText1 ">
                                                    Selected Item Code
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:Label ID="lbtnselectitem" runat="server" Text="N/A" CssClass="Color1 fontWeight900"></asp:Label>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-5">
                                                <div class="col-sm-2 labelText1 ">
                                                    Description :
                                                </div>
                                                <div class="col-sm-10">
                                                    <asp:Label ID="lbtnDes_2" runat="server" Text="" CssClass="Color1 fontWeight900"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="col-sm-3 labelText1 ">
                                                    Model :
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="lbtnmodel_2" runat="server" Text="" CssClass="Color1 fontWeight900"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="col-sm-4 labelText1 ">
                                                    Brand :
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="lbtnbrand_2" runat="server" Text="" CssClass="Color1 fontWeight900"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="col-sm-4 labelText1 ">
                                                    Part # :
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="lbtnpartno_2" runat="server" Text="" CssClass="Color1 fontWeight900"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="panel panel-default ">
                                                <div class="panel-body">
                                                    <div class="bs-example">
                                                        <ul class="nav nav-tabs" id="myTab2">
                                                            <li class="active"><a href="#Item2" data-toggle="tab">Item</a></li>
                                                            <li><a href="#Serial2" data-toggle="tab">Serial #</a></li>
                                                        </ul>
                                                    </div>
                                                    <div class="tab-content">
                                                        <div class="tab-pane active" id="Item2">
                                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy14" runat="server"></asp:ScriptManagerProxy>
                                                            <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                                                                <ContentTemplate>
                                                                    <div class="col-sm-12">
                                                                        <div class="panel panel-default">
                                                                            <div class="panel-body panelscollbar height150">
                                                                                <asp:GridView ID="grdDOItems_2" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Select ">
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="lbtnGet_2" runat="server" CausesValidation="false" Width="5px" OnClick="lbtnGet_2_Click">
                                                                        <span class="glyphicon glyphicon-arrow-up" aria-hidden="true" ></span>
                                                                                                </asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Seq. No" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="PODI_SEQ_NO" runat="server" Text='<%# Bind("PODI_SEQ_NO") %>' Width="100px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="#" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="PODI_LINE_NO" runat="server" Text='<%# Bind("PODI_LINE_NO") %>' Width="20px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <%--   <asp:TemplateField HeaderText="##" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="PODI_DEL_LINE_NO" runat="server" Text='<%# Bind("PODI_DEL_LINE_NO") %>' Width="80px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                                                        <asp:TemplateField HeaderText="Item">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="PODI_ITM_CD" runat="server" Text='<%# Bind("PODI_ITM_CD") %>' Width="50px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Description">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="MI_LONGDESC" runat="server" Text='<%# Bind("MI_LONGDESC") %>' Width="250px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Model">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="MI_MODEL" runat="server" Text='<%# Bind("MI_MODEL") %>' Width="80px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Brand" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="MI_BRAND" runat="server" Text='<%# Bind("MI_BRAND") %>' Width="20px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Item Status" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="POD_ITM_STUS" runat="server" Text='<%# Bind("POD_ITM_STUS") %>' Width="20px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="PO Qty">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="PODI_QTY" runat="server" Text='<%# Bind("PODI_QTY","{0:n}") %>' Width="20px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Remaining Qty">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="PODI_BAL_QTY" runat="server" Text='<%# Bind("PODI_BAL_QTY","{0:n}") %>' Width="20px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Pick Qty">
                                                                                            <EditItemTemplate>
                                                                                                <asp:TextBox ID="txtGRN_QTY" runat="server" Text='<%# Bind("GRN_QTY") %>'></asp:TextBox>
                                                                                            </EditItemTemplate>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="PickQty" runat="server" Text='<%# Bind("GRN_QTY","{0:n}") %>' Width="20px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField ShowHeader="False">
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="lbtngrdDOItemstEdit" CausesValidation="false" runat="server" OnClick="lbtngrdDOItemstEdit_Click">
                                                                <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>
                                                                                                </asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                            <EditItemTemplate>
                                                                                                <asp:LinkButton ID="lbtngrdDOItemstUpdate" runat="server" CausesValidation="false" CommandName="Update" OnClick="lbtngrdDOItemstUpdate_Click">
                                                                <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                                                                </asp:LinkButton>

                                                                                            </EditItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Right" Width="1%" />
                                                                                        </asp:TemplateField>
                                                                                        <%-- <asp:TemplateField HeaderText="Del. Loca" Visible="false">
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="PODI_LOCA" runat="server" Text='<%# Bind("PODI_LOCA") %>' Width="20px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                                                        <%--  <asp:TemplateField HeaderText="Remarks" Visible="false">
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="PODI_REMARKS" runat="server" Text='<%# Bind("PODI_REMARKS") %>' Width="20px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                                                        <asp:TemplateField HeaderText="Unit Cost" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="UNIT_PRICE" runat="server" Text='<%# Bind("UNIT_PRICE") %>' Width="20px"></asp:Label>
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
                                                        <div class="tab-pane" id="Serial2">
                                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy15" runat="server"></asp:ScriptManagerProxy>
                                                            <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                                                                <ContentTemplate>
                                                                    <div class="col-sm-12">
                                                                        <div class="panel panel-default">
                                                                            <div class="panel-body panelscollbar height150">
                                                                                <asp:GridView ID="grdDOSerials_2" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None"
                                                                                    CssClass="table table-hover table-striped" AutoGenerateColumns="false" OnRowDataBound="grdDOSerials_2_DataBound">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Seq No" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="TUS_USRSEQ_NO" runat="server" Text='<%# Bind("TUS_USRSEQ_NO") %>' Width="100px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Delete">
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="lbtnRemove" runat="server" CausesValidation="false" OnClientClick="DeleteConfirm()" Width="5px" OnClick="lbtnRemove_2_Click">
                                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true" ></span>
                                                                                                </asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Bin">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="TUS_BIN" runat="server" Text='<%# Bind("TUS_BIN") %>' Width="40px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Base Item">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="tus_new_itm_cd" runat="server" Text='<%# Bind("tus_new_itm_cd") %>' Width="80px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Item ">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="TUS_ITM_CD" runat="server" Text='<%# Bind("TUS_ITM_CD") %>' Width="80px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Description">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="TUS_ITM_DESC" runat="server" Text='<%# Bind("TUS_ITM_DESC") %>' Width="250px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Model">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="TUS_ITM_MODEL" runat="server" Text='<%# Bind("TUS_ITM_MODEL") %>' Width="50px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Brand">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="TUS_ITM_BRAND" runat="server" Text='<%# Bind("TUS_ITM_BRAND") %>' Width="20px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Item Status">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Mis_desc" runat="server" Text='<%# Bind("Mis_desc") %>' Width="60px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Qty">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Tus_qty" runat="server" Text='<%# Bind("Tus_qty","{0:n}") %>' Width="20px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Item Status" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="TUS_ITM_STUS" runat="server" Text='<%# Bind("TUS_ITM_STUS") %>' Width="60px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>


                                                                                        <asp:TemplateField HeaderText="Serial 1" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="TUS_SER_1" runat="server" Text='<%# Bind("TUS_SER_1") %>' Width="100px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Serial 2" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="TUS_SER_2" runat="server" Text='<%# Bind("TUS_SER_2") %>' Width="100px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Warr No" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="TUS_WARR_NO" runat="server" Text='<%# Bind("TUS_WARR_NO") %>' Width="200px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Serial ID" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="TUS_SER_ID" runat="server" Text='<%# Bind("TUS_SER_ID") %>' Width="20px"></asp:Label>
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
                                </div>
                            </asp:Panel>
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

    <%-- Dulaj Excel Validation --%>
     <asp:UpdatePanel ID="UpdatePanel48" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button21" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="ModalPopupExtenderExcelValidation" runat="server" Enabled="True" TargetControlID="Button21"
                PopupControlID="Panel4" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="Panel4" DefaultButton="lbtnSearch">
        <div runat="server" id="Div17" class="panel panel-default height400 width700">
            <asp:Label ID="Label20" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="LinkButton10" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <%--<span>Commen Search</span>--%>
                    <div class="col-sm-11">
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <div class="panel-body">
                    <asp:GridView ID="GridViewExcelValidation" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None"
                                                                                    CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Row No">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="RowNo" runat="server" Text='<%# Bind("RowNo") %>' Width="50px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>   
                                                                                        <asp:TemplateField HeaderText="Code">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label22" runat="server" Text='<%# Bind("Code") %>' Width="100px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>  
                                                                                        <asp:TemplateField HeaderText="Error">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Label23" runat="server" Text='<%# Bind("Error") %>' Width="300px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>                                                                                
                                                                                    </Columns>
                                                                                </asp:GridView>
                </div>
            </div>
        </div>
    </asp:Panel>


    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserCheckItem" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="pnlpopupSelectItem" CancelControlID="LinkButton1" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel23" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlpopupSelectItem" DefaultButton="lbtnSearch">
                <div runat="server" id="Div1" class="panel panel-default height400 width950">
                    <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
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

                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-sm-10">
                                        <asp:Label ID="lblNotMsg" Style="color: Green" runat="server"></asp:Label>

                                    </div>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 height5">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12" id="Div4" runat="server">
                                    <div class="col-sm-2 labelText1">
                                        Search by key
                                    </div>
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy5" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
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
                                        <asp:TextBox ID="txtSearchbyword2" CausesValidation="false" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbyword_TextChanged"></asp:TextBox>
                                    </div>
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy6" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
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
                                    <div class="panel panel-default">
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-sm-12 paddingRight5">
                                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
                                                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                        <ContentTemplate>
                                                            <asp:GridView ID="grdCheckIItem" OnPageIndexChanging="grdCheckIItem_PageIndexChanging" CausesValidation="false" AutoGenerateColumns="false" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdCheckIItem_SelectedIndexChanged">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Replace" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtnReplace" runat="server" OnClick="lbtnReplace_Click">
                                                            <span class="glyphicon glyphicon-hand-right" aria-hidden="true"  ></span>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Seq. No" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Pop_PODI_SEQ_NO" runat="server" Text='<%# Bind("PODI_SEQ_NO") %>' Width="100px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="#" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Pop_PODI_LINE_NO" runat="server" Text='<%# Bind("PODI_LINE_NO") %>' Width="200px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <%--   <asp:TemplateField HeaderText="##" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="PODI_DEL_LINE_NO" runat="server" Text='<%# Bind("PODI_DEL_LINE_NO") %>' Width="80px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                                    <asp:TemplateField HeaderText="Item Code">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Pop_PODI_ITM_CD" runat="server" Text='<%# Bind("PODI_ITM_CD") %>' Width="150px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Description">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Pop_MI_LONGDESC" runat="server" Text='<%# Bind("MI_LONGDESC") %>' Width="200px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Model">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Pop_MI_MODEL" runat="server" Text='<%# Bind("MI_MODEL") %>' Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Brand" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Pop_MI_BRAND" runat="server" Text='<%# Bind("MI_BRAND") %>' Width="20px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Item Status" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Pop_POD_ITM_STUS" runat="server" Text='<%# Bind("POD_ITM_STUS") %>' Width="20px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="PO Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Pop_PODI_QTY" runat="server" Text='<%# Bind("PODI_QTY") %>' Width="20px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Remaining Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Pop_PODI_BAL_QTY" runat="server" Text='<%# Bind("PODI_BAL_QTY") %>' Width="20px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Pick Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Pop_PickQty" runat="server" Text='<%# Bind("GRN_QTY") %>' Width="20px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <%-- <asp:TemplateField HeaderText="Del. Loca" Visible="false">
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="PODI_LOCA" runat="server" Text='<%# Bind("PODI_LOCA") %>' Width="20px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                                    <%--  <asp:TemplateField HeaderText="Remarks" Visible="false">
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="PODI_REMARKS" runat="server" Text='<%# Bind("PODI_REMARKS") %>' Width="20px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                                    <asp:TemplateField HeaderText="Unit Cost" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Pop_UNIT_PRICE" runat="server" Text='<%# Bind("UNIT_PRICE") %>' Width="20px"></asp:Label>
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

                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="userSubSerial" runat="server" Enabled="True" TargetControlID="Button2"
                PopupControlID="pnlpopupSubSerial" CancelControlID="LinkButton3" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlpopupSubSerial">
                <div class="panel panel-default  height400 width800">
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
                    <%--<asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>--%>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="panel panel-default">
                                    <div class="panel-body panelscollbar height200">
                                        <asp:GridView ID="GgdsubItem" EmptyDataText="No data found..." runat="server" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Update">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnupdate" runat="server" CausesValidation="false" OnClick="lbtnupdate_Click" Width="5px">
                                                                        <span class="glyphicon glyphicon-pencil" aria-hidden="true" ></span>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Main Item Serial" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>' Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Main Item Serial">
                                                    <ItemTemplate>
                                                        <asp:Label ID="tpss_m_ser" runat="server" Text='<%# Bind("tpss_m_ser") %>' Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="tpss_itm_cd" runat="server" Text='<%# Bind("tpss_itm_cd") %>' Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sub Item type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="tpss_tp" runat="server" Text='<%# Bind("tpss_tp") %>' Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sub Item Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="mis_desc" runat="server" Text='<%# Bind("mis_desc") %>' Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sub Item type" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="tpss_itm_stus" runat="server" Text='<%# Bind("tpss_itm_stus") %>' Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sub Item Serial">
                                                    <ItemTemplate>
                                                        <asp:Label ID="tpss_sub_ser" runat="server" Text='<%# Bind("tpss_sub_ser") %>' Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Sub Product
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:TextBox runat="server" ID="txtSubproduct" Enabled="false" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Item Status
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="ddlSIStatus" CausesValidation="false" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text=" - - Select - - " Value="0" Selected="True"></asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Serial #
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:TextBox runat="server" ID="tstSubSerial" CausesValidation="false" CssClass="form-control" OnTextChanged="tstSubSerial_TextChanged"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="panel panel-default">
                                    <div class="panel-body" runat="server" visible="false">
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Warranty No
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:TextBox runat="server" ID="txtWNo" Enabled="false" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel16" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button4" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="userPrefixSerial" runat="server" Enabled="True" TargetControlID="Button4"
                PopupControlID="pnlpopupPrefixSerial" CancelControlID="LinkButton4" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel17" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlpopupPrefixSerial">
                <div class="panel panel-default  height200 Dwidth">
                    <div class="panel-heading">
                        <asp:LinkButton ID="LinkButton4" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                        </asp:LinkButton>
                        <%--<span>Commen Search</span>--%>
                        <div class="col-sm-10">
                        </div>
                        <div class="col-sm-1">
                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                        <ContentTemplate>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="panel panel-default">
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-sm-3 labelText1">
                                                        PreFix
                                                    </div>
                                                    <div class="col-sm-5">
                                                        <asp:DropDownList ID="ddlPreFix" CausesValidation="false" runat="server" CssClass="form-control">
                                                            <asp:ListItem Text=" - - Select - - " Value="0" Selected="True"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:TextBox runat="server" ID="txtFix" Enabled="false" Visible="false" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">

                                                    <div class="col-sm-3 labelText1">
                                                        No of pages
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <asp:TextBox runat="server" ID="txtNoOfPages" Enabled="false" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>

                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <asp:Panel runat="server" DefaultButton="Button14">
                                                        <div class="col-sm-3 labelText1">
                                                            Start Page #
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <asp:TextBox runat="server" ID="txtStartPage" CausesValidation="false" onkeypress="return isNumberKey(event)" CssClass="form-control"></asp:TextBox>
                                                        </div>

                                                        <div class="col-sm-1">
                                                            <asp:Button ID="Button14" runat="server" OnClick="txtStartPage_TextChanged" Text="Submit" Style="display: none;" />
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-3 labelText1">
                                                        last Page #
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <asp:TextBox runat="server" ID="txtlastPages" Enabled="false" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height10">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-3 labelText1">
                                                        Pick Qty
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <asp:TextBox runat="server" ID="txtpickqty" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height10">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-4">
                                                        <asp:LinkButton ID="lbtnSavePreFix" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lbtnSavePreFix_Click">
                                                            <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
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
                <div runat="server" id="Div2" class="panel panel-default height150 width525">
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

        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:UpdatePanel ID="UpdatePanel24" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button6" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="excelUpload" runat="server" Enabled="True" TargetControlID="Button6"
                PopupControlID="pnlexcel" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel37" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlexcel">
                <div runat="server" id="Div7" class="panel panel-default height45 width700 ">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="btnClose3" runat="server" OnClick="btnClose3_Click">
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
                                        <asp:Label ID="lblalert" runat="server" ForeColor="Red" Text="Label" Visible="false"></asp:Label>
                                        <asp:Label ID="lblsuccess2" runat="server" ForeColor="Green" Visible="false"></asp:Label>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <asp:Panel runat="server" ID="pnlupload2" Visible="true">
                                    <div class="row">
                                        <div class="col-sm-12" id="Div8" runat="server">
                                            <div class="col-sm-5 paddingRight5">
                                                <asp:FileUpload ID="fileupexcelupload" runat="server" />
                                            </div>
                                            <div class="col-sm-2 paddingRight5">
                                                <asp:Button ID="btnAsyncUpload" runat="server" Text="Async_Upload" Visible="false" />
                                                <asp:Button ID="btnUpload" class="btn btn-warning btn-xs" runat="server" Text="Upload"
                                                    OnClick="btnUpload_Click" />
                                            </div>
                                        </div>
                                        <%--<div class="row">--%>
                                    </div>
                                </asp:Panel>

                            </div>
                        </div>

                    </div>
                </div>

            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnAsyncUpload"
                EventName="Click" />
            <asp:PostBackTrigger ControlID="btnupload" />
        </Triggers>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button5" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserDPopoup" runat="server" Enabled="True" TargetControlID="Button5"
                PopupControlID="pnlDpopup" CancelControlID="btnDClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel19" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlDpopup" DefaultButton="lbtnSearch">
                <div runat="server" id="Div5" class="panel panel-default height400 width700">

                    <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="btnDClose" runat="server">
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
                                            <asp:TextBox runat="server" Enabled="false" ID="txtFDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnFDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txtFDate"
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
                                            <asp:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="txtTDate"
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
                                                <asp:GridView ID="grdResultDate" CausesValidation="false" runat="server" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" GridLines="None" CssClass="table table-hover table-striped" PageSize="5" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResultDate_SelectedIndexChanged" OnPageIndexChanging="grdResultDate_PageIndexChanging">
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
    <asp:UpdatePanel ID="UpdatePanel29" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button7" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="excelValidate" runat="server" Enabled="True" TargetControlID="Button7"
                PopupControlID="pnlexcelValidate" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:Panel runat="server" ID="pnlexcelValidate">
        <div runat="server" id="Div9" class="panel panel-default height45 width700 ">


            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="lbtnClose" runat="server" OnClick="lbtnClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <%--<span>Commen Search</span>--%>
                    <div class="col-sm-11">
                        <asp:Label ID="lbllError" runat="server" ForeColor="Red" Text="Label" Visible="false"></asp:Label>
                        <asp:Label ID="lbllSucess" runat="server" ForeColor="Green" Visible="false"></asp:Label>
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
                            <asp:Panel runat="server" ID="pnlUpload">
                                <div class="col-sm-12" id="Div11">
                                    <div class="col-sm-5 paddingRight5">
                                        <asp:FileUpload ID="fileupexceluploadValidate" runat="server" />
                                    </div>
                                    <div class="col-sm-2 paddingRight5">
                                        <asp:Button ID="lbtnUpload" class="btn btn-warning btn-xs" runat="server" Text="Upload"
                                            OnClick="lbtnUpload_Click" />
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pnlProcess" Visible="false">
                                <div class="col-sm-12 ">
                                    <div id="div12" class="alert alert-info alert-success" role="alert">
                                        <div class="col-sm-1 padding0">
                                            <%--<asp:Label ID="lblAlertHe" Text="text" runat="server" />
                                            <strong>Alert!</strong>--%>
                                        </div>
                                        <div class="col-sm-10 padding0">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:Label ID="lbllAlert" Text="Excel file upload completed. Do you want to process ?" runat="server"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-sm-1 padding0">
                                            <asp:Button ID="lbtnProcess" class="btn btn-success btn-xs" runat="server" Text="Process" OnClick="lbtnProcess_Click" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </asp:Panel>

    <div class="row">
        <div class="col-sm-12">
            <asp:UpdatePanel runat="server" ID="UpdatePanel30">
                <ContentTemplate>
                    <asp:Button ID="Button12" runat="server" Text="Button" Style="display: none;" />
                    <asp:ModalPopupExtender ID="popupValidateData" runat="server" Enabled="True" TargetControlID="Button12"
                        PopupControlID="pnlSerialGrid" CancelControlID="LinkButton5" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
                    </asp:ModalPopupExtender>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:Panel runat="server" ID="pnlSerialGrid">
        <div class="row">
            <div class="col-sm-12">
                <div runat="server" id="d13" class="panel panel-info width700 height200">
                    <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading" style="height: 28px;">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-sm-11" style="color: red;">
                                        <b>
                                            <asp:Label ID="lblSerHed" Text="" runat="server" />
                                        </b>
                                    </div>

                                    <div class="col-sm-1">
                                        <div style="margin-top: -3px;">
                                            <asp:LinkButton ID="LinkButton5" runat="server">
                                    <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy16" runat="server"></asp:ScriptManagerProxy>
                                            <asp:UpdatePanel ID="UpdatePanel31" runat="server">
                                                <ContentTemplate>
                                                    <div style="max-height: 400px; overflow-y: auto;">
                                                        <div style="height: 360px; overflow-x: hidden; overflow-y: auto">
                                                            <asp:GridView ID="DgvSerInExcNotInGrid" CausesValidation="false" runat="server" GridLines="None" AutoGenerateColumns="false"
                                                                ShowHeaderWhenEmpty="true" EmptyDataText="No data found..."
                                                                CssClass="table table-hover table-striped" PagerStyle-CssClass="cssPager">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Serial 1">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSer1" Text='<%# Bind("Irsm_ser_1") %>' runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Serial 2">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSer1" Text='<%# Bind("Irsm_ser_2") %>' runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
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
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <asp:Button ID="btn10" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popUpUpload" runat="server" Enabled="True" TargetControlID="btn10"
                PopupControlID="pnlExcelUpload" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlExcelUpload">
        <div class="panel panel-default height45 width700 ">
            <div class="panel panel-default">
                <div class="panel-heading height30">
                    <div class="col-sm-11">
                        Excel Upload
                    </div>
                    <div class="col-sm-1">
                        <asp:LinkButton ID="LinkButton6" runat="server" OnClick="lbtnClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                        </asp:LinkButton>
                    </div>
                    <%--<span>Commen Search</span>--%>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12 paddingLeft0 paddingRight0">
                        <div class="row height22">
                            <div class="col-sm-11">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lblExcelUploadError" runat="server" ForeColor="Red" Text="Label" Visible="false"></asp:Label>
                                        <asp:Label ID="lblExcelUploadSuccess" runat="server" ForeColor="Green" Visible="false"></asp:Label>
                                        <asp:Label ID="lblExcelUploadInfo" runat="server" ForeColor="Blue" Visible="false"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="row">
                            <asp:Panel runat="server">
                                <div class="col-sm-12">
                                    <div class="col-sm-10 paddingRight5">
                                        <asp:FileUpload ID="fileUploadExcel" runat="server" />
                                    </div>
                                    <div class="col-sm-2 paddingRight5">
                                        <asp:Button ID="lbtnUploadExcel" class="btn btn-warning btn-xs" runat="server" Text="Upload"
                                            OnClick="lbtnUploadExcel_Click" />
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:UpdatePanel ID="up2" runat="server">
        <ContentTemplate>
            <asp:Button ID="btn11" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popUpProcess" runat="server" Enabled="True" TargetControlID="btn11"
                PopupControlID="pnlExcelProcces" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlExcelProcces">
        <div runat="server" class="panel panel-default height45 width700 ">
            <div class="panel panel-default">
                <div class="panel-heading height30">

                    <div class="col-sm-11">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblExcelProccesError" runat="server" ForeColor="Red" Text="Label" Visible="false"></asp:Label>
                                <asp:Label ID="lblExcelProccesSuccess" runat="server" ForeColor="Green" Visible="false"></asp:Label>
                                <asp:Label ID="lblExcelProccesInfo" runat="server" ForeColor="Blue" Visible="false"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-sm-1">
                        <asp:LinkButton ID="LinkButton8" runat="server" OnClick="lbtnClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12 paddingLeft0 paddingRight0">
                        <div class="row">
                            <asp:Panel runat="server" ID="Panel3">
                                <div class="col-sm-12 ">
                                    <div id="" class="alert alert-info alert-success" role="alert">
                                        <div class="col-sm-1 padding0">
                                            <strong>Alert!</strong>
                                        </div>
                                        <div class="col-sm-10 padding0">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:Label ID="lblProcess" Text="Excel file upload completed. Do you want to process ?" runat="server"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-sm-1 padding0">
                                            <asp:Button ID="lbtnExcelProcess" class="btn btn-success btn-xs" runat="server" Text="Process" OnClick="lbtnExcelProcess_Click" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </asp:Panel>
    <asp:UpdatePanel ID="updatePanelDocument" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnDocument" runat="server" Text="" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupDocument" runat="server" Enabled="True" TargetControlID="btnDocument"
                PopupControlID="panelDocument" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divDocument" class="row">
        <div class="col-sm-12 col-lg-12">
            <asp:Panel runat="server" ID="panelDocument">
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel panel-heading">
                            <strong>Item Details</strong>
                            <asp:LinkButton ID="lbtPopDocClose" runat="server" OnClick="lbtPopDocClose_Click" Style="float: right;">
                             <span class="glyphicon glyphicon-remove" style="margin-left:400px" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                        </div>
                        <div class="panel panel-body" style="padding-top: 0px;">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="dgvSubSerial" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped"
                                        GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Item">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblitempopup" runat="server" Text='<%# Bind("irsms_itm_cd") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Sub Serial">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblmodelpopup" runat="server" Text='<%# Bind("irsms_sub_ser") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>


    <asp:UpdatePanel ID="UpdatePanel32" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button10" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="SbuPopup" runat="server" Enabled="True" TargetControlID="Button10"
                PopupControlID="pnlSBU" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel ID="pnlSBU" runat="server" align="center">
        <asp:Label ID="Label4" runat="server" Text="Label" Visible="false"></asp:Label>
        <asp:ScriptManagerProxy ID="ScriptManagerProxy17" runat="server"></asp:ScriptManagerProxy>
        <asp:UpdatePanel ID="UpdatePanel33" runat="server">
            <ContentTemplate>
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <span>Alert</span>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label5" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblbinMssg" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblbinMssg1" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label9" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label10" runat="server"></asp:Label>
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



    <asp:UpdatePanel ID="updatePanel34" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button11" runat="server" Text="" Style="display: none;" />
            <asp:ModalPopupExtender ID="popupexcelitem" runat="server" Enabled="True" TargetControlID="Button11"
                PopupControlID="panelExcelItem" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="panelExcelItem" class="row">
        <div class="col-sm-12">
            <asp:Panel runat="server" ID="panel1">
                <div class="row">
                    <div class="panel panel-default  width1085">
                        <div class="panel panel-heading">
                            <strong runat="server" id="NewItemDetails">New Item Details</strong>
                            <asp:LinkButton ID="LinkButton9" runat="server" OnClick="lbtPopDocClose_Click" Style="float: right;">
                             <span class="glyphicon glyphicon-remove" style="margin-left:400px" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                        </div>
                        <div class="panel panel-body panelscollbar height200">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="grdExcelItem" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped"
                                        GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                        <Columns>
                                            <asp:TemplateField ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtngrdRevenuEdit" CausesValidation="false" runat="server" OnClick="lbtngrdRevenuEdit_Click">
                                                                <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:LinkButton ID="lbtngrdRevenueUpdate" runat="server" CausesValidation="false" CommandName="Update" OnClick="lbtngrdRevenueUpdate_Click">
                                                                <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                    </asp:LinkButton>

                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" Width="1%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Seq No" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="TUS_USRSEQ_NO" runat="server" Text='<%# Bind("TUS_USRSEQ_NO") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bin" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="TUS_BIN" runat="server" Text='<%# Bind("TUS_BIN") %>' Width="40px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="TUS_ITM_CD" runat="server" Text='<%# Bind("TUS_ITM_CD") %>' Width="80px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Base Item Code">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txttus_new_itm_cd" runat="server" Text='<%# Bind("tus_new_itm_cd") %>'></asp:TextBox>
                                                    <asp:LinkButton ID="lbtnbaseItem" runat="server" CausesValidation="false" OnClick="lbtnbaseItem_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="tus_new_itm_cd" runat="server" Text='<%# Bind("tus_new_itm_cd") %>' Width="80px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Desc">
                                                <ItemTemplate>
                                                    <asp:Label ID="TUS_ITM_DESC" runat="server" Text='<%# Bind("TUS_ITM_DESC") %>' Width="250px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Model" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="TUS_ITM_MODEL" runat="server" Text='<%# Bind("TUS_ITM_MODEL") %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Brand" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="TUS_ITM_BRAND" runat="server" Text='<%# Bind("TUS_ITM_BRAND") %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="Tus_qty" runat="server" Text='<%# Bind("Tus_qty") %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Status" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="TUS_ITM_STUS" runat="server" Text='<%# Bind("TUS_ITM_STUS") %>' Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="Mis_desc" runat="server" Text='<%# Bind("Mis_desc") %>' Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Serial 1" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="TUS_SER_1" runat="server" Text='<%# Bind("TUS_SER_1") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Serial 2" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="TUS_SER_2" runat="server" Text='<%# Bind("TUS_SER_2") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Warr No" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="TUS_WARR_NO" runat="server" Text='<%# Bind("TUS_WARR_NO") %>' Width="200px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Serial ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="TUS_SER_ID" runat="server" Text='<%# Bind("TUS_SER_ID") %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <div class="row">
                                <div class="col-sm-12 height10">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-10">
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button ID="btnexcelsavebaseItem" class="btn btn-default btn-xs" runat="server" Text="Continue" OnClick="btnexcelsavebaseItem_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </asp:Panel>
        </div>
    </div>


    <asp:UpdatePanel ID="UpdatePanel36" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button13" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="POpopup" runat="server" Enabled="True" TargetControlID="Button13"
                PopupControlID="pnlpopupPO" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpopupPO" DefaultButton="lbtnSearch">
        <div runat="server" id="Div13" class="panel panel-default height400 width700">
            <asp:Label ID="Label7" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="lbtnPOcolse" runat="server" OnClick="lbtnPOcolse_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <%--<span>Commen Search</span>--%>
                    <div class="col-sm-11">
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12" id="Div14" runat="server">
                        <div class="row">
                            <div class="col-sm-12">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height5">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12" id="Div15" runat="server">
                                <div class="col-sm-2 labelText1">
                                </div>

                                <div class="col-sm-2 labelText1">
                                    Search by word
                                </div>
                                <div class="col-sm-4 paddingRight5">
                                    <asp:TextBox ID="txtposearch" CausesValidation="false" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtposearch_TextChanged"></asp:TextBox>
                                </div>
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy20" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel38" runat="server">
                                    <ContentTemplate>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnposearch" runat="server" OnClick="lbtnposearch_Click">
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
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy21" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel39" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdpo" CausesValidation="false" runat="server" ShowHeaderWhenEmpty="true" EmptyDataText="No data found..." AutoGenerateColumns="false" GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" AllowPaging="True" OnPageIndexChanging="grdpo_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Item">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtngrdselect" runat="server" OnClick="lbtngrdselect_Click">
                                            <span class="glyphicon glyphicon-arrow-left" aria-hidden="true"  ></span>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPOPODI_ITM_CD" runat="server" Text='<%# Bind("PODI_ITM_CD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPOMI_LONGDESC" runat="server" Text='<%# Bind("MI_LONGDESC") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPOUNIT_PRICE" runat="server" Text='<%# Bind("UNIT_PRICE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPOPODI_LINE_NO" runat="server" Text='<%# Bind("PODI_LINE_NO") %>'></asp:Label>
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

    <asp:UpdatePanel ID="UpdatePanel35" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button15" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="MDPDOConf" runat="server" Enabled="True" TargetControlID="Button15"
                PopupControlID="pnlDO" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel ID="pnlDO" runat="server" align="center">
        <asp:Label ID="Label8" runat="server" Text="Label" Visible="false"></asp:Label>
        <asp:ScriptManagerProxy ID="ScriptManagerProxy18" runat="server"></asp:ScriptManagerProxy>
        <asp:UpdatePanel ID="UpdatePanel40" runat="server">
            <ContentTemplate>
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <span>Alert</span>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label11" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <strong>
                                    <asp:Label ID="txt1" Text="Additional Items with Qty" runat="server"></asp:Label></strong>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <strong>
                                    <asp:Label ID="txt2" Text="Are you sure you want to add all items ?" runat="server"></asp:Label></strong>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label15" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label16" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height22">
                            </div>
                        </div>
                        <div class="col-sm-12 panelscoll width450">
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy22" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel41" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="grdDOItem" AutoGenerateColumns="false" CausesValidation="false" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" AllowPaging="True" OnPageIndexChanging="grdResult_PageIndexChanging">
                                        <Columns>
                                            <%--<asp:ButtonField Text="select" CommandName="Select" ItemStyle-Width="10" />--%>

                                            <asp:TemplateField HeaderText="Item Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("Tus_itm_cd") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQty" runat="server" Text='<%# Bind("tus_qty") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>


    <asp:UpdatePanel ID="UpdatePanel42" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button16" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mdlconf" runat="server" Enabled="True" TargetControlID="Button16"
                PopupControlID="pnlconf" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress5" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel43">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWaitMee" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWaitMee" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:Panel ID="pnlconf" runat="server" align="center">
        <asp:Label ID="Label12" runat="server" Text="Label" Visible="false"></asp:Label>
        <asp:ScriptManagerProxy ID="ScriptManagerProxy23" runat="server"></asp:ScriptManagerProxy>
        <asp:UpdatePanel ID="UpdatePanel43" runat="server">
            <ContentTemplate>
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <span>Alert</span>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label13" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <strong>
                                    <asp:Label ID="lblrow1" Text="" runat="server"></asp:Label></strong>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <strong>
                                    <asp:Label ID="lblrow2" Text="" runat="server"></asp:Label></strong>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblrow3" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblrow4" runat="server"></asp:Label>
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
                                <asp:Button ID="btnlocOK" runat="server" Text="yes" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnlocOK_Click" />
                            </div>
                            <div class="col-sm-2 ">
                                <asp:Button ID="btnlocNO" runat="server" Text="No" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnlocNO_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <asp:UpdatePanel ID="UpdatePanel44" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button17" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PoConfBox" runat="server" Enabled="True" TargetControlID="Button17"
                PopupControlID="ppConfBox" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdateProgress ID="UpdateProgress6" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel45">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWaitloc" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWaitloc" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:Panel ID="ppConfBox" runat="server" align="center">
        <div runat="server" id="Div23" class="panel panel-info height120 width250">
            <asp:Label ID="Label17" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:ScriptManagerProxy ID="ScriptManagerProxy24" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel45" runat="server">
                <ContentTemplate>
                    <div class="panel panel-danger">
                        <div class="panel-heading">
                            <span>Alert</span>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:Label ID="Label18" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <strong>
                                        <asp:Label ID="lblerror" Text="" runat="server"></asp:Label></strong>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <strong>
                                        <asp:Label ID="lbldata" Text="" runat="server"></asp:Label></strong>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:Label ID="Label21" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:Label ID="lblmsg" runat="server"></asp:Label>
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
                                    <asp:Button ID="Button18" runat="server" Text="yes" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnokexceedqty_Click" />
                                </div>
                                <div class="col-sm-2 ">
                                    <asp:Button ID="Button19" runat="server" Text="No" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnnOexceedqty_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>


    <%-- Qty exceed validation --%>
    <asp:UpdatePanel ID="UpdatePanel46" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button20" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popQtyExc" runat="server" Enabled="True" TargetControlID="Button20"
                PopupControlID="Panel2" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdateProgress ID="UpdateProgress7" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel47">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblsWaitloc" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgsWaitloc" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:Panel ID="Panel2" runat="server" align="center">
        <div runat="server" id="Div16" class="panel panel-info height120 width250">
            <asp:Label ID="Label19" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:ScriptManagerProxy ID="ScriptManagerProxy25" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel47" runat="server">
                <ContentTemplate>
                    <div class="panel panel-danger">
                        <div class="panel-heading">
                            <span>Alert</span>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:Label ID="lblExcHdr" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <strong>
                                        <asp:Label ID="lblExcBody" Text="" runat="server"></asp:Label></strong>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <strong>
                                        <asp:Label ID="lblExcB1" Text="" runat="server"></asp:Label></strong>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:Label ID="lblExcMsg" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:Label ID="Label25" runat="server"></asp:Label>
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
                                    <asp:Button ID="btnQtyExcOk" runat="server" Text="yes" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnQtyExcOk_Click" />
                                </div>
                                <div class="col-sm-2 ">
                                    <asp:Button ID="btnQtyExcNo" runat="server" Text="No" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnQtyExcNo_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>
    <script>
        if (typeof jQuery == 'undefined') {
            alert('jQuery is not loaded');
        }
        Sys.Application.add_load(fun);
        function fun() {
            $(document).ready(function () {
                maintainScrollPosition();
                maintainScrollPositionItem();
            });
        }

    </script>
</asp:Content>
