<%@ Page Title="Customer Creation" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CustomerCreation.aspx.cs" Inherits="FF.WebERPClient.General_Modules.CustomerCreation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControls/uc_CustomerCreation.ascx" TagName="uc_CustomerCreation"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/uc_CustCreationExternalDet.ascx" TagName="uc_CustCreationExternalDet"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <%--    <style type="text/css">
        .GridView
        {}
    </style>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function onblurFire(e, buttonid) {
            var evt = e ? e : window.event;
            var bt = document.getElementById(buttonid);
            if (bt) {

                bt.click();
                return false;


            }
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

        function GetItemData() {
            var itemCode = document.getElementById("txtItemCD").value;
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
    <asp:UpdatePanel ID="updtMainPnl" runat="server">
    <ContentTemplate>
    
   
    <div style="float: left; width: 100%; text-align: right;">
        <div style="float: left; width: 30%; visibility: hidden;">
            <asp:TextBox ID="txtHiddenCustCD" runat="server" OnTextChanged="txtHiddenCustCD_TextChanged"></asp:TextBox>
        </div>
        <asp:Button ID="btn_CREATE" runat="server" Text="Create" CssClass="Button" OnClick="btn_CREATE_Click" />
        <asp:Button ID="btn_UPDATE" runat="server" Text="Update" CssClass="Button" OnClick="btn_UPDATE_Click" />
        <asp:Button ID="btn_CLEAR" runat="server" Text="Clear" CssClass="Button" OnClick="btn_CLEAR_Click" />
        <asp:Button ID="btn_CLOSE" runat="server" Text="Close" CssClass="Button" OnClick="btn_CLOSE_Click" />
    </div>
    <div style="float: left; width: 100%;">
        <div style="float: left; width: 55%;">
            <uc1:uc_CustomerCreation ID="uc_CustomerCreation1" runat="server" />
            <uc2:uc_CustCreationExternalDet ID="uc_CustCreationExternalDet1" runat="server" />
            <br />
        </div>
        <div style="float: left; width: 44%;">

            <div style="float: left; width: 100%; height: 130px;">
                <asp:Panel ID="Panel5" runat="server"  GroupingText="Customer Segmentation" >
                
                        <asp:Panel ID="Panel_custSeg" runat="server" 
                            GroupingText="" Height="95px" 
                            ScrollBars="Horizontal">
                <asp:GridView ID="grvCustSegmentation" runat="server" AutoGenerateColumns="False"
                    OnRowDataBound="grvCustSegmentation_RowDataBound" CellPadding="4" ForeColor="#333333"
                    Width="403px" Style="text-align: center" CssClass="GridView">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="Is Mandatory" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblgrvIsMandVal" runat="server" Text='<%# Bind("IsMandatory") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Is Mandatory">
                            <ItemTemplate>
                                <asp:Label ID="lblgrvIsMand" runat="server" Text='<%# Bind("IsMandatory") %>'></asp:Label>
                                <asp:CheckBox ID="checkIsMandetory" runat="server" Enabled="false"/>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Type">
                            <ItemTemplate>
                                <asp:Label ID="lblgrvTypeName" runat="server" Text='<%# Bind("TypeCD_") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Value">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server"></asp:Label>
                                <br />
                                <asp:DropDownList ID="ddlgrvTypeVal" runat="server" Height="18px" Width="166px" CssClass="ComboBox">
                                </asp:DropDownList>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                            </EditItemTemplate>
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
           <%-- <div style="padding: 1.0px; float: left; width: 100%;">
            </div>--%>
            <div style="float: left; width: 100%;" runat="server" id="divPermission">
                <div style="float: left; width: 100%;">
                    <asp:Panel ID="Panel1" runat="server" GroupingText=" ">
                        <div style="padding: 1.0px; float: left; width: 100%;">
                            <div style="float: left; width: 20%; text-align: center;">
                                <asp:Label ID="Label4" runat="server" Text="Showroom status:"></asp:Label>
                            </div>
                            <div style="float: left; width: 30%;">
                                <asp:DropDownList ID="ddlSH_status" runat="server" CssClass="ComboBox">
                                    <asp:ListItem>GOOD</asp:ListItem>
                                    <asp:ListItem>FAIR</asp:ListItem>
                                    <asp:ListItem>BLACK LIST</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div style="float: left; width: 20%; text-align: center;">
                                <asp:Label ID="Label5" runat="server" Text="
                                 Head office:"></asp:Label>
                            </div>
                            <div style="float: left; width: 30%;">
                                <asp:DropDownList ID="ddlHO_status" runat="server" CssClass="ComboBox">
                                    <asp:ListItem>GOOD</asp:ListItem>
                                    <asp:ListItem>FAIR</asp:ListItem>
                                    <asp:ListItem>BLACK LIST</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div style="float: left; width: 100%;">
                    <asp:Panel ID="Panel_creditLimit" runat="server" GroupingText="Credit Limit">
                        <div style="float: left; width: 100%;">
                            <div style="float: left; width: 10%; text-align: center;">
                                &nbsp;&nbsp;</div>
                            <div style="float: left; width: 30%;">
                            </div>
                            <div style="float: left; width: 10%; text-align: center;">
                            </div>
                            <div style="float: left; width: 30%;">
                            </div>
                            <div style="float: left; width: 100%;">
                                <div style="float: left; width: 15%; text-align: center;">
                                    <asp:Label ID="Label1" runat="server" Text="Credit Limit:"></asp:Label>
                                </div>
                                <div style="float: left; width: 30%;">
                                    <asp:TextBox ID="txtCredLimit" runat="server" CssClass="TextBox" Width="80%"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div style="float: left; width: 100%;">
                </div>
                <div style="float: left; width: 100%;">
                    <asp:Panel ID="Panel_Discounts" runat="server" GroupingText="Discounts">
                        <asp:Panel ID="Panel3" runat="server" GroupingText=" ">
                            <div style="float: left; width: 100%;">
                                <div style="float: left; width: 15%; text-align: center;">
                                    <asp:Label ID="Label2" runat="server" Text="From:"></asp:Label>
                                </div>
                                <div style="float: left; width: 30%;">
                                    <asp:TextBox ID="txtFromDT" runat="server" CssClass="TextBox" OnTextChanged="TextBox4_TextChanged" Width="80%"></asp:TextBox>
                                    <asp:CalendarExtender ID="txtFromDT_CalendarExtender" runat="server" Enabled="True"
                                Format="dd/MM/yyyy" TargetControlID="txtFromDT"></asp:CalendarExtender>
                                </div>
                                <div style="float: left; width: 15%; text-align: center;">
                                    <asp:Label ID="Label6" runat="server" Text="To:"></asp:Label>
                                </div>
                                <div style="float: left; width: 30%;">
                                    <asp:TextBox ID="txtToDT" runat="server" CssClass="TextBox" OnTextChanged="TextBox5_TextChanged1" Width="80%"></asp:TextBox>
                                    <asp:CalendarExtender ID="txtToDT_CalendarExtender" runat="server" Enabled="True"
                                Format="dd/MM/yyyy" TargetControlID="txtToDT"></asp:CalendarExtender>
                                </div>
                                 <div style="padding: 2.0px; float: left; width: 1%;">
                            </div>
                            </div>
                            <div style="padding: 1.0px; float: left; width: 100%;">
                            </div>
                            <div style="float: left; width: 100%;">
                                <div style="float: left; width: 15%; text-align: left;">
                                    <asp:Label ID="Label7" runat="server" Text="Sales Type"></asp:Label>
                                </div>
                                <div style="float: left; width: 70%;">
                                    <asp:DropDownList ID="ddlSalesTp" runat="server" CssClass="ComboBox" 
                                        Width="250px">
                                    </asp:DropDownList>
                                </div>
                               
                            </div>
                            <div style="float: left; width: 100%;">
                                <div style="float: left; width: 15%; text-align: left;">
                                    <asp:Label ID="Label9" runat="server" Text="Company"></asp:Label>
                                </div>
                                <div style="float: left; width: 30%;">
                                    <asp:TextBox ID="txtComCode" runat="server" CssClass="TextBox" Width="60%"></asp:TextBox>
                                    <asp:ImageButton ID="imgbtnComSearch" runat="server" 
                                        ImageUrl="~/Images/icon_search.png" onclick="imgbtnComSearch_Click" />

                                </div>
                                <div style="float: left; width: 15%; text-align: left;">
                                    <asp:Label ID="Label10" runat="server" Text="Profit Center" Width="60%"></asp:Label>
                                </div>
                                <div style="float: left; width: 38%;">
                                    <asp:TextBox ID="txtDiscPC" runat="server" CssClass="TextBox" OnTextChanged="TextBox8_TextChanged" Width="50%"></asp:TextBox>
                                    <asp:ImageButton ID="imgPCsearch" runat="server" 
                                        ImageUrl="~/Images/icon_search.png" onclick="imgPCsearch_Click"/>
                                    <asp:CheckBox ID="chkAllPC" runat="server" Font-Size="X-Small" ForeColor="Blue" Text="All"
                                        AutoPostBack="True" OnCheckedChanged="chkAllPC_CheckedChanged" />
                                </div>
                            </div>
                            <div style="float: left; width: 100%;">
                                <div style="float: left; width: 15%; text-align: left;">
                                    <asp:Label ID="Label11" runat="server" Text="Price Book"></asp:Label>

                                </div>
                                <div style="float: left; width: 30%;">
                                    <asp:TextBox ID="txtPB" runat="server" CssClass="TextBox" Width="60%"></asp:TextBox>
                                    <asp:ImageButton ID="imgPBsearch" runat="server" 
                                        ImageUrl="~/Images/icon_search.png" onclick="imgPBsearch_Click"/>
                                </div>
                                <div style="float: left; width: 15%; text-align: left;">
                                    <asp:Label ID="Label12" runat="server" Text="P.Book Level" Width="60%"></asp:Label>
                                </div>
                                <div style="float: left; width: 31%;">
                                    <asp:TextBox ID="txtPBLvl" runat="server" CssClass="TextBox" Width="60%"></asp:TextBox>
                                    <asp:ImageButton ID="imgPBLsearch" runat="server" 
                                        ImageUrl="~/Images/icon_search.png" onclick="imgPBLsearch_Click"/>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel2" runat="server" GroupingText=" ">
                            <div style="float: left; width: 100%;">
                                <div style="float: left; width: 15%; text-align: left;">
                                    <asp:Label ID="Label8" runat="server" Text="Brand"></asp:Label>
                                </div>
                                <div style="float: left; width: 35%;">
                                    <asp:TextBox ID="txtBrand" runat="server" CssClass="TextBoxUpper" Width="60%" 
                                        MaxLength="20"></asp:TextBox>
                                    <asp:ImageButton ID="imgBrandSearch" runat="server" ImageUrl="~/Images/icon_search.png"
                                        OnClick="imgBrandSearch_Click" />
                                </div>
                                <div style="float: left; width: 15%; text-align: left;">
                                    <asp:Label ID="Label13" runat="server" Text="Main Category"></asp:Label>
                                </div>
                                <div style="float: left; width: 30%;">
                                    <asp:TextBox ID="txtCate1" runat="server" CssClass="TextBoxUpper" 
                                        AutoPostBack="True" Width="60%"
                                        OnTextChanged="txtCate1_TextChanged" MaxLength="20"></asp:TextBox>
                                    <asp:ImageButton ID="imgCate1Search" runat="server" ImageUrl="~/Images/icon_search.png"
                                        OnClick="imgCate1Search_Click" />
                                </div>
                            </div>
                            <div style="float: left; width: 100%;">
                                <div style="float: left; width: 15%; text-align: left;">
                                    <asp:Label ID="Label14" runat="server" Text="Category"></asp:Label>
                                </div>
                                <div style="float: left; width: 35%;">
                                    <asp:TextBox ID="txtCate2" runat="server" CssClass="TextBoxUpper" Width="60%" 
                                        MaxLength="20" AutoPostBack="True" ontextchanged="txtCate2_TextChanged"></asp:TextBox>
                                    <asp:ImageButton ID="imgCate2Search" runat="server" ImageUrl="~/Images/icon_search.png"
                                        OnClick="imgCate2Search_Click" />
                                </div>
                                <div style="float: left; width: 15%; text-align: left;">
                                    <asp:Label ID="Label15" runat="server" Text="Sub Category"></asp:Label>
                                </div>
                                <div style="float: left; width: 30%;">
                                    <asp:TextBox ID="txtCate3" runat="server" CssClass="TextBoxUpper" Width="60%" 
                                        MaxLength="20" AutoPostBack="True" ontextchanged="txtCate3_TextChanged"></asp:TextBox>
                                    <asp:ImageButton ID="imgCate3Search" runat="server" ImageUrl="~/Images/icon_search.png"
                                        OnClick="imgCate3Search_Click" />
                                </div>
                            </div>
                            <div style="float: left; width: 100%;">
                                <div style="float: left; width: 15%; text-align: left;">
                                    <asp:Label ID="Label16" runat="server" Text="Item Code"></asp:Label>
                                </div>
                                <div style="float: left; width: 40%;">
                                    <asp:TextBox ID="txtItemCD" runat="server" CssClass="TextBoxUpper" 
                                        MaxLength="20"></asp:TextBox>
                                    <asp:ImageButton ID="imgItmSearch" runat="server" ImageUrl="~/Images/icon_search.png"
                                        OnClick="imgItmSearch_Click" />
                                </div>
                                <div style="padding: 4.0px; float: left; width: 14%; text-align: center;">
                                </div>
                                <div style="padding: 1.0px; float: left; width: 30%;">
                                </div>
                            </div>
                            <div style="float: left; width: 100%;">
                                <div style="float: left; width: 50%; text-align: left;">
                                    <div style="float: left; width: 30%; text-align: left;">
                                        <asp:Label ID="Label17" runat="server" Text="Discount"></asp:Label>
                                    </div>
                                    <div style="float: left; width: 69%; text-align: left;">
                                        <div style="float: left; width: 100%;">
                                            <div style="float: left; width: 50%;">
                                                <asp:TextBox ID="txtDiscount" runat="server" CssClass="TextBox" Width="90%"></asp:TextBox>
                                            </div>
                                            <div style="float: left; width: 40%; text-align: center">
                                                <asp:CheckBox ID="chkIsRate" runat="server" Checked="True" Text="Rate" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div style="float: left; width: 15%; text-align: left;">
                                    <asp:Label ID="Label19" runat="server" Text="No. of times"></asp:Label>
                                </div>
                                <div style="float: left; width: 20%;">
                                    <asp:TextBox ID="txtNoOfcredTimes" runat="server" CssClass="TextBox" Width="60%"></asp:TextBox>
                                </div>
                            </div>
                            <div style="float: left; width: 100%;">
                                <div style="float: left; width: 35%; text-align: center;">
                                    <asp:CheckBox ID="chkAllowSer" runat="server" Text="Allow for serialize prices" />
                                </div>
                                <div style="float: left; width: 35%; text-align: center;">
                                    <asp:CheckBox ID="chkAllowProm" runat="server" Text="Allow for promotions" />
                                </div>
                                <div style="float: left; width: 29%; text-align: center;">
                                    <asp:Button ID="btnAddCredit" runat="server" Text="ADD" CssClass="Button" OnClick="btnAddCredit_Click" />
                                </div>
                            </div>
                        </asp:Panel>
                    </asp:Panel>
                </div>
                <div style="float: left; width: 100%; height: 143px;">
                    <asp:Panel ID="Panel4" runat="server" ScrollBars="Both" Height="100%">
                        <asp:GridView ID="grvCreditLimDet" runat="server" AutoGenerateColumns="False" 
                            CssClass="GridView" onrowcommand="grvCreditLimDet_RowCommand" 
                            DataKeyNames="Sgdd_seq" BackColor="White" BorderColor="#3366CC" 
                            BorderStyle="None" BorderWidth="1px" CellPadding="4">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                        <asp:ImageButton ID="btnImgDelete" runat="server" 
                                            ImageUrl="~/Images/Delete.png"  CommandName="DELETEITEM" 
                                            CommandArgument='<%# Eval("Sgdd_seq") + "|" + Eval("Sgdd_com") + "|" + Eval("Sgdd_pc") + "|" + Eval("Sgdd_sale_tp") + "|" + Eval("Sgdd_pb") + "|" + Eval("Sgdd_pb_lvl") + "|" + Eval("Sgdd_pc")+ "|" + Eval("Sgdd_from_dt")+ "|" + Eval("Sgdd_to_dt")  %>'/>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Sgdd_sale_tp" HeaderText="Sales type" 
                                    ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Sgdd_pb" HeaderText="Price Book" 
                                    ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Sgdd_pb_lvl" HeaderText="PB Level" 
                                    ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Sgdd_pc" HeaderText="Profit Center" 
                                    ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Sgdd_from_dt" HeaderText="From dt." 
                                    DataFormatString="{0:d}" />
                                <asp:BoundField DataField="Sgdd_to_dt" HeaderText="To dt." DataFormatString="{0:d}" />
                                <asp:BoundField DataField="Sgdd_disc_rt" HeaderText="Discount rate"  
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Sgdd_disc_val" HeaderText="Discount Value"  
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Sgdd_no_of_times" HeaderText="No. of Times" 
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                            <RowStyle BackColor="White" ForeColor="#003399" />
                            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                            <SortedAscendingCellStyle BackColor="#EDF6F6" />
                            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                            <SortedDescendingCellStyle BackColor="#D6DFDF" />
                            <SortedDescendingHeaderStyle BackColor="#002876" />
                        </asp:GridView>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
    <div style="visibility: hidden">
     <asp:Button ID="btnItmCD" runat="server" onclick="btnItmCD_Click"  />
    </div>
     </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>
