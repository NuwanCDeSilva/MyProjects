<%@ Page Title="Stock Request Note" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="MRN_Page.aspx.cs" Inherits="FF.WebERPClient.Inventory_Module.MRN_Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControls/uc_CommonSearch.ascx" TagName="uc_CommonSearch"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <%--javascript--%>
    <script type="text/javascript">
        //-------------------------------------GetItemData()------------------------------------------------------//
        function GetItemData() {
            var itemCode = document.getElementById("txtSearchItemCode").value;
            itemCode = itemCode.toUpperCase();
            if (itemCode != "") {
                FF.WebERPClient.LocalWebServices.CommonSearchWebServive.GetAllItemDetailsByItemCode(itemCode, onGetItemDataPass, onGetItemDataFail);
            }
            else { ClearItemFields(); }
        }
        //SucceededCallback method.
        function onGetItemDataPass(result) {
            if (result != null) {
                // alert("Code :" + result.Mi_cd + " Desc : " + result.Mi_longdesc + " Brand :" + result.Mi_brand + " Model : " + result.Mi_model);
                document.getElementById("txtItemDescription").value = result.Mi_longdesc;
                document.getElementById("txtModelNo").value = result.Mi_model;
                document.getElementById("txtBrand").value = result.Mi_brand;
                document.getElementById("hdnIsSubItem").value = result.Mi_is_subitem;
                document.getElementById("txtAvalQty").value = "0";
                document.getElementById("txtFreeQty").value = "0";
                document.getElementById("txtQty").value = "";
                document.getElementById("txtRemarks").value = "";
                document.getElementById('<%=ddlItemStatus.ClientID%>').options.value = "GOD";
                document.getElementById('<%=ddlItemStatus.ClientID%>').onchange();
                document.getElementById('<%=LinkButtonTemp.ClientID%>').click();
                
            }
            else { ClearItemFields(); }

        }
        //FailedCallback method.
        function onGetItemDataFail(error) {
            ClearItemFields();
        }

        function ClearItemFields() {
            document.getElementById("txtItemDescription").value = "";
            document.getElementById("txtModelNo").value = "";
            document.getElementById("txtBrand").value = "";
            document.getElementById("txtAvalQty").value = "0";
            document.getElementById("txtFreeQty").value = "0";
            document.getElementById("txtQty").value = "";
            document.getElementById("txtRemarks").value = "";
            document.getElementById('<%=ddlItemStatus.ClientID%>').options.value = "GOD";
            document.getElementById("txtSearchItemCode").value = "";
        }

        //-------------------------------------------GetInvoiceData()-------------------------------------------------//

        function GetInvoiceData() {
            var invoiceNo = document.getElementById("txtInvoiceNo").value;
            if (invoiceNo != "") {
                FF.WebERPClient.LocalWebServices.CommonSearchWebServive.GetAllInvoiceDetailsByInvoiceNo(invoiceNo, onGetInvoiceDataPass, onGetInvoiceDataFail);
            }
            else { ClearInvoiceFields(); }
        }
        //SucceededCallback method.
        function onGetInvoiceDataPass(result) {
            if (result != null) {
                // alert("Code :" + result.Mi_cd + " Desc : " + result.Mi_longdesc + " Brand :" + result.Mi_brand + " Model : " + result.Mi_model);
                document.getElementById("txtCustomerCode").value = result.Sah_cus_cd;
                document.getElementById("txtCustomerName").value = result.Sah_cus_name;
                document.getElementById("txtAddress1").value = result.Sah_cus_add1;
                document.getElementById("txtAddress2").value = result.Sah_cus_add2;
            }
            else { ClearInvoiceFields(); }

        }
        //FailedCallback method.
        function onGetInvoiceDataFail(error) {
            ClearInvoiceFields();
        }

        function ClearInvoiceFields() {
            document.getElementById("txtCustomerCode").value = ""; document.getElementById("txtCustomerName").value = "";
            document.getElementById("txtAddress1").value = ""; document.getElementById("txtAddress2").value = "";
        }

        //------------------------------------------------------------------------------------------------------//
        function DeleteConfirm() {
            if (confirm("Are you sure to delete?")) {
                return true;
            }
            else {
                return false;
            }
        }

        function EditConfirm(rowNo) {
            alert("Row : " + rowNo);
        }

        function ChangeRequestType() {
            var selectedType = document.getElementById("ddlRequestType").value.toUpperCase();
            var divInvoiceData = document.getElementById("divInvoiceData");

            if (selectedType == "CUST") {
                divInvoiceData.style.visibility = "visible"; //to show it
            }
            else {
                divInvoiceData.style.visibility = "hidden"; //to hide it
            }
        }

        function CheckCharacterCount(text, long) {
            var maxlength = new Number(long); // Change number to your max length.
            if (document.getElementById('txtInvReqRemarks').value.length > maxlength) {
                text.value = text.value.substring(0, maxlength);
                //alert(" Only " + long + " chars");
            }
        }

        function checkdecimalValues(e, long) {
            var unicode = e.charCode ? e.charCode : e.keyCode
            var inputString = document.getElementById('txtQty').value;
            if (long != 0) {
                if ((unicode >= 48 && unicode <= 57) || unicode == 46) {
                    if (inputString.indexOf('.') != -1) {
                        if (unicode == 45) {
                            return false;
                        }
                        else {
                            var stringAfterDecimal = inputString.substring(inputString.indexOf('.') + 1);
                            if (stringAfterDecimal.length >= long) {
                                return false;
                            }
                        }
                    }
                    else {
                        return true;
                    }
                }
                else {
                    return false;
                }
            }
            else {
                if ((unicode >= 48 && unicode <= 57)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }

    </script>
    <%--javascript--%>
    <link href="../MainStyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../MainStyleSheet.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="float: left; width: 3%">
        &nbsp;
    </div>
    <div style="float: left; width: 94%">
        <asp:TabContainer ID="tcMRNContainer" runat="server" ActiveTabIndex="0" Height="500px">
            <%--MRN Entering Tab--%>
            <asp:TabPanel ID="tbpMRNDataEntry" HeaderText="Request Data Entry" runat="server">
                <ContentTemplate>
                    <asp:UpdatePanel ID="updPnlMRNDataEntry" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <%--Selected Request No--%>
                            <div style="float: left; width: 40%; text-align: left; font-family: Verdana; font-size: 11px">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblSelectedReqNo" runat="server"></asp:Label>
                                <asp:HiddenField ID="hdnSelectedReqNo" runat="server" />
                            </div>
                            <%--Selected Request No--%>
                            <%--Button Panel--%>
                            <div style="float: right; width: 60%; text-align: right">
                                <asp:Button ID="btnMRNSave" runat="server" Text="Save" CssClass="Button" OnClick="btnMRNSave_Click" />
                                <asp:Button ID="btnMRNCancel" runat="server" Text="Cancel" CssClass="Button" OnClick="btnMRNCancel_Click" />
                                <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="Button" OnClick="btnMRNPrint_Click" />
                                <asp:Button ID="btnMRNClear" runat="server" Text="Clear" CssClass="Button" OnClick="btnMRNClear_Click" />
                                <asp:Button ID="btnMRNClose" runat="server" Text="Close" CssClass="Button" OnClick="btnMRNClose_Click" />
                            </div>
                            <%--Button Panel--%>
                            <%--Request Header Area--%>
                            <div style="float: left; width: 100%; padding-left: 25px; padding-right: 25px; font-family: Verdana;
                                font-size: 11px;">
                                <%--<div class="MainDivCss">--%>
                                <div style="float: left; width: 52%;">
                                    <%--<div class="SubDivCss">--%>
                                    <div class="innerLeftDivCss">
                                        Request Type . . . . . .</div>
                                    <div class="innerRightDivCss">
                                        <asp:DropDownList ID="ddlRequestType" runat="server" CssClass="ComboBox" AppendDataBoundItems="True"
                                            Width="160px" AutoPostBack="True" ClientIDMode="Static" OnSelectedIndexChanged="ddlRequestType_SelectedIndexChanged"
                                            Enabled="False">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="innerLeftDivCss">
                                        Request Sub Type . . . .</div>
                                    <div class="innerRightDivCss">
                                        <asp:DropDownList ID="ddlRequestSubType" runat="server" CssClass="ComboBox" Width="160px"
                                            AppendDataBoundItems="True">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="innerLeftDivCss">
                                        Dispatch Requried . . . .</div>
                                    <div class="innerRightDivCss">
                                        <asp:TextBox ID="txtDispatchRequried" runat="server" CssClass="TextBoxUpper" Width="135px"></asp:TextBox>
                                        <asp:ImageButton ID="imgDispatchRequried" runat="server" ImageUrl="~/Images/icon_search.png"
                                            OnClick="imgDispatchRequried_Click" />
                                    </div>
                                    <div class="innerLeftDivCss">
                                        Request Date . . . . .
                                    </div>
                                    <div class="innerRightDivCss">
                                        <asp:TextBox ID="txtRequestDate" runat="server" CssClass="TextBox" Width="135px"
                                            Enabled="false"></asp:TextBox>
                                        <asp:Image ID="imgRequestDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                        <asp:CalendarExtender ID="RequestDateCalExtender" runat="server" TargetControlID="txtRequestDate"
                                            PopupButtonID="imgRequestDate" Enabled="True" Format="dd/MMM/yyyy">
                                        </asp:CalendarExtender>
                                    </div>
                                    <div class="innerLeftDivCss">
                                        Requried Date . . . . . .
                                    </div>
                                    <div class="innerRightDivCss">
                                        <asp:TextBox ID="txtRequriedDate" runat="server" CssClass="TextBox" Width="135px"
                                            Enabled="false"></asp:TextBox>
                                        <asp:Image ID="imgRequriedDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                        <asp:CalendarExtender ID="RequriedDateCalExtender" runat="server" TargetControlID="txtRequriedDate"
                                            PopupButtonID="imgRequriedDate" Enabled="True" Format="dd/MMM/yyyy">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                                <div style="float: left; width: 48%;" id="divInvoiceData" clientidmode="Static" runat="server">
                                    <asp:Panel ID="PnaelGrid" runat="server" ScrollBars="Auto" Width="100%" Height="100px">
                                        <asp:GridView ID="GridViewItems" runat="server" Width="95%" EmptyDataText="No data found"
                                            ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                                            CssClass="GridView" GridLines="None"
                                            >
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBoxGridSelect" runat="server" Checked="true" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="20px" Height="15px" />
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Loc" DataField="LOCATION_CODE " />
                                                <asp:BoundField HeaderText="Des" DataField="Description" />
                                                <asp:BoundField HeaderText="Item" DataField="ITEM_CODE" />
                                                <asp:BoundField HeaderText="Status" DataField="STATUS" />
                                                <asp:BoundField HeaderText="Qty Hnd" DataField="QTY_IN_HAND" />
                                                <asp:BoundField HeaderText="Free Qty" DataField="FREE_QTY" />
                                                <asp:BoundField HeaderText="Res Qty" DataField="RESERVED_QTY " />

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
                                        <asp:LinkButton ID="LinkButtonTemp" runat="server" 
                                            onclick="LinkButtonTemp_Click"></asp:LinkButton>
                                    </asp:Panel>
                                    <div style="display: none">
                                        <div class="innerLeftDivCss">
                                            Invoice No :</div>
                                        <div class="innerRightDivCss">
                                            <asp:TextBox ID="txtInvoiceNo" runat="server" CssClass="commonDDLCss" ClientIDMode="Static"></asp:TextBox>
                                            <asp:ImageButton ID="imgInvoiceNo" runat="server" ImageUrl="~/Images/icon_search.png"
                                                OnClick="imgInvoiceNo_Click" />
                                        </div>
                                        <div class="innerLeftDivCss">
                                            Customer Code :</div>
                                        <div class="innerRightDivCss">
                                            <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="commonDDLCss" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                        <div class="innerLeftDivCss">
                                            Customer Name :</div>
                                        <div class="innerRightDivCss">
                                            <asp:TextBox ID="txtCustomerName" runat="server" CssClass="commonDDLCss" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                        <div class="innerLeftDivCss">
                                            Address 1 :</div>
                                        <div class="innerRightDivCss">
                                            <asp:TextBox ID="txtAddress1" runat="server" CssClass="commonDDLCss" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                        <div class="innerLeftDivCss">
                                            Address 2 :</div>
                                        <div class="innerRightDivCss">
                                            <asp:TextBox ID="txtAddress2" runat="server" CssClass="commonDDLCss" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%--Request Header Area--%>
                            <%--Add Item Area 1--%>
                            <div class="MainDivCss">
                                <br />
                                <div style="width: 15%; float: left">
                                    Item Code :<br />
                                    <asp:TextBox ID="txtSearchItemCode" runat="server" Width="70%" ClientIDMode="Static"
                                        CssClass="TextBoxUpper" MaxLength="20"></asp:TextBox>
                                    <asp:HiddenField ID="hdnIsSubItem" runat="server" ClientIDMode="Static" />
                                    <asp:ImageButton ID="ImgItemSearch" runat="server" ImageUrl="~/Images/icon_search.png"
                                        OnClick="ImgItemSearch_Click" />
                                </div>
                                <div style="width: 40%; float: left">
                                    Description :<br />
                                    <asp:TextBox ID="txtItemDescription" runat="server" Width="80%" Enabled="False" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <div style="width: 20%; float: left">
                                    Brand :<br />
                                    <asp:TextBox ID="txtBrand" runat="server" Width="70%" Enabled="False" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <div style="width: 20%; float: left">
                                    Model Number:<br />
                                    <asp:TextBox ID="txtModelNo" runat="server" Width="70%" Enabled="False" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <%--Add Item Area 1--%>
                            <%--Add Item Area 2--%>
                            <div class="MainDivCss">
                                <div style="width: 15%; float: left">
                                    Item Status :<br />
                                    <asp:DropDownList ID="ddlItemStatus" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                        CssClass="ComboBox" OnSelectedIndexChanged="ddlItemStatus_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div style="width: 15%; float: left">
                                    Reservation No :<br />
                                    <asp:TextBox ID="txtReservationNo" runat="server" Width="100px" ReadOnly="True" Text="N/A"
                                        CssClass="TextBoxUpper"></asp:TextBox>
                                </div>
                                <div style="width: 10%; float: left">
                                    Qty :<br />
                                    <asp:TextBox ID="txtQty" runat="server" CssClass="TextBoxNumeric" Width="50px" MaxLength="10"
                                        onkeypress="return checkdecimalValues(event,4)" ClientIDMode="Static"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="ajaxNumericText" runat="server" TargetControlID="txtQty"
                                        FilterType="Custom, Numbers" ValidChars="." />
                                </div>
                                <div style="width: 10%; float: left">
                                    Aval Qty :<br />
                                    <asp:TextBox ID="txtAvalQty" runat="server" CssClass="TextBoxNumeric" Width="50px"
                                        Enabled="False" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <div style="width: 10%; float: left">
                                    Free Qty :<br />
                                    <asp:TextBox ID="txtFreeQty" runat="server" CssClass="TextBoxNumeric" Width="50px"
                                        Enabled="False" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <div style="width: 40%; float: left">
                                    Remarks :<br />
                                    <asp:TextBox ID="txtRemarks" runat="server" Width="275px" MaxLength="200" CssClass="TextBox"
                                        ClientIDMode="Static"></asp:TextBox>
                                    &nbsp;<%--<asp:Button ID="btnAddItem" runat="server" Text="Add" CssClass="Button" 
                                        onclick="btnAddItem_Click" />--%><asp:ImageButton ID="btnAddItemNew" runat="server"
                                            ImageUrl="~/Images/download_arrow_icon.png" OnClick="btnAddItem_Click" Height="16px"
                                            Width="16px" ToolTip="Add Item" />
                                </div>
                            </div>
                            <%--Add Item Area 2--%>
                            <%--Request Item Grid--%>
                            <div class="MainDivCss">
                                <br />
                                <asp:Panel ID="pnlGridView" runat="server" ScrollBars="Auto" Height="200px" Width="850px">
                                    <asp:GridView ID="gvMRNItems" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        ForeColor="#333333" OnRowCommand="gvMRNItems_RowCommand" OnRowDataBound="gvMRNItems_RowDataBound"
                                        CssClass="GridView" EmptyDataText="No data found" ShowHeaderWhenEmpty="True"
                                        Width="100%">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="ItemCode" HeaderStyle-HorizontalAlign="Left" ShowHeader="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MasterItem.Mi_cd") %>'></asp:Label>
                                                    <asp:HiddenField ID="hdnMstItemCode" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Itri_mitm_cd") %>' />
                                                    <asp:HiddenField ID="hdnMstItemStatus" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Itri_mitm_stus") %>' />
                                                    <asp:HiddenField ID="hdnMstQty" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Itri_mqty") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="75px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Left" ShowHeader="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MasterItem.Mi_longdesc") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Model" HeaderStyle-HorizontalAlign="Left" ShowHeader="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblModel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MasterItem.Mi_model") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="75px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Brand" HeaderStyle-HorizontalAlign="Left" ShowHeader="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBrand" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MasterItem.Mi_brand") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="75px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Status" HeaderStyle-HorizontalAlign="Left" ShowHeader="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Itri_itm_stus") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="75px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reservation No" HeaderStyle-HorizontalAlign="Left"
                                                ShowHeader="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReservationNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Itri_res_no") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="75px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks" HeaderStyle-HorizontalAlign="Left" ShowHeader="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRemarks" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Itri_note") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="200px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Req Qty" HeaderStyle-HorizontalAlign="Right" ShowHeader="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReqQty" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Itri_qty") %>'
                                                        Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" Width="30px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="App Qty" HeaderStyle-HorizontalAlign="Right" ShowHeader="true"
                                                Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtAppQty" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Itri_app_qty") %>'
                                                        CssClass="numericFieldCss"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" Width="30px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Select" ShowHeader="true" Visible="false">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chbSelect" runat="server" /></ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="75px"
                                                ShowHeader="true">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnlineNo" runat="server" Value='<%# Eval("Itri_line_no") %>' />
                                                    <asp:ImageButton ID="imgbtnEdit" runat="server" ImageUrl="~/Images/EditIcon.png"
                                                        CommandName="EditItem" CommandArgument='<%# Eval("Itri_line_no") %>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="75px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="75px"
                                                ShowHeader="true">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtndelete" runat="server" ImageUrl="~/Images/Delete.png"
                                                        CommandName="DeleteItem" CommandArgument='<%# Eval("MasterItem.Mi_cd") %>' OnClientClick="return DeleteConfirm()" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="75px" />
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
                            <%--Request Item Grid--%>
                            <%--Footer Area--%>
                            <div class="MainDivCss">
                                <div class="SubDivCss">
                                    <div class="innerLeftDivCss">
                                        Request By . . .
                                    </div>
                                    <div class="innerRightDivCss">
                                        <asp:TextBox ID="txtRequestBy" runat="server" CssClass="TextBox" Width="150px" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="innerLeftDivCss">
                                        Collector's NIC . . .</div>
                                    <div class="innerRightDivCss">
                                        <asp:TextBox ID="txtCollectorNIC" runat="server" CssClass="TextBoxUpper" Width="150px"
                                            MaxLength="10"></asp:TextBox>
                                    </div>
                                    <div class="innerLeftDivCss">
                                        Collector's Name . .</div>
                                    <div class="innerRightDivCss">
                                        <asp:TextBox ID="txtCollectorName" runat="server" CssClass="TextBox" Width="150px"
                                            MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="SubDivCss">
                                    <div class="innerLeftDivCss">
                                        Remarks . .
                                    </div>
                                </div>
                                <div class="innerRightDivCss">
                                    <asp:TextBox ID="txtInvReqRemarks" runat="server" TextMode="MultiLine" Rows="3" Width="350px"
                                        CssClass="TextBox" onKeyUp="javascript:CheckCharacterCount(this,250);" onChange="javascript:CheckCharacterCount(this,250);"
                                        ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <%--Footer Area--%>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </asp:TabPanel>
            <%--MRN Entering Tab--%>
            <%--MRN Searching Tab--%>
            <asp:TabPanel ID="tbpMRNList" HeaderText="Request Enquiry" runat="server">
                <ContentTemplate>
                    <asp:UpdatePanel ID="updPnlMRNList" runat="server">
                        <ContentTemplate>
                            <br />
                            <div class="MainDivCss">
                                <table border="0" cellpadding="2" cellspacing="0">
                                    <tr>
                                        <td style="width: 100px;">
                                            From :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFromDate" runat="server" Width="75px"></asp:TextBox>
                                            <asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate"
                                                Format="dd/MMM/yyyy" PopupButtonID="imgFromDate" Enabled="True">
                                            </asp:CalendarExtender>
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;To :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtToDate" runat="server" Width="75px"></asp:TextBox>
                                            <asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate"
                                                Format="dd/MMM/yyyy" PopupButtonID="imgToDate" Enabled="True">
                                            </asp:CalendarExtender>
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;<asp:ImageButton ID="imgbtnRequestSearch" runat="server" ImageUrl="~/Images/icon_search.png"
                                                OnClick="imgbtnRequestSearch_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px;">
                                            Status :
                                        </td>
                                        <td colspan="4">
                                            <asp:DropDownList ID="ddlRequestStatus" runat="server" CssClass="ComboBox">
                                                <asp:ListItem Text="Pending" Value="P"></asp:ListItem>
                                                <asp:ListItem Text="Approved" Value="A"></asp:ListItem>
                                                <asp:ListItem Text="" Value="X" Selected="True"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px;">
                                            Created User :
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chbCreatedUser" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="MainDivCss">
                                <br />
                                <br />
                                <%--Request Search Grid--%>
                                <asp:GridView ID="gvMRNList" runat="server" AutoGenerateColumns="False" OnRowCommand="gvMRNList_RowCommand"
                                    CellPadding="4" ForeColor="#333333" GridLines="Both" AllowPaging="True" PageSize="15"
                                    OnPageIndexChanging="gvMRNList_PageIndexChanging" CssClass="GridView" EmptyDataText="No data found"
                                    ShowHeaderWhenEmpty="True" Width="90%">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Req No">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtnReqNo" Text='<%# DataBinder.Eval(Container.DataItem, "Itr_req_no") %>'
                                                    ClientIDMode="Static" CommandName="SelectInvReq" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Itr_req_no") %>'
                                                    runat="server"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Company Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Itr_com") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <%--<asp:Label ID="lblReqStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Itr_stus").ToString().ToUpper().Equals("P") ? "Pending" : "Approved" %>'></asp:Label>--%>
                                                <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Itr_stus") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Itr_tp") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sub Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SubTpDesc") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" runat="server" Text='<%#  DataBinder.Eval(Container.DataItem, "Itr_dt", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle BackColor="White" />
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
                                <%--Request Search Grid--%>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="gvMRNList" />
                        </Triggers>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </asp:TabPanel>
            <%--MRN Searching Tab--%>
        </asp:TabContainer>
    </div>
</asp:Content>
