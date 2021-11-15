<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="ItemTaxStructure.aspx.cs" Inherits="FastForward.SCMWeb.View.MasterFiles.Inventory.Item_Tax_Structure" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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
            //  $(".toastmessagey").attr('style', 'background-color: #1111; opacity:1; z-index:1000;');
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
        function CancelConfirm() {
            var selectedvalue = confirm("Do you want to cancel MRN?");
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
        };
        function checkDate(sender, args) {

            if ((sender._selectedDate < new Date())) {
                $().toastmessage('showToast', {

                    text: 'You cannot select a day earlier than today!',

                    sticky: false,
                    position: 'top-center',
                    type: 'warning',
                    closeText: '',
                    close: function () {
                        console.log("toast is closed ...");
                    }

                });
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
        function CheckAllgrdTax(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdTax.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:HiddenField ID="txtClearlconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtSavelconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtUpdateconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtApprovalconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtCancelconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtDeleteconformmessageValue" runat="server" />
    <asp:HiddenField ID="TabName" runat="server" />
    <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
        <ContentTemplate>
            <div class="panel panel-default marginLeftRight5">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12 height5">
                        </div>
                    </div>
                    <%--<div class="row">--%>
                    <div class="col-sm-8">
                    </div>
                    <div class="col-sm-4 buttonRow">
                        <div class="col-sm-3 paddingLeft0 paddingRight0">
                        </div>
                        <div class="col-sm-2 paddingLeft0 paddingRight0">
                            <asp:LinkButton ID="lbtnSave" runat="server" CausesValidation="false" OnClick="lbtnSave_Click" OnClientClick="SaveConfirm()">
                                                                    <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                            </asp:LinkButton>
                        </div>

                        <div class="col-sm-2 paddingLeft0 paddingRight0">
                            <asp:LinkButton ID="lbtnClear2" runat="server" CausesValidation="false" OnClick="lbtnClear2_Click" OnClientClick="ClearConfirm()">
                                                                    <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-2 paddingLeft0 paddingRight0">
                            <asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="false" OnClick="lbtnDelete_Click" OnClientClick="DeleteConfirm()">
                                                                    <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>Delete
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-3  paddingRight0 paddingLeft0">
                            <asp:LinkButton Visible="false" ID="lbtnExcelUpload" runat="server" OnClick="lbtnExcelUpload_Click">
                                        <span class="glyphicon glyphicon-upload" aria-hidden="true"></span>Excel Upload
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
                 <div class="row">
                    <div class="col-sm-12">
                        <div class="panel panel-default">
                            <div class="panel-heading "><b>Tax Structure</b></div>
                            <div class="panel-body">
                <div class="row">
                    <div class="col-sm-3  ">

                        <div class="row">
                            <div class="col-sm-4 labelText1">
                                Structure Code
                            </div>
                            <div class="col-sm-7">
                                <asp:TextBox runat="server" ID="txtStruc" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtStruc_TextChanged"></asp:TextBox>
                            </div>
                            <div class="col-sm-1 paddingLeft0 paddingRight0">
                                <asp:LinkButton ID="lbtnsrhStuc" runat="server" CausesValidation="false" OnClick="lbtnsrhStuc_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-5 ">

                        <div class="row">
                            <div class="col-sm-3 labelText1">
                                Description
                            </div>
                            <div class="col-sm-9">
                                <asp:TextBox runat="server" ID="txtDes" TextMode="MultiLine" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                            </div>

                        </div>

                    </div>

                </div>
                <div class="row">
                    <div class="col-sm-12 height5">
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="panel panel-default">
                            <div class="panel-heading ">Tax</div>
                            <div class="panel-body">
                                <div class="col-sm-4">
                                    <div class="row">
                                        <div class="col-sm-3 labelText1">
                                            Company
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" Enabled="false" ID="txtCompany" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtCompany_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                            <asp:LinkButton ID="lbtnSearchreCom" runat="server" CausesValidation="false" OnClick="lbtnSearchreCom_Click">
                                                                                 <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                         <div class="col-sm-3 paddingLeft0 paddingRight0">
                                            <asp:LinkButton ID="lbtnMultiplecompany" runat="server" Text="Multiple company" OnClick="lbtnMultiplecompany_Click">
                                                            
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3 labelText1">
                                            Item Status
                                        </div>
                                        <div class="col-sm-5">
                                            <asp:DropDownList ID="ddlwStatus" runat="server" class="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2 paddingLeft0 paddingRight0">
                                            <asp:LinkButton ID="lbtnMultiplestatus" runat="server" Text="Multiple Status" OnClick="lbtnMultiplestatus_Click">
                                                            
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3 labelText1">
                                            Tax Type
                                        </div>
                                        <div class="col-sm-5">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="ddlTaxCodeSts_SelectedIndexChanged"  AppendDataBoundItems="true"  ID="ddlTaxCodeSts" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="0" Text="--Select--" />
                                                    </asp:DropDownList>
                                                 <%--   <asp:DropDownList AppendDataBoundItems="true" ID="ddlTaxCode" runat="server" AutoPostBack="true" 
                                                CssClass="form-control" OnSelectedIndexChanged="ddlTaxCode_SelectedIndexChanged">
                                                <asp:ListItem Value="0" Text="--Select--" />
                                            </asp:DropDownList>--%>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3 labelText1">
                                            Tax Rate
                                        </div>
                                        <div class="col-sm-5">
                                            <asp:TextBox runat="server"  ID="txtTax" Enabled="false" CausesValidation="false" CssClass="form-control text-right"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3 labelText1">
                                            From Date
                                        </div>
                                        <div class="col-sm-5 ">
                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control"
                                                Format="dd/MMM/yyyy" Enabled="false">
                                            </asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 paddingLeft0 paddingRight0">
                                            <asp:LinkButton ID="lbtnFromDate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender4" OnClientDateSelectionChanged="checkDate" runat="server" TargetControlID="txtFromDate"
                                                PopupButtonID="lbtnFromDate" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3 labelText1">
                                            To Date
                                        </div>
                                        <div class="col-sm-5 ">
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control"
                                                Format="dd/MMM/yyyy" Enabled="false">
                                            </asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 paddingLeft0 paddingRight0">
                                            <asp:LinkButton ID="lbtnToDate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender5" OnClientDateSelectionChanged="checkDate" runat="server" TargetControlID="txtToDate"
                                                PopupButtonID="lbtnToDate" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                            <asp:LinkButton ID="lbtnAddstatus" runat="server" OnClientDateSelectionChanged="checkDate" CausesValidation="false" OnClick="lbtnAddstatus_Click">
                                                 <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3 labelText1">
                                            Active
                                        </div>
                                        <div class="col-sm-5">
                                            <asp:CheckBox ID="check_active" runat="server" Width="5px"></asp:CheckBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-8">
                                    <div class="row">
                                        <div class="col-sm-12 ">
                                            <div class="panel-body  panelscollbar height400 paddingtopbottom0">
                                                <asp:GridView ID="grdTax" ShowHeader="true" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="" Visible="true">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="allchk_reqitem" runat="server" Width="5px" onclick="CheckAllgrdTax(this)"></asp:CheckBox>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chk_ReqItem" AutoPostBack="true" runat="server" Checked="false" Width="5px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Company">
                                                            <ItemTemplate>
                                                                <asp:Label ID="colmCode" runat="server" Text='<%# Bind("ITS_COM") %>' Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="colmDes" runat="server" Visible="false" Text='<%# Bind("ITS_STUS") %>' Width="80px"></asp:Label>
                                                                  <asp:Label ID="Label5" runat="server" Text='<%# Bind("Its_stus_Des") %>' Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tax Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="colTaxtype" runat="server" Text='<%# Bind("ITS_TAX_CD") %>' Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tax Rate (%)">
                                                            <ItemTemplate>
                                                                <asp:Label ID="coltaxrate" runat="server" Text='<%# Bind("ITS_TAX_RATE","{0:N}") %>' Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Active">
                                                            <ItemTemplate>
                                                                <asp:CheckBox AutoPostBack="true" ID="colmStataus" runat="server" Checked='<%#Convert.ToBoolean(Eval("ITS_ACT")) %>' Width="5px" OnCheckedChanged="colmStataus_CheckedChanged" />
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
                </div>
                                </div>
                            </div>
                        </div>
                     
            </div>
            </div>
            
        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:ScriptManagerProxy ID="ScriptManagerProxy6" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="Multiplestatus" runat="server" Enabled="True" TargetControlID="Button2"
                PopupControlID="pnlMultiple" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlMultiple" DefaultButton="lbtnSearch">
                <div runat="server" id="Div3" class="panel panel-default height300 width1085 panelscollbar">
                    <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
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
                            <div class="col-sm-12" id="Div6" runat="server">
                                <div class="row">
                                    <div class="col-sm-12">
                                    </div>
                                </div>
                                <div class="row">
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="panel-body ">

                                            <asp:CheckBoxList runat="server" ID="chklstbox"
                                                RepeatColumns="5"
                                                RepeatDirection="Vertical"
                                                RepeatLayout="Table" Width="1000"
                                                TextAlign="Right"
                                                ForeColor="#333"
                                                Font-Bold="false">
                                            </asp:CheckBoxList>


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


     <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button4" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="Multiplecompany" runat="server" Enabled="True" TargetControlID="Button4"
                PopupControlID="pnlMultiplecomp" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlMultiplecomp" DefaultButton="lbtnSearch">
                <div runat="server" id="Div5" class="panel panel-default height300 width700 panelscollbar">
                    <asp:Label ID="Label4" runat="server" Text="Label" Visible="false"></asp:Label>
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
                            <div class="col-sm-12" id="Div7" runat="server">
                                <div class="row">
                                    <div class="col-sm-12">
                                    </div>
                                </div>
                                <div class="row">
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="panel-body ">

                                            <asp:CheckBoxList runat="server" ID="chklstboxcom"
                                                RepeatColumns="5"
                                                RepeatDirection="Vertical"
                                                RepeatLayout="Table" Width="600"
                                                TextAlign="Right"
                                                ForeColor="#333"
                                                Font-Bold="false">
                                            </asp:CheckBoxList>


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



    <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
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
    <asp:UpdatePanel ID="UpdatePanel4" runat="server">

        <ContentTemplate>

            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="excelUpload" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="pnlexcel" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>


        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlexcel">
        <div runat="server" id="Div1" class="panel panel-default height45 width700 ">


            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="LinkButton1" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <%--<span>Commen Search</span>--%>
                    <div class="col-sm-11">
                        <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="Label" Visible="false"></asp:Label>
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

                            <div class="col-sm-12" id="Div2" runat="server">
                                <div class="col-sm-5 paddingRight5">
                                    <asp:FileUpload ID="fileupexcelupload" runat="server" />

                                </div>

                                <div class="col-sm-2 paddingRight5">
                                    <asp:Button ID="btnUpload" class="btn btn-warning btn-xs" runat="server" Text="Upload"
                                        OnClick="btnUpload_Click" />
                                </div>


                            </div>


                            <%--<div class="row">--%>
                            <div class="col-sm-12 ">
                                <div id="divUpcompleted" visible="false" runat="server" class="alert alert-info alert-success" role="alert">
                                    <div class="col-sm-9">
                                        <strong>Alert!</strong>
                                        <asp:Label ID="Label2" Text="Excel file upload completed. Do you want to process?" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="btnprocess" class="btn btn-success btn-xs" runat="server" Text="Process" OnClick="btnprocess_Click" />
                                    </div>
                                </div>
                            </div>


                        </div>
                    </div>
                </div>
            </div>
        </div>

    </asp:Panel>


</asp:Content>
