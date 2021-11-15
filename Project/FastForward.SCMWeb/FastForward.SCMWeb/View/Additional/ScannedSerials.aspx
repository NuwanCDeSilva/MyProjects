<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="ScannedSerials.aspx.cs" Inherits="FastForward.SCMWeb.View.Additional.ScannedSerials" %>

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

    <script>
        function DeleteConfirm() {
            var selectedvalue = confirm("Do you want to delete data?");
            if (selectedvalue) {
                document.getElementById('<%=txtDeleteconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtDeleteconformmessageValue.ClientID %>').value = "No";
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
        function CheckBoxCheck(rb) {
            debugger;
            var gv = document.getElementById("<%=grdpickserial.ClientID%>");
            var chk = gv.getElementsByTagName("input");
            var row = rb.parentNode.parentNode;
            for (var i = 0; i < chk.length; i++) {
                if (chk[i].type == "checkbox") {
                    if (chk[i].checked && chk[i] != rb) {
                        chk[i].checked = false;
                        break;
                    }
                }
            }
        }
        function CheckAll(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdpickserial.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <asp:HiddenField ID="txtACancelconformmessageValue" runat="server" />
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
       <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait2" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait2" runat="server"
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
                        <asp:LinkButton ID="lbtnDelete"  runat="server" OnClick="lbtnDelete_TextChanged" CssClass="floatRight" OnClientClick="DeleteConfirm()">
                                        <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>Remove
                        </asp:LinkButton>
                    </div>
                    <div class="col-sm-3 paddingRight0">
                        <asp:LinkButton ID="lbtnview"  runat="server" OnClick="lbtnview_TextChanged" CssClass="floatRight">
                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>View
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
            <strong>Scanned Serial </strong>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-sm-12 height10">
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-3 paddingRight5">
                        <div class="panel panel-default">
                            <div class="panel-heading paddingtopbottom0">
                                Scanned Serial 
                            </div>

                            <div class="panel-body">

                                <div class="row">
                                    <div class="col-sm-3 labelText1">
                                        Item Code
                                    </div>
                                    <div class="col-sm-5 paddingRight5">
                                        <asp:TextBox runat="server" ID="txtscanItem" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtscanItem_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-1 paddingLeft0">
                                        <asp:LinkButton ID="lbtnScanItem" CausesValidation="false" runat="server" OnClick="lbtnScanItem_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                 <div class="row">
                                    <div class="col-sm-3 labelText1">
                                        Serial I
                                    </div>
                                    <div class="col-sm-5 paddingRight5">
                                        <asp:TextBox runat="server" ID="txtserial" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtscanItem_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-1 paddingLeft0">
                                        <asp:LinkButton ID="LinkButton1" Visible="false" CausesValidation="false" runat="server" OnClick="lbtnScanItem_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                
                                <div class="row">
                                    <div class="col-sm-3 labelText1">
                                        Description :
                                    </div>
                                    <div class="col-sm-9 paddingRight5">

                                        <asp:Label runat="server" ID="lblItemDescription"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3 labelText1">
                                        Model : 
                                    </div>
                                    <div class="col-sm-9 paddingRight5">
                                        <asp:Label runat="server" ID="lblItemModel"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3 labelText1">
                                        Brand : 
                                    </div>
                                    <div class="col-sm-9 paddingRight5">
                                        <asp:Label runat="server" ID="lblItemBrand"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3 labelText1">
                                        Document No :
                                    </div>
                                    <div class="col-sm-9 paddingRight5">
                                        <asp:Label runat="server" ID="lblpickdocno"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3 labelText1">
                                        Pick User :
                                    </div>
                                    <div class="col-sm-9 paddingRight5">
                                        <asp:Label runat="server" ID="lblpickuser"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4 labelText1">
                                        Pick Date/Time :
                                    </div>
                                    <div class="col-sm-6 paddingRight5">
                                        <asp:Label runat="server" ID="lblpickdatetime"></asp:Label>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="col-sm-9 paddingLeft5">
                        <div class="panel panel-default">
                            <div class="panel-heading paddingtopbottom0">
                                Pick Serial
                            </div>
                            <div class="panel-body panelscollbar height400">
                                <asp:GridView ID="grdpickserial" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="" Visible="true">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="allchk" runat="server" Width="5px" onclick="CheckAll(this)"></asp:CheckBox>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_Req" AutoPostBack="true" runat="server" onclick="CheckBoxCheck(this);"  Checked="false" Width="15px" OnCheckedChanged="chk_Req_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Company " Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="col_company" runat="server" Text='<%# Bind("INS_COM") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Location" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="col_Location" runat="server" Text='<%# Bind("INS_LOC") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Code">
                                            <ItemTemplate>
                                                <asp:Label ID="col_ItmCode" runat="server" Text='<%# Bind("INS_ITM_CD") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Description">
                                            <ItemTemplate>
                                                <asp:Label ID="col_ItmDesc" runat="server" Text='<%# Bind("tmpItmDesc") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Modle">
                                            <ItemTemplate>
                                                <asp:Label ID="col_Modle" runat="server" Text='<%# Bind("tmpItmModel") %>' ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Brand" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="col_Brand" runat="server" Text='<%# Bind("tmpItmBrand") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Status">
                                            <ItemTemplate>
                                                <asp:Label ID="col_ItemStatusDesc" runat="server" Text='<%# Bind("tmpItmStsDesc") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Serial ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="col_SerialID" runat="server" Text='<%# Bind("INS_SER_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Serial I">
                                            <ItemTemplate>
                                                <asp:Label ID="col_Serial1" runat="server" Text='<%# Bind("INS_SER_1") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Serial II">
                                            <ItemTemplate>
                                                <asp:Label ID="col_Serial2" runat="server" Text='<%# Bind("INS_SER_2") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inward Document">
                                            <ItemTemplate>
                                                <asp:Label ID="col_InwardDoc" runat="server" Text='<%# Bind("INS_DOC_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inward Date">
                                            <ItemTemplate>
                                                <asp:Label ID="col_InwardDocDate" runat="server" Text='<%# Bind("INS_DOC_DT","{0:dd/MMM/yyyy}") %>'></asp:Label>
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
</asp:Content>
