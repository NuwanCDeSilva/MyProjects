<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="StockLedger.aspx.cs" Inherits="FastForward.SCMWeb.View.Enquiries.Inventory.StockLedger" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>
        function SaveConfirm() {
            var selectedvalue = confirm("Do you want to save data?");
            if (selectedvalue) {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "No";
            }
        };
        function ConfView() {
            return true;
            //var selectedvalueOrd = confirm("Do you want to view ?");
            //if (selectedvalueOrd) {
            //    return true;
            //} else {
            //    return false;
            //}
        };
    </script>
    <script type="text/javascript">
        function closeDialog() {
            $(this).showStickySuccessToast("close");
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
        function showStickyNoticeToast(value) {
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
            $().toastmessage('showToast', {
                text: value,
                sticky: true,
                position: 'top-left',
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
    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('show');
        }
    </script>

    <script type="text/javascript">

        function NewConfirm() {
            var Ok = confirm('Do you want to create new user?');
            if (Ok == true) {
                document.getElementById('txtID').focus();
                $('#txtID').focus();
            }
            else
                window.location.replace('User_Creation.aspx');
            //return false;
        }

        function ClearConfirmExtented() {
            var selectedvalue = confirm("Do you want to Clear data?");
            if (selectedvalue) {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "No";
            }
        };
    </script>

    <script type="text/javascript">

        function Confirm() {
            var selectedvalue = confirm("Do you want to save ?");
            if (selectedvalue) {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "No";
            }
        };

    </script>

    <script type="text/javascript">
        function UpdateConfirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "User account disable";
            if (confirm("User update with DISABLE status!\nPlease confirm?\n\nNote-\nAfter update the user account as DISABLE, your never activate again.")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>

    <script type="text/javascript">

        function DeleteConfirm() {
            var selectedvalue = confirm("Do you want to delete data?");
            if (selectedvalue) {
                document.getElementById('<%=txtDeleteconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtDeleteconformmessageValue.ClientID %>').value = "No";
            }
        };
        function ClearConfirm() {
            var selectedvalue = confirm("Do you want to Clear data?");
            if (selectedvalue) {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "No";
            }
        };
        function newUserConfirm() {
            var selectedvalue = confirm("Do you want to create new employee?");
            if (selectedvalue) {
                document.getElementById('<%=txtDeleteconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtDeleteconformmessageValue.ClientID %>').value = "No";
            }
        };


        function CheckAllgrdReqDW(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdDWlocations.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }

        function CheckBoxCheckDW(rb) {
            debugger;
            var gv = document.getElementById("<%=grdDWlocations.ClientID%>");
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

        function CheckAllgrdReqSW(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdSWlocations.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }

        function CheckBoxCheckSW(rb) {
            debugger;
            var gv = document.getElementById("<%=grdSWlocations.ClientID%>");
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

<%--        function CheckSublocationBoxCheckDW(oCheckbox) {

            if (document.getElementById("<%=chkSublocationAll_DW.ClientID %>" == oCheckbox.checked) {
                $("#btnSubLocationsDW").show();
            }
            else {
                $("#btnSubLocationsDW").hide();
            }
        }

        function CheckSublocationBoxCheckSW(oCheckbox) {

            if (document.getElementById("<%=chkSublocationAll_SW.ClientID %>" == oCheckbox.checked) {
                $("#btnSubLocationsSW").show();
            }
            else {
                $("#btnSubLocationsSW").hide();
            }
        }--%>

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
    <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="pnlBasePanel">

        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait1" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait1" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>

    </asp:UpdateProgress>

    <asp:HiddenField ID="txtClearlconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtSavelconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtUpdateconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtApprovalconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtCancelconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtDeleteconformmessageValue" runat="server" />
    <asp:HiddenField ID="hdfTabIndex" runat="server" />

    <div class="panel panel-default marginLeftRight5">
        <div class="panel-body">
            <asp:UpdatePanel ID="pnlBasePanel" runat="server">
                <ContentTemplate>
                    <asp:Panel runat="server" DefaultButton="lbtnViewLedger">

                   
                    <div class="row">
                        <div class="col-sm-8  buttonrow">
                        </div>
                        <div class="col-sm-4 buttonRow">
                            <div class="col-sm-3">
                            </div>
                            <div class="col-sm-3 paddingRight0">
                            </div>
                            <div class="col-sm-3 paddingRight0">
                                <asp:LinkButton ID="lbtnViewLedger" CausesValidation="false" runat="server" OnClientClick="return ConfView();" CssClass="floatRight" OnClick="lbtnViewLedger_Click">
                                        <span class="glyphicon glyphicon-search fontsize20" aria-hidden="true"></span>View
                                </asp:LinkButton>
                            </div>
                            <div class="col-sm-3">
                                 <asp:LinkButton ID="lbtnClearLdgr" runat="server" CssClass="floatRight" OnClick="lbtnClearStkLedgr_Click" OnClientClick="ClearConfirmExtented()">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-default marginLeftRight5">
                                <div class="panel-body">
                                    <div class="bs-example">
                                        <ul class="nav nav-tabs" id="myTab">
                                            <li class="active"><a href="#DocumentWise" data-toggle="tab">Item Details</a></li>
                                            <li onclick="document.getElementById('<%= lbtnSerWise.ClientID %>').click();"><a href="#SerialWise" data-toggle="tab">Serial Wise</a></li>
                                        </ul>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>

                                    <div class="tab-content">

                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                        <asp:HiddenField ID="txtSaveconformmessageValue" runat="server" />
                                        <asp:HiddenField ID="HiddenField2" runat="server" />
                                        <asp:HiddenField ID="txtAlertValue" runat="server" />
                                        <%-- Document wise tab --%>
                                        <div class="tab-pane active" id="DocumentWise">
                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                                            <asp:UpdatePanel ID="pnlTabDocWsePanel" runat="server">
                                                <ContentTemplate>
                                                    <asp:UpdatePanel ID="pnlTopDocStkLedgrPanel" runat="server">
                                                        <ContentTemplate>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="panel panel-default">
                                                                        <div class="panel-heading paddingtopbottom0">
                                                                            Item Details
                                                                        </div>
                                                                        <div class="panel-body">
                                                                            <div class="row">

                                                                                <div class="col-sm-4">
                                                                                    <div class="row">
                                                                                        <div class="col-sm-2 labelText1">
                                                                                            Item Code
                                                                                        </div>
                                                                                        <div class="col-sm-3 padding0">
                                                                                            <asp:TextBox runat="server" ID="txtDWItemCode" OnTextChanged="txtDWItemCode_TextChanged" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                                        </div>
                                                                                        <div class="col-sm-1 paddingLeft3">
                                                                                            <asp:LinkButton ID="lbtnDWItem" runat="server" CausesValidation="false" OnClick="lbtnDWItem_Click">
                                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                        <div class="col-sm-5 padding0 labelText1">
                                                                                            <strong>
                                                                                            <asp:Label runat="server" ID="lblDWItemName"  AutoPostBack="true" CausesValidation="false" ReadOnly="true"></asp:Label>
                                                                                            </strong>
                                                                                        </div>

                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-sm-2 labelText1">
                                                                                    </div>
                                                                                        <div class="col-sm-5 labelText1  padding0">
                                                                                           <strong> <asp:Label ID="lblBrand1"  runat="server" /></strong>
                                                                                        </div>
                                                                                        <div class="col-sm-5 labelText1  padding0">
                                                                                            <strong><asp:Label ID="lblModel1"  runat="server" /></strong>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-sm-2 labelText1">
                                                                                            Company
                                                                                        </div>
                                                                                        <div class="col-sm-2 paddingRight0 paddingLeft0">
                                                                                            <asp:TextBox runat="server" ID="txtDWCompany" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                                        </div>
                                                                                        <div class="col-sm-2 paddingLeft3">
                                                                                            <asp:LinkButton ID="lbtnDWCompany" runat="server" CausesValidation="false" OnClick="lbtnDWCompany_Click">
                                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                        <div class="col-sm-6 paddingRight0 paddingLeft5 labelText1">
                                                                                            <asp:Label runat="server" ID="lblDWCompanyName" AutoPostBack="true" ReadOnly="true"></asp:Label>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-4">
                                                                                    <div class="row">

                                                                                        <div class="col-sm-3 labelText1">
                                                                                            Location
                                                                                        </div>
                                                                                        <div class="col-sm-2 padding0">
                                                                                            <asp:TextBox runat="server" Style="text-transform: uppercase" ID="txtDWLocation" AutoPostBack="true" OnTextChanged="txtDWLocation_TextChanged" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                                        </div>
                                                                                        <div class="col-sm-1 paddingLeft3">
                                                                                            <asp:LinkButton ID="lbtnDWLocation" runat="server" CausesValidation="false" OnClick="lbtnDWLocation_Click">
                                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                        <div class="col-sm-6 padding0 labelText1">
                                                                                            <asp:Label runat="server" ID="lblDWLocationName" AutoPostBack="true" CausesValidation="false" ReadOnly="true"></asp:Label>
                                                                                        </div>

                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-sm-3 labelText1 text-left">
                                                                                        </div>
                                                                                        </div>
                                                                                    <div class="row">
                                                                                        <div class="col-sm-3 labelText1 text-left">
                                                                                            Sub Location
                                                                                        </div>
                                                                                        <div class="col-sm-2 labelText1 paddingLeft0">
                                                                                            <div class="col-sm-2  paddingLeft0">
                                                                                                <asp:CheckBox ID="chkSublocationAll_DW" runat="server" Check="true" AutoPostBack="true" OnCheckedChanged="CheckSublocationBoxCheckDW_Click" />
                                                                                            </div>
                                                                                            <div class="col-sm-8  paddingLeft3">
                                                                                                All
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-6 padding0 labelText1">
                                                                                            <asp:LinkButton ID="btnSubLocationsDW" runat="server" CausesValidation="false" OnClick="btnDWsublocation_Click">
                                                                                                    Select Sub Location
                                                                                            </asp:LinkButton>
                                                                                            <%--<asp:Button ID="btnSubLocationsDW" runat="server" Text="Select Sublocation" CausesValidation="false" class="btn btn-success btn-xs" Style="padding: 1px; font-size: 10px" OnClick="btnDWsublocation_Click" />--%>
                                                                                        </div>
                                                                                    </div>

                                                                                </div>
                                                                                <div class="col-sm-2">
                                                                                    <div class="row">
                                                                                        <div class="col-sm-2 labelText1 width70">
                                                                                            Doc Type
                                                                                        </div>
                                                                                        <div class="col-sm-4 paddingRight0 paddingLeft0">
                                                                                            <asp:TextBox runat="server" ID="txtDWDocType" OnTextChanged="txtDWDocType_TextChanged" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                                        </div>
                                                                                        <div class="col-sm-2 paddingLeft3">
                                                                                            <asp:LinkButton ID="lbtnDocType" runat="server" CausesValidation="false" OnClick="lbtnDocType_Click">
                                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-sm-2 width25 labelText1">
                                                                                            <asp:CheckBox ID="chkStatus" runat="server" />
                                                                                        </div>
                                                                                        <div class="col-sm-4 labelText1">
                                                                                            With Status
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-2">
                                                                                    <asp:UpdatePanel runat="server">
                                                                                        <ContentTemplate>
                                                                                            <div class="row">
                                                                                                <div class="col-sm-2 labelText1 width50">
                                                                                                    From
                                                                                                </div>
                                                                                                <asp:UpdatePanel runat="server">
                                                                                                        <ContentTemplate>
                                                                                                <div class="col-sm-5 padding0">
                                                                                                            <asp:TextBox runat="server" ReadOnly="true" ID="txtDWfrom" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                                                </div>
                                                                                                <div class="col-sm-1 paddingLeft3">
                                                                                                    <asp:LinkButton ID="lbtnDWfrom" runat="server" CausesValidation="false">
                                                                                             <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                                                    </asp:LinkButton>
                                                                                                    <asp:CalendarExtender ID="cldDWfrom" runat="server" TargetControlID="txtDWfrom"
                                                                                                        PopupButtonID="lbtnDWfrom" Format="dd/MMM/yyyy">
                                                                                                    </asp:CalendarExtender>
                                                                                                </div>
                                                                                                 </ContentTemplate>
                                                                                                    </asp:UpdatePanel>
                                                                                            </div>
                                                                                            <div class="row">
                                                                                                <div class="col-sm-2 labelText1 width50">
                                                                                                    To
                                                                                                </div>
                                                                                                <div class="col-sm-5 padding0">
                                                                                                    <asp:TextBox runat="server" ReadOnly="true" ID="txtDWto" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                                                </div>
                                                                                                <div class="col-sm-1 paddingLeft3">
                                                                                                    <asp:LinkButton ID="lbtnDWto" runat="server" CausesValidation="false">
                                                                                             <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                                                    </asp:LinkButton>
                                                                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDWto"
                                                                                                        PopupButtonID="lbtnDWto" Format="dd/MMM/yyyy">
                                                                                                    </asp:CalendarExtender>
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
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <asp:UpdatePanel ID="pnlBottomDocStkLedgrPanel" runat="server">
                                                        <ContentTemplate>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="panel panel-default marginLeftRight5">
                                                                        <div class="panel-heading paddingtopbottom0">
                                                                            Item Details
                                                                        </div>
                                                                        <div class="panel-body">
                                                                            <div class="col-sm-12">
                                                                                <div class="panel-body panelscollbar height300">
                                                                                    <asp:GridView ID="grdDWSerialDetails" OnSorting="grdDWSerialDetails_Sorting" AllowSorting="true" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                                        EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="Location" SortExpression="LOCATION">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lbl_location" runat="server" Text='<%# Bind("LOCATION") %>' Width="100%"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle width="60px"/>
                                                                                                <HeaderStyle width="60px"/>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Other Loc." SortExpression="OTHER_LOC">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lbl_otherloc" runat="server" Text='<%# Bind("OTHER_LOC") %>' Width="100%"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle width="60px"/>
                                                                                                <HeaderStyle width="60px"/>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Doc Date" SortExpression="DOC_DATE">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lbl_docdate" runat="server" Text='<%# Bind("DOC_DATE","{0:d}") %>' Width="100%"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle width="60px"/>
                                                                                                <HeaderStyle width="60px"/>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Doc No" SortExpression="DOC_NO">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lbl_docno" runat="server" Text='<%# Bind("DOC_NO") %>' Width="100%"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle width="170px"/>
                                                                                                <HeaderStyle width="170px"/>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Other Doc" SortExpression="OTHER_DOC">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lbl_otherdoc" runat="server" Text='<%# Bind("OTHER_DOC") %>' Width="100%"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle width="150px"/>
                                                                                                <HeaderStyle width="150px"/>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Man Ref" SortExpression="MAN_REF">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lbl_manref" runat="server" Text='<%# Bind("MAN_REF") %>' Width="100%"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle width="150px"/>
                                                                                                <HeaderStyle width="150px"/>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Doc Type" SortExpression="DOC_TYPE">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lbl_doctype" runat="server" Text='<%# Bind("DOC_TYPE") %>' Width="100%"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle width="80px"/>
                                                                                                <HeaderStyle width="80px"/>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Status" SortExpression="STATUS"> 
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lbl_status" runat="server" Text='<%# Bind("STATUS") %>' Width="100%"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle width="50px"/>
                                                                                                <HeaderStyle width="50px"/>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Ins" SortExpression="IN_COU">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label Visible='<%# hdfValTp.Value=="0" %>' ID="lbl_ins1" runat="server" Text='<%# Bind("IN_COU","{0:N0}") %>' Width="100%"></asp:Label>
                                                                                                    <asp:Label Visible='<%# hdfValTp.Value=="1" %>' ID="lbl_ins2" runat="server" Text='<%# Bind("IN_COU","{0:N3}") %>' Width="100%"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                  <ItemStyle CssClass="gridHeaderAlignRight" width="60px"/>
                                                                                                <HeaderStyle CssClass="gridHeaderAlignRight" width="60px"/>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Outs" SortExpression="OUT_COU">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label Visible='<%# hdfValTp.Value=="0" %>' ID="lbl_outs1" runat="server" Text='<%# Bind("OUT_COU","{0:N0}") %>' Width="100%"></asp:Label>
                                                                                                    <asp:Label Visible='<%# hdfValTp.Value=="1" %>' ID="lbl_outs2" runat="server" Text='<%# Bind("OUT_COU","{0:N3}") %>' Width="100%"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                  <ItemStyle CssClass="gridHeaderAlignRight" width="60px"/>
                                                                                                <HeaderStyle CssClass="gridHeaderAlignRight" width="60px"/>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Balance" SortExpression="BALANCE">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label Visible='<%# hdfValTp.Value=="0" %>' ID="lbl_balance1" runat="server"  Text='<%# Bind("BALANCE","{0:N0}") %>' Width="100%"></asp:Label>
                                                                                                    <asp:Label Visible='<%# hdfValTp.Value=="1" %>' ID="lbl_balance2" runat="server"  Text='<%# Bind("BALANCE","{0:N3}") %>' Width="100%"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle CssClass="gridHeaderAlignRight" width="60px"/>
                                                                                                <HeaderStyle CssClass="gridHeaderAlignRight" width="60px"/>
                                                                                            </asp:TemplateField>
                                                                                             <asp:TemplateField HeaderText="">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label  runat="server" Text='' Width="100%"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle CssClass="gridHeaderAlignRight" width="20px"/>
                                                                                                <HeaderStyle CssClass="gridHeaderAlignRight" width="20px"/>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                    <asp:HiddenField ID="hdfValTp" Value="0" runat="server" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                             <asp:Panel runat="server" ID="pnlSum">
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="panel panel-default marginLeftRight5">
                                                                        <div class="panel-body">
                                                                            <div class="col-sm-9">
                                                                            </div>
                                                                           
                                                                            <div class="col-sm-3">
                                                                                <div class="col-sm-1 labelText1 paddingLeft15 paddingRight0 width70">
                                                                                    In Total
                                                                                </div>
                                                                                <div class="col-sm-2 paddingRight0 paddingLeft0">
                                                                                    <asp:TextBox runat="server" ID="txtinTotal" Enabled="false" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-sm-1 labelText1 paddingLeft15 paddingRight0 width70">
                                                                                    Out Total
                                                                                </div>
                                                                                <div class="col-sm-2 paddingRight0 paddingLeft0">
                                                                                    <asp:TextBox runat="server" ID="txtoutTotal" Enabled="false" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            </asp:Panel>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <%-- serial wise tab --%>
                                        <div class="tab-pane " id="SerialWise">
                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                                            <asp:UpdatePanel ID="pnlTabSrlWsePanel" runat="server">
                                                <ContentTemplate>
                                                    <asp:LinkButton ID="lbtnSerWise" CausesValidation="false" runat="server" OnClick="lbtnSerPrice_Click">
                                                                       
                                                    </asp:LinkButton>
                                                    <asp:UpdatePanel ID="pnlTopEmpLocationUPPanel" runat="server">
                                                        <ContentTemplate>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="panel panel-default">
                                                                        <div class="panel-heading paddingtopbottom0">
                                                                            Item Details
                                                                        </div>
                                                                        <div class="panel-body">
                                                                            <div class="row">

                                                                                <div class="col-sm-4">

                                                                                    <div class="row">
                                                                                        <div class="col-sm-2 labelText1">
                                                                                            Item Code
                                                                                        </div>
                                                                                        <div class="col-sm-3 padding0">
                                                                                            <asp:TextBox runat="server" ID="txtSWItemCode" OnTextChanged="txtSWItemCode_TextChanged" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                                        </div>
                                                                                        <div class="col-sm-1 paddingLeft3">
                                                                                            <asp:LinkButton ID="lbtnSWItem" runat="server" CausesValidation="false" OnClick="lbtnSWItem_Click">
                                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                        <div class="col-sm-5 padding0 labelText1">
                                                                                            <strong><asp:Label runat="server" ID="lblSWItemName" AutoPostBack="true" CausesValidation="false" ReadOnly="true"></asp:Label></strong>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-sm-2 labelText1">
                                                                                        </div>
                                                                                        <div class="col-sm-5 labelText1 padding0">
                                                                                           <strong> <asp:Label ID="lblBrand2"  runat="server" /></strong>
                                                                                        </div>
                                                                                        <div class="col-sm-5 labelText1 padding0">
                                                                                            <strong><asp:Label ID="lblModel2"  runat="server" /></strong>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-sm-2 labelText1">
                                                                                            Company
                                                                                        </div>
                                                                                        <div class="col-sm-2 paddingRight0 paddingLeft0">
                                                                                            <asp:TextBox runat="server" ID="txtSWCompany" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                                        </div>
                                                                                        <div class="col-sm-2 paddingLeft3">
                                                                                            <asp:LinkButton ID="lbtnSWCompany" runat="server" CausesValidation="false" OnClick="lbtnSWCompany_Click">
                                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                        <div class="col-sm-6 paddingRight0 paddingLeft5 labelText1">
                                                                                            <asp:Label runat="server" ID="lblSWCompanyName" AutoPostBack="true" CausesValidation="false" ReadOnly="true"></asp:Label>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-4">
                                                                                    <div class="row">
                                                                                        <div class="col-sm-3 labelText1">
                                                                                            Location
                                                                                        </div>
                                                                                        <div class="col-sm-2 paddingRight0 paddingLeft0">
                                                                                            <asp:TextBox runat="server" Style="text-transform: uppercase" ID="txtSWLocation" AutoPostBack="true" OnTextChanged="txtSWLocation_TextChanged" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                                        </div>
                                                                                        <div class="col-sm-1 paddingLeft3">
                                                                                            <asp:LinkButton ID="lbtnSWLocation" runat="server" CausesValidation="false" OnClick="lbtnSWLocation_Click">
                                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                        <div class="col-sm-6 paddingRight0 paddingLeft5 labelText1">
                                                                                            <asp:Label runat="server" ID="lblSWLocationName" AutoPostBack="true" CausesValidation="false" ReadOnly="true"></asp:Label>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-sm-3 labelText1">
                                                                                    </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-sm-3 labelText1">
                                                                                            Sub Location
                                                                                        </div>
                                                                                        <div class="col-sm-2 labelText1 paddingLeft0">
                                                                                            <div class="col-sm-2 paddingLeft0">
                                                                                                <asp:CheckBox ID="chkSublocationAll_SW" runat="server" Check="true" AutoPostBack="true" OnCheckedChanged="CheckSublocationBoxCheckSW_Click" />
                                                                                            </div>
                                                                                            <div class="col-sm-8  paddingLeft3">
                                                                                                All
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-sm-6 labelText1 ">
                                                                                    <%--<asp:Button ID="btnSubLocationsSW" runat="server" Text="Select Sublocation" CausesValidation="false" class="btn btn-success btn-xs" Style="padding: 1px; font-size: 10px" OnClick="btnSWsublocation_Click" />--%>
                                                                                            <asp:LinkButton ID="btnSubLocationsSW" runat="server" OnClick="btnSWsublocation_Click">
                                                                                               Select Sub Location
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-2">
                                                                                    <div class="row">
                                                                                        <div class="col-sm-2 labelText1 width70">
                                                                                            Serial
                                                                                        </div>
                                                                                        <div class="col-sm-7 paddingRight0 paddingLeft0">
                                                                                            <asp:TextBox runat="server" ID="txtSWserial" OnTextChanged="txtSWserial_TextChanged" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                                        </div>
                                                                                        <div class="col-sm-1 paddingLeft3">
                                                                                            <asp:LinkButton ID="lbtnSerial" runat="server" CausesValidation="false" OnClick="lbtnSerial_Click">
                                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-sm-2">
                                                                                    <div class="row">
                                                                                        <div class="col-sm-2 labelText1 width50">
                                                                                            From
                                                                                        </div>
                                                                                        <div class="col-sm-5 padding0">
                                                                                            
                                                                                                    <asp:TextBox runat="server" ReadOnly="true" ID="txtSWfrom" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                                                
                                                                                        </div>
                                                                                        <div class="col-sm-1 paddingLeft3">
                                                                                            <asp:LinkButton ID="btnSerFrom" runat="server" CausesValidation="false">
                                                                                             <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtSWfrom"
                                                                                                PopupButtonID="btnSerFrom" Format="dd/MMM/yyyy">
                                                                                            </asp:CalendarExtender>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <div class="col-sm-2 labelText1 width50">
                                                                                            To
                                                                                        </div>
                                                                                        <div class="col-sm-5 padding0">
                                                                                            <asp:TextBox runat="server" ReadOnly="true" ID="txtSWto" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                                        </div>
                                                                                        <div class="col-sm-1 paddingLeft3">
                                                                                            <asp:LinkButton ID="lbtnSerTo" runat="server" CausesValidation="false">
                                                                                             <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtSWto"
                                                                                                PopupButtonID="lbtnSerTo" Format="dd/MMM/yyyy">
                                                                                            </asp:CalendarExtender>
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
                                                    <asp:UpdatePanel ID="pnlBottomEmpLocationDOWNPanel" runat="server">
                                                        <ContentTemplate>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="panel panel-default marginLeftRight5">
                                                                        <div class="panel-heading paddingtopbottom0">
                                                                            Serial Details
                                                                        </div>
                                                                        <div class="panel-body padding0 margin-top1">
                                                                            <div class="col-sm-12 padding0">
                                                                                <div class="panel-body panelscollbar padding0" style="height:320px;">
                                                                                    <asp:GridView ID="grdSWSerialDetails" AllowSorting="true" CssClass="table table-hover table-striped" runat="server"  GridLines="None" OnSorting="grdSWSerialDetails_Sorting"
                                                                                        ShowHeaderWhenEmpty="True" EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="Date" SortExpression="IN_DATE">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lbl_SWDate" runat="server" Text='<%# Bind("IN_DATE","{0:d}") %>' Width="60px"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Status" SortExpression="STATUS">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lbl_SWStatus" runat="server" Text='<%# Bind("STATUS") %>' Width="60px"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Type" SortExpression="IN_TYPE">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lbl_SWType" runat="server" Text='<%# Bind("IN_TYPE") %>' Width="60px"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Reference" SortExpression="REFERENCE">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lbl_SWReference" runat="server" Text='<%# Bind("REFERENCE") %>' Width="200px"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Serial 01" SortExpression="SERIAL_1">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lbl_SWserial01" runat="server" Text='<%# Bind("SERIAL_1") %>' Width="100px"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Serial 02" SortExpression="SERIAL_2">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lbl_SWserial02" runat="server" Text='<%# Bind("SERIAL_2") %>' Width="100px"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Issue Date" SortExpression="ISSUE_DATE">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lbl_SWIssDate" runat="server" Text='<%# Bind("ISSUE_DATE","{0:dd/MMM/yyyy}") %>' Width="60px"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Issue Type" SortExpression="ISSUE_TYPE">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lbl_SWIssType" runat="server" Text='<%# Bind("ISSUE_TYPE") %>' Width="60px"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Issue Ref" SortExpression="ISSUE_REFERENCE">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lbl_SWIssRef" runat="server" Text='<%# Bind("ISSUE_REFERENCE") %>' Width="150px"></asp:Label>
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
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                         </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <%--common search--%>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserPopoup" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="pnlSearchPanel" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

            <%-- Style="display: none"--%>
            <asp:Panel runat="server" ID="pnlSearchPanel" DefaultButton="lbtnSearch">
                <div runat="server" id="test" class="panel panel-default height350 width700">
                    <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="btnClose" OnClick="btnClose_Click1" runat="server">
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
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtSearchbyword" CausesValidation="false" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbyword_TextChanged"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
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
                                                <asp:GridView ID="grdResult" CausesValidation="false" runat="server" GridLines="None" 
                                                    CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager"
                                                     AllowPaging="True" OnSelectedIndexChanged="grdResult_SelectedIndexChanged" OnPageIndexChanging="grdResult_PageIndexChanging">
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
        </ContentTemplate>
    </asp:UpdatePanel>

    <%--document wise locations--%>

    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
        <ContentTemplate>

            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupDWSubLocations" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="pnlpopupDWSubLocations" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

            <asp:Panel runat="server" ID="pnlpopupDWSubLocations" DefaultButton="lbtnSearch">
                <div runat="server" id="Div5" class="panel panel-default height350 width700">
                    <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading height30">
                            <div class="col-sm-11">
                                <strong><b>Sub Location</b></strong>
                            </div>
                            <div class="col-sm-1">
                                <asp:LinkButton ID="btnPopupDWSubLocationsClose" runat="server" OnClick="btnPopupDWSubLocationsClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12" id="Div6" runat="server">
                                <div class="row">
                                    <div class="col-sm-12">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="panel-body panelscollbar height300">
                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy5" runat="server"></asp:ScriptManagerProxy>
                                            <%--  <asp:UpdatePanel ID="UpdatePanel4" runat="server">--%>
                                            <%-- <ContentTemplate>--%>
                                            <asp:GridView ID="grdDWlocations" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" Visible="true">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="allDWlocchk" runat="server" Checked="false" Width="10px" onclick="CheckAllgrdReqDW(this)"></asp:CheckBox>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chk_DWlocReq" runat="server" Checked="false" Width="5px" OnCheckedChanged="chk_Req_CheckedChangedDW_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_DWCode" runat="server" Text='<%# Bind("Ml_loc_cd") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_DWDescription" runat="server" Text='<%# Bind("Ml_loc_desc") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <%-- </ContentTemplate>--%>
                                            <%--</asp:UpdatePanel>--%>
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


    <%--serial wise locations--%>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupSWSubLocations" runat="server" Enabled="True" TargetControlID="Button2"
                PopupControlID="pnlpopupSWSubLocations" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

            <asp:Panel runat="server" ID="pnlpopupSWSubLocations" DefaultButton="lbtnSearch">
                <div runat="server" id="Div1" class="panel panel-default height350 width700">
                    <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading height30">
                            
                            <%--<span>Commen Search</span>--%>
                            <div class="col-sm-11">
                                <strong><b>Sub Location</b></strong>
                            </div>
                            <div class="col-sm-1">
                                <asp:LinkButton ID="btnPopupSWSubLocationsClose" runat="server" OnClick="btnPopupSWSubLocationsClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12" id="Div2" runat="server">
                                <div class="row">
                                    <div class="col-sm-12">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="panel-body panelscollbar height300">
                                            <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
                                            <asp:GridView ID="grdSWlocations" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" Visible="true">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="allSWlocchk" Checked="true" runat="server" Width="10px" onclick="CheckAllgrdReqSW(this)"></asp:CheckBox>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chk_SWlocReq" runat="server" Checked="false" Width="5px" OnCheckedChanged="chk_Req_CheckedChangedSW_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_SWCode" runat="server" Text='<%# Bind("Ml_loc_cd") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_SWDescription" runat="server" Text='<%# Bind("Ml_loc_desc") %>'></asp:Label>
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

        </ContentTemplate>
    </asp:UpdatePanel>



    <script type="text/javascript">
        Sys.Application.add_load(fun);
        function fun() {
            $('#myTab a').click(function (e) {
                e.preventDefault();
                $(this).tab('show');
                //  alert($(this).attr('href'));
                document.getElementById('<%=hdfTabIndex.ClientID %>').value = $(this).attr('href');
            });

            $(document).ready(function () {
                var tab = document.getElementById('<%= hdfTabIndex.ClientID%>').value;
                // alert(tab);
                $('#myTab a[href="' + tab + '"]').tab('show');
            });
        }

    </script>
</asp:Content>
