<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="CustomerCreation.aspx.cs" Inherits="FastForward.SCMWeb.View.MasterFiles.Operational.CustomerCreation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/ucCustomer.ascx" TagPrefix="uc1" TagName="ucCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script>
        function SaveConfirmExtented() {
            var selectedvalue = confirm("Do you want to save data?");
            if (selectedvalue) {
                document.getElementById('<%=txtSaveconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtSaveconformmessageValue.ClientID %>').value = "No";
            }
        };

    </script>
    <script type="text/javascript">
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
    </script>

    <script type="text/javascript">

        function ConfirmExtented() {
            var selectedvalue = confirm("Do you want to save ?");
            if (selectedvalue) {
                document.getElementById('<%=txtSaveconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtSaveconformmessageValue.ClientID %>').value = "No";
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
        function ClearConfirmExtented() {
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

        .upper-case {
            text-transform: uppercase;
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
    <asp:HiddenField ID="hdfTabIndex" runat="server" />
    <div class="panel panel-default marginLeftRight5">
        <div class="panel-body">
            <asp:UpdatePanel ID="pnlBasePanel" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-sm-6  buttonrow">
                            <div class="col-sm-6">
                                <h1 style="font-size: 11px;">Customer Creation</h1>

                            </div>
                            <%-- <div id="WarningCustomer" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">
                                <div class="col-sm-11">
                                    <strong>Alert!</strong>
                                    <asp:Label ID="lblWCustomer" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-1">
                                    <asp:LinkButton ID="lbtnLinkButton27" runat="server" CausesValidation="false" CssClass="floatright">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div id="SuccessCustomer" runat="server" visible="false" class="alert alert-success alert-dismissible" role="alert">
                                <div class="col-sm-11">
                                    <strong>Success!</strong>
                                    <asp:Label ID="lblSCustomer" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-1">
                                    <asp:LinkButton ID="lbtnLinkButton30" runat="server" CausesValidation="false" CssClass="floatright">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div id="AlertCustomer" runat="server" visible="false" class="alert alert-info alert-dismissible" role="alert">
                                <strong>Alert!</strong>
                                <asp:Label ID="lblCAlert" runat="server"></asp:Label>
                            </div>--%>
                        </div>
                        <div class="col-sm-2 buttonRow">
                            <div class="col-sm-10 paddingRight0">
                                <asp:LinkButton ID="lbtnmulticus" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lbtnmulticus_Click">
                                        <span class="glyphicon glyphicon-upload" aria-hidden="true"></span>Multiple/Customers
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-sm-4 buttonRow">

                            <div class="col-sm-4 paddingRight0">
                                <asp:LinkButton ID="lbtnsunaccountup" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lbtnsunaccountup_Click">
                                        <span class="glyphicon glyphicon-upload" aria-hidden="true"></span>Upload
                                </asp:LinkButton>
                            </div>

                            <div class="col-sm-5 paddingRight0">
                                <asp:LinkButton ID="lbtnAddNewCus" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="SaveConfirmExtented()" OnClick="lbtnAddNewCus_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Add New/Update
                                </asp:LinkButton>
                            </div>
                            <%--<div class="col-sm-3 paddingRight0">
                                <asp:LinkButton ID="lbtnUpdateCus" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="ConfirmExtented()" OnClick="lbtnUpdateCus_Click">
                                        <span class="glyphicon glyphicon-edit" aria-hidden="true"></span>Update
                                </asp:LinkButton>
                            </div>--%>
                            <div class="col-sm-3">
                                <asp:LinkButton ID="lbtnClearCus" runat="server" CssClass="floatRight" OnClick="lbtnClearCus_Click" OnClientClick="ClearConfirmExtented()">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-7 paddingRight0">
                            <asp:UpdatePanel ID="pnlLeftPanel" runat="server">
                                <ContentTemplate>
                                    <uc1:ucCustomer runat="server" ID="ucCustomer" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-sm-5 paddingLeft0">
                            <div class="panel panel-default">
                                <div class="panel-heading paddingtopbottom0">
                                    Sales Types
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-5 paddingRight0 height160">
                                            <div class="row">
                                                <div class="col-sm-12 height10">
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="col-sm-2 labelText1">
                                                        Type
                                                    </div>
                                                    <div class="col-sm-8 paddingLeft5 width200 labelText1">
                                                        <asp:DropDownList ID="ddlSalesTypes" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-1 width10 paddingLeft5 labelText1">
                                                        <asp:CheckBox ID="chkSalesTypes" runat="server" AutoPostBack="True" OnCheckedChanged="chkSalesTypes_CheckedChanged" />
                                                    </div>
                                                    <div class="col-sm-1 paddingRight0 labelText1">
                                                        <asp:LinkButton ID="lbtnAddSalesType" runat="server" CausesValidation="false" OnClick="lbtnAddSalesType_Click">
                                                        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>

                                            <%-- Dulaj 2018/Nov/29 --%>

                                               <div class="row">
                                                     <div class="col-sm-12">
                                                <div class="col-sm-12 padding0">
                                                                <div class="col-sm-3 labelText1">
                                                                    From
                                                                </div>
                                                                <div class="col-sm-7 padding03">
                                                                    <asp:TextBox runat="server" ID="txtFrom" CausesValidation="false" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 padding03">
                                                                    <asp:LinkButton ID="txtFroml" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtFrom"
                                                                        PopupButtonID="txtFroml" Format="dd/MMM/yyyy">
                                                                    </asp:CalendarExtender>
                                                                </div>
                                                            </div>     
                                                         </div>                                                      
                                            </div>
                                            <div class="row">
                                                  <div class="col-sm-12">
                                                 <div class="col-sm-12 padding0">
                                                                <div class="col-sm-3 labelText1">
                                                                    To
                                                                </div>
                                                                <div class="col-sm-7 padding03">
                                                                    <asp:TextBox runat="server" Enabled="false" ID="txtTo" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 padding03">
                                                                    <asp:LinkButton ID="txtTol" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtTo"
                                                                        PopupButtonID="txtTol" Format="dd/MMM/yyyy">
                                                                    </asp:CalendarExtender>
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
                                                    <div class="col-sm-12 labelText1 ">
                                                        Quotation Number Sequence Generation
                                                    </div>
                                                </div>
                                                <div class="col-sm-12">
                                                    <div class="col-sm-6">

                                                        <div class="col-sm-1 width10 labelText2">
                                                            <asp:RadioButton ID="optPCwise" Checked="true" runat="server" value="0" GroupName="Ttle" />
                                                        </div>
                                                        <div class="col-sm-3 labelText2 width30">
                                                            PC-wise
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="col-sm-1 width10 labelText2">
                                                            <asp:RadioButton ID="optCompwise" runat="server" value="1" GroupName="Ttle" />
                                                        </div>
                                                        <div class="col-sm-3 labelText2 width30">
                                                            Company-wise
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="col-sm-6">

                                                        <div class="col-sm-1 width10 labelText2">
                                                            <asp:CheckBox ID="radactive" Checked="true" runat="server" />
                                                        </div>
                                                        <div class="col-sm-3 labelText2 width30">
                                                            Active
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="col-sm-1 width10 labelText2">
                                                            <asp:CheckBox ID="radsuspent" runat="server" />
                                                        </div>
                                                        <div class="col-sm-3 labelText2 width30">
                                                            Suspend
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-1 width10 labelText2">
                                                            <asp:CheckBox ID="chkfoc" runat="server" value="1" />
                                                        </div>
                                                        <div class="col-sm-6">
                                                            FOC Allow
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-sm-7">
                                            <div class="panel-body panelscollbar paddingLeft0 paddingRight0 height120">
                                                <asp:GridView ID="grdSalesTypes" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Remove">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnsaletype_Remove" runat="server" CausesValidation="false" OnClientClick="DeleteConfirm()" Width="5px" OnClick="lbtnsaletype_Remove_Click">
                                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true" ></span>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="srtp_cd" runat="server" Text='<%# Bind("SRTP_CD") %>' Width="100px"></asp:Label>
                                                                <%--<asp:DropDownList ID="ddlSalesTypes" Text='<%# Bind("SRTP_CD") %>' CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control"></asp:DropDownList>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Description">
                                                            <ItemTemplate>
                                                                <asp:Label ID="srtp_desc" runat="server" Text='<%# Bind("SRTP_DESC") %>' Width="100px"></asp:Label>
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
                        <div class="col-sm-5 paddingLeft0">
                            <div class="panel panel-default">
                                <div class="panel-heading paddingtopbottom0">
                                    Customer Segmentation
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="panel-body panelscollbar paddingLeft0 paddingRight0 height120">
                                                <asp:GridView ID="grdCustomerSegmentation" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="_Mandatory" AutoPostBack="true" runat="server" OnCheckedChanged="_Mandatory_CheckedChanged" Enabled="true" Checked='<%#Convert.ToBoolean(Eval("isMandatory")) %>' Width="100px"></asp:CheckBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Type">
                                                            <ItemTemplate>
                                                                <%-- rename labels --%>
                                                                <asp:Label ID="rbt_tp" runat="server" Text='<%# Bind("TypeCD_") %>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Value">
                                                            <ItemTemplate>
                                                                <%-- rename labels --%>
                                                                <asp:Label ID="rbt_desc" runat="server" Text='<%# Bind("TypeDesctipt") %>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlAgeVals" CssClass="form-control" runat="server" Width="100px"></asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 paddingRight0 paddingLeft0">
                                            <div class="col-sm-6">
                                                <div class="col-sm-6 labelText1">
                                                    Showroom Status
                                                </div>
                                                <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                    <asp:DropDownList ID="ddlShowroomStus" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="col-sm-6 labelText1 ">
                                                    Head Office
                                                </div>
                                                <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                    <asp:DropDownList ID="ddlHeadOffStus" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
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
                                            <div class="col-sm-2 labelText2">
                                                Credit Limit
                                            </div>
                                            <div class="col-sm-3 paddingLeft15">
                                                <asp:TextBox ID="txtCredLimit" CssClass="form-control" value="0" runat="server"></asp:TextBox>
                                            </div>

                                            <div class="col-sm-1 width25 labelText2">
                                                <asp:CheckBox ID="chkDCN" runat="server" />
                                            </div>
                                            <div class="col-sm-2 labelText2 width30">
                                                Allow DCN
                                            </div>


                                            <div class="col-sm-1 width25 labelText2">
                                                <asp:CheckBox ID="chkInsuMan" runat="server" />
                                            </div>
                                            <div class="col-sm-3 labelText2 width30">
                                                Insurance Mandatory
                                            </div>

                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 height5">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-sm-4 labelText2 paddingLeft0 paddingRight0">
                                                Minimum Down Payment(%)
                                            </div>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtMinDwnPymnt" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-5 paddingLeft0">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="panel panel-default">
                                        <div class="panel-body">
                                            <div class="bs-example">
                                                <ul class="nav nav-tabs" id="myTab">
                                                    <li class="active"><a href="#Additional" data-toggle="tab">Additional Details</a></li>
                                                    <li><a href="#Office_of_Entry" data-toggle="tab">Office of Entry</a></li>
                                                </ul>
                                                <div class="row">
                                                    <div class="col-sm-12 height5">
                                                    </div>
                                                </div>
                                                <div class="tab-content">
                                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                                    <asp:HiddenField ID="txtSaveconformmessageValue" runat="server" />
                                                    <asp:HiddenField ID="HiddenField2" runat="server" />
                                                    <asp:HiddenField ID="txtAlertValue" runat="server" />
                                                    <div class="tab-pane active" id="Additional">
                                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                                                        <asp:UpdatePanel ID="pnlAddionlDet" runat="server">
                                                            <ContentTemplate>
                                                                <div class="row">
                                                                    <div class="col-sm-12 height5">
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-6 padding0">
                                                                        <div class="col-sm-5 labelText2">
                                                                            TIN No              
                                                                        </div>
                                                                        <div class="col-sm-7 paddingLeft0">
                                                                            <asp:TextBox ID="txtTINNo" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtTINNo_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6 padding0">
                                                                        <div class="col-sm-5 labelText2">
                                                                            Print Value 1         
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight0">
                                                                            <asp:TextBox ID="txtddlPrVal1" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtddlPrVal1_TextChanged"> 
                                                                            </asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-6 padding0">
                                                                        <div class="col-sm-5 labelText2">
                                                                            Procedure Code 
                                                                                         
                                                                        </div>
                                                                        <div class="col-sm-5 paddingLeft0">
                                                                            <asp:TextBox ID="txtProceCode" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtProceCode_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-2 paddingLeft5 paddingLeft0">
                                                                            <asp:LinkButton ID="btnProceSearch" runat="server" CausesValidation="false" OnClick="btnProceSearch_Click">
                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6 padding0">
                                                                        <div class="col-sm-5 labelText2">
                                                                            Print Value 2            
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight0">
                                                                            <asp:TextBox ID="txtddlPrVal2" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtddlPrVal2_TextChanged">
                                                                            </asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-6 padding0">
                                                                        <div class="col-sm-5 labelText2">
                                                                            Warehouse Code            
                                                                        </div>
                                                                        <div class="col-sm-7 paddingLeft0">
                                                                            <asp:TextBox ID="txtWareHseCde" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtWareHseCde_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6 padding0">
                                                                        <div class="col-sm-5 labelText2">
                                                                            Account Code            
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight0">
                                                                            <asp:TextBox ID="txtAccCde" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtAccCde_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-6 padding0">
                                                                        <div class="col-sm-5 labelText2">
                                                                            Executive           
                                                                        </div>
                                                                        <div class="col-sm-7 paddingLeft0">
                                                                            <asp:TextBox ID="txtexecutive" CssClass="form-control  upper-case" runat="server" AutoPostBack="true" OnTextChanged="txtexecutive_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                     <div class="col-sm-6 padding0">
                                                                        <div class="col-sm-5 labelText2">
                                                                            Account Number           
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight0">
                                                                            <asp:TextBox ID="txtaccountNumber" CssClass="form-control  upper-case" runat="server" AutoPostBack="true"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <div class="tab-pane" id="Office_of_Entry">
                                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                                                        <asp:UpdatePanel ID="pnlOfficeofEntryBasePanel" runat="server">
                                                            <ContentTemplate>

                                                                <div class="panel panel-default">
                                                                    <div class="panel-body">
                                                                        <div class="col-sm-6 paddingLeft0">
                                                                            <div class="panel-body">
                                                                                <div class="row">
                                                                                    <div class="col-sm-12 ">
                                                                                        <div class="col-sm-4 labelText1 padding0">
                                                                                            Office of entry
                                                                                        </div>
                                                                                        <div class="col-sm-3 padding0">
                                                                                            <asp:DropDownList ID="ddlOfficeofEntry" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control">
                                                                                            </asp:DropDownList>
                                                                                        </div>
                                                                                        <div class="col-sm-2 labelText1 paddingLeft5">
                                                                                            Office
                                                                                        </div>
                                                                                        <div class="col-sm-3 padding0">
                                                                                            <asp:TextBox runat="server" ID="txtOfficeName" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtOfficeName_TextChanged"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-6 padding0">
                                                                            <div class="panel-body">
                                                                                <div class="row">
                                                                                    <div class="col-sm-1 labelText1 width70">
                                                                                        Direction
                                                                                    </div>
                                                                                    <div class="col-sm-1 labelText1 width100">
                                                                                        <asp:DropDownList ID="ddlDirection" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control">
                                                                                        </asp:DropDownList>
                                                                                    </div>

                                                                                    <div class="col-sm-1 labelText1 width40">
                                                                                        Active
                                                                                    </div>
                                                                                    <div class="col-sm-1 width25 labelText1">
                                                                                        <asp:CheckBox runat="server" ID="chkStatus" AutoPostBack="true" />
                                                                                    </div>

                                                                                    <div class="col-sm-1 paddingRight0">
                                                                                        <asp:LinkButton ID="lbtnAddOfficeofEntry" runat="server" CausesValidation="false" OnClick="lbtnAddOfficeofEntry_Click">
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
                                                                        <div class="panel panel-default marginLeftRight5">
                                                                            <div class="panel-body">
                                                                                <div class="col-sm-12">
                                                                                    <div class="panel-body panelscollbar height120">
                                                                                        <asp:GridView ID="grdcustomerOfficeEntry" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                                            EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                                            <Columns>
                                                                                                <asp:TemplateField HeaderText="Code">
                                                                                                    <ItemTemplate>
                                                                                                        <%-- RenameLabels --%>
                                                                                                        <asp:Label ID="lbl_cCode" runat="server" Text='<%# Bind("_mbbo_cd")%>' Width="100px"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Type">
                                                                                                    <ItemTemplate>
                                                                                                        <%-- RenameLabels --%>
                                                                                                        <asp:Label ID="lbl_cType" runat="server" Text='<%# Bind("_mbbo_tp")%>' Width="100px"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Direction">
                                                                                                    <ItemTemplate>
                                                                                                        <%-- RenameLabels --%>
                                                                                                        <asp:Label ID="lbl_ofoenDir" runat="server" Text='<%# Bind("_mbbo_direct")%>' Width="100px"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Office Code">
                                                                                                    <ItemTemplate>
                                                                                                        <%-- RenameLabels --%>
                                                                                                        <asp:Label ID="lbl_ofoenCode" runat="server" Text='<%# Bind("_mbbo_off_cd")%>' Width="100px"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Active">
                                                                                                    <ItemTemplate>
                                                                                                        <%-- RenameLabels --%>
                                                                                                        <asp:CheckBox ID="chk_ofoenStatus" runat="server" Enabled="true" Checked='<%#Convert.ToBoolean(Eval("_mbbo_act")) %>' Width="100px"></asp:CheckBox>
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
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="Button4" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="upSunaccount" runat="server" Enabled="True" TargetControlID="Button4"
                PopupControlID="pnlsunacc" PopupDragHandleControlID="divsun" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:Panel runat="server" ID="pnlsunacc">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="panel panel-default width250">
                    <div class="panel-heading height30" id="divsun" style="height: 30px;">
                        <div class="col-sm-10">
                        </div>
                        <div class="col-sm-2">
                            <asp:LinkButton ID="lbtnPriceClose" runat="server" Style="float: right" OnClick="lbtnPriceClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="panel-body" style="height: 30px;">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-sm-12">
                                    <div class="row">
                                        <div class="col-sm-5 labelText1 paddingLeft0">
                                            Account No
                                        </div>
                                        <div class="col-sm-5 labelText1 paddingLeft0">
                                            <asp:TextBox ID="txtsunaccno" runat="server" CssClass="form-control" OnTextChanged="txtsunaccno_TextChanged" />
                                        </div>
                                        <div class="col-sm-2 paddingRight5 paddingLeft0">
                                            <asp:LinkButton ID="lbtnloaddata" CausesValidation="false" CssClass="floatRight" runat="server" OnClick="lbtnloaddata_Click">
                                         <span class="glyphicon glyphicon-upload" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>



    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="btnpanalmultiplecus" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="multiplecuspopup" runat="server" Enabled="True" TargetControlID="btnpanalmultiplecus"
                PopupControlID="pnlmultiplecus" PopupDragHandleControlID="divsun" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:Panel runat="server" ID="pnlmultiplecus">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="panel panel-default width450">
                    <div class="panel-heading ">
                        <div class="col-sm-10">
                        </div>
                        <div class="col-sm-2">
                            <asp:LinkButton ID="lbtnmultycusclose" runat="server" Style="float: right" OnClick="lbtnmultycusclose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="panel-body height200">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="row">
                                    <div class="col-sm-1"></div>
                                    <div class="col-sm-4 labelText1 paddingLeft0">
                                        Additional Custormer
                                    </div>
                                    <div class="col-sm-3 labelText1 paddingLeft0">
                                        <asp:TextBox ID="txtmulticus" AutoPostBack="true" runat="server" CssClass="form-control" OnTextChanged="txtmulticus_TextChanged" />
                                    </div>
                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                        <asp:LinkButton ID="lbtnCustomer" runat="server" CausesValidation="false" OnClick="lbtnCustomer_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                        <asp:Label ID="lblCustomerName" Visible="false" runat="server" Text="Customer Name"></asp:Label>
                                    </div>
                                    <div class="col-sm-2 paddingRight5 paddingLeft0">
                                        <asp:LinkButton ID="lbtnaddmulticus" CausesValidation="false" CssClass="floatRight" runat="server" OnClick="lbtnaddmulticus_Click">
                                         <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-sm-8 row">
                                    <asp:GridView ID="grdmultycus" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                        EmptyDataText="No data found..." AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Code">
                                                <ItemTemplate>
                                                    <%-- RenameLabels --%>
                                                    <asp:Label ID="lblmbac_add_cd" runat="server" Text='<%# Bind("mbac_add_cd")%>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Option">
                                                <ItemTemplate>
                                                    <%-- RenameLabels --%>
                                                    <asp:LinkButton ID="lbtndelmulcus" runat="server" Width="100px" OnClick="lbtndelmulcus_Click">
                                                                                                               <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </div>
                        </div>
                        <div class="row col-sm-8">
                            <asp:LinkButton ID="lbtnsubcussave" runat="server" OnClick="lbtnsubcussave_Click">Save</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpUserPopup" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="pnlpopup" PopupDragHandleControlID="divHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel runat="server" ID="pnlpopup" DefaultButton="lbtnSearch" Style="display: none;">
                <div runat="server" id="test" class="panel panel-default height400 width700">
                    <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading" id="divHeader">
                            <asp:LinkButton ID="btnClose" runat="server" OnClick="btnClose_Click">
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
                                    <div class="col-sm-3 paddingRight5">
                                        <asp:DropDownList ID="ddlSearchbykey" runat="server" class="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2 labelText1">
                                        Search by word
                                    </div>
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <div class="col-sm-4 paddingRight5">
                                                <asp:TextBox ID="txtSearchbyword" CausesValidation="false" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbyword_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="lbtnSearch" runat="server" OnClick="lbtnSearch_Click">
                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
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
                                    <asp:GridView ID="grdResult" CausesValidation="false" runat="server" AllowPaging="True" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" OnSelectedIndexChanged="grdResult_SelectedIndexChanged" OnPageIndexChanging="grdResult_PageIndexChanging">
                                        <Columns>
                                            <asp:ButtonField Text="select" CommandName="Select" ItemStyle-Width="10" />
                                        </Columns>
                                    </asp:GridView>
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
            $('#BodyContent_txtTINNo,#BodyContent_txtddlPrVal1,#BodyContent_txtProceCode,#BodyContent_txtddlPrVal2,#BodyContent_txtWareHseCde,#BodyContent_txtAccCde,#BodyContent_txtexecutive,#BodyContent_txtOfficeName').on('keypress', function (event) {
                var regex = new RegExp("^[`~!@#$%^&*()=+]+$");
                var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
                if (regex.test(key)) {
                    event.preventDefault();
                    return false;
                }
            });
        }
    </script>
</asp:Content>
