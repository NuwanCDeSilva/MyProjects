<%@ Page Title="Return Cheque" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ReturnCheque.aspx.cs" Inherits="FF.WebERPClient.Financial_Modules.ReturnCheque" %>

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

        function DisplayConfirmation() {
            var val = document.getElementById('<%=TextBoxReturnDate.ClientID  %>').value;
            var val1 = document.getElementById('<%=GridRowCount.ClientID  %>').value;
            if (val != "" && val != null && val1 != "0") {
                return confirm('Are you sure?');
            }
            else {
                alert('Please fill required information before save');
                return false;
            }

        }

        function DispalyConfirm() {
            var val2 = document.getElementById('<%=GridRowCount.ClientID  %>').value;
            var val1 = document.getElementById('<%=TextBoxNChequeNumber.ClientID %>').value;
            if (val1 != "" && val1 != null && val2 != "0") {
                return confirm('Are you sure?');
            }
            else {
                alert('Please fill required information befaor save');

                return false;
            }

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updtMainPnl" runat="server">
        <ContentTemplate>
            <div style="float: left; width: 100%; color: Black;">
                <asp:HiddenField ID="GridRowCount" runat="server" />
                <%--<asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                    <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Cheque Details">
                        <ContentTemplate>--%>
                <div style="height: 22px; text-align: right;" class="PanelHeader">
                    <asp:Button ID="ButtonSave" runat="server" Text="Save" CssClass="Button" OnClick="ButtonSave_Click"
                        OnClientClick="return DisplayConfirmation()" />
                    <asp:Button ID="ButtonClear" runat="server" Text="Clear" CssClass="Button" OnClick="ButtonClear_Click" />
                    <asp:Button ID="ButtonClose" runat="server" Text="Close" CssClass="Button" OnClick="ButtonClose_Click" />
                </div>
                <div style="float: left; width: 100%; color: Black;">
                </div>
                <div style="float: left; width: 100%; color: Black; padding: 2px 0px 0px 0px;">
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 20%;">
                        Profit Center
                    </div>
                    <div style="float: left; width: 30%;">
                        <asp:TextBox ID="TextBoxLocation" CssClass="TextBoxUpper" runat="server" AutoPostBack="True"
                            OnTextChanged="TextBoxLocation_TextChanged"></asp:TextBox>
                        <asp:ImageButton ID="imgLocation" runat="server" ImageUrl="~/Images/icon_search.png"
                            OnClick="imgLocation_Click" />
                    </div>
                </div>
                <div style="float: left; width: 100%; color: Black; padding: 2px 0px 0px 0px;">
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 20%;">
                        Bank
                    </div>
                    <div style="float: left; width: 20%;">
                        <asp:DropDownList ID="DropDownListBank" CssClass="ComboBox" AppendDataBoundItems="True"
                            runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListBank_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div style="float: left; width: 10%;">
                        &nbsp;</div>
                    <div style="float: left; width: 40%;" runat="server" id="DivMess1">
                    </div>
                </div>
                <div style="float: left; width: 100%; color: Black; padding: 2px 0px 0px 0px;">
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 20%;">
                        Cheque Number
                    </div>
                    <div style="float: left; width: 20%;">
                        <asp:TextBox ID="TextBoxChequeNumber" CssClass="TextBox" MaxLength="15" runat="server"
                            OnTextChanged="TextBoxChequeNumber_TextChanged" AutoPostBack="True"></asp:TextBox>
                        <asp:ImageButton ID="imgSelect" runat="server" CommandName="SELECT" ImageUrl="~/Images/download_arrow_icon.png"
                            Height="15px" OnClick="imgSelect_Click" />
                    </div>
                    <div style="float: left; width: 10%;">
                        &nbsp;</div>
                    <div style="float: left; width: 40%;" runat="server" id="DivMess2">
                    </div>
                    <div style="float: left; width: 100%; color: Black; padding: 10px 0px 0px 0px;" runat="server"
                        id="DivNewCheque" visible="false">
                        <asp:Panel ID="Panel111" runat="server" GroupingText="Cheque Correction">
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 20%;">
                                New Cheque Number
                            </div>
                            <div style="float: left; width: 20%;">
                                <asp:TextBox ID="TextBoxNChequeNumber" CssClass="TextBox" MaxLength="15" runat="server"
                                    OnTextChanged="TextBoxChequeNumber_TextChanged"></asp:TextBox>
                            </div>
                            <div style="float: left; width: 10%;">
                                &nbsp;</div>
                            <div style="float: left; width: 20%;" runat="server" id="Div1">
                                Bank
                            </div>
                            <div style="float: left; width: 20%;" runat="server" id="Div2">
                                <asp:DropDownList ID="DropDownListNBank" CssClass="ComboBox" AppendDataBoundItems="True"
                                    runat="server">
                                </asp:DropDownList>
                            </div>
                            <div style="float: left; width: 100%; color: Black; padding: 2px 0px 0px 0px;">
                                <div style="float: left; width: 1%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 20%;">
                                    Branch
                                </div>
                                <div style="float: left; width: 20%;">
                                    <asp:TextBox ID="TextBoxBranch" CssClass="TextBox" MaxLength="20" runat="server"
                                        OnTextChanged="TextBoxChequeNumber_TextChanged" AutoPostBack="True"></asp:TextBox>
                                </div>
                                <div style="float: left; width: 10%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 20%;">
                                    Date
                                </div>
                                <div style="float: left; width: 20%;">
                                    <asp:TextBox ID="TextBoxChDate" CssClass="TextBox" Enabled="False" runat="server"></asp:TextBox>
                                    <asp:Image ID="imgReturnDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBoxChDate"
                                        PopupButtonID="imgReturnDate" Enabled="True" Format="dd/MMM/yyyy">
                                    </asp:CalendarExtender>
                                </div>
                            </div>
                            <div style="float: left; width: 100%; color: Black; padding: 2px 0px 0px 0px;">
                                <div style="float: left; width: 1%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 20%;">
                                </div>
                                <div style="float: left; width: 20%;">
                                </div>
                                <div style="float: left; width: 10%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 40%; text-align: right;">
                                    <asp:Button ID="ButtonEdit" runat="server" Text="Edit" CssClass="Button" OnClick="ButtonEdit_Click"
                                        OnClientClick="return DispalyConfirm();" />
                                    &nbsp; &nbsp;
                                    <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" CssClass="Button" OnClick="ButtonCancel_Click" />
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <div style="float: left; width: 100%; color: Black; padding: 2px 0px 0px 0px;">
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 20%;">
                            Return Date
                        </div>
                        <div style="float: left; width: 20%;">
                            <asp:TextBox ID="TextBoxReturnDate" CssClass="TextBox" Enabled="False" runat="server"></asp:TextBox>
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TextBoxReturnDate"
                                PopupButtonID="Image1" Enabled="True" Format="dd/MMM/yyyy">
                            </asp:CalendarExtender>
                        </div>
                        <div style="float: left; width: 10%;">
                            &nbsp;</div>
                        <div style="float: left; width: 20%;">
                        </div>
                        <div style="float: left; width: 20%;">
                        </div>
                    </div>
                    <div style="float: left; width: 100%; color: Black; padding: 2px 0px 0px 0px;">
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 20%;">
                            Return Bank
                        </div>
                        <div style="float: left; width: 20%;">
                            <asp:DropDownList ID="DropDownListReturnBank" CssClass="ComboBox" AppendDataBoundItems="True"
                                runat="server">
                            </asp:DropDownList>
                        </div>
                        <div style="float: left; width: 10%;">
                            &nbsp;</div>
                        <div style="float: left; width: 20%;">
                            Banked Type
                        </div>
                        <div style="float: left; width: 20%;">
                            <asp:DropDownList ID="DropDownListBankType" CssClass="ComboBox" runat="server">
                                <asp:ListItem Value="0">Direct</asp:ListItem>
                                <asp:ListItem Value="1">Cash</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div style="float: left; width: 100%; color: Black; padding: 2px 0px 0px 0px;" runat="server"
                        visible="false" id="AAA">
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 20%;">
                            Branch
                        </div>
                        <div style="float: left; width: 20%;">
                            <asp:Label ID="LabelBankBranch" runat="server"></asp:Label>
                        </div>
                        <div style="float: left; width: 10%;">
                            &nbsp;</div>
                        <div style="float: left; width: 20%;">
                            Cheque Date
                        </div>
                        <div style="float: left; width: 20%;">
                            <asp:Label ID="LabelChequeDate" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div style="float: left; width: 100%; color: Black;">
                    </div>
                    <div style="float: left; width: 100%; color: Black;">
                        <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Height="250px">
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <asp:GridView ID="GridViewCheques" runat="server" Width="98%" EmptyDataText="No data found"
                                ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                                CssClass="GridView">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField HeaderText="Type" DataField="sar_receipt_type" />
                                    <asp:BoundField HeaderText="Ref #" DataField="sar_receipt_no" />
                                    <asp:BoundField HeaderText="Prefix" DataField="sar_prefix" />
                                    <asp:BoundField HeaderText="Manual Ref" DataField="sar_manual_ref_no" />
                                    <asp:BoundField HeaderText="Date" DataField="sard_chq_dt" DataFormatString='<%$ appSettings:FormatToDate %>'>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Amount" DataField="sard_settle_amt" DataFormatString='<%$ appSettings:FormatToCurrency %>'>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
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
                    <div style="float: left; width: 100%; color: Black; padding: 2px 0px 0px 0px;">
                        <div style="float: left; width: 100%; color: Black;">
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 20%;">
                                Total System Amount
                            </div>
                            <div style="float: left; width: 20%;">
                                <asp:Label ID="LabelToSysAmo" runat="server"></asp:Label>
                            </div>
                            <div style="float: left; width: 10%;">
                                &nbsp;</div>
                            <div style="float: left; width: 20%;">
                                Total Return Amount
                            </div>
                            <div style="float: left; width: 20%;">
                                <asp:TextBox ID="TextBoxReturnAmount" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div style="float: left; width: 100%; color: Black;">
                    </div>
                    <%--        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Corrections">
                        <ContentTemplate>
                            <div style="float: left; width: 100%; color: Black;">
                                <div style="float: left; width: 100%; color: Black; padding: 2px 0px 0px 0px;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 20%;">
                                        Location
                                    </div>
                                    <div style="float: left; width: 69%;">
                                        <asp:TextBox ID="TextBox1" CssClass="TextBox" runat="server" AutoPostBack="True"
                                            OnTextChanged="TextBoxLocation_TextChanged"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="SUN Upload">
                        <ContentTemplate>
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>--%>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
