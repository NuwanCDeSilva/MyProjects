<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="BufferAllocationSetup.aspx.cs" Inherits="FastForward.SCMWeb.View.MasterFiles.Inventory.BufferAllocationSetup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
   
     <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>

    
    <script>
        function SaveConfirm() {

            var selectedvalue = confirm("Do you want to save data?");
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
        function ConfirmClearForm() {
            var selectedvalueOrd = confirm("Do you want to clear all details ?");
            if (selectedvalueOrd) {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "No";
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

        .readOnlyText {
            background-color: #F2F2F2 !important;
            color: #C6C6C6;
            border-color: #ddd;
        }
    </style>
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy8" runat="server"></asp:ScriptManagerProxy>   
    <asp:HiddenField ID="txtACancelconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtClearlconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtSavelconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtUpdateconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtApprovalconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtCancelconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtDeleteconformmessageValue" runat="server" />
    <asp:HiddenField ID="Hddnvalue" runat="server" />
    <div class="panel panel-default marginLeftRight5">
        <div class="row">
            <div class="col-sm-12 buttonrow">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="col-sm-12 buttonRow paddingRight5" id="divTopCheck" runat="server">
                            <div class="col-sm-7 buttonRow padding0">
                            </div>
                            <div class="col-sm-5 buttonRow padding0">

                                <div class="col-sm-10">
                                </div>

                                <div class="col-sm-2">
                                    <asp:LinkButton ID="lblUClear" runat="server" CssClass="floatRight" OnClick="lbtnclear_Click" OnClientClick="ConfirmClearForm();">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">


                <div class="panel-body">
                    <div class="bs-example">
                        <ul class="nav nav-tabs" id="myTab">
                            <li class="active"><a href="#BLevelGread" data-toggle="tab">Buffer level upload </a></li>
                            <%--  <li><a href="#BLevelLocation" data-toggle="tab" >Buffer level upload (Location wise)</a></li>--%>
                            <%--<li><a href="#Session" data-toggle="tab">Session Setup </a></li>
                    <li><a href="#Grade" data-toggle="tab">Grade Setup </a></li>--%>
                        </ul>
                    </div>
                    <div class="tab-content">
                        <div class="tab-pane active" id="BLevelGread">
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>

                                    <div class="row">
                                        <div class="col-sm-12 height20">
                                        </div>
                                    </div>
                                    <div class="row">

                                        <div class="col-sm-5">
                                            <div class="panel panel-default marginLeftRight5">
                                                <div class="panel-heading ">
                                                    Buffer level upload (Grade Wise)
                                                </div>
                                                <div class="panel-body">
                                                    <div class="row">

                                                        <div class="col-sm-2 labelText1">
                                                            Company
                                                        </div>
                                                        <div class="col-sm-3 paddingRight5">
                                                            <asp:TextBox runat="server" ID="txtCompany" CausesValidation="false" Style="text-transform: uppercase" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtCompany_TextChanged"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0">
                                                            <asp:LinkButton ID="lbtncompany" CausesValidation="false" runat="server" OnClick="lbtncompany_Click">
                         <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                        <div class="col-sm-4 paddingRight0">
                                                            <asp:Label runat="server" ID="lblName"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="row">

                                                        <div class="col-sm-2 labelText1">
                                                            Channel
                                                        </div>
                                                        <div class="col-sm-3 paddingRight5">
                                                            <asp:TextBox runat="server" ID="txtChannel" CausesValidation="false" AutoPostBack="true" CssClass="form-control" Style="text-transform: uppercase" OnTextChanged="txtChannel_TextChanged"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0">
                                                            <asp:LinkButton ID="lblChannel" CausesValidation="false" runat="server" OnClick="lblChannel_Click">
                         <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                        <div class="col-sm-4 paddingRight0">
                                                            <asp:Label runat="server" ID="lblchanel"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="row">

                                                        <div class="col-sm-2 labelText1">
                                                            Grade
                                                        </div>
                                                        <div class="col-sm-3 paddingRight5">
                                                            <asp:DropDownList ID="ddlgrade" CausesValidation="false" runat="server" CssClass="form-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0">
                                                        </div>
                                                        <div class="col-sm-4 paddingRight0">
                                                        </div>
                                                    </div>
                                                    <div class="row">

                                                        <div class="col-sm-2 labelText1">
                                                            Season
                                                        </div>
                                                        <div class="col-sm-3 paddingRight5">
                                                            <asp:DropDownList ID="ddlseason" CausesValidation="false" runat="server" CssClass="form-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0">
                                                        </div>
                                                        <div class="col-sm-4 paddingRight0">
                                                        </div>
                                                    </div>
                                                    <div class="row">

                                                        <div class="col-sm-2 labelText1">
                                                            Item Code
                                                        </div>
                                                        <div class="col-sm-3 paddingRight5">
                                                            <asp:TextBox runat="server" ID="txtItemcode" CausesValidation="false" AutoPostBack="true" CssClass="form-control" Style="text-transform: uppercase" OnTextChanged="txtItemcode_TextChanged"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0">
                                                            <asp:LinkButton ID="lbtnItemCode" CausesValidation="false" runat="server" OnClick="lbtnItemCode_Click">
                         <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                        <div class="col-sm-4 paddingRight0">
                                                            <asp:Label runat="server" ID="lblItem"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="row">

                                                        <div class="col-sm-2 labelText1">
                                                            Item Status
                                                        </div>
                                                        <div class="col-sm-3 paddingRight5">
                                                            <asp:DropDownList ID="ddlStatus" Enabled="false" CausesValidation="false" runat="server" CssClass="form-control">
                                                            </asp:DropDownList>
                                                        </div>


                                                    </div>
                                                    <div class="row">

                                                        <div class="col-sm-2 labelText1">
                                                            Buffer Qty
                                                        </div>
                                                        <div class="col-sm-2 paddingRight5">
                                                            <asp:TextBox runat="server" ID="txtBQty" onkeypress="return isNumberKey(event)" Style="text-align: right" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtBQty_TextChanged"></asp:TextBox>
                                                        </div>


                                                    </div>

                                                    <div class="row">

                                                        <div class="col-sm-3 labelText1">
                                                            Allow Excel upload
                                                        </div>
                                                        <div class="col-sm-2 paddingRight5">
                                                            <asp:CheckBox ID="chkIsExcelupload" AutoPostBack="true" runat="server" OnCheckedChanged="chkIsExcelupload_CheckedChanged" />
                                                        </div>
                                                    </div>
                                                    <asp:Panel runat="server" ID="pnlexcel" Visible="false">
                                                    </asp:Panel>
                                                    <div class="row">
                                                        <div class="col-sm-2 labelText1">
                                                        </div>
                                                        <div class="col-sm-1">
                                                            <asp:Button ID="btnSave" class="btn btn-success btn-xs" runat="server" Text="Save" OnClick="btnSave_Click" OnClientClick="SaveConfirm();" />
                                                        </div>
                                                        <div class="col-sm-2 ">
                                                            <asp:Button ID="btnClear" class="btn btn-default btn-xs" OnClientClick="ConfirmClearForm();" runat="server" Text="Clear" OnClick="btnClear_Click" />
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <asp:Button ID="btnUpdate" class="btn btn-default btn-xs" Visible="false" runat="server" Text="Update" />
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-5">
                                            <div class="panel panel-default marginLeftRight5">
                                                <div class="panel-heading ">
                                                    Buffer level upload (Location Wise)
                                                </div>
                                                <div class="panel-body">
                                                    <div class="row">

                                                        <div class="col-sm-2 labelText1">
                                                            Company
                                                        </div>
                                                        <div class="col-sm-3 paddingRight5">
                                                            <asp:TextBox runat="server" ID="txtLcompany" CausesValidation="false" Style="text-transform: uppercase" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtLcompany_TextChanged"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0">
                                                            <asp:LinkButton ID="lbtnlocompany" CausesValidation="false" runat="server" OnClick="lbtnlocompany_Click">
                         <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                        <div class="col-sm-4 paddingRight0">
                                                            <asp:Label runat="server" ID="lblName_2"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="row">

                                                        <div class="col-sm-2 labelText1">
                                                            Location
                                                        </div>
                                                        <div class="col-sm-3 paddingRight5">
                                                            <asp:TextBox runat="server" ID="txtlocation" CausesValidation="false" AutoPostBack="true" CssClass="form-control" Style="text-transform: uppercase" OnTextChanged="txtlocation_TextChanged"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0">
                                                            <asp:LinkButton ID="lbtnLocation" CausesValidation="false" runat="server" OnClick="lbtnLocation_Click">
                         <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                        <div class="col-sm-4 paddingRight0">
                                                            <asp:Label runat="server" ID="lblLocation"></asp:Label>
                                                        </div>
                                                    </div>


                                                    <div class="row">
                                                        <div class="col-sm-2 labelText1">
                                                            Item Code
                                                        </div>
                                                        <div class="col-sm-3 paddingRight5">
                                                            <asp:TextBox runat="server" ID="txtLitemCode" CausesValidation="false" AutoPostBack="true" CssClass="form-control" Style="text-transform: uppercase" OnTextChanged="txtLitemCode_TextChanged"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0">
                                                            <asp:LinkButton ID="lbtnLoItemCode" CausesValidation="false" runat="server" OnClick="lbtnLoItemCode_Click">
                         <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                        <div class="col-sm-4 paddingRight0">
                                                            <asp:Label runat="server" ID="lblLoItemcode"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="row">

                                                        <div class="col-sm-2 labelText1">
                                                            Item Status
                                                        </div>
                                                        <div class="col-sm-3 paddingRight5">
                                                            <asp:DropDownList ID="ddlStatusLoc" Enabled="false" CausesValidation="false" runat="server" CssClass="form-control">
                                                            </asp:DropDownList>
                                                        </div>


                                                    </div>
                                                    <div class="row">

                                                        <div class="col-sm-2 labelText1">
                                                            Buffer Qty
                                                        </div>
                                                        <div class="col-sm-2 paddingRight5">
                                                            <asp:TextBox runat="server" ID="txtQtyLoc" onkeypress="return isNumberKey(event)" Style="text-align: right" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtQtyLoc_TextChanged"></asp:TextBox>
                                                        </div>                                                      

                                                    </div>
                                                     <div class="row">

                                                            <div class="col-sm-2 labelText1">
                                                                Re.Order Level
                                                            </div>
                                                            <div class="col-sm-2 paddingRight5">
                                                                <asp:TextBox runat="server" ID="ROLText" onkeypress="return isNumberKey(event)" Style="text-align: right" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtreorderLoc_TextChanged"></asp:TextBox>
                                                            </div>


                                                        </div>
                                                    <div class="row">

                                                        <div class="col-sm-3 labelText1">
                                                            Allow Excel upload
                                                        </div>
                                                        <div class="col-sm-2 paddingRight5">
                                                            <asp:CheckBox ID="chkLoc" AutoPostBack="true" runat="server" OnCheckedChanged="chkLoc_CheckedChanged" />
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-2 labelText1">
                                                        </div>
                                                        <div class="col-sm-1">
                                                            <asp:Button ID="btnSave_loc" class="btn btn-success btn-xs" runat="server" Text="Save" OnClick="btnSave_loc_Click" OnClientClick="SaveConfirm();" />
                                                        </div>
                                                        <div class="col-sm-2 ">
                                                            <asp:Button ID="btnClear2" class="btn btn-default btn-xs" OnClientClick="ConfirmClearForm();" runat="server" Text="Clear" OnClick="btnClear2_Click" />
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <asp:Button ID="Button6" class="btn btn-default btn-xs" Visible="false" runat="server" Text="Update" />
                                                        </div>

                                                    </div>

                                                    <asp:Panel runat="server" ID="pnlexcelLoc" Visible="false">
                                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>


                                                    </asp:Panel>
                                                    <div class="row">
                                                        <div class="col-sm-12 height42">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="tab-pane " id="BLevelLocation">
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-sm-12 height20">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-5">
                                        </div>
                                        <div class="col-sm-7">
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

    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserPopup" runat="server" Enabled="True" TargetControlID="Button3"
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
                            <asp:Panel runat="server" ID="searpnl">
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
                            </asp:Panel>
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





    <asp:UpdatePanel ID="UpdatePanel7" runat="server">

        <ContentTemplate>

            <asp:Button ID="Button4" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="excelUpload" runat="server" Enabled="True" TargetControlID="Button4"
                PopupControlID="pnlexcelup" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>


        </ContentTemplate>

    </asp:UpdatePanel>
     <asp:UpdateProgress ID="UpdateProgress3" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel11">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait3" runat="server" Text="Please wait... " />
                <asp:Image ID="imgWait3" runat="server" ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel11" runat="server">

        <ContentTemplate>

            <asp:Panel runat="server" ID="pnlexcelup">
          
                <div runat="server" id="dv" class="panel panel-default height45 width700 ">

                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="lbtnexClose" runat="server" OnClick="lbtnexClose_Click">
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
                                                        <asp:Button ID="btnupload" class="btn btn-warning btn-xs" runat="server" Text="Upload" OnClick="btnupload_Click" />
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


</asp:Content>
