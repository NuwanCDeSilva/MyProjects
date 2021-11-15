<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="BinToBinTransfer.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Inventory.BinToBinTransfer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/ucOutScan.ascx" TagPrefix="uc1" TagName="ucOutScan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <link href="<%=Request.ApplicationPath%>Css/bootstrap.css" rel="stylesheet" />
    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>
    <link href="<%=Request.ApplicationPath%>Css/style.css" rel="stylesheet" />

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
            var selectedvaldelitm = confirm("Do you need to remove this record ?");
            if (selectedvaldelitm) {
                document.getElementById('<%=txtconfirmdelete.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconfirmdelete.ClientID %>').value = "No";
            }
        };

        function ConfirmDeleteSerial() {
            var selectedvaldelser = confirm("Do you need to remove this record ?");
            if (selectedvaldelser) {
                document.getElementById('<%=txtconfirmdeleteSerial.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconfirmdeleteSerial.ClientID %>').value = "No";
            }
        };

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
                sticky: false,
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

        function Enable() {
            return;
        };

        function openFromBinSearch(e) {
            var evt = (e) ? e : window.event;
            var key = (evt.keyCode) ? evt.keyCode : evt.which;
            if (key == 113) {
                var bnItm = document.getElementById('<% =btnFromBin.ClientID %>');
                bnItm.focus();
                document.getElementById('<% = btnFromBin.ClientID %>').click();
            }
        };

        function openToSearch(e) {
            var evt = (e) ? e : window.event;
            var key = (evt.keyCode) ? evt.keyCode : evt.which;
            if (key == 113) {
                var bnItm = document.getElementById('<% =btnToBin.ClientID %>');
                bnItm.focus();
                document.getElementById('<% = btnToBin.ClientID %>').click();
            }
        };
    </script>
    <style type="text/css">
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
        runat="server" AssociatedUpdatePanelID="UpdatePanel3">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="txtconfirmclear" runat="server" />
            <asp:HiddenField ID="txtsaveconfirm" runat="server" />
            <asp:HiddenField ID="txtconfirmdelete" runat="server" />
            <asp:HiddenField ID="txtconfirmdeleteSerial" runat="server" />

            <div class="panel panel-default marginLeftRight5">
                <div class="row">
                    <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="col-sm-4  buttonRow buttonrowitemchange">
                                <div class="col-sm-5 ">
                                </div>
                                <div class="col-sm-4">
                                    <asp:LinkButton ID="btnSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmSave();" OnClick="btnSave_Click">
                                    <span class="glyphicon glyphicon-saved" aria-hidden="true" ></span>Save/Process
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-3">
                                    <asp:LinkButton ID="lbtnprintord" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmClearForm();" OnClick="lbtnprintord_Click">
                                    <span class="glyphicon glyphicon-refresh" aria-hidden="true" ></span>Clear
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="row">
                    <div class="panel-body">
                        <div class="col-sm-12">
                            <div class="col-sm-8">
                                <div class="row">
                                    <div class="panel panel-default ">
                                        <div class="panel-heading pannelheading  ">
                                            General Details
                                        </div>
                                        <div class="panel-body">
                                            <div class="col-sm-4">
                                                <div class="row">
                                                    <div class="col-sm-3 labelText1">
                                                        Date
                                                    </div>
                                                    <div>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="dtpDate" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                                onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                            <asp:CalendarExtender ID="calexdate" runat="server" TargetControlID="dtpDate"
                                                                PopupButtonID="lbtnimgselectdate" Format="dd/MMM/yyyy">
                                                            </asp:CalendarExtender>
                                                        </div>

                                                        <div id="caldv" class="col-sm-1 paddingLeft0" style="margin-left: -10px; margin-top: -2px">
                                                            <asp:LinkButton ID="lbtnimgselectdate" TabIndex="1" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="row">
                                                    <div class="col-sm-5 labelText1">
                                                        Manual Ref #
                                                    </div>
                                                    <div class="col-sm-7 paddingRight5">
                                                        <asp:TextBox ID="txtManualRef" runat="server" TabIndex="2" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="row">
                                                    <div class="col-sm-5 labelText1">
                                                        Other Ref
                                                    </div>
                                                    <div class="col-sm-7 paddingRight5">
                                                        <asp:TextBox ID="txtOtherRef" runat="server" TabIndex="3" CssClass="form-control" MaxLength="30"></asp:TextBox>
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
                                            <div class="col-sm-12">
                                                <div class="row">
                                                    <div class="col-sm-3 labelText1">
                                                        Remarks
                                                    </div>
                                                    <div class="col-sm-9">
                                                        <asp:TextBox ID="txtRemarks" runat="server" MaxLength="200" TabIndex="4" CssClass="form-control" TextMode="MultiLine" Style="resize: none"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4 ">
                                <div class="row">
                                    <div class="panel panel-default documenttypemargin">
                                        <div class="panel-heading pannelheading  ">
                                        </div>
                                        <div class="panel-body">
                                            <div class="col-sm-12">
                                                <div class="col-sm-3 labelText1 paddingLeft0">
                                                    From Bin
                                                </div>
                                                <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                    <asp:TextBox ID="txtFromBin" runat="server" Style="text-transform: uppercase" CssClass="form-control" AutoPostBack="true" onkeydown="openFromBinSearch(event)" OnTextChanged="txtFromBin_TextChanged" />
                                                </div>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="btnFromBin" runat="server" Style="text-transform: uppercase" CausesValidation="false" OnClick="btnFromBin_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="col-sm-12">
                                                <div class="col-sm-3 labelText1 paddingLeft0">
                                                    To Bin
                                                </div>
                                                <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                    <asp:TextBox ID="txtToBin" runat="server" CssClass="form-control" AutoPostBack="true" onkeydown="openToSearch(event)" OnTextChanged="txtToBin_TextChanged" />
                                                </div>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="btnToBin" runat="server" CausesValidation="false" OnClick="btnToBin_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="col-sm-12">
                                                <div class="col-sm-3 labelText1 paddingLeft0">
                                                    Sequence No
                                                </div>
                                                <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                    <asp:TextBox ID="txtUserSeqNo" TabIndex="5" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0">
                                                </div>
                                            </div>
                                            <div class="col-sm-12">
                                                <div class="col-sm-3 labelText1 paddingLeft0">
                                                </div>
                                                <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                    <asp:DropDownList ID="ddlSeqNo" runat="server" TabIndex="6" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlSeqNo_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="row">
                                    <uc1:ucOutScan runat="server" ID="ucOutScan" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 height5">
                                </div>
                            </div>
                            <div class="row">
                                <div class="panel-body">
                                    <div class="col-sm-12">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <ul id="myTab" class="nav nav-tabs">
                                                    <li class="active">
                                                        <a href="#Item" data-toggle="tab">Item</a>
                                                    </li>
                                                    <li>
                                                        <a href="#Serial" data-toggle="tab">Serial</a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div id="myTabContent" class="tab-content">
                                                    <div class="tab-pane fade in active" id="Item">
                                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                                <div class="col-sm-12 padding0">
                                                                    <asp:GridView ID="grdItems" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lbtndelete" runat="server" CausesValidation="false" OnClientClick="ConfirmDelete();" OnClick="lbtndelete_Click">
                                                                                                    <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                                                                    </asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Item">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblitri_itm_cd" runat="server" Text='<%# Bind("itri_itm_cd") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Description">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbldescgrd" runat="server" Text='<%# Bind("mi_longdesc") %>' Width="200px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Model">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblmodelgrd" runat="server" Text='<%# Bind("mi_model") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Current Status">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblcurstgrd_DESC" runat="server" Text='<%# Bind("itri_itm_stus_Desc") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Current Status" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblcurstgrd" runat="server" Text='<%# Bind("itri_itm_stus") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="New Status" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblnwstusgrd" runat="server" Text='<%# Bind("itri_note") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="New Status">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblnwstusgrdDesc" runat="server" Text='<%# Bind("itri_note_desc") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Unit Cost" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblunitcostgrd" runat="server" Text='<%# Bind("itri_unit_price","{0:N2}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="App.Qty">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblapqtygrd" runat="server" Text='<%# Bind("itri_app_qty","{0:N2}") %>' Width="15px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Pick Qty">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblitri_qty" runat="server" Text='<%# Bind("itri_qty","{0:N2}") %>' Width="15px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Line No" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblitri_line_no" runat="server" Text='<%# Bind("itri_line_no","{0:N2}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <div class="tab-pane fade" id="Serial">
                                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <ContentTemplate>
                                                                <div class="row">
                                                                    <div class="col-sm-12 ">
                                                                        <div class="row">
                                                                            <div class="col-sm-12 height5">
                                                                            </div>
                                                                        </div>
                                                                        <div class="panelscoll">
                                                                            <asp:GridView ID="grdSerial" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="lbtndeleteserial" runat="server" CausesValidation="false" OnClientClick="ConfirmDeleteSerial();" OnClick="lbtndeleteserial_Click">
                                                                                                    <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Item">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblitmgrd2" runat="server" Text='<%# Bind("tus_itm_cd") %>' Width="100px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Model">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblmodelgrd2" runat="server" Text='<%# Bind("tus_itm_model") %>' Width="75px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Current Status" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblstusgrd2" runat="server" Text='<%# Bind("tus_itm_stus") %>' Width="100px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Current Status">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblstusgrd2Desc" runat="server" Text='<%# Bind("tus_itm_stus_Desc") %>' Width="100px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="New Status" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblnwstusgrd2" runat="server" Text='<%# Bind("tus_new_status") %>' Width="100px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="New Status">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblnwstusgrd2Desc" runat="server" Text='<%# Bind("tus_new_status_desc") %>' Width="100px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Qty">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblqtygrd2" runat="server" Text='<%# Bind("tus_qty","{0:N2}") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="4%" HorizontalAlign="Center" />
                                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Serial 1">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblser1grd2" runat="server" Text='<%# Bind("tus_ser_1") %>' Width="75px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Serial 2">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblser2grd2" runat="server" Text='<%# Bind("tus_ser_2") %>' Width="100px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Serial 3" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblser2grd3" runat="server" Text='<%# Bind("tus_ser_3") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Bin" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblbingrd" runat="server" Text='<%# Bind("tus_bin") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Remarks">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblremerksgrd2" runat="server" Text='<%# Bind("tus_new_remarks") %>' Width="100px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Serial ID">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblseridgrd2" runat="server" Text='<%# Bind("tus_ser_id") %>' Width="100px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Request" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblrequest2" runat="server" Text='<%# Bind("tus_base_doc_no") %>' Width="100px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="BaseLineNo" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblbaseline" runat="server" Text='<%# Bind("tus_base_itm_line") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="ItemLine" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblItemline" runat="server" Text='<%# Bind("tus_itm_line") %>'></asp:Label>
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
                PopupControlID="pnlpopup" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlpopup" DefaultButton="lbtnSearch">
                <asp:ScriptManagerProxy ID="ScriptManagerProxy17" runat="server"></asp:ScriptManagerProxy>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div runat="server" id="test" class="panel panel-default height400 width850">
                            <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
                            <div class="panel panel-default">
                                <div class="panel-heading height30" id="divSearchheader">
                                    <div class="col-sm-11">
                                    </div>
                                    <div class="col-sm-1">
                                        <asp:LinkButton ID="btnClose" CausesValidation="false" runat="server" OnClick="btnClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <div class="col-sm-12" id="Div10" runat="server">
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
                                                        <asp:GridView ID="grdResult" CausesValidation="false" runat="server" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResult_SelectedIndexChanged" OnPageIndexChanging="grdResult_PageIndexChanging" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
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
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
