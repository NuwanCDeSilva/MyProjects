<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AllocateDriverVehicle.aspx.cs" Inherits="FF.AbansTours.AllocateDriverVehicle" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:HiddenField ID="txtconformmessageValue" runat="server" />
    <asp:UpdatePanel ID="main" runat="server">
        <ContentTemplate>

            <div class="row">

                <div visible="false" class="alert alert-success" role="alert" runat="server" id="DivAsk">
                    <div class="col-sm-11  buttonrow ">
                        <strong>Well done!</strong>
                        <asp:Label ID="lblAsk" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-1  buttonrow">
                        <div class="col-sm-3  buttonrow">
                            <asp:LinkButton ID="lbtColse" runat="server" CausesValidation="false" CssClass="floatright" OnClick="lbtColse_Click">
                                        <span class="glyphicon glyphicon-remove " aria-hidden="true" ></span>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>

                <div visible="false" class="alert alert-danger" role="alert" runat="server" id="Div1">
                    <div class="col-sm-11  buttonrow ">
                        <strong>Oh snap!</strong>
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-1  buttonrow">
                        <div class="col-sm-3  buttonrow">
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false" CssClass="floatright" OnClick="LinkButton2_Click">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="contentspanel" runat="server">
        <ContentTemplate>

            <div style="height: 22px; width: 100%; text-align: right; background-color: Silver;">

                <asp:Button ID="btnaddnew" Visible="false" Text="Add New Allocation" runat="server" Width="106px" OnClick="btnaddnew_Click" />

                <asp:Button ID="btnSave" Text="Save" runat="server" Width="80px" Enabled="true"
                    ValidationGroup="Save" OnClick="btnSave_Click" />
                <asp:Button ID="btnClear" Text="Clear" runat="server" Width="80px" OnClick="btnClear_Click" />

                <asp:ConfirmButtonExtender ID="CbeSave" runat="server" TargetControlID="btnSave"
                    ConfirmText="Do you want to save?" ConfirmOnFormSubmit="false">
                </asp:ConfirmButtonExtender>

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
                            Allocate Driver
                        </div>

                        <div class="panel-body paddingleft0 paddingright30">

                            <div class="row no-margin-left">
                                <div class="col-md-2 padding3">
                                    Registration No
                                </div>

                                <div class="col-md-2 padding5">
                                    <asp:DropDownList ID="ddlvehicle" runat="server" CssClass="textbox ddlhight1" AutoPostBack="true" TabIndex="1" OnSelectedIndexChanged="ddlvehicle_SelectedIndexChanged"></asp:DropDownList>
                                </div>

                                <div class="col-md-2 padding3">
                                    Driver
                                </div>

                                <div class="col-md-2 padding5">
                                    <asp:DropDownList ID="ddldriver" runat="server" CssClass="textbox ddlhight1" AutoPostBack="true" TabIndex="2" OnSelectedIndexChanged="ddldriver_SelectedIndexChanged"></asp:DropDownList>
                                </div>

                                <div class="col-md-2 padding3">
                                    Active
                                </div>

                                <div class="col-md-2 padding5">
                                    <asp:CheckBox ID="chkactive" runat="server" TabIndex="5" AutoPostBack="true" />
                                </div>

                            </div>

                            <div>
                                <div class="row">
                                    <div class="col-sm-12 height5">
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
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>

                                <div class="row no-margin-left">

                                    <div class="col-md-2 padding3">
                                    From
                                </div>

                                <div class="col-md-2 padding5">
                                    <div class="col-md-10 padding0 displayinlineblock">
                                        <asp:TextBox ID="txtfrom" runat="server" CssClass="textbox" Format="dd/MMM/yyyy"
                                            onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtfrom"
                                            PopupButtonID="imgbtnselectfromdate" Format="dd/MMM/yyyy">
                                        </asp:CalendarExtender>
                                    </div>
                                    <div class="col-md-2 padding0 displayinlineblock">
                                        <asp:ImageButton ID="imgbtnselectfromdate" runat="server" TabIndex="3" ImageUrl="~/Images/calendar.png"
                                            ImageAlign="Middle" CssClass="imageicon" />
                                    </div>
                                </div>

                                    <div class="col-md-2 padding3">
                                        To
                                    </div>

                                    <div class="col-md-2 padding5">
                                        <div class="col-md-10 padding0 displayinlineblock">
                                            <asp:TextBox ID="txtto" runat="server" CssClass="textbox" Format="dd/MMM/yyyy"
                                                onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtto"
                                                PopupButtonID="imgbtnselecttodate" Format="dd/MMM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                        <div class="col-md-2 padding0 displayinlineblock">
                                            <asp:ImageButton ID="imgbtnselecttodate" runat="server" TabIndex="4" ImageUrl="~/Images/calendar.png"
                                                ImageAlign="Middle" CssClass="imageicon" />
                                        </div>
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
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-12 height5">
                                    </div>
                                </div>


                                <div class="row no-margin-left">
                                    <div class="col-md-2 padding3">
                                        
                                    </div>

                                    <div class="col-md-2 padding5">
                                        <div style="width:395%; position: relative; float: left; top: 0px; left: 0px; height: 275px; overflow: scroll">
                                            <asp:GridView ID="gridallocations" runat="server" AutoGenerateColumns="False" CssClass="table-bordered" AutoGenerateSelectButton="True" OnSelectedIndexChanged="gridallocations_SelectedIndexChanged">
                                                <Columns>
                                                    <asp:BoundField DataField="MFD_VEH_NO" HeaderText="Vehicle No" SortExpression="MFD_VEH_NO" ItemStyle-Width="100px" >
                                                    <ItemStyle Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="MEMP_FIRST_NAME" HeaderText="Driver Name" SortExpression="MEMP_FIRST_NAME" ItemStyle-Width="200px" >
                                                    <ItemStyle Width="200px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="MFD_DRI" HeaderText="Driver Code" SortExpression="MFD_DRI" />
                                                    <asp:BoundField DataField="MFD_ACT_TEXT" HeaderText="Is Active" SortExpression="MFD_ACT_TEXT" />
                                                    <asp:BoundField DataField="MFD_FRM_DT" HeaderText="From Date" SortExpression="MFD_FRM_DT" />
                                                    <asp:BoundField DataField="MFD_TO_DT" HeaderText="To Date" SortExpression="MFD_TO_DT" />
                                                    <asp:BoundField DataField="mfd_seq" HeaderText="Allocation Entry ID" SortExpression="mfd_seq" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1px" >
                                                    </asp:BoundField>
                                                </Columns>
                                                <HeaderStyle ForeColor="Black" />
                                                <SelectedRowStyle BackColor="Silver" />
                                            </asp:GridView>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
