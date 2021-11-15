<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="InvoicePrint.aspx.cs" Inherits="FF.AbansTours.InvoicePrint" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true"  >
    <Triggers>
           <asp:AsyncPostBackTrigger ControlID="SuperBtn" EventName="Click" />
           <asp:PostBackTrigger ControlID="CrystalReportViewer1"  />
         </Triggers>
        <ContentTemplate>--%>

         <table>
            <tr>
                <td>
                   
        <asp:Label ID="Label8" runat="server" Text="Invoice No"></asp:Label>
        <asp:TextBox ID="txtInoviceNo" runat="server"></asp:TextBox>
        <asp:Button class="btn btn-primary" runat="server" ID="SuperBtn" type="submit" Text="Print View using WebPage"
            Width="189px" Height="32px" BackColor="#6699FF" Visible="true" OnClick="SuperBtn_Click" />
  
                </td>
            </tr>
            <tr>
                <td>
                    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
        EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" ToolPanelView="None" />
                       
                </td>
            </tr>
        </table>
  
   <%-- </ContentTemplate>
        </asp:UpdatePanel>--%>
</asp:Content>
