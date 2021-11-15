<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="InvoicePrint2.aspx.cs" Inherits="FF.AbansTours.InvoicePrint2" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Label ID="Label8" runat="server" Text="Invoice No"></asp:Label>
            <asp:TextBox ID="txtInoviceNo" runat="server"></asp:TextBox>
            <%--<asp:DropDownList ID="DropDownList1" runat="server" Height="23px" Width="369px">
    </asp:DropDownList>--%>
            <asp:Button class="btn btn-primary" OnClientClick="Print()" runat="server" ID="SuperBtn" type="submit" Text="Print View using WebPage"
                Width="189px" Height="32px" BackColor="#6699FF" Visible="true" OnClick="SuperBtn_Click" />
            <%--<asp:Button class="btn btn-primary" runat="server" ID="Button1" type="submit" Text="Print View by List"
                Width="203px" Height="32px" BackColor="#6699FF" Visible="true" 
                onclick="Button1_Click" />--%>
              <div id="dvReport">
                    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
</div>
            <input type="button" id="btnPrint" value="Print" onclick="Print()" />
            <div class="container" visible="False">
              
<br />

                <div class="haeading">
                    <br />
                    <asp:Button ID="btnprintD" runat="server" BackColor="Brown" class="btn btn-primary"
                        Height="32px" Text="Print using Datatable" type="submit" Visible="true" Width="189px"
                        OnClick="btnprintD_Click" />
                    <asp:Button ID="btnprintL" runat="server" BackColor="Brown" class="btn btn-primary"
                        Height="32px" Text="Print using List" type="submit" Visible="true" Width="189px"
                        OnClick="btnprintL_Click" />
                    <br />
                    <br />
                    <br />

                    <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                        <Report FileName="Reports_Module/Sales_Rep/InvoiceCrystalReport.rpt">
                        </Report>
                    </CR:CrystalReportSource>
                    <CR:CrystalReportSource ID="CrystalReportSource2" runat="server">
                        <Report FileName="Reports_Module/Sales_Rep/InvoiceCrystalReport2.rpt">
                        </Report>
                    </CR:CrystalReportSource>


                    <asp:Label ID="lblCName" runat="server" Text="*"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="lblCAddress1" runat="server" Text="*"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="lblCAddress2" runat="server" Text="*"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="lblCTelN" runat="server" Text="T:" />
                    <asp:Label ID="lblCTel" runat="server" Text="*"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblCFaxN" runat="server" Text="F:" />
                    <asp:Label ID="lblCFax" runat="server" Text="*"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblCEmailN" runat="server" Text="E:" />
                    <asp:Label ID="lblCEmail" runat="server" Text="*"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblCWebN" runat="server" Text="W:" />
                    <asp:Label ID="lblCWeb" runat="server" Text="*"></asp:Label>
                    <div style="clear: both">
                    </div>
                    <br />
                    <br />

                    <table border="0" >
                        <tr>
                            <td width="70%">
                                <asp:Label ID="Label1" runat="server" Text="To"></asp:Label>
                                <br />
                                <br />
                                <asp:Label ID="lblCuName" runat="server" Style="text-align: left" Text="*"></asp:Label>
                                <br />
                                <br />
                                <asp:Label ID="lblCUAddress1" runat="server" Text="*"></asp:Label>
                                <br />
                                <br />
                                <asp:Label ID="lblCUAddress2" runat="server" Text="*"></asp:Label>
                            </td>
                            <td>
                                <div id="customer">
                                    <table id="meta">
                                        <tr>
                                            <td class="meta-head">
                                                REF NO #
                                            </td>
                                            <td>
                                                <asp:Label ID="lblRefNo" runat="server" Text="*"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="meta-head">
                                                Invoice Date
                                            </td>
                                            <td>
                                                <asp:Label ID="lblInvoiceDate" runat="server" Text="*"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="meta-head">
                                                Invoice No
                                            </td>
                                            <td>
                                                <asp:Label ID="lblInvoiceNo" runat="server" Text="*"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="meta-head">
                                                VAT NO
                                            </td>
                                            <td>
                                                <div class="due">
                                                    <asp:Label ID="lblVatNo" runat="server" Text="*"></asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="TICKET ARRANGEMENTS">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustCode" runat="server" Text='<%# Bind("sii_alt_itm_desc") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="200px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="QTY">
                                <ItemTemplate>
                                    <asp:Label ID="lblQty" runat="server" Text='<%# Bind("sii_qty") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UNIT PRICE">
                                <ItemTemplate>
                                    <asp:Label ID="lblUnit" runat="server" Text='<%# Bind("sii_unit_rt") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="AMOUNT">
                                <ItemTemplate>
                                    <asp:Label ID="Amount" runat="server" Text='<%# Bind("sii_tot_amt") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="80px" />
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle CssClass="RowStyle" />
                    </asp:GridView>
                    <table id="Table1">
                        <tr>
                            <td class="meta-head" width="83%">
                                TOTAL
                            </td>
                            <td width="28%">
                                <div class="due">
                                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <asp:Label ID="Label3" runat="server" Text="Terms &amp; Conditions:"></asp:Label>
                    <br />
                    <asp:Label ID="Label4" runat="server" Text="* Cheques &amp; Bank Draft should be crossed &amp; made payable to Abans Tours(Pvt)Ltd"></asp:Label>
                    <br />
                    <asp:Label ID="Label5" runat="server" Text="* Payments to be settled within 3 day of invoice date"></asp:Label>
                    <br />
                    <asp:Label ID="Label6" runat="server" Text="* Bankers: Bank of Ceylon - Corporate Branch - Branch Code 660 - Account No:0433 - SWIFT Code:BC/EY/LK/LX"></asp:Label>
                    <br />
                    <%--<div id="terms">
                        <h5>
                            Terms</h5>
                    </div>--%>
                    <br />
                    <br />
                </div>
            </div>
            <div class="container">
                <asp:Label ID="Label7" runat="server" Text=""></asp:Label>
            </div>
            </div> </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
