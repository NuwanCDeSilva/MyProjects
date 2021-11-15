<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="PurchasingOrder.aspx.cs" Inherits="FF.WebERPClient.Purchasing_Modules.PurchasingOrder" %>

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
    <style type="text/css">
        .Textbox
        {
            margin-left: 2px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updtMainPnl" runat="server">
        <ContentTemplate>
            <div style="float: left; width: 100%; height: 549px;">
                <%--Button Panel--%>
                <div style="float: left; width: 100%; height: 22px; text-align: right;">
                    <asp:Button ID="btnApproved" runat="server" Text="Approved" Height="100%" Width="70px"
                        CssClass="Button" OnClick="btnApproved_Click" />
                    <asp:Button ID="btnSave" runat="server" Text="Save" Height="100%" Width="70px" OnClick="btnSave_Click"
                        CssClass="Button" TabIndex="10" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Height="100%" Width="70px"
                        CssClass="Button" TabIndex="11" OnClick="btnCancel_Click" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" Height="100%" Width="70px"
                        OnClick="btnClear_Click" CssClass="Button" />
                </div>
                <div style="float: left; width: 77%; height: 15px;" id="divHeader">
                    <div style="float: left; width: 100%;" id="divMain">
                        <asp:Label ID="lblSupplier" runat="server" Text="Supplier" CssClass="Textbox" Width="23%"></asp:Label>
                        <asp:Label ID="lblSupRef" runat="server" Text="Supplier Ref." Width="35%" CssClass="Textbox"> </asp:Label>
                        <asp:Label ID="lblDate" runat="server" Text="Date" Width="12%" CssClass="Textbox"></asp:Label>
                        <asp:Label ID="lblPO" runat="server" Text="Order #" Width="20%" CssClass="Textbox"></asp:Label>
                    </div>
                </div>
                <div style="float: left; width: 100%; height: 24px;" id="divHeaderEntry">
                    <div style="float: left; width: 77%; height: 24px;" id="divMainEntry">
                        <asp:TextBox ID="txtSupplier" runat="server" CssClass="TextBox" Width="20%"></asp:TextBox>
                        <asp:ImageButton ID="imgsup" runat="server" ImageUrl="~/Images/icon_search.png" OnClick="imgsup_Click" />
                        <asp:TextBox ID="txtSupRef" runat="server" CssClass="TextBox" Width="35%" TabIndex="1"></asp:TextBox>
                        <asp:TextBox ID="txtDate" runat="server" CssClass="TextBox" Width="12%" TabIndex="2"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                            Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                        <asp:TextBox ID="txtOrderNo" runat="server" CssClass="TextBox" Width="22%"></asp:TextBox>
                        <asp:ImageButton ID="imgPOSearch" runat="server" ImageUrl="~/Images/icon_search.png"
                            OnClick="imgPOSearch_Click" Style="width: 16px" />
                    </div>
                    <div style="float: left; width: 23%; height: 22px;" id="div4">
                        <asp:Label ID="lblIsCons" runat="server" Text="Consingment Order :" Width="55%"></asp:Label>
                        <asp:CheckBox ID="chkIsCons" runat="server" Width="9%" AutoPostBack="true" Height="18px"
                            OnCheckedChanged="chkIsCons_CheckedChanged"></asp:CheckBox>
                    </div>
                      <div style="float: left; width: 23%; height: 21px; padding-top: 4px;">
                        <asp:Label ID="lblCurrency" runat="server" Text="Currency :" Width="85px"></asp:Label>
                        <asp:DropDownList ID="ddlCur" runat="server" Width="100px" CssClass="ComboBox" 
                              AutoPostBack="true" onselectedindexchanged="ddlCur_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:Label ID="Label4" runat="server" Text="Ex. Rate :" Width="85px"></asp:Label>
                        <asp:Label ID="lblExRate" runat="server" Text="1" Width="75px" Style="text-align: right"></asp:Label>
                    </div>
                </div>
                <div style="height: 100px; width: 100%; float: left;">
                    <br />
                     <div style="float: left; width: 77%; height: 17px; color: #FFFFFF; background-color: #507CD1; padding-bottom : 4px">
                        <asp:Label ID="Label1" runat="server" Text="Order items" Width="189px"
                            Height="16px"></asp:Label>
                    </div>
                    <div style="float: left; width: 77%;" id="div1">
                        <asp:Label ID="lblItem" runat="server" Text="Item" Width="7%" Style="margin-left: 16px"> 
                        </asp:Label>
                        <asp:TextBox ID="txtItem" runat="server" CssClass="TextBox" Width="18%" TabIndex="3"></asp:TextBox>
                        <asp:ImageButton ID="imgItmSearch" runat="server" ImageUrl="~/Images/icon_search.png"
                            OnClick="imgItmSearch_Click" />
                        <asp:Label ID="lblStatus" runat="server" Text="Status" Width="7%"></asp:Label>
                        <asp:TextBox ID="txtItmStatus" runat="server" CssClass="TextBox" Width="12%" TabIndex="4"></asp:TextBox>
                        <asp:ImageButton ID="imgStatusSearch" runat="server" ImageUrl="~/Images/icon_search.png"
                            OnClick="imgStatusSearch_Click" />
                        <asp:Label ID="lblQty" runat="server" Text="Qty" Width="5%"></asp:Label>
                        <asp:TextBox ID="txtQty" runat="server" CssClass="TextBox" Style="text-align: right;"
                            Width="8%" TabIndex="5" onkeypress="return isNumberKeyAndDot(event,value)"></asp:TextBox>
                        <asp:Label ID="lblUnitPrice" runat="server" Text="Unit Price" Width="11%" Style="margin-left: 5px"></asp:Label>
                        <asp:TextBox ID="txtUPrice" runat="server" CssClass="TextBox" Style="text-align: right;"
                            Width="15%" TabIndex="6" onkeypress="return isNumberKeyAndDot(event,value)"></asp:TextBox>
                    </div>
                  
                    <br />
                    <br />
                    <div style="float: left; width: 77%; padding-top : 4px;" id="div2">
                        <asp:Label ID="lblValue" runat="server" Text="Amount" Width="7%" Style="margin-left: 16px"></asp:Label>
                        <asp:TextBox ID="txtAmount" runat="server" CssClass="TextBox" Style="text-align: right;"
                            Width="18%" ReadOnly="True"></asp:TextBox>
                        <asp:Label ID="lblDiscount" runat="server" Text="Discount" Width="7%" Style="margin-left: 17px"
                            Height="16px"></asp:Label>
                        <asp:TextBox ID="txtDisRate" runat="server" CssClass="TextBox" Style="text-align: right;"
                            Width="4%" TabIndex="6"></asp:TextBox>
                        <asp:TextBox ID="txtDisAmt" runat="server" CssClass="TextBox" Style="text-align: right;
                            margin-left: 0%" Width="8%" TabIndex="7"></asp:TextBox>
                        <asp:Label ID="lblTax" runat="server" Text="Tax" Width="5%" Style="margin-left: 15px"></asp:Label>
                        <asp:TextBox ID="txtTax" runat="server" CssClass="TextBox" Style="text-align: right;"
                            Width="8%" TabIndex="8"></asp:TextBox>
                        <asp:Label ID="lblTotal" runat="server" Text="Total" Width="11%" Style="margin-left: 5px"></asp:Label>
                        <asp:TextBox ID="txtTotal" runat="server" CssClass="TextBox" Style="text-align: right;"
                            Width="15%" ReadOnly="True"></asp:TextBox>
                        <br />
                        <div style="float: left; width: 100%; height: 20px; text-align: right">
                            <asp:Button ID="btnAdd" runat="server" Text="Add Item" Height="100%" Width="15%"
                                OnClick="btnAdd_Click" BorderStyle="Solid" TabIndex="9" CssClass="Button" />
                        </div>
                    </div>
                </div>
                <div style="float: left; width: 77%; height: 131px; padding-top: 2px; padding-bottom: 2px;
                    background-color: #FFFFFF;">
                    <asp:Panel ID="pnlItem" runat="server" Height="130px" ScrollBars="Auto" BorderColor="#9F9F9F"
                        BorderWidth="1px" Font-Bold="true" Width="100%">
                        <asp:GridView ID="gvPOItem" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                            Style="margin-top: 0px" GridLines="None" RowStyle-Height="10px" Width="737px"
                            EmptyDataText="No data found" ShowHeaderWhenEmpty="True" OnRowCommand="gvPOItem_RowCommand"
                            OnRowDeleting="OnRemoveFromPOItemGrid" DataKeyNames="Pod_line_no,Pod_itm_cd,pod_qty,pod_unit_price,pod_dis_amt,pod_line_tax"
                            CellPadding="4" ForeColor="#333333">
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Size="10px" Height="10px"
                                Font-Bold="True" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle Font-Size="10px" BackColor="#EFF3FB" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField='Pod_seq_no' HeaderText='' Visible="false" />
                                <asp:BoundField DataField='Pod_line_no' HeaderText='No' HeaderStyle-Width="5px" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right">
                                    <HeaderStyle HorizontalAlign="Right" Width="5px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField='Pod_itm_cd' HeaderText='Item' ItemStyle-Width="85px" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="85px" />
                                </asp:BoundField>
                                <asp:BoundField DataField='Pod_itm_stus' HeaderText='Status' HeaderStyle-Width="75px"
                                    HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" Width="75px" />
                                </asp:BoundField>
                                <asp:BoundField DataField='Pod_item_desc' HeaderText='Description' ItemStyle-Width="180px"
                                    HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="180px" />
                                </asp:BoundField>
                                <asp:BoundField DataField='Pod_itm_tp' HeaderText='' Visible="false" />
                                <asp:BoundField DataField='Pod_qty' HeaderText='Qty' HeaderStyle-Width="75px" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right">
                                    <HeaderStyle HorizontalAlign="Right" Width="75px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField='Pod_uom' HeaderText='' Visible="false" />
                                <asp:BoundField DataField='Pod_unit_price' HeaderText='Unit Price' HeaderStyle-Width="75px"
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                    <HeaderStyle HorizontalAlign="Right" Width="75px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField='Pod_line_val' HeaderText='Amount' HeaderStyle-Width="75px"
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                    <HeaderStyle HorizontalAlign="Right" Width="75px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField='Pod_dis_rt' HeaderText='Dis. Rate' HeaderStyle-Width="75px"
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                    <HeaderStyle HorizontalAlign="Right" Width="75px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField='Pod_dis_amt' HeaderText='Dis. Amt' HeaderStyle-Width="75px"
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                    <HeaderStyle HorizontalAlign="Right" Width="75px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField='Pod_line_tax' HeaderText='Tax Amt' HeaderStyle-Width="75px"
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                    <HeaderStyle HorizontalAlign="Right" Width="75px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField='Pod_line_amt' HeaderText='Tot. Amt' HeaderStyle-Width="75px"
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                    <HeaderStyle HorizontalAlign="Right" Width="75px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField='Pod_grn_bal' HeaderText='' Visible="false" />
                                <asp:BoundField DataField='Pod_act_unit_price' HeaderText='' Visible="false" />
                                <asp:BoundField DataField='Pod_kit_itm_cd' HeaderText='' Visible="false" />
                                <asp:BoundField DataField='Pod_kit_line_no' HeaderText='' Visible="false" />
                                <asp:BoundField DataField='Pod_lc_bal' HeaderText='' Visible="false" />
                                <asp:BoundField DataField='Pod_nbt' HeaderText='' Visible="false" />
                                <asp:BoundField DataField='Pod_nbt_before' HeaderText='' Visible="false" />
                                <asp:BoundField DataField='Pod_pi_bal' HeaderText='' Visible="false" />
                                <asp:BoundField DataField='Pod_ref_no' HeaderText='' Visible="false" />
                                <asp:BoundField DataField='Pod_si_bal' HeaderText='' Visible="false" />
                                <asp:BoundField DataField='Pod_tot_tax_before' HeaderText='' Visible="false" />
                                <asp:BoundField DataField='Pod_uom' HeaderText='' Visible="false" />
                                <asp:BoundField DataField='Pod_vat' HeaderText='' Visible="false" />
                                <asp:BoundField DataField='Pod_vat_before' HeaderText='' Visible="false" />
                                <%--<asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgBtnGridEdit" runat="server" ImageUrl="~/Images/EditIcon.png"  Width="13px" Height="13px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgBtnGridDelete" runat="server" ImageUrl="~/Images/Delete.png"
                                            Width="13px" Height="13px" CommandName="Delete" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                    </asp:Panel>
                </div>
                <div style="float: right; width: 22.5%; height: 80px; padding-top: 40px; background-color: #FFFFFF;">
                    <asp:Label ID="lblsubTot" runat="server" Text="Sub Total" Width="30%" Style="margin-left: 8px"></asp:Label><asp:Label
                        ID="lblSubAmt" runat="server" Text="111" Width="50%" Style="text-align: right"
                        Font-Bold="True"></asp:Label>
                    <asp:Label ID="lblDis" runat="server" Text="Discount" Width="30%" Style="margin-left: 8px"></asp:Label><asp:Label
                        ID="lblDisAmt" runat="server" Text="111" Width="50%" Style="text-align: right"
                        Font-Bold="True"></asp:Label>
                    <asp:Label ID="lblTaxs" runat="server" Text="Tax" Width="30%" Style="margin-left: 8px"></asp:Label><asp:Label
                        ID="lblTaxAmt" runat="server" Text="111" Width="50%" Style="text-align: right"
                        Font-Bold="True"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="lblTotals" runat="server" Text="Total" Width="30%" Style="margin-left: 8px"></asp:Label><asp:Label
                        ID="lblTotAmt" runat="server" Text="111" Width="50%" Style="text-align: right"
                        Font-Bold="True"></asp:Label>
                </div>
                 <div style="float: left; width: 77%; height: 17px; color: #FFFFFF; background-color: #507CD1; padding-bottom : 4px">
                        <asp:Label ID="Label2" runat="server" Text="Delivery shedule" Width="189px"
                            Height="16px"></asp:Label>
                    </div>
                     <div style="float: right; width: 22.5%; height: 17px; color: #FFFFFF; background-color: #507CD1; padding-bottom : 4px">
                        <asp:Label ID="Label3" runat="server" Text="Quotations" Width="189px"
                            Height="16px"></asp:Label>
                    </div>
                <div style="float: left; width: 77%; height: 149px; padding-top: 2px; padding-bottom: 2px;
                    background-color: #FFFFFF;">
                    <div style="float: right; width: 100%; padding-top: 2px;">
                        <asp:Label ID="l1" runat="server" Text="Seq :" Width="5%" Style="margin-left: 7px"></asp:Label>
                        <asp:Label ID="lblSeq" runat="server" Text="1" Width="4%" Style="margin-left: 7px"></asp:Label>
                        <asp:Label ID="l2" runat="server" Text="Del. Line :" Width="8%" Style="margin-left: 5px"></asp:Label>
                        <asp:Label ID="lbldelLine" runat="server" Text="1" Width="4%" Style="margin-left: 5px"></asp:Label>
                        <asp:Label ID="l3" runat="server" Text="Item :" Width="6%" Style="margin-left: 5px"></asp:Label>
                        <asp:Label ID="lblDelItem" runat="server" Text="LGDVD270" Width="15%" Style="margin-left: 5px"></asp:Label>
                        <asp:Label ID="l4" runat="server" Text="Status :" Width="7%" Style="margin-left: 5px"></asp:Label>
                        <asp:Label ID="lblDelStatus" runat="server" Text="GOD" Width="8%" Style="margin-left: 5px"></asp:Label>
                        <asp:Label ID="l5" runat="server" Text="Qty" Width="5%" Style="margin-left: 5px"></asp:Label>
                        <asp:Label ID="lblDelQty" runat="server" Text="5" Width="5%" Style="margin-left: 5px"></asp:Label>
                        <asp:Label ID="l6" runat="server" Text="Edit Qty" Width="8%" Style="margin-left: 5px"></asp:Label>
                        <asp:TextBox ID="txtEditDelQty" runat="server" CssClass="TextBox" Style="text-align: left;
                            margin-top: 5px" Width="8%"></asp:TextBox>
                    </div>
                    <div style="float: right; width: 100%; padding-top: 2px;">
                        <asp:Label ID="lblDelLoca" runat="server" Text="Location" Width="91px" Style="margin-left: 7px"></asp:Label>
                        <asp:TextBox ID="txtDelLoc" runat="server" CssClass="TextBox" Style="text-align: left;
                            margin-top: 3px;" Width="133px"></asp:TextBox>
                        <asp:ImageButton ID="imgSearchLoc" runat="server" ImageUrl="~/Images/icon_search.png"
                            OnClick="imgSearchLoc_Click" Width="16px" />
                        <asp:Button ID="btnApply" runat="server" Text="Apply to All" Height="15%" Width="100px"
                            OnClick="btnApply_Click" Style="margin-left: 6px" BorderStyle="Solid" CssClass="Button" />
                        <asp:Button ID="btnEdit" runat="server" Text="Add Delivery" Height="15%" Width="100px"
                            Style="margin-left: 6px" BorderStyle="Solid" CssClass="Button" 
                            onclick="btnEdit_Click" />
                    </div>
                   
                    <div style="float: right; width: 100%; padding-top: 4px;">
                        <asp:Panel ID="Panel2" runat="server" Height="112px" ScrollBars="Auto" BorderColor="#9F9F9F"
                            BorderWidth="1px" Font-Bold="true" Width="99%" Style="margin-top: 11px">
                            <asp:GridView ID="gvDelDetails" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                                Style="margin-top: 0px" Width="732px" EmptyDataText="No data found" ShowHeaderWhenEmpty="True"
                                CellPadding="4" ForeColor="#333333" GridLines="None" CssClass="GridView" OnRowDeleting="gvDelDetails_RowCommand"
                                DataKeyNames="Podi_seq_no,Podi_line_no,Podi_del_line_no,Podi_itm_cd,Podi_itm_stus,Podi_qty,Podi_loca,Podi_remarks,Podi_bal_qty">
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Size="10px" Height="15px"
                                    Font-Bold="True" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle Font-Size="10px" BackColor="#EFF3FB" />
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField='Podi_seq_no' HeaderText='' Visible="false" />
                                    <asp:BoundField DataField='Podi_line_no' HeaderText='' Visible="false" />
                                    <asp:BoundField DataField='Podi_del_line_no' HeaderText='No' HeaderStyle-Width="5px"
                                        HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                        <HeaderStyle HorizontalAlign="Right" Width="5px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField='Podi_itm_cd' HeaderText='Item' ItemStyle-Width="85px"
                                        HeaderStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="85px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField='Podi_itm_stus' HeaderText='Status' HeaderStyle-Width="65px"
                                        HeaderStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" Width="65px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField='Podi_qty' HeaderText='Qty' HeaderStyle-Width="65px" HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-HorizontalAlign="Right">
                                        <HeaderStyle HorizontalAlign="Right" Width="65px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField='Podi_loca' HeaderText='Location' HeaderStyle-Width="65px"
                                        HeaderStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" Width="65px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField='Podi_remarks' HeaderText='' Visible="false" />
                                    <%-- <asp:BoundField DataField='Podi_remarks' HeaderText='Remarks' HeaderStyle-Width="260px"
                                    HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" Width="260px" />
                                </asp:BoundField>--%>
                                    <asp:BoundField DataField='Podi_bal_qty' HeaderText='' Visible="false" />
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgBtnEdit" runat="server" ImageUrl="~/Images/EditIcon.png"
                                                Width="13px" Height="13px" CommandName="Delete" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="4%" />
                                    </asp:TemplateField>
                                </Columns>
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            </asp:GridView>
                        </asp:Panel>
                    </div>
                </div>
                      
                <div style="float: right; width: 22.5%; Height : 170px; padding-top: 2px; padding-bottom: 2px;
                    background-color: #FFFFFF;">
                    <asp:Panel ID="pnlQuotation" runat="server"  ScrollBars="Auto" BorderColor="#9F9F9F"
                        BorderWidth="1px" Font-Bold="true" Width="99%" Height="100%" Style="margin-top: 4px">
                        <asp:GridView ID="gvQuotation" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                            Style="margin-top: 0px" GridLines="None" Width="100%" OnRowCommand="gvQuotation_RowCommand"
                            CellPadding="4" ForeColor="#333333" EmptyDataText="No data found" ShowHeaderWhenEmpty="True" CssClass="GridView">
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Size="10px" 
                                Font-Bold="True" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle Font-Size="10px" BackColor="#EFF3FB" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField='qd_frm_qty' HeaderText='From Qty' ItemStyle-Width="3%"
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" Width="3%" />
                                </asp:BoundField>
                                <asp:BoundField DataField='qd_to_qty' HeaderText='To Qty' ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" Width="3%" />
                                </asp:BoundField>
                                <asp:BoundField DataField='qd_unit_price' HeaderText='Price' ItemStyle-Width="8%"
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" Width="8%" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Pick">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgBtnGridDelete" runat="server" ImageUrl="~/Images/download_arrow_icon.png"
                                            Width="13px" Height="13px" CommandName="PICK" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="4%" />
                                </asp:TemplateField>
                            </Columns>
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                    </asp:Panel>
                </div>
                
                <div style="float: left; width: 100%; margin-top: 30px;" id="div3">
                    <asp:Label ID="lblRemarks" runat="server" Text="Remarks" Width="60px" Style="margin-left: 15px"></asp:Label><asp:TextBox
                        ID="txtRemarks" runat="server" CssClass="TextBox" Width="700px" TabIndex="3"></asp:TextBox>
                    <asp:Label ID="lblBaseCons" runat="server" Text="Base to consignment GRN: " Width="17%"
                        Style="margin-left: 17px"></asp:Label>
                    <asp:CheckBox ID="cheBaseCon" runat="server" Width="20%" AutoPostBack="true">
                    </asp:CheckBox>
                </div>
                
                <div style="display: none;">
                    <asp:Button ID="btnItem" runat="server" OnClick="CheckValidItem" />
                    <asp:Button ID="btnQty" runat="server" OnClick="CheckQty" />
                    <asp:Button ID="btnUPrice" runat="server" OnClick="CheckUPrice" />
                    <asp:Button ID="btnDisRate" runat="server" OnClick="CheckDisRate" />
                    <asp:Button ID="btnDisAmt" runat="server" OnClick="CheckDisAmt" />
                    <asp:Button ID="btnTax" runat="server" OnClick="CheckTax" />
                    <asp:Button ID="btnSupplier" runat="server" OnClick="CheckSupplier" />
                    <asp:Button ID="btnPODoc" runat="server" OnClick="CheckPO" />
                    <asp:Button ID="btnItmStatus" runat="server" OnClick="Checkstatus" />
                    <asp:Button ID="btndelLoc" runat="server" OnClick="CheckValidDelLoc" />
                    <asp:Button ID="btnExRate" runat="server" OnClick="GetExRate" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
