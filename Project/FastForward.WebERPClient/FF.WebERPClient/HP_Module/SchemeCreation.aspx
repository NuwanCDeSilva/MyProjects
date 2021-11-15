<%@ Page Title="Scheme Creation" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="SchemeCreation.aspx.cs" Inherits="FF.WebERPClient.HP_Module.SchemeCreation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
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

        function isNumberKeyAndDot(event, value) {
            var charCode = (event.which) ? event.which : event.keyCode
            var intcount = 0;
            var stramount = value;
            for (var i = 0; i < stramount.length; i++) {
                if (stramount.charAt(i) == '.' && charCode == 46) {
                    return false;
                }
            }
            if ((charCode > 31 && (charCode < 48 || charCode > 57)) && charCode != 46)
                return false;
            return true;
        }


        function uppercase() {
            key = window.event.keyCode;
            if ((key > 0x60) && (key < 0x7B))
                window.event.keyCode = key - 0x20;
        }

    </script>
    <%--javascript--%>
    <link href="../MainStyleSheet.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updtMainPnl" runat="server">
        <ContentTemplate>
            <asp:TabContainer ID="tcGRNContainer" runat="server" ActiveTabIndex="0" Height="530px">
                <%--MRN Entering Tab--%>
                <asp:TabPanel ID="tbpGRNDataEntry" HeaderText="Scheme Creation" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="PanelGranAll" runat="server" ScrollBars="Both" Height="528px">
                            <div style="float: left; width: 90%; color: Black;" class="MainDivCss">
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 15%;">
                                        Category</div>
                                    <div style="float: left; width: 1%;">
                                        :</div>
                                    <div style="float: left; width: 35%; text-align: left;">
                                        <asp:DropDownList ID="ddlCate" runat="server" CssClass="ComboBox" Width="80%" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlCate_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 1%;">
                                        &nbsp;</div>
                                    <div style="float: left; width: 15%;">
                                        Type</div>
                                    <div style="float: left; width: 1%;">
                                        :</div>
                                    <div style="float: left; width: 35%; text-align: left;">
                                        <asp:DropDownList ID="ddlType" runat="server" CssClass="ComboBox" Width="80%" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:ImageButton ID="imgNew" runat="server" ImageUrl="~/Images/EditIcon.png" OnClick="imgNewType_Click" />
                                    </div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 50%; padding-top: 1px; padding-bottom: 1px;">
                                        <div style="float: left; width: 99%; height: 19px; color: #FFFFFF; background-color: #507CD1;">
                                            <asp:Label ID="Label2" runat="server" Text="New scheme Type" Width="189px" Height="16px"></asp:Label>
                                        </div>
                                        <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                            <div style="float: left; width: 1%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 34%;">
                                                Type</div>
                                            <div style="float: left; width: 1%;">
                                                :</div>
                                            <div style="float: left; width: 60%; text-align: left;">
                                                <asp:TextBox ID="txtType" runat="server" Width="60%" Style="margin-left: 2%" CssClass="TextBox"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                            <div style="float: left; width: 1%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 34%;">
                                                Description</div>
                                            <div style="float: left; width: 1%;">
                                                :</div>
                                            <div style="float: left; width: 60%; text-align: left;">
                                                <asp:TextBox ID="txtDesc" runat="server" Width="95%" Style="margin-left: 2%" CssClass="TextBox"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                            <div style="float: left; width: 1%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 34%;">
                                                Annual intrest rate</div>
                                            <div style="float: left; width: 1%;">
                                                :</div>
                                            <div style="float: left; width: 60%; text-align: left;">
                                                <asp:TextBox ID="txtAnnualRate" runat="server" Width="60%" Style="margin-left: 2%;
                                                    text-align: right" CssClass="TextBox" onkeypress="return isNumberKeyAndDot(event,value)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                            <div style="float: left; width: 1%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 34%;">
                                                Active</div>
                                            <div style="float: left; width: 1%;">
                                                :</div>
                                            <div style="float: left; width: 60%; text-align: left;">
                                                <asp:CheckBox ID="chkActive" runat="server" /><asp:Button ID="btnSchSave" runat="server"
                                                    Text="Save" CssClass="Button" />
                                            </div>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 50%; padding-top: 1px; padding-bottom: 1px;">
                                        <div style="float: left; width: 99%; height: 19px; color: #FFFFFF; background-color: #507CD1;">
                                            <asp:Label ID="l2" runat="server" Text="Scheme Details" Width="189px" Height="16px"></asp:Label>
                                        </div>
                                        <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                            <div style="float: left; width: 50%; padding-top: 1px; padding-bottom: 1px;">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 40%;">
                                                    Term</div>
                                                <div style="float: left; width: 2%;">
                                                    :</div>
                                                <div style="float: left; width: 50%; text-align: left;">
                                                    <asp:TextBox ID="txtTerm" runat="server" Width="100%" Style="margin-left: 2%; text-align: right"
                                                        CssClass="TextBox"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 50%; padding-top: 1px; padding-bottom: 1px;">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 40%;">
                                                    Scheme Code</div>
                                                <div style="float: left; width: 2%;">
                                                    :</div>
                                                <div style="float: left; width: 50%; text-align: left;">
                                                    <asp:TextBox ID="txtSchCode" runat="server" Width="100%" Style="margin-left: 2%"
                                                        CssClass="TextBox" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                            <div style="float: left; width: 1%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 19.5%;">
                                                Description</div>
                                            <div style="float: left; width: 0.10%;">
                                                :</div>
                                            <div style="float: left; width: 75%; text-align: left;">
                                                <asp:TextBox ID="txtSchDesc" runat="server" Width="100%" Style="margin-left: 2%"
                                                    CssClass="TextBox" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                            <div style="float: left; width: 50%; padding-top: 1px; padding-bottom: 1px;">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 40%;">
                                                    Interest Rate</div>
                                                <div style="float: left; width: 2%;">
                                                    :</div>
                                                <div style="float: left; width: 50%; text-align: left;">
                                                    <asp:TextBox ID="txtIntRt" runat="server" Width="100%" Style="margin-left: 2%; text-align: right"
                                                        CssClass="TextBox"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 50%; padding-top: 1px; padding-bottom: 1px;">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 40%;">
                                                    Add. rental</div>
                                                <div style="float: left; width: 2%;">
                                                    :</div>
                                                <div style="float: left; width: 50%; text-align: left;">
                                                    <asp:TextBox ID="txtAddrent" runat="server" Width="100%" Style="margin-left: 2%;
                                                        text-align: right" CssClass="TextBox"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                            <div style="float: left; width: 50%; padding-top: 1px; padding-bottom: 1px;">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 40%;">
                                                    Allow Insuarance</div>
                                                <div style="float: left; width: 2%;">
                                                    :</div>
                                                <div style="float: left; width: 50%; text-align: left;">
                                                    <asp:CheckBox ID="chkInsu" runat="server" />
                                                </div>
                                            </div>
                                            <div style="float: left; width: 50%; padding-top: 1px; padding-bottom: 1px;">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 40%;">
                                                    Allow group sales</div>
                                                <div style="float: left; width: 2%;">
                                                    :</div>
                                                <div style="float: left; width: 50%; text-align: left;">
                                                    <asp:CheckBox ID="chkGroup" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                    <div style="float: left; width: 99%; height: 19px; color: #FFFFFF; background-color: #507CD1;">
                                        <asp:Label ID="l3" runat="server" Text="Calculations parameters" Width="189px" Height="16px"></asp:Label>
                                    </div>
                                    <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                        <div style="float: left; width: 50%; padding-top: 1px; padding-bottom: 1px;">
                                            <div style="float: left; width: 1%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 100%; font-weight: bold;">
                                                First payment</div>
                                        </div>
                                        <div style="float: left; width: 50%; padding-top: 1px; padding-bottom: 1px;">
                                            <div style="float: left; width: 1%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 100%; font-weight: bold;">
                                                Additional rental</div>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                        <div style="float: left; width: 50%; padding-top: 1px; padding-bottom: 1px;">
                                            <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 40%;">
                                                    Pay Type</div>
                                                <div style="float: left; width: 3%;">
                                                    :</div>
                                                <div style="float: left; width: 40%; text-align: left;">
                                                    <asp:DropDownList ID="ddlFpayTP" runat="server" Width="60%" CssClass="ComboBox">
                                                        <asp:ListItem></asp:ListItem>
                                                        <asp:ListItem>RATE</asp:ListItem>
                                                        <asp:ListItem>VALUE</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 40%;">
                                                    Rate / Amount</div>
                                                <div style="float: left; width: 2%;">
                                                    :</div>
                                                <div style="float: left; width: 40%; text-align: left;">
                                                    <asp:TextBox ID="txtFPrate" runat="server" Width="58%" Style="margin-left: 2%; text-align: right"
                                                        CssClass="TextBox"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 60%;">
                                                    Calculate on VAT inclusive price</div>
                                                <div style="float: left; width: 3%;">
                                                    :</div>
                                                <div style="float: left; width: 5%; text-align: left;">
                                                    <asp:CheckBox ID="chkCalwithVAT" runat="server" />
                                                </div>
                                            </div>
                                            <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 60%;">
                                                    Payment inclusive of VAT</div>
                                                <div style="float: left; width: 3%;">
                                                    :</div>
                                                <div style="float: left; width: 5%; text-align: left;">
                                                    <asp:CheckBox ID="chkpaywithVAT" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 50%; padding-top: 1px; padding-bottom: 1px;">
                                            <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 25%;">
                                                    Inclusive of VAT</div>
                                                <div style="float: left; width: 3%;">
                                                    :</div>
                                                <div style="float: left; width: 5%; text-align: left;">
                                                    <asp:CheckBox ID="chkAddwithVAT" runat="server" />
                                                </div>
                                            </div>
                                            <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 25%;">
                                                    Rate</div>
                                                <div style="float: left; width: 2%;">
                                                    :</div>
                                                <div style="float: left; width: 40%; text-align: left;">
                                                    <asp:TextBox ID="txtAddRnt" runat="server" Width="60%" Style="margin-left: 2%; text-align: right"
                                                        CssClass="TextBox"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                        <div style="float: left; width: 50%; padding-top: 1px; padding-bottom: 1px;">
                                            <div style="float: left; width: 1%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 100%; font-weight: bold;">
                                                Inital payment composition</div>
                                        </div>
                                        <div style="float: left; width: 50%; padding-top: 1px; padding-bottom: 1px;">
                                            <div style="float: left; width: 1%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 100%; font-weight: bold;">
                                                Special discount</div>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                        <div style="float: left; width: 50%; padding-top: 1px; padding-bottom: 1px;">
                                            <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 60%;">
                                                    Service charges</div>
                                                <div style="float: left; width: 3%;">
                                                    :</div>
                                                <div style="float: left; width: 5%; text-align: left;">
                                                    <asp:CheckBox ID="chkSerchg" runat="server" />
                                                </div>
                                            </div>
                                            <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 60%;">
                                                    Insuarance</div>
                                                <div style="float: left; width: 3%;">
                                                    :</div>
                                                <div style="float: left; width: 5%; text-align: left;">
                                                    <asp:CheckBox ID="chkInitInsu" runat="server" />
                                                </div>
                                            </div>
                                            <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 60%;">
                                                    Stamp duty</div>
                                                <div style="float: left; width: 3%;">
                                                    :</div>
                                                <div style="float: left; width: 5%; text-align: left;">
                                                    <asp:CheckBox ID="chkStmDuty" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 50%; padding-top: 1px; padding-bottom: 1px;">
                                            <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 25%;">
                                                    Type</div>
                                                <div style="float: left; width: 3%;">
                                                    :</div>
                                                <div style="float: left; width: 40%; text-align: left;">
                                                    <asp:DropDownList ID="ddlDisType" runat="server" Width="60%" CssClass="ComboBox">
                                                        <asp:ListItem></asp:ListItem>
                                                        <asp:ListItem>RATE</asp:ListItem>
                                                        <asp:ListItem>VALUE</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                                <div style="float: left; width: 1%;">
                                                    &nbsp;</div>
                                                <div style="float: left; width: 25%;">
                                                    Rate / Amount</div>
                                                <div style="float: left; width: 2%;">
                                                    :</div>
                                                <div style="float: left; width: 40%; text-align: left;">
                                                    <asp:TextBox ID="txtDisRate" runat="server" Width="60%" Style="margin-left: 2%; text-align: right"
                                                        CssClass="TextBox"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div style="display: none;">
                                    <asp:Button ID="btnSchCate" runat="server" OnClick="LoadSchTP" />
                                    <asp:Button ID="btnSchTP" runat="server" OnClick="LoadSchTPDetails" />
                                    <asp:Button ID="btnTerm" runat="server" OnClick="CheckExsistTerm" />
                                </div>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel2" HeaderText="Scheme Shedule" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="Panel1" runat="server">
                            <asp:UpdatePanel ID="SheduleDet" runat="server">
                                <ContentTemplate>
                                    <div style="float: left; width: 90%; color: Black;" class="MainDivCss">
                                        <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                            <div style="float: left; width: 1%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 20%;">
                                                System define</div>
                                            <div style="float: left; width: 3%;">
                                                :</div>
                                            <div style="float: left; width: 5%; text-align: left;">
                                                <asp:CheckBox ID="chkSysDef" runat="server" />
                                            </div>
                                        </div>
                                        <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                            <div style="float: left; width: 1%;">
                                                &nbsp;</div>
                                            <div style="float: left; width: 20%;">
                                                User define</div>
                                            <div style="float: left; width: 3%;">
                                                :</div>
                                            <div style="float: left; width: 5%; text-align: left;">
                                                <asp:CheckBox ID="chkUsrDef" runat="server" OnCheckedChanged="chkUsrDef_CheckedChanged"
                                                    AutoPostBack="true" />
                                            </div>
                                        </div>
                                        <div id="divShedule" runat="server" style="float: left; width: 100%; padding-top: 1px;
                                            padding-bottom: 1px;">
                                            <div style="float: left; width: 99%; height: 19px; color: #FFFFFF; background-color: #507CD1;">
                                                <asp:Label ID="Label1" runat="server" Text="User Define parameters" Width="98%" Height="16px"></asp:Label>
                                            </div>
                                            <div style="float: left; width: 25%; padding-top: 1px; padding-bottom: 1px;">
                                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                                    <div style="float: left; width: 1%;">
                                                        &nbsp;</div>
                                                    <div style="float: left; width: 50%;">
                                                        Value Based</div>
                                                    <div style="float: left; width: 3%;">
                                                        :</div>
                                                    <div style="float: left; width: 5%; text-align: left;">
                                                        <asp:CheckBox ID="chkVal" runat="server" />
                                                    </div>
                                                </div>
                                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                                    <div style="float: left; width: 1%;">
                                                        &nbsp;</div>
                                                    <div style="float: left; width: 50%;">
                                                        Percentage Based</div>
                                                    <div style="float: left; width: 3%;">
                                                        :</div>
                                                    <div style="float: left; width: 5%; text-align: left;">
                                                        <asp:CheckBox ID="chkPer" runat="server" />
                                                    </div>
                                                </div>
                                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                                    <div style="float: left; width: 1%;">
                                                        &nbsp;</div>
                                                    <div style="float: left; width: 50%;">
                                                        Rental #</div>
                                                    <div style="float: left; width: 3%;">
                                                        :</div>
                                                    <div style="float: left; width: 45%; text-align: left;">
                                                        <asp:TextBox ID="txtRntNo" runat="server" Width="97%" Style="margin-left: 2%; text-align: right"
                                                            CssClass="TextBox"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                                    <div style="float: left; width: 1%;">
                                                        &nbsp;</div>
                                                    <div style="float: left; width: 50%;">
                                                        Percentage / Amount</div>
                                                    <div style="float: left; width: 3%;">
                                                        :</div>
                                                    <div style="float: left; width: 45%; text-align: left;">
                                                        <asp:TextBox ID="txtSheAmt" runat="server" Width="97%" Style="margin-left: 2%; text-align: right"
                                                            CssClass="TextBox"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div style="float: left; width: 100%; padding-top: 1px; padding-bottom: 1px;">
                                                    <div style="float: left; width: 1%;">
                                                        &nbsp;</div>
                                                    <div style="float: right; width: 60%; text-align: right;">
                                                        <asp:Button ID="btnAddSch" runat="server" Text="Add" CssClass="Button" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="float: right; width: 70%; padding-top: 1px; padding-bottom: 1px;">
                                                <div style="float: left; width: 98%; padding-top: 1px; padding-bottom: 1px;">
                                                    <div style="float: left; width: 1%;">
                                                        &nbsp;</div>
                                                    <div style="float: left; width: 50%;">
                                                        Percentage / Amount</div>
                                                    <div style="float: left; width: 3%;">
                                                        :</div>
                                                    <div style="float: left; width: 45%; text-align: left;">
                                                        <asp:TextBox ID="TextBox1" runat="server" Width="97%" Style="margin-left: 2%; text-align: right"
                                                            CssClass="TextBox"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
