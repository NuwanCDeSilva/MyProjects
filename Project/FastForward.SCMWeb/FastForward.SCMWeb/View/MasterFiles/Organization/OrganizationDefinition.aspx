<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="OrganizationDefinition.aspx.cs" Inherits="FastForward.SCMWeb.View.MasterFiles.Organization.OrganizationDefinition" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.40412.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
       <script type="text/javascript">
           function ClearConfirm() {
               var selectedvalue = confirm("Do you want to clear data?");
               if (selectedvalue) {
                   document.getElementById('<%=hdfClearData.ClientID %>').value = "1";
            } else {
                document.getElementById('<%=hdfClearData.ClientID %>').value = "0";
            }
        };

        function confBankSave() {
            var selectedvalueOrd = confirm("Do you want to save ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        }

        function confBranchSave() {
            var selectedvalueOrd = confirm("Do you want to save ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        }

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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
       
        <div class="col-md-12">
            <div class="col-md-12">
                <div class="row">
                    <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField ID="hdfDilogResult" runat="server" Value="0" />
                            <asp:HiddenField ID="hdfSelectedButton" runat="server" />
                            <asp:HiddenField ID="hdfBankType" Value="0" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="col-md-12">
                        <div class="panel panel-default ">
                            <div class="panel-body" style="height: 558px;">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="panel panel-default ">
                                            <div class="panel-body" style="height: 200px;">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 ">
                                        <div>
                                            <div style="height: 250px;">
                                                <div id="tabArea">
                                                    <ul id="myTab" class="nav nav-tabs">
                                                        <li class="active">
                                                            <a href="#bankTab" data-toggle="tab">Bank/Branch</a>
                                                        </li>

                                                        <li></li>
                                                    </ul>
                                                    <div class="tab-content">
                                                        <div class="tab-pane pade in active" id="bankTab">
                                                            <asp:HiddenField ID="hdfClearData" runat="server" />
                                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                <ContentTemplate>
                                                                    <div class="row">
                                                                        <div class="col-md-6" style="padding-right: 1px;">
                                                                            <div class="panel panel-default " style="padding-left: 1px; padding-right: 1px;">
                                                                                <div class="panel-heading">
                                                                                    <strong>Bank Details</strong>
                                                                                </div>
                                                                                <div class="panel-body" style="padding-left: 0; padding-right: 0; height: 150px;">
                                                                                    <div class="row">
                                                                                        <div class="col-md-12 ">
                                                                                            <div class="col-md-2 labelText1">
                                                                                                <asp:Label runat="server">Country Code</asp:Label>
                                                                                            </div>
                                                                                            <div class="col-md-3">
                                                                                                <asp:TextBox runat="server" Enabled="false" ID="txtCountryCode" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtCountryCode_TextChanged"></asp:TextBox>
                                                                                            </div>
                                                                                            <div class="col-md-1 paddingLeft0">
                                                                                                <asp:LinkButton ID="lbtnSeCountryCode" CausesValidation="false" runat="server" OnClick="lbtnSeCountryCode_Click" >
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                </asp:LinkButton>
                                                                                            </div>
                                                                                            <div class="col-md-4 paddingLeft0">
                                                                                                <asp:Label runat="server" Enabled="true" ID="lblCountryCode"></asp:Label>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-md-12 ">
                                                                                            <div class="col-md-2 labelText1">
                                                                                                <asp:Label runat="server">Bank Id</asp:Label>
                                                                                            </div>
                                                                                            <div class="col-md-3">
                                                                                                <asp:TextBox runat="server" Enabled="true" ID="txtBankId" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtBankId_TextChanged"></asp:TextBox>
                                                                                            </div>
                                                                                            <div class="col-md-1 paddingLeft0">
                                                                                                <asp:LinkButton ID="lbtnSeBankId" CausesValidation="false" runat="server" OnClick="lbtnSeBankId_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                </asp:LinkButton>
                                                                                            </div>
                                                                                            <div class="col-md-4 paddingLeft0">
                                                                                                <asp:Label runat="server" Enabled="true" ID="lblBank"></asp:Label>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-md-12">
                                                                                            <div class="col-md-2 labelText1">
                                                                                                <asp:Label runat="server">Bank Code</asp:Label>
                                                                                            </div>
                                                                                            <div class="col-md-3">
                                                                                                <asp:TextBox runat="server" ID="txtBankCode" CausesValidation="false" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                                                                            </div>
                                                                                            <div class="col-md-3 paddingLeft0">
                                                                                            </div>
                                                                                            <div class="col-md-3 paddingLeft0">
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-md-12">
                                                                                            <div class="col-md-2 labelText1">
                                                                                                <asp:Label runat="server">Bank Name</asp:Label>
                                                                                            </div>
                                                                                            <div class="col-md-6">
                                                                                                <asp:TextBox runat="server" ID="txtBankName" CausesValidation="false" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                                                                            </div>
                                                                                            <div class="col-md-1 paddingLeft0">
                                                                                            </div>
                                                                                            <div class="col-md-3 paddingLeft0">
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-md-12">
                                                                                            <div class="col-md-2 labelText1">
                                                                                                <asp:Label runat="server">Sun Code</asp:Label>
                                                                                            </div>
                                                                                            <div class="col-md-3">
                                                                                                <asp:TextBox runat="server"  ID="txtSunCode" CausesValidation="false" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                                                                            </div>
                                                                                            <div class="col-md-1 paddingLeft0">
                                                                                            </div>
                                                                                            <div class="col-md-3 paddingLeft0">
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-md-12">
                                                                                            <div class="col-md-2 labelText1">
                                                                                            </div>
                                                                                            <div class="col-md-10 ">
                                                                                                <div class="col-md-12 paddingLeft0">
                                                                                                    <div class="col-md-3 padding0">
                                                                                                    <div class="col-md-1 height22 paddingLeft0">
                                                                                                        <asp:CheckBox ID="chkBankActive" runat="server" AutoPostBack="true" Enabled="true" />
                                                                                                    </div>
                                                                                                    <div class="col-md-1 paddingLeft0">
                                                                                                        <asp:Label runat="server" ID="Label1" CssClass="labelText1">Active</asp:Label>
                                                                                                    </div>
                                                                                                         </div>
                                                                                                    <div class="col-md-3"></div>
                                                                                                    <div class="col-md-5">
                                                                                                        <div class="col-md-12">
                                                                                                            <div class="buttonRow">
                                                                                                                <div class="col-md-6 padding0">
                                                                                                                    <asp:LinkButton ID="lbtnBankSave" CausesValidation="false" runat="server" OnClientClick="return confBankSave();" OnClick="lbtnBankSave_Click">
                                                                                                    <span class="glyphicon glyphicon-edit" aria-hidden="true"  ></span>Save
                                                                                                                    </asp:LinkButton>
                                                                                                                </div>
                                                                                                                <div class="col-md-6 padding0">
                                                                                                                    <asp:LinkButton ID="lbtnBankClear" OnClientClick="ClearConfirm()" CausesValidation="false" runat="server" OnClick="lbtnBankClear_Click">
                                                                                                    <span class="glyphicon glyphicon-refresh" aria-hidden="true"  ></span>Clear
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
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6" style="padding-left: 1px;">
                                                                            <div class="panel panel-default" style="padding-left: 1px; padding-right: 1px;">
                                                                                <div class="panel-heading">
                                                                                    <strong>Branch Details</strong>
                                                                                </div>
                                                                                <div class="panel-body" style="padding-left: 0; padding-right: 0; height: 150px;">
                                                                                    <div class="row">
                                                                                        <div class="col-md-12 ">
                                                                                            <div class="col-md-2 labelText1">
                                                                                                <asp:Label runat="server">Country Code</asp:Label>
                                                                                            </div>
                                                                                            <div class="col-md-3">
                                                                                                <asp:TextBox runat="server" Enabled="false" ID="txtBrCountry" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtBrCountry_TextChanged"></asp:TextBox>
                                                                                            </div>
                                                                                            <div class="col-md-1 paddingLeft0">
                                                                                                <asp:LinkButton ID="lbtnBrSeCountry" CausesValidation="false" runat="server" OnClick="lbtnBrSeCountry_Click" >
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                </asp:LinkButton>
                                                                                            </div>
                                                                                            <div class="col-md-4 paddingLeft0">
                                                                                                <asp:Label runat="server" Enabled="true" ID="lblBrCountry"></asp:Label>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-md-12 ">
                                                                                            <div class="col-md-2 labelText1">
                                                                                                <asp:Label runat="server">Bank Id</asp:Label>
                                                                                                <asp:Label runat="server" ID="lblBankId" Visible="false"></asp:Label>
                                                                                                <asp:Label runat="server" ID="lblBankName" Visible="false"></asp:Label>
                                                                                            </div>
                                                                                            <div class="col-md-3">
                                                                                                <asp:TextBox runat="server" Enabled="false" ID="txtBrBankId" CausesValidation="false" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                                                                            </div>
                                                                                            <div class="col-md-1 paddingLeft0">
                                                                                                <asp:LinkButton ID="lbtnSeBrBankId" CausesValidation="false" runat="server" OnClick="lbtnSeBrBankId_Click">
                                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                </asp:LinkButton>
                                                                                            </div>
                                                                                            <div class="col-md-4 paddingLeft0">
                                                                                                <asp:Label runat="server" Enabled="true" ID="lblBranchBank"></asp:Label>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-md-12">
                                                                                            <div class="col-md-2 labelText1">
                                                                                                <asp:Label runat="server">Branch Code</asp:Label>
                                                                                            </div>
                                                                                            <div class="col-md-3">
                                                                                                <asp:TextBox runat="server" ID="txtBranchCode" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtBranchCode_TextChanged"></asp:TextBox>
                                                                                            </div>
                                                                                            <div class="col-md-3 paddingLeft0">
                                                                                                <asp:LinkButton ID="lbtnSeBrCode" CausesValidation="false" runat="server" OnClick="lbtnSeBrCode_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                </asp:LinkButton>
                                                                                            </div>
                                                                                            <div class="col-md-3 paddingLeft0">
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-md-12">
                                                                                            <div class="col-md-2 labelText1">
                                                                                                <asp:Label runat="server">Branch Name</asp:Label>
                                                                                            </div>
                                                                                            <div class="col-md-6">
                                                                                                <asp:TextBox runat="server" ID="txtBranchName" CausesValidation="false" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                                                                            </div>
                                                                                            <div class="col-md-1 paddingLeft0">
                                                                                            </div>
                                                                                            <div class="col-md-3 paddingLeft0">
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-md-12">
                                                                                            <div class="col-md-2 labelText1">
                                                                                            </div>
                                                                                            <div class="col-md-3 ">
                                                                                                <div class="col-md-1 height22 paddingLeft0">
                                                                                                    <asp:CheckBox ID="chkBranchActive" runat="server" AutoPostBack="true" Enabled="true" />
                                                                                                </div>
                                                                                                <div class="col-md-8 paddingLeft0">
                                                                                                    <asp:Label runat="server" ID="lblShowCostValue" CssClass="labelText1">Active</asp:Label>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-7 labelText1">
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-md-12 buttonRow">
                                                                                            <div class="col-md-8 labelText1">
                                                                                            </div>
                                                                                            <div class="col-md-4">
                                                                                                <div class="col-md-12">
                                                                                                    <div class="col-md-6 padding0">
                                                                                                        <asp:LinkButton ID="lbtnBranchSave" OnClientClick="return confBranchSave();" CausesValidation="false" runat="server" OnClick="lbtnBranchSave_Click">
                                                                                                    <span class="glyphicon glyphicon-edit" aria-hidden="true"  ></span>Save
                                                                                                        </asp:LinkButton>
                                                                                                    </div>
                                                                                                    <div class="col-md-6 padding0">
                                                                                                        <asp:LinkButton ID="lbtnBranchClear" OnClientClick="ClearConfirm()" CausesValidation="false" runat="server" OnClick="lbtnBranchClear_Click">
                                                                                                    <span class="glyphicon glyphicon-refresh" aria-hidden="true"  ></span>Clear
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
            </div>
        </div>
    </div>
    <asp:UpdatePanel runat="server" ID="update">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupSearch" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="testPanel" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="testPanel" DefaultButton="lbtnSearch">
        <div runat="server" id="test" class="panel panel-primary Mheight">

            <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="btnClose" runat="server">
                             <span class="glyphicon glyphicon-remove" style="margin-left:560px" aria-hidden="true">Close</span>
                    </asp:LinkButton>
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
                                        <asp:DropDownList ID="cmbSearchbykey" runat="server" class="form-control" AutoPostBack="false">
                                        </asp:DropDownList>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="col-sm-2 labelText1">
                                Search by word
                            </div>

                            <asp:Label ID="lblSearchType" runat="server" Text="" Visible="False"></asp:Label>
                            <div class="col-sm-4 paddingRight5">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                <asp:TextBox ID="txtSearchbyword" CausesValidation="false" class="form-control" AutoPostBack="false" runat="server"></asp:TextBox>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                            </div>
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy9" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-1 paddingLeft0">
                                        <asp:LinkButton ID="lbtnSearch" runat="server" OnClick="lbtnSearch_Click">
                                     <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
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
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="dgvResultItem" CausesValidation="false" runat="server" AllowPaging="True" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" OnPageIndexChanging="dgvResultItem_PageIndexChanging" OnSelectedIndexChanged="dgvResultItem_SelectedIndexChanged">
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
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupDilogResult" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="panelDivDilogResult" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="row">
        <div class="col-sm-12 col-lg-12">
            <asp:Panel runat="server" ID="panelDivDilogResult">
                <div class="row">
                    <div class="col-sm-12 col-lg-12 panel panel-body">
                        <div class="row">
                            <div class="col-sm-12 col-lg-12 fontsize18">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lblDilogResult" runat="server"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="row ">
                            <div class="col-lg-12 col-sm-12">
                                <div class="row buttonRow">
                                    <div class="col-sm-3 col-md-3">
                                    </div>
                                    <div class="col-sm-3 col-md-3" style="padding-left: 3px; padding-right: 3px;">
                                        <asp:LinkButton ID="lBtnDilogResultYes" runat="server" OnClick="lBtnDilogResultYes_Click">
                             <span class="glyphicon glyphicon-ok"  aria-hidden="true"> Yes</span>
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-3 col-md-3" style="padding-left: 3px; padding-right: 3px;">
                                        <asp:LinkButton ID="lBtnDilogResultNo" runat="server" OnClick="lBtnDilogResultNo_Click">
                             <span class="glyphicon glyphicon-remove"  aria-hidden="true"> No</span>
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-3 col-md-3">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>

    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>

</asp:Content>
