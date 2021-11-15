<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="DemurrageTracker.aspx.cs" Inherits="FastForward.SCMWeb.View.Enquiries.Imports.Demurrage_Tracker" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script>

        function SaveConfirm() {
            var selectedvalue = confirm("Do you want to Save data?");
            if (selectedvalue) {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtSavelconformmessageValue.ClientID %>').value = "No";
            }
        };
        function ClearConfirm() {
            var selectedvalue = confirm("Do you want to Clear data?");
            if (selectedvalue) {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtClearlconformmessageValue.ClientID %>').value = "No";
            }
        };
        function DeleteConfirm() {
            var selectedvalue = confirm("Do you want to Delete data?");
            if (selectedvalue) {
                document.getElementById('<%=txtDeleteconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtDeleteconformmessageValue.ClientID %>').value = "No";
            }
        };
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
              && (charCode < 48 || charCode > 57))
                return false;
            return true;
        };
        function checkDateto(sender, args) {

            if ((sender._selectedDate < new Date())) {
                $().toastmessage('showToast', {
                    text: 'You cannot select a day earlier than today!',
                    sticky: false,
                    position: 'top-center',
                    type: 'warning',
                    closeText: '',
                    close: function () {
                        console.log("toast is closed ...");
                    }

                });
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        };

    </script>
    <script type="text/javascript">
        function closeDialog() {
            $(this).showStickySuccessToast("close");
        }
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
                position: 'top-left',
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
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="txtClearlconformmessageValue" runat="server" />
            <asp:HiddenField ID="txtSavelconformmessageValue" runat="server" />
            <asp:HiddenField ID="txtUpdateconformmessageValue" runat="server" />
            <asp:HiddenField ID="txtApprovalconformmessageValue" runat="server" />
            <asp:HiddenField ID="txtCancelconformmessageValue" runat="server" />
            <asp:HiddenField ID="txtDeleteconformmessageValue" runat="server" />
            <div class="panel panel-default marginLeftRight5 paddingbottom0">
                <div class="panel-heading">
                    Demurrage Tracker
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12 height5">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="col-sm-12">
                                <div class="col-sm-4 paddingLeft0">
                                    <div class="row">
                                        <div class="col-sm-4 labelText1">
                                            From Date
                                        </div>
                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control"
                                                Format="dd/MMM/yyyy" Enabled="false">
                                            </asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 paddingLeft0 paddingRight0">
                                            <asp:LinkButton ID="lbtnFromDate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtFromDate"
                                                PopupButtonID="lbtnFromDate" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4 paddingLeft0">
                                    <div class="row">
                                        <div class="col-sm-4 labelText1">
                                            To Date
                                        </div>
                                        <div class="col-sm-6 paddingRight5 paddingLeft0">
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control"
                                                Format="dd/MMM/yyyy" Enabled="false" OnTextChanged="txtToDate_TextChanged">
                                            </asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 paddingLeft0 paddingRight0">
                                            <asp:LinkButton ID="lbtnToDate" runat="server" CausesValidation="false">
                                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                            </asp:LinkButton>
                                            <asp:CalendarExtender ID="CalendarExtender5" runat="server" OnClientDateSelectionChanged="checkDateto" TargetControlID="txtToDate"
                                                PopupButtonID="lbtnToDate" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                </div>
                                <asp:Panel runat="server" DefaultButton="btnRCompnay">
                                    <div class="col-sm-4 paddingLeft0">
                                        <div class="row">
                                            <div class="col-sm-4 labelText1">
                                                BL #
                                            </div>
                                            <div class="col-sm-7 paddingRight5 paddingLeft0">
                                                <asp:TextBox ID="txtBlNo" CausesValidation="false" runat="server" class="form-control" OnTextChanged="txtBlNo_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 paddingLeft0 paddingRight0">
                                                <asp:LinkButton ID="lbtnRequestNo" runat="server" OnClick="lbtnRequestNo_Click">
                                                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:Button ID="btnRCompnay" runat="server" OnClick="lbtnSearchall_Click" Text="Submit" Style="display: none;" />
                                </asp:Panel>
                            </div>
                        </div>
                        <div class="col-sm-1 paddingLeft0">
                           
                            
                                    <asp:LinkButton ID="lbtnSearchall" runat="server" OnClick="lbtnSearchall_Click">
                                                            <span class="glyphicon glyphicon-search fontsize15 right5" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="col-sm-9">
                                <div class="panel-body  panelscollbar height400">
                                    <asp:GridView ID="grdBlDetails" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                        EmptyDataText="No data found..." AutoGenerateColumns="False" OnRowDataBound="OnRowDataBound" OnRowUpdating="grdBlDetails_RowUpdating">
                                        <Columns>

                                            <asp:TemplateField HeaderText="BL #">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_BL" runat="server" Text='<%# Bind("BL") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BL Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_BL_dt" runat="server" Text='<%# Bind("DATE", "{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Supplier">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_ib_supp_cd" runat="server" Text='<%# Bind("ib_supp_cd") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="12%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="name" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_sname" runat="server" Text='<%# Bind("mbe_name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" Width="12%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Consignee">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_ib_consi_cd" runat="server" Text='<%# Bind("ib_consi_cd") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="12%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ETA Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_ib_eta" runat="server" Text='<%# Bind("ib_eta", "{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ETD Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_ib_etd" runat="server" Text='<%# Bind("ib_etd", "{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_Amount" runat="server" Text='<%# Bind("icet_actl_rt","{0:n}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" Width="8%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Demurrage Reason">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_reson" runat="server" ForeColor="Red" Width="150"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlreson2" runat="server" class="form-control" Width="100">
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="lblremarks2" placeholder="Remark" TextMode="MultiLine" class="form-control" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="secondremarks2" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" Width="30%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnAddDemurrage" runat="server" OnClick="lbtnAddDemurrage_Click">
                                                            <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                                                    </asp:LinkButton>

                                                </ItemTemplate>
                                                <%-- <ItemTemplate>
                                                <asp:LinkButton ID="lbtngrdview" CausesValidation="false" runat="server" OnClick="lbtngrdview_Click">
                                                                <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>--%>

                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <div class="dropdown">
                                                        <a id="dLabel" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
                                                            <span class="glyphicon glyphicon-certificate"></span>
                                                        </a>
                                                        <div class="dropdown-menu menupopup" aria-labelledby="dLabel">
                                                            <div class="panel panel-info">

                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <asp:GridView ID="grBLDemurage" CssClass="table table-hover table-condensed" runat="server" GridLines="None"
                                                                            EmptyDataText="No Demurrage reason.." AutoGenerateColumns="False" ShowHeader="false">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="Reson"
                                                                                    SortExpression="Type" />
                                                                                 <asp:BoundField DataField="idi_rmk"
                                                                                    SortExpression="Typea" />

                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ShowHeader="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtngrdInvoiceDetailsEdit" CausesValidation="false" runat="server" OnClick="lbtngrdInvoiceDetailsEdit_Click">
                                                                <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:LinkButton ID="lbtngrdInvoiceDetailsUpdate" runat="server" CausesValidation="false" CommandName="Update" OnClientClick="SaveConfirm()" OnClick="lbtngrdview_Click">
                                                                <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="lbtngrdInvoiceDetailsCancel" runat="server" CausesValidation="false" CommandName="Cancel" OnClick="lbtngrdInvoiceDetailsCancel_Click">
                                                                <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>
                                                    </asp:LinkButton>

                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="15%" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div id="detailedData" style="display: none;">
                                <dl>
                                    <dt>Name: </dt>
                                    <dd id="dd_name"></dd>
                                </dl>
                            </div>
                            <div class="col-sm-3">
                                <div class="row">

                                    <div class="col-sm-12 paddingRight0 paddingtopbottom0">
                                        <asp:GridView ID="DemurrageDetails" CssClass="table table-hover table-responsive" runat="server" GridLines="None"
                                            EmptyDataText="No data found..." AutoGenerateColumns="False" ShowHeaderWhenEmpty="True">
                                            <Columns>
                                                <asp:BoundField DataField="mtp_desc" HeaderText="Demurrage reason"
                                                    SortExpression="Type" />
                                                <asp:BoundField DataField="Percentage" HeaderText="Percentage"
                                                    SortExpression="Percentage" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="row">
                                   
                                  
                                      
                                        <div class="col-sm-12">
                                                <asp:Image ID="Image1" runat="server" Width="300"   ImageUrl="~/images/banners/img_not_available.png" />
                                            <asp:Chart ID="Chart1" Visible="false" runat="server" Height="200" Width="300" OnClick="Chart1_Click">
                                                <Series>
                                                    <asp:Series Name="Default" Label="#VALX: #VALY{N0}"></asp:Series>
                                                </Series>
                                                <ChartAreas>
                                                    <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                                </ChartAreas>
                                            </asp:Chart>

                                        </div>
                                   
                                       
                                </div>

                                <div class="row">

                                    <div class="col-sm-8">
                                        <asp:Label ID="lblEmtymsg" Visible="false" CssClass="fontsize18" runat="server" Text="Non – Allocated reason"></asp:Label>
                                      
                                        <div class="col-sm-12">
                                            <asp:LinkButton runat="server" ID="lbtnFullchart" OnClick="lbtnFullchart_Click">
                                  <span class="glyphicon glyphicon-picture" aria-hidden="true"> </span>View full chart
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-10">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-sm-2">
                            <div class="col-sm-1">
                                <asp:LinkButton runat="server">
                                  <span class="glyphicon glyphicon-certificate" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </div>
                            <div class="col-sm-10">
                                View Chart
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="col-sm-1">
                                <asp:LinkButton runat="server">
                                  <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </div>
                            <div class="col-sm-10">
                                Add Demurrage Reason
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="col-sm-1">
                                <asp:LinkButton runat="server">
                                  <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </div>
                            <div class="col-sm-10">
                                Save Demurrage Reason
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="col-sm-1">
                                <asp:LinkButton runat="server">
                                  <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </div>
                            <div class="col-sm-10">
                                Close
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-12 height5">
                        </div>
                    </div>
                </div>
                <div class="panel-footer">
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel16" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button5" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="AddDemurrage" runat="server" Enabled="True" TargetControlID="Button5"
                PopupControlID="pnlPLimit" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlPLimit" DefaultButton="lbtnSearch">

                <div runat="server" id="Div1" class="panel panel-default height150 width525">

                    <asp:Label ID="Label4" runat="server" Text="Label" Visible="false"></asp:Label>
                    <%--<asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>--%>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:LinkButton ID="LinkButton4" runat="server">
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
                                <div class="col-sm-4 labelText1 ">
                                    Demurrage reason
                                </div>
                                <div class="col-sm-3 paddingLeft0 paddingRight0">
                                    <asp:DropDownList ID="ddlreson" runat="server" class="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 height5">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4 labelText1 ">
                                    Remark
                                </div>
                                <div class="col-sm-6 paddingLeft0 paddingRight0">
                                    <asp:TextBox runat="server" TextMode="MultiLine" ID="txtRemark" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 height30">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4 labelText1 ">
                                </div>
                                <div class="col-sm-2 paddingLeft0 paddingRight0">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CausesValidation="false" class="btn btn-primary" OnClick="btnSave_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="ViewChart" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="pnlChart" CancelControlID="lbtnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanelchat" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlChart">

                <div runat="server" id="Div2" class="panel panel-default height500 width850">

                    <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                    <%--<asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>--%>
                    <div class="panel panel-danger">
                        <div class="panel-heading">
                            <asp:LinkButton ID="lbtnClose" runat="server">
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
                                    <asp:Chart ID="Chart2" runat="server" Height="500" Width="800">
                                        <Series>
                                            <asp:Series Name="Default" Label="#VALX: #VALY:%"></asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>
                                </div>
                            </div>

                        </div>
                        <div class="panel-footer">
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserPopoup" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="pnlpopup" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpopup" DefaultButton="lbtnSearch">
        <div runat="server" id="test" class="panel panel-default height400 width700">
            <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
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
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy4" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
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
</asp:Content>
