<%@ Page Title="Vehical Registration" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="VehicalRegistration.aspx.cs" Inherits="FF.WebERPClient.General_Modules.VehicalRegistration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function DisplayError() {
            var opt = document.getElementById('<%=HiddenFieldOP.ClientID %>').value;
            if (opt == "OP1") {
                var var1 = document.getElementById('<%=TextBoxLastName.ClientID %>').value;
                var var2 = document.getElementById('<%=TextBoxAdd1.ClientID %>').value;
                var var3 = document.getElementById('<%=TextBoxContact.ClientID %>').value;
                var var4 = document.getElementById('<%=HiddenFieldRegCount.ClientID %>').value;
                if (var4 == "0") {
                    alert('No reciept to save');
                    return false;
                }
                else if (var1 != "" && var1 != null && var2 != "" && var2 != null && var3 != "" && var3 != null) {
                    return confirm('Are you sure?');
                }

                else {
                    alert('Please fill required information before save');
                    return false;
                }
            }
            else if (opt == "OP2") {
                var var1 = document.getElementById('<%=TextBoxRMVSend.ClientID %>').value;
                var var2 = document.getElementById('<%=HiddenFieldRMVCount.ClientID %>').value;
                if (var2 == "0") {
                    alert('No reciept to save');
                    return false;
                }
                else if (var1 != "" && var1 != null) {
                    return confirm('Are you sure?');
                }
                else {
                    alert('Please fill required information before save');
                    return false;
                }
            }
            else if (opt == "OP3") {
                var var1 = document.getElementById('<%=TextBoxAssRegNumber.ClientID %>').value;
                var var2 = document.getElementById('<%=TextBoxRegDate.ClientID %>').value;
                var var3 = document.getElementById('<%=HiddenFieldAssRegCount.ClientID %>').value;
                if (var3 == "0") {
                    alert('No reciept to save');
                    return false;
                }
                else if (var1 != "" && var1 != null && var2 != "" && var2 != null && CheckFormat()) {
                    CreateNumber();
                    return confirm('Are you sure?');
                }
                else if (var3 == "0") {
                    alert('No data to save');
                    return false;
                }
                else if (!CheckFormat()) {
                    return false;
                }
                else {
                    alert('Please fill required information before save');
                    return false;
                }
            }
            else if (opt == "OP4") {
                var var1 = document.getElementById('<%=TextBoxCustomerSend.ClientID %>').value;
                var var2 = document.getElementById('<%=HiddenFieldScusCount.ClientID %>').value;
                if (var2 == "0") {
                    alert('No reciept to save');
                    return false;
                }
                else if (var1 != "" && var1 != null) {
                    return confirm('Are you sure?');
                }

                else {
                    alert('Please fill required information before save');
                    return false;
                }
            }
            else if (opt == "OP5") {
                var var1 = document.getElementById('<%=TextBoxJobCloseDt.ClientID %>').value;
                var var2 = document.getElementById('<%=HiddenFieldClsCount.ClientID %>').value;
                if (var2 == "0") {
                    alert('No reciept to save');
                    return false;
                }
                else if (var1 != "" && var1 != null) {
                    return confirm('Are you sure?');
                }
                else {
                    alert('Please fill required information before save');
                    return false;
                }
            }

        }
        function ChangeHiddenField() {

            var var1 = document.getElementById('<%=TextBoxCompany.ClientID %>').value;
            var var2 = document.getElementById('<%=TextBoxLocation.ClientID %>').value;
            if ((var1 != "" && var1 != null) || (var2 != null && var2 != "")) {
                document.getElementById('<%=HiddenFieldChangeComLoc.ClientID %>').value = "1";
                document.getElementById('<%=LinkButtonTem.ClientID %>').click();
                //PageMethods.LoadCombos();
            }
        }

        function PrintConfirm() {
            var opt = document.getElementById('<%=HiddenFieldOP.ClientID %>').value;

            return true;
        }

        function GetFileName() {
            var filePath = document.getElementById('<%=FileUpload1.ClientID %>').value;
            if (filePath.indexOf('.') == -1)
                return false; var validExtensions = new Array();
            var ext = filePath.substring(filePath.lastIndexOf('.') + 1).toLowerCase();
            validExtensions[0] = 'jpg';
            validExtensions[1] = 'jpeg'; validExtensions[2] = 'bmp';
            validExtensions[3] = 'png'; validExtensions[4] = 'gif';
            validExtensions[5] = 'tif'; validExtensions[6] = 'tiff';

            for (var i = 0; i < validExtensions.length; i++) {
                if (ext == validExtensions[i]) {
                    document.getElementById('<%=HiddenFieldFilename.ClientID %>').value = filePath;
                    return true;
                }
            }
            alert('The file extension ' + ext.toUpperCase() + ' is not allowed!');
            document.getElementById('<%=HiddenFieldFilename.ClientID %>').value = "0";
            return false;
        }

        function CheckFormat() {
            var val = document.getElementById('<%=TextBoxR3.ClientID %>').value;
            var val1 = document.getElementById('<%=TextBoxR4.ClientID %>').value;
            var val2 = document.getElementById('<%=TextBoxR5.ClientID %>').value;

            if (val2 == "" || val2 == null) {
                if ((val != "" && val != null) && (val1 != "" && val1 != null)) {
                    alert("Please dont empty last textbox");
                    return false;
                }
            }
            else {
                CreateNumber();
                return true;
            }
        }
        function CreateNumber() {
            var val = document.getElementById('<%=TextBoxR1.ClientID %>').value;
            var val1 = document.getElementById('<%=TextBoxR2.ClientID %>').value;
            var val2 = document.getElementById('<%=TextBoxR3.ClientID %>').value;
            var val3 = document.getElementById('<%=TextBoxR4.ClientID %>').value;
            var val4 = document.getElementById('<%=TextBoxR5.ClientID %>').value;
            var val5 = document.getElementById('<%=TextBoxR6.ClientID %>').value;
            var val6 = document.getElementById('<%=TextBoxR7.ClientID %>').value;
            var val7 = document.getElementById('<%=TextBoxR8.ClientID %>').value;
            var val8 = document.getElementById('<%=TextBoxR9.ClientID %>').value;
            document.getElementById('<%=TextBoxAssRegNumber.ClientID %>').value = val.toUpperCase() + val1.toUpperCase() + " " + val2.toUpperCase() + val3.toUpperCase() + val4.toUpperCase() + "-" + val5.toUpperCase() + val6.toUpperCase() + val7.toUpperCase() + val8.toUpperCase();
        }

        //                  function UpdateCombo() {
        //                      var val = document.getElementById('<%=TextBoxIDNo.ClientID %>').value;
        //                      var val1 = document.getElementById('<%=DropDownListIdType.ClientID %>').value;
        //                      var _bool = PageMethods.IsValidNIC(val);
        //                      if (!_bool && val1 == "NIC") {
        //                          alert("NIC format wrong");
        //                      }
        //                  }
    </script>
    <style type="text/css">
        .fixWidth tr td
        {
            width: 45%;
        }
        
        .Panel legend
        {
            color: Blue;
        }
        .Label
        {
            border: none;
            background-color: white;
        }
        
        
        #nav
        {
            width: 100%;
            float: left;
            margin: 0 0 3em 0;
            padding: 0;
            list-style: none;
            background-color: #f2f2f2;
            border-bottom: 1px solid #ccc;
            border-top: 1px solid #ccc;
        }
        #nav li
        {
            float: left;
        }
        #nav li a
        {
            display: block;
            padding: 1px 30px;
            text-decoration: none;
            font-weight: bold;
            color: #069;
            border-right: 1px solid #ccc;
        }
        #nav li a:hover
        {
            color: #c00;
            background-color: #fff;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updtMainPnl" runat="server">
        <ContentTemplate>
            <div style="float: left; width: 100%; color: Black;">
                <asp:HiddenField ID="HiddenFieldFilename" runat="server" Value="0" />
                <asp:HiddenField ID="HiddenFieldRegPrint" runat="server" Value="0" />
                <asp:HiddenField ID="HiddenFieldRMVPrint" runat="server" Value="0" />
                <asp:HiddenField ID="HiddenFieldOP" runat="server" Value="OP1" />
                <asp:HiddenField ID="HiddenFieldRegCount" runat="server" Value="0" />
                <asp:HiddenField ID="HiddenFieldRMVCount" runat="server" Value="0" />
                <asp:HiddenField ID="HiddenFieldAssRegCount" runat="server" Value="0" />
                <asp:HiddenField ID="HiddenFieldScusCount" runat="server" Value="0" />
                <asp:HiddenField ID="HiddenFieldClsCount" runat="server" Value="0" />
                <asp:HiddenField ID="HiddenFieldChangeComLoc" runat="server" Value="0" />
                <div style="height: 22px; text-align: right;" class="PanelHeader">
                    <asp:Button ID="ButtonSave" runat="server" Text="Save" CssClass="Button" OnClick="ButtonSave_Click"
                        OnClientClick="return DisplayError()" />
                    <asp:Button ID="ButtonPrint" runat="server" Text="Print" CssClass="Button" OnClick="ButtonPrint_Click"
                        OnClientClick="return PrintConfirm()" />
                    <asp:Button ID="ButtonClear" runat="server" Text="Clear" CssClass="Button" OnClick="ButtonClear_Click" />
                    <asp:Button ID="ButtonClose" runat="server" Text="Close" CssClass="Button" OnClick="ButtonClose_Click" />
                </div>
                <div>
                    &nbsp;
                </div>
                <div style="float: left; width: 100%; color: Black;">
                    <asp:Panel ID="PanelSelect" runat="server" CssClass="Panel">
                        <div style="float: left; width: 100%; color: Black;">
                            <%--                            <asp:RadioButtonList ID="RadioButtonListOption" runat="server" AutoPostBack="true"
                                RepeatColumns="3" CssClass="fixWidth" OnSelectedIndexChanged="RadioButtonListOption_SelectedIndexChanged">
                                <asp:ListItem Text="1.Filling Application Details" Value="OP1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="2.Send to RMV" Value="OP2"></asp:ListItem>
                                <asp:ListItem Text="3.Assign vehicle Register Number" Value="OP3"></asp:ListItem>
                                <asp:ListItem Text="4.Send to Customer" Value="OP4"></asp:ListItem>
                                <asp:ListItem Text="5.Job Close" Value="OP5"></asp:ListItem>
                            </asp:RadioButtonList>--%>
                            <ul id="nav">
                                <li>
                                    <asp:LinkButton ID="LinkButtonFillApp" runat="server" Text="Filling Application Details"
                                        OnClick="LinkButtonFillApp_Click"></asp:LinkButton></li>
                                <li>
                                    <asp:LinkButton ID="LinkButtonSenRMV" runat="server" Text="Send to RMV" OnClick="LinkButtonSenRMV_Click"></asp:LinkButton></li>
                                <li>
                                    <asp:LinkButton ID="LinkButtonAssReg" runat="server" Text="Assign vehicle Register Number"
                                        OnClick="LinkButtonAssReg_Click"></asp:LinkButton></li>
                                <li>
                                    <asp:LinkButton ID="LinkButtonSenCus" runat="server" Text="Send to Customer" OnClick="LinkButtonSenCus_Click"></asp:LinkButton></li>
                                <li>
                                    <asp:LinkButton ID="LinkButtonJobCls" runat="server" Text="Job Close" OnClick="LinkButtonJobCls_Click"></asp:LinkButton></li>
                            </ul>
                        </div>
                    </asp:Panel>
                </div>
                <div>
                    &nbsp;
                </div>
                <div style="float: left; width: 100%; color: Black;">
                    <asp:Panel ID="Panel1" runat="server" GroupingText=" ">
                        <div style="float: left; width: 50%;">
                            <div style="float: left; width: 30%;">
                                Company
                            </div>
                            <div style="float: left; width: 70%;">
                                <asp:TextBox ID="TextBoxCompany" runat="server" CssClass="TextBox TextBoxUpper" OnTextChanged="TextBoxCompany_TextChanged"
                                    AutoPostBack="true" onblur="ChangeHiddenField()"></asp:TextBox>
                                <asp:ImageButton ID="ImageButtonCompany" runat="server" ImageUrl="~/Images/icon_search.png"
                                    Style="width: 16px" OnClick="ImageButtonCompany_Click" />
                            </div>
                        </div>
                        <div style="float: left; width: 50%;">
                            <div style="float: left; width: 30%;">
                                Profit Center
                            </div>
                            <div style="float: left; width: 70%;">
                                <asp:TextBox ID="TextBoxLocation" runat="server" CssClass="TextBox TextBoxUpper"
                                    OnTextChanged="TextBoxLocation_TextChanged" AutoPostBack="true" onblur="ChangeHiddenField()"></asp:TextBox>
                                <asp:ImageButton ID="ImageButtonLocation" runat="server" ImageUrl="~/Images/icon_search.png"
                                    Style="width: 16px" OnClick="ImageButtonLocation_Click" />
                                <asp:LinkButton ID="LinkButtonTem" runat="server" OnClick="LinkButtonTem_Click"></asp:LinkButton>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div style="float: left; width: 100%; color: Black;">
                    <asp:Panel ID="PanelApplicationDetails" runat="server" GroupingText="" Height="420px"
                        ScrollBars="Auto">
                        <div>
                            &nbsp;
                        </div>
                        <div style="float: left; width: 100%; color: Black;">
                            <%-- 1st row--%>
                            <div style="float: left; width: 100%; color: Black;">
                                <div style="float: left; width: 1%; color: Black;">
                                    &nbsp;
                                </div>
                                <div style="float: left; width: 32%; color: Black; display: none;">
                                    <div style="float: left; width: 30%;">
                                        Select Reciept
                                    </div>
                                    <div style="float: left; width: 70%;">
                                        <asp:DropDownList ID="DropDownListReg" CssClass="ComboBox" runat="server" Width="150px"
                                            AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DropDownListReg_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div style="float: left; width: 1%; color: Black;">
                                    &nbsp;
                                </div>
                                <div style="float: left; width: 48%; color: Black;">
                                    <%--ADDED 2012/09/08--%>
                                    <%--ADVANCED SEARCH SCREEN--%>
                                    <div style="float: left; width: 100%; color: Black;">
                                        <div style="float: left; width: 100%; color: Black;">
                                            <div style="float: left; width: 30%;">
                                                Acc No
                                            </div>
                                            <div style="float: left; width: 70%;">
                                                <asp:TextBox ID="TextBoxAccNo" CssClass="TextBox" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                            <div style="float: left; width: 30%;">
                                                Invoice
                                            </div>
                                            <div style="float: left; width: 70%;">
                                                <asp:TextBox ID="TextBoxInvNo" CssClass="TextBox" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                            <div style="float: left; width: 30%;">
                                                Reciept No
                                            </div>
                                            <div style="float: left; width: 70%;">
                                                <asp:TextBox ID="TextBoRecNo" CssClass="TextBox" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                            <div style="float: left; width: 30%;">
                                                Vehical Number
                                            </div>
                                            <div style="float: left; width: 70%;">
                                                <asp:TextBox ID="TextBoxVehNo" CssClass="TextBox" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                            <div style="float: left; width: 30%;">
                                                Engine Number
                                            </div>
                                            <div style="float: left; width: 70%;">
                                                <asp:TextBox ID="TextBoxEngNo" CssClass="TextBox" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                            <div style="float: left; width: 30%;">
                                                Chassis Number
                                            </div>
                                            <div style="float: left; width: 70%;">
                                                <asp:TextBox ID="TextBoxChassisNo" CssClass="TextBox" runat="server"></asp:TextBox>
                                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                <asp:LinkButton ID="LinkButtonView" runat="server" Text="View" OnClick="LinkButtonView_Click"></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <%--END--%>
                                </div>
                                <div style="float: left; width: 1%; color: Black;">
                                    &nbsp;
                                </div>
                                <div style="float: left; width: 48%; color: Black;">
                                    <asp:Panel ID="Panel123" runat="server" GroupingText=" " Height="100px" ScrollBars="Auto">
                                        <asp:GridView ID="GridViewSearch" runat="server" Width="600px" EmptyDataText="No data found"
                                            ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                                            CssClass="GridView" OnSelectedIndexChanged="GridViewSearch_SelectedIndexChanged"
                                            OnSelectedIndexChanging="GridViewSearch_SelectedIndexChanging">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgSelect" runat="server" CommandName="SELECT" ImageUrl="~/Images/download_arrow_icon.png"
                                                            Height="15px" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="20px" Height="15px" />
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Rec No" DataField="SVRT_REF_NO" />
                                                <asp:BoundField HeaderText="Reg No" DataField="SVRT_VEH_REG_NO" />
                                                <asp:BoundField HeaderText="Inv No" DataField="SVRT_INV_NO" />
                                                <asp:BoundField HeaderText="Chassis" DataField="SVRT_CHASSIS" />
                                                <asp:BoundField HeaderText="Engine" DataField="SVRT_ENGINE" />
                                                 <asp:BoundField HeaderText="Cus Name" DataField="SVRT_LAST_NAME" />
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
                            <asp:Panel ID="PanelRDetails" runat="server" GroupingText="Reciept Details" CssClass="Panel">
                                <div style="float: left; width: 100%; color: Black;">
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 32%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Sale Date
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxRecieptDate" CssClass="TextBox" Enabled="False" runat="server"></asp:TextBox>
                                            <%-- <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                        <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="TextBoxRecieptDate"
                                            PopupButtonID="Image1" Enabled="True" Format="dd/MMM/yyyy">
                                        </asp:CalendarExtender>--%>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 32%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Invoice No
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxInvoiceNo" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 32%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Sales Type
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxSType" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <%-- end of 1st row--%>
                                <%--2nd row--%>
                                <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 32%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Reg. Amount
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxRecieptAmo" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 32%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Claim Amount
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxClaimAmo" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 32%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Sales Amount
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxSaleAmount" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <%--end of 2nd row--%>
                            <%-- 3rd row--%>
                            <asp:Panel ID="PanelCusDe" runat="server" GroupingText="Customer Details" CssClass="Panel">
                                <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 32%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            ID
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:DropDownList ID="DropDownListIdType" CssClass="ComboBox" runat="server">
                                                <asp:ListItem Text="NIC" Value="NIC"></asp:ListItem>
                                                <asp:ListItem Text="PP" Value="PP"></asp:ListItem>
                                                <asp:ListItem Text="DR" Value="DR"></asp:ListItem>
                                                <asp:ListItem Text="BR" Value="BR"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:TextBox ID="TextBoxIDNo" runat="server" CssClass="TextBox TextBoxUpper" Width="85px"
                                                MaxLength="10"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 32%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Cus. Code
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxCusCode" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                                            <asp:ImageButton ID="ImageButtonCusCode" runat="server" ImageUrl="~/Images/icon_search.png"
                                                Style="width: 16px" OnClick="ImageButtonCusCode_Click" />
                                        </div>
                                    </div>
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 32%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Customer Title
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:DropDownList ID="DropDownListCusTitle" CssClass="ComboBox" runat="server">
                                                <asp:ListItem Text="Mr." Value="Mr."></asp:ListItem>
                                                <asp:ListItem Text="Mrs." Value="Mrs."></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <%-- end of 3rd row--%>
                                <%--4th row--%>
                                <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                    <%--     RMV start--%>
                                    <div style="float: left; width: 100%; color: Black;">
                                        <%--    1st row--%>
                                        <div style="float: left; width: 100%; color: Black;">
                                            <div style="float: left; width: 1%; color: Black;">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 32%; color: Black;">
                                                <div style="float: left; width: 30%;">
                                                    Last Name
                                                </div>
                                                <div style="float: left; width: 70%;">
                                                    <asp:TextBox ID="TextBoxLastName" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 1%; color: Black;">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 32%; color: Black;">
                                                <div style="float: left; width: 30%;">
                                                    Full Name
                                                </div>
                                                <div style="float: left; width: 70%;">
                                                    <asp:TextBox ID="TextBoxFullName" runat="server" CssClass="TextBox TextBoxUpper"
                                                        TextMode="MultiLine" Rows="2"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 1%; color: Black;">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 32%; color: Black;">
                                                <div style="float: left; width: 30%;">
                                                    Initials
                                                </div>
                                                <div style="float: left; width: 70%;">
                                                    <asp:TextBox ID="TextBoxInitials" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <%--end of 1st row--%>
                                        <%-- 2nd row--%>
                                        <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                            <div style="float: left; width: 1%; color: Black;">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 32%; color: Black;">
                                                <div style="float: left; width: 30%;">
                                                    Address Line 1
                                                </div>
                                                <div style="float: left; width: 70%;">
                                                    <asp:TextBox ID="TextBoxAdd1" runat="server" CssClass="TextBox TextBoxUpper" TextMode="MultiLine"
                                                        Rows="2"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 1%; color: Black;">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 32%; color: Black;">
                                                <div style="float: left; width: 30%;">
                                                    Address Line 2
                                                </div>
                                                <div style="float: left; width: 70%;">
                                                    <asp:TextBox ID="TextBoxAdd2" runat="server" CssClass="TextBox TextBoxUpper" TextMode="MultiLine"
                                                        Rows="2"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 1%; color: Black;">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 32%; color: Black;">
                                                <div style="float: left; width: 30%;">
                                                    City
                                                </div>
                                                <div style="float: left; width: 70%;">
                                                    <asp:TextBox ID="TextBoxCity" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <%-- end of 2nd row--%>
                                        <%--3rd row--%>
                                        <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                            <div style="float: left; width: 1%; color: Black;">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 32%; color: Black;">
                                                <div style="float: left; width: 30%;">
                                                    District
                                                </div>
                                                <div style="float: left; width: 70%;">
                                                    <asp:DropDownList ID="DropDownListDistrict" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListDistrict_SelectedIndexChanged"
                                                        CssClass="ComboBox">
                                                    </asp:DropDownList>
                                                    <%--<asp:TextBox ID="TextBoxDistrct" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>--%>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 1%; color: Black;">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 32%; color: Black;">
                                                <div style="float: left; width: 30%;">
                                                    Province
                                                </div>
                                                <div style="float: left; width: 70%;">
                                                    <asp:TextBox ID="TextBoxProvince" runat="server" CssClass="TextBox TextBoxUpper"
                                                        Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 1%; color: Black;">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 32%; color: Black;">
                                                <div style="float: left; width: 30%;">
                                                    Contact
                                                </div>
                                                <div style="float: left; width: 70%;">
                                                    <asp:TextBox ID="TextBoxContact" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                            </asp:Panel>
                            <%-- end of 3rd row--%>
                            <%--   4th row--%>
                            <asp:Panel ID="PanelVehDeta" runat="server" GroupingText="Vehical Details" CssClass="Panel">
                                <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 32%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Model
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxModel" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 32%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Brand
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxBrand" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <%--end of 4th row--%>
                                <%-- 5th row--%>
                                <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 32%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Chassie
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxChassie" runat="server" CssClass="TextBox " Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 32%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Engine
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxEngine" runat="server" CssClass="TextBox " Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 32%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Color
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxColor" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <%--  end of 5th row--%>
                                <%--  6th row--%>
                                <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 32%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Fuel
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxFeul" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 32%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Capacity
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxCapasity" runat="server" CssClass="TextBox " Text="0"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 32%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Unit
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxUnit" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <%-- end of 6th row--%>
                                <%--7th row--%>
                                <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 32%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Horse Power
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxHP" runat="server" CssClass="TextBox " Text="0"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 32%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Wheel base
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxWheelBase" runat="server" CssClass="TextBox " Text="0"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 32%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Front Tire
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxFroTire" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <%-- end of 7th row--%>
                                <%-- 8th row--%>
                                <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 32%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Rear Tire
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxRearTire" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 32%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Weight
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxWeight" runat="server" CssClass="TextBox " Text="0"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 32%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Manf. Year
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxManf" runat="server" CssClass="TextBox " Text="2012"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <%--end of 8th row--%>
                                <%-- 9th row--%>
                                <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 32%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Import License
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxImportLie" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 32%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Authority
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxAuthority" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 32%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Country
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxCountry" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <%--  end of 9th row--%>
                                <%-- 10th row--%>
                                <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 32%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Custom Date
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxCustomDate" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                            <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="TextBoxCustomDate"
                                                PopupButtonID="Image2" Enabled="True" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 32%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Clearance Date
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxClearenceDate" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                                            <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                            <asp:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="TextBoxClearenceDate"
                                                PopupButtonID="Image3" Enabled="True" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 32%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Declrear Date
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxDeclareDate" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                                            <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                            <asp:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="TextBoxDeclareDate"
                                                PopupButtonID="Image4" Enabled="True" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <%-- end of 10th row--%>
                            <%-- 11th row--%>
                            <asp:Panel ID="PanelImpDeta" runat="server" GroupingText="Importer Details" CssClass="Panel">
                                <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 32%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Importer
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxImporter" runat="server" CssClass="TextBox "></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 32%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Importer Address Line 1
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxImpAdd1" runat="server" CssClass="TextBox " TextMode="MultiLine"
                                                Rows="2"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 32%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Importer Address Line 2
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxImpAdd2" runat="server" CssClass="TextBox " TextMode="MultiLine"
                                                Rows="2"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <%--  end of 11th row--%>
                            <%--status view--%>
                                                        <asp:Panel ID="Panel9" runat="server" GroupingText="Status View" CssClass="Panel" Enabled="false">
                                <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 22%; color: Black;">
                                        <div style="float: left; width: 60%;">
                                            Send RMV  
                                        </div>
                                        <div style="float: left; width: 40%;">
                                            <asp:CheckBox ID="CheckBoxRMVst" runat="server" />
                                        </div>
                                    </div>
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 22%; color: Black;">
                                        <div style="float: left; width: 60%;">
                                            Assign Reg
                                        </div>
                                        <div style="float: left; width: 40%;">
                                            <asp:CheckBox ID="CheckBoxAssRegst" runat="server" />
                                        </div>
                                    </div>
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 22%; color: Black;">
                                        <div style="float: left; width: 60%;">
                                            Send Cus
                                        </div>
                                        <div style="float: left; width: 40%;">
                                           <asp:CheckBox ID="CheckBoxSeXCusSt" runat="server" />
                                        </div>
                                    </div>
                                                                        <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 22%; color: Black;">
                                        <div style="float: left; width: 60%;">
                                            Job Close
                                        </div>
                                        <div style="float: left; width: 40%;">
                                           <asp:CheckBox ID="CheckBoxJCst" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                           <%-- end of status--%>

                        </div>
                    </asp:Panel>
                </div>
                <%--   end of 4th row--%>
            </div>
            </div>
            <div style="float: left; width: 100%; color: Black;">
                <asp:Panel ID="PanelSendRMV" runat="server" GroupingText="Send RMV" Visible="false"
                    CssClass="Panel">
                    <div>
                        &nbsp;
                    </div>
                    <div style="float: left; width: 100%; color: Black;">
                        <div style="float: left; width: 100%; color: Black;">
                            <div style="float: left; width: 100%; color: Black; display: none;">
                                <div style="float: left; width: 30%;">
                                    Please Select
                                </div>
                                <div style="float: left; width: 70%;">
                                    <asp:DropDownList ID="DropDownListRMVRegNum" runat="server" Width="150px" AppendDataBoundItems="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div style="float: left; width: 1%; color: Black;">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 48%; color: Black;">
                                <%--ADDED 2012/09/08--%>
                                <%--ADVANCED SEARCH SCREEN--%>
                                <div style="float: left; width: 100%; color: Black;">
                                    <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                        <div style="float: left; width: 30%;">
                                            Acc No
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxRMVAccNo" CssClass="TextBox" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                        <div style="float: left; width: 30%;">
                                            Invoice
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxRMVInvNo" CssClass="TextBox" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                        <div style="float: left; width: 30%;">
                                            Reciept No
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxRMVRecNo" CssClass="TextBox" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; color: Black; padding-top: 2px;display: none;">
                                        <div style="float: left; width: 30%;">
                                            Vehical Number
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxRMVVehNo" CssClass="TextBox" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                        <div style="float: left; width: 30%;">
                                            Engine Number
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxRMVEngNo" CssClass="TextBox" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                        <div style="float: left; width: 30%;">
                                            Chassis Number
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxRMVChassisNo" CssClass="TextBox" runat="server"></asp:TextBox>
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                            <asp:LinkButton ID="LinkButtonRMVView" runat="server" Text="View" OnClick="LinkButtonRMVView_Click"></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <%--END--%>
                            </div>
                            <div style="float: left; width: 1%; color: Black;">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 48%; color: Black;">
                                <asp:Panel ID="Panel5" runat="server" GroupingText=" " Height="100px" ScrollBars="Auto">
                                    <asp:GridView ID="GridViewRMVSearch" runat="server" Width="600px" EmptyDataText="No data found"
                                        ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                                        CssClass="GridView" OnSelectedIndexChanged="GridViewRMVSearch_SelectedIndexChanged">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgSelect" runat="server" CommandName="SELECT" ImageUrl="~/Images/download_arrow_icon.png"
                                                        Height="15px" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="20px" Height="15px" />
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Rec No" DataField="SVRT_REF_NO" />
                                            <%--<asp:BoundField HeaderText="Reg No" DataField="SVRT_VEH_REG_NO" />--%>
                                            <asp:BoundField HeaderText="Inv No" DataField="SVRT_INV_NO" />
                                            <asp:BoundField HeaderText="Chassis" DataField="SVRT_CHASSIS" />
                                            <asp:BoundField HeaderText="Engine" DataField="SVRT_ENGINE" />
                                             <asp:BoundField HeaderText="Cus Name" DataField="SVRT_LAST_NAME" />
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
                        <div style="height: 10px;">
                            &nbsp;
                        </div>
                        <asp:Panel ID="PanelrMVt" runat="server" GroupingText=" ">
                            <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                <div style="float: left; width: 30%;">
                                    Send Date
                                </div>
                                <div style="float: left; width: 70%;">
                                    <asp:TextBox ID="TextBoxRMVSend" CssClass="TextBox" Enabled="False" runat="server"></asp:TextBox>
                                    <asp:Image ID="ImageRMV" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TextBoxRMVSend"
                                        PopupButtonID="ImageRMV" Enabled="True" Format="dd/MMM/yyyy">
                                    </asp:CalendarExtender>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </asp:Panel>
            </div>
            <div style="float: left; width: 100%; color: Black;">
                <asp:Panel ID="PanelAssignNumber" runat="server" GroupingText="Assign Registration Number"
                    Visible="false" CssClass="Panel">
                    <div>
                        &nbsp;
                    </div>
                    <div style="float: left; width: 100%; color: Black;">
                        <div style="float: left; width: 100%; color: Black;">
                            <div style="float: left; width: 100%; color: Black; display: none;">
                                <div style="float: left; width: 30%;">
                                    Please Select
                                </div>
                                <div style="float: left; width: 70%;">
                                    <asp:DropDownList ID="DropDownListAssRegNum" runat="server" Width="175px" AppendDataBoundItems="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div style="float: left; width: 1%; color: Black;">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 48%; color: Black;">
                                <%--ADDED 2012/09/08--%>
                                <%--ADVANCED SEARCH SCREEN--%>
                                <div style="float: left; width: 100%; color: Black;">
                                    <div style="float: left; width: 100%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Acc No
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxARAccNo" CssClass="TextBox" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                        <div style="float: left; width: 30%;">
                                            Invoice
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxARInvNo" CssClass="TextBox" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                        <div style="float: left; width: 30%;">
                                            Reciept No
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxARRecNo" CssClass="TextBox" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; color: Black; padding-top: 2px;display: none;">
                                        <div style="float: left; width: 30%;">
                                            Vehical Number
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxARVehNo" CssClass="TextBox" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                        <div style="float: left; width: 30%;">
                                            Engine Number
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxAREngNo" CssClass="TextBox" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                        <div style="float: left; width: 30%;">
                                            Chassis Number
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxARChassisNo" CssClass="TextBox" runat="server"></asp:TextBox>
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                            <asp:LinkButton ID="LinkButtonARView" runat="server" Text="View" OnClick="LinkButtonARView_Click"></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <%--END--%>
                            </div>
                            <div style="float: left; width: 1%; color: Black;">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 48%; color: Black;">
                                <asp:Panel ID="Panel6" runat="server" GroupingText=" " Height="100px" ScrollBars="Auto">
                                    <asp:GridView ID="GridViewSRSearch" runat="server" Width="600px" EmptyDataText="No data found"
                                        ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                                        CssClass="GridView" OnSelectedIndexChanged="GridViewSRSearch_SelectedIndexChanged">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgSelect" runat="server" CommandName="SELECT" ImageUrl="~/Images/download_arrow_icon.png"
                                                        Height="15px" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="20px" Height="15px" />
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Rec No" DataField="SVRT_REF_NO" />
                                           <%-- <asp:BoundField HeaderText="Reg No" DataField="SVRT_VEH_REG_NO" />--%>
                                            <asp:BoundField HeaderText="Inv No" DataField="SVRT_INV_NO" />
                                            <asp:BoundField HeaderText="Chassis" DataField="SVRT_CHASSIS" />
                                            <asp:BoundField HeaderText="Engine" DataField="SVRT_ENGINE" />
                                             <asp:BoundField HeaderText="Cus Name" DataField="SVRT_LAST_NAME" />
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
                        <div style="height: 10px;">
                            &nbsp;
                        </div>
                        <asp:Panel ID="Panel4" runat="server" GroupingText=" ">
                            <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                <div style="float: left; width: 30%;">
                                    Registration Number
                                </div>
                                <div style="float: left; width: 70%;">
                                    <asp:TextBox ID="TextBoxR1" CssClass="TextBoxUpper" runat="server" MaxLength="1"
                                        Width="10px"></asp:TextBox>
                                    <asp:TextBox ID="TextBoxR2" CssClass="TextBoxUpper" runat="server" MaxLength="1"
                                        Width="10px"></asp:TextBox>
                                    &nbsp;
                                    <asp:TextBox ID="TextBoxR3" CssClass="TextBoxUpper" runat="server" MaxLength="1"
                                        Width="10px"></asp:TextBox>
                                    <asp:TextBox ID="TextBoxR4" CssClass="TextBoxUpper" runat="server" MaxLength="1"
                                        Width="10px"></asp:TextBox>
                                    <asp:TextBox ID="TextBoxR5" CssClass="TextBoxUpper" runat="server" MaxLength="1"
                                        Width="10px" onblur="CreateNumber()"></asp:TextBox>
                                    -
                                    <asp:TextBox ID="TextBoxR6" CssClass="TextBoxUpper" runat="server" MaxLength="1"
                                        Width="10px" onchange="CheckFormat()"></asp:TextBox>
                                    <asp:TextBox ID="TextBoxR7" CssClass="TextBoxUpper" runat="server" MaxLength="1"
                                        Width="10px" onchange="CheckFormat()"></asp:TextBox>
                                    <asp:TextBox ID="TextBoxR8" CssClass="TextBoxUpper" runat="server" MaxLength="1"
                                        Width="10px" onchange="CheckFormat()"></asp:TextBox>
                                    <asp:TextBox ID="TextBoxR9" CssClass="TextBoxUpper" runat="server" MaxLength="1"
                                        Width="10px" onchange="CheckFormat()"></asp:TextBox>
                                    &nbsp;&nbsp;
                                    <asp:TextBox ID="TextBoxAssRegNumber" CssClass="Label" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                <div style="float: left; width: 30%;">
                                    Registration Date
                                </div>
                                <div style="float: left; width: 70%;">
                                    <asp:TextBox ID="TextBoxRegDate" CssClass="TextBox" Enabled="False" runat="server"
                                        Width="175px"></asp:TextBox>
                                    <asp:Image ID="ImageRegDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBoxRegDate"
                                        PopupButtonID="ImageRegDate" Enabled="True" Format="dd/MMM/yyyy">
                                    </asp:CalendarExtender>
                                </div>
                            </div>
                            <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                <div style="float: left; width: 30%;">
                                    Book Image
                                </div>
                                <div style="float: left; width: 70%;">
                                    <asp:FileUpload ID="FileUpload1" runat="server" onchange="GetFileName()" />
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </asp:Panel>
            </div>
            <div style="float: left; width: 100%; color: Black;">
                <asp:Panel ID="PanelSendCustomer" runat="server" GroupingText="Send Customer" Visible="false"
                    CssClass="Panel">
                    <div>
                        &nbsp;
                    </div>
                    <div style="float: left; width: 100%; color: Black;">
                        <div style="float: left; width: 100%; color: Black;">
                            <div style="float: left; width: 100%; color: Black; display: none;">
                                <div style="float: left; width: 30%;">
                                    Please Select
                                </div>
                                <div style="float: left; width: 70%;">
                                    <asp:DropDownList ID="DropDownListCusRegNum" runat="server" Width="150px" AppendDataBoundItems="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div style="float: left; width: 1%; color: Black;">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 48%; color: Black;">
                                <%--ADDED 2012/09/08--%>
                                <%--ADVANCED SEARCH SCREEN--%>
                                <div style="float: left; width: 100%; color: Black;">
                                    <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                        <div style="float: left; width: 30%;">
                                            Acc No
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxScAccNo" CssClass="TextBox" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                        <div style="float: left; width: 30%;">
                                            Invoice
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxScInvNo" CssClass="TextBox" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                        <div style="float: left; width: 30%;">
                                            Reciept No
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxSCRecNo" CssClass="TextBox" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                        <div style="float: left; width: 30%;">
                                            Vehical Number
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxScVehNo" CssClass="TextBox" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                        <div style="float: left; width: 30%;">
                                            Engine Number
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxSCEngNo" CssClass="TextBox" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                        <div style="float: left; width: 30%;">
                                            Chassis Number
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxSCChassisNo" CssClass="TextBox" runat="server"></asp:TextBox>
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                            <asp:LinkButton ID="LinkButtonSCView" runat="server" Text="View" OnClick="LinkButtonSCView_Click"></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <%--END--%>
                            </div>
                            <div style="float: left; width: 1%; color: Black;">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 48%; color: Black;">
                                <asp:Panel ID="Panel7" runat="server" GroupingText=" " Height="100px" ScrollBars="Auto">
                                    <asp:GridView ID="GridViewSCSearch" runat="server" Width="600px" EmptyDataText="No data found"
                                        ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                                        CssClass="GridView" OnSelectedIndexChanged="GridViewSCSearch_SelectedIndexChanged">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgSelect" runat="server" CommandName="SELECT" ImageUrl="~/Images/download_arrow_icon.png"
                                                        Height="15px" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="20px" Height="15px" />
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Rec No" DataField="SVRT_REF_NO" />
                                            <asp:BoundField HeaderText="Reg No" DataField="SVRT_VEH_REG_NO" />
                                            <asp:BoundField HeaderText="Inv No" DataField="SVRT_INV_NO" />
                                            <asp:BoundField HeaderText="Chassis" DataField="SVRT_CHASSIS" />
                                            <asp:BoundField HeaderText="Engine" DataField="SVRT_ENGINE" />
                                             <asp:BoundField HeaderText="Cus Name" DataField="SVRT_LAST_NAME" />
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
                        <div style="height: 10px;">
                            &nbsp;
                        </div>
                        <asp:Panel ID="Panel2" runat="server" GroupingText=" ">
                            <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                <div style="float: left; width: 30%;">
                                    CR Courried Date
                                </div>
                                <div style="float: left; width: 70%;">
                                    <asp:TextBox ID="TextBoxCustomerSend" CssClass="TextBox" Enabled="False" runat="server"></asp:TextBox>
                                    <asp:Image ID="ImageCustomerSend" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="TextBoxCustomerSend"
                                        PopupButtonID="ImageCustomerSend" Enabled="True" Format="dd/MMM/yyyy">
                                    </asp:CalendarExtender>
                                </div>
                            </div>
                            <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                <div style="float: left; width: 30%;">
                                    CR Courried POD Number
                                </div>
                                <div style="float: left; width: 70%;">
                                    <asp:TextBox ID="TextBox6" CssClass="TextBox" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                <div style="float: left; width: 30%;">
                                    Number Plate Courried Date
                                </div>
                                <div style="float: left; width: 70%;">
                                    <asp:TextBox ID="TextBoxNumPlaCou" CssClass="TextBox" Enabled="False" runat="server"></asp:TextBox>
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                    <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="TextBoxNumPlaCou"
                                        PopupButtonID="Image1" Enabled="True" Format="dd/MMM/yyyy">
                                    </asp:CalendarExtender>
                                </div>
                            </div>
                            <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                <div style="float: left; width: 30%;">
                                    Number Plate Courried POD Number
                                </div>
                                <div style="float: left; width: 70%;">
                                    <asp:TextBox ID="TextBoxNumPODNum" CssClass="TextBox" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </asp:Panel>
                        <%-- job close--%>
                        <%--                        <div style="float: left; width: 100%; color: Black; padding-top: 2px;"
                        runat="server" id="DivJobClose">
                            <div style="float: left; width: 30%;">
                                Job Close Date
                            </div>
                            <div style="float: left; width: 70%;">
                                <asp:TextBox ID="TextBoxJCloseDt" CssClass="TextBox" Enabled="False" runat="server"></asp:TextBox>
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="TextBoxJCloseDt"
                                    PopupButtonID="Image1" Enabled="True" Format="dd/MMM/yyyy">
                                </asp:CalendarExtender>
                            </div>
                        </div>--%>
                    </div>
                </asp:Panel>
            </div>
            <div style="float: left; width: 100%; color: Black;">
                <asp:Panel ID="PanelJobClose" runat="server" GroupingText="Job Close" Visible="false"
                    CssClass="Panel">
                    <div>
                        &nbsp;
                    </div>
                    <div style="float: left; width: 100%; color: Black;">
                        <div style="float: left; width: 100%; color: Black;">
                            <div style="float: left; width: 100%; color: Black; display: none">
                                <div style="float: left; width: 30%;">
                                    Please Select
                                </div>
                                <div style="float: left; width: 70%;">
                                    <asp:DropDownList ID="DropDownListJCloseRegNum" runat="server" Width="150px" AppendDataBoundItems="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div style="float: left; width: 1%; color: Black;">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 48%; color: Black;">
                                <%--ADDED 2012/09/08--%>
                                <%--ADVANCED SEARCH SCREEN--%>
                                <div style="float: left; width: 100%; color: Black;">
                                    <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                        <div style="float: left; width: 30%;">
                                            Acc No
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxJCAccNo" CssClass="TextBox" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                        <div style="float: left; width: 30%;">
                                            Invoice
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxJCInvNo" CssClass="TextBox" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                        <div style="float: left; width: 30%;">
                                            Reciept No
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxJCRecNo" CssClass="TextBox" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                        <div style="float: left; width: 30%;">
                                            Vehical Number
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxJCVehNo" CssClass="TextBox" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                        <div style="float: left; width: 30%;">
                                            Engine Number
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxJCEngNo" CssClass="TextBox" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                        <div style="float: left; width: 30%;">
                                            Chassis Number
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxJCChassisNo" CssClass="TextBox" runat="server"></asp:TextBox>
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                            <asp:LinkButton ID="LinkButtonViewJS" runat="server" Text="View" OnClick="LinkButtonViewJS_Click"></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <%--END--%>
                            </div>
                            <div style="float: left; width: 1%; color: Black;">
                                &nbsp;
                            </div>
                            <div style="float: left; width: 48%; color: Black;">
                                <asp:Panel ID="Panel8" runat="server" GroupingText=" " Height="100px" ScrollBars="Auto">
                                    <asp:GridView ID="GridViewJCSearch" runat="server" Width="600px" EmptyDataText="No data found"
                                        ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                                        CssClass="GridView" OnSelectedIndexChanged="GridViewJCSearch_SelectedIndexChanged">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgSelect" runat="server" CommandName="SELECT" ImageUrl="~/Images/download_arrow_icon.png"
                                                        Height="15px" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="20px" Height="15px" />
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Rec No" DataField="SVRT_REF_NO" />
                                            <asp:BoundField HeaderText="Reg No" DataField="SVRT_VEH_REG_NO" />
                                            <asp:BoundField HeaderText="Inv No" DataField="SVRT_INV_NO" />
                                            <asp:BoundField HeaderText="Chassis" DataField="SVRT_CHASSIS" />
                                            <asp:BoundField HeaderText="Engine" DataField="SVRT_ENGINE" />
                                             <asp:BoundField HeaderText="Cus Name" DataField="SVRT_LAST_NAME" />
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
                        <div style="height: 10px;">
                            &nbsp;
                        </div>
                        <asp:Panel ID="Panel3" runat="server" GroupingText=" ">
                            <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                <div style="float: left; width: 30%;">
                                    Date
                                </div>
                                <div style="float: left; width: 70%;">
                                    <asp:TextBox ID="TextBoxJobCloseDt" CssClass="TextBox" Enabled="False" runat="server"></asp:TextBox>
                                    <asp:Image ID="ImageJobCloseDt" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="TextBoxJobCloseDt"
                                        PopupButtonID="ImageJobCloseDt" Enabled="True" Format="dd/MMM/yyyy">
                                    </asp:CalendarExtender>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </asp:Panel>
            </div>
            <%--ADDED 2012/09/08--%>
            <%--ADVANCED SEARCH SCREEN--%>
            <%--    <div style="float: left; width: 100%; color: Black;">
                <div style="float: left; width: 100%; color: Black;">
                    <div style="float: left; width: 30%;">
                        Acc No
                    </div>
                    <div style="float: left; width: 70%;">
                        <asp:TextBox ID="TextBoxAccNo" CssClass="TextBox" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div style="float: left; width: 100%; color: Black;">
                    <div style="float: left; width: 30%;">
                        Invoice
                    </div>
                    <div style="float: left; width: 70%;">
                        <asp:DropDownList ID="DropDownListInvoice" runat="server" Width="150px" AppendDataBoundItems="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div style="float: left; width: 100%; color: Black;">
                    <div style="float: left; width: 30%;">
                        Vehical Number
                    </div>
                    <div style="float: left; width: 70%;">
                        <asp:DropDownList ID="DropDownListVehNumber" runat="server" Width="150px" AppendDataBoundItems="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div style="float: left; width: 100%; color: Black;">
                    <div style="float: left; width: 30%;">
                        Engine Number
                    </div>
                    <div style="float: left; width: 70%;">
                        <asp:DropDownList ID="DropDownListEngineNumber" runat="server" Width="150px" AppendDataBoundItems="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div style="float: left; width: 100%; color: Black;">
                    <div style="float: left; width: 30%;">
                        Chassis Number
                    </div>
                    <div style="float: left; width: 70%;">
                        <asp:DropDownList ID="DropDownListChassis" runat="server" Width="150px" AppendDataBoundItems="true">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>--%>
            <%--END--%>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
