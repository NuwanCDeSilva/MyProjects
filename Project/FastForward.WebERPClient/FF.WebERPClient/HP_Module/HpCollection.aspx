<%@ Page Title="Hp Collection" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="HpCollection.aspx.cs" Inherits="FF.WebERPClient.HP_Module.HpCollection" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControls/uc_HpAccountSummary.ascx" TagName="uc_HpAccountSummary"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/uc_HpAccountDetail.ascx" TagName="uc_HpAccountDetail"
    TagPrefix="uc2" %>
<%@ Register Src="../UserControls/uc_ViewApprovedRequests.ascx" TagName="uc_ViewApprovedRequests"
    TagPrefix="uc3" %>
<%@ Register Src="../UserControls/uc_HPReminder.ascx" TagName="uc_HPReminder" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .GridView
        {
        }
        .style57
        {
            font-family: Verdana;
            font-size: 10px;
        }
        .style58
        {
            font-size: 10px;
            font-family: Verdana;
            border: 1px solid Black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updtMainPnl" runat="server">
        <ContentTemplate>
            <script type="text/javascript">
                function onblurFire(e, buttonid) {
                    var evt = e ? e : window.event;
                    var bt = document.getElementById(buttonid);
                    if (bt) {

                        bt.click();
                        return false;


                    }
                }

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
            <div style="text-align: right">
                <asp:Button ID="btnSave" runat="server" CssClass="Button" OnClick="btnSave_Click"
                    Text="Save" />&nbsp;
                <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="Button" OnClick="btnEdit_Click" />
                &nbsp;<asp:Button ID="btnClear" runat="server" CssClass="Button" Text="Clear" OnClick="btnClear_Click" />
                &nbsp;<asp:Button ID="btnClose" runat="server" Text="Close" CssClass="Button" OnClick="btnClose_Click" />
            </div>
            <div id="divMain" style="color: Black;">
                <div style="float: left; width: 2%;">
                    &nbsp;</div>
                <%--<div style="float: left; width: 45%;">
                    Entry Date:&nbsp;&nbsp; 
                    <asp:Label ID="lblEntryDate" runat="server" Text="dd/mm/yyyy"></asp:Label>
                </div>--%>
                <div id="div_ReciptDet" style="float: left; width: 50%; color: Navy;">
                    <asp:Panel ID="Panel_ReciptDetails" runat="server" GroupingText=" ">  
                    <div>
                        <div style="float: left; width: 2%;">
                                &nbsp;</div>
                    </div>                    
                        <%--<div style="float: left; width: 45%;">
                            Entry Date:&nbsp;&nbsp; 
                            <asp:Label ID="lblEntryDate" runat="server" Text="dd/mm/yyyy"></asp:Label>
                        </div>--%>                       
                        <div style="float: left; width: 100%;">
                            <div style="padding: 2px; float: left; width: 2%;">
                                &nbsp;</div>
                            <div style="float: left; width: 96%;">
                                <div style="float: left; width: 18%;">
                                    <asp:Label ID="Label2" runat="server" Text="Receipt Date: "></asp:Label>
                                </div>
                                <div style="float: left; width: 25%;">
                                    <asp:TextBox ID="txtReceiptDate" runat="server" CssClass="TextBox" Enabled="False"
                                        Width="100%"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        TargetControlID="txtReceiptDate">
                                    </asp:CalendarExtender>
                                </div>
                                <div style="float: left; width: 5%;"> 
                                &nbsp;
                                </div>
                                <div style="float: left; width: 40%;">
                                    <%--Entry Date: --%>
                                    <asp:Label ID="lblEntryDate" runat="server" Text="dd/mm/yyyy" Style="text-align: right"
                                        ForeColor="White"></asp:Label>
                                </div>
                            </div>
                            <div style="float: left; width: 2%;">
                                &nbsp;</div>
                        </div>
                        <div style="float: left; width: 100%;">
                            <div style="float: left; width: 2%;">
                                &nbsp;</div>
                            <div style="float: left; width: 96%;">
                                <div style="float: left; width: 18%;">
                                    <asp:Label ID="Label3" runat="server" Text="Profit Center: "></asp:Label>
                                </div>
                                <asp:DropDownList ID="ddl_Location" runat="server" CssClass="style1" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddl_Location_SelectedIndexChanged" Width="65px">
                                </asp:DropDownList>
                                &nbsp;<asp:ImageButton ID="ImgBtnPC" runat="server" ImageUrl="~/Images/icon_search.png"
                                    OnClick="ImgBtnPC_Click" />
                            </div>
                            <div style="float: left; width: 2%;">
                                &nbsp;</div>
                        </div>
                        <div style="float: left; width: 100%;">
                            <div style="float: left; width: 2%;">
                                &nbsp;</div>
                            <div style="float: left; width: 96%;">
                                <div style="float: left; width: 18%;">
                                    <asp:Label ID="Label4" runat="server" Text="Account No:  "></asp:Label>
                                </div>
                                <asp:TextBox ID="txtAccountNo" runat="server" CssClass="TextBox"></asp:TextBox>
                                &nbsp;<asp:ImageButton ID="ImgBtnAccountNo" runat="server" ImageUrl="~/Images/icon_search.png"
                                    OnClick="ImgBtnAccountNo_Click" />
                                <asp:Button ID="btn_validateACC" runat="server" CssClass="Button" OnClick="btn_validateACC_Click"
                                    Text="..." />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblAccNo" runat="server" Font-Bold="True"></asp:Label>
                                &nbsp;</div>
                            <div style="float: left; width: 2%;">
                                &nbsp;</div>
                        </div>
                        <div style="float: left; width: 100%;">
                            <div style="float: left; width: 2%;">
                                &nbsp;</div>
                            <div style="float: left; width: 96%;">
                                &nbsp;</div>
                            <div style="float: left; width: 2%;">
                                &nbsp;</div>
                        </div>
                        <div style="float: left; width: 100%;">
                            <div style="float: left; width: 2%;">
                                &nbsp;</div>
                            <div style="float: left; width: 96%;">
                                <div style="float: left; width: 20%; text-align: right;">
                                    &nbsp;Type:</div>
                                <div style="float: left; width: 25%;">
                                    &nbsp;
                                    <asp:RadioButton ID="rdoBtnSystem" runat="server" Text="System" GroupName="Rdo_Type"
                                        AutoPostBack="True" Checked="True" OnCheckedChanged="rdoBtnSystem_CheckedChanged" />
                                </div>
                                <div style="float: left; width: 18%;">
                                    &nbsp;
                                    <asp:RadioButton ID="rdoBtnManual" runat="server" Text="Manual" GroupName="Rdo_Type"
                                        AutoPostBack="True" OnCheckedChanged="rdoBtnManual_CheckedChanged" />
                                </div>
                            </div>
                            <div style="float: left; width: 2%;">
                                &nbsp;</div>
                        </div>
                        <div style="float: left; width: 100%;">
                            <div style="float: left; width: 2%;">
                                &nbsp;</div>
                            <div style="float: left; width: 96%;">
                                <div style="float: left; width: 20%; text-align: right;">
                                    &nbsp;<asp:Label ID="Label1" runat="server" Text="Payed By :"></asp:Label>
                                </div>
                                <div style="float: left; width: 25%;">
                                    &nbsp;
                                    <asp:RadioButton ID="rdoBtnCustomer" runat="server" Text="Customer" GroupName="Rdo_IssueBy"
                                        AutoPostBack="True" Checked="True" />
                                </div>
                                <div style="float: left; width: 18%;">
                                    &nbsp;
                                    <asp:RadioButton ID="rdoBtnManager" runat="server" Text="Manager" GroupName="Rdo_IssueBy"
                                        AutoPostBack="True" />
                                </div>
                            </div>
                           <%-- <div style="padding: 1px; float: left; width: 2%;">
                                &nbsp;</div>--%>
                        </div>
                        <div style="float: left; width: 100%;">
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 96%;">
                                <asp:Panel ID="Panel_ECD" runat="server" GroupingText="ECD Info">
                                    <%--<div style="float: left; width: 100%;">
                             <div style="float: left; width: 2%;">&nbsp;</div>
                             <div style="float: left; width: 96%;">&nbsp;</div>
                              <div style="float: left; width: 2%;">&nbsp;</div>
                             </div>--%>
                                    <div style="padding: 5px; float: left; width: 100%;">
                                        <div style="padding: 0.5px; float: left; width: 33%;">
                                            <asp:Label ID="Label5" runat="server" Text="ECD Type : " Style="font-family: Verdana;
                                                font-size: 10px"></asp:Label>
                                            <asp:DropDownList ID="ddlECDType" runat="server" Style="font-size: 10px; font-family: Verdana"
                                                AutoPostBack="True" CssClass="ComboBox" OnSelectedIndexChanged="ddlECDType_SelectedIndexChanged">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem>Normal</asp:ListItem>
                                                <asp:ListItem>Special</asp:ListItem>
                                                <asp:ListItem>Voucher</asp:ListItem>
                                                <asp:ListItem>Custom</asp:ListItem>
                                                <asp:ListItem>Approved Req.</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div style="float: left; width: 65%;">
                                            <asp:Panel ID="Panel_voucher" runat="server" Width="100%">
                                                <div style="float: left; width: 100%;">
                                                    <div style="float: left; width: 40%;">
                                                        <asp:Label ID="Label6" runat="server" Text="Voucher #" Style="font-size: 10px; font-family: Verdana"></asp:Label>
                                                        <asp:TextBox ID="txtVoucherNum" runat="server" CssClass="style58" Width="80px"></asp:TextBox>
                                                    </div>
                                                    <div style="float: left; width: 40%;">
                                                        <asp:Label ID="Label7" runat="server" Text="Amount" CssClass="style57"></asp:Label>
                                                        <asp:TextBox ID="txtVoucherAmt" runat="server" CssClass="style58" Enabled="False"
                                                            Width="80px"></asp:TextBox>
                                                    </div>
                                                    <%--                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" 
                                    Format="dd/MM/yyyy" TargetControlID="txtVoucherDt">
                                </asp:CalendarExtender>--%>
                                                    &nbsp;<div style="float: left; width: 100%;">
                                                        <div style="float: left; width: 50%;">
                                                            &nbsp;</div>
                                                        <div style="float: left; width: 49%;">
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div style="float: left; width: 100%;" id="divCustomRequest" runat="server">
                                            <asp:Panel ID="Panel_ecdCustom" runat="server">
                                                <div style="padding: 0.5px; float: left; width: 100%;">
                                                    <div style="padding: 0.5px; float: left; width: 65%;">
                                                        <asp:Label ID="Label12" runat="server" Text="ECD amount : "></asp:Label>
                                                        <asp:TextBox ID="txtReques" runat="server" CssClass="TextBox" Width="35%"></asp:TextBox>
                                                        <div style="padding: 0.5px; float: left; width: 1%;">
                                                        </div>
                                                        <asp:CheckBox ID="chkIsECDrate" runat="server" Checked="True" Text="Is rate" />
                                                    </div>
                                                    <div style="padding: 0.5px; float: left; width: 25%; text-align: left;">
                                                        <asp:Button ID="btnSendEcdReq" runat="server" Text="Submit Request" CssClass="Button"
                                                            Height="17px" OnClick="btnSendEcdReq_Click" Width="100%" />
                                                        <div style="padding: 1px; float: left; width: 100%; text-align: left;">
                                                        </div>
                                                        <asp:Label ID="Label9" runat="server" Text="Get Pendings :"></asp:Label>
                                                        <asp:DropDownList ID="ddlPendinReqNo" runat="server" CssClass="ComboBox" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlPendinReqNo_SelectedIndexChanged" Width="80%">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div style="float: left; width: 100%;" id="divApprovedReq" runat="server">
                                            <asp:Panel ID="Panel2" runat="server">
                                                <div style="padding: 0.5px; float: left; width: 100%;" id="divECDReqbal" runat="server">
                                                    &nbsp; &nbsp;</div>
                                                <div style="padding: 0.5px; float: left; width: 100%;" id="divECDbal" runat="server">
                                                    <asp:Label ID="Label11" runat="server" Font-Bold="True" Text="ECD Balance : "></asp:Label>
                                                    &nbsp;<asp:Label ID="lblECD_Balance" runat="server"></asp:Label>
                                                </div>
                                                <div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                    <%--  <div  style="float: left; width: 100%;">&nbsp;&nbsp;</div>--%>
                                </asp:Panel>
                            </div>
                            <div style="padding: 0.5px; float: left; width: 2%;">
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
                            <asp:Button ID="btnCancelRecipt" runat="server" CssClass="Button" Height="15px" Text="cancel receipt"
                                Font-Size="Smaller" OnClick="btnCancelRecipt_Click" />
                        </div>
                        <div style="float: left; width: 100%; font-weight: normal; padding-top: 0px; padding-bottom: 0px;">
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
                                    OnClick="ImgBtnAddReceipt_Click" Width="16px" />
                            </div>
                            <%--  <div style="float: left; width: 72px; text-align: right; border-right: 1px solid white;">
                                    </div>
                                <div style="float: left; width: 72px; text-align: right; border-right: 1px solid white;">
                                    </div>
                                <div style="float: left; width: 1%;">
                                    &nbsp;
                                  
                                </div>--%>
                            <div style="float: left; width: 96%; height: 81px;">
                                <asp:Panel ID="Panel_ReceiptDet" runat="server" Height="80px" 
                                    ScrollBars="Vertical">
                                    <asp:GridView ID="gvReceipts" runat="server" AutoGenerateColumns="False" CellPadding="1"
                                        CssClass="GridView" DataKeyNames="Sar_manual_ref_no,Sar_prefix" ForeColor="#333333"
                                        GridLines="Horizontal" OnRowDeleting="gvReceipts_RowDeleting" ShowHeader="False"
                                        Width="240px">
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
                                            <asp:BoundField DataField="Sar_tot_settle_amt" HeaderText="Amount" ItemStyle-HorizontalAlign="Right">
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
                            <div style="float: left; width: 99%;">
                                  <div style="float: left; width: 10%;">&nbsp;</div>
                                <div style="float: left; width: 25%;">
                                    <asp:Label ID="Label13" runat="server" Text="Vehicle Insu."></asp:Label>
                                </div>
                                <div style="float: left; width: 25%;">
                                    <asp:Label ID="Label14" runat="server" Text="Diriya Insu."></asp:Label>
                                </div>
                                <div style="float: left; width: 25%;">
                                    <asp:Label ID="Label15" runat="server" Text="Collection"></asp:Label>
                                </div>
                            </div>
                            <div style="float: left; width: 99%;">
                              <div style="float: left; width: 10%;">Arrers:</div>
                                <div style="float: left; width: 25%;">
                                    <asp:Label ID="lblVehInsu_old" runat="server"></asp:Label>
                                </div>
                                <div style="float: left; width: 25%;">
                                    <asp:Label ID="lblDiriyaInsu_old" runat="server"></asp:Label>
                                </div>
                                <div style="float: left; width: 25%;">
                                    <asp:Label ID="lblCollection_old" runat="server"></asp:Label>
                                </div>
                            </div>
                             <div style="float: left; width: 99%;">
                             <div style="float: left; width: 10%;">
                                current:
                             </div>
                                <div style="float: left; width: 25%;">                                
                                    <asp:Label ID="lblCurVehInsDue" runat="server"></asp:Label>
                                </div>
                                <div style="float: left; width: 25%;">
                                    <asp:Label ID="lblCurInsDue" runat="server"></asp:Label>
                                </div>
                                <div style="float: left; width: 25%;">
                                    <asp:Label ID="lblCurCollDue" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div style="float: left; width: 99%;">
                              <div style="float: left; width: 10%;">&nbsp;</div>
                                <div style="float: left; width: 25%;">
                                    <asp:TextBox ID="txtVehInsurNew" runat="server" CssClass="TextBoxNumeric" Width="80%"></asp:TextBox>
                                </div>
                                <div style="float: left; width: 25%;">
                                    <asp:TextBox ID="txtDiriyaNew" runat="server" CssClass="TextBoxNumeric" Width="80%"></asp:TextBox>
                                </div>
                                <div style="float: left; width: 25%;">
                                    <asp:TextBox ID="txtCollectionNew" runat="server" CssClass="TextBoxNumeric" Width="80%"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div style="float: left; width: 1%;">
                    &nbsp;</div>
                <%--Advance receipt/Credit Note payment--%>
                <div id="div_AccountSummary" style="float: left; width: 45%; color: Navy;">
                    <asp:Panel ID="Panel_AccountSummary" runat="server" GroupingText="Account Summary">
                        <uc1:uc_HpAccountSummary ID="uc_HpAccountSummary1" runat="server" />
                        <div>
                            &nbsp;<asp:Label ID="Label8" runat="server" ForeColor="White" Text="-"></asp:Label>
                            &nbsp;&nbsp;
                        </div>
                        <asp:Panel ID="Panel_uc_AccDet" runat="server" GroupingText="Account Detail" Width="420px">
                            <asp:Panel ID="Panel1" runat="server" Height="114px">
                                <uc2:uc_HpAccountDetail ID="uc_HpAccountDetail1" runat="server" />
                            </asp:Panel>
                        </asp:Panel>
                    </asp:Panel>
                </div>
                <%--Credit/Cheque/Bank Slip payment--%>
                <div style="float: left; width: 2%;">
                    &nbsp;</div>
                <%--Advance receipt/Credit Note payment--%>
                <div style="float: left; width: 100%; padding-top: 2px; padding-bottom: 2px; background-color: #EEEEEE;">
                    <%--Credit/Cheque/Bank Slip payment--%>
                    <div style="height: 16px; background-color: #1E4A9F; color: White; width: 98%; float: left;">
                        <%--Advance receipt/Credit Note payment--%>Payment Details</div>
                    <%--Credit/Cheque/Bank Slip payment--%>
                    <div style="float: left;">
                        <asp:Image runat="server" ID="Image4" ImageUrl="~/Images/16 X 16 DownArrow.jpg" ImageAlign="Right" /></div>
                    <%--Advance receipt/Credit Note payment--%>
                    <asp:CollapsiblePanelExtender ID="CPEPayment" runat="server" TargetControlID="pnlPayment"
                        CollapsedSize="0" ExpandedSize="155" Collapsed="False" ExpandControlID="Image4"
                        CollapseControlID="Image4" AutoCollapse="False" AutoExpand="False" ScrollContents="false"
                        ExpandDirection="Vertical" ImageControlID="Image4" ExpandedImage="~/Images/16 X 16 UpArrow.jpg"
                        CollapsedImage="~/Images/16 X 16 DownArrow.jpg">
                    </asp:CollapsiblePanelExtender>
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
                                        <asp:TextBox ID="txtPayAmount" runat="server" CssClass="TextBoxUpper" Width="50%"></asp:TextBox>
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
                                            <div style="float: left; width: 25%; padding-bottom: 2px;">
                                                <asp:TextBox runat="server" ID="txtPayCrBranch" CssClass="TextBox" Width="65%"></asp:TextBox>
                                                <asp:ImageButton ID="ImgBtnBranchSearch" runat="server" 
                                                    ImageUrl="~/Images/icon_search.png" onclick="ImgBtnBranchSearch_Click" />
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
                                    <div style="float: left; width: 20%; background-color: #1569C7; color: White; height: 22px;">
                                        Paid Amount
                                    </div>
                                    <div style="float: left; width: 18%; text-align: right; border-color: Black; border-style: solid;
                                        border-width: 1px;">
                                        <asp:Label runat="server" ID="lblPayPaid" Text="0.00"></asp:Label>
                                    </div>
                                    <div style="float: left; width: 18%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 20%; background-color: #1569C7; color: White; height: 22px;">
                                        Balance Amount
                                    </div>
                                    <div style="float: left; width: 18%; text-align: right; border-color: Black; border-style: solid;
                                        border-width: 1px;">
                                        <asp:Label runat="server" ID="lblPayBalance" Text="0.00"></asp:Label>
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <%--  <asp:Label ID="lblACC_BAL" runat="server" BackColor="White"></asp:Label>--%>
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
                <%--   CancelControlID="btnViewClose"--%>
                <div style="float: left; width: 100%; padding-top: 2px; padding-bottom: 2px; background-color: #EEEEEE;">
                    <%--  <asp:Label ID="lblACC_BAL" runat="server" BackColor="White"></asp:Label>--%>
                    <div style="float: left; width: 50%; padding-top: 2px; padding-bottom: 2px; background-color: #FFFFFF;">
                        <asp:Panel ID="Panel_viewApprovedReqDet" runat="server" BackColor="#CCCCCC">
                            <div style="padding: 0.5px; float: left; width: 100%; text-align: right;">
                                <asp:Button ID="btnViewClose" runat="server" Text="Close" CssClass="Button" OnClick="btnViewClose_Click" />
                            </div>
                            <div style="float: left; width: 100%;">
                                <asp:Panel ID="Panel_ucReqView" runat="server">
                                    <div>
                                        <uc3:uc_ViewApprovedRequests ID="uc_ViewApprovedRequests1" runat="server" />
                                    </div>
                                </asp:Panel>
                            </div>
                        </asp:Panel>
                    </div>
                    <%--   CancelControlID="btnViewClose"--%>
                    <div style="float: left; width: 49%; padding-top: 2px; padding-bottom: 2px; background-color: #EEEEEE;">
                    </div>
                </div>
                <div>
                    <uc4:uc_HPReminder ID="uc_HPReminder1" runat="server" />
                </div>
                <div style="display: none">
                    <asp:Button ID="btnHidden_popup" runat="server" Text="Button" />
                    <%--  <asp:Label ID="lblACC_BAL" runat="server" BackColor="White"></asp:Label>--%>
                    <asp:Label ID="lblApprovedReqVal" runat="server" Text="Label"></asp:Label>
                    <asp:Button ID="btnVoucherValidate" runat="server" Text="Voucher validate" OnClick="btnVoucherValidate_Click" />
                    <asp:Button ID="btnReceiptEnter" runat="server" Text="Button" OnClick="btnReceiptEnter_Click" />
                    <asp:Button ID="btnHiddenView" runat="server" Text="Button" OnClick="btnHiddenView_Click" />
                    <asp:Button ID="btnCollCal" runat="server" Text="Button" OnClick="btnCollCal_Click" />
                </div>
                <asp:ModalPopupExtender ID="ModalPopupExtViewAppr" runat="server" ClientIDMode="Static"
                    BehaviorID="btnViewClose" BackgroundCssClass="modalBackground" PopupControlID="Panel_viewApprovedReqDet"
                    TargetControlID="btnHiddenView">
                </asp:ModalPopupExtender>
                <asp:ModalPopupExtender ID="ModalPopupExtItem" runat="server" CancelControlID="btnPopupCancel"
                    ClientIDMode="Static" BackgroundCssClass="modalBackground" PopupControlID="Panel_popUp"
                    TargetControlID="btnHidden_popup">
                </asp:ModalPopupExtender>
                <%--   CancelControlID="btnViewClose"--%>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
