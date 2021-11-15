<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConsignmentRN.aspx.cs" Inherits="FF.WebERPClient.WebForm5" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style3
        {
        }
        .style11
        {
            width: 627px;
            text-align: right;
        }
        .style9
        {
            font-family: Verdana;
            font-size: 11px;
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
        .style4
        {
            width: 418px;
        }
        .style2
        {
            width: 510px;
        }
        .style34
        {
            width: 86px;
        }
        .style35
        {
            width: 618px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
       <div><%-- <asp:BoundField DataField="Tus_usrseq_no" HeaderText="Batch #" />--%>
          <div>
          
              <asp:Panel ID="Panel1" runat="server" style="text-align: right">
                  <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="Button" 
                      Visible="False" />
                  &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="Button" 
                      onclick="btnSave_Click" />
                  &nbsp;<asp:Button ID="btnClear" runat="server" CssClass="Button" 
                      onclick="btnClear_Click" Text="Clear" />
&nbsp;
                  </asp:Panel>
          
          </div>
          <div>
                      </div>
          <div>
          
            <asp:Panel ID="PanelItemPopUp" runat="server" Height="320px" Width="642px" 
                BackColor="#E6E6E6" BorderColor="#E0E0E0" BorderWidth="2px" 
                CssClass="style9">
                <b>&nbsp;Search Header</b>&nbsp;
                <br />
                <div class="style11">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    &nbsp; &nbsp;
                    <asp:Button ID="btnPopupOk" runat="server" CssClass="Button" 
                        onclick="btnPopupOk_Click" Text="Ok" Width="50px" />
                    &nbsp;<asp:Button ID="btnPopupCancel" runat="server" CssClass="Button" 
                        onclick="btnPopupCancel_Click" Text="Cancel" />
                </div>
                <div align="right">
                    &nbsp;&nbsp;&nbsp;</div>
                &nbsp; Item Code:
                <asp:Label ID="lblPopupItemCode" runat="server" Font-Bold="True"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Bin Code:&nbsp;&nbsp;<asp:Label 
                    ID="lblPopupBinCode" runat="server" Font-Bold="True"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblPopupAmt" runat="server" 
                    style="text-align: right" ForeColor="#0033CC"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                
                <br />
                <br />
&nbsp;<asp:Label ID="Label11" runat="server" Text="Search by"></asp:Label>
                :
                <asp:DropDownList ID="ddlPopupSerial" runat="server" Height="16px" 
                    Width="111px" CssClass="style9">
                    <asp:ListItem>--select--</asp:ListItem>
                    <asp:ListItem>Serial No 1</asp:ListItem>
                    <asp:ListItem>Serial No 2</asp:ListItem>
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtPopupSearchSer" runat="server" Width="138px" 
                    CssClass="TextBox"></asp:TextBox>
                &nbsp; &nbsp;&nbsp;<asp:Button ID="btnPopupSarch" runat="server" CssClass="Button" 
                    onclick="btnPopupSarch_Click" Text="search" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <br />
                <br />
                &nbsp;<asp:Label ID="lblPopQty" runat="server" Text="Qty . . . . :" Visible="False"></asp:Label>
                &nbsp;<asp:TextBox ID="txtPopupQty" runat="server" ClientIDMode="Static" 
                    CssClass="TextBoxNumeric" Height="16px" Visible="False" Width="48px"></asp:TextBox>
                &nbsp;
                <asp:Button ID="btnPopupAutoSelect" runat="server" CssClass="Button" 
                    OnClick="btnPopupAutoSelect_Click" OnClientClick="SelectAuto()" 
                    Text="Auto Select" Width="81px" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<br />&nbsp;&nbsp;
                <div style="width: 608px">
                    <asp:Panel ID="popupGridPanel" runat="server" Height="172px" ScrollBars="Auto" 
                        style="margin-left: 15px; margin-bottom: 13px" Width="595px">
                        <asp:GridView ID="GridPopup" runat="server" AutoGenerateColumns="False" 
                            CellPadding="2" ForeColor="#333333" Height="45px" Width="673px">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkPopupSelectAll" runat="server"  ClientIDMode ="Static"
                                            onclick="SelectAll(this.id)" />
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
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            
            </asp:Panel>
          
          </div>
          <div>
          <div>
          
                          &nbsp;&nbsp;
          
                          <asp:Label ID="Label10" runat="server" Text="Scanned Batch:" 
                              style="font-weight: 700"></asp:Label>
                      &nbsp;
                          <asp:DropDownList ID="ddlScanBatches" runat="server" AutoPostBack="True" 
                              Height="16px" onselectedindexchanged="ddlScanBatches_SelectedIndexChanged" 
                              style="font-size: 11px; font-family: Verdana" Width="82px">
                          </asp:DropDownList>
                          <asp:Label ID="lblSeqno" runat="server"></asp:Label>
                          <br />
                      &nbsp;
          
                          <br />
          
          </div>
              <asp:Panel ID="Panel_itemDesc" runat="server" GroupingText="Scan Item" 
                  style="font-size: 11px; font-family: Verdana">
                  
                  
                
              <table class="style1">
                  <tr>
                      <td class="style34">
                      <asp:Panel ID="Panel_serSearch" runat="server" GroupingText="Search by serial" 
                              Width="279px">

                          <asp:Label ID="Label21" runat="server" Text="Supplier... :"></asp:Label>
                          <asp:TextBox ID="txtSupplierCd" runat="server" CssClass="TextBox"></asp:TextBox>
                          <asp:ImageButton ID="imgBtnSupplierCd" runat="server" ImageAlign="Middle" 
                              ImageUrl="~/Images/icon_search.png" OnClick="ImageBtnSupplier_Click" />
                          <br />
                          <br />
                          <asp:Label ID="Label22" runat="server" Text="Serial 1.... :"></asp:Label>
                          <asp:TextBox ID="txtSerial1" runat="server" CssClass="TextBox"></asp:TextBox>
                          <asp:Button ID="btnSerchSer" runat="server" CssClass="Button" 
                              onclick="btnSerchSer1_Click" Text="..." />
                          <br />
                          <br />
                          <asp:Label ID="Label23" runat="server" Text="Serial 2 ....:"></asp:Label>
                          <asp:TextBox ID="txtSerial2" runat="server" CssClass="TextBox"></asp:TextBox>
                          <asp:Button ID="btnSerchSer4" runat="server" CssClass="Button" 
                              onclick="btnSerchSer2_Click" Text="..." />
                          <br />

                  </asp:Panel>
                      </td>
                      <td class="style35">
                          <asp:Panel ID="Panel_itmSearch" runat="server" GroupingText="Add item">

                              <asp:Label ID="Label26" runat="server" Text="Bin Code ....:"></asp:Label>
                              <asp:DropDownList ID="ddlBinCode" runat="server" 
                                  style="font-size: 11px; font-family: Verdana">
                              </asp:DropDownList>
                              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              <asp:Label ID="Label19" runat="server" Text="Item Code...:"></asp:Label>
                              <asp:TextBox ID="txtItemCd" runat="server" CssClass="TextBox"></asp:TextBox>
                              <asp:ImageButton ID="imgBtnItemCd" runat="server" ImageAlign="Middle" 
                                  ImageUrl="~/Images/icon_search.png" OnClick="imgBtnItem_Click" />
                              &nbsp;&nbsp;&nbsp;&nbsp;
                              <asp:Button ID="btnAddSearch" runat="server" CssClass="Button" 
                                  onclick="btnAddSearch_Click" Text="..." />
                              &nbsp;
                              <asp:Button ID="btnAdd" runat="server" CssClass="Button" onclick="btnAdd_Click" 
                                  Text="Add" />
                              <br />
                              <br />
                              <asp:Label ID="Label29" runat="server" ForeColor="White" Text="Item Status"></asp:Label>
                              &nbsp;<asp:Label ID="lblItemStatus" runat="server" ForeColor="White" Text="CONSG"></asp:Label>
                              <br />
                              <asp:Label ID="Label27" runat="server" Text="Status.........:" Visible="False"></asp:Label>
                              <asp:Label ID="Label28" runat="server" Text="Consignment" Visible="False"></asp:Label>
                              &nbsp;<br />
                              <br />

                          </asp:Panel>
                          </td>
                  </tr>
                  <tr>
                      <td class="style3" colspan="2">
                          &nbsp;&nbsp;
                          <asp:Label ID="Label4" runat="server" Text="Description:"></asp:Label>
                          &nbsp;&nbsp;&nbsp;<asp:Label ID="lblDiscription" runat="server" ForeColor="Blue"></asp:Label>
                          &nbsp; &nbsp;</td>
                  </tr>
              </table>
              </asp:Panel>
              <asp:ModalPopupExtender ID="ModalPopupExtItem" runat="server" 
                CancelControlID="btnPopupCancel" PopupControlID="PanelItemPopUp" 
                TargetControlID="btnHidden_popup" ClientIDMode="Static" >
            </asp:ModalPopupExtender>
          
         
              <asp:UpdatePanel ID="UpdatePanel1" runat="server"> 
              <ContentTemplate>
              <asp:Panel ID="Panel_gridShow" runat="server" GroupingText=" " 
                      style="margin-top: 3px">
                  <div style="text-align: right; height: 11px;">
                      &nbsp;&nbsp; &nbsp;&nbsp;
                  </div>
                  <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="1" 
                      Width="935px" Height="170px" ScrollBars="Vertical" 
                      style="margin-top: 8px; font-family: Verdana;">
                      <asp:TabPanel runat="server" HeaderText="Items" ID="TabPanel_items" CssClass="tabPanelCss" ScrollBars="Both">
                        
                      
                          <HeaderTemplate>
                              Items
                          </HeaderTemplate>

                          <ContentTemplate>
                          
                          <div>
                          
                              <asp:Panel ID="PanelGridViewItems" runat="server" 
                                  Width="686px" style="font-family: Verdana">
                              <asp:GridView ID="GridViewItems" runat="server" CellPadding="2" 
                                  ForeColor="#333333" AutoGenerateColumns="False" style="text-align: center; margin-left: 20px;" 
                                  Width="646px" CssClass="GridView">
                                  <AlternatingRowStyle BackColor="White" />
                                  <Columns>
                                      <asp:BoundField DataField="Tus_itm_cd" HeaderText="Item Code" />
                                      <asp:BoundField DataField="Tus_itm_desc" HeaderText="Item Description" />
                                      <asp:BoundField DataField="Tus_itm_model" HeaderText="Model" />
                                      <asp:BoundField DataField="Tus_qty" HeaderText="Scanned Qty" />
                                      <asp:BoundField DataField="Tus_itm_stus" HeaderText="Status" />
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
                              
                              <br />
                              </div>
                          </ContentTemplate>

                      
                      </asp:TabPanel>
                      <asp:TabPanel ID="TabPanel_serials" runat="server" HeaderText="Serials" CssClass="tabPanelCss" ScrollBars="Both">
                          <HeaderTemplate>
                              Serials
                          </HeaderTemplate>
                          <ContentTemplate>
                          <div style="text-align: right; width: 903px; height: 37px;">
                              <asp:Panel ID="Panel_serGrid" runat="server" Width="890px">
                              </asp:Panel>
                          <asp:Button ID="btnSerialDel" runat="server" CssClass="Button" 
                          OnClick="btnSerialDel_Click" Text="Delete" /> &nbsp; &nbsp;
                      
                          </div>
                          <div style="font-family: Verdana">
                              <asp:Panel ID="PanelGridViewSerials" runat="server" 
                                  Width="911px" style="margin-left: 14px; font-family: Verdana;" 
                                  ScrollBars="Vertical">
                              <asp:GridView ID="GridViewSerials" runat="server" CellPadding="0" 
                                  ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" Width="877px" 
                                      CssClass="GridView">
                                  <AlternatingRowStyle BackColor="White" />
                                  <Columns>
                <asp:TemplateField>
                    <HeaderTemplate><asp:CheckBox ID="chkSelectAll" runat="server" onclick="SelectAll(this.id)" />All
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="SelectCheck" runat="server" />
                    </ItemTemplate>

                </asp:TemplateField>
                  <asp:BoundField HeaderText="  Serial No1  " DataField="Tus_ser_1" />
                <asp:BoundField HeaderText="Serial No2" />
                <asp:BoundField HeaderText="Item Code" DataField="Tus_itm_cd" />
                <asp:BoundField DataField="Tus_bin" HeaderText="Bin Code" />
                <asp:BoundField HeaderText="Item Description" DataField="Tus_itm_desc" />
                <asp:BoundField HeaderText="Model" DataField="Tus_itm_model" />
                <asp:BoundField HeaderText="Item Status" DataField="Tus_itm_stus" />
            
                <asp:TemplateField HeaderText="Add Remarks" Visible="False">
                  
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                        <asp:TextBox ID="txtNewRemark" runat="server" BorderColor="#99CCFF" Text='<%# Bind("Tus_new_remarks") %>'
                            style="font-size: 11px; font-family: Verdana"></asp:TextBox>
                    </ItemTemplate>
                    <FooterStyle CssClass="TextBox" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Add Remarks" Visible="False">
                  <ItemTemplate>
                  <%--<asp:BoundField HeaderText="Changed status" DataField="Tus_new_status" />--%>
                   <asp:Label ID="ChangedSt" runat="server" BorderColor="#99CCFF" Text='<%# Bind("Tus_new_status") %>'
                            ></asp:Label>
                  </ItemTemplate>
                  </asp:TemplateField>

               <asp:BoundField HeaderText="Qty" DataField="Tus_qty" />
               <asp:TemplateField HeaderText="Batch #" Visible="False">
                  <ItemTemplate>
                    <%-- <asp:BoundField DataField="Tus_usrseq_no" HeaderText="Batch #" />--%>
                     <asp:Label ID="UserSeqNo" runat="server" BorderColor="#99CCFF" Text='<%# Bind("Tus_usrseq_no") %>'
                            ></asp:Label>
                  </ItemTemplate>
                  </asp:TemplateField>
                 <asp:TemplateField HeaderText="SerialID" Visible="False">
                     <ItemTemplate>
                      <%--<asp:Label DataField="Tus_ser_id" HeaderText="Serial ID" ></asp:Label>--%>
                      <asp:Label ID="Serial_ID_" runat="server" BorderColor="#99CCFF" Text='<%# Bind("Tus_ser_id") %>'
                            ></asp:Label>
                      
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
                          <div>
                              <br />
                          </div>
                          <div>
                          
                          </div>
                          </ContentTemplate>
                      </asp:TabPanel>
                      
                  </asp:TabContainer>
              </asp:Panel>
            <div>
            <asp:Panel ID="Panel_final_state" runat="server" GroupingText=" Final fillings" 
                Height="105px" Width="323px" CssClass="style9">

                <table class="style1">
                    <tr>
                        <td class="style5">Date.............:
                            <%--<asp:Label ID="Label2" runat="server" Text=" Date...............:"></asp:Label>--%>
                        </td>
                        <td class="style6">
                            <asp:TextBox ID="txtDate" runat="server" CssClass="TextBox" 
                                Width="97px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExt" runat="server" Animated="true" 
                                EnabledOnClient="true" Format="dd/MM/yyyy" PopupPosition="BottomLeft" 
                                TargetControlID="txtDate">
                            </asp:CalendarExtender>
                            &nbsp;<asp:Label ID="Label14" runat="server" Text="dd/mm/yyyy"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            <asp:Label ID="Label9" runat="server" Text="Manual Ref.... :"></asp:Label>
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtManualRefNo" runat="server" CssClass="TextBox" 
                                Width="165px" MaxLength="25"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            <asp:Label ID="Label13" runat="server" Text="Remarks ...... :"></asp:Label>
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="TextBox" Width="165px" 
                                MaxLength="25"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                &nbsp;
                <br />

            </asp:Panel>
            </div>
              </ContentTemplate>
               </asp:UpdatePanel>
              <div style="display:none">
              
                  <asp:Button ID="btnHidden_popup" runat="server" Text="Button" />
              
              </div>
                      <script language="javascript" type="text/javascript">
                          function SelectAuto() {
                              var text = document.getElementById('<%= txtPopupQty.ClientID %>');
                              var val = text.value;
                              var len;
                              var myform = document.forms[0];
                              if (val != null) {
                                  len = val;
                              }
                              else {
                                  len = 0;
                              }

                              var Elen = myform.elements.length;
                              var counter = 0;

                              //               document.getElementById(Id).checked == true ? document.getElementById(Id).checked = false : document.getElementById(Id).checked = true;
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
                 function SelectAuto() {
                     var text = document.getElementById('<%= txtPopupQty.ClientID %>');
                     var val = text.value;
                     var len;
                     var myform = document.forms[0];
                     if (val != null) {
                         len = val;
                     }
                     else {
                         len = 0;
                     }

                     var Elen = myform.elements.length;
                     var counter = 0;

                     //               document.getElementById(Id).checked == true ? document.getElementById(Id).checked = false : document.getElementById(Id).checked = true;
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
                document.getElementById(Id).checked == true ? document.getElementById(Id).checked = false : document.getElementById(Id).checked = true; for (var i = 0; i < len; i++) { if (myform.elements[i].type == 'checkbox') { if (myform.elements[i].checked) { myform.elements[i].checked = false; } else { myform.elements[i].checked = true; } } }
            }

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
          
          </div>

          <div style="display: none;">
           <asp:Button ID="btnValidate_Supplier" runat="server" OnClick="btnValidate_Supplier_Click" />
                  
          </div>
       </div> <%--<asp:Label ID="Label2" runat="server" Text=" Date...............:"></asp:Label>--%>

    
</asp:Content>
