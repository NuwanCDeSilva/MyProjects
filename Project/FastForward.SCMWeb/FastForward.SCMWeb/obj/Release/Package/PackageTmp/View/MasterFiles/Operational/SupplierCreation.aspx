<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="SupplierCreation.aspx.cs" Inherits="FastForward.SCMWeb.View.MasterFiles.Operational.SupplierCreation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <%-- <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>--%>

    <style>
        .gridScroll {
            height: 115px;
            overflow: auto;
            margin-bottom: 0px;
        }

        .panel-heading {
            padding-top: 1px;
            padding-bottom: 1px;
        }

        #GHead .table.table-hover.table-striped {
            margin-bottom: 0;
        }
    </style>
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
    <script type="text/javascript">
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
                position: 'top-center',
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
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            // alert(charCode.value);
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        function isDays(evt, textBox) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            // alert(charCode.value);
            if (textBox.value.length < 5) {
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                else {
                    return true;
                }
            }
            else {
                textBox.value = textBox.value.substr(0, 5);
                alert('Maximum 5 characters are allowed ');
                return false;
            }
        }

        function isPhoneNo(evt, textBox) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            //alert(charCode);
            //var s = textBox.value;
            if ((charCode == 8)) {
                return true;
            }
            if ((charCode == 9)) {
                return true;
            }
            if (textBox.value.length < 15) {
                if ((charCode < 58 && charCode > 47)) {
                    return true;
                }
                if ((charCode == 43)) {
                    // alert(textBox.value);
                    var no = textBox.value;
                    var result = "+" + no;
                    //  alert(result);
                    if (textBox.value.charAt(0) != "+") {
                        textBox.value = result;
                        return false;
                    }
                    else {
                        return false;
                    }
                }
                else {
                    return false;
                }
            }
            else {
                textBox.value = textBox.value.substr(0, 15);
                alert('Maximum 15 characters are allowed ');
                return false;
            }
        }
        function isName(evt) {
            //A to Z and space
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if ((charCode == 8)) {
                return true;
            }
            if ((charCode == 9)) {
                return true;
            }
            //alert(charCode)
            if ((charCode < 91 && charCode > 64)) {
                return true;
            }
            if ((charCode < 122 && charCode > 96)) {
                return true;
            }
            if ((charCode == 32)) {
                return true;
            }

            else {
                //showStickyWarningToast('');
                return false;
            }
        }
        function Check(textBox, maxLength) {
            if (textBox.value.length > maxLength) {
                alert("Maximum characters allowed are " + maxLength);
                textBox.value = textBox.value.substr(0, maxLength);
            }
        };
        function ConfirmDelete() {
            var selectedvaldelitm = confirm("Do you want to remove ?");
            if (selectedvaldelitm) {
                document.getElementById('<%=hdfDelete.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdfDelete.ClientID %>').value = "No";
            }
        };
        function ConfirmPlaceOrder() {
            var selectedvalueOrdPlace = confirm("Do you want to save ?");
            if (selectedvalueOrdPlace) {
                document.getElementById('<%=hdfSave.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=hdfSave.ClientID %>').value = "No";
            }
        };

        function ClearConfirm() {
            var selectedvalue = confirm("Do you want to clear data?");
            if (selectedvalue) {
                document.getElementById('<%=hdfClearData.ClientID %>').value = "1";
            } else {
                document.getElementById('<%=hdfClearData.ClientID %>').value = "0";
            }
        };
    </script>
    <script type="text/javascript">
        function CheckAllgrdreqItem(oCheckbox) {
            var GridView2 = document.getElementById("<%=dgvSelItms.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }
    </script>
    <style>
        .panel-body {
            padding-top: 0px;
            padding-bottom: 1px;
        }

        .panel {
            margin-bottom: 1px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-12 padding0">
                        <div class="panel panel-default">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row buttonRow" id="HederBtn">
                                        <div class="col-sm-12 col-md-12">
                                            <div class="col-md-10"></div>
                                            <div class="col-md-1 paddingRight0">
                                                <asp:LinkButton ID="lbtnSave" CausesValidation="false" runat="server" OnClientClick="ConfirmPlaceOrder();" CssClass="floatRight" OnClick="lbtnSave_Click"> 
                                                <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save </asp:LinkButton>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:LinkButton ID="lbtnClear" CausesValidation="false" runat="server" OnClientClick="return ClearConfirm()" CssClass="floatRight" OnClick="lbtnClear_Click"> 
                                                <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-heading" style="margin-bottom: 0px;">
                                <strong><b>Supplier Creation</b></strong>
                            </div>
                            <div class="panel panel-body" style="padding-left: 0px; padding-right: 0px; padding-bottom: 0px; padding-top: 0px;">
                                <%--button div--%>
                                <div class="row">
                                    <asp:HiddenField runat="server" ID="hdfClearData" Value="0" />
                                    <asp:HiddenField runat="server" ID="hdfSave" Value="0" />
                                    <asp:HiddenField runat="server" ID="hdfDelete" Value="0" />

                                </div>
                                <%--Supplier div--%>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-sm-6" style="padding-left: 1px; padding-right: 1px;">
                                            <div class="panel panel-default">
                                                <div class="panel panel-heading">
                                                    <b>Details</b>
                                                </div>
                                                <div class="panel panel-body">
                                                    <div class="col-md-12 padding0">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-6" style="padding-left: 0px;">
                                                                    <div class="row">
                                                                        <div class="col-md-12">
                                                                            <div class="col-md-4 labelText1 paddingLeft0">
                                                                                Code
                                                                            </div>
                                                                            <div class="col-md-7" style="padding-left: 20px; padding-right: 0px;">
                                                                                <asp:TextBox CssClass="txtCode form-control" Style="text-transform: uppercase" ID="txtCode" AutoPostBack="true" runat="server" OnTextChanged="txtCode_TextChanged" />
                                                                            </div>
                                                                            <div class="col-md-1" style="padding-left: 3px;">
                                                                                <asp:LinkButton ID="lbtnSeCode" CssClass="" CausesValidation="false" runat="server" OnClick="lbtnSeCode_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6" style="padding-left: 0px; padding-right: 4px;">
                                                                    <div class="row">
                                                                        <div class="col-md-12">
                                                                            <div class="col-md-3 paddingRight0 labelText1">
                                                                                Code Type
                                                                            </div>
                                                                            <div class="col-md-4" style="padding-left: 0px; padding-right: 0px;">
                                                                                <asp:DropDownList CssClass="ddlCodeType form-control" ID="ddlCodeType" runat="server">
                                                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                                    <asp:ListItem Value="S" Text="Supplier"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                            <div class="col-md-2 height20" style="text-align: right; padding-right: 5px;">
                                                                                <asp:CheckBox ID="chkTaxSupplier" CssClass="chk" Text="" runat="server" />
                                                                            </div>
                                                                            <div class="col-md-3" style="padding-left: 0px; padding-right: 0px;">
                                                                                <asp:Label ID="lblt" Text="Tax" runat="server" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-2 labelText1 paddingLeft0">
                                                                    Name
                                                                </div>
                                                                <div class="col-md-10">
                                                                    <asp:TextBox CssClass="txtName form-control" Style="text-transform: uppercase" ID="txtName" runat="server" AutoPostBack="true" OnTextChanged="txtName_TextChanged" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-2 labelText1 paddingLeft0">
                                                                    Address 1
                                                                </div>
                                                                <div class="col-md-10">
                                                                    <asp:TextBox CssClass="txtAddress1 form-control" ID="txtAddress1" Style="text-transform: uppercase" runat="server" AutoPostBack="true" OnTextChanged="txtAddress1_TextChanged"/>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12 ">
                                                                <div class="col-md-2 labelText1 paddingLeft0">
                                                                    Address 2
                                                                </div>
                                                                <div class="col-md-10">
                                                                    <asp:TextBox CssClass="txtAddress2 form-control" ID="txtAddress2" Style="text-transform: uppercase" runat="server" AutoPostBack="true" OnTextChanged="txtAddress2_TextChanged"/>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12 ">
                                                                <div class="col-md-2 labelText1  paddingLeft0">
                                                                    Contact Person
                                                                </div>
                                                                <div class="col-md-10">
                                                                    <asp:TextBox CssClass="txtContactPerson form-control" onpaste="return false" Style="text-transform: uppercase"
                                                                        ID="txtContactPerson" runat="server" AutoPostBack="true" OnTextChanged="txtContactPerson_TextChanged"/>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12 ">
                                                                <div class="col-md-2 labelText1 paddingLeft0">
                                                                    Phone #
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <asp:TextBox CssClass="txtPhone form-control"
                                                                        onpaste="return false" ID="txtPhone" runat="server" AutoPostBack="true" OnTextChanged="txtPhone_TextChanged"/>
                                                                </div>
                                                                <div class="col-md-2 labelText1 text-right ">
                                                                    Fax 
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <asp:TextBox CssClass="txtFax form-control" ID="txtFax"
                                                                        onpaste="return false" runat="server" AutoPostBack="true" OnTextChanged="txtFax_TextChanged"/>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-2 labelText1 paddingLeft0">
                                                                    Email  
                                                                </div>
                                                                <div class="col-md-10">
                                                                    <asp:TextBox CssClass="txtEmail form-control"
                                                                        ID="txtEmail" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-2 labelText1 paddingLeft0">
                                                                    Web Site 
                                                                </div>
                                                                <div class="col-md-10">
                                                                    <asp:TextBox AutoPostBack="true" CssClass="txtWebSite form-control" ID="txtWebSite" runat="server" OnTextChanged="txtWebSite_TextChanged"/>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-6" style="padding-left: 0px;">
                                                                    <div class="row">
                                                                        <div class="col-md-12">
                                                                            <div class="col-md-4 labelText1 paddingLeft0">
                                                                                Country of Origin
                                                                            </div>
                                                                            <div class="col-md-7" style="padding-left: 20px; padding-right: 0px;">
                                                                                <asp:TextBox AutoPostBack="true" CssClass="txtCountry form-control" ID="txtCountry" Style="text-transform: uppercase" runat="server"
                                                                                    OnTextChanged="txtCountry_TextChanged" />
                                                                            </div>
                                                                            <div class="col-md-1" style="padding-left: 3px;">
                                                                                <asp:LinkButton ID="lbtnSeCountry" CssClass="lbtn" CausesValidation="false" runat="server" OnClick="lbtnSeCountry_Click">
                                                           <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6" style="padding-left: 0px; padding-right: 4px;">
                                                                    <div class="row">
                                                                        <div class="col-md-12">
                                                                            <div class="col-md-4 labelText1 paddingLeft0">
                                                                                Dealing Currency 
                                                                            </div>
                                                                            <div class="col-md-7" style="padding-left: 20px; padding-right: 0px;">
                                                                                <asp:TextBox AutoPostBack="true" CssClass="txtCurrency form-control" ID="txtCurrency" Style="text-transform: uppercase" runat="server"
                                                                                    OnTextChanged="txtCurrency_TextChanged" />
                                                                            </div>
                                                                            <div class="col-md-1" style="padding-left: 3px;">
                                                                                <asp:LinkButton ID="lbtnSeCurrency" CssClass="lbtn" CausesValidation="false" runat="server" OnClick="lbtnSeCurrency_Click">
                                                           <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-6" style="padding-left: 0px;">
                                                                    <div class="row">
                                                                        <div class="col-md-12">
                                                                            <div class="col-md-4 labelText1 paddingLeft0">
                                                                                Credit Days
                                                                            </div>
                                                                            <div class="col-md-7" style="padding-left: 20px; padding-right: 0px;">
                                                                                <asp:TextBox CssClass="txtCreditPeriod form-control"
                                                                                    ID="txtCreditPeriod"
                                                                                    onpaste="return false" runat="server" />
                                                                            </div>
                                                                            <div class="col-md-1" style="padding-left: 3px;">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6" style="padding-left: 0px; padding-right: 4px;">
                                                                    <%--<div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="col-md-1 height20">
                                                                            <asp:CheckBox ID="chkSpecialTax" Text="" runat="server" AutoPostBack="true" OnCheckedChanged="chkSpecialTax_CheckedChanged" />
                                                                        </div>
                                                                        <div class="col-md-9" style="padding-left: 1px;">
                                                                            <asp:Label ID="lbl" Text="Special Tax" runat="server" />
                                                                        </div>
                                                                        <div class="col-md-2" style="padding-left: 3px;">
                                                                        </div>
                                                                    </div>
                                                                </div>--%>
                                                                    <div class="row">
                                                                        <div class="col-md-12">
                                                                            <div class="col-md-4 labelText1 paddingLeft0">
                                                                                GL Acc. Code
                                                                            </div>
                                                                            <div class="col-md-7" style="padding-left: 20px; padding-right: 0px;">
                                                                                <asp:TextBox CssClass="txtAccCode form-control" ID="txtAccCode" runat="server" AutoPostBack="true" OnTextChanged="txtAccCode_TextChanged"/>
                                                                            </div>
                                                                            <div class="col-md-1" style="padding-left: 3px;">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-6" style="padding-left: 0px;">

                                                                    <div class="row">
                                                                        <div class="col-md-12">
                                                                            <div class="col-md-4 labelText1 paddingLeft0">
                                                                                Type
                                                                            </div>
                                                                            <div class="col-md-7" style="padding-left: 20px; padding-right: 0px;">
                                                                                <asp:DropDownList CssClass="ddlType form-control" ID="ddlType" runat="server">
                                                                                    <asp:ListItem Text="--Select--" Value="0" />
                                                                                    <asp:ListItem Text="Foreign" Value="F" />
                                                                                    <asp:ListItem Text="Local" Value="L" />
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                            <div class="col-md-4" style="padding-left: 3px;">
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                <div class="col-md-6" style="padding-left: 0px; padding-right: 4px;">
                                                                    <div class="row" style="padding-left: 0px; padding-right: 0px;">
                                                                        <div class="col-md-1 height20" style="padding-left: 15px; padding-right: 0px;">
                                                                            <asp:CheckBox ID="chkActive" CssClass="" Checked="true" Text="" runat="server" />
                                                                        </div>
                                                                        <div class="col-md-10">
                                                                            <asp:Label ID="Label1" Text="Active" runat="server" />
                                                                        </div>
                                                                        <%--<asp:Panel ID="panelSpecialTax" runat="server">
                                                                        <div id="divSpecilTax" runat="server" class="col-md-12" style="padding-left: 24px; padding-right: 24px;">
                                                                            <div class="panel panel-default" style="padding-bottom: 0px; padding-top: 0px;">
                                                                                <div class="row" style="padding-top: 3px;">
                                                                                    <div class="col-md-12">
                                                                                        <div class="col-md-3 labelText1">
                                                                                            Code/Rate
                                                                                        </div>
                                                                                        <div class="col-md-4" style="padding-left: 20px; padding-right: 0px;">
                                                                                            <asp:TextBox AutoPostBack="false" CssClass="form-control" ID="txtRateCode" runat="server" OnTextChanged="txtRateCode_TextChanged" />
                                                                                        </div>
                                                                                        <div class="col-md-1" style="padding-left: 3px;">
                                                                                            <asp:LinkButton ID="lbtnSeRateCode" CausesValidation="false" runat="server" OnClick="lbtnSeRateCode_Click">
                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                        <div class="col-md-4" style="padding-left: 20px; padding-right: 3px;">
                                                                                            <asp:TextBox CssClass="form-control"  ID="txtRate" Enabled="false" runat="server" />
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-md-12">
                                                                                        <div class="col-md-3 labelText1">
                                                                                            Div Rate
                                                                                        </div>
                                                                                        <div class="col-md-4" style="padding-left: 20px; padding-right: 0px;">
                                                                                            <asp:TextBox CssClass="form-control" ID="txtDivRate" onkeypress="return isNumber(event)" runat="server" />
                                                                                        </div>
                                                                                        <div class="col-md-1 labelText1">
                                                                                        </div>
                                                                                        <div class="col-md-4" style="padding-left: 20px; padding-right: 3px;">
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </asp:Panel>--%>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-4 labelText1 paddingLeft0">
                                                                    Auto Replenishment Allow
                                                                </div>
                                                                <div class="col-md-2" style="padding-left: 15px; padding-right: 0px;">
                                                                     <asp:CheckBox ID="AutoRefinementCheck" CssClass=""  Text="" runat="server" />
                                                                </div>                                                          

                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-2 labelText1 paddingLeft0">
                                                                    Tax Category
                                                                </div>
                                                                <div class="col-md-2" style="padding-left: 15px; padding-right: 0px;">
                                                                    <asp:TextBox AutoPostBack="true" CssClass="txtTaxCat form-control" OnTextChanged="txtTaxCat_TextChanged"
                                                                        ID="txtTaxCat" Style="text-transform: uppercase" runat="server" />
                                                                </div>
                                                                <div class="col-md-1" style="padding-left: 3px; padding-right: 0px;">
                                                                    <asp:LinkButton ID="lbtnSeTaxCat" CssClass="lbtn" CausesValidation="false" runat="server" OnClick="lbtnSeTaxCat_Click">
                                                           <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-md-1 labelText1" style="text-align: right;">
                                                                    Tin #
                                                                </div>
                                                                <div class="col-md-2" style="padding-left: 3px; padding-right: 0px;">
                                                                    <asp:TextBox CssClass="txtTinNo form-control" ID="txtTinNo" runat="server" AutoPostBack="true" OnTextChanged="txtTinNo_TextChanged"/>
                                                                </div>
                                                                <div class="col-md-2 labelText1" style="padding-left: 10px; padding-right: 0px; text-align: right;">
                                                                    Tax Reg #
                                                                </div>
                                                                <div class="col-md-2" style="padding-left: 3px; padding-right: 0px;">
                                                                    <asp:TextBox CssClass="txtTaxReg form-control" ID="txtTaxReg" runat="server" AutoPostBack="true" OnTextChanged="txtTaxReg_TextChanged"/>
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-sm-12" style="padding-left: 1px; padding-right: 1px;">
                                                                    <div class="panel panel-default">
                                                                        <div class="panel panel-heading">
                                                                            <b>NBT Details</b>
                                                                        </div>
                                                                        <div class="panel panel-body">
                                                                            <div class="col-md-12 padding0">
                                                                                <div class="row">
                                                                                    <div class="col-md-1 labelText1" style="text-align: right;">
                                                                                        Activate
                                                                                    </div>
                                                                                    <div class="col-md-2" style="padding-left: 3px; padding-right: 0px;">
                                                                                        <asp:CheckBox ID="chkAct" CssClass="" Checked="false" Text="" runat="server" />
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-md-12">
                                                                                        <div class="col-md-2 labelText1 paddingLeft0">
                                                                                            Supplier NBT Details
                                                                                        </div>
                                                                                        <div class="col-md-2" style="padding-left: 15px; padding-right: 0px;">
                                                                                            <asp:TextBox AutoPostBack="true" CssClass="txtTaxCat form-control" ID="txtNBTType" Style="text-transform: uppercase" runat="server" OnTextChanged="txtNBTType_TextChanged" />
                                                                                        </div>
                                                                                        <div class="col-md-1" style="padding-left: 3px; padding-right: 0px;">
                                                                                            <asp:LinkButton ID="lbtnNBT" CssClass="lbtn" CausesValidation="false" runat="server" OnClick="lbtnNBT_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                        <div class="col-md-1 labelText1" style="text-align: right;">
                                                                                            Rate
                                                                                        </div>
                                                                                        <div class="col-md-2" style="padding-left: 3px; padding-right: 0px;">
                                                                                            <asp:TextBox CssClass="txtTinNo form-control" ID="txtNBTRate" runat="server" AutoPostBack="true" OnTextChanged="txtNBTRate_TextChanged"/>
                                                                                        </div>
                                                                                        <div class="col-md-2 labelText1" style="padding-left: 10px; padding-right: 0px; text-align: right;">
                                                                                            Dividend Amount
                                                                                        </div>
                                                                                        <div class="col-md-2" style="padding-left: 3px; padding-right: 0px;">
                                                                                            <asp:TextBox CssClass="txtTaxReg form-control" ID="txtDivAmt" runat="server" AutoPostBack="true" OnTextChanged="txtDivAmt_TextChanged"/>
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
                                        <div class="col-sm-6" style="padding-left: 1px; padding-right: 1px;">
                                            <div class="panel panel-default">
                                                <div class="panel panel-heading">
                                                    <b>Supplier Ports</b>
                                                </div>

                                                <div class="panel panel-body" style="height: 267px;">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <div class="col-sm-2 labelText1" style="padding-left: 0px; padding-right: 0px;">
                                                                <asp:Label Text="From Port" runat="server" />

                                                            </div>
                                                            <div class="col-sm-1" style="padding-left: 0px; padding-right: 0px; margin-left: -45px;">
                                                                <asp:TextBox ID="txtPortFromCode" Width="90px" AutoPostBack="true" Style="text-transform: uppercase" CssClass="txtPortToCode form-control" runat="server" OnTextChanged="txtPortFromCode_TextChanged" />
                                                            </div>
                                                            <div class="col-sm-1" style="padding-left: 4px; padding-right: 0px; margin-left: 42px;">
                                                                <asp:LinkButton ID="lbtnSePortFr" CssClass="lbtn" CausesValidation="false" runat="server" OnClick="lbtnSePortFr_Click">
															    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="col-sm-1 labelText1" style="padding-left: 0px; padding-right: 0px;">
                                                                To Port
                                                            </div>
                                                            <div class="col-sm-2" style="padding-left: 0px; padding-right: 0px;">
                                                                <asp:TextBox AutoPostBack="true" ID="txtPortToCode" CssClass="txtPortToCode form-control" Style="text-transform: uppercase" runat="server" OnTextChanged="txtPortToCode_TextChanged" />
                                                            </div>
                                                            <div class="col-sm-1" style="padding-left: 4px; padding-right: 0px;">
                                                                <asp:LinkButton ID="lbtnSePortTo" CssClass="lbtn" CausesValidation="false" runat="server" OnClick="lbtnSePortTo_Click">
															<span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="col-sm-4 padding0">
                                                                <div class="col-sm-5 labelText1" style="padding-left: 0px; padding-right: 0px;">
                                                                    Lead time CMB
                                                                </div>
                                                                <div class="col-sm-6" style="padding-left: 0px; padding-right: 0px;">
                                                                    <asp:TextBox ID="txtPorTime" CssClass="txtPortTime form-control" runat="server" />
                                                                </div>
                                                                <div class="col-sm-1" style="padding-left: 4px; padding-right: 0px;">
                                                                    <asp:LinkButton ID="lbtnAddPort" CssClass="lbtn" CausesValidation="false" runat="server" OnClick="lbtnAddPort_Click">
															<span class="glyphicon glyphicon-arrow-down" aria-hidden="true"  ></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="padding: 3px;">
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <div>

                                                                <%-- This GHead is added for Store Gridview Header  --%>
                                                                <div style="height: 215px; overflow-y: auto; overflow-x: hidden">
                                                                    <asp:GridView ID="dgvPort" CssClass="table table-hover table-striped " HeaderStyle-BackColor="#f3f3f3" runat="server" GridLines="None"
                                                                        PagerStyle-CssClass="cssPager" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True"
                                                                        AutoGenerateColumns="False">
                                                                        <EditRowStyle BackColor="MidnightBlue" />

                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="">
                                                                                <ItemTemplate>
                                                                                    <div style="margin-top: -3PX; width: 20px;">
                                                                                        <asp:LinkButton ID="lbtnDelPort" runat="server" CausesValidation="false"
                                                                                            OnClientClick="ConfirmDelete();" OnClick="lbtnDelPort_Click">
                                                                                            <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="gridHeaderAlignLeft" />
                                                                                <ItemStyle CssClass="gridHeaderAlignLeft" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="From Port Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblFrCode" Text='<%# Bind("FromPortCd") %>' runat="server" Width="90px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="gridHeaderAlignLeft" />
                                                                                <ItemStyle CssClass="gridHeaderAlignLeft" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Description">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblFrDes" Text='<%# Bind("FromPortDesc") %>' runat="server" Width="120px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="gridHeaderAlignLeft" />
                                                                                <ItemStyle CssClass="gridHeaderAlignLeft" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="To Port Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblToCode" Text='<%# Bind("ToPortCd") %>' runat="server" Width="90px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Description">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblToDes" Text='<%# Bind("ToPortDesc") %>' runat="server" Width="142px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Lead Time CMB">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblLoadTime" runat="server" Text='<%# Bind("CmbTime") %>' Width="80px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                                <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="temp" runat="server" Text='' Width="20px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <PagerStyle CssClass="cssPager"></PagerStyle>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%--Item Div--%>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-12" style="padding-left: 1px; padding-right: 1px;">
                                                    <div class="panel panel-default">
                                                        <div class="panel panel-heading">
                                                            <b>Items</b>
                                                        </div>
                                                        <div class="panel panel-body">
                                                            <div class="col-md-12 padding0">
                                                                <div class="col-sm-3 padding0">
                                                                    <div class="panel panel-default">
                                                                        <div class="panel panel-body">
                                                                            <div class="row">
                                                                                <div class="col-md-12 padding0">
                                                                                    <div class="col-sm-4 labelText1 paddingRight0">
                                                                                        Main Category
                                                                                    </div>
                                                                                    <div class="col-sm-4 padding0">
                                                                                        <asp:TextBox AutoPostBack="true" runat="server" ID="txtMainCat" Style="text-transform: uppercase"
                                                                                            CausesValidation="false" CssClass="txtMainCat form-control" OnTextChanged="txtMainCat_TextChanged"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-sm-1" style="padding-left: 3px; padding-right: 0px;">
                                                                                        <asp:LinkButton ID="lbtnSeMainCat" CssClass="lbtn seCat" CausesValidation="false" runat="server" OnClick="lbtnSeMainCat_Click">
															                        <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                    <div class="col-sm-2 height22 padding0">
                                                                                        <div class="col-sm-4 height22" style="padding-left: 5px; padding-right: 3px; margin-top: 3px;">
                                                                                            <asp:CheckBox Checked="false" CssClass="chkAllMainCat chk" runat="server" ID="chkAllMainCat" OnCheckedChanged="chkAllMainCat_CheckedChanged" AutoPostBack="True"></asp:CheckBox>
                                                                                        </div>
                                                                                        <div class="col-sm-8 labelText1" style="padding-left: 3px; padding-right: 0px;">
                                                                                            <asp:Label runat="server" Text="All" />
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12 padding0">
                                                                                    <div class="col-sm-4 labelText1 paddingRight0">
                                                                                        Sub Category
                                                                                    </div>
                                                                                    <div class="col-sm-4 padding0">
                                                                                        <asp:TextBox AutoPostBack="true" runat="server" ID="txtSubCat" CausesValidation="false" CssClass="txtSubCat form-control" OnTextChanged="txtSubCat_TextChanged"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-md-1 paddingLeft0" style="padding-left: 3px; padding-right: 0px;">
                                                                                        <asp:LinkButton ID="lbtnSeSubCat" CssClass="lbtn seCat" CausesValidation="false" runat="server" OnClick="lbtnSeSubCat_Click">
															<span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                    <div class="col-sm-2 height22 padding0">
                                                                                        <div class="col-sm-4 height22" style="padding-left: 5px; padding-right: 3px; margin-top: 3px;">
                                                                                            <asp:CheckBox Checked="false" CssClass="chkAllSub chk" runat="server" ID="chkAllSub" OnCheckedChanged="chkAllSub_CheckedChanged" AutoPostBack="True"></asp:CheckBox>
                                                                                        </div>
                                                                                        <div class="col-sm-8 labelText1" style="padding-left: 3px; padding-right: 0px;">
                                                                                            <asp:Label runat="server" Text="All" />
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12 padding0">
                                                                                    <div class="col-sm-4 labelText1 paddingRight0">
                                                                                        Item Code
                                                                                    </div>
                                                                                    <div class="col-sm-4 padding0">
                                                                                        <asp:TextBox AutoPostBack="true" runat="server" ID="txtItemCode" CausesValidation="false" CssClass="txtItemCode form-control" OnTextChanged="txtItemCode_TextChanged"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-md-1 paddingLeft0" style="padding-left: 3px; padding-right: 0px;">
                                                                                        <asp:LinkButton ID="lbtnSeItemCode" CssClass="lbtn seCat" CausesValidation="false" runat="server" OnClick="lbtnSeItemCode_Click">
															<span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                    <div class="col-sm-2 height22 padding0">
                                                                                        <div class="col-sm-4 height22" style="padding-left: 5px; padding-right: 3px; margin-top: 3px;">
                                                                                            <asp:CheckBox Checked="false" runat="server" CssClass="chkAllItem chk" ID="chkAllItem" OnCheckedChanged="chkAllItem_CheckedChanged" AutoPostBack="True"></asp:CheckBox>
                                                                                        </div>
                                                                                        <div class="col-sm-8 labelText1" style="padding-left: 3px; padding-right: 0px;">
                                                                                            <asp:Label runat="server" Text="All" />
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12 padding0">
                                                                                    <div class="col-sm-4 labelText1 paddingRight0">
                                                                                        Brand
                                                                                    </div>
                                                                                    <div class="col-sm-4 padding0">
                                                                                        <asp:TextBox runat="server" ID="txtBrand" AutoPostBack="true" Style="text-transform: uppercase" CausesValidation="false"
                                                                                            CssClass=" txtBrand form-control" OnTextChanged="txtBrand_TextChanged"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-sm-1 paddingLeft0" style="padding-left: 3px; padding-right: 0px;">
                                                                                        <asp:LinkButton ID="lbtnSeBrand" CssClass="lbtn seCat" CausesValidation="false" runat="server" OnClick="lbtnSeBrand_Click">
															<span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                    <div class="col-sm-2 height22 padding0">
                                                                                        <div class="col-sm-4 height22" style="padding-left: 5px; padding-right: 3px; margin-top: 3px;">
                                                                                            <asp:CheckBox Checked="false" CssClass="chkAllBrand chk" runat="server" ID="chkAllBrand" OnCheckedChanged="chkAllBrand_CheckedChanged" AutoPostBack="True"></asp:CheckBox>
                                                                                        </div>
                                                                                        <div class="col-sm-8 labelText1" style="padding-left: 3px; padding-right: 0px;">
                                                                                            <asp:Label runat="server" Text="All" />
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12 padding0">
                                                                                    <div class="col-sm-4 labelText1 paddingRight0">
                                                                                        Model
                                                                                    </div>
                                                                                    <div class="col-sm-4 padding0">
                                                                                        <asp:TextBox AutoPostBack="true" runat="server" ID="txtModel" Style="text-transform: uppercase" CausesValidation="false"
                                                                                            CssClass="txtModel form-control" OnTextChanged="txtModel_TextChanged"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-sm-1 paddingLeft0" style="padding-left: 3px; padding-right: 0px;">
                                                                                        <asp:LinkButton ID="lbtnSeModel" CssClass="lbtn seCat" CausesValidation="false" runat="server" OnClick="lbtnSeModel_Click">
															<span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                    <div class="col-sm-2 height22 padding0">
                                                                                        <div class="col-sm-4 height22" style="padding-left: 5px; padding-right: 3px; margin-top: 3px;">
                                                                                            <asp:CheckBox Checked="false" CssClass="chkAllModel chk" runat="server" ID="chkAllModel" OnCheckedChanged="chkAllModel_CheckedChanged" AutoPostBack="True"></asp:CheckBox>
                                                                                        </div>
                                                                                        <div class="col-sm-8 labelText1" style="padding-left: 3px; padding-right: 0px;">
                                                                                            <asp:Label runat="server" Text="All" />
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3">
                                                                    <div class="col-md-1">
                                                                        <div class="row">
                                                                            <div class="buttonRow">
                                                                                <asp:LinkButton ID="lbtnAddSellItem" CssClass="lbtn" CausesValidation="false" runat="server" OnClick="lbtnAddSellItem_Click">
                                                        <span class="glyphicon glyphicon-arrow-right" aria-hidden="true"  ></span>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-10 padding0">
                                                                        <div class="panel panel-default" style="height: 115px; overflow-y: auto; overflow-x: hidden;">
                                                                            <asp:GridView ID="dgvSelItms" CssClass="table table-hover table-striped" runat="server" GridLines="None" PagerStyle-CssClass="cssPager"
                                                                                EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                                                                <EditRowStyle BackColor="MidnightBlue" />
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="">
                                                                                        <HeaderTemplate>
                                                                                            <%--<asp:CheckBox ID="chkboxSelectAll" OnCheckedChanged="chkboxSelectAll_CheckedChanged"  runat="server"  />--%>
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkSelect" runat="server" Width="10px" OnCheckedChanged="chkSelect_CheckedChanged"></asp:CheckBox>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle CssClass="gridHeaderAlignLeft" Width="10px" />
                                                                                        <ItemStyle CssClass="gridHeaderAlignLeft" Width="10px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Item Code">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("CODE") %>' Width="100px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle CssClass="gridHeaderAlignLeft" Width="200px" />
                                                                                        <ItemStyle CssClass="gridHeaderAlignLeft" Width="200px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Description">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("DESCRIPT") %>' Width="200px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle CssClass="gridHeaderAlignLeft" />
                                                                                        <ItemStyle CssClass="gridHeaderAlignLeft" />
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <PagerStyle CssClass="cssPager"></PagerStyle>
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <%--dilshan--%>
                                                                <div class="col-sm-3 padding0">
                                                                    <div class="panel panel-default">
                                                                        <div class="panel panel-body">
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-4 labelText1 ">
                                                                        Sup. Period
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                        <asp:DropDownList ID="ddlSupPrd" runat="server" class="form-control">
                                                                            <asp:ListItem Text="Profit"></asp:ListItem>
                                                                            <asp:ListItem Text="Cost"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 paddingLeft0">
                                                                    <div class="col-sm-4 labelText1 ">
                                                                        Sup. Remarks
                                                                    </div>
                                                                    <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                                        <asp:TextBox ID="txtSupRem" CausesValidation="false" runat="server" class="form-control" AutoPostBack="true" OnTextChanged="txtSupRem_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <%--end--%>
                                                                <div class="col-sm-3 paddingRight0">
                                                                    <div class="col-md-1">
                                                                        <div class="row buttonRow">
                                                                            <asp:LinkButton ID="lbtnAddSupItem" CssClass="lbtn" CausesValidation="false" runat="server"
                                                                                OnClick="lbtnAddSupItem_Click">
                                                                                <span class="glyphicon glyphicon-arrow-right" aria-hidden="true"  ></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-10 padding0">
                                                                        <div class="panel panel-default">
                                                                            <div>
                                                                                <div class="GHead" id="GHead"></div>
                                                                                <div style="height: 114px; overflow-y: auto; overflow-x: auto">
                                                                                    <asp:GridView ID="dgvSupItms" CssClass="dgvSupItms table table-hover table-striped " runat="server" GridLines="None" PagerStyle-CssClass="cssPager"
                                                                                        EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" OnRowDeleting="dgvSupItms_RowDeleting">
                                                                                        <EditRowStyle BackColor="MidnightBlue" />
                                                                                        <Columns>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="lbtnDelSupItems" runat="server" CausesValidation="false"
                                                                                                        OnClientClick="ConfirmDelete();" OnClick="lbtnDelSupItems_Click">
                                                                                                <span aria-hidden="true" class="glyphicon glyphicon-trash"></span>
                                                                                                    </asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Item Code">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblSupItem" runat="server" Text='<%# Bind("mbii_itm_cd") %>' Width="100px"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle CssClass="gridHeaderAlignLeft" />
                                                                                                <ItemStyle CssClass="gridHeaderAlignLeft" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Description">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblSupDescription" runat="server" Text='<%# Bind("mi_shortdesc") %>' Width="200px"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle CssClass="gridHeaderAlignLeft" />
                                                                                                <ItemStyle CssClass="gridHeaderAlignLeft" />
                                                                                            </asp:TemplateField>
                                                                                            <%--dilshan--%>
                                                                                            <asp:TemplateField HeaderText="Sup. Period">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblSupPeriod" runat="server" Text='<%# Bind("MBII_WARR_PERI") %>' Width="80px"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle CssClass="gridHeaderAlignLeft" />
                                                                                                <ItemStyle CssClass="gridHeaderAlignLeft" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Sup. Remarks">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblSupRemarks" runat="server" Text='<%# Bind("MBII_WARR_RMK") %>' Width="100px"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle CssClass="gridHeaderAlignLeft" />
                                                                                                <ItemStyle CssClass="gridHeaderAlignLeft" />
                                                                                            </asp:TemplateField>
                                                                                            <%--end--%>
                                                                                        </Columns>
                                                                                        <PagerStyle CssClass="cssPager"></PagerStyle>
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
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="update">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupSearch" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="testPanel" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel runat="server" ID="testPanel" DefaultButton="lbtnSearch">
        <div runat="server" id="test" class="panel panel-primary Mheight" style="width: 900px;">
            <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default" style="height: 370px;">
                <div class="panel-heading" style="height: 25px;">
                    <div class="col-sm-11"></div>
                    <div class="col-sm-1">
                        <asp:LinkButton ID="btnClose" runat="server">
                             <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                        </asp:LinkButton>
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
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy10" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-2 paddingRight5">
                                        <asp:DropDownList ID="cmbSearchbykey" runat="server" class="form-control" AutoPostBack="false">
                                        </asp:DropDownList>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="col-sm-2 labelText1">
                                Search by word
                            </div>

                            <asp:Label ID="lblSearchType" runat="server" Text="" Visible="False"></asp:Label>
                            <div class="col-sm-3 paddingRight5">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy8" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="dgvResult" CausesValidation="false" runat="server" AllowPaging="True" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True"
                                        GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" OnPageIndexChanging="dgvResult_PageIndexChanging" OnSelectedIndexChanged="dgvResult_SelectedIndexChanged">
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
    <asp:UpdatePanel runat="server" ID="UpdatePanel3">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popupSupplier" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="pnlPopSup" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel ID="pnlPopSup" runat="server" align="center">
        <asp:Label ID="Label5" runat="server" Text="Label" Visible="false"></asp:Label>
        <asp:ScriptManagerProxy ID="ScriptManagerProxy11" runat="server"></asp:ScriptManagerProxy>
        <asp:UpdatePanel ID="UpdatePanel26" runat="server">
            <ContentTemplate>
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <span>Alert</span>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblMsg1" Font-Size="12px" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblMsg2" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblMsg3" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label10" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label11" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height22">
                            </div>
                        </div>
                        <div class="row buttonRow">
                            <div class="col-sm-7">
                            </div>
                            <div class="col-sm-2">
                                <asp:LinkButton ID="lbtnNewSupOk" CausesValidation="false" runat="server" CssClass="floatRight"
                                    OnClick="lbtnNewSupOk_Click"> <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>Yes</asp:LinkButton>
                            </div>
                            <div class="col-sm-2">
                                <asp:LinkButton ID="lbtnNewSupNo" CausesValidation="false" runat="server" CssClass="floatRight"
                                    OnClick="lbtnNewSupNo_Click"> <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>No </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <script>
          <%--$('.dgvSupItms').ready(function () {
                var gridHeader = $('#<%=dgvSupItms.ClientID%>').clone(true); // Here Clone Copy of Gridview with style
                $(gridHeader).find("tr:gt(0)").remove(); // Here remove all rows except first row (header row)
                $('#<%=dgvSupItms.ClientID%> tr th').each(function (i) {
                    // Here Set Width of each th from gridview to new table(clone table) th 
                    $("th:nth-child(" + (i + 1) + ")", gridHeader).css('width', ($(this).width()).toString() + "px");
                });
                $('.GHead').append(gridHeader);
                $('.GHead').css('position', 'inherit');
                $('.GHead').css('width', '99%');
                $('#GHead').css('top', $('#<%=dgvSupItms.ClientID%>').offset().top);
                jQuery('#BodyContent_dgvSupItms tbody').children('tr').eq(1).remove();
            });--%>
        Sys.Application.add_load(fun);
        function fun() {
            if (typeof jQuery == 'undefined') {
                alert('jQuery is not loaded');
            }
            $('.txtPortTime').keypress(function (evt) {
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                var str = $(this).val();
                if (charCode == 46) {
                    return false;
                }
                if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                else {
                    return true;
                }
            });

            $('.txtName , .txtAddress1 , .txtAddress2,.txtContactPerson, .txtPhone, .txtFax, .txtEmail, .txtWebSite, .txtCountry, .txtCurrency, .txtCreditPeriod, .txtAccCode, .txtTaxCat, .txtTinNo, .txtTaxReg, .txtPortToCode, .txtPortToCode, .txtPortTime, .txtMainCat, .txtSubCat, .txtItemCode, .txtBrand, .txtModel').keypress(function (evt) {
                var charCode = (evt.which) ? evt.which : evt.which;
                console.log(charCode);
                var str = $('.txtCode').val();
                if (str == "" && charCode != 0) {
                    showStickyWarningToast('Please enter supplier code frist !');
                    return false;
                }
            });

            $('.txtContactPerson,.txtPhone, .txtFax, .txtEmail , .txtPortTime ').mousedown(function (e) {
                if (e.button == 2) {
                    alert('This functionality is disabled !');
                    return false;
                } else {
                    return true;
                }
            });

            $('.txtEmail').focusout(function () {
                var str = $(this).val();
                var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
                if (!emailReg.test(str)) {
                    showStickyWarningToast('Please enter a valid email address !!!');
                    $(this).val('');
                }
            });

            $('.txtPhone, .txtFax').keypress(function (evt) {
                evt = (evt) ? evt : window.event;
                //var charCode = (evt.which) ? evt.which : evt.keyCode;
                var charCode = evt.which;
                var str = jQuery(this).val();
                //alert(charCode);
                if ((charCode == 8) || (charCode == 9) || (charCode == 0)) {
                    return true;
                }
                else if (str.length < 15) {
                    if ((charCode < 58 && charCode > 47)) {
                        return true;
                    }
                    if ((charCode == 43)) {
                        // var no = str.value;
                        var result = "+" + str;
                        //console.log(result);
                        //  alert(result);
                        if (str.charAt(0) != "+") {
                            $(this).val(result)
                            $(this).value = result;
                            return false;
                        }
                        else {
                            return false;
                        }
                    }
                    else {
                        return false;
                    }
                }
                else {
                    $(this).value = str.substr(0, 15);
                    alert('Maximum 15 characters are allowed ');
                    return false;
                }
            });

            $('.txtCreditPeriod').keypress(function (evt) {
                // var ch = (evt.which) ? evt.which : evt.keyCode;
                var charCode = evt.which;
                var str = $(this).val();
                //console.log(charCode);
                if ((charCode == 8) || (charCode == 9) || (charCode == 0)) {
                    return true;
                }
                else if (str.length < 5) {
                    if (charCode > 47 && charCode < 58) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else {
                    $(this).value = str.substr(0, 5);
                    alert('Maximum 5 characters are allowed ');
                    return false;
                }
            });

            $('.chkAllMainCat input, .chkAllSub input, .chkAllItem input, .chkAllBrand input, .chkAllModel input').click(function () {
                var checked = $(this).prop("checked");
                if (checked) {
                    $('.chk input').prop('checked', false);
                    $(this).prop('checked', true);
                }
            });

            $('.chk input').click(function () {
                var str = $('.txtCode').val();
                if (str == "") {
                    showStickyWarningToast('Please enter a supplier code frist !');
                    return false;
                }
            });

            $('.lbtn').click(function () {
                console.log('click');
                var str = $('.txtCode').val();
                if (str == "") {
                    showStickyWarningToast('Please enter a supplier code frist !');
                    return false;
                }
            });

            $('.seCat').click(function () {
                $('.chk input').prop('checked', false);
            });
            $('#BodyContent_txtCode,#BodyContent_txtContactPerson,#BodyContent_txtPhone,#BodyContent_txtFax,#BodyContent_txtWebSite,#BodyContent_txtAccCode,#BodyContent_txtTinNo,#BodyContent_txtTaxReg,#BodyContent_txtNBTRate,#BodyContent_txtDivAmt,#BodyContent_txtSupRem').on('keypress', function (event) {
                var regex = new RegExp("^[`~!@#$%^&*()=+]+$");
                var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
                if (regex.test(key)) {
                    event.preventDefault();
                    return false;
                }
            });

            $('#BodyContent_txtName,#BodyContent_txtAddress1,#BodyContent_txtAddress2').on('keypress', function (event) {
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
