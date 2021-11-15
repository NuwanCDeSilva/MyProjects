<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="UserChangePassword.aspx.cs" Inherits="FF.WebERPClient.System_Modules.Security.UserChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        //Function to allow only numbers to textbox
        function validate(e) {
            //getting key code of pressed key
            var keycode = (e.which) ? e.which : e.keyCode;
            //var phn = document.getElementById('txtUserName');
            //comparing pressed keycodes
            if (e.keyCode == 13) {
                alert("Enter Key");
                return false;
            }
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="divChangePassword" runat="server" style="padding-left: 35%; padding-top: 5%">
                <%-- <table cellpadding="2" cellspacing="0">--%>
                <table style="font-size: 11px; color: #000000; font-family: Verdana;">
                    <tr>
                        <td style="color: #33CC33" >
                            <strong>Change Password</strong>
                        </td>
                    </tr>
                    <tr>
                        <td >
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
                        <td >
                            <span>User name&nbsp; . . . . . . . . . </span>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtNewUserName" runat="server" Width="192px" CssClass="TextBox" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Current password . . . . .&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtCurrentPassword" runat="server" MaxLength="15" TextMode="Password"
                                Width="192px" CssClass="TextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span>New password . . . . . . . &nbsp;</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNewPassword" runat="server" MaxLength="15" TextMode="Password"
                                Width="192px" CssClass="TextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Confirm new password . .
                        </td>
                        <td>
                            <asp:TextBox ID="txtConfirmNewPassword" runat="server" MaxLength="15" TextMode="Password"
                                Width="192px" CssClass="TextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td >
                            <asp:Button ID="btnSubmit" runat="server" CausesValidation="false" Text="Submit"
                                Width="64px" OnClick="btnSubmit_Click" CssClass="Button"/>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Label ID="lblChangePwErrMsg" runat="server" ForeColor="Red" Width="50%"></asp:Label>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
