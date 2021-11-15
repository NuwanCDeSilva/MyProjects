<%@ Page Title="Receipt entry" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ReceiptEntry.aspx.cs" Inherits="FF.WebERPClient.Financial_Modules.ReceiptEntry" %>

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
                    <%--Button Panel--%>
                    <div style="float: left; width: 100%; height: 22px; text-align: right;">
                        <div style="float: left;">
                            <asp:Label ID="lblDispalyInfor" runat="server" CssClass="Label" ForeColor="blue"></asp:Label>
                        </div>
                        <asp:Button ID="btnSave" runat="server" Text="Save" Height="100%" Width="70px" CssClass="Button"
                            OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Height="100%" Width="70px"
                            CssClass="Button" OnClick="btnCancel_Click" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" Height="100%" Width="70px"
                            CssClass="Button" OnClick="btnClear_Click" />
                        <asp:Button ID="btnPrint" runat="server" Text="Print" Height="100%" Width="70px"
                            CssClass="Button" OnClick="btnPrint_Click" />
                    </div>
                    <%--item selecting area--%>
                    <div style="float: left; width: 100%; padding-top: 2px; padding-bottom: 2px;">
                        <%-- Collaps Header - Items --%>
                        <div style="height: 18px; background-color: #1E4A9F; color: White; width: 98%; float: left;
                            margin-top: 6px;">
                            Receipt Details</div>
                        <%-- Collaps Image - Items --%>
                        <div style="float: left; margin-top: 6px;">
                            <asp:Image runat="server" ID="Image1" ImageUrl="~/Images/16 X 16 DownArrow.jpg" ImageAlign="Right" /></div>
                        <%-- Collaps control - Items --%>
                        <asp:CollapsiblePanelExtender ID="CPEHPItem" runat="server" TargetControlID="pnlRecHdr"
                            CollapsedSize="0" ExpandedSize="490" Collapsed="false" ExpandControlID="Image1"
                            CollapseControlID="Image1" AutoCollapse="False" AutoExpand="false" ScrollContents="false"
                            ExpandDirection="Vertical" ImageControlID="Image1" ExpandedImage="~/Images/16 X 16 UpArrow.jpg"
                            CollapsedImage="~/Images/16 X 16 DownArrow.jpg">
                        </asp:CollapsiblePanelExtender>
                        <%-- Collaps Area - HP Items --%>
                        <div style="width: 100%; float: left; padding-top: 2px; padding-bottom: 2px;">
                            <asp:Panel runat="server" ID="pnlRecHdr" Width="99.8%" BorderColor="#9F9F9F" BorderWidth="1px"
                                Font-Bold="false">
                                <div style="float: left; width: 100%;" id="divHeader">
                                    <asp:Label ID="lblType" runat="server" Text="Receipt Type" Width="175px" Style="margin-left: 19px"></asp:Label>
                                    <asp:Label ID="lblDiv" runat="server" Text="Division" Width="159px" Style="margin-left: 0px"></asp:Label>
                                    <asp:Label ID="lblDate" runat="server" Text="Date" Width="156px" Height="16px"></asp:Label>
                                    <asp:Label ID="lblRecNo" runat="server" Text="Receipt #" Width="219px"></asp:Label>
                                    <asp:Label ID="lblManualRef" runat="server" Text="Manual Ref. #"></asp:Label><asp:CheckBox
                                        ID="chkIsManual" runat="server" Width="10%" AutoPostBack="true" OnCheckedChanged="chkIsManual_CheckedChanged">
                                    </asp:CheckBox>
                                </div>
                                <div style="float: left; width: 100%; height: 17px;" id="divHeaderEntry">
                                    <asp:TextBox ID="txtType" runat="server" Width="136px" Style="margin-left: 18px"
                                        CssClass="TextBox"></asp:TextBox>
                                    <asp:ImageButton ID="imgtypesearch" runat="server" ImageUrl="~/Images/icon_search.png"
                                        OnClick="imgtypesearch_Click" />
                                    <asp:TextBox ID="txtDivision" runat="server" Width="120px" Style="margin-left: 18px"
                                        CssClass="TextBox"></asp:TextBox>
                                    <asp:ImageButton ID="imgSearchDiv" runat="server" ImageUrl="~/Images/icon_search.png"
                                        OnClick="imgSearchDiv_Click" />
                                    <asp:TextBox ID="txtDate" runat="server" Style="margin-left: 18px" CssClass="TextBox"
                                        AutoPostBack="false"></asp:TextBox>
                                    <asp:Image ID="imgRequestDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                    <asp:CalendarExtender ID="RequestDateCalExtender" runat="server" TargetControlID="txtDate"
                                        PopupButtonID="imgRequestDate" Enabled="True" Format="dd/MMM/yyyy">
                                    </asp:CalendarExtender>
                                    <asp:TextBox ID="txtRecNo" runat="server" Style="margin-left: 18px" CssClass="TextBox"
                                        Width="180px"></asp:TextBox><asp:ImageButton ID="imgrecSearch" runat="server" ImageUrl="~/Images/icon_search.png"
                                            OnClick="imgrecSearch_Click" />
                                    <asp:TextBox ID="txtRefNo" runat="server" Style="margin-left: 18px" CssClass="TextBox"
                                        Width="175px" MaxLength="100"></asp:TextBox>
                                </div>
                                <div style="float: left; width: 100%; height: 13px;">
                                    &nbsp;
                                </div>
                                <div style="float: left; width: 50%; height: 97px;" id="divCustomer">
                                    <asp:Panel ID="pnlAdvance" runat="server" Height="140px" ForeColor="Black">
                                        <div style="float: left; width: 99%; height: 19px; background-color: #507CD1; color: #FFFFFF;
                                            font-weight: normal;">
                                            <asp:Label ID="lblReceive" runat="server" Text="Receive From" Width="189px" Height="16px"></asp:Label>
                                        </div>
                                        <div style="float: left; width: 99%; height: 22px;">
                                            &nbsp;<asp:Label ID="lblCusCode" runat="server" Text="Code :" Width="58px"></asp:Label>
                                            <asp:TextBox ID="txtCusCode" runat="server" Width="133px" Style="margin-left: 18px"
                                                CssClass="TextBox"></asp:TextBox>
                                            <asp:ImageButton ID="imgcusSearch" runat="server" ImageUrl="~/Images/icon_search.png"
                                                OnClick="imgcusSearch_Click" /></div>
                                        <div style="float: left; width: 99%; height: 19px;">
                                            &nbsp;<asp:Label ID="lblCusName" runat="server" Text="Name :" Width="58px"></asp:Label>
                                            <asp:TextBox ID="txtCusName" runat="server" Width="371px" Style="margin-left: 18px"
                                                CssClass="TextBox"></asp:TextBox></div>
                                        <div style="float: left; width: 99%; height: 19px;">
                                            &nbsp;<asp:Label ID="Label1" runat="server" Text="Address :" Width="58px"></asp:Label>
                                            <asp:TextBox ID="txtCusAdd1" runat="server" Width="371px" Style="margin-left: 18px"
                                                CssClass="TextBox"></asp:TextBox></div>
                                        <div style="float: left; width: 99%; height: 21px;">
                                            <asp:TextBox ID="txtCusAdd2" runat="server" Width="371px" Style="margin-left: 83px"
                                                CssClass="TextBox"></asp:TextBox></div>
                                        <div style="float: left; width: 99%; height: 19px;">
                                            &nbsp;<asp:Label ID="Label3" runat="server" Text="NIC # :" Width="58px"></asp:Label>
                                            <asp:TextBox ID="txtNIC" runat="server" Width="150px" Style="margin-left: 18px" CssClass="TextBox"
                                                MaxLength="10"></asp:TextBox>
                                            &nbsp;
                                            <asp:Label ID="Label4" runat="server" Text="Mob. # :" Width="58px"></asp:Label>
                                            <asp:TextBox ID="txtMob" runat="server" Width="126px" Style="margin-left: 18px" CssClass="TextBox"
                                                MaxLength="10"></asp:TextBox></div>
                                        <div style="float: left; width: 99%; height: 19px;">
                                            &nbsp;<asp:Label ID="Label5" runat="server" Text="District :" Width="76px"></asp:Label>
                                            <%--<asp:TextBox ID="txtdistrict" runat="server" Width="150px" Style="margin-left: 18px"
                                    CssClass="TextBox" MaxLength="10"></asp:TextBox>--%>
                                            <asp:DropDownList ID="ddlDistrict" runat="server" Width="125px" CssClass="ComboBox"
                                                OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            &nbsp;
                                            <asp:Label ID="Label6" runat="server" Text="Province :" Width="58px" Style="margin-left: 30px"></asp:Label>
                                            <asp:TextBox ID="txtProvince" runat="server" Width="126px" Style="margin-left: 17px"
                                                CssClass="TextBox" MaxLength="10" ReadOnly="True"></asp:TextBox></div>
                                    </asp:Panel>
                                    <div style="height: 10px" dir="rtl">
                                    </div>
                                    <div style="float: left; width: 99%; height: 19px; background-color: #507CD1; color: #000000;
                                        font-weight: normal;" id="divRefBank">
                                        <div style="float: left; width: 99%; height: 19px; color: #FFFFFF; background-color: #507CD1;">
                                            <asp:Label ID="Label2" runat="server" Text="Settlement Bank Details" Width="189px"
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
                                </div>
                                <div style="float: left; width: 50%; height: 19px; background-color: #507CD1; color: #000000;
                                    font-weight: normal;" id="divPayType">
                                    <div style="float: left; width: 99%; height: 19px; background-color: #507CD1; color: #FFFFFF;">
                                        <asp:Label ID="lblSettleDetails" runat="server" Text="Settlement Details" Width="189px"
                                            Height="16px"></asp:Label>
                                    </div>
                                    <div style="float: left; width: 99%; height: 22px;">
                                        &nbsp;<asp:Label ID="lblInvoice" runat="server" Text="Invoice # :" Width="74px"></asp:Label>
                                        <asp:TextBox ID="txtInvoice" runat="server" Width="133px" Style="margin-left: 18px"
                                            CssClass="TextBox"></asp:TextBox>
                                        <asp:ImageButton ID="imgInvSearch" runat="server" ImageUrl="~/Images/icon_search.png"
                                            OnClick="imgInvSearch_Click" /><asp:Label ID="lblInvItem" runat="server" Text="Item :"
                                                Width="50px"></asp:Label>
                                        <asp:TextBox ID="txtInvItem" runat="server" Width="135px" Style="margin-left: 5px"
                                            CssClass="TextBox"></asp:TextBox><asp:ImageButton ID="imgInvItem" runat="server"
                                                ImageUrl="~/Images/icon_search.png" OnClick="imgInvItem_Click" />
                                    </div>
                                    <div style="float: left; width: 99%; height: 19px;">
                                        &nbsp;<asp:Label ID="lblInvAmt" runat="server" Text="Balance :" Width="74px"></asp:Label>
                                        <asp:TextBox ID="txtInvAmt" runat="server" Width="133px" Style="margin-left: 18px;
                                            text-align: right;" CssClass="TextBox" ReadOnly="True" onkeypress="return isNumberKeyAndDot(event,value)"></asp:TextBox><asp:Label
                                                ID="lblInvEngine" runat="server" Text="Engine :" Width="50px" Style="margin-left: 20px"></asp:Label>
                                        <asp:TextBox ID="txtInvEngine" runat="server" Width="135px" Style="margin-left: 5px"
                                            CssClass="TextBox"></asp:TextBox><asp:ImageButton ID="imgInvEngine" runat="server"
                                                ImageUrl="~/Images/icon_search.png" OnClick="imgInvEngine_Click" />
                                    </div>
                                    <div style="float: left; width: 99%; height: 21px; padding-top: 4px;">
                                        &nbsp;<asp:Label ID="lblPayType" runat="server" Text="Pay Type :" Width="92px"></asp:Label>
                                        <asp:DropDownList ID="ddlPayMode" runat="server" Width="133px" CssClass="ComboBox"
                                            OnSelectedIndexChanged="PaymentType_LostFocus" AutoPostBack="true">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblInvChasis" runat="server" Text="Chasis :" Width="50px" Style="margin-left: 20px"></asp:Label>
                                        <asp:TextBox ID="txtInvChasis" runat="server" Width="150px" Style="margin-left: 5px"
                                            CssClass="TextBox"></asp:TextBox><asp:ImageButton ID="imgInvChasis" runat="server"
                                                ImageUrl="~/Images/icon_search.png" OnClick="imgInvChasis_Click" Visible="false" />
                                        <%--<asp:TextBox ID="txtPayType" runat="server" Width="133px" Style="margin-left: 18px"
                                CssClass="TextBox"></asp:TextBox><asp:ImageButton ID="imgpaytypeSearch" runat="server"
                                    ImageUrl="~/Images/icon_search.png" />--%>
                                    </div>
                                    <div style="float: left; width: 99%; height: 19px;">
                                        &nbsp;<asp:Label ID="lblAmt" runat="server" Text="Amount :" Width="74px"></asp:Label>
                                        <asp:TextBox ID="txtAmount" runat="server" Width="133px" Style="margin-left: 18px;
                                            text-align: right;" CssClass="TextBox" onkeypress="return isNumberKeyAndDot(event,value)"></asp:TextBox>
                                        <asp:Label ID="lblInsCom" runat="server" Text="Ins Com:" Width="50px" Style="margin-left: 15px"></asp:Label>
                                        <asp:TextBox ID="txtInsCom" runat="server" Width="100px" Style="margin-left: 10px"
                                            CssClass="TextBox"></asp:TextBox><asp:ImageButton ID="imgInsCom" runat="server" ImageUrl="~/Images/icon_search.png"
                                                Visible="false" OnClick="imgInsCom_Click" />
                                    </div>
                                    <div style="float: left; width: 46.5%; padding-top: 10px;">
                                        &nbsp;<asp:CheckBox ID="chkDeliverItem" runat="server" Width="55%" AutoPostBack="true"
                                            Text="Deliverd Items" Visible="false"></asp:CheckBox>
                                    </div>
                                    <div style="float: right; width: 46.5%; padding-top: 10px;">
                                        <asp:Label ID="lblPolicy" runat="server" Text="Policy :" Width="50px"></asp:Label>
                                        <asp:TextBox ID="txtPolicy" runat="server" Width="100px" Style="margin-left: 10px"
                                            CssClass="TextBox"></asp:TextBox><asp:ImageButton ID="imgPol" runat="server" ImageUrl="~/Images/icon_search.png"
                                                Visible="false" OnClick="imgPol_Click" />
                                        &nbsp;<asp:CheckBox ID="chkAnnual" runat="server" Width="55%" AutoPostBack="true"
                                            Text="Annaul premimum" Visible="false" 
                                            oncheckedchanged="chkAnnual_CheckedChanged"></asp:CheckBox>
                                    </div>
                                    <%-- <div style="float: left; width: 99%; height: 21px;">
                                        &nbsp;<asp:Label ID="Label7" runat="server" Text="Engine # :" Width="74px" Visible="False"></asp:Label>
                                        <asp:TextBox ID="txtEngine" runat="server" Width="133px" Style="margin-left: 18px;"
                                            CssClass="TextBox" Visible="False"></asp:TextBox>
                                        <asp:ImageButton ID="imgEngine" runat="server" ImageUrl="~/Images/icon_search.png"
                                            Visible="False" />
                                        &nbsp;<asp:Label ID="Label8" runat="server" Text="Chasis # :" Width="63px" Visible="False"></asp:Label>
                                        <asp:TextBox ID="txtchasis" runat="server" Width="125px" Style="margin-left: 18px;"
                                            CssClass="TextBox" Visible="False" ReadOnly="True"></asp:TextBox></div>
                                    <div style="float: left; width: 99%; height: 19px;">
                                        &nbsp;<asp:Label ID="Label9" runat="server" Text="Item :" Width="74px" Visible="False"></asp:Label>
                                        <asp:TextBox ID="txtItem" runat="server" Width="133px" Style="margin-left: 18px;"
                                            CssClass="TextBox" Visible="False" ReadOnly="True"></asp:TextBox></div>--%>
                                    <div style="float: left; width: 466px; height: 10px; margin-left: 10px;">
                                    </div>
                                    <div style="float: left; width: 99%; height: 19px; background-color: #507CD1; color: #000000;
                                        font-weight: normal;" id="divDepositBank">
                                        <div style="float: left; width: 99%; height: 19px; color: #FFFFFF; background-color: #507CD1;">
                                            <asp:Label ID="lblDeposit" runat="server" Text="Deposit Bank Details" Width="189px"
                                                Height="16px"></asp:Label>
                                        </div>
                                        <div style="float: left; width: 99%; height: 22px;">
                                            &nbsp;<asp:Label ID="lblDBank" runat="server" Text="Bank :" Width="58px"></asp:Label>
                                            <asp:TextBox ID="txtDBank" runat="server" Width="78px" Style="margin-left: 18px"
                                                CssClass="TextBox"></asp:TextBox><asp:ImageButton ID="Img" runat="server" ImageUrl="~/Images/icon_search.png"
                                                    OnClick="Img_Click" />
                                            <asp:Label ID="lblDBankDesc" runat="server" Width="259px"></asp:Label>
                                        </div>
                                        <div style="float: left; width: 99%; height: 20px;">
                                            &nbsp;<asp:Label ID="lblDBranch" runat="server" Text="Branch :" Width="58px"></asp:Label>
                                            <asp:TextBox ID="txtDBranch" runat="server" Width="226px" Style="margin-left: 18px"
                                                CssClass="TextBox" MaxLength="20"></asp:TextBox>&nbsp;<asp:Button ID="btnAdd" runat="server"
                                                    Text="Add Payments" Height="90%" Width="19%" BorderStyle="Solid" CssClass="Button"
                                                    OnClick="btnAdd_Click" /></div>
                                    </div>
                                </div>
                                <div style="float: left; width: 100%; height: 165px; margin-top: 133px;">
                                    <asp:Panel ID="pnlPayments" runat="server" Height="85%" ScrollBars="Auto" BorderColor="#9F9F9F"
                                        BorderWidth="1px" Font-Bold="true" Width="100%" Style="margin-top: 22px">
                                        <asp:GridView ID="gvRecDetails" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                                            Style="margin-top: 0px" GridLines="None" RowStyle-Height="10px" Width="100%"
                                            CellPadding="4" ForeColor="#333333" CssClass="GridView" EmptyDataText="No data found"
                                            ShowHeaderWhenEmpty="True" OnRowDeleting="OnRemoveFromRecDetails" DataKeyNames="sard_settle_amt">
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
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgBtnGridDelete" runat="server" ImageUrl="~/Images/Delete.png"
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
                                <div style="float: left; width: 100%; height: 2px; margin-top: 5px;">
                                </div>
                                <div style="float: left; width: 100%; height: 25px; margin-top: 5px;">
                                    <asp:Label ID="lblRemarks" runat="server" Text="Note :" Width="58px"></asp:Label>
                                    <asp:TextBox ID="txtRemarks" runat="server" Width="551px" Style="margin-left: 10px;
                                        margin-top: 0px;" CssClass="TextBox"></asp:TextBox>
                                    <asp:Label ID="lblTot" runat="server" Text="Total :" Width="58px" Style="margin-left: 153px"></asp:Label>
                                    <asp:TextBox ID="txtTotal" runat="server" Width="95px" Style="margin-left: 10px;
                                        margin-top: 0px; text-align: right;" CssClass="TextBox" ReadOnly="true"></asp:TextBox>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
                <div>
                    <%--item selecting area--%>
                    <div style="float: left; width: 100%; padding-top: 2px; padding-bottom: 2px;">
                        <%-- Collaps Header - Items --%>
                        <div style="height: 18px; background-color: #1E4A9F; color: White; width: 98%; float: left;
                            margin-top: 6px;">
                            Item Resevation Details</div>
                        <%-- Collaps Image - Items --%>
                        <div style="float: left; margin-top: 6px;">
                            <asp:Image runat="server" ID="Image2" ImageUrl="~/Images/16 X 16 DownArrow.jpg" ImageAlign="Right" /></div>
                        <%-- Collaps control - Items --%>
                        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" TargetControlID="pnlSerial"
                            CollapsedSize="0" ExpandedSize="452" Collapsed="True" ExpandControlID="Image2"
                            CollapseControlID="Image2" AutoCollapse="False" AutoExpand="False" ScrollContents="false"
                            ExpandDirection="Vertical" ImageControlID="Image2" ExpandedImage="~/Images/16 X 16 UpArrow.jpg"
                            CollapsedImage="~/Images/16 X 16 DownArrow.jpg">
                        </asp:CollapsiblePanelExtender>
                        <%-- Collaps Area - HP Items --%>
                        <div style="width: 100%; float: left; padding-top: 2px; padding-bottom: 2px;">
                            <asp:Panel runat="server" ID="pnlSerial" Width="99.8%" BorderColor="#9F9F9F" BorderWidth="1px"
                                Font-Bold="false" ForeColor="Black">
                                <div style="float: left; width: 100%; margin-top: 10px">
                                    &nbsp;<asp:Label ID="Label9" runat="server" Text="Item :" Width="74px"></asp:Label>
                                    <asp:TextBox ID="txtItem" runat="server" Width="133px" Style="margin-left: 10px;"
                                        CssClass="TextBox"></asp:TextBox>
                                    <asp:ImageButton ID="imgItemSearch" runat="server" ImageUrl="~/Images/icon_search.png"
                                        OnClick="imgItmSearch_Click" />
                                    <asp:Label ID="Label10" runat="server" Text="Model :" Width="74px" Style="margin-left: 10px;"></asp:Label>
                                    <asp:Label ID="lblModel" runat="server" Text="Model" Width="125px"></asp:Label>
                                    <asp:Label ID="Label11" runat="server" Text="Desc :" Width="74px"></asp:Label>
                                    <asp:Label ID="lblDesc" runat="server" Text="Desc" Width="250px"></asp:Label>
                                </div>
                                <div style="float: left; width: 100%;">
                                    &nbsp;<asp:Label ID="lblEngine" runat="server" Text="Engine # :" Width="74px"></asp:Label>
                                    <asp:TextBox ID="txtEngine" runat="server" Width="133px" Style="margin-left: 10px;"
                                        CssClass="TextBox"></asp:TextBox>
                                    <asp:ImageButton ID="imgEngine" runat="server" ImageUrl="~/Images/icon_search.png"
                                        OnClick="imgEngine_Click" />
                                    &nbsp;<asp:Label ID="lblChasis" runat="server" Text="Chasis # :" Width="63px"></asp:Label>
                                    <asp:TextBox ID="txtchasis" runat="server" Width="125px" Style="margin-left: 10px;"
                                        CssClass="TextBox"></asp:TextBox><asp:ImageButton ID="imgChasis" runat="server" ImageUrl="~/Images/icon_search.png"
                                            OnClick="imgChasis_Click" />
                                </div>
                                <div style="float: right; width: 100%; height: 20px; margin-top: 20px;">
                                    <asp:Button ID="btnAddSerial" runat="server" Text="Add Reservation" BorderStyle="Solid"
                                        CssClass="Button" OnClick="btnAddSerial_Click" />
                                </div>
                                <div style="float: left; width: 100%; height: 165px; margin-top: 50px;">
                                    <asp:Panel ID="Panel1" runat="server" Height="85%" ScrollBars="Auto" BorderColor="#9F9F9F"
                                        BorderWidth="1px" Font-Bold="true" Width="100%" Style="margin-top: 22px">
                                        <asp:GridView ID="gvSerial" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                                            Style="margin-top: 0px" GridLines="None" Width="99%" CellPadding="2" ForeColor="#333333"
                                            CssClass="GridView" EmptyDataText="No data found" ShowHeaderWhenEmpty="True"
                                            OnRowDeleting="OnRemoveFromResDetails">
                                            <Columns>
                                                <%--<asp:BoundField DataField='TUS_USRSEQ_NO' HeaderText='' Visible="false" />
                                                <asp:BoundField DataField='TUS_DOC_NO' HeaderText='' Visible="false" />
                                                <asp:BoundField DataField='TUS_SEQ_NO' HeaderText='' Visible="false" />
                                                <asp:BoundField DataField='TUS_ITM_LINE' HeaderText='' Visible="false" />
                                                <asp:BoundField DataField='TUS_BATCH_LINE' HeaderText='' Visible="false" />
                                                <asp:BoundField DataField='TUS_SER_LINE' HeaderText='' Visible="false" />
                                                <asp:BoundField DataField='TUS_DOC_DT' HeaderText='' Visible="false" />--%>
                                                <asp:BoundField DataField='TUS_ITM_CD' HeaderText='Item Code' HeaderStyle-Width="12%"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField='TUS_ITM_STUS' HeaderText='Status' HeaderStyle-Width="8%"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField='TUS_ITM_DESC' HeaderText='Description' HeaderStyle-Width="30%"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField='TUS_ITM_MODEL' HeaderText='Model' HeaderStyle-Width="10%"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField='TUS_SER_1' HeaderText='Serial #' HeaderStyle-Width="20%"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField='TUS_SER_2' HeaderText='Other Serial' HeaderStyle-Width="20%"
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
                            </asp:Panel>
                        </div>
                    </div>
                </div>
                <div>
                    <%--item selecting area--%>
                    <div style="float: left; width: 100%; padding-top: 2px; padding-bottom: 2px;">
                        <%-- Collaps Header - Items --%>
                        <div style="height: 18px; background-color: #1E4A9F; color: White; width: 98%; float: left;
                            margin-top: 6px;">
                            Registration details</div>
                        <%-- Collaps Image - Items --%>
                        <div style="float: left; margin-top: 6px;">
                            <asp:Image runat="server" ID="Image3" ImageUrl="~/Images/16 X 16 DownArrow.jpg" ImageAlign="Right" /></div>
                        <%-- Collaps control - Items --%>
                        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server" TargetControlID="pnlRegDetails"
                            CollapsedSize="0" ExpandedSize="452" Collapsed="True" ExpandControlID="Image3"
                            CollapseControlID="Image3" AutoCollapse="False" AutoExpand="False" ScrollContents="false"
                            ExpandDirection="Vertical" ImageControlID="Image3" ExpandedImage="~/Images/16 X 16 UpArrow.jpg"
                            CollapsedImage="~/Images/16 X 16 DownArrow.jpg">
                        </asp:CollapsiblePanelExtender>
                        <%-- Collaps Area - HP Items --%>
                        <div style="width: 100%; float: left; padding-top: 2px; padding-bottom: 2px;">
                            <asp:Panel runat="server" ID="pnlRegDetails" Width="99.8%" BorderColor="#9F9F9F"
                                BorderWidth="1px" Font-Bold="false" ForeColor="Black">
                                <div style="float: left; width: 100%; height: 165px; margin-top: 10px;">
                                    <asp:Panel ID="Panel3" runat="server" Height="85%" ScrollBars="Auto" BorderColor="#9F9F9F"
                                        BorderWidth="1px" Font-Bold="true" Width="100%" Style="margin-top: 4px">
                                        <asp:GridView ID="gvReg" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                                            Style="margin-top: 0px" GridLines="None" Width="99%" CellPadding="2" ForeColor="#333333"
                                            CssClass="GridView" EmptyDataText="No data found" ShowHeaderWhenEmpty="True"
                                            OnRowDeleting="OnRemoveFromRegDetails">
                                            <Columns>
                                                <asp:BoundField DataField='p_svrt_inv_no' HeaderText='Invoice #' HeaderStyle-Width="10%"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField='p_svrt_full_name' HeaderText='Customer' HeaderStyle-Width="20%"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField='p_svrt_district' HeaderText='District' HeaderStyle-Width="10%"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField='p_svrt_province' HeaderText='Province' HeaderStyle-Width="10%"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField='p_svrt_model' HeaderText='Model' HeaderStyle-Width="9%"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField='p_svrt_brd' HeaderText='Brand' HeaderStyle-Width="9%"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField='p_svrt_engine' HeaderText='Engine #' HeaderStyle-Width="20%"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField='p_svrt_chassis' HeaderText='Chasis #' HeaderStyle-Width="20%"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField='p_svrt_reg_val' HeaderText='Reg. Value' HeaderStyle-Width="12%"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgRegDelete" runat="server" ImageUrl="~/Images/Delete.png"
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
                            </asp:Panel>
                        </div>
                    </div>
                </div>
                <div>
                    <%--item selecting area--%>
                    <div style="float: left; width: 100%; padding-top: 2px; padding-bottom: 2px;">
                        <%-- Collaps Header - Items --%>
                        <div style="height: 18px; background-color: #1E4A9F; color: White; width: 98%; float: left;
                            margin-top: 6px;">
                            Insuarance details</div>
                        <%-- Collaps Image - Items --%>
                        <div style="float: left; margin-top: 6px;">
                            <asp:Image runat="server" ID="Image4" ImageUrl="~/Images/16 X 16 DownArrow.jpg" ImageAlign="Right" /></div>
                        <%-- Collaps control - Items --%>
                        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" runat="server" TargetControlID="pnlIns"
                            CollapsedSize="0" ExpandedSize="452" Collapsed="True" ExpandControlID="Image4"
                            CollapseControlID="Image4" AutoCollapse="False" AutoExpand="False" ScrollContents="false"
                            ExpandDirection="Vertical" ImageControlID="Image4" ExpandedImage="~/Images/16 X 16 UpArrow.jpg"
                            CollapsedImage="~/Images/16 X 16 DownArrow.jpg">
                        </asp:CollapsiblePanelExtender>
                        <%-- Collaps Area - HP Items --%>
                        <div style="width: 100%; float: left; padding-top: 2px; padding-bottom: 2px;">
                            <asp:Panel runat="server" ID="pnlIns" Width="99.8%" BorderColor="#9F9F9F" BorderWidth="1px"
                                Font-Bold="false" ForeColor="Black">
                                <div style="float: left; width: 100%; height: 165px; margin-top: 10px;">
                                    <asp:Panel ID="Panel4" runat="server" Height="85%" ScrollBars="Auto" BorderColor="#9F9F9F"
                                        BorderWidth="1px" Font-Bold="true" Width="100%" Style="margin-top: 4px">
                                        <asp:GridView ID="gvInsu" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                                            Style="margin-top: 0px" GridLines="None" Width="99%" CellPadding="2" ForeColor="#333333"
                                            CssClass="GridView" EmptyDataText="No data found" ShowHeaderWhenEmpty="True"
                                            OnRowDeleting="OnRemoveFromInsDetails">
                                            <Columns>
                                                <asp:BoundField DataField='svit_inv_no' HeaderText='Invoice #' HeaderStyle-Width="10%"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField='svit_full_name' HeaderText='Customer' HeaderStyle-Width="20%"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField='svit_district' HeaderText='District' HeaderStyle-Width="10%"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField='svit_province' HeaderText='Province' HeaderStyle-Width="10%"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField='svit_model' HeaderText='Model' HeaderStyle-Width="9%"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField='svit_brd' HeaderText='Brand' HeaderStyle-Width="9%" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField='svit_engine' HeaderText='Engine #' HeaderStyle-Width="20%"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField='svit_chassis' HeaderText='Chasis #' HeaderStyle-Width="20%"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField='svit_ins_val' HeaderText='Ins. Value' HeaderStyle-Width="12%"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField='svit_ins_com' HeaderText='Ins. company' HeaderStyle-Width="12%"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField='svit_ins_polc' HeaderText='Policy' HeaderStyle-Width="12%"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgInsDelete" runat="server" ImageUrl="~/Images/Delete.png"
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
                            </asp:Panel>
                        </div>
                    </div>
                </div>
                <div style="display: none;">
                    <asp:Button ID="btnCustomer" runat="server" OnClick="LoadCustomerDetailsByCustomer" />
                    <asp:Button ID="btnType" runat="server" OnClick="CheckValidType" />
                    <asp:Button ID="btnBank" runat="server" OnClick="GetBankDetails" />
                    <asp:Button ID="btnAcc" runat="server" OnClick="GetAccDetails" />
                    <asp:Button ID="btnRecNo" runat="server" OnClick="GetSaveReceipt" />
                    <asp:Button ID="btnDiv" runat="server" OnClick="CheckValidDivision" />
                    <asp:Button ID="btnOutInv" runat="server" OnClick="GetOutInvAmt" />
                    <asp:Button ID="btnAmount" runat="server" OnClick="ValidateAmount" />
                    <asp:Button ID="btnPayType" runat="server" OnClick="CheckPayType" />
                    <asp:Button ID="btnDistrict" runat="server" OnClick="GetProvince" />
                    <asp:Button ID="btnMob" runat="server" OnClick="CheckValidMob" />
                    <asp:Button ID="btnNIC" runat="server" OnClick="CheckValidNIC" />
                    <asp:Button ID="btnItem" runat="server" OnClick="CheckValidItem" />
                    <asp:Button ID="btnSerial" runat="server" OnClick="CheckValidSerial" />
                    <asp:Button ID="btnChasis" runat="server" OnClick="CheckValidChasis" />
                    <asp:Button ID="btnInvItem" runat="server" OnClick="CheckValidInvItem" />
                    <asp:Button ID="btnInvEngine" runat="server" OnClick="CheckValidInvEngine" />
                    <asp:Button ID="btnInsuCom" runat="server" OnClick="CheckValidInsuCom" />
                    <asp:Button ID="btnPolicy" runat="server" OnClick="CheckValidPolicy" />
                    <asp:Button ID="btnManual" runat="server" OnClick="CheckValidManualRef" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
