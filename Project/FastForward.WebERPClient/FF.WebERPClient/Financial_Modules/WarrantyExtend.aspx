<%@ Page Title="Warranty Extend" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="WarrantyExtend.aspx.cs" Inherits="FF.WebERPClient.Financial_Modules.WarrantyExtend" %>

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
                    <asp:Button ID="btnSave" runat="server" Text="Save" Height="100%" Width="70px" CssClass="Button"
                        OnClick="btnSave_Click" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" Height="100%" Width="70px"
                        CssClass="Button" OnClick="btnClear_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Height="100%" Width="70px" CssClass="Button"
                        OnClick="btnCancel_Click"/>
                    <asp:Button ID="btnPrint" runat="server" Text="Print" Height="100%" Width="70px"
                        CssClass="Button" OnClick="btnPrint_Click" />
                </div>
                <div style="float: left; width: 30%; padding-top: 1px; padding-bottom: 1px; height: 19px;">
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 35%; color: #000000;">
                        Invoice #</div>
                    <div style="float: left; width: 1%; color: #000000;">
                        :</div>
                    <div style="float: left; width: 60%; text-align: left; height: 17px;">
                        <asp:TextBox ID="txtInv" runat="server" Width="80%" Style="margin-left: 1%" CssClass="TextBox"></asp:TextBox>
                        <asp:ImageButton ID="imgInvSearch" runat="server" ImageUrl="~/Images/icon_search.png"
                            OnClick="imgInvSearch_Click" /></div>
                </div>
                <div style="float: right; width: 69%; padding-top: 1px; padding-bottom: 1px; height: 18px;">
                    <%-- <div style="float: left; width: 1%;">
                        &nbsp;</div>--%>
                    <div style="float: left; width: 12%; color: #000000;">
                        Invoice Date</div>
                    <div style="float: left; width: 1%; color: #000000;">
                        :</div>
                    <div style="float: left; width: 12%; text-align: left;">
                        <asp:Label ID="lblInvDate" Width="92%" runat="server" ForeColor="#0033CC"></asp:Label></div>
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 8%; color: #000000;">
                        Customer</div>
                    <div style="float: left; width: 1%; color: #000000;">
                        :</div>
                    <div style="float: left; width: 35%; text-align: left;">
                        <asp:Label ID="lblCusCode" Width="30%" runat="server" ForeColor="#0033CC"></asp:Label>
                        <asp:Label ID="lblCusName" Width="65%" runat="server" ForeColor="#0033CC"></asp:Label></div>
                </div>
                <div style="float: right; width: 69%; padding-top: 1px; padding-bottom: 1px; height: 18px;">
                    <div style="float: left; width: 12%; color: #000000;">
                        Address</div>
                    <div style="float: left; width: 1%; color: #000000;">
                        :</div>
                    <div style="float: left; width: 80%; text-align: left;">
                        <asp:Label ID="lblCusAdd1" Width="100%" runat="server" ForeColor="#0033CC" Text="Address"></asp:Label>
                    </div>
                </div>
                <div style="float: left; width: 30%; padding-top: 1px; padding-bottom: 1px; height: 19px;">
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 35%; color: #000000;">
                        Doc No</div>
                    <div style="float: left; width: 1%; color: #000000;">
                        :</div>
                    <div style="float: left; width: 60%; text-align: left;">
                        <asp:TextBox ID="txtExNo" runat="server" Width="80%" Style="margin-left: 1%" CssClass="TextBox"></asp:TextBox>
                        <asp:ImageButton ID="imgExNo" runat="server" ImageUrl="~/Images/icon_search.png"
                            OnClick="imgExNo_Click" /></div>
                </div>
                <div style="float: right; width: 69%; padding-top: 1px; padding-bottom: 1px; height: 20px;">
                    <div style="float: left; width: 12%; color: #000000;">
                        Address</div>
                    <div style="float: left; width: 1%; color: #000000;">
                        :</div>
                    <div style="float: left; width: 80%; text-align: left;">
                        <asp:Label ID="lblCusAdd2" Width="100%" runat="server" ForeColor="#0033CC" Text="Address"></asp:Label>
                    </div>
                </div>
                <div style="float: left; width: 30%; padding-top: 1px; padding-bottom: 1px; height: 16px;">
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 35%; color: #000000;">
                        Ref. No</div>
                    <div style="float: left; width: 1%; color: #000000;">
                        :</div>
                    <div style="float: left; width: 60%; text-align: left;">
                        <asp:TextBox ID="txtManual" runat="server" Width="80%" Style="margin-left: 1%" CssClass="TextBox"></asp:TextBox><asp:CheckBox
                            ID="chkIsManual" runat="server" Width="8%" AutoPostBack="true" OnCheckedChanged="chkIsManual_CheckedChanged">
                        </asp:CheckBox></div>
                </div>
                <div style="float: left; width: 50%; padding-top: 1px; padding-bottom: 1px;">
                    <div style="float: left; width: 99%; height: 19px; color: #FFFFFF; background-color: #507CD1;">
                        <asp:Label ID="Label2" runat="server" Text="Item Details" Width="189px" Height="16px"></asp:Label>
                    </div>
                    <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                        <div style="float: left; width: 2%;">
                            &nbsp;</div>
                        <div style="float: left; width: 16%; color: #000000;">
                            Item</div>
                        <div style="float: left; width: 1%; color: #000000;">
                            :</div>
                        <div style="float: left; width: 75%; text-align: left;">
                            <asp:TextBox ID="txtInvItem" runat="server" Width="65%" Style="margin-left: 2%" CssClass="TextBox"></asp:TextBox><asp:ImageButton
                                ID="imgInvItem" runat="server" ImageUrl="~/Images/icon_search.png" OnClick="imgInvItem_Click" /></div>
                    </div>
                    <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                        <div style="float: left; width: 2%;">
                            &nbsp;</div>
                        <div style="float: left; width: 16%; color: #000000;">
                            Serial</div>
                        <div style="float: left; width: 1%; color: #000000;">
                            :</div>
                        <div style="float: left; width: 75%; text-align: left;">
                            <asp:TextBox ID="txtInvSerial" runat="server" Width="65%" Style="margin-left: 2%"
                                CssClass="TextBox"></asp:TextBox><asp:ImageButton ID="imgInvEngine" runat="server"
                                    ImageUrl="~/Images/icon_search.png" OnClick="imgInvEngine_Click" /></div>
                    </div>
                    <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                        <div style="float: left; width: 2%;">
                            &nbsp;</div>
                        <div style="float: left; width: 16%; color: #000000;">
                            Other Serial</div>
                        <div style="float: left; width: 1%; color: #000000;">
                            :</div>
                        <div style="float: left; width: 75%; text-align: left;">
                            <asp:Label ID="lblOthSerial" Width="65%" runat="server" ForeColor="Black"></asp:Label></div>
                    </div>
                    <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                        <div style="float: left; width: 2%;">
                            &nbsp;</div>
                        <div style="float: left; width: 16%; color: #000000;">
                            DO #</div>
                        <div style="float: left; width: 1%; color: #000000;">
                            :</div>
                        <div style="float: left; width: 75%; text-align: left;">
                            <asp:Label ID="lblDo" Width="65%" runat="server" ForeColor="Black"></asp:Label></div>
                    </div>
                    <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                        <div style="float: left; width: 2%;">
                            &nbsp;</div>
                        <div style="float: left; width: 16%; color: #000000;">
                            Period</div>
                        <div style="float: left; width: 1%; color: #000000;">
                            :</div>
                        <div style="float: left; width: 75%; text-align: left;">
                            <asp:Label ID="lblWarPeriod" Width="65%" runat="server" ForeColor="Black"></asp:Label></div>
                    </div>
                    <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                        <div style="float: left; width: 2%;">
                            &nbsp;</div>
                        <div style="float: left; width: 16%; color: #000000;">
                            Warranty #</div>
                        <div style="float: left; width: 1%; color: #000000;">
                            :</div>
                        <div style="float: left; width: 75%; text-align: left;">
                            <asp:Label ID="lblWaraNo" Width="85%" runat="server" ForeColor="Black"></asp:Label></div>
                    </div>
                </div>
                <div style="float: right; width: 49%; padding-top: 1px; padding-bottom: 1px;">
                    <div style="float: left; width: 99%; height: 150px; margin-top: 1px;">
                        <asp:Panel ID="Panel1" runat="server" Height="85%" ScrollBars="Auto" BorderColor="#9F9F9F"
                            BorderWidth="1px" Font-Bold="true" Width="100%" Style="margin-top: 1px">
                            <asp:GridView ID="gvWaraDetails" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                                Style="margin-top: 0px" GridLines="None" RowStyle-Height="10px" Width="100%"
                                CellPadding="4" ForeColor="#333333" CssClass="GridView" EmptyDataText="No data found"
                                ShowHeaderWhenEmpty="True">
                                <Columns>
                                    <asp:BoundField DataField='srw_seq_no' HeaderText='' Visible="false" />
                                    <asp:BoundField DataField='srw_line' HeaderText='No' HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-HorizontalAlign="Right">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField='srw_rec_no' HeaderText='' Visible="false" />
                                    <asp:BoundField DataField='srw_inv_no' HeaderText='' Visible="false" />
                                    <%--<asp:BoundField DataField='srw_inv_no' HeaderText='Invoice No' HeaderStyle-Width="12%"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>--%>
                                    <asp:BoundField DataField='srw_do_no' HeaderText='DO #' HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField='srw_date' HeaderText='' Visible="false" />
                                    <asp:BoundField DataField='srw_itm' HeaderText='Item' HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField='srw_ser' HeaderText='Serial' HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField='srw_oth_ser' HeaderText='other Serial' HeaderStyle-Width="15%"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField='srw_warra' HeaderText='Warranty #' HeaderStyle-Width="15%"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField='srw_ex_period' HeaderText='' Visible="false" />
                                    <asp:BoundField DataField='srw_new_period' HeaderText='Period' HeaderStyle-Width="11%"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField='srw_pb' HeaderText='' Visible="false" />
                                    <asp:BoundField DataField='srw_lvl' HeaderText='' Visible="false" />
                                    <asp:BoundField DataField='srw_amt' HeaderText='Amount' HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-HorizontalAlign="Right">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField='srw_cre_by' HeaderText='' Visible="false" />
                                    <asp:BoundField DataField='srw_cre_when' HeaderText='' Visible="false" />
                                    <asp:BoundField DataField='srw_ser_id' HeaderText='' Visible="false" />
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
                <div style="height: 10px; width: 100%;" dir="rtl">
                </div>
                <div style="float: left; width: 30%; padding-top: 2px;">
                    <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                        <div style="float: right; width: 99%; height: 19px; color: #FFFFFF; background-color: #507CD1;">
                            <asp:Label ID="lblprice" runat="server" Text="Amount details" Width="189px" Height="16px"></asp:Label>
                        </div>
                        <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 25%; color: #000000;">
                                Price Book</div>
                            <div style="float: left; width: 1%; color: #000000;">
                                :</div>
                            <div style="float: left; width: 50%; text-align: right;">
                                <asp:DropDownList ID="ddlBook" runat="server" CssClass="ComboBox" Width="80%" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlBook_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 25%; color: #000000;">
                                Price Level</div>
                            <div style="float: left; width: 1%; color: #000000;">
                                :</div>
                            <div style="float: left; width: 50%; text-align: right;">
                                <asp:DropDownList ID="ddlLevel" runat="server" CssClass="ComboBox" Width="80%" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 25%; color: #000000;">
                                Ex. Period</div>
                            <div style="float: left; width: 1%; color: #000000;">
                                :</div>
                            <div style="float: left; width: 50%; text-align: right;">
                                <asp:Label ID="lblNewPeriod" Width="85%" runat="server" ForeColor="Black"></asp:Label>
                            </div>
                        </div>
                        <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 25%; color: #000000;">
                                Amount</div>
                            <div style="float: left; width: 1%; color: #000000;">
                                :</div>
                            <div style="float: left; width: 50%; text-align: right;">
                                <asp:Label ID="lblAmt" Width="85%" runat="server" ForeColor="Black"></asp:Label>
                            </div>
                        </div>
                        <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 25%; color: #000000;">
                                Pay Type</div>
                            <div style="float: left; width: 1%; color: #000000;">
                                :</div>
                            <div style="float: left; width: 50%; text-align: right;">
                                <asp:DropDownList ID="ddlPayMode" runat="server" Width="80%" CssClass="ComboBox"
                                    OnSelectedIndexChanged="PaymentType_LostFocus" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 25%; color: #000000;">
                                Payment Amount</div>
                            <div style="float: left; width: 2%; color: #000000;">
                                :</div>
                            <div style="float: left; width: 49%; text-align: right;">
                                <asp:TextBox ID="txtPayAmt" runat="server" Width="80%" Style="text-align: right;"
                                    CssClass="TextBox" onkeypress="return isNumberKeyAndDot(event,value)"></asp:TextBox></div>
                        </div>
                    </div>
                </div>
                <div style="float: left; width: 68%; padding-top: 2px; padding-left: 4px;">
                    <div style="float: left; width: 50%; padding-top: 1px; padding-bottom: 1px;">
                        <div style="float: left; width: 99%; height: 19px; color: #FFFFFF; background-color: #507CD1;">
                            <asp:Label ID="Label1" runat="server" Text="Settlement Bank Details" Width="189px"
                                Height="16px"></asp:Label>
                        </div>
                        <div style="float: left; width: 99%; height: 22px; padding-top: 2px">
                            &nbsp;<asp:Label ID="lblOtherType" runat="server" Text="Ref Type:" Width="77px"></asp:Label>
                            <asp:DropDownList ID="ddlRefType" runat="server" Width="133px" CssClass="ComboBox">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>VISA</asp:ListItem>
                                <asp:ListItem>MASTER</asp:ListItem>
                                <asp:ListItem>AMEX</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div style="float: left; width: 99%; height: 19px;">
                            &nbsp;<asp:Label ID="lblRefNo" runat="server" Text="Ref. # :" Width="58px"></asp:Label>
                            <asp:TextBox ID="txtRef" runat="server" Width="226px" Style="margin-left: 18px" CssClass="TextBox"
                                MaxLength="15"></asp:TextBox></div>
                        <div style="float: left; width: 99%; height: 22px;">
                            &nbsp;<asp:Label ID="lblBank" runat="server" Text="Bank :" Width="58px"></asp:Label>
                            <asp:TextBox ID="txtBank" runat="server" Width="93px" Style="margin-left: 18px" CssClass="TextBox"></asp:TextBox><asp:ImageButton
                                ID="ImgBankSearch" runat="server" ImageUrl="~/Images/icon_search.png" OnClick="ImgBankSearch_Click" />
                            <asp:Label ID="lblBankName" runat="server" Width="264px"></asp:Label></div>
                        <div style="float: left; width: 99%; height: 19px; padding-bottom: 2px">
                            &nbsp;<asp:Label ID="lblBranch" runat="server" Text="Branch :" Width="58px"></asp:Label>
                            <asp:TextBox ID="txtBranch" runat="server" Width="226px" Style="margin-left: 18px"
                                CssClass="TextBox" MaxLength="20"></asp:TextBox></div>
                    </div>
                    <div style="float: left; width: 48%; padding-top: 1px; padding-bottom: 1px;">
                        <div style="float: left; width: 99%; height: 19px; color: #FFFFFF; background-color: #507CD1;">
                            <asp:Label ID="lblDeposit" runat="server" Text="Deposit Bank Details" Width="189px"
                                Height="16px"></asp:Label>
                        </div>
                        <div style="float: left; width: 99%; height: 22px;">
                            &nbsp;<asp:Label ID="lblDBank" runat="server" Text="Bank :" Width="58px"></asp:Label>
                            <asp:TextBox ID="txtDBank" runat="server" Width="78px" Style="margin-left: 5px" CssClass="TextBox"></asp:TextBox><asp:ImageButton
                                ID="Img" runat="server" ImageUrl="~/Images/icon_search.png" OnClick="Img_Click" />
                            <asp:Label ID="lblDBankDesc" runat="server" Width="259px"></asp:Label>
                        </div>
                        <div style="float: left; width: 99%; height: 23px;">
                            &nbsp;<asp:Label ID="lblDBranch" runat="server" Text="Branch :" Width="58px"></asp:Label>
                            <asp:TextBox ID="txtDBranch" runat="server" Width="226px" Style="margin-left: 5px"
                                CssClass="TextBox" MaxLength="20"></asp:TextBox></div>
                        <div style="float: right; width: 100%; height: 20px; text-align: right; margin-top: 20px;">
                            <asp:Button ID="btnAdd" runat="server" Text="Add Payment" Height="100%" Width="30%"
                                BorderStyle="Solid" CssClass="Button" OnClick="btnAdd_Click" />
                        </div>
                    </div>
                </div>
                <div style="float: left; width: 100%; height: 100px; margin-top: 5px;">
                    <asp:Panel ID="pnlPayments" runat="server" Height="85%" ScrollBars="Auto" BorderColor="#9F9F9F"
                        BorderWidth="1px" Font-Bold="true" Width="100%" Style="margin-top: 22px">
                        <asp:GridView ID="gvRecDetails" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                            Style="margin-top: 0px" GridLines="None" RowStyle-Height="10px" Width="100%"
                            CellPadding="4" ForeColor="#333333" CssClass="GridView" EmptyDataText="No data found"
                            ShowHeaderWhenEmpty="True" DataKeyNames="sard_settle_amt">
                            <Columns>
                                <asp:BoundField DataField='sard_seq_no' HeaderText='' Visible="false" />
                                <asp:BoundField DataField='sard_line_no' HeaderText='No' HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField='sard_receipt_no' HeaderText='' Visible="false" />
                                <asp:BoundField DataField='sard_inv_no' HeaderText='Invoice No' HeaderStyle-Width="12%"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField='sard_pay_tp' HeaderText='Pay type' HeaderStyle-Width="8%"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField='sard_ref_no' HeaderText='Referance No' HeaderStyle-Width="14%"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField='sard_chq_bank_cd' HeaderText='Ref Bank' HeaderStyle-Width="11%"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField='sard_chq_branch' HeaderText='Ref Branch' HeaderStyle-Width="11%"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField='sard_deposit_bank_cd' HeaderText='Deposit Bank' HeaderStyle-Width="11%"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField='sard_deposit_branch' HeaderText='Dep. Branch' HeaderStyle-Width="11%"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField='sard_credit_card_bank' HeaderText='' Visible="false" />
                                <asp:BoundField DataField='sard_cc_tp' HeaderText='' Visible="false" />
                                <asp:BoundField DataField='sard_cc_expiry_dt' HeaderText='' Visible="false" />
                                <asp:BoundField DataField='sard_cc_is_promo' HeaderText='' Visible="false" />
                                <asp:BoundField DataField='sard_cc_period' HeaderText='' Visible="false" />
                                <asp:BoundField DataField='sard_gv_issue_loc' HeaderText='' Visible="false" />
                                <asp:BoundField DataField='sard_gv_issue_dt' HeaderText='' Visible="false" />
                                <asp:BoundField DataField='sard_settle_amt' HeaderText='Amount' HeaderStyle-Width="15%"
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField='sard_sim_ser' HeaderText='' Visible="false" />
                                <asp:BoundField DataField='sard_anal_1' HeaderText='' Visible="false" />
                                <asp:BoundField DataField='sard_anal_2' HeaderText='' Visible="false" />
                                <asp:BoundField DataField='sard_anal_3' HeaderText='' Visible="false" />
                                <asp:BoundField DataField='sard_anal_4' HeaderText='' Visible="false" />
                                <asp:BoundField DataField='sard_anal_5' HeaderText='' Visible="false" />
                                <%-- <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgBtnGridDelete" runat="server" ImageUrl="~/Images/Delete.png"
                                            Width="13px" Height="13px" CommandName="Delete" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
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
                <div style="float: left; width: 100%; height: 1px; margin-top: 2px;">
                </div>
                <div style="float: left; width: 100%; height: 25px; margin-top: 10px;">
                    <asp:Label ID="lblRemarks" runat="server" Text="Note :" Width="58px"></asp:Label>
                    <asp:TextBox ID="txtRemarks" runat="server" Width="551px" Style="margin-left: 10px;
                        margin-top: 0px;" CssClass="TextBox"></asp:TextBox>
                    <asp:Label ID="lblTot" runat="server" Text="Total :" Width="58px" Style="margin-left: 153px"></asp:Label>
                    <asp:TextBox ID="txtTotal" runat="server" Width="95px" Style="margin-left: 10px;
                        margin-top: 0px; text-align: right;" CssClass="TextBox" ReadOnly="true"></asp:TextBox>
                </div>
                <div style="display: none;">
                    <asp:Button ID="btnInvEngine" runat="server" OnClick="CheckValidInvEngine" />
                    <asp:Button ID="btnInv" runat="server" OnClick="GetInvDet" />
                    <asp:Button ID="btnInvItem" runat="server" OnClick="CheckValidInvItem" />
                    <asp:Button ID="btnPriceBook" runat="server" OnClick="LoadPriceLvl" />
                    <asp:Button ID="btnPriceLevel" runat="server" OnClick="LoadAmount" />
                    <asp:Button ID="btnRecNo" runat="server" OnClick="GetSaveReceipt" />
                    <asp:Button ID="btnPayType" runat="server" OnClick="CheckPayType" />
                    <asp:Button ID="btnManual" runat="server" OnClick="CheckValidManualRef" />
                    <asp:Button ID="btnBank" runat="server" OnClick="GetBankDetails" />
                    <asp:Button ID="btnAcc" runat="server" OnClick="GetAccDetails" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
