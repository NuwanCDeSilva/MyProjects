<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="CreditCardAcknow.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Finance.CreditCardAcknow" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/Sales.js"></script>
    <script src="<%=Request.ApplicationPath%>Js/jquery.toastmessage.js"></script>
    <link href="<%=Request.ApplicationPath%>Css/jquery-ui.theme.min.css" rel="stylesheet" />
    <link href="<%=Request.ApplicationPath%>Css/jquery-ui.min.css" rel="stylesheet" />
        <script type="text/javascript">
            function confSave() {
                var res = confirm("Do you want to save?");
                if (res) {
                    return true;
                }
                else {
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
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="upPnl1">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="upPnl1">
        <ContentTemplate>
            <div class="panel panel-default">
                <div class="panel-heading paddingtop0 paddingtopbottom0">
                    <div class="row">
                        <div class="col-sm-7">
                            <strong>Credit Card Payment Recieve Acknowledge</strong>
                        </div>
                        <div class="col-sm-2">
                        </div>
                        <div class="col-sm-3">
                        </div>
                    </div>
                </div>
                <div class="panel-body">
                       <div class="row">
                           <div class="col-sm-8"></div>
                        <div class="col-sm-1">
                            <asp:Button runat="server" ID="btnsave" Text="Save" CssClass="form-control btn-primary" OnClick="btnsave_Click" OnClientClick="return confSave()" />
                        </div>
                        <div class="col-sm-1">
                            <asp:Button runat="server" ID="btnupload" Text="Upload" CssClass="form-control btn-primary" OnClick="btnupload_Click" />
                        </div>
                        <div class="col-sm-1">
                            <asp:Button runat="server" ID="btnprint" Text="Print" CssClass="form-control btn-primary" OnClick="btnprint_Click" />
                        </div>
                        <div class="col-sm-1">
                            <asp:Button runat="server" ID="btnclear" Text="Clear" CssClass="form-control btn-primary" OnClick="btnclear_Click" />
                        </div>
                    </div>
                    <div class="row">
                         <div class="col-sm-12"></div>
                    </div>
                    <div class="row">
                        <div class="col-sm-3">
                            <div class="col-sm-5 labelText1">
                                Doc No
                            </div>
                            <div class="col-sm-6 paddingRight5 paddingLeft0">
                                <asp:TextBox runat="server" ID="txtcrddoc" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-1 paddingLeft0 paddingRight0">
                                <asp:LinkButton ID="btncrddoc" runat="server" CausesValidation="false" OnClick="btncrddoc_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="col-sm-5 labelText1">
                                Bank Code
                            </div>
                            <div class="col-sm-6 paddingRight5 paddingLeft0">
                                <asp:TextBox runat="server" ID="txtbankcd" CausesValidation="false" CssClass="form-control" OnTextChanged="txtbankcd_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </div>
                            <div class="col-sm-1 paddingLeft0 paddingRight0">
                                <asp:LinkButton ID="btnbnkcd" runat="server" CausesValidation="false" OnClick="btnbnkcd_Click">
                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="col-sm-5 labelText1">
                               State Date
                            </div>
                            <div class="col-sm-6 paddingRight5 paddingLeft0">
                                <asp:TextBox runat="server" ID="txtSDate" CausesValidation="false" CssClass="form-control" OnTextChanged="txtSDate_TextChanged" AutoPostBack="true"></asp:TextBox>
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
                                Amount
                            </div>
                            <div class="col-sm-6 paddingRight5 paddingLeft0">
                                <asp:TextBox runat="server" ID="txtamount" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-1 paddingLeft0 paddingRight0">
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-3">
                            <div class="col-sm-5 labelText1">
                                CR Account
                            </div>
                            <div class="col-sm-6 paddingRight5 paddingLeft0">
                                <asp:TextBox runat="server" ID="txtcraccount" CausesValidation="false" CssClass="form-control" OnTextChanged="txtcraccount_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </div>
                            <div class="col-sm-1 paddingLeft0 paddingRight0">
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="col-sm-5 labelText1">
                                DR Account
                            </div>
                            <div class="col-sm-6 paddingRight5 paddingLeft0">
                                <asp:TextBox runat="server" ID="txtdraccount" CausesValidation="false" CssClass="form-control" OnTextChanged="txtdraccount_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </div>
                            <div class="col-sm-1 paddingLeft0 paddingRight0">
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="col-sm-5 labelText1">
                                MID
                            </div>
                            <div class="col-sm-6 paddingRight5 paddingLeft0">
                                <asp:TextBox runat="server" ID="txtmid" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-1 paddingLeft0">
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="col-sm-5 labelText1">
                                Remark
                            </div>
                            <div class="col-sm-6 paddingRight5 paddingLeft0">
                                <asp:TextBox runat="server" ID="txtremark" CausesValidation="false" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                            <div class="col-sm-1 paddingLeft0 paddingRight0">
                            </div>
                        </div>
                        <div class="col-sm-2 padding0" style="padding-left: 35px">
                            <asp:LinkButton ID="lbtnadditems" runat="server" TabIndex="38" CausesValidation="false" OnClick="lbtnadditems_Click">
                                <span class="glyphicon glyphicon-plus" style="font-size:20px;"  aria-hidden="true"></span>
                            </asp:LinkButton>
                        </div>
                    </div>
                 
                </div>
            </div>
                                    <div class="row">
                                            <div class="col-sm-12">
                                                <div class="panel panel-default">
                                                    <div class=" panel-body height200 panelscollbar">
                                                        <asp:GridView ID="grdInvoices" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="Both" CssClass="table table-hover table-striped" AutoGenerateColumns="false" OnSelectedIndexChanged="grdInvoices_SelectedIndexChanged">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemStyle CssClass="gridHeaderAlignCenter" Width="80px" />
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbtnDetaltecost" CausesValidation="false" CommandName="Delete" runat="server" OnClientClick="DeleteConfirm()" OnClick="lbtnDetaltecost_Click">
                                                                            <span class="glyphicon glyphicon-trash" aria-hidden="true" style="font-size:15px"></span> 
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Bank Code" Visible="true">
                                                                    <ItemStyle CssClass="gridHeaderAlignRight" Width="100px"/>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_staj_bankcd" runat="server" Text='<%# Bind("staj_bankcd") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="gridHeaderAlignCenter" />
                                                                    <HeaderStyle CssClass="gridHeaderAlignCenter" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="State Date" Visible="true">
                                                                    <ItemStyle CssClass="gridHeaderAlignRight" Width="100px"/>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_staj_state_date" runat="server" Text='<%# Bind("staj_state_date") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="gridHeaderAlignCenter" />
                                                                    <HeaderStyle CssClass="gridHeaderAlignCenter" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Amount" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_staj_amt" runat="server" Text='<%# Bind("staj_amt","{0:n}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="gridHeaderAlignRight" Width="100px" />
                                                                    <HeaderStyle CssClass="gridHeaderAlignCenter" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CR Account" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_staj_acc_crd" runat="server" Text='<%# Bind("staj_acc_crd") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="gridHeaderAlignCenter" Width="100px" />
                                                                    <HeaderStyle CssClass="gridHeaderAlignCenter" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="DR Account" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_staj_acc_dbt" runat="server" Text='<%# Bind("staj_acc_dbt") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="gridHeaderAlignCenter" Width="100px" />
                                                                    <HeaderStyle CssClass="gridHeaderAlignCenter" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="MID" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_staj_midno" runat="server" Text='<%# Bind("staj_midno") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="gridHeaderAlignCenter" Width="100px" />
                                                                    <HeaderStyle CssClass="gridHeaderAlignCenter" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Remark" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="col_staj_rmk" runat="server" Text='<%# Bind("staj_rmk") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="gridHeaderAlignCenter" Width="180px" />
                                                                    <HeaderStyle CssClass="gridHeaderAlignCenter" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
        </ContentTemplate>
    </asp:UpdatePanel>

        <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserPopup" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="pnlpopup" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpopup" DefaultButton="lbtnSearch">
        <div runat="server" id="test" class="panel panel-primary Mheight">

            <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="btnClose" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
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
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>

                                    <div id="resultgrd" class="panelscoll POPupResultspanelscroll">
                                        <asp:GridView ID="grdResult" CausesValidation="false" AllowPaging="true" runat="server" GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" OnPageIndexChanging="grdResult_PageIndexChanging" OnSelectedIndexChanged="grdResult_SelectedIndexChanged">
                                            <Columns>
                                                <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

        <asp:UpdatePanel ID="UpdatePanel25" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnconfbox" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupConfBox" runat="server" Enabled="True" TargetControlID="btnconfbox"
                PopupControlID="pnlConfBox" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel ID="pnlConfBox" runat="server" align="center">
        <div runat="server" id="Div23" class="panel panel-info height120 width250">
            <asp:Label ID="Label6" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:ScriptManagerProxy ID="ScriptManagerProxy21" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                <ContentTemplate>

                    <div class="panel panel-heading">
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
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblMssg3" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblMssg4" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height22">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-1">
                            </div>
                            <div class="col-sm-4">
                                <asp:Button ID="btnalertYes" runat="server" Text="yes" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnalertYes_Click" />
                            </div>
                            <div class="col-sm-2 ">
                                <asp:Button ID="btnalertNo" runat="server" Text="No" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClick="btnalertNo_Click" />
                            </div>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>
</asp:Content>
