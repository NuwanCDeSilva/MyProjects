<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HTMLPrint.aspx.cs" Inherits="FF.WebERPClient.Test.HTMLPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script language="VBScript">
        sub Print()
            OLECMDID_PRINT = 6
            OLECMDEXECOPT_DONTPROMPTUSER = 2
            OLECMDEXECOPT_PROMPTUSER = 1
            call WB.ExecWB(OLECMDID_PRINT, OLECMDEXECOPT_DONTPROMPTUSER,1)
        End Sub
        document.write "<object id='WB' width='0' height='0' classid='CLSID:8856F961-340A-11D0-A96B-00C04FD705A2'></object>"
    </script>
    <script type="text/javascript">
        function CloseWindow() {
            self.opener = this;
            self.focus();
            self.close();
        }
    </script>
</head>
<body>
<object id="WebBrowser1" width="0" height="0" classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"> </object>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
