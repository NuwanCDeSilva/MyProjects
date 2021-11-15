<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ChargeCodeDetails.aspx.cs" Inherits="FF.AbansTours.ChargeCodeDetails" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row height5 col-md-12">
        &nbsp;
    </div>
    <div class="row rowmargin0">
    </div>

    <div class="row rowmargin0 col-md-12">
        &nbsp;
    </div>
    <div class="row rowmargin0 col-md-12">
        <div class="panel panel-default">

            <div class="panel-body">
                <div class="row height5 col-md-12">
                </div>
                <div class="col-sm-9">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="col-md-2  padding1">
                                Charge Code Type
                            </div>
                            <div class="col-md-8 padding0 ">
                                <asp:DropDownList ID="ddlChargeType" runat="server" Width="200px" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlChargeType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-sm-3 rowmargin0">
                    <asp:UpdatePanel ID="upHedaerbtns" runat="server">
                        <ContentTemplate>
                            <div>
                                <asp:Button ID="btnCreate" Text="Save" CssClass="btn btn-success btn-xs " runat="server" Width="80px" OnClick="btnCreate_Click" />
                                <asp:Button ID="btnClear" Text="Clear" CssClass="btn btn-controls btn-xs " runat="server" Width="80px" OnClick="btnClear_Click" />
                                <asp:Button ID="btnBack" Visible="false" Text="Back" runat="server" Width="80px"
                                    OnClick="btnBack_Click" />
                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" TargetControlID="btnClear"
                                    ConfirmText="Do you want to clear?" ConfirmOnFormSubmit="false">
                                </asp:ConfirmButtonExtender>
                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender4" runat="server" TargetControlID="btnCreate"
                                    ConfirmText="Do you want to save?" ConfirmOnFormSubmit="false">
                                </asp:ConfirmButtonExtender>
                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" TargetControlID="btnBack"
                                    ConfirmText="Do you want go to previous page?" ConfirmOnFormSubmit="false">
                                </asp:ConfirmButtonExtender>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <div class="row rowmargin0 col-md-12">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="DefaultView" runat="server">
                        &nbsp;
                    </asp:View>
                    <asp:View ID="AriTravel" runat="server">
                        <asp:Panel ID="Panel1" runat="server" Width="100%">
                            <div class="panel panel-default">

                                <div class="panel-heading pannelheading">
                                    Air Travel
                                </div>
                                <div class="panel-body">
                                    <div class="row rowmargin0 col-md-12">
                                        <div class="col-md-6">
                                            <div class="col-md-4 padding2">
                                                Code
                                            </div>
                                            <div class="col-md-8 padding2 ">
                                                <asp:TextBox ID="txtCode" runat="server" AutoPostBack="true" OnTextChanged="txtCode_TextChanged"
                                                    Width="80%"></asp:TextBox>
                                                <asp:HiddenField ID="hfSAC_SEQ" runat="server" />
                                                <asp:ImageButton ID="btnCode" runat="server" ImageUrl="~/Images/icon_search.png"
                                                    ImageAlign="Middle" OnClick="btnCode_Click" />
                                                <asp:ImageButton ID="btnCodeLoad" runat="server" ImageUrl="~/Images/LoadDetails.png" ImageAlign="Middle"
                                                    OnClick="txtCode_TextChanged" ToolTip="Load details" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="col-md-4   padding1">
                                                Currency
                                            </div>
                                            <div class="col-md-8 padding2 ">
                                                <asp:DropDownList ID="ddlCurrency" runat="server" Width="80%">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row rowmargin0 col-md-12">
                                        <div class="col-md-6">
                                            <div class="col-md-4 padding1">
                                                Service Provider
                                            </div>
                                            <div class="col-md-8 padding2 ">
                                                <asp:DropDownList ID="ddlServiceProvider" runat="server" Width="80%">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="col-md-4   padding2">
                                                Rate
                                            </div>
                                            <div class="col-md-8 padding2 ">
                                                <asp:TextBox ID="txtRate" onkeydown="return jsDecimals(event);" CssClass="input-xlarge focused" runat="server" Width="80%"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row rowmargin0 col-md-12">
                                        <div class="col-md-6">
                                            <div class="col-md-4 padding1">
                                                Class
                                            </div>
                                            <div class="col-md-8 padding2">
                                                <asp:DropDownList ID="ddlClass" runat="server" Width="80%">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="col-md-4   padding2">
                                                Is Child
                                            </div>
                                            <div class="col-md-8 padding2 ">
                                                <asp:DropDownList ID="ddlIsChild" runat="server" Width="80%">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row rowmargin0 col-md-12">
                                        <div class="col-md-6">
                                            <div class="col-md-4   padding2">
                                                From Date
                                            </div>
                                            <div class="col-md-8 padding2 ">
                                                <asp:TextBox ID="txtFromDate" Enabled="false" CssClass="input-xlarge focused" runat="server"
                                                    Width="80%"></asp:TextBox>
                                                <asp:CalendarExtender ID="txtDateExtender" runat="server" Enabled="True" Format="dd/MMM/yyyy"
                                                    PopupButtonID="cal" TargetControlID="txtFromDate">
                                                </asp:CalendarExtender>
                                                <img alt="Calendar.." height="16" src="images/calendar.png" width="16" id="cal" style="cursor: pointer" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="col-md-4   padding2">
                                                Type
                                            </div>
                                            <div class="col-md-8 padding2 ">
                                                <asp:TextBox ID="txtType" CssClass="input-xlarge focused" runat="server" Width="80%"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row rowmargin0 col-md-12">
                                        <div class="col-md-6">
                                            <div class="col-md-4   padding2">
                                                To Date
                                            </div>
                                            <div class="col-md-8 padding2 ">
                                                <asp:TextBox ID="txtToDate" Enabled="false" CssClass="input-xlarge focused" runat="server"
                                                    Width="80%"></asp:TextBox>
                                                <asp:CalendarExtender ID="dtTo" runat="server" Enabled="True" Format="dd/MMM/yyyy"
                                                    PopupButtonID="cal2" TargetControlID="txtToDate">
                                                </asp:CalendarExtender>
                                                <img alt="Calendar.." height="16" src="images/calendar.png" width="16" id="cal2"
                                                    style="cursor: pointer" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="col-md-4   padding2">
                                                Ticket Details
                                            </div>
                                            <div class="col-md-8 padding2 ">
                                                <asp:TextBox ID="txtTicketDetails" CssClass="input-xlarge focused" runat="server"
                                                    TextMode="MultiLine" MaxLength="100" Width="80%"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row rowmargin0 col-md-12">
                                        <div class="col-md-6">
                                            <div class="col-md-4   padding2">
                                                From
                                            </div>
                                            <div class="col-md-8 padding2 ">
                                                <asp:TextBox ID="txtFrom" CssClass="input-xlarge focused" runat="server" Width="80%"></asp:TextBox>
                                                <asp:ImageButton ID="Imgbtnfrom" runat="server" ImageUrl="~/Images/icon_search.png"
                                                    ImageAlign="Middle" OnClick="Imgbtnfrom_Click" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="col-md-4   padding2">
                                                Additional Details
                                            </div>
                                            <div class="col-md-8 padding2 ">
                                                <asp:TextBox ID="txtAdditional" onkeydown="return jsDecimals(event);" CssClass="input-xlarge focused" runat="server" MaxLength="200"
                                                    TextMode="MultiLine" Width="80%"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row rowmargin0 col-md-12">
                                        <div class="col-md-6">
                                            <div class="col-md-4   padding2">
                                                To
                                            </div>
                                            <div class="col-md-8 padding2 ">
                                                <asp:TextBox ID="txtTo" CssClass="input-xlarge focused" runat="server" Width="80%"></asp:TextBox>
                                                <asp:ImageButton ID="ImgbtnTo" runat="server" ImageUrl="~/Images/icon_search.png"
                                                    ImageAlign="Middle" OnClick="ImgbtnTo_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row rowmargin0 col-md-12">
                                    </div>
                                    <div class="row rowmargin0 col-md-12">
                                    </div>
                                    <div class="row rowmargin0 col-md-12">
                                    </div>
                                </div>

                            </div>
                        </asp:Panel>
                    </asp:View>
                    <asp:View ID="Travel" runat="server">
                        <asp:Panel ID="Panel2" runat="server" Width="100%">
                            <div class="panel panel-default">
                                <div class="panel panel-default">
                                    <div class="panel-heading pannelheading">
                                        Travel
                                    </div>
                                    <div class="panel-body">
                                        <div class="row rowmargin0 col-md-12">
                                            <div class="col-md-6">
                                                <div class="col-md-4   padding2">
                                                    Code
                                                </div>
                                                <div class="col-md-8 padding2 ">
                                                    <asp:TextBox ID="txtCodeTvl" Width="80%" runat="server" AutoPostBack="true" OnTextChanged="txtCodeTvl_TextChanged"></asp:TextBox>
                                                    <asp:ImageButton ID="btnCodeTvl" runat="server" ImageUrl="~/Images/icon_search.png"
                                                        ImageAlign="Middle" OnClick="btnCodeTvl_Click" ToolTip="Search" />
                                                    <asp:HiddenField ID="hfTravlSeq" runat="server" />
                                                    <asp:ImageButton ID="btnLoadCodeTvl" runat="server" ImageUrl="~/Images/LoadDetails.png"
                                                        ImageAlign="Middle" OnClick="btnLoadCodeTvl_Click" ToolTip="Load details" />
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="col-md-4   padding2">
                                                    Vehicle Type
                                                </div>
                                                <div class="col-md-8 padding2 ">
                                                    <asp:TextBox ID="txtVehicalTVl" CssClass="input-xlarge focused" runat="server" Width="80%"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row rowmargin0 col-md-12">
                                            <div class="col-md-6">
                                                <div class="col-md-4   padding2">
                                                    Description
                                                </div>
                                                <div class="col-md-8 padding2 ">
                                                    <asp:TextBox ID="txtDescriptiobTvl" MaxLength="100" Width="80%" runat="server" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="col-md-4   padding2">
                                                    Rate Type
                                                </div>
                                                <div class="col-md-8 padding2 ">
                                                    <asp:TextBox ID="txtRateTypeTvl" CssClass="input-xlarge focused" runat="server" Width="80%"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row rowmargin0 col-md-12">
                                            <div class="col-md-6">
                                                <div class="col-md-4   padding2">
                                                    Service By
                                                </div>
                                                <div class="col-md-8 padding2 ">
                                                    <asp:DropDownList ID="ddlServiceByTvl" runat="server" Width="80%">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="col-md-4   padding2">
                                                    From Km
                                                </div>
                                                <div class="col-md-8 padding2 ">
                                                    <asp:TextBox ID="txtFromKmTvl" onkeydown="return jsDecimals(event);" CssClass="input-xlarge focused" runat="server" Width="80%"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row rowmargin0 col-md-12">
                                            <div class="col-md-6">
                                                <div class="col-md-4   padding2">
                                                    Class
                                                </div>
                                                <div class="col-md-8 padding2 ">
                                                    <asp:DropDownList ID="ddlClassTvl" runat="server" Width="80%">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="col-md-4   padding2">
                                                    TO Km
                                                </div>
                                                <div class="col-md-8 padding2 ">
                                                    <asp:TextBox ID="txtToKmTvl" onkeydown="return jsDecimals(event);" CssClass="input-xlarge focused" runat="server" Width="80%"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row rowmargin0 col-md-12">
                                            <div class="col-md-6">
                                                <div class="col-md-4   padding2">
                                                  Valid From 
                                                </div>
                                                <div class="col-md-8 padding2 ">
                                                    <asp:TextBox ID="txtFromDateTvl" Enabled="false" CssClass="input-xlarge focused"
                                                        runat="server" Width="80%"></asp:TextBox>
                                                    <asp:CalendarExtender ID="dtFromDateTvl" runat="server" Enabled="True" Format="dd/MMM/yyyy"
                                                        PopupButtonID="calTvl1" TargetControlID="txtFromDateTvl">
                                                    </asp:CalendarExtender>
                                                    <img alt="Calendar.." height="16" src="images/calendar.png" width="16" id="calTvl1"
                                                        style="cursor: pointer" />
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="col-md-4   padding2">
                                                    Rate
                                                </div>
                                                <div class="col-md-8 padding2 ">
                                                    <asp:TextBox ID="txtRateTvl" onkeydown="return jsDecimals(event);" CssClass="input-xlarge focused" runat="server" Width="80%"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row rowmargin0 col-md-12">
                                            <div class="col-md-6">
                                                <div class="col-md-4   padding2">
                                                  Valid To
                                                </div>
                                                <div class="col-md-8 padding2 ">
                                                    <asp:TextBox ID="txtTodateTvl" Enabled="false" CssClass="input-xlarge focused" runat="server"
                                                        Width="80%"></asp:TextBox>
                                                    <asp:CalendarExtender ID="dtTodateTvl" runat="server" Enabled="True" Format="dd/MMM/yyyy"
                                                        PopupButtonID="calTvl2" TargetControlID="txtTodateTvl">
                                                    </asp:CalendarExtender>
                                                    <img alt="Calendar.." height="16" src="images/calendar.png" width="16" id="calTvl2"
                                                        style="cursor: pointer" />
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="col-md-4   padding2">
                                                    Additional Rate
                                                </div>
                                                <div class="col-md-8 padding2 ">
                                                    <asp:TextBox ID="txtAdditionalRateTvl" onkeydown="return jsDecimals(event);" CssClass="input-xlarge focused" runat="server"
                                                        Width="80%"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row rowmargin0 col-md-12">
                                            <div class="col-md-6">
                                                <div class="col-md-4   padding2">
                                                    From
                                                </div>
                                                <div class="col-md-8 padding2 ">
                                                    <asp:TextBox ID="txtFromTvl" CssClass="input-xlarge focused" runat="server" Width="80%" ></asp:TextBox>
                                                    <asp:ImageButton ID="ImgbtnFromTvl" runat="server" ImageUrl="~/Images/icon_search.png"
                                                        ImageAlign="Middle" ToolTip="Load details" OnClick="ImgbtnFromTvl_Click" />
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="col-md-4   padding2">
                                                    Currency
                                                </div>
                                                <div class="col-md-8 padding2 ">
                                                    <asp:DropDownList ID="ddlCurrancyTvl" runat="server" Width="80%">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row rowmargin0 col-md-12">
                                            <div class="col-md-6">
                                                <div class="col-md-4   padding2">
                                                    To
                                                </div>
                                                <div class="col-md-8 padding2 ">
                                                    <asp:TextBox ID="txtToTvl" CssClass="input-xlarge focused" runat="server" Width="80%"></asp:TextBox>
                                                    <asp:ImageButton ID="ImgbtnToTvl" runat="server" ImageUrl="~/Images/icon_search.png"
                                                        ImageAlign="Middle" ToolTip="Load details" OnClick="ImgbtnToTvl_Click" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row rowmargin0 col-md-12">
                                        </div>
                                        <div class="row rowmargin0 col-md-12">
                                        </div>
                                        <div class="row rowmargin0 col-md-12">

                                            <div class="col-md-6">
                                                <div class="col-md-4   padding2">
                                                    &nbsp;
                                                </div>
                                                <div class="col-md-8 padding2 ">
                                                    &nbsp;
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </asp:View>
                    <asp:View ID="Miscellaneous" runat="server">
                        <asp:Panel ID="Panel3" runat="server" Width="100%">
                            <div class="panel panel-default">
                                <div class="panel panel-default">
                                    <asp:ScriptManagerProxy ID="ScriptManagerProxy10" runat="server"></asp:ScriptManagerProxy>
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <div class="panel-heading pannelheading">
                                                Miscellaneous
                                            </div>
                                            <div class="panel-body">
                                                <div class="row rowmargin0 col-md-12">
                                                    <div class="col-md-6">
                                                        <div class="col-md-4 textaling padding2">
                                                            Code
                                                        </div>
                                                        <div class="col-md-8 padding2 ">
                                                            <asp:HiddenField ID="hfMiscellaneous" runat="server" />
                                                            <asp:TextBox ID="txtCodeMis" Width="80%" runat="server" AutoPostBack="true" OnTextChanged="txtCodeMis_TextChanged"></asp:TextBox>
                                                            <asp:ImageButton ID="btnSearchCOdeMis" runat="server" ImageUrl="~/Images/icon_search.png"
                                                                ImageAlign="Middle" ToolTip="Search" OnClick="btnSearchCOdeMis_Click" />
                                                            <asp:ImageButton ID="btnLoadMis" runat="server" ImageUrl="~/Images/LoadDetails.png" ImageAlign="Middle"
                                                                ToolTip="Load details" OnClick="txtCodeMis_TextChanged" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="col-md-4 textaling padding2">
                                                            Description
                                                        </div>
                                                        <div class="col-md-8 padding2 ">
                                                            <asp:TextBox ID="txtDescriptionMis" MaxLength="100" Width="80%" runat="server" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row rowmargin0 col-md-12">
                                                    <div class="col-md-6">
                                                        <div class="col-md-4 textaling padding2">
                                                            Service Provider
                                                        </div>
                                                        <div class="col-md-8 padding2 ">
                                                            <asp:DropDownList ID="ddlServiceProviderMis" runat="server" Width="80%">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="col-md-4 textaling padding2">
                                                            Rate Type
                                                        </div>
                                                        <div class="col-md-8 padding2 ">
                                                            <asp:TextBox ID="txtRateTypeMis" Width="80%" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row rowmargin0 col-md-12">
                                                    <div class="col-md-6">
                                                        <div class="col-md-4 textaling padding2">
                                                         Valid From 
                                                        </div>
                                                        <div class="col-md-8 padding2 ">
                                                            <asp:TextBox ID="txtFromDateMis" Enabled="false" CssClass="input-xlarge focused"
                                                                runat="server" Width="80%"></asp:TextBox>
                                                            <asp:CalendarExtender ID="dtFromDateMis" runat="server" Enabled="True" Format="dd/MMM/yyyy"
                                                                PopupButtonID="calMis1" TargetControlID="txtFromDateMis">
                                                            </asp:CalendarExtender>
                                                            <img alt="Calendar.." height="16" src="images/calendar.png" width="16" id="calMis1"
                                                                style="cursor: pointer" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="col-md-4 textaling padding2">
                                                           Valid To 
                                                        </div>
                                                        <div class="col-md-8 padding2 ">
                                                            <asp:TextBox ID="txtToDateMis" Enabled="false" CssClass="input-xlarge focused" runat="server"
                                                                Width="80%"></asp:TextBox>
                                                            <asp:CalendarExtender ID="dtTodateMis" runat="server" Enabled="True" Format="dd/MMM/yyyy"
                                                                PopupButtonID="calMis2" TargetControlID="txtToDateMis">
                                                            </asp:CalendarExtender>
                                                            <img alt="Calendar.." height="16" src="images/calendar.png" width="16" id="calMis2"
                                                                style="cursor: pointer" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row rowmargin0 col-md-12">
                                                    <div class="col-md-6">
                                                        <div class="col-md-4 textaling padding2">
                                                            Currency
                                                        </div>
                                                        <div class="col-md-8 padding2 ">
                                                            <asp:DropDownList ID="ddlCurrancyMis" runat="server" Width="80%">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="col-md-4 textaling padding2">
                                                            Rate
                                                        </div>
                                                        <div class="col-md-8 padding2 ">
                                                            <asp:TextBox ID="txtRateMis" onkeydown="return jsDecimals(event);" Width="80%" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </div>
                            </div>
                        </asp:Panel>
                    </asp:View>
                </asp:MultiView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="row rowmargin0 col-md-12">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>

                <div class="panel-body  panelscollbar ">

                    <asp:GridView ID="grdresult" Visible="true" ShowHeader="true" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                        EmptyDataText="No data found..." AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="Service Provider">
                                <ItemTemplate>
                                    <asp:Label ID="col_sac_scv_by" runat="server" Text='<%# Bind("sac_scv_by") %>' Width="110px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Class">
                                <ItemTemplate>
                                    <asp:Label ID="col_sac_cls" runat="server" Text='<%# Bind("sac_cls") %>' Width="120px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Valid From">
                                <ItemTemplate>
                                    <asp:Label ID="col_sac_frm_dt" runat="server" Text='<%# Bind("sac_frm_dt", "{0:dd/MMM/yyyy}") %>' Width="80px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Valid To">
                                <ItemTemplate>
                                    <asp:Label ID="col_sac_to_dt" runat="server" Text='<%# Bind("sac_to_dt", "{0:dd/MMM/yyyy}") %>' Width="80px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="From">
                                <ItemTemplate>
                                    <asp:Label ID="col_sac_from" runat="server" Text='<%# Bind("sac_from") %>' Width="80px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="To">
                                <ItemTemplate>
                                    <asp:Label ID="col_sac_to" runat="server" Text='<%# Bind("sac_to") %>' Width="80px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Currency">
                                <ItemTemplate>
                                    <asp:Label ID="col_sac_cur" runat="server" Text='<%# Bind("sac_cur") %>' Width="80px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rate">
                                <ItemTemplate>
                                    <asp:Label ID="col_sac_rt" runat="server" Text='<%# Bind("sac_rt") %>' Width="80px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ticket Details">
                                <ItemTemplate>
                                    <asp:Label ID="col_sac_tic_desc" runat="server" Text='<%# Bind("sac_tic_desc") %>' Width="150px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Additional Details">
                                <ItemTemplate>
                                    <asp:Label ID="col_sac_add_desc" runat="server" Text='<%# Bind("sac_add_desc") %>' Width="150px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:GridView ID="grdlvlresult" Visible="false" ShowHeader="true" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                        EmptyDataText="No data found..." AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="Service Provider">
                                <ItemTemplate>
                                    <asp:Label ID="col_STC_SER_BY" runat="server" Text='<%# Bind("STC_SER_BY") %>' Width="110px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Class">
                                <ItemTemplate>
                                    <asp:Label ID="STC_CLS" runat="server" Text='<%# Bind("STC_CLS") %>' Width="120px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Valid From">
                                <ItemTemplate>
                                    <asp:Label ID="col_stc_frm_dt" runat="server" Text='<%# Bind("stc_frm_dt", "{0:dd/MMM/yyyy}") %>' Width="80px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Valid To">
                                <ItemTemplate>
                                    <asp:Label ID="col_stc_to_dt" runat="server" Text='<%# Bind("stc_to_dt", "{0:dd/MMM/yyyy}") %>' Width="80px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="From">
                                <ItemTemplate>
                                    <asp:Label ID="col_stc_frm" runat="server" Text='<%# Bind("stc_frm") %>' Width="80px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="To">
                                <ItemTemplate>
                                    <asp:Label ID="col_stc_to" runat="server" Text='<%# Bind("stc_to") %>' Width="80px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Currency">
                                <ItemTemplate>
                                    <asp:Label ID="col_stc_curr" runat="server" Text='<%# Bind("stc_curr") %>' Width="80px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rate">
                                <ItemTemplate>
                                    <asp:Label ID="col_stc_rt" runat="server" Text='<%# Bind("stc_rt") %>' Width="80px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>

                    <asp:GridView ID="grdMisreult" Visible="false" ShowHeader="true" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                        EmptyDataText="No data found..." AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="Service Provider">
                                <ItemTemplate>
                                    <asp:Label ID="col_STC_SER_BY" runat="server" Text='<%# Bind("ssm_ser_pro") %>' Width="110px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Valid From">
                                <ItemTemplate>
                                    <asp:Label ID="col_stc_frm_dt" runat="server" Text='<%# Bind("ssm_frm_dt", "{0:dd/MMM/yyyy}") %>' Width="80px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Valid To">
                                <ItemTemplate>
                                    <asp:Label ID="col_stc_to_dt" runat="server" Text='<%# Bind("ssm_to_dt", "{0:dd/MMM/yyyy}") %>' Width="80px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Currency">
                                <ItemTemplate>
                                    <asp:Label ID="col_ssm_cur" runat="server" Text='<%# Bind("ssm_cur") %>' Width="110px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rate">
                                <ItemTemplate>
                                    <asp:Label ID="col_ssm_rt" runat="server" Text='<%# Bind("ssm_rt") %>' Width="110px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>


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
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
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
                                        <asp:GridView ID="grdResultsearch" CausesValidation="false" runat="server" GridLines="None" CssClass="table table-hover" PageSize="7" PagerStyle-CssClass="cssPager" AllowPaging="True" OnSelectedIndexChanged="grdResult_SelectedIndexChanged" OnPageIndexChanging="grdResult_PageIndexChanging">
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
