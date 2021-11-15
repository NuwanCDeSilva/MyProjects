<%@ Page Title="Hp Business Rules" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="AccountCreationRestriction.aspx.cs" Inherits="FF.WebERPClient.HP_Module.AccountCreationRestriction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControls/uc_CompanySearch.ascx" TagName="uc_CompanySearch"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/uc_ProfitCenterSearch.ascx" TagName="uc_ProfitCenterSearch"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .GridView
        {}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        function isNumberKeyAndDot(event, value) {
            var charCode = (event.which) ? event.which : event.keyCode
            var intcount = 0;
            var stramount = value;
            for (var i = 0; i < stramount.length; i++) {
                if (stramount.charAt(i) == '.' && charCode == 46) {
                    return false;
                }
            }
            if ((charCode > 31 && (charCode < 48 || charCode > 57)) && charCode != 46)
                return false;
            return true;
        }
        function onblurFire(e, buttonid) {
            var evt = e ? e : window.event;
            var bt = document.getElementById(buttonid);
            if (bt) {

                bt.click();
                return false;
            }
        }
    </script>
    <asp:UpdatePanel ID="updtMainPnl" runat="server">
        <ContentTemplate>
            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%"
                Height="600px">
                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="::Account Creation Restriction::">
                    <ContentTemplate>
                    <div style="display: none">
                       <asp:Button ID="btn_VeiwAcRestr" runat="server" Text="Veiw" CssClass="Button"                          />
                    </div>
                        <div style="float: left; width: 100%; text-align: right; font-size: smaller;">
                            <div style="float: left; width: 100%; text-align: right;">
                                <asp:Button ID="btn_SAVE" runat="server" Text="Save" CssClass="Button" OnClick="btn_SAVE_Click" />
                                  <asp:Button ID="btnAccRestrVeiw" runat="server" Text="Veiw" CssClass="Button" 
                                    Font-Underline="True" onclick="btnAccRestrVeiw_Click" />
                              
                                <asp:Button ID="btn_CLEAR" runat="server" Text="Clear" CssClass="Button" OnClick="btn_CLEAR_Click" />
                                <asp:Button ID="btn_CLOSE" runat="server" Text="Close" CssClass="Button" OnClick="btn_CLOSE_Click" />
                            </div>
                            <asp:Panel ID="Panel16" runat="server" CssClass="PanelHeader">
                            </asp:Panel>
                            <div style="float: left; width: 100%; text-align: right;">
                                <asp:Panel ID="Panel4" runat="server" GroupingText=" ">
                                    <div style="float: left; width: 30%; text-align: left;">
                                        <div style="float: left; width: 50%; text-align: left;">
                                            <asp:RadioButton ID="rdoAnual" runat="server" Text="ANUALLY" Font-Bold="True" AutoPostBack="True"
                                                Checked="True" GroupName="AnualOrMonth" OnCheckedChanged="rdoAnual_CheckedChanged" />
                                        </div>
                                        <div style="float: left; width: 49%; text-align: left;">
                                            <asp:RadioButton ID="rdoMonthly" runat="server" Text="MONTHLY" Font-Bold="True" AutoPostBack="True"
                                                GroupName="AnualOrMonth" OnCheckedChanged="rdoMonthly_CheckedChanged" />
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div style="float: left; width: 60%;">
                                    <asp:Panel ID="Panel1" runat="server" BorderColor="Blue" GroupingText=" ">
                                        <uc2:uc_ProfitCenterSearch ID="uc_ProfitCenterSearch1" runat="server" />
                                    </asp:Panel>
                                </div>
                                <div style="float: left; width: 10%; text-align: center;">
                                    <asp:ImageButton ID="btnAddToPC_list" runat="server" ImageUrl="~/Images/right_arrow_icon.png"
                                        Width="30%" OnClick="btnAddToPC_list_Click" ToolTip="Add to Profit Center List" />
                                </div>
                                <div style="float: left; width: 15%; text-align: right;">
                                    <asp:Panel ID="Panel2" runat="server" Height="170px" ScrollBars="Vertical" BorderColor="Blue"
                                        BorderWidth="1px" GroupingText="Profit Centers">
                                        <asp:GridView ID="grvProfCents" runat="server" AutoGenerateColumns="False" OnRowDataBound="grvProfCents_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="PROFIT_CENTER" ShowHeader="False" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                                        <asp:CheckBox ID="chekPc" runat="server" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                    <div style="float: left; width: 100%; text-align: right;">
                                        <div style="float: left; width: 30%; text-align: right;">
                                            <asp:Button ID="btnAll" runat="server" Text="All" CssClass="Button" Width="100%"
                                                OnClick="btnAll_Click" />
                                        </div>
                                        <div style="float: left; width: 30%; text-align: right;">
                                            <asp:Button ID="btnNone" runat="server" Text="None" CssClass="Button" OnClick="btnNone_Click" />
                                        </div>
                                        <div style="float: left; width: 30%; text-align: right;">
                                            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="Button" OnClick="btnClear_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div style="padding: 3.0px; float: left; width: 96%; text-align: right;">
                            </div>
                            <div style="float: left; width: 100%; text-align: left;">
                                <asp:Panel ID="Panel6" runat="server" GroupingText=" ">
                                    <div style="padding: 1.0px; float: left; width: 99%; text-align: right;">
                                    </div>
                                    <div style="padding: 1.0px; float: left; width: 99%; text-align: right;">
                                        <div style="float: left; width: 25%; text-align: right;">
                                            <asp:Label ID="Label2" runat="server" Text="From Date:"></asp:Label>
                                        </div>
                                        <div style="float: left; width: 15%; text-align: right;">
                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="TextBox"></asp:TextBox>
                                            <asp:CalendarExtender ID="txtFromDt_CalendarExtender" runat="server" Enabled="True"
                                                Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                                            </asp:CalendarExtender>
                                        </div>
                                        <div style="float: left; width: 39%; text-align: left;" id="divToDate" runat="server">
                                            <div style="float: left; width: 20%; text-align: right;">
                                                <asp:Label ID="Label3" runat="server" Text="To Date:"></asp:Label>
                                            </div>
                                            <div style="float: left; width: 40%; text-align: right;">
                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="TextBox"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                    TargetControlID="txtToDate">
                                                </asp:CalendarExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="padding: 1.0px; float: left; width: 99%; text-align: right;">
                                        <div style="float: left; width: 25%; text-align: right;">
                                            <asp:Label ID="Label6" runat="server" Text="Total Approved No. Of Accounts:"></asp:Label>
                                        </div>
                                        <div style="float: left; width: 15%; text-align: right;">
                                            <asp:TextBox ID="txtApprNoOfAcc" runat="server" CssClass="TextBox" onkeypress="return isNumberKeyAndDot(event,this.value)"></asp:TextBox>
                                        </div>
                                        <div style="float: left; width: 15%; text-align: left;">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtApprNoOfAcc"
                                                ErrorMessage=" Enter # of Accounts" ForeColor="Red" ValidationGroup="ReqFiels"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div style="padding: 1.0px; float: left; width: 99%; text-align: right;">
                                        <div style="float: left; width: 25%; text-align: right;">
                                            <asp:Label ID="Label4" runat="server" Text="Total Approved Sales Value:"></asp:Label>
                                        </div>
                                        <div style="float: left; width: 15%; text-align: right;">
                                            <asp:TextBox ID="txtSalesVal" runat="server" CssClass="TextBox" onkeypress="return isNumberKeyAndDot(event,this.value)"></asp:TextBox>
                                        </div>
                                        <div style="float: left; width: 15%; text-align: left;">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSalesVal"
                                                ErrorMessage=" Enter Sales Value" ForeColor="Red" ValidationGroup="ReqFiels"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div style="padding: 1.0px; float: left; width: 99%; text-align: right;" id="divNoOfMonths"
                                        runat="server">
                                        <div style="float: left; width: 25%; text-align: right;">
                                            <asp:Label ID="Label5" runat="server" Text="No. Of Months:"></asp:Label>
                                        </div>
                                        <div style="float: left; width: 15%; text-align: right;">
                                            <asp:TextBox ID="txtNoOfMonths" runat="server" CssClass="TextBox"></asp:TextBox>
                                        </div>
                                        <div style="float: left; width: 15%; text-align: center;">
                                        </div>
                                        <div style="float: left; width: 15%; text-align: center;">
                                        </div>
                                    </div>
                                    <div style="padding: 1.0px; float: left; width: 99%; text-align: center;">
                                        <div style="padding: 2.0px; float: left; width: 25%; text-align: right;">
                                        </div>
                                        <div style="float: left; width: 13%; text-align: center;">
                                            &nbsp;&nbsp;<asp:Button ID="btnAdd" runat="server" CssClass="Button" Height="16px"
                                                OnClick="btnAdd_Click" Text="Add To List" ValidationGroup="ReqFiels" />
                                        </div>
                                        <div style="float: left; width: 49%; text-align: left;">
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                            <div style="padding: 4.0px; float: left; width: 96%; text-align: right;">
                            </div>
                           <asp:Panel ID="Panel21" runat="server" CssClass="PanelHeader" BackColor="#6699FF"
                                    Font-Bold="True" ForeColor="Black" HorizontalAlign="Left">
                                    Added List
                                </asp:Panel>
                            <div style="float: left; width: 100%; text-align: left;">
                                
                                <asp:Panel ID="Panel5" runat="server" GroupingText=" ">
                                    <div style="float: left; width: 100%; text-align: right;">
                                        <div style="float: left; width: 80%; text-align: right;">
                                            <asp:Panel ID="Panel3" runat="server" Height="151px" ScrollBars="Auto">
                                                <asp:GridView ID="grvResctrict" runat="server" CellPadding="4" CssClass="GridView"
                                                    ForeColor="#333333" AutoGenerateColumns="False" OnRowDeleting="grvResctrict_RowDeleting"
                                                    ShowHeaderWhenEmpty="True" Width="98%" >
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <EmptyDataTemplate>
                                                        <div style="width: 100%; text-align: center;">
                                                            No data found
                                                        </div>
                                                    </EmptyDataTemplate>
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server"></asp:Label>
                                                                <asp:ImageButton ID="imgBtnDelete" runat="server" ImageUrl="~/Images/Delete.png"
                                                                    CommandName="Delete" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Hrs_pc" HeaderText="Profit Center" />
                                                        <asp:BoundField DataField="Hrs_from_dt" DataFormatString="{0:d}" HeaderText="From Date" />
                                                        <asp:BoundField DataField="Hrs_to_dt" DataFormatString="{0:d}" HeaderText="To Date" />
                                                        <asp:BoundField DataField="Hrs_no_ac" HeaderText="No of Accounts" />
                                                        <asp:BoundField DataField="Hrs_tot_val" DataFormatString="{0:n2}" HeaderText="Approved Value" />
                                                        <asp:TemplateField HeaderText="Seq" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSEQno" runat="server" Text='<%# Bind("Hrs_seq") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Hrs_seq") %>'></asp:TextBox>
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
                                        <div style="padding: 1.0px; float: left; width: 9%; text-align: left;">
                                            <asp:Button ID="btnClearSaveList" runat="server" CssClass="Button" Text="Clear All"
                                                OnClick="btnClearSaveList_Click" />
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                        <div style="float: left; width: 100%; background-color: #FFFFFF; font-size: 9px;">
                            <asp:Panel runat="server" BackColor="White" Width="70%" BorderColor="#3399FF" BorderStyle="Solid"
                                BorderWidth="3px" ID="Panel_AcRestVeiw">
                                <div style="float: left; width: 100%;">
                                    <div style="padding: 0.5px; float: left; width: 99%; text-align: right;">                                       
                                        <asp:Button ID="btnAcRestVeiwClose" runat="server" Text="Close" 
                                            CssClass="Button"  />  
                                    </div>
                                </div>                                     
                              
                                <div style="padding: 1.0px; float: left; width: 100%;">
                                <div  style="float: left; width: 5%;">
                                    &nbsp;</div>
                                    <div style="float: left; width: 20%;">
                                      <asp:RadioButton ID="rdoVeiwMonthly" runat="server" GroupName="viewAcRestr" 
                                            Text="Monthly" Checked="True" ForeColor="Blue" />
                                    </div>
                                    <div style="float: left; width: 25%;">
                                        <asp:RadioButton ID="rdoVeiwAnnually" runat="server" GroupName="viewAcRestr" 
                                            Text="Annually" ForeColor="Blue"  />
                                       
                                    </div>
                                    <div style="float: left; width: 10%;">
                                       
                                    </div>
                                    <div style="float: left; width: 30%;">
                                        
                                       
                                    </div>
                                </div>

                                <div style="padding: 1.0px; float: left; width: 100%;">
                                   <div  style="float: left; width: 5%;">
                                    &nbsp;
                                    </div>
                                    <div style="float: left; width: 20%;">
                                        <asp:Label ID="Label11" runat="server" Text="Profit Center" Font-Bold="False"></asp:Label>
                                    </div>
                                    <div style="float: left; width: 25%;">
                                        <asp:TextBox ID="txtVeiwPcAcRestr" runat="server" CssClass="TextBoxUpper" 
                                            Width="85%"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 10%;">
                                       
                                        &nbsp;</div>
                                    <div style="float: left; width: 30%;">
                                        <asp:Button ID="btnVeiwAcRestr" runat="server" CssClass="Button" Text="Veiw" 
                                            onclick="btnVeiwAcRestr_Click" />

                                               &nbsp;
                                         </div>
                                </div>
                                <div style="padding: 1.0px; float: left; width: 100%;">
                                <div  style="float: left; width: 5%;">
                                    &nbsp;
                                    </div>
                                    <div style="float: left; width: 20%;">
                                        &nbsp;
                                    </div>
                                       <div style="float: left; width: 25%;">
                                       
                                    </div>
                                </div>

                                <div style="float: left; width: 100%;">
                                   
                                    <div style="float: left; width: 100%;">
                                        <asp:Panel ID="Panel19" runat="server" CssClass="PanelHeader" 
                                            HorizontalAlign="Center"> 
                                        </asp:Panel>
                                        <asp:Panel ID="Panel20" runat="server" Height="140px" ScrollBars="Both" 
                                            GroupingText=" " BorderColor="#3366FF" BorderWidth="1px">
                                            <asp:GridView ID="grvVeiwAcRestr" runat="server" CellPadding="4" CssClass="GridView"
                                                    ForeColor="#333333" AutoGenerateColumns="False" 
                                                    ShowHeaderWhenEmpty="True" Width="98%">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <EmptyDataTemplate>
                                                        <div style="width: 100%; text-align: center;">
                                                            No data found
                                                        </div>
                                                    </EmptyDataTemplate>
                                                    <Columns>
                                                        <asp:BoundField DataField="Hrs_pc" HeaderText="Profit Center" >
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Hrs_from_dt" DataFormatString="{0:d}" 
                                                            HeaderText="From Date" >
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Hrs_to_dt" DataFormatString="{0:d}" 
                                                            HeaderText="To Date" >
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Hrs_no_ac" HeaderText="No of Accounts" >
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Hrs_tot_val" DataFormatString="{0:n2}" 
                                                            HeaderText="Approved Value" >
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Seq" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSEQno" runat="server" Text='<%# Bind("Hrs_seq") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Hrs_seq") %>'></asp:TextBox>
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
                                </div>
                                <div style="float: left; width: 100%;">
                                
                                    &nbsp;</div>
                            </asp:Panel>
                              <asp:ModalPopupExtender ID="ModalPopupVeiwAcRestr" runat="server" ClientIDMode="Static"
                                CancelControlID="btnAcRestVeiwClose" BackgroundCssClass="modalBackground" PopupControlID="Panel_AcRestVeiw"
                                TargetControlID="btn_VeiwAcRestr" DynamicServicePath="" Enabled="True">
                            </asp:ModalPopupExtender>
                        </div>
                        <div>
                           
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel2" runat="server" HeaderText=":: Hp Parameters ::">
                    <ContentTemplate>
                        <div style="float: left; width: 100%; font-size: smaller;">
                            <asp:Panel ID="Panel7" runat="server" CssClass="PanelHeader">
                            </asp:Panel>
                            <div style="padding: 1.0px; float: left; width: 100%; text-align: right;">
                                <asp:Button ID="btnSavePty" runat="server" Text="Save" CssClass="Button" OnClick="btnSavePty_Click" />
                                &nbsp;
                                  <asp:Button ID="btnVeiwPara" runat="server" Text="Veiw" CssClass="Button" 
                                    OnClick="btnVeiwPara_Click" Font-Underline="True" />
                                   &nbsp;
                                <asp:Button ID="btnCloneParam" runat="server" Text="Clone" CssClass="Button" 
                                    Font-Underline="True" onclick="btnCloneParam_Click" />&nbsp;
                                 <asp:Button ID="btnClear2" runat="server" Text="Clear" CssClass="Button" OnClick="btn_CLEAR_Click" />&nbsp;
                                <asp:Button ID="btnClose2" runat="server" Text="Close" CssClass="Button" OnClick="btn_CLOSE_Click" />
                            </div>
                            <div style="float: left; width: 100%;">
                                <div style="float: left; width: 55%;">
                                    <asp:Panel ID="Panel10" runat="server" GroupingText=" ">
                                        <uc2:uc_ProfitCenterSearch ID="uc_ProfitCenterSearch2" runat="server" />
                                    </asp:Panel>
                                </div>
                                <div style="float: left; width: 44%;">
                                    <div style="float: left; width: 10%;">
                                        <asp:ImageButton ID="ImgBtnAdd_pty" runat="server" ImageUrl="~/Images/right_arrow_icon.png"
                                            Height="28px" Width="26px" OnClick="ImgBtnAdd_pty_Click" />
                                    </div>
                                    <div style="float: left; width: 40%;">
                                        <div style="float: left; width: 100%;">
                                            <asp:Panel ID="Panel11" runat="server" ScrollBars="Vertical" Height="130px" BorderColor="Blue"
                                                BorderWidth="1px" GroupingText="Profit Centers">
                                                <asp:GridView ID="grvPC_pty" runat="server" AutoGenerateColumns="False" Width="100%"
                                                    OnRowDataBound="grvPC_pty_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server"></asp:Label>
                                                                <asp:CheckBox ID="chekPc_para" runat="server" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="PROFIT_CENTER" ShowHeader="False" />
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </div>
                                        <div style="float: left; width: 100%;">
                                            <asp:Button ID="btnAll_pty" runat="server" Text="All" CssClass="Button" Width="30%"
                                                OnClick="btnAll_pty_Click" />
                                            <asp:Button ID="btnNone_pty" runat="server" Text="None" CssClass="Button" Width="30%"
                                                OnClick="btnNone_pty_Click" />
                                            <asp:Button ID="btnClear_pty" runat="server" Text="Clear" CssClass="Button" Width="30%"
                                                OnClick="btnClear_pty_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div style="float: left; width: 100%;">
                                <asp:Panel ID="Panel8" runat="server" CssClass="PanelHeader" BackColor="#6699FF"
                                    Font-Bold="True" ForeColor="Black">
                                    Parameter Selection
                                </asp:Panel>
                                <div style="float: left; width: 100%;">
                                    <asp:Panel ID="Panel9" runat="server" GroupingText=" ">
                                        <div style="padding: 2.0px; float: left; width: 100%;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 55%;">
                                            <div style="float: left; width: 20%; text-align: center;">
                                                PARAMETER CODE
                                            </div>
                                            <div style="float: left; width: 20%;">
                                                <asp:TextBox ID="txtParaCode" runat="server" CssClass="TextBoxUpper" 
                                                    Width="100%" AutoPostBack="True" ontextchanged="txtParaCode_TextChanged"></asp:TextBox>
                                            </div>
                                            <div style="float: left; width: 10%; text-align: center;">
                                                <asp:ImageButton ID="ImgCodeSearch" runat="server" 
                                                    ImageUrl="~/Images/icon_search.png" onclick="ImgCodeSearch_Click" />
                                            </div>
                                            <div style="float: left; width: 20%; text-align: center;">
                                            </div>
                                            <div style="float: left; width: 100%;">
                                                <div style="float: left; width: 20%; text-align: center;">
                                                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtParaCode"
                                                        ErrorMessage="Code required" ForeColor="Red" ValidationGroup="para"></asp:RequiredFieldValidator>
                                                </div>
                                                <asp:Label ID="lblCodeDesc" runat="server" ForeColor="Blue"></asp:Label>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 44%;">
                                            <div style="padding: 1.0px; float: left; width: 100%;">
                                                <div style="float: left; width: 20%;">
                                                    From Date
                                                </div>
                                                <div style="float: left; width: 60%;">
                                                    <asp:TextBox ID="txtFromDt_pty" runat="server" CssClass="TextBox" Width="40%"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                        TargetControlID="txtFromDt_pty">
                                                    </asp:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtFromDt_pty"
                                                        ErrorMessage="Enter date" ForeColor="Red" ValidationGroup="para"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div style="padding: 1.0px; float: left; width: 100%;">
                                                <div style="float: left; width: 20%;">
                                                    To Date
                                                </div>
                                                <div style="float: left; width: 60%;">
                                                    <asp:TextBox ID="txtToDt_pty" runat="server" CssClass="TextBox" Width="40%"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                        TargetControlID="txtToDt_pty">
                                                    </asp:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtToDt_pty"
                                                        ErrorMessage="Enter Date" ForeColor="Red" ValidationGroup="para"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div style="padding: 1.0px; float: left; width: 100%;">
                                                <div style="float: left; width: 20%;">
                                                    Value
                                                </div>
                                                <div style="float: left; width: 60%;">
                                                    <asp:TextBox ID="txtValue_pty" runat="server" CssClass="TextBox" Width="40%" onkeypress="return isNumberKeyAndDot(event,this.value)"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtValue_pty"
                                                        ErrorMessage="Enter value" ForeColor="Red" ValidationGroup="para"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div style="padding: 1.0px; float: left; width: 100%;">
                                                <div style="float: left; width: 20%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 60%;">
                                                    <asp:Button runat="server" Text="Add to List" CssClass="Button" ID="btnAddPara" Width="40%"
                                                        OnClick="btnAddPara_Click" ValidationGroup="para" />
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                            <div style="float: left; width: 100%;">
                                <asp:Panel ID="Panel13" runat="server" CssClass="PanelHeader" BackColor="#6699FF"
                                    Font-Bold="True" ForeColor="Black">
                                    Added List
                                </asp:Panel>
                                <asp:Panel ID="Panel12" runat="server" Height="196px" ScrollBars="Vertical">
                                    <asp:GridView ID="grvParameters" runat="server"  
                                        CellPadding="4" CssClass="GridView"
                                        ForeColor="#333333" AutoGenerateColumns="False" Width="80%" OnRowDataBound="grvParameters_RowDataBound"
                                        OnRowDeleting="grvParameters_RowDeleting">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                                    <asp:ImageButton ID="ImgBtnDelPara" runat="server" CommandName="Delete" ImageAlign="Middle"
                                                        ImageUrl="~/Images/Delete.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SEQ" Visible="False">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblParaSeq" runat="server" Text='<%# Eval("Hsy_seq")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Hsy_cd" HeaderText="Code">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Hsy_desc" HeaderText="Code Description">
                                                <ItemStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Hsy_pty_cd" HeaderText="Profit Center">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Hsy_from_dt" DataFormatString="{0:d}" HeaderText="From Date">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Hsy_to_dt" DataFormatString="{0:d}" HeaderText="To Date">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Hsy_val" HeaderText="Value" />
                                            <asp:BoundField DataField="Hsy_cre_dt" DataFormatString="{0:d}" 
                                                HeaderText="Create Date" />
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
                        </div>
                        <div style="float: left; width: 100%; background-color: #FFFFFF; font-size: 9px;">
                            <asp:Panel runat="server" BackColor="White" Width="50%" BorderColor="#3399FF" BorderStyle="Solid"
                                BorderWidth="3px" ID="Panel_pcCommClone">
                                <div style="float: left; width: 100%;">
                                    <div style="padding: 0.5px; float: left; width: 99%; text-align: right;">
                                        
                                        
                                            <asp:Button ID="btnProcessClone" runat="server" Text="Process" CssClass="Button"
                                                 ValidationGroup="clone" onclick="btnProcessClone_Click" />
                                            <asp:Button ID="btnCloneClear" runat="server" Text="Clear" CssClass="Button" 
                                                onclick="btnCloneClear_Click" />
                                            <asp:Button ID="btnCloneCLOSE" runat="server" Text="Close" CssClass="Button" />
                                        
                                    </div>
                                </div>                                     
                              
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 25%;">
                                        <asp:Label ID="Label7" runat="server" Text="Parameter Code" Font-Bold="False"></asp:Label>
                                    </div>
                                    <div style="float: left; width: 30%;">
                                        <asp:TextBox ID="txtCloneParaCode" runat="server" CssClass="TextBoxUpper" Width="85%"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 40%;">
                                        <asp:ImageButton ID="ImgBtnCloneCode" runat="server" 
                                            ImageUrl="~/Images/icon_search.png" onclick="ImgBtnCloneCode_Click" />
                                    </div>
                                </div>
                                <div style="float: left; width: 100%;">                                   
                                 
                                    <div style="float: left; width: 25%;">
                                        <asp:Label ID="Label15" runat="server" Text="Profit Center" Font-Bold="False"></asp:Label>
                                    </div>
                                    <div style="float: left; width: 30%;">
                                        <asp:TextBox ID="txtClonePC" runat="server" CssClass="TextBoxUpper" Width="85%"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 40%;">
                                    </div>
                                       </div>
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 25%;">
                                        <asp:Label ID="Label16" runat="server" Text="Clone Profit Centers" Font-Bold="False"></asp:Label>
                                    </div>
                                    <div style="float: left; width: 30%;">
                                        <asp:TextBox ID="txtCloneAddPc" runat="server" CssClass="TextBoxUpper" Width="85%"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 5%;">
                                        <asp:ImageButton ID="ImgBtnCloneAdd" runat="server" ImageUrl="~/Images/download_arrow_icon.png"
                                            Width="100%" onclick="ImgBtnCloneAdd_Click" />
                                    </div>
                                    <div style="float: left; width: 30%;">
                                    </div>
                                </div>
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 25%;">
                                        &nbsp;&nbsp;</div>
                                    <div style="float: left; width: 25%;">
                                        <asp:Panel ID="Panel15" runat="server" CssClass="PanelHeader" 
                                            HorizontalAlign="Center"> Profit Centers
                                        </asp:Panel>
                                        <asp:Panel ID="Panel14" runat="server" Height="140px" ScrollBars="Vertical" 
                                            GroupingText=" " BorderColor="#3366FF" BorderWidth="1px">
                                       
                                        <asp:GridView ID="grvClonePc" runat="server" ShowHeaderWhenEmpty="True" CssClass="GridView"
                                            Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" ShowHeader="False">
                                            <EditRowStyle BackColor="#2461BF" />
                                            <EmptyDataTemplate>
                                                <div style="width: 100%; text-align: center;">
                                                    No data found
                                                </div>
                                            </EmptyDataTemplate>
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                                        <asp:ImageButton ID="ImgDelClone" runat="server" ImageUrl="~/Images/Delete.png" CommandName="Delete"
                                                            />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
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
                                </div>
                                <div style="float: left; width: 100%;">
                                
                                    &nbsp;</div>
                            </asp:Panel>
                            <asp:DragPanelExtender ID="Panel_pcCommClone_DragPanelExtender" runat="server" 
                                DragHandleID="Panel_pcCommClone" Enabled="True" 
                                TargetControlID="Panel_pcCommClone">
                            </asp:DragPanelExtender>
                        </div>
                <asp:ModalPopupExtender ID="ModalPopupClonePara" runat="server" ClientIDMode="Static"
                    CancelControlID="btnCloneCLOSE" BackgroundCssClass="modalBackground" PopupControlID="Panel_pcCommClone"
                    TargetControlID="btnHiddencLN" DynamicServicePath="" Enabled="True">
                </asp:ModalPopupExtender>
                <div style="float: left; width: 100%; background-color: #FFFFFF; font-size: 9px;">
                            <asp:Panel runat="server" BackColor="White" Width="70%" BorderColor="#3399FF" BorderStyle="Solid"
                                BorderWidth="3px" ID="Panel_VeiwPara">
                                <div style="float: left; width: 100%;">
                                    <div style="padding: 0.5px; float: left; width: 99%; text-align: right;">
                                        
                                        
                                            <asp:Button ID="btnClearPopUp" runat="server" Text="Clear" CssClass="Button" onclick="btnClearPopUp_Click" 
                                                />
                                            <asp:Button ID="btnClosePopUp" runat="server" Text="Close" CssClass="Button" />
                                        
                                    </div>
                                </div>                                     
                              
                                <div style="padding: 1.0px; float: left; width: 100%;">
                                <div  style="float: left; width: 5%;">
                                    &nbsp;</div>
                                    <div style="float: left; width: 20%;">
                                        <asp:Label ID="Label8" runat="server" Text="Parameter Code" Font-Bold="False"></asp:Label>
                                    </div>
                                    <div style="float: left; width: 25%;">
                                        <asp:TextBox ID="txtParaCodePopUp" runat="server" CssClass="TextBoxUpper" Width="85%"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 10%;">
                                        <asp:ImageButton ID="ImgVeiwParamCd" runat="server" 
                                            ImageUrl="~/Images/icon_search.png" onclick="ImgVeiwParamCd_Click" />
                                    </div>
                                    <div style="float: left; width: 30%;">
                                        <asp:CheckBox ID="checkAllCodesPopUp" runat="server" Font-Bold="True" 
                                            Text="All Parameters" />
                                    </div>
                                </div>

                                <div style="padding: 1.0px; float: left; width: 100%;">
                                   <div  style="float: left; width: 5%;">
                                    &nbsp;
                                    </div>
                                    <div style="float: left; width: 20%;">
                                        <asp:Label ID="Label10" runat="server" Text="Profit Center" Font-Bold="False"></asp:Label>
                                    </div>
                                    <div style="float: left; width: 25%;">
                                        <asp:TextBox ID="txtParaPc_PopUp" runat="server" CssClass="TextBoxUpper" 
                                            Width="85%"></asp:TextBox>
                                    </div>
                                    <div style="float: left; width: 10%;">
                                       
                                        &nbsp;</div>
                                    <div style="float: left; width: 30%;">
                                        <asp:Button ID="btnVeiwPopUp" runat="server" CssClass="Button" Text="Veiw" 
                                            ValidationGroup="viewPara" onclick="btnVeiwPopUp_Click" />

                                               &nbsp;
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                            ErrorMessage="Profit center required" ControlToValidate="txtParaPc_PopUp" 
                                               ForeColor="Red" ValidationGroup="viewPara"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div style="padding: 1.0px; float: left; width: 100%;">
                                <div  style="float: left; width: 5%;">
                                    &nbsp;
                                    </div>
                                    <div style="float: left; width: 20%;">
                                        &nbsp;
                                    </div>
                                       <div style="float: left; width: 25%;">
                                       
                                    </div>
                                </div>

                                <div style="float: left; width: 100%;">
                                   
                                    <div style="float: left; width: 100%;">
                                        <asp:Panel ID="Panel17" runat="server" CssClass="PanelHeader" 
                                            HorizontalAlign="Center"> 
                                        </asp:Panel>
                                        <asp:Panel ID="Panel18" runat="server" Height="140px" ScrollBars="Both" 
                                            GroupingText=" " BorderColor="#3366FF" BorderWidth="1px">
                                       <asp:GridView ID="grvVeiwParaPopUp" runat="server" CellPadding="4" CssClass="GridView"
                                        ForeColor="#333333" AutoGenerateColumns="False" Width="100%">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="Hsy_cd" HeaderText="Code">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Hsy_desc" HeaderText="Code Description">
                                                <ItemStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Hsy_pty_cd" HeaderText="Profit Center">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Hsy_from_dt" DataFormatString="{0:d}" HeaderText="From Date">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Hsy_to_dt" DataFormatString="{0:d}" HeaderText="To Date">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Hsy_val" HeaderText="Value" />
                                            <asp:BoundField DataField="Hsy_cre_dt" DataFormatString="{0:d}" 
                                                HeaderText="Create Dt." />
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
                                </div>
                                <div style="float: left; width: 100%;">
                                
                                    &nbsp;</div>
                            </asp:Panel>
                            <asp:DragPanelExtender ID="Panel_VeiwPara_DragPanelExtender" runat="server" 
                                DragHandleID="Panel_VeiwPara" Enabled="True" TargetControlID="Panel_VeiwPara">
                            </asp:DragPanelExtender>
                        </div>
                         <asp:ModalPopupExtender ID="ModalPopupVeiwPara" runat="server" ClientIDMode="Static"
                    CancelControlID="btnClosePopUp" BackgroundCssClass="modalBackground" PopupControlID="Panel_VeiwPara"
                    TargetControlID="btnHidnVeiwPara" DynamicServicePath="" Enabled="True" Y="-2">
                </asp:ModalPopupExtender>
                        <div style="display: none">
                            <asp:Button ID="btnHidn" runat="server" Text="code" OnClick="btnHidn_Click" />
                             <asp:Button ID="btnHiddencLN" runat="server" Text="HIDDEN_CLONE" />
                              <asp:Button ID="btnHidnVeiwPara" runat="server" Text="HIDDEN_CLONE" />
                               <asp:Button ID="btnModPopBlur1" runat="server" Text="code" 
                                onclick="btnModPopBlur1_Click" />
                                 <asp:Button ID="btnModPopBlur2" runat="server" Text="code" onclick="btnModPopBlur2_Click" />
                        </div>
                         
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
