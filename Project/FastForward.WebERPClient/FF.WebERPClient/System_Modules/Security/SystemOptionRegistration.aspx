<%@ Page Title="System Option Registration" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="SystemOptionRegistration.aspx.cs" Inherits="FF.WebERPClient.System_Modules.Security.SystemOptionRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script language="javascript" type="text/javascript">

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <div id="divMain" style="color:Black;">
        <div style="float: left; width: 2%;">
            &nbsp;</div>
        <div style="float: left; width: 47%; color:Navy; id="divTree">
            <asp:Panel ID="pnlTree" runat="server" GroupingText="All System Options">
                <asp:Panel ID="pnlTreeContainer" runat="server" ScrollBars="Auto" Height="350px" ForeColor="Black" >
                <asp:TreeView ID="treeView" runat ="server" ForeColor ="Black"  
                        ImageSet="Arrows" OnSelectedNodeChanged ="treeView_Click" SelectedNodeStyle-BackColor="LightSkyBlue" SelectedNodeStyle-ForeColor="Black"  >
                       
                </asp:TreeView>
                </asp:Panel>
            </asp:Panel>
        </div>
        <div style="float: left; width: 2%;">
            &nbsp;</div>
        <div style="float: left; width: 47%;" id="divEntry">
            <asp:Panel ID="pnlEntry" runat="server" GroupingText="Add/Edit System Option" >
                <div style="float: none;" id="divButton">
                    <asp:Panel ID="pnlButton" runat="server" Direction="RightToLeft">
                        &nbsp;
                        <asp:Button Text="Cancel" ID="btnCancel" runat="server" CssClass="Button" OnClick ="btnCancel_Click" Enabled="False" />
                        &nbsp;
                        <asp:Button Text="Update" ID="btnUpdate" runat="server" CssClass="Button" OnClick ="btnUpdate_Click" />
                        &nbsp;
                        <asp:Button Text="Add New" ID="btnAddNew" runat="server" CssClass="Button" OnClick ="btnAddNew_Click" />
                    </asp:Panel>
                </div>
                <div >

                <div style="float:left; height:23px; width:100%;"> <div style="float:left; width:1%;">&nbsp;</div><div dir="rtl" style="float:left; width:17%;">Title</div><div style="float:left; width:1%;">&nbsp;</div><div style="float:left; width:80%;"><asp:TextBox ID="txtTitle" runat ="server"  CssClass="TextBox" Width="100%" ></asp:TextBox></div><div style="float:left; width:1%;">&nbsp;</div> </div>
                <div style="float:left; height:42px; width:100%;"> <div style="float:left; width:1%;">&nbsp;</div><div dir="rtl" style="float:left; width:17%;">Description</div><div style="float:left; width:1%;">&nbsp;</div><div style="float:left; width:80%;"><asp:TextBox ID="txtDescription" runat ="server"  CssClass="TextBox" Width="100%" Wrap ="true" Height="36px"></asp:TextBox></div><div style="float:left; width:1%;">&nbsp;</div> </div>
                <div style="float:left; height:22px; width:100%;"> <div style="float:left; width:1%;">&nbsp;</div><div dir="rtl" style="float:left; width:17%;">Url</div><div style="float:left; width:1%;">&nbsp;</div><div style="float:left; width:80%;"><asp:TextBox ID="txtUrl" runat ="server"  CssClass="TextBox" Width="100%"></asp:TextBox></div><div style="float:left; width:1%;">&nbsp;</div> </div>
                <div style="float:left; height:22px; width:100%;"> <div style="float:left; width:1%;">&nbsp;</div><div dir="rtl" style="float:left; width:17%;">Parent ID</div><div style="float:left; width:1%;">&nbsp;</div><div style="float:left; width:24%;"><asp:TextBox ID="txtParentID" ReadOnly ="true" runat ="server"  CssClass="TextBox" Width="100%"></asp:TextBox></div><div style="float:left; width:1%;">&nbsp;&nbsp;</div>    <div dir="rtl" style="float:left; width:26%;">Ordinal Position</div><div style="float:left; width:1%;">&nbsp;</div><div style="float:left; width:28%;"><asp:TextBox ID="txtOrdinalPosition" runat ="server"  CssClass="TextBox" Width="100%"></asp:TextBox></div><div style="float:left; width:1%;">&nbsp;</div></div>
                <div style="float:left; height:22px; width:100%;"> <div style="float:left; width:1%;">&nbsp;</div><div dir="rtl" style="float:left; width:17%;">Enable</div><div style="float:left; width:1%;">&nbsp;</div><div style="float:left; width:12%;"><asp:CheckBox ID="chkIsEnable" runat ="server"   Width="100%"></asp:CheckBox></div><div style="float:left; width:1%;">&nbsp;</div>   <div dir="rtl" style="float:left; width:17%;">Hide</div><div style="float:left; width:1%;">&nbsp;</div><div style="float:left; width:12%;"><asp:CheckBox ID="chkIsHide" runat ="server"   Width="100%"></asp:CheckBox></div><div style="float:left; width:1%;">&nbsp;</div>         <div dir="rtl" style="float:left; width:17%;">Active</div><div style="float:left; width:1%;">&nbsp;</div><div style="float:left; width:12%;"><asp:CheckBox ID="chkIsActive" runat ="server"   Width="100%"></asp:CheckBox></div><div style="float:left; width:1%;">&nbsp;</div>       </div>

                </div>
                
            </asp:Panel>
        </div>
        <div style="float: left; width: 2%;">
            &nbsp;</div>
    </div>
         </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
