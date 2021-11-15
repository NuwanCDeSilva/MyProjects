<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="RCC_Page.aspx.cs" Inherits="FF.WebERPClient.Inventory_Module.RCC_Page"
    EnableEventValidation="false" %>

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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ModalPopupExtender ID="MPESerial" TargetControlID="ImgBtnProductSrch" runat="server"
        ClientIDMode="Static" PopupControlID="pnlSerialSearch" BackgroundCssClass="modalBackground"
        CancelControlID="imgBtnBusClose" PopupDragHandleControlID="divpopCompHeader">
    </asp:ModalPopupExtender>
    <div style="float: left; width: 100%; padding-top: 2px; padding-bottom: 2px;">
        <asp:Panel ID="pnlSerialSearch" runat="server" Height="300px" Width="600px" CssClass="ModalWindow">
            <div class="popUpHeader" id="divpopCompHeader" runat="server">
                <div style="float: left; width: 80%" runat="server" id="div2">
                    Product Search</div>
                <div style="float: left; width: 20%; text-align: right">
                    <asp:ImageButton ID="imgBtnBusClose" runat="server" ImageUrl="~/Images/0032-01-16x16x16.ICO" />&nbsp;</div>
            </div>
            <%-- PopUp Handler for drag and control --%>
            <div style="float: left; height: 8px; width: 100%;">
            </div>
            <div style="float: left; height: 6px; width: 100%;">
            </div>
            <div style="float: left; height: 6px; width: 100%;">
                <div style="float: left; width: 1%;">
                    &nbsp;
                </div>
                <div style="float: left; width: 99%">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label></div>
            </div>
            <div style="float: left; height: 6px; width: 100%;">
            </div>
            <div style="float: left; width: 100%;">
                <div style="float: left; width: 1%;">
                    &nbsp;
                </div>
                <div style="float: left; width: 30%">
                    <asp:DropDownList ID="ddlCriteria" runat="server" AutoPostBack="false" Width="100%"
                        AppendDataBoundItems="true" CssClass="ComboBox" />
                </div>
                <div style="float: left; width: 1%;">
                    &nbsp;
                </div>
                <div style="float: left; width: 50%">
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="TextBox" Width="100%"></asp:TextBox>
                </div>
                <div style="float: left; width: 1%;">
                    &nbsp;
                </div>
                <div style="float: left; width: 17%">
                    <asp:Button Text="Search" ID="btnSearch" runat="server" CssClass="Button" Width="70%"
                        OnClick="btnSearch_Click" />
                </div>
            </div>
            <div style="float: left; width: 100%;">
                <div style="float: left; width: 1%;">
                    &nbsp;
                </div>
                <div id="divserial" runat="server" style="float: left; width: 99%;">
                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Height="220px" Width="100%">
                        <asp:GridView ID="gvSerial" CssClass="GridView" CellPadding="4" ForeColor="#333333"
                            OnSelectedIndexChanged="LoadSerial" OnRowDataBound="gvSerial_OnRowBind" GridLines="Both"
                            runat="server" AutoGenerateColumns="false" DataKeyNames="Serial_No,Warranty_No,Doc_No,Item_Code,ITS_SEQ_NO,ITS_ITM_LINE,ITS_BATCH_LINE,ITS_SER_LINE">
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField='Serial_No' HeaderText='Serial' HeaderStyle-Width="75px"
                                    HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField='Warranty_No' HeaderText='Warranty' HeaderStyle-Width="75px"
                                    HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField='Doc_No' HeaderText='Doc No' HeaderStyle-Width="75px" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField='Item_Code' HeaderText='Item Code' HeaderStyle-Width="75px"
                                    HeaderStyle-HorizontalAlign="Left" />
                            </Columns>
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                        <%--<asp:GridView ID="gvSerial" runat="server" AutoGenerateColumns="False" CellPadding="4" OnRowDataBound="gvSerial_OnRowBind"
                            OnSelectedIndexChanged="LoadSerial" ForeColor="#333333" CssClass="GridView" EmptyDataText="No data found"
                            ShowHeaderWhenEmpty="True" Width="100%" PagerStyle-HorizontalAlign="Left">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="Customer Code" HeaderStyle-HorizontalAlign="Left"
                                    ShowHeader="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Serial_No") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Products" HeaderStyle-HorizontalAlign="Left" ShowHeader="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoOfProd" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Warranty_No") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Accounts" HeaderStyle-HorizontalAlign="Left" ShowHeader="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoOfAcc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Doc_No") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Value" HeaderStyle-HorizontalAlign="Left" ShowHeader="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblValue" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Item_Code") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="seqno" HeaderStyle-HorizontalAlign="Left" ShowHeader="true">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="seqno" Value='<%#  DataBinder.Eval(Container.DataItem, "ITS_SEQ_NO") %>' />
                                        <asp:HiddenField runat="server" ID="itemline" Value='<%#  DataBinder.Eval(Container.DataItem, "ITS_ITM_LINE") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Right" />
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
                        </asp:GridView>--%>
                    </asp:Panel>
                </div>
            </div>
            <div style="float: left; height: 8px; width: 100%;">
            </div>
        </asp:Panel>
    </div>
    <div class="commonPageCss" style="float: left; width: 100%">
        <div style="float: left; width: 100%;">
            <div style="float: left; width: 99%; height: 22px; text-align: right; background-color: #1E4A9F">
                <div style="float: left;">
                    <asp:Label ID="lblDispalyInfor" runat="server" Text="Back date allow for" CssClass="Label"
                        ForeColor="Yellow"></asp:Label>
                </div>
                <div style="float: right;">
                    <asp:Button Text="Close" ID="btnClose" runat="server" CssClass="Button" Width="75px"
                        OnClick="btnClose_Click" />
                    &nbsp;
                    <asp:Button Text="Clear" ID="btnClear" runat="server" CssClass="Button" Width="75px"
                        OnClick="btnClear_Click" />
                    &nbsp;
                    <asp:Button Text="Update" ID="btnUpdate" runat="server" CssClass="Button" Width="75px"
                        OnClick="btnUpdate_Click" />
                    &nbsp;
                    <asp:Button Text="New" ID="btnNew" runat="server" CssClass="Button" Width="75px"
                        OnClick="btnNew_Click" />
                    &nbsp;
                </div>
            </div>
        </div>
        <div style="float: left; width: .5%">
            &nbsp;
        </div>
        <asp:UpdatePanel ID="pnlChkBoxes" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div style="float: left; width: 18%">
                    <asp:Panel ID="pnlCheckBoxes" runat="server" GroupingText="RCC Stages" Font-Size="11px"
                        Font-Names="Tahoma">
                        <div style="float: left; height: 23px; width: 100%;">
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 98%;">
                                <asp:CheckBox ID="chkRaise" runat="server" Text="RCC Raised" Enabled="false" />
                            </div>
                            <div style="float: left; width: 1%;">
                                &nbsp;
                            </div>
                        </div>
                        <div style="float: left; height: 23px; width: 100%;">
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 98%;">
                                <asp:CheckBox ID="chkOpen" runat="server" Text="Job Opened" Enabled="false" />
                            </div>
                            <div style="float: left; width: 1%;">
                                &nbsp;
                            </div>
                        </div>
                        <div style="float: left; height: 23px; width: 100%;">
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 98%;">
                                <asp:CheckBox ID="chkRepair" runat="server" Text="Repair/Return Status" Enabled="false" />
                            </div>
                            <div style="float: left; width: 1%;">
                                &nbsp;
                            </div>
                        </div>
                        <div style="float: left; height: 23px; width: 100%;">
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 98%;">
                                <asp:CheckBox ID="chkComplete" runat="server" Text="Completed" Enabled="false" />
                            </div>
                            <div style="float: left; width: 1%;">
                                &nbsp;
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="float: left; width: 1%">
            &nbsp;
        </div>
        <div style="float: left; width: 80%">
            <asp:Panel ID="pnlHeader" runat="server" GroupingText="RCC Details" Font-Size="11px"
                Font-Names="Tahoma">
                <asp:Panel ID="pnlhdr" runat="server" Height="100px">
                    <div style="float: left; height: 6px; width: 100%;">
                    </div>
                    <div style="float: left; width: 100%;">
                        <div style="float: left; width: 1%;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 10%">
                            RCC Number
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 36%">
                            <asp:DropDownList ID="ddlRccNo" runat="server" AutoPostBack="true" Width="250px"
                                AppendDataBoundItems="true" OnSelectedIndexChanged="ddlRccNo_SelectedIndexChanged"
                                CssClass="ComboBox" />
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 15%">
                            RCC Date
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 24%">
                            <asp:TextBox ID="txtRccDate" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                            <asp:Image ID="imgDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="imgDate"
                                TargetControlID="txtRccDate" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                        </div>
                    </div>
                    <div style="float: left; height: 6px; width: 100%;">
                    </div>
                    <div style="float: left; width: 100%;">
                        <div style="float: left; width: 1%;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 10%">
                            RCC Type
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 36%">
                            <asp:DropDownList ID="ddlRccType" runat="server" Width="175px" AppendDataBoundItems="true"
                                AutoPostBack="true" CssClass="ComboBox" OnSelectedIndexChanged="ddlRccType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 15%">
                            Collection Method
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 24%">
                            <asp:DropDownList ID="ddlColMethod" runat="server" Width="200px" AppendDataBoundItems="true"
                                CssClass="ComboBox" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div style="float: left; height: 6px; width: 100%;">
                    </div>
                    <div style="float: left; width: 100%;">
                        <div style="float: left; width: 1%;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 10%">
                            RCC Sub Type
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 36%">
                            <asp:DropDownList ID="ddlRccSubType" runat="server" Width="175px" AppendDataBoundItems="true"
                                CssClass="ComboBox" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 15%">
                            Service Agent
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 24%">
                            <asp:DropDownList ID="ddlAgent" runat="server" Width="175px" AppendDataBoundItems="true"
                                CssClass="ComboBox" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div style="float: left; height: 6px; width: 100%;">
                    </div>
                    <div style="float: left; width: 100%;">
                        <div style="float: left; width: 1%;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 10%">
                            <asp:CheckBox ID="chkManual" runat="server" Text="Is Manual" OnCheckedChanged="chkManual_CheckedChanged"
                                AutoPostBack="true" />
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;
                        </div>
                        <div style="float: left; width: 36%">
                            <asp:TextBox ID="txtManualRef" runat="server" CssClass="TextBox" MaxLength="30" Enabled="false"
                                Width="175"></asp:TextBox>
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 10%">
                            Product Search
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 4%">
                            <asp:ImageButton ID="ImgBtnProductSrch" runat="server" ImageUrl="~/Images/icon_search.png"
                                Width="16px" />
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 15%">
                            <asp:CheckBox ID="chkOthLoc" runat="server" Text="Other SR Sale" />
                        </div>
                    </div>
                    <div style="float: left; height: 6px; width: 100%;">
                    </div>
                    <div style="display: none;">
                        <asp:Button ID="btnManual" runat="server" OnClick="IsValidManualNo" />
                    </div>
                </asp:Panel>
                <%-- </ContentTemplate>--%>
                <%--   </asp:UpdatePanel>--%>
            </asp:Panel>
        </div>
        <div style="float: left; width: .5%">
            &nbsp;
        </div>
    </div>
    <div style="float: left; width: 100%">
        <div style="float: left; width: .5%">
            &nbsp;
        </div>
        <div style="float: left; width: 68%">
            <asp:UpdatePanel ID="pnl_CustDet" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="pnlCustDet" runat="server" GroupingText="Customer Details" Font-Size="11px"
                        Font-Names="Tahoma">
                        <div style="float: left; height: 23px; width: 100%;">
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 17%;">
                                Invoice No :</div>
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 35%;">
                                <asp:TextBox ID="txtInvNo" runat="server" CssClass="TextBox" Width="175" ClientIDMode="Static"
                                    Enabled="false"></asp:TextBox>
                            </div>
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 12%;">
                                Account No :</div>
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 23%;">
                                <asp:TextBox ID="txtAccNo" runat="server" CssClass="TextBox" Width="200" Enabled="false"></asp:TextBox>
                            </div>
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                        </div>
                        <div style="float: left; height: 23px; width: 100%;">
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 17%;">
                                Customer Name :</div>
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 35%;">
                                <asp:TextBox ID="txtCusName" runat="server" CssClass="TextBox" Width="200" Enabled="false"></asp:TextBox>
                            </div>
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 12%;">
                                Invoice Date :</div>
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 23%;">
                                <asp:TextBox ID="txtInvDate" runat="server" CssClass="TextBox" Width="200" Enabled="false"></asp:TextBox>
                            </div>
                            <div style="float: left; width: 1%;">
                                &nbsp;
                            </div>
                        </div>
                        <div style="float: left; height: 23px; width: 100%;">
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 17%;">
                                Customer Address :</div>
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 35%;">
                                <asp:TextBox ID="txtAddr" runat="server" TextMode="MultiLine" Width="200px" Height="25px"
                                    Enabled="false" CssClass="TextBox"></asp:TextBox>
                            </div>
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 12%;">
                                Tele No :</div>
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 23%;">
                                <asp:TextBox ID="txtTelNo" runat="server" CssClass="TextBox" Width="200" MaxLength="30"
                                    Enabled="false" onKeyPress="return numbersonly(event,false)"></asp:TextBox>
                            </div>
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div style="float: left; width: 1%">
            &nbsp;
        </div>
        <div style="float: left; width: 30%">
            <asp:UpdatePanel ID="pnl_Job" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="pnlJob" runat="server" GroupingText="Job Details" Font-Size="11px"
                        Font-Names="Tahoma">
                        <div style="float: left; height: 23px; width: 100%;">
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 30%;">
                                Job Number 1 :</div>
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 50%;">
                                <asp:TextBox ID="txtJob1" runat="server" CssClass="TextBox" Width="165" MaxLength="30"></asp:TextBox>
                            </div>
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                        </div>
                        <div style="float: left; height: 23px; width: 100%;">
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 30%;">
                                Job Number 2 :</div>
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 50%;">
                                <asp:TextBox ID="txtJob2" runat="server" CssClass="TextBox" Width="165"></asp:TextBox>
                            </div>
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                        </div>
                        <div style="float: left; height: 23px; width: 100%;">
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 30%;">
                                Order No :</div>
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 50%;">
                                <asp:TextBox ID="txtOrdNo" runat="server" CssClass="TextBox" Width="165"></asp:TextBox>
                            </div>
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                        </div>
                        <div style="float: left; height: 23px; width: 100%;">
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 30%;">
                                Dispatch No :</div>
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 50%;">
                                <asp:TextBox ID="txtDispatchNo" runat="server" CssClass="TextBox" Width="165"></asp:TextBox>
                            </div>
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="commonPageCss" style="float: left; width: 100%">
        <div style="float: left; width: .5%">
            &nbsp;
        </div>
        <div style="float: left; width: 98%">
            <%--<asp:Panel ID="pnlDet" runat="server" Font-Size="11px" Font-Names="Tahoma">--%>
            <cc1:TabContainer ID="tbItem" runat="server" Height="140px">
                <cc1:TabPanel ID="tbPanelItem" runat="server" HeaderText="--Item Details--">
                    <ContentTemplate>
                        <asp:Panel ID="pnlItem" runat="server" ScrollBars="Auto" Font-Size="11px" Font-Names="Tahoma">
                            <asp:UpdatePanel ID="pnl_Item" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div style="float: left; height: 23px; width: 100%;">
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 8%;">
                                            Item Code :</div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 23%;">
                                            <asp:TextBox ID="txtItem" runat="server" CssClass="TextBox" Width="175" ClientIDMode="Static"
                                                Enabled="false"></asp:TextBox>
                                            <%--<asp:ImageButton ID="imgbtnItem" runat="server" ImageUrl="~/Images/icon_search.png"
                                                OnClick="imgbtnItem_Click" />--%>
                                        </div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 8%;">
                                            Description :</div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 23%;">
                                            <asp:TextBox ID="txtItemDesn" runat="server" CssClass="TextBox" Width="200" ClientIDMode="Static"
                                                Enabled="False"></asp:TextBox>
                                        </div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 8%;">
                                            Model :</div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 23%;">
                                            <asp:TextBox ID="txtModel" runat="server" CssClass="TextBox" Width="200" ClientIDMode="Static"
                                                Enabled="False"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; height: 23px; width: 100%;">
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 8%;">
                                            Serial No :</div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 23%;">
                                            <asp:TextBox ID="txtSerial" runat="server" Width="200px" CssClass="TextBox" ClientIDMode="Static"
                                                Enabled="False">
                                            </asp:TextBox>
                                        </div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 8%;">
                                            Warranty No :</div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 23%;">
                                            <asp:TextBox ID="txtWarranty" runat="server" Width="200px" CssClass="TextBox" ClientIDMode="Static"
                                                Enabled="False">
                                            </asp:TextBox>
                                        </div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 8%;">
                                            Condition :</div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 23%;">
                                            <asp:DropDownList ID="ddlCond" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                                CssClass="ComboBox" Width="200">
                                            </asp:DropDownList>
                                        </div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                    </div>
                                    <div style="float: left; height: 23px; width: 100%;">
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 8%;">
                                            Defect Type :</div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 23%;">
                                            <asp:DropDownList ID="ddlDefect" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                                Width="200px" CssClass="ComboBox">
                                            </asp:DropDownList>
                                        </div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 8%;">
                                            Available Accessories :</div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 23%;">
                                            <asp:DropDownList ID="ddlAcc" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                                CssClass="ComboBox" Width="200">
                                            </asp:DropDownList>
                                        </div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 8%;">
                                            Inspected By :</div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 23%;">
                                            <asp:TextBox ID="txtInsp" runat="server" Width="200px" CssClass="TextBox" MaxLength="100">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; height: 23px; width: 100%;">
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 8%;">
                                            Easy Location :</div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 23%;">
                                            <asp:TextBox ID="txtEasyLoc" runat="server" Width="200px" CssClass="TextBox" MaxLength="50">
                                            </asp:TextBox>
                                        </div>
                                        <div style="float: left; width: 8%;">
                                            <asp:TextBox ID="txtloc" runat="server" CssClass="TextBox" Width="30" Visible="false"></asp:TextBox>
                                        </div>
                                        <div style="float: left; width: 8%;">
                                            <asp:TextBox ID="txtcomp" runat="server" CssClass="TextBox" Width="30" Visible="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; height: 23px; width: 100%;">
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 8%;">
                                            Remarks :</div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="txtRem" runat="server" Width="500px" CssClass="TextBox" MaxLength="100">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="tbPanelRepair" runat="server" HeaderText="--Repair/Return Status--">
                    <ContentTemplate>
                        <asp:Panel ID="pnlRepair" runat="server" ScrollBars="Auto" Font-Size="11px" Font-Names="Tahoma">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div style="float: left; height: 23px; width: 100%;">
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 15%;">
                                            Repaired :</div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 23%;">
                                            <asp:CheckBox ID="chkRepaired" runat="server" />
                                        </div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 13%;">
                                            Reason :</div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 23%;">
                                            <asp:DropDownList ID="ddlReason" runat="server" Width="175px" AppendDataBoundItems="true"
                                                AutoPostBack="true" CssClass="ComboBox">
                                            </asp:DropDownList>
                                        </div>
                                        <div style="float: left; width: 21%;">
                                            &nbsp;</div>
                                    </div>
                                    <div style="float: left; height: 23px; width: 100%;">
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 15%;">
                                            Return Condition :</div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 23%;">
                                            <asp:DropDownList ID="ddlRetCond" runat="server" Width="175px" AppendDataBoundItems="true"
                                                AutoPostBack="true" CssClass="ComboBox">
                                            </asp:DropDownList>
                                        </div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 13%;">
                                            Returned Date :</div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 23%;">
                                            <asp:TextBox ID="txtRetDate" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtRetDate"
                                                Format="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
                                        </div>
                                        <div style="float: left; width: 21%;">
                                            &nbsp;</div>
                                    </div>
                                    <div style="float: left; height: 23px; width: 100%;">
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 15%;">
                                            Remarks :</div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 56%;">
                                            <asp:TextBox ID="txtRepairRem" runat="server" CssClass="TextBox" Width="500"></asp:TextBox>
                                        </div>
                                        <div style="float: left; width: 26%;">
                                            &nbsp;</div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="tbPanelComplete" runat="server" HeaderText="--Completion--">
                    <ContentTemplate>
                        <asp:Panel ID="pnlComplete" runat="server" ScrollBars="Auto" Font-Size="11px" Font-Names="Tahoma">
                            <asp:UpdatePanel ID="pnl_Complete" runat="server">
                                <ContentTemplate>
                                    <div style="float: left; height: 23px; width: 100%;">
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 15%;">
                                            Closed Date..................</div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 23%;">
                                            <asp:TextBox ID="txtCloseDate" runat="server" Enabled="false" CssClass="TextBox"></asp:TextBox>
                                            <%--<cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtCloseDate"
                                                Format="dd/MM/yyyy">
                                            </cc1:CalendarExtender>--%>
                                        </div>
                                        <div style="float: left; width: 59%;">
                                            &nbsp;</div>
                                    </div>
                                    <div style="float: left; height: 23px; width: 100%;">
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 15%;">
                                            Closure Type.................</div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 23%;">
                                            <asp:DropDownList ID="ddlClosureType" runat="server" Width="175px" AppendDataBoundItems="true"
                                                AutoPostBack="true" CssClass="ComboBox">
                                            </asp:DropDownList>
                                        </div>
                                        <div style="float: left; width: 59%;">
                                            &nbsp;</div>
                                    </div>
                                    <div style="float: left; height: 23px; width: 100%;">
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 15%;">
                                            Remarks.......................</div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 56%;">
                                            <asp:TextBox ID="txtCompleteRem" runat="server" CssClass="TextBox" Width="500"></asp:TextBox>
                                        </div>
                                        <div style="float: left; width: 26%;">
                                            &nbsp;</div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
            <%--</asp:Panel>--%>
        </div>
        <div style="float: left; width: 1%">
            &nbsp;
        </div>
    </div>
</asp:Content>
