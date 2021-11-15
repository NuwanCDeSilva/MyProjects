<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="FleetMaster.aspx.cs" Inherits="FastForward.SCMWeb.View.MasterFiles.Fleet_Management.FleetMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>
        function DateValidFrom(sender, args) {

            var fromDate = Date.parse(document.getElementById('<%=txtRegDate.ClientID%>').value);
            var sysDate = Date.parse(document.getElementById('<%=hdfCurrDate.ClientID%>').value);
            // alert(sysDate);
            // alert(sysDate);
            if (sysDate < fromDate) {
                document.getElementById('<%=txtRegDate.ClientID%>').value = document.getElementById('<%=hdfCurrDate.ClientID%>').value;
                showStickyWarningToast('Please select a valid date !');
            };
        }
        function DateValidPur(sender, args) {

            var fromDate = Date.parse(document.getElementById('<%=txtPurDate.ClientID%>').value);
            var sysDate = Date.parse(document.getElementById('<%=hdfCurrDate.ClientID%>').value);
            // alert(sysDate);
            // alert(sysDate);
            if (sysDate < fromDate) {
                document.getElementById('<%=txtPurDate.ClientID%>').value = document.getElementById('<%=hdfCurrDate.ClientID%>').value;
                showStickyWarningToast('Please select a valid date !');
            };
        }
    </script>
    <script>
        function ConfSave() {
            var selectedvalueOrd = confirm("Do you want to save ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfClear() {
            var selectedvalueOrd = confirm("Are you sure do you want to clear ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
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
            padding-top: 0px;
            padding-bottom: 0px;
            margin-bottom: 0px;
            margin-top: 0px;
        }

        .panel-body {
            padding-top: 1px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="pnlMain">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="row">
        <div class="col-sm-12">
            <asp:HiddenField ID="hdfCurrDate" Value="" runat="server" />
            <asp:UpdatePanel runat="server" ID="pnlMain">
                <ContentTemplate>
                    <div class="col-sm-12">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-sm-10">
                                </div>
                                <div class="col-sm-2 paddingRight0 text-right">
                                    <div class="buttonRow" style="height: 30px; margin-top: -12px;">
                                        <div class="col-sm-2 padding0 text-center" style="width: 70px;">
                                            <asp:LinkButton OnClick="lbtnSave_Click" ID="lbtnSave" OnClientClick="return ConfSave();" CausesValidation="false" runat="server"
                                                CssClass=""> 
                                            <span class="glyphicon glyphicon-save" aria-hidden="true"></span>Save</asp:LinkButton>
                                        </div>

                                        <div class="col-sm-2 padding0 text-center" style="width: 65px;">
                                            <asp:LinkButton ID="lbtnClear" OnClick="lbtnClear_Click" CausesValidation="false" runat="server"
                                                OnClientClick="return ConfClear();" CssClass=""> 
                                            <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span>Clear </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <%-- New Design Start --%>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="panel panel-default">
                                    <div class="panel panel-heading">
                                        <strong><b>Vehicle Registration</b></strong>
                                    </div>
                                    <div class="panel panel-body padding0">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-sm-7 padding0">
                                                    <div class="panel panel-default">
                                                        <div class="panel panel-heading padding0">
                                                            <strong>General Information</strong>
                                                        </div>
                                                        <div class="panel panel-body padding0">
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-5">
                                                                        <div class="col-sm-5 labelText1 paddingLeft0">
                                                                            Registration #
                                                                        </div>
                                                                        <div class="col-sm-6 padding0">
                                                                            <asp:TextBox OnTextChanged="txtRegNo_TextChanged" AutoPostBack="true" runat="server" CssClass="form-control" ID="txtRegNo" />
                                                                        </div>
                                                                        <div class="col-sm-1 padding3">
                                                                            <asp:LinkButton OnClick="lbtnSeRegNo_Click" ID="lbtnSeRegNo" runat="server" Visible="true">
                                                                                <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-5">
                                                                        <div class="col-sm-5 labelText1">
                                                                            Registration Date
                                                                        </div>
                                                                        <div class="col-sm-4 padding0">
                                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtRegDate" Enabled="false" />
                                                                        </div>
                                                                        <div class="col-sm-1 padding3">
                                                                            <asp:LinkButton ID="lbtnRegDate" runat="server" CausesValidation="false">
                                                                        <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtRegDate" Animated="true"
                                                                                PopupButtonID="lbtnRegDate" Format="dd/MMM/yyyy" OnClientDateSelectionChanged="DateValidFrom">
                                                                            </asp:CalendarExtender>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-5">
                                                                        <div class="col-sm-5 labelText1 paddingLeft0">
                                                                            Manufacture Year
                                                                        </div>
                                                                        <div class="col-sm-4 padding0">
                                                                            <asp:TextBox ID="txtManYear" runat="server" CssClass="diWMClick validateInt form-control" />
                                                                        </div>
                                                                    </div>
                                                                   <div class="col-sm-5">
                                                                        <div class="col-sm-5 labelText1">
                                                                            Man. Ref
                                                                        </div>
                                                                        <div class="col-sm-7 padding0">
                                                                            <asp:TextBox ID="txtManRef" runat="server" CssClass="form-control" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-5">
                                                                        <div class="col-sm-5 labelText1 paddingLeft0">
                                                                            Make
                                                                        </div>
                                                                        <div class="col-sm-4 padding0">
                                                                            <asp:TextBox AutoPostBack="true" OnTextChanged="txtMake_TextChanged" ID="txtMake" runat="server" CssClass="form-control" />
                                                                        </div>
                                                                        <div class="col-sm-1 padding3">
                                                                            <asp:LinkButton ID="lbtnSEMake" runat="server" OnClick="lbtnSEMake_Click">
                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-5">
                                                                        <div class="col-sm-5 labelText1">
                                                                            Model
                                                                        </div>
                                                                        <div class="col-sm-6 padding0">
                                                                            <asp:TextBox ID="txtModel" AutoPostBack="true" OnTextChanged="txtModel_TextChanged" runat="server" CssClass="form-control" />
                                                                        </div>
                                                                        <div class="col-sm-1 padding3">
                                                                            <asp:LinkButton ID="lbtnSeModel" runat="server" OnClick="lbtnSeModel_Click">
                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-2 padding0">
                                                                        <div class="col-sm-3 labelText1">
                                                                            Color
                                                                        </div>
                                                                        <div class="col-sm-7 paddingRight0">
                                                                            <asp:TextBox ID="txtColor" AutoPostBack="true" OnTextChanged="txtColor_TextChanged" runat="server" CssClass="form-control" />
                                                                        </div>
                                                                        <div class="col-sm-1 padding3">
                                                                            <asp:LinkButton ID="lbtnSeColor" runat="server" OnClick="lbtnSeColor_Click">
                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    
                                                                    
                                                                    
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-5">
                                                                        <div class="col-sm-5 labelText1 paddingLeft0">
                                                                            Country
                                                                        </div>
                                                                        <div class="col-sm-4 padding0">
                                                                            <asp:TextBox AutoPostBack="true" OnTextChanged="txtCountry_TextChanged" ID="txtCountry" runat="server" CssClass="form-control" />
                                                                        </div>
                                                                        <div class="col-sm-1 padding3">
                                                                            <asp:LinkButton ID="lbtnSeCountry" runat="server" OnClick="lbtnSeCountry_Click">
                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-5">
                                                                        <div class="col-sm-5 labelText1">
                                                                            Remark
                                                                        </div>
                                                                        <div class="col-sm-7 padding0">
                                                                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-5">
                                                                        <div class="col-sm-5 labelText1 paddingLeft0">
                                                                            TAX Class
                                                                        </div>
                                                                        <div class="col-sm-4 padding0">
                                                                            <asp:TextBox OnTextChanged="txtTaxClass_TextChanged" AutoPostBack="true" ID="txtTaxClass" runat="server" CssClass="form-control" />
                                                                        </div>
                                                                        <div class="col-sm-1 padding3">
                                                                            <asp:LinkButton ID="lbtnSeTaxClass" runat="server" OnClick="lbtnSeTaxClass_Click">
                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-5">
                                                                        <div class="col-sm-5 labelText1">
                                                                            Class
                                                                        </div>
                                                                        <div class="col-sm-4 padding0">
                                                                            <asp:TextBox AutoPostBack="true" OnTextChanged="txtClass_TextChanged" ID="txtClass" runat="server" CssClass="form-control" />
                                                                        </div>
                                                                        <div class="col-sm-1 padding3">
                                                                            <asp:LinkButton ID="lbtnSeClass" runat="server" OnClick="lbtnSeClass_Click">
                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-2 padding0">
                                                                        <div class="col-sm-2" style="margin-top: 1px;">
                                                                            <asp:CheckBox ID="chkStus" Checked="true" Text="" runat="server" />
                                                                        </div>
                                                                        <div class="col-sm-8 labelText1">
                                                                            Active 
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>


                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-5">
                                                                        <div class="col-sm-5 paddingLeft0">
                                                                            Province
                                                                        </div>
                                                                        <div class="col-sm-4 padding0">
                                                                            <asp:TextBox AutoPostBack="true" OnTextChanged="txtProvince_TextChanged" ID="txtProvince" runat="server" CssClass="form-control" />
                                                                        </div>
                                                                        <div class="col-sm-1 padding3">
                                                                            <asp:LinkButton ID="lbtnSeProvince" runat="server" OnClick="lbtnSeProvince_Click">
                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-5">
                                                                        <div class="col-sm-12 ">
                                                                            <asp:LinkButton ID="lbtnUploadImg" runat="server" OnClick="lbtnUploadImg_Click">
                                                                                    <span class="glyphicon" aria-hidden="true"></span>Image
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-5 padding0">
                                                    <div class="panel panel-default">
                                                        <div class="panel panel-heading">
                                                            <strong>Technical Information</strong>
                                                        </div>
                                                        <div class="panel panel-body padding0">
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-6 padding0">
                                                                        <div class="col-sm-5 labelText1">
                                                                            Transmission Tp 
                                                                        </div>
                                                                        <div class="col-sm-5 padding0">
                                                                            <asp:DropDownList ID="ddlTransTp" CssClass="form-control" runat="server">
                                                                                <asp:ListItem Text="AUTOMATIC" Value="AUTOMATIC" />
                                                                                <asp:ListItem Text="MANUAL" Value="MANUAL" />
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6 padding0">
                                                                        <div class="col-sm-4 paddingRight0 labelText1">
                                                                            Fuel Type 
                                                                        </div>
                                                                        <div class="col-sm-5 padding0">
                                                                            <asp:TextBox AutoPostBack="true" OnTextChanged="txtFuelTp_TextChanged" ID="txtFuelTp" runat="server" CssClass="form-control" />
                                                                        </div>
                                                                        <div class="col-sm-1 padding3">
                                                                            <asp:LinkButton ID="lbtnSeFulTp" runat="server" OnClick="lbtnSeFulTp_Click">
                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-6 padding0">
                                                                        <div class="col-sm-5  labelText1">
                                                                            Fuel Tank Cap. (L)
                                                                        </div>
                                                                        <div class="col-sm-5 padding0">
                                                                            <asp:TextBox ID="txtFuelTankCapacity" runat="server" CssClass="diWMClick validateDecimal text-right form-control " />
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6 padding0">
                                                                        <div class="col-sm-4 labelText1">
                                                                            Chassis #
                                                                        </div>
                                                                        <div class="col-sm-8 padding0">
                                                                            <asp:TextBox ID="txtChasNo" runat="server" CssClass="form-control" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-6 padding0">
                                                                        <div class="col-sm-5 labelText1">
                                                                            Engine Capacity 
                                                                        </div>
                                                                        <div class="col-sm-6 padding0">
                                                                            <asp:TextBox ID="txtEngCapacity" runat="server" CssClass="form-control" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6 padding0">
                                                                        <div class="col-sm-4 labelText1">
                                                                            Engine #
                                                                        </div>
                                                                        <div class="col-sm-8 padding0">
                                                                            <asp:TextBox ID="txtEngNo" runat="server" CssClass="form-control" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                     <div class="col-sm-6 padding0">
                                                                        <div class="col-sm-5 labelText1">
                                                                            Carrier Tp 
                                                                        </div>
                                                                         <div class="col-sm-6  paddingLeft0">
                                                                             <asp:TextBox ID="txtCarryTp" AutoPostBack="true" OnTextChanged="txtCarryTp_TextChanged" runat="server" CssClass="form-control" />
                                                                         </div>
                                                                         <div class="col-sm-1 padding3">
                                                                             <asp:LinkButton ID="lbtnSeCarrTy" runat="server" OnClick="lbtnSeCarrTy_Click">
                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                             </asp:LinkButton>
                                                                         </div>
                                                                     </div>
                                                                    <div class="col-sm-6 padding0">
                                                                        <div class="col-sm-4 labelText1">
                                                                            Condition Tp
                                                                        </div>
                                                                        <div class="col-sm-7 paddingLeft0">
                                                                            <asp:DropDownList ID="dldCondTp" CssClass="form-control" runat="server">
                                                                                <asp:ListItem Value="Brand New" Text="Brand New" />
                                                                                <asp:ListItem Value="Used" Text="Used" />
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    
                                                                    
                                                                    
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-sm-3 padding0">
                                                    <div class="panel panel-default">
                                                        <div class="panel panel-heading padding0">
                                                            <strong>Physical Status </strong>
                                                        </div>
                                                        <div class="panel panel-body padding0">
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-4 labelText1">
                                                                        
                                                                    </div>
                                                                    <div class="col-sm-4 labelText1 text-center">
                                                                        Size (M)
                                                                    </div>
                                                                    <div class="col-sm-4 labelText1 text-center">
                                                                        Quantity
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-4 labelText1">
                                                                        Front Tyre
                                                                    </div>
                                                                    <div class="col-sm-4 labelText1">
                                                                        <asp:TextBox ID="txtFTSize" runat="server" CssClass="text-right form-control validateDecimal diWMClick" />
                                                                    </div>
                                                                    <div class="col-sm-4 labelText1">
                                                                        <asp:TextBox ID="txtFtQty" runat="server" CssClass="text-right validateInt form-control diWMClick" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-4 labelText1">
                                                                        Middle Tyre
                                                                    </div>
                                                                    <div class="col-sm-4 labelText1">
                                                                        <asp:TextBox ID="txtMTSize" runat="server" CssClass="text-right form-control validateDecimal diWMClick" />
                                                                    </div>
                                                                    <div class="col-sm-4 labelText1">
                                                                        <asp:TextBox ID="txtMtQty" runat="server" CssClass="text-right validateInt form-control diWMClick" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-4 labelText1">
                                                                        Rear Tyre
                                                                    </div>
                                                                    <div class="col-sm-4 labelText1">
                                                                        <asp:TextBox ID="txtRtSize" runat="server" CssClass="text-right form-control validateDecimal diWMClick" />
                                                                    </div>
                                                                    <div class="col-sm-4 labelText1">
                                                                        <asp:TextBox ID="txtRtQty" runat="server" CssClass=" text-right validateInt form-control diWMClick" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-4 labelText1">
                                                                        Body Type
                                                                    </div>
                                                                    <div class="col-sm-4 labelText1">
                                                                        <asp:DropDownList ID="ddlBodyTp" CssClass="form-control" runat="server">
                                                                            <asp:ListItem Text="CLOSED" />
                                                                            <asp:ListItem Text="OPEN" />
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-4 labelText1">
                                                                        Battery Type
                                                                    </div>
                                                                    <div class="col-sm-4 labelText1">
                                                                        <asp:TextBox ID="txtBattryTp" AutoPostBack="true" OnTextChanged="txtBattryTp_TextChanged" runat="server" CssClass="form-control" />
                                                                    </div>
                                                                    <div class="col-sm-1 padding3">
                                                                        <asp:LinkButton ID="lbtnSeBattrTp" runat="server" OnClick="lbtnSeBattrTp_Click">
                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-4 labelText1">
                                                                        No of Battery 
                                                                    </div>
                                                                    <div class="col-sm-4 labelText1">
                                                                        <asp:TextBox ID="txtNoOfBattry" MaxLength="2" runat="server" CssClass="diWMClick validateDecimal text-right form-control" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3 padding0">
                                                    <div class="panel panel-default">
                                                        <div class="panel panel-heading padding0">
                                                            <strong>Logistics </strong>
                                                        </div>
                                                        <div class="panel panel-body padding0">
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-5 padding0">
                                                                        <div class="col-sm-5 labelText1">
                                                                            Width
                                                                        </div>
                                                                        <div class="col-sm-7 paddingLeft0">
                                                                            <asp:TextBox ID="txtWidth" runat="server" CssClass="diWMClick validateDecimal text-right form-control" />
                                                                        </div>
                                                                    </div>
                                                                 
                                                                    <div class="col-sm-7 padding0">
                                                                        <div class="col-sm-8 labelText1">
                                                                            Unladen Weight
                                                                        </div>
                                                                        <div class="col-sm-4 padding0">
                                                                            <asp:TextBox ID="txtUnloadeWeight" runat="server" CssClass="diWMClick validateDecimal text-right form-control" />
                                                                        </div>
                                                                    </div>
                                                                    
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-5 padding0">
                                                                        <div class="col-sm-5 labelText1">
                                                                            Length 
                                                                        </div>
                                                                        <div class="col-sm-7 paddingLeft0">
                                                                            <asp:TextBox ID="txtLength" runat="server" CssClass="diWMClick validateDecimal text-right form-control" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-7 padding0">
                                                                        <div class="col-sm-8 labelText1">
                                                                            Gross Weight
                                                                        </div>
                                                                        <div class="col-sm-4 padding0">
                                                                            <asp:TextBox ID="txtGrosWeight" runat="server" CssClass="diWMClick validateDecimal text-right form-control" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-5 padding0">
                                                                        <div class="col-sm-5 labelText1">
                                                                            Height 
                                                                        </div>
                                                                        <div class="col-sm-7 paddingLeft0">
                                                                            <asp:TextBox ID="txtHeight" runat="server" CssClass="diWMClick validateDecimal text-right form-control" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-7 padding0">
                                                                        <div class="col-sm-8 labelText1">
                                                                            Maximum Weight
                                                                        </div>
                                                                        <div class="col-sm-4 padding0">
                                                                            <asp:TextBox ID="txtMaxWeight" runat="server" CssClass="diWMClick validateDecimal text-right form-control" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-5 padding0">
                                                                        <div class="col-sm-5 labelText1">
                                                                            UOM 
                                                                        </div>
                                                                        <div class="col-sm-7 paddingLeft0">
                                                                            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlUomCapacity">
                                                                                <asp:ListItem Value="M" Text="Meters" />
                                                                                <asp:ListItem Value="KM" Text="Km" />
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-7 padding0">
                                                                        <div class="col-sm-8 labelText1">
                                                                            UOM Weight
                                                                        </div>
                                                                        <div class="col-sm-4 paddingLeft0">
                                                                            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlUomWeight">
                                                                                  <asp:ListItem Value="KG" Text="Kg" />
                                                                                 <asp:ListItem Value="G" Text="Gram" />
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                
                                                <div class="col-sm-6 padding0">
                                                    <div class="panel panel-default">
                                                        <div class="panel panel-body padding0">
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-4 padding0">
                                                                        <div class="col-sm-5 labelText1">
                                                                            Company
                                                                        </div>
                                                                        <div class="col-sm-5 padding0">
                                                                            <asp:TextBox runat="server" OnTextChanged="txtCompany_TextChanged" AutoPostBack="true" Style="text-transform: uppercase" CssClass="form-control" ID="txtCompany" />
                                                                        </div>
                                                                        <div class="col-sm-1 padding3">
                                                                            <asp:LinkButton ID="lbtnSeCompany" runat="server" OnClick="lbtnSeCompany_Click">
                                                                                <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                     <div class="col-sm-4 padding0">
                                                                        <div class="col-sm-6 labelText1">
                                                                            Owner Type
                                                                        </div>
                                                                         <div class="col-sm-6 padding0">
                                                                             <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="ddlOwnerTp_SelectedIndexChanged" ID="ddlOwnerTp" runat="server" CssClass="form-control">
                                                                                 <asp:ListItem Value="OWN" Text="OWN" />
                                                                                 <asp:ListItem Value="OTHER" Text="OTHER" />
                                                                             </asp:DropDownList>
                                                                         </div>
                                                                    </div>
                                                                    <div class="col-sm-4 padding0">
                                                                        <div class="col-sm-5 labelText1">
                                                                            Owned By
                                                                        </div>
                                                                        <div class="col-sm-6 padding0">
                                                                            <asp:TextBox runat="server" OnTextChanged="txtOwnBy_TextChanged" AutoPostBack="true" Style="text-transform: uppercase" CssClass="form-control" ID="txtOwnBy" />
                                                                        </div>
                                                                        <div class="col-sm-1 padding3">
                                                                            <asp:LinkButton ID="lbtnSeOwnBy" runat="server" OnClick="lbtnSeOwnBy_Click">
                                                                                <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-4 padding0">
                                                                        <div class="col-sm-5 labelText1">
                                                                            Purchase Type
                                                                        </div>
                                                                        <div class="col-sm-6 padding0">
                                                                            <asp:DropDownList ID="ddlPurTp" runat="server" CssClass="form-control">
                                                                                <asp:ListItem Value="0" Text="--Select--" />
                                                                                <asp:ListItem Value="CASH" Text="CASH" />
                                                                                <asp:ListItem Value="CREDIT" Text="CREDIT" />
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-4 padding0">
                                                                        <div class="col-sm-6 labelText1 paddingRight0">
                                                                            Pur. Date
                                                                        </div>
                                                                        <div class="col-sm-6 padding0">
                                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtPurDate" Enabled="false" />
                                                                        </div>
                                                                        <div class="col-sm-1 padding3">
                                                                            <asp:LinkButton ID="btnPurDate" runat="server" CausesValidation="false">
                                                                                                <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtPurDate" Animated="true"
                                                                                PopupButtonID="btnPurDate" Format="dd/MMM/yyyy" OnClientDateSelectionChanged="DateValidPur">
                                                                            </asp:CalendarExtender>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-4 padding0">
                                                                        <div class="col-sm-6 labelText1 paddingRight0">
                                                                            <asp:Label Text="Purchase Price" ID="lblPurPrice" runat="server" />
                                                                        </div>
                                                                        <div class="col-sm-6 padding0">
                                                                            <asp:TextBox ID="txtPurPrice" runat="server" CssClass="diWMClick validateDecimal text-right form-control" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-4 padding0">
                                                                        <div class="col-sm-5 labelText1">
                                                                            Driver
                                                                        </div>
                                                                        <div class="col-sm-6 padding0">
                                                                            <asp:TextBox ID="txtDriver" AutoPostBack="true" OnTextChanged="txtDriver_TextChanged" MaxLength="30" runat="server" CssClass="form-control" />
                                                                        </div>
                                                                        <div class="col-sm-1 padding3">
                                                                            <asp:LinkButton ID="lbtnSeDriver" runat="server" OnClick="lbtnSeDriver_Click">
                                                                                <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-4 padding0">
                                                                        <div class="col-sm-3 paddingRight0 labelText1">
                                                                            Helper
                                                                        </div>
                                                                        <div class="col-sm-8 paddingRight0">
                                                                            <asp:TextBox ID="txtHelper" AutoPostBack="true" OnTextChanged="txtHelper_TextChanged" MaxLength="30" runat="server" CssClass="form-control" />
                                                                        </div>
                                                                        <div class="col-sm-1 padding3">
                                                                            <asp:LinkButton ID="lbtnSeHelper" runat="server" OnClick="lbtnSeHelper_Click">
                                                                                <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-4 padding0">
                                                                        <div class="col-sm-3 labelText1">
                                                                            Dealer
                                                                        </div>
                                                                        <div class="col-sm-8 paddingRight0">
                                                                            <asp:TextBox ID="txtDelerCd" AutoPostBack="true" OnTextChanged="txtDelerCd_TextChanged" runat="server" CssClass="form-control" />
                                                                        </div>
                                                                        <div class="col-sm-1 padding3">
                                                                            <asp:LinkButton ID="lbtnSeDeler" runat="server" OnClick="lbtnSeDeler_Click">
                                                                                <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <asp:Panel runat="server" Visible="false">
                                                                    <div class="col-sm-4 padding0">
                                                                        <div class="col-sm-4 labelText1 paddingLeft0">
                                                                            Party Code
                                                                        </div>
                                                                        <div class="col-sm-6 padding0">
                                                                            <asp:TextBox runat="server" Style="text-transform: uppercase" CssClass="form-control" ID="txtPartyCd" />
                                                                        </div>
                                                                        <div class="col-sm-1 padding3">
                                                                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lbtnSeCompany_Click" Visible="false">
                                                                                <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-4 padding0">
                                                                        <div class="col-sm-4 labelText1 paddingLeft0">
                                                                            Party Value
                                                                        </div>
                                                                        <div class="col-sm-6 padding0">
                                                                            <asp:TextBox runat="server" Style="text-transform: uppercase" CssClass="form-control" ID="txtPartyVal" />
                                                                        </div>
                                                                        <div class="col-sm-1 padding3">
                                                                            <asp:LinkButton ID="lbtnSePartyVal" runat="server" OnClick="lbtnSePartyVal_Click" Visible="false">
                                                                    <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </asp:Panel>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                   <div class="col-sm-4 padding0">
                                                                        <div class="col-sm-5 labelText1">
                                                                            <asp:Label ID="lblCost" Text="Cost" runat="server" />
                                                                        </div>
                                                                        <div class="col-sm-6 padding0">
                                                                            <asp:TextBox ID="txtCost" runat="server" CssClass="diWMClick validateDecimal text-right form-control" />
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
                        </div>
                        <%-- End  --%>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:UpdatePanel runat="server" ID="upSearch">
        <ContentTemplate>
            <asp:Button ID="btn1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupSearch" runat="server" Enabled="True" TargetControlID="btn1"
                PopupControlID="pnlSearch" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlSearch" DefaultButton="lbtnSearch">
        <div runat="server" id="test" class="panel panel-primary" style="padding: 5px;">
            <div class="panel panel-default" style="height: 350px; width: 700px;">
                <div class="panel-heading" style="height: 25px;">
                    <div class="col-sm-11"></div>
                    <div class="col-sm-1">
                        <asp:LinkButton ID="btnClose" runat="server" OnClick="btnClose_Click">
                             <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="row height16">
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="col-sm-4 labelText1">
                                Search by key
                            </div>
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy10" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-6 paddingRight5">
                                        <asp:DropDownList ID="ddlSerByKey" runat="server" class="form-control" AutoPostBack="false">
                                        </asp:DropDownList>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                        <div class="col-sm-6">
                            <div class="col-sm-4 labelText1">
                                Search by word
                            </div>
                            <div class="col-sm-6 paddingRight5">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtSerByKey" CausesValidation="false" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="lbtnSearch_Click"></asp:TextBox>
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
                                    <asp:AsyncPostBackTrigger ControlID="txtSerByKey" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12 height16">
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
                                            <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="12" />
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:UpdatePanel ID="UpdatePanel32" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button10" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="SbuPopup" runat="server" Enabled="True" TargetControlID="Button10"
                PopupControlID="pnlSBU" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel ID="pnlSBU" runat="server" align="center">
        <asp:Label ID="Label4" runat="server" Text="Label" Visible="false"></asp:Label>
        <asp:ScriptManagerProxy ID="ScriptManagerProxy17" runat="server"></asp:ScriptManagerProxy>
        <asp:UpdatePanel ID="UpdatePanel33" runat="server">
            <ContentTemplate>
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <span>Alert</span>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="lblMsg" Text="" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 height22">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                            <div class="col-sm-4">
                                </div>
                            <div class="col-sm-2">
                                <asp:Button ID="btnOk" runat="server" Text="Yes" CausesValidation="false" class="btn btn-primary" OnClick="btnOk_Click" />
                            </div>
                            <div class="col-sm-2">
                                <asp:Button ID="btnNo" runat="server" Text="No" CausesValidation="false" class="btn btn-primary" OnClick="btnNo_Click" />
                            </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popImgUpload" runat="server" Enabled="True" TargetControlID="Button1"
                PopupControlID="pnlImgUploder" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel ID="pnlImgUploder" runat="server" align="center">
        <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
        <div class="col-sm-12">
            <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-sm-10 text-left">
                                    Image Upload
                                </div>
                                <div class="col-sm-2 padding0">
                                   
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="panel-body">
                        <div class="row">
                            
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label Text="" ID="lblSuccMsg" ForeColor="Green" runat="server" />
                                <asp:Label Text="" ID="lblErrMsg" ForeColor="Red" runat="server" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-sm-3 labelText1">
                                    Image
                                </div>
                                <div class="col-sm-7 labelText1">
                                    <asp:FileUpload ID="FileUpload" runat="server" />
                                </div>
                                <div class="col-sm-2 buttonRow">
                                    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-sm-4">
                                </div>
                                <div class="col-sm-4">
                                    <div class="buttonRow">
                                        <asp:LinkButton ID="lbtnUpload" runat="server" OnClick="lbtnUpload_Click">
                                                                                <span class="glyphicon glyphicon-upload"  aria-hidden="true"></span>Upload
                                    </asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="buttonRow">
                                         <asp:LinkButton ID="lbtnClose" runat="server" OnClick="lbtnClose_Click">
                                                                                <span class="glyphicon glyphicon-remove-circle"  aria-hidden="true"></span>Close
                                    </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </div>
    </asp:Panel>

    <script type="text/javascript">
        Sys.Application.add_load(fun);
        function fun() {
            $('.validateDecimal').keypress(function (evt) {
                // var ch = (evt.which) ? evt.which : evt.keyCode;
                var ch = evt.which;
                var str = $(this).val();
               // console.log(ch);
                if (ch == 46) {
                    if (str.indexOf(".") == -1) {
                        return true;
                    } else {
                        return false;
                    }
                }
                else if ((ch == 8) || (ch == 9) || (ch == 46) || (ch == 0)) {
                    return true;
                }
                else if (ch > 47 && ch < 58) {
                    return true;
                }
                else {
                    return false;
                }
            });
            $('.diWMClick').mousedown(function (e) {
                if (e.button == 2) {
                    alert('This functionality is disabled !');
                    return false;
                } else {
                    return true;
                }
            });
            $('.validateInt').keypress(function (evt) {
                // var ch = (evt.which) ? evt.which : evt.keyCode;
                var charCode = evt.which;
                var str = $(this).val();
                //console.log(charCode);
                if ((charCode == 8) || (charCode == 9) || (charCode == 0) || (charCode == 13)) {
                    return true;
                }
                else if (str.length < 4) {
                    if (charCode > 47 && charCode < 58) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else {
                    $(this).value = str.substr(0, 4);
                    //alert(charCode);
                    alert('Maximum 4 characters are allowed ');
                    return false;
                }
            });
        }
    </script>
</asp:Content>
