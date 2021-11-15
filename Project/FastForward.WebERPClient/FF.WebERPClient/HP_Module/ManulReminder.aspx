<%@ Page Title="Reminder Letter" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ManulReminder.aspx.cs" Inherits="FF.WebERPClient.HP_Module.ManulReminder" %>

<%@ Register Src="../UserControls/uc_HpAccountSummary.ascx" TagName="uc_HpAccountSummary"
    TagPrefix="AC" %>
<%@ Register Src="../UserControls/uc_HpAccountDetail.ascx" TagName="uc_HpAccountDetail"
    TagPrefix="AD" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function LinkClick() {
            document.getElementById('<%= LinkButtonTemp.ClientID %>').click();
        }

        function ShowConfirm() {
            if (confirm("Are you sure?"))
                document.getElementById('<%= LinkButtonTemp1.ClientID %>').click();
        }

        function LinkClick(sender, args) {
            document.getElementById('<%= LinkButtonTemp.ClientID %>').click()
        }

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
            <asp:LinkButton ID="LinkButtonTemp1" runat="server" OnClick="LinkButtonTemp1_Click"></asp:LinkButton>
            <div style="height: 22px; text-align: right;" class="PanelHeader">
                <asp:Button ID="ButtonSave" runat="server" Text="Print" CssClass="Button" OnClick="ButtonSave_Click" />
                <asp:Button ID="ButtonClear" runat="server" Text="Clear" CssClass="Button" OnClick="ButtonClear_Click" />
                <asp:Button ID="ButtonClose" runat="server" Text="Close" CssClass="Button" OnClick="ButtonClose_Click" />
            </div>
            <div style="float: left; height: 10%; color: Black;">
                &nbsp;
            </div>
            <div style="float: left; width: 100%; color: Black;">
                <div style="float: left; width: 50%; color: Black;">
                    <asp:Panel ID="Panel1" runat="server" GroupingText=" ">
                        <div style="float: left; width: 100%; color: Black;">
                            <div style="float: left; width: 1%; color: Black;">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 30%; color: Black;">
                                Account Number
                            </div>
                            <div style="float: left; width: 69%; color: Black;">
                                <asp:TextBox ID="TextBoxAccNo" runat="server" CssClass="TextBoxUpper" onblur="LinkClick()"
                                    onKeyPress="return numbersonly(event,false)"></asp:TextBox>
                                <asp:ImageButton ID="ImgBtnAcc" runat="server" ImageUrl="~/Images/icon_search.png"
                                    OnClick="ImgBtnAcc_Click" />
                                <asp:LinkButton ID="LinkButtonTemp" runat="server" OnClick="LinkButtonTemp_Click"></asp:LinkButton>
                            </div>
                        </div>
                        <div style="float: left; width: 100%; color: Black; padding-top: 3px;">
                            <div style="float: left; width: 1%; color: Black;">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 30%; color: Black;">
                                Reminder Type
                            </div>
                            <div style="float: left; width: 69%; color: Black;">
                                <asp:DropDownList ID="DropDownListRemindeType" runat="server" CssClass="ComboBox">
                                    <asp:ListItem Value="A">Arrears</asp:ListItem>
                                    <asp:ListItem Value="AC">Account Close</asp:ListItem>
                                    <asp:ListItem Value="R">Reminder</asp:ListItem>
                                    <asp:ListItem Value="FR">Final Reminder</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div style="float: left; width: 100%; color: Black; padding-top: 3px; display: none">
                            <div style="float: left; width: 1%; color: Black;">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 30%; color: Black;">
                                Medium
                            </div>
                            <div style="float: left; width: 69%; color: Black;">
                                <asp:DropDownList ID="DropDownListMedium" runat="server" CssClass="ComboBox">
                                    <asp:ListItem Value="S">Sinhala</asp:ListItem>
                                    <asp:ListItem Value="E">English</asp:ListItem>
                                    <asp:ListItem Value="T">Tamil</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div style="float: left; width: 100%; color: Black; padding-top: 3px;">
                            <div style="float: left; width: 1%; color: Black;">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 30%; color: Black;">
                                Reminder Date
                            </div>
                            <div style="float: left; width: 69%; color: Black;">
                                <asp:TextBox ID="TextBoxReminderDate" runat="server" CssClass="TextBox TextBoxUpper"
                                    Enabled="False" Width="108px"></asp:TextBox>
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBoxReminderDate"
                                    PopupButtonID="Image1" Enabled="True" Format="dd/MMM/yyyy">
                                </asp:CalendarExtender>
                            </div>
                        </div>
                        <div style="float: left; width: 100%; color: Black; padding-top: 3px;">
                            <div style="float: left; width: 1%; color: Black;">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 30%; color: Black;">
                                As At Date
                            </div>
                            <div style="float: left; width: 69%; color: Black;">
                                <asp:TextBox ID="TextBoxDueDate" runat="server" CssClass="TextBox TextBoxUpper" Enabled="False"
                                    Width="108px"></asp:TextBox>
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TextBoxDueDate"
                                    PopupButtonID="Image2" Enabled="True" Format="dd/MMM/yyyy" OnClientDateSelectionChanged="LinkClick">
                                </asp:CalendarExtender>
                            </div>
                        </div>
                        <div style="float: left; width: 100%; color: Black; padding-top: 3px;">
                            <div style="float: left; width: 1%; color: Black;">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 49%; color: Black;">
                                <div style="float: left; width: 30%; color: Black;">
                                    Arrears
                                </div>
                                <div style="float: left; width: 69%; color: Black;">
                                    <asp:TextBox ID="TextBoxArrears" runat="server" CssClass="TextBoxNumeric" onKeyPress="return numbersonly(event,true)"></asp:TextBox>
                                </div>
                            </div>
                            <div style="float: left; width: 1%; color: Black;">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 49%; color: Black;">
                                <div style="float: left; width: 30%; color: Black;">
                                    Account Balance
                                </div>
                                <div style="float: left; width: 69%; color: Black;">
                                    <asp:TextBox ID="TextBoxAccBal" runat="server" CssClass="TextBoxNumeric"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div style="float: left; width: 100%; color: Black; padding-top: 3px;">
                            <div style="float: left; width: 1%; color: Black;">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 30%; color: Black;">
                                Remarks
                            </div>
                            <div style="float: left; width: 69%; color: Black;">
                                <asp:TextBox ID="TextBoxRmk" runat="server" CssClass="TextBox" TextMode="MultiLine"
                                    Width="100%"></asp:TextBox>
                            </div>
                        </div>
                    </asp:Panel>
                    <div style="float: left; width: 100%; height: 10px; color: Black;">
                        &nbsp;
                    </div>
                    <div style="float: left; width: 100%;">
                        <asp:Panel ID="Panel4" runat="server" GroupingText="Item Details">
                            <asp:GridView ID="GridViewItem" runat="server" Width="100%" ShowHeaderWhenEmpty="True"
                                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="Both"
                                CssClass="GridView">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="Sad_itm_cd" HeaderText="Item Code" />
                                    <asp:BoundField DataField="Sad_alt_itm_desc" HeaderText="Description" />
                                    <asp:BoundField DataField="Sad_qty" HeaderText="Qty" />
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
                        </asp:Panel>
                        <div style="float: left; width: 100%; height: 10px; color: Black;">
                            &nbsp;
                        </div>
                        <asp:Panel ID="Panel5" runat="server" GroupingText="Guarantors Details">
                            <asp:GridView ID="GridViewGurantor" runat="server" Width="100%" ShowHeaderWhenEmpty="True"
                                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="Both"
                                CssClass="GridView">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="MBE_NAME" HeaderText="Name" />
                                    <asp:BoundField DataField="MBE_NIC" HeaderText="NIC" />
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
                        </asp:Panel>
                    </div>
                </div>
                <div style="float: left; width: 50%; color: Black;">
                    <div style="height: 10px;" class="PanelHeader">
                        Account Summary
                    </div>
                    <div style="float: left; width: 100%;">
                        <asp:Panel ID="Panel2" runat="server" GroupingText=" ">
                            <AC:uc_HpAccountSummary ID="uc_HpAccountSummary1" runat="server" />
                        </asp:Panel>
                    </div>
                    <div style="float: left; width: 100%; height: 10px; color: Black;">
                        &nbsp;
                    </div>
                    <div style="height: 10px;" class="PanelHeader">
                        Account Details
                    </div>
                    <div style="float: left; width: 100%; padding-top: 3px;">
                        <asp:Panel ID="Panel3" runat="server" GroupingText=" ">
                            <AD:uc_HpAccountDetail ID="uc_HpAccountDetail1" runat="server" />
                        </asp:Panel>
                    </div>
                </div>
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
