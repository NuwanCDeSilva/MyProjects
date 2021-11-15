<%@ Page Title="" Language="C#" MasterPageFile="~/View/AdminSite.Master" AutoEventWireup="true" CodeBehind="SalesForecasting.aspx.cs" Inherits="FastForward.SCMWeb.View.Transaction.Forecasting.SalesForecasting" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>
        function setScrollPosition(scrollValue) {
            $('#<%=hfScrollPosition.ClientID%>').val(scrollValue);
        }
        function maintainScrollPosition() {
            $('.divTar').scrollTop($('#<%=hfScrollPosition.ClientID%>').val());
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
            var selectedvalueOrd = confirm("Are you sure do you want to clear ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfClose() {
            var selectedvalueOrd = confirm("Are you sure do you want to close ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfDel() {
            var selectedvalueOrd = confirm("Are you sure do you want to delete and save ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        
        function ConfCreateCal() {
            var selectedvalueOrd = confirm("Do you want to create calendar ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfCreatePeriod() {
            var selectedvalueOrd = confirm("Do you want to create period ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfUploadFile() {
            var selectedvalueOrd = confirm("Do you want to upload file ?");
            if (selectedvalueOrd) {
                return true;
            } else {
                return false;
            }
        };
        function ConfCombine() {
            var selectedvalueOrd = confirm("Do you want to combine file ?");
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
            padding-top: 0px;
            padding-bottom: 0px;
            margin-bottom: 0px;
            margin-top: 0px;
        }
        /*.panel-default{
            padding-top: 0px;
            padding-bottom: 0px;
            margin-bottom: 0px;
            margin-top: 0px;
        }*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
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
            <asp:UpdatePanel runat="server" ID="upMain">
                <ContentTemplate>
                    <div class="col-sm-12">
                        <div class="row">
                            <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                            <asp:HiddenField ID="hdfSaveTp" Value="0" runat="server" />
                            <div class="col-sm-12">
                                <div class="col-sm-6">
                                </div>
                                <div class="col-sm-6 paddingRight0 text-right">
                                    <div class="buttonRow" style="height: 30px; margin-top: -12px;">
                                        <div class="col-sm-2 padding0 text-center" style="width:70px;">
                                            <asp:LinkButton OnClick="lbtnSave_Click" ID="lbtnSave" OnClientClick="return ConfSave();" CausesValidation="false" runat="server"
                                                CssClass=""> 
                                            <span class="glyphicon glyphicon-save" aria-hidden="true"></span>Save</asp:LinkButton>
                                        </div>
                                        <div class="col-sm-2 padding0 text-center" style="width:120px;">
                                            <asp:LinkButton ID="lbtnCreateCalender" OnClick="lbtnCreateCalender_Click" CausesValidation="false" runat="server" CssClass=""> 
                                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>Create Calendar</asp:LinkButton>
                                        </div>
                                        <div class="col-sm-2 padding0 text-center" style="width:120px;">
                                            <asp:LinkButton ID="lbtnCrePeriod" CausesValidation="false" runat="server" OnClick="lbtnCrePeriod_Click" CssClass=""> 
                                            <span class="glyphicon glyphicon-time" aria-hidden="true"></span>Create Period</asp:LinkButton>
                                        </div>

                                        <div class="col-sm-2 padding0 text-center" style="width:130px;">
                                            <asp:LinkButton ID="lbtnComCompany" OnClick="lbtnComCompany_Click" CausesValidation="false" runat="server" CssClass=""> 
                                            <span class="glyphicon glyphicon-saved" aria-hidden="true"></span>Combine Forcasting </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-2 padding0 text-center" style="width:110px;">
                                            <asp:LinkButton ID="lbtnUploadFile" OnClick="lbtnUploadFile_Click" CausesValidation="false" runat="server" CssClass=""> 
                                            <span class="glyphicon glyphicon-upload" aria-hidden="true"></span>Upload File</asp:LinkButton>
                                        </div>
                                        <div class="col-sm-2 padding0 text-center" style="width:65px;">
                                            <asp:LinkButton ID="lbtnClear" OnClick="lbtnClear_Click" CausesValidation="false" runat="server"
                                                OnClientClick="return ConfClear();" CssClass=""> 
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
                                        <strong><b>Sales Forecasts</b></strong>
                                    </div>
                                    <div class="panel panel-body padding0">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="">
                                                    <div class="">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-2 padding0">
                                                                    <div class="panel panel-default">
                                                                        <div class="panel panel-body padding0" style="height: 46px;">
                                                                            <div class="row" style="margin-top: 2px;">
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-4 paddingRight0 labelText1">
                                                                                        Company
                                                                                    </div>
                                                                                    <div class="col-sm-5 padding0">
                                                                                        <asp:TextBox runat="server" AutoPostBack="true" Style="text-transform: uppercase" ID="txtCompany" OnTextChanged="txtCompany_TextChanged" CssClass="form-control" />
                                                                                    </div>
                                                                                    <div class="col-sm-1 padding3">
                                                                                        <asp:LinkButton ID="lbtnSeCompany" CausesValidation="false" runat="server" OnClick="lbtnSeCompany_Click">
                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="row" style="margin-top: 2px;">
                                                                                <div class="col-sm-1"></div>
                                                                                <div class="col-sm-10">
                                                                                    <asp:Label ID="lblCompany" ForeColor="DarkBlue" Text="" runat="server" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-5 padding0">
                                                                    <div class="panel panel-default">
                                                                        <div class="panel panel-body padding0">
                                                                            <div class="row" style="margin-top: 2px;">
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-7 paddingRight0">
                                                                                        <div class="row">
                                                                                            <div class="col-sm-12">
                                                                                                <div class="col-sm-2 padding0 labelText1">
                                                                                                    Calendar
                                                                                                </div>
                                                                                                <div class="col-sm-8 paddingRight0">
                                                                                                    <asp:TextBox ID="txtCalenderCode" AutoPostBack="true" OnTextChanged="txtCalenderCode_TextChanged" runat="server" Style="text-transform: uppercase" CssClass="form-control" />
                                                                                                </div>
                                                                                                <div class="col-sm-1 padding3">
                                                                                                    <asp:LinkButton ID="lbtnSeCalendar" OnClick="lbtnSeCalendar_Click" CausesValidation="false" runat="server">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                    </asp:LinkButton>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="col-sm-12">
                                                                                                <div class="col-sm-2 padding0 labelText1">
                                                                                                    From
                                                                                                </div>
                                                                                                <div class="col-sm-4 paddingRight0">
                                                                                                    <asp:TextBox ID="txtCalFrom" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                                                                        onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                                                                </div>
                                                                                                <div class="col-sm-1 labelText1">
                                                                                                    To
                                                                                                </div>
                                                                                                <div class="col-sm-4 paddingRight0">
                                                                                                    <asp:TextBox ID="txtCalTo" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                                                                        onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-5 labelText1 padding0">
                                                                                        <asp:Label Text="" ForeColor="DarkBlue" ID="lblCalCode" runat="server" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-5 padding0">
                                                                    <div class="panel panel-default">
                                                                        <div class="panel panel-body padding0">
                                                                            <div class="row" style="margin-top: 2px;">
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-7 paddingRight0">
                                                                                        <div class="row">
                                                                                            <div class="col-sm-12">
                                                                                                <div class="col-sm-2 padding0 labelText1">
                                                                                                    Period
                                                                                                </div>
                                                                                                <div class="col-sm-8 paddingRight0">
                                                                                                    <asp:TextBox ID="txtPeriod" AutoPostBack="true" OnTextChanged="txtPeriod_TextChanged" runat="server" Style="text-transform: uppercase" CssClass="form-control" />
                                                                                                </div>
                                                                                                <div class="col-sm-1 padding3">
                                                                                                    <asp:LinkButton ID="lbtnSePeriod" OnClick="lbtnSePeriod_Click" CausesValidation="false" runat="server">
                                                                                                    <span class="glyphicon glyphicon-search" aria-hidden="true"  ></span>
                                                                                                    </asp:LinkButton>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="col-sm-12">
                                                                                                <div class="col-sm-2 padding0 labelText1">
                                                                                                    From
                                                                                                </div>
                                                                                                <div class="col-sm-4 paddingRight0">
                                                                                                    <asp:TextBox ID="txtPerFrom" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                                                                        onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                                                                </div>
                                                                                                <div class="col-sm-1 labelText1">
                                                                                                    To
                                                                                                </div>
                                                                                                <div class="col-sm-4 paddingRight0">
                                                                                                    <asp:TextBox ID="txtPerTo" runat="server" CssClass="form-control" Format="dd/MMM/yyyy"
                                                                                                        onkeypress="return RestrictSpace()" Enabled="false"></asp:TextBox>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-5 labelText1 padding0">
                                                                                        <asp:Label Text="" ForeColor="DarkBlue" ID="lblPer" runat="server" />
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
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-6 padding0">
                                                            <div class="panel panel-default">
                                                                <div class="panel panel-body padding0">
                                                                    <div class="row height5">
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-12">
                                                                            <div class="col-sm-4 ">
                                                                                <div class="col-sm-3 padding0 labelText1">
                                                                                    Define By
                                                                                </div>
                                                                                <div class="col-sm-9">
                                                                                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="ddlDefTp_SelectedIndexChanged" CssClass="form-control" runat="server" ID="ddlDefTp">
                                                                                        <asp:ListItem Value="0" Text="--Select--" />
                                                                                        <asp:ListItem Value="COM" Text="Company" />
                                                                                        <asp:ListItem Value="CHNL" Text="Channel" />
                                                                                        <asp:ListItem Value="SCHNL" Text="Sub Channel" />
                                                                                        <asp:ListItem Value="AREA" Text="Area" />
                                                                                        <asp:ListItem Value="REGION" Text="Region" />
                                                                                        <asp:ListItem Value="ZONE" Text="Zone" />
                                                                                        <asp:ListItem Value="PC" Text="Profit Center" />
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-2 paddingRight0">
                                                                                <div class="col-sm-5 padding0 labelText1">
                                                                                    Code
                                                                                </div>
                                                                                <div class="col-sm-7 padding0">
                                                                                    <asp:TextBox AutoPostBack="true" Style="text-transform: uppercase" OnTextChanged="txtDefCode_TextChanged" ID="txtDefCode" CssClass="form-control" runat="server" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-1 padding3">
                                                                                <asp:LinkButton ID="lbtnSeDefine" runat="server" OnClick="lbtnSeDefine_Click">
                                                                    <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                            <div class="col-sm-4 labelText1 padding3">
                                                                                <asp:Label Text="" ForeColor="DarkBlue" ID="lblDefCdDes" runat="server" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-6 padding0">
                                                            <div class="panel panel-default">
                                                                <div class="panel panel-body padding0">
                                                                    <div class="row height5">
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-12">
                                                                            <div class="col-sm-6">
                                                                                <div class="col-sm-4 padding0 labelText1">
                                                                                    Category Define On
                                                                                </div>
                                                                                <div class="col-sm-8">
                                                                                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="ddlCatDefOn_SelectedIndexChanged" CssClass="form-control" runat="server" ID="ddlCatDefOn">
                                                                                        <asp:ListItem Value="0" Text="--Select--" />
                                                                                        <asp:ListItem Value="CAT1" Text="Main Category" />
                                                                                        <asp:ListItem Value="CAT2" Text="Sub Category" />
                                                                                        <asp:ListItem Value="CAT3" Text="Category 3" />
                                                                                        <asp:ListItem Value="MODEL" Text="Model" />
                                                                                        <asp:ListItem Value="BRAND" Text="Brand" />
                                                                                        <asp:ListItem Value="ITEM" Text="Item" />
                                                                                        <asp:ListItem Value="CAT1_MODEL" Text="Main Category + Model" />
                                                                                        <asp:ListItem Value="MODEL_BRAND" Text="Brand + Model" />
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-sm-6 labelText1">
                                                                                <div class="col-sm-1">
                                                                                    <asp:CheckBox Checked="true"  Text="" ID="chkActTarg" runat="server" />
                                                                                </div>
                                                                                <div class="col-sm-10 padding3">
                                                                                    <asp:Label Text="Active" runat="server" />
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
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="panel panel-default">
                                                    <div class="panel panel-body padding0">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-9 padding0">
                                                                    <div class="col-sm-2 labelText1 padding0">
                                                                        Item
                                                                    </div>
                                                                    <div class="col-sm-2 labelText1 padding0">
                                                                        Brand
                                                                    </div>
                                                                    <div class="col-sm-2 labelText1 padding0">
                                                                        Model
                                                                    </div>
                                                                    <div class="col-sm-2 labelText1 padding0">
                                                                        Main Category
                                                                    </div>
                                                                    <div class="col-sm-2 labelText1 padding0">
                                                                        Sub Category
                                                                    </div>
                                                                    <div class="col-sm-2 labelText1 padding0">
                                                                        Category 3
                                                                    </div>

                                                                </div>
                                                                <div class="col-sm-3 padding0">
                                                                    <div class="col-sm-11 padding0">
                                                                        <div class="col-sm-4 padding3">
                                                                            Quantity
                                                                        </div>
                                                                        <div class="col-sm-5 padding3">
                                                                            Value
                                                                        </div>
                                                                        <div class="col-sm-3 padding0">
                                                                            GP %
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-9 padding0">
                                                                    <div class="col-sm-2 padding0">
                                                                        <div class="col-sm-11 padding03">
                                                                            <asp:TextBox ID="txtItemCode" Style="text-transform: uppercase" AutoPostBack="true" OnTextChanged="txtItemCode_TextChanged" CssClass="form-control" runat="server" />
                                                                        </div>
                                                                        <div class="col-sm-1 padding3">
                                                                            <asp:LinkButton ID="lbtnSeItem" runat="server" OnClick="lbtnSeItem_Click">
                                                                            <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-2 padding0">
                                                                        <div class="col-sm-11 padding03">
                                                                            <asp:TextBox ID="txtBrand" Style="text-transform: uppercase" AutoPostBack="true" OnTextChanged="txtBrand_TextChanged" CssClass="form-control" runat="server" />
                                                                        </div>
                                                                        <div class="col-sm-1 padding3">
                                                                            <asp:LinkButton ID="lbtnSeBrand" runat="server" OnClick="lbtnSeBrand_Click">
                                                                            <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-2 padding0">
                                                                        <div class="col-sm-11 padding03">
                                                                            <asp:TextBox ID="txtModel" Style="text-transform: uppercase" AutoPostBack="true" OnTextChanged="txtModel_TextChanged" CssClass="form-control" runat="server" />
                                                                        </div>
                                                                        <div class="col-sm-1 padding3">
                                                                            <asp:LinkButton ID="lbtnSeModel" runat="server" OnClick="lbtnSeModel_Click">
                                                                            <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-2 padding0">
                                                                        <div class="col-sm-11 padding03">
                                                                            <asp:TextBox ID="txtCat1" Style="text-transform: uppercase" AutoPostBack="true" OnTextChanged="txtCat1_TextChanged" CssClass="form-control" runat="server" />
                                                                        </div>
                                                                        <div class="col-sm-1 padding3">
                                                                            <asp:LinkButton ID="lbtnSeCat1" runat="server" OnClick="lbtnSeCat1_Click">
                                                                            <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-2 padding0">
                                                                        <div class="col-sm-11 padding03">
                                                                            <asp:TextBox ID="txtCat2" Style="text-transform: uppercase" AutoPostBack="true" OnTextChanged="txtCat2_TextChanged" CssClass="form-control" runat="server" />
                                                                        </div>
                                                                        <div class="col-sm-1 padding3">
                                                                            <asp:LinkButton ID="lbtnSeCat2" runat="server" OnClick="lbtnSeCat2_Click">
                                                                            <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-2 padding0">
                                                                        <div class="col-sm-11 padding03">
                                                                            <asp:TextBox ID="txtCat3" Style="text-transform: uppercase" AutoPostBack="true" OnTextChanged="txtCat3_TextChanged" CssClass="form-control" runat="server" />
                                                                        </div>
                                                                        <div class="col-sm-1 padding3">
                                                                            <asp:LinkButton ID="lbtnSeCat3" runat="server" OnClick="lbtnSeCat3_Click">
                                                                            <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3 padding0">
                                                                    <div class="col-sm-11 padding0">
                                                                        <div class="col-sm-4 padding3">
                                                                            <asp:TextBox ID="txtQty" CssClass="txtQty form-control text-right" runat="server" />
                                                                        </div>
                                                                        <div class="col-sm-5 padding3">
                                                                            <asp:TextBox ID="txtVal" CssClass="txtVal form-control text-right" runat="server" />
                                                                        </div>
                                                                        <div class="col-sm-3 paddingLeft0">
                                                                            <asp:TextBox ID="txtGp" CssClass="txtGp form-control text-right" runat="server" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-1 padding3">
                                                                        <div style="margin-top: -3px;">
                                                                            <asp:LinkButton ID="lbtnAddTarItem" runat="server" OnClick="lbtnAddTarItem_Click">
                                                                            <span class="glyphicon glyphicon-arrow-down" style="font-size:20px;"  aria-hidden="true"></span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="margin-bottom: 3px;">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12 ">
                                                <div class="panel panel-default">
                                                    <div class="panel panel-heading">
                                                        <strong>Target Details</strong>
                                                    </div>
                                                    <div class="panel panel-body padding0">
                                                        <div class="divTar" id="divTar" onscroll="setScrollPosition(this.scrollTop);" style="height: 350px; overflow-y: auto;">
                                                            <asp:GridView ID="dgvTarget" CssClass="table table-hover table-striped" runat="server" GridLines="None"
                                                                ShowHeaderWhenEmpty="True"
                                                                EmptyDataText="No data found..." AutoGenerateColumns="False" AllowPaging="false"
                                                                OnPageIndexChanging="dgvCCBCompany_PageIndexChanging"
                                                                PagerStyle-CssClass="cssPager">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Define By">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSfhDefBy" Text='<%# Bind("tmp_def_by") %>' runat="server" Width="100%" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="80px" />
                                                                        <HeaderStyle Width="80px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Define Cd">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSfhDefCd" Text='<%# Bind("tmp_def_cd") %>' runat="server" Width="100%" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="80px" />
                                                                        <HeaderStyle Width="80px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Item">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSfd_itm" Text='<%# Bind("Sfd_itm") %>' runat="server" Width="100%" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="80px" />
                                                                        <HeaderStyle Width="80px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Description">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbltmp_itm_desc" Text='<%# Bind("tmp_itm_desc") %>' runat="server" Width="100%" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="150px" />
                                                                        <HeaderStyle Width="150px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Brand">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSfd_brnd" Text='<%# Bind("Sfd_brnd") %>' runat="server" Width="100%" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="80px" />
                                                                        <HeaderStyle Width="80px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Model">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSfd_model" Text='<%# Bind("Sfd_model") %>' runat="server" Width="100%" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="80px" />
                                                                        <HeaderStyle Width="80px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Main Category">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSfd_cat1" Text='<%# Bind("Sfd_cat1") %>' runat="server" Width="100%" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="80px" />
                                                                        <HeaderStyle Width="80px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Sub Category">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSfd_cat2" Text='<%# Bind("Sfd_cat2") %>' runat="server" Width="100%" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="80px" />
                                                                        <HeaderStyle Width="80px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Category 3">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSfd_cat3" Text='<%# Bind("Sfd_cat3") %>' runat="server" Width="100%" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="80px" />
                                                                        <HeaderStyle Width="80px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Quantity">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSfd_qty" Text='<%# Bind("Sfd_qty","{0:N2}") %>' runat="server" Width="100%" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="80px" CssClass="gridHeaderAlignRight" />
                                                                        <HeaderStyle Width="80px" CssClass="gridHeaderAlignRight" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Value">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSfd_val" Text='<%# Bind("Sfd_val","{0:N2}") %>' runat="server" Width="100%" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="80px" CssClass="gridHeaderAlignRight" />
                                                                        <HeaderStyle Width="80px" CssClass="gridHeaderAlignRight" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="GP %">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSfd_gp" Text='<%# Bind("Sfd_gp","{0:N2}") %>' runat="server" Width="100%" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="50px" CssClass="gridHeaderAlignRight" />
                                                                        <HeaderStyle Width="50px" CssClass="gridHeaderAlignRight" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="">
                                                                        <ItemTemplate>
                                                                            <div style="margin-top: -3px;">
                                                                                <asp:LinkButton ID="lbtnTarDelete" OnClientClick="return ConfDel();" Width="100%" runat="server" OnClick="lbtnTarDelete_Click">
                                                                                <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                                                                </asp:LinkButton>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="10px" />
                                                                        <HeaderStyle Width="10px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Line No" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSfd_line" Text='<%# Bind("Sfd_line") %>' runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="def On" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbltmp_def_on" Text='<%# Bind("tmp_def_on") %>' runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="def On" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSfd_seq" Text='<%# Bind("Sfd_seq") %>' runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
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


    <%-- pnl create calender --%>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <asp:Button ID="btn2" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupCreCal" runat="server" Enabled="True" TargetControlID="btn2"
                PopupControlID="pnlCreCal" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress4" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait13" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait13" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="UpdatePanel3">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlCreCal">
                <div runat="server" id="Div1" class="panel panel-primary" style="padding: 5px;">
                    <div class="panel panel-default" style="height: 180px; width: 700px;">
                        <div class="panel-heading" style="height: 25px;">
                            <div class="col-sm-11 padding0">
                                <strong>Create Calendar</strong>
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel panel-body padding0">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="row buttonRow">
                                                <div class="col-sm-12">
                                                    <div class="col-sm-7">
                                                    </div>

                                                    <div class="col-sm-5">
                                                        <div class="col-sm-4">
                                                            <asp:LinkButton ID="lbtnCCSave" OnClientClick="return ConfSave()" runat="server" OnClick="lbtnCCSave_Click">
                                                      <span class="glyphicon glyphicon-save"  aria-hidden="true"></span>Save
                                                            </asp:LinkButton>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <asp:LinkButton ID="lbtnCCClear" OnClientClick="return ConfClear();" runat="server" OnClick="lbtnCCClear_Click">
                                                      <span class="glyphicon glyphicon-refresh"   aria-hidden="true"></span>Clear
                                                            </asp:LinkButton>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <asp:LinkButton ID="btnCreCalClose"  runat="server" OnClick="btnCreCalClose_Click">
                                                    <span class="glyphicon glyphicon-remove"  aria-hidden="true"></span>Close
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="panel panel-default">
                                                        <div class="panel panel-body padding0">
                                                            <div class="row">
                                                                <div class="col-sm-4">
                                                                    <div class="col-sm-5 labelText1">
                                                                        Company
                                                                    </div>
                                                                    <div class="col-sm-6 paddingRight0">
                                                                        <asp:TextBox runat="server" Style="text-transform: uppercase" CssClass="form-control" ID="txtCCCompany" OnTextChanged="txtCCCompany_TextChanged" AutoPostBack="true" />
                                                                    </div>
                                                                    <div class="col-sm-1 padding3">
                                                                        <asp:LinkButton ID="lbtnSeCCCompany" runat="server" OnClick="lbtnSeCCCompany_Click">
                                                                    <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-4 ">
                                                                    <div class="col-sm-3 padding0  labelText1">
                                                                        Calendar
                                                                    </div>
                                                                    <div class="col-sm-8 paddingRight0">
                                                                        <asp:TextBox runat="server"  CssClass="form-control" ID="txtCCCalenderCode" OnTextChanged="txtCCCalenderCode_TextChanged" AutoPostBack="true" />
                                                                    </div>
                                                                    <div class="col-sm-1 padding3">
                                                                        <asp:LinkButton ID="lbtnSeCCCalendar" runat="server" OnClick="lbtnSeCCCalendar_Click">
                                                                    <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-4">
                                                                    <div class="col-sm-5 labelText1">
                                                                        Year From
                                                                    </div>
                                                                    <div class="col-sm-7">
                                                                        <asp:TextBox runat="server" CssClass="txtYearFrom form-control" ID="txtYearFrom" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="col-sm-3 labelText1 padding0">
                                                                        Year To
                                                                    </div>
                                                                    <div class="col-sm-7">
                                                                        <asp:TextBox runat="server" CssClass="txtYearTo form-control" ID="txtYearTo" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-4">
                                                                    <div class="col-sm-5 labelText1">
                                                                        Start Month
                                                                    </div>
                                                                    <div class="col-sm-7">
                                                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control">
                                                                            <asp:ListItem Value="1">January</asp:ListItem>
                                                                            <asp:ListItem Value="2">February</asp:ListItem>
                                                                            <asp:ListItem Value="3">March</asp:ListItem>
                                                                            <asp:ListItem Value="4">April</asp:ListItem>
                                                                            <asp:ListItem Value="5">May</asp:ListItem>
                                                                            <asp:ListItem Value="6">June</asp:ListItem>
                                                                            <asp:ListItem Value="7">July</asp:ListItem>
                                                                            <asp:ListItem Value="8">August</asp:ListItem>
                                                                            <asp:ListItem Value="9">September</asp:ListItem>
                                                                            <asp:ListItem Value="10">October</asp:ListItem>
                                                                            <asp:ListItem Value="11">November</asp:ListItem>
                                                                            <asp:ListItem Value="12">December</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="col-sm-3 padding0 labelText1">
                                                                        End Month
                                                                    </div>
                                                                    <div class="col-sm-7">
                                                                        <asp:DropDownList ID="ddlMonthEnd" runat="server" CssClass="form-control">
                                                                            <asp:ListItem Value="1">January</asp:ListItem>
                                                                            <asp:ListItem Value="2">February</asp:ListItem>
                                                                            <asp:ListItem Value="3">March</asp:ListItem>
                                                                            <asp:ListItem Value="4">April</asp:ListItem>
                                                                            <asp:ListItem Value="5">May</asp:ListItem>
                                                                            <asp:ListItem Value="6">June</asp:ListItem>
                                                                            <asp:ListItem Value="7">July</asp:ListItem>
                                                                            <asp:ListItem Value="8">August</asp:ListItem>
                                                                            <asp:ListItem Value="9">September</asp:ListItem>
                                                                            <asp:ListItem Value="10">October</asp:ListItem>
                                                                            <asp:ListItem Value="11">November</asp:ListItem>
                                                                            <asp:ListItem Value="12">December</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-7">
                                                                    <div class="col-sm-3 labelText1">
                                                                        Description
                                                                    </div>
                                                                    <div class="col-sm-8 paddingLeft5">
                                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtCCDesc" />
                                                                    </div>

                                                                </div>
                                                                <div class="col-sm-4 paddingLeft0">
                                                                    <div class="col-sm-1 padding0" style="margin-top: 2PX;">
                                                                        <asp:CheckBox Text="" ID="chkCCActive" runat="server" Checked="true" />
                                                                    </div>
                                                                    <div class="col-sm-10 labelText1 padding3">
                                                                        <asp:Label Text="Active" runat="server" />
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
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <%-- pnl Combine Company --%>
    <asp:UpdatePanel runat="server" ID="UpdatePanel4">
        <ContentTemplate>
            <asp:Button ID="btn3" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupComCompany" runat="server" Enabled="True" TargetControlID="btn3"
                PopupControlID="pnlCombineCom" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
     <asp:UpdateProgress ID="UpdateProgress5" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="UpdatePanel5">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait15" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait15" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="UpdatePanel5">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlCombineCom">
                <div runat="server" id="Div2" class="panel panel-primary" style="padding: 5px;">
                    <div class="panel panel-default" style="width: 650px;">
                        <div class="panel-heading" style="height: 25px;">
                            <div class="col-sm-11 padding0">
                                <strong>Combine Company</strong>
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel panel-body padding0">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="row buttonRow">
                                                <div class="col-sm-12">
                                                    <div class="col-sm-7">
                                                    </div>

                                                    <div class="col-sm-5 padding0">
                                                        <div class="col-sm-4">
                                                            <asp:LinkButton ID="lbtnCCBSave" OnClientClick="return ConfSave()" runat="server" OnClick="lbtnCCBSave_Click">
                                                      <span class="glyphicon glyphicon-saved"  aria-hidden="true"></span>Save
                                                            </asp:LinkButton>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <asp:LinkButton ID="lbtnCCBClear" OnClientClick="return ConfClear();" runat="server" OnClick="lbtnCCBClear_Click">
                                                      <span class="glyphicon glyphicon-refresh"   aria-hidden="true"></span>Clear
                                                            </asp:LinkButton>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <asp:LinkButton ID="lbtnCCBClose"  runat="server" OnClick="lbtnCCBClose_Click">
                                                    <span class="glyphicon glyphicon-remove"  aria-hidden="true"></span>Close
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="panel panel-default">
                                                        <div class="panel panel-body padding0">
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="panel panel-default">
                                                                        <div class="panel panel-body">
                                                                            <div class="row" style="margin-top: 5px;">
                                                                                <div class="col-sm-5 paddingRight0">
                                                                                    <div class="col-sm-4 labelText1">
                                                                                        Calendar
                                                                                    </div>
                                                                                    <div class="col-sm-7 paddingRight0">
                                                                                        <asp:TextBox runat="server" Style="text-transform: uppercase" CssClass="form-control" ID="txtCCBCalender" OnTextChanged="txtCCBCalender_TextChanged" AutoPostBack="true" />
                                                                                    </div>
                                                                                    <div class="col-sm-1 padding3">
                                                                                        <asp:LinkButton ID="lbtnSeCCBCal" runat="server" OnClick="lbtnSeCCBCal_Click">
                                                                    <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-4 paddingLeft0">
                                                                                    <div class="col-sm-12 labelText1">
                                                                                        <asp:Label Text="" ForeColor="DarkBlue" ID="lblCCBDesc" runat="server" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row" style="margin-top: 5px;">
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-3 padding0">
                                                                                        <div class="col-sm-5 padding0 labelText1">
                                                                                            Start Year :
                                                                                        </div>
                                                                                        <div class="col-sm-7 padding0 labelText1">
                                                                                            <asp:Label ID="lblCCBStart" Text="" runat="server" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-3 padding0">
                                                                                        <div class="col-sm-5 padding0 labelText1">
                                                                                            Start Month :
                                                                                        </div>
                                                                                        <div class="col-sm-7 padding0 labelText1">
                                                                                            <asp:Label ID="lblCCBStartMonth" Text="" runat="server" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-3 padding0">
                                                                                        <div class="col-sm-5 padding0 labelText1">
                                                                                            End Year :
                                                                                        </div>
                                                                                        <div class="col-sm-7 padding0 labelText1">
                                                                                            <asp:Label ID="lblCCBEnd" Text="" runat="server" />
                                                                                        </div>
                                                                                    </div>

                                                                                    <div class="col-sm-3 padding0">
                                                                                        <div class="col-sm-5 padding0 labelText1">
                                                                                            Start Month :
                                                                                        </div>
                                                                                        <div class="col-sm-7 padding0 labelText1">
                                                                                            <asp:Label ID="lblCCBEndMonth" Text="" runat="server" />
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="panel panel-default">
                                                                        <div class="panel panel-body">
                                                                            <div class="row" style="margin-top: 5px;">
                                                                                <div class="col-sm-4">
                                                                                    <div class="col-sm-5 labelText1">
                                                                                        Company
                                                                                    </div>
                                                                                    <div class="col-sm-6 paddingRight0">
                                                                                        <asp:TextBox runat="server" Style="text-transform: uppercase" CssClass="form-control" ID="txtCCBCompany" OnTextChanged="txtCCBCompany_TextChanged" AutoPostBack="true" />
                                                                                    </div>
                                                                                    <div class="col-sm-1 padding3">
                                                                                        <asp:LinkButton ID="lbtnSeCCBCom" runat="server" OnClick="lbtnSeCCBCom_Click">
                                                                                            <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-sm-6">
                                                                                    <div class="col-sm-10 paddingRight0">
                                                                                        <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="txtCCBCompDesc" />
                                                                                    </div>
                                                                                    <div class="col-sm-1 padding3">
                                                                                        <asp:LinkButton ID="lbtnCCBComAdd" runat="server" OnClick="lbtnCCBComAdd_Click">
                                                                                            <span class="glyphicon glyphicon-arrow-down"  aria-hidden="true"></span>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div style="height: 150px;">
                                                                        <asp:GridView ID="dgvCCBCompany" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                            EmptyDataText="No data found..." AutoGenerateColumns="False" AllowPaging="True" PageSize="5"
                                                                            OnPageIndexChanging="dgvCCBCompany_PageIndexChanging"
                                                                            PagerStyle-CssClass="cssPager">
                                                                            <Columns>

                                                                                <asp:TemplateField HeaderText="Company Code">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSfm_alw_com" Text='<%# Bind("Sfm_alw_com") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="30px" />
                                                                                    <HeaderStyle Width="30px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSfm_cale_cd" Text='<%# Bind("Sfm_cale_cd") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="30px" />
                                                                                    <HeaderStyle Width="30px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Description">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbltmp_com_desc" Text='<%# Bind("tmp_com_desc") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="200px" />
                                                                                    <HeaderStyle Width="200px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Active">
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox AutoPostBack="true" ID="chkCCBComActive" Checked='<%#Convert.ToBoolean(Eval("Sfm_act")) %>' OnCheckedChanged="chkCCBComActive_CheckedChanged" Text="" runat="server" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="30px" />
                                                                                    <HeaderStyle Width="30px" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
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
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <%-- pnl Create Target--%>
    <asp:UpdatePanel runat="server" ID="UpdatePanel6">
        <ContentTemplate>
            <asp:Button ID="btn4" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupCrePer" runat="server" Enabled="True" TargetControlID="btn4"
                PopupControlID="pnlCrePer" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress6" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="UpdatePanel7">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait17" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait17" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="UpdatePanel7">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlCrePer">
                <div runat="server" id="Div3" class="panel panel-primary" style="padding: 5px;">
                    <div class="panel panel-default" style="width: 650px;">
                        <div class="panel-heading" style="height: 25px;">
                            <div class="col-sm-11 padding0">
                                <strong>Create Period</strong>
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel panel-body padding0">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="row buttonRow">
                                                <div class="col-sm-12">
                                                    <div class="col-sm-7">
                                                    </div>

                                                    <div class="col-sm-5 padding0">
                                                        <div class="col-sm-4">
                                                            <asp:LinkButton ID="lbtnCPSave" OnClientClick="return ConfSave()" runat="server" OnClick="lbtnCPSave_Click">
                                                      <span class="glyphicon glyphicon-saved"  aria-hidden="true"></span>Save
                                                            </asp:LinkButton>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <asp:LinkButton ID="lbtnCPClear" OnClientClick="return ConfClear();" runat="server" OnClick="lbtnCPClear_Click">
                                                      <span class="glyphicon glyphicon-refresh"   aria-hidden="true"></span>Clear
                                                            </asp:LinkButton>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <asp:LinkButton ID="lbtnCPClose"  runat="server" OnClick="lbtnCPClose_Click">
                                                    <span class="glyphicon glyphicon-remove"  aria-hidden="true"></span>Close
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="panel panel-default">
                                                        <div class="panel panel-body padding0">
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="panel panel-default">
                                                                        <div class="panel panel-body">
                                                                            <div class="row" style="">
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-5 padding0">
                                                                                        <div class="col-sm-4 labelText1">
                                                                                            Company
                                                                                        </div>
                                                                                        <div class="col-sm-7 padding0">
                                                                                            <asp:TextBox runat="server" Style="text-transform: uppercase" CssClass="form-control" ID="txtCPCom" OnTextChanged="txtCPCom_TextChanged" AutoPostBack="true" />
                                                                                        </div>
                                                                                        <div class="col-sm-1 padding3">
                                                                                            <asp:LinkButton ID="lbtnSeCPCompny" runat="server" OnClick="lbtnSeCPCompny_Click">
                                                                                            <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-3 paddingRight0">
                                                                                        <asp:Label Text="" ForeColor="DarkBlue" ID="lblCPComp" runat="server" />
                                                                                    </div>
                                                                                    <div class="col-sm-4 padding0">
                                                                                        <div class="col-sm-4 labelText1">
                                                                                            Code
                                                                                        </div>
                                                                                        <div class="col-sm-7 padding0">
                                                                                            <asp:TextBox runat="server" Style="text-transform: uppercase" CssClass="form-control"
                                                                                                ID="txtCPCode" OnTextChanged="txtCPCode_TextChanged" Enabled="true" AutoPostBack="true" />
                                                                                        </div>
                                                                                        <div class="col-sm-1 padding3">
                                                                                            <asp:LinkButton ID="lbtnSeCPCode" runat="server" OnClick="lbtnSeCPCode_Click">
                                                                                            <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-5 padding0">
                                                                                        <div class="col-sm-4 labelText1">
                                                                                            Calendar
                                                                                        </div>
                                                                                        <div class="col-sm-7 padding0">
                                                                                            <asp:TextBox runat="server" Style="text-transform: uppercase" CssClass="form-control" ID="txtCPCalender" OnTextChanged="txtCPCalender_TextChanged" AutoPostBack="true" />
                                                                                        </div>
                                                                                        <div class="col-sm-1 padding3">
                                                                                            <asp:LinkButton ID="lbtnSeCPCalendar" runat="server" OnClick="lbtnSeCPCalendar_Click">
                                                                                            <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-4 paddingRight0 labelText1">
                                                                                        <asp:Label Text="" ForeColor="DarkBlue" ID="lblCPCalender" runat="server" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-5 padding0">
                                                                                        <div class="col-sm-4 labelText1">
                                                                                            Period Type
                                                                                        </div>
                                                                                        <div class="col-sm-5 padding0">
                                                                                            <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="ddlCPTp_SelectedIndexChanged" AppendDataBoundItems="true" runat="server" CssClass="form-control" ID="ddlCPTp">
                                                                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </div>
                                                                                        <div class="col-sm-1 padding3">
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-7">
                                                                                        <asp:Label Text="" ForeColor="DarkBlue" ID="lblCPduration" runat="server"  />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-5 padding0">
                                                                                        <div class="col-sm-4 labelText1">
                                                                                            Parent Code
                                                                                        </div>
                                                                                        <div class="col-sm-7 padding0">
                                                                                            <asp:TextBox runat="server" Style="text-transform: uppercase" CssClass="form-control" ID="txtCPParCode"
                                                                                                OnTextChanged="txtCPParCode_TextChanged" AutoPostBack="true" />
                                                                                        </div>
                                                                                        <div class="col-sm-1 padding3">
                                                                                            <asp:LinkButton ID="lbtnSeCPParCd" runat="server" OnClick="lbtnSeCPParCd_Click">
                                                                                            <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-4 paddingRight0 labelText1">
                                                                                        <asp:Label Text="" ForeColor="DarkBlue" ID="lblCPParDes" runat="server" />
                                                                                    </div>

                                                                                </div>
                                                                            </div>

                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-5 padding0">
                                                                                        <div class="col-sm-4 labelText1">
                                                                                            Period From
                                                                                        </div>
                                                                                        <div class="col-sm-4 padding0">
                                                                                            <asp:TextBox runat="server" Enabled="false" Style="text-transform: uppercase" CssClass="form-control" ID="txtCPFrom" />
                                                                                            <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtCPFrom"
                                                                                                PopupButtonID="btnCPFrom" Format="dd/MMM/yyyy">
                                                                                            </asp:CalendarExtender>
                                                                                        </div>
                                                                                        <div class="col-sm-1 padding3">
                                                                                            <asp:LinkButton ID="btnCPFrom" runat="server">
                                                                                            <span class="glyphicon glyphicon-calendar"  aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-5 padding0">
                                                                                        <div class="col-sm-4 labelText1">
                                                                                            Period To
                                                                                        </div>
                                                                                        <div class="col-sm-4 padding0">
                                                                                            <asp:TextBox runat="server" Enabled="false" Style="text-transform: uppercase" CssClass="form-control" ID="txtCPTo" />
                                                                                            <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtCPTo"
                                                                                                PopupButtonID="btnCPTo" Format="dd/MMM/yyyy">
                                                                                            </asp:CalendarExtender>
                                                                                        </div>
                                                                                        <div class="col-sm-1 padding3">
                                                                                            <asp:LinkButton ID="btnCPTo" runat="server">
                                                                                            <span class="glyphicon glyphicon-calendar"  aria-hidden="true"></span>
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <div class="col-sm-5 padding0">
                                                                                        <div class="labelText1 col-sm-4">
                                                                                            Description
                                                                                        </div>
                                                                                        <div class="col-sm-8 padding0">
                                                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtCPDesc" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-sm-3 labelText1 padding0">
                                                                                        <div class="col-sm-1 ">
                                                                                            <asp:CheckBox ID="chkCPActive" Text="" Checked="true" runat="server" />
                                                                                        </div>
                                                                                        <div class="col-sm-8 padding3 ">
                                                                                            <asp:Label Text="Active" runat="server" />
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
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <%-- pnl search --%>
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
                                        <asp:TextBox ID="txtSerByKey" CausesValidation="false" class="form-control" AutoPostBack="true"  runat="server" OnTextChanged="lbtnSearch_Click" ></asp:TextBox>
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


    <asp:UpdatePanel runat="server" ID="UpdatePanel20">
        <ContentTemplate>
            <asp:Button ID="pnlBtn" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="PopupSearch2" runat="server" Enabled="True" TargetControlID="pnlBtn"
                PopupControlID="pnlSearch2" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel runat="server" ID="pnlSearch2" DefaultButton="lbtnSearch">
        <div runat="server" id="Div6" class="panel panel-primary" style="padding: 5px;">
            <div class="panel panel-default" style="height: 350px; width: 700px;">
                <div class="panel-heading" style="height: 25px;">
                    <div class="col-sm-11"></div>
                    <div class="col-sm-1">
                        <asp:LinkButton ID="lbtnPop" runat="server" OnClick="lbtnPop_Click">
                             <span class="glyphicon glyphicon-remove"  aria-hidden="true">Close</span>
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="row height16">
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="col-sm-2 labelText1">
                                From
                            </div>
                            <div class="col-sm-4 paddingRight5">
                                <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox runat="server" ID="txtCPFromDt" Enabled="false" Style="text-transform: uppercase" CssClass="form-control" ></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtCPFromDt"
                                            PopupButtonID="btnCPFromDt" Format="dd/MMM/yyyy">
                                        </asp:CalendarExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy3" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-1 paddingLeft0">
                                        <asp:LinkButton ID="btnCPFromDt" runat="server">
                                            <span class="glyphicon glyphicon-calendar"  aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>
                                </ContentTemplate>

                            </asp:UpdatePanel>
                        </div>
                        <div class="col-sm-6">
                            <div class="col-sm-4 labelText1">
                                Search by key
                            </div>
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy4" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-6 paddingRight5">
                                        <asp:DropDownList ID="ddlSerByKey2" runat="server" class="form-control" AutoPostBack="false">
                                        </asp:DropDownList>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>


                            
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="col-sm-2 labelText1">
                                To
                            </div>
                            <div class="col-sm-4 paddingRight5">
                                <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox runat="server" ID="txtCPToDt" Enabled="false" Style="text-transform: uppercase" CssClass="form-control"  ></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtCPToDt"
                                            PopupButtonID="btnCPToDt" Format="dd/MMM/yyyy">
                                        </asp:CalendarExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy5" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-1 paddingLeft0">
                                        <asp:LinkButton ID="btnCPToDt" runat="server">
                                            <span class="glyphicon glyphicon-calendar"  aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>

                                    <div class="col-sm-1 paddingLeft0">
                                        <asp:LinkButton ID="lbtnCPDt" runat="server" OnClick="lbtnCPDtSer_Click">
                                            <span class="glyphicon glyphicon-search"  aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>
                                </ContentTemplate>

                            </asp:UpdatePanel>
                        </div>
                        <div class="col-sm-6">
                            <div class="col-sm-4 labelText1">
                                Search by word
                            </div>
                            <div class="col-sm-6 paddingRight5">
                                <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtSerByKey2" CausesValidation="false" class="form-control" OnTextChanged="lbtnSearch2_Click" AutoPostBack="true"  runat="server"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy6" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-1 paddingLeft0">
                                        <asp:LinkButton ID="lbtnSearch2" runat="server" OnClick="lbtnSearch2_Click">
                                     <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>
                                </ContentTemplate>

                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="txtSerByKey2" />
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
                            <asp:ScriptManagerProxy ID="ScriptManagerProxy7" runat="server"></asp:ScriptManagerProxy>
                            <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="dgvResult2" CausesValidation="false" runat="server" AllowPaging="True"
                                        GridLines="None" CssClass="table table-hover table-striped" PageSize="10" PagerStyle-CssClass="cssPager" ShowHeaderWhenEmpty="true"
                                        EmptyDataText="No data found..."
                                         OnPageIndexChanging="dgvResult2_PageIndexChanging" OnSelectedIndexChanged="dgvResult2_SelectedIndexChanged">
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

    <%-- pnl excel Upload --%>
     <asp:UpdatePanel ID="upExcelUpload" runat="server">
        <ContentTemplate>
            <asp:Button ID="btn10" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popupExcel" runat="server" Enabled="True" TargetControlID="btn10"
                PopupControlID="pnlExcelUpload" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel runat="server" ID="pnlExcelUpload">
        <div class="panel panel-default height45 width700 ">
            <div class="panel panel-default">
                <div class="panel-heading height30">
                    <div class="col-sm-11">
                        Excel Upload
                    </div>
                    <div class="col-sm-1">
                        <asp:LinkButton ID="lbtnExcelUploadClose" runat="server" OnClick="lbtnExcelUploadClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                        </asp:LinkButton>
                    </div>
                    <%--<span>Commen Search</span>--%>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12 paddingLeft0 paddingRight0">
                        <div class="row height22">
                            <div class="col-sm-11">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lblExcelUploadError" runat="server" ForeColor="Red" Text="Label" Visible="false"></asp:Label>
                                        <asp:Label ID="lblExcelUploadSuccess" runat="server" ForeColor="Green" Visible="false"></asp:Label>
                                        <asp:Label ID="lblExcelUploadInfo" runat="server" ForeColor="Blue" Visible="false"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="row">
                            <asp:Panel runat="server">
                                <div class="col-sm-12">
                                    <div class="col-sm-10 paddingRight5">
                                        <asp:FileUpload ID="fileUploadExcel" runat="server" />
                                    </div>
                                    <div class="col-sm-2 paddingRight5">
                                        <asp:Button ID="lbtnUploadExcel" class="btn btn-warning btn-xs" runat="server" Text="Upload"
                                            OnClick="lbtnUploadExcel_Click" />
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <%-- Pnl Process  --%>
    <asp:UpdatePanel ID="upProcess" runat="server">
        <ContentTemplate>
            <asp:Button ID="btn11" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popupExcProc" runat="server" Enabled="True" TargetControlID="btn11"
                PopupControlID="pnlExcelProcces" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress2" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="UpdatePanel13">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait1" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait1" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlExcelProcces">
                <div runat="server" class="panel panel-default height45 width700 ">
                    <div class="panel panel-default">
                        <div class="panel-heading height30">

                            <div class="col-sm-11">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lblExcelProccesError" runat="server" ForeColor="Red" Text="Label" Visible="false"></asp:Label>
                                        <asp:Label ID="lblExcelProccesSuccess" runat="server" ForeColor="Green" Visible="false"></asp:Label>
                                        <asp:Label ID="lblExcelProccesInfo" runat="server" ForeColor="Blue" Visible="false"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-1">
                                <asp:LinkButton ID="lbtnProcClose" runat="server" OnClick="lbtnProcClose_Click">
                             <span class="glyphicon glyphicon-remove" aria-hidden="true">Close</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-12 paddingLeft0 paddingRight0">
                                <div class="row">
                                    <asp:Panel runat="server" ID="Panel3">
                                        <div class="col-sm-12 ">
                                            <div id="" class="alert alert-info alert-success" role="alert">
                                                <div class="col-sm-1 padding0">
                                                    <strong>Alert!</strong>
                                                </div>
                                                <div class="col-sm-10 padding0">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <asp:Label ID="lblProcess" Text="Excel file upload completed. Do you want to process ?" runat="server"></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-sm-1 padding0">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <asp:Button ID="lbtnExcelProcess" class="btn btn-success btn-xs" runat="server" Text="Process" OnClick="lbtnExcelProcess_Click" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <%-- pnl show error excel data --%>
    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
        <ContentTemplate>
            <asp:Button ID="btn15" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popupErro" runat="server" Enabled="True" TargetControlID="btn15"
                PopupControlID="pnlExcelErro" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server" ID="UpdatePanel12">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlExcelErro">
                <div runat="server" id="Div4" class="panel panel-primary" style="padding: 5px;">
                    <div class="panel panel-default" style="width: 1100px;">
                        <div class="panel-heading" style="height: 25px;">
                            <div class="col-sm-11 padding0">
                                <strong>Excel Incorrect Data</strong>
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel panel-body padding0">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="row buttonRow">
                                                <div class="col-sm-12">
                                                    <div class="col-sm-7">
                                                    </div>

                                                    <div class="col-sm-5 padding0">
                                                        <div class="col-sm-4">
                                                            
                                                        </div>
                                                        <div class="col-sm-4">
                                                           <%-- <asp:LinkButton ID="lbtnExcSave" OnClientClick="return ConfSave()" runat="server" OnClick="lbtnExcSave_Click">
                                                      <span class="glyphicon glyphicon-saved"  aria-hidden="true"></span>Save
                                                            </asp:LinkButton>--%>
                                                        </div>
                                                        <div class="col-sm-4 text-right">
                                                            <asp:LinkButton ID="lbtnExcClose"  runat="server" OnClick="lbtnExcClose_Click">
                                                    <span class="glyphicon glyphicon-remove"  aria-hidden="true"></span>Close
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="panel panel-default">
                                                        <div class="panel panel-body padding0">
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div style="height: 400px; overflow-y:auto;">
                                                                        <asp:GridView ID="dgvExcel" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                            EmptyDataText="No data found..." AutoGenerateColumns="False" 
                                                                            OnPageIndexChanging="dgvCCBCompany_PageIndexChanging"
                                                                            PagerStyle-CssClass="cssPager">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Item">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSfd_itm" Text='<%# Bind("Sfd_itm") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="80px" />
                                                                                    <HeaderStyle Width="80px" />
                                                                                </asp:TemplateField>
                                                                                
                                                                                <asp:TemplateField HeaderText="Brand">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSfd_brnd" Text='<%# Bind("Sfd_brnd") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="60px" />
                                                                                    <HeaderStyle Width="60px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Model">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSfd_model" Text='<%# Bind("Sfd_model") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="60px" />
                                                                                    <HeaderStyle Width="60px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Main Category">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSfd_cat1" Text='<%# Bind("Sfd_cat1") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="60px" />
                                                                                    <HeaderStyle Width="60px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Sub Category">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSfd_cat2" Text='<%# Bind("Sfd_cat2") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="60px" />
                                                                                    <HeaderStyle Width="60px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Category 3">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSfd_cat3" Text='<%# Bind("Sfd_cat3") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="60px" />
                                                                                    <HeaderStyle Width="60px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Quantity">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSfd_qty" Text='<%# Bind("Sfd_qty","{0:N2}") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="60px" CssClass="gridHeaderAlignRight" />
                                                                                    <HeaderStyle Width="60px" CssClass="gridHeaderAlignRight" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Value">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSfd_val" Text='<%# Bind("Sfd_val","{0:N2}") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="60px" CssClass="gridHeaderAlignRight" />
                                                                                    <HeaderStyle Width="60px" CssClass="gridHeaderAlignRight" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="GP %">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSfd_gp" Text='<%# Bind("Sfd_gp","{0:N2}") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="50px" CssClass="gridHeaderAlignRight" />
                                                                                    <HeaderStyle Width="50px" CssClass="gridHeaderAlignRight" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Line No" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSfd_line" Text='<%# Bind("Sfd_line") %>' runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="def On" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbltmp_def_on" Text='<%# Bind("tmp_def_on") %>' runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="def On" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSfd_seq" Text='<%# Bind("Sfd_seq") %>' runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="" >
                                                                                    <ItemTemplate>
                                                                                        <asp:Label Text='' Width="5px" runat="server" />
                                                                                    </ItemTemplate>
                                                                                     <ItemStyle Width="5px" CssClass="" />
                                                                                    <HeaderStyle Width="5px" CssClass="" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Error Description">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbltmp_err_desc" Text='<%# Bind("tmp_err_desc") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="160px" />
                                                                                    <HeaderStyle Width="160px" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
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
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
   
    <%-- pnl excel save --%>
     <asp:UpdatePanel ID="UpdatePanel14" runat="server">
        <ContentTemplate>
            <asp:Button ID="btn16" runat="server" Text="Button" Style="display: none;" />
            <asp:ModalPopupExtender ID="popupExcelSave" runat="server" Enabled="True" TargetControlID="btn16"
                PopupControlID="pnlExcSave" PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
     <asp:UpdateProgress ID="UpdateProgress3" DisplayAfter="10" runat="server" AssociatedUpdatePanelID="UpdatePanel15">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait3" runat="server"
                    Text="Please wait... " />
                <asp:Image ID="imgWait3" runat="server"
                    ImageAlign="Middle" ImageUrl="~/images/icons/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel runat="server" ID="UpdatePanel15">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlExcSave">
                <div runat="server" id="Div5" class="panel panel-primary" style="padding: 5px;">
                    <div class="panel panel-default" style="width: 1100px;">
                        <div class="panel-heading" style="height: 25px;">
                            <div class="col-sm-11 padding0">
                                <strong>Excel Data</strong>
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="panel panel-body padding0">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="row buttonRow">
                                                <div class="col-sm-12">
                                                    <div class="col-sm-7">
                                                    </div>

                                                    <div class="col-sm-5 padding0">
                                                        <div class="col-sm-8">
                                                            
                                                        </div>
                                                        <div class="col-sm-2 padding0 text-center">
                                                           <asp:LinkButton ID="lbtnExcSave" OnClientClick="return ConfSave()" runat="server" OnClick="lbtnExcSave_Click">
                                                      <span class="glyphicon glyphicon-saved"  aria-hidden="true"></span>Save
                                                            </asp:LinkButton>
                                                        </div>
                                                        <div class="col-sm-2 padding0 text-center">
                                                            <asp:LinkButton ID="lbtnExcSaveClos" OnClientClick="return ConfClose();" runat="server" OnClick="lbtnExcSaveClos_Click">
                                                    <span class="glyphicon glyphicon-remove"  aria-hidden="true"></span>Close
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="panel panel-default">
                                                        <div class="panel panel-body padding0">
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div style="height: 400px; overflow-y:auto;">
                                                                        <asp:GridView ID="dgvTarSave" CssClass="table table-hover table-striped" runat="server" GridLines="None" ShowHeaderWhenEmpty="True"
                                                                            EmptyDataText="No data found..." AutoGenerateColumns="False" 
                                                                            OnPageIndexChanging="dgvCCBCompany_PageIndexChanging"
                                                                            PagerStyle-CssClass="cssPager">
                                                                            <Columns>
                                                                                 <asp:TemplateField HeaderText="Def By">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbltmp_def_by" Text='<%# Bind("tmp_def_by") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="40px" />
                                                                                    <HeaderStyle Width="40px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Def Cd">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbltmp_def_cd" Text='<%# Bind("tmp_def_cd") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="40px" />
                                                                                    <HeaderStyle Width="40px" />
                                                                                </asp:TemplateField>
                                                                                 <asp:TemplateField HeaderText="Def On">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbltmp_def_on" Text='<%# Bind("tmp_def_on") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="40px" />
                                                                                    <HeaderStyle Width="40px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Item">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSfd_itm" Text='<%# Bind("Sfd_itm") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="80px" />
                                                                                    <HeaderStyle Width="80px" />
                                                                                </asp:TemplateField>
                                                                                
                                                                                <asp:TemplateField HeaderText="Brand">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSfd_brnd" Text='<%# Bind("Sfd_brnd") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="60px" />
                                                                                    <HeaderStyle Width="60px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Model">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSfd_model" Text='<%# Bind("Sfd_model") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="60px" />
                                                                                    <HeaderStyle Width="60px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Main Category">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSfd_cat1" Text='<%# Bind("Sfd_cat1") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="60px" />
                                                                                    <HeaderStyle Width="60px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Sub Category">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSfd_cat2" Text='<%# Bind("Sfd_cat2") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="60px" />
                                                                                    <HeaderStyle Width="60px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Category 3">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSfd_cat3" Text='<%# Bind("Sfd_cat3") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="60px" />
                                                                                    <HeaderStyle Width="60px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Quantity">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSfd_qty" Text='<%# Bind("Sfd_qty","{0:N2}") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="60px" CssClass="gridHeaderAlignRight" />
                                                                                    <HeaderStyle Width="60px" CssClass="gridHeaderAlignRight" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Value">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSfd_val" Text='<%# Bind("Sfd_val","{0:N2}") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="60px" CssClass="gridHeaderAlignRight" />
                                                                                    <HeaderStyle Width="60px" CssClass="gridHeaderAlignRight" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="GP %">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSfd_gp" Text='<%# Bind("Sfd_gp","{0:N2}") %>' runat="server" Width="100%" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="50px" CssClass="gridHeaderAlignRight" />
                                                                                    <HeaderStyle Width="50px" CssClass="gridHeaderAlignRight" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Line No" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSfd_line" Text='<%# Bind("Sfd_line") %>' runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="def On" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbltmp_def_on" Text='<%# Bind("tmp_def_on") %>' runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="def On" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSfd_seq" Text='<%# Bind("Sfd_seq") %>' runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="" >
                                                                                    <ItemTemplate>
                                                                                        <asp:Label Text='' Width="5px" runat="server" />
                                                                                    </ItemTemplate>
                                                                                     <ItemStyle Width="5px" CssClass="" />
                                                                                    <HeaderStyle Width="5px" CssClass="" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
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
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script>

        Sys.Application.add_load(func);
        function func() {
            if (typeof jQuery == 'undefined') {
                alert('jQuery is not loaded');
            }
            $('.txtYearFrom , .txtYearTo').mousedown(function (e) {
                if (e.button == 2) {
                    alert('This functionality is disabled !');
                    return false;
                } else {
                    return true;
                }
            });
            $('.txtQty , .txtVal , .txtGp ').mousedown(function (e) {
                if (e.button == 2) {
                    alert('This functionality is disabled !');
                    return false;
                } else {
                    return true;
                }
            });

            $('.txtYearFrom , .txtYearTo').keypress(function (evt) {
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
                    alert(charCode);
                    //alert('Maximum 4 characters are allowed ');
                    return false;
                }
            });
            $(document).ready(function () {
               // console.log('redy doc');
                maintainScrollPosition();
            });

            $('.txtQty ,.txtVal,.txtGp').keypress(function (evt) {
                // var ch = (evt.which) ? evt.which : evt.keyCode;
                var ch = evt.which;
                var str = $(this).val();
                console.log(ch);
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

            $('.txtGp').keyup(function (evt) {
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                var str = $(this).val();
                if (charCode != 9) {
                    if (Number(str) > 100) {
                        if (str.indexOf(".") == -1) {
                            str = str.slice(0, -1);
                        } else {
                            str = str.slice(0, -4);
                        }
                        $(this).val(str);
                        alert('Maximum allowed gp is 100 !');
                    }
                }
            });
        }
    </script>
</asp:Content>
