<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="AODTracker.aspx.cs" Inherits="FastForward.SCMWeb.View.Enquiries.Inventory.AODTracker" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.40412.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/Sales.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/jquery.toastmessage.js"></script>
    <link href="<%=Request.ApplicationPath%>Css/jquery-ui.theme.min.css" rel="stylesheet" />
    <link href="<%=Request.ApplicationPath%>Css/jquery-ui.min.css" rel="stylesheet" />


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
                sticky: false,
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
                sticky: false,
                position: 'top-center',
                type: 'error',
                closeText: '',
                close: function () {
                    console.log("toast is closed ...");
                }
            });
        }

        function confClear() {
            var res = confirm("Do you want to clear?");
            if (res) {
                return true;
            }
            else {
                return false;
            }
        };

    </script>
    <style type="text/css">
        .checkboxstyle {
            padding-left: 2px;
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
        runat="server" AssociatedUpdatePanelID="pnlBasePanel">

        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait1" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait1" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <div class="panel panel-default marginLeftRight5">
        <div class="panel-body">
            <asp:UpdatePanel ID="pnlBasePanel" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-sm-3">
                            <h1 style="font-size: 11px; margin-top: 2px; font-weight: bolder">AOD Tracker</h1>
                        </div>
                        <div class="col-sm-8" style="margin-top: 2px">
                            <asp:LinkButton ID="btnClear" runat="server" CssClass="floatRight" OnClientClick="return confClear()" OnClick="btnClear_Click">
                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="panel panel-default marginLeftRight5">
                                <div class="panel-body">
                                    <div class="col-sm-6">
                                        <div class="panel panel-default">
                                            <div class="panel-heading pannelheading ">
                                                Search By Company Details
                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-sm-2 labelText1">
                                                        Out Loc
                                                    </div>
                                                    <div class="col-sm-3 padding0">
                                                        <asp:TextBox runat="server" ID="txtfromloc" OnTextChanged="txtfromloc_TextChanged" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft3">
                                                        <asp:LinkButton ID="lbtnfromloc" runat="server" CausesValidation="false" OnClick="lbtnfromloc_Click">
                                                 <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>

                                                    <div class="col-sm-2 labelText1">
                                                        In Loc
                                                    </div>
                                                    <div class="col-sm-3 padding0">
                                                        <asp:TextBox runat="server" ID="txttoloc" OnTextChanged="txttoloc_TextChanged" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft3">
                                                        <asp:LinkButton ID="lbtntoloc" runat="server" CausesValidation="false" OnClick="lbtntoloc_Click">
                                                 <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-2 labelText1">
                                                        From Date
                                                    </div>
                                                    <div class="col-sm-3 padding0">
                                                        <asp:TextBox runat="server" ID="txtfdate" OnTextChanged="txtfdate_TextChanged" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtnfDate" Visible="false" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtfdate"
                                                            PopupButtonID="lbtnSDate" Format="dd/MMM/yyyy">
                                                        </asp:CalendarExtender>
                                                    </div>

                                                    <div class="col-sm-2 labelText1">
                                                        To Date
                                                    </div>
                                                    <div class="col-sm-3 padding0">
                                                        <asp:TextBox runat="server" ID="txttdate" OnTextChanged="txttoloc_TextChanged" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft0">
                                                        <asp:LinkButton ID="lbtntdate" runat="server" Visible="false" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txttdate"
                                                            PopupButtonID="lbtntDate" Format="dd/MMM/yyyy">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-1 labelText1">
                                                        In
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:CheckBox runat="server" ID="chkinword" Checked="true" OnCheckedChanged="chkinword_CheckedChanged" AutoPostBack="true" CausesValidation="false" CssClass="checkboxstyle"></asp:CheckBox>
                                                    </div>
                                                    <div class="col-sm-1 labelText1">
                                                        Out
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:CheckBox runat="server" ID="chkoutward" OnCheckedChanged="chkoutward_CheckedChanged" AutoPostBack="true" CausesValidation="false" CssClass="checkboxstyle"></asp:CheckBox>
                                                    </div>
                                                    <div class="col-sm-2 labelText1">
                                                        <asp:Label ID="lbcomplete" runat="server" Text="Complete" Visible="false"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-1 padding0">
                                                        <asp:CheckBox runat="server" Visible="false" ID="chkcomplete" OnCheckedChanged="chkcomplete_CheckedChanged" AutoPostBack="true" CausesValidation="false" CssClass="checkboxstyle"></asp:CheckBox>
                                                    </div>
                                                    <div class="col-sm-1 labelText1">
                                                        <asp:Label ID="lbgit" runat="server" Text="GIT" Visible="false"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-1 padding0">
                                                        <asp:CheckBox runat="server" Visible="false" ID="chkgz7" OnCheckedChanged="chkgz7_CheckedChanged" AutoPostBack="true" CausesValidation="false" CssClass="checkboxstyle"></asp:CheckBox>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <asp:LinkButton ID="lbtnsearch" OnClick="lbtnsearch_Click" runat="server" CausesValidation="false">
                                                  <span class="glyphicon glyphicon-search" aria-hidden="true" style="font-size:x-large"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="panel panel-default">
                                            <div class="panel-heading pannelheading ">
                                                Search By Document
                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-sm-3 labelText1">
                                                        Doc #
                                                    </div>
                                                    <div class="col-sm-7 padding0">
                                                        <asp:TextBox runat="server" ID="txtdocno" OnTextChanged="txtdocno_TextChanged" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-2 paddingLeft3">
                                                        <asp:LinkButton ID="lbtndocsearch" runat="server" CausesValidation="false" OnClick="lbtndocsearch_Click">
                                                 <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div class="col-sm-2 paddingLeft3">
                                                        <asp:LinkButton ID="lbtnmainsearch2" runat="server" CausesValidation="false" OnClick="lbtnmainsearch2_Click">
                                                 <span class="glyphicon glyphicon-search" aria-hidden="true" style="font-size:x-large"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="panel panel-default">
                                            <div class="panel-heading pannelheading ">
                                                Search By Transport Details
                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-sm-2 labelText1">
                                                        Method
                                                    </div>
                                                    <div class="col-sm-3 padding0">
                                                        <asp:TextBox runat="server" ID="txttransportmethod" OnTextChanged="txttransportmethod_TextChanged" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft3">
                                                        <asp:LinkButton ID="lbtntrmethod" runat="server" CausesValidation="false" OnClick="lbtntrmethod_Click">
                                                 <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div class="col-sm-2 labelText1">
                                                        Party
                                                    </div>
                                                    <div class="col-sm-3 padding0">
                                                        <asp:TextBox runat="server" ID="txttrparty" OnTextChanged="txttrparty_TextChanged" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft3">
                                                        <asp:LinkButton ID="lbtntrparty" runat="server" CausesValidation="false" OnClick="lbtntrparty_Click">
                                                 <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>

                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-2 labelText1">
                                                        Ref #
                                                    </div>
                                                    <div class="col-sm-3 padding0">
                                                        <asp:TextBox runat="server" ID="txtrefno" OnTextChanged="txtrefno_TextChanged" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1 paddingLeft3">
                                                        <asp:LinkButton ID="lbtnrefno" runat="server" CausesValidation="false" OnClick="lbtnrefno_Click">
                                                 <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div class="col-sm-2 paddingLeft3">
                                                        <asp:LinkButton ID="lbtnsearch3" runat="server" CausesValidation="false" OnClick="lbtnsearch3_Click">
                                                 <span class="glyphicon glyphicon-search" aria-hidden="true" style="font-size:x-large"></span>
                                                        </asp:LinkButton>
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
                        <div class="panel-body">
                            <div class="col-sm-6">
                                <div class="panel panel-default">
                                    <div class="panel-heading pannelheading ">
                                        AOD Details
                                    </div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="panelscoll" style="height: 150px">
                                                    <asp:GridView ID="grvaoddetails" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                        <Columns>
                                                            <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="" />
                                                            <asp:TemplateField Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnedititem" CausesValidation="false" CommandName="Select" OnClick="lbtnedititem_Click" CommandArgument="<%# Container.DataItemIndex %>" runat="server">
                                                                                <span class="glyphicon glyphicon-play-circle" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Location">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbCurrentLoc" runat="server" Text='<%# Bind("CurrentLoc") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Issued Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbDocDate" runat="server" Text='<%# Bind("DocDate", "{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Doc #">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbDocNo" runat="server" Text='<%# Bind("DocNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Manual Ref #">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbManualRef" runat="server" Text='<%# Bind("ManualRef") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Remarks">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="User" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbuser" Visible="false" runat="server" Text='<%# Bind("User") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Create Date" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbCreDate" Visible="false" runat="server" Text='<%# Bind("CreDate", "{0:dd/MMM/yyyy}") %>'></asp:Label>
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
                            <div class="col-sm-6">
                                <div class="panel panel-default">
                                    <div class="panel-heading pannelheading ">
                                        More Details
                                    </div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="panelscoll" style="height: 150px">
                                                    <div class="row">
                                                        <div class="col-sm-2" style="font-weight: bolder">
                                                            <asp:Label Visible="false" ID="lbdocnotxt" Text="DOC # :" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <asp:Label Visible="false" ID="lbdocno" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-2" style="font-weight: bolder">
                                                            <asp:Label Visible="false" ID="lbdocdatetxt" Text="Doc Date :" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <asp:Label Visible="false" ID="lbdocdate" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-2" style="font-weight: bolder">
                                                            <asp:Label Visible="false" ID="lbusertxt" Text="USER :" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <asp:Label Visible="false" ID="lbuser" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-2" style="font-weight: bolder">
                                                            <asp:Label Visible="false" ID="lbcredttxt" Text="Create Date :" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <asp:Label Visible="false" ID="lbcredt" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-2" style="font-weight: bolder">
                                                            <asp:Label Visible="false" ID="lbtrmethodtext" Text="Tr Method :" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <asp:Label Visible="false" ID="lbtrmethod" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-2" style="font-weight: bolder">
                                                            <asp:Label Visible="false" ID="lbpartytxt" Text="Party :" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <asp:Label Visible="false" ID="lbparty" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-2" style="font-weight: bolder">
                                                            <asp:Label Visible="false" ID="lbrefnotxt" Text="Reference # :" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <asp:Label Visible="false" ID="lbrefno" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-2" style="font-weight: bolder">
                                                            <asp:Label Visible="false" ID="lbitmcounttxt" Text="Total Item :" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <asp:Label Visible="false" ID="lbitmcount" runat="server"></asp:Label>
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
                        <div class="panel-body">
                            <div class="col-sm-6">
                                <div class="panel panel-default">
                                    <div class="panel-heading pannelheading ">
                                        Item Details
                                    </div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="panelscoll" style="height: 150px">
                                                    <asp:GridView ID="grditemdet" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="Item Code">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbitmcode" runat="server" Text='<%# Bind("iti_itm_cd") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Model">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbmodel" runat="server" Text='<%# Bind("itmmodel") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbstatus" runat="server" Text='<%# Bind("iti_itm_stus") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Qty">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbManualRef" runat="server" Text='<%# Bind("iti_qty") %>'></asp:Label>
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
                            <div class="col-sm-6">
                                <div class="panel panel-default">
                                    <div class="panel-heading pannelheading ">
                                        Serial Details
                                    </div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="panelscoll" style="height: 150px">
                                                    <asp:GridView ID="grdserialdet" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="Item Code">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbitmcode2" runat="server" Text='<%# Bind("its_itm_cd") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbstatus2" runat="server" Text='<%# Bind("its_itm_stus") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Serial">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbserial1" runat="server" Text='<%# Bind("its_ser_1") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Other Serial">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbserial2" runat="server" Text='<%# Bind("its_ser_2") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Warranty #">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbserial2" runat="server" Text='<%# Bind("its_warr_no") %>'></asp:Label>
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:UpdatePanel runat="server" ID="update">
        <ContentTemplate>
            <asp:Button ID="btn1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupSearch" runat="server" Enabled="True" TargetControlID="btn1"
                PopupControlID="pnlSearch" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel runat="server" ID="pnlSearch" DefaultButton="lbtnSearch">
        <div runat="server" id="test" class="panel panel-primary Mheight" style="width: 700px;">
            <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default" style="height: 370px;">
                <div class="panel-heading" style="height: 25px;">
                    <div class="col-sm-11"></div>
                    <div class="col-sm-1">
                        <asp:LinkButton ID="btnClose" runat="server" OnClick="btnClose_Click">
                             <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="panel-body">
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
                            <div class="col-sm-3 paddingRight5">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtSearchbyword" CausesValidation="false" OnTextChanged="txtSearchbyword_TextChanged" class="form-control" AutoPostBack="false" runat="server"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy9" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-1 paddingLeft0">
                                        <asp:LinkButton ID="lbtnsearchpopup" runat="server" OnClick="lbtnsearchpopup_Click">
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
                                    <asp:GridView ID="dgvResult" CausesValidation="false" runat="server" AllowPaging="True"
                                        GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" ShowHeaderWhenEmpty="true"
                                        EmptyDataText="No data found..."
                                        OnPageIndexChanging="dgvResult_PageIndexChanging" OnSelectedIndexChanged="dgvResult_SelectedIndexChanged">
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
</asp:Content>
