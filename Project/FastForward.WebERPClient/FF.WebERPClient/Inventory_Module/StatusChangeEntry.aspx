<%@ Page Title="Changes to Stock Status" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="StatusChangeEntry.aspx.cs" Inherits="FF.WebERPClient.WebForm4" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 510px;
        }
        .style4
        {
            width: 418px;
        }
        .style5
        {
            width: 418px;
            height: 25px;
        }
        .style6
        {
            width: 510px;
            height: 25px;
        }
        .style7
        {
            font-family: ver;
        }
        .style8
        {
            border: 1px solid Black;
            font-family: Verdana;
            font-size: 11px;
            background-color: Silver;
            margin-bottom: 0px;
        }
        .style9
        {
            font-family: Verdana;
            font-size: 11px;
            text-align: left;
        }
        .style10
        {
            border: 1px solid Black;
            font-size: 11px;
            font-family: Verdana;
        }
        .style11
        {
            text-align: right;
            width: 619px;
        }
        .style13
        {
            width: 7px;
        }
        .style14
        {
            width: 153px;
        }
    </style>
    <script type="text/javascript">
        function GetItemData() {
            var itemCode = document.getElementById("ddlItemCodes").value;
            if (itemCode != "") {
                FF.WebERPClient.LocalWebServices.CommonSearchWebServive.GetAllItemDetailsByItemCode(itemCode, onGetItemDataPass, onGetItemDataFail);
            }
            else { ClearItemFields(); }
        }
        //SucceededCallback method.
        function onGetItemDataPass(result) {
            if (result != null) {
                // alert("Code :" + result.Mi_cd + " Desc : " + result.Mi_longdesc + " Brand :" + result.Mi_brand + " Model : " + result.Mi_model);
                document.getElementById("lblItemCD").value = result.Mi_longdesc;
                //document.getElementById("txtModelNo").value = result.Mi_model;
                // document.getElementById("txtBrand").value = result.Mi_brand;
                // document.getElementById("hdnIsSubItem").value = result.Mi_is_subitem;
            }
            else { ClearItemFields(); }

        }
        //FailedCallback method.
        function onGetItemDataFail(error) {
            ClearItemFields();
        }

        function ClearItemFields() {
            //  document.getElementById("txtItemDescription").value = ""; document.getElementById("txtModelNo").value = "";
            //  document.getElementById("txtBrand").value = ""; document.getElementById("txtSearchItemCode").value = "";
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <%--<asp:Label ID="Label2" runat="server" Text=" Date...............:"></asp:Label>--%>
        <div>
            <asp:Panel ID="roolbarPanel" runat="server">
                 <div style="float: left; width: 99%; height: 22px; text-align: right; background-color: #1E4A9F">
                    <div style="float: left;">
                        <asp:Label ID="lblDispalyInfor" runat="server" Text="Back date allow for" CssClass="Label"
                            ForeColor="Yellow"></asp:Label>
                    </div>
                    <div style="float: right;">
                        <asp:Button ID="btnApproved" runat="server" CssClass="Button" OnClick="btnApproved_Click"
                            Text="Approved" Visible="False" />
                        <asp:Button ID="btnfinalSave" runat="server" CssClass="Button" OnClick="btnSave_Click"
                            Text="Save" />
                        <asp:Button ID="btnClear" runat="server" CssClass="Button" OnClick="btnClear_Click"
                            Text="Clear" />
                        <asp:Button ID="btnHome" runat="server" Text="Close" CssClass="Button" OnClick="btnHome_Click" />
                    </div>
                </div>
            </asp:Panel>
            <%--<asp:Label ID="Label2" runat="server" Text=" Date...............:"></asp:Label>--%>
            <div>
            </div>
            <div style="text-align: left">
                <span class="style9">&nbsp;<b>&nbsp;Scaned Batch</b>&nbsp; </span>
                <asp:DropDownList ID="ddlScanBatches" runat="server" Width="98px" AutoPostBack="True"
                    OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" CssClass="style9">
                </asp:DropDownList>
                &nbsp;<asp:Button ID="btnScanPick" runat="server" CssClass="style8" OnClick="btnScanPick_Click"
                    Text="Pick" Visible="False" />
                <span class="style9">&nbsp;&nbsp; &nbsp;<asp:Label ID="lblSeqno" runat="server" ForeColor="White"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;</span></div>
            <div>
                <asp:Panel ID="panelDisposeAll" runat="server" Height="84px" Width="327px" BorderColor="Blue"
                    GroupingText="Dispose all" CssClass="style9">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnDisposeOk" runat="server" CssClass="Button" Height="18px" OnClick="btnDisposeOk_Click"
                        Text="OK" Width="49px" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnDisposeCancel" runat="server" CssClass="Button" Height="18px"
                        OnClick="btnDisposeCancel_Click" Text="Cancel" Width="49px" />
                    <br />
                    <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Dispose All Items In Location</b><br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Location:&nbsp;
                    <asp:Label ID="lbl_Location" runat="server" CssClass="Label" Text="Label" Font-Bold="True"></asp:Label>
                    <div style="text-align: left">
                        <asp:Button ID="btnDisposeAll" runat="server" CssClass="style8" OnClick="btnAllDispose_Click"
                            Text="Dispose all" Visible="False" />
                        <span class="style9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </span>&nbsp;<span class="style9">&nbsp;&nbsp; &nbsp;<asp:Label ID="lblSeqno0" runat="server"
                            ForeColor="White"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;</span></div>
                    <br />
                    <br />
                </asp:Panel>
                <asp:Panel ID="PanelItemPopUp" runat="server" Height="359px" Width="642px" BackColor="#E6E6E6"
                    BorderColor="#E0E0E0" BorderWidth="2px" CssClass="style9">
                    <div>
                        <b>Search Header</b>&nbsp;
                    </div>
                    <div class="style11">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp; &nbsp;
                        <asp:Button ID="btnPopupOk" runat="server" CssClass="Button" OnClick="btnPopupOk_Click"
                            Text="Ok" Width="50px" />
                        &nbsp;<asp:Button ID="btnPopupCancel" runat="server" CssClass="Button" Text="Cancel"
                            OnClick="btnPopupCancel_Click" />
                    </div>
                    <div align="right" style="text-align: left">
                        <br />
                        &nbsp;Item Code:
                        <asp:Label ID="lblPopupItemCode" runat="server" Font-Bold="True"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Bin Code:&nbsp;&nbsp;<asp:Label ID="lblPopupBinCode"
                            runat="server" Font-Bold="True"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblPopupAmt" runat="server" ForeColor="#0033CC"
                            Style="text-align: right"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <br />
                    <br />
                    &nbsp;&nbsp;
                    <asp:Label ID="Label5" runat="server" Text="Search by"></asp:Label>
                    :
                    <asp:DropDownList ID="ddlPopupSerial" runat="server" Height="16px" Width="111px"
                        CssClass="style9">
                        <asp:ListItem>--select--</asp:ListItem>
                        <asp:ListItem>Serial No 1</asp:ListItem>
                        <asp:ListItem>Serial No 2</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtPopupSearchSer" runat="server" Width="138px" CssClass="TextBox"></asp:TextBox>
                    &nbsp; &nbsp;&nbsp;<asp:Button ID="btnPopupSarch" runat="server" CssClass="Button"
                        OnClick="btnPopupSarch_Click" Text="search" />
                    &nbsp;<asp:Label ID="lblPopupMsg" runat="server" ForeColor="Red"></asp:Label>
                    <br />
                    <br />
                    &nbsp;&nbsp;
                    <asp:Label ID="lblPopQty" runat="server" Text="Qty . . . . :" Visible="False"></asp:Label>
                    &nbsp;<asp:TextBox ID="txtPopupQty" runat="server" ClientIDMode="Static" CssClass="TextBoxNumeric"
                        Height="16px" Visible="False" Width="48px"></asp:TextBox>
                    &nbsp;
                    <asp:Button ID="btnPopupAutoSelect" runat="server" CssClass="Button" Height="16px"
                        OnClick="btnPopupAutoSelect_Click" OnClientClick="SelectAuto()" Text="Auto Select"
                        Width="85px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<br />
                    &nbsp;&nbsp;
                    <div style="width: 608px">
                        <asp:Panel ID="popupGridPanel" runat="server" Height="172px" ScrollBars="Vertical"
                            Style="margin-left: 15px; margin-bottom: 13px" Width="595px">
                            <asp:GridView ID="GridPopup" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                ForeColor="#333333" Height="45px" Width="671px">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkPopupSelectAll" runat="server" ClientIDMode="Static" onclick="SelectAll(this.id)" />
                                            All
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
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </asp:Panel>
            </div>
        </div>
    </div>
    <span class="style9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</span><div>
            <%--<asp:Label ID="Label2" runat="server" Text=" Date...............:"></asp:Label>--%>
            <asp:Panel ID="Panel_itemDesc" runat="server" GroupingText="Scan items" CssClass="style9"
                Height="118px" Width="959px">
                <table class="style1">
                    <tr>
                        <td class="style14">
                            <asp:Panel ID="Panel_serialSearch" runat="server" Width="288px" GroupingText="Search by serial">
                                <div style="width: 277px">
                                    Serial No 1..:
                                    <asp:TextBox ID="txtSerial1" runat="server" CssClass="style10"></asp:TextBox>
                                    &nbsp;&nbsp;<asp:Button ID="btnSearchSerial1" runat="server" CssClass="Button" OnClick="btnSearchSerial1_Click"
                                        Text="..." />
                                    &nbsp;&nbsp;&nbsp;<br />
                                    <br />
                                    Serial No 2..:
                                    <asp:TextBox ID="txtSerial2" runat="server" CssClass="style10"></asp:TextBox>
                                    &nbsp;&nbsp;<asp:Button ID="btnSearchSerial2" runat="server" CssClass="Button" OnClick="btnSearchSerial2_Click"
                                        Text="..." />
                                    &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    &nbsp;&nbsp;&nbsp;
                                </div>
                            </asp:Panel>
                        </td>
                        <td class="style13">
                            &nbsp;
                        </td>
                        <td style="text-align: right">
                            <asp:Panel ID="Panel2" runat="server" GroupingText="Add Item" Style="text-align: justify">
                                <div>
                                    Bin code&nbsp;............:
                                    <asp:DropDownList ID="ddlBinCode" runat="server" CssClass="style9" Height="18px"
                                        Width="128px">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;Item Code..:<asp:TextBox ID="txtItemCD" runat="server" CssClass="TextBox"
                                        OnTextChanged="txtItemCD_TextChanged"></asp:TextBox>
                                    &nbsp;<asp:ImageButton ID="imgBtnItmCDSearch0" runat="server" ImageAlign="Bottom"
                                        ImageUrl="~/Images/icon_search.png" OnClick="imgBtnItmCDSearch_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="Label7" runat="server" Text=" Qty..:" Visible="False"></asp:Label>
                                    <asp:TextBox ID="txtQty0" runat="server" CssClass="style10" Height="16px" Visible="False"
                                        Width="57px"></asp:TextBox>
                                    &nbsp;<asp:Button ID="btnAddSearch" runat="server" CssClass="Button" Font-Bold="True"
                                        OnClick="btnAddSearch_Click" Text="..." />
                                    <br />
                                    <br />
                                    New Item Status...:
                                    <asp:DropDownList ID="ddlItemStatus" runat="server" CssClass="style9" Height="18px"
                                        OnSelectedIndexChanged="ddlItemStatus_SelectedIndexChanged" Width="128px">
                                    </asp:DropDownList>
                                    &nbsp;<asp:ImageButton ID="imgBtnItemStatus" runat="server" ImageUrl="~/Images/icon_search.png"
                                        OnClick="ImageButton1_Click" />
                                    &nbsp;&nbsp;<asp:Button ID="btnAdd0" runat="server" CssClass="Button" OnClick="btnAdd_Click"
                                        Text="ADD" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<asp:ModalPopupExtender ID="ModalPopupExtItem"
                                        runat="server" CancelControlID="btnPopupCancel" ClientIDMode="Static" PopupControlID="PanelItemPopUp"
                                        TargetControlID="btnHidden_popup">
                                    </asp:ModalPopupExtender>
                                    <asp:DropDownList ID="ddlItemCodes0" runat="server" AutoPostBack="True" CssClass="style9"
                                        Height="18px" OnSelectedIndexChanged="ddlItemCodes_SelectedIndexChanged" Visible="False"
                                        Width="132px">
                                    </asp:DropDownList>
                                </div>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                &nbsp;<asp:Label ID="lblDiscription" runat="server" BorderColor="Black" Style="color: #0000FF"></asp:Label>
                <br />
                <div>
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblSelectList"
                        runat="server" Text="Label" Visible="False"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblNewStatus" runat="server" Text="Label" Visible="False"></asp:Label>
                </div>
            </asp:Panel>
            <span class="style9">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div>
                            <asp:Panel ID="PanelGridDisplay" runat="server" Height="172px" CssClass="style9"
                                Width="98%">
                                <asp:Panel ID="Panel_gid_btns" runat="server" Height="30px" Width="100%">
                                    <div style="text-align: right">
                                        New&nbsp;Status:
                                        <asp:DropDownList ID="ddlStatusNew" runat="server" Height="16px" Width="90px" CssClass="style9">
                                            <asp:ListItem>Good</asp:ListItem>
                                            <asp:ListItem>Damage</asp:ListItem>
                                            <asp:ListItem>CLR_SALR</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:ImageButton ID="imgBtnStatusNew" runat="server" ImageUrl="~/Images/icon_search.png"
                                            OnClick="imgBtnStatusNew_Click" />
                                        &nbsp;
                                        <asp:Button ID="btnUpdateSelect" runat="server" CssClass="Button" OnClick="btnUpdateSelect_Click"
                                            Text="Update" />
                                        &nbsp;<span class="style9"><asp:Button ID="btnDeleteSelect0" runat="server" CssClass="Button"
                                            OnClick="btnDeleteSelect_Click" Text="Delete" />
                                        </span>&nbsp;<asp:Button ID="btnGridOK" runat="server" CssClass="Button" OnClick="btnGridOK_Click"
                                            Text="OK" Width="47px" />
                                        &nbsp;
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="Panel_containGrid" runat="server" Height="172px" ScrollBars="Both"
                                    BorderStyle="Solid" BorderWidth="1px">
                                    <asp:GridView ID="GridViewChanged_items" runat="server" AutoGenerateColumns="False"
                                        OnPageIndexChanging="GridViewChanged_items_PageIndexChanging2" CellPadding="2"
                                        ForeColor="#333333" BorderColor="Black" OnSelectedIndexChanged="GridViewChanged_items_SelectedIndexChanged"
                                        Width="945px">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkSelectAll" runat="server" onclick="SelectAll(this.id)" />
                                                    All
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="SelectCheck" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBox1_OnCheckedChanged" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="  Serial No1  " DataField="Tus_ser_1" />
                                            <asp:BoundField HeaderText="Serial No2" />
                                            <asp:BoundField HeaderText="Item Code" DataField="Tus_itm_cd" />
                                            <asp:BoundField DataField="Tus_bin" HeaderText="Bin Code" />
                                            <asp:BoundField HeaderText="Item Description" DataField="Tus_itm_desc" />
                                            <asp:BoundField HeaderText="Model" DataField="Tus_itm_model" />
                                            <asp:BoundField HeaderText="Item Status" DataField="Tus_itm_stus" />
                                            <asp:TemplateField HeaderText="Add Remarks">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                                    <asp:TextBox ID="txtNewRemark" runat="server" Text='<%# Bind("Tus_new_remarks") %>'
                                                        Style="font-size: 11px; font-family: Verdana" BackColor="#FFFFCC" BorderStyle="None"
                                                        MaxLength="20"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle CssClass="TextBox" />
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Changed status" DataField="Tus_new_status" />
                                            <asp:BoundField HeaderText="Qty" DataField="Tus_qty" />
                                            <asp:BoundField DataField="Tus_usrseq_no" HeaderText="Batch #" />
                                            <asp:TemplateField Visible="False">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="ser_id" runat="server" Text='<%# Bind("Tus_ser_id") %>'></asp:Label>
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
                            </asp:Panel>
                        </div>
                        <script language="javascript" type="text/javascript">

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

                                for (var i = 0; i < Elen; i++) {
                                    if (myform.elements[i].checked) {
                                        myform.elements[i].checked = false;
                                    }
                                }

                                //               document.getElementById(Id).checked == true ? document.getElementById(Id).checked = false : document.getElementById(Id).checked = true;
                                for (var i = 0; i < Elen; i++) {
                                    if (myform.elements[i].type == 'checkbox' && myform.elements[i].id != 'chkPopupSelectAll') {
                                        //&& myform.elements[i].id != 'chkSelectAll'
                                        //                       if (myform.elements[i] == document.getElementById("checkPopup") ) 
                                        //                       {//-----
                                        ////                        
                                        if (myform.elements[i].checked) {
                                            myform.elements[i].checked = false;
                                        }
                                        else {
                                            myform.elements[i].checked = true;
                                        }


                                        //                       }//------
                                        counter++;
                                        if (counter == len) {
                                            return;
                                        }
                                    }
                                }




                            }
                        </script>
                        <script language="javascript" type="text/javascript">

                            function SelectAll(Id) {
                                var myform = document.forms[0];
                                var len = myform.elements.length;
                                document.getElementById(Id).checked == true ? document.getElementById(Id).checked = false : document.getElementById(Id).checked = true;
                                for (var i = 0; i < len; i++) {
                                    if (myform.elements[i].type == 'checkbox')  //&& myform.elements[i].id != 'chkSelectAll'
                                    {

                                        if (myform.elements[i].checked)
                                        { myform.elements[i].checked = false; }
                                        else
                                        { myform.elements[i].checked = true; }


                                    }

                                    //var outChek = GridView.getElementsByTagName("chkSelectAll");

                                }
                            }
                        </script>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div>
                    <br />
                    <br />
                    <span class="style9">
                        <br />
                        <asp:Panel ID="Panel_final_state" runat="server" GroupingText=" Final fillings" Height="105px"
                            Width="365px" CssClass="style9">
                            <table class="style1">
                                <tr>
                                    <td class="style5">
                                        Date.............:
                                        <%--<asp:Label ID="Label2" runat="server" Text=" Date...............:"></asp:Label>--%>
                                    </td>
                                    <td class="style6">
                                        <asp:TextBox ID="txtDate" runat="server" CssClass="TextBox" Height="19px" Width="114px"
                                            Enabled="true"></asp:TextBox>
                                        <asp:Image ID="imgRequestDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png"
                                            Visible="False" Width="16px" />
                                        <asp:CalendarExtender ID="CalendarExt" runat="server" Animated="true" EnabledOnClient="true"
                                            Format="dd/MMM/yyyy" PopupButtonID="imgRequestDate" PopupPosition="BottomLeft"
                                            TargetControlID="txtDate">
                                        </asp:CalendarExtender>
                                        &nbsp;<asp:Label ID="Label8" runat="server" Text="dd/MMM/yyyy"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style4">
                                        <asp:Label ID="Label3" runat="server" Text="Manual Ref.... :"></asp:Label>
                                    </td>
                                    <td class="style2">
                                        <asp:TextBox ID="txtManualRefNo" runat="server" CssClass="TextBox" Height="20px"
                                            Width="236px" MaxLength="30"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style4">
                                        <asp:Label ID="Label4" runat="server" Text="Remarks ...... :"></asp:Label>
                                    </td>
                                    <td class="style2">
                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="TextBox" Width="236px" MaxLength="25"
                                            Height="21px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            &nbsp;
                            <br />
                        </asp:Panel>
                    </span>
                </div>
                <div style="display: none">
                    <span class="style9">
                        <br class="style7" />
                        <span class="style7">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <br />
                            &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><asp:Button
                                ID="btnHidden_popup" runat="server" Text="Button" CssClass="style9" />
                    <span class="style9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <br />
                </div>
        </div>
    <br />
    </span></span>
</asp:Content>
