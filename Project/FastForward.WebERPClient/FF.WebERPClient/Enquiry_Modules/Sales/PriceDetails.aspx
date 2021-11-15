<%@ Page Title="Pricing Enquiry" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PriceDetails.aspx.cs" Inherits="FF.WebERPClient.Enquiry_Modules.Sales.PriceDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .GridView
        {}
        .style6
        {
            color: blue;
        }
    </style>
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


    function GetItemData() {
        var itemCode = document.getElementById("txtItemCode").value;
        if (itemCode != "") {
            FF.WebERPClient.LocalWebServices.CommonSearchWebServive.GetAllItemDetailsByItemCode(itemCode, onGetItemDataPass, onGetItemDataFail);
        }
        else { ClearItemFields(); }
    }
    //SucceededCallback method.
    function onGetItemDataPass(result) {
        if (result != null) {
            // alert("Code :" + result.Mi_cd + " Desc : " + result.Mi_longdesc + " Brand :" + result.Mi_brand + " Model : " + result.Mi_model);
           // document.getElementById("lblItemCD").value = result.Mi_longdesc;
          
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
<div style="text-align: right">

    &nbsp;
    &nbsp;</div>
<div style="float: left; width: 100%; text-align: right;">
    
    <asp:Button ID="Button1" runat="server" Text="Clear" CssClass="Button" 
        onclick="Button1_Click" />  

     <asp:Button ID="Button2" runat="server" Text="Close" CssClass="Button" 
        onclick="Button2_Click" />

</div>
 <div id="divMain" style="color:Black;">
 
  <div style="float: left; width: 100%;">
      <%-- ----------------------------------%>
      
  <div style="float: left; width: 20%;">
      <asp:Panel ID="Panel_PricingParas" runat="server" 
          GroupingText="Pricing Parameters">
          <div style="padding: 0.5px; float: left; width: 1%;"></div>
  <div style="float: left; width: 100%;">
    <div  style="float: left; width: 30%;">
     <div style="float: left; width: 1%;">&nbsp;</div>
        <asp:Label ID="Label1" runat="server" Text="Price Book"></asp:Label> 
       </div>
         <div style="float: left; width: 65%;">
        <asp:TextBox ID="txtPriceBook" runat="server" CssClass="TextBox" Width="81px"></asp:TextBox>
             <asp:ImageButton ID="imgBtnPBsearch" runat="server" 
                 ImageUrl="~/Images/icon_search.png" onclick="imgBtnPBsearch_Click" />
        </div>
    </div>
    <div style="float: left; width: 100%;">
    <div  style="float: left; width: 40%;">
     <div style="float: left; width: 1%;">&nbsp;&nbsp; </div>
        </div>
         <div style="float: left; width: 49%;">
             &nbsp;</div>
    </div>
    <div style="float: left; width: 100%;">
    <%-- <div style="float: left; width: 1%;">&nbsp;&nbsp;</div>--%>
      <div  style="float: left; width: 30%;"><asp:Label ID="Label2" runat="server" 
              Text="Level"></asp:Label></div>
         <div style="float: left; width: 65%;">
       <asp:TextBox ID="txtLevel" runat="server" CssClass="TextBox" Width="81px"></asp:TextBox> 
        <asp:ImageButton ID="imgBtnLevelSearch" runat="server" 
                 ImageUrl="~/Images/icon_search.png" onclick="imgBtnLevelSearch_Click" />
        </div>
        
    </div>
    <div style="float: left; width: 100%;">
      <div  style="float: left; width: 30%;"><asp:Label ID="Label13" runat="server" 
              Text="Circular"></asp:Label>
        </div>
         <div style="float: left; width: 65%;">
             <asp:TextBox ID="txtCircular" runat="server" CssClass="TextBox" 
                 Width="81px"></asp:TextBox>
                 <asp:ImageButton ID="imgBtnCircularSarch" runat="server" 
                 ImageUrl="~/Images/icon_search.png" onclick="imgBtnCircularSarch_Click" />
        </div>
        
    </div>
    <div style="padding: 13px; float: left; width: 70%;" ID="div1" runat="server">
          
            &nbsp;&nbsp;</div>    
      </asp:Panel>
    
    
  </div>
  
<%--Credit/Cheque/Bank Slip payment--%>

   <div style="float: left; width: 25%;">
    <asp:Panel ID="Panel_ItemCategories" runat="server" GroupingText="Item Categories">
    <%--<div style="padding: 2px; float: left; width: 1%;"></div>--%>
    <div style="float: left; width: 100%;">
                
    <div  style=" padding: 2px; float: left; width: 39%;">
        <asp:Label runat="server" Text="Item Code"></asp:Label> </div>
         <div style="float: left; width: 49%;">
        <asp:TextBox ID="txtItemCode" runat="server" CssClass="TextBoxUpper" Width="81px"></asp:TextBox>  
             <asp:ImageButton ID="imgBtnSItemcd" runat="server" 
                 ImageUrl="~/Images/icon_search.png" onclick="imgBtnSItemcd_Click" />
        </div>
    </div>
    <div style="float: left; width: 100%;">
    <%-- <div style="float: left; width: 1%;">&nbsp;&nbsp;</div>--%>
        <div  style="float: left; width: 40%; text-align: left;">
          <asp:Label ID="Label4" runat="server" 
              Text="Description   " Font-Size="Smaller" ForeColor="Blue"></asp:Label>
           </div>
         <div style="float: left; width: 59%;">:
             <asp:Label ID="lblDescription" runat="server" Font-Size="X-Small" 
                 ForeColor="Blue"></asp:Label>
             &nbsp;</div>
    </div>
    <div>
     <%--<div style="float: left; width: 1%;">&nbsp;&nbsp;</div>--%>
      <div  style="float: left; width: 40%; text-align: left;">
          <asp:Label ID="Label9" runat="server" 
              Text="Model   " Font-Size="Smaller" ForeColor="Blue"></asp:Label></div>
         <div style="float: left; width: 49%;">:
             <asp:Label ID="lblModal" runat="server" Font-Size="Smaller" ForeColor="Blue"></asp:Label>
             &nbsp;</div>
    </div>
    <div>
     <%--<div style="float: left; width: 1%;">&nbsp;&nbsp;</div>--%>
      <div  style="float: left; width: 40%; text-align: left;">
          <asp:Label ID="Label12" runat="server" 
              Text="Improted Vat rt.   " Font-Size="Smaller" ForeColor="Blue"></asp:Label></div>
         <div style="float: left; width: 49%; margin-bottom: 2px;">:
             <asp:Label ID="lblVatRate" runat="server" Font-Size="Smaller" ForeColor="Blue"></asp:Label>
             &nbsp;</div>
    </div>
    <div>
    <%-- <div style="float: left; width: 1%;">&nbsp;&nbsp;</div>--%>
      <div  style="float: left; width: 40%;">
          <asp:Label ID="Label5" runat="server" 
              Text="Main Category"></asp:Label></div>
         <div style="float: left; width: 49%;">
            <asp:TextBox ID="txtCate1" runat="server" CssClass="TextBox" Width="81px"></asp:TextBox>
        </div>
    </div>
    <div>
    <%-- <div style="float: left; width: 1%;">&nbsp;&nbsp;</div>--%>
      <div  style="float: left; width: 40%;">
          <asp:Label ID="Label11" runat="server" 
              Text="Category"></asp:Label></div>
         <div style="float: left; width: 49%;">
            <asp:TextBox ID="txtCate2" runat="server" CssClass="TextBox" Width="81px"></asp:TextBox>
        </div>
    </div>
    <div>
   <%--  <div style="float: left; width: 1%;">&nbsp;&nbsp;</div>--%>
      <div  style="float: left; width: 40%;">
          <asp:Label ID="Label15" runat="server" 
              Text="Sub Category"></asp:Label></div>
         <div style="float: left; width: 49%;">
             <asp:TextBox ID="txtCate3" runat="server" CssClass="TextBox" Width="81px"></asp:TextBox>
        </div>
    </div>
    
       </asp:Panel>
       
    
   </div>
      <%--<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" CssClass="GridView" ForeColor="#333333" Width="100%">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField HeaderText="Scheme Code" />
                        <asp:BoundField HeaderText="Price Book" />
                        <asp:BoundField HeaderText="Price Book Level" />
                        <asp:BoundField HeaderText="Category" />
                        <asp:BoundField HeaderText="From Date" />
                        <asp:BoundField HeaderText="To Date" />
                        <asp:BoundField HeaderText="Inst. Comm" />
                        <asp:BoundField HeaderText="Promotion" />
                        <asp:BoundField HeaderText="Category" />
                        <asp:BoundField HeaderText="Brand" />
                        <asp:BoundField HeaderText="Item Code" />
                        <asp:BoundField HeaderText="Serial" />
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
                </asp:GridView>--%>
    <div style="float: left; width: 45%;">
    
    <asp:Panel ID="Panel_AddParas" runat="server" GroupingText="Additional Parameters">
    <div style="padding: 0.5px; float: left; width: 100%;"></div>
    <div style="float: left; width: 48%;">
      <div style="float: left; width: 30%;">
       
          <asp:Label ID="Label6" runat="server" Text="Customer"></asp:Label>   
         
      </div>
      <div style="float: left; width: 70%;">

          <asp:TextBox ID="txtCustomer" runat="server" CssClass="TextBox" Width="80%"></asp:TextBox>
          <asp:ImageButton ID="imgBtnCustSearch" runat="server" 
              ImageUrl="~/Images/icon_search.png" onclick="imgBtnCustSearch_Click" />
      </div>

      <div></div>
      <div style="float: left; width: 35%;">
       
       
         
          &nbsp;</div>
      <div style="float: left; width: 60%;">

         

          &nbsp;</div>
        <div style="float: left; width: 30%;">
            <asp:Label ID="Label7" runat="server" Text="Type"></asp:Label>
        </div>
        <div style="float: left; width: 70%;">
            <asp:TextBox ID="txtType" runat="server" CssClass="TextBox" Width="80%"></asp:TextBox>
            <asp:ImageButton ID="imgBtnTypeSearch" runat="server" 
                ImageUrl="~/Images/icon_search.png" onclick="imgBtnTypeSearch_Click" />
        </div>
      
        <div style="padding: 2.5px; float: left; width: 150%;">
          
          <div style="padding: 2px; float: left; width: 100%;">
            &nbsp;<asp:CheckBox ID="chkScheme" runat="server" Text="Scheme" 
                  AutoPostBack="True" oncheckedchanged="chkScheme_CheckedChanged" />
          </div>
          <div style="padding: 3px; float: left; width: 100%;" runat="server" 
                ID="divSchemeCD">
              <asp:Label ID="Label10" runat="server" Text="Scheme Code: "></asp:Label>
            <asp:TextBox ID="txtSchemeCD" runat="server" CssClass="TextBox" 
                Width="29%"></asp:TextBox>
            <asp:ImageButton ID="imgBtnSchemSearch" runat="server" 
                ImageUrl="~/Images/icon_search.png" onclick="imgBtnSchemSearch_Click" />
          </div>
        </div>
        <div style="padding: 13px; float: left; width: 150%;" ID="divpadding" runat="server">
          
            &nbsp;&nbsp;</div>
    </div>
     <%-- ----------------------------------%>
     
    <div style="float: left; width: 50%;">
    <div  style="float: left; width: 35%; height: 17px;">
     
     <div style="padding: 1px; float: left; width: 98%;">
         <asp:RadioButton ID="rdoWithHistory" runat="server" Text="history" 
             GroupName="DateSelect" oncheckedchanged="rdoWithHistory_CheckedChanged" 
             AutoPostBack="True" />
        </div>
     </div>
    <div style="padding: 1px; float: left; width: 60%;">
        <asp:RadioButton ID="rdoDateRange" runat="server" Text="DateRange" 
            AutoPostBack="True" GroupName="DateSelect" 
            oncheckedchanged="rdoDateRange_CheckedChanged" />
      </div> 
    </div>

    <div style="padding: 2px; float: left; width: 50%;" ID="divFromDate" runat="server">
     <div  style="float: left; width: 35%; height: 17px;">
     <div style="float: left; width: 1%;">&nbsp;</div>
     <div style="float: left; width: 98%;">
         <asp:Label ID="Label8" runat="server" Text="From date:"></asp:Label>
         </div>
     </div>
    <div style="float: left; width: 60%;">
        <asp:TextBox ID="txtFromDt" runat="server" CssClass="TextBox" Width="81px"></asp:TextBox>
        <asp:CalendarExtender ID="txtFromDt_CalendarExtender" runat="server" 
            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFromDt">
        </asp:CalendarExtender>
      </div> 
    
    </div>
    <div style="padding: 2px; float: left; width: 50%;" ID="divTodate" runat="server">
     <div  style="float: left; width: 35%; height: 17px;">
     <div style="float: left; width: 1%;">&nbsp;</div>
     <div style="float: left; width: 98%;">
         <asp:Label ID="Label3" runat="server" Text="Upto date:"></asp:Label> </div>
     </div>
    <div style="float: left; width: 60%;">
        <asp:TextBox ID="txtToDt" runat="server" CssClass="TextBox" Width="81px"></asp:TextBox>
         <asp:CalendarExtender ID="toDt_CalendarExtender" runat="server" 
            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtToDt">
        </asp:CalendarExtender>  
      </div> 
    
    </div>
    <div style="padding: 2px; float: left; width: 50%;" ID="div_Asat" runat="server">
     <div  style="float: left; width: 35%; height: 17px;">
     <div style="float: left; width: 1%;">&nbsp;</div>
     <div style="float: left; width: 98%; text-align: center;">
         <asp:Label ID="lblAsatDt" runat="server" Text="As at :"></asp:Label> </div>
     </div>
    <div style="float: left; width: 60%;">
        <asp:TextBox ID="txtAsAtDt" runat="server" CssClass="TextBox" Width="81px"></asp:TextBox>  
         <asp:CalendarExtender ID="AsAtDtCalendarExtender" runat="server" 
            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtAsAtDt">
        </asp:CalendarExtender>  
      </div> 
    
    </div>
       </asp:Panel>
    </div>

    <%--Credit/Cheque/Bank Slip payment--%>
     <div style="float: left; width: 10%; margin-top: 3px;">
     <asp:Panel ID="Panel_Print" runat="server" GroupingText=" ">
       <div style="float: left; width: 100%;">
        <div style="float: left; width: 45% ; ">
            <asp:ImageButton ID="imgBtnSearch" runat="server" 
                ImageUrl="~/Images/icon_search.png" onclick="imgBtnSearch_Click" Width="60%" />
        </div>
       <%-- <div style="float: left; width: 49%;" class="style6"></div>--%>
       </div>
       </asp:Panel>
     </div>
  </div>

  <%--<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" CssClass="GridView" ForeColor="#333333" Width="100%">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField HeaderText="Scheme Code" />
                        <asp:BoundField HeaderText="Price Book" />
                        <asp:BoundField HeaderText="Price Book Level" />
                        <asp:BoundField HeaderText="Category" />
                        <asp:BoundField HeaderText="From Date" />
                        <asp:BoundField HeaderText="To Date" />
                        <asp:BoundField HeaderText="Inst. Comm" />
                        <asp:BoundField HeaderText="Promotion" />
                        <asp:BoundField HeaderText="Category" />
                        <asp:BoundField HeaderText="Brand" />
                        <asp:BoundField HeaderText="Item Code" />
                        <asp:BoundField HeaderText="Serial" />
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
                </asp:GridView>--%>
  <div style="float: left; width: 100%;">
     
     </div>
   <div style="float: left; width: 100%;">
       <asp:Panel ID="Panel_popUp" runat="server" BackColor="Silver" Width="40%" 
           BorderColor="#333399" BorderWidth="2px">
         <div style="padding: 0.5px; float: left; width: 100%; text-align: right;">
         <div style="padding: 0.5px; float: left; width: 92%; height: 13px;">
          <asp:Button ID="btnPopupCancel" runat="server" Text="Close" 
                 CssClass="Button" />
         </div>
            
           </div>
            <div>
            <div style="float: left; width: 6%; height: 13px;"></div>
                <asp:GridView ID="grvPopUpCombines" runat="server" CellPadding="4" 
                    CssClass="GridView" ForeColor="#333333" AutoGenerateColumns="False" 
                    Width="90%" ShowHeaderWhenEmpty="True">

                    <AlternatingRowStyle BackColor="White" />
                    <EmptyDataTemplate > <div style="width:100%; text-align:center;"> No data found </div> </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="Sapc_itm_cd" HeaderText="Item Code" />
                        <asp:BoundField DataField="Mi_longdesc" HeaderText="Item Description" />
                        <asp:BoundField DataField="Mi_model" HeaderText="Model" />
                        <asp:BoundField DataField="SAPC_QTY" HeaderText="Qty" />
                        <asp:BoundField DataField="Sapc_price" HeaderText="Price" />
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
         <div style="float: left; width: 100%;">&nbsp;</div>
       </asp:Panel>
   
   </div>
  <%--start grid--%>
  
     <div style="float: left; width: 100%;">
         <asp:Panel ID="Panel_grvDetail" runat="server" Height="245px" 
             ScrollBars="Vertical" Width="100%">
         <asp:GridView ID="grvPriceDetail" runat="server" CellPadding="3" 
          CssClass="GridView" ForeColor="#333333" AutoGenerateColumns="False" 
                 AllowPaging="True" 
                 Height="100px" PageSize="50" 
                 DataKeyNames="SARPT_INDI,SAPD_SEQ_NO,sapd_itm_cd" 
                 onrowdeleting="grvPriceDetail_RowDeleting" 
                 onpageindexchanging="grvPriceDetail_PageIndexChanging" 
                 ShowHeaderWhenEmpty="True" AllowSorting="True" 
                 onsorting="grvPriceDetail_Sorting">
          <AlternatingRowStyle BackColor="White" />
           <EmptyDataTemplate > <div style="width:100%; text-align:center;"> No data found </div> </EmptyDataTemplate>
             <Columns>
                 <asp:BoundField HeaderText="#"/>
                 <asp:BoundField HeaderText="Circular" DataField="SAPD_CIRCULAR_NO" />
                 <asp:TemplateField HeaderText="Type">
                     <ItemTemplate>
                         <asp:Label ID="Label1" runat="server"></asp:Label>
                         <br />
                         <asp:LinkButton ID="btnGrvLinkType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SARPT_CD") %>' CommandName="Delete" ></asp:LinkButton>
                     </ItemTemplate>
                     <EditItemTemplate>
                         <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                     </EditItemTemplate>
                 </asp:TemplateField>
                 <asp:BoundField HeaderText="Price Book" DataField="sapd_pb_tp_cd" />
                 <asp:BoundField HeaderText="Level" DataField="sapd_pbk_lvl_cd" />
                 <asp:BoundField HeaderText="Item Code" DataField="sapd_itm_cd" 
                     SortExpression="sapd_itm_cd" />
                 <asp:BoundField HeaderText="Status" DataField="SAPD_PRICE_STUS" />
                 <asp:BoundField HeaderText="Active For" DataField="SAPD_CUSTOMER_CD" />
                 <asp:BoundField HeaderText="Valid From" DataField="sapd_from_date" DataFormatString="{0:d}"/>
                 <asp:BoundField HeaderText="Valid To" DataField="sapd_to_date"  DataFormatString="{0:d}"/>
                 <asp:BoundField HeaderText="Qty from" DataField="sapd_qty_from" />
                 <asp:BoundField HeaderText="Qty To" DataField="sapd_qty_to" />
              <%--   <asp:BoundField DataField="MICT_STUS" HeaderText="Item Status" />--%>
                 <asp:BoundField HeaderText="Price (Vat Ex)" DataField="SAPD_ITM_PRICE" />
                 <asp:BoundField HeaderText="Price (Vat In)" DataField="WITH_TAX" />
                 <asp:BoundField HeaderText="Times" DataField="SAPD_NO_OF_TIMES" />
                 <asp:BoundField HeaderText="Used" DataField="SAPD_NO_OF_USE_TIMES" />
                 
                 <asp:ButtonField DataTextField="SARPT_INDI" HeaderText="SARPT_INDI" Visible="false" />
                 
                 <asp:BoundField DataField="SAPD_PB_SEQ" HeaderText="priceBook no" />
                 
             </Columns>
          <EditRowStyle BackColor="#2461BF" />
          <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
          <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
             <PagerSettings PageButtonCount="100" Position="Top" />
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
     <div>
      <div style="height: 16px; background-color: #1E4A9F; color: White; width: 98%; float: left;">
                        Schemes 
                       <asp:Image Detailtyle="float: left;" runat="server" ID="Image4" ImageUrl="~/Images/16 X 16 DownArrow.jpg" ImageAlign="Right" /></div>
        
        <asp:CollapsiblePanelExtender ID="CPE_scheme" runat="server" TargetControlID="Panel_col_schemes"
                        CollapsedSize="0" ExpandedSize="155" Collapsed="True" ExpandControlID="Image4"
                        CollapseControlID="Image4" AutoCollapse="False" AutoExpand="False" ScrollContents="false"
                        ExpandDirection="Vertical" ImageControlID="Image4" ExpandedImage="~/Images/16 X 16 UpArrow.jpg"
                        CollapsedImage="~/Images/16 X 16 DownArrow.jpg">
                    </asp:CollapsiblePanelExtender>
          <div  style="float: left; width: 100%; padding-top:3px">
            <asp:Panel ID="Panel_col_schemes" runat="server" Height="194px" GroupingText="">

            <%--<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" CssClass="GridView" ForeColor="#333333" Width="100%">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField HeaderText="Scheme Code" />
                        <asp:BoundField HeaderText="Price Book" />
                        <asp:BoundField HeaderText="Price Book Level" />
                        <asp:BoundField HeaderText="Category" />
                        <asp:BoundField HeaderText="From Date" />
                        <asp:BoundField HeaderText="To Date" />
                        <asp:BoundField HeaderText="Inst. Comm" />
                        <asp:BoundField HeaderText="Promotion" />
                        <asp:BoundField HeaderText="Category" />
                        <asp:BoundField HeaderText="Brand" />
                        <asp:BoundField HeaderText="Item Code" />
                        <asp:BoundField HeaderText="Serial" />
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
                </asp:GridView>--%>
                <asp:Panel ID="Panel_schemeGrid" runat="server" Height="166px" 
                    ScrollBars="Both">
                    
                <asp:GridView ID="grvSchems" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" CssClass="GridView" ForeColor="#333333" Width="1021px" ShowHeaderWhenEmpty="True">
                    <AlternatingRowStyle BackColor="White" />
                    <EmptyDataTemplate > <div style="width:100%; text-align:center;"> No data found </div> </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="hpc_sch_cd" HeaderText="Scheme Code" />
                        <asp:BoundField DataField="hpc_pb" HeaderText="Price Book" />
                        <asp:BoundField DataField="hpc_pb_lvl" HeaderText="Price Book Level" />
                        <asp:BoundField DataField="hpc_comm_cat" HeaderText="Category" />
                        <asp:BoundField DataField="hpc_from_dt" HeaderText="From Date"  DataFormatString="{0:d}"/>
                        <asp:BoundField DataField="hpc_to_dt" HeaderText="To Date"  DataFormatString="{0:d}"/>
                        <asp:BoundField DataField="hpc_inst_comm" HeaderText="Inst. Comm" />
                        <asp:BoundField DataField="hpc_pro" HeaderText="Promotion" />
                        <asp:BoundField DataField="hpc_cat" HeaderText="Category" />
                        <asp:BoundField DataField="hpc_brd" HeaderText="Brand" />
                        <asp:BoundField DataField="hpc_itm" HeaderText="Item Code" />
                        <asp:BoundField DataField="hpc_ser" HeaderText="Serial" />
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
       
     </div>
    <div style="display: none">
        <asp:Button ID="btn_hiddenn" runat="server" Text="Button" />
    </div>
      <asp:ModalPopupExtender ID="ModalPopupExtItem" runat="server" 
                                CancelControlID="btnPopupCancel" ClientIDMode="Static" 
                                PopupControlID="Panel_popUp" 
         TargetControlID="btn_hiddenn" Drag="True">
                            </asp:ModalPopupExtender>     
    
 </div>
</asp:Content>
