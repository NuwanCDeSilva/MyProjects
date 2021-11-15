<%@ Page Title="" Language="C#" MasterPageFile="~/Basic.Master" AutoEventWireup="true"
    CodeBehind="ReceiptPrint.aspx.cs" Inherits="FF.AbansTours.ReceiptPrint" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Print() {
            var dvReport = document.getElementById("dvReport");
            var frame1 = dvReport.getElementsByTagName("iframe")[0];
            if (navigator.appName.indexOf("Internet Explorer") != -1) {
                frame1.name = frame1.id;
                window.frames[frame1.id].focus();
                window.frames[frame1.id].print();
            }
            else {
                var frameDoc = frame1.contentWindow ? frame1.contentWindow : frame1.contentDocument.document ? frame1.contentDocument.document : frame1.contentDocument;
                frameDoc.print();
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div visible="true">
                <div class="haeading">
                    <br />
                    <asp:Button ID="btnprintL" runat="server" BackColor="Brown" class="btn btn-primary"
                        Height="32px" Text="Print" type="submit" Visible="true" Width="189px" OnClientClick="Print()" />
                    <br />
                    <br />
                    <br />
                    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True" GroupTreeImagesFolderUrl="" Height="50px" ReportSourceID="CrystalReportSource1" ToolbarImagesFolderUrl="" ToolPanelWidth="200px" Width="350px" />
                    
                    <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                        <Report FileName="Reports_Module\Financial_Rep\receiptPrint_Report.rpt">
                        </Report>
                    </CR:CrystalReportSource>
                    
                </div>
                <%--<div>
                    <table style="width: 100%;">
                        <tr>
                            <td class="style4">
                                Customer
                            </td>
                            <td class="style5">
                                <asp:Label ID="lblCD" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style4">
                                &nbsp;
                            </td>
                            <td class="style5">
                                <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblAddress2" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style4">
                                Tel
                            </td>
                            <td class="style5">
                                <asp:Label ID="lblTel" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="style4">
                                Receipt No
                            </td>
                            <td class="style5">
                                <asp:Label ID="lblReceiptNo" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="style4">
                                Amount
                            </td>
                            <td class="style5">
                                <asp:Label ID="lblAmount" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="style4">
                                Receipt Date
                            </td>
                            <td class="style5">
                                <asp:Label ID="lblReceiptdate" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>--%>
            </div>
            </div> </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
