<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucItemSerialView1.ascx.cs" Inherits="FastForward.SCMWeb.UserControls.ucItemSerialView1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<script src="<%=Request.ApplicationPath%>Js/jquery.min.js"></script>
<script src="<%=Request.ApplicationPath%>Js/bootstrap.min.js"></script>
<link href="<%=Request.ApplicationPath%>Css/bootstrap.css" rel="stylesheet" />

<link href="<%=Request.ApplicationPath%>Css/style.css" rel="stylesheet" />


<div class="row">
    <asp:HiddenField ID="hdfTabIndex" runat="server" Value="Document" />
    <asp:HiddenField ID="hdfTabShow" runat="server" Value="#documentTab" />
    <div class="col-sm-12">
        <ul id="myDocTab" class="nav nav-tabs">
            <li class="active"><a data-toggle="tab" href="#serialTab">Serial</a></li>
            <li><a data-toggle="tab" href="#documentTab">Document</a></li>
            <li><a data-toggle="tab" href="#BinTab">Bin Details</a></li>
        </ul>
        <div class="tab-content">
            <div class="tab-pane fade in active"  id="serialTab">
                <div class="row">
                    <div class="col-sm-12">
                        <asp:ScriptManagerProxy ID="ScriptManagerProxy5" runat="server"></asp:ScriptManagerProxy>
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="dgvSerial" CausesValidation="false" runat="server" AllowPaging="True" 
                                    EmptyDataText="No data found..." ShowHeaderWhenEmpty="True"
                                    GridLines="None" CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" AutoGenerateColumns="False" 
                                    OnPageIndexChanging="dgvSerial_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblItem" Text='<%# Bind("INS_ITM_CD") %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       <%-- <asp:BoundField HeaderText="Item Code" DataField="INS_ITM_CD" HeaderStyle-HorizontalAlign="Right">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        </asp:BoundField>--%>
                                        <asp:BoundField HeaderText="Serial 1" DataField="INS_SER_1"/>
                                        <asp:BoundField HeaderText="Serial 2" DataField="INS_SER_2"/>
                                        <asp:BoundField HeaderText="Serial 3" DataField="INS_SER_3"/>
                                        <asp:BoundField HeaderText="Item Status" DataField="MIS_DESC"/>
                                        <asp:BoundField HeaderText="Doc No" DataField="INS_DOC_NO"/>
                                        <asp:BoundField HeaderText="Date" DataField="INS_DOC_DT" DataFormatString="{0:dd/MMM/yyyy}" HeaderStyle-HorizontalAlign="Right">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Warranty No" DataField="INS_WARR_NO"/>
                                        <asp:BoundField HeaderText="Picked" DataField="INS_WARR_PERIOD">
                                              <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                         <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblHdUnitCost" Text='Unit Cost' runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblUnitCost" Text='<%# Bind("ins_unit_cost","{0:N}") %>' runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                            <ItemStyle CssClass="gridHeaderAlignRight" />
                                        </asp:TemplateField>
                                        
                                        <%--<asp:BoundField HeaderText="Column1" DataField="INS_AVAILABLE"/>--%>
                                        <asp:BoundField DataField="INS_AVAILABLE" HeaderText="Column1" />
                                    </Columns>
                                    <PagerStyle CssClass="cssPager" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="tab-pane" id="documentTab">
                <div class="row">
                    <div class="col-sm-8 col-md-8">
                        <asp:ScriptManagerProxy ID="ScriptManagerProxy6" runat="server"></asp:ScriptManagerProxy>
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="dgvDocument" CausesValidation="false" runat="server" AllowPaging="True" GridLines="None" 
                                    EmptyDataText="No data found..." ShowHeaderWhenEmpty="True"
                                    CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" AutoGenerateColumns="False" 
                                    OnPageIndexChanging="dgvDocument_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField HeaderText="Doc No" DataField="INB_DOC_NO">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Date" DataField="INB_DOC_DT" DataFormatString="{0:dd/MMM/yyyy}">
                                       </asp:BoundField>
                                        <asp:BoundField HeaderText="Qty" DataField="INB_QTY">
                                        <HeaderStyle CssClass="gridHeaderAlignRight" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblHdUnitCost" Text='Unit Cost' runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblUnitCost" Text='<%# Bind("INB_UNIT_COST","{0:N}") %>' runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="gridHeaderAlignRight" />
                                            <ItemStyle CssClass="gridHeaderAlignRight" />
                                        </asp:TemplateField>
                                          <asp:BoundField HeaderText="Expire Date" DataField="INB_EXP_DT" DataFormatString="{0:dd/MMM/yyyy}">
                                       </asp:BoundField>
                                        
                                    </Columns>
                                    <PagerStyle CssClass="cssPager" />
                                </asp:GridView>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="tab-pane" id="BinTab">
                <div class="row">
                    <div class="col-sm-8 col-md-8">
                        <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="dgvBin" CausesValidation="false" runat="server" AllowPaging="True" GridLines="None" 
                                    EmptyDataText="No data found..." ShowHeaderWhenEmpty="True"
                                    CssClass="table table-hover table-striped" PageSize="7" PagerStyle-CssClass="cssPager" AutoGenerateColumns="False" 
                                    OnPageIndexChanging="dgvBin_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label  runat="server" Text='Bin Code' Width="100px"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_inb_bin" runat="server"  Text='<%#Bind("inb_bin") %>' Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label  runat="server" Text='Item Status' Width="100px"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_itm_sts_dsc" runat="server" Text='<%#Bind("itm_sts_dsc") %>' Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label  runat="server" Text='Total' Width="100px"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Total" runat="server" Text='<%#Bind("Total") %>' Width="100px"></asp:Label>
                                            </ItemTemplate>
                                             <HeaderStyle CssClass="gridHeaderAlignRight" Width="100px" />
                                             <ItemStyle CssClass="gridHeaderAlignRight" Width="100px" />
                                         </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="cssPager" />
                                </asp:GridView>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>

    </div>
</div>

<script>
    function CustomTabShow() {
        var tab = document.getElementById('<%= hdfTabShow.ClientID%>').value;
       // alert(tab);
        $('#myDocTab a[href="' + tab + '"]').tab('show');
    };
        
</script> 