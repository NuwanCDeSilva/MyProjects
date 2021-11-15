<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="DirectWarrantyAuthorization.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Inventory.WarrantyOperation.DirectWarrantyAuthorization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
    <script>
        function setScrollPosition(scrollValue) {

            $('#<%=hfScrollPosition.ClientID%>').val(scrollValue);
        }
        function maintainScrollPosition() {
            $('.dvScroll').scrollTop($('#<%=hfScrollPosition.ClientID%>').val());
            console.log($('#<%=hfScrollPosition.ClientID%>').val());
        }
        function ConfSave() {
            var selectedvalueOrd = confirm("Do you want to save ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfClear() {
            var selectedvalueOrd = confirm("Do you want to clear ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfPrint() {
            var selectedvalueOrd = confirm("Are you sure you want to give reprint authorization ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfOk() {
            var selectedvalueOrd = confirm("Are you sure ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfCancel() {
            var selectedvalueOrd = confirm("Do you want to cancel ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
    </script>
    <script>
        function showStickySuccessToast(value) {
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
            $().toastmessage('showToast', {
                text: value,
                sticky: true,
                position: 'top-center',
                type: 'success',
                closeText: '',
                close: function () {
                    console.log("toast is closed ...");
                }
            });
        }
        function showStickyNoticeToast(value) {
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
            $().toastmessage('showToast', {
                text: value,
                sticky: true,
                position: 'top-center',
                type: 'notice',
                closeText: '',
                close: function () { console.log("toast is closed ..."); }
            });
        }
        function showStickyWarningToast(value) {
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
            $().toastmessage('showToast', {
                text: value,
                sticky: true,
                position: 'top-center',
                type: 'warning',
                closeText: '',
                close: function () {
                    console.log("toast is closed ...");
                }

            });

        }
        function showStickyErrorToast(value) {
            if (jQuery('.toast-item-wrapper') != null) {
                jQuery('.toast-item-wrapper').remove();
            }
            $().toastmessage('showToast', {
                text: value,
                sticky: true,
                position: 'top-center',
                type: 'error',
                closeText: '',
                close: function () {
                    console.log("toast is closed ...");
                }
            });
        }
    </script>
    <style>
        .DatePanel {
            position: absolute;
            background-color: #FFFFFF;
            border: 1px solid #646464;
            color: #000000;
            z-index: 1;
            font-family: tahoma,verdana,helvetica;
            font-size: 11px;
            padding: 4px;
            text-align: center;
            cursor: default;
            line-height: 20px;
        }

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

        .panel {
            padding-top: 1px;
            padding-bottom: 0px;
            margin-bottom: 0px;
        }

        .panel-heading {
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="row">
                <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="upMain">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Label ID="lblWait" runat="server"
                                Text="Please wait... " />
                            <asp:Image ID="imgWait" runat="server"
                                ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
            <div class="row">
                <asp:UpdatePanel runat="server" ID="upMain">
                    <ContentTemplate>
                        <div class="col-sm-12">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-sm-7">
                                    </div>
                                    <div class="col-sm-5">
                                        <div class="row buttonRow">
                                            <div class="col-sm-3">
                                            </div>
                                            <div class="col-sm-3 padding0">
                                            </div>
                                            <div class="col-sm-2 padding0">
                                            </div>
                                            <div class="col-sm-2 padding0">
                                                <asp:LinkButton ID="lbtnPrint" CausesValidation="false" OnClick="lbtnPrint_Click" runat="server"
                                                    OnClientClick="return ConfPrint();" CssClass="floatRight"> 
                                            <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Approve </asp:LinkButton>
                                            </div>
                                            <div class="col-sm-2 paddingLeft0">
                                                <asp:LinkButton ID="lbtnClear" CausesValidation="false" runat="server" OnClick="lbtnClear_Click"
                                                    OnClientClick="return ConfClear();" CssClass="floatRight"> 
                                            <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="panel panel-default">
                                        <div class="panel panel-heading">
                                            <strong><b>Direct Warranty Print (Authorization)</b></strong>
                                        </div>
                                        <div class="panel panel-body padding0">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="panel panel-default">
                                                        <div class="panel panel-body padding0">
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-4 padding0">
                                                                        <div class="col-sm-2 labelText1">
                                                                            From Date
                                                                        </div>
                                                                        <div class="col-sm-2 padding0">
                                                                            <asp:TextBox ID="txtFrDate" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                                                onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFrDate"
                                                                                PopupButtonID="lbtnFrDate" Format="dd/MMM/yyyy">
                                                                            </asp:CalendarExtender>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft3">
                                                                            <asp:LinkButton ID="lbtnFrDate" CausesValidation="false" runat="server">
                                                                                    <span class="glyphicon glyphicon-calendar" aria-hidden="true"  ></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                        <div class="col-sm-2 labelText1">
                                                                            To Date
                                                                        </div>
                                                                        <div class="col-sm-2 padding0">
                                                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                                                onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate"
                                                                                PopupButtonID="lbtnToDate" Format="dd/MMM/yyyy">
                                                                            </asp:CalendarExtender>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft3">
                                                                            <asp:LinkButton ID="lbtnToDate" CausesValidation="false" runat="server">
                                                                                    <span class="glyphicon glyphicon-calendar" aria-hidden="true"  ></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-3">
                                                                        <div class="col-sm-3 labelText1">
                                                                            Location
                                                                        </div>
                                                                        <div class="col-sm-5 labelText1">
                                                                            <asp:TextBox runat="server" Style="text-transform: uppercase" ID="txtLocation" AutoPostBack="true" OnTextChanged="txtLocation_TextChanged" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft3 labelText1">
                                                                            <asp:LinkButton ID="lbtnSeLocation" OnClick="lbtnSeLocation_Click" CausesValidation="false" runat="server">
                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-2">
                                                                        <div class="col-sm-6 labelText1">
                                                                            Document Type
                                                                        </div>
                                                                        <div class="col-sm-4 labelText1 padding0">
                                                                            <asp:TextBox runat="server" ID="txtDocTp" Style="text-transform: uppercase" AutoPostBack="true" OnTextChanged="txtDocTp_TextChanged" CausesValidation="false" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1 paddingLeft3 labelText1">
                                                                            <asp:LinkButton ID="lbtnSeDocTp" OnClick="lbtnSeDocTp_Click" CausesValidation="false" runat="server">
                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                        <%--  <div class="col-sm-5 padding0 labelText1">
                                                                            <div class="col-sm-2 padding0  labelText1">
                                                                                <asp:CheckBox Text="" ID="chkAllDoc" runat="server" />
                                                                            </div>
                                                                            <div class="col-sm-9  labelText1 paddingLeft3">
                                                                                <asp:Label Text="All" runat="server" />
                                                                            </div>
                                                                        </div>--%>
                                                                    </div>
                                                                    <div class="col-sm-3">
                                                                        <div class="col-sm-4 labelText1">
                                                                            Doc No
                                                                        </div>
                                                                        <div class="col-sm-6 labelText1 padding0">
                                                                            <asp:TextBox runat="server" ID="txtDocNo" Style="text-transform: uppercase" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtDocNo_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                    </div>


                                                                </div>
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-4">
                                                                        <div class="col-sm-2 labelText1">
                                                                            Serial
                                                                        </div>
                                                                        <div class="col-sm-5 labelText1 padding0">
                                                                            <asp:TextBox runat="server" ID="txtSer" Style="text-transform: uppercase" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtSer_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-4">
                                                                        <div class="col-sm-4 labelText1">
                                                                            Warranty Card
                                                                        </div>
                                                                        <div class="col-sm-6 labelText1 padding0">
                                                                            <asp:TextBox runat="server" ID="txtWrnCrd" Style="text-transform: uppercase" AutoPostBack="true" CausesValidation="false" CssClass="form-control" OnTextChanged="txtSer_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-sm-2">
                                                                        <div class="row buttonRow">
                                                                            <asp:LinkButton ID="lbtnMainSer" OnClick="lbtnMainSer_Click" CausesValidation="false" runat="server">
                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <asp:Panel runat="server" Visible="false">

                                                                    <div class="col-sm-12">
                                                                        <div class="panel panel-default">
                                                                            <div class="panel panel-heading">
                                                                                <strong>Page Setup</strong>
                                                                            </div>
                                                                            <div class="panel panel-body">
                                                                                <div class="col-sm-7">
                                                                                    <div class="col-sm-2 padding0">
                                                                                        <div class="col-sm-1" style="margin-top: 2px;">
                                                                                            <asp:RadioButton ID="radHarfPage" Checked="true" GroupName="radPrint" Text="" runat="server" />
                                                                                        </div>
                                                                                        <div class="col-sm-9 labelText1 paddingLeft3">
                                                                                            <asp:Label Text="1/2 Page" runat="server" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-2 padding0">
                                                                                        <div class="col-sm-1" style="margin-top: 2px;">
                                                                                            <asp:RadioButton ID="radFullPage" GroupName="radPrint" Text="" runat="server" />
                                                                                        </div>
                                                                                        <div class="col-sm-9 labelText1 paddingLeft3">
                                                                                            <asp:Label Text="Full Page" runat="server" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-2 padding0">
                                                                                        <div class="col-sm-1" style="margin-top: 2px;">
                                                                                            <asp:RadioButton ID="rad4Inch" GroupName="radPrint" Text="" runat="server" />
                                                                                        </div>
                                                                                        <div class="col-sm-9 labelText1 paddingLeft3">
                                                                                            <asp:Label Text="4 Inches" runat="server" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-2 padding0">
                                                                                        <div class="col-sm-1" style="margin-top: 2px;">
                                                                                            <asp:RadioButton ID="rad8Inch" GroupName="radPrint" Text="" runat="server" />
                                                                                        </div>
                                                                                        <div class="col-sm-9 labelText1 paddingLeft3">
                                                                                            <asp:Label Text="8 Inches" runat="server" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-2 padding0">
                                                                                        <div class="col-sm-1" style="margin-top: 2px;">
                                                                                            <asp:RadioButton ID="radMobile" GroupName="radPrint" Text="" runat="server" />
                                                                                        </div>
                                                                                        <div class="col-sm-9 labelText1 paddingLeft3">
                                                                                            <asp:Label Text="Mobile" runat="server" />
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </asp:Panel>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="panel panel-default">
                                                                        <div class="panel panel-heading">
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-4">
                                                                                        <strong><b>Document Details</b></strong>
                                                                                    </div>
                                                                                    <asp:Panel runat="server" Visible="false">
                                                                                         <div class="col-sm-4">
                                                                                        <asp:Panel runat="server" ID="pnlRePrint">
                                                                                            <div class="col-sm-1">
                                                                                                <asp:CheckBox AutoPostBack="true"  OnCheckedChanged="chkRePrint_CheckedChanged" Checked="true" Text="" ID="chkRePrint" runat="server" />
                                                                                            </div>
                                                                                            <div class="col-sm-10 padding3">
                                                                                                <asp:Label Text="Re-Print" runat="server" />
                                                                                            </div>
                                                                                        </asp:Panel>
                                                                                    </div>
                                                                                    </asp:Panel>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="panel panel-body padding0">
                                                                            <div class="dvScroll" id="dvScroll" style="height: 150px; overflow: scroll;" onscroll="setScrollPosition(this.scrollTop);">
                                                                                <asp:GridView ID="dgvPenDoc" CssClass="table table-hover table-striped"
                                                                                    ShowHeaderWhenEmpty="true" EmptyDataText="No data found..."
                                                                                    runat="server" GridLines="None" PagerStyle-CssClass="cssPager"
                                                                                    AutoGenerateColumns="False">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label Text='' Width="2px" runat="server"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="2px" />
                                                                                            <HeaderStyle Width="2px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="">
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="lbtnSelectDoc" OnClick="lbtnSelectDoc_Click" CausesValidation="false" runat="server" Width="100%"> 
                                                                                    <span class="glyphicon glyphicon-arrow-down" aria-hidden="true"></span>
                                                                                                </asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="6px" />
                                                                                            <HeaderStyle Width="6px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Date">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblIth_doc_date" Width="100%" Text='<%# Bind("Ith_doc_date","{0:dd/MMM/yyyy}") %>' runat="server"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="50px" />
                                                                                            <HeaderStyle Width="50px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Document Type">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblIth_doc_tp" Width="100%" Text='<%# Bind("Ith_doc_tp") %>' runat="server"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="40px" />
                                                                                            <HeaderStyle Width="40px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Doc #">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblIth_doc_no" Width="100%" Text='<%# Bind("Ith_doc_no") %>' runat="server"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="120px" />
                                                                                            <HeaderStyle Width="120px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Manual Ref #">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblIth_manual_ref" Width="100%" Text='<%# Bind("Ith_manual_ref") %>' runat="server"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="100px" />
                                                                                            <HeaderStyle Width="100px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Other Loc">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblIth_oth_loc" Width="100%" Text='<%# Bind("Ith_oth_loc") %>' runat="server"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="40px" />
                                                                                            <HeaderStyle Width="40px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Entry #">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblIth_entry_no" Width="100%" Text='<%# Bind("Ith_entry_no") %>' runat="server"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="80px" />
                                                                                            <HeaderStyle Width="80px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Customer">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblIth_bus_entity" Width="100%" Text='<%# Bind("Ith_bus_entity") %>' runat="server"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="100px" />
                                                                                            <HeaderStyle Width="100px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Remarks">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblIth_remarks" Width="100%" Text='<%# Bind("Ith_remarks") %>' runat="server"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="120px" />
                                                                                            <HeaderStyle Width="120px" />
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <PagerStyle CssClass="cssPager"></PagerStyle>
                                                                                </asp:GridView>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="panel panel-default">
                                                                        <div class="panel panel-heading">
                                                                            <strong><b>Warranty Details</b></strong>
                                                                        </div>
                                                                        <div class="panel panel-body padding0">
                                                                            <div style="height: 200px; overflow: auto;">
                                                                                <asp:GridView ID="dgvDocDetails" CssClass="table table-hover table-striped"
                                                                                    ShowHeaderWhenEmpty="true" EmptyDataText="No data found..."
                                                                                    runat="server" GridLines="None" PagerStyle-CssClass="cssPager"
                                                                                    AutoGenerateColumns="False">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="">

                                                                                            <HeaderTemplate>
                                                                                                <asp:CheckBox ID="chkSerSelectAll" AutoPostBack="true" OnCheckedChanged="chkSerSelectAll_CheckedChanged" Text="" runat="server" />
                                                                                            </HeaderTemplate>
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="chkSerSelect" Text="" runat="server" />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="5px" />
                                                                                            <HeaderStyle Width="5px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Warranty #">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblIns_warr_no" Width="100%" Text='<%# Bind("Ins_warr_no") %>' runat="server"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="100px" />
                                                                                            <HeaderStyle Width="100px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Serial #">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblIns_ser_1" Width="100%" Text='<%# Bind("Ins_ser_1") %>' runat="server"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="100px" />
                                                                                            <HeaderStyle Width="100px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Item">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblIns_itm_cd" Width="100%" Text='<%# Bind("Ins_itm_cd") %>' runat="server"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="100px" />
                                                                                            <HeaderStyle Width="100px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Description">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbltmpItmDesc" Width="100%" Text='<%# Bind("tmpItmDesc") %>' runat="server"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="180px" />
                                                                                            <HeaderStyle Width="180px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Model">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbltmpItmModel" Width="100%" Text='<%# Bind("tmpItmModel") %>' runat="server"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="110px" />
                                                                                            <HeaderStyle Width="110px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Brand">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbltmpItmTp" Width="100%" Text='<%# Bind("tmpItmTp") %>' runat="server"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="60px" />
                                                                                            <HeaderStyle Width="60px" />
                                                                                        </asp:TemplateField>
                                                                                        <%-- <asp:TemplateField HeaderText="Customer">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblQty" Text='<%# Bind("tmpCustCd") %>' ToolTip='<%# Bind("tmpCustDesc") %>' runat="server"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>--%>
                                                                                        <asp:TemplateField HeaderText="" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblIns_ser_id" Text='<%# Bind("Ins_ser_id") %>' runat="server"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblIns_doc_no" Text='<%# Bind("Ins_doc_no") %>' runat="server"></asp:Label>
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
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="panel panel-default">
                                                                        <div class="panel panel-body padding0">
                                                                            <strong>
                                                                                <asp:Label ID="lblDefPrinter" Text="Default Printer : " runat="server" /></strong>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
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
    <asp:UpdatePanel runat="server" ID="update">
        <ContentTemplate>
            <asp:Button ID="btn1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="serPop" runat="server" Enabled="True" TargetControlID="btn1"
                PopupControlID="pnlSearch" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel runat="server" ID="pnlSearch" DefaultButton="lbtnSearch">
        <div runat="server" id="test" class="panel panel-primary Mheight" style="width: 700px;">
            <div class="panel panel-default" style="height: 370px;">
                <div class="panel-heading" style="height: 25px;">
                    <div class="col-sm-11"></div>
                    <div class="col-sm-1">
                        <asp:LinkButton ID="btnClose" runat="server" OnClick="btnClose_Click">
                             <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="col-sm-2 labelText1">
                                Search by key
                            </div>
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy10" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-3 paddingRight5">
                                        <asp:DropDownList ID="cmbSearchbykey" runat="server" class="form-control" AutoPostBack="false">
                                        </asp:DropDownList>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="col-sm-2 labelText1">
                                Search by word
                            </div>
                            <div class="col-sm-3 paddingRight5">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtSearchbyword" CausesValidation="false" class="form-control" AutoPostBack="true" OnTextChanged="txtSearchbyword_TextChanged" runat="server"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy9" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-1 paddingLeft0">
                                        <asp:LinkButton ID="lbtnSearch" runat="server" OnClick="lbtnSearch_Click">
                                     <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
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
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy8" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="dgvResult" CausesValidation="false" runat="server" AllowPaging="True"
                                        GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" ShowHeaderWhenEmpty="true"
                                        EmptyDataText="No data found..."
                                        OnPageIndexChanging="dgvResult_PageIndexChanging" OnSelectedIndexChanged="dgvResult_SelectedIndexChanged">
                                        <Columns>
                                            <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="10" />
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
    </asp:Panel>
    <script>

        Sys.Application.add_load(fun);

        function fun() {
            $(document).ready(function () {
                console.log('redy doc');
                console.log($('#<%=hfScrollPosition.ClientID%>').val());
                 maintainScrollPosition();
             });
         }

    </script>
</asp:Content>
