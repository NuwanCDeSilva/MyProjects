<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucCustomer.ascx.cs" Inherits="FastForward.SCMWeb.UserControls.ucCustomer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
<script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>
<link href="<%=Request.ApplicationPath%>Css/bootstrap.css" rel="stylesheet" />
<link href="<%=Request.ApplicationPath%>Css/style.css" rel="stylesheet" />

<script>

    function Enable() {
        return;
    };


    function SaveConfirm() {
        var selectedvalue = confirm("Do you want to clear data?");
        if (selectedvalue) {
            document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "Yes";
        } else {
            document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "No";
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

    function checkDate(sender, args) {

        if ((sender._selectedDate > new Date())) {
            alert("You cannot select a day earlier than today!");
            sender._selectedDate = new Date();
            sender._textbox.set_Value(sender._selectedDate.format(sender._format))
        }
    };

    function RecallConfirm() {
        var selectedvalue = confirm("Customer already exists for this  passport number, Do you want to recall the existing  customer details ?");
        if (selectedvalue) {
            document.getElementById('<%=txtrecall.ClientID %>').value = "Yes";
        } else {
            document.getElementById('<%=txtrecall.ClientID %>').value = "No";
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

</script>



<div class="panel panel-default marginLeftRight5">

    <asp:HiddenField ID="txtClearlconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtSavelconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtUpdateconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtApprovalconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtCancelconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtDeleteconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtrecall" runat="server" />

    <div class="panel-body">
        
        <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-sm-8  buttonrow">
                       <%-- <div id="WarnningCustomer" visible="false" runat="server" class="alert alert-danger alert-dismissible" role="alert">
                             
                            <div class="col-sm-11 paddingLeft0 paddingRight0">
                                <strong>Alert!</strong>
                                <asp:Label ID="lblWarnningCustomer" runat="server"></asp:Label>
                            </div>
                            <div class="col-sm-1 paddingLeft0 paddingRight0">
                                <asp:LinkButton ID="lbtnclose" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnclose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </div>
                            
                        </div>
                        <div id="SuccessCustomer" runat="server" visible="false" class="alert alert-success alert-dismissible" role="alert">
                            <div class="col-sm-11">
                                <strong>Success!</strong>
                                <asp:Label ID="lblSuccessCustomer" runat="server"></asp:Label>
                            </div>
                            <div class="col-sm-1">
                                <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtnclose_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </div>

                        </div>
                        <div id="Div6" runat="server" visible="false" class="alert alert-info alert-dismissible" role="alert">
                            <strong>Alert!</strong>
                            <asp:Label ID="Label6" runat="server"></asp:Label>
                        </div>--%>
                         
                    </div>
                     
                    <div class="col-sm-4  buttonRow">
                        <div class="col-sm-7 paddingRight0">
                            <asp:LinkButton ID="lbtnSave" runat="server" CssClass="floatRight" OnClientClick="SaveConfirm()" OnClick="lbtnSave_Click">
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Create/Update
                               
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-5">
                            <asp:LinkButton ID="lbtnClear" runat="server" CssClass="floatRight" OnClientClick="ClearConfirm()" OnClick="lbtnClear_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-sm-5 paddingRight5">
                        <div class="row">
                            <div class="col-sm-4 labelText1">
                                Customer Type :
                            </div>
                            <div class="col-sm-8 paddingLeft0 paddingRight0">
                                <div class="col-sm-6 paddingLeft0 paddingRight0">
                                    <asp:DropDownList ID="ddlCtype" CausesValidation="false" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height5">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-4 labelText1">
                                NIC # :
                            </div>
                            <div class="col-sm-6 paddingLeft0 paddingRight0">
                                <asp:TextBox runat="server" ID="txtNIC" MaxLength="10" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtNIC_TextChanged"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height5">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-4 labelText1">
                                Passport # :
                            </div>
                            <div class="col-sm-6 paddingLeft0 paddingRight0">
                                <asp:TextBox runat="server" ID="txtPP" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtPP_TextChanged" MaxLength="9"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height5">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-4 labelText1">
                                DL # :
                            </div>
                            <div class="col-sm-6 paddingLeft0 paddingRight0">
                                <asp:TextBox runat="server" ID="txtDL" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtDL_TextChanged" MaxLength="9"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height5">
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4 paddingLeft5 paddingRight5">
                        <div class="row">
                            <div class="col-sm-5 labelText1">
                                Code :
                            </div>
                            <div class="col-sm-6 paddingLeft0 paddingRight0">
                                <asp:TextBox runat="server" ID="txtCusCode" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtCusCode_TextChanged"></asp:TextBox>
                            </div>
                            <div class="col-sm-1 paddingLeft0 Lwidth">
                                <asp:LinkButton ID="lbtnCusCode" runat="server" CausesValidation="false" OnClick="lbtnCusCode_Click">
                                     <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height5">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-5 labelText1">
                                BR # :
                            </div>
                            <div class="col-sm-6 paddingLeft0 paddingRight0">
                                <asp:TextBox runat="server" ID="txtBR" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtBR_TextChanged" MaxLength="15"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height5">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-5 labelText1">
                                Mobile # :
                            </div>
                            <div class="col-sm-6 paddingLeft0 paddingRight0">
                                <asp:TextBox runat="server" ID="txtMob" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtMob_TextChanged"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height5">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-5 labelText1">
                                Pref. Language :
                            </div>
                            <div class="col-sm-6 paddingLeft0 paddingRight0">
                                <asp:DropDownList ID="ddlPrefLang" CausesValidation="false" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="ENGLISH" Value="0" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height5">
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3 paddingLeft5">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                Pref. Notification Alert
                            </div>
                            <div class="panel-body">
                                <div class="col-sm-3 labelText1">
                                    SMS
                                </div>
                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                    <asp:CheckBox ID="chkSMS" runat="server" Width="5px"></asp:CheckBox>
                                </div>
                                <div class="col-sm-5 labelText1">
                                    E-Mail
                                </div>
                                <div class="col-sm-1 paddingLeft0 paddingRight0">
                                    <asp:CheckBox ID="chkMail" runat="server" Width="5px"></asp:CheckBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                Personal Details
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-2 labelText1">
                                        Title :
                                    </div>
                                    <div class="col-sm-2 paddingLeft0 paddingRight0">
                                        <div class="col-sm-6 paddingLeft0 paddingRight0">
                                            <asp:DropDownList ID="ddlTitle" CausesValidation="false" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="MR." Selected="True" Value="0" />
                                                <asp:ListItem Text="MS." Value="1" />
                                                <asp:ListItem Text="MRS." Value="2" />
                                                <asp:ListItem Text="MISS." Value="3" />
                                                <asp:ListItem Text="DR." Value="4" />
                                                <asp:ListItem Text="REV." Value="5" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-1 labelText1">
                                        Sex :
                                    </div>
                                    <div class="col-sm-2 paddingLeft0 paddingRight0">
                                        <asp:DropDownList ID="ddlSex" CausesValidation="false" runat="server" CssClass="form-control">
                                            <%-- <asp:ListItem Text="FEMALE" Selected="True" Value="0" />
                                            <asp:ListItem Text="MALE" Value="1" />--%>
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-sm-2 labelText1">
                                        Date of birth :
                                    </div>
                                    <div class="col-sm-2 paddingRight5 paddingLeft0">
                                        <asp:TextBox runat="server" ReadOnly="true" ID="txtDOB" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-1 paddingLeft0">
                                        <asp:LinkButton ID="lbtnDOB" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                        <asp:CalendarExtender OnClientDateSelectionChanged="checkDate" ID="CalendarExtender2" runat="server" TargetControlID="txtDOB"
                                            PopupButtonID="lbtnDOB" Format="dd/MMM/yyyy">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-2 labelText1">
                                        Initials :
                                    </div>
                                    <div class="col-sm-3 paddingLeft0 paddingRight0">
                                        <asp:TextBox runat="server" ID="txtInit" CausesValidation="false" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtInit_TextChanged"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-5 paddingLeft0">
                                        <div class="col-sm-5 labelText1">
                                            First Name :
                                        </div>
                                        <div class="col-sm-7 paddingLeft0 ">
                                            <asp:TextBox runat="server" ID="txtFname" CausesValidation="false" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtFname_TextChanged"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-7 paddingLeft0">
                                        <div class="col-sm-3 labelText1">
                                            Surname :
                                        </div>
                                        <div class="col-sm-9 paddingLeft0">
                                            <asp:TextBox runat="server" ID="txtSName" CausesValidation="false" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtSName_TextChanged"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-2 labelText1">
                                        Name in full :
                                    </div>
                                    <div class="col-sm-10 paddingLeft0">
                                        <asp:TextBox runat="server" ID="txtName" CausesValidation="false" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtName_TextChanged"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="row">
            <div class="col-sm-12">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="bs-example">
                            <ul class="nav nav-tabs" id="myTab">
                                <li class="active"><a href="#Permenent" data-toggle="tab">Permenent</a></li>
                                <li><a href="#Present" data-toggle="tab">Present</a></li>
                                <li><a href="#WorkingPlace" data-toggle="tab">Working Place</a></li>
                                <li><a href="#Taxdetails" data-toggle="tab">Tax details</a></li>
                            </ul>
                            <div class="tab-content">

                                <div class="tab-pane active" id="Permenent">
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2 labelText1">
                                                    Address   :
                                                </div>
                                                <div class="col-sm-10 paddingLeft0">
                                                    <asp:TextBox runat="server" AutoPostBack="true" ID="txtPerAdd1" CausesValidation="false" CssClass="form-control" OnTextChanged="txtPerAdd1_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2 labelText1">
                                                </div>
                                                <div class="col-sm-10 paddingLeft0">
                                                    <asp:TextBox runat="server" AutoPostBack="true" ID="txtPerAdd2" CausesValidation="false" CssClass="form-control" OnTextChanged="txtPerAdd2_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2 labelText1">
                                                    Town :
                                                </div>
                                                <div class="col-sm-4 paddingLeft0">
                                                    <asp:TextBox runat="server" AutoPostBack="true" ID="txtPerTown" CausesValidation="false" OnTextChanged="txtPerTown_TextChanged" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0 Lwidth">
                                                    <asp:LinkButton ID="lbtnTown" runat="server" CausesValidation="false" OnClick="lbtnTown_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="col-sm-2 labelText1">
                                                    District :
                                                </div>
                                                <div class="col-sm-3 paddingLeft0">
                                                    <asp:TextBox runat="server" ID="txtPerDistrict" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtPerDistrict_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2 labelText1">
                                                    Postal code :
                                                </div>
                                                <div class="col-sm-4 paddingLeft0">
                                                    <asp:TextBox runat="server" ID="txtPerPostal" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtPerPostal_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0 Lwidth">
                                                </div>
                                                <div class="col-sm-2 labelText1">
                                                    Province :
                                                </div>
                                                <div class="col-sm-3 paddingLeft0">
                                                    <asp:TextBox runat="server" ID="txtPerProvince" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtPerProvince_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2 labelText1">
                                                    Country # :
                                                </div>
                                                <div class="col-sm-4 paddingLeft0">
                                                    <asp:TextBox runat="server" ID="txtPerCountry" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtPerCountry_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0 Lwidth">
                                                </div>
                                                <div class="col-sm-2 labelText1">
                                                    Phone # :
                                                </div>
                                                <div class="col-sm-3 paddingLeft0">
                                                    <asp:TextBox runat="server" ID="txtPerPhone" MaxLength="10" CausesValidation="false" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtPerPhone_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-2 labelText1">
                                                    Email :
                                                </div>
                                                <div class="col-sm-10 paddingLeft0">
                                                    <asp:TextBox runat="server" ID="txtPerEmail" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtPerEmail_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="tab-pane" id="Present">
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2 labelText1">
                                                    Address   :
                                                </div>
                                                <div class="col-sm-10 paddingLeft0">
                                                    <asp:TextBox runat="server" ID="txtPreAdd1" CausesValidation="false" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtPreAdd1_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2 labelText1">
                                                </div>
                                                <div class="col-sm-10 paddingLeft0">
                                                    <asp:TextBox runat="server" ID="txtPreAdd2" CausesValidation="false" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtPreAdd2_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2 labelText1">
                                                    Town :
                                                </div>
                                                <div class="col-sm-4 paddingLeft0">
                                                    <asp:TextBox runat="server" ID="txtPreTown" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtPerTown_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0 Lwidth">
                                                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false" OnClick="lbtnTown_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="col-sm-2 labelText1">
                                                    District :
                                                </div>
                                                <div class="col-sm-3 paddingLeft0">
                                                    <asp:TextBox runat="server" ID="txtPreDistrict" CausesValidation="false" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtPreDistrict_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2 labelText1">
                                                    Postal code :
                                                </div>
                                                <div class="col-sm-4 paddingLeft0">
                                                    <asp:TextBox runat="server" ID="txtPrePostal" CausesValidation="false" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtPrePostal_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0 Lwidth">
                                                </div>
                                                <div class="col-sm-2 labelText1">
                                                    Province :
                                                </div>
                                                <div class="col-sm-3 paddingLeft0">
                                                    <asp:TextBox runat="server" ID="txtPreProvince" CausesValidation="false" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtPreProvince_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2 labelText1">
                                                    Country # :
                                                </div>
                                                <div class="col-sm-4 paddingLeft0">
                                                    <asp:TextBox runat="server" ID="txtPreCountry" CausesValidation="false" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtPreCountry_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0 Lwidth">
                                                </div>
                                                <div class="col-sm-2 labelText1">
                                                    Phone # :
                                                </div>
                                                <div class="col-sm-3 paddingLeft0">
                                                    <asp:TextBox runat="server" AutoPostBack="true" MaxLength="10" ID="txtPrePhone" CausesValidation="false" CssClass="form-control" OnTextChanged="txtPrePhone_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="tab-pane" id="WorkingPlace">
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy4" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2 labelText1">
                                                    Name   :
                                                </div>
                                                <div class="col-sm-10 paddingLeft0">
                                                    <asp:TextBox runat="server" ID="txtWorkName" CausesValidation="false" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtWorkName_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2 labelText1">
                                                    Address   :
                                                </div>
                                                <div class="col-sm-10 paddingLeft0">
                                                    <asp:TextBox runat="server" ID="txtWorkAdd1" CausesValidation="false" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtWorkAdd1_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2 labelText1">
                                                </div>
                                                <div class="col-sm-10 paddingLeft0">
                                                    <asp:TextBox runat="server" ID="txtWorkAdd2" CausesValidation="false" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtWorkAdd2_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2 labelText1">
                                                    Department  :
                                                </div>
                                                <div class="col-sm-4 paddingLeft0">
                                                    <asp:TextBox runat="server" ID="txtWorkDept" CausesValidation="false" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtWorkDept_TextChanged"></asp:TextBox>
                                                </div>

                                                <div class="col-sm-2 labelText1">
                                                    Phone # :
                                                </div>
                                                <div class="col-sm-3 paddingLeft0">
                                                    <asp:TextBox runat="server" ID="txtWorkPhone" CausesValidation="false" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtWorkPhone_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2 labelText1">
                                                    Designation :
                                                </div>
                                                <div class="col-sm-4 paddingLeft0">
                                                    <asp:TextBox runat="server" ID="txtWorkDesig" CausesValidation="false" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtWorkDesig_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1 paddingLeft0 Lwidth">
                                                </div>
                                                <div class="col-sm-2 labelText1">
                                                    Fax # :
                                                </div>
                                                <div class="col-sm-3 paddingLeft0">
                                                    <asp:TextBox runat="server" ID="txtWorkFax" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtWorkFax_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2 labelText1">
                                                    Email :
                                                </div>
                                                <div class="col-sm-10 paddingLeft0">
                                                    <asp:TextBox runat="server" ID="txtWorkEmail" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtWorkEmail_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="tab-pane" id="Taxdetails">
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy5" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2 labelText1">
                                                    VAT Customer 
                                                </div>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:CheckBox ID="chkVAT" AutoPostBack="true" runat="server" Width="5px" OnCheckedChanged="chkVAT_CheckedChanged"></asp:CheckBox>
                                                </div>
                                                <div class="col-sm-2 labelText1">
                                                    VAT Extempted
                                                </div>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:CheckBox ID="chkVatEx" runat="server" Width="5px"></asp:CheckBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2 labelText1">
                                                    VAT Reg. #  :
                                                </div>
                                                <div class="col-sm-5 paddingLeft0">
                                                    <asp:TextBox runat="server" ID="txtVatreg" CausesValidation="false" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtVatreg_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2 labelText1">
                                                    SVAT Customer 
                                                </div>
                                                <div class="col-sm-10 paddingLeft0">
                                                    <asp:CheckBox ID="chkSVAT" AutoPostBack="true" runat="server" Width="5px" OnCheckedChanged="chkSVAT_CheckedChanged"></asp:CheckBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2 labelText1">
                                                    SVAT Reg. #  :
                                                </div>
                                                <div class="col-sm-4 paddingLeft0">
                                                    <asp:TextBox runat="server" ID="txtSVATReg" CausesValidation="false" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtSVATReg_TextChanged"></asp:TextBox>
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
    <div class="panel-footer">
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
<asp:ScriptManagerProxy ID="ScriptManagerProxy8" runat="server"></asp:ScriptManagerProxy>
<asp:UpdatePanel ID="UpdatePanel11" runat="server">
    <ContentTemplate>

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
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy6" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="grdResult" CausesValidation="false" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResult_SelectedIndexChanged" OnPageIndexChanging="grdResult_PageIndexChanging">
                                                <Columns>
                                                    <asp:ButtonField Text="select" CommandName="Select" ItemStyle-Width="10" />

                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
                                runat="server" AssociatedUpdatePanelID="UpdatePanel11">

                                <ProgressTemplate>
                                    <div class="divWaiting">
                                        <asp:Label ID="lblWait" runat="server"
                                            Text="Please wait... " />
                                        <asp:Image ID="imgWait" runat="server"
                                            ImageAlign="Middle" ImageUrl="~/images/banners/hidden_progress_bar.gif" />
                                    </div>
                                </ProgressTemplate>

                            </asp:UpdateProgress>
                        </div>
                    </div>
                </div>

            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>

<asp:ScriptManagerProxy ID="ScriptManagerProxy12" runat="server"></asp:ScriptManagerProxy>
<asp:UpdatePanel ID="UpdatePanel12" runat="server">
    <ContentTemplate>
        <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
        <asp:ModalPopupExtender ID="userconfmbox" runat="server" Enabled="True" TargetControlID="Button1"
            PopupControlID="Panel1" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
    </ContentTemplate>
</asp:UpdatePanel>

<asp:Panel ID="Panel1" runat="server" align="center">
    <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
    <asp:ScriptManagerProxy ID="ScriptManagerProxy11" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
        <ContentTemplate>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span>Alert</span>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:Label ID="lblMssg" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:Label ID="lblMssg1" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:Label ID="lblMssg2" runat="server"></asp:Label>
                        </div>
                    </div>
                    <asp:Panel runat="server" ID="pnlcode">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblMssg3" runat="server" Text="Pls enter valid verification code"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:TextBox runat="server" ID="txtVcode" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="row">
                        <div class="col-sm-12 height22">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-2">
                        </div>
                        <div class="col-sm-4">
                            <asp:Button ID="btnok" runat="server" Text="yes" CausesValidation="false" class="btn btn-primary" OnClick="btnok_Click" />
                        </div>
                        <div class="col-sm-4 ">
                            <asp:Button ID="btncancel" runat="server" Text="No" CausesValidation="false" class="btn btn-primary" OnClick="btncancel_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Panel>
