<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="HPViewReminders.aspx.cs" Inherits="FF.WebERPClient.HP_Module.HPViewReminders" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function numbersonly(e, decimal) {
            var key;
            var keychar;

            if (window.event) {
                key = window.event.keyCode;
            }
            else if (e) {
                key = e.which;
            }
            else {
                return true;
            }
            keychar = String.fromCharCode(key);

            if ((key == null) || (key == 0) || (key == 8) || (key == 9) || (key == 13) || (key == 27)) {
                return true;
            }
            else if ((("0123456789").indexOf(keychar) > -1)) {
                return true;
            }
            else if (decimal && (keychar == ".")) {
                return true;
            }
            else
                return false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>
            <div style="height: 22px; text-align: right;" class="PanelHeader">
                <asp:Button ID="ButtonView" runat="server" Text="View" CssClass="Button" OnClick="ButtonInactive_Click" />
                <asp:Button ID="ButtonInactive" runat="server" Text="Inactive" CssClass="Button"
                    OnClick="ButtonInactive_Click1" />
                <asp:Button ID="ButtonClose" runat="server" Text="Close" CssClass="Button" OnClick="ButtonClose_Click" />
            </div>
            <div style="float: left; height: 10%; color: Black;">
                &nbsp;
            </div>
            <div style="float: left; width: 100%; color: Black; margin-left: 10%; margin-right: 10%;">
                <asp:Panel ID="Panel1" runat="server" GroupingText=" " Width="80%">
                    <div style="float: left; width: 100%; color: Black;">
                        <div style="float: left; width: 1%; color: Black;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 49%; color: Black;">
                            <div style="float: left; width: 30%; color: Black;">
                                Account Number
                            </div>
                            <div style="float: left; width: 70%; color: Black;">
                                <asp:TextBox ID="TextBoxAccNo" runat="server" CssClass="TextBoxUpper" onKeyPress="return numbersonly(event,false)"></asp:TextBox>
                                <asp:ImageButton ID="ImgBtnAcc" runat="server" ImageUrl="~/Images/icon_search.png"
                                    OnClick="ImgBtnAcc_Click" />
                            </div>
                        </div>
                        <div style="float: left; width: 1%; color: Black;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 49%; color: Black;">
                            <div style="float: left; width: 30%; color: Black;">
                                Status
                            </div>
                            <div style="float: left; width: 70%; color: Black;">
                                <asp:DropDownList ID="DropDownListReminderStatus" runat="server" CssClass="ComboBox">
                                    <asp:ListItem Value="A">Active</asp:ListItem>
                                    <asp:ListItem Value="I">Inactive</asp:ListItem>
                                    <asp:ListItem Value="C">Close</asp:ListItem>
                                    <asp:ListItem Value="%">All</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <div style="float: left; height: 10%; color: Black;">
                &nbsp;
            </div>
            <div style="float: left; width: 100%; color: Black; margin-left: 10%; margin-right: 10%;">
                <asp:Panel ID="Panel2" runat="server" GroupingText=" " Width="80%">
                    <div style="float: left; width: 100%; color: Black;">
                        <asp:GridView ID="GridViewDetails" runat="server" Width="100%" ShowHeaderWhenEmpty="True"
                            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="Both"
                            CssClass="GridView" OnRowDataBound="GridViewDetails_RowDataBound" DataKeyNames="hra_seq">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chekPc" runat="server" Checked="true" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Hra_ref" HeaderText="Acc No" />
                                <asp:BoundField DataField="Hra_dt" HeaderText="Remind Date" DataFormatString='<%$ appSettings:FormatToDate %>'
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Hra_rmd" HeaderText="Reminder" />
                                <asp:BoundField DataField="Hra_stus" HeaderText="Status" />
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                    </div>
                </asp:Panel>
            </div>

                            <asp:ModalPopupExtender ID="ModalPopupExtItem" runat="server" CancelControlID="btnPopupCancel" ClientIDMode="Static"  PopupControlID="Panel_popUp" TargetControlID="btnHidden_popup"  BackgroundCssClass="modalBackground" PopupDragHandleControlID="divpopHeader"> </asp:ModalPopupExtender>
    <div class="invunkwn5">
    <asp:Panel ID="Panel_popUp" runat="server" Width="500px" >
        <%-- PopUp Handler for drag and control --%>
        <div class="popUpHeader" id="divpopHeader" runat="server">
        <div class="div80pcelt" runat="server" id="divPopCaption"> Select Account </div>
        <div class="invunkwn57"> <asp:ImageButton ID="btnPopupCancel" runat="server" ImageUrl="~/Images/0032-01-16x16x16.ICO" />&nbsp;</div> 
        </div>
        <asp:Panel ID="PanelPopup_grv" runat="server" ScrollBars="Both" >
            <asp:GridView ID="grvMpdalPopUp" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="HPA_ACC_NO,HPA_ACC_CRE_DT" 
            onselectedindexchanged="grvMpdalPopUp_SelectedIndexChanged">
            <Columns>
                <asp:CommandField SelectText="select" ShowSelectButton="True" />
                <asp:BoundField DataField="HPA_ACC_NO" HeaderText="Account No." />
                <asp:BoundField DataField="HPA_ACC_CRE_DT" HeaderText="Created Date" />
            </Columns>
            </asp:GridView>
        </asp:Panel>
    </asp:Panel>
    </div>
    <div style="display: none;">
    <asp:Button ID="btnHidden_popup" runat="server" Text="Button" />
    </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
