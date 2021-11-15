<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="ScanDocumentTracker.aspx.cs" Inherits="FastForward.SCMWeb.View.Enquiries.WareHouse.ScanDocumentTracker" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
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
                    console.log("toast is closed . ..");
                }
            });
        }
        function ConfirmClearForm() {
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
            var selectedvalueclear = confirm("Do you want to clear all details ?");
            if (selectedvalueclear) {
                document.getElementById('<%=txtconfirmclear.ClientID %>').value = "Yes";
        } else {
            document.getElementById('<%=txtconfirmclear.ClientID %>').value = "No";
        }
    };
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
    </script>
    <style type="text/css">
        body {
            overflow-x: hidden;
        }

        .panel {
            padding-bottom: 1px;
            padding-top: 1px;
            margin-bottom: 0px;
            margin-top: 0px;
            margin-left: 5px;
            /*margin-bottom: 25px;*/
        }

        #SUBCATHIDE {
            display: none;
        }
        /*.panel-default{
            padding-bottom:0px;
            padding-top:1px;
            margin-bottom:1px;
            margin-top:0px;
        }
        .panel-heading{
            padding-bottom:0px;
            padding-top:1px;
            margin-bottom:1px;
            margin-top:0px;
        }
        .panel-body{
            padding-bottom:0px;
            padding-top:1px;
            margin-bottom:1px;
            margin-top:0px;
        }*/
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
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="panel panel-default marginLeftRight5 paddingbottom0">
        <div class="panel-heading paddingtopbottom0">
            <strong>Scan Document Tracker</strong>
        </div>

        <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="txtconfirmclear" runat="server" />
                <div class="panel panel-default">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="col-sm-3 ">
                                <div class="col-sm-4 labelText1 padding0">
                                    Location
                                </div>
                                <div class="col-sm-6 paddingRight0">
                                    <asp:TextBox ID="txtLocation" AutoPostBack="true" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                                <div class="col-sm-1 paddingLeft3">
                                    <asp:LinkButton ID="lbtnLocation" CausesValidation="false" runat="server" OnClick="LinkButtonLoc_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="col-sm-3 ">
                                <div class="col-sm-4 labelText1">
                                    Direction
                                </div>
                                <div class="col-sm-6 paddingRight0">
                                    <asp:DropDownList ID="dropDownListDirection" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-3 ">
                                <div class="col-sm-4 labelText1 padding0">
                                    Doc Type
                                </div>
                                <div class="col-sm-6 ">
                                    <asp:DropDownList ID="dropDownDocType" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-3 ">
                                <div class="col-sm-4 labelText1 padding0" hidden="hidden">
                                    Status
                                </div>
                                <div class="col-sm-6 ">
                                    <asp:DropDownList ID="DropDownListStatus" runat="server" AutoPostBack="true" CssClass="form-control" Visible="false"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 7px">
                        <div class="col-sm-12">
                            <div class="col-sm-3">

                                <div class="col-sm-4 labelText1 padding0">
                                    Date Type
                                </div>
                                <div class="col-sm-6 paddingRight0 ">
                                    <asp:RadioButtonList ID="dbtndate" Enabled="true" AutoPostBack="true" runat="server" RepeatDirection="Vertical">
                                        <asp:ListItem Text="Created Date" Value="0" />
                                        <asp:ListItem Text="Finished Date" Value="1" />
                                    </asp:RadioButtonList>
                                </div>

                            </div>
                            <div class="col-sm-3">
                                <div class="col-sm-6 padding0">
                                    <div class="col-sm-12 paddingRight0">
                                        <div class="col-sm-2 labelText1 padding0">
                                            From
                                        </div>
                                        <div class="col-sm-7 paddingLeft3 paddingRight0">
                                            <asp:TextBox ID="dtpFromDate" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                            <asp:CalendarExtender ID="calexdate" runat="server" TargetControlID="dtpFromDate"
                                                PopupButtonID="lbtnfrm" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>

                                        <div class="col-sm-1 paddingLeft3">
                                            <asp:LinkButton ID="lbtnfrm" TabIndex="1" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6 padding0">
                                    <div class="col-sm-12 paddingRight0">
                                        <div class="col-sm-2 labelText1 padding0">
                                            To
                                        </div>
                                        <div class="col-sm-7 paddingLeft3 paddingRight0">
                                            <asp:TextBox ID="dtpToDate" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                            <asp:CalendarExtender ID="calexreq" runat="server" TargetControlID="dtpToDate"
                                                PopupButtonID="lbtnto" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>

                                        <div class="col-sm-1 paddingLeft3">
                                            <asp:LinkButton ID="lbtnto" TabIndex="2" CausesValidation="false" runat="server">
                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="col-sm-4 labelText1 padding0">
                                    <asp:CheckBox ID="PendingCheckBox" runat="server" AutoPostBack="true" OnCheckedChanged="PendingChckedChanged"/>
                                    <asp:Label Text="Pending" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-4 labelText1 padding0">
                                    <asp:CheckBox ID="CheckBoxSendtoScan" runat="server" AutoPostBack="true" OnCheckedChanged="SendScanChckedChanged"/>
                                    <asp:Label Text="Send to Scan" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-4 labelText1 padding0">
                                    <asp:CheckBox ID="CheckBoxScanning" runat="server" AutoPostBack="true" OnCheckedChanged="ScanningChckedChanged" />
                                    <asp:Label Text="Scanning" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-4 labelText1 padding0">
                                    <asp:CheckBox ID="CheckBoxScanComplete" runat="server" AutoPostBack="true" OnCheckedChanged="ScanCompleteChckedChanged" />
                                    <asp:Label Text="Scan Complete" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-4 labelText1 padding0">
                                    <asp:CheckBox ID="chkInvoice" runat="server" AutoPostBack="true"  OnCheckedChanged="InvoiceChckedChanged" />
                                    <asp:Label Text="Only Invoiced" runat="server"></asp:Label>
                                </div>
                                
                                <div class="col-sm-4 labelText1 padding0">
                                    <asp:CheckBox ID="CheckBoxAll" runat="server" AutoPostBack="true"  OnCheckedChanged="AllChckedChanged" />
                                    <asp:Label Text="All" runat="server"></asp:Label>
                                </div>
                                <%--<div class="col-md-1">
                                <asp:CheckBox ID="chkWorkingOrder" runat="server" AutoPostBack="true" />
                            </div>                         
                            <div class="col-md-3">
                                <asp:Label Text="Only WIP order(s)" runat="server"></asp:Label>
                            </div>--%>
                            </div>
                            <div class="col-sm-2">
                                <div class="buttonRow">
                                    <asp:LinkButton ID="btnGetPO" runat="server" OnClick="lbtnSearch_Main_Click" AutoPostBack="true">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true" style="font-size:20px"></span>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lbtnClear" runat="server" CssClass="floatRight" OnClientClick="ConfirmClearForm();" OnClick="lbtnClear_Click">
                            <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel panel-default marginLeftRight5 paddingbottom0">
                    <div class="panel-heading paddingtopbottom0">
                        <strong>Pending Docs </strong>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-default">
                                <div class="panel panel-body">
                                    <div style="height: 220px; overflow-y: auto; overflow-x: hidden;">
                                        <asp:GridView ID="documentStatusGrid" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found..." OnSelectedIndexChanged="documentStatusGrid_SelectedIndexChanged"
                                            OnRowDataBound="documentStatusGrid_RowDataBound">
                                            <Columns>
                                                <asp:CommandField ShowSelectButton="true" ButtonType="Link" />
                                                <asp:TemplateField HeaderText="User Sq#" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUserSeq" runat="server" Text='<%# Bind("tuh_usrseq_no") %>' Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Doc_No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldocno" runat="server" Text='<%# Bind("Doc_No") %>' Width="160px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Doc_Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldoctype" runat="server" Text='<%# Bind("Doc_Type") %>' Width="40px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SO_No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblso" runat="server" Text='<%# Bind("SO_No") %>' Width="160px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Direction">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldir" runat="server" Text='<%# Bind("Direction") %>' Width="45px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Create_Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcre" runat="server" Text='<%# Bind("Create_Date", "{0:dd/MMM/yyyy H:mm}") %>' Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rec_Location">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblreloc" runat="server" Text='<%# Bind("Rec_Location") %>' Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Loading_Bay">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblldbay" runat="server" Text='<%# Bind("Loading_Bay") %>' Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Finished_Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblfin" runat="server" Text='<%# Bind("Finished_Date", "{0:dd/MMM/yyyy H:mm}") %>' Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblisfin" runat="server" Text='<%# Bind("Status") %>' Width="90px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Scan Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsqty" runat="server" Text='<%# Bind("Scan_Qty") %>' Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Invoice_No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblinvoicetype" runat="server" Text='<%# Bind("Invoice_No") %>' Width="160px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Confirm" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkconfirm" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                            <%--<SelectedRowStyle BackColor="Silver" />--%>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <strong>
                                    <asp:Label ID="lblDocCount" runat="server" CssClass="floatRight marginLeftRight10" Width="100px"></asp:Label></strong>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel panel-default marginLeftRight5 paddingbottom0">
                    <div class="panel-heading paddingtopbottom0">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-sm-3">
                                    <strong>Items </strong>
                                </div>
                                <div class="col-sm-9">
                                    <strong>
                                        <asp:Label ID="lblDoc" runat="server" Width="100%"></asp:Label></strong>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-default">
                                <div class="panel panel-body">
                                    <div style="height: 200px; overflow-y: auto; overflow-x: hidden;">
                                        <asp:GridView ID="itemStatusGrid" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                            <Columns>
                                                <asp:CommandField ShowSelectButton="false" ButtonType="Link" />
                                                <%--<asp:TemplateField HeaderText="User Sq#" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUserSeq" runat="server" Text='<%# Bind("tuh_usrseq_no") %>' Width="150px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Location">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblitmloc" runat="server" Text='<%# Bind("loc") %>' Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BIN">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbin" runat="server" Text='<%# Bind("bin") %>' Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="liditmcode" runat="server" Text='<%# Bind("item_code") %>' Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Model">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmodel" runat="server" Text='<%# Bind("model") %>' Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblstatus" runat="server" Text='<%# Bind("item_status") %>' Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Serial 1">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblser1" runat="server" Text='<%# Bind("serial_1") %>' Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Serial 2">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblser2" runat="server" Text='<%# Bind("serial_2") %>' Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Scan Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblitemsqty" runat="server" Text='<%# Bind("scan_qty") %>' Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Scan Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblscndte" runat="server" Text='<%# Bind("scan_datetime", "{0:MM/dd/yy H:mm:ss}") %>' Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Scan By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblscnby" runat="server" Text='<%# Bind("scan_by") %>' Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Confirm" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkconfirm" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                            <SelectedRowStyle BackColor="Silver" />
                                        </asp:GridView>

                                    </div>
                                </div>
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

                        <%-- Style="display: none"--%>
                        <asp:Panel runat="server" ID="testPanel" DefaultButton="ImageSearch" Style="display: none;">
                            <div runat="server" id="test" class="panel panel-primary Mheight">

                                <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                                <%--<asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>--%>
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
                                                            <asp:LinkButton ID="ImageSearch" runat="server" OnClick="btnSearch_Click">
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
                                                        <asp:GridView ID="dvResultUser" CausesValidation="false" runat="server" OnSelectedIndexChanged="dvResultUser_SelectedIndexChanged" AllowPaging="True" OnPageIndexChanging="dvResultUser_PageIndexChanging" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager">
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
                                <%-- </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="txtSearchbyword" />
                </Triggers>

            </asp:UpdatePanel>--%>
                            </div>
                        </asp:Panel>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
