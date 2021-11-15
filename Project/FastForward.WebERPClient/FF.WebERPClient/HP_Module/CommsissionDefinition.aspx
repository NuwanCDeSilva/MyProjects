<%@ Page Title="Commission Definition" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="CommsissionDefinition.aspx.cs" Inherits="FF.WebERPClient.HP_Module.CommsissionDefinition" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControls/uc_ProfitCenterSearch.ascx" TagName="uc_ProfitCenterSearch"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
    <%--    <asp:UpdatePanel ID="updtMainPnl" runat="server">
    <ContentTemplate>--%>
    <div style="text-align: right">
        &nbsp;
        <asp:Button ID="btnProcess" runat="server" CssClass="Button" Font-Bold="True" Text="Process"
            OnClick="btnProcess_Click" ValidationGroup="commRt" />
        &nbsp;
        <asp:Button ID="btnClone" runat="server" CssClass="Button" Text="Clone" Font-Overline="False"
            Font-Underline="True" OnClick="btnClone_Click" />
        &nbsp;
        <asp:Button ID="btnClear" runat="server" CssClass="Button" Text="Clear" OnClick="btnClear_Click" />
        &nbsp;<asp:Button ID="btnClose" runat="server" Text="Close" CssClass="Button" OnClick="btnClose_Click" />
    </div>
    <div id="divMain1" style="color: Black;">
        <asp:Panel ID="Panel_divMain1" runat="server" BackColor="#CCCCCC" GroupingText=" ">
            <div style="padding: 0.5px; float: left; width: 95%;">
            </div>
            <div style="float: left; width: 100%;">
                <div style="padding: 1.0px; float: left; width: 1%;">
                </div>
                <div style="float: left; width: 15%;">
                    <asp:Label ID="Label1" runat="server" Text="Circular Code :" Font-Bold="True"></asp:Label>
                </div>
                <div style="float: left; width: 15%;">
                    <asp:Label ID="Label2" runat="server" Text="Description :" Font-Bold="True"></asp:Label>
                </div>
                <div style="float: left; width: 18%;">
                    <asp:Label ID="Label3" runat="server" Text="Type :" Font-Bold="True"></asp:Label>
                </div>
                <div style="float: left; width: 15%;">
                    <asp:Label ID="Label22" runat="server" Text="From Date :" Font-Bold="True"></asp:Label>
                </div>
                <div style="float: left; width: 15%;">
                    <asp:Label ID="Label39" runat="server" Text="To Date :" Font-Bold="True"></asp:Label>
                </div>
            </div>
            <div style="float: left; width: 100%;">
                <div style="padding: 1.0px; float: left; width: 1%;">
                </div>
                <div style="float: left; width: 15%;">
                    <asp:TextBox ID="txtCircularCd" runat="server" CssClass="TextBoxUpper"></asp:TextBox>
                </div>
                <div style="float: left; width: 15%;">
                    <asp:TextBox ID="txtCircularDes" runat="server" CssClass="TextBoxUpper"></asp:TextBox>
                </div>
                <div style="float: left; width: 18%;">
                    <asp:TextBox ID="txtCircularType" runat="server" CssClass="TextBoxUpper"></asp:TextBox>
                    <asp:ImageButton ID="ImgBtnType" runat="server" 
                        ImageUrl="~/Images/icon_search.png" onclick="ImgBtnType_Click" />
                </div>
                <div style="float: left; width: 15%;">
                    <asp:TextBox ID="txtFromDt" runat="server" CssClass="TextBoxUpper"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtendertxtFromDt" runat="server" Enabled="True"
                        Format="dd/MM/yyyy" TargetControlID="txtFromDt">
                    </asp:CalendarExtender>
                </div>
                <div style="float: left; width: 15%;">
                    <asp:TextBox ID="txtToDt" runat="server" CssClass="TextBoxUpper"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtendertxtToDt" runat="server" Enabled="True"
                        Format="dd/MM/yyyy" TargetControlID="txtToDt">
                    </asp:CalendarExtender>
                </div>
            </div>
            <div style="padding: 0.5px; float: left; width: 95%;">
            </div>
        </asp:Panel>
    </div>
    <%--    <div id="divpb" style="color: Black;">
        <asp:Panel ID="Panel6" runat="server" GroupingText="Price Book/Level Definition">
            
        </asp:Panel>
    </div>--%>
    <div id="divMain2" style="color: Black;">
        <asp:Panel ID="Panel_divMain2" runat="server" GroupingText=" " Style="margin-top: 0px">
            <div style="float: left; width: 100%;">
                <div style="padding: 0.5px; float: left; width: 15%;">
                    <asp:Label ID="Label13" runat="server" Text="Manager Commssion"></asp:Label>
                </div>
                <div style="float: left; width: 9%;">
                    <asp:Label ID="Label5" runat="server" Text="Cash" ForeColor="Blue"></asp:Label>
                </div>
                <div style="float: left; width: 9%;">
                    <asp:Label ID="Label6" runat="server" Text="Credit Card" ForeColor="Blue"></asp:Label>
                </div>
                <div style="float: left; width: 9%;">
                    <asp:Label ID="Label7" runat="server" Text="Credit Card(Promotion)" ForeColor="Blue"></asp:Label>
                </div>
                <%--                <div style="float: left; width: 9%;">
                    <asp:Label ID="Label4" runat="server" Text="Credit Card"></asp:Label>
                </div>--%>
                <div style="float: left; width: 9%;">
                    <asp:Label ID="Label9" runat="server" Text="Cheque" ForeColor="Blue"></asp:Label>
                </div>
                <div style="float: left; width: 9%;">
                    <asp:Label ID="Label10" runat="server" Text="Gift Voucher" ForeColor="Blue"></asp:Label>
                </div>
                <div style="float: left; width: 9%;">
                    <asp:Label ID="Label11" runat="server" Text="DBC" ForeColor="Blue"></asp:Label>
                </div>
                <div style="float: left; width: 9%;">
                    <asp:Label ID="Label12" runat="server" Text="Other" ForeColor="Blue"></asp:Label>
                </div>
                <%--<div style="float: left; width: 9%;">
                    <asp:Label ID="Label13" runat="server" Text="Amount" ForeColor="Blue"></asp:Label>
                </div>--%>
            </div>
            <div style="float: left; width: 100%;">
                <div style="padding: 0.5px; float: left; width: 15%;" align="left">
                    <asp:Label ID="Label8" runat="server" Text="Rate:"></asp:Label>
                   
                </div>
                <div style="float: left; width: 9%;">
                     
                    <asp:TextBox ID="txtCashComRt" runat="server" CssClass="TextBox"  Width="90%" onkeypress="return isNumberKeyAndDot(event,this.value)"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*"
                        ControlToValidate="txtCashComRt" ForeColor="Red" ValidationExpression="^([1-9][\.\d]*(,\d+)?|0)$"
                        ValidationGroup="commRt" SetFocusOnError="True"></asp:RegularExpressionValidator>
                </div>
                <div style="float: left; width: 9%;">
                    <asp:TextBox ID="txtCrCdComRt" runat="server" CssClass="TextBox" Width="90%" onkeypress="return isNumberKeyAndDot(event,this.value)"></asp:TextBox>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="*"
                        ControlToValidate="txtCrCdComRt" ForeColor="Red" ValidationExpression="^([1-9][\.\d]*(,\d+)?|0)$"
                        ValidationGroup="commRt" SetFocusOnError="True"></asp:RegularExpressionValidator>
                </div>
                <div style="float: left; width: 9%;">
                    <asp:TextBox ID="txtCrCdProComRt" runat="server" CssClass="TextBox" Width="90%" onkeypress="return isNumberKeyAndDot(event,this.value)"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                        ErrorMessage="*" ControlToValidate="txtCrCdProComRt" 
                        ForeColor="Red" ValidationExpression="^([1-9][\.\d]*(,\d+)?|0)$" ValidationGroup="commRt" SetFocusOnError="True"></asp:RegularExpressionValidator> 
                </div>
                <%--                <div style="float: left; width: 9%;">
                    <asp:TextBox ID="TextBox5" runat="server" CssClass="TextBox" Width="90%"></asp:TextBox>
                </div>--%>
                <div style="float: left; width: 9%;">
                    <asp:TextBox ID="txtChqComRt" runat="server" CssClass="TextBox" Width="90%" onkeypress="return isNumberKeyAndDot(event,this.value)"></asp:TextBox>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                        ErrorMessage="*" ControlToValidate="txtChqComRt" 
                        ForeColor="Red" ValidationExpression="^([1-9][\.\d]*(,\d+)?|0)$" ValidationGroup="commRt" SetFocusOnError="True"></asp:RegularExpressionValidator> 
                </div>
                <div style="float: left; width: 9%;">
                    <asp:TextBox ID="txtGVComRt" runat="server" CssClass="TextBox" Width="90%" onkeypress="return isNumberKeyAndDot(event,this.value)"></asp:TextBox>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" 
                        ErrorMessage="*" ControlToValidate="txtGVComRt" 
                        ForeColor="Red" ValidationExpression="^([1-9][\.\d]*(,\d+)?|0)$" ValidationGroup="commRt" SetFocusOnError="True"></asp:RegularExpressionValidator>
                </div>
                <div style="float: left; width: 9%;">
                    <asp:TextBox ID="txtDBCComRt" runat="server" CssClass="TextBox" Width="90%" onkeypress="return isNumberKeyAndDot(event,this.value)"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" 
                        ErrorMessage="*" ControlToValidate="txtDBCComRt" 
                        ForeColor="Red" ValidationExpression="^([1-9][\.\d]*(,\d+)?|0)$" ValidationGroup="commRt" SetFocusOnError="True"></asp:RegularExpressionValidator>
                </div>
                <div style="float: left; width: 9%;">
                    <asp:TextBox ID="txtOthComRt" runat="server" CssClass="TextBox" Width="90%" onkeypress="return isNumberKeyAndDot(event,this.value)"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" 
                        ErrorMessage="*" ControlToValidate="txtOthComRt" 
                        ForeColor="Red" ValidationExpression="^([1-9][\.\d]*(,\d+)?|0)$" ValidationGroup="commRt" 
                        SetFocusOnError="True"></asp:RegularExpressionValidator>
                </div>
                <div style="float: left; width: 15%;">
                </div>
            </div>
            <div style="float: left; width: 100%;">
                <div style="padding: 0.5px; float: left; width: 15%;" align="left">
                    <asp:Label ID="Label14" runat="server" Text="Amount :"></asp:Label>
                </div>
                <div style="float: left; width: 9%;">
                    <asp:TextBox ID="txtCashComAmt" runat="server" CssClass="TextBox" Width="90%" onkeypress="return isNumberKeyAndDot(event,this.value)"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" 
                        ErrorMessage="*" ControlToValidate="txtCashComAmt" 
                        ForeColor="Red" ValidationExpression="^([1-9][\.\d]*(,\d+)?|0)$" ValidationGroup="commRt" 
                        SetFocusOnError="True"></asp:RegularExpressionValidator>
                </div>
                <div style="float: left; width: 9%;">
                    <asp:TextBox ID="txtCrCdComAmt" runat="server" CssClass="TextBox" Width="90%" onkeypress="return isNumberKeyAndDot(event,this.value)"></asp:TextBox>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" 
                        ErrorMessage="*" ControlToValidate="txtCrCdComAmt" 
                        ForeColor="Red" ValidationExpression="^([1-9][\.\d]*(,\d+)?|0)$" ValidationGroup="commRt" 
                        SetFocusOnError="True"></asp:RegularExpressionValidator>
                </div>
                <div style="float: left; width: 9%;">
                    <asp:TextBox ID="txtCrCdProComAmt" runat="server" CssClass="TextBox" Width="90%" onkeypress="return isNumberKeyAndDot(event,this.value)"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" 
                        ErrorMessage="*" ControlToValidate="txtCrCdProComAmt" 
                        ForeColor="Red" ValidationExpression="^([1-9][\.\d]*(,\d+)?|0)$" ValidationGroup="commRt" 
                        SetFocusOnError="True"></asp:RegularExpressionValidator>
                </div>
                <div style="float: left; width: 9%;">
                    <asp:TextBox ID="txtChqComAmt" runat="server" CssClass="TextBox" Width="90%" onkeypress="return isNumberKeyAndDot(event,this.value)"></asp:TextBox>
                      <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" 
                        ErrorMessage="*" ControlToValidate="txtChqComAmt" 
                        ForeColor="Red" ValidationExpression="^([1-9][\.\d]*(,\d+)?|0)$" ValidationGroup="commRt" 
                        SetFocusOnError="True"></asp:RegularExpressionValidator>
                </div>
                <div style="float: left; width: 9%;">
                    <asp:TextBox ID="txtGVComAmt" runat="server" CssClass="TextBox" Width="90%" onkeypress="return isNumberKeyAndDot(event,this.value)"></asp:TextBox>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" 
                        ErrorMessage="*" ControlToValidate="txtGVComAmt" 
                        ForeColor="Red" ValidationExpression="^([1-9][\.\d]*(,\d+)?|0)$" ValidationGroup="commRt" 
                        SetFocusOnError="True"></asp:RegularExpressionValidator>
                </div>
                <div style="float: left; width: 9%;">
                    <asp:TextBox ID="txtDBCComAmt" runat="server" CssClass="TextBox" Width="90%" onkeypress="return isNumberKeyAndDot(event,this.value)"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" 
                        ErrorMessage="*" ControlToValidate="txtDBCComAmt" 
                        ForeColor="Red" ValidationExpression="^([1-9][\.\d]*(,\d+)?|0)$" ValidationGroup="commRt" 
                        SetFocusOnError="True"></asp:RegularExpressionValidator>
                </div>
                <div style="float: left; width: 9%;">
                    <asp:TextBox ID="txtOthComAmt" runat="server" CssClass="TextBox" Width="90%" onkeypress="return isNumberKeyAndDot(event,this.value)"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" 
                        ErrorMessage="*" ControlToValidate="txtOthComAmt" 
                        ForeColor="Red" ValidationExpression="^([1-9][\.\d]*(,\d+)?|0)$" ValidationGroup="commRt" 
                        SetFocusOnError="True"></asp:RegularExpressionValidator>
                </div>
                <%--<div style="float: left; width: 9%;">
                    <asp:TextBox ID="txtAmtComAmt" runat="server" CssClass="TextBox" Width="90%"></asp:TextBox>
                </div>--%>
                <%--                <div style="float: left; width: 9%;">
                    <asp:TextBox ID="TextBox19" runat="server" CssClass="TextBox" Width="90%"></asp:TextBox>
                </div>--%>
            </div>
        </asp:Panel>
    </div>
    <div id="div1" style="color: Black; width: 100%;">
        <asp:Panel ID="Panel5" runat="server" GroupingText=" ">
            <div style="float: left; width: 100%;">
                <div style="padding: 0.5px; float: left; width: 15%;">
                    <asp:Label ID="Label27" runat="server" Text="Excecutive Commssion"></asp:Label>
                </div>
                <div style="float: left; width: 9%;">
                    <asp:Label ID="Label19" runat="server" Text="Cash" ForeColor="Blue"></asp:Label>
                </div>
                <div style="float: left; width: 9%;">
                    <asp:Label ID="Label20" runat="server" Text="Credit Card" ForeColor="Blue"></asp:Label>
                </div>
                <div style="float: left; width: 9%;">
                    <asp:Label ID="Label21" runat="server" Text="Credit Card(Promotion)" ForeColor="Blue"></asp:Label>
                </div>
                <%--                <div style="float: left; width: 9%;">
                    <asp:Label ID="Label22" runat="server" Text="Credit Card"></asp:Label>
                </div>--%>
                <div style="float: left; width: 9%;">
                    <asp:Label ID="Label23" runat="server" Text="Cheque" ForeColor="Blue"></asp:Label>
                </div>
                <div style="float: left; width: 9%;">
                    <asp:Label ID="Label24" runat="server" Text="Gift Voucher" ForeColor="Blue"></asp:Label>
                </div>
                <div style="float: left; width: 9%;">
                    <asp:Label ID="Label25" runat="server" Text="DBC" ForeColor="Blue"></asp:Label>
                </div>
                <div style="float: left; width: 9%;">
                    <asp:Label ID="Label26" runat="server" Text="Other" ForeColor="Blue"></asp:Label>
                </div>
                <%-- <div style="float: left; width: 9%;">
                    <asp:Label ID="Label27" runat="server" Text="Amount" ForeColor="Blue"></asp:Label>
                </div>--%>
            </div>
            <div style="float: left; width: 100%;">
                <div style="padding: 0.5px; float: left; width: 15%;" align="left">
                    <asp:Label ID="Label28" runat="server" Text="Rate:"></asp:Label>
                </div>
                <div style="float: left; width: 9%;">
                    <asp:TextBox ID="txtExCashComRt" runat="server" CssClass="TextBox" Width="90%" onkeypress="return isNumberKeyAndDot(event,this.value)"></asp:TextBox>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" 
                        ErrorMessage="*" ControlToValidate="txtExCashComRt" 
                        ForeColor="Red" ValidationExpression="^([1-9][\.\d]*(,\d+)?|0)$" ValidationGroup="commRt" 
                        SetFocusOnError="True"></asp:RegularExpressionValidator>
                </div>
                <div style="float: left; width: 9%;">
                    <asp:TextBox ID="txtExCrCdComRt" runat="server" CssClass="TextBox" Width="90%" onkeypress="return isNumberKeyAndDot(event,this.value)"></asp:TextBox>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server" 
                        ErrorMessage="*" ControlToValidate="txtExCrCdComRt" 
                        ForeColor="Red" ValidationExpression="^([1-9][\.\d]*(,\d+)?|0)$" ValidationGroup="commRt" 
                        SetFocusOnError="True"></asp:RegularExpressionValidator>
                </div>
                <div style="float: left; width: 9%;">
                    <asp:TextBox ID="txtExCrCdProComRt" runat="server" CssClass="TextBox" Width="90%" onkeypress="return isNumberKeyAndDot(event,this.value)"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server" 
                        ErrorMessage="*" ControlToValidate="txtExCrCdProComRt" 
                        ForeColor="Red" ValidationExpression="^([1-9][\.\d]*(,\d+)?|0)$" ValidationGroup="commRt" 
                        SetFocusOnError="True"></asp:RegularExpressionValidator>
                </div>
                <%--                <div style="float: left; width: 9%;">
                    <asp:TextBox ID="TextBox26" runat="server" CssClass="TextBox" Width="90%"></asp:TextBox>
                </div>--%>
                <div style="float: left; width: 9%;">
                    <asp:TextBox ID="txtExChqComRt" runat="server" CssClass="TextBox" Width="90%" onkeypress="return isNumberKeyAndDot(event,this.value)"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator18" runat="server" 
                        ErrorMessage="*" ControlToValidate="txtExChqComRt" 
                        ForeColor="Red" ValidationExpression="^([1-9][\.\d]*(,\d+)?|0)$" ValidationGroup="commRt" 
                        SetFocusOnError="True"></asp:RegularExpressionValidator>
                </div>
                <div style="float: left; width: 9%;"> 
                    <asp:TextBox ID="txtExGVComRt" runat="server" CssClass="TextBox" Width="90%" onkeypress="return isNumberKeyAndDot(event,this.value)"></asp:TextBox>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator19" runat="server" 
                        ErrorMessage="*" ControlToValidate="txtExGVComRt" 
                        ForeColor="Red" ValidationExpression="^([1-9][\.\d]*(,\d+)?|0)$" ValidationGroup="commRt" 
                        SetFocusOnError="True"></asp:RegularExpressionValidator>
                </div>
                <div style="float: left; width: 9%;">
                    <asp:TextBox ID="txtExDBCComRt" runat="server" CssClass="TextBox" Width="90%" onkeypress="return isNumberKeyAndDot(event,this.value)"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator20" runat="server" 
                        ErrorMessage="*" ControlToValidate="txtExDBCComRt" 
                        ForeColor="Red" ValidationExpression="^([1-9][\.\d]*(,\d+)?|0)$" ValidationGroup="commRt" 
                        SetFocusOnError="True"></asp:RegularExpressionValidator>
                </div>
                <div style="float: left; width: 9%;">
                    <asp:TextBox ID="txtExOthComRt" runat="server" CssClass="TextBox" Width="90%" onkeypress="return isNumberKeyAndDot(event,this.value)"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator21" runat="server" 
                        ErrorMessage="*" ControlToValidate="txtExOthComRt" 
                        ForeColor="Red" ValidationExpression="^([1-9][\.\d]*(,\d+)?|0)$" ValidationGroup="commRt" 
                        SetFocusOnError="True"></asp:RegularExpressionValidator>
                </div>
                <%--<div style="float: left; width: 9%;">
                    <asp:TextBox ID="txtExAmtComRt" runat="server" CssClass="TextBox" Width="90%"></asp:TextBox>
                </div>--%>
            </div>
            <div style="float: left; width: 100%;">
                <div style="padding: 0.5px; float: left; width: 15%;" align="left">
                    <asp:Label ID="Label29" runat="server" Text="Amount :"></asp:Label>
                </div>
                <div style="float: left; width: 9%;">
                    <asp:TextBox ID="txtExCashComAmt" runat="server" CssClass="TextBox" Width="90%" onkeypress="return isNumberKeyAndDot(event,this.value)"></asp:TextBox>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator22" runat="server" 
                        ErrorMessage="*" ControlToValidate="txtExCashComAmt" 
                        ForeColor="Red" ValidationExpression="^([1-9][\.\d]*(,\d+)?|0)$" ValidationGroup="commRt" 
                        SetFocusOnError="True"></asp:RegularExpressionValidator>
                </div>
                <div style="float: left; width: 9%;">
                    <asp:TextBox ID="txtExCrCdComAmt" runat="server" CssClass="TextBox" Width="90%" onkeypress="return isNumberKeyAndDot(event,this.value)"></asp:TextBox>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator23" runat="server" 
                        ErrorMessage="*" ControlToValidate="txtExCrCdComAmt" 
                        ForeColor="Red" ValidationExpression="^([1-9][\.\d]*(,\d+)?|0)$" ValidationGroup="commRt" 
                        SetFocusOnError="True"></asp:RegularExpressionValidator>
                </div>
                <div style="float: left; width: 9%;">
                    <asp:TextBox ID="txtExCrCdProComAmt" runat="server" CssClass="TextBox" Width="90%" onkeypress="return isNumberKeyAndDot(event,this.value)"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator24" runat="server" 
                        ErrorMessage="*" ControlToValidate="txtExCrCdProComAmt" 
                        ForeColor="Red" ValidationExpression="^([1-9][\.\d]*(,\d+)?|0)$" ValidationGroup="commRt" 
                        SetFocusOnError="True"></asp:RegularExpressionValidator>
                </div>
                <div style="float: left; width: 9%;">
                    <asp:TextBox ID="txtExChqComAmt" runat="server" CssClass="TextBox" Width="90%" onkeypress="return isNumberKeyAndDot(event,this.value)"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator25" runat="server" 
                        ErrorMessage="*" ControlToValidate="txtExChqComAmt" 
                        ForeColor="Red" ValidationExpression="^([1-9][\.\d]*(,\d+)?|0)$" ValidationGroup="commRt" 
                        SetFocusOnError="True"></asp:RegularExpressionValidator>
                </div>
                <div style="float: left; width: 9%;">
                    <asp:TextBox ID="txtExGVComAmt" runat="server" CssClass="TextBox" Width="90%" onkeypress="return isNumberKeyAndDot(event,this.value)"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator26" runat="server" 
                        ErrorMessage="*" ControlToValidate="txtExGVComAmt" 
                        ForeColor="Red" ValidationExpression="^([1-9][\.\d]*(,\d+)?|0)$" ValidationGroup="commRt" 
                        SetFocusOnError="True"></asp:RegularExpressionValidator>
                </div>
                <div style="float: left; width: 9%;">
                    <asp:TextBox ID="txtExDBCComAmt" runat="server" CssClass="TextBox" Width="90%" onkeypress="return isNumberKeyAndDot(event,this.value)"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator27" runat="server" 
                        ErrorMessage="*" ControlToValidate="txtExDBCComAmt" 
                        ForeColor="Red" ValidationExpression="^([1-9][\.\d]*(,\d+)?|0)$" ValidationGroup="commRt" 
                        SetFocusOnError="True"></asp:RegularExpressionValidator>
                </div>
                <div style="float: left; width: 9%;">
                    <asp:TextBox ID="txtExOthComAmt" runat="server" CssClass="TextBox" Width="90%" onkeypress="return isNumberKeyAndDot(event,this.value)"></asp:TextBox>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator28" runat="server" 
                        ErrorMessage="*" ControlToValidate="txtExOthComAmt" 
                        ForeColor="Red" ValidationExpression="^([1-9][\.\d]*(,\d+)?|0)$" ValidationGroup="commRt" 
                        SetFocusOnError="True"></asp:RegularExpressionValidator>
                </div>
                <%--   <div style="float: left; width: 9%;">
                    <asp:TextBox ID="txtExAmtComAmt" runat="server" CssClass="TextBox" Width="90%"></asp:TextBox>
                </div>--%>
                <%--                <div style="float: left; width: 9%;">
                    <asp:TextBox ID="TextBox40" runat="server" CssClass="TextBox" Width="90%"></asp:TextBox>
                </div>--%>
            </div>
        </asp:Panel>
    </div>
    <div style="float: left; width: 98%;">
        &nbsp;</div>
    <div style="float: left; width: 100%;">
        <div style="padding: 1px; float: left; width: 2%;">
        </div>
        <div style="float: left; width: 45%;">
            <asp:Panel ID="Panel2" runat="server" GroupingText=" ">
                <div style="float: left; width: 100%; background-color: #3333FF;">
                    <div style="color: #FFFFFF; font-weight: bold; background-color: #0066CC;">
                        Price Book Details</div>
                </div>
                <div style="padding: 1.5px; float: left; width: 100%;">
                </div>
                <div style="float: left; width: 100%;">
                    <div style="float: left; width: 20%;">
                        <asp:Label ID="Label30" runat="server" Text="Price Book" ForeColor="Black" Font-Size="X-Small"></asp:Label>
                    </div>
                    <div style="float: left; width: 26%;">
                        <asp:TextBox ID="txtPriceBook" runat="server" CssClass="TextBox" Width="70%"></asp:TextBox>
                        &nbsp;
                        <asp:ImageButton ID="imgBtnPBsearch" runat="server" ImageUrl="~/Images/icon_search.png"
                            OnClick="imgBtnPBsearch_Click1" />
                    </div>
                    <div style="float: left; width: 17%;">
                        <asp:Label ID="Label31" runat="server" Text="Book Level" ForeColor="Black" Font-Size="X-Small"></asp:Label>
                    </div>
                    <div style="float: left; width: 30%;">
                        <asp:TextBox ID="txtLevel" runat="server" CssClass="TextBox" Width="70%"></asp:TextBox>
                        &nbsp;
                        <asp:ImageButton ID="imgBtnLevelSearch" runat="server" ImageUrl="~/Images/icon_search.png"
                            OnClick="imgBtnLevelSearch_Click1" />
                    </div>
                    <div style="float: left; width: 5%;" align="left">
                        <asp:ImageButton ID="ImgBtnAddPB" runat="server" ImageUrl="~/Images/download_arrow_icon.png"
                            Width="100%" OnClick="ImgBtnAddPB_Click" />
                    </div>
                    <div style="float: left; width: 100%; height: 106px;">
                        <asp:Panel ID="Panel7" runat="server" ScrollBars="Both" Style="margin-bottom: 20px"
                            Height="95px">
                            <asp:GridView ID="grvPB_PBL" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                CellPadding="4" CssClass="GridView" ForeColor="#333333" Width="98%" OnRowDeleting="grvPB_PBL_RowDeleting">
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
                                            <asp:ImageButton ID="ImgBtnGrvDelPB" runat="server" CommandName="Delete" ImageUrl="~/Images/Delete.png" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Price Book" DataField="Sapl_pb" />
                                    <asp:BoundField HeaderText="Price book Level" DataField="Sapl_pb_lvl_cd" />
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
            </asp:Panel>
        </div>
        <div style="padding: 1px; float: left; width: 5%;">
            &nbsp;</div>
        <div style="float: left; width: 45%;">
            <asp:Panel ID="Panel11" runat="server" GroupingText=" ">
                <div style="float: left; width: 100%; background-color: #3333FF;">
                    <div style="color: #FFFFFF; font-weight: bold; background-color: #0066CC;">
                        Excecutive Details</div>
                </div>
                <div style="padding: 1.5px; float: left; width: 100%;">
                </div>
                <div style="float: left; width: 100%;">
                    <div style="float: left; width: 20%;">
                        <asp:Label ID="Label17" runat="server" Text="Excecutive Type" ForeColor="Black" Font-Size="X-Small"></asp:Label>
                    </div>
                    <div style="float: left; width: 27%;">
                        <asp:TextBox ID="txtExcecType" runat="server" CssClass="TextBox" Width="70%"></asp:TextBox>
                        &nbsp;<asp:ImageButton ID="ImgBtnExecTp" runat="server" 
                            ImageUrl="~/Images/icon_search.png" onclick="ImgBtnExecTp_Click" />
                    </div>
                    <div style="float: left; width: 20%;">
                        <asp:Label ID="Label18" runat="server" Text="Excecutive Code" ForeColor="Black" Font-Size="X-Small"></asp:Label>
                    </div>
                    <div style="float: left; width: 25%;">
                        <asp:TextBox ID="txtExcecCd" runat="server" CssClass="TextBox" Width="70%"></asp:TextBox>
                        &nbsp;<asp:ImageButton ID="ImgBtnExecCd" runat="server" 
                            ImageUrl="~/Images/icon_search.png" onclick="ImgBtnExecCd_Click" />
                    </div>
                    <div style="float: left; width: 5%;" align="left">
                        <asp:ImageButton ID="ImgBtnAddExcec" runat="server" ImageUrl="~/Images/download_arrow_icon.png"
                            Width="100%" OnClick="ImgBtnAddExcec_Click" />
                    </div>
                    <div style="float: left; width: 100%; height: 106px;">
                        <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Style="margin-bottom: 20px"
                            Height="95px">
                            <asp:GridView ID="grvExcecutive" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                CellPadding="4" CssClass="GridView" ForeColor="#333333" Width="98%" OnRowDeleting="grvExcecutive_RowDeleting">
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
                                            <asp:ImageButton ID="ImgGrvDelExcec" runat="server" CommandName="Delete" ImageUrl="~/Images/Delete.png" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Excecutive Type" DataField="Sccd_exec_tp" />
                                    <asp:BoundField HeaderText="Excecutive Code" DataField="Sccd_exec_cd" />
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
            </asp:Panel>
        </div>
    </div>
    <%-- <div id="divMain3Grid" style="float: left; width: 100%;">
    </div>--%>
    <div style="float: left; width: 100%; background-color: #FFFFFF;">
        <div style="float: left; width: 45%; background-color: #FFFFFF; font-size: x-small;">
            <asp:Panel ID="Panel3" runat="server" BackColor="White" GroupingText="Alowed Profit Centers">
                <div style="padding: 2.0px; float: left; width: 96%;">
                </div>
                <div style="float: left; width: 65%;" align="left">
                    <uc1:uc_ProfitCenterSearch ID="uc_ProfitCenterSearch1" runat="server" />
                </div>
                <div style="float: left; width: 7%;" align="left">
                    <asp:ImageButton ID="btnAddToPC_list" runat="server" ImageUrl="~/Images/right_arrow_icon.png"
                        Width="70%" OnClick="btnAddToPC_list_Click" ToolTip="Add to Profit Center List" />
                </div>
                <div style="float: left; width: 25%;" align="left">
                    <div style="float: left; width: 100%; text-align: center;">
                        <asp:Panel ID="Panel4" runat="server" Height="150px" ScrollBars="Vertical" BorderColor="Blue"
                            BorderWidth="1px" GroupingText="Profit Centers">
                            <asp:GridView ID="grvProfCents" runat="server" AutoGenerateColumns="False" OnRowDataBound="grvProfCents_RowDataBound"
                                Height="139px">
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
                    </div>
                    <div style="float: left; width: 100%; text-align: right;">
                        <div style="float: left; width: 33%; text-align: right;">
                            <asp:Button ID="btnAll" runat="server" Text="All" CssClass="Button" Width="100%"
                                OnClick="btnAll_Click" Font-Size="XX-Small" />
                        </div>
                        <div style="float: left; width: 33%; text-align: right;">
                            <asp:Button ID="btnNone" runat="server" Text="None" CssClass="Button" OnClick="btnNone_Click"
                                Width="100%" Font-Size="XX-Small" />
                        </div>
                        <div style="float: left; width: 33%; text-align: right;">
                            <asp:Button ID="btnClearPcList" runat="server" Text="Clear" CssClass="Button" OnClick="btnClearPcList_Click"
                                Width="100%" Font-Size="XX-Small" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
        <div style="float: left; width: 54%; background-color: #FFFFFF; font-size: x-small;">
            <asp:Panel ID="Panel8" runat="server" GroupingText="Category Selection" BackColor="White">
                <div style="padding: 2.0px; float: left; width: 96%;">
                </div>
                <div style="float: left; width: 50%;">
                    <div style="float: left; width: 100%; background-color: #FFFFFF;">
                        <div style="float: left; width: 15%;" align="left">
                            <asp:Label ID="Label32" runat="server" Text="Add" Font-Bold="True"></asp:Label>
                        </div>
                        <div style="float: left; width: 65%;">
                            <asp:DropDownList ID="ddlSelectCat" runat="server" CssClass="ComboBox" Font-Size="X-Small"
                                Font-Bold="True" AutoPostBack="True" 
                                onselectedindexchanged="ddlSelectCat_SelectedIndexChanged">
                                <asp:ListItem>BRAND</asp:ListItem>
                                <asp:ListItem Value="CATE1">MAIN CATEGORY</asp:ListItem>
                                <asp:ListItem Value="CATE2">CATEGORY</asp:ListItem>
                                <asp:ListItem>ITEM</asp:ListItem>
                                <asp:ListItem>SERIAL</asp:ListItem>
                                <asp:ListItem>PROMOTION</asp:ListItem>
                                <asp:ListItem Value="BRAND_CATE1">BRAND &amp; M.CATEGORY</asp:ListItem>
                                <asp:ListItem Value="BRAND_CATE2">BRAND &amp; CATEGORY</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div style="float: left; width: 12%;" align="right">
                            <asp:ImageButton ID="ImgBtnAddCat" runat="server" ImageUrl="~/Images/right_arrow_icon.png"
                                Width="80%" OnClick="ImgBtnAddCat_Click" />
                        </div>
                    </div>
                    <div style="float: left; width: 100%;">
                        <div style="float: left; width: 30%;">
                            <asp:Label ID="Label33" runat="server" Text="Brand:"></asp:Label>
                        </div>
                        <div style="float: left; width: 49%;">
                            <asp:TextBox ID="txtBrand" runat="server" CssClass="TextBox" Width="70%"></asp:TextBox>
                            &nbsp;
                            <asp:ImageButton ID="ImgBtnBrand" runat="server" ImageUrl="~/Images/icon_search.png"
                                OnClick="ImgBtnBrand_Click" />
                        </div>
                    </div>
                    <div style="float: left; width: 100%;">
                        <div style="float: left; width: 30%;">
                            <asp:Label ID="Label34" runat="server" Text="Main Category:"></asp:Label>
                        </div>
                        <div style="float: left; width: 49%;">
                            <asp:TextBox ID="txtCate1" runat="server" CssClass="TextBox" Width="70%"></asp:TextBox>
                            &nbsp;
                            <asp:ImageButton ID="ImgBtnMainCat" runat="server" ImageUrl="~/Images/icon_search.png"
                                OnClick="ImageButton3_Click" />
                        </div>
                    </div>
                    <div style="float: left; width: 100%;">
                        <div style="float: left; width: 30%;">
                            <asp:Label ID="Label35" runat="server" Text="Category:"></asp:Label>
                        </div>
                        <div style="float: left; width: 49%;">
                            <asp:TextBox ID="txtCate2" runat="server" CssClass="TextBox" Width="70%"></asp:TextBox>
                            &nbsp;
                            <asp:ImageButton ID="ImgBtnSubCat" runat="server" ImageUrl="~/Images/icon_search.png"
                                OnClick="ImgBtnSubCat_Click" />
                        </div>
                    </div>
                    <div style="float: left; width: 100%;">
                        <div style="float: left; width: 30%;">
                            <asp:Label ID="Label36" runat="server" Text="Item"></asp:Label>
                        </div>
                        <div style="float: left; width: 49%;">
                            <asp:TextBox ID="txtItemCD" runat="server" CssClass="TextBox" Width="70%"></asp:TextBox>
                            &nbsp;
                            <asp:ImageButton ID="ImgBtnItem" runat="server" ImageUrl="~/Images/icon_search.png"
                                Height="16px" OnClick="ImgBtnItem_Click" />
                        </div>
                    </div>
                    <div style="float: left; width: 100%;">
                        <div style="float: left; width: 30%;">
                            <asp:Label ID="Label37" runat="server" Text="Serial"></asp:Label>
                        </div>
                        <div style="float: left; width: 49%;">
                            <asp:TextBox ID="txtSerial" runat="server" CssClass="TextBox" Width="70%"></asp:TextBox>
                            &nbsp;
                            <asp:ImageButton ID="ImgBtnSerial" runat="server" ImageUrl="~/Images/icon_search.png"
                                Height="16px" OnClick="ImgBtnSerial_Click" />
                        </div>
                    </div>
                    <div style="float: left; width: 100%;">
                        <div style="float: left; width: 30%;">
                            <asp:Label ID="Label38" runat="server" Text="Circular #:"></asp:Label>
                        </div>
                        <div style="float: left; width: 49%;">
                            <asp:TextBox ID="txtCircular" runat="server" CssClass="TextBox" Width="70%"></asp:TextBox>
                            &nbsp;
                            <asp:ImageButton ID="ImgBtnCircular" runat="server" ImageUrl="~/Images/icon_search.png"
                                Height="16px" OnClick="ImgBtnCircular_Click" />
                        </div>
                    </div>
                    <div style="float: left; width: 100%;">
                        <div style="float: left; width: 30%;">
                            <asp:Label ID="Label4" runat="server" Text="Promotion:"></asp:Label>
                        </div>
                        <div style="float: left; width: 49%;">
                            <asp:TextBox ID="txtPromotion" runat="server" CssClass="TextBox" Width="70%"></asp:TextBox>
                            &nbsp;
                            <asp:ImageButton ID="ImgBtnPromo" runat="server" ImageUrl="~/Images/icon_search.png"
                                Height="16px" OnClick="ImgBtnPromo_Click" />
                        </div>
                    </div>
                </div>
                <div style="float: left; width: 49%; height: 145px;">
                    <asp:Panel ID="Panel9" runat="server" Height="150px" ScrollBars="Both" Width="100%">
                        <asp:GridView ID="grvItmSelect" runat="server" AutoGenerateColumns="False" CssClass="GridView"
                            ShowHeaderWhenEmpty="True" CellPadding="4" ForeColor="#333333" Width="98%" 
                            onrowdatabound="grvItmSelect_RowDataBound">
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
                                        <asp:CheckBox ID="checkCateSelect" runat="server" Checked="True" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Sccd_itm" HeaderText="Value" />
                                <asp:BoundField HeaderText="Brand" DataField="Sccd_brd" />
                                <asp:BoundField DataField="Sccd_ser" HeaderText="Description" />
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
                    <div style="float: left; width: 100%;">
                        <div style="float: left; width: 40%;" align="left">
                            &nbsp;</div>
                        <div style="float: left; width: 20%;" align="left">
                            <asp:Button ID="btnCatAll" runat="server" Text="All" CssClass="Button" OnClick="btnCatAll_Click"
                                Font-Size="X-Small" Width="100%" />
                        </div>
                        <div style="float: left; width: 20%;" align="left">
                            <asp:Button ID="btnCatNone" runat="server" Text="None" CssClass="Button" OnClick="btnCatNone_Click"
                                Font-Size="X-Small" Width="100%" />
                        </div>
                        <div style="float: left; width: 20%;" align="left">
                            <asp:Button ID="btnCatClear" runat="server" Text="Clear" CssClass="Button" OnClick="btnCatClear_Click"
                                Font-Size="X-Small" Width="100%" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
    <div style="padding: 2.0px; float: left; width: 100%;">
        &nbsp;</div>
    <%--    <div style="float: left; width: 100%;">
        <div style="float: left; width: 79%;">
            &nbsp;</div>
        <div style="float: left; width: 20%;" align="right">
        </div>
    </div>--%>
    <div style="padding: 2.0px; float: left; width: 100%;">
        &nbsp;</div>
    <div style="float: left; width: 100%; background-color: #FFFFFF;">
        <asp:Panel runat="server" BackColor="White" Width="50%" BorderColor="#3399FF" BorderStyle="Solid"
            BorderWidth="3px" ID="Panel_pcCommClone">
            <div style="float: left; width: 100%;">
                <div style="padding: 0.5px; float: left; width: 99%;">
                    <div style="float: left; width: 65%;">
                    </div>
                    <div style="float: left; width: 34%;">
                        <asp:Button ID="btnProcessClone" runat="server" Text="Process" CssClass="Button"
                            OnClick="btnProcessClone_Click" ValidationGroup="clone" />
                        <asp:Button ID="btnCloneClear" runat="server" Text="Clear" CssClass="Button" OnClick="btnCloneClear_Click" />
                        <asp:Button ID="btnCloneCLOSE" runat="server" Text="Close" CssClass="Button" />
                    </div>
                </div>
                <div style="float: left; width: 25%;">
                    <asp:Label ID="Label15" runat="server" Text="Profit Center" Font-Bold="False"></asp:Label>
                </div>
                <div style="float: left; width: 30%;">
                    <asp:TextBox ID="txtClonePC" runat="server" CssClass="TextBoxUpper" Width="99%"></asp:TextBox>
                </div>
                <div style="float: left; width: 40%;">
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ErrorMessage="enter profit center." ControlToValidate="txtClonePC" 
                    ForeColor="Red" ValidationGroup="clone"></asp:RequiredFieldValidator>--%>
                </div>
            </div>
            <div style="float: left; width: 100%;">
                <div style="float: left; width: 25%;">
                    <asp:Label ID="Label16" runat="server" Text="Clone Profit Centers" Font-Bold="False"></asp:Label>
                </div>
                <div style="float: left; width: 30%;">
                    <asp:TextBox ID="txtCloneAddPc" runat="server" CssClass="TextBoxUpper" Width="99%"></asp:TextBox>
                </div>
                <div style="float: left; width: 3%;">
                    <asp:ImageButton ID="ImgBtnCloneAdd" runat="server" ImageUrl="~/Images/download_arrow_icon.png"
                        Width="100%" OnClick="ImgBtnCloneAdd_Click" />
                </div>
                <div style="float: left; width: 30%;">
                </div>
            </div>
            <div style="float: left; width: 100%;">
                <div style="float: left; width: 25%;">
                    &nbsp;&nbsp;</div>
                <div style="float: left; width: 25%;">
                    <asp:GridView ID="grvClonePc" runat="server" ShowHeaderWhenEmpty="True" CssClass="GridView"
                        Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" ShowHeader="False"
                        OnRowDeleting="grvClonePc_RowDeleting">
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
                                        OnClick="ImgDelClone_Click" />
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
                </div>
            </div>
        </asp:Panel>
    </div>
    <asp:ModalPopupExtender ID="ModalPopupCloneCommsis_" runat="server" ClientIDMode="Static"
        CancelControlID="btnCloneCLOSE" BackgroundCssClass="modalBackground" PopupControlID="Panel_pcCommClone"
        TargetControlID="btnHiddencLN">
    </asp:ModalPopupExtender>
    <div style="display: none">
        <asp:Button ID="btnHiddencLN" runat="server" Text="HIDDEN_CLONE" />
    </div>
    <%-- </ContentTemplate>
</asp:UpdatePanel>--%>
</asp:Content>
