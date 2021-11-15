<%@ Page Title="Exchange Rate Setup" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="ExchangeRateSetup.aspx.cs" Inherits="FF.WebERPClient.General_Modules.ExchangeRateSetup" %>

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
                <div>
                    <div style="float: left; width: 100%; height: 20px; text-align: right; background-color: #003399;">
                        <asp:Button ID="btnSave" runat="server" Text="Save" Height="100%" Width="70px" 
                            CssClass="Button" onclick="btnSave_Click" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" Height="100%" Width="70px"
                            CssClass="Button" OnClick="btnClear_Click" />
                    </div>
                    <div style="float: left; width: 100%; padding-top: 2px; padding-bottom: 2px;">
                        <div style="float: left; width: 50%; padding-top: 2px; padding-bottom: 2px;">
                            <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                <div style="float: left; width: 1%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 25%; color: #000000;">
                                    From Currency</div>
                                <div style="float: left; width: 1%; color: #000000;">
                                    :</div>
                                <div style="float: left; width: 35%; text-align: left;">
                                    <asp:DropDownList ID="ddlFromCur" runat="server" Width="125px" CssClass="ComboBox">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                <div style="float: left; width: 1%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 25%; color: #000000;">
                                    To Currency</div>
                                <div style="float: left; width: 1%; color: #000000;">
                                    :</div>
                                <div style="float: left; width: 35%; text-align: left;">
                                    <asp:DropDownList ID="ddlToCur" runat="server" Width="125px" CssClass="ComboBox">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div style="float: left; width: 50%; padding-top: 2px; padding-bottom: 2px;">
                            <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                <div style="float: left; width: 1%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 25%; color: #000000;">
                                    From Date</div>
                                <div style="float: left; width: 1%; color: #000000;">
                                    :</div>
                                <div style="float: left; width: 40%; text-align: left;">
                                    <asp:TextBox ID="txtFDate" runat="server" Style="margin-left: 18px" CssClass="TextBox"></asp:TextBox>
                                    <asp:Image ID="imgFDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                    <asp:CalendarExtender ID="FromDateCalExtender" runat="server" TargetControlID="txtFDate"
                                        PopupButtonID="imgFDate" Enabled="True" Format="dd/MM/yyyy">
                                    </asp:CalendarExtender>
                                </div>
                            </div>
                            <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                <div style="float: left; width: 1%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 25%; color: #000000;">
                                    To Date</div>
                                <div style="float: left; width: 1%; color: #000000;">
                                    :</div>
                                <div style="float: left; width: 40%; text-align: left;">
                                    <asp:TextBox ID="txtTDate" runat="server" Style="margin-left: 18px" CssClass="TextBox"></asp:TextBox>
                                    <asp:Image ID="imgTDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                    <asp:CalendarExtender ID="ToDateCalExtender" runat="server" TargetControlID="txtTDate"
                                        PopupButtonID="imgTDate" Enabled="True" Format="dd/MM/yyyy">
                                    </asp:CalendarExtender>
                                </div>
                            </div>
                        </div>
                        <div style="float: left; width: 100%; padding-top: 2px; padding-bottom: 2px;">
                            <div style="float: left; width: 30%; padding-top: 1px; padding-bottom: 1px;">
                                <div style="float: left; width: 1%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 40%; color: #000000;">
                                    Bank Selling Rate</div>
                                <div style="float: left; width: 1%; color: #000000;">
                                    :</div>
                                <div style="float: left; width: 40%; text-align: left; padding-left: 2px">
                                    <asp:TextBox ID="txtBankSelling" runat="server" ForeColor="Black" CssClass="TextBox"
                                        Style="text-align: right;" onkeypress="return isNumberKeyAndDot(event,value)"></asp:TextBox>
                                </div>
                            </div>
                            <div style="float: left; width: 30%; padding-top: 1px; padding-bottom: 1px;">
                                <div style="float: left; width: 1%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 40%; color: #000000;">
                                    Bank Buying Rate</div>
                                <div style="float: left; width: 1%; color: #000000;">
                                    :</div>
                                <div style="float: left; width: 40%; text-align: left; padding-left: 2px">
                                    <asp:TextBox ID="txtBankBuy" runat="server" ForeColor="Black" CssClass="TextBox"
                                        Style="text-align: right;" onkeypress="return isNumberKeyAndDot(event,value)"></asp:TextBox>
                                </div>
                            </div>
                            <div style="float: left; width: 30%; padding-top: 1px; padding-bottom: 1px;">
                                <div style="float: left; width: 1%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 40%; color: #000000;">
                                    Custom Rate</div>
                                <div style="float: left; width: 1%; color: #000000;">
                                    :</div>
                                <div style="float: left; width: 40%; text-align: left; padding-left: 2px">
                                    <asp:TextBox ID="txtCustom" runat="server" ForeColor="Black" CssClass="TextBox" Style="text-align: right;"
                                        onkeypress="return isNumberKeyAndDot(event,value)"></asp:TextBox>
                                </div>
                            </div>
                            <div style="float: left; width: 10%; padding-top: 1px; padding-bottom: 1px;">
                                <asp:Button ID="btnAdd" runat="server" Text="Add" Width="100%" BorderStyle="Solid"
                                    CssClass="Button" onclick="btnAdd_Click" />
                            </div>
                        </div>
                        <div style="float: left; width: 100%; height: 165px; margin-top: 50px;">
                            <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Height="120px" BorderColor="#9F9F9F" BorderWidth="1px"
                                Font-Bold="true" Width="100%" Style="margin-top: 22px">
                                <asp:GridView ID="gvExchange" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                                    Style="margin-top: 0px" GridLines="None" Width="99%" CellPadding="2" ForeColor="#333333"
                                    CssClass="GridView" EmptyDataText="No data found" ShowHeaderWhenEmpty="True">
                                    <Columns>
                                        <asp:BoundField DataField='mer_id' HeaderText='' Visible="false" />
                                        <asp:BoundField DataField='mer_com' HeaderText='' Visible="false" />
                                        <asp:BoundField DataField='mer_cur' HeaderText='Base Currency' HeaderStyle-Width="12%"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField='mer_to_cur' HeaderText='To Currency' HeaderStyle-Width="8%"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField='mer_vad_from' HeaderText='from date' HeaderStyle-Width="30%" DataFormatString="{0:dd/MMM/yyyy}"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField='mer_vad_to' HeaderText='to date' HeaderStyle-Width="10%" DataFormatString="{0:dd/MMM/yyyy}"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField='mer_bnksel_rt' HeaderText='Selling Rate' HeaderStyle-Width="20%"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField='mer_bnkbuy_rt' HeaderText='Buying Rate' HeaderStyle-Width="20%"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField='mer_cussel_rt' HeaderText='Custom Rate' HeaderStyle-Width="20%"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgSerialDelete" runat="server" ImageUrl="~/Images/Delete.png"
                                                    Width="13px" Height="13px" CommandName="Delete" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle BackColor="White" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" Height="10px" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />

                                </asp:GridView>
                            </asp:Panel>
                        </div>
                </div>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
