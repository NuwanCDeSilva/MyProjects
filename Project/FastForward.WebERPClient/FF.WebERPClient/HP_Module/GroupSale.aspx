<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="GroupSale.aspx.cs" Inherits="FF.WebERPClient.HP_Module.GroupSale" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/uc_MsgInfo.ascx" TagName="uc_MsgInfo" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/uc_CommonSearch.ascx" TagName="uc_CommonSearch"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/uc_CustomerCreation.ascx" TagName="uc_CustomerCreation"
    TagPrefix="uc2" %>
<%@ Register Src="../UserControls/uc_CustCreationExternalDet.ascx" TagName="uc_CustCreationExternalDet"
    TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function uppercase() {
            key = window.event.keyCode;
            if ((key > 0x60) && (key < 0x7B))
                window.event.keyCode = key - 0x20;
        }

        function DeleteConfirm() {
            if (confirm("Are you sure to delete?")) {
                return true;
            }
            else {
                return false;
            }
        }
        function ApproveConfirm() {
            if (confirm("Are you sure to approve?")) {
                return true;
            }
            else {
                return false;
            }
        }
        function numbersonly(e, decimal) {
            var key;
            var keychar;

            if (window.event) {
                key = window.event.keyCode;
            }
            else if (e) {
                key = e.which;
            }
            else {
                return true;
            }
            keychar = String.fromCharCode(key);

            if ((key == null) || (key == 0) || (key == 8) || (key == 9) || (key == 13) || (key == 27)) {
                return true;
            }
            else if ((("0123456789").indexOf(keychar) > -1)) {
                return true;
            }
            else if (decimal && (keychar == ".")) {
                return true;
            }
            else
                return false;
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

        //ADDED BY SACHITH
        //2012/08/16
        //POPOUP CLOSE PROB
        function LoadPopUp() {
            document.getElementById('<%=HiddenFieldCusCrePopUpStats.ClientID %>').value = "1";
        }

        function ClosePopUp() {
            document.getElementById('<%=HiddenFieldCusCrePopUpStats.ClientID %>').value = "0";
        }
        //END
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="float: left; width: 100%;">
        <div style="float: left; width: 99%; height: 22px; text-align: right; background-color: #1E4A9F">
            <div style="float: left;">
                <asp:Label ID="lblDispalyInfor" runat="server" Text="Back date allow for" CssClass="Label"
                    ForeColor="Yellow"></asp:Label>
            </div>
            <div style="float: right;">
                <asp:Button ID="btnApprove" runat="server" Text="Approve" Height="85%" Width="70px"
                    Enabled="false" OnClientClick="return ApproveConfirm()" CssClass="Button" OnClick="btnApprove_Click" />
                <asp:Button ID="btnUpdate" runat="server" Text="Save" Height="85%" Width="70px" CssClass="Button"
                    OnClick="btnUpdate_Click" />
                <asp:Button ID="btnClr" runat="server" Text="Clear" Height="85%" Width="70px" CssClass="Button"
                    OnClick="btnClr_Click" />
                <asp:Button ID="btnClose" runat="server" Text="Close" Height="100%" Width="70px"
                    CssClass="Button" OnClick="Close" />
            </div>
        </div>
    </div>
    <asp:UpdatePanel ID="pnl_head" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="float: left; height: 5px; width: 100%;">
            </div>
            <div style="float: left; width: 100%">
                <div style="float: left; width: 1%">
                    &nbsp;
                </div>
                <div style="float: left; width: 35%">
                    <asp:Label ID="Label24" runat="server" Text="Group Sale Code : "></asp:Label>
                    <asp:TextBox ID="txtGroupSaleCode" runat="server" CssClass="TextBox" Width="200px"></asp:TextBox>
                    <asp:ImageButton ID="imgbtnSearchGrpCode" runat="server" ImageUrl="~/Images/icon_search.png"
                        OnClick="imgbtnSearchGrpCode_Click" />
                </div>
                <div style="float: left; width: 2%;">
                    &nbsp;
                </div>
                <div style="float: left; width: 5%">
                    Date
                </div>
                <div style="float: left; width: 1%;">
                    &nbsp;
                </div>
                <div style="float: left; width: 20%">
                    <asp:TextBox ID="txtDate" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                    <asp:Image ID="imgDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                    <cc1:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="imgDate"
                        TargetControlID="txtDate" Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                </div>
                <div style="float: left; height: 5px; width: 100%;">
                </div>
            </div>
            <div class="commonPageCss" style="float: left; width: 100%">
                <div style="float: left; width: 1%">
                    &nbsp;
                </div>
                <div style="float: left; width: 98%; height: 1000px;">
                    <asp:Panel ID="pnlComp" runat="server" ScrollBars="Auto" Font-Size="11px" Font-Names="Tahoma"
                        Height="700px">
                        <asp:UpdatePanel ID="pnl_Comp" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="commonPageCss" style="float: left; width: 100%">
                                    <div class="CollapsiblePanelHeader" style="width: 80%">
                                        Company Details</div>
                                    <div style="float: left; width: 2%">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 5%">
                                        <asp:ImageButton ID="imgbtnSearchComp" runat="server" ImageUrl="~/Images/icon_search.png"
                                            OnClick="imgbtnSearchComp_Click" />
                                    </div>
                                    <div style="float: left; width: 13%">
                                        <asp:Button ID="btnCreateNew" runat="server" Text="Create New" CssClass="Button"
                                            ClientIDMode="Static" />
                                    </div>
                                    <%--  <div style="float: left; width: 10%">
                                &nbsp;
                            </div>--%>
                                </div>
                                <div>
                                    <table>
                                        <tr style="background-color: #EFF3FB;">
                                            <td style="padding-top: 6px; width: 75px; background-color: #D1DCF3;">
                                                <asp:Label ID="Label2" runat="server" Text="Company Code"></asp:Label>
                                            </td>
                                            <td style="padding-top: 6px">
                                                :
                                            </td>
                                            <td style="text-align: left; background-color: #EFF3FB; padding-top: 6px; width: 175px"
                                                align="left">
                                                <%-- <asp:Label ID="lblCompCode" runat="server"></asp:Label>--%>
                                                <asp:TextBox ID="txtCompCode" runat="server" ClientIDMode="Static" BackColor="#EFF3FB"
                                                    Font-Size="11px" Width="160"></asp:TextBox>
                                            </td>
                                            <td style="padding-top: 6px; background-color: #D1DCF3; width: 90px">
                                                <asp:Label ID="Label1" runat="server" Text="Company Name"></asp:Label>
                                            </td>
                                            <td style="padding-top: 6px">
                                                :
                                            </td>
                                            <td style="text-align: left; padding-top: 6px; width: 380px">
                                                <asp:Label ID="lblCompName" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-top: 6px; background-color: #D1DCF3; width: 75px">
                                                <asp:Label ID="Label3" runat="server" Text="Address 1"></asp:Label>
                                            </td>
                                            <td style="padding-top: 6px; background-color: #EFF3FB;">
                                                :
                                            </td>
                                            <td colspan="4" style="text-align: left; background-color: #EFF3FB; width: 200px">
                                                <asp:Label ID="lblAddr1" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-top: 6px; background-color: #D1DCF3">
                                                <asp:Label ID="Label4" runat="server" Text="Address 2"></asp:Label>
                                            </td>
                                            <td style="padding-top: 6px; background-color: #EFF3FB;">
                                                :
                                            </td>
                                            <td style="text-align: left; padding-top: 6px; background-color: #EFF3FB;" colspan="4"
                                                width="555px">
                                                <asp:Label ID="lblAddr2" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-top: 6px; background-color: #D1DCF3; width: 75px">
                                                <asp:Label ID="Label5" runat="server" Text="Telephone #"></asp:Label>
                                            </td>
                                            <td style="padding-top: 6px; background-color: #EFF3FB;">
                                                :
                                            </td>
                                            <td style="text-align: left; background-color: #EFF3FB; padding-top: 6px; width: 175px">
                                                <asp:Label ID="lblTel" runat="server"></asp:Label>
                                            </td>
                                            <td style="padding-top: 6px; background-color: #D1DCF3; width: 75px">
                                                <asp:Label ID="Label6" runat="server" Text="Fax #"></asp:Label>
                                            </td>
                                            <td style="padding-top: 6px; background-color: #EFF3FB;">
                                                :
                                            </td>
                                            <td style="text-align: left; background-color: #EFF3FB; padding-top: 6px; width: 380px">
                                                <asp:Label ID="lblFax" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-top: 6px; background-color: #D1DCF3; width: 75px">
                                                <asp:Label ID="Label7" runat="server" Text="E-Mail"></asp:Label>
                                            </td>
                                            <td style="padding-top: 6px; background-color: #EFF3FB;">
                                                :
                                            </td>
                                            <td style="text-align: left; padding-top: 6px; background-color: #EFF3FB; width: 175px">
                                                <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                            </td>
                                            <td style="padding-top: 6px; background-color: #D1DCF3; width: 75px">
                                                <asp:Label ID="Label8" runat="server" Text="Country"></asp:Label>
                                            </td>
                                            <td style="padding-top: 6px; background-color: #EFF3FB;">
                                                :
                                            </td>
                                            <td style="text-align: left; padding-top: 6px; background-color: #EFF3FB; width: 380px">
                                                <asp:Label ID="lblCountry" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div style="float: left; height: 8px; width: 100%;">
                                </div>
                                <div class="commonPageCss" style="float: left; width: 100%">
                                    <div class="CollapsiblePanelHeader" style="width: 80%">
                                        Group Sale Details
                                    </div>
                                </div>
                                <div style="float: left; height: 5px; width: 100%;">
                                </div>
                                <div>
                                    <table>
                                        <tr style="background-color: #EFF3FB;">
                                            <td style="padding-top: 6px; width: 155px; background-color: #D1DCF3;">
                                                <asp:Label ID="Label9" runat="server" Text="Group Sale Description"></asp:Label>
                                            </td>
                                            <td style="text-align: left; background-color: #EFF3FB; padding-top: 6px; width: 590px"
                                                align="left">
                                                <asp:Label ID="lblGSaleDesn" runat="server"></asp:Label>
                                            </td>
                                            <%--              <td>
                                        <asp:Button ID="btnUpdate" runat="server" Text="Save" Height="85%" Width="70px" CssClass="Button"
                                            OnClick="btnUpdate_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnClr" runat="server" Text="Clear" Height="85%" Width="70px" CssClass="Button"
                                            OnClick="btnClr_Click" />
                                    </td>--%>
                                        </tr>
                                        <tr>
                                            <td style="padding-top: 6px; width: 75px;">
                                                <asp:RadioButton ID="optCredit" runat="server" Text="Credit Sale" GroupName="opt"
                                                    AutoPostBack="true" />
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="optHire" runat="server" Text="Hire Sale" GroupName="opt" AutoPostBack="true" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div style="float: left; height: 8px; width: 100%;">
                                </div>
                                <div style="float: left; height: 8px; width: 100%;">
                                    <div style="float: left; height: 8px; width: 10%;">
                                        <asp:Label ID="Label10" runat="server" Text="Visit Date"></asp:Label>
                                    </div>
                                    <div style="float: left; width: 1%">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; height: 8px; width: 15%;">
                                        <asp:TextBox ID="txtVisitDate" runat="server" CssClass="TextBox" Width="90px" Enabled="false"></asp:TextBox>
                                        <asp:Image ID="imgVisitDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtVisitDate"
                                            Format="dd/MMM/yyyy" PopupButtonID="imgVisitDate" Enabled="True">
                                        </asp:CalendarExtender>
                                    </div>
                                    <div style="float: left; width: 2%">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; height: 8px; width: 7%;">
                                        <asp:Label ID="Label11" runat="server" Text="Valid From"></asp:Label>
                                    </div>
                                    <div style="float: left; width: 1%">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; height: 8px; width: 15%;">
                                        <asp:TextBox ID="txtValidFrom" runat="server" CssClass="TextBox" Width="90px" Enabled="false"></asp:TextBox>
                                        <asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtValidFrom"
                                            Format="dd/MMM/yyyy" PopupButtonID="imgFromDate" Enabled="True">
                                        </asp:CalendarExtender>
                                    </div>
                                    <div style="float: left; width: 4.5%">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; left: 8px; width: 11%;">
                                        <asp:Label ID="Label12" runat="server" Text="Valid To"></asp:Label>
                                    </div>
                                    <div style="float: left; width: 1%">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; height: 8px; width: 15%;">
                                        <asp:TextBox ID="txtValidTo" runat="server" CssClass="TextBox" Width="90px" Enabled="false"></asp:TextBox>
                                        <asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtValidTo"
                                            Format="dd/MMM/yyyy" PopupButtonID="imgToDate" Enabled="True">
                                        </asp:CalendarExtender>
                                    </div>
                                    <div style="float: left; width: 20%">
                                        &nbsp;
                                    </div>
                                </div>
                                <div style="float: left; height: 14px; width: 100%;">
                                </div>
                                <div style="float: left; height: 8px; width: 100%;">
                                    <div style="float: left; height: 8px; width: 10%;">
                                        <asp:Label ID="Label13" runat="server" Text="Follow up Officer"></asp:Label>
                                    </div>
                                    <div style="float: left; width: 1%">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; height: 8px; width: 30%;">
                                        <asp:TextBox ID="txtFollow" runat="server" CssClass="TextBox" Width="265px" MaxLength="100"></asp:TextBox>
                                    </div>
                                    <div style="float: left; height: 8px; width: 10%;">
                                        <asp:Label ID="Label14" runat="server" Text="Contact Person"></asp:Label>
                                    </div>
                                    <div style="float: left; width: 1%">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; height: 8px; width: 30%;">
                                        <asp:TextBox ID="txtContact" runat="server" CssClass="TextBox" Width="252px" MaxLength="20"></asp:TextBox>
                                    </div>
                                </div>
                                <div style="float: left; height: 12px; width: 100%;">
                                </div>
                                <div style="float: left; height: 8px; width: 100%;">
                                    <div style="float: left; height: 8px; width: 10%;">
                                        <asp:Label ID="Label15" runat="server" Text="Contact Number"></asp:Label>
                                    </div>
                                    <div style="float: left; width: 1%">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; height: 8px; width: 30%;">
                                        <asp:TextBox ID="txtContNo" runat="server" CssClass="TextBox" Width="165px" onKeyPress="return numbersonly(event,false)"
                                            MaxLength="10"></asp:TextBox>
                                    </div>
                                </div>
                                <div style="float: left; height: 15px; width: 100%;">
                                </div>
                                <div class="commonPageCss" style="float: left; width: 100%">
                                    <div class="CollapsiblePanelHeader" style="width: 80%">
                                        Customer Details
                                    </div>
                                    <div style="float: left; width: 2%">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 5%">
                                        <asp:ImageButton ID="imgBtnCustomer" runat="server" ImageUrl="~/Images/icon_search.png"
                                            OnClick="imgBtnCustomer_Click" />
                                    </div>
                                    <div style="float: left; width: 13%">
                                        <asp:Button ID="btnNewCust" runat="server" Text="Create New" CssClass="Button" ClientIDMode="Static"
                                            OnClientClick="LoadPopUp()" />
                                    </div>
                                </div>
                                <div style="float: left; height: 5px; width: 100%;">
                                </div>
                                <div>
                                    <table>
                                        <tr style="background-color: #EFF3FB;">
                                            <td style="padding-top: 6px; width: 75px; background-color: #D1DCF3;">
                                                <asp:Label ID="Label16" runat="server" Text="Customer Code"></asp:Label>
                                            </td>
                                            <td style="padding-top: 6px">
                                                :
                                            </td>
                                            <td style="text-align: right; background-color: #EFF3FB; padding-top: 6px; width: 155px"
                                                align="left">
                                                <asp:TextBox ID="txtCustCode" runat="server" ClientIDMode="Static" BackColor="#EFF3FB"
                                                    Width="160" Font-Size="11px"></asp:TextBox>
                                            </td>
                                            <td style="padding-top: 6px; background-color: #D1DCF3; width: 110px">
                                                <asp:Label ID="Label17" runat="server" Text="Customer Name"></asp:Label>
                                            </td>
                                            <td style="padding-top: 6px">
                                                :
                                            </td>
                                            <td style="text-align: left; padding-top: 6px; width: 380px">
                                                <asp:Label ID="lblCustName" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="background-color: #EFF3FB;">
                                            <td style="padding-top: 6px; width: 75px; background-color: #D1DCF3;" colspan="1">
                                                <asp:Label ID="Label18" runat="server" Text="Address"></asp:Label>
                                            </td>
                                            <td style="padding-top: 6px" colspan="1" rowspan="1">
                                                :
                                            </td>
                                            <td style="text-align: left; background-color: #EFF3FB; padding-top: 6px; width: 155px"
                                                align="left" colspan="4">
                                                <%--                       <asp:TextBox ID="txtAddr" runat="server" ClientIDMode="Static" BackColor="#EFF3FB"
                                                        BorderColor="#EFF3FB" BorderStyle="None" Font-Size="11px"></asp:TextBox>--%>
                                                <asp:Label ID="lblCustAddr" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div style="float: left; height: 5px; width: 100%;">
                                </div>
                                <div style="float: left; height: 8px; width: 100%;">
                                    <div style="float: left; height: 8px; width: 8%;">
                                        <asp:Label ID="Label19" runat="server" Text="No of Products"></asp:Label>
                                    </div>
                                    <div style="float: left; width: 1%">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; height: 8px; width: 10%;">
                                        <asp:TextBox ID="txtNoOfProd" runat="server" CssClass="TextBox" Width="70px" Style="text-align: right"
                                            onKeyPress="return numbersonly(event,false)"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 1%">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; height: 8px; width: 8%;">
                                        <asp:Label ID="Label25" runat="server" Text="No of Accounts"></asp:Label>
                                    </div>
                                    <div style="float: left; width: 1%">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; height: 8px; width: 10%;">
                                        <asp:TextBox ID="txtNoOfAcc" runat="server" CssClass="TextBox" Width="40px" Style="text-align: right"
                                            onKeyPress="return numbersonly(event,false)"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 1%">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; height: 8px; width: 8%;">
                                        <asp:Label ID="Label20" runat="server" Text="Total Value"></asp:Label>
                                    </div>
                                    <div style="float: left; width: 1%">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; height: 8px; width: 28%;">
                                        <asp:TextBox ID="txtValue" runat="server" CssClass="TextBox" Width="70px" Style="text-align: right"
                                            onKeyPress="return isNumberKeyAndDot(event,value)"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 1%">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; height: 8px; width: 15%;">
                                        <asp:ImageButton ID="btnAddItemNew" runat="server" ImageUrl="~/Images/download_arrow_icon.png"
                                            OnClick="btnAdd_Click" Height="16px" Width="16px" ToolTip="Add Item" />
                                    </div>
                                </div>
                                <div style="float: left; height: 13px; width: 100%;">
                                </div>
                                <div class="commonPageCss" style="float: left; width: 100%">
                                    <div style="float: left; width: .5%">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 80%">
                                        <asp:UpdatePanel ID="pnlCust" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:GridView ID="gvCustProd" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                    ForeColor="#333333" OnRowCommand="gvCustProd_RowCommand" CssClass="GridView"
                                                    EmptyDataText="No data found" ShowHeaderWhenEmpty="True" Width="100%" AllowPaging="true"
                                                    PageSize="3" OnPageIndexChanging="gvCustProd_PageIndexChanging" PagerSettings-Mode="NextPreviousFirstLast"
                                                    PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Left">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Customer Code" HeaderStyle-HorizontalAlign="Left"
                                                            ShowHeader="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCustCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "HGC_CUST_CD") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Customer Name" HeaderStyle-HorizontalAlign="Left"
                                                            ShowHeader="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCustomerName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MasterBusinessCompany.mbe_name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Products" HeaderStyle-HorizontalAlign="Left" ShowHeader="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNoOfProd" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "HGC_NO_ITM") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Accounts" HeaderStyle-HorizontalAlign="Left" ShowHeader="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNoOfAcc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "HGC_NO_ACC") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Value" HeaderStyle-HorizontalAlign="Left" ShowHeader="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblValue" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "HGC_VAL") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgbtndelAllSerial" runat="server" ImageUrl="~/Images/Delete.png"
                                                                    CommandName="DeleteItem" OnClientClick="return DeleteConfirm()" CommandArgument='<%# Eval("HGC_CUST_CD")  %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
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
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div style="float: left; width: 18%">
                                        <table>
                                            <tr style="background-color: #EFF3FB;">
                                                <td style="padding-top: 6px; width: 130px; background-color: #D1DCF3;">
                                                    <asp:Label ID="Label21" runat="server" Text="No of Items :"></asp:Label>
                                                </td>
                                                <td style="text-align: right; background-color: #EFF3FB; padding-top: 6px; width: 175px"
                                                    align="left">
                                                    <asp:Label ID="lbl_items" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr style="background-color: #EFF3FB;">
                                                <td style="padding-top: 6px; width: 75px; background-color: #D1DCF3;">
                                                    <asp:Label ID="Label22" runat="server" Text="Total Value   :"></asp:Label>
                                                </td>
                                                <td style="text-align: right; background-color: #EFF3FB; padding-top: 6px; width: 175px"
                                                    align="left">
                                                    <asp:Label ID="lbl_value" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr style="background-color: #EFF3FB;">
                                                <td style="padding-top: 6px; width: 75px; background-color: #D1DCF3;">
                                                    <asp:Label ID="Label23" runat="server" Text="No of A/Cs   :"></asp:Label>
                                                </td>
                                                <td style="text-align: right; background-color: #EFF3FB; padding-top: 6px; width: 175px"
                                                    align="left">
                                                    <asp:Label ID="lbl_acc" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr style="background-color: #EFF3FB;">
                                                <td style="padding-top: 6px; width: 75px; background-color: #D1DCF3;">
                                                    <asp:Label ID="Label26" runat="server" Text="Customers :"></asp:Label>
                                                </td>
                                                <td style="text-align: right; background-color: #EFF3FB; padding-top: 6px; width: 175px"
                                                    align="left">
                                                    <asp:Label ID="lbl_Cust" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr style="background-color: #EFF3FB;">
                                                <td style="padding-top: 6px; width: 75px; background-color: #D1DCF3;">
                                                    <asp:Label ID="Label27" runat="server" Text="Status :"></asp:Label>
                                                </td>
                                                <td style="text-align: left; background-color: #EFF3FB; padding-top: 6px; width: 175px"
                                                    align="left">
                                                    <asp:Label ID="lbl_status" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div style="float: left; width: .5%">
                                        &nbsp;
                                    </div>
                                </div>
                                <div style="display: none;">
                                    <asp:Button ID="btnComp" runat="server" OnClick="GetCompanyData" />
                                    <asp:Button ID="btnCountry" runat="server" OnClick="GetCountryData" />
                                    <asp:Button ID="btnCustomer" runat="server" OnClick="GetCustomerData" />
                                    <asp:Button ID="btnGroupSaleCode" runat="server" OnClick="GetGroupSaleData" />
                                </div>
                                <%-- Modal Popup Extenders for create new company --%>
                                <div>
                                    <asp:ModalPopupExtender ID="MPECompany" TargetControlID="btnCreateNew" runat="server"
                                        ClientIDMode="Static" PopupControlID="pnlCompany" BackgroundCssClass="modalBackground"
                                        CancelControlID="imgBtnBusClose" PopupDragHandleControlID="divpopCompHeader">
                                    </asp:ModalPopupExtender>
                                    <asp:Panel ID="pnlCompany" runat="server" Height="250px" Width="600px" CssClass="ModalWindow">
                                        <div class="popUpHeader" id="divpopCompHeader" runat="server">
                                            <div style="float: left; width: 80%" runat="server" id="div2">
                                                Group Sale Company Detail</div>
                                            <div style="float: left; width: 20%; text-align: right">
                                                <asp:ImageButton ID="imgBtnBusClose" runat="server" ImageUrl="~/Images/0032-01-16x16x16.ICO" />&nbsp;</div>
                                        </div>
                                        <%-- PopUp Handler for drag and control --%>
                                        <div style="float: left; height: 8px; width: 100%;">
                                        </div>
                                        <div style="height: 22px; text-align: right;">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" Height="85%" Width="70px" CssClass="Button"
                                                OnClick="btnSave_Click" />
                                        </div>
                                        <div style="float: left; height: 23px; width: 100%;">
                                        </div>
                                        <div style="float: left; height: 23px; width: 100%;">
                                            <div style="float: left; width: 1%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 15%;">
                                                Company Name :</div>
                                            <div style="float: left; width: 1%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 70%;">
                                                <asp:TextBox ID="txtCompName" runat="server" MaxLength="30" CssClass="TextBox" Width="375"
                                                    ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="float: left; height: 23px; width: 100%;">
                                            <div style="float: left; width: 1%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 15%;">
                                                Address 1 :</div>
                                            <div style="float: left; width: 1%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 70%;">
                                                <asp:TextBox ID="txtAddr1" runat="server" MaxLength="100" CssClass="TextBox" Width="375"
                                                    ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="float: left; height: 23px; width: 100%;">
                                            <div style="float: left; width: 1%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 15%;">
                                                Address 2 :</div>
                                            <div style="float: left; width: 1%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 70%;">
                                                <asp:TextBox ID="txtAddr2" runat="server" MaxLength="100" CssClass="TextBox" Width="375"
                                                    ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="float: left; height: 23px; width: 100%;">
                                            <div style="float: left; width: 1%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 15%;">
                                                Telephone # :</div>
                                            <div style="float: left; width: 1%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 70%;">
                                                <asp:TextBox ID="txtTel" runat="server" CssClass="TextBox" Width="200" ClientIDMode="Static"
                                                    onKeyPress="return numbersonly(event,false)" MaxLength="10"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="float: left; height: 23px; width: 100%;">
                                            <div style="float: left; width: 1%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 15%;">
                                                Fax :</div>
                                            <div style="float: left; width: 1%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 70%;">
                                                <asp:TextBox ID="txtFax" runat="server" CssClass="TextBox" Width="200" ClientIDMode="Static"
                                                    onKeyPress="return numbersonly(event,false)" MaxLength="10"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="float: left; height: 23px; width: 100%;">
                                            <div style="float: left; width: 1%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 15%;">
                                                E-Mail :</div>
                                            <div style="float: left; width: 1%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 70%;">
                                                <asp:TextBox ID="txtemail" runat="server" CssClass="TextBox" Width="375" ClientIDMode="Static"
                                                    MaxLength="50"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%--<div style="float: left; height: 23px; width: 100%;">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 15%;">
                                                    Country Code :</div>
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 18%;">
                                                    <asp:TextBox ID="txtCountry" runat="server" CssClass="TextBox" Width="75" ClientIDMode="Static"></asp:TextBox>
                                                    <asp:ImageButton ID="imgbtnCountry" runat="server" ImageUrl="~/Images/icon_search.png"
                                                        OnClick="imgbtnCountry_Click" />
                                                </div>
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 20%;">
                                                    <asp:TextBox ID="txtCountryDesc" runat="server" CssClass="TextBox" Width="265" ClientIDMode="Static"></asp:TextBox>
                                                </div>
                                            </div>--%>
                                    </asp:Panel>
                                </div>
                                <div>
                                    <%--ADDED BY SACHITH
                                    2012/08/16
                                    ADD NEW CUSTOMER POPUP--%>
                                    <%--START--%>
                                    <div style="display: none;">
                                        <asp:TextBox ID="hdnText" runat="server" OnTextChanged="txtHiddenCustCD_TextChanged"></asp:TextBox>
                                    </div>
                                    <asp:HiddenField ID="HiddenFieldCusCrePopUpStats" runat="server" Value="0" />
                                    <asp:ModalPopupExtender ID="ModalPopupExtender1" TargetControlID="btnNewCust" runat="server"
                                        ClientIDMode="Static" PopupControlID="pnlCustomer" BackgroundCssClass="modalBackground"
                                        CancelControlID="ImgCustomerAdd" PopupDragHandleControlID="div3" OnLoad="ModalPopupExtender1_Load">
                                    </asp:ModalPopupExtender>
                                    <asp:Panel ID="pnlCustomer" runat="server" Width="650px" CssClass="ModalWindow">
                                        <div class="popUpHeader" id="div1" runat="server">
                                            <div style="float: left; width: 80%" runat="server" id="div3">
                                                Group Sale Customer Detail</div>
                                            <div style="float: left; width: 20%; text-align: right">
                                                <asp:ImageButton ID="ImgCustomerAdd" runat="server" ImageUrl="~/Images/0032-01-16x16x16.ICO"
                                                    OnClientClick="ClosePopUp()" />&nbsp;</div>
                                        </div>
                                        <div style="clear: both; float: left; display: block;">
                                            <asp:Panel ID="PanelCCre" runat="server" Height="500px" ScrollBars="Auto">
                                                <div style="float: left; height: 8px; width: 100%;">
                                                </div>
                                                <div style="height: 22px; text-align: right;">
                                                    <asp:Button ID="ButtonAddCus" runat="server" Text="Save" Height="80%" Width="70px"
                                                        CssClass="Button" OnClick="ButtonAddCus_Click" />
                                                    <asp:Button ID="ButtonClear" runat="server" Text="Clear" Height="80%" Width="70px"
                                                        CssClass="Button" OnClick="ButtonClear_Click" />
                                                </div>
                                                <div style="float: left; width: 100%">
                                                    <div style="float: left; width: 100%">
                                                        <uc2:uc_CustomerCreation ID="cusCreP1" runat="server" />
                                                    </div>
                                                    <div style="float: left; width: 100%">
                                                        <uc3:uc_CustCreationExternalDet ID="cusCreP2" runat="server" EnableViewState="false" />
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </asp:Panel>
                                    <%--END OF CUSTOMER CREATION POPUP--%>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
