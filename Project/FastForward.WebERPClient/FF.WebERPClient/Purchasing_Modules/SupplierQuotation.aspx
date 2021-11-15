<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="SupplierQuotation.aspx.cs" Inherits="FF.WebERPClient.Purchasing_Modules.SupplierQuotation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
    <style type="text/css">
        .style1
        {
            width: 100%;
            font-family: Verdana;
        }
        .style15
        {
            width: 64px;
        }
        .style22
        {
            width: 147px;
        }
        .style23
        {
            width: 63px;
        }
        .style24
        {
            width: 45px;
        }
        .style25
        {
            width: 139px;
        }
        .style26
        {
            width: 62px;
        }
        .style27
        {
            width: 135px;
        }
        .style30
        {
            width: 349px;
        }
        .style31
        {
            width: 113px;
        }
        .style36
        {
            width: 10px;
        }
        .style39
        {
            width: 109px;
        }
        .style40
        {
            width: 124px;
        }
        .style41
        {
            width: 85px;
        }
        .style42
        {
            width: 54px;
        }
        .style43
        {
            width: 43px;
        }
        .style48
        {
            width: 89px;
        }
        .style49
        {
            font-family: Verdana;
            border-left-color: #A0A0A0;
            border-right-color: #C0C0C0;
            border-top-color: #A0A0A0;
            border-bottom-color: #C0C0C0;
            padding: 1px;
        }
        .style50
        {
            width: 185px;
        }
    </style>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
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
    </script>
    <div style="font-family: Verdana; font-size: 11px;">
        <asp:Panel ID="Panel_tabs" runat="server">
            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Style="text-align: left"
                Width="95%" Font-Bold="False">
                <asp:TabPanel ID="TabPanel_1" runat="server" HeaderText="Quotation Detail" Width="95%"
                    Font-Bold="False"><HeaderTemplate>Quotation Detail</HeaderTemplate><ContentTemplate><div>&nbsp;&nbsp;</div><asp:Panel ID="Panel_generalInfo" runat="server" GroupingText=" "
                            ><div><asp:Panel ID="Panel_save" runat="server" Style="text-align: right"><asp:Button ID="btnClear" runat="server" CssClass="Button" OnClick="btnClear_Click"
                                        Text="Clear"></asp:Button><asp:Button ID="btnSave" runat="server" CssClass="Button" OnClick="btnSave_Click"
                                        Text="Save New/ Update"></asp:Button><asp:Button ID="btnSaveAs_pend" runat="server" CssClass="Button" OnClick="btnSaveAs_pend_Click"
                                        Text="Save As new"></asp:Button><asp:Button ID="btnSave_approve" runat="server" CssClass="Button" OnClick="btnSave_approve_Click"
                                        Text="Approve"></asp:Button>
                                <asp:Button ID="Button1" runat="server" Text="Close" CssClass="Button" 
                            onclick="Button1_Click" />
                                        </asp:Panel></div>
                    <table style="font-family: Verdana; font-size: 11px"><tr><td >
                        <span class="style49">Quotation Number</span> </td><td class="style50"><asp:TextBox ID="txtQuotaNo" runat="server" CssClass="TextBox" Enabled="False" Height="16px"></asp:TextBox></td><td >
                        <span class="style49">Supplier Code</span> </td><td >
                            <asp:TextBox ID="txtSuppCD" 
                                runat="server" CssClass="TextBox" TabIndex="3" ></asp:TextBox><asp:ImageButton ID="ImageBtnSupplier" runat="server" ImageUrl="~/Images/icon_search.png"
                                            OnClick="ImageBtnSupplier_Click"></asp:ImageButton><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSuppCD"
                                            ErrorMessage="*" ForeColor="Red" ValidationGroup="QTvalid"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblSuppName" runat="server" CssClass="Label"></asp:Label></td><td ></td></tr><tr><td >
                        <span class="style49">From Date</span> </td><td class="style50" ><asp:TextBox ID="txtFromDt" runat="server" CssClass="TextBox" TabIndex="1"></asp:TextBox><asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"  Format="dd/MM/yyyy"
                                            TargetControlID="txtFromDt"></asp:CalendarExtender><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFromDt"
                                            ErrorMessage="*" ForeColor="Red" ValidationGroup="QTvalid"></asp:RequiredFieldValidator></td><td >
                            <span class="style49">Type</span> </td><td ><asp:DropDownList ID="ddlSuppType" runat="server" Height="17px" Width="108px" 
                                            TabIndex="4" style="font-size: 11px; font-family: Verdana"><asp:ListItem></asp:ListItem><asp:ListItem Value="N">NORMAL</asp:ListItem><asp:ListItem Value="C">CONSIGN</asp:ListItem></asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSuppType"
                                            ErrorMessage="*" ForeColor="Red" ValidationGroup="QTvalid"></asp:RequiredFieldValidator></td><td></td></tr><tr><td >
                        <span class="style49">Exp. Date</span> </td><td class="style50"><asp:TextBox ID="txtExpDate" runat="server" CssClass="TextBox" TabIndex="2"></asp:TextBox><asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            TargetControlID="txtExpDate"></asp:CalendarExtender><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtExpDate"
                                            ErrorMessage="*" ForeColor="Red" ValidationGroup="QTvalid"></asp:RequiredFieldValidator></td><td>
                            <span class="style49">Manual Ref No.</span> </td><td ><asp:TextBox ID="txtManRefNo" runat="server" CssClass="TextBox" MaxLength="30"
                                            Width="248px" TabIndex="5"></asp:TextBox></td><td ></td></tr><tr><td></td>
                        <td class="style50" ></td><td >
                        <span class="style49">Remarks</span> </td><td ><asp:TextBox ID="txtNote" runat="server" CssClass="TextBox" MaxLength="30"
                                            Width="251px" TabIndex="6"></asp:TextBox></td><td ></td></tr></table></asp:Panel><div></div><div></div><asp:Panel ID="Panel_QT_AddItmeDet" runat="server" GroupingText="Add Items"><div>
                    <asp:Label ID="Label2" runat="server" Text="Item Code . . . : " 
                                    CssClass="Label"></asp:Label><asp:TextBox ID="txtToAddItemCD" runat="server" CssClass="TextBox" TabIndex="7"></asp:TextBox><asp:ImageButton ID="imgBtnItemCd" runat="server" ImageUrl="~/Images/icon_search.png"
                                    OnClick="imgBtnItem_Click"></asp:ImageButton>
                    <asp:Label ID="Label3" runat="server" Text="Unit Price . . . :" 
                                    CssClass="Label"></asp:Label><asp:TextBox ID="txtQuotPrice" runat="server" CssClass="TextBoxNumeric" 
                                    TabIndex="8"></asp:TextBox><asp:ImageButton ID="btnImg_add" 
                        runat="server" Height="19px" ImageUrl="~/Images/download_arrow_icon.png"
                                    OnClick="btnImg_add_Click" Width="22px" TabIndex="9"></asp:ImageButton><br /></div><asp:Panel ID="Panel_Itm_det" runat="server" ScrollBars="Both" Height="137px"><asp:GridView ID="GridView_itm_det" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" OnRowCommand="GridView_itm_det_RowCommand" 
                                    CssClass="GridView" ><AlternatingRowStyle BackColor="White"></AlternatingRowStyle><Columns><asp:TemplateField HeaderText="Item Code"><ItemTemplate><asp:LinkButton ID="linkbtnItmCD" runat="server" CommandArgument='<%# Eval("Qd_itm_cd") %>'
                                                    CommandName="SELECTITEMCODE" Text='<%# DataBinder.Eval(Container.DataItem, "Qd_itm_cd") %>'></asp:LinkButton></ItemTemplate></asp:TemplateField><asp:BoundField DataField="Qd_itm_desc" HeaderText="Description"></asp:BoundField><asp:BoundField DataField="Qd_nitm_desc" HeaderText="      Model      "></asp:BoundField></Columns><EditRowStyle BackColor="#2461BF" /><FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></FooterStyle><HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></HeaderStyle><PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center"></PagerStyle><RowStyle BackColor="#EFF3FB" /><SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle><SortedAscendingCellStyle BackColor="#F5F7FB"></SortedAscendingCellStyle><SortedAscendingHeaderStyle BackColor="#6D95E1"></SortedAscendingHeaderStyle><SortedDescendingCellStyle BackColor="#E9EBEF"></SortedDescendingCellStyle><SortedDescendingHeaderStyle BackColor="#4870BE"></SortedDescendingHeaderStyle></asp:GridView></asp:Panel></asp:Panel><div></div><div><asp:Panel ID="Panel_QTDetails" runat="server" GroupingText="Add Price Range" ><div><table class="style1"><tr><td class="style23">
                    <asp:Label ID="Label4" runat="server" Text="From Qty:" CssClass="Label"></asp:Label></td><td class="style25"><asp:TextBox ID="txtLineFromQty" runat="server" CssClass="TextBoxNumeric" 
                                                    MaxLength="10"></asp:TextBox></td><td class="style24">
                        <asp:Label ID="Label8" runat="server" Text="To Qty:" CssClass="Label"></asp:Label></td><td class="style22"><asp:TextBox ID="txtLineToQty" runat="server" CssClass="TextBoxNumeric" 
                                                    MaxLength="10"></asp:TextBox></td><td class="style26">
                        <asp:Label ID="Label9" runat="server" Text="Unit Price:" CssClass="Label"></asp:Label></td><td class="style27"><asp:TextBox ID="txtLinePrice" runat="server" CssClass="TextBoxNumeric" 
                                                    MaxLength="12"></asp:TextBox></td><td class="style40">
                        <asp:ImageButton ID="btnImg_addLine" runat="server" Height="19px" ImageUrl="~/Images/download_arrow_icon.png"
                                                    OnClick="btnImg_addLine_Click" Width="22px"></asp:ImageButton></td><td style="text-align: right"><asp:Button ID="btnDeleteLast" runat="server" CssClass="Button" 
                                                    OnClick="btnDeleteLast_Click" Text="Delete last row" /></td></tr></table></div></asp:Panel><asp:Panel ID="Panel_Qty_det" runat="server" Height="111px" 
                                ScrollBars="Vertical"><div style="width: 513px; text-align: right;"><asp:GridView ID="GridView_Qty_det" runat="server" AutoGenerateColumns="False" BackColor="#FFFFCC"
                                        BorderStyle="None" CellPadding="4" ForeColor="#333333" 
                                        OnRowCommand="GridView_Qty_det_RowCommand" CssClass="GridView" ><AlternatingRowStyle BackColor="White"></AlternatingRowStyle><Columns><asp:TemplateField HeaderText="#" Visible="False"><EditItemTemplate><asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Qd_line_no") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:Label ID="lblLine" runat="server" Text='<%# Bind("Qd_line_no") %>' Visible="False"></asp:Label></ItemTemplate></asp:TemplateField><asp:BoundField DataField="Qd_frm_qty" HeaderText="From Qty."></asp:BoundField><asp:BoundField DataField="Qd_to_qty" HeaderText="To Qty."></asp:BoundField><asp:TemplateField HeaderText="Unit Price (Rs.)"><EditItemTemplate><asp:TextBox ID="txt_lineUnitPrice" runat="server" Text='<%# Bind("Qd_unit_price") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:TextBox ID="txt_unitprice" runat="server" CssClass="TextBoxNumeric" MaxLength="12"
                                                        Text='<%# Bind("Qd_unit_price") %>'></asp:TextBox><asp:Button ID="btn_update" runat="server" CommandName="UPDATEPRICE" CssClass="Button"
                                                        OnClick="btn_update_Click" Text="update" /></ItemTemplate></asp:TemplateField></Columns><EditRowStyle BackColor="#2461BF" /><FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></FooterStyle><HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></HeaderStyle><PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center"></PagerStyle><RowStyle BackColor="#EFF3FB" /><SelectedRowStyle BackColor="#FFCCCC" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle><SortedAscendingCellStyle BackColor="#F5F7FB"></SortedAscendingCellStyle><SortedAscendingHeaderStyle BackColor="#6D95E1"></SortedAscendingHeaderStyle><SortedDescendingCellStyle BackColor="#E9EBEF"></SortedDescendingCellStyle><SortedDescendingHeaderStyle BackColor="#4870BE"></SortedDescendingHeaderStyle></asp:GridView></div></asp:Panel></div><div></div></ContentTemplate></asp:TabPanel>
                <asp:TabPanel ID="TabPanel_2" runat="server" HeaderText="Search Quotaion" Font-Bold="False"
                    TabIndex="1">
                    <HeaderTemplate>
                        Search Quotaion
                    </HeaderTemplate>
                    <ContentTemplate><div></div><asp:Panel ID="Panel1" runat="server">
                        <div style="font-family: Verdana; font-size: 11px"><table class="style1"><tr><td class="style48"><asp:Label ID="Label5" runat="server" Text="Supplier :"></asp:Label></td><td class="style31">
                            <asp:TextBox ID="txtVeiwSupp" runat="server" CssClass="TextBox"
                                                Width="126px"></asp:TextBox></td><td class="style36"><asp:ImageButton ID="ImgBtnVeiwSupp" runat="server" ImageUrl="~/Images/icon_search.png"
                                                OnClick="ImgBtnVeiwSupp_Click" /></td><td class="style43"><asp:Label ID="Label6" runat="server" Text="From Date:"></asp:Label></td><td class="style15"><asp:TextBox ID="txtViewFromDT" runat="server" CssClass="TextBox"></asp:TextBox><asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                TargetControlID="txtViewFromDT"></asp:CalendarExtender></td><td class="style42"><asp:Label ID="Label7" runat="server" Text="To Date:"></asp:Label></td><td class="style41"><asp:TextBox ID="txtViewToDT" runat="server" CssClass="TextBox"></asp:TextBox><asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                TargetControlID="txtViewToDT"></asp:CalendarExtender></td><td class="style40"><asp:Label ID="Label1" runat="server" Text="Quotation Status :"></asp:Label><asp:DropDownList ID="ddlVeiwQuotStat" runat="server" Height="16px" 
                                                Width="102px" CssClass="style45"><asp:ListItem></asp:ListItem><asp:ListItem Value="P">PENDING</asp:ListItem><asp:ListItem Value="A">APPROVED</asp:ListItem></asp:DropDownList></td><td class="style39"><asp:Button ID="btnVeiwSearch2" runat="server" CssClass="Button" OnClick="btnVeiwSearch_Click"
                                                Text="Search" /></td></tr><tr><td class="style48"></td><td class="style31"></td><td class="style36"></td><td class="style43"></td><td class="style15"></td><td class="style42"></td><td class="style41"></td><td class="style40"></td><td class="style39"></td></tr></table></div></asp:Panel><div><div><br /></div><asp:Panel ID="Panel_veiwQuot" runat="server" Height="391px" ScrollBars="Both"><asp:GridView ID="GridView_Quatations" runat="server" AutoGenerateColumns="False"
                                    CellPadding="4" ForeColor="#333333" OnRowCommand="GridView_Quatations_RowCommand"
                                    Width="876px" CssClass="GridView"><AlternatingRowStyle BackColor="White" /><Columns><asp:TemplateField HeaderText="Quotation No."><ItemTemplate><asp:LinkButton ID="lnkbtnQTNum" runat="server" CommandArgument='<%# Eval("Qh_no") %>'
                                                    CommandName="GETQOUTATION" Text='<%# DataBinder.Eval(Container.DataItem, "Qh_no") %>'></asp:LinkButton></ItemTemplate></asp:TemplateField><asp:BoundField DataField="Qh_party_cd" HeaderText="Supplier Code" /><asp:BoundField DataField="Qh_party_name" HeaderText="Supplier Name" /><asp:BoundField DataField="Qh_dt" HeaderText="QuotationDate"  DataFormatString="{0:d}" /><asp:BoundField DataField="Qh_frm_dt" HeaderText="Start Date" DataFormatString="{0:d}" /><asp:BoundField DataField="Qh_ex_dt" HeaderText="Exp. Date" DataFormatString="{0:d}"/></Columns><EditRowStyle BackColor="#2461BF" /><EmptyDataTemplate><asp:LinkButton ID="LinkButton1" runat="server">LinkButton</asp:LinkButton></EmptyDataTemplate><FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" /><HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" /><PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" /><RowStyle BackColor="#EFF3FB" /><SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" /><SortedAscendingCellStyle BackColor="#F5F7FB" /><SortedAscendingHeaderStyle BackColor="#6D95E1" /><SortedDescendingCellStyle BackColor="#E9EBEF" /><SortedDescendingHeaderStyle BackColor="#4870BE" /></asp:GridView></asp:Panel></div></ContentTemplate></asp:TabPanel>
            </asp:TabContainer>
        </asp:Panel>
        <div>
            <div>
            </div>
            <div style="width: 100%">
            </div>
            
            <div  style="display: none;">
                 <asp:Button ID="btnValidate_Supplier" runat="server" OnClick="btnValidate_Supplier_Click" />         
            </div>
        </div>
    </div>
</asp:Content>
