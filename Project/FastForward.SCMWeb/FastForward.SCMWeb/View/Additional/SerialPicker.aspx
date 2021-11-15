<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="SerialPicker.aspx.cs" Inherits="FastForward.SCMWeb.View.Additional.Serial_Picker" %>

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
    </script>

    <script>
        function SaveConfirm() {
            var selectedvalue = confirm("Do you want to save data?");
            if (selectedvalue) {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "No";
            }
        };

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
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
              && (charCode < 48 || charCode > 57))
                return false;
            return true;
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="txtClearlconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtSavelconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtUpdateconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtApprovalconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtCancelconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtDeleteconformmessageValue" runat="server" />
    <asp:UpdateProgress ID="UpdateProgressbar" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait1" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait1" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-sm-8  buttonrow">
                </div>
                <div class="col-sm-4  buttonRow">

                    <div class="col-sm-3">
                    </div>
                    <div class="col-sm-3 paddingRight0">
                        <asp:LinkButton ID="LinkButton1" Visible="true" runat="server" CssClass="floatRight" OnClick="lbtnview_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>View serial
                        </asp:LinkButton>
                    </div>

                    <div class="col-sm-3 paddingRight0">
                        <asp:LinkButton ID="lbtnfin" Visible="true" runat="server" CssClass="floatRight" OnClientClick="SaveConfirm()" OnClick="lbtnfin_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>Finish
                        </asp:LinkButton>
                    </div>

                    <div class="col-sm-3">
                        <asp:LinkButton ID="lblUClear" runat="server" CssClass="floatRight" OnClick="lbtnClear_Click" OnClientClick="ClearConfirm()">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="panel panel-default marginLeftRight5 paddingbottom0">

        <div class="panel-heading paddingtopbottom0">
            <strong>Serial Picker</strong>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-sm-12 height10">
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-4 paddingRight5">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                Document Details
                            </div>

                            <div class="panel-body">

                                <div class="row">
                                    <div class="col-sm-2 labelText1">
                                        Category
                                    </div>
                                    <div class="col-sm-3 paddingRight5 paddingLeft0">
                                        <asp:DropDownList ID="ddltypes" AutoPostBack="true" TabIndex="1" runat="server" CssClass="form-control ControlText" OnSelectedIndexChanged="ddltypes_SelectedIndexChanged">
                                            <%--<asp:ListItem Text="Stock In" Value="1"></asp:ListItem>--%>
                                            <asp:ListItem Text="Stock Out" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Stock In" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2 labelText1 paddingRight0 paddingLeft0">
                                        Doc Type
                                    </div>
                                    <div class="col-sm-4 paddingRight5 paddingLeft0">
                                        <asp:DropDownList ID="ddldoctype" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddldoctype_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-1 paddingLeft0">
                                        <asp:LinkButton ID="lbtnDocSearch" Visible="false" runat="server" CausesValidation="false">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-3 labelText1 ">
                                        Doc #
                                    </div>
                                    <div class="col-sm-4 paddingRight5 paddingLeft0">
                                        <asp:TextBox runat="server" ID="txtDocNo" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtDocNo_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-1 paddingLeft0">
                                        <asp:LinkButton ID="lbtndocno" runat="server" CausesValidation="false" OnClick="lbtndocno_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-3 padding0  labelText1">
                                            Doc.Tot.Qty
                                        </div>
                                        <div class="col-sm-2 labelText1 paddingRight5 paddingLeft0">
                                            <asp:Label ID="lblDocTotQty" runat="server" CssClass="Color1 fontWeight900"></asp:Label>
                                        </div>
                                        <div class="col-sm-3 labelText1">
                                            Tot.Scan.Qty
                                        </div>
                                        <div class="col-sm-2 labelText1 paddingRight5 paddingLeft0">
                                            <asp:Label ID="lblTotalqty" runat="server" CssClass="Color1 fontWeight900"></asp:Label>
                                        </div>
                                    </div>
                                </div>


                                <div class="row">
                                    <div class="col-sm-3 labelText1">
                                        Item Code
                                    </div>
                                    <div class="col-sm-4 paddingRight5 paddingLeft0">
                                        <asp:TextBox runat="server" ID="txtItemcode" Enabled="false" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtItemcode_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-1 paddingLeft0">
                                        <asp:LinkButton ID="lbtnItemcode" runat="server" CausesValidation="false" OnClick="lbtnItemcode_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                        <asp:Label ID="lblcost" runat="server" Visible="false"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-2 labelText1">
                                        Bin Code
                                    </div>
                                    <div class="col-sm-3 paddingRight5 paddingLeft0">
                                        <asp:DropDownList ID="ddlBincode" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlBincode_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3 labelText1">
                                        Item Status
                                    </div>
                                    <div class="col-sm-3 paddingRight5 paddingLeft0">
                                        <asp:DropDownList ID="ddlitemStatus" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control" OnTextChanged="ddlitemStatus_SelectedIndexChanged"></asp:DropDownList>
                                    </div>


                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-2 labelText1">
                                        Serial #
                                    </div>
                                    <asp:Panel runat="server" DefaultButton="btnserial">
                                        <div class="col-sm-1">
                                            <asp:Button ID="btnserial" runat="server" OnClick="btnqty_Click" Text="Submit" Style="display: none;" />
                                        </div>
                                        <div class="col-sm-4 paddingRight5 paddingLeft0">
                                            <asp:TextBox runat="server" ID="txtserial" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                        </div>


                                    </asp:Panel>
                                    <div class="col-sm-1 paddingLeft0">
                                        <asp:LinkButton ID="lbtnserial" runat="server" CausesValidation="false" OnClick="lbtnserial_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-1 labelText1">
                                        Qty
                                    </div>

                                    <asp:Panel runat="server" DefaultButton="btnqty">
                                        <div class="col-sm-1">
                                            <asp:Button ID="btnqty" runat="server" OnClick="txtqty_TextChanged" Text="Submit" Style="display: none;" />
                                        </div>
                                        <div class="col-sm-1 paddingRight5 paddingLeft0">
                                            <asp:TextBox runat="server" ID="txtqty" oncopy="return false"
                                                onpaste="return false"
                                                oncut="return false" onkeypress="return isNumberKey(event)" CssClass="form-control"></asp:TextBox>
                                        </div>


                                    </asp:Panel>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-2 labelText1">
                                        Serial # II
                                    </div>
                                    <asp:Panel runat="server" DefaultButton="btnserial">
                                        <div class="col-sm-1">
                                            <asp:Button ID="Button1" runat="server" OnClick="btnqty_Click" Text="Submit" Style="display: none;" />
                                        </div>
                                        <div class="col-sm-4 paddingRight5 paddingLeft0">
                                            <asp:TextBox runat="server" ID="txtserila2" Enabled="false" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                        </div>


                                    </asp:Panel>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3 labelText1">
                                        <strong>Description</strong>
                                    </div>
                                    <div class="col-sm-9 labelText1 paddingRight5 paddingLeft0">
                                        <asp:Label ID="lblitemDes" runat="server" CssClass="Color1 fontWeight900"></asp:Label>
                                    </div>


                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3 labelText1">
                                        <strong>Model</strong>
                                    </div>
                                    <div class="col-sm-8 labelText1 paddingRight5 paddingLeft0">
                                        <asp:Label ID="lblmodel" runat="server" CssClass="Color1 fontWeight900"></asp:Label>
                                    </div>


                                </div>
                                <div class="row">
                                    <div class="col-sm-2 labelText1">
                                        Serialized
                                    </div>
                                    <div class="col-sm-1 labelText1 paddingRight5 paddingLeft0">
                                        <asp:Label ID="lblserialize" runat="server" CssClass="Color1 fontWeight900"></asp:Label>
                                    </div>
                                    <div class="col-sm-3 labelText1">
                                        Sub Serialized:
                                    </div>
                                    <div class="col-sm-2 labelText1 paddingRight5 paddingLeft0">
                                        <asp:Label ID="lblsubsrial" runat="server" CssClass="Color1 fontWeight900"></asp:Label>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-2 labelText1">
                                        Doc.Qty:
                                    </div>
                                    <div class="col-sm-1 labelText1 paddingRight5 paddingLeft0">
                                        <asp:Label ID="lbldocqty" runat="server" CssClass="Color1 fontWeight900"></asp:Label>
                                    </div>
                                    <div class="col-sm-2 labelText1">
                                        Scan.Qty:
                                    </div>
                                    <div class="col-sm-1 labelText1 paddingRight5 paddingLeft0">
                                        <asp:Label ID="lblscanQty" runat="server" CssClass="Color1 fontWeight900"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="row">

                                    <div class="col-sm-3 labelText1">
                                        Chassis No:
                                    </div>
                                    <div class="col-sm-2 labelText1 paddingRight5 paddingLeft0">
                                        <asp:Label ID="lblchassisno" runat="server" CssClass="Color1 fontWeight900"></asp:Label>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="row">

                                    <div class="col-sm-3 labelText1">
                                        Last scan serial:
                                    </div>
                                    <div class="col-sm-2 labelText1 paddingRight5 paddingLeft0">
                                        <asp:Label ID="lbllastserial" runat="server" CssClass="Color1 fontWeight900"></asp:Label>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-8 paddingLeft5">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-8">
                                            Pick Serial
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="col-sm-1">
                                                <asp:CheckBox Text="" ID="chkUserWise" runat="server" />
                                            </div>
                                            <div class="col-sm-8 padding0">
                                                User Serials
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-body" style="height:340px; overflow-x:scroll">
                                <asp:GridView ID="grdpickserial" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnser_Remove" runat="server" CausesValidation="false" OnClientClick="DeleteConfirm()" Width="5px" OnClick="lbtnser_Remove_Click">
                                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true" ></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bin ">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltus_bin" runat="server" Text='<%# Bind("tus_bin") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Scan Item Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltus_itm_cd" runat="server" Text='<%# Bind("tus_itm_cd") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltus_itm_stus" runat="server" Visible="false" Text='<%# Bind("tus_itm_stus") %>'></asp:Label>
                                                <asp:Label ID="lblTus_itm_stus_Desc" runat="server" Text='<%# Bind("Tus_itm_stus_Desc") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Serial #/Engine #">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltus_ser_1" runat="server" Text='<%# Bind("tus_ser_1") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Serial ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltus_ser_id" runat="server" Text='<%# Bind("tus_ser_id") %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltus_qty" runat="server" Text='<%# Bind("tus_qty" ,"{0:N2}") %>'></asp:Label>
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
                                        <asp:GridView ID="grdResult" AutoGenerateColumns="False" CausesValidation="false" runat="server" GridLines="None" CssClass="table table-hover table-striped" PagerStyle-CssClass="cssPager" AllowPaging="True" OnPageIndexChanging="grdResult_PageIndexChanging">
                                            <Columns>
                                                <%--<asp:ButtonField Text="select" CommandName="Select" ItemStyle-Width="10" />--%>
                                                <asp:TemplateField HeaderText="Select">

                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btndgvpendSelect" CausesValidation="false" OnClick="btndgvpendSelect_Click" runat="server">
                                                            <span class="glyphicon glyphicon-arrow-right" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("Item_Code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Model">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblModel" runat="server" Text='<%# Bind("Model") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Line No" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLineNo" runat="server" Text='<%# Bind("Line_No") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQty" runat="server" Text='<%# Bind("Qty","{0:#,##0.####}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="gridHeaderAlignRight" />
                                                    <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pick Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPickQty" runat="server" Text='<%# Bind("Pick_Qty","{0:#,##0.####}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                      <ItemStyle CssClass="gridHeaderAlignRight" />
                                                    <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="cssPager" />
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

    <asp:UpdatePanel runat="server" ID="upSearch">
        <ContentTemplate>
            <asp:Button ID="btn1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupSearch" runat="server" Enabled="True" TargetControlID="btn1"
                PopupControlID="pnlSearch" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlSearch" DefaultButton="lbtnSearch">
        <div runat="server" id="Div1" class="panel panel-primary" style="padding: 5px;">
            <div class="panel panel-default" style="height: 350px; width: 700px;">
                <div class="panel-heading" style="height: 25px;">
                    <div class="col-sm-11"></div>
                    <div class="col-sm-1">
                        <asp:LinkButton ID="lbtnClose" runat="server" OnClick="lbtnClose_Click">
                             <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="row height16">
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="col-sm-4 labelText1">
                                Search by key
                            </div>
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-6 paddingRight5">
                                        <asp:DropDownList ID="ddlSerByKey" runat="server" class="form-control" AutoPostBack="false">
                                        </asp:DropDownList>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                        <div class="col-sm-6">
                            <div class="col-sm-4 labelText1">
                                Search by word
                            </div>
                            <div class="col-sm-6 paddingRight5">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtSerByKey" CausesValidation="false" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="lbtnSearch1_Click"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-1 paddingLeft0">
                                        <asp:LinkButton ID="lbtnSearch1" runat="server" OnClick="lbtnSearch1_Click">
                                     <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>
                                </ContentTemplate>

                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="txtSerByKey" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12 height16">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy8" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="dgvResult" CausesValidation="false" runat="server" AllowPaging="True"
                                        GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" ShowHeaderWhenEmpty="true"
                                        EmptyDataText="No data found..."
                                        OnPageIndexChanging="dgvResult_PageIndexChanging" OnSelectedIndexChanged="dgvResult_SelectedIndexChanged">
                                        <Columns>
                                            <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="12" />
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

</asp:Content>
