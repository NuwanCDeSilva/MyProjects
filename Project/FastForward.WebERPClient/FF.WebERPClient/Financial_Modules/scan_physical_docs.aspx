<%@ Page Title="Scan/Physical Documents Receive Details" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="scan_physical_docs.aspx.cs" Inherits="FF.WebERPClient.Financial_Modules.scan_physical_docs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
    function ConfirmDelete(button) {

        var ans = confirm('Do you want to Delete Previously Processed Data ?');

        if (ans == true || ans == 1) {

            var bt = document.getElementById(button);
            bt.click();
            return true;

        }
        else {
           
            return false; 
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
    function onblurFire(e, buttonid) {
        var evt = e ? e : window.event;
        var bt = document.getElementById(buttonid);
        if (bt) {

            bt.click();
            return false;


        }
    }
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

    </script>
    <asp:UpdatePanel ID="updtMainPnl" runat="server">
        <ContentTemplate>
            <div id="divMain" style="color: Black;">
            </div>
            <div style="float: left; width: 100%;">
                <asp:Panel ID="Panel1" runat="server">
                    <div style="float: left; width: 100%;">
                        <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Height="600px"
                            Width="100%">
                            <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="::Document Details::">
                                <ContentTemplate>
                                    <div style="float: left; width: 100%; font-size: smaller;">
                                        <div style="float: left; width: 25%;">
                                            <asp:Panel ID="Panel2" runat="server" GroupingText="Week selection">
                                                <div style="float: left; width: 100%;">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPC"
                                                        ErrorMessage="Enter Profit center" ForeColor="Red" SetFocusOnError="True" ValidationGroup="pc"></asp:RequiredFieldValidator></div>
                                                <div style="float: left; width: 100%;">
                                                    <div style="float: left; width: 30%;">
                                                        <asp:Label ID="Label1" runat="server" Text="Profit center" Font-Bold="True"></asp:Label></div>
                                                    <div style="float: left; width: 69%;">
                                                        <asp:TextBox ID="txtPC" runat="server" CssClass="TextBox" 
                                                            OnTextChanged="txtPC_TextChanged" Enabled="False" Width="90px"></asp:TextBox>
                                                        &nbsp;<asp:ImageButton ID="imgbtnSearchProfitCenter" runat="server" ImageUrl="~/Images/icon_search.png" OnClick="imgbtnSearchProfitCenter_Click"/>
                                                    </div>
                                                </div>
                                                <div style="float: left; width: 100%;">
                                                    <div style="float: left; width: 30%;">
                                                        <asp:Label ID="Label2" runat="server" Text="Month" Font-Bold="True"></asp:Label></div>
                                                    <div style="float: left; width: 69%;">
                                                        <asp:TextBox ID="txtMonthYear_" runat="server" AutoPostBack="True" CssClass="TextBox"
                                                            OnTextChanged="txtMonthYear_TextChanged" ValidationGroup="pc" Width="90px"></asp:TextBox><asp:CalendarExtender
                                                                ID="CalendarExtender_" runat="server" Enabled="True" Format="MM/yyyy" TargetControlID="txtMonthYear_">
                                                            </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                                <div style="float: left; width: 100%;">
                                                    <div style="float: left; width: 30%;">
                                                        <asp:Label ID="Label3" runat="server" Text="Week" Font-Bold="True"></asp:Label></div>
                                                    <div style="float: left; width: 69%;">
                                                        <asp:DropDownList ID="ddlWeek" runat="server" AutoPostBack="True" CssClass="ComboBox"
                                                            OnSelectedIndexChanged="ddlWeek_SelectedIndexChanged" Width="93px" 
                                                            ValidationGroup="pc">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div style="float: left; width: 100%;">
                                                    <div style="float: left; width: 15%;">
                                                        <asp:Label ID="Label4" runat="server" Text="From: "></asp:Label></div>
                                                    <div style="float: left; width: 20%;">
                                                        <asp:Label ID="lblFrmdtWk" runat="server" ForeColor="Blue" Width="100%"></asp:Label></div>
                                                    <div style="float: left; width: 20%;">
                                                        &#160;</div>
                                                    <div style="float: left; width: 10%;">
                                                        <asp:Label ID="Label5" runat="server" Text="To: "></asp:Label></div>
                                                    <div style="float: left; width: 20%;">
                                                        <asp:Label ID="lblTodtWk" runat="server" ForeColor="Blue" Width="100%"></asp:Label></div>
                                                </div>
                                                <div style="padding: 2.5px; float: left; width: 100%;">
                                                    <div style="padding: 1.0px; float: left; width: 100%; ">                                                        
                                                         <asp:Button ID="btnProcess" runat="server" Text="Process" CssClass="Button" OnClick="btnProcess_Click"
                                                                ValidationGroup="pc" Width="97%" />                                                                                                         
                                                     </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div style="float: left; width: 50%;">
                                            <div style="float: left; width: 100%; background-color: #0066FF;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 100%;">
                                                <asp:Panel ID="Panel4" runat="server" Height="133px" ScrollBars="Both">
                                                    <asp:GridView ID="grvRem" runat="server" CellPadding="4" ShowHeaderWhenEmpty="True"
                                                        ForeColor="#333333" Width="99%" AutoGenerateColumns="False">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Date" DataField="Date" />
                                                            <asp:BoundField HeaderText="Prv. Excess" DataField="PrevExcessRem" />
                                                            <asp:BoundField HeaderText="Excess Rem." DataField="ExcessRem" />
                                                            <asp:BoundField HeaderText="Amt Remited" DataField="AmtRemited" />
                                                            <asp:BoundField HeaderText="CIH" DataField="CashIH" />
                                                            <asp:BoundField HeaderText="Difference" DataField="Difference" />
                                                        </Columns>
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <EmptyDataTemplate>
                                                            <div style="width: 100%; text-align: center;">
                                                                No data found
                                                            </div>
                                                        </EmptyDataTemplate>
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
                                        <div style="float: left; width: 24%;">
                                            <div style="float: left; width: 100%; background-color: #0066FF;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 100%;">
                                                <asp:Panel ID="Panel5" runat="server" Height="133px" ScrollBars="Both">
                                                    <asp:GridView ID="grvSellectTool" runat="server" ShowHeaderWhenEmpty="True" CellPadding="4"
                                                        ForeColor="#333333" Width="99%">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <EmptyDataTemplate>
                                                            <div style="width: 100%; text-align: center;">
                                                                No data found
                                                            </div>
                                                        </EmptyDataTemplate>
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
                                    </div>
                                    <div style="padding: 5.0px; float: left; width: 100%; font-size: smaller;">
                                        <div style="float: left; width: 20%;">
                                            <asp:DropDownList ID="ddlDocTypes" runat="server" AutoPostBack="True" CssClass="ComboBox"
                                                OnSelectedIndexChanged="ddlDocTypes_SelectedIndexChanged">
                                                <asp:ListItem>ALL</asp:ListItem>
                                                <asp:ListItem Value="CHEQUE">CHEQUES</asp:ListItem>
                                                <asp:ListItem>CS SETTLEMENT-CASH</asp:ListItem>
                                                <asp:ListItem>CS SETTLEMENT-CHEQUES</asp:ListItem>
                                                <asp:ListItem>ADVANCE RECEIPTS</asp:ListItem>
                                                <asp:ListItem>CREDIT CARD</asp:ListItem>
                                                <asp:ListItem>COLLECTION BONUS</asp:ListItem>
                                                <asp:ListItem>BANK DEPOSIT SLIP</asp:ListItem>
                                                <asp:ListItem>PRODUCT BONUS</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div style="float: left; width: 10%;">
                                            <asp:Button ID="btnVeiw" runat="server" Text="View" CssClass="Button" Width="70%"
                                                OnClick="btnVeiw_Click" ValidationGroup="pc" /></div>
                                        <div style="float: left; width: 15%;">
                                            <asp:Button ID="btnExtraDocs" runat="server" Text="Extra Docs >>" CssClass="Button"
                                                OnClick="btnExtraDocs_Click" ValidationGroup="pc" /></div>
                                        <div style="float: left; width: 55%;">
                                            <div style="float: left; width: 74%;">
                                                &nbsp;&nbsp;</div>
                                            <div style="float: left; width: 25%;">
                                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="Button" OnClick="btnSave_Click" />&nbsp;&nbsp;<asp:Button
                                                    ID="btnClear" runat="server" Text="Clear" CssClass="Button" OnClick="btnClear_Click" /></div>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; font-size: smaller; background-color: #FFFFFF;">
                                        <asp:Panel ID="Panel3" runat="server" ScrollBars="Both" Height="375px" 
                                            BackColor="White">
                                            <asp:GridView ID="grvDocDetails" runat="server" CellPadding="4" CssClass="GridView"
                                                ForeColor="#333333" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" OnRowDataBound="grvDocDetails_RowDataBound"
                                                DataKeyNames="GRDD_DOC_VAL">
                                                <AlternatingRowStyle BackColor="White" />
                                                  <EmptyDataTemplate>
                                                    <div style="width: 100%; text-align: center;">
                                                        No data found
                                                    </div>
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Date">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtgrvDate" runat="server"></asp:TextBox></EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server"></asp:Label><asp:TextBox ID="txtgrvDate" Text='<%# Bind("GRDD_DT") %>'
                                                                runat="server" CssClass="TextBox" Width="80px" Font-Size="X-Small"></asp:TextBox><asp:CalendarExtender
                                                                    ID="txtgrvDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True"
                                                                    TargetControlID="txtgrvDate">
                                                                </asp:CalendarExtender>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="80px" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Description" DataField="GRDD_DOC_DESC">
                                                    <HeaderStyle Width="165px" />
                                                    <ItemStyle Font-Size="XX-Small" Width="165px" Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Ref #">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2" runat="server"></asp:Label><asp:TextBox ID="txtgrvRefNo" Text='<%# Bind("GRDD_DOC_REF") %>'
                                                                runat="server" CssClass="TextBox" Width="80px" Font-Size="X-Small"></asp:TextBox></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Bank" DataField="GRDD_DOC_BANK" />
                                                    <asp:BoundField HeaderText="Amount" DataField="GRDD_SYS_VAL" />
                                                    <asp:TemplateField HeaderText="Scan Received">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label3" runat="server"></asp:Label><asp:CheckBox ID="chekgrvScnRecive"
                                                                Checked='<%# Eval("GRDD_SCAN_RCV").ToString() =="1"? true:false%>' runat="server" /></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sun Upload">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label4" runat="server"></asp:Label><asp:CheckBox ID="chekgrvSunUp"
                                                                Checked='<%# Eval("GRDD_SUN_UPLOAD").ToString() =="1"? true:false%>' runat="server"
                                                                Enabled="True" /></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Is Realized">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox></EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label5" runat="server"></asp:Label><asp:CheckBox ID="chekgrvIsRealiz"
                                                                Checked='<%# Eval("GRDD_IS_REALIZED").ToString() =="1"? true:false%>' runat="server" /></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Realize Date">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox></EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label6" runat="server"></asp:Label><asp:TextBox ID="txtgrvRealizDt"
                                                                Text='<%# Bind("GRDD_REALIZED_DT") %>' runat="server" CssClass="TextBox" Width="80px" Font-Size="X-Small"></asp:TextBox><asp:CalendarExtender
                                                                    ID="txtgrvRealizDt_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True"
                                                                    TargetControlID="txtgrvRealizDt">
                                                                </asp:CalendarExtender>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Physically Received">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox></EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label7" runat="server"></asp:Label><asp:CheckBox ID="chekPhyReceive"
                                                                Checked='<%# Eval("GRDD_DOC_RCV").ToString() =="1"? true:false%>' runat="server"
                                                                AutoPostBack="True" OnCheckedChanged="chekPhyReceive_CheckedChanged" /></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Physical Value">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox></EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label8" runat="server"></asp:Label><asp:TextBox ID="txtgrvPhyVal"
                                                                Text='<%# Eval("GRDD_DOC_VAL") %>' runat="server" CssClass="TextBoxNumeric" Width="75px" Font-Size="X-Small" onkeypress="return isNumberKeyAndDot(event,this.value)"></asp:TextBox></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Deposit Bank Code">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox></EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label9" runat="server"></asp:Label><asp:DropDownList ID="ddlDepBnkCd"
                                                                runat="server" CssClass="ComboBox" Width="60px" Font-Size="X-Small">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cheque Bank">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox></EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label10" runat="server"></asp:Label><asp:TextBox ID="txtgrvBankCd"
                                                                Text='<%# Bind("GRDD_DOC_BANK_CD") %>' runat="server" CssClass="TextBox" Width="80px" Font-Size="X-Small"></asp:TextBox></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Branch">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox></EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label11" runat="server"></asp:Label><asp:TextBox ID="txtgrvBranch"
                                                                runat="server" Text='<%# Bind("GRDD_DOC_BANK_BRANCH") %>' CssClass="TextBox" Width="80px" Font-Size="X-Small"></asp:TextBox></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox12" runat="server"></asp:TextBox></EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label12" runat="server"></asp:Label><asp:TextBox ID="txtgrvRemk" Text='<%# Bind("GRDD_RMK") %>'
                                                                runat="server" CssClass="TextBox" Font-Size="X-Small"></asp:TextBox></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Seq#" Visible="False">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox13" runat="server" Text='<%# Bind("GRDD_SEQ") %>'></asp:TextBox></EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSeqNo" runat="server" Text='<%# Bind("GRDD_SEQ") %>'></asp:Label></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Doc Type" Visible="False">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox14" runat="server" Text='<%# Bind("GRDD_DOC_TP") %>'></asp:TextBox></EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDocType" runat="server" Text='<%# Bind("GRDD_DOC_TP") %>'></asp:Label></ItemTemplate>
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
                                </ContentTemplate>
                            </asp:TabPanel>
                        </asp:TabContainer>
                    </div>
                </asp:Panel>
            </div>
            <div style="float: left; width: 100%; background-color: #FFFFFF;">
                <asp:Panel ID="Panel_popUpExtraDocs" runat="server" Width="80%" BackColor="White"
                    BorderColor="#66CCFF" BorderStyle="Groove">
                    <div style="float: left; width: 96%; background-color: #0066FF; height: 18px;">
                        <asp:Label ID="Label13" runat="server" Text="Extra Physically Received Documents"
                            Font-Bold="True" ForeColor="White"></asp:Label>
                    </div>
                    <div style="float: left; width: 4%; background-color: #0066FF; text-align: right;  height: 18px;">
                        <asp:ImageButton ID="ImgBtnClose" runat="server" ImageUrl="~/Images/icon_reject2.PNG" />
                    </div>
                    <div style="float: left; width: 96%;">
                    
                        &nbsp;</div>
                    <div style="float: left; width: 60%; ">
                        <div style="float: left; width: 100%;">
                            <div style="float: left; width: 100%; background-color: #6699FF;">
                                <div style="float: left; width: 79%;">
                                    &nbsp;<asp:CheckBox ID="chekShortBS" runat="server" ForeColor="White" 
                                        Text="Short Banking Settlement" Font-Bold="True" />
                                </div>
                                <div style="float: left; width: 15%;">
                                    <asp:Button ID="btnExtraDocFind" runat="server" Text="Find > >" CssClass="Button"
                                        OnClick="btnExtraDocFind_Click" />
                                </div>
                            </div>
                             <div style="padding: 2.5px; float: left; width: 100%;">
                                
                                 &nbsp;</div>
                            <div style="float: left; width: 100%;">
                                <div style="float: left; width: 20%;">
                                    Document Type
                                </div>
                                <div style="float: left; width: 40%;">
                                    <asp:DropDownList ID="ddlPopUpDocTp" runat="server" CssClass="ComboBox" OnSelectedIndexChanged="ddlPopUpDocTp_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div style="float: left; width: 100%;">
                                <div style="float: left; width: 20%;">
                                    Date
                                </div>
                                <div style="float: left; width: 30%;">
                                    <asp:TextBox ID="txtExtraDocDate" runat="server" CssClass="TextBox" 
                                        Width="100px"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="MM/yyyy"
                                        TargetControlID="txtExtraDocDate">
                                    </asp:CalendarExtender>
                                </div>
                                <div style="float: left; width: 39%;">
                                </div>
                            </div>
                            <div style="float: left; width: 100%;">
                                <div style="float: left; width: 20%;">
                                    Bank
                                </div>
                                <div style="float: left; width: 30%;">
                                    <asp:DropDownList ID="ddlPopUpBank" runat="server" CssClass="ComboBox" 
                                        Width="108px">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div style="float: left; width: 100%;">
                                <div style="float: left; width: 20%;">
                                    Amount
                                </div>
                                <div style="float: left; width: 30%;">
                                    <asp:TextBox ID="txtExtraDocAmt" runat="server" CssClass="TextBox" 
                                        Width="100px" onkeypress="return isNumberKeyAndDot(event,this.value)"></asp:TextBox>
                                </div>
                            </div>
                            <div style="float: left; width: 100%;">
                                <div style="float: left; width: 20%;">
                                    Doc. Reference
                                </div>
                                <div style="float: left; width: 30%;">
                                    <asp:TextBox ID="txtExtraDocRef" runat="server" CssClass="TextBox" 
                                        Width="100px"></asp:TextBox>
                                    <asp:HiddenField ID="HiddenField_seq" runat="server" />
                                </div>
                            </div>
                            <div style="float: left; width: 100%;">
                                <div style="float: left; width: 20%;">
                                    Remarks
                                </div>
                                <div style="float: left; width: 70%;">
                                    <asp:TextBox ID="txtExtraDocRemks" runat="server" CssClass="TextBox" MaxLength="100"
                                        Width="100%"></asp:TextBox>
                                </div>
                            </div>
                            <div style="padding: 2.0px; float: left; width: 100%;">
                            </div>
                            <div style="float: left; width: 100%;">
                                <div style="float: left; width: 20%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 70%;">
                                    <asp:Button ID="btnExtraDocAdd" runat="server" CssClass="Button" Text="ADD" 
                                        OnClick="btnExtraDocAdd_Click" Font-Bold="True" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div style="float: left; width: 39%; background-color: #FFFFFF;">
                        <asp:Panel ID="PanelShortBanking" runat="server" GroupingText="Short Banking" Enabled="False">
                            <div style="float: left; width: 100%;">
                                <div style="float: left; width: 50%;">
                                    Date :
                                    <asp:TextBox ID="txtGridExtraDocMonYr" runat="server" CssClass="TextBox" AutoPostBack="True"
                                        OnTextChanged="txtGridExtraDocMonYr_TextChanged" Width="80px"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="MM/yyyy"
                                        TargetControlID="txtGridExtraDocMonYr">
                                    </asp:CalendarExtender>
                                </div>
                                <div style="float: left; width: 49%;">
                                    Week :
                                    <asp:DropDownList ID="ddExtraDoclWeek" runat="server" AutoPostBack="True" CssClass="ComboBox"
                                        OnSelectedIndexChanged="ddExtraDoclWeek_SelectedIndexChanged" Width="80px">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div style="float: left; width: 100%;">
                                <div style="float: left; width: 15%;">
                                    <asp:Label ID="Label14" runat="server" Text="From: "></asp:Label>
                                </div>
                                <div style="float: left; width: 20%;">
                                    <asp:Label ID="lblExtraFromDtWk" runat="server" ForeColor="Blue" Width="100%"></asp:Label>
                                </div>
                                <div style="float: left; width: 20%;">
                                    &nbsp;</div>
                                <div style="float: left; width: 10%;">
                                    <asp:Label ID="Label16" runat="server" Text="To: "></asp:Label>
                                </div>
                                <div style="float: left; width: 20%;">
                                    <asp:Label ID="lblExtraToDtWk" runat="server" ForeColor="Blue" Width="100%"></asp:Label>
                                </div>
                            </div>
                            <div style="float: left; width: 100%;">
                                <asp:Panel ID="Panel6" runat="server" Height="100px" ScrollBars="Vertical">
                                    <asp:GridView ID="grvPopUpExtraDocs" runat="server" AutoGenerateColumns="False" Width="99%"
                                        OnSelectedIndexChanged="grvPopUpExtraDocs_SelectedIndexChanged" 
                                        CssClass="GridView" ShowHeaderWhenEmpty="True" CellPadding="4" 
                                        ForeColor="#333333" GridLines="None">
                                        <EditRowStyle BackColor="#2461BF" />
                                        <EmptyDataTemplate>
                                                    <div style="width: 100%; text-align: center;">
                                                        No data found
                                                    </div>
                                                </EmptyDataTemplate>
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="True">
                                                <ControlStyle ForeColor="#009933" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:CommandField>
                                            <asp:BoundField HeaderText="Ref #" DataField="grdd_seq" >
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="GRDD_DOC_REF" HeaderText="Doc Reference">
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Amount" DataField="grdd_sys_val" >
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
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
                        </asp:Panel>
                    </div>
                </asp:Panel>
                <%--<asp:ModalPopupExtender ID="ExtraDocs_ModalPopupExtender" runat="server" 
                    DynamicServicePath="" Enabled="True" TargetControlID="Panel_popUpExtraDocs">
                </asp:ModalPopupExtender>--%>
                <asp:ModalPopupExtender ID="ModalPopupExtItem" runat="server" ClientIDMode="Static"
                    BackgroundCssClass="modalBackground" PopupControlID="Panel_popUpExtraDocs" TargetControlID="btnHidden_popup">
                </asp:ModalPopupExtender>
            </div>
            <div style="display: none">
                <asp:Button ID="btnHidden_popup" runat="server" Text="Button" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
