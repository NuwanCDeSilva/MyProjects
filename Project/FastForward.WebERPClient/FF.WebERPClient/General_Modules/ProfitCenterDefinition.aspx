<%@ Page Title="Profit Center Definition" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="ProfitCenterDefinition.aspx.cs" Inherits="FF.WebERPClient.General_Modules.ProfitCenterDefinition" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControls/uc_ProfitCenterSearch.ascx" TagName="uc_ProfitCenterSearch"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
        function onblurFire(e, buttonid) {
            var evt = e ? e : window.event;
            var bt = document.getElementById(buttonid);
            if (bt) {

                bt.click();
                return false;
            }
        }

    </script>
    <div style="display: none">
    </div>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <asp:UpdatePanel ID="updtMainPnl" runat="server">
        <ContentTemplate>
            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%"
                Height="600px">
                <%--Create PC--%>
                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Create Profit Center">
                    <ContentTemplate>
                        <div style="float: left; width: 100%; text-align: right; font-size: smaller;">
                            <div style="float: left; width: 100%; text-align: right;">
                                <asp:Button ID="btnSAVE_cre" runat="server" Text="Save" CssClass="Button" OnClick="btnSAVE_cre_Click"
                                    ValidationGroup="save" />
                                <asp:Button ID="btnUPDATE_cre" runat="server" Text="Update" CssClass="Button" OnClick="btnUPDATE_cre_Click" />
                                <asp:Button ID="btnCLEAR_cre" runat="server" Text="Clear" CssClass="Button" OnClick="btnCLEAR_cre_Click" />
                                <asp:Button ID="btnCLOSE_cre" runat="server" Text="Close" CssClass="Button" OnClick="btnCLOSE_cre_Click" />
                            </div>
                            <asp:Panel ID="Panel16" runat="server" CssClass="PanelHeader">
                            </asp:Panel>
                            <div style="float: left; width: 100%; background-color: #E1F5FF;">
                                &nbsp;</div>
                            <div style="float: left; width: 100%; background-color: #E1F5FF;">
                                <asp:Panel ID="Panel1" runat="server">
                                    <div style="float: left; width: 100%;">
                                        <div style="float: left; width: 20%; text-align: center;">
                                            COMPANY</div>
                                        <div style="float: left; width: 20%; text-align: center;">
                                            PROFIT CENTER CODE</div>
                                        <div style="float: left; width: 30%; text-align: center;">
                                            DESCRIPTION</div>
                                        <div style="float: left; width: 20%; text-align: center;">
                                            TYPE</div>
                                    </div>
                                    <div style="float: left; width: 100%;">
                                        <div style="float: left; width: 20%; text-align: center;">
                                            <asp:TextBox ID="txtPc_Com" runat="server" CssClass="TextBoxUpper" MaxLength="5"
                                                Width="50%"></asp:TextBox>
                                            &nbsp;<asp:ImageButton ID="ImgBtnSearchCom" runat="server" ImageUrl="~/Images/icon_search.png"
                                                OnClick="ImgBtnSearchCom_Click" />
                                        </div>
                                        <div style="float: left; width: 20%; text-align: center;">
                                            <asp:TextBox ID="txtPcCode" runat="server" CssClass="TextBoxUpper" MaxLength="10"
                                                Width="50%"></asp:TextBox>
                                            <asp:Button ID="btnHidnGetPcDet" runat="server" Text=". . ." OnClick="btnHidnGetPcDet_Click"
                                                CssClass="Button" Font-Bold="True" />
                                        </div>
                                        <div style="float: left; width: 30%; text-align: center;">
                                            <asp:TextBox ID="txtPcDescript" runat="server" CssClass="TextBoxUpper" Width="100%"
                                                MaxLength="100"></asp:TextBox>
                                        </div>
                                        <div style="float: left; width: 20%; text-align: center;">
                                            <asp:DropDownList ID="ddlPcType" runat="server" CssClass="ComboBox" Width="50%">
                                                <asp:ListItem Value="P">Profit</asp:ListItem>
                                                <asp:ListItem Value="C">Cost</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div style="float: left; width: 10%; text-align: left;">
                                            <asp:CheckBox ID="checkActivePc" runat="server" Checked="True" Text="ACTIVE" />
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                            <div style="float: left; width: 100%;">
                                <div style="float: left; width: 20%; text-align: center;">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter company code"
                                        ControlToValidate="txtPc_Com" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                </div>
                                <div style="float: left; width: 20%; text-align: center;">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter new profit center code "
                                        ControlToValidate="txtPcCode" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                </div>
                                <div style="float: left; width: 30%; text-align: center;">
                                </div>
                                <div style="float: left; width: 20%; text-align: center;">
                                </div>
                            </div>
                            <div style="float: left; width: 100%; background-color: #C6E2FF;">
                                &nbsp;</div>
                            <div style="float: left; width: 100%; background-color: #E1F5FF;">
                                <asp:Panel ID="Panel2" runat="server">
                                    <div style="float: left; width: 100%;">
                                        <div style="float: left; width: 40%; text-align: center;">
                                            ADDRESS
                                        </div>
                                        <div style="float: left; width: 30%; text-align: center;">
                                            TELEPHONE
                                        </div>
                                        <div style="float: left; width: 20%; text-align: center;">
                                            FAX NO.
                                        </div>
                                        <div style="float: left; width: 10%; text-align: center;">
                                            MANAGER
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%;">
                                        <div style="float: left; width: 40%; text-align: center;">
                                            <asp:TextBox ID="txtAddress_1" runat="server" CssClass="TextBoxUpper" Width="90%"
                                                MaxLength="100"></asp:TextBox>
                                            <asp:TextBox ID="txtAddress_2" runat="server" CssClass="TextBoxUpper" Width="90%"
                                                MaxLength="100"></asp:TextBox>
                                        </div>
                                        <div style="float: left; width: 30%; text-align: center;">
                                            <asp:TextBox ID="txtPcTele" runat="server" CssClass="TextBox" MaxLength="32"></asp:TextBox>
                                        </div>
                                        <div style="float: left; width: 20%; text-align: center;">
                                            <asp:TextBox ID="txtPcFax" runat="server" CssClass="TextBox" MaxLength="20"></asp:TextBox>
                                        </div>
                                        <div style="float: left; width: 10%; text-align: center;">
                                            <asp:TextBox ID="txtManagerCd" runat="server" CssClass="TextBox" Width="90%" MaxLength="10"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%;">
                                        <div style="float: left; width: 40%; text-align: center;">
                                            OTHER REFERENCES
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%;">
                                        <div style="float: left; width: 40%; text-align: center;">
                                            <asp:TextBox ID="txtOherRef" runat="server" CssClass="TextBoxUpper" Width="90%" MaxLength="20"></asp:TextBox>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                            <div style="float: left; width: 100%; background-color: #C6E2FF;">
                                &nbsp;</div>
                            <div style="float: left; width: 100%; background-color: #FFFFFF;">
                                <div style="float: left; width: 100%;">
                                    &nbsp;</div>
                                <asp:Panel ID="Panel3" runat="server">
                                    <div style="float: left; width: 100%;">
                                        <div style="float: left; width: 15%; text-align: center;">
                                            CHANEL</div>
                                        <div style="float: left; width: 15%; text-align: center;">
                                            OPE. CODE</div>
                                        <div style="float: left; width: 15%; text-align: center;">
                                            ORDER VALID PERIOD</div>
                                        <div style="float: left; width: 20%; text-align: center;">
                                            EXTENDED WARRANTY PERIOD</div>
                                        <div style="float: left; width: 15%; text-align: center;">
                                            ADD HOURS</div>
                                    </div>
                                    <div style="float: left; width: 100%;">
                                        <div style="float: left; width: 100%; font-size: smaller;">
                                            <div style="float: left; width: 15%; text-align: center;">
                                                <asp:TextBox ID="txtPcChanel" runat="server" CssClass="TextBoxUpper" MaxLength="50"></asp:TextBox>
                                            </div>
                                            <div style="float: left; width: 15%; text-align: center;">
                                                <asp:TextBox ID="txtOPEcd" runat="server" CssClass="TextBoxUpper" MaxLength="5"></asp:TextBox>
                                            </div>
                                            <div style="float: left; width: 15%; text-align: center;">
                                                <asp:TextBox ID="txtOrderValidPeriod" runat="server" CssClass="TextBoxNumeric" MaxLength="2"></asp:TextBox>
                                            </div>
                                            <div style="float: left; width: 20%; text-align: center;">
                                                <asp:TextBox ID="txtExtWarrPeriod" runat="server" CssClass="TextBoxNumeric" MaxLength="2"></asp:TextBox>
                                            </div>
                                            <div style="float: left; width: 15%; text-align: center;">
                                                <asp:TextBox ID="txtAddHours" runat="server" CssClass="TextBoxNumeric" MaxLength="2"></asp:TextBox>
                                            </div>
                                            <div style="float: left; width: 15%; text-align: left; font-size: x-small;">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter no. of hours"
                                                    ControlToValidate="txtAddHours" ForeColor="Red" SetFocusOnError="True" ValidationGroup="save"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div style="float: left; width: 100%;">
                                    &nbsp;</div>
                            </div>
                            <div style="float: left; width: 100%; background-color: #C6E2FF;">
                                &nbsp;</div>
                            <div style="float: left; width: 100%; background-color: #FFFFFF;">
                                <div style="float: left; width: 100%;">
                                    &nbsp;</div>
                                <asp:Panel ID="Panel4" runat="server">
                                    <div style="float: left; width: 100%;">
                                        <div style="float: left; width: 15%; text-align: center;">
                                            DEFAULT LOC.</div>
                                        <div style="float: left; width: 15%; text-align: center;">
                                            DEFAULT CUSTOMER</div>
                                        <div style="float: left; width: 15%; text-align: center;">
                                            DEFAULT PRICE BOOK</div>
                                        <div style="float: left; width: 20%; text-align: center;">
                                            DEFAUL DISCOUNT RT.</div>
                                        <div style="float: left; width: 15%; text-align: center;">
                                            DEFAULT EXCHANGE RT.</div>
                                    </div>
                                    <div style="float: left; width: 100%;">
                                        <div style="float: left; width: 15%; text-align: center;">
                                            <asp:TextBox ID="txtDefLoc" runat="server" CssClass="TextBoxUpper" MaxLength="10"></asp:TextBox>
                                        </div>
                                        <div style="float: left; width: 15%; text-align: center;">
                                            <asp:TextBox ID="txtDefCustomer" runat="server" CssClass="TextBoxUpper" MaxLength="10"></asp:TextBox>
                                        </div>
                                        <div style="float: left; width: 15%; text-align: center;">
                                            <asp:TextBox ID="txtDefPB" runat="server" CssClass="TextBoxUpper" MaxLength="10"></asp:TextBox>
                                        </div>
                                        <div style="float: left; width: 20%; text-align: center;">
                                            <asp:TextBox ID="txtDefDiscountRt" runat="server" CssClass="TextBoxUpper" MaxLength="20"></asp:TextBox>
                                        </div>
                                        <div style="float: left; width: 15%; text-align: center;">
                                            <asp:TextBox ID="txtDefExRt" runat="server" CssClass="TextBox" MaxLength="10"></asp:TextBox>
                                        </div>
                                        <div style="float: left; width: 15%; text-align: center;">
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%;">
                                        <div style="padding: 3.0px; float: left; width: 20%; text-align: left;">
                                            <asp:CheckBox ID="chekIsMultiDept" runat="server" Text="Multi department maintain"
                                                Font-Bold="True" />
                                        </div>
                                        <div style="float: left; width: 15%; text-align: center;">
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%;">
                                        <div style="padding: 1.0px; float: left; width: 20%; text-align: center;">
                                            DEFAULT DEPARTMENT ::</div>
                                        <div style="float: left; width: 15%; text-align: left;">
                                            <asp:TextBox ID="txtDefDepartment" runat="server" CssClass="TextBoxUpper" Width="70%"
                                                MaxLength="5"></asp:TextBox>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div style="float: left; width: 100%;">
                                    &nbsp;</div>
                            </div>
                            <div style="float: left; width: 100%; background-color: #FFFFFF;">
                                <asp:Panel ID="Panel5" runat="server">
                                    <div style="float: left; width: 100%; text-align: left; background-color: #3366FF;
                                        color: #FFFFFF;">
                                        <asp:Label ID="Label1" runat="server" Text="GRANT PERMISSION" Font-Bold="True"></asp:Label>
                                    </div>
                                    <div style="float: left; width: 100%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 100%;">
                                        <div style="float: left; width: 15%; text-align: left;">
                                        </div>
                                        <div style="float: left; width: 15%; text-align: left;">
                                            <asp:CheckBox ID="checkEditPrice" runat="server" Text="Edit Price" Font-Bold="True" />
                                        </div>
                                        <div style="float: left; width: 15%; text-align: left;">
                                            Allowd Rt. for edit:
                                            <asp:TextBox ID="txtAllowRtForEdit" runat="server" CssClass="TextBoxNumeric" Width="70%"
                                                MaxLength="20"></asp:TextBox>
                                        </div>
                                        <div style="float: left; width: 20%; text-align: left;">
                                            <asp:CheckBox ID="checkEnterPriceMan" runat="server" Text="Enter price manually"
                                                Font-Bold="True" />
                                        </div>
                                        <div style="float: left; width: 20%; text-align: left;">
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%;">
                                        <div style="float: left; width: 15%; text-align: left;">
                                        </div>
                                        <div style="float: left; width: 15%; text-align: left;">
                                            <asp:CheckBox ID="check_pay" runat="server" Text="Check Payments" Font-Bold="True" />
                                        </div>
                                        <div style="float: left; width: 15%; text-align: left;">
                                            <asp:CheckBox ID="check_credit" runat="server" Text="Check credit" Font-Bold="True" />
                                        </div>
                                        <div style="float: left; width: 20%; text-align: left;">
                                            <asp:CheckBox ID="check_manCashMemo" runat="server" Text="Check manual cash memo."
                                                Font-Bold="True" />
                                        </div>
                                        <div style="float: left; width: 20%; text-align: left;">
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%;">
                                        <div style="float: left; width: 15%; text-align: left;">
                                        </div>
                                        <div style="float: left; width: 15%; text-align: left;">
                                            <asp:CheckBox ID="checkPrintDisc" runat="server" Text="Print Discount" Font-Bold="True" />
                                        </div>
                                        <div style="float: left; width: 15%; text-align: left;">
                                            <asp:CheckBox ID="checkPrintPaymnt" runat="server" Text="Print Payments" Font-Bold="True" />
                                        </div>
                                        <div style="float: left; width: 20%; text-align: left;">
                                            <asp:CheckBox ID="checkPrintWarrRemk" runat="server" Text="Print Warranty Remarks"
                                                Font-Bold="True" />
                                        </div>
                                        <div style="float: left; width: 20%; text-align: left;">
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%;">
                                        <div style="float: left; width: 15%; text-align: left;">
                                        </div>
                                        <div style="float: left; width: 15%; text-align: left;">
                                            <asp:CheckBox ID="checkInterCom" runat="server" Text="Inter Company" Font-Bold="True" />
                                        </div>
                                        <div style="float: left; width: 15%; text-align: left;">
                                            <asp:CheckBox ID="checkSOsms" runat="server" Text="SO SMS" Font-Bold="True" />
                                        </div>
                                        <div style="float: left; width: 20%; text-align: left;">
                                            <asp:CheckBox ID="checkOrderRestr" runat="server" Text="Order Restriction" Font-Bold="True" />
                                        </div>
                                        <div style="float: left; width: 20%; text-align: left;">
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                            <div style="float: left; width: 100%;">
                                &nbsp;</div>
                            <div style="float: left; width: 100%; background-color: #C6E2FF;">
                                &nbsp;</div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <%--Create PC--%>
                <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Hierarchy Definition">
                    <ContentTemplate>
                        <div style="float: left; width: 100%; text-align: right;">
                            <asp:Button ID="btnSaveHiera" runat="server" Text="Save" CssClass="Button" OnClick="btnSaveHiera_Click" />
                            <asp:Button ID="btnClearHiera" runat="server" Text="Clear" CssClass="Button" OnClick="btnCLEAR_cre_Click" />
                            <asp:Button ID="btnCloseHiera" runat="server" Text="Close" CssClass="Button" OnClick="btnCLOSE_cre_Click" />
                        </div>
                        <div style="float: left; width: 100%; text-align: right; font-size: smaller;">
                            <asp:Panel ID="Panel6" runat="server" CssClass="PanelHeader" HorizontalAlign="Left"
                                Font-Bold="True">
                                HIERARCHY DEFINITION
                            </asp:Panel>
                            <div style="float: left; width: 100%; background-color: #E1F5FF;">
                                &nbsp;</div>
                            <div style="float: left; width: 30%;">
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 40%;">
                                        COMPANY :
                                    </div>
                                    <div style="float: left; width: 35%; text-align: left;">
                                        <asp:DropDownList ID="ddlComPcHiera" runat="server" CssClass="ComboBox" Width="95%">
                                            <asp:ListItem>ABL</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 40%;">
                                        PROFIT CENTER :
                                    </div>
                                    <div style="float: left; width: 35%; text-align: left;">
                                        <asp:TextBox ID="txtPcHiera" runat="server" Width="90%" CssClass="TextBox"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 10%; text-align: left;">
                                        <asp:ImageButton ID="imgBtnHieraPc" runat="server" 
                                            ImageUrl="~/Images/icon_search.png" onclick="imgBtnHieraPc_Click" />
                                    </div>
                                </div>
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 40%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 40%; text-align: left;">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Enter Profit Center"
                                            ControlToValidate="txtPcHiera" ForeColor="Red" ValidationGroup="hireaPc"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 40%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 35%; text-align: left;">
                                        <asp:Button ID="btnAddPcHiera" runat="server" CssClass="Button" OnClick="btnAddPcHiera_Click"
                                            Text="Add &gt;&gt;" Width="90%" ValidationGroup="hireaPc" />
                                    </div>
                                    <div style="float: left; width: 5%; text-align: left;">
                                    </div>
                                </div>
                            </div>
                            <div style="float: left; width: 15%;">
                                <asp:Panel ID="Panel8" runat="server" Height="110px" ScrollBars="Vertical" BorderColor="Blue"
                                    BorderWidth="1px" GroupingText="Profit Centers">
                                    <asp:GridView ID="grvProfCents" runat="server" OnRowDataBound="grvProfCents_RowDataBound"
                                        CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDeleting="grvProfCents_RowDeleting"
                                        ShowHeader="False" Width="95%" AutoGenerateColumns="False">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                                    <asp:ImageButton ID="ImageButton8" runat="server" CommandName="Delete" ImageUrl="~/Images/Delete.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Mpc_cd" />
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
                            <div style="float: left; width: 50%;">
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 30%;">
                                        ZONE
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 25%;">
                                        <asp:TextBox ID="txtHieraZONE" runat="server" CssClass="TextBox"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 10%;">
                                        <asp:ImageButton ID="ImgBtnHieraZONE" runat="server" ImageUrl="~/Images/icon_search.png" />
                                    </div>
                                </div>
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 30%;">
                                        REGION
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 25%;">
                                        <asp:TextBox ID="txtHieraREGION" runat="server" CssClass="TextBox"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 10%;">
                                        <asp:ImageButton ID="ImgBtnHieraREGION" runat="server" ImageUrl="~/Images/icon_search.png" />
                                    </div>
                                </div>
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 30%;">
                                        AREA
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 25%;">
                                        <asp:TextBox ID="txtHieraAREA" runat="server" CssClass="TextBox"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 10%;">
                                        <asp:ImageButton ID="ImgBtnHieraAREA" runat="server" ImageUrl="~/Images/icon_search.png" />
                                    </div>
                                </div>
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 30%;">
                                        CHANEL
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 25%;">
                                        <asp:TextBox ID="txtHieraCHANEL" runat="server" CssClass="TextBox"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 10%;">
                                        <asp:ImageButton ID="ImgBtnHieraCHNL" runat="server" ImageUrl="~/Images/icon_search.png" />
                                    </div>
                                </div>
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 30%;">
                                        SUB CHANEL
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 25%;">
                                        <asp:TextBox ID="txtHieraSUBCHANEL" runat="server" CssClass="TextBox"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 10%;">
                                        <asp:ImageButton ID="ImgBtnHieraSCHNL" runat="server" ImageUrl="~/Images/icon_search.png" />
                                    </div>
                                </div>
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 30%;">
                                        COMPANY GROUP
                                    </div>
                                    <div style="float: left; width: 2%;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 25%;">
                                        <asp:TextBox ID="txtHieraCPG" runat="server" CssClass="TextBox"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 10%;">
                                        <asp:ImageButton ID="ImgBtnHieraCGP" runat="server" ImageUrl="~/Images/icon_search.png" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div style="float: left; width: 100%;">
                            &nbsp;</div>
                        <div style="float: left; width: 100%; text-align: right; font-size: smaller;">
                            <asp:Panel ID="Panel7" runat="server" CssClass="PanelHeader" HorizontalAlign="Left"
                                Font-Bold="True">
                                DISPLAY INFORMATION
                            </asp:Panel>
                            <div style="float: left; width: 100%; background-color: #E1F5FF;">
                                &nbsp;</div>
                            <div style="float: left; width: 100%; text-align: right;">
                                <div style="float: left; width: 30%;">
                                    <div style="float: left; width: 40%;">
                                        COMPANY :
                                    </div>
                                    <div style="float: left; width: 35%; text-align: left;">
                                        <asp:DropDownList ID="ddlVeiwHieraCom" runat="server" CssClass="ComboBox" Width="95%">
                                            <asp:ListItem>ABL</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div style="float: left; width: 30%;">
                                    <div style="float: left; width: 40%;">
                                        PROFIT CENTER :
                                    </div>
                                    <div style="float: left; width: 35%; text-align: left;">
                                        <asp:TextBox ID="txtViewHieraPc" runat="server" Width="90%" CssClass="TextBox"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 10%; text-align: left;">
                                        <asp:ImageButton ID="btnPCHieraDisp" runat="server" 
                                            ImageUrl="~/Images/icon_search.png" onclick="btnPCHieraDisp_Click" />
                                    </div>
                                </div>
                                <div style="float: left; width: 20%; text-align: left;">
                                    <asp:Button ID="btnViewHiera" runat="server" Text="Veiw Hierarchy" CssClass="Button"
                                        OnClick="btnViewHiera_Click" />
                                </div>
                            </div>
                            <div style="float: left; width: 5%; text-align: left;">
                            </div>
                            <div style="float: left; width: 49%; text-align: left;">
                                <asp:GridView ID="grvViewPcHiera" runat="server" CellPadding="4" ForeColor="#333333"
                                    Width="80%" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                                    <AlternatingRowStyle BackColor="White" />
                                    <EmptyDataTemplate>
                                        <div style="width: 100%; text-align: center;">
                                            No data found
                                        </div>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:BoundField HeaderText="CODE" DataField="Mpi_cd">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="VALUE" DataField="Mpi_val">
                                            <ItemStyle HorizontalAlign="Center" />
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
                            </div>
                            <div style="padding: 2.0px; float: left; width: 100%;">
                                &nbsp;</div>
                        </div>
                        <div style="float: left; width: 100%; text-align: right; font-size: smaller;">
                            <asp:Panel ID="Panel9" runat="server" CssClass="PanelHeader" HorizontalAlign="Left"
                                Font-Bold="True">
                                UPDATE INFORMATION
                            </asp:Panel>
                            <div style="float: left; width: 100%; background-color: #E1F5FF;">
                                &nbsp;</div>
                            <div style="float: left; width: 100%; text-align: right;">
                                <div style="float: left; width: 30%;">
                                    <div style="float: left; width: 40%;">
                                        COMPANY :
                                    </div>
                                    <div style="float: left; width: 35%; text-align: left;">
                                        <asp:DropDownList ID="ddlUpdtPcHiera" runat="server" CssClass="ComboBox" Width="95%">
                                            <asp:ListItem>ABL</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div style="float: left; width: 30%;">
                                    <div style="float: left; width: 40%;">
                                        PROFIT CENTER :
                                    </div>
                                    <div style="float: left; width: 35%; text-align: left;">
                                        <asp:TextBox ID="txtUpdtPcHiera" runat="server" Width="90%" CssClass="TextBox"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 10%; text-align: left;">
                                        <asp:ImageButton ID="btnImgHieraUpdatePc" runat="server" 
                                            ImageUrl="~/Images/icon_search.png" Height="16px" 
                                            onclick="btnImgHieraUpdatePc_Click" />
                                    </div>
                                    <div style="float: left; width: 10%; text-align: left;">
                                        <asp:ImageButton ID="ImgBtnLoadHiera" runat="server" ImageUrl="~/Images/download_arrow_icon.png"
                                            Height="28px" OnClick="ImgBtnLoadHiera_Click" />
                                    </div>
                                </div>
                                <div style="float: left; width: 20%; text-align: left;">
                                    <asp:Button ID="btnUpdateHiera" runat="server" CssClass="Button" Text="Update Hierarchy"
                                        OnClick="btnUpdateHiera_Click" />
                                </div>
                            </div>
                            <div style="float: left; width: 5%; text-align: left;">
                            </div>
                            <div style="float: left; width: 90%; text-align: left;">
                                <div style="float: left; width: 60%; text-align: right;">
                                    <div style="float: left; width: 100%;">
                                        <div style="float: left; width: 30%;">
                                            ZONE
                                        </div>
                                        <div style="float: left; width: 2%;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 25%;">
                                            <asp:TextBox ID="txtUpdteZONE" runat="server" CssClass="TextBox"></asp:TextBox>
                                        </div>
                                        <div style="float: left; width: 10%;">
                                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/icon_search.png" />
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%;">
                                        <div style="float: left; width: 30%;">
                                            REGION
                                        </div>
                                        <div style="float: left; width: 2%;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 25%;">
                                            <asp:TextBox ID="txtUpdteREGION" runat="server" CssClass="TextBox"></asp:TextBox>
                                        </div>
                                        <div style="float: left; width: 10%;">
                                            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/icon_search.png" />
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%;">
                                        <div style="float: left; width: 30%;">
                                            AREA
                                        </div>
                                        <div style="float: left; width: 2%;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 25%;">
                                            <asp:TextBox ID="txtUpdteAREA" runat="server" CssClass="TextBox"></asp:TextBox>
                                        </div>
                                        <div style="float: left; width: 10%;">
                                            <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Images/icon_search.png" />
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%;">
                                        <div style="float: left; width: 30%;">
                                            CHANEL
                                        </div>
                                        <div style="float: left; width: 2%;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 25%;">
                                            <asp:TextBox ID="txtUpdteCHNL" runat="server" CssClass="TextBox"></asp:TextBox>
                                        </div>
                                        <div style="float: left; width: 10%;">
                                            <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Images/icon_search.png" />
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%;">
                                        <div style="float: left; width: 30%;">
                                            SUB CHANEL
                                        </div>
                                        <div style="float: left; width: 2%;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 25%;">
                                            <asp:TextBox ID="txtUpdteSUBCHNL" runat="server" CssClass="TextBox"></asp:TextBox>
                                        </div>
                                        <div style="float: left; width: 10%;">
                                            <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/Images/icon_search.png" />
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%;">
                                        <div style="float: left; width: 30%;">
                                            COMPANY GROUP
                                        </div>
                                        <div style="float: left; width: 2%;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 25%;">
                                            <asp:TextBox ID="txtUpdteCGP" runat="server" CssClass="TextBox"></asp:TextBox>
                                        </div>
                                        <div style="float: left; width: 10%;">
                                            <asp:ImageButton ID="ImageButton9" runat="server" ImageUrl="~/Images/icon_search.png" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div style="float: left; width: 100%;">
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="Receipt Category">
                    <ContentTemplate>
                        <div style="float: left; width: 100%; text-align: right;">
                            <asp:Button ID="btnRecSave" runat="server" Text="Save" CssClass="Button" OnClick="btnRecSave_Click" />
                            <asp:Button ID="btnRecClear" runat="server" Text="Clear" CssClass="Button" OnClick="btnCLEAR_cre_Click" />
                            <asp:Button ID="btnRecClose" runat="server" Text="Close" CssClass="Button" OnClick="btnCLOSE_cre_Click" />
                        </div>
                        <div style="float: left; width: 100%; text-align: right; font-size: smaller;">
                            <asp:Panel ID="Panel10" runat="server" CssClass="PanelHeader">
                            </asp:Panel>
                            <div style="float: left; width: 100%; background-color: #E1F5FF;">
                                &nbsp;</div>
                            <div style="float: left; width: 100%; text-align: right;">
                                <div style="float: left; width: 55%;">
                                    <uc1:uc_ProfitCenterSearch ID="uc_ProfitCenterSearch1" runat="server" />
                                </div>
                                <div style="float: left; width: 4%;">
                                    <asp:ImageButton ID="ImgBtnAddPcRcpt" runat="server" ImageUrl="~/Images/right_arrow_icon.png"
                                        OnClick="ImgBtnAddPcRcpt_Click" />
                                </div>
                                <div style="float: left; width: 40%;">
                                    <div style="float: left; width: 40%;">
                                        <div style="float: left; width: 100%;">
                                            <asp:Panel ID="Panel11" runat="server" ScrollBars="Vertical" Height="130px" BorderColor="Blue"
                                                BorderWidth="1px" GroupingText="Profit Centers">
                                                <asp:GridView ID="grvPC_RcptCat" runat="server" AutoGenerateColumns="False" Width="100%"
                                                    OnRowDataBound="grvPC_RcptCat_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server"></asp:Label>
                                                                <asp:CheckBox ID="chekPc_para" runat="server" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="PROFIT_CENTER" ShowHeader="False" />
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </div>
                                        <div style="float: left; width: 100%;">
                                            <asp:Button ID="btnAll_pty" runat="server" Text="All" CssClass="Button" OnClick="btnAll_pty_Click" />
                                            <asp:Button ID="btnNone_pty" runat="server" Text="None" CssClass="Button" OnClick="btnNone_pty_Click" />
                                            <asp:Button ID="btnClear_pty" runat="server" Text="Clear" CssClass="Button" OnClick="btnClear_pty_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div style="float: left; width: 100%; background-color: #E1F5FF;">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 100%;">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 100%; text-align: right;">
                                <div style="float: left; width: 10%;">
                                    CODE</div>
                                <div style="float: left; width: 10%;">
                                    <asp:TextBox ID="txtRecCode" runat="server" CssClass="TextBoxUpper" MaxLength="5"
                                        Width="90%"></asp:TextBox>
                                </div>
                                <div style="float: left; width: 10%;">
                                    DESCRIPTION</div>
                                <div style="float: left; width: 20%;">
                                    <asp:TextBox ID="txtRecDesc" runat="server" CssClass="TextBoxUpper" MaxLength="100"
                                        Width="100%"></asp:TextBox>
                                </div>
                                <div style="float: left; width: 15%; text-align: center;">
                                    <asp:CheckBox ID="checkRecDefault" runat="server" Text="Is Default" />
                                </div>
                                <div style="float: left; width: 15%; text-align: left;">
                                    <asp:Button ID="btnAddRec" runat="server" Text="Add To List" CssClass="Button" OnClick="btnAddRec_Click"
                                        ValidationGroup="recDiv" />
                                </div>
                                <div style="float: left; width: 15%;">
                                    <asp:HiddenField ID="HiddenField_ComRecDiv" runat="server" />
                                </div>
                            </div>
                            <div style="float: left; width: 100%; text-align: right;">
                                <div style="float: left; width: 10%;">
                                </div>
                                <div style="float: left; width: 10%;">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Enter code"
                                        ControlToValidate="txtRecCode" ForeColor="Red" ValidationGroup="recDiv"></asp:RequiredFieldValidator>
                                </div>
                                <div style="float: left; width: 10%;">
                                </div>
                                <div style="float: left; width: 10%;">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Enter description"
                                        ControlToValidate="txtRecDesc" ForeColor="Red" ValidationGroup="recDiv"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div style="float: left; width: 100%;">
                                <div style="float: left; width: 10%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 89%;">
                                    <asp:Panel ID="Panel12" runat="server" Height="198px" ScrollBars="Vertical" Width="432px">
                                        <asp:GridView ID="grvRec" runat="server" CellPadding="4" ForeColor="#333333" AutoGenerateColumns="False"
                                            Width="100%" ShowHeaderWhenEmpty="True">
                                            <AlternatingRowStyle BackColor="White" />
                                            <EmptyDataTemplate>
                                                <div style="width: 100%; text-align: center;">
                                                    No data found
                                                </div>
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                                        <asp:ImageButton ID="ImgBtnDelRec" runat="server" ImageUrl="~/Images/Delete.png" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Code" DataField="Msrd_cd" />
                                                <asp:BoundField HeaderText="Description" DataField="Msrd_desc" />
                                                <asp:BoundField HeaderText="Default" DataField="Msrd_is_def" />
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
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="Profit Center Charges">
                    <ContentTemplate>
                        <div style="float: left; width: 100%; text-align: right;">
                            <asp:Button ID="btnSavePcChg" runat="server" Text="Save" CssClass="Button" 
                                onclick="btnSavePcChg_Click" />
                            <asp:Button ID="btnClosePcChg" runat="server" Text="Clear" CssClass="Button" OnClick="btnCLEAR_cre_Click" />
                            <asp:Button ID="btnClearPcChg" runat="server" Text="Close" CssClass="Button" OnClick="btnCLOSE_cre_Click" />
                        </div>
                        <div style="float: left; width: 100%; text-align: right; font-size: smaller;">
                            <asp:Panel ID="Panel13" runat="server" CssClass="PanelHeader">
                            </asp:Panel>
                            <div style="float: left; width: 100%; background-color: #E1F5FF;">
                                &nbsp;</div>
                        </div>
                        <div style="float: left; width: 100%; text-align: right; font-size: smaller;">
                            <div style="float: left; width: 55%;">
                                <uc1:uc_ProfitCenterSearch ID="uc_ProfitCenterSearch2" runat="server" />
                            </div>
                            <div style="float: left; width: 4%;">
                                <asp:ImageButton ID="ImgBtnPcChgAdd" runat="server" ImageUrl="~/Images/right_arrow_icon.png"
                                    OnClick="ImgBtnPcChgAdd_Click" />
                            </div>
                            <div style="float: left; width: 40%;">
                              <div style="float: left; width: 40%;">
                                <asp:Panel ID="Panel14" runat="server" ScrollBars="Vertical" Height="130px" BorderColor="Blue"
                                    BorderWidth="1px" GroupingText="Profit Centers">
                                    <asp:GridView ID="grvPc_Chg" runat="server" AutoGenerateColumns="False" Width="100%"
                                        OnRowDataBound="grvPc_Chg_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                                    <asp:CheckBox ID="chekPcChg" runat="server" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="Text3" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="PROFIT_CENTER" ShowHeader="False" />
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>                              
                                </div>
                                <div style="float: left; width: 100%; text-align: left;">
                                    <asp:Button ID="btnAllChg" runat="server" Text="All" CssClass="Button" OnClick="btnAllChg_Click" />
                                    <asp:Button ID="btnNoneChg" runat="server" Text="None" CssClass="Button" OnClick="btnNoneChg_Click" />
                                    <asp:Button ID="btnClearChg" runat="server" Text="Clear" CssClass="Button" OnClick="btnClearChg_Click" />
                                </div>
                            </div>
                        </div>
                        <div style="float: left; width: 100%; background-color: #E1F5FF;">
                            &nbsp;</div>
                        <div style="float: left; width: 100%;">
                            &nbsp;<asp:HiddenField ID="HiddenField_pcChg" runat="server" />
                        </div>
                        <div style="float: left; width: 100%; text-align: right; font-size: smaller;">
                            <div style="float: left; width: 10%; text-align: center;">
                                FROM DATE</div>
                            <div style="float: left; width: 5%;">
                                &nbsp;</div>
                            <div style="float: left; width: 10%; text-align: center;">
                                TO DATE</div>
                            <div style="float: left; width: 5%;">
                                &nbsp;</div>
                            <div style="float: left; width: 10%;text-align: center;">
                                EST RT.</div>
                            <div style="float: left; width: 5%;">
                                &nbsp;</div>
                            <div style="float: left; width: 10%; text-align: center;">
                                EPF RT.</div>
                            <div style="float: left; width: 5%;">
                                &nbsp;</div>
                            <div style="float: left; width: 10%; text-align: center;">
                                WHD TAX RT.</div>
                        </div>
                        <div style="float: left; width: 100%; text-align: right; font-size: smaller;">
                            
                            <div style="float: left; width: 10%;">
                              
                                <asp:TextBox ID="txtChgFromDt" runat="server" CssClass="TextBox" Width="100%"></asp:TextBox>
                                <asp:CalendarExtender
                                     ID="CalendarExtendertxtFromDt" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                      TargetControlID="txtChgFromDt">
                                    </asp:CalendarExtender>
                            </div>
                             <div style="float: left; width: 5%;">
                                 &nbsp;</div>
                            <div style="float: left; width: 10%;">
                                <asp:TextBox ID="txtChgToDt" runat="server" CssClass="TextBox" Width="100%"></asp:TextBox>
                                <asp:CalendarExtender
                                   ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtChgToDt">
                                    </asp:CalendarExtender>
                            </div>
                             <div style="float: left; width: 5%;">
                                 &nbsp;</div>
                            <div style="float: left; width: 10%;">
                                <asp:TextBox ID="txtChgESD" runat="server" CssClass="TextBoxNumeric" 
                                    Width="100%"></asp:TextBox>
                            </div>
                             <div style="float: left; width: 5%;">
                                 &nbsp;</div>
                            <div style="float: left; width: 10%;">
                                <asp:TextBox ID="txtChgEPF" runat="server" CssClass="TextBoxNumeric" Width="100%"></asp:TextBox>
                            </div>
                             <div style="float: left; width: 5%;">
                                 &nbsp;</div>
                             <div style="float: left; width: 10%;">
                              <asp:TextBox ID="txtChgWHT" runat="server" CssClass="TextBoxNumeric" Width="100%"></asp:TextBox>
                            </div>
                        </div>
                         <div style="float: left; width: 100%; background-color: #E1F5FF;">
                            &nbsp;</div>
                             <asp:Panel ID="Panel20" runat="server" CssClass="PanelHeader" 
                            HorizontalAlign="Right">
                              <asp:Button ID="btnViewLatestChg" runat="server" Text="Veiw Latest Charges" 
                                CssClass="Button" onclick="btnViewLatestChg_Click" /> 
                            </asp:Panel>
                       
                        <div style="float: left; width: 100%; text-align: right; font-size: smaller;">
                            <asp:Panel ID="Panel19" runat="server" Height="204px" ScrollBars="Both">
                                <asp:GridView ID="grv_PCchgs" runat="server" CellPadding="4" ShowHeaderWhenEmpty="True"
                                    ForeColor="#333333" AutoGenerateColumns="False" Width="100%">
                                    <AlternatingRowStyle BackColor="White" />
                                     <EmptyDataTemplate>
                                        <div style="width: 100%; text-align: center;">
                                             No data found
                                           </div>
                                     </EmptyDataTemplate>
                                    <Columns>
                                        <asp:BoundField DataField="Mpch_com" HeaderText="Company" />
                                        <asp:BoundField DataField="Mpch_pc" HeaderText="Profit Center" />
                                        <asp:BoundField DataField="Mpch_from_dt" HeaderText="From Dt." 
                                            DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="Mpch_to_dt" HeaderText="To Dt." 
                                            DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="Mpch_esd" HeaderText="ESD Rt." />
                                        <asp:BoundField DataField="Mpch_epf" HeaderText="EPF Rt." />
                                        <asp:BoundField DataField="Mpch_wht" HeaderText="WHD Tax Rt." />
                                        <asp:BoundField DataField="Mpch_cre_by" HeaderText="Created by" />
                                        <asp:BoundField DataField="Mpch_cre_dt" HeaderText="Created Dt." 
                                            DataFormatString="{0:d}" />
                                    </Columns>
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" 
                                        HorizontalAlign="Right" />
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
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel5" runat="server" HeaderText="Transaction Pay Types">
                <ContentTemplate>
                <div style="float: left; width: 100%; text-align: right;">
                            <asp:Button ID="btnSaveTxnTp" runat="server" Text="Save" CssClass="Button" 
                                onclick="btnSaveTxnTp_Click"/>
                            
                            <asp:Button ID="Button2" runat="server" Text="Clear" CssClass="Button" OnClick="btnCLEAR_cre_Click" />
                            <asp:Button ID="Button3" runat="server" Text="Close" CssClass="Button" OnClick="btnCLOSE_cre_Click" />
                        </div>
                        <div style="float: left; width: 100%; text-align: right; font-size: smaller;">
                            <asp:Panel ID="Panel15" runat="server" CssClass="PanelHeader">
                            </asp:Panel>
                            <div style="float: left; width: 100%; background-color: #E1F5FF;">
                                &nbsp;</div>
                        </div>
                        <div style="float: left; width: 100%; text-align: right; font-size: smaller;">
                        <div style="float: left; width: 55%;">                        
                            <uc1:uc_ProfitCenterSearch ID="uc_ProfitCenterSearch3" runat="server" />                        
                        </div>
                         <div style="float: left; width: 4%;">    
                             <asp:ImageButton ID="ImgBtnAddPc_Txn" runat="server" 
                                 ImageUrl="~/Images/right_arrow_icon.png" onclick="ImgBtnAddPc_Txn_Click" />
                         </div>
                         <div style="float: left; width: 40%;"> 
                           <div style="float: left; width: 40%;">                         
                             <asp:Panel ID="Panel17" runat="server" ScrollBars="Vertical" Height="130px" BorderColor="Blue"
                                    BorderWidth="1px" GroupingText="Profit Centers">
                          <asp:GridView ID="grvPc_Txn" runat="server" AutoGenerateColumns="False" Width="100%">
                                     
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                                    <asp:CheckBox ID="chekPcTxn" runat="server" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="Text4" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="PROFIT_CENTER" ShowHeader="False" />
                                        </Columns>
                                    </asp:GridView>
                          </asp:Panel>
                           </div>   
                           <div style="float: left; width: 100%; text-align: left;">
                                    <asp:Button ID="btnAllTxn" runat="server" Text="All" CssClass="Button" 
                                        onclick="btnAllTxn_Click" />
                                    <asp:Button ID="btnNoneTxn" runat="server" Text="None" CssClass="Button" 
                                        onclick="btnNoneTxn_Click" />
                                    <asp:Button ID="btnClearTxn" runat="server" Text="Clear" CssClass="Button" 
                                        onclick="btnClearTxn_Click"  />
                            </div>
                         </div>


                        </div>
                        <div style="float: left; width: 100%; background-color: #E1F5FF;">
                                &nbsp;
                        </div>
                        <div style="float: left; width: 100%; text-align: right; font-size: smaller;">
                            <div style="float: left; width: 10%;">
                            
                                Transaction type</div>
                             <div style="float: left; width: 10%;">
                                     <asp:TextBox ID="txtTxnType" runat="server" CssClass="TextBox" Width="100%"></asp:TextBox>
                           
                            </div>
                            <div style="float: left; width: 5%; text-align: left;">
                             &nbsp; 
                                 <asp:ImageButton ID="ImgBtnTxnTp" runat="server" 
                                     ImageUrl="~/Images/icon_search.png" />                     
                               </div>
                             <div style="float: left; width: 5%;">
                            
                                 From:</div>
                             <div style="float: left; width: 10%;">
                            
                                 <asp:TextBox ID="txtTxnFrom" runat="server" CssClass="TextBox" Width="100%"></asp:TextBox>
                                  <asp:CalendarExtender
                                     ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtTxnFrom">
                                 </asp:CalendarExtender>
                            </div>
                             <div style="float: left; width: 10%;">
                            
                                 To:</div>
                             <div style="float: left; width: 10%;">
                             <asp:TextBox ID="txtTxnTo" runat="server" CssClass="TextBox" Width="100%"></asp:TextBox>
                             <asp:CalendarExtender
                                ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtTxnTo">
                             </asp:CalendarExtender>
                            </div>
                            
                             <div style="float: left; width: 10%;">
                            Pay Type
                            </div>
                             <div style="float: left; width: 10%;">
                                 <asp:DropDownList ID="ddlPayTypes" runat="server" CssClass="ComboBox">
                                 </asp:DropDownList>
                                
                            </div>
                             <div style="float: left; width: 5%; text-align: left;"> 
                             &nbsp; 
                                 <asp:ImageButton ID="ImgBtnPayTp" runat="server" 
                                     ImageUrl="~/Images/icon_search.png" />                     
                               </div>
                             <div style="float: left; width: 12%;">
                            
                                 <asp:CheckBox ID="checkDefTxn" runat="server" Text="Set as Default" />
                            
                            </div>
                          
                        </div>
                         
                         <div style="float: left; width: 100%; ">
                         
                             &nbsp;
                         </div>
                         <div style="float: left; width: 100%; text-align: right; font-size: smaller; background-color: #E1F5FF;">
                             <div style="float: left; width: 10%; ">                         
                                  &nbsp;
                             </div>
                              <div style="float: left; width: 10%; text-align: center;">                         
                           
                                  Price Book</div>
                              <div style="float: left; width: 10%; ">                         
                                 &nbsp;
                              </div>
                              <div style="float: left; width: 10%; text-align: center;">                         
                           
                                  Price Level</div>
                              <div style="float: left; width: 10%; ">                         
                                 &nbsp;
                              </div>
                               <div style="float: left; width: 10%; text-align: center;">                         
                                Promotion
                               </div>
                               <div style="float: left; width: 10%; ">                         
                                     &nbsp;
                               </div>
                               <div style="float: left; width: 10%; ">                         
                           
                               </div>
                             
                         </div>
                         <div style="float: left; width: 100%; text-align: right; font-size: smaller;">
                             <div style="float: left; width: 10%; ">                         
                                 &nbsp;
                             </div>
                              <div style="float: left; width: 10%; ">                       
                           
                                  <asp:TextBox ID="txtTxnPB" runat="server" CssClass="TextBox" Width="100%"></asp:TextBox>
                           
                              </div>
                              <div style="float: left; width: 10%; text-align: left;">  
                               &nbsp;                       
                                 <asp:ImageButton ID="ImgBtnPB" runat="server" 
                                     ImageUrl="~/Images/icon_search.png" onclick="ImgBtnPB_Click" /> 
                              </div>
                              <div style="float: left; width: 10%; ">                         
                                 <asp:TextBox ID="txtTxnPBL" runat="server" CssClass="TextBox" Width="100%"></asp:TextBox>
                              </div>
                              <div style="float: left; width: 10%; text-align: left;">                         
                                    &nbsp; 
                                 <asp:ImageButton ID="ImgBtnPbLevel" runat="server" 
                                     ImageUrl="~/Images/icon_search.png" onclick="ImgBtnPbLevel_Click" /> 
                             </div>
                               <div style="float: left; width: 10%; ">    
                                <asp:TextBox ID="txtTxnPromo" runat="server" CssClass="TextBox" Width="100%"></asp:TextBox>                     
                               </div>
                               <div style="float: left; width: 10%;text-align: left; ">                         
                                  &nbsp; 
                                 <asp:ImageButton ID="ImgBtnPromo" runat="server" 
                                     ImageUrl="~/Images/icon_search.png" onclick="ImgBtnPromo_Click" /> 
                                </div>
                               <div style="float: left; width: 10%; ">                         
                               </div>
                               <div style="float: left; width: 10%; ">                         
                                     &nbsp;
                                </div>
                             <div style="float: left; width: 9%; ">                         
                                
                             </div>
                         </div>
                         <div style="float: left; width: 100%; text-align: right; font-size: smaller; background-color: #E1F5FF;">
                             <div style="float: left; width: 10%; ">                         
                                 &nbsp;
                             </div>
                              <div style="float: left; width: 10%; text-align: center;">                         
                                Item Code
                              </div>
                              <div style="float: left; width: 10%; ">                         
                                 &nbsp;
                              </div>
                              <div style="float: left; width: 10%; text-align: center; ">                         
                                    Serial#
                               </div>
                              <div style="float: left; width: 10%; ">                         
                                 &nbsp;
                              </div>
                               <div style="float: left; width: 10%; text-align: center;">                         
                                Brand
                                </div>
                               <div style="float: left; width: 10%; ">                         
                                     &nbsp;
                               </div>
                               <div style="float: left; width: 10%; text-align: center;">                         
                           
                                   Main Category</div>
                               <div style="float: left; width: 5%; ">                         
                                     &nbsp;
                               </div>
                             <div style="float: left; width: 9%; text-align: left;">                         
                                    Sub Category
                             </div>
                         </div>
                         <div style="float: left; width: 100%; text-align: right; font-size: smaller;">
                             <div style="float: left; width: 10%; ">                         
                                 &nbsp;
                             </div>
                              <div style="float: left; width: 10%; ">                       
                           
                                  <asp:TextBox ID="txtTxnItemCd" runat="server" CssClass="TextBox" Width="100%"></asp:TextBox>
                           
                              </div>
                              <div style="float: left; width: 10%; text-align: left;">                         
                               &nbsp; 
                                 <asp:ImageButton ID="ImgBtnItem" runat="server" 
                                     ImageUrl="~/Images/icon_search.png" onclick="ImgBtnItem_Click" /> 
                              </div>
                              <div style="float: left; width: 10%; ">                         
                                 <asp:TextBox ID="txtTxnSerialNo" runat="server" CssClass="TextBox" Width="100%"></asp:TextBox>
                              </div>
                              <div style="float: left; width: 10%; text-align: left;">                         
                                     &nbsp;
                             </div>
                               <div style="float: left; width: 10%; ">                         
                                     <asp:TextBox ID="txtTxnBrand" runat="server" CssClass="TextBox" Width="100%"></asp:TextBox>
                               </div>
                               <div style="float: left; width: 10%; text-align: left;">                         
                                  &nbsp; 
                                 <asp:ImageButton ID="ImgBtnBrand" runat="server" 
                                     ImageUrl="~/Images/icon_search.png" onclick="ImgBtnBrand_Click" /> 
                                </div>
                               <div style="float: left; width: 10%; ">                         
                                     <asp:TextBox ID="txtTxnMainCat" runat="server" CssClass="TextBox" Width="100%"></asp:TextBox>
                               </div>
                               <div style="float: left; width: 5%; text-align: left;">                         
                                     &nbsp; 
                                 <asp:ImageButton ID="ImgBtnMainCat" runat="server" 
                                     ImageUrl="~/Images/icon_search.png" onclick="ImgBtnMainCat_Click" /> 
                                </div>
                             <div style="float: left; width: 9%; ">                         
                               <asp:TextBox ID="txtTxnSubCat" runat="server" CssClass="TextBox" Width="100%"></asp:TextBox>
                             </div>
                              <div style="float: left; width: 5%; text-align: left;">                         
                                   &nbsp; 
                                 <asp:ImageButton ID="ImgBtnSubCat" runat="server" 
                                     ImageUrl="~/Images/icon_search.png" onclick="ImgBtnSubCat_Click" /> 
                                </div>
                         </div>
                         <div style="float: left; width: 100%; text-align: right; font-size: smaller; background-color: #E1F5FF;">
                             <div style="float: left; width: 10%; ">                         
                                 &nbsp;
                             </div>
                              <div style="float: left; width: 10%; text-align: center;">                         
                                Bank code
                              </div>
                              <div style="float: left; width: 5%; ">                         
                                 &nbsp;
                              </div>
                              <div style="float: left; width: 15%; text-align: right;">                         
                                  No of installments
                               </div>
                              <div style="float: left; width: 10%; ">                         
                                 &nbsp;
                              </div>
                               <div style="float: left; width: 10%; ">                         
                                Bank Charge Rt.
                                </div>
                               <div style="float: left; width: 10%; ">                         
                                     &nbsp;
                               </div>
                               <div style="float: left; width: 15%; text-align: left;">                         
                           
                                   Bank Charge value</div>
                              
                            
                         </div>
                         <div style="float: left; width: 100%; text-align: right; font-size: smaller;">
                             <div style="float: left; width: 10%; ">                         
                                 &nbsp;
                             </div>
                              <div style="float: left; width: 10%; ">                       
                           
                                  <asp:TextBox ID="txtTxnBank" runat="server" CssClass="TextBox" Width="100%"></asp:TextBox>
                           
                              </div>
                              <div style="float: left; width: 10%; text-align: left; ">                         
                                 &nbsp; 
                                 <asp:ImageButton ID="ImgBtnBankCd" runat="server" 
                                     ImageUrl="~/Images/icon_search.png" onclick="ImgBtnBankCd_Click" /> 
                              </div>
                              <div style="float: left; width: 10%; ">                         
                                 <asp:TextBox ID="txtTxnPD" runat="server" CssClass="TextBox" Width="100%" 
                                      MaxLength="4" ToolTip="Credit Card Interest Free Period"></asp:TextBox>
                              </div>
                              <div style="float: left; width: 10%; ">                         
                                     &nbsp;
                             </div>
                               <div style="float: left; width: 10%; ">                         
                                     <asp:TextBox ID="txtTxnBnkCgRt" runat="server" CssClass="TextBox" Width="100%"></asp:TextBox>
                               </div>
                               <div style="float: left; width: 10%; ">                         
                                 &nbsp;
                                </div>
                               <div style="float: left; width: 10%; ">                         
                                     <asp:TextBox ID="txtTxnBnkCgVal" runat="server" CssClass="TextBox" Width="100%"></asp:TextBox>
                               </div>
                               <div style="float: left; width: 8%; ">                         
                                     &nbsp;
                                </div>
                             <div style="float: left; width: 9%; text-align: left;">                       
                              
                                 <asp:ImageButton ID="ImgBtnAddTxn" runat="server" 
                                     ImageUrl="~/Images/download_arrow_icon.png" onclick="ImgBtnAddTxn_Click" />                              
                             </div>
                         </div>
                          <div style="float: left; width: 100%; ">
                              <asp:HiddenField ID="HiddenField_TxnSeq" runat="server" Value="0" />
                          </div>
                         <div style="float: left; width: 100%;">
                             <asp:Panel ID="Panel18" runat="server" ScrollBars="Both">                          
                             <asp:GridView ID="grvTxnList" runat="server" CellPadding="4" 
                                 ForeColor="#333333" Width="100%" onrowdeleting="grvTxnList_RowDeleting" 
                                     AutoGenerateColumns="False">
                                 <AlternatingRowStyle BackColor="White" />
                                 <Columns>
                                     <asp:TemplateField>
                                         <EditItemTemplate>
                                             <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                         </EditItemTemplate>
                                         <ItemTemplate>
                                             <asp:Label ID="Label1" runat="server"></asp:Label>
                                             <asp:ImageButton ID="ImageButton11" runat="server" CommandName="Delete" 
                                                 ImageUrl="~/Images/Delete.png" />
                                         </ItemTemplate>
                                         <ItemStyle HorizontalAlign="Center" />
                                     </asp:TemplateField>
                                     <asp:TemplateField Visible="False">
                                         <EditItemTemplate>
                                             <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                         </EditItemTemplate>
                                         <ItemTemplate>
                                             <asp:Label ID="lblTxnSeq" runat="server"></asp:Label>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:BoundField DataField="Stp_txn_tp" HeaderText="Txn. Type" />
                                     <asp:BoundField DataField="Stp_from_dt" HeaderText="From Dt." />
                                     <asp:BoundField DataField="Stp_to_dt" HeaderText="To Dt." />
                                     <asp:BoundField DataField="Stp_def" HeaderText="Is Default" />
                                     <asp:BoundField DataField="Stp_pay_tp" HeaderText="Pay Type" />
                                     <asp:BoundField DataField="Stp_pb" HeaderText="Price Book" />
                                     <asp:BoundField DataField="Stp_pb_lvl" HeaderText="Price book level" />
                                     <asp:BoundField DataField="Stp_pro" HeaderText="Promotion" />
                                     <asp:BoundField DataField="Stp_itm" HeaderText="Item Code" />
                                     <asp:BoundField DataField="Stp_brd" HeaderText="Brand" />
                                     <asp:BoundField DataField="Stp_main_cat" HeaderText="Main Category" />
                                     <asp:BoundField DataField="Stp_cat" HeaderText="Category" />
                                     <asp:BoundField DataField="Stp_ser" HeaderText="Serial #" />
                                     <asp:BoundField DataField="Stp_bank" HeaderText="Bank Code" />
                                     <asp:BoundField DataField="Stp_bank_chg_rt" HeaderText="Bank Chg. Rt." />
                                     <asp:BoundField DataField="Stp_bank_chg_val" HeaderText="Bank Chg. Val" />
                                     <asp:BoundField DataField="Stp_pd " HeaderText="CC. Interest free pd." />
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
                </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
