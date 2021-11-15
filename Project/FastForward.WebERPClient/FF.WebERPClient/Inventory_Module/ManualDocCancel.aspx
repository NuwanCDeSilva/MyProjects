<%@ Page Title="Manual Document Cancellation" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="ManualDocCancel.aspx.cs" Inherits="FF.WebERPClient.Inventory_Module.ManualDocCancel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">

        function showMessage() {
            var des = confirm("Are you sure?");
            document.getElementById('<%=HiddenFieldSaveButon.ClientID%>').value = des;
            
        } 

    </script>
    <link href="../../MainStyleSheet.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel ID="updtMainPnl" runat="server">
        <ContentTemplate>
            <%--Whole Page--%>
            <asp:HiddenField ID="HiddenFieldSaveButon" runat="server" Value="false" />
            <div style="float: left; width: 100%; color: Black;">
                <div style="height: 22px; text-align: right;" class="PanelHeader">
                    <asp:Button ID="ButtonSave" runat="server" Text="Cancel Document" CssClass="Button"
                        OnClick="ButtonSave_Click" />
                    
                    <asp:Button ID="ButtonClear" runat="server" Text="Clear" CssClass="Button" OnClick="ButtonClear_Click" />
                    <asp:Button ID="ButtonClose" runat="server" Text="Close" CssClass="Button" OnClick="ButtonClose_Click" />
                </div>
                <div style="float: left; width: 100%; color: Black;">
                    &nbsp;
                </div>
                <div class="PanelHeader">
                    Document Details
                </div>
                <div style="float: left; width: 100%;">
                    <div style="float: left; width: 100%;">
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 30%;">
                            Select Document
                        </div>
                    </div>
                    <div style="float: left; width: 100%;">
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 95%;">
                            <asp:DropDownList ID="DropDownListDocumentType" runat="server" AppendDataBoundItems="true"
                                AutoPostBack="true" CssClass="ComboBox"  OnSelectedIndexChanged="DropDownListDocumentType_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Label ID="LabelDocumentDescription" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
                <div>
                    &nbsp;
                </div>
                <div style="float: left; width: 100%;">
                    <asp:GridView ID="GridViewDocuments" runat="server" Width="99%" EmptyDataText="No data found"
                        ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                        GridLines="Both" CssClass="GridView" OnSelectedIndexChanging="GridViewDocuments_SelectedIndexChanging">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField HeaderText="Book Number" DataField="Book_Number" />
                            <asp:BoundField HeaderText="Prefix" DataField="mdd_prefix" />
                            <asp:BoundField HeaderText="Received Date" DataField="Recieve_Date" DataFormatString='<%$ appSettings:FormatToDate %>'
                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField HeaderText="Current Number" DataField="Current_Number" HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField HeaderText="Last Number" DataField="Last_Number" HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField HeaderText="No of  Receipts" DataField="Number_Range" HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-HorizontalAlign="Right" />
                            <asp:TemplateField HeaderText="Pick">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgSelect" runat="server" CommandName="SELECT" ImageUrl="~/Images/download_arrow_icon.png"
                                        Height="15px" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="20%" Height="15px" />
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
                </div>
                <div>
                    &nbsp;
                </div>
                <div id="DivRecieptRange" runat="server" visible="false" style="float: left; width: 100%;">
                    <div style="float: left; width: 100%;">
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 45%;">
                            Start Number
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 45%;">
                            End Number
                        </div>
                        <div style="float: left; width: 100%;">
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 45%;">
                                <asp:TextBox ID="TextBoxRecieptStart" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                            <div style="float: left; width: 1%;">
                                &nbsp;</div>
                            <div style="float: left; width: 45%;">
                                <asp:TextBox ID="TextBoxRecieptEnd" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div style="float: left; text-align: right; width: 85%;">
                        <asp:Button ID="ButtonAdd" runat="server" Text="Add" CssClass="Button" OnClick="ButtonAdd_Click" />
                    </div>
                    <div style="float: left; text-align: right; width: 85%;">
                        &nbsp;
                    </div>
                </div>
                <div class="PanelHeader">
                    Documents
                </div>
                <div style="float: left; width: 100%;">
                    <asp:GridView ID="GridViewFinalDocuments" runat="server" Width="99%" EmptyDataText="No data found"
                        ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                        GridLines="Both" CssClass="GridView" OnRowDeleting="GridViewFinalDocuments_RowDeleting">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField HeaderText="Book Number" DataField="BOOKNO" />
                            <asp:BoundField HeaderText="Prefix" DataField="PREFIX" />
                            <asp:BoundField HeaderText="Start Number" DataField="START" />
                            <asp:BoundField HeaderText="End Number" DataField="END" />
                            <asp:TemplateField HeaderText="Remove">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgDelete" runat="server" CommandName="DELETE" ImageUrl="~/Images/Delete.png"
                                        Height="15px" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="20%" Height="15px" />
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
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
