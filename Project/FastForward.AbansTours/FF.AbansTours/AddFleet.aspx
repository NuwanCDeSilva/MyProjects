<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddFleet.aspx.cs" Inherits="FF.AbansTours.AddFleet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script type="text/javascript">

        function jsDecimals(e) {
            var evt = (e) ? e : window.event;
            var key = (evt.keyCode) ? evt.keyCode : evt.which;
            if (key != null) {
                key = parseInt(key, 10);
                if ((key < 48 || key > 57) && (key < 96 || key > 105)) {
                    if (!jsIsUserFriendlyChar(key, "Decimals")) {
                        return false;
                    }
                }
                else {
                    if (evt.shiftKey) {
                        return false;
                    }
                }
            }
            return true;
        };

        function jsIsUserFriendlyChar(val, step) {
            if (val == 8 || val == 9 || val == 13 || val == 45 || val == 46) {
                return true;
            }
            if ((val > 16 && val < 21) || (val > 34 && val < 41)) {
                return true;
            }
            if (step == "Decimals") {
                if (val == 190 || val == 110) {
                    return true;
                }
            }
            return false;
        };

    </script>

    <style type="text/css">
        .divWaiting {
            position: absolute;
            background-color: #FAFAFA;
            z-index: 2147483647 !important;
            opacity: 0.8;
            overflow: hidden;
            text-align: center;
            top: 0;
            left: 0;
            height: 100%;
            width: 100%;
            padding-top: 20%;
        }
    </style>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="messages" runat="server">
        <ContentTemplate>
            <div class="row">
                <div visible="false" class="alert alert-success" role="alert" runat="server" id="DivAsk">
                    <div class="col-sm-11  buttonrow ">
                        <strong>Well done!</strong>
                        <asp:Label ID="lblAsk" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-1  buttonrow">
                        <div class="col-sm-3  buttonrow">
                            <asp:LinkButton ID="lbtColse" runat="server" CausesValidation="false" OnClick="lbtColse_Click" CssClass="floatright">
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
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false" OnClick="LinkButton2_Click" CssClass="floatright">
                                        <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>

                 <div visible="false" class="alert alert-info" role="alert" runat="server" id="DivInfo">
                    <div class="col-sm-11  buttonrow ">
                        <strong>Vehicle Availability !</strong>
                        <asp:Label ID="lblinfo" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-1  buttonrow">
                        <div class="col-sm-3  buttonrow">
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" OnClick="LinkButton1_Click" CssClass="floatright">
                                        <span class="glyphicon glyphicon-remove " aria-hidden="true" ></span>
                            </asp:LinkButton>
                        </div>
                    </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:UpdatePanel ID="upReceiptEntry" runat="server" ChildrenAsTriggers="true" UpdateMode="Always">
        <ContentTemplate>


            <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
                runat="server" AssociatedUpdatePanelID="upReceiptEntry">
                <ProgressTemplate>
                    <div class="divWaiting">
                        <asp:Label ID="lblWait" runat="server"
                            Text="Please wait... " />
                        <asp:Image ID="imgWait" runat="server"
                            ImageAlign="Middle" ImageUrl="~/images/ajax-loader.gif" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>

            <div style="height: 22px; width: 100%; text-align: right; background-color: Silver;">
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
                            Add Fleet
                        </div>
                        <div class="panel-body paddingleft0 paddingright30">

                            <div class="row no-margin-left">

                                <div class="col-md-2 padding3">
                                    Registration No
                                </div>

                                <div class="col-md-2 padding5">
                                    <asp:TextBox ID="txtRegistrationNo" runat="server" TabIndex="1" CssClass="textbox" MaxLength="10" ToolTip="eg : AB-1234,ABC-1234" AutoPostBack="True" OnTextChanged="txtRegistrationNo_TextChanged"></asp:TextBox>
                                </div>

                                <div class="col-md-2 padding0 displayinlineblock">
                                   <asp:ImageButton ID="imgbtnveclecode" runat="server" ImageUrl="~/Images/icon_search.png"
                                        ImageAlign="Baseline" OnClick="imgbtnveclecode_Click" CssClass="imageicon"  />
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
                                    Brand
                                </div>
                                <div class="col-md-2 padding5">
                                    <asp:TextBox ID="txtBrand" runat="server" TabIndex="2" MaxLength="10" CssClass="textbox"></asp:TextBox>
                                </div>

                                <div class="col-md-2 padding3">
                                    Model
                                </div>
                                <div class="col-md-2 padding5">
                                    <asp:TextBox ID="txtModel" runat="server" TabIndex="3" MaxLength="10" CssClass="textbox"></asp:TextBox>
                                </div>

                                <div class="col-md-2 padding3">
                                    Type
                                </div>
                                <div class="col-md-2 padding5">
                                    <asp:DropDownList ID="ddlType" runat="server" TabIndex="4" CssClass="textbox ddlhight1" AutoPostBack="True">
                                    </asp:DropDownList>
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
                                    SIPP Code
                                </div>
                                <div class="col-md-2 padding5">
                                    <asp:TextBox ID="txtSIPPCode" runat="server" TabIndex="5" MaxLength="40" CssClass="textbox"></asp:TextBox>
                                </div>
                                <div class="col-md-2 padding3">
                                    Start Meter
                                </div>
                                <div class="col-md-2 padding5">
                                    <asp:TextBox ID="txtStartMeter" runat="server" TabIndex="6" CssClass="textbox" onkeydown="return jsDecimals(event);" MaxLength="9"></asp:TextBox>
                                </div>

                                <div class="col-md-2 padding3">
                                    Tour Selected Date
                                </div>
                                <div class="col-md-2 padding5">
                                    <div class="col-md-10 padding0 displayinlineblock">
                                        <asp:TextBox ID="txttourdate" runat="server"  CssClass="textbox" Format="dd/MMM/yyyy"
                                            onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txttourdate"
                                            PopupButtonID="imgbtnselectdate" Format="dd/MMM/yyyy">
                                        </asp:CalendarExtender>
                                    </div>
                                    <div class="col-md-2 padding0 displayinlineblock">
                                        <asp:ImageButton ID="imgbtnselectdate" runat="server" TabIndex="7" ImageUrl="~/Images/calendar.png"
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
                                    Owner
                                </div>
                                <div class="col-md-2 padding5">
                                    <asp:DropDownList ID="ddlOwner" runat="server" TabIndex="8" CssClass="textbox ddlhight1" MaxLength="20" AutoPostBack="True" OnTextChanged="ddlOwner_TextChanged">
                                        <asp:ListItem>Select</asp:ListItem>
                                        <asp:ListItem>Internal</asp:ListItem>
                                        <asp:ListItem>Kangaroo</asp:ListItem>
                                        <asp:ListItem>Europ</asp:ListItem>
                                        <asp:ListItem>External</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 padding3">
                                    Owner Name
                                </div>
                                <div class="col-md-2 padding5">
                                    <asp:TextBox ID="txtOwnerName" runat="server" CssClass="textbox" TabIndex="9" MaxLength="400"></asp:TextBox>
                                </div>

                                <div class="col-md-2 padding3">
                                    Owner Contact No
                                </div>
                                <div class="col-md-2 padding5">
                                    <asp:TextBox ID="txtOwnerContactNo" runat="server" TabIndex="10" MaxLength="10" CssClass="textbox" ToolTip="eg : 94700000000" onkeydown="return jsDecimals(event);"></asp:TextBox>
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
                                    Last Service Meter
                                </div>
                                <div class="col-md-2 padding5">
                                    <asp:TextBox ID="txtLastServiceMeter" MaxLength="9" TabIndex="11" runat="server" CssClass="textbox" onkeydown="return jsDecimals(event);"></asp:TextBox>
                                </div>
                                
                                <div class="col-md-2 padding3">
                                    Tourist Board Regno
                                </div>
                                <div class="col-md-2 padding5">
                                    <asp:TextBox ID="txtTouristBoardRegno" runat="server" TabIndex="12" MaxLength="10" CssClass="textbox"></asp:TextBox>
                                </div>

                                <div class="col-md-2 padding3">
                                    Is vehicle lease ?
                                </div>
                                <div class="col-md-2 padding5">
                                    <asp:RadioButtonList ID="rbtislease" runat="server" TabIndex="13"
                                        RepeatDirection="Horizontal" RepeatLayout="Flow" Font-Names="Arial Narrow" Font-Size="Small">
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
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
                                    Insurance Exp Date
                                </div>
                                <div class="col-md-2 padding5">
                                    <div class="col-md-10 padding0 displayinlineblock">
                                        <asp:TextBox ID="txtInsurenceExpDate" runat="server" CssClass="textbox" Format="dd/MMM/yyyy"
                                            onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtInsurenceExpDate"
                                            PopupButtonID="imgbtnInsurenceExpDate" Format="dd/MMM/yyyy">
                                        </asp:CalendarExtender>
                                    </div>
                                    <div class="col-md-2 padding0 displayinlineblock">
                                        <asp:ImageButton ID="imgbtnInsurenceExpDate" runat="server" TabIndex="14"  ImageUrl="~/Images/calendar.png"
                                            ImageAlign="Middle" CssClass="imageicon" />
                                    </div>
                                </div>


                                <div class="col-md-2 padding3">
                                    Revenue Exp Date
                                </div>
                                <div class="col-md-2 padding5">
                                    <div class="col-md-10 padding0 displayinlineblock">
                                        <asp:TextBox ID="txtRevenueExpDate" runat="server"  CssClass="textbox" Format="dd/MMM/yyyy"
                                            onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtRevenueExpDate"
                                            PopupButtonID="imgbtnRevenueExpDate" Format="dd/MMM/yyyy">
                                        </asp:CalendarExtender>
                                    </div>
                                    <div class="col-md-2 padding0 displayinlineblock">
                                        <asp:ImageButton ID="imgbtnRevenueExpDate" runat="server" TabIndex="15" ImageUrl="~/Images/calendar.png"
                                            ImageAlign="Middle" CssClass="imageicon" />
                                    </div>
                                </div>

                                <div class="col-md-2 padding3">
                                    Fuel Type
                                </div>
                                <div class="col-md-2 padding5">
                                    <asp:DropDownList ID="ddlFuelType" runat="server" TabIndex="16" CssClass="textbox ddlhight1" AutoPostBack="true">
                                        <asp:ListItem>Select</asp:ListItem>
                                        <asp:ListItem>Diesel</asp:ListItem>
                                        <asp:ListItem>Petrol</asp:ListItem>
                                    </asp:DropDownList>
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
                                        Engine Capacity
                                    </div>
                                    <div class="col-md-2 padding5">
                                        <asp:TextBox ID="txtengcapacity" runat="server" TabIndex="17" CssClass="textbox" MaxLength="10"></asp:TextBox>
                                    </div>

                                    <div class="col-md-2 padding3">
                                        Number of Seats
                                    </div>
                                    <div class="col-md-2 padding5">
                                        <asp:TextBox ID="txtnoofseats" runat="server" TabIndex="18" CssClass="textbox" MaxLength="3" onkeydown="return jsDecimals(event);"></asp:TextBox>
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
                                     Profit Center
                                 </div>
                                 <div class="col-md-2 padding5">
                                     <div style="height: 175px; overflow: scroll;">
                                         <asp:CheckBoxList ID="clstprofitcenter" runat="server" TabIndex="19" CssClass="textbox ddlhight1">
                                         </asp:CheckBoxList>
                                     </div>
                                 </div>

                                 <div class="col-md-2 padding3">
                                     Status
                                 </div>
                                 <div class="col-md-2 padding5">
                                     <asp:DropDownList ID="ddlStatus" runat="server" TabIndex="20" CssClass="textbox ddlhight1" AutoPostBack="true">
                                         <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                                         <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                         <asp:ListItem Text="Deactive" Value="0"></asp:ListItem>
                                     </asp:DropDownList>
                                 </div>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
