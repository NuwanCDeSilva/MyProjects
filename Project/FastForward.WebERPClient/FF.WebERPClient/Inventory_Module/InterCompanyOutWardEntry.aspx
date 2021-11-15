<%@ Page Title="Inter-Company Outward Entry" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="InterCompanyOutWardEntry.aspx.cs" Inherits="FF.WebERPClient.Inventory_Module.InterCompanyOutWardEntry"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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


        //Developed by Prabhath Wijetunge - on 15 03 2012
        //Allow only numaric and decimal values
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

        function pageLoad(sender, args) {
            smoothAnimation();
        }


        function smoothAnimation() {
            var collPanel = $find("CPEPending");
            collPanel._animation._fps = 45;
            collPanel._animation._duration = 0.70;
        }




    </script>
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

            for (var i = 0; i < Elen; i++) {
                if (myform.elements[i].checked) {
                    myform.elements[i].checked = false;
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
    </script>

    <script language="javascript" type="text/javascript">

            function SelectAll(Id) {
                var myform = document.forms[0];
                var len = myform.elements.length;
                document.getElementById(Id).checked == true ? document.getElementById(Id).checked = false : document.getElementById(Id).checked = true;
                for (var i = 0; i < len; i++) {
                    if (myform.elements[i].type == 'checkbox')
                    {

                        if (myform.elements[i].checked)
                        { myform.elements[i].checked = false; }
                        else
                        { myform.elements[i].checked = true; }


                    }

         

                }
            }
       </script>

           <script  type="text/javascript">
               function ToUpper(ctrl) {
                   var t = ctrl.value;
                   ctrl.value = t.toUpperCase();
               }
               function ToLower(ctrl) {
                   var t = ctrl.value;
                   ctrl.value = t.toLowerCase();
               }
    </script>

    <asp:UpdatePanel ID="uplMain" runat="server">
        <ContentTemplate>
            <%--Whole Page--%>
            <div style="float: left; width: 100%; color: Black;">
                <%--Button Panel--%>
                <div style="float: left; width: 100%; text-align: right; padding-top: 2px;">
                    <asp:Label runat="server" Text="" ID="lblDispalyInfor"></asp:Label>
                    <asp:Button ID="btnSave" runat="server" Text="Process" Width="70px" CssClass="Button" OnClick="Process" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" Width="70px" CssClass="Button" OnClick="btnClear_Click" />
                    <asp:Button ID="btnClose" runat="server" Text="Close" Width="70px" OnClick="Close"
                        CssClass="Button" />
                </div>
                <%--Search Area--%>
                <div style="float: left; width: 100%; padding-top: 2px;">
                    <%-- Collaps Header - Pending Documents --%>
                    <div style="height: 18px; background-color: #1E4A9F; color: White; width: 98%; float: left;">
                        Pending Outward Entries
                    </div>
                    <%-- Collaps Image - Pending Documents --%>
                    <div style="float: left;">
                        <asp:Image runat="server" ID="Image1" ImageUrl="~/Images/16 X 16 DownArrow.jpg" ImageAlign="Right" /></div>
                    <%-- Collaps control - Pending Documents --%>
                    <asp:CollapsiblePanelExtender ID="CPEPending" runat="server" TargetControlID="pnlPending"
                        CollapsedSize="0" ExpandedSize="151" Collapsed="True" ExpandControlID="Image1"
                        CollapseControlID="Image1" AutoCollapse="False" AutoExpand="False" ScrollContents="false"
                        ExpandDirection="Vertical" ImageControlID="Image1" ExpandedImage="~/Images/16 X 16 UpArrow.jpg"
                        CollapsedImage="~/Images/16 X 16 DownArrow.jpg" ClientIDMode="Static">
                    </asp:CollapsiblePanelExtender>
                    <%-- Collaps Area - Pending Documents --%>
                    <div style="width: 100%; float: left; color: Black; padding-top: 2px; padding-bottom: 2px;
                        background-color: #EEEEEE;">
                        <asp:Panel runat="server" ID="pnlPending" Width="99%" BorderColor="#9F9F9F" BorderWidth="1px"
                            Font-Bold="true" ClientIDMode="Static">
                            <div style="float: left; width: 100%; font-weight: normal; padding-top: 1px; padding-bottom: 1px;">
                                <%-- Collaps Area - Searching --%>
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 10%;">
                                        Type . . . . . . . .
                                    </div>
                                    <div style="float: left; width: auto;">
                                        <asp:DropDownList ID="ddlType" runat="server" CssClass="ComboBox">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 10%;">
                                        From . . . . . . .
                                    </div>
                                    <div style="float: left; width: 12%;">
                                        <asp:TextBox ID="txtFrom" runat="server" Width="70%" Enabled="false" CssClass="TextBox"></asp:TextBox>
                                        <asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png"
                                            ImageAlign="Middle" />
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 10%;">
                                        To . . . . . . . . .
                                    </div>
                                    <div style="float: left; width: 12%;">
                                        <asp:TextBox ID="txtTo" runat="server" Width="70%" Enabled="false" CssClass="TextBox"></asp:TextBox>
                                        <asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png"
                                            ImageAlign="Middle" />
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 1%;">
                                        <asp:ImageButton runat="server" ID="imgBtnSearch" ImageUrl="~/Images/icon_search.png"
                                            ImageAlign="Middle" OnClick="SearchRequest" /></div>
                                </div>
                                <%-- Collaps Area - Result --%>
                                <div style="float: left; width: 100%;">
                                    <asp:Panel ID="pnlPendingDoc" runat="server" ScrollBars="Auto" BorderColor="#9F9F9F"
                                        BorderWidth="1px" Font-Bold="true" Height="130px">
                                        <asp:GridView ID="gvPending" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                                            DataKeyNames="itr_req_no,itr_com,itr_rec_to,itr_tp" OnRowDataBound="OnPendingRequestBind"
                                            OnSelectedIndexChanged="BindSelectedMRNDetail" CellPadding="4" CssClass="GridView"
                                            GridLines="Both">
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Size="12px" Font-Bold="True" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle Font-Size="12px" BackColor="#EFF3FB" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField='itr_req_no' HeaderText='Request No' HeaderStyle-Width="120px"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="180px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField='itr_dt' HeaderText='Date' HeaderStyle-Width="75px" DataFormatString="{0:d}"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField='itr_tp' HeaderText='Entry Type' HeaderStyle-Width="75px"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="125px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField='itr_ref' HeaderText='Ref. No' HeaderStyle-Width="75px"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="125px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField='itr_com' HeaderText='Request Company' HeaderStyle-Width="75px"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField='itr_rec_to' HeaderText='Request Location' HeaderStyle-Width="75px"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <%--General--%>
                <div style="height: 18px; background-color: #1E4A9F; color: White; width: 100%; float: left;">
                    General
                    <div style="float: right; padding-right: 10px;">
                        <asp:CheckBox ID="chkDirect" runat="server" Text="Direct" OnCheckedChanged="chkDirect_CheckChange"
                            AutoPostBack="true" />
                    </div>
                </div>
                <div style="float: left; width: 100%; padding-top: 2px;">
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 16%;">
                        Date . . . . . . . . . . . . . . .</div>
                    <div style="float: left; width: 16%;">
                        <asp:TextBox ID="txtDate" runat="server" CssClass="TextBox" Width="62%" Enabled="false"></asp:TextBox>
                        <asp:Image ID="imgDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" Visible="false"
                            ImageAlign="Middle" /></div>
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 16%;">
                        Receiving Company . . . .
                    </div>
                    <div style="float: left; width: 16%;">
                        <asp:DropDownList ID="DDLRecCompany" runat="server" Width="80%" AutoPostBack="true"
                            CssClass="ComboBox" AppendDataBoundItems="True" OnSelectedIndexChanged="ReceiveCompany_OnChange">
                        </asp:DropDownList>
                    </div>
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 16%;">
                        Vehicle No . . . . . . . . . .
                    </div>
                    <div style="float: left; width: 16%;">
                        <asp:TextBox ID="txtVehicle" runat="server" CssClass="TextBoxUpper" MaxLength="9" onchange="ToUpper(this)" ></asp:TextBox>
                    </div>
                </div>
                <div style="float: left; width: 100%; padding-top: 2px;">
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 16%;">
                        Request No . . . . . . . . . . .</div>
                    <div style="float: left; width: 16%;">
                        <asp:TextBox ID="txtRequest" runat="server" CssClass="TextBoxUpper" Enabled="false" MaxLength="10" onchange="ToUpper(this)"></asp:TextBox>
                    </div>
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 16%;">
                        Receiving Location . . . . .
                    </div>
                    <div style="float: left; width: 16%;">
                        <asp:TextBox ID="txtDispatchRequried" runat="server" CssClass="TextBoxUpper" Width="50%" onchange="ToUpper(this)"></asp:TextBox> &nbsp;
                                        <asp:ImageButton ID="imgDispatchRequried" runat="server" ImageUrl="~/Images/icon_search.png"
                                            OnClick="imgDispatchRequried_Click" />
                    </div>
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 16%;">
                        Manual Ref. . . . . .
                    </div>
                    <div style="float: left; width: 16%;">
                        <asp:TextBox runat="server" ID="txtManualRef" Width="100%" CssClass="TextBoxUpper" MaxLength="15" onchange="ToUpper(this)"></asp:TextBox>
                    </div>
                </div>
                <div style="float: left; width: 100%; padding-top: 3px;">
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 16%;">
                        Remarks . . . . . . . . . . . .
                    </div>
                    <div style="float: left; width: 78.2%;">
                        <asp:TextBox ID="txtRemarks" runat="server" Width="100%" CssClass="TextBox" TextMode="MultiLine" MaxLength="300"
                            Rows="2"></asp:TextBox>
                    </div>
                </div>
                <div style="float: left; width: 100%; height: 2px;"> &nbsp;</div>
                <%--Item--%>
                <div style="height: 18px; background-color: #1E4A9F; color: White; width: 100%; float: left;">
                    Item Detail
                </div>
                <div style="float: left; width: 100%;">
                    <%--Row1--%>
                    <div style="float: left; width: 100%; padding-top: 2px;">
                        
                        <div style="float: left; width: 1%;"> &nbsp;</div>
                        <div style="float: left; width: 5%;"> Item </div>
                        <div style="float: left; width: 15%;">  <asp:TextBox ID="txtItem" runat="server" CssClass="TextBoxUpper"  Width="80%" onchange="ToUpper(this)"></asp:TextBox>  <asp:ImageButton ID="imgBtnItem" runat="server" ImageUrl="~/Images/icon_search.png"   ImageAlign="Middle" OnClick="imgBtnItem_Click" />  </div>
      
                        <div style="float: left; width: 5%;"> &nbsp;</div>
                        <div style="float: left; width: 5%;"> Status</div>
                        <div style="float: left; width: 10%;"> <asp:DropDownList ID="ddlStatus" runat="server" Width="100%" CssClass="ComboBox">  </asp:DropDownList> </div>

                        <div style="float: left; width: 5%;"> &nbsp;</div>
                        <div style="float: left; width: 3%;"> Qty</div>
                        <div style="float: left; width: 10%;"> <asp:TextBox ID="txtQty" runat="server" CssClass="TextBox" Width="100%" onKeyPress="return numbersonly(event,false)"></asp:TextBox> </div>

                        <div style="float: left; width: 1%;">  &nbsp;</div>
                        <div style="float: left; width: 36%; "> <asp:TextBox ID="lblModel" runat="server" Font-Size="11px" BorderColor="White" BorderWidth="0px" Width="100%"></asp:TextBox> </div>

                        <div style="float: left; width: 1%;"> &nbsp;<asp:ImageButton ID="imgBtnAddItem" runat="server" ImageAlign="Middle" ImageUrl="~/Images/Add-16x16x16.ICO"   Width="20px" Height="20px" OnClick="AddItem" />     </div>
                    </div>

                    <%--Tab Panel--%>
                    <div style="float: left; width: 100%; padding-top: 4px;">
                        <div style="float: left; width: 1%; height: 161px;">
                        </div>
                        <div style="float: left; width: 98%;">
                            <asp:TabContainer ID="tbContainer" runat="server" Width="100%" Height="161px" 
                                ActiveTabIndex="1">
                                <asp:TabPanel ID="tpItem" HeaderText="Item         " runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlItems" runat="server" ScrollBars="Auto">
                                            <asp:GridView ID="gvItems" runat="server" DataKeyNames="itri_itm_cd,itri_itm_stus,itri_app_qty,itri_line_no"
                                                AutoGenerateColumns="False" OnRowDeleting="OnRemoveFromItemGrid" OnRowDataBound="OnSelectedItemBind"
                                                OnSelectedIndexChanged="GvItems_SelectedIndexChanged"  CellPadding="3" ForeColor="#333333"
                                                CssClass="GridView">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField='itri_itm_cd' HeaderText='Item'>
                                                        <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField='mi_longdesc' HeaderText='Description'>
                                                        <HeaderStyle HorizontalAlign="Left" Width="275px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField='mi_model' HeaderText='Model'>
                                                        <HeaderStyle HorizontalAlign="Left" Width="175px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField='itri_itm_stus' HeaderText='Status'>
                                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField='itri_app_qty' HeaderText='App. Qty'>
                                                        <HeaderStyle HorizontalAlign="Right" Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:Label Text="Pick Qty" ID="lblQty" runat="server" Width="70px" Style="text-align: right;"
                                                                ClientIDMode="Static"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblPickQty" ClientIDMode="Static" ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField='itri_line_no' Visible="False" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgBtnRemoveItem" runat="server" ImageAlign="Middle" ImageUrl="~/Images/Delete.png"
                                                                Width="11px" Height="11px" OnClientClick="return confirm('Do you want to delete?');"
                                                                CommandName="Delete" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField ButtonType="Image" ShowSelectButton="True" SelectImageUrl="~/Images/Add-16x16x16.ICO" />
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
                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="tpSerial" HeaderText="Serial" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlSerial" runat="server" ScrollBars="Auto">
                                            <asp:GridView ID="gvSerial" runat="server" AutoGenerateColumns="False" DataKeyNames="tus_itm_cd,tus_itm_stus,tus_qty,tus_ser_1,tus_ser_id,tus_bin"
                                                OnRowDeleting="OnRemoveFromSerialGrid" CellPadding="3" ForeColor="#333333"
                                                CssClass="GridView">
                                               
                                                <AlternatingRowStyle BackColor="White" />
                                               
                                                <Columns>
                                                    <asp:BoundField DataField='tus_bin' HeaderText='Bin'>
                                                        <HeaderStyle HorizontalAlign="Left" Width="20px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField='tus_itm_cd' HeaderText='Item'>
                                                        <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField='tus_itm_model' HeaderText='Model'>
                                                        <HeaderStyle HorizontalAlign="Left" Width="75px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField='tus_itm_stus' HeaderText='Status'>
                                                        <HeaderStyle HorizontalAlign="Left" Width="75px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField='tus_qty' HeaderText='Qty'>
                                                        <HeaderStyle HorizontalAlign="Right" Width="120px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField='tus_ser_1' HeaderText='Serial 1'>
                                                        <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField='tus_ser_2' HeaderText='Serial 2'>
                                                        <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField='tus_ser_3' HeaderText='Serial 3'>
                                                        <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgBtnRemoveSerial" runat="server" ImageAlign="Middle" ImageUrl="~/Images/Delete.png"
                                                                Width="11px" Height="11px" OnClientClick="return confirm('Do you want to delete?');"
                                                                CommandName="Delete" />
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
                                    </ContentTemplate>
                                </asp:TabPanel>
                            </asp:TabContainer>
                        </div>
                        <div style="float: left; width: 1%; height: 251px;">
                        </div>
                    </div>
                </div>
            </div>
            <%-- Ajax Control Area --%>
            <div>
                <%-- From Date --%>
                <asp:CalendarExtender ID="CEFromDate" runat="server" TargetControlID="txtFrom" PopupButtonID="imgFromDate"
                    PopupPosition="BottomLeft" EnabledOnClient="true" Animated="true" Format="dd/MM/yyyy">
                </asp:CalendarExtender>
                <%-- To Date --%>
                <asp:CalendarExtender ID="CEToDate" runat="server" TargetControlID="txtTo" PopupButtonID="imgToDate"
                    PopupPosition="BottomLeft" EnabledOnClient="true" Animated="true" Format="dd/MM/yyyy">
                </asp:CalendarExtender>
                <%-- Doc Date --%>
                <asp:CalendarExtender ID="CEDocDate" runat="server" TargetControlID="txtDate" PopupButtonID="imgDate"
                    PopupPosition="BottomLeft" EnabledOnClient="true" Animated="true" Format="dd/MM/yyyy">
                </asp:CalendarExtender>

                <%--Modal popup panel--%><%--  ******** AOD - OUT ******--%>
                <asp:ModalPopupExtender ID="ModalPopupExtItem" runat="server" CancelControlID="btnimgCancel"
                    PopupControlID="PanelItemPopUp" TargetControlID="btnHidden_popup" ClientIDMode="Static">
                </asp:ModalPopupExtender>

                <asp:Panel ID="PanelItemPopUp" runat="server" Height="320px" Width="642px" BackColor="#A7C2DA"   BorderColor="#3333FF" BorderWidth="2px">
                    <div style="float: left; width: 100%; height: 22px; text-align: right; padding-top: 2px">
                        <div style="float: left; width: 2%; height: 22px; text-align: left;">   </div>
                        <div id="divPopupImg" runat="server" visible="false" style="float: left; width: 3%;  height: 22px; text-align: left;"> <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/warning.gif" Width="15px"  Height="15px" />  </div>
                        <div style="float: left; width: 65%; height: 22px; text-align: left;">  <asp:Label ID="lblpopupMsg" runat="server" Width="100%" ForeColor="Red" /> </div>
                        <div style="float: left; width: 30%; height: 22px; text-align: right;"> <asp:ImageButton ID="btnimgAdd" runat="server" ImageUrl="~/Images/approve_img.png"   ImageAlign="Middle" OnClick="btnPopupOk_Click" Visible="true" Width="20px" Height="20px" />   &nbsp;  <asp:ImageButton ID="btnimgCancel" runat="server" ImageUrl="~/Images/error_icon.png"   ImageAlign="Middle" OnClick="btnPopupCancel_Click" Visible="true" Width="22px"  Height="22px" /> &nbsp;  </div>
                    </div>
                    <div style="text-align: right">
                        <asp:HiddenField ID="hdnInvoiceNo" runat="server" />
                        <asp:HiddenField ID="hdnInvoiceLineNo" runat="server" />
                        <asp:Label ID="lblPopupAmt" runat="server" Style="text-align: right"></asp:Label>&nbsp;
                    </div>
                    <div style="float: right; width: 100%; height: 22px; text-align: left; padding-top: 2px;  padding-bottom: 2px">  Item Code:
                        <asp:Label ID="lblPopupItemCode" runat="server" Font-Bold="True"></asp:Label>
                        <asp:Label ID="lblPopupBinCode" runat="server" Font-Bold="True"></asp:Label>
                    </div>
                    <div style="float: left; width: 100%; text-align: left;">
                        <div id="divSerialSelect" runat="server" style="float: left; width: 100%; text-align: left;">
                            <div style="float: left; width: 3%; padding-top: 2px; padding-bottom: 3px"> </div>
                            <div style="float: left; width: 15%;">  Search by :  </div>
                            <div style="float: left; width: 14%;">  <asp:DropDownList ID="ddlPopupSerial" runat="server" Width="85%" CssClass="ComboBox">   <asp:ListItem>Serial 1</asp:ListItem>  <asp:ListItem>Serial 2</asp:ListItem>  </asp:DropDownList>  </div>
                            <div style="float: left; width: 15%;">  <asp:TextBox ID="txtPopupSearchSer" runat="server" CssClass="TextBox" Width="100%"></asp:TextBox> </div>
                            <div style="float: left; width: 11%;"> &nbsp;  <asp:Button ID="btnPopupSarch" runat="server" CssClass="Button" OnClick="btnPopupSarch_Click"  Text="Search" />  </div>
                        </div>
                        <div id="divQtySelect" runat="server" visible="false" style="float: left; width: 100%;  text-align: left; padding-top: 2px; padding-bottom: 3px">
                            <div style="float: left; width: 3%;"> </div>
                            <div style="float: left; width: 15%; text-align: left;">   <asp:Label ID="lblPopQty" runat="server" Text="Qty:" Visible="False"></asp:Label>   </div>
                            <div style="float: left; width: 29%; text-align: left;"> <asp:TextBox ID="txtPopupQty" runat="server" CssClass="TextBox" Visible="False" Width="100%"  ClientIDMode="Static"></asp:TextBox>  </div>
                            <div style="float: left; width: 30%; text-align: left;"> &nbsp; <asp:Button ID="btnPopupAutoSelect" runat="server" CssClass="Button" OnClick="btnPopupAutoSelect_Click"    OnClientClick="SelectAuto()" Text="Auto Select" visble="false" />      </div>
                        </div>
                        <div style="float: left; width: 100%; text-align: left; padding-top: 1px; padding-bottom: 2px">
                            <div style="float: left; width: 3%;"> </div>
                            <div style="float: left; width: 15%; text-align: left;">  Requested Qty :  </div>
                            <div style="float: left; width: 15%; text-align: left;">  <asp:Label ID="lblInvoiceQty" runat="server" CssClass="TextBox" Width="85%"></asp:Label> </div>
                        </div>
                        <div style="float: left; width: 100%; text-align: left; padding-top: 1px; padding-bottom: 2px">
                            <div style="float: left; width: 3%;">  </div>
                            <div style="float: left; width: 15%; text-align: left;">  Scaned Qty : </div>
                            <div style="float: left; width: 15%; text-align: left;">  <asp:Label ID="lblScanQty" runat="server" CssClass="TextBox" Width="85%"></asp:Label> </div>
                        </div>
                    </div>
                    <div style="width: 608px">
                        <asp:Panel ID="popupGridPanel" runat="server" Height="172px" ScrollBars="Auto" Style="margin-left: 15px;    margin-bottom: 13px" Width="100%">
                            <asp:GridView ID="GridPopup" runat="server" AutoGenerateColumns="False" CellPadding="4"  Height="45px" Width="95%" CssClass="GridView" ShowHeaderWhenEmpty="True" EmptyDataText="No data found">
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
                </asp:Panel>
            </div>
            <%-- Control Area --%>
            <div style="display: none;">
             
                <asp:Button ID="btnItem" runat="server" OnClick="CheckItem" />
                <asp:Button ID="btnQty" runat="server" OnClick="CheckQty" />
                <asp:Button ID="btnHidden_popup" runat="server" Text="Button" />
                <asp:Button ID="btnLocation" runat="server" OnClick="CheckLocation" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
