<%@ Page Title="Inventory Cancelation" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="InventoryCancelation.aspx.cs" Inherits="FF.WebERPClient.Inventory_Module.InventoryCancelation"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function toggle(toggeldivid, toggeltext) {
            var divelement = document.getElementById(toggeldivid);
            var lbltext = document.getElementById(toggeltext);
            if (divelement.style.display == "block") {
                divelement.style.display = "none";
                lbltext.innerHTML = "+";
            }
            else {
                divelement.style.display = "block";
                lbltext.innerHTML = "-";
            }
        }
    </script>
    <style type="text/css">
        .onfocus
        {
            border-color:red;
        }
        .onblur
        {
            border-color:;
        }
    </style>
    <script type="text/javascript">

        function Change(obj, evt) {
            if (evt.type == "focus")
                obj.className = "onfocus";
            else if (evt.type == "blur")
                obj.className = "onblur";
        }

    </script>
    <script type="text/javascript">

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


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server" ID="updtExchange">
        <ContentTemplate>
            <%--Main Page--%>
            <div style="float: left; width: 100%; color: Black;">
                <%--Button Area--%>
                <div style="height: 22px; text-align: right;" class="PanelHeader">
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Height="85%" Width="70px"
                        CssClass="Button" OnClick="btnCancel_Click" />
                    <asp:Button ID="btnClose" runat="server" Text="Close" Height="85%" Width="70px" CssClass="Button" />
                </div>
                <%--Top Criteria--%>
                <div style="float: left; width: 100%; padding-top: 2px;">
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 20%;">
                        Date
                    </div>
                    <div style="float: left; width: 25%;">
                        &nbsp;<asp:TextBox ID="txtDate" runat="server" CssClass="TextBox" Width="50%" Enabled="false"></asp:TextBox>&nbsp;<asp:Image
                            ID="imgDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" ImageAlign="Middle" /></div>
                </div>
                <div style="float: left; width: 100%; padding-top: 2px;">
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 20%;">
                        Document Type
                    </div>
                    <div style="float: left; width: 25%;">
                        &nbsp;<asp:DropDownList ID="ddlDocType" runat="server" CssClass="ComboBox" Width="60%"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlDocType_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div style="float: left; width: 100%; padding-top: 2px;">
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 20%;">
                        Document No
                    </div>
                    <div style="float: left; width: 25%;">
                        &nbsp;<asp:DropDownList ID="ddlDocNo" runat="server" CssClass="ComboBox" Width="90%"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlDocNo_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div style="float: left; width: 100%; padding-top: 2px;">
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 20%;">
                        Manual Ref No
                    </div>
                    <div style="float: left; width: 25%;">
                        &nbsp;
                        <asp:Label ID="lblManualRefNo" runat="server" Text="----"></asp:Label>
                    </div>
                </div>
                <div style="float: left; width: 100%; padding-top: 2px;">
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 20%;">
                        Base Ref No
                    </div>
                    <div style="float: left; width: 25%;">
                        &nbsp;
                        <asp:Label ID="lblOtherDocNo" runat="server" Text="----"></asp:Label>
                    </div>
                </div>
                <div style="float: left; width: 100%; padding-top: 2px;">
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 20%;">
                        Other Location
                    </div>
                    <div style="float: left; width: 70%;">
                        &nbsp;
                        <asp:Label ID="lblOthLocCode" runat="server" Text="----"></asp:Label>
                        &nbsp;
                        <asp:Label ID="lblOthLocDesc" runat="server" Text="----"></asp:Label>
                    </div>
                </div>
                <div style="float: left; width: 100%; padding-top: 2px;">
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 20%;">
                        Customer/Supplier
                    </div>
                    <div style="float: left; width: 70%;">
                        &nbsp;
                        <asp:Label ID="lblCusCode" runat="server" Text="----"></asp:Label>
                        &nbsp;
                        <asp:Label ID="lblCusDesc" runat="server" Text="----"></asp:Label>
                    </div>
                </div>
                <div style="float: left; width: 100%; padding-top: 2px;">
                    <div style="float: left; width: 1%;">
                        &nbsp;</div>
                    <div style="float: left; width: 20%;">
                        Remarks
                    </div>
                    <div style="float: left; width: 70%;">
                        &nbsp;
                        <asp:Label ID="lblRemarks" runat="server" Text="----"></asp:Label>
                    </div>
                </div>
            </div>
            <div style="float: left; width: 59%; height: 75px;">
                <div style="float: left; width: 1%;">
                    &nbsp;</div>
                <div style="float: left; width: 98%;">
                    <div class="PanelHeader">
                        Item Details
                    </div>
                    <div style="width: 100%; float: left;">
                        <asp:Panel ID="pnlItem" runat="server" ScrollBars="Auto" Height="80px">
                            <asp:GridView runat="server" ID="gvItem" AutoGenerateColumns="False" CssClass="GridView"
                                CellPadding="2" ForeColor="#333333" GridLines="Both" ShowHeaderWhenEmpty="true">
                                <EmptyDataTemplate>
                                    <div style="width: 100%; text-align: center;">
                                        No data found
                                    </div>
                                </EmptyDataTemplate>
                                <AlternatingRowStyle BackColor="White" />
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
                                <Columns>
                                    <asp:BoundField DataField='Sad_itm_cd' HeaderText='Item' HeaderStyle-Width="100px"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField='Mi_longdesc' HeaderText='Description' HeaderStyle-Width="250px"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField='Mi_model' HeaderText='Model' HeaderStyle-Width="100px"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField='Sad_qty' HeaderText='Qty' HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField='Sad_unit_rt' HeaderText='Price' HeaderStyle-Width="150px"
                                        HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <%-- Control Area --%>
            <div style="display: none;">
                <%--<asp:Button ID="btnPopUp" runat="server" ClientIDMode="Static" />--%>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
