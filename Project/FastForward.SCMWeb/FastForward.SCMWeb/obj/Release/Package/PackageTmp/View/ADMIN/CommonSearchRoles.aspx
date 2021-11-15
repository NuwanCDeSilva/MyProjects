<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CommonSearchRoles.aspx.cs" Inherits="FastForward.SCMWeb.View.ADMIN.CommonSearchRoles" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>


    <style type="text/css">
        body {
            font-family: Arial;
            font-size: 10pt;
        }

        table {
            border: 1px solid #ccc;
        }

            table th {
                background-color: #F7F7F7;
                color: #333;
                font-weight: bold;
            }

            table th, table td {
                padding: 5px;
                border-color: #ccc;
            }

        .highlight {
            background-Color: Yellow;
        }
        .auto-style1 {
            font-weight: normal;
            color: #000066;
        }
    </style>


</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p>
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" Font-Names="Tahoma" Font-Size="Small">
                    <asp:ListItem Value="ID">ID</asp:ListItem>
                    <asp:ListItem Value="DESCRIPTION">DESCRIPTION</asp:ListItem>
                </asp:DropDownList>
                &nbsp;<asp:TextBox ID="TextBox1" runat="server" Width="300px" BorderStyle="Solid" Font-Names="Tahoma" Font-Size="Small"></asp:TextBox>
                &nbsp;<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Style="height: 26px" Text="Search" BorderStyle="Solid" Font-Names="Tahoma" Font-Size="Small" />
            </p>

            <hr />
            <br />

                <fieldset>

                    <div id="driddv" style="overflow:scroll">
                    <asp:GridView ID="GridView1" AllowPaging="True" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
                        runat="server" AutoGenerateColumns="False" Width="743px" Font-Names="Tahoma" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnPageIndexChanging="GridView1_PageIndexChanging1" BorderStyle="Ridge" BackColor="White" BorderColor="White" BorderWidth="2px" CellPadding="3" CellSpacing="1" Font-Size="Small" GridLines="Horizontal">

                        <Columns>

                            <asp:CommandField ShowSelectButton="True" />

                            <asp:BoundField DataField="ID" HeaderText="Id" SortExpression="ID" />
                            <asp:BoundField DataField="DESCRIPTION" HeaderText="Description" SortExpression="DESCRIPTION" />

                        </Columns>

                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />

                        <HeaderStyle BackColor="#4A3C8C" ForeColor="#E7E7FF" BorderStyle="None" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small"></HeaderStyle>
                        <PagerStyle BackColor="#C6C3C6" BorderStyle="None" Font-Names="Tahoma" Font-Size="Small" ForeColor="Black" HorizontalAlign="Right" />
                        <RowStyle BackColor="#DEDFDE" Font-Names="Tahoma" Font-Size="Small" ForeColor="Black" />
                        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#594B9C" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#33276A" />
                    </asp:GridView>
                    </div>
                    <br />
                    <asp:TextBox ID="txtnames" runat="server" BorderStyle="Solid" Font-Names="Tahoma" Font-Size="Small"></asp:TextBox>
                    <input type="button" value="OK" onclick="SetName();" style="width: 64px" />

                    <script type="text/javascript">

                        function SetName() {
                            if (window.opener != null && !window.opener.closed) {
                                var txtName = window.opener.document.getElementById("txtName");
                                txtName.value = document.getElementById("txtnames").value;
                                window.opener.document.getElementById('BodyContent_Button17').click();
                            }
                            window.close();
                        }
                    </script>

                </fieldset>
            </div>
    </form>
</body>
</html>
