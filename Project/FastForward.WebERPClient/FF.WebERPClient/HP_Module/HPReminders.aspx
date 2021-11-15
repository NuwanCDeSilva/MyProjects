<%@ Page Title="Account Reminders" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="HPReminders.aspx.cs" Inherits="FF.WebERPClient.HP_Module.HPReminders" %>

<%@ Register Src="../UserControls/uc_HpAccountSummary.ascx" TagName="uc_HpAccountSummary"
    TagPrefix="AC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function LinkClick() {
            document.getElementById('<%= LinkButtonTemp.ClientID %>').click();
        }

        function checkTextAreaMaxLength() {
            var mLen = document.getElementById('<%=TextBoxMessage.ClientID %>').value;
            if (mLen.length > 200) {
                document.getElementById('<%=TextBoxMessage.ClientID %>').focus(); //set focus to prevent jumping         o
                document.getElementById('<%=TextBoxMessage.ClientID %>').value = document.getElementById('<%=TextBoxMessage.ClientID %>').value.substring(0, 50); //truncate the value         
                document.getElementById('<%=TextBoxMessage.ClientID %>').scrollTop = document.getElementById('<%=TextBoxMessage.ClientID %>').scrollHeight; //scroll to the end to prevent jumping         
                return false;
            }

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

        function ViewOpen() {
            window.open('../HP_Module/HPViewReminders.aspx?id=' + document.getElementById('<%= TextBoxAccNo.ClientID %>').value, '_blank');
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>
            <div style="height: 22px; text-align: right;" class="PanelHeader">
                <asp:Button ID="ButtonAdd" runat="server" Text="Add" CssClass="Button" OnClick="ButtonAdd_Click" />
                <asp:Button ID="ButtonView" runat="server" Text="View" CssClass="Button" OnClientClick="ViewOpen()" />
                <asp:Button ID="ButtonClear" runat="server" Text="Clear" CssClass="Button" OnClick="ButtonClear_Click" />
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
                                <asp:TextBox ID="TextBoxAccNo" runat="server" CssClass="TextBoxUpper" onblur="LinkClick()"
                                    onKeyPress="return numbersonly(event,false)"></asp:TextBox>
                                <asp:ImageButton ID="ImgBtnAcc" runat="server" ImageUrl="~/Images/icon_search.png"
                                    OnClick="ImgBtnAcc_Click" />
                                <asp:LinkButton ID="LinkButtonTemp" runat="server" OnClick="LinkButtonTemp_Click"></asp:LinkButton>
                            </div>
                        </div>
                        <div style="float: left; width: 1%; color: Black;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 49%; color: Black;">
                            <div style="float: left; width: 30%; color: Black;">
                                Date
                            </div>
                            <div style="float: left; width: 70%; color: Black;">
                                <asp:TextBox ID="TextBoxDate" runat="server" CssClass="TextBoxUpper" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <div style="float: left; height: 10%; color: Black;">
                &nbsp;
            </div>
            <div style="float: left; width: 100%; color: Black; margin-left: 10%; margin-right: 10%;">
                <div style="width: 80%; float: left;">
                    <div style="width: 49%; float: left;">
                        <div style="height: 10px; width: 100%;" class="PanelHeader">
                            Account Summary
                        </div>
                        <div style="float: left; width: 100%;">
                            <asp:Panel ID="Panel2" runat="server" GroupingText=" ">
                                <AC:uc_HpAccountSummary ID="uc_HpAccountSummary1" runat="server" />
                            </asp:Panel>
                        </div>
                    </div>
                    <div style="float: left; width: 1%; color: Black;">
                        &nbsp;
                    </div>
                    <div style="width: 49%; float: left;">
                        <div style="height: 10px; width: 100%;" class="PanelHeader">
                            Reminders
                        </div>
                        <div style="float: left; width: 100%;">
                            <asp:Panel ID="Panel5" runat="server" GroupingText=" ">
                                <div style="float: left; width: 100%; color: Black;">
                                    <asp:GridView ID="GridViewDetails" runat="server" Width="100%" ShowHeaderWhenEmpty="True"
                                        AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="Both"
                                        CssClass="GridView" OnRowDataBound="GridViewDetails_RowDataBound" DataKeyNames="hra_seq">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
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
                    </div>
                </div>
            </div>
            <div style="float: left; height: 10%; color: Black;">
                &nbsp;
            </div>
            <div style="float: left; width: 100%; color: Black; margin-left: 10%; margin-right: 10%;">
                <asp:Panel ID="Panel3" runat="server" GroupingText=" " Width="80%">
                    <div style="float: left; width: 100%; color: Black;">
                        <div style="float: left; width: 1%; color: Black;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 49%; color: Black;">
                            <div style="float: left; width: 30%; color: Black;">
                                Date
                            </div>
                            <div style="float: left; width: 70%; color: Black;">
                                <asp:TextBox ID="TextBoxRemindDate" runat="server" CssClass="TextBoxUpper" Enabled="false"></asp:TextBox>
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBoxRemindDate"
                                    PopupButtonID="Image1" Enabled="True" Format="dd/MMM/yyyy">
                                </asp:CalendarExtender>
                            </div>
                        </div>
                        <div style="float: left; width: 1%; color: Black;">
                            &nbsp;
                        </div>
                    </div>
                    <div style="float: left; height: 5px; color: Black;">
                        &nbsp;
                    </div>
                    <div style="float: left; width: 100%; color: Black;">
                        <div style="float: left; width: 1%; color: Black;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 49%; color: Black;">
                            <div style="float: left; width: 30%; color: Black;">
                                Customer Mobile
                            </div>
                            <div style="float: left; width: 70%; color: Black;">
                                <asp:TextBox ID="TextBoxCustMob" runat="server" CssClass="TextBoxUpper" onKeyPress="return numbersonly(event,false)"
                                    MaxLength="10"></asp:TextBox>
                            </div>
                        </div>
                        <div style="float: left; width: 1%; color: Black;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 49%; color: Black; text-align: right;">
                            <div style="float: left; width: 30%; color: Black;">
                                Manager Mobile
                            </div>
                            <div style="float: left; width: 70%; color: Black;">
                                <asp:TextBox ID="TextBoxManagerMob" runat="server" CssClass="TextBoxUpper" onKeyPress="return numbersonly(event,false)"
                                    MaxLength="10"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <div style="float: left; height: 10%; color: Black;">
                &nbsp;
            </div>
            <div style="float: left; width: 100%; color: Black; margin-left: 10%; margin-right: 10%;">
                <asp:Panel ID="Panel4" runat="server" GroupingText=" " Width="80%">
                    <div style="float: left; width: 100%; color: Black;">
                        <asp:TextBox ID="TextBoxMessage" runat="server" CssClass="TextBox" TextMode="MultiLine"
                            Width="99%" Height="120px" onKeyUp="checkTextAreaMaxLength();"></asp:TextBox>
                    </div>
                </asp:Panel>
            </div>
            <asp:ModalPopupExtender ID="ModalPopupExtItem" runat="server" CancelControlID="btnPopupCancel"
                ClientIDMode="Static" PopupControlID="Panel_popUp" TargetControlID="btnHidden_popup"
                BackgroundCssClass="modalBackground" PopupDragHandleControlID="divpopHeader">
            </asp:ModalPopupExtender>
            <div class="invunkwn5">
                <asp:Panel ID="Panel_popUp" runat="server" Width="500px">
                    <%-- PopUp Handler for drag and control --%>
                    <div class="popUpHeader" id="divpopHeader" runat="server">
                        <div class="div80pcelt" runat="server" id="divPopCaption">
                            Select Account
                        </div>
                        <div class="invunkwn57">
                            <asp:ImageButton ID="btnPopupCancel" runat="server" ImageUrl="~/Images/0032-01-16x16x16.ICO" />&nbsp;</div>
                    </div>
                    <asp:Panel ID="PanelPopup_grv" runat="server" ScrollBars="Both">
                        <asp:GridView ID="grvMpdalPopUp" runat="server" AutoGenerateColumns="False" DataKeyNames="HPA_ACC_NO,HPA_ACC_CRE_DT"
                            OnSelectedIndexChanged="grvMpdalPopUp_SelectedIndexChanged">
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
