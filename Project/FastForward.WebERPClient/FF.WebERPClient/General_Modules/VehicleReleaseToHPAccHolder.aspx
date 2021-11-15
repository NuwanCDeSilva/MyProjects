<%@ Page Title="Vehicle Release" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="VehicleReleaseToHPAccHolder.aspx.cs" Inherits="FF.WebERPClient.General_Modules.VehicleReleaseToHPAccHolder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="float: left; width: 100%; text-align: right;">
        <asp:Button ID="btn_APPROVE" runat="server" Text="Approve" CssClass="Button" OnClick="btn_APPROVE_Click" />
        <asp:Button ID="btn_Clear" runat="server" Text="Clear" CssClass="Button" OnClick="btn_Clear_Click" />
        <asp:Button ID="btn_CANCEL" runat="server" Text="Cancel" CssClass="Button" OnClick="btn_CANCEL_Click" />
    </div>
    <div style="float: left; width: 100%;">
        <asp:Panel ID="PanelSearch" runat="server">
            <div style="padding: 4.0px; float: left; width: 100%;">
                <div style="padding: 5.0px; float: left; width: 15%; text-align: center;">
                    <asp:Label ID="Label11" runat="server" Text="Invoice Number :" Font-Bold="True"></asp:Label>
                </div>
                <div style="padding: 5.0px; float: left; width: 25%;">
                    <asp:TextBox ID="txtInvoiceNum" runat="server" CssClass="TextBox" 
                        AutoPostBack="True" ontextchanged="txtInvoiceNum_TextChanged"></asp:TextBox>
                    &nbsp;
                    <asp:Button ID="btnInvOk" runat="server" Text="OK" CssClass="Button" OnClick="btnInvOk_Click" />
                    &nbsp;
                    <asp:ImageButton ID="imgbtnSearch" runat="server" ImageUrl="~/Images/icon_search.png"
                        ToolTip="Advance Search" onclick="imgbtnSearch_Click" />
                </div>
                <div style="float: left; width: 45%;">
                </div>
            </div>
            <div style="float: left; width: 100%;" runat="server">
                <%--Invoice Detail,customer details (Code, Name, invoice date) Headings--%>
                <div style="float: left; width: 20%;">
                    <asp:Label ID="Label1" runat="server" Text="Invoice Date:" ForeColor="#3366FF"></asp:Label>
                </div>
                <div style="float: left; width: 20%;">
                    <asp:Label ID="Label2" runat="server" Text="Customer Code:" ForeColor="#3366FF"></asp:Label>
                </div>
                <div style="float: left; width: 20%;">
                    <asp:Label ID="Label3" runat="server" Text="Customer Name:" ForeColor="#3366FF"></asp:Label>
                </div>
                <div style="float: left; width: 20%;">
                    <asp:Label ID="Label4" runat="server" Text="Vehicle Insurance No:" ForeColor="#3366FF"></asp:Label>
                </div>
                <div style="float: left; width: 19%;">
                    <asp:Label ID="Label5" runat="server" ForeColor="#3366FF"></asp:Label>
                </div>
            </div>
            <div style="float: left; width: 100%;" id="divInvDet2">
                <%--Invoice Detail,customer details (Code, Name, invoice date) =Data--%>
                <div style="float: left; width: 20%;">
                    <asp:Label ID="lblInvDt" runat="server" ForeColor="#3366FF">.</asp:Label>
                </div>
                <div style="float: left; width: 20%;">
                    <asp:Label ID="lblCustCD" runat="server" ForeColor="#3366FF"></asp:Label>
                </div>
                <div style="float: left; width: 20%;">
                    <asp:Label ID="lblCustName" runat="server" ForeColor="#3366FF"></asp:Label>
                </div>
                <div style="float: left; width: 20%;">
                    <asp:Label ID="lblInsuranceNo" runat="server" ForeColor="#3366FF"></asp:Label>
                </div>
                <div style="float: left; width: 19%;">
                    <asp:Label ID="Label10" runat="server" ForeColor="#3366FF"></asp:Label>
                </div>
            </div>
            <div style="padding: 4.0px; float: left; width: 100%;">
            </div>
            <div style="padding: 1.0px; float: left; width: 99%;">
                <asp:Panel ID="Panel_invoiceDet" runat="server" GroupingText=" Invoice Detail" ScrollBars="Vertical"
                    Height="161px">
                    <div style="float: left; text-align: center;">
                        <asp:GridView ID="grvInvoiceDet" runat="server" CellPadding="4" CssClass="GridView"
                            ForeColor="#333333" DataKeyNames="P_srvt_ref_no" OnRowDeleting="grvInvoiceDet_RowDeleting"
                            AutoGenerateColumns="False" ShowHeaderWhenEmpty="True">
                            <AlternatingRowStyle BackColor="White" />
                            <EmptyDataTemplate > <div style="width:100%; text-align:center;"> No data found </div> </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="Reference #">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="linkBtnRefNum" runat="server" Text='<%# Bind("P_srvt_ref_no") %>'
                                            OnClick="linkBtnRefNum_Click" CommandName="DELETE"></asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("P_srvt_ref_no") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="P_srvt_itm_cd" HeaderText="Item Code" />
                                <asp:BoundField DataField="P_svrt_model" HeaderText="Modle" />
                                <asp:BoundField HeaderText="Brand" />
                                <asp:BoundField DataField="P_svrt_engine" HeaderText="Engine No." />
                                <asp:BoundField DataField="P_svrt_chassis" HeaderText="Chasse No." />
                                <asp:BoundField DataField="P_srvt_insu_ref" HeaderText="Insurance No." />
                                <asp:BoundField DataField="P_svrt_veh_reg_no" HeaderText="Vehicle No." />
                                <asp:TemplateField HeaderText="Approve">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chekApprove" runat="server" Checked="True" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
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
                </asp:Panel>
            </div>
            <div style="float: left; width: 100%;">
                <asp:Panel ID="Panel_vehicleDet" runat="server">
                    <asp:GridView ID="grvVehicleDet" runat="server" CellPadding="4" CssClass="GridView"
                        ForeColor="#333333" GridLines="None" OnRowDataBound="grvVehicleDet_RowDataBound"
                        OnRowDeleting="grvVehicleDet_RowDeleting" DataKeyNames="Tus_usrseq_no,Tus_ser_1,Tus_ser_2">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="Tus_base_doc_no" HeaderText="Doc #" />
                            <asp:BoundField DataField="Tus_doc_no" HeaderText="Invoice Ref #" />
                            <asp:BoundField DataField="Tus_ser_1" HeaderText="Engine #" />
                            <asp:BoundField DataField="Tus_ser_2" HeaderText="Chasse #" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chekApprove2" runat="server" Text="Approve" AutoPostBack="True"
                                        CommandName="DELETE" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server"></asp:Label>
                                    <asp:ImageButton ID="imgBtnApprove" runat="server" CommandName="DELETE" ImageUrl="~/Images/approve_img.png" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="userSeq" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Tus_usrseq_no") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Tus_usrseq_no") %>'></asp:TextBox>
                                </EditItemTemplate>
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
                </asp:Panel>
            </div>
        </asp:Panel>
    </div>
    <div style="float: left; width:100%; text-align: center;">
    <div style="padding: 1.0px; float: left; width:1%; text-align: center;">
    
    </div>
    <div style="float: left; width:40%; text-align: center;">
    <asp:Panel ID="Panel_docRequired" runat="server" 
            GroupingText="Required Documents for Approval" ScrollBars="Auto">
            <div style="padding: 2.0px; float: left; width:95%; text-align: left;"> </div>
            <asp:GridView ID="grv_docRequired" runat="server" CellPadding="4" 
                ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" 
                onrowdatabound="grv_docRequired_RowDataBound" ShowHeaderWhenEmpty="True">
                <AlternatingRowStyle BackColor="White" />
                <EmptyDataTemplate > <div style="width:100%; text-align:center;"> No data found </div> </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="hpd_desc" HeaderText="Document  Name" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lblStar" runat="server" Text="*" ForeColor="Red"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lbl_IsMand" runat="server" Text='<%# Bind("hsp_is_required") %>' ></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                        </EditItemTemplate>
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
        </asp:Panel>
    </div>
    <div style="float: left; width:15%; text-align: center;">
    
    </div>
        
    </div>
    <div style="float: left; width:100%; text-align: right;">
            <asp:Panel ID="Panel_serchInvNo" runat="server" BackColor="White" Width="60%" 
                GroupingText=" ">
                <%--<div style="float: left; width: 100%; text-align: right;">--%>
                    <div style="float: left; width: 100%; text-align: right;">
                        <asp:Button ID="btnCloseSearch" runat="server" Text="Close" CssClass="Button" />
                    </div>
                    <div style="padding: 2.0px; float: left; width: 99%; text-align: left;">
                        <asp:Label ID="lblSearchMsg" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                    <div style="float: left; width: 100%; text-align: right;">
                        <div style="float: left; width: 20%; text-align: right;">
                            <asp:Label ID="Label7" runat="server" Text="Serch By: "></asp:Label>
                        </div>
                        <div style="float: left; width: 20%; text-align: right;">
                            <asp:DropDownList ID="ddlSerachBy" runat="server" CssClass="ComboBox" 
                                Width="100%">
                                <asp:ListItem>Account No.</asp:ListItem>
                                <asp:ListItem>Invoice Date</asp:ListItem>
                                <asp:ListItem>Registration No.</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                       
                        <div style="float: left; width: 30%; text-align: right;">
                            <asp:TextBox ID="txtInvSerch" runat="server" CssClass="TextBox"></asp:TextBox>
                        </div>
                        <div style="float: left; width: 15%; text-align: center;">
                            
                            <asp:Button ID="btnSearchOk" runat="server" Text="Ok" CssClass="Button" OnClick="btnSearchOk_Click" />
                        </div>
                    </div>
                     <div style="padding: 2.0px; float: left; width: 99%; text-align: right;"></div>
                    <div style="float: left; text-align: right; width: 99%; height: 172px;">
                        
                        <asp:Panel ID="Panel_grvSearch" runat="server" ScrollBars="Auto" Height="120px">
                        <asp:GridView ID="grvSerchResults" runat="server" 
                            OnSelectedIndexChanged="grvSerchResults_SelectedIndexChanged" 
                            ShowHeaderWhenEmpty="True" CellPadding="4" CssClass="GridView" 
                            ForeColor="#333333" AutoGenerateColumns="False" Width="99%">
                             <EditRowStyle BackColor="#2461BF" />
                             
                             <AlternatingRowStyle BackColor="White" />
                             <EmptyDataTemplate > <div style="width:100%; text-align:center;"> No data found </div> </EmptyDataTemplate>
                            <Columns>
                                <asp:CommandField SelectText="  SELECT " ShowSelectButton="True" >
                                <ControlStyle Font-Bold="True" ForeColor="#00CC66" />
                                </asp:CommandField>
                                <asp:BoundField HeaderText="Invoice No." DataField="sah_inv_no" />
                                <asp:BoundField HeaderText="Invoice Date" DataField="sah_dt" />
                                <asp:BoundField HeaderText="Customer Code" DataField="sah_cus_cd" />
                                <asp:BoundField DataField="sah_cus_name" HeaderText="Customer Name" />
                            </Columns>
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
                        </asp:Panel>
                    </div>
                    <div style="padding: 2.0px; float: left; width: 99%; text-align: right;"></div>
                <%--</div>--%>
            </asp:Panel>
      
    </div>
    <div style="display: none">
        <asp:Button ID="btnHidden_popupSearch" runat="server" Text="Button" />
    </div>
    <div style="float: left; width: 100%; text-align: right;">
        <asp:ModalPopupExtender ID="ModalPopupExtSearch" runat="server" ClientIDMode="Static"
            BehaviorID="btnCloseSearch" BackgroundCssClass="modalBackground" PopupControlID="Panel_serchInvNo"
            TargetControlID="btnHidden_popupSearch">
        </asp:ModalPopupExtender>
        <%--<asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server">
        </asp:ModalPopupExtender>--%>
    </div>
</asp:Content>
