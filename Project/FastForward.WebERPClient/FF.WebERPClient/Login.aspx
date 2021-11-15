<%@ Page Title="" Language="C#" MasterPageFile="~/Basic.Master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="FF.WebERPClient.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <style type="text/css">
        .style1
        {
        }
        .style2
        {
            width: 141px;
        }
        .style3
        {
            color: #0066FF;
        }
        .style4
        {
            color: #006699;
        }
        .style5
        {
            color: #000000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
           function validate(e) { var keycode = (e.which) ? e.which : e.keyCode; if (e.keyCode == 13) { alert("Enter Key"); return false; } }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="divlogin" runat="server" style="padding-left: 35%; padding-top: 5%" visible="true">
                <div>
                    <%-- <table cellpadding="2" cellspacing="0">--%>
                    <table style="line-height: 2; border-spacing: 0;">
                        <tr>
                            <td style="font-size: small" class="style5">
                                User name&nbsp; . . . . . . . .
                            </td>
                            <td colspan="2">
                                &nbsp;<asp:TextBox ID="txtUserName" runat="server" Width="192px" AutoPostBack="true"
                                    OnTextChanged="txtUserName_TextChanged" AutoCompleteType="Disabled"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: small">
                                <span class="style5">Password . . . . . . . . . .</span>
                            </td>
                            <td colspan="2">
                                &nbsp;<asp:TextBox ID="txtPassword" runat="server" Width="192px" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: small">
                                <span class="style5">Company . . . . . . . . . .</span>
                            </td>
                            <td colspan="2">
                                &nbsp;<asp:DropDownList ID="ddlCompany" runat="server" Width="200px" AppendDataBoundItems="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td class="style1">
                                &nbsp;<asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click"
                                    CausesValidation="false" Width="64px" />
                            </td>
                            <td class="style2">
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                    Width="64px" />
                            </td>
                        </tr>
                    </table>
                    <br />
                </div>
                <div>
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Style="margin-left: 0px" Width="70%"></asp:Label>
                </div>
            </div>
            <div id="divChangePassword" runat="server" style="padding-left: 35%; padding-top: 5%"
                visible="false">
                <%-- <table cellpadding="2" cellspacing="0">--%>
                <table style="line-height: 2; border-spacing: 0; width: 380px;">
                    <tr>
                        <td class="style4" colspan="2">
                            <strong>Change Password</strong>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3" colspan="2">
                            <asp:Label ID="lblChangePwInfor" runat="server" ForeColor="Blue"></asp:Label>
                            <br />
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: small">
                            <span class="style5">User name&nbsp; . . . . . . . . . </span>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtNewUserName" runat="server" Width="192px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: small" class="style5">
                            Current password . . . . .&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtCurrentPassword" runat="server" MaxLength="15" TextMode="Password"
                                Width="192px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: small">
                            <span class="style5">New password . . . . . . . &nbsp;</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNewPassword" runat="server" MaxLength="15" TextMode="Password"
                                Width="192px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: small; color: #000000;">
                            Confirm new password . .
                        </td>
                        <td>
                            <asp:TextBox ID="txtConfirmNewPassword" runat="server" MaxLength="15" TextMode="Password"
                                Width="192px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td class="style1">
                            <asp:Button ID="btnSubmit" runat="server" CausesValidation="false" Text="Submit"
                                Width="64px" OnClick="btnSubmit_Click" />
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Label ID="lblChangePwErrMsg" runat="server" ForeColor="Red" Width="50%"></asp:Label>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
