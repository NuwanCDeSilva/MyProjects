<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="SunUpload.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Finance.SunUpload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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
    </script>





</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading paddingtop0 paddingtopbottom0">

            <div class="row">
                <div class="col-sm-7">
                    <strong>Sun Upload</strong>
                </div>
                <div class="col-sm-2">
                </div>
                <div class="col-sm-3">
                </div>
            </div>
        </div>

        <div class="panel-body">
            <div class="row">
                <div class="col-sm-3">
                    <div class="row">
                        <div class="col-sm-5 labelText1">
                            Start  Date
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
                </div>
                <div class="col-sm-3">
                    <div class="row">
                        <div class="col-sm-5 labelText1">
                            End  Date
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
                </div>
                <div class="col-sm-3">
                    <div class="row">
                        <div class="col-sm-5 labelText1">
                            Type
                        </div>
                        <div class="col-sm-6 paddingRight5 paddingLeft0">

                            <asp:DropDownList ID="Ddpaytype" runat="server" CssClass="form-control" OnSelectedIndexChanged="Ddpaytype_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Enabled="true" Text="Select Type" Value="-1"></asp:ListItem>
                                <asp:ListItem Text="Invoice" Value="Invoice"></asp:ListItem>
                                <asp:ListItem Text="Reciept" Value="Reciept"></asp:ListItem>
                                <asp:ListItem Text="Local Perch" Value="LCLPC"></asp:ListItem>
                                <asp:ListItem Text="Credit Note" Value="CRNT"></asp:ListItem>
                            </asp:DropDownList>

                        </div>

                    </div>
                </div>
                <div class="col-sm-1">
                    <asp:Button runat="server" ID="btnupload" Text="Upload" CssClass="form-control btn-primary" OnClick="btnupload_Click" />
                </div>
                <div class="col-sm-1">
                    <asp:Button runat="server" ID="btnclear" Text="Clear" CssClass="form-control btn-primary" OnClick="btnclear_Click" />
                </div>
            </div>
            <div class="row">
                <div class="col-sm-3">
                    <div class="row">
                        <div class="col-sm-5 labelText1">
                            Divition  Type
                        </div>
                        <div class="col-sm-6 paddingRight5 paddingLeft0">

                            <asp:DropDownList ID="Dddivtype" runat="server" CssClass="form-control" OnSelectedIndexChanged="Dddivtype_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Enabled="true" Text="Select Type" Value="-1"></asp:ListItem>
                                <asp:ListItem Text="DEBTOR" Value="DEBT"></asp:ListItem>
                                <asp:ListItem Text="OTHR" Value="OTHR"></asp:ListItem>
                            </asp:DropDownList>

                        </div>

                    </div>
                </div>
                <div class="col-sm-2">
                    <div class="row">
                        <div class="col-sm-4 labelText1">
                            Sellect All
                        </div>
                        <div class="col-sm-2 paddingRight5 paddingLeft0">

                            <asp:CheckBox ID="chkall" runat="server" OnCheckedChanged="chkall_CheckedChanged" AutoPostBack="true" />

                        </div>
                        <div class="col-sm-4 labelText1">
                            Block
                        </div>
                        <div class="col-sm-2 paddingRight5 paddingLeft0">

                            <asp:CheckBox ID="chkblock" runat="server" />

                        </div>
                        <div class="col-sm-4 labelText1">
                            Fix
                        </div>
                        <div class="col-sm-2 paddingRight5 paddingLeft0">

                            <asp:CheckBox ID="chkfix" runat="server" OnCheckedChanged="chkfix_CheckedChanged" AutoPostBack="true" />

                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-5">

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
                                        <asp:Label ID="lbpccd" runat="server" Text='<%# Bind("SN_PCCD") %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>

                        </asp:GridView>

                    </div>


                </div>
                <div class="col-sm-6">

                    <div class="panelscoll" style="height: 150px">

                        <asp:GridView ID="gvgrnlist" AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                            <Columns>
                                <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkselectgrn" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="GRN NO">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrnno" runat="server" Text='<%# Bind("ith_doc_no") %>' Width="120px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DATE">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrndate" runat="server" Text='<%# Bind("ith_doc_date", "{0:dd/MMM/yyyy}") %>' Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="lbcost" runat="server" Text='<%# Bind("Cost") %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtlpsundes" runat="server" Width="120px" Text='<%#Bind("InvNo") %>' ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Period">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtlpsundate" runat="server" Text='<%# Bind("ith_doc_date", "{0:dd/MMM/yyyy}") %>' Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Save">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnsavelpdes" runat="server" Width="50px" OnClick="btnsavelpdes_Click">
                                            <span class="glyphicon glyphicon-save"></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btneditlpdes" runat="server" Width="50px" OnClick="btneditlpdes_Click">
                                            <span class="glyphicon glyphicon-edit"></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>

                    </div>


                </div>
            </div>
        </div>



    </div>


</asp:Content>
