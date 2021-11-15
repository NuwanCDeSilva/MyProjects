<%@ Page Title="Vehical Insurance" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="VehicalInsurance.aspx.cs" Inherits="FF.WebERPClient.General_Modules.VehicalInsurance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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

        // javascript
        function DisplayToDateICN() {
            var dateFrom = document.getElementById('<%=TextBoxCNFrom.ClientID%>').value;
            var no = document.getElementById('<%=TextBoxCNDays.ClientID%>').value;
            PageMethods.ReturnToDate(dateFrom, no, onSucessICN);
        }
        function onSucessICN(result) {
            document.getElementById('<%=TextBoxCNTo.ClientID%>').value = result;
        }

        function DisplayToDateECN() {
            var dateFrom = document.getElementById('<%=TextBoxECNFrom.ClientID%>').value;
            var no = document.getElementById('<%=TextBoxECNDays.ClientID%>').value;
            PageMethods.ReturnToDate(dateFrom, no, onSucessECN);
        }
        function onSucessECN(result) {
            document.getElementById('<%=TextBoxECNTo.ClientID%>').value = result;
        }

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

        function ConfirmApproval() {
            var val = document.getElementById('<%=hdnGetConfirm.ClientID%>').value;
            if (val == "1") {
                return confirm('Are you sure?');
            }
            else {
                alert('Can not save');
                return false;
            }
        }

        function MakeLabel() {
            //            var list = document.getElementById('<%=Panel3.ClientID %>').getElementsByTagName('input');
            //            for (var i = 0; i < list.length;i++) {
            //                var e = list[i];
            //                document.getElementById(e.id).className = "Label";
            //                //document.getElementById(e.id).style.border = "none";
            //            }
            document.getElementById('<%=hdnTextStyle.ClientID %>').value = "1";
        }

        function MakeTextBox() {
            //            var list = document.getElementById('<%=Panel3.ClientID %>').getElementsByTagName('input');
            //            for (var i = 0; i < list.length; i++) {
            //                var e = list[i];
            //                document.getElementById(e.id).className = "TextBox";
            //                //document.getElementById(e.id).style.border = "none";
            //            }
            document.getElementById('<%=hdnTextStyle.ClientID %>').value = "0";
        }

    </script>
    <style type="text/css">
        .Label
        {
            overflow: hidden;
            border: none;
            background-color: white;
        }
        
        .fixWidth tr td
        {
            width: 43%;
        }
        
        .fixWidth1 tr td
        {
            width: 40%;
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
            padding: 1px 93px;
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
        
        
        #nav1
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
        #nav1 li
        {
            float: left;
        }
        #nav1 li a
        {
            display: block;
            padding: 1px 45px;
            text-decoration: none;
            font-weight: bold;
            color: #069;
            border-right: 1px solid #ccc;
        }
        #nav1 li a:hover
        {
            color: #c00;
            background-color: #fff;
        }
        
        #nav2
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
        #nav2 li
        {
            float: left;
        }
        #nav2 li a
        {
            display: block;
            padding: 1px 98px;
            text-decoration: none;
            font-weight: bold;
            color: #069;
            border-right: 1px solid #ccc;
        }
        #nav2 li a:hover
        {
            color: #c00;
            background-color: #fff;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updtMainPnl" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdnTextStyle" Value="0" runat="server" />
            <asp:HiddenField ID="hdnGetConfirm" runat="server" Value="0" />
            <asp:HiddenField ID="hdnSetlDebAmo" runat="server" Value="0" />
            <asp:Panel ID="PanelAll" runat="server" Height="550" ScrollBars="Auto">
                <div style="float: left; width: 100%; color: Black;">
                    <div style="text-align: right; height: 18px;" class="PanelHeader">
                        <asp:Button ID="ButtonSave" runat="server" Text="Save" CssClass="Button" OnClick="ButtonSave_Click"
                            OnClientClick="return ConfirmApproval()" />
                        <asp:Button ID="ButtonPrint" runat="server" Text="Print" CssClass="Button" OnClientClick="return ConfirmApproval()"
                            OnClick="ButtonPrint_Click" />
                        <asp:Button ID="ButtonClear" runat="server" Text="Clear" CssClass="Button" OnClick="ButtonClear_Click" />
                        <asp:Button ID="ButtonClose" runat="server" Text="Close" CssClass="Button" OnClick="ButtonClose_Click" />
                    </div>
                    <div style="height: 1px; float: left; width: 100%; color: Black;">
                        &nbsp;
                    </div>
                    <div style="float: left; width: 100%; color: Black; height: 18px;">
                        <ul id="nav">
                            <li>
                                <asp:LinkButton ID="LinkButtonIssCovNot" runat="server" Text="Issue Cover Note & Certificate"
                                    OnClick="LinkButtonIssCovNot_Click"></asp:LinkButton></li>
                            <li>
                                <asp:LinkButton ID="LinkButtonSDN" runat="server" Text="Settle Debit Note" OnClick="LinkButtonSDN_Click"></asp:LinkButton></li>
                            <li>
                                <asp:LinkButton ID="LinkButtonCP" runat="server" Text="Claim Process" OnClick="LinkButtonCP_Click"></asp:LinkButton></li>
                        </ul>
                        <%--   <asp:RadioButtonList ID="RadioButtonListOption" runat="server" AutoPostBack="true"
                            RepeatColumns="3" CssClass="fixWidth">
                            <asp:ListItem Text="1.Issue Cover Note & Certificate" Value="OP2"></asp:ListItem>
                            <asp:ListItem Text="2.Settle Debit Note" Value="OP3"></asp:ListItem>
                            <asp:ListItem Text="3.Claim Process" Value="OP4"></asp:ListItem>
                        </asp:RadioButtonList>--%>
                    </div>
                    <div style="height: 1px; float: left; width: 100%; color: Black;">
                        &nbsp;
                    </div>
                    <asp:Panel ID="PanelInsReciept" runat="server">
                        <div style="float: left; width: 67%; color: Black; height: 18px;">
                            <div style="float: left; width: 100%; color: Black;">
                                <ul id="nav1">
                                    <li>
                                        <asp:LinkButton ID="LinkButtonIssCov" runat="server" Text="Issue Cover Note" OnClick="LinkButtonIssCov_Click"
                                            OnClientClick="MakeTextBox()"></asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="LinkButtonECN" runat="server" Text="Extend the Cover Note" OnClick="LinkButtonECN_Click"
                                            OnClientClick="MakeLabel()"></asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="LinkButtonIssCer" runat="server" Text="Issue Certificate" OnClick="LinkButtonIssCer_Click"
                                            OnClientClick="MakeLabel()"></asp:LinkButton></li>
                                </ul>
                                <%-- <asp:RadioButtonList ID="RadioButtonListISOptions" runat="server" AutoPostBack="true"
                                    RepeatColumns="3" CssClass="fixWidth1">
                                    <asp:ListItem Text="1.Issue Cover Note " Value="OP2"></asp:ListItem>
                                    <asp:ListItem Text="2.Extend the Cover Note" Value="OP3"></asp:ListItem>
                                    <asp:ListItem Text="3.Issue Certificate" Value="OP4"></asp:ListItem>
                                </asp:RadioButtonList>--%>
                                <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 48%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Reciept No
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxRNo" runat="server" CssClass="TextBoxUpper" OnTextChanged="TextBoxRNo_TextChanged"
                                                AutoPostBack="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 48%; color: Black;" id="DivVehical" runat="server"
                                        visible="false">
                                        <div style="float: left; width: 100%; color: Black;">
                                            <div style="float: left; width: 30%;">
                                                Vehical No
                                            </div>
                                            <div style="float: left; width: 30%;">
                                                <%--<asp:TextBox ID="TextBoxVehicalNo" runat="server" CssClass="TextBoxUpper"></asp:TextBox>--%>
                                                <asp:Label ID="LabelVehNo" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 100%; color: Black;">
                                            <div style="float: left; width: 30%;">
                                                Reg Date
                                            </div>
                                            <div style="float: left; width: 70%;">
                                                <%--<asp:TextBox ID="TextBoxRegDate" runat="server" CssClass="TextBoxUpper" Enabled="false" ></asp:TextBox>
                                                <asp:Image ID="Image20" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                                <asp:CalendarExtender ID="CalendarExtender18" runat="server" TargetControlID="TextBoxRegDate"
                                                    PopupButtonID="Image20" Enabled="True" Format="dd/MMM/yyyy">
                                                </asp:CalendarExtender>--%>
                                                <asp:Label ID="LabelRegDate" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div style="float: left; width: 100%; height: 10px;">
                                    &nbsp;
                                </div>
                                <asp:Panel ID="Panel4" runat="server" GroupingText="Insurance Dteails" CssClass="Panel">
                                    <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                        <div style="float: left; width: 1%; color: Black;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 49%; color: Black;">
                                            <div style="float: left; width: 30%;">
                                                Ins. Com.
                                            </div>
                                            <div style="float: left; width: 70%;">
                                                <asp:DropDownList ID="DropDownListInsCom" CssClass="ComboBox" runat="server" Enabled="false">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 1%; color: Black;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 49%; color: Black;">
                                            <div style="float: left; width: 30%;">
                                                Ins. Pol.
                                            </div>
                                            <div style="float: left; width: 70%;">
                                                <asp:DropDownList ID="DropDownListInsPol" CssClass="ComboBox" runat="server" Enabled="false">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                        <div style="float: left; width: 2%;">
                            &nbsp;
                        </div>
                        <%-- searching component--%>
                        <div style="float: left; width: 30%; color: Black;">
                            <div class="PanelHeader" style="float: left; width: 100%; height: 10px;">
                                Reciept Search
                            </div>
                            <asp:Panel ID="Panel1" runat="server" GroupingText=" ">
                                <div style="float: left; width: 98%; height: 1px;">
                                    &nbsp;
                                </div>
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 100%; padding-top: 1px;">
                                        <asp:CheckBox ID="CheckBoxICMrec" runat="server" Text="Most Recent" />
                                    </div>
                                </div>
                                <%--  company sec--%>
                                <div style="float: left; width: 100%; padding-top: 1px;">
                                    <asp:CheckBox ID="CheckBoxICCom" runat="server" Text="By Company" OnCheckedChanged="CheckBoxICCom_CheckedChanged"
                                        AutoPostBack="true" />
                                    <%--by company--%>
                                    <div style="float: left; width: 100%; padding-top: 1px;" runat="server" id="DivComSearch"
                                        visible="false">
                                        <div style="float: left; width: 49%; padding-top: 1px;">
                                            <div style="float: left; width: 100%;">
                                                <div style="float: left; width: 100%;">
                                                    <div style="float: left; width: 20%;">
                                                        Com.
                                                    </div>
                                                    <div style="float: left; width: 80%;">
                                                        <asp:TextBox ID="TextBoxCom" runat="server" CssClass="TextBoxUpper" Width="75px"></asp:TextBox>
                                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/icon_search.png"
                                                            OnClick="imgComSearch_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 1%;">
                                        </div>
                                        <div style="float: left; width: 49%; padding-top: 1px;">
                                            <div style="float: left; width: 100%;">
                                                <div style="float: left; width: 20%;">
                                                    PC
                                                </div>
                                                <div style="float: left; width: 80%;">
                                                    <asp:TextBox ID="TextBoxPC" runat="server" CssClass="TextBoxUpper" Width="75px"></asp:TextBox>
                                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/icon_search.png"
                                                        OnClick="ImgPCsearch_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%-- end of com search--%>
                                <%-- Date range sec--%>
                                <div style="float: left; width: 100%; padding-top: 1px;">
                                    <asp:CheckBox ID="CheckBoxICDate" runat="server" Text="By Date" OnCheckedChanged="CheckBoxICDate_CheckedChanged"
                                        AutoPostBack="true" />
                                    <%--by date--%>
                                    <div style="float: left; width: 100%; padding-top: 1px;" runat="server" id="DivDate"
                                        visible="false">
                                        <div style="float: left; width: 49%; padding-top: 1px;">
                                            <div style="float: left; width: 100%;">
                                                <div style="float: left; width: 100%;">
                                                    <div style="float: left; width: 20%;">
                                                        From
                                                    </div>
                                                    <div style="float: left; width: 80%;">
                                                        <asp:TextBox ID="TextBoxDFrom" runat="server" CssClass="TextBoxUpper" Enabled="false"
                                                            Width="75px"></asp:TextBox>
                                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="TextBoxDFrom"
                                                            PopupButtonID="Image2" Enabled="True" Format="dd/MMM/yyyy">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 1%;">
                                        </div>
                                        <div style="float: left; width: 49%; padding-top: 1px;">
                                            <div style="float: left; width: 100%;">
                                                <div style="float: left; width: 20%;">
                                                    To
                                                </div>
                                                <div style="float: left; width: 80%;">
                                                    <asp:TextBox ID="TextBoxDTo" runat="server" CssClass="TextBoxUpper" Enabled="false"
                                                        Width="75px"></asp:TextBox>
                                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="TextBoxDTo"
                                                        PopupButtonID="Image3" Enabled="True" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%-- end of date range--%>
                                <div style="float: left; width: 100%; padding-top: 1px;">
                                    <asp:CheckBox ID="CheckBoxAcc" runat="server" Text="By Acc" AutoPostBack="true" OnCheckedChanged="CheckBoxAcc_CheckedChanged" />
                                    <%--by date--%>
                                    <div style="float: left; width: 100%; padding-top: 1px;" runat="server" id="DivAcc"
                                        visible="false">
                                        <div style="float: left; width: 30%;">
                                            Acc No
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxAcc" runat="server" CssClass="TextBoxUpper"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div style="float: left; width: 80%; padding-top: 1px; text-align: right;">
                                    <asp:Button ID="Button2" runat="server" Text="Search" CssClass="Button" OnClick="Button1_Click" />
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px;">
                                    <asp:Panel ID="Panel6" runat="server" CssClass="Panel" Height="100" ScrollBars="Auto">
                                        <%--grid view--%>
                                        <asp:GridView ID="GridViewSearch" runat="server" Width="500px" ShowHeaderWhenEmpty="True"
                                            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="Both"
                                            CssClass="GridView" OnSelectedIndexChanged="GridViewSearch_SelectedIndexChanged">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgSelect" runat="server" CommandName="SELECT" ImageUrl="~/Images/download_arrow_icon.png"
                                                            Height="15px" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="3%" Height="15px" />
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Reciept No" DataField="SVIT_REF_NO" />
                                                <asp:BoundField HeaderText="Invoice No" DataField="SVIT_INV_NO" />
                                                <asp:BoundField HeaderText="Cus. Name" DataField="SVIT_LAST_NAME" ItemStyle-Width="200px" />
                                                <asp:BoundField HeaderText="Cov. Nt" DataField="SVIT_CVNT_NO" />
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
                        <%-- end of searching component--%>
                        <div id="DivISCP1" runat="server" style="float: left; width: 100%; color: Black;">
                            <div style="float: left; width: 100%; color: Black;">
                                <%--Main Details part 1--%>
                                <asp:Panel ID="PanelCusDe" runat="server" GroupingText="Customer Details" CssClass="Panel">
                                    <div style="float: left; width: 100%; color: Black; height: 10px;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
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
                                        <div style="float: left; width: 1%; color: Black;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 32%; color: Black;">
                                            <div style="float: left; width: 30%;">
                                                Cus. Code
                                            </div>
                                            <div style="float: left; width: 70%;">
                                                <asp:TextBox ID="TextBoxCusCode" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
                                                <asp:ImageButton ID="ImageButton3" runat="server" OnClick="ImageButtonCusCode_Click"
                                                    ImageUrl="~/Images/icon_search.png" />
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
                                                        City
                                                    </div>
                                                    <div style="float: left; width: 70%;">
                                                        <asp:TextBox ID="TextBoxCity" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
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
                                                        District
                                                    </div>
                                                    <div style="float: left; width: 70%;">
                                                        <asp:DropDownList ID="DropDownListDistrict" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListDistrict_SelectedIndexChanged"
                                                            CssClass="ComboBox">
                                                        </asp:DropDownList>
                                                        <%--<asp:TextBox ID="TextBoxDistrct" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>--%>
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
                                                        Initials
                                                    </div>
                                                    <div style="float: left; width: 70%;">
                                                        <asp:TextBox ID="TextBoxInitials" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>
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
                                                        <asp:TextBox ID="TextBoxContact" runat="server" CssClass="TextBox TextBoxUpper" MaxLength="10"></asp:TextBox>
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
                                            </div>
                                </asp:Panel>
                            </div>
                        </div>
                        <div style="float: left; width: 100%; height: 10px; color: Black;">
                            &nbsp;
                        </div>
                        <%-- Main part 2--%>
                        <div id="DivISCP2" runat="server" style="float: left; width: 100%; color: Black;">
                            <div style="float: left; width: 100%; color: Black;">
                                <asp:Panel ID="Panel3" runat="server" GroupingText="Vehical Registration Details"
                                    CssClass="Panel">
                                    <div style="float: left; width: 100%; color: Black; height: 10px;">
                                        &nbsp;
                                    </div>
                                    <%-- 1st row--%>
                                    <div style="float: left; width: 100%; color: Black;">
                                        <%--     <div style="float: left; width: 32%; color: Black;">
                                        <div style="float: left; width: 30%; color: Black;">
                                            Make
                                        </div>
                                        <div style="float: left; width: 70%; color: Black;">
                                            <asp:TextBox ID="TextBoxMake" runat="server" CssClass="TextBox"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>--%>
                                        <div style="float: left; width: 1%; color: Black;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 32%; color: Black;">
                                            <div style="float: left; width: 30%; color: Black;">
                                                Model
                                            </div>
                                            <div style="float: left; width: 70%; color: Black;">
                                                <asp:TextBox ID="TextBoxModel" runat="server" CssClass="TextBox"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 1%; color: Black;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 32%; color: Black;">
                                            <div style="float: left; width: 30%; color: Black;">
                                                Capacity
                                            </div>
                                            <div style="float: left; width: 70%; color: Black;">
                                                <asp:TextBox ID="TextBoxCC" runat="server" CssClass="TextBox"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 1%; color: Black;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 32%; color: Black;">
                                            <div style="float: left; width: 30%; color: Black;">
                                                Scheme Code
                                            </div>
                                            <div style="float: left; width: 70%; color: Black;">
                                                <asp:TextBox ID="TextBoxSchemeCode" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- 2nd row--%>
                                    <div style="float: left; width: 100%; color: Black; padding-top: 1px;">
                                        <div style="float: left; width: 1%; color: Black;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 32%; color: Black;">
                                            <div style="float: left; width: 30%; color: Black;">
                                                Engine No
                                            </div>
                                            <div style="float: left; width: 70%; color: Black;">
                                                <asp:TextBox ID="TextBoxEngine" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 1%; color: Black;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 32%; color: Black;">
                                            <div style="float: left; width: 30%; color: Black;">
                                                Chassis No
                                            </div>
                                            <div style="float: left; width: 70%; color: Black;">
                                                <asp:TextBox ID="TextBoxChassis" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 1%; color: Black;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 32%; color: Black;">
                                            <div style="float: left; width: 30%; color: Black;">
                                                Sales Price
                                            </div>
                                            <div style="float: left; width: 70%; color: Black;">
                                                <asp:TextBox ID="TextBoxSalesPrice" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; color: Black; padding-top: 1px;">
                                        <div style="float: left; width: 1%; color: Black;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 32%; color: Black;">
                                            <div style="float: left; width: 30%; color: Black;">
                                                Acc No
                                            </div>
                                            <div style="float: left; width: 70%; color: Black;">
                                                <asp:TextBox ID="TextBoxVAcc" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div style="float: left; height: 10px; color: Black; width: 100%;">
                                    &nbsp;
                                </div>
                                <asp:Panel ID="PanelCoverNoteDetails" runat="server" GroupingText="Cover Note Details"
                                    CssClass="Panel" Visible="true">
                                    <div style="float: left; width: 100%; color: Black; height: 10px;">
                                        &nbsp;
                                    </div>
                                    <%-- 1st row--%>
                                    <div style="float: left; width: 100%; color: Black;">
                                        <div style="float: left; width: 1%; color: Black;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 32%; color: Black;">
                                            <div style="float: left; width: 30%; color: Black;">
                                                Cover Note No
                                            </div>
                                            <div style="float: left; width: 70%; color: Black;">
                                                <asp:TextBox ID="TextBoxCNNo" runat="server" CssClass="TextBox" Enabled="False"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 1%; color: Black;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 32%; color: Black;">
                                            <div style="float: left; width: 30%; color: Black;">
                                                Prem. Value
                                            </div>
                                            <div style="float: left; width: 70%; color: Black;">
                                                <asp:TextBox ID="TextBoxInsValue" runat="server" CssClass="TextBox" onKeyPress="return numbersonly(event,true)"
                                                    Style="text-align: right;" Enabled="False"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- 2nd row--%>
                                    <div style="float: left; width: 100%; color: Black; padding-top: 1px;">
                                        <div style="float: left; width: 1%; color: Black;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 32%; color: Black;">
                                            <div style="float: left; width: 30%; color: Black;">
                                                No Days
                                            </div>
                                            <div style="float: left; width: 70%; color: Black;">
                                                <asp:TextBox ID="TextBoxCNDays" runat="server" CssClass="TextBox" OnTextChanged="TextBoxCNDays_TextChanged"
                                                    AutoPostBack="true" onKeyPress="return numbersonly(event,false)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 1%; color: Black;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 32%; color: Black;">
                                            <div style="float: left; width: 30%; color: Black;">
                                                From
                                            </div>
                                            <div style="float: left; width: 70%; color: Black;">
                                                <asp:TextBox ID="TextBoxCNFrom" runat="server" CssClass="TextBox" ContentEditable="false"
                                                    onchange="DisplayToDateICN()"></asp:TextBox>
                                                <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                                <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="TextBoxCNFrom"
                                                    PopupButtonID="Image5" Enabled="True" Format="dd/MMM/yyyy">
                                                </asp:CalendarExtender>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 1%; color: Black;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 32%; color: Black;">
                                            <div style="float: left; width: 30%; color: Black;">
                                                To
                                            </div>
                                            <div style="float: left; width: 70%; color: Black;">
                                                <asp:TextBox ID="TextBoxCNTo" runat="server" CssClass="TextBox" Enabled="False"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="PanelExtendCoverNote" runat="server" GroupingText="Extend Cover Note Details"
                                    CssClass="Panel" Visible="false">
                                    <div style="float: left; width: 100%; color: Black; height: 10px;">
                                        &nbsp;
                                    </div>
                                    <%-- 1st row--%>
                                    <div style="float: left; width: 100%; color: Black;">
                                        <%-- 2nd row--%>
                                        <div style="float: left; width: 100%; color: Black; padding-top: 1px;">
                                            <div style="float: left; width: 1%; color: Black;">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 32%; color: Black;">
                                                <div style="float: left; width: 30%; color: Black;">
                                                    No Days
                                                </div>
                                                <div style="float: left; width: 70%; color: Black;">
                                                    <asp:TextBox ID="TextBoxECNDays" runat="server" CssClass="TextBox" AutoPostBack="true"
                                                        OnTextChanged="TextBoxECNDays_TextChanged" onKeyPress="return numbersonly(event,false)"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 1%; color: Black;">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 32%; color: Black;">
                                                <div style="float: left; width: 30%; color: Black;">
                                                    From
                                                </div>
                                                <div style="float: left; width: 70%; color: Black;">
                                                    <asp:TextBox ID="TextBoxECNFrom" runat="server" CssClass="TextBox" ContentEditable="false"
                                                        onchange="DisplayToDateECN()"></asp:TextBox>
                                                    <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                                    <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="TextBoxECNFrom"
                                                        PopupButtonID="Image6" Enabled="True" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 32%; color: Black;">
                                                <div style="float: left; width: 30%; color: Black;">
                                                    To
                                                </div>
                                                <div style="float: left; width: 70%; color: Black;">
                                                    <asp:TextBox ID="TextBoxECNTo" runat="server" CssClass="TextBox" Enabled="False"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 100%; color: Black; height: 10px;">
                                            &nbsp;
                                        </div>
                                        <asp:Panel ID="PanelOtherDebitNotes" runat="server" GroupingText=" ">
                                        </asp:Panel>
                                </asp:Panel>
                                <asp:Panel ID="PanelIssueCretificate" runat="server" GroupingText="Issue Cretificate Details Details"
                                    CssClass="Panel" Visible="false">
                                    <div style="float: left; width: 100%; color: Black; height: 10px;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 100%; color: Black;">
                                        <%-- 1st row--%>
                                        <div style="float: left; width: 100%; color: Black; padding-top: 1px;">
                                            <div style="float: left; width: 1%; color: Black;">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 32%; color: Black;">
                                                <div style="float: left; width: 30%; color: Black;">
                                                    Policy No
                                                </div>
                                                <div style="float: left; width: 70%; color: Black;">
                                                    <asp:TextBox ID="TextBoxIssCerPol" runat="server" CssClass="TextBox" ></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 1%; color: Black;">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 32%; color: Black;">
                                                <div style="float: left; width: 30%; color: Black;">
                                                    Effective date
                                                </div>
                                                <div style="float: left; width: 70%; color: Black;">
                                                    <asp:TextBox ID="TextBoxIssCerEff" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                                                    <asp:Image ID="Image7" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                                    <asp:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="TextBoxIssCerEff"
                                                        PopupButtonID="Image7" Enabled="True" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 32%; color: Black;">
                                                <div style="float: left; width: 30%; color: Black;">
                                                    Date of Expiry
                                                </div>
                                                <div style="float: left; width: 70%; color: Black;">
                                                    <asp:TextBox ID="TextBoxIssCerExp" runat="server" Enabled="false" CssClass="TextBox"></asp:TextBox>
                                                    <asp:Image ID="Image8" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                                    <asp:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="TextBoxIssCerExp"
                                                        PopupButtonID="Image8" Enabled="True" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <%--2nd row--%>
                                        <div style="float: left; width: 100%; color: Black; padding-top: 1px;">
                                            <div style="float: left; width: 1%; color: Black;">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 32%; color: Black;">
                                                <div style="float: left; width: 30%; color: Black;">
                                                    Debit Note no
                                                </div>
                                                <div style="float: left; width: 70%; color: Black;">
                                                    <asp:TextBox ID="TextBoxIssCerDebNo" runat="server" CssClass="TextBox" ></asp:TextBox>
                                                </div>
                                            </div>
                                            <%--3rd row--%>
                                            <div style="float: left; width: 100%; color: Black; padding-top: 1px;">
                                                <div style="float: left; width: 1%; color: Black;">
                                                    &nbsp;
                                                </div>
                                                <div style="float: left; width: 32%; color: Black;">
                                                    <div style="float: left; width: 30%; color: Black;">
                                                        Net Premium
                                                    </div>
                                                    <div style="float: left; width: 70%; color: Black;">
                                                        <asp:TextBox ID="TextBoxIssCerNet" runat="server" CssClass="TextBox" AutoPostBack="true"
                                                            OnTextChanged="TextBoxIssCerNet_TextChanged" onKeyPress="return numbersonly(event,true)"
                                                            Style="text-align: right;"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div style="float: left; width: 1%; color: Black;">
                                                    &nbsp;
                                                </div>
                                                <div style="float: left; width: 32%; color: Black;">
                                                    <div style="float: left; width: 30%; color: Black;">
                                                        S.R.C.C.Premium
                                                    </div>
                                                    <div style="float: left; width: 70%; color: Black;">
                                                        <asp:TextBox ID="TextBoxIssCerSRCC" runat="server" CssClass="TextBox" AutoPostBack="true"
                                                            OnTextChanged="TextBoxIssCerSRCC_TextChanged" onKeyPress="return numbersonly(event,true)"
                                                            Style="text-align: right;"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div style="float: left; width: 32%; color: Black;">
                                                    <div style="float: left; width: 30%; color: Black;">
                                                        Others
                                                    </div>
                                                    <div style="float: left; width: 70%; color: Black;">
                                                        <asp:TextBox ID="TextBoxIssCerOther" runat="server" CssClass="TextBox" Enabled="false"
                                                            onKeyPress="return numbersonly(event,true)" Style="text-align: right;"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <%-- 4th row--%>
                                            <div style="float: left; width: 100%; color: Black; padding-top: 1px;">
                                                <div style="float: left; width: 1%; color: Black;">
                                                    &nbsp;
                                                </div>
                                                <div style="float: left; width: 32%; color: Black;">
                                                    <div style="float: left; width: 30%; color: Black;">
                                                        Total
                                                    </div>
                                                    <div style="float: left; width: 70%; color: Black;">
                                                        <asp:TextBox ID="TextBoxIssCreTot" runat="server" CssClass="TextBox" Enabled="false"
                                                            Style="text-align: right;"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div style="float: left; width: 1%; color: Black;">
                                                    &nbsp;
                                                </div>
                                                <div style="float: left; width: 32%; color: Black;">
                                                    <div style="float: left; width: 30%; color: Black;">
                                                        File No
                                                    </div>
                                                    <div style="float: left; width: 70%; color: Black;">
                                                        <asp:TextBox ID="TextBoxFileNo" runat="server" CssClass="TextBox"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 100%; color: Black; padding-top: 1px;">
                                                &nbsp;
                                            </div>
                                            <%-- 4th row--%>
                                        </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="PanelSettleDebitNote" runat="server" Visible="false">
                        <div style="float: left; width: 67%; color: Black;">
                            <div style="float: left; width: 100%; color: Black;">
                                <%--Main Details part 1--%>
                                <div style="float: left; width: 100%; height: 20px;">
                                    &nbsp;
                                </div>
                                <asp:Panel ID="Panel5" runat="server" GroupingText=" " CssClass="Panel">
                                    <div style="float: left; width: 100%;">
                                        <div style="float: left; width: 20%;">
                                            Debit note No
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxSDRec" runat="server" CssClass="TextBoxUpper"></asp:TextBox>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:LinkButton ID="LinkButtonSDAdd" runat="server" Text="Add" OnClick="LinkButtonSDAdd_Click"></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; height: 10px;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 100%; padding-top: 1px;">
                                        <div style="float: left; width: 20%;">
                                            Debit Notes
                                        </div>
                                        <div style="float: left; width: 69%;">
                                            <asp:ListBox ID="ListBoxRecieptList" runat="server" Rows="4" Width="250px"></asp:ListBox>
                                            <asp:LinkButton ID="LinkButtonSDRemove" runat="server" Text="Remove" OnClick="LinkButtonSDRemove_Click"></asp:LinkButton>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                        <div style="float: left; width: 2%;">
                            &nbsp;
                        </div>
                        <%-- searching component--%>
                        <div style="float: left; width: 30%; color: Black;">
                            <div class="PanelHeader" style="float: left; width: 98%; height: 10px;">
                                Debit Note Search
                            </div>
                            <asp:Panel ID="Panel2" runat="server" GroupingText=" ">
                                <div style="float: left; width: 98%; height: 1px;">
                                    &nbsp;
                                </div>
                                <div style="float: left; width: 100%;">
                                    <div style="float: left; width: 100%; padding-top: 1px;">
                                        <asp:CheckBox ID="CheckBoxSDMre" runat="server" Text="Most Recent" />
                                    </div>
                                </div>
                                <%--  company sec--%>
                                <div style="float: left; width: 100%; padding-top: 1px;">
                                    <asp:CheckBox ID="CheckBoxSDCom" runat="server" Text="By Company" AutoPostBack="true"
                                        OnCheckedChanged="CheckBoxSDCom_CheckedChanged" />
                                    <%--by company--%>
                                    <div style="float: left; width: 100%; padding-top: 1px;" runat="server" id="DivSDCom"
                                        visible="false">
                                        <div style="float: left; width: 49%; padding-top: 1px;">
                                            <div style="float: left; width: 100%;">
                                                <div style="float: left; width: 100%;">
                                                    <div style="float: left; width: 20%;">
                                                        Com.
                                                    </div>
                                                    <div style="float: left; width: 80%;">
                                                        <asp:TextBox ID="TextBoxSDCom" runat="server" CssClass="TextBoxUpper" Width="75px"></asp:TextBox>
                                                        <asp:ImageButton ID="ImageButtonSDC" runat="server" ImageUrl="~/Images/icon_search.png"
                                                            OnClick="ImageButtonSDC_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 1%;">
                                        </div>
                                        <div style="float: left; width: 49%; padding-top: 1px;">
                                            <div style="float: left; width: 100%;">
                                                <div style="float: left; width: 20%;">
                                                    PC
                                                </div>
                                                <div style="float: left; width: 80%;">
                                                    <asp:TextBox ID="TextBoxSDPc" runat="server" CssClass="TextBoxUpper" Width="75px"></asp:TextBox>
                                                    <asp:ImageButton ID="ImageButtonSDPc" runat="server" ImageUrl="~/Images/icon_search.png"
                                                        OnClick="ImageButtonSDPc_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%-- end of com search--%>
                                <%-- Date range sec--%>
                                <div style="float: left; width: 100%; padding-top: 1px;">
                                    <asp:CheckBox ID="CheckBoxSDDate" runat="server" Text="By Date" AutoPostBack="true"
                                        OnCheckedChanged="CheckBoxSDDate_CheckedChanged" />
                                    <%--by date--%>
                                    <div style="float: left; width: 100%; padding-top: 1px;" runat="server" id="DivSDDate"
                                        visible="false">
                                        <div style="float: left; width: 49%; padding-top: 1px;">
                                            <div style="float: left; width: 100%;">
                                                <div style="float: left; width: 100%;">
                                                    <div style="float: left; width: 20%;">
                                                        From
                                                    </div>
                                                    <div style="float: left; width: 80%;">
                                                        <asp:TextBox ID="TextBoxSDFrom" runat="server" CssClass="TextBoxUpper" Enabled="false"
                                                            Width="75px"></asp:TextBox>
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBoxSDFrom"
                                                            PopupButtonID="Image1" Enabled="True" Format="dd/MMM/yyyy">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 1%;">
                                        </div>
                                        <div style="float: left; width: 49%; padding-top: 1px;">
                                            <div style="float: left; width: 100%;">
                                                <div style="float: left; width: 20%;">
                                                    To
                                                </div>
                                                <div style="float: left; width: 80%;">
                                                    <asp:TextBox ID="TextBoxSDTo" runat="server" CssClass="TextBoxUpper" Enabled="false"
                                                        Width="75px"></asp:TextBox>
                                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TextBoxSDTo"
                                                        PopupButtonID="Image4" Enabled="True" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--  start of vehical no--%>
                                <div style="float: left; width: 100%; padding-top: 1px;">
                                    <asp:CheckBox ID="CheckBoxRegNo" runat="server" Text="By Veh No" AutoPostBack="true"
                                        OnCheckedChanged="CheckBoxRegNo_CheckedChanged" />
                                    <div style="float: left; width: 100%; padding-top: 1px;" runat="server" id="DivVeh"
                                        visible="false">
                                        <div style="float: left; width: 60%; padding-top: 1px;">
                                            <div style="float: left; width: 100%;">
                                                <div style="float: left; width: 100%;">
                                                    <div style="float: left; width: 30%;">
                                                        Veh No.
                                                    </div>
                                                    <div style="float: left; width: 70%;">
                                                        <asp:TextBox ID="TextBoxVehNo" runat="server" CssClass="TextBoxUpper" Width="75px"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px;">
                                    <asp:CheckBox ID="CheckBoxDAcc" runat="server" Text="By Acc" AutoPostBack="true"
                                        OnCheckedChanged="CheckBoxDAcc_CheckedChanged" />
                                    <%--by date--%>
                                    <div style="float: left; width: 100%; padding-top: 1px;" runat="server" id="DivDAcc"
                                        visible="false">
                                        <div style="float: left; width: 30%;">
                                            Acc No
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxDAcc" runat="server" CssClass="TextBoxUpper"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <%-- end of vehical no--%>
                                <div style="float: left; width: 80%; padding-top: 1px; text-align: right;">
                                    <asp:Button ID="ButtonSDSearch" runat="server" Text="Search" CssClass="Button" OnClick="ButtonSDSearch_Click" />
                                </div>
                                <%-- end of date range--%>
                                <div style="float: left; width: 100%; padding-top: 1px;">
                                    <%--grid view--%>
                                    <asp:Panel ID="SePanel" runat="server" ScrollBars="Auto" Height="50px">
                                        <asp:GridView ID="GridViewSettleDebit" runat="server" Width="500px" ShowHeaderWhenEmpty="True"
                                            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="Both"
                                            CssClass="GridView" OnSelectedIndexChanged="GridViewSearch_SelectedIndexChanged"
                                            DataKeyNames="SVIT_REF_NO">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="checkReciept" runat="server" OnCheckedChanged="checkReciept_CheckedChanged"
                                                            AutoPostBack="true" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Deb. No" DataField="SVIT_DBT_NO" />
                                                <asp:BoundField HeaderText="Veh. No" DataField="SVIT_VEH_REG_NO" ItemStyle-Width="75px" />
                                                <asp:BoundField HeaderText="Inv. No" DataField="SVIT_INV_NO" />
                                                <asp:BoundField HeaderText="Cus. Name" DataField="SVIT_LAST_NAME" ItemStyle-Width="200px" />
                                                <asp:BoundField HeaderText="Cov. Nt" DataField="SVIT_CVNT_NO" />
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
                        <%-- end of searching component--%>
                        <%--  main part 2--%>
                        <div style="float: left; width: 100%;">
                            <%--pay mode details--%>
                            <asp:Panel ID="Panel7" runat="server" GroupingText=" " CssClass="Panel">
                                <div style="float: left; width: 100%; padding-top: 1px;">
                                    <div style="float: left; width: 20%;">
                                        Total Amount
                                    </div>
                                    <div style="float: left; width: 69%;">
                                        <asp:TextBox ID="TextBoxSeDeTot" runat="server" Enabled="false" Text="0"></asp:TextBox>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div style="float: left; width: 100%; height: 10px;">
                                &nbsp;
                            </div>
                            <asp:Panel ID="pnlPayment" runat="server" Height="171px" GroupingText=" ">
                                <div style="float: left; width: 100%; color: Black; height: 10px;">
                                    &nbsp;
                                </div>
                                <div style="float: left; width: 50%;">
                                    <div style="float: left; width: 100%;">
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 15%;">
                                            Pay Mode
                                        </div>
                                        <div style="float: left; width: 25%;">
                                            <asp:DropDownList ID="ddlPayMode" runat="server" Width="80%" CssClass="ComboBox"
                                                OnSelectedIndexChanged="PaymentType_LostFocus" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 10%;">
                                            Amount
                                        </div>
                                        <div style="float: left; width: 35%;">
                                            <asp:TextBox ID="txtPayAmount" runat="server" CssClass="TextBoxUpper" Width="50%"
                                                onKeyPress="return numbersonly(event,true)"></asp:TextBox>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <div style="float: left; width: 85%; padding-top: 2px; text-align: right;">
                                                <asp:Button ID="btnPayAdd" runat="server" CssClass="Button" Text="Add" OnClick="AddPayment" />
                                            </div>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; padding: 2px 2px 0px 0px;">
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 15%;">
                                            Remarks
                                        </div>
                                        <div style="float: left; width: 75%;">
                                            <asp:TextBox ID="txtPayRemarks" runat="server" CssClass="TextBox" Width="100%" TextMode="MultiLine"
                                                Rows="2"></asp:TextBox></div>
                                    </div>
                                    <div style="float: left; width: 100%; padding: 2px 2px 0px 0px;">
                                        <%--Credit/Cheque/Bank Slip payment--%>
                                        <div style="float: left; width: 100%; height: 100px;" runat="server" id="divCredit"
                                            visible="false">
                                            <div style="float: left; width: 100%;" id="divCRDno" runat="server">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 15%;">
                                                    Card No</div>
                                                <div style="float: left; width: 75%; padding-bottom: 2px;">
                                                    <asp:TextBox runat="server" ID="txtPayCrCardNo" CssClass="TextBox" Width="80%"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 100%;" id="divBankDet" runat="server">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 15%;">
                                                    Bank
                                                </div>
                                                <div style="float: left; width: 20%; padding-bottom: 2px;">
                                                    <asp:TextBox runat="server" ID="txtPayCrBank" CssClass="TextBox" Width="35%"></asp:TextBox>
                                                    <asp:ImageButton ID="ImgBtnBankSearch" runat="server" ImageUrl="~/Images/icon_search.png"
                                                        OnClick="ImgBtnBankSearch_Click" />
                                                </div>
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 15%;">
                                                    Branch
                                                </div>
                                                <div style="float: left; width: 20%; padding-bottom: 2px;">
                                                    <asp:TextBox runat="server" ID="txtPayCrBranch" CssClass="TextBox" Width="70%"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 100%;" id="divCreditCard" runat="server">
                                                <div style="float: left; width: 100%;" id="divCardDet" runat="server">
                                                    <div style="float: left; width: 1%;">
                                                        &nbsp;</div>
                                                    <div style="float: left; width: 15%;">
                                                        Card Type
                                                    </div>
                                                    <div style="float: left; width: 15%; padding-bottom: 2px;">
                                                        <asp:TextBox runat="server" ID="txtPayCrCardType" CssClass="TextBox" Width="90%"></asp:TextBox>
                                                    </div>
                                                    <div style="float: left; width: 1%;">
                                                        &nbsp;</div>
                                                    <div style="float: left; width: 15%;">
                                                        Expiry Date
                                                    </div>
                                                    <div style="float: left; width: 25%; padding-bottom: 2px;">
                                                        <asp:TextBox runat="server" ID="txtPayCrExpiryDate" CssClass="TextBox" Width="70%"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender9" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                            TargetControlID="txtPayCrExpiryDate">
                                                        </asp:CalendarExtender>
                                                        &nbsp;<asp:Image ID="btnImgCrExpiryDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png"
                                                            ImageAlign="Middle" />
                                                    </div>
                                                </div>
                                                <div style="float: left; width: 100%;">
                                                    <div style="float: left; width: 1%;">
                                                        &nbsp;</div>
                                                    <div style="float: left; width: 15%;">
                                                        Promotion
                                                    </div>
                                                    <div style="float: left; width: 75%; padding-bottom: 2px;">
                                                        <asp:CheckBox runat="server" ID="chkPayCrPromotion" />
                                                        &nbsp; Period &nbsp;
                                                        <asp:TextBox runat="server" ID="txtPayCrPeriod" CssClass="TextBoxNumeric" Width="20%"></asp:TextBox>
                                                        months
                                                    </div>
                                                </div>
                                                <div style="float: left; width: 100%;" id="divCredBatch">
                                                    <div style="float: left; width: 1%;">
                                                        &nbsp;</div>
                                                    <div style="float: left; width: 15%;">
                                                        Batch No
                                                    </div>
                                                    <div style="float: left; width: 75%; padding-bottom: 2px;">
                                                        <asp:TextBox runat="server" ID="txtPayCrBatchNo" CssClass="TextBox" Width="80%"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 100%;" id="divChequeNum" runat="server">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 15%;">
                                                    Cheque No
                                                </div>
                                                <div style="float: left; width: 75%; padding-bottom: 2px;">
                                                    <asp:TextBox runat="server" ID="txtChequeNo" CssClass="TextBox" Width="80%"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <%--Advance receipt/Credit Note payment--%>
                                        <div style="float: left; width: 100%;" runat="server" id="divAdvReceipt" visible="false">
                                            <div style="float: left; width: 100%;">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 20%;">
                                                    Referance</div>
                                                <div style="float: left; width: 75%; padding-bottom: 2px;">
                                                    <asp:TextBox runat="server" ID="txtPayAdvReceiptNo" CssClass="TextBox" Width="80%"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 100%;">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 20%;">
                                                    Ref. Amount</div>
                                                <div style="float: left; width: 75%; padding-bottom: 2px;">
                                                    <asp:TextBox runat="server" ID="txtPayAdvRefAmount" CssClass="TextBox" Width="80%"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div style="float: left; width: 50%;">
                                    <div style="float: left; width: 100%;">
                                        <asp:Panel ID="pnlPay" runat="server" Height="140px" ScrollBars="Auto">
                                            <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False" CssClass="GridView"
                                                CellPadding="3" ForeColor="#333333" DataKeyNames="sard_pay_tp,sard_settle_amt"
                                                OnRowDeleting="gvPayment_OnDelete" ShowHeaderWhenEmpty="True">
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <AlternatingRowStyle BackColor="White" />
                                                <EmptyDataTemplate>
                                                    <div style="width: 100%; text-align: center;">
                                                        No data found
                                                    </div>
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnImgDelete" runat="server" ImageAlign="Middle" ImageUrl="~/Images/Delete.png"
                                                                Width="10px" Height="10px" CommandName="Delete" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField='sard_seq_no' HeaderText='' Visible="false" />
                                                    <asp:BoundField DataField='sard_line_no' HeaderText='' Visible="false" />
                                                    <asp:BoundField DataField='sard_receipt_no' HeaderText='' Visible="false" />
                                                    <asp:BoundField DataField='sard_inv_no' HeaderText='' Visible="false" />
                                                    <asp:BoundField DataField='sard_pay_tp' HeaderText='Payment Type' ItemStyle-Width="110px"
                                                        HeaderStyle-Width="110px">
                                                        <HeaderStyle Width="110px" />
                                                        <ItemStyle Width="110px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField='sard_ref_no' HeaderText='' Visible="false" />
                                                    <asp:BoundField DataField='sard_chq_bank_cd' HeaderText='Bank' ItemStyle-Width="90px"
                                                        HeaderStyle-Width="90px">
                                                        <HeaderStyle Width="90px" />
                                                        <ItemStyle Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField='sard_chq_branch' HeaderText='Branch' ItemStyle-Width="90px"
                                                        HeaderStyle-Width="90px">
                                                        <HeaderStyle Width="90px" />
                                                        <ItemStyle Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField='sard_deposit_bank_cd' HeaderText='' Visible="false" />
                                                    <asp:BoundField DataField='sard_deposit_branch' HeaderText='' Visible="false" />
                                                    <asp:BoundField DataField='sard_credit_card_bank' HeaderText='' Visible="false" />
                                                    <asp:BoundField DataField='sard_cc_tp' HeaderText='Card Type' />
                                                    <asp:BoundField DataField='sard_cc_expiry_dt' HeaderText='Expiry Date' Visible="false" />
                                                    <asp:BoundField DataField='sard_cc_is_promo' HeaderText='Promotion' Visible="false" />
                                                    <asp:BoundField DataField='sard_cc_period' HeaderText='Period' Visible="false" />
                                                    <asp:BoundField DataField='sard_gv_issue_loc' HeaderText='' Visible="false" />
                                                    <asp:BoundField DataField='sard_gv_issue_dt' HeaderText='' Visible="false" />
                                                    <asp:BoundField DataField='sard_settle_amt' HeaderText='Amount' />
                                                    <asp:BoundField DataField='sard_sim_ser' HeaderText='' Visible="false" />
                                                    <asp:BoundField DataField='sard_receipt_no' HeaderText='receipt_no' Visible="False" />
                                                    <asp:BoundField DataField='sard_anal_3' HeaderText="Bank/Other Charges" />
                                                </Columns>
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>
                                    <div style="float: left; width: 100%;">
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                        <div style="float: left; width: 20%; background-color: #1569C7; color: White; height: 22px;">
                                            Paid Amount
                                        </div>
                                        <div style="float: left; width: 18%; text-align: right; border-color: Black; border-style: solid;
                                            border-width: 1px;">
                                            <asp:Label runat="server" ID="lblPayPaid" Text="0.00"></asp:Label>
                                        </div>
                                        <div style="float: left; width: 18%;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 20%; background-color: #1569C7; color: White; height: 22px;">
                                            Balance Amount
                                        </div>
                                        <div style="float: left; width: 18%; text-align: right; border-color: Black; border-style: solid;
                                            border-width: 1px;">
                                            <asp:Label runat="server" ID="lblPayBalance" Text="0.00"></asp:Label>
                                        </div>
                                        <div style="float: left; width: 1%;">
                                            &nbsp;</div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="PanelClaimProcess" runat="server" Visible="false">
                        <div style="float: left; width: 100%; color: Black;">
                            <ul id="nav2">
                                <li>
                                    <asp:LinkButton ID="LinkButtonCPCI" runat="server" Text="Claim Intimated" OnClick="LinkButtonCPCI_Click"></asp:LinkButton></li>
                                <li>
                                    <asp:LinkButton ID="LinkButtonCPRD" runat="server" Text="Receive Documents" OnClick="LinkButtonCPRD_Click"></asp:LinkButton></li>
                                <li>
                                    <asp:LinkButton ID="LinkButtonCPCS" runat="server" Text="Customer Settlement" OnClick="LinkButtonCPCS_Click"></asp:LinkButton></li>
                            </ul>
                        </div>
                        <div style="float: left; width: 100%; color: Black;">
                            <div style="float: left; width: 100%;">
                            </div>
                            <asp:Panel ID="PanelClaimInimated" runat="server">
                                <div style="float: left; width: 100%; color: Black;">
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 49%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Registration No
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:TextBox ID="TextBoxCPRegNo" runat="server" CssClass="TextBoxUpper" AutoPostBack="true"
                                                OnTextChanged="TextBoxCPRegNo_TextChanged"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div style="float: left; width: 100%; height: 10px; color: Black;">
                                    &nbsp;
                                </div>
                                <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" TargetControlID="PanelGrid"
                                    CollapsedSize="0" ExpandControlID="Image17" CollapseControlID="Image17" Collapsed="true"
                                    AutoCollapse="False" AutoExpand="False" ScrollContents="false" ExpandDirection="Vertical"
                                    ImageControlID="Image17" ExpandedImage="~/Images/16 X 16 UpArrow.jpg" CollapsedImage="~/Images/16 X 16 DownArrow.jpg"
                                    BehaviorID="collapsibleBehavior">
                                </asp:CollapsiblePanelExtender>
                                <div style="background-color: #1E4A9F; color: White; width: 98%; float: left; height: 18px;">
                                </div>
                                <div style="float: left;">
                                    <asp:Image runat="server" ID="Image17" ImageUrl="~/Images/16 X 16 DownArrow.jpg"
                                        ImageAlign="Right" /></div>
                                <div style="float: left; width: 100%;">
                                    <asp:Panel ID="PanelGrid" runat="server">
                                        <div style="float: left; width: 99%;">
                                            <asp:GridView ID="GridViewPreviousClaims" runat="server" Width="100%" ShowHeaderWhenEmpty="True"
                                                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="Both"
                                                CssClass="GridView" OnRowDataBound="GridViewPreviousClaims_RowDataBound" OnSelectedIndexChanged="GridViewPreviousClaims_SelectedIndexChanged">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgSelect" runat="server" CommandName="SELECT" ImageUrl="~/Images/download_arrow_icon.png"
                                                                Height="15px" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="20px" Height="15px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelStatus" runat="server" Text='<%# BIND("Doc_stus") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="20px" Height="15px" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Acc Date" DataField="Acc_date" DataFormatString='<%$ appSettings:FormatToDate %>'>
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Reg No" DataField="Reg_No" />
                                                    <asp:BoundField HeaderText="Dri Name" DataField="Dri_name" />
                                                    <asp:BoundField HeaderText="Dri Lic" DataField="Dl_lic" />
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
                                <div style="float: left; width: 100%; color: Black; height: 10px;">
                                    &nbsp;
                                </div>
                                <div style="float: left; width: 100%; color: Black;">
                                    <asp:Panel ID="Panel8" runat="server" GroupingText="Customer Details" CssClass="Panel">
                                        <div style="float: left; width: 100%; color: Black; height: 10px;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                            <div style="float: left; width: 1%; color: Black;">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 32%; color: Black;">
                                                <div style="float: left; width: 30%;">
                                                    Customer Title
                                                </div>
                                                <div style="float: left; width: 70%;">
                                                    <asp:Label ID="LabelCusTi" runat="server"></asp:Label>
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
                                                            <asp:Label ID="LabelLastName" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div style="float: left; width: 1%; color: Black;">
                                                        &nbsp;
                                                    </div>
                                                    <div style="float: left; width: 32%; color: Black;">
                                                        <div style="float: left; width: 30%;">
                                                            Address Line 1
                                                        </div>
                                                        <div style="float: left; width: 70%;">
                                                            <asp:Label ID="LabelAddLin1" runat="server"></asp:Label>
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
                                                            <asp:Label ID="LabelCity" runat="server"></asp:Label>
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
                                                            Full Name
                                                        </div>
                                                        <div style="float: left; width: 70%;">
                                                            <asp:Label ID="LabelFullName" runat="server"></asp:Label>
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
                                                            <asp:Label ID="LabelAddlin2" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div style="float: left; width: 1%; color: Black;">
                                                        &nbsp;
                                                    </div>
                                                    <div style="float: left; width: 32%; color: Black;">
                                                        <div style="float: left; width: 30%;">
                                                            District
                                                        </div>
                                                        <div style="float: left; width: 70%;">
                                                            <asp:Label ID="LabelDistrict" runat="server"></asp:Label>
                                                            <%--<asp:TextBox ID="TextBoxDistrct" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>--%>
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
                                                            Initials
                                                        </div>
                                                        <div style="float: left; width: 70%;">
                                                            <asp:Label ID="LabelInitials" runat="server"></asp:Label>
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
                                                            <asp:Label ID="LabelContact" runat="server"></asp:Label>
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
                                                            <asp:Label ID="LabelProvince" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                    </asp:Panel>
                                </div>
                                <div style="float: left; width: 100%; color: Black;">
                                    &nbsp;
                                </div>
                                <div style="float: left; width: 100%; height: 10px; color: Black;">
                                    <asp:Panel ID="Panel11" runat="server" GroupingText="Claim Details" CssClass="Panel">
                                        <div style="float: left; width: 100%; color: Black;">
                                            <div style="float: left; width: 1%; color: Black;">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 32%; color: Black;">
                                                <div style="float: left; width: 30%;">
                                                    Accident Date
                                                </div>
                                                <div style="float: left; width: 70%;">
                                                    <asp:TextBox ID="TextBoxAccDate" runat="server" Enabled="false" CssClass="TextBox"></asp:TextBox>
                                                    <asp:Image ID="Image9" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                                    <asp:CalendarExtender ID="CalendarExtender10" runat="server" TargetControlID="TextBoxAccDate"
                                                        PopupButtonID="Image9" Enabled="True" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 1%; color: Black;">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 32%; color: Black;">
                                                <div style="float: left; width: 30%;">
                                                    Police Report
                                                </div>
                                                <div style="float: left; width: 70%;">
                                                    <asp:CheckBox ID="CheckBoxPoliceReport" runat="server" CssClass="CheckBox" />
                                                </div>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                            <div style="float: left; width: 1%; color: Black;">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 32%; color: Black;">
                                                <div style="float: left; width: 30%;">
                                                    Driver Name
                                                </div>
                                                <div style="float: left; width: 70%;">
                                                    <asp:TextBox ID="TextBoxDriverName" runat="server" CssClass="TextBox"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 1%; color: Black;">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 32%; color: Black;">
                                                <div style="float: left; width: 30%;">
                                                    Licence No
                                                </div>
                                                <div style="float: left; width: 70%;">
                                                    <asp:TextBox ID="TextBoxLicenceNo" runat="server" CssClass="TextBox"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="PanelRecieveDocument" runat="server" Visible="false">
                                <div style="float: left; width: 100%; color: Black;">
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 32%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Select Registration No
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:DropDownList ID="DropDownListRDRegNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListRDRegNo_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div style="float: left; width: 100%; height: 10px; color: Black;">
                                    &nbsp;
                                </div>
                                <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server" TargetControlID="PanelGrid1"
                                    CollapsedSize="0" ExpandControlID="Image18" CollapseControlID="Image18" Collapsed="true"
                                    AutoCollapse="False" AutoExpand="False" ScrollContents="false" ExpandDirection="Vertical"
                                    ImageControlID="Image18" ExpandedImage="~/Images/16 X 16 UpArrow.jpg" CollapsedImage="~/Images/16 X 16 DownArrow.jpg"
                                    BehaviorID="collapsibleBehavior">
                                </asp:CollapsiblePanelExtender>
                                <div style="background-color: #1E4A9F; color: White; width: 98%; float: left; height: 18px;">
                                </div>
                                <div style="float: left;">
                                    <asp:Image runat="server" ID="Image18" ImageUrl="~/Images/16 X 16 DownArrow.jpg"
                                        ImageAlign="Right" /></div>
                                <div style="float: left; width: 100%;">
                                    <asp:Panel ID="PanelGrid1" runat="server">
                                        <asp:GridView ID="GridViewRDPreviousClaim" runat="server" Width="100%" ShowHeaderWhenEmpty="True"
                                            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="Both"
                                            CssClass="GridView" OnSelectedIndexChanged="GridViewRDPreviousClaim_SelectedIndexChanged">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgSelect" runat="server" CommandName="SELECT" ImageUrl="~/Images/download_arrow_icon.png"
                                                            Height="15px" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="20px" Height="15px" />
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Acc Date" DataField="Acc_date" DataFormatString='<%$ appSettings:FormatToDate %>'>
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Veh No" DataField="Reg_no" />
                                                <asp:BoundField HeaderText="Dri Name" DataField="Dri_name" />
                                                <asp:BoundField HeaderText="Dri Lic" DataField="Dl_lic" />
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
                                <div style="float: left; width: 100%; color: Black; height: 10px;">
                                    &nbsp;
                                </div>
                                <div style="float: left; width: 100%; color: Black;">
                                    <asp:Panel ID="Panel9" runat="server" GroupingText="Customer Details" CssClass="Panel">
                                        <div style="float: left; width: 100%; color: Black; height: 10px;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                            <div style="float: left; width: 1%; color: Black;">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 32%; color: Black;">
                                                <div style="float: left; width: 30%;">
                                                    Customer Title
                                                </div>
                                                <div style="float: left; width: 70%;">
                                                    <asp:Label ID="LabelRDCusTi" runat="server"></asp:Label>
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
                                                            <asp:Label ID="LabelRDLastName" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div style="float: left; width: 1%; color: Black;">
                                                        &nbsp;
                                                    </div>
                                                    <div style="float: left; width: 32%; color: Black;">
                                                        <div style="float: left; width: 30%;">
                                                            Address Line 1
                                                        </div>
                                                        <div style="float: left; width: 70%;">
                                                            <asp:Label ID="LabelRDAddLin1" runat="server"></asp:Label>
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
                                                            <asp:Label ID="LabelRDCity" runat="server"></asp:Label>
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
                                                            Full Name
                                                        </div>
                                                        <div style="float: left; width: 70%;">
                                                            <asp:Label ID="LabelRDFullName" runat="server"></asp:Label>
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
                                                            <asp:Label ID="LabelRDAddlin2" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div style="float: left; width: 1%; color: Black;">
                                                        &nbsp;
                                                    </div>
                                                    <div style="float: left; width: 32%; color: Black;">
                                                        <div style="float: left; width: 30%;">
                                                            District
                                                        </div>
                                                        <div style="float: left; width: 70%;">
                                                            <asp:Label ID="LabelRDDistrict" runat="server"></asp:Label>
                                                            <%--<asp:TextBox ID="TextBoxDistrct" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>--%>
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
                                                            Initials
                                                        </div>
                                                        <div style="float: left; width: 70%;">
                                                            <asp:Label ID="LabelRDInitials" runat="server"></asp:Label>
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
                                                            <asp:Label ID="LabelRDContact" runat="server"></asp:Label>
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
                                                            <asp:Label ID="LabelRDProvince" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                    </asp:Panel>
                                </div>
                                <div style="float: left; width: 100%; color: Black;">
                                    <asp:Panel ID="Panel12" runat="server" GroupingText="Claim Details" CssClass="Panel">
                                        <div style="float: left; width: 100%; color: Black; height: 10px;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 100%; color: Black;">
                                            <div style="float: left; width: 1%; color: Black;">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 32%; color: Black;">
                                                <div style="float: left; width: 40%;">
                                                    Claim Form Recieve
                                                </div>
                                                <div style="float: left; width: 60%;">
                                                    <asp:TextBox ID="TextBoxClaimFormRec" runat="server" Enabled="false" CssClass="TextBox"></asp:TextBox>
                                                    <asp:Image ID="Image10" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                                    <asp:CalendarExtender ID="CalendarExtender11" runat="server" TargetControlID="TextBoxClaimFormRec"
                                                        PopupButtonID="Image10" Enabled="True" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 1%; color: Black;">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 32%; color: Black;">
                                                <div style="float: left; width: 40%;">
                                                    Licence Recieve
                                                </div>
                                                <div style="float: left; width: 60%;">
                                                    <asp:TextBox ID="TextBoxLicenceRecieve" runat="server" Enabled="false" CssClass="TextBox"></asp:TextBox>
                                                    <asp:Image ID="Image11" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                                    <asp:CalendarExtender ID="CalendarExtender12" runat="server" TargetControlID="TextBoxLicenceRecieve"
                                                        PopupButtonID="Image11" Enabled="True" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 1%; color: Black;">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 32%; color: Black;">
                                                <div style="float: left; width: 40%;">
                                                    Repair Estimate Recieve
                                                </div>
                                                <div style="float: left; width: 60%;">
                                                    <asp:TextBox ID="TextBoxRapirEstiRec" runat="server" Enabled="false" CssClass="TextBox"></asp:TextBox>
                                                    <asp:Image ID="Image12" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                                    <asp:CalendarExtender ID="CalendarExtender13" runat="server" TargetControlID="TextBoxRapirEstiRec"
                                                        PopupButtonID="Image12" Enabled="True" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                            <div style="float: left; width: 1%; color: Black;">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 32%; color: Black;">
                                                <div style="float: left; width: 40%;">
                                                    Police Report Recieve
                                                </div>
                                                <div style="float: left; width: 60%;">
                                                    <asp:TextBox ID="TextBoxPoliceRepRecieve" runat="server" Enabled="false" CssClass="TextBox"></asp:TextBox>
                                                    <asp:Image ID="Image13" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                                    <asp:CalendarExtender ID="CalendarExtender14" runat="server" TargetControlID="TextBoxPoliceRepRecieve"
                                                        PopupButtonID="Image13" Enabled="True" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 1%; color: Black;">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 32%; color: Black;">
                                                <div style="float: left; width: 40%;">
                                                    Forwading Approval
                                                </div>
                                                <div style="float: left; width: 60%;">
                                                    <asp:TextBox ID="TextBoxFowadinApp" runat="server" Enabled="false" CssClass="TextBox"></asp:TextBox>
                                                    <asp:Image ID="Image14" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                                    <asp:CalendarExtender ID="CalendarExtender15" runat="server" TargetControlID="TextBoxFowadinApp"
                                                        PopupButtonID="Image14" Enabled="True" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 1%; color: Black;">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 32%; color: Black;">
                                                <div style="float: left; width: 40%;">
                                                    Final Invoice
                                                </div>
                                                <div style="float: left; width: 60%;">
                                                    <asp:TextBox ID="TextBoxFinalApp" runat="server" Enabled="false" CssClass="TextBox"></asp:TextBox>
                                                    <asp:Image ID="Image15" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                                    <asp:CalendarExtender ID="CalendarExtender16" runat="server" TargetControlID="TextBoxFinalApp"
                                                        PopupButtonID="Image15" Enabled="True" Format="dd/MMM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                            <div style="float: left; width: 1%; color: Black;">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 32%; color: Black;">
                                                <div style="float: left; width: 40%;">
                                                    Repair Estimate
                                                </div>
                                                <div style="float: left; width: 60%;">
                                                    <asp:TextBox ID="TextBoxRepaireEsti" runat="server" CssClass="TextBox" onKeyPress="return numbersonly(event,true)"
                                                        Style="text-align: right;"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="PanelCustomerSettlement" runat="server" Visible="false">
                                <div style="float: left; width: 100%; color: Black;">
                                    <div style="float: left; width: 1%; color: Black;">
                                        &nbsp;
                                    </div>
                                    <div style="float: left; width: 32%; color: Black;">
                                        <div style="float: left; width: 30%;">
                                            Select Registration No
                                        </div>
                                        <div style="float: left; width: 70%;">
                                            <asp:DropDownList ID="DropDownListCSRegNo" runat="server" OnSelectedIndexChanged="DropDownListCSRegNo_SelectedIndexChanged"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div style="float: left; width: 100%; color: Black; height: 10px;">
                                    &nbsp;
                                </div>
                                <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" runat="server" TargetControlID="PanelGrid2"
                                    CollapsedSize="0" ExpandControlID="Image19" CollapseControlID="Image19" Collapsed="true"
                                    AutoCollapse="False" AutoExpand="False" ScrollContents="false" ExpandDirection="Vertical"
                                    ImageControlID="Image19" ExpandedImage="~/Images/16 X 16 UpArrow.jpg" CollapsedImage="~/Images/16 X 16 DownArrow.jpg"
                                    BehaviorID="collapsibleBehavior">
                                </asp:CollapsiblePanelExtender>
                                <div style="background-color: #1E4A9F; color: White; width: 98%; float: left; height: 18px;">
                                </div>
                                <div style="float: left;">
                                    <asp:Image runat="server" ID="Image19" ImageUrl="~/Images/16 X 16 DownArrow.jpg"
                                        ImageAlign="Right" /></div>
                                <div style="float: left; width: 100%;">
                                    <asp:Panel ID="PanelGrid2" runat="server">
                                        <div style="float: left; width: 100%;">
                                            <asp:GridView ID="GridViewCSPreviousClaim" runat="server" Width="100%" ShowHeaderWhenEmpty="True"
                                                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="Both"
                                                CssClass="GridView" OnSelectedIndexChanged="GridViewCSPreviousClaim_SelectedIndexChanged">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgSelect" runat="server" CommandName="SELECT" ImageUrl="~/Images/download_arrow_icon.png"
                                                                Height="15px" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="20px" Height="15px" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Acc Date" DataField="Acc_date" DataFormatString='<%$ appSettings:FormatToDate %>'>
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Veh No" DataField="Reg_no" />
                                                    <asp:BoundField HeaderText="Dri Name" DataField="Dri_name" />
                                                    <asp:BoundField HeaderText="Dri Lic" DataField="Dl_lic" />
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
                                <div style="float: left; width: 100%; height: 15px; color: Black;">
                                    &nbsp;
                                </div>
                                <div style="float: left; width: 100%; color: Black;">
                                    <asp:Panel ID="Panel10" runat="server" GroupingText="Customer Details" CssClass="Panel">
                                        <div style="float: left; width: 100%; color: Black; height: 10px;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                            <div style="float: left; width: 1%; color: Black;">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 32%; color: Black;">
                                                <div style="float: left; width: 30%;">
                                                    Customer Title
                                                </div>
                                                <div style="float: left; width: 70%;">
                                                    <asp:Label ID="LabelCSCusTi" runat="server"></asp:Label>
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
                                                            <asp:Label ID="LabelCSLastName" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div style="float: left; width: 1%; color: Black;">
                                                        &nbsp;
                                                    </div>
                                                    <div style="float: left; width: 32%; color: Black;">
                                                        <div style="float: left; width: 30%;">
                                                            Address Line 1
                                                        </div>
                                                        <div style="float: left; width: 70%;">
                                                            <asp:Label ID="LabelCSAddLin1" runat="server"></asp:Label>
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
                                                            <asp:Label ID="LabelCSCity" runat="server"></asp:Label>
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
                                                            Full Name
                                                        </div>
                                                        <div style="float: left; width: 70%;">
                                                            <asp:Label ID="LabelCSFullName" runat="server"></asp:Label>
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
                                                            <asp:Label ID="LabelCSAddlin2" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div style="float: left; width: 1%; color: Black;">
                                                        &nbsp;
                                                    </div>
                                                    <div style="float: left; width: 32%; color: Black;">
                                                        <div style="float: left; width: 30%;">
                                                            District
                                                        </div>
                                                        <div style="float: left; width: 70%;">
                                                            <asp:Label ID="LabelCSDistrict" runat="server"></asp:Label>
                                                            <%--<asp:TextBox ID="TextBoxDistrct" runat="server" CssClass="TextBox TextBoxUpper"></asp:TextBox>--%>
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
                                                            Initials
                                                        </div>
                                                        <div style="float: left; width: 70%;">
                                                            <asp:Label ID="LabelCSInitials" runat="server"></asp:Label>
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
                                                            <asp:Label ID="LabelCSContact" runat="server"></asp:Label>
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
                                                            <asp:Label ID="LabelCSProvince" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                    </asp:Panel>
                                    <div style="float: left; width: 100%; color: Black;">
                                        <asp:Panel ID="Panel13" runat="server" GroupingText="Settlement Details" CssClass="Panel">
                                            <div style="float: left; width: 100%; color: Black; height: 10px;">
                                                &nbsp;
                                            </div>
                                            <div style="float: left; width: 100%; color: Black;">
                                                <div style="float: left; width: 1%; color: Black;">
                                                    &nbsp;
                                                </div>
                                                <div style="float: left; width: 32%; color: Black;">
                                                    <div style="float: left; width: 40%;">
                                                        Claim Amount
                                                    </div>
                                                    <div style="float: left; width: 60%;">
                                                        <asp:TextBox ID="TextBoxClaimAmo" runat="server" CssClass="TextBox" onKeyPress="return numbersonly(event,true)"
                                                            Style="text-align: right;"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div style="float: left; width: 1%; color: Black;">
                                                    &nbsp;
                                                </div>
                                                <div style="float: left; width: 32%; color: Black;">
                                                    <div style="float: left; width: 40%;">
                                                        Policy Excess
                                                    </div>
                                                    <div style="float: left; width: 60%;">
                                                        <asp:TextBox ID="TextBoxPolicyExcess" runat="server" CssClass="TextBox" onKeyPress="return numbersonly(event,true)"
                                                            Style="text-align: right;"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div style="float: left; width: 1%; color: Black;">
                                                    &nbsp;
                                                </div>
                                                <div style="float: left; width: 32%; color: Black;">
                                                    <div style="float: left; width: 40%;">
                                                        Other Deduction
                                                    </div>
                                                    <div style="float: left; width: 60%;">
                                                        <asp:TextBox ID="TextBoxOtherDeduction" runat="server" CssClass="TextBox" onKeyPress="return numbersonly(event,true)"
                                                            Style="text-align: right;"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                                <div style="float: left; width: 1%; color: Black;">
                                                    &nbsp;
                                                </div>
                                                <div style="float: left; width: 32%; color: Black;">
                                                    <div style="float: left; width: 40%;">
                                                        Balance Value
                                                    </div>
                                                    <div style="float: left; width: 60%;">
                                                        <asp:TextBox ID="TextBoxBalValue" runat="server" CssClass="TextBox" onKeyPress="return numbersonly(event,true)"
                                                            Style="text-align: right;"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div style="float: left; width: 1%; color: Black;">
                                                    &nbsp;
                                                </div>
                                                <div style="float: left; width: 32%; color: Black;">
                                                    <div style="float: left; width: 40%;">
                                                        Account No
                                                    </div>
                                                    <div style="float: left; width: 60%;">
                                                        <asp:TextBox ID="TextBoxAccNo" runat="server" CssClass="TextBox"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div style="float: left; width: 1%; color: Black;">
                                                    &nbsp;
                                                </div>
                                                <div style="float: left; width: 32%; color: Black;">
                                                    <div style="float: left; width: 40%;">
                                                        Cheque No
                                                    </div>
                                                    <div style="float: left; width: 60%;">
                                                        <asp:TextBox ID="TextBoxCheNo" runat="server" CssClass="TextBox"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                                <div style="float: left; width: 1%; color: Black;">
                                                    &nbsp;
                                                </div>
                                                <div style="float: left; width: 32%; color: Black;">
                                                    <div style="float: left; width: 40%;">
                                                        Cheque Bank
                                                    </div>
                                                    <div style="float: left; width: 60%;">
                                                        <asp:TextBox ID="TextBoxCheBank" runat="server" CssClass="TextBox"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div style="float: left; width: 1%; color: Black;">
                                                    &nbsp;
                                                </div>
                                                <div style="float: left; width: 32%; color: Black;">
                                                    <div style="float: left; width: 40%;">
                                                        Cheque Branch
                                                    </div>
                                                    <div style="float: left; width: 60%;">
                                                        <asp:TextBox ID="TextBoxCheBranch" runat="server" CssClass="TextBox"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div style="float: left; width: 1%; color: Black;">
                                                    &nbsp;
                                                </div>
                                                <div style="float: left; width: 32%; color: Black;">
                                                    <div style="float: left; width: 40%;">
                                                        Cheque Date
                                                    </div>
                                                    <div style="float: left; width: 60%;">
                                                        <asp:TextBox ID="TextBoxCheDate" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                                                        <asp:Image ID="Image16" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                                        <asp:CalendarExtender ID="CalendarExtender17" runat="server" TargetControlID="TextBoxCheDate"
                                                            PopupButtonID="Image16" Enabled="True" Format="dd/MMM/yyyy">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 100%; color: Black; padding-top: 2px;">
                                                <div style="float: left; width: 1%; color: Black;">
                                                    &nbsp;
                                                </div>
                                                <div style="float: left; width: 32%; color: Black;">
                                                    <div style="float: left; width: 40%;">
                                                        Cheque Value
                                                    </div>
                                                    <div style="float: left; width: 60%;">
                                                        <asp:TextBox ID="TextBoxCheVal" runat="server" CssClass="TextBox" Style="text-align: right;"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </asp:Panel>
                    </asp:Panel>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
