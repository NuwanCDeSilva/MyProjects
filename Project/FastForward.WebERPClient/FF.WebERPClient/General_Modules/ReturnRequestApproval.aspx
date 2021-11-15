<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReturnRequestApproval.aspx.cs" Inherits="FF.WebERPClient.General_Modules.ReturnRequestApproval" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript"  >

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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%--Whole Page--%>
<div style=" float:left; width:100%; color:Black; ">
    <%--Button Area--%>
    <div style="height: 22px; text-align: right; " class="PanelHeader">
        <asp:Button ID="btnSave" runat="server" Text="Process" Height="85%" Width="70px"  CssClass="Button" ToolTip="Make a request" OnClick="Process"  />
        <asp:Button ID="btnReject" runat="server" Text="Castoff" Height="85%" Width="70px"  CssClass="Button" ToolTip="Make a Reject" OnClick="CastOff"  />
        <asp:Button ID="btnClear" runat="server" Text="Clear" Height="85%" Width="70px" CssClass="Button" />
    </div>
    <div style="float:left; width:100%; height:1px;" > &nbsp; </div>
    <%--Common Request Area--%>
    <%-- Collaps Header - Common Request Area --%>
    <div class="CollapsiblePanelHeader" > Pending Outside Return Request </div>
    <%-- Collaps Image - Common Request Area --%>
    <div style="float: left;">  <asp:Image runat="server" ID="Image1" ImageUrl="~/Images/16 X 16 DownArrow.jpg" ImageAlign="Right" /></div>
    <%-- Collaps control - Common Request Area --%>
    <asp:CollapsiblePanelExtender ID="CPEPending" runat="server" TargetControlID="pnlPending"  CollapsedSize="0" ExpandedSize="81" Collapsed="True" ExpandControlID="Image1"
    CollapseControlID="Image1" AutoCollapse="False" AutoExpand="False" ScrollContents="false"
    ExpandDirection="Vertical" ImageControlID="Image1" ExpandedImage="~/Images/16 X 16 UpArrow.jpg"
    CollapsedImage="~/Images/16 X 16 DownArrow.jpg" ClientIDMode="Static">
    </asp:CollapsiblePanelExtender>
    <div style=" float:left;width:100%;">
        <asp:Panel runat="server" ID="pnlPending" Width="99%"   >
            <div style=" float:left;width:30%;">
                <div style=" float:left;width:100%; padding-top:2px;">
                    <div style=" float:left;width:1%;">&nbsp; </div>
                    <div style=" float:left;width:30%;"> Warranty No </div>
                    <div style=" float:left;width:69%;"> <asp:TextBox ID="txtWarranty" runat="server" CssClass="TextBox"></asp:TextBox>&nbsp;<asp:ImageButton ID="imgBtnOWarranty" runat="server"  ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" /> </div>
                </div>

                <div style=" float:left;width:100%; padding-top:2px;">
                    <div style=" float:left;width:1%;">&nbsp;</div>
                    <div style=" float:left;width:30%;"> Serial No </div>
                    <div style=" float:left;width:69%;"> <asp:TextBox ID="txtSerial" runat="server" CssClass="TextBox"></asp:TextBox>&nbsp;<asp:ImageButton ID="imgBtnOSerial" runat="server"  ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" /> </div>
                </div>

                <div style=" float:left;width:100%; padding-top:2px;">
                    <div style=" float:left;width:1%;">&nbsp;</div>
                    <div style=" float:left;width:30%;"> Invoice No </div>
                    <div style=" float:left;width:69%;"> <asp:TextBox ID="txtInvoice" runat="server" CssClass="TextBox"></asp:TextBox>&nbsp;<asp:ImageButton ID="imgBtnOInvoice" runat="server"  ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" /> </div>
                </div>

                <div style=" float:left;width:100%; padding-top:2px;">
                    <div style=" float:left;width:1%;">&nbsp; </div>
                    <div style=" float:left;width:30%;"> Request No </div>
                    <div style=" float:left;width:69%;"> <asp:TextBox ID="txtRequest" runat="server" CssClass="TextBox"></asp:TextBox>&nbsp;<asp:ImageButton ID="imgBtnORequest" runat="server"  ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" /> </div>
                </div>
            </div>
            <div style=" float:left;width:1%;">&nbsp;</div>
            <div style=" float:left;width:69%; ">
                <asp:Panel ID="pnlRequest" runat ="server" Height="101px" ScrollBars="Auto">
                    <asp:GridView runat="server" ID="gvRequest" AutoGenerateColumns="False" 
                        CellPadding="3" ForeColor="#333333" GridLines="Both"  CssClass="GridView">
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
                </asp:Panel>
            </div>
        </asp:Panel>
    </div>
    <div style="float:left; width:100%; height:1px;" > &nbsp; </div>
    <%--AD-HOC Request Area--%>
    <div class="PanelHeader" style="height:12px; "><div style="float:left; width:70%;"> Return Accept Detail</div><div style="float:left; width:30%;"> Date &nbsp; <asp:TextBox runat="server" ID="txtDate" CssClass="TextBox"></asp:TextBox>  </div>  </div>
    <div style=" float:left;width:100%;">
        <div style=" float:left;width:100%; padding-top:2px;"> 
            <%--Searching Area--%>
            <div style=" float:left;width:25%; ">
                <div class="PanelHeader" style="height:12px; border-bottom:2px solid #9F9F9F;"> Searching... </div>
                <div style=" float:left;width:100%;padding-top:2px;"> 
                    <div style=" float:left;width:1%;"> &nbsp; </div>
                    <div style=" float:left;width:35%;"> Warranty No </div>
                    <div style=" float:left;width:64%;"><asp:TextBox runat="server" ID="txtAWarrantyNo" CssClass="TextBox" Width="70%"></asp:TextBox>&nbsp;<asp:ImageButton ID="imgBtnAWarranty" runat="server"  ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" OnClick="imgBtnAWarranty_Click" />  </div>
                </div>
                <div style=" float:left;width:100%;padding-top:2px;"> 
                    <div style=" float:left;width:1%;"> &nbsp; </div>
                    <div style=" float:left;width:35%;"> Serial No </div>
                    <div style=" float:left;width:64%;"><asp:TextBox runat="server" ID="txtASerialNo" CssClass="TextBox" Width="70%"></asp:TextBox>&nbsp;<asp:ImageButton ID="imgBtnASerial" runat="server"  ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" OnClick="imgBtnASerial_Click" />  </div>
                </div>
                <div style=" float:left;width:100%;padding-top:2px;"> 
                    <div style=" float:left;width:1%;"> &nbsp; </div>
                    <div style=" float:left;width:35%;"> Doc. Type </div>
                    <div style=" float:left;width:64%;"><asp:DropDownList ID="ddlADocType" runat="server" CssClass="ComboBox" Width="50%"><asp:ListItem Text="Cash"></asp:ListItem><asp:ListItem Text="Hire"></asp:ListItem> <asp:ListItem Text="Request"></asp:ListItem> </asp:DropDownList> </div>
                </div>
                <div style=" float:left;width:100%;padding-top:2px;"> 
                    <div style=" float:left;width:1%;"> &nbsp; </div>
                    <div style=" float:left;width:35%;"> Document No </div>
                    <div style=" float:left;width:64%;"><asp:TextBox runat="server" ID="txtADocumentNo" CssClass="TextBox" Width="70%"></asp:TextBox>&nbsp;<asp:ImageButton ID="imgBtnAInvoice" runat="server"  ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" OnClick="imgBtnAInvoice_Click" />  </div>
                </div>
            </div>

            <div style=" float:left;width:1%;">&nbsp;</div>

            <%--Customer Detail--%>
            <div style=" float:left;width:25%; ">
                    <div class="PanelHeader" style="height:12px; border-bottom:2px solid #9F9F9F;"> Customer Detail </div> 
                    <div style=" float:left;width:100%;font-size:11px;"> 
                    <div style=" float:left;width:100%;padding-top:2px;  background-color:#EFF3FB;"> 
                        <div style=" float:left;width:1%;"> &nbsp; </div>
                        <div style=" float:left;width:20%;"> Code </div><div style=" float:left;width:2%;">:</div>
                        <div style=" float:left;width:77%; "><asp:Label ID="lblACode" runat="server" ></asp:Label> </div>
                    </div>

                    <div style=" float:left;width:100%;padding-top:2px;"> 
                        <div style=" float:left;width:1%;"> &nbsp; </div>
                        <div style=" float:left;width:20%;"> Name </div><div style=" float:left;width:2%;">:</div>
                        <div style=" float:left;width:77%;"><asp:Label ID="lblAName" runat="server" ></asp:Label> </div>
                    </div>

                    <div style=" float:left;width:100%;padding-top:2px;  background-color:#EFF3FB;"> 
                        <div style=" float:left;width:1%;"> &nbsp; </div>
                        <div style=" float:left;width:20%;"> Address </div><div style=" float:left;width:2%;">:</div>
                        <div style=" float:left;width:77%;"><asp:Label ID="lblAAddress1" runat="server" ></asp:Label> </div>
                    </div>

                    <div style=" float:left;width:100%;padding-top:2px; ">
                        <div style=" float:left;width:1%;"> &nbsp; </div>
                        <div style=" float:left;width:20%;"> &nbsp; </div><div style=" float:left;width:2%;">:</div>
                        <div style=" float:left;width:77%;"><asp:Label ID="lblAAddress2" runat="server" ></asp:Label> </div>
                    </div> 

                    <div style=" float:left;width:100%;padding-top:2px; background-color:#EFF3FB;">
                        <div style=" float:left;width:1%;"> &nbsp; </div>
                        <div style=" float:left;width:20%;"> &nbsp; </div><div style=" float:left;width:2%;">:</div>
                        <div style=" float:left;width:77%;"><asp:Label ID="lblAAddress3" runat="server" ></asp:Label> </div>
                    </div> 
                    </div>
            </div>

            <div style=" float:left;width:1%;">&nbsp;</div> 

            <%--Document Detail--%>
            <div style=" float:left;width:48%; ">
                 <div class="PanelHeader" style="height:12px; border-bottom:2px solid #9F9F9F;"> Trade Detail </div>
                 <div style=" float:left;width:100%;padding-top:2px; "> 
                    <asp:Panel ID="pnlTrade" runat="server" ScrollBars="Auto" Height="75px" >
                        <asp:GridView runat="server" ID="gvATradeItem" AutoGenerateColumns="False"  CssClass="GridView" RowStyle-Wrap ="false"
                            CellPadding="3" ForeColor="#333333" GridLines="Both" DataKeyNames="Sad_itm_cd,Sad_qty,Sad_unit_rt,Sad_itm_line,Mi_act,sad_inv_no,Sad_itm_stus" ShowHeaderWhenEmpty="true" OnRowDataBound="AccountItem_OnRowBind" OnSelectedIndexChanged="BindSelectedAccItemetail" >
                            <EmptyDataTemplate > <div style="width:100%; text-align:center;"> No data found </div> </EmptyDataTemplate>
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
                            <Columns> 
                                <asp:BoundField DataField='Sad_itm_cd' HeaderText='Item' HeaderStyle-Width ="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                                <asp:BoundField DataField='Mi_longdesc' HeaderText='Description' HeaderStyle-Width ="250px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                                <asp:BoundField DataField='Mi_model' HeaderText='Model' HeaderStyle-Width ="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                                <asp:BoundField DataField='Sad_qty' HeaderText='Qty' HeaderStyle-Width ="70px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField='Sad_unit_rt' HeaderText='U. Price'  HeaderStyle-Width ="150px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                                <asp:TemplateField Visible="false" > <ItemTemplate> <asp:HiddenField runat="server" ID="hdnlineNo"   Value='<%# DataBinder.Eval(Container.DataItem, "Sad_itm_line") %>' />    <asp:HiddenField runat="server" ID="hdnIsForwardSale"  Value='<%# DataBinder.Eval(Container.DataItem, "Mi_act") %>' /> <asp:HiddenField runat="server" ID="hdnInvoiceNo"                                              Value='<%# DataBinder.Eval(Container.DataItem, "sad_inv_no") %>' />   </ItemTemplate>    </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                 </div>

            </div>
        </div>

        <div style=" float:left;width:100%; padding-top:2px;"> 
            <div style=" float:left;width:40%; padding-top:2px; ">
                <div class="PanelHeader" style="height:12px; border-bottom:2px solid #9F9F9F;"> Returning Detail </div>
                <div style=" float:left;width:100%;padding-top:2px; "> 
                    <asp:Panel ID="pnlReturn" runat="server" ScrollBars="Auto" Height="200px" >
                        <asp:GridView runat="server" ID="gvAReturnItem" RowStyle-Wrap ="false"  Width="100%"  AutoGenerateColumns="False"  CssClass="GridView" OnRowDeleting="SelectedItem_OnDelete" ShowHeaderWhenEmpty="true"
                            CellPadding="3" ForeColor="#333333" GridLines="Both" DataKeyNames="Tus_ser_id,Tus_itm_cd,Tus_ser_1,Tus_ser_2,Tus_ser_3,Tus_warr_no,Tus_itm_stus,Tus_base_doc_no,Tus_base_itm_line,Tus_batch_line,Tus_ser_line" >
                            <EmptyDataTemplate > <div style="width:100%; text-align:center;"> No data found </div> </EmptyDataTemplate>
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
                            <Columns>
                                <asp:TemplateField  HeaderText=""  ItemStyle-Width="18px"  >
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgBtnPickItemDelete" runat ="server" ImageAlign="Middle" ImageUrl="~/Images/Delete.png" Width="12px" Height="12px"  CommandName="Delete" />
                                        <asp:HiddenField ID="hdnPopSerialID" runat ="server" Value=' <%# DataBinder.Eval(Container.DataItem, "Tus_ser_id") %>' />
                                        <asp:HiddenField ID="hdnPopItem" runat ="server" Value= ' <%# DataBinder.Eval(Container.DataItem, "Tus_itm_cd") %>'  />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField='Tus_doc_no' HeaderText='DO No'  ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                                <asp:BoundField DataField='Tus_itm_cd' HeaderText='Warranty No'  ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                                <asp:BoundField DataField='Tus_ser_1' HeaderText='Serial No'  ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                                <asp:BoundField DataField='tus_unit_price' HeaderText='U.Price'  ItemStyle-Width="75px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </div>
            </div>
            <div style=" float:left;width:1%;">&nbsp;</div>
            <div style=" float:left;width:59%; padding-top:2px; "> 
                <div class="PanelHeader" style="height:12px; border-bottom:2px solid #9F9F9F;"> Issuing Detail </div>
                <div style="float: left; width: 100%; font-weight: normal; padding-top: 1px; padding-bottom: 1px;">
                    <div style="float: left; width: 100%;">
                        <div style="float: left; width: 1%;"> &nbsp;</div>
                        <div style="float: left; width: 15%;">Price Book </div>
                        <div style="float: left; width: 16%;"><asp:TextBox runat="server" onchange="ToUpper(this)" CssClass="TextBoxUpper" ID="txtPriceBook" Width="70%" MaxLength="10"></asp:TextBox>&nbsp;<asp:ImageButton  ID="imgBtnPriceBook" runat="server" ImageUrl="~/Images/icon_search.png" ImageAlign="Middle"   OnClick="imgBtnPriceBook_Click" /></div>
                        
                        <div style="float: left; width: 10%;"> &nbsp;</div>
                        <div style="float: left; width: 15%;">Price Level </div>
                        <div style="float: left; width: 16%;"><asp:TextBox runat="server" onchange="ToUpper(this)" ID="txtPriceLevel" CssClass="TextBoxUpper" Width="70%" MaxLength="10"></asp:TextBox>&nbsp;<asp:ImageButton ID="imgBtnPriceLevel" runat="server" ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" OnClick="imgBtnPriceLevel_Click" /></div>
                    </div>
                </div>
                <div style="float:left;width :100%;">
                    <div style="float:left;width :26%;background-color: #507CD1; border-right: 1px solid white; color: White;"><asp:TextBox runat="server" ID="txtOutItem" Text="Item" onfocus="if (this.value=='Item')this.value='';" onblur="if(this.value=='') this.value='Item';"  style=" background-color:#507CD1;width:93%; font-size:11px; font-family:Verdana;  border:0px none #507CD1 ; color:White; "  ></asp:TextBox> </div>
                    <div style="float:left;width :20%;background-color: #507CD1; border-right: 1px solid white; color: White;"><asp:TextBox runat="server" ID="txtOutStatus"  onfocus="if (this.value=='Status')this.value='';" onblur="if(this.value=='') this.value='Status';"  Text ="Status"  style=" background-color:#507CD1;width:93%; font-size:11px; font-family:Verdana;  border:0px none #507CD1 ; color:White; " ></asp:TextBox> </div>
                    <div style="float:left;width :6%;background-color: #507CD1; border-right: 1px solid white; color: White;text-align:right;"><asp:TextBox runat="server"  onfocus="if (this.value=='Qty')this.value='';" onblur="if(this.value=='') this.value='Qty';"  ID="txtOutQty" Text="Qty" style=" background-color:#507CD1;width:93%;font-size:11px; font-family:Verdana; text-align:right; color:White; border:0px none #507CD1 ;"  ></asp:TextBox> </div>
                    <div style="float:left;width :20%;background-color: #507CD1; border-right: 1px solid white; color: White;text-align:right; height:16px;"><asp:TextBox runat="server"  onfocus="if (this.value=='Unit Rate')this.value='';" onblur="if(this.value=='') this.value='Unit Rate';"  ID="txtOutUnitRate" Text="Unit Rate" style=" background-color:#507CD1;width:93%;font-size:11px; font-family:Verdana; text-align:right; color:White; border:0px none #507CD1 ;"  ReadOnly="true" ></asp:TextBox> </div>
                    <div style="float:left;width :22%;background-color: #507CD1; border-right: 1px solid white; color: White;text-align:right; height:16px;"><asp:TextBox runat="server"  onfocus="if (this.value=='Amount')this.value='';" onblur="if(this.value=='') this.value='Amount';"  ID="txtOutAmount" Text="Amount" style=" background-color:#507CD1;width:93%;font-size:11px; font-family:Verdana; text-align:right; color:White; border:0px none #507CD1 ;"  ReadOnly="true" ></asp:TextBox> </div>
                    <div style="float:left;width :1%;"><asp:ImageButton runat="server" ID="btnAddItem"  ImageAlign="Middle"  ImageUrl="~/Images/Add-16x16x16.ICO" OnClick="AddItem" /></div>
                </div>

                <div style="width:100%;float:left;"> 
                    <asp:Panel ID="pnlExchangeOutItm" runat="server" ScrollBars="Auto">
                        <asp:GridView runat ="server" ID="gvExchangeOutItm" AutoGenerateColumns="False"   OnRowDeleting="OnRemoveFromInvoiceItemGrid" 
                            CssClass="GridView" CellPadding="3" ForeColor="#333333" GridLines="Both" ShowHeader="false" DataKeyNames="sad_itm_cd,sad_itm_stus,sad_unit_rt,sad_pbook,sad_pb_lvl,Sad_qty,sad_disc_amt,sad_itm_tax_amt,sad_promo_cd,sad_is_promo,sad_job_line">
                            <AlternatingRowStyle BackColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"  />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            <Columns>
                                <asp:BoundField DataField='Sad_itm_cd' HeaderText='Item'  ItemStyle-Width="78px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                                <asp:BoundField DataField='sad_itm_stus' HeaderText='Description' ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                                <asp:BoundField DataField='Sad_qty' HeaderText='Qty' ItemStyle-Width="14px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField='Sad_unit_rt' HeaderText='Unit Price' ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField='Sad_unit_amt' HeaderText='Unit Price'   ItemStyle-Width="66px"  HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                                <asp:TemplateField ItemStyle-Height="5px" ItemStyle-Width="5px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgBtnGridDelete" runat="server" ImageUrl="~/Images/Delete.png"  Height="8px" Width="8px"     CommandName="Delete" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </div>
            </div>
        </div> 
        <%--filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#1e5799', endColorstr='#007db9e8',GradientType=1);--%>
        <div style=" float:left;width:100%; padding-top:3px; " > 
            <div style=" float:left;width:100%; "> 
                  <div style=" float:left;width:1%; ">&nbsp; </div>
                  <div style=" float:left;width:12%; "> Old Item Amount </div>
                  <div style=" float:left;width:10%; "> <asp:Label ID="lblOldAmount" runat="server" Text="0.00" ></asp:Label> </div>

                  <div style=" float:left;width:1%; ">&nbsp; </div>
                  <div style=" float:left;width:12%; "> New Item Amount </div>
                  <div style=" float:left;width:10%; "> <asp:Label ID="lblNewAmount" runat="server" Text="0.00" ></asp:Label> </div>

                  <div style=" float:left;width:1%; ">&nbsp; </div>
                  <div style=" float:left;width:8%; "> Differance </div>
                  <div style=" float:left;width:10%; "> <asp:Label ID="lblDifference" runat="server" Text="0.00" ></asp:Label> </div>

                  <div style=" float:left;width:1%; ">&nbsp; </div>
                  <div style=" float:left;width:8%; "> Usage Type </div>
                  <div style=" float:left;width:10%; "> <asp:DropDownList ID="ddlUsageType" runat="server" CssClass="ComboBox"> <asp:ListItem Text="Discount"></asp:ListItem><asp:ListItem Text="Usage Charge"></asp:ListItem> </asp:DropDownList> </div>

                  <div style=" float:left;width:1%; ">&nbsp; </div>
                  <div style=" float:left;width:10%; "><asp:TextBox runat="server" ID="txtUsageAmount" CssClass="TextBox" Width="100%" ></asp:TextBox> </div>
            </div>
            <div style=" float:left;width:100%;"> 

                               
            </div>
        </div>

      <asp:HiddenField ID="hdnUserQty" Value ="" runat="server" ClientIDMode="Static" />          
    </div>


    <%-- Modal Popup Extenders for hire sales serial --%>
    <div>
        <asp:ModalPopupExtender ID="MPESerial" RepositionMode="RepositionOnWindowScroll" BehaviorID="Modal2"
                    TargetControlID="btnPopUpSer" runat="server" ClientIDMode="Static" PopupControlID="pnlSerialPopUp"
                    BackgroundCssClass="modalBackground" CancelControlID="imgbtnserClose" PopupDragHandleControlID="divpopserHeader">
        </asp:ModalPopupExtender>

        <asp:Panel ID="pnlSerialPopUp" runat="server" Height="350px" Width="500px" CssClass="ModalWindow">
            <%-- PopUp Handler for drag and control --%>
            <div class="popUpHeader" id="divpopserHeader" runat="server">
                <div style="float: left; width: 80%" >   Select Deliverd Serial</div>
                <div style="float: left; width: 20%; text-align: right">
                <asp:ImageButton ID="imgbtnserClose" runat="server" ImageUrl="~/Images/0032-01-16x16x16.ICO" />&nbsp;</div>
            </div>
            <%-- PopUp Message Area --%>
            <div style="float: left; width: 85%; color: red; border-color: Red;">
                        <asp:Label ID="Label1" runat="server" Text="" Width="100%"></asp:Label>
            </div>
            <div style="float: left; width: 15%; padding-bottom:3px;">
                        <asp:Button ID="btnPopSerConfirm" runat="server" Text="Confirm" Width="75px" CssClass="Button" OnClick="ConfirmPopUpSerial_Click" />
            </div>
            <asp:Panel runat="server" ID="pnlSerMain" Width="100%" ScrollBars="Auto">
                <asp:GridView runat="server" ID="gvPopSerial" AutoGenerateColumns="false" 
                CssClass="GridView" CellPadding="3" ForeColor="#333333" GridLines="Both"  DataKeyNames="Tus_ser_id,Tus_itm_cd,Tus_ser_1,Tus_ser_2,Tus_ser_3,Tus_warr_no,Tus_itm_stus,Tus_base_doc_no,Tus_base_itm_line,Tus_batch_line,Tus_ser_line" >
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
                    <Columns>
                        <asp:TemplateField  HeaderText=""  ItemStyle-Width="18px"  >
                            <ItemTemplate>
                                <asp:CheckBox ID="chkPopSerSelect" runat ="server"  Checked=' <%# MyFunction(Convert.ToString(DataBinder.Eval(Container.DataItem, "tus_serial_id"))) %>'  />
                                <asp:HiddenField ID="hdnPopSerialID" runat ="server" Value=' <%# DataBinder.Eval(Container.DataItem, "Tus_ser_id") %>' />
                                <asp:HiddenField ID="hdnPopItem" runat ="server" Value= ' <%# DataBinder.Eval(Container.DataItem, "Tus_itm_cd") %>'  />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField='Tus_doc_no' HeaderText='DO No'  ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                        <asp:BoundField DataField='Tus_ser_1' HeaderText='Serial No'  ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                        <asp:BoundField DataField='Tus_warr_no' HeaderText='Warranty No'  ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                        <asp:BoundField DataField='Tus_itm_stus' HeaderText='Status'  ItemStyle-Width="75px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                    </Columns>
                </asp:GridView>
            </asp:Panel>

        </asp:Panel>


    </div>

    <%-- Modal Popup Extenders for Sale --%>
    <div>
        <asp:ModalPopupExtender ID="MPEPopup" RepositionMode="RepositionOnWindowScroll" BehaviorID="Modal"
                    TargetControlID="btnPopUp" runat="server" ClientIDMode="Static" PopupControlID="pnlPopUp"
                    BackgroundCssClass="modalBackground" CancelControlID="imgbtnClose" PopupDragHandleControlID="divpopHeader">
        </asp:ModalPopupExtender>
        <asp:Panel ID="pnlPopUp" runat="server" Height="350px" Width="500px" CssClass="ModalWindow">
            <%-- PopUp Handler for drag and control --%>
            <div class="popUpHeader" id="divpopHeader" runat="server">
                        <div style="float: left; width: 80%" runat="server" id="divPopCaption">
                            Select Price</div>
                        <div style="float: left; width: 20%; text-align: right">
                            <asp:ImageButton ID="imgbtnClose" runat="server" ImageUrl="~/Images/0032-01-16x16x16.ICO" />&nbsp;</div>
                    </div>
            <%-- PopUp Message Area --%>
            <div style="float: left; width: 100%; color: red; border-color: Red;">
                <asp:Label ID="lblMsg" runat="server" Text="" Width="100%"></asp:Label>
            </div>
            <asp:Panel runat="server" ID="pnlPopMain" Width="100%" ScrollBars="None">
                <%-- Popup for Multiple Price Pick for Serialized price level --%>
                <div style="float: left; width: 100%; background-color: Silver; padding-top: 10px;" runat="server" id="divPopSerialPriceList" visible="false">
                    <%-- Confirm Button Area --%>
                    <div style="float: left; width: 100%; color: Black;">
                    <div style="float: left; width: 1%;"> &nbsp;</div>
                    <div style="float: left; width: 20%; background-color: Black; color: White; border-color: Black;  border-style: solid; border-width: 1px; border-right-style: none;">    Selected Qty      </div>
                    <div style="float: left; width: 10%; border: 1px solid balck; border-color: Black; border-style: solid; border-width: 1px; text-align: right;">
                        <asp:Label ID="lblPopSerialQty" runat="server" Text="" Width="100%"></asp:Label>
                    </div>
                    <div style="float: right; width: 20%;"> <asp:Button ID="btnPopSerialConfirm" Text="Confirm" runat="server" CssClass="Button"  OnClick="btnPopConfirm_Click" />  </div>
                </div>
                    <asp:Panel ID="pnlPopPricePick" runat="server" ScrollBars="Auto" Height="111px" Width="100%">
                                <asp:GridView ID="gvPopSerialPricePick" CssClass="GridView" CellPadding="4" ForeColor="#333333"
                                    GridLines="Both" runat="server" AutoGenerateColumns="false" DataKeyNames="sars_price_type">
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chkPopPricePick" ClientIDMode="Static" OnCheckedChanged="CheckPopPriceListClick"
                                                    AutoPostBack="true" />
                                                <asp:HiddenField ID="hdnPriceType" Value=' <%# DataBinder.Eval(Container.DataItem, "sars_price_type") %>'
                                                    runat="server" />
                                                <asp:HiddenField ID="hdnPbSeq" Value=' <%# DataBinder.Eval(Container.DataItem, "Sars_pb_seq") %>'
                                                    runat="server" />
                                                <asp:HiddenField ID="hdnMainItem" Value=' <%# DataBinder.Eval(Container.DataItem, "Sars_itm_cd") %>'
                                                    runat="server" />
                                                <asp:HiddenField ID="hdnIsFixQty" Value=' <%# DataBinder.Eval(Container.DataItem, "sars_is_fix_qty") %>'
                                                    runat="server" />

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField='sars_circular_no' HeaderText='Circler No' HeaderStyle-Width="100px"
                                            HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField='sars_price_type_desc' HeaderText='Price Type' HeaderStyle-Width="100px"
                                            HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField='sars_ser_no' HeaderText='Serial No' HeaderStyle-Width="150px"
                                            HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField='sars_itm_price' HeaderText='Unit Price' HeaderStyle-Width="100px"
                                            HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:#,##0.00;(#,##0.00);0}"
                                            HtmlEncode="false" />
                                        <asp:BoundField DataField='sars_val_to' HeaderText='Valid Until' HeaderStyle-Width="100px"
                                            DataFormatString="{0:d}" />
                                    </Columns>
                                    <SelectedRowStyle BackColor="#D7D3F2" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                            </asp:Panel>
                </div>
                <%-- Popup for Multiple Price Pick from Non-Serialized price level --%>
                <div style="float: left; width: 100%; padding-top: 10px;" runat="server" id="divPopPriceList"
                            visible="false">
                            <%-- Confirm Button Area --%>
                            <div style="float: left; width: 100%; color: Black;">
                                <div style="float: left; width: 1%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 20%; background-color: Black; color: White; border-color: Black;
                                    border-style: solid; border-width: 1px; border-right-style: none;">
                                    <asp:Label ID="lblPopNonSerialQty" runat="server" Text="" Width="100%"></asp:Label>
                                </div>
                                <div style="float: left; width: 20%;">
                                    <asp:Button ID="btnPopPriceListNonSerial" Text="Confirm" runat="server" CssClass="Button"
                                        OnClick="btnPopConfirm_Click" />
                                </div>
                            </div>
                            <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Height="111px" Width="100%">
                                <asp:GridView ID="gvPopPricePick" CssClass="GridView" CellPadding="4" ForeColor="#333333"
                                    GridLines="Both" runat="server" AutoGenerateColumns="false" DataKeyNames="sapd_itm_cd,sapd_pb_seq,sapd_is_fix_qty,sapd_price_type,sapd_itm_price,sapd_seq_no,sapd_circular_no"
                                    OnRowDataBound="gvPopPricePick_OnRowBind" OnSelectedIndexChanged="LoadNonSerializedCombination">
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField='Sapd_circular_no' HeaderText='Circler No' HeaderStyle-Width="100px"
                                            HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField='Sarpt_cd' HeaderText='Price Type' HeaderStyle-Width="100px"
                                            HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField='Sapd_itm_price' HeaderText='Unit Price' HeaderStyle-Width="100px"
                                            HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:#,##0.00;(#,##0.00);0}"
                                            HtmlEncode="false" />
                                        <asp:BoundField DataField='Sapd_to_date' HeaderText='Valid Until' HeaderStyle-Width="100px"
                                            DataFormatString="{0:d}" />
                                    </Columns>
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                <%-- Popup for Load MRP List for the consumable items --%>
                <div style="float: left; width: 100%; padding-top: 10px;" runat="server" id="divConsumPricePick"
                            visible="false">
                            <%-- Confirm Button Area --%>
                            <div style="float: left; width: 100%; color: Black;">
                                <div style="float: left; width: 1%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 20%; background-color: Black; color: White; border-color: Black;
                                    border-style: solid; border-width: 1px; border-right-style: none;">
                                    <asp:Label ID="lblConsumReqQty" runat="server" Text="" Width="100%"></asp:Label>
                                </div>
                            </div>
                            <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" Height="151px" Width="100%">
                                <asp:GridView ID="gvPopConsumPricePick" CssClass="GridView" CellPadding="4" ForeColor="#333333"
                                    GridLines="Both" runat="server" AutoGenerateColumns="false" DataKeyNames="inb_unit_price,inb_doc_no,inb_itm_stus,inb_free_qty,inb_itm_line"
                                    OnRowDataBound="gvPopConsumPricePick_OnRowBind" OnSelectedIndexChanged="LoadConsumablePriceList">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField='inb_doc_no' HeaderText='Document No' HeaderStyle-Width="100px"
                                            HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField='inb_itm_stus' HeaderText='Item Status' HeaderStyle-Width="100px"
                                            HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField='inb_unit_price' HeaderText='Unit Price' HeaderStyle-Width="100px"
                                            HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:#,##0.00;(#,##0.00);0}"
                                            HtmlEncode="false" />
                                        <asp:BoundField DataField='inb_free_qty' HeaderText='Qty' HeaderStyle-Width="100px" />
                                    </Columns>
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                <%-- Popup for Price Item Combination --%>
                <div style="float: left; width: 100%; padding-top: 10px;" runat="server" id="divPopPriceItemCombination"    visible="false">
                    <%-- Confirm Button Area --%>
                    <div style="float: left; width: 100%; color: Black; text-align: right;">
                    <div style="float: right;"> <asp:Button ID="btnPopPriceItmCombinConfirm" Text="Confirm" runat="server" CssClass="Button"  OnClick="btnPopConfirm_Click" />  &nbsp;  </div>
                    <div style="float: right;"><asp:Button ID="btnPopPriceItmCombinCancel" Text="Cancel" runat="server" CssClass="Button"   OnClick="btnPopCancel_Click" />  &nbsp; </div> 
                </div>
                    <asp:Panel ID="pnlPopItemCombine" runat="server" ScrollBars="Auto" Height="100px"
                                Width="100%">
                                <asp:GridView ID="gvPriceItemCombine" runat="server" AutoGenerateColumns="false"
                                    OnRowDataBound="gvPriceItemCombine_OnRowBind" CssClass="GridView" CellPadding="4"
                                    ForeColor="#333333" GridLines="Both" DataKeyNames="sapc_qty,sapc_main_itm_cd,sapc_pb_seq,sapc_main_line,sapc_itm_line,sapc_main_ser">
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField='sapc_itm_cd' HeaderText='Item' />
                                        <asp:BoundField DataField='mi_longdesc' HeaderText='Description' HeaderStyle-Width="220px" />
                                        <asp:BoundField DataField='mi_model' HeaderText='Model' />
                                        <asp:BoundField DataField='sapc_price' HeaderText='UnitPrice' />
                                        <asp:BoundField DataField='sapc_qty' HeaderText='Qty' />
                                        <asp:TemplateField HeaderText="Selected Qty" HeaderStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="95px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPopPriceItmComSelQty" runat="server" Width="100%" Text='<%# DataBinder.Eval(Container.DataItem, "sapc_qty") %>'
                                                    Style="text-align: right; border-style: none;"></asp:TextBox>
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
                    <div style="float: left; width: 100%; color: Black;">
                                <div style="float: left; width: 1%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 20%; background-color: Black; color: White; border-color: Black;
                                    border-style: solid; border-width: 1px; border-right-style: none;">
                                    Total Points
                                </div>
                                <div style="float: left; width: 10%; border: 1px solid balck; border-color: Black;
                                    border-style: solid; border-width: 1px; text-align: right;">
                                    <asp:Label ID="lblPopPriceItmCombinTotalPoint" runat="server" Text="" Width="100%"></asp:Label>
                                </div>
                                <div style="float: left; width: 1%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 20%; background-color: Black; color: White; border-color: Black;
                                    border-style: solid; border-width: 1px; border-right-style: none;">
                                    Used Points
                                </div>
                                <div style="float: left; width: 10%; border: 1px solid balck; border-color: Black;
                                    border-style: solid; border-width: 1px; text-align: right;">
                                    <asp:Label ID="lblPopPriceItmCombinUsedPoint" runat="server" Text="" Width="100%"></asp:Label>
                                </div>
                                <div style="float: left; width: 1%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 20%; background-color: Black; color: White; border-color: Black;
                                    border-style: solid; border-width: 1px; border-right-style: none;">
                                    Balance Points</div>
                                <div style="float: left; width: 10%; border: 1px solid balck; border-color: Black;
                                    border-style: solid; border-width: 1px; text-align: right;">
                                    <asp:Label ID="lblPopPriceItmCombinBalancePoint" runat="server" Text="" Width="100%"></asp:Label>
                                </div>
                                <div style="float: left; width: 1%;">
                                </div>
                            </div>
                </div>
                <%-- Popup for Inventory Combination --%>
                <div style="float: left; width: 100%; padding-top: 10px;" runat="server" id="divPopInventoryItemCombination" visible="false">
                    <%-- Confirm Button Area --%>
                    <div style="float: left; width: 100%; color: Black;">
                    <div style="float: left; width: 20%;">
                                    <asp:Button ID="btnPopInventoryCombin" Text="Confirm" runat="server" CssClass="Button"
                                        OnClick="btnPopConfirm_Click" />
                                </div>
                </div>
                    <asp:Panel ID="pnlPopInventoryCombine" runat="server" ScrollBars="Auto" Height="100px"
                                Width="100%">
                                <asp:GridView ID="gvItemInventoryCombine" runat="server" AutoGenerateColumns="false"
                                    CssClass="GridView" CellPadding="4" ForeColor="#333333" GridLines="Both">
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField='micp_comp_itm_cd' HeaderText='Item' />
                                        <asp:BoundField DataField='mi_longdesc' HeaderText='Description' HeaderStyle-Width="220px" />
                                        <asp:BoundField DataField='mi_model' HeaderText='Model' />
                                        <asp:BoundField DataField='micp_qty' HeaderText='Qty' />
                                        <asp:BoundField DataField='micp_cost_percentage' HeaderText='UnitPrice' />
                                        <asp:BoundField DataField='micp_qty' HeaderText='Select Qty' />
                                    </Columns>
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                            </asp:Panel>
                </div>
                <%-- Popup for serial pick --%>
                <%--<div style="float: left; width: 100%; padding-top: 10px;" runat="server" id="divPopPickSerial"
                            visible="false">
                        </div>--%>
            </asp:Panel>
        </asp:Panel>
    </div>

    <%-- Control Area --%>
    <div style="display: none;">
        <asp:Button ID="btnDocument" runat="server" ClientIDMode="Static" OnClick="txtDocument_LostFocus" />
        <asp:Button ID="btnPopUpSer" runat="server" ClientIDMode="Static" />
        <asp:ImageButton ID="imgBtnInvNo" runat="server" OnClick="imgBtnItem_Click" />
        <asp:Button ID="btnQty" runat="server" OnClick="CheckQty" />
        <asp:Button ID="btnPopUpCombineItemQty" runat="server" ClientIDMode="Static" OnClick="CheckPopUpCombineItemQty" />
        <asp:Button ID="btnPopUp" runat="server" ClientIDMode="Static" />
        <asp:Button ID="btnBook" runat="server" OnClick="CheckPriceBook" />
        <asp:Button ID="btnPriceLevel" runat="server" OnClick="CheckPriceLevel" />
    </div>
</div>
</asp:Content>
