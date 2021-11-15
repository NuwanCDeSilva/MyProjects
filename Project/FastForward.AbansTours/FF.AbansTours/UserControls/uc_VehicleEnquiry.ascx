<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_VehicleEnquiry.ascx.cs" Inherits="FF.AbansTours.UserControls.uc_VehicleEnquery" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
 <link href="js/bootstrap/dist/css/bootstrap.css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="js/jquery.gritter/css/jquery.gritter.css" />
    <link rel="stylesheet" href="fonts/font-awesome-4/css/font-awesome.min.css">

 <link href="css/style.css" rel="stylesheet" />
    <link href="css/StyleSheet.css" rel="stylesheet" type="text/css" />

<style>
    .table tbody td {
    font-size: 11px;
}
</style>
<div class="col-md-12">
    <div class="row">
        <div class="col-md-12">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
            <div class="panel panel-default">
                <div class="row">
                    <div style="padding-bottom:23px; padding-top:8px;">
                    <div class="col-md-12">
                        <asp:Panel runat="server" DefaultButton="lbtnSearch">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <div class="col-md-11 padding0">
                                        <div class="col-md-3 padding0">
                                            <div class="col-md-5 padding0">
                                                Vehicle Type
                                            </div>
                                            <div class="col-md-7" style="padding-left: 0px; padding-right: 0px;">
                                                <asp:DropDownList ID="ddlVehicleType" Width="100%" runat="server" CssClass="input-xlarge focused"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="col-md-5 padding0">
                                                Vehicle
                                            </div>
                                            <div class="col-md-7" style="padding-left: 0px; padding-right: 0px;">
                                                <asp:TextBox ID="txtVehicle" runat="server" Width="100%" class="input-xlarge focused" />
                                            </div>
                                        </div>
                                        <div class="col-md-6" style="padding-left: 0px; padding-right: 0px;">
                                            <div class="col-md-3 padding0">
                                                Allocated  Date
                                            </div>
                                            <div class="col-md-3" style="padding-left: 0px; padding-right: 0px;">
                                                <asp:TextBox ID="txtExpectedDate" CssClass="input-xlarge focused" runat="server" Width="100%"></asp:TextBox>
                                                <asp:CalendarExtender ID="txtExpectedDateExtender" runat="server" Enabled="True" Format="dd/MMM/yyyy" PopupButtonID="cal" TargetControlID="txtExpectedDate"></asp:CalendarExtender>

                                            </div>
                                            <div class="col-md-3" style="padding-right:0px;">
                                                <img alt="Calendar.." height="16" src="../images/calendar.png" width="16" id="cal" style="cursor: pointer" />
                                                <MKB:TimeSelector ID="tmExpect" Height="12px" runat="server" BorderStyle="None" DisplayButtons="False" DisplaySeconds="False" SelectedTimeFormat="TwentyFour"></MKB:TimeSelector>
                                            </div>
                                            <div class="col-md-1" style="padding-right:3px;padding-left:0px;height:22px;">
                                                <asp:CheckBox Text="" id="chkDay" runat="server" />
                                            </div>
                                            <div class="col-md-2" style="padding-right:0px;padding-left:0px; margin-left:-21px;">
                                                <asp:Label Text="Whole Day" runat="server" />
                                            </div>
                                        </div>
                                        <%--<div class="col-md-4">
                                <div class="col-md-4 padding2">
                                    Driver
                                </div>
                                <div class="col-md-8 padding2">
                                    <asp:TextBox ID="txtDriver" class="form-control" AutoPostBack="true" runat="server"  />
                                </div>
                            </div>--%>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="lbtnSearch" OnClick="lbtnSearch_Click" runat="server">
                                            <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                    </div>
                </div>
                <div class="row ">
                    <div class="col-md-12">
                        <div class="panel panel-default">
                            <div class="panel panel-heading">
                                <strong><b>Not Allocated Vehicle</b></strong>
                            </div>
                            <div class="" style="height: 150px; overflow: auto;">
                                <asp:GridView ID="dgvInquery" CssClass="table table-hover table-striped"
                                    ShowHeaderWhenEmpty="true" EmptyDataText="No data found..."
                                    runat="server" GridLines="None" PagerStyle-CssClass="cssPager"
                                    AutoGenerateColumns="False">
                                    <EditRowStyle BackColor="MidnightBlue" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Fleet #">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFleetNo" Text='<%# Bind("gce_fleet") %>' runat="server" Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Enquiry #">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEnq" Text='<%# Bind("gce_enq_id") %>' runat="server" Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reference">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRef" Text='<%# Bind("gce_ref") %>' runat="server" Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Customer">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCust" ToolTip='<%# Bind("gce_name") %>' Text='<%# Bind("gce_cus_cd") %>' runat="server" Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cust. Mobile">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMobile" Text='<%# Bind("gce_mob") %>' runat="server" Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Expected Time">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExTime" Text='<%# Bind("gce_expect_dt","{0:dd/MMM/yyy HH:MM}") %>' runat="server" Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Return Time">
                                            <ItemTemplate>
                                                <asp:Label ID="lblReturnTime" Text='<%# Bind("gce_ret_dt","{0:dd/MMM/yyy HH:MM}") %>' runat="server" Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="cssPager"></PagerStyle>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row ">
                    <div class="col-md-12">
                        <div class="panel panel-default">
                            <div class="panel panel-heading">
                                <strong><b>Not Allocated Vehicle</b></strong>
                            </div>
                            <div class="" style="height: 150px; overflow: auto;">

                                <asp:GridView ID="dgvNotAllocate" CssClass="table table-hover table-striped"
                                    ShowHeaderWhenEmpty="true" EmptyDataText="No data found..."
                                    runat="server" GridLines="None" PagerStyle-CssClass="cssPager"
                                    AutoGenerateColumns="False">
                                    <EditRowStyle BackColor="MidnightBlue" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Vehicle #">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVehNo" Text='<%# Bind("mstf_regno") %>' runat="server" Width="80px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Model">
                                            <ItemTemplate>
                                                <asp:Label ID="lblModel" Text='<%# Bind("mstf_model") %>' runat="server" Width="80px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vehicle Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVehTp" Text='<%# Bind("mstf_veh_tp") %>' runat="server" Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Owner Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOwnTp" Text='<%# Bind("mstf_own") %>' runat="server" Width="200px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Owner Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOwnName" Text='<%# Bind("mstf_own_nm") %>' runat="server" Width="80px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Owner Contact">
                                            <ItemTemplate>
                                                <asp:Label ID="lblContact" Text='<%# Bind("mstf_own_cont") %>' runat="server" Width="80px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="cssPager"></PagerStyle>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
                    </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</div>
 <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery.cookie/jquery.cookie.js"></script>
    <script type="text/javascript" src="../js/jquery.pushmenu/js/jPushMenu.js"></script>
    <script type="text/javascript" src="../js/jquery.nanoscroller/jquery.nanoscroller.js"></script>
    <script type="text/javascript" src="../js/jquery.sparkline/jquery.sparkline.min.js"></script>
    <script type="text/javascript" src="../js/jquery.ui/jquery-ui.js"></script>
    <script type="text/javascript" src="../js/jquery.gritter/js/jquery.gritter.js"></script>


    <script type="text/javascript" src="../js/behaviour/core.js"></script>
