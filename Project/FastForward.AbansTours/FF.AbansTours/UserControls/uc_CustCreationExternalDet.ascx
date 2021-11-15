<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_CustCreationExternalDet.ascx.cs"
    Inherits="FF.AbansTours.UserControls.uc_CustCreationExternalDet" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    .style1
    {
        font-family: Verdana;
    }
</style>
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
<div style="font-family: Verdana; font-size: 11px">
    <asp:Panel ID="Panel_Tabs" runat="server" ForeColor="Blue" GroupingText="">
        <asp:TabContainer ID="TabContainer" runat="server" ActiveTabIndex="0" Font-Size="Smaller"
            Style="color: #000000; font-family: Verdana;">
            <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Home Address">
                <HeaderTemplate>
                    Home Address</HeaderTemplate>
                <ContentTemplate>
                    <asp:Panel ID="Panel_homeAddr" runat="server" GroupingText="Home Address" Style="font-family: Verdana;
                        font-size: small;" ForeColor="Blue">
                        <div style="padding: 2.0px; float: left; width: 96%;"></div>
                        <div style="padding: 1.0px; float: left; width: 98%;">
                            <div style="float: left; width: 18%;">
                                <asp:Label ID="Label13" runat="server" Text="Address" Font-Size="Smaller"></asp:Label></div>
                            <div style="float: left; width: 69%;">
                                <asp:TextBox ID="txtAddresline1" runat="server" Width="100%" CssClass="TextBoxUpper" 
                                    MaxLength="200"></asp:TextBox></div>
                        </div>
                        <div style="padding: 1.0px; float: left; width: 98%;">
                            <div style="float: left; width: 18%;">
                                &#160;&#160;</div>
                            <div style="float: left; width: 69%;">
                                <asp:TextBox ID="txtAddresline2" runat="server" Width="100%" CssClass="TextBoxUpper" 
                                    MaxLength="200"></asp:TextBox></div>
                            <div style="padding: 2.0px; float: left; width: 1%;">
                            </div>
                        </div>
                        <div>
                            <div style="float: left; width:90%; padding: 2.0px">
                            </div>
                        </div>
                        <div>
                            <div style="float: left; width: 45%;">
                                <div>
                                    <div style="float: left; width: 40%;">
                                        <asp:Label ID="Label14" runat="server" Text="Town" Font-Size="X-Small"></asp:Label></div>
                                    <div style="float: left; width: 49%;">
                                        <asp:TextBox ID="txtTown" runat="server" CssClass="TextBoxUpper" Width="75%" 
                                            MaxLength="15"></asp:TextBox>
                                        <asp:ImageButton ID="imgBtnTownSearch" runat="server" 
                                            ImageUrl="~/Images/icon_search.png" onclick="imgBtnTownSearch_Click" />
                                    </div>
                                </div>
                                <div>
                                    <div style="float: left; width: 40%;">
                                        <asp:Label ID="Label15" runat="server" Text="Postal Code" Font-Size="X-Small"></asp:Label></div>
                                    <div style="float: left; width: 49%;">
                                        <asp:TextBox ID="txtPostalCode" runat="server" CssClass="TextBoxUpper" Width="75%" 
                                            MaxLength="10"></asp:TextBox></div>
                                </div>
                                <div>
                                    <div style="float: left; width: 40%;">
                                        <asp:Label ID="Label1" runat="server" Text="Country Code" Font-Size="X-Small"></asp:Label></div>
                                    <div style="float: left; width: 49%;">
                                        <asp:TextBox ID="txtCountryCD" runat="server" CssClass="TextBoxUpper" Width="75%" 
                                            MaxLength="15"></asp:TextBox></div>
                                </div>
                            </div>
                            <div style="float: left; width: 45%;">
                                <div>
                                    <div style="float: left; width: 40%;">
                                        <asp:Label ID="Label16" runat="server" Text="District" Font-Size="X-Small"></asp:Label></div>
                                    <div style="float: left; width: 49%;">
                                        <asp:TextBox ID="txtDistrict" runat="server" CssClass="TextBoxUpper" Width="75%" 
                                            MaxLength="15"></asp:TextBox></div>
                                </div>
                                <div>
                                    <div style="float: left; width: 40%;">
                                        <asp:Label ID="Label17" runat="server" Text="Province" Font-Size="Small" 
                                            style="font-size: x-small; font-family: Verdana"></asp:Label></div>
                                    <div style="float: left; width: 49%;">
                                        <asp:TextBox ID="txtProvince" runat="server" CssClass="TextBoxUpper" Width="75%" 
                                            MaxLength="15"></asp:TextBox></div>
                                </div>
                                <div>
                                    <div style="float: left; width: 40%;">
                                        <asp:Label ID="Label4" runat="server" Text="Phone" Font-Size="X-Small"></asp:Label></div>
                                    <div style="float: left; width: 49%;">
                                        <asp:TextBox ID="txtPhone" runat="server" CssClass="TextBox" Width="75%" 
                                            MaxLength="10"></asp:TextBox></div>
                                </div>
                            </div>
                        </div>
                        <div style="padding: 1.0px; float: left; width: 96%;">
                        </div>
                        <div style="float: left; width: 100%;">
                            <div style="float: left; width: 18%;">
                                <asp:Label ID="Label18" runat="server" Font-Size="X-Small" Text="E-mail"></asp:Label>
                            </div>
                            <div style="float: left; width: 69%;">
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="TextBox" MaxLength="50" 
                                    Width="100%"></asp:TextBox>
                            </div>
                        </div>
                        <div style="padding: 1.0px; float: left; width: 96%;"></div>
                    </asp:Panel>
                    <asp:Panel ID="Panel_CurrentAddress" runat="server" GroupingText="Current Address"
                        Style="font-family: Verdana" ForeColor="Blue">
                         <div style="padding: 2.0px; float: left; width: 96%;"></div>
                        <div>
                            <div style="float: left; width: 18%;">
                                <asp:Label ID="Label19" runat="server" Text="Address" Font-Size="X-Small"></asp:Label></div>
                            <div style="float: left; width: 69%;">
                                <asp:TextBox ID="txtCurrentAddrline1" runat="server" Width="100%" 
                                    CssClass="TextBoxUpper" MaxLength="200"></asp:TextBox></div>
                        </div>
                        <div>
                            <div style="float: left; width: 18%;">
                                &#160;&#160;</div>
                            <div style="float: left; width: 69%;">
                                <asp:TextBox ID="txtCurrentAddrline2" runat="server" Width="100%" 
                                    CssClass="TextBoxUpper" MaxLength="200"></asp:TextBox></div>
                        </div>
                        <div>
                             <div style="padding: 2.0px; float: left; width: 96%;"></div>
                        </div>
                        <div>
                            <div style="float: left; width: 50%;">
                                <div>
                                    <div style="float: left; width: 35%;">
                                        <asp:Label ID="Label20" runat="server" Text="Town" Font-Size="X-Small"></asp:Label></div>
                                    <div style="float: left; width: 49%;">
                                        <asp:TextBox ID="txtCurTown" runat="server" CssClass="TextBoxUpper" Width="75%" 
                                            MaxLength="15"></asp:TextBox>
                                        <asp:ImageButton ID="imgBtnSearchCurTown" runat="server" 
                                            ImageUrl="~/Images/icon_search.png" onclick="imgBtnSearchCurTown_Click" />
                                    </div>
                                </div>
                                <div>
                                    <div style="float: left; width: 35%;">
                                        <asp:Label ID="Label21" runat="server" Text="Postal Code" Font-Size="X-Small"></asp:Label></div>
                                    <div style="float: left; width: 49%;">
                                        <asp:TextBox ID="txtCurPostalCode" runat="server" CssClass="TextBoxUpper" 
                                            Width="75%" MaxLength="10"></asp:TextBox></div>
                                </div>
                                <div>
                                    <div style="float: left; width: 35%;">
                                        <asp:Label ID="Label2" runat="server" Text="Country Code" Font-Size="X-Small"></asp:Label></div>
                                    <div style="float: left; width: 49%;">
                                        <asp:TextBox ID="txtCurContryCD" runat="server" CssClass="TextBoxUpper" Width="75%" 
                                            MaxLength="15"></asp:TextBox></div>
                                </div>
                            </div>
                            <div style="float: left; width: 49%;">
                                <div>
                                    <div style="float: left; width: 35%;">
                                        <asp:Label ID="Label22" runat="server" Text="District" Font-Size="X-Small"></asp:Label></div>
                                    <div style="float: left; width: 49%;">
                                        <asp:TextBox ID="txtCurDistrict" runat="server" CssClass="TextBoxUpper" Width="75%" 
                                            MaxLength="15"></asp:TextBox></div>
                                </div>
                                <div>
                                    <div style="float: left; width: 35%;">
                                        <asp:Label ID="Label23" runat="server" Text="Province" Font-Size="X-Small"></asp:Label></div>
                                    <div style="float: left; width: 49%;">
                                        <asp:TextBox ID="txtCurProvince" runat="server" CssClass="TextBoxUpper" Width="75%" 
                                            MaxLength="15"></asp:TextBox></div>
                                </div>
                                <div>
                                    <div style="float: left; width: 35%;">
                                        <asp:Label ID="Label5" runat="server" Text="Phone" Font-Size="X-Small"></asp:Label></div>
                                    <div style="float: left; width: 49%;">
                                        <asp:TextBox ID="txrCurPhone" runat="server" CssClass="TextBox" Width="75%" 
                                            MaxLength="10"></asp:TextBox></div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="Tab_workPlace" runat="server" HeaderText="Work Place">
                <ContentTemplate>
                    <asp:Panel ID="Panel_workplace" runat="server" GroupingText="Work Place Details">
                        <div>
                            <div style="float: left; width: 25%;">
                                <asp:Label ID="Label24" runat="server" Text="Company Name" CssClass="style1" 
                                    Font-Size="X-Small"></asp:Label></div>
                            <div style="float: left; width: 90%;">
                                <asp:TextBox ID="txtWorkComName" runat="server" CssClass="TextBoxUpper" Width="100%" 
                                    MaxLength="200"></asp:TextBox></div>
                            
                        </div>
                        <div>
                            <div>
                                <div style="float: left; width: 50%;">
                                    <asp:Label ID="Label25" runat="server" Text="Address" CssClass="style1" 
                                        Font-Size="X-Small"></asp:Label></div>
                                <div style="float: left; width: 100%;">
                                    <asp:TextBox ID="txtWrkAddrLine1" runat="server" Width="90%" CssClass="TextBoxUpper" 
                                        MaxLength="200"></asp:TextBox></div>
                            </div>
                        </div>
                        <div style="padding: 1.0px; float: left; width: 96%;"></div>
                        <div>
                            <div style="float: left; width: 100%;">
                                <asp:TextBox ID="txtWrkAddrLine2" runat="server" Width="90%" CssClass="TextBoxUpper" 
                                    MaxLength="200"></asp:TextBox></div>
                        </div>
                        <div style="padding: 2.0px; float: left; width: 96%;"></div>
                        <div style="float: left; width: 100%;">
                            <div style="float: left; width: 50%;">
                                <div>
                                    <div style="float: left; width: 30%;">
                                        <asp:Label ID="Label26" runat="server" Text="Department" CssClass="style1" 
                                            Font-Size="X-Small"></asp:Label></div>
                                    <div style="float: left; width: 49%;">
                                        <asp:TextBox ID="txtWrkDept" runat="server" CssClass="TextBoxUpper" 
                                            MaxLength="15"></asp:TextBox></div>
                                </div>
                                <div>
                                    <div style="float: left; width: 30%;">
                                        <asp:Label ID="Label27" runat="server" Text="Profession" CssClass="style1" 
                                            Font-Size="X-Small"></asp:Label></div>
                                    <div style="float: left; width: 49%;">
                                        <asp:TextBox ID="txtWrkProfession" runat="server" CssClass="TextBoxUpper" 
                                            MaxLength="50"></asp:TextBox></div>
                                </div>
                                <div>
                                    <div style="float: left; width: 30%;">
                                        <asp:Label ID="Label28" runat="server" Text="Designation" CssClass="style1" 
                                            Font-Size="X-Small"></asp:Label></div>
                                    <div style="float: left; width: 49%;">
                                        <asp:TextBox ID="txtWrkDesignation" runat="server" CssClass="TextBoxUpper" 
                                            MaxLength="50"></asp:TextBox></div>
                                </div>
                            </div>
                            <div style="float: left; width: 49%;">
                                <div>
                                    <div style="float: left; width: 20%;">
                                        <asp:Label ID="Label29" runat="server" Text="Phone" CssClass="style1" 
                                            Font-Size="X-Small"></asp:Label></div>
                                    <div style="float: left; width: 70%;">
                                        <asp:TextBox ID="txtWrkPhone" runat="server" CssClass="TextBox" MaxLength="10"></asp:TextBox></div>
                                </div>
                                <div>
                                    <div style="float: left; width: 20%;">
                                        <asp:Label ID="Label30" runat="server" Text="Fax" CssClass="style1" 
                                            Font-Size="X-Small"></asp:Label></div>
                                    <div style="float: left; width: 70%;">
                                        <asp:TextBox ID="txtWrkFax" runat="server" CssClass="TextBox" MaxLength="10"></asp:TextBox></div>
                                </div>
                            </div>
                        </div>
                        <div>
                            <div>
                            <div  style="padding: 1.0px; float: left; width: 96%;"></div>
                                <div style="float: left; width: 30%;">
                                    <asp:Label ID="Label3" runat="server" Text="E-mail" CssClass="style1" 
                                        Font-Size="X-Small"></asp:Label></div>
                                <div style="float: left; width: 100%;">
                                    <asp:TextBox ID="txtWrEmail" runat="server" Width="90%" CssClass="TextBox" 
                                        MaxLength="50"></asp:TextBox></div>
                            </div>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="Tab_taxDet" runat="server" HeaderText="Tax Details">
                <ContentTemplate>
                    <asp:Panel ID="Panel_taxDet" runat="server" GroupingText="Tax Details">
                        <div style="float: left; width: 100%;">
                            <div style="float: left; width: 60%;">
                            <div style="padding: 2.0px; float: left; width: 96%;"></div>
                                <div>
                                    <div style="float: left; width: 45%;">
                                        <asp:CheckBox ID="chkVATextempted" runat="server" Text="VAT Extempted" 
                                            TextAlign="Left" CssClass="style1" Font-Size="X-Small" /></div>
                                    <div style="float: left; width: 45%;">
                                        <asp:CheckBox ID="chkVATcustomer" runat="server" Text="VAT Customer" 
                                            TextAlign="Left" CssClass="style1" Font-Size="X-Small" /></div>
                                </div>
                                <div style="padding: 2.0px; float: left; width: 98%;"></div>
                                <div>
                                    <div style="padding: 2.0px; float: left; width: 40%;">
                                        <asp:Label ID="Label34" runat="server" Text="VAT Registration No." 
                                            CssClass="style1" Font-Size="X-Small"></asp:Label></div>
                                    <div style="float: left; width: 55%;">
                                        <asp:TextBox ID="txtVatRegNo" runat="server" CssClass="TextBoxUpper" 
                                            MaxLength="60"></asp:TextBox></div>
                                </div>
                                <div style="padding: 4.0px; float: left; width: 95%;">
                                    <hr/>
                                
                                </div>
                                <div>
                                    <div style="padding: 2.0px; float: left; width: 98%;">
                                        <asp:CheckBox ID="chkSVATcustomer" runat="server" Text="SVAT Customer" 
                                            TextAlign="Left" CssClass="style1" Font-Size="X-Small" /></div>
                                </div>
                                <div style="padding: 2.0px; float: left; width: 96%;">
                                </div>
                                <div>
                                    <div style="padding: 2.0px; float: left; width: 40%;">
                                        <asp:Label ID="Label35" runat="server" Text="SAVT Registration No" 
                                            CssClass="style1" Font-Size="X-Small"></asp:Label></div>
                                    <div style="float: left; width: 50%;">
                                        <asp:TextBox ID="txtSVATno" runat="server" CssClass="TextBoxUpper" 
                                            MaxLength="60"></asp:TextBox></div>
                                </div>
                            </div>
                            <div style="float: left; width: 38%;">
                            </div>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
    </asp:Panel>
</div>
<div style="visibility: hidden">
    <%--style="visibility: hidden"--%>
    <asp:Button ID="btnEmail" runat="server" OnClick="btnEmail_Click" />
    <asp:Button ID="btnEmailWr" runat="server" onclick="btnEmailWr_Click"/>
    <asp:Button ID="btnPhn" runat="server" onclick="btnPhn_Click"  />
    <asp:Button ID="btnPhnCur" runat="server" onclick="btnPhnCur_Click"  />
    <asp:Button ID="btnPhnWr" runat="server" onclick="btnPhnWr_Click" />
    <asp:Button ID="btnTown" runat="server" onclick="btnTown_Click"/>
    <asp:Button ID="btnCurTown" runat="server" onclick="btnCurTown_Click"/>
    <%--<asp:Button ID="btnCurPhn" runat="server" OnClick="ValidateCurPhoneNum" />
    <asp:Button ID="btnWrPhn" runat="server" OnClick="ValidateWrPhoneNum" />--%>
</div>
