<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="SunAutomation.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Finance.SunAutomation" %>

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
             var res = confirm("Do you want to upload?");
             if (res) {
                 return true;
             }
             else {
                 return false;
             }
         };
          </script>

      <script type="text/javascript">
          function disale() {
              <%--  var btn = document.getElementById("<%=btnPrdChngYes.ClientID%>");
              btn.disabled = true;
              var btn2 = document.getElementById("<%=btnPrdChngNo.ClientID%>");
              btn2.disabled = true;--%>
              //var pnl = document.getElementById("<%=pnlSoConfBox.ClientID%>");
              document.getElementById('btnPrdChngYes').disabled = true;
              document.getElementById('btnPrdChngNo').disabled = true;
              //pnl.hidden = true;

              
          };
          </script>



--    <script type="text/javascript">
        function confirmation() {
            var answer = confirm("Sun period closed for Upload Date.Press ‘OK’ to upload details into current month.Press ‘CANCEL’ to abort upload.")
            if (answer) {
                //document.getElementByID("hdnValue").value = 'true';
                Sunupload("Invoice",true,Onsuc,onfai) ;
            }
            else {
                //document.getElementByID("hdnValue").value = 'false';
            }
        }
    </script>-


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
                            <strong>Sun Automation</strong>
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
                        <div class="col-sm-3">
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
                        <div class="col-sm-3">
                            <div class="col-sm-5 labelText1">
                                Type
                            </div>
                            <div class="col-sm-6 paddingRight5 paddingLeft0">

                                <asp:DropDownList ID="Ddpaytype" runat="server" CssClass="form-control" OnSelectedIndexChanged="Ddpaytype_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Enabled="true" Text="Select Type" Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="Invoice" Value="Invoice"></asp:ListItem>
                                    <asp:ListItem Text="Reciept" Value="Reciept"></asp:ListItem>
                                    <asp:ListItem Text="Local Purchase" Value="LCLPC"></asp:ListItem>
                                    <asp:ListItem Text="Credit Note" Value="CRNT"></asp:ListItem>
                                    <asp:ListItem Text="Imports Schedule" Value="IMPSDL"></asp:ListItem>
                                     <asp:ListItem Text="Credit Card" Value="CRCDACK"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-1">
                            <asp:Button runat="server" ID="btnview" Text="View" CssClass="form-control btn-primary" OnClick="btnview_Click" />
                        </div>
                        <div class="col-sm-1">
                            <asp:Button runat="server" ID="btnclear" Text="Clear" CssClass="form-control btn-primary" OnClick="btnclear_Click" />
                        </div>
                        <div class="col-sm-1">
                            <asp:Button runat="server" ID="btnexport" Text="Export" CssClass="form-control btn-primary" OnClick="btnexport_Click" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-3">
                            <div class="col-sm-5 labelText1">
                                SBU
                            </div>
                            <div class="col-sm-6 padding0">
                                <asp:DropDownList runat="server" ID="drpsbu" AutoPostBack="true" CausesValidation="false" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <%--                            <div class="col-sm-1 paddingLeft3">
                                <asp:LinkButton ID="btnsbusearch" runat="server" CausesValidation="false" OnClick="btnsbusearch_Click">
                                                 <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </div>--%>
                        </div>
                        <div class="col-sm-1">
                            <asp:Button runat="server" ID="btnupload" Text="Upload" CssClass="form-control btn-info" OnClick="btnupload_Click" OnClientClick="return confSave()"/>
                        </div>
                        <div class="col-sm-3">
                            <div class="col-sm-1">
                                <asp:CheckBox ID="chkPstCurPrd" AutoPostBack="true" OnCheckedChanged="chkPstCurPrd_CheckedChanged" runat="server" Width="1px"></asp:CheckBox>
                            </div>
                            <div class="col-sm-5 padding3">
                                Post Current Period
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-2">                      
                        <div class="panel panel-default">
                            <div class="panel-heading pannelheading ">
                                PC Details
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="panelscoll" style="height: 250px">
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
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <div class="col-sm-6 labelText1">
                                Sellect All
                            </div>
                            <div class="col-sm-1 paddingRight5 paddingLeft0">
                                <asp:CheckBox ID="chkall" runat="server" OnCheckedChanged="chkall_CheckedChanged" AutoPostBack="true" />
                            </div>
                        </div>
                    </div>

                     

                    <div class="col-sm-10">
                        <div class="panel panel-default">
                            <div class="panel-heading pannelheading ">
                                SUN Details
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="panelscoll" style="height: 250px">
                                            <asp:GridView ID="grdsundata"  AutoGenerateColumns="false" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No data found...">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Reference">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTransactionRef" runat="server" Text='<%# Bind("TransactionRef") %>' Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Document">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldocno" runat="server" Text='<%# Bind("Docno") %>' Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Journal Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblJournalType" runat="server" Text='<%# Bind("JournalType") %>' Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Period">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPeriod" runat="server" Text='<%# Bind("Period") %>' Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTransactionDate" runat="server" Text='<%# Bind("TransactionDate","{0:dd/MMM/yyyy}") %>' Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Desc">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>' Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Account">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAccountCode" runat="server" Text='<%# Bind("AccountCode") %>' Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CAT">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("CommonCat") %>' Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Charge">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("CommonVal") %>' Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("Amount") %>' Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CR/DR">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDebtCrdt" runat="server" Text='<%# Bind("DebtCrdt") %>' Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="LC">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLC_VehicleNo" runat="server" Text='<%# Bind("LC_VehicleNo") %>' Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PC">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPC" runat="server" Text='<%# Bind("PC") %>' Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Journal Source">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblJournalSours" runat="server" Text='<%# Bind("JournalSours") %>' Width="120px"></asp:Label>
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
                            <div class="panel-heading">
                                Local Purcase Details
                            </div>
                            <div class="panel-body">
                                <div class="panelscoll" style="height: 120px">

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
                                                    <asp:TextBox ID="txtlpsundes" runat="server" Width="120px" Text='<%# Bind("ith_oth_docno") %>'></asp:TextBox>
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
                    >
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%-----------------------------------------------------------%>
<%--    <asp:UpdateProgress ID="UpdateProgress11" DisplayAfter="1" runat="server" AssociatedUpdatePanelID="UpdatePanel27">
                            <ProgressTemplate>
                                <div class="divWaiting">
                                    <asp:Label ID="lblWaitx" runat="server" Text="Please wait... " />
                                    <asp:Image ID="imgWaitx" runat="server" ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>--%>
      <asp:UpdatePanel ID="UpdatePanel27" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button5" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="MdlSunUpload" runat="server" Enabled="True" TargetControlID="Button5"
                PopupControlID="pnlSoConfBox" PopupDragHandleControlID="PopupSoHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel ID="pnlSoConfBox" runat="server" align="center">
        <div runat="server" id="PopupSoHeader" class="panel panel-info height140" style="width: 300px;">
            <asp:Label ID="Label16" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:ScriptManagerProxy ID="ScriptManagerProxy23" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                <ContentTemplate>

                    <div class="panel panel-heading">
                        <span>Alert</span>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label18" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label19" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label20" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label21" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label22" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height22">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-2">
                            </div>
                            <div class="col-sm-4">
                                <asp:Button ID="btnPrdChngYes" runat="server" Text="yes" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs" OnClientClick="return disale()" OnClick="btnPrdChngYes_Click" />
                            </div>
                            <div class="col-sm-2 ">
                                <asp:Button ID="btnPrdChngNo" runat="server" Text="No" Width="80px" CausesValidation="false" class="btn btn-primary btn-xs"  OnClientClick="return disale()" OnClick="btnPrdChngNo_Click"  />
                            </div>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>

    <%-----------------------------------------------------------%>
</asp:Content>
