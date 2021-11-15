<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="ProductConditionUpdateEntry.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Inventory.ProductConditionUpdateEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script type="text/javascript">
        function ConfDelete() {
            var selectedvalueOrd = confirm("Do you want to delete ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfirmClearForm() {
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
            var selectedvalueclear = confirm("Do you want to clear all details ?");
            if (selectedvalueclear) {
                document.getElementById('<%=txtconfirmclear.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconfirmclear.ClientID %>').value = "No";
            }
        };

        function ConfirmSave() {
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
            var selectedvalsave = confirm("Do you want to confirm ?");
            if (selectedvalsave) {
                document.getElementById('<%=txtsaveconfirm.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtsaveconfirm.ClientID %>').value = "No";
            }
        };

        function ConfirmSelect() {
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
            var selectedvalsave = confirm("Do you want to select this document ?");
            if (selectedvalsave) {
                document.getElementById('<%=txtconfirmselectnew.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconfirmselectnew.ClientID %>').value = "No";
            }
        };

        function ConfirmDelete() {
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
            var selectedvaldelitm = confirm("Do you want to remove ?");
            if (selectedvaldelitm) {
                document.getElementById('<%=txtconfirmdelete.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconfirmdelete.ClientID %>').value = "No";
            }
        };

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


    </script>

    <style type="text/css">
        body {
            overflow-x: hidden;
        }

        .panel {
            padding-bottom: 1px;
            padding-top: 1px;
            margin-bottom: 0px;
            margin-top: 0px;
        }

        #SUBCATHIDE {
            display: none;
        }
        /*.panel-default{
            padding-bottom:0px;
            padding-top:1px;
            margin-bottom:1px;
            margin-top:0px;
        }
        .panel-heading{
            padding-bottom:0px;
            padding-top:1px;
            margin-bottom:1px;
            margin-top:0px;
        }
        .panel-body{
            padding-bottom:0px;
            padding-top:1px;
            margin-bottom:1px;
            margin-top:0px;
        }*/
    </style>

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

    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanelMain">

        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait1" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait1" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>

    </asp:UpdateProgress>

    <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
        <ContentTemplate>

            <asp:HiddenField ID="txtconfirmclear" runat="server" />
            <asp:HiddenField ID="txtsaveconfirm" runat="server" />
            <asp:HiddenField ID="txtconfirmselectnew" runat="server" />
            <asp:HiddenField ID="txtconfirmdelete" runat="server" />

            <div class="panel panel-default marginLeftRight5">

                <div class="row">

                    <div class="col-sm-10  buttonrow">
                        <div id="Information" runat="server" visible="false" class="alert alert-success alert-info" role="alert">

                            <strong>Info!</strong>
                            <asp:Label ID="lblBackDateInfor" runat="server"></asp:Label>
                            <asp:Label ID="lblH1" runat="server" Text="Label"></asp:Label>

                        </div>
                    </div>

                    <div class="col-sm-2  buttonRow">
                        <div class="col-sm-6 paddingRight0">
                            <asp:LinkButton ID="btnSave" CausesValidation="false" Visible="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmSave();" OnClick="btnSave_Click">
                            <span class="glyphicon glyphicon-edit" aria-hidden="true"></span>Update
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-2 paddingRight0">

                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="floatRight" OnClick="lbtnView_Click">
                            <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>View
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-2 paddingRight0">
                            <asp:LinkButton ID="lbtnClear" runat="server" CssClass="floatRight" OnClientClick="ConfirmClearForm();" OnClick="lbtnClear_Click">
                            <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                            </asp:LinkButton>
                        </div>

                        <div class="col-sm-1 paddingRight0">
                        </div>
                    </div>

                </div>
                <%-- pnl Old Part remove --%>
                <asp:UpdatePanel runat="server" ID="UpdatePanel15">
                    <ContentTemplate>
                        <asp:Button ID="btnpop1" runat="server" Text="Button" Style="display: none;" />
                        <asp:ModalPopupExtender ID="popProdCondition" runat="server" Enabled="True" TargetControlID="btnpop1"
                            PopupControlID="prdCondShw" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
                        </asp:ModalPopupExtender>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdateProgress ID="UpdateProgress7" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="prdCondShwDet">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Label ID="lblWait10" runat="server"
                                Text="Please wait... " />
                            <asp:Image ID="imgWait10" runat="server"
                                ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:Panel runat="server" ID="prdCondShw">
                    <asp:UpdatePanel runat="server" ID="prdCondShwDet">
                        <ContentTemplate>
                            <div runat="server" id="Div5" class="panel panel-primary" style="padding: 5px;">
                                <div class="panel panel-default" style="height: 350px; width: 600px;">
                                    <div class="panel-heading" style="height: 25px;">
                                        <div class="col-sm-10">
                                            <strong>Product Condition Details</strong>
                                        </div>
                                        <div class="col-sm-2 text-right">
                                            <asp:LinkButton ID="lbtnOldPartRemClose" runat="server" OnClick="lbtnClosePop_Click">
                                    <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="panel-body">
                                        <div style="height: 300px; overflow-y: auto; overflow-x: auto;">
                                            <asp:GridView ID="gvProdCondition" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Serial Id">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblaodindate" runat="server" Text='<%# Bind("irsc_ser_id") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitemcode" runat="server" Text='<%# Bind("ins_itm_cd") %>' Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Serial">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblothdocno" runat="server" Text='<%# Bind("ins_ser_1") %>' Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Condition">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblothloc" runat="server" Text='<%# Bind("irsc_tp") %>' Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Condition Desc">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblothloc" runat="server" Text='<%# Bind("rct_desc") %>' Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remark">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblothloc" runat="server" Text='<%# Bind("irsc_rmk") %>' Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Charge">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblothloc" runat="server" Text='<%# Bind("irsc_cha") %>' Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <SelectedRowStyle BackColor="Silver" />
                                            </asp:GridView>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>


                <div class="row">
                    <div class="col-sm-12">
                        <div class="panel panel-default">
                            <div class="panel panel-heading">
                                <strong><b>Product Condition Update</b></strong>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" style="margin-top: 17px; margin-right: 5px;">
                    <div class="col-sm-12">
                        <div class="col-sm-6 padding0">
                            <div class="panel panel-default">
                                <div class="panel panel-heading">
                                    <b>Pending Document(s) </b>
                                </div>
                                <div class="panel panel-body">
                                    <div class="row">
                                        <div class="col-sm-11 paddingRight0">
                                            <div class="row">
                                                <div class="col-sm-3 padding0">
                                                    <div class="col-sm-12 paddingRight0">
                                                        <div class="col-sm-2 labelText1 padding0">
                                                            From
                                                        </div>
                                                        <div class="col-sm-7 paddingLeft3 paddingRight0">
                                                            <asp:TextBox ID="dtpFromDate" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                                onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                            <asp:CalendarExtender ID="calexdate" runat="server" TargetControlID="dtpFromDate"
                                                                PopupButtonID="lbtnfrm" Format="dd/MMM/yyyy">
                                                            </asp:CalendarExtender>
                                                        </div>

                                                        <div class="col-sm-1 paddingLeft3">
                                                            <asp:LinkButton ID="lbtnfrm" TabIndex="1" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4 padding0">
                                                    <div class="col-sm-3 padding0 labelText1">
                                                        Type
                                                    </div>
                                                    <div class="col-sm-8 padding0">
                                                        <asp:DropDownList ID="ddlType" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" runat="server" TabIndex="4" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-5 padding0">
                                                    <div class="col-sm-4 paddingRight0 labelText1">
                                                        Doc No
                                                    </div>

                                                    <div class="col-sm-6 padding0">
                                                        <asp:TextBox ID="txtAODNumber" AutoPostBack="true" runat="server" CssClass="form-control"></asp:TextBox><%--OnTextChanged="txtAODNumber_TextChanged" --%>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft3">
                                                        <asp:LinkButton ID="lbtnsearchrec" runat="server" TabIndex="4" OnClick="lbtnsearchrec_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 padding0">
                                                    <div class="col-sm-12 paddingRight0">
                                                        <div class="col-sm-2 labelText1 padding0">
                                                            To
                                                        </div>
                                                        <div class="col-sm-7 paddingLeft3 paddingRight0">
                                                            <asp:TextBox ID="dtpToDate" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                                onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                            <asp:CalendarExtender ID="calexreq" runat="server" TargetControlID="dtpToDate"
                                                                PopupButtonID="lbtnto" Format="dd/MMM/yyyy">
                                                            </asp:CalendarExtender>
                                                        </div>

                                                        <div class="col-sm-1 paddingLeft3">
                                                            <asp:LinkButton ID="lbtnto" TabIndex="2" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-4 padding0">
                                                    <div class="col-sm-3 labelText1 padding0">
                                                        Serial No
                                                    </div>
                                                    <div class="col-sm-8 padding0">
                                                        <asp:TextBox ID="txtdirectserial" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtdirectserial_TextChanged"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3 padding0">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="col-sm-4 padding0">
                                                        <div class="col-sm-4 labelText1 padding0">
                                                            Category 1
                                                        </div>
                                                        <div class="col-sm-6 paddingRight0">
                                                            <asp:TextBox ID="txtCat1" AutoPostBack="true" OnTextChanged="txtCat1_TextChanged" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>

                                                        <div class="col-sm-1 paddingLeft3">
                                                            <asp:LinkButton ID="lbtnSeCat1" OnClick="lbtnSeCat1_Click" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4 padding0">
                                                        <div class="col-sm-4 labelText1 padding0">
                                                            Category 2
                                                        </div>
                                                        <div class="col-sm-6 padding0">
                                                            <asp:TextBox ID="txtCat2" AutoPostBack="true" OnTextChanged="txtCat2_TextChanged" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>

                                                        <div class="col-sm-1 paddingLeft3">
                                                            <asp:LinkButton ID="lbtnSeCat2" OnClick="lbtnSeCat2_Click" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4 padding0">
                                                        <div class="col-sm-4 labelText1 padding0">
                                                            Category 3
                                                        </div>
                                                        <div class="col-sm-6 paddingRight0">
                                                            <asp:TextBox ID="txtCat3" AutoPostBack="true" OnTextChanged="txtCat3_TextChanged" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>

                                                        <div class="col-sm-1 paddingLeft3">
                                                            <asp:LinkButton ID="lbtnSeCat3" OnClick="lbtnSeCat3_Click" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">

                                                <div runat="server">
                                                    <div class="col-md-3">
                                                        <asp:Label Text="Updated Documents" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:CheckBox ID="chkUpdatedType" runat="server" AutoPostBack="true" OnCheckedChanged="chkUpdatatedType_CheckedChanged" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-1 padding0">
                                            <div class="buttonRow">
                                                <asp:LinkButton ID="btnGetPO" runat="server" TabIndex="5" OnClick="btnGetPO_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true" style="font-size:20px"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="panel panel-default padding0">
                                                <div class="panel panel-body padding0">
                                                    <div style="height: 150px; overflow-y: auto; overflow-x: hidden;">
                                                        <asp:GridView ID="dvPendingPO" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found..." OnSelectedIndexChanged="dvPendingPO_SelectedIndexChanged" OnRowDataBound="dvPendingPO_RowDataBound">
                                                            <Columns>
                                                                <asp:CommandField ShowSelectButton="true" ButtonType="Link" />

                                                                <asp:TemplateField HeaderText="Doc #">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbldocno" runat="server" Text='<%# Bind("ith_doc_no") %>' Width="150px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Doc Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblaodindate" runat="server" Text='<%# Bind("ith_doc_date", "{0:dd/MMM/yyyy}") %>' Width="100px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Other Doc #">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblothdocno" runat="server" Text='<%# Bind("ith_oth_docno") %>' Width="150px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Other Loc">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblothloc" runat="server" Text='<%# Bind("ith_oth_loc") %>' Width="150px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Confirm" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkconfirm" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                            </Columns>
                                                            <SelectedRowStyle BackColor="Silver" />
                                                        </asp:GridView>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 4px;">
                                        <div class="col-sm-12">
                                            <div class="panel panel-default">
                                                <div class="panel panel-heading">
                                                    <b>Item Details</b>
                                                </div>
                                                <div class="panel panel-body padding0">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <div class="" style="height: 150px; overflow: auto;">
                                                                <div class="GridScroll">
                                                                <asp:GridView ID="gvselecteditems" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped"
                                                                    GridLines="None" OnRowDataBound="gvselecteditems_RowDataBound" OnSelectedIndexChanged="gvselecteditems_SelectedIndexChanged"
                                                                    ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                                    <Columns>
                                                                        <asp:CommandField ShowSelectButton="true" ButtonType="Link" />
                                                                        <asp:TemplateField HeaderText="Item Code">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblitmcode" runat="server" Text='<%# Bind("its_itm_cd") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Description">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbldesc" runat="server" Text='<%# Bind("mi_shortdesc") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Model">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblmodelselect" runat="server" Text='<%# Bind("mi_model") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField Visible="false" HeaderText="Item Status Value">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblstus" runat="server" Text='<%# Bind("its_itm_stus") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Status">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblstatustext" runat="server" Text='<%# Bind("mis_desc") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Serial 1">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblser1" runat="server" Text='<%# Bind("its_ser_1") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Serial 2">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblser2" runat="server" Text='<%# Bind("its_ser_2") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="New Item Status" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblstatusvalue" runat="server" Text='<%# Eval("its_itm_stus") %>' Visible="false" />
                                                                                <asp:DropDownList ID="ddlnewstus" runat="server" CssClass="form-control" AutoPostBack="true">
                                                                                </asp:DropDownList>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField Visible="false" HeaderText="Serial ID">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblserialid" runat="server" Text='<%# Bind("its_ser_id") %>' Width="1px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField Visible="false" HeaderText="Category">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblcate" runat="server" Text='<%# Bind("mi_cate_1") %>' Width="1px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField Visible="false" HeaderText="Is Serialized">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblisserialid" runat="server" Text='<%# Bind("mi_is_ser1") %>' Width="1px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField Visible="false" HeaderText="ItsPick">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblitspick" runat="server" Text='<%# Bind("its_pick") %>' Width="1px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%-- <asp:TemplateField Visible="false" HeaderText="GRN #">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblITS_ORIG_GRNNO" runat="server" Text='<%# Bind("ITS_ORIG_GRNNO") %>' Width="1px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>--%>
                                                                        <asp:TemplateField HeaderText="Remark">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblremark" runat="server" Text='<%# Bind("remrk") %>' Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                    </Columns>
                                                                    <SelectedRowStyle BackColor="Silver" />

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
                        <div class="col-sm-6 padding0">
                            <div class="panel panel-default">
                                <div class="panel panel-heading">
                                    <%--<strong><b>Product Condition Update</b></strong>--%>
                                </div>
                                <div class="panel panel-body">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-sm-4 padding0">
                                                <div class="col-sm-4 paddingRight0 labelText1">
                                                    Serial #
                                                </div>

                                                <div class="col-sm-8 padding0">
                                                    <asp:TextBox ID="txtserial" ReadOnly="true" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtserial_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 padding0">
                                                <div class="col-sm-4 paddingRight0 labelText1">
                                                    Item Code
                                                </div>

                                                <div class="col-sm-8 padding0">
                                                    <asp:TextBox ID="txtItmCode" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:Label Text="" Visible="false" ID="lblSerId" runat="server" />
                                                </div>
                                            </div>
                                            <asp:Label ID="lblCat1" Visible="false" runat="server" />
                                            <asp:Label ID="lblCat2" Visible="false" runat="server" />
                                            <asp:Label ID="lblCat3" Visible="false" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-sm-4 padding0">
                                                <div class="col-sm-4 paddingRight0 labelText1">
                                                    GRN #
                                                </div>

                                                <div class="col-sm-8 padding0">
                                                    <asp:TextBox ID="txtGrnNo" ReadOnly="true" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtserial_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 padding0">
                                                <div class="col-sm-4 paddingRight0 labelText1">
                                                    GRN Date
                                                </div>

                                                <div class="col-sm-8 padding0">
                                                    <asp:TextBox ID="txtGrnDate" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 padding0">
                                                <div class="col-sm-4 paddingRight0 labelText1">
                                                    GRAN#
                                                </div>

                                                <div class="col-sm-8 padding0">
                                                    <asp:TextBox ID="txtgranno" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="panel panel-default padding0">
                                                <div class="panel panel-heading">
                                                    <b>Initial Feedback</b>
                                                </div>
                                                <div class="panel panel-body padding0">
                                                    <asp:Panel runat="server" ID="pnlInitialFeedBack">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-3 labelText1">
                                                                    Condition
                                                                </div>
                                                                <div class="col-sm-2 labelText1">
                                                                    <asp:TextBox runat="server" AutoPostBack="true" ID="txtCondition" OnTextChanged="txtCondition_TextChanged" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft3 labelText1">
                                                                    <asp:LinkButton ID="lbtnSeCondition" OnClick="lbtnSeCondition_Click" CausesValidation="false" runat="server">
                                                                                   <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-2 labelText1 padding0">
                                                                    <asp:Label ID="lblMainChg" Text="Charge" runat="server" />
                                                                </div>
                                                                <div class="col-sm-2 labelText1">
                                                                    <asp:TextBox runat="server" ID="txtMainConPrice" CssClass="txtMainConPrice form-control" />
                                                                    <asp:Label Text="" ID="lblCharge" runat="server" Visible="false" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-3 labelText1">
                                                                    Description
                                                                </div>
                                                                <div class="col-sm-3 labelText1">
                                                                    <asp:TextBox runat="server" ID="TextDesc" ReadOnly="true" onkeypress="return isNumberKey(event)" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-3 labelText1">
                                                                    Remarks
                                                                </div>
                                                                <div class="col-sm-6 labelText1">
                                                                    <asp:TextBox runat="server" ID="txtConRemarks" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <asp:Panel runat="server" ID="pnlCondition" Enabled="false">
                                            <div class="col-sm-12">
                                                <div class="panel panel-default padding0">
                                                    <div class="panel panel-heading" style="height: 24px;">
                                                        <div class="row">
                                                            <div class="col-sm-6 ">
                                                                <b>Detailed Feedback</b>
                                                            </div>
                                                            <div class="col-sm-6 ">
                                                                <div class="col-sm-6 labelText1 padding0">
                                                                    Is Conventional 
                                                                </div>
                                                                <div class="col-sm-1  labelText1 padding0">
                                                                    <asp:CheckBox ID="isConventionalCheckBox" runat="server" AutoPostBack="true" OnCheckedChanged="ConventionalCheckBox_CheckedChanged" />
                                                                    <%--<asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" OnCheckedChanged="chkUpdatatedType_CheckedChanged" />--%>
                                                                </div>
                                                            </div>
                                                        </div>                                                        
                                                    </div>
                                                    <div class="panel panel-body padding0">
                                                         <div class="row">
                                                             <div class="col-sm-12">
                                                            <div class="col-sm-6 padding0 ">
                                                                <div class="col-sm-6 ">
                                                                    New Stock Status
                                                                </div>
                                                                <div class="col-sm-5 padding0 labelText1">
                                                                    <asp:TextBox AutoPostBack="true" Style="text-transform: uppercase" OnTextChanged="txtNewStockSts_TextChanged" runat="server" ID="txtNewStockSts" CssClass="form-control" />
                                                                </div>
                                                                <div class="col-sm-1 padding0 labelText1">
                                                                    <asp:LinkButton ID="LinkButton2" CausesValidation="false" OnClick="lbtnSeStatus_Click" runat="server">
                                                                                   <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                              <div class="col-sm-6 padding0">
                                                                    <div class="col-sm-3 padding0 labelText1">
                                                                        Condition
                                                                    </div>
                                                                    <div class="col-sm-8 padding0 labelText1">
                                                                        <asp:TextBox runat="server" AutoPostBack="true" OnTextChanged="txtSubCon_TextChanged" ID="txtSubCon" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-8 padding0 labelText1" id="SUBCATHIDE">
                                                                        <asp:TextBox runat="server" AutoPostBack="true" ID="txtSubConCat" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft3 labelText1">
                                                                        <asp:LinkButton ID="lbtnSeSubCon" OnClick="lbtnSeSubCon_Click" CausesValidation="false" runat="server">
                                                                                   <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                                 </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">                                                               
                                                                <div class="col-sm-3 padding0">
                                                                    <div class="col-sm-3">
                                                                        Charge
                                                                    </div>
                                                                    <div class="col-sm-9  labelText1">
                                                                        <asp:TextBox runat="server" ReadOnly="true" ID="txtSubCharg" CssClass="txtSubCharg form-control text-right"></asp:TextBox>
                                                                    </div>
                                                                </div>  
                                                                 <div class="col-sm-8">
                                                                <div class="col-sm-2 labelText1 padding0">
                                                                    Description
                                                                </div>
                                                                <div class="col-sm-6  labelText1 padding0">
                                                                    <asp:TextBox runat="server" ReadOnly="true" ID="txtDescription" CssClass="txtSubCharg form-control"></asp:TextBox>
                                                                </div>                                                               
                                                              <%--  <div class="col-sm-2 labelText1 padding0">
                                                                    Status
                                                                </div>
                                                                <div class="col-sm-6  labelText1 padding0">
                                                                    <asp:TextBox runat="server" ReadOnly="true" ID="TextBox1" CssClass="txtSubCharg form-control"></asp:TextBox>
                                                                </div>--%>
                                                            </div>
                                                                <div class="col-sm-1 paddingLeft3">
                                                                        <div class="col-sm-1  buttonRow">
                                                                            <asp:LinkButton ID="lbtnAddCon" OnClick="lbtnAddCon_Click" CausesValidation="false" runat="server">
                                                                                   <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"  ></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                </div>                                                              
                                                            </div>
                                                        </div>                                                    
                                                       
                                                         <div class="row">
                                                            <div class="col-sm-5 ">
                                                                    <div class="col-sm-2   labelText1">
                                                                        Remark
                                                                    </div>
                                                                    <div class="col-sm-8  labelText1">
                                                                        <asp:TextBox runat="server" AutoPostBack="false" ID="txtSubRemark" CssClass="form-control"></asp:TextBox>
                                                                    </div>                                                                    
                                                                </div>
                                                        </div>                                                     
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <%-- <asp:UpdatePanel runat="server">
                                                <ContentTemplate>--%>
                                            <div class="panel panel-default">
                                                <div class="panel panel-body">
                                                    <div class="" style="height: 150px; overflow: auto;">
                                                        <asp:GridView ID="dgvCond" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped"
                                                            GridLines="None" ShowHeaderWhenEmpty="True"
                                                            EmptyDataText="No data found...">
                                                            <Columns>
                                                                <asp:TemplateField Visible="true" HeaderText="">
                                                                    <ItemTemplate>
                                                                        <div style="margin-top: -3px;">
                                                                            <asp:LinkButton Width="10px" OnClientClick="return ConfDelete();" ID="lbtnDelete" OnClick="lbtnDelete_Click" CausesValidation="false" runat="server">
                                                                                   <span class="glyphicon glyphicon-trash" aria-hidden="true"  ></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="true" HeaderText="Condition">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblirsc_tp" runat="server" Text='<%# Bind("irsc_tp") %>' Width="60px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="true" HeaderText="Description">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbltmpCondescription" runat="server" Text='<%# Bind("tmpCondescription") %>' Width="100px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="true" HeaderText="Item">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("ItemCode") %>' Width="80px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="true" HeaderText="Status">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStatusText" runat="server" Text='<%# Bind("StatusText") %>' Width="40px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="true" HeaderText="Serial">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItemSearial" runat="server" Text='<%# Bind("ItemSearial") %>' Width="80px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="true" HeaderText="">
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblChgHeder" Text="Charge" runat="server" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblConCharge" runat="server" Width="50px" Text='<%# Bind("irsc_cha","{0:N2}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                    <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="true" HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblConCharge1" runat="server" Text='' Width="5px"></asp:Label>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="true" HeaderText="Remark">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblirsc_rmk" runat="server" ToolTip='<%# Bind("irsc_rmk") %>' Text='<%# Bind("tmpRemark") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                            <%-- </ContentTemplate>
                                            </asp:UpdatePanel>--%>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-sm-3">
                                                <asp:Label Text="Total Charge Amount" runat="server" ID="lbltotChgAmount" />
                                            </div>
                                            <div class="col-sm-2 paddingLeft0">
                                                <asp:Label Text="" ID="lblTotal" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <%--<div class="row">
                                        <div class="col-sm-12 ">
                                            <div class="col-sm-3 labelText1 labelText1">
                                                New Stock Status
                                            </div>
                                            <div class="col-sm-2 padding0 labelText1">
                                                <asp:TextBox AutoPostBack="true" Style="text-transform: uppercase" OnTextChanged="txtNewStockSts_TextChanged" runat="server" ID="txtNewStockSt" CssClass="form-control" />
                                            </div>
                                            <div class="col-sm-1 paddingLeft3 labelText1">
                                                <asp:LinkButton ID="lbtnSeStatus" CausesValidation="false" OnClick="lbtnSeStatus_Click" runat="server">
                                                                                   <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                </asp:LinkButton>
                                            </div>

                                        </div>
                                    </div>--%>
                                    <div class="row">
                                        <div class="col-sm-8 ">
                                        </div>
                                        <div class="col-sm-3 ">
                                            <div class="buttonRow">
                                                <asp:LinkButton ID="lbtnConfirm" CausesValidation="false" OnClientClick="ConfirmSave();" OnClick="btnSave_Click" runat="server">
                                                                                   <span class="glyphicon glyphicon-saved" aria-hidden="true"  ></span>Confirm
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>

                                    <asp:Panel runat="server" Visible="false">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="panel panel-default padding0">
                                                    <div class="panel panel-body padding0">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="" style="height: 100px; overflow: auto;">
                                                                    <asp:GridView ID="gvaddconditions" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">

                                                                        <Columns>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="col_p_Get" runat="server" Width="30px" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Type">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbladdtype" runat="server" Text='<%# Bind("rct_tp") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Condition">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbladdconditions" runat="server" Text='<%# Bind("rct_desc") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Remark">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox runat="server" ID="txtremarks" CssClass="form-control" Text='<%# Bind("irsc_rmk") %>'></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                        </Columns>

                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-6">
                                                                <asp:LinkButton ID="lbtndconfirm" CausesValidation="false" runat="server" CssClass="floatleft" OnClick="lbtndconfirm_Click">
                                                        <span class="glyphicon glyphicon-plus fontsize20" aria-hidden="true"></span>Add
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <asp:LinkButton ID="lbtndreset" CausesValidation="false" CssClass="floatleft" runat="server" OnClick="lbtndreset_Click">
                                                        <span class="glyphicon glyphicon-remove fontsize20" aria-hidden="true"></span>Cancel
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <br />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="panel panel-default padding0">
                                                    <div class="panel panel-body padding0">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="" style="height: 100px; overflow: auto;">
                                                                    <asp:GridView ID="gvconditions" AutoGenerateColumns="false" OnRowUpdating="gvconditions_RowUpdating" runat="server" CssClass="table table-hover table-striped" OnRowDataBound="gvconditions_RowDataBound" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found..." OnRowDeleting="gvconditions_RowDeleting">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lbtngrdInvoiceDetailsDalete" runat="server" CausesValidation="false" OnClientClick="ConfirmDelete();" CommandName="Delete">
                                                                                                <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                                                                    </asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField ShowHeader="false">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lbtngrdInvoiceDetailsEdit" CausesValidation="false" runat="server" OnClick="lbtngrdInvoiceDetailsEdit_Click">
                                                                                <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>
                                                                                    </asp:LinkButton>
                                                                                </ItemTemplate>

                                                                                <EditItemTemplate>
                                                                                    <asp:LinkButton ID="lbtngrdInvoiceDetailsUpdate" runat="server" CausesValidation="false" CommandName="Update" OnClick="lbtngrdInvoiceDetailsUpdate_Click">
                                                                                <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                                                    </asp:LinkButton>
                                                                                </EditItemTemplate>

                                                                                <ItemStyle HorizontalAlign="Right" Width="1%" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField Visible="false" HeaderText="Serial">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblitmserialid" runat="server" Text='<%# Bind("irsc_ser_id") %>' Width="1px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField Visible="false" HeaderText="ct003cate">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblct003" runat="server" Text='<%# Bind("irsc_cat") %>' Width="1px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Item Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblitmcodemyref" runat="server" Text='<%# Bind("its_itm_cd") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Type">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbltype" runat="server" Text='<%# Bind("irsc_tp") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Description">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbldesc" runat="server" Text='<%# Bind("rct_desc") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Remarks">
                                                                                <EditItemTemplate>
                                                                                    <asp:TextBox ID="txtremarkscondition" runat="server" Text='<%# Bind("irsc_rmk") %>'></asp:TextBox>
                                                                                </EditItemTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblremarks" runat="server" Text='<%# Bind("irsc_rmk") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>


                                                                            <asp:TemplateField HeaderText="Added By">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbladdedby" runat="server" Text='<%# Bind("irsc_cre_by") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Status">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblstatusvalueitmconditions" runat="server" Text='<%# Eval("irsc_stus") %>' Visible="false" />
                                                                                    <asp:DropDownList ID="ddlconditstus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlconditstus_SelectedIndexChanged">
                                                                                        <asp:ListItem Text="Active" Value="A">
                                                                                        </asp:ListItem>
                                                                                        <asp:ListItem Text="Inactive" Value="C">
                                                                                        </asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField Visible="false" HeaderText="Cat">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblcategory" runat="server" Text='<%# Bind("rct_cat1") %>' Width="1px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField Visible="false" HeaderText="cancelledby">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblcancelledby" runat="server" Text='<%# Bind("irsc_cnl_by") %>' Width="1px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField Visible="false" HeaderText="cancel date">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblcancelleddate" runat="server" Text='<%# Bind("irsc_cnl_dt") %>'></asp:Label>
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
                                    </asp:Panel>
                                </div>
                                <asp:Panel runat="server" Visible="false">
                                    <div class="panel-heading">

                                        <div class="col-sm-4">
                                            <div class="row">
                                                Item Details
                                            </div>
                                        </div>

                                        <div class="col-sm-4">
                                            <div class="row">

                                                <div class="col-sm-4 labelText1">
                                                    Serialized Item Only
                                                </div>

                                                <div class="col-sm-8 labelText1" style="margin-top: -3px">
                                                    <asp:CheckBox ID="chkser" TabIndex="6" runat="server" />
                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-sm-4">
                                            <div class="row">
                                            </div>
                                        </div>

                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>


                <asp:Panel runat="server" Visible="false">
                    <div class="row">

                        <div class="panel-body">

                            <div class="col-sm-12 ">

                                <div class="panel panel-default">

                                    <div class="panel-heading">
                                        Pending Document(s)
                                    </div>

                                    <div class="panel-body">

                                        <div class="col-sm-2">
                                            <div class="row">

                                                <div class="col-sm-3 labelText1">
                                                    From
                                                </div>

                                                <div>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-sm-2">
                                            <div class="row">

                                                <div class="col-sm-3 labelText1">
                                                    To
                                                </div>

                                                <div>
                                                    <div class="col-sm-8">
                                                    </div>

                                                    <div class="col-sm-1 paddingLeft0" style="margin-left: -10px; margin-top: -2px">
                                                    </div>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-sm-3">
                                            <div class="row">
                                            </div>
                                        </div>

                                        <div class="col-sm-3" style="margin-left: -75px">
                                            <div class="row">
                                            </div>
                                        </div>

                                        <div class="col-sm-2">
                                            <div class="row">

                                                <div class="col-sm-4 labelText1">
                                                    <strong></strong>
                                                </div>


                                            </div>
                                        </div>

                                        <div class="col-sm-2 crnsearchbtn">
                                            <div class="row">
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
                                                            <div class="col-sm-12">
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
                </asp:Panel>

                <asp:Panel runat="server" Visible="false">
                    <div class="row">

                        <div class="panel-body">

                            <div class="col-sm-12">

                                <div class="panel panel-default">

                                    <div class="panel-heading">
                                        Item Conditions
                                    </div>

                                    <div class="panel-body">

                                        <div class="row">
                                            <div class="panel-body">
                                                <div class="col-sm-12">
                                                    <div class="panel panel-default">

                                                        <div class="panel-heading panelHeadingInfoBar">

                                                            <div class="col-sm-3">

                                                                <div class="row">

                                                                    <div class="col-sm-5 labelText1">
                                                                        Item Code-
                                                                    </div>

                                                                    <div class="col-sm-8 prnlbldescription">
                                                                    </div>

                                                                </div>

                                                            </div>

                                                            <div class="col-sm-2">

                                                                <div class="row">

                                                                    <div class="col-sm-5 labelText1">
                                                                        Item Status-
                                                                    </div>
                                                                    <div class="col-sm-7" style="margin-top: 3px">
                                                                        <asp:Label ID="lblstus" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                    </div>

                                                                </div>

                                                            </div>

                                                            <div class="col-sm-4">

                                                                <div class="row">

                                                                    <div class="col-sm-4 labelText1">
                                                                        Serial-
                                                                    </div>
                                                                    <div class="col-sm-8" style="margin-top: 3px">
                                                                        <asp:Label ID="lblser" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                    </div>

                                                                </div>

                                                            </div>

                                                            <div class="col-sm-2">

                                                                <div class="row">

                                                                    <div class="row">

                                                                        <div class="col-sm-5 labelText1">
                                                                            Model-
                                                                        </div>
                                                                        <div class="col-sm-7" style="margin-top: 3px">
                                                                            <asp:Label ID="lblmodel" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                        </div>

                                                                    </div>

                                                                </div>

                                                            </div>

                                                            <div class="col-sm-1">
                                                                <asp:LinkButton ID="lbtnaddinvitems" CausesValidation="false" TabIndex="8" CssClass="floatRight" runat="server" OnClick="lbtnaddinvitems_Click">
                                                        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>

                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">

                                            <div class="col-sm-12">

                                                <div class="panel panel-default">

                                                    <div class="panel-body" id="panelbodydiv1">

                                                        <div class="row">
                                                            <div class="col-sm-12">

                                                                <div class="panelscollheight50">
                                                                </div>


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

                                </div>

                            </div>

                        </div>

                    </div>
                </asp:Panel>




            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="SIPopup" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="pnlpopup" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpopup" DefaultButton="lbtnSearch">
        <div runat="server" id="test" class="panel panel-primary Mheight">

            <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="btnClose" runat="server">
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

                                    <div id="resultgrd" class="panelscoll POPupResultspanelscroll">
                                        <asp:GridView ID="grdResult" CausesValidation="false" AllowPaging="true" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" OnPageIndexChanging="grdResult_PageIndexChanging" OnSelectedIndexChanged="grdResult_SelectedIndexChanged">
                                            <Columns>
                                                <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10" />
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
    </asp:Panel>


    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btn2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="MpDelivery" runat="server" Enabled="True" TargetControlID="btn2"
                PopupControlID="pnldel" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeaderKit" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnldel" DefaultButton="lbtnSearch">
        <div runat="server" id="Div1" class="panel panel-primary height350 width525">

            <asp:Label ID="Label7" runat="server" Text="Label" Visible="false"></asp:Label>

            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="LinkButton13" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <div class="col-sm-11">
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <div class="panel-body">

                    <asp:UpdatePanel ID="blkcancelpnl" runat="server">
                        <ContentTemplate>

                            <div class="col-sm-12">

                                <div class="row">
                                </div>

                            </div>





                            <div class="row">
                                <div class="col-sm-12">

                                    <div class="col-sm-12">
                                    </div>
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>

        </div>
    </asp:Panel>

    <asp:UpdatePanel runat="server" ID="update">
        <ContentTemplate>
            <asp:Button ID="btn1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupSearch" runat="server" Enabled="True" TargetControlID="btn1"
                PopupControlID="pnlSearch" CancelControlID="lbtnClosePop" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel runat="server" ID="pnlSearch" DefaultButton="lbtnSearchNew">
        <div runat="server" id="Div2" class="panel panel-primary Mheight" style="width: 700px;">
            <div class="panel panel-default" style="height: 370px;">
                <div class="panel-heading" style="height: 25px;">
                    <div class="col-sm-11"></div>
                    <div class="col-sm-1">
                        <asp:LinkButton ID="lbtnClosePop" runat="server" OnClick="lbtnClosePop_Click">
                             <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="col-sm-2 labelText1">
                                Search by key
                            </div>
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-3 paddingRight5">
                                        <asp:DropDownList ID="cmbSearchbykey" runat="server" class="form-control" AutoPostBack="false">
                                        </asp:DropDownList>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="col-sm-2 labelText1">
                                Search by word
                            </div>
                            <div class="col-sm-3 paddingRight5">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtSearchWord" CausesValidation="false" class="form-control" AutoPostBack="false" runat="server"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-1 paddingLeft0">
                                        <asp:LinkButton ID="lbtnSearchNew" runat="server" OnClick="lbtnSearchNew_Click">
                                     <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>
                                </ContentTemplate>


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
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy8" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="dgvResult" CausesValidation="false" runat="server" AllowPaging="True"
                                        GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" ShowHeaderWhenEmpty="true"
                                        EmptyDataText="No data found..."
                                        OnPageIndexChanging="dgvResult_PageIndexChanging" OnSelectedIndexChanged="dgvResult_SelectedIndexChanged">
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
    </asp:Panel>
    <script>
        Sys.Application.add_load(func);
        function func() {
            /*Validate decimal value*/
            $('.txtSubCharg,.txtMainConPrice').keypress(function (evt) {
                // var ch = (evt.which) ? evt.which : evt.keyCode;
                var ch = evt.which;
                var str = $(this).val();
                console.log(ch);
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
            })

            $('.txtSubCharg,.txtMainConPrice').mousedown(function (e) {
                if (e.button == 2) {
                    alert('This functionality is disabled !');
                    return false;
                } else {
                    return true;
                }
            });
        }
    </script>
</asp:Content>
