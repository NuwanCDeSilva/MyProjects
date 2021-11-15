<%@ Page Title="Account creation" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AccountCreation.aspx.cs" Inherits="FF.WebERPClient.HP_Module.AccountCreation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControls/uc_CustomerCreation.ascx" TagName="uc_CustomerCreation"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/uc_CustCreationExternalDet.ascx" TagName="uc_CustCreationExternalDet"
    TagPrefix="uc2" %>
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
    <style type="text/css">
        .GridView
        {
            margin-left: 0px;
        }
    </style>
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
                <div style="float: left; width: 100%; height: 50px;">
                    <div style="height: 18px; background-color: #1E4A9F; color: White; width: 100%; float: left;
                        padding-bottom: 5px">
                        Check Customer</div>
                    <div style="float: left; width: 70%; height: 30px; padding-top: 4px">
                        &nbsp;<asp:Label ID="lblIdentity" runat="server" Text="Identification :" Width="90px"></asp:Label>
                        <asp:TextBox ID="txtIdentiry" runat="server" Width="150px" Style="margin-left: 10px"
                            CssClass="TextBox" MaxLength="20"></asp:TextBox>&nbsp;<asp:Button ID="btnCheck" runat="server"
                                Text="Check" Height="70%" Width="70px" CssClass="Button" OnClick="btnCheck_Click"
                                ViewStateMode="Disabled" />&nbsp;
                        <asp:Label ID="Label5" runat="server" Text="Account # :" Width="90px"></asp:Label>
                        <asp:TextBox ID="txtReAcc" runat="server" Width="120px" Style="margin-left: 2px"
                            CssClass="TextBox"></asp:TextBox>
                        <asp:ImageButton ID="imgSearchAcc" runat="server" ImageUrl="~/Images/icon_search.png"
                            OnClick="imgSearchAcc_Click" />
                        <asp:Button ID="btnInsuPrint" runat="server" Text="Print Insu" Height="70%" Width="70px"
                            CssClass="Button" OnClick="btnInsuPrint_Click" />
                    </div>
                    <div style="float: left; width: 30%; height: 30px; padding-top: 4px">
                        <asp:CheckBox ID="chkDel" runat="server" Width="10%"></asp:CheckBox>
                        <asp:Label ID="Label4" runat="server" Text="Deliver Later" Width="100px"></asp:Label>
                    </div>
                </div>
                <%--item selecting area--%>
                <div style="float: left; width: 100%; padding-top: 2px; padding-bottom: 2px;">
                    <%-- Collaps Header - Items --%>
                    <div style="height: 18px; background-color: #1E4A9F; color: White; width: 98%; float: left;
                        margin-top: 6px;">
                        Item Detail</div>
                    <%-- Collaps Image - Items --%>
                    <div style="float: left; margin-top: 6px;">
                        <asp:Image runat="server" ID="Image1" ImageUrl="~/Images/16 X 16 DownArrow.jpg" ImageAlign="Right" /></div>
                    <%-- Collaps control - Items --%>
                    <asp:CollapsiblePanelExtender ID="CPEHPItem" runat="server" TargetControlID="pnlHPItem"
                        CollapsedSize="0" ExpandedSize="470" Collapsed="false" ExpandControlID="Image1"
                        CollapseControlID="Image1" AutoCollapse="False" AutoExpand="False" ScrollContents="false"
                        ExpandDirection="Vertical" ImageControlID="Image1" ExpandedImage="~/Images/16 X 16 UpArrow.jpg"
                        CollapsedImage="~/Images/16 X 16 DownArrow.jpg">
                    </asp:CollapsiblePanelExtender>
                    <%-- Collaps Area - HP Items --%>
                    <div style="width: 100%; float: left; padding-top: 2px; padding-bottom: 2px;">
                        <asp:Panel runat="server" ID="pnlHPItem" Width="100%" BorderColor="#9F9F9F" BorderWidth="1px"
                            Font-Bold="true">
                            <div style="float: left; width: 100%;">
                                <div style="float: left; width: 100%; font-weight: normal; padding-top: 1px; padding-bottom: 1px;
                                    height: 15px;">
                                    <div style="float: left; width: 150px; background-color: #507CD1; border-right: 1px solid white;
                                        color: White; height: 15px;">
                                        Item</div>
                                    <div style="float: left; width: 350px; background-color: #507CD1; border-right: 1px solid white;
                                        color: White; height: 15px;">
                                        Description</div>
                                    <div style="float: left; width: 160px; background-color: #507CD1; border-right: 1px solid white;
                                        color: White; height: 15px;">
                                        Model</div>
                                    <div style="float: left; width: 160px; background-color: #507CD1; border-right: 1px solid white;
                                        color: White; height: 15px;">
                                        Brand</div>
                                    <div style="float: left; width: 125px; background-color: #507CD1; border-right: 1px solid white;
                                        color: White; height: 15px; text-align: right">
                                        Qty</div>
                                </div>
                                <%--<div style="float: right; width: 39%; font-weight: normal; padding-top: 1px; padding-bottom: 1px;
                                    height: 15px;">
                                    <div style="float: right; width: 100%; background-color: #507CD1; border-right: 1px solid white;
                                        color: White; height: 15px;">
                                        Available prices</div>
                                </div>--%>
                                <div style="float: left; width: 100%; font-weight: normal; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 150px; border-right: 1px solid white;">
                                        <asp:TextBox runat="server" ID="txtItem" CssClass="TextBox" ClientIDMode="Static"
                                            Width="97.5%"></asp:TextBox></div>
                                    <div style="float: left; width: 350px; border-right: 1px solid white;">
                                        <asp:TextBox runat="server" ID="txtDesc" CssClass="TextBox" ClientIDMode="Static"
                                            Width="99%" ReadOnly="true"></asp:TextBox></div>
                                    <div style="float: left; width: 160px; border-right: 1px solid white;">
                                        <asp:TextBox runat="server" ID="txtModel" CssClass="TextBox" ClientIDMode="Static"
                                            Width="97.5%" ReadOnly="true"></asp:TextBox></div>
                                    <div style="float: left; width: 160px; border-right: 1px solid white;">
                                        <asp:TextBox runat="server" ID="txtBrand" CssClass="TextBox" ClientIDMode="Static"
                                            Width="97.5%" ReadOnly="true"></asp:TextBox></div>
                                    <div style="float: left; width: 110px; border-right: 1px solid white;">
                                        <asp:TextBox runat="server" ID="txtQty" CssClass="TextBox" ClientIDMode="Static"
                                            Width="95%" Style="text-align: right;" onkeypress="return isNumberKeyAndDot(event,value)"></asp:TextBox></div>
                                    <div style="float: left; width: 16px;">
                                        <asp:ImageButton runat="server" ID="btnAddItem" ImageAlign="Middle" ImageUrl="~/Images/Add-16x16x16.ICO"
                                            OnClick="btnAddItem_Click" Height="16px" Style="margin-left: 1px" />
                                    </div>
                                </div>
                                <div style="float: left; width: 100%;">
                                    <asp:Panel ID="Panel_popUp" runat="server" BackColor="Silver" Width="50%" BorderColor="#333399"
                                        BorderWidth="2px" ScrollBars="Auto" Height = "520px">
                                        <div style="padding: 0.5px; float: left; width: 100%; text-align: right;">
                                            <div style="padding: 0.5px; float: left; width: 92%; height: 13px;">
                                                <asp:Button ID="btnPopupConfirm" runat="server" Text="Confirm" CssClass="Button"
                                                    OnClick="btnPopupConfirm_Click" />
                                                <asp:Button ID="btnPopupCancel" runat="server" Text="Close" CssClass="Button" OnClick="btnPopupCancel_Click" />
                                            </div>
                                        </div>
                                        <div style="float: left; width: 100%; height: 8px; padding-top: 5px;">
                                        </div>
                                        <div>
                                            <div style="float: left; width: 100%; height: 8px; padding-top: 5px;">
                                            </div>
                                            <asp:GridView ID="grvPopUpCombines" runat="server" CellPadding="4" CssClass="GridView"
                                                ForeColor="#333333" AutoGenerateColumns="False" Width="90%" ShowHeaderWhenEmpty="True"
                                                DataKeyNames="sapd_itm_cd,sapd_pb_seq,sapd_seq_no,sapd_itm_price" OnRowCommand="grvPopUpCombines_Rowcommand">
                                                <AlternatingRowStyle BackColor="White" />
                                                <EmptyDataTemplate>
                                                    <div style="width: 100%; text-align: center;">
                                                        No data found
                                                    </div>
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Select">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkPromoSelect" runat="server" Checked="false" Enabled="false" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="sapd_pb_tp_cd" HeaderText="Book">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="sapd_pbk_lvl_cd" HeaderText="Level">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="sapd_itm_cd" HeaderText="Item">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="sapd_itm_price" HeaderText="Price" DataFormatString="{0:F}">
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" Width="8%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="sapd_promo_cd" HeaderText="Promotion code">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="sapd_pb_seq" HeaderText="Seq #">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="sapd_seq_no" HeaderText="Seq #">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgfreeSelect" runat="server" CommandName="SELECT" ImageUrl="~/Images/download_arrow_icon.png"
                                                                Height="15px" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="5%" Height="15px" />
                                                    </asp:TemplateField>
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
                                        <div style="float: left; width: 100%; height: 10px; padding-top: 5px;">
                                        </div>
                                        <div>
                                            <div style="float: left; width: 100%; height: 10px; padding-top: 5px;">
                                            </div>
                                            <asp:GridView ID="gvfreeItem" runat="server" CellPadding="4" CssClass="GridView"
                                                ForeColor="#333333" AutoGenerateColumns="False" Width="90%" ShowHeaderWhenEmpty="True">
                                                <AlternatingRowStyle BackColor="White" />
                                                <EmptyDataTemplate>
                                                    <div style="width: 100%; text-align: center;">
                                                        No data found
                                                    </div>
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:BoundField DataField="sapc_main_itm_cd" Visible="false" />
                                                    <asp:BoundField DataField="sapc_itm_cd" HeaderText="Free item">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="sapc_qty" HeaderText="Qty">
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" Width="8%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="sapc_price" HeaderText="Price" DataFormatString="{0:F}">
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" Width="8%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="sapc_pb_seq" Visible="false" />
                                                    <asp:BoundField DataField="sapc_main_line" Visible="false" />
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
                                        <div style="float: left; width: 100%;">
                                            &nbsp;</div>
                                    </asp:Panel>
                                </div>
                                <div style="float: left; width: 100%; height: 100px; padding-top: 1.5px; padding-bottom: 2px;
                                    background-color: #FFFFFF;">
                                    <asp:Panel ID="pnlItem" runat="server" Height="99px" ScrollBars="Auto" BorderColor="#9F9F9F"
                                        BorderWidth="1px" Font-Bold="true" Width="99%">
                                        <asp:GridView ID="gvHPItem" CssClass="GridView" runat="server" AutoGenerateColumns="False"
                                            Font-Bold="False" CellPadding="4" ForeColor="#333333" GridLines="None" ShowHeader="False"
                                            ShowHeaderWhenEmpty="True" OnRowDeleting="OnRemoveFromHPItem">
                                            <AlternatingRowStyle BackColor="White" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <EmptyDataTemplate>
                                                <div style="width: 925px; text-align: center;">
                                                    No item selected.
                                                </div>
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField='mi_cd' HeaderText='Item Code' ItemStyle-Width="142px"
                                                    HeaderStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField='mi_longdesc' HeaderText='Description' ItemStyle-Width="348px"
                                                    HeaderStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField='mi_model' HeaderText='Model' ItemStyle-Width="155px" HeaderStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField='mi_brand' HeaderText='Brand' ItemStyle-Width="155px" HeaderStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField='mi_dim_length' HeaderText='Qty' ItemStyle-Width="100px"
                                                    ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgBtnGridDelete" runat="server" ImageUrl="~/Images/Delete.png"
                                                            Width="10px" Height="13px" CommandName="Delete" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
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
                                <%-- <div style="float: right; width: 39%; font-weight: normal;">
                                    <asp:TabContainer ID="Tab1" runat="server" ActiveTabIndex="0" Style="text-align: left"
                                        Width="98%" Font-Bold="False" Height="80px">
                                        <asp:TabPanel ID="tbNormal" runat="server" HeaderText="Normal prices" Width="98%"
                                            Font-Bold="False">
                                            <ContentTemplate>
                                                <asp:Panel ID="pnlNormal" runat="server" Height="80px" ScrollBars="Auto" BorderWidth="0px"
                                                    Width="100%">
                                                    <asp:GridView ID="gvNormalPrice" CssClass="GridView" runat="server" AutoGenerateColumns="False"
                                                        Font-Bold="False" CellPadding="4" ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True"
                                                        Width="332px">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:BoundField DataField='sapd_itm_cd' HeaderText='Item'>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle Width="90px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField='sapd_pb_tp_cd' HeaderText='Book'>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle Width="80px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField='sapd_pbk_lvl_cd' HeaderText='Level'>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle Width="60px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField='sapd_itm_price' HeaderText='Price' DataFormatString="{0:F}">
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                <ItemStyle Width="75px" HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <EmptyDataTemplate>
                                                            <div style="width: 320px; text-align: center;">
                                                                No data found.
                                                            </div>
                                                        </EmptyDataTemplate>
                                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Height="12px" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                        <asp:TabPanel ID="tbPromo" runat="server" HeaderText="Promotion prices" Width="98%"
                                            Font-Bold="False">
                                            <ContentTemplate>
                                                <asp:Panel ID="pnlPromo" runat="server" Height="80px" ScrollBars="Auto" BorderWidth="0px"
                                                    Width="100%">
                                                    <asp:GridView ID="gvPromoPrice" CssClass="GridView" runat="server" AutoGenerateColumns="False"
                                                        Font-Bold="False" CellPadding="4" ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True"
                                                        Width="332px">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <EmptyDataTemplate>
                                                            <div style="width: 320px; text-align: center;">
                                                                No data found.
                                                            </div>
                                                        </EmptyDataTemplate>
                                                        <Columns>
                                                            <asp:BoundField DataField='sapd_itm_cd' HeaderText='Item'>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle Width="90px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField='sapd_pb_tp_cd' HeaderText='Book'>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle Width="80px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField='sapd_pbk_lvl_cd' HeaderText='Level'>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle Width="60px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField='sapd_itm_price' HeaderText='Price' DataFormatString="{0:F}">
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                <ItemStyle Width="75px" HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Height="12px" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                    </asp:TabContainer>
                                </div>--%>
                                <div style="float: left; width: 100%; font-weight: normal; padding-top: 1px; padding-bottom: 1px;
                                    height: 15px;">
                                    <div style="float: left; width: 100%; background-color: #507CD1; border-right: 1px solid white;
                                        color: White; height: 15px;">
                                        Available common price books & related prices</div>
                                </div>
                                <div style="float: left; width: 60%; height: 100px; padding-top: 1.5px; padding-bottom: 2px;
                                    background-color: #FFFFFF;">
                                    <asp:Panel ID="pnlBook" runat="server" Height="99px" ScrollBars="Auto" BorderColor="#9F9F9F"
                                        BorderWidth="1px" Font-Bold="true" Width="99%">
                                        <asp:GridView ID="gvBooks" CssClass="GridView" runat="server" AutoGenerateColumns="False"
                                            Font-Bold="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="gvBooks_Rowcommand"
                                            ShowHeaderWhenEmpty="True">
                                            <AlternatingRowStyle BackColor="White" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <EmptyDataTemplate>
                                                <div style="width: 550px; text-align: center;">
                                                    No data found.
                                                </div>
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField HeaderText="Select">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" Checked="false" Enabled="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField='tcp_pb_cd' HeaderText='Book'>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle Width="15%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField='tcp_pb_desc' HeaderText='Description'>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle Width="60%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField='tcp_pb_lvl' HeaderText='Level'>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle Width="15%" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgSelect" runat="server" CommandName="SELECT" ImageUrl="~/Images/download_arrow_icon.png"
                                                            Height="15px" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="20%" Height="15px" />
                                                </asp:TemplateField>
                                            </Columns>
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
                                <%--  <div style="float: right; width: 39%; font-weight: normal; padding-top: 1px; padding-bottom: 1px;
                                    height: 15px;">
                                    <div style="float: right; width: 100%; background-color: #507CD1; border-right: 1px solid white;
                                        color: White; height: 15px;">
                                        Available prices</div>
                                </div>--%>
                                <div style="float: right; width: 39%; font-weight: normal;">
                                    <asp:TabContainer ID="Tab1" runat="server" ActiveTabIndex="0" Style="text-align: left"
                                        Width="98%" Font-Bold="False" Height="70px">
                                        <asp:TabPanel ID="tbNormal" runat="server" HeaderText="Normal prices" Width="98%"
                                            Font-Bold="False">
                                            <ContentTemplate>
                                                <asp:Panel ID="pnlNormal" runat="server" Height="70px" ScrollBars="Auto" BorderWidth="0px"
                                                    Width="100%">
                                                    <asp:GridView ID="gvNormalPrice" CssClass="GridView" runat="server" AutoGenerateColumns="False"
                                                        Font-Bold="False" CellPadding="4" ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True"
                                                        Width="332px">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:BoundField DataField='sapd_itm_cd' HeaderText='Item'>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle Width="90px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField='sapd_pb_tp_cd' HeaderText='Book'>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle Width="80px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField='sapd_pbk_lvl_cd' HeaderText='Level'>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle Width="60px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField='sapd_itm_price' HeaderText='Price' DataFormatString="{0:F}">
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                <ItemStyle Width="75px" HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <EmptyDataTemplate>
                                                            <div style="width: 320px; text-align: center;">
                                                                No data found.
                                                            </div>
                                                        </EmptyDataTemplate>
                                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Height="12px" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                        <asp:TabPanel ID="tbPromo" runat="server" HeaderText="Promotion prices" Width="98%"
                                            Font-Bold="False">
                                            <ContentTemplate>
                                                <asp:Panel ID="pnlPromo" runat="server" Height="70px" ScrollBars="Auto" BorderWidth="0px"
                                                    Width="100%">
                                                    <asp:GridView ID="gvPromoPrice" CssClass="GridView" runat="server" AutoGenerateColumns="False"
                                                        Font-Bold="False" CellPadding="4" ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True"
                                                        Width="332px">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <EmptyDataTemplate>
                                                            <div style="width: 320px; text-align: center;">
                                                                No data found.
                                                            </div>
                                                        </EmptyDataTemplate>
                                                        <Columns>
                                                            <asp:BoundField DataField='sapd_itm_cd' HeaderText='Item'>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle Width="90px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField='sapd_pb_tp_cd' HeaderText='Book'>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle Width="80px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField='sapd_pbk_lvl_cd' HeaderText='Level'>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle Width="60px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField='sapd_itm_price' HeaderText='Price' DataFormatString="{0:F}">
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                <ItemStyle Width="75px" HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Height="12px" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                    </asp:TabContainer>
                                </div>
                                <%-- <div style="float: right; width: 39%; font-weight: normal; padding-top: 5px; padding-bottom: 1px;
                                    height: 15px;">
                                    <div style="float: right; width: 100%; background-color: #507CD1; border-right: 1px solid white;
                                        color: White;">
                                        Selected item prices</div>
                                </div>--%>
                                <%-- <div style="float: right; width: 39%; height: 100px; padding-top: 1.5px; padding-bottom: 2px;
                                    background-color: #FFFFFF;">
                                    <asp:Panel ID="pnlItemPrice" runat="server" Height="99px" ScrollBars="Auto" BorderColor="#9F9F9F"
                                        BorderWidth="1px" Font-Bold="true" Width="99%">
                                        <asp:GridView ID="gvItemWithPrice" CssClass="GridView" runat="server" AutoGenerateColumns="False"
                                            Font-Bold="False" CellPadding="4" ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True"
                                            OnRowCommand="gvItemWithPrice_Rowcommand" DataKeyNames="tcp_pb_cd,tcp_pb_lvl">
                                            <AlternatingRowStyle BackColor="White" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <EmptyDataTemplate>
                                                <div style="width: 320px; text-align: center;">
                                                    No data found.
                                                </div>
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkGet" runat="server" Checked="false" Enabled="false" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField='tcp_itm' HeaderText='Item'>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle Width="15%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField='tcp_itm_desc' HeaderText='Description'>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle Width="30%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField='tcp_qty' HeaderText='Qty'>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle Width="8%" HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField='tcp_price' HeaderText='Price' DataFormatString="{0:F}">
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle Width="10%" HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField='tcp_total' HeaderText='Total' DataFormatString="{0:F}">
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle Width="10%" HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgGet" runat="server" CommandName="GET" ImageUrl="~/Images/download_arrow_icon.png"
                                                            Height="10px" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="8%" Height="10px" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField='tcp_pb_cd' HeaderText='' Visible="false" />
                                                <asp:BoundField DataField='tcp_pb_lvl' HeaderText='' Visible="false" />
                                            </Columns>
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
                                </div>--%>
                            </div>
                            <%-- <div style="float: right; width: 100%; 
                                            background-color: #FFFFFF; text-align: right;">
                            <asp:Button ID="btnAll" runat="server" Text="GetAll" BorderStyle="Solid" CssClass="Button"/>
                            </div>--%>
                            <div style="float: left; width: 100%; background-color: #FFFFFF;">
                                &nbsp;
                                <div style="float: left; width: 100%; font-weight: normal; padding-top: 5px; padding-bottom: 1px;
                                    height: 15px;">
                                    <div style="float: left; width: 100%; background-color: #507CD1; border-right: 1px solid white;
                                        color: White; height: 15px;">
                                        Agreed items
                                    </div>
                                    <div style="float: left; width: 100%; height: 85px; padding-top: 1.5px; padding-bottom: 2px;
                                        background-color: #FFFFFF;">
                                        <asp:Panel ID="pnlAllItems" runat="server" Height="93px" ScrollBars="Auto" BorderColor="#9F9F9F"
                                            BorderWidth="1px" Font-Bold="true" Width="98%">
                                            <asp:GridView ID="gvAllItems" CssClass="GridView" runat="server" AutoGenerateColumns="False"
                                                Font-Bold="False" CellPadding="4" ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True"
                                                DataKeyNames="sad_pbook,sad_pb_lvl">
                                                <AlternatingRowStyle BackColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <EmptyDataTemplate>
                                                    <div style="width: 900px; text-align: center;">
                                                        No data found.
                                                    </div>
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:BoundField DataField="sad_seq_no" Visible="false" />
                                                    <asp:BoundField DataField="sad_itm_line" HeaderText="Line">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="sad_inv_no" Visible="false" />
                                                    <asp:BoundField DataField="sad_itm_cd" HeaderText="Code">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" Width="24%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="sad_itm_stus" Visible="false" />
                                                    <asp:BoundField DataField="sad_itm_tp" Visible="false" />
                                                    <asp:BoundField DataField="sad_uom" Visible="false" />
                                                    <asp:BoundField DataField="sad_qty" HeaderText="Qty">
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" Width="8%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="sad_do_qty" Visible="false" />
                                                    <asp:BoundField DataField="sad_unit_rt" HeaderText="Unit price" DataFormatString="{0:F}">
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" Width="12%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="sad_unit_amt" HeaderText="Amount" DataFormatString="{0:F}">
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" Width="15%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="sad_disc_rt" Visible="false" />
                                                    <asp:BoundField DataField="sad_disc_amt" HeaderText="Discount" DataFormatString="{0:F}">
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" Width="12%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="sad_itm_tax_amt" HeaderText="Tax Amount" DataFormatString="{0:F}">
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" Width="12%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="sad_tot_amt" HeaderText="Total" DataFormatString="{0:F}">
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" Width="20%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="sad_pbook" Visible="false" />
                                                    <asp:BoundField DataField="sad_pb_lvl" Visible="false" />
                                                    <asp:BoundField DataField="sad_pb_price" Visible="false" />
                                                    <asp:BoundField DataField="sad_seq" Visible="False" />
                                                    <asp:BoundField DataField="sad_itm_seq" Visible="false" />
                                                    <asp:BoundField DataField="sad_warr_period" Visible="false" />
                                                    <asp:BoundField DataField="sad_warr_remarks" Visible="false" />
                                                    <asp:BoundField DataField="sad_is_promo" Visible="false" />
                                                    <asp:BoundField DataField="sad_promo_cd" Visible="false" />
                                                    <asp:BoundField DataField="sad_comm_amt" Visible="false" />
                                                    <asp:BoundField DataField="sad_alt_itm_cd" Visible="false" />
                                                    <asp:BoundField DataField="sad_alt_itm_desc" Visible="false" />
                                                    <asp:BoundField DataField="sad_print_stus" Visible="false" />
                                                    <asp:BoundField DataField="sad_res_no" Visible="false" />
                                                    <asp:BoundField DataField="sad_res_line_no" Visible="false" />
                                                    <asp:BoundField DataField="sad_job_no" Visible="false" />
                                                    <asp:BoundField DataField="sad_fws_ignore_qty" Visible="false" />
                                                    <asp:BoundField DataField="sad_warr_based" Visible="false" />
                                                    <asp:BoundField DataField="sad_merge_itm" Visible="false" />
                                                    <asp:BoundField DataField="sad_job_line" Visible="false" />
                                                    <asp:BoundField DataField="sad_outlet_dept" Visible="false" />
                                                </Columns>
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
                                        <div style="float: right; width: 100%; height: 15px; padding-top: 1.5px; padding-bottom: 2px;
                                            background-color: #FFFFFF; text-align: right; padding-right: 30px;">
                                            <asp:Label ID="Label2" runat="server" ForeColor="Black" Text="VAT :"></asp:Label><asp:Label
                                                ID="lblTotVat" runat="server" ForeColor="Black" Text="VAT" Width="50px"></asp:Label>
                                            <asp:Label ID="Label1" runat="server" ForeColor="Black" Text="Total :" Width="65px"></asp:Label><asp:Label
                                                ID="lblTot" runat="server" ForeColor="Black" Text="Total" Width="70px"></asp:Label>
                                        </div>
                                        <div style="float: right; width: 100%; height: 7px; padding-top: 1.5px; padding-bottom: 2px;
                                            background-color: #FFFFFF; text-align: right;">
                                            <asp:Button ID="btnContinue" runat="server" Text="Continue" BorderStyle="Solid" CssClass="Button"
                                                OnClick="btnContinue_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <div style="display: none">
                        <asp:Button ID="btn_hiddenn" runat="server" Text="Button" />
                    </div>
                    <asp:ModalPopupExtender ID="ModalPopupExtItem" runat="server" ClientIDMode="Static"
                        PopupControlID="Panel_popUp" TargetControlID="btn_hiddenn" Drag="True">
                    </asp:ModalPopupExtender>
                </div>
                <%--item selecting area--%>
                <div style="float: left; width: 100%; padding-top: 2px; padding-bottom: 2px;">
                    <%-- Collaps Header - Items --%>
                    <div style="height: 18px; background-color: #1E4A9F; color: White; width: 98%; float: left;
                        margin-top: 6px;">
                        Scheme Details</div>
                    <%-- Collaps Image - Items --%>
                    <div style="float: left; margin-top: 6px;">
                        <asp:Image runat="server" ID="Image2" ImageUrl="~/Images/16 X 16 DownArrow.jpg" ImageAlign="Right" /></div>
                    <%-- Collaps control - Items --%>
                    <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" TargetControlID="pnlScheme"
                        CollapsedSize="0" ExpandedSize="435" Collapsed="True" ExpandControlID="Image2"
                        CollapseControlID="Image2" AutoCollapse="False" AutoExpand="False" ScrollContents="false"
                        ExpandDirection="Vertical" ImageControlID="Image2" ExpandedImage="~/Images/16 X 16 UpArrow.jpg"
                        CollapsedImage="~/Images/16 X 16 DownArrow.jpg">
                    </asp:CollapsiblePanelExtender>
                    <%-- Collaps Area - HP Items --%>
                    <div style="width: 100%; float: left; padding-top: 2px; padding-bottom: 2px;">
                        <asp:Panel runat="server" ID="pnlScheme" Width="99.8%" BorderColor="#9F9F9F" BorderWidth="1px">
                            <div style="float: left; width: 49.5%; padding-top: 1px; padding-bottom: 1px;">
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 55%; color: #000000;">
                                        Scheme</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 30%; text-align: right;">
                                        <asp:DropDownList ID="ddlScheme" runat="server" CssClass="ComboBox" Width="100px"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                        <%-- <asp:Label ID="lblSch" runat="server" ForeColor="Black" Text="[Select Scheme]"></asp:Label>--%>
                                    </div>
                                    <div style="float: left; width: 2%; color: #000000;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 2%; height: 15px; text-align: right;">
                                        <asp:ImageButton ID="imgPro" runat="server" ImageUrl="~/Images/expand_icon.jpg" OnClick="imgPro_Click" />
                                    </div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px; background-color: #EFF3FB;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 90%; color: #000000; font-weight: bold;">
                                        :: HP CALCULATION ::</div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 55%; color: #000000;">
                                        Create date</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 30%; text-align: right;">
                                        <asp:Label ID="lblCreateDate" runat="server" ForeColor="Black"></asp:Label></div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px; background-color: #EFF3FB;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 55%; color: #000000;">
                                        Term</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 30%; text-align: right;">
                                        <asp:Label ID="lblTerm" runat="server" ForeColor="Black"></asp:Label></div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 55%; color: #000000;">
                                        Cash price</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 30%; text-align: right;">
                                        <asp:Label ID="lblCashPrice" runat="server" ForeColor="Black"></asp:Label></div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px; background-color: #EFF3FB;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 55%; color: #000000;">
                                        Amount finance</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 30%; text-align: right;">
                                        <asp:Label ID="lblAmtFin" runat="server" ForeColor="Black"></asp:Label></div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 55%; color: #000000;">
                                        Interest amount</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 30%; text-align: right;">
                                        <asp:Label ID="lblIntAmt" runat="server" ForeColor="Black"></asp:Label></div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px; background-color: #EFF3FB;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 55%; color: #000000;">
                                        Total hire value</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 30%; text-align: right;">
                                        <asp:Label ID="lblTotHire" runat="server" ForeColor="Black"></asp:Label></div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 55%; color: #000000;">
                                        Commission Rate</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 30%; text-align: right;">
                                        <asp:Label ID="lblComRate" runat="server" ForeColor="Black"></asp:Label>
                                    </div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px; background-color: #EFF3FB;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 55%; color: #000000;">
                                        Commission amount</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 30%; text-align: right;">
                                        <asp:Label ID="lblComAmt" runat="server" ForeColor="Black"></asp:Label></div>
                                </div>
                                <div style="float: right; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 55%; color: #000000;">
                                        Discount</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 10%; text-align: right;">
                                        <asp:Label ID="lblDisRate" runat="server" ForeColor="Black" Text="10"></asp:Label>
                                    </div>
                                    <div style="float: left; width: 20%; text-align: right;">
                                        <asp:Label ID="lblDisAmt" runat="server" ForeColor="Black" Text="1000"></asp:Label>
                                    </div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px; background-color: #EFF3FB;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 55%; color: #000000;">
                                        Total cash</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 30%; text-align: right;">
                                        <asp:Label ID="lblTotCash" runat="server" ForeColor="Black"></asp:Label></div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 55%; color: #000000;">
                                        Customer payment</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 30%; text-align: right;">
                                        <asp:TextBox ID="txtCusPay" runat="server" ForeColor="Black" CssClass="TextBox" Style="text-align: right;"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 0.5%;">
                                        &nbsp;</div>
                                    <div style="float: left; text-align: center;">
                                        <asp:Button ID="btnCal" runat="server" Text="Re-Cal" BorderStyle="Solid" CssClass="Button"
                                            OnClick="btnCal_Click" />
                                    </div>
                                </div>
                            </div>
                            <div style="float: right; width: 49.5%; padding-top: 1px; padding-bottom: 1px; padding-right: 1px;">
                                <div style="float: left; width: 50%; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 50%; color: #000000;">
                                        Insuarance Company</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 40%; text-align: right;">
                                        <asp:TextBox ID="txtInsCom" runat="server" Width="75%" Style="margin-left: 1%" CssClass="TextBox"></asp:TextBox><asp:ImageButton
                                            ID="imgInsCom" runat="server" ImageUrl="~/Images/icon_search.png" OnClick="imgInsCom_Click" />
                                    </div>
                                </div>
                                <div style="float: left; width: 49%; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 40%; color: #000000;">
                                        Policy</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 40%; text-align: right;">
                                        <asp:TextBox ID="txtInsPol" runat="server" Width="75%" Style="margin-left: 1%" CssClass="TextBox"></asp:TextBox><asp:ImageButton
                                            ID="imgInsPol" runat="server" ImageUrl="~/Images/icon_search.png" OnClick="imgPol_Click" />
                                    </div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px; background-color: #EFF3FB;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 90%; color: #000000; font-weight: bold;">
                                        :: INITIAL PAYMENT ::</div>
                                </div>
                                <div style="float: right; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 55%; color: #000000;">
                                        Down Payment</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 30%; text-align: right;">
                                        <asp:Label ID="lblDPay" runat="server" ForeColor="Black"></asp:Label>
                                    </div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px; background-color: #EFF3FB;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 55%; color: #000000;">
                                        VAT amount</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 30%; text-align: right;">
                                        <asp:Label ID="lblVat" runat="server" ForeColor="Black"></asp:Label></div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 55%; color: #000000;">
                                        Service charge</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 30%; text-align: right;">
                                        <asp:Label ID="lblService" runat="server" ForeColor="Black"></asp:Label></div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px; background-color: #EFF3FB;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 55%; color: #000000;">
                                        Diriya amount</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 30%; text-align: right;">
                                        <asp:Label ID="lblInsu" runat="server" ForeColor="Black"></asp:Label></div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 55%; color: #000000;">
                                        Additional rental</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 30%; text-align: right;">
                                        <asp:Label ID="lblAddRent" runat="server" ForeColor="Black"></asp:Label></div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px; background-color: #EFF3FB;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 55%; color: #000000;">
                                        Stamp duty</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 30%; text-align: right;">
                                        <asp:Label ID="lblstampduty" runat="server" ForeColor="Black"></asp:Label></div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 55%; color: #000000;">
                                        Other charges</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 30%; text-align: right;">
                                        <asp:Label ID="lblTotCha" runat="server" ForeColor="Black"></asp:Label></div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px; background-color: #EFF3FB;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 55%; color: #000000; font-weight: bold;">
                                        HP initial payment</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 30%; text-align: right; font-weight: bold;">
                                        <asp:Label ID="lblTotAmt" runat="server" ForeColor="Black"></asp:Label></div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 55%; color: #000000;">
                                        Vehicle Ins. Fee
                                    </div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 30%; text-align: right;">
                                        <asp:Label ID="lblInsFee" runat="server" ForeColor="Black"></asp:Label></div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px; background-color: #EFF3FB;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 55%; color: #000000;">
                                        Registration Fee
                                    </div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 30%; text-align: right;">
                                        <asp:Label ID="lblRegFee" runat="server" ForeColor="Black"></asp:Label></div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 55%; color: #000000; font-weight: bold;">
                                        Total payable amount</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 30%; text-align: right; font-weight: bold;">
                                        <asp:Label ID="lblTobePay" runat="server" ForeColor="Black"></asp:Label></div>
                                </div>
                            </div>
                            <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                <div style="float: left; width: 70%; height: 175px; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 99%; background-color: #507CD1; border-right: 1px solid white;
                                        color: White; height: 15px;">
                                        Monthly Payment Schdule
                                    </div>
                                    <div style="float: left; width: 100%; height: 2px">
                                    </div>
                                    <asp:Panel ID="pnlShedule" runat="server" ScrollBars="Auto" BorderColor="#9F9F9F"
                                        BorderWidth="1px" Font-Bold="true" Width="98%" Height="175px">
                                        <asp:GridView ID="gvSchedule" CssClass="GridView" runat="server" AutoGenerateColumns="False"
                                            Font-Bold="False" CellPadding="4" ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True">
                                            <AlternatingRowStyle BackColor="White" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <EmptyDataTemplate>
                                                <div style="width: 500px; text-align: center;">
                                                    No data found.
                                                </div>
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="hts_seq" Visible="false" />
                                                <asp:BoundField DataField="hts_acc_no" Visible="false" />
                                                <asp:BoundField DataField="hts_rnt_no" HeaderText="Line">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="hts_due_dt" HeaderText="Due date" DataFormatString="{0:d}">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="hts_ins" HeaderText="Diriya amount" DataFormatString="{0:F}">
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Width="12%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="hts_vat" HeaderText="Tax amount" DataFormatString="{0:F}">
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Width="12%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="hts_sdt" HeaderText="Stamp duty" DataFormatString="{0:F}">
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Width="12%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="hts_ser" HeaderText="Service Chag." DataFormatString="{0:F}">
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Width="12%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="hts_rnt_val" HeaderText="Amount" DataFormatString="{0:F}">
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Width="12%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="hts_veh_insu" HeaderText="Insuarance" DataFormatString="{0:F}">
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Width="12%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="hts_tot_val" HeaderText="Total" DataFormatString="{0:F}">
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Width="12%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="hts_intr" Visible="false" />
                                                <asp:BoundField DataField="hts_cre_by" Visible="false" />
                                                <asp:BoundField DataField="hts_cre_dt" Visible="false" />
                                            </Columns>
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
                                <div style="float: right; width: 30%; height: 175px; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 100%; background-color: #507CD1; border-right: 1px solid white;
                                        color: White; height: 15px;">
                                        Other charges
                                    </div>
                                    <div style="float: left; width: 100%; height: 2px">
                                    </div>
                                    <asp:Panel ID="pnlOther" runat="server" ScrollBars="Auto" BorderColor="#9F9F9F" BorderWidth="1px"
                                        Font-Bold="true" Width="98%" Height="175px">
                                        <asp:GridView ID="gvOther" CssClass="GridView" runat="server" AutoGenerateColumns="False"
                                            Font-Bold="False" CellPadding="4" ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True"
                                            Width="98%">
                                            <AlternatingRowStyle BackColor="White" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <EmptyDataTemplate>
                                                <div style="width: 350px; text-align: center;">
                                                    No data found.
                                                </div>
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="hoc_seq" Visible="false" />
                                                <asp:BoundField DataField="hoc_sch_cd" Visible="false" />
                                                <asp:BoundField DataField="hoc_tp" HeaderText="Code">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" Width="20%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="hoc_desc" HeaderText="Description">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" Width="50%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="hoc_from_dt" Visible="false" />
                                                <asp:BoundField DataField="hoc_to_dt" Visible="false" />
                                                <asp:BoundField DataField="hoc_comm_cat" Visible="false" />
                                                <asp:BoundField DataField="hoc_cus_cd" Visible="false" />
                                                <asp:BoundField DataField="hoc_pb" Visible="false" />
                                                <asp:BoundField DataField="hoc_pb_lvl" Visible="false" />
                                                <asp:BoundField DataField="hoc_brd" Visible="false" />
                                                <asp:BoundField DataField="hoc_main_cat" Visible="false" />
                                                <asp:BoundField DataField="hoc_cat" Visible="false" />
                                                <asp:BoundField DataField="hoc_itm" Visible="false" />
                                                <asp:BoundField DataField="hoc_ser" Visible="false" />
                                                <asp:BoundField DataField="hoc_pro" Visible="false" />
                                                <asp:BoundField DataField="hoc_val" HeaderText="Amount" DataFormatString="{0:F}">
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Width="30%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="hoc_cre_by" Visible="false" />
                                                <asp:BoundField DataField="hoc_cre_dt" Visible="false" />
                                            </Columns>
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
                                    <div style="float: right; width: 100%; padding-top: 1.5px; padding-bottom: 2px; background-color: #FFFFFF;
                                        text-align: right; padding-right: 30px;">
                                        <asp:Label ID="Label3" runat="server" ForeColor="Black" Text="Total :"></asp:Label><asp:Label
                                            ID="lblOtherTotal" runat="server" ForeColor="Black" Text="total" Width="50px"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <div style="display: none;">
                        <asp:Button ID="btnItem" runat="server" OnClick="CheckValidItem" />
                        <asp:Button ID="btnItemSelect" runat="server" OnClick="LoadItem" />
                        <asp:Button ID="btnQty" runat="server" OnClick="CheckValidQty" />
                        <asp:Button ID="btnInsuCom" runat="server" OnClick="CheckValidInsuCom" />
                        <asp:Button ID="btnPolicy" runat="server" OnClick="CheckValidPolicy" />
                    </div>
                </div>
                <%--item selecting area--%>
                <div style="float: left; width: 100%; padding-top: 2px; padding-bottom: 2px;">
                    <%-- Collaps Header - Items --%>
                    <div style="height: 18px; background-color: #1E4A9F; color: White; width: 98%; float: left;
                        margin-top: 6px;">
                        Customer and guranter details</div>
                    <%-- Collaps Image - Items --%>
                    <div style="float: left; margin-top: 6px;">
                        <asp:Image runat="server" ID="Image3" ImageUrl="~/Images/16 X 16 DownArrow.jpg" ImageAlign="Right" /></div>
                    <%-- Collaps control - Items --%>
                    <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server" TargetControlID="pnlCus"
                        CollapsedSize="0" ExpandedSize="575" Collapsed="True" ExpandControlID="Image3"
                        CollapseControlID="Image3" AutoCollapse="False" AutoExpand="False" ScrollContents="false"
                        ExpandDirection="Vertical" ImageControlID="Image3" ExpandedImage="~/Images/16 X 16 UpArrow.jpg"
                        CollapsedImage="~/Images/16 X 16 DownArrow.jpg">
                    </asp:CollapsiblePanelExtender>
                    <div style="width: 100%; float: left; padding-top: 2px; padding-bottom: 2px;">
                        <asp:Panel runat="server" ID="pnlCus" Width="99.8%" BorderColor="#9F9F9F" BorderWidth="1px">
                            <div style="width: 100%; float: right; padding-top: 2px; padding-bottom: 2px;">
                                <asp:RadioButton ID="rbCus" Text="Customer Details" runat="server" TextAlign="Left"
                                    GroupName="Radio" />
                                <asp:RadioButton ID="rbGur" Text="Gurantor Details" runat="server" TextAlign="Left"
                                    GroupName="Radio" />
                                <asp:Button ID="btnCusCreate" runat="server" Text="Create" BorderStyle="Solid" CssClass="Button"
                                    OnClick="btnCreate_Click" />
                                <div style="float: right; width: 30%; visibility: hidden;">
                                    <asp:TextBox ID="txtHiddenCustCD" runat="server" OnTextChanged="txtHiddenCustCD_TextChanged"></asp:TextBox>
                                </div>
                            </div>
                            <div style="width: 60%; float: left; padding-top: 2px; padding-bottom: 2px;">
                                <%--<div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 15%; color: #000000;">
                                        NIC #</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 50%; text-align: left;">
                                        <asp:TextBox ID="txtNIC" runat="server" CssClass="TextBox" MaxLength="10"></asp:TextBox>
                                    </div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 15%; color: #000000;">
                                        Passport #</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 50%; text-align: left;">
                                        <asp:TextBox ID="txtPass" runat="server" CssClass="TextBox"></asp:TextBox>
                                    </div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 15%; color: #000000;">
                                        Name</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 80%; text-align: left;">
                                        <asp:TextBox ID="txtcusName" runat="server" CssClass="TextBox" Width="100%"></asp:TextBox>
                                    </div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 15%; color: #000000;">
                                        Address</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 80%; text-align: left;">
                                        <asp:TextBox ID="txtAdd1" runat="server" CssClass="TextBox" Width="100%"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 80%; text-align: left; margin-left: 17%; padding-top: 2px">
                                        <asp:TextBox ID="txtAdd2" runat="server" CssClass="TextBox" Width="100%"></asp:TextBox>
                                    </div>
                                </div>--%>
                                <div>
                                    <uc1:uc_CustomerCreation ID="cusCreP1" runat="server" />
                                </div>
                                <div>
                                    <uc2:uc_CustCreationExternalDet ID="cusCreP2" runat="server" />
                                </div>
                            </div>
                            <div style="width: 38%; float: right; padding-top: 2px; padding-bottom: 2px;">
                                <div style="float: left; width: 100%; background-color: #507CD1; border-right: 1px solid white;
                                    color: White; height: 15px;">
                                    Customer Details
                                </div>
                                <div style="float: left; width: 100%; height: 2px">
                                </div>
                                <asp:Panel ID="pnlCusDetails" runat="server" ScrollBars="Auto" BorderColor="#9F9F9F"
                                    BorderWidth="1px" Font-Bold="true" Width="99%" Height="100px">
                                    <asp:GridView ID="gvCus" CssClass="GridView" runat="server" AutoGenerateColumns="False"
                                        Font-Bold="False" CellPadding="4" ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True"
                                        OnRowDeleting="OnRemoveFromCusDetails" DataKeyNames="mbe_cd" Width="98%">
                                        <AlternatingRowStyle BackColor="White" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <EmptyDataTemplate>
                                            <div style="width: 100px; text-align: center;">
                                                No data found.
                                            </div>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField DataField="mbe_cd" HeaderText="Code">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="mbe_name" HeaderText="Name">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="50%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="mbe_nic" HeaderText="NIC">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="20%" />
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgBtnGridDelete" runat="server" ImageUrl="~/Images/Delete.png"
                                                        Width="13px" Height="13px" CommandName="Delete" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
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
                                <div style="float: left; width: 100%; padding-top : 5px; height: 18px">
                                    <asp:Button ID="btnClearCus" runat="server" Text="Clear Customer" BorderStyle="Solid"
                                        CssClass="Button"  OnClick="btnClearCus_Click"/>
                                </div>
                            </div>
                            <div style="width: 38%; float: right; padding-top: 2px; padding-bottom: 2px;">
                                <div style="float: left; width: 100%; background-color: #507CD1; border-right: 1px solid white;
                                    color: White; height: 15px;">
                                    Gurentor Details
                                </div>
                                <div style="float: left; width: 100%; height: 2px">
                                </div>
                                <asp:Panel ID="pnlGur" runat="server" ScrollBars="Auto" BorderColor="#9F9F9F" BorderWidth="1px"
                                    Font-Bold="true" Width="99%" Height="200px">
                                    <asp:GridView ID="gvGur" CssClass="GridView" runat="server" AutoGenerateColumns="False"
                                        Font-Bold="False" CellPadding="4" ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True"
                                        OnRowDeleting="OnRemoveFromGurDetails" DataKeyNames="mbe_cd" Width="98%">
                                        <AlternatingRowStyle BackColor="White" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <EmptyDataTemplate>
                                            <div style="width: 100px; text-align: center;">
                                                No data found.
                                            </div>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField DataField="mbe_cd" HeaderText="Code">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="mbe_name" HeaderText="Name">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="50%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="mbe_nic" HeaderText="NIC">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="20%" />
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgBtnGridDelete" runat="server" ImageUrl="~/Images/Delete.png"
                                                        Width="13px" Height="13px" CommandName="Delete" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
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
                                <div style="float: left; width: 100%; padding-top :5px; height: 18px" >
                                    <asp:Button ID="btnClearGur" runat="server" Text="Clear Gurentors" BorderStyle="Solid"
                                        CssClass="Button" OnClick = "btnClearGur_Click"/>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <%--item selecting area--%>
                <div style="float: left; width: 100%; padding-top: 2px; padding-bottom: 2px;">
                    <%-- Collaps Header - Items --%>
                    <div style="height: 18px; background-color: #1E4A9F; color: White; width: 98%; float: left;
                        margin-top: 6px;">
                        Proof documents & other details</div>
                    <%-- Collaps Image - Items --%>
                    <div style="float: left; margin-top: 6px;">
                        <asp:Image runat="server" ID="Image4" ImageUrl="~/Images/16 X 16 DownArrow.jpg" ImageAlign="Right" /></div>
                    <%-- Collaps control - Items --%>
                    <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" runat="server" TargetControlID="pnlProof"
                        CollapsedSize="0" ExpandedSize="375" Collapsed="True" ExpandControlID="Image4"
                        CollapseControlID="Image4" AutoCollapse="False" AutoExpand="False" ScrollContents="false"
                        ExpandDirection="Vertical" ImageControlID="Image4" ExpandedImage="~/Images/16 X 16 UpArrow.jpg"
                        CollapsedImage="~/Images/16 X 16 DownArrow.jpg">
                    </asp:CollapsiblePanelExtender>
                    <div style="width: 100%; float: left; padding-top: 2px; padding-bottom: 2px;">
                        <asp:Panel runat="server" ID="pnlProof" Width="99.8%" BorderColor="#9F9F9F" BorderWidth="1px">
                            <div style="width: 50%; float: left; padding-top: 2px; padding-bottom: 2px; height: 150px;">
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 15%; color: #000000;">
                                        Group sales</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 80%; text-align: left;">
                                        <asp:DropDownList ID="ddlGroup" runat="server" CssClass="ComboBox" Width="120px"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 15%; color: #000000;">
                                        Sales Person</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 80%; text-align: left;">
                                        <asp:TextBox ID="txtEx" runat="server" CssClass="TextBox" Width="120px" 
                                            MaxLength="10"></asp:TextBox>
                                    </div>
                                    <div style="float: left; margin-left: 17%; width: 80%; text-align: left;">
                                        <asp:Label ID="lblExName" runat="server" ForeColor="Black" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div style="width: 50%; float: right; padding-top: 2px; padding-bottom: 2px; height: 150px;">
                                <div style="float: left; width: 100%; background-color: #507CD1; border-right: 1px solid white;
                                    color: White; height: 15px;">
                                    Proof documents
                                    <div style="float: left; width: 100%; height: 2px">
                                    </div>
                                    <div style="width: 100%; float: left; padding-top: 2px; padding-bottom: 2px;">
                                        <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" BorderColor="#9F9F9F" BorderWidth="1px"
                                            Font-Bold="true" Width="99%" Height="145px">
                                            <asp:GridView ID="gvProof" CssClass="GridView" runat="server" AutoGenerateColumns="False"
                                                Font-Bold="False" CellPadding="4" ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True"
                                                Width="98%">
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                            <div style="width: 49%; float: left; padding-top: 2px; padding-bottom: 2px; height: 150px;">
                                <div style="float: left; width: 100%; background-color: #507CD1; border-right: 1px solid white;
                                    color: White; height: 15px;">
                                    Present Address
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 15%; color: #000000;">
                                        Code</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 80%; text-align: left;">
                                        <asp:TextBox ID="txtPreCode" runat="server" CssClass="TextBox" Width="175px" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 15%; color: #000000;">
                                        Name</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 80%; text-align: left;">
                                        <asp:TextBox ID="txtPreName" runat="server" CssClass="TextBox" Width="375px" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 15%; color: #000000;">
                                        Address</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 80%; text-align: left;">
                                        <asp:TextBox ID="txtPreAdd1" runat="server" CssClass="TextBox" Width="375px"></asp:TextBox>
                                    </div>
                                    <div style="float: left; margin-left: 17%; width: 80%; text-align: left; padding-top: 1px;">
                                        <asp:TextBox ID="txtPreAdd2" runat="server" CssClass="TextBox" Width="375px"></asp:TextBox>
                                    </div>
                                    <div style="float: left; margin-left: 17%; width: 80%; text-align: left; padding-top: 1px;">
                                        <asp:TextBox ID="txtPreAdd3" runat="server" CssClass="TextBox" Width="375px"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div style="width: 50%; float: right; padding-top: 2px; padding-bottom: 2px; height: 150px;">
                                <div style="float: left; width: 100%; background-color: #507CD1; border-right: 1px solid white;
                                    color: White; height: 15px;">
                                    Product available Address
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 15%; color: #000000;">
                                        Address</div>
                                    <div style="float: left; width: 1%; color: #000000;">
                                        :</div>
                                    <div style="float: left; width: 80%; text-align: left;">
                                        <asp:TextBox ID="txtProAdd1" runat="server" CssClass="TextBox" Width="375px"></asp:TextBox>
                                    </div>
                                    <div style="float: left; margin-left: 17%; width: 80%; text-align: left; padding-top: 1px;">
                                        <asp:TextBox ID="txtProAdd2" runat="server" CssClass="TextBox" Width="375px"></asp:TextBox>
                                    </div>
                                    <div style="float: left; margin-left: 17%; width: 80%; text-align: left; padding-top: 1px;">
                                        <asp:TextBox ID="txtProAdd3" runat="server" CssClass="TextBox" Width="375px"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <%--item selecting area--%>
                <div style="float: left; width: 100%; padding-top: 2px; padding-bottom: 2px;">
                    <%-- Collaps Header - Items --%>
                    <div style="height: 18px; background-color: #1E4A9F; color: White; width: 98%; float: left;
                        margin-top: 6px;">
                        Payment details</div>
                    <%-- Collaps Image - Items --%>
                    <div style="float: left; margin-top: 6px;">
                        <asp:Image runat="server" ID="Image5" ImageUrl="~/Images/16 X 16 DownArrow.jpg" ImageAlign="Right" /></div>
                    <%-- Collaps control - Items --%>
                    <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender4" runat="server" TargetControlID="pnlRec"
                        CollapsedSize="0" ExpandedSize="375" Collapsed="True" ExpandControlID="Image5"
                        CollapseControlID="Image5" AutoCollapse="False" AutoExpand="False" ScrollContents="false"
                        ExpandDirection="Vertical" ImageControlID="Image5" ExpandedImage="~/Images/16 X 16 UpArrow.jpg"
                        CollapsedImage="~/Images/16 X 16 DownArrow.jpg">
                    </asp:CollapsiblePanelExtender>
                    <div style="width: 100%; float: left; padding-top: 2px; padding-bottom: 2px;">
                        <asp:Panel runat="server" ID="pnlRec" Width="99.8%" BorderColor="#9F9F9F" BorderWidth="1px">
                            <div style="float: left; width: 100%;">
                                <div style="float: left; width: 2%;">
                                    &nbsp;</div>
                                <div style="padding: 1px; float: left; width: 50%;">
                                    <div style="float: left; width: 10%; text-align: left;">
                                        &nbsp;Type:</div>
                                    <div style="float: left; width: 18%;">
                                        &nbsp;
                                        <asp:RadioButton ID="rdoBtnSystem" runat="server" Text="System" GroupName="Rdo_Type"
                                            AutoPostBack="True" Checked="True" OnCheckedChanged="rdoBtnSystem_CheckedChanged" />
                                    </div>
                                    <div style="float: left; width: 18%;">
                                        &nbsp;
                                        <asp:RadioButton ID="rdoBtnManual" runat="server" Text="Manual" GroupName="Rdo_Type"
                                            AutoPostBack="True" OnCheckedChanged="rdoBtnManual_CheckedChanged" />
                                    </div>
                                    <div style="float: left; width: 30%; text-align: left;">
                                        &nbsp;Installment type :
                                        <asp:DropDownList ID="ddlinsType" runat="server" Width="133px" CssClass="ComboBox"
                                            OnSelectedIndexChanged="ddlinsType_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>Total Cash</asp:ListItem>
                                            <asp:ListItem>Additional Rental</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left; width: 30%; text-align: left;">
                                        &nbsp;Amount :
                                        <asp:Label runat="server" ID="lblCashAmt" Text="0.00"></asp:Label></div>
                                    <div style="float: left; width: 30%; text-align: left;">
                                        &nbsp;Balance :
                                        <asp:Label runat="server" ID="lblCashBal" Text="0.00"></asp:Label></div>
                                </div>
                                <div style="float: left; width: 2%;">
                                    &nbsp;</div>
                            </div>
                            <div style="float: left; width: 100%; font-weight: normal; padding-top: 1px; padding-bottom: 1px;
                                height: 15px;">
                                <div style="float: left; width: 4px; border-right: 1px solid white; color: White;
                                    height: 15px;">
                                </div>
                                <div style="float: left; width: 72px; background-color: #507CD1; text-align: center;
                                    border-right: 1px solid white; color: White; height: 15px;">
                                    <%-- VAT Amt--%>
                                    Prefix</div>
                                <div style="float: left; width: 97px; background-color: #507CD1; text-align: center;
                                    border-right: 1px solid white; color: White; height: 15px;">
                                    <%--Amt--%>
                                    Receipt No
                                </div>
                                <div style="float: left; width: 71px; background-color: #507CD1; text-align: center;
                                    border-right: 1px solid white; color: White; height: 15px;">
                                    <%--Book--%>
                                    Amount</div>
                                <div style="float: left; width: 1px; background-color: #507CD1; text-align: center;
                                    border-right: 1px solid white; color: White; height: 15px;">
                                </div>
                                <asp:Button ID="btnDeleteLast" runat="server" CssClass="Button" Font-Size="Smaller"
                                    Height="15px" OnClick="btnDeleteLast_Click" Text="Delete last" Width="70px" />
                                &nbsp; &nbsp; &nbsp; &nbsp;
                                <%-- <asp:Button ID="btnCancelRecipt" runat="server" CssClass="Button" Height="15px" Text="cancel receipt"
                                    Font-Size="Smaller" OnClick="btnCancelRecipt_Click" />--%>
                            </div>
                            <div style="float: left; width: 60%; font-weight: normal; padding-top: 0px; padding-bottom: 0px;">
                                <div style="float: left; width: 4px; border-right: 1px solid white;">
                                    &nbsp;</div>
                                <%-- <div style="float: left; width: 99px; border-right: 1px solid white;">
                                    <asp:TextBox runat="server" ID="txtItem" CssClass="TextBoxUpper" ClientIDMode="Static"
                                        Width="95%"></asp:TextBox></div>--%>
                                <%--  <div style="float: left; width: 170px; border-right: 1px solid white;">
                                    <asp:TextBox runat="server" ID="txtDescription" BackColor="#EEEEEE" BorderWidth="0px"
                                        ClientIDMode="Static" Width="95%" Font-Size="10px"></asp:TextBox></div>--%>
                                <div style="float: left; width: 72px; border-right: 1px solid white;">
                                    <asp:DropDownList ID="ddlPrefix" runat="server" CssClass="ComboBox" Width="72px">
                                    </asp:DropDownList>
                                </div>
                                <div style="float: left; width: 78px; text-align: right; border-right: 1px solid white;
                                    height: 15px;">
                                    <asp:TextBox runat="server" ID="txtReceiptNo" CssClass="TextBox" Style="text-align: right;"
                                        Width="95.4%"></asp:TextBox>
                                    &nbsp;</div>
                                <div style="float: left; width: 16px; text-align: left; border-right: 1px solid white;
                                    height: 15px;">
                                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/icon_search.png" />
                                </div>
                                <div style="float: left; width: 72px; text-align: right; border-right: 1px solid white;">
                                    <asp:TextBox runat="server" ID="txtReciptAmount" CssClass="TextBox" Style="text-align: right;"
                                        Width="95.4%"></asp:TextBox></div>
                                <div style="float: left; width: 77px; text-align: left; border-right: 1px solid white;">
                                    <%-- <asp:TextBox runat="server" ID="txtDiscountAmt" CssClass="TextBox" Style="text-align: right;"
                                        Width="94.4%"></asp:TextBox>--%>
                                    <asp:ImageButton ID="ImgBtnAddReceipt" runat="server" ImageUrl="~/Images/download_arrow_icon.png"
                                        Width="16px" OnClick="ImgBtnAddReceipt_Click" />
                                </div>
                                <%--  <div style="float: left; width: 72px; text-align: right; border-right: 1px solid white;">
                                    </div>
                                <div style="float: left; width: 72px; text-align: right; border-right: 1px solid white;">
                                    </div>
                                <div style="float: left; width: 1%;">
                                    &nbsp;
                                  
                                </div>--%>
                                <div style="float: left; width: 96%; height: 95px;">
                                    <asp:Panel ID="Panel_ReceiptDet" runat="server" Height="89px" ScrollBars="Vertical">
                                        <asp:GridView ID="gvReceipts" runat="server" AutoGenerateColumns="False" CellPadding="1"
                                            CssClass="GridView" DataKeyNames="Sar_manual_ref_no,Sar_prefix,Sar_tot_settle_amt"
                                            ForeColor="#333333" GridLines="Horizontal" OnRowDeleting="gvReceipts_RowDeleting"
                                            ShowHeader="False" Width="235px">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <%--<EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                        </EditItemTemplate>--%>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImgBtnDelRecipt" runat="server" CommandName="Delete" ImageUrl="~/Images/Delete.png"
                                                            Visible="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Sar_prefix" HeaderText="Prefix" />
                                                <asp:BoundField DataField="Sar_manual_ref_no" HeaderText="Receipt No" />
                                                <asp:BoundField DataField="Sar_tot_settle_amt" HeaderText="Amount" ItemStyle-HorizontalAlign="Right" />
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
                            <div style="float: left; width: 2%;">
                                &nbsp;</div>
                            <div style="float: left; width: 100%; padding-top: 3px">
                                <asp:Panel ID="pnlPayment" runat="server" Height="171px">
                                    <div style="float: left; width: 50%;">
                                        <div style="float: left; width: 100%;">
                                            <div style="float: left; width: 1%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 15%;">
                                                Pay Mode
                                            </div>
                                            <div style="float: left; width: 25%;">
                                                <asp:DropDownList ID="ddlPayMode" runat="server" Width="80%" CssClass="ComboBox"
                                                    OnSelectedIndexChanged="PaymentType_LostFocus" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div>
                                            <div style="float: left; width: 1%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 10%;">
                                                Amount
                                            </div>
                                            <div style="float: left; width: 35%;">
                                                <asp:TextBox ID="txtPayAmount" runat="server" CssClass="TextBoxUpper" Width="50%"
                                                    onkeypress="return isNumberKeyAndDot(event,value)"></asp:TextBox>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnPayAdd" runat="server" CssClass="Button" Text="Add" OnClick="AddPayment" />
                                            </div>
                                        </div>
                                        <div style="float: left; width: 100%; padding: 2px 2px 0px 0px;">
                                            <div style="float: left; width: 1%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 15%;">
                                                Remarks
                                            </div>
                                            <div style="float: left; width: 75%;">
                                                <asp:TextBox ID="txtPayRemarks" runat="server" CssClass="TextBox" Width="100%" TextMode="MultiLine"
                                                    Rows="2"></asp:TextBox></div>
                                        </div>
                                        <div style="float: left; width: 100%; padding: 2px 2px 0px 0px;">
                                            <%--Credit/Cheque/Bank Slip payment--%>
                                            <div style="float: left; width: 100%; height: 100px;" runat="server" id="divCredit"
                                                visible="false">
                                                <div style="float: left; width: 100%;" id="divCRDno" runat="server">
                                                    <div style="float: left; width: 1%;">
                                                        &nbsp;</div>
                                                    <div style="float: left; width: 15%;">
                                                        Card No</div>
                                                    <div style="float: left; width: 75%; padding-bottom: 2px;">
                                                        <asp:TextBox runat="server" ID="txtPayCrCardNo" CssClass="TextBox" Width="80%"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div style="float: left; width: 100%;" id="divBankDet" runat="server">
                                                    <div style="float: left; width: 1%;">
                                                        &nbsp;</div>
                                                    <div style="float: left; width: 15%;">
                                                        Bank
                                                    </div>
                                                    <div style="float: left; width: 20%; padding-bottom: 2px;">
                                                        <asp:TextBox runat="server" ID="txtPayCrBank" CssClass="TextBox" Width="35%"></asp:TextBox>
                                                        <asp:ImageButton ID="ImgBtnBankSearch" runat="server" ImageUrl="~/Images/icon_search.png"
                                                            OnClick="ImgBtnBankSearch_Click" />
                                                    </div>
                                                    <div style="float: left; width: 1%;">
                                                        &nbsp;</div>
                                                    <div style="float: left; width: 15%;">
                                                        Branch
                                                    </div>
                                                    <div style="float: left; width: 20%; padding-bottom: 2px;">
                                                        <asp:TextBox runat="server" ID="txtPayCrBranch" CssClass="TextBox" Width="70%"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div style="float: left; width: 100%;" id="divCreditCard" runat="server">
                                                    <div style="float: left; width: 100%;" id="divCardDet" runat="server">
                                                        <div style="float: left; width: 1%;">
                                                            &nbsp;</div>
                                                        <div style="float: left; width: 15%;">
                                                            Card Type
                                                        </div>
                                                        <div style="float: left; width: 15%; padding-bottom: 2px;">
                                                            <asp:TextBox runat="server" ID="txtPayCrCardType" CssClass="TextBox" Width="90%"></asp:TextBox>
                                                        </div>
                                                        <div style="float: left; width: 1%;">
                                                            &nbsp;</div>
                                                        <div style="float: left; width: 15%;">
                                                            Expiry Date
                                                        </div>
                                                        <div style="float: left; width: 25%; padding-bottom: 2px;">
                                                            <asp:TextBox runat="server" ID="txtPayCrExpiryDate" CssClass="TextBox" Width="70%"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                TargetControlID="txtPayCrExpiryDate">
                                                            </asp:CalendarExtender>
                                                            &nbsp;<asp:Image ID="btnImgCrExpiryDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png"
                                                                ImageAlign="Middle" />
                                                        </div>
                                                    </div>
                                                    <div style="float: left; width: 100%;">
                                                        <div style="float: left; width: 1%;">
                                                            &nbsp;</div>
                                                        <div style="float: left; width: 15%;">
                                                            Promotion
                                                        </div>
                                                        <div style="float: left; width: 75%; padding-bottom: 2px;">
                                                            <asp:CheckBox runat="server" ID="chkPayCrPromotion" />
                                                            &nbsp; Period &nbsp;
                                                            <asp:TextBox runat="server" ID="txtPayCrPeriod" CssClass="TextBoxNumeric" Width="20%"></asp:TextBox>
                                                            months
                                                        </div>
                                                    </div>
                                                    <div style="float: left; width: 100%;" id="divCredBatch">
                                                        <div style="float: left; width: 1%;">
                                                            &nbsp;</div>
                                                        <div style="float: left; width: 15%;">
                                                            Batch No
                                                        </div>
                                                        <div style="float: left; width: 75%; padding-bottom: 2px;">
                                                            <asp:TextBox runat="server" ID="txtPayCrBatchNo" CssClass="TextBox" Width="80%"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div style="float: left; width: 100%;" id="divChequeNum" runat="server">
                                                    <div style="float: left; width: 1%;">
                                                        &nbsp;</div>
                                                    <div style="float: left; width: 15%;">
                                                        Cheque No
                                                    </div>
                                                    <div style="float: left; width: 75%; padding-bottom: 2px;">
                                                        <asp:TextBox runat="server" ID="txtChequeNo" CssClass="TextBox" Width="80%"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <%--Advance receipt/Credit Note payment--%>
                                            <div style="float: left; width: 100%;" runat="server" id="divAdvReceipt" visible="false">
                                                <div style="float: left; width: 100%;">
                                                    <div style="float: left; width: 1%;">
                                                        &nbsp;</div>
                                                    <div style="float: left; width: 20%;">
                                                        Referance</div>
                                                    <div style="float: left; width: 75%; padding-bottom: 2px;">
                                                        <asp:TextBox runat="server" ID="txtPayAdvReceiptNo" CssClass="TextBox" Width="80%"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div style="float: left; width: 100%;">
                                                    <div style="float: left; width: 1%;">
                                                        &nbsp;</div>
                                                    <div style="float: left; width: 20%;">
                                                        Ref. Amount</div>
                                                    <div style="float: left; width: 75%; padding-bottom: 2px;">
                                                        <asp:TextBox runat="server" ID="txtPayAdvRefAmount" CssClass="TextBox" Width="80%"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 50%;">
                                        <div style="float: left; width: 100%;">
                                            <asp:Panel ID="pnlPay" runat="server" Height="140px" ScrollBars="Auto">
                                                <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False" CssClass="GridView"
                                                    CellPadding="3" ForeColor="#333333" DataKeyNames="sard_pay_tp,sard_settle_amt"
                                                    OnRowDeleting="gvPayment_OnDelete" ShowHeaderWhenEmpty="True">
                                                    <EditRowStyle BackColor="#2461BF" />
                                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#EFF3FB" />
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <EmptyDataTemplate>
                                                        <div style="width: 100%; text-align: center;">
                                                            No data found
                                                        </div>
                                                    </EmptyDataTemplate>
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnImgDelete" runat="server" ImageAlign="Middle" ImageUrl="~/Images/Delete.png"
                                                                    Width="10px" Height="10px" CommandName="Delete" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField='sard_seq_no' HeaderText='' Visible="false" />
                                                        <asp:BoundField DataField='sard_line_no' HeaderText='' Visible="false" />
                                                        <asp:BoundField DataField='sard_receipt_no' HeaderText='' Visible="false" />
                                                        <asp:BoundField DataField='sard_inv_no' HeaderText='' Visible="false" />
                                                        <asp:BoundField DataField='sard_pay_tp' HeaderText='Payment Type' ItemStyle-Width="110px"
                                                            HeaderStyle-Width="110px">
                                                            <HeaderStyle Width="110px" />
                                                            <ItemStyle Width="110px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField='sard_ref_no' HeaderText='' Visible="false" />
                                                        <asp:BoundField DataField='sard_chq_bank_cd' HeaderText='Bank' ItemStyle-Width="90px"
                                                            HeaderStyle-Width="90px">
                                                            <HeaderStyle Width="90px" />
                                                            <ItemStyle Width="90px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField='sard_chq_branch' HeaderText='Branch' ItemStyle-Width="90px"
                                                            HeaderStyle-Width="90px">
                                                            <HeaderStyle Width="90px" />
                                                            <ItemStyle Width="90px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField='sard_deposit_bank_cd' HeaderText='' Visible="false" />
                                                        <asp:BoundField DataField='sard_deposit_branch' HeaderText='' Visible="false" />
                                                        <asp:BoundField DataField='sard_credit_card_bank' HeaderText='' Visible="false" />
                                                        <asp:BoundField DataField='sard_cc_tp' HeaderText='Card Type' />
                                                        <asp:BoundField DataField='sard_cc_expiry_dt' HeaderText='Expiry Date' Visible="false" />
                                                        <asp:BoundField DataField='sard_cc_is_promo' HeaderText='Promotion' Visible="false" />
                                                        <asp:BoundField DataField='sard_cc_period' HeaderText='Period' Visible="false" />
                                                        <asp:BoundField DataField='sard_gv_issue_loc' HeaderText='' Visible="false" />
                                                        <asp:BoundField DataField='sard_gv_issue_dt' HeaderText='' Visible="false" />
                                                        <asp:BoundField DataField='sard_settle_amt' HeaderText='Amount' />
                                                        <asp:BoundField DataField='sard_sim_ser' HeaderText='' Visible="false" />
                                                        <asp:BoundField DataField='sard_receipt_no' HeaderText='receipt_no' Visible="False" />
                                                        <asp:BoundField DataField='sard_anal_3' HeaderText="Bank/Other Charges" />
                                                    </Columns>
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                </asp:GridView>
                                            </asp:Panel>
                                        </div>
                                        <div style="float: left; width: 100%;">
                                            <div style="float: left; width: 1%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 15%; background-color: #1569C7; color: White; height: 18px;">
                                                Paid Amount
                                            </div>
                                            <div style="float: left; width: 15%; text-align: right; border-color: Black; border-style: solid;
                                                border-width: 1px;">
                                                <asp:Label runat="server" ID="lblPayPaid" Text="0.00"></asp:Label>
                                            </div>
                                            <div style="float: left; width: 10%;">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 15%; background-color: #1569C7; color: White; height: 18px;">
                                                Balance
                                            </div>
                                            <div style="float: left; width: 15%; text-align: right; border-color: Black; border-style: solid;
                                                border-width: 1px;">
                                                <asp:Label runat="server" ID="lblPayBalance" Text="0.00"></asp:Label>
                                            </div>
                                            <div style="float: left; width: 1%;">
                                                &nbsp;</div>
                                            <div style="width: 12%; float: left; padding-top: 2px; padding-bottom: 2px;">
                                                <asp:Button ID="btnCreateAcc" runat="server" Text="Create Account" BorderStyle="Solid"
                                                    CssClass="Button" OnClick="btnCreateAcc_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
