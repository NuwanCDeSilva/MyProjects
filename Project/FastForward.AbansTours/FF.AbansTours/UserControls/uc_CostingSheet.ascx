<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_CostingSheet.ascx.cs"
    Inherits="FF.AbansTours.UserControls.uc_CostingSheet" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<script type="text/javascript">
    function onblurFire(e, buttonid) {
        var evt = e ? e : window.event;
        var bt = document.getElementById(buttonid);
        if (bt) {
            bt.click();
            return false;
        }
    }

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

    function fun1(e, button2) {
        var evt = e ? e : window.event;
        var bt = document.getElementById(button2);
        if (bt) {
            if (evt.keyCode == 13) {
                bt.click();
                return false;
            }
        }
    }
</script>
<style type="text/css">
    .div_header
    {
        background-color: #b0b0b0;
        padding: 7px;
        color: #ffffff;
        font-size: inherit;
        font-weight: inherit;
        font-family: Arial, Helvetica, sans-serif;
        font-style: inherit;
        text-decoration: inherit;
        letter-spacing: 0.12em;
    }
    .div_Input
    {
        border: 1px #888888 solid;
        background-color: #919191;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <asp:Panel ID="pnlEnquiry" runat="server" Width="80%" Height="30%" Style="top: 5% !important;">
            <div style="float: left; height: auto; width: 100%; text-align: left; background-color: DIMGRAY;"
                class="div_header">
                <div style="float: left; height: 100%; width: 68%;">
                    <h1>
                        <asp:Label ID="Label2" Text="Abans Holidays" runat="server" /></h1>
                </div>
                <div style="float: right; height: 100%; width: 30%; text-align: right">
                    <asp:ImageButton ID="btnClose" ImageUrl="~/images/uploadify-cancel.png" runat="server"
                        OnClick="btnClose_Click" />
                </div>
            </div>
            <div style="float: left; height: 30px; width: 100%; text-align: left; background-color: LIGHTGREY">
                <asp:Label ID="Label1" Text="Costing Format" runat="server" />
            </div>
            <div style="float: left; height: 70px; width: 100%; text-align: left; background-color: AliceBlue">
                <div style="float: left; height: 100%; width: 68%;">
                    <div style="float: left; height: 30%; width: 100%;">
                        &nbsp;</div>
                    <div style="float: left; height: 30%; width: 100%;">
                        <div style="float: left; height: 100%; width: 30%;">
                            &nbsp;Client
                        </div>
                        <div style="float: left; height: 100%; width: 68%;">
                            <asp:Label ID="lblClient" Text="Client Details" runat="server" />
                        </div>
                    </div>
                    <div style="float: left; height: 30%; width: 100%;">
                        &nbsp;</div>
                </div>
                <div style="float: left; height: 100%; width: 28%;">
                    <div style="float: left; height: 30%; width: 100%;">
                        <div style="float: left; height: 100%; width: 30%;">
                            Date
                        </div>
                        <div style="float: left; height: 100%; width: 68%;">
                            <asp:TextBox ID="txtDateOfBirth" Enabled="false" CssClass="input-xlarge focused"
                                runat="server" onkeypress="return RestrictSpace()"></asp:TextBox>
                            <%--   <asp:CalendarExtender ID="txtClearingDateExtender" runat="server" Enabled="True"
                                Format="dd/MMM/yyyy" PopupButtonID="cal" TargetControlID="txtDateOfBirth">
                            </asp:CalendarExtender>
                            <img alt="Calendar.." height="16" src="../images/calendar.png width="16" id="cal" style="cursor: pointer" />--%>
                        </div>
                    </div>
                    <div style="float: left; height: 30%; width: 100%;">
                        <div style="float: left; height: 100%; width: 30%;">
                            REF
                        </div>
                        <div style="float: left; height: 100%; width: 68%;">
                            <asp:TextBox ID="txtReffNum" runat="server" />
                        </div>
                    </div>
                    <div style="float: left; height: 30%; width: 100%;">
                        <div style="float: left; height: 100%; width: 30%;">
                            PAX
                        </div>
                        <div style="float: left; height: 100%; width: 68%;">
                            <asp:TextBox ID="txtPAX" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <div style="float: left; height: 80%; width: 100%; background-color: AliceBlue" class="div_div_Input">
                <div style="float: left; height: 22px; width: 100%;">
                    &nbsp;
                </div>
                <div style="float: left; height: 22px; width: 100%;">
                    <div style="float: left; height: 22px; width: 50px;">
                        &nbsp;</div>
                    <div style="float: left; height: 22px; width: 50px;">
                        &nbsp;</div>
                    <div style="float: left; height: 22px; width: 150px">
                        &nbsp;</div>
                    <div style="float: left; height: 22px; width: 150px;">
                        &nbsp; Sub Type</div>
                    <div style="float: left; height: 22px; width: 100px;">
                        &nbsp;FARE USD</div>
                    <div style="float: left; height: 22px; width: 50px;">
                        &nbsp;TAX</div>
                    <div style="float: left; height: 22px; width: 100px;">
                        &nbsp;Total</div>
                    <div style="float: left; height: 22px; width: 100px;">
                        &nbsp;Total LKR</div>
                    <div style="float: left; height: 22px; width: 150px;">
                        &nbsp;Remarks</div>
                </div>
                <div style="float: left; height: 22px; width: 100%; background-color: AliceBlue">
                    <div style="float: left; height: 22px; width: 50px;">
                        &nbsp;</div>
                    <div style="float: left; height: 22px; width: 50px;">
                        Type</div>
                    <div style="float: left; height: 22px; width: 150px">
                        <asp:DropDownList ID="ddlCostType" runat="server" Width="90%">
                        </asp:DropDownList>
                    </div>
                    <div style="float: left; height: 22px; width: 150px;">
                        <asp:TextBox ID="txtCostSubType" runat="server" Width="90%" /></div>
                    <div style="float: left; height: 22px; width: 100px;">
                        <asp:TextBox ID="txtUSD" runat="server" Width="90%" /></div>
                    <div style="float: left; height: 22px; width: 50px;">
                        <asp:TextBox ID="txtTAX" runat="server" Width="90%" /></div>
                    <div style="float: left; height: 22px; width: 100px;">
                        <asp:TextBox ID="txtTotal" runat="server" Width="90%" /></div>
                    <div style="float: left; height: 22px; width: 100px;">
                        <asp:TextBox ID="txtTotalLKR" runat="server" Width="90%" /></div>
                    <div style="float: left; height: 22px; width: 150px;">
                        <asp:TextBox ID="txtRemark" runat="server" Width="90%" /></div>
                    <div style="float: left; height: 22px; width: 150px;">
                        <asp:ImageButton ID="btnAddtoGrid" ImageUrl="~/images/stat-down.png" runat="server"
                            Width="20px" OnClick="btnAddtoGrid_Click" />
                    </div>
                </div>
            </div>
            <div style="float: left; height: 1%; width: 100%; background-color: AliceBlue">
                &nbsp;
            </div>
            <div style="float: left; height: 80%; width: 100%; background-color: AliceBlue">
                <div style="float: left; height: 100%; width: 50px;">
                </div>
                <div style="float: left; height: 100%; width: 80%">
                    <asp:GridView ID="dgvCostSheet" runat="server" AutoGenerateColumns="False" Font-Size="X-Small">
                        <AlternatingRowStyle BackColor="Gainsboro" />
                        <Columns>
                            <asp:TemplateField Visible="false" HeaderText="Seq">
                                <ItemTemplate>
                                    <asp:Label ID="lblQCD_SEQ" runat="server" Text='<%# Bind("QCD_SEQ") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false" HeaderText="Cost Number">
                                <ItemTemplate>
                                    <asp:Label ID="lblQCD_COST_NO" runat="server" Text='<%# Bind("QCD_COST_NO") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Category" HeaderStyle-Width="180">
                                <ItemTemplate>
                                    <asp:Label ID="lblQCD_CAT" runat="server" Text='<%# Bind("QCD_CAT") %>'></asp:Label></ItemTemplate>
                                <HeaderStyle Width="180px" />
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false" HeaderText="Sub Category">
                                <ItemTemplate>
                                    <asp:Label ID="lblQCD_SUB_CATE" runat="server" Text='<%# Bind("QCD_SUB_CATE") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblQCD_DESC" runat="server" Text='<%# Bind("QCD_DESC") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false" HeaderText="Currency">
                                <ItemTemplate>
                                    <asp:Label ID="lblQCD_CURR" runat="server" Text='<%# Bind("QCD_CURR") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false" HeaderText="Exchange Rate">
                                <ItemTemplate>
                                    <asp:Label ID="lblQCD_EX_RATE" runat="server" Text='<%# Bind("QCD_EX_RATE") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false" HeaderText="Qty">
                                <ItemTemplate>
                                    <%# Eval("Monto", "{0:n}")%></ItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblQCD_QTY" runat="server" Text='<%# Bind("QCD_QTY") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unite Cost">
                                <ItemTemplate>
                                    <asp:Label ID="lblQCD_UNIT_COST" runat="server" Text='<%# Bind("QCD_UNIT_COST") %>'></asp:Label></ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TAX">
                                <ItemTemplate>
                                    <asp:Label ID="lblQCD_TAX" runat="server" Text='<%# Bind("QCD_TAX") %>'></asp:Label></ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Cost">
                                <ItemTemplate>
                                    <asp:Label ID="lblQCD_TOT_COST" runat="server" Text='<%# Bind("QCD_TOT_COST") %>'></asp:Label></ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Cost(LKR)">
                                <ItemTemplate>
                                    <asp:Label ID="lblQCD_TOT_LOCAL" runat="server" Text='<%# Bind("QCD_TOT_LOCAL") %>'></asp:Label></ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false" HeaderText="Mark up">
                                <ItemTemplate>
                                    <asp:Label ID="lblQCD_MARKUP" runat="server" Text='<%# Bind("QCD_MARKUP") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false" HeaderText="After Mark up">
                                <ItemTemplate>
                                    <asp:Label ID="lblQCD_AF_MARKUP" runat="server" Text='<%# Bind("QCD_AF_MARKUP") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remark">
                                <ItemTemplate>
                                    <asp:Label ID="lblQCD_RMK" runat="server" Text='<%# Bind("QCD_RMK") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="  ">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btndelete" runat="server" CommandArgument="<%# Container.DisplayIndex %>"
                                        CommandName="delete" ImageUrl="~/images/deleteicon.png" ToolTip="Delete.." ImageAlign="Middle" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="bodycolordarkgreen" HorizontalAlign="Center" Width="2%" />
                                <ItemStyle HorizontalAlign="Center" Width="2%" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Wrap="False" />
                    </asp:GridView>
                </div>
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:Button ID="btnEnquiry" runat="server" Text="D3" CssClass="hideButton" />
<asp:ModalPopupExtender ID="mpEnquiry" runat="server" DynamicServicePath="" Enabled="True"
    PopupControlID="pnlEnquiry" TargetControlID="btnEnquiry" BackgroundCssClass="modalBackground"
    PopupDragHandleControlID="pnlEnquiry" X="-140" Y="-250">
</asp:ModalPopupExtender>