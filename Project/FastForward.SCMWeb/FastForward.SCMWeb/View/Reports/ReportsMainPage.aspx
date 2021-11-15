<%@ Page Title="" Language="C#" UICulture="auto" MasterPageFile="~/View/AdminSite.Master"
    AutoEventWireup="true" CodeBehind="ReportsMainPage.aspx.cs" Inherits="FastForward.SCMWeb.View.Reports.ReportsMainPage" %>

<%@ Register Src="~/UserControls/ucProfitCenterSearch.ascx" TagPrefix="uc1" TagName="ucProfitCenterSearch" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <div class="panel panel-default marginLeftRight5">
        <div class="panel-body">
            <div class="col-sm-12">
                <h3>Audit Reports</h3>
                <div class="col-sm-3">
                    <div class="panel panel-default">
                        <div class="panel-heading">Reports</div>
                        <div class="panel-body">
                            <div class="row">
                                <asp:ListBox ID="lstMenuItems" runat="server" Height="100%" AutoPostBack="True" Width="100%" OnSelectedIndexChanged="lstMenuItems_SelectedIndexChanged"></asp:ListBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-9">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-md-10">
                                            <asp:Label Text="Report Name" ID="lblReportName" runat="server" />
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button ID="btnDisplay" Text="Display" runat="server" Width="80px" OnClick="btnDisplay_Click" />
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender4" runat="server" TargetControlID="btnDisplay"
                                                ConfirmText="Do you want to Display?" ConfirmOnFormSubmit="false">
                                            </asp:ConfirmButtonExtender>
                                        </div>
                                    </div>

                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="panel panel-default" runat="server" id="divProfitCenter" visible="false">
                                                <div class="panel-heading panelheading1">Profit Center</div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div id="divUS" runat="server" class="col-md-6">
                                                                <uc1:ucProfitCenterSearch runat="server" ID="ucProfitCenterSearch" />
                                                            </div>
                                                            <div class="col-md-1">
                                                                <asp:Button ID="btnAddParameters" Text="Add" runat="server" OnClick="btnAddParameters_Click" />
                                                            </div>
                                                            <div class="col-md-5">
                                                                <asp:Panel ID="pnlGrd" runat="server" Width="100%" Height="161px" ScrollBars="Auto">
                                                                    <asp:GridView ID="dgvPcs" runat="server">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="select">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkSelect" Text="" runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="PC">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPC" runat="server" Text='<%# Bind("PROFIT_CENTER") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Width="200px" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                                <asp:Button ID="btnSelectAllPC" Width="100px" Text="All" runat="server" OnClick="btnSelectAllPC_Click" />
                                                                <asp:Button ID="btnUnselectAllPc" Width="100px" Text="None" runat="server" OnClick="btnUnselectAllPc_Click" />
                                                                <asp:Button ID="btnClear" Width="100px" Text="Clear" runat="server" OnClick="btnClear_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-5 paddingRight0">
                                            <div class="panel panel-default">
                                                <div class="panel-heading panelheading1">Document Criteria</div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div id="divCompany" runat="server" visible="false">
                                                            <div class="col-sm-4 labelText1">
                                                                Company
                                                            </div>
                                                            <div class="col-sm-4 paddingRight5">
                                                                <asp:TextBox ID="txtCompany" Enabled="false" runat="server" CssClass="form-control" />
                                                            </div>
                                                            <div class="col-sm-1">
                                                                <asp:LinkButton ID="btnCompanyNew" runat="server" OnClick="btnCompanyNew_Click">
                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="col-sm-1   height10">
                                                                <asp:CheckBox Checked="true" ID="chkCompany" AutoPostBack="true" Text=" " runat="server" OnCheckedChanged="chkCompany_CheckedChanged" />
                                                            </div>
                                                            <div class="col-sm-1  ">
                                                                All
                                                            </div>
                                                            <div class="col-sm-1  ">
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>
                                                    <div class="row">

                                                        <div id="divDepartment" runat="server" visible="false">
                                                            <div class="col-sm-4 labelText1">
                                                                Department
                                                            </div>
                                                            <div class="col-md-4 paddingRight5  ">
                                                                <asp:TextBox ID="txtDepartment" Enabled="false" runat="server" CssClass="form-control" />
                                                            </div>
                                                            <div class="col-md-1  ">
                                                                <asp:LinkButton ID="btnDepartmentNew" runat="server" OnClick="btnDepartmentNew_Click">
                                                                   <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="col-md-1   height10">
                                                                <asp:CheckBox AutoPostBack="true" Checked="true" ID="chkDepartment" Text=" " runat="server" OnCheckedChanged="chkDepartment_CheckedChanged" />
                                                            </div>
                                                            <div class="col-md-1  ">
                                                                All
                                                            </div>
                                                            <div class="col-md-1  ">
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div id="divUser" runat="server" visible="false">
                                                            <div class="col-sm-4 labelText1">
                                                                User
                                                            </div>
                                                            <div class="col-md-4  paddingRight5 ">
                                                                <asp:TextBox ID="txtUser" Enabled="false" runat="server" CssClass="form-control" />
                                                            </div>
                                                            <div class="col-md-1  ">
                                                                <asp:LinkButton ID="btnUserNew" runat="server" OnClick="btnUserNew_Click">
                                                                   <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="col-md-1   height10">
                                                                <asp:CheckBox AutoPostBack="true" Checked="true" ID="chkUser" Text=" " runat="server" OnCheckedChanged="chkUser_CheckedChanged" />
                                                            </div>
                                                            <div class="col-md-1  ">
                                                                All
                                                            </div>
                                                            <div class="col-md-1  ">
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>
                                                    <div class="row">

                                                        <div id="divSalesType" runat="server" visible="false">
                                                            <div class="col-sm-4 labelText1">
                                                                Sales Type
                                                            </div>
                                                            <div class="col-md-4 paddingRight5  ">
                                                                <asp:TextBox ID="txtSalesType" Enabled="false" runat="server" CssClass="form-control" />
                                                            </div>
                                                            <div class="col-md-1  ">
                                                                <asp:LinkButton ID="btnSalesTypeNew" runat="server" OnClick="btnSalesTypeNew_Click">
                                                                   <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="col-md-1   height10">
                                                                <asp:CheckBox AutoPostBack="true" Checked="true" ID="chkSalesType" Text=" " runat="server" OnCheckedChanged="chkSalesType_CheckedChanged" />
                                                            </div>
                                                            <div class="col-md-1  ">
                                                                All
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>
                                                    <div class="row">

                                                        <div id="divSalesSubType" runat="server" visible="false">
                                                            <div class="col-sm-4 labelText1">
                                                                Sales Sub Type
                                                            </div>
                                                            <div class="col-md-4  paddingRight5 ">
                                                                <asp:TextBox ID="txtSalesSubType" Enabled="false" runat="server" CssClass="form-control" />
                                                            </div>
                                                            <div class="col-md-1  ">
                                                                <asp:LinkButton ID="btnSalesSubTypeNew" runat="server">
                                                                   <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="col-md-1   height10">
                                                                <asp:CheckBox AutoPostBack="true" Checked="true" ID="ckSalesSubType" Text=" " runat="server" OnCheckedChanged="ckSalesSubType_CheckedChanged" />
                                                            </div>
                                                            <div class="col-md-1  ">
                                                                All
                                                            </div>
                                                            <div class="col-md-1  ">
                                                                <asp:LinkButton ID="btnSalesSubTypeAddINew" runat="server" OnClick="btnSalesSubTypeAddINew_Click">
                                                                   <span class="glyphicon glyphicon-circle-arrow-down" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div id="divDocumentNo" runat="server" visible="false">
                                                            <div class="col-sm-4 labelText1">
                                                                <asp:Label ID="lblDocNum" Text="Document No" runat="server" />
                                                            </div>
                                                            <div class="col-md-4  paddingRight5 ">
                                                                <asp:TextBox ID="txtDocumentNo" Enabled="false" runat="server" CssClass="form-control" />
                                                            </div>
                                                            <div class="col-md-1  ">
                                                                <asp:LinkButton ID="btnDocumentNoNew" runat="server" OnClick="btnDocumentNoNew_Click">
                                                                   <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="col-md-1   height10">
                                                                <asp:CheckBox AutoPostBack="true" Checked="true" ID="chkDocumentNo" Text=" " runat="server" OnCheckedChanged="chkDocumentNo_CheckedChanged" />
                                                            </div>
                                                            <div class="col-md-1  ">
                                                                All
                                                            </div>
                                                            <div class="col-md-1  ">
                                                                <asp:LinkButton ID="btnDocumentNoAddINew" runat="server" OnClick="btnDocumentNoAddINew_Click">
                                                        <span class="glyphicon glyphicon-circle-arrow-down" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div id="divDirection" runat="server" visible="false">
                                                            <div class="col-sm-4 labelText1">
                                                                <asp:Label ID="lblDirection" Enabled="false" Text="Direction" runat="server" />
                                                            </div>
                                                            <div class="col-md-4 paddingRight5  ">
                                                                <asp:TextBox ID="txtDirection" runat="server" CssClass="form-control" />
                                                            </div>
                                                            <div class="col-md-1  ">
                                                                <asp:LinkButton ID="btnDirectionNew" runat="server" OnClick="btnDirectionNew_Click">
                                                                   <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="col-md-1   height10">
                                                                <asp:CheckBox AutoPostBack="true" Checked="true" ID="chkDirection" Text=" " runat="server" OnCheckedChanged="chkDirection_CheckedChanged" />
                                                            </div>
                                                            <div class="col-md-1  ">
                                                                All
                                                            </div>
                                                            <div class="col-md-1  ">
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>
                                                    <div class="row">

                                                        <div id="divEntryType" runat="server" visible="false">
                                                            <div class="col-sm-4 labelText1">
                                                                Entry Type
                                                            </div>
                                                            <div class="col-md-4  paddingRight5 ">
                                                                <asp:DropDownList ID="ddlEntryType" Enabled="false" CssClass="form-control" runat="server" Height="19px">
                                                                    <asp:ListItem>Invoice</asp:ListItem>
                                                                    <asp:ListItem>Receipt</asp:ListItem>
                                                                    <asp:ListItem>Movement</asp:ListItem>
                                                                    <asp:ListItem>Manual Documents </asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-md-1  ">
                                                                <asp:LinkButton ID="btnEntryTypeNew" runat="server" OnClick="btnEntryTypeNew_Click">
                                                                   <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="col-md-1   height10">
                                                                <asp:CheckBox AutoPostBack="true" Checked="true" ID="chkEntryType" Text=" " runat="server" OnCheckedChanged="chkEntryType_CheckedChanged" />
                                                            </div>
                                                            <div class="col-md-1  ">
                                                                All
                                                            </div>
                                                            <div class="col-md-1  ">
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>
                                                    <div class="row">

                                                        <div id="divReceiptType" runat="server" visible="false">
                                                            <div class="col-sm-4 labelText1">
                                                                Receipt Type
                                                            </div>
                                                            <div class="col-md-4  paddingRight5 ">
                                                                <asp:TextBox ID="txtReceiptType" Enabled="false" runat="server" CssClass="form-control" />
                                                            </div>
                                                            <div class="col-md-1  ">
                                                                <asp:LinkButton ID="btnReceiptTypeNew" runat="server" OnClick="btnReceiptTypeNew_Click">
                                                                   <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="col-md-1   height10">
                                                                <asp:CheckBox AutoPostBack="true" Checked="true" ID="chkReciptType" Text=" " runat="server" OnCheckedChanged="chkReciptType_CheckedChanged" />
                                                            </div>
                                                            <div class="col-md-1  ">
                                                                All
                                                            </div>
                                                            <div class="col-md-1  ">
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div id="divStatus" runat="server" visible="false">
                                                            <div class="col-sm-4 labelText1">
                                                                Status
                                                            </div>
                                                            <div class="col-md-4  paddingRight5 ">
                                                                <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false" CssClass="form-control" Height="19px">
                                                                    <asp:ListItem>Active</asp:ListItem>
                                                                    <asp:ListItem>Cancel</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-md-1  ">
                                                                <asp:LinkButton ID="btnStatusNew" runat="server" OnClick="btnStatusNew_Click">
                                                                   <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="col-md-1   height10">
                                                                <asp:CheckBox AutoPostBack="true" Checked="true" ID="chkStatus" Text=" " runat="server" OnCheckedChanged="chkStatus_CheckedChanged" />
                                                            </div>
                                                            <div class="col-md-1  ">
                                                                All
                                                            </div>
                                                            <div class="col-md-1  ">
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div id="divCustomer" runat="server" visible="false">
                                                            <div class="col-sm-4 labelText1">
                                                                Customer
                                                            </div>
                                                            <div class="col-md-4  paddingRight5 ">
                                                                <asp:TextBox ID="txtCustomer" Enabled="false" runat="server" CssClass="form-control" />
                                                            </div>
                                                            <div class="col-md-1  ">
                                                                <asp:LinkButton ID="btnCustomerNew" runat="server" OnClick="btnCustomerNew_Click">
                                                                   <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="col-md-1   height10">
                                                                <asp:CheckBox AutoPostBack="true" Checked="true" ID="chkCustomer" Text=" " runat="server" OnCheckedChanged="chkCustomer_CheckedChanged" />
                                                            </div>
                                                            <div class="col-md-1  ">
                                                                All
                                                            </div>
                                                            <div class="col-md-1  ">
                                                                <asp:LinkButton ID="btnCustomerAddINew" runat="server" OnClick="btnCustomerAddINew_Click">
                                                                   <span class="glyphicon glyphicon-circle-arrow-down" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div id="divExecutive" runat="server" visible="false">
                                                            <div class="col-sm-4 labelText1">
                                                                Executive
                                                            </div>
                                                            <div class="col-md-4  paddingRight5 ">
                                                                <asp:TextBox ID="txtExecutive" Enabled="false" runat="server" CssClass="form-control" />
                                                            </div>
                                                            <div class="col-md-1  ">
                                                                <asp:LinkButton ID="btnExecutiveNew" runat="server" OnClick="btnExecutiveNew_Click">
                                                                   <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="col-md-1   height10">
                                                                <asp:CheckBox AutoPostBack="true" Checked="true" ID="chkExecutive" Text=" " runat="server" OnCheckedChanged="chkExecutive_CheckedChanged" />
                                                            </div>
                                                            <div class="col-md-1  ">
                                                                All
                                                            </div>
                                                            <div class="col-md-1  ">
                                                                <asp:LinkButton ID="btnExecutiveAddINew" runat="server" OnClick="btnExecutiveAddINew_Click">
                                                                   <span class="glyphicon glyphicon-circle-arrow-down" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div id="divItemStatus" runat="server" visible="false">
                                                            <div class="col-sm-4 labelText1">
                                                                Item Status
                                                            </div>
                                                            <div class="col-md-4  paddingRight5 ">
                                                                <asp:TextBox ID="txtItemStatus" Enabled="false" runat="server" CssClass="form-control" />
                                                            </div>
                                                            <div class="col-md-1  ">
                                                                <asp:LinkButton ID="btnItemStatusNew" runat="server" OnClick="btnItemStatusNew_Click">
                                                                   <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="col-md-1   height10">
                                                                <asp:CheckBox AutoPostBack="true" Checked="true" ID="chkItemStatus" Text=" " runat="server" OnCheckedChanged="chkItemStatus_CheckedChanged" />
                                                            </div>
                                                            <div class="col-md-1  ">
                                                                All
                                                            </div>
                                                            <div class="col-md-1  ">
                                                                <asp:LinkButton ID="btnItemStatusAddINew" runat="server" OnClick="btnItemStatusAddINew_Click">
                                                                   <span class="glyphicon glyphicon-circle-arrow-down" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div id="divPayType" runat="server" visible="false">
                                                            <div class="col-sm-4 labelText1">
                                                                Pay Type
                                                            </div>
                                                            <div class="col-md-4  paddingRight5 ">
                                                                <asp:TextBox ID="txtPayType" Enabled="false" runat="server" CssClass="form-control" />
                                                            </div>
                                                            <div class="col-md-1  ">
                                                                <asp:LinkButton ID="btnPayTypeNew" runat="server" OnClick="btnPayTypeNew_Click">
                                                                   <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="col-md-1   height10">
                                                                <asp:CheckBox AutoPostBack="true" Checked="true" ID="chkPayType" Text=" " runat="server" OnCheckedChanged="chkPayType_CheckedChanged" />
                                                            </div>
                                                            <div class="col-md-1  ">
                                                                All
                                                            </div>
                                                            <div class="col-md-1  ">
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-7">
                                            <div class="panel panel-default">
                                                <div class="panel-heading panelheading1">Document Criteria</div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-sm-2 labelText1 ">
                                                            Year
                                                        </div>
                                                        <div class="col-sm-4 paddingRight5">
                                                            <asp:DropDownList ID="ddlYear" CssClass="form-control" Height="19px" Width="100%" runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-2 labelText1">
                                                            Month
                                                        </div>
                                                        <div class="col-sm-4 paddingRight5">
                                                            <asp:DropDownList ID="ddlMonth" CssClass="form-control" Height="19px" Width="100%" runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>
                                                    <div class="row">

                                                        <div class="col-sm-2 labelText1">
                                                            From
                                                        </div>
                                                        <div class="col-sm-4 paddingRight5">
                                                            <asp:TextBox ID="txtFromDate" Style="width: 86%; display: initial;" CssClass="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtFromDate_TextChanged" />
                                                            <asp:CalendarExtender ID="txtFromDateE" runat="server" Enabled="True"
                                                                Format="dd/MMM/yyyy" PopupButtonID="cal1" TargetControlID="txtFromDate">
                                                            </asp:CalendarExtender>
                                                            <img alt="Calendar.." height="16" src="../../images/icons/calendar.png" width="16" id="cal1" style="cursor: pointer" />
                                                        </div>
                                                        <div class="col-sm-2 labelText1">
                                                            To
                                                        </div>
                                                        <div class="col-sm-4 paddingRight5">
                                                            <asp:TextBox ID="txtTodate" Style="width: 86%; display: initial;" CssClass="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtTodate_TextChanged" />
                                                            <asp:CalendarExtender ID="txtTodateE" runat="server" Enabled="True"
                                                                Format="dd/MMM/yyyy" PopupButtonID="cal2" TargetControlID="txtTodate">
                                                            </asp:CalendarExtender>
                                                            <img alt="Calendar.." height="16" src="../../images/icons/calendar.png" width="16" id="cal2" style="cursor: pointer" />
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12 height5">
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-2 labelText1 ">
                                                            As at Date
                                                        </div>
                                                        <div class="col-sm-4 paddingRight5">
                                                            <asp:TextBox ID="txtAsAtDate" Style="width: 86%; display: initial;" Width="80%" CssClass="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtAsAtDate_TextChanged" />
                                                            <asp:CalendarExtender ID="txtAsAtDateE" runat="server" Enabled="True"
                                                                Format="dd/MMM/yyyy" PopupButtonID="cal" TargetControlID="txtAsAtDate">
                                                            </asp:CalendarExtender>
                                                            <img alt="Calendar.." height="16" src="../../images/icons/calendar.png" width="16" id="cal3" style="cursor: pointer" />
                                                        </div>
                                                        <div class="col-sm-6  ">
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12 height5">
                                                </div>
                                            </div>

                                            <div class="panel panel-default" runat="server" id="divitemCriteria" visible="false">
                                                <div class="panel-heading panelheading1">item Criteria</div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-sm-9  ">
                                                            <div class="row">
                                                                <div class="col-sm-3 labelText1">
                                                                    Item Cat. 1
                                                                </div>
                                                                <div class="col-sm-4  paddingRight5">
                                                                    <asp:TextBox ID="txtItemCate1" Enabled="false" runat="server" CssClass="form-control" />
                                                                </div>
                                                                <div class="col-sm-1  ">
                                                                    <asp:LinkButton ID="btnItemCate1New" runat="server" OnClick="btnItemCate1New_Click">
                                                                   <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-1 height10">
                                                                    <asp:CheckBox AutoPostBack="true" Checked="true" ID="chkItemCate1" Text=" " runat="server" OnCheckedChanged="chkItemCate1_CheckedChanged" />
                                                                </div>
                                                                <div class="col-sm-1  ">
                                                                    All
                                                                </div>
                                                                <div class="col-sm-1  ">
                                                                    <asp:LinkButton ID="btnItemCate1AddDNew" runat="server" OnClick="btnItemCate1AddDNew_Click">
                                                                   <span class="glyphicon glyphicon-circle-arrow-down" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-1  ">
                                                                    <asp:LinkButton ID="btnItemCate1AddINew" runat="server" OnClick="btnItemCate1AddINew_Click">
                                                                   <span class="glyphicon glyphicon-circle-arrow-right" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 height5">
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-3 labelText1 ">
                                                                    Item Cat. 2
                                                                </div>
                                                                <div class="col-sm-4  paddingRight5">
                                                                    <asp:TextBox ID="txtItemCate2" Enabled="false" runat="server" CssClass="form-control" />
                                                                </div>
                                                                <div class="col-sm-1  ">
                                                                    <asp:LinkButton ID="btnItemCate2New" runat="server" OnClick="btnItemCate2New_Click">
                                                                   <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-1   height10">
                                                                    <asp:CheckBox AutoPostBack="true" Checked="true" ID="chkItemCate2" Text=" " runat="server" OnCheckedChanged="chkItemCate2_CheckedChanged" />
                                                                </div>
                                                                <div class="col-sm-1  ">
                                                                    All
                                                                </div>
                                                                <div class="col-sm-1  ">
                                                                    <asp:LinkButton ID="btnItemCate2AddDNew" runat="server" OnClick="btnItemCate2AddDNew_Click">
                                                                   <span class="glyphicon glyphicon-circle-arrow-down" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-1  ">
                                                                    <asp:LinkButton ID="btnItemCate2AddINew" runat="server" OnClick="btnItemCate2AddINew_Click">
                                                                   <span class="glyphicon glyphicon-circle-arrow-right" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 height5">
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-3 labelText1 ">
                                                                    Item Cat. 3
                                                                </div>
                                                                <div class="col-sm-4 paddingRight5">
                                                                    <asp:TextBox ID="txtItemCate3" Enabled="false" runat="server" CssClass="form-control" />
                                                                </div>
                                                                <div class="col-sm-1  ">
                                                                    <asp:LinkButton ID="btnItemCate3New" runat="server" OnClick="btnItemCate3New_Click">
                                                                   <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-1   height10">
                                                                    <asp:CheckBox AutoPostBack="true" Checked="true" ID="chkItemCate3" Text=" " runat="server" OnCheckedChanged="chkItemCate3_CheckedChanged" />
                                                                </div>
                                                                <div class="col-sm-1  ">
                                                                    All
                                                                </div>
                                                                <div class="col-sm-1  ">
                                                                    <asp:LinkButton ID="btnItemCate3AddDNew" runat="server">
                                                                   <span class="glyphicon glyphicon-circle-arrow-down" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-1  ">
                                                                    <asp:LinkButton ID="btnItemCate3AddINew" runat="server" OnClick="btnItemCate3AddINew_Click">
                                                                   <span class="glyphicon glyphicon-circle-arrow-right" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 height5">
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-3 labelText1">
                                                                    Item Code
                                                                </div>
                                                                <div class="col-sm-4 paddingRight5">
                                                                    <asp:TextBox ID="txtItemCode" Enabled="false" runat="server" CssClass="form-control" />
                                                                </div>
                                                                <div class="col-sm-1  ">
                                                                    <asp:LinkButton ID="btnItemCodeNew" runat="server" OnClick="btnItemCodeNew_Click1">
                                                                   <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-1   height10">
                                                                    <asp:CheckBox AutoPostBack="true" Checked="true" ID="chkItemCode" Text=" " runat="server" OnCheckedChanged="chkItemCode_CheckedChanged" />
                                                                </div>
                                                                <div class="col-sm-1  ">
                                                                    All
                                                                </div>
                                                                <div class="col-sm-1  ">
                                                                    <asp:LinkButton ID="btnItemCodeAddDNew" runat="server">
                                                                   <span class="glyphicon glyphicon-circle-arrow-down" aria-hidden="true"></span>
                                                                    </asp:LinkButton>

                                                                </div>
                                                                <div class="col-sm-1  ">
                                                                    <asp:LinkButton ID="btnItemCodeAddINew" runat="server" OnClick="btnItemCodeAddINew_Click">
                                                                   <span class="glyphicon glyphicon-circle-arrow-right" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 height5">
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-3 labelText1">
                                                                    Brand
                                                                </div>
                                                                <div class="col-sm-4 paddingRight5">
                                                                    <asp:TextBox ID="txtBrand" Enabled="false" runat="server" CssClass="form-control" />
                                                                </div>
                                                                <div class="col-sm-1  ">
                                                                    <asp:LinkButton ID="btnBrandNew" runat="server" OnClick="btnBrandNew_Click1">
                                                                   <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-1   height10">
                                                                    <asp:CheckBox AutoPostBack="true" Checked="true" ID="chkBrand" Text=" " runat="server" OnCheckedChanged="chkBrand_CheckedChanged" />
                                                                </div>
                                                                <div class="col-sm-1  ">
                                                                    All
                                                                </div>
                                                                <div class="col-sm-1  ">
                                                                    <asp:LinkButton ID="btnBrandAddDNew" runat="server" OnClick="btnBrandAddDNew_Click">
                                                                   <span class="glyphicon glyphicon-circle-arrow-down" aria-hidden="true"></span>
                                                                    </asp:LinkButton>

                                                                </div>
                                                                <div class="col-sm-1  ">
                                                                    <asp:LinkButton ID="btnBrandAddINew" runat="server" OnClick="btnBrandAddINew_Click">
                                                                   <span class="glyphicon glyphicon-circle-arrow-right" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 height5">
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-3 labelText1">
                                                                    Model
                                                                </div>
                                                                <div class="col-sm-4 paddingRight5">
                                                                    <asp:TextBox ID="txtModel" Enabled="false" runat="server" CssClass="form-control" />
                                                                </div>
                                                                <div class="col-sm-1  ">
                                                                    <asp:LinkButton ID="btnModelNew" runat="server" OnClick="btnModelNew_Click">
                                                                   <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div class="col-sm-1   height10">
                                                                    <asp:CheckBox AutoPostBack="true" Checked="true" ID="chkModel" Text=" " runat="server" OnCheckedChanged="chkModel_CheckedChanged" />
                                                                </div>
                                                                <div class="col-sm-1  ">
                                                                    All
                                                                </div>
                                                                <div class="col-sm-1  ">
                                                                </div>
                                                                <div class="col-sm-1  ">
                                                                    <asp:LinkButton ID="btnModelDNew" runat="server" OnClick="btnModelDNew_Click">
                                                                   <span class="glyphicon glyphicon-circle-arrow-right" aria-hidden="true"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 height5">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-3  ">
                                                            <div>
                                                                <asp:ListBox ID="lstGroupingDetails" Width="100%" runat="server"></asp:ListBox>
                                                            </div>
                                                            <div style="text-align: center;">
                                                                <asp:Button ID="btnRemoveItems" Text="Remove Item" runat="server" OnClick="btnRemoveItems_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12" id="divFilteringDetails" runat="server" visible="false">
                                            <div class="panel panel-default">
                                                <div class="panel-heading panelheading1">Filtering details</div>
                                                <div class="panel-body">
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        Category 1
                                                                <div>
                                                                    <asp:ListBox ID="lstCate1" Width="100%" runat="server"></asp:ListBox>
                                                                </div>
                                                        <div style="text-align: center;">
                                                            <asp:Button ID="btnRemoveCate1" Text="Remove Item" runat="server" OnClick="btnRemoveCate1_Click" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-2">
                                                        Category 2
                                                                <div>
                                                                    <asp:ListBox ID="lstCate2" Width="100%" runat="server"></asp:ListBox>
                                                                </div>
                                                        <div style="text-align: center;">
                                                            <asp:Button ID="btnRemoveCate2" Text="Remove Item" runat="server" OnClick="btnRemoveCate2_Click" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-2">
                                                        Category 3
                                                                <div>
                                                                    <asp:ListBox ID="lstCate3" Width="100%" runat="server"></asp:ListBox>
                                                                </div>
                                                        <div style="text-align: center;">
                                                            <asp:Button ID="btnRemoveCate3" Text="Remove Item" runat="server" OnClick="btnRemoveCate3_Click" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-2">
                                                        item Code
                                                                <div>
                                                                    <asp:ListBox ID="lstItemCode" Width="100%" runat="server"></asp:ListBox>
                                                                </div>
                                                        <div style="text-align: center;">
                                                            <asp:Button ID="btnRemoveItemCode" Text="Remove Item" runat="server" OnClick="btnRemoveItemCode_Click" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-2">
                                                        brand
                                                                <div>
                                                                    <asp:ListBox ID="lstBrands" Width="100%" runat="server"></asp:ListBox>
                                                                </div>
                                                        <div style="text-align: center;">
                                                            <asp:Button ID="btnRemoveBrand" Text="Remove Item" runat="server" OnClick="btnRemoveBrand_Click" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                </div>
                                            </div>


                                        </div>

                                    </div>
                                </div>
                            </div>


                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none;" />
                            <asp:ModalPopupExtender ID="UserPopoup" runat="server" Enabled="True" TargetControlID="Button3"
                                PopupControlID="test" CancelControlID="btnClose" PopupDragHandleControlID="test" Drag="true" BackgroundCssClass="modalBackground">
                            </asp:ModalPopupExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:Panel ID="test" runat="server" CssClass="modalPopup" align="center" Style="display: none">
                        <asp:Label ID="lblTargetControl" runat="server" Text="Label" Visible="false"></asp:Label>
                        <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                        <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btnClose" runat="server" Text="Close" />
                                <table>
                                    <tr>
                                        <td>
                                            <label>
                                                Search by key</label>
                                            <asp:DropDownList ID="cmbSearchbykey" runat="server" Height="19px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <label class="control-label" for="radiobtns">
                                                Search by word
                                            </label>
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtSearchbyword" CausesValidation="false" AutoPostBack="true" runat="server" OnTextChanged="txtSearchbyword_TextChanged"></asp:TextBox>
                                                    <asp:ImageButton ID="ImageSearch" OnClick="ImageSearch_Click" CausesValidation="false" runat="server" ImageUrl="~/img/icons/specialsearch2icon.png" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Panel runat="server" ScrollBars="Auto">
                                    <asp:GridView ID="dvResultUser" CausesValidation="false" runat="server" OnSelectedIndexChanged="dvResultUser_SelectedIndexChanged" GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                        <Columns>
                                            <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10" />
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
