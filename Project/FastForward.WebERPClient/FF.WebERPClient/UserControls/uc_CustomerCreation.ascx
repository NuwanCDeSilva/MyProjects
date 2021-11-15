<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_CustomerCreation.ascx.cs"
    Inherits="FF.WebERPClient.UserControls.uc_CustomerCreation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="uc_CustCreationExternalDet.ascx" tagname="uc_CustCreationExternalDet" tagprefix="uc1" %>
<link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
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
<%--Main div--%>
<div style="float: left; width: 100%;">
    <%--  ----------------Save/Update buttons-------------------------%>
    <div style="float: left; width: 100%;" ID="divButtons" runat="server">
        <div style="float: left; width: 70%;">
        </div>
        <div style="float: left; width: 29%; text-align: right;">
            <asp:Button ID="btnCreate" runat="server" Text="Create" CssClass="Button" 
                onclick="btnCreate_Click" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="Button" 
                onclick="btnUpdate_Click" />
        </div>
    </div>
    <div style="float: left; width: 100%;">
        <%--  ---------------- Identification-------------------------%>
        <asp:Panel ID="Panel_Identification" runat="server" ForeColor="Blue" GroupingText="Identification">
            <div style="float: left; width: 100%;">
                <div style="float: left; width: 50%;">
                    <div>
                        <%--//row--%>
                        <div style="padding: 1.0px; float: left; width: 40%;">
                        </div>
                        <div style="float: left; width: 49%;">
                        </div>
                    </div>
                    <div style="float: left; width: 100%;">
                        <%--//row--%>
                        <div style="float: left; width: 40%;">
                            <asp:Label ID="Label1" runat="server" Text="Customer Type" ForeColor="Black"></asp:Label>
                        </div>
                        <div style="padding: 1.0px; float: left; width: 49%;">
                            <asp:DropDownList ID="ddlCustType" runat="server" CssClass="ComboBox">
                                <asp:ListItem>INDIVIDUAL</asp:ListItem>
                                <asp:ListItem>GROUP</asp:ListItem>
                                <asp:ListItem>LEASE</asp:ListItem>
                            </asp:DropDownList>
                            <div> 
                            </div>
                        </div>
                    </div>
                    <div style="float: left; width: 100%;">
                        <%--//row--%>
                        <div style="float: left; width: 40%;">
                            <asp:Label ID="Label2" runat="server" Text="NIC Number" ForeColor="Black"></asp:Label>
                        </div>
                        <div style="float: left; width: 49%;">
                            <asp:TextBox ID="txtNicNo" runat="server" CssClass="TextBoxUpper" 
                                MaxLength="10"></asp:TextBox>
                        </div>
                    </div>
                    <div style="float: left; width: 100%;">
                        <%--//row--%>
                        <div style="float: left; width: 40%;">
                            <asp:Label ID="Label3" runat="server" Text="Passport No" ForeColor="Black"></asp:Label>
                        </div>
                        <div style="float: left; width: 49%;">
                            <asp:TextBox ID="txtPassportNo" runat="server" CssClass="TextBoxUpper" 
                                MaxLength="10"></asp:TextBox>
                        </div>
                    </div>
                    <div style="padding: 1.0px">
                        <%--//row--%>
                        <div style="float: left; width: 40%;">
                            <asp:Label ID="Label4" runat="server" Text="DL No" ForeColor="Black"></asp:Label>
                        </div>
                        <div style="float: left; width: 49%;">
                            <asp:TextBox ID="txtDLno" runat="server" CssClass="TextBoxUpper" MaxLength="10"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div style="float: left; width: 49%;">
                    <div>
                        <%--//row--%>
                        <div style="float: left; width: 30%;">
                        </div>
                        <div style="float: left; width: 49%;">
                        </div>
                    </div>
                    <div>
                        <%--//row--%>
                        <div style="float: left; width: 30%;">
                            <asp:Label ID="Label5" runat="server" Text="Code" ForeColor="Black"></asp:Label>
                        </div>
                        <div style="float: left; width: 59%;">
                            <asp:TextBox ID="txtCustCode" runat="server" CssClass="TextBox"></asp:TextBox>
                        </div>
                    </div>
                
                  <div>
                        <%--//row--%>
                        <div style="float: left; width: 30%;">
                            <asp:Label ID="Label33" runat="server" Text="Mobile" ForeColor="Black"></asp:Label>
                        </div>
                        <div style="float: left; width: 59%;">
                            <asp:TextBox ID="txtMobile" runat="server" CssClass="TextBox" MaxLength="10"></asp:TextBox>
                            <br />
                            <asp:CheckBox ID="chekAgreSendSMS" runat="server" Text="Agreed to send SMS" />
                        </div>
                    </div>
                    <div ID="divBRno" runat="server">
                        <%--//row--%>
                        <div style="float: left; width: 30%;">
                            <asp:Label ID="Label36" runat="server" Text="BR No" ForeColor="Black"></asp:Label>
                        </div>
                        <div style="float: left; width: 59%;">
                            <asp:TextBox ID="txtBrNo" runat="server" CssClass="TextBoxUpper" MaxLength="10"></asp:TextBox>
                        </div>
                    </div>
                </div>
              
                   
                 
            </div>
        </asp:Panel>
        <%--  ---------------- Personal Detail-------------------------%>
        <asp:Panel ID="Panel_PersonalDet" runat="server" ForeColor="Blue" GroupingText="Personal Detail">
            <div>
                <div style="float: left; width: 50%;">
                    <div>
                        <%--//row--%>
                        <div style="float: left; width: 50%;">
                        </div>
                        <div style="float: left; width: 49%;">
                        </div>
                    </div>
                    <div>
                        <%--//row--%>
                        <div style="float: left; width: 40%;">
                            <asp:Label ID="Label12" runat="server" Text="Title" ForeColor="Black"></asp:Label>
                        </div>
                        <div style="float: left; width: 49%;">
                            <asp:DropDownList ID="ddlTitle" runat="server" CssClass="ComboBox">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>Mr.</asp:ListItem>
                                <asp:ListItem>Mrs.</asp:ListItem>
                                <asp:ListItem>Ms.</asp:ListItem>
                                <asp:ListItem>Miss</asp:ListItem>
                                <asp:ListItem>Dr.</asp:ListItem>
                                <asp:ListItem>Rev.</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div>
                        <%--//row--%>
                        <div style="float: left; width: 50%;">
                            <asp:Label ID="Label8" runat="server" Text="Name In Full" ForeColor="Black"></asp:Label>
                        </div>
                        <div style="float: left; width: 149%;">
                            <asp:TextBox ID="txtFirstName" runat="server" Width="130%" CssClass="TextBoxUpper" 
                                MaxLength="200"></asp:TextBox>
                        </div>
                    </div>
                    <div>
                        <%--//row--%>
                        <div style="padding: 1.0px; float: left; width: 100%;">
                        </div>
                    </div>
                    <div>
                        <%--//row--%>
                        <div style="float: left; width: 40%;">
                            <asp:Label ID="Label11" runat="server" Text="Date of Birth" ForeColor="Black"></asp:Label>
                        </div>
                        <div style="padding: 1px; float: left; width: 49%;">
                            <asp:TextBox ID="txtDateOfBirth" runat="server" CssClass="TextBox"></asp:TextBox>
                            <asp:CalendarExtender ID="txtFromDt_CalendarExtender" runat="server" Enabled="True"
                                Format="dd/MM/yyyy" TargetControlID="txtDateOfBirth"></asp:CalendarExtender>
                        </div>
                    </div>
                </div>
                <div style="float: left; width: 49%;">
                    <div>
                        <%--//row--%>
                        <div style="float: left; width: 50%;">
                        </div>
                        <div style="float: left; width: 49%;">
                        </div>
                    </div>
                    <div>
                        <%--//row--%>
                        <div style="float: left; width: 30%;">
                            <asp:Label ID="Label7" runat="server" Text="Sex" ForeColor="Black"></asp:Label>
                        </div>
                        <div style="float: left; width: 49%;">
                            <asp:DropDownList ID="ddlSex" runat="server" CssClass="ComboBox">
                                <asp:ListItem>Male</asp:ListItem>
                                <asp:ListItem>Female</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>

</div>
<%--Validations--%>
<div>
    <%-- <div style="display: none;">--%>
                   
    <%--<asp:Button ID="btnPPno" runat="server" OnClick="ValidatePPno" />--%><%-- <asp:Button ID="btnDLno" runat="server" OnClick="ValidateDLno" />--%>
                    
    <%--<asp:Button ID="btnDisAmt" runat="server" OnClick="CheckDisAmt" />
                    <asp:Button ID="btnTax" runat="server" OnClick="CheckTax" />
                    <asp:Button ID="btnSupplier" runat="server" OnClick="CheckSupplier" />
                    <asp:Button ID="btnPODoc" runat="server" OnClick="CheckPO" />
                    <asp:Button ID="btnItmStatus" runat="server" OnClick="Checkstatus" />--%>
                    
<div style="visibility: hidden">
         <asp:Button ID="btnNIC" runat="server" OnClick="ValidateNIC" />
         <asp:Button ID="btnPhone" runat="server" OnClick="ValidatePhoneNum" />
          <asp:Button ID="btnDL" runat="server" OnClick="ValidateDL" />
          <asp:Button ID="btnPP" runat="server" OnClick="ValidatePP" />

            <asp:Button ID="btnGetbyCustCD" runat="server" 
             onclick="btnGetbyCustCD_Click"/>
        <asp:Button ID="btnGetbyNIC" runat="server" 
                onclick="btnGetbyNIC_Click"/>
        <asp:Button ID="btnGetbyDL" runat="server" 
                 onclick="btnGetbyDL_Click"/>
         <asp:Button ID="btnGetbyPPno" runat="server" 
                    onclick="btnGetbyPPno_Click"/>
                      <asp:Button ID="btnGetbyBrNo" runat="server" 
                    onclick="btnGetbyBrNo_Click"/>
</div>
                    
    <%--   </div>--%>
</div>
