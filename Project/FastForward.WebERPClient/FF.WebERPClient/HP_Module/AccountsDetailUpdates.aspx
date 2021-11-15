<%@ Page Title="Accounts Detail Updates" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="AccountsDetailUpdates.aspx.cs" Inherits="FF.WebERPClient.HP_Module.AccountsDetailUpdates" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControls/uc_CustomerCreation.ascx" TagName="uc_CustomerCreation"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/uc_CustCreationExternalDet.ascx" TagName="uc_CustCreationExternalDet"
    TagPrefix="uc2" %>
<%@ Register Src="../UserControls/uc_HpAccountSummary.ascx" TagName="uc_HpAccountSummary"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .GridView
        {
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
        <div style="float: left; width: 30%; visibility: hidden;">
            <asp:Button ID="btnGetCust" runat="server" Text="getcust" OnClick="btnGetCust_Click" />
            <asp:TextBox ID="txtHiddenCustCD" runat="server" OnTextChanged="txtHiddenCustCD_TextChanged"></asp:TextBox>
        </div>
        &nbsp;<asp:Button ID="btnClear" runat="server" CssClass="Button" Text="Clear" 
            onclick="btnClear_Click" />
        &nbsp;<asp:Button ID="btnClose" runat="server" Text="Close" CssClass="Button" 
            onclick="btnClose_Click" />
    </div>
    <div style="text-align: left" id="divCurDate" runat="server">
        <asp:TextBox ID="txtCurrentDt" runat="server" CssClass="TextBox"></asp:TextBox>
        &nbsp;<asp:ImageButton ID="ImgBtnCurDt" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
        <asp:CalendarExtender ID="txtFromDt_CalendarExtender" runat="server" Enabled="True"
            Format="dd/MM/yyyy" TargetControlID="txtCurrentDt" PopupButtonID="ImgBtnCurDt">
        </asp:CalendarExtender>
    </div>
    <div style="padding: 2.0px; color: Black;">
    </div>
    <div id="divMain1" style="color: Black;">
        <div style="padding: 2.0px; float: left; width: 1%;">
        </div>
        <div style="float: left; width: 100%;">
            <div style="padding: 2.0px; float: left; width: 2%;">
            </div>
            <div style="float: left; width: 40%;">
                <asp:Panel ID="Panel_serach1" runat="server" BackColor="#E5E5E5" GroupingText=" ">
                    <div style="float: left; width: 100%;">
                        <div style="padding: 0.5px; float: left; width: 96%;" align="right">
                            <asp:Button ID="btnAccOk" runat="server" CssClass="Button" OnClick="btnAccOk_Click"
                                Text="OK" ValidationGroup="dateGroup" />
                        </div>
                        <div style="float: left; width: 30%;" align="right">
                            <asp:Label ID="Label1" runat="server" Text="Profit Center :"></asp:Label>
                        </div>
                        <div style="float: left; width: 69%;">
                            <asp:TextBox ID="txtPC" runat="server" CssClass="TextBox"></asp:TextBox>
                            &nbsp;<asp:ImageButton ID="ImgBtnPC" runat="server" ImageUrl="~/Images/icon_search.png"
                                OnClick="ImgBtnPC_Click1" />
                            &nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldPCenter" runat="server" ErrorMessage="*"
                                Font-Bold="True" ForeColor="Red" ValidationGroup="dateGroup" ControlToValidate="txtPC"></asp:RequiredFieldValidator>
                        </div>
                        <div style="padding: 1.0px; float: left; width: 96%;">
                        </div>
                        <div style="padding: 3.0px; float: left; width: 96%;">
                            <asp:Label ID="Label2" runat="server" Text="Account creation period :"></asp:Label>
                        </div>
                        <div style="padding: 1.0px; float: left; width: 10%;">

                        </div>
                        <div style="float: left; width: 100%;">
                            <div style="float: left; width: 30%;" align="right">
                                <asp:Label ID="Label3" runat="server" Text="From:"></asp:Label>
                              
                            </div>
                            <div style="float: left; width: 69%;">
                                <asp:TextBox ID="txtFrmDt" runat="server" CssClass="TextBox"></asp:TextBox>
                                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldFROM" runat="server" ErrorMessage="RequiredFieldValidator"
                                    ControlToValidate="txtFrmDt" Font-Bold="True" ForeColor="Red" ValidationGroup="dateGroup">*</asp:RequiredFieldValidator>
                                <asp:CalendarExtender ID="txtFrmDt_CalendarExtender" runat="server" Enabled="True"
                                    Format="dd/MM/yyyy" TargetControlID="txtFrmDt">
                                </asp:CalendarExtender>
                            </div>
                        </div>
                        <div style="float: left; width: 100%;">
                            <div style="float: left; width: 30%;" align="right">
                                <asp:Label ID="Label4" runat="server" Text="To:"></asp:Label>
                            </div>
                            <div style="float: left; width: 69%;">
                                <asp:TextBox ID="txtToDt" runat="server" CssClass="TextBox"></asp:TextBox>
                                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldtO" runat="server" ErrorMessage="*"
                                    ControlToValidate="txtToDt" Font-Bold="True" ForeColor="Red" ValidationGroup="dateGroup"></asp:RequiredFieldValidator>
                                <asp:CalendarExtender ID="txtToDt_CalendarExtender" runat="server" Enabled="True"
                                    Format="dd/MM/yyyy" TargetControlID="txtToDt">
                                </asp:CalendarExtender>
                            </div>
                        </div>
                        <div style="padding: 0.5px; float: left; width: 96%;">
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <div style="float: left; width: 2%;">
            </div>
            <div style="float: left; width: 40%;">
                <asp:Panel ID="Panel_serach2" runat="server" BackColor="#E5E5E5" GroupingText=" ">
                    <div style="padding: 0.5px; float: left; width: 96%;" align="right">
                        <asp:Button ID="btnAccOk_2" runat="server" Text="OK" CssClass="Button" OnClick="btnAccOk_2_Click" />
                    </div>
                    <div style="float: left; width: 30%;" align="right">
                        <asp:Label ID="Label5" runat="server" Text="Account No. :"></asp:Label>
                    </div>
                    <div style="float: left; width: 69%;">
                        <asp:TextBox ID="txtAccNo" runat="server" CssClass="TextBox"></asp:TextBox>
                        &nbsp;<asp:ImageButton ID="ImgBtnAcc" runat="server" ImageUrl="~/Images/icon_search.png"
                            OnClick="ImgBtnAcc_Click" />
                    </div>
                    <div style="padding: 0.5px; float: left; width: 96%;">
                    </div>
                </asp:Panel>
            </div>
            <div style="float: left; width: 15%;" align="center">
            </div>
        </div>
    </div>
    <div id="divMain2" style="color: Black; width: 100%;">
        
       
       <div style="float: left; width: 100%;" align="left">
       <asp:Panel ID="Panel3" runat="server" GroupingText="Accounts" Width="100%">
       <div style="padding: 1.0px; float: left; width: 95%;" align="left">
            <asp:Button ID="btnAll" runat="server" Text="All" CssClass="Button" OnClick="btnAll_Click"
                Width="5%" />
            &nbsp;<asp:Button ID="btnNone" runat="server" Text="None" CssClass="Button" OnClick="btnNone_Click"
                Width="5%" />
            &nbsp;</div>
            <div style="float: left; width: 100%;" align="left">
                <asp:Panel ID="Panel1" runat="server" Height="180px" ScrollBars="Auto" GroupingText=" ">
                    <asp:GridView ID="grvAccounts" runat="server" CellPadding="4" CssClass="GridView"
                        ForeColor="#333333" Width="98%" DataKeyNames="hpa_acc_no" OnRowDeleting="grvAccounts_RowDeleting"
                        AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" 
                        OnRowDataBound="grvAccounts_RowDataBound">
                        <AlternatingRowStyle BackColor="White" />
                        <EmptyDataTemplate>
                            <div style="width: 100%; text-align: center;">
                                No data found
                            </div>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                    <asp:CheckBox ID="chkSelect" runat="server" Checked="True" ForeColor="#009933" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Account No." DataField="hpa_acc_no" />
                            <asp:BoundField DataField="hpa_sch_cd" HeaderText="Scheme" />
                            <asp:BoundField HeaderText="Name" />
                            <asp:BoundField HeaderText="Mortgaged At" DataField="hpa_bank" />
                            <asp:BoundField HeaderText="Category(Flag)" DataField="hpa_flag" />
                            <asp:BoundField HeaderText="Account Balance" DataFormatString="{0:n2}" />
                            <asp:BoundField HeaderText="Hire Value" DataField="hpa_hp_val" 
                                DataFormatString="{0:n2}" />
                            <asp:BoundField HeaderText="Arrears" />
                            <asp:TemplateField HeaderText="PC" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblAccPC" runat="server" Text='<%# Bind("hal_pc") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("hal_pc") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server"></asp:Label>
                                    <br />
                                    <asp:ImageButton ID="ImgBtnAccUC" runat="server" CommandName="Delete" 
                                        ImageUrl="~/Images/searchIcon.png" OnClick="ImgBtnAccUC_Click" />
                                </ItemTemplate>
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
                </asp:Panel>
            </div>
        </asp:Panel>
       </div>
        
    </div>
    <div id="divMain3" style="color: Black;">
        <div style="padding: 3.0px; float: left; width: 1%;">
        </div>
        <div style="float: left; width: 100%; background-color: #CCFFFF;">
            <asp:Panel ID="Panel2" runat="server">
                <div style="float: left; width: 20%;" align="center">
                    <asp:RadioButton ID="rdoMortgage" runat="server" ForeColor="Blue" GroupName="Actions"
                        Text="Mortgaged @" AutoPostBack="True" 
                        OnCheckedChanged="rdoMortgage_CheckedChanged" />
                </div>
                <div style="float: left; width: 20%;" align="center">
                    <asp:RadioButton ID="rdoCategorize" runat="server" ForeColor="Blue" GroupName="Actions"
                        Text="Categorize (flag)" AutoPostBack="True" OnCheckedChanged="rdoCategorize_CheckedChanged" />
                </div>
                <div style="float: left; width: 20%;" align="center">
                    <asp:RadioButton ID="rdoAccTransfer" runat="server" ForeColor="Blue" GroupName="Actions"
                        Text="Accounts Transfer" AutoPostBack="True" 
                        OnCheckedChanged="rdoAccTransfer_CheckedChanged" />
                </div>
                <div style="float: left; width: 20%;" align="center">
                    <asp:RadioButton ID="rdoCustTransfer" runat="server" ForeColor="Blue" GroupName="Actions"
                        Text="Customer Transfer" AutoPostBack="True" OnCheckedChanged="rdoCustTransfer_CheckedChanged" />
                </div>
                <div style="float: left; width: 19%;" align="center">
                    <asp:RadioButton ID="rdoCustDetChange" runat="server" ForeColor="Blue" GroupName="Actions"
                        Text="Customer Detail Change" AutoPostBack="True" OnCheckedChanged="rdoCustDetChange_CheckedChanged" />
                </div>
            </asp:Panel>
        </div>
        <div style="padding: 2.5px; float: left; width: 96%;">
        </div>
        <div style="border-width: thin; border-style: groove; float: left; width: 100%;"
            id="divMortgage" runat="server">
            <div style="float: left; width: 96%;" align="right">
               
            </div>
            <div style="float: left; width: 15%;" align="right">
                <asp:Label ID="Label6" runat="server" Text="Mortgage At :"></asp:Label>
            </div>
            <div style="float: left; width: 25%;">
                <asp:DropDownList ID="ddlMortgageCd" runat="server" CssClass="ComboBox">
                </asp:DropDownList>
            </div>
            <div style="float: left; width: 35%;">
            &nbsp;<asp:Button ID="btnNewMortg" runat="server" CssClass="Button" Text="create new" Font-Underline="True"
                    OnClick="btnNewMortg_Click" />
            &nbsp;
            <asp:Button ID="btnConfMorg" runat="server" Text="Confirm" CssClass="Button" OnClick="btnConfMorg_Click" />
            </div>
            <div style="float: left; width: 10%;">
                
            </div>
            <div style="padding: 4.0px; float: left; width: 95%;">
            </div>
        </div>
        <div style="border-width: thin; border-style: groove; float: left; width: 100%;"
            id="divCategorize" runat="server">
            <div style="float: left; width: 96%;" align="right">
               
            </div>
            <div style="float: left; width: 15%;" align="right">
                <asp:Label ID="Label7" runat="server" Text="Categorize/Flag At :"></asp:Label>
            </div>
            <div style="float: left; width: 25%;">
                <asp:DropDownList ID="ddlCategoCd" runat="server" CssClass="ComboBox">
                </asp:DropDownList>
            </div>
            <div style="float: left; width: 35%;">
            &nbsp;<asp:Button ID="btnNewFlag" runat="server" CssClass="Button" Font-Underline="True"
                OnClick="btnNewFlag_Click" Text="create new" />
            &nbsp;
             <asp:Button ID="btnConfCatego" runat="server" Text="Confirm" CssClass="Button" OnClick="btnConfCatego_Click" />
            </div>
            
            <div style="padding: 4.0px; float: left; width: 95%;">
            </div>
        </div>
        <div style="border-width: thin; border-style: groove; float: left; width: 100%;"
            id="divAccTrans" runat="server">
            <div style="float: left; width: 96%;" align="right">
               
            </div>
            <div style="float: left; width: 15%;" align="right">
                <asp:Label ID="Label8" runat="server" Text="New Profit Center :"></asp:Label>
            </div>
            <div style="float: left; width: 25%;">
                <asp:TextBox ID="txtTrPC" runat="server" CssClass="TextBox"></asp:TextBox>
                &nbsp;<asp:ImageButton ID="ImgBtnNewPC" runat="server" ImageUrl="~/Images/icon_search.png"
                    OnClick="ImgBtnNewPC_Click" />
            </div>
            <div style="float: left; width: 25%;">
                <asp:Label ID="lblNewPcDes" runat="server" Text=""></asp:Label>
                <asp:Button ID="btnConfAccTr" runat="server" CssClass="Button" 
                    OnClick="btnConfAccTr_Click" Text="Confirm" ValidationGroup="newPcGroup" />
            </div>
            <div style="float: left; width: 24%;">
                <asp:RequiredFieldValidator ID="RequiredFieldNewPc" runat="server" ErrorMessage="Enter Profit center"
                    ControlToValidate="txtTrPC" ForeColor="Red" ValidationGroup="newPcGroup"></asp:RequiredFieldValidator>
            </div>
            <div style="padding: 4.0px; float: left; width: 95%;">
            </div>
        </div>
        <div style="border-width: thin; border-style: groove; float: left; width: 100%;"
            id="divCustTrans" runat="server">
            <div style="float: left; width: 96%;" align="right">
            </div>
            <div style="float: left; width: 15%;" align="right">
                <asp:Label ID="Label9" runat="server" Text="Customer Code :"></asp:Label>
            </div>
            <div style="float: left; width: 25%;">
                <asp:TextBox ID="txtCustCode" runat="server" CssClass="TextBox"></asp:TextBox>
                &nbsp;
                <asp:ImageButton ID="ImgBtnCust" runat="server" ImageUrl="~/Images/icon_search.png"
                    OnClick="ImgBtnCust_Click" />
            </div>
            <div style="float: left; width: 25%;">
                &nbsp;<asp:Button ID="btnNewCust" runat="server" CssClass="Button" Text="create new" Font-Underline="True"
                    OnClick="btnNewCust_Click" />
            &nbsp;
                <asp:Button ID="btnConfCustTr" runat="server" Text="Confirm" CssClass="Button" OnClick="btnConfCustTr_Click"
                    ValidationGroup="AddressValid" />
            </div>
            <div style="float: left; width: 24%;">
                <asp:RequiredFieldValidator ID="RequiredFieldCustCD" runat="server" ErrorMessage="Enter new Customer Code"
                    ValidationGroup="AddressValid" ControlToValidate="txtCustCode" Font-Bold="False"
                    ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <div style="padding: 4.0px; float: left; width: 95%;">
            </div>
            <div style="float: left; width: 50%;">
                <asp:Panel ID="Panel_DelvrAddr" runat="server" GroupingText="Product Available Address"
                    Style="font-family: Verdana; font-size: Small;" ForeColor="Blue">
                    <div style="padding: 2.0px; float: left; width: 96%;">
                    </div>
                    <div style="padding: 1.0px; float: left; width: 98%;">
                        <div style="float: left; width: 18%;">
                            <asp:Label ID="Label13" runat="server" Text="Line 1" Font-Size="Smaller"></asp:Label></div>
                        <div style="float: left; width: 69%;">
                            <asp:TextBox ID="txtAddresline1" runat="server" Width="100%" CssClass="TextBoxUpper"
                                MaxLength="100"></asp:TextBox></div>
                    </div>
                    <div style="padding: 1.0px; float: left; width: 98%;">
                        <div style="float: left; width: 18%;">
                            <asp:Label ID="Label10" runat="server" Text="Line 2" Font-Size="Smaller"></asp:Label></div>
                        <div style="float: left; width: 69%;">
                            <asp:TextBox ID="txtAddresline2" runat="server" Width="100%" CssClass="TextBoxUpper"
                                MaxLength="100"></asp:TextBox></div>
                    </div>
                    <div style="padding: 1.0px; float: left; width: 98%;">
                        <div style="float: left; width: 18%;">
                            <asp:Label ID="Label11" runat="server" Text="Line 3" Font-Size="Smaller"></asp:Label></div>
                        <div style="float: left; width: 69%;">
                            <asp:TextBox ID="txtAddresline3" runat="server" Width="100%" CssClass="TextBoxUpper"
                                MaxLength="100"></asp:TextBox></div>
                    </div>
                    <div style="padding: 1.0px; float: left; width: 95%;">
                    </div>
                </asp:Panel>
            </div>
            <div style="float: left; width: 30%;" align="center">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAddresline1"
                    ErrorMessage=" Please enter address details." ForeColor="Red" ValidationGroup="AddressValid"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div style="border-width: thin; border-style: groove; float: left; width: 100%; background-color: #FFFFFF;"
            id="divCustDetChange" runat="server">
            <div style="float: left; width: 99%;" align="right" id="divCustDetClose" runat="server">
                <asp:ImageButton ID="ImgBtnClose" runat="server" ImageUrl="~/Images/error_icon.png"
                    OnClick="ImgBtnClose_Click" />
            </div>
            <div style="float: left; width: 99%;" align="right" id="divCustUcButtons" runat="server">
                <asp:Button ID="btn_CREATE" runat="server" Text="Create" CssClass="Button" OnClick="btn_CREATE_Click" />
                <asp:Button ID="btn_UPDATE" runat="server" Text="Update" CssClass="Button" OnClick="btn_UPDATE_Click" />
                <asp:Button ID="btn_CLEAR" runat="server" Text="Clear" CssClass="Button" OnClick="btn_CLEAR_Click" />
            </div>
            <asp:Panel ID="Panel_custDet" runat="server" BackColor="White" Width="100%">
                <div style="float: left; width: 50%;">
                    <uc1:uc_CustomerCreation ID="uc_CustomerCreation1" runat="server" />
                </div>
                <div style="float: left; width: 49%;">
                    <uc2:uc_CustCreationExternalDet ID="uc_CustCreationExternalDet1" runat="server" />
                </div>
            </asp:Panel>
        </div>
        <div style="float: left; width: 100%;">
        </div>
    </div>
    <div style="float: left; width: 100%;">
        <asp:Panel ID="Panel_NewFlagBank" runat="server" BackColor="White">
            <div style="float: left; width: 100%;">
                <div style="float: left; width: 60%;" align="left">
                    <div style="float: left; width: 90%;" align="left">
                        <asp:Label ID="lblFlagBankSave" runat="server" Font-Bold="True"></asp:Label>
                    </div>
                    <div style="padding: 3.0px; float: left; width: 10%;" align="left">
                    </div>
                </div>
                <div style="float: right; width: 39%;" align="right">
                    <asp:Button ID="btnFB_Create" runat="server" Text="Create" CssClass="Button" OnClick="btnFB_Create_Click"
                        ValidationGroup="FBGroup" />
                    <asp:Button ID="btnFB_Close" runat="server" Text="Close" CssClass="Button" />
                </div>
                <div style="padding: 2.0px; float: left; width: 95%;">
                    <div style="float: left; width: 20%;">
                        <asp:Label ID="Label14" runat="server" Text="Code :" Font-Bold="True" ForeColor="Blue"></asp:Label>
                    </div>
                    <div style="float: left; width: 50%;">
                        <asp:TextBox ID="txt_fbCode" runat="server" CssClass="TextBoxUpper" MaxLength="10"
                            Width="25%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldFBCode" runat="server" ErrorMessage="*"
                            ControlToValidate="txt_fbCode" Font-Bold="True" ForeColor="Red" ValidationGroup="FBGroup"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div style="padding: 2.0px; float: left; width: 95%;">
                    <div style="float: left; width: 20%;">
                        <asp:Label ID="Label12" runat="server" Text="Type :" Font-Bold="True" ForeColor="Blue"></asp:Label>
                    </div>
                    <div style="float: left; width: 50%;">
                        <asp:DropDownList ID="ddlFB_type" runat="server" CssClass="ComboBox" Width="25%">
                            <asp:ListItem>BANK</asp:ListItem>
                            <asp:ListItem>FLAG</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div style="padding: 2.0px; float: left; width: 95%;">
                    <div style="float: left; width: 20%;">
                        <asp:Label ID="Label15" runat="server" Text="Description :" Font-Bold="True" ForeColor="Blue"></asp:Label>
                    </div>
                    <div style="float: left; width: 50%;">
                        <asp:TextBox ID="txt_fbDescript" runat="server" CssClass="TextBoxUpper" MaxLength="50"
                            Width="90%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldFBdes" runat="server" ErrorMessage="*"
                            ControlToValidate="txt_fbDescript" Font-Bold="True" ForeColor="Red" ValidationGroup="FBGroup"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <div style="float: left; width: 100%; text-align: right;">
            <asp:ModalPopupExtender ID="ModalPopupExtender_FB" runat="server" ClientIDMode="Static"
                BackgroundCssClass="modalBackground" PopupControlID="Panel_NewFlagBank" BehaviorID="btnFB_Close"
                TargetControlID="btnHidden_popupSearch">
            </asp:ModalPopupExtender>
            <%--<asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server">
        </asp:ModalPopupExtender>--%>
        </div>
    </div>
    <div style="float: left; width: 100%;" align="left">
        <asp:Panel ID="Panel_AccSumm" runat="server" BackColor="White" Width="50%">
            <div style="padding: 1.0px; float: left; width: 99%;" align="right">
                <asp:Button ID="btnCloseSearch" runat="server" Text="Close" CssClass="Button" />
            </div>
            <div style="float: left; width: 100%;" align="left">
                <uc3:uc_HpAccountSummary ID="uc_HpAccountSummary1" runat="server" />
            </div>
        </asp:Panel>
        <div style="display: none" align="right">
            <asp:Button ID="btnHidden_popupSearch" runat="server" Text="" />
        </div>
        <div style="float: left; width: 100%; text-align: right;">
            <asp:ModalPopupExtender ID="ModalPopupExtSearch" runat="server" ClientIDMode="Static"
                BackgroundCssClass="modalBackground" PopupControlID="Panel_AccSumm" BehaviorID="btnCloseSearch"
                TargetControlID="btnHidden_popupSearch">
            </asp:ModalPopupExtender>
            <%--<asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server">
        </asp:ModalPopupExtender>--%>
        </div>
    </div>
</asp:Content>
