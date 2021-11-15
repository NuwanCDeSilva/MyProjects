<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="ChangeItemStatus.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Inventory.ChangeItemStatus" %>

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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

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
                            <div class="col-sm-8">
                                  <div id="Information" runat="server" visible="false" class="alert alert-success alert-info" role="alert">
                            <div class="col-sm-11">
                                <strong>Info!</strong>
                                <asp:Label ID="lblBackDateInfor" runat="server"></asp:Label>
                                <asp:Label ID="lblH1" runat="server" Text="Label"></asp:Label>
                            </div>
                        </div>
                            </div>
                           
                            <div class="col-sm-4  buttonRow buttonrowitemchange">

                                <div class="col-sm-2 ">
                                </div>
                                <div class="col-sm-1 ">
                                </div>
                                <div class="col-sm-1 ">
                                </div>

                                <div class="col-sm-2">

                                    <asp:LinkButton ID="btnSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmSave();" OnClick="btnSave_Click">
                                    <span class="glyphicon glyphicon-saved" aria-hidden="true" ></span>Save/Process
                                    </asp:LinkButton>
                                </div>

                                <div class="col-sm-2">
                                    <asp:LinkButton ID="lbtnprintord" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmClearForm();" OnClick="lbtnprintord_Click">
                                    <span class="glyphicon glyphicon-refresh" aria-hidden="true" ></span>Clear
                                    </asp:LinkButton>
                                </div>

                                <div class="col-sm-4 ">
                                    <asp:LinkButton ID="lbtnupload" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lbtnupload_Click">
                                     <span class="glyphicon glyphicon-upload"></span>Upload Excel
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

                                    <div class="panel panel-default">

                                        <div class="panel-heading pannelheading">
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

                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>

                                            <div class="col-sm-4">
                                                <div class="row">

                                                    <div class="col-sm-12 labelText1">
                                                        Sequence No
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="col-sm-4">
                                                <div class="row">
                                                    <div class="col-sm-10 paddingRight5 stockchangeseqnomargin">
                                                        <asp:TextBox ID="txtUserSeqNo" TabIndex="5" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="col-sm-4">
                                                <div class="row">
                                                    <div class="col-sm-10 paddingRight5 stockchangeseqnomargin">
                                                        <asp:DropDownList ID="ddlSeqNo" runat="server" TabIndex="6" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlSeqNo_SelectedIndexChanged"></asp:DropDownList>

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

                            <div class="col-sm-4 ">

                                <div class="row">

                                    <div class="panel panel-default GeneralDetailGrd documenttypemargin">

                                        <div class="panel-heading pannelheading  ">
                                        </div>

                                        <div class="panel-body">

                                            <div class="col-sm-12">
                                                <div class="row">

                                                    <div class="col-sm-3 labelText1">
                                                        Type
                                                    </div>

                                                    <div class="col-sm-9 paddingRight5">
                                                        <asp:DropDownList ID="ddlAdjTypeSearch" runat="server" TabIndex="7" AutoPostBack="true" CssClass="form-control">
                                                            <asp:ListItem>ADJ+</asp:ListItem>
                                                            <asp:ListItem>ADJ-</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>

                                            <div class="col-sm-12">
                                                <div class="row">

                                                    <div class="col-sm-3 labelText1">
                                                        Document No
                                                    </div>
                                                    <div class="col-sm-8 paddingRight5">
                                                        <asp:TextBox ID="txtDocumentNo" AutoPostBack="true" OnTextChanged="txtDocumentNo_TextChanged" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnmodelfind" runat="server" TabIndex="8" OnClick="lbtnmodelfind_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>

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
                                                                <div class="row ">
                                                                    <div class="col-sm-12 ">

                                                                        <div class="row">
                                                                            <div class="col-sm-12 height5">
                                                                            </div>
                                                                        </div>
                                                                        <div class="panelscoll">
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
                                                                                            <asp:Label ID="lblitri_itm_cd" runat="server" Text='<%# Bind("itri_itm_cd") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="New Item Code">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblitri_itm_cdnew" runat="server" Text='<%# Bind("itri_mitm_cd") %>' Width="100px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Description">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbldescgrd" runat="server" Text='<%# Bind("mi_longdesc") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Model">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblmodelgrd" runat="server" Text='<%# Bind("mi_model") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Current Status">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblitri_itm_stus_desc" runat="server" Text='<%# Bind("itri_itm_stus_desc") %>'></asp:Label>
                                                                                            <asp:Label ID="lblcurstgrd" runat="server" Text='<%# Bind("itri_itm_stus") %>' Visible="false"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="New Status">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblitri_note_desc" runat="server" Text='<%# Bind("itri_note_desc") %>'></asp:Label>
                                                                                            <asp:Label ID="lblnwstusgrd" runat="server" Text='<%# Bind("itri_note") %>' Visible="false"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Unit Cost" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblunitcostgrd" runat="server" Text='<%# Bind("itri_unit_price") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="App.Qty">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblapqtygrd" runat="server" Text='<%# Bind("itri_app_qty","{0:N2}") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Pick Qty">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblitri_qty" runat="server" Text='<%# Bind("itri_qty","{0:N2}") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                                        <HeaderStyle HorizontalAlign="Right" />
                                                                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Line No" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblitri_line_no" runat="server" Text='<%# Bind("itri_line_no") %>'></asp:Label>
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

                                                                                    <asp:TemplateField HeaderText="Current Status">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbltus_itm_stus" runat="server" Text='<%# Bind("tus_itm_stus_desc") %>' Width="100px"></asp:Label>
                                                                                            <asp:Label ID="lblstusgrd2" runat="server" Text='<%# Bind("tus_itm_stus") %>' Width="100px" Visible="false"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="New Status">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbltus_new_status" runat="server" Text='<%# Bind("tus_new_status_desc") %>' Width="100px"></asp:Label>
                                                                                            <asp:Label ID="lblnwstusgrd2" runat="server" Text='<%# Bind("tus_new_status") %>' Width="100px" Visible="false"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Qty">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblqtygrd2" runat="server" Text='<%# Bind("tus_qty","{0:N2}") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="4%" HorizontalAlign="Center" />
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


<%--    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnexcel" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpexcel" runat="server" Enabled="True" TargetControlID="btnexcel"
                PopupControlID="pnlpopupExcel" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeaderExcel" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpopupExcel" DefaultButton="lbtnSearch">
        <div runat="server" id="Div1" class="panel panel-primary Mheight">

            <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="LinkButton2" runat="server">
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

                            <div class="col-sm-6">
                                <div class="row">

                                    <div class="col-sm-4 labelText1">
                                        File Name
                                    </div>
                                    <div class="col-sm-8">
                                        <asp:FileUpload ID="fileupexcelupload" runat="server" />
                                    </div>

                                </div>
                            </div>

                            <div class="col-sm-6">
                                <div class="row">
                                    <div class="col-sm-4 labelText1">
                                        <asp:RadioButtonList ID="rbHDR" runat="server" RepeatDirection="Horizontal" Enabled="false" Visible="false">
                                            <asp:ListItem Text="Yes" Value="Yes" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>


                                    <div class="col-sm-8">
                                        <asp:LinkButton ID="lbtnuploadexcel" runat="server" OnClick="lbtnuploadexcel_Click">
                                        <span class="glyphicon glyphicon-upload" aria-hidden="true">Upload</span>
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
                                    <div class="col-sm-12 labelText1">
                                        <asp:Label runat="server" ID="lblwarningEXCEL" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>

                </div>
            </div>
        </div>
    </asp:Panel>--%>

       <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="excelUpload" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="pnlexcel" CancelControlID="btnClose_excel" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel11" runat="server">

        <ContentTemplate>

            <asp:Panel runat="server" ID="pnlexcel">
                <div runat="server" id="dv" class="panel panel-default height45 width700 ">

                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="btnClose_excel" runat="server" >
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <%--<span>Commen Search</span>--%>
                            <div class="col-sm-11">
                                <strong>Excel Upload</strong>
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12 paddingLeft0 paddingRight0">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="Label" Visible="false"></asp:Label>
                                        <asp:Label ID="lblsuccess2" runat="server" ForeColor="Green" Visible="false"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height10">
                                    </div>
                                </div>

                                <div class="row">
                                    <asp:Panel runat="server" ID="pnlupload" Visible="true">
                                        <div class="col-sm-12" id="Div6" runat="server">
                                            <div class="col-sm-8 paddingRight5">



                                                <div class="row">
                                                    <div class="col-sm-7 labelText1">
                                                        <asp:FileUpload ID="fileupexcelupload" runat="server" />
                                                    </div>
                                                    <div class="col-sm-2 paddingRight5">
                                                        <asp:Button ID="btnAsyncUpload" runat="server" Text="Async_Upload" Visible="false" />
                                                        <asp:Button ID="btnupload" class="btn btn-warning btn-xs" runat="server" Text="Upload" OnClick="btnUpload_Click" />
                                                    </div>

                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height20">
                                                </div>
                                            </div>



                                        </div>
                                    </asp:Panel>

                                    <%--<div class="row">--%>
                                    <%-- <div class="col-sm-12 ">
                                <div id="divUpcompleted" visible="false" runat="server" class="alert alert-info alert-success" role="alert">
                                    <div class="col-sm-9">
                                        <strong>Alert!</strong>
                                        <asp:Label ID="Label4" Text="Excel file upload completed. Do you want to process?" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="btnprocess" class="btn btn-success btn-xs" runat="server" Text="Process" OnClick="btnprocess_Click" />
                                    </div>
                                </div>
                            </div>--%>
                                </div>
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
                <div runat="server" id="test" class="panel panel-default width950 height450">

                    <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>

                    <div class="panel-heading">
                        <asp:LinkButton ID="btnClose" CausesValidation="false" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                        </asp:LinkButton>
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
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
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

            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
