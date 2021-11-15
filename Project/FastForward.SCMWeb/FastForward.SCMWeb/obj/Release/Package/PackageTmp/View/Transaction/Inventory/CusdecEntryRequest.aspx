 <%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="CusdecEntryRequest.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Inventory.Cusdec_Entry_Request" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/jquery.toastmessage.js"></script>

    <script type="text/javascript">
        function showStickySuccessToast(value) {
            $().toastmessage('showToast', {
                text: value,
                sticky: true,
                //position: 'top-center',
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
        function CancelConfirm() {
            var selectedvalue = confirm("Do you want to cancel?");
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
        function CheckAllgrdItem(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdItem.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }
        function CheckAllgrdBlItem(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdBlItem.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }
        function CheckAllgrdreqItem(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdrequestItem.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }
        function CheckBoxCheck(rb) {
            debugger;
            var gv = document.getElementById("<%=grdrequest.ClientID%>");
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
        };
        function CheckBoxCheckReq_item(rb) {
            debugger;
            var gv = document.getElementById("<%=grdrequestItem.ClientID%>");
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
     <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="pnlMain">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="pnlMain">
        <ContentTemplate>
            <asp:HiddenField ID="txtClearlconformmessageValue" runat="server" />
            <asp:HiddenField ID="txtSavelconformmessageValue" runat="server" />
            <asp:HiddenField ID="txtUpdateconformmessageValue" runat="server" />
            <asp:HiddenField ID="txtApprovalconformmessageValue" runat="server" />
            <asp:HiddenField ID="txtCancelconformmessageValue" runat="server" />
            <asp:HiddenField ID="txtDeleteconformmessageValue" runat="server" />
            <asp:HiddenField ID="HiddenFieldRequesttype" runat="server" />
            <asp:HiddenField ID="HiddenFieldBQty" runat="server" />
            <asp:HiddenField ID="HiddenFieldReqQty" runat="server" />
            <asp:HiddenField ID="HiddenFieldReqcomp" runat="server" />
            <asp:HiddenField ID="HiddenGRNloc" runat="server" />
            <asp:HiddenField ID="HiddenItem" runat="server" />
            <asp:HiddenField ID="Hiddenstatus" runat="server" />
            <div class="panel panel-default marginLeftRight5">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="col-sm-6  buttonrow">
                            </div>
                            <div class="col-sm-6  buttonRow">
                                <div class="col-sm-4"></div>
                                <div class="col-sm-2 ">
                                    <asp:LinkButton ID="lbtnSave" CausesValidation="false" runat="server" OnClientClick="SaveConfirm()" CssClass="floatRight" OnClick="lbtnSave_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-2 ">
                                    <asp:LinkButton ID="lbtnupdate" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lbtnupdate_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Amend
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-2 ">
                                    <asp:LinkButton ID="lbtnCancel" CausesValidation="false" runat="server" OnClientClick="CancelConfirm()" CssClass="floatRight"  OnClick="lbtnCancel_Click">
                                        <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>Cancel
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-2">
                                    <asp:LinkButton ID="lbtnClear" runat="server" CssClass="floatRight" OnClientClick="ClearConfirm()" OnClick="lbtnClear_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6 paddingRight5">
                            <div class="panel panel-default ">
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-sm-7">
                                            Cusdec Entry Request
                                        </div>
                                        <div class="col-sm-1">
                                            Status:
                                        </div>
                                        <div class="col-sm-3 paddingLeft0">
                                            <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>


                                </div>

                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="row">
                                                <div class="col-sm-4 labelText1">
                                                    Request # 
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox runat="server" Enabled="true" ID="txtRequest" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtRequest_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                    <asp:LinkButton ID="lbtnreqno" runat="server" CausesValidation="false" OnClick="lbtnreqno_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4 labelText1">
                                                    Request Type
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="ddlRequestType" runat="server" class="form-control">
                                                    </asp:DropDownList>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4 labelText1">
                                                    Ref. #
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox runat="server" ID="txtnewref" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4 labelText1">
                                                    Transfer Type
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox runat="server" Enabled="false" ID="txttransfertype" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1  paddingLeft0 paddingRight0">
                                                    <asp:LinkButton ID="lbtntransfertype" Visible="false" runat="server" CausesValidation="false">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4 labelText1">
                                                    Preferred Location
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox runat="server" ID="txtplocation" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtplocation_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                    <asp:LinkButton ID="lbtnplocation" runat="server" CausesValidation="false" OnClick="lbtnplocation_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                             <div class="row">
                                                <div class="col-sm-4 labelText1">
                                                    Issue Location
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox runat="server" ID="txtissueloc" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtissueloc_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                    <asp:LinkButton ID="btnissueloc" runat="server" CausesValidation="false" OnClick="btnissueloc_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-sm-6">
                                            <div class="row">
                                                <div class="col-sm-4 labelText1">
                                                    Customer
                                                </div>
                                                <div class="col-sm-5">

                                                    <asp:TextBox runat="server" ID="txtCustomer" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtCustomer_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                    <asp:LinkButton ID="lbtnCustomer" runat="server" CausesValidation="false" OnClick="lbtnCustomer_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                    <asp:Label ID="lblCustomerName" Visible="false" runat="server" Text="Customer Name"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4 labelText1">
                                                    Other Ref. #
                                                </div>
                                                <div class="col-sm-7">
                                                    <asp:TextBox runat="server" Enabled="false" ID="txtref" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4 labelText1">
                                                    Date
                                                </div>
                                                <div class="col-sm-5 ">
                                                    <asp:TextBox runat="server" ReadOnly="true" ID="txtDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <%--<asp:LinkButton ID="lbtnDate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDate"
                                                        PopupButtonID="lbtnDate" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>--%>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4 labelText1">
                                                    Goods Given Date
                                                </div>
                                                <div class="col-sm-5 ">
                                                    <asp:TextBox runat="server" ReadOnly="true" ID="txtgoodgivenDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtngoodgivenDate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtgoodgivenDate"
                                                        PopupButtonID="lbtngoodgivenDate" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <asp:Panel runat="server" ID="pnlToBod">
                                                <div class="row">
                                                    <div class="col-sm-4 labelText1">
                                                        To Bond/BL #
                                                    </div>
                                                    <div class="col-sm-7">
                                                        <asp:TextBox runat="server" Enabled="false" ID="txtTobond" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>

                                                </div>
                                            </asp:Panel>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>

                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-2 labelText1">
                                            Remark
                                        </div>
                                        <div class="col-sm-10">
                                            <asp:TextBox runat="server" TextMode="MultiLine" ID="txtremark" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <asp:Panel runat="server" ID="pnlItemadd">
                                            <div class="col-sm-12">
                                                <div class="panel panel-default ">
                                                    <div class="panel-body">
                                                        <div class="row">
                                                            <div class="col-sm-2 labelText1  ">
                                                                Item
                                                            </div>
                                                            <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                <asp:LinkButton ID="lbtnblItem" runat="server" CausesValidation="false" OnClick="lbtnblItem_Click" ToolTip="SI Base">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="col-sm-3 labelText1 paddingLeft5 paddingRight0 ">
                                                                Res #
                                                            </div>
                                                            <asp:Panel runat="server" ID="pnltoboneName">
                                                                <div class="col-sm-1 labelText1 paddingLeft5 paddingRight0">
                                                                    To Bond #
                                                                </div>
                                                                <div class="col-sm-1   paddingLeft0">
                                                                    <asp:RadioButton ID="rdotobound" AutoPostBack="true" Checked="true" GroupName="bondgroup" runat="server" OnCheckedChanged="rdotobound_CheckedChanged" />

                                                                </div>
                                                                <div class="col-sm-1 labelText1 paddingLeft5 paddingRight0 ">
                                                                    BL #
                                                                </div>
                                                                <div class="col-sm-1  paddingRight0 paddingLeft0">
                                                                    <asp:RadioButton ID="rdBl" AutoPostBack="true" GroupName="bondgroup" runat="server" OnCheckedChanged="rdBl_CheckedChanged" />

                                                                </div>
                                                            </asp:Panel>
                                                            <div class="col-sm-2 labelText1 paddingLeft5 ">
                                                                Quantity
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-3  paddingRight0">
                                                                <div class="col-sm-11 paddingLeft0 paddingRight0">
                                                                    <asp:TextBox runat="server" ID="txtItem" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtItem_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:LinkButton ID="lbtnitem" runat="server" CausesValidation="false" OnClick="lbtnitem_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-3 paddingLeft0 paddingRight0">
                                                                <div class="col-sm-11 paddingLeft0 paddingRight0">
                                                                    <asp:TextBox runat="server" ID="txtRes" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:LinkButton ID="lbtnres" runat="server" CausesValidation="false" OnClick="lbtnres_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <asp:Panel runat="server" ID="pnltobonetxtbox">
                                                                <div class="col-sm-4 paddingLeft5 paddingRight5">
                                                                    <div class="col-sm-11 paddingLeft0 paddingRight0">
                                                                        <asp:TextBox runat="server" ID="txtTobond_Bl" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtTobond_Bl_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnTobond_bl" runat="server" CausesValidation="false" OnClick="lbtnTobond_bl_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                            <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                <asp:TextBox runat="server" ID="txtqty" CausesValidation="false" AutoPostBack="true" CssClass="form-control" onkeypress="return isNumberKey(event)" Style="text-align: right"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1 Lwidth paddingLeft5 paddingRight5">
                                                                <asp:LinkButton ID="lbtnAddItem" runat="server" CausesValidation="false" OnClick="lbtnAddItem_Click">
                                                <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12 height5">
                                                            </div>
                                                        </div>
                                                        <div class="row">

                                                            <div class="col-sm-7 paddingLeft0">
                                                                <div class="col-sm-3 labelText1  ">
                                                                    <strong>Description:</strong>
                                                                </div>
                                                                <div class="col-sm-8 labelText1 ">
                                                                    <asp:Label runat="server" ID="lblItemDes" CssClass="Color1 fontWeight900"></asp:Label>

                                                                </div>
                                                            </div>
                                                            <div class="col-sm-3 paddingLeft0">
                                                                <div class="col-sm-4 labelText1 paddingLeft0 ">
                                                                    <strong>Model:</strong>
                                                                </div>
                                                                <div class="col-sm-8 labelText1 ">
                                                                    <asp:Label runat="server" ID="lblItemModel" CssClass="Color1 fontWeight900"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-2 paddingLeft0">
                                                                <div class="col-sm-4 labelText1 paddingLeft0 ">
                                                                    <strong>UOM:</strong>
                                                                </div>
                                                                <div class="col-sm-8 labelText1 ">
                                                                    <asp:Label runat="server" ID="lblItemUom" CssClass="Color1 fontWeight900"></asp:Label>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>


                                    <div class="row">
                                        <div class="col-sm-12 height10">
                                            <asp:LinkButton ID="lbtnDeleteItem" runat="server" CausesValidation="false" OnClick="lbtnDeleteItem_Click" OnClientClick="DeleteConfirm()">
                                                                    <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 height10">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 ">
                                            <div class="panel-body  panelscollbar height200">
                                                <asp:GridView ID="grdItem" ShowHeader="true" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No data found..." AutoGenerateColumns="False" OnRowDataBound="grdItem_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="" Visible="true">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="allchk" runat="server" Width="5px" onclick="CheckAllgrdItem(this)"></asp:CheckBox>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chk_Items" runat="server" AutoPostBack="true" Checked="false" Width="5px" OnCheckedChanged="chk_Items_Click" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Code">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_Itri_itm_cd" runat="server" Text='<%# Bind("Itri_itm_cd") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="GRN Item Code">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_itri_mitm_cd" runat="server" Text='<%# Bind("itri_mitm_cd") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="RER #" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_Itri_res_no" runat="server" Text='<%# Bind("Itri_res_no") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="To Bond #">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_tobond" runat="server" Text='<%# Bind("To_bond") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="BL #">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_BL" runat="server" Text='<%# Bind("BL") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_Itri_qty" runat="server" Text='<%# Bind("Itri_qty","{0:n}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cond" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_itri_itm_cond" runat="server" Text='<%# Bind("itri_itm_cond") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Job Line No" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_itri_job_line" runat="server" Text='<%# Bind("Itri_job_line") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Line No" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_itri_line_no" runat="server" Text='<%# Bind("itri_line_no") %>'></asp:Label>
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
                        <div class="col-sm-6 paddingLeft5">

                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="panel panel-default ">
                                        <div class="panel-body">
                                            <div class="col-sm-4">
                                                <div class="col-sm-10 labelText1 paddingLeft5 paddingRight5" runat="server" id="All">
                                                    Pending Entry Requests
                                                </div>
                                                <div class="col-sm-1  paddingRight0 paddingLeft5">
                                                    <asp:RadioButton ID="radpendingreq" AutoPostBack="true" GroupName="reqgroup" runat="server" OnCheckedChanged="radpendingreq_CheckedChanged" />

                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="col-sm-10 labelText1 paddingLeft5 paddingRight5" runat="server" id="Div1">
                                                    Approved Reservations
                                                </div>
                                                <div class="col-sm-1  paddingRight0 paddingLeft5">
                                                    <asp:RadioButton ID="radpendigres" runat="server" AutoPostBack="true" GroupName="reqgroup" OnCheckedChanged="radpendigres_CheckedChanged" />

                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="panel panel-default ">
                                <div class="panel-body">
                                    <asp:Panel runat="server" ID="pnlPermision">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="row">
                                                    <div class="col-sm-4 labelText1  ">
                                                        Customer
                                                    </div>
                                                    <div class="col-sm-5 paddingLeft0">
                                                        <asp:TextBox runat="server" AutoPostBack="true" ID="txtSearchCustomer" CausesValidation="false" CssClass="form-control" OnTextChanged="txtSearchCustomer_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                        <asp:LinkButton ID="lbtnSearchCustomer" runat="server" CausesValidation="false" OnClick="lbtnSearchCustomer_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div class="col-sm-1 labelText1 paddingLeft5 paddingRight5" runat="server" id="Div3">
                                                        All
                                                    </div>
                                                    <div class="col-sm-1  paddingRight0 paddingLeft5">
                                                        <asp:CheckBox ID="CheckBox3" AutoPostBack="true" Checked="true" runat="server" Width="5px"></asp:CheckBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <asp:Panel runat="server" Visible="false" ID="Panel2">
                                                    <div class="row">
                                                        <div class="col-sm-4 labelText1  ">
                                                            Profit Center
                                                        </div>
                                                        <div class="col-sm-5 paddingLeft0">
                                                            <asp:TextBox runat="server" AutoPostBack="true" ID="txtSearchprofitcenter" CausesValidation="false" CssClass="form-control" OnTextChanged="txtSearchprofitcenter_TextChanged"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                            <asp:LinkButton ID="lbtnsearchproftcenter" runat="server" CausesValidation="false" OnClick="lbtnsearchproftcenter_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                            </asp:LinkButton>
                                                        </div>
                                                        <div class="col-sm-1 labelText1 paddingLeft5 paddingRight5" runat="server" id="Div2">
                                                            All
                                                        </div>
                                                        <div class="col-sm-1  paddingRight0 paddingLeft5">
                                                            <asp:CheckBox ID="CheckBox2" AutoPostBack="true" Checked="true" runat="server" Width="5px"></asp:CheckBox>
                                                        </div>
                                                    </div>
                                                </asp:Panel>


                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="row">
                                                    <div class="col-sm-2 labelText1">
                                                        From
                                                    </div>
                                                    <div class="col-sm-7 ">
                                                        <asp:TextBox runat="server" ReadOnly="true" ID="txtFrom" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnFrom" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtFrom"
                                                            PopupButtonID="lbtnFrom" Format="dd/MMM/yyyy">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-2 labelText1">
                                                        To
                                                    </div>
                                                    <div class="col-sm-7 ">
                                                        <asp:TextBox runat="server" ReadOnly="true" ID="txtto" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnto" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtto"
                                                            PopupButtonID="lbtnto" Format="dd/MMM/yyyy">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:LinkButton ID="btnSearch" runat="server" CausesValidation="false" OnClick="btnSearch_Click">
                                                    <span class="glyphicon glyphicon-search fontsize20 right5" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                                <asp:Label ID="lblres" Visible="false"  runat="server" ></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12 ">
                                                <div class="panel-body  panelscollbar height200">
                                                    <asp:GridView ID="grdrequest" ShowHeader="true" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                        EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chk_Req" AutoPostBack="true" runat="server" onclick="CheckBoxCheck(this);" Checked="false" Width="10px" OnCheckedChanged="chk_Req_CheckedChanged_Click" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Req #" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="col_itr_req_no" runat="server" Text='<%# Bind("itr_req_no") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Doc #">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="col_ird_res_no" runat="server" Text='<%# Bind("ird_res_no") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Req.Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="col_itr_dto" runat="server" Text='<%# Bind("itr_dt","{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Profit Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="col_itr_loc" runat="server" Text='<%# Bind("itr_loc") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Customer">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="col_itr_bus_code" runat="server" Text='<%# Bind("itr_bus_code") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Customer Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="col_mbe_name" runat="server" Text='<%# Bind("Customername") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Tran.Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="col_itr_tp" runat="server" Text='<%# Bind("itr_tp") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Tran.Type" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="col_Itr_sub_tp" runat="server" Text='<%# Bind("Itr_sub_tp") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Tran.Type" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="col_itr_exp_dt" runat="server" Text='<%# Bind("itr_exp_dt") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Tran.Type" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="col_itr_ref" runat="server" Text='<%# Bind("itr_ref") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Tran.Type" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="col_itr_note" runat="server" Text='<%# Bind("itr_note") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Tran.Type" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="col_itr_collector_name" runat="server" Text='<%# Bind("itr_collector_name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>

                                        </div>
                                    </asp:Panel>
                                    <asp:Panel runat="server" ID="pnlres">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="panel panel-default ">
                                                    <div class="panel-body">
                                                        <div class="row">
                                                            <asp:Panel runat="server" ID="Panel1">
                                                                <div class="col-sm-2 labelText1  paddingRight0">
                                                                    To Bond #
                                                                </div>
                                                                <div class="col-sm-1   paddingLeft0">
                                                                    <asp:RadioButton ID="radpendingTobond" AutoPostBack="true" Checked="true" GroupName="bondgroup" runat="server" OnCheckedChanged="radpendingTobond_CheckedChanged" />

                                                                </div>
                                                                <div class="col-sm-1 labelText1 paddingLeft5 paddingRight0 ">
                                                                    BL #
                                                                </div>
                                                                <div class="col-sm-1  paddingRight0 paddingLeft0">
                                                                    <asp:RadioButton ID="radpendingBL" Enabled="false" AutoPostBack="true" GroupName="bondgroup" runat="server" OnCheckedChanged="radpendingBL_CheckedChanged" />

                                                                </div>

                                                                <div class="col-sm-3 paddingLeft5 paddingRight5">
                                                                    <div class="col-sm-11 paddingLeft0 paddingRight0">
                                                                        <asp:TextBox runat="server" ID="txtaddbond" Enabled="false" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnAddbondsearch" runat="server" CausesValidation="false" OnClick="lbtnAddbondsearch_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-1 labelText1 paddingLeft5 ">
                                                                    Qty
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                    <asp:TextBox runat="server" ID="txtbondqty" CausesValidation="false" CssClass="form-control" onkeypress="return isNumberKey(event)" Style="text-align: right"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-2">
                                                                    <asp:LinkButton ID="lbtnAddbond" runat="server" CausesValidation="false" OnClick="lbtnAddbond_Click">
                                                                         <span class="glyphicon glyphicon-plus " aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 paddingLeft5 ">
                                                <div class="panel-body panelscollbar height175">
                                                    <asp:GridView ID="grdrequestItem" ShowHeader="true" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                        EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="" Visible="true">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="allchk_reqitem" runat="server" Width="10px" onclick="CheckAllgrdreqItem(this)"></asp:CheckBox>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chk_ReqItem" AutoPostBack="true" runat="server" onclick="CheckBoxCheckReq_item(this);" Checked="false" Width="5px" OnCheckedChanged="chk_ReqItem_CheckedChanged_Click" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Item">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="col_ITRI_ITM_CD" runat="server" Text='<%# Bind("ITRI_ITM_CD") %>' Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Description">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="col_Mi_longdesc" runat="server" Text='<%# Bind("Mi_longdesc") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Model">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="col_mi_model" runat="server" Text='<%# Bind("Mst_item_model") %>' Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="RER #" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="col_itri_res_no" runat="server" Text='<%# Bind("itri_res_no") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Request Qty">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="col_itri_qty" runat="server" Text='<%# Bind("itri_qty","{0:n}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Balance Qty">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="col_itri_bqty" runat="server" Text='<%# Bind("itri_bqty","{0:n}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Line No" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="col_Ritri_line_no" runat="server" Text='<%# Bind("itri_line_no") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Job Line No" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="col_itri_job_line" runat="server" Text='<%# Bind("Itri_job_line") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupBLItems" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="pnlpopupBlItem" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpopupBlItem">
        <div runat="server" id="Div5" class="panel panel-default height400 width700">
            <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="btnClose_Click">
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
                            <div class="col-sm-12 height5">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-10">
                            </div>
                            <div class="col-sm-2">
                                <asp:Button ID="btnBlItem" runat="server" Text="Add Item" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnBlItem_Click" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-10">
                            </div>
                            <div class="col-sm-2">
                                <asp:Button ID="btnBlItemUpdate" runat="server" Text="Update" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnBlItemUpdate_Click" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="panel-body panelscollbar height300">
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy5" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="grdBlItem" CausesValidation="false" AutoGenerateColumns="false" runat="server" GridLines="None" CssClass="table table-hover table-striped">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" Visible="true">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="allchk_Blitem" onclick="CheckAllgrdBlItem(this)" runat="server" Width="10px"></asp:CheckBox>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chk_BlItem" runat="server" Checked="false" Width="5px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_ibi_itm_cd" runat="server" Text='<%# Bind("ibi_itm_cd") %>' Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="BL quantity" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_Ibi_bl_qty_temp" Style="text-align: right" runat="server" Text='<%# Bind("Ibi_bl_qty_temp","{0:n}") %>' Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="BL quantity" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_ibi_qty" Style="text-align: right" runat="server" Text='<%# Bind("ibi_qty","{0:n}") %>' Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="line" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_ibi_line" Style="text-align: right" runat="server" Text='<%# Bind("ibi_line") %>' Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remaining quantity">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="col_invRevQty" Style="text-align: right"  onkeypress="return isNumberKey(event)" CssClass="form-control" runat="server" Text='<%# Bind("ibi_bal_qty","{0:n}") %>' Width="80px" ></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Remarks">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="col_invRemarks" Style="text-align: right"   CssClass="form-control" runat="server" Text='<%# Bind("ibi_remarks") %>' Width="80px" ></asp:TextBox>
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
    </asp:Panel>

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
        <div runat="server" id="test" class="panel panel-default height400 width950">
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
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy4" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdResult" CausesValidation="false" runat="server" GridLines="None" CssClass="table table-hover table-striped" 
                                            PageSize="10" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResult_SelectedIndexChanged" OnPageIndexChanging="grdResult_PageIndexChanging">
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

    <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserPopoupRequest" runat="server" Enabled="True" TargetControlID="Button2"
                PopupControlID="pnlpopupRequest" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpopupRequest" DefaultButton="lbtnSearchRequest">
        <div runat="server" id="testRequest" class="panel panel-default height400 width850">
            <asp:Label ID="lblvalueRequest" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="btnCloseRequest" runat="server" OnClick="btnClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <%--<span>Commen Search</span>--%>
                    <div class="col-sm-11">
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12" id="searchRequest" runat="server">
                        <div class="row">
                            <div class="col-sm-12">
                            </div>
                        </div>
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
                                    <asp:TextBox runat="server" TabIndex="200" Enabled="true" ID="txtFDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-1 paddingLeft0">
                                    <asp:LinkButton ID="lbtnFDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFDate"
                                        PopupButtonID="lbtnFDate" Format="dd/MMM/yyyy">
                                    </asp:CalendarExtender>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-2 labelText1">
                                    To
                                </div>
                                <div class="col-sm-6 paddingRight5 paddingLeft0">
                                    <asp:TextBox runat="server" Enabled="true" TabIndex="201" ID="txtTDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-1 paddingLeft0">
                                    <asp:LinkButton ID="lbtnTDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                    <asp:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txtTDate"
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
                        <div class="col-sm-1">
                        </div>
                        <div class="col-sm-6">
                            <div class="row">
                                <div class="col-sm-12" id="Div9" runat="server">
                                    <div class="row">
                                    <div class="col-sm-3 labelText1">
                                        Search by key
                                    </div>
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <div class="col-sm-7 paddingRight5">
                                                <asp:DropDownList ID="ddlSearchbykeyRequest" runat="server" class="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    </div>
                                    <div class="row">
                                    <div class="col-sm-3 labelText1">
                                        Search by word
                                    </div>
                                    <div class="col-sm-7 paddingRight5">
                                        <asp:TextBox ID="txtSearchbywordRequest" CausesValidation="false" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbywordRequest_TextChanged"></asp:TextBox>
                                    </div>
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy6" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="lbtnSearchRequest" runat="server" OnClick="lbtnSearchRequest_Click">
                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                </asp:LinkButton>
                                            </div>
                                        </ContentTemplate>

                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="txtSearchbywordRequest" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    </div>
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
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy8" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdResultRequest" CausesValidation="false" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResultRequest_SelectedIndexChanged" OnPageIndexChanging="grdResultRequest_PageIndexChanging">
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

     <asp:UpdatePanel ID="UpdatePanel11" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button4" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mdlGRN" runat="server" Enabled="True" TargetControlID="Button4"
                PopupControlID="pnlpopupGRN" CancelControlID="LinkButton2" PopupDragHandleControlID="Div7" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpopupGRN" DefaultButton="lbtnSearchRequest">
        <div runat="server" id="Div7" class="panel panel-default height400 width850">
            <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="LinkButton2" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <%--<span>Commen Search</span>--%>
                    <div class="col-sm-11">
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12" id="Div8" runat="server">
                        <div class="row">
                            <div class="col-sm-12">
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
                            <div class="col-sm-12 height5">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy13" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdGRN" CausesValidation="false" runat="server" GridLines="None" CssClass="table table-hover table-striped"  OnSelectedIndexChanged="grdGRN_SelectedIndexChanged">
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
