<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SerSearch.ascx.cs" Inherits="FastForward.SCMWeb.UserControls.SerSearch" %>

 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>  
 
     <script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
      <script  src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>
<link href="<%=Request.ApplicationPath%>Css/bootstrap.css" rel="stylesheet" />
<link href="<%=Request.ApplicationPath%>Css/style.css" rel="stylesheet" />

 

     <div class="panel panel-default">
            <div class="panel-body">
        <div class="row">       
            <div class="col-sm-12">
                <div class="col-sm-2">
        <asp:DropDownList ID="cmbSerialType" runat="server" Height="22px" Width="136px">
                            <asp:ListItem>Serial 1</asp:ListItem>
                            <asp:ListItem>Serial 2</asp:ListItem>
                            <asp:ListItem>Serial 3</asp:ListItem>
                            <asp:ListItem>Serial 4</asp:ListItem>
                        </asp:DropDownList>
               
                &nbsp;</div>
                
                <div class="col-sm-4">
                    
                   <asp:TextBox ID="txtSerialNo" runat="server"  placeholder="Enter Serial"></asp:TextBox> 
                    
              
                   
                    
                 
                    <asp:LinkButton ID="LinkButton1" runat="server"  >
                        <span class="glyphicon glyphicon-search " aria-hidden="true"></span>
                    </asp:LinkButton>
                   
                    
                        
                
                        <asp:Label ID="Label1" runat="server" Text="Item"></asp:Label>
                        <asp:Label ID="lblItem" runat="server"></asp:Label> 

                   
              

                        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">LinkButton</asp:LinkButton>

                   
              

                        </div>
              
                        
                      <!--  </div>-->       
                
                <div class="col-sm-5">

                    <asp:Label ID="Label2" runat="server" Text="Advance Search" BackColor="Purple" ForeColor="White"></asp:Label>
                    <asp:Label ID="Label3" runat="server" Text="Character Case"></asp:Label>
                             
                    <asp:DropDownList ID="cmbCaseType" runat="server">
                        <asp:ListItem>Normal</asp:ListItem>
                        <asp:ListItem>Upper</asp:ListItem>
                        <asp:ListItem>Lower</asp:ListItem>
                    </asp:DropDownList>
                            
                    <asp:CheckBox ID="chkWholeWord" runat="server" Text="Match Whole World" Checked="True" />
                       
                    
                </div>     
                <div>
                     <asp:LinkButton ID="Clear" runat="server"   >
                        <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear
                     </asp:LinkButton>
                </div>           
                

                </div>
             </div>
                </div>
          
</div>
        
<div runat="server" style="width: 427px">
    <asp:Button ID="Button3" runat="server" Text="" Style="display: none;" />
</div>


<asp:ModalPopupExtender ID="UserPopoup1" runat="server" Enabled="True" TargetControlID="Button3"
    PopupControlID="testPanel" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
</asp:ModalPopupExtender>

<asp:Panel runat="server" ID="testPanel" DefaultButton="ImgSearch">

     <div runat="server" id="test" class="panel panel-primary Mheight">

        <asp:Label ID="Label8" runat="server" Text="Label" Visible="false"></asp:Label>


        <div class="panel panel-default">
            <div class="panel-heading">
                <asp:LinkButton ID="btnClose" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                </asp:LinkButton>
                <%--<span>Commen Search</span>--%>
                <div class="col-sm-11">
                </div>
                <div class="col-sm-1">
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-sm-12">
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12 height5">
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="col-sm-2 labelText1">
                            Search by key
                        </div>

                        <div class="col-sm-3 paddingRight5">
                            <asp:DropDownList ID="cmbSearchbykey" runat="server" class="form-control">
                            </asp:DropDownList>
                        </div>

                        <div class="col-sm-2 labelText1">
                            Search by word
                        </div>


                        <%--onkeydown="return (event.keyCode!=13);"--%>
                        <div class="col-sm-4 paddingRight5">
                            <asp:TextBox ID="txtSearchbyword" placeholder="Search by word"  CausesValidation="false" class="form-control" AutoPostBack="true" runat="server" ></asp:TextBox>
                        </div>

                        <asp:ScriptManagerProxy ID="ScriptManagerProxy9" runat="server"></asp:ScriptManagerProxy>
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <div class="col-sm-1 paddingLeft0">
                                    <asp:LinkButton ID="ImgSearch" runat="server" >
                                     <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                    </asp:LinkButton>
                                </div>
                            </ContentTemplate>

                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txtSearchbyword" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12 height5">
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12 height5">
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">

                        <asp:GridView ID="dvResult" CausesValidation="false" runat="server"  AllowPaging="True"  GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager">
                            <Columns>
                                <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10" />                                                             
                            </Columns>
                            <HeaderStyle Width="10px" />
                        </asp:GridView>

                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Panel>