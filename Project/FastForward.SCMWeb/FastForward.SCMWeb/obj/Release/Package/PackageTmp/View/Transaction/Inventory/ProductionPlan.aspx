<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="ProductionPlan.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Inventory.ProductionPlan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">

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

    </script>

    <script>

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


        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
              && (charCode < 48 || charCode > 57))
                return false;

            return true;
        };


        function checkDate(sender, args) {

            if ((sender._selectedDate < new Date())) {
                alert("You cannot select a day earlier than today!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
        function SaveConfirm() {

            var selectedvalue = confirm("Do you want to save data?");
            if (selectedvalue) {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "No";
            }
        };
        function DeleteConfirm() {

            var selectedvalue = confirm("Do you want to delete item?");
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
            var selectedvalueOrd = confirm("Do you want to clear all details ?");
            if (selectedvalueOrd) {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "No";
            }
        };
    </script>
    <style>
        .dropdownpalan {
            left: -126px !important;
            top: 25px !important;
        }

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
    <asp:HiddenField ID="txtACancelconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtClearlconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtSavelconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtUpdateconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtApprovalconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtCancelconformmessageValue" runat="server" />
    <asp:HiddenField ID="txtDeleteconformmessageValue" runat="server" />
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField ID="hdfTabIndex" runat="server" />

        <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel3">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait1" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait1" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait2" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait2" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    
    <div class="panel panel-default marginLeftRight5">
        <div class="row">
            <div class="col-sm-12 buttonrow">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="col-sm-12 buttonRow paddingRight5" id="divTopCheck" runat="server">
                            <div class="col-sm-7 buttonRow padding0">
                            </div>
                            <div class="col-sm-5 buttonRow padding0">

                                <div class="col-sm-2">
                                </div>
                                <div class="col-sm-2 paddingRight0">
                                    <asp:LinkButton ID="LinkButton5" runat="server" CssClass="floatRight" Visible="false">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Complete
                                    </asp:LinkButton>
                                </div>

                                <div class="col-sm-2 paddingRight0">
                                    <asp:LinkButton ID="lbtnSave" runat="server" CssClass="floatRight" OnClientClick="SaveConfirm()" OnClick="lbtnSave_Click"> 
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-2 paddingRight0">
                                    <asp:LinkButton ID="lbtnapprove" runat="server" CssClass="floatRight" OnClientClick="ApproveConfirm()" OnClick="lbtnapprove_Click">
                                        <span class="glyphicon glyphicon-thumbs-up" aria-hidden="true"></span>Approve
                                    </asp:LinkButton>
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
        <div class="row" id="divMainRow">
            <div class="panel-body paddingbottom0">
                <div class="col-sm-12">
                    <div class="panel panel-default marginLeftRight5">
                        <div class="panel-heading pannelheading  paddingtop0">
                            Production Plan
                        </div>
                        <div class="panel-body">

                            <div class="bs-example">
                                <ul class="nav nav-tabs" id="myTab">
                                    <li class="active"><a href="#Project" data-toggle="tab">Production </a></li>
                                    <li><a href="#Cost" data-toggle="tab">Production Details</a></li>


                                    <div class="col-sm-5 width450">
                                    </div>

                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <div class="col-sm-1 labelText1 ">
                                                Production Status:
                                            </div>
                                            <div class="col-sm-2 labelText1 " style="color: red">
                                                <asp:Label runat="server" ID="lblstatus"></asp:Label>
                                            </div>
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
                                                                                    <asp:TextBox ID="txtCustomer" Style="text-transform: uppercase" runat="server" TabIndex="7"
                                                                                        CssClass="form-control" AutoPostBack="true" OnTextChanged="txtCustomer_TextChanged"></asp:TextBox>
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
                                                                                <div class="col-sm-9 paddingRight5">
                                                                                    <asp:TextBox ID="txtNIC" Style="text-transform: uppercase" runat="server"
                                                                                        TabIndex="8" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                                                </div>

                                                                            </div>
                                                                        </div>

                                                                        <div class="col-sm-3">
                                                                            <div class="row">

                                                                                <div class="col-sm-3 labelText1">
                                                                                    Mobile
                                                                                </div>
                                                                                <div class="col-sm-9 paddingRight5">
                                                                                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" MaxLength="10"
                                                                                        TabIndex="9" AutoPostBack="true"></asp:TextBox>
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

                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6 paddingRight0 paddingLeft5">
                                                        <div class="panel panel-default">
                                                            <div class="panel-heading pannelheading height16 paddingtop0 ">
                                                                Project
                                                            </div>
                                                            <div class="panel-body">
                                                                <div class="row">

                                                                    <div class="col-sm-6">
                                                                        <div class="row">
                                                                            <div class="col-sm-4 labelText1">
                                                                                Production plan #
                                                                            </div>
                                                                            <div class="col-sm-6">
                                                                                <asp:TextBox runat="server" ID="txtdoc" OnTextChanged="txtdoc_TextChanged" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                                <asp:LinkButton ID="lbtnProCode" runat="server" CausesValidation="false" OnClick="lbtnProCode_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                        </div>

                                                                        <div class="row">
                                                                            <div class="col-sm-4 labelText1">
                                                                                Ref #
                                                                            </div>
                                                                            <div class="col-sm-6">
                                                                                <asp:TextBox runat="server" ID="txtref" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                            </div>

                                                                        </div>

                                                                        <div class="row">
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

                                                                        <div class="row">
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
                                                                                <asp:CalendarExtender ID="CalendarExtender1" OnClientDateSelectionChanged="checkDate" runat="server" TargetControlID="txtcompletedate"
                                                                                    PopupButtonID="lbtncompletedate" Format="dd/MMM/yyyy">
                                                                                </asp:CalendarExtender>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <div class="row">
                                                                            <div class="col-sm-2 labelText1">
                                                                                Dis.Loc
                                                                            </div>
                                                                            <div class="col-sm-6">
                                                                                <asp:TextBox runat="server" ID="txtlocation" AutoPostBack="true" Style="text-transform: uppercase" OnTextChanged="txtlocation_TextChanged" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                                <asp:LinkButton ID="lbtnloc" runat="server" CausesValidation="false" OnClick="lbtnloc_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">

                                                                            <div class="col-sm-2 padding0 labelText1">
                                                                                Price Book
                                                                            </div>
                                                                            <div class="col-sm-6  paddingRight5">
                                                                                <asp:DropDownList ID="ddlPriceBook" AutoPostBack="true" CausesValidation="false" runat="server" CssClass="form-control">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">

                                                                            <div class="col-sm-2 padding0 labelText1">
                                                                                Price Level
                                                                            </div>
                                                                            <div class="col-sm-6 paddingRight5">
                                                                                <asp:DropDownList ID="ddlLevel" CausesValidation="false" runat="server" CssClass="form-control">
                                                                                </asp:DropDownList>
                                                                            </div>

                                                                        </div>
                                                                        <div class="row">

                                                                            <div class="col-sm-5 paddingRight0">
                                                                                <div class="col-sm-1  paddingLeft0 paddingRight0  ">
                                                                                    <asp:RadioButton ID="chkCus" GroupName="Cus" Checked="true" runat="server"></asp:RadioButton>
                                                                                </div>
                                                                                <div class="col-sm-10  paddingLeft5 paddingRight0">
                                                                                    Customer Order
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-5 paddingLeft0">
                                                                                <div class="col-sm-1  paddingLeft0   ">
                                                                                    <asp:RadioButton ID="chkstock" GroupName="Cus" runat="server"></asp:RadioButton>
                                                                                </div>
                                                                                <div class="col-sm-10  paddingLeft5 paddingRight0">
                                                                                    Safty stock
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
                                            <div class="row">
                                                <div class="col-sm-6 paddingRight0">

                                                    <div class="panel panel-default">
                                                        <div class="panel-body">
                                                            <asp:Panel runat="server" ID="pnlchart">
                                                            <div class="col-sm-6">
                                                                   <asp:Chart ID="COSTTestChart" runat="server">
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
                                                                </asp:Panel>
                                                        </div>
                                                    </div>
                                                </div>
                                                <asp:Panel runat="server" Visible="true">
                                                    <div class="col-sm-6 paddingLeft5">
                                                        <div class="panel panel-default">
                                                            <div class="panel-heading pannelheading height16 paddingtop0 ">
                                                                Project Line
                                                            </div>
                                                            <div class="panel-body">
                                                                <div class="row">
                                                                    <div class="col-sm-4">
                                                                        <div class="col-sm-4 padding0 labelText1">
                                                                            Line #
                                                                        </div>
                                                                        <div class="col-sm-8  paddingRight5">
                                                                            <asp:DropDownList ID="ddlLine" AutoPostBack="true" CausesValidation="false" runat="server" CssClass="form-control">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <div class="col-sm-4 padding0 labelText1">
                                                                            Target Qty
                                                                        </div>
                                                                        <div class="col-sm-6  paddingRight5">
                                                                            <asp:TextBox runat="server" ID="txtlineTQty" onkeypress="filterDigits(event)" Style="text-align: right" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-6 paddingLeft0">
                                                                        <div class="col-sm-4 labelText1">
                                                                            Commence Date
                                                                        </div>
                                                                        <div class="col-sm-6  ">
                                                                            <asp:TextBox runat="server" Enabled="false" ID="txtlcomm" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0">
                                                                            <asp:LinkButton ID="lbtncomdate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtlcomm"
                                                                                PopupButtonID="lbtncomdate" Format="dd/MMM/yyyy">
                                                                            </asp:CalendarExtender>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6 paddingLeft0">
                                                                        <div class="col-sm-3 labelText1">
                                                                            End Date
                                                                        </div>
                                                                        <div class="col-sm-6">
                                                                            <asp:TextBox runat="server" Enabled="false" ID="txtend" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0">
                                                                            <asp:LinkButton ID="lbtnEnddate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtend"
                                                                                PopupButtonID="lbtnEnddate" Format="dd/MMM/yyyy">
                                                                            </asp:CalendarExtender>
                                                                        </div>
                                                                        <div class="col-sm-2 padding0">
                                                                            <asp:LinkButton ID="lbtnaddproductionline" runat="server" TabIndex="103" CausesValidation="false" OnClick="lbtnaddPline_Click">
                                                                     <span class="glyphicon glyphicon-plus" style="font-size:20px;"  aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12 ">
                                                                        <div class="row">
                                                                            <div class="col-sm-12 height5">
                                                                            </div>
                                                                        </div>
                                                                        <div class="panelscoll">
                                                                            <asp:GridView ID="grdProLine" AutoGenerateColumns="False" TabIndex="39" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                                                <Columns>
                                                                                    <%--<asp:BoundField DataField="itri_seq_no" HeaderText="Seq No" ItemStyle-Width="150" ReadOnly="true" />--%>
                                                                                    <asp:TemplateField HeaderText="Remove">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="lbtnDetalteline" CausesValidation="false" CommandName="Delete" runat="server" OnClientClick="DeleteConfirm()" OnClick="lbtnPlDelete_Click">
                                                                            <span class="glyphicon glyphicon-trash" aria-hidden="true" style="font-size:15px"></span> 
                                                                                            </asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="2px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Line #">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblSPL_PRO_LIN_DES" runat="server" Text='<%# Bind("SPL_PRO_LIN_DES") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="10px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Line #" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbSPL_PRO_LINE" runat="server" Text='<%# Bind("SPL_PRO_LINE") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="10px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Target Qty">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblSPL_QTY" runat="server" Text='<%# Bind("SPL_QTY") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="50px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Commence Date">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblSPL_ST_DT" runat="server" Text='<%# Bind("SPL_ST_DT", "{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="10px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="End Date">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblSPL_EN_DT" runat="server" Text='<%# Bind("SPL_EN_DT", "{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="10px" />
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
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
                                                            <asp:TextBox ID="txtkitqty" onkeypress="filterDigits(event)" Style="text-align: right" TabIndex="101" runat="server"
                                                                CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="col-sm-2 padding0">
                                                    <asp:LinkButton ID="lbtnKitadd" runat="server" TabIndex="103" CausesValidation="false" OnClick="lbtnKitadd_Click">
                                                                     <span class="glyphicon glyphicon-plus" style="font-size:20px;"  aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 height10">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-8">
                                                    <div class="panel panel-default " id="y5">
                                                        <div class="panel-heading pannelheading height16 paddingtop0">
                                                            <div class="row">
                                                                <div class="col-sm-8 paddingRight0">
                                                                    Inputs-Raw Materials
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-sm-3">
                                                                    <div class="row">
                                                                        <div class="col-sm-3 labelText1">
                                                                            Item
                                                                        </div>
                                                                        <div class="col-sm-8 paddingRight0">
                                                                            <asp:TextBox ID="txtCItem" Style="text-transform: uppercase" runat="server" TabIndex="100" OnTextChanged="txtItem_TextChanged"
                                                                                CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0">
                                                                            <asp:LinkButton ID="btnSearch_Item" CausesValidation="false" runat="server" OnClick="btnSearch_Item_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-2">
                                                                    <div class="row">
                                                                        <div class="col-sm-3 labelText1">
                                                                            Qty
                                                                        </div>
                                                                        <div class="col-sm-8 paddingRight0">
                                                                            <asp:TextBox ID="txtCQty" onkeypress="filterDigits(event)"  OnTextChanged="txtQty_TextChanged" Style="text-align: right" TabIndex="101" runat="server"
                                                                                CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3">
                                                                    <div class="row">
                                                                        <div class="col-sm-4 labelText1">
                                                                            Unit Cost
                                                                        </div>
                                                                        <div class="col-sm-8 paddingRight0">
                                                                            <asp:TextBox ID="txtunitcost" onkeypress="filterDigits(event)" Style="text-align: right" TabIndex="102" runat="server" OnTextChanged="txtunitcost_TextChanged"
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
                                                                            <asp:TextBox ID="txtCtotal" Enabled="false" onkeydown="return jsDecimals(event);" Style="text-align: right" runat="server"
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
                                                            <div class="row">
                                                                <div class="col-sm-12">

                                                                    <div class="row">
                                                                        <div class="col-sm-12">


                                                                            <div class="col-sm-5 paddingLeft0">
                                                                                <div class="row">
                                                                                    <div class="col-sm-3 labelText1" style="font-weight: bolder">
                                                                                        Description:
                                                                                    </div>
                                                                                    <div class="col-sm-8 paddingRight0" style="margin-top: 3px">
                                                                                        <asp:Label ID="lblItemDescription" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-3">
                                                                                <div class="row">
                                                                                    <div class="col-sm-3 labelText1" style="font-weight: bolder">
                                                                                        Model:
                                                                                    </div>
                                                                                    <div class="col-sm-9" style="margin-top: 3px">
                                                                                        <asp:Label ID="lblItemModel" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-3">
                                                                                <div class="row">
                                                                                    <div class="col-sm-3 labelText1" style="font-weight: bolder">
                                                                                        Brand:
                                                                                    </div>
                                                                                    <div class="col-sm-9" style="margin-top: 3px">
                                                                                        <asp:Label ID="lblItemBrand" runat="server" ForeColor="#A513D0"></asp:Label>
                                                                                    </div>
                                                                                </div>
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
                                                                    <div class="panelscoll2">
                                                                        <asp:GridView ID="grdCost" AutoGenerateColumns="False" TabIndex="39" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                                            <Columns>
                                                                                <%--<asp:BoundField DataField="itri_seq_no" HeaderText="Seq No" ItemStyle-Width="150" ReadOnly="true" />--%>
                                                                                <asp:TemplateField HeaderText="Item">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lbtnDetaltecost" CausesValidation="false" CommandName="Delete" runat="server" OnClientClick="DeleteConfirm()" OnClick="lbtnkitDelete_Click">
                                                                            <span class="glyphicon glyphicon-trash" aria-hidden="true" style="font-size:15px"></span> 
                                                                                        </asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="2px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Item">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblmikc_itm_code_main" runat="server" Text='<%# Bind("mikc_itm_code_component") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="10px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Description">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblmikc_desc_component" runat="server" Text='<%# Bind("mikc_desc_component") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="100px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Unit Cost" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblmikc_cost" runat="server" Text='<%# Bind("mikc_cost","{0:n2}") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="50px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Qty">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblmikc_no_of_unit" runat="server" Text='<%# Bind("mikc_no_of_unit") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="50px" />
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
                                                                            <asp:Label ID="lblTotalCost" runat="server"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4 paddingLeft5">
                                                    <div class="panel panel-default ">
                                                        <div class="panel-heading pannelheading height16 paddingtop0">
                                                            <div class="row">
                                                                <div class="col-sm-8 paddingRight0">
                                                                    Output-Finish Good
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-sm-7">
                                                                    <div class="row">
                                                                        <div class="col-sm-2 labelText1">
                                                                            Item
                                                                        </div>
                                                                        <div class="col-sm-8 paddingRight0">
                                                                            <asp:TextBox ID="txtFinItem" Style="text-transform: uppercase" runat="server" TabIndex="100"
                                                                                CssClass="form-control" AutoPostBack="true" OnTextChanged="txtFinItem_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft0">
                                                                            <asp:LinkButton ID="lblFItem" CausesValidation="false" runat="server" OnClick="lblFItem_Click">
                                                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3">
                                                                    <div class="row">
                                                                        <div class="col-sm-3 labelText1">
                                                                            Qty
                                                                        </div>
                                                                        <div class="col-sm-8 paddingRight0">
                                                                            <asp:TextBox ID="txtfqty" onkeypress="filterDigits(event)" Style="text-align: right" TabIndex="101" runat="server"
                                                                                CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-2 padding0">
                                                                    <asp:LinkButton ID="lbtnFAdd" runat="server" TabIndex="103" CausesValidation="false" OnClick="lbtnaddfgood_Click">
                                                                     <span class="glyphicon glyphicon-plus" style="font-size:20px;"  aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="row ">
                                                                <div class="col-sm-12 ">
                                                                    <div class="row">
                                                                        <div class="col-sm-12 height20">
                                                                        </div>
                                                                    </div>
                                                                    <div class="panelscoll2">
                                                                        <asp:GridView ID="grdFGood" AutoGenerateColumns="False" TabIndex="39" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                                            <Columns>
                                                                                <%--<asp:BoundField DataField="itri_seq_no" HeaderText="Seq No" ItemStyle-Width="150" ReadOnly="true" />--%>
                                                                                <asp:TemplateField HeaderText="Remove">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lbtnDetaltecost" CausesValidation="false" CommandName="Delete" runat="server" OnClientClick="DeleteConfirm()" OnClick="lbtnGFDelete_Click">
                                                                            <span class="glyphicon glyphicon-trash" aria-hidden="true" style="font-size:15px"></span> 
                                                                                        </asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="2px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Item">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblspd_itm" runat="server" Text='<%# Bind("SPF_ITM") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="50px" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Qty">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblspd_est_qty" runat="server" Text='<%# Bind("SPF_QTY") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="50px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Revenue" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSPF_ANAL_1" runat="server" Text='<%# Bind("SPF_ANAL_1") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="50px" />
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
                                                                <div class="col-sm-12 height10">
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-8">
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="row">
                                                                        <div class="col-sm-3 labelText1">
                                                                            Total:
                                                                        </div>
                                                                        <div class="col-sm-9 paddingRight0" style="margin-top: 3px">
                                                                            <asp:Label ID="lblTotalRevenue" Visible="true" runat="server"></asp:Label>
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
            <asp:Panel runat="server" ID="pnlDpopup">
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
                                            <asp:TextBox runat="server" TabIndex="200" Enabled="false" ID="txtFDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
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
                                            <asp:TextBox runat="server" Enabled="false" TabIndex="201" ID="txtTDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
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

                                <asp:UpdateProgress ID="UpdateProgress6" DisplayAfter="10"
                                    runat="server" AssociatedUpdatePanelID="UpdatePanel24">

                                    <ProgressTemplate>
                                        <div class="divWaiting">
                                            <asp:Label ID="lblWait6" runat="server"
                                                Text="Please wait... " />
                                            <asp:Image ID="imgWait6" runat="server"
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
