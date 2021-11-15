<%@ Page Title="Inventory Document Cancelation" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="InventoryDocCancelation.aspx.cs" Inherits="FF.WebERPClient.Inventory_Module.InventoryDocCancelation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updtMainPnl" runat="server">
        <ContentTemplate>
            <script type="text/javascript">
                function fun1(e, button2) {
                    var evt = e ? e : window.event;
                    var bt = document.getElementById(button2);
                    if (bt) {
                        if (evt.keyCode == 13) {
                            bt.click();
                            return false;
                        }
                    }
                }

            </script>
            <div id="divMain" style="color: Black;">
            </div>
            <div style="text-align: right">
                <div style="height: 18px; background-color: #1E4A9F; color: White; width: 100%; float: left;">
                    <asp:HiddenField ID="hdnAllowBin" runat="Server" Value="0" />
                    <asp:HiddenField ID="hdnDefBinCd" runat="Server" Value="0" />
                    <%--  <asp:HiddenField ID="hdnOutwarddate" runat="Server" Value="" />   --%>
                </div>
                <asp:Button ID="btnConfirm" runat="server" CssClass="Button" Text="Cancel" OnClick="btnConfirm_Click" />
                &nbsp;
                <asp:Button ID="btnClear" runat="server" CssClass="Button" Text="Clear" OnClick="btnClear_Click" />
                &nbsp;<asp:Button ID="btnClose" runat="server" Text="Close" CssClass="Button" OnClick="btnClose_Click" />
            </div>
            <div style="float: left; width: 100%;">
                <asp:Panel ID="PanelHeader1" runat="server" CssClass="PanelHeader" Font-Bold="True">
                    Document Search Panal
                </asp:Panel>
                <asp:Panel ID="Panel1" runat="server">
                    <div class="div100pcelt" style="padding: 3.0px">
                        <div style="float: left; width: 25%;">
                            <div style="float: left; width: 50%; font-weight: bold;">
                                Document Type</div>
                            <div style="float: left; width: 49%;">
                                <asp:DropDownList ID="ddlDocType" runat="server" CssClass="ComboBox" Width="95%">
                                    <asp:ListItem>AOD</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div style="float: left; width: 20%;">
                            <div style="float: left; width: 40%;">
                                From Date:</div>
                            <div style="float: left; width: 59%;">
                                <asp:TextBox ID="txtFromDt" runat="server" CssClass="TextBox" Width="80%"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtendertxtFromDt" runat="server" Enabled="True"
                                    Format="dd/MM/yyyy" TargetControlID="txtFromDt">
                                </asp:CalendarExtender>
                            </div>
                        </div>
                        <div style="float: left; width: 20%;">
                            <div style="float: left; width: 40%;">
                                To Date:</div>
                            <div style="float: left; width: 59%;">
                                <asp:TextBox ID="txtToDt" runat="server" CssClass="TextBox" Width="80%"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                    TargetControlID="txtToDt">
                                </asp:CalendarExtender>
                            </div>
                        </div>
                        <div style="float: left; width: 20%;">
                            <div style="float: left; width: 50%;">
                                Invalid Location</div>
                            <div style="float: left; width: 49%;">
                                <asp:TextBox ID="txtInvalidLoc" runat="server" CssClass="TextBox" Width="90%"></asp:TextBox>
                            </div>
                        </div>
                        <div style="float: left; width: 10%; text-align: right;">
                        </div>
                    </div>
                    <div class="div100pcelt" style="padding: 3.0px">
                        <div style="float: left; width: 50%; font-weight: bold;">
                            <div style="float: left; width: 25%;">
                                Document No.
                            </div>
                            <div style="float: left; width: 74%; font-weight: bold;">
                                <asp:TextBox ID="txtDocNum" runat="server" CssClass="TextBox" Width="80%"></asp:TextBox>
                                &nbsp;
                                <asp:ImageButton ID="ImgBtnDocSearch" runat="server" ImageUrl="~/Images/icon_search.png"
                                    OnClick="ImgBtnDocSearch_Click" />
                            </div>
                            <div style="float: left; width: 50%; font-weight: bold;">
                            </div>
                        </div>
                    </div>
                    <div class="div100pcelt" style="padding: 3.0px">
                        <div style="float: left; width: 25%;">
                            <div style="float: left; width: 40%;">
                                Issued Location:</div>
                            <div style="float: left; width: 40%;">
                                <asp:Label ID="lblIssuedLoc" runat="server" Text="" ForeColor="Blue"></asp:Label>
                            </div>
                        </div>
                        <div style="float: left; width: 25%;">
                            <div style="float: left; width: 40%;">
                                Invalid Location:</div>
                            <div style="float: left; width: 40%;">
                                <asp:Label ID="lblInvalidLoc" runat="server" Text="" ForeColor="Blue"></asp:Label>
                            </div>
                        </div>
                        <div style="float: left; width: 25%;">
                            <div style="float: left; width: 40%;">
                                Issued Date:</div>
                            <div style="float: left; width: 40%;">
                                <asp:Label ID="lblIssuedDt" runat="server" Text="" ForeColor="Blue"></asp:Label>
                            </div>
                        </div>
                        <div style="float: left; width: 25%;">
                            <div style="float: left; width: 40%;">
                                Status:</div>
                            <div style="float: left; width: 40%;">
                                <asp:Label ID="lblStatus" runat="server" Text="" ForeColor="Blue"></asp:Label>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <div style="float: left; width: 100%;">
                <div style="float: left; width: 60%;">
                    <asp:Panel ID="Panel3" runat="server" CssClass="PanelHeader" Font-Bold="True">
                        Item Details</asp:Panel>
                    <asp:Panel ID="Panel2" runat="server" BackColor="White" Height="180px" ScrollBars="Both">
                        <asp:GridView ID="grvItemDet" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                            CellPadding="4" ForeColor="#333333" Width="99%" OnSelectedIndexChanged="grvItemDet_SelectedIndexChanged"
                            CssClass="GridView">
                            <AlternatingRowStyle BackColor="White" />
                            <EmptyDataTemplate>
                                <div style="width: 100%; text-align: center;">
                                    No data found
                                </div>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:CommandField SelectText="select" ShowSelectButton="True">
                                    <ItemStyle Font-Bold="True" ForeColor="#009900" />
                                </asp:CommandField>
                                <asp:BoundField HeaderText="Item Code" DataField="ITI_ITM_CD" />
                                <asp:BoundField HeaderText="Description" DataField="mi_shortdesc" />
                                <asp:BoundField HeaderText="Model" DataField="mi_model" />
                                <asp:BoundField DataField="mi_brand" HeaderText="Brand" />
                                <asp:BoundField HeaderText="Qty." DataField="ITI_QTY" />
                                <asp:BoundField HeaderText="Status" DataField="ITI_ITM_STUS" />
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
                <div style="float: left; width: 39%;">
                    <asp:Panel ID="Panel4" runat="server" CssClass="PanelHeader" Font-Bold="True">
                        Serial Details
                    </asp:Panel>
                    <asp:Panel ID="Panel5" runat="server" BackColor="White" Height="180px" ScrollBars="Both">
                        <asp:GridView ID="grvSerialDet" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                            CellPadding="4" ForeColor="#333333" Width="99%" CssClass="GridView">
                            <AlternatingRowStyle BackColor="White" />
                            <EmptyDataTemplate>
                                <div style="width: 100%; text-align: center;">
                                    No data found
                                </div>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField HeaderText="Serial #" DataField="its_ser_1" />
                                <asp:BoundField HeaderText="Warranty #" DataField="its_warr_no" />
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
            <div style="float: left; width: 100%;">
                <asp:Panel ID="Panel6" runat="server" CssClass="PanelHeader" Font-Bold="True">
                    Cancellation Remarks</asp:Panel>
                <asp:Panel ID="Panel7" runat="server">
                    <div class="div100pcelt" style="padding: 3.0px">
                        <div style="float: left; width: 1%; color: #333333;">
                            &nbsp;</div>
                    </div>

                    <div class="div100pcelt" style="padding: 3.0px">
                        <div style="float: left; width: 20%; color: #333333;">
                            Remarks
                        </div>
                        <div style="float: left; width: 40%;">
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="TextBox" Width="100%"></asp:TextBox>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <div style="display: none">
                <asp:Button ID="btnGetDocDet" runat="server" Text="Button" OnClick="btnGetDocDet_Click" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
