<%@ Page Title="Account reshedule" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AccountReshedule.aspx.cs" Inherits="FF.WebERPClient.HP_Module.AccountReshedule" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function clickButton(e, buttonid) {
            var evt = e ? e : window.event;
            var bt = document.getElementById(buttonid);
            if (bt) {
                if (evt.keyCode == 113) {
                    bt.click();
                    return false;
                }

            }
        }

        function isNumberKeyAndDot(event, value) {
            var charCode = (event.which) ? event.which : event.keyCode
            var intcount = 0;
            var stramount = value;
            for (var i = 0; i < stramount.length; i++) {
                if (stramount.charAt(i) == '.' && charCode == 46) {
                    return false;
                }
            }
            if ((charCode > 31 && (charCode < 48 || charCode > 57)) && charCode != 46)
                return false;
            return true;
        }


        function uppercase() {
            key = window.event.keyCode;
            if ((key > 0x60) && (key < 0x7B))
                window.event.keyCode = key - 0x20;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updtMainPnl" runat="server">
        <ContentTemplate>
            <div style="float: left; width: 100%;">
                <div style="float: left; width: 100%; height: 22px; text-align: right;">
                    <%-- <asp:Button ID="btnCreate" runat="server" Text="Create" Height="100%" Width="70px"
                        CssClass="Button" OnClick="btnCreate_Click1" />--%>
                    <asp:Button ID="btnClear" runat="server" Text="Clear" Height="100%" Width="70px"
                        CssClass="Button" OnClick="btnClear_Click" />
                    <asp:Button ID="btnClose" runat="server" Text="Close" Height="100%" Width="70px"
                        CssClass="Button" OnClick="btnClose_Click" />
                </div>
                <%--item selecting area--%>
                <div style="float: left; width: 100%; padding-top: 2px; padding-bottom: 2px;">
                    <%-- Collaps Header - Items --%>
                    <div style="height: 18px; background-color: #1E4A9F; color: White; width: 98%; float: left;
                        margin-top: 6px;">
                        :: Request / Approval ::</div>
                    <%-- Collaps Image - Items --%>
                    <div style="float: left; margin-top: 6px;">
                        <asp:Image runat="server" ID="Image1" ImageUrl="~/Images/16 X 16 DownArrow.jpg" ImageAlign="Right" /></div>
                    <%-- Collaps control - Items --%>
                    <asp:CollapsiblePanelExtender ID="CPEHPItem" runat="server" TargetControlID="pnlRequest"
                        CollapsedSize="0" ExpandedSize="250" Collapsed="false" ExpandControlID="Image1"
                        CollapseControlID="Image1" AutoCollapse="False" AutoExpand="False" ScrollContents="false"
                        ExpandDirection="Vertical" ImageControlID="Image1" ExpandedImage="~/Images/16 X 16 UpArrow.jpg"
                        CollapsedImage="~/Images/16 X 16 DownArrow.jpg">
                    </asp:CollapsiblePanelExtender>
                    <%-- Collaps Area - HP Items --%>
                    <div style="width: 99%; float: left; padding-top: 2px; padding-bottom: 2px;">
                        <asp:Panel runat="server" ID="pnlRequest" Width="99%" BorderColor="#9F9F9F" BorderWidth="1px"
                            Font-Bold="true">
                            <div style="float: left; width: 50%; font-weight: normal; padding-top: 1px; padding-bottom: 1px;">
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 25%; color: #000000;">
                                        Account #</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 65%; text-align: left;">
                                        <asp:TextBox ID="txtAccSeq" runat="server" Width="20%" Style="margin-left: 1%" CssClass="TextBox"></asp:TextBox>
                                        <asp:TextBox ID="txtAccNo" runat="server" Width="60%" Style="margin-left: 1%" CssClass="TextBox"></asp:TextBox></div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 25%; color: #000000;">
                                        Account Date</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 65%; text-align: left;">
                                        <asp:Label ID="lblAccDate" runat="server" ForeColor="Black"></asp:Label></div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 25%; color: #000000;">
                                        Current Scheme</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 65%; text-align: left;">
                                        <asp:Label ID="lblCurShe" runat="server" ForeColor="Black"></asp:Label></div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 25%; color: #000000;">
                                        Change Scheme</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 30%; text-align: left;">
                                        <asp:DropDownList ID="ddlScheme" runat="server" CssClass="ComboBox" Width="100px"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 25%; color: #000000;">
                                        Remarks</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 70%; text-align: left;">
                                        <asp:TextBox ID="txtRemarks" runat="server" Width="95%" Style="margin-left: 1%" CssClass="TextBox"></asp:TextBox></div>
                                </div>
                                  <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 25%; color: #000000;">
                                        Request #</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 70%; text-align: left;">
                                        <asp:TextBox ID="txtReq" runat="server" Width="50%" Style="margin-left: 1%" CssClass="TextBox"></asp:TextBox></div>
                                </div>
                                   <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 25%; color: #000000;">
                                        Status</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 70%; text-align: left;">
                                         <asp:Label ID="lblStatus" runat="server" ForeColor="Black"></asp:Label></div>
                                </div>
                                <div style="float: right; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                    <asp:Button ID="btnReq" runat="server" Text="Request" Width="70px" CssClass="Button"
                                        OnClick="btnReq_Click" />
                                    <asp:Button ID="btnCan" runat="server" Text="Cancel" Width="70px" CssClass="Button" />
                                </div>
                            </div>
                            <div style="float: right; width: 49%; font-weight: normal; padding-top: 1px; padding-bottom: 1px;">
                                <asp:Panel ID="pnlDetails" runat="server" ScrollBars="Auto" BorderColor="#9F9F9F"
                                    BorderWidth="0px" Font-Bold="true" Width="100%">
                                    <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 15%; color: #000000;">
                                            From Date</div>
                                        <div style="float: left; width: 1%; color: #000000;">
                                            :</div>
                                        <div style="float: left; width: 30%; text-align: left;">
                                            <asp:TextBox ID="txtfDate" runat="server" CssClass="TextBox" Width="80%" Enabled="false"></asp:TextBox>
                                            <asp:Image ID="imgfDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfDate"
                                                Format="dd/MMM/yyyy" PopupButtonID="imgfDate">
                                            </asp:CalendarExtender>
                                        </div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 15%; color: #000000;">
                                            To Date</div>
                                        <div style="float: left; width: 1%; color: #000000;">
                                            :</div>
                                        <div style="float: left; width: 30%; text-align: left;">
                                            <asp:TextBox ID="txttDate" runat="server" CssClass="TextBox" Width="80%" Enabled="false"></asp:TextBox>
                                            <asp:Image ID="imgtDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txttDate"
                                                Format="dd/MMM/yyyy" PopupButtonID="imgtDate">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                    <div style="float: right; width: 100%; padding-bottom: 2px;">
                                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="70px" CssClass="Button"
                                            OnClick="btnRefresh_Click" />
                                    </div>
                                    <asp:GridView ID="gvPendingApp" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                                        Style="margin-top: 0px" GridLines="None" RowStyle-Height="10px" Width="100%"
                                        CellPadding="4" ForeColor="#333333" EmptyDataText="No data found" ShowHeaderWhenEmpty="True"
                                        CssClass="GridView" OnRowCommand ="gvPendingApp_Rowcommand" >
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Size="11px" Height="11px"
                                            Font-Bold="True" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle Font-Size="11px" BackColor="#EFF3FB" />
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField='grah_com' HeaderText='' Visible="false" />
                                            <asp:BoundField DataField='grah_ref' HeaderText='Request #' ItemStyle-Width="10%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField='grah_loc' HeaderText='Profit center' ItemStyle-Width="8%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="8%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField='grah_app_tp' HeaderText='' Visible="false" />
                                            <asp:BoundField DataField='grah_fuc_cd' HeaderText='Account #' ItemStyle-Width="6%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="6%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField='grah_oth_loc' HeaderText='' Visible="false" />
                                            <asp:BoundField DataField='grah_cre_by' HeaderText='' Visible="false" />
                                            <asp:BoundField DataField='grah_cre_dt' HeaderText='Request date' ItemStyle-Width="14%"
                                                HeaderStyle-HorizontalAlign="Left" DataFormatString="{0:d}">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="14%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField='grah_mod_by' HeaderText='' Visible="false" />
                                            <asp:BoundField DataField='grah_mod_dt' HeaderText='' Visible="false" />
                                            <asp:BoundField DataField='grah_app_stus' HeaderText='Status' ItemStyle-Width="6%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="6%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField='grah_app_lvl' HeaderText='' Visible="false" />
                                            <asp:BoundField DataField='grah_app_by' HeaderText='' Visible="false" />
                                            <asp:BoundField DataField='grah_app_dt' HeaderText='' Visible="false" />
                                            <asp:BoundField DataField='grah_remaks' HeaderText='Remarks' ItemStyle-Width="6%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="10%" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnView" runat="server" Text="V" CssClass="Button" CommandName="VIEW" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="8%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnGet" runat="server" Text="G" CssClass="Button" CommandName="GET" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="8%" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    </asp:GridView>
                                </asp:Panel>
                                <div style="float: right; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                    <asp:Button ID="btnApp" runat="server" Text="Approve" Width="70px" 
                                        CssClass="Button" onclick="btnApp_Click" />
                                    <asp:Button ID="btnRej" runat="server" Text="Reject" Width="70px" 
                                        CssClass="Button" onclick="btnRej_Click" />
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <div style="float: left; width: 100%; padding-top: 2px; padding-bottom: 2px; background-color: #EEEEEE;">
                    <asp:Panel ID="Panel_popUp" runat="server" Width="500px" BackColor="#D8D8D8" BorderColor="Black"
                        BorderWidth="1px">
                        <div style="text-align: right">
                            <asp:Button ID="btnPopupCancel" runat="server" Text="close" CssClass="Button" /></div>
                        <asp:Panel ID="PanelPopup_grv" runat="server" ScrollBars="Vertical" Height="80px"
                            Width="460px">
                            <asp:GridView ID="grvMpdalPopUp" runat="server" AutoGenerateColumns="False" DataKeyNames="HPA_ACC_NO,HPA_ACC_CRE_DT"
                                OnSelectedIndexChanged="grvMpdalPopUp_SelectedIndexChanged" Style="text-align: left">
                                <Columns>
                                    <asp:CommandField SelectText="select" ShowSelectButton="True" />
                                    <asp:BoundField DataField="HPA_ACC_NO" HeaderText="Account No." />
                                    <asp:BoundField DataField="HPA_ACC_CRE_DT" HeaderText="Created Date" DataFormatString="{0:d}" />
                                    <asp:BoundField HeaderText="Customer Name" />
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </asp:Panel>
                </div>
                <asp:ModalPopupExtender ID="ModalPopupExtItem" runat="server" CancelControlID="btnPopupCancel"
                    ClientIDMode="Static" PopupControlID="Panel_popUp" TargetControlID="btnHidden_popup">
                </asp:ModalPopupExtender>
                <div style="display: none">
                    <asp:Button ID="btnHidden_popup" runat="server" Text="Button" />
                    <asp:Button ID="btnHiddenView" runat="server" Text="Button" />
                    <asp:Button ID="btn_validateAcc" runat="server" CssClass="Button" OnClick="btn_validateAcc_Click" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
