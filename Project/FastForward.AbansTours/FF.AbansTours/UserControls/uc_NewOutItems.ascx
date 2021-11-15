<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_NewOutItems.ascx.cs" Inherits="FF.AbansTours.UserControls.uc_NewOutItems" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%--<link href="../Styles/Site.css" rel="stylesheet" type="text/css" />--%>
<%--<link href="../js/jquery-ui-1.9.1.custom.css" rel="stylesheet" type="text/css" />--%>

<style type="text/css">
    .Overlay { 
  position:fixed; 
  top:0px; 
  bottom:0px; 
  left:0px; 
  right:0px; 
  overflow:hidden; 
  padding:0;  
  margin:0; 
  background-color:#000;  
  filter:alpha(opacity=50); 
  opacity:0.5; 
  z-index:1000;
}
 
.PopUpPanel {  
  position:absolute;
  background-color: #FFFFFF;   
  top:682px;
  left:25%;
  z-index:2001; 
  padding:30px;
  min-width:400px;
  max-width:700px;
  height:342px;
    
  -moz-box-shadow:3.5px 4px 5px #000000;
  -webkit-box-shadow:3.5px 4px 5px #000000;
  box-shadow:3.5px 4px 5px #000000;

  border-radius:5px;
  -moz-border-radiux:5px;
  -webkit-border-radiux:5px;

 border: 1px solid #CCCCCC;
}
    .style1
    {
        width: 106%;
    }
    .style4
    {
        width: 154px;
        height: 29px;
    }
    .style5
    {
        height: 29px;
    }
    .style6
    {
    }
</style>


  <%--   javascript --%>
   <%-- <script type="text/javascript">

        //-------------------------------------GetItemData()------------------------------------------------------//
        function GetItemData() {
            var itemCode = document.getElementById('<%=txtItemCode.ClientID%>').value;
            itemCode = itemCode.toUpperCase();
            if (itemCode == "") {

                alert("ItemCode cannot be empty...");

            }
        }



    </script>--%>

      <script type="text/javascript">


          function clearControls() {
              //alert("ok");
              document.getElementById('MainContent_uc_NewOutItems1_ddlStatus').selectedIndex = 0;

            }



    </script>



<asp:Panel ID="Panel1" runat="server" Width="100%">
    <div id = "OuterGrid" runat="server" class="Overlay" style="display: none "></div>
    <div id="InnerGird" runat="server" class="PopUpPanel" 
        style="display:none; overflow: auto; width: 383px;" >

        <div style="position: absolute; top: 7px; right: 5px">
            <asp:Button ID="btnExit" runat="server" Text="X" onclick="Exit_Click" CssClass="btn-round" BackColor="#43A1DA" ForeColor="White"/>
        </div>
        <h5>
            Add New Out Items
        </h5>
        <hr />
        <br />



                    <table class="style1">
                <tr>
                    <td class="style4">
                        Item Code:</td>
                    <td class="style5">
                        <asp:TextBox ID="txtItemCode" OnChange="clearControls();" ForeColor="#0000ff" CssClass="input-large" Width="135px" runat="server" 
                AutoPostBack="True" ontextchanged="txtItemCode_TextChanged"></asp:TextBox>
                        
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ErrorMessage="Item Code cannot be Empty" ControlToValidate="txtItemCode" 
                        ForeColor="#CC3300" SetFocusOnError="True" ValidationGroup="AA">*</asp:RequiredFieldValidator>

                    <asp:ImageButton ID="SearchItems" ImageUrl="~/images/icon_search.png" 
                        runat="server" onclick="SearchItems_Click" />

                        </td>
                </tr>
                <tr>
                    <td class="style4">
                        Status:</td>
                    <td>
                       
                       <asp:DropDownList ID="ddlStatus" 
                        Font-Size="Small" 
                        ForeColor="#000099" AutoPostBack="true" runat="server"
                        onselectedindexchanged="ddlStatus_SelectedIndexChanged" Width="140px">
                    </asp:DropDownList>

                       </td>
                </tr>
                <tr>
                    <td class="style4">
                        Serial No:</td>
                    <td>
                        <asp:DropDownList ID="ddlSerial" Font-Size="Small" 
                        ForeColor="#000099" Width="140px" AutoPostBack="false" runat="server">
                    </asp:DropDownList>

                        </td>
                </tr>
                <tr>
                    <td class="style4">
                        Account Create Scheme:</td>
                    <td>
                         <asp:DropDownList ID="ddlAccScm" Font-Size="Small" 
                        ForeColor="#000099" Width="140px" AutoPostBack="false" runat="server">
                    </asp:DropDownList>
                       
                         <asp:RadioButton ID="rdoAccScm" GroupName="Toggle" AutoPostBack="false" runat="server"/>
                       
                       </td>
                </tr>
                <tr>
                    <td class="style4">
                        Current Scheme:</td>
                    <td>
                         <asp:DropDownList ID="ddlCurrScm" Font-Size="Small" 
                        ForeColor="#000099" Width="140px" AutoPostBack="false" runat="server">
                    </asp:DropDownList>
                    
                         <asp:RadioButton ID="rdoCurrScm" AutoPostBack="false"
                             runat="server" GroupName="Toggle"/>
                    
                    </td>
                </tr>
                <tr>
                    <td class="style6">
                        Price:</td>
                    <td>
                    <asp:TextBox ID="txtPrice" CssClass="input-large" Width="135px"
                     runat="server"></asp:TextBox>
                       
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="Required Price" ControlToValidate="txtPrice" 
                        ForeColor="#CC3300" SetFocusOnError="True" ValidationGroup="AA">*</asp:RequiredFieldValidator>


                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
                        ControlToValidate="txtPrice" ValidationExpression="[0-9]*\.?[0-9]+" 
                        runat="server" ErrorMessage="Invalid Format" ValidationGroup="AA" 
                        ForeColor="#CC0000">*</asp:RegularExpressionValidator>

                       </td>
                </tr>
                <tr>
                    <td class="style6">
                        &nbsp;</td>
                    <td>
                             <div>

                             <asp:Button ID="btnAdd" 
                             CssClass="btn-round" BackColor="#43A1DA" ForeColor="White" Width="70px"
                
                            runat="server" Text="Add" onclick="btnAdd_Click" ValidationGroup="AA" />


                             <asp:Button ID="btnClear" Width="70px"
                              CssClass="btn-round" BackColor="#43A1DA" ForeColor="White" 
                              runat="server" Text="Clear" onclick="btnClear_Click" />

          

                          </div>
                        
                        </td>
                </tr>
                <tr>
                    <td class="style6" colspan="2">
                       
                        
                          <asp:ValidationSummary ID="ValidationSummary2" ForeColor="Red" 
                          DisplayMode="BulletList" ValidationGroup="AA" runat="server" Height="48px" 
                              Width="253px" />


                                <asp:Label ID="lblmsg" Font-Bold="true" Visible = "false" ForeColor="Red" runat="server" Text=""></asp:Label>


                        </td>
                </tr>
            </table>



       <%-- <div>
            <asp:Label ID="Label1" CssClass="lable_box" runat="server" Text="Item Code"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtItemCode" CssClass="text_box" Width="140px" runat="server" 
                AutoPostBack="True" ontextchanged="txtItemCode_TextChanged"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="Required Item" ControlToValidate="txtItemCode" 
                ForeColor="#CC3300" SetFocusOnError="True" ValidationGroup="AA">*</asp:RequiredFieldValidator>

            <asp:ImageButton ID="SearchItems" ImageUrl="~/images/icon_search.png" 
                runat="server" onclick="SearchItems_Click" />


        </div>--%>

       <%-- <div>--%>
      <%--   <asp:Label ID="Label2" CssClass="lable_box" runat="server" Text="Status"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;<asp:DropDownList ID="ddlStatus" 
                Font-Size="Small" 
                ForeColor="#000099" Width="130px" AutoPostBack="true" runat="server" 
                Font-Names="Times New Roman" 
                onselectedindexchanged="ddlStatus_SelectedIndexChanged">
            </asp:DropDownList>--%>
        
       <%--  <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlStatus"
            ErrorMessage="Select a Status" Operator="GreaterThan" SetFocusOnError="True"
            Type="Integer" ValidationGroup="AA" ValueToCompare="-1" ForeColor="#CC0000">*</asp:CompareValidator>
--%>


        <%--</div>--%>

        <%--<div style="margin-top:8px;">
         <asp:Label ID="Label3" CssClass="lable_box" runat="server" Text="Serial no"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; <asp:DropDownList ID="ddlSerial" Font-Size="Small" 
                ForeColor="#000099" Width="130px" AutoPostBack="false" runat="server">
            </asp:DropDownList>
        </div>--%>

     <%--   <div style="margin-top:8px;">
            <asp:Label ID="Label4" CssClass="lable_box" runat="server" Text="Acc Create Scheme"></asp:Label>
            &nbsp;&nbsp;
            <asp:TextBox ID="txtscheme" CssClass="text_box" Width="140px" runat="server"></asp:TextBox>
        </div>--%>

       <%-- <div style="margin-top:8px;">
            <asp:Label ID="Label6" CssClass="lable_box" runat="server" Text="Current Scheme"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
            <asp:TextBox ID="TextBox1" CssClass="text_box" Width="140px" runat="server"></asp:TextBox>
        </div>--%>

     <%--   <div>
            <asp:Label ID="Label5" CssClass="lable_box" runat="server" Text="Price"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
            <asp:TextBox ID="txtPrice" CssClass="text_box" Width="140px" runat="server"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ErrorMessage="Required Price" ControlToValidate="txtPrice" 
                ForeColor="#CC3300" SetFocusOnError="True" ValidationGroup="AA">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
                ControlToValidate="txtPrice" ValidationExpression="[0-9]*\.?[0-9]+" 
                runat="server" ErrorMessage="Invalid Format" ValidationGroup="AA" 
                ForeColor="#CC0000">*</asp:RegularExpressionValidator>

        </div>--%>
     <%--   <div style="padding-left:118px">

            <asp:Button ID="btnAdd" 
              CssClass="btn-round" BackColor="#43A1DA" ForeColor="White" Width="70px"
                
                runat="server" Text="Add" OnClientClick="GetItemData()" onclick="btnAdd_Click" ValidationGroup="AA" />


              <asp:Button ID="btnClear" Width="70px"
               CssClass="btn-round" BackColor="#43A1DA" ForeColor="White" 
                runat="server" Text="Clear" onclick="btnClear_Click" />

          

        </div>--%>
        <%--<div>
        
         
        
            <asp:ValidationSummary ID="ValidationSummary1" ForeColor="Red" DisplayMode="BulletList" ValidationGroup="AA" runat="server" />
        
        </div>--%>

       
     <%--    <div style="float: left; width: 99%;">
                  
         

         </div>--%>

        <%--<div style="margin-top:7px">
            <asp:Label ID="lblmsg" Font-Bold="true" Visible = "false" ForeColor="Red" runat="server" Text=""></asp:Label>
        </div>--%>



   </div>
    </asp:Panel>

