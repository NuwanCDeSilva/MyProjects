<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="User_Creation.aspx.cs" Inherits="FastForward.SCMWeb.View.ADMIN.User_Creation" %>

<%@ Register Src="~/UserControls/ucProfitCenterSearch.ascx" TagPrefix="uc1" TagName="ucProfitCenterSearch" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/ucLoactionSearch.ascx" TagPrefix="uc1" TagName="ucLoactionSearch" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('show');
        }
    </script>
    <script type="text/javascript">
        function CheckAll(oCheckbox) {
            var GridView2 = document.getElementById("<%=grvLocs.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }
        function CheckAll2(oCheckbox) {
            var GridView2 = document.getElementById("<%=gvUserLoc.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }
        function CheckAllPro(oCheckbox) {
            var GridView2 = document.getElementById("<%=gvUserPC.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }
    </script>
    <script type="text/javascript">
        function NewConfirm() {
            <%-- var selectedvalue = confirm("Do you want to create new user?");
            if (selectedvalue) {
                document.getElementById('<%=txtAlertValue.ClientID %>').value = "Yes";
                return;
            } else {
                document.getElementById('<%=txtAlertValue.ClientID %>').value = "No";
                return;
            }--%>
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
        //function Confirm() {
        //    var confirm_value = document.createElement("INPUT");
        //    confirm_value.type = "hidden";
        //    confirm_value.name = "confirm_value";
        //    if (confirm("Do you want to save data?")) {
        //        confirm_value.value = "Yes";
        //    } else {
        //        confirm_value.value = "No";
        //    }
        //    document.forms[0].appendChild(confirm_value);
        //}
        function Confirm() {
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
        //function DeleteConfirm() {
        //    var confirm_value = document.createElement("INPUT");
        //    confirm_value.type = "hidden";
        //    confirm_value.name = "DeleteConfirm_value";
        //    if (confirm("Do you want to Delete data?")) {
        //        confirm_value.value = "Yes";
        //    } else {
        //        confirm_value.value = "No";
        //    }
        //    document.forms[0].appendChild(confirm_value);
        //}
        function DeleteConfirm() {
            var selectedvalue = confirm("Are you sure you want to delete ?");
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
        function newUserConfirm() {
            var selectedvalue = confirm("Do you want to create new user?");
            if (selectedvalue) {
                document.getElementById('<%=txtDeleteconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtDeleteconformmessageValue.ClientID %>').value = "No";
            }
        };

    </script>
    <script type="text/javascript"><!--
    function filterDigits(eventInstance) {
        eventInstance = eventInstance || window.event;
        key = eventInstance.keyCode || eventInstance.which;
        if ((key < 58) && (key > 47) || key == 45 || key == 8) {
            return true;
        }

        else {
            if (eventInstance.preventDefault)
                eventInstance.preventDefault();
            eventInstance.returnValue = false;
            return false;

        } //if
    } //filterDigits


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="panel panel-default marginLeftRight5">
        <div class="panel-body">
            <div class="bs-example">
                <ul class="nav nav-tabs" id="myTab">
                    <li class="active"><a href="#UserDetails" data-toggle="tab">Create User</a></li>
                    <li><a href="#Assign_Company" data-toggle="tab">Assign Company</a></li>
                    <li><a href="#Assign_Role" data-toggle="tab">Assign Role</a></li>
                    <li><a href="#SBU" data-toggle="tab">Strategic Business Units</a></li>
                    <li><a href="#Assign_Location" data-toggle="tab">Assign Location</a></li>
                    <li><a href="#Assign_Profit_Center" data-toggle="tab">Assign Profit Center</a></li>
                    <li><a href="#Special_Permissions" data-toggle="tab">Special Permissions</a></li>
                    <li><a href="#Approve_Permissions" data-toggle="tab">Approve Permissions</a></li>
                </ul>
            </div>
            <div class="tab-content">
                <asp:HiddenField ID="txtClearlconformmessageValue" runat="server" />
                <asp:HiddenField ID="txtSaveconformmessageValue" runat="server" />
                <asp:HiddenField ID="txtDeleteconformmessageValue" runat="server" />
                <asp:HiddenField ID="txtAlertValue" runat="server" />
                <div class="tab-pane active" id="UserDetails">
                    <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-sm-8  buttonrow">
                                    <div id="WarningUser" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">
                                        <div class="col-sm-11">
                                            <strong>Alert!</strong>
                                            <asp:Label ID="lblWUser" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:LinkButton ID="LinkButton27" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnlayplaneclose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div id="SuccessUser" runat="server" visible="false" class="alert alert-success alert-dismissible" role="alert">
                                        <div class="col-sm-11">
                                            <strong>Success!</strong>
                                            <asp:Label ID="lblSUser" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:LinkButton ID="LinkButton30" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnlayplaneclose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div id="AlertUser" runat="server" visible="false" class="alert alert-info alert-dismissible" role="alert">
                                        <strong>Alert!</strong>
                                        <asp:Label ID="lblUAlert" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-4  buttonRow">
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-3 paddingRight0">
                                        <asp:LinkButton ID="btnAddNew" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="btnAddNew_Click" OnClientClick="Confirm()">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Add New
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-3 paddingRight0">
                                        <asp:LinkButton ID="btnUpdateAdvnDet" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="btnUpdateAdvnDet_Click" OnClientClick="Confirm()">
                                        <span class="glyphicon glyphicon-edit" aria-hidden="true"></span>Update
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:LinkButton ID="lblUClear" runat="server" CssClass="floatRight" OnClick="lblUClear_Click" OnClientClick="ClearConfirm()">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-5">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">User Details</div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    User ID
                                                </div>
                                                <div class="col-sm-8 paddingRight5">
                                                    <asp:TextBox ID="txtID" CausesValidation="false" AutoPostBack="true" runat="server" class="form-control" OnTextChanged="txtID_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="ImgbtnUID" runat="server" OnClick="ImgbtnUID_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Full Name
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtName" AutoPostBack="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Description
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtDescription" TextMode="MultiLine" AutoPostBack="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Password
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtPW" AutoPostBack="true" CssClass="form-control" runat="server" TextMode="Password" OnTextChanged="txtPW_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Confirm Password
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtCPW" AutoPostBack="true" CssClass="form-control" runat="server" TextMode="Password" OnTextChanged="txtCPW_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height22">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height22">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4 paddingLeft0">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">Advance user details </div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Designation
                                                </div>
                                                <div class="col-sm-8 paddingRight5">
                                                    <asp:TextBox ID="txtCate" AutoPostBack="false" CssClass="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="ImgbtnDesignation" runat="server" OnClick="ImgbtnDesignation_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Department
                                                </div>
                                                <div class="col-sm-8 paddingRight5">
                                                    <asp:TextBox ID="txtDept" AutoPostBack="false" CssClass="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="ImgbtnDept" runat="server" CausesValidation="false" OnClick="ImgbtnDept_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Employe ID
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtEmpID" AutoPostBack="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5 ">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Emp. Code
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtEmpCode" AutoPostBack="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5 ">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Domain User
                                                </div>
                                                <div class="col-sm-2 paddingRight0 height22">
                                                    <asp:CheckBox ID="chkIsDomain" runat="server" />
                                                </div>
                                                <div class="col-sm-4 labelText1">
                                                    Windows Authenticate
                                                </div>
                                                <div class="col-sm-3 height22">
                                                    <asp:CheckBox ID="chkIsWinAuth" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Domain ID
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtDomainID" AutoPostBack="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5 ">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    SUN User ID
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtSunUserID" AutoPostBack="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5 ">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3 paddingLeft0">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">Domain Details</div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Name
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="lblDName" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Title
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="lblDTitle" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Department
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="lblDDept" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height22 ">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height22 ">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height22 ">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height22 ">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height22 ">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-5">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">User Contacts </div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    E- mail
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtEMail" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Mobile No.
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtMobile" CausesValidation="false" AutoPostBack="true" MaxLength="10" CssClass="form-control" runat="server" OnTextChanged="txtMobile_TextChanged" onkeypress="filterDigits(event)"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Phone No.
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtPhone" AutoPostBack="true" MaxLength="10" CssClass="form-control" runat="server" OnTextChanged="txtPhone_TextChanged1"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height22">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4 paddingLeft0">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">Status</div>
                                        <div class="panel-body">
                                            <div class="row height22">
                                                <div class="col-sm-8">
                                                    <div class="col-sm-1 padding0">
                                                        <asp:RadioButton ID="optActive" Checked="true" runat="server" GroupName="Ststuse" />
                                                    </div>
                                                    <div class="col-sm-3 labelText2">
                                                        Active
                                                    </div>
                                                    <div class="col-sm-1 padding0">
                                                        <asp:RadioButton ID="optInactive" runat="server" GroupName="Ststuse" />
                                                    </div>
                                                    <div class="col-sm-3 labelText2">
                                                        Inactive
                                                    </div>
                                                    <div class="col-sm-1 padding0">
                                                        <asp:RadioButton ID="optLock" runat="server" GroupName="Ststuse" />
                                                    </div>
                                                    <div class="col-sm-3 labelText2">
                                                        Locked
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row height22">
                                                <div class="col-sm-8">
                                                    <div class="col-sm-1 padding0">
                                                        <asp:RadioButton ID="optDisable" runat="server" GroupName="Ststuse" />
                                                    </div>
                                                    <div class="col-sm-10 labelText2">
                                                        Permanently disable
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
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
                                            <div class="row height44">
                                                <div class="col-sm-12">
                                                    <asp:TextBox ID="txtDisableRmks" AutoPostBack="false" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3  paddingLeft0">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">Other Details</div>
                                        <div class="panel-body">
                                            <div class="row" runat="server" visible="false">
                                                <div class="col-sm-10 labelText1">
                                                    PW Valid Period
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:TextBox ID="txtValid" AutoPostBack="false" Text="0" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-10 labelText1">
                                                    Allow user to change password
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:CheckBox ID="chkPWChange" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row" runat="server" visible="false">
                                                <div class="col-sm-10 labelText1">
                                                    Password Expire
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:CheckBox ID="chkPWExpire" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-10 labelText1">
                                                    User must change password at next login
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:CheckBox ID="chkMustChange" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height22">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height22">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="tab-pane " id="Assign_Company">
                    <asp:ScriptManagerProxy ID="ScriptManagerProxy4" runat="server"></asp:ScriptManagerProxy>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-sm-6  buttonrow">
                                    <div id="tests" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">
                                        <div class="col-sm-11">
                                            <strong>Alert!</strong>
                                            <asp:Label ID="AlertID" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:LinkButton ID="LinkButton24" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnlayplaneclose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div id="DivSuccess" runat="server" visible="false" class="alert alert-success alert-dismissible" role="alert">
                                        <div class="col-sm-11">
                                            <strong>Success!</strong>
                                            <asp:Label ID="lblSuccess" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:LinkButton ID="LinkButton26" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnlayplaneclose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6  buttonRow">
                                    <div class="col-sm-5">
                                    </div>
                                    <div class="col-sm-3 paddingRight0">
                                        <asp:LinkButton ID="LinkButton1" CausesValidation="false" runat="server" OnClick="btnAddNewCom_Click" OnClientClick="Confirm()">
                                                        <span class="glyphicon glyphicon-save" aria-hidden="true"></span>AddNew/Update
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:LinkButton ID="btnDeleteCom" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="btnDeleteCom_Click" OnClientClick="DeleteConfirm()">
                                        <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>Delete
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false" CssClass="floatRight" OnClick="lblUClear_Click" OnClientClick="ClearConfirm()">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-5">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">User Details</div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    User ID
                                                </div>
                                                <div class="col-sm-8 paddingRight5">
                                                    <asp:TextBox runat="server" CausesValidation="false" ID="txtUser" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtUser_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="LinkButton9" CausesValidation="false" runat="server" OnClick="ImgbtnUID_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Full Name
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtFullName" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Description
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtDesn" ReadOnly="true" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Designation
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtCat" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Department
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtDept_" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Emp. ID
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtEmpID_" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height22">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height22">
                                                </div>
                                            </div>
                                             <div class="row">
                                                <div class="col-sm-12 height10">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-7 paddingLeft0">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">Company Details</div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Company
                                                </div>
                                                <div class="col-sm-5 paddingRight5">
                                                    <asp:TextBox ID="txtCom_C" CausesValidation="false" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtCom_C_TextChanged" ></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="ImgbtnCompany" CausesValidation="false" runat="server" OnClick="ImgbtnCompany_Click" >
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>

                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5 ">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Description
                                                </div>
                                                <div class="col-sm-5 paddingRight5">
                                                    <asp:TextBox ID="txtComDesc_C" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Default Company
                                                </div>
                                                <div class="col-sm-1 paddingRight0 height22">
                                                    <asp:CheckBox ID="chkDefault" runat="server" />
                                                </div>
                                                <div class="col-sm-2 labelText1">
                                                    Active
                                                </div>
                                                <div class="col-sm-1 height22">
                                                    <asp:CheckBox ID="chkActive" Checked="true" runat="server" />
                                                </div>
                                            </div>
                                           
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-7 paddingLeft0">
                                    <div class="panel panel-default">

                                        <div class="panel-body panelscollbar height140">
                                            <asp:GridView ID="grdUserComp" EmptyDataText="No data found..." DataKeyNames="SEC_COM_CD" ShowHeaderWhenEmpty="True" CssClass="table table-hover table-striped" runat="server" GridLines="None" AllowPaging="true" PageSize="2" OnPageIndexChanging="grdUserComp_PageIndexChanging" PagerStyle-CssClass="cssPager">

                                                <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ChkUserComr" runat="server" Width="20px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Company Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CompanyCode" runat="server" Text='<%# Bind("SEC_COM_CD") %>' Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="mc_desc" runat="server" Text='<%# Bind("mc_desc") %>' Width="230px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Is Default" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="SEC_DEF_COMCD" runat="server" Checked='<%#Convert.ToBoolean(Eval("SEC_DEF_COMCD")) %>' Width="100px" />

                                                        </ItemTemplate>
                                                        <%-- <EditItemTemplate>
                                                                                            <asp:CheckBox ID="SEC_DEF_COMCD" runat="server" Checked='<%# Bind("SEC_DEF_COMCD") %>' />
                                                                                        </EditItemTemplate>--%>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Active" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="SEC_ACT" runat="server" Checked='<%#Convert.ToBoolean(Eval("SEC_ACT")) %>' Width="80px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="LinkButton1" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="tab-pane " id="Assign_Role">
                    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-sm-6  buttonrow">
                                    <div id="errorDiv" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">
                                        <div class="col-sm-11">
                                            <strong>Alert!</strong>
                                            <asp:Label ID="lblWarn" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:LinkButton ID="LinkButton21" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnlayplaneclose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>

                                    </div>
                                    <div id="successDiv" runat="server" visible="false" class="alert alert-success alert-dismissible" role="alert">
                                        <div class="col-sm-11">
                                            <strong>Success!</strong>
                                            <asp:Label ID="Label2" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:LinkButton ID="LinkButton23" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnlayplaneclose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6  buttonRow">
                                    <div class="col-sm-5">
                                    </div>
                                    <div class="col-sm-3 paddingRight0">
                                        <asp:LinkButton ID="btnAddNewRole" runat="server" CausesValidation="false" CssClass="floatRight" OnClick="btnAddNewRole_Click" OnClientClick="Confirm()">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>AddNew/Update
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-2 paddingRight0">
                                        <asp:LinkButton ID="btnDeleteRole" runat="server" CausesValidation="false" CssClass="floatRight" OnClick="btnDeleteRole_Click" OnClientClick="DeleteConfirm()">
                                        <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>Delete
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:LinkButton ID="LinkButton16" runat="server" CausesValidation="false" CssClass="floatRight" OnClick="lblUClear_Click" OnClientClick="ClearConfirm()">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                        </asp:LinkButton>
                                    </div>
                                </div>

                            </div>
                            <div class="col-sm-5">
                                <div class="panel panel-default">
                                    <div class="panel-heading">User Details</div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                User ID
                                            </div>
                                            <div class="col-sm-8 paddingRight5">
                                                <asp:TextBox runat="server" ID="txtUser_R" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtUser_R_TextChanged"></asp:TextBox>
                                            </div>

                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="LinkButton17" runat="server" OnClick="ImgbtnUID_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Full Name
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtFullName_R" ReadOnly="true" AutoPostBack="false" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Description
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtDesn_R" ReadOnly="true" TextMode="MultiLine" AutoPostBack="false" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Designation
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtCat_R" ReadOnly="true" AutoPostBack="false" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Department
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtDept_R" ReadOnly="true" AutoPostBack="false" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Emp. ID
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtEmpID_R" ReadOnly="true" AutoPostBack="false" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height22">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height22">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height22">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-5 paddingLeft0">
                                <div class="panel panel-default">
                                    <div class="panel-heading">Role Details</div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Company
                                            </div>
                                            <div class="col-sm-5 paddingRight5">
                                                <asp:TextBox ID="txtCom_R" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtCom_R_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="LinkButton18" runat="server" OnClick="ImgbtnCompany_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5 ">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Role
                                            </div>
                                            <div class="col-sm-5 paddingRight5">
                                                <asp:TextBox ID="txtRoleID" AutoPostBack="false" CssClass="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="LinkButton19" runat="server" OnClick="LinkButton19_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5 ">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Description
                                            </div>
                                            <div class="col-sm-7 paddingRight5">
                                                <asp:TextBox ID="txtRoleDesn" AutoPostBack="false" TextMode="MultiLine" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-7 paddingLeft0">
                                <div class="panel panel-default">

                                    <div class="panel-body panelscollbar height140">
                                        <asp:GridView ID="gvUserRole" EmptyDataText="No data found..." DataKeyNames="SERM_ROLE_ID" ShowHeaderWhenEmpty="True" CssClass="table table-hover table-striped" AllowPaging="true"  PageSize="2" runat="server" GridLines="None" OnRowDataBound="gvUserRole_RowDataBound" PagerStyle-CssClass="cssPager" OnPageIndexChanging="gvUserRole_PageIndexChanging" >
                                            <Columns>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ChkUserRole" runat="server" Width="20px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Company">
                                                    <ItemTemplate>
                                                        <asp:Label ID="SERM_COM_CD" runat="server" Text='<%# Bind("SERM_COM_CD") %>' Width="200px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="SERM_USR_ID" runat="server" Text='<%# Bind("SERM_USR_ID") %>' Width="5px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Role ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="SERM_ROLE_ID" runat="server" Text='<%# Bind("SERM_ROLE_ID") %>' Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Role Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ssrr_rolename" runat="server" Text='<%# Bind("ssrr_rolename") %>' Width="230px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="tab-pane " id="SBU">
                    <asp:ScriptManagerProxy ID="ScriptManagerProxy11" runat="server"></asp:ScriptManagerProxy>
                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-sm-6  buttonrow">
                                    <div id="SBUerror" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">
                                        <div class="col-sm-11">
                                            <strong>Alert!</strong>
                                            <asp:Label ID="lblSBUerror" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:LinkButton ID="LinkButton15" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnlayplaneclose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>

                                    </div>
                                    <div id="SBUSuccess" runat="server" visible="false" class="alert alert-success alert-dismissible" role="alert">
                                        <div class="col-sm-11">
                                            <strong>Success!</strong>
                                            <asp:Label ID="lblSBUSuccess" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:LinkButton ID="LinkButton20" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnlayplaneclose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-sm-6  buttonRow">
                                    <div class="col-sm-5">
                                    </div>
                                    <div class="col-sm-3 paddingRight0">
                                        <asp:LinkButton ID="lbtnSaveSBu" CausesValidation="false" runat="server" OnClientClick="Confirm()" OnClick="lbtnSaveSBu_Click">
                                                        <span class="glyphicon glyphicon-save" aria-hidden="true"></span>AddNew/Update
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:LinkButton ID="lbtnDeleteSbu" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="DeleteConfirm()" OnClick="lbtnDeleteSbu_Click">
                                        <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>Delete
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:LinkButton ID="LinkButton5" runat="server" CausesValidation="false" CssClass="floatRight" OnClick="lblUClear_Click" OnClientClick="ClearConfirm()">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-5">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">User Details</div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    User ID
                                                </div>
                                                <div class="col-sm-8 paddingRight5">
                                                    <asp:TextBox runat="server" CausesValidation="false" ID="txtUserIdSBU" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtUserIdSBU_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="LinkButton6" CausesValidation="false" runat="server" OnClick="ImgbtnUID_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Full Name
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtFullNameSBU" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Description
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtDesSBU" ReadOnly="true" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Designation
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtDesigSBU" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Department
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtDepSBU" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Emp. ID
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtEmpIDSBU" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height22">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height22">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height60">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-7 paddingLeft0">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">Strategic Business Units</div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Company
                                                </div>
                                                <div class="col-sm-5 paddingRight5">
                                                    <asp:TextBox ID="txtCompanySBU" CausesValidation="false" AutoPostBack="true" CssClass="form-control" runat="server" OnTextChanged="txtCompanySBU_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnSBUCompany" CausesValidation="false" runat="server" OnClick="lbtnSBUCompany_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>

                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5 ">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Description
                                                </div>
                                                <div class="col-sm-5 paddingRight5">
                                                    <asp:TextBox ID="txtCDesSBU" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5 ">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    SBU Code
                                                </div>
                                                <div class="col-sm-5 paddingRight5">
                                                    <asp:TextBox ID="txtSbuCode" ReadOnly="false" AutoPostBack="true" MaxLength="5" CssClass="form-control" runat="server" OnTextChanged="txtSbuCode_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnSbu" CausesValidation="false" runat="server" OnClick="lbtnSbu_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>

                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5 ">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    SBU Description
                                                </div>
                                                <div class="col-sm-5 paddingRight5">
                                                    <asp:Label ID="txtSBUDes" runat="server" Text="Label"></asp:Label>
                                                    <%--<asp:TextBox ID="txtSBUDes" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>--%>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Default Company
                                                </div>
                                                <div class="col-sm-1 paddingRight0 height22">
                                                    <asp:CheckBox ID="chk_DefSbu" runat="server" />
                                                </div>
                                                <div class="col-sm-2 labelText1">
                                                    Active
                                                </div>
                                                <div class="col-sm-1 height22">
                                                    <asp:CheckBox ID="chk_ActSbu" Checked="true" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-7 paddingLeft0">
                                    <div class="panel panel-default">

                                        <div class="panel-body panelscollbar height140">
                                            <asp:GridView ID="grdSbu" EmptyDataText="No data found..." DataKeyNames="seo_ope_cd" ShowHeaderWhenEmpty="True" runat="server" GridLines="None" AllowPaging="true" PageSize="2" PagerStyle-CssClass="cssPager" CssClass="table table-hover table-striped" AutoGenerateColumns="false" OnPageIndexChanging="grdSbu_PageIndexChanging">
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chk_sbu" runat="server" Width="20px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Company">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_com_cd" runat="server" Text='<%# Bind("seo_com_cd") %>' Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SBU CODE">
                                                        <ItemTemplate>
                                                            <asp:Label ID="col_sbucode" runat="server" Text='<%# Bind("seo_ope_cd") %>' Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Is Default">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="col_chkDef" Enabled="true" runat="server" Checked='<%#Convert.ToBoolean(Eval("seo_def_opecd")) %>' Width="100px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Active">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="col_chkAct" runat="server" Checked='<%#Convert.ToBoolean(Eval("seo_act")) %>' Width="80px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="LinkButton1" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="tab-pane " id="Assign_Location">
                    <asp:ScriptManagerProxy ID="ScriptManagerProxy5" runat="server"></asp:ScriptManagerProxy>
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-sm-6  buttonrow">
                                    <div id="WarnningLoc" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">
                                        <div class="col-sm-11">
                                            <strong>Alert!</strong>
                                            <asp:Label ID="lblWLoc" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:LinkButton ID="LinkButton11" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnlayplaneclose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>

                                    </div>
                                    <div id="SuccessLoc" runat="server" visible="false" class="alert alert-success alert-dismissible" role="alert">
                                        <div class="col-sm-11">
                                            <strong>Success!</strong>
                                            <asp:Label ID="lblSuccLoc" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:LinkButton ID="LinkButton14" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnlayplaneclose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6  buttonRow">
                                    <div class="col-sm-6">
                                    </div>
                                    <div class="col-sm-2 paddingRight0">
                                        <asp:LinkButton ID="btnLocationSave" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="btnLocationSave_Click" OnClientClick="Confirm()">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Add New
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-2 paddingRight0">
                                        <asp:LinkButton ID="btnLocationDete" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="btnLocationDete_Click" OnClientClick="DeleteConfirm()">
                                        <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>Delete
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:LinkButton ID="LinkButton28" CausesValidation="false" OnClick="lblUClear_Click" runat="server" CssClass="floatRight" OnClientClick="ClearConfirm()">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-5 height200">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">User Details</div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    User ID
                                                </div>
                                                <div class="col-sm-8 paddingRight5">
                                                    <asp:TextBox runat="server" ID="txtUser_L" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtUser_L_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="LinkButton29" runat="server" OnClick="ImgbtnUID_Click" CausesValidation="false">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Full Name
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtFullName_L" ReadOnly="true" AutoPostBack="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Description
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtDesn_L" ReadOnly="true" TextMode="MultiLine" AutoPostBack="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Designation
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtCat_L" ReadOnly="true" AutoPostBack="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Department
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtDept_L" ReadOnly="true" AutoPostBack="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Emp. ID
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtEmpID_L" ReadOnly="true" AutoPostBack="false" CssClass="form-control" runat="server"></asp:TextBox>
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
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <uc1:ucLoactionSearch runat="server" ID="ucLoactionSearch" />
                                </div>
                                <div class="col-sm-1 Lwidth">
                                    <asp:LinkButton ID="btnAddLocs" runat="server" OnClick="btnAddLocs_Click">
                                                        <span class="glyphicon glyphicon-triangle-right" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-4 Dwidth">
                                    <div class="panel panel-default">
                                        <div class="panel-heading"></div>
                                        <div class="panel-body panelscoll">
                                            <asp:GridView ID="grvLocs" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" DataKeyNames="LOCATION" CausesValidation="false" runat="server" AllowPaging="false" PageSize="2" GridLines="None" PagerStyle-CssClass="cssPager" CssClass="table table-hover table-striped" OnPageIndexChanging="grvLocs_PageIndexChanging" OnDataBound="grvLocs_DataBound" OnRowDataBound="grvLocs_RowDataBound">
                                                <Columns>

                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <HeaderTemplate>
                                                                                    <asp:CheckBox ID="allchk" runat="server"  Width="10px" onclick="CheckAll(this)"></asp:CheckBox>
                                                                                </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="selectchk" runat="server" Width="10px"></asp:CheckBox>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Set As Default" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="Is_default" runat="server" Width="5px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LOCATION" runat="server" Text='<%# Bind("LOCATION") %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LOC_DESCRIPTION" runat="server" Text='<%# Bind("LOC_DESCRIPTION") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="company">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="l_SEL_COM_CD" runat="server" Text='<%# Bind("SEL_COM_CD") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>--%>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="panel panel-default">

                                        <div class="panel-body panelscoll">
                                            <asp:GridView ID="gvUserLoc" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" DataKeyNames="SEL_LOC_CD" CssClass="table table-hover table-striped" ScrollBars="Both" CausesValidation="false" runat="server" AllowPaging="True" GridLines="None" AllowSorting="True" PageSize="2" OnPageIndexChanging="gvUserLoc_PageIndexChanging" PagerStyle-CssClass="cssPager">

                                                <Columns>

                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="allchk" runat="server" Width="5px" onclick="CheckAll2(this)"></asp:CheckBox>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="selectchkU" runat="server" Width="5px"></asp:CheckBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Company Code" HeaderStyle-Width="150px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SEL_COM_CD" runat="server" Text='<%# Bind("SEL_COM_CD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Location" HeaderStyle-Width="100px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SEL_LOC_CD" runat="server" Text='<%# Bind("SEL_LOC_CD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description" HeaderStyle-Width="800px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ml_loc_desc" runat="server" Text='<%# Bind("ml_loc_desc") %>' Width="800px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Is Default" >
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="SEL_DEF_LOCCD" runat="server" Checked='<%#Convert.ToBoolean(Eval("SEL_DEF_LOCCD")) %>' Width="10px" />
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
                <div class="tab-pane " id="Special_Permissions">
                    <asp:ScriptManagerProxy ID="ScriptManagerProxy6" runat="server"></asp:ScriptManagerProxy>
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-sm-6  buttonrow">
                                    <div id="WarnnPer" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">
                                        <div class="col-sm-11">
                                            <strong>Alert!</strong>
                                            <asp:Label ID="lblWper" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:LinkButton ID="LinkButton8" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnlayplaneclose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>


                                    </div>
                                    <div id="SuccesPer" runat="server" visible="false" class="alert alert-success alert-dismissible" role="alert">
                                        <div class="col-sm-11">
                                            <strong>Success!</strong>
                                            <asp:Label ID="lblSPer" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:LinkButton ID="LinkButton10" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnlayplaneclose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>


                                    </div>
                                </div>
                                <div class="col-sm-6  buttonRow">
                                    <div class="col-sm-6">
                                    </div>
                                    <div class="col-sm-2 paddingRight0">
                                        <asp:LinkButton ID="btnAddUserPerm" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="btnAddUserPerm_Click" OnClientClick="Confirm()">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Add New
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-2 paddingRight0">
                                        <asp:LinkButton ID="btnDeleteUserPerm" runat="server" CssClass="floatRight" OnClick="btnDeleteUserPerm_Click" OnClientClick="DeleteConfirm()">
                                        <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>Delete
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:LinkButton ID="LinkButton22" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lblUClear_Click" OnClientClick="ClearConfirm()">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                        </asp:LinkButton>
                                    </div>
                                </div>

                            </div>
                            <div class="col-sm-5">
                                <div class="panel panel-default">
                                    <div class="panel-heading">User Details</div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                User ID
                                            </div>
                                            <div class="col-sm-8 paddingRight5">
                                                <asp:TextBox runat="server" ID="txtUser_S" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtUser_S_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="btnSearchUser_S" runat="server" OnClick="ImgbtnUID_Click" CausesValidation="false">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Full Name
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtFullName_S" ReadOnly="true" AutoPostBack="false" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Description
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtDesn_S" ReadOnly="true" TextMode="MultiLine" AutoPostBack="false" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Designation
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtCat_S" ReadOnly="true" AutoPostBack="false" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Department
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtDept_S" ReadOnly="true" AutoPostBack="false" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Emp. ID
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtEmpID_S" ReadOnly="true" AutoPostBack="false" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height22">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height22">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-7 paddingLeft0">
                                <div class="panel panel-default">
                                    <div class="panel-heading">Company Details</div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Company
                                            </div>
                                            <div class="col-sm-5 paddingRight5">
                                                <asp:TextBox ID="txtCom_S"  CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtCom_S_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="btnSearchCom_S" runat="server" CausesValidation="false" OnClick="ImgbtnCompany_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Permission Code
                                            </div>
                                            <div class="col-sm-5 paddingRight5">
                                                <asp:TextBox ID="txtPermCd" AutoPostBack="true" CssClass="form-control" runat="server" OnTextChanged="txtPermCd_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="btnSearchPerm" CausesValidation="false" runat="server" OnClick="btnSearchPerm_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                            </div>
                                            <div class="col-sm-1 labelText1">
                                                Any
                                            </div>
                                            <div class="col-sm-1 paddingRight0 height22">
                                                <asp:RadioButton ID="rdoAnyParty" runat="server" AutoPostBack="true" GroupName="permission" OnCheckedChanged="rdoAnyParty_CheckedChanged" />
                                            </div>
                                            <div class="col-sm-1 labelText1">
                                                Location
                                            </div>
                                            <div class="col-sm-1 height22">
                                                <asp:RadioButton ID="rdoLoc" AutoPostBack="true" runat="server" GroupName="permission" OnCheckedChanged="rdoLoc_CheckedChanged" />
                                            </div>
                                            <div class="col-sm-1 labelText1">
                                                PC
                                            </div>
                                            <div class="col-sm-1 height22">
                                                <asp:RadioButton ID="rdoPC" AutoPostBack="true" runat="server" GroupName="permission" OnCheckedChanged="rdoPC_CheckedChanged" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                            </div>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtParty" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="btnSearchParty" runat="server" OnClick="btnSearchParty_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-7 paddingLeft0">
                                <div class="panel panel-default">

                                    <div class="panel-body panelscollbar height200">
                                        <asp:GridView ID="grdUserPerm" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" CssClass="table table-hover table-striped" GridLines="None" AllowPaging="true" runat="server">

                                            <Columns>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ChkPer" runat="server" Width="20px" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Company Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="SEUR_COM" runat="server" Text='<%# Bind("SEUR_COM") %>' Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Permission Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="SEUR_CD" runat="server" Text='<%# Bind("SEUR_CD") %>' Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Permission Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Per" runat="server" Text='<%# Bind("SEUP_USR_PERMDESC") %>' Width="200px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="LOC/PC">
                                                    <ItemTemplate>
                                                        <asp:Label ID="SEUR_PARTY" runat="server" Text='<%# Bind("SEUR_PARTY") %>' Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="tab-pane " id="Approve_Permissions">
                    <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>

                            <div class="row">
                                <div class="col-sm-6  buttonrow">
                                    <div id="AprovalWarning" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">
                                        <div class="col-sm-11">
                                            <strong>Alert!</strong>
                                            <asp:Label ID="lblWApp" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnlayplaneclose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div id="ApprovalSuccess" runat="server" visible="false" class="alert alert-success alert-dismissible" role="alert">
                                        <div class="col-sm-11">
                                            <strong>Success!</strong>
                                            <asp:Label ID="lblSApp" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:LinkButton ID="LinkButton7" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnlayplaneclose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6  buttonRow">
                                    <div class="col-sm-6">
                                    </div>
                                    <div class="col-sm-2 paddingRight0">
                                        <asp:LinkButton ID="btnAddAppPerm" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="btnAddAppPerm_Click" OnClientClick="Confirm()">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Add New
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-2 paddingRight0">
                                        <asp:LinkButton ID="btnDeleteApprPerm" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="btnDeleteApprPerm_Click" OnClientClick="DeleteConfirm()">
                                        <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>Delete
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:LinkButton ID="LinkButton25" CausesValidation="false" runat="server" CssClass="floatRight" OnClick="lblUClear_Click" OnClientClick="ClearConfirm()">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-5">
                                <div class="panel panel-default">
                                    <div class="panel-heading">User Details</div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                User ID
                                            </div>
                                            <div class="col-sm-8 paddingRight5">
                                                <asp:TextBox runat="server" ID="txtUser_A" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtUser_A_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="ImageUserA_A" runat="server" CausesValidation="false" OnClick="ImgbtnUID_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Full Name
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtFullName_A" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Description
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtDesn_A" ReadOnly="true" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Designation
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtCat_A" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Department
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtDept_A" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Emp. ID
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtEmpID_A" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-5 paddingLeft0">
                                <div class="panel panel-default">
                                    <div class="panel-heading">Approve Permission</div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Permission Code
                                            </div>
                                            <div class="col-sm-4 paddingRight5">
                                                <asp:TextBox runat="server" ID="txtAppr_Code" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtAppr_Code_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="btnSearchApprPermCode" CausesValidation="false" runat="server" OnClick="btnSearchApprPermCode_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Description
                                            </div>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtApprCdDesc" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Final Approval Level
                                            </div>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtFinApprLevel" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height5">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3 labelText1">
                                                Level Code
                                            </div>
                                            <div class="col-sm-5 paddingRight5">
                                                <asp:TextBox runat="server" ID="txtPermLvl" CssClass="form-control"  AutoPostBack="true" OnTextChanged="txtPermLvl_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0">
                                                <asp:LinkButton ID="btnSearchApprLvl" CausesValidation="false" runat="server" OnClick="btnSearchApprLvl_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height22">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 height22">
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
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-12">
                                <div class="panel panel-default">
                                    <div class="panel-body panelscollbar height120">
                                        <asp:GridView ID="grvApprLevel" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" CssClass="table table-hover table-striped" runat="server" GridLines="None" AllowPaging="true" AutoGenerateColumns="false">

                                            <Columns>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="col_Chk" runat="server" Width="80px" />

                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Permission Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="sart_cd" runat="server" Text='<%# Bind("sart_cd") %>' Width="120px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Main Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="sart_main_tp" runat="server" Text='<%# Bind("sart_main_tp") %>' Width="120px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Final Approval Level">
                                                    <ItemTemplate>
                                                        <asp:Label ID="sart_app_lvl" runat="server" Text='<%# Bind("sart_app_lvl") %>' Width="120px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="User Permission Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="saup_prem_cd" runat="server" Text='<%# Bind("saup_prem_cd") %>' Width="120px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="User Permission Level">
                                                    <ItemTemplate>
                                                        <asp:Label ID="sarp_app_lvl" runat="server" Text='<%# Bind("sarp_app_lvl") %>' Width="120px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Active" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="Col_Act" runat="server" Checked='<%#Convert.ToBoolean(Eval("saup_act")) %>' Width="80px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="tab-pane " id="Assign_Profit_Center">
                    <asp:ScriptManagerProxy ID="ScriptManagerProxy8" runat="server"></asp:ScriptManagerProxy>
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>


                            <div class="row">
                                <div class="col-sm-6  buttonrow">
                                    <div id="WarrningPro" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">

                                        <div class="col-sm-11">
                                            <strong>Alert!</strong>
                                            <asp:Label ID="lblWPro" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:LinkButton ID="lbtnlayplaneclose" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnlayplaneclose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div id="SuccessPro" runat="server" visible="false" class="alert alert-success alert-dismissible" role="alert">
                                        <div class="col-sm-11">
                                            <strong>Success!</strong>
                                            <asp:Label ID="lblSuPro" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnlayplaneclose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6  buttonRow">
                                    <div class="col-sm-6">
                                    </div>
                                    <div class="col-sm-2 paddingRight0">
                                        <asp:LinkButton ID="btn_AddPC" runat="server" CssClass="floatRight" OnClick="btn_AddPC_Click" OnClientClick="Confirm()"> <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Add New </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-2 paddingRight0">
                                        <asp:LinkButton ID="btn_DelPC" runat="server" CssClass="floatRight" OnClick="btn_DelPC_Click" OnClientClick="DeleteConfirm()"> <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>Delete </asp:LinkButton>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:LinkButton ID="LinkButton12" runat="server" CssClass="floatRight" OnClick="lblUClear_Click" OnClientClick="ClearConfirm()"> <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear </asp:LinkButton>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-5">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">User Details</div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    User ID
                                                </div>
                                                <div class="col-sm-8 paddingRight5">
                                                    <asp:TextBox runat="server" ID="txtUser_P" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtUser_P_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="LinkButton13" OnClick="ImgbtnUID_Click" runat="server"> <span class="glyphicon glyphicon-search" aria-hidden="true"></span></asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Full Name
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtFullName_P" ReadOnly="true" AutoPostBack="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Description
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtDesn_P" ReadOnly="true" TextMode="MultiLine" AutoPostBack="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Designation
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtCat_P" ReadOnly="true" AutoPostBack="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Department
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtDept_P" ReadOnly="true" AutoPostBack="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Emp. ID
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtEmpID_P" ReadOnly="true" AutoPostBack="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height20">
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <uc1:ucProfitCenterSearch runat="server" ID="ucProfitCenterSearch" />
                                </div>
                                <div class="col-sm-1 Lwidth">
                                    <asp:LinkButton ID="btnAddPc" runat="server" OnClick="btnAddPc_Click"> <span class="glyphicon glyphicon-triangle-right" aria-hidden="true"></span></asp:LinkButton>
                                </div>
                                <div class="col-sm-4 Dwidth">
                                    <div class="panel panel-default">
                                        <div class="panel-heading"></div>
                                        <div class="panel-body panelscoll">
                                            <asp:GridView ID="grvPCs" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" CausesValidation="false" DataKeyNames="PC_DESCRIPTION" runat="server" AllowPaging="false" GridLines="None" CssClass="table table-hover table-striped" PageSize="2" OnPageIndexChanging="grvPCs_PageIndexChanging" PagerStyle-CssClass="cssPager">
                                                <Columns>

                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <HeaderTemplate>
                                                                                    <asp:CheckBox ID="allchk" runat="server" Text="all" Width="10px" onclick="CheckAll(this)"></asp:CheckBox>
                                                                                </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkPr" runat="server" Width="10px"></asp:CheckBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Set As Default" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="Is_defPC" runat="server" Width="5px" />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="PROFIT_CENTER" runat="server" Text='<%# Bind("PROFIT_CENTER") %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="PC_DESCRIPTION" runat="server" Text='<%# Bind("PC_DESCRIPTION") %>'  Width="500px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- <asp:TemplateField HeaderText="company">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="l" runat="server" Text='<%# Bind("SEL_COM_CD") %>' Width="100px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>--%>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="panel panel-default">
                                        <div class="panel-heading"></div>
                                        <div class="panel-body panelscoll">
                                            <asp:GridView ID="gvUserPC" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" DataKeyNames="mpc_desc" CssClass="table table-hover table-striped" ScrollBars="Both" CausesValidation="false" runat="server" AllowPaging="True" GridLines="None" AllowSorting="True" PageSize="2" OnPageIndexChanging="gvUserPC_PageIndexChanging" PagerStyle-CssClass="cssPager">

                                                <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="allchkPC" runat="server" Width="10px" onclick="CheckAllPro(this)"></asp:CheckBox>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkDelPc" runat="server" Width="10px"></asp:CheckBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Company Code" HeaderStyle-Width="150px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="p_SUP_COM_CD" runat="server" Text='<%# Bind("SUP_COM_CD") %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Profit Center" HeaderStyle-Width="100px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="p_SUP_PC_CD" runat="server" Text='<%# Bind("SUP_PC_CD") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description" HeaderStyle-Width="800px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="mpc_desc" runat="server" Text='<%# Bind("mpc_desc") %>' Width="500px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Is Default">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="p_SUP_DEF_PCCD" runat="server" Checked='<%#Convert.ToBoolean(Eval("SUP_DEF_PCCD")) %>' Width="80px" />

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

            </div>
        </div>
    </div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserPopoup" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="testPanel" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>

    <%-- Style="display: none"--%>
    <asp:Panel runat="server" ID="testPanel" DefaultButton="ImageSearch">
        <div runat="server" id="test" class="panel panel-default height400 width700">
            <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
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
                            <div class="col-sm-12">
                                <div class="col-sm-2 labelText1">
                                    Search by key
                                </div>
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy10" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                    <ContentTemplate>
                                        <div class="col-sm-3 paddingRight5">
                                            <asp:DropDownList ID="cmbSearchbykey" runat="server" class="form-control">
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
                                            <asp:LinkButton ID="ImageSearch" runat="server" OnClick="ImageSearch_Click">
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
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="dvResultUser" CausesValidation="false" runat="server" OnSelectedIndexChanged="dvResultUser_SelectedIndexChanged" AllowPaging="True" OnPageIndexChanging="dvResultUser_PageIndexChanging" GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager">
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
            <%-- </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="txtSearchbyword" />
                </Triggers>

            </asp:UpdatePanel>--%>
        </div>
    </asp:Panel>


</asp:Content>
