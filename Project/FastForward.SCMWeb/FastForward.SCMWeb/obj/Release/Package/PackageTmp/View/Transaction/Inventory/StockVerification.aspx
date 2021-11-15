<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="StockVerification.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Inventory.StockVerification" %>

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
        function CheckAllgrdItem(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdsubjob.ClientID %>");
                    for (i = 1; i < GridView2.rows.length; i++) {
                        GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
                    }
                }
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
        function FineshConfirm() {

            var selectedvalue = confirm("Do you want to Finesh Scan?");
            if (selectedvalue) {
                document.getElementById('<%=txtFinesh.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtFinesh.ClientID %>').value = "No";
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

            var selectedvalue = confirm("Do you want to finesh data?");
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
        function ConfirmPDAForm() {
            var selectedvalueOrd = confirm("Do you want to sent to PDA ?");
            if (selectedvalueOrd) {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "No";
            }
        };
    </script>
        <style>
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
    <asp:HiddenField ID="txtFinesh" runat="server" />
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField ID="hdfTabIndex" runat="server" />
     <asp:HiddenField ID="txtpda" runat="server" />
        <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="upmain">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <div class="panel panel-default marginLeftRight5">
        <asp:UpdatePanel runat="server" ID ="upmain">
            <ContentTemplate>
                <div class="row">
                    <div class="col-sm-12 buttonrow">

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
                                    <asp:LinkButton ID="lbtnSave" runat="server" CssClass="floatRight" OnClientClick="SaveConfirm()" OnClick="lbtnSave_Click"> 
                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-2 paddingRight0">
                                    <asp:LinkButton ID="lbtnFinesh" runat="server" CssClass="floatRight" OnClientClick="ApproveConfirm()" OnClick="lbtnFinesh_Click">
                                        <span class="glyphicon glyphicon-thumbs-up" aria-hidden="true"></span>Finish Job
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-3 paddingRight0">
                                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="floatRight" OnClientClick="FineshConfirm()" OnClick="lbtnFineshScan_Click">
                                        <span class="glyphicon glyphicon-thumbs-up" aria-hidden="true"></span>Scan Finish
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-2">
                                    <asp:LinkButton ID="lblUClear" runat="server" CssClass="floatRight" OnClientClick="ConfirmClearForm()" OnClick="lbtnclear_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="row" id="divMainRow">
                    <div class="panel-body paddingbottom0">
                        <div class="col-sm-12">
                            <div class="panel panel-default marginLeftRight5">
                                <div class="panel-heading pannelheading  paddingtop0">
                                    Stock Verification
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-12">
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
                                                                        Main job #
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox runat="server" ID="txtdoc" AutoPostBack="true" OnTextChanged="txtdoc_TextChanged" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="lbtnjobCode" runat="server" CausesValidation="false" OnClick="lbtnjobCode_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
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
                                                                        From Date 
                                                                    </div>
                                                                    <div class="col-sm-6  ">
                                                                        <asp:TextBox runat="server" Enabled="false" ID="txtfrmdate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="lbtnfrmdate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                        <asp:CalendarExtender ID="CalendarExtender1" OnClientDateSelectionChanged="checkDate" runat="server" TargetControlID="txtfrmdate"
                                                                            PopupButtonID="lbtnfrmdate" Format="dd/MMM/yyyy">
                                                                        </asp:CalendarExtender>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-4 labelText1">
                                                                        To Date 
                                                                    </div>
                                                                    <div class="col-sm-6  ">
                                                                        <asp:TextBox runat="server" Enabled="false" ID="txttodate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0">
                                                                        <asp:LinkButton ID="lbtntodate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" OnClientDateSelectionChanged="checkDate" TargetControlID="txttodate"
                                                                            PopupButtonID="lbtntodate" Format="dd/MMM/yyyy">
                                                                        </asp:CalendarExtender>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <div class="row">
                                                                    <div class="col-sm-4 labelText1">
                                                                        No of sub job 
                                                                    </div>
                                                                    <div class="col-sm-3">
                                                                        <asp:TextBox runat="server" onkeypress="filterDigits(event)" Style="text-align: right" ID="txtnosubjob" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-2 labelText1 ">
                                                                        Status:
                                                                    </div>
                                                                    <div class="col-sm-2 labelText1 " style="color: red">
                                                                        <asp:Label runat="server" ID="lblstatus"></asp:Label>
                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-4 labelText1">
                                                                        Remark
                                                                    </div>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox runat="server" TextMode="MultiLine" ID="txtremark" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12 height5">
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-4 labelText1">
                                                                        Location
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox runat="server" ID="txtloc" AutoPostBack="true" Style="text-transform: uppercase" CausesValidation="false" CssClass="form-control" OnTextChanged="txtlocation_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false" OnClick="lbtnloc_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>

                                            </div>
                                            <div class="col-sm-6 paddingRight0 paddingLeft5">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading pannelheading height16  paddingtop0">
                                                        All Jobs
                                                    </div>
                                                    <div class="panel-body">
                                                        <div class="row">
                                                            <div class="col-sm-7">
                                                                <div class="panelscoll">
                                                                    <asp:GridView ID="grdsubjob" AutoGenerateColumns="False" TabIndex="39" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                                        <Columns>
                                                                            <%--<asp:BoundField DataField="itri_seq_no" HeaderText="Seq No" ItemStyle-Width="150" ReadOnly="true" />--%>
                                                                            <asp:TemplateField HeaderText="Remove" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lbtnDetalteline" CausesValidation="false" CommandName="Delete" runat="server" OnClientClick="DeleteConfirm()">
                                                                            <span class="glyphicon glyphicon-trash" aria-hidden="true" style="font-size:15px"></span> 
                                                                                    </asp:LinkButton>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="2px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="JOB #">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblausm_job" runat="server" Text='<%# Bind("ausm_job") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                               
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="" Visible="true">
                                                                                <HeaderTemplate>
                                                                                    <asp:CheckBox ID="alljobschk" runat="server" Width="5px" onclick="CheckAllgrdItem(this)"></asp:CheckBox>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chk_jobs" runat="server" AutoPostBack="true" Checked="false" Width="5px" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-5">
                                                                <div class="row">

                                                                    <div class="col-sm-4 labelText1">
                                                                        Loading Bay
                                                                    </div>
                                                                    <div class="col-sm-7">
                                                                        <asp:DropDownList ID="ddlloadingbay" runat="server" TabIndex="2" CssClass="form-control"></asp:DropDownList>
                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <asp:Button Text="Send to PDA" ID="Button1" runat="server" OnClick="btnSentScan_Click" OnClientClick="ConfirmPDAForm()" />
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

                                            <div class="panel panel-default">
                                                <div class="panel-heading pannelheading height16 paddingtop0 ">
                                                    scan
                                                </div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-sm-12">

                                                            <div class="col-sm-3">
                                                                <div class="row">
                                                                    <div class="col-sm-2 padding0 labelText1">
                                                                        Doc #
                                                                    </div>
                                                                    <div class="col-sm-10  paddingRight5">
                                                                        <asp:DropDownList ID="ddlSUBJOB" OnSelectedIndexChanged="ddlSUBJOB_SelectedIndexChanged" AutoPostBack="true" CausesValidation="false" runat="server" CssClass="form-control">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="col-sm-3">
                                                                <div class="row">
                                                                    <div class="col-sm-2 labelText1">
                                                                        Item
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox runat="server" ID="txtitem" OnTextChanged="txtItem_TextChanged" AutoPostBack="true" Style="text-transform: uppercase" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                                        <asp:LinkButton ID="btnSearch_Item" runat="server" CausesValidation="false" OnClick="btnSearch_Item_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <div class="row">
                                                                    <asp:Panel runat="server" DefaultButton="btnserial" ID="pnlserial">
                                                                        <div class="col-sm-3 labelText1">
                                                                            Serial #
                                                                        </div>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox runat="server" ID="txtserial" Style="text-transform: uppercase" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1">
                                                                            <asp:Button ID="btnserial" runat="server" OnClick="btnqty_Click" Text="Submit" Style="display: none;" />
                                                                        </div>
                                                                    </asp:Panel>
                                                                    <asp:Panel runat="server" DefaultButton="btnqty" ID="pnlnonserial" Visible="false">
                                                                        <div class="col-sm-3 labelText1">
                                                                            Qty 
                                                                        </div>

                                                                        <div class="col-sm-1">
                                                                            <asp:Button ID="btnqty" runat="server" OnClick="txtqty_TextChanged" Text="Submit" Style="display: none;" />
                                                                        </div>
                                                                        <div class="col-sm-3 paddingRight5 paddingLeft0">
                                                                            <asp:TextBox runat="server" ID="txtqty" oncopy="return false"
                                                                                onpaste="return false"
                                                                                oncut="return false" onkeypress="return isNumberKey(event)" CssClass="form-control"></asp:TextBox>
                                                                        </div>


                                                                    </asp:Panel>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <div class="row">

                                                                    <div class="col-sm-3 labelText1">
                                                                        Scan.Qty:
                                                                    </div>
                                                                    <div class="col-sm-8 labelText1">
                                                                        <asp:Label ID="lblscanQty" runat="server" CssClass="Color1 fontWeight900"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-1 padding0">
                                                                <asp:LinkButton ID="lbtnviewserial" runat="server" TabIndex="103" CausesValidation="false" OnClick="lbtnviewserial_Click">
                                                                     View Serial
                                                                </asp:LinkButton>
                                                            </div>


                                                        </div>

                                                    </div>
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

                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">

                                            <div class="panel panel-default">
                                                <div class="panel-body panelscollbar height200">
                                                    <asp:GridView ID="grdpickserial" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Remove">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnser_Remove" runat="server" CausesValidation="false" OnClientClick="DeleteConfirm()" Width="5px" OnClick="lbtnser_Remove_Click">
                                                                        <span class="glyphicon glyphicon-trash" aria-hidden="true" ></span>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Serial ID" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltus_ser_id" runat="server" Text='<%# Bind("tus_ser_id") %>' Width="20px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Scan Item Code">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltus_itm_cd" runat="server" Text='<%# Bind("tus_itm_cd") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Item Status" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltus_itm_stus" runat="server" Visible="false" Text='<%# Bind("tus_itm_stus") %>'></asp:Label>
                                                                    <asp:Label ID="lblTus_itm_stus_Desc" runat="server" Text='<%# Bind("Tus_itm_stus_Desc") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Serial #/Engine #">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltus_ser_1" runat="server" Text='<%# Bind("tus_ser_1") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Qty">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltus_qty" runat="server" Text='<%# Bind("tus_qty" ,"{0:N2}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="LINE" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltus_itm_line" runat="server" Text='<%# Bind("tus_itm_line") %>'></asp:Label>
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
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
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
</asp:Content>

