<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_HPReminder.ascx.cs"
    Inherits="FF.AbansTours.UserControls.uc_HPReminder" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="lnkbtnDummy"
    ClientIDMode="Static" PopupControlID="panEdit" BackgroundCssClass="modalBackground"
    CancelControlID="imgbtnClose" PopupDragHandleControlID="divpopHeader">
    
</asp:ModalPopupExtender>
<asp:Panel ID="panEdit" runat="server" Height="450px" Width="650px" CssClass="ModalWindow">
    <div class="popUpHeader" id="divpopHeader">
        <div style="float: left; width: 80%">
            Reminders</div>
        <div style="float: left; width: 20%; text-align: right">
            <asp:ImageButton ID="imgbtnClose" runat="server" ImageUrl="~/Images/0032-01-16x16x16.ICO" />&nbsp;
        </div>
    </div>
    <asp:Panel ID="Panel1" runat="server" GroupingText=" ">
        <div style="float: left; width: 100%;">
            <div style="height: 22px; text-align: right;" class="PanelHeader">
                <asp:Button ID="ButtonAccept" runat="server" Text="Accept" CssClass="Button" 
                    onclick="ButtonAccept_Click" />
            </div>
            <div style="float: left; height: 10%; color: Black;">
                &nbsp;
            </div>
            <asp:Panel ID="Panel13" runat="server" GroupingText=" ">
                <asp:GridView ID="GridViewReminder" runat="server" AutoGenerateColumns="False" DataKeyNames="hra_seq" GridLines="None">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chekPc" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="hra_rmd" ShowHeader=" False" />
                    </Columns>
                </asp:GridView>
            </asp:Panel>
        </div>
    </asp:Panel>
</asp:Panel>
 <asp:LinkButton ID="lnkbtnDummy" runat="server"></asp:LinkButton>
