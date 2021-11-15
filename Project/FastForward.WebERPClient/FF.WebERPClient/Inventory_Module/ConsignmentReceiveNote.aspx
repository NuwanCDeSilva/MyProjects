<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ConsignmentReceiveNote.aspx.cs" Inherits="FF.WebERPClient.Inventory_Module.ConsignmentReceiveNote" %>

<%@ Register Src="~/UserControls/uc_MsgInfo.ascx" TagName="uc_MsgInfo" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../MainStyleSheet.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        function DeleteConfirm() {
            if (confirm("Are you sure to delete?")) {
                return true;
            }
            else {
                return false;
            }
        }
    
    </script>

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
</script>
    <style type="text/css">
        .newStyle1
        {
            font-family: Verdana;
            font-size: 11px;
        }
        .newStyle2
        {
            font-family: Verdana;
            font-size: 11px;
        }
        .style1
        {
            float: left;
            width: 30%;
            vertical-align: bottom;
            color: white;
            padding-top: 2px;
            padding-bottom: 2px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="float: left; width: 3%">
        &nbsp;
    </div>
    <div style="float: left; width: 94%">
        <div style="float: left; width: 94%; height: 251px;">
            <asp:Image ID="imgPendingRequests" runat="server" ImageUrl="~/Images/expand_icon.jpg" />
            <asp:Panel ID="PanelPendReq" runat="server" GroupingText="Search panel" >
                    <div > <%--class="MainDivCss"--%>
                    <br />
                    <div style="width: 15%; float: left">
                        From :<br />
                        <asp:TextBox ID="txtFromDate" runat="server" Width="75px" ClientIDMode="Static"></asp:TextBox>
                        <asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate"
                            Format="dd/MM/yyyy" PopupButtonID="imgFromDate" Enabled="True">
                        </asp:CalendarExtender>
                    </div>
                    <div style="width: 14%; float: left">
                        To :<br />
                        <asp:TextBox ID="txtToDate" runat="server" Width="75px" ClientIDMode="Static"></asp:TextBox>
                        <asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate"
                            Format="dd/MM/yyyy" PopupButtonID="imgToDate" Enabled="True">
                        </asp:CalendarExtender>
                    </div>
                    <div style="width: 20%; float: left">
                        Supplier :<br />
                        <asp:TextBox ID="txtSupplier" runat="server" Width="70%" ClientIDMode="Static"></asp:TextBox>
                        <asp:ImageButton ID="imgFindSupplier" runat="server" ImageUrl="~/Images/icon_search.png" onclick="ImageBtnSupplier_Click"/>
                    </div>
                    <div style="width: 20%; float: left">
                        RequestNo :<br />
                        <asp:TextBox ID="txtRequestNo" runat="server" Width="70%" ClientIDMode="Static"></asp:TextBox>
                        <asp:ImageButton ID="imgFindRequestNo" runat="server" ImageUrl="~/Images/icon_search.png" onclick="imgPOSearch_Click"/>
                    </div>
                    <div style="width: 25%; float: left">
                        &nbsp;<br />
                        <asp:Button ID="btnRequestSearch" runat="server" Text="Search" ClientIDMode="Static"
                            CssClass="Button" OnClick="btnRequestSearch_Click" />&nbsp;&nbsp;<asp:Button ID="btnConsReceiptSave" runat="server" Text="Save" CssClass="Button"
                            OnClick="btnConsReceiptSave_Click" />&nbsp;
                        <asp:Button ID="btnConsReceiptClear" runat="server" Text="Clear" CssClass="Button"
                            OnClick="btnConsReceiptClear_Click" />
                    </div>
                </div>
                    </asp:Panel>
            <asp:Panel ID="pnlPendingRequests" runat="server" 
                GroupingText="Pending Requests" BorderStyle="Solid" BorderWidth="1px">
                
                
                <div style="border-width: 11px" > <%--class="MainDivCss"--%>
                    <br />
                    
                    
                </div>
                <asp:GridView ID="gvPendingRequests" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" OnRowCommand="gvPendingRequests_RowCommand" 
                    CssClass="GridView">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Req. No" ShowHeader="true">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbtnReqNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Poh_doc_no") %>'
                                        CommandName="SELECTREQUEST" CommandArgument='<%# Eval("Poh_doc_no") %>'></asp:LinkButton>
                                    <asp:HiddenField ID="hdnPohSeqNo" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Poh_seq_no") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Date" ShowHeader="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblReqDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Poh_remarks") %>'></asp:Label> <%--for displya purpose, "Poh_dt" replaced by "Poh_remarks"--%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Supplier Code"
                                ShowHeader="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblSuppCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MasterBusinessEntity.Mbe_cd") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Supplier Name"
                                ShowHeader="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblSuppName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MasterBusinessEntity.Mbe_name") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Width="200px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Supplier AccCode"
                                ShowHeader="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblSuppAccNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MasterBusinessEntity.Mbe_acc_cd") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="RefNo" ShowHeader="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblRefNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Poh_ref") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#003366" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
            </asp:Panel>
            <asp:CollapsiblePanelExtender ID="cpePendingRequests" runat="server" TargetControlID="pnlPendingRequests"
                CollapsedSize="0" ExpandedSize="150" Collapsed="false" ExpandControlID="imgPendingRequests"
                CollapseControlID="imgPendingRequests" AutoCollapse="False" AutoExpand="False"
                ScrollContents="true" CollapsedText="Show Details..." ExpandedText="Hide Details"
                ImageControlID="imgPendingRequests" ExpandedImage="~/Images/expand_icon.jpg"
                CollapsedImage="~/Images/collapse_icon.jpg" ExpandDirection="Vertical">
            </asp:CollapsiblePanelExtender>
            <br />
        </div>
        <div style="float: left; width: 94%; height: 119px;"> <%----%>
            <asp:Image ID="imgReceiptItemDetails" runat="server" ImageUrl="~/Images/expand_icon.jpg" />
            <asp:Panel ID="pnlReceiptItemDetails" runat="server" 
                GroupingText="Receipt Item Details" BorderStyle="Solid" BorderWidth="1px" 
                style="margin-right: 0px">
                <asp:TabContainer ID="tcReceiptItemDetails" runat="server" ActiveTabIndex="0" 
                    Height="100px" ScrollBars="Vertical" >
                    <asp:TabPanel ID="tbpItemDetails" HeaderText="Item Details" runat="server">
                        <ContentTemplate>
                                <asp:GridView ID="gvReceiptItemDetails" runat="server" AutoGenerateColumns="False"
                                    OnRowCommand="gvReceiptItemDetails_RowCommand" CellPadding="4" ForeColor="#333333"
                                    GridLines="None" OnRowDataBound="gvReceiptItemDetails_RowDataBound" 
                                style="font-size: 11px; font-family: Verdana">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Item Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MasterItem.Mi_cd") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MasterItem.Mi_longdesc") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="150px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Brand">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBrand" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MasterItem.Mi_brand") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Model">
                                            <ItemTemplate>
                                                <asp:Label ID="lblModel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MasterItem.Mi_model") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Podi_itm_stus") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="75px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Req Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBalQty" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Podi_bal_qty") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Act Qty">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnItemIsSerialize1" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "MasterItem.Mi_is_ser1") %>' />
                                                <asp:TextBox ID="txtActualQty" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Actual_qty") %>' BorderStyle="None" Font-Bold="True" Font-Size="11px" ForeColor="Black"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Add Items">
                                            <ItemTemplate>
                                                <asp:Button ID="btnAddSerials" runat="server" CssClass="Button" Text="Add Items"
                                                    CommandName="ADDSERIALS" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "MasterItem.Mi_cd") + "|" + DataBinder.Eval(Container.DataItem, "PurchaseOrderDetail.Pod_unit_price") + "|" + DataBinder.Eval(Container.DataItem, "PurchaseOrderDetail.Pod_line_no") + "|" + DataBinder.Eval(Container.DataItem, "Podi_bal_qty") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="75px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="tbpSerialDetails" HeaderText="Serial Details" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlAllItemsSerials" runat="server" Width="100%" Height="100px"> <%--ScrollBars="Auto"--%>
                                <div> <%--class="MainDivCss">--%>
                                    <asp:GridView ID="gvAllItemSerials" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        ForeColor="#333333" GridLines="None" OnRowCommand="gvAllItemSerials_RowCommand" style="font-size: 11px; font-family: Verdana" CssClass="GridView">
                                        <Columns>
                                            <asp:BoundField DataField="Tus_usrseq_no" HeaderText="Seq no" />
                                            <asp:BoundField DataField="Tus_itm_cd" HeaderText="Item Code" />
                                            <asp:BoundField DataField="Tus_ser_1" HeaderText="Serial No1" />
                                            <asp:BoundField DataField="Tus_ser_2" HeaderText="Serial No2" />
                                            <asp:BoundField DataField="Tus_ser_3" HeaderText="Serial No3" />
                                            <asp:BoundField DataField="Tus_itm_stus" HeaderText="Item Status" />
                                            <asp:BoundField DataField="Tus_bin" HeaderText="Bin Code" />
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtndelAllSerial" runat="server" ImageUrl="~/Images/Delete.png"
                                                        CommandName="DeleteItem" CommandArgument='<%# Eval("Tus_ser_id") + "|" + Eval("Tus_itm_cd") + "|" + Eval("Tus_ser_1") + "|" + Eval("Tus_bin")+ "|" + Eval("Tus_itm_line")  %>'
                                                        OnClientClick="return DeleteConfirm()" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="75px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
            </asp:Panel>
            <asp:CollapsiblePanelExtender ID="cpeReceiptItemDetails" runat="server" TargetControlID="pnlReceiptItemDetails"
                CollapsedSize="0" ExpandedSize="200" Collapsed="false" ExpandControlID="imgReceiptItemDetails"
                CollapseControlID="imgReceiptItemDetails" AutoCollapse="False" AutoExpand="False"
                ScrollContents="true" CollapsedText="Show Details..." ExpandedText="Hide Details"
                ImageControlID="imgReceiptItemDetails" ExpandedImage="~/Images/expand_icon.jpg"
                CollapsedImage="~/Images/collapse_icon.jpg" ExpandDirection="Vertical">
            </asp:CollapsiblePanelExtender>
        </div>
        <div  style="float: left; width: 94%; height: 127px; margin-top: 65px;">
            <asp:Image ID="imgRequestInfo" runat="server" ImageUrl="~/Images/expand_icon.jpg" />
            <asp:Panel ID="pnlRequestInfo" runat="server" GroupingText="Request Info" 
                BorderStyle="Solid" BorderWidth="1px" >
                <div > <%--class="MainDivCss"--%>
                    <div class="SubDivCss">
                        <div class="innerLeftDivCss">
                            Receipt Date :</div>
                        <div class="innerRightDivCss">
                            <asp:TextBox ID="txtReceiptDate" runat="server" Width="75px" CssClass="commonDDLCss"
                                ClientIDMode="Static" Enabled="False"></asp:TextBox>
                            <asp:Image ID="imgRequestDate" runat="server" 
                                ImageUrl="~/Images/Calendar_scheduleHS.png" Visible="False" />
                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtReceiptDate"
                                Format="dd/MM/yyyy" PopupButtonID="imgRequestDate" Enabled="True">
                            </asp:CalendarExtender>
                        </div>
                        <div class="innerLeftDivCss">
                            Supplier Code :</div>
                        <div class="innerRightDivCss">
                            <asp:Label ID="lblSupplierCode" runat="server" CssClass="commonDDLCss" Width="75px"></asp:Label>
                        </div>
                        <div class="innerLeftDivCss">
                            Request Ref No :</div>
                        <div class="innerRightDivCss">
                            <asp:Label ID="lblReqRefNo" runat="server" Width="75px" CssClass="commonDDLCss"></asp:Label>
                        </div>
                    </div>
                    <div class="SubDivCss"> 
                        <div class="innerLeftDivCss">
                            Request No :</div>
                        <div class="innerRightDivCss">
                            <asp:Label ID="lblRequestNo" runat="server" Width="75px" CssClass="commonDDLCss"></asp:Label>
                        </div>
                        <div class="style1">
                            Approved by :</div>
                        <div class="innerRightDivCss">
                            &nbsp;<asp:Label ID="lblSupplierName" runat="server" Width="75px" Visible="False"></asp:Label>
                        </div>
                        <div class="innerLeftDivCss">
                            Ref No :</div>
                        <div class="innerRightDivCss">
                            <asp:TextBox ID="txtRefNo" runat="server" CssClass="commonDDLCss" Width="150px" 
                                ClientIDMode="Static" MaxLength="16"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div > <%--class="MainDivCss"--%>
                    <div style="width: 100%; float: left">
                        Remarks : &nbsp;
                        <asp:TextBox ID="txtRemarks" runat="server" Width="30%" ClientIDMode="Static" 
                            MaxLength="30" style="font-size: 11px; font-family: Verdana"></asp:TextBox>
                    </div>
                </div>
            </asp:Panel>
            <asp:CollapsiblePanelExtender ID="cpeRequestInfo" runat="server" TargetControlID="pnlRequestInfo"
                CollapsedSize="0" ExpandedSize="200" Collapsed="false" ExpandControlID="imgRequestInfo"
                CollapseControlID="imgRequestInfo" AutoCollapse="False" AutoExpand="False" ScrollContents="false"
                CollapsedText="Show Details..." ExpandedText="Hide Details" ImageControlID="imgRequestInfo"
                ExpandedImage="~/Images/expand_icon.jpg" CollapsedImage="~/Images/collapse_icon.jpg"
                ExpandDirection="Vertical">
            </asp:CollapsiblePanelExtender>
            <br />
        </div>
        <asp:ModalPopupExtender ID="serialmdpExtender" runat="server" TargetControlID="lnkbtnDummy"
            ClientIDMode="Static" PopupControlID="pnlAddSerials" BackgroundCssClass="modalBackground"
            CancelControlID="imgbtnClose" PopupDragHandleControlID="divpopHeader">
        </asp:ModalPopupExtender>
        <asp:Panel ID="pnlAddSerials" runat="server" Height="380px" Width="500px" CssClass="ModalWindow">
            <div class="popUpHeader" id="divpopHeader">
                <div style="float: left; width: 80%">
                    Add Items </div>
                <div style="float: left; width: 20%; text-align: right">
                    <asp:ImageButton ID="imgbtnClose" runat="server" ImageUrl="~/Images/0032-01-16x16x16.ICO" />&nbsp;
                </div>
            </div>
            <div style="float: left; width: 100%">
                <br />
                <div class="MainDivCss">
                    <uc1:uc_MsgInfo ID="uc_SerialPopUpMsgInfo" runat="server" />
                </div>
                <div align="right">
                    <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="Button" OnClick="btnAdd_Click" />&nbsp;&nbsp;
                </div>
                <div class="MainDivCss">
                    Item Code : &nbsp;<asp:Label ID="lblmpeItemCode" runat="server" CssClass="commonDDLCss"></asp:Label>
                    <asp:HiddenField ID="hdnSelectedReqNo" runat="server" />
                </div>
                <div class="MainDivCss">
                    Req Qty : &nbsp;<asp:Label ID="lblReqQty" runat="server" CssClass="commonDDLCss"></asp:Label>&nbsp;&nbsp;
                   
                    Actual Qty: &nbsp;<asp:Label ID="lblActQty_" runat="server" CssClass="commonDDLCss"></asp:Label>
                </div>
                <br />
                <br />
                <div class="MainDivCss">
                    <div class="SubDivCss">
                        <div class="innerLeftDivCss">
                            Bin Code :</div>
                        <div class="innerRightDivCss">
                            <asp:DropDownList ID="ddlmpeBinCode" runat="server" CssClass="commonDDLCss">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="SubDivCss">
                        <div class="innerLeftDivCss">
                            Status :</div>
                        <div class="innerRightDivCss">
                            <asp:DropDownList ID="ddlmpeItemStatus" runat="server" CssClass="commonDDLCss">
                                <asp:ListItem Text="CONS" Value="CONS" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br />               
                <div class="MainDivCss" id="divNonSerial" runat="server" visible="false">
                    Add to Actual Qty : &nbsp;<asp:TextBox ID="txtActualQty" runat="server" CssClass="commonDDLCss"
                        Width="75px"></asp:TextBox>
                </div>
                <div style="float:left;width:100%" id="divSerial" runat="server" visible="false">
                <div class="MainDivCss">
                    Serial No 1 : &nbsp;<asp:TextBox ID="txtSerialNo1" runat="server" CssClass="commonDDLCss"
                        Width="150px" MaxLength="40"></asp:TextBox>
                </div>
                <div class="MainDivCss">
                    Serial No 2 : &nbsp;<asp:TextBox ID="txtSerialNo2" runat="server" CssClass="commonDDLCss"
                        Width="150px" MaxLength="40"></asp:TextBox>
                </div>
                <div class="MainDivCss">
                    Serial No 3 : &nbsp;<asp:TextBox ID="txtSerialNo3" runat="server" CssClass="commonDDLCss"
                        Width="150px" MaxLength="40"></asp:TextBox>
                    &nbsp;
                </div>
                <div class="MainDivCss">
                 <br />
                    <asp:Panel ID="pnlItemSerials" runat="server" Height="150px" Width="475px" ScrollBars="Auto">
                        <asp:GridView ID="gvItemSerials" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" GridLines="None" OnRowCommand="gvItemSerials_RowCommand">
                            <Columns>
                              
                                <asp:BoundField DataField="Tus_usrseq_no" HeaderText=" " />
                                <asp:BoundField DataField="Tus_ser_1" HeaderText="Serial No1" />
                                <asp:BoundField DataField="Tus_ser_2" HeaderText="Serial No2" />
                                <asp:BoundField DataField="Tus_ser_3" HeaderText="Serial No3" />
                                <asp:BoundField DataField="Tus_itm_stus" HeaderText="Item Status" />
                                <asp:BoundField DataField="Tus_bin" HeaderText="Bin Code" />
                                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="55px"
                                    ShowHeader="true">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtndelSerial" runat="server" ImageUrl="~/Images/Delete.png"
                                            CommandName="DeleteItem" CommandArgument='<%# Eval("Tus_ser_id") + "|" + Eval("Tus_itm_cd") + "|" + Eval("Tus_ser_1") + "|" + Eval("Tus_bin")  %>'
                                            OnClientClick="return DeleteConfirm()" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                            </Columns>
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </asp:Panel>
                </div>
                </div> 
                <br /><br />            
                <div class="MainDivCss">
                    <%--<asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="Button" OnClick="btnAdd_Click" />--%>
                    &nbsp;&nbsp;<asp:Button ID="btnSubmit" runat="server" Text="Done" CssClass="Button" OnClick="btnSubmit_Click" Visible="false" />
                </div>
            </div>
            <asp:LinkButton ID="lnkbtnDummy" runat="server"></asp:LinkButton>
            <asp:HiddenField ID="hdnUnitPrice" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="hdnLineNo" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="hdngvRowIndex" runat="server"></asp:HiddenField>
        </asp:Panel>
        <br />
    </div>
    <div style="float: left; width: 3%">
        &nbsp;
    </div>
</asp:Content>
