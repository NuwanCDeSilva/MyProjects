<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="RemSummaryAdjesment.aspx.cs" Inherits="FF.WebERPClient.Financial_Modules.RemSummaryAdjesment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
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
        function LinkClick(sender, args) {
            var i = sender.ID
            document.getElementById('<%= LinkButtonView.ClientID %>').click()
        }

    </script>
    <style>
        .Panel legend
        {
            color: Blue;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>
            <div style="height: 22px; text-align: right;" class="PanelHeader">
                <asp:Button ID="ButtonSave" runat="server" Text="Save" CssClass="Button" OnClick="ButtonSave_Click" />
                <asp:Button ID="ButtonClear" runat="server" Text="Clear" CssClass="Button" OnClick="ButtonClear_Click" />
                <asp:Button ID="ButtonClose" runat="server" Text="Close" CssClass="Button" OnClick="ButtonClose_Click" />
            </div>
            <div style="float: left; height: 10%; color: Black;">
                &nbsp;
            </div>

            <div style="float: left; width: 100%; height: 10px; color: Black;">
                &nbsp;
            </div>
            <asp:Panel ID="Panel2" runat="server" GroupingText=" " CssClass="Panel">
                <div style="float: left; width: 100%;">
                    <div style="float: left; width: 1%; color: Black;">
                        &nbsp;
                    </div>
                    <div style="float: left; width: 60%;">
                        <div style="float: left; width: 23%;">
                            Remitance Date
                        </div>
                        <div style="float: left; width: 60%;">
                            <asp:TextBox ID="TextBoxRemitanceDate" runat="server" CssClass="TextBox TextBoxUpper"
                                Enabled="False" Width="108px"></asp:TextBox>
                            <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="TextBoxRemitanceDate"
                                PopupButtonID="Image3" Enabled="True" Format="dd/MMM/yyyy" OnClientDateSelectionChanged="LinkClick">
                            </asp:CalendarExtender>
                            &nbsp;&nbsp;
                            <asp:LinkButton ID="LinkButtonView" runat="server" OnClick="LinkButtonView_Click"></asp:LinkButton>
                        </div>
                    </div>
                    <div style="float: left; width: 30%;" runat="server" id="DivViewAmo" visible="false">
                        <div style="float: left; width: 50%; color: Red; font-weight: bolder; font-size: 13px;">
                           Pre. Day Excess Amount
                        </div>
                        <div style="float: left; width: 50%;">
                            <asp:Label ID="LabelExcess" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                </div>
                <div style="float: left; width: 100%; padding-top: 4px;">
                    <div style="float: left; width: 1%; color: Black;">
                        &nbsp;
                    </div>
                    <div style="float: left; width: 70%;">
                        <div style="float: left; width: 20%;">
                            Section
                        </div>
                        <div style="float: left; width: 69%;">
                            <asp:DropDownList ID="DropDownListSection" runat="server" CssClass="ComboBox" Width="220px"
                                AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DropDownListSection_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div style="float: left; width: 100%; padding-top: 4px;">
                    <div style="float: left; width: 1%; color: Black;">
                        &nbsp;
                    </div>
                    <div style="float: left; width: 70%;">
                        <div style="float: left; width: 20%;">
                            RemitType
                        </div>
                        <div style="float: left; width: 69%;">
                            <asp:DropDownList ID="DropDownListRemitType" runat="server" CssClass="ComboBox" Width="220px"
                                AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DropDownListRemitType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div style="float: left; width: 100%; padding-top: 4px;">
                    <div style="float: left; width: 1%; color: Black;">
                        &nbsp;
                    </div>
                    <div style="float: left; width: 94%;">
                        <div style="float: left; width: 50%;">
                            <div style="float: left; width: 30%;">
                                Original Amount
                            </div>
                            <div style="float: left; width: 69%;">
                                <asp:TextBox ID="TextBoxOriginalAmount" runat="server" CssClass="TextBox" Enabled="False"
                                    onKeyPress="return numbersonly(event,true)"></asp:TextBox>
                            </div>
                        </div>
                        <div style="float: left; width: 49%;">
                            <div style="float: left; width: 30%;">
                                Final Amount
                            </div>
                            <div style="float: left; width: 69%;">
                                <asp:TextBox ID="TextBoxFinalAmount" runat="server" CssClass="TextBox" onKeyPress="return numbersonly(event,true)"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div style="float: left; width: 100%; padding-top: 4px;">
                    <div style="float: left; width: 1%; color: Black;">
                        &nbsp;
                    </div>
                    <div style="float: left; width: 94%;">
                        <div style="float: left; width: 50%;">
                            <div style="float: left; width: 30%;">
                                Original Comment
                            </div>
                            <div style="float: left; width: 69%;">
                                <asp:TextBox ID="TextBoxOriginalComment" runat="server" TextMode="MultiLine" Rows="4"
                                    CssClass="TextBox" Width="270px" Enabled="False"></asp:TextBox>
                            </div>
                        </div>
                        <div style="float: left; width: 49%;">
                            <div style="float: left; width: 30%;">
                                Final Comment
                            </div>
                            <div style="float: left; width: 69%;">
                                <asp:TextBox ID="TextBoxFinalComment" runat="server" TextMode="MultiLine" Rows="4"
                                    CssClass="TextBox" Width="270px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <div style="float: left; width: 100%; height: 10px; color: Black;">
                &nbsp;
            </div>
            <asp:Panel ID="Panel3" runat="server" GroupingText=" " CssClass="Panel">
                <div style="float: left; width: 100%; color: Black;">
                    <asp:GridView ID="gvRemLimit" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" CssClass="GridView" EmptyDataText="No data found" ShowHeaderWhenEmpty="True"
                        Width="100%" PageSize="20">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="Section">
                           
                                <ItemTemplate>
                                    <asp:Label ID="lblSec" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "REM_SEC") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Width="35px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemCd" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Rem_cd") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Width="35px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblDesc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RemSumDet.RSD_DESC") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Width="300px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date" >
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "REM_DT","{0:dd/MMM/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Original Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblCre" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Rem_val") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="200px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Final Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblFinAm" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Rem_val_final") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="150px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Original Remarks">
                                <ItemTemplate>
                                    <asp:Label ID="lblRem" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Rem_rmk") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Width="300px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Final Remarks">
                                <ItemTemplate>
                                    <asp:Label ID="lblFinalRem" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Rem_rmk_fin") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Width="300px" />
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerSettings Mode="NextPreviousFirstLast" />
                        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
                </div>
            </asp:Panel>
            <div style="float: left; width: 100%; height: 10px; color: Black;">
                &nbsp;
            </div>
            <asp:Panel ID="Panel4" runat="server" GroupingText=" " CssClass="Panel">
                <div style="float: left; width: 100%; color: Black;">
                    <div style="float: left; width: 60%; color: Black;">
                        <div style="float: left; width: 70%; color: Black;">
                            <div style="float: left; width: 20%;">
                                Month
                            </div>
                            <div style="float: left; width: 69%;">
                                <asp:TextBox ID="TextBoxMonth" runat="server" CssClass="TextBox"></asp:TextBox>
                                <asp:NumericUpDownExtender ID="NumericUpDownExtender1" runat="server" TargetControlID="TextBoxMonth"
                                    Width="80" RefValues="January;February;March;April;May;June;July;August;September;October;November;December"
                                    ServiceDownMethod="" ServiceUpMethod="" TargetButtonDownID="" TargetButtonUpID=""
                                    Enabled="True" Maximum="1.7976931348623157E+308" Minimum="-1.7976931348623157E+308"
                                    ServiceDownPath="" Tag="" />
                                &nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="TextBoxYear" runat="server" CssClass="TextBox"></asp:TextBox>
                                <asp:NumericUpDownExtender ID="NumericUpDownExtender2" runat="server" TargetControlID="TextBoxYear"
                                    Width="60" RefValues="2012;2013;2014;2015;2016;2017;2018;2019;2020;2021;2022;2023;2024;2025;2026;2027;2028;2029;2030"
                                    ServiceDownMethod="" ServiceUpMethod="" TargetButtonDownID="" TargetButtonUpID=""
                                    Enabled="True" Maximum="1.7976931348623157E+308" Minimum="-1.7976931348623157E+308"
                                    ServiceDownPath="" Tag="" />
                            </div>
                        </div>
                        <div style="float: left; width: 30%; color: Black;">
                            <asp:Button ID="Finalize" runat="server" CssClass="Button" Text="Finalize" OnClick="Finalize_Click" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
