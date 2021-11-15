<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Test.aspx.cs" Inherits="WebApplication1.Test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <form id="form1" runat="server">
    <table style="width: 100%; line-height: normal;">
        <tr>
            <td style="border-style: none; background-color: #CCCCCC;" align="left" colspan="5">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="border-style: none; background-color: #F4FAFF;" align="left" colspan="5">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="border-style: none; background-color: #F4FAFF;" align="left" colspan="5">
                <h2 style="font-family: 'times New Roman'; color: #000000;">
                    &nbsp;&nbsp; END CONTRACT</h2>
            </td>
        </tr>
        <tr>
            <td style="border-style: none; background-color: #F4FAFF;" align="left">
                <label style="color: #000000">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    Trainee Name&nbsp;&nbsp;
                </label>
                <label>
                    <asp:DropDownList ID="termiDropdown" runat="server" AutoPostBack="true" class="form-control"
                        Width="205px" OnDataBound="termiDropdown_DataBound" OnSelectedIndexChanged="termiDropdown_SelectedIndexChanged" />
                </label>
            </td>
            <td style="border-style: none; background-color: #F4FAFF;" align="left" colspan="4">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="border-style: none; background-color: #F4FAFF;" align="left" colspan="5">
                <label style="color: #000000">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    Start Date&nbsp;&nbsp;&nbsp;</label><label><asp:TextBox ID="termStartDte" runat="server"
                        Height="34px" type="date" Width="205px" f></asp:TextBox>
                    </label>
            </td>
        </tr>
        <tr>
            <td style="border-style: none; background-color: #F4FAFF;" align="left" colspan="5">
                <label style="color: #000000">
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    End Date</label>&nbsp;&nbsp;&nbsp;<label><asp:TextBox ID="termEnd" runat="server"
                        Height="34px" type="date" Width="205px"></asp:TextBox>
                    </label>
            </td>
        </tr>
        <tr>
            <td style="border-style: none; background-color: #F4FAFF;" align="left" colspan="5">
                <label style="color: #000000">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    Status&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </label>
                <label style="color: #000000">
                    <asp:TextBox ID="sttusTextBox" runat="server" Height="34px" Width="205px"></asp:TextBox>
                </label>
            </td>
        </tr>
        <tr>
            <td style="border-style: none; background-color: #F4FAFF;" align="left" colspan="5">
                <label style="color: #000000">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    Deactive&nbsp;</label>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" />
            </td>
        </tr>
        <tr>
            <td style="border-style: none; background-color: #F4FAFF;" align="left" colspan="5">
                &nbsp;
                <asp:Button class="btn btn-primary" runat="server" ID="SuperBtn" type="submit" Text="To HR "
                    Width="100px" Height="32px" BackColor="#6699FF" Visible="False" />
                &nbsp;
                <asp:Button class="btn btn-primary" runat="server" ID="hrBtn" type="submit" Text="To Supervisor"
                    Width="112px" Height="32px" BackColor="#6699FF" Visible="False" />
            </td>
        </tr>
        <tr>
            <td style="border-style: none; background-color: #F4FAFF;" align="left" colspan="5">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;
                <asp:Button class="btn btn-primary" runat="server" ID="termBtn" type="submit" Text="Terminate"
                    Width="100px" Height="32px" BackColor="#6699FF" OnClick="termBtn_Click" />
            </td>
        </tr>
        <tr>
            <td style="border-style: none; background-color: #F4FAFF;" align="left" colspan="5">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td style="border-style: none; background-color: #F4FAFF;" align="left" colspan="5">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td style="border-style: none; background-color: #F4FAFF;" align="right" colspan="2">
                &nbsp;
            </td>
            <td style="border-style: none; background-color: #F4FAFF;" align="right">
                &nbsp;
            </td>
            <td style="border-style: none; background-color: #F4FAFF;" align="left">
                &nbsp;
            </td>
            <td style="border-style: none; background-color: #F4FAFF;" align="left">
            </td>
        </tr>
        <tr>
            <td style="border-style: none; background-color: #F4FAFF;" align="right" colspan="2">
                &nbsp;
            </td>
            <td style="border-style: none; background-color: #F4FAFF;" align="left">
                &nbsp;<td style="border-style: none; background-color: #F4FAFF;" align="right">
                    &nbsp;&nbsp;<br />
                    <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label>
                </td>
            <td style="border-style: none; background-color: #F4FAFF;" align="left">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="border-style: none; background-color: #F4FAFF;" align="right" colspan="2">
                &nbsp;
            </td>
            <td style="border-style: none; background-color: #F4FAFF;" align="left">
                &nbsp;
            </td>
            <td style="border-style: none; background-color: #F4FAFF;" align="right">
                &nbsp;<br />
            </td>
            <td style="border-style: none; background-color: #F4FAFF;" align="left">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="border-style: none; background-color: #F4FAFF;" align="right" colspan="2">
                &nbsp;
            </td>
            <td style="border-style: none; background-color: #F4FAFF;" align="left">
                &nbsp;
            </td>
            <td style="border-style: none; background-color: #F4FAFF;" align="right">
                &nbsp;
            </td>
            <td style="border-style: none; background-color: #F4FAFF;" align="left">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="border-style: none; background-color: #F4FAFF;" align="right" colspan="2">
                &nbsp;
            </td>
            <td style="border-style: none; background-color: #F4FAFF;" align="left">
                &nbsp;
            </td>
            <td style="border-style: none; background-color: #F4FAFF;" align="left">
                &nbsp;&nbsp;
            </td>
            <td style="border-style: none; background-color: #F4FAFF;" align="left">
                &nbsp;
            </td>
        </tr>
    </table>
    </form>
</asp:Content>
