<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="ItemAllocationSetup.aspx.cs" Inherits="FastForward.SCMWeb.View.MasterFiles.Inventory.ItemAllocationSetup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>
        function ClearConfirm() {
            var selectedvalue = confirm("Do you want to Clear data?");
            if (selectedvalue) {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "No";
            }
        };
        function SaveConfirm() {
            var selectedvalue = confirm("Do you want to save this order ?");
            if (selectedvalue) {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "No";
            }
        };
        function CancelConfirm() {
            var selectedvalue = confirm("Do you want to remove selected item details ?");
            if (selectedvalue) {
                document.getElementById('<%=txtCancelconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtCancelconformmessageValue.ClientID %>').value = "No";
            }
        };
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
              && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
        function CheckAllItems(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdAlItem.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
              && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
    </script>
    <script>

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
        function DeleteConfirm() {
            var selectedvalue = confirm("Do you want to delete data?");
            if (selectedvalue) {
                document.getElementById('<%=txtDeleteconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtDeleteconformmessageValue.ClientID %>').value = "No";
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

        .txtalignright {
            text-align: right;
            margin-left: 20px;
        }

        .txtalignright2 {
            text-align: right;
            margin-left: 45px;
        }

        .marginleft {
            margin-left: 4px;
        }

        .gridHeaderAlignRight {
            text-align: left;
            width: 120px;
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

    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="txtClearlconformmessageValue" runat="server" />
            <asp:HiddenField ID="txtSavelconformmessageValue" runat="server" />
            <asp:HiddenField ID="txtUpdateconformmessageValue" runat="server" />
            <asp:HiddenField ID="txtApprovalconformmessageValue" runat="server" />
            <asp:HiddenField ID="txtCancelconformmessageValue" runat="server" />
            <asp:HiddenField ID="txtDeleteconformmessageValue" runat="server" />
            <asp:HiddenField ID="hiddQty" runat="server" />
            <div class="row">
                <div class="col-sm-12">
                    <div class="col-sm-8  buttonrow">
                        <div id="WarningItem" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">
                            <div class="col-sm-11">
                                <strong>Alert!</strong>
                                <asp:Label ID="lblWItem" runat="server"></asp:Label>
                            </div>
                            <div class="col-sm-1">
                                <asp:LinkButton ID="lbtnWItemok" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnWItemok_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </div>

                        </div>

                        <div id="SuccessItem" runat="server" visible="false" class="alert alert-success alert-dismissible" role="alert">
                            <div class="col-sm-11">
                                <strong>Success!</strong>
                                <asp:Label ID="lblSItem" runat="server"></asp:Label>

                            </div>
                            <div class="col-sm-1">
                                <asp:LinkButton ID="LinkButton7" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnWItemok_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </div>

                        </div>
                    </div>

                    <div class="col-sm-4  buttonRow">
                        <div class="col-sm-3"></div>
                        <div class="col-sm-3 paddingRight0">
                            <asp:LinkButton ID="lbtnSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lbtnAdd_Click" OnClientClick="SaveConfirm()">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-3 paddingRight0">
                            <asp:LinkButton ID="lbtnCancel" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="CancelConfirm()" OnClick="lbtnCancel_Click">
                                        <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>Cancel
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-3">
                            <asp:LinkButton ID="lbtnClear" runat="server" CssClass="floatRight" OnClientClick="ClearConfirm()" OnClick="lbtnClear_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-default marginLeftRight5">
                <div class="panel-heading"><strong>Item Allocation Setup</strong> </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-default">

                                <div class="panel-body">

                                    <div class="col-sm-2 paddingLeft0">
                                        <div class="row">
                                            <div class="col-sm-4 labelText1">
                                                Doc.Type
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <asp:DropDownList ID="ddlDocType" AutoPostBack="True" CausesValidation="false" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDocType_SelectedIndexChanged">
                                                    <asp:ListItem Text=" - - Select - - " Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Purchase Order" Value="PO" />
                                                    <asp:ListItem Text="GRN" Value="GRN" />
                                                    <asp:ListItem Text="AOD-IN-LOCAL" Value="AOD" />
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 ">
                                        <div class="row" runat="server" id="DivType" visible="false">
                                            <div class="row">
                                                <div class="col-sm-4 labelText1">
                                                    Type
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-10 ">
                                                    <asp:DropDownList ID="ddlPType" CausesValidation="false" runat="server" CssClass="form-control">
                                                        <asp:ListItem Text=" - - Select - - " Value="0" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Local" Value="L" />
                                                        <asp:ListItem Text="Imports" Value="I" />
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row" runat="server" id="DivLoc" visible="true">
                                            <div class="row">
                                                <div class="col-sm-4 labelText1">
                                                    Location
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-10 ">
                                                    <asp:TextBox runat="server" ID="txtlocation" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtlocation_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0 paddingRight0 ">
                                                    <asp:LinkButton ID="lbtnlocation" runat="server" CausesValidation="false" OnClick="lbtnlocation_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Document #
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-10">
                                                <asp:TextBox runat="server" ID="txtDocNo" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtDocNo_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0 paddingRight0 ">
                                                <asp:LinkButton ID="lbtnDocNo" runat="server" CausesValidation="false" OnClick="lbtnDocNo_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-5">
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Document Item Search
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-2 paddingLeft0 paddingRight0">
                                                <div class="col-sm-1">
                                                    <asp:RadioButton ID="optItem" runat="server" AutoPostBack="true" GroupName="Ststuse" OnCheckedChanged="optItem_CheckedChanged" />
                                                </div>
                                                <div class="col-sm-7 labelText2">
                                                    By Item
                                                </div>
                                            </div>
                                            <div class="col-sm-2 paddingLeft0 paddingRight0">
                                                <div class="col-sm-1">
                                                    <asp:RadioButton ID="optModel" runat="server" AutoPostBack="true" GroupName="Ststuse" OnCheckedChanged="optModel_CheckedChanged" />
                                                </div>
                                                <div class="col-sm-6 labelText2 paddingLeft0 paddingRight0">
                                                    By Model
                                                </div>

                                            </div>
                                            <div class="col-sm-2 paddingLeft0 paddingRight0" runat="server" id="DivPo" visible="false">
                                                <div class="col-sm-1">
                                                    <asp:RadioButton ID="optPo" runat="server" AutoPostBack="true" GroupName="Ststuse" />
                                                </div>
                                                <div class="col-sm-6 labelText2 paddingLeft0 paddingRight0">
                                                    Order #
                                                </div>

                                            </div>
                                            <div class="col-sm-6">
                                                <div class="col-sm-8 paddingLeft0">
                                                    <asp:TextBox runat="server" ID="txtItemSearch" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0 paddingRight0 ">
                                                    <asp:LinkButton ID="lbtnItem" runat="server" CausesValidation="false" OnClick="lbtnItem_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0 Lwidth">
                                                    <asp:LinkButton ID="lbtnFilter" runat="server" CausesValidation="false" OnClick="lbtnFilter_Click">
                                                        <span class="glyphicon glyphicon-search" style="font-size:20px" aria-hidden="true"></span>
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
                        <div class="col-sm-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    Document Item Details
                                </div>
                                <div class="panel-body panelscollbar height200">
                                    <asp:GridView ID="grdDocItem" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Add" Visible="true">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnAdd" runat="server" CausesValidation="false" Width="5px" OnClick="lbtnPOItemsAdd_Click">
                                                <span class="glyphicon glyphicon-arrow-down" aria-hidden="true" ></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_ItemCode" runat="server" Text='<%# Bind("Itemcode") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_Description" runat="server" Text='<%# Bind("Description") %>' Width="180px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Model">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_Model" runat="server" Text='<%# Bind("Model") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Brand">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_Brand" runat="server" Text='<%# Bind("Brand") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_Istatus" runat="server" Text='<%# Bind("IStaus") %>' Width="80px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_IstatusName" runat="server" Text='<%# Bind("mis_desc") %>' Width="80px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Doc Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_qty" runat="server" Text='<%# Bind("qty","{0:n2}") %>' Width="20px" CssClass="txtalignright"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Available Alloc Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_Aqty" runat="server" Text='<%# Bind("tqty","{0:n2}") %>' Width="20px" CssClass="txtalignright2"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qty to be Alloc" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_Aqty2" Visible="false" runat="server" Text='<%# Bind("tqty") %>' Width="20px" CssClass="txtalignright2"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Doc No">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_Idoc" runat="server" Text='<%# Bind("inb_doc_no") %>' Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_ILine" runat="server" Text='<%# Bind("inb_itm_line") %>' Width="10px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_Ibatch" runat="server" Text='<%# Bind("inb_batch_line") %>' Width="10px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <div class="col-sm-2 ">
                                        <div class="row">
                                            <div class="col-sm-4 labelText1">
                                                Company
                                            </div>
                                            <div class="col-sm-8">
                                                <asp:TextBox runat="server" ID="txtCompany" ReadOnly="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 ">
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Channel
                                            </div>
                                            <div class="col-sm-8 paddingLeft0 ">
                                                <asp:TextBox runat="server" ID="txtChannel" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtChannel_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0 paddingRight0 ">
                                                <asp:LinkButton ID="lbtnchannel" runat="server" CausesValidation="false" OnClick="lbtnchannel_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="row">
                                            <div class="col-sm-2 labelText1">
                                                Item
                                            </div>
                                            <div class="col-sm-10">
                                                <asp:TextBox runat="server" ID="txtItem" ReadOnly="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 ">
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Allocation Qty
                                            </div>
                                            <div class="col-sm-4 paddingLeft0 ">
                                                <asp:TextBox runat="server" onkeypress="return isNumberKey(event)" ID="txtQty" AutoPostBack="true" CausesValidation="false" OnTextChanged="txtQty_TextChanged" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0 paddingRight0 ">
                                                <asp:LinkButton ID="lbtnItemadd" runat="server" CausesValidation="false" OnClick="lbtnItemadd_Click">
                                                        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                </asp:LinkButton>

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
                                <div class="panel-heading">

                                    <div class="row">
                                        <div class="col-sm-2">
                                            Allocated Item Details
                                        </div>
                                        <div class="col-sm-2">
                                         
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-body panelscollbar height150">
                                    <asp:GridView ID="grdAlItem" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None"
                                        OnRowEditing="grdAlItem_RowEditing"
                                        CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="" Visible="true" ControlStyle-CssClass="marginleft">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="allchk" runat="server" Width="5px" onclick="CheckAllItems(this)"></asp:CheckBox>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_Req" runat="server" Checked="false" Width="5px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Channel " ControlStyle-CssClass="marginleft">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_channel" runat="server" Text='<%# Bind("Channel") %>' ToolTip='<%# Bind("Chnldes") %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Doc #">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_reqDate" runat="server" Text='<%# Bind("Doc") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_Item" runat="server" Text='<%# Bind("Item") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_Des" runat="server" Text='<%# Bind("Des") %>' ToolTip="" Width="150px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Model">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_rmodel" runat="server" Text='<%# Bind("Model") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_status" runat="server" Text='<%# Bind("status") %>' Width="40px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_statusName" runat="server" Text='<%# Bind("statusName") %>' Width="80px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Alloc Qty" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAqty" runat="server" Text='<%# Bind("Aqty","{0:n2}") %>' Width="20px" CssClass="txtalignright2"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Alloc Qty">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtAqty" onkeypress="return isNumberKey(event)" runat="server" Text='<%# Bind("Aqty") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="col_Aqty" runat="server" Text='<%# Bind("Aqty","{0:n2}") %>' Width="20px" CssClass="txtalignright"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Balance">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_Bqty" runat="server" Text='<%# Bind("Bqty","{0:n2}") %>' Width="20px" CssClass="txtalignright"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MRN Qty" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_MRN" runat="server" Text='<%# Bind("MRN","{0:n2}") %>' Width="20px" CssClass="txtalignright"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="seq" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_seq" runat="server" Text='<%# Bind("seq") %>' Width="8px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>

                                                    <div id="editbtndiv">
                                                        <asp:LinkButton ID="lbtnedititem" CausesValidation="false" CommandName="Edit" runat="server">
                                                            <span class="glyphicon glyphicon-pencil" aria-hidden="true" style="font-size:12px"></span>
                                                        </asp:LinkButton>
                                                    </div>

                                                </ItemTemplate>
                                                <EditItemTemplate>

                                                    <asp:LinkButton ID="lbtnUpdateEdit" CausesValidation="false" runat="server" OnClick="lbtnUpdateEdit_Click">
                                                           <span class="glyphicon glyphicon-ok" aria-hidden="true" style="font-size:12px"></span>
                                                    </asp:LinkButton>


                                                    <asp:LinkButton ID="lbtnCancelEdit" CausesValidation="false" OnClick="lbtnCancelEdit_Click" runat="server">
                                                                                            <span class="glyphicon glyphicon-remove" aria-hidden="true" style="font-size:12px"></span>
                                                    </asp:LinkButton>

                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remove">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnRemove" runat="server" CausesValidation="false" OnClientClick="DeleteConfirm()" Width="5px" OnClick="lbtnRemove_Click">
                                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true" ></span>
                                                    </asp:LinkButton>
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

        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserPopoup" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="pnlpopup" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
        <ContentTemplate>
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
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="col-sm-5">
                                    <div class="row">
                                        <div class="col-sm-2 labelText1">
                                            From
                                        </div>
                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                            <asp:TextBox runat="server" ReadOnly="true" ID="txtFDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnFDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtFDate"
                                                PopupButtonID="lbtnFDate" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-2 labelText1">
                                            To
                                        </div>
                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                            <asp:TextBox runat="server" ReadOnly="true" ID="txtTDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnTDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtTDate"
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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy10" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                            <ContentTemplate>
                                                <div class="col-sm-8 paddingRight5">
                                                    <asp:DropDownList ID="ddlSearchbykey" runat="server" class="form-control">
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
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdResult" CausesValidation="false" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResult_SelectedIndexChanged" OnPageIndexChanging="grdResult_PageIndexChanging">
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

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserPopoup2" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="pnlpopup2" CancelControlID="btnClose2" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpopup2" DefaultButton="lbtnSearch">
        <div runat="server" id="Div1" class="panel panel-default height400 width700">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="btnClose2" runat="server">
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
                            <div class="col-sm-12" id="search" runat="server">
                                <div class="col-sm-2 labelText1">
                                    Search by key
                                </div>
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
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
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnSearch2" runat="server" OnClick="lbtnSearch2_Click">
                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                            </asp:LinkButton>
                                        </div>
                                    </ContentTemplate>

                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtSearchbyword2" />
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
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdResult2" CausesValidation="false" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResult2_SelectedIndexChanged" OnPageIndexChanging="grdResult2_PageIndexChanging">
                                            <Columns>
                                                <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="5" />

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

</asp:Content>
