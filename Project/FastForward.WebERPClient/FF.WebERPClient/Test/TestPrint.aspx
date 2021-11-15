<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPrint.aspx.cs" Inherits="FF.WebERPClient.Test.TestPrint" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <SCRIPT type="text/javascript" LANGUAGE="JavaScript">
        function executeCommands(inputparms) {
            // Instantiate the Shell object and invoke 
            // its execute method.

            //var oShell = new ActiveXObject("Shell.Application"); WScript.Shell
            var oShell = new ActiveXObject("WScript.Shell");


            //sRegVal = "HKCUSoftwareMicrosoftWindows "
            //sRegVal = sRegVal & "NTCurrentVersionWindowsDevice"
            sRegVal = 'HKEY_CURRENT_USER\\Software\\Microsoft\\Windows NT\\CurrentVersion\\Windows\\Device'
            sDefault = ""

            sDefault = oShell.RegRead(sRegVal)
            //sDefault = Left(sDefault, InStr(sDefault, ",") - 1)

            GetDefaultPrinter = sDefault

            //var commandtoRun = "notepad.exe";
            //if (inputparms != "")
            // {
            // var commandParms = document.Form1.filename.value;
            // }

            // Invoke the execute method. 
            //oShell.Run(commandtoRun, commandParms, "", "open", "1");
            //oShell.Run(commandtoRun);

            //document.write(GetDefaultPrinter);
            document.getElementById('HiddenField1').value=GetDefaultPrinter;
        }
</SCRIPT>
</head>
<body onload="executeCommands()">
    <form id="form1" runat="server">
    <asp:HiddenField ID="HiddenField1" Value="" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="upPrint"> <ContentTemplate>
    <asp:TextBox ID="TextBoxInv" runat="server"></asp:TextBox>
    <asp:Button runat="server" ID="btnOk" Text="Test" onclick="btnOk_Click" />
     <asp:Button runat="server" ID="Button1" Text="Test1" onclick="Button1_Click" 
              />
      <asp:Button runat="server" ID="Button2" Text="Test1" onclick="Button2_Click"  />
        <asp:Button ID="Button3" runat="server" onclick="Button3_Click" 
            Text="Get Printer" />
        <asp:Button ID="Button4" runat="server" onclick="Button4_Click" Text="Test2" />
     <br />
     <asp:Label ID="Label1" runat="server"></asp:Label>
     <asp:Label ID="Label2" runat="server"></asp:Label>
    </ContentTemplate></asp:UpdatePanel>
    </form>
</body>
</html>

