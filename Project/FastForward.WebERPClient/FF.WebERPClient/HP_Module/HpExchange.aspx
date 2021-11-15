<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HpExchange.aspx.cs" Inherits="FF.WebERPClient.HP_Module.HpExchange"  EnableEventValidation="false"   %>
<%@ Register src="../UserControls/uc_HpAccountSummary.ascx" tagname="uc_HpAccountSummary" tagprefix="AC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript" >
    function toggle(toggeldivid, toggeltext) {
        var divelement = document.getElementById(toggeldivid);
        var lbltext = document.getElementById(toggeltext);
        if (divelement.style.display == "block") {
            divelement.style.display = "none";
            lbltext.innerHTML = "+";
        }
        else {
            divelement.style.display = "block";
            lbltext.innerHTML = "-";
        }
    }
</script>
<style type ="text/css">
        .onfocus
        {
            border-color:red;
        }
        .onblur
        {
            border-color:;
        }
    </style>
<script type="text/javascript" >

    function Change(obj, evt)
    {
        if(evt.type=="focus")
            obj.className ="onfocus";
        else if(evt.type=="blur")
           obj.className ="onblur";
    }

</script>
<script type="text/javascript" >

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

<style  type="text/css" >
    .box  {  filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#f2f5f6', endColorstr='#c8d7dc',GradientType=0 );    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:UpdatePanel runat="server" ID="updtExchange">
<ContentTemplate>
<%--Main Page--%>
<div style=" float:left; width:100%; color:Black;" >
    <%--Button Area--%>
    <div style="height: 22px; text-align: right;" class="PanelHeader">
        <asp:Button ID="btnSave" runat="server" Text="Process" Height="85%" Width="70px"  CssClass="Button" OnClick="Process"  />
        <asp:Button ID="btnClear" runat="server" Text="Clear" Height="85%" Width="70px" CssClass="Button" />
        <asp:Button ID="btnClose" runat="server" Text="Clear" Height="85%" Width="70px" CssClass="Button" />
    </div>

    <%--Top Criteria--%>
    <div style=" float:left; width:100%; padding-top:2px;" >
        <div style="float: left; width: 1%;">&nbsp;</div>
        <div style="float: left; width: 5%;">Date </div>
        <div style="float: left; width: 10%;"><asp:TextBox ID="txtDate" runat="server" CssClass="TextBox" Width="70%" Enabled="false"></asp:TextBox>&nbsp;<asp:Image ID="imgDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png"  ImageAlign="Middle" /></div>

        <div style=" float:left; width:1%;" >&nbsp;</div>
        <div style=" float:left; width:8%;" >Account No</div>
        <div style=" float:left; width:15%;"> <asp:TextBox ID="txtAccountNo" runat="server" CssClass="TextBox" Width="70%"></asp:TextBox>  &nbsp;<asp:ImageButton ID="ImgBtnAccountNo" runat="server"  ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" OnClick="ImgAccountSearch_Click" /></div>
        <div style=" float:left; width:1%;" >&nbsp;</div>
        <div style=" float:left; width:10%;" ><asp:Label ID="lblAccountNo" runat="server" ></asp:Label> </div>

        <div style=" float:left; width:5%;" >&nbsp;</div>
        <div style=" float:left; width:8%;" >Request No</div>
        <div style=" float:left; width:10%;" ><asp:DropDownList ID="ddlRequestNo" runat="server" CssClass="ComboBox" Width="90%" ></asp:DropDownList> </div>
        <div style=" float:left; width:2%;" ><asp:ImageButton runat="server" ID="imgBtnRequest" ImageAlign="Middle" ImageUrl="~/Images/EditIcon.png" Width="16px" Height="16px" ToolTip="Make new request" /> </div>
        <div style=" float:left; width:15%;" ><asp:CheckBox runat="server" ID="chkApproved" Text="Approved Request" AutoPostBack="true" OnCheckedChanged="chkApproved_CheckedChanged" /> </div>
    </div>

    <%--Detail Area--%>
    <div style=" float:left; width:100%; padding-top:3px;" >
        <%--Purchased/Exchanged Item History--%>
        <div style=" float:left; width:40%; height:100px; ">
            <div class="PanelHeader"> Purchased/Exchanged Item History</div>
            <div style="width:100%;float:left;"> 
                <asp:Panel ID="pnlItmHistory" runat="server" ScrollBars="Auto"  Height="100px" >
                    <asp:GridView runat ="server" ID="gvItmHistory" AutoGenerateColumns="False" 
                        CssClass="GridView" CellPadding="2" ForeColor="#333333" GridLines="Both"  ShowHeaderWhenEmpty="true" OnRowEditing= "ParentGridView_OnRowEditing" OnRowCancelingEdit="ParentGridView_OnCanceling" >
                        <EmptyDataTemplate > <div style="width:100%; text-align:center;"> No data found </div> </EmptyDataTemplate>
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#92CFFF" />
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
                            <%--<asp:CommandField ShowSelectButton="True"      SelectText="Show Details"/>--%>
                            <asp:TemplateField>
                                <ItemTemplate>
                                     <%--<asp:Button ID="ViewChild_Button" runat="server" Text="+"   CommandName="Edit" Height="20px" Width="20px" />--%>
                                     <asp:ImageButton ID="ViewChild_Button" runat="server" CommandName="Edit" Height="10px" Width="10px" ImageAlign="Middle" ImageUrl="~/Images/Loading.gif" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                   <%-- <asp:Button ID="CancelChild_Button"    runat="server" Text="-" CommandName="Cancel" />--%>
                                    <asp:ImageButton ID="CancelChild_Button" runat="server"   CommandName="Cancel" ImageAlign="Middle" ImageUrl="~/Images/Loading.gif" Width="12px" Height="12px" />
                                    <asp:Panel runat="server" ID="pnlHisGrid" ScrollBars="Auto" Width="300px" >
                                    <asp:GridView runat ="server" ID="gvNstHistory" AutoGenerateColumns="False" 
                                    CssClass="GridView" CellPadding="2" ForeColor="#333333" GridLines="Both"  ShowHeaderWhenEmpty="true" OnRowDataBound="AccountItemHistory_OnRowBind" >
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
                                            <asp:BoundField DataField='Sah_dt' HeaderText='Date' HeaderStyle-Width ="120px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  DataFormatString="{0:d}" />
                                            <asp:BoundField DataField='Sah_inv_no' HeaderText='Invoice' HeaderStyle-Width ="120px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                                            <asp:BoundField DataField='Sah_epf_rt' HeaderText='Qty' HeaderStyle-Width ="120px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"  />
                                            <asp:TemplateField Visible="false" > <ItemTemplate>  <asp:HiddenField runat="server" ID="hdnInvoiceDirection"                                              Value='<%# DataBinder.Eval(Container.DataItem, "Sah_direct") %>' />  </ItemTemplate>    </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>   
                                    </asp:Panel>         
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText ="Item" HeaderStyle-Width ="120px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblHisItem" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Sad_itm_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField  HeaderText='Description'  HeaderStyle-Width ="220px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblHisDesc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Mi_longdesc") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField   HeaderText='Model' HeaderStyle-Width ="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  >
                                <ItemTemplate>
                                    <asp:Label ID="lblHisModel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Mi_model") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField  HeaderText='Brand' HeaderStyle-Width ="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  >
                                <ItemTemplate>
                                    <asp:Label ID="lblHisBrand" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Mi_brand") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </div>
        </div>
        <div style=" float:left; width:1%; ">&nbsp;</div>
        <%--Account Item--%>
        <div style=" float:left; width:59%; height:75px; ">
        <div style=" float:left; width:100%;">
            <div class="PanelHeader"> Account Item </div>
            <div style="width:100%;float:left;"> 
            <asp:Panel ID="pnlAccItem" runat="server" ScrollBars="Auto" Height="60px"  >
                <asp:GridView runat ="server" ID="gvAccItem" AutoGenerateColumns="False" 
                    CssClass="GridView" CellPadding="2" ForeColor="#333333" GridLines="Both" ShowHeaderWhenEmpty="true" OnRowDataBound="AccountItem_OnRowBind" 
                    DataKeyNames="Sad_itm_cd,Sad_qty,Sad_unit_rt,Sad_itm_line,Mi_act,sad_inv_no" >
                    <EmptyDataTemplate > <div style="width:100%; text-align:center;"> No data found </div> </EmptyDataTemplate>
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"/>
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
                        <asp:BoundField DataField='Sad_unit_rt' HeaderText='Price'  HeaderStyle-Width ="150px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                        <asp:TemplateField Visible="false" > <ItemTemplate> <asp:HiddenField runat="server" ID="hdnlineNo"   Value='<%# DataBinder.Eval(Container.DataItem, "Sad_itm_line") %>' />    <asp:HiddenField runat="server" ID="hdnIsForwardSale"  Value='<%# DataBinder.Eval(Container.DataItem, "Mi_act") %>' /> <asp:HiddenField runat="server" ID="hdnInvoiceNo"                                              Value='<%# DataBinder.Eval(Container.DataItem, "sad_inv_no") %>' />   </ItemTemplate>    </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>
            </div>
        </div>
        </div>
        <div style=" float:left; width:1%;">&nbsp;</div>
        
        <div style=" float:left; width:59%;  ">
        <%--Exchange Item - Receiving--%>
        <div style=" float:left; width:40%; height:82px;">
            <div class="PanelHeader"> Exchange Item - Receiving  </div>  
            <div style="width:100%;float:left;"> 
                <asp:Panel ID="pnlExchangeInItm" runat="server" ScrollBars="Auto" Height="80px">
                    <asp:GridView runat ="server" ID="gvExchangeInItm" AutoGenerateColumns="False"  Width="100%"
                        CssClass="GridView" CellPadding="2" ForeColor="#333333" GridLines="Both" ShowHeaderWhenEmpty="true">
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
                                <asp:BoundField DataField='Tus_doc_no' HeaderText='DO No'  ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                                <asp:BoundField DataField='Tus_itm_cd' HeaderText='Warranty'  ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                                <asp:BoundField DataField='Tus_ser_1' HeaderText='Serial'  ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                                <asp:BoundField DataField='tus_unit_price' HeaderText='U.Price'  ItemStyle-Width="75px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  />
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </div>       
        </div>
        <div style=" float:left; width:2%;">&nbsp;</div>
        <%--Exchange Item - Issuing--%>
        <div style=" float:left; width:58%; height:82px;">
            <div class="PanelHeader"> Exchange Item - Issuing  </div>  
            <div style="width:100%;float:left;"> 
                <asp:Panel ID="pnlExchangeOutItm" runat="server" ScrollBars="Auto">
                    <asp:GridView runat ="server" ID="gvExchangeOutItm" AutoGenerateColumns="False"   Width="100%"
                        CssClass="GridView" CellPadding="2" ForeColor="#333333" GridLines="Both"  ShowHeaderWhenEmpty="true" DataKeyNames="sad_itm_cd,sad_itm_stus,sad_unit_rt,sad_pbook,sad_pb_lvl,Sad_qty,sad_disc_amt,sad_itm_tax_amt,sad_promo_cd,sad_is_promo,sad_job_line">
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
                            <asp:BoundField DataField='Sad_unit_amt' HeaderText='Unit Amount'   ItemStyle-Width="66px"  HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </div>       
        </div>
        </div>
    </div>
    <div style=" float:left; width:100%;" >
        <%--Account Summary--%>
        <div style=" float:left; width:40%;"> <AC:uc_HpAccountSummary ID="uc_HpAccountSummary1" runat="server" /></div>
        <div style=" float:left; width:1%;">&nbsp;</div>
        <%--Account Summary--%>
        <div style=" float:left; width:19%;" >
            <div style=" float:left; width:100%;" >
                <div class="PanelHeader"> Differences </div>
                <div style=" float:left; width:100%; padding-top:2px;" >
                    <div style=" float:left; width:1%; " > &nbsp; </div>
                    <div style=" float:left; width:60%;" > Total Difference </div>
                    <div style=" float:Left; width:39%; text-align:right;" > <asp:Label runat="server" ID="lblDifference" Text="0.00" Width="100%"></asp:Label> </div>
                </div>
                <div style=" float:left; width:100%; padding-top:2px;" >
                    <div style=" float:left; width:1%;" > &nbsp; </div>
                    <div style=" float:left; width:60%;" > Discount </div>
                    <div style=" float:Left; width:39%;  text-align:right;" > <asp:Label runat="server" ID="lblDiscount" Text="0.00" Width="100%"></asp:Label> </div>
                </div>
                <div style=" float:left; width:100%; padding-top:2px;" >
                    <div style=" float:left; width:1%;" > &nbsp; </div>
                    <div style=" float:left; width:60%;" > Usage Charge </div>
                    <div style=" float:Left; width:39%;  text-align:right;" > <asp:Label runat="server" ID="lblUsageCharge" Text="0.00" Width="100%"></asp:Label> </div>
                </div>
                <div style=" float:left; width:100%; padding-top:2px;" >
                    <div style=" float:left; width:1%;" > &nbsp; </div>
                    <div style=" float:left; width:60%;" > Fresh Value </div>
                    <div style=" float:Left; width:39%;  text-align:right;" > <asp:Label runat="server" ID="lblNewValue" Text="0.00" Width="100%"></asp:Label> </div>
                </div>
            </div>
   
            <div style=" float:left; width:100%; padding-top:2px;" >
                <div class="PanelHeader"> Payment Prediction </div>
                <div style=" float:left; width:100%;padding-top:2px;" >
                    <div style=" float:left; width:1%; " > &nbsp; </div>
                    <div style=" float:left; width:70%;" > Cash Price Diff. </div>
                    <div style=" float:Left; width:29%; text-align:right;" > <asp:Label runat="server" ID="lblCashPriceDiff" Text="0.00" Width="100%"></asp:Label> </div>
                </div>
                <div style=" float:left; width:100%;" >
                    <div style=" float:left; width:1%; padding-top:2px; " > &nbsp; </div>
                    <div style=" float:left; width:70%;" > Min. First Pay </div>
                    <div style=" float:Left; width:29%; text-align:right;" > <asp:Label runat="server" ID="lblMinDownPayment" Text="0.00" Width="100%"></asp:Label> </div>
                </div>
                <div style=" float:left; width:100%;padding-top:2px; " >
                    <div style=" float:left; width:1%; " > &nbsp; </div>
                    <div style=" float:left; width:70%;" >Tot. Cash Diff. </div>
                    <div style=" float:Left; width:29%; text-align:right;" > <asp:Label runat="server" ID="lblDownPaymentDiff" Text="0.00" Width="100%"></asp:Label> </div>
                </div>
                <div style=" float:left; width:100%; border-top-style:solid;border-top-width:1px; padding-top:3px;">
                    <div style=" float:left; width:1%; ">&nbsp;</div>
                    <div style=" float:left; width:40%;  font-weight:bold; ">Payment</div>
                    <div style=" float:left; width:55%; text-align:right; font-weight:bold; "> <asp:TextBox runat="server" ID="txtNewDownPayment" CssClass="TextBox" Width="100%" Text="0.00"></asp:TextBox>  </div>
                </div>
                <div style=" float:left; width:100%; padding-top:3px;">
                    <div style=" float:left; width:1%; ">&nbsp;</div>
                    <div style=" float:left; width:75%;">Schedule Prediction</div>
                    <div style=" float:left; width:20%; text-align:right;"> 
                        <asp:ImageButton runat="server" ID="imgBtnSchedule" ImageAlign="Middle" 
                            ImageUrl="~/Images/searchIcon.png" Width="16px" Height="16px" 
                            /> </div>
                </div>
            </div>
        </div>
        <div style=" float:left; width:1%;">&nbsp;</div>
        <%--Prediction and Insuarance--%>
        <div style=" float:left; width:39%;" >
            <asp:Accordion runat="server"  ID="acdPriceBookCreation" SelectedIndex="0"   AutoSize="None" FadeTransitions="true"  TransitionDuration="250"    FramesPerSecond="40"     RequireOpenedPane="true"    SuppressHeaderPostbacks="true" Font-Strikeout="False">
            <Panes>
                <asp:AccordionPane runat="server" ID="accPanel1"   >
                    <Header><div  class ="box" style="float: left; width: 100%; color:Black; font-size:11px; border-color:LightGray;border-style:solid; border-width:1px; padding-top:1px; ">Account Prediction &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </div></Header>
                    <Content>
                    <%--Accounts--%>
                    <div style=" float:left; width:100%;" >
                        <div style="width:100%;float:left;"> 
                            <%--Old Value--%>
                            <div style="width:49%;float:left;">
                                <div class="PanelHeader" style=" background-color:lightskyblue; color:Black;"> Current Account</div> 
                                <div style="width:100%;float:left; padding-top:1px;"> 
                                    <div style="width:1%;float:left;"> &nbsp;</div>
                                    <div style="width:59%;float:left;"> Cash Price</div>
                                    <div style="width:40%;float:left; text-align:right;"> <asp:Label runat="server" ID="lblAOCashPrice" Text="0.00" Width="100%"></asp:Label> </div>
                                </div>
                                <div style="width:100%;float:left; padding-top:1px;"> 
                                    <div style="width:1%;float:left;"> &nbsp;</div>
                                    <div style="width:59%;float:left;"> Total Vat Amount</div>
                                    <div style="width:40%;float:left; text-align:right;"> <asp:Label runat="server" ID="lblAOTotVatAmt" Text="0.00" Width="100%"></asp:Label> </div>
                                </div>
                                <div style="width:100%;float:left; padding-top:1px;"> 
                                    <div style="width:1%;float:left;"> &nbsp;</div>
                                    <div style="width:59%;float:left;"> Amount Finance</div>
                                    <div style="width:40%;float:left; text-align:right;"> <asp:Label runat="server" ID="lblAOAmtFinance" Text="0.00" Width="100%"></asp:Label> </div>
                                </div>
                                <div style="width:100%;float:left; padding-top:1px;"> 
                                    <div style="width:1%;float:left;"> &nbsp;</div>
                                    <div style="width:59%;float:left;"> Service Charge</div>
                                    <div style="width:40%;float:left; text-align:right;"> <asp:Label runat="server" ID="lblAOServiceCharge" Text="0.00" Width="100%"></asp:Label> </div>
                                </div>
                                <div style="width:100%;float:left; padding-top:1px;"> 
                                    <div style="width:1%;float:left;"> &nbsp;</div>
                                    <div style="width:59%;float:left;"> Interest Amount</div>
                                    <div style="width:40%;float:left; text-align:right;"> <asp:Label runat="server" ID="lblAOIntAmt" Text="0.00" Width="100%"></asp:Label> </div>
                                </div>  
                                <div style="width:100%;float:left; padding-top:1px;"> 
                                    <div style="width:1%;float:left;"> &nbsp;</div>
                                    <div style="width:49%;float:left;"> Tot. Hire Value</div>
                                    <div style="width:50%;float:left; text-align:right;"> <asp:Label runat="server" ID="lblAOTotHireValue" Text="0.00" Width="100%"></asp:Label> </div>
                                </div>
                                <div style="width:100%;float:left; padding-top:1px;"> 
                                    <div style="width:1%;float:left;"> &nbsp;</div>
                                    <div style="width:49%;float:left;"> Comm. Amount</div>
                                    <div style="width:50%;float:left; text-align:right;"> <asp:Label runat="server" ID="lblAOCommAmt" Text="0.00" Width="100%"></asp:Label> </div>
                                </div>
                                <div style="width:100%;float:left; padding-top:1px;"> 
                                    <div style="width:1%;float:left;"> &nbsp;</div>
                                    <div style="width:49%;float:left;"> Down Payment</div>
                                    <div style="width:50%;float:left; text-align:right;"> <asp:Label runat="server" ID="lblAODownPayment" Text="0.00" Width="100%"></asp:Label> </div>
                                </div>
                            </div>
                            <div style="width:2%;float:left;"> &nbsp; </div>
                            <%--New Value--%>
                            <div style="width:49%;float:left;"> 
                                <div class="PanelHeader" style=" background-color:lightskyblue; color:Black;"> New Account</div> 
                                <div style="width:100%;float:left; padding-top:1px;"> 
                                    <div style="width:1%;float:left;"> &nbsp;</div>
                                    <div style="width:59%;float:left;"> Cash Price</div>
                                    <div style="width:40%;float:left; text-align:right;"> <asp:Label runat="server" ID="lblANCashPrice" Text="0.00" Width="100%"></asp:Label> </div>
                                </div>
                                <div style="width:100%;float:left; padding-top:1px;"> 
                                    <div style="width:1%;float:left;"> &nbsp;</div>
                                    <div style="width:59%;float:left;"> Total Vat Amount</div>
                                    <div style="width:40%;float:left; text-align:right;"> <asp:Label runat="server" ID="lblANTotVatAmt" Text="0.00" Width="100%"></asp:Label> </div>
                                </div>
                                <div style="width:100%;float:left; padding-top:1px;"> 
                                    <div style="width:1%;float:left;"> &nbsp;</div>
                                    <div style="width:59%;float:left;"> Amount Finance</div>
                                    <div style="width:40%;float:left; text-align:right;"> <asp:Label runat="server" ID="lblANAmtFinance" Text="0.00" Width="100%"></asp:Label> </div>
                                </div>
                                <div style="width:100%;float:left; padding-top:1px;"> 
                                    <div style="width:1%;float:left;"> &nbsp;</div>
                                    <div style="width:59%;float:left;"> Service Charge</div>
                                    <div style="width:40%;float:left; text-align:right;"> <asp:Label runat="server" ID="lblANServiceCharge" Text="0.00" Width="100%"></asp:Label> </div>
                                </div>
                                <div style="width:100%;float:left; padding-top:1px;"> 
                                    <div style="width:1%;float:left;"> &nbsp;</div>
                                    <div style="width:59%;float:left;"> Interest Amount</div>
                                    <div style="width:40%;float:left; text-align:right;"> <asp:Label runat="server" ID="lblANIntAmt" Text="0.00" Width="100%"></asp:Label> </div>
                                </div>
                                <div style="width:100%;float:left; padding-top:1px;"> 
                                    <div style="width:1%;float:left;"> &nbsp;</div>
                                    <div style="width:49%;float:left;"> Tot. Hire Value</div>
                                    <div style="width:50%;float:left; text-align:right;"> <asp:Label runat="server" ID="lblANTotHireValue" Text="0.00" Width="100%"></asp:Label> </div>
                                </div>
                                <div style="width:100%;float:left; padding-top:1px;"> 
                                    <div style="width:1%;float:left;"> &nbsp;</div>
                                    <div style="width:49%;float:left;"> Comm. Amount</div>
                                    <div style="width:50%;float:left; text-align:right;"> <asp:Label runat="server" ID="lblANCommAmt" Text="0.00" Width="100%"></asp:Label> </div>
                                </div>
                                <div style="width:100%;float:left; padding-top:1px;"> 
                                    <div style="width:1%;float:left;"> &nbsp;</div>
                                    <div style="width:49%;float:left;"> Down Payment</div>
                                    <div style="width:50%;float:left; text-align:right;"> <asp:Label runat="server" ID="lblANDownPayment" Text="0.00" Width="100%"></asp:Label> </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    </Content>
                </asp:AccordionPane>
                <asp:AccordionPane runat="server" ID="AccordionPane1"   >
                <Header><div  class ="box" style="float: left; width: 100%; color:Black; font-size:11px; border-color:LightGray;border-style:solid; border-width:1px; padding-top:1px; ">Insurance Prediction &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  </div></Header>
                <Content>
                <%--Insurance--%>
                <div style=" float:left; width:100%;" >
                    <div style=" float:left; width:49%;" >
                        <div class="PanelHeader"> Current Insurance Detail </div>
                        <div style=" float:left; width:100%; padding-top:1px;" >
                            <div style=" float:left; width:1%;" >&nbsp;</div>
                            <div style=" float:left; width:60%;" >Policy No</div>
                            <div style=" float:left; width:39%; text-align:right;" > <asp:Label runat="server" ID="lblIOPolicyNo" Width="100%" Text="0.00"></asp:Label> </div>
                        </div>
                        <div style=" float:left; width:100%; padding-top:1px;" >
                            <div style=" float:left; width:1%;" >&nbsp;</div>
                            <div style=" float:left; width:60%;" >Cash Memo No</div>
                            <div style=" float:left; width:39%; text-align:right;" > <asp:Label runat="server" ID="lblIOCashMemoNo" Width="100%" Text="0.00"></asp:Label> </div>
                        </div>
                        <div style=" float:left; width:100%; padding-top:1px;" >
                            <div style=" float:left; width:1%;" >&nbsp;</div>
                            <div style=" float:left; width:60%;" >Amount</div>
                            <div style=" float:left; width:39%; text-align:right;" > <asp:Label runat="server" ID="lblIOAmount" Width="100%" Text="0.00"></asp:Label> </div>
                        </div>
                        <div style=" float:left; width:100%; padding-top:1px;" >
                            <div style=" float:left; width:1%;" >&nbsp;</div>
                            <div style=" float:left; width:60%;" >Comm. Rate</div>
                            <div style=" float:left; width:39%; text-align:right;" > <asp:Label runat="server" ID="lblIOCommRate" Width="100%" Text="0.00"></asp:Label> </div>
                        </div>
                        <div style=" float:left; width:100%; padding-top:1px;" >
                            <div style=" float:left; width:1%;" >&nbsp;</div>
                            <div style=" float:left; width:60%;" >Comm. Amount</div>
                            <div style=" float:left; width:39%; text-align:right;" > <asp:Label runat="server" ID="lblIOCommAmount" Width="100%" Text="0.00"></asp:Label> </div>
                        </div>
                        <div style=" float:left; width:100%; padding-top:1px;" >
                            <div style=" float:left; width:1%;" >&nbsp;</div>
                            <div style=" float:left; width:60%;" >Tax Rate</div>
                            <div style=" float:left; width:39%; text-align:right;" > <asp:Label runat="server" ID="lblIOTaxRate" Width="100%" Text="0.00"></asp:Label> </div>
                        </div>
                        <div style=" float:left; width:100%; padding-top:1px;" >
                            <div style=" float:left; width:1%;" >&nbsp;</div>
                            <div style=" float:left; width:60%;" >Tax Amount</div>
                            <div style=" float:left; width:39%; text-align:right;" > <asp:Label runat="server" ID="lblIOTaxAmount" Width="100%" Text="0.00"></asp:Label> </div>
                        </div>
                    </div>
                     <div style=" float:left; width:2%;" > &nbsp;</div>
                    <div style=" float:left; width:49%;" >
                        <div class="PanelHeader"> New Insurance Detail </div>
                        <div style=" float:left; width:100%; padding-top:1px;" >
                            <div style=" float:left; width:1%;" >&nbsp;</div>
                            <div style=" float:left; width:60%;" >Policy No</div>
                            <div style=" float:left; width:39%; text-align:right;" > <asp:Label runat="server" ID="lblINPolicyNo" Width="100%" Text="0.00"></asp:Label> </div>
                        </div>
                        <div style=" float:left; width:100%; padding-top:1px;" >
                            <div style=" float:left; width:1%;" >&nbsp;</div>
                            <div style=" float:left; width:60%;" >Cash Memo No</div>
                            <div style=" float:left; width:39%; text-align:right;" > <asp:Label runat="server" ID="lblINCashMemoNo" Width="100%" Text="0.00"></asp:Label> </div>
                        </div>
                        <div style=" float:left; width:100%; padding-top:1px;" >
                            <div style=" float:left; width:1%;" >&nbsp;</div>
                            <div style=" float:left; width:60%;" >Amount</div>
                            <div style=" float:left; width:39%; text-align:right;" > <asp:Label runat="server" ID="lblINAmount" Width="100%" Text="0.00"></asp:Label> </div>
                        </div>
                        <div style=" float:left; width:100%; padding-top:1px;" >
                            <div style=" float:left; width:1%;" >&nbsp;</div>
                            <div style=" float:left; width:60%;" >Comm. Rate</div>
                            <div style=" float:left; width:39%; text-align:right;" > <asp:Label runat="server" ID="lblINCommRate" Width="100%" Text="0.00"></asp:Label> </div>
                        </div>
                        <div style=" float:left; width:100%; padding-top:1px;" >
                            <div style=" float:left; width:1%;" >&nbsp;</div>
                            <div style=" float:left; width:60%;" >Comm. Amount</div>
                            <div style=" float:left; width:39%; text-align:right;" > <asp:Label runat="server" ID="lblINCommAmount" Width="100%" Text="0.00"></asp:Label> </div>
                        </div>
                        <div style=" float:left; width:100%; padding-top:1px;" >
                            <div style=" float:left; width:1%;" >&nbsp;</div>
                            <div style=" float:left; width:60%;" >Tax Rate</div>
                            <div style=" float:left; width:39%; text-align:right;" > <asp:Label runat="server" ID="lblINTaxRate" Width="100%" Text="0.00"></asp:Label> </div>
                        </div>
                        <div style=" float:left; width:100%; padding-top:1px;" >
                            <div style=" float:left; width:1%;" >&nbsp;</div>
                            <div style=" float:left; width:60%;" >Tax Amount</div>
                            <div style=" float:left; width:39%; text-align:right;" > <asp:Label runat="server" ID="lblINTaxAmount" Width="100%" Text="0.00"></asp:Label> </div>
                        </div>
                    </div>
                </div>
                </Content>
                </asp:AccordionPane>
            </Panes>
            </asp:Accordion>
        </div>
    </div>
    <div style=" float:left; width:100%;">
        <div style=" float:left; width:41%; height:190px; ">
            <%--Receipt Collection--%>
            <div style="float: left; width: 100%; padding-top: 2px;">
                    <div class="PanelHeader" style="height: 12px; border-bottom: 2px solid #9F9F9F;">
                        Issue Receipts
                    </div>
                    <div style="float: left; width: 100%; font-weight: normal; padding-top: 1px; padding-bottom: 1px;">
                        <div style="float:left;width:50%;"><asp:RadioButton ID="radSys" runat="server" Text="System" GroupName="PrintReceipt" Checked="true" OnCheckedChanged="radOptionCheckChange" AutoPostBack="true" /> </div>
                        <div style="float:left;width:50%;"><asp:RadioButton ID="radMan" runat="server" Text="Manual" GroupName="PrintReceipt" OnCheckedChanged="radOptionCheckChange"  AutoPostBack="true" /></div>
                    </div>
                    <div style="float: left; width: 100%; font-weight: normal; padding-top: 1px; padding-bottom: 1px;
                        height: 15px;">
                        <div style="float: left; width: 5%; background-color: #507CD1; text-align: left;
                            border-right: 1px solid white; color: White; height: 15px;">
                            Seq</div>
                        <div style="float: left; width: 20%; background-color: #507CD1; text-align: left;
                            border-right: 1px solid white; color: White; height: 15px;">
                            <%-- VAT Amt--%>
                            Prefix</div>
                        <div style="float: left; width: 30%; background-color: #507CD1; text-align: left;
                            border-right: 1px solid white; color: White; height: 15px;">
                            <%--Amt--%>
                            Receipt No
                        </div>
                        <div style="float: left; width: 25%; background-color: #507CD1; text-align: right;
                            border-right: 1px solid white; color: White; height: 15px;">
                            <%--Book--%>
                            Amount</div>
                        <div style="float: left; width: 6%; text-align: center; border-right: 1px solid white;
                            color: White; height: 15px;">
                        </div>
                    </div>
                    <div style="float: left; width: 100%; font-weight: normal; padding-top: 0px; padding-bottom: 0px;">
                        <div style="float: left; width: 5%; text-align: left; border-right: 1px solid white;
                            color: White; height: 15px;">
                            Seq</div>
                        <div style="float: left; width: 20%; border-right: 1px solid white;">
                            <asp:DropDownList ID="ddlPrefix" runat="server" CssClass="ComboBox" Width="95%">
                            </asp:DropDownList>
                        </div>
                        <div style="float: left; width: 27%; border-right: 1px solid white; height: 15px;">
                            <asp:TextBox runat="server" ID="txtReceiptNo" CssClass="TextBox" Style="text-align: right;"
                                Width="95.4%"></asp:TextBox>
                            &nbsp;</div>
                        <div style="float: left; width: 3%; border-right: 1px solid white; height: 15px;">
                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/icon_search.png" />
                        </div>
                        <div style="float: left; width: 25%; border-right: 1px solid white;">
                            <asp:TextBox runat="server" ID="txtReciptAmount" CssClass="TextBox" Style="text-align: right;"
                                Width="97%"></asp:TextBox></div>
                        <div style="float: left; width: 72px; border-right: 1px solid white;">
                            <asp:ImageButton ID="ImgBtnAddReceipt" runat="server" ImageUrl="~/Images/Add-16x16x16.ICO"
                                OnClick="ImgBtnAddReceipt_Click" Width="16px" />
                        </div>
                        <div style="float: left; width: 100%; font-weight: normal; padding-top: 0px; padding-bottom: 0px;">
                            <asp:Panel ID="Panel_ReceiptDet" runat="server" Height="69px" ScrollBars="Auto">
                                <asp:GridView ID="gvReceipts" runat="server" AutoGenerateColumns="False" CellPadding="1"
                                    CssClass="GridView" DataKeyNames="Sar_manual_ref_no,Sar_prefix" ForeColor="#333333"
                                    GridLines="Both" OnRowDeleting="gvReceipts_RowDeleting" ShowHeader="False" ShowHeaderWhenEmpty="true"
                                    OnRowDataBound="gvReceipts_OnRowDataBind">
                                    <AlternatingRowStyle BackColor="White" />
                                    <EmptyDataTemplate>
                                        <div style="width: 380px; text-align: center;">
                                            No data found
                                        </div>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:BoundField DataField="Sar_anal_7" HeaderText="Seq" ItemStyle-Width="25px" HeaderStyle-Width="25px" />
                                        <asp:BoundField DataField="Sar_prefix" HeaderText="Prefix" ItemStyle-Width="107px"
                                            HeaderStyle-Width="107px" />
                                        <asp:BoundField DataField="Sar_manual_ref_no" HeaderText="Receipt No" ItemStyle-Width="162px"
                                            HeaderStyle-Width="162px" />
                                        <asp:BoundField DataField="Sar_tot_settle_amt" HeaderText="Amount" ItemStyle-HorizontalAlign="Right"
                                            ItemStyle-Width="134px" HeaderStyle-Width="134px" />
                                        <asp:TemplateField ItemStyle-Width="80px" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgBtnDelRecipt" runat="server" CommandName="Delete" ImageUrl="~/Images/Delete.png"
                                                    Width="16px" />
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
                        </div>
                    </div>
                </div>
        </div>
        <div style=" float:left; width:1%;">&nbsp;</div>
        <div style=" float:left; width:57%;">
            <%--Payment--%>
            <div style="float: left; width: 100%; padding-top: 2px;">
                    <div class="PanelHeader" style="height: 12px; border-bottom: 2px solid #9F9F9F;">
                        Payment
                        <div style="float: right; width: 80%;">
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 20%; color: LightGreen; height: 14px;">
                                Paid Amount
                            </div>
                            <div style="float: left; width: 18%; color: LightGreen; text-align: right;">
                                <asp:Label runat="server" ID="lblPayPaid" Text="0.00"></asp:Label>
                            </div>
                            <div style="float: left; width: 18%;">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 20%; color: LightGreen; height: 14px;">
                                Balance Amount</div>
                            <div style="float: left; width: 18%; color: LightGreen; text-align: right;">
                                <asp:Label runat="server" ID="lblPayBalance" Text="0.00"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div style="float: left; width: 100%; padding-bottom: 2px; padding-top: 2px;">
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 13%;">
                            Pay Mode
                        </div>
                        <div style="float: left; width: 35%;">
                            <asp:DropDownList ID="ddlPayMode" Width="60%" runat="server" CssClass="ComboBox"
                                OnSelectedIndexChanged="PaymentType_LostFocus" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 15%;">
                            Amount
                        </div>
                        <div style="float: left; width: 29%;">
                            <asp:TextBox ID="txtPayAmount" runat="server" CssClass="TextBoxUpper" Width="50%"></asp:TextBox>
                            &nbsp;
                            <asp:Button ID="btnPayAdd" runat="server" CssClass="Button" Text="Add" OnClick="AddPayment" />
                        </div>
                    </div>
                    <div style="float: left; width: 100%; padding-bottom: 2px;">
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 13%;">
                            Remarks
                        </div>
                        <div style="float: left; width: 75%;">
                            <asp:TextBox ID="txtPayRemarks" runat="server" CssClass="TextBox" Width="100%"></asp:TextBox></div>
                    </div>
                    <div style="float: left; width: 50%;">
                        <div style="float: left; width: 100%; padding: 2px 2px 0px 0px;">
                            <%--Credit/Cheque/Bank Slip payment--%>
                            <div style="float: left; width: 100%; height: 100px;" runat="server" id="divCredit"
                                visible="false">
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 27%;">
                                        Card No</div>
                                    <div style="float: left; width: 72%; padding-bottom: 2px;">
                                        <asp:TextBox runat="server" ID="txtPayCrCardNo" CssClass="TextBox" Width="100%" MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 27%;">
                                        Bank
                                    </div>
                                    <div style="float: left; width: 50%; padding-bottom: 2px;">
                                        <asp:TextBox runat="server" ID="txtPayCrBank" CssClass="TextBox" Width="60%"></asp:TextBox><asp:ImageButton
                                            ID="ImageButton1" runat="server" ImageUrl="~/Images/icon_search.png" ImageAlign="Middle"
                                            OnClick="ImgBankSearch_Click" />
                                    </div>
                                </div>
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 27%;">
                                        Branch
                                    </div>
                                    <div style="float: left; width: 65%; padding-bottom: 2px;">
                                        <asp:TextBox runat="server" ID="txtPayCrBranch" CssClass="TextBox" Width="100%" MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 27%;">
                                        Card Type
                                    </div>
                                    <div style="float: left; width: 27%; padding-bottom: 2px;">
                                        <asp:DropDownList runat="server" ID="txtPayCrCardType" CssClass="ComboBox" Width="90%">
                                            <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="HSBC"></asp:ListItem>
                                            <asp:ListItem Text="AMEX"></asp:ListItem>
                                            <asp:ListItem Text="VISA"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 27%;">
                                        Expiry Date
                                    </div>
                                    <div style="float: left; width: 40%; padding-bottom: 2px;">
                                        <asp:TextBox runat="server" ID="txtPayCrExpiryDate" Enabled="false" CssClass="TextBox"
                                            Width="70%"></asp:TextBox>
                                        <asp:Image ID="btnImgCrExpiryDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png"
                                            ImageAlign="Middle" />
                                    </div>
                                </div>
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 27%;">
                                        Promotion
                                    </div>
                                    <div style="float: left; width: 67%; padding-bottom: 2px;">
                                        <asp:CheckBox runat="server" ID="chkPayCrPromotion" onclick="PromotionPeriod()" />
                                        &nbsp;&nbsp;&nbsp; Period &nbsp;
                                        <asp:TextBox runat="server" ID="txtPayCrPeriod" CssClass="TextBoxNumeric" Width="20%"
                                            MaxLength="2"></asp:TextBox>
                                        months
                                    </div>
                                </div>
                            </div>
                            <%--Advance receipt/Credit Note payment/Loyalty/Gift vouchas--%>
                            <div style="float: left; width: 100%;" runat="server" id="divAdvReceipt" visible="false">
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 27%;">
                                        Referance</div>
                                    <div style="float: left; width: 71%; padding-bottom: 2px;">
                                        <asp:TextBox runat="server" ID="txtPayAdvReceiptNo" CssClass="TextBox" Width="80%"></asp:TextBox><asp:ImageButton
                                            ID="ImageButton2" runat="server" ImageUrl="~/Images/icon_search.png" ImageAlign="Middle"
                                            OnClick="ImgBankSearch_Click" />
                                    </div>
                                </div>
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 27%;">
                                        Ref. Amount</div>
                                    <div style="float: left; width: 25%; padding-bottom: 2px;">
                                        <asp:TextBox runat="server" ID="txtPayAdvRefAmount" CssClass="TextBox" Width="80%"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div style="float: left; width: 1%;">
                        &nbsp;
                    </div>
                    <div style="float: left; width: 49%;">
                        <div style="float: left; width: 100%;">
                            <asp:Panel ID="pnlPay" runat="server" Height="120px" ScrollBars="Auto">
                                <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False" CssClass="GridView"
                                    CellPadding="2" ForeColor="#333333" GridLines="Both" DataKeyNames="sard_pay_tp,sard_settle_amt"
                                    OnRowDeleting="gvPayment_OnDelete" ShowHeaderWhenEmpty="true">
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <EmptyDataTemplate>
                                        <div style="width: 100%; text-align: center;">
                                            No data found
                                        </div>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnImgDelete" runat="server" ImageAlign="Middle" ImageUrl="~/Images/Delete.png"
                                                    Width="10px" Height="10px" CommandName="Delete" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField='sard_seq_no' HeaderText='' Visible="false" />
                                        <asp:BoundField DataField='sard_line_no' HeaderText='' Visible="false" />
                                        <asp:BoundField DataField='sard_receipt_no' HeaderText='' Visible="false" />
                                        <asp:BoundField DataField='sard_inv_no' HeaderText='' Visible="false" />
                                        <asp:BoundField DataField='sard_pay_tp' HeaderText='Payment Type' ItemStyle-Width="110px"
                                            HeaderStyle-Width="110px" />
                                        <asp:BoundField DataField='sard_ref_no' HeaderText='' Visible="false" />
                                        <asp:BoundField DataField='sard_chq_bank_cd' HeaderText='Bank' ItemStyle-Width="90px"
                                            HeaderStyle-Width="90px" />
                                        <asp:BoundField DataField='sard_chq_branch' HeaderText='Branch' ItemStyle-Width="90px"
                                            HeaderStyle-Width="90px" />
                                        <asp:BoundField DataField='sard_deposit_bank_cd' HeaderText='' Visible="false" />
                                        <asp:BoundField DataField='sard_deposit_branch' HeaderText='' Visible="false" />
                                        <asp:BoundField DataField='sard_credit_card_bank' HeaderText='' Visible="false" />
                                        <asp:BoundField DataField='sard_cc_tp' HeaderText='Card Type' />
                                        <asp:BoundField DataField='sard_cc_expiry_dt' HeaderText='Expiry Date' Visible="false" />
                                        <asp:BoundField DataField='sard_cc_is_promo' HeaderText='Promotion' Visible="false" />
                                        <asp:BoundField DataField='sard_cc_period' HeaderText='Period' Visible="false" />
                                        <asp:BoundField DataField='sard_gv_issue_loc' HeaderText='' Visible="false" />
                                        <asp:BoundField DataField='sard_gv_issue_dt' HeaderText='' Visible="false" />
                                        <asp:BoundField DataField='sard_settle_amt' HeaderText='Amount' />
                                        <asp:BoundField DataField='sard_sim_ser' HeaderText='' Visible="false" />
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
                </div>
        </div>
    </div>


</div>

<%--Ajax Control Area--%>
<div style="float:left;width :100%;"> 
<%--Modal pop-up --%>
<asp:ModalPopupExtender ID="ModalPopupExtItem" runat="server" CancelControlID="btnPopupCancel" ClientIDMode="Static"  PopupControlID="Panel_popUp" TargetControlID="btnHidden_popup" BackgroundCssClass="modalBackground"    PopupDragHandleControlID="divpopHeader"> </asp:ModalPopupExtender>
<div style="float: left; width: 100%;">
                <asp:Panel ID="Panel_popUp" runat="server" Width="500px" CssClass="ModalWindow" >
                <%-- PopUp Handler for drag and control --%>
                <div class="popUpHeader" id="div1" runat="server">
                    <div style="float: left; width: 80%" runat="server" id="div2"> Select Account </div>
                    <div style="float: left; width: 20%; text-align: right"> <asp:ImageButton ID="btnPopupCancel" runat="server" ImageUrl="~/Images/0032-01-16x16x16.ICO" />&nbsp;</div> 
                </div>
                <asp:Panel ID="PanelPopup_grv" runat="server"  >
                    <asp:GridView ID="grvMpdalPopUp" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="HPA_ACC_NO,HPA_ACC_CRE_DT" 
                    onselectedindexchanged="grvMpdalPopUp_SelectedIndexChanged">
                    <Columns>
                        <asp:CommandField SelectText="select" ShowSelectButton="True" />
                        <asp:BoundField DataField="HPA_ACC_NO" HeaderText="Account No." />
                        <asp:BoundField DataField="HPA_ACC_CRE_DT" HeaderText="Created Date" DataFormatString="{0:d}" />
                    </Columns>
                    </asp:GridView>
                </asp:Panel>
                </asp:Panel>
</div>
<asp:ModalPopupExtender ID="ModalPopupExtSch" runat="server" CancelControlID="imgBtnSchClose" ClientIDMode="Static"  PopupControlID="pnlPopSch" TargetControlID="imgBtnSchedule" BackgroundCssClass="modalBackground"    PopupDragHandleControlID="divpopHeaderSch"> </asp:ModalPopupExtender>
<div style="float: left; width: 100%;">
    <asp:Panel ID="pnlPopSch" runat="server" Width="350px" Height="260px" CssClass="ModalWindow" >
        <div class="popUpHeader" id="divpopHeaderSch" runat="server">
            <div style="float: left; width: 80%;" runat="server" > Schedule Prediction </div>
            <div style="float: left; width: 20%; text-align: right;"> <asp:ImageButton ID="imgBtnSchClose" runat="server" ImageUrl="~/Images/0032-01-16x16x16.ICO" />&nbsp;</div> 
            <div style="float: left; width: 100%;">
                <div style="float: left; width: 48%;"> 
                    <div style="float: left; width: 100%; padding-bottom:1px;" class="PanelHeader"> Old Schedule </div>
                    <asp:Panel runat="server" ID="pnlOldSch" >
                        <asp:GridView runat="server" ID="gvOldSch" AutoGenerateColumns="false" CssClass="GridView"    CellPadding="2" ForeColor="#333333" GridLines="Both" ShowHeaderWhenEmpty="true">
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <AlternatingRowStyle BackColor="White" />
                            <EmptyDataTemplate>
                            <div style="width: 100%; text-align: center;"> No data found </div> </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField='Hts_rnt_no' HeaderText='No' ItemStyle-Width="15px" HeaderStyle-Width="15px"  />
                                <asp:BoundField DataField='Hts_due_dt' HeaderText='Date'  DataFormatString="{0:d}" ItemStyle-Width="25px"  HeaderStyle-Width="25px"/>
                                <asp:BoundField DataField='Hts_rnt_val' HeaderText='Rental' ItemStyle-Width="25px" HeaderStyle-Width="25px" />
                            </Columns>
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                    </asp:Panel>
                </div>
                <div style="float: left; width: 3%;"> &nbsp; </div>
                <div style="float: left; width: 48%;"> 
                    <div style="float: left; width: 100%; padding-bottom:1px;" class="PanelHeader"> New Schedule </div>
                    <asp:Panel runat="server" ID="pnlNewSch" >
                        <asp:GridView runat="server" ID="gvNewSch" AutoGenerateColumns="false" CssClass="GridView"    CellPadding="2" ForeColor="#333333" GridLines="Both" ShowHeaderWhenEmpty="true">
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <AlternatingRowStyle BackColor="White" />
                            <EmptyDataTemplate>
                            <div style="width: 100%; text-align: center;"> No data found </div> </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField='Hts_rnt_no' HeaderText='No' ItemStyle-Width="10px" HeaderStyle-Width="10px"  />
                                <asp:BoundField DataField='Hts_due_dt' HeaderText='Date' DataFormatString="{0:d}"  ItemStyle-Width="20px" HeaderStyle-Width="20px" />
                                <asp:BoundField DataField='Hts_rnt_val' HeaderText='Rental' ItemStyle-Width="20px" HeaderStyle-Width="20px"  />
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
       </div>
    </asp:Panel>
</div>

</div>

<%-- Control Area --%>
<div style="display: none;">
    <asp:Button ID="btnPopUp" runat="server" ClientIDMode="Static" />
    <asp:Button ID="btnAccount" runat="server" OnClick="btn_validateACC_Click" />
    <asp:Button ID="btnHidden_popup" runat="server" Text="Button" />
    <asp:Button ID="btnBank" runat="server" OnClick="CheckBank" />
    <asp:Button ID="btnPayment" runat="server" OnClick="HpReverseCalculationForTally" />
</div>

</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
