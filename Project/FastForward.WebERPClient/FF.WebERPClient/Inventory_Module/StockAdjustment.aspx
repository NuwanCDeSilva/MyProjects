<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="StockAdjustment.aspx.cs" Inherits="FF.WebERPClient.WebForm2" %>

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

        function LoadPopUp() {
            document.getElementById('<%=HiddenFieldCusCrePopUpStats.ClientID %>').value = "1";
        }

        function ClosePopUp() {
            document.getElementById('<%=HiddenFieldCusCrePopUpStats.ClientID %>').value = "0";
        }

        function SelectAll(Id) {
            var myform = document.forms[0];
            var len = myform.elements.length;
            document.getElementById(Id).checked == true ? document.getElementById(Id).checked = false : document.getElementById(Id).checked = true; for (var i = 0; i < len; i++) { if (myform.elements[i].type == 'checkbox') { if (myform.elements[i].checked) { myform.elements[i].checked = false; } else { myform.elements[i].checked = true; } } }
        }
    </script>
    <style type="text/css">
        .style1
        {
            font-family: Verdana;
            font-size: 11px;
        }
        .style3
        {
            font-family: Verdana;
            font-size: 11px;
        }
        .style4
        {
            width: 98%;
        }
        .style6
        {
            width: 217px;
        }
        .style10
        {
            width: 115px;
        }
        .style11
        {
            width: 80px;
            height: 25px;
        }
        .style12
        {
            width: 217px;
            height: 25px;
        }
        .style13
        {
            width: 6px;
            height: 25px;
        }
        .style14
        {
            width: 115px;
            height: 25px;
        }
        .style15
        {
            height: 25px;
        }
        .style16
        {
            color: black;
            font-size: 11px;
            font-family: Verdana;
        }
        .style17
        {
            width: 139px;
        }
        .style18
        {
            width: 161px;
        }
        .style19
        {
            width: 113px;
        }
        .style20
        {
            width: 188px;
        }
        .style21
        {
            width: 51px;
        }
        .style22
        {
            width: 6px;
        }
        .style23
        {
            width: 89px;
        }
        .style24
        {
            width: 80px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <br class="style1" />
        <div style="width: 868px; margin-left: 38px; height: 30px;">
            <asp:Panel ID="Panel2" runat="server" Height="52px" Width="877px" BorderStyle="None"
                BorderWidth="0px" BorderColor="White" GroupingText="  ">
                &nbsp;&nbsp;&nbsp;&nbsp;<table class="style4">
                    <tr>
                        <td class="style17">
                            <span class="style16"><span class="style1">Reasent Scan Batches :</span></span>
                        </td>
                        <td class="style18">
                            <asp:DropDownList ID="ddlScanBatches" runat="server" AutoPostBack="True" CssClass="style3"
                                Height="20px" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" Width="115px">
                            </asp:DropDownList>
                            <asp:Label ID="lblBatchSeq" runat="server" CssClass="style16" Text="Label" Visible="False"></asp:Label>
                        </td>
                        <td class="style19">
                            <asp:Label ID="Label19" runat="server" CssClass="style16" Font-Bold="False" ForeColor="Black"
                                Text="Adjustment Type :"></asp:Label>
                        </td>
                        <td class="style20">
                            <asp:DropDownList ID="ddlAdjType_" runat="server" AutoPostBack="True" CssClass="style3"
                                Height="18px" OnSelectedIndexChanged="ddlAdjType__SelectedIndexChanged" Width="98px">
                                <asp:ListItem>+ADJ</asp:ListItem>
                                <asp:ListItem>- ADJ</asp:ListItem>
                                <asp:ListItem Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="lblDirect" runat="server" CssClass="style16" Text="Label" Visible="False"></asp:Label>
                            <asp:Button ID="btnSaveAdj" runat="server" CssClass="Button" Font-Bold="True" Height="17px"
                                OnClick="btnSaveAdj_Click" Text="Save This Adjustment" ValidationGroup="newScan"
                                Visible="False" Width="48px" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td class="style21">
                            &nbsp;
                        </td>
                        <td style="text-align: right">
                            <asp:Button ID="btnSerialScan" runat="server" CssClass="Button" Font-Bold="True"
                                OnClick="btnSerialScan_Click" Style="margin-left: 0px; font-weight: 400; text-align: right;"
                                Text="New Serial Scan" Width="108px" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <div style="height: 138px; margin-top: 0px">
            <br class="style1" />
            <asp:Panel ID="Panel1" runat="server" BackColor="White" Style="margin-left: 33px;
                margin-top: 9px;" Width="882px" CssClass="style1" GroupingText="Save Adjustment">
                <div style="text-align: right; width: 863px;">
                    <asp:Button ID="btnFinalSave0" runat="server" CssClass="Button" OnClick="btnFinalSave_Click"
                        Text="Save Adjustment" ValidationGroup="newScan" />
                    <asp:Button ID="ButtonAddCost" runat="server" CssClass="Button" Text="Add Cost" Visible="false" OnClientClick="LoadPopUp()" />
                    &nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="Button" OnClick="btnCancel_Click"
                        Text="Clear" Width="83px" />
                    &nbsp;&nbsp;
                    <br />
                </div>
                <div>
                    <table class="style4">
                        <tr>
                            <td class="style24">
                                <asp:Label ID="Label16" runat="server" Font-Bold="True" Text="Date"></asp:Label>
                            </td>
                            <td class="style6">
                                <asp:TextBox ID="txtDate_" runat="server" CssClass="style1" Enabled="False" Width="142px"></asp:TextBox>
                                <asp:CalendarExtender ID="txtDate__CalendarExtender" runat="server" Animated="true"
                                    EnabledOnClient="true" Format="dd/MM/yyyy" PopupPosition="BottomLeft" TargetControlID="txtDate_">
                                </asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtDate_"
                                    ErrorMessage="*" ForeColor="Red" ValidationGroup="newScan"></asp:RequiredFieldValidator>
                            </td>
                            <td class="style22">
                                &nbsp;
                            </td>
                            <td class="style10">
                                <asp:Label ID="Label21" runat="server" Text="Adjustment Sub Type"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlAdjSubTyepe" runat="server" AutoPostBack="True" CssClass="style3"
                                    Height="23px" OnSelectedIndexChanged="ddlAdjSubTyepe_SelectedIndexChanged" Width="99px">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="lblSubTpDesc" runat="server"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="style11">
                                <asp:Label ID="Label3" runat="server" Text="Manual Ref. No "></asp:Label>
                            </td>
                            <td class="style12">
                                <asp:TextBox ID="txtManualRefNo" runat="server" CssClass="style1" Width="142px" MaxLength="30"></asp:TextBox>
                            </td>
                            <td class="style13">
                            </td>
                            <td class="style14">
                                <asp:Label ID="Label22" runat="server" Text="Adjustment Based" Visible="False"></asp:Label>
                            </td>
                            <td class="style15">
                                <asp:DropDownList ID="ddlAdjBased" runat="server" CssClass="style3" Height="23px"
                                    Visible="False" Width="99px">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem>PHYSICAL</asp:ListItem>
                                    <asp:ListItem>DOCUMENT</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="style15">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="style24">
                                <asp:Label ID="Label18" runat="server" Text="Remarks"></asp:Label>
                            </td>
                            <td class="style6">
                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="style1" MaxLength="30" Width="255px"></asp:TextBox>
                            </td>
                            <td class="style22">
                                &nbsp;
                            </td>
                            <td class="style10">
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lblNewScanDirect" runat="server" CssClass="style16" Visible="False"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <%--    <asp:ModalPopupExtender ID="ModalPopupEx" runat="server" CancelControlID="btnCancel" PopupControlID="Panel1" TargetControlID="ddlAdjType_">
                </asp:ModalPopupExtender> --%>
            <%--<asp:ModalPopupExtender ID="ModalPopupEx" runat="server" 
                    CancelControlID="btnCancel" 
                    PopupControlID="Panel1" TargetControlID="btnSaveAdj">
                </asp:ModalPopupExtender>--%>
        </div>
        <div style="height: 9px">
            &nbsp;&nbsp;</div>
        <div>
            <asp:Panel ID="PanelGrid" runat="server" Style="margin-left: 29px; text-align: center;"
                Width="864px">
                <asp:GridView ID="gridShowAdjustedData" runat="server" OnSelectedIndexChanged="GridView2_SelectedIndexChanged"
                    CellPadding="4" ForeColor="#333333" AutoGenerateColumns="False" Width="882px"
                    AllowPaging="True" OnPageIndexChanging="gridShowAdjustedData_PageIndexChanging"
                    CssClass="style1" PageSize="12">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="Tus_bin" HeaderText="Bin Code" />
                        <asp:BoundField DataField="Tus_itm_cd" HeaderText="Item Code" />
                        <asp:BoundField DataField="Tus_itm_desc" HeaderText="Item Description" />
                        <asp:BoundField DataField="Tus_itm_model" HeaderText="Item Model" />
                        <asp:BoundField DataField="Tus_itm_stus" HeaderText="Item Status" />
                        <asp:BoundField DataField="Tus_qty" HeaderText="Qty" />
                        <asp:BoundField DataField="Tus_ser_1" HeaderText="Serial No" />
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
            <div>
                <span class="style1">
                    <asp:Label ID="Label11" runat="server" Text="rows inserted:" Visible="False"></asp:Label>
                    <asp:Label ID="lblrowsInserted" runat="server" Text="Label" Visible="False"></asp:Label>
            </div>
        </div>
        <br />
    </div>
    <div>
        <br />
        <br />
        <br />
        <br />
        </span>
    </div>
    <%-- ADDED BY SACHITH--%>
    <%--COST ADD POP UP SCREEN--%>
    <asp:HiddenField ID="HiddenFieldCusCrePopUpStats" runat="server" Value="0" />
    <asp:ModalPopupExtender ID="ModalPopupExtender1" TargetControlID="ButtonAddCost"
        runat="server" ClientIDMode="Static" PopupControlID="pnlShowCost" BackgroundCssClass="modalBackground"
        CancelControlID="ImgAdd" PopupDragHandleControlID="div3">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlShowCost" runat="server" Width="650px" CssClass="ModalWindow">
        <div class="popUpHeader" id="div1" runat="server">
            <div style="float: left; width: 80%" runat="server" id="div3">
                Add Item Unit Cost</div>
            <div style="float: left; width: 20%; text-align: right">
                <asp:ImageButton ID="ImgAdd" runat="server" ImageUrl="~/Images/0032-01-16x16x16.ICO" OnClientClick="ClosePopUp()" />&nbsp;</div>
        </div>
        <asp:Panel ID="PanelCCre" runat="server">
            <div style="float: left; width: 100%; height: 10px; color: Black;">
                &nbsp;
            </div>
            <div style="float: left; width: 1%; color: Black;">
                &nbsp;
            </div>
            <asp:Panel ID="Panel3" runat="server" GroupingText=" ">
                <%-- 1 row--%>
                <div style="float: left; width: 100%; color: Black;">
                    <div style="float: left; width: 30%;">
                        Item Code
                    </div>
                    <div style="float: left; width: 70%;">
                        <asp:DropDownList ID="DropDownListItemCode" CssClass="ComboBox" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="DropDownListItemCode_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <%--2 row--%>
                <div style="float: left; width: 100%; color: Black; padding-top: 5px;">
                    <div style="float: left; width: 30%;">
                        Item Status
                    </div>
                    <div style="float: left; width: 70%;">
                        <asp:DropDownList ID="DropDownListStatus" runat="server" CssClass="ComboBox" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="DropDownListStatus_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <%--grid view--%>
                <div style="float: left; width: 100%; color: Black; padding-top: 5px;">
                    <asp:GridView ID="GridViewPopUp" runat="server" Width="100%" EmptyDataText="No data found"
                        ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                        CssClass="GridView" GridLines="None">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderStyle-Width="20px">
                            <HeaderTemplate>
                            <asp:CheckBox ID="CheckBoxGridHdrSelect" runat="server" Checked="true" onclick="SelectAll(this.id)" />
                            </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBoxGridSelect" runat="server" Checked="true" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="20px" Height="15px" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Itm Code" DataField="TUS_ITM_CD" />
                            <asp:BoundField HeaderText="Itm Status" DataField="TUS_ITM_STUS" />
                            <asp:BoundField HeaderText="Ser. 01" DataField="TUS_SER_1" />
                            <asp:BoundField HeaderText="Ser. 02" DataField="TUS_SER_2" />
                            <asp:BoundField HeaderText="Ser. Id" DataField="TUS_SER_ID" />
                            <asp:BoundField HeaderText="Unit Cost" DataField="TUS_UNIT_COST" />
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
                <%--cost--%>
                <div style="float: left; width: 100%; color: Black; padding-top: 5px;">
                    <div style="float: left; width: 30%;">
                        Unit Cost
                    </div>
                    <div style="float: left; width: 70%;">
                        <asp:TextBox ID="TextBoxUnitCost" runat="server" CssClass="TextBox" onKeyPress="return numbersonly(event,true)"></asp:TextBox>
                    </div>
                </div>
                <div style="float: left; width: 75%; color: Black; text-align: right;">
                    <asp:Button ID="ButtonApply" Text="Apply" CssClass="Button" runat="server" OnClick="ButtonApply_Click" />
                </div>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
    <%--END--%>
</asp:Content>
