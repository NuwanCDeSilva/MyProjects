<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DriverInquiry.aspx.cs" Inherits="FF.AbansTours.DriverInquiry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="upReceiptEntry" runat="server" ChildrenAsTriggers="true" UpdateMode="Always">
        <ContentTemplate>
            <div style="height: 22px; width: 100%; text-align: right; background-color: Silver;">
                <asp:Button ID="btnSearch" Text="Search" runat="server" Width="80px" Enabled="true" OnClick="btnSearch_Click" />
                <asp:Button ID="btnClear" Text="Clear" runat="server" Width="80px" OnClick="btnClear_Click" />
                <asp:ConfirmButtonExtender ID="CbeClear" runat="server" TargetControlID="btnClear"
                    ConfirmText="Do you want to clear?" ConfirmOnFormSubmit="false">
                </asp:ConfirmButtonExtender>
            </div>
            <div class="col-md-12">
                &nbsp;
            </div>
            <div>
                <div class="row rowmargin0 col-md-12">
                    <div class="panel panel-default">
                        <div class="panel-heading pannelheading">
                            Trip Tracker Enquiry
                        </div>
                        <div class="panel-body paddingleft0 paddingright30">
                            <div class="row no-margin-left">
                                <div class="col-md-2 padding3">
                                    Vehicle No
                                </div>
                                <div class="col-md-2 padding5">
                                    <div class="col-md-10 padding0 displayinlineblock">
                                        <asp:TextBox ID="txtVehicleNo" runat="server" CssClass="textbox"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2 padding0 displayinlineblock">
                                        <asp:ImageButton ID="imgbtnVehicleNo" runat="server" ImageUrl="~/Images/icon_search.png"
                                            ImageAlign="Middle" CssClass="imageicon" OnClick="imgbtnVehicleNo_Click" />
                                    </div>

                                </div>
                                <div class="col-md-2 padding3">
                                    Driver EPF No
                                </div>
                                <div class="col-md-2 padding5 ">
                                    <div class="col-md-10 padding0 displayinlineblock">
                                        <asp:TextBox ID="txtDriver" runat="server" CssClass="textbox"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2 padding0 displayinlineblock">
                                        <asp:ImageButton ID="imgbtnDriverEPFNo" runat="server" ImageUrl="~/Images/icon_search.png"
                                            ImageAlign="Middle" CssClass="imageicon" OnClick="imgbtnDriverEPFNo_Click" />
                                    </div>
                                </div>
                                <div class="col-md-2 padding3">
                                    Customer Code
                                </div>
                                <div class="col-md-2 padding5">
                                    <div class="col-md-10 padding0 displayinlineblock">
                                        <asp:TextBox ID="txtCustomer" runat="server" CssClass="textbox"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2 padding0 displayinlineblock">
                                        <asp:ImageButton ID="imgbtnCustomerCode" runat="server" ImageUrl="~/Images/icon_search.png"
                                            ImageAlign="Middle" CssClass="imageicon" OnClick="imgbtnCustomerCode_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row no-margin-left">
                                <div class="col-md-2 padding3">
                                    From Date
                                </div>
                                <div class="col-md-2 padding5">
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
                                <div class="col-md-2 padding3">
                                    To Date
                                </div>
                                <div class="col-md-2 padding0">
                                    <div class="col-md-10 padding0 displayinlineblock">
                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox" Format="dd/MMM/yyyy"
                                            onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate"
                                            PopupButtonID="imgbtnToDate" Format="dd/MMM/yyyy">
                                        </asp:CalendarExtender>
                                    </div>
                                    <div class="col-md-2 padding0 displayinlineblock">
                                        <asp:ImageButton ID="imgbtnToDate" runat="server" ImageUrl="~/Images/calendar.png"
                                            ImageAlign="Middle" CssClass="imageicon" />
                                    </div>
                                </div>
                                <div class="col-md-2 padding3">
                                </div>
                                <div class="col-md-2 padding0">
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    &nbsp;
                </div>
                <div class="row rowmargin0 col-md-12">
                    <div class="panel panel-default">
                        <div class="panel-body paddingleft0">
                            <div class="row no-margin-left">
                                <div class="col-md-12 padding0">
                                    <asp:GridView ID="grdDriverInquiry" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered" AllowPaging="true"
                                        OnPageIndexChanging="grdDriverInquiry_PageIndexChanging" >
                                        <Columns>
                                            <asp:BoundField DataField='GCE_ENQ_ID' HeaderText='Inquiry No' ShowHeader="true" />
                                            <asp:BoundField DataField='GCE_REF' HeaderText='ReF No' />
                                            <asp:BoundField DataField='GCE_NAME' HeaderText='Customer Name' />
                                            <asp:BoundField DataField='GCE_MOB' HeaderText='Mobile' />
                                            <asp:BoundField DataField='GCE_EXPECT_DT' HeaderText='From Date' DataFormatString="{0:dd/MMM/yyyy}" />
                                            <asp:BoundField DataField='GCE_RET_DT' HeaderText='To Date' DataFormatString="{0:dd/MMM/yyyy}" />
                                            <asp:BoundField DataField='GCE_FLEET' HeaderText='Vehicle No' />
                                            <asp:BoundField DataField='MEMP_FIRST_NAME' HeaderText='Driver' />
                                            <asp:BoundField DataField='GCE_FRM_TN' HeaderText='From' />
                                            <asp:BoundField DataField='GCE_TO_TN' HeaderText='To' />
                                            <asp:BoundField DataField='GCE_VEH_TP' HeaderText='Vehicle Type' />
                                            <asp:BoundField DataField='GCE_NO_PASS' HeaderText='passengers' />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
