<%@ Page Title="Receipt Reversal" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="HpReceiptReversal.aspx.cs" Inherits="FF.WebERPClient.HP_Module.HpReceiptReversal" %>

<%@ Register Src="../UserControls/uc_HpAccountSummary.ascx" TagName="uc_HpAccountSummary"
    TagPrefix="AC" %>
<%@ Register Src="../UserControls/uc_HpAccountDetail.ascx" TagName="uc_HpAccountDetail"
    TagPrefix="AD" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--Main Page--%>
    <div style="float: left; width: 100%; color: Black;">
        <%--Button Panel--%>
        <div style="float: left; width: 100%;">
            <%--Button Area--%>
            <div style="height: 22px; text-align: right;" class="PanelHeader">
                <asp:Button ID="btnRequest" runat="server" Text="Request/Approve" Height="85%" Width="110px"
                    OnClick="btnRequest_Click" CssClass="Button" />
                <asp:Button ID="btnReject" runat="server" Text="Reject" Height="85%" Width="70px"
                    OnClick="btnReject_Click" CssClass="Button" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" Height="85%" Width="70px" OnClick="btnClear_Click"
                    CssClass="Button" />
                <asp:Button ID="btnClose" runat="server" Text="Close" Height="85%" Width="70px" OnClick="btnClose_Click"
                    CssClass="Button" />
            </div>
        </div>
        <%--Detaiil Panel--%>
        <div>
            <%--Seperator--%>
            <div style="float: left; width: 50%; padding-top: 3px;">
                <div style="float: left; width: 100%; padding-top: 2px; padding-bottom: 2px;">
                    <div style="float: left; width: 1%;">
                        &nbsp;
                    </div>
                    <div style="float: left; width: 20%;">
                        Request Type :</div>
                    <div style="float: left; width: 50%;">
                        <asp:RadioButton ID="optNewReq" runat="server" Text="Request" Checked="True" Width="115px"
                            GroupName="RequestType" />
                        <asp:RadioButton ID="optApproved" runat="server" Text="Approved" Width="115px" GroupName="RequestType" />
                    </div>
                    <div style="float: left; width: 28%;">
                        &nbsp;
                    </div>
                    <div style="float: left; width: 1%;">
                        &nbsp;
                    </div>
                </div>
                <div style="float: left; width: 100%; padding-top: 2px; padding-bottom: 2px;">
                    <div style="float: left; width: 1%;">
                        &nbsp;
                    </div>
                    <div style="float: left; width: 20%;">
                        Refund Type :</div>
                    <div style="float: left; width: 78%;">
                        <asp:RadioButton ID="optManagerIssue" runat="server" Text="Manager Issue" Checked="True"
                            Width="115px" GroupName="RefundType" AutoPostBack="true" OnCheckedChanged="optManagerIssue_CheckedChanged" />
                        <asp:RadioButton ID="optRtnCheque" runat="server" Text="Return Cheque" Width="115px"
                            GroupName="RefundType" AutoPostBack="true" OnCheckedChanged="optRtnCheque_CheckedChanged" />
                        <asp:RadioButton ID="optOthReceipt" runat="server" Text="Other Receipt" Width="115px"
                            GroupName="RefundType" AutoPostBack="true" OnCheckedChanged="optOthReceipt_CheckedChanged" />
                    </div>
                    <div style="float: left; width: 1%;">
                        &nbsp;
                    </div>
                </div>
                <div style="float: left; width: 100%; padding-top: 2px; padding-bottom: 2px;">
                    <div style="float: left; width: 1%;">
                        &nbsp;
                    </div>
                    <div style="float: left; width: 20%;">
                        Account No :</div>
                    <div style="float: left; width: 30%;">
                        <asp:TextBox ID="txtAccountNo" runat="server" CssClass="TextBox" Width="70%"></asp:TextBox>
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/icon_search.png"
                            ImageAlign="Middle" OnClick="btn_validateACC_Click" />
                    </div>
                    <div style="float: left; width: 48%;">
                        <asp:Label ID="lblAccountNo" runat="server" Width="90%" ForeColor="IndianRed"></asp:Label>
                    </div>
                    <div style="float: left; width: 1%;">
                        &nbsp;
                    </div>
                </div>
                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px">
                    <div style="float: left; width: 99%;">
                        <div class="PanelHeader">
                            Receipt Details</div>
                    </div>
                    <div style="float: left; width: 55%;">
                        <div style="float: left; width: 2%;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 36%;">
                            Request No :
                        </div>
                        <div style="float: left; width: 57%;">
                            <asp:DropDownList ID="ddlRequestNo" runat="server" CssClass="ComboBox" AutoPostBack="true"
                                Width="90%" OnSelectedIndexChanged="ddlRequestNo_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div style="float: left; width: 40%;">
                        <asp:CheckBox runat="server" ID="chkApproved" Text="Approved Request" AutoPostBack="true"
                            OnCheckedChanged="chkApproved_CheckedChanged" />
                    </div>
                    <div style="float: left; width: 99%;">
                        <asp:Panel runat="server" ID="pnlItemDetail" ScrollBars="None" Height="131px">
                            <asp:GridView runat="server" ID="gvItemDetail" AutoGenerateColumns="false" CssClass="GridView"
                                CellPadding="3" ForeColor="#333333" GridLines="Both" ShowHeaderWhenEmpty="true">
                                <EmptyDataTemplate>
                                    <div style="width: 100%; text-align: center;">
                                        No data found
                                    </div>
                                </EmptyDataTemplate>
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" />
                                <RowStyle BackColor="#EFF3FB" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                <Columns>
                                    <%--Coulumn 1--%>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkOneReceipt" runat="server" ClientIDMode="Static" AutoPostBack="true"
                                                OnCheckedChanged="chkOneReceipt_CheckChangedMethod" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--Coulumn 2--%>
                                    <asp:BoundField DataField='SAR_COM_CD' HeaderText='Company Code' HeaderStyle-Width="100px"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                                    <%--Coulumn 3--%>
                                    <asp:BoundField DataField='SAR_RECEIPT_TYPE' HeaderText='Receipt Type' HeaderStyle-Width="100px"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                                    <%--Coulumn 4--%>
                                    <asp:BoundField DataField='SAR_PROFIT_CENTER_CD' HeaderText='PC Code' HeaderStyle-Width="100px"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                                    <%--Coulumn 5--%>
                                    <asp:BoundField DataField='SAR_ACC_NO' HeaderText='Account No' HeaderStyle-Width="100px"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                                    <%--Coulumn 6--%>
                                    <asp:BoundField DataField='SAR_RECEIPT_NO' HeaderText='Receipt No' HeaderStyle-Width="100px"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <%--Coulumn 7--%>
                                    <asp:BoundField DataField='SAR_RECEIPT_DATE' HeaderText='Date' HeaderStyle-Width="80px"
                                        DataFormatString='<%$ appSettings:FormatToDate %>' HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <%--Coulumn 8--%>
                                    <asp:BoundField DataField='SAR_MANUAL_REF_NO' HeaderText='Manual Ref' HeaderStyle-Width="100px"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="true" />
                                    <%--Coulumn 9--%>
                                    <asp:BoundField DataField='SAR_PREFIX' HeaderText='Prefix' HeaderStyle-Width="40px"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <%--Coulumn 10--%>
                                    <asp:BoundField DataField='SAR_TOT_SETTLE_AMT' HeaderText='Receipt Amount' HeaderStyle-Width="90px"
                                        HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString='<%$ appSettings:FormatToCurrency %>' />
                                    <%--Coulumn 11--%>
                                    <asp:BoundField DataField='SAR_ANAL_5' HeaderText='Refund Amount' HeaderStyle-Width="90px"
                                        HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString='<%$ appSettings:FormatToCurrency %>' />
                                    <%--Coulumn 12--%>
                                    <asp:BoundField DataField='SAR_ANAL_1' HeaderText='Refund Type' HeaderStyle-Width="90px"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <%--Seperator--%>
            <div style="float: left; width: 48%; padding-top: 3px;">
                <div style="float: left; width: 100%; padding-top: 3px;">
                    <AC:uc_HpAccountSummary ID="uc_HpAccountSummary1" runat="server" />
                </div>
                <div style="float: left; width: 100%; padding-top: 3px;">
                    <AD:uc_HpAccountDetail ID="uc_HpAccountDetail1" runat="server" />
                </div>
            </div>
            <%--Seperator--%>
            <div style="float: left; width: 1%;">
                &nbsp;</div>
        </div>
    </div>
    <%--Modal popup account search--%>
    <asp:ModalPopupExtender ID="mpeAccDet" runat="server" CancelControlID="imgmpeAccDetClose"
        ClientIDMode="Static" PopupControlID="pnlAccDet" TargetControlID="btnHidden_popup"
        PopupDragHandleControlID="divAccDetHeader" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <div style="float: left; width: 100%; padding-top: 2px; padding-bottom: 2px;">
        <asp:Panel ID="pnlAccDet" runat="server" Width="400px" BackColor="#E6E6E6" BorderColor="#E0E0E0">
            <div id="divAccDetHeader" runat="server" class="popUpHeader">
                <div id="divAccDetCaption" runat="server" style="float: left; width: 90%">
                    Select account no ....
                </div>
                <div style="float: left; width: 10%; text-align: right">
                    <asp:ImageButton ID="imgmpeAccDetClose" runat="server" ImageUrl="~/Images/0032-01-16x16x16.ICO" />&nbsp;</div>
            </div>
            <asp:Panel ID="divAccDetGRV" runat="server" ScrollBars="Vertical">
                <asp:GridView ID="grvMpdalPopUpAccDet" runat="server" AutoGenerateColumns="False"
                    DataKeyNames="HPA_ACC_NO,HPA_ACC_CRE_DT" OnSelectedIndexChanged="grvMpdalPopUpAccDet_SelectedIndexChanged">
                    <Columns>
                        <asp:CommandField SelectText="select" ShowSelectButton="True" />
                        <asp:BoundField DataField="HPA_ACC_NO" HeaderText="Account No" />
                        <asp:BoundField DataField="HPA_ACC_CRE_DT" HeaderText="Created Date" DataFormatString='<%$ appSettings:FormatToDate %>' />
                    </Columns>
                </asp:GridView>
            </asp:Panel>
        </asp:Panel>
    </div>
    <%--Modal popup refund history--%>
    <asp:ModalPopupExtender ID="mpeRefundHistory" runat="server" CancelControlID="imgRefundHistoryClose"
        ClientIDMode="Static" PopupControlID="pnlRefundHistoryHeader" TargetControlID="btnHidden_popup"
        PopupDragHandleControlID="divRefundHistoryHeader" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <div style="float: left; width: 100%; padding-top: 2px; padding-bottom: 2px;">
        <asp:Panel ID="pnlRefundHistoryHeader" runat="server" Width="400px" BackColor="#E6E6E6"
            BorderStyle="Solid" BorderColor="Black" BorderWidth="1px">
            <div id="divRefundHistoryHeader" runat="server" class="popUpHeader">
                <div style="float: left; width: 90%" runat="server" id="div2">
                    Enter the refund amount ....
                </div>
                <div style="float: left; width: 10%; text-align: right">
                    <asp:ImageButton ID="imgRefundHistoryClose" runat="server" ImageUrl="~/Images/0032-01-16x16x16.ICO" />&nbsp;
                </div>
            </div>
            <div style="float: left; width: 94.5%; padding-top: 0px; padding-bottom: 3px;">
                <div style="float: left; width: 1%;">
                    &nbsp;
                </div>
                <div style="float: left; width: 80%; text-align: left; color: Red">
                    <asp:Label ID="lblRefundHistoryMsg" runat="server"></asp:Label>
                </div>
            </div>
            <asp:Panel ID="pnlRefundHistory" runat="server" ScrollBars="Vertical" Width="99%">
                <asp:GridView runat="server" ID="grvRefundHistory" AutoGenerateColumns="False" Width="99%"
                    ShowHeaderWhenEmpty="True">
                    <EmptyDataTemplate>
                        <div style="width: 100%; text-align: center;">
                            No data found
                        </div>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField='SAR_RECEIPT_NO' HeaderText='Refund No' HeaderStyle-Width="100px"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField='SAR_RECEIPT_DATE' HeaderText='Refund Date' HeaderStyle-Width="80px"
                            DataFormatString='<%$ appSettings:FormatToDate %>' HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left" Width="80px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField='SAR_PREFIX' HeaderText='Prefix' HeaderStyle-Width="50px"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Left" Width="50px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField='SAR_TOT_SETTLE_AMT' HeaderText='Refund Amount' HeaderStyle-Width="100px"
                            HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString='<%$ appSettings:FormatToCurrency %>'>
                            <HeaderStyle HorizontalAlign="Right" Width="100px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>
            <div style="float: left; width: 94.5%; padding-top: 0px; padding-bottom: 0px; border-top-style: inherit">
                <div style="float: left; width: 68%; text-align: right">
                    Refund Total :
                </div>
                <div style="float: left; width: 31%; text-align: right">
                    <asp:Label ID="lblRefundTot" runat="server" Text="0.00"></asp:Label>
                </div>
            </div>
            <div style="float: left; width: 94.5%; padding-top: 0px; padding-bottom: 0px;">
                <div style="float: left; width: 68%; text-align: right">
                    Balance Amount :
                </div>
                <div style="float: left; width: 31%; text-align: right">
                    <asp:Label ID="lblRefundBalAmt" runat="server" Text="0.00"></asp:Label>
                </div>
            </div>
            <div style="float: left; width: 94.5%; padding-top: 0px; padding-bottom: 4px;">
                <div style="float: left; width: 68%; text-align: right">
                    Refund Amount :
                </div>
                <div style="float: left; width: 31%; text-align: right">
                    <asp:TextBox ID="txtRefundAmt" runat="server" Text="0.00" CssClass="TextBoxNumeric"
                        Width="95%"></asp:TextBox>
                </div>
            </div>
            <div style="float: left; width: 94.5%; padding-top: 0px; padding-bottom: 4px;">
                <div style="float: left; width: 68%; text-align: left; color: Blue">
                    Selected Receipt is
                    <asp:Label ID="lblSelectedReceipt" runat="server"></asp:Label>
                </div>
                <div style="float: left; width: 31%; text-align: right">
                    <asp:Button ID="btnRefundAmt" runat="server" Text="Apply" CssClass="Button" OnClick="btnRefundAmt_Click" />
                </div>
            </div>
        </asp:Panel>
    </div>
    <%-- Control Area --%>
    <div style="display: none;">
        <asp:Button ID="btnHidden_popup" runat="server" Text="Button" />
    </div>
</asp:Content>
