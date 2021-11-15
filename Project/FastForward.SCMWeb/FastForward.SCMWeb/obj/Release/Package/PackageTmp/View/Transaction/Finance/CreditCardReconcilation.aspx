<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="CreditCardReconcilation.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Finance.CreditCardReconcilation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/Sales.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/jquery.toastmessage.js"></script>
    <link href="<%=Request.ApplicationPath%>Css/jquery-ui.theme.min.css" rel="stylesheet" />
    <link href="<%=Request.ApplicationPath%>Css/jquery-ui.min.css" rel="stylesheet" />

    <script>
        function ConfSave() {
            var selectedvalueOrd = confirm("Are you sure do you want to save ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfEdit() {
            var selectedvalueOrd = confirm("Are you sure do you want to edit ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfClear() {
            var selectedvalueOrd = confirm("Are you sure do you want to clear ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfExp() {
            var selectedvalueOrd = confirm("Are you sure do you want to Export details?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };

    </script>

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

        .panel {
            padding-top: 0px;
            padding-bottom: 0px;
            margin-bottom: 0px;
            margin-top: 0px;
        }

        .txtUpper {
            text-transform: uppercase;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="row">
                <div class="col-sm-12">
                    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="pnlMain">
                        <ProgressTemplate>
                            <div class="divWaiting">
                                <asp:Label ID="lblWait" runat="server"
                                    Text="Please wait... " />
                                <asp:Image ID="imgWait" runat="server"
                                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:UpdatePanel runat="server" ID="pnlMain">
                        <ContentTemplate>
                            <div class="col-sm-12">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-9">
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="buttonRow" style="height: 30px; margin-top: -12px;">
                                                <div class="col-sm-4 padding0 text-center" style="width: 70px;">
                                                    <asp:LinkButton OnClick="lbexport_Click" ID="lbexport" OnClientClick="return ConfExp();" CausesValidation="false" runat="server"
                                                        CssClass=""> 
                                                        <span class="glyphicon glyphicon-export" aria-hidden="true"></span>Export</asp:LinkButton>
                                                </div>
                                                <div class="col-sm-4 padding0 text-center" style="width: 70px;">
                                                    <asp:LinkButton OnClick="lbtnSave_Click" ID="lbtnSave" OnClientClick="return ConfSave();" CausesValidation="false" runat="server"
                                                        CssClass=""> 
                                                        <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Save</asp:LinkButton>
                                                </div>

                                                <div class="col-sm-4 padding0 text-center">
                                                    <asp:LinkButton ID="lbtnClear" OnClick="lbtnClear_Click" CausesValidation="false" runat="server"
                                                        OnClientClick="return ConfClear();" CssClass=""> 
                                                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="panel panel-default">
                                            <div class="panel panel-heading">
                                                <strong>Credit Card Reconcilation</strong>
                                            </div>
                                            <div class="panel panel-body padding0">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="panel panel-default">
                                                            <div class="panel panel-heading">
                                                                <asp:CheckBox runat="server" ID="chkselectall" OnCheckedChanged="chkselectall_CheckedChanged" Text="All" AutoPostBack="true" />
                                                                <div class="panel panel-body padding0">
                                                                    <div class="col-sm-3">
                                                                        <div class="col-sm-12">
                                                                            <div class="row">
                                                                                <div class="panelscoll" style="height: 150px">
                                                                                    <asp:GridView ID="gvpclist" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="Select">
                                                                                                <ItemTemplate>
                                                                                                    <asp:CheckBox runat="server" ID="chkselect" Width="1px" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="PC">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lbpccd" runat="server" Text='<%# Bind("Code") %>' Width="50px"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-9">
                                                                        <div class="col-sm-12">
                                                                            <div class="row">
                                                                                <div class="col-sm-1">
                                                                                    <asp:LinkButton ID="lbtnaddmid" runat="server" CausesValidation="false" OnClick="lbtnaddmid_Click">
                                                 <span class="glyphicon glyphicon-arrow-right" style="font-size:xx-large" aria-hidden="true">  </span> 
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                                <div class="col-sm-1">
                                                                                    <asp:CheckBox ID="chkmidall" runat="server" Text="All" />
                                                                                </div>
                                                                                <div class="col-sm-3">
                                                                                    <div class="col-sm-2">MID</div>
                                                                                    <div class="col-sm-10">
                                                                                        <asp:DropDownList ID="dropMID" Visible="true" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="dropMID_SelectedIndexChanged"></asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-3">
                                                                                    <div class="col-sm-5 labelText1">
                                                                                        From  Date
                                                                                    </div>
                                                                                    <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                                        <asp:TextBox runat="server" ID="txtSDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-sm-1 paddingLeft0">
                                                                                        <asp:LinkButton ID="lbtnSDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                                        </asp:LinkButton>
                                                                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtSDate"
                                                                                            PopupButtonID="lbtnSDate" Format="dd/MMM/yyyy">
                                                                                        </asp:CalendarExtender>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-3">
                                                                                    <div class="col-sm-5 labelText1">
                                                                                        To  Date
                                                                                    </div>
                                                                                    <div class="col-sm-6 paddingRight5 paddingLeft0">
                                                                                        <asp:TextBox runat="server" ID="txtEDate" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-sm-1 paddingLeft0">
                                                                                        <asp:LinkButton ID="lbtnEDate" runat="server" CausesValidation="false">
                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                                        </asp:LinkButton>
                                                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEDate"
                                                                                            PopupButtonID="lbtnEDate" Format="dd/MMM/yyyy">
                                                                                        </asp:CalendarExtender>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-1">
                                                                                    <asp:LinkButton ID="lbtnsearch" runat="server" CausesValidation="false" OnClick="lbtnsearch_Click">
                                                 <span class="glyphicon glyphicon-search" style="font-size:xx-large" aria-hidden="true">  </span> 
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-sm-2">
                                                                                    <asp:Label ID="lbbank" Visible="true" runat="server"></asp:Label>
                                                                                </div>
                                                                                <div class="col-sm-3">
                                                                                    <div class="col-sm-2">CR</div>
                                                                                    <div class="col-sm-10">
                                                                                        <asp:TextBox ID="txtcraccount" Visible="true" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-3">
                                                                                    <div class="col-sm-2">DR</div>
                                                                                    <div class="col-sm-10">
                                                                                        <asp:TextBox ID="txtdraccount" Visible="true" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-3">
                                                                                    <asp:TextBox ID="txtremark" TextMode="multiline" Visible="true" CausesValidation="false" AutoPostBack="true" runat="server" CssClass="form-control" placeholder="Remark"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-12">
                                                        <div class="panel panel-default">
                                                            <div class="panel panel-heading">
                                                                <asp:CheckBox runat="server" ID="chkallreciept" OnCheckedChanged="chkallreciept_CheckedChanged" Text="All" AutoPostBack="true" />
                                                                <div class="panel panel-body padding0">
                                                                    <div class="col-sm-10">
                                                                        <div class="panelscoll" style="height: 250px">
                                                                            <asp:GridView ID="gvreciptlist" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Select">
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox runat="server" ID="chkselect2" Width="1px" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Trans Date">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbtrdate" runat="server" Text='<%# Bind("sar_receipt_date", "{0:dd/MMM/yyyy}") %>' Width="50px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Description">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbdescrip" runat="server" Text='<%# Bind("sard_ref_no") %>' Width="50px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Auth Code">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbauth" runat="server" Text='<%# Bind("sar_create_by") %>' Width="50px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Trans Ammount">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbtrammount" runat="server" Text='<%# Bind("sar_tot_settle_amt") %>' Width="50px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Comm Ammount">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbcomm" runat="server" Text='<%# Bind("sard_bnk_amt") %>' Width="50px"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="RecNo" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbrecno" runat="server" Text='<%# Bind("sar_receipt_no") %>' Width="50px" Visible="false"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                      <asp:TemplateField HeaderText="MID" Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbmid" runat="server" Text='<%# Bind("sard_chq_branch") %>' Width="50px" Visible="false"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-2">CALCULATE</div>
                                                                    <div class="col-sm-2">
                                                                        <asp:LinkButton ID="lbncalc" runat="server" CausesValidation="false" OnClick="lbncalc_Click">
  <span class="glyphicon glyphicon-arrow-down" style="font-size:xx-large"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-12">
                                                        <div class="panel panel-default">
                                                            <div class="panel panel-heading">
                                                                <div class="panel panel-body padding0">

                                                                    <div class="col-sm-4">
                                                                        <div class="col-sm-4">Gross Amount :</div>
                                                                        <div class="col-sm-6">
                                                                            <asp:Label ID="lbgrossamount" runat="server" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-4">
                                                                        <div class="col-sm-4">Commissions :</div>
                                                                        <div class="col-sm-6">
                                                                            <asp:TextBox ID="txtcommissions" runat="server" CssClass="form-control" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-4">
                                                                        <div class="col-sm-4">Net Payments :</div>
                                                                        <div class="col-sm-6">
                                                                            <asp:Label ID="lbnetpayment" runat="server" />
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
                </div>
            </div>
        </div>
    </div>

</asp:Content>
