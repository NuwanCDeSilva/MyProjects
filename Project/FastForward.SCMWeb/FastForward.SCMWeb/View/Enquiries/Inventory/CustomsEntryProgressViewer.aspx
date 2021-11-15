<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="CustomsEntryProgressViewer.aspx.cs" Inherits="FastForward.SCMWeb.View.Enquiries.Inventory.Customs_Entry_Progress_Viewer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="<%=Request.ApplicationPath%>Css/jquery-ui.theme.min.css" rel="stylesheet" />
    <link href="<%=Request.ApplicationPath%>Css/jquery-ui.min.css" rel="stylesheet" />

    <script type="text/javascript">
        //function pageLoad(sender, args) {

        //    jQuery(".txtExpectedFrom").datetimepicker({ dateFormat: "d/M/yy", timeFormat: "hh:mm tt" });
        //    jQuery(".txtExpectedTo").datetimepicker({ dateFormat: "d/M/yy", timeFormat: "hh:mm tt" });
        //}
        function closeDialog() {
            $(this).showStickySuccessToast("close");
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
    <style>
        .tblTest th {
            border-left: 2px solid;
            border-left-color: darkgray;
        }

            .tblTest th:nth-child(1) {
                border-left: none;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel1">

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

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-sm-8  buttonrow">
                </div>
                <div class="col-sm-4  buttonRow">

                    <div class="col-sm-6">
                    </div>
                    <div class="col-sm-3 paddingRight0">
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
            <strong>Customs Entry Progress Viewer</strong>
        </div>
        <div class="panel-body padding0">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="col-sm-2 padding0">
                                <div class="panel panel-default">
                                    <div class="panel panel-body padding0">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-sm-3 labelText1 padding0">
                                                    Customer
                                                </div>
                                                <div class="col-sm-8 padding0 ">
                                                    <asp:TextBox runat="server" ID="txtCustomer" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtCustomer_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft3 paddingRight0">
                                                    <asp:LinkButton ID="lbtnCustomer" runat="server" CausesValidation="false" OnClick="lbtnCustomer_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-sm-3 labelText1 padding0">
                                                    Entry #
                                                </div>
                                                <div class="col-sm-8 padding0">
                                                    <asp:TextBox runat="server" ID="txtEntryno" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtEntryno_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft3 paddingRight0">
                                                    <asp:LinkButton ID="lbtnEntry" runat="server" CausesValidation="false" OnClick="lbtnEntry_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 labelText1 ">
                                                <div class="col-sm-4">
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="col-sm-2 ">
                                                        <asp:LinkButton ID="lbtnSerchoption1" CausesValidation="false" runat="server" OnClick="lbtnSerchoption1_Click">
                                                                        <span class="glyphicon glyphicon-search fontsize20" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-2 padding0">
                                <div class="panel panel-default">
                                    <div class="panel panel-body padding0">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-sm-1 labelText1 paddingLeft0">
                                                   <asp:RadioButton runat="server" ID="chkreqdate" GroupName="chktype" Checked="true"/>
                                                </div>
                                                <div class="col-sm-2 labelText1">
                                                    Req  
                                                </div>
                                                
                                                <div class="col-sm-2 labelText1">
                                                    From
                                                </div>
                                                <div class="col-sm-5 paddingRight0">
                                                    <asp:TextBox runat="server" ID="txtReqFoDate" ReadOnly="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 paddingLeft3 paddingRight0">
                                                    <asp:LinkButton ID="lbtnReqFoDate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtReqFoDate"
                                                        PopupButtonID="lbtnReqFoDate" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-sm-1 labelText1 paddingLeft0">
                                                   <asp:RadioButton runat="server" ID="chkbonddate" GroupName="chktype"/>
                                                </div>
                                                <div class="col-sm-2 labelText1">
                                                    Bond 
                                                </div>
                                                
                                                <div class="col-sm-2 labelText1">
                                                    To
                                                </div>
                                                <div class="col-sm-5 paddingRight0">
                                                    <asp:TextBox runat="server" ID="txtReqToDate" ReadOnly="true" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 paddingLeft3 paddingRight0">
                                                    <asp:LinkButton ID="lbtnReqToDate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtReqToDate"
                                                        PopupButtonID="lbtnReqToDate" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-sm-5 labelText1 ">
                                                </div>
                                                <div class="col-sm-6 paddingLeft3 paddingRight0">
                                                    <div class="col-sm-2 ">
                                                        <asp:LinkButton ID="lbtnSerchoption2" CausesValidation="false" runat="server" OnClick="lbtnSerchoption2_Click">
                                                                        <span class="glyphicon glyphicon-search fontsize20" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-2 padding0">
                                <div class="panel panel-default">
                                    <div class="panel panel-body padding0">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                 <div class="col-sm-1 paddingLeft0 labelText1 ">
                                                   <asp:RadioButton runat="server" ID="chkexp" GroupName="chkentrydt" Checked="true"/>
                                                </div>
                                                <div class="col-sm-2 paddingLeft0 labelText1">
                                                    Expct  
                                                </div>
                                                <div class="col-sm-2 paddingLeft0 labelText1">
                                                    From
                                                </div>
                                                <div class="col-sm-5 padding0">
                                                    <asp:TextBox runat="server" ID="txtExpectedFrom" ReadOnly="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2 paddingLeft3 paddingRight0">
                                                    <asp:LinkButton ID="lbtnExpectedFrom" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtExpectedFrom"
                                                        PopupButtonID="lbtnExpectedFrom" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-sm-1 paddingLeft0 labelText1">
                                                   <asp:RadioButton runat="server" ID="chkentry" GroupName="chkentrydt"/>
                                                </div>
                                                <div class="col-sm-2 paddingLeft0 labelText1">
                                                    Entry
                                                </div>
                                                <div class="col-sm-2 paddingLeft0 labelText1">
                                                    To
                                                </div>
                                                <div class="col-sm-5 padding0">
                                                    <asp:TextBox runat="server" ID="txtExpectedTo" ReadOnly="true" Format="dd/MMM/yyyy" CausesValidation="false" CssClass="txtExpectedTo form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft3 paddingRight0">
                                                    <asp:LinkButton ID="lbtnExpectedTo" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtExpectedTo"
                                                        PopupButtonID="lbtnExpectedTo" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-sm-5 labelText1 ">
                                                </div>
                                                <div class="col-sm-6 paddingLeft3 paddingRight0">
                                                    <div class="col-sm-2 ">
                                                        <asp:LinkButton ID="lbtnSerchoption3" CausesValidation="false" runat="server" OnClick="lbtnSerchoption3_Click">
                                                                        <span class="glyphicon glyphicon-search fontsize20" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-2 padding0">
                                <div class="panel panel-default">
                                    <div class="panel panel-body padding0">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-sm-5 labelText1 ">
                                                    profit Center
                                                </div>
                                                <div class="col-sm-6 paddingRight0 paddingLeft0">
                                                    <asp:TextBox runat="server" ID="txtProcenter" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtProcenter_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft3 paddingRight0">
                                                    <asp:LinkButton ID="lbtnProcenter" runat="server" CausesValidation="false" OnClick="lbtnProcenter_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-sm-5 labelText1">
                                                    Ref.Loc
                                                </div>
                                                <div class="col-sm-6 paddingRight0 paddingLeft0">
                                                    <asp:TextBox runat="server" ID="txtloc" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtloc_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft3 paddingRight0">
                                                    <asp:LinkButton ID="lbtn" runat="server" CausesValidation="false" OnClick="lbtn_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-sm-5 labelText1 ">
                                                </div>
                                                <div class="col-sm-6 paddingLeft3 paddingRight0">
                                                    <div class="col-sm-2 ">
                                                        <asp:LinkButton ID="lbtnSerchoption4" CausesValidation="false" runat="server" OnClick="lbtnSerchoption4_Click">
                                                                        <span class="glyphicon glyphicon-search fontsize20" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-2 padding0">
                                <div class="panel panel-default">
                                    <div class="panel panel-body padding0">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-sm-3 labelText1">
                                                    Item
                                                </div>
                                                <div class="col-sm-8 paddingRight0">
                                                    <asp:TextBox runat="server" ID="txtItem" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtitem_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft3 paddingRight0">
                                                    <asp:LinkButton ID="lbtnItem" runat="server" CausesValidation="false" OnClick="lbtnItem_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-sm-5 labelText1 ">
                                                    Item Catergory
                                                </div>
                                                <div class="col-sm-6 paddingRight0 ">
                                                    <asp:TextBox runat="server" ID="txtCat1" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtCat1_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft3 paddingRight0">
                                                    <asp:LinkButton ID="lbtnCat1" runat="server" CausesValidation="false" OnClick="lbtnCat1_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-sm-5 labelText1 ">
                                                </div>
                                                <div class="col-sm-6 paddingRight0 paddingLeft0">
                                                    <div class="col-sm-2 ">
                                                        <asp:LinkButton ID="lbtnSerchoption5" CausesValidation="false" runat="server" OnClick="lbtnSerchoption5_Click">
                                                                        <span class="glyphicon glyphicon-search fontsize20" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-2 padding0">
                                <div class="panel panel-default ">
                                    <div class="panel panel-body padding0">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-sm-5 padding0 labelText1">
                                                    Request Status
                                                </div>
                                                <div class="col-sm-5 paddingRight0">
                                                    <asp:DropDownList ID="ddlStatus" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control">
                                                        <asp:ListItem Text="All"></asp:ListItem>
                                                        <asp:ListItem Text="Pending"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2 ">
                                                    <asp:LinkButton ID="lbtnSerchoption6" CausesValidation="false" runat="server" OnClick="lbtnSerchoption6_Click">
                                                                        <span class="glyphicon glyphicon-search fontsize20" aria-hidden="true"></span>
                                                    </asp:LinkButton>


                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-2 labelText1">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-2 labelText1">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-default">
                                <div class="panel panel-body padding0">
                                    <div style="height: 225px; overflow-y: auto;">
                                        <asp:GridView ID="grdDeatails" AllowPaging="True" PagerStyle-CssClass="cssPager" PageSize="8"
                                            CssClass="table table-hover table-striped tblTest" runat="server"
                                            GridLines="None" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No data found..." AutoGenerateColumns="False" OnPageIndexChanging="grdDeatails_PageIndexChanging"
                                            OnRowDataBound="grdDeatails_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Request #">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_ReqA" runat="server" Text='<%# Bind("Req_App_No") %>' Width="110px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Request No" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_Reqno" runat="server" Width="70px" Text='<%# Bind("Req_No") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Request By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_ReqBy" runat="server" Text='<%# Bind("Req_By") %>' Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Request Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_Reqdate" runat="server" Text='<%# Bind("Req_Date", "{0:dd/MMM/yyyy}") %>' Width="120px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Approved On">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_ReqADate" runat="server" Text='<%# Bind("Req_App_Date", "{0:dd/MMM/yyyy}") %>' Width="120px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_ReqStatus" runat="server" Text='<%# Bind("ReqStatus") %>' Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bond #">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_entryno" runat="server" Text='<%# Bind("Entry_No") %>' Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bond Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_entrydate" runat="server" Text='<%# Bind("Entry_Date", "{0:dd/MMM/yyyy}") %>' Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Entry #">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_entryno2" runat="server" Text='<%# Bind("Cusdec_EntryNo") %>' Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Entry Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_entrydate2" runat="server" Text='<%# Bind("Cusdec_EntryDate", "{0:dd/MMM/yyyy}") %>' Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Created On">
                                                    <ItemTemplate>
                                                        <asp:Label ID="col_entrycreatedon" runat="server" Text='<%# Bind("Entry_Create_Date", "{0:dd/MMM/yyyy  hh:mm tt}") %>' Width="120px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Dispatch Qty" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtndisqty" CausesValidation="false" runat="server" Text='<%# Bind("Dispatch_Qty","{0:n}") %>' OnClick="lbtndisqty_Click">                                                                      
                                                        </asp:LinkButton>
                                                        <%-- <asp:Label ID="ib_bl_dt" runat="server" Text='<%# Bind("Dispatch_Qty","{0:n}") %>' Width="70px"></asp:Label>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Receipt Qty" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtn_recqty" CausesValidation="false" runat="server" Text='<%# Bind("Receipt_Qty","{0:n}") %>' OnClick="lbtn_recqty_Click">                                                                      
                                                        </asp:LinkButton>
                                                        <%--<asp:Label ID="ib_bl_dt" runat="server" Text='<%# Bind("Receipt_Qty","{0:n}") %>' Width="70px"></asp:Label>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnLoadGrid" CausesValidation="false" runat="server" OnClick="lbtnLoadGrid_Click">   
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>                                                                   
                                                        </asp:LinkButton>

                                                        <%-- <asp:Label ID="ib_bl_dt" runat="server" Text='<%# Bind("Dispatch_Qty","{0:n}") %>' Width="70px"></asp:Label>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--     <asp:TemplateField HeaderText="IS_VALUE_FOR_GRN" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtn_recqty" CausesValidation="false" runat="server" Text='<%# Bind("IS_VALUE_FOR_GRN") %>'>                                                                      
                                                    </asp:LinkButton>
                                                   
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnLoadGRN" CausesValidation="false" runat="server"
                                                            OnClick="lbtnLoadGRN_Click" Enabled='<%# !Eval("IS_VALUE_FOR_GRN").ToString().Equals("")%>'
                                                            CommandArgument='<%# Eval("Req_App_No").ToString()%>'>
                                                            <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                        </asp:LinkButton>

                                                        <%-- <asp:Label ID="ib_bl_dt" runat="server" Text='<%# Bind("Dispatch_Qty","{0:n}") %>' Width="70px"></asp:Label>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                            <PagerStyle CssClass="cssPager" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="col-sm-2" style="padding-left: 1px; padding-right: 1px;">
                                <div class="panel panel-default">
                                    <div class="panel-heading paddingtopbottom0">
                                        <strong>Cusdec Entry Details</strong>
                                    </div>
                                    <div class="panel-body" style="height: 180px;">
                                        <div class="col-sm-12">
                                            <div class="panelscoll" style="height: 180px">
                                                <asp:GridView ID="grdentrydetails" CssClass="table table-hover table-striped tblTest" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Item">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbITRI_ITM_CD" runat="server" Text='<%# Bind("ITRI_ITM_CD") %>' Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbITRI_QTY" runat="server" Text='<%# Bind("ITRI_QTY") %>' Width="30px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bal Qty">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbITRI_BQTY" runat="server" Text='<%# Bind("ITRI_BQTY") %>' Width="30px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-5" style="padding-left: 1px; padding-right: 1px;">
                                <div class="panel panel-default">
                                    <div class="panel-heading paddingtopbottom0">
                                        <strong>Inventory Movement - Outward</strong>
                                    </div>
                                    <div class="panel-body" style="height: 180px;">

                                        <div class="col-sm-12">
                                            <div class="panelscoll" style="height: 180px">
                                                <asp:GridView ID="gridDispach" CssClass="table table-hover table-striped tblTest" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                    <Columns>
                                                           <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtngrnsearch" CausesValidation="false" runat="server" OnClick="lbtngrnsearch_Click">   
                                                        <span class="glyphicon glyphicon-ban-circle" aria-hidden="true"></span>                                                                   
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Doc #">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblith_doc_no" runat="server" Text='<%# Bind("ith_doc_no") %>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Doc Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblith_doc_date" runat="server" Text='<%# Bind("ith_doc_date", "{0:dd/MMM/yyyy}") %>' Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbliti_itm_cd" runat="server" Text='<%# Bind("iti_itm_cd") %>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbliti_itm_stus" runat="server" Text='<%# Bind("iti_itm_stus") %>' Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbliti_qty" runat="server" Text='<%# Bind("iti_qty") %>' Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUom" runat="server" Text='' Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Physical Item">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbitb_base_itmcd" runat="server" Text='<%# Bind("itb_base_itmcd") %>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rec Loc">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbith_oth_loc" runat="server" Text='<%# Bind("ith_oth_loc") %>' Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Model">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbliti_Model" runat="server" Text='' Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="col-sm-5" style="padding-left: 1px; padding-right: 1px;">
                                <div class="panel panel-default">
                                    <div class="panel-heading paddingtopbottom0">
                                        <strong>Inventory Movement - Inward</strong>
                                    </div>
                                    <div class="panel-body">

                                        <div class="col-sm-12">
                                            <div class="panelscoll" style="height: 180px">
                                                <asp:GridView ID="grdreceipt" CssClass="table table-hover table-striped tblTest" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Doc #">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblith_doc_no" runat="server" Text='<%# Bind("ith_doc_no") %>' Width="90px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Other Ref">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblith_oth_docno" runat="server" Text='<%# Bind("ith_oth_docno") %>' Width="90px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Doc Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblith_doc_date" runat="server" Text='<%# Bind("ith_doc_date", "{0:dd/MMM/yyyy}") %>' Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbliti_itm_cd" runat="server" Text='<%# Bind("iti_itm_cd") %>' Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbliti_itm_stus" runat="server" Text='<%# Bind("iti_itm_stus") %>' Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbliti_qty" runat="server" Text='<%# Bind("iti_qty") %>' Width="30px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUom" runat="server" Text='' Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Model">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbliti_Model" runat="server" Text='' Width="50px"></asp:Label>
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>


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
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="GRNUserPopoup" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="grnpnlpopup" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="grnpnlpopup" DefaultButton="lbtnSearch">
                <div runat="server" id="Div1" class="panel panel-default height400 width700">
                    <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>

                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">


                            <div class="col-sm-12" style="padding-left: 1px; padding-right: 1px;">
                                <div class="panel panel-default">
                                    <div class="panel-heading paddingtopbottom0">
                                        <strong>GRN Details</strong>
                                    </div>
                                    <div class="panel-body panel-body panelscollbar height120" style="overflow-x: hidden">

                                        <div class="col-sm-12">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView ID="grdGrnItemHdr" CssClass="table table-hover table-striped tblTest" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                        EmptyDataText="No data found..." AutoGenerateColumns="False" OnPageIndexChanging="grdGrnItem_PageIndexChanging">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true"
                                                                        OnCheckedChanged="CheckBox1_CheckedChanged" CommandArgument='<%# Eval("ITH_SEQ_NO").ToString()%>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <%--                                                      <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chk_selected_grn" runat="server" CommandArgument='<%# Eval("ITH_SEQ_NO").ToString()%>' Width="20px"
                                                                OnCheckedChanged="chk_selected_grn_CheckedChanged" ></asp:CheckBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                            <asp:TemplateField HeaderText="Doc #" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblITH_SEQ_NO" runat="server" Text='<%# Bind("ITH_SEQ_NO") %>' Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Location">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblITH_LOC" runat="server" Text='<%# Bind("ITH_LOC") %>' Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Doc #">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblITH_DOC_NO" runat="server" Text='<%# Bind("ITH_DOC_NO") %>' Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblITH_DOC_DATE" runat="server" Text='<%# Bind("ITH_DOC_DATE", "{0:dd/MMM/yyyy}") %>' Width="90px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Entry #">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblITH_ENTRY_NO" runat="server" Text='<%# Bind("ITH_ENTRY_NO") %>' Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Ref">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblITH_MANUAL_REF" runat="server" Text='<%# Bind("ITH_MANUAL_REF") %>' Width="70px"></asp:Label>
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



                            <div class="col-sm-12" style="padding-left: 1px; padding-right: 1px;">
                                <div class="panel panel-default">
                                    <div class="panel-heading paddingtopbottom0">
                                        <strong>Item Details</strong>
                                    </div>
                                    <div class="panel-body panel-body panelscollbar height200" style="overflow-x: hidden">

                                        <div class="col-sm-12">

                                            <asp:GridView ID="grnItemDetail" CssClass="table table-hover table-striped tblTest" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No data found..." AutoGenerateColumns="False" OnPageIndexChanging="grdGrnItem_PageIndexChanging">
                                                <Columns>

                                                    <%-- <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chk_selected_grn" runat="server" CommandArgument='<%# Eval("ITB_SEQ_NO").ToString()%>' Width="20px"></asp:CheckBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                    <asp:TemplateField HeaderText="Doc #" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblINB_SEQ_NO" runat="server" Text='<%# Bind("ITB_SEQ_NO") %>' Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--  <asp:TemplateField HeaderText="Location">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblITB_LOC" runat="server" Text='<%# Bind("ITB_LOC") %>' Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                    <asp:TemplateField HeaderText="Item Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblINB_ITM_CD" runat="server" Text='<%# Bind("ITB_ITM_CD") %>' Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblITH_DOC_DATE" runat="server" Text='<%# Bind("ITB_DOC_DATE", "{0:dd/MMM/yyyy}") %>' Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblINB_ITM_STUS" runat="server" Text='<%# Bind("MIS_DESC") %>' Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qty" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblINB_BASE_QTY" runat="server" Text='<%# Bind("BASE_QTY") %>' Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Balance" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblINB_QTY" runat="server" Text='<%# Bind("BALANCE_QTY") %>' Width="70px"></asp:Label>
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


</asp:Content>
