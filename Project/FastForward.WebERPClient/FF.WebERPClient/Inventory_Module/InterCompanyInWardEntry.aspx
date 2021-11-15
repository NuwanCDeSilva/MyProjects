<%@ Page Title="Transfer-IN Stock Entry" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="InterCompanyInWardEntry.aspx.cs" Inherits="FF.WebERPClient.Inventory_Module.InterCompanyInWardEntry"
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

        function CheckCharacterCount(text, long) {
            var maxlength = new Number(long); // Change number to your max length.
            if (document.getElementById('txtRemarks').value.length > maxlength) {
                text.value = text.value.substring(0, maxlength);
                //alert(" Only " + long + " chars");
            }
        }

    </script>
    <asp:UpdatePanel ID="uplMain" runat="server">
        <ContentTemplate>
            <%--Whole Page--%>
            <div style="float: left; width: 100%; color: Black;">
                <%--Button Panel--%>
                <div style="float: left; width: 100%; height: 22px; text-align: right; background-color: #1E4A9F">
                    <div style="float: left;">
                        <asp:Label ID="lblDispalyInfor" runat="server" Text="Back date allow for" CssClass="Label"
                            ForeColor="Yellow"></asp:Label>
                    </div>
                    <div style="float: right;">
                        <asp:Button ID="btnSave" runat="server" Text="Save" Height="100%" Width="70px" OnClick="btnSave_Click"
                            CssClass="Button" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" Height="100%" Width="70px"
                            CssClass="Button" OnClick="btnClear_Click" />
                        <asp:Button ID="btnClose" runat="server" Text="Close" Height="100%" Width="70px"
                            OnClick="Close" CssClass="Button" />
                    </div>
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
                        CollapsedSize="0" ExpandedSize="151" Collapsed="false" ExpandControlID="Image1"
                        CollapseControlID="Image1" AutoCollapse="False" AutoExpand="False" ScrollContents="false"
                        ExpandDirection="Vertical" ImageControlID="Image1" ExpandedImage="~/Images/16 X 16 UpArrow.jpg"
                        CollapsedImage="~/Images/16 X 16 DownArrow.jpg">
                    </asp:CollapsiblePanelExtender>
                    <%-- Collaps Area - Pending Documents --%>
                    <div style="width: 100%; float: left; color: Black; padding-top: 2px; padding-bottom: 2px;
                        background-color: #EEEEEE;">
                        <asp:Panel runat="server" ID="pnlPending" Width="98%" ScrollBars="Vertical" BorderColor="#9F9F9F"
                            BorderWidth="1px" Font-Bold="true">
                            <div style="float: left; width: 100%; font-weight: normal; padding-top: 1px; padding-bottom: 1px;">
                                <%-- Collaps Area - Searching --%>
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 5%;">
                                        Type . .
                                    </div>
                                    <div style="float: left; width: 22%;">
                                        <asp:DropDownList ID="ddlType" runat="server" Width="95%" Font-Names="Tahoma" Font-Size="12px">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 10%;">
                                        From . . . . . . . .
                                    </div>
                                    <div style="float: left; width: 12%;">
                                        <asp:TextBox ID="txtFrom" runat="server" Width="70%" Font-Names="Tahoma" Font-Size="12px"
                                            Enabled="false"></asp:TextBox>
                                        <asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png"
                                            ImageAlign="Middle" />
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 10%;">
                                        To . . . . . . . . . .
                                    </div>
                                    <div style="float: left; width: 12%;">
                                        <asp:TextBox ID="txtTo" runat="server" Width="70%" Font-Names="Tahoma" Font-Size="12px"
                                            Enabled="false"></asp:TextBox>
                                        <asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png"
                                            ImageAlign="Middle" />
                                    </div>
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left;">
                                        <asp:ImageButton runat="server" ID="imgBtnSearch" ImageUrl="~/Images/icon_search.png"
                                            ImageAlign="Middle" OnClick="SearchRequest" /></div>
                                </div>
                                <%-- Collaps Area - Result --%>
                                <div style="float: left; width: 100%;">
                                    <asp:Panel ID="pnlPendingDoc" runat="server" ScrollBars="Vertical" BorderColor="#9F9F9F"
                                        BorderWidth="1px" Font-Bold="true" Height="115px" Width="98%">
                                        <asp:GridView ID="gvPending" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                                            DataKeyNames="ith_doc_no,ith_doc_date,ith_doc_tp,ith_com,ith_loc" OnRowDataBound="OnPendingRequestBind"
                                            OnSelectedIndexChanged="BindSelectedOutwardNo" CssClass="GridView" Width="100%"
                                            CellPadding="4" EmptyDataText="No data found" ForeColor="#333333" ShowHeaderWhenEmpty="True">
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Size="12px" Font-Bold="True" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle Font-Size="12px" BackColor="#EFF3FB" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField='ith_doc_no' HeaderText='Outward Doc No' HeaderStyle-Width="170px"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="170px" />
                                                </asp:BoundField>
                                                <%--   <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkbtnReqNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "itr_req_no") %>'
                                                            CommandName="SELECTREQUEST" CommandArgument='<%# Eval("itr_req_no") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:BoundField DataField='ith_doc_date' HeaderText='Date' HeaderStyle-Width="75px"
                                                    DataFormatString="{0:dd/MMM/yyyy}" HeaderStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="75px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField='ith_doc_tp' HeaderText='Outward Type' HeaderStyle-Width="90px"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField='ith_manual_ref' HeaderText='Ref. No' HeaderStyle-Width="300px"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="300px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField='ith_com' HeaderText='Company' HeaderStyle-Width="75px"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="75px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField='ith_loc' HeaderText='Location' HeaderStyle-Width="75px"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" Width="75px" />
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
                <%--General Row 1--%>
                <div style="height: 18px; background-color: #1E4A9F; color: White; width: 100%; float: left;">
                    <asp:HiddenField ID="hdnAllowBin" runat="Server" Value="0" />
                    <asp:HiddenField ID="hdnDefBinCd" runat="Server" Value="0" />
                    <asp:HiddenField ID="hdnOutwarddate" runat="Server" Value="" />
                    General
                </div>
                <%--General Row 2--%>
                <div style="float: left; width: 100%; padding-top: 2px;">
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 14%;">
                        Date . . . . . . . . . . . . .</div>
                    <div style="float: left; width: 18%;">
                        <asp:TextBox ID="txtDate" runat="server" CssClass="TextBox" Font-Names="Tahoma" Font-Size="12px"
                            Width="62%" Enabled="false"></asp:TextBox>
                        <asp:Image ID="imgDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png"
                            ImageAlign="Middle" Visible="False" /></div>
                    <div style="float: left; width: 2%;">
                        &nbsp;</div>
                    <div style="float: left; width: 17%;">
                        Issuing Company . . . . . .
                    </div>
                    <div style="float: left; width: 16%;">
                        <asp:TextBox ID="txtIssueCom" runat="server" CssClass="TextBox" ReadOnly="True" Width="100%"></asp:TextBox>
                    </div>
                    <div style="float: left; width: 3%;">
                        &nbsp;</div>
                    <div style="float: left; width: 12%;">
                        Vehicle No . . . . . .
                    </div>
                    <div style="float: left; width: 14%;">
                        <asp:TextBox ID="txtVehicle" runat="server" CssClass="TextBox" MaxLength="50"></asp:TextBox>
                    </div>
                    <div style="float: left; width: 2%;">
                        &nbsp;</div>
                </div>
                <%--General Row 3--%>
                <div style="float: left; width: 100%; padding-top: 2px;">
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 14%;">
                        Outward No . . . . . . . .
                    </div>
                    <div style="float: left; width: 18%;">
                        <asp:TextBox ID="txtRequest" runat="server" CssClass="TextBox" Width="100%" ReadOnly="True"></asp:TextBox>
                    </div>
                    <div style="float: left; width: 2%;">
                        &nbsp;</div>
                    <div style="float: left; width: 17%;">
                        Issuing Location . . . . . . .</div>
                    <div style="float: left; width: 16%;">
                        <asp:TextBox ID="txtIssueLoca" runat="server" CssClass="TextBox" ReadOnly="True"
                            Width="100%"></asp:TextBox></div>
                </div>
                <%--General Row 4--%>
                <div style="float: left; width: 100%; padding-top: 2px;">
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 14%;">
                        Remarks . . . . . . . . . .</div>
                    <div style="float: left; width: 82%;">
                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Rows="2" Width="100%"
                            ClientIDMode="Static" CssClass="TextBox" onKeyUp="javascript:CheckCharacterCount(this,250);"
                            onChange="javascript:CheckCharacterCount(this,250);"></asp:TextBox>
                    </div>
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                </div>
                <%--General Row 5--%>
                <div style="float: left; width: 100%; padding-top: 2px;">
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                </div>
                <%--Item--%>
                <div style="height: 18px; background-color: #1E4A9F; color: White; width: 100%; float: left;">
                    Item Detail
                </div>
                <div style="float: left; width: 100%;">
                    <div style="float: left; width: 100%; display: none;">
                        <%--Row1--%>
                        <div style="float: left; width: 100%; padding-top: 2px;">
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 8%;">
                                Serial 1 . . . .</div>
                            <div style="float: left; width: 24%;">
                                <asp:TextBox ID="txtSerial1" runat="server" CssClass="TextBox" Width="70%"></asp:TextBox>
                                <asp:ImageButton ID="imgBtnSerial1" runat="server" ImageUrl="~/Images/icon_search.png"
                                    ImageAlign="Middle" OnClick="imgBtnSerial_Click" />
                            </div>
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 8%;">
                                Bin . . . . . . .
                            </div>
                            <div style="float: left; width: 20%;">
                                <asp:DropDownList ID="DDLBinCode" runat="server" Width="50%" Font-Names="Tahoma"
                                    Font-Size="12px">
                                </asp:DropDownList>
                            </div>
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 30%;">
                                <asp:TextBox ID="lblDescription" runat="server" BorderColor="White" BorderWidth="0px"
                                    Width="100%"></asp:TextBox>
                            </div>
                        </div>
                        <%--Row2--%>
                        <div style="float: left; width: 100%; padding-top: 2px;">
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 8%;">
                                Serial 2 . . . .</div>
                            <div style="float: left; width: 24%;">
                                <asp:TextBox ID="txtSerial2" runat="server" CssClass="TextBox" Width="70%"></asp:TextBox>
                            </div>
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 8%;">
                                Item . . . . . .
                            </div>
                            <div style="float: left; width: 20%;">
                                <asp:TextBox ID="txtItem" runat="server" CssClass="TextBox" Width="80%"></asp:TextBox>
                                <asp:ImageButton ID="imgBtnItem" runat="server" ImageUrl="~/Images/icon_search.png"
                                    ImageAlign="Middle" OnClick="imgBtnItem_Click" />
                            </div>
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 30%;">
                                <asp:TextBox ID="lblModel" runat="server" BorderColor="White" BorderWidth="0px" Width="100%"></asp:TextBox>
                            </div>
                        </div>
                        <%--Row3--%>
                        <div style="float: left; width: 100%; padding-top: 2px;">
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 8%;">
                                Serial 3 . . . .</div>
                            <div style="float: left; width: 24%;">
                                <asp:TextBox ID="txtSerial3" runat="server" CssClass="TextBox" Width="70%"></asp:TextBox>
                            </div>
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 8%;">
                                Status . . . . .</div>
                            <div style="float: left; width: 20%;">
                                <asp:DropDownList ID="ddlStatus" runat="server" Width="50%" Font-Names="Tahoma" Font-Size="12px">
                                </asp:DropDownList>
                            </div>
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 8%;">
                                Qty . . . . . . .</div>
                            <div style="float: left; width: 25%;">
                                <asp:TextBox ID="txtQty" runat="server" CssClass="TextBox" Width="25%" onKeyPress="return numbersonly(event,false)"></asp:TextBox>
                            </div>
                            <div style="float: left; width: 1%;">
                                &nbsp;<asp:ImageButton ID="imgBtnAddItem" runat="server" ImageAlign="Middle" ImageUrl="~/Images/download_arrow_icon.png"
                                    Width="16px" Height="16px" BorderColor="DarkKhaki" BorderStyle="Solid" BorderWidth="1px"
                                    OnClick="AddItem" />
                            </div>
                        </div>
                    </div>
                    <%--Tab Panel--%>
                    <div style="float: left; width: 100%; padding-top: 4px; height: 140px;">
                        <div style="float: left; width: 1%; height: 140px;">
                        </div>
                        <div style="float: left; width: 98%;">
                            <asp:TabContainer ID="tbContainer" runat="server" Width="100%" Height="140px" BorderColor="Black"
                                BorderWidth="1px" BorderStyle="Solid" ActiveTabIndex="1">
                                <%--Item Tab Panel--%>
                                <asp:TabPanel ID="tpItem" HeaderText="Item" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlItem" runat="server" ScrollBars="Auto">
                                            <asp:GridView ID="gvItem" runat="server" DataKeyNames="itemcode" AutoGenerateColumns="False"
                                                OnRowDeleting="OnRemoveFromItemGrid" OnRowDataBound="OnSelectedItemBind" OnSelectedIndexChanged="BindSelectedItemToText"
                                                CssClass="GridView" CellPadding="4" EmptyDataText="No data found" ForeColor="#333333"
                                                ShowHeaderWhenEmpty="True">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField='itemcode' HeaderText='Item'>
                                                        <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField='itemdesc' HeaderText='Description'>
                                                        <HeaderStyle HorizontalAlign="Left" Width="350px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField='itemmodel' HeaderText='Model'>
                                                        <HeaderStyle HorizontalAlign="Left" Width="175px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField='itemstatus' HeaderText='Status'>
                                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField='itemqty' HeaderText='Qty' DataFormatString='<%$ appSettings:FormatToQty %>'>
                                                        <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField Visible="False">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgBtnRemoveItem" runat="server" ImageAlign="Middle" ImageUrl="~/Images/Delete.png"
                                                                Width="12px" Height="12px" OnClientClick="return confirm('Do you want to delete?');"
                                                                CommandName="Delete" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Size="12px" Font-Bold="True" />
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
                                <%--Item Tab Panel--%>
                                <%--Serial Tab Panel--%>
                                <asp:TabPanel ID="tpSerial" HeaderText="Serial" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlSerial" runat="server" ScrollBars="Auto">
                                            <asp:GridView ID="gvSerial" runat="server" AutoGenerateColumns="False" DataKeyNames="tus_itm_cd,tus_itm_stus,tus_qty,tus_ser_1,tus_ser_id,tus_bin"
                                                OnRowDeleting="OnRemoveFromSerialGrid" CssClass="GridView" CellPadding="4" EmptyDataText="No data found"
                                                ForeColor="#333333" ShowHeaderWhenEmpty="True">
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Size="12px" Font-Bold="True" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle Font-Size="12px" BackColor="#EFF3FB" />
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
                                                    <asp:BoundField DataField='tus_qty' HeaderText='Qty' DataFormatString='<%$ appSettings:FormatToQty %>'>
                                                        <HeaderStyle HorizontalAlign="Right" Width="120px" />
                                                        <ItemStyle HorizontalAlign="Right" />
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
                                                                Width="12px" Height="12px" OnClientClick="return confirm('Do you want to delete?');"
                                                                CommandName="Delete" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:TabPanel>
                                <%--Serial Tab Panel--%>
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
                    PopupPosition="BottomLeft" EnabledOnClient="true" Animated="true" Format="dd/MMM/yyyy">
                </asp:CalendarExtender>
                <%-- To Date --%>
                <asp:CalendarExtender ID="CEToDate" runat="server" TargetControlID="txtTo" PopupButtonID="imgToDate"
                    PopupPosition="BottomLeft" EnabledOnClient="true" Animated="true" Format="dd/MMM/yyyy">
                </asp:CalendarExtender>
                <%-- Doc Date --%>
                <asp:CalendarExtender ID="CEDocDate" runat="server" TargetControlID="txtDate" PopupButtonID="imgDate"
                    PopupPosition="BottomLeft" EnabledOnClient="true" Animated="true" Format="dd/MMM/yyyy">
                </asp:CalendarExtender>
            </div>
            <%-- Control Area --%>
            <div style="display: none;">
                <%--<asp:Button ID="btnSerial1" runat="server" OnClick="CheckSerial1" />
                <asp:Button ID="btnItem" runat="server" OnClick="CheckItem" />
                <asp:Button ID="btnQty" runat="server" OnClick="CheckQty" />--%>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
