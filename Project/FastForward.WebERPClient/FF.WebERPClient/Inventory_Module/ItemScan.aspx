<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ItemScan.aspx.cs" Inherits="FF.WebERPClient.Inventory_Module.ItemScan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script language="javascript" type="text/javascript"  >

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

</script>
    <div style="float:left;width:100%; color:Black;">

<div style="float:left;width:100%; text-align:right;">
<asp:ImageButton ID="imgBtnReturn" runat="server" ImageUrl="~/Images/approve_img.png"  OnClick="ReturnToBasePage" />
</div>

<div style="float:left;width:30%; border-color:Silver;border-width:1px; border-bottom-style:solid;">

<div style="float:left;width:100%;height:30%;border-color:Silver;border-width:1px; border-bottom-style:solid;background-color:#EFEFFF;">

<div style="float:left;width:100%;">
  <div style="float:left; width:100%;">
   <div style="background-image:url('/Images/blue_01.jpg'); height:30px; background-repeat:no-repeat; width:33px; float:left;"></div>
   <div style="background-image:url('/Images/blue_03.jpg'); height:30px; background-repeat:repeat-x; width:82%; float:left; padding-top:4px; ">Request Detail</div>
   <div style="background-image:url('/Images/blue_05.jpg'); height:30px; background-repeat:no-repeat; width:16px; float:left;"></div>
   </div>
<asp:Panel ID="pnlReq" runat="server" ScrollBars="Horizontal" Height="121px">
<asp:GridView ID="gvRequest" runat="server" AutoGenerateColumns="false">
  <HeaderStyle BackColor="#153E7E" ForeColor="White"  Font-Size="12px"     />
  <RowStyle Font-Size="11px"  Height="12px"  />
<Columns>
<asp:BoundField DataField="tui_req_itm_cd" HeaderText="Item"  HeaderStyle-Width="120px" HeaderStyle-HorizontalAlign="Left"/>
<asp:BoundField DataField="tui_req_itm_stus" HeaderText="Status" HeaderStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" />
<asp:BoundField DataField="tui_req_itm_qty" HeaderText="Req. Qty"  HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
<asp:BoundField DataField="tui_pic_itm_qty" HeaderText="Pick Qty"  HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />

</Columns>
</asp:GridView>
</asp:Panel>
</div>
<div>&nbsp;</div>

<div style="float:left;width:100%; border-color:Silver;border-width:1px; border-bottom-style:solid;">
  <div style="float:left; width:100%;">
        <div style="background-image:url('/Images/blue_01.jpg'); height:30px; background-repeat:no-repeat; width:33px; float:left;"></div>
        <div style="background-image:url('/Images/blue_03.jpg'); height:30px; background-repeat:repeat-x; width:82%; float:left; padding-top:4px; ">Document Detail</div>
        <div style="background-image:url('/Images/blue_05.jpg'); height:30px; background-repeat:no-repeat; width:16px; float:left;"></div>
   </div>

<div style="float:left;width:100%; border-color:Silver;border-width:1px; border-bottom-style:solid; ">
<div style="float:left;width:25%; ">Date..........
</div>
<div style="float:left;width:25%;"><asp:Label ID="lblDate" runat="server" Text="" ClientIDMode="Static" ></asp:Label>
</div>
<div style="float:left;width:25%; ">Doc Type...
</div>
<div style="float:left;width:25%;"><asp:Label ID="lblDocType" runat="server" Text="" ClientIDMode="Static" ></asp:Label>
</div>
</div>

<div style="float:left;width:100%; border-color:Silver;border-width:1px; border-bottom-style:solid;">
<div style="float:left;width:25%; ">Batch. No
</div>
<div style="float:left;width:25%;"><asp:Label ID="lblSeqNo" runat="server" Text="" ClientIDMode="Static" ></asp:Label>
</div>
<div style="float:left;width:25%; ">User ID......
</div>
<div style="float:left;width:25%;"><asp:Label ID="lblUser" runat="server" Text="" ClientIDMode="Static" ></asp:Label>
</div>
</div>

<div style="float:left;width:100%; border-color:Silver;border-width:1px; border-bottom-style:solid; "> &nbsp;
</div>
 
<div style="float:left;width:100%;border-color:Silver;border-width:1px; border-bottom-style:solid; ">
<div style="float:left;width:80%; ">Allow Model Change....................................
</div>
<div style="float:left;width:20%;"><asp:Label ID="lblAllowModelChange" runat="server" Text="Yes" ></asp:Label>
</div>
</div>

<div style="float:left;width:100%;border-color:Silver;border-width:1px; border-bottom-style:solid; ">
<div style="float:left;width:80%; ">Allow Status Change.....................................
</div>
<div style="float:left;width:20%;"><asp:Label ID="lblAllowStatusChange" runat="server" Text="Yes" ></asp:Label>
</div>
</div>

<div style="float:left;width:100%; border-color:Silver;border-width:1px; border-bottom-style:solid;">
<div style="float:left;width:80%; ">Allow Any Qty..................................................
</div>
<div style="float:left;width:20%;"><asp:Label ID="lblAllowAnyQty" runat="server" Text="Yes" ></asp:Label>
</div>
</div>

<div style="float:left;width:100%; ">
<div style="float:left;width:80%; ">Allow Any Item................................................
</div>
<div style="float:left;width:20%;"><asp:Label ID="lblAllowAnyItem" runat="server" Text="Yes" ></asp:Label>
</div>
</div>


</div>
</div>



</div>

<div style="float:left;width:49.5%;height:127px; border-style:solid; border-color:Silver; border-width:1px;">
<div style="float:left;width:100%; height:1px;"> 
</div>
<div style="float:left;width:100%;">
<div style="float:left;width:20%;">Bin..........................
</div>
<div style="float:left;width:17%; "><asp:DropDownList ID="DDLBin" runat="server" Width="100%" Font-Names="Tahoma"  Font-Size="12px"   ></asp:DropDownList></div>

<div style="float:left;width:22%; ">&nbsp;&nbsp;Item.....................
</div>
<div style="float:left;width:40%;  "><asp:TextBox ID="txtItem" CssClass="TextBox" runat="server"  Width="87%"   ClientIDMode="Static"></asp:TextBox>
<asp:ImageButton ID="imgBtnItem" runat="server" ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" OnClick="imgBtnItem_Click"  />
</div>
</div>

<div style="float:left;width:100%;">
<div style="float:left;width:20%;">Status....................
</div>
<div style="float:left;width:17%;"><asp:DropDownList ID="DDLStatus" runat="server" Width="100%" Font-Names="Tahoma"  Font-Size="12px"   ></asp:DropDownList></div>

<div style="float:left;width:22%;">&nbsp;&nbsp;Serial 1................
</div>
<div style="float:left;width:40%;"><asp:TextBox ID="txtSerial1" CssClass="TextBox" runat="server"    ClientIDMode="Static" Width="87%"></asp:TextBox>
<asp:ImageButton ID="imgBtnSerial1" runat="server" ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" OnClick="imgBtnSerial_Click"  />
</div>
</div>

<div style="float:left;width:100%;">
<div style="float:left;width:20%;">Serial 2.................
</div>
<div style="float:left;width:80%;"><asp:TextBox ID="txtSerial2" CssClass="TextBox" runat="server" ClientIDMode="Static"  Width="85%"></asp:TextBox>&nbsp;
</div>
</div>

<div style="float:left;width:100%;">
<div style="float:left;width:20%;">Serial 3.................
</div>
<div style="float:left;width:80%;"><asp:TextBox ID="txtSerial3" CssClass="TextBox" runat="server" ClientIDMode="Static"  Width="85%"></asp:TextBox>
</div>
</div>

<div style="float:left;width:100%;">
<div style="float:left;width:20%;">MFC......................
</div>
<div style="float:left;width:22%;"><asp:TextBox ID="txtMFC" CssClass="TextBox" runat="server" Width="100%"  ClientIDMode="Static" Text="N/A"></asp:TextBox>
</div>

<div style="float:left;width:17%;">&nbsp;&nbsp;Qty.......
</div>
<div style="float:left;width:30%;"><asp:TextBox ID="txtQty" CssClass="TextBox" runat="server" Width="20%" ClientIDMode="Static"  onKeyPress="return numbersonly(event,false)"></asp:TextBox>
</div>
<div style="float:left;width:3%;">  &nbsp; <asp:ImageButton runat="server" ID="imgBtnAddItem" ImageAlign="Middle" ImageUrl="~/Images/Add-16x16x16.ICO" OnClick="AddItem" />
</div>
</div>

<div style="float:left;width:100%; height:10px;"> 
</div>

<%--Balance --%>
<div style="float:left;width:100%;">

<div style="float:left;width:50%;">
<div style="float:left;width:20%;"> &nbsp;
</div>
<div style="float:left;width:40%; background-color:Black; color:White; height:19px;"> &nbsp; Free Qty
</div>
<div style="float:left;width:20%; border-color:Black;border-style:solid; border-width:thin;" id="divFree" runat="server">&nbsp;
</div>
</div>

<div style="float:left;width:50%;">
<div style="float:left;width:20%;"> &nbsp;
</div>
<div style="float:left;width:40%;  background-color:Black; color:White; height:19px;">  &nbsp; Reserved Qty
</div>
<div style="float:left;width:20%;  border-color:Black;border-style:solid; border-width:thin;" id="divReserved" runat="server">&nbsp;
</div>
</div>

</div>


</div>

<div style="float:left;width:20%;height:128px; border-width:1px;border-color:Silver; ">

<div style="float:left;width:100%; height:56px;border-color:Silver;border-width:1px; border-bottom-style:solid;"><asp:Label ID="lblItmDescription" runat="server" Text="" ClientIDMode="Static"  Font-Names="Tahoma"  Font-Size="10px" Height="56px" Width="100%" style=" text-align:center;" ></asp:Label>
</div>

<div style="float:left;width:30%;border-color:Silver;border-width:1px; border-bottom-style:solid;">Model
</div>
<div style="float:left;width:70%;border-color:Silver;border-width:1px; border-bottom-style:solid;">&nbsp;<asp:Label ID="lblModel" runat="server" ClientIDMode="Static" Text=""></asp:Label>
</div>

<div style="float:left;width:30%;border-color:Silver;border-width:1px; border-bottom-style:solid;">Brand
</div>
<div style="float:left;width:70%;border-color:Silver;border-width:1px; border-bottom-style:solid;">&nbsp;<asp:Label ID="lblBrand" runat="server" ClientIDMode="Static" Text=""></asp:Label>
</div>

<div style="float:left;width:30%;border-color:Silver;border-width:1px; border-bottom-style:solid;">Serial
</div>
<div id="divSerialized" runat="server" style="float:left;width:10%;border-color:Silver;border-width:1px; border-bottom-style:solid; background-color:Green;">&nbsp;
</div>
<div  style="float:left;width:40%;border-color:Silver;border-width:1px; border-bottom-style:solid;">Sub Serial
</div>
<div id="divSSerialized" runat="server" style="float:left;width:10%;border-color:Silver;border-width:1px; border-bottom-style:solid;background-color:Red;">&nbsp;
</div>
<div style="float:left;width:30%;border-color:Silver;border-width:1px; border-bottom-style:solid;">Warranty
</div>
<div id="divWarranty" runat="server" style="float:left;width:10%;border-color:Silver;border-width:1px; border-bottom-style:solid; background-color:Green;">&nbsp;
</div>

</div>

<div style="float:left;width:70%;height:200px;border-color:Silver;border-width:1px; border-bottom-style:solid;">
 <asp:Panel ID="pnlMain" runat="server" ScrollBars="Auto" Height="150px">
     <asp:GridView runat="server" ID="gvMainSerial" DataKeyNames="tus_itm_cd,tus_itm_stus,tus_qty,tus_ser_1,tus_ser_id,tus_bin" ClientIDMode="Static" BackColor="White"  BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" AutoGenerateColumns="false"  OnRowDeleting="OnRemoveFromGrid" >
            <HeaderStyle BackColor="#153E7E" ForeColor="White"  Font-Size="12px" />
            <RowStyle Font-Size="11px"  Height="11px"  />
            <Columns>
                <asp:BoundField DataField="tus_bin" HeaderText="Bin" HeaderStyle-Width="30px"   HeaderStyle-HorizontalAlign="Left"/>
                <asp:BoundField DataField="tus_itm_cd" HeaderText="Item" HeaderStyle-Width="110px"  HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="tus_itm_stus" HeaderText="Status"  HeaderStyle-Width="30px"  HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="tus_qty" HeaderText="Qty"  HeaderStyle-Width="30px"  HeaderStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="tus_ser_1" HeaderText="Serial 1"  HeaderStyle-Width="90px"  HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="tus_ser_2" HeaderText="Serial 2"  HeaderStyle-Width="90px"  HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="tus_ser_3" HeaderText="Serial 3"  HeaderStyle-Width="90px"  HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="tus_warr_no" HeaderText="Warranty No"  HeaderStyle-Width="90px"  HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="tus_ser_id" HeaderText="Serial ID"  />
                <asp:TemplateField>
                   <ItemTemplate>
                   <asp:ImageButton runat="server" ID="imgBtnRemove" ImageAlign="Middle" ImageUrl="~/Images/Delete.png"  Height ="12px" Width="12px"  OnClientClick="return confirm('Do you want to delete?');" CommandName="Delete" />
                   </ItemTemplate>
                   </asp:TemplateField>
                   <asp:TemplateField>
                   <ItemTemplate>
                   <asp:ImageButton ID="imgBtnSubSerial" runat="server" ImageUrl="~/Images/icon_search.png" ImageAlign="Middle" Height ="12px" Width="12px"  />
                   </ItemTemplate>
                   </asp:TemplateField>
                
             </Columns>
     </asp:GridView>
</asp:Panel>
</div>

</div>
<%-- Control Area --%>
<div style="display: none;">
    <asp:Button ID="btnItem" runat="server" OnClick="CheckItem" />
    <asp:Button ID="btnSerial1" runat="server" OnClick="CheckSerial" />
    <asp:Button ID="btnQty" runat="server" OnClick="CheckQty" />
</div>
</asp:Content>
