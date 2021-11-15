<%@ Page Title="Customer Delivery Order" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="DeliveryOrder.aspx.cs" Inherits="FF.WebERPClient.Inventory_Module.DeliveryOrder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <%--javascript--%>
    <script type="text/javascript">
        function SelectAuto() {
            var text = document.getElementById('<%= txtPopupQty.ClientID %>');
            var val = text.value;
            var len;
            var myform = document.forms[0];
            if (val != null && val != "") {
                len = val;
            }
            else {
                len = 0;
                return;
            }

            var Elen = myform.elements.length;
            var counter = 0;

            for (var j = 0; j < Elen; j++) {
                if (myform.elements[j].checked) {
                    myform.elements[j].checked = false;
                }
            }
            //document.getElementById(Id).checked == true ? document.getElementById(Id).checked = false : document.getElementById(Id).checked = true;
            for (var i = 0; i < Elen; i++) {
                if (myform.elements[i].type == 'checkbox' && myform.elements[i].id != 'chkPopupSelectAll') {

                    if (myform.elements[i].checked) {
                        myform.elements[i].checked = false;
                    }
                    else {
                        myform.elements[i].checked = true;
                    }
                    counter++;
                    if (counter == len) {
                        return;
                    }

                }
            }
        }

        function SelectAll(Id) {
            var myform = document.forms[0];
            var len = myform.elements.length;
            document.getElementById(Id).checked == true ? document.getElementById(Id).checked = false : document.getElementById(Id).checked = true; for (var i = 0; i < len; i++) { if (myform.elements[i].type == 'checkbox') { if (myform.elements[i].checked) { myform.elements[i].checked = false; } else { myform.elements[i].checked = true; } } }
        }


        // Change number to your max length.
        function CheckCharacterCount(text, long) {
            var maxlength = new Number(long);
            if (document.getElementById(text).value.length > maxlength) {
                document.getElementById(text).value = document.getElementById(text).value.substring(0, maxlength);

            }
        }
    </script>
    <script type="text/javascript">
        function showPopup(url, option) {
            var name = 'newwindow' + option;

            newwindow = window.open(url, name, 'height=600,width=700,top=200,left=300,resizable,scrollbars=yes');
            if (window.focus) { newwindow.focus() }
        }
    </script>
    <script type="text/javascript">

        function popitup(url) {
            newwindow = window.open(url, 'name', 'height=600,width=800');
            if (window.focus) { newwindow.focus() }
            return false;
        }

    </script>
    <%--javascript--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="float: left; width: 100%;">
        <%-- Item Detail --%>
        <div style="float: left; width: 99%; height: 22px; text-align: right; background-color: #1E4A9F">
            <div style="float: left;">
                <asp:Label ID="lblDispalyInfor" runat="server" Text="Back date allow for" CssClass="Label"
                    ForeColor="Yellow"></asp:Label>
            </div>
            <div style="float: right;">
                <asp:Button ID="btnSaveDO" runat="server" Text="Save" Height="100%" Width="70px"
                    CssClass="Button" OnClick="btnSaveDO_Click" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" Height="100%" Width="70px"
                    CssClass="Button" OnClick="btnClear_Click" />
                <asp:Button ID="btnClose" runat="server" Text="Close" Height="100%" Width="70px"
                    CssClass="Button" OnClick="Close" />
            </div>
        </div>
        <%--Pending Invoice Grid--%>
        <div style="float: left; width: 100%; padding-top: 2px; padding-bottom: 2px;">
            <div style="height: 18px; background-color: #1E4A9F; color: #FFFFFF; width: 98%;
                float: left;">
                Pending Invoices</div>
            <div style="float: left;">
                <asp:Image runat="server" ID="Image1" ImageUrl="~/Images/16 X 16 DownArrow.jpg" ImageAlign="Right" /></div>
            <asp:CollapsiblePanelExtender ID="CPEInvoiceItem" runat="server" TargetControlID="pnlInvItem"
                CollapsedSize="0" ExpandedSize="160" Collapsed="false" ExpandControlID="Image1"
                CollapseControlID="Image1" AutoCollapse="False" AutoExpand="False" ScrollContents="false"
                ExpandDirection="Vertical" ImageControlID="Image1" ExpandedImage="~/Images/16 X 16 UpArrow.jpg"
                CollapsedImage="~/Images/16 X 16 DownArrow.jpg">
            </asp:CollapsiblePanelExtender>
            <div style="width: 100%; float: left; color: Navy; padding-top: 2px; padding-bottom: 2px;">
                <asp:Panel runat="server" Height="100%" ID="pnlInvItem" Width="99.8%" ScrollBars="Auto"
                    BorderColor="#9F9F9F" BorderWidth="1px" Font-Bold="true">
                    <div style="float: left; width: 100%; font-weight: normal; padding-top: 2px; padding-bottom: 3px;">
                        <div style="float: left; width: 100%;">
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 6%; height: 13px;">
                                From . . . .
                            </div>
                            <div style="float: left; width: 17%;">
                                <asp:TextBox runat="server" ID="txtDateFrom" CssClass="TextBox" Width="80%"></asp:TextBox>
                                &nbsp;
                                <asp:Image ID="imgDOFromDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png"
                                    ImageAlign="Middle" />
                                <asp:CalendarExtender ID="DOFromDateCalExtender" runat="server" Animated="true" EnabledOnClient="true"
                                    Format="dd/MM/yyyy" PopupPosition="BottomLeft" TargetControlID="txtDateFrom"
                                    PopupButtonID="imgDOFromDate">
                                </asp:CalendarExtender>
                            </div>
                            <div style="float: left; width: 4%;">
                                &nbsp;</div>
                            <div style="float: left; width: 6%;">
                                To . . . .
                            </div>
                            <div style="float: left; width: 17%;">
                                <asp:TextBox runat="server" ID="txtDateTo" CssClass="TextBox" Width="80%"></asp:TextBox>
                                &nbsp;
                                <asp:Image ID="imgDOToDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png"
                                    ImageAlign="Middle" />
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Animated="true" EnabledOnClient="true"
                                    Format="dd/MM/yyyy" PopupPosition="BottomLeft" TargetControlID="txtDateTo" PopupButtonID="imgDOToDate">
                                </asp:CalendarExtender>
                            </div>
                            <div style="float: left; width: 2%;">
                                &nbsp;</div>
                            <div style="float: left; width: 25%;">
                                <asp:ImageButton ID="btnImgSearch" runat="server" ImageUrl="~/Images/icon_search.png"
                                    ImageAlign="Middle" OnClick="btnSearch_Click" Visible="true" />
                                &nbsp;
                            </div>
                            <div style="float: left; width: 15%;">
                            </div>
                        </div>
                    </div>
                    <%-- Item Detail --%>
                    <div style="float: left; width: 100%; height: 131px; padding-bottom: 2px; background-color: #FFFFFF;">
                        <asp:Panel ID="pnlItem" runat="server" Height="131px" ScrollBars="Vertical" BorderColor="#9F9F9F"
                            BorderWidth="1px" Font-Bold="true" Width="99%">
                            <%--Pending Invoice Grid--%>
                            <asp:GridView ID="gvInvoiceItem" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                                CellPadding="4" ForeColor="#333333" DataKeyNames="SAH_INV_NO,SAH_DT" CssClass="GridView"
                                ShowHeaderWhenEmpty="True" OnRowDeleting="gvInvoiceItem_RowDeleting">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <%--<asp:CommandField ShowSelectButton="True" SelectText="select" />
                                    <asp:BoundField DataField="SAH_INV_NO" HeaderText="Invoice No" />--%>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Invoice No" ShowHeader="true">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbtnInvoiceNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SAH_INV_NO") %>'
                                                CommandArgument='<%# Eval("SAH_INV_NO") %>' CommandName="Delete"></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="SAH_DT" HeaderText="Invoice Date" HeaderStyle-Width="75px"
                                        DataFormatString="{0:dd/MMM/yyyy}" HeaderStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="SAH_CUS_CD" HeaderText="Customer Code" />
                                    <asp:BoundField DataField="SAH_CUS_NAME" HeaderText="Customer Name" />
                                    <asp:BoundField DataField="SAH_CUS_ADD1" HeaderText="Customer Address 1" />
                                    <asp:BoundField DataField="SAH_CUS_ADD2" HeaderText="Customer Address 2" />
                                </Columns>
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Size="12px" Font-Bold="True" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <%--<RowStyle Font-Size="12px" BackColor="#EFF3FB" />--%>
                                <RowStyle BackColor="#EFF3FB" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            </asp:GridView>
                            <%--Pending Invoice Grid--%>
                        </asp:Panel>
                    </div>
                </asp:Panel>
            </div>
        </div>
        <%--Modal popup panel--%>
        <asp:Panel ID="PanelItemPopUp" runat="server" Height="320px" Width="642px" BackColor="#A7C2DA"
            BorderColor="#3333FF" BorderWidth="2px">
            <div style="float: left; width: 100%; height: 22px; text-align: right; padding-top: 2px">
                <div style="float: left; width: 2%; height: 22px; text-align: left;">
                </div>
                <div id="divPopupImg" runat="server" visible="false" style="float: left; width: 3%;
                    height: 22px; text-align: left;">
                    <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/warning.gif" Width="15px"
                        Height="15px" />
                </div>
                <div style="float: left; width: 65%; height: 22px; text-align: left;">
                    <asp:Label ID="lblpopupMsg" runat="server" Width="100%" ForeColor="Red" />
                </div>
                <div style="float: left; width: 30%; height: 22px; text-align: right;">
                    <asp:ImageButton ID="btnimgAdd" runat="server" ImageUrl="~/Images/approve_img.png"
                        ImageAlign="Middle" OnClick="btnPopupOk_Click" Visible="true" Width="20px" Height="20px" />
                    &nbsp;
                    <asp:ImageButton ID="btnimgCancel" runat="server" ImageUrl="~/Images/error_icon.png"
                        ImageAlign="Middle" OnClick="btnPopupCancel_Click" Visible="true" Width="22px"
                        Height="22px" />
                    &nbsp;
                </div>
            </div>
            <div style="text-align: right">
                <asp:HiddenField ID="hdnInvoiceNo" runat="server" />
                <asp:HiddenField ID="hdnInvoiceLineNo" runat="server" />
                <asp:Label ID="lblPopupAmt" runat="server" Style="text-align: right"></asp:Label>&nbsp;
            </div>
            <div style="float: right; width: 100%; height: 22px; text-align: left; padding-top: 2px;
                padding-bottom: 2px">
                Item Code:
                <asp:Label ID="lblPopupItemCode" runat="server" Font-Bold="True"></asp:Label>
                <asp:Label ID="lblPopupBinCode" runat="server" Font-Bold="True"></asp:Label>
            </div>
            <div style="float: left; width: 100%; text-align: left;">
                <div id="divSerialSelect" runat="server" style="float: left; width: 100%; text-align: left;">
                    <div style="float: left; width: 3%; padding-top: 2px; padding-bottom: 3px">
                    </div>
                    <div style="float: left; width: 15%;">
                        Search by :
                    </div>
                    <div style="float: left; width: 14%;">
                        <asp:DropDownList ID="ddlPopupSerial" runat="server" Width="85%" CssClass="ComboBox">
                            <asp:ListItem>Serial 1</asp:ListItem>
                            <asp:ListItem>Serial 2</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div style="float: left; width: 15%;">
                        <asp:TextBox ID="txtPopupSearchSer" runat="server" CssClass="TextBox" Width="100%"></asp:TextBox>
                    </div>
                    <div style="float: left; width: 11%;">
                        &nbsp;
                        <asp:Button ID="btnPopupSarch" runat="server" CssClass="Button" OnClick="btnPopupSarch_Click"
                            Text="Search" />
                    </div>
                </div>
                <div id="divQtySelect" runat="server" visible="false" style="float: left; width: 100%;
                    text-align: left; padding-top: 2px; padding-bottom: 3px">
                    <div style="float: left; width: 3%;">
                    </div>
                    <div style="float: left; width: 15%; text-align: left;">
                        <asp:Label ID="lblPopQty" runat="server" Text="Qty:" Visible="False"></asp:Label>
                    </div>
                    <div style="float: left; width: 29%; text-align: left;">
                        <asp:TextBox ID="txtPopupQty" runat="server" CssClass="TextBox" Visible="False" Width="100%"
                            ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <div style="float: left; width: 30%; text-align: left;">
                        &nbsp;
                        <asp:Button ID="btnPopupAutoSelect" runat="server" CssClass="Button" OnClick="btnPopupAutoSelect_Click"
                            OnClientClick="SelectAuto()" Text="Auto Select" visble="false" />
                    </div>
                </div>
                <div style="float: left; width: 100%; text-align: left; padding-top: 1px; padding-bottom: 2px">
                    <div style="float: left; width: 3%;">
                    </div>
                    <div style="float: left; width: 15%; text-align: left;">
                        Invoice Qty :
                    </div>
                    <div style="float: left; width: 15%; text-align: left;">
                        <asp:Label ID="lblInvoiceQty" runat="server" CssClass="TextBox" Width="85%"></asp:Label>
                    </div>
                </div>
                <div style="float: left; width: 100%; text-align: left; padding-top: 1px; padding-bottom: 2px">
                    <div style="float: left; width: 3%;">
                    </div>
                    <div style="float: left; width: 15%; text-align: left;">
                        Delivered Qty :
                    </div>
                    <div style="float: left; width: 15%; text-align: left;">
                        <asp:Label ID="lblDeliveredQty" runat="server" CssClass="TextBox" Width="85%"></asp:Label>
                    </div>
                </div>
                <div style="float: left; width: 100%; text-align: left; padding-top: 1px; padding-bottom: 2px">
                    <div style="float: left; width: 3%;">
                    </div>
                    <div style="float: left; width: 15%; text-align: left;">
                        Scan Qty :
                    </div>
                    <div style="float: left; width: 15%; text-align: left;">
                        <asp:Label ID="lblScanQty" runat="server" CssClass="TextBox" Width="85%"></asp:Label>
                    </div>
                </div>
            </div>
            <div style="width: 608px">
                <asp:Panel ID="popupGridPanel" runat="server" Height="172px" ScrollBars="Auto" Style="margin-left: 15px;
                    margin-bottom: 13px" Width="100%">
                    <asp:GridView ID="GridPopup" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        Height="45px" Width="95%" CssClass="GridView" ShowHeaderWhenEmpty="True" EmptyDataText="No data found">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkPopupSelectAll" runat="server" ClientIDMode="Static" onclick="SelectAll(this.id)" />
                                </HeaderTemplate>
                                <EditItemTemplate>
                                    <asp:CheckBox ID="CheckBox2" runat="server" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="checkPopup" runat="server" ClientIDMode="Static" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Tus_ser_1" HeaderText="Serial No 1" />
                            <asp:BoundField DataField="Tus_ser_2" HeaderText="Serial No 2" />
                            <asp:BoundField DataField="Tus_itm_stus" HeaderText="Current Status" />
                            <asp:BoundField DataField="Tus_warr_no" HeaderText="Warrant #" />
                            <asp:BoundField DataField="Tus_bin" HeaderText="Bin Code" />
                            <asp:BoundField DataField="Tus_itm_desc" HeaderText="Description" />
                            <asp:BoundField DataField="Tus_ser_id" HeaderText="Serial ID" />
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
            <br />
            &nbsp;
        </asp:Panel>
        <%--Modal popup panel--%><%--  ********ADD*****--%>
        <asp:ModalPopupExtender ID="ModalPopupExtItem" runat="server" CancelControlID="btnimgCancel"
            PopupControlID="PanelItemPopUp" TargetControlID="btnHidden_popup" ClientIDMode="Static">
        </asp:ModalPopupExtender>
        <%--********ADD******--%><%-- Collaps Header - General Detail --%>
        <div style="height: 18px; background-color: #1E4A9F; color: White; width: 98%; float: left;">
            <div style="width: 50%; float: left;">
                General Detail</div>
            <div style="width: 20%; float: right;">
                Invoice Date :
                <asp:Label ID="lblInvoiceDate" runat="server" ForeColor="Yellow"></asp:Label>
            </div>
            <div style="width: 20%; float: right;">
                Invoice No :
                <asp:Label ID="lblInvoiceNo" runat="server" ForeColor="Yellow"></asp:Label>
            </div>
        </div>
        <%-- Collaps Image - General Detail --%>
        <div style="float: left;">
            <asp:Image runat="server" ID="Image2" ImageUrl="~/Images/16 X 16 DownArrow.jpg" ImageAlign="Right" /></div>
        <%-- Collaps control - General Detail --%>
        <asp:CollapsiblePanelExtender ID="CPEGeneral" runat="server" TargetControlID="Panel_NewScanDetail"
            CollapsedSize="0" ExpandedSize="131" Collapsed="True" ExpandControlID="Image2"
            CollapseControlID="Image2" AutoCollapse="False" AutoExpand="False" ScrollContents="false"
            ExpandDirection="Vertical" ImageControlID="Image2" ExpandedImage="~/Images/16 X 16 UpArrow.jpg"
            CollapsedImage="~/Images/16 X 16 DownArrow.jpg" ClientIDMode="Static">
        </asp:CollapsiblePanelExtender>
        <%--General panel --%>
        <div style="float: left; width: 100%; padding-top: 2px; padding-bottom: 2px; color: Black">
            <asp:Panel ID="Panel_NewScanDetail" runat="server" Width="99%" Visible="True">
                <%--<div style="float: left; width: 100%; padding-top: 2px;">
                    <div id ="divlblDirectDo1" runat="server" style="float: left; width: 1%;" visible="false">
                        &nbsp;</div>
                    <div id ="divlblDirectDo" runat="server" style="float: left; width: 10%;" visible="false">
                        Direct DO . . . .
                    </div>
                    <div id ="divchkDirectDo" runat="server" style="float: left; width: 12%;" visible="false">
                        <asp:CheckBox ID="chkDirectDO" runat="server" OnCheckedChanged="chkDirectDO_CheckChange"
                            AutoPostBack="true" />
                    </div>
                    <div style="float: left; width: 8%;">
                        &nbsp;</div>
                    <div style="float: left; width: 10%; margin-bottom: 0px;" >
                        Invoice No . . . .</div>
                    <div style="float: left; width: 19%;">
                        &nbsp;<asp:Label ID="lblInvoiceNo" runat="server" ForeColor="#CC3300"></asp:Label>
                    </div>
                    <div style="float: left; width: 14%;" id ="div1" runat="server" visible="false">
                        Scan Batches . . . . . . .</div>
                    <div style="float: left; width: 11%;" id ="div2" runat="server" visible="false">
                        <asp:TextBox runat="server" ID="TextBox2" CssClass="TextBox" Width="90%"></asp:TextBox>
                    </div>
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 10%;" id ="div3" runat="server" visible="false">
                        <asp:Button ID="btnPickScan" runat="server" Text="Pick Scan" CssClass="Button" Width="80%"
                            OnClick="btnPickScan_Click" />
                    </div>
                </div>--%>
                <div style="float: left; width: 100%; padding-top: 2px;">
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 10%;">
                        Date . . . . . . . .</div>
                    <div style="float: left; width: 16%;">
                        <asp:TextBox runat="server" ID="txtDODate" CssClass="TextBox" Width="70%"></asp:TextBox>
                        <asp:Image ID="imgDODate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png"
                            ImageAlign="Middle" Visible="False" />
                        <asp:CalendarExtender ID="DODateCalExtender" runat="server" TargetControlID="txtDODate"
                            PopupButtonID="imgDODate" Enabled="True" Format="dd/MMM/yyyy">
                        </asp:CalendarExtender>
                    </div>
                    <div style="float: left; width: 4%;">
                        &nbsp;</div>
                    <div style="float: left; width: 10%;">
                        Customer . . . . .
                    </div>
                    <div style="float: left; width: 14%;">
                        <asp:TextBox runat="server" Width="95%" ID="txtCustCode" CssClass="TextBoxUpper"
                            Enabled="false"></asp:TextBox></div>
                    <div style="float: left; width: 3%;">
                        &nbsp;<asp:ImageButton ID="imgBtnSerial2" runat="server" ImageAlign="Middle" ImageUrl="~/Images/icon_search.png"
                            Enabled="false" />
                    </div>
                    <div style="float: left; width: 41%;">
                        <asp:TextBox runat="server" ID="txtCustName" Width="95%" CssClass="TextBox" Enabled="false"></asp:TextBox></div>
                </div>
                <div style="float: left; width: 100%; padding-top: 3px;">
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 10%;">
                        Manual Ref No .</div>
                    <div style="float: left; width: 16%;">
                        <asp:TextBox runat="server" ID="txtManualRefNo" CssClass="TextBox" Style="text-align: left;"
                            MaxLength="30"></asp:TextBox>
                    </div>
                    <div style="float: left; width: 4%;">
                        &nbsp;</div>
                    <div style="float: left; width: 10%;">
                        Address . . . . . .
                    </div>
                    <div style="float: left; width: 56%;">
                        <asp:TextBox runat="server" ID="txtCustAdd1" Width="100%" CssClass="TextBox" Enabled="false"></asp:TextBox></div>
                </div>
                <div style="float: left; width: 100%; padding-top: 2px;">
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 10%;">
                        Vehicle No . . . .</div>
                    <div style="float: left; width: 16%;">
                        <asp:TextBox runat="server" ID="txtVehicleNo" CssClass="TextBox" Style="text-align: left;"
                            MaxLength="30"></asp:TextBox>
                    </div>
                    <div style="float: left; width: 4%;">
                        &nbsp;</div>
                    <div style="float: left; width: 10%;">
                        &nbsp;
                    </div>
                    <div style="float: left; width: 56%;">
                        <asp:TextBox runat="server" ID="txtCustAdd2" Width="100%" CssClass="TextBox" Enabled="false"></asp:TextBox></div>
                </div>
                <div style="float: left; width: 100%; padding-top: 2px;">
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 10%;">
                        Remarks
                    </div>
                    <div style="float: left; width: 86%;">
                        <%--  <asp:TextBox runat="server" ID="txtRemarks" Width="100%" TextMode="MultiLine" Rows="2"
                            Height="21px" CssClass="TextBox"></asp:TextBox>--%>
                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Rows="2" Width="100%"
                            ClientIDMode="Static" CssClass="TextBox" onKeyUp="javascript:CheckCharacterCount('txtRemarks',250);"
                            onChange="javascript:CheckCharacterCount('txtRemarks',250);"></asp:TextBox>
                    </div>
                </div>
            </asp:Panel>
        </div>
        <%--General panel --%><%-- Collaps Header - General Detail --%>
        <div style="height: 18px; background-color: #1E4A9F; color: White; width: 98%; float: left;">
            Scan Detail
        </div>
        <div style="float: left;">
            <asp:Image runat="server" ID="Image3" ImageUrl="~/Images/16 X 16 DownArrow.jpg" ImageAlign="Right" />
        </div>
        <%-- Collaps control - General Detail --%>
        <asp:CollapsiblePanelExtender ID="CPEScanItem" runat="server" TargetControlID="pnlScanItem"
            CollapsedSize="0" ExpandedSize="50" Collapsed="True" ExpandControlID="Image3"
            CollapseControlID="Image3" AutoCollapse="False" AutoExpand="False" ScrollContents="false"
            ExpandDirection="Vertical" ImageControlID="Image3" ExpandedImage="~/Images/16 X 16 UpArrow.jpg"
            CollapsedImage="~/Images/16 X 16 DownArrow.jpg" ClientIDMode="Static">
        </asp:CollapsiblePanelExtender>
        <%--Item Scan panel --%>
        <div style="float: left; width: 100%; padding-top: 2px; padding-bottom: 2px; color: Black">
            <asp:Panel ID="pnlScanItem" runat="server" Width="99%" Visible="false">
                <div style="float: left; width: 100%; padding-top: 2px;">
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 5%;">
                        Item</div>
                    <div style="float: left; width: 15%;">
                        <asp:TextBox ID="txtItem" runat="server" CssClass="TextBox" Width="80%"></asp:TextBox>
                        <asp:ImageButton ID="imgBtnSerial1" runat="server" ImageUrl="~/Images/icon_search.png"
                            ImageAlign="Middle" />
                    </div>
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 8%;">
                        Description</div>
                    <div style="float: left; width: 35%;">
                        <asp:TextBox ID="txtItemDesc" runat="server" Width="95%" Style="font-size: 11px;
                            border-style: none;"></asp:TextBox></div>
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 6%;">
                        Model</div>
                    <div style="float: left; width: 13%;">
                        <asp:TextBox ID="txtModel" runat="server" Width="100%" Style="font-size: 11px; border-style: none;"></asp:TextBox></div>
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 4%;">
                        Brand</div>
                    <div style="float: left; width: 8%;">
                        <asp:TextBox ID="txtBrand" runat="server" Width="100%" Style="font-size: 11px; border-style: none;"></asp:TextBox></div>
                </div>
                <div style="float: left; width: 100%; padding-top: 2px;">
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 5%;">
                        Status</div>
                    <div style="float: left; width: 15%;">
                        <asp:DropDownList runat="server" ID="ddlStatus" Width="73%">
                        </asp:DropDownList>
                    </div>
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 8%;">
                        Qty</div>
                    <div style="float: left; width: 10%;">
                        <asp:TextBox ID="txtQty" runat="server" Width="80%" CssClass="TextBox"></asp:TextBox></div>
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 8%;">
                        Aval. Qty</div>
                    <div style="float: left; width: 10%;">
                        <asp:TextBox ID="txtAvalQty" runat="server" Width="80%" CssClass="TextBox"></asp:TextBox></div>
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 8%;">
                        Free Qty</div>
                    <div style="float: left; width: 10%;">
                        <asp:TextBox ID="txtFreeQty" runat="server" Width="80%" CssClass="TextBox"></asp:TextBox></div>
                    <div style="float: left; width: 12%;">
                        &nbsp;</div>
                    <div style="float: left; width: 5%;">
                        <asp:ImageButton ID="btnAddItemNew" runat="server" ImageUrl="~/Images/download_arrow_icon.png"
                            Height="16px" Width="16px" ToolTip="Add Item" />
                        <%--OnClick="btnAddItem_Click" --%>
                    </div>
                </div>
            </asp:Panel>
        </div>
        <%--Item Scan panel --%><%--Tab panel--%>
        <div style="float: left; width: 99%; padding-top: 2px; padding-bottom: 2px;">
            <div>
                <asp:Panel ID="PanelTabContainer" runat="server">
                    <asp:TabContainer ID="TabContainerShow" runat="server" ActiveTabIndex="0" Width="100%"
                        Height="151px" Style="margin-left: 0px">
                        <%--Tab Panel - Items--%>
                        <asp:TabPanel runat="server" HeaderText=" Items " ID="TabPanel_item">
                            <HeaderTemplate>
                                Items
                            </HeaderTemplate>
                            <ContentTemplate>
                                <asp:Panel ID="Panel_itmDo" runat="server" Height="151px" Width="100%">
                                    <div>
                                    </div>
                                    <asp:GridView ID="GridViewDo_itm" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                                        OnSelectedIndexChanged="GridViewDo_itm_SelectedIndexChanged" CellPadding="4"
                                        ForeColor="#333333" CssClass="GridView" OnRowDataBound="InvoiceItemLoad" ShowHeaderWhenEmpty="True"
                                        Width="100%" EmptyDataText="No data found" DataKeyNames="Sad_itm_cd">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" ShowSelectButton="True" SelectImageUrl="~/Images/Add-16x16x16.ICO" />
                                            <asp:BoundField DataField="sad_itm_line" HeaderText="#">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Sad_itm_cd" HeaderText="Item Code">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Mi_longdesc" HeaderText="Description">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Mi_model" HeaderText="Model">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Sad_qty" HeaderText="Invoice Qty">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Sad_do_qty" HeaderText="Saved DO Qty">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Pick Qty">
                                                <ItemTemplate>
                                                    <%--<asp:TextBox ID="txtGridDoQty" runat="server" Text='<%# Bind("Sad_tot_amt") %>'></asp:TextBox>--%>
                                                    <asp:TextBox ID="txtGridDoQty" runat="server" Text='<%# Bind("sad_srn_qty") %>' Style="text-align: right"
                                                        Width="100px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="False">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnGridUpdate" runat="server" OnClick="btnGridUpdate_Click" Text="Update" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item_line" Visible="False">
                                                <ItemTemplate>
                                                    <div style="overflow: auto; width: 450px; height: 200px;">
                                                        <asp:Label ID="lblItemLine" runat="server" Text='<%# Bind("Sad_itm_line")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="sad_pbook" HeaderText="Price Book">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="sad_pb_lvl" HeaderText="Price Level">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="Sad_itm_stus" HeaderText="Item Status">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                        </Columns>
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Size="12px" ForeColor="White" Font-Bold="True" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle Font-Size="12px" BackColor="#EFF3FB" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    </asp:GridView>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:TabPanel>
                        <%--Tab Panel - Items--%>
                        <%--Tab Panel - Serials--%>
                        <asp:TabPanel ID="TabPanel_serial" runat="server" HeaderText="Serials">
                            <HeaderTemplate>
                                Serials
                            </HeaderTemplate>
                            <ContentTemplate>
                                <asp:Panel ID="Panel_serDo" runat="server" Height="151px">
                                    <div style="text-align: right;">
                                        <asp:Button ID="btnDeleteSer" runat="server" Text="Delete" CssClass="Button" OnClick="btnDeleteSer_Click" />
                                    </div>
                                    <asp:Panel ID="Panel1" runat="server" Height="140px" ScrollBars="Both">
                                        <asp:GridView ID="GridViewDo_serials" runat="server" AutoGenerateColumns="False"
                                            CellPadding="4" ForeColor="#333333" CssClass="GridView" ShowHeaderWhenEmpty="True"
                                            GridLines="None" EmptyDataText="No data found">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="SelectCheck" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Tus_ser_1" HeaderText="Serial No 1" />
                                                <asp:BoundField DataField="Tus_itm_cd" HeaderText="Item Code" />
                                                <asp:BoundField DataField="Tus_bin" HeaderText="Bin Code" />
                                                <asp:BoundField DataField="Tus_itm_desc" HeaderText="Item Description" />
                                                <asp:BoundField DataField="Tus_itm_model" HeaderText="Model" />
                                                <asp:BoundField DataField="Tus_itm_stus" HeaderText="Item Status" />
                                                <asp:BoundField DataField="Tus_qty" HeaderText="Qty" />
                                                <asp:BoundField DataField="Tus_usrseq_no" HeaderText="User sequence No" />
                                                <asp:BoundField DataField="Tus_ser_id" HeaderText="Serial ID" />
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
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:TabPanel>
                        <%--Tab Panel - Serials--%>
                    </asp:TabContainer>
                </asp:Panel>
            </div>
        </div>
        <%--Tab Panel - Items--%>
        <div style="display: none">
            <span class="style9">
                <br class="style7" />
                <span class="style7">&nbsp;
                    <br />
                    &nbsp;</span></span><asp:Button ID="btnHidden_popup" runat="server" Text="Button"
                        CssClass="style9" />
            <span class="style9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <br />
            </span>
        </div>
    </div>
</asp:Content>
