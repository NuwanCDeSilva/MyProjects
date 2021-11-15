<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PriceDefinition.aspx.cs" Inherits="FF.WebERPClient.Sales_Module.PriceDefinition" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
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


    function SelectAll(id)
        {
            
            var grid = document.getElementById("<%= gvPPProfits.ClientID %>");
            var cell;
            
            if (grid.rows.length > 0)
            {
                for (i=1; i<grid.rows.length; i++)
                {
                    cell = grid.rows[i].cells[0];
                    for (j=0; j<cell.childNodes.length; j++)
                    {           
                        if (cell.childNodes[j].type =="checkbox")
                        {
                            cell.childNodes[j].checked = document.getElementById(id).checked;
                        }
                    }
                }
            }
        }


        function checkFileExtension(elem) {
            var filePath = elem.value;


            if (filePath.indexOf('.') == -1)
                return false;


            var validExtensions = new Array();
            var ext = filePath.substring(filePath.lastIndexOf('.') + 1).toLowerCase();

            validExtensions[0] = 'xlsx';
            validExtensions[1] = 'xls';

            for (var i = 0; i < validExtensions.length; i++) {
                if (ext == validExtensions[i])
                    return true;
            }


            alert('The file extension ' + ext.toUpperCase() + ' is not allowed!');
            var Box = document.getElementById("FileUpload1");
            Box.outerHTML = Box.outerHTML;
            return false;
        }


    </script>

    <%--Whole Page--%>
<div style="float: left; width: 100%; color:Black; font-size:11px; ">
    <%--Control Panel--%>
    <div style="float: left; width: 100%; height: 22px; text-align: right; background-color: #00004C"> 
        <%--<asp:Button runat="server" ID="btnClear" Text="Clear" CssClass="TextBox" />--%>
        <asp:Button runat="server" ID="btnClose" Text="Close" CssClass="TextBox" OnClick="Close" />
    </div>
    <%--Main Tab for Sub Processes--%>
    <div style="float: left; width: 100%; color:Black; ">
    
    <%--Main Tab for Sub Processes--%>
    <asp:TabContainer ID="TabMain" runat="server" Width="100%"  Height="470px" BorderColor="Black" BorderWidth="1px" BorderStyle="Solid" ActiveTabIndex="0"   style=" background-color:Black;"   > 

        <%-- =============================== Main Tab for Price Updation =============================== --%>
        <asp:TabPanel runat="server" ID="tbpPriceAssign" HeaderText="Price Assign"  >
            <ContentTemplate>

                <div style="float: left; width: 49%; font-size:11px; border-color:LightGray;border-style:solid; border-width:1px; padding-top:1px; height:70px; "> 

                    <div style="float: left; width: 100%;"> 

                        <div style="float: left; width: 1%;">&nbsp;</div>
                        <div style="float: left; width: 24%;">From . .</div>
                        <div style="float: left; width: 25%;"><asp:TextBox ID="txtFrom" runat="server" Width="70%" Font-Names="Tahoma" ClientIDMode="Static"  Font-Size="12px" CssClass="TextBox"  ></asp:TextBox>  <asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" ImageAlign="Middle" /> </div>

                        <div style="float: left; width: 1%;">&nbsp;</div>
                        <div style="float: left; width: 24%;">To . . . . </div>
                        <div style="float: left; width: 25%;"><asp:TextBox ID="txtTo" runat="server" Width="70%" Font-Names="Tahoma"  ClientIDMode="Static" Font-Size="12px"  CssClass="TextBox" ></asp:TextBox> <asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png"  ImageAlign="Middle" /> </div>

                    </div>

                    <div style="float: left; width: 100%;"> 

                        <div style="float: left; width: 1%;">&nbsp;</div>
                        <div style="float: left; width: 24%;">Price Book . </div>
                        <div style="float: left; width: 25%;"> <asp:ListSearchExtender id="ListSearchExtender4" runat="server"    TargetControlID="ddlPABook"   PromptText=""    IsSorted="True"   Enabled="True"   /> <asp:DropDownList runat="server" ID="ddlPABook"   Width="65%" OnSelectedIndexChanged="LoadPriceLevels" Font-Size="11px"  AutoPostBack="True"  ></asp:DropDownList>  </div>

                        <div style="float: left; width: 1%;">&nbsp;</div>
                        <div style="float: left; width: 24%;">Price Level . </div>
                        <div style="float: left; width: 25%;"> <asp:ListSearchExtender id="ListSearchExtender5" runat="server"    TargetControlID="ddlPALevel"   PromptText=""    IsSorted="True"  Enabled="True"   /> <asp:DropDownList runat="server" ID="ddlPALevel" Width="65%" Font-Size="11px" ></asp:DropDownList></div>

                    </div>

                    <div style="float: left; width: 100%;"> 

                        <div style="float: left; width: 1%;">&nbsp;</div>
                        <div style="float: left; width: 24%;">Circuler No . . . </div>
                        <div style="float: left; width: 25%;"><asp:TextBox ID="txtCirculer" runat="server" Width="70%" Font-Names="Tahoma"  Font-Size="12px"  CssClass="TextBox" ></asp:TextBox> <asp:ImageButton ID="ImageButton2" runat ="server" ImageUrl="~/Images/icon_search.png" ImageAlign="Middle"  /> </div>
                        <div style="float: left; width: 25%;"><asp:CheckBox ID="chkFixQty" runat="server" Text="Fixed Qty" /></div>
                     
                    </div>

                    <div>
                        <asp:CalendarExtender ID="CEFromDate"   runat="server" TargetControlID="txtFrom"  PopupButtonID="imgFromDate" Format="dd/MM/yyyy"  Enabled="True"> </asp:CalendarExtender>
                        <asp:CalendarExtender ID="CEToDate"   runat="server" TargetControlID="txtTo"  PopupButtonID="imgToDate" Format="dd/MM/yyyy" Enabled="True"> </asp:CalendarExtender>
                    </div>

                </div>

                <div style="float: left; width: 49%; font-size:11px; height:50px;">
                    
                    <div style="float: left; width: 100%; margin-bottom:2px;"> 

                        <div style="float: left; width: 1%;">&nbsp;</div>
                        <div style="float: left; width: 13%;">Category</div>
                        <div style="float: left; width: 35%;"><asp:ListSearchExtender id="ListSearchExtender10" runat="server" TargetControlID="ddlPriceCat"   PromptText=""    IsSorted="True"    Enabled="True"   /> <asp:DropDownList runat="server" ID="ddlPriceCat"  Width="70%"  Font-Size="11px"   OnSelectedIndexChanged="LoadPriceTypeFromPriceCategory" AutoPostBack="True" ></asp:DropDownList>  </div>

                        <div style="float: left; width: 1%;">&nbsp;</div>
                        <div style="float: left; width: 13%;">Type</div>
                        <div style="float: left; width: 35%;"><asp:ListSearchExtender id="ListSearchExtender11" runat="server"     TargetControlID="ddlPriceType"   PromptText=""    IsSorted="True"    Enabled="True"   /> <asp:DropDownList runat="server" ID="ddlPriceType" Width="70%"  Font-Size="11px" ></asp:DropDownList>  </div>

                    </div>

                    <div style="float: left; width: 100%;  "> 
                        <div style="float: left; width: 1%;">&nbsp;</div>
                        <div style="float: left; width: 13%;">Customer</div>
                        <div style="float: left; width: 25%;"><asp:TextBox runat="server" ID="txtCustomer" CssClass="TextBox" Width="70%"></asp:TextBox>  <asp:ImageButton ID="imgBtnCustomer" runat="server" ImageUrl="~/Images/icon_search.png"  ImageAlign="Middle"  /></div>

                        <div style="float: left; width: 1%;">&nbsp;</div>
                        <div style="float: left; width: 55%; font-size:10px; text-align:center;"><asp:Label runat="server" ID="lblCustomer" Width="100%" Text="Customer Name and the address Customer Name and the address Customer Name and the address" Height="20px"   ></asp:Label> </div>

                    </div>

                </div>
                <div style="float: left; width: 100%;">&nbsp;</div>
                <asp:TabContainer runat="server" ID="TabPriceUpdate" Height="360px" >

                    <asp:TabPanel runat="server" ID="tbpPriceUpdate" HeaderText="Price Updation"   >
                            <ContentTemplate>

                                <div style="float: left; width: 100%; padding-top:2px; padding-bottom:10px;">
                                    
                                    <div style="float: left; width: 1%;">&nbsp;</div>
                                    <div style="float: left; width: 12%;">File to Upload</div>

                                    <div style="float: left; width: 1%;">&nbsp;</div>
                                    <div style="float: left; width: 50%;"><asp:FileUpload ID="FileUpload1" runat="server" Width="100%" CssClass="TextBox" ClientIDMode="Static"  /></div>

                                    <div style="float: left; width: 1%;">&nbsp;</div>
                                    <div style="float: left; width: 17%;"><asp:CheckBox runat="server" ID="chkPUCombine" Text="Combine Promotion" /> </div>

                                    <div style="float: left; width: 1%;">&nbsp;</div>
                                    <div style="float: left; width: 13%;"><asp:Button ID="btnPUUpload" runat ="server" CssClass ="Button" Text ="Upload" OnClick="UploadPrice" />   </div>
                                    
                                </div>

                                <div style="float: left; width: 100%; ">

                                    <div style="float: left; width: 12%;background-color:#153E7E; border-right:1px solid white; color:White;">Item</div>
                                    <div style="float: left; width: 10%;background-color:#153E7E; border-right:1px solid white; color:White;">Model</div>
                                    <div style="float: left; width: 19%;background-color:#153E7E; border-right:1px solid white; color:White;">Description</div>
                                    <div style="float: left; width: 8%;background-color:#153E7E; border-right:1px solid white; color:White;text-align:right;">Qty From</div>
                                    <div style="float: left; width: 6%;background-color:#153E7E; border-right:1px solid white; color:White;text-align:right;">Qty To</div>
                                    <div style="float: left; width: 8%;background-color:#153E7E; border-right:1px solid white; color:White;text-align:right;">Unit Price</div>
                                    <div style="float: left; width: 11%;background-color:#153E7E; border-right:1px solid white; color:White;text-align:right;">No of Attempt</div>
                                    <div style="float: left; width: 19%;background-color:#153E7E; border-right:1px solid white; color:White;">Warra. Remarks</div>
                                    <div style="float: left; width: 6%;background-color:#153E7E; color:White;">Status</div>

                                </div>

                                <div style="float: left; width: 100%; ">

                                    <div style="float: left; width: 12%;background-color:#153E7E; border-right:1px solid white; color:White;"><asp:TextBox runat="server" ID="txtPUItem" CssClass="TextBox" Width="96%"></asp:TextBox> </div>
                                    <div style="float: left; width: 10%;background-color:#153E7E; border-right:1px solid white; color:White;"><asp:TextBox runat="server" ID="txtPUModel" CssClass="TextBox" Width="96%"></asp:TextBox></div>
                                    <div style="float: left; width: 19%;background-color:#153E7E; border-right:1px solid white; color:White;"><asp:TextBox runat="server" ID="txtPUDescription" CssClass="TextBox" Width="97%"></asp:TextBox></div>
                                    <div style="float: left; width: 8%;background-color:#153E7E; border-right:1px solid white; color:White;"><asp:TextBox runat="server" ID="txtPUQtyFrom" CssClass="TextBox" Width="94%"></asp:TextBox></div>
                                    <div style="float: left; width: 6%;background-color:#153E7E; border-right:1px solid white; color:White;"><asp:TextBox runat="server" ID="txtPUQtyTo" CssClass="TextBox" Width="93%"></asp:TextBox></div>
                                    <div style="float: left; width: 8%;background-color:#153E7E; border-right:1px solid white; color:White;"><asp:TextBox runat="server" ID="txtPUUnitPrice" CssClass="TextBox" Width="95%"></asp:TextBox></div>
                                    <div style="float: left; width: 11%;background-color:#153E7E; border-right:1px solid white; color:White;"><asp:TextBox runat="server" ID="txtPUNofAttempt" CssClass="TextBox" Width="95%"></asp:TextBox></div>
                                    <div style="float: left; width: 19%;background-color:#153E7E; border-right:1px solid white; color:White;"><asp:TextBox runat="server" ID="txtPUWarraRemarks" CssClass="TextBox" Width="98%"></asp:TextBox></div>
                                    <div style="float: left; width: 6%; color:White;"><asp:CheckBox runat="server" ID="TextBox8" > </asp:CheckBox> &nbsp;<asp:ImageButton runat="server" ID="imgBtnPUAdd" ImageAlign="Baseline" ImageUrl="~/Images/Add-16x16x16.ICO" Width="16px" Height="16px" /> </div>
                                
                                </div>

                                <div style="float: left; width: 100%;"> 
                                    
                                    <asp:Panel ID="pnlPUEntry" runat ="server" ScrollBars="Auto" >
                                        <asp:GridView ID="gvPUEntry" runat ="server" AutoGenerateColumns ="False" 
                                            ClientIDMode ="Static" >
                                            <HeaderStyle BackColor="White" ForeColor="White" Font-Size="0px"    />
                                            <RowStyle Font-Size="11px"   />
                                            <Columns>
                                                <asp:BoundField DataField ='sapd_itm_cd'  HeaderText='Item' >
                                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField ='Mi_model' HeaderText='Model' >
                                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField ='Mi_longdesc' HeaderText='Description' >
                                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField ='sapd_qty_from' HeaderText='Qty From' >
                                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField ='sapd_qty_to' HeaderText='Qty To' >
                                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField ='sapd_itm_price' HeaderText='Unit Price' > 
                                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField ='sapd_no_of_times' HeaderText='No of Attempt' >
                                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField ='sapd_warr_remarks' HeaderText='Warra. Remarks' >
                                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField ='sapd_price_stus' HeaderText='Status' > 
                                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>

                                </div>

                            </ContentTemplate>
                        </asp:TabPanel>

                    <asp:TabPanel runat="server" ID="tbpOnDemand" HeaderText="Price Updation on demand" >
                            <ContentTemplate>
                                    <%--Second Sub Section for Price On Demand--%>
                                    <asp:TabContainer ID="TabOnDemand" runat="server" Height="329px">
                                        <%--Second Sub Tab for Shipping Cost--%>
                                        <asp:TabPanel runat="server" ID="tbpShippingCost" HeaderText="...By Shipping Cost">
                                            <ContentTemplate> 
                                                <div style="float: left; width: 100%; color:Black;"></div>
                                                <div style="float: left; width: 100%; color:Black;"></div>
                                                <div style="float: left; width: 100%; color:Black;"></div>
                                                <div style="float: left; width: 100%; color:Black;"></div>
                                                <div style="float: left; width: 100%; color:Black;"></div>
                                                <div style="float: left; width: 100%; color:Black;"></div>
                                                <div style="float: left; width: 100%; color:Black;"></div>
                                                <div style="float: left; width: 100%; color:Black;"></div>
                                                <div style="float: left; width: 100%; color:Black;"></div>
                                                <div style="float: left; width: 100%; color:Black;"></div>
                                                <div style="float: left; width: 100%; color:Black;"></div>
                                                <div style="float: left; width: 100%; color:Black;"></div>
                                                <div style="float: left; width: 100%; color:Black;"></div>
                                                <div style="float: left; width: 100%; color:Black;"></div>
                                                <div style="float: left; width: 100%; color:Black;"></div>
                                                <div style="float: left; width: 100%; color:Black;"></div>
                                                <div style="float: left; width: 100%; color:Black;"></div>
                                                <div style="float: left; width: 100%; color:Black;"></div>
                                                <div style="float: left; width: 100%; color:Black;"></div>
                                                <div style="float: left; width: 100%; color:Black;"></div>
                                                <div style="float: left; width: 100%; color:Black;"></div>
                                                <div style="float: left; width: 100%; color:Black;"></div>
                                                <div style="float: left; width: 100%; color:Black;"></div>
                                                <div style="float: left; width: 100%; color:Black;"></div>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                        <%--Second Sub Tab for Current Inventory--%>
                                        <asp:TabPanel  runat="server" ID="tbpCurrentInventory" HeaderText="...By Current Inventory Cost">
                                            <ContentTemplate> &nbsp;
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                        <%--Second Sub Tab for Previous Price Book--%>
                                        <asp:TabPanel  runat="server" ID="tbpPreviousBook" HeaderText="...By Previous Price Book">
                                            <ContentTemplate> &nbsp;
                                            </ContentTemplate>
                                        </asp:TabPanel>

                                    </asp:TabContainer>

                            </ContentTemplate>
                        </asp:TabPanel>

                </asp:TabContainer>

            </ContentTemplate>
        </asp:TabPanel>

        <%-- =============================== Main Tab for Pricing Parameters =============================== --%>
        <asp:TabPanel runat="server" ID="tbpPriceParameter" HeaderText="Pricing Parameters">
            <ContentTemplate>

             <div style="float: left; width: 94%; font-size:12px; height:171px;">
                  <div style="float: left; width: 25%; font-size:12px; background-color:Lavender; height:171px;">

                    <div style="float: left; width: 100%; font-size:12px; margin-bottom:1px;">
                        <div style="float: left; width: 1%;">&nbsp;</div>
                        <div style="float: left; width: 40%;">Price Book</div>
                        <div style="float: left; width: 35%;"><asp:ListSearchExtender id="ListSearchExtender1" runat="server"    TargetControlID="ddlPPBook"   PromptText=""     PromptPosition="Top"    IsSorted="true"  QueryPattern="StartsWith"   /> <asp:DropDownList runat="server" ID="ddlPPBook" Width="100%"  Font-Size="11px" OnSelectedIndexChanged="LoadPPLevels" AutoPostBack="true"  ></asp:DropDownList>  </div>
                    </div>

                    <div style="float: left; width: 100%; font-size:12px; margin-bottom:1px;">
                        <div style="float: left; width: 1%;">&nbsp;</div>
                        <div style="float: left; width: 40%;">Price Level</div>
                        <div style="float: left; width: 35%;"><asp:ListSearchExtender id="ListSearchExtender6" runat="server"    TargetControlID="ddlPPLevel"   PromptText=""     PromptPosition="Top"    IsSorted="true"  QueryPattern="StartsWith"   /> <asp:DropDownList runat="server" ID="ddlPPLevel" Width="100%" Font-Size="11px" OnSelectedIndexChanged="LoadPriceDeinitionAtBook"  AutoPostBack ="true"></asp:DropDownList></div>
                    </div>

                    <div style="float: left; width: 100%; font-size:12px; margin-bottom:1px;">
                        <div style="float: left; width: 1%;">&nbsp;</div>
                        <div style="float: left; width: 40%;">Channal</div>
                        <div style="float: left; width: 35%;"><asp:ListSearchExtender id="ListSearchExtender8" runat="server"    TargetControlID="ddlPPChannal"   PromptText=""     PromptPosition="Top"    IsSorted="true"  QueryPattern="StartsWith"   /> <asp:DropDownList runat="server" ID="ddlPPChannal" Width="100%" Font-Size="11px" ></asp:DropDownList></div>
                    </div>

                    <div style="float: left; width: 100%; font-size:12px; margin-bottom:1px;">
                        <div style="float: left; width: 1%;">&nbsp;</div>
                        <div style="float: left; width: 40%;">Profit Center</div>
                        <div style="float: left; width: 35%;"><asp:ListSearchExtender id="ListSearchExtender7" runat="server"    TargetControlID="ddlPPProfitCenter"   PromptText=""     PromptPosition="Top"    IsSorted="true"  QueryPattern="StartsWith"   /> <asp:DropDownList runat="server" ID="ddlPPProfitCenter" Width="100%" Font-Size="11px" ></asp:DropDownList></div>
                    </div>

                    <div style="float: left; width: 100%; font-size:12px; margin-bottom:1px;">
                        <div style="float: left; width: 1%;">&nbsp;</div>
                        <div style="float: left; width: 40%;">Invoice Type</div>
                        <div style="float: left; width: 35%;"><asp:ListSearchExtender id="ListSearchExtender9" runat="server"    TargetControlID="ddlPPInvType"   PromptText=""     PromptPosition="Top"    IsSorted="true"  QueryPattern="StartsWith"   /> <asp:DropDownList runat="server" ID="ddlPPInvType" Width="100%" Font-Size="11px" ></asp:DropDownList></div>
                    </div>

                     <div style="float: left; width: 100%; font-size:12px; margin-bottom:1px;">
                     <asp:Button runat ="server" id ="btnPPAddItem" Text ="Add" CssClass ="Button" OnClick="AddItem" />
                     </div>

                    
                  </div> 
                  <%--empty row--%>
                  <div style="float: left; width: 1%; font-size:12px; "> &nbsp;
                  </div>

                  <div style="float: left; width: 38%; font-size:12px; height:151px; ">
                    <div style="float: left; width: 100%; font-size:12px; ">
                        <div style="float: left; width:22px; font-size:12px;"> &nbsp; </div>
                        <div style="float: left; width:51px; font-size:12px;"> <asp:TextBox runat="server" ID="txtPPCode" CssClass ="TextBox" Width="90%" ></asp:TextBox>  </div>
                        <div style="float: left; width:257px; font-size:12px;"> <asp:TextBox runat="server" ID="txtPPDescription" CssClass ="TextBox" Width="100%"></asp:TextBox> </div>
                    </div>
                    <div style="float: left; width: 100%; font-size:12px; ">
                        <asp:Panel id="pnlPPPcenters" runat="server" ScrollBars="Auto"  >
                            <asp:GridView ID="gvPPProfits" runat ="server" AutoGenerateColumns="false" ClientIDMode="Static" OnRowDataBound="gvPPProfits_RowDataBound" >
                                <HeaderStyle BackColor="#153E7E" ForeColor="White" Font-Size="11px"    />
                                <RowStyle Font-Size="11px"   />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkPPSelectAll" runat ="server" ClientIDMode="Static"  />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox runat ="server" ID="chkPPSelect" ClientIDMode="Static" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                    <asp:BoundField DataField='mpc_cd' HeaderText='Code' HeaderStyle-Width="50px"  HeaderStyle-HorizontalAlign="Left"  />
                                    <asp:BoundField DataField='mpc_desc' HeaderText='Description' HeaderStyle-Width="300px"  HeaderStyle-HorizontalAlign="Left"  />
                                </Columns>

                            </asp:GridView>
                        </asp:Panel>
                    </div>
                  </div>
                  <%--empty row--%>
                  <div style="float: left; width: 1%; font-size:12px; "> &nbsp;
                  </div>

                  <div style="float: left; width: 35%; font-size:12px; background-color:Lavender; height:171px;">
                    <asp:Panel ID="pnlPPPCAssign" runat="server" ScrollBars="Auto" >
                       <asp:GridView runat="server" ID="gvPPPCAssign" AutoGenerateColumns ="false" >
                         <HeaderStyle BackColor="#153E7E" ForeColor="White" Font-Size="11px"    />
                         <RowStyle Font-Size="11px"   /> 
                         <Columns>
                             <asp:BoundField DataField='sadd_pc' HeaderText='Profit Center' HeaderStyle-Width="50px"  HeaderStyle-HorizontalAlign="Left"  />
                             <asp:BoundField DataField='sadd_doc_tp' HeaderText='Invoice Type' HeaderStyle-Width="100px"  HeaderStyle-HorizontalAlign="Left"  />
                             <asp:BoundField DataField='sadd_pb' HeaderText='Book' HeaderStyle-Width="100px"  HeaderStyle-HorizontalAlign="Left"  />
                             <asp:BoundField DataField='sadd_p_lvl' HeaderText='Level' HeaderStyle-Width="100px"  HeaderStyle-HorizontalAlign="Left"  />
                         </Columns>
                       </asp:GridView> 
                    </asp:Panel>
                  </div>
             </div>

             <div style="float: left; width: 6%; font-size:12px; background-color:Silver; height:171px;"> 
                <asp:Button runat="server" ID="btnPPConfirm" Text="Confirm" CssClass="Button" OnClick="SavePriceDefinition" style=" margin-bottom:1px;  width:100%; text-align:left;" />
                <asp:Button runat="server" ID="btnPPClear" Text="Clear" CssClass="Button"  style=" margin-bottom:1px;  width:100%; text-align:left;" />
             </div>

             <div style="float: left; width: 1%; font-size:12px; height:5px; ">  &nbsp;
             </div>

             
             <div style="float: left; width: 92%; font-size:12px; height:171px;"> 
                <asp:Panel ID="pnlPPEntry" runat ="server" ScrollBars ="Auto" >
                    <asp:GridView runat ="server" ID="gvPPEntry" AutoGenerateColumns ="false" >
                        <HeaderStyle BackColor="#153E7E" ForeColor="White" Font-Size="11px"    />
                        <RowStyle Font-Size="11px"   /> 
                        <Columns>
                            <asp:BoundField DataField='sadd_pc' HeaderText='Profit Center' HeaderStyle-Width="250px"  HeaderStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField='sadd_doc_tp' HeaderText='Invoice Type' HeaderStyle-Width="250px"  HeaderStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField='sadd_pb' HeaderText='Price Book' HeaderStyle-Width="250px"  HeaderStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField='sadd_p_lvl' HeaderText='Price Level' HeaderStyle-Width="250px"  HeaderStyle-HorizontalAlign="Left" />
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
             </div>
            

            </ContentTemplate>
        </asp:TabPanel>

        <%-- =============================== Main Tab for Price Book Creation =============================== --%>
        <asp:TabPanel runat="server" ID="tbpBookCreation" HeaderText ="Price Book Creation" >
            <ContentTemplate >

                <asp:Accordion runat="server"  ID="acdPriceBookCreation" SelectedIndex="0"   AutoSize="None" FadeTransitions="true" 
                    TransitionDuration="250"    FramesPerSecond="40"     RequireOpenedPane="true"    SuppressHeaderPostbacks="true" Font-Strikeout="False">
                        <Panes>
                            <asp:AccordionPane runat="server" ID="accPanel1"   >
                                <Header><div style="float: left; width: 100%;  background-color:Gray; color:Black; font-size:11px; border-color:LightGray;border-style:solid; border-width:1px; padding-top:1px; ">  Book Creation</div></Header>
                                <Content>
                                    
                                    <div style="float: left; width: 94%; font-size:12px; background-color:Lavender;">
                                        <div style="float: left; width: 100%; font-weight: normal; padding-top: 1px; padding-bottom: 1px; font-size:13px; height:13px;">
                                            <div style="float: left; width: 110px; background-color:#153E7E; border-right:1px solid white; color:White;">Book</div>
                                            <div style="float: left; width: 455px; background-color:#153E7E; border-right:1px solid white; color:White;">Description</div>
                                            <div style="float: left; width: 72px; background-color:#153E7E; border-right:1px solid white; color:White;">Status</div>
                                        </div>
                                        <div style="float: left; width: 100%; font-weight: normal; padding-top: 1px; padding-bottom: 1px; font-size:13px;">
                                            <div style="float: left; width: 110px; border-right:1px solid white;"><asp:TextBox runat="server" ID="txtBCBook" CssClass="TextBox" ClientIDMode="Static"  Width ="96%"  style=" text-transform:uppercase;"></asp:TextBox></div>
                                            <div style="float: left; width: 456px;border-right:1px solid white;"><asp:TextBox runat="server" ID="txtBCDescription"   ClientIDMode="Static"  CssClass="TextBox"  Width ="99%"  style=" text-transform:uppercase;"></asp:TextBox></div>
                                            <div style="float: left; width: 72px; border-right:1px solid white;"><asp:DropDownList ID="ddlBCStatus" runat ="server" Width="95%"  ><asp:ListItem Text="Active" Value="Active" ></asp:ListItem><asp:ListItem Text="Inactive" Value="Inactive" ></asp:ListItem></asp:DropDownList></div>
                                            <div style="float: left; width: 110px; border-right:1px solid white;"><asp:ImageButton runat="server" ID="imgBtnBCAdd" ImageAlign="Middle" ImageUrl="~/Images/Add-16x16x16.ICO" Width="16px" Height="16px" OnClick="AddBook" /> </div>
                                        </div>
                                        <div style="float: left; width: 100%;">
                                        <asp:Panel ID="pnlBookCreate" runat ="server" ScrollBars="Auto" >
                                            <asp:GridView ID="gvBCBook" runat="server" AutoGenerateColumns="false" GridLines="Both" OnRowDataBound="BookRowDataBound" >
                                                <HeaderStyle BackColor="White" ForeColor="White" Font-Size="0px"    />
                                                <RowStyle Font-Size="11px"   />
                                                <Columns>
                                                    <asp:BoundField DataField='sapb_pb' HeaderText='Book' HeaderStyle-Width="107px"  HeaderStyle-HorizontalAlign="Left"  />
                                                    <asp:BoundField DataField='sapb_desc' HeaderText='Description' HeaderStyle-Width="450px"  HeaderStyle-HorizontalAlign="Left"  />
                                                    <asp:BoundField DataField='sapb_act' HeaderText='Status' HeaderStyle-Width="73px"  HeaderStyle-HorizontalAlign="Left"  />
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>
                                    </div>
                                    <div style="float: left; width: 6%; font-size:12px; background-color:Silver;"> 
                                        <asp:Button runat="server" ID="btnBCConfirm" Text="Confirm" CssClass="Button" OnClick="SavePriceBook" style=" margin-bottom:1px;  width:100%; text-align:left;" />
                                        <asp:Button runat="server" ID="btnBCClear" Text="Clear" CssClass="Button"  style=" margin-bottom:1px;  width:100%; text-align:left;" />
                                    </div>

                                </Content>
                            </asp:AccordionPane>
                            <asp:AccordionPane runat="server" ID ="accPanel2"    >
                                <Header><div style="float: left; width: 100%; background-color:Gray; color:Black; font-size:11px; border-color:LightGray;border-style:solid; border-width:1px; padding-top:1px; ">Level Creation</div></Header>
                                <Content>

                                    <div style="float: left; width: 94%; font-size:12px; background-color:Lavender;" >
                                        <div style="float: left; width: 100%; font-size:12px; margin-top:2px; ">
                                            <div style="float: left; width: 1%;">&nbsp;</div>
                                            <div style="float: left; width: 10%;">Book</div>
                                            <div style="float: left; width: 22%;"><asp:ListSearchExtender id="ListSearchExtender0" runat="server"    TargetControlID="ddlLCBook"   PromptText=""     PromptPosition="Top"    IsSorted="true"  QueryPattern="StartsWith"   /> <asp:DropDownList runat="server" ID="ddlLCBook" Width="45%" Font-Size="11px" ></asp:DropDownList> </div>
                                            <div style="float: left; width: 1%;">&nbsp;</div>
                                            <div style="float: left; width: 10%;">Level</div>
                                            <div style="float: left; width: 22%;"><asp:TextBox runat="server" ID="txtLCLevel" Width="40%" CssClass="TextBox" ></asp:TextBox> &nbsp; <asp:ImageButton ID="imgBtnLCLevel" ClientIDMode="Static" runat ="server" ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" OnClick="imgBtnPriceLevel_Click"  /> </div>
                                            <div style="float: left; width: 1%;">&nbsp;</div>
                                            <div style="float: left; width: 10%;">Status</div>
                                            <div style="float: left; width: 22%; "><asp:ListSearchExtender id="ListSearchExtender2" runat="server"    TargetControlID="ddlLCStatus"   PromptText=""     PromptPosition="Top"    IsSorted="true"  QueryPattern="StartsWith"   /> <asp:DropDownList runat="server" ID="ddlLCStatus" Width="45%" Font-Size="11px" ></asp:DropDownList> &nbsp; <asp:CheckBox ID="chkLCIsCheckStatus" runat="server" Text="Check Status"  Font-Size="Smaller"/></div>
                                            <div style="float: left; width: 1%;">&nbsp;</div>
                                        </div>
                                        <div style="float: left; width: 100%;">
                                       
                                        </div>
                                        <div style="float: left; width: 100%; font-size:12px; margin-top:2px;">
                                            <div style="float: left; width: 1%;">&nbsp;</div>
                                            <div style="float: left; width: 10%;">Currency</div>
                                            <div style="float: left; width: 22%;"><asp:ListSearchExtender id="ListSearchExtender3" runat="server"    TargetControlID="ddlLCCurrency" PromptText=""     PromptPosition="Top"    IsSorted="true"  QueryPattern="StartsWith"   /> <asp:DropDownList runat="server" ID="ddlLCCurrency" Width="45%" Font-Size="11px"  ></asp:DropDownList></div>
                                            <div style="float: left; width: 1%;">&nbsp;</div>
                                            <div style="float: left; width: 10%;">Credit Period</div>
                                            <div style="float: left; width: 22%;"><asp:TextBox runat="server" ID="txtLCCreditPeriod" CssClass="TextBox" Width="20%" ></asp:TextBox> </div>
                                            <div style="float: left; width: 1%;">&nbsp;</div>
                                            <div style="float: left; width: 10%;">Warranty Base</div>
                                            <div style="float: left; width: 22%;"><asp:CheckBox runat="server" ID="chkLCIsWarranty" style="font-size:smaller;" />  Period (Months)  <asp:TextBox runat="server" ID="txtLCWaraPeriod" CssClass="TextBox" Font-Size="Smaller" Width="20%"></asp:TextBox>  </div>
                                            <div style="float: left; width: 1%;">&nbsp;</div>
                                        </div>
                                        <div style="float: left; width: 2%; margin-top:5px;">&nbsp;</div>
                                        <div style="float: left; width: 95%; font-size:11px; border:1px solid black;  margin-top:5px; background-color:Silver;">
                                            <div style="float: left; width: 17%;"><asp:CheckBox ID="chkLCIsSerialzed" runat="server" Text ="Enable Serialized" /></div>
                                            <div style="float: left; width: 17%;"><asp:CheckBox ID="chkLCWOPrice" runat="server" Text ="Enable Without Price" /></div>
                                            <div style="float: left; width: 17%;"><asp:CheckBox ID="chkLCTransfer" runat="server" Text ="Enable Transfer" /></div>
                                            <div style="float: left; width: 17%;"><asp:CheckBox ID="chkLCVat" runat="server" Text ="Enable VAT" /></div>
                                            <div style="float: left; width: 23%;"><asp:CheckBox ID="chkLCTotalQty" runat="server" Text ="Enable Base on Totak Qty" /></div>
                                        </div>
                                        <div style="float: left; width: 100%;">
                                        <asp:Panel ID="pnlLevel" runat="server" ScrollBars="Auto" >
                                            
                                            <asp:GridView runat="server" ID="gvLCLevel" AutoGenerateColumns="false" >
                                                <HeaderStyle BackColor="#153E7E" ForeColor="White" Font-Size="11px"    />
                                                <RowStyle Font-Size="12px"   />
                                                <Columns>
                                                    <asp:BoundField DataField='sapl_pb' HeaderText='Book' HeaderStyle-Width="100px"  HeaderStyle-HorizontalAlign="Left"  />
                                                    <asp:BoundField DataField='sapl_pb_lvl_cd' HeaderText='Level' HeaderStyle-Width="100px"  HeaderStyle-HorizontalAlign="Left"  />
                                                    <asp:BoundField DataField='sapl_itm_stuts' HeaderText='Status' HeaderStyle-Width="100px"  HeaderStyle-HorizontalAlign="Left"  />
                                                    <asp:BoundField DataField='sapl_chk_st_tp' HeaderText='Check Status' HeaderStyle-Width="100px"  HeaderStyle-HorizontalAlign="Left"  />
                                                    <asp:BoundField DataField='sapl_currency_cd' HeaderText='Currancy' HeaderStyle-Width="100px"  HeaderStyle-HorizontalAlign="Left"  />
                                                    <asp:BoundField DataField='sapl_credit_period' HeaderText='Credit Period' HeaderStyle-Width="100px"  HeaderStyle-HorizontalAlign="Left"  />
                                                    <asp:BoundField DataField='sapl_set_warr' HeaderText='Warra. Base' HeaderStyle-Width="100px"  HeaderStyle-HorizontalAlign="Left"  />
                                                    <asp:BoundField DataField='sapl_warr_period' HeaderText='Warra. Period' HeaderStyle-Width="100px"  HeaderStyle-HorizontalAlign="Left"  />
                                                    <asp:BoundField DataField='sapl_is_serialized' HeaderText='Serialized' HeaderStyle-Width="100px"  HeaderStyle-HorizontalAlign="Left"  />
                                                    <asp:BoundField DataField='sapl_is_without_p' HeaderText='W/O Price' HeaderStyle-Width="100px"  HeaderStyle-HorizontalAlign="Left"  />
                                                    <asp:BoundField DataField='sapl_is_transfer' HeaderText='Transfer' HeaderStyle-Width="100px"  HeaderStyle-HorizontalAlign="Left"  />
                                                    <asp:BoundField DataField='sapl_vat_calc' HeaderText='VAT' HeaderStyle-Width="100px"  HeaderStyle-HorizontalAlign="Left"  />
                                                    <asp:BoundField DataField='sapl_base_on_tot_inv_qty' HeaderText='On Total Qty' HeaderStyle-Width="100px"  HeaderStyle-HorizontalAlign="Left"  />

                                                </Columns>
                                            </asp:GridView>

                                        </asp:Panel>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 6%; font-size:12px; background-color:Silver;"> 
                                        <asp:Button runat="server" ID="btnLCConfirm" Text="Confirm" CssClass="Button" OnClick="SaveLevel" style=" margin-bottom:1px;  width:100%; text-align:left;" />
                                        <asp:Button runat="server" ID="btnLCClear" Text="Clear" CssClass="Button" OnClick="ClearLevelCreation" style=" margin-bottom:1px;  width:100%; text-align:left;" />
                                    </div>
                                    
                                </Content>
                            </asp:AccordionPane>
                        </Panes>
                </asp:Accordion>

            </ContentTemplate>
        </asp:TabPanel>
        
    </asp:TabContainer>

    </div>

    <%-- Control Area --%>
    <div style="display: none;">
        <asp:ImageButton ID="imgBtnPriceBook" runat="server" OnClick="imgBtnPriceBook_Click" />
        <asp:Button ID="btnBCBook" runat="server" OnClick="CheckBCBook" />

        <asp:Button ID="btnLCBook" runat="server" OnClick="CheckLCBook" />
        <asp:Button ID="btnLCLevel" runat="server" OnClick="CheckLCLevel" />
        <asp:Button ID="btnLCStatus" runat="server" OnClick="CheckLCStatus" />

         <asp:Button ID="btnPPCode" runat="server" OnClick="LoadPPCentersByCodeLike" />
         <asp:Button ID="btnPPDescription" runat="server" OnClick="LoadPPCentersByCodeLike" />
         
    </div>
   
</div>
</asp:Content>
