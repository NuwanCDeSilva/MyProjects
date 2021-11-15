<%@ Page Title="Inventory Tracker" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="InventoryTracker.aspx.cs" Inherits="FF.WebERPClient.Enquiry_Modules.Inventory.InventoryTracker" %>

<%@ Register Src="../../UserControls/uc_CompanySearch.ascx" TagName="uc_CompanySearch"
    TagPrefix="AC" %>
<%@ Register Src="../../UserControls/uc_ItemSerialView.ascx" TagName="uc_ItemSerial"
    TagPrefix="IS" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <%--   javascript --%>
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


        //-------------------------------------GetItemData()------------------------------------------------------//
        function GetItemData() {
            var itemCode = document.getElementById('<%=TextBoxCode.ClientID%>').value;
            itemCode = itemCode.toUpperCase();
            if (itemCode != "") {
                // alert();
                FF.WebERPClient.LocalWebServices.CommonSearchWebServive.GetAllItemDetailsByItemCode(itemCode, onGetItemDataPass, onGetItemDataFail);
            }
            else {
                // alert("Invalid item code");
                document.getElementById('<%=TextBoxModel.ClientID%>').value = "";
            }
        }
        //SucceededCallback method.
        function onGetItemDataPass(result) {
            if (result != null) {

                document.getElementById('<%=TextBoxModel.ClientID%>').value = result.Mi_model;
            }
            else {
                alert("Invalid item code");
                document.getElementById('<%=TextBoxModel.ClientID%>').value = "";

            }

        }
        //FailedCallback method.
        function onGetItemDataFail(error) {
            alert("Invalid item code");
            document.getElementById('<%=TextBoxModel.ClientID%>').value = "";
        }



        function pageLoad(sender, args) {

            if ($find("collapsibleBehavior") != null) {

                $find("collapsibleBehavior").add_expandComplete(expandHandler);

                $find("collapsibleBehavior").add_collapseComplete(collapseHandler);
            }
        }

        function expandHandler(sender, args) {

            document.getElementById('<%=TextBoxCompany.ClientID%>').disabled = true;
            document.getElementById('<%=ImageButtonCompany.ClientID%>').disabled = true;
            document.getElementById('<%=TextBoxLoc.ClientID%>').disabled = true;
            document.getElementById('<%=ImageButtonLocation.ClientID%>').disabled = true;
            document.getElementById('<%=Panel2.ClientID%>').style.height = "170px";
        }

        function collapseHandler(sender, args) {
            if (document.getElementById('<%=hdnUserLevel.ClientID%>').value == "1") {
                document.getElementById('<%=ImageButtonCompany.ClientID%>').disabled = false;
                document.getElementById('<%=TextBoxCompany.ClientID%>').disabled = false;
            }
            document.getElementById('<%=TextBoxLoc.ClientID%>').disabled = false;
            document.getElementById('<%=ImageButtonLocation.ClientID%>').disabled = false;
            document.getElementById('<%=Panel2.ClientID%>').style.height = "300px";

        }

        function ClearControls(id) {

            if (id == '<%=TextBoxCompany.ClientID %>') {
                document.getElementById('<%=TextBoxLoc.ClientID%>').value = "";
            }
            if (id == '<%=TextBoxMain.ClientID %>') {
                document.getElementById('<%=TextBoxSub.ClientID%>').value = "";
                document.getElementById('<%=TextBoxRange.ClientID%>').value = "";
            }
            if (id == '<%=TextBoxSub.ClientID %>') {
                document.getElementById('<%=TextBoxRange.ClientID%>').value = "";
            }
        }

    </script>
    <link href="../../MainStyleSheet.css" rel="stylesheet" type="text/css" />
    <style>
        .cpBody
        {
            height: 0px;
            overflow: hidden;
        }
        .ModalWindowLocal
        {
            border: solid 1px #c0c0c0;
            background: #f0f0f0;
            padding: 0px 0px 0px 0px;
            position: absolute;
            top: -1000px;
            width: 80%;
        }
 
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updtMainPnl" runat="server">
        <ContentTemplate>
            <%--Whole Page--%>
            <asp:HiddenField Value="0" ID="hdnUserLevel" runat="server" />
            <div style="float: left; width: 100%; color: black;">
                <div style="height: 22px; text-align: right;" class="PanelHeader">
                    <asp:Button ID="ButtonView" runat="server" Text="View" CssClass="Button" OnClick="ButtonView_Click" />
                    <asp:Button ID="ButtonClear" runat="server" Text="Clear" CssClass="Button" OnClick="ButtonClear_Click" />
                    <asp:Button ID="ButtonClose" runat="server" Text="Close" CssClass="Button" OnClick="ButtonClose_Click" />
                </div>
                <div style="float: left; width: 100%;">
                    &nbsp;</div>
                <div runat="server" id="DivCompanySearch" visible="true" style="float: left; width: 100%;
                    color: black;">
                    <div style="background-color: #1E4A9F; color: White; width: 98%; float: left; height: 18px;">
                        Advanced Search
                    </div>
                    <%-- Collaps Image - Pending Documents --%>
                    <div style="float: left;">
                        <asp:Image runat="server" ID="Image2" ImageUrl="~/Images/16 X 16 DownArrow.jpg" ImageAlign="Right" /></div>
                    <div style="width: 100%; float: left; color: Black;">
                        <asp:Panel ID="PanelCompanyDetails" runat="server" CssClass="cpBody">
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 99%;">
                                <AC:uc_CompanySearch ID="Uc_CompanySearch1" runat="server" ClientIDMode="Static" />
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" TargetControlID="PanelCompanyDetails"
                    CollapsedSize="0" ExpandControlID="Image2" CollapseControlID="Image2" Collapsed="true"
                    AutoCollapse="False" AutoExpand="False" ScrollContents="false" ExpandDirection="Vertical"
                    ImageControlID="Image2" ExpandedImage="~/Images/16 X 16 UpArrow.jpg" CollapsedImage="~/Images/16 X 16 DownArrow.jpg"
                    BehaviorID="collapsibleBehavior">
                </asp:CollapsiblePanelExtender>
                <div style="float: left; width: 100%;">
                    &nbsp;</div>
                <div class="PanelHeader">
                    Item Details
                </div>
                <div style="float: left; width: 100%;">
                    <%--0 Row--%>
                    <div style="float: left; width: 100%;">
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 14%;">
                            Item Code
                        </div>
                        <div style="float: left; width: 18%;">
                            <asp:TextBox ID="TextBoxCode" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                            <asp:ImageButton ID="imgItemSearch" runat="server" ImageUrl="~/Images/icon_search.png"
                                OnClick="imgItemSearch_Click" />
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 14%;">
                            Main Category
                        </div>
                        <div style="float: left; width: 18%;">
                            <asp:TextBox ID="TextBoxMain" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                            <asp:ImageButton ID="ImageButtonMain" runat="server" ImageUrl="~/Images/icon_search.png"
                                OnClick="ImageButton1_Click" />
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div id="DivCompany" runat="server" style="float: left; width: 14%;">
                            Company
                        </div>
                        <div runat="server" id="DivCompanyText" style="float: left; width: 18%;">
                            <asp:TextBox ID="TextBoxCompany" runat="server" CssClass="TextBox TextBoxUpper" AutoPostBack="True"></asp:TextBox>
                            <asp:ImageButton ID="ImageButtonCompany" runat="server" ImageUrl="~/Images/icon_search.png"
                                OnClick="ImageButtonCompany_Click" Style="width: 16px" />
                        </div>
                    </div>
                    
                    <%-- 1st Row --%>
                    <div style="float: left; width: 100%;">
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 14%;">
                            Model
                        </div>
                         <div style="float: left; width: 18%;">
                            <asp:TextBox ID="TextBoxModel" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 14%;">
                            Sub Category
                        </div>
                        <div style="float: left; width: 18%;">
                            <asp:TextBox ID="TextBoxSub" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                            <asp:ImageButton ID="ImageButtonSub" runat="server" ImageUrl="~/Images/icon_search.png"
                                OnClick="ImageButton2_Click" />
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 14%;">
                            Location
                        </div>
                        <div style="float: left; width: 18%;">
                            <asp:TextBox ID="TextBoxLoc" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                            <asp:ImageButton ID="ImageButtonLocation" runat="server" ImageUrl="~/Images/icon_search.png"
                                OnClick="ImageButtonLocation_Click" />
                        </div>
                    </div>

                    <%-- 2nd Row --%>
                    <div style="float: left; width: 100%;">
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 14%;">
                            Status
                        </div>
                        <div style="float: left; width: 18%;">
                            <asp:DropDownList ID="DDLStatus" runat="server" CssClass="ComboBox" AppendDataBoundItems="True">
                            </asp:DropDownList>
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 14%;">
                            Item Range
                        </div>
                        <div style="float: left; width: 18%;">
                            <asp:TextBox ID="TextBoxRange" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                            <asp:ImageButton ID="ImageButtonSub1" runat="server" ImageUrl="~/Images/icon_search.png"
                                OnClick="ImageButton3_Click" />
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 32%;">
                            &nbsp;
                        </div>
                    </div>
           
                </div>
                <div style="text-align: right; width: 90%; float: left;">
                    <asp:CheckBox ID="CheckBoxShowCostVal" runat="server" Text="Show Cost Value" OnCheckedChanged="CheckBoxShowCostVal_CheckedChanged"
                        AutoPostBack="true" Checked="true" />
                </div>
                <div style="background-color: #1E4A9F; width: 98%; float: left; height: 18px; color: White;">
                    Item Details
                </div>
                <div style="float: left; width: 1%;">
                    &nbsp;</div>
                <div style="float: left; width: 100%;">
                    <asp:Panel ID="Panel2" runat="server" ScrollBars="Both" Height="300px">
                        <asp:GridView ID="GridViewItemDetails" runat="server" Width="99%" EmptyDataText="No stock found"
                            ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="GridViewItemDetails_PageIndexChanging"
                            PageSize="10" CellPadding="4" ForeColor="#333333" GridLines="Both" CssClass="GridView"
                            OnSelectedIndexChanged="GridViewItemDetails_SelectedIndexChanged">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField HeaderText="Company" DataField="COMPANY" />
                                <asp:BoundField HeaderText="Location" DataField="LOC" />
                                <asp:BoundField HeaderText="Item Code" DataField="ITEM_CODE" />
                                <asp:BoundField HeaderText="Description" DataField="ITEM_DESC" ItemStyle-Width="250px" />
                                <asp:BoundField HeaderText="Model" DataField="MODEL" />
                                <asp:BoundField HeaderText="Status" DataField="ITEM_STATUS" />
                                <asp:BoundField HeaderText="Avail. Stock" DataField="FREE_QTY" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField HeaderText="Cost Value" DataField="COST_VAL" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString='<%$ appSettings:FormatToCurrency %>' />
                                <asp:BoundField HeaderText="Res. Stock" DataField="RES_QTY" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField HeaderText="Buffer Level" DataField="BUFFER_LEVEL" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField HeaderText="UOM" DataField="UOM" />
                                <asp:TemplateField HeaderStyle-Width="20px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgSelect" runat="server" CommandName="SELECT" ImageUrl="~/Images/download_arrow_icon.png"
                                            Height="15px" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="20px" Height="15px" />
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
                <div style="text-align: right; width: 90%; float: left;" runat="server" id="DivTotal"
                    visible="false">
                    Total Qty:
                    <asp:TextBox ID="TextBoxTqty" runat="server" Enabled="false"></asp:TextBox>
                </div>
                <asp:Panel ID="PanelPopUp" runat="server" CssClass="ModalWindowLocal">
                    <div class="popUpHeader" id="divpopHeader">
                        <div style="float: left; width: 80%">
                            Serial Header</div>
                        <div style="float: left; width: 20%; text-align: right">
                            <asp:ImageButton ID="imgbtnClose" runat="server" ImageUrl="~/Images/0032-01-16x16x16.ICO" />&nbsp;
                        </div>
                    </div>
                    <div style="float: left; width: 100%;">
                        <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Height="200px">
                            <IS:uc_ItemSerial ID="uc_ItemSerial1" runat="server" />
                        </asp:Panel>
                    </div>
                </asp:Panel>
                <asp:LinkButton ID="LinkButtonHidden" runat="server"></asp:LinkButton>
                <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="LinkButtonHidden"
                    ClientIDMode="Static" PopupControlID="PanelPopUp" BackgroundCssClass="modalBackground"
                    CancelControlID="imgbtnClose" PopupDragHandleControlID="divpopHeader">
                </asp:ModalPopupExtender>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
