<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="BillOfQuantitiesNew.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Sales.BillOfQuantitiesNew" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function updateProgress(percentage) {
            document.getElementById('ProgressBar').style.width = percentage + "%";
        }
    </script>
    <script type="text/javascript">

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
        function UncheckOthers(objchkbox) {
            var objchkList = objchkbox.parentNode.parentNode.parentNode;
            var chkboxControls = objchkList.getElementsByTagName("input");
            for (var i = 0; i < chkboxControls.length; i++) {
                if (chkboxControls[i] != objchkbox && objchkbox.checked) {
                    chkboxControls[i].checked = false;
                }
            }
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
        function DeleteConfirm() {

            var selectedvalue = confirm("Do you want to delete item?, it will permanently delete, please verify...!!!");
            if (selectedvalue) {
                document.getElementById('<%=txtDeleteconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtDeleteconformmessageValue.ClientID %>').value = "No";
            }
        };
        function UpdateConfirm() {

            var selectedvalue = confirm("Do you want to update data?");
            if (selectedvalue) {
                document.getElementById('<%=txtUpdateconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtUpdateconformmessageValue.ClientID %>').value = "No";
            }
        };
        function ApproveConfirm() {

            var selectedvalue = confirm("Do you want to approve data?");
            if (selectedvalue) {
                document.getElementById('<%=txtApprovalconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtApprovalconformmessageValue.ClientID %>').value = "No";
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
            var selectedvalueOrd = confirm("Do you want to Clear ?");
            if (selectedvalueOrd) {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "No";
            }
        };
        function ConfirmCancleForm() {
            var selectedvalue = confirm("Do you want to cancle BOQ?");
            if (selectedvalue) {
                document.getElementById('<%=txtCancelconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtCancelconformmessageValue.ClientID %>').value = "No";
            }
        };

    </script>
    <script src="../../../Js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript">
        $(function () {
            var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "Project";
            $('#myTab a[href="#' + tabName + '"]').tab('show');
            $("#myTab a").click(function () {
                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
            });
        });
    </script>

    <style type="text/css">
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

        .cb label {
            margin-left: 6px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="upHeader">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait1" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait1" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait2" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait2" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdateProgress ID="UpdateProgress3" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait3" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait3" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdateProgress ID="UpdateProgress4" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel3">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait4" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait4" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:HiddenField ID="txtACancelconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtClearlconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtSavelconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtUpdateconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtApprovalconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtCancelconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtDeleteconformmessageValue" runat="server" />
    <asp:HiddenField ID="TabName" runat="server" />
    <asp:HiddenField ID="HiddenField1" runat="server" />

    <div class="panel panel-default marginLeftRight5">
        <div class="row">
            <div class="col-sm-12 buttonrow">
                <asp:UpdatePanel runat="server" ID="upHeader">
                    <ContentTemplate>
                        <div class="col-sm-12 buttonRow paddingRight5" id="divTopCheck" runat="server">
                            <div class="col-sm-7 buttonRow padding0">
                            </div>
                            <div class="col-sm-5 buttonRow padding0">
                                <div class="col-sm-2 paddingRight0">
                                    <asp:LinkButton ID="LinkButton5" runat="server" CssClass="floatRight" Visible="false">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Complete
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-2 paddingRight0">
                                    <asp:LinkButton ID="lbtnapprove" runat="server" CssClass="floatRight" OnClientClick="ApproveConfirm()" OnClick="lbtnapprove_Click">
                                        <span class="glyphicon glyphicon-thumbs-up" aria-hidden="true"></span>Approve
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-2 paddingRight0">
                                    <asp:LinkButton ID="lbtnSave" runat="server" CssClass="floatRight" OnClientClick="SaveConfirm()" OnClick="lbtnSave_Click"> 
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-2 paddingRight0">
                                    <asp:LinkButton ID="lbtnUpdate" runat="server" CssClass="floatRight" OnClientClick="UpdateConfirm()" OnClick="lbtnUpdate_Click"> 
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Update
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-2">
                                    <asp:LinkButton ID="lblUClear" runat="server" CssClass="floatRight" OnClick="lbtnclear_Click" OnClientClick="ConfirmClearForm();">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-2">
                                    <asp:LinkButton ID="lbtCancle" runat="server" CssClass="floatRight" OnClick="lbtCancle_Click" OnClientClick="ConfirmCancleForm();">
                                        <span class="glyphicon glyphicon-remove-circle" aria-hidden="true"></span>Cancel
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="row" id="divMainRow">
            <div class="panel-body paddingbottom0">
                <div class="col-sm-12">
                    <div class="panel panel-default marginLeftRight5">
                        <div class="panel-heading pannelheading  paddingtop0">
                            <strong>Bill of Quantities</strong>
                        </div>
                        <div class="panel-body">

                            <div class="bs-example">
                                <ul class="nav nav-tabs" id="myTab">
                                    <li class="active"><a href="#Project" data-toggle="tab">BOQ Details</a></li>
                                    <li><a href="#Cost" data-toggle="tab">Cost Details</a></li>
                                    <li><a href="#Revenue" data-toggle="tab">Revenue Details</a></li>
                                    <%--<li><a href="#Kit" data-toggle="tab">Kit Details</a></li>--%>


                                    <div class="col-sm-5 width450">
                                    </div>

                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <div class="col-sm-1 labelText1 ">
                                                BOQ Status:
                                            </div>
                                            <div class="col-sm-2 labelText1 " style="color: red; width: 15px">
                                                <asp:Label runat="server" ID="lblstatus"></asp:Label>
                                            </div>
                                            <%-- 
                                            <div class="col-sm-1 labelText1" style="padding-left:80px">Activation:</div>
                                            <div class="col-sm-2 labelText1" style="padding-left:50px">
                                                    <asp:RadioButtonList runat="server" ID="boqAct" CssClass="cb" RepeatDirection="Horizontal">
                                                        <asp:ListItem Selected="True" Text="Active" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="De active" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                            </div>
                                            --%>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ul>

                            </div>
                            <div class="tab-content">
                                <div class="tab-pane active" id="Project">
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-sm-12 height10">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="col-sm-6 paddingRight0 paddingLeft0">
                                                        <div class="panel panel-default">
                                                            <div class="panel-heading pannelheading height16 paddingtop0 ">
                                                                Customer
                                                            </div>
                                                            <div class="panel-body" id="2">
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <div class="col-sm-3">
                                                                            <div class="row">
                                                                                <div class="col-sm-3 labelText1">
                                                                                    Code
                                                                                </div>
                                                                                <div class="col-sm-7 paddingRight5">
                                                                                    <asp:TextBox ID="txtCustomer" Style="text-transform: uppercase" runat="server" TabIndex="7" OnTextChanged="txtCustomer_TextChanged"
                                                                                        CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-sm-2 paddingLeft0">
                                                                                    <asp:LinkButton ID="lbtncode" CausesValidation="false" runat="server" OnClick="lbtncode_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-sm-3">
                                                                            <div class="row">
                                                                                <div class="col-sm-3 labelText1">
                                                                                    NIC
                                                                                </div>
                                                                                <div class="col-sm-7 paddingRight5">
                                                                                    <asp:TextBox ID="txtNIC" Style="text-transform: uppercase" runat="server" OnTextChanged="txtNIC_TextChanged"
                                                                                        TabIndex="8" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-sm-2 paddingLeft0">
                                                                                    <asp:LinkButton ID="btnSearch_NIC" runat="server" OnClick="lbtnSearch_NIC_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-sm-3">
                                                                            <div class="row">

                                                                                <div class="col-sm-3 labelText1">
                                                                                    Mobile
                                                                                </div>
                                                                                <div class="col-sm-7 paddingRight5">
                                                                                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" MaxLength="10" OnTextChanged="txtMobile_TextChanged"
                                                                                        TabIndex="9" AutoPostBack="true"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-sm-2 paddingLeft0">
                                                                                    <asp:LinkButton ID="btnSearch_Mobile" runat="server" CausesValidation="false" OnClick="lbtnSearch_Mobile_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                </div>


                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <div class="col-sm-3">
                                                                            <div class="row">
                                                                                <div class="col-sm-3 labelText1">
                                                                                    Name
                                                                                </div>
                                                                                <div class="col-sm-7 paddingRight5">
                                                                                    <asp:DropDownList runat="server" ID="cmbTitle" AutoPostBack="true" TabIndex="11" CssClass="form-control">
                                                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                                                        <asp:ListItem>MR.</asp:ListItem>
                                                                                        <asp:ListItem>MRS.</asp:ListItem>
                                                                                        <asp:ListItem>MS.</asp:ListItem>
                                                                                        <asp:ListItem>MISS.</asp:ListItem>
                                                                                        <asp:ListItem>DR.</asp:ListItem>
                                                                                        <asp:ListItem>REV.</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                                <div class="col-sm-2 paddingLeft0">
                                                                                    <asp:TextBox ID="txtCusName" runat="server" Style="text-transform: uppercase" Width="328px" CssClass="form-control" TabIndex="12"
                                                                                        AutoPostBack="true"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>



                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <div class="col-sm-3">
                                                                            <div class="row">
                                                                                <div class="col-sm-3 labelText1">
                                                                                    Address
                                                                                </div>
                                                                                <div class="col-sm-6 paddingRight5">
                                                                                    <asp:TextBox ID="txtAddress1" runat="server" Style="text-transform: uppercase" TabIndex="13"
                                                                                        CssClass="form-control salesinvoaddresstxt" AutoPostBack="true"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <div class="col-sm-3">
                                                                            <div class="row">
                                                                                <div class="col-sm-3 labelText1">
                                                                                </div>
                                                                                <div class="col-sm-6 paddingRight5">
                                                                                    <asp:TextBox ID="txtAddress2" runat="server" Style="text-transform: uppercase" TabIndex="14" CssClass="form-control salesinvoaddresstxt" AutoPostBack="true"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row labelText1"></div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6 paddingRight0 paddingLeft5">
                                                        <div class="panel panel-default">
                                                            <div class="panel-heading pannelheading height16 paddingtop0 ">
                                                                BOQ
                                                            </div>
                                                            <div class="panel-body">
                                                                <div class="row">

                                                                    <div class="col-sm-6" style="width: 310px">
                                                                        <div class="row" style="width: 280px">
                                                                            <div class="col-sm-4 labelText1">
                                                                                BOQ code 
                                                                            </div>
                                                                            <div class="col-sm-6">
                                                                                <asp:TextBox Width="160px" ReadOnly="true" runat="server" ID="txtdoc" AutoPostBack="true" OnTextChanged="txtdoc_TextChanged" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-sm-1 paddingLeft0 paddingRight0" style="padding-left: 40px">
                                                                                <asp:LinkButton ID="lbtnProCode" runat="server" CausesValidation="false" OnClick="lbtnProCode_Click">
                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                        </div>

                                                                        <div class="row" style="width: 280px">
                                                                            <div class="col-sm-4 labelText1">
                                                                                Ref #
                                                                            </div>
                                                                            <div class="col-sm-6">
                                                                                <asp:TextBox runat="server" ID="txtref" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                            </div>

                                                                        </div>

                                                                        <div class="row" style="width: 280px">
                                                                            <div class="col-sm-4 labelText1">
                                                                                Date 
                                                                            </div>
                                                                            <div class="col-sm-6  ">
                                                                                <asp:TextBox runat="server" Enabled="false" ID="txtDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                <asp:LinkButton ID="lbtnDate" Visible="false" runat="server" CausesValidation="false">
                                                                                    <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDate"
                                                                                    PopupButtonID="lbtnDate" Format="dd/MMM/yyyy">
                                                                                </asp:CalendarExtender>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row" style="width: 280px">
                                                                            <div class="col-sm-4 labelText1">
                                                                                Commence Date 
                                                                            </div>
                                                                            <div class="col-sm-6  ">
                                                                                <asp:TextBox runat="server" Enabled="false" ID="txtComme" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                <asp:LinkButton ID="lbtnComm" runat="server" CausesValidation="false">
                                                                                    <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtComme"
                                                                                    PopupButtonID="lbtnComm" Format="dd/MMM/yyyy">
                                                                                </asp:CalendarExtender>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row" style="width: 280px">
                                                                            <div class="col-sm-4 labelText1">
                                                                                Complete Date 
                                                                            </div>
                                                                            <div class="col-sm-6  ">
                                                                                <asp:TextBox runat="server" Enabled="false" ID="txtcompletedate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-sm-1 paddingLeft0">
                                                                                <asp:LinkButton ID="lbtncompletedate" runat="server" CausesValidation="false">
                                                                                    <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtcompletedate"
                                                                                    PopupButtonID="lbtncompletedate" Format="dd/MMM/yyyy">
                                                                                </asp:CalendarExtender>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6 paddingRight15">
                                                                        <div class="row">
                                                                            <div class="col-sm-2 labelText1 padding0">
                                                                                Location
                                                                            </div>
                                                                            <div class="col-sm-3 paddingRight0">
                                                                                <asp:TextBox runat="server" ID="txtlocation" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-sm-1 paddingLeft5 paddingRight0">
                                                                                <asp:LinkButton ID="lbtnloc" runat="server" CausesValidation="false" OnClick="lbtnloc_Click">
                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                            <div class="col-sm-10">
                                                                                <asp:TextBox ReadOnly="true" runat="server" ID="txtLocDesc" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">

                                                                            <div class="col-sm-2 padding0 labelText1">
                                                                                Price Book
                                                                            </div>
                                                                            <div class="col-sm-6  paddingRight0">
                                                                                <asp:DropDownList ID="ddlPriceBook" AutoPostBack="true" CausesValidation="false" runat="server" CssClass="form-control" OnTextChanged="ddlPriceBook_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">

                                                                            <div class="col-sm-2 padding0 labelText1">
                                                                                Price Level
                                                                            </div>
                                                                            <div class="col-sm-6 paddingRight0">
                                                                                <asp:DropDownList ID="ddlLevel" CausesValidation="false" runat="server" CssClass="form-control">
                                                                                </asp:DropDownList>
                                                                            </div>

                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-sm-2 labelText1 padding0">
                                                                                Sales Exe.
                                                                            </div>
                                                                            <div class="col-sm-3 paddingRight0">
                                                                                <asp:TextBox ID="txtexcutive" runat="server" AutoPostBack="true" OnTextChanged="txtexcutive_TextChanged" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-sm-1 paddingLeft5 paddingRight0">
                                                                                <asp:LinkButton ID="lbtnEx" runat="server" CausesValidation="false" OnClick="lbtnEx_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                            <div class="col-sm-6 paddingLeft10">
                                                                                <asp:TextBox ID="txtExcDesc" ReadOnly="true" runat="server" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height10">
                                                </div>
                                            </div>
                                            <div class="row">

                                                <div class="col-sm-6 paddingRight0">
                                                    <%--                                          <div class="progress progress-striped active progress-success" style="height: 43px">
      <div id="ProgressBar" class="progress-bar" role="progressbar" runat="server"
          aria-valuemin="0" aria-valuemax="100" style="width: 50%">
      </div>
 </div>--%>
                                                    <div class="panel panel-default">
                                                        <div class="panel-body">

                                                            <div class="col-sm-6">
                                                                <asp:Chart ID="cTestChart" runat="server">
                                                                    <Series>
                                                                        <asp:Series Name="Testing" YValueType="Int32">

                                                                            <Points>
                                                                                <%-- <asp:DataPoint AxisLabel="Cost" YValues="40" />
                                                                                <asp:DataPoint AxisLabel="Revenue" YValues="50" />--%>
                                                                            </Points>
                                                                        </asp:Series>
                                                                    </Series>
                                                                    <ChartAreas>
                                                                        <asp:ChartArea Name="ChartArea1">
                                                                        </asp:ChartArea>

                                                                    </ChartAreas>
                                                                </asp:Chart>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <div class="row">
                                                                    <div class="col-sm-3 padding0 labelText1">
                                                                        Total Cost:
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <asp:Label runat="server" ID="lblcost" ForeColor="#A513D0"></asp:Label>
                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-3 padding0 labelText1">
                                                                        Total Revenue:
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <asp:Label runat="server" ID="lblRevenue" ForeColor="#A513D0"></asp:Label>
                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-3 padding0 labelText1">
                                                                        Profit:
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <asp:Label runat="server" ID="lblprofitvalue" ForeColor="#A513D0"></asp:Label>
                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-3 padding0 labelText1">
                                                                        Profit %:
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <asp:Label runat="server" ID="lblprofit" ForeColor="#A513D0"></asp:Label>
                                                                    </div>

                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 paddingLeft5">
                                                    <div class="panel panel-default">
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-sm-2 labelText1">
                                                                    Remark
                                                                </div>
                                                                <div class="col-sm-10">
                                                                    <asp:TextBox runat="server" ID="txtremark" TextMode="MultiLine" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 paddingLeft5">
                                                    <div class="panel panel-default">
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-sm-2 labelText1">
                                                                    Current Utilization
                                                                </div>
                                                                <div class="col-sm-5">
                                                                    <asp:TextBox runat="server" ID="currentUtilization" ReadOnly="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="tab-pane" id="Cost">
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-sm-12 height10">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height10">
                                                </div>
                                            </div>
                                            <div class="row" id="kitCode" runat="server" visible="true">
                                                <div class="col-sm-3">
                                                    <div class="col-sm-3 labelText1">
                                                        Kit Code
                                                    </div>
                                                    <div class="col-sm-7 paddingRight5">
                                                        <asp:TextBox ID="txtkititemcode" Style="text-transform: uppercase" runat="server" TabIndex="7" OnTextChanged="txtkititemcode_TextChanged"
                                                            CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-2 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnkititem" Visible="true" CausesValidation="false" runat="server" OnClick="lbtnkititem_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="row">
                                                        <div class="col-sm-3 labelText1">
                                                            Qty
                                                        </div>
                                                        <div class="col-sm-8 paddingRight0">
                                                            <asp:TextBox ID="txtkitqty" onkeypress="return jsDecimals(event);" Style="text-align: right" TabIndex="8" runat="server"
                                                                CssClass="form-control"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="row">
                                                        <div class="col-sm-3 labelText1">
                                                            Unit Cost
                                                        </div>
                                                        <div class="col-sm-6 paddingRight0">
                                                            <asp:TextBox ID="KitCostTextBox" Style="text-align: right" TabIndex="9" onkeypress="return jsDecimals(event);" runat="server"
                                                                CssClass="form-control" AutoPostBack="true" OnTextChanged="txtUnitCost_TextChanged"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="row">
                                                        <div class="col-sm-3 labelText1">
                                                            Unit Price
                                                        </div>
                                                        <div class="col-sm-6 paddingRight0">
                                                            <asp:TextBox ID="KitPriceTextBox" onkeypress="return jsDecimals(event);" Style="text-align: right" TabIndex="10" runat="server"
                                                                CssClass="form-control" AutoPostBack="true" OnTextChanged="txtUnitPrice_TextChanged"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">

                                                <div class="col-sm-3">
                                                    <div class="row">
                                                        <div class="col-sm-3 labelText1">
                                                            Total Cost
                                                        </div>
                                                        <div class="col-sm-6 paddingRight0">
                                                            <asp:TextBox ID="KitTotalCost" onkeypress="filterDigits(event)" Style="text-align: right" TabIndex="101" runat="server"
                                                                CssClass="form-control" AutoPostBack="true" ReadOnly="true"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="row">
                                                        <div class="col-sm-3 labelText1">
                                                            Total Price
                                                        </div>
                                                        <div class="col-sm-6 paddingRight0">
                                                            <asp:TextBox ID="KitTotalPrice" onkeypress="filterDigits(event)" Style="text-align: right" TabIndex="101" runat="server"
                                                                CssClass="form-control" AutoPostBack="true" ReadOnly="true"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                </div>
                                                    <div class="col-sm-3">
                                                    <div class="row">
                                                        <div class="col-sm-3 labelText1">
                                                            Remarks
                                                        </div>
                                                        <div class="col-sm-6 paddingRight0">
                                                            <asp:TextBox ID="TextBoxRemarks"  Style="text-align: right" TabIndex="11" runat="server"
                                                                CssClass="form-control"  ></asp:TextBox>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="col-sm-1 padding0">
                                                    <asp:LinkButton ID="lbtnKitadd" runat="server" TabIndex="12" CausesValidation="false" OnClick="lbtnKitadd_Click">
                                                                     <span class="glyphicon glyphicon-plus" style="font-size:20px;"  aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="col-sm-3 padding0">
                                                        <asp:LinkButton ID="buttonExcel" runat="server" TabIndex="103" CausesValidation="false" OnClick="lbtnexcel_download">
                                                                     <span class="glyphicon glyphicon-circle-arrow-down" style="font-size:20px;"  aria-hidden="true"></span>Excel Download
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div class="col-sm-4 padding0">
                                                        <asp:LinkButton ID="buttonExcelupload" runat="server" TabIndex="103" CausesValidation="false" OnClick="btnExcelDataUpload_Click">
                                                                     <span class="glyphicon glyphicon-circle-arrow-up" style="font-size:20px;"  aria-hidden="true"></span>Excel Upload
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div style="height: 30px;">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="panel panel-default " id="y5">
                                                        <div class="panel-heading pannelheading height16 paddingtop0">
                                                            <div class="row">
                                                                <div class="col-sm-1 paddingRight0">
                                                                    Item details
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-sm-2">
                                                                    <div class="row">
                                                                        <div class="col-sm-3 labelText1">
                                                                            Item
                                                                        </div>
                                                                        <div class="col-sm-8 paddingRight0">
                                                                            <asp:TextBox ID="txtCItem" Style="text-transform: uppercase" runat="server" TabIndex="100" OnTextChanged="txtItem_TextChanged"
                                                                                CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0">
                                                                            <asp:LinkButton ID="LinkButton1" CausesValidation="false" runat="server" OnClick="btnSearch_Item_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-1">
                                                                    <div class="row">
                                                                        <div class="col-sm-3 labelText1">
                                                                            Qty
                                                                        </div>
                                                                        <div class="col-sm-8 paddingRight0">
                                                                            <asp:TextBox ID="txtCQty" onkeydown="return jsDecimals(event);" Style="text-align: right" TabIndex="101" runat="server" OnTextChanged="txtQty_TextChanged"
                                                                                CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-2">
                                                                    <div class="row">
                                                                        <div class="col-sm-4 labelText1">
                                                                            Unit Cost
                                                                        </div>
                                                                        <div class="col-sm-8 paddingRight0">
                                                                            <asp:TextBox ID="txtunitcost" onkeydown="return jsDecimals(event);" Style="text-align: right" TabIndex="102" runat="server" OnTextChanged="txtunitcost_TextChanged"
                                                                                CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-2">
                                                                    <div class="row">
                                                                        <div class="col-sm-3 labelText1">
                                                                            Total Cost
                                                                        </div>
                                                                        <div class="col-sm-8 paddingRight0">
                                                                            <asp:TextBox ID="txtCtotal" Enabled="false" onkeydown="return jsDecimals(event);" Style="text-align: right" runat="server" OnTextChanged="txtCustomer_TextChanged"
                                                                                CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-2 padding0">
                                                                    <asp:LinkButton ID="lbtnaddcost" runat="server" TabIndex="103" CausesValidation="false" OnClick="lbtnaddcost_Click">
                                                                     <span class="glyphicon glyphicon-plus" style="font-size:20px;"  aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <br />
                                                            <div class="row">
                                                                <div class="col-sm-12">


                                                                    <div class="col-sm-3 paddingLeft0">
                                                                        <div class="row">
                                                                            <div class="col-sm-3 labelText1" style="font-weight: bolder">
                                                                                Description:
                                                                            </div>
                                                                            <div class="col-sm-9 paddingRight0" style="margin-top: 3px">
                                                                                <asp:Label ID="lblItemDescription" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-2 padding0">
                                                                        <div class="row">
                                                                            <div class="col-sm-2 labelText1 padding0" style="font-weight: bolder">
                                                                                Model:
                                                                            </div>
                                                                            <div class="col-sm-10" style="margin-top: 3px">
                                                                                <asp:Label ID="lblItemModel" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-2 padding0">
                                                                        <div class="row">
                                                                            <div class="col-sm-2 labelText1" style="font-weight: bolder">
                                                                                Brand:
                                                                            </div>
                                                                            <div class="col-sm-10" style="margin-top: 3px">
                                                                                <asp:Label ID="lblItemBrand" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-2 padding0">
                                                                        <div class="row">
                                                                            <div class="col-sm-4 labelText1 padding0" style="font-weight: bolder">
                                                                                UOM :
                                                                            </div>
                                                                            <div class="col-sm-8" style="margin-top: 3px">
                                                                                <asp:Label ID="lblUom" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-3 padding0">
                                                                        <div class="row">
                                                                            <div class="col-sm-4 labelText1" style="font-weight: bolder">
                                                                                Serial Status:
                                                                            </div>
                                                                            <div class="col-sm-8" style="margin-top: 3px">
                                                                                <asp:Label ID="lblItemSerialStatus" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-6">
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="col-sm-2 text-right labelText1" style="padding-right: 3px;">
                                                                Kit Item 
                                                            </div>
                                                            <div class="col-sm-3 padding0">
                                                                <asp:TextBox CssClass="form-control" runat="server" AutoPostBack="true" ID="txtKitCode" OnTextChanged="txtKitCode_TextChanged" />
                                                            </div>
                                                            <div class="col-sm-1 labelText1" style="padding-left: 3px; padding-right: 0px; margin-top: -3px;">
                                                                <asp:LinkButton ID="lbtnClearKitCode" CausesValidation="false" runat="server" OnClick="lbtnClearKitCode_Click">
                                                                            <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="col-sm-5 labelText1" style="padding-left: 3px; padding-right: 0px;">
                                                                <asp:LinkButton ID="lbtnKitBrekUp" CausesValidation="false" runat="server" OnClick="lbtnKitBrekUp_Click">
                                                                            <span class="glyphicon " aria-hidden="true"></span><strong>KIT Breakup</strong>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row ">
                                                <div class="col-sm-12 ">
                                                    <div class="row">
                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>
                                                    <div class="bs-example">
                                                        <ul class="nav nav-tabs" id="mysubTab">
                                                            <%--<li><a href="#Costdet" data-toggle="tab" runat="server" onclick="CostdetClick" autopostback="true" >Cost Details</a></li>
                                                            <li><a href="#Kit" data-toggle="tab" runat="server" onclick="KitdetClick" autopostback="true" >Kit Summary</a></li>--%>
                                                            <asp:LinkButton ID="LinkButton3" CausesValidation="false" runat="server" OnClick="KitdetClick">
                                                                            <span class="glyphicon " aria-hidden="true"></span><strong>KIT Summary</strong>
                                                            </asp:LinkButton>
                                                            <asp:LinkButton ID="LinkButton2" CausesValidation="false" runat="server" OnClick="CostdetClick">
                                                                            <span class="glyphicon " aria-hidden="true"></span><strong>Kit Item Details</strong>
                                                            </asp:LinkButton>

                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="itemdetailsdiv" runat="server" visible="false">
                                                <div class="tab-pane" id="Costdet">
                                                    <div class="panelscoll250">
                                                        <asp:GridView ID="grdCost" AutoGenerateColumns="False" TabIndex="39" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                            <Columns>
                                                                <%--<asp:BoundField DataField="itri_seq_no" HeaderText="Seq No" ItemStyle-Width="150" ReadOnly="true" />--%>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbtnDetaltecost" CausesValidation="false" CommandName="Delete" runat="server" OnClientClick="DeleteConfirm()" OnClick="lbtnDetaltecost_Click">
                                                                            <span class="glyphicon glyphicon-trash" aria-hidden="true" style="font-size:15px"></span> 
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <%--<ItemStyle Width="2px" />--%>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Item">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblspd_itm" runat="server" Text='<%# Bind("spd_itm") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <%--<ItemStyle Width="50px" />--%>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Kit Item">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSPD_KIT_ITM" runat="server" Text='<%# Bind("SPD_KIT_ITM") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <%--<ItemStyle Width="50px" />--%>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="UOM">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSPD_MI_ITM_UOM" runat="server" Text='<%# Bind("SPD_MI_ITM_UOM") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <%--<ItemStyle Width="50px" />--%>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Description">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblspd_itm_desc" runat="server" Text='<%# Bind("spd_itm_desc") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <%--<ItemStyle Width="50px" />--%>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Unit Cost">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblspd_est_unit" runat="server" Text='<%# Bind("SPD_ACT_COST","{0:n2}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                    <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                    <%--<ItemStyle Width="50px" HorizontalAlign="Right"/>--%>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Qty">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblspd_est_qty" runat="server" Text='<%# Bind("spd_est_qty","{0:n2}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                    <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                    <%--<ItemStyle Width="50px" />--%>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total Cost">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSPD_EST_COST" runat="server" Text='<%# Bind("SPD_EST_COST","{0:n2}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="gridHeaderAlignRight" />
                                                                    <HeaderStyle CssClass="gridHeaderAlignRight" />
                                                                    <%--<ItemStyle Width="50px" />--%>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Doc" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblspd_no" runat="server" Text='<%# Bind("spd_no") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <%--<ItemStyle Width="50px" />--%>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Doc" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblspd_seq" runat="server" Text='<%# Bind("spd_seq") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <%--<ItemStyle Width="50px" />--%>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="kitdetailsdiv" runat="server">
                                                <div class="tab-pane" id="Kit">
                                                    <div class="panelscoll250">
                                                        <asp:GridView ID="SatProjectGridView" AutoGenerateColumns="False" TabIndex="39" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbtnDetalteKit" CausesValidation="false" CommandName="Delete" runat="server" OnClick="lbtnDetalteKit_Click">
                                                                            <span class="glyphicon glyphicon-trash" aria-hidden="true" style="font-size:15px"></span> 
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>                                                             
                                                                </asp:TemplateField>
                                                                <%--<asp:BoundField DataField="itri_seq_no" HeaderText="Seq No" ItemStyle-Width="150" ReadOnly="true" />--%>
                                                                <asp:TemplateField HeaderText="KitCode">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblspk_kit_cd" runat="server" Text='<%# Bind("SPK_KIT_CD") %>'></asp:Label>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Model">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblspk_kit_model" runat="server" Text='<%# Bind("SPK_KIT_MODEL") %>'></asp:Label>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>

                                                                <%--<asp:TemplateField HeaderText="Description">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblspk_kit_desc" runat="server" Text='<%# Bind("SPK_KIT_DESC") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>

                                                                 <asp:TemplateField HeaderText="Description">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtspkKittextBox"  runat="server" Text='<%# Bind("SPK_KIT_DESC") %>'></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblspk_kit_desc" runat="server" Text='<%# Bind("SPK_KIT_DESC") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Qty">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblspk_kit_qty" runat="server" Text='<%# Bind("SPK_QTY","{0:n2}") %>'></asp:Label>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Unit Cost">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblspk_kit_cost" runat="server" Text='<%# Bind("SPK_COST","{0:n2}") %>'></asp:Label>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Unit Price">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblspk_kit_price" runat="server" Text='<%# Bind("SPK_UNIT_PRICE","{0:n2}") %>'></asp:Label>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total Cost">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblspk_kit_total_cost" runat="server" Text='<%# Bind("SPK_TOTAL_COST","{0:n2}") %>'></asp:Label>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total Price">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblspk_kit_total_price" runat="server" Text='<%# Bind("SPK_TOTAL_PRICE","{0:n2}") %>'></asp:Label>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>

                                                                 <asp:TemplateField HeaderText="Remarks">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblspk_kit_remarks" runat="server" Text='<%# Bind("SPK_RMK") %>'></asp:Label>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>

                                                                <asp:TemplateField ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbtngrdRevenuEdit" CausesValidation="false" runat="server" OnClick="lbtngrdKitEdit_Click">
                                                                <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:LinkButton ID="lbtngrdRevenueUpdate" runat="server" CausesValidation="false" CommandName="Update" OnClick="lbtngrdKitUpdate_Click">
                                                                <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                                        </asp:LinkButton>

                                                                    </EditItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" Width="1%" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height10">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                </div>
                                                <div class="col-sm-6">
                                                    <div class="row">
                                                        <div class="col-sm-3 labelText1">
                                                            Total:
                                                        </div>
                                                        <div class="col-sm-9 paddingRight0" style="margin-top: 3px">
                                                            <asp:Label ID="lblTotalCost" runat="server"></asp:Label>
                                                        </div>                                                       
                                                    </div>
                                                </div>
                                                <div class="col-sm-6">
                                                     <div class="col-sm-3 labelText1">
                                                            Current Utilization:
                                                        </div>
                                                        <div class="col-sm-9 paddingRight0" style="margin-top: 3px">
                                                            <asp:Label ID="LabelCurrentCost" runat="server"></asp:Label>
                                                        </div>
                                                </div>



                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="tab-pane" id="Revenue">
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-sm-12 height10">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="panel panel-default ">
                                                        <div class="panel-heading pannelheading height16 paddingtop0">
                                                            <div class="row">
                                                                <div class="col-sm-1 paddingRight0">
                                                                    Item details
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-sm-2">
                                                                    <div class="row">
                                                                        <div class="col-sm-3 labelText1">
                                                                            Item
                                                                        </div>
                                                                        <div class="col-sm-8 paddingRight0">
                                                                            <asp:TextBox ID="txtRitem" Style="text-transform: uppercase" runat="server" TabIndex="7" OnTextChanged="txtRitem_TextChanged"
                                                                                CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0">
                                                                            <asp:LinkButton ID="lbtnSearch_Item2" CausesValidation="false" runat="server" OnClick="lbtnSearch_Item2_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-1">
                                                                    <div class="row">
                                                                        <div class="col-sm-3 labelText1">
                                                                            Qty
                                                                        </div>
                                                                        <div class="col-sm-8 paddingRight0">
                                                                            <asp:TextBox ID="txtRQty" onkeydown="return jsDecimals(event);" Style="text-align: right" runat="server" TabIndex="7" OnTextChanged="txtRQty_TextChanged"
                                                                                CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-2">
                                                                    <div class="row">
                                                                        <div class="col-sm-4 labelText1">
                                                                            Revenue
                                                                        </div>
                                                                        <div class="col-sm-8 paddingRight0">
                                                                            <asp:TextBox ID="txtunitRevenue" onkeydown="return jsDecimals(event);" Style="text-align: right" runat="server" TabIndex="7" OnTextChanged="txtunitRevenue_TextChanged"
                                                                                CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-2">
                                                                    <div class="row">
                                                                        <div class="col-sm-3 labelText1">
                                                                            Total
                                                                        </div>
                                                                        <div class="col-sm-8 paddingRight0">
                                                                            <asp:TextBox ID="txtRtotal" Enabled="false" onkeydown="return jsDecimals(event);" Style="text-align: right" runat="server" TabIndex="7" OnTextChanged="txtCustomer_TextChanged"
                                                                                CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-2 padding0">
                                                                    <asp:LinkButton ID="lbtnaddRevenue" runat="server" TabIndex="38" CausesValidation="false" OnClick="lbtnaddRevenue_Click">
                                                                     <span class="glyphicon glyphicon-plus" style="font-size:20px;"  aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">

                                                                    <div class="row">
                                                                        <div class="col-sm-12">


                                                                            <div class="col-sm-3 paddingLeft0">
                                                                                <div class="row">
                                                                                    <div class="col-sm-3 labelText1" style="font-weight: bolder">
                                                                                        Description:
                                                                                    </div>
                                                                                    <div class="col-sm-9 paddingRight0" style="margin-top: 3px">
                                                                                        <asp:Label ID="lblRItemDescription" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-3 padding0">
                                                                                <div class="row">
                                                                                    <div class="col-sm-2 labelText1" style="font-weight: bolder">
                                                                                        Model:
                                                                                    </div>
                                                                                    <div class="col-sm-10" style="margin-top: 3px">
                                                                                        <asp:Label ID="lblRItemModel" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-3">
                                                                                <div class="row">
                                                                                    <div class="col-sm-2 labelText1" style="font-weight: bolder">
                                                                                        Brand:
                                                                                    </div>
                                                                                    <div class="col-sm-10" style="margin-top: 3px">
                                                                                        <asp:Label ID="lblRItemBrand" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="col-sm-3">
                                                                                <div class="row">
                                                                                    <div class="col-sm-4 labelText1" style="font-weight: bolder">
                                                                                        Serial Status:
                                                                                    </div>
                                                                                    <div class="col-sm-8" style="margin-top: 3px">
                                                                                        <asp:Label ID="lblRItemSerialStatus" runat="server" ForeColor="#A513D0"></asp:Label>
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
                                            <div class="row">
                                                <div class="col-sm-12 height10">
                                                </div>
                                            </div>
                                            <div class="row ">
                                                <div class="col-sm-12 ">
                                                    <div class="row">
                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>
                                                    <div class="panelscoll250">
                                                        <asp:GridView ID="grdRevenue" AutoGenerateColumns="False" TabIndex="39" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                            <Columns>
                                                                <%--<asp:BoundField DataField="itri_seq_no" HeaderText="Seq No" ItemStyle-Width="150" ReadOnly="true" />--%>
                                                                <asp:TemplateField HeaderText="Item">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblspd_itm" runat="server" Text='<%# Bind("spd_itm") %>'></asp:Label>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Description">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblspd_itm_desc" runat="server" Text='<%# Bind("spd_itm_desc") %>'></asp:Label>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Unit Cost">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblspd_est_unit" runat="server" Text='<%# Bind("SPD_ACT_COST","{0:n2}") %>'></asp:Label>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Qty">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblspd_est_qty" runat="server" Text='<%# Bind("spd_est_qty","{0:n2}") %>'></asp:Label>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total Cost">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblspd_est_cost" runat="server" Text='<%# Bind("spd_est_cost","{0:n2}") %>'></asp:Label>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total Revenue">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtspd_est_rev" onkeypress="return isNumberKey(event)" runat="server" Text='<%# Bind("spd_est_rev","{0:N2}") %>'></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblspd_est_rev" runat="server" Text='<%# Bind("spd_est_rev","{0:N2}") %>'></asp:Label>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Doc" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblspd_no" runat="server" Text='<%# Bind("spd_no") %>'></asp:Label>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Doc" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblspd_seq" runat="server" Text='<%# Bind("spd_seq") %>'></asp:Label>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>
                                                                <asp:TemplateField ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbtngrdRevenuEdit" CausesValidation="false" runat="server" OnClick="lbtngrdRevenuEdit_Click">
                                                                <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:LinkButton ID="lbtngrdRevenueUpdate" runat="server" CausesValidation="false" CommandName="Update" OnClick="lbtngrdRevenueUpdate_Click">
                                                                <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                                        </asp:LinkButton>

                                                                    </EditItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" Width="1%" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height10">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-10">
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="row">
                                                        <div class="col-sm-3 labelText1">
                                                            Total:
                                                        </div>
                                                        <div class="col-sm-9 paddingRight0" style="margin-top: 3px">
                                                            <asp:Label ID="lblTotalRevenue" runat="server"></asp:Label>
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
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy6" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
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

    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserDPopoup" runat="server" Enabled="True" TargetControlID="Button2"
                PopupControlID="pnlDpopup" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel21" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlDpopup" DefaultButton="lbtnSearch">
                <div runat="server" id="Div20" class="panel panel-default height400 width1085">

                    <asp:Label ID="Label12" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="btnDClose" runat="server" OnClick="btnDClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                            </asp:LinkButton>
                            <%--<span>Commen Search</span>--%>
                            <div class="col-sm-11">
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12" id="Div21" runat="server">
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
                                            <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtFDate"
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
                                            <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtTDate"
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
                                <div class="col-sm-7">

                                    <div class="row">

                                        <div class="col-sm-3 labelText1">
                                            Search by key
                                        </div>
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy18" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                            <ContentTemplate>
                                                <div class="col-sm-8 paddingRight5">
                                                    <asp:DropDownList ID="ddlSearchbykeyD" TabIndex="202" runat="server" class="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3 labelText1">
                                            Search by word
                                        </div>
                                        <div class="col-sm-8 paddingRight5">
                                            <asp:TextBox ID="txtSearchbywordD" CausesValidation="false" TabIndex="203" placeholder="Search by word" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbywordD_TextChanged"></asp:TextBox>
                                        </div>
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy19" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                            <ContentTemplate>
                                                <div class="col-sm-1 paddingLeft0">
                                                    <asp:LinkButton ID="lbtnSearchD" runat="server" OnClick="lbtnSearchD_Click">
                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </ContentTemplate>

                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtSearchbywordD" />
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
                                        <asp:ScriptManagerProxy ID="ScriptManagerProxy20" runat="server"></asp:ScriptManagerProxy>
                                        <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdResultD" CausesValidation="false" runat="server" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" GridLines="None" CssClass="table table-hover table-striped" PageSize="5" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResultD_SelectedIndexChanged" OnPageIndexChanging="grdResultD_PageIndexChanging">
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

                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <%-- pnl Kit --%>
    <asp:UpdatePanel runat="server" ID="UpdatePanel15">
        <ContentTemplate>
            <asp:Button ID="btnpop1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popKitBup" runat="server" Enabled="True" TargetControlID="btnpop1"
                PopupControlID="pnlKitBup" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress5" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="upPnlKitPop">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait101" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait101" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:Panel runat="server" ID="pnlKitBup">
        <asp:UpdatePanel runat="server" ID="upPnlKitPop">
            <ContentTemplate>
                <div runat="server" id="Div5" class="panel panel-primary" style="padding: 5px;">
                    <div class="panel panel-default" style="height: 235px; width: 260px;">
                        <div class="panel-heading" style="height: 25px;">
                            <div class="col-sm-12 paddingLeft0">
                                <div class="col-sm-10 text-left">
                                    <%--<strong>KIT Codes Breakup</strong>--%>
                                </div>
                                <div class="col-sm-2 text-right">
                                    <asp:LinkButton ID="lbtnKitBupClose" runat="server" OnClick="lbtnKitBupClose_Click">
                                    <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div style="height: 200px; overflow-y: scroll;">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="dgvKitBup" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnSelectKit" runat="server" OnClick="lbtnSelectKit_Click">
                                                    <span class="glyphicon glyphicon-arrow-down"  aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="KIT Item Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMIKC_ITM_CODE_MAIN" runat="server" Text='<%# Bind("MIKC_ITM_CODE_MAIN") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="90%" />
                                                    <HeaderStyle Width="90%" />
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <%-- pnl excel Upload --%>
    <asp:UpdatePanel ID="upExcelUpload" runat="server">
        <ContentTemplate>
            <asp:Button ID="btn10" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popupExcel" runat="server" Enabled="True" TargetControlID="btn10"
                PopupControlID="pnlExcelUpload" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlExcelUpload">
        <div class="panel panel-default height45 width700 ">
            <div class="panel panel-default">
                <div class="panel-heading height30">
                    <div class="col-sm-11">
                        Excel Upload
                    </div>
                    <div class="col-sm-1">
                        <asp:LinkButton ID="lbtnExcelUploadClose" runat="server" OnClick="lbtnExcelUploadClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                        </asp:LinkButton>
                    </div>
                    <%--<span>Commen Search</span>--%>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12 paddingLeft0 paddingRight0">
                        <div class="row height22">
                            <div class="col-sm-11">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lblExcelUploadError" runat="server" ForeColor="Red" Text="Label" Visible="false"></asp:Label>
                                        <asp:Label ID="lblExcelUploadSuccess" runat="server" ForeColor="Green" Visible="false"></asp:Label>
                                        <asp:Label ID="lblExcelUploadInfo" runat="server" ForeColor="Blue" Visible="false"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="row">
                            <asp:Panel runat="server">
                                <div class="col-sm-12">
                                    <div class="col-sm-10 paddingRight5">
                                        <asp:FileUpload ID="fileUploadExcel" runat="server" />
                                    </div>
                                    <div class="col-sm-2 paddingRight5">
                                        <asp:Button ID="lbtnUploadExcelFile" class="btn btn-warning btn-xs" runat="server" Text="Upload"
                                            OnClick="lbtnUploadExcelFile_Click" />
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
            <asp:Button ID="btn15" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popupErro" runat="server" Enabled="True" TargetControlID="btn15"
                PopupControlID="pnlExcelErro" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server" ID="UpdatePanel6">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlExcelErro">
                <div runat="server" id="Div1" class="panel panel-primary" style="padding: 5px;">
                    <div class="panel panel-default" style="width: 1100px;">
                        <div class="panel-heading" style="height: 25px;">
                            <div class="col-sm-11 padding0">
                                <strong>Excel Incorrect Data</strong>
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel panel-body padding0">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="row buttonRow">
                                                <div class="col-sm-12">
                                                    <div class="col-sm-7">
                                                    </div>

                                                    <div class="col-sm-5 padding0">
                                                        <div class="col-sm-4">
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <%-- <asp:LinkButton ID="lbtnExcSave" OnClientClick="return ConfSave()" runat="server" OnClick="lbtnExcSave_Click">
                                                      <span class="glyphicon glyphicon-saved"  aria-hidden="true"></span>Save
                                                            </asp:LinkButton>--%>
                                                        </div>
                                                        <div class="col-sm-4 text-right">
                                                            <asp:LinkButton ID="lbtnExcClose" runat="server" OnClick="lbtnExcClose_Click">
                                                                    <span class="glyphicon glyphicon-remove"  aria-hidden="true"></span>Close
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="panel panel-default">
                                                        <div class="panel panel-body padding0">
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div style="height: 400px; overflow-y: auto;">
                                                                        <asp:GridView ID="dgvError" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                            EmptyDataText="No data found..." AutoGenerateColumns="False"
                                                                            PagerStyle-CssClass="cssPager">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Excel Line">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblLine" Text='<%# Bind("Line","{0:#00}") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="60px" />
                                                                                    <HeaderStyle Width="60px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Err Data">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblTmp_err_text" Text='<%# Bind("Tmp_err_text") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="50px" />
                                                                                    <HeaderStyle Width="50px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Error">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblTmp_err" Text='<%# Bind("Tmp_err") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="100px" />
                                                                                    <HeaderStyle Width="100px" />
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
    <%-- pnl save order plan excel --%>
    <asp:UpdatePanel runat="server" ID="UpdatePanel7">
        <ContentTemplate>
            <asp:Button ID="btnpop12" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popOpExcSave" runat="server" Enabled="True" TargetControlID="btnpop12"
                PopupControlID="pnlOpExcSave" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress7" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="upOpExcSave">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWaidt10" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWaidt10" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:Panel runat="server" ID="pnlOpExcSave">
        <asp:UpdatePanel runat="server" ID="upOpExcSave">
            <ContentTemplate>
                <div runat="server" id="Div2" class="panel panel-primary" style="padding: 1px;">
                    <div class="panel panel-default" style="height: 40px; width: 500px;">
                        <div class="panel-heading" style="height: 40px;">
                            <div class="col-sm-8">
                                <strong>Generate order plans</strong>
                            </div>
                            <div class="col-sm-2 text-right padding0">
                                <asp:Button Text="Generate" ID="btnGenOrdPlans" OnClick="btnGenOrdPlans_Click" runat="server" />
                            </div>
                            <div class="col-sm-2 text-right padding0">
                                <asp:Button Text="Cancel" ID="btnCancelProcess" OnClick="btnCancelProcess_Click" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <%-- Pnl document popup --%>
    <asp:UpdatePanel runat="server" ID="UpdatePanel12">
        <ContentTemplate>
            <asp:Button ID="Button4" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popDocNoShow" runat="server" Enabled="True" TargetControlID="Button4"
                PopupControlID="pnlDocNoShow" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel runat="server" ID="pnlDocNoShow">
        <asp:UpdatePanel runat="server" ID="upDocNoShow">
            <ContentTemplate>
                <div runat="server" id="Div6" class="panel panel-primary" style="padding: 5px;">
                    <div class="panel panel-default" style="height: 280px; width: 225px;">
                        <div class="panel-heading" style="height: 25px;">
                            <div class="col-sm-10 padding0">
                                <strong>Document Numbers</strong>
                            </div>
                            <div class="col-sm-2 text-right paddingLeft0">
                                <asp:LinkButton ID="lbtnOldPartRemClose" runat="server">
                                    <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="panel-body" style="padding-bottom: 0px; padding-top: 0px;">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div style="height: 250px; overflow-y: auto;">
                                        <asp:GridView ID="dgvDocNo" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No data found..." AutoGenerateColumns="False"
                                            PagerStyle-CssClass="cssPager">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Order #">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIO_OP_NO" Text='<%# Bind("IO_OP_NO") %>' runat="server" Width="100%" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="50px" />
                                                    <HeaderStyle Width="50px" />
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
    </asp:Panel>
    <script type="text/javascript">
        Sys.Application.add_load(fun);
        function fun() {
            $('.validateDecimal').keypress(function (evt) {
                // var ch = (evt.which) ? evt.which : evt.keyCode;
                var ch = evt.which;
                var str = $(this).val();
                // console.log(ch);
                if (ch == 46) {
                    if (str.indexOf(".") == -1) {
                        return true;
                    } else {
                        return false;
                    }
                }
                else if ((ch == 8) || (ch == 9) || (ch == 46) || (ch == 0)) {
                    return true;
                }
                else if (ch > 47 && ch < 58) {
                    return true;
                }
                else {
                    return false;
                }
            });
            $('.diWMClick').mousedown(function (e) {
                if (e.button == 2) {
                    alert('This functionality is disabled !');
                    return false;
                } else {
                    return true;
                }
            });
            $('.validateInt').keypress(function (evt) {
                // var ch = (evt.which) ? evt.which : evt.keyCode;
                var charCode = evt.which;
                var str = $(this).val();
                //console.log(charCode);
                if ((charCode == 8) || (charCode == 9) || (charCode == 0) || (charCode == 13)) {
                    return true;
                }
                else if (str.length < 10) {
                    if (charCode > 47 && charCode < 58) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else {
                    $(this).value = str.substr(0, 10);
                    //alert(charCode);
                    alert('Maximum 4 characters are allowed ');
                    return false;
                }
            });
        }
    </script>
</asp:Content>
