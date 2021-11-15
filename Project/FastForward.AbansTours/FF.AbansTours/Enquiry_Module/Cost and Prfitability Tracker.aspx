<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cost and Prfitability Tracker.aspx.cs" Inherits="FF.AbansTours.Enquiry_Module.Cost_and_Prfitability_Tracker" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>

        $('#GridView1 .pieChart').each(function (index, value) {
            var percent = $(this).text();
            var deg = 360 / 100 * percent;
            $(this).html('<div class="percent">' + Math.round(percent) + '%' + '</div><div class="pieBack"></div><div ' + (percent > 50 ? ' class="slice gt50"' : 'class="slice"') + '><div class="pie"></div>' + (percent > 50 ? '<div class="pie fill"></div>' : '') + '</div>');
            $(this).find('.slice .pie').css({
                '-moz-transform': 'rotate(' + deg + 'deg)',
                '-webkit-transform': 'rotate(' + deg + 'deg)',
                '-o-transform': 'rotate(' + deg + 'deg)',
                'transform': 'rotate(' + deg + 'deg)'
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 height5 ">
                </div>
            </div>
            <div class="row rowmargin0 col-md-12">
                <div class="panel panel-danger">
                    <div class="panel-heading pannelheading">
                        Cost and Profitability Tracker
                    </div>
                    <div class="panel-body paddingleft0 paddingright30">
                        <div class="row no-margin-left">
                            <div class="row">
                                <div class="col-md-12 height5 ">
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="row">
                                    <div class="col-md-4 paddingtopbottom0">
                                        Profit Center
                                    </div>
                                    <div class="col-md-8 paddingtopbottom0">
                                        <div class="col-md-10 padding0 ">
                                            <asp:TextBox ID="txtProc" runat="server" CssClass="textbox"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1 padding0 ">
                                            <asp:ImageButton ID="imgbtnProNo"  runat="server" ImageUrl="~/Images/icon_search.png"
                                                ImageAlign="Middle" CssClass="imageicon" OnClick="imgbtnProNo_Click" />
                                        </div>

                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-md-4 paddingtopbottom0">
                                        Request #
                                    </div>
                                    <div class="col-md-8 paddingtopbottom0">
                                        <div class="col-md-10 padding0 displayinlineblock">
                                            <asp:TextBox ID="txtReqNo" runat="server" CssClass="textbox"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2 padding0 displayinlineblock">
                                            <asp:ImageButton ID="imgbtnReqno" runat="server" ImageUrl="~/Images/icon_search.png"
                                                ImageAlign="Middle" CssClass="imageicon" OnClick="imgbtnReqno_Click" />
                                        </div>

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4 paddingtopbottom0">
                                        Cost Sheet Ref
                                    </div>
                                    <div class="col-md-8 paddingtopbottom0">
                                        <div class="col-md-10 padding0 displayinlineblock">
                                            <asp:TextBox ID="txtCostsheet" runat="server" CssClass="textbox"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2 padding0 displayinlineblock">
                                            <asp:ImageButton ID="imgbtnCostS" runat="server" ImageUrl="~/Images/icon_search.png"
                                                ImageAlign="Middle" CssClass="imageicon" OnClick="imgbtnCostS_Click" />
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="row">
                                    <div class="col-md-4 paddingtopbottom0">
                                        Customer
                                    </div>
                                    <div class="col-md-8 paddingtopbottom0">
                                        <div class="col-md-10 padding0 displayinlineblock">
                                            <asp:TextBox ID="txtcustomer" runat="server" CssClass="textbox"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2 padding0 displayinlineblock">
                                            <asp:ImageButton ID="imgbtncustomer" runat="server" ImageUrl="~/Images/icon_search.png"
                                                ImageAlign="Middle" CssClass="imageicon" OnClick="imgbtncustomer_Click" />
                                        </div>

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4 paddingtopbottom0">
                                        Service Code
                                    </div>
                                    <div class="col-md-8 paddingtopbottom0">
                                        <div class="col-md-10 padding0 displayinlineblock">
                                            <asp:TextBox ID="txtcatergory" runat="server" CssClass="textbox"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2 padding0 displayinlineblock">
                                            <asp:ImageButton ID="imgbtncatergory" runat="server" ImageUrl="~/Images/icon_search.png"
                                                ImageAlign="Middle" CssClass="imageicon" OnClick="imgbtncatergory_Click" />
                                        </div>

                                    </div>
                                </div>

                            </div>
                            <div class="col-md-3">
                                <div class="row">
                                    <div class="col-md-4 paddingtopbottom0">
                                        From Date
                                    </div>
                                    <div class="col-md-8 paddingtopbottom0">
                                        <div class="col-md-10 padding0 displayinlineblock">
                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox" Format="dd/MMM/yyyy"
                                                onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtFromDate"
                                                PopupButtonID="imgbtnFromDate" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                        <div class="col-md-2 padding0 displayinlineblock">
                                            <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/calendar.png"
                                                ImageAlign="Middle" CssClass="imageicon" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4 paddingtopbottom0">
                                        From Date
                                    </div>
                                    <div class="col-md-8 paddingtopbottom0">
                                        <div class="col-md-10 padding0 displayinlineblock">
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox" Format="dd/MMM/yyyy"
                                                onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDate"
                                                PopupButtonID="imgbtnToDate" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                        <div class="col-md-2 padding0 displayinlineblock">
                                            <asp:ImageButton ID="imgbtnToDate" runat="server" ImageUrl="~/Images/calendar.png"
                                                ImageAlign="Middle" CssClass="imageicon" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-1">
                                        <asp:RadioButton ID="chkDetails" GroupName="option" CausesValidation="false"   runat="server" />
                                    </div>
                                    <div class="col-sm-3 labelText1">
                                        Details
                                    </div>
                                    <div class="col-sm-1">
                                        <asp:RadioButton ID="chkSummery"  GroupName="option"  runat="server" />
                                    </div>
                                    <div class="col-sm-3 labelText1">
                                        Summery
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-1">


                                <asp:LinkButton ID="lbtnSearchall" runat="server" OnClick="lbtnSearchall_Click">
                                                            <span class="glyphicon glyphicon-search fontsize30 " aria-hidden="true"></span>
                                </asp:LinkButton>


                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 height5 ">
                    </div>
                </div>
                <div class="row rowmargin0">
                    <div class="col-md-8 paddingleft0">
                        <div class="panel panel-danger">
                            <div class="panel-heading pannelheading">
                            </div>
                            <div class="panel-body panelscollbar height450">
                                <asp:Panel runat="server" ID="pnlSuumery" Visible="false">
                                    <asp:GridView ID="grdsummery" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Profit Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_pro" runat="server" Text='<%# Bind("qch_sbu") %>' Width="200px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Cost">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_COST" runat="server" Style="text-align: right" Text='<%# Bind("QCH_TOT_COST_LOCAL","{0:n}") %>' Width="40px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Revenue">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_REVERNUE" runat="server" Style="text-align: right" Text='<%# Bind("QCH_TOT_VALUE","{0:n}") %>' Width="40px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GP">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_GP" runat="server" Text='<%# Bind("QCH_GP","{0:n}") %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GP %">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_GpPre" runat="server" Text='<%# Bind("QCH_GP_Pre") %>' Width="40px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                                 <asp:Panel runat="server" ID="pnlDetails" Visible="false">
                                    <asp:GridView ID="grdDetails" EmptyDataText="No data found..." ShowHeaderWhenEmpty="True" runat="server" GridLines="None" CssClass="table" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Cost Sheet #">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_no" runat="server" Text='<%# Bind("qch_cost_no") %>' Width="200px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_dt" runat="server" Text='<%# Bind("qch_dt", "{0:dd/MMM/yyyy}") %>' Width="80px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Cost">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_COST" runat="server" Style="text-align: right" Text='<%# Bind("QCH_TOT_COST_LOCAL","{0:n}") %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Revenu">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_REVERNUE" runat="server" Style="text-align: right" Text='<%# Bind("QCH_TOT_VALUE","{0:n}") %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GP">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_GP" runat="server" Text='<%# Bind("QCH_GP","{0:n}") %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GP %">
                                                <ItemTemplate>
                                                    <asp:Label ID="col_GpPre" runat="server" Text='<%# Bind("QCH_GP_Pre") %>' Width="40px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4 paddingleft0 paddingright0">
                        <div class="row rowmargin0">
                            <div class="panel panel-danger">
                                <div class="panel-heading pannelheading">
                                    GP Chart
                                </div>

                                <asp:Chart ID="Chart1" BorderSkin-BackColor="YellowGreen" Visible="false" runat="server" Height="200" Width="362">
                                    <Series>
                                        <asp:Series Name="Default" Label="#VALX: #VALY{N0}"></asp:Series>
                                    </Series>
                                    <ChartAreas >
                                        
                                        <asp:ChartArea Name="ChartArea1">
                                           
                                        </asp:ChartArea>
                                    </ChartAreas>

                                    <BorderSkin BackColor="Transparent" PageColor="Transparent"
                                        SkinStyle="Emboss" />
                                </asp:Chart>
                            </div>

                        </div>
                        <div class="row rowmargin0">

                            <div class="panel panel-danger">
                                <div class="panel-heading pannelheading">
                                    Revenue Chart 
                                </div>

                                <asp:Chart ID="Chart2" BorderSkin-BackColor="YellowGreen" Visible="false" runat="server" Height="200" Width="362">
                                    <Series>
                                        <asp:Series Name="Default" Label="#VALX: #VALY{N0}"></asp:Series>
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                    </ChartAreas>

                                    <BorderSkin BackColor="Transparent" PageColor="Transparent"
                                        SkinStyle="Emboss" />
                                </asp:Chart>
                            </div>


                        </div>
                    </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

     <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="UserPopoup" runat="server" Enabled="True" TargetControlID="Button3"
                PopupControlID="pnlpopup" CancelControlID="btnClose" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>

    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlpopup" DefaultButton="lbtnSearch">
        <div runat="server" id="test" class="panel panel-default height350 width1000">
            <asp:Label ID="lblvalue" runat="server" Text="Label" Visible="false"></asp:Label>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:LinkButton ID="btnClose" runat="server">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                    </asp:LinkButton>
                    <%--<span>Commen Search</span>--%>
                    <div class="col-sm-11">
                    </div>
                    <div class="col-sm-1">
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12" id="Div4" runat="server">
                        <div class="row">
                            <div class="col-sm-12">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height5">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12" id="search" runat="server">
                                <div class="col-sm-2 labelText1">
                                    Search by key
                                </div>
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy10" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                    <ContentTemplate>
                                        <div class="col-md-2 paddingRight5 ">
                                            <asp:DropDownList ID="ddlSearchbykey" runat="server" class="textbox">
                                            </asp:DropDownList>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="col-md-2 labelText1">
                                    Search by word
                                </div>
                                <div class="col-sm-2 paddingRight5 ">
                                    <asp:TextBox ID="txtSearchbyword" CausesValidation="false" placeholder="Search by word" class="textbox" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbyword_TextChanged"></asp:TextBox>
                                </div>
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy9" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                    <ContentTemplate>
                                        <div class="col-sm-1 paddingLeft0">
                                            <asp:LinkButton ID="lbtnSearch" runat="server" OnClick="lbtnSearch_Click">
                                            <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                            </asp:LinkButton>
                                        </div>
                                    </ContentTemplate>

                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtSearchbyword" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height5">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height5">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy4" runat="server"></asp:ScriptManagerProxy>
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdResult" CausesValidation="false" runat="server" GridLines="None" CssClass="table table-hover" PageSize="7" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResult_SelectedIndexChanged" OnPageIndexChanging="grdResult_PageIndexChanging">
                                            <Columns>
                                                <asp:ButtonField Text="select" CommandName="Select" ItemStyle-Width="10" />

                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
