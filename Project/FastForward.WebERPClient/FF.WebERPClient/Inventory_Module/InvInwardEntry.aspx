<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InvInwardEntry.aspx.cs" Inherits="FF.WebERPClient.WebForm3" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<br />
<div>
    
    <asp:Panel ID="rootPanel" runat="server" style="margin-left: 0px" 
        Width="962px">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnSave1" runat="server" CssClass="Button" 
                                onclick="btnSave_Click" Text="Save" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnClear0" runat="server" CssClass="Button" 
                                onclick="btnClear_Click" Text="Clear" />
                            &nbsp;
                        
    <br />
    <div>
       <asp:Panel runat="server" ID="cp_pending" BorderColor="#0066FF">
        <div style="font-weight: bold; background-color: ThreeDShadow;">
            <asp:Label ID="lblPendingOutEnt" runat="server" 
                Text="&gt;&gt;Pending outward Entries" Font-Bold="True" Height="25px"></asp:Label> 
        </div>

        <asp:CollapsiblePanelExtender runat="server" ID="cpe" TargetControlID="colPanel" ExpandControlID="cp_pending" CollapseControlID= "cp_pending" Collapsed="true" CollapsedSize="0" ExpandedSize="120" ExpandedText="(Collapse...)" CollapsedText="(Expand...)" TextLabelID="textLabel" >
    </asp:CollapsiblePanelExtender>
        </asp:Panel>  

        <asp:Panel ID="colPanel" runat="server">
            <br />
            <div>
                <div style="width: 956px">
                <asp:Label ID="Label1" runat="server" Text="Outward Type:"></asp:Label>
                &nbsp;<asp:DropDownList ID="DropDownList1" runat="server">
            </asp:DropDownList> 
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="checkAll" runat="server" />
                    &nbsp;All&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; From:
                    <asp:DropDownList ID="DropDownList2" runat="server">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp; To:
                    <asp:DropDownList ID="DropDownList3" runat="server">
                    </asp:DropDownList>
                </div>
                <br />
                <div>
                    <asp:GridView ID="GridViewPend" runat="server" AutoGenerateColumns="False" 
                        BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                        CellPadding="4" ShowHeaderWhenEmpty="True">
                        <Columns>
                            <asp:BoundField HeaderText="Outward Entry No" />
                            <asp:BoundField HeaderText="Entry Date" />
                            <asp:BoundField HeaderText="Entry Type" />
                            <asp:BoundField HeaderText="Ref-No" />
                            <asp:BoundField HeaderText="Company" />
                            <asp:BoundField HeaderText="Location" />
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
                    <br />
                </div>
            <br />
                <asp:Label ID="lblPendingOut" runat="server" Text="Pending outward Entries--End">
                </asp:Label>
            <br />
            </div>
           
            
        </asp:Panel>
          
        
    </div>  
    <br />
   
    <%--General --%>
    
    <div>
        <asp:Panel runat="server" ID="cp_general" BorderColor="#0066FF">
        <div style="font-weight: bold; background-color: ThreeDShadow;">
            <asp:Label ID="Label2" runat="server" 
                Text="&gt;&gt; General" Font-Bold="True" Height="25px"></asp:Label> 
        </div>

         <asp:CollapsiblePanelExtender runat="server" ID="genCpe" TargetControlID="colPanel2" ExpandControlID="cp_general" CollapseControlID= "cp_general" Collapsed="true" CollapsedSize="0" ExpandedSize="120" ExpandedText="(Collapse...)" CollapsedText="(Expand...)" TextLabelID="textLabel" >
    </asp:CollapsiblePanelExtender>
        </asp:Panel>

        <asp:Panel ID="colPanel2" runat="server">
        <br />
        <div>
            <div> 
                <asp:Label ID="Label3" runat="server" Text="Document No "></asp:Label> 
                <asp:TextBox ID="TextBox1" runat="server" Height="21px" Width="122px"></asp:TextBox> 
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    <asp:Label ID="Label4" runat="server" Text="Outward Company "></asp:Label>
                <asp:DropDownList ID="ddlOutCompany" runat="server" Height="16px" Width="131px">
                </asp:DropDownList>
                &nbsp;</div>
            <div>
                <asp:Label ID="Label5" runat="server" Text="Document Date "></asp:Label>
                <asp:DropDownList ID="ddlDocDate" runat="server">
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                <asp:Label ID="Label10" runat="server" Text="Outward Location "></asp:Label>
                &nbsp;<asp:DropDownList ID="ddlOutLoc0" runat="server" Height="17px" Width="132px">
                </asp:DropDownList>
            </div>
             <div>
                <asp:Label ID="Label6" runat="server" Text="Document Type "></asp:Label>
                <asp:DropDownList ID="ddlDocType" runat="server">
                </asp:DropDownList>
            </div>
             <div>
                <asp:Label ID="Label7" runat="server" Text="Document Sub Type "></asp:Label>
                <asp:DropDownList ID="ddlDocSubType" runat="server">
                </asp:DropDownList> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:Label ID="Label11" runat="server" Text="Remarks"></asp:Label>
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:TextBox ID="txtRemarks0" runat="server" Height="16px" TextMode="MultiLine" 
                     Width="181px"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="Label8" runat="server" Text="Outward Entry No "></asp:Label>
                <asp:TextBox ID="txtOutwEntryNo" runat="server"></asp:TextBox>
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                 <asp:Label ID="Label12" runat="server" Text="Vehicle No "></asp:Label>
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtVehicleNo1" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="Label9" runat="server" Text="Manual Ref No "></asp:Label>
                <asp:TextBox ID="txtManRefNo" runat="server"></asp:TextBox>
            </div>
        </div>
        </asp:Panel>
    </div>
    <br />    
    <div>
        <asp:Panel ID="cp_Items" runat="server">
        <div style="font-weight: bold; background-color: ThreeDShadow;">
            <asp:Label ID="Label13" runat="server" 
                Text="&gt;&gt;Items" Font-Bold="True" Height="25px"></asp:Label> 
        </div>
        <asp:CollapsiblePanelExtender runat="server" ID="CollapsiblePanelExtender1" TargetControlID="col_panel3" ExpandControlID="cp_Items" CollapseControlID= "cp_Items" Collapsed="true" CollapsedSize="0" ExpandedSize="120" ExpandedText="(Collapse...)" CollapsedText="(Expand...)" TextLabelID="textLabel" >
    </asp:CollapsiblePanelExtender>
        </asp:Panel>

        <asp:Panel ID="col_panel3" runat="server">
        <br />
            <div>
                <div style="width: 956px">
                <asp:Label ID="Label14" runat="server" Text="Item Code:"></asp:Label>
                &nbsp;<asp:TextBox ID="txtItemCode" runat="server"></asp:TextBox>
             
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Item description&nbsp;
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp; Model&nbsp;
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp; Item Status
                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                </div>
                <div>
                
                    Qty
                    <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                    &nbsp;
                    <asp:Button ID="Button2" runat="server" Text="..." />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" Text="ok" onclick="Button1_Click" />
                
                </div>
                <br />
                <div>
                    <asp:GridView ID="GridView_Items" runat="server" AutoGenerateColumns="False" 
                        BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                        CellPadding="4" ShowHeaderWhenEmpty="True" Width="466px">
                        <Columns>
                            <asp:CheckBoxField />
                            <asp:CommandField ShowDeleteButton="True" />
                            <asp:BoundField HeaderText="Item Code" />
                            <asp:BoundField HeaderText="Item Description" />
                            <asp:BoundField HeaderText="Model" />
                            <asp:BoundField HeaderText="Brand" />
                            <asp:BoundField HeaderText="Item Status" />
                            <asp:BoundField HeaderText="Out Qty" />
                            <asp:BoundField HeaderText="Qty" />
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
                    <br />
                </div>
            <br />
                <asp:Label ID="Label15" runat="server" Text="Items--End"></asp:Label>
            <br />
            </div>
           
            
        </asp:Panel>

    </div>
 </asp:Panel> <%--root panel--%>
      
</div>
<br />
<br />
</asp:Content>
