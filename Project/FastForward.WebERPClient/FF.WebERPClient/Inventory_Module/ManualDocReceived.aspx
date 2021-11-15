<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ManualDocReceived.aspx.cs" Inherits="FF.WebERPClient.Inventory_Module.ManualDocReceived" %>

<%@ Register Src="~/UserControls/uc_MsgInfo.ascx" TagName="uc_MsgInfo" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../MainStyleSheet.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        function DeleteConfirm() {
            if (confirm("Are you sure that you want to remove this?")) {
                return true;
            }
            else {
                return false;
            }
        }

        function SaveConfirm() {
            if (confirm("Are you sure ?")) {
                return true;
            }
            else {
                return false;
            }
        }

    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="float: left; width: 100%">
        <asp:Panel ID="pnlHeader" runat="server" GroupingText="Manual Document Details" Font-Size="11px"
            Font-Names="Tahoma">
            <asp:UpdatePanel ID="pnlButtons" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlButton" runat="server" Direction="RightToLeft">
                        <asp:Button Text="Clear" ID="btnClear" runat="server" CssClass="Button" Width="75px" />
                        &nbsp;
                        <asp:Button Text="Confirm" ID="btnConfirm" runat="server" CssClass="Button" Width="75px" OnClientClick="return SaveConfirm()"
                            OnClick="btnConfirm_Click" />
                        &nbsp;
                        <asp:Button Text="Transfer" ID="btnTrans" runat="server" CssClass="Button" Width="75px" OnClientClick="return SaveConfirm()"
                            OnClick="btnTrans_Click" />
                        &nbsp;
                        <asp:Button Text="Print" ID="btnPrint" runat="server" CssClass="Button" Width="75px" />
                    </asp:Panel>
                    <div style="float: left; width: 3%">
                        &nbsp;
                    </div>
                    <div style="float: left; width: 94%">
                        <asp:UpdatePanel ID="pnlHead" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Panel ID="pnl_header" runat="server" GroupingText="" Font-Size="11px" Font-Names="Tahoma">
                                    <div style="float: left; height: 23px; width: 100%;">
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 17%;">
                                            Document Ref #. . . . . .
                                        </div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 35%;">
                                            <asp:DropDownList ID="ddlRef" runat="server" Width="200px" AppendDataBoundItems="true"
                                                OnSelectedIndexChanged="ddlRef_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 12%;">
                                            Date of Issue. . . . . .
                                        </div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 23%;">
                                            <asp:TextBox ID="txtDate" runat="server" Width="190px" CssClass="TextBox"></asp:TextBox>
                                        </div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                    </div>
                                    <div style="float: left; height: 23px; width: 100%;">
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 17%;">
                                            Issue Location. . . . . .</div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 35%;">
                                            <asp:TextBox ID="txtIssLoc" runat="server" Width="190px" CssClass="TextBox"></asp:TextBox>
                                        </div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 12%;">
                                            Remarks. . . . . .</div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 23%;">
                                            <asp:TextBox ID="txtRem" runat="server" Width="190px" CssClass="TextBox"></asp:TextBox>
                                        </div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;
                                        </div>
                                    </div>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel ID="pnlManualDocDet" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:GridView ID="gvManualDocDet" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="None" OnRowCommand="gvManualDocDet_RowCommand"
                                    CssClass="GridView" ShowHeaderWhenEmpty="True">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <%--                                        <asp:BoundField DataField="MDD_LINE" HeaderText="">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="0px" />
                                        </asp:BoundField>--%>
                                        <asp:BoundField DataField="MDD_ITM_CD" HeaderText="Item Code">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MDD_PREFIX" HeaderText="Prefix">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MDD_BK_NO" HeaderText="Book No">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MDD_BK_TP" HeaderText="Book Type">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MDD_FIRST" HeaderText="First Page">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MDD_LAST" HeaderText="Last Page">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MDD_CNT" HeaderText="Page Count">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MDD_ISSUE_BY" HeaderText="Issued By">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MDD_USER" HeaderText="">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="1px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtndelAllSerial" runat="server" ImageUrl="~/Images/Delete.png"
                                                    CommandName="DeleteItem" OnClientClick="return DeleteConfirm()" CommandArgument='<%# Eval("MDD_BK_NO") + "|" + Eval("MDD_USER")  + "|" + Eval("MDD_PREFIX")  %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="75px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#003366" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div style="float: left; width: 3%">
                        &nbsp;
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
    <div style="float: left; width: 3%">
        &nbsp;
    </div>
    <div style="float: left; width: 94%">
        <asp:UpdatePanel ID="pnlFooter" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="Pnl_foot" runat="server" GroupingText="" Font-Size="11px" Font-Names="Tahoma">
                    <div style="float: left; height: 23px; width: 100%;">
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 17%;">
                            Date of receive. . . . . .
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 35%;">
                           <asp:TextBox ID="txtRecDate" runat="server" Width="190px" CssClass="TextBox" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 12%;">
                            <asp:CheckBox ID="chkTrans" runat="server" Text="Transfer Location" OnCheckedChanged="chkTrans_CheckedChanged" AutoPostBack="true"/>
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                        <div style="float: left; width: 23%;">
                            <asp:TextBox ID="txtTransLoc" runat="server" CssClass="TextBox" Width="175" ClientIDMode="Static"></asp:TextBox>
                            <asp:ImageButton ID="imgbtnTLoc" runat="server" ImageUrl="~/Images/icon_search.png" OnClick="imgbtnTLoc_Click" />
                        </div>
                        <div style="float: left; width: 1%;">
                            &nbsp;</div>
                    </div>
                    
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 130px">
                            
                        </td>
                        <td style="width: 100px">
                            
                        </td>
                        <td>
                            
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
    <div style="float: left; width: 3%">
        &nbsp;
    </div>
</asp:Content>
