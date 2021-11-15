<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FixAssetOrAdhocRequestAndApprove.aspx.cs" Inherits="FF.WebERPClient.Inventory_Module.FixAssetOrAdhocRequestAndApprove" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
    function SelectAll(id) {

        var grid = document.getElementById("<%= grvAvailableSerials.ClientID %>");
        //alert(grid);
        var cell;


        if (grid.rows.length > 0) {

            for (i = 1; i < grid.rows.length; i++) {

                cell = grid.rows[i].cells[0];

                for (j = 0; j < cell.childNodes.length; j++) {
                    if (cell.childNodes[j].type == "checkbox") {

                        cell.childNodes[j].checked = id.checked;
                    }
                }
            }
        }
    }
    function SelectAll2(id) {

        var grid = document.getElementById("<%= grvApproveItms.ClientID %>");
        //alert(grid);
        var cell;


        if (grid.rows.length > 0) {

            for (i = 1; i < grid.rows.length; i++) {

                cell = grid.rows[i].cells[0];

                for (j = 0; j < cell.childNodes.length; j++) {
                    if (cell.childNodes[j].type == "checkbox") {

                        cell.childNodes[j].checked = id.checked;
                    }
                }
            }
        }
    }
    function onblurFire(e, buttonid) {
        var evt = e ? e : window.event;
        var bt = document.getElementById(buttonid);
        if (bt) {

            bt.click();
            return false;


        }
    }
    function onFocusFire(e, buttonid) {
        var evt = e ? e : window.event;
        var bt = document.getElementById(buttonid);
        var textBox = document.getElementById("<%= txtRefNo.ClientID %>");
        if (bt) {
            var textVal = textBox.value;
            if (textVal != '') {
                            
                bt.click();
            }
            else {
                

            }
           
            return false;


        }
    }
  </script>
  <div style="display: none">  <%--style="display: none"--%>
      <asp:Button ID="btnItmCd" runat="server" Text="Button" 
          onclick="btnItmCd_Click" />
      <asp:Button ID="btnUnitPrice" runat="server" Text="Button" 
          onclick="btnUnitPrice_Click" />
  </div>
    <div style="float: left; width: 100%; text-align: right;">
        <div style="float: left; width: 100%; text-align: right;">
            <asp:Button ID="btn_SendReq" runat="server" Text="Send Request" CssClass="Button"
                OnClick="btn_SendReq_Click" />
            <asp:Button ID="btn_CLEAR" runat="server" Text="Clear" CssClass="Button" OnClick="btn_CLEAR_Click" />
             <asp:Button ID="btn_PRINT" runat="server" Text="Print" CssClass="Button" 
                onclick="btn_PRINT_Click" />
            <asp:Button ID="btn_CLOSE" runat="server" Text="Close" CssClass="Button" OnClick="btn_CLOSE_Click" />
        </div>
        <div style="float: left; width: 100%; text-align: right;">
            <div style="float: left; width: 100%; text-align: right;">
                <asp:Panel ID="Panel1" runat="server" GroupingText=" ">
                    <div style="float: left; width: 100%; text-align: right;">
                    </div>
                    <div style="padding: 2.0px; float: left; width: 25%; text-align: left;">
                        <asp:Label ID="Label1" runat="server" Text="Transaction Type: " ForeColor="Blue"></asp:Label>
                        <asp:DropDownList ID="ddlReuestType" runat="server" CssClass="ComboBox" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlReuestType_SelectedIndexChanged">
                            <asp:ListItem Value="2"> FIXED ASSETS</asp:ListItem>
                            <asp:ListItem Value="1">FGAP</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div style="padding: 2.0px; float: left; width: 20%; text-align: left;" id="divLoc"
                        runat="server">
                        <asp:Label ID="Label23" runat="server" Text="Location :" ForeColor="Blue"></asp:Label>
                        <asp:TextBox ID="txtSendLoc" runat="server" CssClass="TextBoxUpper" Width="50%"></asp:TextBox>
                    </div>
                    <div style="float: left; width: 20%; text-align: left;">
                        <asp:Label ID="Label17" runat="server" Text="Action :" ForeColor="Blue"></asp:Label>
                        <asp:DropDownList ID="ddlAction" runat="server" CssClass="ComboBox">
                            <asp:ListItem Value="Request">New Request</asp:ListItem>
                            <asp:ListItem Value="Approve">Approve Request</asp:ListItem>
                            <asp:ListItem>Confirmation</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div style="float: left; width: 20%; text-align: left;">
                        <asp:Label ID="Label2" runat="server" Text="Ref. No : " ForeColor="Blue"></asp:Label>
                        <asp:TextBox ID="txtRefNo" runat="server" CssClass="TextBoxUpper" 
                            AutoPostBack="True" ontextchanged="txtRefNo_TextChanged"></asp:TextBox>
                        &nbsp;
                       
                    </div>
                    <div style="float: left; width: 5%; text-align: left;">
                     <asp:Button ID="btnRefOk" runat="server" CssClass="Button" Font-Bold="True" Text="OK"
                            OnClick="btnRefOk_Click" />
                    </div>
                    <div style="float: left; width: 5%; text-align: center;">
                        <asp:ImageButton ID="ImgSearchRefNo" runat="server" ImageUrl="~/Images/icon_search.png"
                            OnClick="ImgSearchRefNo_Click" />
                    </div>
                </asp:Panel>
            </div>
            <%--            <div style="float: left; width: 100%; text-align: right;">
                <asp:Panel ID="Panel2" runat="server" GroupingText=" ">
                    <div style="float: left; width: 100%; text-align: right;">
                    </div>
                    <div style="float: left; width: 100%; text-align: left;">
                        <asp:Panel ID="Panel9" runat="server" GroupingText="Search Status">
                            <div style="float: left; width: 100%; text-align: left;">
                                <div style="float: left; width: 50%; text-align: left;">
                                    <asp:RadioButton ID="rdoPending" runat="server" Text="Pending" Checked="True" GroupName="SearchStatus" />
                                    <asp:RadioButton ID="rdoApproved" runat="server" Text="Approved" GroupName="SearchStatus" />
                                </div>
                                <div style="float: left; width: 49%; text-align: left;">
                               
                                </div>
                               
                            </div>
                        </asp:Panel>
                    </div>
                    <div style="padding: 2.0px; float: left; width: 100%; text-align: left;">
                       
                    </div>
                    <div style="float: left; width: 100%; text-align: right;">
                    </div>
                </asp:Panel>
            </div>--%>
            <div style="padding: 2.0px; float: left; width: 96%; text-align: right;">
            </div>
            <div style="float: left; width: 100%; text-align: right;">
                <div style="float: left; width: 100%; text-align: left;">
                    <div style="height: 16px; background-color: #1E4A9F; color: White; width: 98%; float: left; font-weight: bold;">
                        <%-- <asp:TemplateField HeaderText="ApprSerID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblApprSerID" runat="server" Text='<%# Bind("Iadd_anal4") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>--%>Request Details</div>
                    <%--Credit/Cheque/Bank Slip payment--%>
                    <div style="float: left;">
                    </div>
                       <%-- <asp:Image runat="server" ID="Image2" ImageUrl="~/Images/16 X 16 DownArrow.jpg" ImageAlign="Right" /></div>--%>
                    <%--Advance receipt/Credit Note payment--%>
                  <%--  <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server" TargetControlID="pnlProductDescr"
                        CollapsedSize="0" ExpandedSize="130" Collapsed="False" ExpandControlID="Image2"
                        CollapseControlID="Image2" AutoCollapse="False" AutoExpand="False" ScrollContents="false"
                        ExpandDirection="Vertical" ImageControlID="Image2" ExpandedImage="~/Images/16 X 16 UpArrow.jpg"
                        CollapsedImage="~/Images/16 X 16 DownArrow.jpg">
                    </asp:CollapsiblePanelExtender>--%>
                </div>
                <div style="float: left; width: 100%; text-align: right;">
                    <asp:Panel ID="pnlProductDescr" runat="server">
                        <div style="float: left; width: 100%; text-align: right;">
                         
                            &nbsp;<asp:Button ID="btnItmClear" runat="server" Text="Clear" CssClass="Button" 
                                OnClick="btnItmClear_Click" Visible="False" />
                        </div>
                        <div style="float: left; width: 100%; text-align: right;">
                            <div style="float: left; width: 8%; text-align: right;">
                                <asp:Label ID="Label3" runat="server" Text="Item Code:"></asp:Label>
                            </div>
                            <div style="float: left; width: 20%; text-align: left;">
                                <asp:TextBox ID="txtItemCD" runat="server" CssClass="TextBoxUpper"></asp:TextBox>
                                &nbsp;
                                <asp:ImageButton ID="imgBtnSearchItmCD" runat="server" ImageUrl="~/Images/icon_search.png"
                                    OnClick="imgBtnSearchItmCD_Click" />
                            </div>
                            <div style="float: left; width: 5%; text-align: left;">
                                <asp:Label ID="Label4" runat="server" Text="Model:"></asp:Label>
                            </div>
                            <div style="float: left; width: 10%; text-align: right;">
                                <asp:TextBox ID="txtModel" runat="server" CssClass="TextBoxUpper" Width="95%"></asp:TextBox>
                            </div>
                            <div style="float: left; width: 15%; text-align: center;">
                                <asp:Label ID="Label5" runat="server" Text="Item Description:"></asp:Label>
                            </div>
                            <div style="float: left; width: 20%; text-align: right;">
                                <asp:TextBox ID="txtItmDescription" runat="server" CssClass="TextBoxUpper" Width="100%"></asp:TextBox>
                            </div>
                            <div style="float: left; width: 5%; text-align: right;">
                                <asp:Label ID="Label6" runat="server" Text="Qty. :"></asp:Label>
                            </div>
                            <div style="float: left; width: 9%; text-align: right;">
                                <asp:TextBox ID="txtQty" runat="server" CssClass="TextBox" Width="100%"></asp:TextBox>
                            </div>
                            <div style="float: left; width: 6%; text-align: right;">
                               <asp:Button ID="btnItmAdd" runat="server" Text="Add" CssClass="Button" 
                                    OnClick="btnItmAdd_Click" Font-Bold="True" ForeColor="#0000CC" />
                            </div>
                        </div>
                        <div  style="float: left; width: 100%; "> 
                            <asp:Panel ID="Panel_PriceDet" runat="server">
                            <div style="float: left; width: 100%; text-align: right;">                          
                           
                            <div style="float: left; width: 8%; text-align: right;">
                                <asp:Label ID="Label7" runat="server" Text="Price Book:"></asp:Label>
                            </div>
                            <div style="float: left; width: 10%; text-align: left;">
                                <asp:TextBox ID="txtPriceBook" runat="server" CssClass="TextBoxUpper" Width="70%"></asp:TextBox>
                                <asp:ImageButton ID="imgBtnPriceBook" runat="server" ImageUrl="~/Images/icon_search.png"
                                    OnClick="imgBtnPriceBook_Click" Width="16px" />
                            </div>
                            <div style="float: left; width: 10%; text-align: right;">
                                <asp:Label ID="Label8" runat="server" Text="P.Book Level:"></asp:Label>
                            </div>
                            <div style="float: left; width: 10%; text-align: left;">
                                <asp:TextBox ID="txtPBLevel" runat="server" CssClass="TextBoxUpper" Width="60%"></asp:TextBox>
                                <asp:ImageButton ID="imgBtnLevelSearch" runat="server" ImageUrl="~/Images/icon_search.png"
                                    OnClick="imgBtnLevelSearch_Click" />
                            </div>
                            <div style="float: left; width: 5%; text-align: right;">
                                <asp:Label ID="Label9" runat="server" Text="Location:"></asp:Label>
                            </div>
                            <div style="float: left; width: 12%; text-align: left;">
                                <asp:TextBox ID="txtFgapLoc" runat="server" CssClass="TextBoxUpper" Width="60%"></asp:TextBox>
                               
                                <asp:ImageButton ID="imgbtnSearchLocation" runat="server" ImageUrl="~/Images/icon_search.png"
                                    OnClick="imgbtnSearchLocation_Click" />
                            </div>
                          <div style="float: left; width: 10%; text-align: right;">
                             <asp:Label ID="Label14" runat="server" Text="Profit Center:"></asp:Label>
                          </div>
                           <div style="float: left; width: 12%; text-align: left;">
                            <asp:TextBox ID="txtPC" runat="server" CssClass="TextBoxUpper" Width="60%"></asp:TextBox>
                                        <asp:ImageButton ID="imgbtnSearchProfitCenter" runat="server" ImageUrl="~/Images/icon_search.png"
                                            OnClick="imgbtnSearchProfitCenter_Click" />
                          </div>
                          <div style="float: left; width: 5%; text-align: left;">
                           <asp:Label ID="Label10" runat="server" Text="Unit price:"></asp:Label>
                          </div>
                          <div style="float: left; width: 10%; text-align: left;">
                               <asp:TextBox ID="txtUnitPrice" runat="server" CssClass="TextBox" BackColor="#FFFFCC"
                                            BorderStyle="None" ForeColor="Blue" Width="100%"></asp:TextBox>
                          </div>
                          
                        </div>
                            </asp:Panel>
                        </div>
                        
                       <%-- <div style="float: left; width: 40%; text-align: right;">
                            <asp:Panel ID="Panel8" runat="server">
                                <div style="float: left; width: 100%; text-align: right;">
                                    <div style="float: left; width: 50%; text-align: right;">
                                    </div>
                                    <div style="float: left; width: 49%; text-align: right;">
                                    </div>
                                </div>
                                <div style="float: left; width: 100%; text-align: right;">
                                    <div style="float: left; width: 50%; text-align: right;">
                                    </div>
                                    <div style="float: left; width: 49%; text-align: right;">
                                    </div>
                                </div>
                                <div style="float: left; width: 100%; text-align: right;">
                                    <div style="float: left; width: 50%; text-align: right;">
                                    </div>
                                    <div style="float: left; width: 49%; text-align: right;">
                                    </div>
                                </div>
                                <div style="float: left; width: 100%; text-align: right;">
                                    <div style="float: left; width: 50%; text-align: right;">
                                    </div>
                                    <div style="float: left; width: 49%; text-align: right;">
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>--%>
                     <%--   <div style="float: left; width: 10%; text-align: left;">
                        </div>--%>
                    <%--    <div style="float: left; width: 39%; text-align: right;">
                            <asp:Panel ID="Panel_PriceDet1" runat="server">
                                <div style="float: left; width: 100%; text-align: right;">
                                    <div style="float: left; width: 50%; text-align: right;">
                                    </div>
                                    <div style="float: left; width: 49%; text-align: center;">
                                    </div>
                                </div>
                                <div style="float: left; width: 100%; text-align: right;">
                                    <div style="float: left; width: 50%; text-align: right;">
                                    </div>
                                    <div style="float: left; width: 49%; text-align: center;">
                                    </div>
                                </div>
                                <div style="float: left; width: 100%; text-align: right;">
                                    <div style="float: left; width: 50%; text-align: right;">
                                    </div>
                                    <div style="float: left; width: 49%; text-align: center;">
                                    </div>
                                </div>
                                <div style="float: left; width: 100%; text-align: right;">
                                    <div style="float: left; width: 50%; text-align: right;">
                                       
                                    </div>
                                    <div style="float: left; width: 49%; text-align: center;">
                                        
                                    </div>
                                </div>
                                <div style="float: left; width: 100%; text-align: right;">
                                    <div style="float: left; width: 50%; text-align: right;">
                                       
                                    </div>
                                    <div style="float: left; width: 49%; text-align: center;">
                                        
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>--%>
                        <div style="float: left; width: 100%; text-align: right;">
                            <div style="float: left; width: 100%; text-align: right;">
                                <asp:Panel ID="Panel5" runat="server" GroupingText=" " ScrollBars="Both"
                                    Height="100px">
                                    <%--BackColor="#99CCFF" --%>
                                    <asp:GridView ID="grvItmDes" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        ForeColor="#333333" OnSelectedIndexChanged="grvItmDes_SelectedIndexChanged"
                                        ShowHeaderWhenEmpty="True" OnRowDataBound="grvItmDes_RowDataBound" 
                                        OnRowDeleting="grvItmDes_RowDeleting" Width="98%">
                                        <AlternatingRowStyle BackColor="White" />
                                        <EmptyDataTemplate>
                                            <div style="width: 100%; text-align: center;">
                                                No data found
                                            </div>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="True" SelectText="Select&gt;&gt;" >
                                            <ItemStyle ForeColor="#009933" HorizontalAlign="Center" />
                                            </asp:CommandField>
                                            <asp:BoundField HeaderText="Item Code" DataField="Iadd_claim_itm" >
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Model" DataField="Iadd_anal1" >
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Description" DataField="Iadd_anal2" >
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Qty. " DataField="Iadd_anal1" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                                    <asp:ImageButton ID="imgBtnDeleteItm" runat="server" ImageUrl="~/Images/Delete.png"
                                                        CommandName="Delete" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="lineNo" Visible="False">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Iadd_line") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="txtLineNo" runat="server" Text='<%# Bind("Iadd_line") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="status" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_itm_Status" runat="server" Text='<%# Bind("Iadd_stus") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="ApprSerID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblApprSerID" runat="server" Text='<%# Bind("Iadd_anal4") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>--%>
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
                       <%-- <div style="padding: 1.0px; float: left; width: 90%; text-align: left;">
                        </div>--%>
                    </asp:Panel>
                </div>
            </div>
            <div style="float: left; width: 96%; text-align: left; margin-bottom: 0px;">
                &nbsp;&nbsp;</div>
            <div style="float: left; width: 100%; text-align: right;">
                <div style="float: left; width: 100%; text-align: left;">
                    <div style="height: 16px; background-color: #1E4A9F; color: White; width: 59%; float: left; font-weight: bold;">
                        <%--Credit/Cheque/Bank Slip payment--%>Approval</div>
                    <%--Advance receipt/Credit Note payment--%>
                    <div style="float: left;">
                        <asp:Image runat="server" ID="Image4" ImageUrl="~/Images/16 X 16 DownArrow.jpg" 
                            ImageAlign="Right" Visible="False" /></div>
                    <%--Credit/Cheque/Bank Slip payment--%>
                   <%-- <asp:CollapsiblePanelExtender ID="CPEPayment" runat="server" TargetControlID="pnlProductDesc"
                        CollapsedSize="0" ExpandedSize="170" Collapsed="False" ExpandControlID="Image4"
                        CollapseControlID="Image4" AutoCollapse="False" AutoExpand="False" ScrollContents="false"
                        ExpandDirection="Vertical" ImageControlID="Image4" ExpandedImage="~/Images/16 X 16 UpArrow.jpg"
                        CollapsedImage="~/Images/16 X 16 DownArrow.jpg">
                    </asp:CollapsiblePanelExtender>--%>
                  <div style="float: left; width: 39%; text-align: right;">                         
                            <asp:Button ID="Button1" runat="server" Text="Reject" CssClass="Button" 
                                OnClick="btnReject_Click" Font-Bold="True" ForeColor="Red" />
                            <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="Button" 
                                OnClick="btnApprove_Click" Font-Bold="True" ForeColor="#006600" />
                  </div>
                </div>
              
                <div style="float: left; width: 100%; text-align: right;">
                    <asp:Panel ID="pnlProductDesc" runat="server">                       
                        <div style="float: left; width: 100%; text-align: right;" id="divStatus" runat="server">                           
                            <div style="float: left; width: 20%; text-align: left;">
                               <%-- <asp:Label ID="lblSelectedStatus" runat="server"></asp:Label>--%>
                            </div>
                        </div>
                       <div style="float: left; width: 45%; text-align: right;">
                      
                          <div style="float: left; width: 100%; text-align: right;">
                             
                                
                              &nbsp;</div>
                        
                         <div style="float: left; width: 100%; text-align: right;">
                            <div style="float: left; width: 100%; text-align: right; vertical-align: bottom;">
                               <asp:Label ID="Label13" runat="server" Text="Filter by Status :"></asp:Label>
                               <%-- <div style="float: left; width: 20%; text-align: right;">
                                    <asp:Label ID="Label11" runat="server" Text="Approved By:"></asp:Label>
                                </div>
                                <div style="float: left; width: 100%; text-align: left;">
                                    <asp:TextBox ID="txtApprBy" runat="server" CssClass="TextBox"></asp:TextBox>
                                </div>--%>

                            </div>
                            <div style="float: left; width: 100%; text-align: right;">
                              <%--  <div style="float: left; width: 20%; text-align: right;">
                                    <asp:Label ID="Label12" runat="server" Text="Date:"></asp:Label>
                                </div>
                                <div style="float: left; width: 100%; text-align: left;">
                                    <asp:TextBox ID="txtApprDate" runat="server" CssClass="TextBox"></asp:TextBox>
                                </div>--%>
                            </div>
                        </div>    
                        </div>
                        <div style="float: left; width: 14%; text-align: right;">
                           
                            <div style="float: left; width: 100%; text-align: center;">
                               <%-- <asp:Button ID="btnAllIn" runat="server" Text="All In >> " CssClass="Button" OnClick="btnAllIn_Click" />--%>
                                &nbsp;
                            </div>
                            <div style="float: left; width: 100%; text-align: center;">
                               
                                <div style="float: left; width: 100%; text-align: left;">
                                    <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" 
                                        CssClass="ComboBox" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" 
                                        Width="70%">
                                    </asp:DropDownList>
                                    &nbsp;<asp:ImageButton ID="imgBtnItemStatus" runat="server" 
                                        ImageUrl="~/Images/icon_search.png" OnClick="imgBtnItemStatus_Click" />
                                </div>
                               
                            </div>
                            <div style="float: left; width: 100%; text-align: center;">
                             
                            </div>
                            <div style="float: left; width: 100%; text-align: center;">
                               <%-- <asp:Button ID="btnAllBack" runat="server" Text="<<All Back" CssClass="Button" OnClick="btnAllBack_Click" />--%>
                            </div>
                              
                            
                             <div style="float: left; width: 100%; text-align: center;">
                               
                            </div>
                        </div>
                       <div style="float: left; width: 39%; text-align: right;">
                         <asp:Panel ID="Panel3" runat="server" GroupingText="Available List" Height="134px"
                                ScrollBars="Both">
                               
                               <%-- <div style="float: left; width: 100%; text-align: right;">--%>
                                    <asp:GridView ID="grvAvailableSerials" runat="server" AutoGenerateColumns="False"
                                        ShowHeaderWhenEmpty="True" 
                                        OnSelectedIndexChanged="grvAvailableSerials_SelectedIndexChanged" 
                                        CellPadding="1" ForeColor="#333333" Width="100%" Font-Size="Smaller" 
                                   onrowdatabound="grvAvailableSerials_RowDataBound">
                                        <EditRowStyle BackColor="#2461BF" />
                                        <EmptyDataTemplate>
                                            <div style="width: 100%; text-align: center;">
                                                No data found
                                            </div>
                                        </EmptyDataTemplate>
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField>
                                            <HeaderTemplate>
                                                   <asp:CheckBox ID="Checkall" runat="server"  onclick="javascript: SelectAll(this);" />

                                            </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                                    <asp:CheckBox ID="chekSelect1" runat="server" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Item Code" DataField="Tus_itm_cd" >
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Serial #" DataField="Tus_ser_1" >
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Status" DataField="Tus_itm_stus" >
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Serial_ID" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSerID_av" runat="server" Text='<%# Bind("Tus_ser_id") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtSerID_available" runat="server" Text='<%# Bind("Tus_ser_id") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
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
                               <%-- </div>--%>
                            </asp:Panel>
                            <div  style="float: left; width: 100%; text-align: right;">
                             <asp:Button ID="btnIn" runat="server" Text="Add to list" CssClass="Button" 
                                    OnClick="btnIn_Click" Font-Bold="True" ForeColor="#0000CC" />
                            </div>                           
                            
                         </div>
                        
                       
                    </asp:Panel>
                </div>
            </div>
            
            <div style="float: left; width: 100%; text-align: right;">
                <div style="float: left; width: 100%; text-align: left;">
                    <div style="height: 16px; background-color: #1E4A9F; color: White; width: 59%; float: left; font-weight: bold;">
                        <%--Advance receipt/Credit Note payment--%>Transfer</div>
                    <%--Credit/Cheque/Bank Slip payment--%>
                    <div style="float: left;">
                        <asp:Image runat="server" ID="Image1" ImageUrl="~/Images/16 X 16 DownArrow.jpg" 
                            ImageAlign="Right" Visible="False" /></div>
                    <%--Advance receipt/Credit Note payment--%>
                   <%-- <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" TargetControlID="pnlProductInTake"
                        CollapsedSize="0" Collapsed="False" ExpandControlID="Image1" CollapseControlID="Image1"
                        AutoCollapse="False" AutoExpand="False" ScrollContents="false" ExpandDirection="Vertical"
                        ImageControlID="Image1" ExpandedImage="~/Images/16 X 16 UpArrow.jpg" CollapsedImage="~/Images/16 X 16 DownArrow.jpg"
                        ExpandedSize="400">
                    </asp:CollapsiblePanelExtender>--%>
                </div>
                <div style="float: right; width: 100%; text-align: right; background-color: #FFFFFF;">
                    <asp:Panel ID="pnlProductInTake" runat="server" HorizontalAlign="Right">
                        <asp:Panel ID="pnlPayss" runat="server">
                        <div style="float: left; width: 59%;" ID="div_payment" runat="server" >
                      <div style="float: left; width: 100%; text-align: right;">
                            <asp:Button ID="Button10" runat="server" Text="Payment Complete" CssClass="Button"
                                OnClick="Button10_Click" Font-Bold="True" ForeColor="#006600" />
                        </div>
                                              
                        
                        <div style="float: left; width: 100%; text-align: right;">
                            <asp:Panel ID="Panel7" runat="server" GroupingText="Price Detail">
                                <div style="float: left; width: 25%; text-align: right;">
                                    <div style="float: left; width: 100%; text-align: center;">
                                        <asp:Label ID="Label15" runat="server" Text="Approved Value:"></asp:Label>
                                    </div>
                                    <div style="float: left; width: 100%; text-align: center;">
                                        <asp:Label ID="lblApprVal" runat="server" Font-Bold="True"></asp:Label>
                                    </div>
                                </div>
                                <div style="float: left; width: 25%; text-align: right;">
                                    <div style="float: left; width: 100%; text-align: center;">
                                        <asp:Label ID="Label16" runat="server" Text="Collection Value"></asp:Label>
                                    </div>
                                    <div style="float: left; width: 100%; text-align: center;">
                                        <asp:Label ID="lblCollectVal" runat="server" Font-Bold="True"></asp:Label>
                                    </div>
                                </div>
                                <div style="float: left; width: 25%; text-align: right;">
                                    <div style="float: left; width: 100%; text-align: center;">
                                        <asp:Label ID="Label18" runat="server" Text="Price Deference:"></asp:Label>
                                    </div>
                                    <div style="float: left; width: 100%; text-align: center;">
                                        <asp:Label ID="lblPriceDeference" runat="server" Font-Bold="True"></asp:Label>
                                    </div>
                                </div>
                                <div style="float: left; width: 24%; text-align: right;">
                                    <div style="float: left; width: 100%; text-align: center;">
                                        <asp:Label ID="Label20" runat="server" Text="Recipt Amount"></asp:Label>
                                    </div>
                                    <div style="float: left; width: 100%; text-align: center;">
                                        <asp:Label ID="lblReceiptAmt" runat="server" Font-Bold="True"></asp:Label>
                                    </div>
                                </div>
                             
                            </asp:Panel>
                        </div>
                        <div style="float: left; width: 100%; text-align: left;">
                            <asp:Panel ID="pnlPayment" runat="server" BackColor="#CCCCFF">
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 100%;">
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 15%;">
                                            Pay Mode
                                        </div>
                                        <div style="float: left; width: 25%;">
                                            <asp:DropDownList ID="ddlPayMode" runat="server" Width="80%" CssClass="ComboBox"
                                                OnSelectedIndexChanged="PaymentType_LostFocus" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 10%;">
                                            Amount
                                        </div>
                                        <div style="float: left; width: 35%;">
                                            <asp:TextBox ID="txtPayAmount" runat="server" CssClass="TextBoxUpper" Width="50%"></asp:TextBox>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnPayAdd" runat="server" CssClass="Button" Text="Add" OnClick="AddPayment" />
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; padding: 2px 2px 0px 0px;">
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 15%;">
                                            Remarks
                                        </div>
                                        <div style="float: left; width: 75%;">
                                            <asp:TextBox ID="txtPayRemarks" runat="server" CssClass="TextBox" Width="100%" TextMode="MultiLine"
                                                Rows="2"></asp:TextBox></div>
                                    </div>
                                    <div style="float: left; width: 100%; padding: 2px 2px 0px 0px;">
                                        <%--Credit/Cheque/Bank Slip payment--%>
                                        <div style="float: left; width: 100%; height: 100px;" runat="server" id="divCredit"
                                            visible="false">
                                            <div style="float: left; width: 100%;" id="divCRDno" runat="server">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 15%;">
                                                    Card No</div>
                                                <div style="float: left; width: 75%; padding-bottom: 2px;">
                                                    <asp:TextBox runat="server" ID="txtPayCrCardNo" CssClass="TextBox" Width="80%"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 100%;" id="divBankDet" runat="server">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 15%;">
                                                    Bank
                                                </div>
                                                <div style="float: left; width: 20%; padding-bottom: 2px;">
                                                    <asp:TextBox runat="server" ID="txtPayCrBank" CssClass="TextBox" Width="35%"></asp:TextBox>
                                                    <asp:ImageButton ID="ImgBtnBankSearch" runat="server" ImageUrl="~/Images/icon_search.png"
                                                        OnClick="ImgBtnBankSearch_Click" />
                                                </div>
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 15%;">
                                                    Branch
                                                </div>
                                                <div style="float: left; width: 20%; padding-bottom: 2px;">
                                                    <asp:TextBox runat="server" ID="txtPayCrBranch" CssClass="TextBox" Width="70%"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 100%;" id="divCreditCard" runat="server">
                                                <div style="float: left; width: 100%;" id="divCardDet" runat="server">
                                                    <div style="float: left; width: 1%;">
                                                        &nbsp;</div>
                                                    <div style="float: left; width: 15%;">
                                                        Card Type
                                                    </div>
                                                    <div style="float: left; width: 15%; padding-bottom: 2px;">
                                                        <asp:TextBox runat="server" ID="txtPayCrCardType" CssClass="TextBox" Width="90%"></asp:TextBox>
                                                    </div>
                                                    <div style="float: left; width: 1%;">
                                                        &nbsp;</div>
                                                    <div style="float: left; width: 15%;">
                                                        Expiry Date
                                                    </div>
                                                    <div style="float: left; width: 25%; padding-bottom: 2px;">
                                                        <asp:TextBox runat="server" ID="txtPayCrExpiryDate" CssClass="TextBox" Width="70%"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                            TargetControlID="txtPayCrExpiryDate">
                                                        </asp:CalendarExtender>
                                                        &nbsp;<asp:Image ID="btnImgCrExpiryDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png"
                                                            ImageAlign="Middle" />
                                                    </div>
                                                </div>
                                                <div style="float: left; width: 100%;">
                                                    <div style="float: left; width: 1%;">
                                                        &nbsp;</div>
                                                    <div style="float: left; width: 15%;">
                                                        Promotion
                                                    </div>
                                                    <div style="float: left; width: 75%; padding-bottom: 2px;">
                                                        <asp:CheckBox runat="server" ID="chkPayCrPromotion" />
                                                        &nbsp; Period &nbsp;
                                                        <asp:TextBox runat="server" ID="txtPayCrPeriod" CssClass="TextBoxNumeric" Width="20%"></asp:TextBox>
                                                        months
                                                    </div>
                                                </div>
                                                <div style="float: left; width: 100%;" id="divCredBatch">
                                                    <div style="float: left; width: 1%;">
                                                        &nbsp;</div>
                                                    <div style="float: left; width: 15%;">
                                                        Batch No
                                                    </div>
                                                    <div style="float: left; width: 75%; padding-bottom: 2px;">
                                                        <asp:TextBox runat="server" ID="txtPayCrBatchNo" CssClass="TextBox" Width="80%"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 100%;" id="divChequeNum" runat="server">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 15%;">
                                                    Cheque No
                                                </div>
                                                <div style="float: left; width: 75%; padding-bottom: 2px;">
                                                    <asp:TextBox runat="server" ID="txtChequeNo" CssClass="TextBox" Width="80%"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <%--Advance receipt/Credit Note payment--%>
                                        <div style="float: left; width: 100%;" runat="server" id="divAdvReceipt" visible="false">
                                            <div style="float: left; width: 100%;">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 20%;">
                                                    Referance</div>
                                                <div style="float: left; width: 75%; padding-bottom: 2px;">
                                                    <asp:TextBox runat="server" ID="txtPayAdvReceiptNo" CssClass="TextBox" Width="80%"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 100%;">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 20%;">
                                                    Ref. Amount</div>
                                                <div style="float: left; width: 75%; padding-bottom: 2px;">
                                                    <asp:TextBox runat="server" ID="txtPayAdvRefAmount" CssClass="TextBox" Width="80%"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 100%;">
                                        <asp:Panel ID="pnlPay" runat="server" Height="140px" ScrollBars="Auto">
                                            <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False" CssClass="GridView"
                                                CellPadding="3" ForeColor="#333333" DataKeyNames="sard_pay_tp,sard_settle_amt"
                                                OnRowDeleting="gvPayment_OnDelete" ShowHeaderWhenEmpty="True">
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
                                                        HeaderStyle-Width="110px">
                                                        <HeaderStyle Width="110px" />
                                                        <ItemStyle Width="110px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField='sard_ref_no' HeaderText='' Visible="false" />
                                                    <asp:BoundField DataField='sard_chq_bank_cd' HeaderText='Bank' ItemStyle-Width="90px"
                                                        HeaderStyle-Width="90px">
                                                        <HeaderStyle Width="90px" />
                                                        <ItemStyle Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField='sard_chq_branch' HeaderText='Branch' ItemStyle-Width="90px"
                                                        HeaderStyle-Width="90px">
                                                        <HeaderStyle Width="90px" />
                                                        <ItemStyle Width="90px" />
                                                    </asp:BoundField>
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
                                                    <asp:BoundField DataField='sard_receipt_no' HeaderText='receipt_no' Visible="False" />
                                                    <asp:BoundField DataField='sard_anal_3' HeaderText="Bank/Other Charges" />
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
                                    <div style="float: left; width: 100%;">
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 20%; background-color: #1569C7; color: White; height: 22px;">
                                            Paid Amount
                                        </div>
                                        <div style="float: left; width: 18%; text-align: right; border-color: Black; border-style: solid;
                                            border-width: 1px;">
                                            <asp:Label runat="server" ID="lblPayPaid" Text="0.00"></asp:Label>
                                        </div>
                                        <div style="float: left; width: 18%;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 20%; background-color: #1569C7; color: White; height: 22px;">
                                            Balance Amount
                                        </div>
                                        <div style="float: left; width: 18%; text-align: right; border-color: Black; border-style: solid;
                                            border-width: 1px;">
                                            <asp:Label runat="server" ID="lblPayBalance" Text="0.00"></asp:Label>
                                        </div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                    </div>
                               
                            </asp:Panel>
                         </div>
                    </div>
                        </asp:Panel>
                    
                      <div style="float: left; width: 39%;">
                       <div  style="float: left; width: 100%; text-align: right;">  
                           <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="Button" 
                                OnClick="btnReject_Click" Font-Bold="True" ForeColor="Red" />
                           <asp:Button ID="btnConfirm" runat="server" CssClass="Button" Font-Bold="True" 
                               ForeColor="#006600" OnClick="btnConfirm_Click" Text="Confirm" />
                          
                       </div>
                        <div style="float: right; width: 100%; text-align: right;">
                            <asp:Panel ID="Panel4" runat="server" GroupingText="Selected List" 
                                Height="150px" ScrollBars="Both" >
                              
                                <%--<div style="float: left; width: 100%; text-align: right;">--%>
                                    <asp:GridView ID="grvApproveItms" runat="server" AutoGenerateColumns="False" 
                                        ShowHeaderWhenEmpty="True" CellPadding="1" ForeColor="#333333" 
                                    Width="100%" Font-Size="Smaller">
                                        <EditRowStyle BackColor="#2461BF" />
                                        <EmptyDataTemplate>
                                            <div style="width: 100%; text-align: center;">
                                                No data found
                                            </div>
                                        </EmptyDataTemplate>
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField>
                                             <HeaderTemplate>
                                                   <asp:CheckBox ID="Checkall2" runat="server"  onclick="javascript: SelectAll2(this);" />

                                            </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                                    <asp:CheckBox ID="chekSelect2" runat="server" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Item Code" DataField="Tus_itm_cd" >
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Serial #" DataField="Tus_ser_1" >
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Status" DataField="Tus_itm_stus" >
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:TemplateField Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server"></asp:Label>
                                                    <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Images/Delete.png" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Serial_ID" Visible="False">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSerID_appr" runat="server" Text='<%# Bind("Tus_ser_id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
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
                                <%--</div>--%>
                            </asp:Panel>
                            <div  style="float: left; width: 100%; text-align: right;">  
                                   <asp:Button ID="btnBack" runat="server" Text="Remove from list" 
                                       CssClass="Button" OnClick="btnBack_Click" Font-Bold="True" 
                                       ForeColor="#0000CC" />
                            </div>
                        </div>
                                            
                      </div>
                   </asp:Panel>
                    </div>
                 
                  
              </asp:Panel>   
            </div>
                                    
                   
                </div>
            <%--</div>--%>
             
        </div>
</asp:Content>
