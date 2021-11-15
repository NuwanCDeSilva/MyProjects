<%@ Page Title="HP Adjustment" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="HPAdjustment.aspx.cs" Inherits="FF.WebERPClient.HP_Module.HPAdjustment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControls/uc_HpAccountDetail.ascx" TagName="uc_HpAccountDetail"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/uc_HpAccountSummary.ascx" TagName="uc_HpAccountSummary"
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updtMainPnl" runat="server">
        <ContentTemplate>
            <div style="float: left; width: 100%;">
                <div style="float: left; width: 100%; height: 22px; text-align: right;">
                    <asp:Button ID="btnCreate" runat="server" Text="Create" Height="100%" Width="70px"
                        CssClass="Button" onclick="btnCreate_Click" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" Height="100%" Width="70px"
                        CssClass="Button" OnClick="btnClear_Click" />
                    <asp:Button ID="btnClose" runat="server" Text="Close" Height="100%" Width="70px"
                        CssClass="Button" OnClick="btnClose_Click" />
                </div>
                <div style="float: left; width: 100%; padding-bottom: 2px;">
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 12%;">
                        Adjustment Date
                    </div>
                    <div style="float: left; width: 1%;">
                        :</div>
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 12%;">
                        <asp:TextBox ID="txtDate" runat="server" CssClass="TextBox" Width="70%" Enabled="false"></asp:TextBox>
                        <asp:Image ID="imgDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                            Format="dd/MMM/yyyy" PopupButtonID="imgDate" Enabled="false">
                        </asp:CalendarExtender>
                    </div>
                    <div style="float: left; width: 2%;">
                        &nbsp;&nbsp;</div>
                    <div style="float: left; width: 12%;">
                        Adjustment Type
                    </div>
                    <div style="float: left; width: 1%;">
                        :</div>
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 25%; text-align: left;">
                        <asp:DropDownList ID="ddlType" runat="server" CssClass="ComboBox" Width="100%" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div style="float: left; width: 100%; padding-top: 4px; padding-bottom: 1px; padding-left: 2px;">
                    <div style="float: left; width: 50%; padding-top: 1px">
                        <asp:Panel ID="pnlMainAcc" Width="100%" runat="server" GroupingText="Master Account Details"
                            ForeColor="Black">
                            <div style="float: left; width: 100%; padding-top: 1px;">
                                <div style="float: left; width: 1%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 23%;">
                                    Account No
                                </div>
                                <div style="float: left; width: 1%;">
                                    :</div>
                                <div style="float: left; width: 1%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 20%;">
                                    <asp:TextBox ID="txtAccountNo" runat="server" CssClass="TextBox" Width="98%"></asp:TextBox>
                                </div>
                                <div style="float: left; width: 1%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 50%;">
                                    <asp:ImageButton ID="imgMasterAccNo" runat="server" ImageUrl="~/Images/icon_search.png"
                                        ImageAlign="Middle" Visible ="false"/>
                                    <asp:Label ID="lblAccountNo" runat="server" Width="90%" ForeColor="IndianRed"></asp:Label>
                                </div>
                            </div>
                            <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                <div style="float: left; width: 1%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 23%;">
                                    Manual Ref.
                                </div>
                                <div style="float: left; width: 1%;">
                                    :</div>
                                <div style="float: left; width: 1%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 50%; text-align: left;">
                                    <asp:TextBox ID="txtManualRef" runat="server" CssClass="TextBox" Width="90%"></asp:TextBox>
                                </div>
                            </div>
                            <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                <div style="float: left; width: 1%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 23%;">
                                    Credit Amount
                                </div>
                                <div style="float: left; width: 1%;">
                                    :</div>
                                <div style="float: left; width: 1%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 25%; text-align: left;">
                                    <asp:TextBox ID="txtCredit" runat="server" CssClass="TextBox" Width="98%" onkeypress="return isNumberKeyAndDot(event,value)" Style="text-align: right;"></asp:TextBox>
                                </div>
                            </div>
                            <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                <div style="float: left; width: 1%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 23%;">
                                    Debit Amount
                                </div>
                                <div style="float: left; width: 1%;">
                                    :</div>
                                <div style="float: left; width: 1%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 25%; text-align: left;">
                                    <asp:TextBox ID="txtDebit" runat="server" CssClass="TextBox" Width="98%" onkeypress="return isNumberKeyAndDot(event,value)" Style="text-align: right;"></asp:TextBox>
                                </div>
                            </div>
                            <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                <div style="float: left; width: 1%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 23%;">
                                    Remarks
                                </div>
                                <div style="float: left; width: 1%;">
                                    :</div>
                                <div style="float: left; width: 1%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 73%; text-align: left;">
                                    <asp:TextBox ID="txtRematks" runat="server" CssClass="TextBox" Width="98%" MaxLength = "200"></asp:TextBox>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <div style="float: right; width: 49%; padding-top: 1px">
                        <asp:Panel ID="pnlOtherAcc" Width="100%" runat="server" ForeColor="Black" GroupingText="Other Account Details">
                            <div style="float: left; width: 100%; padding-top: 1px;">
                                <div style="float: left; width: 1%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 23%;">
                                    Account No
                                </div>
                                <div style="float: left; width: 1%;">
                                    :</div>
                                <div style="float: left; width: 1%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 20%;">
                                    <asp:TextBox ID="txtOthAcc" runat="server" CssClass="TextBox" Width="98%"></asp:TextBox>
                                </div>
                                <div style="float: left; width: 1%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 50%;">
                                    <asp:ImageButton ID="imgOthAcc" runat="server" ImageUrl="~/Images/icon_search.png"
                                        ImageAlign="Middle" Visible = "false" />
                                    <asp:Label ID="lblOthAcc" runat="server" Width="90%" ForeColor="IndianRed"></asp:Label>
                                </div>
                            </div>
                            <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                <div style="float: left; width: 1%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 23%;">
                                    Manual Ref.
                                </div>
                                <div style="float: left; width: 1%;">
                                    :</div>
                                <div style="float: left; width: 1%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 50%; text-align: left;">
                                    <asp:TextBox ID="txtOthManual" runat="server" CssClass="TextBox" Width="90%"></asp:TextBox>
                                </div>
                            </div>
                            <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                <div style="float: left; width: 1%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 23%;">
                                    Credit Amount
                                </div>
                                <div style="float: left; width: 1%;">
                                    :</div>
                                <div style="float: left; width: 1%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 25%; text-align: left;">
                                    <asp:TextBox ID="txtOthCrAmt" runat="server" CssClass="TextBox" Width="98%" onkeypress="return isNumberKeyAndDot(event,value)" Style="text-align: right;"></asp:TextBox>
                                </div>
                            </div>
                            <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                <div style="float: left; width: 1%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 23%;">
                                    Debit Amount
                                </div>
                                <div style="float: left; width: 1%;">
                                    :</div>
                                <div style="float: left; width: 1%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 25%; text-align: left;">
                                    <asp:TextBox ID="txtOthDeAmt" runat="server" CssClass="TextBox" Width="98%" onkeypress="return isNumberKeyAndDot(event,value)" Style="text-align: right;"></asp:TextBox>
                                </div>
                            </div>
                            <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                <div style="float: left; width: 1%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 23%;">
                                    Remarks
                                </div>
                                <div style="float: left; width: 1%;">
                                    :</div>
                                <div style="float: left; width: 1%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 73%; text-align: left;">
                                    <asp:TextBox ID="txtOthRemarks" runat="server" CssClass="TextBox" Width="98%" MaxLength = "200"></asp:TextBox>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <div style="float: left; width: 50%; padding-top: 1px; padding-left :2px">
                    <asp:Panel ID="pnlMasterSummary" Width="100%" runat="server" ForeColor="Black" GroupingText="Master Account Summary">
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 100%; padding-top: 1px">
                            <uc2:uc_HpAccountSummary ID="uc_HpAccountSummary1" runat="server" />
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 100%; padding-top: 1px">
                            <uc1:uc_HpAccountDetail ID="uc_HpAccountDetail1" runat="server" />
                        </div>
                    </asp:Panel>
                </div>
                <div style="float: right; width: 49%; padding-top: 1px">
                    <asp:Panel ID="pnlOtherSummary" Width="100%" runat="server" ForeColor="Black" GroupingText="Other Account Summary">
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 100%; padding-top: 1px">
                            <uc2:uc_HpAccountSummary ID="uc_HpAccountSummary2" runat="server" />
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 100%; padding-top: 1px">
                            <uc1:uc_HpAccountDetail ID="uc_HpAccountDetail2" runat="server" />
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <div style="float: left; width: 100%; padding-top: 2px; padding-bottom: 2px; background-color: #EEEEEE;">
                <asp:Panel ID="Panel_popUp" runat="server" Width="500px" BackColor="#D8D8D8" BorderColor="Black"
                    BorderWidth="1px">
                    <div style="text-align: right">
                        <asp:Button ID="btnPopupCancel" runat="server" Text="close" CssClass="Button" /></div>
                    <asp:Panel ID="PanelPopup_grv" runat="server" ScrollBars="Vertical" Height="80px"
                        Width="460px">
                        <asp:GridView ID="grvMpdalPopUp" runat="server" AutoGenerateColumns="False" DataKeyNames="HPA_ACC_NO,HPA_ACC_CRE_DT"
                            OnSelectedIndexChanged="grvMpdalPopUp_SelectedIndexChanged" Style="text-align: left">
                            <Columns>
                                <asp:CommandField SelectText="select" ShowSelectButton="True" />
                                <asp:BoundField DataField="HPA_ACC_NO" HeaderText="Account No." />
                                <asp:BoundField DataField="HPA_ACC_CRE_DT" HeaderText="Created Date" DataFormatString="{0:d}" />
                                <asp:BoundField HeaderText="Customer Name" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </asp:Panel>
            </div>
            <asp:ModalPopupExtender ID="ModalPopupExtItem" runat="server" CancelControlID="btnPopupCancel"
                ClientIDMode="Static" PopupControlID="Panel_popUp" TargetControlID="btnHidden_popup">
            </asp:ModalPopupExtender>
            <div style="display: none">
                <asp:Button ID="btnHidden_popup" runat="server" Text="Button" />
                <asp:Button ID="btnHiddenView" runat="server" Text="Button" />
                <asp:Button ID="btn_validateAcc" runat="server" CssClass="Button" OnClick="btn_validateAcc_Click" />
                <asp:Button ID="btn_validateOthAcc" runat="server" CssClass="Button" OnClick="btn_validateOthAcc_Click" />
                <asp:Button ID="btnCrAmt" runat="server" CssClass="Button" OnClick="btnCrAmt_Click" />
                <asp:Button ID="btnDrAmt" runat="server" CssClass="Button" OnClick="btnDrAmt_Click" />
                <asp:Button ID="btnOthCrAmt" runat="server" CssClass="Button" OnClick="btnOthCrAmt_Click" />
                <asp:Button ID="btnOthDrAmt" runat="server" CssClass="Button" OnClick="btnOthDrAmt_Click" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
