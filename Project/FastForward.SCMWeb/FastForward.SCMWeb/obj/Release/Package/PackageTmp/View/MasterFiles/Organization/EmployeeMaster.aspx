<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="EmployeeMaster.aspx.cs" Inherits="FastForward.SCMWeb.View.MasterFiles.Organization.EmployeeMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script>
        function SaveConfirm() {
            var selectedvalue = confirm("Do you want to save data?");
            if (selectedvalue) {
                document.getElementById('<%=txtSaveconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtSaveconformmessageValue.ClientID %>').value = "No";
            }
        };

    </script>
    <script type="text/javascript">
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

        function Confirm() {
            var selectedvalue = confirm("Do you want to Update ?");
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

        <%--        function toggle(obj) {
            if (obj.value == "optMale") {
                document.getElementById('<%=optFemale.ClientID%>').checked = false;
            }
            else if (obj.value == "optFemale") {
                document.getElementById('<%=optMale.ClientID%>').checked = false;
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
            <asp:UpdatePanel ID="pnlTopPanel" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-sm-8  buttonrow">
                            <div id="WarningEmployee" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">
                                <div class="col-sm-11">
                                    <strong>Alert!</strong>
                                    <asp:Label ID="lblWEmployee" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-1">
                                    <asp:LinkButton ID="lbtnLinkButton27" runat="server" CausesValidation="false" CssClass="floatright">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div id="SuccessEmployee" runat="server" visible="false" class="alert alert-success alert-dismissible" role="alert">
                                <div class="col-sm-11">
                                    <strong>Success!</strong>
                                    <asp:Label ID="lblSEmployee" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-1">
                                    <asp:LinkButton ID="lbtnLinkButton30" runat="server" CausesValidation="false" CssClass="floatright">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div id="AlertEmployee" runat="server" visible="false" class="alert alert-info alert-dismissible" role="alert">
                                <strong>Alert!</strong>
                                <asp:Label ID="lblEAlert" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-4 buttonRow">
                            <div class="col-sm-3" style="color:red">
                                  <asp:Label ID="lbactive" runat="server"></asp:Label>
                            </div>
                            <div class="col-sm-3 paddingRight0">
                                <asp:LinkButton ID="lbtnAddNewEmp" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="SaveConfirm()" OnClick="lbtnAddNewEmp_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Add New
                                </asp:LinkButton>
                            </div>
                            <div class="col-sm-3 paddingRight0">
                                <asp:LinkButton ID="lbtnUpdateEmp" CausesValidation="false" runat="server" CssClass="floatRight" OnClientClick="Confirm()" OnClick="lbtnUpdateEmp_Click">
                                        <span class="glyphicon glyphicon-edit" aria-hidden="true"></span>Update
                                </asp:LinkButton>
                            </div>
                            <div class="col-sm-3">
                                <asp:LinkButton ID="lbtnClearEmp" runat="server" CssClass="floatRight" OnClick="lbtnClearEmp_Click" OnClientClick="ClearConfirm()">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>

                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <strong><b>Employee Profile</b></strong>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-5">
                                    <div class="panel panel-default">
                                        <div class="panel-heading height22">
                                            <strong><b>Personal Details</b></strong>
                                        </div>
                                        <div class="panel-body">

                                            <div class="row">

                                                <div class="col-sm-3 labelText1">
                                                     Employee Code
                                                </div>
                                                <div class="col-sm-2 paddingRight5">
                                                    <asp:TextBox ID="txtEPFNo" CausesValidation="false" runat="server" AutoPostBack="true" class="form-control" Style="text-transform: uppercase" TabIndex="100" OnTextChanged="txtEPFNo_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                    <asp:LinkButton ID="lbtnEPFNo" runat="server" CausesValidation="false" OnClick="lbtnEPFNo_Click" TabIndex="101">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>

                                                <div class="col-sm-2 labelText1">
                                                    EPF No 
                                                </div>
                                                <div class="col-sm-2 paddingRight5">
                                                    <asp:TextBox ID="txtEmpCode" AutoPostBack="true" CssClass="form-control" runat="server" TabIndex="102" OnTextChanged="txtEmpCode_TextChanged" Style="text-transform: uppercase"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnEmpCode" runat="server" CausesValidation="false" OnClick="lbtnEmpCode_Click" TabIndex="103">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                                  <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnepfupdate" runat="server" CausesValidation="false" OnClick="lbtnepfupdate_Click" >
                                                        <span class="glyphicon glyphicon-upload" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Title
                                                </div>
                                                <div class="col-sm-2 paddingRight5">

                                                    <%-- <div class="col-sm-8">
                                                        <div class="col-sm-1 padding0">
                                                            <asp:RadioButton ID="optMr" Checked="true" runat="server" GroupName="Ttle" TabIndex="104" />
                                                        </div>
                                                        <div class="col-sm-3 labelText2">
                                                            Mr.
                                                        </div>
                                                        <div class="col-sm-1 padding0">
                                                            <asp:RadioButton ID="optMrs" runat="server" GroupName="Ttle" TabIndex="105" />
                                                        </div>
                                                        <div class="col-sm-3 labelText2">
                                                            Mrs.
                                                        </div>
                                                        <div class="col-sm-1 padding0">
                                                            <asp:RadioButton ID="optMs" runat="server" GroupName="Ttle" TabIndex="106" />
                                                        </div>
                                                        <div class="col-sm-3 labelText2">
                                                            Ms.
                                                        </div>
                                                    </div>--%>
                                                    <asp:DropDownList ID="ddlTitle" CausesValidation="false" runat="server" CssClass="form-control" TabIndex="104">
                                                        <asp:ListItem Text="Mr." Selected="True" Value="Mr." />
                                                        <asp:ListItem Text="Ms." Value="Ms." />
                                                        <asp:ListItem Text="Mrs." Value="Mrs." />
                                                        <asp:ListItem Text="Miss." Value="Miss." />
                                                        <asp:ListItem Text="Dr." Value="Dr." />
                                                        <asp:ListItem Text="Rev." Value="Rev." />
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-1 labelText1">
                                                </div>
                                                <div class="col-sm-2 labelText1">
                                                    Sex
                                                </div>
                                                <div class="col-sm-3 paddingRight5">
                                                    <%--<div class="col-sm-1 padding0">
                                                            <asp:RadioButton ID="optMale" Checked="true" name="gender" runat="server" TabIndex="110" AutoPostBack="true" onClick="toggle(this)" />
                                                        </div>
                                                        <div class="col-sm-3 labelText2">
                                                            Male
                                                        </div>
                                                        <div class="col-sm-1 padding0">
                                                            <asp:RadioButton ID="optFemale" runat="server" name="gender" TabIndex="111" AutoPostBack="true" onClick="toggle(this)" />
                                                        </div>
                                                        <div class="col-sm-3 labelText2">
                                                            Female
                                                        </div>--%>
                                                    <%--<div class="col-sm-1 padding0">
                                                            <asp:RadioButton ID="optOther" runat="server" GroupName="Gndr" />
                                                            </div>
                                                            <div class="col-sm-3 labelText2">
                                                                Other
                                                        </div>--%>
                                                    <asp:DropDownList ID="ddlSex" CausesValidation="false" runat="server" CssClass="form-control" TabIndex="105">
                                                        <asp:ListItem Text="Male" Selected="True" Value="M" />
                                                        <asp:ListItem Text="Female" Value="F" />
                                                    </asp:DropDownList>
                                                </div>
                                                
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    First Name
                                                </div>
                                                <div class="col-sm-3 paddingRight5">
                                                    <asp:TextBox runat="server" CausesValidation="false" ID="txtEmpFirstName" AutoPostBack="true" CssClass="form-control" TabIndex="106" Style="text-transform: uppercase" OnTextChanged="txtEmpFirstName_TextChanged"></asp:TextBox>
                                                </div>

                                                <div class="col-sm-2 labelText1">
                                                    Last Name
                                                </div>
                                                <div class="col-sm-3 paddingRight5">
                                                    <asp:TextBox runat="server" CausesValidation="false" ID="txtEmpLastName" AutoPostBack="true" CssClass="form-control" TabIndex="107" Style="text-transform: uppercase" OnTextChanged="txtEmpLastName_TextChanged"></asp:TextBox>
                                                </div>

                                            </div>

                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    Name with Initials
                                                </div>
                                                <div class="col-sm-8 paddingRight5">
                                                    <asp:TextBox runat="server" CausesValidation="false" ID="txtEmpNameInt" AutoPostBack="true" CssClass="form-control" Style="text-transform: uppercase" TabIndex="108" onkeydown="DetectTab(event)" OnTextChanged="txtEmpNameInt_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="row">
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-3 labelText1">
                                                    NIC
                                                </div>
                                                <div class="col-sm-3 paddingRight5">
                                                    <asp:TextBox runat="server" CausesValidation="false" ID="txtEmpNIC" AutoPostBack="true" CssClass="form-control" TabIndex="109" OnTextChanged="txtEmpNIC_TextChanged" Style="text-transform: uppercase"></asp:TextBox>
                                                </div>

                                                <div class="col-sm-2 labelText1">
                                                    Date of Birth
                                                </div>
                                                <div class="col-sm-3 paddingRight5">
                                                    <asp:TextBox runat="server" ReadOnly="true" ID="txtEmpDOB" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnEmpDOB" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true" tabindex="110"></span>
                                                    </asp:LinkButton>
                                                    <asp:CalendarExtender ID="cldEmpDOB" runat="server" TargetControlID="txtEmpDOB"
                                                        PopupButtonID="lbtnEmpDOB" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="panel panel-default">
                                        <div class="panel-heading height22">
                                            <strong><b>Employee Details</b></strong>
                                        </div>
                                        <div class="panel-body">

                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="col-sm-4 labelText1">
                                                        Manager
                                                    </div>
                                                    <div class="col-sm-7 paddingRight5">
                                                        <asp:TextBox ID="txtManager" AutoPostBack="true" CssClass="form-control" runat="server" TabIndex="111" Style="text-transform: uppercase" OnTextChanged="txtManager_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnManager" runat="server" CausesValidation="false" OnClick="lbtnManager_Click" TabIndex="112">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6">
                                                    <div class="col-sm-3 labelText1">
                                                        Contractor
                                                    </div>
                                                    <div class="col-sm-8 paddingRight5">
                                                        <asp:TextBox ID="txtContractor" AutoPostBack="true" CssClass="form-control" MaxLength="29" runat="server" CausesValidation="false" TabIndex="113" Style="text-transform: uppercase" OnTextChanged="txtContractor_TextChanged"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="col-sm-4 labelText1">
                                                        Designation
                                                    </div>
                                                    <div class="col-sm-7 paddingRight5">
                                                        <asp:TextBox ID="txtCategory" AutoPostBack="true" CssClass="form-control" runat="server" TabIndex="114" Style="text-transform: uppercase" OnTextChanged="txtCategory_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnCategory" runat="server" CausesValidation="false" OnClick="lbtnCategory_Click" TabIndex="115">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>

                                                <div class="col-sm-6">
                                                    <div class="col-sm-3 labelText1">
                                                        Department
                                                    </div>
                                                    <div class="col-sm-8 paddingRight5">
                                                        <asp:TextBox ID="txtDepProfit" AutoPostBack="true" CssClass="form-control" runat="server" TabIndex="116" Style="text-transform: uppercase" OnTextChanged="txtDepProfit_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnDepProfit" runat="server" CausesValidation="false" OnClick="lbtnDepProfit_Click" TabIndex="117">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">

                                                <div class="col-sm-6">
                                                    <div class="col-sm-4 labelText1">
                                                        Sub category
                                                    </div>
                                                    <div class="col-sm-7 paddingRight5">
                                                        <asp:TextBox ID="txtSubCat" AutoPostBack="true" CssClass="form-control" runat="server" TabIndex="118" Style="text-transform: uppercase" OnTextChanged="txtSubCat_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnSubCat" runat="server" CausesValidation="false" OnClick="lbtnSubCat_Click" TabIndex="119">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>

                                                <div class="col-sm-6">
                                                    <div class="col-sm-3 labelText1">
                                                        Immediate Supervisor
                                                    </div>
                                                    <div class="col-sm-8 paddingRight5">
                                                        <asp:TextBox ID="txtSupvsr" AutoPostBack="true" CssClass="form-control" runat="server" TabIndex="120" Style="text-transform: uppercase" OnTextChanged="txtSupvsr_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnSupvsr" runat="server" CausesValidation="false" OnClick="lbtnSupvsr_Click" TabIndex="121">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                </div>
                                <div class="col-sm-7 paddingLeft0">
                                    <div class="panel panel-default">
                                        <div class="panel-heading height22">
                                            <strong><b>Contact Details</b></strong>
                                        </div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-sm-2 labelText1">
                                                    E-mail
                                                </div>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtEmpEMail" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtEmpEMail_TextChanged" TabIndex="122"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2 labelText1">
                                                    Mobile No.
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:TextBox ID="txtEmpMobile" CausesValidation="false" AutoPostBack="true" MaxLength="10" CssClass="form-control" runat="server" OnTextChanged="txtEmpMobile_TextChanged" TabIndex="123"></asp:TextBox>
                                                </div>

                                                <div class="col-sm-2 labelText1">
                                                    Home Phone No.
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:TextBox ID="txtEmpHomePhone" AutoPostBack="true" MaxLength="10" CssClass="form-control" runat="server" OnTextChanged="txtEmpHomePhone_TextChanged" TabIndex="124"></asp:TextBox>
                                                </div>

                                                <div class="col-sm-2 labelText1">
                                                    Office Phone No.
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:TextBox ID="txtEmpOfficePhone" AutoPostBack="true" MaxLength="10" CssClass="form-control" runat="server" OnTextChanged="txtEmpOfficePhone_TextChanged" TabIndex="125"></asp:TextBox>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2 labelText1">
                                                    Police Station
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:TextBox ID="txtEmpPolice" AutoPostBack="true" MaxLength="30" CssClass="form-control" runat="server" TabIndex="126" Style="text-transform: uppercase" OnTextChanged="txtEmpPolice_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6 paddingRight5">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <div class="panel panel-default">
                                                                <div class="panel-heading height22">Permanant Address</div>
                                                                <div class="panel-body">
                                                                    <div class="row">
                                                                        <div class="col-sm-4 labelText1">
                                                                            Address <%--Line 1--%>
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight5">
                                                                            <asp:TextBox runat="server" CausesValidation="false" ID="txtEmpPAL1" AutoPostBack="true" CssClass="form-control" TabIndex="127" Style="text-transform: uppercase" OnTextChanged="txtEmpPAL1_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-4 labelText1">
                                                                            <%-- Address Line 2--%>
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight5">
                                                                            <asp:TextBox runat="server" CausesValidation="false" ID="txtEmpPAL2" AutoPostBack="true" CssClass="form-control" TabIndex="128" Style="text-transform: uppercase" OnTextChanged="txtEmpPAL2_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-4 labelText1">
                                                                            <%--Address Line 3--%>
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight5">
                                                                            <asp:TextBox runat="server" CausesValidation="false" ID="txtEmpPAL3" AutoPostBack="true" CssClass="form-control" TabIndex="129" Style="text-transform: uppercase" OnTextChanged="txtEmpPAL3_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 paddingLeft5">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <div class="panel panel-default">
                                                                <div class="panel-heading height22">Current Address</div>
                                                                <div class="panel-body">
                                                                    <div class="row">
                                                                        <div class="col-sm-4 labelText1">
                                                                            Address <%--Line 1--%>
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight5">
                                                                            <asp:TextBox runat="server" CausesValidation="false" ID="txtEmpCAL1" AutoPostBack="true" CssClass="form-control" TabIndex="130" Style="text-transform: uppercase" OnTextChanged="txtEmpCAL1_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-4 labelText1">
                                                                            <%--Address Line 2--%>
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight5">
                                                                            <asp:TextBox runat="server" CausesValidation="false" ID="txtEmpCAL2" AutoPostBack="true" CssClass="form-control" TabIndex="131" Style="text-transform: uppercase" OnTextChanged="txtEmpCAL2_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-4 labelText1">
                                                                            <%--Address Line 3--%>
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight5">
                                                                            <asp:TextBox runat="server" CausesValidation="false" ID="txtEmpCAL3" AutoPostBack="true" CssClass="form-control" TabIndex="132" Style="text-transform: uppercase" OnTextChanged="txtEmpCAL3_TextChanged"></asp:TextBox>
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
                                    <div class="col-sm-5 paddingLeft0">
                                        <div class="panel panel-default">
                                            <div class="panel-heading height22">
                                                <strong><b>Status</b></strong>
                                            </div>
                                            <div class="panel-body">
                                                <div class="row height22">
                                                    <div class="col-sm-8">
                                                        <div class="col-sm-1 padding0">
                                                            <asp:RadioButton ID="optActive" Checked="true" runat="server" TabIndex="133" GroupName="Ststuse" />
                                                        </div>
                                                        <div class="col-sm-3 labelText2">
                                                            Active
                                                        </div>
                                                        <div class="col-sm-1 padding0">
                                                            <asp:RadioButton ID="optInactive" runat="server" TabIndex="134" GroupName="Ststuse" />
                                                        </div>
                                                        <div class="col-sm-3 labelText2">
                                                            Inactive
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-7  paddingLeft0">
                                        <div class="panel panel-default">
                                            <div class="panel-heading height22">
                                                <strong><b>Other Details</b></strong>
                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-sm-5  paddingLeft0">
                                                        <div class="col-sm-8 labelText1">
                                                            Maintain Stock Value
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <asp:CheckBox ID="chkSVChange" runat="server" TabIndex="135" AutoPostBack="true" OnCheckedChanged="chkSVChange_CheckChange" />
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-7  paddingLeft0">
                                                        <div class="col-sm-6 labelText1">
                                                            Max Stock Value(<asp:Label runat="server" ID="Currency"></asp:Label>)
                                                        </div>
                                                        <div class="col-sm-4 paddingRight5">
                                                            <asp:TextBox ID="txtMaxSVal" onkeydown="return jsDecimals(event);" Style="text-align: right" AutoPostBack="true" OnTextChanged="txtMaxSVal_TextChanged" runat="server" CssClass="txtMaxSVal form-control" MaxLength="15" TabIndex="136"></asp:TextBox>
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
            <%--  <asp:UpdatePanel ID="pnlBottomPanel" runat="server">
                <ContentTemplate>--%>
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel panel-default marginLeftRight5">
                        <div class="panel-body">
                            <div class="bs-example">
                                <ul class="nav nav-tabs" id="myEmpTab">
                                    <li class="active" tabindex="137"><a href="#Employee_ProfitCenter" data-toggle="tab">Employee Profit Center</a></li>
                                    <li tabindex="149"><a href="#Employee_Location" data-toggle="tab">Employee Location</a></li>
                                    <li><a href="#Customer_Assign" data-toggle="tab">Assign Customer</a></li>
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
                                <div class="tab-pane active" id="Employee_ProfitCenter">
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="pnlBottomEmpPrftCntrPanel" runat="server">
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="pnlBottomEmpPrftCntrUPPanel" runat="server">
                                                <ContentTemplate>
                                                    <div class="panel panel-default">
                                                        <div class="panel-body">
                                                            <div class="col-sm-6 paddingRight0 width525">
                                                                <div class="col-sm-1 labelText1 paddingLeft0 paddingRight0 width70">
                                                                    Profit Center
                                                                </div>
                                                                <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                                    <asp:TextBox runat="server" ID="txtPCPrftCntr" AutoPostBack="true" CausesValidation="false" CssClass="form-control" TabIndex="138" OnTextChanged="txtPCPrftCntr_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft5 paddingRight15 width25">
                                                                    <asp:LinkButton ID="lbtnPCPrftCntr" runat="server" CausesValidation="false" OnClick="lbtnPCPrftCntr_Click" TabIndex="139">
                                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-1 labelText1 paddingLeft0 paddingRight0 width70">
                                                                    Assigned Date
                                                                </div>
                                                                <div class="col-sm-2 paddingRight5 width100">
                                                                    <asp:TextBox runat="server" ReadOnly="true" ID="txtPCAssDate" CausesValidation="false" CssClass="form-control" TabIndex="140"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 paddingRight5 width25">
                                                                    <asp:LinkButton ID="lbtnPCAssDate" runat="server" CausesValidation="false" TabIndex="141">
                                                                                                     <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                    <asp:CalendarExtender ID="cldPCAssdate" runat="server" TargetControlID="txtPCAssDate"
                                                                        PopupButtonID="lbtnPCAssDate" Format="dd/MMM/yyyy">
                                                                    </asp:CalendarExtender>
                                                                </div>
                                                                <div class="col-sm-2 labelText1 paddingRight0 width125">
                                                                    Representative Code
                                                                </div>
                                                                <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                                    <asp:TextBox runat="server" ID="txtPCRptCode" AutoPostBack="true" CausesValidation="false" CssClass="form-control" TabIndex="142"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-7 paddingLeft0">
                                                                <div class="col-sm-2 labelText1 paddingLeft0">
                                                                    Max Stock Value(<asp:Label runat="server" ID="PCCurrency"></asp:Label>)
                                                                </div>
                                                                <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                                    <asp:TextBox ID="txtPCMaxSVal" onkeydown="return jsDecimals(event);" Style="text-align: right" AutoPostBack="true" OnTextChanged="txtPCMaxSVal_TextChanged" runat="server" CssClass="txtPCMaxSVal form-control" MaxLength="15" TabIndex="143"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 labelText1 paddingRight0 width70">
                                                                    Manager
                                                                </div>
                                                                <div class="col-sm-2 paddingRight0 paddingLeft0">
                                                                    <asp:TextBox runat="server" ID="txtPCManager" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtPCManager_TextChanged" Style="text-transform: uppercase" TabIndex="144"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 Lwidth">
                                                                    <asp:LinkButton ID="lbtnPCManager" runat="server" CausesValidation="false" OnClick="lbtnPCManager_Click" TabIndex="145">
                                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-1 labelText1 paddingRight0 width50">
                                                                    Active
                                                                </div>
                                                                <div class="col-sm-1 labelText1 padding0 width25">
                                                                    <asp:CheckBox runat="server" ID="chkPCStatus" TabIndex="146" AutoPostBack="true" />
                                                                </div>
                                                                <div class="col-sm-2 labelText1 padding0 width125">
                                                                    Restrict to profit center
                                                                </div>
                                                                <div class="col-sm-1 labelText1 padding0 width25">
                                                                    <asp:CheckBox runat="server" ID="chkPCPermission" TabIndex="147" AutoPostBack="true" />
                                                                </div>
                                                                <div class="col-sm-1 paddingRight0">
                                                                    <asp:LinkButton ID="lbtnAddPrftCntr" runat="server" CausesValidation="false" OnClick="lbtnAddPrftCntr_Click" TabIndex="148">
                                                                                    <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:UpdatePanel ID="pnlBottomEmpPrftCntrDOWNPanel" runat="server">
                                                <ContentTemplate>
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <div class="panel panel-default marginLeftRight5">
                                                                <div class="panel-body">
                                                                    <div class="col-sm-12">
                                                                        <div class="panel-body panelscollbar height85">
                                                                            <asp:GridView ID="grdemployeemaster_profitmaster" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                                EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                                <Columns>
                                                                                    <%-- <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="lbtngrdemployeemaster_profitmasterDelete" runat="server" CausesValidation="false" OnClientClick="DeleteConfirm();" OnClick="lbtngrdemployeemaster_profitmasterDelete_Click">
                                                                                                    <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                                                                            </asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                       </asp:TemplateField>--%>
                                                                                    <asp:TemplateField HeaderText="Company">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_Company" runat="server" ToolTip='<%# Bind("companyname") %>' Text='<%# Bind("_mpce_com") %>' Width="100px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <%--<asp:TemplateField HeaderText="Status" Visible="false">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="rep_status" runat="server" Text='<%# Bind("Rpl_active") %>' Width="100px"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                    </asp:TemplateField>--%>
                                                                                    <asp:TemplateField HeaderText="Profit Center">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_PrftCntr" runat="server" ToolTip='<%# Bind("profitcenterorlocation") %>' Text='<%# Bind("_mpce_pc") %>' Width="100px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Representative Code">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_RptCode" runat="server" Text='<%# Bind("_mpce_rep_cd") %>' Width="100px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Stock Value(Max)">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_MaxSVal" runat="server" Text='<%# Bind("_mpce_max_stk_val", "{0:F}") %>' Width="100px" Style="text-align: right"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Manager">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_Manager" runat="server" ToolTip='<%# Bind("manager") %>' Text='<%# Bind("_mpce_mgr") %>' Width="100px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Restricted">
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chk_PCPermission" runat="server" Enabled="true" Checked='<%#Convert.ToBoolean(Eval("_mpce_is_rest")) %>' Width="100px"></asp:CheckBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Active">
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chk_PCStatus" runat="server" Enabled="true" Checked='<%#Convert.ToBoolean(Eval("_mpce_act")) %>' Width="100px"></asp:CheckBox>
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
                                <div class="tab-pane " id="Employee_Location">
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="pnlBottomEmpLocationPanel" runat="server">
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="pnlBottomEmpLocationUPPanel" runat="server">
                                                <ContentTemplate>
                                                    <div class="panel panel-default">
                                                        <div class="panel-body">
                                                            <div class="col-sm-6 paddingRight0 width525">
                                                                <div class="col-sm-1 labelText1 paddingLeft0 paddingRight0 width50">
                                                                    Location 
                                                                </div>
                                                                <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                                    <asp:TextBox runat="server" ID="txtLLocation" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtLLocation_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft5 paddingRight15 width25">
                                                                    <asp:LinkButton ID="lbtnLLocation" runat="server" CausesValidation="false" OnClick="lbtnLLocation_Click">
                                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-1 labelText1 paddingLeft0 paddingRight0 width70">
                                                                    Assigned Date
                                                                </div>
                                                                <div class="col-sm-2 paddingRight5 width100">
                                                                    <asp:TextBox runat="server" ReadOnly="true" ID="txtLAssDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0">
                                                                    <asp:LinkButton ID="lbtnLAssDate" runat="server" CausesValidation="false">
                                                                                                     <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                    <asp:CalendarExtender ID="cldLAssDate" runat="server" TargetControlID="txtLAssDate"
                                                                        PopupButtonID="lbtnLAssDate" Format="dd/MMM/yyyy">
                                                                    </asp:CalendarExtender>
                                                                </div>
                                                                <div class="col-sm-3 labelText1">
                                                                    Representative Code
                                                                </div>
                                                                <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                                    <asp:TextBox runat="server" ID="txtLRptCde" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-7 paddingLeft0">
                                                                <div class="col-sm-2 labelText1 paddingLeft0">
                                                                    Max Stock Value(<asp:Label runat="server" ID="LCurrency"></asp:Label>)
                                                                </div>
                                                                <div class="col-sm-1 paddingRight0 paddingLeft0">
                                                                    <asp:TextBox ID="txtLMaxSVal" onkeydown="return jsDecimals(event);" Style="text-align: right" AutoPostBack="true" OnTextChanged="txtLMaxSVal_TextChanged" runat="server" CssClass="txtLMaxSVal form-control" MaxLength="15"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-2 labelText1 paddingRight0 width70">
                                                                    Manager
                                                                </div>
                                                                <div class="col-sm-2 paddingRight0 paddingLeft0">
                                                                    <asp:TextBox runat="server" ID="txtLManager" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtLManager_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 Lwidth">
                                                                    <asp:LinkButton ID="lbtnLManager" runat="server" CausesValidation="false" OnClick="lbtnLManager_Click">
                                                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-1 labelText1 paddingRight0 width50">
                                                                    Active
                                                                </div>
                                                                <div class="col-sm-1 labelText1 padding0 width25">
                                                                    <asp:CheckBox runat="server" ID="chkLStatus" AutoPostBack="true" />
                                                                </div>
                                                                <div class="col-sm-2 labelText1 padding0 width125">
                                                                    Restrict to location
                                                                </div>
                                                                <div class="col-sm-1 labelText1 padding0 width25">
                                                                    <asp:CheckBox runat="server" ID="chkLPermission" AutoPostBack="true" />
                                                                </div>
                                                                <div class="col-sm-1 paddingRight0">
                                                                    <asp:LinkButton ID="lbtnAddLocation" runat="server" CausesValidation="false" OnClick="lbtnAddLocation_Click">
                                                                                            <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
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
                                                                <div class="panel-body">
                                                                    <div class="col-sm-12">
                                                                        <div class="panel-body panelscollbar height85">
                                                                            <asp:GridView ID="grdemployeemaster_location" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                                <Columns>
                                                                                    <%-- <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="lbtngrdemployeemaster_locationDelete" runat="server" CausesValidation="false" OnClientClick="DeleteConfirm();" OnClick="lbtngrdemployeemaster_locationDelete_Click">
                                                                                                    <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                                                                            </asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>--%>
                                                                                    <asp:TemplateField HeaderText="Company">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_Company" runat="server" ToolTip='<%# Bind("companyname") %>' Text='<%# Bind("_mpce_com") %>' Width="100px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Location">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_LLocation" runat="server" ToolTip='<%# Bind("profitcenterorlocation") %>' Text='<%# Bind("_mpce_pc") %>' Width="100px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Representative Code">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_LRptCde" runat="server" Text='<%# Bind("_mpce_rep_cd") %>' Width="100px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Stock Value(Max)">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_LMaxSVal" runat="server" Text='<%# Bind("_mpce_max_stk_val", "{0:F}") %>' Width="100px" Style="text-align: right"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Manager">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_LManger" runat="server" ToolTip='<%# Bind("manager") %>' Text='<%# Bind("_mpce_mgr") %>' Width="100px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Restricted">
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chk_LPermission" runat="server" Enabled="true" Checked='<%#Convert.ToBoolean(Eval("_mpce_is_rest")) %>' Width="100px"></asp:CheckBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Active">
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chk_LStatus" runat="server" Enabled="true" Checked='<%#Convert.ToBoolean(Eval("_mpce_act")) %>' Width="100px"></asp:CheckBox>
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
                                <div class="tab-pane " id="Customer_Assign">
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="pnlBottomCusAssignPanel" runat="server">
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="pnlBottomCusAssignUPPanel" runat="server">
                                                <ContentTemplate>
                                                    <div class="panel panel-default">
                                                        <div class="panel-body">
                                                            <div class="col-sm-12 paddingLeft0">
                                                                <div class="col-sm-1 labelText1 paddingRight0 width125">
                                                                    Customer Code
                                                                </div>
                                                                <div class="col-sm-2 paddingRight5 paddingLeft0 width150">
                                                                    <asp:TextBox runat="server" ID="txtCusCde" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtCusCde_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 paddingLeft0 Lwidth">
                                                                    <asp:LinkButton ID="lbtnCusCde" runat="server" CausesValidation="false" OnClick="lbtnCusCde_Click">
                                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-2 labelText1 width100">
                                                                    Customer Name
                                                                </div>
                                                                <div class="col-sm-3">
                                                                    <asp:TextBox ID="txtCusName" AutoPostBack="true" MaxLength="10" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-1 labelText1 width40">
                                                                    Active
                                                                </div>
                                                                <div class="col-sm-1 labelText1">
                                                                    <asp:CheckBox runat="server" ID="chkCusAssStatus" AutoPostBack="true" />
                                                                </div>
                                                                <div class="col-sm-1 paddingRight0">
                                                                    <asp:LinkButton ID="lbtnAddCusAss" runat="server" CausesValidation="false" OnClick="lbtnAddCusAss_Click">
                                                                                    <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <%--  <div class="col-sm-4 paddingLeft0">
                                                            </div>--%>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>

                                            </asp:UpdatePanel>
                                            <asp:UpdatePanel ID="pnlBottomCusAssignDOWNPanel" runat="server">
                                                <ContentTemplate>
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <div class="panel panel-default marginLeftRight5">
                                                                <div class="panel-body">
                                                                    <div class="col-sm-12">
                                                                        <div class="panel-body panelscollbar height85">
                                                                            <asp:GridView ID="grdcustomer_employee" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found..." AutoGenerateColumns="False">
                                                                                <Columns>
                                                                                    <%--<asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="lbtngrdcustomer_employeeDelete" runat="server" CausesValidation="false" OnClientClick="DeleteConfirm();" OnClick="lbtngrdcustomer_employeeDelete_Click">
                                                                                                    <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                                                                            </asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>--%>
                                                                                    <asp:TemplateField HeaderText="Customer Code">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_CusCde" runat="server" Text='<%# Bind("_mpce_cus_cd")%>' Width="100%"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Employee Code" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_CusEmpCde" runat="server" Text='<%# Bind("_mpce_emp_cd")%>' Width="100%"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Customer Name">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbl_CusNme" runat="server" Text='<%# Bind("Customer")%>' Width="100%"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Active">
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chk_CusStatus" runat="server" Enabled="true" Checked='<%#Convert.ToBoolean(Eval("_mpce_stus")) %>' Width="100%"></asp:CheckBox>
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
            <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
        </div>
    </div>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserPopoup" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="pnlSearchPanel" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <%-- Style="display: none"--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlSearchPanel" DefaultButton="ImageSearch">
                <div runat="server" id="test" class="panel panel-default height400 width700">
                    <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="btnClose" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <%--<span>Common Search</span>--%>
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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy4" runat="server"></asp:ScriptManagerProxy>
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
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

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
                                            EPF NO
                                        </div>
                                        <div class="col-sm-5 labelText1 paddingLeft0">
                                            <asp:TextBox ID="txtepfnew" runat="server" CssClass="form-control" OnTextChanged="txtepfnew_TextChanged" />
                                        </div>
                                        <div class="col-sm-2 paddingRight5 paddingLeft0">
                                            <asp:LinkButton ID="lbtnupdateepfnew" CausesValidation="false" CssClass="floatRight" runat="server" OnClick="lbtnupdateepfnew_Click">
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



    <script>
        // kelum : 2016-June-20 : Enter only one dot value(.), postiove values, allow backspace ,delete
        Sys.Application.add_load(fun);
        function fun() {
            if (typeof jQuery == 'undefined') {
                alert('jQuery is not loaded');
            }
            $('.txtMaxSVal,.txtPCMaxSVal,.txtLMaxSVal').keypress(function (event) {
                return isNumber(event, this)
            });

            // THE SCRIPT THAT CHECKS IF THE KEY PRESSED IS A NUMERIC OR DECIMAL VALUE.
            function isNumber(evt, element) {

                var charCode = (evt.which) ? evt.which : event.keyCode

                if (
                    (charCode != 46 || $(element).val().indexOf('.') != -1) &&  // “.” CHECK DOT, AND ONLY ONE.
                    (charCode < 48 || charCode > 57) &&
                    (charCode != 8)                                             // ALLOW BACKSPACE 
                   )
                    return false;

                return true;
            }
            $('#BodyContent_txtEPFNo,#BodyContent_txtEmpCode,#BodyContent_txtEmpFirstName,#BodyContent_txtEmpLastName,#BodyContent_txtManager,#BodyContent_txtEmpNIC,#BodyContent_txtEmpMobile,#BodyContent_txtEmpHomePhone,#BodyContent_txtEmpOfficePhone,#BodyContent_txtContractor,#BodyContent_txtCategory,#BodyContent_txtDepProfit,#BodyContent_txtSubCat,#BodyContent_txtSupvsr,#BodyContent_txtPCRptCode').on('keypress', function (event) {
                var regex = new RegExp("^[`~!@#$%^&*()=+]+$");
                var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
                if (regex.test(key)) {
                    event.preventDefault();
                    return false;
                }
            });

            $('#BodyContent_txtEmpPolice,#BodyContent_txtEmpNameInt,#BodyContent_txtEmpPAL1,#BodyContent_txtEmpPAL2,#BodyContent_txtEmpPAL3,#BodyContent_txtEmpCAL1,#BodyContent_txtEmpCAL2,#BodyContent_txtEmpCAL3').on('keypress', function (event) {
                var regex = new RegExp("^[`~!@#$%^&*=+]+$");
                var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
                if (regex.test(key)) {
                    event.preventDefault();
                    return false;
                }
            });
        }
    </script>
</asp:Content>
